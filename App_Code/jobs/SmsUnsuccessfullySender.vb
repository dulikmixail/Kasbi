Imports System.Threading.Tasks
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Quartz
Imports Service

Namespace Jobs
    Public Class SmsUnsuccessfullySender
        Inherits PageBase
        Implements IJob
        ReadOnly _serviceSms As ServiceSms = New ServiceSms()

        Public Function Execute(context As IJobExecutionContext) As Task Implements IJob.Execute
            Dim ds = _serviceSms.GetUnsuccessfullySentSms()
            Dim smsSendSysIds As List(Of Integer) = New List(Of Integer)()
            If ds.Tables.Count > 0
                For Each dr As DataRow In ds.Tables.Item(0).Rows
                    smsSendSysIds.Add(Convert.ToInt32(dr("sms_send_sys_id")))
                Next
                If smsSendSysIds.Count > 0
                    _serviceSms.SendManySmsWithUpdateSmsSend(smsSendSysIds.ToArray())
                End If
            End If
        End Function
    End Class
End Namespace

