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
        public void PrintWelcomeMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {userName}! Ask me about cybersecurity topics.");
            Console.WriteLine("You can ask about passwords, phishing, or safe browsing.");
            Console.WriteLine("Type 'exit' to end the chat.");
            Console.ResetColor();
        }

        public void PromptForQuestion()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nWhat would you like to know about? ");
            Console.ResetColor();
        }

        public void PrintNoInputMessage()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("I didn't hear anything. Try again.");
            Console.ResetColor();
        }

        public void PrintGoodbyeMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"\nGoodbye, {userName}! Stay safe online!");
            Console.ResetColor();
        }

        public void PrintResponse(string response)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + response);
            Console.ResetColor();
        }
    }
}