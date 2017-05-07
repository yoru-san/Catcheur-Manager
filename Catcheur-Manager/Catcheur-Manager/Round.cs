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
        public int id { get; set; }
        public string Action { get; set; }
        public Wrestler BeginnerOfMatch { get; set; }
        public int MatchSeason { get; set; }
        public Round(int Id)
        {
            id = Id;
        }

        public Round()
        {
            //XML only
        }

        public string ToShortString()
        {
            return $"Round {id}";
        }

        public override string ToString()
        {
            string res =
                $"Round {id}";
            return base.ToString();
        }
    }
}
