﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;

namespace SuperbetBeclean.Models
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private int balance;

        private DBService dbService;

        public int Balance
        {
            get
            {
                return balance;
            }

            set
            {
                if (balance != value)
                {
                    balance = value;
                    OnPropertyChanged(nameof(Balance));
                }
            }
        }

        public List<ShopItem> ShopItems { get; set; }

        public MainViewModel(int currentBalance, int userId)
        {
            Balance = currentBalance;
            ShopItems = new List<ShopItem>();
            dbService = new DBService();
            LoadItems(userId);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadItems(int userId)
        {
            List<ShopItem> ownedItems = dbService.GetAllUserIconsByUserId(userId);
            List<ShopItem> allItems = dbService.GetShopItems();

            // Use a HashSet to store the names of owned items for fast lookup
            HashSet<string> ownedItemNames = new HashSet<string>();
            foreach (var item in ownedItems)
            {
                ownedItemNames.Add(item.Name);
            }

            // Initialize the list for purchasable items
            List<ShopItem> purchasableItems = new List<ShopItem>();

            // Check each item in the shop to see if it's not owned by the user
            foreach (var item in allItems)
            {
                if (!ownedItemNames.Contains(item.Name))
                {
                    item.UserId = userId;
                    purchasableItems.Add(item);
                }
            }

            ShopItems = purchasableItems;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

// TODO: Delete once the commands are implemented
public class PlaceholderCommand : ICommand
{
    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
        return true;
    }

    public void Execute(object parameter)
    {
        return;
    }
}