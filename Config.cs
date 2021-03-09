using System;
using System.Collections.Generic;
using System.Linq;

namespace wallpaper_changer
{
    class General
    {
        public int time { get; set; } = 10000;
        public bool loop { get; set; } = true;
    }

    class Config
    {
        public General general { get; set; } = new General();
        public string[] paths { get; set; } = new string[] {};
    }

    class ConfigFormatter
    {
        public static Dictionary<String, String> Replaces = new Dictionary<String, String> { 
            { "root_path", Environment.CurrentDirectory },
            { "config_path", Program.ConfigPath } 
        };

        public static string[] Format(string[] paths)
        {
            return paths.Select<String, String>((path) => {
                foreach (KeyValuePair<String, String> entry in Replaces)
                    path = path.Replace("{" + entry.Key + "}", entry.Value);
                return path;
            }).ToArray();
        }
    }
}
