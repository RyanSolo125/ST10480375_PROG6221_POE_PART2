using System;
using System.Collections.Generic;
using ST10480375_PROG6221_POE_PART_2;

namespace ST10480375_PROG6221_POE_PART_2
{
    public class Chatbot
    {
        private Memory memory = new Memory();
        private Random random = new Random();

        // stores multiple responses per topic for random selection
        private Dictionary<string, List<string>> responses = new Dictionary<string, List<string>>()
        {
            { "password", new List<string> {
                "Use strong passwords with letters, numbers and symbols. Never reuse them.",
                "Never share your password with anyone, not even IT support.",
                "Use a password manager to keep track of strong unique passwords."
            }},
            { "phishing", new List<string> {
                "Phishing tricks you into giving personal info. Always check the sender's email.",
                "Be cautious of urgent emails asking you to click links or enter details.",
                "Hover over links before clicking to see where they really go."
            }},
            { "malware", new List<string> {
                "Malware is harmful software. Keep your antivirus updated to stay protected.",
                "Signs of malware include slow performance and unexpected pop-ups.",
                "Only download software from trusted, official sources."
            }},
            { "ransomware", new List<string> {
                "Ransomware locks your files and demands payment. Never pay — back up your files instead.",
                "Regular backups are the best defence against ransomware attacks.",
                "Ransomware usually arrives through phishing emails or unsafe downloads."
            }},
            { "privacy", new List<string> {
                "Check your social media privacy settings and limit what strangers can see.",
                "Use a VPN on public Wi-Fi to protect your personal data.",
                "Be careful what personal information you share online."
            }},
            { "scam", new List<string> {
                "If something looks too good to be true, it probably is a scam.",
                "Scammers pretend to be banks or government offices. Always verify by calling back.",
                "Never pay anyone using gift cards — this is always a scam."
            }},
            { "firewall", new List<string> {
                "A firewall blocks unauthorised access to your computer or network.",
                "Always keep your firewall turned on, even at home.",
                "Firewalls work best when combined with antivirus software."
            }},
            { "antivirus", new List<string> {
                "Antivirus software detects and removes harmful programs from your device.",
                "Keep your antivirus updated so it can catch the latest threats.",
                "Run regular scans to make sure your device stays clean."
            }},
            { "2fa", new List<string> {
                "Two-factor authentication means even a stolen password won't let attackers in.",
                "Use an authenticator app for 2FA instead of SMS — it is more secure.",
                "Turn on 2FA for every account that supports it, especially email."
            }},
            { "virus", new List<string> {
                "A virus attaches to programs and spreads when you run them.",
                "Avoid downloading files from unknown or untrusted websites.",
                "Keep your operating system updated to patch security holes viruses exploit."
            }},
            { "spyware", new List<string> {
                "Spyware secretly records your activity and sends it to attackers.",
                "Be careful installing free apps — many contain hidden spyware.",
                "A good antivirus with real-time protection can block spyware."
            }},
            { "safe browsing", new List<string> {
                "Always check for HTTPS and a padlock before entering personal details.",
                "Avoid clicking on pop-up ads — they can lead to malicious sites.",
                "Use a browser extension to block harmful ads and tracking scripts."
            }}
        };

        public Memory GetMemory()
        {
            return memory;
        }

