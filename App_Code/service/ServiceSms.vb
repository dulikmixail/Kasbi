Imports System.Data.SqlClient
Imports Exeption
Imports Kasbi
Imports Microsoft.Ajax.Utilities
Imports Microsoft.VisualBasic
Imports Models
Imports Models.Sms.Sending.Request
Imports Models.Sms.Sending.Response
Imports Models.Sms.Statusing.Request
Imports Models.Sms.Statusing.Response
Imports Newtonsoft.Json

Namespace Service
    Public Class ServiceSms
        Inherits ServiceExeption
        Implements IService
        Private ReadOnly _serviseHttp As ServiseHttp = New ServiseHttp()
        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()

        Const MaxRequestNumber As Integer = 50
        Const MaxNumbRepetitions As Integer = 3


        Private Const Login As String = "Ramok"
        Private Const Password As String = "B414Nv9p"
        Private Const Sender As String = "Ramok.by"
        Private Const ValidatyPeriod As Integer = 6

        Private ReadOnly _urls As List(Of Uri) = New List(Of Uri)() From {
            New Uri("https://userarea.sms-assistent.by/api/v1/send_sms/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/statuses/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/credits/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/xml"),
            New Uri("https://userarea.sms-assistent.by/api/v1/json")
            }


        Public Function SendManySmsWithDifferentText(phonesAndSmsTexts As Dictionary(Of String, String),
                                                     Optional defaultText As String =
                                                        "Ошибка: 101. Если вы получили это СМС. Сообщите этот код ошибки по телефону  8 (017) 213-67-00. Спасибо.",
                                                     Optional dataSend As DateTime = Nothing) As SmsSendingResponse
            Dim exeption = New SmsExeption()

            If (phonesAndSmsTexts.Count = 0) Then
                exeption.AddTextToList("Список с телефонами и тектом сообщения пустой")
            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
                Throw New Exception(exeption.GetAllTextString())
            Else
                Dim smsSendingRequest = New SmsSendingRequest(dataSend)
                smsSendingRequest.SetDefaultMessage(defaultText)

                For Each kvp In phonesAndSmsTexts
                    smsSendingRequest.AddMessage(kvp.Key, kvp.Value)
                Next

                Dim res As SmsSendingResponse =
                        JsonConvert.DeserializeObject (Of SmsSendingResponse)(
                            _serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                 JsonConvert _
                                                                    .
                                                                    SerializeObject(
                                                                        smsSendingRequest)))
                Return res

            End If
        End Function

        Public Function SendOneSms(phoneNumber As String, smsText As String, Optional dataSend As DateTime = Nothing) _
            As SmsSendingResponse
            Dim exeption = New SmsExeption()

            If String.IsNullOrEmpty(phoneNumber) Then
                exeption.AddTextToList("Не задан телефон")
            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
                Throw New Exception(exeption.GetAllTextString())
            Else
                Return SendSameSms(New List(Of String) From {phoneNumber}, smsText, dataSend)
            End If
        End Function

        Public Function SendSameSms(phoneNumbers As List(Of String), smsText As String,
                                    Optional dataSend As DateTime = Nothing) As SmsSendingResponse
            Dim smsSendingRequest As SmsSendingRequest
            Dim exeption = New SmsExeption()

            If (phoneNumbers.Count = 0) Then
                exeption.AddTextToList("Список с телефонами пустой")
            End If
            If (String.IsNullOrEmpty(smsText)) Then
                exeption.AddTextToList("Текс сообщения не найден")
            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
                Throw New Exception(exeption.GetAllTextString())
            Else
                smsSendingRequest = New SmsSendingRequest(dataSend)
                smsSendingRequest.SetDefaultMessage(smsText)

                For Each phoneNumber As String In phoneNumbers
                    smsSendingRequest.AddMessage(phoneNumber)
                Next
            End If

            Dim res As SmsSendingResponse =
                    JsonConvert.DeserializeObject (Of SmsSendingResponse)(_serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                                                               JsonConvert _
                                                                                                                  .
                                                                                                                  SerializeObject(
                                                                                                                      smsSendingRequest)))
            Return SmsSendingRepiter(smsSendingRequest, res)
        End Function

        Private Sub SendOneSmsWithInsertSmsHistory(phoneNumber As String, smsText As String,
                                                   executorId As Integer,
                                                   smsType As Integer,
                                                   Optional dataSend As DateTime = Nothing,
                                                   Optional cashHistoryId As Integer = 0,
                                                   Optional goodId As Integer = 0,
                                                   Optional customerId As Integer = 0)
            If Not String.IsNullOrEmpty(Trim(phoneNumber)) AND Trim(phoneNumber) <> "Нет номера"
                Dim smsSendSysId As Integer = 0

                smsSendSysId = IsertSmsHistory(phoneNumber, smsText, executorId, smsType, cashHistoryId, goodId,
                                               customerId)
                SendOneSmsWithUpdateSmsSend(smsSendSysId, phoneNumber, smsText, dataSend)
            End If
        End Sub

        Public Sub SendManySmsWithInsertSmsHistory(smsModels As SmsModel())
            Dim smsSendSysIds As List(Of Integer) = New List(Of Integer)
            For Each smsModel As SmsModel In smsModels
                smsSendSysIds.Add(IsertSmsHistory(smsModel.PhoneNumber, smsModel.SmsText, smsModel.ExecutorId,
                                                  smsModel.SmsType, smsModel.CashHistoryId, smsModel.GoodId,
                                                  smsModel.CustomerId))
            Next
            SendManySmsWithUpdateSmsSend(smsSendSysIds.ToArray())
        End Sub

        Private Function IsertSmsHistory(phoneNumber As String, smsText As String,
                                         executorId As Integer,
                                         smsType As Integer,
                                         Optional cashHistoryId As Integer = 0,
                                         Optional goodId As Integer = 0,
                                         Optional customerId As Integer = 0) As Integer
            Dim cmd1 As SqlCommand
            Dim smsSendSysId As Integer = 0
            If Not String.IsNullOrEmpty(Trim(phoneNumber)) AND Trim(phoneNumber) <> "Нет номера"
                Try
                    cmd1 = New SqlCommand("insert_sms_send")
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.AddWithValue("@pi_recipient", phoneNumber)
                    If cashHistoryId = 0
                        cmd1.Parameters.AddWithValue("@pi_hc_sys_id", DBNull.Value)
                    Else
                        cmd1.Parameters.AddWithValue("@pi_hc_sys_id", cashHistoryId)
                    End If
                    If goodId = 0
                        cmd1.Parameters.AddWithValue("@pi_good_sys_id", DBNull.Value)
                    Else
                        cmd1.Parameters.AddWithValue("@pi_good_sys_id", goodId)
                    End If
                    If customerId = 0
                        cmd1.Parameters.AddWithValue("@pi_customer_sys_id", DBNull.Value)
                    Else
                        cmd1.Parameters.AddWithValue("@pi_customer_sys_id", customerId)
                    End If
                    cmd1.Parameters.AddWithValue("@pi_validity_period", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_sms_text", smsText)
                    cmd1.Parameters.AddWithValue("@pi_sms_sys_id", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_error", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_executor", executorId)
                    cmd1.Parameters.AddWithValue("@pi_sms_type_sys_id", smsType)
                    cmd1.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                    _sharedDbSql.ExecuteScalar(cmd1)
                    smsSendSysId = Convert.ToInt32(cmd1.Parameters("@result").Value)
                Catch
                    Throw New Exception("Ошибка вставки данных об отправке СМС 2!<br>" & Err.Description)
                End Try
            End If
            Return smsSendSysId
        End Function

        Public Sub SendOneSmsWithInsertSmsHistoryForCashHistory(phoneNumber As String, smsText As String,
                                                                cashHistoryId As Integer, executorId As Integer,
                                                                smsType As Integer,
                                                                Optional dataSend As DateTime = Nothing)
            SendOneSmsWithInsertSmsHistory(phoneNumber, smsText, executorId, smsType, dataSend, cashHistoryId)
        End Sub

        Public Sub SendOneSmsWithInsertSmsHistoryForGood(phoneNumber As String, smsText As String,
                                                         goodId As Integer, executorId As Integer,
                                                         smsType As Integer,
                                                         Optional dataSend As DateTime = Nothing)
            SendOneSmsWithInsertSmsHistory(phoneNumber, smsText, executorId, smsType, dataSend, Nothing, goodId)
        End Sub

        Private Sub SendManySmsWithUpdateSmsSend(smsSendSysIds As Integer(),
                                                 Optional dataSend As DateTime = Nothing)
            Dim phonesAndSmsTexts As Dictionary(Of String, String) = New Dictionary(Of String,String)()
            Dim cmd As SqlCommand
            Dim adapt As SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rows As DataRowCollection
            If smsSendSysIds.Length > 0
                cmd = New SqlCommand("get_sms_send_by_ids")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sms_send_sys_ids", String.Join(";", smsSendSysIds))
                adapt = _sharedDbSql.GetDataAdapter(cmd)
                adapt.Fill(ds)
            End If

            If ds.Tables.Count > 0 And ds.Tables(0).Rows.Count > 0
                rows = ds.Tables(0).Rows
                For Each row In rows
                    phonesAndSmsTexts.Add(row("recipient").ToString(), row("sms_text").ToString())
                Next
            End If

            Dim smsSendingR As SmsSendingResponse = SendManySmsWithDifferentText(phonesAndSmsTexts, dataSend := dataSend)
            If Not IsNothing(smsSendingR)
                If smsSendSysIds.Length <> smsSendingR.message.msg.Length
                    Throw New Exception("Длинна запроса не соответствует длинне ответа<br>")
                End If
                Dim smsSendSysId As Integer
                Dim msg = smsSendingR.message.msg
                For i As Integer = 0 To smsSendSysIds.Length - 1
                    smsSendSysId = smsSendSysIds(i)
                    UpdateSmsSend(smsSendSysId, msg(i).sms_id, msg(i).error_code)
                Next
            End If
        End Sub

        Private Sub UpdateSmsSend(smsSendSysId As Integer, smsId As Integer, smsError As Integer)
            Dim cmd1 As SqlCommand
            Try
                cmd1 = New SqlCommand("update_sms_send")
                cmd1.CommandType = CommandType.StoredProcedure
                cmd1.Parameters.AddWithValue("@pi_sms_send_sys_id", smsSendSysId)
                cmd1.Parameters.AddWithValue("@pi_sms_sys_id", smsId)
                cmd1.Parameters.AddWithValue("@pi_error", smsError)
                _sharedDbSql.Execute(cmd1)
            Catch
                Throw New Exception("Ошибка обновления данных об отправке СМС 3!<br>" & Err.Description)
            End Try
        End Sub

        Private Sub SendOneSmsWithUpdateSmsSend(smsSendSysId As Integer, phoneNumber As String, smsText As String,
                                                Optional dataSend As DateTime = Nothing)
            If Not String.IsNullOrEmpty(Trim(phoneNumber)) AND Trim(phoneNumber) <> "Нет номера"
                Dim smsSendingR As SmsSendingResponse = SendOneSms(phoneNumber, smsText, dataSend)
                If Not IsNothing(smsSendingR)

                    Dim msg As Sms.Sending.Response.Msg = smsSendingR.message.msg(0)
                    UpdateSmsSend(smsSendSysId, msg.sms_id, msg.error_code)
                End If
            End If
        End Sub

        Public Function GetSmsStatusingByIds(smsIds As List(Of Integer)) As SmsStatusingResponse
            Dim smsStatusingRequest As SmsStatusingRequest
            If smsIds.Count <> 0 Then
                smsStatusingRequest = New SmsStatusingRequest()
                For Each smsId As Integer In smsIds
                    smsStatusingRequest.AddSmsId(smsId.ToString())
                Next
            Else
                Throw New Exception("Ошибка. Получение статуса СМС. Не найдены ID СМС")
            End If
            Return _
                JsonConvert.DeserializeObject (Of SmsStatusingResponse)(_serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                                                             JsonConvert _
                                                                                                                .
                                                                                                                SerializeObject(
                                                                                                                    smsStatusingRequest)))
        End Function

        Public Function GetSmsStatusingByJsonSmsSendingResponse(json As String) As SmsStatusingResponse
            Dim smsSendingResponse As SmsSendingResponse = JsonConvert.DeserializeObject (Of SmsSendingResponse)(json)
            Dim smsIds As List(Of Integer) = (From msg In smsSendingResponse.message.msg Select msg.sms_id).ToList()
            Return GetSmsStatusingByIds(smsIds)
        End Function

        Public Function GetSmsStatusingBySmsSendingResponse(smsSendingResponse As SmsSendingResponse) _
            As SmsStatusingResponse
            If smsSendingResponse IsNot Nothing
                Dim smsIds As List(Of Integer) = (From msg In smsSendingResponse.message.msg Select msg.sms_id).ToList()
                Return GetSmsStatusingByIds(smsIds)
            Else
                Return New SmsStatusingResponse()
            End If
        End Function

        Public Sub UpdateStatuses()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            cmd = New SqlClient.SqlCommand("get_sms_status_history_all_with_ignore_statuses")
            Dim todayDate As DateTime = Today
            cmd.Parameters.AddWithValue("@pi_start_update_date", todayDate)
            cmd.Parameters.AddWithValue("@pi_max_request_number", MaxRequestNumber)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            adapt = _sharedDbSql.GetDataAdapter(cmd)
            adapt.Fill(ds)

            ReadAndInsertStatuses(ds)
        End Sub

        Private Sub ReadAndInsertStatuses(ds As DataSet)
            Dim cmd = New SqlCommand()
            Dim smsStatusingResponse = New SmsStatusingResponse()
            Dim smsIds = New List(Of Integer)()

            For Each dr As DataRow In ds.Tables(0).Rows
                If Convert.ToInt32(dr("request_number")) < MaxRequestNumber
                    If Not IsDBNull(dr("sms_sys_id"))
                        smsIds.Add(Convert.ToInt32(dr("sms_sys_id")))
                    Else
                        If Not IsDBNull(dr("recipient")) And Not IsDBNull(dr("sms_text"))
                            SendOneSmsWithUpdateSmsSend(Convert.ToInt32(dr("sms_send_sys_id")), dr("recipient").ToString,
                                                        dr("sms_text").ToString)
                        End If
                    End If
                End If
            Next
            If smsIds.Count <> 0
                smsStatusingResponse = GetSmsStatusingByIds(smsIds)
                If Not smsStatusingResponse Is Nothing
                    For Each msg As Sms.Statusing.Response.Msg In smsStatusingResponse.status.msg
                        cmd = New SqlCommand("insert_or_update_sms_status_history")
                        With cmd.Parameters
                            .AddWithValue("@pi_sms_sys_id", msg.sms_id)
                            .AddWithValue("@pi_sms_count", msg.sms_count)
                            .AddWithValue("@pi_operator", msg.operator)
                            .AddWithValue("@pi_error_code", DBNull.Value)
                            .AddWithValue("@pi_sms_status", msg.sms_status)
                            .AddWithValue("@pi_recipient", msg.recipient)
                            .AddWithValue("@pi_do_increment_request_number", 1)
                        End With
                        cmd.CommandType = CommandType.StoredProcedure
                        _sharedDbSql.Execute(cmd)
                    Next
                End If
            End If
        End Sub

        Private Function SmsSendingRepiter(smsSendingRequest As SmsSendingRequest,
                                           smsSendingResponse As SmsSendingResponse) As SmsSendingResponse

            For i As Integer = 0 To MaxNumbRepetitions

                If Not IsNothing(smsSendingResponse)
                    Exit For
                Else
                    smsSendingResponse =
                        JsonConvert.DeserializeObject (Of SmsSendingResponse)(
                            _serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                 JsonConvert.SerializeObject(smsSendingRequest)))
                End If
            Next

            Return smsSendingResponse
        End Function
    End Class
End Namespace