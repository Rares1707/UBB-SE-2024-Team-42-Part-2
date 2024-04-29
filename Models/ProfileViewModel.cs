using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperbetBeclean.Model;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Models
{
    internal class ProfileViewModel
    {
        private DBService dbService;

        private MenuWindow mainWindow;
        public List<ShopItem> OwnedItems { get; set; }

        public ProfileViewModel(MenuWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            OwnedItems = new List<ShopItem>();
            dbService = new DBService();
            LoadItems();
        }

        private void LoadItems()
        {
            List<ShopItem> ownedItems = dbService.GetAllUserIconsByUserId(mainWindow.userId());
            foreach (var item in ownedItems)
            {
                item.UserId = mainWindow.userId();
                OwnedItems.Add(item);
            }
        }
    }
}
