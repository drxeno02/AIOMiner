<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MyEmail
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MyEmail))
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtRigname = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtApikey = New System.Windows.Forms.TextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.lblSTATUS = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(502, 294)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(72, 23)
        Me.Button2.TabIndex = 9
        Me.Button2.Text = "Exit"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(419, 294)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(62, 23)
        Me.Button4.TabIndex = 18
        Me.Button4.Text = "Save"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 255)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Rigname:"
        Me.ToolTip1.SetToolTip(Me.Label5, "This code is the one you got from your e-mail. it's tired to your e-mail address")
        '
        'txtRigname
        '
        Me.txtRigname.Location = New System.Drawing.Point(67, 252)
        Me.txtRigname.Name = "txtRigname"
        Me.txtRigname.Size = New System.Drawing.Size(511, 20)
        Me.txtRigname.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 222)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(48, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "API Key:"
        Me.ToolTip1.SetToolTip(Me.Label4, "This code is the one you got from your e-mail. it's tired to your e-mail address")
        '
        'txtApikey
        '
        Me.txtApikey.Location = New System.Drawing.Point(67, 219)
        Me.txtApikey.Name = "txtApikey"
        Me.txtApikey.Size = New System.Drawing.Size(511, 20)
        Me.txtApikey.TabIndex = 15
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(9, 299)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(28, 13)
        Me.LinkLabel1.TabIndex = 12
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "FAQ"
        '
        'lblSTATUS
        '
        Me.lblSTATUS.AutoSize = True
        Me.lblSTATUS.Location = New System.Drawing.Point(91, 166)
        Me.lblSTATUS.Name = "lblSTATUS"
        Me.lblSTATUS.Size = New System.Drawing.Size(53, 13)
        Me.lblSTATUS.TabIndex = 10
        Me.lblSTATUS.Text = "|||||||||||||||||||||||"
        Me.ToolTip1.SetToolTip(Me.lblSTATUS, "Online = Good, Offline = Bad")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 166)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "API STATUS:"
        Me.ToolTip1.SetToolTip(Me.Label3, "The status of AIOMiner API System")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(409, 166)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "MY STATUS:"
        Me.ToolTip1.SetToolTip(Me.Label1, "The status of AIOMiner API System")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(487, 166)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 13)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "|||||||||||||||||||||||"
        Me.ToolTip1.SetToolTip(Me.Label2, "Online = Good, Offline = Bad")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(180, 166)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(108, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "ACCOUNT STATUS:"
        Me.ToolTip1.SetToolTip(Me.Label6, "The status of AIOMiner API System")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(294, 166)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(53, 13)
        Me.Label7.TabIndex = 23
        Me.Label7.Text = "|||||||||||||||||||||||"
        Me.ToolTip1.SetToolTip(Me.Label7, "Online = Good, Offline = Bad")
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(12, 12)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.ReadOnly = True
        Me.RichTextBox1.Size = New System.Drawing.Size(562, 151)
        Me.RichTextBox1.TabIndex = 14
        Me.RichTextBox1.Text = ""
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(335, 294)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(62, 23)
        Me.Button1.TabIndex = 19
        Me.Button1.Text = " ENABLED | DISABLED"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'MyEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(586, 332)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblSTATUS)
        Me.Controls.Add(Me.txtRigname)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.txtApikey)
        Me.Controls.Add(Me.Label4)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "MyEmail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "AIOMiner - Online Settings"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As Button
	Friend WithEvents ToolTip1 As ToolTip
	Friend WithEvents LinkLabel1 As LinkLabel
	Friend WithEvents lblSTATUS As Label
	Friend WithEvents Label3 As Label
	Friend WithEvents Label5 As Label
	Friend WithEvents txtRigname As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtApikey As TextBox
	Friend WithEvents Button4 As Button
	Friend WithEvents RichTextBox1 As RichTextBox
	Friend WithEvents Button1 As Button
	Friend WithEvents Label1 As Label
	Friend WithEvents Label2 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
