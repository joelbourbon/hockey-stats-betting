using System.Collections.Generic;

namespace NHLBetter
{
    class YesOrNo:Bet
    {
        public bool answer;
        
        public YesOrNo()
        {
            TypeOfBet = BetType.YesOrNoBet;
            multiplicator = 2;
        }
        
        ~YesOrNo()
        {
        }

        public override void Initialize()
        {
            answer = iniString.ToLower().Contains("oui");
            IniGetOdd();
            IniGetPid();
        }

        public override List<Bet> ManageBetList(List<Bet> betList)
        {
            return betList;
        }

    }
}
