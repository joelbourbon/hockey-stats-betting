
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;
using mshtml;

namespace NHLBetter
{
    public class HockeyDay
    {
        public List<Match> MatchList = new List<Match>();
        public List<Bet> BetList = new List<Bet>();
        private int parsingMethod = 0;
        private int[] BetsOfEachType = new int[12];
        public string nhlRawData;
        public string mojRawData;
        
        //RefreshData
        //This method refreshes data from NHL.com and miseojeu.com
        public void RefreshData()
        {
            const bool isLoadCalled = false;
            Cursor.Current = Cursors.AppStarting;
            
            //Clears the lists if they exist
            ClearLists();

            parsingMethod = 0;

            MatchList = GetListOfGamesToday("http://www.nhl.com/ice/schedulebyday.htm#?navid=nav-sch-today", isLoadCalled);
            BetList = GetListOfBetsToday("https://miseojeu.lotoquebec.com/en/betting-offer/hockey/national/matches?idAct=2", isLoadCalled);
            
            Cursor.Current = Cursors.Default;
        }

        // GetListOfGamesToday
        // This method queries nhl.com to get games available today
        // Parameters : 
        // adress       The network adress to get source code from
        public List<Match> GetListOfGamesToday(string adress , bool isLoadCalled)
        {
            var matchList = new List<Match>();
            var ms = new HTMLDocument();
            var htmlDoc = (IHTMLDocument2) ms;
            var wc = new WebClient();
            var matchStr = new List<string>();

            var rawData = isLoadCalled ? nhlRawData : Encoding.ASCII.GetString(wc.DownloadData(adress));
            nhlRawData = rawData;

            htmlDoc.write(rawData);
            var htmlStr = htmlDoc.body.innerHTML;

            // Gets wanted arrays only
            htmlStr = htmlStr.Substring(htmlStr.IndexOf("VISITING TEAM"),
                                        htmlStr.LastIndexOf("</TBODY>") - htmlStr.IndexOf("VISITING TEAM"));

            while (htmlStr.Contains("</TR>") && htmlStr.LastIndexOf("<TR>") > -1 && htmlStr.LastIndexOf("/TR") > -1)
            {
                var startIndex = htmlStr.LastIndexOf("<TR>");
                var endIndex = htmlStr.LastIndexOf("</TR>") + "</TR>".Length;

                if (startIndex > -1 && endIndex > -1)
                {
                    var newStr = htmlStr.Substring(startIndex, endIndex - startIndex);

                    if (!newStr.Contains("VISITING TEAM") && newStr != "")
                    {
                        matchStr.Add(newStr);
                        htmlStr = htmlStr.Remove(startIndex, endIndex - startIndex);
                    }
                    else if (newStr.Contains("VISITING TEAM"))
                    {
                        htmlStr = htmlStr.Remove(startIndex, endIndex - startIndex);
                    }
                }
            }

            foreach (var matchString in matchStr)
            {
                // String indexes
                var startIndexAwayTeam = matchString.IndexOf("rel=") + "rel=".Length;
                var startIndexHomeTeam = matchString.LastIndexOf("rel=") + "rel=".Length;
                var startIndexTimeOfGame = matchString.IndexOf("skedStartTimeLocal>") + "skedStartTimeLocal>".Length;
                var endIndexAwayTeam = startIndexAwayTeam + "ABB".Length;
                var endIndexHomeTeam = startIndexHomeTeam + "ABB".Length;
                var endIndexTimeOfGame = startIndexTimeOfGame + "HH:MM AM".Length;
                var isBetOver = false;

                // Gets Teams' abbreviations
                var awayTeamAbbreviation = matchString.Substring(startIndexAwayTeam,
                                                                 endIndexAwayTeam - startIndexAwayTeam);
                var homeTeamAbbreviation = matchString.Substring(startIndexHomeTeam,
                                                                 endIndexHomeTeam - startIndexHomeTeam);

                // Gets Time of the game
                var timeString = matchString.Substring(startIndexTimeOfGame, endIndexTimeOfGame - startIndexTimeOfGame);
                var timeOfGame = GetTimeOfGameFromTimeString(timeString);
                var timeToday = System.DateTime.Now.Hour;

                // Creates Home team et Away Team
                var awayTeam = new Team(awayTeamAbbreviation, false);
                var homeTeam = new Team(homeTeamAbbreviation, false);

                if (timeToday >= timeOfGame)
                    isBetOver = true;

                // Adds the match to the match list
                matchList.Add(new Match(awayTeam, homeTeam, timeOfGame, isBetOver));
            }

            return matchList;
        }

