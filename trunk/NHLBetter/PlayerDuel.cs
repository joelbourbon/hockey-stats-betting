using System.Collections.Generic;

namespace NHLBetter
{
    public class PlayerDuel : Bet
    {
        public string playerName;

        public PlayerDuel()
        {
            TypeOfBet = Bet.BetType.PlayerDuelBet;
            multiplicator = 3;
        }

        ~PlayerDuel()
        {
        }

        override public void Initialize()
        {
            isTie = iniString.Contains("Nul");
            return;
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }
    }
}