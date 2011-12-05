using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace HockeyStats
{
  public partial class Form1 : Form
  {
    // Listes pour conserver les données
    public SeasonStats ActualSeason = new SeasonStats();

    public Form1()
    {
      InitializeComponent();

      //Addresses.Add("http://lotoquebec.com/mise-o-jeu/fr/offre-de-paris/hockey?idact=1");
    }

    private void buttonRefresh_Click(object sender, EventArgs e)
    {
      ActualSeason.RefreshData("http://www.nhl.com/ice/teamstats.htm?season=20112012&gameType=2&viewName=summary");
    }
  }
}
