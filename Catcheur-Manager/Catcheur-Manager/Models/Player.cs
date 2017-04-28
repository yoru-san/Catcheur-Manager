using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Player
    {
        public int Money { get; set; }

        public string Name { get; set; }

        public List<Wrestler> ContactList { get; protected set; }

        public Season CurrentSeason { get; set; }

        public List<Season> SeasonHistory;

        public Player(string name)
        {
            Name = name;
            Money = 0;
            SeasonHistory = new List<Season>();
            SeasonHistory.Add(new Season());
            CurrentSeason = SeasonHistory[0];
            ContactList = new List<Wrestler>();

            generateBaseContacts();


            Menu.MenuPlayer(this);
            
            
        }

        public void generateBaseContacts()
        {
            new Wrestler_Brute("Judy Sunny", Wrestler._status.Disponible, this);
            new Wrestler_Agile("Triple Hache", Wrestler._status.Disponible, this);
            new Wrestler_Agile("Dead Poule", Wrestler._status.Disponible, this);

        }

        public void orderContactList()
        {
            ContactList = ContactList.OrderBy(o => o.Status).OrderBy(o => o.Name).ToList();
        }
        public void printContactList(List<Wrestler> ContactList)
        {
            string res = "";
            for (int i = 0; i < ContactList.Count; i++)
            {
                res = $"{i + 1}. ";

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

        public List<Wrestler> getAvailableWrestler()
        {
            return ContactList.Where(w => w.Status == Wrestler._status.Disponible && !w.isSelected).ToList().OrderBy(w => w.Name).ToList();
        }

        public Wrestler SelectWrestler(int index)
        {
            Wrestler selectedWrestler = getAvailableWrestler()[index];
            selectedWrestler.isSelected = true;
            return selectedWrestler;

        }

        public Match getCurrentMatch()
        {
            return CurrentSeason.CurrentMatch;
        }

    }
}
