using System.Collections.Generic;

namespace NHLBetter
{
    public class TeamWithMoreHits : Bet
    {
        public TeamWithMoreHits()
        {
            TypeOfBet = Bet.BetType.TeamWithMoreHitsBet;
            multiplicator = 2;
        }

        ~TeamWithMoreHits()
        {
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}