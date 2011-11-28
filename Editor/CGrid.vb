Public Class CGrid
    Public mData(15, 17) As Color
    Public mNext As CGrid = Nothing
    Public mPrev As CGrid = Nothing

    Public Sub New()
        For xc As Integer = 0 To 15
            For yc As Integer = 0 To 17
                mData(xc, yc) = Color.Black
            Next
        Next
    End Sub

End Class
