using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Wrestler))]
    [XmlInclude(typeof(Wrestler_Agile))]
    [XmlInclude(typeof(Wrestler_Brute))]
    public abstract class Wrestler
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

        public Wrestler()
        {
            //XML only
        }

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
            lifePoint -= (opponent.attackPoint - defensePoint);
            Console.WriteLine($"{Name} se défend ! ");

        }

        private void specialAttack()
        {
            Console.WriteLine($"{Name} fait son attaque spéciale ! ");

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

        public string GetStringStatus()
        {
            switch ((short)Status)
            {
                case 0:
                    return "Disponible";
                case 1:
                    return"En convalescence";
                case 2:
                    return "Hors d'état";
                default:
                    return "0";
            }
        }

        public abstract string GetStringType();

        public void DeterminateStatus(Wrestler opponent)
        {
 
            if (defensePoint < opponent.defensePoint || defensePoint < defensePoint/2)
            {
                Console.WriteLine($"Catcheur {this.Name} à l'hopital");
                this.Status = Wrestler._status.En_Convalescence;
            }

            StateOfCombat();



        }

        public void StateOfCombat()
        {
            if (this.defensePoint < 1)
            {
                Console.WriteLine($"Catcheur {this.Name} mort");
                this.Status = Wrestler._status.Hors_d_etat;
            }
        }

        public void ChangeStatus()
        {
            if (this.Status == Wrestler._status.En_Convalescence)
            {

            }
        }
        public void UnselectWrestler()
        {
            isSelected = false;
        }

        public override string ToString()
        {
            return $"{Name} :"
                //+ {Description}
                + $"\n\nStatut : {GetStringStatus()}\n"
                + $"Points de vie:\t\t{lifePoint}\nPoints d'attaque:\t{attackPoint}\nPoints de défense:\t{defensePoint}\n"
                //+ $"\nAttaque spéciale: {specialDesc}"
                + "\n\n";
        }


    }
}
