using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class Font
    {
        private int fontID;
        private string fontName;
        private int fontPrice;
        private string fontType;

        public Font(int fontID = 0,  string fontName = "", int fontPrice = 0, string fontType = "")
        {
            this.fontID = fontID;
            this.fontName = fontName;
            this.fontPrice = fontPrice;
            this.fontType = fontType;
        }

        public int FontID
        {
            get { return fontID; } set { fontID = value; }
        }
        public string FontName
        {
            get { return fontName; } set { fontName = value; }
        }
        public int FontPrice
        {
            get { return fontPrice; }
            set { fontPrice = value; }
        }
        public string FontType
        {
            get { return fontType; }
            set { fontType = value; }
        }
    }
}
