Imports System.Reflection
Imports Microsoft.VisualBasic

Namespace Exeption
    Public Class GoodTransferExeption
        Inherits BaseExeption
        Implements IExeption
        Public IdGoodType As Integer
        Public IdDeliveryFrom As Integer
        Public IdDeliveryTo As Integer

        Public Sub New(ByVal idGoodType As Integer, ByVal idDeliveryFrom As Integer, ByVal idDeliveryTo As Integer)
            Me.IdGoodType = idGoodType
            Me.IdDeliveryFrom = idDeliveryFrom
            Me.IdDeliveryTo = idDeliveryTo
        End Sub

    End Class
End Namespace
