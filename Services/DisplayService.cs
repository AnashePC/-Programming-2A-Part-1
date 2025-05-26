using System;
using System.Text;
using System.Threading;

namespace CybersecurityChatbot.Services
{
    public class DisplayService
    {
        private static readonly string[] AsciiArt =
        {
            @"  ____        _                  _   _       _   _                  ____            _     ",
            @" / ___| _   _| |__  _ __ ___  __| | | | ___ | |_| |__   ___  _ __   | __ )  __ _ ___| |__  ",
            @" \___ \| | | | '_ \| '__/ _ \/ _` | | |/ _ \| __| '_ \ / _ \| '_ \  |  _ \ / _` / __| '_ \ ",
            @"  ___) | |_| | |_) | | |  __/ (_| | | | (_) | |_| | | | (_) | | | | | |_) | (_| \__ \ | | |",
            @" |____/ \__,_|_.__/|_|  \___|\__,_| |_|\___/ \__|_| |_|\___/|_| |_| |____/ \__,_|___/_| |_|",
            ""
        };

        private const int ConsoleWidth = 80;
        private readonly StringBuilder _displayBuffer = new StringBuilder();

        public void DisplayAsciiArt()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (string line in AsciiArt)
            {
                Console.WriteLine(line.PadCenter(ConsoleWidth));
                Thread.Sleep(100);
            }
            Console.ResetColor();
            PrintDivider();
        }

        public void DisplayWelcomeScreen()
        {
            ClearBuffer();
            AddToBuffer("CYBERSECURITY CHATBOT".PadCenter(ConsoleWidth), ConsoleColor.Yellow);
            AddToBuffer(new string('=', ConsoleWidth), ConsoleColor.Yellow);
            AddToBuffer("\nThis interactive chatbot can help you with:", ConsoleColor.Cyan);

            AddToBuffer("• Password security and management", ConsoleColor.White);
            AddToBuffer("• Identifying and avoiding phishing scams", ConsoleColor.White);
            AddToBuffer("• Safe internet browsing practices", ConsoleColor.White);
            AddToBuffer("• Protecting against malware and viruses", ConsoleColor.White);
            AddToBuffer("• Social media privacy and security", ConsoleColor.White);
            AddToBuffer("• Securing your personal data online", ConsoleColor.White);
            AddToBuffer("• Home network and WiFi security", ConsoleColor.White);
            AddToBuffer("• Online gaming safety", ConsoleColor.White);
            AddToBuffer("• Mobile device protection", ConsoleColor.White);
            AddToBuffer("• Cloud storage security", ConsoleColor.White);

            AddToBuffer("\nYou can ask questions in natural language, like:", ConsoleColor.Cyan);
            AddToBuffer("- How do I create a strong password?", ConsoleColor.White);
            AddToBuffer("- What should I look for in phishing emails?", ConsoleColor.White);
            AddToBuffer("- How can I browse safely on public WiFi?", ConsoleColor.White);

            AddToBuffer("\nPress ENTER to begin...".PadCenter(ConsoleWidth), ConsoleColor.Green);

            FlushBuffer();
        }

        public string GetUserName()
        {
            ClearBuffer();
            AddToBuffer("What's your name? ", ConsoleColor.Yellow);
            FlushBuffer();

            string name = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(name) || name.Length < 3)
            {
                ClearBuffer();
                AddToBuffer("Name must be at least 3 characters long.", ConsoleColor.Red);
                AddToBuffer("What's your name? ", ConsoleColor.Yellow);
                FlushBuffer();
                name = Console.ReadLine();
            }

