using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;

namespace Catcheur_Manager
{
    class Round
    {
        public int ID { get; set; }
        public string Action { get; set; }
        public Wrestler BeginnerOfMatch { get; set; }

    }
}
