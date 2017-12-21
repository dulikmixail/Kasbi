'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\Stub_Documents_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'Documents.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================
Imports System.Collections.Generic
Imports System.IO
Imports Microsoft.Office.Interop


Namespace Kasbi


    'Partial Class Documents
    Partial Class Migrated_Documents : Inherits Documents


#Region " Web Form Designer Generated Code "


        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        'Const ConnectionString = "data source=srvmain;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=srvmain;packet size=4096;password=webdb;"
        'Const ConnectionString = "data source=BY-MN-DBSRV;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=BY-MN-DBSRV;packet size=4096;password=webdb;"

        'Const ConnectionString = "data source=192.168.11.14;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=198.168.11.14;packet size=4096;password=webdb;"
        Dim act_num_global

        Const DocName0 As String = "InvoiceNDS.doc"
        Const DocName1 As String = "Dogovor.doc"
        Const DocName2 As String = "Zayavlenie_Na_Knigu_Kassira.doc"
        Const DocName3 As String = "Zayavlenie.doc"
        Const DocName4 As String = "Akt_Pokazaniy_In.doc"
        Const DocName5 As String = "TTN.doc"
        Const DocName6 As String = "Dogovor_Na_TO.doc"
        Const DocName7 As String = "Spisok_KKM.doc"
        Const DocName8 As String = "Teh_Zaklyuchenie.doc"
        Const DocName9 As String = "Udostoverenie_Kassira.doc"
        Const DocName10 As String = "Akt_Work.doc"
        Const DocName57 As String = "Dogovor_Na_TO_Dop.doc"
        'постановка на ТО
        Const DocName11 As String = "Akt_Pokazaniy_In.doc"
        Const DocName12 As String = "Teh_Zaklyuchenie_In.doc"
        Const DocName13 As String = "Dogovor_Na_TO.doc"
        ' снятие с ТО
        Const DocName14 As String = "Akt_Pokazaniy_Out.doc"
        Const DocName15 As String = "Akt_Pokazaniy_Out.doc"
        Const DocName56 As String = "Zayavlenie_snyat_imns.doc"
        'ремонт
        Const DocName16 As String = "Akt_Pokazaniy_Repair.doc"
        'гарантийный талон
        Const DocName17 As String = "Garantia.doc"
        'заявление в ИМНС
        Const DocName18 As String = "Zayavlenie_IMNS.doc"
        '
        Const DocName19 As String = "Teh_Zaklyuchenie_Out.doc"
        Const DocName20 As String = "Teh_Zaklyuchenie_Out.doc"
        '
        Const DocName31 As String = "DefectAct.doc"
        Const DocName32 As String = "Akt_RepairRealization.doc"
        Const DocName33 As String = "TTN_Repair.doc"
        Const DocName34 As String = "InvoiceNDS_Repair.doc"
        Const DocName35 As String = "TTN_Transport.doc"
        '
        Const DocName30 As String = "Marka_Quartal_Report.xls"
        Const DocName40 As String = "Akt_Dismissal_Mark.doc"
        Const DocName41 As String = "Izveschenie.doc"
        Const DocName42 As String = "DefectActForGood.doc"
        'маршрут
        Const DocName50 As String = "Route_Report.xls"
        'остатки на складе
        Const DocName51 As String = "Rest_Report.xls"
        Const DocName52 As String = "WarehouseCard.xls"
        Const DocName53 As String = "MasterTO_Report.xls"
        Const DocName54 As String = "Route_Report.xls"
        Const DocName55 As String = "IMNS_letter.doc"


        Private reader As SqlClient.SqlDataReader
        Private wrdApp As Word.ApplicationClass
        Dim xlsApp As Excel.ApplicationClass

        Private doc As Word.DocumentClass
        Private sheet As Excel.WorksheetClass
        Private book As Excel.WorkbookClass

        Dim query

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim customer_sys_id%, sale_sys_id%, sub_num%, doc_path$, good_sys_id%, history_id, rebilling%, vid_plateza%
            Dim customer_sys_id_s, good_sys_id_s As String
            Dim begin_date As System.DateTime, end_date As DateTime
            Dim doc_type(0) As Integer
            Dim path$

            Dim s As String()


            'проверка параметров
            Try

                customer_sys_id = GetPageParam("c")
                sale_sys_id = GetPageParam("s")
                doc_type(0) = GetPageParam("t")
                sub_num = GetPageParam("n")
                good_sys_id = GetPageParam("g")
                history_id = GetPageParam("h")
                rebilling = GetPageParam("rebilling")
                vid_plateza = GetPageParam("vidplateza")
                If (doc_type(0) = 10) Then
                    Dim dataStream As Stream = Request.InputStream
                    Dim reader As StreamReader = New StreamReader(dataStream)
                    Dim responseFromServer As String = reader.ReadToEnd()
                    customer_sys_id_s = Request.QueryString("c_list")
                    good_sys_id_s = Request.QueryString("good_id_list")
                End If
                Try
                    begin_date = DateTime.Parse(Request.Params.Item("begin_date"))
                    end_date = DateTime.Parse(Request.Params.Item("end_date"))
                    'Dim e$ = end_date.ToLongDateString()
                Catch
                End Try
            Catch
                WriteError("Неверные параметры")
                Exit Sub
            End Try

            If (begin_date <> DateTime.MinValue) Then
                If Not ProcessReportQuartal(doc_type, begin_date, end_date, False) Then Exit Sub
            End If
            If doc_type(0) = 31 Then
                If Not ProcessDefectAct(doc_type, customer_sys_id, good_sys_id, history_id) Then Exit Sub
            ElseIf doc_type(0) = 42 Then
                If Not ProcessDefectActForGood(doc_type, customer_sys_id) Then Exit Sub
            ElseIf doc_type(0) = 32 Or doc_type(0) = 33 Or doc_type(0) = 34 Then
                If Not ProcessRepairRealizationAct(doc_type, customer_sys_id, good_sys_id, history_id) Then Exit Sub
            ElseIf doc_type(0) = 50 Then
                If Not ProcessRoute(Request.Params.Item("crs"), Request.Params.Item("region"), Request.Params.Item("g_t"), Request.Params.Item("date")) Then Exit Sub
            ElseIf doc_type(0) = 51 Then
                If Not ProcessRestReport(Request.Params.Item("group_id")) Then Exit Sub
            ElseIf doc_type(0) = 52 Then
                If Not ProcessWarehouseCardReport(Request.Params.Item("good_type_id")) Then Exit Sub
            ElseIf doc_type(0) = 53 Then
                If Not ProcessMasterTOReport(CDate(Request.QueryString("start_date")), CDate(Request.QueryString("end_date")), CInt(Request.QueryString("period")), Request.QueryString("ex")) Then Exit Sub
            ElseIf doc_type(0) = 54 Then
                If Not ProcessKKMListTO() Then Exit Sub
            ElseIf doc_type(0) = 10 Then
                If Not ProcessSupportDocuments(doc_type(0), customer_sys_id_s, good_sys_id_s) Then Exit Sub
            ElseIf doc_type(0) = 11 Or doc_type(0) = 12 Or doc_type(0) = 13 Or doc_type(0) = 14 Or doc_type(0) = 15 Or doc_type(0) = 16 Or doc_type(0) = 19 Or doc_type(0) = 20 Or doc_type(0) = 32 Or doc_type(0) = 55 Or doc_type(0) = 56 Then
                If Not ProcessSingleDocuments(doc_type, customer_sys_id, sale_sys_id, good_sys_id, history_id, sub_num, True) Then Exit Sub
            ElseIf doc_type(0) = 41 Or doc_type(0) = 42 Then
                If Not ProcessIzveschenieDocuments(doc_type, customer_sys_id, sale_sys_id, vid_plateza, sub_num, False) Then Exit Sub
            Else
                If Not ProcessDocuments(doc_type, customer_sys_id, sale_sys_id, rebilling, sub_num, False) Then Exit Sub
            End If
            'End If
            'достаем всю нужную инфо из базы
            Try
                Select Case doc_type(0)
                    Case 0 : doc_path = DocName0
                    Case 1 : doc_path = DocName1
                    Case 2 : doc_path = DocName2
                    Case 3 : doc_path = DocName3
                    Case 4 : doc_path = DocName4
                    Case 5 : doc_path = DocName5
                    Case 6 : doc_path = DocName6
                    Case 7 : doc_path = DocName7
                    Case 8 : doc_path = DocName8
                    Case 9 : doc_path = DocName9
                    Case 10 : doc_path = DocName10
                    Case 11 : doc_path = DocName11
                    Case 12 : doc_path = DocName12
                    Case 13 : doc_path = DocName13
                    Case 14 : doc_path = DocName14
                    Case 15 : doc_path = DocName15
                    Case 16 : doc_path = DocName16
                    Case 17 : doc_path = DocName17
                    Case 18 : doc_path = DocName18
                    Case 19 : doc_path = DocName19
                    Case 20 : doc_path = DocName20
                    Case 30 : doc_path = DocName30
                    Case 31 : doc_path = DocName31
                    Case 32 : doc_path = DocName32
                    Case 33 : doc_path = DocName33
                    Case 34 : doc_path = DocName34
                    Case 35 : doc_path = DocName35
                    Case 40 : doc_path = DocName40
                    Case 41 : doc_path = DocName41
                    Case 42 : doc_path = DocName42
                    Case 50 : doc_path = DocName50
                    Case 51 : doc_path = DocName51
                    Case 52 : doc_path = DocName52
                    Case 53 : doc_path = DocName53
                    Case 54 : doc_path = DocName54
                    Case 55 : doc_path = DocName55
                    Case 56 : doc_path = DocName56
                    Case 57 : doc_path = DocName57

                    Case Else
                        WriteError("Неверные параметры")
                        Exit Sub
                End Select

                If doc_type(0) = 4 Or doc_type(0) = 8 Or doc_type(0) = 9 Then
                    path = Server.MapPath("Docs/") & customer_sys_id & "\" & sale_sys_id & "\" & sub_num & doc_path
                ElseIf doc_type(0) = 11 Or doc_type(0) = 12 Or doc_type(0) = 13 Or doc_type(0) = 14 Or doc_type(0) = 15 Or doc_type(0) = 16 Or doc_type(0) = 19 Or doc_type(0) = 20 Or doc_type(0) = 55 Or doc_type(0) = 56 Then
                    path = Server.MapPath("Docs/") & customer_sys_id & "\" & sale_sys_id & "\" & good_sys_id & "\" & history_id & doc_path
                    'MsgBox(path)
                ElseIf doc_type(0) = 10 Then
                    path = Server.MapPath("Docs/") & customer_sys_id & "\Support\" & sale_sys_id & doc_path
                Else
                    path = Server.MapPath("Docs/") & customer_sys_id & "\" & sale_sys_id & "\" & doc_path
                End If
                'End If
                If doc_type(0) = 30 Or doc_type(0) = 40 Then
                    path = Server.MapPath("Docs/") & "Reports\" & Format(begin_date, "yyyyMMdd") & "-" & Format(end_date, "yyyyMMdd") & "\" & doc_path
                End If
                query = ""
                If doc_type(0) = 31 Or doc_type(0) = 42 Then
                    path = Server.MapPath("Docs/") & "DefectActs\" & doc_path
                End If
                If doc_type(0) = 32 Or doc_type(0) = 33 Or doc_type(0) = 34 Then
                    path = Server.MapPath("Docs/repair/") & doc_path
                End If
                If doc_type(0) = 50 Then
                    path = Server.MapPath("Docs/") & "Reports\" & Format(DateTime.Now, "yyyyMMdd") & "\" & doc_path
                End If
                If doc_type(0) = 51 Then
                    path = Server.MapPath("Docs/") & "Reports\Rest\" & Format(DateTime.Now, "yyyyMMdd") & "\" & doc_path
                End If
                If doc_type(0) = 52 Then
                    path = Server.MapPath("Docs/") & "Reports\Card\" & Request.Params.Item("good_type_id") & "\" & Format(DateTime.Now, "yyyyMMdd") & "\" & doc_path
                End If
                If doc_type(0) = 53 Then
                    path = Server.MapPath("Docs/") & "Reports\MasterTO\" & Format(DateTime.Now, "yyyyMMdd") & "\" & doc_path
                End If
                If doc_type(0) = 54 Then
                    path = Server.MapPath("Docs/") & "Reports\" & Format(DateTime.Now, "yyyyMMdd") & "\" & doc_path
                End If
                Dim fls As New IO.FileInfo(path)
                If Not fls.Exists Then
                    WriteError("Документ еще не готов. Попробуйте еще раз. (F5) 2")
                    Exit Sub
                End If

                Response.ClearContent()
                Response.ClearHeaders()

                If doc_type(0) = 30 Or doc_type(0) = 50 Or doc_type(0) = 51 Or doc_type(0) = 52 Or doc_type(0) = 53 Or doc_type(0) = 54 Then
                    Response.ContentType = "application/vnd.ms-excel"
                    Response.AddHeader("Content-Disposition", "attachment; filename=" & doc_path)
                    Threading.Thread.Sleep(2000)
                Else
                    Response.ContentType = "Application/msword"
                End If

                Response.WriteFile(path)
                Response.Flush()
                Response.Close()

            Catch ex As Exception

                If Err.Number = 53 Or Err.Number = 76 Then
                    WriteError("Документ еще не готов. Попробуйте еще раз. (F5) 3")
                Else
                    WriteError(Err.Description & "<BR>" & ex.ToString)
                End If

                Exit Sub
            Finally
            End Try
        End Sub

        Private Sub WriteError(ByVal s As String)
            Try
                Response.Write("<p align=center><font color=red size=5 face=Tahoma verdana>" & s & "</font></p>")
            Catch
            End Try
        End Sub

        Public Overrides Function ProcessDocuments(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal sale As Integer, Optional ByVal rebill As Integer = 0, Optional ByVal sub_num As Integer = -1, Optional ByVal isRefresh As Boolean = True) As Boolean

            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            Dim docFullPath$
            Dim boos_name$, customer_name$, accountant$, unn$, registration$, saler$, sDate$, dogovor$

            Dim path$ = Server.MapPath("Docs") & "\" & customer

            ProcessDocuments = True

            Dim customer_fax
            Dim customer_unn
            Dim customer_phone1
            Dim customer_phone2
            Dim customer_phone3
            Dim text_phones
            Dim customer_id = GetPageParam("c")

            Try

                If customer_id Then
                    reader = dbSQL.GetReader("select unn, phone1,phone2,phone3,phone4  FROM customer WHERE customer_sys_id='" & GetPageParam("c") & "'")

                    If reader.Read Then
                        customer_unn = reader("unn")
                        customer_fax = reader("phone1")
                        customer_phone1 = reader("phone2")
                        customer_phone2 = reader("phone3")
                        customer_phone3 = reader("phone4")
                    End If

                    reader.Close()

                    If customer_fax <> "" Then
                        text_phones &= " Факс: " & customer_fax
                    End If

                    If customer_phone1 <> "" Or customer_phone2 <> "" Or customer_phone3 <> "" Then
                        text_phones &= " Тел: " & customer_phone1 & " " & customer_phone2 & " " & customer_phone3
                    End If
                    If text_phones <> "" Then
                        text_phones = text_phones.ToString.Replace("  ", " ")
                    End If

                End If

            Catch ex As Exception
            End Try



            Try
                'Create folders and copy templates
                Dim fldr As New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If
                path = path & "\" & sale & "\"
                fldr = New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If
            Catch ex As Exception
                WriteError(Err.Description & "<BR>" & ex.ToString)
                ProcessDocuments = False
                Exit Function
            End Try

            Try
                'get data from database for specified sale
                'cn = New SqlClient.SqlConnection("data source=BY-MN-SRV1;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=BY-MN-SRV1;packet size=4096;password=webdb;")

                ds = New DataSet

                'sale
                cmd = New SqlClient.SqlCommand("get_sale_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                adapt = dbSQL.GetDataAdapter(cmd)
                If Not ds.Tables("sale") Is Nothing Then
                    ds.Tables("sale").Clear()
                End If
                adapt.Fill(ds, "sale")

                If ds.Tables("sale").Rows.Count = 0 Then GoTo ExitFunction

                saler = ds.Tables("sale").Rows(0)("saler")
                sDate = GetRussianDate(ds.Tables("sale").Rows(0)("sale_date"))

                If num_doc(0) = 10 Then
                    'support
                    cmd = New SqlClient.SqlCommand("get_support_info")
                    cmd.Parameters.AddWithValue("@pi_sys_id", sub_num)
                Else
                    'customer
                    cmd = New SqlClient.SqlCommand("get_customer_info")
                End If

                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
                adapt = dbSQL.GetDataAdapter(cmd)
                If Not ds.Tables("customer") Is Nothing Then
                    ds.Tables("customer").Clear()
                End If
                adapt.Fill(ds, "customer")

                If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction

                dogovor = ds.Tables("customer").Rows(0)("dogovor") & ds.Tables("sale").Rows(0)("dogovor")
                boos_name = ds.Tables("customer").Rows(0)("boos_name")
                customer_name = ds.Tables("customer").Rows(0)("customer_name")
                accountant = ds.Tables("customer").Rows(0)("accountant")
                unn = ds.Tables("customer").Rows(0)("unn")
                registration = ds.Tables("customer").Rows(0)("registration")
                If rebill = 0 Then
                    ' get list of goods for specified sale
                    cmd = New SqlClient.SqlCommand("get_goods_info_by_sale2")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                Else
                    cmd = New SqlClient.SqlCommand("prc_rpt_Rebilling")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                    cmd.Parameters.AddWithValue("@pi_owner_id", customer)
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                If Not ds.Tables("goods") Is Nothing Then
                    ds.Tables("goods").Clear()
                End If
                adapt.Fill(ds, "goods")

                If ds.Tables("goods").Rows.Count = 0 Then
                    WriteError("Не выбраны товары для данного клиента и продажи")
                    ProcessDocuments = False
                    GoTo ExitFunction
                End If

            Catch ex As Exception

                WriteError("Загрузка данных<br>" & Err.Description)
                ProcessDocuments = False
                GoTo ExitFunction

            End Try


            Try
                ' Create instance of Word!
                wrdApp = New Word.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessDocuments = False
                GoTo ExitFunction
            End Try

            'Dim tbl1 As Table, tbl2 As Table
            Dim r1 As Word.Row, r2 As Word.Row
            Dim i%, j%, k%, n%, l%, iLower%, iUpper%
            Dim q, p, dNDS, dTotal, sSum, sNDS, CPP, dTotalQuantity As Double
            Dim Nadbavka As Boolean
            Dim iCount%, iGoodType%, sTmp$

            For k = num_doc.GetLowerBound(0) To num_doc.GetUpperBound(0)
                dNDS = 0
                dTotal = 0
                If num_doc(k) = 0 Then

                    '''''''''''''''''''''
                    'Счет-фактура по НДС'
                    '''''''''''''''''''''

                    Try
                        docFullPath = path & DocName0

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then
                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If
                            IO.File.Copy(Server.MapPath("Templates/") & DocName0, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("RECIPIENT_ADDRESS").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                            doc.Bookmarks("RECIPIENT_NAME").Range.Text = customer_name
                            doc.Bookmarks("RECIPIENT_UNN").Range.Text = unn
                            doc.Bookmarks("Date").Range.Text = sDate

                            iGoodType = -1
                            j = 1

                            'тут проверяем есть ли ккм в продаже и есть ли услуги
                            Dim reader As SqlClient.SqlDataReader
                            Dim sale_sys_id = GetPageParam("s")
                            Dim num_cash
                            Dim query = "SELECT info, (SELECT COUNT(*) AS Expr1 FROM good WHERE (sale_sys_id = " & sale_sys_id & ") AND (num_cashregister <> '')) as num_kass from sale WHERE sale.sale_sys_id=" & sale_sys_id & " ORDER BY sale_sys_id DESC"
                            reader = dbSQL.GetReader(query)
                            Dim info
                            If reader.Read() Then
                                Try
                                    info = reader.Item(0)
                                    num_cash = reader.Item(1)
                                    If info = "" Or info Is DBNull.Value Then info = 0
                                    If num_cash = "" Or info Is DBNull.Value Then num_cash = 0
                                    'MsgBox(info & ";" & num_cash)
                                Catch
                                End Try
                            Else
                            End If
                            reader.Close()

                            If Not num_cash Is DBNull.Value And Not info Is DBNull.Value Then
                                'Если кассовые аппараты и надо выставить услуги
                                q = 50000
                                p = num_cash
                                If info = 1 Or info = 3 Then
                                    r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(j + 2))
                                    r1.Cells(1).Range.Text = "Оформление документов для ИМНС"

                                    r1.Cells(2).Range.Text = q * p
                                    r1.Cells(3).Range.Text = 0
                                    r1.Cells(4).Range.Text = 20
                                    r1.Cells(5).Range.Text = q * p * 0.2
                                    r1.Cells(6).Range.Text = q * p * 1.2
                                    j = j + 1
                                End If

                                If info = 1 Or info = 2 Then
                                    r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(j + 2))
                                    r1.Cells(1).Range.Text = "Техническое обслуживание на 2 месяца"

                                    r1.Cells(2).Range.Text = q * p
                                    r1.Cells(3).Range.Text = 0
                                    r1.Cells(4).Range.Text = 20
                                    r1.Cells(5).Range.Text = q * p * 0.2
                                    r1.Cells(6).Range.Text = q * p * 1.2
                                    j = j + 1
                                    q = q * 2
                                End If
                            Else
                                For i = 0 To ds.Tables("goods").Rows.Count - 1
                                    If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then
                                        If i <> 0 Then
                                            r1.Cells(2).Range.Text = q * p
                                            r1.Cells(3).Range.Text = 0
                                            r1.Cells(4).Range.Text = 20
                                            r1.Cells(5).Range.Text = q * p * 0.2
                                            r1.Cells(6).Range.Text = q * p * 1.2
                                            dTotal = dTotal + q * p
                                        End If
                                        iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
                                        r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(j + 2))
                                        j = j + 1
                                        q = 0
                                        p = CDbl(ds.Tables("goods").Rows(i)("price"))
                                        r1.Cells(1).Range.Text = ds.Tables("goods").Rows(i)("good_name")
                                    End If
                                    q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))
                                Next
                            End If

                            dTotal = dTotal + q * p

                            doc.Bookmarks("TotalNDS").Range.Text = dTotal * 0.2
                            doc.Bookmarks("TotalAll").Range.Text = dTotal * 1.2
                            doc.Bookmarks("Total").Range.Text = dTotal
                            doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dTotal * 0.2)
                            doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal * 1.2)

                            doc.Save()
                        End If

                    Catch
                        WriteError("Счет-фактура  по НДС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 1 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Договор - Счет-фактура
                    Dim trace = ""

                    Try
                        docFullPath = path & DocName1

                        trace = "tr1"

                        fl = New IO.FileInfo(docFullPath)

                        trace = "tr2"

                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            trace = "tr4"
                            IO.File.Copy(Server.MapPath("Templates/") & DocName1, docFullPath, True)
                            trace = "tr4"
                            doc = wrdApp.Documents.Open(docFullPath)
                            trace = "tr5"
                            doc.Bookmarks("Registration").Range.Text = registration
                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("Customer").Range.Text = customer_name & ", " & ds.Tables("customer").Rows(0)("customer_address") & ", " & ds.Tables("customer").Rows(0)("bank") & ", УНП:" & customer_unn & ", " & text_phones
                            doc.Bookmarks("BoosName").Range.Text = boos_name
                            doc.Bookmarks("Dogovor").Range.Text = dogovor
                            'doc.Bookmarks("Dogovor2").Range.Text = dogovor
                            doc.Bookmarks("Dogovor3").Range.Text = dogovor
                            doc.Bookmarks("Date").Range.Text = sDate
                            'doc.Bookmarks("Date2").Range.Text = sDate
                            trace = "tr6"
                            doc.Bookmarks("Date3").Range.Text = sDate
                            Dim sFirmName$ = ds.Tables("sale").Rows(0)("firm_name")
                            doc.Bookmarks("FirmName").Range.Text = sFirmName
                            If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
                                doc.Bookmarks("FirmName2").Range.Text = sFirmName
                                doc.Bookmarks("FirmName3").Range.Text = sFirmName
                                doc.Bookmarks("rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
                                doc.Bookmarks("Ustav").Range.Text = ds.Tables("sale").Rows(0)("ustav")
                                doc.Bookmarks("Razreshil").Range.Text = ds.Tables("sale").Rows(0)("razreshil")
                            End If
                            trace = "tr7"
                            iGoodType = -1
                            j = 0

                            For i = 0 To ds.Tables("goods").Rows.Count - 1

                                If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then
                                    If i <> 0 Then
                                        sSum = Math.Round(q * p * 1.2, 2) 'Math.Round(q * (p * 1.18) / 10) * 10
                                        sNDS = sSum - (p * q)
                                        r1.Cells(3).Range.Text = q
                                        r1.Cells(4).Range.Text = p
                                        r1.Cells(5).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                                        r1.Cells(6).Range.Text = String.Format("{0:0.00}", sNDS)
                                        r1.Cells(7).Range.Text = String.Format("{0:0.00}", sSum)

                                        r2.Cells(2).Range.Text = String.Format("{0:0.00}", q)
                                        r2.Cells(5).Range.Text = String.Format("{0:0.00}", p)
                                        r2.Cells(6).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                                        r2.Cells(7).Range.Text = "20"
                                        r2.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                                        r2.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)

                                        dNDS = dNDS + sNDS
                                        dTotal = dTotal + sSum
                                    End If

                                    iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
                                    r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(j + 2))
                                    r2 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(j + 2))
                                    j = j + 1

                                    q = 0
                                    p = CDbl(ds.Tables("goods").Rows(i)("price"))

                                    r1.Cells(1).Range.Text = ds.Tables("goods").Rows(i)("good_name")
                                    r1.Cells(2).Range.Text = "шт."

                                    r2.Cells(1).Range.Text = ds.Tables("goods").Rows(i)("good_name")
                                    Nadbavka = False
                                    If Not IsDBNull(ds.Tables("goods").Rows(i)("nadbavka1")) Then
                                        Nadbavka = ds.Tables("goods").Rows(i)("nadbavka1")
                                    Else
                                        '//Изменил добавил это
                                        If Not IsDBNull(ds.Tables("goods").Rows(i)("nadbavka")) Then
                                            Nadbavka = ds.Tables("goods").Rows(i)("nadbavka")
                                        End If
                                        '//
                                    End If

                                    If Not Nadbavka Then
                                        CPP = p
                                    Else
                                        CPP = ds.Tables("goods").Rows(i)("price_in")
                                    End If
                                    r2.Cells(3).Range.Text = CPP
                                    r2.Cells(4).Range.Text = CStr(Math.Round(((p / CPP - 1) * 100), 4))
                                End If
                                q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))
                            Next
                            trace = "tr8"
                            sSum = Math.Round(q * p * 1.2, 2) 'Math.Round(q * (p * 1.18) / 10) * 10
                            sNDS = sSum - (p * q)
                            r1.Cells(3).Range.Text = String.Format("{0:0.00}", q)
                            r1.Cells(4).Range.Text = String.Format("{0:0.00}", p)
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(7).Range.Text = String.Format("{0:0.00}", sSum)

                            r2.Cells(5).Range.Text = String.Format("{0:0.00}", p)
                            r2.Cells(6).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                            r2.Cells(7).Range.Text = "20"
                            r2.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                            r2.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)
                            r2.Cells(2).Range.Text = String.Format("{0:0.00}", q)

                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                            trace = "tr9"
                            doc.Bookmarks("TotalNDS").Range.Text = String.Format("{0:0.00}", dNDS)
                            doc.Bookmarks("Total").Range.Text = String.Format("{0:0.00}", dTotal)
                            doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                            doc.Bookmarks("TotalPropis").Range.Text = Summa_propis(dTotal)
                            trace = "tr10"
                            doc.Save()
                            trace = "tr11"
                        End If

                    Catch
                        WriteError("Договор - Счет-фактура test " & trace & "<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 2 Then
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Заявление на книгу кассира

                    Try
                        docFullPath = path & DocName2

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName2, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("BoosName").Range.Text = boos_name
                            doc.Bookmarks("Accountant").Range.Text = accountant
                            doc.Bookmarks("UNN").Range.Text = unn
                            doc.Bookmarks("Date").Range.Text = sDate
                            'doc.Bookmarks("SetPlace").Range.Text = ""
                            doc.Save()
                        End If
                    Catch
                        WriteError("Заявление на книгу кассира<br>" & Err.Description)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 3 Then
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Заявление

                    Try
                        docFullPath = path & DocName3

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName3, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("BoosName").Range.Text = boos_name
                            doc.Bookmarks("Boos").Range.Text = boos_name & " " & ds.Tables("customer").Rows(0)("customer_phone")
                            doc.Bookmarks("Customer").Range.Text = customer_name & " " & ds.Tables("customer").Rows(0)("customer_address")
                            doc.Bookmarks("Accountant").Range.Text = accountant
                            doc.Bookmarks("AccountantName").Range.Text = accountant
                            doc.Bookmarks("UNN").Range.Text = unn
                            doc.Bookmarks("Bank").Range.Text = ds.Tables("customer").Rows(0)("bank") & ", УНН:" & customer_unn
                            doc.Bookmarks("Branch").Range.Text = ds.Tables("customer").Rows(0)("branch")
                            doc.Bookmarks("Registration").Range.Text = registration
                            doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                            doc.Bookmarks("Date").Range.Text = sDate
                            doc.Bookmarks("Date2").Range.Text = sDate

                            iCount = 0
                            For i = 0 To ds.Tables("goods").Rows.Count - 1

                                If ds.Tables("goods").Rows(i)("is_cashregister") Then iCount = iCount + 1

                            Next
                            doc.Bookmarks("CountKKM").Range.Text = iCount

                            doc.Save()
                        End If

                    Catch
                        WriteError("Заявление<br>" & Err.Description)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try
                End If
                If num_doc(k) = 4 Then
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Акт о снятии показаний счетчика при постановке на учет

                    Try

                        If sub_num = -1 Then
                            iLower = 0
                            iUpper = ds.Tables("goods").Rows.Count - 1
                        Else
                            iLower = sub_num
                            iUpper = sub_num
                        End If

                        For n = iLower To iUpper
                            If ds.Tables("goods").Rows(n)("is_cashregister") Then

                                docFullPath = path & n & DocName4

                                ' get cash history 
                                cmd = New SqlClient.SqlCommand("get_cashowner_history")
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("@good_sys_id", ds.Tables("goods").Rows(n)("good_sys_id"))
                                adapt = dbSQL.GetDataAdapter(cmd)
                                If Not ds.Tables("cash") Is Nothing Then
                                    ds.Tables("cash").Clear()
                                End If
                                adapt.Fill(ds, "cash")

                                If ds.Tables("cash").Rows.Count = 0 Then
                                    WriteError("Не выбраны товары для данного клиента и продажи")
                                    ProcessDocuments = False
                                    GoTo ExitFunction
                                End If
                                fl = New IO.FileInfo(docFullPath)
                                If (Not fl.Exists()) Or isRefresh Then
                                    If fl.Exists() Then
                                        Try
                                            fl.Delete()
                                        Catch
                                        End Try
                                    End If

                                    IO.File.Copy(Server.MapPath("Templates/") & DocName4, docFullPath, True)

                                    doc = wrdApp.Documents.Open(docFullPath)

                                    Try
                                        doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(0)("good_name")
                                        doc.Bookmarks("Version").Range.Text = ds.Tables("cash").Rows(0)("version")
                                        doc.Bookmarks("boos_name").Range.Text = boos_name
                                        doc.Bookmarks("CustomerName").Range.Text = customer_name
                                        doc.Bookmarks("Saler").Range.Text = ds.Tables("cash").Rows(0)("executor")
                                        doc.Bookmarks("Saler2").Range.Text = ds.Tables("cash").Rows(0)("executor")
                                        doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("cash").Rows(0)("worker_document")
                                        doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                                        doc.Bookmarks("num_cashregister").Range.Text = add_nulls(ds.Tables("cash").Rows(0)("num_cashregister"))
                                        doc.Bookmarks("Reestr").Range.Text = ds.Tables("cash").Rows(0)("marka_reestr_out")
                                        doc.Bookmarks("PZU").Range.Text = ds.Tables("cash").Rows(0)("marka_pzu_out")
                                        doc.Bookmarks("MFP").Range.Text = ds.Tables("cash").Rows(0)("marka_mfp_out")
                                        doc.Bookmarks("CTO").Range.Text = ds.Tables("cash").Rows(0)("marka_cto_out")
                                        doc.Bookmarks("CTO2").Range.Text = ds.Tables("cash").Rows(0)("marka_cto2_out")
                                        doc.Bookmarks("CP").Range.Text = ds.Tables("cash").Rows(0)("marka_cp_out")
                                        doc.Bookmarks("ZReport").Range.Text = ds.Tables("cash").Rows(0)("zreport_out")
                                        doc.Bookmarks("Itog").Range.Text = ds.Tables("cash").Rows(0)("itog_out") & "(" & IIf(Summa_propis(ds.Tables("cash").Rows(0)("itog_out")) = "", "ноль", Summa_propis(ds.Tables("cash").Rows(0)("itog_out"))) & ")"
                                        doc.Bookmarks("DateDismissal").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(0)("support_date"))
                                    Catch ex As Exception

                                    End Try


                                    'doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("goods").Rows(n)("good_name")
                                    'doc.Bookmarks("Version").Range.Text = ds.Tables("goods").Rows(n)("version")
                                    'doc.Bookmarks("boos_name").Range.Text = boos_name
                                    'doc.Bookmarks("CustomerName").Range.Text = customer_name
                                    ''doc.Bookmarks("kassir").Range.Text = ds.Tables("goods").Rows(n)("kassir1") & " " & ds.Tables("goods").Rows(n)("kassir2")
                                    'doc.Bookmarks("Saler").Range.Text = ds.Tables("goods").Rows(n)("worker")
                                    'doc.Bookmarks("Saler2").Range.Text = ds.Tables("goods").Rows(n)("worker")
                                    'doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("goods").Rows(0)("worker_document")
                                    'doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                                    'doc.Bookmarks("num_cashregister").Range.Text = ds.Tables("goods").Rows(n)("num_cashregister")
                                    'doc.Bookmarks("Reestr").Range.Text = ds.Tables("goods").Rows(n)("num_control_reestr")
                                    'doc.Bookmarks("PZU").Range.Text = ds.Tables("goods").Rows(n)("num_control_pzu")
                                    'doc.Bookmarks("MFP").Range.Text = ds.Tables("goods").Rows(n)("num_control_mfp")
                                    'doc.Bookmarks("CTO").Range.Text = ds.Tables("goods").Rows(n)("num_control_cto")
                                    'doc.Bookmarks("ZReport").Range.Text = ds.Tables("cash").Rows(n)("zreport")
                                    'doc.Bookmarks("Itog").Range.Text = ds.Tables("cash").Rows(n)("itog")
                                    'doc.Bookmarks("DateDismissal").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("support_date"))
                                    doc.Save()
                                End If
                            End If
                        Next
                    Catch
                        WriteError("Акт о снятии показаний счетчика<br>" & Err.Description)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If


                If num_doc(k) = 5 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Товарная накладная
                    ''''''
                    Try
                        docFullPath = path & DocName5

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            fls.Copy(Server.MapPath("Templates/") & DocName5, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)
                            doc.Bookmarks("Boos").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                            doc.Bookmarks("Boos2").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                            doc.Bookmarks("CustomerAddress").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("Dogovor").Range.Text = dogovor
                            Dim s$ = ds.Tables("customer").Rows(0)("bank") & ", УНН:" & customer_unn
                            If s.Trim.Length = 0 Then s = "нет"
                            'doc.Bookmarks("Bank").Range.Text = s
                            doc.Bookmarks("UNN1").Range.Text = unn
                            doc.Bookmarks("UNN2").Range.Text = unn
                            doc.Bookmarks("Date").Range.Text = sDate
                            doc.Bookmarks("Date2").Range.Text = sDate
                            doc.Bookmarks("Razreshil").Range.Text = ds.Tables("sale").Rows(0)("razreshil")
                            If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
                                doc.Bookmarks("FirmName1").Range.Text = ds.Tables("sale").Rows(0)("firm_name")
                                'doc.Bookmarks("Rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
                                doc.Bookmarks("Employee").Range.Text = ds.Tables("sale").Rows(0)("fio")
                            Else
                                'кто сделал все это
                                Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
                                If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                                Else
                                    doc.Bookmarks("Employee").Range.Text = sEmployee
                                End If
                            End If

                            iGoodType = -1
                            Dim num_cash_str$ = ""
                            j = 1
                            For i = 0 To ds.Tables("goods").Rows.Count - 1

                                If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then
                                    If i <> 0 Then
                                        num_cash_str = ""
                                        If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                            num_cash_str = " №"
                                            For l = 0 To ds.Tables("goods").Rows.Count - 1
                                                If (ds.Tables("goods").Rows(l)("is_cashregister")) And (ds.Tables("goods").Rows(l)("good_type_sys_id") = iGoodType) Then
                                                    num_cash_str = num_cash_str & ds.Tables("goods").Rows(l)("num_cashregister") & " "
                                                End If
                                            Next
                                            If (num_cash_str = " №") Then
                                                num_cash_str = ""
                                            End If
                                        End If

                                        r1.Cells(1).Range.Text = j - 1
                                        r1.Cells(1).Range.Text = j - 1
                                        r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i - 1)("good_name")
                                        r1.Cells(2).Range.InsertAfter(num_cash_str)
                                        r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-" & ds.Tables("goods").Rows(i - 1)("country") & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))

                                        'r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-РФ" & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))
                                        r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i - 1)("units")
                                        r1.Cells(4).Range.Text = q 'убрано 2.02.2010

                                        'If ds.Tables("goods").Rows(i - 1)("is_cashregister") Or iGoodType = 3 Or iGoodType = 6 Then

                                        Nadbavka = False

                                        If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("nadbavka1")) Then
                                            Nadbavka = ds.Tables("goods").Rows(i - 1)("nadbavka1")
                                        End If

                                        If Not Nadbavka Then
                                            'If Not ds.Tables("goods").Rows(i - 1)("nadbavka1") Then
                                            CPP = p
                                        Else
                                            CPP = ds.Tables("goods").Rows(i - 1)("price_in")
                                            'CStr(CInt(1000 * (p / ds.Tables("goods").Rows(i - 1)("price_in") - 1)) / 10)
                                        End If

                                        sSum = Math.Round(q * p * 1.2, 2) 'Math.Round(q * (p * 1.18) / 10) * 10
                                        sNDS = (sSum - (p * q)) 'sSum - Math.Round(sSum / 1.18)
                                        r1.Cells(5).Range.Text = String.Format("{0:0.00}", CPP)
                                        r1.Cells(6).Range.Text = String.Format("{0:0.00}", CStr(Math.Round((p / CPP - 1) * 100, 4)))
                                        r1.Cells(6).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                                        r1.Cells(7).Range.Text = "20"
                                        r1.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                                        r1.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)

                                        If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                            'r1.Cells(11).Range.Text = "кор"
                                            'r1.Cells(12).Range.Text = q
                                            'r1.Cells(13).Range.Text = "1"
                                        End If

                                        dNDS = dNDS + sNDS
                                        dTotal = dTotal + sSum
                                    End If

                                    iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
                                    num_cash_str = ""
                                    If ds.Tables("goods").Rows(i)("is_cashregister") Then

                                        num_cash_str = " №"
                                        For l = 0 To ds.Tables("goods").Rows.Count - 1
                                            If (ds.Tables("goods").Rows(l)("is_cashregister")) And (ds.Tables("goods").Rows(l)("good_type_sys_id") = iGoodType) Then
                                                num_cash_str = num_cash_str & ds.Tables("goods").Rows(l)("num_cashregister") & " "
                                            End If
                                        Next
                                        If (num_cash_str = " №") Then
                                            num_cash_str = ""
                                        End If
                                    End If
                                    r1 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(j + 2))
                                    j = j + 1

                                    q = 0

                                    p = CDbl(ds.Tables("goods").Rows(i)("price"))
                                End If


                                If Not IsDBNull(ds.Tables("goods").Rows(i)("nadbavka1")) Then
                                    Nadbavka = ds.Tables("goods").Rows(i)("nadbavka1")
                                End If

                                Try
                                    If Not Nadbavka Then
                                        'Информация о надбавке и т.д.
                                        r1.Cells(10).Range.Text = "Цена производителя-импортера:" & ds.Tables("goods").Rows(i)("price_in") & vbCrLf & _
                                        "Размер оптовой надбавки: 0"
                                    Else
                                        'Информация о надбавке и т.д.
                                        r1.Cells(10).Range.Text = "Цена производителя-импортера:" & ds.Tables("goods").Rows(i)("price_in") & vbCrLf & _
                                        "Размер оптовой надбавки: 20"
                                    End If
                                Catch
                                End Try

                                q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))
                            Next

                            r1.Cells(1).Range.Text = j - 1
                            r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i - 1)("good_name")
                            r1.Cells(2).Range.InsertAfter(num_cash_str)
                            r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-" & ds.Tables("goods").Rows(i - 1)("country") & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))
                            'r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-РФ" & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))


                            If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("units")) Then
                                r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i - 1)("units")
                            End If

                            r1.Cells(4).Range.Text = q

                            'If ds.Tables("goods").Rows(i - 1)("is_cashregister") Or iGoodType = 3 Or iGoodType = 6 Then
                            Nadbavka = False
                            If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("nadbavka1")) Then
                                Nadbavka = ds.Tables("goods").Rows(i - 1)("nadbavka1")
                            End If
                            If Not Nadbavka Then
                                'If Not ds.Tables("goods").Rows(i - 1)("nadbavka1") Then
                                CPP = p
                            Else
                                CPP = ds.Tables("goods").Rows(i - 1)("price_in")
                            End If
                            sSum = Math.Round(q * p * 1.2, 0)
                            sNDS = (sSum - (p * q))
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", CPP)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", CStr(Math.Round((p / CPP - 1) * 100, 4)))
                            r1.Cells(6).Range.Text = (sSum - sNDS)
                            r1.Cells(7).Range.Text = "20"
                            r1.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)
                            '
                            If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                'r1.Cells(11).Range.Text = "кор"
                                'r1.Cells(12).Range.Text = q
                                'r1.Cells(13).Range.Text = "1"
                            End If

                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                            doc.Bookmarks("TotalNDS").Range.Text = dNDS
                            doc.Bookmarks("TotalAll").Range.Text = dTotal
                            doc.Bookmarks("Total").Range.Text = dTotal - dNDS
                            doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                            doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                            doc.Bookmarks("Count").Range.Text = Summa_propis(j - 1, False)

                            doc.Save()
                        End If

                    Catch
                        WriteError("Товарная накладная<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try
                End If

                If num_doc(k) = 35 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    '      Товарно-транспортная накладная 
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Try
                        docFullPath = path & DocName35

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName35, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("Driver").Range.Text = ""
                            'doc.Bookmarks("Driver1").Range.Text = ""
                            doc.Bookmarks("Driver2").Range.Text = ""
                            'doc.Bookmarks("CustomerAddress").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                            'doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("OsnovanieOtpuska").Range.Text = "Договор № б/н от " & sDate
                            'doc.Bookmarks("ChelPriobretenia").Range.Text = "Для оптовой торговли  "
                            doc.Bookmarks("PutevList").Range.Text = ""
                            doc.Bookmarks("TransportForm").Range.Text = "авто"
                            doc.Bookmarks("Automobil").Range.Text = ""
                            doc.Bookmarks("PunktPogruzki").Range.Text = "г.Минск,ул.Лермонтова,29"
                            doc.Bookmarks("PunktRazgruzki").Range.Text = ""
                            doc.Bookmarks("Massa1").Range.Text = ""
                            'doc.Bookmarks("Massa2").Range.Text = ""

                            Dim s$ = customer_name & " , " & ds.Tables("customer").Rows(0)("customer_address") & " , " & ds.Tables("customer").Rows(0)("bank") & ", УНН:" & customer_unn

                            If s.Trim.Length = 0 Then s = "нет"

                            doc.Bookmarks("Customer").Range.Text = s
                            doc.Bookmarks("Razgruzka").Range.Text = customer_name
                            doc.Bookmarks("Gruzopoluchatel").Range.Text = s
                            doc.Bookmarks("UNN11").Range.Text = "100001879"
                            doc.Bookmarks("UNN21").Range.Text = "100001879"
                            doc.Bookmarks("UNN12").Range.Text = unn
                            doc.Bookmarks("UNN22").Range.Text = unn
                            doc.Bookmarks("UNN13").Range.Text = unn
                            doc.Bookmarks("UNN23").Range.Text = unn
                            doc.Bookmarks("Date").Range.Text = sDate
                            'doc.Bookmarks("Date2").Range.Text = sDate
                            doc.Bookmarks("Razreshil").Range.Text = ds.Tables("sale").Rows(0)("razreshil")
                            If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
                                doc.Bookmarks("FirmName1").Range.Text = ds.Tables("sale").Rows(0)("firm_name")
                                doc.Bookmarks("Rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
                                doc.Bookmarks("Employee").Range.Text = ds.Tables("sale").Rows(0)("fio")
                            Else
                                'кто сделал все это

                                Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
                                If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                                Else
                                    doc.Bookmarks("Employee").Range.Text = sEmployee
                                End If
                            End If

                            iGoodType = -1
                            Dim num_cash_str$ = ""
                            dTotalQuantity = 0
                            j = 1

                            For i = 0 To ds.Tables("goods").Rows.Count - 1

                                If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then
                                    If i <> 0 Then
                                        num_cash_str = ""
                                        If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                            num_cash_str = " №"
                                            For l = 0 To ds.Tables("goods").Rows.Count - 1
                                                If (ds.Tables("goods").Rows(l)("is_cashregister")) And (ds.Tables("goods").Rows(l)("good_type_sys_id") = iGoodType) Then
                                                    num_cash_str = num_cash_str & ds.Tables("goods").Rows(l)("num_cashregister") & " "
                                                End If
                                            Next

                                            If (num_cash_str = " №") Then
                                                num_cash_str = ""
                                            End If
                                        End If
                                        r1.Cells(1).Range.Text = j - 1
                                        'r1.Cells(1).Range.Text = j - 1
                                        r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i - 1)("good_name")
                                        r1.Cells(2).Range.InsertAfter(num_cash_str)
                                        r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-" & ds.Tables("goods").Rows(i - 1)("country") & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))

                                        'r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-РФ" & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))


                                        If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("units")) Then
                                            r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i - 1)("units")
                                        End If


                                        r1.Cells(4).Range.Text = q
                                        'If ds.Tables("goods").Rows(i - 1)("is_cashregister") Or iGoodType = 3 Or iGoodType = 6 Then
                                        Nadbavka = False
                                        If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("nadbavka1")) Then
                                            Nadbavka = ds.Tables("goods").Rows(i - 1)("nadbavka1")
                                        End If
                                        If Not Nadbavka Then
                                            'If Not ds.Tables("goods").Rows(i - 1)("nadbavka1") Then
                                            CPP = p
                                        Else
                                            CPP = ds.Tables("goods").Rows(i - 1)("price_in")
                                            'CStr(CInt(1000 * (p / ds.Tables("goods").Rows(i - 1)("price_in") - 1)) / 10)
                                        End If
                                        sSum = Math.Round(q * p * 1.2, 2) 'Math.Round(q * (p * 1.18) / 10) * 10
                                        sNDS = (sSum - (p * q)) 'sSum - Math.Round(sSum / 1.18)
                                        r1.Cells(5).Range.Text = CPP
                                        'r1.Cells(6).Range.Text = CStr(Math.Round((p / CPP - 1) * 100, 4))
                                        r1.Cells(6).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                                        r1.Cells(7).Range.Text = "20"
                                        r1.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                                        r1.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)
                                        If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                            'r1.Cells(11).Range.Text = "кор"
                                            r1.Cells(10).Range.Text = q
                                            r1.Cells(11).Range.Text = "1"
                                        Else
                                            'r1.Cells(11).Range.Text = "кор"
                                            r1.Cells(10).Range.Text = q
                                            r1.Cells(11).Range.Text = "1"
                                        End If
                                        dNDS = dNDS + sNDS
                                        dTotal = dTotal + sSum
                                        dTotalQuantity = dTotalQuantity + q
                                    End If

                                    iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
                                    num_cash_str = ""
                                    If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                        num_cash_str = " №"
                                        For l = 0 To ds.Tables("goods").Rows.Count - 1
                                            If (ds.Tables("goods").Rows(l)("is_cashregister")) And (ds.Tables("goods").Rows(l)("good_type_sys_id") = iGoodType) Then
                                                num_cash_str = num_cash_str & ds.Tables("goods").Rows(l)("num_cashregister") & " "
                                            End If
                                        Next
                                        If (num_cash_str = " №") Then
                                            num_cash_str = ""
                                        End If
                                    End If
                                    r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(j + 2))
                                    j = j + 1

                                    q = 0

                                    p = CDbl(ds.Tables("goods").Rows(i)("price"))
                                End If

                                If Not IsDBNull(ds.Tables("goods").Rows(i)("nadbavka1")) Then
                                    Nadbavka = ds.Tables("goods").Rows(i)("nadbavka1")
                                End If

                                Try
                                    If Not Nadbavka Then
                                        'Информация о надбавке и т.д.
                                        r1.Cells(12).Range.Text = "Цена производителя-импортера:" & ds.Tables("goods").Rows(i)("price_in") & vbCrLf & _
                                        "Размер оптовой надбавки: 0"
                                    Else
                                        'Информация о надбавке и т.д.
                                        r1.Cells(12).Range.Text = "Цена производителя-импортера:" & ds.Tables("goods").Rows(i)("price_in") & vbCrLf & _
                                        "Размер оптовой надбавки: 20"
                                    End If
                                Catch
                                End Try

                                q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))
                            Next

                            r1.Cells(1).Range.Text = j - 1
                            r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i - 1)("good_name")
                            r1.Cells(2).Range.InsertAfter(num_cash_str)
                            r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-" & ds.Tables("goods").Rows(i - 1)("country") & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))
                            'r1.Cells(2).Range.InsertAfter(vbCrLf & "Страна ввоза-РФ" & vbCrLf & ds.Tables("goods").Rows(i - 1)("pricelist"))

                            If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("units")) Then
                                r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i - 1)("units")
                            End If

                            r1.Cells(4).Range.Text = q
                            'If ds.Tables("goods").Rows(i - 1)("is_cashregister") Or iGoodType = 3 Or iGoodType = 6 Then
                            Nadbavka = False
                            If Not IsDBNull(ds.Tables("goods").Rows(i - 1)("nadbavka1")) Then
                                Nadbavka = ds.Tables("goods").Rows(i - 1)("nadbavka1")
                            End If
                            If Not Nadbavka Then
                                'If Not ds.Tables("goods").Rows(i - 1)("nadbavka1") Then
                                CPP = p
                            Else
                                CPP = ds.Tables("goods").Rows(i - 1)("price_in")
                            End If

                            sSum = Math.Round(q * p * 1.2, 2)
                            sNDS = (sSum - (p * q))
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", CPP)
                            'r1.Cells(6).Range.Text = CStr(Math.Round((p / CPP - 1) * 100, 4))
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", (sSum - sNDS))
                            r1.Cells(7).Range.Text = "20"
                            r1.Cells(8).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(9).Range.Text = String.Format("{0:0.00}", sSum)

                            If ds.Tables("goods").Rows(i - 1)("is_cashregister") Then
                                'r1.Cells(11).Range.Text = "кор"
                                r1.Cells(10).Range.Text = q
                                r1.Cells(11).Range.Text = "1"
                            Else
                                'r1.Cells(11).Range.Text = "кор"
                                r1.Cells(10).Range.Text = q
                                r1.Cells(11).Range.Text = "1"
                            End If

                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                            dTotalQuantity = dTotalQuantity + q
                            doc.Bookmarks("TotalQuantity").Range.Text = dTotalQuantity
                            doc.Bookmarks("TotalNDS").Range.Text = String.Format("{0:0.00}", dNDS)
                            doc.Bookmarks("TotalAll").Range.Text = String.Format("{0:0.00}", dTotal)
                            doc.Bookmarks("Total").Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
                            doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                            doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                            'doc.Bookmarks("CountGoods").Range.Text = Summa_propis(j - 1, False)
                            doc.Bookmarks("CountPlace").Range.Text = Summa_propis(dTotalQuantity, False)

                            doc.Save()
                        End If
                    Catch
                        WriteError("Товарная накладная<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If
                If num_doc(k) = 6 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Договор на техническое обслуживание
                    Try
                        Dim s$

                        docFullPath = path & DocName6

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then
                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName6, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("BoosName").Range.Text = boos_name
                            doc.Bookmarks("Registration").Range.Text = registration
                            If ds.Tables("customer").Rows(0)("NDS") <> "" Then
                                'doc.Bookmarks("NDS").Range.Text = ""
                            End If
                            Dim a As Integer = 0
                            For i = 0 To ds.Tables("goods").Rows.Count - 1
                                If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                    r2 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                    a = a + 1
                                    r2.Cells(1).Range.Text = a
                                    r2.Cells(2).Range.Text = ds.Tables("goods").Rows(i)("good_name")
                                    r2.Cells(3).Range.Text = add_nulls(ds.Tables("goods").Rows(i)("num_cashregister"))
                                    r2.Cells(4).Range.Text = ds.Tables("goods").Rows(i)("set_place")
                                    If rebill = 0 Then
                                        r2.Cells(5).Range.Text = "Новый"
                                    Else
                                        r2.Cells(5).Range.Text = "Переоформлен"
                                    End If
                                End If
                            Next

                            s = customer_name.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_address")
                            If customer_name.Trim.Length > 0 And sTmp.Trim.Length > 0 Then s = s & ", "
                            s = s & sTmp.Trim
                            If s.Length > 0 Then s = s & ". "
                            sTmp = ds.Tables("customer").Rows(0)("bank")
                            s = s & sTmp.Trim
                            If sTmp.Trim.Length > 0 Then s = s & "."
                            sTmp = ""
                            If unn.Trim.Length > 0 Then s = s & " УНП"
                            s = s & unn.Trim
                            sTmp = ds.Tables("customer").Rows(0)("okpo")
                            If sTmp.Trim.Length > 0 Then s = s & " ОКЮЛП "
                            s = s & sTmp.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_phone")
                            If sTmp.Trim.Length > 0 Then s = s & " Тел/ф "
                            s = s & sTmp.Trim
                            If sTmp.Trim.Length > 0 Or unn.Trim.Length > 0 Then s = s & "."
                            doc.Bookmarks("Customer").Range.Text = s
                            doc.Bookmarks("Dogovor").Range.Text = dogovor
                            doc.Bookmarks("Date").Range.Text = sDate
                            doc.Bookmarks("Date2").Range.Text = sDate & " по " & GetRussianDate(CDate(ds.Tables("sale").Rows(0)("sale_date")).AddYears(1))

                            doc.Save()
                        End If

                    Catch
                        WriteError("Договор на техническое обслуживание<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 57 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Договор на техническое обслуживание
                    Try
                        Dim s$

                        docFullPath = path & DocName57

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then
                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName57, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)

                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("BoosName").Range.Text = boos_name
                            doc.Bookmarks("Registration").Range.Text = registration
                            If ds.Tables("customer").Rows(0)("NDS") <> "" Then
                                'doc.Bookmarks("NDS").Range.Text = ""
                            End If

                            s = customer_name.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_address")
                            If customer_name.Trim.Length > 0 And sTmp.Trim.Length > 0 Then s = s & ", "
                            s = s & sTmp.Trim
                            If s.Length > 0 Then s = s & ". "
                            sTmp = ds.Tables("customer").Rows(0)("bank")
                            s = s & sTmp.Trim
                            If sTmp.Trim.Length > 0 Then s = s & "."
                            sTmp = ""
                            If unn.Trim.Length > 0 Then s = s & " УНП"
                            s = s & unn.Trim
                            sTmp = ds.Tables("customer").Rows(0)("okpo")
                            If sTmp.Trim.Length > 0 Then s = s & " ОКЮЛП "
                            s = s & sTmp.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_phone")
                            If sTmp.Trim.Length > 0 Then s = s & " Тел/ф "
                            s = s & sTmp.Trim
                            If sTmp.Trim.Length > 0 Or unn.Trim.Length > 0 Then s = s & "."
                            doc.Bookmarks("Customer").Range.Text = s
                            doc.Bookmarks("Dogovor").Range.Text = dogovor
                            doc.Bookmarks("Dogovor2").Range.Text = dogovor
                            doc.Bookmarks("Date").Range.Text = DateTime.Today.ToString(" MMMM yyyy").ToLower()

                            doc.Save()
                        End If

                    Catch
                        WriteError("Дополнение к договору на ТО<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 7 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Список ККМ
                    Try

                        docFullPath = path & DocName7

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName7, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)
                            Dim a As Integer = 0

                            For i = 0 To ds.Tables("goods").Rows.Count - 1
                                If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                    r2 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(a + 1))
                                    a = a + 1
                                    r2.Cells(1).Range.Text = a
                                    r2.Cells(3).Range.Text = ds.Tables("goods").Rows(i)("good_name") & " " & ds.Tables("goods").Rows(i)("version") & ", №" & add_nulls(ds.Tables("goods").Rows(i)("num_cashregister")) & " " & ds.Tables("goods").Rows(i)("year") & " г.в. ФГУП(КЗТА)"
                                    r2.Cells(4).Range.Text = "0" & vbCrLf & "0"
                                    r2.Cells(5).Range.Text = "0001"
                                    r2.Cells(6).Range.Text = "№" & dogovor & " от " & sDate & " исправен"
                                    r2.Cells(7).Range.Text = ds.Tables("goods").Rows(i)("set_place")
                                    If (IsDBNull(ds.Tables("goods").Rows(i)("d")) = True) Then
                                        r2.Cells(8).Range.Text = ds.Tables("goods").Rows(i)("num_control_reestr") & vbCrLf & ds.Tables("goods").Rows(i)("num_control_pzu") & vbCrLf & ds.Tables("goods").Rows(i)("num_control_mfp") & vbCrLf & IIf(ds.Tables("goods").Rows(i)("num_control_cp").ToString().Trim() = "", "", ds.Tables("goods").Rows(i)("num_control_cp"))
                                    Else
                                        r2.Cells(8).Range.Text = ds.Tables("goods").Rows(i)("num_control_reestr") & " " & Format(ds.Tables("goods").Rows(i)("d"), "dd.MM.yyyy г.") & " " & ds.Tables("goods").Rows(i)("num_control_pzu") & " " & Format(ds.Tables("goods").Rows(i)("d"), "dd.MM.yyyy г.") & " " & ds.Tables("goods").Rows(i)("num_control_mfp") & " " & Format(ds.Tables("goods").Rows(i)("d"), "dd.MM.yyyy г.") & " " & IIf(ds.Tables("goods").Rows(i)("num_control_cp").ToString().Trim() = "", "", ds.Tables("goods").Rows(i)("num_control_cp") & " " & Format(ds.Tables("goods").Rows(i)("d"), "dd.MM.yyyy г."))
                                    End If
                                    r2.Cells(9).Range.Text = ds.Tables("goods").Rows(i)("num_control_cto") & vbCrLf & " " & vbCrLf & ds.Tables("goods").Rows(i)("num_control_cto2")
                                End If
                            Next

                            doc.Bookmarks("CustomerName").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                            doc.Bookmarks("CustomerName2").Range.Text = customer_name
                            doc.Bookmarks("UNN").Range.Text = ds.Tables("customer").Rows(0)("unn")
                            doc.Tables(2).Rows(1).Cells(2).Range.Text = customer_name & ", " & ds.Tables("customer").Rows(0)("customer_address")
                            doc.Bookmarks("Date").Range.Text = sDate
                            If boos_name.Trim.Length > 0 Then
                                doc.Bookmarks("Boos").Range.Text = boos_name
                            Else
                                If customer_name.Trim.Length > 0 Then doc.Bookmarks("Boos").Range.Text = customer_name
                            End If

                            If accountant.Trim.Length > 0 Then doc.Bookmarks("Accountant").Range.Text = accountant

                            doc.Save()
                        End If

                    Catch
                        WriteError("Список ККМ<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 8 Then
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Техническое заключение

                    Try
                        If sub_num = -1 Then
                            iLower = 0
                            iUpper = ds.Tables("goods").Rows.Count - 1
                        Else
                            iLower = sub_num
                            iUpper = sub_num
                        End If

                        For n = iLower To iUpper
                            If ds.Tables("goods").Rows(n)("is_cashregister") Then

                                docFullPath = path & n & DocName8

                                fl = New IO.FileInfo(docFullPath)
                                If (Not fl.Exists()) Or isRefresh Then

                                    If fl.Exists() Then
                                        Try
                                            fl.Delete()
                                        Catch
                                        End Try
                                    End If

                                    IO.File.Copy(Server.MapPath("Templates/") & DocName8, docFullPath, True)

                                    doc = wrdApp.Documents.Open(docFullPath)

                                    doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("goods").Rows(n)("good_name")
                                    doc.Bookmarks("CustomerName").Range.Text = customer_name
                                    doc.Bookmarks("CashregisterNumber").Range.Text = add_nulls(ds.Tables("goods").Rows(n)("num_cashregister"))
                                    doc.Bookmarks("Dogovor").Range.Text = dogovor
                                    doc.Bookmarks("Date").Range.Text = sDate

                                    doc.Save()
                                End If
                            End If
                        Next
                    Catch
                        WriteError("Техническое заключение<br>" & Err.Description)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

                If num_doc(k) = 9 Then
                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    ' Удостоверение кассира

                    Try

                        If sub_num = -1 Then
                            iLower = 0
                            iUpper = ds.Tables("goods").Rows.Count - 1
                        Else
                            iLower = sub_num
                            iUpper = sub_num
                        End If

                        Dim sNum$, iNum%, sYear$, sKassir$

                        sTmp = ds.Tables("sale").Rows(0)("dogovor")
                        sTmp = sTmp.Trim.PadLeft(2, "0")
                        sNum = ds.Tables("customer").Rows(0)("dogovor")
                        sNum = "К" & sNum.PadLeft(4, "0") & sTmp & "/"
                        sYear = "-" & Format(ds.Tables("sale").Rows(0)("sale_date"), "yy")
                        iNum = 0

                        For n = iLower To iUpper
                            docFullPath = path & n & DocName9

                            fl = New IO.FileInfo(docFullPath)
                            If (Not fl.Exists()) Or isRefresh Then

                                If fl.Exists() Then
                                    Try
                                        fl.Delete()
                                    Catch
                                    End Try
                                End If

                                IO.File.Copy(Server.MapPath("Templates/") & DocName9, docFullPath, True)

                                doc = wrdApp.Documents.Open(docFullPath)

                                sKassir = ds.Tables("goods").Rows(n)("kassir1")
                                If sKassir.Length > 0 Then
                                    iNum = iNum + 1
                                    doc.Bookmarks("Kassir1").Range.Text = sKassir
                                    doc.Bookmarks("Number1").Range.Text = sNum & iNum & sYear
                                    doc.Bookmarks("Type1").Range.Text = ds.Tables("goods").Rows(n)("good_name")
                                    doc.Bookmarks("Date1").Range.Text = sDate
                                Else
                                    doc.Bookmarks("Kassir1").Range.Text = "____________________________"
                                    doc.Bookmarks("Number1").Range.Text = "________"
                                    doc.Bookmarks("Type1").Range.Text = "_______________"
                                    doc.Bookmarks("Date1").Range.Text = """___""_______________"
                                End If

                                sKassir = ds.Tables("goods").Rows(n)("kassir2")
                                If sKassir.Length > 0 Then
                                    iNum = iNum + 1
                                    doc.Bookmarks("Kassir2").Range.Text = sKassir
                                    doc.Bookmarks("Number2").Range.Text = sNum & iNum & sYear
                                    doc.Bookmarks("Type2").Range.Text = ds.Tables("goods").Rows(n)("good_name")
                                    doc.Bookmarks("Date2").Range.Text = sDate
                                Else
                                    doc.Bookmarks("Kassir2").Range.Text = "____________________________"
                                    doc.Bookmarks("Number2").Range.Text = "________"
                                    doc.Bookmarks("Type2").Range.Text = "_______________"
                                    doc.Bookmarks("Date2").Range.Text = """___""_______________"
                                End If

                                doc.Save()
                            End If
                        Next
                    Catch
                        WriteError("Удостоверение кассира<br>" & Err.Description)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try
                End If
                If num_doc(k) = 17 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Гарантийный талон

                    Try

                        docFullPath = path & DocName17

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then

                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName17, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)
                            doc.Bookmarks("garantia_number").Range.Text = dogovor & " от " & sDate
                            doc.Bookmarks("buyer").Range.Text = customer_name & ", " & ds.Tables("customer").Rows(0)("customer_address") & ", " & ds.Tables("customer").Rows(0)("bank")

                            Dim a As Integer = 0
                            For i = 0 To ds.Tables("goods").Rows.Count - 1

                                If ds.Tables("goods").Rows(i)("garantia") = 0 Then

                                    r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                    a = a + 1
                                    r1.Cells(1).Range.Text = a
                                    r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i)("good_name") & " " & ds.Tables("goods").Rows(i)("version")
                                    r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i)("garantia")
                                    r1.Cells(4).Range.Text = ds.Tables("goods").Rows(i)("quantity")

                                    If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                        r1.Cells(5).Range.Text = add_nulls(ds.Tables("goods").Rows(i)("num_cashregister"))
                                    End If
                                Else
                                    Dim quantity% = CInt(ds.Tables("goods").Rows(i)("quantity"))
                                    For j = 0 To quantity - 1
                                        r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                        a = a + 1
                                        r1.Cells(1).Range.Text = a
                                        r1.Cells(2).Range.Text = ds.Tables("goods").Rows(i)("good_name") & " " & ds.Tables("goods").Rows(i)("version")
                                        r1.Cells(3).Range.Text = ds.Tables("goods").Rows(i)("garantia")
                                        r1.Cells(4).Range.Text = "1"
                                        If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                            r1.Cells(5).Range.Text = add_nulls(ds.Tables("goods").Rows(i)("num_cashregister"))
                                        End If
                                    Next
                                End If

                            Next
                            doc.Save()
                        End If

                    Catch
                        WriteError("Гарантийный талон<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If
                If num_doc(k) = 18 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Заявление в ИМНС
                    ''''''''''''''''''''

                    Try
                        Dim s$

                        docFullPath = path & DocName18

                        fl = New IO.FileInfo(docFullPath)
                        If (Not fl.Exists()) Or isRefresh Then
                            If fl.Exists() Then
                                Try
                                    fl.Delete()
                                Catch
                                End Try
                            End If

                            IO.File.Copy(Server.MapPath("Templates/") & DocName18, docFullPath, True)

                            doc = wrdApp.Documents.Open(docFullPath)
                            doc.Bookmarks("imns_name").Range.Text = "по " & ds.Tables("customer").Rows(0)("tax_inspection")
                            doc.Bookmarks("unp").Range.Text = ds.Tables("customer").Rows(0)("unn")
                            doc.Bookmarks("customer").Range.Text = customer_name

                            Dim kkm_text As String

                            Try
                                Dim a As Integer = 0

                                For i = 0 To ds.Tables("goods").Rows.Count - 1
                                    If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                        kkm_text &= ds.Tables("goods").Rows(i)("good_name") & ds.Tables("goods").Rows(i)("version") & _
", з.н. № " & add_nulls(ds.Tables("goods").Rows(i)("num_cashregister")) & ", место установки: " & ds.Tables("goods").Rows(i)("set_place") & ", год изготовления: " & Format(ds.Tables("goods").Rows(i)("d"), "dd.MM.yyyy г.") & vbCrLf

                                    End If
                                Next
                            Catch ex As Exception
                            End Try

                            doc.Bookmarks("kkm_text").Range.Text = kkm_text

                            doc.Bookmarks("customer_boos_info").Range.Text = boos_name
                            doc.Bookmarks("accountant_info").Range.Text = accountant
                            sDate = GetRussianDate1(ds.Tables("sale").Rows(0)("sale_date"))
                            If (sDate = String.Empty) Then
                                sDate = GetRussianDate1(DateTime.Now)
                            End If

                            'doc.Bookmarks("date_registration1").Range.Text = sDate
                            doc.Bookmarks("date_registration2").Range.Text = sDate
                            doc.Bookmarks("registration_info").Range.Text = registration
                            Dim count% = 0
                            For i = 0 To ds.Tables("goods").Rows.Count - 1
                                If ds.Tables("goods").Rows(i)("is_cashregister") Then
                                    count = count + 1
                                End If

                            Next
                            doc.Bookmarks("count").Range.Text = CStr(count)
                            doc.Bookmarks("bank_info").Range.Text = ds.Tables("customer").Rows(0)("bank")

                            s = customer_name.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_address")
                            If customer_name.Trim.Length > 0 And sTmp.Trim.Length > 0 Then s = s & ", "
                            s = s & sTmp.Trim
                            sTmp = ds.Tables("customer").Rows(0)("customer_phone")
                            If sTmp.Trim.Length > 0 Then s = s & ". Тел/ф: "
                            s = s & sTmp.Trim & "."
                            doc.Bookmarks("customer_info").Range.Text = s

                            doc.Save()
                        End If

                    Catch
                        WriteError("Заявление в ИМНС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessDocuments = False
                        GoTo ExitFunction
                    End Try

                End If

            Next

ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing

            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try

        End Function

        Overrides Function Summa_propis(ByVal s As String, Optional ByVal b As Boolean = True) As String
            Dim ss@, txt$, n%, i%
            Static triad(4) As Integer, numb1(19) As String, numb2(9) As String, numb3(9) As String
            If s = 0 Then
                Summa_propis = ""
                Exit Function
            End If


            Dim kop_arr = s.ToString.Split(",")
            Dim cop

            Try
                cop = kop_arr(1)
                If cop <> "" Then
                    s = kop_arr(0)
                End If
                'MsgBox(cop)
            Catch ex As Exception

            End Try

            If cop = "" Then
                kop_arr = s.ToString.Split(".")
                Try
                    cop = kop_arr(1)
                    If cop <> "" Then
                        s = kop_arr(0)
                    End If
                    'MsgBox(cop)
                Catch ex As Exception
                End Try
            End If


            ss@ = s
            triad(1) = ss@ - Int(ss@ / 1000) * 1000
            ss@ = Int(ss@ / 1000)
            triad(2) = ss@ - Int(ss@ / 1000) * 1000
            ss@ = Int(ss@ / 1000)
            triad(3) = ss@ - Int(ss@ / 1000) * 1000
            ss@ = Int(ss@ / 1000)
            triad(4) = ss@ - Int(ss@ / 1000) * 1000
            ss@ = Int(ss@ / 1000)
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
                Summa_propis = ""
                Exit Function
            End If
            For i% = 4 To 1 Step -1
                n% = 0
                If triad(i%) > 0 Then
                    n% = Int(triad(i%) / 100)
                    txt$ = txt$ & numb3(n%)
                    n% = Int((triad(i%) - n% * 100) / 10)
                    txt$ = txt$ & numb2(n%)
                    If n% < 2 Then
                        n% = triad(i%) - (Int(triad(i%) / 10) - n%) * 10
                    Else
                        n% = triad(i%) - Int(triad(i%) / 10) * 10
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
                If n% = 0 Or n% > 4 Then
                    txt$ = txt$ + "рублей"
                Else
                    If n% = 1 Then txt$ = txt$ + "рубль" Else txt$ = txt$ + "рубля"
                End If
            End If
            txt$ = UCase$(Left$(txt$, 1)) & Mid$(txt$, 2)
            Summa_propis = txt$

            If s = 0 Then
                Summa_propis = "Ноль рублей"
            End If


            If cop <> "" Then
                If cop.ToString.Length = 1 Then
                    cop = cop * 10
                End If
                Dim cop2 As String = ""
                cop2 = Summa_propis(cop).ToLower().Replace("рублей", "копеек")
                cop2 = cop2.Replace("рубль", "копейка")
                cop2 = cop2.Replace("рубля", "копейки")
                cop2 = cop2.Replace("один ", "одна ")
                cop2 = cop2.Replace("два ", "две ")
                'cop.ToString.Replace("рублей", "копеек")
                'cop.ToString.ToLower()
                Summa_propis = Summa_propis + " " + cop2
            End If

        End Function

        Public Overrides Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Overrides Function GetRussianDate1(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate1 = " « " & Day(d) & " » " & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Overrides Function GetRussianDate2(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate2 = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Overrides Function GetRussianDate3(ByVal d As Date) As String
            Dim m() As String = {" Январь ", " Февраль ", " Март ", " Апрель ", " Май ", " Июнь ", " Июль ", " Август ", " Сентябрь ", " Октябрь ", " Ноябрь ", " Декабрь "}
            GetRussianDate3 = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Overrides Function ProcessSupportDocuments(ByVal doc_type As Integer, ByVal customer_sys_id_s As String, ByVal good_sys_id_s As String, Optional ByVal isRefresh As Boolean = False)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            Dim docFullPath, customer_name, materials, works, dogovor, unn, sDate, filter As String
            Dim cost, nds, costwithoutnds As Double


            Dim path$ = Server.MapPath("Docs") & "\WorkActs\"

            ProcessSupportDocuments = True

            Try
                'Create folders and copy templates
                Dim fldr As New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If

            Catch
                WriteError(Err.Description)
                ProcessSupportDocuments = False
                Exit Function
            End Try


            Try
                ' get data from database for specified good and customer
                ds = New DataSet
                cmd = New SqlClient.SqlCommand("get_cto_master2")
                cmd.CommandType = CommandType.StoredProcedure
                filter = " where good.num_control_cto like 'МН%' and good.good_sys_id in (" & good_sys_id_s & ") "
                cmd.Parameters.AddWithValue("@pi_filter", filter)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)

                For Each dt As DataTable In ds.Tables
                    For Each row As DataRow In dt.Rows

                    Next
                Next
                If ds.Tables(0).Rows.Count = 0 Then GoTo ExitFunction



                'customer_name = ds.Tables(0).Rows(0)("customer_name") & ", " & ds.Tables(0).Rows(0)("customer_address") & ", " & ds.Tables(0).Rows(0)("bank")
                'dogovor = ds.Tables(0).Rows(0)("dogovor")
                'unn = ds.Tables(0).Rows(0)("unn")
                'works = ds.Tables(0).Rows(0)("works")
                'materials = ds.Tables(0).Rows(0)("materials")
                'sDate = GetRussianDate(Now())
                'cost = ds.Tables(0).Rows(0)("summ")
                'nds = Math.Round(cost * (1 - 1 / 1.2), 2)
                'costwithoutnds = Math.Round(cost / 1.2, 2)

            Catch

                WriteError("Загрузка данных<br>" & Err.Description)
                ProcessSupportDocuments = False
                GoTo ExitFunction

            End Try


            Try
                ' Create instance of Word!
                wrdApp = New Word.Application

            Catch
                WriteError(Err.Description)
                ProcessSupportDocuments = False
                GoTo ExitFunction
            End Try

            Dim r1 As Word.Row

            If doc_type = 10 Then

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Акт приема-сдачи выполненных работ
                Try
                    docFullPath = path & DocName10

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then

                        IO.File.Copy(Server.MapPath("Templates/") & DocName0, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("RECIPIENT_ADDRESS").Range.Text = ds.Tables(0).Rows(0)("customer_address")
                        doc.Bookmarks("RECIPIENT_NAME").Range.Text = customer_name
                        doc.Bookmarks("RECIPIENT_UNN").Range.Text = unn
                        doc.Bookmarks("Date").Range.Text = sDate
                        doc.Bookmarks("Boos").Range.Text = ds.Tables(0).Rows(0)("boos")

                        r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(3))
                        r1.Cells(1).Range.Text = "Техническое обслуживание кассового аппарата" & IIf(works.Length > 0, ", " & works, "")
                        r1.Cells(2).Range.Text = costwithoutnds
                        r1.Cells(3).Range.Text = 0
                        r1.Cells(4).Range.Text = 20
                        r1.Cells(5).Range.Text = nds
                        r1.Cells(6).Range.Text = cost

                        doc.Bookmarks("Total").Range.Text = costwithoutnds
                        doc.Bookmarks("TotalNDS").Range.Text = nds
                        doc.Bookmarks("TotalAll").Range.Text = cost
                        doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(nds)
                        doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(cost)

                        doc.Save()
                    End If

                Catch
                    WriteError("Счет-фактура  по НДС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessSupportDocuments = False
                    GoTo ExitFunction
                End Try

            End If

            '            If doc_type = 10 Then

            '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                'Акт приема-сдачи выполненных работ
            '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '                Try
            '                    docFullPath = path & DocName10

            '                    'Определяем стомость ТО
            '                    Dim good_type = GetPageParam("good_type")
            '                    costwithoutnds = dbSQL.ExecuteScalar("SELECT price_to FROM good_type WHERE good_type_sys_id='" & good_type & "'")
            '                    nds = costwithoutnds * 0.2
            '                    cost = costwithoutnds + nds

            '                    fl = New IO.FileInfo(docFullPath)
            '                    If (Not fl.Exists()) Or isRefresh Or (fl.Exists()) Then

            '                        IO.File.Copy(Server.MapPath("Templates/") & DocName10, docFullPath, True)

            '                        doc = wrdApp.Documents.Open(docFullPath)

            '                        doc.Bookmarks("Customer1").Range.Text = customer_name
            '                        doc.Bookmarks("Customer2").Range.Text = customer_name
            '                        doc.Bookmarks("Dogovor11").Range.Text = dogovor
            '                        doc.Bookmarks("Dogovor12").Range.Text = dogovor
            '                        doc.Bookmarks("Dogovor21").Range.Text = dogovor
            '                        doc.Bookmarks("Dogovor22").Range.Text = dogovor

            '                        If materials.Length > 0 Then
            '                            doc.Bookmarks("Materials1").Range.Text = materials
            '                            doc.Bookmarks("Materials2").Range.Text = materials
            '                        End If

            '                        If works.Length > 0 Then
            '                            doc.Bookmarks("Works1").Range.Text = ", " & works
            '                            doc.Bookmarks("Works2").Range.Text = ", " & works
            '                        End If

            '                        doc.Bookmarks("Cost1").Range.Text = Summa_propis(costwithoutnds)
            '                        doc.Bookmarks("Cost2").Range.Text = Summa_propis(costwithoutnds)
            '                        doc.Bookmarks("FullCost1").Range.Text = Summa_propis(cost)
            '                        doc.Bookmarks("FullCost2").Range.Text = Summa_propis(cost)
            '                        doc.Bookmarks("NDS1").Range.Text = Summa_propis(nds)
            '                        doc.Bookmarks("NDS2").Range.Text = Summa_propis(nds)
            '                        doc.Bookmarks("KOplate1").Range.Text = Summa_propis(cost)
            '                        doc.Bookmarks("KOplate2").Range.Text = Summa_propis(cost)

            '                        doc.Bookmarks("d1").Range.Text = GetRussianDate(Now)
            '                        doc.Bookmarks("d2").Range.Text = GetRussianDate(Now)

            '                        doc.Save()
            '                    End If
            '                Catch
            '                    WriteError("Акт приема-сдачи выполненных работ<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
            '                    ProcessSupportDocuments = False
            '                    GoTo ExitFunction
            '                End Try

            '            End If

ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing

            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try

        End Function

        Public Function add_nulls(ByVal str As String)
            Dim str_null As String = "000000000000000000"
            str = str.Trim
            Dim count_symb = str.ToString.Length
            Dim cut_len = 13 - count_symb

            Dim new_str As String = str_null.ToString.Substring(0, cut_len)

            new_str = new_str & str


            Return new_str
        End Function

        Public Overrides Function ProcessSingleDocuments(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal sale As Integer, ByVal cash As Integer, ByVal history As Integer, Optional ByVal sub_num As Integer = -1, Optional ByVal isRefresh As Boolean = True) As Boolean
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim rebill% = 0
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            Dim docFullPath$

            Dim path$ = Server.MapPath("Docs") & "\" & customer

            Dim boos_name$, customer_name$, accountant$, unn$, registration$, saler$, sDate$, dogovor$

            ProcessSingleDocuments = True

            Try
                'Create folders and copy templates
                Dim fldr As New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If
                path = path & "\" & sale & "\" & cash & "\"
                fldr = New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If
            Catch ex As Exception
                WriteError(Err.Description & "<BR>" & ex.ToString)
                ProcessSingleDocuments = False
                Exit Function
            End Try

            Try
                ' get data from database for specified sale
                ds = New DataSet
                'sale

                If sale <> 0 Then
                    cmd = New SqlClient.SqlCommand("get_sale_info")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds, "sale")

                    If ds.Tables("sale").Rows.Count = 0 Then GoTo ExitFunction

                    saler = ds.Tables("sale").Rows(0)("saler")
                    sDate = GetRussianDate(ds.Tables("sale").Rows(0)("sale_date"))

                    rebill = IIf(ds.Tables("sale").Rows(0)("state") = 4, 1, 0)
                End If

                'customer
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds, "customer")

                If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
                If sale = 0 Then
                    dogovor = ds.Tables("customer").Rows(0)("dogovor")
                Else
                    dogovor = ds.Tables("customer").Rows(0)("dogovor") & ds.Tables("sale").Rows(0)("dogovor")
                End If

                boos_name = ds.Tables("customer").Rows(0)("boos_name")
                customer_name = ds.Tables("customer").Rows(0)("customer_name")
                accountant = ds.Tables("customer").Rows(0)("accountant")
                unn = ds.Tables("customer").Rows(0)("unn")
                registration = ds.Tables("customer").Rows(0)("registration")

                ' get cash history 
                cmd = New SqlClient.SqlCommand("get_cash_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@good_sys_id", cash)
                cmd.Parameters.AddWithValue("@sys_id", history)

                If num_doc(0) = 11 Or num_doc(0) = 12 Or num_doc(0) = 13 Then
                    cmd.Parameters.AddWithValue("@state", 4)
                ElseIf num_doc(0) = 14 Or num_doc(0) = 19 Then
                    cmd.Parameters.AddWithValue("@state", 2)
                ElseIf num_doc(0) = 15 Or num_doc(0) = 20 Then
                    cmd.Parameters.AddWithValue("@state", 3)
                ElseIf num_doc(0) = 16 Or num_doc(0) = 32 Then
                    cmd.Parameters.AddWithValue("@state", 5)
                End If
                adapt = dbSQL.GetDataAdapter(cmd)
                If Not ds.Tables("cash") Is Nothing Then
                    ds.Tables("cash").Clear()
                End If
                adapt.Fill(ds, "cash")

                If ds.Tables("cash").Rows.Count = 0 Then
                    WriteError("Не выбраны товары для данного клиента и продажи")
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End If

            Catch ex As Exception

                WriteError("Загрузка данных<br>" & Err.Description)
                ProcessSingleDocuments = False
                GoTo ExitFunction

            End Try


            Try
                ' Create instance of Word!
                wrdApp = New Word.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessSingleDocuments = False
                GoTo ExitFunction
            End Try

            Dim r2 As Word.Row
            Dim i%, k%, n%
            Dim sTmp$


            If num_doc(k) = 11 Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Акт о снятии показаний счетчика при постановке в ИМНС

                Try

                    docFullPath = path & history & DocName11

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName11, docFullPath, True)
                        doc = wrdApp.Documents.Open(docFullPath)
                        doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        doc.Bookmarks("Version").Range.Text = ds.Tables("cash").Rows(n)("version")
                        doc.Bookmarks("boos_name").Range.Text = boos_name
                        doc.Bookmarks("CustomerName").Range.Text = customer_name
                        doc.Bookmarks("Saler").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        doc.Bookmarks("Saler2").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("cash").Rows(0)("worker_document")
                        doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                        doc.Bookmarks("num_cashregister").Range.Text = add_nulls(add_nulls(ds.Tables("cash").Rows(n)("num_cashregister")))
                        doc.Bookmarks("Reestr").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_out")
                        doc.Bookmarks("PZU").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_out")
                        doc.Bookmarks("MFP").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_out")
                        doc.Bookmarks("CTO").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_out")
                        doc.Bookmarks("CTO2").Range.Text = ds.Tables("cash").Rows(n)("marka_cto2_out")
                        doc.Bookmarks("CP").Range.Text = ds.Tables("cash").Rows(n)("marka_cp_out")
                        doc.Bookmarks("ZReport").Range.Text = ds.Tables("cash").Rows(n)("zreport_out")
                        doc.Bookmarks("Itog").Range.Text = ds.Tables("cash").Rows(n)("itog_out") & "(" & IIf(Summa_propis(ds.Tables("cash").Rows(n)("itog_out")) = "", "ноль", Summa_propis(ds.Tables("cash").Rows(n)("itog_out"))) & ")"
                        doc.Bookmarks("DateDismissal").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("support_date"))

                        'doc.Bookmarks("Date2").Range.Text = sDate
                        doc.Save()
                    End If

                Catch
                    WriteError("Акт о снятии показаний счетчика<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If
            If num_doc(k) = 12 Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Техническое заключение

                Try

                    docFullPath = path & history & DocName12

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName12, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        doc.Bookmarks("CustomerName").Range.Text = customer_name
                        doc.Bookmarks("CashregisterNumber").Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))
                        doc.Bookmarks("Dogovor").Range.Text = dogovor
                        doc.Bookmarks("Master").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        doc.Bookmarks("Date").Range.Text = sDate

                        doc.Save()
                    End If
                Catch
                    WriteError("Техническое заключение<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try

            End If
            If num_doc(k) = 13 Then
                '
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                'Договор на техническое обслуживание
                Try
                    Dim s$

                    docFullPath = path & history & DocName13

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName13, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("CustomerName").Range.Text = customer_name
                        doc.Bookmarks("BoosName").Range.Text = boos_name
                        doc.Bookmarks("Registration").Range.Text = registration
                        If ds.Tables("customer").Rows(0)("NDS") <> "" Then
                            'doc.Bookmarks("NDS").Range.Text = ""
                        End If

                        r2 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(i + 2))

                        r2.Cells(1).Range.Text = i + 1
                        r2.Cells(2).Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        r2.Cells(3).Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))
                        r2.Cells(4).Range.Text = ds.Tables("cash").Rows(n)("set_place")

                        If rebill = 0 Then
                            r2.Cells(5).Range.Text = "Новый"
                        Else
                            r2.Cells(5).Range.Text = "Переоформлен"
                        End If

                        s = customer_name.Trim
                        sTmp = ds.Tables("customer").Rows(0)("customer_address")
                        If customer_name.Trim.Length > 0 And sTmp.Trim.Length > 0 Then s = s & ", "
                        s = s & sTmp.Trim
                        If s.Length > 0 Then s = s & ". "
                        sTmp = ds.Tables("customer").Rows(0)("bank")
                        s = s & sTmp.Trim
                        If sTmp.Trim.Length > 0 Then s = s & "."
                        sTmp = ""
                        If unn.Trim.Length > 0 Then s = s & " УНП "
                        s = s & unn.Trim
                        sTmp = ds.Tables("customer").Rows(0)("okpo")
                        If sTmp.Trim.Length > 0 Then s = s & " ОКЮЛП "
                        s = s & sTmp.Trim
                        sTmp = ds.Tables("customer").Rows(0)("customer_phone")
                        If sTmp.Trim.Length > 0 Then s = s & " Тел/ф "
                        s = s & sTmp.Trim
                        If sTmp.Trim.Length > 0 Or unn.Trim.Length > 0 Then s = s & "."
                        doc.Bookmarks("Customer").Range.Text = s
                        doc.Bookmarks("Dogovor").Range.Text = dogovor
                        sDate = GetRussianDate(CDate(ds.Tables("cash").Rows(n)("support_date")))
                        doc.Bookmarks("Date").Range.Text = sDate
                        doc.Bookmarks("Date2").Range.Text = sDate & " по " & GetRussianDate(CDate(ds.Tables("cash").Rows(0)("support_date")).AddYears(1))

                        doc.Save()
                    End If

                Catch
                    WriteError("Договор на техническое обслуживание<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try

            End If

            ''Письмо в ИМНС
            If num_doc(k) = 55 Then
                Try

                    Dim s$

                    docFullPath = path & history & DocName55

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName55, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("customer").Range.Text = customer_name
                        doc.Bookmarks("unn").Range.Text = unn.Trim
                        doc.Bookmarks("dogovor").Range.Text = dogovor
                        doc.Bookmarks("date").Range.Text = sDate
                        doc.Bookmarks("ksa").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        doc.Bookmarks("numcash").Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))

                        Dim addp = GetPageParam("addp")
                        If addp = 2 Then
                            doc.Bookmarks("action").Range.Text = "заключен"
                        Else
                            doc.Bookmarks("action").Range.Text = "расторгнут"
                        End If

                        doc.Save()
                    End If

                Catch
                    WriteError("Письмо в ИМНС о снятии/постановке<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If


            'Заявление в ИМНС на снятие ККМ
            If num_doc(k) = 56 Then
                Try
                    Dim s$

                    docFullPath = path & history & DocName56

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName56, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("imns").Range.Text = ""
                        doc.Bookmarks("company").Range.Text = customer_name & ", " & unn.Trim
                        doc.Bookmarks("model").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        doc.Bookmarks("num_cushregister").Range.Text = ds.Tables("cash").Rows(n)("num_cashregister")
                        doc.Bookmarks("day").Range.Text = ""
                        doc.Bookmarks("month").Range.Text = ""
                        doc.Bookmarks("year").Range.Text = ""
                        doc.Bookmarks("name_ur").Range.Text = ""
                        doc.Bookmarks("inicial").Range.Text = boos_name

                        doc.Save()
                    End If

                Catch
                    WriteError("Заявление на снятие <br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If

            If num_doc(k) = 14 Or num_doc(k) = 15 Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Акт о снятии показаний счетчика при снятии с учета

                Try

                    docFullPath = path & history & DocName14

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName14, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        Try
                            doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                            doc.Bookmarks("Version").Range.Text = ds.Tables("cash").Rows(n)("version")
                            doc.Bookmarks("boos_name").Range.Text = boos_name
                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            doc.Bookmarks("Saler").Range.Text = ds.Tables("cash").Rows(n)("executor")
                            doc.Bookmarks("Saler2").Range.Text = ds.Tables("cash").Rows(n)("executor")
                            doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("cash").Rows(0)("worker_document")
                            doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
                            doc.Bookmarks("num_cashregister").Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))
                            doc.Bookmarks("Reestr").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_out")
                            doc.Bookmarks("PZU").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_out")
                            doc.Bookmarks("MFP").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_out")
                            doc.Bookmarks("CTO").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_in")
                            doc.Bookmarks("CTO2").Range.Text = ds.Tables("cash").Rows(n)("marka_cto2_in")
                            doc.Bookmarks("CP").Range.Text = ds.Tables("cash").Rows(n)("marka_cp_in")
                            doc.Bookmarks("ZReport").Range.Text = ds.Tables("cash").Rows(n)("zreport_out")
                            doc.Bookmarks("Itog").Range.Text = ds.Tables("cash").Rows(n)("itog_out") & "(" & IIf(Summa_propis(ds.Tables("cash").Rows(n)("itog_out")) = "", "ноль", Summa_propis(ds.Tables("cash").Rows(n)("itog_out"))) & ")"
                            doc.Bookmarks("DateDismissal").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("dismissal_date"))
                        Catch ex As Exception

                        End Try

                        doc.Save()

                    End If
                Catch
                    WriteError("Акт о снятии показаний счетчика<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If

            If num_doc(k) = 16 Then

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Акт снятия показаний счетчиков при сдаче-выдаче в ремонт прил.5

                Try

                    docFullPath = path & history & DocName16

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName16, docFullPath, True)
                        doc = wrdApp.Documents.Open(docFullPath)

                        Try
                            doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                            doc.Bookmarks("Version").Range.Text = ds.Tables("cash").Rows(n)("version")
                            doc.Bookmarks("boos_name").Range.Text = boos_name
                            doc.Bookmarks("CustomerName").Range.Text = customer_name
                            'MsgBox(ds.Tables("cash").Rows(n)("executor").ToString())

                            Try
                                doc.Bookmarks("Master").Range.Text = ds.Tables("cash").Rows(n)("executor")
                                doc.Bookmarks("Master1").Range.Text = ds.Tables("cash").Rows(n)("executor")
                                doc.Bookmarks("Master2").Range.Text = ds.Tables("cash").Rows(n)("executor")
                            Catch ex As Exception

                            End Try


                            doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("cash").Rows(0)("worker_document")
                            doc.Bookmarks("ActNumber").Range.Text = ds.Tables("cash").Rows(0)("akt")
                            doc.Bookmarks("num_cashregister").Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))
                            doc.Bookmarks("Reestr_In").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_in")
                            doc.Bookmarks("Reestr_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_out")
                            doc.Bookmarks("PZU_In").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_in")
                            doc.Bookmarks("PZU_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_out")
                            doc.Bookmarks("MFP_In").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_in")
                            doc.Bookmarks("MFP_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_out")
                            doc.Bookmarks("CTO_In").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_in")
                            doc.Bookmarks("CTO_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_out")
                            doc.Bookmarks("CTO2_In").Range.Text = ds.Tables("cash").Rows(n)("marka_cto2_in")
                            doc.Bookmarks("CTO2_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_cto2_out")
                            doc.Bookmarks("CP_In").Range.Text = ds.Tables("cash").Rows(n)("marka_cp_in")
                            doc.Bookmarks("CP_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_cp_out")
                            doc.Bookmarks("ZReport_In").Range.Text = ds.Tables("cash").Rows(n)("zreport_in")
                            doc.Bookmarks("ZReport_Out").Range.Text = ds.Tables("cash").Rows(n)("zreport_out")
                            Dim itog_in = ds.Tables("cash").Rows(n)("itog_in")
                            Dim itog_out = ds.Tables("cash").Rows(n)("itog_out")
                            itog_in = itog_in.ToString.Replace(".", ",")
                            itog_out = itog_out.ToString.Replace(".", ",")
                            doc.Bookmarks("Itog_In").Range.Text = itog_in & "(" & IIf(Summa_propis(itog_in) = "", "ноль", Summa_propis(itog_in)) & ")"
                            doc.Bookmarks("Itog_Out").Range.Text = itog_out & "(" & IIf(Summa_propis(itog_out) = "", "ноль", Summa_propis(itog_out)) & ")"
                            If ds.Tables("cash").Rows(n)("garantia_repair") = 0 Then
                                doc.Bookmarks("Repair_info").Range.Text = ds.Tables("cash").Rows(n)("info")
                            Else
                                doc.Bookmarks("Repair_info").Range.Text = "Гарантийный ремонт: " & ds.Tables("cash").Rows(n)("info")
                            End If
                            doc.Bookmarks("Repairdate_In").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("repairdate_in"))
                            doc.Bookmarks("Repairdate_Out").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("repairdate_out"))
                        Catch ex As Exception

                        End Try

                        doc.Save()
                    End If
                Catch
                    WriteError("Акт о снятии показаний счетчика<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If
            If num_doc(k) = 19 Or num_doc(k) = 20 Then

                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                '       Техническое заключение                                                   '
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                Try
                    docFullPath = path & history & DocName19

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName19, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        doc.Bookmarks("CustomerName").Range.Text = customer_name
                        doc.Bookmarks("CashregisterNumber").Range.Text = add_nulls(ds.Tables("cash").Rows(n)("num_cashregister"))
                        doc.Bookmarks("Dogovor").Range.Text = dogovor
                        doc.Bookmarks("Master").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        doc.Bookmarks("Date").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("dismissal_date"))

                        doc.Save()
                    End If

                Catch
                    WriteError("Техническое заключение<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try

            End If
            If num_doc(k) = 32 Then
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                ' Акт о проведении ремонта

                Try

                    docFullPath = path & history & DocName32

                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If

                        IO.File.Copy(Server.MapPath("Templates/") & DocName32, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        'doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("cash").Rows(n)("good_name")
                        'doc.Bookmarks("Version").Range.Text = ds.Tables("cash").Rows(n)("version")
                        'doc.Bookmarks("boos_name").Range.Text = boos_name
                        'doc.Bookmarks("CustomerName").Range.Text = customer_name
                        'doc.Bookmarks("Master").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        'doc.Bookmarks("Master1").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        'doc.Bookmarks("Master2").Range.Text = ds.Tables("cash").Rows(n)("executor")
                        'doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("cash").Rows(0)("worker_document")
                        'doc.Bookmarks("ActNumber").Range.Text = ds.Tables("cash").Rows(0)("akt")
                        'doc.Bookmarks("num_cashregister").Range.Text = ds.Tables("cash").Rows(n)("num_cashregister")
                        'doc.Bookmarks("Reestr_In").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_in")
                        'doc.Bookmarks("Reestr_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_reestr_out")
                        'doc.Bookmarks("PZU_In").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_in")
                        'doc.Bookmarks("PZU_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_pzu_out")
                        'doc.Bookmarks("MFP_In").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_in")
                        'doc.Bookmarks("MFP_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_mfp_out")
                        'doc.Bookmarks("CTO_In").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_in")
                        'doc.Bookmarks("CTO_Out").Range.Text = ds.Tables("cash").Rows(n)("marka_cto_out")
                        'doc.Bookmarks("ZReport_In").Range.Text = ds.Tables("cash").Rows(n)("zreport_in")
                        'doc.Bookmarks("ZReport_Out").Range.Text = ds.Tables("cash").Rows(n)("zreport_out")
                        'doc.Bookmarks("Itog_In").Range.Text = ds.Tables("cash").Rows(n)("itog_in") & "(" & IIf(Summa_propis(ds.Tables("cash").Rows(n)("itog_in")) = "", "ноль", Summa_propis(ds.Tables("cash").Rows(n)("itog_in"))) & ")"
                        'doc.Bookmarks("Itog_Out").Range.Text = ds.Tables("cash").Rows(n)("itog_out") & "(" & IIf(Summa_propis(ds.Tables("cash").Rows(n)("itog_out")) = "", "ноль", Summa_propis(ds.Tables("cash").Rows(n)("itog_out"))) & ")"
                        'doc.Bookmarks("Repair_info").Range.Text = ds.Tables("cash").Rows(n)("info")
                        'doc.Bookmarks("Repairdate_In").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("repairdate_in"))
                        'doc.Bookmarks("Repairdate_Out").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(n)("repairdate_out"))
                        'doc.Save()
                    End If

                Catch
                    WriteError("Акт о проведении ремонта<br>" & Err.Description)
                    ProcessSingleDocuments = False
                    GoTo ExitFunction
                End Try
            End If
ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try

        End Function

        Public Overrides Sub DeleteHistoryDocument(ByVal history As String)
            Dim reader As SqlClient.SqlDataReader

            Try
                reader = dbSQL.GetReader("select hc.sys_id,hc.good_sys_id ,hc.state,hc.owner_sys_id,s.sale_sys_id FROM cash_history hc left outer join good g on hc.good_sys_id = g.good_sys_id left outer join sale s on s.sale_sys_id=g.sale_sys_id WHERE sys_id='" & history & "'")
                If reader.Read Then

                    If reader("state") = 4 Then

                        DeleteDocument(11, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                        DeleteDocument(12, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                        DeleteDocument(13, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                    ElseIf reader("state") = 2 Then
                        DeleteDocument(14, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                        DeleteDocument(19, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                    ElseIf reader("state") = 3 Then
                        DeleteDocument(15, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                        DeleteDocument(20, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                    ElseIf reader("state") = 5 Then
                        DeleteDocument(16, reader("owner_sys_id"), reader("sale_sys_id"), reader("good_sys_id"), reader("sys_id"))
                    End If
                End If

                reader.Close()
            Catch
                Exit Sub
            End Try
        End Sub

        Public Overrides Sub DeleteDocument(ByVal num_doc As Integer, ByVal customer As Integer, ByVal sale As Integer, ByVal cash As Integer, ByVal history As Integer)
            Dim doc_path$, path$

            Select Case num_doc
                Case 0 : doc_path = DocName0
                Case 1 : doc_path = DocName1
                Case 2 : doc_path = DocName2
                Case 3 : doc_path = DocName3
                Case 4 : doc_path = DocName4
                Case 5 : doc_path = DocName5
                Case 6 : doc_path = DocName6
                Case 7 : doc_path = DocName7
                Case 8 : doc_path = DocName8
                Case 9 : doc_path = DocName9
                Case 10 : doc_path = DocName10
                Case 11 : doc_path = DocName11
                Case 12 : doc_path = DocName12
                Case 13 : doc_path = DocName13
                Case 14 : doc_path = DocName14
                Case 15 : doc_path = DocName15
                Case 16 : doc_path = DocName16
                Case 19 : doc_path = DocName19
                Case 20 : doc_path = DocName20

            End Select
            path = Server.MapPath("Docs/") & customer & "\" & sale & "\" & cash & "\" & history & doc_path
            Dim fls As New IO.FileInfo(path)
            If fls.Exists Then
                fls.Delete()
            End If
        End Sub

        Public Overrides Function ProcessReportQuartal(ByVal num_doc() As Integer, ByVal begin_date As DateTime, ByVal end_date As DateTime, Optional ByVal isRefresh As Boolean = True) As Boolean
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Dim fls As IO.File
            Dim fl As IO.FileInfo

            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\" & Format(begin_date, "yyyyMMdd") & "-" & Format(end_date, "yyyyMMdd")

            ProcessReportQuartal = True

            Try
                'Create folders and copy templates
                Dim fldr As New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If

            Catch ex As Exception
                WriteError(Err.Description & "<BR>" & ex.ToString)
                ProcessReportQuartal = False
                Exit Function
            End Try

            Dim dv As DataView
            Dim t As DataTable
            Dim row As DataRow

            If num_doc(0) = 30 Then
                Try
                    ' get data from database for specified sale
                    t = CreateReportQuartalTable("ReportmarkaQuartal")
                    ds = New DataSet
                    ' проданные ККМ 
                    cmd = New SqlClient.SqlCommand("prc_report_marka_quartal")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)

                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds, "marka")

                    Dim i%, j%, good_id%
                    For i = 0 To ds.Tables("marka").Rows.Count - 1
                        Try
                            good_id = CInt(ds.Tables("marka").Rows(i)("good_sys_id"))
                            row = t.NewRow()
                            row("set_date") = ds.Tables("marka").Rows(i)("set_date")
                            row("KKM") = ds.Tables("marka").Rows(i)("kkm")
                            row("num_cashregister") = add_nulls(ds.Tables("marka").Rows(i)("num_cashregister"))
                            row("set_place_marka") = ds.Tables("marka").Rows(i)("set_place_marka")
                            row("seria_marka") = ds.Tables("marka").Rows(i)("seria_marka")
                            row("marka") = ds.Tables("marka").Rows(i)("marka")
                            row("marka_dismissal") = ""
                            row("executor") = ds.Tables("marka").Rows(i)("executor")
                            row("akt") = ""
                            row("unn") = ds.Tables("marka").Rows(i)("unn")
                            row("owner_name") = ds.Tables("marka").Rows(i)("owner_name")
                            row("address") = ds.Tables("marka").Rows(i)("address")
                            row("set_place") = ds.Tables("marka").Rows(i)("set_place")
                            t.Rows.Add(row)
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри информацию(предпродажная подготовка) по ККМ : " & add_nulls(ds.Tables("marka").Rows(i)("num_cashregister")))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next
                    ' замена при ТО
                    cmd = New SqlClient.SqlCommand("prc_report_marka_support")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@good_sys_id", good_id)
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("marka_support") Is Nothing Then
                        ds.Tables("marka_support").Clear()
                    End If
                    adapt.Fill(ds, "marka_support")
                    Dim marka$ = ""
                    Dim seria_marka$ = ""
                    Dim set_place_marka$ = ""
                    Dim marka_dismissal$ = ""
                    For j = 0 To ds.Tables("marka_support").Rows.Count - 1
                        marka = ""
                        seria_marka = ""
                        set_place_marka = ""
                        marka_dismissal = ""
                        Try
                            If ds.Tables("marka_support").Rows(j)("change_reestr") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_reestr_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_reestr_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_reestr_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support").Rows(j)("change_pzu") = "1" Then
                                set_place_marka &= "ПЗУ "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_pzu_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_pzu_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_pzu_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support").Rows(j)("change_mfp") = "1" Then
                                set_place_marka &= "МФП "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_mfp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_mfp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_mfp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support").Rows(j)("change_cp") = "1" Then
                                set_place_marka &= "ЦП "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_cp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support").Rows(j)("change_cto") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support").Rows(j)("change_cto2") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto2_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto2_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support").Rows(j)("marka_cto2_in")).Substring(2, 9) & " "
                            End If
                            If set_place_marka.Length > 0 Then
                                row = t.NewRow()
                                row("set_date") = ds.Tables("marka_support").Rows(j)("set_date")
                                row("KKM") = ds.Tables("marka_support").Rows(j)("kkm")
                                row("num_cashregister") = add_nulls(ds.Tables("marka_support").Rows(j)("num_cashregister"))
                                row("set_place_marka") = set_place_marka
                                row("seria_marka") = seria_marka
                                row("marka") = marka
                                row("marka_dismissal") = marka_dismissal
                                row("executor") = ds.Tables("marka_support").Rows(j)("executor")
                                row("akt") = "Замена при ТО"
                                row("unn") = ds.Tables("marka_support").Rows(j)("unn")
                                row("owner_name") = ds.Tables("marka_support").Rows(j)("owner_name")
                                row("address") = ds.Tables("marka_support").Rows(j)("address")
                                row("set_place") = ds.Tables("marka_support").Rows(j)("set_place")
                                t.Rows.Add(row)
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри историю ТО(проведение) по ККМ : " & add_nulls(ds.Tables("marka_support").Rows(j)("num_cashregister")))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next
                    ' постановка на учет вторичный
                    cmd = New SqlClient.SqlCommand("prc_report_marka_support_second")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@good_sys_id", good_id)
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("marka_support_second") Is Nothing Then
                        ds.Tables("marka_support_second").Clear()
                    End If
                    adapt.Fill(ds, "marka_support_second")


                    For j = 0 To ds.Tables("marka_support_second").Rows.Count - 1
                        marka = ""
                        seria_marka = ""
                        set_place_marka = ""
                        marka_dismissal = ""
                        Try
                            If ds.Tables("marka_support_second").Rows(j)("change_reestr") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_reestr_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_reestr_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_reestr_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support_second").Rows(j)("change_pzu") = "1" Then
                                set_place_marka &= "ПЗУ "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_pzu_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_pzu_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_pzu_in")).Substring(2, 9) & " "
                            End If

                            If ds.Tables("marka_support_second").Rows(j)("change_mfp") = "1" Then
                                set_place_marka &= "МФП "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_mfp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_mfp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_mfp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support_second").Rows(j)("change_cp") = "1" Then
                                set_place_marka &= "ЦП "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support_second").Rows(j)("change_cto") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto_out")).Substring(2, 9) & " "
                                marka_dismissal &= "" 'CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto_out")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_support_second").Rows(j)("change_cto2") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto2_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto2_out")).Substring(2, 9) & " "
                                marka_dismissal &= "" 'CStr(ds.Tables("marka_support_second").Rows(j)("marka_cto_out")).Substring(2, 9) & " "
                            End If
                            If set_place_marka.Length > 0 Then
                                row = t.NewRow()
                                row("set_date") = ds.Tables("marka_support_second").Rows(j)("set_date")
                                row("KKM") = ds.Tables("marka_support_second").Rows(j)("kkm")
                                row("num_cashregister") = add_nulls(ds.Tables("marka_support_second").Rows(j)("num_cashregister"))
                                row("set_place_marka") = set_place_marka
                                row("seria_marka") = seria_marka
                                row("marka") = marka
                                row("marka_dismissal") = marka_dismissal
                                row("executor") = ds.Tables("marka_support_second").Rows(j)("executor")
                                row("akt") = "Постановка на учет"
                                row("unn") = ds.Tables("marka_support_second").Rows(j)("unn")
                                row("owner_name") = ds.Tables("marka_support_second").Rows(j)("owner_name")
                                row("address") = ds.Tables("marka_support_second").Rows(j)("address")
                                row("set_place") = ds.Tables("marka_support_second").Rows(j)("set_place")
                                t.Rows.Add(row)
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри историю ТО(повторная постановка) по ККМ : " & add_nulls(ds.Tables("marka_support_second").Rows(j)("num_cashregister")))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next
                    'снятие с учета
                    cmd = New SqlClient.SqlCommand("prc_report_marka_dismissal")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@good_sys_id", good_id)
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("marka_dismissal") Is Nothing Then
                        ds.Tables("marka_dismissal").Clear()
                    End If
                    adapt.Fill(ds, "marka_dismissal")

                    For j = 0 To ds.Tables("marka_dismissal").Rows.Count - 1
                        marka = ""
                        seria_marka = ""
                        set_place_marka = ""
                        marka_dismissal = ""
                        Try
                            If ds.Tables("marka_dismissal").Rows(j)("change_reestr") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_reestr_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_reestr_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_reestr_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_dismissal").Rows(j)("change_pzu") = "1" Then
                                set_place_marka &= "ПЗУ "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_pzu_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_pzu_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_pzu_in")).Substring(2, 9) & " "
                            End If

                            If ds.Tables("marka_dismissal").Rows(j)("change_mfp") = "1" Then
                                set_place_marka &= "МФП "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_mfp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_mfp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_mfp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_dismissal").Rows(j)("change_cp") = "1" Then
                                set_place_marka &= "ЦП "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_dismissal").Rows(j)("change_cto") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto_in")).Substring(0, 2) & " "
                                marka &= "" 'CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto_in")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_dismissal").Rows(j)("change_cto2") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto2_in")).Substring(0, 2) & " "
                                marka &= "" 'CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto_in")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_dismissal").Rows(j)("marka_cto2_in")).Substring(2, 9) & " "
                            End If
                            If set_place_marka.Length > 0 Then
                                row = t.NewRow()
                                row("set_date") = ds.Tables("marka_dismissal").Rows(j)("set_date")
                                row("KKM") = ds.Tables("marka_dismissal").Rows(j)("kkm")
                                row("num_cashregister") = add_nulls(ds.Tables("marka_dismissal").Rows(j)("num_cashregister"))
                                row("set_place_marka") = set_place_marka
                                row("seria_marka") = seria_marka
                                row("marka") = marka
                                row("marka_dismissal") = marka_dismissal
                                row("executor") = ds.Tables("marka_dismissal").Rows(j)("executor")
                                row("akt") = "Снятие с учета"
                                row("unn") = ds.Tables("marka_dismissal").Rows(j)("unn")
                                row("owner_name") = ds.Tables("marka_dismissal").Rows(j)("owner_name")
                                row("address") = ds.Tables("marka_dismissal").Rows(j)("address")
                                row("set_place") = ds.Tables("marka_dismissal").Rows(j)("set_place")
                                t.Rows.Add(row)
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри историю ТО(снятие) по ККМ : " & add_nulls(ds.Tables("marka_dismissal").Rows(j)("num_cashregister")))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next
                    ' замена при ремонте
                    cmd = New SqlClient.SqlCommand("prc_report_marka_repair")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@good_sys_id", good_id)
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("marka_repair") Is Nothing Then
                        ds.Tables("marka_repair").Clear()
                    End If

                    adapt.Fill(ds, "marka_repair")

                    For j = 0 To ds.Tables("marka_repair").Rows.Count - 1
                        marka = ""
                        seria_marka = ""
                        set_place_marka = ""
                        marka_dismissal = ""
                        Try
                            If ds.Tables("marka_repair").Rows(j)("change_reestr") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_reestr_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_reestr_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_reestr_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_repair").Rows(j)("change_pzu") = "1" Then
                                set_place_marka &= "ПЗУ "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_pzu_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_pzu_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_pzu_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_repair").Rows(j)("change_mfp") = "1" Then
                                set_place_marka &= "МФП "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_mfp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_mfp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_mfp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_repair").Rows(j)("change_cp") = "1" Then
                                set_place_marka &= "ЦП "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cp_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cp_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cp_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_repair").Rows(j)("change_cto") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto_in")).Substring(2, 9) & " "
                            End If
                            If ds.Tables("marka_repair").Rows(j)("change_cto2") = "1" Then
                                set_place_marka &= "корпус "
                                seria_marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto2_out")).Substring(0, 2) & " "
                                marka &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto2_out")).Substring(2, 9) & " "
                                marka_dismissal &= CStr(ds.Tables("marka_repair").Rows(j)("marka_cto2_in")).Substring(2, 9) & " "
                            End If
                            If set_place_marka.Length > 0 Then
                                row = t.NewRow()
                                row("set_date") = ds.Tables("marka_repair").Rows(j)("set_date")
                                row("KKM") = ds.Tables("marka_repair").Rows(j)("kkm")
                                row("num_cashregister") = add_nulls(ds.Tables("marka_repair").Rows(j)("num_cashregister"))
                                row("set_place_marka") = set_place_marka
                                row("seria_marka") = seria_marka
                                row("marka") = marka
                                row("marka_dismissal") = marka_dismissal
                                row("executor") = ds.Tables("marka_repair").Rows(j)("executor")
                                row("akt") = Format(ds.Tables("marka_repair").Rows(j)("set_date"), "dd.MM.yyyy") & " №" & ds.Tables("marka_repair").Rows(j)("akt")
                                row("unn") = ds.Tables("marka_repair").Rows(j)("unn")
                                row("owner_name") = ds.Tables("marka_repair").Rows(j)("owner_name")
                                row("address") = ds.Tables("marka_repair").Rows(j)("address")
                                row("set_place") = ds.Tables("marka_repair").Rows(j)("set_place")
                                t.Rows.Add(row)
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри историю ТО(ремонты) по ККМ : " & add_nulls(ds.Tables("marka_repair").Rows(j)("num_cashregister")))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next

                    dv = t.DefaultView
                    dv.Sort = " set_date"
                    Dim k%
                    docFullPath = path & "\" & DocName30
                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        IO.File.Copy(Server.MapPath("Templates/") & DocName30, docFullPath, True)
                        Try
                            ' Create instance of Word!
                            xlsApp = New Excel.Application

                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString)
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try

                        book = xlsApp.Workbooks.Open(docFullPath)

                        For k = 0 To t.Rows.Count - 1
                            book.ActiveSheet.Range("B" & CStr(7 + k)).Value = dv.Item(k)("set_date")
                            book.ActiveSheet.Range("C" & CStr(7 + k)).Value = dv.Item(k)("KKM")
                            book.ActiveSheet.Range("D" & CStr(7 + k)).Value = add_nulls(dv.Item(k)("num_cashregister"))
                            book.ActiveSheet.Range("E" & CStr(7 + k)).Value = dv.Item(k)("set_place_marka")
                            book.ActiveSheet.Range("F" & CStr(7 + k)).Value = dv.Item(k)("seria_marka")
                            book.ActiveSheet.Range("G" & CStr(7 + k)).Value = dv.Item(k)("marka")
                            book.ActiveSheet.Range("H" & CStr(7 + k)).Value = dv.Item(k)("marka_dismissal")
                            book.ActiveSheet.Range("I" & CStr(7 + k)).Value = dv.Item(k)("executor")
                            book.ActiveSheet.Range("J" & CStr(7 + k)).Value = dv.Item(k)("akt")
                            book.ActiveSheet.Range("K" & CStr(7 + k)).Value = dv.Item(k)("unn")
                            book.ActiveSheet.Range("L" & CStr(7 + k)).Value = dv.Item(k)("owner_name")
                            book.ActiveSheet.Range("M" & CStr(7 + k)).Value = dv.Item(k)("address")
                            book.ActiveSheet.Range("N" & CStr(7 + k)).Value = dv.Item(k)("set_place")
                        Next
                        ExcelReportFormating("A7:N" & CStr(7 + t.Rows.Count - 1))
                        xlsApp.Run("renumerate")
                        book.Save()
                    End If
                Catch ex As Exception

                    WriteError("Загрузка данных<br>" & Err.Description)
                    ProcessReportQuartal = False
                    GoTo ExitFunction

                End Try

            End If
            If num_doc(0) = 40 Then
                Try
                    t = CreateDismissalMarkaQuartalTable("DismissalMarkaQuartal")
                    ds = New DataSet

                    ' снятые марки 
                    cmd = New SqlClient.SqlCommand("prc_report_marka_dismissal_repair")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@begin_date", begin_date)
                    cmd.Parameters.AddWithValue("@end_date", end_date)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("marka_dismissal_repair") Is Nothing Then
                        ds.Tables("marka_dismissal_repair").Clear()
                    End If

                    adapt.Fill(ds, "marka_dismissal_repair")
                    Dim j%
                    For j = 0 To ds.Tables("marka_dismissal_repair").Rows.Count - 1
                        Try
                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_reestr") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_reestr_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_reestr_in") <> "" Then
                                    row = t.NewRow()
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_reestr_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_reestr_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If
                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_pzu") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_pzu_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_pzu_in") <> "" Then
                                    row = t.NewRow()
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_pzu_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_pzu_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If

                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_mfp") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in") <> "" Then
                                    row = t.NewRow()
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If
                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_cp") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_mfp_in") <> "" Then
                                    row = t.NewRow()
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cp_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cp_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If
                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_cto") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in") <> "" Then
                                    row = t.NewRow()
                                    'WriteError(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in") & "<br>")
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If
                            If ds.Tables("marka_dismissal_repair").Rows(j)("change_cto2") = "1" Then
                                If Not IsDBNull(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto2_in")) And ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto_in") <> "" Then
                                    row = t.NewRow()
                                    row("seria_marka") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto2_in")).Substring(0, 2)
                                    row("marka_dismissal") = CStr(ds.Tables("marka_dismissal_repair").Rows(j)("marka_cto2_in")).Substring(2, 9)
                                    t.Rows.Add(row)
                                End If
                            End If
                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString & "<br>" & "Cмотри все снятия марок по ККМ : " & ds.Tables("marka_dismissal_repair").Rows(j)("num_cashregister"))
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                    Next


                    dv = t.DefaultView
                    dv.Sort = " seria_marka,marka_dismissal"
                    Dim k%
                    docFullPath = path & "\" & DocName40
                    fl = New IO.FileInfo(docFullPath)
                    If (Not fl.Exists()) Or isRefresh Then
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        IO.File.Copy(Server.MapPath("Templates/") & DocName40, docFullPath, True)
                        Try
                            ' Create instance of Word!
                            wrdApp = New Word.Application

                        Catch ex As Exception
                            WriteError(Err.Description & "<br>" & ex.ToString)
                            ProcessReportQuartal = False
                            GoTo ExitFunction
                        End Try
                        doc = wrdApp.Documents.Open(docFullPath)

                        Dim r1 As Word.Row
                        Dim a% = 0
                        Dim ser$ = ""
                        Dim mark$ = ""
                        Dim markcount% = 0
                        For k = 0 To t.Rows.Count - 1
                            If ser <> "" Then
                                If ser <> dv.Item(k)("seria_marka") Then

                                    r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                    a = a + 1
                                    r1.Cells(1).Range.Text = markcount
                                    r1.Cells(2).Range.Text = ser
                                    r1.Cells(3).Range.Text = mark
                                    ser = dv.Item(k)("seria_marka")
                                    markcount = 0
                                    mark = dv.Item(k)("marka_dismissal")
                                    markcount = markcount + 1
                                Else
                                    'If dv.Item(k)("marka_dismissal") <> "" Then
                                    If mark <> "" Then
                                        mark = mark & ";" & dv.Item(k)("marka_dismissal")
                                    Else
                                        mark = dv.Item(k)("marka_dismissal")
                                    End If
                                    markcount = markcount + 1
                                    'End If
                                End If
                            Else
                                'If dv.Item(k)("marka_dismissal") <> "" Then
                                If mark <> "" Then
                                    mark = mark & ";" & dv.Item(k)("marka_dismissal")
                                Else
                                    mark = dv.Item(k)("marka_dismissal")
                                End If

                                markcount = markcount + 1
                                ser = dv.Item(k)("seria_marka")
                                'End If
                            End If
                        Next
                        r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                        a = a + 1
                        r1.Cells(1).Range.Text = markcount
                        r1.Cells(2).Range.Text = ser
                        r1.Cells(3).Range.Text = mark
                        markcount = 0
                        mark = ""
                        doc.Save()
                    End If
                Catch ex As Exception

                    WriteError("Загрузка данных<br>" & Err.Description)
                    ProcessReportQuartal = False
                    GoTo ExitFunction

                End Try
            End If

ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function CreateReportQuartalTable(ByVal tableName As String) As DataTable
            Dim table As DataTable

            table = New DataTable(tableName)
            Dim col As DataColumn = New DataColumn("id", System.Type.GetType("System.Int32"))
            col.AllowDBNull = False
            col.AutoIncrement = True
            col.AutoIncrementSeed = 1
            table.Columns.Add(col)
            table.Columns.Add("number", System.Type.GetType("System.Int32"))
            table.Columns.Add("set_date", System.Type.GetType("System.DateTime"))
            table.Columns.Add("KKM", System.Type.GetType("System.String"))
            table.Columns.Add("num_cashregister", System.Type.GetType("System.String"))
            table.Columns.Add("set_place_marka", System.Type.GetType("System.String"))
            table.Columns.Add("seria_marka", System.Type.GetType("System.String"))
            table.Columns.Add("marka", System.Type.GetType("System.String"))
            table.Columns.Add("marka_dismissal", System.Type.GetType("System.String"))
            table.Columns.Add("executor", System.Type.GetType("System.String"))
            table.Columns.Add("akt", System.Type.GetType("System.String"))
            table.Columns.Add("unn", System.Type.GetType("System.String"))
            table.Columns.Add("owner_name", System.Type.GetType("System.String"))
            table.Columns.Add("address", System.Type.GetType("System.String"))
            table.Columns.Add("set_place", System.Type.GetType("System.String"))
            CreateReportQuartalTable = table
        End Function

        Private Function CreateDismissalMarkaQuartalTable(ByVal tableName As String) As DataTable
            Dim table As DataTable

            table = New DataTable(tableName)
            Dim col As DataColumn = New DataColumn("id", System.Type.GetType("System.Int32"))
            col.AllowDBNull = False
            col.AutoIncrement = True
            col.AutoIncrementSeed = 1
            table.Columns.Add(col)
            table.Columns.Add("seria_marka", System.Type.GetType("System.String"))
            table.Columns.Add("marka_dismissal", System.Type.GetType("System.String"))
            CreateDismissalMarkaQuartalTable = table
        End Function

        Private Function ProcessRoute(ByVal cashregisters As String, ByVal region As String, ByVal goodType As String, ByVal dateTO As String) As Boolean
            Dim j As Integer
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet
            If (region = 0) Then
                cmd = New SqlClient.SqlCommand("prc_rpt_Route")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@pi_Registers", cashregisters)
                ' cmd.Parameters.AddWithValue("@pi_list", Session("route_good_list"))
            Else
                cmd = New SqlClient.SqlCommand("prc_rpt_RouteRegion")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@pi_place_rn_id", cashregisters)
                cmd.Parameters.AddWithValue("@d1", CDate(dateTO))
                cmd.Parameters.AddWithValue("@good_type", goodType)

            End If
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "route_report")

            Dim dv As DataView
            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\" & Format(DateTime.Now, "yyyyMMdd")
            docFullPath = path & "\" & DocName50
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessRoute = False
                    Exit Function
                End Try
            End If
            IO.File.Copy(Server.MapPath("Templates/") & DocName50, docFullPath, True)
            Try
                ' Create instance of Word!
                xlsApp = New Excel.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessRoute = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables("route_report").DefaultView
            With ds.Tables("route_report")
                For j = 0 To .Rows.Count - 1
                    book.ActiveSheet.Range("A" & CStr(3 + j)).Value = Format(j + 1)
                    If (IsDBNull(dv.Item(j)("payer"))) Then
                        book.ActiveSheet.Range("B" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("B" & CStr(3 + j)).Value = Format(dv.Item(j)("payer")).Replace("<br>", vbLf)

                    End If

                    book.ActiveSheet.Range("C" & CStr(3 + j)).Value = add_nulls(Format(dv.Item(j)("num_cashregister")))
                    book.ActiveSheet.Range("D" & CStr(3 + j)).Value = Format(dv.Item(j)("num_control_reestr")) & vbLf & Format(dv.Item(j)("num_control_pzu")) & vbLf & Format(dv.Item(j)("num_control_mfp")) & vbLf & Format(dv.Item(j)("num_control_cp")) & vbLf & Format(dv.Item(j)("num_control_cto"))
                    If (IsDBNull(dv.Item(j)("place_region"))) Then
                        book.ActiveSheet.Range("E" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("E" & CStr(3 + j)).Value = Format(dv.Item(j)("place_region"))
                    End If
                    book.ActiveSheet.Range("F" & CStr(3 + j)).Value = Format(dv.Item(j)("set_place")).Replace("<br>", vbLf)

                    If (IsDBNull(dv.Item(j)("balance"))) Then
                        book.ActiveSheet.Range("G" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("G" & CStr(3 + j)).Value = Format(dv.Item(j)("balance"))
                    End If

                    If ((IsDBNull(dv.Item(j)("lastTO"))) OrElse (IsDBNull(dv.Item(j)("lastTOMaster")))) Then
                        book.ActiveSheet.Range("I" & CStr(3 + j)).Value = "ТО не проводилось"
                    Else
                        book.ActiveSheet.Range("I" & CStr(3 + j)).Value = GetRussianDate2(dv.Item(j)("lastTO")) & vbLf & Format(dv.Item(j)("lastTOMaster")).Replace("<br>", vbLf)
                    End If
                Next
            End With
            If ds.Tables("route_report").Rows.Count > 0 Then
                ExcelReportFormating("A3:I" & CStr(3 + ds.Tables("route_report").Rows.Count - 1))
            End If


            book.Save()

            ProcessRoute = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessTransportTTN(ByVal cashregisters As String, ByVal region As String, ByVal goodType As String, ByVal dateTO As String) As Boolean
            Dim j As Integer
            Dim cn As SqlClient.SqlConnection
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet
            If (region = 0) Then
                cmd = New SqlClient.SqlCommand("prc_rpt_Route")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@pi_Registers", cashregisters)

            Else
                cmd = New SqlClient.SqlCommand("prc_rpt_RouteRegion")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@pi_place_rn_id", cashregisters)
                cmd.Parameters.AddWithValue("@d1", CDate(dateTO))
                cmd.Parameters.AddWithValue("@good_type", goodType)
            End If
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "route_report")

            Dim dv As DataView
            Dim k$, docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\" & Format(DateTime.Now, "yyyyMMdd")

            docFullPath = path & "\" & DocName35
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessTransportTTN = False
                    Exit Function
                End Try
            End If
            IO.File.Copy(Server.MapPath("Templates/") & DocName35, docFullPath, True)
            Try
                ' Create instance of Word!
                xlsApp = New Excel.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessTransportTTN = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)

            dv = ds.Tables("route_report").DefaultView
            With ds.Tables("route_report")
                For j = 0 To .Rows.Count - 1
                    book.ActiveSheet.Range("A" & CStr(3 + j)).Value = Format(j + 1)
                    If (IsDBNull(dv.Item(j)("payer"))) Then
                        book.ActiveSheet.Range("B" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("B" & CStr(3 + j)).Value = Format(dv.Item(j)("payer")).Replace("<br>", vbLf)
                    End If

                    book.ActiveSheet.Range("C" & CStr(3 + j)).Value = add_nulls(Format(dv.Item(j)("num_cashregister")))
                    book.ActiveSheet.Range("D" & CStr(3 + j)).Value = Format(dv.Item(j)("num_control_reestr")) & vbLf & Format(dv.Item(j)("num_control_pzu")) & vbLf & Format(dv.Item(j)("num_control_mfp")) & vbLf & Format(dv.Item(j)("num_control_cto"))
                    If (IsDBNull(dv.Item(j)("place_region"))) Then
                        book.ActiveSheet.Range("E" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("E" & CStr(3 + j)).Value = Format(dv.Item(j)("place_region"))
                    End If
                    book.ActiveSheet.Range("F" & CStr(3 + j)).Value = Format(dv.Item(j)("set_place")).Replace("<br>", vbLf)

                    If (IsDBNull(dv.Item(j)("balance"))) Then
                        book.ActiveSheet.Range("G" & CStr(3 + j)).Value = ""
                    Else
                        book.ActiveSheet.Range("G" & CStr(3 + j)).Value = Format(dv.Item(j)("balance"))
                    End If

                    If ((IsDBNull(dv.Item(j)("lastTO"))) OrElse (IsDBNull(dv.Item(j)("lastTOMaster")))) Then
                        book.ActiveSheet.Range("I" & CStr(3 + j)).Value = "ТО не проводилось"
                    Else
                        book.ActiveSheet.Range("I" & CStr(3 + j)).Value = GetRussianDate2(dv.Item(j)("lastTO")) & vbLf & Format(dv.Item(j)("lastTOMaster")).Replace("<br>", vbLf)
                    End If
                Next
            End With
            If ds.Tables("route_report").Rows.Count > 0 Then
                ExcelReportFormating("A3:I" & CStr(3 + ds.Tables("route_report").Rows.Count - 1))
            End If


            book.Save()

            ProcessTransportTTN = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Sub ExcelReportFormating(ByVal range As String)
            Try


                With book.ActiveSheet.Range(range)
                    .Name = "Table"
                    .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                    .VerticalAlignment = Excel.XlVAlign.xlVAlignCenter
                    .WrapText = True
                    .Orientation = 0
                    .AddIndent = False
                    .IndentLevel = 0
                    .ShrinkToFit = False
                    '.MergeCells = False
                    .Borders(Excel.XlBordersIndex.xlDiagonalDown).LineStyle = Excel.XlLineStyle.xlLineStyleNone
                    .Borders(Excel.XlBordersIndex.xlDiagonalUp).LineStyle = Excel.XlLineStyle.xlLineStyleNone
                    With .Borders(Excel.XlBordersIndex.xlEdgeLeft)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                    With .Borders(Excel.XlBordersIndex.xlEdgeTop)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                    With .Borders(Excel.XlBordersIndex.xlEdgeBottom)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                    With .Borders(Excel.XlBordersIndex.xlEdgeRight)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                    With .Borders(Excel.XlBordersIndex.xlInsideVertical)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                    With .Borders(Excel.XlBordersIndex.xlInsideHorizontal)
                        .LineStyle = Excel.XlLineStyle.xlContinuous
                        .Weight = Excel.XlBorderWeight.xlThin
                        .ColorIndex = Excel.XlColorIndex.xlColorIndexAutomatic
                    End With
                End With
            Catch ex As Exception

            End Try
        End Sub

        Private Function ProcessRepairRealizationAct(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal cash As Integer, ByVal history As Integer) As Boolean
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
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "RepairRealizationAct")

            cmd = New SqlClient.SqlCommand("get_repair_info")
            cmd.Parameters.AddWithValue("@pi_hc_sys_id", history)
            cmd.CommandType = CommandType.StoredProcedure
            adapt = dbSQL.GetDataAdapter(cmd)
            If Not ds.Tables("details") Is Nothing Then
                ds.Tables("details").Clear()
            End If
            adapt.Fill(ds, "details")

            If ds.Tables("details").Rows.Count = 0 Then GoTo ExitFunction
            Dim boos_name$, customer_name$, accountant$, unn$, registration$, sDate$, dogovor$

            cmd = New SqlClient.SqlCommand("get_customer_info")

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
            adapt = dbSQL.GetDataAdapter(cmd)
            If Not ds.Tables("customer") Is Nothing Then
                ds.Tables("customer").Clear()
            End If
            adapt.Fill(ds, "customer")

            If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
            Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
            If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                sEmployee = ""
            End If

            Dim act_num$ = ds.Tables("RepairRealizationAct").Rows(0)("akt")
            Dim WorkNotCall% = IIf(IsDBNull(ds.Tables("RepairRealizationAct").Rows(0)("workNotCall")), 0, ds.Tables("RepairRealizationAct").Rows(0)("workNotCall"))

            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs\")

            Dim fls As IO.File
            Dim fl As IO.FileInfo
            ProcessRepairRealizationAct = True

            Try
                ' Create instance of Word!
                wrdApp = New Word.Application
            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessRepairRealizationAct = False
                GoTo ExitFunction
            End Try

            If num_doc(0) = 32 Then

                act_num_global = act_num
                Try
                    MkDir(path & "repair\temp\")
                Catch ex As Exception
                End Try
                Try
                    docFullPath = path & "repair\temp\" & DocName32
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
                            ProcessRepairRealizationAct = False
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(Server.MapPath("Templates/") & DocName32, docFullPath, True)

                    doc = wrdApp.Documents.Open(docFullPath)
                    doc.Bookmarks("act_number1").Range.Text = act_num
                    doc.Bookmarks("num_cashregister1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
                    ' doc.Bookmarks("num_cashregister2").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")
                    doc.Bookmarks("good_name1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("good_name")
                    'doc.Bookmarks("good_type2").Range.Text = ds.Tables("defectAct").Rows(0)("good_name")
                    doc.Bookmarks("customer1").Range.Text = ds.Tables("RepairRealizationAct").Rows(0)("customer_name")
                    '  doc.Bookmarks("customer2").Range.Text = ds.Tables("defectAct").Rows(0)("customer_name")
                    'кто сделал все это

                    doc.Bookmarks("master1").Range.Text = sEmployee

                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    sDate = ""
                    doc.Bookmarks("dogovor1").Range.Text = dogovor '& " от " & sDate
                    Try
                        doc.Bookmarks("sale_date1").Range.Text = GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))
                    Catch
                        doc.Bookmarks("sale_date1").Range.Text = "-"
                    End Try
                    Dim rdate_in As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_in"))
                    Dim rdate_out As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_out"))
                    Dim sRepairDates$ = GetRussianDate(rdate_in) & " " & rdate_in.ToString("HH:mm") & " - " & GetRussianDate(rdate_out)
                    doc.Bookmarks("repair_date1").Range.Text = sRepairDates

                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                            a = a + 1
                            r1.Cells(1).Range.Text = a
                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            r1.Cells(3).Range.Text = Quantity
                            r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2)))
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2)))
                            ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2) * 1)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                            r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1))
                            r1.Cells(8).Range.Text = String.Format("{0:0.00}", (IIf(WorkNotCall = 1, Quantity * ds.Tables("details").Rows(i)("norma_hour"), "0")))

                            Cost = Cost + Math.Round(Quantity * ds.Tables("details").Rows(i)("price") / 1.0 / 1, 2) * 1
                            NDS = NDS + ndsItem
                            TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("price") * 1.2 / 1, 2) * 1

                            ' замена  детали
                            If ds.Tables("details").Rows(i)("cost_service") > 0 Then
                                NormaHour = NormaHour + Quantity * ds.Tables("details").Rows(i)("norma_hour")
                                If WorkNotCall = 0 Then
                                    r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                    a = a + 1
                                    r1.Cells(1).Range.Text = a
                                    r1.Cells(2).Range.Text = "Замена " & ds.Tables("details").Rows(i)("name")
                                    r1.Cells(3).Range.Text = ""
                                    r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1))
                                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1))
                                    ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1)
                                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                                    r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1))
                                    r1.Cells(8).Range.Text = String.Format("{0:0.00}", (Quantity * ds.Tables("details").Rows(i)("norma_hour")))
                                    Cost = Cost + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1
                                    NDS = NDS + ndsItem
                                    TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1
                                End If
                            End If
                        Else
                            If WorkNotCall = 0 Then
                                r1 = doc.Tables(1).Rows.Add(doc.Tables(1).Rows(a + 2))
                                a = a + 1
                                r1.Cells(1).Range.Text = a
                                r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                                r1.Cells(3).Range.Text = ""
                                Quantity = ds.Tables("details").Rows(i)("quantity")
                                r1.Cells(4).Range.Text = String.Format("{0:0.00}", (Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2)))
                                r1.Cells(5).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2)))

                                ndsItem = Quantity * (Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1 - Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1)
                                r1.Cells(6).Range.Text = String.Format("{0:0.00}", ndsItem)
                                r1.Cells(7).Range.Text = String.Format("{0:0.00}", (Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2)))
                                r1.Cells(8).Range.Text = String.Format("{0:0.00}", (Quantity * ds.Tables("details").Rows(i)("norma_hour")))
                                NormaHour = NormaHour + Quantity * ds.Tables("details").Rows(i)("norma_hour")
                                Cost = Cost + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.0 / 1, 2) * 1
                                NDS = NDS + ndsItem
                                TotalSum = TotalSum + Quantity * Math.Round(ds.Tables("details").Rows(i)("cost_service") * 1.2 / 1, 2) * 1
                            End If
                        End If
                    Next
                    r1 = doc.Tables(1).Rows.Last
                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", Cost)
                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", NDS)
                    r1.Cells(7).Range.Text = String.Format("{0:0.00}", TotalSum)
                    r1.Cells(8).Range.Text = String.Format("{0:0.00}", NormaHour)

                    doc.Bookmarks("cost1").Range.Text = Summa_propis(Cost)
                    doc.Bookmarks("NDS1").Range.Text = Summa_propis(NDS)
                    doc.Bookmarks("Summa1").Range.Text = Summa_propis(TotalSum)
                    doc.Bookmarks("norma_hour").Range.Text = CStr(NormaHour) & " ч."

                    doc.Save()

                    IO.File.Copy(docFullPath, path & "repair\" & DocName32, True)

                    '
                    'Сохраняем инфу для экспорта на сайт
                    '



                    'находим УНН клиента
                    Dim customer_unn = dbSQL.ExecuteScalar("SELECT unn FROM customer WHERE customer_sys_id='" & GetPageParam("c") & "'")

                    'Копируем док
                    IO.File.Copy(docFullPath, Server.MapPath("XML/repair_docs/" & Trim(customer_unn) & "+" & Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ".doc"), True)

                    Dim export_content = Trim(customer_unn) & ";" & Trim(ds.Tables("RepairRealizationAct").Rows(0)("num_cashregister")) & ";" & Now & ";ready;" & TotalSum & vbCrLf

                    Dim content_temp
                    Dim file_open As IO.StreamReader
                    i = 1
                    file_open = IO.File.OpenText(Server.MapPath("XML/new_repair.csv"))
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
                        file_save = IO.File.CreateText(Server.MapPath("XML/new_repair.csv"))
                        file_save.Write(export_content)
                        file_save.Close()
                    Catch ex As Exception
                    End Try


                Catch
                    WriteError("Акт о проведении ремонта<br>" & Err.Description)
                    ProcessRepairRealizationAct = False
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
                            ProcessRepairRealizationAct = False
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(Server.MapPath("Templates/") & DocName33, docFullPath, True)

                    doc = wrdApp.Documents.Open(docFullPath)

                    'doc.Bookmarks("Boos").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                    'doc.Bookmarks("Boos2").Range.Text = ds.Tables("sale").Rows(0)("proxy")
                    doc.Bookmarks("CustomerAddress").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                    doc.Bookmarks("CustomerName").Range.Text = customer_name
                    doc.Bookmarks("Dogovor").Range.Text = act_num
                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    sDate = GetRussianDate(ds.Tables("RepairRealizationAct").Rows(0)("sale_date"))


                    Dim s1$ = ds.Tables("customer").Rows(0)("bank")
                    If s1.Trim.Length = 0 Then s1 = "нет"
                    doc.Bookmarks("Bank").Range.Text = s1
                    doc.Bookmarks("UNN1").Range.Text = unn
                    doc.Bookmarks("UNN2").Range.Text = unn
                    doc.Bookmarks("Date").Range.Text = dogovor '& " от " & sDate
                    doc.Bookmarks("Date2").Range.Text = GetRussianDate(Now)
                    doc.Bookmarks("Razreshil").Range.Text = "Яско Владимир Федорович" 'ds.Tables("sale").Rows(0)("razreshil")
                    'If ds.Tables("sale").Rows(0)("firm_sys_id") <> 1 Then
                    '    doc.Bookmarks("FirmName1").Range.Text = ds.Tables("sale").Rows(0)("firm_name")
                    '    doc.Bookmarks("Rekvisit").Range.Text = ds.Tables("sale").Rows(0)("rekvisit")
                    '    doc.Bookmarks("Employee").Range.Text = ds.Tables("sale").Rows(0)("fio")
                    'Else
                    'кто сделал все это

                    doc.Bookmarks("Employee").Range.Text = sEmployee

                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = a
                            r1.Cells(2).Range.Text = ds.Tables("details").Rows(i)("name")
                            r1.Cells(3).Range.Text = "шт."
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            r1.Cells(4).Range.Text = Quantity
                            p = Math.Round(ds.Tables("details").Rows(i)("price") / 1.2, 2)
                            sSum = Quantity * ds.Tables("details").Rows(i)("price")
                            sNDS = (sSum - Quantity * p)
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
                    doc.Bookmarks("TotalNDS").Range.Text = String.Format("{0:0.00}", dNDS)
                    doc.Bookmarks("TotalAll").Range.Text = String.Format("{0:0.00}", dTotal)
                    doc.Bookmarks("Total").Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
                    doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                    doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                    doc.Bookmarks("Count").Range.Text = Summa_propis(ds.Tables("details").Rows.Count, False)

                    doc.Save()

                Catch
                    WriteError("Товарная накладная<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessRepairRealizationAct = False
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
                            ProcessRepairRealizationAct = False
                            Exit Function
                        End Try
                    End If
                    IO.File.Copy(Server.MapPath("Templates/") & DocName34, docFullPath, True)

                    doc = wrdApp.Documents.Open(docFullPath)

                    Dim rdate_in As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_in"))
                    Dim rdate_out As DateTime = CDate(ds.Tables("RepairRealizationAct").Rows(0)("repairdate_out"))
                    sDate = GetRussianDate(rdate_out)


                    dogovor = ds.Tables("RepairRealizationAct").Rows(0)("dogovor")
                    'doc.Bookmarks("Dogovor").Range.Text = act_num
                    doc.Bookmarks("RECIPIENT_ADDRESS").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
                    doc.Bookmarks("RECIPIENT_NAME").Range.Text = ds.Tables("customer").Rows(0)("customer_name")
                    doc.Bookmarks("RECIPIENT_UNN").Range.Text = ds.Tables("customer").Rows(0)("unn")
                    doc.Bookmarks("Date").Range.Text = sDate
                    doc.Bookmarks("Date1").Range.Text = sDate
                    doc.Bookmarks("BILL_DATE").Range.Text = sDate
                    doc.Bookmarks("ACCEPT_INVOICE").Range.Text = "к акту №" & act_num & " от " & rdate_out.ToString("dd.MM.yyyy") & "г."
                    doc.Bookmarks("Saler").Range.Text = sEmployee
                    Dim a As Integer = 0
                    Dim ndsItem As Double = 0

                    For i = 0 To ds.Tables("details").Rows.Count - 1

                        If ds.Tables("details").Rows(i)("is_detail") = True Then
                            ' детали
                            r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            p = Math.Round(ds.Tables("details").Rows(i)("price") / 1.2, 2)
                            sSum = Quantity * ds.Tables("details").Rows(i)("price")
                            sNDS = (sSum - Quantity * p)
                            r1.Cells(2).Range.Text = String.Format("{0:0.00}", (Quantity * p))
                            r1.Cells(3).Range.Text = " "
                            r1.Cells(4).Range.Text = "20"
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                            If ds.Tables("details").Rows(i)("cost_service") > 0 Then
                                r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(a + 3))
                                a = a + 1
                                r1.Cells(1).Range.Text = "Замена " & ds.Tables("details").Rows(i)("name")
                                Quantity = ds.Tables("details").Rows(i)("quantity")
                                p = Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.2, 2)
                                sSum = Quantity * ds.Tables("details").Rows(i)("cost_service")
                                sNDS = (sSum - Quantity * p)
                                r1.Cells(2).Range.Text = Quantity * p
                                r1.Cells(3).Range.Text = " "
                                r1.Cells(4).Range.Text = "20"
                                r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                                r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                                dNDS = dNDS + sNDS
                                dTotal = dTotal + sSum
                            End If
                        Else
                            r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(a + 3))
                            a = a + 1
                            r1.Cells(1).Range.Text = ds.Tables("details").Rows(i)("name")
                            Quantity = ds.Tables("details").Rows(i)("quantity")
                            p = Math.Round(ds.Tables("details").Rows(i)("cost_service") / 1.2, 2)
                            sSum = Quantity * ds.Tables("details").Rows(i)("cost_service")
                            sNDS = (sSum - Quantity * p)
                            r1.Cells(2).Range.Text = String.Format("{0:0.00}", (Quantity * p))
                            r1.Cells(3).Range.Text = " "
                            r1.Cells(4).Range.Text = "20"
                            r1.Cells(5).Range.Text = String.Format("{0:0.00}", sNDS)
                            r1.Cells(6).Range.Text = String.Format("{0:0.00}", sSum)
                            dNDS = dNDS + sNDS
                            dTotal = dTotal + sSum
                        End If
                    Next
                    r1 = doc.Tables(3).Rows.Last
                    r1.Cells(2).Range.Text = String.Format("{0:0.00}", (dTotal - dNDS))
                    r1.Cells(3).Range.Text = ""
                    r1.Cells(4).Range.Text = "x"
                    r1.Cells(5).Range.Text = String.Format("{0:0.00}", dNDS)
                    r1.Cells(6).Range.Text = String.Format("{0:0.00}", dTotal)


                    'doc.Bookmarks("TotalNDS").Range.Text = dNDS
                    'doc.Bookmarks("TotalAll").Range.Text = dTotal
                    'doc.Bookmarks("Total").Range.Text = dTotal - dNDS
                    doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dNDS)
                    doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal)

                    doc.Save()


                Catch
                    WriteError("Счет-фактура  по НДС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                    ProcessRepairRealizationAct = False
                    GoTo ExitFunction
                End Try

            End If
ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing

            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessRestReport(ByVal group As String) As Boolean
            Dim j As Integer
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet
            cmd = New SqlClient.SqlCommand("get_good_rest_by_group")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            If (group > 0) Then
                cmd.Parameters.AddWithValue("@pi_good_group_sys_id", group)
            End If

            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "rest_report")

            Dim dv As DataView
            Dim k$, docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\Rest\" & Format(DateTime.Now, "yyyyMMdd")
            docFullPath = path & "\" & DocName51
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessRestReport = False
                    Exit Function
                End Try
            End If
            IO.File.Copy(Server.MapPath("Templates/") & DocName51, docFullPath, True)
            Try
                'Create instance of Word!
                xlsApp = New Excel.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessRestReport = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables("rest_report").DefaultView
            Dim group_name$ = ""
            Dim a% = 0
            With ds.Tables("rest_report")

                For j = 0 To .Rows.Count - 1
                    Try


                        Dim price$
                        Dim priceWithNDS$
                        If Not IsDBNull(dv.Item(j)("price")) Then
                            price = Math.Round(CDbl(dv.Item(j)("price")), 2)
                            priceWithNDS = Math.Round(CDbl(dv.Item(j)("price") * 1.2), 2)
                        Else
                            If Not IsDBNull(dv.Item(j)("price_opt")) Then
                                price = Math.Round(CDbl(dv.Item(j)("price_opt")), 2)
                                priceWithNDS = Math.Round(CDbl(dv.Item(j)("price_opt") * 1.2), 2)
                            Else
                                price = "нет информации о цене"
                                priceWithNDS = ""
                            End If
                        End If

                        If (group_name <> dv.Item(j)("group_name")) Then
                            group_name = dv.Item(j)("group_name")
                            book.ActiveSheet.Range("B" & CStr(3 + a)).Value = group_name
                            With book.ActiveSheet.Range("A" & CStr(3 + a) + ":E" + CStr(3 + a))
                                .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                                .VerticalAlignment = Excel.XlHAlign.xlHAlignCenter
                                .WrapText = True
                                .Orientation = 0
                                .AddIndent = False
                                .IndentLevel = 0
                                '.ReadingOrder = Excel.Xl
                                .ShrinkToFit = False
                                .MergeCells = False
                            End With
                            book.ActiveSheet.Range("A" & CStr(3 + a) + ":E" + CStr(3 + a)).Merge()
                            book.ActiveSheet.Range("A" & CStr(3 + a) + ":E" + CStr(3 + a)).Font.ColorIndex = 5
                            With book.ActiveSheet.Range("A" & CStr(3 + a) + ":E" + CStr(3 + a)).Interior
                                .ColorIndex = 15
                                .Pattern = Excel.XlPattern.xlPatternSolid
                            End With
                            a = a + 1
                        End If
                        book.ActiveSheet.Range("A" & CStr(3 + a)).Value = Format(dv.Item(j)("artikul"))
                        book.ActiveSheet.Range("B" & CStr(3 + a)).Value = Format(dv.Item(j)("name"))
                        book.ActiveSheet.Range("C" & CStr(3 + a)).Value = Format(dv.Item(j)("quantity"))
                        book.ActiveSheet.Range("D" & CStr(3 + a)).Value = Format(price)
                        book.ActiveSheet.Range("E" & CStr(3 + a)).Value = Format(priceWithNDS)
                        a = a + 1
                    Catch ex As Exception
                    End Try
                Next
            End With
            If ds.Tables("rest_report").Rows.Count > 0 Then
                ExcelReportFormating("A3:E" & CStr(3 + a - 1)) 'ds.Tables("rest_report").Rows.Count - 1))
            End If


            book.Save()

            ProcessRestReport = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessMasterTOReport(ByVal date_start As DateTime, ByVal date_end As DateTime, ByVal period As Int32, ByVal executors As String) As Boolean
            Dim j As Integer

            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet
            If period = 0 Then
                cmd = New SqlClient.SqlCommand("prc_rpt_MasterTO")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                If Not executors Is Nothing AndAlso executors <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_executor", executors)
                End If
            Else
                cmd = New SqlClient.SqlCommand("prc_rpt_MasterTOForPeriod")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                If Not executors Is Nothing AndAlso executors <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_executor", executors)
                End If
            End If

            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "master_TO_report")

            Dim dv As DataView
            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\MasterTO\" & Format(DateTime.Now, "yyyyMMdd")
            docFullPath = path & "\" & DocName53
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessMasterTOReport = False
                    Exit Function
                End Try
            End If
            IO.File.Copy(Server.MapPath("Templates/") & DocName53, docFullPath, True)
            Try
                ' Create instance of Word!
                xlsApp = New Excel.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessMasterTOReport = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables("master_TO_report").DefaultView
            dv.Sort = " balance desc,change_state_date"
            Dim customer_name$ = ""
            Dim a% = 0
            Dim startRow% = 8
            Dim marka$ = ""
            book.ActiveSheet.Range("B3").Value = "Начальная дата : " & Format(date_start, "dd.MM.yyyy")
            book.ActiveSheet.Range("B4").Value = "Конечная дата :  " & Format(date_end, "dd.MM.yyyy")

            With ds.Tables("master_TO_report")

                For j = 0 To .Rows.Count - 1

                    If (customer_name <> dv.Item(j)("customer_name")) Then
                        customer_name = dv.Item(j)("customer_name")
                        book.ActiveSheet.Range("A" & CStr(startRow + a)).Value = customer_name.Replace("<br>", vbLf)
                        With book.ActiveSheet.Range("A" & CStr(startRow + a) + ":G" + CStr(startRow + a))
                            .HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
                            .VerticalAlignment = Excel.XlHAlign.xlHAlignCenter
                            .WrapText = True
                            .Orientation = 0
                            .AddIndent = False
                            .IndentLevel = 0
                            '.ReadingOrder = Excel.Xl
                            .ShrinkToFit = False
                            .MergeCells = False
                        End With
                        book.ActiveSheet.Range("A" & CStr(startRow + a) + ":H" + CStr(startRow + a)).Merge()
                        book.ActiveSheet.Range("A" & CStr(startRow + a) + ":H" + CStr(startRow + a)).Font.ColorIndex = 5
                        With book.ActiveSheet.Range("A" & CStr(startRow + a) + ":I" + CStr(startRow + a)).Interior
                            .ColorIndex = 15
                            .Pattern = Excel.XlPattern.xlPatternSolid
                        End With
                        book.ActiveSheet.Range("I" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("balance")), "", dv.Item(j)("balance")))

                        a = a + 1

                    End If
                    book.ActiveSheet.Range("A" & CStr(startRow + a)).Value = Format(j + 1)
                    book.ActiveSheet.Range("B" & CStr(startRow + a)).Value = Format(dv.Item(j)("change_state_date"), "dd.MM.yyyy")
                    book.ActiveSheet.Range("C" & CStr(startRow + a)).Value = add_nulls(Format(dv.Item(j)("num_cashregister")))
                    marka = Format(IIf(IsDBNull(dv.Item(j)("marka_cto_out")), "", dv.Item(j)("marka_cto_out"))) & " " & Format(IIf(IsDBNull(dv.Item(j)("marka_cto2_out")), "", dv.Item(j)("marka_cto2_out"))) & " " & Format(IIf(IsDBNull(dv.Item(j)("marka_reestr_out")), "", dv.Item(j)("marka_reestr_out"))) & " " & Format(IIf(IsDBNull(dv.Item(j)("marka_pzu_out")), "", dv.Item(j)("marka_pzu_out"))) & " " & Format(IIf(IsDBNull(dv.Item(j)("marka_mfp_out")), "", dv.Item(j)("marka_mfp_out"))) & " " & Format(IIf(IsDBNull(dv.Item(j)("marka_cp_out")), "", dv.Item(j)("marka_cp_out")))
                    book.ActiveSheet.Range("D" & CStr(startRow + a)).Value = Format(marka)
                    book.ActiveSheet.Range("E" & CStr(startRow + a)).Value = Format(GetRussianDate3(dv.Item(j)("start_date")))
                    book.ActiveSheet.Range("F" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("set_place")), "", dv.Item(j)("set_place")))
                    book.ActiveSheet.Range("G" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("place_region")), "", dv.Item(j)("place_region")))
                    book.ActiveSheet.Range("H" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("employee_name")), "", dv.Item(j)("employee_name")))
                    a = a + 1
                Next
            End With
            If ds.Tables("master_TO_report").Rows.Count > 0 Then
                ExcelReportFormating("A" & CStr(startRow) & ":I" & CStr(startRow + a - 1))
                book.ActiveSheet.Range("A" & CStr(startRow) & ":I" & CStr(startRow + a - 1)).EntireRow.AutoFit()

            End If
            Dim formula$ = "=SUM(R[-" & a & "]C:R[-1]C)"
            book.ActiveSheet.Range("I" & CStr(startRow + a)).FormulaR1C1 = formula
            book.ActiveSheet.Range("I" & CStr(startRow) & ":I" & CStr(startRow + a)).Font.ColorIndex = 3
            book.Save()

            ProcessMasterTOReport = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessWarehouseCardReport(ByVal good_type_id As String) As Boolean
            Dim j As Integer
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet
            cmd = New SqlClient.SqlCommand("prc_rpt_TurnoverListByGoodForCard")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", good_type_id)

            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "warehouse_card_report")

            Dim dv As DataView
            Dim k$, docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\Card\" & good_type_id & "\" & Format(DateTime.Now, "yyyyMMdd")
            docFullPath = path & "\" & DocName52
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessWarehouseCardReport = False
                    Exit Function
                End Try
            End If
            IO.File.Copy(Server.MapPath("Templates/") & DocName52, docFullPath, True)
            Try
                ' Create instance of Word!
                xlsApp = New Excel.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessWarehouseCardReport = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables("warehouse_card_report").DefaultView
            Dim group_name$ = ""
            Dim a% = 0
            Dim rest As Double
            rest = 0
            Dim startRow% = 26
            With ds.Tables("warehouse_card_report")

                For j = 0 To .Rows.Count - 1

                    If j = 0 Then
                        book.ActiveSheet.Range("J9").Value = Format(dv.Item(j)("good_name"))
                    End If

                    book.ActiveSheet.Range("A" & CStr(startRow + a)).Value = Format(dv.Item(j)("dateDoc"), "dd.MM.yyyy")
                    '
                    book.ActiveSheet.Range("D" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("document")), "", dv.Item(j)("document")))
                    book.ActiveSheet.Range("H" & CStr(startRow + a)).Value = Format(dv.Item(j)("typeDoc"))
                    If (dv.Item(j)("typeDoc")) = "Приход" Then
                        book.ActiveSheet.Range("AD14").Value = Format(IIf(IsDBNull(dv.Item(j)("units")), "", dv.Item(j)("units")))
                        book.ActiveSheet.Range("AI14").Value = Format(IIf(IsDBNull(dv.Item(j)("price")), "", dv.Item(j)("price")))
                    End If
                    book.ActiveSheet.Range("O" & CStr(startRow + a)).Value = Format(j + 1)
                    book.ActiveSheet.Range("Q" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("customer_name")), "", dv.Item(j)("customer_name")))
                    book.ActiveSheet.Range("AA" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("prichod_Kol")), "", dv.Item(j)("prichod_Kol")))
                    book.ActiveSheet.Range("AE" & CStr(startRow + a)).Value = Format(IIf(IsDBNull(dv.Item(j)("rashod_Kol")), "", dv.Item(j)("rashod_Kol")))
                    rest = rest + CDbl(dv.Item(j)("ostatok"))
                    book.ActiveSheet.Range("AI" & CStr(startRow + a)).Value = Format(rest)
                    a = a + 1
                Next
            End With
            book.ActiveSheet.Range("Q" & CStr(startRow + a)).Value = "Остаток на " & Format(Now, "dd.MM.yyyy")
            book.ActiveSheet.Range("AI" & CStr(startRow + a)).Value = Format(rest)
            If ds.Tables("warehouse_card_report").Rows.Count > 0 Then
                ExcelReportFormating("A" & startRow & ":AP" & CStr(startRow + a)) 'ds.Tables("rest_report").Rows.Count - 1))
            End If
            book.ActiveSheet.Range("A" & startRow + a + 1 & ":A1200").EntireRow.Delete()

            book.Save()

            ProcessWarehouseCardReport = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessDefectAct(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal cash As Integer, ByVal history As Integer) As Boolean
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            ds = New DataSet

            cmd = New SqlClient.SqlCommand("prc_rpt_DefectAct")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_sys_id", cash)
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
            cmd.Parameters.AddWithValue("@pi_sys_id", history)
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "defectAct")

            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\DefectActs"
            docFullPath = path & "\" & DocName31
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            ProcessDefectAct = True

            Try
                ' Create instance of Word!
                wrdApp = New Word.Application
            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessDefectAct = False
                GoTo ExitFunction
            End Try
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
                        ProcessDefectAct = False
                        Exit Function
                    End Try
                End If

                Dim repare_in_info = ds.Tables("defectAct").Rows(0)("repare_in_info")
                If repare_in_info.ToString.Length > 1 Then
                    Dim arr_parce() As String
                    arr_parce = Split(repare_in_info, ",")
                    Dim i = 0
                    repare_in_info = ""
                    While i < arr_parce.Length - 1
                        query = dbSQL.ExecuteScalar("SELECT name FROM repair_bads WHERE sys_id='" & arr_parce(i) & "'")
                        repare_in_info &= query & " ("
                        query = dbSQL.ExecuteScalar("SELECT price FROM repair_bads WHERE sys_id='" & arr_parce(i) & "'")
                        repare_in_info &= query & "), "
                        i += 1
                    End While
                End If

                IO.File.Copy(Server.MapPath("Templates/") & DocName31, docFullPath, True)
                doc = wrdApp.Documents.Open(docFullPath)

                doc.Bookmarks("num_cashregister1").Range.Text = add_nulls(ds.Tables("defectAct").Rows(0)("num_cashregister"))
                doc.Bookmarks("num_cashregister2").Range.Text = add_nulls(ds.Tables("defectAct").Rows(0)("num_cashregister"))
                doc.Bookmarks("good_type1").Range.Text = ds.Tables("defectAct").Rows(0)("good_name")
                doc.Bookmarks("good_type3").Range.Text = ds.Tables("defectAct").Rows(0)("good_name")
                doc.Bookmarks("customer1").Range.Text = ds.Tables("defectAct").Rows(0)("customer_name")
                doc.Bookmarks("customer2").Range.Text = ds.Tables("defectAct").Rows(0)("customer_name")
                doc.Bookmarks("phone1").Range.Text = ds.Tables("defectAct").Rows(0)("phone")
                doc.Bookmarks("phone2").Range.Text = ds.Tables("defectAct").Rows(0)("phone")
                doc.Bookmarks("repair_bads").Range.Text = repare_in_info
                doc.Bookmarks("repair_bads2").Range.Text = repare_in_info

                'кто сделал все это

                Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
                If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                Else
                    doc.Bookmarks("master1").Range.Text = sEmployee
                    doc.Bookmarks("master2").Range.Text = sEmployee
                End If

                'doc.Bookmarks("repairdate_in").Range.Text = GetRussianDate1(Now) & " " & Now.ToString("HH:mm")
                'doc.Bookmarks("repairdate_in2").Range.Text = GetRussianDate1(Now) & " " & Now.ToString("HH:mm")

                Dim d As DateTime
                d = ds.Tables("defectAct").Rows(0)("repairdate_in")
                doc.Bookmarks("repairdate_in").Range.Text = GetRussianDate1(d) & " " & d.ToString("HH:mm")
                doc.Bookmarks("repairdate_in2").Range.Text = GetRussianDate1(d) & " " & d.ToString("HH:mm")
                doc.Bookmarks("repairdate_in3").Range.Text = GetRussianDate1(d)
                doc.Bookmarks("repairdate_in4").Range.Text = GetRussianDate1(d)

                doc.Save()
            Catch
                WriteError("Акт приемки и выдачи из ремонта<br>" & Err.Description)
                ProcessDefectAct = False
                GoTo ExitFunction
            End Try
ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing
            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try
        End Function

        Public Function ProcessKKMListTO() As Boolean
            Dim j, a As Integer
            Dim ds As DataSet

            ds = Session("KKM_ds")

            Dim dv As DataView
            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\Reports\" & Format(DateTime.Now, "yyyyMMdd")
            docFullPath = path & "\" & DocName54
            Dim fls As IO.File
            Dim fl As IO.FileInfo

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
                    ProcessKKMListTO = False
                    Exit Function
                End Try
            End If
            Try
                IO.File.Copy(Server.MapPath("Templates/") & DocName54, docFullPath, True)
            Catch ex As Exception
            End Try
            Try
                ' Create instance of Excel!
                xlsApp = New Excel.Application
            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessKKMListTO = False
                GoTo ExitFunction
            End Try

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables(0).DefaultView
            Dim selected_KKM As String() = Session("selected_KKM").ToString.Split(",")
            a = 0
            With ds.Tables(0)
                For j = 0 To .Rows.Count - 1
                    Try
                        Dim good_id$ = Format(dv.Item(j)("good_sys_id"))
                        Dim index As Integer = Array.IndexOf(selected_KKM, good_id)

                        If index <> -1 Then
                            book.ActiveSheet.Range("A" & CStr(3 + a)).Value = Format(a + 1)

                            Dim parced_text = Format(dv.Item(j)("payerInfo")).Replace("<br>", " ")

                            'Достаем договор
                            Dim payersysid = dv.Item(j)("payer_sys_id")
                            Dim dogovor_text
                            Dim reader As SqlClient.SqlDataReader
                            Dim query = "SELECT dogovor FROM customer WHERE customer_sys_id='" & payersysid & "'"

                            reader = dbSQL.GetReader(query)
                            If reader.Read() Then
                                Try
                                    dogovor_text = reader.Item(0)
                                Catch
                                End Try
                            Else
                            End If
                            reader.Close()
                            '
                            book.ActiveSheet.Range("B" & CStr(3 + a)).Value = parced_text & "; Договор: " & dogovor_text
                            '
                            book.ActiveSheet.Range("C" & CStr(3 + a)).Value = Format(dv.Item(j)("good_name")) & vbLf & add_nulls(Format(dv.Item(j)("num_cashregister")))
                            book.ActiveSheet.Range("D" & CStr(3 + a)).Value = Format(dv.Item(j)("num_control_reestr")) & vbLf & Format(dv.Item(j)("num_control_pzu")) & vbLf & Format(dv.Item(j)("num_control_mfp")) & vbLf & Format(dv.Item(j)("num_control_cto"))
                            If (IsDBNull(dv.Item(j)("place_region"))) Then
                                book.ActiveSheet.Range("E" & CStr(3 + a)).Value = ""
                            Else
                                book.ActiveSheet.Range("E" & CStr(3 + a)).Value = Format(dv.Item(j)("place_region"))
                            End If
                            book.ActiveSheet.Range("F" & CStr(3 + a)).Value = Format(dv.Item(j)("set_place")).Replace("<br>", vbLf)
                            If (IsDBNull(dv.Item(j)("dolg"))) Then
                                book.ActiveSheet.Range("G" & CStr(3 + a)).Value = ""
                            Else
                                book.ActiveSheet.Range("G" & CStr(3 + a)).Value = Format(dv.Item(j)("dolg"))
                            End If

                            If ((IsDBNull(dv.Item(j)("lastTO"))) OrElse (IsDBNull(dv.Item(j)("lastTOMaster")))) Then
                                book.ActiveSheet.Range("I" & CStr(3 + a)).Value = "ТО не проводилось"
                            Else
                                book.ActiveSheet.Range("I" & CStr(3 + a)).Value = GetRussianDate2(dv.Item(j)("lastTO")) & vbLf & Format(dv.Item(j)("lastTOMaster")).Replace("<br>", vbLf)
                            End If

                            Try
                                book.ActiveSheet.Range("J" & CStr(3 + a)).Value = Format(dv.Item(j)("cto_master"))
                            Catch
                            End Try


                            a = a + 1
                        End If
                    Catch ex As Exception
                    End Try
                Next
            End With

            If a > 0 Then
                ExcelReportFormating("A3:J" & CStr(3 + a - 1))
            End If

            book.Save()

            ProcessKKMListTO = True
