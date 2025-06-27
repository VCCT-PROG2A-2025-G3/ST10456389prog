using System;
using System.Collections.Generic;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbot
{
    // Handles topic-based tips, emojis, and colors
    public static class CyberTipManager
    {
        private static readonly Random random = new Random();

        private static readonly Dictionary<string, string[]> CyberTips = new Dictionary<string, string[]>();

        // Dictionary of topic colors
        private static readonly Dictionary<string, ConsoleColor> TopicColors = new Dictionary<string, ConsoleColor>
        {
            { "password", ConsoleColor.Blue },
            { "phishing", ConsoleColor.Yellow },
            { "scam", ConsoleColor.Red },
            { "privacy", ConsoleColor.Green }
        };

        // Static constructor to populate tips
        static CyberTipManager()
        {
            CyberTips.Add("password", new[]
            {
                "Use strong, unique passwords for each account. Avoid using personal info like your name or birthday.",
                "Consider using a password manager to keep track of complex passwords.",
                "Change your passwords regularly and avoid reusing them.",
                "Avoid using the same password across multiple sites — if one gets compromised, all are at risk.",
                "Enable multi-factor authentication (MFA) wherever possible for an extra layer of security."
            });

            CyberTips.Add("phishing", new[]
            {
                "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                "Always hover over links to see the actual URL before clicking.",
                "Look for spelling mistakes or urgent language — these are signs of phishing.",
                "Never download attachments from unknown or unexpected senders.",
                "Be wary of messages with emotional manipulation like fear or urgency."
            });

            CyberTips.Add("scam", new[]
            {
                "Scammers use tricks to steal money or information. Stay alert for too-good-to-be-true offers.",
                "Never send money or share personal info with someone you don’t trust.",
                "If something feels off, take a moment to verify the source before acting.",
                "Avoid clicking on links in messages from unknown senders.",
                "Be skeptical of job offers, giveaways, or prizes that ask for upfront fees."
            });

            CyberTips.Add("privacy", new[]
            {
                "Adjust your social media settings to limit data sharing.",
                "Only share information online with trusted sources.",
                "Regularly review and update app permissions on your devices.",
                "Avoid posting personal details like your birthday or address publicly.",
                "Clear cookies and browser data regularly to reduce tracking."
            });
        }

        // Return a random tip from a topic
        public static string GetRandomTip(string topic)
        {
            if (!CyberTips.ContainsKey(topic)) return "No tips found for this topic.";
            return CyberTips[topic][random.Next(CyberTips[topic].Length)];
        }

        // Return topic emoji
        public static string GetTopicEmoji(string topic)
        {
            if (topic == "password") return "🔐";
            if (topic == "phishing") return "🎓";
            if (topic == "scam") return "⚠️";
            if (topic == "privacy") return "🛡️";
            return "💡";
        }

        // Get color for topic
        public static ConsoleColor GetTopicColor(string topic)
        {
            return TopicColors.ContainsKey(topic) ? TopicColors[topic] : ConsoleColor.White;
        }

        // Capitalize the first letter of a word
        public static string Capitalize(string word)
        {
            return char.ToUpper(word[0]) + word.Substring(1);
        }

        // Return all known topics
        public static IEnumerable<string> GetTopics()
        {
            return CyberTips.Keys;
        }

        // Check for follow-up words in input
        public static bool IsFollowUp(string input)
        {
            return input.Contains("more") || input.Contains("explain") || input.Contains("why") || input.Contains("tell me");
        }

        // Print another random tip from the last topic
        public static string GetMoreInfo(string topic)
        {
            if (string.IsNullOrEmpty(topic) || !CyberTips.ContainsKey(topic))
            {
                return "Could you clarify what you'd like to know more about? (e.g., passwords, phishing, etc.)";
            }

            string tip = GetRandomTip(topic);
            return $"{GetTopicEmoji(topic)} More on {Capitalize(topic)}: {tip}";
        }

    }
}

//---------------------------------End of File---------------------------------//
