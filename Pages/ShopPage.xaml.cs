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
using System.Windows.Shapes;
using SuperbetBeclean.Models;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Pages
{
    /// <summary>
    /// Interaction logic for ShopPage.xaml
    /// </summary>
    public partial class ShopPage : Page
    {
        private Frame mainFrame;
        public ShopPage(Frame mainFrame, MenuWindow menuWindow)
        {
            InitializeComponent();
            DataContext = new MainViewModel(menuWindow.UserChips(), menuWindow.UserId());
            this.mainFrame = mainFrame;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.GoBack();
        }

        public static readonly DependencyProperty BalanceProperty = DependencyProperty.Register(
                       "Balance", typeof(int), typeof(ShopPage), new PropertyMetadata(default(int)));

        public int Balance
        {
            get { return (int)GetValue(BalanceProperty); }
            set { SetValue(BalanceProperty, value); }
        }
    }
}
