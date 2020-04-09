Imports System.ComponentModel
Imports System.Management
Imports System.Text.RegularExpressions
Imports System.Threading

Imports AIOminer.GPU
Imports AIOminer.DeviceInfo
Imports AIOminer.General_Utils





Public Class Overclocker
    Public FMC = False



    Private Sub VideoCard_Info_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If ReturnAIOsetting("disableamd") = "True" Then
            Button3.Text = "AMD: Disabled"
        Else
            Button3.Text = "AMD: Enabled"
        End If

        If ReturnAIOsetting("disablenvidia") = "True" Then
            Button4.Text = "NVIDIA: Disabled"
        Else
            Button4.Text = "NVIDIA: Enabled"
        End If

        ComboBox1.Items.Clear()

        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False
        Dim GraphicsCardName = String.Empty
        Dim i As Integer

        Try
            i = 0
            Dim WmiSelect As New ManagementObjectSearcher("SELECT * FROM Win32_VideoController")
            For Each WmiResults As ManagementObject In WmiSelect.Get()
                GraphicsCardName = WmiResults.GetPropertyValue("Name").ToString
                If GraphicsCardName.ToString.Contains("NVIDIA") Then
                    ComboBox1.Items.Add(GraphicsCardName + "     [" + i.ToString + "]")
                    i = i + 1
                End If
                If GraphicsCardName.ToString.ToUpper.Contains("RADEON") Then
                    ComboBox1.Items.Add(GraphicsCardName + "     [" + i.ToString + "]")
                    i = i + 1
                End If

            Next
        Catch err As ManagementException
            MessageBox.Show(err.Message & " Seems you have an unsupported CUDA9 video card")
        End Try

        'Check to see if disable all Nvidia is checked
        'If ReturnAIOsetting("disableamd") = "True" Then CheckBox3.Checked = True
        'If ReturnAIOsetting("disablenvidia") = "True" Then CheckBox2.Checked = True
    End Sub

    Private Sub TrackBar1_Scroll(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        'For NVIDIA
        Try
            BackgroundWorker1.CancelAsync()
        Catch ex As Exception

        End Try

        Try
            Dim DID As String
            DID = GetDeviceID(ComboBox1.Text)
            If DID = "0" Then
                If ReturnAIOsetting("gpu0") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "1" Then
                If ReturnAIOsetting("gpu1") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "2" Then
                If ReturnAIOsetting("gpu2") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "3" Then
                If ReturnAIOsetting("gpu3") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "4" Then
                If ReturnAIOsetting("gpu4") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "5" Then
                If ReturnAIOsetting("gpu5") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "6" Then
                If ReturnAIOsetting("gpu6") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If DID = "7" Then
                If ReturnAIOsetting("gpu7") = "True" Then
                    CheckBox1.Checked = True
                Else
                    CheckBox1.Checked = False
                End If
            End If

            If PubShared.nvidia = True Then
                TextBox1.Text = GPU_BIOS(DID)
                TextBox4.Text = GPU_POWERDRAW(DID)
                TextBox5.Text = GPU_DRIVER(DID)
                TextBox2.Text = GPU_CLOCKSPEED(DID)
                TextBox3.Text = GPU_MEMORYSPEED(DID)
                TextBox6.Text = GPU_TEMP(DID)
                TextBox7.Text = GPU_POWERSTATE(DID)
                TextBox8.Text = GPU_FANSPEED(DID)
                BackgroundWorker1.RunWorkerAsync()
            End If


            If PubShared.amd = True Then
                TextBox1.Text = "AMD N/A"
                TextBox4.Text = "AMD N/A"
                TextBox5.Text = "AMD N/A"
                TextBox2.Text = "AMD N/A"
                TextBox3.Text = "AMD N/A"
                TextBox6.Text = "AMD N/A"
                TextBox7.Text = "AMD N/A"
                TextBox8.Text = "AMD N/A"
            End If

        Catch ex As Exception
        End Try

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        Me.Close()
        'Get Tick Frequency

        '1 - 55%
        '2 - 50%
        '3 - 60%
        '4 - 70%
        '5 - 75%
        '6 - 80%
        '7 - 85%
        '8 - 90%
        '9 - 95%
        '10 - 100%
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim DID As String
        Try
            Do Until FMC = True
                DID = GetDeviceID(ComboBox1.Text)
                TextBox2.Text = GPU_CLOCKSPEED(DID)
                TextBox4.Text = GPU_POWERDRAW(DID)
                TextBox3.Text = GPU_MEMORYSPEED(DID)
                TextBox6.Text = GPU_TEMP(DID)
                TextBox7.Text = GPU_POWERSTATE(DID)
                TextBox8.Text = GPU_FANSPEED(DID)
                Threading.Thread.Sleep(3000)
            Loop
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Overclocker_Closed(sender As Object, e As EventArgs) Handles Me.Closed


    End Sub

    Private Sub Overclocker_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        FMC = (True)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim DID As String
        DID = GetDeviceID(ComboBox1.Text)
        If DID = "0" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu0", "True")
            Else
                SaveAIOsetting("gpu0", "False")
            End If
        End If

        If DID = "1" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu1", "True")
            Else
                SaveAIOsetting("gpu1", "False")
            End If
        End If

        If DID = "2" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu2", "True")
            Else
                SaveAIOsetting("gpu2", "False")
            End If
        End If

        If DID = "3" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu3", "True")
            Else
                SaveAIOsetting("gpu3", "False")
            End If
        End If

        If DID = "4" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu4", "True")
            Else
                SaveAIOsetting("gpu4", "False")
            End If
        End If

        If DID = "5" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu5", "True")
            Else
                SaveAIOsetting("gpu5", "False")
            End If
        End If

        If DID = "6" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu6", "True")
            Else
                SaveAIOsetting("gpu6", "False")
            End If
        End If

        If DID = "7" Then
            If CheckBox1.Checked = True Then
                SaveAIOsetting("gpu7", "True")
            Else
                SaveAIOsetting("gpu7", "False")
            End If
        End If

        'If checkbox2 (nvidia) is checked but was not previously let them know
        'If CheckBox2.Checked = True Then

        'End If

        'If CheckBox3.Checked = True Then
        '    MsgBox("We have disabled AMD from mining, it's possible some applications will still try to use it, but we will do our best!")
        'End If


    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs)
        'If CheckBox2.Checked = True Then
        '    MsgBox("We have disabled NVIDIA from mining, it's possible some applications will still try to use it, but we will do our best!")
        '    SaveAIOsetting("disablenvidia", "True")
        'Else
        '    MsgBox("We have re-enabled NVIDIA cards.")
        '    SaveAIOsetting("disablenvidia", "False")
        'End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(sender As Object, e As EventArgs)
        'If CheckBox3.Checked = True Then
        '    MsgBox("We have disabled AMD from mining, it's possible some applications will still try to use it, but we will do our best!")
        '    SaveAIOsetting("disableamd", "True")
        'Else
        '    MsgBox("We have re-enabled AMD cards.")
        '    SaveAIOsetting("disableamd", "False")
        'End If
    End Sub

    Private Sub TabPage8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If Button3.Text = "AMD: Enabled" Then
            Button3.Text = "AMD: Disabled"
            SaveAIOsetting("disableamd", "True")
        Else
            Button3.Text = "AMD: Enabled"
            SaveAIOsetting("disableamd", "False")
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If Button4.Text = "NVIDIA: Enabled" Then
            Button4.Text = "NVIDIA: Disabled"
            SaveAIOsetting("disablenvidia", "True")
        Else
            Button4.Text = "NVIDIA: Enabled"
            SaveAIOsetting("disablenvidia", "False")
        End If
    End Sub
End Class