using System.Windows;
using System.Windows.Controls;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        private Frame mainFrame;
        private MenuWindow menuWindow;
        private User user;
        private MainService service;
        public MainMenu(Frame mainFrame, MenuWindow mainWindow, MainService service, User user)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            menuWindow = mainWindow;
            this.service = service;
            this.user = user;
        }

        private void OnClickRulesButton(object sender, RoutedEventArgs routedEvent)
        {
            RulesWindow rulesWindow = new RulesWindow();
            rulesWindow.Show();
        }

        private void OnClickPlayButton(object sender, RoutedEventArgs routedEvent)
        {
            mainFrame.Navigate(new LobbyPage(mainFrame, menuWindow, service, user));
        }

        private void OnClickQuitButton(object sender, RoutedEventArgs routedEvent)
        {
            menuWindow.Close();
        }
    }
}
