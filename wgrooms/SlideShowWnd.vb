Imports System
Imports System.IO
Imports System.Threading

Public Class SlideShowWnd

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            Const CS_NOCLOSE As Integer = &H200
            cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE
            Return cp
        End Get
    End Property

    Shared Property lblDeluxWaiting As Object

    Private Sub SlideShowWnd_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.FormClosing

    End Sub

    Private Sub SlideShowWnd_Load_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub lblFullTitle_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub lblFullWaitingNumbers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub PictureBox1_Click(sender As System.Object, e As System.EventArgs)

    End Sub

    Private Sub Label1_Click(sender As System.Object, e As System.EventArgs) Handles lblFullWindow.Click
        If Me.WindowState = FormWindowState.Maximized Then
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    Public Sub ResizeLableBox()
        Dim p As Point
        p = lblFullWindow.Size
        p.Y = Me.Size.Height
        p.X = Me.Size.Width
        lblFullWindow.Size = p
    End Sub
End Class