using System;
using System.IO;
using System.Media;
using System.Threading;

namespace CybersecurityChatbot.Services
{
    public class SoundService
    {
        public void PlayWelcomeSound()
        {
            try
            {
                string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "welcome.wav");

                if (File.Exists(soundPath))
                {
                    using (var player = new SoundPlayer(soundPath))
                    {
                        player.Play();
                        Thread.Sleep(500);
                    }
                }
                else
                {
                    Console.WriteLine("Sound file not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sound error: {ex.Message}");
            }
        }
    }
}