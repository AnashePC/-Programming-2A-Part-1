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
                // Play sound asynchronously so it doesn't block the UI
                new Thread(() =>
                {
                    string soundPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "welcome.wav");

                    if (File.Exists(soundPath))
                    {
                        using (var player = new SoundPlayer(soundPath))
                        {
                            player.PlaySync(); // PlaySync ensures the sound plays completely
                        }
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Sound error: {ex.Message}");
            }
        }
    }
}