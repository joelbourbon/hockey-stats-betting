using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace HockeyStats.classes
{
    public class NumberOfGoals : Bet
    {
        public bool isMoreThan;
        public double numberOfGoals;

        public NumberOfGoals()
        {
            TypeOfBet = Bet.BetType.NumberOfGoalsBet;
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
    }
}