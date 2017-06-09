Namespace Kasbi.Reports

    Partial Class SellingReport
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents DataGrid1 As System.Web.UI.WebControls.DataGrid
        Protected WithEvents pnlTotal As System.Web.UI.WebControls.Panel


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim rt%, cs%
        Dim customers$
        Dim date_start As DateTime
        Dim date_end As DateTime
        Dim WithEvents repSales As Repeater
        Dim WithEvents grdGoods As DataGrid
        Dim dTotal%, iCount1%, iCount2%, i%, iCustomer%, iSale%
        Dim TotalSum, TotalClientSum As Double
        Dim iCashCount As New SortedList

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            rt = GetPageParam("rt")
            Try
                date_start = CDate(Request.QueryString("db"))
                date_end = CDate(Request.QueryString("de"))
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try


            cs = GetPageParam("cs")
            If cs = 4 Then
                customers = CStr(Request.QueryString("cust"))
            End If
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("get_customers_with_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_type", cs)
                If cs = 4 Then
                    cmd.Parameters.AddWithValue("@pi_customers", customers)
                End If
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                repCustomers.DataSource = ds
                repCustomers.DataBind()
                If ds.Tables(0).Rows.Count = 0 Then
                    lblTotalSum.Text = ""
                Else
                    lblTotalSum.Text = "Итого по всем клиентам :  " & CStr(TotalSum)
                End If
            Catch
            End Try
        End Sub

        Private Sub repCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repCustomers.ItemDataBound
            Dim tbl As HtmlTable
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView
            Dim cust_id%

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Try
                    repSales = CType(e.Item.FindControl("repSales"), Repeater)
                    tbl = CType(e.Item.FindControl("tblSaleNotExists"), HtmlTable)

                    rv = CType(e.Item.DataItem, DataRowView)
                    cust_id = CInt(rv.Row("customer_sys_id"))

                    cmd = New SqlClient.SqlCommand("get_sales_by_customer")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust_id)
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    adapter = dbSQL.GetDataAdapter(cmd)
                    adapter.Fill(ds)

                    If ds.Tables(0).Rows.Count = 0 Then
                        repSales.Visible = False
                    Else
                        tbl.Visible = False
                        repSales.DataSource = ds
                        repSales.DataBind()
                    End If

                Catch
                End Try
            End If
        End Sub

        Private Sub repSales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repSales.ItemDataBound
            Dim sale_id%
            Dim drv As DataRowView
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Try
                    grdGoods = CType(e.Item.FindControl("grdGoods"), DataGrid)
                    drv = CType(e.Item.DataItem, DataRowView)
                    sale_id = CInt(drv.Row("sale_sys_id"))

                    Dim s$, sDogovor$
                    sDogovor = drv.Row("subdogovor")
                    If sDogovor.Trim.Length > 0 Then
                        sDogovor = drv.Row("dogovor") & sDogovor
                    Else
                        sDogovor = drv.Row("dogovor")
                    End If

                    If drv.Row("state") = 1 Then
                        s = "&nbsp;Заказ №" & sDogovor
                    Else
                        s = "&nbsp;Продажа №" & sDogovor
                        If drv.Row("type") = 1 Then
                            s &= "(без/нал)"
                        ElseIf drv.Row("type") = 3 Then
                            s &= "(сберкасса)"
                        Else
                            s &= "(наличные)"
                        End If
                    End If
                    Dim lbl As Label = CType(e.Item.FindControl("HeaderSale"), Label)
                    lbl.Text = s

                    cmd = New SqlClient.SqlCommand("get_goods_by_sale")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sale_sys_id", sale_id)
                    adapter = dbSQL.GetDataAdapter(cmd)
                    adapter.Fill(ds)
                    iCashCount.Clear()
                    grdGoods.DataSource = ds
                    grdGoods.DataBind()
                Catch
                End Try
            ElseIf e.Item.ItemType = ListItemType.Footer Then

                CType(e.Item.FindControl("lblTotalClient"), Label).Text = CStr(TotalClientSum)
                TotalClientSum = 0
            End If
        End Sub

        Private Sub grdGoods_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoods.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim d As Double
                If (e.Item.DataItem("is_cashregister") = True) Then
                    grdGoods.Columns(3).Visible = True
                    CType(e.Item.FindControl("lblGoodDescription"), Label).Text = e.Item.DataItem("good_description").ToString().Substring(0, 9) & "../" & e.Item.DataItem("good_description").ToString().Substring(9, e.Item.DataItem("good_description").ToString().Length - 9)
                End If
                If Not (IsDBNull(e.Item.DataItem("price")) Or IsDBNull(e.Item.DataItem("quantity"))) Then
                    d = CDbl(e.Item.DataItem("price")) * CDbl(e.Item.DataItem("quantity"))
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
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i

            ElseIf e.Item.ItemType = ListItemType.Footer Then
                TotalSum = TotalSum + dTotal
                TotalClientSum = TotalClientSum + dTotal
                CType(e.Item.FindControl("lblTotal"), Label).Text = CStr(dTotal)
                dTotal = 0
                Dim s$ = ""
                For i = 0 To iCashCount.Count - 1
                    s = s & iCashCount.GetKey(i) & ":&nbsp;&nbsp;" & iCashCount.GetByIndex(i) & "<br>"
                Next
                CType(e.Item.FindControl("lblTotalCountByCash"), Label).Text = s
            End If
        End Sub

    End Class

End Namespace
