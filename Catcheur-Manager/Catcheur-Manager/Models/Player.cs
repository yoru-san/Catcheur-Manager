using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Player))]
    public class Player
    {
        public int SeasonId { get; set; }
        public int Money { get; set; }

        public string Name { get; set; }

        public List<Wrestler> ContactList { get; set; }

        public Season CurrentSeason { get; set; }

        public List<Season> SeasonHistory;

        
        public static List<Player> PlayerList { get; set; } = new List<Player>();

        public Player()
        {
            //XML only
        }

        public Player(string name)
        {
            Name = name;
            Money = 0;
            SeasonId = 1;
            SeasonHistory = new List<Season>();
            SeasonHistory.Add(new Season(this));
            CurrentSeason = SeasonHistory.Last();
            ContactList = new List<Wrestler>();

            generateBaseContacts();
            PlayerList.Add(this);

            SerializePlayers();

            Menu.MenuPlayer(this);
            
            
        }

        public void generateBaseContacts()
        {
            new Wrestler_Brute("Judy Sunny", Wrestler._status.Disponible, this);
            new Wrestler_Agile("Triple Hache", Wrestler._status.Disponible, this);
            new Wrestler_Agile("Dead Poule", Wrestler._status.Disponible, this);
            new Wrestler_Brute("L'ordonnateur des pompes funèbres", Wrestler._status.Disponible, this);
            new Wrestler_Brute("Jarvan cinquième du nom", Wrestler._status.En_Convalescence, this);
            new Wrestler_Agile("Madusa", Wrestler._status.Disponible, this);
            new Wrestler_Agile("John Cinéma", Wrestler._status.En_Convalescence, this);
            new Wrestler_Brute("Jeff Radis", Wrestler._status.En_Convalescence, this);
            new Wrestler_Brute("Raie Mystérieuse", Wrestler._status.Disponible, this);
            new Wrestler_Brute("Chris Hart", Wrestler._status.Disponible, this);
            new Wrestler_Agile("Bret Benoit", Wrestler._status.Disponible, this);

        }

        public void orderContactList()
        {
            ContactList = ContactList.OrderBy(o => o.Status).OrderBy(o => o.Name).ToList();
        }
        public void printContactList(List<Wrestler> ContactList)
        {

            for (int i = 0; i < ContactList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {ContactList[i].GetStringStatus()} - {ContactList[i].GetStringType()} - { ContactList[i].Name}");

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

        public void EndSeason()
        {
            SeasonHistory.Add(new Season(this));
            CurrentSeason = SeasonHistory.Last();
        }

        public override string ToString()
        {
            //Osef
            return base.ToString();
        }

        public void Delete()
        {
            PlayerList.Remove(this);
            SerializePlayers();
        }

        public static void SerializePlayers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
            using (StreamWriter wr = new StreamWriter("players.xml"))
            {
                serializer.Serialize(wr, PlayerList);
            }
        }

        public static void DeserializePlayers()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));
            using (StreamReader rd = new StreamReader("players.xml"))
            {
                PlayerList = serializer.Deserialize(rd) as List<Player>;
            }

        }
    }


}
