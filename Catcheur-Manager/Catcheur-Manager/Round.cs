using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Xml.Serialization;

namespace Catcheur_Manager
{
    [XmlInclude(typeof(Round))]
    public class Round
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public Wrestler BeginnerOfMatch { get; set; }
        public int MatchSeason { get; set; }
        public Round(int id)
        {
            ID = id;
        }

        public Round()
        {
            //XML only
        }

        public string ToShortString()
        {
            return $"Round {ID}";
        }

        public override string ToString()
        {
            string res =
                $"Round {ID}";
            return base.ToString();
        }
    }
}
