
Imports AIOminer.JSON_Utils
Imports AIOminer.CoinMining
Imports AIOminer.Log


Public Class PoolAdvanced

    Public Property paSettings As PoolAdvancedSettings

    Private clearArgsOnExit As Boolean = True


    Private Sub RefreshPage(Optional ByVal fromRestoreDefault As Boolean = False)
        'Load cointomine with the current coincombo from PoolSettings
        Dim algo As String = ""

        'Read the main JSON
        Dim newpool As Boolean = False
        Dim foundit As Boolean = False


        Try
            For Each eCoin As Coin In GetAIOMinerJson().Pools.Coins
                If eCoin.Type = paSettings.coin Then
                    foundit = True
                    paSettings.algo = eCoin.Algo
                    If eCoin.Pool = paSettings.pool Then
                        If eCoin.altargument <> "" Then
                            txtCurSettings.Text = eCoin.altargument
                            newpool = True
                        End If
                    End If
                End If
            Next



            If foundit = False Then
                For Each dCoin As Coin In GetAIOMinerDefaultJson().Pools.Coins
                    If dCoin.Type = paSettings.coin Then
                        foundit = True
                        paSettings.algo = dCoin.Algo
                        If dCoin.Pool = paSettings.pool Then
                            If dCoin.altargument <> "" Then
                                txtCurSettings.Text = dCoin.altargument
                                newpool = True
                            End If
                        End If
                    End If
                Next
            End If


            If newpool = False Then
                CoinToMine(paSettings.algo, paSettings.coin, paSettings.ip, paSettings.port, paSettings.worker,
                           paSettings.password, paSettings.pool, True)
                paSettings.currentargs = PubShared.currentargs        ' coin to mine info back to us in PubShared.currentargs
                txtCurSettings.Text = paSettings.currentargs

                If fromRestoreDefault Then
                    PubShared.currentargs = ""
                End If
            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try


    End Sub
    Private Sub PoolAdvanced_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshPage()
    End Sub

    Private Sub btnCopyFromCurrent_Click(sender As Object, e As EventArgs) Handles btnCopyFromCurrent.Click
        txtNewSettings.Text = txtCurSettings.Text
        clearArgsOnExit = False
    End Sub

    Private Sub btnRestoreDefault_Click(sender As Object, e As EventArgs) Handles btnRestoreDefaults.Click

        Dim PoolsConfig As mPools = GetAIOMinerJson()
        Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList
        'Find the Pool in the .json

        Dim EditFlag As Boolean = False

        Dim EditFlagCoin As Coin = TheCoins.Find(Function(x) x.Type = paSettings.coin AndAlso x.Pool = paSettings.pool)
        If EditFlagCoin Is Nothing Then
            EditFlag = False
        Else
            EditFlag = True
        End If

        If EditFlag = False Then
            txtNewSettings.Text = ""
        Else
            txtNewSettings.Text = ""
            EditFlagCoin.altargument = ""
            'Save the JSON File
            PoolsConfig.Pools.Coins = TheCoins.ToArray
            Try
                SaveAIOMinerJson(PoolsConfig)
                paSettings.currentargs = ""
                paSettings.altargument = ""

                PubShared.currentargs = ""
                PubShared.altargument = ""

                clearArgsOnExit = False

                RefreshPage(fromRestoreDefault:=True)           ' so PubShared.currentargs stays cleared after refresh

            Catch ex As Exception
                MsgBox(ex.Message)
                LogUpdate(ex.Message, eLogLevel.Err)
            End Try
        End If


    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        If clearArgsOnExit Then
            PubShared.currentargs = ""
        End If

        'MsgBox("Make sure you hit save in pool settings for any changes made here to take effect!")

        Me.Close()

    End Sub

    Private Sub btnContinue_Click(sender As Object, e As EventArgs) Handles btnContinue.Click


        If clearArgsOnExit Then PubShared.currentargs = ""

        If txtNewSettings.Text <> "" Then
            paSettings.currentargs = txtNewSettings.Text
            PubShared.currentargs = paSettings.currentargs        ' to pass back for saving later
            txtCurSettings.Text = txtNewSettings.Text
            MsgBox("Make sure you hit save in pool settings for any changes made here to take effect!")
        End If

        Me.Close()
    End Sub


End Class