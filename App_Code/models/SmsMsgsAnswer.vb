Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsMsgsAnswer
        Private Property SmsId As Integer
        Private Property SmsCount As Integer
        Private Property ErrorCode As Integer
  
        Public Sub New(smsId As Integer, smsCount As Integer, errorCode As Integer)
            Me.SmsId = smsId
            Me.SmsCount = smsCount
            Me.ErrorCode = errorCode
        End Sub

        Public Overrides Function ToString() As String
            Return "{""sms_id"":""" & SmsId & """,""sms_count:""" & SmsCount & """,""error_code:""" & ErrorCode & """}"
        End Function
    End Class

End Namespace
