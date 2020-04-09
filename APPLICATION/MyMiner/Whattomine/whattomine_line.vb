Imports System.Text
Imports Newtonsoft.Json

Public Class whattomine_line
    Public Property Id As String
    Public Property CoinName As String
    Public Property Algorithm As String
    Public Property Pool_Setup As String
    Public Property Profit_Estimate As String
    Public Property Difficulty As String
    Public Property Profitzz As String



    Public Sub New(_Id As String, cName As String, algo As String, _poolsetup As String, _profitest As String, _diff As String, _profitzz As String)
        Id = _Id
        CoinName = cName
        Algorithm = algo
        Pool_Setup = _poolsetup
        Profit_Estimate = _profitest
        Difficulty = _diff
        Profitzz = _profitzz
    End Sub

    Public Function toJSONString() As String
        Dim joutString As String = JsonConvert.SerializeObject(Me, Formatting.None)
        Return joutString

    End Function

End Class
