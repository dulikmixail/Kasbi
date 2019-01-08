﻿Imports Kasbi
Imports Microsoft.VisualBasic
Imports Quartz
Imports Quartz.Impl

Namespace Jobs
    Public Class SmsScheduler
        Inherits PageBase
        Const MinutesForUpdateSmsStatus As Integer = 60

        Public Shared Async Sub Start()
            Dim scheduler As IScheduler = Await StdSchedulerFactory.GetDefaultScheduler()
            Await scheduler.Start()
            Dim job As IJobDetail = JobBuilder.Create (Of SmsStatusUpdater)().Build()
            Dim trigger As ITrigger =
                    TriggerBuilder.Create().WithIdentity("update_sms_status", "sms_group").StartNow().WithSimpleSchedule(
                        Function(x) x.WithIntervalInMinutes(MinutesForUpdateSmsStatus).RepeatForever()).Build()
            Await scheduler.ScheduleJob(job, trigger)

            job = JobBuilder.Create (Of SmsSender)().Build()
            'trigger = TriggerBuilder.Create().WithIdentity("send_sms_about_longtime_repair", "sms_group").StartNow().WithSimpleSchedule(
            '    Function(x) x.WithIntervalInMinutes(2).RepeatForever()).Build()
            trigger =
                TriggerBuilder.Create().WithIdentity("send_sms_about_longtime_repair", "sms_group").StartNow.
                    WithSchedule(CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(10, 10,
                                                                                      New DayOfWeek() _
                                                                                         {DayOfWeek.Monday,
                                                                                          DayOfWeek.Tuesday,
                                                                                          DayOfWeek.Wednesday,
                                                                                          DayOfWeek.Saturday,
                                                                                          DayOfWeek.Friday})).Build()
            Await scheduler.ScheduleJob(job, trigger)
        End Sub
    End Class
End Namespace

