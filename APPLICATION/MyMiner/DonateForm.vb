Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils
Imports AIOminer.MyEmail
Imports AIOminer.AIOMinerWebAPI






Public Class DonateForm
    Private Sub DonateForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim DonationTime As Integer
        Dim DonationChecks As Integer

        'Flip Donation Started Process
        PubShared.donationsStarted = True
        PubShared.donationStartedTime = DateTime.Now
        'Find out how long we should mine


        Select Case ReturnAIOsetting("donation")
            Case "Thank You!"
                DonationTime = 2.5
                '150 Seconds
                DonationChecks = 150
                            '50 Checks if time is every 3 seconds

            Case "Whoa, you are amazing!"
                DonationTime = 5.0
                '300 Seconds
                DonationChecks = 300
                            '100 Checks if time is every 3 seconds

            Case "Add more features!"
                DonationTime = 7.5
                '450 Seconds
                DonationChecks = 450
                            '150 Checks if time is every 3 seconds

            Case "Team Lambo"
                DonationTime = 10.0
                '600 Seconds
                DonationChecks = 600
                            '200 Checks if the time is every 3 seconds

            Case "Diamond Platinum Level"
                DonationTime = 15.0
                '900 Seconds
                DonationChecks = 900
                '300 Checks if the time is every 3 seconds
        End Select

        SecondsLBL.Text = DonationChecks



        'Get the current mining 
        If PubShared.monitoring = True Then
            PubShared.donationPreviousMining = True

            'Get what they were mining and save it
            PubShared.donationPreviousCoin = PubShared.coin

            'Stop Mining
            Do Until 1 = 2
                Application.DoEvents()
                If AIOMiner.Button1.Text.ToLower = "stop" Then
                    AIOMiner.Button1.PerformClick()
                    Exit Do
                End If
                Threading.Thread.Sleep(500)
            Loop

            'Wait for stopped mining to be done with
            Do Until 1 = 2
                Threading.Thread.Sleep(1000)
                Application.DoEvents()
                If PubShared.StoppedMiningIsShowing = True Then
                Else
                    Exit Do
                End If
            Loop

            'Start Mining Donation Coin
            Do Until 1 = 2
                Application.DoEvents()
                If AIOMiner.Button1.Text.ToLower = "start" Then
                    Do Until 1 = 2
                        Application.DoEvents()
                        If AIOMiner.ComboBox1.Text <> "Donate" Then
                            AIOMiner.ComboBox1.Text = "Donate"
                        Else
                            Exit Do
                        End If
                        Threading.Thread.Sleep(500)
                    Loop
                End If

                'Check Again
                If AIOMiner.ComboBox1.Text = "Donate" Then
                    AIOMiner.Button1.PerformClick()
                    Exit Do
                End If
            Loop

            'Start the timer
            Timer1.Interval = 1000
            Timer1.Start()



        Else

            'Start Mining Donation Coin
            Do Until 1 = 2
                Application.DoEvents()
                If AIOMiner.Button1.Text.ToLower = "start" Then
                    Do Until 1 = 2
                        Application.DoEvents()
                        If AIOMiner.ComboBox1.Text <> "Donate" Then
                            Dim comboBox1 As ComboBox = AIOMiner.ComboBox1
                            comboBox1.Text = "Donate"
                        Else
                            Exit Do
                        End If
                        Threading.Thread.Sleep(500)
                    Loop
                End If

                'Check Again
                If AIOMiner.ComboBox1.Text = "Donate" Then
                    AIOMiner.Button1.PerformClick()
                    Exit Do
                End If
            Loop

            'Start the timer
            Timer1.Interval = 1000
            Timer1.Start()

        End If





    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Dim secondsint As Integer
        secondsint = Int(SecondsLBL.Text)
        If secondsint <= 0 Then
            'Stop mining donation
            PubShared.donationsCompletedToday = True

            'Stop Mining
            Do Until 1 = 2
                Application.DoEvents()
                If AIOMiner.Button1.Text.ToLower = "stop" Then
                    AIOMiner.Button1.PerformClick()
                    Exit Do
                End If
                Threading.Thread.Sleep(500)
            Loop

            'Wait for stopped mining to be done with
            Do Until 1 = 2
                Threading.Thread.Sleep(500)
                Application.DoEvents()
                If PubShared.StoppedMiningIsShowing = True Then
                Else
                    Exit Do
                End If
            Loop

            'Start back what they were previously mining
            If PubShared.donationPreviousCoin <> "" Then
                Do Until 1 = 2
                    Application.DoEvents()
                    If AIOMiner.Button1.Text.ToLower = "start" Then
                        AIOMiner.ComboBox1.Text = PubShared.donationPreviousCoin
                        'Change it no matter what
                        Do Until AIOMiner.ComboBox1.Text.ToLower <> "donate"
                            Application.DoEvents()
                            If AIOMiner.ComboBox1.Text = "Donate" Then
                                AIOMiner.ComboBox1.Text = PubShared.donationPreviousCoin
                                Exit Do
                            End If
                            Threading.Thread.Sleep(500)
                        Loop
                        AIOMiner.Button1.PerformClick()
                        Exit Do
                    End If
                    Threading.Thread.Sleep(500)
                Loop
            End If

            Me.Close()
        Else
            secondsint = secondsint - 1
            SecondsLBL.Text = Str(secondsint)
        End If
    End Sub
End Class