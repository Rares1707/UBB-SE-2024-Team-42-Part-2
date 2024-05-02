using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using SuperbetBeclean.Models;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private Frame mainFrame;
        private MenuWindow mainWindow;
        public ProfilePage(Frame mainFrame, MenuWindow mainWindow)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
            this.mainWindow = mainWindow;
            User player = mainWindow.Player();
            DataContext = new ProfileViewModel(mainWindow);
            if (!string.IsNullOrEmpty(player.UserCurrentIconPath))
            {
                profilePageUserAvatar.ImageSource = new BitmapImage(new Uri(player.UserCurrentIconPath, UriKind.Absolute));
            }

            profilePageUsernameTextBlock.Text = mainWindow.UserName();
            profilePageChipsTextBlock.Text = mainWindow.UserChips().ToString();
            profilePageDailyStreakTextBlock.Text = mainWindow.UserStreak().ToString();
            profilePageLevelTextBlock.Text = mainWindow.UserLevel().ToString() + ": ";
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEvent)
        {
            mainFrame.NavigationService.GoBack();
        }
    }
}
