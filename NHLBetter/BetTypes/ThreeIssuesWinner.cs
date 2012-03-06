using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{

    public class ThreeIssuesWinner : Bet
    {

        //Default Constructor
        public ThreeIssuesWinner()
        {
            TypeOfBet = Bet.BetType.ThreeIssuesWinnerBet;
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

        public override string MakeBetStr()
        {
            string betStr = "";
            
            return betStr;
        }

        public override string ToString()
        {
            if(isTie)
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
    }

}