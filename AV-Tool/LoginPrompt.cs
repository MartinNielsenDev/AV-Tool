using System;
using System.Windows.Forms;

namespace AV_Tool
{
    public partial class LoginPrompt : Form
    {
        public LoginPrompt()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void downloadButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text != "" && passwordTextBox.Text != "")
            {
                Hide();
                Program.gui.urlTextBox.Text = Program.gui.urlTextBox.Text.Replace("#", "");
                Program.gui.totalFileSizeTextBox.Text = "";
                Downloader.abort = false;
                Downloader.PrepareDownload();
            }
        }

        private void LoginPrompt_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
