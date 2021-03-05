using System;

namespace wallpaper_changer
{
    class Program
    {
        static void Main(string[] args)
        {
            Debugger.Debug("Loading application...");
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
