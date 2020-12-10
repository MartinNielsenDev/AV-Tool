using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace AV_Tool
{
    public partial class Gui : Form
    {
        public Gui()
        {
            InitializeComponent();
        }

        private void audioRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (audioRadioButton.Checked)
            {
                forceCheckBox.Text = "Force MP3 format";
                qualityTrackBar.Enabled = true;
                subtitleCheckBox.Enabled = false;
                subtitlesComboBox.Enabled = false;
            }

            videoRadioButton.Checked = !audioRadioButton.Checked;
        }

        private void videoRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (videoRadioButton.Checked)
            {
                forceCheckBox.Text = "Force MP4 format";
                qualityTrackBar.Enabled = false;
                subtitleCheckBox.Enabled = true;
                subtitlesComboBox.Enabled = true;
            }

            audioRadioButton.Checked = !videoRadioButton.Checked;
        }

        private void forceCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (audioRadioButton.Checked)
            {
                qualityTrackBar.Enabled = forceCheckBox.Checked;
            }
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            Program.LoginPrompt.usernameTextBox.Text = "";
            Program.LoginPrompt.passwordTextBox.Text = "";
            Program.Gui.totalFileSizeTextBox.Text = "";
            Program.LoginPrompt.Hide();
            Program.Gui.urlTextBox.Text = Program.Gui.urlTextBox.Text.Replace("#", "");
            CreateDownloadOptions();
            Downloader.Abort = false;
            Downloader.PrepareDownload();
        }

        private void downloadWithLoginButton_Click(object sender, EventArgs e)
        {
            Program.LoginPrompt.Show();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            foreach (var process in Process.GetProcessesByName("youtube-dl"))
            {
                process.Kill();
            }

            foreach (var process in Process.GetProcessesByName("ffmpeg"))
            {
                process.Kill();
            }

            Downloader.Abort = true;
        }

        private void downloadLocationBrowseButton_Click(object sender, EventArgs e)
        {
            var folderDialog = new FolderBrowserDialog();

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(Path.Combine(Downloader.Path, "downloadLocation"), folderDialog.SelectedPath);
                Downloader.DownloadPath = folderDialog.SelectedPath;
                downloadLocationTextBox.Text = folderDialog.SelectedPath;
            }
        }

        public void CreateDownloadOptions()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(CreateDownloadOptions));
            }
            else
            {
                Downloader._downloadOptions = new Downloader.DownloadOptions(
                    Program.Gui.urlTextBox.Lines,
                    Program.Gui.forceCheckBox.Checked,
                    Program.Gui.audioRadioButton.Checked,
                    Program.Gui.videoRadioButton.Checked,
                    Program.Gui.subtitleCheckBox.Checked,
                    Program.Gui.subtitlesComboBox.SelectedIndex,
                    Program.Gui.qualityTrackBar.Value,
                    Program.LoginPrompt.usernameTextBox.Text,
                    Program.LoginPrompt.passwordTextBox.Text
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

                var output = message;
                if (newLine) output += Environment.NewLine;

                logTextBox.AppendText(output);
            }
        }

        private bool IsFileSizeAdded(string fileSize)
        {
            for (var i = 0; i < Downloader.FileSizes.Count; i++)
            {
                if (Downloader.FileSizes[i].Equals(fileSize)) return true;
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
                    Downloader.FileSizes.Add($"{size} {sizeUnit}");

                    if (totalFileSizeTextBox.Text == "")
                    {
                        totalFileSizeTextBox.Text = $@"{size} {sizeUnit}";
                    }
                    else
                    {
                        UpdateFileSize(totalFileSizeTextBox, size, sizeUnit);
                    }
                }

                downloadSpeedIndicatorLabel.Text = $@"{speed} {speedUnit}/s";
                try
                {
                    downloadProgressBar.Value = Convert.ToInt16(percent);
                }
                catch
                {
                }
            }
        }

        private static void UpdateFileSize(Control textBox, string size, string sizeUnit)
        {
            var textBoxInfo = textBox.Text.Split(' ');

            if (!double.TryParse(textBoxInfo[0], out var currentFileSize) ||
                !double.TryParse(size, out var newFileSize))
            {
                return;
            }

            var outputTotalFileSize = 0.0;
            if (textBoxInfo[1] == "MiB") currentFileSize *= 1000;
            if (sizeUnit == "MiB") newFileSize *= 1000;

            outputTotalFileSize = currentFileSize + newFileSize;

            if (currentFileSize + newFileSize >= 1000)
            {
                outputTotalFileSize /= 1000;
                sizeUnit = "MiB";
            }

            textBox.Text = $@"{Math.Round(outputTotalFileSize, 2)} {sizeUnit}";
        }

        public void UpdateURLs(string[] lines)
        {
            if (urlTextBox.InvokeRequired)
            {
                Invoke(new Action<string[]>(UpdateURLs), new object[] { lines });
            }
            else
            {
                Program.Gui.urlTextBox.Lines = lines;
            }
        }

        public void ToggleElements(bool choice)
        {
            if (audioRadioButton.InvokeRequired)
            {
                Invoke(new Action<bool>(ToggleElements), new object[] { choice });
            }
            else
            {
                audioRadioButton.Enabled = choice;
                videoRadioButton.Enabled = choice;
                forceCheckBox.Enabled = choice;
                downloadButton.Enabled = choice;
                downloadWithLoginButton.Enabled = choice;
                urlTextBox.Enabled = choice;
                downloadLocationBrowseButton.Enabled = choice;

                Program.Gui.downloadProgressBar.Style = choice ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
            }
        }

        public void ToggleDownloadBar(ProgressBarStyle style)
        {
            if (audioRadioButton.InvokeRequired)
            {
                Invoke(new Action<ProgressBarStyle>(ToggleDownloadBar), new object[] { style });
            }
            else
            {
                Program.Gui.downloadProgressBar.Style = style;
            }
        }

        private void GUI_Shown(object sender, EventArgs e)
        {
            subtitlesComboBox.SelectedIndex = 27;
            Downloader.SetupDirectory();
            downloadLocationTextBox.Text = Downloader.DownloadPath;

            new Thread(() =>
            {
                if (Downloader.VerifyFiles())
                {
                    Downloader.LockFiles();
                }
            }).Start();
        }
    }
}