using System;
using System.Collections.Generic;

namespace NHLBetter
{
    public class Bet
    {
        public enum BetType
        {
            NoType,
            ThreeIssuesWinnerBet,
            Overtime,
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
        public int betID;
        public int multiplicator;
        public double prob;

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

        ////////////////////////////////////////////////////////////////////////////////////
        /// INITIALIZERS
        ////////////////////////////////////////////////////////////////////////////////////
        virtual public void Initialize()
        {
            UsedFields();
            IniGetTeam();
            IniGetOdd();
            IniGetPid();
            IniGetId();

            // Bug fix 'é' token is replaced by '?' token, so we replace it with 'e' token
            teamCity = teamCity.Replace('?', 'e');
        }     //Initializer
        virtual protected void IniGetId()
        {
            var idStr = "";
            var index = 0;
            while(iniString[index] >= '0' && iniString[index] <= '9')
            {
                idStr += iniString[index++];
            }
            betID = int.Parse(idStr);
        }    //Id Initializer
        virtual protected void IniGetTeam()
        {
            teamCity = "";

            var index = iniString.IndexOf("descActivite=\"") + "descActivite=\"".Length;
            while (iniString[index] != '\"' && iniString[index] != ',')
            {
                teamCity += iniString[index++];
            }
        }  //Team Initializer
        virtual protected void IniGetOdd()
        {
            var oddStr = "";

            var index = iniString.IndexOf("<BR>") + "<BR>".Length;
            while (iniString[index] != '<')
            {
                //Double.Parse does not take dots... we change it for a comma
                if (iniString[index] == '.')
                {
                    oddStr += ",";
                }
                else
                {
                    oddStr += iniString[index];
                }

                index++;
            }

            Odd = double.Parse(oddStr);
        }   //Odd Initializer
        virtual protected void IniGetPid()
        {
            var pidStr = "";

            var index = iniString.IndexOf(":&nbsp;") + ":&nbsp;".Length;
            while (iniString[index] != '<')
            {
                pidStr += iniString[index++];
            }

            Pid = int.Parse(pidStr);
        }   //PID Initializer

        ////////////////////////////////////////////////////////////////////////////////////
        /// GETTERS
        ////////////////////////////////////////////////////////////////////////////////////
        public double GetOdd()
        {
            return Odd;
        }                //Odd Getter
        public BetType GetBetType()
        {
            return TypeOfBet;
        }           //BetType Getter
        public Match GetAssociatedMatch()
        {
            return AssociatedMatch;
        }     //AssociatedMatch getter
        public double GetProb()
        {
            return GetProb(3);
        }               //Default GetProb (precision of 3 decimals)
        public double GetProb(int precision)
        {
            return Math.Round(prob, precision);
        }  //Specific GetProb (variable number of decimals)
        public double GetDuty()
        {
            return GetDuty(3);
        }               //Default GetDuty (Precision of 3 decimals)
        public double GetDuty(int precision)
        {
            return Math.Round(GetOdd() * GetProb(precision) / 100, precision);
        }  //Specific GetDuty (variable number of decimals)
        public string GetPidString()
        {
            return Pid.ToString().Length == 5 ? Pid.ToString() : "0" + Pid;
        }          //PIDString getter

        /// <summary>
        /// 
        /// </summary>
        virtual protected void UsedFields()
        {
            AddAllLabelsToUsedFields("GamesPlayedLbl");
        }

        protected void AddAllLabelsToUsedFields(string GenericLbl)
        {
            var LblHome = GenericLbl.Insert(GenericLbl.IndexOf("Lbl"), "Home");
            var LblAway = GenericLbl.Insert(GenericLbl.IndexOf("Lbl"), "Away");
            usedFields.Add(GenericLbl);
            usedFields.Add(LblHome);
            usedFields.Add(LblAway);
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
            return "Not Implemented yet (" + TypeOfBet + ")";
        }

        virtual public void Probs()
        {
            prob = 0;
        }

        public virtual List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}