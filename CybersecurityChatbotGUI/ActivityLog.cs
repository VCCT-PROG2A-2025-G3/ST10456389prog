using System;
using System.Collections.Generic;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbot
{
    // A static class for recording the chatbot's activity history
    public static class ActivityLog
    {
        // Store log entries
        private static readonly List<string> logs = new List<string>();

        // Adds a new log entry 
        public static void Add(string message)
        {
            // Format
            logs.Add($"{DateTime.Now:HH:mm} - {message}");
        }

        // Returns a copy of the current activity
        public static List<string> GetLog()
        {
            // Return a new list
            return new List<string>(logs);
        }
    }
}
//---------------------------------End of File---------------------------------//

