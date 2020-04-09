Imports System.Text.RegularExpressions
Imports AIOminer.CoinMining
Imports AIOminer.Log
Imports AIOminer.General_Utils
Imports System.ComponentModel

Public Class TestPool
    Public Property CoinTM As String
    Public Property ipTM As String
    Public Property PortTM As String
    Public Property WalletTM As String
    Public Property PassTM As String
    Public Property AlgoTM As String
    Public Property PoolTM As String

    Public MessageFromMainForm As String
    Private timeLeft As Integer
    Private TargetDT As DateTime
    'Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(0.3)
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(0.3)


    Private Sub TestPool_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RadioButton1.BackColor = Color.Black
        RadioButton2.BackColor = Color.Black
        RadioButton3.BackColor = Color.Black

        RadioButton1.ForeColor = Color.White
        RadioButton2.ForeColor = Color.White
        RadioButton3.ForeColor = Color.White
        RadioButton1.Text = "Unknown"
        RadioButton2.Text = "Unknown"
        RadioButton3.Text = "Unknown"

        PubShared.DoingTestPool = True



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Button2.Enabled = False

        Dim PortTest As String


        If ipTM.StartsWith("ip") Then
            Dim result As Integer = MessageBox.Show(MsgBox("You sure about that Keith? Your pool has IP in it, it might be ok if you'r pool name has it, but it shouldn't be ip.poolname or ip.address etc..."), "AIOMiner Keith 1.0", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
            ElseIf result = DialogResult.No Then
                Exit Sub
            End If
        End If


        If PoolTM = "" Then
            PoolTM = "x"
        End If

        PortTest = ipTM.ToString


        If ipTM.Contains("stratum+tcp://") Then
            PortTest = ipTM.ToString
            PortTest = PortTest.Replace("stratum+tcp://", "")
        End If

        If ipTM.Contains("stratum+ssl://") Then
            PortTest = ipTM.ToString
            PortTest = PortTest.Replace("stratum+ssl://", "")
        End If

        If ipTM.Contains("http://") Then
            PortTest = ipTM.ToString
            PortTest = PortTest.Replace("http://", "")
        End If

        If ipTM.Contains("https://") Then
            PortTest = ipTM.ToString
            PortTest = PortTest.Replace("https://", "")
        End If

        If ipTM.Contains(" ") Then
            PortTest = ipTM.Replace(" ", "")
        End If

        'Try to open the port on the machine
        Dim clientSocket As New Net.Sockets.TcpClient()
        Dim stopWatch As New Stopwatch()
        Dim StopWatchTimeMs As Int32
        Dim I As Int16
        I = 0
        Dim PingTotals As Integer
        PingTotals = 0
        Dim PingTotalZ As Integer
        PingTotalZ = 0

        Try

            stopWatch.Start()
            clientSocket.Connect(PortTest, PortTM)
            clientSocket.Close()
            stopWatch.Stop()
            StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            PingTotals = PingTotals + StopWatchTimeMs

            'stopWatch.Start()
            'clientSocket.Connect(ipTM, PortTM)
            'clientSocket.Close()
            'stopWatch.Stop()
            'StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            'PingTotals = PingTotals + StopWatchTimeMs
            'Threading.Thread.Sleep(1000)

            'stopWatch.Start()
            'clientSocket.Connect(ipTM, PortTM)
            'clientSocket.Close()
            'stopWatch.Stop()
            'StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            'PingTotals = PingTotals + StopWatchTimeMs
            'Threading.Thread.Sleep(1000)

            'stopWatch.Start()
            'clientSocket.Connect(ipTM, PortTM)
            'clientSocket.Close()
            'stopWatch.Stop()
            'StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            'PingTotals = PingTotals + StopWatchTimeMs
            'Threading.Thread.Sleep(1000)

            'stopWatch.Start()
            'clientSocket.Connect(ipTM, PortTM)
            'clientSocket.Close()
            'stopWatch.Stop()
            'StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            'PingTotals = PingTotals + StopWatchTimeMs
            'Threading.Thread.Sleep(1000)

            RadioButton1.Text = PingTotals & " MS"

            If PingTotalZ > "100" Then
                RadioButton1.BackColor = Color.Yellow
                RadioButton1.ForeColor = Color.Black
            Else
                RadioButton1.BackColor = Color.Green
                RadioButton1.ForeColor = Color.Black
            End If


        Catch ex As Exception
            MsgBox(ex.Message)
            RadioButton1.BackColor = Color.Red
            RadioButton1.Text = "Failed"
            RadioButton1.ForeColor = Color.Black
            Button2.Enabled = True
            Exit Sub
        End Try


        'Set it back the way it was for the ip
        'ipTM = PoolSettings.TextBox2.Text

        Try
            CoinToMine(AlgoTM, CoinTM, ipTM, PortTM, WalletTM, PassTM, PoolTM)

            If PubShared.api = "" Then
                RadioButton3.BackColor = Color.Red
                RadioButton3.Text = "Unknown API"
                RadioButton3.ForeColor = Color.Black
            Else
                RadioButton3.BackColor = Color.Green
                RadioButton3.Text = PubShared.api
                RadioButton3.ForeColor = Color.Black
            End If
        Catch ex As Exception
            MsgBox("We are unable to start the application needed to run, please check the logs on the Main Page for more details")
            LogUpdate(ex.Message, eLogLevel.Err)
            RadioButton2.BackColor = Color.Red
            RadioButton2.Text = "Failed"
            RadioButton2.ForeColor = Color.Black
			Button2.Enabled = True
			PubShared.monitoring = False

			Exit Sub
        End Try
        Timer1.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        Timer1.Start()



    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim wasrunning As Boolean = False
        Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now)
        If ts.TotalMilliseconds > 0 Then
            Button2.Text = ts.ToString("mm\:ss")
        Else
            Try
                If MinerInstances.RunningMiners IsNot Nothing AndAlso MinerInstances.RunningMiners.Count > 0 Then
                    KillAllMiningApps()
                    wasrunning = True
                End If

                PubShared.speed = "0"

            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                PubShared.speed = "0"
            End Try

            If wasrunning Then
                PubShared.speed = "0"
                Button2.Enabled = True
                RadioButton2.BackColor = Color.Green
                RadioButton2.Text = "Passed!"
                RadioButton2.ForeColor = Color.Black
                LogUpdate("Pool Passed!")
                Button2.Enabled = True
                Button1.Enabled = True
                Button2.Text = "Start Test"
                Timer1.Stop()
            Else
                PubShared.speed = "0"
                Button2.Enabled = True
                RadioButton2.BackColor = Color.Red
                RadioButton2.Text = "Failed!"
                RadioButton2.ForeColor = Color.Black
                Button2.Enabled = True
                Button1.Enabled = True
                Button2.Text = "Start Test"
                Timer1.Stop()
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub TestPool_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        PubShared.DoingTestPool = False
        KillAllMiningApps()
        PubShared.monitoring = False

    End Sub

End Class