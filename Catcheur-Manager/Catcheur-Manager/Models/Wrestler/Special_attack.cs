﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catcheur_Manager.Models
{
    class Special_attack
    {
        public Random percent { get; set; }

        public static int GetProbability()
        {
            Random pct = new Random();
            return pct.Next(1, 101);      
        }

        public static Action<Wrestler> OPF = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
                
            }
        };

        public static Action<Wrestler> JS = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 40)
            {
               
            }
            if (Special_attack.GetProbability() <= 60)
            {

            }
        };

        public static Action<Wrestler> TH = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {

            }
        };

        public static Action<Wrestler> DP = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 10)
            {

            }
            if (Special_attack.GetProbability() <= 30)
            {

            }
            if (Special_attack.GetProbability() <= 10)
            {

            }
        };

        public static Action<Wrestler> JN = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;

            }
        };

        public static Action<Wrestler> M = delegate (Wrestler target)
        {
            if (Special_attack.GetProbability() <= 40)
            {
                
            }
        };

        public static Action<Wrestler> JC = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 20)
            {

            }
        };

        public static Action<Wrestler> JR = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        };

        public static Action<Wrestler> RM = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 40)
            {

            }
            else
            {

            }
        };

        public static Action<Wrestler> CH = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 30)
            {
                opponent.attackPoint = 0;
            }
        };

        public static Action<Wrestler> BB = delegate (Wrestler opponent)
        {
            if (Special_attack.GetProbability() <= 8)
            {
                opponent.lifePoint = 0; 
            }
        };


    }
}
