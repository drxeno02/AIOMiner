Imports System.IO
Imports System.ServiceProcess

Imports AIOminer.General_Utils
Imports AIOminer.JSON_Utils
Imports AIOminer.Log




Public Class MinerSettings
    Private Sub MinerSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        VirtualMemoryTXT.ReadOnly = True
        VirtualMemoryTXT.Text = "If your GPU Count is above (1), you need atleast 16GB of Page File Space/Virtual Memory for Ethash.  32GB For Cryptonight based coins."
        HardDriveTXT.ReadOnly = True
        HardDriveTXT.Text = "You need atleast 1.5GB of space for AIOMiner and the applications it downloads.  Keep an eye on your free disk space!"
        WindowsDefenderTXT.Text = "Most users will need to whitelist your mining folder from Windows Defender and other Virus Scanners.  As you can imagine, having this installed on an unauthorized machine is not ideal.  If the button is red to the right, click it to whitelist your AIOMiner folder to help mining applications run."
        WindowsDefenderTXT.ReadOnly = True
        WindowsUpdateTXT.ReadOnly = True
        WindowsUpdateTXT.Text = "Windows Updates for a Rig owner can force a surprise reboot and stop your mining.  You want to disable the Windows Updates Service (while we don't advise it) to stop this auto update from occuring.  This is especially not recomended if you are on a personal machine that won't be mining 24/7.  CHANGES TO THIS WILL REQUIRE A REBOOT "

        Refresh_Stats()

    End Sub
    Private Sub Refresh_Stats()

        Try


            ' Windows Update
            For Each s As ServiceController In ServiceController.GetServices()
                If s.ServiceName = "wuauserv" Then
                    If s.Status = ServiceControllerStatus.Running Then
                        WinUpdateLBL.Text = "Enabled"
                        WindowsUpdateBTN.Text = "Fix"
                        WindowsUpdateBTN.BackColor = Color.MediumPurple
                        PubShared.WindowsUpdatePassed = True

                        'MsgBox("Windows Update is running")
                    Else
                        WinUpdateLBL.Text = "Disabled"
                        WindowsUpdateBTN.Text = "OK!"
                        WindowsUpdateBTN.BackColor = Color.Green
                        PubShared.WindowsUpdatePassed = True
                        'MsgBox("windows update is not runnig")
                    End If
                End If

            Next

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            WinUpdateLBL.Text = "Error!"
            WindowsUpdateBTN.Text = "Fix"
            PubShared.WindowsUpdatePassed = True
        End Try


        Try


            'Windows Defender
            Dim keyname As String = Application.StartupPath()
            Dim readValue As String
            readValue = My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\Microsoft\Windows Defender\Exclusions\Paths", keyname, Nothing)

            ' Dim regKey As Object = My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows Defender\Exclusions\Paths", True).GetValue(keyname)
            If readValue Is Nothing Then
                DefenderBTN.BackColor = Color.MediumPurple
                DefenderBTN.Text = "Fix"
                PubShared.WindowsDefenderPassed = True
            Else
                DefenderBTN.BackColor = Color.Green
                DefenderBTN.Text = "OK!"
                PubShared.WindowsDefenderPassed = True

            End If


        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            DefenderBTN.BackColor = Color.MediumPurple
            DefenderBTN.Text = "Fix"
            PubShared.WindowsDefenderPassed = True
        End Try

        Try
            'DiskSpace
            Dim RETURNED_SPACE As String = ""
            RETURNED_SPACE = ReturnAvaliableDiskSpace()

            If RETURNED_SPACE <> "ERR" Then
                Try
                    If Convert.ToInt32(RETURNED_SPACE) < 1.5 Then
                        FreeDiskSpaceBTN.Text = RETURNED_SPACE & " GB FREE"
                        FreeDiskSpaceBTN.BackColor = Color.MediumPurple
                        PubShared.HardDriveCheckPassed = True
                    Else
                        FreeDiskSpaceBTN.Text = RETURNED_SPACE & " GB FREE"
                        FreeDiskSpaceBTN.BackColor = Color.Green
                        PubShared.HardDriveCheckPassed = True


                    End If
                Catch ex As Exception
                    FreeDiskSpaceBTN.Text = "ERROR"
                    FreeDiskSpaceBTN.BackColor = Color.MediumPurple
                    PubShared.HardDriveCheckPassed = True
                End Try
            Else
                FreeDiskSpaceBTN.Text = "ERROR"
                FreeDiskSpaceBTN.BackColor = Color.MediumPurple
                PubShared.HardDriveCheckPassed = True

            End If
        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            FreeDiskSpaceBTN.Text = "ERROR"
            FreeDiskSpaceBTN.BackColor = Color.MediumPurple
            PubShared.HardDriveCheckPassed = True
        End Try

        Try


            'VirtualMemory



            Dim RETURNED_VIRUTAL_MEMORY As Integer = 0
            RETURNED_VIRUTAL_MEMORY = Convert.ToInt32(ReturnVirtualMemory)
            Dim RETURNED_GB As String = ""
            RETURNED_GB = (RETURNED_VIRUTAL_MEMORY / 1024).ToString

            ReturnedVirtualGbLBL.Text = RETURNED_GB.Substring(0, 4) + " GB"

            If PubShared.cardcount <= 1 Then
                'This usually doesn' matter with one card, do as you want
                ETHVirtualMemoryBTN.BackColor = Color.Green
                CNVirtualMemoryBTN.BackColor = Color.Green
                ETHVirtualMemoryBTN.Text = "OK!"
                CNVirtualMemoryBTN.Text = "OK!"
                PubShared.VirtualMemoryPassed = True

            Else

                If RETURNED_VIRUTAL_MEMORY < 15000 Then
                    ETHVirtualMemoryBTN.BackColor = Color.MediumPurple
                    ETHVirtualMemoryBTN.Text = "Fix"
                    PubShared.VirtualMemoryPassed = True
                Else
                    ETHVirtualMemoryBTN.BackColor = Color.Green
                    ETHVirtualMemoryBTN.Text = "OK!"
                    PubShared.VirtualMemoryPassed = True
                End If


                If RETURNED_VIRUTAL_MEMORY < 31000 Then
                    CNVirtualMemoryBTN.BackColor = Color.MediumPurple
                    CNVirtualMemoryBTN.Text = "Fix"
                    PubShared.VirtualMemoryPassed = True
                Else
                    CNVirtualMemoryBTN.BackColor = Color.Green
                    CNVirtualMemoryBTN.Text = "OK!"
                    PubShared.VirtualMemoryPassed = True
                    PubShared.VirtualMemoryPassed = True
                End If
            End If

        Catch ex As Exception
            LogUpdate(ex.Message, eLogLevel.Err)
            CNVirtualMemoryBTN.BackColor = Color.MediumPurple
            CNVirtualMemoryBTN.Text = "Fix"
            ETHVirtualMemoryBTN.BackColor = Color.MediumPurple
            ETHVirtualMemoryBTN.Text = "Fix"
            ReturnedVirtualGbLBL.Text = "No Pagefile Set"
            PubShared.VirtualMemoryPassed = True
        End Try

    End Sub

    Private Sub FreeDiskSpaceBTN_Click(sender As Object, e As EventArgs) Handles FreeDiskSpaceBTN.Click
        Process.Start("explorer.exe")
    End Sub

    Private Sub ETHVirtualMemoryBTN_Click(sender As Object, e As EventArgs) Handles ETHVirtualMemoryBTN.Click
        If sender.text.ToString.ToLower.Contains("fix") Then
            Process.Start("https://www.tomshardware.com/news/how-to-manage-virtual-memory-pagefile-windows-10,36929.html")
        Else

        End If
    End Sub

    Private Sub CNVirtualMemoryBTN_Click(sender As Object, e As EventArgs) Handles CNVirtualMemoryBTN.Click
        If sender.text.ToString.ToLower.Contains("fix") Then
            Process.Start("https://www.tomshardware.com/news/how-to-manage-virtual-memory-pagefile-windows-10,36929.html")
        Else

        End If
    End Sub

    Private Sub WindowsUpdateBTN_Click(sender As Object, e As EventArgs) Handles WindowsUpdateBTN.Click
        If WindowsUpdateBTN.BackColor = Color.MediumPurple Then
            Dim appPath As String = Application.StartupPath()
            Try
                Dim DefenderProcess As New ProcessStartInfo
                DefenderProcess.FileName = (appPath + "\AIO-DisableWindowsUpdates.exe")
                DefenderProcess.UseShellExecute = True
                'Process.Start(DefenderProcess)
                Dim zipper As System.Diagnostics.Process = System.Diagnostics.Process.Start(DefenderProcess)
                zipper.WaitForExit()
                Refresh_Stats()
            Catch ex As Exception
                MsgBox("Unable to Start/Find AIO-DisableWindowsUpdates.exe, Please contact support@aiominer.com")
            End Try
        Else

        End If

    End Sub

    Private Sub DefenderBTN_Click(sender As Object, e As EventArgs) Handles DefenderBTN.Click

        'Dim pHelp As New ProcessStartInfo
        'pHelp.FileName = appPath & "/Miners/NVIDIA/OhGodAnETHlargementPill/OhGodAnETHlargementPill-r2.exe"
        'pHelp.Arguments = ""
        'pHelp.UseShellExecute = True
        'pHelp.WindowStyle = ProcessWindowStyle.Normal
        'Dim proc As Process = Process.Start(pHelp)

        If DefenderBTN.BackColor = Color.MediumPurple Then
            Dim DefenderProcess As New ProcessStartInfo

            Dim appPath As String = Application.StartupPath()
            Try
                DefenderProcess.FileName = (appPath + "\AIO-WindowsDefenderExclusion.exe")
                DefenderProcess.UseShellExecute = True
                'Process.Start(DefenderProcess)
                Dim zipper As System.Diagnostics.Process = System.Diagnostics.Process.Start(DefenderProcess)
                zipper.WaitForExit()
                Refresh_Stats()

                '   Process.Start(appPath + "\AIO-WindowsDefenderExclusion.exe")

            Catch ex As Exception
                MsgBox("Unable to Start/Find AIO-WindowsDefenderExclusion.exe, Please contact support@aiominer.com")
            End Try
        Else

        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Refresh_Stats()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()

    End Sub
End Class