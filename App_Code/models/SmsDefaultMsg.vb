Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsDefaultMsg
        Protected Property Sender As String
        Protected Property ValidityPeriod As Integer
        Protected Property SmsText As String

        Public Sub New(sender As String, validityPeriod As Integer, smsText As String)
            Me.Sender = sender
            Me.ValidityPeriod = validityPeriod
            Me.SmsText = smsText
        End Sub

        Protected Function ToStringSeparated(separator As String) As String
            Dim jsonStr As String = ""
            If (Not Sender Is Nothing) Then
                jsonStr &= """sender"":""" & Sender & """" & separator
            End If
            If (ValidityPeriod > 0) Then
                jsonStr &= """validity_period"":""" & ValidityPeriod & """" & separator
            End If
            If (Not SmsText Is Nothing) Then
                jsonStr &= """sms_text"":""" & SmsText & """" & separator
            End If
            If (Not String.IsNullOrEmpty(jsonStr)) Then
                jsonStr = jsonStr.Substring(0, jsonStr.Length - 1)
            End If

            Return jsonStr
        End Function

        Public Overrides Function ToString() As String
            Return "{" &
                   ToStringSeparated(",") &
                   "}"
        End Function
    End Class
End Namespace
