Imports Newtonsoft.Json

Public Class Algorithm

    <JsonProperty("type")>
    Public Property Type As String = ""

    <JsonProperty("MH")>
    Public Property MH As String = ""

    <JsonProperty("PowerDraw")>
    Public Property PowerDraw As String = ""

    <JsonProperty("LastTimeRan")>
    Public Property LastTimeRan As String = ""

    <JsonProperty("Miner")>
    Public Property Miner As String = ""

    <JsonProperty("Prefered")>
    Public Property Prefered As String = ""

End Class

Public Class Profitability1

    <JsonProperty("Algorithm")>
    Public Property Algorithm As Algorithm()
End Class

Public Class Profits

    <JsonProperty("Profitability")>
    Public Property Profitability As Profitability1
End Class


