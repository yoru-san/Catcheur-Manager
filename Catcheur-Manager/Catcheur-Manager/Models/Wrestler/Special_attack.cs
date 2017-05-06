using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Special_attack))]
    public class Special_attack
    {
        public Special_attack()
        {
            //XML only
        }
        public static Random percent { get; set; }

        public static int GetProbability()
        {
            Random pct = new Random();
            return pct.Next(1, 101);      
        }

        public static void OPF(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;

            }
        }

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

        public static void TH(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {
                instance.attackPoint += 2;
                instance.lifePoint -= 1;
            }
        }

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

        public static void JN(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;

            }
        }

        public static void M(Wrestler instance, Wrestler target)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                instance.defensePoint += 4;
                instance.attackPoint = 1;
            }
        }

        public static void JC(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {
                instance.attackPoint += 2;
                instance.lifePoint -= 1;
            }
        }

        public static void JR(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        }

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

        public static void CH(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        }

        public static void BB(Wrestler instance, Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 8)
            {
                opponent.lifePoint = 0; 
            }
        }


    }
}
