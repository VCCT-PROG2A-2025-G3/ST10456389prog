# Cybersecurity Chatbot GUI - WPF Application

## Overview
The Cybersecurity Chatbot GUI is a WPF-based desktop application developed in C# (.NET Framework 4.8). It simulates an intelligent assistant that educates users on cybersecurity topics such as phishing, strong passwords, scams, and privacy. The chatbot includes task management, a cybersecurity quiz, sentiment recognition, keyword-based NLP simulation, and an activity log.

---

## Features

### Chatbot (Main Feature)
- Accepts natural language input.
- Provides tips on cybersecurity topics.
- Detects sentiment in user input (e.g., worried, confused).
- Responds flexibly using keyword detection and simulated NLP.

### Task Assistant
- Add tasks with title, description, and optional reminders.
- View all current tasks in a list.
- Mark tasks as completed or delete them.
- Fully accessible from the GUI or via chatbot commands.

### Reminder Functionality
- Set reminders using phrases like “remind me to update password tomorrow”.
- Reminder dates are stored and shown next to tasks.

### Cybersecurity Quiz
- 10 questions: mix of multiple-choice and true/false.
- Instant feedback on correct and incorrect answers.
- Final score summary and encouragement.

### Activity Log
- Keeps track of key user actions:
  - Tasks added
  - Reminders set
  - Quiz activity
- View log with commands like “What have you done for me?” or “Show activity log”.

### Sentiment Detection
- Recognizes emotional inputs (e.g., “I’m anxious”, “I’m confused”).
- Responds with friendly, supportive messages.

---

## Technologies Used

- **C# (.NET Framework 4.8)**  
  Used for writing the application's logic and integrating core functionality like task management, reminders, and chatbot responses.

- **WPF (Windows Presentation Foundation)**  
  Provides the graphical user interface for the chatbot, task assistant, and quiz modules using XAML for layout.

- **XAML**  
  Enables declarative design of the interface components such as windows, buttons, textboxes, and layout grids.

- **System.Media (SoundPlayer)**  
  Used to play the greeting WAV audio file as part of the introductory experience.

- **LINQ (Language Integrated Query)**  
  Utilized for filtering, transforming, and querying task data and chatbot inputs.

- **Event-Driven Programming**  
  Handles user interactions such as button clicks, task submissions, and quiz answers using event listeners.

- **Custom Classes & Utilities**  
  Includes reusable modules like:
  - CyberTask – Represents each task with title, description, status, and reminder.
  - TaskAssistant – Manages all task-related logic.
  - ActivityLog – Records significant user and chatbot actions.
  - SentimentHandler – Detects and responds to emotional user input.
  - CyberTipManager – Stores and serves topic-specific security tips.
  - VoicePlayer – Plays welcome audio (console version).
  - LogoDisplay – Displays ASCII art branding (console version).

---

## 🖥️ Running the Application

1. **Ensure you are using Visual Studio with .NET Framework 4.8.**
2. **Build the solution**
3. **Run the project.**
   - **Open Chatbot**
   - **Open Task Assistant**
   - **Start Quiz**

---

