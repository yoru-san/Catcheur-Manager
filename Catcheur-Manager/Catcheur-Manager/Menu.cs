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
            Console.WriteLine("Appuyez sur ENTRER pour continuer...");
            Console.ReadLine();



            bool end = false;
            int choix = -1;

            while (!end)
            {
                Console.Clear();

                if (Player.PlayerList.Count != 0)
                {
                    Console.WriteLine("Sélectionnez un joueur\n\n0. -> Nouveau joueur\n1. -> Highscores\n");
                    for (int i = 0; i < Player.PlayerList.Count; i++)
                    {
                        Console.WriteLine($"{i + 2}. {Player.PlayerList[i].Name}");
                    }

                    choix = MenuIntParse(0, Player.PlayerList.Count+1);

                    switch (choix)
                    {
                        case 0:
                            MenuNewPlayer();
                            if (MenuPlayer(Player.PlayerList.Last()))
                            {
                                end = true;
                            }
                            break;
                        case 1:
                            MenuHighscores();
                            break;
                        default:
                            if (MenuPlayer(Player.PlayerList[choix - 2]))
                            {
                                end = true;
                            }
                            break;

                    }
                }
                else
                {
                    MenuNewPlayer();
                    if (MenuPlayer(Player.PlayerList.Last()))
                    {
                        end = true;
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
            new Player(name);
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
                if (player.getWrestlerList(Wrestler._status.Disponible).Count <= 2)
                {
                    MenuGameOver(player);
                    res = false;
                    end = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine($" --- {player.Name} --- \n\nBénéfices: {player.Money}\tSaison {player.getCurrentSeason().id} - Match {player.getCurrentMatch().getMatchNum()}\n\n0. -> Créer le match de samedi prochain\n" +
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
                            Console.WriteLine("Voulez vous vraiment quitter le jeu?\n0. -> Oui\t1. -> Non");
                            if (!MenuTORChoice())
                            {
                                //Player.SerializePlayers();
                                end = true;
                                res = true;
                            }
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
            string search = string.Empty;
            List<Wrestler> list = player.ContactList;



            while (!end)
            {
                Console.Clear();
                Console.Write("Liste des contacts: \n(Entrez un nom pour le rechercher -- ENTRER pour reset)\n\n");
                if (search != "")
                {
                    Console.WriteLine($"Recherche: \"{search}\"");
                }
                Console.WriteLine("0. -> Quitter\n");
                player.printContactList(list);



                search = Console.ReadLine();

                if (int.TryParse(search, out choix))
                {
                    if (choix < 0 || choix > list.Count)
                    {
                        Console.WriteLine($"Erreur: le nombre doit être compris entre {0} et {list.Count}");
                    }
                    else
                    {
                        if (choix == 0)
                        {
                            end = true;
                        }
                        else
                        {
                            search = "";
                            Console.Clear();
                            Console.WriteLine($"{list[choix - 1].ToString()}\n\nAppuyez sur ENTRER pour continuer...");
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    list = Search(player.ContactList, search);
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
                    Console.WriteLine("Création du match de samedi soir: \nSélectionnez deux catcheurs parmis la liste:\nPremier catcheur:\n\n0. -> Quitter\n");
                    player.printContactList(player.getWrestlerList(Wrestler._status.Disponible));

                    choix = MenuIntParse(0, player.getWrestlerList(Wrestler._status.Disponible).Count());

                    if (choix != 0)
                    {
                        wres1 = player.SelectWrestler(choix - 1);
                        Console.Clear();
                        choix = 0;

                        Console.WriteLine($"Création du match de samedi soir: \nSélectionnez deux catcheurs parmis la liste:\nPremier catcheur: {wres1.Name}\nSecond catcheur:\n\n0 -> Quitter");
                        player.printContactList(player.getWrestlerList(Wrestler._status.Disponible));
                        choix = MenuIntParse(0, player.getWrestlerList(Wrestler._status.Disponible).Count());

                        if (choix != 0)
                        {
                            wres2 = player.SelectWrestler(choix - 1);

                            Console.WriteLine($"Catcheurs sélectionnés: {wres1.Name} et {wres2.Name}\nConfirmer?\t0 -> Oui\t1 -> Non");
                            choix = MenuIntParse(0, 1);

                            if (choix == 0)
                            {
                                new Match(wres1, wres2, player.getCurrentSeason());
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

                if (player.getCurrentMatch().isEnd)
                {
                    player.UpdateStats();

                    if (player.getCurrentMatch().id == 8)
                    {
                        player.EndSeason();
                    }
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
                Console.Clear();
                match.Start();
                Console.WriteLine("\nAppuyez sur ENTRER pour continuer...");
                Console.ReadLine();


            }



        }

        public static void MenuHistory(Player player)
        {
            bool end = false;
            int choix = -1;

            while (!end)
            {
                Console.Clear();
                Console.WriteLine("Historique des matchs:\n\n");
                Console.WriteLine("\n0. -> Quitter\n");
                foreach (Season season in player.SeasonHistory)
                {
                    Console.WriteLine($"{season.id}. Saison {season.id}: {season.MatchHistory.Count}");
                    foreach (Match match in season.MatchHistory)
                    {
                        Console.WriteLine($"\t{match.ToShortString()}");
                    }
                }
                choix = MenuIntParse(0, player.SeasonHistory.Count);

                if (choix != 0)
                {
                    MenuHistorySeason(player.SeasonHistory[choix - 1]);
                }
                else
                {
                    end = true;
                }
            }

        }

        public static void MenuHistorySeason(Season season)
        {
            int choix = -1;
            bool end = false;
            while (!end)
            {
                Console.Clear();
                
                Console.WriteLine(season.ToString());
                Console.WriteLine("\n0. -> Quitter\n");

                foreach (Match match in season.MatchHistory)
                {
                    Console.WriteLine($"{match.id}. {match.ToShortString()}");
                }

                choix = MenuIntParse(0, season.MatchHistory.Count);

                if (choix != 0)
                {
                    MenuHistoryMatch(season.MatchHistory[choix - 1]);
                }
                else
                {
                    end = true;
                }
            }

        }

        public static void MenuHistoryMatch(Match match)
        {
            Console.Clear();
            Console.WriteLine(match.ToString());
            Console.WriteLine("\n0. -> Quitter\n");

            foreach(Round round in match.Rounds)
            {
                Console.WriteLine($"{round.id}. {round.ToShortString()}");
            }
            Console.WriteLine("\nAppuyez sur une touche pour continuer...");
            Console.ReadKey();
        }

        public static void MenuHighscores()
        {
            if (Highscore.Scores.Count != 0)
            {

                bool end = false;
                int choix = -1;
                string search = string.Empty;
                List<Highscore> list = Highscore.Scores;



                while (!end)
                {
                    Console.Clear();
                    Console.Write("Liste des highscores: \n(Entrez un nom pour le rechercher -- ENTRER pour reset)\n\n");
                    if (search != "")
                    {
                        Console.WriteLine($"Recherche: \"{search}\"");
                    }
                    Console.WriteLine("0. -> Quitter\n");
                    Highscore.PrintHighscores();



                    search = Console.ReadLine();

                    if (int.TryParse(search, out choix))
                    {
                        if (choix < 0 || choix > list.Count)
                        {
                            Console.WriteLine($"Erreur: le nombre doit être compris entre {0} et {list.Count}");
                        }
                        else
                        {
                            if (choix == 0)
                            {
                                end = true;
                            }
                            else
                            {
                                search = "";
                                Console.Clear();
                                Console.WriteLine($"{list[choix - 1].ToString()}\n\nAppuyez sur ENTRER pour continuer...");
                                Console.ReadLine();
                            }
                        }
                    }
                    else
                    {
                        list = Search(Highscore.Scores, search);
                    }

                }
            }
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
                    if (res < min || res > max)
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
            Console.Clear();
            Console.WriteLine($"GAME OVER \nVous avez perdu car vous avez moins de 2 catcheurs dans votre base de contacts ! \nVotre score : {player.Money} euros\n\nVotre personnage va être supprimé, mes ses statistiques seront conservées dans les highscores\n\nAppuyez sur ENTRER pour continuer...");
            Highscore.Scores.Add(new Highscore(player));
            Console.ReadLine();
            player.Delete();
        }

        static List<T> Search<T>(List<T> list, string search) where T: ISearchable
        {
            return list.Where(w => w.Name.ToLower().Contains(search.ToLower())).ToList();
        }

    }
}
