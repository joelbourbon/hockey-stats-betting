using System;
using System.Collections.Generic;

namespace NHLBetter
{
    public class Match
    {
        // À mettre dans le .h
        public enum HomeAway
        {
            Home = 0,
            Away = 1
        };

        public Match()
        {
        }

        public Match(Team awayTeam, Team homeTeam, double timeOfGame, bool isBetOver) 
        {
            TeamList.Add(awayTeam);
            TeamList.Add(homeTeam);
            TimeOfGame = timeOfGame;
            IsBetOver = isBetOver;
        }

        public List<Team> TeamList = new List<Team>();
        public double TimeOfGame; //19.5 -­­> 19h30
        public bool IsBetOver;

        public bool GetIsBetOver()
        {
            return IsBetOver;
        }

        public Team GetAwayTeam()
        {
            return TeamList[(int)HomeAway.Away];
        }

        public Team GetHomeTeam()
        {
            return TeamList[(int)HomeAway.Home];
        }

        public override string ToString()
        {
            //Creates a timeOfGame string : Format = (xxhyy)
            var timeOfGame = " (" + Math.Floor(TimeOfGame) + "h" + (TimeOfGame%1 > 0 ? (TimeOfGame%1*60).ToString() : "00") + ")";
            return GetHomeTeam().City + " vs " + GetAwayTeam().City + timeOfGame +"\n";
        }

        // override object.Equals
        public override bool Equals(object obj)
        {
            var match = (Match) obj;
            if(match == null)
            {
                return false;
            }

            return match.GetAwayTeam().City == GetAwayTeam().City && 
                match.GetHomeTeam().City == GetHomeTeam().City;    
        }
    }
}