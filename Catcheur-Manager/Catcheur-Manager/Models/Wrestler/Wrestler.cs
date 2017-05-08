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


    public abstract class Wrestler : ISearchable
    {
        //public delegate void Special(Wrestler instance, Wrestler opponent);

        //public static List<Wrestler> AvailableWrestler { get; set; } = new List<Wrestler>();

        public enum _status { Disponible = 0, En_Convalescence = 1, Hors_d_etat = 2 };
        public string Name { get; set; }
        public int lifePoint { get; set; }
        public _status Status { get; set; }
        public int ConvTime { get; set; }
        public int attackPoint { get; set; }
        public int defensePoint { get; set; }

        public string specialDesc { get; set; }

        /*[XmlIgnore]
        public Action<Wrestler, Wrestler> SpecialAttack { get; set; }*/
        public int SpecialAttackIndex { get; set; }

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

        public Wrestler(string name, _status status, Player player, int sp)
        {
            Name = name;
            Status = status;
            SpecialAttackIndex = sp;
        }

        public Wrestler(string name, _status status, Player player, int sp, string sp_desc)
        {
            Name = name;
            Status = status;
            SpecialAttackIndex = sp;
            specialDesc = sp_desc;
        }

        public void Hit(Wrestler opponent)
        {
            opponent.lifePoint -= attackPoint;
            

        }

        public void Block(Wrestler opponent)
        {
            lifePoint -= (opponent.attackPoint - defensePoint);
            

        }

        public void SpecialAttack(Wrestler opponent)
        {
            Special_attack.AttackList[SpecialAttackIndex](this, opponent);
        }


        public Round.action ChooseAction(Wrestler opponent)
        {
            Random rnd = new Random();
            int rand = rnd.Next(0, 3);

            switch (rand)
            {
                case 0:
                    //Hit(opponent);
                    Console.WriteLine($"{Name} attaque {opponent.Name}!");
                    return Round.action.Attack;

                case 1:
                    //block(opponent);
                    Console.WriteLine($"{Name} se défend!");
                    return Round.action.Block;

                default:
                    //SpecialAttack(/*this,*/ opponent);
                    Console.WriteLine($"{Name} lance son attaque spéciale!" );
                    return Round.action.Special;

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

        public void UnselectWrestler()
        {
            isSelected = false;
        }

        public bool VerifyLifepoint()
        {
            if (lifePoint <= 0)
            {
                Console.WriteLine($"fin du combat !");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetConvalescent()
        {
            Status = _status.En_Convalescence;
            ConvTime = new Random().Next(2, 6);
            Console.WriteLine($"{Name} part à l'hopital pour une durée de {ConvTime} jours.");
        }

        public void DecreaseConvTime()
        {
            ConvTime--;
            if (ConvTime <= 0)
            {
                Status = _status.Disponible;
                SetMaxLife();

                Console.WriteLine($"{Name} s'est remis de ses blessures et est de nouveau disponible!");
            }
        }

        public abstract void SetMaxLife();

        public abstract int GetMaxLife();

        public override string ToString()
        {
            return $"{Name} :"
                //+ {Description}
                + $"\n\nStatut : {GetStringStatus()}\n"
                + $"Points de vie:\t\t{lifePoint}\nPoints d'attaque:\t{attackPoint}\nPoints de défense:\t{defensePoint}\n"
                + $"\nAttaque spéciale: {specialDesc}"
                + "\n\n";
        }


    }
}
