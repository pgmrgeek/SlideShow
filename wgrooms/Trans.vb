Imports System
Imports System.Windows.Forms
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.ComponentModel

Public Class Transition

    'Velocidad a la que se dibujará el efecto
    Private _fadespeed As Integer = 20  ' ' number of steps from 0 to 1, 10 fast, 20 slow

    'Color que se usará para rellenar el fondo de la imagen
    Private _Color As Color = Color.Transparent

    'Contiene una copia de la imagén pasada a uno de los constructores para realizar los efectos
    Private _bmpSourceImg As Image
    Private _bmpInterim As Image

    'Contiene una copia vacia de _bmpTextura donde se dibujará el efecto
    Private _bmpTargetImg As Image

    'Creamos un objeto _Graphics para dibujar en _bmpDibujar
    Private _Gr As Graphics

    'Contienen el Ancho y Alto de _bmpTextura respectivamente
    Private _WidthImagen, _HeightImagen As Single

    ' timer enable/disable flag
    Private _tGo As Boolean = False

    ' increasing opacity
    Private _Opacity As Single = 0.01F

    ' speed of the fade
    Private _CopiaVelocidad As Single = 0.01F

    '================================== constructors =======================================

    Public Sub New(ByVal FirstImage As String, Optional ByVal intVelocidad As Integer = 20)
        Try
            _bmpSourceImg = Image.FromFile(FirstImage)
            FadeInitialize(intVelocidad)
        Catch ex As Exception When Not IO.Directory.Exists(FirstImage)
            MessageBox.Show("First file Missing - " & FirstImage)
        Catch ex As Exception When _bmpSourceImg Is Nothing
            MessageBox.Show("Could not Load the Image.")
        End Try
    End Sub

    Public Sub New(ByVal Imagen As Image, Optional ByVal intVelocidad As Integer = 20)
        Try
            _bmpSourceImg = Imagen
            FadeInitialize(intVelocidad)
        Catch ex As Exception When _bmpSourceImg Is Nothing
            MessageBox.Show("La imagen no puede ser nula.")
        End Try
    End Sub

    '=============================== methods ====================================
    Public Sub FadeSpeed(ByVal i As Integer)

        If i = 70 Then
            _fadespeed = 20
        Else
            _fadespeed = 10
        End If

    End Sub

    ' Called to do one increment of the fade effect 
    Public Sub Fade()
        Static Dim sema As Int16 = -1

        ' only process if we're not already here..
        sema += 1
        If sema <> 0 Then
            sema -= 1
            Return
        End If

        ' ---- Cm represents a 5X5 matrix that will be used to alter the opacity of the image
        Dim Cm As ColorMatrix = New ColorMatrix

        ' Ia establish ---- With color matrix Cm then used to draw the image Ia using DrawImage
        Dim Ia As ImageAttributes = New ImageAttributes

        ' ---- We increase the value of Opacity
        _Opacity += _CopiaVelocidad

        ' ---- The Opacity value must be between 0.0 and 1.0

        If _Opacity > 1.0F Then
            _Opacity = 1.0F
        ElseIf _Opacity < 0.01F Then
            _Opacity = 0.01F
        End If

        ' ---- We clean image
        '_Gr.Clear(Color.Transparent)

        ' ---- Changing the value in column 3 , row 3 of the matrix color change is achieved
        ' Opacity image
        Cm.Matrix33 = _Opacity

        ' Establish a Cm as the color matrix of the object image attributes Ia
        Ia.SetColorMatrix(Cm)

        ' Draw the image using the image object attributes previously prepared
        _Gr.DrawImage(_bmpInterim, New Rectangle(0, 0, CInt(_WidthImagen), CInt(_HeightImagen)), 0, 0, _WidthImagen, _HeightImagen, GraphicsUnit.Pixel, Ia)

        ' DSC draw all the text too
        'drawtextboxes()

        ' Refreshed image container
        SlideShowWnd.Refresh()

        ' When the image is completely opaque finish the effect
        If _Opacity >= 1.0F Then
            _tGo = False
            _Opacity = 0.01F
        End If
        Ia.Dispose()

        sema -= 1

    End Sub

    Private Sub drawtextboxes()
    End Sub

    ' returns true/false if a fade is in progress
    ReadOnly Property IsRunning() As Boolean
        Get
            Return _tGo
        End Get
    End Property
    ' returns true/false if a fade is in progress
    ReadOnly Property Graphix() As Graphics
        Get
            Return _Gr
        End Get
    End Property

    ' Force the provided image into the slideshow now

    Public Sub Force(ByVal img As Image)

        ' save the source image for the upcoming fades
        _bmpSourceImg = img

        ' copy the source image to the interim
        copyToInterim()

        ' ---- 100 is the maximum value of the property VelocidadEfecto
        ' To calculate the speed based on this property CSng calculated ( 101 - _Speed ​​) / 1000
        _CopiaVelocidad = CSng(101 - _fadespeed) / 1000

        ' we blow the whole thing at once

        _Opacity = 1.0F
        _tGo = True

    End Sub
    ' passes in the next image to fade to, then starts the fade

    Public Sub Start(ByVal img As Image, ByVal FXtype As Integer)
        'Dim gr As Graphics
        Dim Brush As New SolidBrush(Color.Green)

        ' save the source image for the upcoming fades, but use the interim as the target
        _bmpSourceImg = img
        copyToInterim()

        ' copy the source image to the interim buffer with text overlays

        'If FXtype = -1 Then
        'Call fxNormalWriteText(FXtype)
        'End If

        ' ---- Opacity value contains the actual opacity with which to draw the image
        _Opacity = 0.01F
        ' ---- 100 is the maximum value of the property VelocidadEfecto
        ' To calculate the speed based on this property CSng calculated ( 101 - _Speed ​​) / 1000
        _CopiaVelocidad = CSng(101 - _fadespeed) / 1000

        ' if no fade, we blow the whole thing at once
        If Form1.chkFade.Checked = False Then
            _Opacity = 1.0F
        End If

        _tGo = True

    End Sub

    Private Sub fxNormalWriteText(ByVal FXtype As Integer)

    End Sub

    Private Sub fxSpecificRoomWriteText(ByRef title As String, _
                                        ByVal typ As Integer, _
                                        ByRef strAvail As String, _
                                        ByRef strNot1 As String, _
                                        ByRef strNot2 As String, _
                                        ByRef strNot3 As String)
    End Sub

    Private Sub copyToInterim()
        Dim gr As Graphics
        'Dim Opacity As Single = 1.0F
        'Dim x As Integer
        'Dim y As Integer

        ' ---- Cm represents a 5X5 matrix that will be used to alter the opacity of the image
        Dim Cm As ColorMatrix = New ColorMatrix

        ' Ia establish ---- With color matrix Cm then used to draw the image Ia using DrawImage
        Dim Ia As ImageAttributes = New ImageAttributes

        ' clear the whole interim 

        gr = Graphics.FromImage(_bmpInterim)
        gr.Clear(Color.Black)

        ' setup to copy the source to the interim, to be centered
        'gr = Graphics.FromImage(_bmpSourceImg)

        ' ---- Changing the value in column 3 , row 3 of the matrix color change is achieved
        ' Opacity image
        Cm.Matrix33 = 1.0F ' Opacity

        ' Establish a Cm as the color matrix of the object image attributes Ia
        Ia.SetColorMatrix(Cm)

        ' center the image by creating the starting point

        'x = 0
        'If (_bmpSourceImg.Width < 1024) Then x = (1024 - _bmpSourceImg.Width) / 2
        'y = 0
        'If (_bmpSourceImg.Height < 768) Then y = (768 - _bmpSourceImg.Height) / 2

        ' Draw the image using the image object attributes previously prepared

        ' good gr.DrawImage(_bmpSourceImg, New Rectangle(x, y, CInt(_WidthImagen), CInt(_HeightImagen)), 0, 0, _WidthImagen, _HeightImagen, GraphicsUnit.Pixel, Ia)
        gr.DrawImage(_bmpSourceImg, New Rectangle(0, 0, CInt(_WidthImagen), CInt(_HeightImagen)), 0, 0, _bmpSourceImg.Width, _bmpSourceImg.Height, GraphicsUnit.Pixel, Ia)

        Ia.Dispose()

    End Sub
    '================================== internal routines ===========================================

    'Establecemos las variables de nivel de modulo
    Private Sub FadeInitialize(ByVal intVelocidad As Integer)

        ' Create the target bitmap thats always displayed

        _WidthImagen = 1152 ' _bmpSourceImg.Width
        _HeightImagen = 768 ' _bmpSourceImg.Height

        _bmpTargetImg = New Bitmap(CInt(_WidthImagen), CInt(_HeightImagen), PixelFormat.Format32bppArgb)

        _Gr = Graphics.FromImage(_bmpTargetImg)
        _Gr.Clear(_Color)

        SlideShowWnd.BackgroundImage = _bmpTargetImg

        ' Create an interim buffer that accepts multiple sized incoming images

        _WidthImagen = 1152 ' _bmpSourceImg.Width
        _HeightImagen = 768 ' _bmpSourceImg.Height

        _bmpInterim = New Bitmap(CInt(_WidthImagen), CInt(_HeightImagen), PixelFormat.Format32bppArgb)

    End Sub

    Private Sub tOurSafeTimerTick()
        If _tGo Then
            globals.trans.Fade()
        End If
    End Sub

    'Descarga objetos y detiene el Timer
    Private Sub DescargaObjetos()
        ' we save the background for reuse, so commenting out the next line.
        '_Gr.Dispose()

        '_Tiempo.Stop()

    End Sub

End Class
