using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Player
    {
        public int Money { get; set; }

        public string Name { get; set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}
