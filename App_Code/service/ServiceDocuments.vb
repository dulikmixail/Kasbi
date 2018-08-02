Imports Kasbi
Imports Microsoft.Office.Interop.Word
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Service
    Public Class ServiceDocuments
        Inherits PageBase

        ReadOnly _monthReplacements1 As Hashtable = New Hashtable()
        ReadOnly _monthReplacements2 As Hashtable = New Hashtable()

        Private _wrdApp As Microsoft.Office.Interop.Word.ApplicationClass
        Private _wrdDoc As Microsoft.Office.Interop.Word.DocumentClass

        Public Sub New()
            _monthReplacements1.Add("01", "января")
            _monthReplacements1.Add("02", "февраля")
            _monthReplacements1.Add("03", "марта")
            _monthReplacements1.Add("04", "апреля")
            _monthReplacements1.Add("05", "мая")
            _monthReplacements1.Add("06", "июня")
            _monthReplacements1.Add("07", "июля")
            _monthReplacements1.Add("08", "августа")
            _monthReplacements1.Add("09", "сентября")
            _monthReplacements1.Add("10", "октября")
            _monthReplacements1.Add("11", "ноября")
            _monthReplacements1.Add("12", "декабря")

            _monthReplacements2.Add("01", "январе")
            _monthReplacements2.Add("02", "феврале")
            _monthReplacements2.Add("03", "марте")
            _monthReplacements2.Add("04", "апреле")
            _monthReplacements2.Add("05", "мае")
            _monthReplacements2.Add("06", "июне")
            _monthReplacements2.Add("07", "июле")
            _monthReplacements2.Add("08", "августе")
            _monthReplacements2.Add("09", "сентябре")
            _monthReplacements2.Add("10", "октябре")
            _monthReplacements2.Add("11", "ноябре")
            _monthReplacements2.Add("12", "декабре")
        End Sub


        Public Function AktForTOandDolg(checkGoods As Hashtable, Optional withDate As Boolean = True, Optional dateAkt As DateTime = Nothing) As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet


            Dim docPath, savePath, filter As String
            Dim drs() As Data.DataRow

            If dateAkt = Nothing Then
                dateAkt = DateTime.Today
            End If


            docPath = Server.MapPath("Templates\") & "Akt_Of_TO_And_Dolg.doc"
            savePath = Server.MapPath("Docs") & "\Akts\" & Session("User").sys_id.ToString & "\" & "Akt_Of_TO_And_Dolg.doc"
            CopyFile(docPath, savePath, overwrite:=True)
            Try
                _wrdApp = New ApplicationClass()
                _wrdApp.Caption = "Caption1"
                _wrdApp.DisplayAlerts = WdAlertLevel.wdAlertsNone
                _wrdApp.Visible = False

                _wrdDoc = _wrdApp.Documents.Open(savePath)
                With _wrdDoc.Sections(1).Range
                    .Select()
                    .Copy()
                End With

                filter = " "
                If checkGoods.Count <> 0 Then
                    Dim stringKeys As String = ""
                    For Each key In checkGoods.Keys
                        stringKeys &= key.ToString() & ","
                    Next
                    stringKeys = stringKeys.Remove(stringKeys.Length - 1, 1)
                    filter = " WHERE good.good_sys_id IN (" & stringKeys & ") "
                End If

                cmd = New SqlClient.SqlCommand("prc_rpt_AktsTOAndDolg")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_filter", filter)

                cmd.CommandTimeout = 0

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                drs = ds.Tables(0).Select()

                For k As Integer = 0 To drs.Length - 1

                    'If k Mod 2 = 0 Then
                    paste_Part1(drs, k, withDate, dateAkt, checkGoods)
                    'Else
                    paste_Part2(drs, k, withDate, dateAkt, checkGoods)
                    empty_Part2()
                    If k <> (drs.Length - 1) Then
                        With _wrdDoc.Sections(1).Range
                            .Collapse(WdCollapseDirection.wdCollapseEnd)
                            .InsertBreak(WdBreakType.wdPageBreak)
                            .Collapse(WdCollapseDirection.wdCollapseEnd)
                            .Paste()
                        End With
                    End If
                    'End If
                Next
                If drs.Length Mod 2 = 1 Then
                    clear_Part2()
                End If

                _wrdDoc.Close(True)
                _wrdApp.Quit()


            Catch ex As Exception
                _wrdDoc.Close(True)
                _wrdApp.Quit()
            End Try

            Return savePath
        End Function

        Private Sub paste_Part1(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime, checkGoods As Hashtable)
            If withDate Then
                _wrdDoc.Bookmarks("Day1").Range.Text = dateAkt.ToString("dd")
            End If
            _wrdDoc.Bookmarks("Counter1").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt1").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month1_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("Month1_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("NumDog1").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works1").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash1").Range.Text = drs(k).Item(2).ToString().Trim()
            paste_Summa_TO(drs, k, 1)
            _wrdDoc.Bookmarks("DateDolg1").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg1").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer1").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master1").Range.Text = CurrentUser.Name
        End Sub

        Private Sub paste_Part2(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime, checkGoods As Hashtable)
            If withDate Then
                _wrdDoc.Bookmarks("Day2").Range.Text = dateAkt.ToString("dd")
            End If
            _wrdDoc.Bookmarks("Counter2").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt2").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month2_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("Month2_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM"))
            _wrdDoc.Bookmarks("NumDog2").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works2").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash2").Range.Text = drs(k).Item(2).ToString().Trim()
            paste_Summa_TO(drs, k, 2)
            _wrdDoc.Bookmarks("DateDolg2").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg2").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer2").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master2").Range.Text = CurrentUser.Name
        End Sub

        Private Sub paste_Summa_TO(drs() As Data.DataRow, k As Integer, num As Integer)
            If InStr(drs(k).Item(6).ToString(), "ТО1") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text = "15.00 (пятнадцать) рублей 00 копеек, в т. ч. НДС (20%) 2.50 (два рубля 50 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО2") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text = "7.00 (семь) рублей 50 копеек, в т. ч. НДС (20%) 1.25 (один рубль 25 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО3") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text = "9.00 (девять) рублей 60 копеек, в т. ч. НДС (20%) 1.60 (один рубль 60 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО4") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text = "24.00 (двадцать четыре) рубля 00 копеек, в т. ч. НДС (20%) 4.00 (четыре рубля 00 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО5") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text = "6.00 (шесть) рублей 00 копеек, в т. ч. НДС (20%) 1.00 (один рубль 00 копеек)."
            End If
        End Sub
        Private Sub clear_Part2()
            _wrdDoc.Bookmarks("Akt2_body").Range.Text = " "
        End Sub
        Private Sub empty_Part2()
            _wrdDoc.Bookmarks("Akt2_body").Delete()
        End Sub

        '        Private Function ProcessRepairRealizationAct(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal cash As Integer, ByVal history As Integer) As Boolean
        '            Dim cmd As SqlClient.SqlCommand
        '            Dim adapt As SqlClient.SqlDataAdapter
        '            Dim ds As DataSet
        '            Dim r1 As Row
        '            Dim i%
        '            Dim Cost, NDS, TotalSum, NormaHour, Quantity As Double
        '            Dim q, p, dNDS, dTotal, sSum, sNDS, CPP As Double
        '            Dim DocName32 As String

        '            DocName32 = "Akt_RepairRealization.doc"

        '            Cost = 0
        '            NDS = 0
        '            TotalSum = 0
        '            NormaHour = 0
        '            Quantity = 1

        '            ds = New DataSet
        '            cmd = New SqlClient.SqlCommand("prc_rpt_RepairRealizationAct_New")
        '            cmd.CommandType = CommandType.StoredProcedure
        '            cmd.Parameters.AddWithValue("@pi_good_sys_id", cash)
        '            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
        '            cmd.Parameters.AddWithValue("@pi_hc_sys_id", history)
        '            adapt = dbSQL.GetDataAdapter(cmd)
        '            adapt.Fill(ds, "RepairRealizationAct")

        '            cmd = New SqlClient.SqlCommand("get_repair_info")
        '            cmd.Parameters.AddWithValue("@pi_hc_sys_id", history)
        '            cmd.CommandType = CommandType.StoredProcedure
        '            adapt = dbSQL.GetDataAdapter(cmd)
        '            If Not ds.Tables("details") Is Nothing Then
        '                ds.Tables("details").Clear()
        '            End If
        '            adapt.Fill(ds, "details")

        '            If ds.Tables("details").Rows.Count = 0 Then GoTo ExitFunction
        '            Dim boos_name$, customer_name$, accountant$, unn$, registration$, sDate$, dogovor$

        '            cmd = New SqlClient.SqlCommand("get_customer_info")

        '            cmd.CommandType = CommandType.StoredProcedure
        '            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
        '            adapt = dbSQL.GetDataAdapter(cmd)
        '            If Not ds.Tables("customer") Is Nothing Then
        '                ds.Tables("customer").Clear()
        '            End If
        '            adapt.Fill(ds, "customer")

        '            If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
        '            Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
        '            If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
        '                sEmployee = ""
        '            End If

        '            Dim act_num$ = ds.Tables("RepairRealizationAct").Rows(0)("akt")
        '            Dim WorkNotCall% = IIf(IsDBNull(ds.Tables("RepairRealizationAct").Rows(0)("workNotCall")), 0, ds.Tables("RepairRealizationAct").Rows(0)("workNotCall"))

        '            Dim docFullPath$
        '            Dim path$ = Server.MapPath("Docs\")

        '            Dim fls As IO.File
        '            Dim fl As IO.FileInfo
        '            ProcessRepairRealizationAct = True

        '            Try
        '                ' Create instance of Word!
        '                _wrdApp = New ApplicationClass()
        '            Catch ex As Exception
        '                WriteError(Err.Description & "<br>" & ex.ToString)
        '                ProcessRepairRealizationAct = False
        '                GoTo ExitFunction
        '            End Try

        '            If num_doc(0) = 32 Then
        '                Try
        '                    MkDir(path & "repair\temp\")
        '                Catch ex As Exception
        '                End Try
        '                Try
        '                    docFullPath = path & "repair\temp\" & DocName32
        '                    fl = New IO.FileInfo(docFullPath)

        '                    If Not fl.Exists() Then
        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If
        '                        Try
        '                            'Create folders and copy templates
        '                            Dim fldr As New IO.DirectoryInfo(path)
        '                            If Not fldr.Exists Then
        '                                fldr.Create()
        '                            End If

        '                        Catch ex As Exception
        '                            WriteError(Err.Description & "<BR>" & ex.ToString)
        '                            ProcessRepairRealizationAct = False
        '                            Exit Function
        '                        End Try
        '                    End If
        '                    IO.File.Copy(Server.MapPath("Templates/") & DocName32, docFullPath, True)

        '                    _wrdDoc = _wrdApp.Documents.Open(docFullPath)
        '                    _wrdDoc.Bookmarks("act_number1").Range.Text = act_num
        '                    _wrdDoc.Bookmarks("num_cashregister1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
        '                    ' doc.Bookmarks("num_cashregister2").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
        '                    _wrdDoc.Bookmarks("good_name1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("good_name")
        '                    'doc.Bookmarks("good_type2").Range.Text = ds.Tables("defectAct").Rows(0)("good_name")
        '                    _wrdDoc.Bookmarks("customer1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("customer_name")
        '                    '  doc.Bookmarks("customer2").Range.Text = ds.Tables("defectAct").Rows(0)("customer_name")
        '                    'кто сделал все это

        '                    _wrdDoc.Bookmarks("master1").Range.Text = sEmployee
        '                    _wrdDoc.Bookmarks("work_type").Range.Text = dbSQL.ExecuteScalar("select ISNULL(work_type, '') from employee where sys_id =" & CStr(CurrentUser.sys_id)).ToString()

        '                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
        '                    sDate = ""
        '                    _wrdDoc.Bookmarks("dogovor1").Range.Text = dogovor '& " от " & sDate
        '                    Try
        '                        _wrdDoc.Bookmarks("sale_date1").Range.Text = GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))
        '                    Catch
        '                        _wrdDoc.Bookmarks("sale_date1").Range.Text = "-"
        '                    End Try
        '                    Dim rdate_in As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_in"))
        '                    Dim rdate_out As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_out"))
        '                    Dim sRepairDates$ = GetRussianDate(rdate_in) & " " & rdate_in.ToString("HH:mm") & " - " & GetRussianDate(rdate_out)
        '                    _wrdDoc.Bookmarks("repair_date1").Range.Text = sRepairDates

        '                    Dim a As Integer = 0
        '                    Dim ndsItem As Double = 0

        '                    For i = 0 To ds.Tables("details").Rows.Count - 1

        '                        If ds.Tables("details").Rows(i)("is_detail") = True Then
        '                            ' детали
        '                            r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
        '                            a = a + 1
        '                            r1.Cells(1).Range.Text = a
        '                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
        '                            Quantity = ds.Tables("details").Rows(i)("quantity")
        '                            r1.Cells(3).Range.Text = Quantity
        '                            r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2)))
        '                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2)))
        '                            ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2) * 1)
        '                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
        '                            r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1))
        '                            r1.Cells(8).Range.Text = String.Format("{0:0.00}", (IIf(WorkNotCall = 1, Quantity * ds.Tables("details").Rows(i)("norma_hour"), "0")))

        '                            Cost = Cost + Math.Round(Quantity * ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2) * 1
        '                            NDS = NDS + ndsItem
        '                            TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1

        '                            ' замена  детали
        '                            If ds.Tables("details").Rows(i)("cost_service") > 0 Then
        '                                NormaHour = NormaHour + Quantity * ds.Tables("details").Rows(i)("norma_hour")
        '                                If WorkNotCall = 0 Then
        '                                    r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
        '                                    a = a + 1
        '                                    r1.Cells(1).Range.Text = a
        '                                    r1.Cells(2).Range.Text = "Замена " & ds.Tables("details").Rows(i)("name")
        '                                    r1.Cells(3).Range.Text = ""
        '                                    r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1))
        '                                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1))
        '                                    ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1)
        '                                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
        '                                    r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1))
        '                                    r1.Cells(8).Range.Text = String.Format("{0:0.00}", (Quantity * ds.Tables("details").Rows(i)("norma_hour")))
        '                                    Cost = Cost + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1
        '                                    NDS = NDS + ndsItem
        '                                    TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1
        '                                End If
        '                            End If
        '                        Else
        '                            If WorkNotCall = 0 Then
        '                                r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
        '                                a = a + 1
        '                                r1.Cells(1).Range.Text = a
        '                                r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
        '                                r1.Cells(3).Range.Text = ""
        '                                Quantity = ds.Tables("details").Rows(i)("quantity")
        '                                r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2)))
        '                                r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2)))

        '                                ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1)
        '                                r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
        '                                r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2)))
        '                                r1.Cells(8).Range.Text = String.Format("{0:0.00}", (Quantity * ds.Tables("details").Rows(i)("norma_hour")))
        '                                NormaHour = NormaHour + Quantity * ds.Tables("details").Rows(i)("norma_hour")
        '                                Cost = Cost + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1
        '                                NDS = NDS + ndsItem
        '                                TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1
        '                            End If
        '                        End If
        '                    Next
        '                    r1 = _wrdDoc.Tables(1).Rows.Last
        '                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", Cost)
        '                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", NDS)
        '                    r1.Cells(7).Range.Text = String.Format("{0:0.00}", TotalSum)
        '                    r1.Cells(8).Range.Text = String.Format("{0:0.00}", NormaHour)

        '                    _wrdDoc.Bookmarks("cost1").Range.Text = Summa_propis(Cost)
        '                    _wrdDoc.Bookmarks("NDS1").Range.Text = Summa_propis(NDS)
        '                    _wrdDoc.Bookmarks("Summa1").Range.Text = Summa_propis(TotalSum)
        '                    _wrdDoc.Bookmarks("norma_hour").Range.Text = CStr(NormaHour) & " ч."

        '                    _wrdDoc.Save()

        '                    IO.File.Copy(docFullPath, path & "repair\" & DocName32, True)

        '                    '
        '                    'Сохраняем инфу для экспорта на сайт
        '                    '



        '                    'находим УНП клиента
        '                    Dim customer_unn = dbSQL.ExecuteScalar("SELECT unn FROM customer WHERE customer_sys_id='" & GetPageParam("c") & "'")

        '                    'Копируем док
        '                    IO.File.Copy(docFullPath, Server.MapPath("XML/repair_docs/" & Trim(customer_unn) & "+" & Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ".doc"), True)

        '                    Dim export_content = Trim(customer_unn) & ";" & Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ";" & Now & ";ready;" & TotalSum & vbCrLf

        '                    Dim content_temp
        '                    Dim file_open As IO.StreamReader
        '                    i = 1
        '                    file_open = IO.File.OpenText(Server.MapPath("XML/new_repair.csv"))
        '                    While Not file_open.EndOfStream
        '                        i = i + 1
        '                        content_temp = file_open.ReadLine()
        '                        If i < 20 Then
        '                            export_content &= content_temp & vbCrLf
        '                        End If
        '                    End While
        '                    file_open.Close()
        '                    Try
        '                        Dim file_save As IO.StreamWriter
        '                        file_save = IO.File.CreateText(Server.MapPath("XML/new_repair.csv"))
        '                        file_save.Write(export_content)
        '                        file_save.Close()
        '                    Catch ex As Exception
        '                    End Try


        '                Catch
        '                    WriteError("Акт о проведении ремонта<br>" & Err.Description)
        '                    ProcessRepairRealizationAct = False
        '                    GoTo ExitFunction
        '                End Try
        '            ElseIf num_doc(0) = 33 Then
        '                'Товарная накладная
        '                docFullPath = path & "repair\" & DocName33


        '                'ds.Tables("customer").Rows(0)("dogovor") & ds.Tables("sale").Rows(0)("dogovor")
        '                boos_name = ds.Tables("customer").Rows(0)("boos_name")
        '                customer_name = ds.Tables("customer").Rows(0)("customer_name")
        '                accountant = ds.Tables("customer").Rows(0)("accountant")
        '                unn = ds.Tables("customer").Rows(0)("unn")
        '                registration = ds.Tables("customer").Rows(0)("registration")
        '                Try
        '                    fl = New IO.FileInfo(docFullPath)

        '                    If Not fl.Exists() Then
        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If
        '                        Try
        '                            'Create folders and copy templates
        '                            Dim fldr As New IO.DirectoryInfo(path)
        '                            If Not fldr.Exists Then
        '                                fldr.Create()
        '                            End If
        '                        Catch ex As Exception
        '                            WriteError(Err.Description & "<BR>" & ex.ToString)
        '                            ProcessRepairRealizationAct = False
        '                            Exit Function
        '                        End Try
        '                    End If
        '                    IO.File.Copy(Server.MapPath("Templates/") & DocName33, docFullPath, True)

        '                    doc = wrdApp.Documents.Open(docFullPath)

        '                    '_wrdDoc.Bookmarks("Boos").Range.Text = ds.Tables("sale").Rows(0)("proxy")
        '                    '_wrdDoc.Bookmarks("Boos2").Range.Text = ds.Tables("sale").Rows(0)("proxy")
        '                    _wrdDoc.Bookmarks("CustomerAddress").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
        '                    _wrdDoc.Bookmarks("CustomerName").Range.Text = customer_name
        '                    _wrdDoc.Bookmarks("Dogovor").Range.Text = act_num
        '                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
        '                    sDate = GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))


        '                    Dim s1$ = ds.Tables("customer").Rows(0)("bank")
        '                    If s1.Trim.Length = 0 Then s1 = "нет"
        '                    _wrdDoc.Bookmarks("Bank").Range.Text = s1
        '                    _wrdDoc.Bookmarks("UNN1").Range.Text = unn
        '                    _wrdDoc.Bookmarks("UNN2").Range.Text = unn
        '                    _wrdDoc.Bookmarks("Date").Range.Text = dogovor '& " от " & sDate
        '                    _wrdDoc.Bookmarks("Date2").Range.Text = GetRussianDate(Now)
        '                    _wrdDoc.Bookmarks("Razreshil").Range.Text = "Яско Владимир Федорович!" 'ds.Tables("sale").Rows(0)("razreshil")
        '                    'If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
        '                    '    _wrdDoc.Bookmarks("FirmName1").Range.Text = ds.Tables("sale").Rows(0)("firm_name")
        '                    '    _wrdDoc.Bookmarks("Rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
        '                    '    _wrdDoc.Bookmarks("Employee").Range.Text = ds.Tables("sale").Rows(0)("fio")
        '                    'Else
        '                    'кто сделал все это

        '                    _wrdDoc.Bookmarks("Employee").Range.Text = sEmployee

        '                    Dim a As Integer = 0
        '                    Dim ndsItem As Double = 0

        '                    For i = 0 To ds.Tables("details").Rows.Count - 1

        '                        If ds.Tables("details").Rows(i)("is_detail") = True Then
        '                            ' детали
        '                            r1 = _wrdDoc.Tables(2).Rows.Add(_wrdDoc.Tables(2).Rows(a + 3))
        '                            a = a + 1
        '                            r1.Cells(1).Range.Text = a
        '                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
        '                            r1.Cells(3).Range.Text = "шт."
        '                            Quantity = ds.Tables("details").Rows(i)("quantity")
        '                            r1.Cells(4).Range.Text = Quantity
        '                            p = Math.Round(ds.Tables("details").Rows(i)("price") / 1.2, 2)
        '                            sSum = Quantity * ds.Tables("details").Rows(i)("price")
        '                            sNDS = (sSum - Quantity * p)
        '                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", p)
        '                            r1.Cells(6).Range.Text = "0"
        '                            r1.Cells(7).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
        '                            r1.Cells(8).Range.Text = "20"
        '                            r1.Cells(9).Range.Text = String.Format("{0:0.00}", sNDS)
        '                            r1.Cells(10).Range.Text = String.Format("{0:0.00}", sSum)
        '                            dNDS = dNDS + sNDS
        '                            dTotal = dTotal + sSum
        '                        End If
        '                    Next
        '                    _wrdDoc.Bookmarks("TotalNDS").Range.Text = String.Format("{0:0.00}", dNDS)
        '                    _wrdDoc.Bookmarks("TotalAll").Range.Text = String.Format("{0:0.00}", dTotal)
        '                    _wrdDoc.Bookmarks("Total").Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
        '                    _wrdDoc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
        '                    _wrdDoc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

        '                    _wrdDoc.Bookmarks("Count").Range.Text = Summa_propis(ds.Tables("details").Rows.Count, False)

        '                    _wrdDoc.Save()

        '                Catch
        '                    WriteError("Товарная накладная<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
        '                    ProcessRepairRealizationAct = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If

        'ExitFunction:
        '            Try
        '                ds.Clear()
        '                If Not doc Is Nothing Then
        '                    _wrdDoc.Close(True)
        '                End If
        '                If Not wrdApp Is Nothing Then
        '                    wrdApp.Quit(False)
        '                End If
        '                wrdApp = Nothing
        '                doc = Nothing
        '                cmd = Nothing
        '                ds = Nothing
        '                adapt = Nothing

        '            Catch
        '                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
        '            End Try
        '        End Function



    End Class
End Namespace