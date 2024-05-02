using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class Icon
    {
        private int iconID;
        private string iconName;
        private int iconPrice;
        private string iconPath;



        public Icon(int iconID = 0, string iconName = "", int iconPrice = 0, string iconPath = "")
        {
            this.iconID = iconID;
            this.iconName = iconName;
            this.iconPrice = iconPrice;
            this.iconPath = iconPath;
        }

        public int IconID
        {
            get { return iconID; } set { iconID = value;  }
        }
        public string IconName
        {
            get { return iconName; } set { iconName = value; }
        }
        public int IconPrice
        {
            get { return iconPrice; } set { iconPrice = value; }
        }
        public string IconPath
        {
            get { return iconPath; } set { iconPath = value; }
        }
    }
}
