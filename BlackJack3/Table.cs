using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack3
{
    class Table
    {
        public static int MinimumBet { get; } = 10;

        /// <param name="hand"></param>
        /// <returns>Returns true als de hand blackjack is</returns>
        public static bool IsHandBlackjack(List<Card> hand)
        {
            if (hand.Count == 2)
            {
                if (hand[0].Face == Face.Aas && hand[1].Value == 10) return true;
                else if (hand[1].Face == Face.Aas && hand[0].Value == 10) return true;
            }
            return false;
        }

        /// <summary>
        /// Reset Console kleur naar Donker Grijs van Zwart
        /// </summary>
        public static void ResetColor()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
