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
        public string downloadUrl = String.Empty;
        public int downloadSize = 0;
        private int downloadProgress = 0;
        private double previousPercentage = 0;
        private WebClient webClient = new WebClient();

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

            if (!downloadUrl.Equals(String.Empty))
            {
                changeLogTextBox.Text = "";
                AppendChangeLog("Starting download... size " + FileSuffix(downloadSize));
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
            double bytesDownloaded = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesDownloaded / totalBytes;

            downloadProgress++;

            if (Math.Truncate(percentage * 100) != previousPercentage)
            {
                previousPercentage = Math.Truncate(percentage * 100);
                AppendChangeLog("Download " + Math.Truncate(percentage * 100) + "%");
            }
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
                webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(downloadUrl), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"av-tool\AV-Tool.exe"));
            }
            catch { }
        }
        public void OpenUpdate()
        {
            string argument = "/C choice /C Y /N /D Y /T 4 & Del /F /Q \"{0}\" & choice /C Y /N /D Y /T 2 & Move /Y \"{1}\" \"{2}\" & Start \"\" /D \"{3}\" \"{4}\" {5}";
            string tempPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"av-tool\AV-Tool.exe");
            string currentPath = Application.ExecutablePath;

            ProcessStartInfo Info = new ProcessStartInfo();
            Info.Arguments = String.Format(argument, currentPath, tempPath, currentPath, Path.GetDirectoryName(currentPath), Path.GetFileName(currentPath), "");
            Info.WindowStyle = ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            Info.FileName = "cmd.exe";
            Process.Start(Info);
            Environment.Exit(0);
        }

        private string FileSuffix(int fileSize)
        {
            if ((double)fileSize / 1024 / 1024 > 0)
            {
                return Math.Round((double)fileSize / 1024 / 1024, 2) + " MB";
            }
            else if ((double)fileSize / 1024 > 0)
            {
                return Math.Round((double)fileSize / 1024, 2) + " KB";
            }
            return fileSize + " B";
        }
    }
}
