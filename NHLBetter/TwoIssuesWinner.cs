namespace NHLBetter
{

    public class TwoIssuesWinner:Bet
    {
        public TwoIssuesWinner()
        {
            TypeOfBet = Bet.BetType.TwoIssuesWinnerBet;
        }

        ~TwoIssuesWinner()
        { 
        }

        public override string MakeBetStr()
        {
            return "";
        }

        public override string ToString()
        {
            return teamCity + " wins (Two Issues)";
        }
    }

}
