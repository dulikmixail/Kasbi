Imports System.Threading.Tasks
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Quartz
Imports Service

Namespace Jobs
    Public Class SmsStatusUpdater
        Inherits PageBase
        Implements IJob
        ReadOnly _serviceSms As ServiceSms = New ServiceSms()

        Public Function Execute(ctx As IJobExecutionContext) As Task Implements IJob.Execute
            _serviceSms.UpdateStatuses(ServiceDbConnector.GetConnection())
            'dfsdfasdfsdf
        End Function
    End Class
End Namespace

