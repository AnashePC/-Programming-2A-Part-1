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

        public void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("       CYBERSECURITY CHATBOT");
            Console.WriteLine(new string('=', 50));

            Console.WriteLine("\n");
            Console.ResetColor();

            // Draw a button with sound indicator
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Green;
            Console.WriteLine("    =====================    ");
            Console.WriteLine("    |   PRESS ENTER TO   |    ");
            Console.WriteLine("    |       START        |    ");
            Console.WriteLine("    =====================    ");
            Console.ResetColor();

            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("(You should hear a welcome sound)");
            Console.ResetColor();
        }

        public string GetUserName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("\nWhat's your name? ");
            Console.ResetColor();

            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name must be at least 3 characters long.");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("What's your name? ");
                Console.ResetColor();
                name = Console.ReadLine();
            }

            return name.Trim();
        }

        public void PrintWelcomeMessage(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {userName}! Ask me about cybersecurity topics.");
            Console.WriteLine("You can ask about passwords, phishing, malware, social media,");
            Console.WriteLine("privacy, network security, gaming security, mobile security,");
            Console.WriteLine("or cloud security. Type 'exit' to end the chat.");
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

        public void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public void PrintQuizQuestion(string question, int questionNumber)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\nQuestion {questionNumber}: {question}");
            Console.ResetColor();
        }

        public void PrintQuizOption(int optionNumber, string optionText)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{optionNumber}. {optionText}");
            Console.ResetColor();
        }

        public void PrintQuizResults(int score, int totalQuestions)
        {
            double percentage = (double)score / totalQuestions * 100;

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nQuiz Results:");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"You scored {score} out of {totalQuestions}");
            Console.ResetColor();

            if (percentage >= 60)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Well done! You've got a good understanding of these topics.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You should spend more time learning about cybersecurity!");
                Console.WriteLine("Staying safe online is important - keep studying!");
            }
            Console.ResetColor();
        }

        public void PrintExitMessage()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nChatbot exited. Stay safe online!");
            Console.ResetColor();
        }

        public void ClearScreen()
        {
            Console.Clear();
        }
    }
}