using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack3
{
    class Program
    {
        static void Main(string[] args)
        {
            BlackJack bj = new BlackJack();

            Console.OutputEncoding = Encoding.UTF8;

            Table.ResetColor();
            Console.Title = "♠♥♣♦ Blackjack";

            Console.WriteLine("♠♥♣♦ Welkom bij Blackjack ");
            Console.WriteLine("Druk een toets om verder te gaan.");
            Console.ReadKey();
            BlackJack.StartRound();
        }
    }
}
