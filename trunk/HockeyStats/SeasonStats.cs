using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Windows.Forms;

namespace HockeyStats
{
  public class SeasonStats : BindingSource
  {
    public uint         Year;
    public DateTime     DernierUpdate;
    public List<Team>   Teams         = new List<Team>();
    public string       AddresseNhl;

    private List<String>          _nhlData;
    private string                _rawData;
    private mshtml.IHTMLDocument2 _htmlDoc;

    public SeasonStats()
    {   }

    public void RefreshData(string addresseNhl)
    {
      AddresseNhl = addresseNhl;

      // Détruit l'ancienne liste de Teams
      Teams.Clear();

      // Cherche les codes source de chacune des adresses, et les mets dans une liste de code source (RawData)
      RetrieveSourceCode();

      // Convertis la liste de RawData en list de HtmlDocs
      ConvertSourceCodeToHtml();

      // Parse the Team Infos in the RawData
      _nhlData = GetNHLData();

      // Pour chacune des équipes trouvées dans les HtmlDocs
      foreach (string statStr in _nhlData)
      {
        // Création d'une équipe
        var newTeam = new Team();

        // On remplit les stats de l'équipe
        newTeam.FillTeamStats(statStr);

        // On ajoute l'équipe à la liste
        Teams.Add(newTeam);
      }
    }

    // Retrieve the pure source code from the Team Stats NHL Webpage
    private void RetrieveSourceCode()
    {
      var wc = new WebClient();

      // Mets les source code de chacune des adresses dans la liste de RawData
      _rawData = Encoding.ASCII.GetString(wc.DownloadData(AddresseNhl));
    }

    // Convert the Source Code to an HTML Document
    private void ConvertSourceCodeToHtml()
    {
      var ms = new mshtml.HTMLDocument();
      var htmlDoc = (mshtml.IHTMLDocument2)ms;
      htmlDoc.write(_rawData);
      _htmlDoc = htmlDoc;
    }

    private List<string> GetNHLData()
    {
      string htmlStr = _htmlDoc.body.innerHTML;
      var statStr = new List<string>();

      // Conserve seulement le tableau recherché
      htmlStr = htmlStr.Substring(htmlStr.LastIndexOf("<TBODY>"),
      htmlStr.LastIndexOf("</TBODY>") - htmlStr.LastIndexOf("<TBODY>"));

      while (htmlStr.Contains("</TR>"))
      {
        int startIndex = htmlStr.IndexOf("<TR>");
        int endIndex = htmlStr.IndexOf("</TR>") + 5;

        statStr.Add(htmlStr.Substring(startIndex, endIndex - startIndex));
        htmlStr = htmlStr.Remove(startIndex, endIndex - startIndex);
      }

      return statStr;
    }

    private class SeasonYear
    {
      private readonly uint _startingYear;
      private readonly uint _endingYear;

      public SeasonYear(uint startingYear, uint endingYear)
      {
        _endingYear   = endingYear;
        _startingYear = startingYear;
      }

      public string toNhlStringFormat()
      {
        string nhlSeasonFormat = _startingYear.ToString() + _endingYear.ToString();
        return nhlSeasonFormat;
      }
    }
  }
}
