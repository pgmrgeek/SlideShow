<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SlideShowWnd
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
        Me.lblFullWindow = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblFullWindow
        '
        Me.lblFullWindow.BackColor = System.Drawing.Color.Transparent
        Me.lblFullWindow.Location = New System.Drawing.Point(12, 9)
        Me.lblFullWindow.Name = "lblFullWindow"
        Me.lblFullWindow.Size = New System.Drawing.Size(682, 459)
        Me.lblFullWindow.TabIndex = 0
        '
        'SlideShowWnd
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = Global.SlideShow.My.MySettings.Default.ssWindowSize
        Me.Controls.Add(Me.lblFullWindow)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.SlideShow.My.MySettings.Default, "sswLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DataBindings.Add(New System.Windows.Forms.Binding("ClientSize", Global.SlideShow.My.MySettings.Default, "ssWindowSize", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.DoubleBuffered = True
        Me.ForeColor = System.Drawing.Color.Transparent
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = Global.SlideShow.My.MySettings.Default.sswLocation
        Me.Name = "SlideShowWnd"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblFullWindow As System.Windows.Forms.Label
End Class
