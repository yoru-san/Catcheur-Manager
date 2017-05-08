using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{

    public static class Special_attack
    {
        public static Random percent { get; set; } = new Random(); //Marche mieux que de re-déclarer

        public static List<Action<Wrestler, Wrestler, Round>> AttackList = new List<Action<Wrestler, Wrestler, Round>>();

        public static void BuildAttackList()
        {
            AttackList.Add(OPF); //0
            AttackList.Add(JS); //1
            AttackList.Add(TH); //2
            AttackList.Add(DP); //3
            AttackList.Add(M);  //4
            AttackList.Add(RM); //5
            AttackList.Add(BB); //6


        }

        public static int GetProbability()
        {
            //Random pct = new Random();
            return percent.Next(1, 101);
        }


        public static string OPF_desc = "30% de chance d'annuler une attaque";

        //Marche avec l'Action<> ou une simple fonction
        public static Action<Wrestler, Wrestler, Round> OPF = delegate (Wrestler instance, Wrestler opponent, Round round)
        {
            int rnd = Special_attack.GetProbability();
            Console.WriteLine(rnd);
            if (rnd <= 30) //A CHANGER
            {
                Console.WriteLine($"{instance.Name} annule l'attaque de son adversaire!");
                round.SetActionFromOrder(opponent, Round.action.Null);
                instance.Shield = true;

            }
            else
            {
                Console.WriteLine($"{instance.Name} rate son attaque spéciale");

            }

            round.SetActionFromOrder(instance, Round.action.Null);

        };


        public static string JS_desc = "40% de chance de regagner 5 points de vie (ne peut dépasser la vie max), 60% de chance de parer 1 dégât supplémentaire";
        public static void JS(Wrestler instance, Wrestler opponent, Round round)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.Heal(5);

                Console.WriteLine($"{instance.Name} regagne 5 points de vie");
            }
            if (Special_attack.GetProbability() <= 60)
            {
                instance.defensePoint += 1;
                Console.WriteLine($"La défense de {instance.Name} augmente d'un point");
            }

            round.SetActionFromOrder(instance, Round.action.Null);
        }

        public static string TH_desc = $"20% de chance d’infliger 2 dégâts supplémentaires mais perd alors 1 point de vie";
        public static void TH(Wrestler instance, Wrestler opponent, Round round)
        {
            if (Special_attack.GetProbability() <= 20)
            {
                instance.attackPoint += 2;
                instance.Damage(1);
                Console.WriteLine($"L'attaque de {instance.Name} augmente de deux points mais il perd 1 point de vie");
            }
            else
            {
                Console.WriteLine($"{instance.Name} rate son attaque spéciale");
            }

            round.SetActionFromOrder(instance, Round.action.Null);
        }

        public static string DP_desc = "10% de chance de subtiliser 3 pvs de l’ennemi en plus de l’attaque, 30% de chances de se soigner de 2 pvs et 10% de chances de parer 1 dégat";
        public static void DP(Wrestler instance, Wrestler opponent, Round round)
        {
            round.SetActionFromOrder(instance, Round.action.Null);
            if (Special_attack.GetProbability() <= 30)
            {
                instance.Heal(2);
                Console.WriteLine($"{instance.Name} se soigne de deux points de vie");
                round.SetActionFromOrder(instance, Round.action.Null);
            }
            if (Special_attack.GetProbability() <= 10)
            {
                instance.extraBlock = -(instance.defensePoint - 1);
                Console.WriteLine($"{instance.Name} rate son attaque spéciale mais effectue une blocage de dernière seconde");
                round.SetActionFromOrder(instance, Round.action.Null);
            }
            if (Special_attack.GetProbability() <= 10)
            {
                opponent.Damage(3);
                instance.Heal(3);
                round.SetActionFromOrder(instance, Round.action.Attack);
                Console.WriteLine($"{instance.Name} vole 3 points de vie à {opponent.Name} et l'attaque");
            }

        }


        public static string M_desc = "40% de chance de se protéger contre 4 points de dégâts, mais n’en inflige qu’un seul durant le tour";
        public static void M(Wrestler instance, Wrestler opponent, Round round)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.extraBlock = 4;
                instance.extraAttack = -(instance.attackPoint - 1);
                round.SetActionFromOrder(instance, Round.action.Attack);
                Console.WriteLine($"{instance.Name} se protège de 4 points de dégats et en inflige 1");
            }
            else
            {
                Console.WriteLine($"{instance.Name} rate son attaque spéciale");
                round.SetActionFromOrder(instance, Round.action.Null);
            }
        }

        public static string RM_desc = "40% de chance de s’infliger 3 dégâts, sinon inflige 1 dégât supplémentaire et se protège de 2 dégâts infligés";
        public static void RM(Wrestler instance, Wrestler opponent, Round round)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.Damage(3);
                Console.WriteLine($"{instance.Name} s'inglige 3 points de dégats");
            }
            else
            {
                instance.attackPoint += 1;
                instance.extraBlock += 2;
                Console.WriteLine($"{instance.Name} augmente ses dégats de 1 et bloque 2 dégats");
                
            }
            round.SetActionFromOrder(instance, Round.action.Null);
        }


        public static string BB_desc = "8% de chance de mettre instantanément K.O l’adversaire";
        public static void BB(Wrestler instance, Wrestler opponent, Round round)
        {
            if (Special_attack.GetProbability() <= 8)
            {
                opponent.Damage(opponent.lifePoint);
                Console.WriteLine($"{instance.Name} OS son adversaire, c'est cheaté...");
            }
            else
            {
                Console.WriteLine($"{instance.Name} rate son attaque spéciale");
            }
            round.SetActionFromOrder(instance, Round.action.Null);
        }

        

    }
}
