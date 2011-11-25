namespace HockeyStats
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.dataGridView1 = new System.Windows.Forms.DataGridView();
      this.gamesPlayedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.winsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.lossesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.overTimeLossesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.pointsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.pointPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.goalsPerGameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.goalsAgainstPerGameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.powerPlayPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.penaltyKillPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.shotsPerGameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.shotsAgainstPerGameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.winningPercentageScoringFirstDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.winningPercentageTrailingFistDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.outshootingPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.outShotPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.faceOffWinningPercentageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.teamBindingSource = new System.Windows.Forms.BindingSource(this.components);
      this.buttonRefresh = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.teamBindingSource)).BeginInit();
      this.SuspendLayout();
      // 
      // dataGridView1
      // 
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AllowUserToOrderColumns = true;
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.gamesPlayedDataGridViewTextBoxColumn,
            this.winsDataGridViewTextBoxColumn,
            this.lossesDataGridViewTextBoxColumn,
            this.overTimeLossesDataGridViewTextBoxColumn,
            this.pointsDataGridViewTextBoxColumn,
            this.pointPercentageDataGridViewTextBoxColumn,
            this.goalsPerGameDataGridViewTextBoxColumn,
            this.goalsAgainstPerGameDataGridViewTextBoxColumn,
            this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn,
            this.powerPlayPercentageDataGridViewTextBoxColumn,
            this.penaltyKillPercentageDataGridViewTextBoxColumn,
            this.shotsPerGameDataGridViewTextBoxColumn,
            this.shotsAgainstPerGameDataGridViewTextBoxColumn,
            this.winningPercentageScoringFirstDataGridViewTextBoxColumn,
            this.winningPercentageTrailingFistDataGridViewTextBoxColumn,
            this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn,
            this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn,
            this.outshootingPercentageDataGridViewTextBoxColumn,
            this.outShotPercentageDataGridViewTextBoxColumn,
            this.faceOffWinningPercentageDataGridViewTextBoxColumn});
      this.dataGridView1.DataSource = this.teamBindingSource;
      this.dataGridView1.Location = new System.Drawing.Point(12, 38);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.ReadOnly = true;
      this.dataGridView1.Size = new System.Drawing.Size(928, 282);
      this.dataGridView1.TabIndex = 0;
      // 
      // gamesPlayedDataGridViewTextBoxColumn
      // 
      this.gamesPlayedDataGridViewTextBoxColumn.DataPropertyName = "GamesPlayed";
      this.gamesPlayedDataGridViewTextBoxColumn.HeaderText = "GamesPlayed";
      this.gamesPlayedDataGridViewTextBoxColumn.Name = "gamesPlayedDataGridViewTextBoxColumn";
      this.gamesPlayedDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // winsDataGridViewTextBoxColumn
      // 
      this.winsDataGridViewTextBoxColumn.DataPropertyName = "Wins";
      this.winsDataGridViewTextBoxColumn.HeaderText = "Wins";
      this.winsDataGridViewTextBoxColumn.Name = "winsDataGridViewTextBoxColumn";
      this.winsDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // lossesDataGridViewTextBoxColumn
      // 
      this.lossesDataGridViewTextBoxColumn.DataPropertyName = "Losses";
      this.lossesDataGridViewTextBoxColumn.HeaderText = "Losses";
      this.lossesDataGridViewTextBoxColumn.Name = "lossesDataGridViewTextBoxColumn";
      this.lossesDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // overTimeLossesDataGridViewTextBoxColumn
      // 
      this.overTimeLossesDataGridViewTextBoxColumn.DataPropertyName = "OverTimeLosses";
      this.overTimeLossesDataGridViewTextBoxColumn.HeaderText = "OverTimeLosses";
      this.overTimeLossesDataGridViewTextBoxColumn.Name = "overTimeLossesDataGridViewTextBoxColumn";
      this.overTimeLossesDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // pointsDataGridViewTextBoxColumn
      // 
      this.pointsDataGridViewTextBoxColumn.DataPropertyName = "Points";
      this.pointsDataGridViewTextBoxColumn.HeaderText = "Points";
      this.pointsDataGridViewTextBoxColumn.Name = "pointsDataGridViewTextBoxColumn";
      this.pointsDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // pointPercentageDataGridViewTextBoxColumn
      // 
      this.pointPercentageDataGridViewTextBoxColumn.DataPropertyName = "PointPercentage";
      this.pointPercentageDataGridViewTextBoxColumn.HeaderText = "PointPercentage";
      this.pointPercentageDataGridViewTextBoxColumn.Name = "pointPercentageDataGridViewTextBoxColumn";
      this.pointPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // goalsPerGameDataGridViewTextBoxColumn
      // 
      this.goalsPerGameDataGridViewTextBoxColumn.DataPropertyName = "GoalsPerGame";
      this.goalsPerGameDataGridViewTextBoxColumn.HeaderText = "GoalsPerGame";
      this.goalsPerGameDataGridViewTextBoxColumn.Name = "goalsPerGameDataGridViewTextBoxColumn";
      this.goalsPerGameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // goalsAgainstPerGameDataGridViewTextBoxColumn
      // 
      this.goalsAgainstPerGameDataGridViewTextBoxColumn.DataPropertyName = "GoalsAgainstPerGame";
      this.goalsAgainstPerGameDataGridViewTextBoxColumn.HeaderText = "GoalsAgainstPerGame";
      this.goalsAgainstPerGameDataGridViewTextBoxColumn.Name = "goalsAgainstPerGameDataGridViewTextBoxColumn";
      this.goalsAgainstPerGameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn
      // 
      this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn.DataPropertyName = "FiveOnFiveForAgainstRatio";
      this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn.HeaderText = "FiveOnFiveForAgainstRatio";
      this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn.Name = "fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn";
      this.fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // powerPlayPercentageDataGridViewTextBoxColumn
      // 
      this.powerPlayPercentageDataGridViewTextBoxColumn.DataPropertyName = "PowerPlayPercentage";
      this.powerPlayPercentageDataGridViewTextBoxColumn.HeaderText = "PowerPlayPercentage";
      this.powerPlayPercentageDataGridViewTextBoxColumn.Name = "powerPlayPercentageDataGridViewTextBoxColumn";
      this.powerPlayPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // penaltyKillPercentageDataGridViewTextBoxColumn
      // 
      this.penaltyKillPercentageDataGridViewTextBoxColumn.DataPropertyName = "PenaltyKillPercentage";
      this.penaltyKillPercentageDataGridViewTextBoxColumn.HeaderText = "PenaltyKillPercentage";
      this.penaltyKillPercentageDataGridViewTextBoxColumn.Name = "penaltyKillPercentageDataGridViewTextBoxColumn";
      this.penaltyKillPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // shotsPerGameDataGridViewTextBoxColumn
      // 
      this.shotsPerGameDataGridViewTextBoxColumn.DataPropertyName = "ShotsPerGame";
      this.shotsPerGameDataGridViewTextBoxColumn.HeaderText = "ShotsPerGame";
      this.shotsPerGameDataGridViewTextBoxColumn.Name = "shotsPerGameDataGridViewTextBoxColumn";
      this.shotsPerGameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // shotsAgainstPerGameDataGridViewTextBoxColumn
      // 
      this.shotsAgainstPerGameDataGridViewTextBoxColumn.DataPropertyName = "ShotsAgainstPerGame";
      this.shotsAgainstPerGameDataGridViewTextBoxColumn.HeaderText = "ShotsAgainstPerGame";
      this.shotsAgainstPerGameDataGridViewTextBoxColumn.Name = "shotsAgainstPerGameDataGridViewTextBoxColumn";
      this.shotsAgainstPerGameDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // winningPercentageScoringFirstDataGridViewTextBoxColumn
      // 
      this.winningPercentageScoringFirstDataGridViewTextBoxColumn.DataPropertyName = "WinningPercentageScoringFirst";
      this.winningPercentageScoringFirstDataGridViewTextBoxColumn.HeaderText = "WinningPercentageScoringFirst";
      this.winningPercentageScoringFirstDataGridViewTextBoxColumn.Name = "winningPercentageScoringFirstDataGridViewTextBoxColumn";
      this.winningPercentageScoringFirstDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // winningPercentageTrailingFistDataGridViewTextBoxColumn
      // 
      this.winningPercentageTrailingFistDataGridViewTextBoxColumn.DataPropertyName = "WinningPercentageTrailingFist";
      this.winningPercentageTrailingFistDataGridViewTextBoxColumn.HeaderText = "WinningPercentageTrailingFist";
      this.winningPercentageTrailingFistDataGridViewTextBoxColumn.Name = "winningPercentageTrailingFistDataGridViewTextBoxColumn";
      this.winningPercentageTrailingFistDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // winningPercentageLeadingAfterOneDataGridViewTextBoxColumn
      // 
      this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn.DataPropertyName = "WinningPercentageLeadingAfterOne";
      this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn.HeaderText = "WinningPercentageLeadingAfterOne";
      this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn.Name = "winningPercentageLeadingAfterOneDataGridViewTextBoxColumn";
      this.winningPercentageLeadingAfterOneDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn
      // 
      this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn.DataPropertyName = "WinningPercentageLeadingAfterTwo";
      this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn.HeaderText = "WinningPercentageLeadingAfterTwo";
      this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn.Name = "winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn";
      this.winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // outshootingPercentageDataGridViewTextBoxColumn
      // 
      this.outshootingPercentageDataGridViewTextBoxColumn.DataPropertyName = "OutshootingPercentage";
      this.outshootingPercentageDataGridViewTextBoxColumn.HeaderText = "OutshootingPercentage";
      this.outshootingPercentageDataGridViewTextBoxColumn.Name = "outshootingPercentageDataGridViewTextBoxColumn";
      this.outshootingPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // outShotPercentageDataGridViewTextBoxColumn
      // 
      this.outShotPercentageDataGridViewTextBoxColumn.DataPropertyName = "OutShotPercentage";
      this.outShotPercentageDataGridViewTextBoxColumn.HeaderText = "OutShotPercentage";
      this.outShotPercentageDataGridViewTextBoxColumn.Name = "outShotPercentageDataGridViewTextBoxColumn";
      this.outShotPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // faceOffWinningPercentageDataGridViewTextBoxColumn
      // 
      this.faceOffWinningPercentageDataGridViewTextBoxColumn.DataPropertyName = "FaceOffWinningPercentage";
      this.faceOffWinningPercentageDataGridViewTextBoxColumn.HeaderText = "FaceOffWinningPercentage";
      this.faceOffWinningPercentageDataGridViewTextBoxColumn.Name = "faceOffWinningPercentageDataGridViewTextBoxColumn";
      this.faceOffWinningPercentageDataGridViewTextBoxColumn.ReadOnly = true;
      // 
      // teamBindingSource
      // 
      this.teamBindingSource.DataSource = typeof(HockeyStats.Team);
      // 
      // buttonRefresh
      // 
      this.buttonRefresh.Location = new System.Drawing.Point(13, 13);
      this.buttonRefresh.Name = "buttonRefresh";
      this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
      this.buttonRefresh.TabIndex = 1;
      this.buttonRefresh.Text = "Refresh";
      this.buttonRefresh.UseVisualStyleBackColor = true;
      this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1029, 576);
      this.Controls.Add(this.buttonRefresh);
      this.Controls.Add(this.dataGridView1);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.teamBindingSource)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.DataGridView dataGridView1;
    private System.Windows.Forms.BindingSource teamBindingSource;
    private System.Windows.Forms.DataGridViewTextBoxColumn gamesPlayedDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn winsDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn lossesDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn overTimeLossesDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn pointsDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn pointPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn goalsPerGameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn goalsAgainstPerGameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn fiveOnFiveForAgainstRatioDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn powerPlayPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn penaltyKillPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn shotsPerGameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn shotsAgainstPerGameDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn winningPercentageScoringFirstDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn winningPercentageTrailingFistDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn winningPercentageLeadingAfterOneDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn winningPercentageLeadingAfterTwoDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn outshootingPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn outShotPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.DataGridViewTextBoxColumn faceOffWinningPercentageDataGridViewTextBoxColumn;
    private System.Windows.Forms.Button buttonRefresh;
  }
}

