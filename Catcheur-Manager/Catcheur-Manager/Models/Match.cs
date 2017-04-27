using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Match
    {
        protected int Iteration {get; set; }
        protected Wrestler FirstWrestler { get; set; }
        protected Wrestler SecondWrestler { get; set; }
        public Wrestler Winner { get; set; }
        public Wrestler Loser { get; set; }
        public  bool  WayOfWinning { get; set; }
        public int Profit { get; set; }

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

        public void CreateMatch()
        {

        }

    }
}
