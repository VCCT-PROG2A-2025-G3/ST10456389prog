using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CybersecurityChatbot;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbotGUI
{
    public partial class ChatbotWindow : Window
    {
        private string lastTopic = "";
        private string userInterest = "";
        private string pendingTaskTitle = "";
        private string pendingTaskDescription = "";
        private bool waitingForReminder = false;

        private string ExtractTaskFromReminder(string input)
        {
            // Extracts what's after "remind me to" or "set a reminder to"
            if (input.Contains("remind me to"))
                return input.Substring(input.IndexOf("remind me to") + 12).Trim();
            if (input.Contains("set a reminder to"))
                return input.Substring(input.IndexOf("set a reminder to") + 18).Trim();
            if (input.Contains("set a reminder for"))
                return input.Substring(input.IndexOf("set a reminder for") + 21).Trim();

            return "Unnamed Task";
        }

        private string ExtractTaskFromAddition(string input)
        {
            if (input.Contains("add a task to"))
                return input.Substring(input.IndexOf("add a task to") + 13).Trim();
            if (input.Contains("add a task for"))
                return input.Substring(input.IndexOf("add a task for") + 15).Trim();
            if (input.Contains("create a task"))
                return input.Substring(input.IndexOf("create a task") + 13).Trim();

            return "Unnamed Task";
        }

        public ChatbotWindow()
        {
            InitializeComponent();

            // Set the ASCII logo
            AsciiLogoBlock.Text = @"
              +---------------+
              |.-------------.|
              ||Cybersecurity||
              ||Chatbot      ||
              ||             ||
              ||             ||
              |+-------------+|
              +-..--------..--+
              .---------------.
             / /=============\ \
            / /===============\ \
           /_____________________\
           \_____________________/
            ";

            // Play greeting sound
            VoicePlayer.PlayVoiceGreeting();

            // Welcome message
            AddMessage(
            "Welcome to the Cybersecurity Chatbot!\n" +
            "You can ask me about: passwords, phishing, scams, privacy.\n" +
            "You can also: add tasks, set reminders, view tasks, start the quiz, or check the activity log.\n" +
            "Type your request naturally and I'll do my best to help you stay safe online.",
             isBot: true
            );


        }


        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string input = UserInputBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(input)) return;

            AddMessage("You: " + input, isBot: false);
            ProcessInput(input.ToLower());

            UserInputBox.Clear();
        }

        private void AddMessage(string message, bool isBot)
        {
            var text = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(5),
                Foreground = isBot ? System.Windows.Media.Brushes.DarkBlue : System.Windows.Media.Brushes.Black
            };

            ChatPanel.Children.Add(text);
        }

        private void ProcessInput(string input)
        {
            input = input.Trim().ToLower();

            // Activity log
            if (input.Contains("what have you done for me") || input.Contains("recent actions") || input.Contains("activity summary"))
            {
                var log = ActivityLog.GetLog();
                if (log.Count == 0)
                    AddMessage("📄 I haven't done much for you yet. Try adding a task or setting a reminder.", true);
                else
                {
                    AddMessage("📄 Here's a summary of recent actions:", true);
                    int count = 1;
                    foreach (var entry in log)
                    {
                        AddMessage($"{count++}. {entry}", true);
                    }
                }
                return;
            }

            // Sentiment detection
            string sentiment = SentimentHandler.DetectSentiment(input);
            if (!string.IsNullOrEmpty(sentiment))
            {
                AddMessage(sentiment, true);
                return;
            }

            // Activity log command
            if (input.Contains("show activity log") || input.Contains("activity log"))
            {
                var log = ActivityLog.GetLog();
                if (log.Count == 0)
                    AddMessage("📄 No activity recorded yet.", true);
                else
                {
                    AddMessage("📄 Activity Log:", true);
                    foreach (var entry in log)
                        AddMessage(entry, true);
                }
                return;
            }

            // Handle reminder follow-up
            if (waitingForReminder)
            {
                if (input.Contains("remind me in") && int.TryParse(new string(input.Where(char.IsDigit).ToArray()), out int days))
                {
                    DateTime reminderDate = DateTime.Now.AddDays(days);

                    TaskAssistant.AddTask(new CyberTask
                    {
                        Title = pendingTaskTitle,
                        Description = pendingTaskDescription,
                        ReminderDate = reminderDate,
                        IsCompleted = false
                    });

                    ActivityLog.Add($"Reminder set: '{pendingTaskTitle}' on {reminderDate.ToShortDateString()}"); // ✅ Log reminder

                    AddMessage($"✅ Got it! I'll remind you in {days} days.", true);

                    // Reset 
                    pendingTaskTitle = "";
                    pendingTaskDescription = "";
                    waitingForReminder = false;
                }
                else
                {
                    AddMessage("I didn't understand. Please say 'remind me in X days' or 'no reminder'.", true);
                }
                return;
            }

            // NLP: Flexible reminder phrasing
            if (input.Contains("remind me to") || input.Contains("set a reminder to") || input.Contains("set a reminder for"))
            {
                string taskTitle = ExtractTaskFromReminder(input);
                DateTime reminderDate = input.Contains("tomorrow") ? DateTime.Now.AddDays(1) : DateTime.Now.AddDays(3);

                TaskAssistant.AddTask(new CyberTask
                {
                    Title = taskTitle,
                    Description = $"Reminder: {taskTitle}",
                    ReminderDate = reminderDate,
                    IsCompleted = false
                });

                ActivityLog.Add($"Reminder set: '{taskTitle}' on {reminderDate:dd/MM/yyyy}");

                AddMessage($"Reminder set for '{taskTitle}' on {reminderDate:dd MMM yyyy}. ✅", true);
                return;
            }

            // NLP: Add task to something
            if (input.Contains("add a task to") || input.Contains("add a task for") || input.Contains("create a task"))
            {
                string taskTitle = ExtractTaskFromAddition(input);
                TaskAssistant.AddTask(new CyberTask
                {
                    Title = taskTitle,
                    Description = $"Task: {taskTitle}",
                    ReminderDate = null,
                    IsCompleted = false
                });

                ActivityLog.Add($"Task added: '{taskTitle}'");

                AddMessage($"Task added: '{taskTitle}'. Would you like to set a reminder for this task?", true);

                // Prep for follow up reminder
                pendingTaskTitle = taskTitle;
                pendingTaskDescription = $"Task: {taskTitle}";
                waitingForReminder = true;

                return;
            }

            // NLP: Trigger quiz
            if (input.Contains("start the quiz") || input.Contains("quiz about") || input.Contains("test me") || input.Contains("begin the quiz") || input.Contains("start a quiz"))
            {
                AddMessage("Opening the cybersecurity quiz... ", true);
                ActivityLog.Add("Quiz started"); // ✅ Log quiz
                QuizWindow quiz = new QuizWindow();
                quiz.ShowDialog();
                return;
            }

            if (input.StartsWith("add task"))
            {
                // Remove "add task" from the start
                string raw = input.Substring("add task".Length).TrimStart();

                // Remove common separators like '-' or ':' if directly following
                if (raw.StartsWith("-") || raw.StartsWith(":"))
                    raw = raw.Substring(1).TrimStart();

                string taskTitle = raw;

                // Input validation
                if (string.IsNullOrWhiteSpace(taskTitle) || taskTitle.Length < 3)
                {
                    AddMessage("❗ Please include a clear task title after 'add task'.", true);
                    return;
                }

                if (taskTitle.Length > 100)
                {
                    AddMessage("❗ That task title is too long. Please keep it under 100 characters.", true);
                    return;
                }

                // Normalize the title
                taskTitle = char.ToUpper(taskTitle[0]) + taskTitle.Substring(1);

                // Contextual default description
                string defaultDescription = "Cybersecurity task to stay safe online.";

                if (taskTitle.ToLower().Contains("privacy"))
                    defaultDescription = "Review your account privacy settings.";
                else if (taskTitle.ToLower().Contains("password"))
                    defaultDescription = "Update your passwords and enable two-factor authentication.";
                else if (taskTitle.ToLower().Contains("2fa") || taskTitle.ToLower().Contains("authentication"))
                    defaultDescription = "Enable two-factor authentication for added security.";

                // Set task state
                pendingTaskTitle = taskTitle;
                pendingTaskDescription = defaultDescription;
                waitingForReminder = true;

                ActivityLog.Add($"Task added: '{taskTitle}'");

                AddMessage($"Task added: \"{defaultDescription}\".\nWould you like to set a reminder for this task?", true);
                return;
            }


            // --- Regular chatbot logic (unchanged) ---
            if (CyberTipManager.IsFollowUp(input))
            {
                string moreInfo = CyberTipManager.GetMoreInfo(lastTopic);
                AddMessage(moreInfo, true);

            }
            else if (input.Contains("how are you"))
            {
                AddMessage("I'm feeling cyber-secure, thanks for asking!", true);
            }
            else if (input.Contains("purpose"))
            {
                AddMessage("My purpose is to help you stay safe online!", true);
            }
            else if (input.Contains("what can i ask"))
            {
                AddMessage("Ask me about phishing, passwords, scams, or privacy!", true);
            }
            else if (input.Contains("my name is "))
            {
                var name = input.Substring(input.IndexOf("my name is ") + 11).Trim();
                AddMessage($"Nice to meet you, {name}!", true);
            }
            else if (input.Contains("i'm interested in "))
            {
                int startIndex = input.IndexOf("i'm interested in ") + 18;
                userInterest = input.Substring(startIndex).Trim();
                AddMessage($"Got it! You're interested in {userInterest}.", true);
            }
            else if (input.Contains("what should i do") && !string.IsNullOrEmpty(userInterest))
            {
                AddMessage($"Since you're interested in {userInterest}, review your account security settings.", true);
            }
            else if (input.Contains("show tasks"))
            {
                var tasks = TaskAssistant.GetTasks();
                if (tasks.Count == 0)
                    AddMessage("You have no tasks at the moment.", true);
                else
                {
                    foreach (var task in tasks)
                    {
                        string reminder = task.ReminderDate.HasValue ? $"Reminder: {task.ReminderDate.Value.ToShortDateString()}" : "No reminder";
                        string status = task.IsCompleted ? "Completed" : "Pending";
                        AddMessage($"{task.Title} - {status}\n{task.Description}\n{reminder}", true);
                    }
                }
            }
            else if (input.Contains("exit"))
            {
                AddMessage("Goodbye! Stay safe online.", true);
                Close();
            }
            else
            {
                bool matched = false;
                foreach (var topic in CyberTipManager.GetTopics())
                {
                    if (input.Contains(topic))
                    {
                        lastTopic = topic;
                        var tip = CyberTipManager.GetRandomTip(topic);
                        AddMessage($"{CyberTipManager.GetTopicEmoji(topic)} {CyberTipManager.Capitalize(topic)} Tip: {tip}", true);
                        matched = true;
                        break;
                    }
                }

                if (!matched)
                {
                    AddMessage("Hmm... I didn’t catch that. Try asking about phishing, scams, or privacy.", true);
                }
            }
        }
    }
}
//---------------------------------End of File---------------------------------//


