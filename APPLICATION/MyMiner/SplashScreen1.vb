Imports System.IO
Imports System.Management
Imports System.Net
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils

Imports AIOminer.Log
Imports System.Drawing.Drawing2D



Public NotInheritable Class SplashScreen1

    Private CHECKING_API As Boolean = False
    'TODO: This form can easily be set as the splash screen for the application by going to the "Application" tab
    '  of the Project Designer ("Properties" under the "Project" menu).


    Private Sub MainForm_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles Me.Paint

        Dim sColor As Color = Color.FromArgb(0, 122, 51)        ' pantone 356c
        Dim BaseRectangle As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        'Dim Gradient_Brush As New LinearGradientBrush(BaseRectangle, Color.LightGreen, Color.Black, 90)
        Dim Gradient_Brush As New LinearGradientBrush(BaseRectangle, sColor, Color.Black, 90)

        e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle)

    End Sub


    Private Sub SplashScreen1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Check for previous instance
        Control.CheckForIllegalCrossThreadCalls = False

        If Process.GetProcessesByName(Process.GetCurrentProcess.ProcessName).Length > 1 Then
            CreateObject("WScript.Shell").Popup("AIOMiner Already Running.  The Konami code, use it on the help screen", 3, "AIOMiner Dupe")
            Application.Exit()
        End If

        'Check the status of settings.json
        Dim appPath As String = Application.StartupPath()

        Try
            If System.IO.File.Exists(appPath & "\Settings\Backups\AIOSettings.json") Then
                'Purge old file
                System.IO.File.Delete(appPath & "\Settings\AIOSettings.json")
                System.IO.File.Copy(appPath & "\Settings\Backups\AIOSettings.json", appPath & "\Settings\AIOSettings.json")
                'System.IO.File.Copy(appPath & "\Settings\AIOSettings.json", appPath & "\Backups\AIOSettings.json")
            End If
        Catch ex As Exception

        End Try

        PubShared.Version = "07.11.2020"
        PubShared.aioLoading = True
        Ver.Text = PubShared.Version

        APICheck.Start()

















    End Sub

    Private Sub MainLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles MainLayoutPanel.Paint
        Dim sColor As Color = Color.FromArgb(21, 99, 179)        ' pantone 356c
        Dim BaseRectangle As New Rectangle(0, 0, Me.Width - 1, Me.Height - 1)
        'Dim Gradient_Brush As New LinearGradientBrush(BaseRectangle, Color.LightGreen, Color.Black, 90)
        Dim Gradient_Brush As New LinearGradientBrush(BaseRectangle, sColor, Color.FromArgb(94, 50, 148), 90)
        e.Graphics.FillRectangle(Gradient_Brush, BaseRectangle)
    End Sub



    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Try


                APICheck.Stop()
            Catch ex As Exception

            End Try

            'Health check 
            Dim appPath As String = Application.StartupPath()
            If System.IO.File.Exists(appPath & "\Settings\MinerProcessInfo.json") Then
                Label1.Text = "Found your MinerProcessInfo file, looking good!"
                '####################ADD NEW SETTINGS HERE#######################
                Try
                    '8.2.1 - Added Miner Versions
                    If AddNewSettings("minerversion", "0.0.0.1") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    '8.2.1 - Adding donation setting
                    If AddNewSettings("donation", "No Thanks") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    '8.2.2 - Adding restartrig
                    If AddNewSettings("restartrig", "False") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    If AddNewSettings("restartrigtime", "999999") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    '8.2.2 - Adding restart mining
                    If AddNewSettings("restartmining", "False") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    If AddNewSettings("restartminingtime", "999999") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                '8.4.0
                If AddNewSettings("baseapiurl", PubShared.API_LOCATION) = "added" Then
                    Label1.Text = "Checking under the bed for monsters!"
                End If
                If AddNewSettings("apikey", "") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    If AddNewSettings("rigname", "") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    If AddNewSettings("apienabled", "False") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If
                    If AddNewSettings("dontaskwebsite", "False") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If

                    '8.6.0
                    If AddNewSettings("powercosts", "0.10") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If

                    If AddNewSettings("systemsettings", "") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If

                    If AddNewSettings("WhatToMineUpdate", "1400") = "added" Then
                        Label1.Text = "Checking under the bed for monsters!"
                    End If

                    Try
                        Dim x As String = appPath
                        Dim y As String = "powershell -inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath " & x
                        'Windows Defender Whitelist Test
                        Shell("powershell -inputformat none -outputformat none -NonInteractive -Command Add-MpPreference -ExclusionPath " & appPath)
                    Catch ex As Exception

                    End Try

                Catch ex As Exception
                    LogUpdate("Fatal Error adding New settings, possible corrupt settings file", eLogLevel.Err)
                End Try
                '#################### END NEW SETTINGS HERE #######################





                Timer2.Start()
                Timer1.Stop()
            Else
                Timer1.Stop()
                MsgBox("Unable to find your MinerProcessInfo.json, we kinda needs this")
                Exit Sub
            End If


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Dim appPath As String = Application.StartupPath()
        If System.IO.File.Exists(appPath & "\Settings\Miners.json") Then
            Label1.Text = "Found your Miners file, looking good!"

            If ReturnAIOsetting("updatecheck") = "True" Then
                Label1.Text = "Checking Subscription Status!"
                Timer3.Start()
                Timer2.Stop()
            Else
                Timer4.Start()
                Timer2.Stop()
                Label1.Text = "Good Luck Mining!"
            End If
        Else
            Timer2.Stop()
            MsgBox("Unable to find \Settings\Miners.json, this Is pretty important!")
            Exit Sub

        End If
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Try
            Dim appPath As String = Application.StartupPath()

            If System.IO.File.Exists(appPath & "\Settings\Updates\AIOMiner_Default.json") Then
                System.IO.File.Delete(appPath & "\Settings\Updates\AIOMiner_Default.json")
            End If

            Dim uri As System.Uri = New System.Uri(PubShared.WEB_DEFAULT_COINS_POOLS_JSON)
            Dim webclient As System.Net.WebClient = New System.Net.WebClient()


            Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\AIOMiner_Default.json")
            If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
            End If

            AddHandler webclient.DownloadFileCompleted, AddressOf webclient_DownloadDataCompleted

            webclient.DownloadFileAsync(uri, appPath & "\Settings\Updates\AIOMiner_Default.json")

        Catch ex As Exception
            LogUpdate("Unable to download AIOMiner_Default.json!")
            Timer4.Start()
            Timer3.Stop()
        End Try

        Timer4.Start()
        Timer3.Stop()
    End Sub

    Private Sub webclient_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim appPath As String = Application.StartupPath()

        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download AIOminer_Default.json", eLogLevel.Err)
            Label1.Text = "Error downloading AIOminer_Default.json!, try again or check with support!"
            If ReturnAIOsetting("firstrun") = "True" Then
                Dim wel As New Welcome
                wel.ShowDialog()
                Timer4.Stop()
                Me.Close()
            Else
                AIOMiner.Show()
                Timer4.Stop()
                Me.Close()
            End If
        Else
            Try

                'Purge old backup
                If System.IO.File.Exists(appPath & "\Settings\Backups\AIOMiner_Default.json") Then
                    System.IO.File.Delete(appPath & "\Settings\Backups\AIOMiner_Default.json")
                End If

                'Copy current to backup
                If System.IO.File.Exists(appPath & "\Settings\AIOMiner_Default.json") Then
                    System.IO.File.Move(appPath & "\Settings\AIOMiner_Default.json", appPath & "\Settings\Backups\AIOMiner_Default.json")
                End If

                'Copy update to new location
                If System.IO.File.Exists(appPath & "\Settings\Updates\AIOMiner_Default.json") Then
                    System.IO.File.Move(appPath & "\Settings\Updates\AIOMiner_Default.json", appPath & "\Settings\AIOMiner_Default.json")
                End If


            Catch ex As Exception


            End Try
        End If








    End Sub
    Private Sub webclient1_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Timer4.Start()
        Timer3.Stop()
    End Sub
    Private Sub Timer4_Tick(sender As Object, e As EventArgs) Handles Timer4.Tick


        If ReturnAIOsetting("firstrun") = "True" Then

            Timer4.Stop()
            Dim wel As New Welcome
            wel.Show()
            Me.Close()
        Else
            AIOMiner.Show()
            Timer4.Stop()
            Me.Close()
        End If



    End Sub


	'Private Sub AIOUD_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
	'    Dim appPath As String = Application.StartupPath()

	'    If e.Error IsNot Nothing Then
	'        LogUpdate("Unable to download update!", eLogLevel.Err)
	'        Label1.Text = "Error downloading Update, check with support!"
	'        Timer4.Start()
	'        Timer5.Stop()
	'    Else
	'        Try
	'            Process.Start(appPath & "\Settings\Updates\AIOMinerUpdater.exe")
	'        Catch ex As Exception
	'            Label1.Text = "Error starting update"
	'            LogUpdate(ex.Message, eLogLevel.Err)
	'            Timer4.Start()
	'            Timer5.Stop()
	'        End Try
	'    End If

	'End Sub

	Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub APICheck_Tick(sender As Object, e As EventArgs) Handles APICheck.Tick
        If CHECKING_API = True Then
        Else
            CHECKING_API = True
            Try

                If ReturnAIOsetting("apienabled") = "True" Then
                    Label1.Text = "API Enabled, Waiting for network stack..."
                    Dim ping As New System.Net.NetworkInformation.Ping
                    For i = 2 To 100

                        Try
                            Label1.Refresh()
                            i = i - 1

                            Label1.Text = "API Enabled, Checking Network " & (i).ToString & "/100"
                            Dim request As WebRequest = WebRequest.Create(PubShared.API_LOCATION)
                            request.Timeout = 5000
                            request.Method = "GET"
                            Dim response As WebResponse = request.GetResponse()
                            Dim inputstream1 As Stream = response.GetResponseStream()
                            Dim reader As New StreamReader(inputstream1)
                            Dim workspace As String = reader.ReadToEnd
                            inputstream1.Dispose()
                            reader.Close()
                            If workspace.Contains("OK!") Then
                                Label1.Text = "AIOMiner API is online!"
                                Exit For
                            Else
                                'Fuck you Sr. <3 u

                            End If
                        Catch ex As Exception
                            ' Application.DoEvents()
                            Threading.Thread.Sleep(3000)

                        End Try
                        i += 1

                    Next

                End If



                Try


                    Dim i As Integer = 0

                    Label1.Text = "Loading..."
                    'Get list of video cards
                    Dim GraphicsCardName As String
                    'Dim i As Int64
                    Dim found As Boolean = False


                    Dim WmiSelect As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")
                    For Each WmiResults As ManagementObject In WmiSelect.Get()
                        GraphicsCardName = WmiResults.GetPropertyValue("Name").ToString
                        'Get the lits of NVIDIA CARDS
                        If GraphicsCardName.ToString.ToUpper.Contains("NVIDIA") Then
                            found = True
                            Label1.Text = "Team Green it would seem!"
                            PubShared.nvidia = True

                            If Timer1.Interval = 2000 Then
                            Else
                                Timer1.Interval = 2000
                                Timer1.Start()
                                APICheck.Stop()


                                Label1.Text = "Don't look up..."
                            End If


                        End If
                        'Get the list of AMD CARDS
                        If GraphicsCardName.ToString.ToUpper.Contains("RADEON") Then
                            found = True
                            Label1.Text = "Team Red it would seem!"
                            PubShared.amd = True

                            If Timer1.Interval = 2000 Then
                            Else
                                Timer1.Interval = 2000
                                Timer1.Start()
                                APICheck.Stop()
                                Label1.Text = "Don't look up..."
                            End If

                        End If
                    Next

                    If Timer1.Interval = 2000 Then
                    Else
                        Timer1.Interval = 2000
                        Timer1.Start()
                        APICheck.Stop()

                        Label1.Text = "Don't look up..."
                    End If

                Catch err As Exception
                End Try


            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub DetailsLayoutPanel_Paint(sender As Object, e As PaintEventArgs) Handles DetailsLayoutPanel.Paint

    End Sub
End Class
