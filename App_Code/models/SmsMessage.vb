Imports System.Diagnostics
Imports System.Web.Script.Serialization
Imports Microsoft.VisualBasic

Namespace Models
    Public Class SmsMessage
        Private Property DefaultMsg As SmsDefaultMsg
        Private Property Msgs As List(Of SmsMsg)
        Private Property MsgsAnswer As List(Of SmsMsgsAnswer)
            Get
                Return MsgsAnswer
            End Get
            Set(MsgsAnswer As List(Of SmsMsgsAnswer))
                Me.MsgsAnswer = MsgsAnswer
            End Set
        End Property

        Private Property StatusAnswer As List(Of SmsStatusAnswer)
            Get
                Return StatusAnswer
            End Get
            Set(StatusAnswer As List(Of SmsStatusAnswer))
                Me.StatusAnswer = StatusAnswer
            End Set
        End Property

        Public Sub New(defaultMsg As SmsDefaultMsg, msgs As List(Of SmsMsg))
            Me.DefaultMsg = defaultMsg
            Me.Msgs = msgs
        End Sub

        Public Overrides Function ToString() As String
            Dim defaultMsgJson As String = DefaultMsg.ToString()
            Dim msgsJson As String = String.Join(",", Msgs)

            Dim jsonStr As String = """default"":" &
                                    defaultMsgJson &
                                    "," &
                                    """msg"":" & "[" &
                                    msgsJson &
                                    "]"

            Return jsonStr
        End Function
    End Class
End Namespace