Imports AIOminer.General_Utils
Imports System.Net
Imports System.ComponentModel
Imports SevenZipExtractor


Public Class ThirdParty
    Dim loaded As Boolean = False

    Private Sub AIOMiner_3rdParty_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Set Status
        Label4.Text = ""

        If ReturnAIOsetting("nvidia") = "True" Then
            CheckBox1.Checked = True
        End If

        loaded = True

        RichTextBox1.ReadOnly = True
        RichTextBox1.Text = "All of the applications here are used to help you, the miner enhance your mining experience.  They are in no way associated with AIOMiner or Modern Mining LLC.  Enable and use as you want"
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim appPath As String = Application.StartupPath()

        If loaded = True Then
            If CheckBox1.Checked = True Then
                MsgBox("If you want to use this automagicly we can, you just need to disable UAC.  You can do this by going to Start, type 'uac s' then put the bar to the bottom of the page, and reboot.  If not, when your rig reboots or you change what you are mining, it will stop at a UAC window.this application needs admin rights.  We are using Elevate.exe to start it, but UAC is a jerk")

                'Check to see if they have downloaded it, if not, download it
                If System.IO.File.Exists(appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill-r2.exe") Then
                    SaveAIOsetting("nvidia", "True")
                    Label4.Text = "Enabled"
                Else
                    System.IO.Directory.CreateDirectory(appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill")
                    'You need to download this
                    Label4.Text = "Please hold while we download this beast!"
                    Try
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12

                        Dim client As WebClient = New WebClient
                        AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
                        client.DownloadFileAsync(New Uri("https://github.com/AIOminer/AIOMiner/releases/download/Software/OhGodAnETHlargementPill.zip"), appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill.zip")
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try
                    '


                End If
                'Set a flag in mining/closing mining

                'The Settings Nvidia flag is not used.  Use it.  IN the future put in random
                'flags that don't mean anything so people don't have to update settings files

            Else
                SaveAIOsetting("nvidia", "False")
                Label4.Text = "Disabled"
            End If
        End If

    End Sub
    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        If e.Error IsNot Nothing Then
            Label4.Text = "Error Downloading, please try again!"
            Exit Sub
        Else
            Try
                Dim appPath As String = Application.StartupPath()

                Label4.Text = "Download Complete, Extracting files!"
                Dim NewExtract As ArchiveFile = New ArchiveFile(appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill.zip")
                NewExtract.Extract(appPath & "\Miners\NVIDIA\OhGodAnETHlargementPill", True)
                SaveAIOsetting("nvidia", "True")
                Label4.Text = "Enabled!"
            Catch ex As Exception
                Label4.Text = "Error Extracting! Please try again, or contact support!"
            End Try
        End If

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim LNK As String = PubShared.WEB_WIKI_LINK_UPDATES
        Process.Start(LNK)
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub
End Class