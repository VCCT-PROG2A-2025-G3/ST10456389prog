using System;
using System.Media;

namespace CybersecurityChatbot
{
    public static class VoicePlayer
    {
        public static void PlayVoiceGreeting()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("Greeting.wav"); // Ensure this file is in the output directory
                player.Load();
                player.Play(); // Use Play for async playback in GUI
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error playing audio: " + ex.Message);
            }
        }
    }
}
