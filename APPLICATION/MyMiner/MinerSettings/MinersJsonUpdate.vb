Imports System.Net
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports SevenZipExtractor


Public Class MinersJsonUpdate


    Private Class minerupdate
        Property miner As Minerz
        Property action As String
    End Class



    'Private Shared DL As String = ""
    'Private Shared MinerName As String = ""
    'Private Shared FileName1 As String = ""
    'Private Shared Directory2 As String = ""
    'Private Shared StillDownloading As Boolean = False




    Public Shared Sub UpdateMinersJson()

        Dim orig_MinersJson As MinerDownloadz = GetMinerJson()

        Dim new_MinersJson As MinerDownloadz = GetNewMinerJson()

        Dim newMinersList As List(Of Minerz) = new_MinersJson.Miners.ToList()
        Dim origMinersList As List(Of Minerz) = orig_MinersJson.Miners.ToList()


        Dim diffs_MinersJson As List(Of minerupdate) = New List(Of minerupdate)()

        ' see if existing in both but diff ver info
        For Each minr As Minerz In orig_MinersJson.Miners
            Dim nMinr As Minerz = newMinersList.Find(Function(x) x.NAME = minr.NAME)
            If nMinr IsNot Nothing AndAlso nMinr.VER <> minr.VER Then   ' found same name but version different
                Dim updt As New minerupdate With {
                    .miner = nMinr,
                    .action = "UPDATE"
                }
                diffs_MinersJson.Add(updt)
            End If
        Next

        ' see if any new entries in new miners.json
        'For Each minr As Minerz In new_MinersJson.Miners
        '    Dim nMinr As Minerz = origMinersList.Find(Function(x) x.NAME = minr.NAME)
        '    If nMinr Is Nothing Then    ' a new one found so add to list
        '        Dim newminr As New minerupdate With {
        '            .miner = minr,
        '            .action = "ADD"
        '        }
        '        diffs_MinersJson.Add(newminr)
        '    End If
        'Next

        ' see if any entries in original and NOT in new   sooooo a deletion
        For Each minr As Minerz In orig_MinersJson.Miners
            Dim nMinr As Minerz = newMinersList.Find(Function(x) x.NAME = minr.NAME)
            If nMinr Is Nothing Then    ' current entry not found in new json file so this is a delete
                Dim newminr As New minerupdate With {
                    .miner = minr,
                    .action = "DELETE"
                }
                diffs_MinersJson.Add(newminr)
            End If
        Next


        If diffs_MinersJson.Count > 0 Then  ' have some changes to do...

            Try


                For Each uminer As minerupdate In diffs_MinersJson
                    Select Case uminer.action
                        Case "ADD"
                            HandleAdd(uminer)
                        Case "UPDATE"
                            HandleUpdate(uminer)
                        Case "DELETE"
                            'HandleDelete(uminer)
                            HandleUpdate(uminer)
                    End Select
                Next
            Catch ex As Exception

            End Try

            updateCurrentMinersJson(newMinersList)

        Else
            updateCurrentMinersJson(newMinersList)
            LogUpdate("You have the latest Miners.json")
        End If



    End Sub
    Private Shared Sub HandleAdd(ByVal _uminer As minerupdate)
        Try


            ' if a new miner entry then create the new directory to download to later on BM or mining
            Dim newDir As String = _uminer.miner.PATH
            newDir = newDir.Replace("~", Application.StartupPath())
            newDir = newDir.Replace("\\", "\")
            If Not System.IO.Directory.Exists(newDir) Then
                System.IO.Directory.CreateDirectory(newDir)
            End If

            LogUpdate("Created directory for new miner at " & newDir)
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub HandleUpdate(ByVal _uminer As minerupdate)
        Try


            ' if an updated miner then rename current directory to a datetime stamped name and create the directory for dload later
            Dim curDir As String = _uminer.miner.PATH
            curDir = curDir.Replace("~", Application.StartupPath())
            curDir = curDir.Replace("\\", "\")

            Dim dtstamp As String = Date.Now.ToString("_yyyyMMdd_HHmmss")
            Dim newDir As String = curDir & dtstamp
            newDir = newDir.Replace("\_", "_")

            'System.IO.Directory.Move(curDir, newDir)
            System.IO.Directory.Delete(curDir)
            ' System.IO.Directory.CreateDirectory(curDir)

            LogUpdate("Removed old directory " & curDir)
        Catch ex As Exception

        End Try
    End Sub

    Private Shared Sub HandleDelete(ByVal _uminer As minerupdate)
        ' if deleting an existing miner entry then rename the cur dir so miner will not be found ergo 'deleting' it
        Try


            Dim curDir As String = _uminer.miner.PATH
            curDir = curDir.Replace("~", Application.StartupPath())
            curDir = curDir.Replace("\\", "\")

            Dim dtstamp As String = Date.Now.ToString("_yyyyMMdd_HHmmss")
            Dim newDir As String = curDir & dtstamp
            newDir = newDir.Replace("\_", "_")

            System.IO.Directory.Move(curDir, newDir)

            LogUpdate("Renamed directory to effectively delete miner.  Directory name now is " & newDir)
        Catch ex As Exception

        End Try

    End Sub


    Private Shared Sub updateCurrentMinersJson(ByVal newMList As List(Of Minerz))
        ' backup the current miners.json
        ' then copy the new miners.json over the existing one in the \settings\ folder
        Try


            Dim vroot As String = Application.StartupPath

            ' create backup dir if needed
            If Not System.IO.Directory.Exists(vroot & "\Settings\Backups") Then
                System.IO.Directory.CreateDirectory(vroot & "\Settings\Backups")
            End If

            ' place a copy of the existing miners.json in the backup folder
            My.Computer.FileSystem.CopyFile(vroot & "\Settings\Miners.json", vroot & "\Settings\Backups\Miners.json", overwrite:=True)

            ' replace orig miners.json with new miners.json
            Dim newMs As New MinerDownloadz With {.Miners = newMList.ToArray()}

            SaveMinersJson(newMs)

        Catch ex As Exception

        End Try
    End Sub








    'Private Shared Sub HandleAdd(ByVal _uminer As minerupdate)
    '    ' miners.json local has been updated to latest   this is for doing the add a new miner to directory action 
    '    LogUpdate("Attempting add on for " & _uminer.miner.NAME)

    '    Dim tick As Integer = 0
    '    ' wait on any previous downloads
    '    If StillDownloading = True Then
    '        Do
    '            Threading.Thread.Sleep(2000)
    '            Application.DoEvents()
    '            If Not StillDownloading Then
    '                tick = 0
    '                Exit Do
    '            End If
    '            tick += 1
    '        Loop While tick < 150
    '        If tick <> 0 Then
    '            LogUpdate("Kicked out of still downloading loop after 20 seconds on " & DL)
    '            Exit Sub
    '        End If
    '    End If

    '    ' download the miner 
    '    DL = _uminer.miner.WEBLOCATION
    '    FileName1 = DL.Substring(DL.LastIndexOf("/") + 1)
    '    Directory2 = _uminer.miner.PATH
    '    Directory2 = Directory2.Replace("~", Application.StartupPath())
    '    Directory2 = Directory2.Replace("\\", "\")

    '    ' create directory2 if needed
    '    If Not System.IO.Directory.Exists(Directory2) Then
    '        System.IO.Directory.CreateDirectory(Directory2)
    '    End If

    '    Try
    '        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 Or SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12
    '        Dim client As WebClient = New WebClient


    '        'AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
    '        AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted

    '        client.DownloadFileAsync(New Uri(DL), Directory2 & FileName1)

    '        StillDownloading = True
    '        MinerName = Directory2.Substring(Directory2.LastIndexOf("/") + 1)

    '    Catch ex As Exception

    '    End Try
    'End Sub

    'Private Shared Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)


    '    If e.Error IsNot Nothing Then
    '        StillDownloading = False
    '        Log.LogUpdate("Error downloading file " & DL, Log.eLogLevel.Err)
    '        LogUpdate(e.Error.Message & IIf(e.Error.InnerException IsNot Nothing AndAlso e.Error.InnerException.Message.Length > 0, e.Error.InnerException.Message, ""))
    '        Exit Sub
    '    End If




    '    Dim FileToExtract As String = DL
    '    Try
    '        If FileToExtract.Contains(".exe") Then
    '            LogUpdate("Nothing to extract for downloaded file, just a .exe")
    '        Else
    '            Log.LogUpdate("Completed the download of file " & DL & ", extracting now")
    '            Dim NewExtract As ArchiveFile = New ArchiveFile(Directory2 & FileName1)
    '            NewExtract.Extract(Directory2, True)
    '            LogUpdate("Extracted download file " & DL)
    '        End If

    '    Catch ex As Exception

    '        StillDownloading = False
    '    End Try

    '    StillDownloading = False

    'End Sub


End Class
