using System;
using System.Windows;

//---------------------------------Start of File---------------------------------//
namespace CybersecurityChatbotGUI
{
    // Main application window for the GUI
    public partial class MainWindow : Window
    {
        // Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        // Opens the Task Manager window when the user clicks "Open Task Assistant"
        private void OpenTaskManager_Click(object sender, RoutedEventArgs e)
        {
            var taskWindow = new CybersecurityChatbot.TaskManagerWindow();
            taskWindow.ShowDialog();
        }

        // Opens the Chatbot interaction window when "Open Chatbot" is clicked
        private void OpenChatbot_Click(object sender, RoutedEventArgs e)
        {
            ChatbotWindow chat = new ChatbotWindow();
            chat.ShowDialog(); 
        }

        // Launches the cybersecurity quiz window when "Start Quiz" is clicked
        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            QuizWindow quiz = new QuizWindow();
            quiz.ShowDialog(); 
        }
    }
}
//---------------------------------End of File---------------------------------//

