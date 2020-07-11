AIOMiner ver: 07.11.2020

- Updated for version 8.6.2 with API access
-  Still will not download miners unless you manually update the Settings/Miners.json and Settings/MinerProcessInfo.json

- Version is now the date the code was comiled for release to public  exp: 07.11.2020 

AioSettings.vb

-  Removed hard coded items that point to aiominer.com,  Created variables that anyone can use to put in thier own data.  Right now they are blank until the test stie/app is online for testing purposes. 
```
   Website/API
    'Change to your own items later
    Public Shared API_LOCATION As String = ""
    Public Shared WEB_VERSION As String = ""
    Public Shared WEB_MINER_VERSION As String = ""
    Public Shared WEB_DISCORD_LINK As String = ""
    Public Shared WEB_DEFAULT_COINS_POOLS_JSON As String = ""
    Public Shared WEB_GITHUB_LINK As String = ""
    Public Shared WEB_WIKI_LINK_UPDATES As String = ""
    Public Shared WEB_WIKI_LINK_MINER_UPDATES As String = ""
    Public Shared WEB_REDDIT_LINK As String = ""

    'Your Hosted Items
    Public Shared HOSTED_DATA_STORE As String = "" 'example https://aiominer.com/files  do not include the / at the end, but this is where your files are stored for images etc
    Public Shared HOSTED_WEBSITE As String = "" ' example https://aiominer.com
```


---

Notes for the release
---
```
PubShared - Items to hold into memory for access later on, a cache spot.  They are Publicy Shared
PubShared.API_LOCATION = https://sassy.aiominer.com/api
\Settings\AIOSettings.json - Controls most settings in AIOMiner, can be restored from backup to a new machine
\Settings\MinerProcessInfo.json - Controls Mining processes for the applications.  Determines what varables are passed in the p.shell
\Settings\AIOMiner_Default.json - Default Pools and coin list loaded from here

\Settings\Updates\\ - Folder location for new settings files to be downloaded 
\Settings\Backups\\ - Folder location to place settings files into from a manual or automated backup.  


PubShared

Version - Controls what version you are on current, set in SplashScreen1, hard coded



Start Phase:

SplashPage1.vb -
  Confirm mining files exist
  Check API for registration
  Add new items to Settings files
	exp: If 
 		AddNewSettings("minerversion", "0.0.0.1") = "added" Then
                Label1.Text = "Checking under the bed for monsters!"
             End If



Main Phase:

Aiominer
   Start Timer *
   Confirm if we should be sending data to the api
   Retrieve settings from settings file, set GUI checkmarks etc 
   Enabled Start Button
   Setup Logs Location
   Enabled GPU/List views
   Enable Marquee **
   Change Mining to Donation (Make sure it's not blank)
   Confirm no other mining application that we have is running right now, kill any that are running
   Check if it's the users first time runing AIOMiner, show WELCOME screen
   If user != Subscriber, set PubShared AdvertiseShowing
   Check Timed Mining
   Check GPU's in the system, gather static data to individual GraphicsCards PubShared.  This is a hard coded nightmare...why did I go this route.  past me was a jerk. TODO
   Set GPU Count
   Refresh Coins List from Default settings file
   Start Background Workers
      ** Variable names here are pretty bad TODO**

	My_BgWorker.DoWork - Can't find out wtf is this is doing, links back to a Button2_Click that doesn't exist, and the code is all comented out, talked about loading prices	
	 My_BgWorker5.DoWork - Handles with Progress Changed - Runs Async - Checks for Updates
         
          - Incomplete, Pending pick back up

```

---
Overall - Workable, needs attention to load process and file/folder locations
---