using System;
using System.Collections.Generic;

namespace NHLBetter
{

    public class ThreeIssuesWinner : Bet
    {

        //Default Constructor
        public ThreeIssuesWinner()
        {
            TypeOfBet = Bet.BetType.ThreeIssuesWinnerBet;
            multiplicator = 3;
        }

        //Destructor
        ~ThreeIssuesWinner()
        {
        }

        public override void Initialize()
        {
            isTie = iniString.Contains("Nul");
            base.Initialize();
        }

        public override string ToString()
        {
            if (isTie)
            {
                return "Tie game (Three Issues)";
            }

            return teamCity + " wins (Three Issues)";
        }

        protected override void UsedFields()
        {
            AddAllLabelsToUsedFields("GamesPlayedLbl");
            AddAllLabelsToUsedFields("WinsLbl");
            AddAllLabelsToUsedFields("LossesLbl");
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            var index = betList.IndexOf(this);

            if (index >= 2 && index <= betList.Count - 2)
            {
                var bet1Before = betList[index - 1];
                var bet1After = betList[index + 1];
                var bet2Before = betList[index - 2];
                var bet2After = betList[index + 2];

                if (isTie && 
                    ((GetAssociatedMatch() == null || bet1Before.GetAssociatedMatch() == null || bet1After.GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()) || 
                     !bet1Before.GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index - 1, multiplicator);
                }
                else if (bet1Before.isTie && 
                    ((bet2Before.GetAssociatedMatch() == null || bet1Before.GetAssociatedMatch() == null || GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet2Before.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()) ||
                     !bet2Before.GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index - 2, multiplicator);
                }
                else if (bet1After.isTie && 
                    ((GetAssociatedMatch() == null || bet1After.GetAssociatedMatch() == null || bet2After.GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet2After.GetAssociatedMatch()) ||
                     !bet1After.GetAssociatedMatch().Equals(bet2After.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index, multiplicator);
                }
            }
            else if (index == 0)
            {
                var bet1After = betList[index + 1];
                var bet2After = betList[index + 2];
                if (bet1After.isTie &&
                    ((GetAssociatedMatch() == null || bet1After.GetAssociatedMatch() == null || bet2After.GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet2After.GetAssociatedMatch()) ||
                     !bet1After.GetAssociatedMatch().Equals(bet2After.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index, multiplicator);
                }
            }
            else if(index == 1 || index == betList.Count - 2)
            {
                var bet1Before = betList[index - 1];
                var bet1After = betList[index + 1];
                if (isTie &&
                    ((GetAssociatedMatch() == null || bet1Before.GetAssociatedMatch() == null || bet1After.GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()) ||
                     !bet1Before.GetAssociatedMatch().Equals(bet1After.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index - 1, multiplicator);
                }
            }
            else if (index == betList.Count - 1)
            {
                var bet1Before = betList[index - 1];
                var bet2Before = betList[index - 2];
                if (bet1Before.isTie &&
                    ((bet2Before.GetAssociatedMatch() == null || bet1Before.GetAssociatedMatch() == null || GetAssociatedMatch() == null) ||
                    (!GetAssociatedMatch().Equals(bet2Before.GetAssociatedMatch()) || !GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()) ||
                     !bet2Before.GetAssociatedMatch().Equals(bet1Before.GetAssociatedMatch()))))
                {
                    betList.RemoveRange(index - 2, multiplicator);
                }
            }

            return betList;
        }

        public override void Probs()
        {
            //Initializes the probability to 0%
            prob = 0;

            //TeamBetAgainst is the Team on which this bet doesn't bet on
            var TeamBetAgainst = (AssociatedMatch.TeamList[0].City == TeamBetOn.City
                                      ? AssociatedMatch.TeamList[1]
                                      : AssociatedMatch.TeamList[0]);
            const int precision = 10;
            var randVar = 0;
            
            var arrayOfMatchList_TBO = new List<MatchOver>[precision];
            var arrayOfMatchList_TBA = new List<MatchOver>[precision];
            var probArray_TBO = new double[precision];
            var probArray_TBA = new double[precision];

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

                probArray_TBO[randVar] = (double) arrayOfMatchList_TBO[randVar].Count/TeamBetOn.GamesPlayed;

                arrayOfMatchList_TBA[randVar] = new List<MatchOver>();

                foreach (var match in TeamBetAgainst.MatchOverList)
                {
                    if (match.goalsFor == randVar)
                    {
                        arrayOfMatchList_TBA[randVar].Add(match);
                    }
                }

                probArray_TBA[randVar] = (double) arrayOfMatchList_TBA[randVar].Count/TeamBetAgainst.GamesPlayed;
                randVar++;
            }

            if (!isTie)
            {
                for (randVar = 0; randVar < precision; randVar++)
                {
                    var sumOfTBAProbs = 0.0;
                    for (var j = 0; j < randVar; j++)
                    {
                        sumOfTBAProbs += probArray_TBA[j];
                    }
                    prob += probArray_TBO[randVar]*sumOfTBAProbs;
                }
            }
            else
            {
                for (randVar = 0; randVar < precision; randVar++)
                {
                    prob += probArray_TBA[randVar]*probArray_TBO[randVar];
                }
            }

            prob *= 100;
        }
    }
}