# AV Tool - Video / Audio downloader and converter
[![Build status](https://ci.appveyor.com/api/projects/status/65594ghmtjavjw29?svg=true)](https://ci.appveyor.com/project/MartinNielsenDev/av-tool)
[![Latest Release](https://img.shields.io/github/release/martinnielsendev/av-tool.svg)](https://github.com/MartinNielsenDev/av-tool/releases)

![av tool](https://i.imgur.com/1LVY3BC.jpg)

## What does it do?
My AV tool makes it easier to download and convert audio or videos with an accessible interface, actual download functionality is done by youtube-dl and conversion is done through ffmpeg
Below is a list of things AV Tool can do
* Download audio and video files from a wide range of websites, not just YouTube
* Download whole playlists
* Download private videos or playlists by logging into the associated account
* Convert audio to MP3 format
* Convert video to MP4 format
* Insert multiple video links and have the AV Tool automatically go through the list and download them with your selected options

## Usage
Head to [releases](https://github.com/MartinNielsenDev/AV-Tool/releases) and download `AV-Tool.exe` and place it where you'd like your files to be downloaded to, the AV Tool places all downloads in the same directory as where AV-Tool.exe is

## Requirements
I have made sure to make it as easy as possible, everything needed is included in the executable AV-Tool.exe file, the AV Tool is packaged with the following tools:

* `youtube-dl 2019.03.18`
* `ffmpeg 4.1.1`
* `.NET Framework 4.6.1`

If AV Tool does not run, you may not have .NET Framework 4.6.1 installed, this version comes with Windows 10, but if you run on Windows 8 or earlier you can simply download and install it [here](https://www.microsoft.com/en-us/download/details.aspx?id=49981)

## Info
When you launch AV-Tool.exe it will do a whether an update is available, if there is you will be prompted and have the option to download the update.

The quality slider only works for MP3 conversion

If you encounter any bugs or inconsistencies, you can enable the Verbose logs which will show all logs instead of the relevant ones. 
