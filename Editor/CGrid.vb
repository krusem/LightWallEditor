Public Class CGrid
    Public Data(1, 1) As Color
    Public FrameNumber As Integer

    Public Sub Init(ByVal f As Integer, ByVal x As Integer, ByVal y As Integer)
        ReDim Data(x, y)
        FrameNumber = f
        For xc As Integer = 0 To x
            For yc As Integer = 0 To y
                Data(xc, yc) = Color.Black
            Next
        Next
    End Sub
End Class
