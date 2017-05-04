using System;
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
        
        static void Main(string[] args)
        {
            if (!File.Exists("players.xml"))
            {
                File.Create("players.xml");
            }
            if (new FileInfo("players.xml").Length > 0)
            {
                Player.DeserializePlayers();
            }


            Menu.MenuStart();

        }
    }
}
