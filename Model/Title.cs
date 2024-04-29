using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class Title
    {
        private int titleID;
        private string titleName;
        private int titlePrice;
        private string titleContent;

        public Title(int titleID = 0, string titleName = "", int titlePrice = 0, string titleContent = "")
        {
            this.titleID = titleID;
            this.titleName = titleName;
            this.titlePrice = titlePrice;
            this.titleContent = titleContent;
        }

        public int TitleID
        {
            get { return titleID; } set { titleID = value; }
        }
        public string TitleName
        {
            get { return titleName; } set { titleName = value; }
        }
        public int TitlePrice
        {
            get { return titlePrice; } set { titlePrice = value; }
        }
        public string TitleContent
        {
            get { return titleContent; } set { titleContent = value; }
        }
    }
}
