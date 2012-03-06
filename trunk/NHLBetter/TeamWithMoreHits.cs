namespace NHLBetter
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