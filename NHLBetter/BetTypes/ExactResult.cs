using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats.classes
{

public class ExactResult:Bet
{
    public int winningTeamScore;
    public int losingTeamScore;
    
    //Default Constructor
    public ExactResult()
    {
        TypeOfBet = Bet.BetType.ExactScoreBet;
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
            //This means you can bet on "any other score"
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


}

}
