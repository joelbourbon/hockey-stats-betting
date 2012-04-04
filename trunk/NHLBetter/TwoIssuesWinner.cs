using System;
using System.Collections.Generic;

namespace NHLBetter
{

    public class TwoIssuesWinner:Bet
    {
        public TwoIssuesWinner()
        {
            TypeOfBet = Bet.BetType.TwoIssuesWinnerBet;
            multiplicator = 2;
        }

        ~TwoIssuesWinner()
        { 
        }

        public override string ToString()
        {
            return teamCity + " wins (Two Issues)";
        }

        public override void Probs()
        {
            //Initializes prob value to 0
            prob = 0;
            var probTie = 0.0;

            //Opponent is the Team on which this bet doesn't bet on
            var Opponent = (AssociatedMatch.TeamList[0].City == TeamBetOn.City ? AssociatedMatch.TeamList[1] : AssociatedMatch.TeamList[0]);

            const int precision = 10;
            var randVar = 0;

            var arrayOfMatchList_TBO = new List<MatchOver>[precision];
            var arrayOfMatchList_TBA = new List<MatchOver>[precision];
            var probArray_TBO = new double[precision];
            var probArray_TBA = new double[precision];
            var OTProportion = ((double)Opponent.OverTimeLosses/Opponent.GamesPlayed) /
                (((double)Opponent.OverTimeLosses / Opponent.GamesPlayed) + ((double)TeamBetOn.OverTimeLosses / TeamBetOn.GamesPlayed));
            
            while (randVar != precision)
            {
                arrayOfMatchList_TBO[randVar] = new List<MatchOver>();

                foreach (var match in TeamBetOn.MatchOverList)
                {
                    if (match.goalsFor == randVar)
                    {
                        arrayOfMatchList_TBO[randVar].Add(match);
                    }
                }

                probArray_TBO[randVar] = (double)arrayOfMatchList_TBO[randVar].Count / TeamBetOn.GamesPlayed;

                arrayOfMatchList_TBA[randVar] = new List<MatchOver>();

                foreach (var match in Opponent.MatchOverList)
                {
                    if (match.goalsFor == randVar)
                    {
                        arrayOfMatchList_TBA[randVar].Add(match);
                    }
                }

                probArray_TBA[randVar] = (double)arrayOfMatchList_TBA[randVar].Count / Opponent.GamesPlayed;
                randVar++;
            }

            //Probability of winning WITHOUT needs of overtime
            for (randVar = 0; randVar < precision; randVar++)
            {
                var sumOfTBAProbs = 0.0;
                for (var j = 0; j < randVar; j++)
                {
                    sumOfTBAProbs += probArray_TBA[j];
                }
                prob += probArray_TBO[randVar] * sumOfTBAProbs * 100;
            }
            
            //Probability that the game goes overtime
            for (randVar = 0; randVar < precision; randVar++)
            {
                probTie += probArray_TBA[randVar] * probArray_TBO[randVar] * 100;
            }

            prob += probTie;
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            var index = betList.IndexOf(this);
            var match = GetAssociatedMatch();
            var betAfter = betList[index + 1];
            var matchAfter = betAfter.GetAssociatedMatch();
            var indexOfFirstTwoIssuesBet = 0;

            //Gets the first index of the twoIssuesWinnerBet
            foreach (var bet in betList)
            {
                if (bet.GetBetType() == BetType.TwoIssuesWinnerBet)
                {
                    if (indexOfFirstTwoIssuesBet == 0)
                    {
                        indexOfFirstTwoIssuesBet = betList.IndexOf(bet);
                        break;
                    }
                }
            }

            //We want to make sure the index of the bet is a factor of 2, starting at the first index of the two issues winner bets
            if ((index - indexOfFirstTwoIssuesBet) % multiplicator == 0)
            {
                if (match == null || matchAfter == null)
                {
                    betList.RemoveRange(index, multiplicator);
                }
                else if (!match.Equals(matchAfter))
                {
                    betList.RemoveRange(index, multiplicator);
                }
            }

            return betList;
        }
    }
}
