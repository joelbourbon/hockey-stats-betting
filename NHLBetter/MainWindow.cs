using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace NHLBetter
{
    public partial class Form1 : Form
    {
        private HockeyDay Today = new HockeyDay();
        private List<Control> boldControlList = new List<Control>();

        private enum FilterFactors
        {
            eHappeningProbabilityOver = 0,
            eOddsOver = 1,
            eHappeningProbabilityUnder = 2, 
            eOddsUnder = 3,
            ePID = 4,
            eTypeOfBet = 5,
            eDutyOver = 6,
            eDutyUnder = 7
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            ProgressLbl.Text = @"Clearing Lists";
            listBox1.Items.Clear();
            AvailableBetList.Items.Clear();

            Today.RefreshData();

            ProgressLbl.Text = @"Filling up lists";
            foreach (var match in Today.MatchList)
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
            ProgressLbl.Text = "";
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ReadIni();
            FilterFactor.SelectedIndex = 0;

            loadToolStripMenuItem1.Enabled = Directory.GetFiles(@"..\..\Saved Days\").Length > 0;
        }

        private static void ReadIni()
        {
            //Reads ini file
        
        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            ClearLists();
            Today = null;
        }

        private void ClearLists()
        {
            Today.ClearLists();
            if(boldControlList != null)
                boldControlList.Clear();
        }

        private void ThreeIssuesWinnerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var selectedMatch = Today.MatchList[index];

            Odd.Text  = @"";
            Pid.Text  = @"";
            Prob.Text = @"%";
            Duty.Text = @"";

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
                BestDuty.Enabled = true;
                BestProb.Enabled = true;
                BestOdd.Enabled = true;
            }
            else
            {
                ComputeProb.Enabled = false;
                AvailableBetList.Enabled = false;
                BestDuty.Enabled = false;
                BestProb.Enabled = false;
                BestOdd.Enabled = false;
            }
        }

        private void AvailableBetList_SelectedIndexChanged(object sender, EventArgs e)
        {
            var betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet) AvailableBetList.Items[betIndex == -1 ? 1 : betIndex];

            //Displays the betItem attributes
            Odd.Text = betItem.GetOdd().ToString();
            Pid.Text = betItem.GetPidString();
            Prob.Text = Math.Round(betItem.GetProb(), 3) + @"%";
            Duty.Text = Math.Round(betItem.GetOdd()*betItem.GetProb()/100, 3).ToString();

            //Sets all labels regular fontstyle
            foreach (var boldControl in boldControlList)
            {
                boldControl.Font = new Font(boldControl.Font, FontStyle.Regular);
            }

            //Sets betItem used labels bold
            foreach (var usedField in betItem.usedFields)
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
            //Default control visibility
            Unit.Text = "";
            FilterBetTypeDropDown.Visible = false;
            FilterUpDown.Visible = true;
            FilterUpDown.Minimum = 0;
            FilterUpDown.Increment = new decimal(1);

            //Specific control visibility & values
            switch ((FilterFactors)FilterFactor.SelectedIndex)
            {
                case FilterFactors.eHappeningProbabilityOver:
                case FilterFactors.eHappeningProbabilityUnder:
                    Unit.Text = @"%";
                    FilterUpDown.DecimalPlaces = 0;
                    FilterUpDown.Maximum = 100; // %
                    FilterUpDown.Value = 50;
                    break;
                case FilterFactors.eOddsOver:
                    FilterUpDown.Increment = new decimal(0.5);
                    FilterUpDown.DecimalPlaces = 2;
                    FilterUpDown.Maximum = 200; // ODD
                    FilterUpDown.Value = new decimal(1.69);
                    break;
                case FilterFactors.eOddsUnder:
                    FilterUpDown.Increment = new decimal(0.5);
                    FilterUpDown.DecimalPlaces = 2;
                    FilterUpDown.Maximum = 200; // ODD
                    FilterUpDown.Value = new decimal(1.71);
                    break;
                case FilterFactors.ePID:
                    var betIndex = AvailableBetList.SelectedIndex;
                    if(betIndex != -1)
                    {
                        var betItem = (Bet)AvailableBetList.Items[betIndex];
                        Unit.Text = "";
                        FilterUpDown.DecimalPlaces = 0;
                        FilterUpDown.Maximum = 99999;
                        FilterUpDown.Value = betItem.Pid;
                    }
                    break;
                case FilterFactors.eTypeOfBet:
                    FilterBetTypeDropDown.Visible = true;
                    FilterUpDown.Visible = false;
                    FilterBetTypeDropDown.SelectedIndex = 0;
                    FilterBetTypeDropDown_SelectedIndexChanged(sender, e);
                    break;
                case FilterFactors.eDutyOver:
                case FilterFactors.eDutyUnder:
                    FilterUpDown.DecimalPlaces = 3;
                    FilterUpDown.Maximum = 3;
                    FilterUpDown.Value = new decimal(1.000);
                    FilterUpDown.Increment = new decimal(0.01);
                    break;
                }

            FilterUpDown_ValueChanged(sender, e);
        }

        private void FilterUpDown_ValueChanged(object sender, EventArgs e)
        {
            FilterItems.Items.Clear();
            if (Today.BetList != null)
            {
                foreach (var bet in Today.BetList)
                {
                    if (bet.GetAssociatedMatch() != null)
                    {
                        switch ((FilterFactors)FilterFactor.SelectedIndex)
                        {
                            case FilterFactors.eHappeningProbabilityOver:
                                if (bet.prob >= (double)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.eOddsOver:
                                if (bet.GetOdd() >= (double)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.eHappeningProbabilityUnder:
                                if (bet.prob <= (double)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.eOddsUnder:
                                if (bet.GetOdd() <= (double)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.ePID:
                                if (bet.Pid == (int)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.eDutyOver:
                                if (bet.GetDuty() >= (int)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                            case FilterFactors.eDutyUnder:
                                if (bet.GetDuty() <= (int)FilterUpDown.Value)
                                    FilterItems.Items.Add(bet);
                                break;
                        }
                    }
                }
            }
            FilterItems_SelectedIndexChanged(sender, e);
        }

        private void ComputeProb_Click(object sender, EventArgs e)
        {
            var betIndex = AvailableBetList.SelectedIndex;
            var betItem = (Bet) AvailableBetList.Items[betIndex];

            //Recomputes the probability to help debugging
            betItem.Probs();
        }

        private void FilterItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            var betIndex = FilterItems.SelectedIndex;
            if(betIndex != -1)
                AutoSelect((Bet)FilterItems.Items[betIndex]);
        }

        private void AutoSelect(Bet betItem)
        {
            //Selects the corresponding index in the matchList
            if (Today.MatchList != null)
            {
                foreach (var match in Today.MatchList)
                {
                    if (betItem.GetAssociatedMatch() != null && betItem.GetAssociatedMatch().Equals(match))
                    {
                        listBox1.SelectedIndex = listBox1.Items.IndexOf(match.ToString());
                        break;
                    }
                }
            }

            //Selects the corresponding index in the betList
            if (Today.BetList != null)
            {
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
            if (Today.BetList != null)
            {
                foreach (var bet in Today.BetList)
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

        private void Prob_Click(object sender, EventArgs e)
        {

        }

        private void BestDuty_Click(object sender, EventArgs e)
        {
            var betItemWithBestDuty = (Bet) AvailableBetList.Items[0];
            var bestDuty = betItemWithBestDuty.GetDuty();

            foreach (var betItem in Today.BetList)
            {
                if(betItem.GetDuty() > bestDuty)
                {
                    bestDuty = betItem.GetDuty();
                    betItemWithBestDuty = betItem;
                }
            }

            AutoSelect(betItemWithBestDuty);
        }

        private void BestProb_Click(object sender, EventArgs e)
        {
            var betItemWithBestProb = (Bet)AvailableBetList.Items[0];
            var bestProb = betItemWithBestProb.GetProb();

            foreach (var betItem in Today.BetList)
            {
                if (betItem.GetProb() > bestProb)
                {
                    bestProb = betItem.GetProb();
                    betItemWithBestProb = betItem;
                }
            }

            AutoSelect(betItemWithBestProb);
        }

        private void BestOdd_Click(object sender, EventArgs e)
        {
            var betItemWithBestOdd = (Bet)AvailableBetList.Items[0];
            var bestOdd = betItemWithBestOdd.GetOdd();

            foreach (var betItem in Today.BetList)
            {
                if (betItem.GetOdd() > bestOdd)
                {
                    bestOdd = betItem.GetOdd();
                    betItemWithBestOdd = betItem;
                }
            }

            AutoSelect(betItemWithBestOdd);
        }
    }
}