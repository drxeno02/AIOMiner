<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class PoolAdvanced
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(PoolAdvanced))
        Me.txtCurSettings = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNewSettings = New System.Windows.Forms.TextBox()
        Me.btnCopyFromCurrent = New System.Windows.Forms.Button()
        Me.btnRestoreDefaults = New System.Windows.Forms.Button()
        Me.btnContinue = New System.Windows.Forms.Button()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtCurSettings
        '
        Me.txtCurSettings.Enabled = False
        Me.txtCurSettings.Location = New System.Drawing.Point(15, 25)
        Me.txtCurSettings.Name = "txtCurSettings"
        Me.txtCurSettings.Size = New System.Drawing.Size(596, 20)
        Me.txtCurSettings.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Current Miner Settings"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "New Miner Settings"
        '
        'txtNewSettings
        '
        Me.txtNewSettings.Location = New System.Drawing.Point(15, 77)
        Me.txtNewSettings.Name = "txtNewSettings"
        Me.txtNewSettings.Size = New System.Drawing.Size(596, 20)
        Me.txtNewSettings.TabIndex = 2
        '
        'btnCopyFromCurrent
        '
        Me.btnCopyFromCurrent.Location = New System.Drawing.Point(15, 117)
        Me.btnCopyFromCurrent.Name = "btnCopyFromCurrent"
        Me.btnCopyFromCurrent.Size = New System.Drawing.Size(106, 31)
        Me.btnCopyFromCurrent.TabIndex = 4
        Me.btnCopyFromCurrent.Text = "Copy From Current"
        Me.btnCopyFromCurrent.UseVisualStyleBackColor = True
        '
        'btnRestoreDefaults
        '
        Me.btnRestoreDefaults.Location = New System.Drawing.Point(127, 117)
        Me.btnRestoreDefaults.Name = "btnRestoreDefaults"
        Me.btnRestoreDefaults.Size = New System.Drawing.Size(106, 31)
        Me.btnRestoreDefaults.TabIndex = 5
        Me.btnRestoreDefaults.Text = "Restore Defaults"
        Me.btnRestoreDefaults.UseVisualStyleBackColor = True
        '
        'btnContinue
        '
        Me.btnContinue.Location = New System.Drawing.Point(393, 117)
        Me.btnContinue.Name = "btnContinue"
        Me.btnContinue.Size = New System.Drawing.Size(106, 31)
        Me.btnContinue.TabIndex = 6
        Me.btnContinue.Text = "Continue"
        Me.btnContinue.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(505, 117)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(106, 31)
        Me.btnExit.TabIndex = 7
        Me.btnExit.Text = "Cancel"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'PoolAdvanced
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(629, 157)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnContinue)
        Me.Controls.Add(Me.btnRestoreDefaults)
        Me.Controls.Add(Me.btnCopyFromCurrent)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNewSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCurSettings)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "PoolAdvanced"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Advanced Pool Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtCurSettings As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtNewSettings As TextBox
    Friend WithEvents btnCopyFromCurrent As Button
    Friend WithEvents btnRestoreDefaults As Button
    Friend WithEvents btnContinue As Button
    Friend WithEvents btnExit As Button
End Class
