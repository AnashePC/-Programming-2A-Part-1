using System;
using System.Collections.Generic;

namespace CybersecurityChatbot
{
    class Program
    {
        // Memory: store user name, interest, and mood
        static string userName = "";
        static string userInterest = "";
        static string userMood = "";

        // Keyword responses using a dictionary of lists
        static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
        {
            {"password", new List<string> {
                "Use strong, unique passwords for each account.",
                "Avoid using personal information like your name in your passwords.",
                "Consider using a password manager to store your passwords securely."
            }},
            {"scam", new List<string> {
                "Be cautious of emails or messages asking for money or personal details.",
                "Scammers often impersonate trusted entities like banks.",
                "Always verify links before clicking — scammers often disguise URLs."
            }},
            {"privacy", new List<string> {
                "Review your app and website permissions regularly.",
                "Avoid oversharing personal information online.",
                "Use privacy-focused browsers and search engines."
            }},
            {"phishing", new List<string> {
                "Be cautious of emails asking for credentials — phishing is common.",
                "Look for spelling mistakes and suspicious links in emails.",
                "Don't click links from unknown sources. Always verify the sender."
            }},
        };
        // Delegate for response actions
        delegate void ResponseAction(string keyword);

        static void Main()
        {
            Console.WriteLine("👋 Welcome to the Cybersecurity Awareness Chatbot!");
            Console.Write("Before we begin, what's your name? ");
            userName = Console.ReadLine();

            Console.WriteLine($"Nice to meet you, {userName}! What cybersecurity topic interests you most (e.g., password, scam, privacy)?");
            userInterest = Console.ReadLine().ToLower();

            if (keywordResponses.ContainsKey(userInterest))
            {
                Console.WriteLine($"Great! I'll remember that you're interested in {userInterest}.");
            }

            Console.WriteLine("\n✅ You can now chat with me. Type 'exit' to leave anytime.\n");

            while (true)
            {
                Console.Write("You: ");
                string input = Console.ReadLine().ToLower();

                if (input == "exit")
                {
                    Console.WriteLine("Chatbot: Stay safe online, and have a great day!");
                    break;
                }
