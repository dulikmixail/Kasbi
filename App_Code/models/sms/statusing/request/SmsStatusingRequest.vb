Imports Microsoft.VisualBasic

Namespace Models.Sms.Statusing.Request

    Public Class SmsStatusingRequest
        Public Property login As String = "Ramok"
        Public Property password As String = "B414Nv9p"
        Public Property command As String = "statuses"
        Public Property status As Status = New Status()

        Public Sub AddSmsId(smsId As String)
            If IsNumeric(smsId) Then
                Me.status.msg.Add(New Msg(smsId))
            Else
                Throw New Exception("Ошибка. Запрос статуса. Не валидный ID СМС")
            End If
        End Sub
    End Class

    Public Class Status
        Public Property msg As List(Of Msg) = New List(Of Msg)
    End Class

    Public Class Msg
        Public Property sms_id As String
        Public Sub New(smsId As String)
            sms_id = smsId
        End Sub
    End Class

End Namespace