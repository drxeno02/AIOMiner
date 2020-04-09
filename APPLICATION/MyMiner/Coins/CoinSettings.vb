Imports AIOminer.General_Utils
Imports AIOminer.CoinMining



Public Class CoinSettings
    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = True Then
            CheckBox2.Checked = False
            CheckBox1.Checked = False
            SaveAIOsetting("prices", "GBP")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub CoinSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        RichTextBox1.Text = "Place in the 3-4 letter abbreviation (BTC,ETH,VTC...) and hit the test button.  If it goes green it has been saved and will show up on your ticker.  To remove a coin just remove all text and hit test."

        'Speed
        For i = 1 To 11
            If Not ComboBox1.Items.Contains(i) Then ComboBox1.Items.Add(i)


        Next
        ComboBox1.Text = ReturnAIOsetting("marqueespeed")
        'Size
        For i = 8 To 16
            If Not ComboBox2.Items.Contains(i) Then ComboBox2.Items.Add(i)


        Next
        ComboBox2.Text = ReturnAIOsetting("marqueesize")

        If ReturnAIOsetting("marquee") = "True" Then
            CheckBox4.Checked = True
        Else
            CheckBox4.Checked = False
        End If

        Select Case ReturnAIOsetting("prices")
            Case "USD"
                CheckBox1.Checked = True

            Case "EUR"
                CheckBox2.Checked = True

            Case "GBP"
                CheckBox3.Checked = True

        End Select

        TextBox1.Text = ReturnAIOsetting("coinprice1")
        TextBox2.Text = ReturnAIOsetting("coinprice2")
        TextBox3.Text = ReturnAIOsetting("coinprice3")
        TextBox4.Text = ReturnAIOsetting("coinprice4")
        TextBox5.Text = ReturnAIOsetting("coinprice5")
        TextBox6.Text = ReturnAIOsetting("coinprice6")
        TextBox7.Text = ReturnAIOsetting("coinprice7")

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            CheckBox2.Checked = False
            CheckBox3.Checked = False
            SaveAIOsetting("prices", "USD")
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        If CheckBox2.Checked = True Then
            CheckBox3.Checked = False
            CheckBox1.Checked = False
            SaveAIOsetting("prices", "EUR")
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If UpdateCoins(TextBox1.Text) IsNot "" Then
            TextBox1.BackColor = Color.Green
            SaveAIOsetting("coinprice1", TextBox1.Text)
        Else
            TextBox1.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice1", " ")
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        SaveAIOsetting("marqueespeed", ComboBox1.Text)
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        SaveAIOsetting("marqueesize", ComboBox2.Text)
    End Sub

    Private Sub CheckBox4_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox4.CheckedChanged
        If CheckBox4.Checked = True Then
            SaveAIOsetting("marquee", "True")
            AIOMiner.PricesLBL.Visible = True
            AIOMiner.Marquee.Start()
        Else
            SaveAIOsetting("marquee", "False")
            AIOMiner.PricesLBL.Visible = False
            AIOMiner.Marquee.Stop()
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If UpdateCoins(TextBox2.Text) IsNot "" Then
            TextBox2.BackColor = Color.Green
            SaveAIOsetting("coinprice2", TextBox2.Text)
        Else
            TextBox2.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice2", "")
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If UpdateCoins(TextBox3.Text) IsNot "" Then
            TextBox3.BackColor = Color.Green
            SaveAIOsetting("coinprice3", TextBox3.Text)
        Else
            TextBox3.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice3", " ")
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If UpdateCoins(TextBox4.Text) IsNot "" Then
            TextBox4.BackColor = Color.Green
            SaveAIOsetting("coinprice4", TextBox4.Text)
        Else
            TextBox4.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice4", " ")
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If UpdateCoins(TextBox5.Text) IsNot "" Then
            TextBox5.BackColor = Color.Green
            SaveAIOsetting("coinprice5", TextBox5.Text)
        Else
            TextBox5.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice5", " ")
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If UpdateCoins(TextBox6.Text) IsNot "" Then
            TextBox6.BackColor = Color.Green
            SaveAIOsetting("coinprice6", TextBox6.Text)
        Else
            TextBox6.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice6", " ")
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If UpdateCoins(TextBox7.Text) IsNot "" Then
            TextBox7.BackColor = Color.Green
            SaveAIOsetting("coinprice7", TextBox7.Text)
        Else
            TextBox7.BackColor = Color.PaleVioletRed
            SaveAIOsetting("coinprice7", " ")
        End If
    End Sub
End Class