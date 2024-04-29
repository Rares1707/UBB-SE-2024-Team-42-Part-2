﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    /// <summary>
    /// Interaction logic for GameTablePage.xaml
    /// </summary>
    public partial class GameTablePage : Page
    {
        private MenuWindow mainWindow;
        private Frame mainFrame;
        private MainService service;
        private int timer = 0;
        private int playerBet = 0;
        private string action = string.Empty;
        private string tableType;
        private ChatWindow chatWindow;
        private ChatService chatService;
        public GameTablePage(Frame mainFrame, MenuWindow mainWindow, MainService service,string tableType)
        {
            InitializeComponent();
            Loaded += GameTablePage_Loaded;
            this.mainWindow = mainWindow;
            this.service = service;
            this.mainFrame = mainFrame;
            PlayerNameTextBox.Text = this.mainWindow.userName();
            Uri uri = new Uri(this.mainWindow.userIcon(), UriKind.Absolute);
            PlayerIconImg.Source = new BitmapImage(uri);
            PlayerLvlTextBox.Text = "Level: " + this.mainWindow.userLevel().ToString();
            PlayerChipsTextBox.Text = "Chips: " + this.mainWindow.userChips().ToString();
            this.tableType = tableType;
             chatService = new ChatService();
        }
        public void UpdateChips(User player)
        {
            Application.Current.Dispatcher.Invoke(() => {
                PlayerChipsTextBox.Text = player.UserChips.ToString();
            });
        }
        private void GameTablePage_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                window.Width = 920;
                window.Height = 720;
            }
        }
        private void QuitBttn_Click(object sender, RoutedEventArgs e)
        {
            chatService.closeChat(mainWindow);
            mainFrame.NavigationService.GoBack();
            action = "Fold";
            mainWindow.disconnectUser(sender, e);
        }

        public void EndTimer()
        {
            timer = -1;
            Application.Current.Dispatcher.Invoke(() => 
            {
                PlayerTimer.Text = string.Empty;
                SliderBet.Maximum = 0;
                MinValueBet.Content = "0";
                SliderBet.Minimum = 0;
                MaxValueBet.Content = "0";
                SliderBet.Value = 0;
                SliderValueBet.Content = "0";
                PlayerTimer.Foreground = Brushes.White;
            });
        }

        public void EndTurn(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerBet.Content = "-";
                CurrentBetValue.Content = "-";
            });
        }
        public void StartRound(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PotValue.Content = "0";
                CurrentBetValue.Content = "-";
                Label playerStack = FindName("Player" + player.UserTablePlace + "Stack") as Label;
                playerStack.Content = "Stack: " + player.UserStack;
            });
        }

        public void ShowCards(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerBet.Content = "-";
                Image playerCard1 = FindName("Player" + player.UserTablePlace + "Card1") as Image;
                Image playerCard2 = FindName("Player" + player.UserTablePlace + "Card2") as Image;
                Uri uri1 = new Uri("/assets/cards/" + player.UserCurrentHand[0].Info() + ".png", UriKind.Relative);
                playerCard1.Source = new BitmapImage(uri1);
                Uri uri2 = new Uri("/assets/cards/" + player.UserCurrentHand[1].Info() + ".png", UriKind.Relative);
                playerCard2.Source = new BitmapImage(uri2);
            });
        }
        public void ResetPot()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PotValue.Content = "0";
            });
        }

        public void ResetTimer(int minBet, int maxBet)
        {
            timer = 20;
            action = string.Empty;
            playerBet = 0;
            SliderBet.Minimum = Math.Max(0, minBet);
            MinValueBet.Content = Math.Max(0, minBet).ToString();
            SliderBet.Value = SliderBet.Minimum;
            SliderValueBet.Content = MinValueBet.Content;
            SliderBet.Maximum = maxBet;
            MaxValueBet.Content = maxBet.ToString();
            PlayerTimer.Text = "Time: " + timer.ToString();
            PlayerTimer.Foreground = Brushes.White;
        }
        public void DecrementTimer()
        {
            timer--;
            if (timer == 5)
            {
                PlayerTimer.Foreground = Brushes.Red;
            }
            PlayerTimer.Text = "Time: " + timer.ToString();
        }

        public void ResetCards()
        {
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 2; j++)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Image playerCard = FindName("Player" + i + "Card" + j) as Image;
                        playerCard.Visibility = Visibility.Hidden;
                    });
                }
            }
            for (int i = 1; i <= 5; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Image playerCard = FindName("CardTable" + i) as Image;
                    playerCard.Visibility = Visibility.Hidden;
                });
            }
        }

        public void AddUserCard(bool visible, int player, int card, string cardValue)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Image playerCard = FindName("Player" + player + "Card" + card) as Image;
                if (visible == false)
                {
                    Uri uri = new Uri("/assets/cards/downCard.jpg", UriKind.Relative);
                    playerCard.Source = new BitmapImage(uri);
                }
                else
                {
                    Uri uri = new Uri("/assets/cards/" + cardValue + ".png", UriKind.Relative);
                    playerCard.Source = new BitmapImage(uri);
                }
                playerCard.Visibility = Visibility.Visible;
            });
        }

        public void PlayerFold(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Image playerCard1 = FindName("Player" + player.UserTablePlace + "Card1") as Image;
                Image playerCard2 = FindName("Player" + player.UserTablePlace + "Card2") as Image;
                playerCard1.Visibility = Visibility.Hidden;
                playerCard2.Visibility = Visibility.Hidden;
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerBet.Content = "-";
            });
        }

        public void ShowPlayer(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Border playerBorder = FindName("Player" + player.UserTablePlace + "Border") as Border;
                TextBlock playerName = FindName("Player" + player.UserTablePlace + "NameTextBox") as TextBlock;
                TextBlock playerLevel = FindName("Player" + player.UserTablePlace + "LvlTextBox") as TextBlock;
                Image playerIcon = FindName("Player" + player.UserTablePlace + "IconImg") as Image;
                playerBorder.Visibility = Visibility.Visible;
                playerName.Text = player.UserName;
                playerLevel.Text = player.UserLevel.ToString();
                Uri uri = new Uri(player.UserCurrentIconPath, UriKind.Absolute);
                playerIcon.Source = new BitmapImage(uri);
                Border playerMoney = FindName("Player" + player.UserTablePlace + "MoneyData") as Border;
                Label playerStack = FindName("Player" + player.UserTablePlace + "Stack") as Label;
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerMoney.Visibility = Visibility.Visible;
                playerStack.Content = "Stack: " + player.UserStack;
                playerBet.Content = "-";
            });
        }

        public void HidePlayer(User player)
        {
            Application.Current.Dispatcher.Invoke(() => {

                Border playerBorder = FindName("Player" + player.UserTablePlace + "Border") as Border;
                TextBlock playerName = FindName("Player" + player.UserTablePlace + "NameTextBox") as TextBlock;
                TextBlock playerLevel = FindName("Player" + player.UserTablePlace + "LvlTextBox") as TextBlock;
                playerBorder.Visibility = Visibility.Hidden;
                playerName.Text = string.Empty;
                playerLevel.Text = string.Empty;
                Border playerMoney = FindName("Player" + player.UserTablePlace + "MoneyData") as Border;
                Label playerStack = FindName("Player" + player.UserTablePlace + "Stack") as Label;
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerMoney.Visibility = Visibility.Hidden;
                playerStack.Content = string.Empty;
                playerBet.Content = string.Empty;
            });
        }
        public void AddTableCard(int card, string cardValue)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Image playerCard = FindName("CardTable" + card) as Image;
                Uri uri = new Uri("/assets/cards/" + cardValue + ".png", UriKind.Relative);
                playerCard.Source = new BitmapImage(uri);
                playerCard.Visibility = Visibility.Visible;
            });
        }

        async public Task<int> RunTimer(int minBet, int maxBet)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ResetTimer(minBet, maxBet);
            });
            while (timer != 0)
            {
                if (action != string.Empty)
                {
                    break;
                }
                await Task.Delay(1000);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    DecrementTimer();
                });
                Console.WriteLine(timer.ToString());
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                EndTimer();
            });
            if (action == string.Empty)
            {
                action = "Fold";
                playerBet = -1;
            }
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (action == "Fold")
                {
                    CurrentBetValue.Content = "Folded";
                }
                else
                {
                    CurrentBetValue.Content = playerBet.ToString();
                }
            });
            return playerBet;
        }

        private void CallBtn_Click(object sender, RoutedEventArgs e)
        {
            action = "Call";
            playerBet = Convert.ToInt32(SliderBet.Minimum);
        }

        private void RaiseBttn_Click(object sender, RoutedEventArgs e)
        {
            action = "Raise";
            playerBet = Convert.ToInt32(SliderBet.Value);
        }
        private void FoldBttn_Click(object sender, RoutedEventArgs e)
        {
            action = "Fold";
            playerBet = -1;
        }
        public void UpdatePot(int pot)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                PotValue.Content = pot.ToString();
            });
        }

        public void UpdatePlayerMoney(User player)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Border playerMoney = FindName("Player" + player.UserTablePlace + "MoneyData") as Border;
                Label playerStack = FindName("Player" + player.UserTablePlace + "Stack") as Label;
                Label playerBet = FindName("Player" + player.UserTablePlace + "Bet") as Label;
                playerMoney.Visibility = Visibility.Visible;
                playerStack.Content = "Stack: " + player.UserStack.ToString();
                playerBet.Content = "Bet: " + player.UserBet.ToString();
            });
        }

        public void DisplayWinner(User player, bool status)
        {
            if (status)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Border playerBorder = FindName("Player" + player.UserTablePlace + "Border") as Border;
                    Grid playerGrid = FindName("Player" + player.UserTablePlace + "Grid") as Grid;
                    TextBlock playerName = FindName("Player" + player.UserTablePlace + "NameTextBox") as TextBlock;
                    TextBlock playerLevel = FindName("Player" + player.UserTablePlace + "LvlTextBox") as TextBlock;
                    playerBorder.Background = Brushes.LightGreen;
                    playerGrid.Background = Brushes.LightGreen;
                    playerName.Foreground = Brushes.WhiteSmoke;
                    playerLevel.Foreground = Brushes.WhiteSmoke;
                });
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Border playerBorder = FindName("Player" + player.UserTablePlace + "Border") as Border;
                    Grid playerGrid = FindName("Player" + player.UserTablePlace + "Grid") as Grid;
                    TextBlock playerName = FindName("Player" + player.UserTablePlace + "NameTextBox") as TextBlock;
                    TextBlock playerLevel = FindName("Player" + player.UserTablePlace + "LvlTextBox") as TextBlock;
                    playerBorder.Background = Brushes.WhiteSmoke;
                    playerGrid.Background = Brushes.WhiteSmoke;
                    playerName.Foreground = Brushes.Black;
                    playerLevel.Foreground = Brushes.Black;
                });
            }
        }
        private void ChallengesBttn_Click(object sender, RoutedEventArgs e)
        {
            ChallengesWindow challengesWindow = new ChallengesWindow();
            challengesWindow.Show();
        }

        private void MsgBttn_Click(object sender, RoutedEventArgs e)
        {
            chatService.newChat(mainWindow, tableType);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SliderValueBet.Content = Convert.ToInt32(SliderBet.Value).ToString();
        }
    }
}
