Imports System.Data.SqlClient
Imports System.Threading.Tasks
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Models.Sms.Sending.Response
Imports Models.Sms.Statusing.Response
Imports Newtonsoft.Json
Imports Quartz
Imports Service

Namespace Jobs
    Public Class SmsSender
        Inherits PageBase
        Implements IJob

        Const DaysBeforeSendingSms = 7
        Const SmsTextForLongTimeSkno As String = "Ваше СКНО будет возвращено в РУП ИИЦ после 7 дней. Тел. +375291502047"
        Const SmsTextForLongTimeRepair As String = "Срочно заберите оборудование из ремонта. АКТ от {0} сформирован"
        ReadOnly _serviceSms As ServiceSms = New ServiceSms()

        Public Function Execute(ctx As IJobExecutionContext) As Task Implements IJob.Execute
        End Function

        Public Sub GetRepairInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim longTimeSkno As Dictionary(Of Integer, String) =
                    New Dictionary(Of Integer, String)
            Dim longTimeRepair As Dictionary(Of Integer, List(Of String)) =
                    New Dictionary(Of Integer, List(Of String))
            Dim repairDateOut As DateTime
            Dim stateRepair As Integer = 0
            Dim neadSkno As Boolean
            cmd = New SqlClient.SqlCommand("get_repair_master3")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds)

            For Each dr As DataRow In ds.Tables(0).Rows
                If Not IsDBNull(dr("state_repair")) And Not IsDBNull(dr("repairdate_out"))
                    stateRepair = Convert.ToInt32(dr("state_repair"))
                    repairDateOut = Date.Parse(dr("repairdate_out").ToString())
                    If IsDBNull(dr("nead_SKNO"))
                        neadSkno = False
                    Else
                        neadSkno = Convert.ToBoolean(dr("nead_SKNO"))
                    End If
                    If repairDateOut.AddDays(daysBeforeSendingSms) < Now
                        If Not IsDBNull(dr("tel_notice"))
                            If Not String.IsNullOrEmpty(dr("tel_notice").ToString())
                                If _
                                    stateRepair = 3 And neadSkno And
                                    Convert.ToInt32(dr("is_sms_sended_about_long_skno")) = 0
                                    longTimeSkno.Add(Convert.ToInt32(dr("sys_id")), dr("tel_notice").ToString())
                                ElseIf stateRepair = 3 And Convert.ToInt32(dr("is_sms_sended_about_long_repair")) = 0
                                    longTimeRepair.Add(Convert.ToInt32(dr("sys_id")),
                                                       New List(Of String) _
                                                          From {dr("tel_notice").ToString(),
                                                          dr("repairdate_out").ToString()})

                                End If
                            End If
                        End If
                    End If
                End If
            Next

            For Each kvp In longTimeSkno
                _serviceSms.SendOneSmsWithInsertSmsHistory(kvp.Value, SmsTextForLongTimeSkno, kvp.Key, 79, 4)
            Next

            For Each kvp In longTimeRepair
                _serviceSms.SendOneSmsWithInsertSmsHistory(kvp.Value(0),
                                                           String.Format(SmsTextForLongTimeRepair,
                                                                         CDate(kvp.Value(1)).ToString("dd.MM.yyyy")),
                                                           kvp.Key, 79, 5)
            Next


            'If Not String.IsNullOrEmpty(Trim(txtPhoneNumber.Text)) And cbxSmsSend.Checked And Trim(txtPhoneNumber.Text) <> "Нет номера"
            '    Dim smsText = txtSmsText.Text
            '    Dim cmd1 As SqlCommand
            '    Dim phoneNumber As String = txtPhoneNumber.Text
            '    Dim smsSendingR As SmsSendingResponse = _serviceSms.SendSameSms()
            '    Dim msgSendingR As Models.Sms.Sending.Response.Msg = smsSendingR.message.msg(0)

            '    Try
            '        cmd1 = New SqlCommand("insert_sms_send")
            '        cmd1.CommandType = CommandType.StoredProcedure
            '        cmd1.Parameters.AddWithValue("@pi_recipient", phoneNumber)
            '        cmd1.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
            '        cmd1.Parameters.AddWithValue("@pi_validity_period", DBNull.Value)
            '        cmd1.Parameters.AddWithValue("@pi_sms_text", smsText)
            '        cmd1.Parameters.AddWithValue("@pi_sms_sys_id", msgSendingR.sms_id)
            '        cmd1.Parameters.AddWithValue("@pi_error", DBNull.Value)
            '        cmd1.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
            '        cmd1.Parameters.AddWithValue("@pi_sms_type_sys_id", smsType)
            '        dbSQL.Execute(cmd1)
            '    Catch
            '        Console.WriteLine("Ошибка вставки данных об отправке СМС!<br>" & Err.Description)
            '        Exit Sub
            '    End Try
            '    System.Diagnostics.Debug.WriteLine(result)

            '    Dim smsStatusingR As SmsStatusingResponse =
            '            _serviceSms.GetSmsStatusingByJsonSmsSendingResponse(result)
            '    System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(smsStatusingR))

            '    Dim msgStatusingR As Models.Sms.Statusing.Response.Msg = smsStatusingR.status.msg(0)

            '    Try
            '        cmd1 = New SqlCommand("insert_or_update_sms_status_history")
            '        With cmd1.Parameters
            '            .AddWithValue("@pi_sms_sys_id", msgStatusingR.sms_id)
            '            .AddWithValue("@pi_sms_count", msgStatusingR.sms_count)
            '            .AddWithValue("@pi_operator", msgStatusingR.operator)
            '            .AddWithValue("@pi_error_code", DBNull.Value)
            '            .AddWithValue("@pi_sms_status", msgStatusingR.sms_status)
            '            .AddWithValue("@pi_recipient", msgStatusingR.recipient)
            '        End With
            '        cmd1.CommandType = CommandType.StoredProcedure
            '        dbSQL.Execute(cmd1)
            '    Catch
            '        Console.WriteLine("Ошибка вставки данных об статусе СМС!<br>" & Err.Description)
            '        Exit Sub
            '    End Try


            'End If
        End Sub
    End Class
End Namespace

