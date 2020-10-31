Imports System.ComponentModel
Imports AIOminer.JSON_Utils
Imports SevenZipExtractor
Imports AIOminer.Log
Imports AIOminer.General_Utils



Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO

Public Class Downloader
    Private camefromprofits As Boolean = False
    Private My_BgWorkerDL As BackgroundWorker = New BackgroundWorker
    Private STATUS As String = ""
    Private DL As String = ""
    Private Directory2 As String = ""
    Private FileName1 As String = ""
    Private StillDownloading As Boolean = False
    Private completed As Boolean = False



    Private Sub Downloader_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Check Windows Defender
        If PubShared.WindowsDefenderPassed <> True Then
            MsgBox("Before Downloading Miners, Please whitelist your AIOMiner Folder.  Please click OK to open the system checks page.")
            MinerSettings.Show()
            Me.Close()
            Exit Sub
        End If

        If PubShared.JustCheckingMiners = True Then
            AddHandler My_BgWorkerDL.DoWork, AddressOf My_BgWorkerDL_DoWork
            AddHandler My_BgWorkerDL.ProgressChanged, AddressOf My_BgWorkerDL_Progress
            Downloader_Start()
            Timer1.Start()
        Else
            Label3.Text = "Status - Downloading Miners.json.."
            Dim appPath As String = Application.StartupPath()
            'Clear the pipes
            If System.IO.File.Exists(appPath & "\Settings\Updates\Miners.json") Then
                System.IO.File.Delete(appPath & "\Settings\Updates\Miners.json")
            End If

            If System.IO.File.Exists(appPath & "\Settings\Backups\Miners.json") Then
                System.IO.File.Delete(appPath & "\Settings\Backups\Miners.json")
            End If

            If System.IO.File.Exists(appPath & "\Settings\Updates\MinerProcessInfo.json") Then
                System.IO.File.Delete(appPath & "\Settings\Updates\MinerProcessInfo.json")
            End If

            If System.IO.File.Exists(appPath & "\Settings\Backups\MinerProcessInfo.json") Then
                System.IO.File.Delete(appPath & "\Settings\Backups\MinerProcessInfo.json")
            End If

            Try

                Dim uri As System.Uri = New System.Uri(PubShared.HOSTED_DATA_STORE + "/Miners.json")
                Dim DMJ As System.Net.WebClient = New System.Net.WebClient()
                Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\Miners.json")
                If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
                    System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
                End If

                AddHandler DMJ.DownloadProgressChanged, AddressOf DMJ_ProgressChanged
                AddHandler DMJ.DownloadFileCompleted, AddressOf DMJ_DownloadDataCompleted

                DMJ.DownloadFileAsync(uri, appPath & "\Settings\Updates\Miners.json")

            Catch ex As Exception
                LogUpdate("Unable to download Miners.json!", eLogLevel.Err)
                Label3.Text = "Status: Status - Error downloading Miners.json"

            End Try
        End If


        'If PubShared.monitoring = True Then
        '    LogUpdate("Unable to benchmark while mining, please stop mining first")
        '    Exit Sub
        '    Me.Close()
        'End If


        'Update for miners
        If AIOMiner.TextBox1.Text.ToLower.Contains("miners") Then
            AIOMiner.TextBox1.Visible = False
            AIOMiner.TextBox2.Visible = False
        End If






    End Sub

    Private Sub DMJ_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)


		Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())

		Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())

		Dim percentage As Double = bytesIn / totalBytes * 100
		'STATUS = "Downloading Files Now"
		ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())


	End Sub
	Private Sub DMJ_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim appPath As String = Application.StartupPath()
        If e.Error IsNot Nothing Then
			LogUpdate("Unable to download Miners.json!", eLogLevel.Err)
			Label3.Text = "Status: Error downloading Miners.json, try again or check with support!"
			Timer1.Stop()
			ProgressBar1.Value = 0
			Exit Sub
		End If

        ProgressBar1.Value = 0
        Label3.Text = "Status: Making a backup of MinerProcessInfo.json"

        If System.IO.File.Exists(appPath & "\Settings\Miners.json") Then
            System.IO.File.Move(appPath & "\Settings\Miners.json", appPath & "\Settings\Backups\Miners.json")
        Else
            Label3.Text = "Status: Somehow you didn't already have this file...no backup made..good luck bro!"
            LogUpdate("User was missing Miners.json, no backup made", eLogLevel.Info)
        End If


        Label3.Text = "Status: Importing new Miners.json"
        If System.IO.File.Exists(appPath & "\Settings\Updates\Miners.json") Then
            System.IO.File.Move(appPath & "\Settings\Updates\Miners.json", appPath & "\Settings\Miners.json")
        Else
            Label3.Text = "Status: Critical error restoring this file..it's missing already?  try the download again"
            LogUpdate("Critical error restoring Miners.json", eLogLevel.Err)
            Exit Sub
        End If


        ProgressBar1.Value = 0

		Label3.Text = "Status: Downloading MinerProcessInfo.json"
		Try

            Dim uri As System.Uri = New System.Uri(PubShared.HOSTED_DATA_STORE & "/MinerProcessInfo.json")
            Dim DMJ1 As System.Net.WebClient = New System.Net.WebClient()
			Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\MinerProcessInfo.json")
			If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
				System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
			End If

			AddHandler DMJ1.DownloadProgressChanged, AddressOf DMJ_ProgressChanged
			AddHandler DMJ1.DownloadFileCompleted, AddressOf DMJ1_DownloadDataCompleted

			DMJ1.DownloadFileAsync(uri, appPath & "\Settings\Updates\MinerProcessInfo.json")

		Catch ex As Exception
			LogUpdate("Unable to download MinerProcessInfo.json!", eLogLevel.Err)
			Label3.Text = "Status: Error downloading MinerProcessInfo.json"
		End Try
	End Sub

	Private Sub DMJ1_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
		If e.Error IsNot Nothing Then
			LogUpdate("Unable to download MinerProcessInfo.json!", eLogLevel.Err)
			Label3.Text = "Status: Error downloading MinerProcessInfo.json!, try again or check with support!"
			ProgressBar1.Value = 0
			Exit Sub
		End If
		Try


			ProgressBar1.Value = 0
			Label3.Text = "Status: Making a backup of MinerProcessInfo.json"
			Dim appPath As String = Application.StartupPath()
			If System.IO.File.Exists(appPath & "\Settings\MinerProcessInfo.json") Then
				System.IO.File.Move(appPath & "\Settings\MinerProcessInfo.json", appPath & "\Settings\Backups\MinerProcessInfo.json")
			Else
				Label3.Text = "Status: Somehow you didn't already have this file...no backup made..good luck bro!"
				LogUpdate("User was missing MinerProcessInfo.json, no backup made", eLogLevel.Info)
			End If


			Label3.Text = "Status: Importing new MinerProcessInfo.json"
			If System.IO.File.Exists(appPath & "\Settings\Updates\MinerProcessInfo.json") Then
				System.IO.File.Move(appPath & "\Settings\Updates\MinerProcessInfo.json", appPath & "\Settings\MinerProcessInfo.json")
			Else
				Label3.Text = "Status: Critical error restoring this file..it's missing already?  try the download again"
				LogUpdate("Critical error restoring MinerProcessInfo.json", eLogLevel.Err)
				Exit Sub
			End If

			AddHandler My_BgWorkerDL.DoWork, AddressOf My_BgWorkerDL_DoWork
			AddHandler My_BgWorkerDL.ProgressChanged, AddressOf My_BgWorkerDL_Progress




			Downloader_Start()
			Timer1.Start()



		Catch ex As Exception
			LogUpdate(ex.Message, eLogLevel.Err)
			Label3.Text = "Status: Critical Error trying to adjust your Miners, please contact support ASAP."
			Label3.Text = "Status: You can restore by taking files from ~\Settings\Backup and putting them into ~\Settings folder!"
			Timer1.Stop()
		End Try
	End Sub

	Public Sub Downloader_Start()

		If My_BgWorkerDL Is Nothing Then My_BgWorkerDL = New BackgroundWorker
		My_BgWorkerDL.WorkerSupportsCancellation = True
		My_BgWorkerDL.WorkerReportsProgress = True

		If Not My_BgWorkerDL.IsBusy Then
			My_BgWorkerDL.RunWorkerAsync()
		Else
			My_BgWorkerDL.CancelAsync()

		End If

	End Sub
	Private Sub My_BgWorkerDL_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()
        Dim GPUTYPE As String = ""
        Dim FoundDir As Boolean = False
        Dim FoundFile As Boolean = False




        If PubShared.nvidia = True Then
            GPUTYPE = "NVIDIA"
        End If

        If PubShared.amd = True Then
            GPUTYPE = "AMD"
        End If




        Do While Not worker.CancellationPending
            Try
                'Find the algo
                For Each XZ In GetMinerProcessInfoJson().MinerProcesses
                    If worker.CancellationPending Then Exit For
                    For Each XYZ In XZ.Infos
                        If worker.CancellationPending Then Exit For
                        If XYZ.GPUx = GPUTYPE Then
                            'Start a Loop Of Death, Enjoy
                            If StillDownloading = True Then
                                Do
                                    Threading.Thread.Sleep(2000)
                                    If StillDownloading = True Then
                                    Else
                                        Exit Do
                                    End If
                                Loop
                            End If
                            Dim Directory1 As String = XYZ.PATH
                            Directory1 = Directory1.ToString.Replace("~", Application.StartupPath())
                            Directory1 = Directory1.ToString.Replace("\\", "\")

                            If Not System.IO.Directory.Exists(Directory1) Then
                                System.IO.Directory.CreateDirectory(Directory1)
                                Label3.Text = ("Created new directory:" & Directory1)
                                FoundDir = False
                            Else
                                STATUS = (Directory1 & " Exists.")
                                FoundDir = True
                            End If

                            If System.IO.File.Exists(Directory1 & XYZ.EXECUTABLE) Then
                                FoundFile = True
                                Label3.Text = (Directory1 & XYZ.EXECUTABLE & " Exists.")
                            Else
                                Dim ThingToDownload As MinerDownloadz = GetMinerJson()
                                'Find the directory's match up
                                Dim FindTheDownload As Minerz = ThingToDownload.Miners.ToList.Find(Function(x) XYZ.PATH.Contains(x.PATH))
                                If FindTheDownload IsNot Nothing Then
                                    'We found a match!
                                    DL = FindTheDownload.WEBLOCATION
                                    FileName1 = DL.Substring(DL.LastIndexOf("/") + 1)
                                    Directory2 = FindTheDownload.PATH
                                    Directory2 = Directory2.Replace("~", Application.StartupPath())
                                    Directory2 = Directory2.Replace("\\", "\")
                                    Try
                                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                        ' | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                                        Dim client As WebClient = New WebClient
                                        'AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
                                        AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
                                        AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
                                        client.DownloadFileAsync(New Uri(DL), Directory2 & FileName1)
                                        Dim MinerName As String = Directory2.Substring(Directory2.LastIndexOf("/") + 1)
                                        Label3.Text = "Downloading: " & MinerName
                                        ListBox1.Items.Add(MinerName)
                                        StillDownloading = True

                                    Catch ex As Exception
                                        completed = False

                                        LogUpdate(ex.Message, eLogLevel.Err)
                                    End Try

                                End If
                            End If
                        End If

                    Next

                    'Update your version of miner
                    Try
                        'Get the latest version

                    Catch ex As Exception

                    End Try

                    'Done looping through the list
                    'Profitability.ShowDialog()
                    'Me.Close()

                Next

                My_BgWorkerDL.ReportProgress(100)
                My_BgWorkerDL.CancelAsync()



            Catch ex As Exception

            End Try


        Loop
    End Sub
    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If e.Error IsNot Nothing Then
            StillDownloading = False
            Label3.Text = "Error while downloading"
            completed = False

            Exit Sub
        End If


        Label3.Text = "Completed the download, extracting now"
        Dim FileToExtract As String = DL
        Try
            If FileToExtract.Contains(".exe") Then
                Label3.Text = "Nothing to extract, just a .exe"
            Else

                Dim NewExtract As ArchiveFile = New ArchiveFile(Directory2 & FileName1)
                NewExtract.Extract(Directory2, True)
                NewExtract.Dispose()

            End If

        Catch ex As Exception
            ProgressBar1.Value = 0
            StillDownloading = False
        End Try

        ProgressBar1.Value = 0
        StillDownloading = False

    End Sub
    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)


        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())

        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())

        Dim percentage As Double = bytesIn / totalBytes * 100
        'STATUS = "Downloading Files Now"
        ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())


    End Sub
    Private Sub My_BgWorkerDL_Progress(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        ' handle updating UI from UI thread to stop issue of updating for bgworker thread directely
        Try
            If e.ProgressPercentage = 50 Then               ' all is ok
                'Label4.Text = ""
            ElseIf e.ProgressPercentage = 0 Then        ' miner failed so redx it
                ' Label4.Text = ""
            ElseIf e.ProgressPercentage = 100 Then      ' all done so cleanup
                Label3.Text = "All done!"

                'Check for a Miners Update
                Try
                    SetAllowUnsafeHeaderParsing20()
                    Dim VERResults As String
                    Dim address As String = "https://raw.githubusercontent.com/BobbyGR/AIOMiner/master/TOOLS/minersversion.txt"
                    Dim client As WebClient = New WebClient()
                    Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
                    VERResults = reader.ReadToEnd
                    SaveAIOsetting("minerversion", VERResults)

                Catch ex As Exception
                    LogUpdate("Unable to save minerversion! maybe s3 bill wasn't paid?", eLogLevel.Err)
                End Try

                Me.Visible = False
                Me.Close()

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Label4.Text = STATUS
    End Sub

    Private Sub Downloader_Activated(sender As Object, e As EventArgs) Handles Me.Activated

    End Sub

    Private Sub Downloader_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Try


            Timer1.Stop()
            My_BgWorkerDL.CancelAsync()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        PubShared.monitoring = False
        My_BgWorkerDL.CancelAsync()
        Timer1.Stop()
        Me.Close()

    End Sub
End Class