Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Win32

Imports AIOminer.Log


Public Class Autologin
    Private Sub Autologin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Enabled = False
        RichTextBox1.Text = "This will setup your machine to auto login to the username that you give below.  This use MUST be in the Administrators Group.  I would 
not use this way to get this done.  While this will work, you should goto Start->Run  Type in netplwiz.exe and uncheck Users must enter a username and password to use this 
computer.  It will ask that you put in your password.  With the way to do this programatically we will need to store your username and password in the registery unencrypted 
NOTE: you must right click on AIOMiner.exe and run as administrator for this to work!  once you do, restart AIOMiner normally!"

        'Most likely it's the current user who's logged in
        TextBox1.Text = Environment.UserName
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then
            MsgBox("Please put in a username")
            Exit Sub
        End If
        If TextBox2.Text = "" Then
            MsgBox("Please put in a password")
            Exit Sub
        End If
        Try
            Dim RegistrationKey As Microsoft.Win32.RegistryKey
            RegistrationKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon\", True)
            Try
                RegistrationKey.SetValue("AutoAdminLogin", "1", Microsoft.Win32.RegistryValueKind.String)
                MsgBox("beep bloop")
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                MsgBox("Something went wrong.  Please review logs")
            End Try
            Exit Sub

            RegistrationKey.SetValue("AutoAdminLogon", "1")
            RegistrationKey.SetValue("DefaultUserName", TextBox1.Text)
            RegistrationKey.SetValue("DefaultPassword", TextBox2.Text)
            ' Shell("REG ADD " & regKey & " /v AutoAdminLogon /t REG_SZ /d 1 /f")
            'Shell("REG ADD " & regKey & " /v DefaultDomainName /t REG_SZ /d domainname /f")
            'Shell("REG ADD " & regKey & " /v DefaultUserName /t REG_SZ /d " & TextBox1.Text & " /f")
            'Shell("REG ADD " & regKey & " /v DefaultPassword /t REG_SZ /d " & TextBox2.Text & " /f")
            '  End If
            MsgBox("Updated your auto-login settings!  Please verify you can auto-login before trusting 'the man'")
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

            Dim rekey As RegistryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon")
            If rekey Is Nothing Then
                System.Windows.Forms.MessageBox.Show("Registry write error")
            Else
                'deleting the values,
                ' first parameter is the Name of the value, 
                'second is a boolean flag,
                ' indication wether the method should 
                'raise an exception if the specified 
                'value is NOT present in the 
                'registry. We set it to false, 
                'because we 'know' its there... we just added it :P
                rekey.DeleteValue("DefaultUserName", False)
                rekey.DeleteValue("DefaultPassword", False)
                rekey.DeleteValue("AutoAdminLogon", False)
            End If
            'close the registry object
            rekey.Close()
            MsgBox("Updated your auto-login settings!  Please verify we ripped the soul out of this machine!")
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub
End Class