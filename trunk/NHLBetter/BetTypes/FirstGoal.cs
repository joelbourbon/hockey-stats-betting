﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{


    public class FirstGoal : Bet
    {
        public string specialPlayer;
        public string teamAbb;

        public FirstGoal()
        {
            TypeOfBet = Bet.BetType.FirstGoalBet;
        }

        ~FirstGoal()
        {
        }

        override public void Initialize()
        {
            var index = iniString.IndexOf("descActivite=\"") + "descActivite=\"".Length;
            while (iniString[index] != '\"')
            {
                specialPlayer += iniString[index++];
            }

            IniGetOdd();
            IniGetPid();

            if(iniString.Contains("Tout autre joueur"))
            {
                specialPlayer = "anyOtherPlayer";
            }
            else
            {
                index = iniString.IndexOf("-") + 1;
                while (iniString[index] != '\"')
                {
                    teamAbb += iniString[index++];
                }
                index = specialPlayer.IndexOf("-");
                specialPlayer = specialPlayer.Substring(0, index);
            }
        }
    }
}