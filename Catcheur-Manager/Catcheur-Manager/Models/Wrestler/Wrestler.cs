using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    abstract class Wrestler
    {
        

        //public static List<Wrestler> AvailableWrestler { get; set; } = new List<Wrestler>();

        public enum _status { Disponible = 0, En_Convalescence = 1, Hors_d_etat = 2};
        public string Name { get; set; }
        public int lifePoint { get; set; }
        public _status Status { get; set; }
        public int attackPoint { get; set; }
        public int defensePoint { get; set; }

        public string specialDesc { get; set; }

        public bool isSelected { get; set; }

        public Wrestler(string name, _status status, Player player)
        {
            Name = name;
            Status = status;
        }

        public void hit (Wrestler wrestler1, Wrestler wrestler2)
        {
            wrestler2.lifePoint -= wrestler1.attackPoint;

        }

        public void block(Wrestler wrestler1, Wrestler wrestler2)
        {
            wrestler2.attackPoint -= wrestler1.defensePoint;
        }

        public void specialAttack()
        {

        }











        public void UnselectWrestler()
        {
            isSelected = false;
        }

        
    }
}
