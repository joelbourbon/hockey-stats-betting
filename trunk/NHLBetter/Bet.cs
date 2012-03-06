using System.Collections.Generic;

namespace NHLBetter
{
    public class Bet
    {
        public enum BetType
        {
            NoType,
            ThreeIssuesWinnerBet,
            TwoIssuesWinnerBet,
            NumberOfGoalsBet,
            WinnerWithGoalDifferenceBet,
            PlayerDuelBet,
            YesOrNoBet,
            ShotsOnGoalBet,
            MostMinutesOfPenaltyBet,
            TeamWithMoreHitsBet,
            FirstGoalBet,
            GainMarginBet,
            ExactScoreBet
        };

        public Match AssociatedMatch;
        public Team TeamBetOn;
        public BetType TypeOfBet;
        public double Odd;
        public int Pid;
        public string teamCity;
        public bool gameIsToday;
        public bool isTie = false;
        public string iniString;
        public List<string> usedFields = new List<string>(); 

        public Bet()
        {

        }

        ~Bet()
        {

        }

        public Bet(BetType typeOfBet, Team teamBetOn)
        {
            TypeOfBet = typeOfBet;
            TeamBetOn = teamBetOn;
        }

        public BetType GetBetType()
        {
            return TypeOfBet;
        }

        public Match GetAssociatedMatch()
        {
            return AssociatedMatch;
        }

        virtual public string MakeBetStr()
        {
            const string BetStr = "Not implemented yet";

            return BetStr;
        }

        // Virtual method for initialization
        virtual public void Initialize()
        {
            UsedFields();
            IniGetTeam();
            IniGetOdd();
            IniGetPid();
        }

        virtual protected void UsedFields()
        {
            AddAllLabelsToUsedFields("GamesPlayedLbl");
        }

        public double GetOdd()
        {
            return Odd;
        }

        // Use of a string in the case of less-than-five-numbers pids
        public string GetPidString()
        {
            return Pid.ToString().Length == 5 ? Pid.ToString() : "0" + Pid;
        }

        protected void AddAllLabelsToUsedFields(string GenericLbl)
        {
            var LblHome = GenericLbl.Insert(GenericLbl.IndexOf("Lbl"), "Home");
            var LblAway = GenericLbl.Insert(GenericLbl.IndexOf("Lbl"), "Away");
            usedFields.Add(GenericLbl);
            usedFields.Add(LblHome);
            usedFields.Add(LblAway);
        }

        // Virtual method for getting team city name
        virtual protected void IniGetTeam()
        {
            teamCity = "";

            var index = iniString.IndexOf("descActivite=\"") + "descActivite=\"".Length;
            while(iniString[index] != '\"' && iniString[index] != ',')
            {
                teamCity += iniString[index++];
            }
        }

        // Virtual method for getting loto-quebec odds
        virtual protected void IniGetOdd()
        {
            var oddStr = "";
            
            var index = iniString.IndexOf("<BR>") + "<BR>".Length;
            while(iniString[index] != '<')
            {
                //Double.Parse does not take dots... we change it for a comma
                if (iniString[index] == '.')
                {      
                    oddStr += ",";
                    index++;
                }
                else
                    oddStr += iniString[index++];
            }

            Odd = double.Parse(oddStr);
        }

        // Virtual method for getting loto-quebec product ID number of the bet-object
        virtual protected void IniGetPid()
        {
            var pidStr = "";

            var index = iniString.IndexOf(":&nbsp;") + ":&nbsp;".Length;
            while (iniString[index] != '<')
            {
                pidStr += iniString[index++];
            }

            Pid = int.Parse(pidStr);
        }

        // Gets the Match according to the team city
        public void AssociateMatch(List<Match> matchList)
        {
            AssociatedMatch = null;
            foreach(var match in matchList)
            {
                if(teamCity.ToUpper() == match.GetAwayTeam().City.ToUpper() || 
                    teamCity.ToUpper() == match.GetHomeTeam().City.ToUpper())
                {
                    gameIsToday = true;
                    AssociatedMatch = match;
                    return;
                }
            }
        }

        // Gets the team according to the Match played and the teamCity set (usually in bet child)
        public void AssociateTeam()
        {
            TeamBetOn = null;
            foreach(var team in AssociatedMatch.TeamList)
            {
                if (teamCity.ToUpper() == team.City.ToUpper())
                {
                    TeamBetOn = team;
                    return;
                }
            }
        }

        public override string ToString()
        {
            return "Not a bet";
        }
    }
}