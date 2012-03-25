using System.Collections.Generic;

namespace NHLBetter
{

    public class MostMinutesOfPenalty : Bet
    {
        public MostMinutesOfPenalty()
        {
            TypeOfBet = Bet.BetType.MostMinutesOfPenaltyBet;
            multiplicator = 3;
        }

        ~MostMinutesOfPenalty()
        {
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}