            return name.Trim();
        }

        public void PrintWelcomeMessage(string userName)
        {
            ClearBuffer();
            PrintDivider();
            AddToBuffer($"Welcome, {userName}!", ConsoleColor.Green);
            AddToBuffer("Ask me anything about cybersecurity!", ConsoleColor.Green);
            AddToBuffer("\nTry asking about:", ConsoleColor.Cyan);
            AddToBuffer("- Creating and managing secure passwords", ConsoleColor.White);
            AddToBuffer("- Recognizing online scams and phishing", ConsoleColor.White);
            AddToBuffer("- Protecting your devices and data", ConsoleColor.White);
            AddToBuffer("\nType 'topics' to see everything I can help with", ConsoleColor.Cyan);
            AddToBuffer("Type 'exit' to end our conversation", ConsoleColor.Cyan);
            PrintDivider();
            FlushBuffer();
        }

        public void PromptForQuestion()
        {
            ClearBuffer();
            AddToBuffer("\nWhat would you like to know about? ", ConsoleColor.Yellow);
            FlushBuffer();
        }

        public void PrintResponse(string response)
        {
            ClearBuffer();
            PrintDivider();

            // Special formatting for phishing responses
            if (response.Contains("PHISHING ALERT"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string('=', ConsoleWidth));
                Console.WriteLine("⚠️ IMPORTANT SECURITY WARNING ⚠️".PadCenter(ConsoleWidth));
                Console.WriteLine(new string('=', ConsoleWidth));
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(response);
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string('=', ConsoleWidth));
                Console.WriteLine("🚨 REMEMBER: Never share sensitive data via links! 🚨".PadCenter(ConsoleWidth));
                Console.WriteLine(new string('=', ConsoleWidth));
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(response);
                Console.ResetColor();
                PrintDivider();
            }
        }

        public void PrintNoInputMessage()
        {
            ClearBuffer();
            AddToBuffer("I didn't hear anything. Try again.", ConsoleColor.Red);
            PrintInputHint();
            FlushBuffer();
        }

        public void PrintGoodbyeMessage(string userName)
        {
            ClearBuffer();
            PrintDivider();
            AddToBuffer($"Goodbye, {userName}! Stay safe online!", ConsoleColor.Magenta);
            PrintDivider();
            FlushBuffer();
        }

        public void PrintQuizInvitation()
        {
            ClearBuffer();
            PrintDivider();
            AddToBuffer("Would you like to take a short quiz on what we've discussed?", ConsoleColor.Yellow);
            AddToBuffer("Type 'yes' to begin the quiz or anything else to continue chatting...", ConsoleColor.Cyan);
            PrintDivider();
            FlushBuffer();
        }

        public void PrintQuizOffer()
        {
            ClearBuffer();
            PrintDivider('*');
            AddToBuffer("QUIZ TIME!".PadCenter(ConsoleWidth), ConsoleColor.Magenta);
            AddToBuffer($"You've learned enough to test your knowledge!", ConsoleColor.Yellow);
            AddToBuffer("Type 'quiz' at any time to start the cybersecurity quiz", ConsoleColor.Cyan);
            AddToBuffer("or keep asking questions to learn more first.", ConsoleColor.Cyan);
            PrintDivider('*');
            FlushBuffer();
        }

        public void PrintInputHint()
        {
            var hint = new StringBuilder();
            hint.AppendLine("Try asking about:");
            hint.AppendLine("- Password creation and management");
            hint.AppendLine("- Recognizing online scams");
            hint.AppendLine("- Securing your social media");
            hint.AppendLine("- Or type 'topics' for full list");

            PrintMessage(hint.ToString(), ConsoleColor.DarkCyan);
        }

        public void PrintMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        #region Helper Methods
        private void PrintDivider(char dividerChar = '=')
        {
            _displayBuffer.AppendLine(new string(dividerChar, ConsoleWidth));
        }

        private void AddToBuffer(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            _displayBuffer.AppendLine(text);
            Console.ResetColor();
        }

        private void ClearBuffer()
        {
            _displayBuffer.Clear();
        }

        private void FlushBuffer()
        {
            Console.Write(_displayBuffer.ToString());
            ClearBuffer();
        }
        #endregion
    }

    public static class StringExtensions
    {
        public static string PadCenter(this string str, int totalWidth, char paddingChar = ' ')
        {
            int padding = totalWidth - str.Length;
            if (padding <= 0) return str;

            return str.PadLeft(str.Length + padding / 2, paddingChar)
                     .PadRight(totalWidth, paddingChar);
        }
    }
}