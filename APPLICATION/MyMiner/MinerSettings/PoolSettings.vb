Imports Newtonsoft.Json
Imports AIOminer.JSON_Utils
Imports AIOminer.Log
Imports AIOminer.General_Utils



Public Class PoolSettings
    Public WhatsTheAlgo As String = ""
    Private PoolTested As Boolean = False



    Public Sub CoinRefresh()
        CoinCombo.Items.Clear()
        Try
            For Each rCoin In GetAIOMinerJson().Pools.Coins
                If CoinCombo.Items.Contains(rCoin.Type) Then
                Else
                    CoinCombo.Items.Add(rCoin.Type)
                End If
            Next
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        'Load Default Pools
        Dim appPath As String = Application.StartupPath()
        Try
            For Each dCoin In GetAIOMinerDefaultJson().Pools.Coins
                If CoinCombo.Items.Contains(dCoin.Type) Then
                Else
                    CoinCombo.Items.Add(dCoin.Type)
                End If
            Next
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub refreshPool()
        PoolTested = False
        PoolCombo.Items.Clear()
        PoolCombo.Items.Add("Add a new pool...")

        Try
            For Each eCoin In GetAIOMinerJson().Pools.Coins
                If eCoin.Type = CoinCombo.Text Then
                    'Load up all of the pools and populate them
                    If eCoin.Pool <> "" Then
                        If PoolCombo.Items.Contains(eCoin.Pool) Then
                        Else
                            PoolCombo.Items.Add(eCoin.Pool)
                        End If
                    End If
                    WhatsTheAlgo = (eCoin.Algo)
                End If
            Next
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        'Allow Pool name again
        'TextBox1.Enabled = True
        Dim appPath As String = Application.StartupPath()
        Try
            For Each dCoin In GetAIOMinerDefaultJson().Pools.Coins
                If dCoin.Type = CoinCombo.Text Then
                    'Load up all of the pools and populate them
                    If dCoin.Pool <> "" Then

                        If PoolCombo.Items.Contains(dCoin.Pool) Then
                        Else
                            PoolCombo.Items.Add(dCoin.Pool)
                        End If
                    End If
                    WhatsTheAlgo = dCoin.Algo
                End If
            Next

            If TextBox1.Text = "" Then
                PoolCombo.Text = "Add a new pool..."
            Else
                PoolCombo.Text = TextBox1.Text

            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub ClearText()
        PoolTested = False
        'Clear all previous items
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        CheckBox1.Checked = False
        AdvancedLabel.Text = "No"
        PubShared.currentargs = ""
        AlgoTxt.Text = "|"
        'MinerTxt.Text = "|"


        'Clear all of the pool Pool Items
        PoolCombo.Items.Clear()

        'Redisable the pools groupbox
        'GroupBox2.Enabled = False
        If PoolCombo.Items.Contains("Add a new pool...") Then
        Else
            PoolCombo.Items.Add("Add a new pool...")
        End If

        PoolCombo.Text = "Add a new pool..."

        'Clear Pool Image
        PictureBox2.Image = My.Resources.Resources.redx
    End Sub

    ' Save button action below
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Check if they are dumb
        If TextBox1.Text = "Add a new pool..." Then
            MsgBox("You can't name your pool 'Add a new pool...'  Change the name of your pool smarty pants")
            Exit Sub
        End If
        'Check to make sure name is not AIOTest
        If TextBox1.Text.ToUpper = "AIOTEST" Then
            MsgBox("Now you and I both know you can't name it that...")
            'https://www.youtube.com/watch?v=RfiQYRn7fBg'
            Exit Sub
        End If

        Dim appPath As String = Application.StartupPath()
        Dim PoolsConfig As mPools = GetAIOMinerJson()
        Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList
        Dim purge As Boolean = False
        'Dim addpool As Boolean = False
        Dim PrimaryPool As String = "0"

        'Check if Primary
        If CheckBox1.Checked = True Then
            'Remove it from any other .type
            PrimaryPool = "1"
        End If


        If TextBox1.Text.Length <> 0 AndAlso TextBox2.Text.Length <> 0 AndAlso TextBox3.Text.Length <> 0 AndAlso TextBox4.Text.Length <> 0 Then
        Else
            MsgBox("You are missing a setting!  Please review")
            LogUpdate("Missing Settings!")
            Exit Sub
        End If

        'Make sure they test before they save the pool (Future us)

        'Check if this is an addition or an edit

        Dim EditFlag As Boolean = False ' TODO Figured this out

        'Search type 
        'If the selected coin and pool together exist editFlag = True

        'Strip the * from the coin name
        Dim stripcoin As String
        stripcoin = CoinCombo.Text
        If stripCoin.Contains("*") Then
            stripCoin = stripCoin.Replace("*", "")
        End If

        Dim EditFlagCoin As Coin = TheCoins.Find(Function(x) x.Type = stripcoin AndAlso x.Pool = TextBox1.Text)
        If EditFlagCoin Is Nothing Then
            EditFlag = False
        Else
            EditFlag = True
        End If

        If PrimaryPool = "1" Then
            Dim IsPrimaryPool As Coin = TheCoins.Find(Function(x) x.Primary = "1" AndAlso x.Type = stripcoin)
            If IsPrimaryPool IsNot Nothing Then
                'Find the old pool, remove it's primary flag
                'Dim SuperOldPrimary As String = "0" 'RIP JACKASS
                IsPrimaryPool.Primary = "0"
            End If
        Else
            'If they have no primary pools, but they added this and forgot to check primary pool, check it for them
            Dim IsPrimaryPool As Coin = TheCoins.Find(Function(x) x.Primary = "1" AndAlso x.Type = stripcoin)
            If IsPrimaryPool Is Nothing Then
                PrimaryPool = "1"
            End If
        End If


        '' Search Collection of type 
        '' If textbox1.text(pools) is found
        ''  This is an Edit
        '' else
        '' This is an add

        '' If this is an Edit
        '' find the entry, modify all fields 

        '' if this is a new entry
        '' add in the new entry


        'Add a Type/Pool
        If Not EditFlag Then
            Try
                Dim NewCoin As Coin = New Coin With {
                    .Algo = WhatsTheAlgo,
                    .Type = stripcoin,
                    .Ip = TextBox2.Text.Trim,
                    .Pool = TextBox1.Text.Trim,
                    .Port = TextBox3.Text.Trim,
                    .altargument = PubShared.currentargs,
                    .Worker = TextBox4.Text.Trim,
                    .Primary = PrimaryPool,
                    .password = TextBox5.Text.Trim
                }
                TheCoins.Add(NewCoin)
            Catch ex As Exception
                LogUpdate("You are fucked, get a new JSON <3")
                MsgBox("Issue with your JSON file!  Critical Error!",, "Json Error!")
            End Try
        Else
            Dim FoundCoin As Coin = TheCoins.Find(Function(x) x.Type = stripcoin AndAlso x.Pool = TextBox1.Text.Trim)
            FoundCoin.Algo = WhatsTheAlgo
            FoundCoin.Type = stripcoin
            FoundCoin.Ip = TextBox2.Text.Trim
            FoundCoin.Pool = TextBox1.Text.Trim
            FoundCoin.Port = TextBox3.Text.Trim
            FoundCoin.altargument = PubShared.currentargs
            FoundCoin.Worker = TextBox4.Text.Trim
            FoundCoin.password = TextBox5.Text.Trim
            FoundCoin.Primary = PrimaryPool
            'Get type (Coin)
            'Get pool (Pool)
        End If

        '
        'If IntensityBox1.Visible = True Then

        'End If


        'Save the JSON File
        PoolsConfig.Pools.Coins = TheCoins.ToArray
        Dim strJson As String = JsonConvert.SerializeObject(PoolsConfig)
        Try
            System.IO.File.WriteAllText(appPath & "\Settings\AIOMiner.json", strJson)
            If EditFlag <> True Then
                MsgBox("I saved your new pool!")
                Button7.Enabled = True

                'Refresh the List
                refreshPool()
                'CoinRefresh()

            Else
                MsgBox("I edited your existing pool!")
                Button7.Enabled = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub PoolSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Load AIOMiner.json
        Try
            For Each yCoin In GetAIOMinerJson().Pools.Coins
                If yCoin.Type = "" Then
                Else
                    If CoinCombo.Items.Contains(yCoin.Type) Then

                    Else
                        CoinCombo.Items.Add(yCoin.Type)
                    End If
                End If

            Next

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            MsgBox("Unable to load AIOMiner settings!  Reinstall?  JSON ERRORS")
        End Try


        'Load Default Pools
        Dim appPath As String = Application.StartupPath()
        Try
            For Each dCoin In GetAIOMinerDefaultJson().Pools.Coins
                Dim foundit As Boolean = False
                If CoinCombo.Items.Contains(dCoin.Type & "*") Then
                    foundit = True
                End If

                If CoinCombo.Items.Contains(dCoin.Type) Then
                    foundit = True
                End If

                If foundit = False Then
                    CoinCombo.Items.Add(dCoin.Type)
                End If
            Next

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            MsgBox("Unable to load AIOMiner settings!  Reinstall?  JSON ERRORS")
        End Try


        'Check to see if they had a coin selected, if they did goto that coin now
        If AIOMiner.ComboBox1.Text IsNot "" Then
			CoinCombo.Text = AIOMiner.ComboBox1.Text
		End If

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoinCombo.SelectedIndexChanged
        ClearText()
        Try
            For Each eCoin In GetAIOMinerJson().Pools.Coins
                'Remove any * first
                Dim coinname As String
                coinname = CoinCombo.Text
                If coinname.Contains("*") Then
                    coinname = coinname.Replace("*", "")
                End If

                If eCoin.Type = coinname Then
                    AlgoTxt.Text = (eCoin.Algo)
                    'Load up all of the pools and populate them
                    If eCoin.Pool <> "" Then
                        If PoolCombo.Items.Contains(eCoin.Pool) Then
                        Else
                            PoolCombo.Items.Add(eCoin.Pool)

                        End If
                    End If
                    WhatsTheAlgo = (eCoin.Algo)
                End If
            Next
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try

        'Allow Pool name again
        'TextBox1.Enabled = True
        Dim appPath As String = Application.StartupPath()
        Try
            For Each dCoin In GetAIOMinerDefaultJson().Pools.Coins
                If dCoin.Type = CoinCombo.Text Then
                    AlgoTxt.Text = (dCoin.Algo)
                    'Load up all of the pools and populate them
                    If dCoin.Pool <> "" Then
                        If PoolCombo.Items.Contains(dCoin.Pool) Then
                            AlgoTxt.Text = (dCoin.Algo)
                        Else
                            PoolCombo.Items.Add(dCoin.Pool)
                        End If
                    End If
                    WhatsTheAlgo = dCoin.Algo
                    If WhatsTheAlgo = "Lyra2REv2" Then
                        'Label3.Visible = True
                        'IntensityBox1.Visible = True
                        'IntensityBox1.Items.Clear()
                        'IntensityBox1.Items.Add("18")
                        'IntensityBox1.Items.Add("19")
                        'IntensityBox1.Items.Add("20")
                        'IntensityBox1.Items.Add("21")
                        'IntensityBox1.Text = ReturnAIOsetting("intensity")
                    Else
                        'Label3.Visible = False
                        'IntensityBox1.Visible = False
                    End If
                    'TextBox1.Text = (dCoin.item("pool"))
                    'TextBox2.Text = (dCoin.item("ip"))
                    'TextBox3.Text = (dCoin.item("port"))
                    'TextBox4.Text = (dCoin.item("worker"))
                    'If (dCoin.item("primary")) = "Yes" Then
                    ' CheckBox1.Checked = True
                    ' End If
                End If
            Next
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try





        'emoji
        Try
            Dim lowerimage As String = CoinCombo.Text.ToLower()
            'MsgBox(lowerimage)

            Dim PictureName As String = lowerimage & "_logo_1"

            If My.Resources.Resources.ResourceManager.GetObject(PictureName) Is Nothing Then
                PictureBox1.Image = My.Resources.Resources.aiominer_logo_1
            Else
                Dim PictureContents = My.Resources.Resources.ResourceManager.GetObject(PictureName)
                PictureBox1.Image = PictureContents
            End If

        Catch ex As Exception
            '  PictureBox1.Image = My.Resources.neutral
        End Try
        If PoolCombo.Items.Count = 0 Then
            MsgBox("No pool settings found, add them here and hit save!")
        Else
            GroupBox2.Enabled = True
        End If

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'We can only have one primary pool clear out the other one

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
        'We can only have one primary pool clear out the other one
        If CheckBox1.Checked = True Then
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub PoolCombo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PoolCombo.SelectedIndexChanged
        'Clear all previous items
        For Each ctrl As Control In Me.Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).Text = String.Empty
            End If
        Next

        CheckBox1.Checked = False
        PubShared.currentargs = ""
        PubShared.altargument = ""


        Dim FoundMyPool As Boolean = False

        'Disabled Editing the pool name
        ' TextBox1.Enabled = False
        Try
            Dim stripCoin As String
            If PoolCombo.Text <> "Add a new pool..." Then
                For Each eCoin In GetAIOMinerJson().Pools.Coins
                    stripCoin = CoinCombo.Text
                    If stripCoin.Contains("*") Then
                        stripCoin = stripCoin.Replace("*", "")
                    End If

                    If eCoin.Type = stripCoin AndAlso eCoin.Pool = PoolCombo.Text Then
                        'Load the pool
                        FoundMyPool = True

                        TextBox1.Text = eCoin.Pool
                        TextBox2.Text = eCoin.Ip
                        TextBox3.Text = eCoin.Port
                        TextBox4.Text = eCoin.Worker
                        If TextBox4.Text <> Nothing Then
                            Button7.Enabled = True
                        Else
                            Button7.Enabled = False
                        End If
                        TextBox5.Text = eCoin.password
                        If eCoin.Primary = "1" Then
                            CheckBox1.Checked = True
                        End If
                        If eCoin.altargument.Trim.Length = 0 Then
                            AdvancedLabel.Text = "No"
                            Exit For

                        Else
                            AdvancedLabel.Text = "Yes"
                            Exit For


                        End If

                        'emoji
                        Try
                            PictureBox2.Image = My.Resources.Resources.aiominer_logo_1


                        Catch ex As Exception


                        End Try
                    End If
                Next

                If FoundMyPool = True Then
                Else
                    For Each eCoin In GetAIOMinerDefaultJson().Pools.Coins
                        'Check if it was already added
                        stripCoin = CoinCombo.Text
                        If stripCoin.Contains("*") Then
                            stripCoin = stripCoin.Replace("*", "")
                        End If

                        If eCoin.Type = stripCoin Then
                            If eCoin.Pool = PoolCombo.Text Then
                                'Load the pool
                                TextBox1.Text = eCoin.Pool
                                TextBox2.Text = eCoin.Ip
                                TextBox3.Text = eCoin.Port
                                TextBox4.Text = eCoin.Worker
                                TextBox5.Text = eCoin.password
                                If eCoin.Primary = "1" Then
                                    CheckBox1.Checked = True
                                End If
                                If eCoin.altargument.Trim.Length = 0 Then
                                    AdvancedLabel.Text = "No"
                                    Exit For
                                Else
                                    AdvancedLabel.Text = "Yes"
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                End If
                'Load defaults
                Dim appPath As String = Application.StartupPath()
                'emoji
                PictureBox2.Image = My.Resources.Resources.aiominer_logo_1
            Else
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                CheckBox1.Checked = False
            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
    '    Dim appPath As String = Application.StartupPath()
    '    Dim PoolsConfig As mPools = GetAIOMinerJson()
    '    Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList
    '    Dim purge As Boolean = False
    '    'Dim addpool As Boolean = False
    '    Dim PrimaryPool As String = "No"

    '    'Check if Primary
    '    If CheckBox1.Checked = True Then
    '        'Remove it from any other .type
    '        PrimaryPool = "Yes"
    '    End If

    '    'Find out the algo for the current selected Type
    '    '                      .algo = "Test",
    '    '                      .pool = TextBox1.Text,
    '    '                      .ip = TextBox2.Text,
    '    '                      .port = TextBox3.Text,
    '    '                      .worker = TextBox4.Text

    '    If TextBox1.Text.Length <> 0 AndAlso TextBox2.Text.Length <> 0 AndAlso TextBox3.Text.Length <> 0 AndAlso TextBox4.Text.Length <> 0 Then
    '    Else
    '        MsgBox("You are missing a setting!  Please review")
    '        LogUpdate("Missing Settings!")
    '        Exit Sub
    '    End If

    '    Dim EditFlag As Boolean = True

    '    If Not EditFlag Then
    '        Try
    '            Dim NewCoin As Coin = New Coin
    '            NewCoin.Algo = WhatsTheAlgo
    '            NewCoin.Ip = TextBox2.Text.Trim
    '            NewCoin.Pool = TextBox1.Text.Trim
    '            NewCoin.Port = TextBox3.Text.Trim
    '            NewCoin.Worker = TextBox4.Text.Trim
    '            NewCoin.password = TextBox5.Text.Trim
    '            TheCoins.Add(NewCoin)
    '        Catch ex As Exception
    '            LogUpdate("You are fucked, Get a New JSON <3")
    '            MsgBox("Issue With your JSON file!  Critical Error!",, "Json Error!")
    '        End Try
    '    Else

    '    End If

    '    'Save the JSON File
    '    PoolsConfig.Pools.Coins = TheCoins.ToArray
    '    Try
    '        SaveAIOMinerJson(PoolsConfig)
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        LogUpdate(ex.Message,eLogLevel.Err)
    '    End Try
    'End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click




        If TextBox1.Text = "Add a New pool..." Then
            MsgBox("You can't delete your pool 'Add a new pool...'  Change the name of your pool ")
            Exit Sub
        End If

        If TextBox1.Text = "" Then
            MsgBox("You can't delete a pool that has no name!  You bring shame on your family!")
            Exit Sub
        End If



        Dim appPath As String = Application.StartupPath()
        Dim PoolsConfig As mPools = GetAIOMinerJson()
        Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList

        Dim SpecialCoin As String
        SpecialCoin = CoinCombo.Text

        If CoinCombo.Text.Contains("*") Then
            SpecialCoin = CoinCombo.Text.Replace("*", "")
        End If
        Dim FoundCoin As Coin = TheCoins.Find(Function(x) x.Type = SpecialCoin AndAlso x.Pool = TextBox1.Text.Trim)
        If FoundCoin IsNot Nothing Then
            TheCoins.Remove(FoundCoin)
        End If

        'Save the JSON File
        PoolsConfig.Pools.Coins = TheCoins.ToArray

        Try
            SaveAIOMinerJson(PoolsConfig)


            'If PoolCombo.Items.Count <> 0 Then



            '    refreshPool()
            '    ClearText()
            '    'PoolCombo.SelectedIndex = 0
            'Else
            '    'GroupBox2.Enabled = False
            'End If
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            CheckBox1.Checked = False
            AdvancedLabel.Text = ""

            PubShared.currentargs = ""
            PubShared.altargument = ""


            MsgBox("We just removed " & TextBox1.Text & " pool for your " & CoinCombo.Text & " coins")
            ClearText()
            refreshPool()

        Catch ex As Exception
            MsgBox(ex.Message)
            LogUpdate(ex.Message, eLogLevel.Err)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text.Length <> 0 AndAlso TextBox2.Text.Length <> 0 AndAlso TextBox3.Text.Length <> 0 AndAlso TextBox4.Text.Length <> 0 Then
        Else
            MsgBox("You are missing a setting!  Please review")
            LogUpdate("Missing Settings!")
            Exit Sub
        End If

        If TextBox1.Text = "Add a New Pool" Then
            MsgBox("You can't test your pool 'Add a New Pool' Change the name of your pool")
            Exit Sub
        End If

        Dim TP As New TestPool
        TP.PoolTM = TextBox1.Text
        TP.CoinTM = CoinCombo.Text
        TP.ipTM = TextBox2.Text
        TP.PortTM = TextBox3.Text
        TP.WalletTM = TextBox4.Text
        TP.PassTM = TextBox5.Text
        TP.AlgoTM = AlgoTxt.Text

        TP.ShowDialog()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        refreshPool()
        Me.Close()
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub IntensityBox1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'SaveAIOsetting("intensity", IntensityBox1.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)
        Dim result As Integer = MessageBox.Show("Do you want to reset all coins pools?  This will kill each and every pool you have for each and every coin you have..this is more for the dev than it is for you...", "AIOMiner Killer 1.0", MessageBoxButtons.YesNoCancel)
        If result = DialogResult.Cancel Then
            MessageBox.Show("Cancel pressed")
        ElseIf result = DialogResult.No Then
            MessageBox.Show("No pressed")
        ElseIf result = DialogResult.Yes Then
            MessageBox.Show("Yes pressed")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If CoinCombo.Text = "" Then
            MsgBox("Please select a coin")
            Exit Sub
        End If

        If TextBox1.Text.Length <> 0 AndAlso TextBox2.Text.Length <> 0 AndAlso TextBox3.Text.Length <> 0 AndAlso TextBox4.Text.Length <> 0 Then
        Else
            MsgBox("You are missing a setting!  Please review")
            LogUpdate("Missing Settings!")
            Exit Sub
        End If

        Dim DillyDillyCoinSilly As String = CoinCombo.Text.Trim

        'if coin has an * in it, remove it
        If dillydillycoinsilly.Contains("*") Then
            DillyDillyCoinSilly = DillyDillyCoinSilly.Replace("*", "")
        End If
        Dim paSettings As New PoolAdvancedSettings With {.currentargs = "", .altargument = "", .algo = "", .ip = TextBox2.Text,
                                                          .port = TextBox3.Text, .worker = TextBox4.Text, .pool = TextBox1.Text,
                                                          .password = TextBox5.Text, .coin = DillyDillyCoinSilly}

        Dim frm As New PoolAdvanced With {
            .paSettings = paSettings
        }


        'frm.CoinInPoolSettings = CoinCombo.Text.Trim
        'frm.PoolInPoolSettings = TextBox1.Text
        frm.ShowDialog()

    End Sub

    Private Sub PoolSettings_BackColorChanged(sender As Object, e As EventArgs) Handles Me.BackColorChanged

    End Sub

    Private Sub PoolSettings_Activated(sender As Object, e As EventArgs) Handles Me.Activated


        If PubShared.newcoin = True Then
            CoinRefresh()
            PubShared.newcoin = False
        End If

        If Not PubShared.currentargs = "" Then
            AdvancedLabel.Text = "Yes"
        Else
            AdvancedLabel.Text = "No"
        End If


    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Button5_Click_1(sender As Object, e As EventArgs) Handles Button5.Click
        Dim frm As New CustomCoin
        frm.ShowDialog()



    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label9_MouseHover(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim coinname As String
        coinname = CoinCombo.Text

        If coinname.Contains("*") Then
            coinname = coinname.Replace("*", "")
        End If
        Process.Start("{{WIKI_SITE}}index.php/" & coinname)

    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Process.Start("{{WIKI_SITE}}index.php/PoolSettings")

    End Sub

	Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click


		If CoinCombo.Text = "Add a New pool..." Then
			MsgBox("So, Add a New Pool... is a coin you made?  Oh we are just gonna leave that there!")
			Exit Sub
		End If

		If CoinCombo.Text.Trim = "" Then
			MsgBox("You can't delete a coin that has no name!  You bring shame on your family!")
			Exit Sub
		End If



		Dim appPath As String = Application.StartupPath()
		Dim PoolsConfig As mPools = GetAIOMinerJson()
		Dim TheCoins As List(Of Coin) = PoolsConfig.Pools.Coins.ToList

		Dim SpecialCoin As String
		SpecialCoin = CoinCombo.Text

		If CoinCombo.Text.Contains("*") Then
			SpecialCoin = CoinCombo.Text.Replace("*", "")
		End If

		Dim foundit As Boolean = False
		Do Until foundit = True
			Dim FoundCoin As Coin = TheCoins.Find(Function(x) x.Type = SpecialCoin)
			If FoundCoin IsNot Nothing Then
				TheCoins.Remove(FoundCoin)
			Else
				foundit = True
			End If
		Loop


		'Save the JSON File
		PoolsConfig.Pools.Coins = TheCoins.ToArray

		Try
			SaveAIOMinerJson(PoolsConfig)


			'If PoolCombo.Items.Count <> 0 Then



			'    refreshPool()
			'    ClearText()
			'    'PoolCombo.SelectedIndex = 0
			'Else
			'    'GroupBox2.Enabled = False
			'End If
			TextBox1.Text = ""
			TextBox2.Text = ""
			TextBox3.Text = ""
			TextBox4.Text = ""
			TextBox5.Text = ""
			CheckBox1.Checked = False
			AdvancedLabel.Text = ""

			PubShared.currentargs = ""
			PubShared.altargument = ""


			MsgBox("We just removed " & SpecialCoin & "!  It won't bother you again.  Unless it was a default coin, then well...it's still here!")

			CoinCombo.Items.Remove(SpecialCoin)
			ClearText()
			refreshPool()

		Catch ex As Exception
			MsgBox(ex.Message)
			LogUpdate(ex.Message, eLogLevel.Err)
		End Try
	End Sub
End Class