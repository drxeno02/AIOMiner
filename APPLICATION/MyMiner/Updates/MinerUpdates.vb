Imports System.Net
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports AIOminer.General_Utils
Imports System.IO


Public Class MinerUpdates
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
    Dim US_Status As String = ""
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
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

                Dim uri As System.Uri = New System.Uri(pubshared.HOSTED_DATA_STORE & "/aiominer/Miners.json")
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

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label1.Text = US_Status
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

        US_Status = "Update Completed."
        Try
            Dim appPath As String = Application.StartupPath()
            Dim uri As System.Uri = New System.Uri(pubshared.HOSTED_DATA_STORE & "/aiominer/MinerProcessInfo.json")
            Dim DMJ1 As System.Net.WebClient = New System.Net.WebClient()
            Dim fileInfo As System.IO.FileInfo = New System.IO.FileInfo(appPath & "\Settings\Updates\MinerProcessInfo.json")
            If Not System.IO.Directory.Exists(fileInfo.Directory.FullName) Then
                System.IO.Directory.CreateDirectory(fileInfo.Directory.FullName)
            End If

            AddHandler DMJ1.DownloadProgressChanged, AddressOf DMJ_ProgressChanged
            AddHandler DMJ1.DownloadFileCompleted, AddressOf DMJ1_DownloadDataCompleted

            DMJ1.DownloadFileAsync(uri, appPath & "\Settings\Updates\MinerProcessInfo.json")
            Label1.Text = "Download Complete"
        Catch ex As Exception
            LogUpdate("Unable to download MinerProcessInfo.json!", eLogLevel.Err)
            US_Status = "Error downloading MinerProcessInfo.json"
            Timer1.Stop()
        End Try
    End Sub
    Private Sub DMJ1_DownloadDataCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If e.Error IsNot Nothing Then
            LogUpdate("Unable to download MinerProcessInfo.json!", eLogLevel.Err)
            Label1.Text = "Error downloading MinerProcessInfo.json!, try again or check with support!"
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

    Private Sub MinerUpdates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate(New Uri(pubshared.WEB_WIKI_LINK_MINER_UPDATES))
    End Sub
End Class