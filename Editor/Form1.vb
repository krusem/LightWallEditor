Imports System.Drawing

Public Class Editor
    Dim BlockSize As Integer = 30
    Dim GridPixelWidth As Integer = 450
    Dim GridPixelHeight As Integer = 510
    Dim GridWidth As Integer = 15
    Dim GridHeight As Integer = 17
    Dim HoverX As Integer
    Dim HoverY As Integer
    Dim CurrentFrame As Integer = 0
    Dim FrameCount As Integer = 0
    Dim MaxFrames As Integer = 32
    Dim Grid(0 To MaxFrames) As CGrid
    'Dim Grid As LinkedList(Of CGrid)
    Dim myPen As New Pen(Color.White)
    Dim RedBrush As New SolidBrush(Color.Red)
    Dim YPen As New Pen(Color.Yellow)
    Dim Buffer As New Bitmap(GridPixelWidth, GridPixelHeight)
    Dim formGraphics As Graphics = Graphics.FromImage(Buffer)
    Dim CurrentColor As Color = Color.Red

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ClientSize = New Size(GridPixelWidth, GridPixelHeight)

        Me.DoubleBuffered() = True

        'For i As Integer = 0 To MaxFrames
        Grid(CurrentFrame) = New CGrid
        Grid(CurrentFrame).Init(0, GridWidth, GridHeight)
        'Next
        FrameCount = 0

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1
        Me.Text = frametext

    End Sub

    Private Sub Form1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseClick
        Dim Mouse As Point = e.Location
        If e.Button = MouseButtons.Right Then
            ContextMenuStrip1.Show(e.Location + Me.Location)
        End If

        If e.Button = MouseButtons.Left Then
            Grid(CurrentFrame).Data(Math.Floor(Mouse.X / BlockSize), Math.Floor(Mouse.Y / BlockSize)) = CurrentColor
            Refresh()
        End If

    End Sub

    Private Sub Form1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseMove
        Dim Mouse As Point = e.Location
        Dim x As Integer
        Dim y As Integer

        HoverX = Math.Floor(Mouse.X / BlockSize)
        HoverY = Math.Floor(Mouse.Y / BlockSize)

        If e.Button = MouseButtons.Left Then
            x = Mouse.X / BlockSize
            y = Mouse.Y / BlockSize
            If x > 0 And x < GridWidth And y > 0 And y < GridHeight Then
                Grid(CurrentFrame).Data(Math.Floor(Mouse.X / BlockSize), Math.Floor(Mouse.Y / BlockSize)) = CurrentColor
            End If
        End If


        Refresh()
    End Sub

    Private Sub Form1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        Dim i As Integer = 0
        e.Graphics.Clear(Color.Black)

        For i = 0 To GridHeight
            formGraphics.DrawLine(myPen, 0, i * BlockSize, GridPixelWidth, i * BlockSize)
        Next
        For i = 0 To GridWidth
            formGraphics.DrawLine(myPen, i * BlockSize, 0, i * BlockSize, GridPixelHeight)
        Next

        For x = 0 To GridWidth
            For y = 0 To GridHeight
                Dim blockcolor As Color
                blockcolor = Grid(CurrentFrame).Data(x, y)
                Dim Brush As New SolidBrush(blockcolor)
                formGraphics.FillRectangle(Brush, x * BlockSize + 1, y * BlockSize + 1, BlockSize - 1, BlockSize - 1)
            Next
        Next

        formGraphics.DrawRectangle(YPen, HoverX * BlockSize, HoverY * BlockSize, BlockSize, BlockSize)

        e.Graphics.DrawImage(Buffer, 0, 0)

    End Sub

    Private Sub PickColorToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickColorToolStripMenuItem.Click
        ColorDialog1.ShowDialog()
        CurrentColor = ColorDialog1.Color()
    End Sub

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        MsgBox("I need a file format Coop")
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        MsgBox("I need a file format Coop")
    End Sub

    Private Sub NewFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewFrameToolStripMenuItem.Click
        FrameCount = FrameCount + 1
        'Grid(FrameCount).Init(FrameCount, GridWidth, GridHeight)
        Grid(FrameCount) = New CGrid
        Grid(FrameCount).Init(FrameCount, GridWidth, GridHeight)

        CurrentFrame = FrameCount

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1
        Me.Text = frametext
    End Sub

    Private Sub NextFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NextFrameToolStripMenuItem.Click
        If CurrentFrame = FrameCount Then
            Return
        End If
        CurrentFrame = CurrentFrame + 1

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1
        Me.Text = frametext
    End Sub

    Private Sub ToolStripMenuItem3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem3.Click
        For x = 0 To GridWidth
            For y = 0 To GridHeight
                Dim blockcolor As Color
                blockcolor = Color.Black
                Grid(CurrentFrame).Data(x, y) = blockcolor
            Next
        Next
    End Sub

    Private Sub PrevFrameToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrevFrameToolStripMenuItem.Click
        If CurrentFrame = 0 Then
            Return
        End If
        CurrentFrame = CurrentFrame - 1

        Dim frametext As String
        frametext = "Light Display Editor    Current Frame: " & CurrentFrame + 1
        Me.Text = frametext
    End Sub
End Class
