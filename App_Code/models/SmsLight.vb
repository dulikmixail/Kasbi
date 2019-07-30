Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsLight
        Public Property recipient() As String
        Public Property text() As String

        Public Sub New(recipient As String, text As String)
            Me.recipient = recipient
            Me.text = text
        End Sub
    End Class
End Namespace

