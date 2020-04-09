Imports AIOminer.Log

Public Class StartOnBoot
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim RegistrationKey As Microsoft.Win32.RegistryKey
        Dim KeyName As String = "AIOMiner"
        Dim KeyValue As String = System.Windows.Forms.Application.StartupPath & "\AIOMiner.exe"
        RegistrationKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        Try
            If RegistrationKey.GetValue(KeyName) = Nothing Then
                RegistrationKey.SetValue(KeyName, KeyValue, Microsoft.Win32.RegistryValueKind.String)
            End If
            LogUpdate("Imported AIOMiner.exe to autostart on boot")
            MsgBox("We have enabled autostart of AIOMiner.exe!")
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            MsgBox("Something went wrong.  Please review logs")
        End Try
    End Sub

    Private Sub StartOnBoot_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Enabled = False
        RichTextBox1.Text = "This will allow for you to have AIOMiner.exe start auto-magicly when windows loads or to Remove it!"
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim RegistrationKey As Microsoft.Win32.RegistryKey
        Dim KeyName As String = "AIOMiner"
        Dim KeyValue As String = System.Windows.Forms.Application.StartupPath & "\AIOMiner.exe"
        RegistrationKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Run", True)
        Try
            If Not RegistrationKey.GetValue(KeyName) = Nothing Then
                RegistrationKey.DeleteValue(KeyName, False)
            End If
            LogUpdate("Removed AIOMiner.exe to autostart on boot")
            MsgBox("We have disabled autostart of AIOMiner.exe!")
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            MsgBox("Something went wrong.  Please review logs")
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class