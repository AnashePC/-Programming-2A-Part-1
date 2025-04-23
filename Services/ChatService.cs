using System;
using System.Collections.Generic;

namespace CybersecurityChatbot.Services
{
    public class ChatService
    {
        private readonly DisplayService _displayService;
        private readonly ResponseService _responseService;
        private List<string> _discussedTopics = new List<string>();

        public ChatService(DisplayService displayService, ResponseService responseService)
        {
            _displayService = displayService;
            _responseService = responseService;
        }

        public void StartChat(string userName, QuizService quizService)
        {
            _displayService.PrintWelcomeMessage(userName);

            bool continueChat = true;

            while (continueChat)
            {
                _displayService.PromptForQuestion();

                string userInput = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrWhiteSpace(userInput))
                {
                    _displayService.PrintNoInputMessage();
                    continue;
                }

                if (userInput == "exit")
                {
                    continueChat = false;
                    _displayService.PrintGoodbyeMessage(userName);
                    continue;
                }

                if (userInput == "quiz")
                {
                    quizService.OfferQuiz(_discussedTopics);
                    continue;
                }

                var (response, topic) = _responseService.GetResponse(userInput, userName);
                _displayService.PrintResponse(response);

                if (!_discussedTopics.Contains(topic) && topic != "greeting" && topic != "thanks" && topic != "unknown" && topic != "help")
                {
                    _discussedTopics.Add(topic);
                }

                if (_responseService.ShouldOfferQuiz())
                {
                    quizService.OfferQuiz(_discussedTopics);
                }
            }
        }
    }
}