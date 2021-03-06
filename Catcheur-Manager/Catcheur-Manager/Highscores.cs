﻿using Catcheur_Manager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Catcheur_Manager
{
    [XmlInclude(typeof(Highscore))]
    public class Highscore : ISearchable
    {
        public static List<Highscore> Scores { get; set; } = new List<Highscore>();

        public double Value { get; set; }

        public string Name { get; set; }

        public int Season { get; set; }

        public int Match { get; set; }

        public Player player { get; set; } //sauvegarde du joueur au cas où...

        public Highscore(Player player)
        {
            Value = player.Money;
            Name = player.Name;
            Season = player.getCurrentSeason().id;
            Match = player.getCurrentMatch().id;
        }

        public static void PrintHighscores()
        {
            if (Scores.Count > 9)
            {
                for (int i = 0; i < Scores.Count; i++)
                {
                    if (i < 9)
                    {
                        Console.WriteLine($" {i + 1}. {Scores[i].Value} - {Scores[i].Name}");
                    }
                    else
                    {
                        Console.WriteLine($"{i + 1}. {Scores[i].Value} - {Scores[i].Name}");
                    }
                }
            }
            else
            {
                for (int i = 0; i < Scores.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Scores[i].Value} - {Scores[i].Name}");
                }
            }
        }

        public static void DeserializeHighscores()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Player>));

            using (StreamReader rd = new StreamReader("highscores"))
            {
                Scores = serializer.Deserialize(rd) as List<Highscore>;
            }

        }
    }
}
