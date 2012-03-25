using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mshtml;
using System.Net;

namespace NHLBetter
{

    public class Team
    {
        public string City = "";
        public string Abbreviation = "";
        public string LogoPath = "";
        public uint GamesPlayed { get; private set; }
        public uint Wins { get; private set; }
        public uint Losses { get; private set; }
        public uint OverTimeLosses { get; private set; }
        public uint Points { get; private set; }
        public float PointPercentage { get; private set; }
        public float GoalsPerGame { get; private set; }
        public float GoalsAgainstPerGame { get; private set; }
        public float FiveOnFiveForAgainstRatio { get; private set; }
        public float PowerPlayPercentage { get; private set; }
        public float PenaltyKillPercentage { get; private set; }
        public float ShotsPerGame { get; private set; }
        public float ShotsAgainstPerGame { get; private set; }
        public float WinningPercentageScoringFirst { get; private set; }
        public float WinningPercentageTrailingFist { get; private set; }
        public float WinningPercentageLeadingAfterOne { get; private set; }
        public float WinningPercentageLeadingAfterTwo { get; private set; }
        public float OutshootingPercentage { get; private set; }
        public float OutShotPercentage { get; private set; }
        public float FaceOffWinningPercentage { get; private set; }
        public List<MatchOver> MatchOverList = new List<MatchOver>();

        // Constructeur privé
        private Team()
        {
        }

        // Constructeur public
        public Team(string abbreviation)
        {
            var wc = new WebClient();
            var ms = new HTMLDocument();
            var htmlDoc = (IHTMLDocument2) ms;
            var teamList = new List<Team>();
            var teamStrList = new List<string>();
            
            var rawData =
                Encoding.ASCII.GetString(
                    wc.DownloadData("http://www.nhl.com/ice/teamstats.htm?season=20112012&gameType=2&viewName=summary"));
            htmlDoc.write(rawData);
            var htmlStr = htmlDoc.body.innerHTML;

            while (htmlStr.Contains("</TR>"))
            {
                var startIndex = htmlStr.IndexOf("<TR>");
                var endIndex = htmlStr.IndexOf("</TR>") + "</TR>".Length;
                teamStrList.Add(htmlStr.Substring(startIndex, endIndex - startIndex));
                htmlStr = htmlStr.Remove(startIndex, endIndex - startIndex);
            }

            foreach (var teamStr in teamStrList)
            {
                if (teamStr.IndexOf("rel=" + abbreviation + ">") != -1)
                {
                    this.FillTeamStats(RawDataToSeparatedStats(teamStr));
                    this.LogoPath = "Logos\\" + abbreviation + ".png";
                }
            }

            var numberOfPages = (int) GamesPlayed/30;
            if (GamesPlayed % 30 > 0)
                numberOfPages++;

            var MatchOverStrList = new List<string>();
            for (var i = 1; i <= numberOfPages; i++)
            {
                rawData = Encoding.ASCII.GetString(
                    wc.DownloadData("http://www.nhl.com/ice/gamestats.htm?season=20112012&gameType=2&team=" +
                                    abbreviation + "&viewName=gameSummary&pg=" + i));
                var currentPage = numberOfPages - i + 1;
                var startIndex = rawData.LastIndexOf("<tbody>");
                var count = rawData.IndexOf("</tbody>", startIndex) - startIndex;
                htmlStr = rawData.Substring(startIndex, count);

                MatchOverStrList.AddRange(StringSeparator(htmlStr, "<tr>", "</tr>"));
            }

            foreach(var MatchOverStr in MatchOverStrList)
            {
                const string stringAfter = "</td";
                const char charBefore = '>';
                var separatedStats = StringSeparator(MatchOverStr, "<td ", stringAfter); 
                
                var oDate = GetStr(separatedStats[0], charBefore, "</a>");
                var oTeamAgainstAbb = GetStr(separatedStats[5], charBefore, stringAfter);
                var oDecision = GetChar(separatedStats[2], stringAfter);
                var oIsHome = GetChar(separatedStats[4], stringAfter) == 'H';
                var oGoalsFor = GetInt(separatedStats[7], charBefore, stringAfter);
                var oGoalsAgainst = GetInt(separatedStats[8], charBefore, stringAfter);
                var oPowerPlayGoals = GetInt(separatedStats[10], charBefore, stringAfter);
                var oPowerPlayOpportunities = GetInt(separatedStats[11], charBefore, stringAfter);
                var oPowerPlayGoalsAgainst = GetInt(separatedStats[12], charBefore, stringAfter);
                var oTimesShorthanded = GetInt(separatedStats[13], charBefore, stringAfter);
                var oShotsFor = GetInt(separatedStats[14], charBefore, stringAfter);
                var oShotsAgainst = GetInt(separatedStats[15], charBefore, stringAfter);
                var oWinningGoalie = GetStr(separatedStats[16], charBefore, "</a>");
                
                MatchOverList.Add(new MatchOver(oDate, oTeamAgainstAbb, oDecision,
                                                oIsHome, oGoalsFor, oGoalsAgainst, oPowerPlayGoals,
                                                oPowerPlayOpportunities, oPowerPlayGoalsAgainst, oTimesShorthanded,
                                                oShotsFor, oShotsAgainst, oWinningGoalie));
            }
        }

