Imports System.ComponentModel
Imports AIOminer.General_Utils

Public Class Welcome_New__
    Private Sub Welcome_New___Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        Dim webAddress As String
        webAddress = "https://www.exodus.io/releases/"
        Process.Start(webAddress)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Welcome_New_3.Show()
        Me.Close()

    End Sub

    Private Sub Welcome_New___Closed(sender As Object, e As EventArgs) Handles Me.Closed

    End Sub

    Private Sub Welcome_New___Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing


    End Sub
End Class