using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CybersecurityChatbot.Services
{
    public class ResponseService
    {
        private int _questionsAnswered = 0;
        private readonly Dictionary<string, Topic> _topics;

        public ResponseService()
        {
            _topics = InitializeTopics();
        }

        public (string response, string topic) GetResponse(string input, string userName)
        {
            input = input.ToLower().Trim();

            // Handle meta-commands first
            if (input == "topics" || input == "help" || input == "what can you help with")
                return (GetAvailableTopics(), "help");

            if (input == "quiz" && ShouldOfferQuiz())
                return ("Type 'yes' to start the quiz!", "quiz");

            if (input == "yes" && ShouldOfferQuiz())
                return ("Starting quiz...", "start-quiz");

            // Handle greetings
            if (ContainsAny(input, "hello", "hi", "hey", "greetings"))
                return ($"Hello {userName}! How can I help with cybersecurity today?", "greeting");

            if (ContainsAny(input, "how are you", "how's it going"))
                return ($"I'm just a bot, {userName}, but I'm ready to help!", "greeting");

            if (ContainsAny(input, "thank", "thanks", "appreciate"))
                return ($"You're welcome, {userName}! Stay safe online!", "thanks");

            // Find the most relevant topic
            var matchedTopic = FindRelevantTopic(input);

            if (matchedTopic != null)
            {
                _questionsAnswered++;
                return (GenerateTopicResponse(matchedTopic, userName), matchedTopic.Name);
            }

            // Fallback response with suggestions
            return (GetFallbackResponse(input, userName), "unknown");
        }

        private Topic FindRelevantTopic(string input)
        {
            input = input.ToLower();

            // Priority matching for phishing
            var phishingKeywords = new List<string> {
                "phish", "scam", "fraud", "fake", "spoof",
                "email", "message", "link", "suspect", "fishy",
                "hoax", "trick", "deceive", "pretend", "impersonate"
            };

            if (phishingKeywords.Any(k => input.Contains(k)))
            {
                return _topics["phishing protection"];
            }

            // Then check other topics
            foreach (var topic in _topics.Values)
            {
                if (topic.Keywords.Any(k => input.Contains(k)) ||
                    topic.Aliases.Any(a => input.Contains(a)))
                {
                    return topic;
                }
            }
            return null;
        }

        private string GenerateTopicResponse(Topic topic, string userName)
        {
            var response = new StringBuilder();

            // Special header for phishing
            if (topic.Name == "Phishing Protection")
            {
                response.AppendLine("⚠️ PHISHING ALERT ⚠️");
                response.AppendLine($"{topic.Name} is crucial, {userName}!");
            }
            else
            {
                response.AppendLine($"{topic.Name} is important, {userName}!");
            }

            response.AppendLine(topic.Description);
            response.AppendLine("\nKey things to know:");

            foreach (var point in topic.KeyPoints)
            {
                response.AppendLine($"• {point}");
            }

            // Special footer for phishing
            if (topic.Name == "Phishing Protection")
            {
                response.AppendLine("\n🚨 Remember: When in doubt, don't click! 🚨");
            }

            response.AppendLine($"\nRelated questions: {string.Join(", ", topic.ExampleQuestions.Take(3))}");
            return response.ToString();
        }

        private string GetFallbackResponse(string input, string userName)
        {
            var similarTopics = _topics.Values
                .OrderByDescending(t => t.Keywords.Count(k => input.Contains(k)))
                .Take(3)
                .ToList();

            var response = new StringBuilder();
            response.AppendLine($"I'm not sure I understand your question about '{input}', {userName}.");

            if (similarTopics.Count > 0)
            {
                response.AppendLine("I can help with these similar topics:");
                foreach (var topic in similarTopics)
                {
                    response.AppendLine($"- {topic.Name} (try: '{topic.ExampleQuestions.First()}')");
                }
            }
            else
            {
                response.AppendLine("I specialize in:");
                response.AppendLine("- Password security");
                response.AppendLine("- Phishing protection");
                response.AppendLine("- Safe browsing");
            }

            response.AppendLine("\nTry being more specific or type 'topics' for full list.");
            return response.ToString();
        }

        private Dictionary<string, Topic> InitializeTopics()
        {
            var topics = new List<Topic>
            {
                new Topic(
                    name: "Password Security",
                    description: "Creating and managing strong, secure passwords for all your accounts.",
                    keyPoints: new List<string>
                    {
                        "Use at least 12 characters with mixed character types",
                        "Never reuse passwords across different sites",
                        "Consider using a password manager",
                        "Enable two-factor authentication",
                        "Change passwords after breaches"
                    },
                    exampleQuestions: new List<string>
                    {
                        "How do I create a strong password?",
                        "What's the best password manager?",
                        "How often should I change passwords?",
                        "What's two-factor authentication?"
                    },
                    keywords: new List<string> { "password", "login", "credentials", "authentication" },
                    aliases: new List<string> { "pass word", "log in", "account security" }),

                new Topic(
                    name: "Phishing Protection",
                    description: "Identifying and avoiding fraudulent attempts to steal your information through emails, messages, or fake websites.",
                    keyPoints: new List<string>
                    {
                        "Never trust urgent requests for personal information",
                        "Check sender addresses carefully for misspellings",
                        "Hover over links to see actual URLs",
                        "Legitimate companies won't ask for passwords via email",
                        "When in doubt, contact the company directly"
                    },
                    exampleQuestions: new List<string>
                    {
                        "How can I spot phishing emails?",
                        "What should I do if I clicked a phishing link?",
                        "How do I report phishing attempts?",
                        "Can phishing happen through text messages?"
                    },
                    keywords: new List<string> {
                        "phish", "scam", "fraud", "fake", "spoof",
                        "email", "message", "link", "suspect"
                    },
                    aliases: new List<string> {
                        "email scam", "online fraud", "fake link",
                        "suspicious email", "message scam"
                    }),

                // Other topics remain the same...
            };

            return topics.ToDictionary(t => t.Name.ToLower());
        }

        public string GetAvailableTopics()
        {
            var response = new StringBuilder();
            response.AppendLine("I can help with these cybersecurity topics:\n");

            foreach (var topic in _topics.Values)
            {
                response.AppendLine($"► {topic.Name.ToUpper()}");
                response.AppendLine($"  {topic.Description}");
                response.AppendLine($"  Example questions: {string.Join(", ", topic.ExampleQuestions.Take(2))}");
                response.AppendLine();
            }

            response.AppendLine("You can ask about these in your own words!");
            return response.ToString();
        }

        private bool ContainsAny(string input, params string[] terms)
        {
            return terms.Any(term => input.Contains(term));
        }

        public bool ShouldOfferQuiz()
        {
            return _questionsAnswered >= 3;
        }
    }

    public class Topic
    {
        public string Name { get; }
        public string Description { get; }
        public List<string> KeyPoints { get; }
        public List<string> ExampleQuestions { get; }
        public List<string> Keywords { get; }
        public List<string> Aliases { get; }

        public Topic(string name, string description, List<string> keyPoints,
                   List<string> exampleQuestions, List<string> keywords, List<string> aliases)
        {
            Name = name;
            Description = description;
            KeyPoints = keyPoints;
            ExampleQuestions = exampleQuestions;
            Keywords = keywords;
            Aliases = aliases;
        }
    }
}