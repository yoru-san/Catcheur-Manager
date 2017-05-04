using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catcheur_Manager.Models;

namespace Catcheur_Manager
{

    static class Menu
    {
        public static void MenuStart()
        {
            Console.Write("Bienvenue dans Catcheur Manager!\n");

            if (Player.PlayerList.Count == 0)
            {
                MenuNewPlayer();
            }
            else
            {
                bool end = false;
                int choix = -1;

                while (!end)
                {
                    Console.WriteLine("Sélectionnez un joueur\n\n0 -> Nouveau joueur\n");
                    for (int i = 0; i < Player.PlayerList.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {Player.PlayerList[i].Name}");
                    }

                    choix = MenuIntParse(0, Player.PlayerList.Count);

                    if (choix == 0)
                    {
                        MenuNewPlayer();
                    }
                    else
                    {
                        if (MenuPlayer(Player.PlayerList[choix - 1]))
                        {
                            end = true;
                        }
                    }
                }

            }
            

        }

        public static void MenuNewPlayer()
        {
            bool end = false;
            
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

        public static void MenuDeletePlayer(Player player)
        {
            Console.WriteLine($"Suppression de personnage\n\n/!\\ Voulez vous vraiment supprimer le personnage {player.Name}? /!\\\n0 -> Oui\t1 -> Non");
            if (!MenuTORChoice())
            {
                Console.WriteLine("C'est votre dernier mot?\n0 -> Oui\t1 -> Non");
                if (!MenuTORChoice())
                {
                    player.Delete();
                }
                
            }
        }

        public static bool MenuPlayer(Player player)
        {
            bool end = false;
            int choix = -1;
            bool res = false;

            while (!end)
            {
                if (player.getAvailableWrestler().Count <= 2)
                {
                    MenuGameOver(player);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($" --- {player.Name} --- \n\nBénéfices: {player.Money}\tSaison {player.CurrentSeason.id} - Match {player.getCurrentMatch().getMatchNum()}\n\n0. -> Créer le match de samedi prochain\n" +
                        $"1. -> Consulter l'historique des matchs\n2. -> Consulter la base des contacts\n3. -> Changer de personnage\n4. -> Quitter le jeu\n5. -> Supprimer le personnage");

                    choix = MenuIntParse(0, 5);

                    switch (choix)
                    {
                        case 0:
                            MenuMatch(player);
                            break;
                        case 1:
                            MenuHistory(player);
                            break;
                        case 2:
                            MenuList(player);
                            break;
                        case 3:
                            Console.Clear();
                            end = true;
                            res = false;
                            break;
                        case 4:
                            Console.Clear();
                            end = true;
                            res = true;
                            break;
                        case 5:
                            MenuDeletePlayer(player);
                            end = true;
                            res = false;
                            break;
                    }
                }
                
                
            }
            return res;



        }

        public static void MenuList(Player player)
        {
            bool end = false;
            int choix = -1;

            while (!end)
            {
                Console.Clear();
                Console.WriteLine("Liste des contacts: \n\n0 -> Quitter");
                player.printContactList(player.ContactList);

                choix = MenuIntParse(0, player.ContactList.Count);

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

        public static void MenuMatch(Player player)
        {
            Console.Clear();

            if (player.getCurrentMatch().isEnd)
            {
                bool end = false;
                int choix = -1;
                Wrestler wres1;
                Wrestler wres2;



                while (!end)
                {

                    Console.Clear();
                    Console.WriteLine("Création du match de samedi soir: \nSélectionnez deux catcheurs parmis la liste:\nPremier catcheur:\n\n0 -> Quitter");
                    player.printContactList(player.getAvailableWrestler());

                    choix = MenuIntParse(0, player.getAvailableWrestler().Count());

                    if (choix != 0)
                    {
                        wres1 = player.SelectWrestler(choix - 1);
                        Console.Clear();
                        choix = 0;

                        Console.WriteLine($"Création du match de samedi soir: \nSélectionnez deux catcheurs parmis la liste:\nPremier catcheur: {wres1.Name}\nSecond catcheur:\n\n0 -> Quitter");
                        player.printContactList(player.getAvailableWrestler());
                        choix = MenuIntParse(0, player.getAvailableWrestler().Count());

                        if (choix != 0)
                        {
                            wres2 = player.SelectWrestler(choix - 1);

                            Console.WriteLine($"Catcheurs sélectionnés: {wres1.Name} et {wres2.Name}\nConfirmer?\t0 -> Oui\t1 -> Non");
                            choix = MenuIntParse(0, 1);

                            if (choix == 0)
                            {
                                new Match(wres1, wres2, player.CurrentSeason);
                                end = true;
                            }
                            else
                            {
                                wres1.UnselectWrestler();
                                wres2.UnselectWrestler();
                            }
                        }
                        else
                        {
                            wres1.UnselectWrestler();
                            end = true;
                        }
                    }
                    else
                    {

                        end = true;
                    }
                }
            }

            if (!player.getCurrentMatch().isEnd)
            {
                MenuMatchLaunch(player.getCurrentMatch());

                if (player.getCurrentMatch().id == 8)
                {
                    player.EndSeason();
                }
            }



        }

        public static void MenuMatchLaunch(Match match)
        {
            Console.Clear();
            Console.WriteLine($"Voulez vous lancer le match opposant {match.FirstWrestler.Name} à {match.SecondWrestler.Name} maintenant?\n\n{match.FirstWrestler.ToString()}\n\n{match.SecondWrestler.ToString()}\n\n0 -> Oui\t1 -> Non");
            if (!MenuTORChoice())
            {
                match.FirstWrestler.UnselectWrestler();
                match.SecondWrestler.UnselectWrestler();

                match.Start();

                while (!match.isEnd)
                {

                }

                    
            }



        }

        public static void MenuHistory(Player player)
        {
            Console.Clear();
            Console.WriteLine("Historique des matchs:\n\n");
            foreach (Season season in player.SeasonHistory)
            {
                Console.WriteLine($"Saison {season.id}: ");
                foreach (Match match in season.MatchHistory)
                {
                    Console.WriteLine($"\t{match.ToShortString()}");
                }
            }
            Console.ReadLine();
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

        static bool MenuTORChoice()
        {
            if (MenuIntParse(0, 1) == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
            
        }

        static void MenuGameOver(Player player)
        {
         Console.WriteLine($"GAME OVER \nVous avez perdu car vous avez moins de 2 catcheurs dans votre base de contact ! Votre score : {player.Money} euros");
        }

    }



    class Choix
    {

    }
}
