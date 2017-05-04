using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Timers;
using System.Xml.Serialization;
using System.Diagnostics;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Match))]
    public class Match
    {

        public int id { get; set; }

        public int Iteration { get; set; }
        protected int IterationMax { get; set; }
        public Wrestler FirstWrestler { get; set; }
        public Wrestler SecondWrestler { get; set; }
        public Wrestler WrestlerRound { get; set; }
        public Wrestler Winner { get; set; }
        public Wrestler Loser { get; set; }
        public bool WayOfWinning { get; set; }
        public int Profit { get; set; }

        public int SeasonId { get; set; }

        public int RoundId { get; set; }

        public List<Round> Rounds { get; set; }

        private Timer timer;

        public bool isReady { get; set; }

        public bool isEnd { get; set; }

        
        public int MatchSeason { get; set; }

        public Match()
        {
            //XML only
        }

        public Match(Season currentSeason)
        {
            id = 0;
            isReady = false;
            isEnd = true;

            MatchSeason = currentSeason.id;
            currentSeason.CurrentMatch = this;
        }

        public Match(int iteration, Wrestler firstWrestler, Wrestler secondWrestler, Wrestler winner, Wrestler loser, bool wayOfWinning, int profit)
        {
            Iteration = iteration;
            FirstWrestler = firstWrestler;
            SecondWrestler = secondWrestler;
            Winner = winner;
            Loser = loser;
            WayOfWinning = wayOfWinning;
            Profit = profit;
        }

        public Match(Wrestler wres1, Wrestler wres2, Season currentSeason)
        {
            id = currentSeason.MatchId;
            currentSeason.MatchId++;
            isReady = true;
            isEnd = false;

            Iteration = 0;
            IterationMax = 2;

            RoundId = 1;
            Rounds = new List<Round>();

            MatchSeason = currentSeason.id;
            currentSeason.CurrentMatch = this;
            currentSeason.MatchHistory.Add(this);

            FirstWrestler = wres1;
            SecondWrestler = wres2;
            timer = new Timer();
        }

       public void ChooseBeginner()
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, 2);

            if (rand == 0)
            {
                Console.WriteLine($"C'est {FirstWrestler.Name} qui engage le combat !");
                WrestlerRound = FirstWrestler;
            }
            else
            {
                Console.WriteLine($"C'est {SecondWrestler.Name} qui commence le combat !");
                WrestlerRound = SecondWrestler;
            }
        }

        public void DeterminateWayOfWinning()
        {
            if (Loser.lifePoint == 0)
            {
                Console.WriteLine($"{Winner.Name} gagnant par K.O");
                WayOfWinning = true;
            }

            else if (Iteration == IterationMax)
            {
                Console.WriteLine($"{Winner.Name} remporte le combat par délai avec {Iteration} rounds au compteur !");
                WayOfWinning = false;
            }

        }

        public void EndOfMatch()
        {
            DeterminateWinner();
            DeterminateWayOfWinning();
            CalculateProfit();
        }

        public void DeterminateWinner()
        {
            if (FirstWrestler.lifePoint > SecondWrestler.lifePoint)
            {
                //Console.WriteLine($"C'est le catcheur {FirstWrestler.Name} qui remporte la victoire");
                Winner = FirstWrestler;
                Loser = SecondWrestler;
                SecondWrestler.DeterminateStatus(FirstWrestler);
            }
            else
            {
               //Console.WriteLine($"C'est le catcheur {SecondWrestler.Name} qui gagne");
                Winner = SecondWrestler;
                Loser = FirstWrestler;
                FirstWrestler.DeterminateStatus(SecondWrestler);
            }
        }


        public void Start()
        {
            ChooseBeginner();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 1000;
            timer.Enabled = true;

            while(!isEnd)
            {
                //Debug.WriteLine("Match toujours en cours...");
            }

            Console.ReadLine();
        }

        bool midRound = false;
        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Iteration < IterationMax)
            {
                if (!midRound)
                    Console.WriteLine($"Round #{Iteration+1}");
                    

                if (WrestlerRound == null || WrestlerRound == FirstWrestler)
                {
                    Console.WriteLine($"C'est au tour de {FirstWrestler.Name} !");
                    FirstWrestler.ChooseAction(SecondWrestler);
                    WrestlerRound = SecondWrestler;
                    
                }
                else
                {
                    Console.WriteLine($"C'est au tour de {SecondWrestler.Name} !");
                    SecondWrestler.ChooseAction(FirstWrestler);
                    WrestlerRound = FirstWrestler;
                }

                if (midRound)
                {
                    midRound = false;
                    Iteration++;
                    Profit += 5000;
                }
                else
                    midRound = true;
            }
            else
            {
                EndOfMatch();
                Console.WriteLine($"Le combat est fini en {Iteration} round ! Bravo ! Vous avez gagné {Profit} euros");
                timer.Enabled = false;
                timer.Close();
                isEnd = true;
                isReady = false;
            }

        }

        public void CalculateProfit()
        {
            if (WayOfWinning)
            {
                Profit += 10000;
            }
            else
            {
                Profit += 1000;
            }
        }

        public string ToShortString()
        {
            return $"Match {id} - W {Winner.Name} vs L {Loser.Name} - {Profit}e";
        }

        public override string ToString()
        {
            string res = 
                $"Saison {MatchSeason} - Match {id}:\n\n"
                + $"Combattant 1: {FirstWrestler.Name}\nCombattant 2: {SecondWrestler.Name}\n"
                + $"Gagnant: {Winner.Name} par {GetStringWayOfWinning()}\n"
                + $"Nombre de rounds: {Iteration}/{IterationMax}\n"
                + $"Profit: {Profit}";

            foreach (Round round in Rounds)
            {
                res += round.ToShortString();
            }

            return res;
        }


        public int getMatchNum()
        {
            if (isEnd)
            {
                return id + 1;
            }
            else
            {
                return id;
            }
        }

        public string GetStringWayOfWinning()
        {
            if (WayOfWinning)
            {
                return "K.O";
            }
            else
            {
                return "Délai";
            }
        }
    }
}