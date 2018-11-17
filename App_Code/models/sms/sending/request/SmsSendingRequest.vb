Imports Microsoft.VisualBasic


Namespace Models.Sms.Sending.Request
    Public Class SmsSendingRequest
        Const SenderSms As String = "Ramok.by"
        Public Property login As String = "Ramok"
        Public Property password As String = "B414Nv9p"
        Public Property command As String = "sms_send"
        Public Property date_send As String
        Public Property message As Message = New Message()

        Public Sub New(Optional dateSend As DateTime = Nothing)
            If (dateSend = Nothing) Then
                date_send = Nothing
            Else
                date_send = dateSend.ToString("yyyyMMddHHmmss")
            End If
            
        End Sub

        Public Sub SetDefaultMessage(smsText As String, Optional validityPeriod As Integer = 0)
            Me.message.default = New [Default](smsText, SenderSms, validityPeriod)
        End Sub

        Public Sub AddMessage(recipient As String, Optional smsText As String = Nothing, Optional validityPeriod As Integer = 0)
            Me.message.msg.Add(New Msg(recipient, smsText, SenderSms, validityPeriod))
        End Sub
    End Class
    Public Class Message
        Public Property [default] As [Default]
        Public Property msg As List(Of Msg) = New List(Of Msg)

        Public Sub New([default] As [Default], msg As List(Of Msg))
            Me.[default] = [default]
            Me.msg = msg
        End Sub

        Public Sub New()
        End Sub
    End Class

    Public Class [Default]
        Public Property sender As String
        Public Property validity_period As Integer
        Public Property sms_text As String

        Public Sub New(smsText As String, sender As String, Optional validityPeriod As Integer = 0)
            Me.sender = sender
            sms_text = smsText
            validity_period = validityPeriod
        End Sub
    End Class

    Public Class Msg
        Public Property recipient As String
        Public Property sender As String
        Public Property validity_period As Integer
        Public Property sms_text As String

        Public Sub New(recipient As String, smsText As String, sender As String, Optional validityPeriod As Integer = 0)
            Me.recipient = recipient
            Me.sender = sender
            validity_period = validityPeriod
            sms_text = smsText
        End Sub
    End Class

End Namespace