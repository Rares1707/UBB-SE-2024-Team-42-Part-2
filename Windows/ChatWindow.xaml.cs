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
using SuperbetBeclean.Services;

namespace SuperbetBeclean.Windows
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private IChatService chatService;
        public ChatWindow(IChatService chatService)
        {
            InitializeComponent();
            this.chatService = chatService;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Assuming the height of the upper part is 60 (adjust as needed)
                if (e.GetPosition(this).Y < 60)
                {
                    DragMove();
                }
            }
            catch
            {
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            chatService.NewMessage(chatInputTextBox.Text + "\n", this);
        }

        private void MessagingBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
    }
}
