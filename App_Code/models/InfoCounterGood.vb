Imports Microsoft.VisualBasic

Namespace Models
    Public Class InfoCounterGood

        Public idDelivery As Integer
        Public idGoodType As Integer
        Public nameGoodType As String
        Public prihod As Integer
        Public rashod As Integer
        Public ostatok As Integer

        Public Sub New(idDelivery As Integer, idGoodType As Integer, nameGoodType As String, prihod As Integer, rashod As Integer, ostatok As Integer)
            Me.idGoodType = idGoodType
            Me.idDelivery = idDelivery
            Me.nameGoodType = nameGoodType
            Me.prihod = prihod
            Me.rashod = rashod
            Me.ostatok = ostatok
        End Sub
    End Class
End Namespace

