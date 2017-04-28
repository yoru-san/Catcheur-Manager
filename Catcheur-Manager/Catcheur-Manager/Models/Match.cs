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
        protected int Iteration {get; set; }
        protected int IterationMax { get; set; }
        protected Wrestler FirstWrestler { get; set; }
        protected Wrestler SecondWrestler { get; set; }
        public Wrestler Winner { get; set; }
        public Wrestler Loser { get; set; }
        public bool WayOfWinning { get; set; }
        public int Profit { get; set; }
        private Random Rnd;
        private Timer timer;

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

        public Match(Wrestler wres1, Wrestler wres2)
        {
            Iteration = 0;
            IterationMax = 19;
            FirstWrestler = wres1;
            SecondWrestler = wres2;
            Rnd = new Random();
            timer = new Timer();
        }

        //A REVOIR AVEC LE PROF !
        public void Start()
        {
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 2000;
            timer.Enabled = true;
            Console.WriteLine("coucou");
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (Iteration <= IterationMax)
            {
                int rand = Rnd.Next(0, 2);

                switch (rand)
                {
                    case 0:
                        Console.WriteLine($"C'est {FirstWrestler.Name} qui commence !");
                        FirstWrestler.ChooseAction(SecondWrestler);
                        break;
                    case 1:
                        Console.WriteLine($"C'est {SecondWrestler.Name} qui engage le combat !");
                        SecondWrestler.ChooseAction(FirstWrestler);
                        break;
                }

                Iteration++;
            }

            else
            {
                Console.WriteLine($"Le combat est fini en {Iteration} round ! Bravo !");
                timer.Enabled = false;
            }

        }
    }
}
