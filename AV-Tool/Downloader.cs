using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AV_Tool
{
    internal abstract class Downloader
    {
        private static string _playlistFolder = "";
        private static string _currentFileName = "";
        private static readonly List<string> _fileNames = new List<string>();

        public static DownloadOptions _downloadOptions;
        public static readonly string Path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "av-tool");
        public static string DownloadPath = "";
        public static bool Abort;
        public static readonly List<string> FileSizes = new List<string>();
        static FileStream _youtubedlLock, _ffmpegLock;
        static bool _isConverting;
        static bool _isDownloading;
        static int _downloadersActive;

        private static readonly string[] Subtitles =
        {
            "ab", "aa", "af", "sq", "am", "ar", "hy", "as", "ay", "az", "ba", "eu", "dz", "bh", "bi", "br", "bg", "my", "be", "km", "ca", "zh", "co", "hr", "cs", "da", "nl", "en", "eo", "et", "fo",
            "fj", "fi", "fr", "fy", "gl", "ka", "de", "el", "kl", "gn", "gu", "ha", "iw", "hi", "hu", "is", "in", "ia", "ie", "ik", "ga", "it", "ja", "jw", "kn", "ks", "kk", "rw", "ky", "rn", "ko",
            "ku", "lo", "la", "ln", "lt", "mk", "mg", "ms", "ml", "mt", "mi", "mr", "mo", "mn", "na", "ne", "no", "oc", "or", "fa", "pl", "pt", "pa", "qu", "ro", "ru", "sm", "sg", "sa", "sr", "st",
            "tn", "sn", "sd", "si", "ss", "sk", "sl", "so", "es", "su", "sw", "sv", "tl", "tg", "ta", "tt", "te", "th", "bo", "ti", "to", "ts", "tr", "tk", "tw", "uk", "ur", "uz", "vi", "vo", "cy",
            "wo", "xh", "ji", "yo", "zu"
        };

        public static void SetupDirectory()
        {
            Directory.CreateDirectory(Path);

            if (File.Exists(System.IO.Path.Combine(Path, "downloadLocation")))
            {
                DownloadPath = File.ReadAllText(System.IO.Path.Combine(Path, "downloadLocation"));
            }
            else
            {
                DownloadPath = Directory.GetCurrentDirectory();
                File.WriteAllText(System.IO.Path.Combine(Path, "downloadLocation"), DownloadPath);
            }
        }

        private static string CheckNewestYoutubeDlVersion()
        {
            try
            {
                var serverResponse = "";
                var thisVersion = "";
                using (var client = new WebClient())
                {
                    client.Headers.Add("User-Agent", "Mozilla/5.0");
                    serverResponse = client.DownloadString("https://api.github.com/repos/ytdl-org/youtube-dl/releases/latest");
                }

                var json = JsonConvert.DeserializeObject<Json>(serverResponse);

                if (File.Exists(System.IO.Path.Combine(Path, "youtube-dl.exe")))
                {
                    thisVersion = FileVersionInfo.GetVersionInfo(System.IO.Path.Combine(Path, "youtube-dl.exe")).FileVersion;
                }

                if (json.tag_name.Split('.').Length >= 3 && json.assets.Count > 0 && !json.tag_name.Equals(thisVersion))
                {
                    foreach (var asset in json.assets.Where(asset => asset.name.Equals("youtube-dl.exe")))
                    {
                        return asset.browser_download_url;
                    }
                }
            }
            catch
            {
                // ignored
            }

            return "";
        }

        public static bool VerifyFiles()
        {
            string[] md5LocalHash = { "", "" };
            string serverResponse;

            using (var client = new WebClient())
            {
                client.Headers.Add("Cache-Control", "no-cache");
                client.Headers.Add("User-Agent", "Mozilla/5.0");
                serverResponse = client.DownloadString("https://raw.githubusercontent.com/MartinNielsenDev/AV-Tool/master/fileHash");
            }

            var md5ServerHash = serverResponse.Split(new[] { "\n" }, StringSplitOptions.None);

            if (md5ServerHash.Length != 2)
            {
                return true;
            }

            var ytdlDownloadLink = CheckNewestYoutubeDlVersion();

            using (var md5 = MD5.Create())
            {
                if (File.Exists(System.IO.Path.Combine(Path, "ffmpeg.exe")))
                {
                    using (var stream = File.OpenRead(System.IO.Path.Combine(Path, "ffmpeg.exe")))
                    {
                        md5LocalHash[1] = Convert.ToBase64String(md5.ComputeHash(stream));
                    }
                }
            }

            if (!ytdlDownloadLink.Equals("") || !md5ServerHash[1].Equals(md5LocalHash[1]))
            {
                Program.Gui.ToggleElements(false);
                Program.Gui.AppendLog($"Downloading required files, this will only take a moment... {Environment.NewLine}", true);
            }

            if (!ytdlDownloadLink.Equals(""))
            {
                _downloadersActive++;
                DownloadUpdate(ytdlDownloadLink, "youtube-dl.exe");
            }

            if (md5ServerHash[1].Equals(md5LocalHash[1]))
            {
                return md5ServerHash[0].Equals(md5LocalHash[0]) && md5ServerHash[1].Equals(md5LocalHash[1]);
            }

            _downloadersActive++;
            DownloadUpdate("https://github.com/MartinNielsenDev/AV-Tool/raw/master/AV-Tool/Resources/ffmpeg.exe", "ffmpeg.exe");
            return md5ServerHash[0].Equals(md5LocalHash[0]) && md5ServerHash[1].Equals(md5LocalHash[1]);
        }

        private static void DownloadUpdate(string downloadUrl, string fileName)
        {
            try
            {
                using (var webClient = new WebClient())
                {
                    webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
                    webClient.DownloadFileAsync(new Uri(downloadUrl), System.IO.Path.Combine(Path, fileName));
                }
            }
            catch
            {
                // ignored
            }
        }

        public static void LockFiles()
        {
            _youtubedlLock = new FileStream(System.IO.Path.Combine(Path, "youtube-dl.exe"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            _ffmpegLock = new FileStream(System.IO.Path.Combine(Path, "ffmpeg.exe"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        private static void MoveDownloadedFile()
        {
            try
            {
                if (_playlistFolder == "")
                {
                    return;
                }

                Program.Gui.AppendLog($"Moving {_fileNames.Count} files to {_playlistFolder}...", false);

                foreach (var fileName in _fileNames)
                {
                    var fileSearch = Directory.GetFiles(DownloadPath, $"*{fileName}*");

                    if (fileSearch.Length > 0 && !File.Exists(System.IO.Path.Combine(DownloadPath, _playlistFolder, System.IO.Path.GetFileName(fileSearch[0]))))
                    {
                        File.Move(fileSearch[0], System.IO.Path.Combine(DownloadPath, _playlistFolder, System.IO.Path.GetFileName(fileSearch[0])));
                    }
                }

                Program.Gui.AppendLog(" complete", true);
            }
            catch
            {
                // ignored
            }
        }

        public static void PrepareDownload(bool recursive = false)
        {
            var url = "";
            var urls = _downloadOptions.Lines;

            for (var i = 0; i < urls.Length; i++)
            {
                if (urls[i].StartsWith("#") || urls[i] == "")
                {
                    continue;
                }

                url = urls[i].Trim();
                urls[i] = $"# {url}";
                break;
            }

            Program.Gui.UpdateURLs(urls);

            if (url.Equals(""))
            {
                Program.Gui.AppendLog(recursive ? "====== Finish ======" : "No URL found", true);
                Program.Gui.ToggleElements(true);
                return;
            }

            FileSizes.Clear();
            _fileNames.Clear();
            _currentFileName = "";
            _playlistFolder = "";

            Program.Gui.ToggleElements(false);
            Program.Gui.AppendLog($"====== Starting download of {url} ======", true);

            Action action = 0;

            if (_downloadOptions.Force)
            {
                if (_downloadOptions.Audio) action = Action.AudioForced;
                else if (_downloadOptions.Video) action = Action.VideoForced;
            }
            else
            {
                if (_downloadOptions.Audio) action = Action.Audio;
                else if (_downloadOptions.Video) action = Action.Video;
            }

            Download(url, action, _downloadOptions.Subtitle, _downloadOptions.SubtitleIndex, _downloadOptions.Quality, _downloadOptions.Username, _downloadOptions.Password);
        }

        public static void Download(string url, Action action, bool subtitle, int subtitleIndex, int quality, string username, string password)
        {
            var argument = "";

            switch (action)
            {
                case Action.Audio:
                    argument = $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} --extract-audio {url}";
                    break;
                case Action.AudioForced:
                    argument =
                        $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} --audio-quality {quality} --audio-format mp3 --extract-audio {url}";
                    break;
                case Action.Video:
                    if (subtitle)
                    {
                        argument =
                            $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --write-sub --sub-lang {Subtitles[subtitleIndex]} --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} {url}";
                    }
                    else
                    {
                        argument = $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} {url}";
                    }

                    break;
                case Action.VideoForced:
                    if (subtitle)
                    {
                        argument =
                            $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --write-sub --sub-lang {Subtitles[subtitleIndex]} --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} --recode-video mp4 {url}";
                    }
                    else
                    {
                        argument = $"-o \"{DownloadPath}\\%(title)s.%(ext)s\" --ignore-errors --prefer-ffmpeg --ffmpeg-location {System.IO.Path.Combine(Path, "ffmpeg.exe")} --recode-video mp4 {url}";
                    }

                    break;
            }

            if (username != "" && password != "")
            {
                argument = $"-u {username} -p {password} {argument}";
            }

            if (!File.Exists(System.IO.Path.Combine(Path, "youtube-dl.exe")))
            {
                Program.Gui.AppendLog("ERROR: youtube-dl could not be found, please restart AV Tool", true);
                Program.Gui.ToggleElements(true);
                return;
            }

            if (!File.Exists(System.IO.Path.Combine(Path, "ffmpeg.exe")))
            {
                Program.Gui.AppendLog("ERROR: ffmpeg could not be found, please restart AV Tool", true);
                Program.Gui.ToggleElements(true);
                return;
            }

            var proc = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = System.IO.Path.Combine(Path, "youtube-dl.exe"),
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
                Program.Gui.AppendLog(e.Data, true, true);
                Console.WriteLine(e.Data);
                var match = Regex.Match(e.Data, @"(\d+).?\d*% *of *(\d+.?\d*)(KiB|MiB) at *(\d*.?\d+)?(KiB|MiB)", RegexOptions.IgnoreCase);

                if (match.Success && match.Groups.Count == 6)
                {
                    Program.Gui.UpdateDownloadInformation(
                        match.Groups[1].Value,
                        match.Groups[2].Value,
                        match.Groups[3].Value,
                        match.Groups[4].Value,
                        match.Groups[5].Value);
                }
                else if (e.Data.Contains("Downloading playlist"))
                {
                    match = Regex.Match(e.Data, @"Downloading playlist: (.*)");

                    if (!match.Success)
                    {
                        return;
                    }

                    foreach (var m in match.Groups[1].Value)
                    {
                        if (Array.IndexOf(System.IO.Path.GetInvalidFileNameChars(), m) < 0)
                        {
                            _playlistFolder += m;
                        }
                    }

                    Directory.CreateDirectory(System.IO.Path.Combine(DownloadPath, _playlistFolder));
                }
                else if (e.Data.Contains("Downloading video"))
                {
                    match = Regex.Match(e.Data, @"Downloading video (\d*) of (\d*)");

                    if (match.Success)
                    {
                        Program.Gui.AppendLog($"Video {match.Groups[1].Value} of {match.Groups[2].Value}", true);
                    }
                }
                else if (Program.Gui.forceCheckBox.Checked && (e.Data.Contains("[ffmpeg] Destination:") || e.Data.Contains("[ffmpeg] Converting video")))
                {
                    _isConverting = true;
                    Program.Gui.AppendLog("Converting file...", false);
                }
                else if (Program.Gui.forceCheckBox.Checked && _isConverting && e.Data.Contains("Deleting original file"))
                {
                    _isConverting = false;
                    Program.Gui.AppendLog(" complete", true);
                }
                else if (_isDownloading && (e.Data.Contains("Merging formats into") || e.Data.Contains("[download] 100%")))
                {
                    _isDownloading = false;
                    Program.Gui.AppendLog(" complete", true);
                }
                else if (e.Data.Contains("has already been downloaded"))
                {
                    Program.Gui.AppendLog("Already downloaded", true);
                }
                else
                {
                    match = Regex.Match(e.Data, "\\[download\\] Destination: *(.*)");

                    if (match.Success)
                    {
                        var fileName = System.IO.Path.GetFileNameWithoutExtension(match.Groups[1].Value);

                        if (_downloadOptions.Video)
                        {
                            fileName = new Regex("\\.f[0-9]{3,3}").Replace(fileName, "", 1);
                        }

                        if (!System.IO.Path.GetFileName(fileName).Equals(_currentFileName))
                        {
                            _isDownloading = true;
                            _currentFileName = fileName;
                            _fileNames.Add(fileName);
                            Program.Gui.AppendLog($"Downloading ({fileName})...", false);
                            Program.Gui.ToggleDownloadBar(ProgressBarStyle.Blocks);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Program.Gui.AppendLog(err.ToString(), true);
            }
        }

        static void HandleErrorData(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
                return;
            if (e.Data.Contains("WARNING: Requested formats are incompatible for merge"))
                return;
            Program.Gui.AppendLog(e.Data, true);
        }

        static void HandleExit(object sender, EventArgs e)
        {
            if (Abort)
            {
                Program.Gui.ToggleElements(true);
                Program.Gui.AppendLog("====== Aborted ======", true);
                Abort = false;
            }
            else
            {
                MoveDownloadedFile();
                PrepareDownload(true);
            }
        }

        private static void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _downloadersActive--;

            if (_downloadersActive == 0)
            {
                if (e.Error == null)
                {
                    LockFiles();
                    Program.Gui.AppendLog("==== Ready ====", true);
                    Program.Gui.ToggleElements(true);
                }
                else
                {
                    Program.Gui.AppendLog("FATAL ERROR: Could not download required files", true);
                    Program.Gui.AppendLog("You must download youtube-dl.exe and ffmpeg.exe and place them inside the folder", true);
                    Program.Gui.AppendLog(Path, true);
                    Program.Gui.AppendLog("youtube-dl: https://youtube-dl.org/", true);
                    Program.Gui.AppendLog("ffmpeg: https://www.ffmpeg.org/", true);
                    Program.Gui.ToggleElements(true);
                }
            }
        }

        public enum Action
        {
            Audio = 0,
            AudioForced = 1,
            Video = 2,
            VideoForced = 3
        }

        public class DownloadOptions
        {
            public readonly string[] Lines;
            public readonly bool Force;
            public readonly bool Audio;
            public readonly bool Video;
            public readonly bool Subtitle;
            public readonly int SubtitleIndex;
            public readonly int Quality;
            public readonly string Username;
            public readonly string Password;

            public DownloadOptions(string[] lines, bool force, bool audio, bool video, bool subtitle, int subtitleIndex, int quality, string username, string password)
            {
                Lines = lines;
                Force = force;
                Audio = audio;
                Video = video;
                Subtitle = subtitle;
                SubtitleIndex = subtitleIndex;
                Quality = quality;
                Username = username;
                Password = password;
            }
        }
    }
}