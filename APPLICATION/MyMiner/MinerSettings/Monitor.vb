Imports System.Management
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.IO
Imports System.ComponentModel

Public Class Monitor
    Public Shadows ParentForm As MyMiner
    Public rebootcount = 5
    Public MessageFromBob As String
    Public CoinFromMain As String
    Public IPFromMain As String
    Public PoolFromMain As String
    Public AlgoFromMain As String
    Public WorkerFromMain As String
    Public PassFromMain As String
    Public PortFromMain As String


    Private timeLeft As Integer
    Private TargetDT As DateTime
    Public MonitorMining As Thread
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(1)


    Private Sub Monitor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        My.Settings.Monitoring = True
        MyMiner.Label14.Text = "Following Schedule!"
        MonitorMining = New Thread(AddressOf MyMonitor)
        ParentForm.stopjob = False
        MonitorMining.Start()
        ' Me.TransparencyKey = Color.LightBlue
        ' Me.BackColor = Color.LightBlue


    End Sub
    Public Sub Closeme()
        MonitorMining.Abort()
    End Sub
    Private Sub MyMonitor()
        Dim running As Boolean
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        System.Threading.Thread.Sleep(10000)
        For i As Integer = 0 To 1 Step 0

            If My.Settings.Monitoring = False Then
                Exit For
                Me.Close()
            End If

            If ParentForm.stopjob = False And MonitorMining.ThreadState = ThreadState.Running Then
                If rebootcount <= 0 Then
                    SendEmail("5 Failures occured, we are rebooting " + System.Net.Dns.GetHostName, My.Settings.EMAIL)
                    LogUpdate("5 Failures, we are going to reboot!")
                    System.Diagnostics.Process.Start("ShutDown", "/r /t 01")
                End If

                running = False
                'Check for ZCash
                If My.Settings.algo = "Equihash" Then

                    If My.Settings.pool = "AIOTEST" Then
                        'Start the timer 3
                    End If

                    Try
                        For Each p As Process In System.Diagnostics.Process.GetProcesses
                            If p.ProcessName.Equals("miner") Then
                                running = True
                            End If
                        Next
                    Catch ex As Exception

                        'No idea why this would trigger, thus a crazy message as such
                        LogUpdate("Monitoring: Error 17765")
                        Exit Sub
                    End Try

                    'Check if errors were found in the log files
                    If My.Settings.ERRR = True Then
                        running = False
                    End If

                    If running = True Then
                        rebootcount = 5
                        Label3.Text = rebootcount
                        PictureBox2.Image = My.Resources.Resources.Checkmark
                    Else
                        'Need to check again for log's just to make sure
                        If running = True Then
                        Else
                            PictureBox2.Image = My.Resources.Resources.redx
                            LogUpdate("An error has been detected, we are restarting your mining!")
                            Dim hostname As String
                            hostname = System.Net.Dns.GetHostName
                            hostname = hostname.Replace(" ", " ")
                            hostname = hostname.Replace("-", "")
                            SendEmail("Errors detected on  " + hostname.ToString + ".  We are restarting mining.", My.Settings.EMAIL)
                            rebootcount = rebootcount - 1
                            Label3.Text = rebootcount.ToString
                            'MsgBox(AlgoFromMain & CoinFromMain & IPFromMain & PortFromMain & WorkerFromMain & PassFromMain & PoolFromMain)
                            'Exit Sub
                            CoinToMineNVIDIA(My.Settings.algo, My.Settings.coin, My.Settings.ip, My.Settings.port, My.Settings.worker, My.Settings.password, My.Settings.pool)

                        End If
                    End If
                End If


                If My.Settings.algo = "Lyra2REv2" Then
                    Try
                        'This is shit, come back to this and specify ccminer vs looking at every damn process
                        For Each p As Process In System.Diagnostics.Process.GetProcesses
                            If p.ProcessName.Equals("ccminer") Then
                                running = True
                            End If
                        Next
                    Catch ex As Exception
                        'No idea why this would trigger, thus a crazy message as such
                        LogUpdate("Monitoring: Error 17765")
                    End Try

                    If running = True Then
                        rebootcount = 5
                        Label3.Text = rebootcount
                        PictureBox2.Image = My.Resources.Resources.Checkmark
                    Else
                        PictureBox2.Image = My.Resources.Resources.redx
                        LogUpdate("We failed to find your mining running, restarting!")
                        Dim Hostname As String
                        Hostname = System.Net.Dns.GetHostName
                        Hostname = Hostname.Replace(" ", "")
                        SendEmail("Errors detected on  " + Hostname.ToString + ".  We are restarting mining.", My.Settings.EMAIL)
                        rebootcount = rebootcount - 1
                        Label3.Text = rebootcount.ToString
                        CoinToMineNVIDIA(My.Settings.algo, My.Settings.coin, My.Settings.ip, My.Settings.port, My.Settings.worker, My.Settings.password, My.Settings.pool)
                    End If
                End If


                If My.Settings.algo = "Ethash" Then
                    Try
                        'This is shit, come back to this and specify ccminer vs looking at every damn process
                        For Each p As Process In System.Diagnostics.Process.GetProcesses
                            If p.ProcessName.Contains("ethminer") Then
                                running = True
                            End If
                        Next
                    Catch ex As Exception
                        'No idea why this would trigger, thus a crazy message as such
                        LogUpdate("Monitoring: Error 17765")
                    End Try

                    If running = True Then
                        rebootcount = 5
                        Label3.Text = rebootcount
                        PictureBox2.Image = My.Resources.Resources.Checkmark
                    Else
                        PictureBox2.Image = My.Resources.Resources.redx
                        LogUpdate("We failed to find your mining running, restarting!")
                        Dim Hostname As String
                        Hostname = System.Net.Dns.GetHostName
                        Hostname = Hostname.Replace(" ", "")
                        SendEmail("Errors detected on  " + Hostname.ToString + ".  We are restarting mining.", My.Settings.EMAIL)
                        rebootcount = rebootcount - 1
                        Label3.Text = rebootcount.ToString
                        CoinToMineNVIDIA(My.Settings.algo, My.Settings.coin, My.Settings.ip, My.Settings.port, My.Settings.worker, My.Settings.password, My.Settings.pool)
                    End If
                End If

            Else
                Me.Close()
                Exit For
            End If

            System.Threading.Thread.Sleep(10000)
        Next
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick


    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer1.Stop()
        Me.Close()
        Timer2.Stop()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Monitor_Load_1(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Timer1_Tick_1(sender As Object, e As EventArgs) Handles Timer1.Tick

    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        'Profitability.Label6
    End Sub
End Class