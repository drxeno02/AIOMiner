Imports AIOminer.JSON_Utils
Imports AIOminer.Log

Imports Newtonsoft.Json

Public Class CustomCoin
    Private Sub CustomCoin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim mpInfos As MinerProcInfos = GetMinerProcessInfoJson()
        For Each blah In mpInfos.MinerProcesses()
            If ComboBox1.Items.Contains(blah.Algo) Then
            Else
                ComboBox1.Items.Add(blah.Algo)
            End If

        Next
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim appPath As String = Application.StartupPath()
        Dim PoolsConfig As mPools = GetAIOMinerJson()
        Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList


        If TextBox1.Text = "*" Then
            MsgBox("Sorry, that is a special char to me! Try another name!")
            Exit Sub

        End If

        If TextBox1.Text = "" Then
            MsgBox("Hey, Looks like you forgot to give this coin a name?")
            Exit Sub

        End If
        Dim DisHereNoMaybe As Coin = TheCoins.Find(Function(x) x.Type.ToLower = TextBox1.Text.ToLower AndAlso x.Algo = ComboBox1.Text)
        If DisHereNoMaybe Is Nothing Then
        Else
            MsgBox("We already have this coin, and this coin with this algo.  Sorry bro")
            Exit Sub
        End If

        'You made it!
        Dim NewCoin As Coin = New Coin With {
            .Algo = ComboBox1.Text,
            .Type = TextBox1.Text,
            .Ip = Nothing,
            .Pool = Nothing,
            .Port = Nothing,
            .altargument = Nothing,
            .Worker = Nothing,
            .Primary = Nothing,
            .password = Nothing
        }
        TheCoins.Add(NewCoin)

        'Save the JSON File
        PoolsConfig.Pools.Coins = TheCoins.ToArray
        Dim strJson As String = JsonConvert.SerializeObject(PoolsConfig)
        Try
            System.IO.File.WriteAllText(appPath & "\Settings\AIOMiner.json", strJson)
            'PoolSettings.CoinRefresh()
            PubShared.newcoin = True
            MsgBox("Added your new coin!")
            Try
                PubShared.newcoinname = TextBox1.Text
            Catch ex As Exception

            End Try
            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message,eLogLevel.Err)
        End Try

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TextBox1.Text = Nothing
        ComboBox1.Text = Nothing
        Me.Close()



    End Sub
End Class