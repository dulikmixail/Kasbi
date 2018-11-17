Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsStatusAnswer
        Private Property SmsId As Integer
        Private Property SmsCount As Integer
        Private Property SmsStatus As String

        Public Sub New(smsId As Integer, smsCount As Integer, smsStatus As String)
            Me.SmsId = smsId
            Me.SmsCount = smsCount
            Me.SmsStatus = smsStatus
        End Sub

        Public Overrides Function ToString() As String
            Return "{""sms_id"":""" & SmsId & """,""sms_count:""" & SmsCount & """,""sms_status:""" & smsStatus & """}"
        End Function
    End Class
End Namespace