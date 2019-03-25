using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace AV_Tool
{
    public partial class GUI : Form
    {
        public GUI()
        {
            InitializeComponent();
        }

        private void audioCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (audioCheckBox.Checked)
            {
                forceCheckBox.Text = "Force MP3 format";
                qualityTrackBar.Enabled = true;
            }
            videoCheckBox.Checked = !audioCheckBox.Checked;
        }

        private void videoCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (videoCheckBox.Checked)
            {
                forceCheckBox.Text = "Force MP4 format";
                qualityTrackBar.Enabled = false;
            }
            audioCheckBox.Checked = !videoCheckBox.Checked;
        }

        private void forceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (audioCheckBox.Checked)
            {
                qualityTrackBar.Enabled = forceCheckBox.Checked;
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            Program.loginPrompt.usernameTextBox.Text = "";
            Program.loginPrompt.passwordTextBox.Text = "";
            Program.gui.totalFileSizeTextBox.Text = "";
            Program.loginPrompt.Hide();
            Program.gui.urlTextBox.Text = Program.gui.urlTextBox.Text.Replace("#", "");
            CreateDownloadOptions();
            Downloader.abort = false;
            Downloader.PrepareDownload();
        }

        private void downloadWithLoginButton_Click(object sender, EventArgs e)
        {
            Program.loginPrompt.Show();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            foreach (Process process in Process.GetProcessesByName("youtube-dl"))
            {
                process.Kill();
            }
            foreach (Process process in Process.GetProcessesByName("ffmpeg"))
            {
                process.Kill();
            }
            Downloader.abort = true;
        }

        public void CreateDownloadOptions()
        {
            if (this.InvokeRequired)
            {
                Invoke(new Action(CreateDownloadOptions));
            }
            else
            {
                Downloader.downloadOptions = new Downloader.DownloadOptions(
                    Program.gui.urlTextBox.Lines,
                    Program.gui.forceCheckBox.Checked,
                    Program.gui.audioCheckBox.Checked,
                    Program.gui.videoCheckBox.Checked,
                    Program.gui.qualityTrackBar.Value,
                    Program.loginPrompt.usernameTextBox.Text,
                    Program.loginPrompt.passwordTextBox.Text
                    );
            }
        }

        public void AppendLog(string message, bool newLine, bool isVerbose = false)
        {
            if (logTextBox.InvokeRequired)
            {
                Invoke(new Action<string, bool, bool>(AppendLog), new object[] { message, newLine, isVerbose });
            }
            else
            {
                if (verboseLogsCheckBox.Checked && !isVerbose) return;
                else if (!verboseLogsCheckBox.Checked && isVerbose) return;

                string output = message;
                if (newLine) output += Environment.NewLine;

                logTextBox.AppendText(output);
            }
        }

        private bool IsFileSizeAdded(string fileSize)
        {
            for (int i = 0; i < Downloader.fileSizes.Count; i++)
            {
                if (Downloader.fileSizes[i].Equals(fileSize)) return true;
            }
            return false;
        }

        public void UpdateDownloadInformation(string percent, string size, string sizeUnit, string speed, string speedUnit)
        {
            if (downloadLabel.InvokeRequired)
            {
                Invoke(new Action<string, string, string, string, string>(UpdateDownloadInformation), new object[] { percent, size, sizeUnit, speed, speedUnit });
            }
            else
            {
                downloadLabel.Text = percent + "%";

                if (!IsFileSizeAdded($"{size} {sizeUnit}"))
                {
                    Downloader.fileSizes.Add($"{size} {sizeUnit}");

                    if (totalFileSizeTextBox.Text == "")
                    {
                        totalFileSizeTextBox.Text = $"{size} {sizeUnit}";
                    }
                    else
                    {
                        UpdateFileSize(totalFileSizeTextBox, size, sizeUnit);
                    }
                }
                downloadSpeedIndicatorLabel.Text = $"{speed} {speedUnit}/s";
                try { downloadProgressBar.Value = Convert.ToInt16(percent); } catch { }
            }
        }
        private void UpdateFileSize(TextBox textBox, string size, string sizeUnit)
        {
            string[] textBoxInfo = textBox.Text.Split(' ');

            if (double.TryParse(textBoxInfo[0], out double currentFileSize) &&
                double.TryParse(size, out double newFileSize))
            {
                double outputTotalFileSize = 0.0;
                if (textBoxInfo[1] == "MiB") currentFileSize *= 1000;
                if (sizeUnit == "MiB") newFileSize *= 1000;

                outputTotalFileSize = currentFileSize + newFileSize;

                if (currentFileSize + newFileSize >= 1000)
                {
                    outputTotalFileSize /= 1000;
                    sizeUnit = "MiB";
                }
                textBox.Text = $"{Math.Round(outputTotalFileSize, 2)} {sizeUnit}";
            }
        }

        public void UpdateURLs(string[] lines)
        {
            if (urlTextBox.InvokeRequired)
            {
                Invoke(new Action<string[]>(UpdateURLs), new object[] { lines });
            }
            else
            {
                Program.gui.urlTextBox.Lines = lines;
            }
        }

        public void ToggleElements(bool choice)
        {
            if (audioCheckBox.InvokeRequired)
            {
                Invoke(new Action<bool>(ToggleElements), new object[] { choice });
            }
            else
            {
                audioCheckBox.Enabled = choice;
                videoCheckBox.Enabled = choice;
                forceCheckBox.Enabled = choice;
                downloadButton.Enabled = choice;
                downloadWithLoginButton.Enabled = choice;
                urlTextBox.Enabled = choice;
                downloadLocationBrowseButton.Enabled = choice;

                if (choice)
                {
                    Program.gui.downloadProgressBar.Style = ProgressBarStyle.Blocks;
                }
                else
                {
                    Program.gui.downloadProgressBar.Style = ProgressBarStyle.Marquee;
                }
            }
        }

        public void ToggleDownloadBar(ProgressBarStyle style)
        {
            if (audioCheckBox.InvokeRequired)
            {
                Invoke(new Action<ProgressBarStyle>(ToggleDownloadBar), new object[] { style });
            }
            else
            {
                Program.gui.downloadProgressBar.Style = style;
            }
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            Downloader.SetupFiles();
        }

        private void downloadLocationBrowseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Path.Combine(Downloader.path, "downloadLocation"), folderDialog.SelectedPath);
                Downloader.downloadPath = folderDialog.SelectedPath;
                downloadLocationTextBox.Text = folderDialog.SelectedPath;
            }
        }
    }
}