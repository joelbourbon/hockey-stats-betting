using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{

    public class MostMinutesOfPenalty : Bet
    {
        public MostMinutesOfPenalty()
        {
            TypeOfBet = Bet.BetType.MostMinutesOfPenaltyBet;
        }

        ~MostMinutesOfPenalty()
        {
        }
    }
}