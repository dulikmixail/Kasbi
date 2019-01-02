Imports Kasbi
Imports Microsoft.VisualBasic
Imports Quartz
Imports Quartz.Impl

Namespace Jobs
    Public Class SmsScheduler
        Inherits PageBase
        Const MinutesForUpdateSmsStatus As Integer = 30
        Const HoursCheckForFindLongTimeRepair As Integer = 1
        Public Shared Async Sub Start()
            Dim scheduler As IScheduler = Await StdSchedulerFactory.GetDefaultScheduler()
            Await scheduler.Start()
            Dim job As IJobDetail = JobBuilder.Create(Of SmsStatusUpdater)().Build()
            Dim trigger As ITrigger = TriggerBuilder.Create().WithIdentity("update_sms_status", "sms_group").StartNow().WithSimpleSchedule(Function(x) x.WithIntervalInMinutes(MinutesForUpdateSmsStatus).RepeatForever()).Build()
            Await scheduler.ScheduleJob(job, trigger)

            'job  = JobBuilder.Create(Of SmsStatusUpdater)().Build()
            'trigger = TriggerBuilder.Create().WithIdentity("send_sms_about_longtime_repair", "sms_group").StartNow().WithSimpleSchedule(Function(x) x.WithIntervalInHours(HoursCheckForFindLongTimeRepair).RepeatForever()).Build()
            'Await scheduler.ScheduleJob(job, trigger)

        End Sub
    End Class
End Namespace

