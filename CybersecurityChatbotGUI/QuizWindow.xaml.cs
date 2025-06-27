using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CybersecurityChatbotGUI
{
    public partial class QuizWindow : Window
    {
       
        private int currentQuestion = 0;

        // Track the user's score
        private int score = 0;

        // List of quiz questions and answers
        private List<QuizQuestion> questions = new List<QuizQuestion>
        {
            
            new QuizQuestion("What should you do if you receive an email asking for your password?",
                new[] { "A) Reply with your password", "B) Delete the email", "C) Report the email as phishing", "D) Ignore it" }, 2,
                "Correct! Reporting phishing emails helps prevent scams."),
            new QuizQuestion("True or False: It's okay to use the same password on multiple websites.",
                new[] { "True", "False" }, 1,
                "Correct! Reusing passwords puts all your accounts at risk."),
            new QuizQuestion("Which is the safest way to browse the internet?",
                new[] { "A) Click every pop-up", "B) Use incognito mode only", "C) Use a secure browser and avoid suspicious links", "D) Disable antivirus" }, 2,
                "Well done! Using a secure browser and staying cautious keeps you safer online."),
            new QuizQuestion("What is two-factor authentication?",
                new[] { "A) A backup email", "B) A second step of verifying identity", "C) Using your password twice", "D) A firewall setting" }, 1,
                "Correct! Two-factor authentication adds a layer of security."),
            new QuizQuestion("Which of these is a strong password?",
                new[] { "A) password123", "B) MyDog", "C) Qw!8z$Lp#", "D) 123456" }, 2,
                "Great choice! A strong password is long, random, and includes symbols."),
            new QuizQuestion("True or False: Public Wi-Fi is always safe if it doesn't ask for a password.",
                new[] { "True", "False" }, 1,
                "Right! Public Wi-Fi can be risky — use a VPN if possible."),
            new QuizQuestion("What should you do if a website seems suspicious?",
                new[] { "A) Enter fake info", "B) Leave the site", "C) Try to hack it", "D) Click everything to test" }, 1,
                "Correct! Leaving is the safest choice."),
            new QuizQuestion("Which is a sign of a phishing attempt?",
                new[] { "A) Grammatical errors", "B) Urgent messages", "C) Unfamiliar senders", "D) All of the above" }, 3,
                "Exactly! All of these are red flags."),
            new QuizQuestion("What's a good way to manage many different passwords?",
                new[] { "A) Write them on paper", "B) Use the same password", "C) Use a password manager", "D) Save them in Notepad" }, 2,
                "Correct! A password manager keeps them secure and organized."),
            new QuizQuestion("True or False: Antivirus software is unnecessary if you're careful online.",
                new[] { "True", "False" }, 1,
                "Wrong! Antivirus is an important safety net even for careful users.")
        };

        // Constructor loads the first question when the quiz window opens
        public QuizWindow()
        {
            InitializeComponent();
            LoadQuestion();
        }

        // Loads the current question into the UI
        private void LoadQuestion()
        {
            // If all questions have been answered, show the final score
            if (currentQuestion >= questions.Count)
            {
                ShowFinalScore();
                return;
            }

            // Otherwise, display the next question and clear any feedback
            var q = questions[currentQuestion];
            QuestionText.Text = $"Question {currentQuestion + 1}: {q.Question}";
            AnswerList.ItemsSource = q.Options;
            FeedbackText.Text = "";
        }

        // Called when a user selects an answer
        private void Answer_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                var selected = button.Content.ToString();
                int correctIndex = questions[currentQuestion].CorrectIndex;
                var correctAnswer = questions[currentQuestion].Options[correctIndex];

                // Check if selected answer is correct
                if (selected == correctAnswer)
                {
                    score++; // Increment score
                    FeedbackText.Text = questions[currentQuestion].Feedback; // Show positive feedback
                }
                else
                {
                    FeedbackText.Text = $" Incorrect. Correct answer: {correctAnswer}"; // Show correct answer
                }

                currentQuestion++; // Move to next question

                // Delay next question so user can read feedback
                Dispatcher.InvokeAsync(async () =>
                {
                    await System.Threading.Tasks.Task.Delay(2000);
                    LoadQuestion();
                });
            }
        }

        // Display final score and summary at the end of the quiz
        private void ShowFinalScore()
        {
            QuestionText.Text = $" Quiz Complete! You scored {score} out of {questions.Count}.";
            AnswerList.ItemsSource = null;

            if (score >= 8)
                FeedbackText.Text = "Great job! You're a cybersecurity pro!";
            else if (score >= 5)
                FeedbackText.Text = "Not bad! Keep learning to stay safe.";
            else
                FeedbackText.Text = "Keep practicing — cybersecurity is important!";
        }
    }

    // Represents a single quiz question with its options and correct answer
    public class QuizQuestion
    {
        public string Question { get; }
        public string[] Options { get; }
        public int CorrectIndex { get; }
        public string Feedback { get; }

        public QuizQuestion(string question, string[] options, int correctIndex, string feedback)
        {
            Question = question;
            Options = options;
            CorrectIndex = correctIndex;
            Feedback = feedback;
        }
    }
}


