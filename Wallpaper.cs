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

        public Wallpaper(string imagePath)
        {
            ImagePath = imagePath;
            Extension = Path.GetExtension(imagePath);
            FileName = Path.GetFileNameWithoutExtension(imagePath);
            FullFileName = Path.GetFileName(imagePath);
        }
    }

    class WallpaperManager
    {
        public List<Wallpaper> Wallpapers { get; }
        public int CurrentIndex { get; set; }
        public Wallpaper CurrentWallpaper { get; set; }

        [DllImport("user32.dll")]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);
    
        public WallpaperManager(List<Wallpaper> wallpapers)
        {
            Wallpapers = wallpapers;
            CurrentIndex = 0;
            CurrentWallpaper = Wallpapers[CurrentIndex];
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
