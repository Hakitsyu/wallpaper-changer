using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace wallpaper_changer
{
    class Program
    {
        public static string ConfigPath = Environment.CurrentDirectory + "//resources//config.json"; 

        static void Main(string[] args)
        {
            Debugger.Debug("Loading application...");
            Config config = ReadConfig();
            string[] formattedPaths = Config.FormatPaths(config.paths);
            List<Wallpaper> wallpapers = WallpaperUtils.ToWallpapers(formattedPaths); 

            WallpaperManager wallpaperManager = new WallpaperManager(wallpapers, config.general);
        }

        private static Config ReadConfig()
        {
            Debugger.Debug("Reading config file...");
            string content = File.ReadAllText(ConfigPath);

            try {
                Debugger.Debug("Trying convert to Config class...");
                return JsonSerializer.Deserialize<Config>(content);
            } catch (Exception ex) {
                throw ex;
            }
        } 
    }

    class Debugger
    {
        private static bool Active = true;
        public enum Type
        {
            Info,
            Error,
            Success
        }

        public static void Debug(Type type, string message)
        {
            if (Active)
                Console.WriteLine("[" + Enum.GetName(typeof(Type), type) + "] " + message);
        }

        public static void Debug(string message)
        {
            Debug(Type.Info, message);
        }
    }

}
