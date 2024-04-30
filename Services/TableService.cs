using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using SuperbetBeclean.Model;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Services
{
    public class TableService : ITableService
    {
        private string tableType;
        private static Mutex mutex;
        private const int FULL = 8;
        private const int EMPTY = 0;
        private const int PREFLOP = 0;
        private const int FLOP = 1;
        private const int TURN = 2;
        private const int RIVER = 3;
        private const int INACTIVE = 0;
        private const int WAITING = 1;
        private const int FOLDED = 2;
        private const int PLAYING = 3;
        private bool ended = false;
        private int buyIn;
        private int smallBlind;
        private int bigBlind;
        private List<MenuWindow> users;
        private Deck deck; // all the cards that are not in the players hands
        private DBService dbService;
        private Random random;
        private HandRankCalculator rankCalculator;

        private Card[] communityCards;
        private int[] freeSpace;
        public TableService(int buyIn, int smallBlind, int bigBlind, string tableType, DBService dbService)
        {
            this.dbService = dbService;
            this.tableType = tableType;
            users = new List<MenuWindow>();
            rankCalculator = new HandRankCalculator();

            this.buyIn = buyIn;
            this.smallBlind = smallBlind;
            this.bigBlind = bigBlind;

            communityCards = new Card[6];
            freeSpace = new int[9];

            Task.Run(() => RunTable());

            mutex = new Mutex();

            random = new Random();
            this.dbService = dbService;
        }

        public Card GetRandomCardAndRemoveIt()
        {
            int index = random.Next(0, deck.GetDeckSize());
            Card card = deck.GetCardFromIndex(index);
            deck.RemoveCardFromIndex(index);
            return card;
        }

        public Card GenerateCard()
        {
            return GetRandomCardAndRemoveIt();
        }

        public async void RunTable()
        {
            while (true)
            {
                if (ended)
                {
                    break;
                }
                if (IsEmpty())
                {
                    await Task.Delay(3000);
                    continue;
                }
                mutex.WaitOne();
                // start new round with all players
                Queue<MenuWindow> activePlayers = new Queue<MenuWindow>(users);
                mutex.ReleaseMutex();
                foreach (MenuWindow menuWindow in activePlayers)
                {
                    User player = menuWindow.Player();
                    player.UserStatus = PLAYING;
                    player.UserBet = 0;
                    if (player.UserStack == 0)
                    {
                        if (player.UserChips < buyIn)
                        {
                            player.UserStatus = INACTIVE;
                            DisconnectUser(menuWindow);
                        }
                        else
                        {
                            player.UserChips -= buyIn;
                            dbService.UpdateUserChips(player.UserID, player.UserChips);
                            player.UserStack = buyIn;
                            dbService.UpdateUserStack(player.UserID, player.UserStack);
                            menuWindow.UpdateChips(tableType, player);
                        }
                    }
                }
                foreach (MenuWindow currentWindow in activePlayers)
                {
                    User currentPlayer = currentWindow.Player();
                    foreach (MenuWindow otherWindow in activePlayers)
                    {
                        User otherPlayer = otherWindow.Player();
                        currentWindow.StartRound(tableType, otherPlayer);
                    }
                }
                foreach (MenuWindow window in activePlayers)
                {
                    window.ResetCards(tableType);
                }
                if (activePlayers.Count < 2)
                {
                    await Task.Delay(5000);
                    continue;
                }
                deck = new Deck();

                await Task.Delay(1000);
                /// give first card
                foreach (MenuWindow playerWindow in activePlayers)
                {
                    User player = playerWindow.Player();
                    Card card = GenerateCard();
                    player.UserCurrentHand[0] = card;
                    foreach (MenuWindow window in activePlayers)
                    {
                        window.NotifyUserCard(tableType, player, 1, card.Value + card.Suit);
                    }
                    await Task.Delay(400);
                }
                /// give second card
                foreach (MenuWindow playerWindow in activePlayers)
                {
                    User player = playerWindow.Player();
                    Card card = GenerateCard();
                    player.UserCurrentHand[1] = card;
                    foreach (MenuWindow window in activePlayers)
                    {
                        window.NotifyUserCard(tableType, player, 2, card.Value + card.Suit);
                    }
                    await Task.Delay(400);
                }
                for (int i = 1; i <= 5; i++)
                {
                    Card card = GenerateCard();
                    communityCards[i] = card;
                }
                int tablePot = 0;
                int canBetPlayers = activePlayers.Count;
                for (int turn = 0; turn <= 3; turn++)
                {
                    if (activePlayers.Count < 2)
                    {
                        break;
                    }
                    if (turn == PREFLOP)
                    {
                    }
                    else if (turn == FLOP)
                    {
                        for (int cardNumber = 1; cardNumber <= 3; cardNumber++)
                        {
                            foreach (MenuWindow window in activePlayers)
                            {
                                window.NotifyTableCard(tableType, cardNumber, communityCards[cardNumber].Info());
                            }
                            await Task.Delay(400);
                        }
                    }
                    else if (turn == TURN)
                    {
                        foreach (MenuWindow window in activePlayers)
                        {
                            window.NotifyTableCard(tableType, 4, communityCards[4].Info());
                        }
                        await Task.Delay(400);
                    }
                    else if (turn == RIVER)
                    {
                        foreach (MenuWindow window in activePlayers)
                        {
                            window.NotifyTableCard(tableType, 5, communityCards[5].Info());
                        }
                        await Task.Delay(400);
                    }
                    bool turnEnded = false;
                    int currentBet = -1;
                    int currentBetPlayer = -1;
                    while (!turnEnded)
                    {
                        if (canBetPlayers < 2)
                        {
                            break;
                        }
                        MenuWindow currentWindow = activePlayers.Peek();
                        User player = currentWindow.Player();

                        if (player.UserStatus != PLAYING)
                        {
                            activePlayers.Dequeue();
                            continue;
                        }
                        if (player.UserID == currentBetPlayer)
                        {
                            break;
                        }
                        if (player.UserStack == 0)
                        {
                            canBetPlayers--;
                            activePlayers.Enqueue(activePlayers.Dequeue());
                            continue;
                        }
                        int playerBet = await currentWindow.StartTime(tableType, Math.Min(currentBet, player.UserStack + player.UserBet), player.UserStack + player.UserBet);
                        if (playerBet == -1)
                        {
                            activePlayers.Dequeue();
                            Console.WriteLine(player.UserName + " folded!");
                            player.UserStatus = WAITING;
                            player.UserBet = 0;
                            canBetPlayers--;
                            foreach (MenuWindow window in activePlayers)
                            {
                                window.FoldPlayer(tableType, player);
                            }
                        }
                        else
                        {
                            activePlayers.Enqueue(activePlayers.Dequeue());
                            int extraBet = playerBet - player.UserBet;
                            player.UserStack -= extraBet;
                            tablePot += extraBet;
                            dbService.UpdateUserStack(player.UserID, player.UserStack);
                            player.UserBet = playerBet;
                            if (playerBet > currentBet)
                            {
                                currentBet = playerBet;
                                currentBetPlayer = player.UserID;
                            }
                        }
                        foreach (MenuWindow window in activePlayers)
                        {
                            window.Notify(tableType, player, tablePot);
                        }
                    }
                    foreach (MenuWindow currentWindow in activePlayers)
                    {
                        User currentPlayer = currentWindow.Player();
                        foreach (MenuWindow otherWindow in activePlayers)
                        {
                            User otherPlayer = otherWindow.Player();
                            currentWindow.EndTurn(tableType, otherPlayer);
                        }
                        currentPlayer.UserBet = 0;
                    }
                }
                foreach (MenuWindow currentWindow in activePlayers)
                {
                    User currentPlayer = currentWindow.Player();
                    foreach (MenuWindow otherWindow in activePlayers)
                    {
                        User otherPlayer = otherWindow.Player();
                        currentWindow.ShowCards(tableType, otherPlayer);
                    }
                }
                List<User> winners = DetermineWinners(activePlayers);
                foreach (User winner in winners)
                {
                    Console.WriteLine("Winner: " + winner.UserName);
                    winner.UserStack += Convert.ToInt32(tablePot / winners.Count);
                    dbService.UpdateUserStack(winner.UserID, winner.UserStack);
                    for (int i = 1; i <= 6; i++)
                    {
                        foreach (MenuWindow menuWindow in activePlayers)
                        {
                            if (i % 2 == 1)
                            {
                                menuWindow.DisplayWinner(tableType, winner, true);
                            }
                            else
                            {
                                menuWindow.DisplayWinner(tableType, winner, false);
                            }
                        }
                        if (i == 5)
                        {
                            await Task.Delay(2000);
                        }
                        else
                        {
                            await Task.Delay(200);
                        }
                    }
                }
                await Task.Delay(3000);
            }
        }

        private void GenerateHands(List<Card> currentHand, List<Card> possibleCards, int lastCard, int numberCards, List<List<Card>> allHands)
        {
            for (int i = lastCard + 1; i < possibleCards.Count; i++)
            {
                currentHand.Add(possibleCards[i]);
                if (currentHand.Count == numberCards)
                {
                    List<Card> handCopy = new List<Card>(currentHand);
                    allHands.Add(handCopy);
                }
                else
                {
                    GenerateHands(currentHand, possibleCards, i, numberCards, allHands);
                }
                currentHand.Remove(possibleCards[i]);
            }
        }

        public Tuple<int, int> DetermineMaxHand(List<Card> possibleCards)
        {
            Tuple<int, int> maxHandValue = new Tuple<int, int>(0, 0);
            List<List<Card>> allHands = new List<List<Card>>();
            List<Card> currentHand = new List<Card>();
            GenerateHands(currentHand, possibleCards, -1, 5, allHands);
            // Console.WriteLine("Generated hands: " + allHands.Count);
            foreach (List<Card> hand in allHands)
            {
                Tuple<int, int> handValue = rankCalculator.GetValue(hand);
                if (handValue.Item1 > maxHandValue.Item1 || (handValue.Item1 == maxHandValue.Item1 && handValue.Item2 > maxHandValue.Item2))
                {
                    maxHandValue = handValue;
                }
            }
            // Console.WriteLine("Best hand: " + maxHandValue);
            return maxHandValue;
        }

        public List<User> DetermineWinners(Queue<MenuWindow> activePlayers)
        {
            Tuple<int, int> maxHand = new Tuple<int, int>(0, 0);
            List<User> winners = new List<User>();
            Dictionary<User, Tuple<int, int>> results = new Dictionary<User, Tuple<int, int>>();
            foreach (MenuWindow window in activePlayers)
            {
                User player = window.Player();
                List<Card> possibleCards = new List<Card>();
                for (int i = 1; i <= 5; i++)
                {
                    possibleCards.Add(communityCards[i]);
                }
                possibleCards.Add(player.UserCurrentHand[0]);
                possibleCards.Add(player.UserCurrentHand[1]);
                Tuple<int, int> hand = DetermineMaxHand(possibleCards);
                if (hand.Item1 > maxHand.Item1 || (hand.Item1 == maxHand.Item1 && hand.Item2 > maxHand.Item2))
                {
                    maxHand = hand;
                    winners.Clear();
                    winners.Add(player);
                }
                else if (hand.Item1 == maxHand.Item1 && hand.Item2 == maxHand.Item2)
                {
                    winners.Add(player);
                }
            }
            return winners;
        }

        public void DisconnectUser(MenuWindow window)
        {
            User player = window.Player();
            freeSpace[player.UserTablePlace] = 0;
            mutex.WaitOne();
            foreach (MenuWindow windowUser in users)
            {
                windowUser.FoldPlayer(tableType, player);
                windowUser.HidePlayer(tableType, player);
            }
            users.Remove(window);
            mutex.ReleaseMutex();
        }

        public int JoinTable(MenuWindow window, ref SqlConnection sqlConnection)
        {
            if (IsFull())
            {
                return 0;
            }

            User player = window.Player();

            if (player.UserChips < buyIn)
            {
                return -1;
            }

            player.UserChips -= buyIn;
            dbService.UpdateUserChips(player.UserID, player.UserChips);

            player.UserStack = buyIn;
            dbService.UpdateUserStack(player.UserID, player.UserStack);

            player.UserStatus = WAITING;
            for (int i = 1; i <= FULL; i++)
            {
                if (freeSpace[i] == 0)
                {
                    freeSpace[i] = 1;
                    player.UserTablePlace = i;
                    break;
                }
            }
            mutex.WaitOne();
            users.Add(window);
            foreach (MenuWindow windowUser in users)
            {
                windowUser.ShowPlayer(tableType, player);
                window.ShowPlayer(tableType, windowUser.Player());
            }
            mutex.ReleaseMutex();

            return 1;
        }

        public bool IsFull()
        {
            return users.Count == FULL;
        }

        public bool IsEmpty()
        {
            return users.Count == EMPTY;
        }

        public int Occupied()
        {
            return users.Count;
        }
    }
}
