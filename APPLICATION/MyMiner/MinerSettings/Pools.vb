Imports Newtonsoft.Json

Public Class Coin

    <JsonProperty("type")>
    Public Property Type As String = ""

    <JsonProperty("isdefault")>
    Public Property isdefault As String = "0"

    <JsonProperty("primary")>
    Public Property Primary As String = ""

    <JsonProperty("algo")>
    Public Property Algo As String = ""

    <JsonProperty("pool")>
    Public Property Pool As String = ""

    <JsonProperty("ip")>
    Public Property Ip As String = ""

    <JsonProperty("port")>
    Public Property Port As String = ""

    <JsonProperty("altargument")>
    Public Property altargument As String = ""

    <JsonProperty("worker")>
    Public Property Worker As String = ""

    <JsonProperty("password")>
    Public Property password As String = ""



End Class

Public Class Pools

    <JsonProperty("Coin")>
    Public Property Coins As Coin()
End Class

Public Class mPools
    <JsonProperty("Pools")>
    Public Property Pools As Pools
End Class
