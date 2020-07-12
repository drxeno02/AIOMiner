Imports System.IO

Public Class Log

    Public Enum eLogLevel
        Info = 0
        Err = 1
        Debug = 3
    End Enum


    Public Shared Sub LogUpdate(Update As String, Optional ByVal Level As eLogLevel = eLogLevel.Info)
        Try

            If PubShared.LogsListbox Is Nothing Then
            Else


                PubShared.LogsListbox.Items.Insert(0, Update & " " & DateAndTime.Now)

                ' log to file with log4
                If Level = eLogLevel.Info Then
                    PubShared.log4.Info(Update)
                ElseIf Level = eLogLevel.Err Then
                    PubShared.log4.Error(Update)
                ElseIf Level = eLogLevel.Debug Then
                    PubShared.log4.Debug(Update)
                Else
                    PubShared.log4.Info(Update & "  <<unknown log level>>")
                End If
            End If

        Catch ex As Exception

        End Try

    End Sub

    Public Shared Sub FlushLogs()
        Dim appPath As String = Application.StartupPath()
        Try
            If File.Exists(appPath + "\miner.log") Then
                File.Delete(appPath + "\miner.log")
            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Public Shared Function ErrorMessage(Message As String)
        Return MsgBox(Message)
    End Function

End Class