        //Gets the date string
        private static string GetStr(string stringToParse, char separatorTo, string separatorFrom)
        {
            var index = stringToParse.IndexOf(separatorFrom) - 1;
            var returnStr = "";

            while (stringToParse[index] != separatorTo)
            {
                returnStr = returnStr.Insert(0, stringToParse[index--].ToString());
            }

            return returnStr;
        }


        private static char GetChar(string stringToParse, string after)
        {
            return stringToParse[stringToParse.IndexOf(after) - 1];
        }

        private static int GetInt(string stringToParse, char separatorTo, string separatorFrom)
        {
            var index = stringToParse.IndexOf(separatorFrom) - 1;
            var returnStr = "";
            
            while(stringToParse[index] != separatorTo)
            {
                returnStr = returnStr.Insert(0, stringToParse[index--].ToString());
            }

            return int.Parse(returnStr);
        }

        /// <summary>
        /// Fill team stats with Separated Stats (String --> Values)
        /// </summary>
        /// <param name="separatedStats"></param>
        public void FillTeamStats(List<string> separatedStats)
        {
            // Lit chacune des stats et les met dans le bon champ de la classe
            Abbreviation = separatedStats[0];
            City = separatedStats[1];
            GamesPlayed = uint.Parse(separatedStats[2]);
            Wins = uint.Parse(separatedStats[3]);
            Losses = uint.Parse(separatedStats[4]);
            OverTimeLosses = uint.Parse(separatedStats[5]);
            Points = uint.Parse(separatedStats[6]);
            PointPercentage = float.Parse(separatedStats[7]);
            GoalsPerGame = float.Parse(separatedStats[8]);
            GoalsAgainstPerGame = float.Parse(separatedStats[9]);
            FiveOnFiveForAgainstRatio = float.Parse(separatedStats[10]);
            PowerPlayPercentage = float.Parse(separatedStats[11]);
            PenaltyKillPercentage = float.Parse(separatedStats[12]);
            ShotsPerGame = float.Parse(separatedStats[13]);
            ShotsAgainstPerGame = float.Parse(separatedStats[14]);
            WinningPercentageScoringFirst = float.Parse(separatedStats[15]);
            WinningPercentageTrailingFist = float.Parse(separatedStats[16]);
            WinningPercentageLeadingAfterOne = float.Parse(separatedStats[17]);
            WinningPercentageLeadingAfterTwo = float.Parse(separatedStats[18]);
            OutshootingPercentage = float.Parse(separatedStats[19]);
            OutShotPercentage = float.Parse(separatedStats[20]);
            FaceOffWinningPercentage = float.Parse(separatedStats[21]);
        }

