Imports System.IO
Imports System.Net
Imports System.Text
Imports Newtonsoft.Json
Imports HtmlAgilityPack
Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils
Imports AIOminer.Log
Imports AIOminer.RigsApi


Public Class AIOMinerWebAPI
	Private Shared dummyAPIKey As String = ""
	Private Shared WorkRecId As Integer = -1

    Public Shared Function GetRigname() As String
		'Dim hostname As String = Environment.MachineName.Replace(" ", "").Replace(".", "")
		Dim hostname As String = ReturnAIOsetting("rigname")
		PubShared.Rigname = hostname

		'If PubShared.Rigname.Length > 0 Then hostname = PubShared.Rigname

		Return hostname
    End Function

    Private Shared Function GetBaseApiUrl() As String


        Return ReturnAIOsetting("baseapiurl")

        ' todo:   setup for dynamic add to settings and apikey and rigname

        'If AddNewSettings("minerversion", "0.0.0.1") = "added" Then
        '    Label1.Text = "Checking under the bed for monsters!"
        'End If



    End Function
    Private Shared Function getapikey() As String
        ' TODO:  get the 'real' api key
        If PubShared.ApiKey = "" Then
			PubShared.ApiKey = ReturnAIOsetting("apikey")
			Return PubShared.ApiKey
		Else
            Return PubShared.ApiKey
        End If
    End Function

    Public Shared Function checkPlanStatus() As Boolean
        Dim baseuri As String = GetBaseApiUrl()
        Dim responseText As String = ""
        Dim retVal As Boolean = False

        Try
            Dim request As WebRequest = WebRequest.Create(baseuri & "/api/info/" & PubShared.ApiKey)
            request.Method = "GET"
            Dim response As WebResponse = request.GetResponse()
            Dim inputstream1 As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(inputstream1)
            responseText = reader.ReadToEnd
            inputstream1.Dispose()
            reader.Close()
            response.Dispose()


            '#TODO - 10/30/2020 - Removed this for simplicity 

            'Check if correct api key was given
            'If responseText.ToLower.Contains("email_verified") = True Then
            '    'Correct API, see if it's a subscriber  
            '    If responseText.ToLower.Contains("normie") = True Then
            '        'Not a paying user, show ad
            '        retVal = False
            '    Else
            '        retVal = True
            '    End If
            'Else
            '    'No or incorrect API Key, show ad
            '    PubShared.ShouldBeUpdatingToApi = False
            '    retVal = False

            'End If

        Catch ex As Exception
            If ex IsNot Nothing AndAlso ex.Message IsNot Nothing AndAlso Not ex.Message.ToLower.Contains("unable to connect") Then
                'Log.LogUpdate("Error on checking API online status!  Exception is: " & ex.Message)
            End If
            PubShared.ShouldBeUpdatingToApi = False
            retVal = False
        End Try

        Return retVal

    End Function

    Private Shared Function genJobStatusHash(apikey As String, rigname As String) As String

        Dim hashCode As String = ""
        Try
            hashCode = String.Format("{0:X}", CStr(apikey & rigname & Now.ToLongDateString()).GetHashCode())
            PubShared.JobHashValue = hashCode

        Catch ex As Exception
            hashCode = "AbCdEf"
        End Try

        Return hashCode


    End Function

    Public Shared Function checkApiOnline() As Boolean
        Dim baseuri As String = GetBaseApiUrl()



        Dim responseText As String = ""
        Dim retVal As Boolean = False

        Try
            Dim request As WebRequest = WebRequest.Create(baseuri & "/api")
            request.Method = "GET"
            Dim response As WebResponse = request.GetResponse()
            Dim inputstream1 As Stream = response.GetResponseStream()
            Dim reader As New StreamReader(inputstream1)
            responseText = reader.ReadToEnd
            inputstream1.Dispose()
            reader.Close()
            response.Dispose()

            If responseText IsNot Nothing AndAlso responseText.ToLower.Contains("ok") Then
                retVal = True
            Else
                retVal = False
            End If

        Catch ex As Exception
            If ex IsNot Nothing AndAlso ex.Message IsNot Nothing AndAlso Not ex.Message.ToLower.Contains("unable to connect") Then
                ' Log.LogUpdate("Error on checking API online status!  Exception is: " & ex.Message)
            End If

            retVal = False
        End Try

        Return retVal

    End Function
    Private Shared Function buildRigInfoForApi(rigname As String) As String
        Return "{""RigInfo"": [ { ""apikey"": """ & getapikey() & """, ""rigname"": """ & rigname & """ }]}"
    End Function

    Private Shared Function decodedStat(rstatus As String) As rUpdateInfo
        Dim rtnVal As New rUpdateInfo()
        Dim lStatus As String = rstatus.ToLower

        If lStatus.Contains("started") Then
            rtnVal.rstatus = "IN PROGRESS"
            rtnVal.api = "api/rigs/jobstarted"
            rtnVal.errPrefix = "Job Started"
        ElseIf lStatus.Contains("done") Then
            rtnVal.rstatus = "DONE"
            rtnVal.api = "api/rigs/jobdone"
            rtnVal.errPrefix = "Job Done"
        End If

        Return rtnVal
    End Function

    Private Shared Sub send_rig_jobupdate(hostname As String, rstatus As String)
        Dim baseuri As String = GetBaseApiUrl()
        Dim webClient As New WebClient()
        Dim reqString() As Byte


        If PubShared.ShouldBeUpdatingToApi Then
            Dim myrUpdateInfo = decodedStat(rstatus)
            Try
                Try
                    If checkApiOnline() Then
                        Dim myJobsApi As New JobStatusApi()
                        Dim myJobs As List(Of JobStatus) = New List(Of JobStatus)


                        ' build out the job info 
                        Dim aJobStatus As New JobStatus()
                        With aJobStatus
                            .apikey = getapikey()
                            .rigname = hostname
                            .rigid = PubShared.myRigJob.recid
                            .hash = genJobStatusHash(.apikey, .rigname)
                            .status = myrUpdateInfo.rstatus
                        End With

                        myJobs.Add(aJobStatus)
                        myJobsApi.Jobs = myJobs.ToArray()


                        If (myJobsApi.Jobs.Length > 0) Then
                            Dim urlToPost As String = baseuri & "/" & myrUpdateInfo.api
                            Try
                                webClient.Headers.Add("Content-Type", "application/json")
                                Dim jsonstring As Object = JsonConvert.SerializeObject(myJobsApi)
                                reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(myJobsApi))
                                webClient.UploadDataAsync(New Uri(urlToPost), "POST", reqString)
                                webClient.Dispose()

                            Catch ex As Exception
                                'LogUpdate("GPU Stats - Failed to contact API for sending gpu stats!")
                                PubShared.log4.Error(myrUpdateInfo.errPrefix & " - Failed to contact API for updating job entry to in progress!  Exception: " & ex.Message)
                            End Try
                        End If
                    End If
                Catch ex As Exception
                    If ex.Message.ToLower.Contains("actively refused") Then
                        LogUpdate("Web API service endpoint for rigs updating appears down ")
                    End If
                End Try

            Catch ex As Exception
                LogUpdate(myrUpdateInfo.errPrefix + " update to web failed.  Exception: " & ex.Message)
            End Try
        End If

    End Sub


    Public Shared Sub send_rig_jobstarted(hostname As String)
        send_rig_jobupdate(hostname, "started")
    End Sub

    Public Shared Sub send_rig_jobdone(hostname As String)
        send_rig_jobupdate(hostname, "done")
    End Sub

    Public Shared Sub send_rig_stats(hostname As String)
        Dim baseuri As String = GetBaseApiUrl()
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim resString As String
        Dim reqString() As Byte
        Dim coinsarray As String() = {}


        If PubShared.ShouldBeUpdatingToApi Then

            ' get coinsarray 
            Try

                Dim lstCoins As New List(Of String)()
                For Each cboitem As Object In PubShared.ccmlistcombo.Items
                    Dim t As String = PubShared.ccmlistcombo.GetItemText(cboitem)
                    If Not lstCoins.Contains(t) Then
                        lstCoins.Add(t)
                    End If
                Next
                coinsarray = lstCoins.ToArray()

            Catch ex As Exception
                LogUpdate(ex.Message, eLogLevel.Err)
                Exit Sub
            End Try




            Try
                ' There is a race condition going on here, not sure why or how any of this ever worked but during debug the below can fail. It's being checked before it exists , quick bug fix is to set if date = date
                ' If PubShared.GpuListView.Items.Count > 0 Then
                If Today = Today Then

                    'Check that API is online
                    Try
                        Try
                            If checkApiOnline() Then
                                Dim myRigsApi As New RigsApi()
                                Dim myRigs As List(Of Rig) = New List(Of Rig)()
                                Dim myGpuStats As List(Of GpuForApi) = New List(Of GpuForApi)()

                                ' build out the gpu stats array for use in the rig entry
                                Dim gpuidtmp As Integer = 0

                                For Each li As ListViewItem In PubShared.GpuListView.Items
                                    Dim strgpuid As String = gpuidtmp.ToString
                                    Dim aGpuInfo As New GpuForApi With {
                                        .gpuid = strgpuid,
                                        .gpuname = li.SubItems(0).Text,
                                        .fanpercent = li.SubItems(1).Text,
                                        .temperature = li.SubItems(2).Text,
                                        .utilpercent = li.SubItems(3).Text
                                    }

                                    'TEST

                                    gpuidtmp += 1

                                    myGpuStats.Add(aGpuInfo)

                                Next

                                ' build out the rig info 
                                Dim aRig As New Rig()
                                With aRig
                                    .apikey = getapikey()
                                    .currentdatetime = Now()
                                    .currentlymining = IIf(PubShared.algo = "", "Idle", "Mining")
                                    'Changed to Idle for website to pick up on as Not Mining and Mining contain Minining :|
                                    .coinmining = PubShared.coin
                                    .selected_coin = PubShared.SelectedCoin
                                    .pool_ip = PubShared.ip
                                    .hashrate = PubShared.speed.ToString
                                    .hashtype = PubShared.speedtype.Trim
                                    .poolmining = PubShared.pool
                                    .algo = PubShared.algo
                                    .rigname = hostname
                                    .wallet = IIf(PubShared.algo = "", "", PubShared.worker)
                                    .ccmlist = coinsarray
                                    .commandnext = "OK"
                                    .GPUs = myGpuStats.ToArray()
                                    .gpu_count = .GPUs.Length.ToString()
                                    .aiover = PubShared.Version
                                    .uptime = PubShared.uptime & ";" & PubShared.miner_uptime




                                End With

                                myRigs.Add(aRig)
                                myRigsApi.Rigs = myRigs.ToArray()


                                If (myRigsApi.Rigs.Length > 0 AndAlso myRigsApi.Rigs(0).GPUs.Length > 0) Then
                                    Dim urlToPost As String = baseuri & "/api/rigs/update"
                                    Try
                                        webClient.Headers.Add("Content-Type", "application/json")
                                        Dim jsonstring As Object = JsonConvert.SerializeObject(myRigsApi)
                                        reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(myRigsApi))
                                        resByte = webClient.UploadData(urlToPost, "POST", reqString)
                                        resString = Encoding.Default.GetString(resByte)

                                        'LogUpdate("rigs update call results " & resString)

                                        webClient.Dispose()

                                    Catch ex As Exception
                                        'LogUpdate("GPU Stats - Failed to contact API for sending gpu stats!")
                                        PubShared.log4.Error("GPU Stats - Failed to contact API for sending gpu stats!  Exception: " & ex.Message)

                                    End Try
                                End If

                            End If
                        Catch ex As Exception

                            If ex.Message.ToLower.Contains("actively refused") Then
                                ' LogUpdate("Web API service endpoint for rigs updating appears down ")
                            End If
                        End Try
                    Catch ex As Exception

                    End Try
                End If
            Catch ex As Exception
                '  LogUpdate("GPU Stats - Failed to update API with gpu stats!  Exception: " & ex.Message)
            End Try
        End If


    End Sub

    Public Shared Function get_rig_work(hostname As String) As RigJob
        Dim baseuri As String = GetBaseApiUrl()
        Dim webClient As New WebClient()

        'Force this, eventual purge out the subscription portion
        'PubShared.ShouldBeUpdatingToApi = True

        If PubShared.ShouldBeUpdatingToApi Then
            Try
                'Bug condition during debug, #TODO
                'If PubShared.GpuListView.Items.Count > 0 Then
                If Today = Today Then

                    'Check that API is online
                    Try
                        Try
                            If checkApiOnline() Then
                                Dim grequest As WebRequest = WebRequest.Create(baseuri & "/api/rigs/getwork")
                                grequest.Method = "GET"
                                grequest.Headers.Add("RigInfo", buildRigInfoForApi(hostname))
                                Dim gresponse As WebResponse = grequest.GetResponse()
                                Dim gresponseString As String = New StreamReader(gresponse.GetResponseStream()).ReadToEnd
                                Dim jobjRigswork = Linq.JObject.Parse(gresponseString)
                                Dim myRigsWork = jobjRigswork.ToObject(Of RigsWork)

                                'Check if Rigs Works contains multiple items (Add Pool for instance has multiple items)
                                'Change minning
                                'CHANGE_MINING $COINNAME$


                                Dim arigJob As RigJob = DoWorkForApiRequest.DecodeApiWork(myRigsWork, hostname)

                                Return arigJob

                            End If
                        Catch ex As Exception
                            Dim methodName$ = System.Reflection.MethodBase.GetCurrentMethod().Name
                            LogUpdate(methodName$ & ":" & ex.Message, eLogLevel.Err)
                            If ex.Message.ToLower.Contains("actively refused") Then
                                'LogUpdate("Web API service endpoint for rigs get work appears down ")
                            End If
                        End Try
                    Catch ex As Exception
                        LogUpdate(ex.Message)
                    End Try
                End If
            Catch ex As Exception
                'LogUpdate("Get Rig Work - Failed to get work items!  Exception: " & ex.Message)
            End Try
            ' else   let user know they are banned
        End If
        Return New RigJob()

    End Function

End Class
