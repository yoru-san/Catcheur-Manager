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
            IterationMax = 20;

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

        public void Start()
        {
            ChooseBeginner();
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Interval = 2000;
            timer.Enabled = true;

            while(isEnd)
            {
                Debug.WriteLine("Match toujours en cours...");
            }

            Console.ReadLine();
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

        public override string ToString()
        {
            string res = $"Saison {MatchSeason} - Match n°{id}:\nParticipants: {FirstWrestler}, {SecondWrestler}\n\nVainqueur:\t{Winner} par {WayOfWinning}\nNombre de rounds\t{Iteration}";

            return base.ToString();
        }
    }
}