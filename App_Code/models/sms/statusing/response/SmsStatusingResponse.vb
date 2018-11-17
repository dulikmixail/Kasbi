Imports Microsoft.VisualBasic

Namespace Models.Sms.Statusing.Response
    Public Class SmsStatusingResponse
        Public Property status As Status
    End Class

    Public Class Status
        Public Property msg As List(Of Msg) = New List(Of Msg)
    End Class

    Public Class Msg
        Public Property sms_id As Integer
        Public Property sms_count As String
        Public Property [operator] As String
        Public Property sms_status As String
        Public Property recipient As String
    End Class
End Namespace
