
Imports AIOminer.General_Utils



Public Class NewAPI
	Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		Dim l As New MyEmail
		l.Show()
		Me.Close()


	End Sub

	Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
		MsgBox("No problem, if you want to enable it later on check out the AIOSettings/Online settings to get you going!")
		SaveAIOsetting("dontaskwebsite", "True")
		Me.Close()

	End Sub

	Private Sub NewAPI_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub
End Class