ExitFunction:
            Try
                ds.Clear()
                If Not xlsApp Is Nothing Then
                    xlsApp.Quit()
                End If
                xlsApp = Nothing
                ds = Nothing
                xlsApp.Quit()
            Catch
                WriteError("Аварийное завершение работы Microsoft Excel<br>" & Err.Description)
            End Try
        End Function

        Private Function ProcessDefectActForGood(ByVal num_doc() As Integer, ByVal customer As Integer) As Boolean
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim customer_name$

            ds = New DataSet
            cmd = New SqlClient.SqlCommand("get_customer_info")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
            adapt = dbSQL.GetDataAdapter(cmd)
            If Not ds.Tables("customer") Is Nothing Then
                ds.Tables("customer").Clear()
            End If
            adapt.Fill(ds, "customer")

            If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
            If Not IsDBNull(ds.Tables("customer").Rows(0)("dogovor")) Then
                customer_name = String.Format("{0} Дог №{1}, {2}", ds.Tables("customer").Rows(0)("customer_name"), ds.Tables("customer").Rows(0)("dogovor"), ds.Tables("customer").Rows(0)("customer_address"))
            Else
                customer_name = String.Format("{0}, {2}", ds.Tables("customer").Rows(0)("customer_name"), ds.Tables("customer").Rows(0)("customer_address"))
            End If
            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs") & "\DefectActs"
            docFullPath = path & "\" & DocName42
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            ProcessDefectActForGood = True

            Try
                ' Create instance of Word!
                wrdApp = New Word.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessDefectActForGood = False
                GoTo ExitFunction
            End Try
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
                        ProcessDefectActForGood = False
                        Exit Function
                    End Try
                End If
                IO.File.Copy(Server.MapPath("Templates/") & DocName42, docFullPath, True)

                doc = wrdApp.Documents.Open(docFullPath)

                doc.Bookmarks("num_cashregister1").Range.Text = "<сер номер>"
                doc.Bookmarks("num_cashregister2").Range.Text = "<сер номер>"
                doc.Bookmarks("good_type1").Range.Text = "<товар>"
                doc.Bookmarks("good_type2").Range.Text = ""
                doc.Bookmarks("good_type3").Range.Text = "<товар>"
                doc.Bookmarks("good_type4").Range.Text = ""
                doc.Bookmarks("customer1").Range.Text = customer_name
                doc.Bookmarks("customer2").Range.Text = customer_name
                doc.Bookmarks("phone1").Range.Text = ds.Tables("customer").Rows(0)("customer_phone")
                doc.Bookmarks("phone2").Range.Text = ds.Tables("customer").Rows(0)("customer_phone")
                'кто сделал все это

                Dim sEmployee$ = dbSQL.ExecuteScalar("select Name from Employee where sys_id='" & CStr(CurrentUser.sys_id) & "'")
                If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                Else
                    doc.Bookmarks("master1").Range.Text = sEmployee
                    doc.Bookmarks("master2").Range.Text = sEmployee
                End If
                doc.Bookmarks("repairdate_in").Range.Text = GetRussianDate1(Now) & " " & Now.ToString("HH:mm")
                doc.Bookmarks("repairdate_in2").Range.Text = GetRussianDate1(Now) & " " & Now.ToString("HH:mm")

                doc.Save()

            Catch
                WriteError("Акт приемки и выдачи из ремонта<br>" & Err.Description)
                ProcessDefectActForGood = False
                GoTo ExitFunction
            End Try
ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing

            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try
        End Function

        Public Function ProcessIzveschenieDocuments(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal sale As Integer, Optional ByVal vid_plateza As Integer = 0, Optional ByVal sub_num As Integer = -1, Optional ByVal isRefresh As Boolean = True) As Boolean

            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim fls As IO.File
            Dim fl As IO.FileInfo
            Dim docFullPath$
            Dim boos_name$, customer_name$, accountant$, unn$, saler$, sDate$

            Dim path$ = Server.MapPath("Docs") & "\" & customer

            ProcessIzveschenieDocuments = True

            Try
                'Create folders and copy templates
                Dim fldr As New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If
                path = path & "\" & sale & "\"
                fldr = New IO.DirectoryInfo(path)
                If Not fldr.Exists Then
                    fldr.Create()
                End If

            Catch ex As Exception
                WriteError(Err.Description & "<BR>" & ex.ToString)
                ProcessIzveschenieDocuments = False
                Exit Function
            End Try

            Try
                ds = New DataSet
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer)
                adapt = dbSQL.GetDataAdapter(cmd)
                If Not ds.Tables("customer") Is Nothing Then
                    ds.Tables("customer").Clear()
                End If
                adapt.Fill(ds, "customer")

                If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction
                boos_name = ds.Tables("customer").Rows(0)("boos_name")
                customer_name = ds.Tables("customer").Rows(0)("customer_name")
                If Not customer_name.StartsWith("ИП") Then
                    customer_name = customer_name + " через " & boos_name
                End If

                accountant = ds.Tables("customer").Rows(0)("accountant")
                unn = ds.Tables("customer").Rows(0)("unn")

                If vid_plateza = 0 Then
                    'sale
                    cmd = New SqlClient.SqlCommand("get_sale_info")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("sale") Is Nothing Then
                        ds.Tables("sale").Clear()
                    End If
                    adapt.Fill(ds, "sale")
                    If ds.Tables("sale").Rows.Count = 0 Then GoTo ExitFunction

                    saler = ds.Tables("sale").Rows(0)("saler")
                    sDate = GetRussianDate(ds.Tables("sale").Rows(0)("sale_date"))

                    cmd = New SqlClient.SqlCommand("get_goods_info_by_sale")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    If Not ds.Tables("goods") Is Nothing Then
                        ds.Tables("goods").Clear()
                    End If
                    adapt.Fill(ds, "goods")

                    If ds.Tables("goods").Rows.Count = 0 Then
                        WriteError("Не выбраны товары для данного клиента и продажи")
                        ProcessIzveschenieDocuments = False
                        GoTo ExitFunction
                    End If
                End If

            Catch ex As Exception

                WriteError("Загрузка данных<br>" & Err.Description)
                ProcessIzveschenieDocuments = False
                GoTo ExitFunction

            End Try


            Try
                ' Create instance of Word!
                wrdApp = New Word.Application

            Catch ex As Exception
                WriteError(Err.Description & "<br>" & ex.ToString)
                ProcessIzveschenieDocuments = False
                GoTo ExitFunction
            End Try

            Dim i%, j%, k%
            Dim q, p, dTotal As Double
            Dim iGoodType%
            For k = num_doc.GetLowerBound(0) To num_doc.GetUpperBound(0)

                dTotal = 0
                If num_doc(k) = 41 Then

                    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                    'Извещение
                    Try
                        docFullPath = path & DocName41

                        fl = New IO.FileInfo(docFullPath)
                        If fl.Exists() Then
                            Try
                                fl.Delete()
                            Catch
                            End Try
                        End If
                        IO.File.Copy(Server.MapPath("Templates/") & DocName41, docFullPath, True)

                        doc = wrdApp.Documents.Open(docFullPath)

                        doc.Bookmarks("Customer_info").Range.Text = unn & " " & customer_name & " " & ds.Tables("customer").Rows(0)("customer_address")
                        doc.Bookmarks("Customer_info1").Range.Text = unn & " " & customer_name & " " & ds.Tables("customer").Rows(0)("customer_address")

                        If vid_plateza = 0 Then
                            iGoodType = -1
                            j = 1
                            For i = 0 To ds.Tables("goods").Rows.Count - 1
                                If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then

                                    If i <> 0 Then
                                        dTotal = dTotal + q * p

                                    End If
                                    iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
                                    j = j + 1
                                    q = 0
                                    p = CDbl(ds.Tables("goods").Rows(i)("price"))
                                End If

                                q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))

                            Next
                            dTotal = dTotal + q * p
                            doc.Bookmarks("Vid_Plateza").Range.Text = "За торговое оборудование"
                            doc.Bookmarks("Vid_Plateza1").Range.Text = "За торговое оборудование"
                            doc.Bookmarks("Summa").Range.Text = dTotal
                            doc.Bookmarks("Summa1").Range.Text = dTotal
                            doc.Bookmarks("total").Range.Text = dTotal
                            doc.Bookmarks("total1").Range.Text = dTotal
                        Else
                            doc.Bookmarks("Vid_Plateza").Range.Text = "За ТО и ремонт"
                            doc.Bookmarks("Vid_Plateza1").Range.Text = "За ТО и ремонт"
                            If Not IsDBNull(ds.Tables("customer").Rows(0)("dolg")) Then
                                dTotal = ds.Tables("customer").Rows(0)("dolg")
                            Else
                                dTotal = 0
                            End If
                            doc.Bookmarks("Summa").Range.Text = dTotal
                            doc.Bookmarks("Summa1").Range.Text = dTotal
                            doc.Bookmarks("total").Range.Text = dTotal
                            doc.Bookmarks("total1").Range.Text = dTotal
                        End If

                        doc.Save()

                    Catch
                        WriteError("Извещение <br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
                        ProcessIzveschenieDocuments = False
                        GoTo ExitFunction
                    End Try

                End If


            Next

ExitFunction:
            Try
                ds.Clear()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
                cmd = Nothing
                ds = Nothing
                adapt = Nothing

            Catch
                WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
            End Try

        End Function




        '    Private Function ProcessRebilling(ByVal num_doc() As Integer, ByVal customer As Integer, ByVal cashregisters As String, Optional ByVal isRefresh As Boolean = True) As Boolean
        '        Dim cn As SqlClient.SqlConnection
        '        Dim cmd As SqlClient.SqlCommand
        '        Dim adapt As SqlClient.SqlDataAdapter
        '        Dim ds As DataSet

        '        Dim fls As IO.File
        '        Dim fl As IO.FileInfo

        '        Dim docFullPath$
        '        Dim path$ = Server.MapPath("Docs") & "\" & customer

        '        Dim boos_name$, customer_name$, accountant$, unn$, registration$, saler$, sDate$, dogovor$

        '        ProcessRebilling = True

        '        Try
        '            'Create folders and copy templates
        '            Dim fldr As New IO.DirectoryInfo(path)
        '            If Not fldr.Exists Then
        '                fldr.Create()
        '            End If
        '            path = path & "\Rebilling\"
        '            fldr = New IO.DirectoryInfo(path)
        '            If Not fldr.Exists Then
        '                fldr.Create()
        '            End If

        '        Catch ex As Exception
        '            WriteError(Err.Description & "<BR>" & ex.ToString)
        '            ProcessRebilling = False
        '            Exit Function
        '        End Try
        '        Try
        '            ' get data from database for specified sale

        '            'cn = New SqlClient.SqlConnection("data source=BY-MN-SRV1;initial catalog=Ramok;persist security info=False;user id=WebDB;workstation id=BY-MN-SRV1;packet size=4096;password=webdb;")
        '            cn = New SqlClient.SqlConnection(ConnectionString)
        '            ds = New DataSet

        '            cn.Open()

        '            ''sale
        '            'cmd = New SqlClient.SqlCommand("get_sale_info", cn)
        '            'cmd.CommandType = CommandType.StoredProcedure
        '            'cmd.Parameters.Add("@pi_sale_sys_id", Sale)
        '            'adapt = New SqlClient.SqlDataAdapter(cmd)
        '            'adapt.Fill(ds, "sale")

        '            'If ds.Tables("sale").Rows.Count = 0 Then GoTo ExitFunction

        '            'saler = ds.Tables("sale").Rows(0)("saler")
        '            sDate = GetRussianDate(DateTime.Now)

        '            'customer
        '            cmd = New SqlClient.SqlCommand("get_customer_info", cn)
        '            cmd.CommandType = CommandType.StoredProcedure
        '            cmd.Parameters.Add("@pi_customer_sys_id", customer)
        '            adapt = New SqlClient.SqlDataAdapter(cmd)
        '            adapt.Fill(ds, "customer")

        '            If ds.Tables("customer").Rows.Count = 0 Then GoTo ExitFunction

        '            dogovor = ds.Tables("customer").Rows(0)("dogovor")
        '            boos_name = ds.Tables("customer").Rows(0)("boos_name")
        '            customer_name = ds.Tables("customer").Rows(0)("customer_name")
        '            accountant = ds.Tables("customer").Rows(0)("accountant")
        '            unn = ds.Tables("customer").Rows(0)("unn")
        '            registration = ds.Tables("customer").Rows(0)("registration")

        '            ' get list of goods for specified sale
        '            cmd = New SqlClient.SqlCommand("prc_rpt_Rebilling", cn)
        '            cmd.CommandType = CommandType.StoredProcedure
        '            cmd.Parameters.Add("@pi_Registers", cashregisters)
        '            cmd.Parameters.Add("@pi_owner_id", customer)
        '            adapt = New SqlClient.SqlDataAdapter(cmd)
        '            adapt.Fill(ds, "goods")

        '            If ds.Tables("goods").Rows.Count = 0 Then
        '                WriteError("Не выбранны товары для данного клиента и продажи")
        '                ProcessRebilling = False
        '                GoTo ExitFunction
        '            End If


        '        Catch ex As Exception

        '            WriteError("Загрузка данных<br>" & Err.Description)
        '            ProcessRebilling = False
        '            GoTo ExitFunction

        '        End Try


        '        Try
        '            ' Create instance of Word!
        '            wrdApp = New Word.Application

        '        Catch ex As Exception
        '            WriteError(Err.Description & "<br>" & ex.ToString)
        '            ProcessRebilling = False
        '            GoTo ExitFunction
        '        End Try



        '        Dim tbl1 As Table, tbl2 As Table
        '        Dim r1 As Word.Row, r2 As Word.Row
        '        Dim i%, j%, k%, n%, iLower%, iUpper%
        '        Dim q, p, dNDS, dTotal, sSum, sNDS As Double
        '        Dim iCount%, iGoodType%, sTmp$

        '        For k = num_doc.GetLowerBound(0) To num_doc.GetUpperBound(0)
        '            dNDS = 0
        '            dTotal = 0
        '            If num_doc(k) = 0 Then

        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                'Счет-фактура по НДС
        '                Try
        '                    docFullPath = path & DocName0

        '                    fl = New IO.FileInfo(docFullPath)
        '                    If (Not fl.Exists()) Or isRefresh Then
        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If
        '                        fls.Copy(Server.MapPath("Templates/") & DocName0, docFullPath, True)

        '                        doc = wrdApp.Documents.Open(docFullPath)

        '                        doc.Bookmarks("RECIPIENT_ADDRESS").Range.Text = ds.Tables("customer").Rows(0)("customer_address")
        '                        doc.Bookmarks("RECIPIENT_NAME").Range.Text = customer_name
        '                        doc.Bookmarks("RECIPIENT_UNN").Range.Text = unn
        '                        doc.Bookmarks("Date").Range.Text = sDate

        '                        iGoodType = -1
        '                        j = 1
        '                        For i = 0 To ds.Tables("goods").Rows.Count - 1

        '                            If iGoodType <> ds.Tables("goods").Rows(i)("good_type_sys_id") Then

        '                                If i <> 0 Then
        '                                    r1.Cells(2).Range.Text = q * p
        '                                    r1.Cells(3).Range.Text = 0
        '                                    r1.Cells(4).Range.Text = 18
        '                                    r1.Cells(5).Range.Text = q * p * 0.18
        '                                    r1.Cells(6).Range.Text = q * p * 1.18

        '                                    dTotal = dTotal + q * p

        '                                End If

        '                                iGoodType = ds.Tables("goods").Rows(i)("good_type_sys_id")
        '                                r1 = doc.Tables(3).Rows.Add(doc.Tables(3).Rows(j + 2))
        '                                j = j + 1

        '                                q = 0
        '                                p = CDbl(ds.Tables("goods").Rows(i)("price"))

        '                                r1.Cells(1).Range.Text = ds.Tables("goods").Rows(i)("good_name")

        '                            End If

        '                            q = q + CDbl(ds.Tables("goods").Rows(i)("quantity"))

        '                        Next

        '                        r1.Cells(2).Range.Text = q * p
        '                        r1.Cells(5).Range.Text = q * p * 0.18
        '                        r1.Cells(6).Range.Text = q * p * 1.18

        '                        dTotal = dTotal + q * p

        '                        doc.Bookmarks("TotalNDS").Range.Text = dTotal * 0.18
        '                        doc.Bookmarks("TotalAll").Range.Text = dTotal * 1.18
        '                        doc.Bookmarks("Total").Range.Text = dTotal
        '                        doc.Bookmarks("TotalNDSPropis").Range.Text = Summa_propis(dTotal * 0.18)
        '                        doc.Bookmarks("TotalAllPropis").Range.Text = Summa_propis(dTotal * 1.18)

        '                        doc.Save()
        '                    End If

        '                Catch
        '                    WriteError("Счет-фактура  по НДС<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If

        '            If num_doc(k) = 4 Then
        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                ' Акт о снятии показаний счетчика при постановке на учет

        '                Try

        '                    If ds.Tables("goods").Rows(0)("is_cashregister") Then

        '                        docFullPath = path & DocName4

        '                        ' get cash history 
        '                        cmd = New SqlClient.SqlCommand("get_cashowner_history", cn)
        '                        cmd.CommandType = CommandType.StoredProcedure
        '                        cmd.Parameters.Add("@good_sys_id", ds.Tables("goods").Rows(0)("good_sys_id"))
        '                        adapt = New SqlClient.SqlDataAdapter(cmd)
        '                        adapt.Fill(ds, "cash")

        '                        If ds.Tables("cash").Rows.Count = 0 Then
        '                            WriteError("Не выбраны товары для данного клиента и продажи")
        '                            ProcessRebilling = False
        '                            GoTo ExitFunction
        '                        End If
        '                        fl = New IO.FileInfo(docFullPath)
        '                        If (Not fl.Exists()) Or isRefresh Then
        '                            If fl.Exists() Then
        '                                Try
        '                                    fl.Delete()
        '                                Catch
        '                                End Try
        '                            End If

        '                            fls.Copy(Server.MapPath("Templates/") & DocName4, docFullPath, True)

        '                            doc = wrdApp.Documents.Open(docFullPath)

        '                            doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("goods").Rows(0)("good_name")
        '                            doc.Bookmarks("Version").Range.Text = ds.Tables("goods").Rows(0)("version")
        '                            doc.Bookmarks("boos_name").Range.Text = boos_name
        '                            doc.Bookmarks("CustomerName").Range.Text = customer_name
        '                            'doc.Bookmarks("kassir").Range.Text = ds.Tables("goods").Rows(n)("kassir1") & " " & ds.Tables("goods").Rows(n)("kassir2")
        '                            doc.Bookmarks("Saler").Range.Text = ds.Tables("goods").Rows(0)("worker")
        '                            doc.Bookmarks("Saler2").Range.Text = ds.Tables("goods").Rows(0)("worker")
        '                            doc.Bookmarks("SalerDocument").Range.Text = ds.Tables("goods").Rows(0)("worker_document")
        '                            doc.Bookmarks("TaxInspection").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
        '                            doc.Bookmarks("num_cashregister").Range.Text = ds.Tables("goods").Rows(0)("num_cashregister")
        '                            doc.Bookmarks("Reestr").Range.Text = ds.Tables("goods").Rows(0)("num_control_reestr")
        '                            doc.Bookmarks("PZU").Range.Text = ds.Tables("goods").Rows(0)("num_control_pzu")
        '                            doc.Bookmarks("MFP").Range.Text = ds.Tables("goods").Rows(0)("num_control_mfp")
        '                            doc.Bookmarks("CTO").Range.Text = ds.Tables("goods").Rows(0)("num_control_cto")
        '                            doc.Bookmarks("DateDismissal").Range.Text = GetRussianDate1(ds.Tables("cash").Rows(0)("support_date"))
        '                            doc.Save()
        '                        End If
        '                    End If
        '                Catch
        '                    WriteError("Акт о снятии показаний счетчика<br>" & Err.Description)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If



        '            If num_doc(k) = 6 Then

        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                'Договор на техническое обслуживание
        '                Try
        '                    Dim s$

        '                    docFullPath = path & DocName6

        '                    fl = New IO.FileInfo(docFullPath)
        '                    If (Not fl.Exists()) Or isRefresh Then
        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If

        '                        fls.Copy(Server.MapPath("Templates/") & DocName6, docFullPath, True)

        '                        doc = wrdApp.Documents.Open(docFullPath)

        '                        doc.Bookmarks("CustomerName").Range.Text = customer_name
        '                        doc.Bookmarks("BoosName").Range.Text = boos_name
        '                        doc.Bookmarks("Registration").Range.Text = registration
        '                        If ds.Tables("customer").Rows(0)("NDS") <> "" Then
        '                            doc.Bookmarks("NDS").Range.Text = ""
        '                        End If

        '                        For i = 0 To ds.Tables("goods").Rows.Count - 1

        '                            If ds.Tables("goods").Rows(i)("is_cashregister") Then

        '                                r2 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(i + 2))

        '                                r2.Cells(1).Range.Text = i + 1
        '                                r2.Cells(2).Range.Text = ds.Tables("goods").Rows(i)("good_name")
        '                                r2.Cells(3).Range.Text = ds.Tables("goods").Rows(i)("num_cashregister")
        '                                r2.Cells(4).Range.Text = ds.Tables("goods").Rows(i)("set_place")
        '                                r2.Cells(5).Range.Text = "Переоформлен"

        '                            End If

        '                        Next

        '                        s = customer_name.Trim
        '                        sTmp = ds.Tables("customer").Rows(0)("customer_address")
        '                        If customer_name.Trim.Length > 0 And sTmp.Trim.Length > 0 Then s = s & ", "
        '                        s = s & sTmp.Trim
        '                        If s.Length > 0 Then s = s & ". "
        '                        sTmp = ds.Tables("customer").Rows(0)("bank")
        '                        s = s & sTmp.Trim
        '                        If sTmp.Trim.Length > 0 Then s = s & "."
        '                        sTmp = ""
        '                        If unn.Trim.Length > 0 Then s = s & " УНП "
        '                        s = s & unn.Trim
        '                        sTmp = ds.Tables("customer").Rows(0)("okpo")
        '                        If sTmp.Trim.Length > 0 Then s = s & " ОКЮЛП "
        '                        s = s & sTmp.Trim
        '                        sTmp = ds.Tables("customer").Rows(0)("customer_phone")
        '                        If sTmp.Trim.Length > 0 Then s = s & " Тел/ф "
        '                        s = s & sTmp.Trim
        '                        If sTmp.Trim.Length > 0 Or unn.Trim.Length > 0 Then s = s & "."
        '                        doc.Bookmarks("Customer").Range.Text = s
        '                        doc.Bookmarks("Dogovor").Range.Text = dogovor
        '                        doc.Bookmarks("Date").Range.Text = sDate
        '                        doc.Bookmarks("Date2").Range.Text = sDate & " по " & GetRussianDate(DateTime.Now.AddYears(1))

        '                        doc.Save()
        '                    End If

        '                Catch
        '                    WriteError("Договор на техническое обслуживание<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If

        '            If num_doc(k) = 7 Then

        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                'Список ККМ
        '                Try

        '                    docFullPath = path & DocName7

        '                    fl = New IO.FileInfo(docFullPath)
        '                    If (Not fl.Exists()) Or isRefresh Then

        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If

        '                        fls.Copy(Server.MapPath("Templates/") & DocName7, docFullPath, True)

        '                        doc = wrdApp.Documents.Open(docFullPath)

        '                        For i = 0 To ds.Tables("goods").Rows.Count - 1

        '                            If ds.Tables("goods").Rows(i)("is_cashregister") Then

        '                                r2 = doc.Tables(2).Rows.Add(doc.Tables(2).Rows(i + 1))

        '                                r2.Cells(1).Range.Text = i + 1
        '                                r2.Cells(3).Range.Text = ds.Tables("goods").Rows(i)("good_name") & " " & ds.Tables("goods").Rows(i)("version") & ", №" & ds.Tables("goods").Rows(i)("num_cashregister") & " " & ds.Tables("goods").Rows(i)("year") & " г.в. ФГУП(КЗТА)"
        '                                r2.Cells(4).Range.Text = ds.Tables("goods").Rows(i)("itog") & vbCrLf & ds.Tables("goods").Rows(i)("zreport") '"0" & vbCrLf & "0"
        '                                r2.Cells(5).Range.Text = "0001"
        '                                r2.Cells(6).Range.Text = "№" & dogovor & " от " & sDate & " исправен"
        '                                r2.Cells(7).Range.Text = ds.Tables("goods").Rows(i)("set_place")
        '                                r2.Cells(9).Range.Text = ds.Tables("goods").Rows(i)("num_control_reestr") & " " & ds.Tables("goods").Rows(i)("num_control_pzu") & " " & ds.Tables("goods").Rows(i)("num_control_mfp")
        '                                r2.Cells(10).Range.Text = ds.Tables("goods").Rows(i)("num_control_cto")

        '                            End If

        '                        Next

        '                        doc.Bookmarks("CustomerName").Range.Text = ds.Tables("customer").Rows(0)("tax_inspection")
        '                        doc.Bookmarks("CustomerName2").Range.Text = customer_name
        '                        doc.Bookmarks("UNN").Range.Text = ds.Tables("customer").Rows(0)("unn")
        '                        doc.Tables(2).Rows(1).Cells(2).Range.Text = customer_name & ", " & ds.Tables("customer").Rows(0)("customer_address")
        '                        'doc.Bookmarks("Date").Range.Text = sDate
        '                        If boos_name.Trim.Length > 0 Then
        '                            doc.Bookmarks("Boos").Range.Text = boos_name
        '                        Else
        '                            If customer_name.Trim.Length > 0 Then doc.Bookmarks("Boos").Range.Text = customer_name
        '                        End If

        '                        If accountant.Trim.Length > 0 Then doc.Bookmarks("Accountant").Range.Text = accountant

        '                        doc.Save()
        '                    End If

        '                Catch
        '                    WriteError("Список ККМ<br>" & Err.Description & "<br>" & Err.Erl & "<br>" & Err.LastDllError & "<br>" & Err.Number & "<br>" & Err.Source)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If

        '            If num_doc(k) = 8 Then
        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                ' Техническое заключение

        '                Try

        '                    If ds.Tables("goods").Rows(0)("is_cashregister") Then

        '                        docFullPath = path & DocName8

        '                        fl = New IO.FileInfo(docFullPath)
        '                        If (Not fl.Exists()) Or isRefresh Then

        '                            If fl.Exists() Then
        '                                Try
        '                                    fl.Delete()
        '                                Catch
        '                                End Try
        '                            End If

        '                            fls.Copy(Server.MapPath("Templates/") & DocName8, docFullPath, True)

        '                            doc = wrdApp.Documents.Open(docFullPath)

        '                            doc.Bookmarks("CashregisterName").Range.Text = ds.Tables("goods").Rows(0)("good_name")
        '                            doc.Bookmarks("CustomerName").Range.Text = customer_name
        '                            doc.Bookmarks("CashregisterNumber").Range.Text = ds.Tables("goods").Rows(0)("num_cashregister")
        '                            doc.Bookmarks("Dogovor").Range.Text = dogovor
        '                            doc.Bookmarks("Date").Range.Text = sDate

        '                            doc.Save()
        '                        End If
        '                    End If

        '                Catch
        '                    WriteError("Техническое заключение<br>" & Err.Description)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If

        '            If num_doc(k) = 9 Then
        '                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        '                ' Удостоверение кассира

        '                Try

        '                    Dim sNum$, iNum%, sYear$, sKassir$

        '                    'sTmp = ds.Tables("sale").Rows(0)("dogovor")
        '                    'sTmp = sTmp.Trim.PadLeft(2, "0")
        '                    sNum = ds.Tables("customer").Rows(0)("dogovor")
        '                    sNum = "К" & sNum.PadLeft(4, "0") & "/"
        '                    sYear = "-" & Format(DateTime.Now, "yy")
        '                    iNum = 0

        '                    docFullPath = path & DocName9

        '                    fl = New IO.FileInfo(docFullPath)
        '                    If (Not fl.Exists()) Or isRefresh Then

        '                        If fl.Exists() Then
        '                            Try
        '                                fl.Delete()
        '                            Catch
        '                            End Try
        '                        End If

        '                        fls.Copy(Server.MapPath("Templates/") & DocName9, docFullPath, True)

        '                        doc = wrdApp.Documents.Open(docFullPath)

        '                        sKassir = ds.Tables("goods").Rows(0)("kassir1")
        '                        If sKassir.Length > 0 Then
        '                            iNum = iNum + 1
        '                            doc.Bookmarks("Kassir1").Range.Text = sKassir
        '                            doc.Bookmarks("Number1").Range.Text = sNum & iNum & sYear
        '                            doc.Bookmarks("Type1").Range.Text = ds.Tables("goods").Rows(0)("good_name")
        '                            doc.Bookmarks("Date1").Range.Text = sDate
        '                        Else
        '                            doc.Bookmarks("Kassir1").Range.Text = "____________________________"
        '                            doc.Bookmarks("Number1").Range.Text = "________"
        '                            doc.Bookmarks("Type1").Range.Text = "_______________"
        '                            doc.Bookmarks("Date1").Range.Text = """___""_______________"
        '                        End If

        '                        sKassir = ds.Tables("goods").Rows(0)("kassir2")
        '                        If sKassir.Length > 0 Then
        '                            iNum = iNum + 1
        '                            doc.Bookmarks("Kassir2").Range.Text = sKassir
        '                            doc.Bookmarks("Number2").Range.Text = sNum & iNum & sYear
        '                            doc.Bookmarks("Type2").Range.Text = ds.Tables("goods").Rows(0)("good_name")
        '                            doc.Bookmarks("Date2").Range.Text = sDate
        '                        Else
        '                            doc.Bookmarks("Kassir2").Range.Text = "____________________________"
        '                            doc.Bookmarks("Number2").Range.Text = "________"
        '                            doc.Bookmarks("Type2").Range.Text = "_______________"
        '                            doc.Bookmarks("Date2").Range.Text = """___""_______________"
        '                        End If

        '                        doc.Save()
        '                    End If
        '                Catch
        '                    WriteError("Удостоверение кассира<br>" & Err.Description)
        '                    ProcessRebilling = False
        '                    GoTo ExitFunction
        '                End Try

        '            End If
        '        Next

        'ExitFunction:
        '        Try
        '            If cn.State = ConnectionState.Open Then cn.Close()

        '            ds.Clear()

        '            If Not doc Is Nothing Then
        '                doc.Close(True)
        '            End If

        '            If Not wrdApp Is Nothing Then
        '                wrdApp.Quit(False)
        '            End If

        '            wrdApp = Nothing
        '            doc = Nothing
        '            cmd = Nothing
        '            ds = Nothing
        '            adapt = Nothing

        '        Catch
        '            WriteError("Аварийное завершение работы Microsoft Word<br>" & Err.Description)
        '        End Try

        '    End Function

    End Class

End Namespace
