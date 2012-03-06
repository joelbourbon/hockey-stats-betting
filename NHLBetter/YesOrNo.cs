namespace NHLBetter
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
