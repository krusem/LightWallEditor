Imports System.Drawing

Public Class Editor
    Dim BlockSize As Integer = 30
    Dim GridPixelWidth As Integer = 450
    Dim GridPixelHeight As Integer = 480
    Dim MenuGap As Integer = 24
    Dim GridWidth As Integer = 15
    Dim GridHeight As Integer = 16
    Dim HoverX As Integer
    Dim HoverY As Integer
    Dim CurrentFrame As Integer = 0
    Dim FrameCount As Integer = 0
    Dim Current As New CGrid
    Dim myPen As New Pen(Color.White)
    Dim RedBrush As New SolidBrush(Color.Red)
    Dim YPen As New Pen(Color.Yellow)
    Dim Buffer As New Bitmap(GridPixelWidth, GridPixelHeight + MenuGap)
    Dim formGraphics As Graphics = Graphics.FromImage(Buffer)
    Dim CurrentColor As Color = Color.Red
    Dim Color1 As Color = Color.Black
    Dim Color2 As Color = Color.Black
    Dim Color3 As Color = Color.Black
    Dim Color4 As Color = Color.Black
    Dim color5 As Color = Color.Black
    Dim timechange As Integer = 0
    Dim RightPressed As Boolean = False
    Dim LeftPressed As Boolean = False
    Dim CtrlPressed As Boolean = False
    Dim FrameDelay As Integer = 0
    Dim IsPlaying As Boolean = False

    Private Sub Editor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Right Then
            RightPressed = True
        End If
        If e.KeyCode = Keys.Left Then
            LeftPressed = True
        End If
        If e.KeyCode = Keys.ControlKey Then
            CtrlPressed = True
        End If
    End Sub

    Private Sub Editor_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.ControlKey Then
            CtrlPressed = False
        End If
        If e.KeyCode = Keys.Right Then
            If CurrentFrame = FrameCount Then
                Return
            End If
            If e.Shift Then
                While Not Current.mNext Is Nothing
                    Current = Current.mNext
                End While
                CurrentFrame = FrameCount
                Me.Text = "Light Display Editor   Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Refresh()
            End If
            If Not Current.mNext Is Nothing Then
                Current = Current.mNext
                CurrentFrame = CurrentFrame + 1
                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
                Refresh()
            End If
            RightPressed = False
        End If
        If e.KeyCode = Keys.Left Then
            If CurrentFrame = 0 Then
                Return
            End If
            If e.Shift Then
                While Not Current.mPrev Is Nothing
                    Current = Current.mPrev
                End While
                CurrentFrame = 0
                Me.Text = "Light Display Editor   Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Refresh()
            End If
            If Not Current.mPrev Is Nothing Then
                Current = Current.mPrev
                CurrentFrame = CurrentFrame - 1
                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
                Refresh()
            End If
            LeftPressed = False
        End If
        If e.KeyCode = Keys.D1 Then
            CurrentColor = Color1
        End If
        If e.KeyCode = Keys.D2 Then
            CurrentColor = Color2
        End If
        If e.KeyCode = Keys.D3 Then
            CurrentColor = Color3
        End If
        If e.KeyCode = Keys.D4 Then
            CurrentColor = Color4
        End If
        If e.KeyCode = Keys.D5 Then
            CurrentColor = color5
        End If
        If e.KeyCode = Keys.Space Then
            IsPlaying = Not IsPlaying
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ClientSize = New Size(GridPixelWidth, GridPixelHeight + MenuGap)

        Me.DoubleBuffered() = True

        FrameCount = 0

        Timer1.Enabled = True
        Timer1.Start()

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
        Me.Text = frametext

    End Sub

    Private Sub Form1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Dim Mouse As Point = e.Location
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show(e.Location + Me.Location)
        End If

        If e.Button = MouseButtons.Left Then

            Current.mData(Math.Floor(Mouse.X / BlockSize), Math.Floor((Mouse.Y - MenuGap) / BlockSize)) = CurrentColor
            Refresh()
        End If

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim Mouse As Point = e.Location
        Dim x As Integer
        Dim y As Integer

        HoverX = Math.Floor(Mouse.X / BlockSize)
        HoverY = Math.Floor((Mouse.Y - MenuGap) / BlockSize)

        If e.Button = MouseButtons.Left Then
            x = Math.Floor(Mouse.X / BlockSize)
            y = Math.Floor((Mouse.Y - MenuGap) / BlockSize)

            If x >= 0 And x <= GridWidth And y >= 0 And y <= GridHeight Then
                Current.mData(Math.Floor(Mouse.X / BlockSize), Math.Floor((Mouse.Y - MenuGap) / BlockSize)) = CurrentColor
            End If
        End If

        Refresh()
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim i As Integer = 0
        e.Graphics.Clear(Color.Black)

        For i = 0 To GridHeight
            formGraphics.DrawLine(myPen, 0, (i * BlockSize) + MenuGap, GridPixelWidth, (i * BlockSize) + MenuGap)
        Next
        For i = 0 To GridWidth
            formGraphics.DrawLine(myPen, i * BlockSize, MenuGap, i * BlockSize, GridPixelHeight + MenuGap)
        Next

        For x = 0 To GridWidth
            For y = 0 To GridHeight
                Dim blockcolor As Color
                blockcolor = Current.mData(x, y)
                Dim Brush As New SolidBrush(blockcolor)
                formGraphics.FillRectangle(Brush, x * BlockSize + 1, y * BlockSize + 1 + MenuGap, BlockSize - 1, BlockSize - 1)
            Next
        Next

        formGraphics.DrawRectangle(YPen, HoverX * BlockSize, HoverY * BlockSize + MenuGap, BlockSize, BlockSize)

        e.Graphics.DrawImage(Buffer, 0, 0)

    End Sub

    Private Sub PickColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickColorToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        CurrentColor = ColorDialog1.Color()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        Dim ret As MsgBoxResult
        If FrameCount > 0 Then
            ret = MsgBox("Clear the existing file?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo)
            If ret = MsgBoxResult.No Then
                Return
            End If
        End If
        While Not Current.mPrev Is Nothing
            Current = Current.mPrev
        End While
        While Not Current.mNext Is Nothing
            Current = Current.mNext
            Current.mPrev = Nothing
        End While
        FrameCount = 0
        CurrentFrame = 0

        OpenFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        OpenFileDialog1.FileName = "wall"
        OpenFileDialog1.DefaultExt = "dat"
        OpenFileDialog1.Filter = "Dat files (*.dat)|*.dat|All files (*.*)|*.*"

        Dim path As String
        Dim oret As DialogResult
        oret = OpenFileDialog1.ShowDialog()
        If oret = Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        path = OpenFileDialog1.FileName

        Dim fsStream As System.IO.FileStream
        Dim brReader As System.IO.BinaryReader

        fsStream = New System.IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)

        brReader = New System.IO.BinaryReader(fsStream)

        Dim pointer As CGrid
        pointer = Current
        
        Dim x As Integer
        Dim y As Integer
        Dim bit As Integer
        Dim pin As Integer
        Dim inbyte As Byte
        Dim databyte As Byte
        Dim EOF As Boolean
        Dim shade As Byte
		Dim inword As UShort

        EOF = False

        inbyte = brReader.ReadByte()
        If Not inbyte = 3 Then 'Protocol version 3
            MsgBox("Bad protocol version in header, aborting")
            Return
        End If

        inbyte = brReader.ReadByte()
        If Not inbyte = 0 Then 'Message type 0 Pixel Stream
            MsgBox("Bad Pixel Stream type in header, aborting")
            Return
        End If

        inbyte = brReader.ReadByte()
        If Not inbyte = 30 Then 'Lights per strand count
            MsgBox("Bad Light Count in header, aborting")
            Return
        End If

        inbyte = brReader.ReadByte()
        If Not inbyte = 15 Then 'Lights per row count
            MsgBox("Bad Light Count in header, aborting")
            Return
        End If

        inword = brReader.ReadUInt16()
        FrameDelay = inword

        'POINT TO FIRST FRAME
        While fsStream.Position < (fsStream.Length - 11)
            For y = 0 To 1
                For x = 0 To 14
                    'Start getting Blue bits
                    For bit = 0 To 3
                        inbyte = brReader.ReadByte()
                        inbyte = inbyte Xor 255
                        For pin = 0 To 7
                            databyte = inbyte
                            shade = 0
                            shade = (((databyte >> (7 - pin)) And 1) << (7 - bit)) Or pointer.mData(x, y + (pin * 2)).B
                            pointer.mData(x, y + (pin * 2)) = Color.FromArgb(pointer.mData(x, y + (pin * 2)).R, pointer.mData(x, y + (pin * 2)).G, shade)
                        Next
                    Next
                    'Start getting Green bits
                    For bit = 0 To 3
                        inbyte = brReader.ReadByte()
                        inbyte = inbyte Xor 255
                        For pin = 0 To 7
                            databyte = inbyte
                            shade = 0
                            shade = (((databyte >> (7 - pin)) And 1) << (7 - bit)) Or pointer.mData(x, y + (pin * 2)).G
                            pointer.mData(x, y + (pin * 2)) = Color.FromArgb(pointer.mData(x, y + (pin * 2)).R, shade, pointer.mData(x, y + (pin * 2)).B)
                        Next
                    Next
                    'Start getting Red bits
                    For bit = 0 To 3
                        inbyte = brReader.ReadByte()
                        inbyte = inbyte Xor 255
                        For pin = 0 To 7
                            databyte = inbyte
                            shade = 0
                            shade = (((databyte >> (7 - pin)) And 1) << (7 - bit)) Or pointer.mData(x, y + (pin * 2)).R
                            pointer.mData(x, y + (pin * 2)) = Color.FromArgb(shade, pointer.mData(x, y + (pin * 2)).G, pointer.mData(x, y + (pin * 2)).B)
                        Next
                    Next
                Next
            Next
            'Next Frame
            Dim temp As New CGrid
            Current.mNext = temp
            temp.mPrev = Current
            Current = Current.mNext
            pointer = Current
            FrameCount = FrameCount + 1
            CurrentFrame = CurrentFrame + 1
        End While

        brReader.Close()

        fsStream.Close()
        fsStream.Dispose()

        Current = Current.mPrev
        Current.mNext = Nothing
        CurrentFrame = CurrentFrame - 1
        FrameCount = FrameCount - 1

        While Not Current.mPrev Is Nothing
            Current = Current.mPrev
        End While
        CurrentFrame = 0

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
        Me.Text = frametext

        Refresh()

    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        SaveFileDialog1.FileName = "wall"
        SaveFileDialog1.DefaultExt = "dat"
        SaveFileDialog1.Filter = "Dat files (*.dat)|*.dat|All files (*.*)|*.*"
        SaveFileDialog1.OverwritePrompt = True

        Dim path As String
        Dim sret As DialogResult
        sret = SaveFileDialog1.ShowDialog()
        If sret = Windows.Forms.DialogResult.Cancel Then
            Return
        End If

        path = SaveFileDialog1.FileName

        Dim fsStream As System.IO.FileStream
        Dim bwWriter As System.IO.BinaryWriter

        fsStream = New System.IO.FileStream(path, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.ReadWrite)

        bwWriter = New System.IO.BinaryWriter(fsStream)

        Dim pointer As CGrid
        pointer = Current
        While Not pointer.mPrev Is Nothing
            pointer = pointer.mPrev
        End While

        Dim x As Integer
        Dim y As Integer
        Dim bit As Integer
        Dim pin As Integer
        Dim col As Integer
        Dim Outbyte As Byte
		Dim Outword as UShort

        'Write header
        Outbyte = 3 'Protocol version 2
        bwWriter.Write(Outbyte)
        Outbyte = 0 'Message type 0 Pixel Stream
        bwWriter.Write(Outbyte)
        Outbyte = 30 'Lights per strand count
        bwWriter.Write(Outbyte)
        Outbyte = 15 'Lights per row count
        bwWriter.Write(Outbyte)
        Outword = FrameDelay 'Global Frame Delay
        bwWriter.Write(Outword)

        Outbyte = 0

        Do
            For col = 0 To 29
                x = col Mod 15
                If col > 14 Then
                    y = 1
                Else
                    y = 0
                End If
                'Start getting Blue bits
                For bit = 0 To 3
                    For pin = 0 To 7
                        Outbyte = (((pointer.mData(x, y + (pin * 2)).B >> (7 - bit)) And 1) << (7 - pin)) Or Outbyte
                    Next
                    Outbyte = Outbyte Xor 255
                    bwWriter.Write(Outbyte)
                    Outbyte = 0
                Next
                'Start getting Green bits
                For bit = 0 To 3
                    For pin = 0 To 7
                        Outbyte = (((pointer.mData(x, y + (pin * 2)).G >> (7 - bit)) And 1) << (7 - pin)) Or Outbyte
                    Next
                    Outbyte = Outbyte Xor 255
                    bwWriter.Write(Outbyte)
                    Outbyte = 0
                Next
                'Start getting Red bits
                For bit = 0 To 3
                    For pin = 0 To 7
                        Outbyte = (((pointer.mData(x, y + (pin * 2)).R >> (7 - bit)) And 1) << (7 - pin)) Or Outbyte
                    Next
                    Outbyte = Outbyte Xor 255
                    bwWriter.Write(Outbyte)
                    Outbyte = 0
                Next
            Next
            'If Not pointer.mNext Is Nothing Then
            pointer = pointer.mNext
            'End If

        Loop While Not pointer Is Nothing

        bwWriter.Close()

        fsStream.Close()
        fsStream.Dispose()
    End Sub

    Private Sub NewFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFrameToolStripMenuItem.Click
        FrameCount = FrameCount + 1

        Dim temp As New CGrid
        If Current.mNext Is Nothing Then
            Current.mNext = temp
            temp.mPrev = Current
        ElseIf Not Current.mNext Is Nothing Then
            temp.mNext = Current.mNext
            temp.mPrev = Current

            Current.mNext.mPrev = temp
            Current.mNext = temp
        End If

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
        Me.Text = frametext
    End Sub

    Private Sub NextFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextFrameToolStripMenuItem.Click
        If CurrentFrame = FrameCount Then
            Return
        End If

        If Not Current.mNext Is Nothing Then
            Current = Current.mNext
            CurrentFrame = CurrentFrame + 1
            Dim frametext As String
            frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
            Me.Text = frametext
        Else
            MsgBox("mNext was nothing")
        End If
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        For x = 0 To GridWidth
            For y = 0 To GridHeight
                Dim blockcolor As Color
                blockcolor = Color.Black
                Current.mData(x, y) = blockcolor
            Next
        Next
    End Sub

    Private Sub PrevFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevFrameToolStripMenuItem.Click
        If CurrentFrame = 0 Then
            Return
        End If

        If Not Current.mPrev Is Nothing Then
            Current = Current.mPrev
            CurrentFrame = CurrentFrame - 1
            Dim frametext As String
            frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
            Me.Text = frametext
        Else
            MsgBox("mPrev was nothing")
        End If

    End Sub

    Private Sub CopyFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyFrameToolStripMenuItem.Click
        Dim temp As New CGrid
        FrameCount = FrameCount + 1

        If Current.mNext Is Nothing Then
            Current.mNext = temp
            temp.mPrev = Current
            temp.mNext = Nothing
        Else
            temp.mNext = Current.mNext
            temp.mPrev = Current
            Current.mNext.mPrev = temp
            Current.mNext = temp
        End If

        For x = 0 To GridWidth
            For y = 0 To GridHeight
                temp.mData(x, y) = Current.mData(x, y)
            Next
        Next

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
        Me.Text = frametext

        Refresh()
    End Sub

    Private Sub DeleteFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFrameToolStripMenuItem.Click
        
        Dim result
        result = MsgBox("Are you sure you want to delete this frame?", MsgBoxStyle.ApplicationModal Or MsgBoxStyle.Exclamation Or MsgBoxStyle.YesNo, "Are you sure?")
        If result = MsgBoxResult.Yes Then
            If CurrentFrame = 0 And FrameCount = 0 Then
                For x = 0 To GridWidth
                    For y = 0 To GridHeight
                        Current.mData(x, y) = Color.Black
                    Next
                Next
            Else
                Dim temp As CGrid
                temp = Current

                If CurrentFrame = 0 Then
                    temp.mNext.mPrev = Nothing
                    Current = temp.mNext
                ElseIf Current.mNext Is Nothing Then
                    temp.mPrev.mNext = Nothing
                    Current = Current.mPrev
                    CurrentFrame = CurrentFrame - 1
                Else
                    temp.mNext.mPrev = temp.mPrev
                    temp.mPrev.mNext = temp.mNext
                    Current = temp.mPrev
                    CurrentFrame = CurrentFrame - 1
                End If

                temp = Nothing
                FrameCount = FrameCount - 1

                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
            End If
            Refresh()
        End If
    End Sub

    Private Sub ToolStripMenuItem6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem6.Click
        ColorDialog1.ShowDialog()
        Color1 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem7.Click
        ColorDialog1.ShowDialog()
        Color2 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem8.Click
        ColorDialog1.ShowDialog()
        Color3 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem9.Click
        ColorDialog1.ShowDialog()
        Color4 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem10.Click
        ColorDialog1.ShowDialog()
        color5 = ColorDialog1.Color()
    End Sub

    Private Sub ShiftFrameUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftFrameUpToolStripMenuItem.Click
        Dim temp As New CGrid

        For x = 0 To GridWidth
            For y = 0 To GridWidth
                temp.mData(x, y) = Current.mData(x, y)
            Next
        Next

        For x = 0 To GridWidth
            For y = 0 To GridHeight - 1
                Current.mData(x, y) = temp.mData(x, y + 1)
            Next
        Next

        temp = Nothing
    End Sub

    Private Sub ShiftFrameDownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftFrameDownToolStripMenuItem.Click
        Dim temp As New CGrid

        For x = 0 To GridWidth
            For y = 0 To GridWidth
                temp.mData(x, y) = Current.mData(x, y)
            Next
        Next

        For x = 0 To GridWidth
            For y = 1 To GridHeight
                Current.mData(x, y) = temp.mData(x, y - 1)
            Next
        Next

        For x = 0 To GridWidth
            Current.mData(x, 0) = Color.Black
        Next

        temp = Nothing
    End Sub

    Private Sub ShiftFrameRightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftFrameRightToolStripMenuItem.Click
        Dim temp As New CGrid

        For x = 0 To GridWidth
            For y = 0 To GridWidth
                temp.mData(x, y) = Current.mData(x, y)
            Next
        Next

        For x = 1 To GridWidth
            For y = 0 To GridHeight
                Current.mData(x, y) = temp.mData(x - 1, y)
            Next
        Next

        For y = 0 To GridHeight
            Current.mData(0, y) = Color.Black
        Next

        temp = Nothing
    End Sub

    Private Sub ShiftLeftToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftLeftToolStripMenuItem.Click
        Dim temp As New CGrid

        For x = 0 To GridWidth
            For y = 0 To GridWidth
                temp.mData(x, y) = Current.mData(x, y)
            Next
        Next

        For x = 0 To GridWidth - 1
            For y = 0 To GridHeight
                Current.mData(x, y) = temp.mData(x + 1, y)
            Next
        Next

        temp = Nothing
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        timechange = timechange + 1
        If RightPressed And CtrlPressed And Not IsPlaying Then
            If Not Current.mNext Is Nothing Then
                Current = Current.mNext
                CurrentFrame = CurrentFrame + 1
                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
                Refresh()
            End If
        End If
        If LeftPressed And CtrlPressed And Not IsPlaying Then
            If Not Current.mPrev Is Nothing Then
                Current = Current.mPrev
                CurrentFrame = CurrentFrame - 1
                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
                Refresh()
            End If
        End If
        If IsPlaying Then
            If Current.mNext Is Nothing Then
                IsPlaying = False
            End If
            If ((timechange * 10) > FrameDelay) And IsPlaying Then
                Current = Current.mNext
                CurrentFrame = CurrentFrame + 1
                Dim frametext As String
                frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
                Me.Text = frametext
                Refresh()
                timechange = 0
            End If
        End If
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub OpenToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem1.Click
        OpenToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub SaveToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem1.Click
        SaveToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PickColorToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickColorToolStripMenuItem1.Click
        PickColorToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ToolStripMenuItem11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem11.Click
        ColorDialog1.ShowDialog()
        Color1 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem12.Click
        ColorDialog1.ShowDialog()
        Color2 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem13.Click
        ColorDialog1.ShowDialog()
        Color3 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem14.Click
        ColorDialog1.ShowDialog()
        Color4 = ColorDialog1.Color()
    End Sub

    Private Sub ToolStripMenuItem15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem15.Click
        ColorDialog1.ShowDialog()
        color5 = ColorDialog1.Color()
    End Sub

    Private Sub NewFrameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFrameToolStripMenuItem1.Click
        NewFrameToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub NextFrameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextFrameToolStripMenuItem1.Click
        NextFrameToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub PrevFrameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevFrameToolStripMenuItem1.Click
        PrevFrameToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub CopyFrameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyFrameToolStripMenuItem1.Click
        CopyFrameToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub DeleteFrameToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteFrameToolStripMenuItem1.Click
        DeleteFrameToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ClearToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearToolStripMenuItem.Click
        ToolStripMenuItem3_Click(sender, e)
    End Sub

    Private Sub ShiftUpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftUpToolStripMenuItem.Click
        ShiftFrameUpToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ShiftDownToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftDownToolStripMenuItem.Click
        ShiftFrameDownToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ShiftRightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftRightToolStripMenuItem.Click
        ShiftFrameRightToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ShiftLeftToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShiftLeftToolStripMenuItem1.Click
        ShiftLeftToolStripMenuItem_Click(sender, e)
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        Dim ret As MsgBoxResult
        If FrameCount >= 0 Then
            ret = MsgBox("Clear the existing file?", MsgBoxStyle.Critical Or MsgBoxStyle.YesNo)
            If ret = MsgBoxResult.No Then
                Return
            End If
        End If
        While Not Current.mPrev Is Nothing
            Current = Current.mPrev
        End While
        While Not Current.mNext Is Nothing
            Current = Current.mNext
            Current.mPrev = Nothing
        End While
        FrameCount = 0
        CurrentFrame = 0

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1 & "/" & FrameCount + 1
        Me.Text = frametext

        Refresh()
    End Sub

    Private Sub FrameDelayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrameDelayToolStripMenuItem.Click
        FrameDelayToolStripMenuItem1_Click(sender, e)
    End Sub

    Private Sub FrameDelayToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FrameDelayToolStripMenuItem1.Click
        Dim tmp As String
        tmp = InputBox("Frame delay in ms", "Frame Delay", FrameDelay)
        If tmp <> "" Then
            FrameDelay = tmp
        End If
    End Sub

    Private Sub ToolStripMenuItem5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem5.Click
        IsPlaying = Not IsPlaying
    End Sub

    Private Sub PlayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PlayToolStripMenuItem.Click
        IsPlaying = Not IsPlaying
    End Sub

    Private Sub HotKeyHelpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HotKeyHelpToolStripMenuItem.Click
        Dim msg As String
        msg = "Hotkey help" & vbCrLf & _
            vbCrLf & _
            "left arrow   - previous frame" & vbCrLf & _
            "right arrow - next frame" & vbCrLf & _
            "ctrl            - fast foward with arrows" & vbCrLf & _
            "shift          - jump to beginning or end with arrows" & vbCrLf & _
            "1 - 5          - color key selection" & vbCrLf & _
            "space         - begin playback"
        Beep()
        MsgBox(msg)
    End Sub

    Private Sub AboutToolStripMenuItem2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem2.Click
        AboutBox1.ShowDialog()
    End Sub
End Class
