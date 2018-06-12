Imports Microsoft.VisualBasic

Public Class GoodExeption
    Public idGoodType As Integer
    Public idDeliveryFrom As Integer
    Public idDeliveryTo As Integer
    Public text As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal idGoodType As Integer, ByVal idDeliveryFrom As Integer, ByVal idDeliveryTo As Integer, ByVal text As String)
        Me.idGoodType = idGoodType
        Me.idDeliveryFrom = idDeliveryFrom
        Me.idDeliveryTo = idDeliveryTo
        Me.text = text
    End Sub
End Class
