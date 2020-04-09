Imports System.ComponentModel
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports AIOminer.General_Utils
Imports Newtonsoft.Json

Public Class Welcome_New_3
    Private Sub Welcome_New_3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If PubShared.coin = "Ethereum Classic" Then
            Label3.Text = "Ethereum Classic"
            Label6.Text = "Ethereum Classic"
            ETHlbl.Visible = True
        Else
            Label3.Text = "Bitcoin Gold"
            Label6.Text = "Bitcoin Gold"
            ZEClbl.Visible = True
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = Nothing Then
            MsgBox("You should put in something!")
            Exit Sub
        End If

        Dim appPath As String = Application.StartupPath()
        Dim PoolsConfig As mPools = GetAIOMinerJson()
        Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList


        If Label3.Text = "Ethereum Classic" Then
            PubShared.worker = TextBox1.Text
			PubShared.ip = "stratum+tcp://pool.aiominer.com"
			PubShared.algo = "ethash"
            PubShared.pool = "Ethash"
            PubShared.password = ""
            PubShared.coin = "Ethereum Classic"
			PubShared.port = "8008"
		Else
			PubShared.ip = "us-btg.2miners.com"
			PubShared.port = "4040"
			PubShared.worker = TextBox1.Text
            PubShared.algo = "zhash_btg"
			PubShared.pool = "2Miners"
			PubShared.password = ""
            PubShared.coin = "BitcoinGold"
        End If


        'Add a Type/Pool
        Try
            Dim NewCoin As Coin = New Coin With {
                    .Algo = PubShared.algo,
                    .Type = Label3.Text,
                    .Ip = PubShared.ip,
                    .Pool = PubShared.pool,
                    .Port = PubShared.port,
                    .altargument = "",
                    .Worker = PubShared.worker,
                    .Primary = "1",
                    .password = ""
                }
            TheCoins.Add(NewCoin)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Info)
            'No idea how we messed this up but...there you have it
        End Try

        PoolsConfig.Pools.Coins = TheCoins.ToArray
        Dim strJson As String = JsonConvert.SerializeObject(PoolsConfig)
        Try
            System.IO.File.WriteAllText(appPath & "\Settings\AIOMiner.json", strJson)
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Info)
            'No idea how we messed this up but...there you have it
        End Try

        Welcome_New_4.Show()
        Me.Close()

    End Sub

    Private Sub Welcome_New_3_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub
End Class