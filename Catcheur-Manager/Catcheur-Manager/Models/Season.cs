using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Season
    {
        static int idNum = 1;

        public int id { get; set; }
        public int Profit { get; set; }
        public Match CurrentMatch { get; set; }
        public int Rate { get; set; }

        public Season()
        {
            id = idNum;
            idNum++;
        }
    }

}
