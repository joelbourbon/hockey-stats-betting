using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HockeyStats.classes
{
    class YesOrNo:Bet
    {
        public bool answer;
        
        public YesOrNo()
        {
            TypeOfBet = BetType.YesOrNoBet;
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

    }
}
