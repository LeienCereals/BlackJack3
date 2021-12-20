using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BlackJack3.Suit;
using static BlackJack3.Face;

namespace BlackJack3
{//enum voor de waarden van de kaarten
    public enum Face
    {
        Aas = 1,
        Twee = 2,
        Drie = 3,
        Vier = 4,
        Vijf = 5,
        Zes = 6,
        Zeven = 7,
        Acht = 8,
        Negen = 9,
        Tien = 10,
        Boer = 11,
        Vrouw = 12,
        Koning = 13
    }
    //enum voor de symbolen van de kaarten
    public enum Suit
    {
        Harten = 1,
        Ruiten = 2,
        Schoppen = 3,
        Klaveren = 4
    }
    class Card
    {
        public Suit Suit { get; }
        public Face Face { get; }
        public int Value { get; set; }
        public char Symbol { get; }

        /// <summary>
        /// Initilize waarde en symbool
        /// </summary> 
        public Card(Suit suit, Face face)
        {
            Suit = suit;
            Face = face;

            switch (Suit)
            {
                case Suit.Klaveren:
                    Symbol = '♣';
                    break;
                case Suit.Schoppen:
                    Symbol = '♠';
                    break;
                case Suit.Ruiten:
                    Symbol = '♦';
                    break;
                case Suit.Harten:
                    Symbol = '♥';
                    break;
            }
            switch (Face)
            {
                case Face.Tien:
                case Face.Boer:
                case Face.Vrouw:
                case Face.Koning:
                    Value = 10;
                    break;
                case Face.Aas:
                    Value = 11;
                    break;
                default:
                    Value = (int)Face + 1;
                    break;
            }
        }

        /// <summary>
        /// Print de kaarten uit en bij de aas vermeld of het soft of hard is. Soft= 1 or 11 Hard = 1
        /// </summary>
        public void WriteDescription()
        {
            if (Suit == Suit.Ruiten || Suit == Suit.Harten)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
            }

            if (Face == Face.Aas)
            {
                if (Value == 11)
                {
                    Console.WriteLine(Symbol + " Soft " + Suit + " " + Face);
                }
                else
                {
                    Console.WriteLine(Symbol + " Hard " + Suit + " " + Face);
                }
            }
            else
            {
                Console.WriteLine(Symbol + " " + Suit + " " + Face);
            }
            Table.ResetColor();
        }

    }
}