        public string GetResponse(string userInput)
        {
            string input = userInput.ToLower();

            // detect sentiment first
            string sentimentNote = DetectSentiment(input);
            bool hasSentiment = sentimentNote != "";

            // check if user is telling us their name
            if (input.Contains("my name is"))
            {
                string name = input.Replace("my name is", "").Trim();
                name = char.ToUpper(name[0]) + name.Substring(1);
                memory.UserName = name;
                return "Nice to meet you, " + name + "! How can I help you stay safe online?";
            }

            // check if user is sharing a topic interest
            if (input.Contains("i am interested in") || input.Contains("i'm interested in"))
            {
                string topic = input.Replace("i'm interested in", "").Replace("i am interested in", "").Trim();
                memory.FavouriteTopic = topic;
                return "Great! I'll remember that you're interested in " + topic + ". It's an important area of cybersecurity!";
            }

            // recall what we remember about the user
            if (input.Contains("what do you remember") || input.Contains("what do you know about me"))
            {
                return GetMemorySummary();
            }

            // follow-up: give another tip on the same topic
            if (input.Contains("tell me more") || input.Contains("another tip") || input.Contains("more info"))
            {
                if (memory.LastTopic != null)
                {
                    string tip = GetRandomResponse(memory.LastTopic);
                    return sentimentNote + tip;
                }
                return "What topic would you like more information on? Try asking about phishing or passwords.";
            }

            // help menu
            if (input.Contains("help"))
            {
                return "You can ask me about:\n- password\n- phishing\n- malware\n- ransomware\n- privacy\n- scam\n- firewall\n- antivirus\n- 2fa\n- virus\n- spyware\n- safe browsing\n\nYou can also say 'my name is ...' or 'I'm interested in ...'";
            }

            if (input.Contains("how are you"))
            {
                return "I'm doing great and ready to help you stay safe online!";
            }

            if (input.Contains("purpose") || input.Contains("who are you"))
            {
                return "I'm CCP, your Cyber Crime Protection chatbot. I help you learn how to stay safe online.";
            }

            // check each keyword and return a random matching response
            foreach (string keyword in responses.Keys)
            {
                if (input.Contains(keyword))
                {
                    memory.LastTopic = keyword;
                    string tip = GetRandomResponse(keyword);
                    return sentimentNote + tip;
                }
            }

            // if sentiment was detected but no keyword matched, still respond empathetically
            // and give a tip based on the last topic or ask what they need help with
            if (hasSentiment)
            {
                if (memory.LastTopic != null)
                {
                    string tip = GetRandomResponse(memory.LastTopic);
                    return sentimentNote + tip;
                }
                return sentimentNote + "I'm here to help. You can ask me about phishing, passwords, scams, malware and more. Type 'help' to see all topics.";
            }

            // default fallback if nothing matched
            return "I'm not sure I understand. Type 'help' to see what I can assist with.";
        }


        // picks a random response from the list for a topic
        private string GetRandomResponse(string topic)
        {
            List<string> options = responses[topic];
            int index = random.Next(options.Count);
            return options[index];
        }

        // detects the user's mood and adds an empathy
        private string DetectSentiment(string input)
        {
            // worried / scared
            if (input.Contains("worried") || input.Contains("scared") ||
                input.Contains("anxious") || input.Contains("nervous") ||
                input.Contains("afraid") || input.Contains("unsafe"))
            {
                return "It's completely understandable to feel that way. " +
                       "Scammers and cybercriminals can be very convincing. " +
                       "Let me share something that can help:\n";
            }

            // frustrated / confused
            if (input.Contains("frustrated") || input.Contains("confused") ||
                input.Contains("don't understand") || input.Contains("complicated") ||
                input.Contains("difficult") || input.Contains("too hard"))
            {
                return "I hear you — cybersecurity can feel overwhelming at first. " +
                       "Let me break it down simply:\n";
            }

            // curious
            if (input.Contains("curious") || input.Contains("interesting") ||
                input.Contains("want to know") || input.Contains("tell me about"))
            {
                return "Great curiosity — learning about this is the first step to staying safe!\n";
            }

            return "";
        }


        // builds a sentence from what we remember about the user
        private string GetMemorySummary()
        {
            if (memory.UserName == null && memory.FavouriteTopic == null && memory.LastTopic == null)
                return "I don't know much about you yet! Try saying 'my name is ...' or 'I'm interested in ...'";

            string result = "Here's what I remember: ";
            if (memory.UserName != null) result += "Your name is " + memory.UserName + ". ";
            if (memory.FavouriteTopic != null) result += "You're interested in " + memory.FavouriteTopic + ". ";
            if (memory.LastTopic != null) result += "We last talked about " + memory.LastTopic + ".";
            return result;
        }
    }
}