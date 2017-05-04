using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    
    public class Wrestler_Agile : Wrestler
    {
        public Wrestler_Agile()
        {
            //XML only
        }

        public Wrestler_Agile(string name, _status status, Player player, Action<Wrestler> sp) : base(name, status, player, sp)
        {
            lifePoint = 1;
            attackPoint = 3;
            defensePoint = 3;

            player.ContactList.Add(this);
            player.orderContactList();
        }

        public override string GetStringType()
        {
            return "Agile";
        }
    }
}
