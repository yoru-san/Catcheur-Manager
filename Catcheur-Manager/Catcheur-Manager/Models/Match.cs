using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Timers;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Match))]
    public class Match
    {
        public static int idNum { get; set; } = 1;

        public int id { get; set; }

        protected int Iteration {get; set; }
        protected int IterationMax { get; set; }
        public Wrestler FirstWrestler { get; set; }
        public Wrestler SecondWrestler { get; set; }
        public Wrestler Winner { get; set; }
        public Wrestler Loser { get; set; }
        public bool WayOfWinning { get; set; }
        public int Profit { get; set; }
        private Random Rnd;
        private Timer timer;

        public bool isReady { get; set; }

        public bool isEnd { get; set; }

        [XmlIgnore]
        public Season MatchSeason { get; set; }

        public Match()
        {
            //XML only
        }

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
            IterationMax = 20;

            MatchSeason = currentSeason;
            currentSeason.CurrentMatch = this;
            currentSeason.MatchHistory.Add(this);

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
                isEnd = true;
                isReady = false;
            }

        }
    }
}