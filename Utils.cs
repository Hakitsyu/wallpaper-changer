using System;
using System.Collections.Generic;
using System.IO;

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
            foreach (string path in paths)
            {
                Debugger.Debug("Trying convert " + path);
                if (File.GetAttributes(path).HasFlag(FileAttributes.Directory)) {
                    Debugger.Debug("Reading directory " + path);

                    string[] files = Directory.GetFiles(path);
                    foreach(string file in files)
                    {
                        if (FileUtils.IsImage(file))
                            wallpapers.Add(new Wallpaper(file));
                    }
                } else
                    wallpapers.Add(new Wallpaper(path));
            }

            return wallpapers;
        }
    }
}