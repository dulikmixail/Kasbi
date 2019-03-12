Imports System.Reflection
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Quartz
Imports Quartz.Impl
Imports Service

Namespace Jobs
    Public Class EmailScheduler
        Inherits PageBase
        Const MinutesForCheckDillersAkt As Integer = 10

        Public Shared Async Sub Start()
            Try 
                Dim scheduler As IScheduler = Await StdSchedulerFactory.GetDefaultScheduler()
                Await scheduler.Start()
                Dim job As IJobDetail = JobBuilder.Create (Of EmailSender)().Build()
                Dim trigger As ITrigger =
                        TriggerBuilder.Create().WithIdentity("send_dillers_repair_akt", "email_group").StartNow().WithSimpleSchedule(
                            Function(x) x.WithIntervalInMinutes(MinutesForCheckDillersAkt).RepeatForever()).Build()
                Await scheduler.ScheduleJob(job, trigger)
            Catch ex As Exception
                ServiceLogger.Write(MethodBase.GetCurrentMethod(), ex.Message)
            End Try
        End Sub
    End Class

End Namespace
