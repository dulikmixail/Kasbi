Namespace Exeption
    Public Class ToExeption
        Inherits BaseExeption
        Implements IExeption
        Public GoodSysId As Integer
        Public ClosePeriod As String
        Public CloseDate As String
        Public Sub New()
        End Sub
        Public Sub New(ByVal goodSysId As Integer, ByVal closePeriod As String, ByVal closeDate As String)
            Me.GoodSysId = goodSysId
            Me.ClosePeriod = closePeriod
            Me.CloseDate = closeDate
        End Sub
    End Class
End Namespace
