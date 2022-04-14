# BanTwithLurkerBots
Ban Twitch lurker bots, for streamer.bot

Action to be added to your Streamer.bot.

## What does it do?
- First it gets your list of viewers from https://tmi.twitch.tv/group/user/change-for-your-twitch-username/chatters
- Gets your whitelist file stated on the argument called %WhiteListBot%
- Gets the list of online lurkers bots from [http://twitchinsights.net/bots] It uses their api [https://api.twitchinsights.net/v1/bots/online] (credit to them, thank you very much for the great work!).
- The code read the list of viewers, check if the viewer is on your whitelist, if it's not, it then check if the viewer is on twitchinsights list (lurker bot), if it's there, then ban the viewer.

## How to setup?
- Get the **BanBots (auto) file**
- Import it to your streamer.bot
- Edit the "Fetch URL" sub-action to use your twitch name.
- Edit the argument %WhiteListBot% to set the path to your whitelist file.  The file must be a text file stating the name of the bots that you don't need to ban, one for each line.
- Edit the sub-action **Execute code (Ban lurker bots)** and in the **References** tab right click and **add reference from file**, then look for **System.Linq.dll**, select it, click on open to add it.
- Make sure to click on compile and it compiles with no error.
- Run it as you want, optionally you can create a command to be run as you wish.

![image](https://user-images.githubusercontent.com/494355/163444186-bd0c8061-b5f2-4494-b182-2f602261fac6.png)
