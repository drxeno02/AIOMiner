<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Restart
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
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Restart))
		Me.Label1 = New System.Windows.Forms.Label()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.CheckBox1 = New System.Windows.Forms.CheckBox()
		Me.Button1 = New System.Windows.Forms.Button()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.CheckBox2 = New System.Windows.Forms.CheckBox()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.CheckBox3 = New System.Windows.Forms.CheckBox()
		Me.ComboBox1 = New System.Windows.Forms.ComboBox()
		Me.Label4 = New System.Windows.Forms.Label()
		Me.CheckBox4 = New System.Windows.Forms.CheckBox()
		Me.ComboBox2 = New System.Windows.Forms.ComboBox()
		Me.SuspendLayout()
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(44, 9)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(108, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Restart machine after"
		'
		'Label2
		'
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(194, 9)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(73, 13)
		Me.Label2.TabIndex = 1
		Me.Label2.Text = "mining failures"
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(158, 6)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(30, 20)
		Me.TextBox1.TabIndex = 2
		Me.TextBox1.Text = "5"
		'
		'CheckBox1
		'
		Me.CheckBox1.AutoSize = True
		Me.CheckBox1.Location = New System.Drawing.Point(11, 43)
		Me.CheckBox1.Name = "CheckBox1"
		Me.CheckBox1.Size = New System.Drawing.Size(172, 17)
		Me.CheckBox1.TabIndex = 3
		Me.CheckBox1.Text = "Never Reboot on mining failure"
		Me.CheckBox1.UseVisualStyleBackColor = True
		'
		'Button1
		'
		Me.Button1.Location = New System.Drawing.Point(181, 227)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(85, 31)
		Me.Button1.TabIndex = 4
		Me.Button1.Text = "Exit"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'Button2
		'
		Me.Button2.Location = New System.Drawing.Point(47, 227)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(85, 31)
		Me.Button2.TabIndex = 5
		Me.Button2.Text = "Save"
		Me.Button2.UseVisualStyleBackColor = True
		'
		'CheckBox2
		'
		Me.CheckBox2.AutoSize = True
		Me.CheckBox2.Location = New System.Drawing.Point(11, 76)
		Me.CheckBox2.Name = "CheckBox2"
		Me.CheckBox2.Size = New System.Drawing.Size(309, 17)
		Me.CheckBox2.TabIndex = 6
		Me.CheckBox2.Text = "Check Internet connection  on a mining failure before reboot"
		Me.CheckBox2.UseVisualStyleBackColor = True
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Location = New System.Drawing.Point(55, 96)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(128, 13)
		Me.Label3.TabIndex = 7
		Me.Label3.Text = "(We will ping google.com)"
		'
		'CheckBox3
		'
		Me.CheckBox3.AutoSize = True
		Me.CheckBox3.Location = New System.Drawing.Point(11, 141)
		Me.CheckBox3.Name = "CheckBox3"
		Me.CheckBox3.Size = New System.Drawing.Size(122, 17)
		Me.CheckBox3.TabIndex = 8
		Me.CheckBox3.Text = "Restart mining every"
		Me.CheckBox3.UseVisualStyleBackColor = True
		'
		'ComboBox1
		'
		Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.ComboBox1.FormattingEnabled = True
		Me.ComboBox1.Location = New System.Drawing.Point(139, 141)
		Me.ComboBox1.Name = "ComboBox1"
		Me.ComboBox1.Size = New System.Drawing.Size(181, 21)
		Me.ComboBox1.TabIndex = 9
		'
		'Label4
		'
		Me.Label4.AutoSize = True
		Me.Label4.Location = New System.Drawing.Point(207, 145)
		Me.Label4.Name = "Label4"
		Me.Label4.Size = New System.Drawing.Size(0, 13)
		Me.Label4.TabIndex = 10
		'
		'CheckBox4
		'
		Me.CheckBox4.AutoSize = True
		Me.CheckBox4.Location = New System.Drawing.Point(11, 186)
		Me.CheckBox4.Name = "CheckBox4"
		Me.CheckBox4.Size = New System.Drawing.Size(132, 17)
		Me.CheckBox4.TabIndex = 11
		Me.CheckBox4.Text = "Restart machine every"
		Me.CheckBox4.UseVisualStyleBackColor = True
		'
		'ComboBox2
		'
		Me.ComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
		Me.ComboBox2.FormattingEnabled = True
		Me.ComboBox2.Location = New System.Drawing.Point(142, 184)
		Me.ComboBox2.Name = "ComboBox2"
		Me.ComboBox2.Size = New System.Drawing.Size(178, 21)
		Me.ComboBox2.TabIndex = 12
		'
		'Restart
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(332, 268)
		Me.Controls.Add(Me.ComboBox2)
		Me.Controls.Add(Me.CheckBox4)
		Me.Controls.Add(Me.Label4)
		Me.Controls.Add(Me.ComboBox1)
		Me.Controls.Add(Me.CheckBox3)
		Me.Controls.Add(Me.Label3)
		Me.Controls.Add(Me.CheckBox2)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.Button1)
		Me.Controls.Add(Me.CheckBox1)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.Label1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "Restart"
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		Me.Text = "AIOMiner - Restart Settings"
		Me.TopMost = True
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents CheckBox1 As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents Label3 As Label
	Friend WithEvents CheckBox3 As CheckBox
	Friend WithEvents ComboBox1 As ComboBox
	Friend WithEvents Label4 As Label
	Friend WithEvents CheckBox4 As CheckBox
	Friend WithEvents ComboBox2 As ComboBox
End Class
