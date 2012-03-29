using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace NHLBetter
{
    public partial class Form1 : Form
    {
        private HockeyDay Today = new HockeyDay();
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

            foreach (Match match in Today.MatchList)
            {
                listBox1.Items.Add(match.ToString());
            }
            if (Today.MatchList != null)
            {
                listBox1.SetSelected(0, true);
                listBox1_SelectedIndexChanged(sender, e);
                saveToolStripMenuItem1.Enabled = true;
            }
            else
            {
                listBox1.Items.Clear();
            }

            FilterFactor_SelectedIndexChanged(sender, e);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ReadIni();
            FilterFactor.SelectedIndex = 0;

            loadToolStripMenuItem1.Enabled = Directory.GetFiles(@"..\..\Saved Days\").Length > 0;
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
            int index = listBox1.SelectedIndex;
            Match selectedMatch = Today.MatchList[index];

            Odd.Text = "";
            Pid.Text = "";
            Prob.Text = @"%";

            string cd = Directory.GetCurrentDirectory();

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

            if (Today.BetList != null)
            {
                foreach (Bet bet in Today.BetList)
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
            int betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet) AvailableBetList.Items[betIndex];

            Odd.Text = betItem.GetOdd().ToString();
            Pid.Text = betItem.GetPidString();
            Prob.Text = Math.Round(betItem.prob, 3) + @"%";

            foreach (Control boldControl in boldControlList)
            {
                boldControl.Font = new Font(boldControl.Font, FontStyle.Regular);
            }

            foreach (string usedField in betItem.usedFields)
            {
                if (ActiveForm != null)
                {
                    var control = (Label) Controls.Find(usedField, false)[0];
                    control.Font = new Font(control.Font, FontStyle.Bold);
                    boldControlList.Add(control);
                }
            }
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
                    int betIndex = AvailableBetList.SelectedIndex;
                    var betItem = (Bet) AvailableBetList.Items[betIndex];

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

            foreach (Bet bet in Today.BetList)
            {
                if (bet.GetAssociatedMatch() != null)
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
                            if (bet.prob <= (double) FilterUpDown.Value)
                                FilterItems.Items.Add(bet);
                            break;
                        case 3:
                            if (bet.GetOdd() <= (double) FilterUpDown.Value)
                                FilterItems.Items.Add(bet);
                            break;
                        case 4:
                            if (bet.Pid == (int) FilterUpDown.Value)
                                FilterItems.Items.Add(bet);
                            break;
                    }
                }
            }
            FilterItems_SelectedIndexChanged(sender, e);
        }

        private void ComputeProb_Click(object sender, EventArgs e)
        {
            int betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet) AvailableBetList.Items[betIndex];

            //Recomputes the probability to help debugging
            betItem.Probs();
        }

        private void FilterItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            int betIndex = FilterItems.SelectedIndex;
            if (betIndex != -1)
            {
                var betItem = (Bet) FilterItems.Items[betIndex];

                //Selects the corresponding index in the matchList
                foreach (Match match in Today.MatchList)
                {
                    if (betItem.GetAssociatedMatch() != null && betItem.GetAssociatedMatch().Equals(match))
                    {
                        listBox1.SelectedIndex = listBox1.Items.IndexOf(match.ToString());
                        break;
                    }
                }

                //Selects the corresponding index in the betList
                foreach (Bet bet in Today.BetList)
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
            foreach (Bet bet in Today.BetList)
            {
                switch (FilterBetTypeDropDown.SelectedIndex)
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

        private void progressBar1_Click(object sender, EventArgs e)
        {
        }

        private void quitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (ActiveForm != null)
                ActiveForm.Close();
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var path = @"..\..\Saved Days\"+ DateTime.Now.ToString().Replace(" ", "").Replace("-", "").Replace(":", "") +".sav";
            var fs = File.Create(path);
            
            fs.Write(Encoding.ASCII.GetBytes("NhlRawData\n"), 0, "NhlRawData\n".Length);
            fs.Write(Encoding.ASCII.GetBytes(Today.nhlRawData + "\n\nMojRawData"), 0, Today.nhlRawData.Length + "\n\nmojRawData".Length);
            fs.Write(Encoding.ASCII.GetBytes(Today.mojRawData), 0, Today.mojRawData.Length);
            fs.Close();
            
            loadToolStripMenuItem1.Enabled = true;
        }

        private void loadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
                    {InitialDirectory = @"..\..\Saved Days\", AddExtension = false,
                     Filter = @"Day files (*.sav)|*.sav|All files (*.*)|*.*", FilterIndex = 0, 
                     Multiselect = false, ReadOnlyChecked = false, RestoreDirectory = false, 
                     ShowHelp = false, ShowReadOnly = false, SupportMultiDottedExtensions = false, 
                     Title = @"Select a file to load"};
            openFileDialog.ShowDialog();
            
            var fs = new FileStream(openFileDialog.InitialDirectory + openFileDialog.SafeFileName, FileMode.Open);
            
            byte[] buffer = {};
            fs.Read(buffer, 0, -1);

            var loadedStr = Encoding.ASCII.GetString(buffer);
            const int NhlBeginIndex = 0;
            var MojBeginIndex = loadedStr.IndexOf("\n\nMojRawData") + "\n\n".Length;
            var MojCount = loadedStr.Length - MojBeginIndex;
            Today.nhlRawData = loadedStr.Substring(NhlBeginIndex, MojBeginIndex);
            Today.mojRawData = loadedStr.Substring(MojBeginIndex, MojCount);

            Today.ClearLists();

            Today.GetListOfGamesToday("", true);
            Today.GetListOfBetsToday("", true);

            saveToolStripMenuItem1.Enabled = true;
        }
    }
}