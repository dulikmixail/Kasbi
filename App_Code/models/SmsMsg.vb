Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsMsg
        Inherits SmsDefaultMsg
        Private Property Recipient As String

        Public Sub New(sender As String, validityPeriod As Integer, smsText As String, recipient As String)
            MyBase.New(sender, validityPeriod, smsText)
            Me.Recipient = recipient
        End Sub
        Public Sub New(recipient As String)
            MyBase.New(Nothing, 0, Nothing)
            Me.Recipient = recipient
        End Sub
        Public Sub New(recipient As String, smsText As String)
            MyBase.New(Nothing, 0, smsText)
            Me.Recipient = recipient
        End Sub

        Public Overrides Function ToString() As String
            Dim jsonStr As String = "{" &
                                    """recipient"":""" & Recipient & ""","
            Dim myBaseStr = MyBase.ToStringSeparated(",")
            If (Not String.IsNullOrEmpty(myBaseStr)) Then
                jsonStr &= myBaseStr & ","
            End If

            jsonStr = jsonStr.Substring(0, jsonStr.Length - 1) & "}"

            Return jsonStr
        End Function
    End Class
End Namespace

