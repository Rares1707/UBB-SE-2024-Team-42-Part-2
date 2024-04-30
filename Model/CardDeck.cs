using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class CardDeck
    {
        private List<PlayingCard> deck;

        public CardDeck()
        {
            deck = new List<PlayingCard>();

            for (int cardValue = 2; cardValue <= 10; cardValue++)
            {
                AddAllCardsOfThisValue(cardValue.ToString());
            }

            AddAllCardsOfThisValue("J");
            AddAllCardsOfThisValue("Q");
            AddAllCardsOfThisValue("K");
            AddAllCardsOfThisValue("A");
        }

        public void AddAllCardsOfThisValue(string value)
        {
            deck.Add(new PlayingCard(value, PlayingCard.HEART_SYMBOL));
            deck.Add(new PlayingCard(value, PlayingCard.DIAMOND_SYMBOL));
            deck.Add(new PlayingCard(value, PlayingCard.SPADE_SYMBOL));
            deck.Add(new PlayingCard(value, PlayingCard.CLUB_SYMBOL));
        }

        public void RemoveCardFromIndex(int index)
        {
            deck.RemoveAt(index);
        }
        public PlayingCard GetCardFromIndex(int index)
        {
            return deck[index];
        }

        public int GetDeckSize()
        {
            return deck.Count;
        }
    }
}
