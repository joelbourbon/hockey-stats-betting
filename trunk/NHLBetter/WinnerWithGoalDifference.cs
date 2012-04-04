using System.Collections.Generic;

namespace NHLBetter
{
    class WinnerWithGoalDifference:Bet
    {
        public bool isMoreThan;
        public double goalDifference;

        public WinnerWithGoalDifference()
        {
            TypeOfBet = BetType.WinnerWithGoalDifferenceBet;
            multiplicator = 2;
        }

        ~WinnerWithGoalDifference()
        {
        }

        override public void Initialize()
        {
            base.Initialize();
            isMoreThan = iniString.Contains("+");

            var goalDifferenceStr = "";
            var index = iniString.IndexOf("but(s)") - 4; 
            while(iniString[index] != ' ')
            {
                goalDifferenceStr += iniString[index++];
            }

            goalDifference = double.Parse(goalDifferenceStr);
        }

        protected override void IniGetTeam()
        {
            teamCity = "";

            var index = iniString.IndexOf("descActivite=\"") + "descActivite=\"".Length;
            while (iniString[index] != ' ')
            {
                teamCity += iniString[index++];
            }
        }

        public override string ToString()
        {
            return teamCity + " wins by " + (isMoreThan ? " more than " : " less than ") + goalDifference + " goals ";
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }

        public override void Probs()
        {
            //Gets the opponent
            var Opponent = (AssociatedMatch.TeamList[0].City == TeamBetOn.City ? AssociatedMatch.TeamList[1] : AssociatedMatch.TeamList[0]);

            const int precision = 10;
            var randVar = 0;

            // arrayOfMatchList_TBO[i] will contain MatchOvers on which TeamBetOn scored i goals
            var arrayOfMatchList_TBO = new List<MatchOver>[precision];

            // arrayOfMatchList_TBO[i] will contain MatchOvers on which Opponent scored i goals
            var arrayOfMatchList_TBA = new List<MatchOver>[precision];
            
            // probArray_TBO[i] will contain the probability of TeamBetOn scoring i goals
            var probArray_TBO = new double[precision];

            // probArray_TBA[i] will contain the probability of Opponent scoring i goals
            var probArray_TBA = new double[precision];

            //Filling the four arrays
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

            //calculating probabilities
            for (randVar = 0; randVar < precision; randVar++)
            {
                var sumOfTBAProbs = 0.0;
                for (var j = 0; j < randVar; j++)
                {
                    //Dont try to figure this out. It works.
                    if ((isMoreThan && randVar - j > goalDifference) ||
                        (!isMoreThan && randVar - j < goalDifference))
                    {
                        sumOfTBAProbs += probArray_TBA[j];
                    }
                }
                prob += probArray_TBO[randVar] * sumOfTBAProbs * 100;
            }
        }
    }
}
