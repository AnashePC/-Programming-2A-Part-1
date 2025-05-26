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
                DetectSentiment(input);
                bool keywordFound = false;
                foreach (var keyword in keywordResponses.Keys)
                {
                    if (input.Contains(keyword))
                    {
                        RespondToKeyword(keyword);
                        keywordFound = true;
                        break;
                    }
                }
                if (!keywordFound)
                {
                    if (input.Contains("help") || input.Contains("more info"))
                    {
                        Console.WriteLine("Chatbot: You can ask about password, scam, phishing, or privacy.");
                    }
                    else if (userInterest != "" && input.Contains("recommend"))
                    {
                        Console.WriteLine($"Chatbot: Since you're interested in {userInterest}, I recommend enabling two-factor authentication for your accounts.");
                    }
                    else
                    {
                        Console.WriteLine("Chatbot: I'm not sure I understand. Could you rephrase that?");
                    }
                }
            }
        }
        static void RespondToKeyword(string keyword)
        {
            Random rnd = new Random();
            List<string> responses = keywordResponses[keyword];
            int index = rnd.Next(responses.Count);
            Console.WriteLine($"Chatbot: {responses[index]}");
        }
        static void DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("anxious"))
            {
                userMood = "worried";
                Console.WriteLine("Chatbot: It's completely understandable to feel that way. Let's work together to stay safe online.");
            }
            else if (input.Contains("curious"))
            {
                userMood = "curious";
                Console.WriteLine("Chatbot: Curiosity is great! Feel free to ask me anything about cybersecurity.");
            }
            else if (input.Contains("frustrated") || input.Contains("confused"))
            {
                userMood = "frustrated";
                Console.WriteLine("Chatbot: I understand. Cybersecurity can be overwhelming, but I’m here to help make it simpler.");
            }
        }
    }
}