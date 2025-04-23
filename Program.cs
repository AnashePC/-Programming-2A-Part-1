using System;
using CybersecurityChatbot.Services;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            var soundService = new SoundService();
            var displayService = new DisplayService();
            var responseService = new ResponseService();
            var chatService = new ChatService(displayService, responseService);
            var quizService = new QuizService(displayService);

            // Play sound immediately when program starts
            soundService.PlayWelcomeSound();

            // Then show the interface
            displayService.DisplayAsciiArt();
            displayService.DisplayWelcomeScreen();

            Console.ReadLine(); // Wait for user to press Enter

            string userName = displayService.GetUserName();
            chatService.StartChat(userName, quizService);
        }
    }
}