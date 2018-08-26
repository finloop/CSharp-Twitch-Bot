using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Simple_twitch_bot.Core
{
    class IRC
    {
        private List<String> channels;
        private string botName;
        private string oauth;
        private int port;
        private string ip;

        private TcpClient tcpClient = new TcpClient();
        private StreamReader reader;
        private StreamWriter writer;

        ///<summary>Twitch IRC (chat) client.</summary>
        ///<para name="_channels">List of channels to join</para>
        ///<para name="_botName">Bot's username</para>
        ///<para name="_oauth">Your oauth Twitch token</para>
        ///<para name="_port">Port</para>
        ///<para name="_ip">Ip adress</para>
        public IRC(List<String> _channels, string _botName, string _oauth, int _port, string _ip)
        {
            channels = _channels;
            botName = _botName;
            oauth = _oauth;
            port = _port;
            ip = _ip;
        }

        ///<summary>Twitch IRC (chat) client.</summary>
        public IRC(FileIO.ConfigParameters parameters)
        {
            channels = parameters.channels;
            botName = parameters.botName;
            oauth = parameters.oauth;
            port = parameters.port;
            ip = parameters.ip;
        }
        private void Connect()
        {
            try
            {
                tcpClient = new TcpClient(ip, port);
                reader = new StreamReader(tcpClient.GetStream());
                writer = new StreamWriter(tcpClient.GetStream());

                writer.WriteLine("PASS " + oauth);
                writer.WriteLine("NICK " + botName);
                writer.WriteLine("USER " + botName + " 8 * :" + botName);

                for (int i = 0; i < channels.Count; i++)
                {
                    writer.WriteLine("JOIN #" + channels[i]);
                }

                // JOIN WHISPERS CHANNEL
                writer.WriteLine("CAP REQ :twitch.tv/commands");

                writer.Flush();

            } catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        ///<summary>Use this to join channel.</summary>
        ///<para name="channel">The name of channel you want to join</para>
        public void ConnectTo(string channel)
        {
            if(tcpClient.Connected)
            {
                SendRawMessage("JOIN #" + channel);
                channels.Add(channel);
            }
        }

        ///<summary>Sends a raw message to the server.</summary>
        ///<para name="message">Message you want to send.</para>
        public void SendRawMessage(string message)
        {
            try
            {
                writer.WriteLine(message);
                writer.Flush();
            }
            catch (Exception ex)
            {
                Connect();
            }
        }

        ///<summary>Sends a message to specified channel.</summary>
        ///<para name="message">Message you want to send.</para>
        ///<para name="channel">Channel you want to send message to.</para>
        public void SendChatMessage(string channel, string message)
        {
            try
            {
                SendRawMessage(":" + botName + "!" + botName + "@" + botName +
                ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
            }
            catch (Exception ex)
            {
                Connect();
            }
        }
        ///<summary>Sends a whisper to specified reciever.</summary>
        ///<para name="message">Message you want to send.</para>
        ///<para name="reciever">Person/Account you want to send message to.</para>
        public void SendWhisper(string reciever, string message)
        {
            try
            { // PRIVMSG #jtv :/w [TargetUser][whitespace][message]
                SendRawMessage("PRIVMSG jtv :/w " + reciever + " " + message);
            }
            catch (Exception ex)
            {
                Connect();
            }
        }
        ///<summary>Reads message from twitch server.</summary>
        ///<returns>Returns raw message from the server.</returns>
        public string ReadMessage()
        {
            try
            {
                string message = reader.ReadLine();
                if (message.Equals("PING :tmi.twitch.tv"))
                {
                    SendRawMessage("PONG :tmi.twitch.tv");
                    return "PONG";
                }               
                return message;
            }
            catch (Exception ex)
            {
                Connect();
                return "ERROR";
            }
        }

    }
}
