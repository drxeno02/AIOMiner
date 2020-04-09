Imports System.ComponentModel
Imports AIOminer.General_Utils


Public Class Welcome_New_1
    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        PubShared.coin = "Bitcoin Gold"
        Welcome_New__.Show()
        Me.Close()

    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click

        PubShared.coin = "Ethereum Classic"
        Welcome_New__.Show()
        Me.Close()
    End Sub

    Private Sub Welcome_New_1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Welcome_New_1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing


    End Sub
End Class