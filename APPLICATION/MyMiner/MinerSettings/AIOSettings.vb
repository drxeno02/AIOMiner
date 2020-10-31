Imports Newtonsoft.Json
Imports AIOminer.General_Utils

Public Class PubShared

    'Website/API
    'Change to your own items later
    Public Shared API_LOCATION As String = ReturnAIOsetting("apilocation")
    Public Shared WEB_VERSION As String = ReturnAIOsetting("web_version")
    Public Shared WEB_MINER_VERSION As String = ReturnAIOsetting("web_miner_version")
    Public Shared WEB_DISCORD_LINK As String = ReturnAIOsetting("web_discord_link")
    Public Shared WEB_DEFAULT_COINS_POOLS_JSON As String = ReturnAIOsetting("web_default_coins_pools_json")
    Public Shared WEB_GITHUB_LINK As String = ReturnAIOsetting("web_github_link")
    Public Shared WEB_WIKI_LINK_UPDATES As String = ReturnAIOsetting("web_wiki_link_updates")
    Public Shared WEB_WIKI_LINK_MINER_UPDATES As String = ReturnAIOsetting("web_wiki_link_miner_updates")
    Public Shared WEB_REDDIT_LINK As String = ReturnAIOsetting("web_reddit_link")

    'Your Hosted Items
    Public Shared HOSTED_DATA_STORE As String = ReturnAIOsetting("hosted_data_store") 'example https://aiominer.com/files  do not include the / at the end, but this is where your files are stored for images etc
    Public Shared HOSTED_WEBSITE As String = ReturnAIOsetting("hosted_website") ' example https://aiominer.com




    Public Shared ScheduledMining As Boolean = False
    Public Shared MinerMissing As Boolean
    Public Shared WindowsUpdatePassed As Boolean
    Public Shared HardDriveCheckPassed As Boolean
    Public Shared WindowsDefenderPassed As Boolean
    Public Shared VirtualMemoryPassed As Boolean

    Public Shared WTM_Check_Tick As Integer = 0
    Public Shared Timer2_Tick_Count As Integer = 0
    Public Shared powercosts As String = "0"
    Public Shared newcoinname As String = ""
    Public Shared AdvertiseShowing As Boolean = False
    Public Shared Subscriber As Boolean = True
    Public Shared uptime As String = ""
    Public Shared DTime_Started As DateTime

    Public Shared miner_uptime As String
    Public Shared Miner_Started As DateTime







    Public Shared DebugMining As Boolean = False
    Public Shared AIORestartRig As Boolean = False
    Public Shared AIORestartRigtime As Integer = 999999
    Public Shared DebugMiner As String = ""
    Public Shared AIORestartMining As Boolean = False
    Public Shared AIORestartMiningtime As Integer = 999999
    Public Shared AIOAuxCommand As String = ""

    Public Shared JustCheckingMiners As Boolean = False
    Public Shared Coin2Change2 As String = ""
    Public Shared SelectedCoin As String = ""


    Public Shared aioLoading As Boolean = False
    Public Shared donationTimeLeft As String = ""
    Public Shared StoppedMiningIsShowing As Boolean = False
    Public Shared donationPreviousMining As Boolean = False
    Public Shared donationPreviousCoin As String = ""
    Public Shared donationStartedTime As DateTime
    Public Shared donationsStarted As Boolean = False
    Public Shared donationsCompletedToday As Boolean = False
    Public Shared ackupdates As Boolean = False
    Public Shared WorkingDirectory As String = ""
    Public Shared TimedMining As Boolean = False
    Public Shared TimedMiningSettings As TimerSettings.cTimerSettings
    Public Shared DoingProfitability As Boolean = False
    Public Shared DoingTestPool As Boolean = False
    Public Shared GpuStatus As Boolean = False
    Public Shared ETHPill As Boolean = False
    Public Shared Need2Download As Boolean = False
    Public Shared FirstRun As Boolean = False
    Public Shared Version As String = ""
    Public Shared newcoin As Boolean = False
    Public Shared GPUSTOUSE As String = ""
    Public Shared speed As Integer = 0.00
    Public Shared Dualspeed As Integer = 0
    Public Shared speed_chk1 As Integer = 0
    Public Shared speed_chk2 As Integer = 0
    Public Shared speed_chk3 As Integer = 0
    Public Shared speed_chk4 As Integer = 0
    Public Shared speed_chk5 As Integer = 0
    Public Shared speedtype As String = ""
    Public Shared Dualspeedtype As String = ""
    Public Shared nvidia As Boolean = False
    Public Shared amd As Boolean = False
    Public Shared worker As String = ""
    Public Shared ip As String = ""
    Public Shared algo As String = ""
    Public Shared api As String = ""
    Public Shared process_running As String = ""
    Public Shared password As String = ""
    Public Shared coin As String = ""
    Public Shared pool As String = ""
    Public Shared port As String = ""
    Public Shared altargument As String = ""
    Public Shared currentargs As String = ""
    Public Shared amdpwr As Integer = 0
    Public Shared monitoring As Boolean = False
    Public Shared errorswhilemining As Boolean = False
    Public Shared cardcount As Integer = 0
    Public Shared coinprice As String = ""
    'Hard Coded card list, how could this go wrong
    'AMD
    Public Shared two80 As Integer = 0

    Public Shared three80 As Integer = 0
    Public Shared fury As Integer = 0
    Public Shared four70 As Integer = 0
    Public Shared four80 As Integer = 0
    Public Shared five70 As Integer = 0
    Public Shared five80 As Integer = 0
    Public Shared vega As Integer = 0
    Public Shared vii As Integer = 0
    'Nvidia
    Public Shared sixteen60 As Integer = 0
    Public Shared sixteen60ti As Integer = 0
    Public Shared seven50ti As Integer = 0
    Public Shared ten50ti As Integer = 0
    Public Shared ten60 As Integer = 0
    Public Shared ten70 As Integer = 0
    Public Shared ten70ti As Integer = 0
    Public Shared ten80 As Integer = 0
    Public Shared ten80ti As Integer = 0
    Public Shared twenty60 As Integer = 0
    Public Shared twenty70 As Integer = 0
    Public Shared twenty80 As Integer = 0
    Public Shared twenty80ti As Integer = 0

    ' log4net vars for app
    Public Shared ReadOnly log4 As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Public Shared LogsListbox As ListBox = Nothing      ' set in AIOMiner.vb during load to listbox1 for logs

    Public Shared systemsIP As String
    Public Shared GpuListView As ListView
    Public Shared Property ccmlistcombo As ComboBox
    Public Shared Property ShouldBeUpdatingToApi As Boolean = True      ' flip to false based on commands received from api eg  disable rig/user
    Public Shared Property BusyWithRigCommand As Boolean = False
    Public Shared Property ApiKey As String = ""
    Public Shared Property Rigname As String = ""

    Public Shared Property JobHashValue As String
    Public Shared Property myRigJob As RigJob



End Class

Public Class MAINAPP

    <JsonProperty("type")>
    Public Property type As String

    <JsonProperty("value")>
    Public Property value As String

End Class

Public Class AIOS

    <JsonProperty("MAINAPP")>
    Public Property MAINAPPs As MAINAPP()

End Class


Public Class mAIOS
    <JsonProperty("AIOS")>
    Public Property AIOSs As AIOS
End Class



