using System.Collections.Generic;

namespace NHLBetter
{
    public class NumberOfGoals : Bet
    {
        public bool isMoreThan;
        public double numberOfGoals;

        public NumberOfGoals()
        {
            TypeOfBet = Bet.BetType.NumberOfGoalsBet;
            multiplicator = 2;
        }

        ~NumberOfGoals()
        {
        }

        override public void Initialize()
        {
            base.Initialize();
            IniGetComparison();
        }

        private void IniGetComparison()
        {
            var index = iniString.IndexOf("souligne>") + "souligne>".Length;
            var compStr = "";
            var numberOfGoalsStr = "";

            while (iniString[index] != '<')
            {
                compStr += iniString[index++];
            } index -= 3;

            isMoreThan = compStr.Contains("More than");

            while (iniString[index] != '<')
            {
                numberOfGoalsStr += iniString[index++];
            }

            numberOfGoals = double.Parse(numberOfGoalsStr);
        }

        public override string ToString()
        {
            return (isMoreThan ? "More than " : "Less than ") + numberOfGoals + " goals";
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}