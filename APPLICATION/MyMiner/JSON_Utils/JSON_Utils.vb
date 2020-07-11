Imports System.IO
Imports Newtonsoft.Json
Imports AIOminer.Log
Imports System.Net
Imports System.Runtime.Caching



Public Class JSON_Utils


    Private Shared cSettings As ObjectCache = MemoryCache.Default

    Public Shared Sub updateSettingsCache(ByVal key As String, ByVal data As String)
        cSettings.Item(key) = data
    End Sub
    Public Shared Function readJstring(ByVal JSON As String) As Linq.JObject
        Try
            Dim Json_string As String = File.ReadAllText(JSON)
            Dim read_json = Linq.JObject.Parse(Json_string)
            Return read_json
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        Return New Linq.JObject()

    End Function
    Private Shared Function cFileRead(ByVal fpath As String, Optional ByVal cKey As String = "aiosettings") As String

        Dim fdata As String = cSettings.Item(cKey)

        If fdata Is Nothing Then
            fdata = File.ReadAllText(fpath)
            updateSettingsCache(cKey, fdata)
        End If

        Return fdata


    End Function
    Private Shared Function readJFile(ByVal Optional FilePathName As String = "") As Linq.JObject
        Dim doingAioSettingsFile As Boolean = False
        Try
            doingAioSettingsFile = IIf(FilePathName = "", False, FilePathName.ToLower().Contains("aiosettings.json"))

            Dim appPath As String = Application.StartupPath()
            If FilePathName = "" Then
                FilePathName = appPath & "\Settings\AIOMiner.json"
            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        Dim Json_Pool As String
        If doingAioSettingsFile Then
            Json_Pool = cFileRead(FilePathName)
        Else
            Json_Pool = File.ReadAllText(FilePathName)
        End If

        Dim read_json = Linq.JObject.Parse(Json_Pool)
        Return read_json
    End Function

    Public Shared Function GetMinerProcessUpdate() As MinerProcInfos

        Dim appPath As String = Application.StartupPath()
        Try
            Dim Minerpinfos As MinerProcInfos = JsonConvert.DeserializeObject(Of MinerProcInfos)(readJFile(appPath & "\Settings\MP_Update.json").ToString)
            Return Minerpinfos
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try
    End Function


    Public Shared Function GetMinerProcessInfoJson() As MinerProcInfos

        Dim appPath As String = Application.StartupPath()
        Try
            Dim Minerpinfos As MinerProcInfos = JsonConvert.DeserializeObject(Of MinerProcInfos)(readJFile(appPath & "\Settings\MinerProcessInfo.json").ToString)
            Return Minerpinfos
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try
    End Function

    Public Shared Function GetTimerSettingsJson() As TimerSettings.cTimerSettings
        Dim appPath As String = Application.StartupPath()
        Try
            Dim tsettings As TimerSettings.cTimerSettings = JsonConvert.DeserializeObject(Of TimerSettings.cTimerSettings)(readJFile(appPath & "\Settings\TimerSettings.json").ToString)
            Return tsettings
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try
    End Function


    Public Shared Function GetAIOSettings() As mAIOS
        Dim appPath As String = Application.StartupPath()
        Try
            Dim AIOSet As mAIOS = JsonConvert.DeserializeObject(Of mAIOS)(readJFile(appPath & "\Settings\AIOSettings.json").ToString)
            Return AIOSet
        Catch ex As Exception
            'LogUpdate(ex.Message, eLogLevel.Err)
            'Throw
            'Error has occured check for a backup of the file

            If System.IO.File.Exists(appPath & "\Settings\Backups\AIOSettings.json") Then
                'Purge old file
                System.IO.File.Delete(appPath & "\Settings\AIOSettings.json")
                System.IO.File.Move(appPath & "\Settings\Backups\AIOSettings.json", appPath & "\Settings\AIOSettings.json")
                Dim AIOSet As mAIOS = JsonConvert.DeserializeObject(Of mAIOS)(readJFile(appPath & "\Settings\AIOSettings.json").ToString)
                Return AIOSet
            End If
        End Try
    End Function

    Public Shared Function GetMinerJson() As MinerDownloadz

        Dim appPath As String = Application.StartupPath()
        Try
            Dim XYZ As MinerDownloadz = JsonConvert.DeserializeObject(Of MinerDownloadz)(readJFile(appPath & "\Settings\Miners.json").ToString)
            Return XYZ
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try



    End Function
    Public Shared Function AddNewSettings(Nitem As String, Nvalue As String, Optional ByVal update As Boolean = False)
        Dim thingtochange As MAINAPP
        Dim appPath As String = Application.StartupPath()
        'Find value
        Try
            Dim SettingsConfig As mAIOS = GetAIOSettings()
            Dim TheConfig As List(Of MAINAPP) = SettingsConfig.AIOSs.MAINAPPs.ToList
            'See if the setting already exists
            thingtochange = TheConfig.Find(Function(x) x.type = Nitem)
            If thingtochange Is Nothing Then
                Dim NewSinfo As MAINAPP = New MAINAPP With {
                    .type = Nitem,
                    .value = Nvalue
                }
                TheConfig.Add(NewSinfo)
            Else
                'Added for a quick update, this kinda overlaps with saveaiosetting, see what works best.  7/15/2018 br-jr
                If update = False Then
                    Return ("existing")
                Else
                    Dim NewSinfo As MAINAPP = New MAINAPP With {
                    .type = Nitem,
                    .value = Nvalue
                    }
                    TheConfig.Add(NewSinfo)
                End If

            End If
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
                'Backup AIOSettings.json
                Try
                    System.IO.File.Copy(appPath & "\Settings\AIOSettings.json", appPath & "\Settings\Backups\AIOSettings.json", True)
                Catch ex As Exception

                End Try
                Return ("added")
            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                Throw
            End Try
        Catch ex As Exception
            Return ("error")
            LogUpdate("Error adding new Setting", eLogLevel.Err)
        End Try


    End Function
    Public Shared Function GetNewMinerJson() As MinerDownloadz
        Dim retVal As MinerDownloadz = Nothing

        Try
            retVal = GetNewMinerJsonFromNet()
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            LogUpdate(ex.StackTrace, eLogLevel.Err)
        End Try

        Return retVal

    End Function

    Public Shared Function GetNewMinerJsonFromNet() As MinerDownloadz
        Dim retVal As MinerDownloadz = Nothing
        Dim appPath As String = Application.StartupPath()


        'Wrap it up brah
        Try
            ' get file from local
            retVal = JsonConvert.DeserializeObject(Of MinerDownloadz)(readJFile(appPath & "\Settings\Updates\Miners.json").ToString)
        Catch ex As Exception
            LogUpdate("Unable to Deserialize this Miners.json, seems the format is incorrect or something..HAAALP!")
            Throw
        End Try

        ' todo:   get file from internet loc and put in \Settings\Updates
        ' This file was already downloading to call this command, seems you can't download shit in json utils class 
        ' whaddayagonnadooooo - BRj 3/21/2018
        ' retVal = JsonConvert.DeserializeObject(Of MinerDownloadz)(readJFile(appPath & "\Settings\Updates\Miners.json").ToString)


        Return retVal


    End Function

    Public Shared Function GetAIOMinerJson() As mPools

        Dim appPath As String = Application.StartupPath()
        Try
            Dim Poolsc As mPools = JsonConvert.DeserializeObject(Of mPools)(readJFile(appPath & "\Settings\AIOMiner.json").ToString)
            Return Poolsc
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try

    End Function
    Public Shared Function RClaymoreAPI(Res As String) As ClaymoreAPI
        Try
            Dim Algos As ClaymoreAPI = JsonConvert.DeserializeObject(Of ClaymoreAPI)(Res.ToString)
            Return Algos
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            LogUpdate(ex.StackTrace)
            Throw
        End Try
    End Function

    Public Shared Function GetProfitabilityJson() As Profits

        Dim appPath As String = Application.StartupPath()
        Try
            Dim Algos As Profits = JsonConvert.DeserializeObject(Of Profits)(readJFile(appPath & "\Settings\AIOProfitability.json").ToString)
            Return Algos
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try

    End Function
    Public Shared Sub SaveProfitabilityJson(ByVal _items As Profits)
        Dim appPath As String = Application.StartupPath()

        Try
            Dim strJson As String = JsonConvert.SerializeObject(_items)
            File.WriteAllText(appPath & "\Settings\AIOProfitability.json", strJson)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try

    End Sub

    Public Shared Sub SaveTimerSettingsJson(ByVal _settings As TimerSettings.cTimerSettings)
        Dim appPath As String = Application.StartupPath()

        Try
            Dim strJson As String = JsonConvert.SerializeObject(_settings)
            File.WriteAllText(appPath & "\Settings\TimerSettings.json", strJson)

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try
    End Sub

    Public Shared Function GetAIOMinerDefaultJson() As mPools

        Dim appPath As String = Application.StartupPath()
        Try
            Dim Poolsc As mPools = JsonConvert.DeserializeObject(Of mPools)(readJFile(appPath & "\Settings\AIOMiner_Default.json").ToString)
            Return Poolsc
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try



    End Function
    Public Shared Sub SaveMinerProcJson(ByVal _procinfo As MinerProcInfos)
        Dim appPath As String = Application.StartupPath()

        Try
            Dim strJson As String = JsonConvert.SerializeObject(_procinfo)
            File.WriteAllText(appPath & "\Settings\MinerProcessInfo.json", strJson)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try

    End Sub

    Public Shared Sub SaveMinersJson(ByVal _miners As MinerDownloadz)
        Dim appPath As String = Application.StartupPath
        Try
            Dim strJson As String = JsonConvert.SerializeObject(_miners)
            File.WriteAllText(appPath & "\Settings\Miners.json", strJson)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try
    End Sub

    Public Shared Sub SaveAIOMinerJson(ByVal _poolsc As mPools)
        Dim appPath As String = Application.StartupPath()

        Try
            Dim strJson As String = JsonConvert.SerializeObject(_poolsc)
            File.WriteAllText(appPath & "\Settings\AIOMiner.json", strJson)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            Throw
        End Try

    End Sub
End Class
