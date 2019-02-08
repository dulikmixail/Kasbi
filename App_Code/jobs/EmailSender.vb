Imports System.Web
Imports System.IO
Imports Kasbi
Imports GrapeCity.Documents
Imports GrapeCity.Documents.Word
Imports Microsoft.VisualBasic
Imports Quartz
Imports Service

Namespace Jobs
    Public Class EmailSender
        Inherits PageBase
        Implements IJob
        Dim ReadOnly _serviceEmail As ServiceEmail = New ServiceEmail()
        Dim ReadOnly _serviceDocuments As ServiceDocuments = New ServiceDocuments()
        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()

        Public Function Execute(context As IJobExecutionContext) As System.Threading.Tasks.Task Implements IJob.Execute
            SendAktDillers()
        End Function

        Public Sub SendAktDillers()
            Dim pathAkt As String = String.Empty
            Dim goodId As Integer = 0
            Dim customerId As Integer = 0
            Dim cashHistoryId As Integer = 0
            Dim ds As DataSet = GetEmailNotSend()
            'Dim doc As GcWordDocument
            Dim saveDocPath = String.Empty

            Dim bodyTextFormat = File.ReadAllText(Server.MapPath("Templates/Email/RepairAkt.html"))
            If ds.Tables(0).Rows.Count > 0
                For Each row As DataRow In ds.Tables(0).Rows
                    goodId = Convert.ToInt32(row("good_sys_id"))
                    customerId = Convert.ToInt32(row("customer_sys_id"))
                    cashHistoryId = Convert.ToInt32(row("hc_sys_id"))
                    pathAkt = _serviceDocuments.ProcessRepairRealizationAct(New Integer() {32}, customerId, goodId,
                                                                            cashHistoryId)
                    'doc = New GcWordDocument()
                    'doc.Load(pathAkt)
                    'saveDocPath = pathAkt & ".pdf"
                    'doc.SaveAsPdf(saveDocPath)
                    _serviceEmail.SendEmail(row("recipient").ToString(), row("email_subject").ToString(),
                                            String.Format(bodyTextFormat, row("email_text").ToString()),
                                            New String() {pathAkt})
                Next
            End If
        End Sub

        Private Function GetEmailNotSend() As DataSet
            Const cmd As String = "SELECT * FROM email_send WHERE is_send <> 1"
            Dim ds As DataSet = New DataSet()
            _sharedDbSql.GetDataAdapter(cmd).Fill(ds)
            Return ds
        End Function
    End Class
End Namespace