Imports Microsoft.VisualBasic

Namespace Models.Sms.Sending.Err

    Public Class SmsSendingErr
        Public Property [error] As Integer

        Public Sub New([error] As Integer)
            Me.[error] = [error]
        End Sub
    End Class

End Namespace