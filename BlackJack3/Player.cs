using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack3
{
    class Player
    {
        public int Chips { get; set; } = 500;
        public int Bet { get; set; }
        public int Wins { get; set; }
        public int HandsCompleted { get; set; } = 1;

        public List<Card> Hand { get; set; }

        /// <summary>
        /// Voegt spelers chipt bij de inleg
        /// </summary>
        /// <param name="bet"></param>
        public void AddBet(int bet)
        {
            Bet += bet;
            Chips -= bet;
        }

        /// <summary>
        /// Zet Bet op 0
        /// </summary>
        public void ClearBet()
        {
            Bet = 0;
        }

        /// <summary>
        /// Annuleert de spelers inleg en krijgt geen chips erbij of eraf
        /// </summary>
        public void ReturnBet()
        {
            Chips += Bet;
            ClearBet();
        }

        /// <summary>
        /// Geeft speler chips van de gewonnen ronde
        /// </summary>
        /// <param name="blackjack">Als de speler wint met blackjack, speler wint 1.5 keer zijn inleg</param>
        public int WinBet(bool blackjack)
        {
            int chipsWon;
            if (blackjack)
            {
                chipsWon = (int)Math.Floor(Bet * 1.5);
            }
            else
            {
                chipsWon = Bet * 2;
            }

            Chips += chipsWon;
            ClearBet();
            return chipsWon;
        }

        /// <returns>
        /// Speler hand
        /// </returns>
        public int GetHandValue()
        {
            int value = 0;
            foreach (Card card in Hand)
            {
                value += card.Value;
            }
            return value;
        }

        /// <summary>
        /// Laat de hand van de speler in de console zien
        /// </summary>
        public void WriteHand()
        {
            // Schrijft Bet, Chip, Win, Waarde met kleur, en schrijf Ronde #
            Console.Write("Inleg: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(Bet + "  ");
            Table.ResetColor();
            Console.Write("Chips: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(Chips + "  ");
            Table.ResetColor();
            Console.Write("Wint: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Wins);
            Table.ResetColor();
            Console.WriteLine("Ronde #" + HandsCompleted);

            Console.WriteLine();
            Console.WriteLine("Jouw Hand (" + GetHandValue() + "):");
            foreach (Card card in Hand)
            {
                card.WriteDescription();
            }
            Console.WriteLine();
        }
    }
}
