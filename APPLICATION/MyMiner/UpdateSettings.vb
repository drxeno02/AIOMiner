Imports System.Net
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports AIOminer.General_Utils
Imports System.IO

Public Class UpdateSettings
    Dim US_Status As String = ""

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim result As Integer = MessageBox.Show("You will need to go back and review your prefered miners after doing this! This will check for new miner applications and processes. Do you want to do this?", "WARNING", MessageBoxButtons.YesNo)


        If result = DialogResult.No Then
            MessageBox.Show("Nothing Changed")
        ElseIf result = DialogResult.Yes Then

            Timer1.Start()
            US_Status = "Downloading Miners.json.."
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

                Dim uri As System.Uri = New System.Uri(PubShared.HOSTED_DATA_STORE & "/Miners.json")
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
                US_Status = "Error downloading Miners.json"
                Timer1.Stop()
            End Try
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

        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download Miners.json!", eLogLevel.Err)
            US_Status = "Error downloading Miners.json, try again or check with support!"
            Timer1.Stop()
            ProgressBar1.Value = 0
            Exit Sub
        End If

        ProgressBar1.Value = 0

        US_Status = "Downloading MinerProcessInfo.json"
        Try
            Dim appPath As String = Application.StartupPath()
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
            US_Status = "Error downloading MinerProcessInfo.json"
            Timer1.Stop()
        End Try
    End Sub

    Private Sub DMJ1_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download MinerProcessInfo.json!", eLogLevel.Err)
            Label3.Text = "Error downloading MinerProcessInfo.json!, try again or check with support!"
            Timer1.Stop()
            ProgressBar1.Value = 0
            Exit Sub
        End If
        Try


            ProgressBar1.Value = 0
            US_Status = "Making a backup of MinerProcessInfo.json"
            Dim appPath As String = Application.StartupPath()
            If System.IO.File.Exists(appPath & "\Settings\MinerProcessInfo.json") Then
                System.IO.File.Move(appPath & "\Settings\MinerProcessInfo.json", appPath & "\Settings\Backups\MinerProcessInfo.json")
            Else
                US_Status = "Somehow you didn't already have this file...no backup made..good luck bro!"
                LogUpdate("User was missing MinerProcessInfo.json, no backup made", eLogLevel.Info)
            End If


            US_Status = "Importing new MinerProcessInfo.json"
            If System.IO.File.Exists(appPath & "\Settings\Updates\MinerProcessInfo.json") Then
                System.IO.File.Move(appPath & "\Settings\Updates\MinerProcessInfo.json", appPath & "\Settings\MinerProcessInfo.json")
            Else
                US_Status = "Critical error restoring this file..it's missing already?  try the download again"
                LogUpdate("Critical error restoring MinerProcessInfo.json", eLogLevel.Err)
                Timer1.Stop()
                Exit Sub
            End If

            US_Status = "Reviewing if we have any new miners or algos!"

            Try
                MinersJsonUpdate.UpdateMinersJson()
                Downloader.Show()
                US_Status = "Downloading updates if you have any!"
                Timer1.Stop()
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                US_Status = "Critical Error trying to adjust your Miners, please contact support ASAP."

                US_Status = "You can restore by taking files from ~\Settings\Backups and putting them into ~\Settings folder!"
                Timer1.Stop()
            End Try
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            US_Status = "Critical Error trying to adjust your Miners, please contact support ASAP."
            US_Status = "You can restore by taking files from ~\Settings\Backup and putting them into ~\Settings folder!"
            Timer1.Stop()
        End Try
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label3.Text = US_Status

    End Sub

    Private Sub UpdateSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ReturnAIOsetting("updatecheck") = "True" Then
            CheckBox2.Checked = True
        Else
            CheckBox2.Checked = False
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim appPath As String = Application.StartupPath()

        'Flush that toilet



        Try
            If System.IO.File.Exists(appPath & "\Settings\Updates\AIOMiner_Default.json") Then
                System.IO.File.Delete(appPath & "\Settings\Updates\AIOMiner_Default.json")
            End If

            Dim uri As System.Uri = New System.Uri(PubShared.HOSTED_DATA_STORE & "/AIOMiner_Default.json")
            Dim AIOMD As System.Net.WebClient = New System.Net.WebClient()
            Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\AIOMiner_Default.json")
            If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
            End If

            AddHandler AIOMD.DownloadProgressChanged, AddressOf DMJ_ProgressChanged
            AddHandler AIOMD.DownloadFileCompleted, AddressOf AIOMD_DownloadDataCompleted
            Label3.Text = "Downloading latest AIOMiner_Default.json"
            AIOMD.DownloadFileAsync(uri, appPath & "\Settings\Updates\AIOMiner_Default.json")

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Label3.Text = "Error downloading AIOminer_Default.json"
        End Try

    End Sub
    Private Sub AIOMD_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim appPath As String = Application.StartupPath()
        ProgressBar1.Value = 0
        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download AIOminer_Default.json", eLogLevel.Err)
            Label3.Text = "Error downloading AIOminer_Default.json!, try again or check with support!"
            ProgressBar1.Value = 0
            Exit Sub
        End If
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

            Label3.Text = "Coins and Pools list has been updated!"
        Catch ex As Exception
            Label3.Text = "Error with doing some ninja copy/paste, try again or contact support"
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            SaveAIOsetting("updatecheck", "True")
        Else
            SaveAIOsetting("updatecheck", "False")
        End If
    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs)
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim VERResults As String
            Dim address As String = pubshared.HOSTED_WEBSITE & "/products/version.html"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            VERResults = reader.ReadToEnd
            'MsgBox(VERResults)

            If PubShared.Version = VERResults Then
                Label3.Text = "You are on the latest"
            Else
                Label3.Text = "Update Available! " & VERResults.Trim

                'Download AIOMinerUpdater.exe
                Dim appPath As String = Application.StartupPath()
                Try
                    If System.IO.File.Exists(appPath & "\Settings\Updates\AIOMinerUpdater.exe") Then
                        System.IO.File.Delete(appPath & "\Settings\Updates\AIOMinerUpdater.exe")
                    End If

                    Dim uri As System.Uri = New System.Uri(PubShared.HOSTED_DATA_STORE & "/AIOMinerUpdater.exe")
                    Dim AIOUD As System.Net.WebClient = New System.Net.WebClient()
                    Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\AIOMinerUpdater.exe")
                    If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
                        System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
                    End If

                    AddHandler AIOUD.DownloadProgressChanged, AddressOf DMJ_ProgressChanged
                    AddHandler AIOUD.DownloadFileCompleted, AddressOf AIOUD_DownloadDataCompleted
                    Label3.Text = "Downloading Updater"
                    AIOUD.DownloadFileAsync(uri, appPath & "\Settings\Updates\AIOMinerUpdater.exe")

                Catch ex As Exception
                    LogUpdate(ex.Message, eLogLevel.Err)
                    Label3.Text = "Error downloading AIOMinerUpdater.exe"
                End Try
            End If
        Catch ex As Exception

        End Try



    End Sub
    Private Sub AIOUD_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim appPath As String = Application.StartupPath()
        ProgressBar1.Value = 0
        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download update!", eLogLevel.Err)
            Label3.Text = "Error downloading Update, check with support!"
            ProgressBar1.Value = 0
            Exit Sub
        Else
            Try
                Process.Start(appPath & "\Settings\Updates\AIOMinerUpdater.exe")
            Catch ex As Exception
                Label3.Text = "Error starting update"
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim result As Integer = MessageBox.Show("Time for a folder cleaning?  This will delete all miners and re-download them, are you sure?", "WARNING", MessageBoxButtons.YesNo)


        If result = DialogResult.No Then
            MessageBox.Show("Nothing Changed")
        ElseIf result = DialogResult.Yes Then
            Dim appPath As String = Application.StartupPath()
            Try

                Dim path As String = appPath & "\Miners"
                System.IO.Directory.Delete(path, True)

            Catch ex As Exception
                LogUpdate("Unable to purge your Miners directory!, Restart without mining first and try again!")
                Exit Sub

            End Try
            Downloader.Show()

        End If

    End Sub
End Class