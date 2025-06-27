using System;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbot
{
    // Handles emotional input responses
    public static class SentimentHandler
    {
        // Returns a response string if emotional sentiment is detected, otherwise null
        public static string DetectSentiment(string input)
        {
            if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious"))
            {
                return "😟 It's completely understandable to feel that way. Scammers can be very convincing. Let me share some tips to help you stay safe.";
            }
            else if (input.Contains("frustrated") || input.Contains("confused"))
            {
                return "🤔 Don't worry, I'm here to help you through any confusion. Cybersecurity can be tricky, but you're doing great by learning about it!";
            }
            else if (input.Contains("curious") || input.Contains("interested"))
            {
                return "🧠 Curiosity is the first step to strong cybersecurity! Ask me anything you'd like to know more about.";
            }

            return null;
        }
    }
}
//---------------------------------End of File---------------------------------//
