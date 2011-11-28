Public Class LinkedList
    Private mHead As LinkedListItem
    Private mTail As LinkedListItem

    Public Sub Add(ByVal o As Object)
        Dim item As New LinkedListItem(o)
        If mHead Is Nothing Then
            mHead = item
        End If
        If mTail Is Nothing Then
            mTail = item
        Else
            mTail.NextItem = item
            item.PrevItem = mTail
            mTail = item
        End If
    End Sub

    Public Property Head() As LinkedListItem
        Get
            Return mHead
        End Get
        Set(ByVal Value As LinkedListItem)
            mHead = Value
        End Set
    End Property
    Public Property Tail() As LinkedListItem
        Get
            Return mTail
        End Get
        Set(ByVal value As LinkedListItem)
            mTail = value
        End Set
    End Property
End Class
