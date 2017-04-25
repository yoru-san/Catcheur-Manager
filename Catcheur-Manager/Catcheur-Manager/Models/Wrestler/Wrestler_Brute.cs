using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models.Wrestler
{
    class Wrestler_Brute : Wrestler
    {

        public Wrestler_Brute(string name, _status status) : base(name, status)
        {
            lifePoint = 100;
            attackPoint = 5;
            defensePoint = 1;

            ContactList.Add(this);
            orderContactList();
        }
    }
}
