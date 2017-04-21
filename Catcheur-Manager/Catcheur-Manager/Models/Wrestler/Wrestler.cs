using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models.Wrestler
{
    class Wrestler
    {
        protected string Name { get; set; }
        protected int LifePoint { get; set; }
        protected int Status { get; set; }
        protected int attackPoint { get; set; }
        protected int defensePoint { get; set; }

        public void hit (Wrestler wrestler1, Wrestler wrestler2)
        {
            wrestler2.LifePoint -= wrestler1.attackPoint;

        }

        public void block(Wrestler wrestler1, Wrestler wrestler2)
        {
            wrestler2.attackPoint -= wrestler1.defensePoint;
        }

        public void specialAttack()
        {

        }
    }
}
