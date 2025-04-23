using System;
using System.Windows.Forms.Design;
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

            displayService.DisplayAsciiArt();
            displayService.DisplayWelcomeScreen();

            Console.ReadLine(); // Wait for user to press Enter

            soundService.PlayWelcomeSound();
            string userName = displayService.GetUserName();
            chatService.StartChat(userName, quizService);
        }
    }
}