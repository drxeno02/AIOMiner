Imports Newtonsoft.Json

Public Class RigsWork

    <JsonProperty("Jobs")>
    Public Property Jobs As RigJob()

End Class

Public Class RigJob
    Public Property recid As Integer
    Public Property apikey As String
    Public Property rig_id As String
    Public Property nextcommand As String
    Public Property timestamp As String
	Public Property status As String
	Public Property aux_command As String
	Public Property decodedrigcommand As DoWorkForApiRequest.eApiCommands

End Class
