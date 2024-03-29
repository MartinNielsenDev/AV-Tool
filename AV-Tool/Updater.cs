﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AV_Tool
{
    internal abstract class Updater
    {
        private const string ThisVersion = "1.3.7";

        public static void CheckNewestVersion()
        {
            try
            {
                string serverResponse;
                using (var client = new WebClient())
                {
                    client.Headers.Add("Cache-Control", "no-cache");
                    client.Headers.Add("User-Agent", "Mozilla/5.0");
                    serverResponse = client.DownloadString("https://api.github.com/repos/MartinNielsenDev/AV-Tool/releases/latest");
                }

                var json = JsonConvert.DeserializeObject<Json>(serverResponse);

                if (json.tag_name.Split('.').Length != 3 || json.assets.Count <= 0)
                {
                    return;
                }

                var localVersion = new Versioning(ThisVersion);
                var serverVersion = new Versioning(json.tag_name);

                if (!serverVersion.IsNewerThan(localVersion))
                {
                    return;
                }

                var changeLogOnly = json.body.Split(new[] { "```" }, StringSplitOptions.None);

                if (changeLogOnly.Length != 3)
                {
                    return;
                }

                var updateForm = new UpdateNotificationForm();
                updateForm.installedVersionLabel.Text += ThisVersion;
                updateForm.newestVersionLabel.Text += json.tag_name;
                updateForm.changeLogTextBox.Text = changeLogOnly[1].Trim();
                updateForm.DownloadUrl = json.assets[0].browser_download_url;
                updateForm.DownloadSize = json.assets[0].size;
                Application.Run(updateForm);
            }
            catch
            {
                // ignored
            }
        }
    }

    public class YouTubeDlStats
    {
        [JsonProperty("version")] public string version { get; set; }
        [JsonProperty("url")] public string url { get; set; }
    }

    public class Json
    {
        [JsonProperty("tag_name")] public string tag_name { get; set; }
        [JsonProperty("body")] public string body { get; set; }
        [JsonProperty("assets")] public List<Assets> assets { get; set; }
    }

    public class Assets
    {
        [JsonProperty("name")] public string name { get; set; }
        [JsonProperty("browser_download_url")] public string browser_download_url { get; set; }
        [JsonProperty("size")] public int size { get; set; }
    }
}