Public Class LinkedListItem
    Private mValue As Object
    Private mNext As LinkedListItem
    Private mPrev As LinkedListItem

    Public Sub New(ByVal o As Object)
        mValue = o
    End Sub

    Public Property NextItem() As LinkedListItem
        Get
            Return mNext
        End Get
        Set(ByVal Value As LinkedListItem)
            mNext = Value
        End Set
    End Property

    Public Property PrevItem() As LinkedListItem
        Get
            Return mPrev
        End Get
        Set(ByVal value As LinkedListItem)
            mPrev = value
        End Set
    End Property

    Public ReadOnly Property Value() As Object
        Get
            Return mValue
        End Get
    End Property
End Class