        // Populates a double TimeOfGame from a timeString
        private static double GetTimeOfGameFromTimeString(string timeString)
        {
            var timeOfGame = 0.0;
            var offSet = 0;

            if(timeString.EndsWith(""))
            {
                if (timeString.Contains("AM"))
                    offSet = 0;
                else if (timeString.Contains("PM"))
                    offSet = 12;
                
                timeOfGame = timeString[0] - '0';
                timeOfGame += (double)(timeString[2] - '0') / 6;
                timeOfGame += (double)(timeString[3] - '0') / 60;
            }
            else
            {
                if (timeString.Contains("AM"))
                    offSet = 0;
                else if (timeString.Contains("PM"))
                    offSet = 12;
                
                timeOfGame = (timeString[0] - '0') * 10;
                timeOfGame += timeString[1] - '0';
                timeOfGame += (double)(timeString[3] - '0') / 6;
                timeOfGame += (double)(timeString[4] - '0') / 60;
            }

            return timeOfGame + offSet;
        }

        private static List<string> StringSeparator(string stringToSeparate, string separatorFrom, string separatorTo)
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

        private static string FixHtmlStrForShotsOnGoal(string htmlStr)
        {
            var startIndex = 0;
            while (htmlStr.IndexOf("Total shots on goal in the match for ", startIndex) != -1)
            {
                var index = htmlStr.IndexOf("Total shots on goal in the match for ", startIndex) + "Total shots on goal in the match for ".Length;
                var endIndex = htmlStr.IndexOf(" (excluding SO)", index);
                startIndex = index;
                var teamStr = "";
                
                teamStr = htmlStr.Substring(startIndex, endIndex - startIndex);
                
                index = htmlStr.IndexOf("gabarit", endIndex);
                htmlStr = htmlStr.Insert(index, teamStr);
                
                endIndex = index + "gabarit".Length + teamStr.Length;
                
                index = htmlStr.IndexOf("gabarit", endIndex);
                htmlStr = htmlStr.Insert(index, teamStr);
            }

            return htmlStr;
        }

