using System;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using ST10480375_PROG6221_POE_PART_2;

namespace ST10480375_PROG6221_POE_PART_2
{
    public partial class MainWindow : Window
    {
        private Chatbot chatbot = new Chatbot();
        private bool awaitingName = true;

        public MainWindow()
        {
            InitializeComponent();
            PlayVoice();
            AddBotMessage("Welcome to CCP - Cyber Crime Protection!\n" +
                "██████╗ ██████╗ ██████╗\r\n██╔════╝██╔════╝██╔══██╗\r\n██║     ██║     ██████╔╝\r\n██║     ██║     ██╔═══╝\r\n╚██████╗╚██████╗██║\r\n╚═════╝ ╚═════╝╚═╝\r\n[ CYBER CRIME PROTECTION ]\r\n\r\n\r\n\r\nSecure Your Digital Life <");
            AddBotMessage("Before we start, what is your name?");
        }

        // runs when Send button is clicked
        private void SendBtn_Click(object sender, RoutedEventArgs e)
        {
            HandleSend();
        }

        // runs when Enter key is pressed in the input box
        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                HandleSend();
        }

        // runs when any quick-topic button is clicked
        private void QuickBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            InputBox.Text = btn.Content.ToString();
            HandleSend();
        }

        private void HandleSend()
        {
            string userText = InputBox.Text.Trim();
            if (userText == "") return;

            InputBox.Clear();
            AddUserMessage(userText);

            // first message is always the user's name
            if (awaitingName)
            {
                awaitingName = false;
                string name = char.ToUpper(userText[0]) + userText.Substring(1).ToLower();
                chatbot.GetMemory().UserName = name;
                UpdateMemoryBar();
                AddBotMessage("Nice to meet you, " + name + "! Type 'help' to see what I can do, or just ask me anything.");
                return;
            }

            // get response from chatbot and display it
            string response = chatbot.GetResponse(userText);
            UpdateMemoryBar();
            AddBotMessage(response);
        }

        // adds a user message to the chat in yellow
        private void AddUserMessage(string text)
        {
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Run("You: ") { Foreground = Brushes.Yellow, FontWeight = FontWeights.Bold });
            para.Inlines.Add(new Run(text) { Foreground = Brushes.White });
            ChatBox.Document.Blocks.Add(para);
            ChatScroller.ScrollToBottom();
        }

        // adds a bot message to the chat in cyan blue
        private void AddBotMessage(string text)
        {
            Paragraph para = new Paragraph();
            para.Inlines.Add(new Run("Bot: ") { Foreground = Brushes.Cyan, FontWeight = FontWeights.Bold });
            para.Inlines.Add(new Run(text) { Foreground = Brushes.LightGray });
            ChatBox.Document.Blocks.Add(para);
            ChatScroller.ScrollToBottom();
        }

        // updates the memory bar at the top with stored user info
        private void UpdateMemoryBar()
        {
            Memory mem = chatbot.GetMemory();
            string name = mem.UserName ?? "—";
            string interest = mem.FavouriteTopic ?? "—";
            string last = mem.LastTopic ?? "—";
            MemoryLabel.Text = "Memory:  Name: " + name + "   |   Interest: " + interest + "   |   Last Topic: " + last;
        }

        private void PlayVoice()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("welcome.wav");
                player.Play();
            }
            catch
            {
                // no voice file found then continue without it
            }
        }
    }
}