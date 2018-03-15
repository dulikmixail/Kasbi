Namespace service
    Public Class ToExeption
        Public goodSysId As Integer
        Public text As String
        Public closePeriod As String
        Public closeDate As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal goodSysId As Integer, ByVal text As String, ByVal closePeriod As String, ByVal closeDate As String)
            Me.goodSysId = goodSysId
            Me.text = text
            Me.closePeriod = closePeriod
            Me.closeDate = closeDate
        End Sub
    End Class
End Namespace
