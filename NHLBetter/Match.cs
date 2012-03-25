﻿using System;
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
            this.TeamList.Add(awayTeam);
            this.TeamList.Add(homeTeam);
            this.TimeOfGame = timeOfGame;
            this.IsBetOver = isBetOver;
        }

        public List<Team> TeamList = new List<Team>();
        public double TimeOfGame; //19.5 -­­> 19h30
        public bool IsBetOver;

        public bool GetIsBetOver()
        {
            return this.IsBetOver;
        }

        public Team GetAwayTeam()
        {
            return this.TeamList[(int)HomeAway.Away];
        }

        public Team GetHomeTeam()
        {
            return this.TeamList[(int)HomeAway.Home];
        }

        public override string ToString()
        {
            return this.GetHomeTeam().City + " vs " + GetAwayTeam().City + "\n";
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