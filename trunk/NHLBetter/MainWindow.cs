using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace NHLBetter
{
    public partial class Form1 : Form
    {
        HockeyDay Today = new HockeyDay();
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

            FilterFactor_SelectedIndexChanged(sender, e);
        }

      
        private void Form1_Load(object sender, EventArgs e)
        {
            ReadIni();
            FilterFactor.SelectedIndex = 0;
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
            Today.ClearLists();
        }


        private void ThreeIssuesWinnerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var selectedMatch = Today.MatchList[index];

            Odd.Text = "";
            Pid.Text = "";
            Prob.Text = @"%";

            var cd = Directory.GetCurrentDirectory();

            //Home Display Management
            pictureBox1.ImageLocation = @"..\\..\\" + Today.MatchList[index].GetHomeTeam().LogoPath;
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
            pictureBox2.ImageLocation = @"..\\..\\" + Today.MatchList[index].GetAwayTeam().LogoPath;
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
            AvailableBetList.Enabled = true;

            if(Today.BetList != null)
            {
                foreach (var bet in Today.BetList)
                {
                    if (bet.AssociatedMatch != null &&
                        selectedMatch.TeamList[0].City == bet.AssociatedMatch.TeamList[0].City &&
                        selectedMatch.TeamList[1].City == bet.AssociatedMatch.TeamList[1].City)
                    {
                        AvailableBetList.Items.Add(bet);
                    }
                }
            }

            if (AvailableBetList.Items.Count > 0)
            {
                AvailableBetList.SelectedIndex = 0;
                AvailableBetList_SelectedIndexChanged(sender, e); //Callback
                ComputeProb.Enabled = true;
            }
            else
            {
                ComputeProb.Enabled = false;
                AvailableBetList.Enabled = false;
            }
        }

        private void AvailableBetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet)AvailableBetList.Items[betIndex];

            Odd.Text = betItem.GetOdd().ToString();
            Pid.Text = betItem.GetPidString();
            Prob.Text = Math.Round(betItem.prob, 3) + @"%";

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

        private void FilterSentence_Click(object sender, EventArgs e)
        {

        }

        private void FilterFactor_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterBetTypeDropDown.Visible = false;
            FilterUpDown.Visible = true;
            switch (FilterFactor.SelectedIndex)
            {
                case 0:
                    Unit.Text = @"%";
                    FilterUpDown.DecimalPlaces = 0;
                    FilterUpDown.Value = 50;
                    FilterUpDown.Maximum = 100; // %
                    break;
                case 1:
                    Unit.Text = "";
                    FilterUpDown.DecimalPlaces = 2;
                    FilterUpDown.Value = new decimal(1.69);
                    FilterUpDown.Maximum = 200; // ODD
                    break;
                case 2:
                    Unit.Text = @"%";
                    FilterUpDown.DecimalPlaces = 0;
                    FilterUpDown.Value = 50;
                    FilterUpDown.Maximum = 100; // %
                    break;
                case 3:
                    Unit.Text = "";
                    FilterUpDown.DecimalPlaces = 2;
                    FilterUpDown.Value = new decimal(1.71);
                    FilterUpDown.Maximum = 200; // ODD
                    break;
                case 4:
                    var betIndex = AvailableBetList.SelectedIndex;
                    var betItem = (Bet)AvailableBetList.Items[betIndex];
                    
                    Unit.Text = "";
                    FilterUpDown.DecimalPlaces = 0;
                    FilterUpDown.Value = betItem.Pid;
                    FilterUpDown.Maximum = 99999;
                    break;
                case 5:
                    FilterBetTypeDropDown.Visible = true;
                    FilterUpDown.Visible = false;
                    Unit.Text = "";
                    FilterBetTypeDropDown.SelectedIndex = 0;
                    FilterBetTypeDropDown_SelectedIndexChanged(sender, e);
                    break;
            }

            FilterUpDown_ValueChanged(sender, e);
        }

        private void FilterUpDown_ValueChanged(object sender, EventArgs e)
        {
            FilterItems.Items.Clear();

            foreach (var bet in Today.BetList)
            {
                switch (FilterFactor.SelectedIndex)
                {
                    case 0:
                        if (bet.prob >= (double) FilterUpDown.Value)
                            FilterItems.Items.Add(bet);
                        break;
                    case 1:
                        if (bet.GetOdd() >= (double) FilterUpDown.Value)
                            FilterItems.Items.Add(bet);
                        break;
                    case 2:
                        if (bet.prob <= (double)FilterUpDown.Value)
                            FilterItems.Items.Add(bet);
                        break;
                    case 3:
                        if (bet.GetOdd() <= (double)FilterUpDown.Value)
                            FilterItems.Items.Add(bet);
                        break;
                    case 4:
                        if (bet.Pid == (int) FilterUpDown.Value)
                            FilterItems.Items.Add(bet);
                        break;
                   }
            }

            FilterItems_SelectedIndexChanged(sender, e);
        }

        private void ComputeProb_Click(object sender, EventArgs e)
        {
            var betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet)AvailableBetList.Items[betIndex];
            
            //Recomputes the probability to help debugging
            betItem.Probs();
        }

        private void FilterItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var betIndex = FilterItems.SelectedIndex;
            if (betIndex != -1)
            {
                var betItem = (Bet)FilterItems.Items[betIndex];

                //Selects the corresponding index in the matchList
                foreach (var match in Today.MatchList)
                {
                    if (betItem.GetAssociatedMatch().Equals(match))
                    {
                        listBox1.SelectedIndex = listBox1.Items.IndexOf(match.ToString());
                        break;
                    }
                }

                //Selects the corresponding index in the betList
                foreach (var bet in Today.BetList)
                {
                    if (betItem == bet)
                    {
                        AvailableBetList.SelectedIndex = AvailableBetList.Items.IndexOf(bet);
                        break;
                    }
                }
            }
        }

        private void FilterBetTypeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterItems.Items.Clear();
            foreach(var bet in Today.BetList)
            {
                switch(FilterBetTypeDropDown.SelectedIndex)
                {
                    case 0:
                        if (bet.GetBetType() == Bet.BetType.ThreeIssuesWinnerBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 1:
                        if (bet.GetBetType() == Bet.BetType.TwoIssuesWinnerBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 2:
                        if (bet.GetBetType() == Bet.BetType.WinnerWithGoalDifferenceBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 3:
                        if (bet.GetBetType() == Bet.BetType.NumberOfGoalsBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 4:
                        if (bet.GetBetType() == Bet.BetType.PlayerDuelBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 5:
                        if (bet.GetBetType() == Bet.BetType.YesOrNoBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 6:
                        if (bet.GetBetType() == Bet.BetType.ShotsOnGoalBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 7:
                        if (bet.GetBetType() == Bet.BetType.MostMinutesOfPenaltyBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 8:
                        if (bet.GetBetType() == Bet.BetType.TeamWithMoreHitsBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 9:
                        if (bet.GetBetType() == Bet.BetType.FirstGoalBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 10:
                        if (bet.GetBetType() == Bet.BetType.GainMarginBet)
                            FilterItems.Items.Add(bet);
                        break;
                    case 11:
                        if (bet.GetBetType() == Bet.BetType.ExactScoreBet)
                            FilterItems.Items.Add(bet);
                        break;
                 }
            }
            FilterItems_SelectedIndexChanged(sender, e);
        }
    }
}

