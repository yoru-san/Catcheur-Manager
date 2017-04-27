using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Season
    {
        public int Profit { get; set; }
        public Match CurrentMatch { get; set; }
        public int Rate { get; set; }

        public Season(int profit, Match currentMatch, int rate)
        {
            Profit = profit;
            CurrentMatch = currentMatch;
            Rate = rate;
        }
    }

}
