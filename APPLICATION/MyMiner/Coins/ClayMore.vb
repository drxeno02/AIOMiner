Imports Newtonsoft.Json

Public Class ClaymoreAPI
    <JsonProperty("id")>
    Public Property Id As Integer

    <JsonProperty("error")>
    Public Property Errors As Object

    <JsonProperty("result")>
    Public Property Result As Object()
End Class

