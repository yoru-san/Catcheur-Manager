using Catcheur_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;
using Catcheur_Manager.Models.Wrestler;

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

            Console.Clear();
            Player player = new Player(name);

        }

        public static void MenuPlayer(Player player)
        {
            bool end = false;
            int choix = -1;

            while (!end)
            {
                Console.WriteLine($"{player.Name}\n\nBénéfices: {player.Money}\n\n0. -> Créer le match de samedi prochain\n1. -> Consulter l'historique des matchs\n2. -> Consulter la base des contacts\n3. -> Quitter le jeu\n");

                choix = MenuIntParse(0, 3);

                switch (choix)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    case 2:
                        MenuList();
                        break;
                    case 3:
                        end = true;
                        break;
                }
            }


        }

        public static void MenuList()
        {
            Console.Clear();
            

            

            bool end = false;
            int choix = -1;

            while (!end)
            {
                Console.WriteLine("Liste des contacts: \n\n0 -> Quitter");
                Wrestler.printContactList();

                choix = MenuIntParse(0, Wrestler.ContactList.Count);

                switch (choix)
                {
                    case 0:
                        end = true;
                        break;

                        // Ajout éventuel d'un choix pour afficher des détails sur les catcheurs
                    default:
                        end = true;
                        break;
                }

            }
            Console.Clear();

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
