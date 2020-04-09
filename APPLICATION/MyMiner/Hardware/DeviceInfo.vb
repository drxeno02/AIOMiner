Imports System.Text.RegularExpressions
Imports AIOminer.Log


Public Class DeviceInfo


    Public Shared Function GetDeviceID(txt As String) As String
        'Get the Device ID
        Dim sbraces2 As String
        Dim re1 As String = ".*?" 'Non-greedy match on filler
        Dim re2 As String = "(\[.*?\])"   'Square Braces 1

        Dim r As Regex = New Regex(re1 + re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
        Dim m As Match = r.Match(txt)
        If (m.Success) Then
            Dim sbraces1 = m.Groups(1)
            sbraces2 = sbraces1.ToString
            sbraces2 = sbraces2.Replace("[", "")
            sbraces2 = sbraces2.Replace("]", "")
            Return sbraces2
        Else
            Return ErrorMessage("Unable to determine DeviceID")

        End If
    End Function


    Public Shared Function ReturnHostname() As String
        Dim hostname As String = String.Empty
        Try
            hostname = System.Net.Dns.GetHostName().Replace(" ", " ").Replace("-", "")
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            hostname = String.Empty
        End Try

        Return hostname

    End Function


    Public Shared Function GetIPv4Address() As String
        Dim _GetIPv4Address As String = String.Empty
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Dim iphe As System.Net.IPHostEntry = System.Net.Dns.GetHostEntry(strHostName)

        For Each ipheal As System.Net.IPAddress In iphe.AddressList
            If ipheal.AddressFamily = System.Net.Sockets.AddressFamily.InterNetwork Then
                _GetIPv4Address = ipheal.ToString()
            End If
        Next

        Return _GetIPv4Address

    End Function


End Class
