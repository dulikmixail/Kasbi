Imports exeption
Imports Kasbi

Namespace Service
    Public Class ServiceTo
        Inherits ServiceExeption
        Implements IService


        Public Function CheckCashHistoryItem(ByVal idGood As Integer, ByVal closePeriod As DateTime, ByVal closeDateText As String) As Boolean
            Dim exeption As ToExeption = New ToExeption(idGood, closePeriod.ToString(), closeDateText)

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
                                        exeption.AddTextToList("Закрываемый вами период уже закрыт.")
                                    End If
                                Case 6
                                    If closePeriod < enddate Then
                                        exeption.AddTextToList("Кассовый аппарат находиться на приостановке ТО.")
                                    End If
                                Case 2 To 3
                                    exeption.AddTextToList("Кассовый аппарат уже снят с ТО.")
                            End Select
                        End With
                    Else
                        exeption.AddTextToList("Нет постановки на ТО и договор не заключен.")
                    End If

                Catch
                    exeption.AddTextToList(Err.Description)
                End Try

            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
            End If

            Return Not HaveAnyExeption()

        End Function

        Public Function CheckDate(ByVal closePeriod As DateTime, ByVal closeDateText As String) As Boolean
            Dim exeption As ToExeption = New ToExeption(Nothing, closePeriod.ToString(), closeDateText)
            Dim dStarPeriod, dEndPeriod, dToday As DateTime
            Dim firstDayOfPeriod As Integer = 3

            dToday = Now
            'задаем начальный период
            dStarPeriod = dToday
            'задаем конечный период
            dEndPeriod = dStarPeriod.AddDays(7).AddMinutes(-1)
            'поправка на один дополнительный день для проведения ТО
            If (dToday.DayOfWeek = firstDayOfPeriod) Then
                dStarPeriod = dStarPeriod.AddDays(-1)
                dEndPeriod = dEndPeriod.AddDays(1)
            End If
            'ищем начало отчетного периода, в данном случае это Среда
            While (dStarPeriod.DayOfWeek <> firstDayOfPeriod)
                dStarPeriod = dStarPeriod.AddDays(-1)
            End While

            If String.IsNullOrEmpty(closeDateText) Then
                exeption.AddTextToList("Не выбрана дата выполнения.")
            Else
                Dim closeDate As DateTime = DateTime.Parse(closeDateText)
                If closePeriod <> New DateTime(closeDate.Year, closeDate.Month, 1) Then
                    exeption.AddTextToList("Дата закрытия периода и дата выполнения имеют разные месяца.")
                ElseIf closeDate > dToday Then
                    exeption.AddTextToList("Вы собираетесь провести ТО днем, который еще не наступил.")
                ElseIf closePeriod < New DateTime(dStarPeriod.Year, dStarPeriod.Month, 1) Then
                    exeption.AddTextToList("Закрываемый вами период уже прошел.")
                ElseIf closePeriod > New DateTime(dEndPeriod.Year, dEndPeriod.Month, 1) Then
                    exeption.AddTextToList("Закрываемый вами период еще не настал.")
                ElseIf (dStarPeriod > closeDate Or closeDate > dEndPeriod) Then
                    exeption.AddTextToList("Дата закрытия должна входить в отчетный период. Действующий отчетный период на данный момент с " & dStarPeriod.ToString("dd") & "." & dStarPeriod.ToString("MM") & "." & dStarPeriod.ToString("yy") & " по " & dEndPeriod.ToString("dd") & "." & dEndPeriod.ToString("MM") & "." & dEndPeriod.ToString("yy") & " включительно.")
                End If
            End If

            If exeption.HaveAnyText() Then
                AddExeption(exeption)
            End If

            Return Not exeption.HaveAnyText()
        End Function

        Public Function GetListStringGoodSysId() As String()
            Dim listExeption As List(Of IExeption) = GetListAllExeption()
            Dim list As String() = New String(listExeption.Count) {}
            Dim toExeption As ToExeption
            For j = 0 To listExeption.Count - 1
                toExeption = TryCast(listExeption(j), ToExeption)
                list(j) = toExeption.GoodSysId.ToString()
            Next
            Return list
        End Function

        Public Function GetExeptionTextByGoodId(goodSysId As Integer) As String
            Dim listExeption As List(Of IExeption) = GetListAllExeption()
            Dim list As String() = New String(listExeption.Count) {}
            Dim toExeption As ToExeption
            Dim i As Integer = 0
            For j = 0 To listExeption.Count - 1
                toExeption = TryCast(listExeption(j), ToExeption)
                If (toExeption.GoodSysId = goodSysId) Then
                    list(i) = toExeption.GetAllTextString()
                    i += 1
                End If
            Next
            Return String.Join(" ", list)
        End Function

    End Class

End Namespace