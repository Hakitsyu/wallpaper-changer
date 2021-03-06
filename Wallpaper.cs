using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace wallpaper_changer
{
    class Wallpaper
    {
        public string ImagePath { get; }
        public string Extension { get; }
        public string FileName { get; }
        public string FullFileName { get; }
        public static List<String> AcceptedExtensions = new List<String> { "JPG", "GIF", "PNG", "JPE", "BMP", "WEBP" };

        public Wallpaper(string imagePath)
        {
            if (!IsFile(imagePath))
                throw new Exception(imagePath + " don't is file");
            if (!IsImage(imagePath))
                throw new Exception(imagePath + " don't is image");

            ImagePath = imagePath;
            Extension = Path.GetExtension(imagePath);
            FileName = Path.GetFileNameWithoutExtension(imagePath);
            FullFileName = Path.GetFileName(imagePath);
        }

        public static bool IsFile(string imagePath)
        {
            Debugger.Debug("Checking if is file " + imagePath);
            return !File.GetAttributes(imagePath).HasFlag(FileAttributes.Directory);
        }

        public static bool IsImage(string imagePath)
        {
            Debugger.Debug("Checking if is image " + imagePath);
            return AcceptedExtensions.Contains(Path.GetExtension(imagePath).Replace(".", "").ToUpper());
        }

        public static List<Wallpaper> ToWallpapers(string[] paths)
        {
            List<Wallpaper> wallpapers = new List<Wallpaper>();
            foreach (string path in paths)
            {
                Debugger.Debug("Trying convert " + path);
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                    Debugger.Debug("Reading directory " + path);

                    string[] files = Directory.GetFiles(path);
                    foreach(string file in files)
                    {
                        if (IsImage(file))
                            wallpapers.Add(new Wallpaper(file));
                    }
                } else
                    wallpapers.Add(new Wallpaper(path));
            }

            return wallpapers;
        }
    }

    class WallpaperManager
    {
        public List<Wallpaper> Wallpapers { get; }
        public int CurrentIndex { get; set; }
        public Wallpaper CurrentWallpaper { get; set; }
        public General GeneralConfig { get; set; }

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);
    
        public WallpaperManager(List<Wallpaper> wallpapers, General generalConfig)
        {
            Wallpapers = wallpapers;
            CurrentIndex = 0;
            CurrentWallpaper = Wallpapers[CurrentIndex];
            GeneralConfig = generalConfig;
        }

        public void Next()
        {
            int nextWallpaper = CurrentIndex >= Wallpapers.Count - 1 ? 0 : CurrentIndex + 1;
            Debugger.Debug("Moving on the next wallpaper with index " + nextWallpaper);

            CurrentIndex = nextWallpaper;
            CurrentWallpaper = Wallpapers[CurrentIndex];
            UpdateWallpaper();
        }

        public void Back()
        {
            int nextWallpaper = CurrentIndex <= 0 ? Wallpapers.Count - 1 : CurrentIndex - 1;
            Debugger.Debug("Moving on the back wallpaper with index " + nextWallpaper);

            CurrentIndex = nextWallpaper;
            CurrentWallpaper = Wallpapers[CurrentIndex];
            UpdateWallpaper();
        }

        public void UpdateWallpaper()
        {
            Debugger.Debug("Updating wallpaper to " + CurrentWallpaper.ImagePath);
            SystemParametersInfo(0x0014, 0, CurrentWallpaper.ImagePath, 0);
        }
    }
}
