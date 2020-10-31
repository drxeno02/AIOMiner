Imports System.ComponentModel
Imports System.IO
Imports System.Net
Imports System.Windows.Forms
Imports AIOminer.General_Utils
Imports AIOminer.Log


Public Class Advertising
    Private Counter As Integer = 5000

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        PubShared.AdvertiseShowing = False
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        PubShared.AdvertiseShowing = False
        Me.Close()

    End Sub

    Private Sub Advertising_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            Dim MyWebClient As New System.Net.WebClient
            Dim ImageInBytes() As Byte = MyWebClient.DownloadData(PubShared.HOSTED_DATA_STORE & "/advertiseaio.png")
            Dim ImageStream As New IO.MemoryStream(ImageInBytes)
            PictureBox1.Image = New System.Drawing.Bitmap(ImageStream)
            Timer1.Interval = 5000
            Timer1.Start()
        Catch ex As Exception
            PubShared.AdvertiseShowing = False
            Me.Close()

        End Try

        Try
            Dim MyWebClient As New System.Net.WebClient
            Dim ImageInBytes() As Byte = MyWebClient.DownloadData(PubShared.HOSTED_WEBSITE & "/img/logo/dot.png")
            Dim ImageStream As New IO.MemoryStream(ImageInBytes)
            PictureBox2.Image = New System.Drawing.Bitmap(ImageStream)
        Catch ex As Exception
            PubShared.AdvertiseShowing = False
        End Try

        'AS U SEE, NO FILE NEEDS TO BE WRITTEN TO THE HARD DRIVE, ITS ALL DONE IN MEMORY
    End Sub

    Private Sub Advertising_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        PubShared.aioLoading = False
        PubShared.AdvertiseShowing = False
        Timer1.Stop()
        Me.Close()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        'Pull down where you should go
        Dim Results As String

        Try
            SetAllowUnsafeHeaderParsing20()
            Dim address As String = PubShared.HOSTED_WEBSITE & "/products/adclick.html"
            Dim client As WebClient = New WebClient()
            Dim reader As StreamReader = New StreamReader(client.OpenRead(address))
            Results = reader.ReadToEnd
        Catch ex As Exception
            LogUpdate("Advertisement Click failed to find the location!", eLogLevel.Err)
            Results = "err"
            PubShared.AdvertiseShowing = False
        End Try
        'take user to where they should go
        Try
            If Results = "err" Then
            Else
                Process.Start(Results)
            End If

        Catch ex As Exception
            PubShared.AdvertiseShowing = False
        End Try
    End Sub
End Class
