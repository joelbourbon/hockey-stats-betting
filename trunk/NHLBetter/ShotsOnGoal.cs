namespace NHLBetter
{
    public class ShotsOnGoal:Bet
    {
        public bool isMoreThan;
        public double numberOfShots;
        
        //Special cases where you can bet on a player's number of shots
        public string specialPlayer;
        public bool special;

        public ShotsOnGoal()
        {
            TypeOfBet = Bet.BetType.ShotsOnGoalBet;
        }

        ~ShotsOnGoal()
        { 
        }

        override public void Initialize()
        {
            var startIndex = 0;
            var endIndex = 0;
            var numberOfShotsStr = "";

            IniGetOdd();
            IniGetPid();

            special = iniString.IndexOf("\"gabarit") != -1;
            if (special)
            {
                isMoreThan = iniString.Contains(" ou plus");
                startIndex = 0;
                endIndex = 0;
                teamCity = "Montreal"; //Only happens with montreal games 
                specialPlayer = iniString.Substring(startIndex, endIndex - startIndex);
            }
            else
            {
                IniGetTeam(); 
                isMoreThan = iniString.Contains("Plus de ");

                var index = iniString.IndexOf(" de ") + " de ".Length;
             
                while (iniString[index] != '\"')
                {
                    numberOfShotsStr += iniString[index++];
                }
                numberOfShots = double.Parse(numberOfShotsStr);
            }
        }

        protected override void IniGetTeam()
        {
            var startIndex = iniString.IndexOf("class=\"") + "class=\"".Length;
            var endIndex = iniString.IndexOf("gabarit");
            teamCity = iniString.Substring(startIndex, endIndex - startIndex);
        }

        public override string ToString()
        {
            return teamCity + " shoots " + (isMoreThan ? "more than " : "less than ") + numberOfShots;
        }
    }
}