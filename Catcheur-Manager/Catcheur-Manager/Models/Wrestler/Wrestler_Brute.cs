using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    
    public class Wrestler_Brute : Wrestler
    {
        public Wrestler_Brute()
        {
            //XML only
        }

        public Wrestler_Brute(string name, _status status, Player player, int sp, string sp_desc) : base(name, status, player, sp, sp_desc)
        {
            lifePoint = 100;
            attackPoint = 5;
            defensePoint = 1;

            player.ContactList.Add(this);
            player.orderContactList();
        }

        public override string GetStringType()
        {
            return "Brute";
        }

        public override void SetMaxLife()
        {
            lifePoint = 100;
        }

        public override int GetMaxLife()
        {
            return 100;
        }

        public override void ResetStats()
        {
            attackPoint = 5;
            defensePoint = 1;
        }
    }
}
