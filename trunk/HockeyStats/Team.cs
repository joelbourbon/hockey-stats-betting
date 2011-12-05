using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HockeyStats
{
  public class Team
  {
    public string City = "";
    public string Abbreviation = "";
    public uint   GamesPlayed { get; private set; }
    public uint   Wins { get; private set; }
    public uint   Losses { get; private set; }
    public uint   OverTimeLosses { get; private set; }
    public uint   Points { get; private set; }
    public float  PointPercentage { get; private set; }
    public float  GoalsPerGame { get; private set; }
    public float  GoalsAgainstPerGame { get; private set; }
    public float  FiveOnFiveForAgainstRatio { get; private set; }
    public float  PowerPlayPercentage { get; private set; }
    public float  PenaltyKillPercentage { get; private set; }
    public float  ShotsPerGame { get; private set; }
    public float  ShotsAgainstPerGame { get; private set; }
    public float  WinningPercentageScoringFirst { get; private set; }
    public float  WinningPercentageTrailingFist { get; private set; }
    public float  WinningPercentageLeadingAfterOne { get; private set; }
    public float  WinningPercentageLeadingAfterTwo { get; private set; }
    public float  OutshootingPercentage { get; private set; }
    public float  OutShotPercentage { get; private set; }
    public float  FaceOffWinningPercentage { get; private set; }
            
    // Constructeur
    public Team(){}

    /// <summary>
    /// Fill Team Stats with Raw string for a team
    /// </summary>
    /// <param name="statStr"></param>
    public void FillTeamStats(string statStr)
    {
      FillTeamStats(RawDataToSeparatedStats(statStr));
    }

    /// <summary>
    /// Fill team stats with Separated Stats (String --> Values)
    /// </summary>
    /// <param name="separatedStats"></param>
    public void FillTeamStats(List<string> separatedStats)
    {
      // Lit chacune des stats et les met dans le bon champ de la classe
      GamesPlayed                       =  uint.Parse(separatedStats[0]);
      Wins                              =  uint.Parse(separatedStats[1]);
      Losses                            =  uint.Parse(separatedStats[2]);
      OverTimeLosses                    =  uint.Parse(separatedStats[3]);
      Points                            =  uint.Parse(separatedStats[4]);
      PointPercentage                   = float.Parse(separatedStats[5]);
      GoalsPerGame                      = float.Parse(separatedStats[6]);
      GoalsAgainstPerGame               = float.Parse(separatedStats[7]);
      FiveOnFiveForAgainstRatio         = float.Parse(separatedStats[8]);
      PowerPlayPercentage               = float.Parse(separatedStats[9]);
      PenaltyKillPercentage             = float.Parse(separatedStats[10]);
      ShotsPerGame                      = float.Parse(separatedStats[11]);
      ShotsAgainstPerGame               = float.Parse(separatedStats[12]);
      WinningPercentageScoringFirst     = float.Parse(separatedStats[13]);
      WinningPercentageTrailingFist     = float.Parse(separatedStats[14]);
      WinningPercentageLeadingAfterOne  = float.Parse(separatedStats[15]);
      WinningPercentageLeadingAfterTwo  = float.Parse(separatedStats[16]);
      OutshootingPercentage             = float.Parse(separatedStats[17]);
      OutShotPercentage                 = float.Parse(separatedStats[18]);
      FaceOffWinningPercentage          = float.Parse(separatedStats[19]);
    }

    /// <summary>
    /// Take the Raw Data string and put it into a list of Separated Stats
    /// </summary>
    /// <param name="statStr"></param>
    /// <returns></returns>
    private List<string> RawDataToSeparatedStats(string statStr)
    {
      // Liste de statistiques
      List<string> separatedStats;

      // Pour les string de Abbreviation et City
      int startIndex = statStr.IndexOf("rel=") + "rel=".Length;
      int endIndex = statStr.IndexOf("</A>");

      // Crée les séparateurs de string
      var separators = new string[3];
      separators[0] = "</TD>\r\n<TD>";
      separators[1] = "</TD>\r\n<TD style=\"TEXT-ALIGN: center\">";
      separators[2] = "</TD></TR>";

      // On prend chacune des string de chaque côté de '<'
      Abbreviation = statStr.Substring(startIndex, endIndex - startIndex).Split('>')[0];
      City = statStr.Substring(startIndex, endIndex - startIndex).Split('>')[1];

      // Ajuste la string à ce qu'on a besoin, et remplace les virgules par des points
      statStr = statStr.Remove(0, endIndex + "</A>".Length).Replace('.', ',');

      // Crée un string[] en fonction des séparateurs et la storer dans une liste
      separatedStats = statStr.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
      separatedStats.Insert(0, Abbreviation);
      separatedStats.Insert(1, City);

      return separatedStats;
    }

    public override string ToString()
    {
      var teamString = City + " (" + 
                       Abbreviation + ") : " + 
                       GamesPlayed.ToString() + ", " +
                       Wins.ToString() + ", " +
                       Losses.ToString() + ", " +
                       OverTimeLosses.ToString() + ", " +
                       Points.ToString() + ", " +
                       PointPercentage.ToString() + ", " +
                       GoalsPerGame.ToString() + ", " +
                       GoalsAgainstPerGame.ToString() + ", " +
                       FiveOnFiveForAgainstRatio.ToString() + ", " +
                       PowerPlayPercentage.ToString() + ", " +
                       PenaltyKillPercentage.ToString() + ", " +
                       ShotsPerGame.ToString() + ", " +
                       ShotsAgainstPerGame.ToString() + ", " +
                       WinningPercentageScoringFirst.ToString() + ", " +
                       WinningPercentageTrailingFist.ToString() + ", " +
                       WinningPercentageLeadingAfterOne.ToString() + ", " +
                       WinningPercentageLeadingAfterTwo.ToString() + ", " +
                       OutshootingPercentage.ToString() + ", " +
                       OutShotPercentage.ToString() + ", " +
                       FaceOffWinningPercentage.ToString() + ", ";
      return teamString;
    }
  }
}
