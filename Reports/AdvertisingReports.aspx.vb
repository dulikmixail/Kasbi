
Namespace Kasbi.Reports

    Partial Class AdvertisingReports
        Inherits PageBase

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

        Public date_start As DateTime
        Public date_end As DateTime
        Dim adv_num%
        Dim WithEvents repCustomers As Repeater
        Dim WithEvents grdSales As DataGrid
        Dim i%
        Dim TotalCost, TotalGoodsCount As Double
        Dim Total_advercity

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))

                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")

                Dim parce = Split(date_start, ".")
                date_start = parce(1) & "/" & parce(0) & "/" & parce(2)
                parce = Split(date_end, ".")
                date_end = parce(1) & "/" & parce(0) & "/" & parce(2)

            Catch
                'date_start = Now.AddMonths(-1)
                'date_end = Now
            End Try
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try

                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")
                cmd = New SqlClient.SqlCommand("prc_rpt_Advertising")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                adv_num = 0

                repAdvertising.DataSource = ds
                repAdvertising.DataBind()
            Catch
            End Try
        End Sub

        Private Sub repAdvertising_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repAdvertising.ItemDataBound
            Dim tbl As HtmlTable
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView
            Dim advertise_id As Object

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Try
                    repCustomers = CType(e.Item.FindControl("repCustomers"), Repeater)
                    tbl = CType(e.Item.FindControl("tblSaleNotExists"), HtmlTable)

                    rv = CType(e.Item.DataItem, DataRowView)
                    If Not IsDBNull(rv.Row("advertise_id")) Then
                        advertise_id = CInt(rv.Row("advertise_id"))
                    Else
                        advertise_id = DBNull.Value
                    End If

                    cmd = New SqlClient.SqlCommand("prc_rpt_get_customers_by_advertising")

                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_advertise_id", advertise_id)
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    adapter = dbSQL.GetDataAdapter(cmd)
                    adapter.Fill(ds)

                    If ds.Tables(0).Rows.Count = 0 Then
                        repCustomers.Visible = False
                    Else
                        tbl.Visible = False
                        repCustomers.DataSource = ds
                        repCustomers.DataBind()
                        Try
                            CType(e.Item.FindControl("lbltotaladvertis1"), Label).Text = Str(Total_advercity)
                            CType(e.Item.FindControl("lbltotaladvertis2"), Label).Text = Str(Total_advercity)
                            Total_advercity = 0
                        Catch
                        End Try
                    End If
                Catch
                End Try
            End If
        End Sub

        Private Sub repCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repCustomers.ItemDataBound
            Dim cust_id%
            Dim drv As DataRowView
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Try
                    grdSales = CType(e.Item.FindControl("grdSales"), DataGrid)
                    drv = CType(e.Item.DataItem, DataRowView)
                    cust_id = CInt(drv.Row("customer_sys_id"))

                    'Dim s$, sDogovor$
                    'sDogovor = drv.Row("subdogovor")
                    'If sDogovor.Trim.Length > 0 Then
                    '    sDogovor = drv.Row("dogovor") & sDogovor
                    'Else
                    '    sDogovor = drv.Row("dogovor")
                    'End If
                    '
                    'If drv.Row("state") = 1 Then
                    '    s = "&nbsp;Заказ №" & sDogovor
                    'Else
                    '    s = "&nbsp;Продажа №" & sDogovor
                    '    If drv.Row("type") = 1 Then
                    '        s &= "(без/нал)"
                    '    ElseIf drv.Row("type") = 3 Then
                    '        s &= "(сберкасса)"
                    '    Else
                    '        s &= "(наличные)"
                    '    End If
                    'End If
                    'Dim lbl As Label = CType(e.Item.FindControl("HeaderSale"), Label)
                    'lbl.Text = s

                    cmd = New SqlClient.SqlCommand("prc_rpt_get_sale_by_cust")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust_id)
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    adapter = dbSQL.GetDataAdapter(cmd)
                    adapter.Fill(ds)

                    grdSales.DataSource = ds
                    grdSales.DataBind()

                Catch
                End Try

            End If
        End Sub


        Private Sub grdSales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSales.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                TotalGoodsCount = TotalGoodsCount + CInt(e.Item.DataItem("goods_count"))
                TotalCost = TotalCost + CDbl(e.Item.DataItem("summa"))
                i = i + 1
                CType(e.Item.FindControl("lblNumSale"), Label).Text = i
            ElseIf e.Item.ItemType = ListItemType.Footer Then

                Dim query As String
                query = "SELECT * FROM cash_history ORDER by sys_id desc"

                CType(e.Item.FindControl("lblTotalGoodsCount"), Label).Text = CStr(TotalGoodsCount)
                CType(e.Item.FindControl("lblTotalCost"), Label).Text = CStr(TotalCost)
                Total_advercity = Total_advercity + TotalCost
                TotalCost = 0
                TotalGoodsCount = 0
                i = 0
            End If

        End Sub

    End Class

End Namespace
