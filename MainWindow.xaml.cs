﻿using System.Windows;
using SuperbetBeclean.Model;
using SuperbetBeclean.Pages;
using SuperbetBeclean.Services;

namespace SuperbetBeclean
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IMainService service;
        public MainWindow()
        {
            InitializeComponent();
            service = new MainService();
            MainFrame.Navigate(new LoginPage(MainFrame, this));
            Title = "Superbet Beclean - Poker";
        }
        public void OpenNewWindow(string username)
        {
            service.AddWindow(username);
        }
    }
}
