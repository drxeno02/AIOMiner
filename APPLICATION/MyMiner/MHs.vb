Public Class MHs
    Private Sub MHs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Timer1.Enabled = True

    End Sub

    Private Sub MHs_Click(sender As Object, e As EventArgs) Handles Me.Click
        Timer1.Enabled = False
        Me.Close()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label2.Text = PubShared.speed
    End Sub
End Class