using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Timers;

namespace Catcheur_Manager.Models
{
    class Match
    {
        public static int idNum { get; set; } = 1;

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
        private Timer timer;

        public bool isReady { get; set; }

        public bool isEnd { get; set; }

        public Season MatchSeason { get; set; }

        public Match(Season currentSeason)
        {
            id = idNum;
            idNum++;
            isReady = false;
            isEnd = true;

            MatchSeason = currentSeason;
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
            id = idNum;
            idNum++;
            isReady = true;
            isEnd = false;

            Iteration = 0;
            IterationMax = 3;

            MatchSeason = currentSeason;
            currentSeason.CurrentMatch = this;
            currentSeason.MatchHistory.Add(this);

            FirstWrestler = wres1;
            SecondWrestler = wres2;
            timer = new Timer();
        }

       

        public void Start()
        {
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 2000;
            timer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Iteration < IterationMax)
            {
                Console.WriteLine($"Round #{Iteration}");

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

                Iteration++;
            }
            else
            {
                Console.WriteLine($"Le combat est fini en {Iteration} round ! Bravo !");
                timer.Enabled = false;
                timer.Close();
                isEnd = true;
                isReady = false;
            }
        }

    }
}