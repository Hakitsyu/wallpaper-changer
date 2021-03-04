using System;
using System.Collections.Generic;
using System.Text;

namespace wallpaper_changer
{
    class General
    {
        public int time { get; set; } = 10000;
        public bool loop { get; set; } = true;
    }

    class Config
    {
        public General general { get; set; } = null;
    }
}
