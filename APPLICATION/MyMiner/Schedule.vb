Imports AIOminer.Log
Imports AIOminer.General_Utils



Public Class Schedule
    Private Sub Schedule_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.Text = "Days are tied to your computer time.  Change will occur at 12:00:00~AM (Midnight).  You must have the Schedule enabled on the main application.  You must have at least one pool added of the coin type.  It will mine your primary pool for that coin. To mine the most profitable coin (according to WhatToMine.com) select ""Most Profitable"", if you have a pool setup we will swap over to it, if not we will go down the top 5.  If none are avaiable we won't change
a damn thing..."





        If ComboBox1.Items.Contains("Most Profitable") Then
        Else
            ComboBox1.Items.Add("Most Profitable")
            ComboBox2.Items.Add("Most Profitable")
            ComboBox3.Items.Add("Most Profitable")
            ComboBox4.Items.Add("Most Profitable")
            ComboBox5.Items.Add("Most Profitable")
            ComboBox6.Items.Add("Most Profitable")
            ComboBox7.Items.Add("Most Profitable")
        End If


        If ReturnAIOsetting("monday") = "" Then
            For Each item As String In AIOMiner.ComboBox1.Items
                ComboBox1.Items.Add(item)
                ComboBox2.Items.Add(item)
                ComboBox3.Items.Add(item)
                ComboBox4.Items.Add(item)
                ComboBox5.Items.Add(item)
                ComboBox6.Items.Add(item)
                ComboBox7.Items.Add(item)
            Next
        Else
            Try
                For Each item As String In AIOMiner.ComboBox1.Items
                    ComboBox1.Items.Add(item)
                    ComboBox2.Items.Add(item)
                    ComboBox3.Items.Add(item)
                    ComboBox4.Items.Add(item)
                    ComboBox5.Items.Add(item)
                    ComboBox6.Items.Add(item)
                    ComboBox7.Items.Add(item)
                Next
                ComboBox1.Text = ReturnAIOsetting("monday")
                ComboBox2.Text = ReturnAIOsetting("tuesday")
                ComboBox3.Text = ReturnAIOsetting("wednesday")
                ComboBox4.Text = ReturnAIOsetting("thursday")
                ComboBox5.Text = ReturnAIOsetting("friday")
                ComboBox6.Text = ReturnAIOsetting("saturday")
                ComboBox7.Text = ReturnAIOsetting("sunday")
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        MsgBox(TimeNow)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        'Check if any are empty
        'ErrorMessage
        Dim ErrMsg As String
        ErrMsg = "All days must be filled in, please review"
        Dim empty = Me.Controls.OfType(Of ComboBox)().Where(Function(txt) txt.Text.Length = 0)
        If empty.Any Then
            MsgBox(ErrMsg)
            Exit Sub
        Else
            SaveAIOsetting("monday", ComboBox1.Text)
            SaveAIOsetting("tuesday", ComboBox2.Text)
            SaveAIOsetting("wednesday", ComboBox3.Text)
            SaveAIOsetting("thursday", ComboBox4.Text)
            SaveAIOsetting("friday", ComboBox5.Text)
            SaveAIOsetting("saturday", ComboBox6.Text)
            SaveAIOsetting("sunday", ComboBox7.Text)
            MsgBox("Schedule has been saved!")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub
End Class