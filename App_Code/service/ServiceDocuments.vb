Imports System.Diagnostics
Imports System.IO
Imports System.IO.Compression
Imports System.Threading
Imports Kasbi
Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Service
    Public Class ServiceDocuments
        Inherits PageBase

        Const DocName32 As String = "Akt_RepairRealization_2.doc"
        Const DocName33 As String = "TTN_Repair.doc"
        Const DocName34 As String = "InvoiceNDS_Repair.doc"

        ReadOnly _monthReplacements1 As Hashtable = New Hashtable()
        ReadOnly _monthReplacements2 As Hashtable = New Hashtable()

        Private _wrdApp As Microsoft.Office.Interop.Word.ApplicationClass
        Private _wrdDoc As Microsoft.Office.Interop.Word.DocumentClass

        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()
        Dim ReadOnly _sharedDbSql2 As MSSqlDB = ServiceDbConnector.GetConnection2()

        ReadOnly _pathToDocs As String = Hosting.HostingEnvironment.MapPath("~/Docs/")
        ReadOnly _pathToTemplates As String = Hosting.HostingEnvironment.MapPath("~/Templates/")
        ReadOnly _pathToXml As String = Hosting.HostingEnvironment.MapPath("~/XML/")


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

        Public Function ProcessRepairRealizationAct(ByVal num_doc() As Integer, ByVal customer As Integer,
                                                    ByVal cash As Integer, ByVal history As Integer,
                                                    Optional userId As Integer = 0) As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim r1 As Word.Row
            Dim i%
            Dim Cost, NDS, TotalSum, NormaHour, Quantity As Double
            Dim q, p, dNDS, dTotal, sSum, sNDS, CPP As Double
            Cost = 0
            NDS = 0
            TotalSum = 0
            NormaHour = 0
            Quantity = 1

            ds = New DataSet
            cmd = New SqlClient.SqlCommand("prc_rpt_RepairRealizationAct_New")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_sys_id", cash)
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", history)
            adapt = _sharedDbSql2.GetDataAdapter(cmd)
            adapt.Fill(ds, "RepairRealizationAct")

            cmd = New SqlClient.SqlCommand("get_repair_info")
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", history)
            cmd.CommandType = CommandType.StoredProcedure
            adapt = _sharedDbSql2.GetDataAdapter(cmd)
            If Not ds.Tables("details") Is Nothing Then
                ds.Tables("details").Clear()
            End If
            adapt.Fill(ds, "details")

            If ds.Tables("details").Rows.Count = 0 Then GoTo ExitFunction
            Dim boos_name$, customer_name$, accountant$, unn$, registration$, sDate$, dogovor$

            cmd = New SqlClient.SqlCommand("get_customer_info")

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
            adapt = _sharedDbSql2.GetDataAdapter(cmd)
            If Not ds.Tables("customer") Is Nothing Then
                ds.Tables("customer").Clear()
            End If
            adapt.Fill(ds, "customer")

            If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
            Dim sEmployee$ =
                    _sharedDbSql2.ExecuteScalar(
                        "select Name from Employee where sys_id='" &
                        ds.Tables("RepairRealizationAct").Rows(0)("executor") & "'")
            If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                sEmployee = ""
            End If

            Dim act_num$ = ds.Tables("RepairRealizationAct").Rows(0)("akt")
            Dim WorkNotCall% = IIf(IsDBNull(ds.Tables("RepairRealizationAct").Rows(0)("workNotCall")), 0,
                                   ds.Tables("RepairRealizationAct").Rows(0)("workNotCall"))

            Dim docFullPath$ = String.Empty
            Dim path$ = _pathToDocs

            Dim fls As IO.File
            Dim fl As IO.FileInfo
            

            Try
                ' Create instance of Word!
                _wrdApp = New Word.Application
            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessRepairRealizationAct = String.Empty
                GoTo ExitFunction
            End Try

            If num_doc(0) = 32 Then
                Dim docFolder = path & "repair\" & userId
                Try
                    MkDir(docFolder)
                Catch ex As Exception
                End Try
                Try
                    docFullPath = docFolder & "\" & DocName32
                    fl = New IO.FileInfo(docFullPath)

                    If Not fl.Exists() Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        Try
                            'Create folders and copy templates
                            Dim fldr As New IO.DirectoryInfo(path)
                            If Not fldr.Exists Then
                                fldr.Create()
                            End If

                        Catch ex As Exception
                            WriteError(Err.Description & "<BR>" & ex.ToString)
                            ProcessRepairRealizationAct = String.Empty
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(_pathToTemplates & DocName32, docFullPath, True)

                    _wrdDoc = _wrdApp.Documents.Open(docFullPath)
                    _wrdDoc.Bookmarks("act_number1").Range.Text = act_num
                    _wrdDoc.Bookmarks("num_cashregister1").Range.Text =
                        ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
                    ' _wrdDoc.Bookmarks("num_cashregister2").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
                    _wrdDoc.Bookmarks("good_name1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("good_name")
                    '_wrdDoc.Bookmarks("good_type2").Range.Text = ds.Tables("defectAct").Rows(0)("good_name")
                    _wrdDoc.Bookmarks("customer1").Range.Text =
                        ds.Tables("RepairRealizationAct").Rows(0)("customer_name")
                    '  _wrdDoc.Bookmarks("customer2").Range.Text = ds.Tables("defectAct").Rows(0)("customer_name")
                    'кто сделал все это

                    _wrdDoc.Bookmarks("master1").Range.Text = sEmployee
                    _wrdDoc.Bookmarks("work_type").Range.Text =
                        _sharedDbSql2.ExecuteScalar(
                            "select ISNULL(work_type, '') from employee where sys_id =" &
                            ds.Tables("RepairRealizationAct").Rows(0)("executor")).ToString()

                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    sDate = ""
                    _wrdDoc.Bookmarks("dogovor1").Range.Text = dogovor '& " от " & sDate
                    Try
                        _wrdDoc.Bookmarks("sale_date1").Range.Text =
                            GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))
                    Catch
                        _wrdDoc.Bookmarks("sale_date1").Range.Text = "-"
                    End Try
                    Dim rdate_in As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_in"))
                    Dim rdate_out As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_out"))
                    Dim sRepairDates$ = GetRussianDate(rdate_in) & " " & rdate_in.ToString("HH:mm") & " - " &
                                        GetRussianDate(rdate_out)
                    _wrdDoc.Bookmarks("repair_date1").Range.Text = sRepairDates

                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
                            a = a + 1
                            r1.Cells(1).Range.Text = a
                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            r1.Cells(3).Range.Text = Quantity
                            r1.Cells(4).Range.Text = String.Format("{0:0.00}",
                                                                   (Math.Round(
                                                                       ds.Tables("details").Rows(i)("price")/1.0/1, 2)))
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}",
                                                                   (Quantity*
                                                                    Math.Round(
                                                                        ds.Tables("details").Rows(i)("price")/1.0/1, 2)))
                            ndsItem = Quantity*
                                      (Math.Round(ds.Tables("details").Rows(i)("price")*1.2/1, 2)*1 -
                                       Math.Round(ds.Tables("details").Rows(i)("price")/1.0/1, 2)*1)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                            r1.Cells(7).Range.Text = String.Format("{0:0.00}",
                                                                   (Quantity*
                                                                    Math.Round(
                                                                        ds.Tables("details").Rows(i)("price")*1.2/1, 2)*
                                                                    1))
                            r1.Cells(8).Range.Text = String.Format("{0:0.00}",
                                                                   (IIf(WorkNotCall = 1,
                                                                        Quantity*
                                                                        ds.Tables("details").Rows(i)("norma_hour"), "0")))

                            Cost = Cost + Math.Round(Quantity*ds.Tables("details").Rows(i)("price")/1.0/1, 2)*1
                            NDS = NDS + ndsItem
                            TotalSum = TotalSum + Quantity*Math.Round(ds.Tables("details").Rows(i)("price")*1.2/1, 2)*1

                            ' замена  детали
                            If ds.Tables("details").Rows(i)("cost_service") > 0 Then
                                NormaHour = NormaHour + Quantity*ds.Tables("details").Rows(i)("norma_hour")
                                If WorkNotCall = 0 Then
                                    r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
                                    a = a + 1
                                    r1.Cells(1).Range.Text = a
                                    r1.Cells(2).Range.Text = "Замена " & ds.Tables("details").Rows(i)("name")
                                    r1.Cells(3).Range.Text = ""
                                    r1.Cells(4).Range.Text = String.Format("{0:0.00}",
                                                                           (Math.Round(
                                                                               ds.Tables("details").Rows(i)(
                                                                                   "cost_service")/1.0/1, 2)*1))
                                    r1.Cells(5).Range.Text = String.Format("{0:0.00}",
                                                                           (Quantity*
                                                                            Math.Round(
                                                                                ds.Tables("details").Rows(i)(
                                                                                    "cost_service")/1.0/1, 2)*1))
                                    ndsItem = Quantity*
                                              (Math.Round(ds.Tables("details").Rows(i)("cost_service")*1.2/1, 2)*1 -
                                               Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.0/1, 2)*1)
                                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                                    r1.Cells(7).Range.Text = String.Format("{0:0.00}",
                                                                           (Quantity*
                                                                            Math.Round(
                                                                                ds.Tables("details").Rows(i)(
                                                                                    "cost_service")*1.2/1, 2)*1))
                                    r1.Cells(8).Range.Text = String.Format("{0:0.00}",
                                                                           (Quantity*
                                                                            ds.Tables("details").Rows(i)("norma_hour")))
                                    Cost = Cost +
                                           Quantity*Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.0/1, 2)*1
                                    NDS = NDS + ndsItem
                                    TotalSum = TotalSum +
                                               Quantity*
                                               Math.Round(ds.Tables("details").Rows(i)("cost_service")*1.2/1, 2)*1
                                End If
                            End If
                        Else
                            If WorkNotCall = 0 Then
                                r1 = _wrdDoc.Tables(1).Rows.Add(_wrdDoc.Tables(1).Rows(a + 2))
                                a = a + 1
                                r1.Cells(1).Range.Text = a
                                r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                                r1.Cells(3).Range.Text = ""
                                Quantity = ds.Tables("details").Rows(i)("quantity")
                                r1.Cells(4).Range.Text = String.Format("{0:0.00}",
                                                                       (Math.Round(
                                                                           ds.Tables("details").Rows(i)("cost_service")/
                                                                           1.0/1, 2)))
                                r1.Cells(5).Range.Text = String.Format("{0:0.00}",
                                                                       (Quantity*
                                                                        Math.Round(
                                                                            ds.Tables("details").Rows(i)("cost_service")/
                                                                            1.0/1, 2)))

                                ndsItem = Quantity*
                                          (Math.Round(ds.Tables("details").Rows(i)("cost_service")*1.2/1, 2)*1 -
                                           Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.0/1, 2)*1)
                                r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                                r1.Cells(7).Range.Text = String.Format("{0:0.00}",
                                                                       (Quantity*
                                                                        Math.Round(
                                                                            ds.Tables("details").Rows(i)("cost_service")*
                                                                            1.2/1, 2)))
                                r1.Cells(8).Range.Text = String.Format("{0:0.00}",
                                                                       (Quantity*
                                                                        ds.Tables("details").Rows(i)("norma_hour")))
                                NormaHour = NormaHour + Quantity*ds.Tables("details").Rows(i)("norma_hour")
                                Cost = Cost +
                                       Quantity*Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.0/1, 2)*1
                                NDS = NDS + ndsItem
                                TotalSum = TotalSum +
                                           Quantity*Math.Round(ds.Tables("details").Rows(i)("cost_service")*1.2/1, 2)*1
                            End If
                        End If
                    Next
                    'Игнорируем предыдущие расчеты НДС и пересчитываем НДС, так как не сходяться расчеты с 1С
                    '-----------------------------------START----------------------------------------
                    NDS = Math.Round(Cost*0.2, 2)
                    TotalSum = Cost + NDS
                    '-----------------------------------END----------------------------------------------
                    '-----------------------------------------------------------------------------------

                    r1 = _wrdDoc.Tables(1).Rows.Last
                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", Cost)
                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", NDS)
                    r1.Cells(7).Range.Text = String.Format("{0:0.00}", TotalSum)
                    r1.Cells(8).Range.Text = String.Format("{0:0.00}", NormaHour)

                    _wrdDoc.Bookmarks("cost1").Range.Text = Summa_propis(Cost)
                    _wrdDoc.Bookmarks("NDS1").Range.Text = Summa_propis(NDS)
                    _wrdDoc.Bookmarks("Summa1").Range.Text = Summa_propis(TotalSum)
                    _wrdDoc.Bookmarks("norma_hour").Range.Text = CStr(NormaHour) & " ч."

                    _wrdDoc.Save()


                    Dim docFolderForUser = path & "repair\" & userId
                    Dim fldrForUser As New IO.DirectoryInfo(docFolderForUser)
                    If Not fldrForUser.Exists Then
                        fldrForUser.Create()
                    End If
                    'IO.File.Copy(docFullPath, docFolderForUser & "\" & DocName32, True)

                    '
                    'Сохраняем инфу для экспорта на сайт
                    '


                    'находим УНП клиента
                    Dim customer_unn =
                            _sharedDbSql2.ExecuteScalar(
                                "SELECT unn FROM customer WHERE customer_sys_id='" & GetPageParam("c") & "'")

                    'Копируем док
                    IO.File.Copy(docFullPath,
                                 _pathToXml & "/repair_docs/" & Trim(customer_unn) & "+" &
                                     Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ".doc", True)

                    Dim export_content = Trim(customer_unn) & ";" &
                                         Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ";" & Now &
                                         ";ready;" & TotalSum & vbCrLf

                    Dim content_temp
                    Dim file_open As IO.StreamReader
                    i = 1
                    file_open = IO.File.OpenText(_pathToXml & "/new_repair.csv")
                    While Not file_open.EndOfStream
                        i = i + 1
                        content_temp = file_open.ReadLine()
                        If i < 20 Then
                            export_content &= content_temp & vbCrLf
                        End If
                    End While
                    file_open.Close()
                    Try
                        Dim file_save As IO.StreamWriter
                        file_save = IO.File.CreateText(_pathToXml & "/new_repair.csv")
                        file_save.Write(export_content)
                        file_save.Close()
                    Catch ex As Exception
                    End Try


                Catch
                    WriteError("Акт о проведении ремонта<br>" & Err.Description)
                    ProcessRepairRealizationAct = String.Empty
                    GoTo ExitFunction
                End Try
            ElseIf num_doc(0) = 33 Then
                'Товарная накладная
                docFullPath = path & "repair\" & DocName33


                'ds.Tables("customer").Rows(0)("dogovor") & ds.Tables("sale").Rows(0)("dogovor")
                boos_name = ds.Tables("customer").Rows(0)("boos_name")
                customer_name = ds.Tables("customer").Rows(0)("customer_name")
                accountant = ds.Tables("customer").Rows(0)("accountant")
                unn = ds.Tables("customer").Rows(0)("unn")
                registration = ds.Tables("customer").Rows(0)("registration")
                Try
                    fl = New IO.FileInfo(docFullPath)

                    If Not fl.Exists() Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        Try
                            'Create folders and copy templates
                            Dim fldr As New IO.DirectoryInfo(path)
                            If Not fldr.Exists Then
                                fldr.Create()
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<BR>" & ex.ToString)
                            ProcessRepairRealizationAct = String.Empty
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(_pathToTemplates & DocName33, docFullPath, True)

                    _wrdDoc = _wrdApp.Documents.Open(docFullPath)

                    '_wrdDoc.Bookmarks("Boos").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                    '_wrdDoc.Bookmarks("Boos2").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                    _wrdDoc.Bookmarks("CustomerAddress").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                    _wrdDoc.Bookmarks("CustomerName").Range.Text = customer_name
                    _wrdDoc.Bookmarks("Dogovor").Range.Text = act_num
                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    sDate = GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))


                    Dim s1$ = ds.Tables("customer").Rows(0)("bank")
                    If s1.Trim.Length = 0 Then s1 = "нет"
                    _wrdDoc.Bookmarks("Bank").Range.Text = s1
                    _wrdDoc.Bookmarks("UNN1").Range.Text = unn
                    _wrdDoc.Bookmarks("UNN2").Range.Text = unn
                    _wrdDoc.Bookmarks("Date").Range.Text = dogovor '& " от " & sDate
                    _wrdDoc.Bookmarks("Date2").Range.Text = GetRussianDate(Now)
                    _wrdDoc.Bookmarks("Razreshil").Range.Text = "Яско Владимир Федорович!" _
                    'ds.Tables("sale").Rows(0)("razreshil")
                    'If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
                    '    _wrdDoc.Bookmarks("FirmName1").Range.Text = ds.Tables("sale").Rows(0)("firm_name")
                    '    _wrdDoc.Bookmarks("Rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
                    '    _wrdDoc.Bookmarks("Employee").Range.Text = ds.Tables("sale").Rows(0)("fio")
                    'Else
                    'кто сделал все это

                    _wrdDoc.Bookmarks("Employee").Range.Text = sEmployee

                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = _wrdDoc.Tables(2).Rows.Add(_wrdDoc.Tables(2).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = a
                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                            r1.Cells(3).Range.Text = "шт."
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            r1.Cells(4).Range.Text = Quantity
                            p = Math.Round(ds.Tables("details").Rows(i)("price")/1.2, 2)
                            sSum = Quantity*ds.Tables("details").Rows(i)("price")
                            sNDS = (sSum - Quantity*p)
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", p)
                            r1.Cells(6).Range.Text = "0"
                            r1.Cells(7).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                            r1.Cells(8).Range.Text = "20"
                            r1.Cells(9).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(10).Range.Text = String.Format("{0:0.00}", sSum)
                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                        End If
                    Next
                    _wrdDoc.Bookmarks("TotalNDS").Range.Text = String.Format("{0:0.00}", dNDS)
                    _wrdDoc.Bookmarks("TotalAll").Range.Text = String.Format("{0:0.00}", dTotal)
                    _wrdDoc.Bookmarks("Total").Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
                    _wrdDoc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                    _wrdDoc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                    _wrdDoc.Bookmarks("Count").Range.Text = Summa_propis(ds.Tables("details").Rows.Count, False)

                    _wrdDoc.Save()

                Catch
                    WriteError(
                        "Товарная накладная<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError &
                        "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessRepairRealizationAct = String.Empty
                    GoTo ExitFunction
                End Try


            End If
            If num_doc(0) = 34 Then

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Счет-фактура по НДС

                docFullPath = path & "repair\" & DocName34

                Try
                    fl = New IO.FileInfo(docFullPath)

                    If Not fl.Exists() Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        Try
                            'Create folders and copy templates
                            Dim fldr As New IO.DirectoryInfo(path)
                            If Not fldr.Exists Then
                                fldr.Create()
                            End If

                        Catch ex As Exception
                            WriteError(Err.Description & "<BR>" & ex.ToString)
                            ProcessRepairRealizationAct = String.Empty
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(_pathToTemplates & DocName34, docFullPath, True)

                    _wrdDoc = _wrdApp.Documents.Open(docFullPath)

                    Dim rdate_in As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_in"))
                    Dim rdate_out As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_out"))
                    sDate = GetRussianDate(rdate_out)


                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    '_wrdDoc.Bookmarks("Dogovor").Range.Text = act_num
                    _wrdDoc.Bookmarks("RECIPIENT_ADDRESS").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                    _wrdDoc.Bookmarks("RECIPIENT_NAME").Range.Text = ds.Tables("customer").Rows(0)("customer_name")
                    _wrdDoc.Bookmarks("RECIPIENT_UNN").Range.Text = ds.Tables("customer").Rows(0)("unn")
                    _wrdDoc.Bookmarks("Date").Range.Text = sDate
                    _wrdDoc.Bookmarks("Date1").Range.Text = sDate
                    _wrdDoc.Bookmarks("BILL_DATE").Range.Text = sDate
                    _wrdDoc.Bookmarks("ACCEPT_INVOICE").Range.Text = "к акту №" & act_num & " от " &
                                                                     rdate_out.ToString("dd.MM.yyyy") & "г."
                    _wrdDoc.Bookmarks("Saler").Range.Text = sEmployee
                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = _wrdDoc.Tables(3).Rows.Add(_wrdDoc.Tables(3).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            p = Math.Round(ds.Tables("details").Rows(i)("price")/1.2, 2)
                            sSum = Quantity*ds.Tables("details").Rows(i)("price")
                            sNDS = (sSum - Quantity*p)
                            r1.Cells(2).Range.Text = String.Format("{0:0.00}", (Quantity*p))
                            r1.Cells(3).Range.Text = " "
                            r1.Cells(4).Range.Text = "20"
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                            If ds.Tables("details").Rows(i)("cost_service") > 0 Then
                                r1 = _wrdDoc.Tables(3).Rows.Add(_wrdDoc.Tables(3).Rows(a + 3))
                                a = a + 1
                                r1.Cells(1).Range.Text = "Замена " & ds.Tables("details").Rows(i)("name")
                                Quantity = ds.Tables("details").Rows(i)("quantity")
                                p = Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.2, 2)
                                sSum = Quantity*ds.Tables("details").Rows(i)("cost_service")
                                sNDS = (sSum - Quantity*p)
                                r1.Cells(2).Range.Text = Quantity*p
                                r1.Cells(3).Range.Text = " "
                                r1.Cells(4).Range.Text = "20"
                                r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                                r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                                dNDS = dNDS + sNDS
                                dTotal = dTotal + sSum
                            End If
                        Else
                            r1 = _wrdDoc.Tables(3).Rows.Add(_wrdDoc.Tables(3).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            p = Math.Round(ds.Tables("details").Rows(i)("cost_service")/1.2, 2)
                            sSum = Quantity*ds.Tables("details").Rows(i)("cost_service")
                            sNDS = (sSum - Quantity*p)
                            r1.Cells(2).Range.Text = String.Format("{0:0.00}", (Quantity*p))
                            r1.Cells(3).Range.Text = " "
                            r1.Cells(4).Range.Text = "20"
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                        End If
                    Next
                    r1 = _wrdDoc.Tables(3).Rows.Last
                    r1.Cells(2).Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
                    r1.Cells(3).Range.Text = ""
                    r1.Cells(4).Range.Text = "x"
                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", dNDS)
                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", dTotal)


                    '_wrdDoc.Bookmarks("TotalNDS").Range.Text = dNDS
                    '_wrdDoc.Bookmarks("TotalAll").Range.Text = dTotal
                    '_wrdDoc.Bookmarks("Total").Range.Text = dTotal - dNDS
                    _wrdDoc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                    _wrdDoc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                    _wrdDoc.Save()

                Catch
                    WriteError(
                        "Счет-фактура  по НДС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError &
                        "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessRepairRealizationAct = String.Empty
                    GoTo ExitFunction
                End Try

            End If
            ExitFunction:
            Try
                ds.Clear()
                If Not _wrdDoc Is Nothing Then
                    _wrdDoc.Close(True)
                End If
                If Not _wrdApp Is Nothing Then
                    _wrdApp.Quit(False)
                End If
                _wrdApp = Nothing
                _wrdDoc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
                ProcessRepairRealizationAct = docFullPath
            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try
        End Function


        Public Sub AktForTOandDolg(checkGoods As ListDictionary, httpResponse As HttpResponse,
                                   Optional withDate As Boolean = True,
                                   Optional dateAkt As DateTime = Nothing)
            Dim savePath As String = String.Empty
            If checkGoods.Count > 0 Then
                savePath = CreateAllAktForTOandDolg(checkGoods, withDate, dateAkt)
            End If
            ResponseFile(savePath, httpResponse)
        End Sub

        Private Function CreateAllAktForTOandDolg(checkGoods As ListDictionary, Optional withDate As Boolean = True,
                                                  Optional dateAkt As DateTime = Nothing) As String

            Dim filePath, rootPath, folderPath As String
            Const fileName As String = "Akt_Of_TO_And_Dolg"
            Const folderName As String = "Akt_Of_TO_And_Dolg"
            Dim interval As Integer = 50
            Dim currentStep As Integer = interval
            Dim currentIndex As Integer = 0
            Dim subCheckGoods As ListDictionary = New ListDictionary


            rootPath = _pathToDocs & "\Akts\" & Session("User").sys_id.ToString
            folderPath = rootPath & "\" & folderName

            If dateAkt = Nothing Then
                dateAkt = DateTime.Today
            End If

            If Directory.Exists(folderPath)
                Directory.Delete(folderPath, True)
            End If

            If checkGoods.Count > 50
                For Each checkGood As DictionaryEntry In checkGoods
                    If currentIndex < currentStep
                        SubCheckGoods.Add(checkGood.Key, checkGood.Value)
                        currentIndex += 1
                    Else
                        CreateAktForTOandDolg(SubCheckGoods, folderPath, fileName & "__" & currentIndex, withDate,
                                              dateAkt)
                        currentStep += interval
                        subCheckGoods = New ListDictionary
                    End If
                Next
                CreateAktForTOandDolg(SubCheckGoods, folderPath, fileName & "__" & checkGoods.Count, withDate, dateAkt)
                filePath = rootPath & "\" & fileName & ".zip"
                If File.Exists(filePath)
                    File.Delete(filePath)
                End If
                Thread.Sleep(2000)
                ZipFile.CreateFromDirectory(folderPath, filePath)
            Else
                filePath = rootPath & "\" &
                           CreateAktForTOandDolg(checkGoods, rootPath, fileName, withDate, dateAkt)
            End If
            Return filePath
        End Function

        Private Function CreateAktForTOandDolg(checkGoods As ListDictionary, rootPath As String, fileName As String,
                                               Optional withDate As Boolean = True,
                                               Optional dateAkt As DateTime = Nothing) As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim drs() As Data.DataRow
            Dim templatePath, savePath, filter, fullFileName As String

            templatePath = _pathToTemplates & "Akt_Of_TO_And_Dolg.doc"
            fullFileName = fileName & ".doc"
            savePath = rootPath & "\" & fullFileName

            CopyFile(templatePath, savePath, overwrite := True)
            Try
                _wrdApp = New Word.ApplicationClass()
                _wrdApp.Caption = "Caption1"
                _wrdApp.DisplayAlerts = Word.WdAlertLevel.wdAlertsNone
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

                adapt = _sharedDbSql.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                drs = ds.Tables(0).Select()

                For k = drs.Length - 1 To 0 Step - 1

                    'If k Mod 2 = 0 Then
                    pasteAktForTOPart1(drs, k, withDate, dateAkt, checkGoods)
                    'Else
                    pasteAktForTOPart2(drs, k, withDate, dateAkt, checkGoods)
                    EmptyAktForToPart2()
                    If k <> 0 Then
                        With _wrdDoc.Sections(1).Range
                            .Collapse(Word.WdCollapseDirection.wdCollapseEnd)
                            .InsertBreak(Word.WdBreakType.wdPageBreak)
                            .Collapse(Word.WdCollapseDirection.wdCollapseEnd)
                            .Paste()
                        End With
                    End If
                    'End If
                Next
                If drs.Length Mod 2 = 1 Then
                    ClearAktForToPart2()
                End If

                _wrdDoc.Close(True)
                _wrdApp.Quit()

            Catch ex As Exception
                _wrdDoc.Close(True)
                _wrdApp.Quit()
            End Try
            Return fullFileName
        End Function

        Private Sub PasteAktForToPart1(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime,
                                       checkGoods As ListDictionary)
            If withDate Then
                Dim test As String = dateAkt.ToString("dd")
                _wrdDoc.Bookmarks("Day1").Range.Text = test
            End If
            _wrdDoc.Bookmarks("Counter1").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt1").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month1_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM")).ToString()
            _wrdDoc.Bookmarks("Month1_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM")).ToString()
            _wrdDoc.Bookmarks("NumDog1").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works1").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash1").Range.Text = drs(k).Item(2).ToString().Trim()
            PasteAktForToSummaTo(drs, k, 1)
            _wrdDoc.Bookmarks("DateDolg1").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg1").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer1").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master1").Range.Text = CurrentUser.Name
        End Sub

        Private Sub PasteAktForToPart2(drs() As Data.DataRow, k As Integer, withDate As Boolean, dateAkt As DateTime,
                                       checkGoods As ListDictionary)
            If withDate Then
                _wrdDoc.Bookmarks("Day2").Range.Text = dateAkt.ToString("dd")
            End If
            _wrdDoc.Bookmarks("Counter2").Range.Text = checkGoods.Item(drs(k).Item(0).ToString()).ToString()
            _wrdDoc.Bookmarks("NumAkt2").Range.Text = drs(k).Item(2).ToString().Trim() & "/" & dateAkt.Month
            _wrdDoc.Bookmarks("Month2_1").Range.Text = _monthReplacements1(dateAkt.ToString("MM")).ToString()
            _wrdDoc.Bookmarks("Month2_2").Range.Text = _monthReplacements2(dateAkt.ToString("MM")).ToString()
            _wrdDoc.Bookmarks("NumDog2").Range.Text = drs(k).Item(7).ToString()
            _wrdDoc.Bookmarks("Works2").Range.Text = drs(k).Item(1).ToString()
            _wrdDoc.Bookmarks("NumCash2").Range.Text = drs(k).Item(2).ToString().Trim()
            PasteAktForToSummaTo(drs, k, 2)
            _wrdDoc.Bookmarks("DateDolg2").Range.Text = DateTime.Today.ToString("dd.MM.yy")
            _wrdDoc.Bookmarks("Dolg2").Range.Text = drs(k).Item(10).ToString()
            _wrdDoc.Bookmarks("Customer2").Range.Text = drs(k).Item(9).ToString()
            _wrdDoc.Bookmarks("Master2").Range.Text = CurrentUser.Name
        End Sub

        Private Sub PasteAktForToSummaTo(drs() As Data.DataRow, k As Integer, num As Integer)
            If InStr(drs(k).Item(6).ToString(), "ТО1") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "15.00 (пятнадцать) рублей 00 копеек, в т. ч. НДС (20%) 2.50 (два рубля 50 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО2") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "7.00 (семь) рублей 50 копеек, в т. ч. НДС (20%) 1.25 (один рубль 25 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО3") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "9.00 (девять) рублей 60 копеек, в т. ч. НДС (20%) 1.60 (один рубль 60 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО4") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "24.00 (двадцать четыре) рубля 00 копеек, в т. ч. НДС (20%) 4.00 (четыре рубля 00 копеек)."
            ElseIf InStr(drs(k).Item(6).ToString(), "ТО5") <> 0 Then
                _wrdDoc.Bookmarks("NDS" & num).Range.Text =
                    "6.00 (шесть) рублей 00 копеек, в т. ч. НДС (20%) 1.00 (один рубль 00 копеек)."
            End If
        End Sub

        Private Sub ClearAktForToPart2()
            _wrdDoc.Bookmarks("Akt2_body").Range.Text = " "
        End Sub

        Private Sub EmptyAktForToPart2()
            _wrdDoc.Bookmarks("Akt2_body").Delete()
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String =
                    {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ",
                     " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDate1(ByVal d As Date) As String
            Dim m() As String =
                    {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ",
                     " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate1 = " « " & Day(d) & " » " & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDate2(ByVal d As Date) As String
            Dim m() As String =
                    {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ",
                     " Дек "}
            GetRussianDate2 = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDate3(ByVal d As Date) As String
            Dim m() As String =
                    {" Январь ", " Февраль ", " Март ", " Апрель ", " Май ", " Июнь ", " Июль ", " Август ",
                     " Сентябрь ", " Октябрь ", " Ноябрь ", " Декабрь "}
            GetRussianDate3 = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function Summa_propis(ByVal s As String, Optional ByVal b As Boolean = True,
                                     Optional ByVal b_cop As Boolean = True) As String
            Dim sum_p_rub, sum_p_cop, cop As String
            Dim kop_arr = s.ToString.Split(",")
            sum_p_cop = String.Empty

            If kop_arr.Length >= 2 Then
                s = kop_arr(0)
                cop = kop_arr(1)
            Else
                s = kop_arr(0)
                cop = "00"
            End If
            sum_p_rub = Summa_propis_rub(s, b)
            If (b_cop) Then
                sum_p_cop = " " & Summa_propis_cop(cop, b)
            End If
            Return sum_p_rub & sum_p_cop
        End Function

        Public Function Summa_propis_rub(ByVal s As String, Optional ByVal b As Boolean = True) As String
            Dim ss@, txt$, n%, i%
            Static triad(4) As Integer, numb1(19) As String, numb2(9) As String, numb3(9) As String
            If s = 0 Then
                Summa_propis_rub = ""
                Exit Function
            End If

            ss@ = s
            triad(1) = ss@ - Int(ss@/1000)*1000
            ss@ = Int(ss@/1000)
            triad(2) = ss@ - Int(ss@/1000)*1000
            ss@ = Int(ss@/1000)
            triad(3) = ss@ - Int(ss@/1000)*1000
            ss@ = Int(ss@/1000)
            triad(4) = ss@ - Int(ss@/1000)*1000
            ss@ = Int(ss@/1000)
            numb1(0) = ""
            numb1(1) = "один "
            numb1(2) = "два "
            numb1(3) = "три "
            numb1(4) = "четыре "
            numb1(5) = "пять "
            numb1(6) = "шесть "
            numb1(7) = "семь "
            numb1(8) = "восемь "
            numb1(9) = "девять "
            numb1(10) = "десять "
            numb1(11) = "одиннадцать "
            numb1(12) = "двенадцать "
            numb1(13) = "тринадцать "
            numb1(14) = "четырнадцать "
            numb1(15) = "пятнадцать "
            numb1(16) = "шестнадцать "
            numb1(17) = "семнадцать "
            numb1(18) = "восемнадцать "
            numb1(19) = "девятнадцать "
            numb2(0) = ""
            numb2(1) = ""
            numb2(2) = "двадцать "
            numb2(3) = "тридцать "
            numb2(4) = "сорок "
            numb2(5) = "пятьдесят "
            numb2(6) = "шестьдесят "
            numb2(7) = "семьдесят "
            numb2(8) = "восемьдесят "
            numb2(9) = "девяносто "
            numb3(0) = ""
            numb3(1) = "сто "
            numb3(2) = "двести "
            numb3(3) = "триста "
            numb3(4) = "четыреста "
            numb3(5) = "пятьсот "
            numb3(6) = "шестьсот "
            numb3(7) = "семьсот "
            numb3(8) = "восемьсот "
            numb3(9) = "девятьсот "
            txt$ = ""
            If ss@ <> 0 Then
                'n% = MsgBox("Сумма выходит за границы формата", 16, "Сумма прописью")
                Summa_propis_rub = ""
                Exit Function
            End If
            For i% = 4 To 1 Step - 1
                n% = 0
                If triad(i%) > 0 Then
                    n% = Int(triad(i%)/100)
                    txt$ = txt$ & numb3(n%)
                    n% = Int((triad(i%) - n%*100)/10)
                    txt$ = txt$ & numb2(n%)
                    If n% < 2 Then
                        n% = triad(i%) - (Int(triad(i%)/10) - n%)*10
                    Else
                        n% = triad(i%) - Int(triad(i%)/10)*10
                    End If
                    Select Case n%
                        Case 1
                            If i% = 2 Then txt$ = txt$ & "одна " Else txt$ = txt$ & "один "
                        Case 2
                            If i% = 2 Then txt$ = txt$ & "две " Else txt$ = txt$ & "два "
                        Case Else
                            txt$ = txt$ & numb1(n%)
                    End Select
                    Select Case i%
                        Case 2
                            If n% = 0 Or n% > 4 Then
                                txt$ = txt$ + "тысяч "
                            Else
                                If n% = 1 Then txt$ = txt$ + "тысяча " Else txt$ = txt$ + "тысячи "
                            End If
                        Case 3
                            If n% = 0 Or n% > 4 Then
                                txt$ = txt$ + "миллионов "
                            Else
                                If n% = 1 Then txt$ = txt$ + "миллион " Else txt$ = txt$ + "миллиона "
                            End If
                        Case 4
                            If n% = 0 Or n% > 4 Then
                                txt$ = txt$ + "миллиардов "
                            Else
                                If n% = 1 Then txt$ = txt$ + "миллиард " Else txt$ = txt$ + "миллиарда "
                            End If
                    End Select
                End If
            Next i%
            If b Then
                txt$ = txt$ + "бел. руб."
            End If
            txt$ = UCase$(Left$(txt$, 1)) & Mid$(txt$, 2)
            Summa_propis_rub = txt$
        End Function

        Private Function Summa_propis_cop(ByVal cop As String, Optional ByVal b As Boolean = True) As String
            If cop.Length = 1 Then
                cop = cop & "0"
            End If

            If b Then
                Return cop & " коп."
            Else
                Return cop
            End If
        End Function

        Public Sub ResponseFile(savePath As String, httpResponse As HttpResponse)
            Dim fileExtension = savePath.Substring(savePath.Length - 3, 3)
            Dim file As IO.FileInfo
            file = New FileInfo(savePath)
            If file.Exists Then
                httpResponse.Clear()
                httpResponse.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                httpResponse.AddHeader("Content-Length", file.Length.ToString())
                httpResponse.ContentType = "application/octet-stream"
                If fileExtension = "zip"
                    httpResponse.ContentType = "application/zip"
                Else
                    httpResponse.ContentType = "application/octet-stream"
                End If
                httpResponse.WriteFile(savePath)
                httpResponse.End()
            Else
                httpResponse.Write("This file does not exist.")
            End If
        End Sub

        Private Sub WriteError(ByVal s As String)
            Try
                Response.Write("<p align=center><font color=red size=5 face=Tahoma verdana>" & s & "</font></p>")
            Catch
            End Try
        End Sub
    End Class
End Namespace