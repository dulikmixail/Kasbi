Imports Microsoft.VisualBasic

Namespace Models.Sms.Sending.Response

    Public Class SmsSendingResponse
        Public Property message As Message

        Public Sub New(message As Message)
            Me.message = message
        End Sub
    End Class

    Public Class Message
        Public Property msg As Msg()

        Public Sub New(msg As Msg())
            Me.msg = msg
        End Sub
    End Class

    Public Class Msg
        Public Property sms_id As Integer
        Public Property sms_count As Integer
        Public Property [operator] As Integer
        Public Property error_code As Integer

        Public Sub New(smsId As Integer, smsCount As Integer, [operator] As Integer, errorCode As Integer)
            sms_id = smsId
            sms_count = smsCount
            Me.operator = [operator]
            error_code = errorCode
        End Sub
    End Class

End Namespace