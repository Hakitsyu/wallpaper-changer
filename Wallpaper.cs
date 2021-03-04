using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

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
}
