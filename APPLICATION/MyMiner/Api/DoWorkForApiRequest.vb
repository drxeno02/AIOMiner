Imports AIOminer.Log

Public Class DoWorkForApiRequest
    Public Enum eApiCommands
        eUnknown = -1
        eStop = 1
		eStart = 10
		eRestartMining = 11
		eRestartRig = 12
		eShutdownRig = 13
		eAddNewPool = 14
		eChangeCoinMining = 15
		eDisable = 16

	End Enum

    Private Shared myrigname As String = ""

    Private Shared Function decodeMyJob(daJob As RigJob) As Integer
		Dim rtnVal As Integer = eApiCommands.eUnknown

		'EUNknown


		Select Case daJob.nextcommand.ToLower
			Case "stop_mining"
				rtnVal = eApiCommands.eStop
			Case "start_mining"
				rtnVal = eApiCommands.eStart
			Case "restart_mining"
				rtnVal = eApiCommands.eRestartMining
			Case "shutdown"
				rtnVal = eApiCommands.eShutdownRig
			Case "add_pool"
				rtnVal = eApiCommands.eAddNewPool
				PubShared.AIOAuxCommand = daJob.aux_command
			Case "restart"
				rtnVal = eApiCommands.eRestartRig
			Case "disable"
				rtnVal = eApiCommands.eDisable
		End Select

		'Outside Case's with multiple options
		If daJob.nextcommand.ToLower.Contains("change_mining_") Then
			Dim string2change = daJob.nextcommand.Replace("CHANGE_MINING_", "")
			PubShared.Coin2Change2 = string2change
			rtnVal = eApiCommands.eChangeCoinMining
		End If

		Return rtnVal

    End Function
    Public Shared Function DecodeApiWork(workitems As RigsWork, rigname As String) As RigJob
        myrigname = rigname


        For Each j As RigJob In workitems.Jobs
            If isMyEntry(j) Then
                ' do the bidding for my rig
                '  LogUpdate("Working on command " & j.nextcommand & " from web user ...")

                ' decode job command and return to caller
                j.decodedrigcommand = decodeMyJob(j)

                ' send update to web api saying working on my job
                PubShared.myRigJob = j
                AIOMinerWebAPI.send_rig_jobstarted(rigname)

                Return j
            End If
        Next

        Return Nothing

    End Function

    Private Shared Function isMyEntry(job As RigJob) As Boolean
        Dim rtnVal As Boolean = False

        If job.rig_id = myrigname AndAlso job.apikey = PubShared.ApiKey Then
            rtnVal = True
        End If

        Return rtnVal

    End Function

End Class
