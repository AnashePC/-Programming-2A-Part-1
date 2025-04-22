namespace CybersecurityChatbot.Services
{
    public class ResponseService
    {
        public string GetResponse(string input, string userName)
        {
            input = input.ToLower().Trim();

            if (input.Contains("hello") || input.Contains("hi") || input.Contains("hey"))
                return $"Hello {userName}! How can I help with cybersecurity today?";
            if (input.Contains("how are you"))
                return $"I'm just a bot, {userName}, but I'm ready to help!";
            if (input.Contains("thank"))
                return $"You're welcome, {userName}! Stay safe online!";

            if (input.Contains("password"))
                return GetPasswordResponse(userName);
            if (input.Contains("phishing"))
                return GetPhishingResponse(userName);
            if (input.Contains("browsing") || input.Contains("internet") || input.Contains("vpn"))
                return GetBrowsingResponse(userName);

            return "I focus on cybersecurity. Try asking about passwords, phishing, or browsing safety.";
        }

        private string GetPasswordResponse(string userName)
        {
            return $"Passwords matter, {userName}!\n" +
                   "• Use at least 12 characters\n" +
                   "• Mix uppercase, lowercase, numbers, and symbols\n" +
                   "• Avoid personal information\n" +
                   "• Enable two-factor authentication";
        }

        private string GetPhishingResponse(string userName)
        {
            return $"Phishing scams are dangerous, {userName}!\n" +
                   "• Never click on suspicious links\n" +
                   "• Verify email senders\n" +
                   "• Check for urgent or scare tactics\n" +
                   "• Report phishing attempts to authorities";
        }

        private string GetBrowsingResponse(string userName)
        {
            return $"Stay safe while browsing, {userName}!\n" +
                   "• Use HTTPS websites\n" +
                   "• Avoid public WiFi for sensitive transactions\n" +
                   "• Enable browser security features\n" +
                   "• Keep your software updated";
        }
    }
}