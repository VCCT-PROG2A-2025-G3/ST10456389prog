using System;
using System.Collections.Generic;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbot
{
    // Manages a shared list of cybersecurity-related tasks
    public static class TaskAssistant
    {
        // List that stores all added tasks
        private static readonly List<CyberTask> tasks = new List<CyberTask>();

        // Adds a new task to the task list
        public static void AddTask(CyberTask task) => tasks.Add(task);

        // Returns a copy of the current task list 
        public static List<CyberTask> GetTasks() => new List<CyberTask>(tasks);

        // Marks a specific task as completed
        public static void CompleteTask(CyberTask task) => task.IsCompleted = true;

        // Removes a task from the task list
        public static void RemoveTask(CyberTask task) => tasks.Remove(task);
    }
}
//---------------------------------End of File---------------------------------//
