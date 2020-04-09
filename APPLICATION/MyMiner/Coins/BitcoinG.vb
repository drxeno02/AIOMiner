Imports System.Threading
Public Class ZCash
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim frm As New AddPool With {.MessageFromMainForm = "ZCash"}
        Try
            frm.ShowDialog()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ComboBox1.Text = "" Then
            MsgBox("I can't seem to find anything to delete here...")
            Exit Sub
        End If

        If ComboBox1.Text = My.Settings.ZEC1 Then
            ComboBox1.Items.Remove(My.Settings.VCP1)
            My.Settings.ZEC1 = ""
            My.Settings.Save()
            ComboBox1.Refresh()
            Exit Sub

        End If

        If ComboBox1.Text = My.Settings.ZEC2 Then
            ComboBox1.Items.Remove(My.Settings.ZEC2)
            My.Settings.ZEC2 = ""
            My.Settings.Save()
            ComboBox1.Refresh()
            Exit Sub

        End If

        If ComboBox1.Text = My.Settings.ZEC3 Then
            ComboBox1.Items.Remove(My.Settings.ZEC3)
            My.Settings.ZEC3 = ""
            My.Settings.Save()
            ComboBox1.Refresh()
            Exit Sub

        End If

        Try
            ComboBox1.Text = " "
            ComboBox1.SelectedIndex = 0
            MsgBox("Deleted!")
        Catch ex As Exception
            MsgBox("Nothing to Delete!")
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ComboBox1.Items.Clear()

        If My.Settings.ZEC1 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC1)
            ComboBox1.SelectedIndex = 0
        End If

        If My.Settings.ZEC2 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC2)
        End If

        If My.Settings.ZEC3 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC3)
        End If
    End Sub

    Private Sub ZCash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If My.Settings.ZEC1 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC1)
            ComboBox1.SelectedIndex = 0
        End If

        If My.Settings.ZEC2 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC2)
        End If

        If My.Settings.ZEC3 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC3)
        End If
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim frm As New TestPool With {.MessageFromMainForm = "ZCash"}
        Try
            frm.ShowDialog()
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.Close()
        MyMiner.Show()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Update to make whatever is selected to be ZEC1
        If ComboBox1.Text = "" Then
            MsgBox("I don't see a pool selected?")
            Exit Sub
        Else
            Try
                My.Settings.ZEC2 = My.Settings.ZEC1
            Catch ex As Exception

            End Try

            My.Settings.ZEC1 = ComboBox1.Text
            My.Settings.Save()
        End If

        'Refresh the list
        ComboBox1.Items.Clear()

        If My.Settings.ZEC1 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC1)
            ComboBox1.SelectedIndex = 0
        End If

        If My.Settings.ZEC2 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC2)
        End If

        If My.Settings.ZEC3 = "" Then
        Else
            ComboBox1.Items.Add(My.Settings.ZEC3)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)


    End Sub
End Class