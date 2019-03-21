using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AV_Tool
{
    class Updater
    {
        static readonly string thisVersion = "1.0.0";

        public static bool CheckNewestVersion()
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
                        DialogResult answer = MessageBox.Show($"Found a new update! AV-Tool v{json.tag_name}\r\n\r\n\r\nWould you like to download the update?", $"AV-Tool v{thisVersion}", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (answer == DialogResult.Yes)
                        {
                            Process.Start(json.assets[0].browser_download_url);
                            return true;
                        }
                    }
                }
            }
            catch { }
            return false;
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
