Imports AIOminer.Log
Imports AIOminer.Email
Imports AIOminer.DeviceInfo
Imports AIOminer.MinerInstances
Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils






Imports System.Net
Imports System.IO
Imports System.Diagnostics
Imports System.Text
Imports System.Text.RegularExpressions
Imports HtmlAgilityPack
Imports Newtonsoft.Json

Public Class CoinMining

	Private WithEvents tb As New TextBox


	Public Shared Function WhatToMineToday()
		Dim Today As String = Date.Now.DayOfWeek
		Dim ShouldBeMining As String = ""
		Try


			ShouldBeMining = (New Dictionary(Of String, String) From {
			{"1", ReturnAIOsetting("monday")},
			{"2", ReturnAIOsetting("tuesday")},
			{"3", ReturnAIOsetting("wednesday")},
			{"4", ReturnAIOsetting("thursday")},
			{"5", ReturnAIOsetting("friday")},
			{"6", ReturnAIOsetting("saturday")},
			{"0", ReturnAIOsetting("sunday")}
		 })(Today)

            If ShouldBeMining = "Most Profitable" Then
                Dim foundit As Boolean
                For i = 0 To 4
                    If AIOMiner.ListView1.Items(i).SubItems(4).Text <> "" Then
                        ShouldBeMining = AIOMiner.ListView1.Items(i).SubItems(4).Text
                        foundit = True
                        Exit For
                    End If
                Next

                If foundit = False Then
                    LogUpdate("We were unable to change your coin to mine, as we didn't find any pools setup for the top 5 coins!")
                    ShouldBeMining = AIOMiner.ComboBox1.Text
                End If
            End If
		Catch ex As Exception
			LogUpdate(ex.Message, eLogLevel.Err)
		End Try




		Return ShouldBeMining

	End Function


	Public Shared Sub CoinToMine(algo As String, coin As String, ip As String, port As String, worker As String, pass As String, pool As String, Optional ByVal GetArgs As Boolean = False, Optional ByVal TestPool As Boolean = False)

		If Not GetArgs OrElse (Not PubShared.DoingProfitability) OrElse (Not PubShared.DoingTestPool) Then
			If TimerSettings.OkToMine(PubShared.TimedMiningSettings) = False Then
				KillAllMiningApps()
				LogUpdate("Timed Mining override in CoinToMine.  Mining will NOT be started...")
				Return
			End If
		End If


		Dim appPath As String = Application.StartupPath()
		Dim minerProcess As Process = Nothing


		'If blank password, set it to x
		pass = IIf(pass = "", "x", pass)

		Dim ccminer As String = ","

		If PubShared.amd = True Then
			If algo.ToLower = "equihash" Then
				ccminer = ""
			End If

			If algo.ToLower = "" Then
			End If
		End If

		If PubShared.nvidia = True Then
			If algo.ToLower = "equihash" Then
				ccminer = " "
			End If
		End If

		If PubShared.nvidia = True Then
			If algo.ToLower.Contains("zhash") Then
				ccminer = " "
			End If
		End If

		If algo.ToLower = "ethash" Then
			ccminer = ""
		End If




		Dim GPUSTOUSE As String = ""


		If ReturnAIOsetting("gpu0") = "True" Then GPUSTOUSE = "0"

		If PubShared.cardcount > 1 Then
			If ReturnAIOsetting("gpu1") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "1"
		End If
		If PubShared.cardcount > 2 Then
			If ReturnAIOsetting("gpu2") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "2"
		End If

		If PubShared.cardcount > 3 Then
			If ReturnAIOsetting("gpu3") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "3"
		End If

		If PubShared.cardcount > 4 Then
			If ReturnAIOsetting("gpu4") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "4"
		End If

		If PubShared.cardcount > 5 Then
			If ReturnAIOsetting("gpu5") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "5"
		End If
		If PubShared.cardcount > 6 Then
			If ReturnAIOsetting("gpu6") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "6"
		End If
		If PubShared.cardcount > 7 Then
			If ReturnAIOsetting("gpu7") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "7"
		End If
		If PubShared.cardcount > 8 Then
			If ReturnAIOsetting("gpu8") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "8"
		End If
		If PubShared.cardcount > 9 Then
			If ReturnAIOsetting("gpu9") = "True" Then GPUSTOUSE = GPUSTOUSE & ccminer & "9"
		End If
		If PubShared.cardcount > 10 Then
			If ReturnAIOsetting("gpu10") = "True" Then
				If algo.ToLower = "ethash" Then
					GPUSTOUSE = GPUSTOUSE & ccminer & "a"
				Else
					GPUSTOUSE = GPUSTOUSE & ccminer & "10"
				End If
			End If

		End If

		If PubShared.cardcount >= 11 Then
			If ReturnAIOsetting("gpu11") = "True" Then
				If algo.ToLower = "ethash" Then
					GPUSTOUSE = GPUSTOUSE & ccminer & "b"
				Else
					GPUSTOUSE = GPUSTOUSE & ccminer & "11"
				End If
			End If
		End If

		If PubShared.cardcount >= 12 Then
			If ReturnAIOsetting("gpu12") = "True" Then
				If algo.ToLower = "ethash" Then
					GPUSTOUSE = GPUSTOUSE & ccminer & "c"
				Else
					GPUSTOUSE = GPUSTOUSE & ccminer & "12"
				End If
			End If
		End If


		'Check to see if they disabled both
		If PubShared.amd = False AndAlso PubShared.nvidia = False Then
			LogUpdate("You have disabled both AMD/NVIDIA, Not much we can do here!")
			AIOMiner.Button1.Text = "Start"
			AIOMiner.Button1.BackgroundImage = My.Resources.Resources.START

			PubShared.monitoring = False
			Exit Sub
		End If



		'Check if user wants to disable all AMD
		If ReturnAIOsetting("disableamd") = "True" Then
			LogUpdate("You have disabled AMD")
			If PubShared.amd = True Then
				If PubShared.nvidia = False Then
					LogUpdate("Might want to rethink that disable setting again...")
					AIOMiner.Button1.Text = "Start"
					AIOMiner.Button1.BackgroundImage = My.Resources.Resources.START
					PubShared.monitoring = False
					Exit Sub
				Else
					PubShared.amd = False
				End If
			End If
			'PubShared.monitoring = False

		End If

		'Check if user wants to disable all Nvidia
		If ReturnAIOsetting("disablenvidia") = "True" Then
			LogUpdate("You have disabled NVIDIA")
			If PubShared.nvidia = True Then
				If PubShared.amd = False Then
					LogUpdate("Might want to rethink that disable setting again...")
					AIOMiner.Button1.Text = "Start"
					AIOMiner.Button1.BackgroundImage = My.Resources.Resources.START
					PubShared.monitoring = False
					Exit Sub
				Else
					PubShared.nvidia = False
				End If
			End If
		End If



		'Check if user wants to disable all Nvidia

		' init running miner processes collection if needed
		If MinerInstances.RunningMiners Is Nothing Then
			MinerInstances.RunningMiners = New List(Of Process)()
		End If

		Try
			If GetArgs Then
				If TestPool Then
					LogUpdate("Started Mining " + coin + "  On Pool:" & pool)
				Else
				End If
			End If


			Dim px As ProcessStartInfo = GetMinerProcessStartInfo(PubShared.nvidia, PubShared.amd, algo, pass, port, ip, worker, GPUSTOUSE)

            If px Is Nothing Then
                'LogUpdate("Looks like you are missing a miner?  We are downloading it for you now...")
                PubShared.MinerMissing = True
                Exit Sub
            Else
                PubShared.MinerMissing = False
            End If

			If GetArgs Then
				PubShared.currentargs = px.Arguments
			Else


				'Check if Lyra2v2 and AMD


				If ReturnAIOsetting("nvidia") = "True" Then

					Try
						If PubShared.algo.ToLower = "ethash" Then

							'Start the Process
							Dim pHelp As New ProcessStartInfo
							pHelp.FileName = appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill-r2.exe"
							pHelp.Arguments = ""
							pHelp.UseShellExecute = True
							pHelp.WindowStyle = ProcessWindowStyle.Normal
							Dim proc As Process = Process.Start(pHelp)
						End If


						If PubShared.algo.ToLower.Contains("cryptonight") Then
							'Start the Process
							Dim pHelp As New ProcessStartInfo
							pHelp.FileName = appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill-r2.exe"
							pHelp.Arguments = ""
							pHelp.UseShellExecute = True
							pHelp.WindowStyle = ProcessWindowStyle.Normal
							Dim proc As Process = Process.Start(pHelp)
						End If

					Catch ex As Exception
						LogUpdate(ex.Message)
					End Try

				End If



				'if lyclkMiner setup new info
				If PubShared.process_running.ToLower.Contains("lyclminer") Then


					Dim FilePath As String = appPath & "\Miners\AMD\lyclminer\lyclMiner.conf"
					Try
						File.WriteAllText(FilePath, File.ReadAllText(FilePath).Replace("stratum+tcp://example.com:port", PubShared.ip & ":" & PubShared.port))
					Catch ex As Exception
						LogUpdate("Unable to set path for lyclminer")
					End Try

					Try
						File.WriteAllText(FilePath, File.ReadAllText(FilePath).Replace("""user""", """" & PubShared.worker & """"))
					Catch ex As Exception
						LogUpdate("Unable to set user for lyclminer")
					End Try

					Try

						File.WriteAllText(FilePath, File.ReadAllText(FilePath).Replace("123", PubShared.password))
					Catch ex As Exception
						LogUpdate("Unable to set password for lyclminer")
					End Try
				End If


                px.WorkingDirectory = PubShared.WorkingDirectory
                If PubShared.DebugMining = True Then
                    Dim shittorun As String = """" & px.FileName.ToString & """" & " " & px.Arguments.ToString
                    ' Create a mother fucking batch file
                    Dim sb As New System.Text.StringBuilder
                    sb.AppendLine("@echo off")
                    sb.AppendLine("cls()")
                    sb.AppendLine(": begin()")
                    sb.AppendLine("echo ::::: AIOMINER - DEBUG MODE ENABLED :::::")
                    sb.AppendLine(shittorun)
                    sb.AppendLine("echo ::::: AIOMINER - Please close this window when you are done seeing why your gpu's hate you :::::")
                    sb.AppendLine("pause")

                    IO.File.WriteAllText(appPath + "\Miners\debugTest.bat", sb.ToString())

                    Process.Start(appPath + "\Miners\debugTest.bat")
                Else
                    'px.RedirectStandardOutput = True
                    'px.RedirectStandardError = True
                    px.UseShellExecute = False
                    minerProcess = Process.Start(px)

                    MinerInstances.RunningMiners.Add(minerProcess)

                End If




            End If

        Catch ex As Exception
			LogUpdate(ex.Message, eLogLevel.Err)
			AIOMiner.Button1.Text = "Start"
			AIOMiner.Button1.BackgroundImage = My.Resources.Resources.START
		End Try


		'Send an e-mail
		'If Not GetArgs Then
		'    If TestPool Then
		'        Try
		'            SendEmail("Hello!  I have started to mine:" & coin.ToString & "!  Going to Pool:" & pool & "  As a reminder my name is " & ReturnHostname() & ".  My ip address is: " & GetIPv4Address(), "hey")
		'        Catch ex As Exception
		'        End Try
		'    End If
		'End If

		'ethminer.exe --farm - recheck 200 -G -S eu1.ethermine.org: 4444 -FS us1.ethermine.org:4444 -O <Your_Ethereum_Address>.<RigName>


	End Sub
	Private Shared Sub BG_Worker_DebugMonitor_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

	End Sub
	Public Shared Sub proc_OutputDataReceived(ByVal sender As Object, ByVal e As DataReceivedEventArgs)

		If PubShared.DebugMiner = e.Data Then
			'Remove duplicates being sent to the logs
		Else
			PubShared.DebugMiner = e.Data
		End If
	End Sub



	Public Shared Function UpdateCoins(coin As String) As String
		Dim retry As Boolean = True
		While retry
			Try
				Dim strURL As String = "https://min-api.cryptocompare.com/data/price?fsym=" + coin + "&tsyms=" + ReturnAIOsetting("prices")
				Dim strOutput As String = ""
				Dim wrResponse As WebResponse = HttpWebRequest.Create(strURL).GetResponse()
				Using sr As New StreamReader(wrResponse.GetResponseStream())
					strOutput = sr.ReadToEnd()
					' Close and clean up the StreamReader
					sr.Close()
				End Using

				Dim re1 As String = ".*?"   'Non-greedy match on filler
				Dim re2 As String = "([+-]?\d*\.\d+)(?![-+0-9\.])"  'Float 1


				Dim r As Regex = New Regex(re1 + re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
				Dim m As Match = r.Match(strOutput)
				If (m.Success) Then
					Dim csv1 = m.Groups(1)
					Dim USD As String
					USD = csv1.ToString
					USD = USD.Replace("""", "")
					USD = USD.Replace(":", " ")
					USD = USD.Replace(ReturnAIOsetting("prices"), "")
					USD = USD.Replace(" ", "")
					Return USD
				Else
					retry = False
					Return ""
				End If
			Catch ex As Exception
				retry = True
				System.Threading.Thread.Sleep(1000)
				' LogUpdate(ex.Message & " For:" + coin, eLogLevel.Err)
				' Return "APIerr"
			End Try
		End While

        Return ""

    End Function

	Public Shared Function GetCoinAlgoFromWeb(ByVal wtmWebAddrs As String) As List(Of ListViewItem)



		Dim webb As New HtmlWeb()
		Dim jsonToReturn As String = String.Empty
		Dim wLines As New List(Of whattomine_line)()

		Dim rtnList As New List(Of ListViewItem)()

		Dim ids As Integer = 1

		Try
			Dim myWebResponse As WebResponse = Nothing

			Try
				' Create a new WebRequest Object to the mentioned URL.
				Dim myWebRequest As WebRequest = WebRequest.Create(wtmWebAddrs)

				' Set the 'Timeout' property in Milliseconds.
				myWebRequest.Timeout = 3000

				' Assign the response object of 'WebRequest' to a 'WebResponse' variable.
				myWebResponse = myWebRequest.GetResponse()
				myWebResponse.Close()


			Catch ex1 As Exception
				' do stuff to say site down??
				LogUpdate("WhatToMine.com seems unreachable", eLogLevel.Err)
				Return rtnList
			Finally
				If myWebResponse IsNot Nothing Then
					myWebResponse.Dispose()
				End If


			End Try



			Dim doc As HtmlDocument = webb.Load(wtmWebAddrs)   ' load up web doc of site


			Dim TheCoins As List(Of Coin) = GetAIOMinerJson().Pools.Coins.ToList
			'Threading.Thread.Sleep(5000)
			Dim rowcnt As Integer = 1
			For Each trow In doc.DocumentNode.SelectNodes("//tbody/tr")
				If trow.ChildNodes.Count >= 16 Then
					Dim c2 As String = trow.ChildNodes(1).InnerHtml

					If Not c2.ToLower.Contains("nicehash") Then
						' parse out the info from the 2nd cell of the row for coin name
						Dim sndx As Int16 = c2.IndexOf("<a")
						' if no anchor tag on this column then ignore row
						If sndx < 0 Then
							Continue For
						End If

						Dim endx As Int16 = c2.IndexOf("</a")
						Dim c2len As Int16 = endx - sndx

						Dim c2_anch = c2.Substring(sndx, c2len)
						sndx = c2_anch.IndexOf(">") + 1

						Dim cname As String = c2_anch.Substring(sndx)



						sndx = cname.IndexOf("(")
						Dim cnameshort As String = IIf(sndx > 0, cname.Substring(0, sndx), "")


						sndx = c2_anch.LastIndexOf("-") + 1
						endx = c2_anch.IndexOf(">") - 1

                        'Fails right here
                        Dim algotxt As String = ""
                        Try


							algotxt = c2_anch.Substring(sndx, endx - sndx).ToUpper
						Catch ex As Exception

						End Try


						' get text for the difficulty from the 6th cell of the row
						Dim c6 As String = trow.ChildNodes(5).InnerHtml
						sndx = c6.IndexOf("<br>") + 4
						endx = c6.IndexOf("</div>")

						Dim difficulty As String = c6.Substring(sndx, endx - sndx).Replace(vbLf, "").Trim


						' get text for profit from the 16th cell of the row
						Dim c16 As String = trow.ChildNodes(15).InnerHtml
						'sndx = c16.IndexOf("<strong>") + 8
						sndx = 0
						endx = c16.IndexOf("<br>")   ' c16.IndexOf("</strong>") - 1

                        Dim endxx As Integer
                        endxx = c16.IndexOf("</strong>")

                        Dim profit As String = c16.Substring(sndx, endx - sndx).Replace(vbLf, "").Trim
                        Dim other_profit As String = c16.Substring(endx, endxx - endx).Replace(vbLf, "").Trim
                        other_profit = other_profit.Replace("<br><strong>", "").Trim


                        'Dim profit2 As String = c16.Substring()
                        Try


							'pool setup value
							Dim pSetup As String = "No"
							Dim fndEntry = TheCoins.Find(Function(c) c.Type.ToLower() = cnameshort.ToLower() AndAlso c.Primary = "1" AndAlso c.Pool IsNot Nothing AndAlso c.Pool.Length > 0)
							If fndEntry IsNot Nothing Then pSetup = "Yes"

							' determine profitzzzzzzz
							Dim profitzz As String = IIf(pSetup = "Yes", cnameshort, "")

                            ' add a new line to whattomine collection
                            Dim wLine As whattomine_line = New whattomine_line(CStr(rowcnt), cname, algotxt, pSetup, profit, other_profit, difficulty, profitzz)
                            wLines.Add(wLine)
							rowcnt += 1

                            If rowcnt > 5 Then Exit For
                        Catch ex As Exception
							LogUpdate(ex.Message, eLogLevel.Err)
						End Try

					End If

				End If

			Next


		Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

		Try


			' gen a listviewitem collection for ui   top 5 only
			For i As Short = 0 To 9
				Try
					Dim wline As whattomine_line = wLines.Item(i)
                    rtnList.Add(New ListViewItem(New String() {CStr(i + 1), wline.CoinName, wline.Pool_Setup, wline.Profit_Estimate, wline.Profit_After_Power, wline.Profitzz}))
                Catch ex As Exception
					Exit For
				End Try

			Next

		Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

		Return rtnList


	End Function

	Private Class BackgroundWorker
	End Class
End Class
