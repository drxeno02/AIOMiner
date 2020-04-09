Imports Newtonsoft.Json

Public Class TimerSettings

    Public Class TimerSetting

        <JsonProperty("hour")>
        Public Property hour As String

        <JsonProperty("on")>
        Public Property isOn As String

    End Class

    Public Class cTimerSettings
        <JsonProperty("TimerSettings")>
        Public Property Settings As TimerSetting()
    End Class


    Public Shared Function OkToMine(ByVal _settings As cTimerSettings) As Boolean
        Dim ok As Boolean = False

        ' if timed mining is not on then say ok to mine
        ' if timed mining is on, then check current time and timed settings to see if ok or not
        If Not PubShared.TimedMining Then
            ok = True
        Else
            Dim tset As TimerSetting() = _settings.Settings
            Dim curHour As String = Date.Now.ToString("hh")
            If curHour.StartsWith("0") Then curHour = curHour.Replace("0", "")

            If Date.Now.ToString.Contains("PM") Then
                curHour &= " PM"
            Else
                curHour &= " AM"
            End If
            Dim found As Boolean = False
            For Each ts As TimerSetting In tset
                If curHour.ToLower = ts.hour.ToLower AndAlso ts.isOn = "True" Then
                    ok = True
                    Exit For
                End If
            Next
        End If

        Return ok


    End Function
End Class
