using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager
{
    class Menu
    {
        static void MenuStart()
        {
            bool end = false;
            Console.Write("Bienvenue dans Catch Manager!\n\nEntrez votre nom pour commencer à jouer:\n");

            while (!end)
            {
                string name = Console.ReadLine();

                Console.WriteLine($"Vos avec choisi le nom: {name}, êtes vous sûr?\0. -> Oui\t1. -> Non");

            }
        }

        static void MenuIntTryParse(int min, int max)
        {
            int res = -1;
            if (!int.TryParse(Console.ReadLine(), out res))
            {

            }
        }


    }
    class Choix
    {

    }
}
