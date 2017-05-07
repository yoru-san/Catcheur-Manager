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
        public static Random percent { get; set; }

        public static List<Action<Wrestler, Wrestler>> AttackList = new List<Action<Wrestler, Wrestler>>();

        public static void BuildAttackList()
        {
            AttackList.Add(OPF); //0
            AttackList.Add(JS); //1
            AttackList.Add(TH); //2
            AttackList.Add(DP); //3
            AttackList.Add(JN); //4
            AttackList.Add(M);  //5
            AttackList.Add(JC); //6
            AttackList.Add(JR); //7
            AttackList.Add(RM); //8
            AttackList.Add(CH); //9
            AttackList.Add(BB); //10


        }

        public static int GetProbability()
        {
            Random pct = new Random();
            return pct.Next(1, 101); 
        }


        public static string OPF_desc = "30% de chance d'annuler une attaque";

        //Marche avec l'Action<> ou une simple fonction
        public static Action<Wrestler, Wrestler> OPF = delegate (Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;

            }
        };



        public static string JS_desc = "40% de chance de regagner 5 points de vie (ne peut dépasser la vie max), 60% de chance de parer 1 dégât supplémentaire";
        public static void JS(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.lifePoint += 5;
            }
            if (Special_attack.GetProbability() <= 60)
            {
                instance.defensePoint += 1;
            }
        }

        public static string TH_desc = $"20% de chance d’infliger 2 dégâts supplémentaires mais perd alors 1 point de vie";
        public static void TH(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {
                instance.attackPoint += 2;
                instance.lifePoint -= 1;
            }
        }

        public static string DP_desc = "10% de chance de subtiliser 3 pvs de &l’ennemie en plus de l’attaque, 30% de chances de se soigner de 2 pvs et 10% de chances de parer 1 dégat";
        public static void DP(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 10)
            {
                opponent.lifePoint -= 3;
                instance.lifePoint += 3;
                instance.Hit(opponent);
            }
            if (Special_attack.GetProbability() <= 30)
            {
                instance.lifePoint += 2;
            }
            if (Special_attack.GetProbability() <= 10)
            {
                instance.defensePoint += 1;
            }
        }

        public static string JN_desc = "30% de chance d’annuler une attaque";
        public static void JN(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;

            }
        }

        public static string M_desc = "40% de chance de se protéger contre 4 points de dégâts, mais n’en inflige qu’un seul durant le tour";
        public static void M(Wrestler instance, Wrestler target)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.defensePoint += 4;
                instance.attackPoint = 1;
            }
        }

        public static string JC_desc = "20% de chance d’infliger 2 dégâts supplémentaires mais perd alors 1 point de vie";   
        public static void JC(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {
                instance.attackPoint += 2;
                instance.lifePoint -= 1;
            }
        }

        public static string JR_desc = "30% de chance d’annuler une attaque";
        public static void JR(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        }

        public static string RM_desc = "40% de chance de s’infliger 3 dégâts, sinon inflige 1 dégât supplémentaire et se protège de 2 dégâts infligés";
        public static void RM(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.lifePoint -= 3;
            }
            else
            {
                instance.attackPoint += 1;
                instance.defensePoint += 2;
            }
        }

        public static string CH_desc = "30% de chance d’annuler une attaque";
        public static void CH(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        }

        public static string BB_desc = "8% de chance de mettre instantanément K.O l’adversaire";
        public static void BB(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 8)
            {
                opponent.lifePoint = 0; 
            }
        }

        

    }
}
