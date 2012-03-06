using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{
    public class TeamWithMoreHits : Bet
    {
        public TeamWithMoreHits()
        {
            TypeOfBet = Bet.BetType.TeamWithMoreHitsBet;
        }

        ~TeamWithMoreHits()
        {
        }

        public override string ToString()
        {
            return "Not implemented (TeamWithMoreHits)";
        }
    }
}