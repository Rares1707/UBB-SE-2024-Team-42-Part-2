using System.Collections.Generic;
using SuperbetBeclean.Services;
using SuperbetBeclean.Windows;

namespace SuperbetBeclean.Models
{
    internal class ProfileViewModel
    {
        private IDataBaseService dbService;

        private MenuWindow mainWindow;
        public List<ShopItem> OwnedItems { get; set; }

        public ProfileViewModel(MenuWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            OwnedItems = new List<ShopItem>();
            dbService = new DataBaseService();
            LoadItems();
        }

        private void LoadItems()
        {
            List<ShopItem> ownedItems = dbService.GetAllUserIconsByUserId(mainWindow.UserId());
            foreach (var item in ownedItems)
            {
                item.UserId = mainWindow.UserId();
                OwnedItems.Add(item);
            }
        }
    }
}
