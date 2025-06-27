using System;
using System.Windows;

namespace CybersecurityChatbot
{
    // WPF window that allows users to manage cybersecurity-related tasks via GUI
    public partial class TaskManagerWindow : Window
    {
        // Constructor Initializes the window and populates the task list
        public TaskManagerWindow()
        {
            InitializeComponent();
            RefreshTaskList();
        }

        // Refreshes the ListBox to display the latest list of tasks
        private void RefreshTaskList()
        {
            TaskListBox.Items.Clear();
            foreach (var task in TaskAssistant.GetTasks())
                TaskListBox.Items.Add(task);
        }

        // Handles the "Add Task" button click
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            // Validate user input for title and description
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                MessageBox.Show("Please enter both a title and description.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Create a new CyberTask with the provided input
            var task = new CyberTask
            {
                Title = TitleBox.Text,
                Description = DescriptionBox.Text,
                ReminderDate = ReminderDatePicker.SelectedDate,
                IsCompleted = false
            };

            // Add the task and refresh the task list UI
            TaskAssistant.AddTask(task);
            RefreshTaskList();

            // Clear input fields after adding
            TitleBox.Clear();
            DescriptionBox.Clear();
            ReminderDatePicker.SelectedDate = null;
        }

        // Handles the "Complete Task" button click
        private void CompleteTask_Click(object sender, RoutedEventArgs e)
        {
            // If a task is selected, mark it as completed
            if (TaskListBox.SelectedItem is CyberTask selected)
            {
                TaskAssistant.CompleteTask(selected);
                RefreshTaskList();
            }
        }

        // Handles the "Delete Task" button click
        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            // If a task is selected, remove it from the list
            if (TaskListBox.SelectedItem is CyberTask selected)
            {
                TaskAssistant.RemoveTask(selected);
                RefreshTaskList();
            }
        }
    }
}


