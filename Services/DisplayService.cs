using System;
using System.Threading;

namespace CybersecurityChatbot.Services
{
    public class DisplayService
    {
        private static readonly string[] asciiArt = new string[]
        {
            @"  ____        _                  _   _       _   _                  ____            _     ",
            @" / ___| _   _| |__  _ __ ___  __| | | | ___ | |_| |__   ___  _ __   | __ )  __ _ ___| |__  ",
            @" \___ \| | | | '_ \| '__/ _ \/ _` | | |/ _ \| __| '_ \ / _ \| '_ \  |  _ \ / _` / __| '_ \ ",
            @"  ___) | |_| | |_) | | |  __/ (_| | | | (_) | |_| | | | (_) | | | | | |_) | (_| \__ \ | | |",
            @" |____/ \__,_|_.__/|_|  \___|\__,_| |_|\___/ \__|_| |_|\___/|_| |_| |____/ \__,_|___/_| |_|",
            ""
        };

        public void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string line in asciiArt)
            {
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.WriteLine(new string('=', 80));
        }

        public string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("What's your name? ");
            Console.ResetColor();

            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name must be at least 3 characters long.");
                Console.ResetColor();
                name = Console.ReadLine();
            }

            return name.Trim();
        }
    }
}