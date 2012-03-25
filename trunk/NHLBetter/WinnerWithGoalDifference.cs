using System.Collections.Generic;

namespace NHLBetter
{
    class WinnerWithGoalDifference:Bet
    {
        public bool isMoreThan;
        public double goalDifference;

        public WinnerWithGoalDifference()
        {
            TypeOfBet = BetType.WinnerWithGoalDifferenceBet;
            multiplicator = 2;
        }

        ~WinnerWithGoalDifference()
        {
        }

        override public void Initialize()
        {
            base.Initialize();

            isMoreThan = iniString.Contains("+");

            var goalDifferenceStr = "";
            var index = iniString.IndexOf("but(s)") - 4; 
            while(iniString[index] != ' ')
            {
                goalDifferenceStr += iniString[index++];
            }

            goalDifference = double.Parse(goalDifferenceStr);
        }

        public override string ToString()
        {
            return teamCity + " wins by " + (isMoreThan ? " more than " : " less than ") + goalDifference + " goals ";
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}
