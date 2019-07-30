Imports System.IO
Imports System.Reflection
Imports Kasbi
Imports Quartz
Imports Service

Namespace Jobs
    Public Class EmailSender
        Inherits PageBase
        Implements IJob
        Dim ReadOnly _serviceEmail As ServiceEmail = New ServiceEmail()
        Dim ReadOnly _serviceDocuments As ServiceDocuments = New ServiceDocuments()

        Public Function Execute(context As IJobExecutionContext) As System.Threading.Tasks.Task Implements IJob.Execute
            SendAktDillers()
        End Function

        Public Sub SendAktDillers()
            Try
                Dim pathAkt As String = String.Empty
                Dim emailSendId As Integer = 0
                Dim goodId As Integer = 0
                Dim customerId As Integer = 0
                Dim cashHistoryId As Integer = 0
                Dim ds As DataSet = GetEmailNotSend()
                If ds.Tables(0).Rows.Count > 0
                    Dim bodyTextFormat =
                            File.ReadAllText(Hosting.HostingEnvironment.MapPath("~/Templates") & "\Email\RepairAkt.html")
                    For Each row As DataRow In ds.Tables(0).Rows
                        emailSendId = Convert.ToInt32(row("email_send_sys_id"))
                        goodId = Convert.ToInt32(row("good_sys_id"))
                        customerId = Convert.ToInt32(row("customer_sys_id"))
                        cashHistoryId = Convert.ToInt32(row("hc_sys_id"))
                        pathAkt = _serviceDocuments.ProcessRepairRealizationAct(New Integer() {32}, customerId, goodId,
                                                                                cashHistoryId)

                        _serviceEmail.SendEmail(row("recipient").ToString(), row("email_subject").ToString(),
                                                String.Format(bodyTextFormat, row("email_text").ToString()),
                                                New String() {pathAkt})
                        Using con = ServiceDbConnector.GetSharedConnection()
                            con.Execute("UPDATE email_send SET is_send = 1 WHERE email_send_sys_id = " & emailSendId)
                        End Using

                    Next
                End If
            Catch ex As Exception
                ServiceLogger.Write(MethodBase.GetCurrentMethod(), ex.Message)
            End Try
            
        End Sub

        Private Function GetEmailNotSend() As DataSet
            Const cmd As String = "SELECT TOP 1 * FROM email_send WHERE is_send <> 1 ORDER BY email_send_sys_id"
            Dim ds As DataSet = New DataSet()
            Using con = ServiceDbConnector.GetSharedConnection()
                con.GetDataAdapter(cmd).Fill(ds)
            End Using
            Return ds
        End Function
    End Class
End Namespace