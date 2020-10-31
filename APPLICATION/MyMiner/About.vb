Imports AIOminer.JSON_Utils
Imports AIOminer.General_Utils

Imports System.Drawing.Drawing2D


Public Class About

    ' The key press order used in the code is stored in this array of objects.
    Dim keyorder() As Object = {System.Windows.Forms.Keys.Up, System.Windows.Forms.Keys.Up,
                                    System.Windows.Forms.Keys.Down, System.Windows.Forms.Keys.Down,
                                    System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.Right,
                                    System.Windows.Forms.Keys.Left, System.Windows.Forms.Keys.Right,
                                    System.Windows.Forms.Keys.B, System.Windows.Forms.Keys.A}

    ' Index variable to keep track of key presses.
    Dim index As Integer = 0

    ' This array of booleans will verify the sequence is going well.
    Dim sequence() As Boolean = {False, False, False, False, False, False, False, False, False, False}

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub About_Load(sender As Object, e As EventArgs) Handles MyBase.Load


        VERSION.Text = PubShared.Version
        miners.Text = ReturnAIOsetting("minerversion")
        RichTextBox1.Text =
           "AIOMiner is brought to you by users like yourself!  If you can help code, please consider helping!
All of the software that you are downloading ""CCMINER"", ""Phoenix"", etc, etc are brought to you by those developers, any fees they may have implied have not been tampered with. If you are one of those developers and wish that your software is no longer use it, please visit our github repo and open up an issue"

        RichTextBox1.Text = RichTextBox1.Text & "Konami"
        ' This call is required by the designer.
        ' InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ' This allows for key presses to be recognized while the form is in focus.
        Me.KeyPreview = True

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub RichTextBox1_TextChanged(sender As Object, e As EventArgs) Handles RichTextBox1.TextChanged

    End Sub

    Private Sub About_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint

    End Sub



    Private Sub About_KeyUp(sender As Object, e As KeyEventArgs) Handles Me.KeyUp
        ' The key press loop changes the sequence array elements to true if the key pressed matches the keyorder() array. 
        If index < 9 And sequence(index) = False And e.KeyCode = keyorder(index) Then
            sequence(index) = True
            index += 1

        ElseIf index = 9 And e.KeyCode = keyorder(index) Then
            ' On the last key press unlock a secret.
            MsgBox("Congrats! Send a screen shot of this to the General Discord channel, if you were the first to do this, I will mine for you for one week!")

        Else
            ' If the keys are not pressed in the correct order, reset the sequence.
            index = 0
            For i As Integer = 0 To sequence.Length - 1
                sequence(i) = False

            Next

        End If
    End Sub

End Class