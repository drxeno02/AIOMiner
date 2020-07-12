<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MinerSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MinerSettings))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.WindowsDefenderLBL = New System.Windows.Forms.Label()
        Me.WindowsUpdateLBL = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.FreeDiskSpaceBTN = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.HardDriveTXT = New System.Windows.Forms.RichTextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.VirtualMemoryTXT = New System.Windows.Forms.RichTextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.WindowsDefenderTXT = New System.Windows.Forms.RichTextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.WindowsUpdateTXT = New System.Windows.Forms.RichTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ETHVirtualMemoryBTN = New System.Windows.Forms.Button()
        Me.CNVirtualMemoryBTN = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.ReturnedVirtualGbLBL = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DefenderBTN = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.WindowsUpdateBTN = New System.Windows.Forms.Button()
        Me.WinUpdateLBL = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Hard Drive Space:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 136)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Virtual Memory:"
        '
        'WindowsDefenderLBL
        '
        Me.WindowsDefenderLBL.AccessibleRole = System.Windows.Forms.AccessibleRole.SpinButton
        Me.WindowsDefenderLBL.AutoSize = True
        Me.WindowsDefenderLBL.Location = New System.Drawing.Point(21, 260)
        Me.WindowsDefenderLBL.Name = "WindowsDefenderLBL"
        Me.WindowsDefenderLBL.Size = New System.Drawing.Size(101, 13)
        Me.WindowsDefenderLBL.TabIndex = 2
        Me.WindowsDefenderLBL.Text = "Windows Defender:"
        '
        'WindowsUpdateLBL
        '
        Me.WindowsUpdateLBL.AutoSize = True
        Me.WindowsUpdateLBL.Location = New System.Drawing.Point(21, 374)
        Me.WindowsUpdateLBL.Name = "WindowsUpdateLBL"
        Me.WindowsUpdateLBL.Size = New System.Drawing.Size(97, 13)
        Me.WindowsUpdateLBL.TabIndex = 3
        Me.WindowsUpdateLBL.Text = "Windows Updates:"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LightGray
        Me.Panel1.Controls.Add(Me.FreeDiskSpaceBTN)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.HardDriveTXT)
        Me.Panel1.Location = New System.Drawing.Point(22, 38)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(948, 95)
        Me.Panel1.TabIndex = 4
        '
        'FreeDiskSpaceBTN
        '
        Me.FreeDiskSpaceBTN.Location = New System.Drawing.Point(764, 40)
        Me.FreeDiskSpaceBTN.Name = "FreeDiskSpaceBTN"
        Me.FreeDiskSpaceBTN.Size = New System.Drawing.Size(82, 27)
        Me.FreeDiskSpaceBTN.TabIndex = 3
        Me.FreeDiskSpaceBTN.Text = "Button1"
        Me.FreeDiskSpaceBTN.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(697, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(216, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Free Disk Space where AIOMiner is installed"
        '
        'HardDriveTXT
        '
        Me.HardDriveTXT.Location = New System.Drawing.Point(16, 12)
        Me.HardDriveTXT.Name = "HardDriveTXT"
        Me.HardDriveTXT.Size = New System.Drawing.Size(659, 70)
        Me.HardDriveTXT.TabIndex = 1
        Me.HardDriveTXT.Text = ""
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.LightGray
        Me.Panel2.Controls.Add(Me.ReturnedVirtualGbLBL)
        Me.Panel2.Controls.Add(Me.Label7)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.CNVirtualMemoryBTN)
        Me.Panel2.Controls.Add(Me.ETHVirtualMemoryBTN)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.VirtualMemoryTXT)
        Me.Panel2.Location = New System.Drawing.Point(22, 152)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(948, 105)
        Me.Panel2.TabIndex = 5
        '
        'VirtualMemoryTXT
        '
        Me.VirtualMemoryTXT.Location = New System.Drawing.Point(16, 12)
        Me.VirtualMemoryTXT.Name = "VirtualMemoryTXT"
        Me.VirtualMemoryTXT.Size = New System.Drawing.Size(659, 79)
        Me.VirtualMemoryTXT.TabIndex = 0
        Me.VirtualMemoryTXT.Text = ""
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.LightGray
        Me.Panel3.Controls.Add(Me.DefenderBTN)
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.WindowsDefenderTXT)
        Me.Panel3.Location = New System.Drawing.Point(22, 276)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(948, 95)
        Me.Panel3.TabIndex = 6
        '
        'WindowsDefenderTXT
        '
        Me.WindowsDefenderTXT.Location = New System.Drawing.Point(14, 12)
        Me.WindowsDefenderTXT.Name = "WindowsDefenderTXT"
        Me.WindowsDefenderTXT.Size = New System.Drawing.Size(659, 69)
        Me.WindowsDefenderTXT.TabIndex = 1
        Me.WindowsDefenderTXT.Text = ""
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.LightGray
        Me.Panel4.Controls.Add(Me.WinUpdateLBL)
        Me.Panel4.Controls.Add(Me.WindowsUpdateBTN)
        Me.Panel4.Controls.Add(Me.Label8)
        Me.Panel4.Controls.Add(Me.WindowsUpdateTXT)
        Me.Panel4.Location = New System.Drawing.Point(22, 390)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(948, 105)
        Me.Panel4.TabIndex = 7
        '
        'WindowsUpdateTXT
        '
        Me.WindowsUpdateTXT.Location = New System.Drawing.Point(14, 12)
        Me.WindowsUpdateTXT.Name = "WindowsUpdateTXT"
        Me.WindowsUpdateTXT.Size = New System.Drawing.Size(659, 78)
        Me.WindowsUpdateTXT.TabIndex = 2
        Me.WindowsUpdateTXT.Text = ""
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(692, 12)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(157, 13)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Allocated Page(Virtual) Memory:"
        '
        'ETHVirtualMemoryBTN
        '
        Me.ETHVirtualMemoryBTN.Location = New System.Drawing.Point(700, 64)
        Me.ETHVirtualMemoryBTN.Name = "ETHVirtualMemoryBTN"
        Me.ETHVirtualMemoryBTN.Size = New System.Drawing.Size(82, 27)
        Me.ETHVirtualMemoryBTN.TabIndex = 4
        Me.ETHVirtualMemoryBTN.Text = "Button1"
        Me.ETHVirtualMemoryBTN.UseVisualStyleBackColor = True
        '
        'CNVirtualMemoryBTN
        '
        Me.CNVirtualMemoryBTN.Location = New System.Drawing.Point(840, 64)
        Me.CNVirtualMemoryBTN.Name = "CNVirtualMemoryBTN"
        Me.CNVirtualMemoryBTN.Size = New System.Drawing.Size(82, 27)
        Me.CNVirtualMemoryBTN.TabIndex = 5
        Me.CNVirtualMemoryBTN.Text = "Button1"
        Me.CNVirtualMemoryBTN.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(718, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(40, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Ethash"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(853, 39)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(60, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Cryptonight"
        '
        'ReturnedVirtualGbLBL
        '
        Me.ReturnedVirtualGbLBL.AutoSize = True
        Me.ReturnedVirtualGbLBL.Location = New System.Drawing.Point(867, 12)
        Me.ReturnedVirtualGbLBL.Name = "ReturnedVirtualGbLBL"
        Me.ReturnedVirtualGbLBL.Size = New System.Drawing.Size(31, 13)
        Me.ReturnedVirtualGbLBL.TabIndex = 8
        Me.ReturnedVirtualGbLBL.Text = "0 GB"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(692, 15)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(236, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "AIOMiner folders excluded in Windows Defender"
        '
        'DefenderBTN
        '
        Me.DefenderBTN.Location = New System.Drawing.Point(767, 40)
        Me.DefenderBTN.Name = "DefenderBTN"
        Me.DefenderBTN.Size = New System.Drawing.Size(82, 27)
        Me.DefenderBTN.TabIndex = 5
        Me.DefenderBTN.Text = "Button1"
        Me.DefenderBTN.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(697, 12)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(131, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Windows Update Service:"
        '
        'WindowsUpdateBTN
        '
        Me.WindowsUpdateBTN.Location = New System.Drawing.Point(767, 52)
        Me.WindowsUpdateBTN.Name = "WindowsUpdateBTN"
        Me.WindowsUpdateBTN.Size = New System.Drawing.Size(82, 27)
        Me.WindowsUpdateBTN.TabIndex = 6
        Me.WindowsUpdateBTN.Text = "Button1"
        Me.WindowsUpdateBTN.UseVisualStyleBackColor = True
        '
        'WinUpdateLBL
        '
        Me.WinUpdateLBL.AutoSize = True
        Me.WinUpdateLBL.Location = New System.Drawing.Point(852, 12)
        Me.WinUpdateLBL.Name = "WinUpdateLBL"
        Me.WinUpdateLBL.Size = New System.Drawing.Size(46, 13)
        Me.WinUpdateLBL.TabIndex = 7
        Me.WinUpdateLBL.Text = "Enabled"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(377, 523)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 50)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "Refresh "
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(526, 523)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(94, 50)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "Done"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'MinerSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(994, 596)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.WindowsUpdateLBL)
        Me.Controls.Add(Me.WindowsDefenderLBL)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MinerSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Systems Check"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents WindowsDefenderLBL As Label
    Friend WithEvents WindowsUpdateLBL As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents VirtualMemoryTXT As RichTextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents HardDriveTXT As RichTextBox
    Friend WithEvents WindowsDefenderTXT As RichTextBox
    Friend WithEvents FreeDiskSpaceBTN As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents WindowsUpdateTXT As RichTextBox
    Friend WithEvents ETHVirtualMemoryBTN As Button
    Friend WithEvents Label5 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents CNVirtualMemoryBTN As Button
    Friend WithEvents ReturnedVirtualGbLBL As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents DefenderBTN As Button
    Friend WithEvents WinUpdateLBL As Label
    Friend WithEvents WindowsUpdateBTN As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents Button2 As Button
    Friend WithEvents Button3 As Button
End Class
