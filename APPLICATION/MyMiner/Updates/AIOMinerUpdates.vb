Public Class AIOMinerUpdates
    Private Sub AIOMinerUpdates_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        WebBrowser1.Navigate(New Uri(pubshared.WEB_WIKI_LINK_UPDATES))
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs)

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Process.Start("https://github.com/BobbyGR/AIOMiner/releases/download/A8.1.0/AIOInstaller.exe")
    End Sub
End Class