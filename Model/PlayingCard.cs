using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class PlayingCard
    {
        private string value;
        private string suit;

        public const string HEART_SYMBOL = "H";
        public const string DIAMOND_SYMBOL = "D";
        public const string SPADE_SYMBOL = "S";
        public const string CLUB_SYMBOL = "C";

        public PlayingCard(string value, string suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public string Value
        {
            get { return value; } set { this.value = value; }
        }
        public string Suit
        {
            get { return suit; } set { suit = value; }
        }
        public string CompleteInformation()
        {
            return value + suit;
        }
    }
}
