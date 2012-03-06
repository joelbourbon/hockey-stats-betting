using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{
    public class PlayerDuel : Bet
    {
        public string playerName;

        public PlayerDuel()
        {
            TypeOfBet = Bet.BetType.PlayerDuelBet;
        }

        ~PlayerDuel()
        {
        }

        override public void Initialize()
        {
            isTie = iniString.Contains("Nul");
            return;
        }
    }
}