using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Simple_twitch_bot.Core
{
    class FileIO
    {
        public static ConfigParameters ReadConfigParameters(string filename)
        {
            using (StreamReader readtext = new StreamReader(filename))
            {
                string json = "";
                string temp;
                while ((temp = readtext.ReadLine()) != null)
                {
                    json += temp;
                }
                return JsonConvert.DeserializeObject<ConfigParameters>(json);
            }
        }

        public class ConfigParameters
        {
            public List<String> channels { get; set; }
            public string botName { get; set; }
            public string oauth { get; set; }
            public int port { get; set; }
            public string ip { get; set; }
        }
    }
}
