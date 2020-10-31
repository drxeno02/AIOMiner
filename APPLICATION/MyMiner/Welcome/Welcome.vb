Imports System.Management
Imports System.Text.RegularExpressions
Imports System.Threading

Imports System.Net
Imports System.IO
Imports System.ComponentModel
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils




Public Class Welcome

    Public Shared buttonmademedoit As Boolean = False
    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ReturnAIOsetting("firstrun") = "False" Then
            'yMiner.Opacity = 100
            ' Me.Close()
        Else
            AIOMiner.Opacity = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ReturnAIOsetting("firstrun") = "False" Then
            Me.Close()
        Else
            PubShared.FirstRun = True
            PubShared.Need2Download = True
            AIOMiner.Show()
            AIOMiner.Visible = True
            AIOMiner.Opacity = 100
            SaveAIOsetting("firstrun", "False")
            Me.Close()
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        PubShared.FirstRun = True

        Welcome_New_1.Show()
        Me.Close()



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        SaveAIOsetting("firstrun", "False")

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Welcome_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String
        webAddress = PubShared.HOSTED_WEBSITE
        Process.Start(webAddress)
    End Sub

End Class