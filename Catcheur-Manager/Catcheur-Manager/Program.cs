﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using System.Xml.Serialization;
using System.IO;

namespace Catcheur_Manager
{

    
    class Program
    {
        public static void Loader()
        {
            FileCheck("players.xml");
            FileCheck("highscores.xml");

            if (new FileInfo("players.xml").Length > 0)
            {
                Player.DeserializePlayers();
            }
            if (new FileInfo("highscores.xml").Length > 0)
            {
                Highscore.DeserializeHighscores();
            }

            Special_attack.BuildAttackList();

        }
        public static void FileCheck(string file) //Entraine des fois des exceptions à la création du fichier
        {
            if (!File.Exists(file))
            {
                File.Create(file);
            }

        }

        //Action<T>
        static void Main(string[] args)
        {
            Console.Title = "Catcheur-Manager";
            //Console.SetWindowSize(100, 80);
            Loader();
            Menu.MenuStart();

        }
    }
}
