Imports Microsoft.Office.Interop
Imports System.Collections


Namespace Kasbi
    Partial Class Sale
        Inherits System.Web.UI.UserControl
        Dim dTotal As Double
        Public iSale As Integer
        Public iCustomer As Integer
        Public iSaleCount As Integer = 0

        Const DocName0 = "InvoiceNDS.doc"
        Const DocName1 = "Dogovor.doc"
        Const DocName2 = "Zayavlenie_Na_Knigu_Kassira.doc"
        Const DocName3 = "Zayavlenie.doc"
        Const DocName4 = "Akt_Pokazaniy.doc"
        Const DocName5 = "TTN.doc"
        Const DocName6 = "Dogovor_Na_TO.doc"
        Const DocName7 = "Spisok_KKM.doc"
        Const DocName8 = "Teh_Zaklyuchenie.doc"
        Const DocName9 = "Udostoverenie_Kassira.doc"
        Const DocName17 = "Garantia.doc"
        Const DocName18 = "Zayavlenie_IMNS.doc"
        Const DocName41 = "Izveschenie.xls"

        Private wrdApp As Word.ApplicationClass
        Protected WithEvents radiobuttons As System.Web.UI.WebControls.Label
        Protected WithEvents lblTotalCount As System.Web.UI.WebControls.Label
        Private doc As Word.DocumentClass

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim i%
        Dim iCashCount As New SortedList
        Private currentPage As PageBase

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currentPage = Page
            'Право на удаление заказа
            'If currentPage.CurrentUser.is_admin = True Then
            'CType(FindControl("btnDelete"), ImageButton).Visible = True
            'Else
            'CType(FindControl("btnDelete"), ImageButton).Visible = False
            'End If
            Bind()


            Dim saletype
            'теперь когда удаляем, смотрим: если продажа еще не оплачена, то удаляем сумму заказа из долга
            Dim reader As SqlClient.SqlDataReader
            Dim query = "SELECT type FROM sale WHERE (sale_sys_id = '" & iSale & "')"
            reader = currentPage.dbSQL.GetReader(query)
            If reader.Read() Then
                saletype = reader.Item(0)
            End If
            reader.Close()

            If (Session("rule9") = "1" And saletype <> 0) Or (Session("rule27") = "1" And saletype = 0) Then

            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_goods_by_sale")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                adapt = currentPage.dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                grdSale.DataSource = ds.Tables(0).DefaultView
                grdSale.DataKeyField = "good_sys_id"
                dTotal = 0
                i = 0
                iCashCount.Clear()
                grdSale.DataBind()
            Catch
                msgSale.Text = Err.Description
            End Try
        End Sub

        Private Sub grd_ItemDataBound(ByVal sender As System.Object,
                                      ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdSale.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim d As Double

                Dim query As String
                Try
                    doc_num.Text = e.Item.DataItem("num_document").ToString()
                Catch
                End Try

                'формируем ссылку для добавления товара в базу
                add_new_goods.PostBackUrl = "NewGoodTO.aspx?c=" & iCustomer & "&s=" & iSale

                If Not (IsDBNull(e.Item.DataItem("price")) Or IsDBNull(e.Item.DataItem("quantity"))) Then
                    d = CDbl(e.Item.DataItem("price"))*CDbl(e.Item.DataItem("quantity"))
                    CType(e.Item.FindControl("lblCost"), Label).Text = CStr(d)
                    dTotal = dTotal + d
                End If

                If e.Item.DataItem("is_cashregister") Then
                    If iCashCount.Item(e.Item.DataItem("good_name")) Is Nothing Then
                        iCashCount.Add(e.Item.DataItem("good_name"), "1")
                    Else
                        Dim iC As Int32 = CInt(iCashCount.Item(e.Item.DataItem("good_name")))
                        iC = iC + 1
                        iCashCount.Item(e.Item.DataItem("good_name")) = CStr(iC)
                    End If

                    CType(e.Item.FindControl("lnkAkt_Pokazaniy"), HyperLink).NavigateUrl =
                        currentPage.GetAbsoluteUrl(
                            "~/documents.aspx?c=" & iCustomer & "&s=" & iSale & "&t=4&n=" & e.Item.ItemIndex)
                    CType(e.Item.FindControl("lnkTeh_Zaklyuchenie"), HyperLink).NavigateUrl =
                        currentPage.GetAbsoluteUrl(
                            "~/documents.aspx?c=" & iCustomer & "&s=" & iSale & "&t=8&n=" & e.Item.ItemIndex)
                    CType(e.Item.FindControl("lnkUdostoverenie_Kassira"), HyperLink).NavigateUrl =
                        currentPage.GetAbsoluteUrl(
                            "~/documents.aspx?c=" & iCustomer & "&s=" & iSale & "&t=9&n=" & e.Item.ItemIndex)
                Else
                    CType(e.Item.FindControl("lnkAkt_Pokazaniy"), HyperLink).Visible = False
                    CType(e.Item.FindControl("lnkTeh_Zaklyuchenie"), HyperLink).Visible = False
                    CType(e.Item.FindControl("lnkUdostoverenie_Kassira"), HyperLink).Visible = False
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i
                ' Подтверждение удаления записи
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick",
                                                                                   "if (confirm('Вы действительно хотите удалить товар ?')){return confirm('Отменить удаление невозможно!!! Продолжить удаление?');}else {return false};")
                If currentPage.CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim d As Double

                If Not (IsDBNull(e.Item.DataItem("price")) Or IsDBNull(e.Item.DataItem("quantity"))) Then
                    d = CDbl(e.Item.DataItem("price"))*CDbl(e.Item.DataItem("quantity"))
                    CType(e.Item.FindControl("lblCostEdit"), Label).Text = CStr(d)
                    dTotal = dTotal + d
                End If
                If e.Item.DataItem("is_cashregister") Then
                    If iCashCount.Item(e.Item.DataItem("good_name")) Is Nothing Then
                        iCashCount.Add(e.Item.DataItem("good_name"), "1")
                    Else
                        Dim iC As Int32 = CInt(iCashCount.Item(e.Item.DataItem("good_name")))
                        iC = iC + 1
                        iCashCount.Item(e.Item.DataItem("good_name")) = CStr(iC)
                    End If
                End If
                i = i + 1
                CType(e.Item.FindControl("lblNumGoodEdit"), Label).Text = i

            ElseIf e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblTotal"), Label).Text = CStr(dTotal)
                dTotal = 0
                Dim s$ = ""
                For i = 0 To iCashCount.Count - 1
                    s = s & iCashCount.GetKey(i) & ":&nbsp;&nbsp;" & iCashCount.GetByIndex(i) & "<br>"
                Next
                CType(e.Item.FindControl("lblTotalCountByCash"), Label).Text = s
            End If
        End Sub

        Private Sub btnPrint1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint1.Click
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Dim sErr$, sFldr$
            Dim doc_num() As Integer = New Integer(7) {6, 1, 7, 4, 8, 9, 17, 18}
            Dim docs As New Kasbi.Migrated_Documents
            Dim i% = 0

            Try
                sFldr = Server.MapPath("Docs/") & iCustomer & "\" & iSale & "\"

                docs.ProcessDocuments(doc_num, iCustomer, iSale)
                docs = Nothing
                ' Create instance of Word!
                sErr = "Проблема с запуском Microsoft Word"
                wrdApp = New Word.Application

                'Тех заключение
                PrintDocument(sFldr & DocName6, 3, True)
                'Договор/счет-факутра
                PrintDocument(sFldr & DocName1)
                'Список ККМ
                PrintDocument(sFldr & DocName7)
                'Гарантийный талон
                PrintDocument(sFldr & DocName17)
                'Заявление в ИМНС
                PrintDocument(sFldr & DocName18)
                'Извещение
                PrintDocument(sFldr & DocName41)
                ' get list of goods for specified sale
                cmd = New SqlClient.SqlCommand("get_goods_info_by_sale")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                reader = currentPage.dbSQL.GetReader(cmd)
                While reader.Read
                    'Акт списания показаний счетчика
                    PrintDocument(sFldr & i & DocName4)
                    'Тех заключение
                    PrintDocument(sFldr & i & DocName8)
                    'Удостоверение кассира
                    PrintDocument(sFldr & i & DocName9)
                    i = i + 1
                End While
            Catch
                msgSale.Text = "Ошибка печати<br>" & Err.Description
            Finally
                reader.Close()
                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
            End Try
        End Sub

        Private Sub btnPrint2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint2.Click
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Dim sErr$, sFldr$
            Dim doc_num() As Integer = New Integer(5) {6, 1, 7, 4, 17, 18}
            Dim docs As New Kasbi.Migrated_Documents
            Dim i% = 0
            Dim s$ = ""
            Try
                sFldr = Server.MapPath("Docs/") & iCustomer & "\" & iSale & "\"

                docs.ProcessDocuments(doc_num, iCustomer, iSale)
                docs = Nothing
                ' Create instance of Word!
                sErr = "Проблема с запуском Microsoft Word"
                wrdApp = New Word.Application
                wrdApp.ActivePrinter = "\\Buhgalter\samsung ml"
                'Тех заключение
                s = PrintDocument(sFldr & DocName6, 1, True)
                If s.Length > 0 Then GoTo ExitSub
                'Договор/счет-факутра
                PrintDocument(sFldr & DocName1)
                'Список(ККМ)
                PrintDocument(sFldr & DocName7)
                'Гарантийный талон
                PrintDocument(sFldr & DocName17)
                'Заявление в ИМНС
                PrintDocument(sFldr & DocName18)
                'Извещение
                PrintDocument(sFldr & DocName41)
                'get list of goods for specified sale
                cmd = New SqlClient.SqlCommand("get_goods_info_by_sale")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                reader = currentPage.dbSQL.GetReader(cmd)
                While reader.Read
                    'Акт списания показаний счетчика
                    PrintDocument(sFldr & i & DocName4)
                    i = i + 1
                End While
                ExitSub:
            Catch
                msgSale.Text = "Ошибка печати<br>" & Err.Description
            Finally
                reader.Close()

                If Not doc Is Nothing Then
                    doc.Close(True)
                End If
                If Not wrdApp Is Nothing Then
                    wrdApp.Quit(False)
                End If
                wrdApp = Nothing
                doc = Nothing
            End Try
        End Sub

        Private Function PrintDocument(ByVal sPath As String, Optional ByVal CopyCount As Integer = 1,
                                       Optional ByVal isFirstPage As Boolean = False) As String
            Try
                PrintDocument = ""
                Dim fls As New IO.FileInfo(sPath)
                If fls.Exists Then
                    doc = wrdApp.Documents.Open(sPath)
                    If isFirstPage Then
                        doc.PrintOut(, , 4, , , , , CopyCount, "1")
                    Else
                        doc.PrintOut(, , , , , , , CopyCount)
                    End If
                    doc.Close()
                End If
            Catch
                PrintDocument = Err.Description
            End Try
        End Function

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnDelete.Click
            'Ограничение прав на удаление
            If Session("rule9") = "1" Or Session("rule27") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet
                Try

                    Dim saletype
                    'теперь когда удаляем, смотрим: если продажа еще не оплачена, то удаляем сумму заказа из долга
                    Dim reader As SqlClient.SqlDataReader
                    Dim query = "SELECT type FROM sale WHERE (sale_sys_id = '" & iSale & "')"
                    reader = currentPage.dbSQL.GetReader(query)
                    If reader.Read() Then
                        saletype = reader.Item(0)
                    End If
                    reader.Close()

                    'удаляем запись если есть права
                    If (Session("rule9") = "1" And saletype <> 0) Or (Session("rule27") = "1" And saletype = 0) Then

                        'умеьшаем долг
                        If saletype = 0 Then
                            query =
                                "UPDATE  customer SET  dolg = dolg - (SELECT SUM(good.price) AS Expr1  FROM  sale INNER JOIN good ON sale.sale_sys_id = good.sale_sys_id  WHERE (sale.sale_sys_id = '" &
                                iSale & "')) * 1.2  WHERE (customer_sys_id = '" & iCustomer & "')"
                            adapt = currentPage.dbSQL.GetDataAdapter(query)
                            ds = New DataSet
                            adapt.Fill(ds)
                        End If

                        cmd = New SqlClient.SqlCommand("remove_sale")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                        cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                        currentPage.dbSQL.Execute(cmd)
                    End If

                    Session("CurrentPage") = "CustomerSales"
                    If saletype = 0 Then
                        Session("insalesneopl") = 0
                        Response.Redirect("SalesNeopl.aspx")
                    Else
                        Response.Redirect("CustomerSales.aspx?" & iCustomer)
                    End If
                Catch
                    If Err.Number = 1 Then
                        msgSale.Text = "Выбранную запись нельзя удалить!"
                    Else
                        msgSale.Text = "Ошибка удаления записи!<br>" & Err.Description
                    End If
                End Try
            End If
        End Sub

        Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnConfirm.Click
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                cmd = New SqlClient.SqlCommand("confirm_sale")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                Dim i%
                If optBeznal.Checked Then
                    i = 1
                ElseIf optNal.Checked Then
                    i = 2
                ElseIf optSberkassa.Checked Then
                    i = 3
                Else
                    i = 1
                End If
                cmd.Parameters.AddWithValue("@pi_type", i)
                currentPage.dbSQL.Execute(cmd)


                Dim query =
                        "UPDATE  customer SET  dolg = dolg - (SELECT SUM(good.price) AS Expr1  FROM  sale INNER JOIN good ON sale.sale_sys_id = good.sale_sys_id  WHERE (sale.sale_sys_id = '" &
                        iSale & "')) * 1.2  WHERE (customer_sys_id = '" & iCustomer & "')"
                adapt = currentPage.dbSQL.GetDataAdapter(query)
                ds = New DataSet
                adapt.Fill(ds)


            Catch
                msgSale.Text = "Ошибка подтверждения продажи!<br>" & Err.Description
            End Try
            Session("CurrentPage") = "CustomerSales"
            If Session("insalesneopl") = 1 Then
                Session("insalesneopl") = 0
                Response.Redirect("SalesNeopl.aspx")
            Else
                Response.Redirect("CustomerSales.aspx?" & iCustomer)
            End If
        End Sub

        Private Sub btnCreateDocuments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnCreateDocuments.Click
            Dim doc_num() As Integer = New Integer(9) {5, 6, 1, 7, 4, 8, 9, 17, 18, 35}
            Dim docs As New Kasbi.Migrated_Documents
            docs.ProcessDocuments(doc_num, iCustomer, iSale)
            docs = Nothing
        End Sub

        Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnEdit.Click
            'Ограничение прав на редактирование
            If Session("rule9") = "1" Then
                Session("AddGoodsForSale") = iSale
                Session("AddSaleForCustomer") = iCustomer
                Session("CurrentPage") = "NewRequest"
                Response.Redirect(currentPage.GetAbsoluteUrl("~NewRequest.aspx?" & iCustomer))
            End If
        End Sub


        'Private Sub grdSale_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSale.UpdateCommand
        '    Dim cmd As SqlClient.SqlCommand
        '    Dim quantity As Double
        '    Try
        '        quantity = Double.Parse(CType(FindControl("txtQuantityEdit"), TextBox).Text)
        '    Catch ex As Exception
        '        msgSale.Text = "Проверьте корректность значения введенного количества"
        '        Exit Sub
        '    End Try

        '    Try
        '        cmd = New SqlClient.SqlCommand("prc_updateGoodFromSale")
        '        cmd.CommandType = CommandType.StoredProcedure
        '        cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
        '        cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
        '        cmd.Parameters.AddWithValue("@pi_good_sys_id", grdSale.DataKeys(e.Item.ItemIndex))
        '        cmd.Parameters.AddWithValue("@pi_quantity", quantity)
        '        currentPage.dbSQL.Execute(cmd)
        '    Catch
        '        msgSale.Text = "Ошибка сохранения записи!<br>" & Err.Description
        '    End Try
        '    grdSale.EditItemIndex = -1
        '    Bind()
        'End Sub

        Private Sub grdSale_DeleteCommand(ByVal source As Object,
                                          ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdSale.DeleteCommand
            'Ограничение прав на удаление
            If Session("rule9") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                Try
                    cmd = New SqlClient.SqlCommand("prc_removeGoodFromSale")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                    cmd.Parameters.AddWithValue("@pi_good_sys_id", grdSale.DataKeys(e.Item.ItemIndex))
                    currentPage.dbSQL.Execute(cmd)
                Catch
                    If Err.Number = 1 Then
                        msgSale.Text = "Выбранную запись нельзя удалить!"
                    Else
                        msgSale.Text = "Ошибка удаления записи!<br>" & Err.Description
                    End If
                End Try
                grdSale.EditItemIndex = - 1
                Bind()
            End If
        End Sub

        Protected Sub btnTTN_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTTN.Click
            Dim cmd As SqlClient.SqlCommand

            If _
                MsgBox("Вы точно хотите сформировать накладную?", MsgBoxStyle.OkCancel, "Формирование накладной") =
                MsgBoxResult.Ok Then
                Dim inp
                inp = InputBox("Введите номер накладной", "Формирование накладной")
                If inp.ToString.Length > 3 Then
                    MsgBox("Накладная сформирована", MsgBoxStyle.Information, "Формирование накладной")

                    cmd = New SqlClient.SqlCommand("doc_insert")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@date", Date.Today)
                    cmd.Parameters.AddWithValue("@sale", btnTTN.TabIndex)
                    cmd.Parameters.AddWithValue("@num", inp.ToString)
                    cmd.Parameters.AddWithValue("@doctype", 1)

                    currentPage.dbSQL.Execute(cmd)

                    Response.Redirect(currentPage.GetAbsoluteUrl(lnkTTN.NavigateUrl))
                Else
                    MsgBox("Неверно введен номер накладной. Накладная не сформирована", MsgBoxStyle.Critical,
                           "Формирование накладной")
                End If
            End If
        End Sub

        Protected Sub btnInvoiceNDS_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles btnInvoiceNDS.Click
            Dim cmd As SqlClient.SqlCommand

            If _
                MsgBox("Вы точно хотите сформировать счет-фактуру?", MsgBoxStyle.OkCancel, "Формирование счет-фактуры") =
                MsgBoxResult.Ok Then
                Dim inp
                inp = InputBox("Введите номер счет-фактуры по НДС", "Формирование счет-фактуры")
                If inp.ToString.Length > 3 Then
                    MsgBox("Счет-фактура сформирована", MsgBoxStyle.Information, "Формирование счет-фактуры")

                    cmd = New SqlClient.SqlCommand("doc_insert")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@date", Date.Today)
                    cmd.Parameters.AddWithValue("@sale", btnInvoiceNDS.TabIndex)
                    cmd.Parameters.AddWithValue("@num", inp.ToString)
                    cmd.Parameters.AddWithValue("@doctype", 2)

                    currentPage.dbSQL.Execute(cmd)

                    Response.Redirect(currentPage.GetAbsoluteUrl(lnkInvoiceNDS.NavigateUrl))
                Else
                    MsgBox("Неверно введен номер счета. Счет-фактура не сформирована", MsgBoxStyle.Critical,
                           "Формирование счет-фактуры")
                End If
            End If
        End Sub

        Protected Sub btnTTNT_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTTNT.Click
            Dim cmd As SqlClient.SqlCommand

            If _
                MsgBox("Вы точно хотите сформировать ТТН?", MsgBoxStyle.OkCancel, "Формирование транспортной накладной") =
                MsgBoxResult.Ok Then
                Dim inp
                inp = InputBox("Введите номер ТТН", "Формирование транспортной накладной")
                If inp.ToString.Length > 3 Then
                    MsgBox("ТТН сформирована", MsgBoxStyle.Information, "Формирование транспортной накладной")

                    cmd = New SqlClient.SqlCommand("doc_insert")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@date", Now)
                    cmd.Parameters.AddWithValue("@sale", btnTTNT.TabIndex)
                    cmd.Parameters.AddWithValue("@num", inp.ToString)
                    cmd.Parameters.AddWithValue("@doctype", 3)

                    currentPage.dbSQL.Execute(cmd)

                    Response.Redirect(currentPage.GetAbsoluteUrl(lnkTTN_Transport.NavigateUrl))
                Else
                    MsgBox("Неверно введен номер накладной. ТТН не сформирована", MsgBoxStyle.Critical,
                           "Формирование транспортной накладной")
                End If
            End If
        End Sub

        Protected Sub btndocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btndocs.Click
            Dim cmd As SqlClient.SqlCommand
            If type_doc.Text = 1 Then

                Dim reader As SqlClient.SqlDataReader
                Dim query = "UPDATE sale SET id1c='" & doc_num.Text & "' WHERE sale_sys_id = '" & iSale & "'"
                reader = currentPage.dbSQL.GetReader(query)
                reader.Close()

                cmd = New SqlClient.SqlCommand("doc_insert")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Now)
                cmd.Parameters.AddWithValue("@sale", btnTTN.TabIndex)
                cmd.Parameters.AddWithValue("@num", doc_num.Text)
                cmd.Parameters.AddWithValue("@doctype", 1)

                currentPage.dbSQL.Execute(cmd)

                Response.Redirect(currentPage.GetAbsoluteUrl(lnkTTN.NavigateUrl))
            ElseIf type_doc.Text = 2 Then
                cmd = New SqlClient.SqlCommand("doc_insert")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Now)
                cmd.Parameters.AddWithValue("@sale", btnInvoiceNDS.TabIndex)
                cmd.Parameters.AddWithValue("@num", doc_num.Text)
                cmd.Parameters.AddWithValue("@doctype", 2)

                currentPage.dbSQL.Execute(cmd)

                Response.Redirect(currentPage.GetAbsoluteUrl(lnkInvoiceNDS.NavigateUrl))
            ElseIf type_doc.Text = 3 Then
                cmd = New SqlClient.SqlCommand("doc_insert")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Now)
                cmd.Parameters.AddWithValue("@sale", btnTTNT.TabIndex)
                cmd.Parameters.AddWithValue("@num", doc_num.Text)
                cmd.Parameters.AddWithValue("@doctype", 3)

                currentPage.dbSQL.Execute(cmd)

                Response.Redirect(currentPage.GetAbsoluteUrl(lnkTTN_Transport.NavigateUrl))
            ElseIf type_doc.Text = 4 Then


                Response.Redirect(currentPage.GetAbsoluteUrl(lnkTTN_Transport.NavigateUrl))


            End If
        End Sub
    End Class
End Namespace

