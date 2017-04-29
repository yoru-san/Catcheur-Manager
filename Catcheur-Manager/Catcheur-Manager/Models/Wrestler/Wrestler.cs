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

        public enum _status { Disponible = 0, En_Convalescence = 1, Hors_d_etat = 2 };
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

        private void hit(Wrestler opponent)
        {
            opponent.lifePoint -= attackPoint;
            Console.WriteLine($"{Name} attaque ! ");

        }

        private void block(Wrestler opponent)
        {
            opponent.attackPoint -= defensePoint;
            Console.WriteLine($"{Name} se défend ! ");

        }

        private void specialAttack()
        {
            Console.WriteLine($"{Name} fait son attaque spécial ! ");

        }

        public void ChooseAction(Wrestler opponent)
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, 3);

            switch (rand)
            {
                case 0:
                    hit(opponent);
                    break;

                case 1:
                    block(opponent);
                    break;

                case 2:
                    specialAttack();
                    break;
            }

        }


        public void UnselectWrestler()
        {
            isSelected = false;
        }


    }
}
