Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsModel
        Public Property PhoneNumber As String
        Public Property SmsText As String
        Public Property ExecutorId As Integer
        Public Property SmsType As Integer
        Public Property DataSend As DateTime
        Public Property CashHistoryId As Integer
        Public Property GoodId As Integer
        Public Property CustomerId As Integer

        Public Sub New(phoneNumber As String, smsText As String, executorId As Integer, smsType As Integer,
                       Optional dataSend As Date = Nothing, Optional cashHistoryId As Integer = 0,
                       Optional goodId As Integer = 0, Optional customerId As Integer = 0)
            Me.PhoneNumber = phoneNumber
            Me.SmsText = smsText
            Me.ExecutorId = executorId
            Me.SmsType = smsType
            Me.DataSend = dataSend
            Me.CashHistoryId = cashHistoryId
            Me.GoodId = goodId
            Me.CustomerId = customerId
        End Sub
    End Class
End Namespace

