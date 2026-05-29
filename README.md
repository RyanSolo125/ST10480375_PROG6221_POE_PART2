# ST10480375_PROG6221_POE_PART2
Cyber Crime Protection - ST10480375 PROG6221 POE Part 2

## Part 2 — WPF GUI Application

### New Features Added
- Full WPF graphical user interface with dark cybersecurity theme
- All Part 1 features into the GUI (voice greeting, ASCII logo, topics)
- Keyword recognition for topics
- Random responses — multiple answers per topic using a List
- Conversation - say "tell me more" or "another tip" to continue a topic
- Memory and recall — chatbot remembers your name, favourite topic, and last topic discussed
- Sentiment detection — detects worried, curious, and frustrated moods and adjusts responses
- Error handling — default responses for unrecognised input, no crashes on unexpected input
- Quick topic buttons for fast answers
- memory bar showing stored user information

### How to Use
1. Open the project in Visual Studio
2. Build and run
3. Enter your name when prompted
4. Type a question or click a quick topic button
5. Say "tell me more" to get another tip on the same topic
6. Say "my name is ..." or "I'm interested in ..." to personalise the conversation
7. Say "what do you remember about me?" to see stored memory
8. Type "help" to see all available topics

### Prompt Topics
- password
- phishing
- malware
- ransomware
- spyware
- virus / worms / macros
- safe browsing
- 2FA
- firewall
- antivirus
- privacy
- scam

### Project Structure
ST10480375_PROG6221_POE_PART_2/
├── Memory.cs            — stores user name, favourite topic, last topic
├── Chatbot.cs           — all chatbot main code, keyword detection, responses
├── MainWindow.xaml      — WPF GUI layout
├── MainWindow.xaml.cs   — input and displays messages
└── README.md


Youtube link: https://youtu.be/DlUgY8x4M_k
