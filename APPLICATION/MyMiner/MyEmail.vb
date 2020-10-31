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

        RichTextBox1.Text = "Register with " & PubShared.HOSTED_WEBSITE


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