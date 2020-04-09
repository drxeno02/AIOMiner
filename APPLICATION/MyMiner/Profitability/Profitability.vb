Imports System.ComponentModel
Imports System.Text.RegularExpressions

Imports AIOminer.CoinMining
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils
Imports AIOminer.MinerInstances
Imports AIOminer.Log
Imports Newtonsoft.Json

Public Class Profitability
    Private WhosTagIsit As Integer = 0
    Private Ethcountdown = 45
    Private Loaded As Boolean = False
    Private EthHashRate = 0
    Private GPUSTOUSE As String = ""
    Private MHcollection As New List(Of Label)()
    Private PDcollection As New List(Of Label)()
    Private LRcollection As New List(Of Label)()
    Private CBcollection As New List(Of ComboBox)()
    Private BHoleCollection As New List(Of Button)()
    Private timeLeft As Integer
    Private TargetDT As DateTime
    'Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(0.3)
    Private CountDownFrom As TimeSpan = TimeSpan.FromMinutes(3)
    Private My_BgWorkerMonitor As BackgroundWorker = New BackgroundWorker   ' Monitor 


    'Private Class pinfoargs
    '    Property nvidiasetting As Boolean
    '    Property amdsetting As Boolean
    '    Property algo As String
    '    Property pass As String
    '    Property port As String
    '    Property ip As String
    '    Property worker As String
    '    Property PubShared.GPUTOUSE As String
    '    Property apppath As String
    '    Property intensity As String




    'End Class

    Private Enum AlgoEnums
        Ethash = 0
		CNHeavy = 1
		phi1612 = 2
		zhash = 3
		CNSaber = 4
		Lyra2REv2 = 5
        NeoScrypt = 6
        BitCore = 7
        Xevan = 8
        x16r = 9
        CryptoNightv7 = 10
        lyra2z = 11
		HEX = 12
		phi2 = 13
		CryptoNightv8 = 14
		ProgPow = 15
	End Enum
    Private Sub DisableControls()
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is Button Then
                ctrl.Enabled = False
            End If
            If TypeOf ctrl Is ComboBox Then
                ctrl.Enabled = False
            End If
        Next
    End Sub

    Private Sub EnableControls()
        Dim ctrl As Control
        For Each ctrl In Me.Controls
            If TypeOf ctrl Is Button Then
                ctrl.Enabled = True
            End If
            If TypeOf ctrl Is ComboBox Then
                ctrl.Enabled = True
            End If
        Next
    End Sub

    Private Sub COLH()
        MHcollection.Add(Label2) ' 0 
        MHcollection.Add(Label13) '1
        MHcollection.Add(Label24) '2
        MHcollection.Add(Label32) '3
        MHcollection.Add(Label40) '4
        MHcollection.Add(Label48) '5
        MHcollection.Add(Label96) '6
        MHcollection.Add(Label64) '7
        MHcollection.Add(Label72) '8
        MHcollection.Add(Label80) '9
        MHcollection.Add(Label88) '10
        MHcollection.Add(Label56) '11
		MHcollection.Add(Label105) '12
		MHcollection.Add(Label126) '13
		MHcollection.Add(Label146) '14
		MHcollection.Add(Label137) '15

		PDcollection.Add(Label4) ' 0 
        PDcollection.Add(Label11) '1
        PDcollection.Add(Label22) '2
        PDcollection.Add(Label30) '3
        PDcollection.Add(Label38) '4
        PDcollection.Add(Label46) '5
        PDcollection.Add(Label94) '6
        PDcollection.Add(Label62) '7
        PDcollection.Add(Label70) '8
        PDcollection.Add(Label78) '9
        PDcollection.Add(Label86) '10
        PDcollection.Add(Label54) '11
		PDcollection.Add(Label103) '12
		PDcollection.Add(Label124) '13
		PDcollection.Add(Label144) '14
		PDcollection.Add(Label135) '15

		LRcollection.Add(Label6) ' 0
        LRcollection.Add(Label9) ' 1
        LRcollection.Add(Label17) '2
        LRcollection.Add(Label28) '3
        LRcollection.Add(Label36) '4
        LRcollection.Add(Label44) '5
        LRcollection.Add(Label92) '6
        LRcollection.Add(Label60) '7
        LRcollection.Add(Label68) '8
        LRcollection.Add(Label76) '9
        LRcollection.Add(Label84) '10
        LRcollection.Add(Label52) '11
		LRcollection.Add(Label101) '12
		LRcollection.Add(Label122) '13
		LRcollection.Add(Label142) '14
		LRcollection.Add(Label133) '15

		CBcollection.Add(EthashCombo) '0
		CBcollection.Add(CNHeavyCombo) '1
		CBcollection.Add(phi1612Combo) '2
		CBcollection.Add(zhashCombo) '3
		CBcollection.Add(CryptonightSaberCombo) '4
		CBcollection.Add(Lyra2REv2Combo) '5
		CBcollection.Add(NeoScryptCombo) '6
		CBcollection.Add(BitCoreCombo) '7
		CBcollection.Add(Blake2bCombo) '8
		CBcollection.Add(X16RCombo) '9
		CBcollection.Add(CryptoNightv7Combo) '10
		CBcollection.Add(lyra2zCombo) '11
		CBcollection.Add(Hex5Combo) '12
		CBcollection.Add(phi2Combo) '13
		CBcollection.Add(cnv8COMBO) '14
		CBcollection.Add(ProgPowCombo) '15

		BHoleCollection.Add(Button3) '0
		BHoleCollection.Add(Button4) '1
		BHoleCollection.Add(Button5) 'Keep counting from here jhole
		BHoleCollection.Add(Button6)
		BHoleCollection.Add(Button7)
		BHoleCollection.Add(Button8)
		BHoleCollection.Add(Button9)
		BHoleCollection.Add(Button10)
		BHoleCollection.Add(Button11)
		BHoleCollection.Add(Button12)
		BHoleCollection.Add(Button13)
		BHoleCollection.Add(Button14)
		BHoleCollection.Add(Button15)
		BHoleCollection.Add(Button1)
		BHoleCollection.Add(Button17)
		BHoleCollection.Add(Button16)
		'BHoleCollection.Add(Button16)




	End Sub

	'Private AIO_Profitability As BackgroundWorker = New BackgroundWorker
	Private Sub Profitability_Load(sender As Object, e As EventArgs) Handles MyBase.Load



		Dim blahX As New Downloader()
		blahX.ShowDialog()

		My_BgWorkerMonitor.WorkerSupportsCancellation = True


		'Setup for pollings
		AddHandler My_BgWorkerMonitor.DoWork, AddressOf My_BgWorkerMonitor_DoWork
		AddHandler My_BgWorkerMonitor.ProgressChanged, AddressOf My_BgWorkerMonitor_Progress



		COLH()
		Dim I As Int16
		I = 0

		'Load em' up
		Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()
		Dim _Path As String
		_Path = ""

		'dev use only change later
		'PubShared.amd = True
		'remove me
		For Each blah In mpInfos.MinerProcesses()
			I = 9999
			Select Case blah.Algo.ToLower
				Case "ethash"
					I = 0
				Case "cryptonightheavy"
					I = 1
				Case "phi1612"
					I = 2
				Case "zhash"
					I = 3
				Case "cryptonightsaber"
					I = 4
				Case "lyra2rev2"
					I = 5
				Case "neoscrypt"
					I = 6
				Case "bitcore"
					I = 7
				Case "xevan"
					I = 8
				Case "x16r"
					I = 9
				Case "cryptonightv7"
					I = 10
				Case "lyra2z"
					I = 11
				Case "hex"
					I = 12
				Case "phi2"
					I = 13
				Case "cryptonightv8"
					I = 14
				Case "progpow"
					I = 15

			End Select

			For Each ALGOX In blah.Infos()
				If I = 9999 Then
					If OtherCombo.Items.Contains(blah.Algo.ToLower) Then
					Else
						OtherCombo.Items.Add(blah.Algo.ToLower)
					End If
				Else
					Try
						_Path = ReturnList(ALGOX.PATH).ToString
						'IF NVIDIA, PUT IN THE APPLICATIONS ONLY FOR NVIDIA
						If PubShared.nvidia = True Then
							If ALGOX.GPUx = "NVIDIA" Then
								If Not CBcollection.Item(I).Items.Contains(_Path) Then CBcollection.Item(I).Items.Add(_Path)
								' if Not X.items.contains(_Path) then X.items.add(_Path)
								If ALGOX.PREFERED = "1" Then
									CBcollection.Item(I).Text = _Path
									'CBcollection.Item(I).Text = ALGOX.EXECUTABLE.Replace(".exe", "")
								End If
							End If
							'    'Set the combo text to whatever is set as the primary
						End If

						If PubShared.amd = True Then
							'IF AMD, PUT IN THE APPLICATIONS ONLY FOR AMD
							If ALGOX.GPUx = "AMD" Then
								If Not CBcollection.Item(I).Items.Contains(_Path) Then CBcollection.Item(I).Items.Add(_Path)
								If ALGOX.PREFERED = "1" Then
									CBcollection.Item(I).Text = _Path
								End If
							End If
							'    'Set the combo text to whatever is set as the primary
						End If

					Catch ex As Exception
						MsgBox(ex.Message)
					End Try
				End If
			Next
		Next

		'Shane requested this
		OtherCombo.Text = OtherCombo.Items(0)

		Loaded = True

		PubShared.DoingProfitability = True


	End Sub
	Private Function ReturnList(list As String) As Group
		'Get a list of all of the miners, add it to a list
		Dim re1 As String = ".*?"   'Non-greedy match on filler
		Dim re2 As String = "(?:[a-z][a-z0-9_]*)"   'Uninteresting: var
		Dim re3 As String = ".*?"   'Non-greedy match on filler
		Dim re4 As String = "(?:[a-z][a-z0-9_]*)"   'Uninteresting: var
		Dim re5 As String = ".*?"   'Non-greedy match on filler
		Dim re6 As String = "((?:[a-z][a-z0-9_]*))" 'Variable Name 1
		Dim r As Regex = New Regex(re1 + re2 + re3 + re4 + re5 + re6, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
		Dim m As Match = r.Match(list)
		If (m.Success) Then
			Dim var1 = m.Groups(1)
			Return var1
		Else
			Return Nothing
		End If



	End Function
	Private Sub ChangePrimaryMiner(algo As String, Box As Object)
		Try
			Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()
			Dim TheMiners As List(Of MinerProcess) = mpInfos.MinerProcesses.ToList

			For Each blah In TheMiners
				If blah.Algo.ToLower = algo Then
					For Each ALGOX In blah.Infos()
						If PubShared.nvidia = True Then
							If ALGOX.GPUx = "NVIDIA" Then
								If ALGOX.PATH.Contains(Box) Then
									ALGOX.PREFERED = "1"
								Else
									ALGOX.PREFERED = " "
								End If
							End If
						End If
						If PubShared.amd = True Then
							If ALGOX.GPUx = "AMD" Then
								If ALGOX.PATH.Contains(Box) Then
									ALGOX.PREFERED = "1"
								Else
									ALGOX.PREFERED = " "
								End If
							End If
						End If
					Next
					Exit For
				End If
			Next
			mpInfos.MinerProcesses = TheMiners.ToArray
			SaveMinerProcJson(mpInfos)
		Catch ex As Exception

			LogUpdate(ex.Message, eLogLevel.Err)
		End Try
	End Sub
	Private Sub Button1_Click(sender As Object, e As EventArgs)



	End Sub

	Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "zhash"
		PubShared.coin = "btcz"
		PubShared.ip = "mine.equipool.1ds.us"
		PubShared.port = "50062"
        PubShared.worker = ""
        PubShared.pool = "Equipool"
		PubShared.password = "x"



		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		Monitor_Start()

		Ethash.Interval = 1000
		TargetDT = DateTime.Now.Add(CountDownFrom)
		WhosTagIsit = Button6.Tag
		CBcollection(WhosTagIsit).Enabled = False

		DisableControls()

		BenchLBL.Visible = True
		PictureBox1.Visible = True
		Ethash.Start()

	End Sub

	Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "ethash"
		PubShared.coin = "Akroma"
		PubShared.ip = "Mining.Akroma.org"
		PubShared.port = "8008"
        PubShared.worker = ""
        PubShared.pool = "mining.akroma"
		PubShared.password = "x"



		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		BenchLBL.Visible = True
		PictureBox1.Visible = True
		'Check to see if a download was needed
		Monitor_Start()
		Ethash.Interval = 1000
		TargetDT = DateTime.Now.Add(CountDownFrom)
		WhosTagIsit = Button3.Tag
		CBcollection(WhosTagIsit).Enabled = False
		DisableControls()
		BenchLBL.Visible = True
		PictureBox1.Visible = True
		Ethash.Start()



	End Sub

	Private Sub OtherCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OtherCombo.SelectedIndexChanged
		OtherCombo2.Items.Clear()

		Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()
		Dim _Path As String
		_Path = ""
		' Dim I As Integer

		'dev use only change later

		'remove me
		For Each blah In mpInfos.MinerProcesses()
			If blah.Algo.ToLower = OtherCombo.Text Then
				For Each ALGOX In blah.Infos()
					Try
						_Path = ReturnList(ALGOX.PATH).ToString
						'IF NVIDIA, PUT IN THE APPLICATIONS ONLY FOR NVIDIA
						If PubShared.nvidia = True Then
							If ALGOX.GPUx = "NVIDIA" Then
								If Not OtherCombo2.Items.Contains(_Path) Then OtherCombo2.Items.Add(_Path)
								' if Not X.items.contains(_Path) then X.items.add(_Path)
								If ALGOX.PREFERED = "1" Then
									OtherCombo2.Text = _Path
								End If
							End If
							'    'Set the combo text to whatever is set as the primary
						Else
							'IF AMD, PUT IN THE APPLICATIONS ONLY FOR AMD
							If ALGOX.GPUx = "AMD" Then
								If Not OtherCombo2.Items.Contains(_Path) Then OtherCombo2.Items.Add(_Path)
								If ALGOX.PREFERED = "1" Then
									OtherCombo2.Text = _Path
								End If
							End If
							'    'Set the combo text to whatever is set as the primary
						End If

					Catch ex As Exception
						MsgBox(ex.Message)
					End Try
				Next

			End If
		Next




	End Sub

	Private Sub ComboKiller(sender As Object, e As EventArgs) Handles _
		EthashCombo.SelectedIndexChanged,
		CNHeavyCombo.SelectedIndexChanged,
		phi1612Combo.SelectedIndexChanged,
		zhashCombo.SelectedIndexChanged,
		CryptonightSaberCombo.SelectedIndexChanged,
		Lyra2REv2Combo.SelectedIndexChanged,
		NeoScryptCombo.SelectedIndexChanged,
		BitCoreCombo.SelectedIndexChanged,
		Blake2bCombo.SelectedIndexChanged,
		X16RCombo.SelectedIndexChanged,
		CryptoNightv7Combo.SelectedIndexChanged,
		lyra2zCombo.SelectedIndexChanged,
		Hex5Combo.SelectedIndexChanged,
		phi2Combo.SelectedIndexChanged,
		cnv8COMBO.SelectedIndexChanged


		Dim Profits As Profits = GetProfitabilityJson()
		Dim CCI As Integer = CInt(sender.tag.ToString)
		sender.name = sender.name.ToString.Replace("Combo", "")
		Dim found As Boolean = False

		If Loaded = True Then
			Try
				'MsgBox(sender.name.ToString.ToLower & " " & sender.text)
				ChangePrimaryMiner(sender.name.ToString.ToLower, sender.text)
			Catch ex As Exception
				LogUpdate(ex.Message, eLogLevel.Err)
			End Try
		End If


		'This is only if it found it in the profitability json
		For Each Dilly In Profits.Profitability.Algorithm
			If Dilly.Type.Contains(sender.name.ToString.ToLower) Then
				If Dilly.Miner.Contains(sender.text) Then
					MHcollection.Item(CCI).Text = Dilly.MH
					PDcollection.Item(CCI).Text = Dilly.PowerDraw
					LRcollection.Item(CCI).Text = Dilly.LastTimeRan
					found = True
					'BHoleCollection.Item(CCI).Enabled = True
				End If
			End If
			If found = False Then
				MHcollection.Item(CCI).Text = "0"
				PDcollection.Item(CCI).Text = "N/A"
				LRcollection.Item(CCI).Text = "6/30/2012"
				'BHoleCollection.Item(CCI).Enabled = False
			End If
		Next

		'Enable the box cauz we found it my dude!
		BHoleCollection.Item(CCI).Enabled = True



	End Sub

	Private Sub Ethash_Tick(sender As Object, e As EventArgs) Handles Ethash.Tick
        Dim wasrunning As Boolean = False

        Dim whoitbe As String = ""
        Dim WhoBeMyMiner As Label = Nothing

		If WhosTagIsit = AlgoEnums.Ethash Then
			whoitbe = "ethash"
			WhoBeMyMiner = Label21
		ElseIf WhosTagIsit = AlgoEnums.CNHeavy Then
			whoitbe = "cryptonightheavy"
			WhoBeMyMiner = Label7
		ElseIf WhosTagIsit = AlgoEnums.phi1612 Then
			whoitbe = "phi1612"
			WhoBeMyMiner = Label15
		ElseIf WhosTagIsit = AlgoEnums.zhash Then
			whoitbe = "zhash"
			WhoBeMyMiner = Label26
		ElseIf WhosTagIsit = AlgoEnums.CNSaber Then
			whoitbe = "cryptonightsaber"
			WhoBeMyMiner = Label34
		ElseIf WhosTagIsit = AlgoEnums.Lyra2REv2 Then
			whoitbe = "lyra2rev2"
			WhoBeMyMiner = Label42
		ElseIf WhosTagIsit = AlgoEnums.NeoScrypt Then
			whoitbe = "neoscrypt"
			WhoBeMyMiner = Label90
		ElseIf WhosTagIsit = AlgoEnums.BitCore Then
			whoitbe = "bitcore"
			WhoBeMyMiner = Label58
		ElseIf WhosTagIsit = AlgoEnums.Xevan Then
			whoitbe = "xevan"
			WhoBeMyMiner = Label66
		ElseIf WhosTagIsit = AlgoEnums.x16r Then
			whoitbe = "x16r"
			WhoBeMyMiner = Label74
		ElseIf WhosTagIsit = AlgoEnums.CryptoNightv7 Then
			whoitbe = "cryptonightv7"
			WhoBeMyMiner = Label82
		ElseIf WhosTagIsit = AlgoEnums.lyra2z Then
			whoitbe = "lyra2z"
			WhoBeMyMiner = Label50
		ElseIf WhosTagIsit = AlgoEnums.HEX Then
			whoitbe = "hex"
			WhoBeMyMiner = Label99
		ElseIf WhosTagIsit = AlgoEnums.phi2 Then
			whoitbe = "phi2"
			WhoBeMyMiner = Label19
		ElseIf WhosTagIsit = AlgoEnums.CryptoNightv8 Then
			whoitbe = "cryptonightv8"
			WhoBeMyMiner = Label140
		ElseIf WhosTagIsit = AlgoEnums.ProgPow Then
			whoitbe = "progpow"
			WhoBeMyMiner = Label131
		End If


        If MinerInstances.Downloading = True Then
            BenchLBL.Text = "Downloading new miner!"
            My_BgWorkerMonitor.CancelAsync()
            KillAllMiningApps()
            MinerInstances.Benchmark = True
        Else
            MinerInstances.Benchmark = True
            Dim ts As TimeSpan = TargetDT.Subtract(DateTime.Now)
            BenchLBL.Text = "Benchmark Is running!"
            If ts.TotalMilliseconds > 0 Then

                WhoBeMyMiner.Text = ts.ToString("mm\:ss")

                LRcollection.Item(WhosTagIsit).Text = Date.Today
                PDcollection.Item(WhosTagIsit).Text = AIOMiner.Label3.Text.Replace("Current GPU Power Draw:", "")
                MHcollection.Item(WhosTagIsit).Text = PubShared.speed & " " & PubShared.speedtype


            Else
                MinerInstances.Benchmark = False
                BenchLBL.Text = "Done, closing mining..."

                Try
                    If MinerInstances.RunningMiners IsNot Nothing AndAlso MinerInstances.RunningMiners.Count > 0 Then
                        If My_BgWorkerMonitor IsNot Nothing AndAlso My_BgWorkerMonitor.WorkerSupportsCancellation = True Then
                            My_BgWorkerMonitor.CancelAsync()
                            KillAllMiningApps()
                            WhoBeMyMiner.Text = "180"
                            CBcollection(WhosTagIsit).Enabled = True
                            PubShared.api = ""
                            EnableControls()
                            PubShared.monitoring = False

                            Dim Editflag As Boolean = False

                            'Save results
                            Try
                                Dim Pinfo As Profits = GetProfitabilityJson()
                                Dim ThePinfo As List(Of Algorithm) = Pinfo.Profitability.Algorithm.ToList
                                Dim EditProfitz As Algorithm = ThePinfo.Find(Function(x) x.Type.ToLower = whoitbe.ToLower AndAlso x.Miner = CBcollection.Item(WhosTagIsit).Text)


                                If EditProfitz Is Nothing Then
                                    Dim NewPinfo As Algorithm = New Algorithm With {
                                        .Type = whoitbe,
                                        .LastTimeRan = Date.Today,
                                        .MH = MHcollection.Item(WhosTagIsit).Text,
                                        .PowerDraw = PDcollection.Item(WhosTagIsit).Text,
                                        .Miner = CBcollection.Item(WhosTagIsit).Text
                                    }
                                    ThePinfo.Add(NewPinfo)
                                Else
                                    Dim FoundExisting As Algorithm = ThePinfo.Find(Function(x) x.Type.ToLower = whoitbe.ToLower AndAlso x.Miner = CBcollection.Item(WhosTagIsit).Text)
                                    FoundExisting.LastTimeRan = Date.Today
                                    FoundExisting.MH = MHcollection.Item(WhosTagIsit).Text
                                    FoundExisting.PowerDraw = PDcollection.Item(WhosTagIsit).Text
                                    FoundExisting.Miner = CBcollection.Item(WhosTagIsit).Text
                                    Editflag = True
                                End If
                                Pinfo.Profitability.Algorithm = ThePinfo.ToArray
                                Dim appPath As String = Application.StartupPath()
                                Dim strJson As String = JsonConvert.SerializeObject(Pinfo)
                                Try
                                    System.IO.File.WriteAllText(appPath & "\Settings\AIOProfitability.json", strJson)
                                Catch ex As Exception
                                    LogUpdate(ex.Message, eLogLevel.Err)
                                    Ethash.Stop()
                                    EnableControls()
                                    PubShared.api = ""
                                    BenchLBL.Visible = False
                                    BenchLBL.Text = "Benchmarking Now!"
                                    PictureBox1.Visible = False
                                    PubShared.monitoring = False
                                    ' Label21.Text = "60"
                                End Try

                                EnableControls()
                                PubShared.api = ""
                                PubShared.monitoring = False
                                BenchLBL.Visible = False
                                BenchLBL.Text = "Benchmarking Now!"
                                PictureBox1.Visible = False
                                KillAllMiningApps()
                                PubShared.speed = 0
                                PubShared.Dualspeed = 0
                                CBcollection(WhosTagIsit).Enabled = True
                                'WhosTagIsit = 9999
                                MinerInstances.Benchmark = False

                                Ethash.Stop()
                                'Label21.Text = "60"
                            Catch ex As Exception
                                LogUpdate(ex.Message, eLogLevel.Err)
                            End Try
                        End If

                        wasrunning = True
                    End If

                Catch ex As Exception
                Finally
                    EnableControls()
                    PubShared.api = ""
                    PubShared.monitoring = False
                    BenchLBL.Visible = False
                    BenchLBL.Text = "Benchmarking Now!"
                    PictureBox1.Visible = False
                    KillAllMiningApps()
                    PubShared.speed = 0
                    PubShared.Dualspeed = 0
                    CBcollection(WhosTagIsit).Enabled = True
                    'WhosTagIsit = 9999
                    MinerInstances.Benchmark = False
                    Ethash.Stop()
                End Try
            End If
        End If


    End Sub
    Public Sub Monitor_Start()
        PubShared.monitoring = True

        If My_BgWorkerMonitor Is Nothing Then My_BgWorkerMonitor = New BackgroundWorker
        My_BgWorkerMonitor.WorkerSupportsCancellation = True
        My_BgWorkerMonitor.WorkerReportsProgress = True

        If Not My_BgWorkerMonitor.IsBusy Then
            My_BgWorkerMonitor.RunWorkerAsync()
        Else
            My_BgWorkerMonitor.CancelAsync()

        End If
    End Sub

    Private Sub My_BgWorkerMonitor_DoWork(ByVal sender As Object, ByVal e As DoWorkEventArgs)

        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)
        'YOUWANTTHIS
        'YOU WANT THIS
        Dim Monitor_running As Boolean = False

        ' For i As Integer = 0 To 1 Step 0
        Do While Not worker.CancellationPending
            Try
                'If PubShared.monitoring = False Then
                '    rebootcount = ReturnAIOsetting("reboot")
                '    Exit Do   '  For

                'End If

                'If stopjob = False Then 'And MonitorMining.ThreadState = ThreadState.Running Then
                '    If rebootcount <= 0 Then
                '        If ReturnAIOsetting("restart") = "True" Then
                '            SendEmail("5 Failures occured, we are rebooting " + System.Net.Dns.GetHostName, ReturnAIOsetting("email"))
                '            LogUpdate("5 Failures, we are going to reboot!")
                '            System.Diagnostics.Process.Start("ShutDown", "/r /t 01")
                '        Else
                '            LogUpdate("Restart Is disabled.")
                '            rebootcount = ReturnAIOsetting("reboot")
                '        End If
                '    End If

                Monitor_running = False


                'End If

                'If PubShared.process_running = "" Then
                '    'No clue, but maybe they fat fingered an manual entry on the json?
                '    LogUpdate("Unable to determine Algorithum used, unable to monitor! Critical Error!")
                '    Exit Do
                'End If


                Try
                    For Each p As Process In System.Diagnostics.Process.GetProcesses
                        If p.ProcessName.Equals(PubShared.process_running) Then
                            Monitor_running = True
                        End If
                    Next
                Catch ex As Exception
                    'No idea why this would trigger, thus a crazy message as such
                    LogUpdate("Monitoring: Error 17765")
                    Exit Sub
                End Try


                If Monitor_running = True Then
                    worker.ReportProgress(50)
                Else
                    'Need to check again for log's just to make sure
                    If Not Monitor_running Then
                        LogUpdate("An error has been detected, we are restarting your mining!")
                        Dim hostname As String
                        hostname = System.Net.Dns.GetHostName
                        hostname = hostname.Replace(" ", " ")
                        hostname = hostname.Replace("-", "")
                        'worker.ReportProgress(0)
                        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
                    End If
                End If

            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
            System.Threading.Thread.Sleep(5000)        ' 10 second delay   chgd to 5 sec 
            '     Next
        Loop

        CBcollection(WhosTagIsit).Enabled = True
        worker.ReportProgress(100)

        KillAllMiningApps()



    End Sub
    Public Sub My_BgWorkerMonitor_Progress(ByVal sender As Object, ByVal e As ProgressChangedEventArgs)
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        ' handle updating UI from UI thread to stop issue of updating for bgworker thread directely
        Try
            If e.ProgressPercentage = 50 Then               ' all is ok
                Benchmark = True

            ElseIf e.ProgressPercentage = 0 Then        ' miner failed so redx it
                Benchmark = True

            ElseIf e.ProgressPercentage = 100 Then      ' all done so cleanup
                Benchmark = False
            End If
        Catch ex As Exception
            LogUpdate("Error updating status bar via progress event of monitor worker...." & ex.Message)
        End Try


    End Sub

    Private Sub Label121_Click(sender As Object, e As EventArgs) Handles Label121.Click

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "cryptonightheavy"
		PubShared.coin = "loki"
		PubShared.ip = "loki.miner.rocks"
		PubShared.port = "5555"
        PubShared.worker = ""
        PubShared.pool = "miner rocks"
		PubShared.password = ""
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button4.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()

        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "phi1612"
		PubShared.coin = "folm"
		PubShared.ip = "stratum+tcp://us.bsod.pw"
		PubShared.port = "2150"
        PubShared.worker = ""
        PubShared.pool = "BSOD"
		PubShared.password = "x"
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		Monitor_Start()
		Ethash.Interval = 500
		TargetDT = DateTime.Now.Add(CountDownFrom)

		WhosTagIsit = Button5.Tag
		CBcollection(WhosTagIsit).Enabled = False
		DisableControls()

		BenchLBL.Visible = True
		PictureBox1.Visible = True
		Ethash.Start()
	End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "Lyra2rev2"
        PubShared.coin = "Lyra2rev2"
        PubShared.ip = "stratum+tcp://hub.miningpoolhub.com"
        PubShared.port = "20507"
        PubShared.worker = ""
        PubShared.pool = "miningpoolhub"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button8.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()

        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "CryptonightSaber"
		PubShared.coin = "BitTube"
		PubShared.ip = "bittube.miner.rocks"
		PubShared.port = "5555"
        PubShared.worker = ""
        PubShared.pool = "MinerRocks"
		PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button7.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()

        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "neoscrypt"
        PubShared.coin = "neoscrypt"
        PubShared.ip = "stratum+tcp://hub.miningpoolhub.com"
        PubShared.port = "20510"
        PubShared.worker = ""
        PubShared.pool = "miningpoolhub"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button9.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub BenchLBL_Click(sender As Object, e As EventArgs) Handles BenchLBL.Click


    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "bitcore"
        PubShared.coin = "bitcore"
        PubShared.ip = "stratum+tcp://btx.suprnova.cc"
        PubShared.port = "3629"
        PubShared.worker = ""
        PubShared.pool = "suprnova"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button10.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "xevan"
        PubShared.coin = "xevan"
        PubShared.ip = "stratum+tcp://bsd.suprnova.cc"
        PubShared.port = "8686"
        PubShared.worker = ""
        PubShared.pool = ""
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button11.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'Disable the benchmark tool
        MsgBox("This is a multi-algo-algo, a benchmark to test them all could take hours.  Take this as just an estimate of what to expect!")
        'Show a new form 
        PubShared.algo = "x16r"
        PubShared.coin = "decred"
        PubShared.ip = "stratum+tcp://rvn.suprnova.cc"
        PubShared.port = "6667"
        PubShared.worker = ""
        PubShared.pool = "suprnova"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button12.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "lyra2z"
        PubShared.coin = "GINCoin"
        PubShared.ip = " stratum+tcp://eu.bsod.pw"
        PubShared.port = "2159"
        PubShared.worker = ""
        PubShared.pool = "bsod.pw"
        PubShared.password = "c=GIN"
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button14.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "hex"
		PubShared.coin = "xdna"
		PubShared.ip = "stratum+tcp://pool.bsod.pw"
		PubShared.port = "2320"
        PubShared.worker = ""
        PubShared.pool = "BSOD"
		PubShared.password = "c=XDNA"
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button15.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()

        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PubShared.monitoring = False
        Me.Close()

    End Sub

    Private Sub Label19_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Profitability_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        Ethash.Stop()

        EnableControls()
        PubShared.api = ""
        PubShared.monitoring = False

        If Benchmark = True Then
            My_BgWorkerMonitor.CancelAsync()
        End If

        BenchLBL.Visible = False
        PictureBox1.Visible = False
        KillAllMiningApps()

        PubShared.DoingProfitability = False

    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "cryptonightv7"
        PubShared.coin = "etn"
        PubShared.ip = "pool.etn.spacepools.org"
        PubShared.port = "3333"
        PubShared.worker = ""
        PubShared.pool = "spacepool"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)

        WhosTagIsit = Button13.Tag
        CBcollection(WhosTagIsit).Enabled = False

        DisableControls()


        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        Ethash.Stop()
        EnableControls()



        My_BgWorkerMonitor.CancelAsync()
        PubShared.api = ""
        PubShared.monitoring = False
        PubShared.speed = 0
        PubShared.speedtype = ""
        BenchLBL.Visible = False
        PictureBox1.Visible = False
        KillAllMiningApps()
        CBcollection(WhosTagIsit).Enabled = True
        Dim CBNUM As Integer = CBcollection(WhosTagIsit).FindStringExact(CBcollection(WhosTagIsit).Text)
        Dim CBName As String = CBcollection(WhosTagIsit).Text
        CBcollection(WhosTagIsit).SelectedIndex = -1
        CBcollection(WhosTagIsit).Text = CBName
    End Sub

    Private Sub OtherCombo2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles OtherCombo2.SelectedIndexChanged
        If Loaded = True Then
            Try
                'MsgBox(sender.name.ToString.ToLower & " " & sender.text)
                ChangePrimaryMiner(OtherCombo.Text.ToLower, OtherCombo2.Text)
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        End If
    End Sub

    Private Sub GroupBox13_Enter(sender As Object, e As EventArgs) Handles GroupBox13.Enter

    End Sub

	Private Sub GroupBox5_Enter(sender As Object, e As EventArgs) Handles GroupBox5.Enter

	End Sub

	Private Sub GroupBox15_Enter(sender As Object, e As EventArgs) Handles GroupBox15.Enter

	End Sub

	Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "phi2"
		PubShared.coin = "lux"
		PubShared.ip = "stratum+tcp://us.bsod.pw"
		PubShared.port = "6667"
        PubShared.worker = ""
        PubShared.pool = "BSOD"
		PubShared.password = "x"
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		Monitor_Start()
		Ethash.Interval = 500
		TargetDT = DateTime.Now.Add(CountDownFrom)

		WhosTagIsit = Button1.Tag
		CBcollection(WhosTagIsit).Enabled = False
		DisableControls()

		BenchLBL.Visible = True
		PictureBox1.Visible = True
		Ethash.Start()
	End Sub

	Private Sub GroupBox16_Enter(sender As Object, e As EventArgs) Handles GroupBox16.Enter

	End Sub

	Private Sub GroupBox17_Enter(sender As Object, e As EventArgs) Handles GroupBox17.Enter

	End Sub

	Private Sub Button17_Click(sender As Object, e As EventArgs) Handles Button17.Click
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "CryptonightV8"
		PubShared.coin = "monero"
		PubShared.ip = "pool.supportxmr.com"
		PubShared.port = "5555"
        PubShared.worker = ""
        PubShared.pool = "support.xmr"
		PubShared.password = "Benchmarker"
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		Monitor_Start()
		Ethash.Interval = 500
		TargetDT = DateTime.Now.Add(CountDownFrom)

		WhosTagIsit = Button17.Tag
		CBcollection(WhosTagIsit).Enabled = False
		DisableControls()

		BenchLBL.Visible = True
		PictureBox1.Visible = True
		BenchLBL.Visible = True
	End Sub

	Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
		PictureBox1.Visible = True
		Ethash.Start()
		'Disable the benchmark tool
		'Show a new form 
		PubShared.algo = "ProgPow"
		PubShared.coin = "Bitcoin Intrest"
		PubShared.ip = "us-1.pool.bci-server.com"
		PubShared.port = "3869"
        PubShared.worker = ""
        PubShared.pool = "x"
		PubShared.password = ""
		CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
		Monitor_Start()
		Ethash.Interval = 500
		TargetDT = DateTime.Now.Add(CountDownFrom)
		WhosTagIsit = Button16.Tag
		CBcollection(WhosTagIsit).Enabled = False

		DisableControls()


		BenchLBL.Visible = True
		PictureBox1.Visible = True
		Ethash.Start()

	End Sub
End Class