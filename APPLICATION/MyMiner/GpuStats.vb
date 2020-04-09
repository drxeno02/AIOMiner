Imports System.ComponentModel
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils


Public Class GpuStats
    Private Sub GpuStats_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ListView1.View = View.Details

        'Check if you should be ontop
        Try
            If ReturnAIOsetting("ontop") = True Then
                Me.TopMost = True
            Else
                Me.TopMost = False
            End If
        Catch ex As Exception

        End Try



    End Sub

    Private Sub GpuStats_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        PubShared.GpuStatus = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub
End Class