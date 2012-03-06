namespace NHLBetter
{
    class WinnerWithGoalDifference:Bet
    {
        public bool isMoreThan;
        public double goalDifference;

        public WinnerWithGoalDifference()
        {
            TypeOfBet = BetType.WinnerWithGoalDifferenceBet;
        }

        ~WinnerWithGoalDifference()
        {
        }

        override public void Initialize()
        {
            IniGetTeam();
            IniGetOdd();
            IniGetPid();

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
    }
}
