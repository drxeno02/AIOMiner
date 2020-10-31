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
        CNHaven = 5
        NeoScrypt = 6
        BitCore = 7
        Xevan = 8
        x16r = 9
        CNFast = 10
        lyra2rev3 = 11
        HEX = 12
        phi2 = 13
        ProgPow = 14
        Aion = 15
        BCD = 16
        x25x = 17
        MTP = 18
        Cuckaroo29 = 19
        CryptoNightR = 20

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
        'MHcollection.Add(Label146) '14
        MHcollection.Add(Label137) '15
        MHcollection.Add(Label155) '16
        MHcollection.Add(Label164) '17
        MHcollection.Add(Label173) '18
        MHcollection.Add(Label182) '19
        MHcollection.Add(Label190) '20
        MHcollection.Add(Label200) '21


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
        PDcollection.Add(Label135) '15
        PDcollection.Add(Label153) '16
        PDcollection.Add(Label162) '17
        PDcollection.Add(Label171) '18
        PDcollection.Add(Label180) '19
        PDcollection.Add(Label188) '19
        PDcollection.Add(Label198) '19

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
        LRcollection.Add(Label133) '15
        LRcollection.Add(Label151) '12
        LRcollection.Add(Label160) '13
        LRcollection.Add(Label169) '14
        LRcollection.Add(Label178) '15

        LRcollection.Add(Label186) '15
        LRcollection.Add(Label196) '15

        CBcollection.Add(EthashCombo) '0
        CBcollection.Add(CryptonightHeavyCombo) '1
        CBcollection.Add(phi1612Combo) '2
        CBcollection.Add(zhashCombo) '3
        CBcollection.Add(CryptonightSaberCombo) '4
        CBcollection.Add(CryptonightHavenCombo) '5
        CBcollection.Add(NeoScryptCombo) '6
        CBcollection.Add(BitCoreCombo) '7
        CBcollection.Add(Blake2bCombo) '8
        CBcollection.Add(X16RCombo) '9
        CBcollection.Add(CryptoNightFastCombo) '10
        CBcollection.Add(lyra2rev3Combo) '11
        CBcollection.Add(Hex5Combo) '12
        CBcollection.Add(phi2Combo) '13
        CBcollection.Add(ProgPowCombo) '14
        CBcollection.Add(AionCombo) '15
        CBcollection.Add(BCDCombo) '16
        CBcollection.Add(x25xCombo) '17
        CBcollection.Add(MTPCombo) '18
        CBcollection.Add(Cuckaroo29Combo) '19
        CBcollection.Add(CryptonightRCombo) '20

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
        BHoleCollection.Add(Button16)
        BHoleCollection.Add(Button18)
        BHoleCollection.Add(Button19)
        BHoleCollection.Add(Button20)
        BHoleCollection.Add(Button21)
        BHoleCollection.Add(Button22)
        BHoleCollection.Add(Button23)





    End Sub

    'Private AIO_Profitability As BackgroundWorker = New BackgroundWorker
    Private Sub Profitability_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        'Check Windows Defender
        If PubShared.WindowsDefenderPassed <> True Then
            MsgBox("Before Downloading Miners, Please whitelist your AIOMiner Folder.  Please click OK to open the system checks page.")
            MinerSettings.Show()
            Me.Close()
            Exit Sub
        End If


        PowerTXT.Text = PubShared.powercosts


        wtmCOMBO.Items.Add("30 Minutes")
        wtmCOMBO.Items.Add("1 Hour")
        wtmCOMBO.Items.Add("3 Hours")
        wtmCOMBO.Items.Add("6 Hours")
        wtmCOMBO.Items.Add("12 Hours")
        wtmCOMBO.Items.Add("24 Hours")

        If PubShared.Subscriber = False Then
            wtmCOMBO.Text = "24 Hours"
            wtmCOMBO.Enabled = False

        Else
            Select Case ReturnAIOsetting("WhatToMineUpdate")
                Case 1400
                    wtmCOMBO.Text = "24 Hours"
                Case 720
                    wtmCOMBO.Text = "12 Hours"
                Case 360
                    wtmCOMBO.Text = "6 Hours"
                Case 180
                    wtmCOMBO.Text = "3 Hours"
                Case 60
                    wtmCOMBO.Text = "1 Hours"
                Case 30
                    wtmCOMBO.Text = "30 Minutes"

            End Select

        End If

        PowerTXT.Text = ReturnAIOsetting("powercosts")

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
                Case "cryptonighthaven"
                    I = 5
                Case "neoscrypt"
                    I = 6
                Case "bitcore"
                    I = 7
                Case "xevan"
                    I = 8
                Case "x16r"
                    I = 9
                Case "cryptonightfast"
                    I = 10
                Case "lyra2rev3"
                    I = 11
                Case "hex"
                    I = 12
                Case "phi2"
                    I = 13
                Case "progpow"
                    I = 14
                Case "aion"
                    I = 15
                Case "bcd"
                    I = 16
                Case "x25x"
                    I = 17
                Case "mtp"
                    I = 18
                Case "cuckaroo29"
                    I = 19
                Case "cryptonightv4(r)"
                    I = 20

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

        Try


            'Shane requested this
            OtherCombo.Text = OtherCombo.Items(0)
        Catch ex As Exception

        End Try
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
        PubShared.worker = "t1fcTHmc6GmnXKKPC5WyEVchsDkSk4tgog1"
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
        PubShared.coin = "eth"
        PubShared.ip = "us1.ethermine.org"
        PubShared.port = "4444"
        PubShared.worker = "0x07bC4d2a376b770b69156f6f1616dbc033a94395"
        PubShared.pool = "us1.ethermine.org"
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
        CryptonightHeavyCombo.SelectedIndexChanged,
        phi1612Combo.SelectedIndexChanged,
        zhashCombo.SelectedIndexChanged,
        CryptonightSaberCombo.SelectedIndexChanged,
        CryptonightHavenCombo.SelectedIndexChanged,
        NeoScryptCombo.SelectedIndexChanged,
        BitCoreCombo.SelectedIndexChanged,
        Blake2bCombo.SelectedIndexChanged,
        X16RCombo.SelectedIndexChanged,
        CryptoNightFastCombo.SelectedIndexChanged,
        lyra2rev3Combo.SelectedIndexChanged,
        Hex5Combo.SelectedIndexChanged,
        phi2Combo.SelectedIndexChanged,
        AionCombo.SelectedIndexChanged,
        BCDCombo.SelectedIndexChanged,
        x25xCombo.SelectedIndexChanged,
        MTPCombo.SelectedIndexChanged,
        Cuckaroo29Combo.SelectedIndexChanged,
        CryptonightRCombo.SelectedIndexChanged,
        ProgPowCombo.SelectedIndexChanged




        Dim Profits As Profits = GetProfitabilityJson()
        Dim CCI As Integer = CInt(sender.tag.ToString)
        sender.name = sender.name.ToString.Replace("Combo", "")
        sender.name = sender.name.ToString.Replace("combo", "")
        sender.name = sender.name.ToString.Replace("COMBO", "")
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
        ElseIf WhosTagIsit = AlgoEnums.CNHaven Then
            whoitbe = "CryptoNightHaven"
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
        ElseIf WhosTagIsit = AlgoEnums.CNFast Then
            whoitbe = "cryptonightfast"
            WhoBeMyMiner = Label82
        ElseIf WhosTagIsit = AlgoEnums.lyra2rev3 Then
            whoitbe = "lyra2rev3"
            WhoBeMyMiner = Label50
        ElseIf WhosTagIsit = AlgoEnums.HEX Then
            whoitbe = "hex"
            WhoBeMyMiner = Label99
        ElseIf WhosTagIsit = AlgoEnums.phi2 Then
            whoitbe = "phi2"
            WhoBeMyMiner = Label19
        ElseIf WhosTagIsit = AlgoEnums.ProgPow Then
            whoitbe = "progpow"
            WhoBeMyMiner = Label131
        ElseIf WhosTagIsit = AlgoEnums.Aion Then
            whoitbe = "aion"
            WhoBeMyMiner = Label149
        ElseIf WhosTagIsit = AlgoEnums.BCD Then
            whoitbe = "bcd"
            WhoBeMyMiner = Label158
        ElseIf WhosTagIsit = AlgoEnums.x25x Then
            whoitbe = "x25x"
            WhoBeMyMiner = Label167
        ElseIf WhosTagIsit = AlgoEnums.MTP Then
            whoitbe = "mtp"
            WhoBeMyMiner = Label176
        ElseIf WhosTagIsit = AlgoEnums.Cuckaroo29 Then
            whoitbe = "cuckaroo29"
            WhoBeMyMiner = Label184
        ElseIf WhosTagIsit = AlgoEnums.CryptoNightR Then
            whoitbe = "CryptoNightv4(R)"
            WhoBeMyMiner = Label194
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
        PubShared.worker = "L6SfQvraSPjRfhHnFaLddxZEN88QeXp2Yha8gp94eYfxQv8f7Ma2WJxfRZungfhVkV4nXm5a9xxwM66K34GegypEHzRhi1Q"
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
        PubShared.worker = "Fg5pKqt3hS3hCaAEpsqS5pXgkDpftE1pgT"
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
        PubShared.algo = "CryptonightHaven"
        PubShared.coin = "CryptonightHaven"
        PubShared.ip = "haven.miner.rocks"
        PubShared.port = "4005"
        PubShared.worker = "hvxxuV9dwjTddP8CL4bRWyGvv5zU8rHv7M7L1xcVVUEzFjtei3QgGR1ZvdVevr8SYFXTScWoubRvAawzZyVuNVvZ3tntnwvobd"
        PubShared.pool = "haven.rocks"
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
        PubShared.worker = "bxcjcogpFg7Xpa8AG6PRhPXxVghUtZzKjK7xgxrauPz4GtgnVxnjMyDDHUyNGkaXpa2TYwgLRxzJ65kHyApNTWbh1SRH8sJDQ"
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
        PubShared.worker = "AIOminer.aiominer"
        PubShared.pool = "miningpoolhub"
        PubShared.password = "aiominer1"
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
        PubShared.worker = "aiominer.aiominer"
        PubShared.pool = "suprnova"
        PubShared.password = "aiominer1"
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
        PubShared.worker = "aiominer1.aiominer"
        PubShared.pool = "aiominer1"
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
        PubShared.worker = "aiominer.aiominer"
        PubShared.pool = "suprnova"
        PubShared.password = "aiominer1"
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
        PubShared.algo = "lyra2rev3"
        PubShared.coin = "Hana"
        PubShared.ip = "stratum+tcp://lyra2v3.mine.zergpool.com"
        PubShared.port = "4550"
        PubShared.worker = "PDJXMCDnhaZDJTqZgNXZeBJX1aXmJ1EuQR"
        PubShared.pool = "zerg"
        PubShared.password = ""
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
        PubShared.worker = "XR8LT6dQ3GyT5eBnLhjqkiGxPK9ebPNCdx"
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
        If PubShared.DebugMining = True Then
            PubShared.DebugMining = False

        End If

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
        PubShared.algo = "CryptoNightFast"
        PubShared.coin = "Conceal"
        PubShared.ip = "conceal.herominers.com"
        PubShared.port = "10361"
        PubShared.worker = "ccx7WHYjuCVKmvWe4ZedjfDTQKEodvVDS2RU9VrTrUC2UssmaLUj5emA17eMFBBXxD1kKkM32McuWQ5GcmousYix4bFbFTmp41"
        PubShared.pool = "conceal.herominers.com"
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
        PubShared.worker = "LdqyVYURvYnaQsAJocuffU8z4a4z99SNFR"
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

    Private Sub GroupBox17_Enter(sender As Object, e As EventArgs)

    End Sub

    'Private Sub Button17_Click(sender As Object, e As EventArgs)
    '    'Disable the benchmark tool
    '    'Show a new form 
    '    PubShared.algo = "CryptonightV8"
    '    PubShared.coin = "monero"
    '    PubShared.ip = "pool.supportxmr.com"
    '    PubShared.port = "5555"
    '    PubShared.worker = "44M5MoEdZBJgAk9GMH3wo27h5peF6X1mPGXyidX9ztt7BXgPfKEo6wpSqPmZvHgRAKCheqbsAgb6vE2Teq6MNAkJB14VfeE"
    '    PubShared.pool = "support.xmr"
    '    PubShared.password = "Benchmarker"
    '    CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
    '    Monitor_Start()
    '    Ethash.Interval = 500
    '    TargetDT = DateTime.Now.Add(CountDownFrom)

    '    WhosTagIsit = Button17.Tag
    '    CBcollection(WhosTagIsit).Enabled = False
    '    DisableControls()

    '    BenchLBL.Visible = True
    '    PictureBox1.Visible = True
    '    BenchLBL.Visible = True
    'End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "ProgPow"
        PubShared.coin = "Bitcoin Intrest"
        PubShared.ip = "us-1.pool.bci-server.com"
        PubShared.port = "3869"
        PubShared.worker = "iCW2MCAW9ZSRU8Z22QXKYVypGccS4T5ZBN"
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

    Private Sub Button18_Click(sender As Object, e As EventArgs) Handles Button18.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "aion"
        PubShared.coin = "aion"
        PubShared.ip = "aion-us.luxor.tech"
        PubShared.port = "3366"
        PubShared.worker = "0xa043f0cf384be759e6755174ff9066467da510fa97337ceefb37db1aec69ed93"
        PubShared.pool = "https://aionpool.tech/connect.html"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button18.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()
        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button19_Click(sender As Object, e As EventArgs) Handles Button19.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "bcd"
        PubShared.coin = "bcd"
        PubShared.ip = "stratum+tcp://bcd.coinfoundry.org"
        PubShared.port = "3056"
        PubShared.worker = "1Fx65qffYzhi5sg83Bp2ysmQpEtM9X36Fp"
        PubShared.pool = "https://coinfoundry.org/pool/bcd"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button19.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()
        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button20_Click(sender As Object, e As EventArgs) Handles Button20.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "x25x"
        PubShared.coin = "sinovate"
        PubShared.ip = "stratum+tcp://uspool.sinovate.io"
        PubShared.port = "3253"
        PubShared.worker = "SaAnmDEps6n5YUXR8kVGUTb9tbFVDXEeb7"
        PubShared.pool = "pool.sinovate.io"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button20.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()
        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button21_Click(sender As Object, e As EventArgs) Handles Button21.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "mtp"
        PubShared.coin = "zcoin"
        PubShared.ip = "stratum+tcp://xzc.2miners.com"
        PubShared.port = "8080"
        PubShared.worker = "aGo6MsX7At14VaHS9uubS4dDYc5jNRYFqs"
        PubShared.pool = "2miners"
        PubShared.password = ""
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button21.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()
        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button22_Click(sender As Object, e As EventArgs) Handles Button22.Click
        PictureBox1.Visible = True
        Ethash.Start()
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "cuckaroo29"
        PubShared.coin = "grin"
        PubShared.ip = "grin29.f2pool.com"
        PubShared.port = "13654"
        PubShared.worker = "aiominer"
        PubShared.pool = "f2pool.com"
        PubShared.password = "Miner123?"
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button22.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()
        BenchLBL.Visible = True
        PictureBox1.Visible = True
        Ethash.Start()
    End Sub

    Private Sub Button23_Click(sender As Object, e As EventArgs) Handles Button23.Click
        'Disable the benchmark tool
        'Show a new form 
        PubShared.algo = "CryptoNightv4(R)"
        PubShared.coin = "monero"
        PubShared.ip = "pool.supportxmr.com"
        PubShared.port = "5555"
        PubShared.worker = "44M5MoEdZBJgAk9GMH3wo27h5peF6X1mPGXyidX9ztt7BXgPfKEo6wpSqPmZvHgRAKCheqbsAgb6vE2Teq6MNAkJB14VfeE"
        PubShared.pool = "support.xmr"
        PubShared.password = "Benchmarker"
        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
        Monitor_Start()
        Ethash.Interval = 500
        TargetDT = DateTime.Now.Add(CountDownFrom)
        WhosTagIsit = Button23.Tag
        CBcollection(WhosTagIsit).Enabled = False
        DisableControls()

        BenchLBL.Visible = True
        PictureBox1.Visible = True
        BenchLBL.Visible = True
        Ethash.Start()
    End Sub

    Private Sub GroupBox17_Enter_1(sender As Object, e As EventArgs) Handles GroupBox17.Enter

    End Sub

    Private Sub PowerTXT_TextChanged(sender As Object, e As EventArgs) Handles PowerTXT.TextChanged

    End Sub

    Private Sub SaveBTN_Click(sender As Object, e As EventArgs) Handles SaveBTN.Click
        'Simple check if power can do maths, if not then wtf did they type in?
        Try
            If Convert.ToDouble(PowerTXT.Text) + 100 <> 9898776555554465 Then
                SaveAIOsetting("powercosts", PowerTXT.Text)
                PubShared.powercosts = PowerTXT.Text
            End If
        Catch ex As Exception
            MsgBox("Your power costs must only be numbers.  Unless your Power Company charges you in Apples, I can't really help.")
        End Try

        If PubShared.Subscriber = True Then
            '30 = 30 Minutes
            '60 = 1 Hour
            '180 = 3 Hours
            '360 = 6 Hours
            '720 = 12 Hours
            '1440 = 24 Hours
            If wtmCOMBO.Text.Contains("30 Minutes") Then
                PubShared.WTM_Check_Tick = 30
                PubShared.Timer2_Tick_Count = 0


            ElseIf wtmCOMBO.Text.Contains("1 Hour") Then
                PubShared.WTM_Check_Tick = 60
                PubShared.Timer2_Tick_Count = 0


            ElseIf wtmCOMBO.Text.Contains("3 Hours") Then
                PubShared.WTM_Check_Tick = 180
                PubShared.Timer2_Tick_Count = 0


            ElseIf wtmCOMBO.Text.Contains("6 Hours") Then
                PubShared.WTM_Check_Tick = 360
                PubShared.Timer2_Tick_Count = 0


            ElseIf wtmCOMBO.Text.Contains("12 Hours") Then
                PubShared.WTM_Check_Tick = 720
                PubShared.Timer2_Tick_Count = 0


            ElseIf wtmCOMBO.Text.Contains("24 Hours") Then
                PubShared.WTM_Check_Tick = 1440
                PubShared.Timer2_Tick_Count = 0
            End If
        Else
            PubShared.WTM_Check_Tick = 1440
            PubShared.Timer2_Tick_Count = 0

        End If

        SaveAIOsetting("WhatToMineUpdate", PubShared.WTM_Check_Tick.ToString)

    End Sub

    Private Sub WtmCOMBO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles wtmCOMBO.SelectedIndexChanged

    End Sub
End Class