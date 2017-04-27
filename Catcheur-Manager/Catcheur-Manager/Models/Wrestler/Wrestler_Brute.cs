using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Wrestler_Brute : Wrestler
    {

        public Wrestler_Brute(string name, _status status, Player player) : base(name, status, player)
        {
            lifePoint = 100;
            attackPoint = 5;
            defensePoint = 1;

            player.ContactList.Add(this);
            player.orderContactList();
        }
    }
}
