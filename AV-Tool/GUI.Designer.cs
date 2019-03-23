namespace AV_Tool
{
    partial class GUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GUI));
            this.downloadOptionsGroupBox = new System.Windows.Forms.GroupBox();
            this.forceCheckBox = new System.Windows.Forms.CheckBox();
            this.qualityLowestLabel = new System.Windows.Forms.Label();
            this.qualityHighestLabel = new System.Windows.Forms.Label();
            this.qualityLabel = new System.Windows.Forms.Label();
            this.qualityTrackBar = new System.Windows.Forms.TrackBar();
            this.videoCheckBox = new System.Windows.Forms.CheckBox();
            this.audioCheckBox = new System.Windows.Forms.CheckBox();
            this.downloadInformationGroupBox = new System.Windows.Forms.GroupBox();
            this.downloadLabel = new System.Windows.Forms.Label();
            this.downloadSpeedIndicatorLabel = new System.Windows.Forms.Label();
            this.downloadSpeedLabel = new System.Windows.Forms.Label();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.totalFileSizeLabel = new System.Windows.Forms.Label();
            this.totalFileSizeTextBox = new System.Windows.Forms.TextBox();
            this.downloadButton = new System.Windows.Forms.Button();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.downloadWithLoginButton = new System.Windows.Forms.Button();
            this.urlTextBox = new System.Windows.Forms.TextBox();
            this.abortButton = new System.Windows.Forms.Button();
            this.verboseLogsCheckBox = new System.Windows.Forms.CheckBox();
            this.downloadLocationTextBox = new System.Windows.Forms.TextBox();
            this.downloadLocationLabel = new System.Windows.Forms.Label();
            this.downloadLocationBrowseButton = new System.Windows.Forms.Button();
            this.downloadLocationFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadOptionsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).BeginInit();
            this.downloadInformationGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // downloadOptionsGroupBox
            // 
            this.downloadOptionsGroupBox.Controls.Add(this.forceCheckBox);
            this.downloadOptionsGroupBox.Controls.Add(this.qualityLowestLabel);
            this.downloadOptionsGroupBox.Controls.Add(this.qualityHighestLabel);
            this.downloadOptionsGroupBox.Controls.Add(this.qualityLabel);
            this.downloadOptionsGroupBox.Controls.Add(this.qualityTrackBar);
            this.downloadOptionsGroupBox.Controls.Add(this.videoCheckBox);
            this.downloadOptionsGroupBox.Controls.Add(this.audioCheckBox);
            this.downloadOptionsGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadOptionsGroupBox.Location = new System.Drawing.Point(12, 34);
            this.downloadOptionsGroupBox.Name = "downloadOptionsGroupBox";
            this.downloadOptionsGroupBox.Size = new System.Drawing.Size(316, 202);
            this.downloadOptionsGroupBox.TabIndex = 0;
            this.downloadOptionsGroupBox.TabStop = false;
            this.downloadOptionsGroupBox.Text = "Options";
            // 
            // forceCheckBox
            // 
            this.forceCheckBox.AutoSize = true;
            this.forceCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.forceCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.forceCheckBox.Location = new System.Drawing.Point(22, 82);
            this.forceCheckBox.Name = "forceCheckBox";
            this.forceCheckBox.Size = new System.Drawing.Size(132, 20);
            this.forceCheckBox.TabIndex = 2;
            this.forceCheckBox.Text = "Force MP3 format";
            this.forceCheckBox.UseVisualStyleBackColor = true;
            this.forceCheckBox.CheckedChanged += new System.EventHandler(this.forceCheckBox_CheckedChanged);
            // 
            // qualityLowestLabel
            // 
            this.qualityLowestLabel.AutoSize = true;
            this.qualityLowestLabel.Location = new System.Drawing.Point(235, 166);
            this.qualityLowestLabel.Name = "qualityLowestLabel";
            this.qualityLowestLabel.Size = new System.Drawing.Size(46, 15);
            this.qualityLowestLabel.TabIndex = 0;
            this.qualityLowestLabel.Text = "Lowest";
            this.qualityLowestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // qualityHighestLabel
            // 
            this.qualityHighestLabel.AutoSize = true;
            this.qualityHighestLabel.Location = new System.Drawing.Point(36, 166);
            this.qualityHighestLabel.Name = "qualityHighestLabel";
            this.qualityHighestLabel.Size = new System.Drawing.Size(49, 15);
            this.qualityHighestLabel.TabIndex = 0;
            this.qualityHighestLabel.Text = "Highest";
            this.qualityHighestLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // qualityLabel
            // 
            this.qualityLabel.AutoSize = true;
            this.qualityLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.qualityLabel.Location = new System.Drawing.Point(129, 125);
            this.qualityLabel.Name = "qualityLabel";
            this.qualityLabel.Size = new System.Drawing.Size(49, 16);
            this.qualityLabel.TabIndex = 0;
            this.qualityLabel.Text = "Quality";
            this.qualityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // qualityTrackBar
            // 
            this.qualityTrackBar.AutoSize = false;
            this.qualityTrackBar.Enabled = false;
            this.qualityTrackBar.Location = new System.Drawing.Point(45, 144);
            this.qualityTrackBar.Maximum = 9;
            this.qualityTrackBar.Name = "qualityTrackBar";
            this.qualityTrackBar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.qualityTrackBar.Size = new System.Drawing.Size(223, 26);
            this.qualityTrackBar.TabIndex = 3;
            this.qualityTrackBar.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // videoCheckBox
            // 
            this.videoCheckBox.AutoSize = true;
            this.videoCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.videoCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.videoCheckBox.Location = new System.Drawing.Point(22, 57);
            this.videoCheckBox.Name = "videoCheckBox";
            this.videoCheckBox.Size = new System.Drawing.Size(125, 20);
            this.videoCheckBox.TabIndex = 1;
            this.videoCheckBox.Text = "Download video";
            this.videoCheckBox.UseVisualStyleBackColor = true;
            this.videoCheckBox.CheckedChanged += new System.EventHandler(this.videoCheckBox_CheckedChanged);
            // 
            // audioCheckBox
            // 
            this.audioCheckBox.AutoSize = true;
            this.audioCheckBox.Checked = true;
            this.audioCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.audioCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.audioCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.audioCheckBox.Location = new System.Drawing.Point(22, 32);
            this.audioCheckBox.Name = "audioCheckBox";
            this.audioCheckBox.Size = new System.Drawing.Size(125, 20);
            this.audioCheckBox.TabIndex = 0;
            this.audioCheckBox.Text = "Download audio";
            this.audioCheckBox.UseVisualStyleBackColor = true;
            this.audioCheckBox.CheckedChanged += new System.EventHandler(this.audioCheckBox_CheckedChanged);
            // 
            // downloadInformationGroupBox
            // 
            this.downloadInformationGroupBox.Controls.Add(this.downloadLabel);
            this.downloadInformationGroupBox.Controls.Add(this.downloadSpeedIndicatorLabel);
            this.downloadInformationGroupBox.Controls.Add(this.downloadSpeedLabel);
            this.downloadInformationGroupBox.Controls.Add(this.downloadProgressBar);
            this.downloadInformationGroupBox.Controls.Add(this.totalFileSizeLabel);
            this.downloadInformationGroupBox.Controls.Add(this.totalFileSizeTextBox);
            this.downloadInformationGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadInformationGroupBox.Location = new System.Drawing.Point(334, 34);
            this.downloadInformationGroupBox.Name = "downloadInformationGroupBox";
            this.downloadInformationGroupBox.Size = new System.Drawing.Size(316, 202);
            this.downloadInformationGroupBox.TabIndex = 1;
            this.downloadInformationGroupBox.TabStop = false;
            this.downloadInformationGroupBox.Text = "Information";
            // 
            // downloadLabel
            // 
            this.downloadLabel.Location = new System.Drawing.Point(17, 174);
            this.downloadLabel.Name = "downloadLabel";
            this.downloadLabel.Size = new System.Drawing.Size(280, 15);
            this.downloadLabel.TabIndex = 0;
            this.downloadLabel.Text = "0%";
            this.downloadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // downloadSpeedIndicatorLabel
            // 
            this.downloadSpeedIndicatorLabel.Location = new System.Drawing.Point(207, 127);
            this.downloadSpeedIndicatorLabel.Name = "downloadSpeedIndicatorLabel";
            this.downloadSpeedIndicatorLabel.Size = new System.Drawing.Size(90, 15);
            this.downloadSpeedIndicatorLabel.TabIndex = 0;
            this.downloadSpeedIndicatorLabel.Text = "0.0 KiB/s";
            this.downloadSpeedIndicatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // downloadSpeedLabel
            // 
            this.downloadSpeedLabel.AutoSize = true;
            this.downloadSpeedLabel.Location = new System.Drawing.Point(17, 127);
            this.downloadSpeedLabel.Name = "downloadSpeedLabel";
            this.downloadSpeedLabel.Size = new System.Drawing.Size(106, 15);
            this.downloadSpeedLabel.TabIndex = 0;
            this.downloadSpeedLabel.Text = "Download speed: ";
            this.downloadSpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.Location = new System.Drawing.Point(19, 148);
            this.downloadProgressBar.MarqueeAnimationSpeed = 35;
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(278, 23);
            this.downloadProgressBar.TabIndex = 0;
            // 
            // totalFileSizeLabel
            // 
            this.totalFileSizeLabel.AutoSize = true;
            this.totalFileSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalFileSizeLabel.Location = new System.Drawing.Point(16, 36);
            this.totalFileSizeLabel.Name = "totalFileSizeLabel";
            this.totalFileSizeLabel.Size = new System.Drawing.Size(86, 16);
            this.totalFileSizeLabel.TabIndex = 2;
            this.totalFileSizeLabel.Text = "Total file size";
            this.totalFileSizeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // totalFileSizeTextBox
            // 
            this.totalFileSizeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.totalFileSizeTextBox.Location = new System.Drawing.Point(109, 34);
            this.totalFileSizeTextBox.Name = "totalFileSizeTextBox";
            this.totalFileSizeTextBox.ReadOnly = true;
            this.totalFileSizeTextBox.Size = new System.Drawing.Size(188, 21);
            this.totalFileSizeTextBox.TabIndex = 999;
            // 
            // downloadButton
            // 
            this.downloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadButton.FlatAppearance.BorderSize = 0;
            this.downloadButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.downloadButton.Location = new System.Drawing.Point(14, 329);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(145, 35);
            this.downloadButton.TabIndex = 2;
            this.downloadButton.Text = "Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // logTextBox
            // 
            this.logTextBox.Location = new System.Drawing.Point(14, 387);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.ReadOnly = true;
            this.logTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logTextBox.Size = new System.Drawing.Size(634, 127);
            this.logTextBox.TabIndex = 6;
            // 
            // downloadWithLoginButton
            // 
            this.downloadWithLoginButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadWithLoginButton.FlatAppearance.BorderSize = 0;
            this.downloadWithLoginButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.downloadWithLoginButton.Location = new System.Drawing.Point(165, 329);
            this.downloadWithLoginButton.Name = "downloadWithLoginButton";
            this.downloadWithLoginButton.Size = new System.Drawing.Size(145, 35);
            this.downloadWithLoginButton.TabIndex = 3;
            this.downloadWithLoginButton.Text = "Download with login";
            this.downloadWithLoginButton.UseVisualStyleBackColor = true;
            this.downloadWithLoginButton.Click += new System.EventHandler(this.downloadWithLoginButton_Click);
            // 
            // urlTextBox
            // 
            this.urlTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.urlTextBox.Location = new System.Drawing.Point(12, 242);
            this.urlTextBox.Multiline = true;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.urlTextBox.Size = new System.Drawing.Size(638, 81);
            this.urlTextBox.TabIndex = 1;
            // 
            // abortButton
            // 
            this.abortButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.abortButton.FlatAppearance.BorderSize = 0;
            this.abortButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.abortButton.Location = new System.Drawing.Point(528, 329);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(120, 35);
            this.abortButton.TabIndex = 4;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // verboseLogsCheckBox
            // 
            this.verboseLogsCheckBox.AutoSize = true;
            this.verboseLogsCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.verboseLogsCheckBox.Location = new System.Drawing.Point(15, 368);
            this.verboseLogsCheckBox.Name = "verboseLogsCheckBox";
            this.verboseLogsCheckBox.Size = new System.Drawing.Size(87, 17);
            this.verboseLogsCheckBox.TabIndex = 5;
            this.verboseLogsCheckBox.Text = "Verbose logs";
            this.verboseLogsCheckBox.UseVisualStyleBackColor = true;
            // 
            // downloadLocationTextBox
            // 
            this.downloadLocationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.downloadLocationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadLocationTextBox.Location = new System.Drawing.Point(99, 7);
            this.downloadLocationTextBox.Name = "downloadLocationTextBox";
            this.downloadLocationTextBox.ReadOnly = true;
            this.downloadLocationTextBox.Size = new System.Drawing.Size(489, 21);
            this.downloadLocationTextBox.TabIndex = 7;
            // 
            // downloadLocationLabel
            // 
            this.downloadLocationLabel.AutoSize = true;
            this.downloadLocationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadLocationLabel.Location = new System.Drawing.Point(9, 9);
            this.downloadLocationLabel.Name = "downloadLocationLabel";
            this.downloadLocationLabel.Size = new System.Drawing.Size(84, 16);
            this.downloadLocationLabel.TabIndex = 0;
            this.downloadLocationLabel.Text = "File Location";
            this.downloadLocationLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // downloadLocationBrowseButton
            // 
            this.downloadLocationBrowseButton.Location = new System.Drawing.Point(594, 6);
            this.downloadLocationBrowseButton.Name = "downloadLocationBrowseButton";
            this.downloadLocationBrowseButton.Size = new System.Drawing.Size(56, 23);
            this.downloadLocationBrowseButton.TabIndex = 0;
            this.downloadLocationBrowseButton.Text = "...";
            this.downloadLocationBrowseButton.UseVisualStyleBackColor = true;
            this.downloadLocationBrowseButton.Click += new System.EventHandler(this.downloadLocationBrowseButton_Click);
            // 
            // GUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 526);
            this.Controls.Add(this.downloadLocationBrowseButton);
            this.Controls.Add(this.downloadLocationLabel);
            this.Controls.Add(this.downloadLocationTextBox);
            this.Controls.Add(this.verboseLogsCheckBox);
            this.Controls.Add(this.abortButton);
            this.Controls.Add(this.downloadWithLoginButton);
            this.Controls.Add(this.logTextBox);
            this.Controls.Add(this.downloadInformationGroupBox);
            this.Controls.Add(this.downloadOptionsGroupBox);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.urlTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AV Tool v1.1.0";
            this.Shown += new System.EventHandler(this.GUI_Shown);
            this.downloadOptionsGroupBox.ResumeLayout(false);
            this.downloadOptionsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.qualityTrackBar)).EndInit();
            this.downloadInformationGroupBox.ResumeLayout(false);
            this.downloadInformationGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.CheckBox videoCheckBox;
        public System.Windows.Forms.CheckBox audioCheckBox;
        public System.Windows.Forms.CheckBox forceCheckBox;
        private System.Windows.Forms.GroupBox downloadOptionsGroupBox;
        private System.Windows.Forms.GroupBox downloadInformationGroupBox;
        public System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label qualityLowestLabel;
        private System.Windows.Forms.Label qualityHighestLabel;
        private System.Windows.Forms.Label qualityLabel;
        public System.Windows.Forms.TrackBar qualityTrackBar;
        public System.Windows.Forms.TextBox totalFileSizeTextBox;
        private System.Windows.Forms.Label totalFileSizeLabel;
        private System.Windows.Forms.Label downloadSpeedIndicatorLabel;
        private System.Windows.Forms.Label downloadSpeedLabel;
        public System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.Label downloadLabel;
        private System.Windows.Forms.TextBox logTextBox;
        public System.Windows.Forms.Button downloadWithLoginButton;
        public System.Windows.Forms.TextBox urlTextBox;
        public System.Windows.Forms.Button abortButton;
        public System.Windows.Forms.CheckBox verboseLogsCheckBox;
        public System.Windows.Forms.TextBox downloadLocationTextBox;
        private System.Windows.Forms.Label downloadLocationLabel;
        private System.Windows.Forms.Button downloadLocationBrowseButton;
        private System.Windows.Forms.FolderBrowserDialog downloadLocationFolderBrowserDialog;
    }
}

