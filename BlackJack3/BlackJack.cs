using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack3
{
    class BlackJack
    {
        private static Deck deck = new Deck();
        private static Player player = new Player();

        // enum van de resultaten van de ronden
        private enum RoundResult
        {
            PUSH,
            PLAYER_WIN,
            PLAYER_BUST,
            PLAYER_BLACKJACK,
            DEALER_WIN,
            SURRENDER,
            INVALID_BET
        }

        /// <summary>
        /// Initialize Deck, geeft speler en dealer de kaarten, en laat ze zien.
        /// </summary>
        static void InitializeHands()
        {
            deck.Initialize();

            player.Hand = deck.DealHand();
            Dealer.HiddenCards = deck.DealHand();
            Dealer.RevealedCards = new List<Card>();

            //Als de hand 2 Aasen heeft, maak een Hard=1.
            if (player.Hand[0].Face == Face.Aas && player.Hand[1].Face == Face.Aas)
            {
                player.Hand[1].Value = 1;
            }

            if (Dealer.HiddenCards[0].Face == Face.Aas && Dealer.HiddenCards[1].Face == Face.Aas)
            {
                Dealer.HiddenCards[1].Value = 1;
            }

            Dealer.RevealCard();

            player.WriteHand();
            Dealer.WriteHand();
        }

        /// <summary>
        /// Hier wordt het resultaat van de ronde bestemd.
        /// </summary>
        public static void StartRound()
        {
            Console.Clear();

            if (!TakeBet())
            {
                EndRound(RoundResult.INVALID_BET);
                return;
            }
            Console.Clear();

            InitializeHands();
            TakeActions();

            Dealer.RevealCard();

            Console.Clear();
            player.WriteHand();
            Dealer.WriteHand();

            player.HandsCompleted++;

            if (player.Hand.Count == 0)
            {
                EndRound(RoundResult.SURRENDER);
                return;
            }
            else if (player.GetHandValue() > 21)
            {
                EndRound(RoundResult.PLAYER_BUST);
                return;
            }

            while (Dealer.GetHandValue() <= 16)
            {

                Dealer.RevealedCards.Add(deck.DrawCard());

                Console.Clear();
                player.WriteHand();
                Dealer.WriteHand();
            }


            if (player.GetHandValue() > Dealer.GetHandValue())
            {
                player.Wins++;
                if (Table.IsHandBlackjack(player.Hand))
                {
                    EndRound(RoundResult.PLAYER_BLACKJACK);
                }
                else
                {
                    EndRound(RoundResult.PLAYER_WIN);
                }
            }
            else if (Dealer.GetHandValue() > 21)
            {
                player.Wins++;
                EndRound(RoundResult.PLAYER_WIN);
            }
            else if (Dealer.GetHandValue() > player.GetHandValue())
            {
                EndRound(RoundResult.DEALER_WIN);
            }
            else
            {
                EndRound(RoundResult.PUSH);
            }

        }

        /// <summary>
        /// Speler wordt gevraagd om eenkaart erbij te vragen, te doubbelen te wachten of om op te geven.
        /// </summary>
        static void TakeActions()
        {
            string action;
            do
            {
                Console.Clear();
                player.WriteHand();
                Dealer.WriteHand();

                Console.Write("Enter Action (? for help): ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                action = Console.ReadLine();
                Table.ResetColor();

                switch (action.ToUpper())
                {
                    case "HIT":
                        player.Hand.Add(deck.DrawCard());
                        break;
                    case "STAND":
                        break;
                    case "SURRENDER":
                        player.Hand.Clear();
                        break;
                    case "DOUBLE":
                        if (player.Chips <= player.Bet)
                        {
                            player.AddBet(player.Chips);
                        }
                        else
                        {
                            player.AddBet(player.Bet);
                        }
                        player.Hand.Add(deck.DrawCard());
                        break;
                    default:
                        Console.WriteLine("Geldige zetten:");
                        Console.WriteLine("Hit, Stand, Opgeven, Double");
                        Console.WriteLine("Druk een toets om door te gaan.");
                        Console.ReadKey();
                        break;
                }

                if (player.GetHandValue() > 21)
                {
                    foreach (Card card in player.Hand)
                    {
                        if (card.Value == 11) 
                        {
                            card.Value = 1;
                            break;
                        }
                    }
                }
            } while (!action.ToUpper().Equals("STAND") && !action.ToUpper().Equals("DOUBLE")
                && !action.ToUpper().Equals("") && player.GetHandValue() <= 21);
        }

        /// <summary>
        /// Vraagt de speler voor zijn inleg
        /// </summary>
        /// <returns>De inleg was geldig</returns>
        static bool TakeBet()
        {
            Console.Write("Huidige chips: ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(player.Chips);
            Table.ResetColor();

            Console.Write("Minimum Inleg: ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(Table.MinimumBet);
            Table.ResetColor();

            Console.Write("Geef een inleg om het spel te beginnen " + player.HandsCompleted + ": ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            string s = Console.ReadLine();
            Table.ResetColor();

            if (Int32.TryParse(s, out int bet) && bet >= Table.MinimumBet && player.Chips >= bet)
            {
                player.AddBet(bet);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Perform action based on result of round and start next round. De chips worden verdeeld van het eind resultaat en als er niet meer genoeg chips zijn worden de chips opnieuw verdeelt.
        /// </summary>
        /// <param name="result">Het resultaat van de ronde</param>
        static void EndRound(RoundResult result)
        {
            switch (result)
            {
                case RoundResult.PUSH:
                    player.ReturnBet();
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Speler and Dealer gelijkspel.");
                    break;
                case RoundResult.PLAYER_WIN:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Speler wint " + player.WinBet(false) + " chips");
                    break;
                case RoundResult.PLAYER_BUST:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Speler is af ");
                    break;
                case RoundResult.PLAYER_BLACKJACK:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Speler Wins " + player.WinBet(true) + " chips with Blackjack.");
                    break;
                case RoundResult.DEALER_WIN:
                    player.ClearBet();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Dealer Wint.");
                    break;
                case RoundResult.SURRENDER:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Speler geeft op " + (player.Bet / 2) + " chips");
                    player.Chips += player.Bet / 2;
                    player.ClearBet();
                    break;
                case RoundResult.INVALID_BET:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect Bet.");
                    break;
            }

            if (player.Chips <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine();
                Console.WriteLine("Jouw Chips zijn op geweest naar " + (player.HandsCompleted - 1) + " ronden.");
                Console.WriteLine("500 Chips zuller er bijgevoegd worden en de leaderboard heeft zich gereset.");

                player = new Player();
            }

            Table.ResetColor();
            Console.WriteLine("Druk op een toets om verder te gaan");
            Console.ReadKey();
            StartRound();
        }
    }
}
