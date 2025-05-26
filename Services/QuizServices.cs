using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Services
{
    public class QuizService
    {
        private readonly DisplayService _displayService;
        private readonly ResponseService _responseService;
        private readonly Dictionary<string, List<QuizQuestion>> _quizBank;

        public QuizService(DisplayService displayService, ResponseService responseService)
        {
            _displayService = displayService;
            _responseService = responseService;
            _quizBank = InitializeQuizBank();
        }

        public void OfferQuiz(List<string> discussedTopics)
        {
            _displayService.PrintQuizInvitation();

            if (Console.ReadLine()?.ToLower() != "yes")
                return;

            var questions = SelectQuestions(discussedTopics);
            if (questions.Count == 0)
            {
                _displayService.PrintMessage("No quiz questions available for discussed topics.", ConsoleColor.Yellow);
                return;
            }

            int score = 0;
            for (int i = 0; i < questions.Count; i++)
            {
                score += PresentQuestion(questions[i], i + 1);
            }

            DisplayResults(score, questions.Count);
        }

        private List<QuizQuestion> SelectQuestions(List<string> discussedTopics)
        {
            var selectedQuestions = new List<QuizQuestion>();
            var random = new Random();

            // Get at least one question per discussed topic
            foreach (var topic in discussedTopics)
            {
                if (_quizBank.ContainsKey(topic))
                {
                    var availableQuestions = _quizBank[topic];
                    selectedQuestions.Add(availableQuestions[random.Next(availableQuestions.Count)]);
                }
            }

            // Fill up to 5 questions if possible
            while (selectedQuestions.Count < 5 && discussedTopics.Count > 0)
            {
                var randomTopic = discussedTopics[random.Next(discussedTopics.Count)];
                if (_quizBank.ContainsKey(randomTopic))
                {
                    var newQuestion = _quizBank[randomTopic][random.Next(_quizBank[randomTopic].Count)];
                    if (!selectedQuestions.Contains(newQuestion))
                    {
                        selectedQuestions.Add(newQuestion);
                    }
                }
            }

            return selectedQuestions;
        }

        private int PresentQuestion(QuizQuestion question, int questionNumber)
        {
            _displayService.PrintMessage($"\nQuestion {questionNumber}: {question.Text}", ConsoleColor.White);

            for (int i = 0; i < question.Options.Count; i++)
            {
                _displayService.PrintMessage($"{i + 1}. {question.Options[i]}", ConsoleColor.Cyan);
            }

            while (true)
            {
                _displayService.PrintMessage("\nYour answer (1-4): ", ConsoleColor.Yellow);
                if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 1 && answer <= 4)
                {
                    if (answer - 1 == question.CorrectAnswerIndex)
                    {
                        _displayService.PrintMessage("✓ Correct!", ConsoleColor.Green);
                        return 1;
                    }
                    else
                    {
                        _displayService.PrintMessage($"✗ Incorrect. The correct answer was: {question.Options[question.CorrectAnswerIndex]}", ConsoleColor.Red);
                        return 0;
                    }
                }
                _displayService.PrintMessage("Please enter a number between 1 and 4", ConsoleColor.Red);
            }
        }

        private void DisplayResults(int score, int totalQuestions)
        {
            double percentage = score / (double)totalQuestions * 100;

            _displayService.PrintMessage("\nQuiz Results:", ConsoleColor.Magenta);
            _displayService.PrintMessage($"You scored {score} out of {totalQuestions} ({percentage:0}%)", ConsoleColor.White);

            if (percentage >= 80)
            {
                _displayService.PrintMessage("Excellent work! You're a cybersecurity pro!", ConsoleColor.Green);
            }
            else if (percentage >= 60)
            {
                _displayService.PrintMessage("Good job! You have solid cybersecurity knowledge.", ConsoleColor.Green);
            }
            else if (percentage >= 40)
            {
                _displayService.PrintMessage("Not bad, but you should review these topics more.", ConsoleColor.Yellow);
            }
            else
            {
                _displayService.PrintMessage("You need to spend more time learning about cybersecurity!", ConsoleColor.Red);
                _displayService.PrintMessage("These concepts are important for your online safety.", ConsoleColor.Red);
            }
        }

        private Dictionary<string, List<QuizQuestion>> InitializeQuizBank()
        {
            return new Dictionary<string, List<QuizQuestion>>
            {
                ["password security"] = new List<QuizQuestion>
                {
                    new QuizQuestion(
                        "What's the recommended minimum length for a strong password?",
                        new List<string> { "6 characters", "8 characters", "12 characters", "16 characters" },
                        2),
                    new QuizQuestion(
                        "Which of these is NOT a good password practice?",
                        new List<string> { "Using a password manager", "Reusing passwords across sites", "Enabling two-factor authentication", "Using a mix of character types" },
                        1)
                },
                ["phishing protection"] = new List<QuizQuestion>
                {
                    new QuizQuestion(
                        "What's a common sign of a phishing email?",
                        new List<string> { "Perfect spelling and grammar", "Links that match the displayed text", "Urgent requests for personal information", "Coming from a known contact" },
                        2),
                    new QuizQuestion(
                        "What should you do if you receive a suspicious email?",
                        new List<string> { "Click links to verify", "Reply asking for more information", "Report it to your IT department", "Forward it to all your contacts" },
                        2)
                },
                ["safe browsing"] = new List<QuizQuestion>
                {
                    new QuizQuestion(
                        "What does HTTPS indicate in a website URL?",
                        new List<string> { "The site is popular", "The connection is encrypted", "The site has videos", "The site is government-run" },
                        1),
                    new QuizQuestion(
                        "Why should you be careful with public WiFi?",
                        new List<string> { "It's often slow", "Others might see what you're doing", "It can expose your data to hackers", "Both 2 and 3" },
                        3)
                },
                ["malware protection"] = new List<QuizQuestion>
                {
                    new QuizQuestion(
                        "Which of these is NOT a type of malware?",
                        new List<string> { "Virus", "Ransomware", "Firewall", "Spyware" },
                        2),
                    new QuizQuestion(
                        "What's the best way to prevent malware infections?",
                        new List<string> { "Never update your software", "Use reputable antivirus software", "Download programs from any site", "Disable all security features" },
                        1)
                },
                ["social media safety"] = new List<QuizQuestion>
                {
                    new QuizQuestion(
                        "What information should you avoid sharing on social media?",
                        new List<string> { "Your favorite color", "Your vacation plans", "Your pet's name", "All of the above" },
                        3),
                    new QuizQuestion(
                        "How often should you review your social media privacy settings?",
                        new List<string> { "Never", "Only when you create the account", "At least once a year", "Every time you post" },
                        2)
                }
            };
        }
    }

    public class QuizQuestion
    {
        public string Text { get; }
        public List<string> Options { get; }
        public int CorrectAnswerIndex { get; }

        public QuizQuestion(string text, List<string> options, int correctAnswerIndex)
        {
            Text = text;
            Options = options;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}