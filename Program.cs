using System;
using System.Threading;
using CybersecurityChatbot.Services;

namespace CybersecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            var soundService = new SoundService();
            var displayService = new DisplayService();
            var chatService = new ChatService(displayService);

            soundService.PlayWelcomeSound();
            displayService.DisplayAsciiArt();

            string userName = displayService.GetUserName();
            chatService.StartChat(userName);
        }
    }
}