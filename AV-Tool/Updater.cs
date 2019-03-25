using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AV_Tool
{
    class Updater
    {
        static readonly string thisVersion = "1.2.3";

        public static void CheckNewestVersion()
        {
            try
            {
                string serverResponse;
                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0");
                    serverResponse = client.DownloadString("https://api.github.com/repos/MartinNielsenDev/AV-Tool/releases/latest");
                }
                Json json = JsonConvert.DeserializeObject<Json>(serverResponse);

                if (json.tag_name.Split('.').Length == 3 && json.assets.Count > 0)
                {
                    Versioning localVersion = new Versioning(thisVersion);
                    Versioning serverVersion = new Versioning(json.tag_name);

                    if (serverVersion.IsNewerThan(localVersion))
                    {
                        string[] changeLogOnly = json.body.Split(new string[] { "```" }, StringSplitOptions.None);

                        if (changeLogOnly.Length == 3)
                        {
                            UpdateNotificationForm updateForm = new UpdateNotificationForm();
                            updateForm.installedVersionLabel.Text += thisVersion;
                            updateForm.newestVersionLabel.Text += json.tag_name;
                            updateForm.changeLogTextBox.Text = changeLogOnly[1].Trim();
                            updateForm.downloadUrl = json.assets[0].browser_download_url;
                            updateForm.downloadSize = json.assets[0].size;
                            Application.Run(updateForm);
                        }
                    }
                }
            }
            catch { }
        }
        public class Json
        {
            [JsonProperty("tag_name")]
            public string tag_name { get; set; }
            [JsonProperty("body")]
            public string body { get; set; }
            [JsonProperty("assets")]
            public List<Assets> assets { get; set; }
        }
        public class Assets
        {
            [JsonProperty("browser_download_url")]
            public string browser_download_url { get; set; }
            [JsonProperty("size")]
            public int size { get; set; }
        }
    }
    class Versioning
    {
        public int major = 0;
        public int minor = 0;
        public int patch = 0;

        public Versioning(string rawVersion)
        {
            string[] versions = rawVersion.Split('.');

            if (versions.Length == 3)
            {
                int.TryParse(versions[0], out this.major);
                int.TryParse(versions[1], out this.minor);
                int.TryParse(versions[2], out this.patch);
            }
        }
        public bool IsNewerThan(Versioning version)
        {
            if (this.major > version.major)
            {
                return true;
            }
            else if (this.major == version.major)
            {
                if (this.minor > version.minor)
                {
                    return true;
                }
                else if (this.minor == version.minor && this.patch > version.patch)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
