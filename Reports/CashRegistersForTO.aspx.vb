Imports System.Globalization
Imports System.Threading
Imports Microsoft.Office.Interop.Excel

Namespace Kasbi.Reports
    Partial Class CashRegistersForTO
        Inherits PageBase
        Dim CurrentCustomer%
        Dim WithEvents oExcel As Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet
        Const ClearString = "---------------"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            'Common.SetUpLocalization()
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")

            If Not IsPostBack Then
                Try
                    'tbxBeginDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    'tbxEndDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                ShowContent()
            End If
        End Sub

        Private Sub ImgSearchClient_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearchClient.Click
            Dim str$ = tbSearchClient.Text.Replace("'", "")
            If Trim(str).Length = 0 Then
                LoadClientList()
                Exit Sub
            End If
            LoadClientList(str)
        End Sub

        Private Sub ImgSearchDealer_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearchDealers.Click
            Dim str$ = tbSearchDealers.Text.Replace("'", "")
            If Trim(str).Length = 0 Then
                LoadDealerList()
                Exit Sub
            End If
            LoadDealerList(str)
        End Sub

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
            Dim docPath, strExpr, strSort, filter, filterCustomers, filterCashRegisters As String
            filterCustomers = ""
            filterCashRegisters = ""
            Dim file As IO.FileInfo
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim dt As Data.DataTable
            Dim drs() As Data.DataRow
            Dim endInsertIndex As Integer


            If lstClient.GetSelectedIndices().Length <> 0 Or lstDealers.GetSelectedIndices().Length <> 0 Then
                filterCustomers &= " and customer.customer_sys_id in ("
                For Each item As ListItem In lstClient.Items
                    If item.Value <> ClearString Then
                        If item.Selected Then
                            filterCustomers &= item.Value & ","
                        End If
                    End If
                Next item
                For Each item As ListItem In lstDealers.Items
                    If item.Value <> ClearString Then
                        If item.Selected Then
                            filterCustomers &= item.Value & ","
                        End If
                    End If
                Next item
                filterCustomers = Left(filterCustomers, filterCustomers.Length - 1)
                filterCustomers &= ")"
            End If
            Dim str As String = lstCashRegister.SelectedItem.Attributes.Item(ClearString)

            If lstCashRegister.GetSelectedIndices().Length <> 0 And lstCashRegister.SelectedItem.Attributes.Item(ClearString) Then
                filterCashRegisters &= " and good.good_type_sys_id in ("
                For Each item As ListItem In lstCashRegister.Items
                    If item.Value <> ClearString Then
                        If item.Selected Then
                            filterCashRegisters &= item.Value & ","
                        End If
                    End If
                Next item
                filterCashRegisters = Left(filterCashRegisters, filterCashRegisters.Length - 1)
                filterCashRegisters &= ")"
            End If


            oExcel = New ApplicationClass()
            oExcel.DisplayAlerts = False
            oBook = oExcel.Workbooks.Add
            oSheet = oBook.Worksheets(1)
            oSheet.Columns("A").ColumnWidth = 5
            oSheet.Columns("B").ColumnWidth = 50
            oSheet.Columns("C").ColumnWidth = 50
            oSheet.Columns("D").ColumnWidth = 20
            oSheet.Columns("E").ColumnWidth = 20
            oSheet.Columns("F").ColumnWidth = 100

            cmd = New SqlClient.SqlCommand("get_cashregister_for_to")
            cmd.CommandType = CommandType.StoredProcedure
            filter = " where good.num_control_cto like 'МН%' " & filterCustomers & filterCashRegisters
            cmd.Parameters.AddWithValue("@pi_filter", filter)

            cmd.CommandTimeout = 0

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)
            dt = ds.Tables(0)
            strExpr = "payer_sys_id IS NOT NULL and buyer_sys_id IS NOT NULL and cto=0"
            strSort = "buyer_sys_id, payer_sys_id"
            drs = dt.Select(strExpr, strSort)

            endInsertIndex = InsertExelData(drs, 0)

            strExpr = "payer_sys_id IS NOT NULL and buyer_sys_id IS NOT NULL and cto=1"
            drs = dt.Select(strExpr, strSort)

            InsertExelData(drs, endInsertIndex + 1)

            docPath = Server.MapPath("Docs") & "\test.xlsx"
            oBook.SaveAs(docPath)
            oExcel.Quit()
            Thread.Sleep(1000)

            file = New System.IO.FileInfo(docPath)
            If file.Exists Then 'set appropriate headers
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(docPath)
                Response.End() 'if file does not exist
            Else
                Response.Write("This file does not exist.")
            End If
        End Sub

        Private Function InsertExelData(ByVal drs() As Data.DataRow, ByVal startInsertIndex As Integer) As Integer
            Dim payer_sys_id, payer_sys_id_old, payer_counter, cr_counter, crs_counter, str_i, str_i_start, i As Integer

            payer_sys_id = -1
            payer_sys_id_old = -2
            payer_counter = 0
            cr_counter = 0
            crs_counter = 0
            str_i_start = startInsertIndex
            str_i = str_i_start

            For i = 0 To drs.Length - 1
                payer_sys_id_old = payer_sys_id
                payer_sys_id = drs(i).Item(19)

                If payer_sys_id = payer_sys_id_old Then
                    cr_counter += 1
                    oSheet.Range("B" & i + str_i + 2).Value() = cr_counter
                    oSheet.Range("C" & i + str_i + 2).Value() = drs(i).Item(13)
                    oSheet.Range("D" & i + str_i + 2).Value() = "№" + drs(i).Item(2)
                    oSheet.Range("E" & i + str_i + 2).Value() = drs(i).Item(12)
                    oSheet.Range("F" & i + str_i + 2).Value() = drs(i).Item(9)

                Else
                    payer_counter += 1
                    oSheet.Range("A" & i + str_i + 2, "F" & i + str_i + 2).Interior.Color = RGB(214, 220, 228)
                    oSheet.Range("A" & i + str_i + 2).Value() = payer_counter
                    oSheet.Range("B" & i + str_i + 2).Value() = drs(i).Item(22)
                    oSheet.Range("C" & i + str_i + 2).Value() = drs(i).Item(23)
                    oSheet.Range("D" & i + str_i + 1 - cr_counter).Value() = cr_counter
                    If i <> 0 Then
                        oSheet.Range(i + str_i + 1 & ":" & i + str_i + 2 - cr_counter).Rows.Group()
                    End If
                    crs_counter += cr_counter
                    cr_counter = 0


                    cr_counter += 1
                    str_i += 1
                    oSheet.Range("B" & i + str_i + 2).Value() = cr_counter
                    oSheet.Range("C" & i + str_i + 2).Value() = drs(i).Item(13)
                    oSheet.Range("D" & i + str_i + 2).Value() = "№" + drs(i).Item(2)
                    oSheet.Range("E" & i + str_i + 2).Value() = drs(i).Item(12)
                    oSheet.Range("F" & i + str_i + 2).Value() = drs(i).Item(9)

                End If
            Next

            crs_counter += cr_counter
            oSheet.Range("D" & i + str_i + 1 - cr_counter).Value() = cr_counter
            oSheet.Range("D" & i + str_i + 2).Value() = "Итого: " & crs_counter
            oSheet.Range("A" & i + str_i + 2 & ":F" & i + str_i + 2).Font.Bold = True
            oSheet.Range("A" & i + str_i + 2 & ":F" & i + str_i + 2).Font.Size = 14
            oSheet.Range("A" & i + str_i + 2 & ":F" & i + str_i + 2).HorizontalAlignment = XlHAlign.xlHAlignRight
            oSheet.Range("A" & i + str_i + 2 & ":F" & i + str_i + 2).Interior.Color = RGB(172, 185, 202)
            oSheet.Range(i + str_i + 1 & ":" & i + str_i + 2 - cr_counter).Rows.Group()



            oSheet.Rows(str_i_start + 1).HorizontalAlignment = XlHAlign.xlHAlignCenter
            oSheet.Rows(str_i_start + 1).Font.Bold = True
            oSheet.Rows(str_i_start + 1).Font.Size = 16
            oSheet.Range("A" & str_i_start + 1, "F" & str_i_start + 1).Interior.Color = RGB(172, 185, 202)
            oSheet.Range("A" & str_i_start + 1).Value = "№"
            oSheet.Range("B" & str_i_start + 1).Value = "Покупатель"
            oSheet.Range("C" & str_i_start + 1).Value = "Плательщик"
            oSheet.Range("D" & str_i_start + 1).Value = "Кол-во ККМ"
            oSheet.Outline.SummaryRow = XlSummaryRow.xlSummaryAbove
            Return i + str_i + 2
        End Function

        Private Sub ShowContent()
            LoadCashRegisterList()
            LoadDealerList()
            LoadClientList()
        End Sub

        Private Function GetCustomerDataView(Optional ByVal sRequest$ = "") As DataView
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim dv As DataView = New DataView()
            Try
                cmd = New SqlClient.SqlCommand("get_customer_for_support")
                cmd.Parameters.AddWithValue("@pi_filter", sRequest$)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                dv = ds.Tables(0).DefaultView
            Catch
                lblError.Text = "Ошибка загрузки информации customer!<br>" & Err.Description
            End Try
            Return dv
        End Function

        Private Sub LoadCashRegisterList()
            Dim sql$ = ""

            sql = "select * from good_type where is_cashregister='1' order by name"

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstCashRegister.DataSource = ds.Tables(0).DefaultView
                lstCashRegister.DataTextField = "name"
                lstCashRegister.DataValueField = "good_type_sys_id"
                lstCashRegister.DataBind()
                lstCashRegister.Items.Insert(0, New ListItem(ClearString, ClearString))
            Catch
                lblError.Text = "Ошибка загрузки информации о кассовых аппаратах!<br>" & Err.Description
            End Try
            lstCashRegister.Enabled = True
        End Sub

        Private Sub LoadClientList(Optional ByVal clientName$ = "")
            Dim s As String = " cto=0 " & "and customer_name like '%" & clientName & "%'"
            lstClient.Items.Clear()
            lstClient.DataSource = GetCustomerDataView(s)
            lstClient.DataTextField = "customer_name"
            lstClient.DataValueField = "customer_sys_id"
            lstClient.DataBind()
            lstClient.Items.Insert(0, New ListItem(ClearString, ClearString))
            lstClient.Enabled = True
        End Sub

        Private Sub LoadDealerList(Optional ByVal dealerName$ = "")
            Dim s As String = " cto=1 " & "and customer_name like '%" & dealerName & "%'"
            lstDealers.Items.Clear()
            lstDealers.DataSource = GetCustomerDataView(s)
            lstDealers.DataTextField = "customer_name"
            lstDealers.DataValueField = "customer_sys_id"
            lstDealers.DataBind()
            lstDealers.Items.Insert(0, New ListItem(ClearString, ClearString))
            lstDealers.Enabled = True
        End Sub

    End Class
End Namespace
