Imports System.Reflection
Imports System.Text.RegularExpressions
Imports System.Net

Imports AIOminer.Log
Imports AIOminer.JSON_Utils
Imports Newtonsoft.Json

Public Class General_Utils

    Public Shared Function Plexiglass(dialog As Form) As DialogResult
        Using plexi = New Form()
            plexi.FormBorderStyle = FormBorderStyle.None
            plexi.Bounds = Screen.FromPoint(dialog.Location).Bounds
            plexi.StartPosition = FormStartPosition.Manual
            plexi.AutoScaleMode = AutoScaleMode.None
            plexi.ShowInTaskbar = False
            plexi.BackColor = Color.Black
            plexi.Opacity = 0.45
            plexi.Show()
            dialog.StartPosition = FormStartPosition.CenterParent
            Return dialog.ShowDialog(plexi)
        End Using
    End Function


    Public Shared Function CheckOpenPort(ip As String, port As Integer) As Boolean

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
            clientSocket.Connect(ip, port)
            clientSocket.Close()
            stopWatch.Stop()
            StopWatchTimeMs = stopWatch.ElapsedMilliseconds
            PingTotals = PingTotals + StopWatchTimeMs
        Catch ex As Exception
            Return False
        End Try

        If PingTotals <= "100000" Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Shared Function ReturnAIOsetting(ASettings As String) As String
        Dim ThingToReturn As String
        ThingToReturn = ""
        Try
            Dim SettingsConfig As mAIOS = GetAIOSettings()
            Dim TheConfig As List(Of MAINAPP) = SettingsConfig.AIOSs.MAINAPPs.ToList
            ThingToReturn = TheConfig.Find(Function(x) x.type = ASettings).value
            Return ThingToReturn
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        Return ThingToReturn

    End Function

    Public Shared Function GetWTMString()
        Dim Beginning As String
        Beginning = "https://whattomine.com/coins?utf8=%E2%9C%93"

        Dim Middle As String = ""

        Dim Ending As String
		Ending = "&l2z=true&eth=true&grof=true&x11gf=true&lre=true&ns=true&lbry=true&bk2bf=true&bk14=true&pas=true&skh=true&n5=true&factor%5Bcost%5D=0.1&sort=Profitability24&volume=0&revenue=24h&factor%5Bexchanges%5D%5B%5D=&factor%5Bexchanges%5D%5B%5D=abucoins&factor%5Bexchanges%5D%5B%5D=bitfinex&factor%5Bexchanges%5D%5B%5D=bittrex&factor%5Bexchanges%5D%5B%5D=cryptopia&factor%5Bexchanges%5D%5B%5D=hitbtc&factor%5Bexchanges%5D%5B%5D=poloniex&factor%5Bexchanges%5D%5B%5D=yobit&dataset=Main&commit=Calculate"
		'&xn=true&factor%5Bxn_hr%5D=4.8&
		Dim eth As Integer = 0
		Dim PHI1612 As Integer = 0
		Dim CryptoNightV8 As Integer = 0
		Dim CryptoNightV7 As Integer = 0
		Dim Lyra2REv2 As Integer = 0
		Dim ProgPow As Integer = 0
		Dim NeoScrypt As Integer = 0
        Dim TimeTravel10 As Integer = 0
        Dim X16R As Integer = 0
		Dim lyra2z As Integer = 0
		Dim Xevan As Integer = 0
		Dim phi2 As Integer = 0
		Dim zhash As Integer = 0
		Dim hex As Integer = 0
		Dim CNHeavy As Integer = 0
		Dim CNSaber As Integer = 0


		'AMDPHI1612 = PHI1612 +
		If PubShared.two80 > 0 Then

			eth = eth + (11 * PubShared.two80)
			CryptoNightV8 = CryptoNightV8 + (0 * PubShared.two80)
			ProgPow = ProgPow + (0 * PubShared.two80)
			Lyra2REv2 = Lyra2REv2 + (23000 * PubShared.two80)
			NeoScrypt = NeoScrypt + (490 * PubShared.two80)
			lyra2z = lyra2z + (0.0 * PubShared.two80)
			TimeTravel10 = TimeTravel10 + (0.0 * PubShared.two80)
			PHI1612 = PHI1612 + (0.0 * PubShared.two80)
			X16R = X16R + (0.0 * PubShared.two80)
			CryptoNightV7 = CryptoNightV7 + (490 * PubShared.two80)
			phi2 = phi2 + (0.0 * PubShared.two80)
			zhash = zhash + (0.0 * PubShared.two80)
			hex = hex + (0.0 * PubShared.two80)
			CNHeavy = CNHeavy + (0.0 * PubShared.two80)
			CNSaber = CNSaber + (0.0 * PubShared.two80)
			Xevan = Xevan + (0.0 * PubShared.two80)
			Middle = Middle & "&adapt_280=true&adapt_q_280x=" & PubShared.two80.ToString
        End If
        If PubShared.three80 > 0 Then

			eth = eth + (20 * PubShared.three80)
			TimeTravel10 = TimeTravel10 + (4.5 * PubShared.three80)
			PHI1612 = PHI1612 + (0.0 * PubShared.three80)
			X16R = X16R + (0.0 * PubShared.three80)
			CryptoNightV7 = CryptoNightV7 + (530 * PubShared.three80)
			CryptoNightV8 = CryptoNightV8 + (530 * PubShared.three80)
			ProgPow = ProgPow + (4.8 * PubShared.three80)
			Lyra2REv2 = Lyra2REv2 + (20000 * PubShared.three80)
			NeoScrypt = NeoScrypt + (350 * PubShared.three80)
			lyra2z = lyra2z + (0.25 * PubShared.three80)
			phi2 = phi2 + (0.0 * PubShared.three80)
			zhash = zhash + (0.0 * PubShared.three80)
			hex = hex + (0.0 * PubShared.three80)
			CNHeavy = CNHeavy + (500 * PubShared.three80)
			CNSaber = CNSaber + (500 * PubShared.three80)
			Xevan = Xevan + (0.0 * PubShared.three80)
			Middle = Middle & "&adapt_380=true&adapt_q_=380" & PubShared.three80.ToString
		End If

        If PubShared.fury > 0 Then

			eth = eth + (16.5 * PubShared.fury)
			TimeTravel10 = TimeTravel10 + (0 * PubShared.fury)
			PHI1612 = PHI1612 + (0.0 * PubShared.fury)
			X16R = X16R + (0.0 * PubShared.fury)
			CryptoNightV7 = CryptoNightV7 + (800 * PubShared.fury)
			CryptoNightV8 = CryptoNightV8 + (0 * PubShared.fury)
			ProgPow = ProgPow + (6.6 * PubShared.fury)
			Lyra2REv2 = Lyra2REv2 + (48500 * PubShared.fury)
			NeoScrypt = NeoScrypt + (1250 * PubShared.fury)
			lyra2z = lyra2z + (0.0 * PubShared.fury)
			phi2 = phi2 + (0.0 * PubShared.fury)
			zhash = zhash + (0.0 * PubShared.fury)
			hex = hex + (0.0 * PubShared.fury)
			CNHeavy = CNHeavy + (400 * PubShared.fury)
			CNSaber = CNSaber + (400 * PubShared.fury)
			Xevan = Xevan + (0.0 * PubShared.fury)
			Middle = Middle & "&adapt_fury=true&adapt_q_fury=" & PubShared.fury.ToString
		End If

        If PubShared.four70 > 0 Then

			eth = eth + (26 * PubShared.four70)
			TimeTravel10 = TimeTravel10 + (7.5 * PubShared.four70)
			PHI1612 = PHI1612 + (10.0 * PubShared.four70)
			X16R = X16R + (4.5 * PubShared.four70)
			CryptoNightV7 = CryptoNightV7 + (730 * PubShared.four70)
			CryptoNightV8 = CryptoNightV8 + (660.0 * PubShared.four70)
			ProgPow = ProgPow + (6.4 * PubShared.four70)
			Lyra2REv2 = Lyra2REv2 + (28000 * PubShared.four70)
			NeoScrypt = NeoScrypt + (680 * PubShared.four70)
			lyra2z = lyra2z + (0.4 * PubShared.four70)
			phi2 = phi2 + (0 * PubShared.four70)
			zhash = zhash + (0.0 * PubShared.four70)
			hex = hex + (0.0 * PubShared.four70)
			CNHeavy = CNHeavy + (590 * PubShared.four70)
			CNSaber = CNSaber + (590 * PubShared.four70)
			Xevan = Xevan + (0.0 * PubShared.four70)

			Middle = Middle & "&adapt_470=true&adapt_q_470=" & PubShared.four70.ToString
		End If

        If PubShared.four80 > 0 Then

			eth = eth + (29.5 * PubShared.four80)
			PHI1612 = PHI1612 + (15.0 * PubShared.four80)
			CryptoNightV7 = CryptoNightV7 + (860 * PubShared.four80)
			TimeTravel10 = TimeTravel10 + (9.0 * PubShared.four80)
			X16R = X16R + (7.0 * PubShared.four80)
			CryptoNightV8 = CryptoNightV8 + (850 * PubShared.four80)
			ProgPow = ProgPow + (7.9 * PubShared.four80)
			Lyra2REv2 = Lyra2REv2 + (35500 * PubShared.four80)
			NeoScrypt = NeoScrypt + (820 * PubShared.four80)
			lyra2z = lyra2z + (0.45 * PubShared.four80)
			phi2 = phi2 + (0 * PubShared.four80)
			zhash = zhash + (0.0 * PubShared.four80)
			hex = hex + (2.7 * PubShared.four80)
			CNHeavy = CNHeavy + (900 * PubShared.four80)
			CNSaber = CNSaber + (900 * PubShared.four80)
			Xevan = Xevan + (1.6 * PubShared.four80)

			Middle = Middle & "&adapt_480=true&adapt_q_480=" & PubShared.four80.ToString

		End If

        If PubShared.five70 > 0 Then

			eth = eth + (27.9 * PubShared.five70)
			PHI1612 = PHI1612 + (13.0 * PubShared.five70)
			CryptoNightV7 = CryptoNightV7 + (830 * PubShared.five70)
			TimeTravel10 = TimeTravel10 + (8.0 * PubShared.five70)
			X16R = X16R + (5.0 * PubShared.five70)
			CryptoNightV8 = CryptoNightV8 + (700 * PubShared.five70)
			ProgPow = ProgPow + (6.7 * PubShared.five70)
			Lyra2REv2 = Lyra2REv2 + (29500 * PubShared.five70)
			NeoScrypt = NeoScrypt + (700 * PubShared.five70)
			lyra2z = lyra2z + (0.42 * PubShared.five70)
			phi2 = phi2 + (13 * PubShared.five70)
			zhash = zhash + (0.0 * PubShared.five70)
			hex = hex + (0.0 * PubShared.five70)
			CNHeavy = CNHeavy + (610 * PubShared.five70)
			CNSaber = CNSaber + (610 * PubShared.five70)
			Xevan = Xevan + (0.0 * PubShared.five70)

			Middle = Middle & "&adapt_570=true&adapt_q_570=" & PubShared.five70.ToString

		End If

        If PubShared.five80 > 0 Then

			eth = eth + (30.2 * PubShared.five80)
			PHI1612 = PHI1612 + (15.0 * PubShared.five80)
			CryptoNightV7 = CryptoNightV7 + (860 * PubShared.five80)
			TimeTravel10 = TimeTravel10 + (8.2 * PubShared.five80)
			X16R = X16R + (7.0 * PubShared.five80)
			CryptoNightV8 = CryptoNightV8 + (850 * PubShared.five80)
			ProgPow = ProgPow + (7.9 * PubShared.five80)
			Lyra2REv2 = Lyra2REv2 + (35500 * PubShared.five80)
			NeoScrypt = NeoScrypt + (820 * PubShared.five80)
			lyra2z = lyra2z + (0.45 * PubShared.five80)
			phi2 = phi2 + (0 * PubShared.five80)
			zhash = zhash + (0.0 * PubShared.five80)
			hex = hex + (0.0 * PubShared.five80)
			CNHeavy = CNHeavy + (900 * PubShared.five80)
			CNSaber = CNSaber + (900 * PubShared.five80)
			Xevan = Xevan + (1.6 * PubShared.five80)

			Middle = Middle & "&adapt_580=true&adapt_q_580=" & PubShared.five80.ToString
		End If

        If PubShared.vega > 0 Then

			eth = eth + (40 * PubShared.vega)
			PHI1612 = PHI1612 + (0.0 * PubShared.vega)
			CryptoNightV7 = CryptoNightV7 + (1850 * PubShared.vega)
			TimeTravel10 = TimeTravel10 + (16.5 * PubShared.vega)
			X16R = X16R + (13 * PubShared.vega)
			CryptoNightV8 = CryptoNightV8 + (1800 * PubShared.vega)
			ProgPow = ProgPow + (17 * PubShared.vega)
			Lyra2REv2 = Lyra2REv2 + (75000 * PubShared.vega)
			NeoScrypt = NeoScrypt + (2000 * PubShared.vega)
			lyra2z = lyra2z + (1.05 * PubShared.vega)
			phi2 = phi2 + (0.0 * PubShared.vega)
			zhash = zhash + (0.0 * PubShared.vega)
			hex = hex + (0.0 * PubShared.vega)
			CNHeavy = CNHeavy + (1400 * PubShared.vega)
			CNSaber = CNSaber + (1400 * PubShared.vega)
			Xevan = Xevan + (0 * PubShared.vega)
			Middle = Middle & "&adapt_vega64=true&adapt_q_vega64=" & PubShared.vega.ToString
		End If

		'Nvidia
		If PubShared.seven50ti > 0 Then

			eth = eth + (0.46 * PubShared.seven50ti)
			PHI1612 = PHI1612 + (0.0 * PubShared.seven50ti)
			CryptoNightV7 = CryptoNightV7 + (250 * PubShared.seven50ti)
			TimeTravel10 = TimeTravel10 + (0 * PubShared.seven50ti)
			X16R = X16R + (0 * PubShared.seven50ti)
			CryptoNightV8 = CryptoNightV8 + (0 * PubShared.seven50ti)
			ProgPow = ProgPow + (0 * PubShared.seven50ti)
			Lyra2REv2 = Lyra2REv2 + (6640 * PubShared.seven50ti)
			NeoScrypt = NeoScrypt + (220 * PubShared.seven50ti)
			lyra2z = lyra2z + (0 * PubShared.seven50ti)
			phi2 = phi2 + (0.0 * PubShared.seven50ti)
			zhash = zhash + (0.0 * PubShared.seven50ti)
			hex = hex + (0.0 * PubShared.seven50ti)
			CNHeavy = CNHeavy + (0 * PubShared.seven50ti)
			CNSaber = CNSaber + (0.0 * PubShared.seven50ti)
			Middle = Middle & "&adapt_750Ti=true&adapt_q_750Ti=" & PubShared.seven50ti.ToString

		End If

		If PubShared.ten50ti > 0 Then

			eth = eth + (13.9 * PubShared.ten50ti)
			phi2 = phi2 + (2.4 * PubShared.ten50ti)
			zhash = zhash + (17 * PubShared.ten50ti)
			hex = hex + (5 * PubShared.ten50ti)
			CNHeavy = CNHeavy + (390 * PubShared.ten50ti)
			PHI1612 = PHI1612 + (9.5 * PubShared.ten50ti)
			CryptoNightV7 = CryptoNightV7 + (340 * PubShared.ten50ti)
			TimeTravel10 = TimeTravel10 + (11 * PubShared.ten50ti)
			X16R = X16R + (6 * PubShared.ten50ti)
			CryptoNightV8 = CryptoNightV8 + (170 * PubShared.ten50ti)
			ProgPow = ProgPow + (5.8 * PubShared.ten50ti)
			Lyra2REv2 = Lyra2REv2 + (18500 * PubShared.ten50ti)
			NeoScrypt = NeoScrypt + (420 * PubShared.ten50ti)
			lyra2z = lyra2z + (1.5 * PubShared.ten50ti)
			Xevan = Xevan + (1.7 * PubShared.ten50ti)
			CNSaber = CNSaber + (390 * PubShared.ten50ti)
			Middle = Middle & "&adapt_1050Ti=true&adapt_q_1050Ti=" & PubShared.ten50ti.ToString

		End If
        If PubShared.ten60 > 0 Then

			eth = eth + (22.5 * PubShared.ten60)
			PHI1612 = PHI1612 + (13.5 * PubShared.ten60)
			CryptoNightV7 = CryptoNightV7 + (520 * PubShared.ten60)
			TimeTravel10 = TimeTravel10 + (15 * PubShared.ten60)
			X16R = X16R + (9 * PubShared.ten60)
			CryptoNightV8 = CryptoNightV8 + (390 * PubShared.ten60)
			ProgPow = ProgPow + (7.8 * PubShared.ten60)
			Lyra2REv2 = Lyra2REv2 + (26500 * PubShared.ten60)
			NeoScrypt = NeoScrypt + (680 * PubShared.ten60)
			lyra2z = lyra2z + (2.1 * PubShared.ten60)
			phi2 = phi2 + (3.9 * PubShared.ten60)
			zhash = zhash + (27 * PubShared.ten60)
			hex = hex + (7.5 * PubShared.ten60)
			CNHeavy = CNHeavy + (590 * PubShared.ten60)
			Xevan = Xevan + (2.3 * PubShared.ten60)
			CNSaber = CNSaber + (590 * PubShared.ten60)
			Middle = Middle & "&adapt_10606=true&adapt_q_10606=" & PubShared.ten60.ToString

		End If


		If PubShared.ten70 > 0 Then
			eth = eth + (30 * PubShared.ten70)
			PHI1612 = PHI1612 + (23 * PubShared.ten70)
			CryptoNightV7 = CryptoNightV7 + (730 * PubShared.ten70)
			TimeTravel10 = TimeTravel10 + (25.5 * PubShared.ten70)
			X16R = X16R + (14 * PubShared.ten70)
			CryptoNightV8 = CryptoNightV8 + (520 * PubShared.ten70)
			ProgPow = ProgPow + (13 * PubShared.ten70)
			Lyra2REv2 = Lyra2REv2 + (44500 * PubShared.ten70)
			NeoScrypt = NeoScrypt + (1150 * PubShared.ten70)
			lyra2z = lyra2z + (3.6 * PubShared.ten70)
			Xevan = Xevan + (3.9 * PubShared.ten70)
			phi2 = phi2 + (5.6 * PubShared.ten70)
			zhash = zhash + (43 * PubShared.ten70)
			hex = hex + (11 * PubShared.ten70)
			CNHeavy = CNHeavy + (800 * PubShared.ten70)
			CNSaber = CNSaber + (800 * PubShared.ten70)

			'Middle = Middle & "&eth=true&factor%5Beth_hr%5D=29"
			Middle = Middle & "&adapt_1070=true&adapt_q_1070=" & PubShared.ten70.ToString
		End If

		'FIX PHI2 ABOVE HERE

		If PubShared.ten70ti > 0 Then

			eth = eth + (30.5 * PubShared.ten70ti)
			PHI1612 = PHI1612 + (26.5 * PubShared.ten70ti)
			CryptoNightV7 = CryptoNightV7 + (750 * PubShared.ten70ti)
			TimeTravel10 = TimeTravel10 + (26 * PubShared.ten70ti)
			X16R = X16R + (16 * PubShared.ten70ti)
			CryptoNightV8 = CryptoNightV8 + (530 * PubShared.ten70ti)
			ProgPow = ProgPow + (14 * PubShared.ten70ti)
			Lyra2REv2 = Lyra2REv2 + (53000 * PubShared.ten70ti)
			NeoScrypt = NeoScrypt + (1200 * PubShared.ten70ti)
			phi2 = phi2 + (6.5 * PubShared.ten70ti)
			zhash = zhash + (44 * PubShared.ten70ti)
			hex = hex + (11.2 * PubShared.ten70ti)
			CNHeavy = CNHeavy + (830 * PubShared.ten70ti)
			lyra2z = lyra2z + (4.3 * PubShared.ten70ti)
			Xevan = Xevan + (4.6 * PubShared.ten70ti)
			CNSaber = CNSaber + (830 * PubShared.ten70ti)
			Middle = Middle & "&adapt_1070Ti=true&adapt_q_1070Ti=" & PubShared.ten70ti.ToString

		End If

		If PubShared.ten80 > 0 Then

			eth = eth + (40 * PubShared.ten80)
			PHI1612 = PHI1612 + (30.2 * PubShared.ten80)
			CryptoNightV7 = CryptoNightV7 + (580 * PubShared.ten80)
			TimeTravel10 = TimeTravel10 + (30.5 * PubShared.ten80)
			X16R = X16R + (19 * PubShared.ten80)
			CryptoNightV8 = CryptoNightV8 + (550 * PubShared.ten80)
			ProgPow = ProgPow + (15.7 * PubShared.ten80)
			Lyra2REv2 = Lyra2REv2 + (55000 * PubShared.ten80)
			NeoScrypt = NeoScrypt + (1500 * PubShared.ten80)
			phi2 = phi2 + (6.7 * PubShared.ten80)
			zhash = zhash + (47 * PubShared.ten80)
			hex = hex + (13.5 * PubShared.ten80)
			CNHeavy = CNHeavy + (700 * PubShared.ten80)
			lyra2z = lyra2z + (4.4 * PubShared.ten80)
			Xevan = Xevan + (5.1 * PubShared.ten80)
			CNSaber = CNSaber + (700.0 * PubShared.ten80)
			Middle = Middle & "&adapt_1080=true&adapt_q_1080=" & PubShared.ten80.ToString
		End If

		If PubShared.ten80ti > 0 Then

			eth = eth + (50 * PubShared.ten80ti)
			PHI1612 = PHI1612 + (40.5 * PubShared.ten80ti)
			CryptoNightV7 = CryptoNightV7 + (850 * PubShared.ten80ti)
			TimeTravel10 = TimeTravel10 + (41.5 * PubShared.ten80ti)
			X16R = X16R + (24 * PubShared.ten80ti)
			CryptoNightV8 = CryptoNightV8 + (720 * PubShared.ten80ti)
			ProgPow = ProgPow + (19.5 * PubShared.ten80ti)
			Lyra2REv2 = Lyra2REv2 + (74000 * PubShared.ten80ti)
			NeoScrypt = NeoScrypt + (1900 * PubShared.ten80ti)
			lyra2z = lyra2z + (5.7 * PubShared.ten80ti)
			Xevan = Xevan + (6.9 * PubShared.ten80ti)
			phi2 = phi2 + (8.9 * PubShared.ten80ti)
			zhash = zhash + (63 * PubShared.ten80ti)
			hex = hex + (17 * PubShared.ten80ti)
			CNHeavy = CNHeavy + (1000 * PubShared.ten80ti)
			CNSaber = CNSaber + (1000.0 * PubShared.ten80ti)
			Middle = Middle & "&adapt_1080Ti=true&adapt_q_1080Ti=" & PubShared.ten80ti.ToString
		End If

		If PubShared.twenty80 > 0 Then

			eth = eth + (48 * PubShared.ten80ti)
			PHI1612 = PHI1612 + (25 * PubShared.ten80ti)
			CryptoNightV7 = CryptoNightV7 + (870 * PubShared.ten80ti)
			TimeTravel10 = TimeTravel10 + (46.5 * PubShared.ten80ti)
			X16R = X16R + (24.5 * PubShared.ten80ti)
			CryptoNightV8 = CryptoNightV8 + (820 * PubShared.ten80ti)
			ProgPow = ProgPow + (24.4 * PubShared.ten80ti)
			Lyra2REv2 = Lyra2REv2 + (88000 * PubShared.ten80ti)
			NeoScrypt = NeoScrypt + (2000 * PubShared.ten80ti)
			lyra2z = lyra2z + (5.1 * PubShared.ten80ti)
			Xevan = Xevan + (7.4 * PubShared.ten80ti)
			phi2 = phi2 + (10 * PubShared.ten80ti)
			zhash = zhash + (74 * PubShared.ten80ti)
			hex = hex + (20 * PubShared.ten80ti)
			CNHeavy = CNHeavy + (1000 * PubShared.ten80ti)
			CNSaber = CNSaber + (1000.0 * PubShared.ten80ti)
			Middle = Middle & "&adapt_2080=true&adapt_q_2080=" & PubShared.ten80ti.ToString
		End If

		If PubShared.twenty80ti > 0 Then

			eth = eth + (56.5 * PubShared.ten80ti)
			PHI1612 = PHI1612 + (33 * PubShared.ten80ti)
			CryptoNightV7 = CryptoNightV7 + (1200 * PubShared.ten80ti)
			TimeTravel10 = TimeTravel10 + (50 * PubShared.ten80ti)
			X16R = X16R + (30 * PubShared.ten80ti)
			CryptoNightV8 = CryptoNightV8 + (1200 * PubShared.ten80ti)
			ProgPow = ProgPow + (26.5 * PubShared.ten80ti)
			Lyra2REv2 = Lyra2REv2 + (96000 * PubShared.ten80ti)
			NeoScrypt = NeoScrypt + (2300 * PubShared.ten80ti)
			lyra2z = lyra2z + (7 * PubShared.ten80ti)
			Xevan = Xevan + (9.8 * PubShared.ten80ti)
			phi2 = phi2 + (12.8 * PubShared.ten80ti)
			zhash = zhash + (80 * PubShared.ten80ti)
			hex = hex + (21.5 * PubShared.ten80ti)
			CNHeavy = CNHeavy + (1200 * PubShared.ten80ti)
			CNSaber = CNSaber + (1200 * PubShared.ten80ti)
			Middle = Middle & "&adapt_2080ti=true&adapt_q_2080ti=" & PubShared.ten80ti.ToString
		End If

		'Add the Math in here
		Middle = Middle & "&eth=true&factor%5Beth_hr%5D=" & eth.ToString
		Middle = Middle & "&phi=true&factor%5Bphi_hr%5D=" & PHI1612.ToString
		Middle = Middle & "&cn7=true&factor%5Bcn7_hr%5D=" & CryptoNightV7.ToString
		Middle = Middle & "&lre=true&factor%5Blrev2_hr%5D=" & Lyra2REv2.ToString
        Middle = Middle & "&ns=true&factor%5Bns_hr%5D=" & NeoScrypt.ToString
        Middle = Middle & "&tt10=true&factor%5Btt10_hr%5D=" & TimeTravel10.ToString
        Middle = Middle & "&x16r=true&factor%5Bx16r_hr%5D=" & X16R.ToString
        Middle = Middle & "&l2z=true&factor%5Bl2z_hr%5D=" & lyra2z.ToString
		Middle = Middle & "&zh=true&factor%5Bzh_hr%5D=" & zhash.ToString
		Middle = Middle & "&xn=true&factor%5Bxn_hr%5D=" & Xevan.ToString
		Middle = Middle & "&phi2=true&factor%5Bphi2_hr%5D=" & phi2.ToString
		Middle = Middle & "&cnh=true&factor%5Bcnh_hr%5D=" & CNHeavy.ToString
		Middle = Middle & "&zh=true&factor%5Bzh_hr%5D=" & zhash.ToString
		Middle = Middle & "&hx=true&factor%5Bhx_hr%5D=" & hex.ToString
		Middle = Middle & "&ppw=true&factor%5Bppw_hr%5D=" & ProgPow.ToString
		Middle = Middle & "&cn8=true&factor%5Bcn8_hr%5D=" & CryptoNightV8.ToString
		Middle = Middle & "&cns=true&factor%5Bcns_hr%5D=" & CNSaber.ToString


		Return Beginning & Middle & Ending



    End Function
    Public Shared Function IsCurrentTimeBetween(ByVal dteStartTime As DateTime, ByVal dteEndTime As DateTime) As Boolean

        Dim startTime, endTime, currentTime As TimeSpan

        startTime = dteStartTime.TimeOfDay
        endTime = dteEndTime.TimeOfDay
        currentTime = Now.TimeOfDay

        If endTime < startTime Then
            'the times span midnight     
            Return (currentTime <= endTime)
        Else
            'the times do not span midnight
            Return (startTime <= currentTime AndAlso currentTime <= endTime)
        End If

    End Function
    Public Shared Sub SaveAIOsetting(ASettings As String, SettingValue As String)
        Dim thingtochange As MAINAPP
        Dim appPath As String = Application.StartupPath()
        'Find value
        Try
            Dim SettingsConfig As mAIOS = GetAIOSettings()
            Dim TheConfig As List(Of MAINAPP) = SettingsConfig.AIOSs.MAINAPPs.ToList
            thingtochange = TheConfig.Find(Function(x) x.type = ASettings)
            thingtochange.value = SettingValue
            'passback json
            SettingsConfig.AIOSs.MAINAPPs = TheConfig.ToArray
            Dim strJson As String = JsonConvert.SerializeObject(SettingsConfig)
            updateSettingsCache("aiosettings", strJson)
            Dim i As Int16 = 0
            'Try it 3 times :|
            'For realz!!!1!!!
            Try
                Do While i <= 3
                    Try
                        System.IO.File.WriteAllText(appPath & "\Settings\AIOSettings.json", strJson)
                        i = 4
                    Catch ex As Exception
                        i = i + 1
                    End Try
                Loop
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                Throw
            End Try
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            'Throw
        End Try



        'Change value
        'Save value

    End Sub

    Public Shared Function ValidateEmail(ByVal strCheck As String) As Boolean
        Try
            Dim vEmailAddress As New System.Net.Mail.MailAddress(strCheck)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function

    Public Shared Sub SetAllowUnsafeHeaderParsing20()
        Dim a As New System.Net.Configuration.SettingsSection
        Dim aNetAssembly As System.Reflection.Assembly = Assembly.GetAssembly(a.GetType)
        Dim aSettingsType As Type = aNetAssembly.GetType("System.Net.Configuration.SettingsSectionInternal")
        Dim args1 As Object()
