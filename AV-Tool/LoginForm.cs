using System;
using System.Windows.Forms;

namespace AV_Tool
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordTextBox.Text == "")
            {
                return;
            }

            Hide();
            Program.Gui.urlTextBox.Text = Program.Gui.urlTextBox.Text.Replace("#", "");
            Program.Gui.totalFileSizeTextBox.Text = "";
            Program.Gui.CreateDownloadOptions();
            Downloader.Abort = false;
            Downloader.PrepareDownload();
        }

        private void LoginPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}