using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models.Wrestler
{
    abstract class Wrestler
    {
        public static List<Wrestler> ContactList { get; protected set; } = new List<Wrestler>();

        //public static List<Wrestler> AvailableWrestler { get; set; } = new List<Wrestler>();

        public enum _status { Disponible = 0, En_Convalescence = 1, Hors_d_etat = 2};
        protected string Name { get; set; }
        protected int lifePoint { get; set; }
        protected _status Status { get; set; }
        protected int attackPoint { get; set; }
        protected int defensePoint { get; set; }

        public string specialDesc { get; set; }

        protected bool isSelected { get; set; }

        public Wrestler(string name, _status status)
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

        public static void generateBaseContacts()
        {
            new Wrestler_Brute("Judy Sunny", _status.Disponible);
            new Wrestler_Agile("Triple Hache", _status.Disponible);
            new Wrestler_Agile("Dead Poule", _status.Disponible);

        }

        protected static void orderContactList()
        {
            ContactList = ContactList.OrderBy(o => o.Status).OrderBy(o => o.Name).ToList();
        }

        public static void printContactList(List<Wrestler> ContactList)
        {
            string res = "";
            for (int i = 0; i < ContactList.Count; i++)
            {
                res = $"{i+1}. ";

                switch ((short)ContactList[i].Status)
                {
                    case 0:
                        res += "Disponible";
                        break;
                    case 1:
                        res += "En convalescence";
                        break;
                    case 2:
                        res += "Hors d'état";
                        break;
                }

                if (ContactList[i].GetType() == typeof(Wrestler_Brute))
                {
                    res += " - Brute";
                }
                else
                {
                    res += " - Agile";
                }

                res += $" - {ContactList[i].Name}";

                Console.WriteLine(res);

            }
        }

        public static List<Wrestler> getAvailableWrestler()
        {
            return ContactList.Where(w => w.Status == _status.Disponible && !w.isSelected).ToList().OrderBy(w => w.Name).ToList();
        }

        public static Wrestler SelectWrestler(int index)
        {
            Wrestler selectedWrestler = getAvailableWrestler()[index];
            selectedWrestler.isSelected = true;
            return selectedWrestler;

        }

        public void UnselectWrestler()
        {
            isSelected = false;
        }

        
    }
}
