<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.SlidesGroup = New System.Windows.Forms.GroupBox()
        Me.lblFileCount = New System.Windows.Forms.Label()
        Me.btnFolderDialog = New System.Windows.Forms.Button()
        Me.tbFilePath = New System.Windows.Forms.TextBox()
        Me.lblFilePath = New System.Windows.Forms.Label()
        Me.btnExit = New System.Windows.Forms.Button()
        Me.chkSmoothFade = New System.Windows.Forms.CheckBox()
        Me.btnSecondUp = New System.Windows.Forms.Button()
        Me.btnSecondDown = New System.Windows.Forms.Button()
        Me.lblSecondsPer = New System.Windows.Forms.Label()
        Me.txtSecondsPer = New System.Windows.Forms.TextBox()
        Me.chkFade = New System.Windows.Forms.CheckBox()
        Me.btnMaximize = New System.Windows.Forms.Button()
        Me.btnResetPos = New System.Windows.Forms.Button()
        Me.PauseButton = New System.Windows.Forms.Button()
        Me.ssBoarders = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.ViewSlideShowPB = New System.Windows.Forms.PictureBox()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.SlidesGroup.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.ViewSlideShowPB, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SlidesGroup
        '
        Me.SlidesGroup.Controls.Add(Me.lblFileCount)
        Me.SlidesGroup.Controls.Add(Me.btnFolderDialog)
        Me.SlidesGroup.Controls.Add(Me.tbFilePath)
        Me.SlidesGroup.Controls.Add(Me.lblFilePath)
        Me.SlidesGroup.Controls.Add(Me.btnExit)
        Me.SlidesGroup.Controls.Add(Me.chkSmoothFade)
        Me.SlidesGroup.Controls.Add(Me.btnSecondUp)
        Me.SlidesGroup.Controls.Add(Me.btnSecondDown)
        Me.SlidesGroup.Controls.Add(Me.lblSecondsPer)
        Me.SlidesGroup.Controls.Add(Me.txtSecondsPer)
        Me.SlidesGroup.Controls.Add(Me.chkFade)
        Me.SlidesGroup.Controls.Add(Me.btnMaximize)
        Me.SlidesGroup.Controls.Add(Me.btnResetPos)
        Me.SlidesGroup.Controls.Add(Me.PauseButton)
        Me.SlidesGroup.Controls.Add(Me.ssBoarders)
        Me.SlidesGroup.Controls.Add(Me.GroupBox3)
        Me.SlidesGroup.Location = New System.Drawing.Point(12, 12)
        Me.SlidesGroup.Name = "SlidesGroup"
        Me.SlidesGroup.Size = New System.Drawing.Size(440, 324)
        Me.SlidesGroup.TabIndex = 4
        Me.SlidesGroup.TabStop = False
        Me.SlidesGroup.Text = "Slide Show View"
        '
        'lblFileCount
        '
        Me.lblFileCount.Location = New System.Drawing.Point(101, 266)
        Me.lblFileCount.Name = "lblFileCount"
        Me.lblFileCount.Size = New System.Drawing.Size(182, 17)
        Me.lblFileCount.TabIndex = 53
        Me.lblFileCount.Text = "X  .JPGs in the folder"
        '
        'btnFolderDialog
        '
        Me.btnFolderDialog.Location = New System.Drawing.Point(369, 286)
        Me.btnFolderDialog.Name = "btnFolderDialog"
        Me.btnFolderDialog.Size = New System.Drawing.Size(26, 20)
        Me.btnFolderDialog.TabIndex = 52
        Me.btnFolderDialog.Text = "..."
        Me.btnFolderDialog.UseVisualStyleBackColor = True
        '
        'tbFilePath
        '
        Me.tbFilePath.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SlideShow.My.MySettings.Default, "asFilePath", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.tbFilePath.Location = New System.Drawing.Point(26, 286)
        Me.tbFilePath.Name = "tbFilePath"
        Me.tbFilePath.Size = New System.Drawing.Size(337, 20)
        Me.tbFilePath.TabIndex = 51
        Me.tbFilePath.Text = Global.SlideShow.My.MySettings.Default.asFilePath
        '
        'lblFilePath
        '
        Me.lblFilePath.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFilePath.Location = New System.Drawing.Point(15, 266)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(111, 17)
        Me.lblFilePath.TabIndex = 50
        Me.lblFilePath.Text = "Watching folder:"
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(303, 196)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(92, 23)
        Me.btnExit.TabIndex = 49
        Me.btnExit.Text = "Exit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'chkSmoothFade
        '
        Me.chkSmoothFade.AutoSize = True
        Me.chkSmoothFade.Checked = Global.SlideShow.My.MySettings.Default.SmoothCheck
        Me.chkSmoothFade.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSmoothFade.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SlideShow.My.MySettings.Default, "SmoothCheck", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkSmoothFade.Location = New System.Drawing.Point(322, 112)
        Me.chkSmoothFade.Name = "chkSmoothFade"
        Me.chkSmoothFade.Size = New System.Drawing.Size(89, 17)
        Me.chkSmoothFade.TabIndex = 40
        Me.chkSmoothFade.Text = "Smooth Fade"
        Me.chkSmoothFade.UseVisualStyleBackColor = True
        '
        'btnSecondUp
        '
        Me.btnSecondUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSecondUp.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSecondUp.Location = New System.Drawing.Point(305, 30)
        Me.btnSecondUp.Name = "btnSecondUp"
        Me.btnSecondUp.Size = New System.Drawing.Size(22, 20)
        Me.btnSecondUp.TabIndex = 39
        Me.btnSecondUp.Text = "+"
        Me.btnSecondUp.UseVisualStyleBackColor = True
        '
        'btnSecondDown
        '
        Me.btnSecondDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSecondDown.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSecondDown.Location = New System.Drawing.Point(334, 30)
        Me.btnSecondDown.Name = "btnSecondDown"
        Me.btnSecondDown.Size = New System.Drawing.Size(22, 20)
        Me.btnSecondDown.TabIndex = 38
        Me.btnSecondDown.Text = "-"
        Me.btnSecondDown.UseVisualStyleBackColor = True
        '
        'lblSecondsPer
        '
        Me.lblSecondsPer.AutoSize = True
        Me.lblSecondsPer.Location = New System.Drawing.Point(336, 61)
        Me.lblSecondsPer.Name = "lblSecondsPer"
        Me.lblSecondsPer.Size = New System.Drawing.Size(93, 13)
        Me.lblSecondsPer.TabIndex = 37
        Me.lblSecondsPer.Text = "Seconds per Slide"
        '
        'txtSecondsPer
        '
        Me.txtSecondsPer.DataBindings.Add(New System.Windows.Forms.Binding("Text", Global.SlideShow.My.MySettings.Default, "SecondsPerSlide", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.txtSecondsPer.Location = New System.Drawing.Point(305, 58)
        Me.txtSecondsPer.Name = "txtSecondsPer"
        Me.txtSecondsPer.Size = New System.Drawing.Size(25, 20)
        Me.txtSecondsPer.TabIndex = 36
        Me.txtSecondsPer.Text = Global.SlideShow.My.MySettings.Default.SecondsPerSlide
        Me.txtSecondsPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'chkFade
        '
        Me.chkFade.AutoSize = True
        Me.chkFade.Checked = Global.SlideShow.My.MySettings.Default.FadeCheck
        Me.chkFade.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkFade.DataBindings.Add(New System.Windows.Forms.Binding("Checked", Global.SlideShow.My.MySettings.Default, "FadeCheck", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.chkFade.Location = New System.Drawing.Point(305, 89)
        Me.chkFade.Name = "chkFade"
        Me.chkFade.Size = New System.Drawing.Size(90, 17)
        Me.chkFade.TabIndex = 35
        Me.chkFade.Text = " Fade Images"
        Me.chkFade.UseVisualStyleBackColor = True
        '
        'btnMaximize
        '
        Me.btnMaximize.Location = New System.Drawing.Point(108, 228)
        Me.btnMaximize.Name = "btnMaximize"
        Me.btnMaximize.Size = New System.Drawing.Size(87, 23)
        Me.btnMaximize.TabIndex = 34
        Me.btnMaximize.Text = "Min/Max Wnd"
        Me.btnMaximize.UseVisualStyleBackColor = True
        '
        'btnResetPos
        '
        Me.btnResetPos.Location = New System.Drawing.Point(18, 228)
        Me.btnResetPos.Name = "btnResetPos"
        Me.btnResetPos.Size = New System.Drawing.Size(84, 23)
        Me.btnResetPos.TabIndex = 33
        Me.btnResetPos.Text = "Reset Pos"
        Me.btnResetPos.UseVisualStyleBackColor = True
        '
        'PauseButton
        '
        Me.PauseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PauseButton.Location = New System.Drawing.Point(303, 141)
        Me.PauseButton.Name = "PauseButton"
        Me.PauseButton.Size = New System.Drawing.Size(92, 49)
        Me.PauseButton.TabIndex = 25
        Me.PauseButton.Text = "Click to Run"
        Me.PauseButton.UseVisualStyleBackColor = True
        '
        'ssBoarders
        '
        Me.ssBoarders.Location = New System.Drawing.Point(201, 228)
        Me.ssBoarders.Name = "ssBoarders"
        Me.ssBoarders.Size = New System.Drawing.Size(96, 23)
        Me.ssBoarders.TabIndex = 21
        Me.ssBoarders.Text = "Disable Boarders"
        Me.ssBoarders.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.ViewSlideShowPB)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 24)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(279, 198)
        Me.GroupBox3.TabIndex = 13
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "View of Slideshow"
        '
        'ViewSlideShowPB
        '
        Me.ViewSlideShowPB.Location = New System.Drawing.Point(26, 19)
        Me.ViewSlideShowPB.Name = "ViewSlideShowPB"
        Me.ViewSlideShowPB.Size = New System.Drawing.Size(228, 152)
        Me.ViewSlideShowPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ViewSlideShowPB.TabIndex = 13
        Me.ViewSlideShowPB.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(466, 348)
        Me.Controls.Add(Me.SlidesGroup)
        Me.DataBindings.Add(New System.Windows.Forms.Binding("Location", Global.SlideShow.My.MySettings.Default, "fm1PanelLocation", True, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged))
        Me.Location = Global.SlideShow.My.MySettings.Default.fm1PanelLocation
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "Form1"
        Me.Text = "SlideShow 1.0"
        Me.SlidesGroup.ResumeLayout(False)
        Me.SlidesGroup.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.ViewSlideShowPB, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SlidesGroup As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ViewSlideShowPB As System.Windows.Forms.PictureBox
    Friend WithEvents ssBoarders As System.Windows.Forms.Button
    Friend WithEvents PauseButton As System.Windows.Forms.Button
    Friend WithEvents btnResetPos As System.Windows.Forms.Button
    Friend WithEvents btnMaximize As System.Windows.Forms.Button
    Friend WithEvents lblSecondsPer As System.Windows.Forms.Label
    Friend WithEvents txtSecondsPer As System.Windows.Forms.TextBox
    Friend WithEvents chkFade As System.Windows.Forms.CheckBox
    Friend WithEvents btnSecondUp As System.Windows.Forms.Button
    Friend WithEvents btnSecondDown As System.Windows.Forms.Button
    Friend WithEvents chkSmoothFade As System.Windows.Forms.CheckBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents tbFilePath As System.Windows.Forms.TextBox
    Friend WithEvents btnFolderDialog As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblFileCount As System.Windows.Forms.Label

End Class
