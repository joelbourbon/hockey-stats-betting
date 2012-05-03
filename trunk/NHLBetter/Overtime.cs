using System;
using System.Collections.Generic;

namespace NHLBetter
{

    public class Overtime : Bet
    {
        private bool endsInOT;

        //Default Constructor
        public Overtime()
        {
            TypeOfBet = Bet.BetType.Overtime;
            multiplicator = 3;
        }

        //Destructor
        ~Overtime()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            endsInOT = iniString.Contains("Oui");
        }

        protected override void IniGetTeam()
        {
            var indexBeforeTeam = iniString.IndexOf("\"") + 1;
            var indexAfterTeam = iniString.IndexOf("gabarit");
            teamCity = iniString.Substring(indexBeforeTeam, indexAfterTeam - indexBeforeTeam);
        }

        public override string ToString()
        {
            return endsInOT ? "Ends in overtime" : "Ends regularly";
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
            var match = GetAssociatedMatch();
            var betAfter = betList[index + 1];
            var matchAfter = betAfter.GetAssociatedMatch();
            var indexOfOvertimeBet = 0;

            //Gets the first index of the Overtime Bet
            foreach (var bet in betList)
            {
                if (bet.GetBetType() == BetType.Overtime)
                {
                    if (indexOfOvertimeBet == 0)
                    {
                        indexOfOvertimeBet = betList.IndexOf(bet);
                        break;
                    }
                }
            }

            //We want to make sure the index of the bet is a factor of 2, starting at the first index of the two issues winner bets
            if ((index - indexOfOvertimeBet) % multiplicator == 0)
            {
                if (match == null || matchAfter == null || !match.Equals(matchAfter))
                {
                    betList.RemoveRange(index, multiplicator);
                }
            }

            return betList;
        }

        public override void Probs()
        {
            //Initializes prob value to 0
            prob = 0;

            //Opponent is the Team on which this bet doesn't bet on
            var Opponent = (AssociatedMatch.TeamList[0].City == TeamBetOn.City ? AssociatedMatch.TeamList[1] : AssociatedMatch.TeamList[0]);

            const int precision = 10;
            var randVar = 0;

            //Will contain the opponent's games in which they will have scored randVar goals
            var arrayOfMatchList_TBO = new List<MatchOver>[precision];

            //Will contain the teamBetOn's games in which they will have scored randVar goals
            var arrayOfMatchList_TBA = new List<MatchOver>[precision];

            //Will contain the opponent's probability of scoring randVar goals
            var probArray_TBO = new double[precision];

            //Will contain the teamBetOn's probability of scoring randVar goals
            var probArray_TBA = new double[precision];
            
            //Fills arrayOfMatchLists
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

                probArray_TBA[randVar] = (double)arrayOfMatchList_TBA[randVar++].Count / Opponent.GamesPlayed;
            }

            //Probability that we don't need an overtime computation
            for (randVar = 0; randVar < precision; randVar++)
            {
                var sumOfTBAProbs = 0.0;
                for (var j = 0; j < precision; j++)
                {
                    //If the score is not the same, we dont need an OT
                    if(j != randVar) 
                        sumOfTBAProbs += probArray_TBA[j];
                }

                prob += probArray_TBO[randVar] * sumOfTBAProbs * 100;
            }

            prob = endsInOT ? 100 - prob : prob;
        }
    }
}