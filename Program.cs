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