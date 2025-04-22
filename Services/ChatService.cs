using System;

namespace CybersecurityChatbot.Services
{
    public class ChatService
    {
        private readonly DisplayService _displayService;
        private readonly ResponseService _responseService;

        public ChatService(DisplayService displayService)
        {
            _displayService = displayService;
            _responseService = new ResponseService();
        }

        public void StartChat(string userName)
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

                string response = _responseService.GetResponse(userInput, userName);
                _displayService.PrintResponse(response);
            }
        }
    }
}