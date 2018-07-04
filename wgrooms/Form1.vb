Imports System
Imports System.IO
Imports System.Threading

'============================== Startup ==========================
Public Class Form1

    Protected Overrides ReadOnly Property CreateParams() As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            Const CS_NOCLOSE As Integer = &H200
            cp.ClassStyle = cp.ClassStyle Or CS_NOCLOSE
            Return cp
        End Get
    End Property

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        globals.fForm1 = Me
        'globals.fSlideShow = New SlideShowWnd

        ' give the new title 
        Me.Text = globals.PgmTitle

        ' config the background thread timer
        globals.alarm = New Threading.Timer(AddressOf OurTimerTick, Nothing, 2000, globals.TimerRate)

        globals.cmdLineDebug = False
        For Each argument As String In My.Application.CommandLineArgs

            If argument = "/d" Then
                globals.cmdLineDebug = True
            End If
            If argument = "/r" Then
                Me.WindowState = FormWindowState.Normal
                SlideShowWnd.WindowState = FormWindowState.Normal
                SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
                SlideShowWnd.Location() = New Point(0, 0)
                SlideShowWnd.Size = New Point(640, 480)
            End If
            If argument = "/R" Then
                My.Settings.Reset() ' if the big R, reset all the saved prior settings
            End If
        Next

        ' use the saved path or the default path
        If tbFilePath.Text <> "" Then
            If IO.Directory.Exists(tbFilePath.Text) Then
                globals.fpath = tbFilePath.Text
            Else
                globals.fpath = My.Application.Info.DirectoryPath + "\"
            End If
        Else
            globals.fpath = My.Application.Info.DirectoryPath + "\"
        End If

        tbFilePath.Text = globals.fpath
        ' make sure it ends with a backslash
        If Microsoft.VisualBasic.Right(tbFilePath.Text, 1) <> "\" Then
            tbFilePath.Text = tbFilePath.Text & "\"
        End If

        globals.SlideshowSeconds = txtSecondsPer.Text

        ' prep the slideshow

        FileResetArray()
        FileAddToArray()
        lblFileCount.Text = globals.FileNamesMax.ToString + " .JPGs in the folder"

        'failsafe one image..
        If globals.FileNamesMax = 0 Then
            MsgBox("!!! NEED AT LEAST ONE .JPG To RUN THIS PROGRAM !!!")
            _exitprogram()
        End If

        ' create the fade class, specify the initial visible image (and its size)
        globals.trans = New Transition(globals.fpath + globals.FileNames(0), 10)

        ' calculate the fade speed
        If chkSmoothFade.Checked Then
            globals.TimerRate = 70
        Else
            globals.TimerRate = 140
        End If

        ' watch the slideshow folder for new incoming images
        WatchThisFolder(True)

        ' enable the timer
        globals.trans.FadeSpeed(globals.TimerRate)
        globals.alarm.Change(2000, globals.TimerRate)

        globals.loadcomplete = True
        _SmoothFadeSet()

        _showslideform()
        FileDisplayNext(False)

        SlideShowWnd.Show()        ' DSC this happens AFTEr the first draw..

    End Sub

    Private Sub logchange(ByVal source As Object, ByVal e As System.IO.FileSystemEventArgs)

        '''''''''''''''''''''''''''''''''' file changed - reload the image for a new thumbnail

        If e.ChangeType = IO.WatcherChangeTypes.Changed Then

            ' signal the timer thread new files have arrived
            globals.NewFilesArrived += 1

        End If

        '''''''''''''''''''''''''''''''''' file created - just landed, set the green light..  
        '''''If we want to dynamically add it 
        ' to the list, we'll have to resort the list, too much work right now..

        If e.ChangeType = IO.WatcherChangeTypes.Created Then

            globals.NewFilesArrived += 1

        End If

        '''''''''''''''''''''''''''''''''' File deleted, we need to flush this out of our system.

        If e.ChangeType = IO.WatcherChangeTypes.Deleted Then

        End If

    End Sub

    ' Define the event handlers. 
    Private Shared Sub OnChanged(source As Object, e As FileSystemEventArgs)

        globals.StateUpdated = True
        globals.statetick = 1000 / globals.TimerRate ' this causes the read to wait 1 second

    End Sub

    Private Sub Form1_Close(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing

    End Sub

    '============================== Group Boxes ==========================

    Private Sub ShowButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable Then
            ssBoarders.Text = "Disable Boarders"
        Else
            ssBoarders.Text = "Enable Boarders"
        End If

    End Sub

    Private Sub _showslideform()

        If SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable Then
            ssBoarders.Text = "Disable Boarders"
        Else
            ssBoarders.Text = "Enable Boarders"
        End If

    End Sub

    Private Sub ExitButton_Click(sender As System.Object, e As System.EventArgs)

        _exitprogram()

    End Sub

    Private Sub _exitprogram()

        ' no more slide show
        globals.SlideShowPaused = True

        'Stop the Timer.
        globals.alarm.Change(Timeout.Infinite, Timeout.Infinite)

        ' pause for a bit to allow the timer to be killed
        Thread.Sleep(2000)

        Application.Exit()

    End Sub

    Public Sub BuildFreeRoomList()

    End Sub

    Public Sub UpdateWaitingList()
       
    End Sub

    Public Sub BuildTextRoomList()

    End Sub

    ' =================== read/write state data to a file ======================

    Public Sub DataFileRead()

    End Sub

    Public Sub DataFileWrite()

    End Sub

    '============================== timer tick routine ==========================

    Private Sub OurTimerTick(ByVal state As Object)
        OurSafeTimerTick()
    End Sub

    Private Sub OurSafeTimerTick()
        Static dec As Integer = 10 * 3  ' # of ticks for our visual pause delay 

        ' InvokeRequired required compares the thread ID of the
        ' calling thread to the thread ID of the creating thread.
        ' If these threads are different, it returns true.
        If Me.InvokeRequired Then
            Dim d As New globals.dOurSafeTimerTick(AddressOf OurSafeTimerTick)
            Me.Invoke(d, New Object() {})
        Else

            If Not globals.SlideShowPaused Then

                If globals.NewFilesArrived > 0 Then

                    ' collect all the new jpgs.
                    Do While globals.NewFilesArrived > 0
                        FileAddMoreToArray()
                        globals.NewFilesArrived -= 1
                    Loop

                    ' give the operator the count of jpgs
                    lblFileCount.Text = globals.FileNamesMax.ToString + " .JPGs in the folder"

                End If

                ' Stage 1 - if a fade is in progress, process it
                If globals.trans.IsRunning() Then

                    ' do a fade step
                    globals.trans.Fade()

                    ' if we finish, set the next stage timeout
                    If Not globals.trans.IsRunning() Then
                        dec = globals.SlideshowSeconds * (1000 / globals.TimerRate)
                    End If

                Else
                    ' else we do the stage 2 - wait 3 seconds before loading the next slide
                    If dec > 0 Then

                        dec -= 1
                        If dec = 0 Then

                            FileDisplayNext(True)
                            dec = globals.SlideshowSeconds * (1000 / globals.TimerRate)

                        End If
                    Else
                        dec = globals.SlideshowSeconds * (1000 / globals.TimerRate)
                    End If

                End If

            End If
        End If

    End Sub

    Public Sub WatchFolderChange(ByVal dir As String)
        globals.WatchFolder.EnableRaisingEvents = False
        globals.WatchFolder.Path = dir
        globals.WatchFolder.EnableRaisingEvents = True
    End Sub

    Public Sub WatchThisFolder(ByVal turnon As Boolean)

        ' turn on the watcher of the background print processor folder
        If turnon Then

            ' allocate the system resources
            If globals.WatchFolderSetup = False Then
                globals.WatchFolder = New System.IO.FileSystemWatcher()
            End If

            ' set the path, turn it on..
            globals.WatchFolder.Path = globals.fpath ' "c:\onsite\slides"
            globals.WatchFolder.Filter = "*.jp*"
            globals.WatchFolder.EnableRaisingEvents = True

            'Add a list of Filter we want to specify
            'make sure you use OR for each Filter as we need to
            'all of those 

            globals.WatchFolder.NotifyFilter = IO.NotifyFilters.DirectoryName Or _
                                       IO.NotifyFilters.FileName Or _
                                       IO.NotifyFilters.Attributes

            ' add the handler to each event
            AddHandler globals.WatchFolder.Changed, AddressOf logchange
            AddHandler globals.WatchFolder.Created, AddressOf logchange
            AddHandler globals.WatchFolder.Deleted, AddressOf logchange

            ' add the rename handler as the signature is different
            AddHandler globals.WatchFolder.Renamed, AddressOf logchange
        Else
            ' disable the watching
            globals.WatchFolder.EnableRaisingEvents = False
        End If

    End Sub

    '============================= global generic functions ============================

    '
    '-----=====< FileDisplayNext >=====------
    '
    ' All file names and counts are held in arrays (up to 2048 entries) This routine
    ' zeros out the array of file names and print counts. We'll reload from the folder. 
    ' Image data is managed later for smart release
    '
    Private Sub FileDisplayNext(ByVal run As Boolean)
        Dim fimg As Image = globals.FileCurrentImage
        'Dim FXtype As Integer = 0
        Dim fnam As String
        Dim loopcount As Integer = 0
        Dim idxFound As Integer

        ' pause on false
        If Not run Then

            ' pause the slide show then force a new image on the screen
            globals.SlideShowPaused = True
            tbFilePath.Enabled = True
            PauseButton.Text = "Click to Run"

            ' display the static image, set the current image to null
            ViewSlideShowPB.Image = Image.FromFile(globals.fpath + globals.FileNames(globals.FileNext))
            globals.FileCurrentImage = Image.FromFile(globals.fpath + globals.FileNames(globals.FileNext))

            globals.trans.Force(globals.FileCurrentImage)
            globals.trans.Fade()

        Else

            ' run if true
            tbFilePath.Enabled = False
            globals.SlideShowPaused = False
            PauseButton.Text = "Pause"

            idxFound = -1

            Do While loopcount <= globals.FileNamesMax

                ' load the next image
                fnam = globals.FileNames(globals.FileNext)

                ' move to the next file
                globals.FileNext += 1
                If globals.FileNext = globals.FileNamesMax Then
                    globals.FileNext = 0
                End If

                If File.Exists(globals.fpath + fnam) Then
                    ' if found, then exit the loop
                    idxFound = globals.FileNext
                    Exit Do
                End If

                loopcount += 1

            Loop

            If (idxFound >= 0) Then
                globals.FileCurrentImage = Image.FromFile(globals.fpath + globals.FileNames(idxFound))
            End If

        End If

        ' the new fade code
        globals.trans.Start(globals.FileCurrentImage, 0)

        ' the old switch-to code
        ViewSlideShowPB.Image = globals.FileCurrentImage
        'SlideShowWnd.BackgroundImage = globals.FileCurrentImage

        ' release the old displayed image
        If Not (fimg Is Nothing) Then fimg.Dispose()

    End Sub

    '
    '-----=====< FileResetArray >=====------
    '
    ' All file names and counts are held in arrays (up to 2048 entries) This routine
    ' zeros out the array of file names and print counts. We'll reload from the folder. 
    ' Image data is managed later for smart release
    '
    Private Sub FileResetArray()
        'debug.TextBox1_println("ResetFilesArray")
        globals.FileNamesMax = 0
        globals.FileNext = 0

    End Sub

    '-----=====< FileAddToArray >=====-----
    '
    ' this routine loads all the .jpg file names and reads in the count files.  At the end, 
    ' the image manager will be called to
    ' figure out whats to be displayed or released
    '
    Private Sub FileAddToArray()

        ' load the first file if available
        Dim files As New List(Of FileInfo)(New DirectoryInfo(globals.fpath).GetFiles("*.jpg"))

        For Each fi As FileInfo In files

            ' save .jpg file info locally
            globals.FileNames(globals.FileNamesMax) = fi.Name
            globals.FileNamesMax += 1

            ' top out at 1024 files
            If globals.FileNamesMax = 1024 Then
                Exit For
            End If

        Next

    End Sub


    '-----=====< FileAddToArray >=====-----
    '
    ' this routine loads all the .jpg file names and reads in the count files.  At the end, 
    ' the image manager will be called to
    ' figure out whats to be displayed or released
    '
    Private Sub FileAddMoreToArray()
        Dim cnt As Integer
        Dim i As Integer
        Dim matchfound As Boolean

        cnt = globals.FileNamesMax
        ' load the first file if available
        Dim files As New List(Of FileInfo)(New DirectoryInfo(globals.fpath).GetFiles("*.jpg"))

        For Each fi As FileInfo In files

            matchfound = False
            ' see if the name is already listed
            For i = 0 To globals.FileNamesMax
                If globals.FileNames(i) = fi.Name Then
                    matchfound = True
                    Exit For
                End If
            Next

            ' if not in the array, add this to the end of the array
            If Not matchfound Then
                ' save .jpg file info locally
                globals.FileNames(globals.FileNamesMax) = fi.Name
                globals.FileNamesMax += 1

                ' top out at 1024 files
                If globals.FileNamesMax = 1024 Then
                    Exit For
                End If
            End If

        Next

    End Sub


    Private Sub ssSizeDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub ssSizeUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub ssPosUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub ssPosDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub ssPosLeft_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub
    Private Sub ssPosRit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    End Sub

    Private Sub ssAdd1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        globals.ssAddAmount = 1
    End Sub

    Private Sub ssAdd10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        globals.ssAddAmount = 10
    End Sub

    Private Sub ssPAdd1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        globals.ssAddAmount = 1
    End Sub

    Private Sub ssPAdd10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        globals.ssAddAmount = 10
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ssBoarders.Click
        If SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable Then
            ssBoarders.Text = "Enable Boarder"
            SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Else
            ssBoarders.Text = "Disable Boarder"
            SlideShowWnd.FormBorderStyle = Windows.Forms.FormBorderStyle.Sizable
        End If

    End Sub

    'Private Sub q1ButtonUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q1ButtonUp.Click
    '    globals.q1RoomsNowAvail += 1
    '    If globals.q1RoomsNowAvail > globals.MaxRegularRooms Then globals.q1RoomsNowAvail = globals.MaxRegularRooms
    '    q1RoomsAvail.Text = "Rooms Available: " & globals.q1RoomsNowAvail
    'End Sub

    'Private Sub q1ButtonDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q1ButtonDown.Click
    '    globals.q1RoomsNowAvail -= 1
    '    If globals.q1RoomsNowAvail < 0 Then globals.q1RoomsNowAvail = 0
    '    q1RoomsAvail.Text = "Rooms Available: " & globals.q1RoomsNowAvail
    'End Sub

    'Private Sub q2ButtonUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q2ButtonUp.Click
    '    globals.q2RoomsNowAvail += 1
    '    If globals.q2RoomsNowAvail > globals.MaxTVRooms Then globals.q2RoomsNowAvail = globals.MaxTVRooms
    '
    '    q2RoomsAvail.Text = "Rooms Available: " & globals.q2RoomsNowAvail
    'End Sub
    'Private Sub q2ButtonDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q2ButtonDown.Click
    '    globals.q2RoomsNowAvail -= 1
    '   If globals.q2RoomsNowAvail < 0 Then globals.q2RoomsNowAvail = 0
    '    q2RoomsAvail.Text = "Rooms Available: " & globals.q2RoomsNowAvail
    'End Sub

    'Private Sub q3ButtonUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q3ButtonUp.Click
    '    globals.q3RoomsNowAvail += 1
    '    If globals.q3RoomsNowAvail > globals.MaxDeluxRooms Then globals.q3RoomsNowAvail = globals.MaxDeluxRooms
    '    q3RoomsAvail.Text = "Rooms Available: " & globals.q3RoomsNowAvail
    'End Sub

    'Private Sub q3ButtonDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles q3ButtonDown.Click
    '    globals.q3RoomsNowAvail -= 1
    '    If globals.q3RoomsNowAvail < 0 Then globals.q3RoomsNowAvail = 0
    '    q3RoomsAvail.Text = "Rooms Available: " & globals.q3RoomsNowAvail
    'End Sub

    Public Sub LoadRoomTypeArray()

    End Sub

    Private Sub PauseButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PauseButton.Click

        If globals.SlideShowPaused Then

            ' free up the files
            FileResetArray()

            ' adopt any changes to the path.  Need to validate this path and one .jpg in the folder!
            globals.fpath = tbFilePath.Text

            ' reload files
            FileAddToArray()

            lblFileCount.Text = globals.FileNamesMax.ToString + " .JPGs in the folder"

            If globals.FileNamesMax > 0 Then
                ' start the slideshow
                FileDisplayNext(True)
            Else
                MsgBox("Cannot run slideshow until there are .JPGs in this folder")
            End If

        Else

            ' Keep the last image up till resumed
            FileDisplayNext(False)

        End If


    End Sub

    Private Sub btnCallUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim siz As Integer = SlideShowWnd.lblTxtCalling.Font.Size

        'siz += 2
        'If siz > 96 Then siz = 96
        'SlideShowWnd.lblTxtCalling.Font = New Font("Microsoft Sans Serif", siz)

        'txtCallFontSize.Text = siz

        'Dim stringSize As New SizeF
        'stringSize = globals.trans.Graphix.MeasureString(globals.DisplayCalling, SlideShowWnd.lblTxtCalling.Font)
    End Sub
    Private Sub btnCallDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim siz As Integer = SlideShowWnd.lblTxtCalling.Font.Size

        ' siz -= 2
        'If siz < 20 Then siz = 20
        'SlideShowWnd.lblTxtCalling.Font = New Font("Microsoft Sans Serif", siz)
        '
        'txtCallFontSize.Text = siz
    End Sub

    Private Sub btnWaitUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim siz As Integer = SlideShowWnd.lblRegWaiting.Font.Size

        'siz += 2
        'If siz > 64 Then siz = 64
        'txtWaitFontSize.Text = siz

    End Sub

    Private Sub btnWaitDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Dim siz As Integer = SlideShowWnd.lblRegWaiting.Font.Size

        'siz -= 2
        'If siz < 12 Then siz = 12
        'txtWaitFontSize.Text = siz

    End Sub

    Private Sub txtCallFontSize_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnResetPos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPos.Click
        'SlideShowWnd.MaximizeBox = False
        'SlideShowWnd.MinimizeBox = False

        SlideShowWnd.WindowState = FormWindowState.Normal
        SlideShowWnd.Location() = New Point(0, 0)
        SlideShowWnd.Size = New Point(640, 480)

    End Sub

    Private Sub btnMaximize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaximize.Click
        If SlideShowWnd.WindowState = FormWindowState.Maximized Then
            SlideShowWnd.WindowState = FormWindowState.Normal

        Else
            SlideShowWnd.WindowState = FormWindowState.Maximized
        End If
        Call SlideShowWnd.ResizeLableBox()
    End Sub

    Private Sub lblSecondsPer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSecondsPer.Click

    End Sub

    Private Sub txtSecondsPer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSecondsPer.TextChanged

        If IsNumeric(txtSecondsPer.Text) Then
            globals.SlideshowSeconds = txtSecondsPer.Text
        Else
            MessageBox.Show("Bad Second Count, Try again")
        End If

    End Sub

    Private Sub btnSecondUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecondUp.Click
        Dim sec As Integer

        If IsNumeric(txtSecondsPer.Text) Then
            sec = txtSecondsPer.Text
            sec += 1
            txtSecondsPer.Text = sec

        Else
            MessageBox.Show("Bad Second Count, Try again")
        End If

    End Sub

    Private Sub btnSecondDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSecondDown.Click
        Dim sec As Integer

        If IsNumeric(txtSecondsPer.Text) Then
            sec = txtSecondsPer.Text
            sec -= 1
            If sec < 1 Then sec = 1
            txtSecondsPer.Text = sec

        Else
            MessageBox.Show("Bad Second Count, Try again")
        End If

    End Sub

    Private Sub chkSmoothFade_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSmoothFade.CheckedChanged

        _SmoothFadeSet()

    End Sub

    Private Sub _SmoothFadeSet()

        If globals.loadcomplete Then

            ' calculate the fade speed
            If chkSmoothFade.Checked Then
                globals.TimerRate = 70
            Else
                globals.TimerRate = 140
            End If

            globals.trans.FadeSpeed(globals.TimerRate)
            globals.alarm.Change(2000, globals.TimerRate)

        End If

    End Sub

    Public Function _checkthispath(ByRef srcStr As String, ByRef defStr As String, ByVal mask As Int16, ByVal verbose As Boolean) As Boolean
        Dim ret As Boolean = True

        ' force a final slash on the backend of the string
        If Microsoft.VisualBasic.Right(srcStr, 1) <> "\" Then
            srcStr = srcStr & "\"
        End If

        If IO.Directory.Exists(srcStr) Then
            MsgBox("Path to Remote is Good")
        Else
            MsgBox("'" & srcStr & "' is not a valid file or directory.")
            ret = False
        End If

        ' return true if good
        Return ret

    End Function

    'Private Sub cbSchedules_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSchedules.CheckedChanged
    '    globals.showSchedulesOnly = cbSchedules.Checked 
    ' End Sub

    Private Sub btnExit_Click(sender As System.Object, e As System.EventArgs) Handles btnExit.Click
        _exitprogram()
    End Sub

    Private Sub chkFade_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkFade.CheckedChanged

    End Sub

    Private Sub ViewSlideShowPB_Click(sender As System.Object, e As System.EventArgs) Handles ViewSlideShowPB.Click

    End Sub

    Private Sub tbFilePath_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbFilePath.TextChanged
        'MsgBox("test")
    End Sub

    Private Sub btnFolderDialog_Click(sender As System.Object, e As System.EventArgs) Handles btnFolderDialog.Click
        Dim str As String

        FolderBrowserDialog1.SelectedPath = tbFilePath.Text
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            str = FolderBrowserDialog1.SelectedPath
            If Microsoft.VisualBasic.Right(str, 1) <> "\" Then
                str = str & "\"
            End If
            tbFilePath.Text = str
            lblFileCount.Text = " Click RUN to see .jpg count"
            WatchFolderChange(str)
        End If

    End Sub
End Class

'================================= global variables =================================

Public Class globals

    Public Shared PgmTitle As String = "SlideShow by Bay Area Event Photography V01.02.00"
    Public Shared fForm1 As New Form1
    Public Shared fSlideShow As New SlideShowWnd
    Public Shared trans As Transition
    Public Shared fpath As String
    Public Shared loadcomplete As Boolean = False

    ' handler control for watching changes to our source folder
    Public Shared WatchFolder As FileSystemWatcher
    Public Shared WatchFolderSetup As Boolean = False

    ' list of file names for the slide show
    Public Shared FileNames(1024) As String             ' string filenames
    Public Shared FileNamesMax As Integer = 0           ' max filename index in array
    Public Shared FileNext As Integer                   ' next index in a round robin
    Public Shared FileCurrentImage As Image             ' the currently open file
    Public Shared SlideshowSeconds As Integer = 2       ' 4 seconds per slide
    Public Shared SlideShowPaused As Boolean = True     ' paused till its ready
    Public Shared NewFilesArrived As Integer = 0

    'Public Shared remote As Boolean = False             ' this copy runs as a slave 
    Public Shared watcher As FileSystemWatcher         ' file system watcher control
    Public Shared StateUpdated As Boolean = False       ' set true when state file updates
    Public Shared statetick As Int16 = 0                ' timer tick for reading the state

    Public Shared alarm As Threading.Timer              ' VB control structure
    Public Shared TimerRate As Integer                  ' # of millseconds per timer tick
    Public Shared cmdLineDebug As Int16

    'Shared Property fPic2Print As Object

    'Public Shared showSchedulesOnly As Boolean = False  ' controls the next file displayed routine
    Public Delegate Sub dOurSafeTimerTick()             ' called by the timer tick routine

    ' misc global stuff I wish were local
    Public Shared ssAddAmount As Integer = 1            ' add amount of 1 or 10


End Class