        /// <summary>
        /// Take the Raw Data string and put it into a list of Separated Stats
        /// </summary>
        /// <param name="statStr"></param>
        /// <returns></returns>
        private List<string> RawDataToSeparatedStats(string statStr)
        {
            // Liste de statistiques
            List<string> separatedStats;

            // Pour les string de Abbreviation et City
            int startIndex = statStr.IndexOf("rel=") + "rel=".Length;
            int endIndex = statStr.IndexOf("</A>");

            // Crée les séparateurs de string
            var separators = new string[4];
            separators[0] = "</TD>\r\n<TD>";
            separators[1] = "</TD>\r\n<TD style=\"TEXT-ALIGN: center\">";
            separators[2] = "</TD></TR>";
            separators[3] = "</TD>\r\n<TD class=active>";

            // On prend chacune des string de chaque côté de '<'
            Abbreviation = statStr.Substring(startIndex, endIndex - startIndex).Split('>')[0];
            City = statStr.Substring(startIndex, endIndex - startIndex).Split('>')[1];

            // Ajuste la string à ce qu'on a besoin, et remplace les virgules par des points
            statStr = statStr.Remove(0, endIndex + "</A>".Length).Replace('.', ',');

            // Crée un string[] en fonction des séparateurs et la storer dans une liste
            separatedStats = statStr.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
            separatedStats.Insert(0, Abbreviation);
            separatedStats.Insert(1, City);
            
            return separatedStats;
        }

        public override string ToString()
        {
            return City + " (" + Abbreviation + ") : " + GamesPlayed + ", " + Wins + ", " + Losses + ", " +
                   OverTimeLosses + ", " + Points + ", " + PointPercentage + ", " + GoalsPerGame + ", " + GoalsAgainstPerGame + ", " +
                   FiveOnFiveForAgainstRatio + ", " + PowerPlayPercentage + ", " + PenaltyKillPercentage + ", " + ShotsPerGame + ", " + 
                   ShotsAgainstPerGame + ", " + WinningPercentageScoringFirst + ", " + WinningPercentageTrailingFist + ", " +
                   WinningPercentageLeadingAfterOne + ", " + WinningPercentageLeadingAfterTwo + ", " + OutshootingPercentage + ", " + 
                   OutShotPercentage + ", " + FaceOffWinningPercentage + ", ";
        }

        private List<string> StringSeparator(string stringToSeparate, string separatorFrom, string separatorTo)
        {
            if (separatorFrom != separatorTo && separatorFrom != "" && separatorTo != "" &&
                stringToSeparate.Contains(separatorFrom) && stringToSeparate.Contains(separatorTo))
            {
                var stringList = new List<string>();

                stringToSeparate = stringToSeparate.Remove(0, stringToSeparate.IndexOf(separatorFrom));
                while (stringToSeparate.Contains(separatorFrom))
                {
                    var startIndex = stringToSeparate.IndexOf(separatorFrom);
                    var endIndex = stringToSeparate.IndexOf(separatorTo) + separatorTo.Length;

                    if (startIndex < endIndex && startIndex != -1 && endIndex != -1 && startIndex != endIndex)
                    {
                        stringList.Add(stringToSeparate.Substring(startIndex, endIndex - startIndex));
                        stringToSeparate = stringToSeparate.Remove(startIndex, endIndex - startIndex);
                    }
                    else if (startIndex != -1 && endIndex != -1 && startIndex != endIndex)
                    {
                        stringList.Add(stringToSeparate.Substring(endIndex, startIndex - endIndex));
                        stringToSeparate = stringToSeparate.Remove(endIndex, startIndex - endIndex);
                    }
                    else if (startIndex == -1 || endIndex == -1 || startIndex == endIndex ||
                        ((!stringToSeparate.Contains(separatorFrom) || !stringToSeparate.Contains(separatorTo)) && stringList.Count > 0))
                    {
                        return stringList;
                    }
                    else
                    {
                        return null;
                    }
                }

                //List of strings is returned
                return stringList;
            }
            //if conditions were'nt satisfied, null pointer is returned
            return null;
        }
    }
}