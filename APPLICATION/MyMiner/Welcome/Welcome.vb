Imports System.Management
Imports System.Text.RegularExpressions
Imports System.Threading

Imports System.Net
Imports System.IO
Imports System.ComponentModel
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils




Public Class Welcome

    Public Shared buttonmademedoit As Boolean = False
    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If ReturnAIOsetting("firstrun") = "False" Then
            'yMiner.Opacity = 100
            ' Me.Close()
        Else
            AIOMiner.Opacity = 0
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ReturnAIOsetting("firstrun") = "False" Then
            Me.Close()
        Else
            PubShared.FirstRun = True
            PubShared.Need2Download = True
            AIOMiner.Show()
            AIOMiner.Visible = True
            AIOMiner.Opacity = 100
            SaveAIOsetting("firstrun", "False")
            Me.Close()
        End If



    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        PubShared.FirstRun = True

        Welcome_New_1.Show()
        Me.Close()



    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        SaveAIOsetting("firstrun", "False")

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub Welcome_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim webAddress As String
        webAddress = "https://aiominer.com"
        Process.Start(webAddress)
    End Sub
    '    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ''        'RichTextBox1.Text = "This application was created to help with the mining of crypto currency's.
    'Under no obligations does this application take responsibility for damaging
    'your machine.  If you find yourself overclocking your hardware and you want
    'to blame someone please look in the mirror.  

    'Any information collected about your hardware will never be sold to anyone 
    'for any reason.  The purpose of this application is to help miners mine.

    'At some point in time we might collect what BIOS you are running on your cards
    'and help others to see what video card settings are being used while mining.
    'But when that comes, you will need to opt in/out of this process.  

    'The included applications were not made by/for AIO Miner. At
    'any point in time you want to update the cores of those applications feel free,
    'but there are no guarantees it will work with AIO Miner.  Any Fees implemeted by
    'those developers are implemented here.  It is not going to anyone in this project

    'In the event that you would like to contribute more to AIO Miner or you find yourself
    'looking for answers please visit the community on Discord.  https://discord.gg/vfj4m7F

    'http://AIOMINER.COM coming soon!

    'As always, you agree not to be a dick.  This is here for the advancement of batch files
    'we are better than that, you are better than that.  

    'NOTE: This version of AIOMiner is in Alpha and will stop working on 6/1/2018.  Please update
    'when you can to the latest version. 

    '-BRob (Bobby Roberts)
    'cisco.bobbyr@gmail.com
    '"
    '        'Check if file exists
    '        Label3.Text = Version

    '    End Sub

    '    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '        'If CheckBox2.Checked = True Then
    '        '    'Open up a webpage showing people how to send an email to be handled as an SMS
    '        '    Dim webAddress As String
    '        '    webAddress = "https://youtu.be/80dykaMvKOA"
    '        '    Process.Start(webAddress)
    '        'End If

    '        If CheckBox1.Checked = False Then
    '            MsgBox("The only thing you need to do, is not be a dick.  Check the box, and be cool man!")
    '            Exit Sub
    '        Else
    '            SaveAIOsetting("firstrun", "False")
    '        End If
    '        Me.Close()
    '        MyMiner.Opacity = 100


    '    End Sub

    '    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)

    '    End Sub
End Class