Public Class Debug
    Private Sub Debug_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TMRupdate.Start()
    End Sub

    Private Sub TMRupdate_Tick(sender As Object, e As EventArgs) Handles TMRupdate.Tick
        Try
            RichTextBox1.AppendText(PubShared.DebugMiner + "  " + Format(TimeOfDay, "HH:mm:ss") + vbNewLine)
        Catch ex As Exception
        End Try


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TMRupdate.Stop()

        PubShared.DebugMining = False
        Me.Close()

    End Sub
End Class