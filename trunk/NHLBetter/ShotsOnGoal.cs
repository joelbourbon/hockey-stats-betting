using System;
using System.Collections.Generic;

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
            multiplicator = 2;
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
            IniGetId();

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

        public override void Probs()
        {
            if (!special)
            {
                //TeamBetAgainst is the Team on which this bet doesn't bet on
                var TeamBetAgainst = (AssociatedMatch.TeamList[0].City == TeamBetOn.City
                                          ? AssociatedMatch.TeamList[1]
                                          : AssociatedMatch.TeamList[0]);

                var arrayOfMatchList = new List<MatchOver>();
                var MoreShotsAnyTeam_TBO = 0;   //Associated to prob1 and proportion1
                var prob1 = 0.0;
                const double proportion1 = 0.3333; 
                
                var MoreShotsSameTeam_TBO  = 0; //Associated to prob2 and proportion2
                var prob2 = 0.0;
                const double proportion2 = 0.3334;
                
                var MoreShotsOn_TBA = 0;        //Associated to prob3 and proportion3
                var prob3 = 0.0;
                const double proportion3 = 0.3333;

                //Gets prob1 AND gets a matchOver list of the two teams records
                foreach(var match in TeamBetOn.MatchOverList)
                {
                    if(match.teamAgainstAbb == TeamBetAgainst.Abbreviation)
                    {
                        arrayOfMatchList.Add(match);
                    }
                    if ((match.shotsFor > numberOfShots && isMoreThan) || (match.shotsFor < numberOfShots && !isMoreThan))
                        MoreShotsAnyTeam_TBO++;
                }
                prob1 = TeamBetOn.GamesPlayed > 0 ? (double) MoreShotsAnyTeam_TBO/TeamBetOn.GamesPlayed : 0.5;


                //Gets prob2
                foreach (var match in arrayOfMatchList)
                {
                    if ((match.shotsFor > numberOfShots && isMoreThan) || (match.shotsFor < numberOfShots && !isMoreThan))
                        MoreShotsSameTeam_TBO++;
                }
                prob2 = arrayOfMatchList.Count > 0 ? (double) MoreShotsSameTeam_TBO/arrayOfMatchList.Count : 0.5;

                foreach(var match in TeamBetAgainst.MatchOverList)
                {
                    if ((match.shotsAgainst > numberOfShots && isMoreThan) || (match.shotsAgainst < numberOfShots && !isMoreThan))
                        MoreShotsOn_TBA++;
                }
                prob3 = TeamBetAgainst.GamesPlayed > 0 ? (double) MoreShotsOn_TBA/TeamBetAgainst.GamesPlayed : 0.5;

                prob = proportion1*prob1 + proportion2*prob2 + proportion3*prob3;
                prob *= 100;
            }
            else
            {
                
            }
        }
    }
}