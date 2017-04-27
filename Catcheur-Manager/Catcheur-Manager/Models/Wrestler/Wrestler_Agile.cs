using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Wrestler_Agile : Wrestler
    {

        public Wrestler_Agile(string name, _status status): base(name, status)
        {
            lifePoint = 125;
            attackPoint = 3;
            defensePoint = 3;

            ContactList.Add(this);
            orderContactList();
        }

    }
}