#Disable Warning BC42104 ' Variable is used before it has been assigned a value
        Dim anInstance As Object = aSettingsType.InvokeMember("Section", BindingFlags.Static Or BindingFlags.GetProperty Or BindingFlags.NonPublic, Nothing, Nothing, args1)
#Enable Warning BC42104 ' Variable is used before it has been assigned a value
        Dim aUseUnsafeHeaderParsing As FieldInfo = aSettingsType.GetField("useUnsafeHeaderParsing", BindingFlags.NonPublic Or BindingFlags.Instance)
        aUseUnsafeHeaderParsing.SetValue(anInstance, True)
    End Sub
    Public Shared Sub KillAllMiningApps()

        ' make sure they all get killed ok 
        'Do Until MinerInstances.RunningMiners.Count = 0
        'Removed : Message	"Object reference not set to an instance of an object."	String

        'Do it twice
        Try
            If MinerInstances.RunningMiners IsNot Nothing Then
                For Each p As Process In MinerInstances.RunningMiners
                    If Not p.HasExited Then
                        p.Kill()
                    End If
                Next
            End If
            MinerInstances.RunningMiners = New List(Of Process)()
        Catch ex As Exception
            If ex.Message.ToUpper.Contains("ACCESS IS DENIED") Then
            Else
                LogUpdate(ex.Message, eLogLevel.Err)
            End If
        End Try

        Try
            If MinerInstances.RunningMiners IsNot Nothing Then
                For Each p As Process In MinerInstances.RunningMiners
                    If Not p.HasExited Then
                        p.Kill()
                    End If
                Next
            End If
            MinerInstances.RunningMiners = New List(Of Process)()
        Catch ex As Exception
            If ex.Message.ToUpper.Contains("ACCESS IS DENIED") Then
            Else
                LogUpdate(ex.Message, eLogLevel.Err)
            End If
        End Try

        'Loop

        'The Claymore Clause
        Try
            Dim CM As Boolean = False
            Dim p() As Process

            Do Until CM = True
                p = Process.GetProcessesByName("EthDcrMiner64")
                If p.Count > 0 Then
                    For Each Z As Process In System.Diagnostics.Process.GetProcessesByName("EthDcrMiner64")
                        Z.Kill()
                    Next
                Else
                    CM = True
                    Exit Do
                End If
                System.Threading.Thread.Sleep(2000)
            Loop
        Catch ex As Exception

        End Try


        'The Phoenix Clause
        Try
            Dim CM As Boolean = False
            Dim p() As Process

            Do Until CM = True
                p = Process.GetProcessesByName("PhoenixMiner")
                If p.Count > 0 Then
                    For Each Z As Process In System.Diagnostics.Process.GetProcessesByName("Phoenix")
                        Z.Kill()
                    Next
                Else
                    CM = True
                    Exit Do
                End If
                System.Threading.Thread.Sleep(2000)
            Loop
        Catch ex As Exception

        End Try


        'Kill ETHPill if it's running
        If ReturnAIOsetting("nvidia") = "True" Then
            Dim EP As Boolean = False
            Dim q() As Process

            Do Until EP = True
                q = Process.GetProcessesByName("OhGodAnETHlargementPill-r2")
                If q.Count > 0 Then
                    For Each Z As Process In System.Diagnostics.Process.GetProcessesByName("OhGodAnETHlargementPill-r2")
                        Z.Kill()
                    Next
                Else
                    EP = True
                    Exit Do
                End If
                System.Threading.Thread.Sleep(2000)
            Loop
        End If
    End Sub
    Public Shared Function SupportedPools() As String
        'Support for Ethermine
        Dim supported As String = ""

        If PubShared.donationsStarted = False Then


			If PubShared.ip.ToUpper.Contains("BSOD") Then
				supported = "https://bsod.pw/?address=" & PubShared.worker
			End If

			If PubShared.ip.ToUpper.Contains("AIOMINER") Then
                supported = "https://pool.aiominer.com/?address=" & PubShared.worker
            End If

            If PubShared.ip.ToLower.Contains("btgpool.pro") Then
                supported = "http://btgpool.pro/workers/" & PubShared.worker
            End If



            If PubShared.ip.ToUpper.Contains("ETHERMINE") Then
                Dim LL As String
                If PubShared.ip.ToUpper.Contains("ETC") Then
                    LL = "https://etc.ethermine.org/miners/" & PubShared.worker
                Else
                    LL = "https://ethermine.org/miners/" & PubShared.worker
                End If


                Dim re1 As String = "((?:[a-z][a-z]+))" 'Word 1
                Dim re2 As String = "(.)"   'Any Single Character 1
                Dim re3 As String = "(.)"   'Any Single Character 2
                Dim re4 As String = "(.)"   'Any Single Character 3
                Dim re5 As String = "((?:[a-z][a-z\.\d\-]+)\.(?:[a-z][a-z\-]+))(?![\w\.])"  'Fully Qualified Domain Name 1
                Dim re6 As String = "(.)"   'Any Single Character 4
                Dim re7 As String = "((?:[a-z][a-z]+))" 'Word 2
                Dim re8 As String = "(.)"   'Any Single Character 5
                Dim re9 As String = "((?:[a-z][a-z]*[0-9]+[a-z0-9]*))"  'Alphanum 1

                Dim r As Regex = New Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(LL)
                If (m.Success) Then
                    Dim alphanum1 = m.Groups(9)
                    If PubShared.ip.ToUpper.Contains("ETC") Then
                        supported = "https://etc.ethermine.org/miners/" & alphanum1.ToString
                    Else
                        supported = "https://ethermine.org/miners/" & alphanum1.ToString
                    End If

                Else
                    supported = LL
                End If

            End If



            'Support for FlyPool
            If PubShared.ip.ToUpper.Contains("FLYPOOL") Then
                Dim LL As String
                LL = "https://zcash.flypool.org/miners/" & PubShared.worker

                Dim re1 As String = "((?:[a-z][a-z]+))" 'Word 1
                Dim re2 As String = "(.)"   'Any Single Character 1
                Dim re3 As String = "(.)"   'Any Single Character 2
                Dim re4 As String = "(.)"   'Any Single Character 3
                Dim re5 As String = "((?:[a-z][a-z\.\d\-]+)\.(?:[a-z][a-z\-]+))(?![\w\.])"  'Fully Qualified Domain Name 1
                Dim re6 As String = "(.)"   'Any Single Character 4
                Dim re7 As String = "((?:[a-z][a-z]+))" 'Word 2
                Dim re8 As String = "(.)"   'Any Single Character 5
                Dim re9 As String = "((?:[a-z][a-z]*[0-9]+[a-z0-9]*))"  'Alphanum 1

                Dim r As Regex = New Regex(re1 + re2 + re3 + re4 + re5 + re6 + re7 + re8 + re9, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(LL)
                If (m.Success) Then
                    Dim alphanum1 = m.Groups(9)
                    supported = "https://zcash.flypool.org/miners/" & alphanum1.ToString
                Else

                    supported = LL
                End If
            End If
            Return supported
        Else

        End If

        Return ""

    End Function


    Public Shared Function TimeNow() As String
        Dim HMS As String
        HMS = TimeOfDay.Hour.ToString + ":" + TimeOfDay.Minute.ToString + ":" + TimeOfDay.Second.ToString
        Return HMS

    End Function

    ' http stuff


    Public Shared Function HttpPost(url As String, data As String) As String
        Dim req As WebRequest = WebRequest.Create(url)
        req.ContentType = "application/x-www-form-urlencoded"
        req.Method = "POST"

        Dim bytes() As Byte = System.Text.Encoding.ASCII.GetBytes(data)
        req.ContentLength = bytes.Length
        Dim os As System.IO.Stream = req.GetRequestStream()
        os.Write(bytes, 0, bytes.Length)
        os.Close()
        Dim resp As WebResponse = req.GetResponse()
        If resp Is Nothing Then Return Nothing
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(resp.GetResponseStream())
        Dim rtnval As String = sr.ReadToEnd().Trim()

        Return rtnval


    End Function





#Region "Unused?"

    'Public Shared Function RemoveCharacter(ByVal stringToCleanUp As String, ByVal characterToRemove As String) As String
    '    ' replace the target with nothing
    '    ' Replace() returns a new String and does not modify the current one
    '    Return stringToCleanUp.Replace(characterToRemove, "")
    'End Function



    ' Public LastSysTime As DateTime

    'Public Function logo(what As String) As String
    '    Dim appPath As String = Application.StartupPath()
    '    If what = "ZCash" Then
    '        Return appPath & "/images/zcash-logo-1.png"
    '    End If
    '    Return what
    'End Function





    'Public Function GetNISTTime(ByVal host As String) As DateTime

    '    Dim timeStr As String

    '    timeStr = ""


    '    Try
    '        Dim reader As New StreamReader(New TcpClient(host, 13).GetStream)
    '        'LastSysTime = DateTime.UtcNow()
    '        timeStr = reader.ReadToEnd()
    '        reader.Close()
    '    Catch ex As SocketException

    '    Catch ex As Exception

    '    End Try

    '    Dim jd As Integer = Integer.Parse(timeStr.Substring(1, 5))
    '    Dim yr As Integer = Integer.Parse(timeStr.Substring(7, 2))
    '    Dim mo As Integer = Integer.Parse(timeStr.Substring(10, 2))
    '    Dim dy As Integer = Integer.Parse(timeStr.Substring(13, 2))
    '    Dim hr As Integer = Integer.Parse(timeStr.Substring(16, 2))
    '    Dim mm As Integer = Integer.Parse(timeStr.Substring(19, 2))
    '    Dim sc As Integer = Integer.Parse(timeStr.Substring(22, 2))
    '    Dim Temp As Integer = CInt(AscW(timeStr(7)))

    '    Return New DateTime(yr + 2000, mo, dy, hr, mm, sc)

    'End Function

#End Region

End Class
