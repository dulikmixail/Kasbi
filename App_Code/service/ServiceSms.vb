﻿Imports System.Data.SqlClient
Imports Exeption
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
        Private serviseHttp As ServiseHttp = New ServiseHttp()

        Const MaxRequestNumber As Integer = 50

        Private Const Login As String = "Ramok"
        Private Const Password As String = "B414Nv9p"
        Private Const Sender As String = "Ramok.by"
        Private Const ValidatyPeriod As Integer = 6

        Private _urls As List(Of Uri) = New List(Of Uri)() From {
            New Uri("https://userarea.sms-assistent.by/api/v1/send_sms/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/statuses/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/credits/plain"),
            New Uri("https://userarea.sms-assistent.by/api/v1/xml"),
            New Uri("https://userarea.sms-assistent.by/api/v1/json")
            }


        Public Function SendManySmsWithDifferentText(phonesAndSmsTexts As Dictionary(Of String, String),
                                                     Optional defaultText As String =
                                                        "Ошибка: 101. Если вы получили это СМС. Сообщите этот код ошибки по телефону  8 (017) 213-67-00",
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
                            serviseHttp.SendRequestPostJsonUtf8(_urls(4),
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
                    JsonConvert.DeserializeObject (Of SmsSendingResponse)(serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                                                              JsonConvert _
                                                                                                                 .
                                                                                                                 SerializeObject(
                                                                                                                     smsSendingRequest)))
            Return res
        End Function

        Public Sub SendOneSmsWithInsertSmsHistory(phoneNumber As String, smsText As String, cashHistoryId As Integer, executorId As Integer, smsType As Integer,
                                                  Optional dataSend As DateTime = Nothing)
            If Not String.IsNullOrEmpty(Trim(phoneNumber)) AND Trim(phoneNumber) <> "Нет номера"
                Dim cmd1 As SqlCommand
                Dim smsSendSysId As Integer = 0

                Try
                    cmd1 = New SqlCommand("insert_sms_send")
                    cmd1.CommandType = CommandType.StoredProcedure
                    cmd1.Parameters.AddWithValue("@pi_recipient", phoneNumber)
                    cmd1.Parameters.AddWithValue("@pi_hc_sys_id", cashHistoryId)
                    cmd1.Parameters.AddWithValue("@pi_validity_period", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_sms_text", smsText)
                    cmd1.Parameters.AddWithValue("@pi_sms_sys_id", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_error", DBNull.Value)
                    cmd1.Parameters.AddWithValue("@pi_executor", executorId)
                    cmd1.Parameters.AddWithValue("@pi_sms_type_sys_id", smsType)
                    cmd1.Parameters.Add("@result", SqlDbType.Int).Direction = ParameterDirection.ReturnValue
                    dbSQL.ExecuteScalar(cmd1)
                    smsSendSysId = Convert.ToInt32(cmd1.Parameters("@result").Value)
                Catch
                    Throw New Exception("Ошибка вставки данных об отправке СМС 2!<br>" & Err.Description)
                End Try
                SendOneSmsWithUpdateSmsSend(smsSendSysId,phoneNumber,smsText, dataSend)
            End If
        End Sub

        Private Sub SendOneSmsWithUpdateSmsSend(smsSendSysId As Integer, phoneNumber As String, smsText As String, Optional dataSend As DateTime = Nothing)
            If Not String.IsNullOrEmpty(Trim(phoneNumber)) AND Trim(phoneNumber) <> "Нет номера"
                Dim cmd1 As SqlCommand
                Dim smsSendingR As SmsSendingResponse = SendOneSms(phoneNumber, smsText, dataSend)
                If Not IsNothing(smsSendingR)
                    Dim msgSendingR As Sms.Sending.Response.Msg = smsSendingR.message.msg(0)
                    If smsSendSysId > 0
                        Try
                            cmd1 = New SqlCommand("update_sms_send")
                            cmd1.CommandType = CommandType.StoredProcedure
                            cmd1.Parameters.AddWithValue("@pi_sms_send_sys_id", smsSendSysId)
                            cmd1.Parameters.AddWithValue("@pi_sms_sys_id", msgSendingR.sms_id)
                            dbSQL.Execute(cmd1)
                        Catch
                            Throw New Exception("Ошибка обновления данных об отправке СМС 3!<br>" & Err.Description)
                        End Try

                    End If
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
                JsonConvert.DeserializeObject (Of SmsStatusingResponse)(serviseHttp.SendRequestPostJsonUtf8(_urls(4),
                                                                                                            JsonConvert.
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
            adapt = dbSQL.GetDataAdapter(cmd)
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
                        SendOneSmsWithUpdateSmsSend(Convert.ToInt32(dr("sms_send_sys_id")), dr("recipient").ToString, dr("sms_text").ToString)
                    End If
                    End If
                End If
            Next
            If smsIds.Count <> 0
                smsStatusingResponse = GetSmsStatusingByIds(smsIds)
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
                    dbSQL.Execute(cmd)
                Next
            End If
        End Sub
    End Class
End Namespace