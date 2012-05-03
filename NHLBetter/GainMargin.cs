using System.Collections.Generic;

namespace NHLBetter
{

    public class GainMargin : Bet
    {
        public bool isMoreThan;
        public int numberOfGoals;

        public GainMargin()
        {
            TypeOfBet = Bet.BetType.GainMarginBet;
            multiplicator = 8;
        }

        ~GainMargin()
        {
        }

        override public void Initialize()
        {
            base.Initialize();
            isMoreThan = iniString.Contains("ou plus");

            var numberOfGoalsStr = "";
            var index = iniString.IndexOf("par ") + "par ".Length;

            while(iniString[index] != ' ')
            {
                numberOfGoalsStr += iniString[index++];
            }

            numberOfGoals = int.Parse(numberOfGoalsStr);
        }

        public override string ToString()
        {
            return teamCity + " wins by " + numberOfGoals + " goals" + (isMoreThan ? " or more" : "");
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
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
            for (randVar = numberOfGoals; randVar < precision; randVar++)
            {
                for (var j = 0; j < precision - numberOfGoals; j++)
                {
                    if(isMoreThan ? randVar >= j + numberOfGoals : randVar == j + numberOfGoals)
                    {
                        prob += probArray_TBA[j] * probArray_TBO[randVar]*100;
                        if (!isMoreThan)
                            break;
                    }
                }    
            }
        }
    }
}