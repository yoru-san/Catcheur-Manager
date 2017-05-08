using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Xml.Serialization;

namespace Catcheur_Manager
{
    [XmlInclude(typeof(Round))]
    public class Round
    {
        public enum action {Attack = 0, Block = 1, Special = 3};
        public int id { get; set; }
        public string Action { get; set; }
        public Wrestler Beginner { get; set; }
        public action FirstAction { get; set; }

       
        public Wrestler Second { get; set; }
        public action SecondAction { get; set; }


        public Round(int Id)
        {
            id = Id;

        }

        public Round(int Id, Match match)
        {
            id = Id;
            match.Rounds.Add(this);

        }

        public Round()
        {
            //XML only
        }

        public void PlayRound()
        {
            if (FirstAction == action.Attack)
            {
                if (SecondAction == action.Attack)
                {
                    Beginner.Hit(Second);
                    Console.WriteLine($"La vie de {Second} passe à {Second.lifePoint}");
                    Second.Hit(Beginner);
                    Console.WriteLine($"La vie de {Beginner} passe à {Beginner.lifePoint}");
                }
                else if (SecondAction == action.Block)
                {
                    Console.WriteLine($"{Second.Name} a bloqué l'attaque de {Beginner.Name}!");
                    Second.Block(Beginner);
                    Console.WriteLine($"La vie de {Second} passe à {Second.lifePoint}");
                }
                else
                {
                    Beginner.Hit(Second);
                    Second.SpecialAttack(Beginner);
                }
            }
            else if(FirstAction == action.Block)
            {
                if (SecondAction == action.Attack)
                {
                    Console.WriteLine($"{Beginner.Name} a bloqué l'attaque de {Second.Name}!");
                    Beginner.Block(Second);
                }
                else if(SecondAction == action.Block)
                {
                    Console.WriteLine("Les deux adversaires se défendent, cela n'a aucun effet...");
                }
                else
                {
                    Second.SpecialAttack(Beginner);
                }
            }
            else
            {
                if (SecondAction == action.Attack)
                {
                    Beginner.SpecialAttack(Second);
                    Second.Hit(Beginner);
                    Console.WriteLine($"La vie de {Beginner} passe à {Beginner.lifePoint}");
                }
                else if (SecondAction == action.Block)
                {
                    Beginner.SpecialAttack(Second);
                }
                else
                {
                    Beginner.SpecialAttack(Second);
                    Second.SpecialAttack(Beginner);
                }
            }
        }

        public string ToShortString()
        {
            return $"Round {id}";
        }

        public override string ToString()
        {
            string res =
                $"Round {id}";
            return base.ToString();
        }
    }
}
