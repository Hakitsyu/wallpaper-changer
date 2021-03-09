using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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
            if (!FileUtils.IsFile(imagePath))
                throw new Exception(imagePath + " don't is file");
            if (!FileUtils.IsImage(imagePath))
                throw new Exception(imagePath + " don't is image");

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
        public General GeneralConfig { get; set; }
        public Task Task { get; }

        [DllImport("user32.dll")]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        public WallpaperManager(List<Wallpaper> wallpapers, General generalConfig)
        {
            Wallpapers = wallpapers;
            CurrentIndex = 0;
            CurrentWallpaper = Wallpapers[CurrentIndex];
            GeneralConfig = generalConfig;
            Task = new Task(WallpaperTask);
        }

        private void WallpaperTask()
        {
            bool run = true;
            while (run)
            {
                UpdateWallpaper();
                Task.Delay(GeneralConfig.time).Wait();
                run = !isConcluded();
                if (run)
                    Next();
            }
        }

        private bool isConcluded()
        {
            return (!GeneralConfig.loop) ? CurrentIndex >= Wallpapers.Count - 1 : false;
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
