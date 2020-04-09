Imports Newtonsoft.Json


Public Class Minerz
    <JsonProperty("NAME")>
    Public Property NAME As String

    <JsonProperty("WEBLOCATION")>
    Public Property WEBLOCATION As String

    <JsonProperty("PATH")>
    Public Property PATH As String

    <JsonProperty("VER")>
    Public Property VER As String
End Class


Public Class MinerDownloadz
    <JsonProperty("miners")>
    Public Property Miners As Minerz()
End Class

