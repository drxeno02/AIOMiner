Option Strict On
Imports System.Net
Imports System.Net.Mail
Imports AIOminer.General_Utils
Imports AIOminer.Log
Imports AIOminer.Email
Imports AIOminer.AIOMinerWebAPI
Imports AIOminer.JSON_Utils
Imports System.IO
Imports Newtonsoft.Json
Imports System.Text

Public Class MyEmail
    Dim loaded As Boolean = False

    Dim mail As New MailMessage()

	'Private Sub Button1_Click(sender As Object, e As EventArgs)
	'	'Verify email
	'	Dim webClient As New WebClient()
	'	Dim resByte As Byte()
	'	Dim resString As String
	'	Dim reqString() As Byte

	'	Try
	'		If ValidateEmail(TextBox1.Text) = True Then
	'			'Confirm API is online
	'			If lblSTATUS.Text = "ONLINE" Then
	'				' Dim requestBody As String = ("{" & """Email"":""" & TextBox1.Text & """" & "," &
	'				'                                "Provider:BTCZ }")
	'				Dim dictData As New Dictionary(Of String, String)
	'				dictData.Add("email", TextBox1.Text)
	'				Dim urlToPost As String = "{{OLD_API_SITE}}api/user_manager/user"
	'				'  Dim postArray As Byte() = Encoding.ASCII.GetBytes(requestBody)
	'				'WARNING: RATE LIMIT AND BAN IN PLACE, I KNOW YOU CAN SEE THIS CODE
	'				'I MADE THIS SO YOU COULD SEE I'M NOT DOING ANYTHING "ILL GAINED"
	'				'IF YOU ABUSE THE SYSTEM WE WILL SHUT YOU DOWN, DON'T BE A JERK, JUST HAPPY MINING!
	'				Try
	'					webClient.Headers.Add("Content-Type", "application/json")
	'					Dim jsonstring As Object = JsonConvert.SerializeObject(dictData)

	'					reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData))

	'					resByte = webClient.UploadData(urlToPost, "post", reqString)
	'					' MsgBox(resByte.ToString)
	'					resString = Encoding.Default.GetString(resByte)
	'					webClient.Dispose()
	'					'Check if it said new user
	'					'MsgBox(resString)
	'					If resString.ToLower.Contains("new user registered") Then
	'						MsgBox("Please check your e-mail for your confirmation code!  Remember to check your Spam Filter!")
	'					ElseIf resString.ToLower.Contains("exist") Then
	'						MsgBox("Please check your e-mail, again...from awhile ago")
	'					End If

	'				Catch ex As Exception
	'					LogUpdate("Unable to send confirmation e-mail!  Try again later!")
	'					MsgBox("Error connecting to API!  try again later!" & " " & ex.Message)
	'				End Try


	'			Else
	'				LogUpdate("Unable to verify user email, api is offline")
	'				MsgBox("Looks like our API is offline, try again later!")
	'				Exit Sub

	'			End If
	'		Else
	'			LogUpdate("Invalid E-mail address used!")
	'			MsgBox("Invalid E-Mail address")
	'			Exit Sub
	'		End If
	'	Catch ex As Exception

	'	End Try
	'End Sub

	'Private Sub MyEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load
	'	RichTextBox1.Enabled = False



	'	loaded = False




	'	RichTextBox1.Text = "Register on AIOMiner.com, you can get your API key in your Profile.  Place it here and hit save! "
	'	Me.TopMost = True
	'	If ReturnAIOsetting("port") = "True" Then
	'		CheckBox1.Checked = True
	'	Else
	'		CheckBox1.Checked = False

	'	End If

	'	Try
	'		If AIOMinerWebAPI.checkApiOnline() Then

	'			'    Dim request As WebRequest = WebRequest.Create("{{OLD_API_SITE}}api")
	'			'    request.Method = "GET"
	'			'Dim response As WebResponse = request.GetResponse()
	'			'Dim inputstream1 As Stream = response.GetResponseStream()
	'			'Dim reader As New StreamReader(inputstream1)
	'			'Dim workspace As String = reader.ReadToEnd
	'			'inputstream1.Dispose()
	'			'reader.Close()
	'			''
	'			'If workspace.Contains("OK!") Then

	'			lblSTATUS.Text = "ONLINE"
	'			lblSTATUS.ForeColor = Color.Green
	'		Else
	'			lblSTATUS.Text = "OFFLINE"
	'			lblSTATUS.ForeColor = Color.Red
	'		End If
	'	Catch ex As Exception
	'		lblSTATUS.Text = "OFFLINE"
	'		lblSTATUS.ForeColor = Color.Red
	'	End Try

	'	TextBox1.Text = ReturnAIOsetting("worker")
	'	TextBox2.Text = ReturnAIOsetting("password")


	'	txtApikey.Text = ReturnAIOsetting("apikey")
	'	txtRigname.Text = ReturnAIOsetting("rigname")

	'	If txtApikey.Text <> "" Then
	'		PubShared.ApiKey = txtApikey.Text
	'	End If

	'	If txtRigname.Text <> "" Then
	'		PubShared.Rigname = txtRigname.Text
	'	End If

	'	loaded = True


	'End Sub

	'Private Sub Button3_Click(sender As Object, e As EventArgs)
	'	Dim webClient As New WebClient()
	'	Dim resByte As Byte()
	'	Dim resString As String
	'	Dim reqString() As Byte
	'	Try

	'		'Confirm API is online
	'		If lblSTATUS.Text = "ONLINE" Then
	'			'Check to see if they gave you an e-mail address
	'			If ValidateEmail(TextBox1.Text) = True Then



	'				Dim dictData As New Dictionary(Of String, String)
	'				dictData.Add("hash", TextBox2.Text)
	'				dictData.Add("email", TextBox1.Text)

	'				Dim urlToPost As String = "{{OLD_API_SITE}}api/email_manager/verify"
	'				'  Dim postArray As Byte() = Encoding.ASCII.GetBytes(requestBody)
	'				'WARNING: RATE LIMIT AND BAN IN PLACE, I KNOW YOU CAN SEE THIS CODE
	'				'I MADE THIS SO YOU COULD SEE I'M NOT DOING ANYTHING "ILL GAINED"
	'				'IF YOU ABUSE THE SYSTEM WE WILL SHUT YOU DOWN, DON'T BE A JERK, JUST HAPPY MINING!
	'				Try
	'					webClient.Headers.Add("Content-Type", "application/json")
	'					Dim jsonstring As Object = JsonConvert.SerializeObject(dictData)

	'					reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData))

	'					resByte = webClient.UploadData(urlToPost, "post", reqString)
	'					' MsgBox(resByte.ToString)
	'					resString = Encoding.Default.GetString(resByte)
	'					webClient.Dispose()
	'					'Check if it said new user
	'					'MsgBox(resString)
	'					If resString.ToLower.Contains("confirmed") Then
	'						'Set email flag
	'						'Why do we have an email flag?

	'						SaveAIOsetting("email", "True")
	'						'Save email address
	'						SaveAIOsetting("worker", TextBox1.Text)
	'						'Save confirmation code
	'						SaveAIOsetting("password", TextBox2.Text)
	'						'Enable Alerts
	'						SaveAIOsetting("port", "True")
	'						'Notify USER
	'						MsgBox("Confirmed, enjoy your alerts!  Keep this e-mail, use this e-mail and code for other AIOMiner Rigs and Products!")
	'					End If

	'				Catch ex As Exception

	'					MsgBox("Error connecting to API!  try again later!" & " " & ex.Message)
	'				End Try

	'			End If
	'		Else
	'			MsgBox("Please put in an e-mail address!")

	'		End If


	'	Catch ex As Exception

	'	End Try
	'End Sub

	'Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click

	'End Sub

	'Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs)
	'	If loaded = True Then
	'		If CheckBox1.Checked = True Then
	'			SaveAIOsetting("port", "True")
	'		Else
	'			SaveAIOsetting("port", "False")
	'		End If
	'	End If
	'End Sub

	'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


	'	Me.Close()

	'   End Sub

	Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
		Process.Start("{{WIKI_SITE}}index.php/Alerts")
	End Sub

	Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		If txtApikey.Text = "" Then
			MsgBox("invalid apikey/rigname")
			Exit Sub
		End If

		If txtRigname.Text = "" Then
			MsgBox("invalid apikey/rigname")
			Exit Sub
		End If

		SaveAIOsetting("apikey", txtApikey.Text)
		SaveAIOsetting("rigname", txtRigname.Text)

		MsgBox("Settings saved")



	End Sub

	Private Sub MyEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

		RichTextBox1.Text = "Register on AIOMiner.com to get your API key (Check your Profile).  Alerts are managed online and you can monitor all of your rigs for free."

		If PubShared.ShouldBeUpdatingToApi = False Then
			Label2.ForeColor = Color.Red
			Label2.Text = "Disabled"
			Button1.Text = "Enable"
		Else
			Label2.ForeColor = Color.Green
			Label2.Text = "Enabled"
			Button1.Text = "Disable"
		End If

		Try
			If AIOMinerWebAPI.checkApiOnline() Then

				Dim request As WebRequest = WebRequest.Create("{{API_SITE}}api")
				request.Method = "GET"
				Dim response As WebResponse = request.GetResponse()
				Dim inputstream1 As Stream = response.GetResponseStream()
				Dim reader As New StreamReader(inputstream1)
				Dim workspace As String = reader.ReadToEnd
				inputstream1.Dispose()
				reader.Close()
				'
				If workspace.Contains("OK!") Then

					lblSTATUS.Text = "ONLINE"
					lblSTATUS.ForeColor = Color.Green
				Else
					lblSTATUS.Text = "OFFLINE"
					lblSTATUS.ForeColor = Color.Red
				End If
			End If

			txtRigname.Text = ReturnAIOsetting("rigname")
			txtApikey.Text = ReturnAIOsetting("apikey")

		Catch ex As Exception
			lblSTATUS.Text = "OFFLINE"
			lblSTATUS.ForeColor = Color.Red
		End Try
	End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()

    End Sub

	Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

	End Sub

	Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
		If Button1.Text = "Disable" Then
			PubShared.ApiKey = ""
			PubShared.ShouldBeUpdatingToApi = False
			Button1.Text = "Enable"
			SaveAIOsetting("apienabled", "False")

		Else
			If txtApikey.Text = "" Then
				MsgBox("Hey, missing the apikey?")
				Exit Sub
			End If
			PubShared.ApiKey = txtApikey.Text
			PubShared.ShouldBeUpdatingToApi = True
			Button1.Text = "Disable"
			SaveAIOsetting("apienabled", "True")

		End If

		If PubShared.ShouldBeUpdatingToApi = False Then
			Label2.ForeColor = Color.Red
			Label2.Text = "Disabled"
			Button1.Text = "Enable"

		Else
			Label2.ForeColor = Color.Green
			Label2.Text = "Enabled"
			Button1.Text = "Disable"
		End If
	End Sub
End Class