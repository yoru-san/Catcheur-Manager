﻿using Catcheur_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager
{
    class Menu
    {
        public static void MenuStart()
        {
            bool end = false;
            Console.Write("Bienvenue dans Catch Manager!\n");
            int choix = -1;

            string name = "";

            while (!end)
            {
                Console.WriteLine("Entrez votre nom pour commencer à jouer:\n");
                name = Console.ReadLine();

                Console.WriteLine($"Vous avec choisi le nom: {name}\n\nEtes vous sûr?\n0. -> Oui\t1. -> Non");
                choix = MenuIntParse(0, 1);
                if (choix == 0)
                {
                    end = true;
                }

            }

            Player player = new Player(name);

        }

        static int MenuIntParse(int min, int max)
        {
            bool ok = false;
            int res = -1;
            while (!ok)
            {
                if (!int.TryParse(Console.ReadLine(), out res))
                {
                    Console.WriteLine("Erreur: entrée incorrecte: vous devez entrer un entier.");
                }
                else
                {
                    if(res < min || res > max)
                    {
                        Console.WriteLine($"Erreur: le nombre doit être compris entre {min} et {max}");
                    }
                    else
                    {
                        ok = true;
                    } 
                }
            }
            return res;

        }


    }
    class Choix
    {

    }
}
