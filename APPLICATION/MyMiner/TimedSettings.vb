Imports AIOminer.Log

Public Class TimedSettings

    Private timersArray() As String = {"1 AM", "2 AM", "3 AM", "4 AM", "5 AM", "6 AM", "7 AM", "8 AM", "9 AM", "10 AM", "11 AM",
                                        "12 PM", "1 PM", "2 PM", "3 PM", "4 PM", "5 PM", "6 PM", "7 PM", "8 PM", "9 PM", "10 PM",
                                        "11 PM", "12 AM"}




    Private Sub TimedSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Check if /Settings/Timed.json exists, if not create it
        Dim appPath As String = Application.StartupPath()


        Try
            If System.IO.File.Exists(appPath & "\Settings\TimerSettings.json") Then
                ' get settings and fill in form appropriately
                Dim ts As TimerSettings.cTimerSettings

                ts = JSON_Utils.GetTimerSettingsJson()

                If (ts IsNot Nothing AndAlso ts.Settings IsNot Nothing AndAlso ts.Settings.Length > 0) Then

                    For i As Short = 0 To ts.Settings.Length - 2
                        Dim cndx As String = (i + 1).ToString
                        Dim cbx As CheckBox = GroupBox2.Controls("checkbox" & cndx)
                        cbx.Checked = IIf(ts.Settings(i).isOn = "True", True, False)
                    Next


                End If

            Else
                'Create the File
                '  no   the writing will do the create ...
            End If
        Catch ex As Exception

        End Try

        ' get the settings for timed choices from settings json file


        RichTextBox1.Enabled = False
        RichTextBox1.Text = "This is great if your power company charges you an arm and a leg during certain times of the day.  Now with timed mining you can specify what times of the day you want your rig to be mining!  Items with a checkbox indicate that you want to be mining for that hour, remove a check box and we will turn off mining.  We check every hour if you have enabled or disabled mining!"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Try
            Dim cntr As Integer = 1
            Dim ts As New List(Of TimerSettings.TimerSetting)()

            For i As Short = 0 To 23
                Dim isSet As String = IIf(CType(GroupBox2.Controls("checkbox" & cntr.ToString), CheckBox).Checked, "True", "False")
                Dim tsetting As New TimerSettings.TimerSetting()
                tsetting.hour = timersArray(i)
                tsetting.isOn = isSet
                ts.Add(tsetting)
                cntr += 1
            Next

            ' add on the app's setting for timed mining 
            Dim tset As New TimerSettings.TimerSetting()
            tset.hour = "AppTimedMining"
            tset.isOn = IIf(PubShared.TimedMining, "True", "False")
            ts.Add(tset)

            Dim tss As New TimerSettings.cTimerSettings()
            tss.Settings = ts.ToArray()

            JSON_Utils.SaveTimerSettingsJson(tss)
            MsgBox("Timed mining settings saved ok")


        Catch ex As Exception
            LogUpdate("Error saving Timed Settings.  Message: " & ex.Message)

        End Try




    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' reset all the checkboxes to off
        For i As Short = 1 To 24
            CType(GroupBox2.Controls("checkbox" & i.ToString), CheckBox).Checked = True
        Next
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

    End Sub
End Class