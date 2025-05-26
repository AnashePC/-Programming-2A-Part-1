using System;
using System.Collections.Generic;
using System.Linq;

namespace CybersecurityChatbot.Services
{
    public class ChatService
    {
        private readonly DisplayService _displayService;
        private readonly ResponseService _responseService;
        private readonly QuizService _quizService;
        private List<string> _discussedTopics = new List<string>();
        private bool _quizOffered = false;
        private string _lastTopic = "";
        private int _repeatCount = 0;

        public ChatService(DisplayService displayService, ResponseService responseService, QuizService quizService)
        {
            _displayService = displayService;
            _responseService = responseService;
            _quizService = quizService;
        }

        public void StartChat(string userName)
        {
            _displayService.PrintWelcomeMessage(userName);

            bool continueChat = true;
            while (continueChat)
            {
                _displayService.PromptForQuestion();
                string userInput = Console.ReadLine()?.Trim();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    _displayService.PrintNoInputMessage();
                    continue;
                }

                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    continueChat = false;
                    _displayService.PrintGoodbyeMessage(userName);
                    continue;
                }

                var (response, topic) = _responseService.GetResponse(userInput, userName);

                // Handle topic repetition
                if (topic == _lastTopic)
                {
                    _repeatCount++;
                    if (_repeatCount > 2)
                    {
                        response += "\n\nWould you like to:\n1. Get more examples\n2. Move to another topic\n3. Take the quiz now";
                    }
                }
                else
                {
                    _repeatCount = 0;
                    _lastTopic = topic;
                }

                _displayService.PrintResponse(response);

                // Track discussed topics
                if (!string.IsNullOrEmpty(topic) &&
                    !new[] { "greeting", "thanks", "help", "quiz", "start-quiz", "unknown" }.Contains(topic))
                {
                    if (!_discussedTopics.Contains(topic))
                    {
                        _discussedTopics.Add(topic);
                    }
                }

                // Offer quiz
                if (_responseService.ShouldOfferQuiz() && !_quizOffered)
                {
                    _displayService.PrintQuizOffer();
                    _quizOffered = true;
                }
            }
        }
    }
}