        public List<Bet> GetListOfBetsToday(string adress, bool isLoadCalled)
        {
            var betList = new List<Bet>();
            var ms = new HTMLDocument();
            var htmlDoc = (IHTMLDocument2) ms;
            var wc = new WebClient();
            var betStrList = new List<string>();
            var dateStrList = new List<string>();

            var rawData = isLoadCalled ? mojRawData : Encoding.ASCII.GetString(wc.DownloadData(adress));
            mojRawData = rawData;

            htmlDoc.write(rawData);
            var htmlStr = htmlDoc.body.innerHTML;

            //Todo Find a better fix for shots on goal
            htmlStr = FixHtmlStrForShotsOnGoal(htmlStr);

            //Gets boutonTypeSelected list of strings
            var boutonTypeSelectedList = StringSeparator(htmlStr, "boutonTypeSelected", "<TD class=");

            //DEBUG
            for (var i = 0; i < 12; i++)
                BetsOfEachType[i] = 0;

            foreach (var boutonStr in boutonTypeSelectedList)
            {
                //This list is created in function of the bets we are supposed to find in the source code
                betList.AddRange((IEnumerable<Bet>) MakeBetListForType(boutonStr));
            }

            //Memory management
            boutonTypeSelectedList.Clear();

            //This list is created in function of the bet we actually find in the source code
            betStrList.AddRange(StringSeparator(htmlStr, "<TD id=", "</SPAN> <SPAN>"));

            //If these two lists have the same size, then we can assume the lists are good.
            if (betStrList.Count != betList.Count)
            {
                BetsOfEachType[0]--;
                for (var i = 1; i < 12; i++)
                {
                    BetsOfEachType[i] += BetsOfEachType[i - 1];
                }

                parsingMethod++;
                if (parsingMethod > 1)
                    return null;

                // Recursive call if the counts are not equal, we try parsing htmlStr in a different way
                betList =
                    GetListOfBetsToday(
                        "https://miseojeu.lotoquebec.com/en/betting-offer/hockey/national/matches?idAct=2", isLoadCalled);
                return betList;
            }

            //Lists contain the same number of bets.
            for (var i = 0; i < betList.Count; i++)
            {
                betList[i].iniString = betStrList[i].Remove(0, "<TD id=".Length);
                betList[i].Initialize();
            }
            for (var i = 0; i < betList.Count; i++)
            {
                if (betList[i].GetBetType() == Bet.BetType.ExactScoreBet && betList[i].teamCity == "")
                {
                    betList[i].teamCity = betList[i - 1].teamCity;
                }
                else if (true)
                {
                    //other special conditions
                }

                // Fix to get the game correctly with the team of the bet
                if (betList[i].isTie)
                {
                    betList[i].teamCity = betList[i].GetBetType() == Bet.BetType.ExactScoreBet ? 
                                            betList[i - 1].teamCity : betList[i + 1].teamCity;
                }

                // These bets are not managed yet
                if (betList[i].GetBetType() != Bet.BetType.PlayerDuelBet &&
                    betList[i].GetBetType() != Bet.BetType.YesOrNoBet &&
                    betList[i].GetBetType() != Bet.BetType.FirstGoalBet)
                {

                    betList[i].AssociateMatch(MatchList);

                }

                if (betList[i].gameIsToday &&
                    betList[i].GetBetType() != Bet.BetType.PlayerDuelBet &&
                    //Does not manage the player duel bets... YET!  //
                    betList[i].GetBetType() != Bet.BetType.YesOrNoBet &&
                    //Does not manage the yes or no bets... YET!   <<AssociateTeam() would crash
                    betList[i].GetBetType() != Bet.BetType.FirstGoalBet)
                    //Does not manage the first goal bets... YET!   \\
                {
                    betList[i].AssociateTeam();
                    if (betList[i].isTie)
                    {
                        betList[i].teamCity = "Tie game";
                    }
                    if (betList[i].TeamBetOn != null)
                    {
                        betList[i].Probs();
                    }
                }
            }

            for(var i = 0 ; i < betList.Count ; i++)
            {
                var betListCount = betList.Count;
                var nextBetListCount = betList[i].ManageBetList(betList).Count;
                var countDiff = betListCount - nextBetListCount;

                betList = betList[i].ManageBetList(betList);
                if (countDiff != 0)
                    i--;
            }

            return betList;
        }

        public Match GetMatchFromTeamStr(string awayTeamStr, string homeTeamStr)
        {
            foreach(var match in MatchList)
            {
                if((match.TeamList[(int)Match.HomeAway.Away].City.Equals(awayTeamStr) && match.TeamList[(int)Match.HomeAway.Home].City.Equals(homeTeamStr)) ||  
                    (match.TeamList[(int)Match.HomeAway.Away].City.Equals(homeTeamStr) && match.TeamList[(int)Match.HomeAway.Home].City.Equals(awayTeamStr)))
                {
                    return match;
                }
            }
            return null;
        }

