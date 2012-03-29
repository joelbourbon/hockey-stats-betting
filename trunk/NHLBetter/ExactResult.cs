using System.Collections.Generic;

namespace NHLBetter
{

public class ExactResult:Bet
{
    public int winningTeamScore;
    public int losingTeamScore;
    
    //Default Constructor
    public ExactResult()
    {
        TypeOfBet = Bet.BetType.ExactScoreBet;
        multiplicator = 37;
    }

    //Destructor
    ~ExactResult() 
    { 
    }

    override public void Initialize()
    {
        isTie = iniString.Contains("Match nul");

        if (!isTie)
        {
            IniGetTeam();
        }

        IniGetOdd();
        IniGetPid();
        IniGetId();

        var index = iniString.IndexOf('?');
        
        if(!iniString.Contains("Tout autre pointage"))
        {
            var winningTeamScoreStr = "";
            var losingTeamScoreStr = "";

            winningTeamScoreStr += iniString[index - 2];
            losingTeamScoreStr += iniString[index + 2];

            winningTeamScore = int.Parse(winningTeamScoreStr);
            losingTeamScore = int.Parse(losingTeamScoreStr);
        }
        else
        {
            //This case means you can bet on "any other score"
            winningTeamScore = -1;
            losingTeamScore = -1;
        }
    }

    protected override void IniGetTeam()
    {
        var index = iniString.IndexOf("pour ") + "pour ".Length;
        teamCity = "";
        while(iniString[index] != '\"')
        {
            teamCity += iniString[index++];
        }
    }

    public override string ToString()
    {
        if(isTie)
        {
            return "Tie game : " + losingTeamScore + " - " + winningTeamScore;
        }

        return teamCity + " wins " + winningTeamScore + " - " + losingTeamScore;
    }

    protected override void UsedFields()
    {
        AddAllLabelsToUsedFields("GoalsPerGameLbl");
        AddAllLabelsToUsedFields("GoalsAgainstPerGameLbl");
        AddAllLabelsToUsedFields("GamesPlayedLbl");
        AddAllLabelsToUsedFields("WinsLbl");
        AddAllLabelsToUsedFields("LossesLbl");
    }

    public override List<Bet> ManageBetList(List<Bet> betList)
    {
        return betList;
    }

    public override void Probs()
    {
        //TeamBetAgainst is the Team on which this bet doesn't bet on
        var TeamBetAgainst = (AssociatedMatch.TeamList[0].City == TeamBetOn.City
                                  ? AssociatedMatch.TeamList[1]
                                  : AssociatedMatch.TeamList[0]);

        var MatchList_TBO = new List<MatchOver>();
        var MatchList_TBA = new List<MatchOver>();
        var prob_TBO = 0.0;
        var prob_TBA = 0.0;

        foreach (var match in TeamBetOn.MatchOverList)
        {
            if (match.goalsFor == winningTeamScore)
            {
                MatchList_TBO.Add(match);
            }
        }

        prob_TBO = (double)MatchList_TBO.Count / TeamBetOn.GamesPlayed;

        foreach (var match in TeamBetAgainst.MatchOverList)
        {
            if (match.goalsFor == losingTeamScore)
            {
                MatchList_TBA.Add(match);
            }
        }

        prob_TBA = (double)MatchList_TBA.Count / TeamBetAgainst.GamesPlayed;

        // Probability that the TBO score winningTeamScore goals AND that the TBA score losingTeamScore goals
        prob = prob_TBA*prob_TBO*100;
    }


}

}
