Imports System.Collections.Generic
Imports System.Runtime.InteropServices
Imports System.Web.UI.WebControls.Expressions
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
                                    If closePeriod = enddate.AddMonths(-1) Then
                                        exeptionText = "Закрываемый вами период уже закрыт"
                                    End If
                                Case 6
                                    If closePeriod < enddate Then
                                        exeptionText = "Кассовый аппарат находиться на приостановке ТО"
                                    End If
                                Case 2 To 3
                                    exeptionText = "Кассовый аппарат уже снят с ТО"
                            End Select
                        End With
                    Else
                        exeptionText = "Нет постановки на ТО и договор не заключен"
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
            Dim dNow, dStarPeriod, dEndPeriod, dToday As DateTime
            Dim firstDayOfPeriod As Integer = 3
            dNow = New Date(Now.Year, Now.Month, 1)


            dToday = DateTime.Today
            dStarPeriod = dToday

            'поправка на один дополнительный день для проведения ТО
            If (dToday.DayOfWeek = firstDayOfPeriod) Then
                dStarPeriod = dStarPeriod.AddDays(-1)
            End If
            'ищем начало отчетного периода, в данном случае это Ср
            While (dStarPeriod.DayOfWeek <> firstDayOfPeriod)
                dStarPeriod = dStarPeriod.AddDays(-1)
            End While
            'задаем конечный период
            dEndPeriod = dStarPeriod.AddDays(7).AddMinutes(-1)
            'поправка на один дополнительный день для проведения ТО
            If (dToday.DayOfWeek = firstDayOfPeriod) Then
                dEndPeriod = dEndPeriod.AddDays(1)
            End If


            If String.IsNullOrEmpty(closeDateText) Then
                exeptionText = "Не выбрана дата выполнения"
            Else
                Dim closeDate As DateTime = DateTime.Parse(closeDateText)

                'If (closePeriod > dNow) Then
                '    exeptionText = "Закрываемый вами период больше текущего периода"
                'ElseIf (closePeriod < dNow) Then
                '    exeptionText = "Закрываемый вами период уже прошел. Проводить ТО можно только за текуший период (" & dNow.ToString("MMMM") & " " & dNow.ToString("yyyy") & ")"
                If closePeriod <> New DateTime(closeDate.Year, closeDate.Month, 1) Then
                    exeptionText = "Дата закрытия периода и дата выполнения имеют разные месяца"
                ElseIf closeDate > dToday Then
                    exeptionText = "Вы собираетесь провести ТО днем, который еще не наступил."
                ElseIf closePeriod < New DateTime(dStarPeriod.Year, dStarPeriod.Month, 1) Then
                    exeptionText = "Закрываемый вами период уже прошел"
                ElseIf closePeriod > New DateTime(dEndPeriod.Year, dEndPeriod.Month, 1) Then
                    exeptionText = "Закрываемый вами период еще не настал"
                ElseIf (dStarPeriod > closeDate Or closeDate > dEndPeriod) Then
                    exeptionText = "Дата закрытия должна входить в отчетный период. Действующий отчетный период на данный момент с " & dStarPeriod.ToString("dd") & "." & dStarPeriod.ToString("MM") & "." & dStarPeriod.ToString("yy") & " по " & dEndPeriod.ToString("dd") & "." & dEndPeriod.ToString("MM") & "." & dEndPeriod.ToString("yy") & " включительно."
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