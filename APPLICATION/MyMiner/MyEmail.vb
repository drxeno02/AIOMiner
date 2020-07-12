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
Imports System.Text.RegularExpressions

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
    '				Dim urlToPost As String = "{{api}}.aiominer.com/api/user_manager/user"
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
    '	Me.TopMost = False
    '	If ReturnAIOsetting("port") = "True" Then
    '		CheckBox1.Checked = True
    '	Else
    '		CheckBox1.Checked = False

    '	End If

    '	Try
    '		If AIOMinerWebAPI.checkApiOnline() Then

    '			'    Dim request As WebRequest = WebRequest.Create("{{api}}.aiominer.com/api")
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

    '				Dim urlToPost As String = "{{api}}.aiominer.com/api/email_manager/verify"
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
        Process.Start(PubShared.WEB_WIKI_LINK_MINER_UPDATES)
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
        If txtRigname.Text.ToLower.Contains("/") Then
            MsgBox("/ can't be used in your rigname")
            Exit Sub
        End If

        Dim cleanString As String = Regex.Replace(txtRigname.Text, "[^A-Za-z0-9\-]", "")





        SaveAIOsetting("apikey", txtApikey.Text)
        SaveAIOsetting("rigname", cleanString)

        Try


            'Check Subscription Status
            If checkPlanStatus() = True Then
                PubShared.Subscriber = True
                PubShared.ShouldBeUpdatingToApi = True

            Else
                PubShared.Subscriber = False
                PubShared.ShouldBeUpdatingToApi = True
            End If
        Catch ex As Exception
            PubShared.ShouldBeUpdatingToApi = False
            Label7.Text = "BAD API KEY"
            Label7.ForeColor = Color.Red
        End Try

        'Update UI
        If PubShared.Subscriber = True Then
            Label7.Text = "SUBSCRIBER"
            Label7.ForeColor = Color.Green
        Else
            If PubShared.ShouldBeUpdatingToApi = True Then
                Label7.Text = "NORMIE"
                Label7.ForeColor = Color.Gray
            End If


        End If


        MsgBox("Settings saved")



    End Sub

    Private Sub MyEmail_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RichTextBox1.Text = "Register on AIOMiner.com to get your API key (Check your Profile).  Alerts are managed online and you can monitor all of your rigs for free."

        If PubShared.ShouldBeUpdatingToApi = False Then
            Label2.ForeColor = Color.Red
            Label2.Text = "DISABLED/BAD API KEY"
            Button1.Text = "Enable"
        Else
            Label2.ForeColor = Color.Green
            Label2.Text = "ENABLED"
            Button1.Text = "Disable"
        End If

        Try
            If AIOMinerWebAPI.checkApiOnline() Then

                Dim request As WebRequest = WebRequest.Create(PubShared.API_LOCATION)
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

            If PubShared.Subscriber = True Then
                Label7.Text = "SUBSCRIBER"
                Label7.ForeColor = Color.Green

            Else
                If PubShared.ShouldBeUpdatingToApi = True Then


                    Label7.Text = "NORMIE"
                    Label7.ForeColor = Color.Gray
                Else
                    Label7.Text = "API KEY BAD"
                    Label7.ForeColor = Color.Red

                End If

            End If

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

    Private Sub txtRigname_TextChanged(sender As Object, e As EventArgs) Handles txtRigname.TextChanged

    End Sub
End Class