# BanTwithLurkerBots
Ban Twitch lurker bots, for streamer.bot

Action to be added to your Streamer.bot.

**Warning: make sure to read "How to setup" section and you have your whitelist file with the list of the bots you already use (just like streamelements, soundalerts, nightbot, etc.) as it may ban them**.

**Be advised that some messages are sent to the chat in order to let you know about the proccess, these messages can be modified in the BanBots action**

## What does it do?
- First it gets your list of viewers using the action "Get Current Viewers"
- Gets your whitelist file stated on the argument called %WhiteListBot%
- Gets the list of online lurkers bots from [http://twitchinsights.net/bots](http://twitchinsights.net/bots) It uses their api [https://api.twitchinsights.net/v1/bots/online](https://api.twitchinsights.net/v1/bots/online) (credit to them, thank you very much for the great work!).
- The code read the list of viewers, check if the viewer is on your whitelist, if it's not, it then check if the viewer is on twitchinsights list (lurker bot), if it's there, then ban the viewer.

## How to setup?
- Get the [BanBots (auto) file](https://github.com/ciskosv/BanTwithLurkerBots/blob/main/BanBots%20(auto))
- Import it to your streamer.bot
- Edit the argument %WhiteListBot% to set the path to your whitelist file.  The file must be a text file stating the name of the bots that you don't need to ban, one for each line.
  
![image](https://user-images.githubusercontent.com/494355/163449478-4918ac61-06b0-4021-a53f-214a2b410755.png)
- Edit the argument's field to show the messages in your chat.
- (This might not be needed in streamer.bot v0.1.8) Edit the sub-action **Execute code (Ban lurker bots)** and in the **References** tab right click and **add reference from file**, then look for **System.Linq.dll**, select it, click on open to add it.
- Make sure to click on compile and it compiles with no error.
- Run it as you want, optionally you can create a command to be run as you wish.
  
![image](https://github.com/ciskosv/BanTwithLurkerBots/assets/494355/d8491f01-6868-4db4-b4a0-ad625d0e17b3)

