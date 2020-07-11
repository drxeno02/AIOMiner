Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports System.Net
Imports System.Net.Sockets
Imports System.IO
Imports System.Text.RegularExpressions
Imports HtmlAgilityPack

'Supported API's
'ProgPowApi
'CCMInerAPI
'SGMinerAPI
'DSTMAPI
'TREXAPI
'LOLMinerAPI
'xmrigapi
'CastAPI
'GMinerAPI
'EWBFAPI
'PhoenixAPI
'ClaymoreAPI
'ClaymoreOLDAPI
'BMINER
'WILDRIG
'SRBMiner
''veominer
'MiniZ
'NBminer


Public Class MinerApi

    Public Shared Sub NBminer()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim NBminer As String
            Dim address As String = "http://127.0.0.1:1880/api/v1/status"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            NBminer = reader.ReadToEnd
            Dim NBminer_json = Newtonsoft.Json.Linq.JObject.Parse(NBminer)
            reader.Close()

            Try
                Dim jsonsettings = NBminer_json
                Dim ID As Object
                Dim miner_hashrate As Decimal
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("miner")
                Dim Z As Object = ID.item("total_hashrate")
                miner_hashrate = Z
                PubShared.speed = CInt(miner_hashrate)
                PubShared.speedtype = " g/s"
                reader.Close()
            Catch ex As Exception
                'LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try





    End Sub

    Public Shared Sub MiniZ()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim address As String = "http://127.0.0.1:42000"
            Dim webb As New HtmlWeb()
            Dim total_count As Integer = 1
            Dim found_total As Boolean = False


            Try

                Dim doc As HtmlDocument = webb.Load(address)   ' load up web doc of site
                Dim FoundTotal As Boolean = False
                For Each tablecell In doc.DocumentNode.SelectNodes("//tr/td")      ' for the anchor tags where the coin and algo come from
                    Dim celltext As String = tablecell.InnerHtml

                    If found_total = True Then
                        If total_count = 3 Then
                            FoundTotal = True
                        Else
                            total_count = total_count + 1
                        End If
                    End If
                    If celltext.ToUpper.Contains("TOTAL") Then
                        found_total = True
                    Else

                        If FoundTotal = True Then
                            total_count = 1
                            found_total = False
                            If IsNumeric(celltext) Then
                                PubShared.speed = CInt(celltext)
                                PubShared.speedtype = " Sol/s"
                                Exit For
                            End If
                        End If
                    End If
                Next


            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try



    End Sub
    Public Shared Sub ProgPowAPI()

        Dim hashtype As String
        hashtype = "Mh/s"
        Dim TotalSpeed As String = ""
        Dim TotalSpeed2 As String = ""
        Dim Connected As Boolean = False
        Dim clientSocket As New System.Net.Sockets.TcpClient()
        Dim jsontxt As String = "{""id"":0,""jsonrpc"": ""2.0"",""method"": ""miner_getstat1""}"
        Dim ClaymoreJSON As String = ""

        Try
            clientSocket.Connect("127.0.0.1", 3456)
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(jsontxt & vbCrLf)
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            Dim inStream(512) As Byte
            serverStream.Read(inStream, 0, 512)
            ClaymoreJSON = System.Text.Encoding.ASCII.GetString(inStream)
            clientSocket.Close()
            Connected = True

        Catch ex As Exception

        End Try

        Dim x As Integer = 0
        If Connected = True Then
            Dim ToteSpeed As String = ""

            Try
                Dim ClayRez As ClaymoreAPI = RClaymoreAPI(ClaymoreJSON)
                ToteSpeed = ClayRez.Result(2).ToString

                'Get the Total Speed for all cards
                Dim re1 As String = "(\d+)"
                Dim r As Regex = New Regex(re1, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(ToteSpeed)
                If (m.Success) Then
                    Dim aline0 = m.Groups(1).ToString
                    Dim maxspeed As String = aline0
                    If maxspeed < 9999 Then
                        TotalSpeed = maxspeed.Substring(0, 1)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " Mh/s"
                    ElseIf maxspeed >= 10000 And maxspeed < 99999 Then
                        TotalSpeed = maxspeed.Substring(0, 2)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " Mh/s"

                    ElseIf maxspeed >= 100000 And maxspeed < 999999 Then
                        TotalSpeed = maxspeed.Substring(0, 3)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " Mh/s"

                    ElseIf maxspeed >= 1000000 And maxspeed < 9999999 Then
                        TotalSpeed = maxspeed.Substring(0, 4)
                        PubShared.speed = CInt(TotalSpeed)

                        PubShared.speedtype = " Mh/s"

                    End If
                End If
            Catch ex As Exception

            End Try
        End If



    End Sub
    Public Shared Sub CcminerAPI()
        Dim _client As TcpClient
        Dim _data As String
        Dim _sendbuffer(128) As Byte
        Dim _readbuffer(128) As Byte
        Dim _bytecount As Integer
        Dim _stream As NetworkStream
        'Get CCMiner
        Try
            _client = New TcpClient("127.0.0.1", 4068)
            _stream = _client.GetStream
            _stream = _client.GetStream
            _sendbuffer = System.Text.Encoding.ASCII.GetBytes("summary")
            _stream.Write(_sendbuffer, 0, _sendbuffer.Length)
            _bytecount = _stream.Read(_readbuffer, 0, _readbuffer.Length)
            _data = System.Text.Encoding.ASCII.GetString(_readbuffer)
            Dim FullInfo As String
            FullInfo = _data
            Dim realspeed() As String
            Dim miner_hashrate As Integer
            realspeed = _data.Split(";")
            miner_hashrate = 0
            Try
                For Each line In realspeed
                    If line.Contains("KHS") And Not line.Contains("NET") Then
                        miner_hashrate = Convert.ToDecimal((line.Replace("KHS=", "")))
                        If PubShared.algo.ToLower = "neoscrypt" Then
                            PubShared.speed = miner_hashrate
                            PubShared.speedtype = " kh/s"
                        Else
                            If miner_hashrate < 1000 Then
                                miner_hashrate = Math.Round(miner_hashrate, 2)
                                PubShared.speed = miner_hashrate
                                PubShared.speedtype = " KH/s"
                            ElseIf miner_hashrate >= 1000 And miner_hashrate < 1000000 Then
                                miner_hashrate = Math.Round((miner_hashrate / 1000), 2)
                                PubShared.speed = miner_hashrate
                                PubShared.speedtype = " MH/s"
                            ElseIf miner_hashrate >= 1000000 And miner_hashrate < 1000000000 Then
                                miner_hashrate = Math.Round((miner_hashrate / 1000000), 2)
                                PubShared.speed = miner_hashrate
                                PubShared.speedtype = " GH/s"
                            ElseIf miner_hashrate >= 1000000000 And miner_hashrate < 1000000000000 Then
                                miner_hashrate = Math.Round((miner_hashrate / 1000000000), 2)
                                PubShared.speed = miner_hashrate
                                PubShared.speedtype = " TH/s"
                            End If
                            _client.Close()
                            Exit For
                        End If

                    End If
                Next
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try

    End Sub
    Public Shared Sub sgminerAPI()
        Dim _client As TcpClient
        Dim _data As String
        Dim _sendbuffer(128) As Byte
        Dim _readbuffer(128) As Byte
        Dim _bytecount As Integer
        Dim _stream As NetworkStream
        'Get NSGminer
        Try
            _client = New TcpClient("127.0.0.1", "4028")
            _stream = _client.GetStream
            _sendbuffer = System.Text.Encoding.ASCII.GetBytes("summary")
            _stream.Write(_sendbuffer, 0, _sendbuffer.Length)
            _bytecount = _stream.Read(_readbuffer, 0, _readbuffer.Length)
            _data = System.Text.Encoding.ASCII.GetString(_readbuffer)
            Dim FullInfo As String
            FullInfo = _data
            Dim realspeed() As String
            Dim realspeed1 As Decimal
            'Dim realspeed1 As Integer

            realspeed = _data.Split(",")
            realspeed1 = 0.00 '
            Dim thing As String

            Try
                For Each line In realspeed
                    If line.Contains("MHS") Then
                        thing = line
                        thing = thing.Replace("MHS av=", "")
                        thing = thing.Replace("0.", "")
                        If thing < 9999 Then
                            thing = thing.Substring(0, 2)
                            PubShared.speed = CInt(thing)
                            PubShared.speedtype = " Mh/s"
                        End If
                        Exit For
                    End If
                Next
            Catch ex As Exception

            End Try

            'PubShared.speed = realspeed1
        Catch ex As Exception
        End Try
    End Sub
    Public Shared Sub dstmAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim address As String = "http://127.0.0.1:42002"
            Dim webb As New HtmlWeb()
            Try

                Dim doc As HtmlDocument = webb.Load(address)   ' load up web doc of site
                Dim FoundTotal As Boolean = False
                For Each tablecell In doc.DocumentNode.SelectNodes("//tr/td")      ' for the anchor tags where the coin and algo come from
                    Dim celltext As String = tablecell.InnerHtml
                    If celltext.ToUpper.Contains("TOTAL") Then
                        FoundTotal = True
                    Else
                        If FoundTotal = True Then
                            If IsNumeric(celltext) Then
                                PubShared.speed = CInt(celltext)
                                PubShared.speedtype = " Sol/s"
                                Exit For
                            End If
                        End If
                    End If
                Next


            Catch ex As Exception

            End Try

        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub VeoAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim VeoAPI1 As String
            Dim address As String = "http://127.0.0.1:11363/hashrate"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            VeoAPI1 = reader.ReadToEnd
            Dim VeoAPI_json = Newtonsoft.Json.Linq.JObject.Parse(VeoAPI1)
            reader.Close()

            Try
                Dim jsonsettings = VeoAPI_json
                Dim ID As Object
                Dim miner_hashrate As Integer
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("Total")
                miner_hashrate = ID
                Dim maxspeed As String = miner_hashrate
                If maxspeed < 9999 Then
                    TotalSpeed = maxspeed.Substring(0, 1)
                    PubShared.speed = CInt(TotalSpeed)
                    PubShared.speedtype = " kh/s"
                ElseIf maxspeed >= 10000 And maxspeed < 99999999 Then
                    TotalSpeed = maxspeed.Substring(0, 2)
                    PubShared.speed = CInt(TotalSpeed)
                    PubShared.speedtype = " Mh/s"

                ElseIf maxspeed >= 100000000 And maxspeed < 999999999 Then
                    TotalSpeed = maxspeed.Substring(0, 3)
                    PubShared.speed = CInt(TotalSpeed)
                    PubShared.speedtype = " Mh/s"

                ElseIf maxspeed >= 1000000000 And maxspeed < 9999999999 Then
                    TotalSpeed = maxspeed.Substring(0, 4)
                    PubShared.speed = CInt(TotalSpeed)

                    PubShared.speedtype = " Mh/s"
                End If

                reader.Close()
            Catch ex As Exception
                'LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub SrbAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim MultiAPI As String
            Dim address As String = "http://127.0.0.1:21555/stats/json"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            MultiAPI = reader.ReadToEnd
            Dim bminer_json = Newtonsoft.Json.Linq.JObject.Parse(MultiAPI)
            reader.Close()

            Try
                Dim jsonsettings = bminer_json
                Dim ID As Object
                Dim miner_hashrate As Integer
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("hashrate_total_now")
                miner_hashrate = ID
                PubShared.speed = miner_hashrate
                PubShared.speedtype = " h/s"
                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub MultiAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim MultiAPI As String
            Dim address As String = "http://127.0.0.1:3334"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            MultiAPI = reader.ReadToEnd
            Dim multiapi_json = Newtonsoft.Json.Linq.JObject.Parse(MultiAPI)
            reader.Close()

            Try
                Dim jsonsettings = multiapi_json
                Dim ID As Object
                Dim miner_hashrate As Integer
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("hashrate")
                Dim Z As Object = ID.item("total").item(0)
                miner_hashrate = Z
                If miner_hashrate < 1000 Then
                    miner_hashrate = Math.Round(miner_hashrate, 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " KH/s"
                ElseIf miner_hashrate >= 1000 And miner_hashrate < 1000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                ElseIf miner_hashrate >= 1000000 And miner_hashrate < 1000000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                ElseIf miner_hashrate >= 1000000000 And miner_hashrate < 1000000000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000000000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                End If


                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub BminerAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim BMiner As String
            Dim address As String = "http://127.0.0.1:1880/api/status"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            BMiner = reader.ReadToEnd
            Dim bminer_json = Newtonsoft.Json.Linq.JObject.Parse(BMiner)
            reader.Close()

            Try
                Dim jsonsettings = bminer_json
                Dim ID As Object
                Dim miner_hashrate As Integer
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("miners")
                '"6": {
                '    "solver": {
                '        "solution_rate": 8.27,
                For Each X In ID
                    For Each Y In X
                        Dim Z As Object = Y.item("solver")
                        Speed = Speed + Z.item("solution_rate")
                    Next

                Next
                PubShared.speed = Speed
                If PubShared.algo.Contains("150") Then
                    PubShared.speedtype = " Sol/s"
                Else
                    PubShared.speedtype = "MH/s"
                End If

                reader.Close()
            Catch ex As Exception
                '  LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub TrexAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim LOLResults As String
            Dim address As String = "http://127.0.0.1:4068/summary"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            LOLResults = reader.ReadToEnd
            Dim lol_json = Newtonsoft.Json.Linq.JObject.Parse(LOLResults)
            reader.Close()

            Try
                Dim jsonsettings = lol_json
                Dim miner_hashrate As Integer
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                miner_hashrate = Convert.ToInt32(jsonsettings.Item("hashrate"))
                If miner_hashrate < 1000 Then
                    miner_hashrate = Math.Round(miner_hashrate, 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " KH/s"
                ElseIf miner_hashrate >= 1000 And miner_hashrate < 1000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                ElseIf miner_hashrate >= 1000000 And miner_hashrate < 1000000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                ElseIf miner_hashrate >= 1000000000 And miner_hashrate < 1000000000000 Then
                    miner_hashrate = Math.Round((miner_hashrate / 1000000000), 2)
                    PubShared.speed = miner_hashrate
                    PubShared.speedtype = " MH/s"
                End If

                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub LOLMinerAPI()
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim LOLResults As String
            Dim address As String = "http://127.0.0.1:3456/summary"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            LOLResults = reader.ReadToEnd
            Dim lol_json = Newtonsoft.Json.Linq.JObject.Parse(LOLResults)
            reader.Close()

            Try
                Dim jsonsettings = lol_json
                Dim ID As Object
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("Session")
                For Each EHash In ID
                    If EHash.ToString.ToLower.Contains("performance_summary") Then
                        Dim strofit As String = Convert.ToString(EHash)
                        strofit = strofit.Replace("Performance_Summary", "")
                        strofit = strofit.Replace("""", "")
                        strofit = strofit.Replace(":", "")
                        strofit = strofit.Replace(",", "")
                        strofit = strofit.Replace(" ", "")
                        Speed = Convert.ToDecimal(strofit)
                        TotalSpeed = TotalSpeed + Speed
                    End If

                Next
                PubShared.speed = TotalSpeed
                PubShared.speedtype = " Sol/s"
                reader.Close()
            Catch ex As Exception
                'LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub


    Public Shared Sub xmrigAPI()
        'Get the EWBF speed while you are here
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim xmrigResults As String
            Dim address As String = "http://127.0.0.1:33999"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            xmrigResults = reader.ReadToEnd
            Dim xmrig_json = Newtonsoft.Json.Linq.JObject.Parse(xmrigResults)
            reader.Close()

            Try
                Dim jsonsettings = xmrig_json
                Dim ID As Object
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("hashrate")
                Dim Test As String
                Test = ID.ToString
                Dim re1 As String = ".*?"   'Non-greedy match on filler
                Dim re2 As String = "([+-]?\d*\.\d+)(?![-+0-9\.])"  'Float 1

                Dim r As Regex = New Regex(re1 + re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(Test)
                If (m.Success) Then
                    Dim float1 = m.Groups(1).ToString
                    TotalSpeed = TotalSpeed + float1
                End If
                PubShared.speed = TotalSpeed
                PubShared.speedtype = " H/s"
                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub CastAPI()
        'Get the EWBF speed while you are here
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim EWBFResults As String
            Dim address As String = "http://127.0.0.1:3444"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            EWBFResults = reader.ReadToEnd
            Dim ewbf_json = Newtonsoft.Json.Linq.JObject.Parse(EWBFResults)
            reader.Close()

            Try
                Dim jsonsettings = ewbf_json
                Dim num As String = ""
                Dim num2 As String = ""
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                TotalSpeed = Convert.ToInt32(jsonsettings.Item("total_hash_rate"))
                'Decimal places, fuck em'
                Dim TotalSpeedString As String = CStr(TotalSpeed)
                Dim SumNum As Integer = TotalSpeedString.Length
                Dim SumOtherNum As Integer = SumNum - 3
                TotalSpeed = CInt(TotalSpeedString.Substring(0, SumOtherNum))
                PubShared.speedtype = " H/s"
                PubShared.speed = TotalSpeed
            Catch ex As Exception

            End Try
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Sub GminerAPI()
        'Get the EWBF speed while you are here
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim EWBFResults As String
            Dim address As String = "http://127.0.0.1:42000/stat"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            EWBFResults = reader.ReadToEnd
            Dim ewbf_json = Newtonsoft.Json.Linq.JObject.Parse(EWBFResults)
            reader.Close()

            Try
                Dim jsonsettings = ewbf_json
                Dim ID As Object
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("devices")
                For Each EHash In ID
                    Speed = Convert.ToInt32(EHash.item("speed"))
                    TotalSpeed = TotalSpeed + Speed
                Next
                PubShared.speed = TotalSpeed
                If PubShared.algo.ToLower = "cuckaroo29" Then
                    PubShared.speedtype = " G/s"
                Else
                    PubShared.speedtype = " Sol/s"
                End If

                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub ewbfAPI()
        'Get the EWBF speed while you are here
        Try
            SetAllowUnsafeHeaderParsing20()
            Dim EWBFResults As String
            Dim address As String = "http://127.0.0.1:42000/getstat"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            EWBFResults = reader.ReadToEnd
            Dim ewbf_json = Newtonsoft.Json.Linq.JObject.Parse(EWBFResults)
            reader.Close()

            Try
                Dim jsonsettings = ewbf_json
                Dim ID As Object
                Dim Speed As Integer
                Speed = 0
                Dim TotalSpeed As Integer
                TotalSpeed = 0
                ID = jsonsettings.Item("result")
                For Each EHash In ID
                    Speed = Convert.ToInt32(EHash.item("speed_sps"))
                    TotalSpeed = TotalSpeed + Speed
                Next
                PubShared.speed = TotalSpeed
                PubShared.speedtype = " Sol/s"
                reader.Close()
            Catch ex As Exception
                ' LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        Catch ex As Exception

        End Try
    End Sub
    Public Shared Sub phoenixAPI()
        Dim hashtype As String
        hashtype = "Mh/s"
        Dim TotalSpeed As String = ""
        Dim TotalSpeed2 As String = ""
        Dim Connected As Boolean = False
        Dim clientSocket As New System.Net.Sockets.TcpClient()
        Dim jsontxt As String = "{""id"":0,""jsonrpc"": ""2.0"",""method"": ""miner_getstat1""}"
        Dim ClaymoreJSON As String = ""

        Try
            clientSocket.Connect("127.0.0.1", 3333)
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(jsontxt & vbCrLf)
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            Dim inStream(256) As Byte
            serverStream.Read(inStream, 0, 256)
            Dim returndata As String = System.Text.Encoding.ASCII.GetString(inStream)
            clientSocket.Close()
            ClaymoreJSON = returndata
            Connected = True
        Catch ex As Exception

        End Try

        Dim x As Integer = 0
        If Connected = True Then
            Dim ToteSpeed As String = ""

            Try
                Dim ClayRez As ClaymoreAPI = RClaymoreAPI(ClaymoreJSON)
                ToteSpeed = ClayRez.Result(2).ToString

                'Get the Total Speed for all cards
                Dim re1 As String = "(\d+)"
                Dim r As Regex = New Regex(re1, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m As Match = r.Match(ToteSpeed)
                If (m.Success) Then
                    Dim aline0 = m.Groups(1).ToString
                    Dim maxspeed As String = aline0
                    If maxspeed < 9999 Then
                        TotalSpeed = maxspeed.Substring(0, 1)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " kh/s"
                    ElseIf maxspeed >= 10000 And maxspeed < 99999 Then
                        TotalSpeed = maxspeed.Substring(0, 2)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " Mh/s"

                    ElseIf maxspeed >= 100000 And maxspeed < 999999 Then
                        TotalSpeed = maxspeed.Substring(0, 3)
                        PubShared.speed = CInt(TotalSpeed)
                        PubShared.speedtype = " Mh/s"

                    ElseIf maxspeed >= 1000000 And maxspeed < 9999999 Then
                        TotalSpeed = maxspeed.Substring(0, 4)
                        PubShared.speed = CInt(TotalSpeed)

                        PubShared.speedtype = " Mh/s"

                    End If
                End If


                Dim ToteSpeed2 As String = ""
                ToteSpeed2 = ClayRez.Result(4).ToString
                'Get the Total Speed for Dual Mining
                Dim re2 As String = "(\d+)"
                Dim r2 As Regex = New Regex(re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                Dim m2 As Match = r2.Match(ToteSpeed2)
                If (m2.Success) Then
                    If m2.Groups(1).ToString = "0" Then
                    Else
                        Dim aline02 = m2.Groups(1).ToString
                        Dim maxspeed2 As String = aline02
                        If maxspeed2 < 9999 Then
                            TotalSpeed = maxspeed2.Substring(0, 1)
                            PubShared.Dualspeed = CInt(TotalSpeed)
                            PubShared.Dualspeedtype = " kh/s"
                        ElseIf maxspeed2 >= 10000 And maxspeed2 < 99999 Then
                            TotalSpeed = maxspeed2.Substring(0, 2)
                            PubShared.Dualspeed = CInt(TotalSpeed)
                            PubShared.Dualspeedtype = " Mh/s"
                        ElseIf maxspeed2 >= 100000 And maxspeed2 < 999999 Then
                            TotalSpeed = maxspeed2.Substring(0, 3)
                            PubShared.Dualspeed = CInt(TotalSpeed)
                            PubShared.Dualspeedtype = " Mh/s"

                        ElseIf maxspeed2 >= 1000000 And maxspeed2 < 9999999 Then
                            TotalSpeed = maxspeed2.Substring(0, 4)
                            PubShared.Dualspeed = CInt(TotalSpeed)
                            PubShared.Dualspeedtype = " Mh/s"

                        End If
                    End If
                End If
            Catch ex As Exception

            End Try

        End If

    End Sub

    Public Shared Sub claymoreAPIZ()
        Dim hashtype As String
        hashtype = "Mh/s"
        Dim TotalSpeed As String = ""
        Dim TotalSpeed2 As String = ""
        Dim Connected As Boolean = False
        Dim clientSocket As New System.Net.Sockets.TcpClient()
        Dim jsontxt As String = "{""id"":0,""jsonrpc"": ""2.0"",""method"": ""miner_getstat1""}"
        Dim ClaymoreJSON As String = ""

        Try
            clientSocket.Connect("127.0.0.1", 3333)
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(jsontxt & vbCrLf)
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            Dim inStream(512) As Byte
            serverStream.Read(inStream, 0, 512)
            ClaymoreJSON = System.Text.Encoding.ASCII.GetString(inStream)
            'LogUpdate(ClaymoreJSON)
            clientSocket.Close()
            Connected = True
        Catch ex As Exception

        End Try

        Dim x As Integer = 0
        If Connected = True Then
            Dim ToteSpeed As String = ""

            Try
                Dim ClayRez As ClaymoreAPI = RClaymoreAPI(ClaymoreJSON)
                Try



                    ToteSpeed = ClayRez.Result(2).ToString

                    'Get the Total Speed for all cards
                    Dim re1 As String = "(\d+)"
                    Dim r As Regex = New Regex(re1, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim m As Match = r.Match(ToteSpeed)
                    If (m.Success) Then
                        Dim aline0 = m.Groups(1).ToString
                        Dim maxspeed As String = aline0
                        If maxspeed >= 0 And maxspeed < 999 Then
                            TotalSpeed = maxspeed.Substring(0, 3)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " h/s"
                            End If
                        ElseIf maxspeed >= 1000 And maxspeed < 9999 Then
                            TotalSpeed = maxspeed.Substring(0, 1)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " kh/s"
                            End If
                        ElseIf maxspeed >= 10000 And maxspeed < 99999 Then
                            TotalSpeed = maxspeed.Substring(0, 2)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " Mh/s"
                            End If
                        ElseIf maxspeed >= 100000 And maxspeed < 999999 Then
                            TotalSpeed = maxspeed.Substring(0, 3)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " Mh/s"
                            End If
                        ElseIf maxspeed >= 10000000 And maxspeed < 99999999 Then
                            TotalSpeed = maxspeed.Substring(0, 3)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " MH/s"
                            End If
                        End If
                    End If
                Catch ex As Exception
                    'LogUpdate(ex.StackTrace)
                End Try
                Dim ToteSpeed2 As String = ""

                Try
                    ToteSpeed2 = ClayRez.Result(4).ToString
                    'Get the Total Speed for Dual Mining
                    Dim re2 As String = "(\d+)"
                    Dim r2 As Regex = New Regex(re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim m2 As Match = r2.Match(ToteSpeed2)
                    If (m2.Success) Then
                        If m2.Groups(1).ToString = "0" Then
                        Else
                            Dim aline02 = m2.Groups(1).ToString
                            Dim maxspeed2 As String = aline02
                            If maxspeed2 < 9999 Then
                                TotalSpeed = maxspeed2.Substring(0, 1)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " kh/s"
                                End If
                            ElseIf maxspeed2 >= 10000 And maxspeed2 < 99999 Then
                                TotalSpeed = maxspeed2.Substring(0, 2)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mh/s"
                                End If
                            ElseIf maxspeed2 >= 100000 And maxspeed2 < 9999999 Then
                                TotalSpeed = maxspeed2.Substring(0, 4)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.Dualspeedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mh/s"
                                End If
                            ElseIf maxspeed2 >= 10000000 And maxspeed2 < 99999999 Then
                                TotalSpeed = maxspeed2.Substring(0, 5)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mhs/s"
                                End If
                            End If
                        End If
                    End If


                Catch ex As Exception
                    'LogUpdate(ex.StackTrace)
                End Try
            Catch ex As Exception

            End Try

        End If
    End Sub

    Public Shared Sub claymoreAPIold()
        Dim hashtype As String
        hashtype = "Mh/s"
        Dim TotalSpeed As String = ""
        Dim TotalSpeed2 As String = ""
        Dim Connected As Boolean = False
        Dim clientSocket As New System.Net.Sockets.TcpClient()
        Dim jsontxt As String = "{""id"":0,""jsonrpc"": ""2.0"",""method"": ""miner_getstat1""}"
        Dim ClaymoreJSON As String = ""

        Try
            clientSocket.Connect("127.0.0.1", 3333)
            Dim serverStream As NetworkStream = clientSocket.GetStream()
            Dim outStream As Byte() = System.Text.Encoding.ASCII.GetBytes(jsontxt & vbCrLf)
            serverStream.Write(outStream, 0, outStream.Length)
            serverStream.Flush()
            Dim inStream(512) As Byte
            serverStream.Read(inStream, 0, 512)
            ClaymoreJSON = System.Text.Encoding.ASCII.GetString(inStream)
            'LogUpdate(ClaymoreJSON)
            clientSocket.Close()
            Connected = True
        Catch ex As Exception

        End Try

        Dim x As Integer = 0
        If Connected = True Then
            Dim ToteSpeed As String = ""

            Try
                Dim ClayRez As ClaymoreAPI = RClaymoreAPI(ClaymoreJSON)
                Try



                    ToteSpeed = ClayRez.Result(2).ToString

                    'Get the Total Speed for all cards
                    Dim re1 As String = "(\d+)"
                    Dim r As Regex = New Regex(re1, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim m As Match = r.Match(ToteSpeed)
                    If (m.Success) Then
                        Dim aline0 = m.Groups(1).ToString
                        Dim maxspeed As String = aline0
                        'Dev Test
                        'maxspeed = "43"
                        If maxspeed >= 0 And maxspeed < 99 Then
                            TotalSpeed = maxspeed.Substring(0, 2)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " h/s"
                            End If
                        ElseIf maxspeed >= 100 And maxspeed < 999 Then
                            TotalSpeed = maxspeed.Substring(0, 3)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " kh/s"
                            End If
                        ElseIf maxspeed >= 1000 And maxspeed < 9999 Then
                            TotalSpeed = maxspeed.Substring(0, 4)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " kh/s"
                            End If
                        ElseIf maxspeed >= 10000 And maxspeed < 99999 Then
                            TotalSpeed = maxspeed.Substring(0, 5)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " Mh/s"
                            End If
                        ElseIf maxspeed >= 100000 And maxspeed < 999999 Then
                            TotalSpeed = maxspeed.Substring(0, 6)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " Mh/s"
                            End If
                        ElseIf maxspeed >= 10000000 And maxspeed < 99999999 Then
                            TotalSpeed = maxspeed.Substring(0, 7)
                            PubShared.speed = CInt(TotalSpeed)
                            If PubShared.algo = "equihash" Then
                                PubShared.speedtype = " Sol/s"
                            Else
                                PubShared.speedtype = " Mh/s"
                            End If
                        End If
                    End If
                Catch ex As Exception
                    'LogUpdate(ex.StackTrace)
                End Try
                Dim ToteSpeed2 As String = ""

                Try
                    ToteSpeed2 = ClayRez.Result(4).ToString
                    'Get the Total Speed for Dual Mining
                    Dim re2 As String = "(\d+)"
                    Dim r2 As Regex = New Regex(re2, RegexOptions.IgnoreCase Or RegexOptions.Singleline)
                    Dim m2 As Match = r2.Match(ToteSpeed2)
                    If (m2.Success) Then
                        If m2.Groups(1).ToString = "0" Then
                        Else
                            Dim aline02 = m2.Groups(1).ToString
                            Dim maxspeed2 As String = aline02
                            If maxspeed2 < 9999 Then
                                TotalSpeed = maxspeed2.Substring(0, 1)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " kh/s"
                                End If
                            ElseIf maxspeed2 >= 10000 And maxspeed2 < 99999 Then
                                TotalSpeed = maxspeed2.Substring(0, 2)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mh/s"
                                End If
                            ElseIf maxspeed2 >= 100000 And maxspeed2 < 9999999 Then
                                TotalSpeed = maxspeed2.Substring(0, 4)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.Dualspeedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mh/s"
                                End If
                            ElseIf maxspeed2 >= 10000000 And maxspeed2 < 99999999 Then
                                TotalSpeed = maxspeed2.Substring(0, 5)
                                PubShared.Dualspeed = CInt(TotalSpeed)
                                If PubShared.algo = "equihash" Then
                                    PubShared.speedtype = " Sol/s"
                                Else
                                    PubShared.Dualspeedtype = " Mhs/s"
                                End If
                            End If
                        End If
                    End If


                Catch ex As Exception
                    'LogUpdate(ex.StackTrace)
                End Try
            Catch ex As Exception

            End Try

        End If
    End Sub

End Class
