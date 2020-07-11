Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils
Imports System.ComponentModel

Public Class Welcome_New_4
    Private Sub Welcome_New_4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If PubShared.coin = "Ethereum" Then
            Label9.Text = "Select Ethereum and hit start."
        Else
            Label9.Text = "Select ZCash and hit start."
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If ReturnAIOsetting("firstrun") = "False" Then
			Me.Close()

		Else
			'Check for miner update file
			PubShared.FirstRun = True
            PubShared.Need2Download = True
            AIOMiner.Show()
            AIOMiner.Visible = True
            AIOMiner.Opacity = 100
            SaveAIOsetting("firstrun", "False")
            Me.Close()

        End If






    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        'https://www.reddit.com/r/AIOMiner/
        Dim webAddress As String
        webAddress = "https://www.reddit.com/r/AIOMiner/"
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim webAddress As String
        webAddress = PubShared.WEB_DISCORD_LINK
        Process.Start(webAddress)
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        'https://github.com/BobbyGR/AIOMiner
        Dim webAddress As String
        webAddress = "https://github.com/BobbyGR/AIOMiner"
        Process.Start(webAddress)
    End Sub

    Private Sub Welcome_New_4_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub
End Class