Imports AIOminer.General_Utils

Public Class Restart
    Dim BACKSPACE As Boolean
	Private Sub Restart_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		'make restartrig
		ComboBox1.Items.Add("1 Hour")
		ComboBox1.Items.Add("3 Hours")
		ComboBox1.Items.Add("6 Hours")
		ComboBox1.Items.Add("12 Hours")
		ComboBox1.Items.Add("24 Hours")

        'add options for restart rig combobox2
        ComboBox2.Items.Add("1 Hours")
        ComboBox2.Items.Add("3 Hours")
        ComboBox2.Items.Add("6 Hours")
        ComboBox2.Items.Add("12 Hours")
		ComboBox2.Items.Add("24 Hours")

		If ReturnAIOsetting("restartrig") = "True" Then
			CheckBox4.Checked = True
			ComboBox2.Text = ReturnAIOsetting("restartrigtime")
		Else
			CheckBox4.Checked = False
		End If

		If ReturnAIOsetting("restartmining") = "True" Then
			CheckBox3.Checked = True
			ComboBox1.Text = ReturnAIOsetting("restartminingtime")
		Else
			CheckBox3.Checked = False
		End If



		TextBox1.Text = ReturnAIOsetting("reboot").ToString

		If ReturnAIOsetting("restart") = "True" Then
		Else
			CheckBox1.Checked = True
		End If

		If ReturnAIOsetting("checkgoogle") = "True" Then
			CheckBox2.Checked = True
		Else
			CheckBox2.Checked = False
		End If
	End Sub

	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            SaveAIOsetting("restart", "False")
        Else
            SaveAIOsetting("restart", "True")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Back Then
            BACKSPACE = True
        Else
            BACKSPACE = False
        End If
    End Sub

    Private Sub TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox1.KeyPress
        If BACKSPACE = False Then
            Dim allowedChars As String = "0123456789"
            If allowedChars.IndexOf(e.KeyChar) = -1 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Please put in a number")
            Exit Sub
        End If

        If TextBox1.Text = "0" Then
            MsgBox("Please put in a number other than 0")
            Exit Sub
        End If

        If TextBox1.Text = "00" Then
            MsgBox("Please put in a number other than 00")
            Exit Sub
        End If

        If TextBox1.Text = "000" Then
            MsgBox("Tripple 000's?  Ok I can only fix stupid so many times.  Don't set it to 0's brah")
			Exit Sub
        End If

		SaveAIOsetting("reboot", TextBox1.Text)


		'Check if restart rig is enabled
		If CheckBox4.Checked = True Then
			If ComboBox2.Text = "Disabled" Then
				MsgBox("Rig Restart is enabled, but no time was selected, womp womp")
				Exit Sub
			End If
			PubShared.AIORestartRig = True
			SaveAIOsetting("restartrig", "True")
			SaveAIOsetting("restartrigtime", ComboBox2.Text)
			PubShared.AIORestartRigtime = 999999
		Else
			PubShared.AIORestartRig = False
			SaveAIOsetting("restartrig", "False")
			PubShared.AIORestartRigtime = 999999
		End If

		'Check if restart mining is enabled
		If CheckBox3.Checked = True Then
			If ComboBox1.Text = "Disabled" Then
				MsgBox("Restart Mining is enabled, but no time was selected, womp womp")
				Exit Sub
			End If
			PubShared.AIORestartMining = True
			SaveAIOsetting("restartmining", "True")
			SaveAIOsetting("restartminingtime", ComboBox1.Text)
			PubShared.AIORestartMiningtime = 999999
		Else
			PubShared.AIORestartMining = False
			SaveAIOsetting("restartmining", "False")
			PubShared.AIORestartMiningtime = 999999
		End If

		MsgBox("Settings Saved")
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            SaveAIOsetting("checkgoogle", "True")
        Else
            SaveAIOsetting("checkgoogle", "False")
        End If
    End Sub
End Class