using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Services
{
    public class ResponseService
    {
        private int _questionsAnswered = 0;
        private readonly Dictionary<string, string> _topicKeywords = new Dictionary<string, string>
        {
            {"password", "password|passwords|login|credentials|authentication"},
            {"phishing", "phishing|scam|email fraud|suspicious email"},
            {"browsing", "browsing|internet|vpn|proxy|web|online"},
            {"malware", "malware|virus|ransomware|trojan|spyware"},
            {"social", "social media|facebook|twitter|instagram|linkedin"},
            {"privacy", "privacy|data protection|gdpr|personal data"},
            {"network", "network|wifi|router|firewall|encryption"},
            {"gaming", "gaming|game|steam|epic|playstation|xbox"},
            {"mobile", "mobile|phone|smartphone|android|ios"},
            {"cloud", "cloud|storage|google drive|dropbox|icloud"}
        };

        private readonly Dictionary<string, Func<string, string>> _responseGenerators;

        public ResponseService()
        {
            _responseGenerators = new Dictionary<string, Func<string, string>>
            {
                {"password", GetPasswordResponse},
                {"phishing", GetPhishingResponse},
                {"browsing", GetBrowsingResponse},
                {"malware", GetMalwareResponse},
                {"social", GetSocialMediaResponse},
                {"privacy", GetPrivacyResponse},
                {"network", GetNetworkResponse},
                {"gaming", GetGamingResponse},
                {"mobile", GetMobileResponse},
                {"cloud", GetCloudResponse}
            };
        }

        public (string response, string topic) GetResponse(string input, string userName)
        {
            _questionsAnswered++;
            input = input.ToLower().Trim();

            // Handle greetings
            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
                return ($"Hello {userName}! How can I help with cybersecurity today?", "greeting");
            if (input.Contains("how are you"))
                return ($"I'm just a bot, {userName}, but I'm ready to help!", "greeting");
            if (input.Contains("thank"))
                return ($"You're welcome, {userName}! Stay safe online!", "thanks");

            // Find the most relevant topic
            var matchedTopic = FindRelevantTopic(input);

            if (matchedTopic != null)
            {
                return (_responseGenerators[matchedTopic](userName), matchedTopic);
            }

            return ("I focus on cybersecurity. Try asking about passwords, phishing, or other security topics.", "unknown");
        }

        private string FindRelevantTopic(string input)
        {
            // Find topic with most keyword matches
            var topicMatches = _topicKeywords
                .Select(kv => new
                {
                    Topic = kv.Key,
                    MatchCount = kv.Value.Split('|')
                        .Count(keyword => input.Contains(keyword))
                })
                .Where(x => x.MatchCount > 0)
                .OrderByDescending(x => x.MatchCount)
                .ToList();

            return topicMatches.FirstOrDefault()?.Topic;
        }

        public bool ShouldOfferQuiz()
        {
            return _questionsAnswered >= 5;
        }

        #region Topic Responses
        private string GetPasswordResponse(string userName)
        {
            return $"Password security is crucial, {userName}!\n" +
                   "• Use at least 12 characters with mixed characters\n" +
                   "• Never reuse passwords across sites\n" +
                   "• Consider using a password manager\n" +
                   "• Enable two-factor authentication wherever possible\n" +
                   "• Change passwords immediately after a data breach";
        }

        private string GetPhishingResponse(string userName)
        {
            return $"Phishing is a serious threat, {userName}!\n" +
                   "• Never click links in unsolicited emails\n" +
                   "• Check sender addresses carefully\n" +
                   "• Look for poor grammar/spelling\n" +
                   "• Hover over links to see real destinations\n" +
                   "• When in doubt, contact the company directly";
        }

        private string GetBrowsingResponse(string userName)
        {
            return $"Safe browsing is essential, {userName}!\n" +
                   "• Always look for HTTPS in URLs\n" +
                   "• Use a reputable VPN on public WiFi\n" +
                   "• Keep browsers and plugins updated\n" +
                   "• Use ad-blockers to avoid malicious ads\n" +
                   "• Be cautious with browser extensions";
        }

        private string GetMalwareResponse(string userName)
        {
            return $"Malware protection is vital, {userName}!\n" +
                   "• Install reputable antivirus software\n" +
                   "• Never disable security features\n" +
                   "• Be extremely careful with downloads\n" +
                   "• Keep all software updated\n" +
                   "• Regular system scans are important";
        }

        private string GetSocialMediaResponse(string userName)
        {
            return $"Social media safety matters, {userName}!\n" +
                   "• Review privacy settings regularly\n" +
                   "• Be careful what personal info you share\n" +
                   "• Beware of fake profiles\n" +
                   "• Don't overshare vacation plans\n" +
                   "• Be cautious with third-party apps";
        }

        private string GetPrivacyResponse(string userName)
        {
            return $"Data privacy is important, {userName}!\n" +
                   "• Regularly review app permissions\n" +
                   "• Use encrypted messaging apps\n" +
                   "• Be selective about what you share online\n" +
                   "• Consider using privacy-focused services\n" +
                   "• Read privacy policies before signing up";
        }

        private string GetNetworkResponse(string userName)
        {
            return $"Network security is key, {userName}!\n" +
                   "• Change default router passwords\n" +
                   "• Use WPA3 encryption if available\n" +
                   "• Disable WPS and UPnP when possible\n" +
                   "• Create a separate guest network\n" +
                   "• Regularly update router firmware";
        }

        private string GetGamingResponse(string userName)
        {
            return $"Gaming security is often overlooked, {userName}!\n" +
                   "• Use unique passwords for gaming accounts\n" +
                   "• Enable two-factor authentication\n" +
                   "• Be wary of 'free' game offers\n" +
                   "• Don't share account details\n" +
                   "• Watch out for phishing scams targeting gamers";
        }

        private string GetMobileResponse(string userName)
        {
            return $"Mobile security is critical, {userName}!\n" +
                   "• Always lock your device\n" +
                   "• Only download apps from official stores\n" +
                   "• Review app permissions carefully\n" +
                   "• Enable remote wipe capability\n" +
                   "• Keep your OS updated";
        }

        private string GetCloudResponse(string userName)
        {
            return $"Cloud security requires attention, {userName}!\n" +
                   "• Use strong, unique passwords\n" +
                   "• Enable two-factor authentication\n" +
                   "• Encrypt sensitive files before uploading\n" +
                   "• Be careful with sharing permissions\n" +
                   "• Regularly review connected apps";
        }
        #endregion
    }
}