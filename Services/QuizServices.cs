using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Services
{
    public class QuizService
    {
        private readonly DisplayService _displayService;
        private readonly Dictionary<string, List<QuizQuestion>> _topicQuestions;

        public QuizService(DisplayService displayService)
        {
            _displayService = displayService;
            _topicQuestions = InitializeQuestions();
        }

        public void OfferQuiz(List<string> discussedTopics)
        {
            _displayService.PrintMessage("\nWould you like to take a short quiz on what we've discussed?", ConsoleColor.Yellow);
            _displayService.PrintMessage("Type 'yes' to take the quiz or anything else to continue chatting...", ConsoleColor.Cyan);

            if (Console.ReadLine()?.ToLower() != "yes")
                return;

            var questions = SelectQuizQuestions(discussedTopics);
            int score = 0;

            for (int i = 0; i < questions.Count; i++)
            {
                score += AskQuestion(questions[i], i + 1);
            }

            DisplayQuizResults(score, questions.Count);
        }

        private int AskQuestion(QuizQuestion question, int questionNumber)
        {
            _displayService.PrintMessage($"\nQuestion {questionNumber}: {question.QuestionText}", ConsoleColor.White);

            for (int i = 0; i < question.Options.Count; i++)
            {
                _displayService.PrintMessage($"{i + 1}. {question.Options[i]}", ConsoleColor.Cyan);
            }

            while (true)
            {
                _displayService.PrintMessage("\nYour answer (1-4): ", ConsoleColor.Yellow);
                if (int.TryParse(Console.ReadLine(), out int answer) && answer >= 1 && answer <= 4)
                {
                    if (answer - 1 == question.CorrectOptionIndex)
                    {
                        _displayService.PrintMessage("Correct!", ConsoleColor.Green);
                        return 1;
                    }
                    else
                    {
                        _displayService.PrintMessage($"Incorrect. The correct answer was: {question.Options[question.CorrectOptionIndex]}", ConsoleColor.Red);
                        return 0;
                    }
                }
                _displayService.PrintMessage("Please enter a number between 1 and 4", ConsoleColor.Red);
            }
        }

        private void DisplayQuizResults(int score, int totalQuestions)
        {
            double percentage = (double)score / totalQuestions * 100;

            _displayService.PrintMessage("\nQuiz Results:", ConsoleColor.Magenta);
            _displayService.PrintMessage($"You scored {score} out of {totalQuestions}", ConsoleColor.White);

            if (percentage >= 60)
            {
                _displayService.PrintMessage("Well done! You've got a good understanding of these topics.", ConsoleColor.Green);
            }
            else
            {
                _displayService.PrintMessage("You should spend more time learning about cybersecurity!", ConsoleColor.Red);
                _displayService.PrintMessage("Staying safe online is important - keep studying!", ConsoleColor.Red);
            }
        }

        private List<QuizQuestion> SelectQuizQuestions(List<string> discussedTopics)
        {
            var selectedQuestions = new List<QuizQuestion>();
            var random = new Random();

            // Get at least one question from each discussed topic
            foreach (var topic in discussedTopics)
            {
                if (_topicQuestions.ContainsKey(topic)
                {
                    var topicQuestions = _topicQuestions[topic];
                    selectedQuestions.Add(topicQuestions[random.Next(topicQuestions.Count)]);
                }
            }

            // Fill remaining questions from any discussed topic
            while (selectedQuestions.Count < 5 && discussedTopics.Count > 0)
            {
                var randomTopic = discussedTopics[random.Next(discussedTopics.Count)];
                var topicQuestions = _topicQuestions[randomTopic];
                var newQuestion = topicQuestions[random.Next(topicQuestions.Count)];

                if (!selectedQuestions.Contains(newQuestion))
                {
                    selectedQuestions.Add(newQuestion);
                }
            }

            return selectedQuestions;
        }

        private Dictionary<string, List<QuizQuestion>> InitializeQuestions()
        {
            return new Dictionary<string, List<QuizQuestion>>
            {
                {
                    "password",
                    new List<QuizQuestion>
                    {
                        new QuizQuestion(
                            "What's the recommended minimum length for a strong password?",
                            new List<string> { "6 characters", "8 characters", "12 characters", "16 characters" },
                            2),
                        new QuizQuestion(
                            "Which of these is NOT a good password practice?",
                            new List<string> { "Using a password manager", "Reusing passwords across sites", "Enabling two-factor authentication", "Using a mix of character types" },
                            1)
                    }
                },
                {
                    "phishing",
                    new List<QuizQuestion>
                    {
                        new QuizQuestion(
                            "What's a common sign of a phishing email?",
                            new List<string> { "Perfect spelling and grammar", "Links that match the displayed text", "Urgent requests for personal information", "Coming from a known contact" },
                            2),
                        new QuizQuestion(
                            "What should you do if you receive a suspicious email?",
                            new List<string> { "Click links to verify", "Reply asking for more information", "Report it to your IT department", "Forward it to all your contacts" },
                            2)
                    }
                },
                {
                    "browsing",
                    new List<QuizQuestion>
                    {
                        new QuizQuestion(
                            "What does HTTPS indicate in a website URL?",
                            new List<string> { "The site is popular", "The connection is encrypted", "The site has videos", "The site is government-run" },
                            1),
                        new QuizQuestion(
                            "Why should you be careful with public WiFi?",
                            new List<string> { "It's often slow", "Others might see what you're doing", "It can expose your data to hackers", "Both 2 and 3" },
                            3)
                    }
                },
                // Add questions for all other topics...
            };
        }
    }

    public class QuizQuestion
    {
        public string QuestionText { get; }
        public List<string> Options { get; }
        public int CorrectOptionIndex { get; }

        public QuizQuestion(string questionText, List<string> options, int correctOptionIndex)
        {
            QuestionText = questionText;
            Options = options;
            CorrectOptionIndex = correctOptionIndex;
        }
    }
}