using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net;
using System.Windows.Forms;

namespace AV_Tool
{
    public partial class UpdateNotificationForm : Form
    {
        public string DownloadUrl = string.Empty;
        public int DownloadSize = 0;

        private int _downloadProgress;
        private double _previousPercentage;
        private readonly WebClient _webClient = new WebClient();

        public UpdateNotificationForm()
        {
            InitializeComponent();
        }

        private void UpdateNotificationForm_Shown(object sender, EventArgs e)
        {
            SystemSounds.Asterisk.Play();
            updateButton.Focus();
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            updateButton.Enabled = false;
            laterButton.Enabled = false;

            if (!DownloadUrl.Equals(String.Empty))
            {
                changeLogTextBox.Text = "";
                AppendChangeLog("Starting download... size " + FileSuffix(DownloadSize));
                DownloadUpdate();
            }
        }

        private void laterButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void UpdateNotificationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            var bytesDownloaded = double.Parse(e.BytesReceived.ToString());
            var totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            var percentage = bytesDownloaded / totalBytes;

            _downloadProgress++;

            if (Math.Truncate(percentage * 100) == _previousPercentage)
            {
                return;
            }

            _previousPercentage = Math.Truncate(percentage * 100);
            AppendChangeLog("Download " + Math.Truncate(percentage * 100) + "%");
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (!IsDisposed)
            {
                AppendChangeLog("Download completed... restarting");
                OpenUpdate();
            }
        }

        private void AppendChangeLog(string message)
        {
            changeLogTextBox.AppendText(message + Environment.NewLine);
        }

        private void DownloadUpdate()
        {
            try
            {
                _webClient.DownloadProgressChanged += webClient_DownloadProgressChanged;
                _webClient.DownloadFileCompleted += webClient_DownloadFileCompleted;
                _webClient.DownloadFileAsync(new Uri(DownloadUrl), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"av-tool\AV-Tool.exe"));
            }
            catch
            {
                // ignored
            }
        }

        private static void OpenUpdate()
        {
            const string argument = "/C choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";
            var tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"av-tool\AV-Tool.exe");
            var currentPath = Application.ExecutablePath;

            var info = new ProcessStartInfo
            {
                Arguments = string.Format(argument, currentPath, tempPath, currentPath, Path.GetDirectoryName(currentPath), Path.GetFileName(currentPath), ""),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };
            Process.Start(info);
            Environment.Exit(0);
        }

        private string FileSuffix(int fileSize)
        {
            if ((double)fileSize / 1024 / 1024 > 0)
            {
                return Math.Round((double)fileSize / 1024 / 1024, 2) + " MB";
            }

            if ((double)fileSize / 1024 > 0)
            {
                return Math.Round((double)fileSize / 1024, 2) + " KB";
            }

            return fileSize + " B";
        }
    }
}