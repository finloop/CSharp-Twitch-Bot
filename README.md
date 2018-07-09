# CSharp-Twitch-Bot
## Getting Started
To run a but you need to create a **config file**. It uses a json to store settings, that are applied when the application starts. Here's the example one below.

```json
{
  "channels": ["CHANNEL1", "CHANNEL2"],
  "botName": "USERNAME",
  "oauth": "oauth:YOUROAUTH",
  "port": 6667,
  "ip": "irc.twitch.tv"
}
```
The **config file** needs to be in yours application working directory. Also the default config file name is **Config.json**.

