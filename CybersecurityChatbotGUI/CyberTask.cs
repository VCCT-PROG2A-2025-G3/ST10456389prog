using System;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbot
{
    // Represents a single cybersecurity-related task that can include a reminder and completion status
    public class CyberTask
    {
        // Title of the task 
        public string Title { get; set; }

        // Description of the task
        public string Description { get; set; }

        // Reminder date for the task
        public DateTime? ReminderDate { get; set; }

        // Indicates whether the task has been marked as completed
        public bool IsCompleted { get; set; }

        // Returns summary of the task 
        public override string ToString()
        {
            // Format the reminder
            string reminder = ReminderDate.HasValue 
                ? $" (Reminder: {ReminderDate.Value.ToShortDateString()})" 
                : "";

            // Show whether the task is completed or pending
            string status = IsCompleted ? " Completed" : " Pending";

            // Combine the task summary
            return $"{Title} - {status}{reminder}\n{Description}";
        }
    }
}
//---------------------------------End of File---------------------------------//
