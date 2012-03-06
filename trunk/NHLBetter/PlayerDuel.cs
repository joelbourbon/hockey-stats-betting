namespace NHLBetter
{
    public class PlayerDuel : Bet
    {
        public string playerName;

        public PlayerDuel()
        {
            TypeOfBet = Bet.BetType.PlayerDuelBet;
        }

        ~PlayerDuel()
        {
        }

        override public void Initialize()
        {
            isTie = iniString.Contains("Nul");
            return;
        }
    }
}