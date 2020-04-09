

Imports System.Management

Public Class GPU

    Private Shared Function GPU_Q(deviceID As Int32, stat As String) As String
        ' VBIOS Version
        ' GPU UUID
        ' Fan Speed
        ' Performance State
        ' FB Memory Usage

        'Utilization
        '''''Gpu
        '''''Memory

        'Temperature
        'Power Readings
        ''''''Power Draw == power.draw
        ''''''Power Limit == power.limit
        Try
            Dim appPath As String = Application.StartupPath()
            Dim args As String
            Dim p As Process = New Process
            Dim output As String
            args = "-i " + deviceID.ToString + " --format=csv --query-gpu=" + stat
            With p
                .StartInfo.CreateNoWindow = True
                .StartInfo.UseShellExecute = False
                .StartInfo.RedirectStandardOutput = True
                .StartInfo.FileName = appPath + "\nvidia-smi.exe"
                .StartInfo.Arguments = args
                .Start()
                output = .StandardOutput.ReadToEnd
            End With
            Return output
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    Shared Function GPU_BIOS(DID As String) As String


        'Dim I As String
        'I = GPU_Q(DID, "vbios_version")
        'I = I.Replace("vbios_version", "")
        'I = I.Replace(" ", "")
        'Return I

        Return GPU_Q_Proxy(DID, "vbios_version")

    End Function
    Shared Function GPU_DRIVER(DID As String) As String

        'Dim I As String
        ''Get the Driver Information
        'I = GPU_Q(DID, "driver_version")
        'I = I.Replace("driver_version", "")
        'I = I.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "driver_version")
    End Function
    Shared Function GPU_TEMP(DID As String) As String
        'Dim I As String
        'I = GPU_Q(DID, "temperature.gpu")
        'I = I.Replace("temperature.gpu", "")
        'I = I.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "temperature.gpu")
    End Function
    Shared Function GPU_POWERDRAW(DID As String) As String
        'Dim I As String
        'I = GPU_Q(DID, "power.draw")
        'I = I.Replace("power.draw", "")
        'I = I.Replace(" ", "")

        Dim retVal As String = GPU_Q_Proxy(DID, "power.draw").Replace("[W]", "").Replace("W", "").Trim
        ' Dim intRslt As Integer
        ' If Integer.TryParse(retVal, intRslt) Then
        ' retVal = CStr(intRslt)
        ' Else
        ' retVal = "-1"
        ' End If

        Return retVal

    End Function
    Shared Function GPU_POWERLIMIT(DID As String) As String
        'Dim I As String
        'I = GPU_Q(DID, "power.max_limit")
        'I = I.Replace("power.max_limit", "")
        'I = I.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "power.max_limit").Replace("[W]", "")

    End Function
    Shared Function GetGpuCount()
        Dim GraphicsCardName As String
        Dim i As Int64
        Dim cardcount As Integer = 0
        Try
            i = 0
            Dim WmiSelect As New ManagementObjectSearcher("Select * FROM Win32_VideoController")
            For Each WmiResults As ManagementObject In WmiSelect.Get()
                GraphicsCardName = WmiResults.GetPropertyValue("Name").ToString
                'Get the lits of NVIDIA CARDS
                If GraphicsCardName.ToString.ToUpper.Contains("NVIDIA") Then
                    cardcount = i + 1
                ElseIf GraphicsCardName.ToString.ToUpper.Contains("AMD") Then
                    cardcount = i + 1
                End If
            Next
        Catch ex As Exception
            Return 0
        End Try

        Return cardcount

    End Function
    Shared Function GPU_CLOCKSPEED(DID As String) As String
        'Get the GPU Clock Speed
        'clocks.current.graphics
        'Dim i As String
        'i = GPU_Q(DID, "clocks.current.graphics")
        'i = i.Replace("clocks.current.graphics", "")
        'i = i.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "clocks.current.graphics").Replace("[MHz]", "")

    End Function
    Shared Function GPU_MEMORYSPEED(DID As String) As String
        'Dim I As String
        ''Get the Memory Clock Speed
        ''clocks.current.graphics
        'I = GPU_Q(DID, "clocks.current.memory")
        'I = I.Replace("clocks.current.memory", "")
        'I = I.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "clocks.current.memory").Replace("[MHz]", "")

    End Function

    Shared Function GPU_FANSPEED(DID As String) As String
        'Dim I As String
        ''Get the Memory Clock Speed
        ''clocks.current.graphics
        'I = GPU_Q(DID, "fan.speed")
        'I = I.Replace("fan.speed", "")
        'I = I.Replace(" ", "")

        Return GPU_Q_Proxy(DID, "fan.speed").Replace("[%]", "")

    End Function

    Shared Function GPU_POWERSTATE(DID As String) As String
        'Dim I As String
        ''Get the Memory Clock Speed
        ''clocks.current.graphics
        'I = GPU_Q(DID, "pstate")
        'I = I.Replace("pstate", "")
        'I = I.Replace(" ", "")

        'Return I

        Return GPU_Q_Proxy(DID, "pstate")

    End Function

    Private Shared Function GPU_Q_Proxy(ByVal _devId As Int32, ByVal _stat As String) As String
        Dim retVal As String = String.Empty

        retVal = GPU_Q(_devId, _stat)
        retVal = retVal.Replace(_stat, "").Replace(" ", "")

        Return retVal

    End Function


End Class
