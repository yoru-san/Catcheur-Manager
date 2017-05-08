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
        public enum action {Attack = 0, Block = 1, Special = 3, Null = 4};
        public int id { get; set; }
        public string Action { get; set; }
        public Wrestler Beginner { get; set; }
        public action FirstAction { get; set; }

       
        public Wrestler Second { get; set; }
        public action SecondAction { get; set; }

        public bool CancelAttack { get; set; }


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

        public void PermuteOrder() //Inutilisé
        {
            Wrestler perm = Beginner;
            Beginner = Second;
            Second = Beginner;
        }

        public void SetActionFromOrder(Wrestler wres, action action)
        {
            if (wres == Beginner)
            {
                FirstAction = action;
            }
            else if(wres == Second){

                SecondAction = action;
            }
            else
            {
                Console.WriteLine("Erreur retour action");
            }
        }

        public void PlayRound()
        {
            if (FirstAction == action.Special) // Les attaques spéciales changent le déroulement du round -> Test en premier
            {
                Console.WriteLine($"{Beginner.Name} lance son attaque spéciale");
                Beginner.SpecialAttack(Second, this);

            }

            if (SecondAction == action.Special)
            {
                Console.WriteLine($"{Second.Name} lance son attaque spéciale");
                Second.SpecialAttack(Beginner, this);
            }


            if (FirstAction == action.Attack)
            {
                Console.WriteLine($"{Beginner.Name} attaque {Second.Name}");
                if (SecondAction == action.Attack)
                {
                    Console.WriteLine($"{Second.Name} attaque {Beginner.Name}");
                    Beginner.Hit(Second);
                    Second.Hit(Beginner);
                }
                else if (SecondAction == action.Block)
                {
                    Console.WriteLine($"{Second.Name} a bloqué l'attaque de {Beginner.Name}!");
                    Second.Block(Beginner);
                }
                else if (SecondAction == action.Null)
                {
                    Beginner.Hit(Second);
                }
                else{
                    Console.WriteLine("ERREUR ACTION 1");
                }
            }


            else if(FirstAction == action.Block)
            {
                Console.WriteLine($"{Beginner.Name} se défend");
                if (SecondAction == action.Attack)
                {
                    Console.WriteLine($"{Second.Name} attaque {Beginner.Name}");
                    Console.WriteLine($"{Beginner.Name} a bloqué l'attaque de {Second.Name}!");
                    Beginner.Block(Second);
                }
                else if(SecondAction == action.Block)
                {
                    Console.WriteLine("Les deux adversaires se défendent, cela n'a aucun effet...");
                }
                else if (SecondAction == action.Null)
                {
                    Console.WriteLine($"{Beginner.Name} se défend dans le vide");
                }
            }
            else if (FirstAction == action.Null)
            {
                if (SecondAction == action.Attack)
                {
                    Console.WriteLine($"{Second.Name} attaque {Beginner.Name}");
                    Second.Hit(Beginner);
                }
                else if (SecondAction == action.Block)
                {
                    Console.WriteLine($"{Second.Name} se défend dans le vide");
                }
                else if (SecondAction == action.Null)
                {

                }
                else
                {
                    Console.WriteLine("ERREUR ACTION 2");
                }
            }
            else
            {
                Console.WriteLine("ERREUR ACTION 3");
            }

            Beginner.ResetExtra();
            Second.ResetExtra();
        }

        public string Resume()
        {
            string res = "";
            return res;


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
