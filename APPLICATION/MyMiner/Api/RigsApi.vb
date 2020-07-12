Imports Newtonsoft.Json

Public Class RigsApi

    <JsonProperty("Rigs")>
    Public Property Rigs As Rig()

End Class

Public Class Rig
    Public Property rigname As String
    Public Property currentdatetime As String
    Public Property currentlymining As String
    Public Property coinmining As String

    Public Property algo As String
    Public Property gpu_count As String

    Public Property poolmining As String
    Public Property wallet As String
    Public Property hashrate As String
    Public Property hashtype As String

    Public Property apikey As String
    Public Property commandnext As String

    Public Property selected_coin As String
    Public Property aiover As String
    Public Property uptime As String

    <JsonProperty("ccm_list")>
    Public Property ccmlist As String()

    <JsonProperty("gpu_stats")>
    Public Property GPUs As GpuForApi()
	Public Property pool_ip As String
End Class

Public Class GpuForApi
    <JsonProperty("gpu_id")>
    Public Property gpuid As String
    <JsonProperty("gpu_name")>
    Public Property gpuname As String
    <JsonProperty("gpu_fan")>
    Public Property fanpercent As String
    <JsonProperty("gpu_temp")>
    Public Property temperature As String
    <JsonProperty("gpu_util")>
    Public Property utilpercent As String
End Class

Public Class JobStatusApi
    <JsonProperty("Jobs")>
    Public Property Jobs As JobStatus()
End Class
Public Class JobStatus
    Public Property rigname As String
    Public Property apikey As String
    Public Property rigid As Integer
    Public Property hash As String
    Public status As String
End Class

Public Class rUpdateInfo
    Public Property rstatus As String
    Public Property api As String
    Public Property errPrefix As String
End Class

