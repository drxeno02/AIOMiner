
Imports System.Net
Imports System.Net.Mail
Imports System.Text
Imports AIOminer.Log
Imports AIOminer.General_Utils


Public Class Email


    Public Shared Sub SendEmail(What As String, Who As String)
        If ReturnAIOsetting("email") = "" Then
            LogUpdate("Email would go out..but none is setup! " & What)
        Else
            Who = ReturnAIOsetting("email")
            Dim mail As New MailMessage()
            Dim MyMailMessage As New MailMessage With {
                .From = New MailAddress("xxxxxx")
            }
            MyMailMessage.To.Add(Who)
            MyMailMessage.Subject = "AIOMiner Notification!"
            MyMailMessage.Body = (What)
            Dim SMTPServer As New SmtpClient("smtp.gmail.com") With {
                .Port = 587,
                .Credentials = New NetworkCredential("xxxxxxx", xyzit()),
                .EnableSsl = True
            }
            Try
                SMTPServer.Send(MyMailMessage)
                LogUpdate("E-Mail sent to " + Who)
            Catch ex As SmtpException
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        End If

    End Sub


    Public Shared Function xyzit() As String

        Dim tarr() As UShort = {185, 138, 156, 148, 171, 158, 141, 155, 140, 206, 205, 204, 219, 219, 219}
        Dim tu() As UShort = {170, 255}
        Dim z() As UShort = {0, 0, 1}

        Dim res() As UShort = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        Dim ndx As UShort = 0
        For Each n As UShort In tarr
            For Each zt As UShort In z
                n = n Xor tu(zt)
            Next
            res(ndx) = n
            ndx += 1
        Next

        Dim rtnval As StringBuilder = New StringBuilder()

        For Each c As UShort In res
            rtnval.Append(Chr(c))
        Next

        Return rtnval.ToString()

    End Function
End Class
