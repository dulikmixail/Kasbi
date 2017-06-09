Namespace Kasbi.Reports

    Partial Class SellingReportByGoods
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim WithEvents repSales As Repeater
        Dim WithEvents grdGoods As DataGrid
        Dim goodTypes$
        Dim date_start As DateTime
        Dim date_end As DateTime
        Dim dTotal%, i%, j%, rt%, cs%
        Dim TotalSum As Double

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
            If cs = 2 Then
                goodTypes = CStr(Request.QueryString("g_t"))
            End If
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim tbl As HtmlTable

            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("get_goodTypes_with_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_gt_type", cs)
                If cs = 2 Then
                    cmd.Parameters.AddWithValue("@pi_goodTypes", goodTypes)
                End If

                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                repGoodTypes.DataSource = ds
                repGoodTypes.DataBind()
                tbl = CType(FindControl("tblSaleNotExists"), HtmlTable)

                If ds.Tables(0).Rows.Count = 0 Then
                    tbl.Visible = True
                    pnlTotal.Visible = False
                    lblTotalSum.Text = ""
                Else
                    tbl.Visible = False
                    pnlTotal.Visible = True
                    lblTotalSum.Text = "Итого :  " & CStr(TotalSum)
                End If

            Catch
            End Try
        End Sub

        Private Sub repGoodTypes_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGoodTypes.ItemDataBound
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView
            Dim gt_id%

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                grdGoods = CType(e.Item.FindControl("grdGoods"), DataGrid)
                rv = CType(e.Item.DataItem, DataRowView)
                gt_id = CInt(rv.Row("good_type_sys_id"))
                i = 0
                j = 0
                Try
                    cmd = New SqlClient.SqlCommand("get_sales_by_good_type")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_type_sys_id", gt_id)
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds)
                    grdGoods.DataSource = ds
                    grdGoods.DataBind()
                Catch
                End Try
            End If
        End Sub

        Private Sub grdGoods_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoods.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim num_cash As Label = CType(e.Item.FindControl("lblNum_Cashregister"), Label)
                If (e.Item.DataItem("is_cashregister") = True) Then
                    grdGoods.Columns(3).Visible = True
                    num_cash.Text = e.Item.DataItem("num_cashregister")
                Else
                    grdGoods.Columns(3).Visible = False
                End If
                CType(e.Item.FindControl("lblSaleID"), Label).Text = e.Item.DataItem("sale_sys_id")
                CType(e.Item.FindControl("lblSaleDate"), Label).Text = Format(e.Item.DataItem("sale_date"), "dd.MM.yyyy")
                CType(e.Item.FindControl("lblSaleCustomer"), Label).Text = e.Item.DataItem("customer_name")

                Dim d As Double
                If Not (IsDBNull(e.Item.DataItem("price")) Or IsDBNull(e.Item.DataItem("quantity"))) Then
                    d = CDbl(e.Item.DataItem("price")) * CDbl(e.Item.DataItem("quantity"))
                    CType(e.Item.FindControl("lblCost"), Label).Text = CStr(d)
                    dTotal = dTotal + d
                End If
                i = i + CDbl(e.Item.DataItem("quantity"))
                j = j + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = j
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                TotalSum = TotalSum + dTotal
                CType(e.Item.FindControl("lblTotal"), Label).Text = CStr(dTotal)
                dTotal = 0
                CType(e.Item.FindControl("lblGoodsCount"), Label).Text = CStr(i)
            End If
        End Sub

    End Class

End Namespace
