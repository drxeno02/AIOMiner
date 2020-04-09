Imports AIOminer.AIOMiner
Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils

Public Class DonateSettings

    Private Sub DonateSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RichTextBox1.ReadOnly = True
        RichTextBox1.Text = "Per Request, here you can donate some of your hash to AIOMiner.  This is an Opt-In only. "

        RichTextBox2.ReadOnly = True
        RichTextBox2.Text = "Every night at One (1) AM we will check our pillows to see if you have left us a donation %, if you have we will stop mining what you currently are, start up for however long you allow for us to steal from the power gods and then stop the donation and start back to your new moon coin! Thank you for anything and we will use this money to make you a better product!  Remember, this product was meant to be free.  At no point should you feel pressured into doing this.  You worked hard for your GPU's, you work hard to pay the power bills.  Good Luck Mining!"

        DonationBOX.Items.Add("No Thanks")
        DonationBOX.Items.Add("Thank You!")
        DonationBOX.Items.Add("Whoa, you are amazing!")
        DonationBOX.Items.Add("Add more features!")
        DonationBOX.Items.Add("Team Lambo")
        DonationBOX.Items.Add("Diamond Platinum Level")



        Try
            DonationBOX.Text = ReturnAIOsetting("donation")
        Catch ex As Exception

        End Try



    End Sub

    Private Sub DonationBOX_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DonationBOX.SelectedIndexChanged
        If DonationBOX.Text = "Thank You!" Then
            MinutesLBL.Text = "2.5"
        End If

        If DonationBOX.Text = "Whoa, you are amazing!" Then
            MinutesLBL.Text = "5.0"
        End If

        If DonationBOX.Text = "Add more features!" Then
            MinutesLBL.Text = "7.5"
        End If

        If DonationBOX.Text = "Team Lambo" Then
            MinutesLBL.Text = "10.0"
        End If

        If DonationBOX.Text = "Diamond Platinum Level" Then
            MinutesLBL.Text = "15.0"
        End If

        If DonationBOX.Text = "No Thanks" Then
            MinutesLBL.Text = "0"
        End If


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            SaveAIOsetting("donation", DonationBOX.Text)
            MsgBox("We have updated your Donation settings!  Thank You!")
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()

    End Sub
End Class