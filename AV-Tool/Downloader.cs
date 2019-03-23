using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AV_Tool
{
    class Downloader
    {
        public static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "av-tool");
        public static string downloadPath = "";
        public static bool abort = false;
        public static List<string> fileSizes = new List<string>();
        static bool isConverting = false;
        static bool isDownloading = false;
        static string currentFileName = "";

        public static void SetupFiles()
        {
            Directory.CreateDirectory(path);
            if (File.Exists(Path.Combine(path, "downloadLocation")))
            {
                downloadPath = File.ReadAllText(Path.Combine(path, "downloadLocation"));
            }
            else
            {
                downloadPath = Directory.GetCurrentDirectory();
                File.WriteAllText(Path.Combine(path, "downloadLocation"), downloadPath);
            }
            Program.gui.downloadLocationTextBox.Text = downloadPath;
            UnpackFiles();
        }
        public static void UnpackFiles()
        {
            string[] assemblyNames = { "youtube-dl.exe", "ffmpeg.exe" };

            for (int i = 0; i < assemblyNames.Length; i++)
            {
                if (File.Exists(Path.Combine(path, assemblyNames[i]))) continue;

                using (Stream input = Assembly.GetExecutingAssembly().GetManifestResourceStream("AV_Tool.Resources." + assemblyNames[i]))
                {
                    using (Stream output = File.Create(Path.Combine(path, assemblyNames[i])))
                    {
                        byte[] buffer = new byte[8192];
                        int bytesRead;

                        while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                        }
                    }
                }
            }
        }
        public static void PrepareDownload(bool recursive = false)
        {
            string url = "";
            string[] urls = Program.gui.urlTextBox.Lines;

            for (int i = 0; i < urls.Length; i++)
            {
                if(!urls[i].StartsWith("#") && urls[i] != "")
                {
                    url = Program.gui.urlTextBox.Lines[i].Trim(); ;
                    urls[i] = $"# {url}";
                    break;
                }
            }
            Program.gui.UpdateURLs(urls);

            if (url.Equals(""))
            {
                if (recursive)
                {
                    Program.gui.AppendLog("====== Finish ======", true);
                }
                else
                {
                    Program.gui.AppendLog("No URL found", true);
                }
                Program.gui.ToggleElements(true);
                return;
            }
            fileSizes.Clear();
            currentFileName = "";
            Program.gui.ToggleElements(false);
            Program.gui.AppendLog($"====== Starting download of {url} ======", true);

            Action action = 0;

            if (Program.gui.forceCheckBox.Checked)
            {
                if (Program.gui.audioCheckBox.Checked) action = Action.AudioForced;
                else if (Program.gui.videoCheckBox.Checked) action = Action.VideoForced;
            }
            else
            {
                if (Program.gui.audioCheckBox.Checked) action = Action.Audio;
                else if (Program.gui.videoCheckBox.Checked) action = Action.Video;
            }
            Download(url, action, Program.gui.qualityTrackBar.Value, Program.loginPrompt.usernameTextBox.Text, Program.loginPrompt.passwordTextBox.Text);
        }
        public static void Download(string url, Action action, int quality, string username, string password)
        {
            string argument = "";

            switch (action)
            {
                case Action.Audio:
                    argument = $"-o \"{downloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {Path.Combine(path, "ffmpeg.exe")} --extract-audio {url}";
                    Console.WriteLine(argument);
                    break;
                case Action.AudioForced:
                    argument = $"-o \"{downloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {Path.Combine(path, "ffmpeg.exe")} --audio-quality {quality} --audio-format mp3 --extract-audio {url}";
                    break;
                case Action.Video:
                    argument = $"-o \"{downloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {Path.Combine(path, "ffmpeg.exe")} {url}";
                    break;
                case Action.VideoForced:
                    argument = $"-o \"{downloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {Path.Combine(path, "ffmpeg.exe")} --recode-video mp4 {url}";
                    break;
            }
            if (username != "" && password != "")
            {
                argument = $"-u {username} -p {password} {argument}";
            }

            Process proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = Path.Combine(path, "youtube-dl.exe"),
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Arguments = argument
                }
            };
            proc.EnableRaisingEvents = true;
            proc.OutputDataReceived += HandleOutputData;
            proc.ErrorDataReceived += HandleErrorData;
            proc.Exited += HandleExit;
            proc.Start();
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
        }
        static void HandleOutputData(object sender, DataReceivedEventArgs e)
        {
            try
            {
                if (e.Data == null)
                    return;
                Program.gui.AppendLog(e.Data, true, true);
                Console.WriteLine(e.Data);
                Match match = Regex.Match(e.Data, @"(\d+).?\d*% *of *(\d+.?\d*)(KiB|MiB) at *(\d*.?\d+)?(KiB|MiB)", RegexOptions.IgnoreCase);

                if (match.Success && match.Groups.Count == 6)
                {
                    Program.gui.UpdateDownloadInformation(
                        match.Groups[1].Value,
                        match.Groups[2].Value,
                        match.Groups[3].Value,
                        match.Groups[4].Value,
                        match.Groups[5].Value);

                }
                else if (e.Data.Contains("Downloading video"))
                {
                    match = Regex.Match(e.Data, @"Downloading video (\d*) of (\d*)");

                    if (match.Success)
                    {
                        Program.gui.AppendLog($"Video {match.Groups[1].Value} of {match.Groups[2].Value}", true);
                    }
                }
                else if (Program.gui.forceCheckBox.Checked && (e.Data.Contains("[ffmpeg] Destination:") || e.Data.Contains("[ffmpeg] Converting video")))
                {
                    isConverting = true;
                    Program.gui.AppendLog("Converting file...", false);
                }
                else if (Program.gui.forceCheckBox.Checked && isConverting && e.Data.Contains("Deleting original file"))
                {
                    isConverting = false;
                    Program.gui.AppendLog(" complete", true);
                }
                else if (isDownloading && (e.Data.Contains("Merging formats into") || e.Data.Contains("[download] 100%")))
                {
                    isDownloading = false;
                    Program.gui.AppendLog(" complete", true);
                }
                else if (e.Data.Contains("has already been downloaded"))
                {
                    Program.gui.AppendLog("Already downloaded", true);
                }
                else
                {
                    match = Regex.Match(e.Data, "\\[download\\] Destination: *(.*)");

                    if (match.Success)
                    {
                        string[] split = match.Groups[1].Value.Split('.');
                        if (split.Length > 0 && !Path.GetFileName(split[0]).Equals(currentFileName))
                        {
                            isDownloading = true;
                            currentFileName = Path.GetFileName(split[0]);
                            Program.gui.AppendLog($"Downloading ({currentFileName})...", false);
                        }
                    }
                }
            }
            catch (Exception err) { Program.gui.AppendLog(err.ToString(), true); }
        }
        static void HandleErrorData(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;
            if (e.Data.Contains("WARNING: Requested formats are incompatible for merge"))
                return;
            Program.gui.AppendLog(e.Data, true);
        }
        static void HandleExit(object sender, EventArgs e)
        {
            if (abort)
            {
                Program.gui.ToggleElements(true);
                Program.gui.AppendLog("====== Aborted ======", true);
                abort = false;
            }
            else
            {
                PrepareDownload(true);
            }
        }
        public enum Action
        {
            Audio = 0,
            AudioForced = 1,
            Video = 2,
            VideoForced = 3
        }
    }
}
