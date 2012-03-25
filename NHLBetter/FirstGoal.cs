using System.Collections.Generic;

namespace NHLBetter
{
    public class FirstGoal : Bet
    {
        public string specialPlayer;
        public string teamAbb;

        public FirstGoal()
        {
            TypeOfBet = Bet.BetType.FirstGoalBet;
            multiplicator = 20;
        }

        ~FirstGoal()
        {
        }

        override public void Initialize()
        {
            var index = iniString.IndexOf("descActivite=\"") + "descActivite=\"".Length;
            while (iniString[index] != '\"')
            {
                specialPlayer += iniString[index++];
            }

            IniGetOdd();
            IniGetPid();
            IniGetId();

            if(iniString.Contains("Tout autre joueur"))
            {
                specialPlayer = "anyOtherPlayer";
            }
            else
            {
                index = iniString.IndexOf("-") + 1;
                while (iniString[index] != '\"')
                {
                    teamAbb += iniString[index++];
                }
                index = specialPlayer.IndexOf("-");
                specialPlayer = specialPlayer.Substring(0, index);
            }
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}