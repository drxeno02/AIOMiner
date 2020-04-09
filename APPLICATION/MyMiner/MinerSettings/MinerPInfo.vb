Imports Newtonsoft.Json

Public Class MinerProcInfo

    <JsonProperty("GPUx")>
    Public Property GPUx As String

    <JsonProperty("MINER")>
    Public Property MINER As String

    <JsonProperty("PATH")>
    Public Property PATH As String

    <JsonProperty("EXECUTABLE")>
    Public Property EXECUTABLE As String

    <JsonProperty("PREFERED")>
    Public Property PREFERED As String

    <JsonProperty("API")>
    Public Property API As String

    <JsonProperty("ARGS")>
    Public Property ARGS As String
End Class

Public Class MinerProcess
    <JsonProperty("Algo")>
    Public Property Algo As String

    <JsonProperty("Info")>
    Public Property Infos As MinerProcInfo()
End Class

Public Class MinerProcInfos

    <JsonProperty("Process")>
    Public Property MinerProcesses As MinerProcess()
End Class
