using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Season))]
    public class Season
    {
        
        public int id { get; set; }

        public int MatchId { get; set; }
        public int Profit { get; set; }

        public Match CurrentMatch { get; set; }

        public List<Match> MatchHistory { get; set; }


        public int Rate { get; set; }
        public int NumberMatch { get; set; }
        public Season()
        {
            //XML only
        }

        public Season(Player player)
        {
            id = player.SeasonId;
            player.SeasonId ++;
            MatchId = 1;

            MatchHistory = new List<Match>();
            CurrentMatch = new Match(this);
            Profit = 0;
        }


    }

}
