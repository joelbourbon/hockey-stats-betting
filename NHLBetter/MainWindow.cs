using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NHLBetter
{
    public partial class Form1 : Form
    {
        HockeyDay Today = new HockeyDay();
        private List<Bet> bets = new List<Bet>();
        private List<Control> boldControlList = new List<Control>();

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            AvailableBetList.Items.Clear();
             
            Today.RefreshData();

            foreach (var match in Today.MatchList)
            {
                listBox1.Items.Add(match.ToString());
            }
            if (Today.MatchList[0] != null)
            {
                listBox1.SetSelected(0, true);
                listBox1_SelectedIndexChanged(null, null);
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Items.Add("No games to display");
            }
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadIni();
        } 

        private void SavePreferences()
        {
            MessageBox.Show(new Form1(), @"Test");

            WriteIni();
        }

        private static void ReadIni()
        {
            //Reads ini file
        }

        private static void WriteIni()
        {
            //Writes to ini file
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            //Write to ini file
        }


        private void ThreeIssuesWinnerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;

            //Home Display Management
            pictureBox1.ImageLocation = @"C:/Users/Felix/Documents/HockeyStats/Logos/" +
                                        Today.MatchList[index].GetHomeTeam().Abbreviation + @".png";
            GamesPlayedHomeLbl.Text = Today.MatchList[index].GetHomeTeam().GamesPlayed.ToString();
            WinsHomeLbl.Text = Today.MatchList[index].GetHomeTeam().Wins.ToString();
            LossesHomeLbl.Text = Today.MatchList[index].GetHomeTeam().Losses.ToString();
            OverTimeLossesHomeLbl.Text = Today.MatchList[index].GetHomeTeam().OverTimeLosses.ToString();
            PointsHomeLbl.Text = Today.MatchList[index].GetHomeTeam().Points.ToString();
            PointPercentageHomeLbl.Text = Today.MatchList[index].GetHomeTeam().PointPercentage.ToString();
            GoalsPerGameHomeLbl.Text = Today.MatchList[index].GetHomeTeam().GoalsPerGame.ToString();
            GoalsAgainstPerGameHomeLbl.Text = Today.MatchList[index].GetHomeTeam().GoalsAgainstPerGame.ToString();
            FFFARatioHomeLbl.Text = Today.MatchList[index].GetHomeTeam().FiveOnFiveForAgainstRatio.ToString();
            PowerPlayPercentageHomeLbl.Text = Today.MatchList[index].GetHomeTeam().PowerPlayPercentage.ToString();
            PenaltyKillPercentageHomeLbl.Text = Today.MatchList[index].GetHomeTeam().PenaltyKillPercentage.ToString();
            ShotsPerGameHomeLbl.Text = Today.MatchList[index].GetHomeTeam().ShotsPerGame.ToString();
            ShotsAgainstPerGameHomeLbl.Text = Today.MatchList[index].GetHomeTeam().ShotsAgainstPerGame.ToString();
            WPSFHomeLbl.Text = Today.MatchList[index].GetHomeTeam().WinningPercentageScoringFirst.ToString();
            WPTFHomeLbl.Text = Today.MatchList[index].GetHomeTeam().WinningPercentageTrailingFist.ToString();
            WPLA1HomeLbl.Text = Today.MatchList[index].GetHomeTeam().WinningPercentageLeadingAfterOne.ToString();
            WPLA2HomeLbl.Text = Today.MatchList[index].GetHomeTeam().WinningPercentageLeadingAfterTwo.ToString();
            WPOutshootingHomeLbl.Text = Today.MatchList[index].GetHomeTeam().OutshootingPercentage.ToString();
            WPOutshotHomeLbl.Text = Today.MatchList[index].GetHomeTeam().OutShotPercentage.ToString();
            FOWPHomeLbl.Text = Today.MatchList[index].GetHomeTeam().FaceOffWinningPercentage.ToString();

            //Away Display Management    
            pictureBox2.ImageLocation = @"C:/Users/Felix/Documents/HockeyStats/Logos/" +
                                        Today.MatchList[index].GetAwayTeam().Abbreviation + @".png";
            GamesPlayedAwayLbl.Text = Today.MatchList[index].GetAwayTeam().GamesPlayed.ToString();
            WinsAwayLbl.Text = Today.MatchList[index].GetAwayTeam().Wins.ToString();
            LossesAwayLbl.Text = Today.MatchList[index].GetAwayTeam().Losses.ToString();
            OverTimeLossesAwayLbl.Text = Today.MatchList[index].GetAwayTeam().OverTimeLosses.ToString();
            PointsAwayLbl.Text = Today.MatchList[index].GetAwayTeam().Points.ToString();
            PointPercentageAwayLbl.Text = Today.MatchList[index].GetAwayTeam().PointPercentage.ToString();
            GaolsPerGameAwayLbl.Text = Today.MatchList[index].GetAwayTeam().GoalsPerGame.ToString();
            GoalsAgainstPerGameAwayLbl.Text = Today.MatchList[index].GetAwayTeam().GoalsAgainstPerGame.ToString();
            FFFARatioAwayLbl.Text = Today.MatchList[index].GetAwayTeam().FiveOnFiveForAgainstRatio.ToString();
            PowerPlayPercentageAwayLbl.Text = Today.MatchList[index].GetAwayTeam().PowerPlayPercentage.ToString();
            PenaltyKillPercentageAwayLbl.Text = Today.MatchList[index].GetAwayTeam().PenaltyKillPercentage.ToString();
            ShotsPerGameAwayLbl.Text = Today.MatchList[index].GetAwayTeam().ShotsPerGame.ToString();
            ShotsAgainstPerGameAwayLbl.Text = Today.MatchList[index].GetAwayTeam().ShotsAgainstPerGame.ToString();
            WPSFAwayLbl.Text = Today.MatchList[index].GetAwayTeam().WinningPercentageScoringFirst.ToString();
            WPTFAwayLbl.Text = Today.MatchList[index].GetAwayTeam().WinningPercentageTrailingFist.ToString();
            WPLA1AwayLbl.Text = Today.MatchList[index].GetAwayTeam().WinningPercentageLeadingAfterOne.ToString();
            WPLA2AwayLbl.Text = Today.MatchList[index].GetAwayTeam().WinningPercentageLeadingAfterTwo.ToString();
            WPOutshootingAwayLbl.Text = Today.MatchList[index].GetAwayTeam().OutshootingPercentage.ToString();
            WPOutshotAwayLbl.Text = Today.MatchList[index].GetAwayTeam().OutShotPercentage.ToString();
            FOWPAwayLbl.Text = Today.MatchList[index].GetAwayTeam().FaceOffWinningPercentage.ToString();

            //Bets Display Management
            AvailableBetList.Items.Clear();
            bets.Clear();
            for (var i = 0; i < Today.BetList.Count; i++)
            {
                if (Today.BetList[i].AssociatedMatch != null &&
                    Today.MatchList[index].TeamList[0].City == Today.BetList[i].AssociatedMatch.TeamList[0].City &&
                    Today.MatchList[index].TeamList[1].City == Today.BetList[i].AssociatedMatch.TeamList[1].City && 
                    AvailableBetList.Items.IndexOf(Today.BetList[i]) == -1) //Does not display the bet if it's already in the list
                {
                    AvailableBetList.Items.Add(Today.BetList[i]);
                }
            }

            if (AvailableBetList.Items.Count > 0)
            {
                AvailableBetList.SelectedIndex = 0;
                listBox2_SelectedIndexChanged(null, null); //Callback
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var matchIndex = listBox1.SelectedIndex;
            var betIndex = AvailableBetList.SelectedIndex;

            var match = Today.MatchList[matchIndex];
            var betItem = (Bet)AvailableBetList.Items[betIndex];

            Odd.Text = betItem.GetOdd().ToString();
            Pid.Text = betItem.GetPidString();

            foreach(var boldControl in boldControlList)
            {
                boldControl.Font = new Font(boldControl.Font, FontStyle.Regular);
            }

            foreach (var usedField in betItem.usedFields)
            {
                if(ActiveForm != null)
                {
                    var control = (Label)Controls.Find(usedField, false)[0];
                    control.Font = new Font(control.Font, FontStyle.Bold);
                    boldControlList.Add(control);        
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void AwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void HomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void PointPercentageLbl_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LossesHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinPercentageOutShot_Click(object sender, EventArgs e)
        {

        }

        private void PointsAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinPercentageOutshooting_Click(object sender, EventArgs e)
        {

        }

        private void WPLA2HomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPLA2AwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GPAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPSFHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinsHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void GAPGAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPTFHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPSFAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GPHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void PPPHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPOutshotHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void FFFARatioHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinningPercentageTrailingFirstLbl_Click(object sender, EventArgs e)
        {

        }

        private void PointPercentageAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void FaceoffWinPercentage_Click(object sender, EventArgs e)
        {

        }

        private void SPGHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPOutshootingAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GoalsAgainstPerGameLbl_Click(object sender, EventArgs e)
        {

        }

        private void PenaltyKillPercentageLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinningPercentageScoringFirstLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinsLbl_Click(object sender, EventArgs e)
        {

        }

        private void LossesLbl_Click(object sender, EventArgs e)
        {

        }

        private void FFFARatio_Click(object sender, EventArgs e)
        {

        }

        private void PointPercentageHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinsAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void SAPGAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void SPGAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPOutshootingHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void GoalsPerGameLbl_Click(object sender, EventArgs e)
        {

        }

        private void FOWPAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void SAPGHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void PowerPlayPercentageLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPOutshotAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void OTLHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPLA1HomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void LossesAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPLA1AwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GPGAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void PKPHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void PPPAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinningPercentageLeadingAfterTwoLbl_Click(object sender, EventArgs e)
        {

        }

        private void PKPAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void OTLAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void ShotsPerGameLbl_Click(object sender, EventArgs e)
        {

        }

        private void WPTFAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GAPGHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void GPGHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void PointsHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void FFFARatioAwayLbl_Click(object sender, EventArgs e)
        {

        }

        private void GamesPlayedLbl_Click(object sender, EventArgs e)
        {

        }

        private void ShotsAgainstPerGameLbl_Click(object sender, EventArgs e)
        {

        }

        private void FOWPHomeLbl_Click(object sender, EventArgs e)
        {

        }

        private void OvertimeLossesLbl_Click(object sender, EventArgs e)
        {

        }

        private void WinPercentageLeadingAfterOneLbl_Click(object sender, EventArgs e)
        {

        }

        private void OddsAndPIDs_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void WinnerWithGoalDifferenceCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}

