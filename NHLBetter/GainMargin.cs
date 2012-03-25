using System.Collections.Generic;

namespace NHLBetter
{

    public class GainMargin : Bet
    {
        public bool isMoreThan;
        public int numberOfGoals;

        public GainMargin()
        {
            TypeOfBet = Bet.BetType.GainMarginBet;
            multiplicator = 8;
        }

        ~GainMargin()
        {
        }

        override public void Initialize()
        {
            base.Initialize();
            isMoreThan = iniString.Contains("ou plus");

            var numberOfGoalsStr = "";
            var index = iniString.IndexOf("par ") + "par ".Length;

            while(iniString[index] != ' ')
            {
                numberOfGoalsStr += iniString[index++];
            }

            numberOfGoals = int.Parse(numberOfGoalsStr);
        }

        public override string ToString()
        {
            return teamCity + " wins by " + numberOfGoals + " goals" + (isMoreThan ? " or more" : "");
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}