using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack3
{
    class Deck
    {
        private List<Card> cards;

        /// <summary>
        /// Initilize on creation of Deck.
        /// </summary>
        public Deck()
        {
            Initialize();
        }

        /// <returns>
        /// Returns a BlackJack Deck-- a deck organized by Suit and Face.
        /// </returns>
        public List<Card> GetBJDeck()
        {
            List<Card> bjDeck = new List<Card>();

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    bjDeck.Add(new Card((Suit)j, (Face)i));
                }
            }

            return bjDeck;
        }

        /// <summary>
        ///Verwijderd de eerste 2 kaarten van het deck en voegt het toe aan een list.
        /// </summary>
        /// <returns>List van 2 Kaarten</returns>
        public List<Card> DealHand()
        {
            //Maakt een tijdelijke list van de kaarten and geeft de 2 kaarten aan de speler of dealer
            List<Card> hand = new List<Card>();
            hand.Add(cards[0]);
            hand.Add(cards[1]);

            // Verwijdert de kaarten van de hand
            cards.RemoveRange(0, 2);

            return hand;
        }

        /// <summary>
        /// Haalt de eerste kaart uit het deck. De kaart wordt uit het deck verwijdert.
        /// </summary>
        /// <returns>Eerste kaart van het deck</returns>
        public Card DrawCard()
        {
            Card card = cards[0];
            cards.Remove(card);

            return card;
        }

        /// <summary>
        /// Shud de kaarten van het deck
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();

            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card card = cards[k];
                cards[k] = cards[n];
                cards[n] = card;
            }
        }

        /// <summary>
        /// Vervangt het deck met het BlackJack deck en shud het.
        /// </summary>
        public void Initialize()
        {
            cards = GetBJDeck();
            Shuffle();
        }
    }
}
