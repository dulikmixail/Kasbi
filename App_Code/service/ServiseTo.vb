Imports System.Collections.Generic
Imports Kasbi

Namespace service
    Public Class ServiceTo
        Inherits PageBase
        Private exeptionText As String = String.Empty
        Private listToExeption As New List(Of ToExeption)


        Public Function CheckCashHistoryItem(ByVal idGood As Integer, ByVal closePeriod As DateTime, ByVal closeDateText As String) As Boolean
            exeptionText = String.Empty
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            If CheckDate(closePeriod, closeDateText) Then
                Try
                    cmd = New SqlClient.SqlCommand("get_supportconduct_end_date")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_sys_id", idGood)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)

                    If ds.Tables(0).Rows.Count > 0 Then
                        With ds.Tables(0).DefaultView(0)
                            Dim enddate As Date = .Item("end_date")
                            Dim state As Integer = .Item("state")
                            Select Case state
                                Case 1
                                    If closePeriod < enddate Then
                                        exeptionText = "Закрываемый вами период прошел или уже закрыт"
                                    End If
                                Case 6
                                    If closePeriod < enddate Then
                                        exeptionText = "Кассовый аппарат находиться на приостановке ТО"
                                    End If
                                Case 2 To 3
                                    exeptionText = "Кассовый аппарат уже снят с ТО"
                            End Select
                        End With
                    End If

                Catch
                    exeptionText = Err.Description
                End Try

            End If
            If String.IsNullOrEmpty(exeptionText) Then
                Return True
            Else
                listToExeption.Add(New ToExeption(idGood, exeptionText, closePeriod.ToString(), closeDateText))
                Return False
            End If

        End Function

        Public Function CheckDate(ByVal closePeriod As DateTime, ByVal closeDateText As String) As Boolean
            exeptionText = String.Empty
            Dim dNow, dLastReportingDay As DateTime

            dNow = New Date(Now.Year, Now.Month, 1)

            dLastReportingDay = DateTime.Today
            While (dLastReportingDay.DayOfWeek <> 3)
                dLastReportingDay = dLastReportingDay.AddDays(-1)
            End While

            If String.IsNullOrEmpty(closeDateText) Then
                exeptionText = "Не выбрана дата выполнения"
            Else
                Dim closeDate As DateTime = DateTime.Parse(closeDateText)
                If (closePeriod > dNow) Then
                    exeptionText = "Закрываемый вами период больше текущего периода"
                ElseIf (closePeriod < dNow) Then
                    exeptionText = "Закрываемый вами период уже прошел. Проводить ТО можно только за текуший период (" & dNow.ToString("MMMM") & " " & dNow.ToString("yyyy") & ")"
                ElseIf (dLastReportingDay > closeDate Or closeDate > DateTime.Today) Then
                    exeptionText = "Дата закрытия должна входить в отчетный период. Действующий отчетный период на данный момент с " & dLastReportingDay.ToString("dd") & "." & dLastReportingDay.ToString("MM") & "." & dLastReportingDay.ToString("yy") & " по сегодняшний день"
                End If
            End If

            Return String.IsNullOrEmpty(exeptionText)
        End Function

        Public Function GetLastExeption() As String
            Return exeptionText
        End Function

        Public Function GetListToExeption() As List(Of ToExeption)
            Return listToExeption
        End Function
        Public Function GetListStringGoodSysId() As String()
            Dim list As String() = New String(listToExeption.Count) {}
            For j = 0 To listToExeption.Count - 1
                list(j) = listToExeption(j).goodSysId.ToString()
            Next
            Return list
        End Function

        Public Function GetExeptionTextByGoodId(goodSysId As Integer) As String
            For Each toExeption In listToExeption
                If toExeption.goodSysId = goodSysId Then
                    Return toExeption.text
                End If
            Next
            Return ""
        End Function

    End Class



End Namespace