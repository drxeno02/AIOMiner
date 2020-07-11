Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils
Imports AIOminer.Log
Imports AIOminer.CoinMining

Imports SevenZipExtractor

Imports System.Text.RegularExpressions
Imports System.Net

Public Class MinerInstances
    Public Shared RunningMiners As List(Of Process)
    Public Shared DL As String = ""
    Public Shared Directory1 As String = ""
    Public Shared Directory2 As String = ""
    Public Shared FileName1 As String = ""
    Public Shared Benchmark As Boolean = False
    Public Shared Downloading As Boolean = False


    Private Class pinfoargs
        Property nvidiasetting As Boolean
        Property amdsetting As Boolean
        Property algo As String
        Property pass As String
        Property port As String
        Property ip As String
        Property worker As String
        Property gputouse As String
        Property apppath As String
        Property intensity As String




    End Class


    Private Shared Function GetPStartInfo(ByVal _pinfo As MinerProcInfo,
                                          ByVal _pa As pinfoargs,
                                          Optional ByVal doGpuReplace As Boolean = False) As ProcessStartInfo
        Dim retVal As ProcessStartInfo = Nothing

        If _pinfo IsNot Nothing Then
            Dim fpath As String = IIf(_pinfo.PATH.Trim.StartsWith("~"), _pa.apppath & _pinfo.PATH.Trim.Replace("~", ""), _pinfo.PATH.Trim) & _pinfo.EXECUTABLE


            If doGpuReplace Then _pa.gputouse = _pa.gputouse.Replace(" ", "")
            'If PubShared.api = "ccminer" Then _pa.gputouse = _pa.gputouse.Replace(" ", "")

            Dim pargs As String = _pinfo.ARGS
            pargs = pargs.Replace("_ip", _pa.ip).Replace("_worker", _pa.worker).Replace("_pass", _pa.pass).Replace("_port", _pa.port).Replace("_gputouse", _pa.gputouse).Replace("_intensity", _pa.intensity)

            retVal = New ProcessStartInfo(fpath) With {
                .Arguments = IIf(PubShared.altargument = "", pargs, PubShared.altargument),
                .CreateNoWindow = False
            }
            ' retVal.UseShellExecute = True

        End If

        Return retVal

    End Function




    ' for now just encap the getting of startinfo here so for later we can use this one point to drive the user bring ur miner possibility
    Public Shared Function GetMinerProcessStartInfo(ByVal _nvidiaSetting As Boolean,
                                                    ByVal _amdSetting As Boolean,
                                                    ByVal _algo As String,
                                                    ByVal _pass As String,
                                                    ByVal _port As String,
                                                    ByVal _ip As String,
                                                    ByVal _worker As String,
                                                    ByVal _gputouse As String) As ProcessStartInfo

        Dim pStartInfo As ProcessStartInfo = Nothing

        Dim _pinfoargs As New pinfoargs() With {.nvidiasetting = _nvidiaSetting, .amdsetting = _amdSetting,
                                                 .algo = _algo, .ip = _ip, .port = _port,
                                                 .pass = _pass, .worker = _worker, .gputouse = _gputouse,
                                                 .apppath = Application.StartupPath(),
                                                 .intensity = ReturnAIOsetting("intensity")}

        ' get the miner process entry values from json file that are associated with this algo passed
        Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()

        'Find the algo
        Dim mpInfo As MinerProcess = mpInfos.MinerProcesses.ToList.Find(Function(x) x.Algo.ToLower = _algo.ToLower)
        DL = ""
        Directory1 = ""
        Directory2 = ""
        FileName1 = ""
        Select Case _algo.ToLower
            Case _algo.ToLower
                'Find what type, and the primary 
                Dim GPUTYPE As String = ""

                If PubShared.nvidia = True Then
                    GPUTYPE = "nvidia"
                    If PubShared.amd = True Then
                        'If both == true, use the AMD one
                        'GPUTYPE = "amd"
                    End If
                Else
                    GPUTYPE = "amd"
                End If


                Dim pinfo As MinerProcInfo = mpInfo.Infos.ToList.Find(Function(x) x.GPUx.ToLower = GPUTYPE AndAlso x.PREFERED = "1")
                'if you don't find a prefered one then just use whatever
                If pinfo IsNot Nothing Then

                    'Check if the folder and the .exe exist
                    Directory1 = pinfo.PATH
                    Directory1 = Directory1.Replace("~", Application.StartupPath())
                    Directory1 = Directory1.Replace("\\", "\")
                    PubShared.WorkingDirectory = Directory1
                    ' If System.IO.Directory.Exists Then
                    If Not System.IO.Directory.Exists(Directory1) Then
                        Downloading = True
                        System.IO.Directory.CreateDirectory(Directory1)
                        LogUpdate("Created new directory:" & Directory1)
                    End If

                    If Not System.IO.File.Exists(Directory1 & pinfo.EXECUTABLE) Then
                        'Kill any Monitoring
                        PubShared.monitoring = False
                        Downloading = True
                        LogUpdate("Seems you are missing a miner, we are going to open up the downloader for you")
                        Downloader.Show()
                        Return Nothing





                        Dim ThingToDownload As MinerDownloadz = GetMinerJson()
                        'Find the directory's match up
                        Dim FindTheDownload As Minerz = ThingToDownload.Miners.ToList.Find(Function(x) pinfo.PATH.Contains(x.PATH))
                        If FindTheDownload IsNot Nothing Then
                            'We found a match!
                            DL = FindTheDownload.WEBLOCATION
                            FileName1 = DL.Substring(DL.LastIndexOf("/") + 1)
                            Directory2 = FindTheDownload.PATH
                            Directory2 = Directory2.Replace("~", Application.StartupPath())
                            Directory2 = Directory2.Replace("\\", "\")
                            Try
                                'ServicePointManager.Expect100Continue = True
                                'ServicePointManager.DefaultConnectionLimit = 9999
                                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
                                ' | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                                Dim client As WebClient = New WebClient


                                'AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
                                AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
                                client.DownloadFileAsync(New Uri(DL), Directory2 & FileName1)
                            Catch ex As Exception
                                LogUpdate(ex.Message, eLogLevel.Err)
                            End Try
                        End If
                    Else
                        PubShared.process_running = pinfo.EXECUTABLE.Replace(".exe", "")
                        PubShared.api = pinfo.API
                        'Check if it is lyclMiner
                        If pinfo.EXECUTABLE.ToString.ToLower.Contains("lyclminer") Then
                            'Delete any .conf file in the directory
                            Try


                                If System.IO.File.Exists(PubShared.WorkingDirectory & "lyclMiner.conf") Then
                                    System.IO.File.Delete(PubShared.WorkingDirectory & "lyclMiner.conf")
                                End If
                            Catch ex As Exception
                                LogUpdate("lyclminer.conf purge error")
                            End Try

                            Try
                                'Generate a new config file 
                                Dim lycstartinfo As New ProcessStartInfo(PubShared.WorkingDirectory & pinfo.EXECUTABLE)
                                lycstartinfo.WindowStyle = ProcessWindowStyle.Hidden
                                lycstartinfo.WorkingDirectory = PubShared.WorkingDirectory
                                lycstartinfo.Arguments = " -g lyclMiner.conf"
                                Process.Start(lycstartinfo)

                            Catch ex As Exception
                                LogUpdate("lyclminer.conf create error")
                            End Try

                            'Wait until the file exist
                            Dim blah As Boolean = False

                            Do Until blah = True
                                If System.IO.File.Exists(PubShared.WorkingDirectory & "lyclMiner.conf") Then
                                    blah = True
                                    Exit Do
                                Else
                                    System.Threading.Thread.Sleep(200)

                                End If
                            Loop

                        End If
                            'Check if the folder and the .exe exist
                            Directory1 = pinfo.PATH
                        Directory1 = Directory1.Replace("~", Application.StartupPath())
                        Directory1 = Directory1.Replace("\\", "\")
                        PubShared.WorkingDirectory = Directory1
                        pStartInfo = GetPStartInfo(pinfo, _pinfoargs)
                    End If


                Else


                    Dim pinfoRandom As MinerProcInfo = mpInfo.Infos.ToList.Find(Function(x) x.GPUx.ToLower = GPUTYPE)
                    'Check if the folder and the .exe exist
                    Directory1 = pinfoRandom.PATH
                    Directory1 = Directory1.Replace("~", Application.StartupPath())
                    Directory1 = Directory1.Replace("\\", "\")
                    PubShared.WorkingDirectory = Directory1
                    PubShared.process_running = pinfo.EXECUTABLE.Replace(".exe", "")
                    PubShared.api = pinfo.API
                    pStartInfo = GetPStartInfo(pinfoRandom, _pinfoargs)
                End If
        End Select


        Return pStartInfo


    End Function
    Public Shared Sub client_BenchDownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        'Extract
        If e.Error IsNot Nothing Then
            LogUpdate(e.Error.Message)
            Downloading = False
            Exit Sub
        End If




        Dim FileToExtract As String = DL
        Try
            If FileToExtract.Contains(".exe") Then
            Else
                LogUpdate("Extracting the miner now...")
                Dim NewExtract As ArchiveFile = New ArchiveFile(Directory2 & FileName1)
                NewExtract.Extract(Directory2, True)
            End If
        Catch ex As Exception
            LogUpdate(e.Error.Message)
            Downloading = False
        End Try

        If Downloading = True Then
            Downloading = False
        End If
        'Start over cointomine
    End Sub



    Public Shared Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        'Extract
        If e.Error IsNot Nothing Then
            LogUpdate(e.Error.Message)
            Downloading = False
            Exit Sub
        End If

        Dim FileToExtract As String = DL
        Try
            If FileToExtract.Contains(".exe") Then
                'Put in a pause
                System.Threading.Thread.Sleep(500)
                LogUpdate("Restarting mining!")
                If TimerSettings.OkToMine(PubShared.TimedMiningSettings) = False Then
                    KillAllMiningApps()
                    LogUpdate("Timed Mining override in MinerInstances.client_DownloadCompleted.  Mining will NOT be started...")
                Else
                    CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
                    If Benchmark = True Then
                    Else
                        AIOMiner.Monitor_Start()
                    End If
                End If
            Else
                LogUpdate("Extracting the miner now...")
                Dim NewExtract As ArchiveFile = New ArchiveFile(Directory2 & FileName1)
                NewExtract.Extract(Directory2, True)

                'Put in a pause
                System.Threading.Thread.Sleep(500)
                LogUpdate("Restarting mining!")
                If Benchmark = True Then
                Else
                    If TimerSettings.OkToMine(PubShared.TimedMiningSettings) = False Then
                        KillAllMiningApps()
                        LogUpdate("Timed Mining override in MinerInstances.  Mining will NOT be started...")
                    Else
                        CoinToMine(PubShared.algo, PubShared.coin, PubShared.ip, PubShared.port, PubShared.worker, PubShared.password, PubShared.pool)
                        AIOMiner.Monitor_Start()
                    End If
                End If
            End If

        Catch ex As Exception
            LogUpdate(e.Error.Message)
            Downloading = False
        End Try
        'Start over cointomine
        Downloading = False
    End Sub

End Class
