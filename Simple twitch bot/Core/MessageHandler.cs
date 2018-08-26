using System;
using System.Collections.Generic;
using System.Text;

namespace Simple_twitch_bot.Core
{
    class MessageHandler
    {
        static public void HandleMessage(IRC irc, string message)
        {
            Console.WriteLine(message);

            if (message.Contains("PRIVMSG") | message.Contains("WHISPER"))
            {
                string[] res = StringTokenizer.TokenizeChatMsg(message);

                string sender = res[0];
                string channel = res[1];
                string msg = res[2];

                if (msg.StartsWith("!hello"))
                {
                    irc.SendChatMessage(channel, "Hello World!");
                }

            }
        }
    }
}
