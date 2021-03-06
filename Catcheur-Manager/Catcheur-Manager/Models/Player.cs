﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Catcheur_Manager.Models
{
    [XmlInclude(typeof(Player))]
    [XmlInclude(typeof(List<Season>))]


    public class Player
    {
        public int SeasonId { get; set; }
        public double Money { get; set; }

        public string Name { get; set; }

        public List<Wrestler> ContactList { get; set; }

        //public Season CurrentSeason { get; set; }

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
            //CurrentSeason = SeasonHistory[0];
            ContactList = new List<Wrestler>();

            generateBaseContacts();
            PlayerList.Add(this);

            SerializePlayers();

           
            
            
        }

        public void generateBaseContacts()
        {
            new Wrestler_Brute("Judy Sunny", Wrestler._status.Disponible, this, 1, Special_attack.JS_desc);
            new Wrestler_Agile("Triple Hache", Wrestler._status.Disponible, this, 2, Special_attack.TH_desc);
            new Wrestler_Agile("Dead Poule", Wrestler._status.Disponible, this, 3, Special_attack.DP_desc);
            new Wrestler_Brute("L'ordonnateur des pompes funèbres", Wrestler._status.Disponible, this, 0, Special_attack.OPF_desc);
            new Wrestler_Brute("Jarvan cinquième du nom", Wrestler._status.En_Convalescence, this, 0, Special_attack.OPF_desc).SetConvalescent();
            new Wrestler_Agile("Madusa", Wrestler._status.Disponible, this, 4, Special_attack.M_desc);
            new Wrestler_Agile("John Cinéma", Wrestler._status.En_Convalescence, this, 2, Special_attack.TH_desc).SetConvalescent();
            new Wrestler_Brute("Jeff Radis", Wrestler._status.En_Convalescence, this, 0, Special_attack.OPF_desc).SetConvalescent();
            new Wrestler_Brute("Raie Mystérieuse", Wrestler._status.Disponible, this, 5, Special_attack.RM_desc);
            new Wrestler_Brute("Chris Hart", Wrestler._status.Disponible, this, 0, Special_attack.OPF_desc);
            new Wrestler_Agile("Bret Benoit", Wrestler._status.Disponible, this, 6, Special_attack.BB_desc);

        }

        public void orderContactList()
        {
            ContactList = ContactList.OrderBy(o => o.Name).OrderBy(o => o.Status).ToList();
        }
        public void printContactList(List<Wrestler> ContactList)
        {
            if (ContactList.Count > 9)
            {
                for (int i = 0; i < ContactList.Count; i++)
                {
                    if (i < 9)
                    {
                        Console.WriteLine($" {i + 1}. {ContactList[i].GetStringStatus()} - {ContactList[i].GetStringType()} - {ContactList[i].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {ContactList[i].GetStringStatus()} - {ContactList[i].GetStringType()} - {ContactList[i].Name}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < ContactList.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ContactList[i].GetStringStatus()} - {ContactList[i].GetStringType()} - {ContactList[i].Name}");
                }
            }


        }



        public List<Wrestler> getWrestlerList(Wrestler._status status)
        {
            return ContactList.Where(w => w.Status == status && !w.isSelected).ToList().OrderBy(w => w.Name).ToList();
        }



        public Wrestler SelectWrestler(int index)
        {
            Wrestler selectedWrestler = getWrestlerList(Wrestler._status.Disponible)[index];
            selectedWrestler.isSelected = true;
            return selectedWrestler;

        }

        public Match getCurrentMatch()
        {
            return getCurrentSeason().CurrentMatch;
        }

        public Season getCurrentSeason()
        {
            return SeasonHistory.Last();
        }

        public void EndSeason()
        {
            SeasonHistory.Add(new Season(this));
            //CurrentSeason = SeasonHistory.Last();
        }

        public override string ToString()
        {
            //Osef
            return base.ToString();
        }

        public void Delete()
        {
            PlayerList.Remove(this);
            //SerializePlayers();
        }

        public void UpdateStats()
        {
            Money += getCurrentSeason().GetLastMatchProfit();
            getCurrentSeason().Profit += getCurrentSeason().GetLastMatchProfit();

            List<Wrestler> convWres = getWrestlerList(Wrestler._status.En_Convalescence);

            foreach (Wrestler wres in convWres)
            {
                wres.DecreaseConvTime();
            }

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
