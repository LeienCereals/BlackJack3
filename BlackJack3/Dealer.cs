using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack3
{
    class Dealer
    {
        public static List<Card> HiddenCards { get; set; } = new List<Card>();
        public static List<Card> RevealedCards { get; set; } = new List<Card>();

        /// <summary>
        /// Take the top card from HiddenCards, remove it, and add it to RevealedCards. Pakt de eerste kaart van de HiddenCards, er wordt weggehaalt van de de hiddencards en komt erbij bij de revealedcards
        /// </summary> 
        public static void RevealCard()
        {
            RevealedCards.Add(HiddenCards[0]);
            HiddenCards.RemoveAt(0);
        }

        /// <returns>
        /// Hier worden alle waardes van de revealed cards bij elkaar opgeteld
        /// </returns>
        public static int GetHandValue()
        {
            int value = 0;
            foreach (Card card in RevealedCards)
            {
                value += card.Value;
            }
            return value;
        }

        /// <summary>
        /// Laat de revealed cards van de dealer in de Console zien.
        /// </summary>
        public static void WriteHand()
        {
            Console.WriteLine("Dealer's Hand (" + GetHandValue() + "):");
            foreach (Card card in RevealedCards)
            {
                card.WriteDescription();
            }
            for (int i = 0; i < HiddenCards.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("<verstopt>");
                Table.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
