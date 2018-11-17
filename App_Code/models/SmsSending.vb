Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsSending
        Private Property Login As String
        Private Property Password As String
        Private Property Command As SmsCommand
        Private Property DataSend As DateTime
        Private Property Message As SmsMessage

        Public Sub New(login As String, password As String, command As SmsCommand, message As SmsMessage, Optional dataSend As DateTime = Nothing)
            Me.Login = login
            Me.Password = password
            Me.Command = command
            Me.DataSend = dataSend
            Me.Message = message
        End Sub

        Public Overrides Function ToString() As String
            Dim serializer As New JavaScriptSerializer()
            Dim messageJson As String = Message.ToString()
            Dim commandStr As String = String.Empty
            Select Case Command
                Case SmsCommand.sms_send
                    commandStr = "sms_send"
                Case SmsCommand.statuses
                    commandStr = "statuses"
            End Select

            Dim jsonStr As String = "{" & _
                                    """login"":""" & Login &"""," & _
                                    """password"":""" & Password &"""," & _
                                    """command"":""" & commandStr &""","
            'If()

            If (DataSend <> DateTime.MinValue) Then
                jsonStr &= """date_send"":""" & DataSend.ToString("yyyyMMddHHmm") &""","
            End If
            
            jsonStr &= """message"":" & "{" & _
                       messageJson & _
                       "}" & _
                       "}"

            Return jsonStr
        End Function
    End Class
End Namespace

