﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;

namespace Catcheur_Manager.Models
{
    class Match
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
            IterationMax = 20;

            MatchSeason = currentSeason;
            currentSeason.CurrentMatch = this;
            currentSeason.MatchHistory.Add(this);

            FirstWrestler = wres1;
            SecondWrestler = wres2;

        }

        public void CreateMatch()
        {

        }

        public void Start()
        {
            while (Iteration <= IterationMax)
            {
                Iteration++;
            }
            isEnd = true;
            isReady = false;
        }
    }
}
