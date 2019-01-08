Imports System.Web.Http.ModelBinding.Binders
Imports Kasbi
Imports Quartz
Imports Service

Namespace Jobs
    Public Class SmsSender
        Inherits PageBase
        Implements IJob

        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()
        Const DaysBeforeSendingSknoSms = 7
        Const DaysBeforeSendingSmsRepair = 7

        Const SmsTextForLongTimeSkno As String =
            "Вам необходимо установить СКНО для ККМ №{0}, иначе оно будет возвращено в РУП ИИЦ после 7 дней. Тел. +375291502047"

        Const SmsTextForLongTimeRepair As String = "Срочно заберите ККМ №{0} из ремонта. АКТ от {1} сформирован"
        ReadOnly _serviceSms As ServiceSms = New ServiceSms()

        Public Function Execute(ctx As IJobExecutionContext) As System.Threading.Tasks.Task Implements IJob.Execute
            SendSmses()
        End Function

        Public Sub SendSmses()
            For Each kvp In FindLongTimeSkno()
                _serviceSms.SendOneSmsWithInsertSmsHistoryForGood(kvp.Value(0),
                                                                  String.Format(SmsTextForLongTimeSkno, kvp.Value(1)),
                                                                  kvp.Key, 79, 4)
            Next
            For Each kvp In FindLongTimeRepair()
                _serviceSms.SendOneSmsWithInsertSmsHistoryForCashHistory(kvp.Value(0),
                                                                         String.Format(SmsTextForLongTimeRepair,
                                                                                       kvp.Value(1),
                                                                                       CDate(kvp.Value(2)).ToString(
                                                                                           "dd.MM.yyyy")),
                                                                         kvp.Key, 79, 5)
            Next
        End Sub

        Public Function FindLongTimeSkno() As Dictionary(Of Integer, List(Of String))
            Dim longTimeSkno As Dictionary(Of Integer, List(Of String)) =
                    New Dictionary(Of Integer, List(Of String))
            Dim adapt As SqlClient.SqlDataAdapter = _sharedDbSql.GetDataAdapter("get_good_with_long_time_skno", True)
            Dim ds As DataSet = New DataSet
            adapt.Fill(ds)

            For Each dr As DataRow In ds.Tables(0).Rows
                Dim sknoReceivedUpdateDate = Date.Parse(dr("skno_received_update_date").ToString())
                If sknoReceivedUpdateDate.AddDays(DaysBeforeSendingSknoSms) < Now() And Not IsDBNull(dr("tel_notice"))
                    If Convert.ToInt32(dr("count_sms_with_type_4")) = 0
                        longTimeSkno.Add(Convert.ToInt32(dr("good_sys_id")),
                                         New List(Of String) From {dr("tel_notice"), Trim(dr("num_cashregister").ToString())})
                    End If
                End If
            Next
            Return longTimeSkno
        End Function

        Public Function FindLongTimeRepair() As Dictionary(Of Integer, List(Of String))
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim longTimeRepair As Dictionary(Of Integer, List(Of String)) =
                    New Dictionary(Of Integer, List(Of String))
            Dim repairDateOut As DateTime
            Dim stateRepair As Integer = 0
            cmd = New SqlClient.SqlCommand("get_repair_with_long_time")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            adapt = _sharedDbSql.GetDataAdapter(cmd)
            adapt.Fill(ds)

            For Each dr As DataRow In ds.Tables(0).Rows
                If Not IsDBNull(dr("state_repair")) And Not IsDBNull(dr("repairdate_out"))
                    stateRepair = Convert.ToInt32(dr("state_repair"))
                    repairDateOut = Date.Parse(dr("repairdate_out").ToString())
                    If repairDateOut.AddDays(DaysBeforeSendingSmsRepair) < Now()
                        If Not IsDBNull(dr("tel_notice"))
                            If Not String.IsNullOrEmpty(dr("tel_notice").ToString())
                                If _
                                    (stateRepair = 3 Or stateRepair = 33) And
                                    Convert.ToInt32(dr("is_sms_sended_about_long_repair")) = 0
                                    longTimeRepair.Add(Convert.ToInt32(dr("sys_id")),
                                                       New List(Of String) _
                                                          From {dr("tel_notice").ToString(),
                                                          Trim(dr("num_cashregister").ToString()),
                                                          dr("repairdate_out").ToString()})

                                End If
                            End If
                        End If
                    End If
                End If
            Next
            Return longTimeRepair
        End Function
    End Class
End Namespace

