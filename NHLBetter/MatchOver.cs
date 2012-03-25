using System;
using System.Collections.Generic;

namespace NHLBetter
{
    public partial class MatchOver : Match
    {
        public enum GameDecision
        {
            eNoDecision = 0,
            eW = 1,
            eL = 2,
            eO = 3,
        };

        public DateTime date                { get; private set; }
        public string teamAgainstAbb        { get; private set; }
        public GameDecision decision        { get; private set; }
        public bool isHome                  { get; private set; }
        public int goalsFor                 { get; private set; }
        public int goalsAgainst             { get; private set; }
        public int powerPlayGoals           { get; private set; }
        public int powerPlayOpportunities   { get; private set; }
        public int powerPlayGoalsAgainst    { get; private set; }
        public int timesShorthanded         { get; private set; }
        public int shotsFor                 { get; private set; }
        public int shotsAgainst             { get; private set; }
        public string winningGoalie         { get; private set; }

        public MatchOver()
        { }

        public MatchOver(string iDate, string iTeamAgainstAbb, char iDecision, bool iIsHome, int iGoalsFor, int iGoalsAgainst, int iPowerPlayGoals, 
            int iPowerPlayOpportunities, int iPowerPlayGoalsAgainst, int iTimesShorthanded, int iShotsFor, int iShotsAgainst, string iWinningGoalie)
        {
            IsBetOver = true;
            TimeOfGame = 0.0;

            teamAgainstAbb = iTeamAgainstAbb;
            isHome = iIsHome;
            goalsFor = iGoalsFor;
            goalsAgainst = iGoalsAgainst;
            powerPlayGoals = iPowerPlayGoals;
            powerPlayOpportunities = iPowerPlayOpportunities;
            powerPlayGoalsAgainst = iPowerPlayGoalsAgainst;
            timesShorthanded = iTimesShorthanded;
            shotsFor = iShotsFor;
            shotsAgainst = iShotsAgainst;
            winningGoalie = iWinningGoalie;
            
            switch(iDecision)
            {
                case 'W':
                    decision = GameDecision.eW;
                    break;
                case 'L':
                    decision = GameDecision.eL;
                    break;
                case 'O':
                    decision = GameDecision.eO;
                    break;
                default :
                    decision = GameDecision.eNoDecision;
                    break;
            }

            date = GetDate(iDate);
        }

        ~MatchOver() { }

        private DateTime GetDate(string iDate)
        {
            //2012-03-17
            var dateStr = "";
            var yearStr = "20";
            var monthStr = "";
            var dayStr = "";

            var index = 0;

            while (iDate[index] != ' ')
            {
                monthStr += iDate[index++];
            } index ++;

            while (iDate[index] != ' ')
            {
                dayStr += iDate[index++];
            } index+=2;

            while (index < 10)
            {
                yearStr += iDate[index++];
            }

            monthStr = GetMonthIntStrFromMonthStr(monthStr);
            dateStr = yearStr + "-" + monthStr + "-" + dayStr;
            DateTime date = DateTime.Parse(dateStr);

            return date;
        }

        private static string GetMonthIntStrFromMonthStr(string monthStr)
        {
            switch (monthStr.ToLower())
            { 
                case "jan":
                    return "01";
                case "feb":
                    return "02";
                case "mar":
                    return "03";
                case "apr":
                    return "04";
                case "may":
                    return "05";
                case "jun":
                    return "06";
                case "jul":
                    return "07";
                case "aug":
                    return "08";
                case "sep":
                    return "09";
                case "oct":
                    return "10";
                case "nov":
                    return "11";
                case "dec":
                    return "12";
                default:
                    return "00";
            }
        }

        public override string ToString()
        {
            return (decision == GameDecision.eW ? "Won" : decision == GameDecision.eL ? "Lost" : "Tied") + 
                " " + goalsFor + " - " + goalsAgainst + " against " + teamAgainstAbb + " on " + date.ToString().Remove(10);
        }

    }
}
