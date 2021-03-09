using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace wallpaper_changer
{
    class FileUtils
    {
        public static List<String> ImageExtensions = new List<String> { "JPG", "GIF", "PNG", "JPE", "BMP", "WEBP" };
        
        public static bool IsFile(string imagePath)
        {
            Debugger.Debug("Checking if is file " + imagePath);
            return !File.GetAttributes(imagePath).HasFlag(FileAttributes.Directory);
        }

        public static bool IsImage(string imagePath)
        {
            Debugger.Debug("Checking if is image " + imagePath);
            return ImageExtensions.Contains(Path.GetExtension(imagePath).Replace(".", "").ToUpper());
        }
    }

    class WallpaperUtils
    {
        public static List<Wallpaper> ToWallpapers(string[] paths)
        {
            List<Wallpaper> wallpapers = new List<Wallpaper>();
            Array.ForEach<String>(paths, (path) => {
                Debugger.Debug("Trying convert " + path);
                if (!FileUtils.IsFile(path)) {
                    Debugger.Debug("Reading directory " + path);

                    string[] files = Directory.GetFiles(path);
                    Array.ForEach<String>(files, (file) => {
                        if (FileUtils.IsImage(file))
                            wallpapers.Add(new Wallpaper(file));
                    });
                } else if (FileUtils.IsImage(path))
                    wallpapers.Add(new Wallpaper(path));
            });

            return wallpapers;
        }
    }
}