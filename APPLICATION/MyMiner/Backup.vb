Imports System.Configuration
Imports System.IO

Imports AIOminer.Log


Public Class Backup

    Private Sub Backup_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim appPath As String = Application.StartupPath()
        'MsgBox(config.FilePath)
        'Copy config to apppath

        Try
            If Not System.IO.Directory.Exists(appPath & "\Backup") Then
                System.IO.Directory.CreateDirectory(appPath & "\Backup")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        Try
            System.IO.File.Copy(appPath & "\Settings\AIOSettings.json", appPath & "\Backup\AIOSettings.json", True)
            System.IO.File.Copy(appPath & "\Settings\AIOMiner.json", appPath & "\Backup\AIOMiner.json", True)
            System.IO.File.Copy(appPath & "\Settings\MinerProcessInfo.json", appPath & "\Backup\MinerProcessInfo.json", True)
            System.IO.File.Copy(appPath & "\Settings\AIOProfitability.json", appPath & "\Backup\AIOProfitability.json", True)
            System.IO.File.Copy(appPath & "\Settings\TimerSettings.json", appPath & "\Backup\TimerSettings.json", True)
            MsgBox("Backup has completed to: " + appPath & "\Backup")
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            Dim appPath As String = Application.StartupPath()
            System.IO.File.Copy(appPath & "\Backup\AIOSettings.json", appPath & "\Settings\AIOSettings.json", True)
            System.IO.File.Copy(appPath & "\Backup\AIOMiner.json", appPath & "\Settings\AIOMiner.json", True)
            System.IO.File.Copy(appPath & "\Backup\MinerProcessInfo.json", appPath & "\Settings\MinerProcessInfo.json", True)
            System.IO.File.Copy(appPath & "\Backup\AIOProfitability.json", appPath & "\Settings\AIOProfitability.json", True)
            System.IO.File.Copy(appPath & "\Backup\TimerSettings.json", appPath & "\Settings\TimerSettings.json", True)
            MsgBox("Restore has completed to: " + appPath & "\Settings")
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class