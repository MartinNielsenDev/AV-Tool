﻿namespace AV_Tool
{
    partial class UpdateNotificationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateNotificationForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.laterButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.changeLogLabel = new System.Windows.Forms.Label();
            this.newestVersionLabel = new System.Windows.Forms.Label();
            this.installedVersionLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.changeLogTextBox = new System.Windows.Forms.TextBox();
            this.titleSubLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::AV_Tool.Properties.Resources.small_icon;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // laterButton
            // 
            this.laterButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.laterButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.laterButton.Location = new System.Drawing.Point(413, 260);
            this.laterButton.Name = "laterButton";
            this.laterButton.Size = new System.Drawing.Size(80, 30);
            this.laterButton.TabIndex = 11;
            this.laterButton.Text = "Later";
            this.laterButton.UseVisualStyleBackColor = true;
            this.laterButton.Click += new System.EventHandler(this.laterButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.updateButton.Cursor = System.Windows.Forms.Cursors.Default;
            this.updateButton.Location = new System.Drawing.Point(327, 260);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(80, 30);
            this.updateButton.TabIndex = 7;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // changeLogLabel
            // 
            this.changeLogLabel.AutoSize = true;
            this.changeLogLabel.Location = new System.Drawing.Point(12, 119);
            this.changeLogLabel.Name = "changeLogLabel";
            this.changeLogLabel.Size = new System.Drawing.Size(61, 13);
            this.changeLogLabel.TabIndex = 13;
            this.changeLogLabel.Text = "Change log";
            // 
            // newestVersionLabel
            // 
            this.newestVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newestVersionLabel.Location = new System.Drawing.Point(12, 90);
            this.newestVersionLabel.Name = "newestVersionLabel";
            this.newestVersionLabel.Size = new System.Drawing.Size(210, 15);
            this.newestVersionLabel.TabIndex = 8;
            this.newestVersionLabel.Text = "Newest version: ";
            // 
            // installedVersionLabel
            // 
            this.installedVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installedVersionLabel.Location = new System.Drawing.Point(12, 72);
            this.installedVersionLabel.Name = "installedVersionLabel";
            this.installedVersionLabel.Size = new System.Drawing.Size(210, 15);
            this.installedVersionLabel.TabIndex = 9;
            this.installedVersionLabel.Text = "Installed version: ";
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(71, 12);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(300, 18);
            this.titleLabel.TabIndex = 12;
            this.titleLabel.Text = "AV Tool update available";
            // 
            // changeLogTextBox
            // 
            this.changeLogTextBox.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changeLogTextBox.Location = new System.Drawing.Point(12, 137);
            this.changeLogTextBox.Multiline = true;
            this.changeLogTextBox.Name = "changeLogTextBox";
            this.changeLogTextBox.ReadOnly = true;
            this.changeLogTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.changeLogTextBox.Size = new System.Drawing.Size(481, 113);
            this.changeLogTextBox.TabIndex = 10;
            this.changeLogTextBox.TabStop = false;
            // 
            // titleSubLabel
            // 
            this.titleSubLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleSubLabel.Location = new System.Drawing.Point(71, 36);
            this.titleSubLabel.Name = "titleSubLabel";
            this.titleSubLabel.Size = new System.Drawing.Size(300, 15);
            this.titleSubLabel.TabIndex = 15;
            this.titleSubLabel.Text = "Would you like to download the newest version?";
            // 
            // UpdateNotificationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 301);
            this.Controls.Add(this.titleSubLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.laterButton);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.changeLogLabel);
            this.Controls.Add(this.newestVersionLabel);
            this.Controls.Add(this.installedVersionLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.changeLogTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateNotificationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New version available";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateNotificationForm_FormClosing);
            this.Shown += new System.EventHandler(this.UpdateNotificationForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button laterButton;
        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label changeLogLabel;
        public System.Windows.Forms.Label newestVersionLabel;
        public System.Windows.Forms.Label installedVersionLabel;
        private System.Windows.Forms.Label titleLabel;
        public System.Windows.Forms.TextBox changeLogTextBox;
        public System.Windows.Forms.Label titleSubLabel;
    }
}