        private IEnumerable<Bet> MakeBetListForType(string boutonStr)
        {
            var betList = new List<Bet>();
            var BetType = Bet.BetType.NoType;
            var multiplicator = 0;

            if (boutonStr.Contains("Correct score"))
            {
                BetType = Bet.BetType.ExactScoreBet;
                multiplicator = 37;
            }
            else if (boutonStr.Contains("Match winner - 3 way") && !boutonStr.Contains("Total goals"))
            {
                BetType = Bet.BetType.ThreeIssuesWinnerBet;
                multiplicator = 3;
            }
            else if (boutonStr.Contains("Match winner - 2 way"))
            {
                BetType = Bet.BetType.TwoIssuesWinnerBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("Total goals"))
            {
                BetType = Bet.BetType.NumberOfGoalsBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("Match winner with goal spread"))
            {
                BetType = Bet.BetType.WinnerWithGoalDifferenceBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("Opposing players"))
            {
                BetType = Bet.BetType.PlayerDuelBet;
                multiplicator = 3;
            }
            else if (boutonStr.Contains("Yes or no?"))
            {
                BetType = Bet.BetType.YesOrNoBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("Total shots on goal"))
            {
                BetType = Bet.BetType.ShotsOnGoalBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("Most penalty minutes"))
            {
                BetType = Bet.BetType.MostMinutesOfPenaltyBet;
                multiplicator = 3;
            }
            else if (boutonStr.Contains("Most hits"))
            {
                BetType = Bet.BetType.TeamWithMoreHitsBet;
                multiplicator = 2;
            }
            else if (boutonStr.Contains("First goal of the match"))
            {
                BetType = Bet.BetType.FirstGoalBet;
                multiplicator = 20;
            }
            else if (boutonStr.Contains("Winning margin"))
            {
                BetType = Bet.BetType.GainMarginBet;
                multiplicator = 8;
            }

            //Wraps the number of the boutonTypeStr string
            var nbStr = StringSeparator(boutonStr, "title=\"", "bets")[0];
            nbStr = nbStr.Remove(nbStr.IndexOf("bets"), "bets".Length).Remove(0, "title=\"".Length);
            var nbFields = int.Parse(nbStr);
            var nbBets = 0;

            // The number of bets for shots on goal is function of Montreal playing...
            if (parsingMethod == 1 && BetType == Bet.BetType.ShotsOnGoalBet)
                nbBets = (nbFields- 2) * multiplicator + 10;
            else
                nbBets = nbFields*multiplicator;

            for (var i = 0; i < nbBets ; i++)
            {
                switch (BetType)
                {
                    case Bet.BetType.ThreeIssuesWinnerBet:
                        betList.Add(new ThreeIssuesWinner());
                        BetsOfEachType[0]++;
                        break;
                    case Bet.BetType.TwoIssuesWinnerBet:
                        betList.Add(new TwoIssuesWinner());
                        BetsOfEachType[1]++;
                        break;
                    case Bet.BetType.NumberOfGoalsBet:
                        betList.Add(new NumberOfGoals());
                        BetsOfEachType[2]++;
                        break;
                    case Bet.BetType.WinnerWithGoalDifferenceBet:
                        betList.Add(new WinnerWithGoalDifference());
                        BetsOfEachType[3]++;
                        break;
                    case Bet.BetType.PlayerDuelBet:
                        betList.Add(new PlayerDuel());
                        BetsOfEachType[4]++;
                        break;
                    case Bet.BetType.YesOrNoBet:
                        betList.Add(new YesOrNo());
                        BetsOfEachType[5]++;
                        break;
                    case Bet.BetType.ShotsOnGoalBet:
                        betList.Add(new ShotsOnGoal());
                        BetsOfEachType[6]++;
                        break;
                    case Bet.BetType.MostMinutesOfPenaltyBet:
                        betList.Add(new MostMinutesOfPenalty());
                        BetsOfEachType[7]++;
                        break;
                    case Bet.BetType.TeamWithMoreHitsBet:
                        betList.Add(new TeamWithMoreHits());
                        BetsOfEachType[8]++;
                        break;
                    case Bet.BetType.FirstGoalBet:
                        betList.Add(new FirstGoal());
                        BetsOfEachType[9]++;
                        break;
                    case Bet.BetType.GainMarginBet:
                        betList.Add(new GainMargin());
                        BetsOfEachType[10]++;
                        break;
                    case Bet.BetType.ExactScoreBet:
                        betList.Add(new ExactResult());
                        BetsOfEachType[11]++;
                        break;
                    

                    // No type : Error case
                    default:
                        break;
                }
            }

            return betList;
        }

        public string ConvertMOJToNHL(string MOJCity)
        {
            //Some cities cannot be simply converted to caps, they are written differently from one website to the other
            switch (MOJCity)
            {
                case "New York-I":
                    MOJCity = "NY ISLANDERS";
                    break;
                case "New York-R":
                    MOJCity = "NY RANGERS";
                    break;
                case "Floride":
                    MOJCity = "FLORIDA";
                    break;
                case "Saint Louis":
                    MOJCity = "ST LOUIS";
                    break;
                case "Philadelphie":
                    MOJCity = "PHILADELPHIA";
                    break;


                default:
                    break;
            }
            return MOJCity.ToUpper();
        }

        public void ClearLists()
        {
            if(MatchList != null)
                MatchList.Clear();
            
            if(BetList != null)
                BetList.Clear();
        }
    }
}