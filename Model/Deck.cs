using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperbetBeclean.Model
{
    public class Deck
    {
        private const string HEART = "H";
        private const string DIAMOND = "D";
        private const string SPADE = "S";
        private const string CLUB = "C";

        private List<Card> deck;

        public Deck()
        {
            deck = new List<Card>();

            for (int cardValue = 2; cardValue <= 10; cardValue++)
            {
                AddCardsForValue(cardValue.ToString());
            }

            AddCardsForValue("J");
            AddCardsForValue("Q");
            AddCardsForValue("K");
            AddCardsForValue("A");
        }
        public void AddCardsForValue(string value)
        {
            deck.Add(new Card(value, HEART));
            deck.Add(new Card(value, DIAMOND));
            deck.Add(new Card(value, SPADE));
            deck.Add(new Card(value, CLUB));
        }

        public void RemoveCardFromIndex(int index)
        {
            deck.RemoveAt(index);
        }
        public Card GetCardFromIndex(int index)
        {
            return deck[index];
        }

        public int GetDeckSize()
        {
            return deck.Count;
        }
    }
}
