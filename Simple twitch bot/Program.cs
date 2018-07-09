using Simple_twitch_bot.Core;
using System;

namespace Simple_twitch_bot
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read config from file
            IRC irc = new IRC(FileIO.ReadConfigParameters("Config.json"));

            // Main loop
            while(true)
            {
                // Handle all the messages
                MessageHandler.HandleMessage(ref irc, irc.ReadMessage()); 
                
            }
        }
    }
}
