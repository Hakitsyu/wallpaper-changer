using System;

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

        public static string[] FormatPaths(string[] paths)
        {
            string[] result = new string[paths.Length];
            for (int i = 0; i < paths.Length; i++)
            {
                result[i] = paths[i]
                    .Replace("{root_path}", Environment.CurrentDirectory)
                    .Replace("{config_path}", Program.ConfigPath);
            }

            return result;
        }
    }
}
