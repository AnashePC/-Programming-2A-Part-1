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

            // Play sound immediately on startup
            soundService.PlayWelcomeSound();

            // Then show the welcome screen
            displayService.DisplayAsciiArt();
            displayService.DisplayWelcomeScreen();

            // Wait for user to press Enter
            Console.ReadLine();

            // Get user name and start chat
            string userName = displayService.GetUserName();
            chatService.StartChat(userName, quizService);
        }
    }
}