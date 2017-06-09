Namespace Kasbi.Reports

    Partial Class SellingReportByManager
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
        Dim rt%, cs%, ttype%, first_sale%
        Dim managers$
        Dim date_start As DateTime
        Dim date_end As DateTime
        Dim WithEvents repSales As Repeater
        Dim WithEvents grdGoods As DataGrid
        Dim dTotal%, iCount1%, iCount2%, i%, iCustomer%, iSale%
        Dim TotalSum, TotalClientSum, TotalClientNum As Double
        Dim iCashCount As New SortedList

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            rt = GetPageParam("rt")
            ttype = GetPageParam("ttype")
            first_sale = GetPageParam("first_sale")

            Try
                date_start = CDate(Request.QueryString("db"))
                date_end = CDate(Request.QueryString("de"))
                'date_start = Request.QueryString("db")
                'date_end = Request.QueryString("de")
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try

            'Dim parce = Split(date_start, ".")
            'date_start = parce(0) & "/" & parce(1) & "/" & parce(2)
            'parce = Split(date_end, ".")
            'date_end = parce(0) & "/" & parce(1) & "/" & parce(2)

            cs = GetPageParam("cs")
            If cs = 2 Then
                managers = CStr(Request.QueryString("mana"))
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
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("get_manager")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_manager_type", cs)
                cmd.Parameters.AddWithValue("@pi_managers", managers)

                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                repManager.DataSource = ds
                repManager.DataBind()
            Catch
            End Try
        End Sub

        Private Sub repManager_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repManager.ItemDataBound
            Dim tbl As HtmlTable
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView
            Dim man_id%
            If ((e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem)) Then
                Try
                    repSales = CType(e.Item.FindControl("repSales"), Repeater)
                    tbl = CType(e.Item.FindControl("tblSaleNotExists"), HtmlTable)

                    rv = CType(e.Item.DataItem, DataRowView)
                    man_id = CInt(rv.Row("sys_id"))

                    If ttype = 2 Then
                        cmd = New SqlClient.SqlCommand("get_customers_by_manager2")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_manager_sys_id", man_id)
                        cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                        cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    Else

                        If first_sale = 1 Then
                            cmd = New SqlClient.SqlCommand("get_customers_by_manager_firstsale")
                        Else
                            cmd = New SqlClient.SqlCommand("get_customers_by_manager")
                        End If

                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_manager_sys_id", man_id)
                        cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                        cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    End If

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
                CType(e.Item.FindControl("lblTotalClient1"), Label).Text = CStr(TotalClientSum)
                CType(e.Item.FindControl("lblTotalClient2"), Label).Text = CStr(TotalClientSum)
                CType(e.Item.FindControl("lblTotalClientNum"), Label).Text = CStr(TotalClientNum)

                Try
                    CType(e.Item.FindControl("lblTotalClientNum2"), Label).Text = CStr(TotalClientNum)
                Catch ex As Exception
                End Try

                If TotalClientNum = 0 Then
                    e.Item.Visible = False
                End If

                TotalClientSum = 0
                TotalClientNum = 0
            End If
        End Sub

        Private Sub repSales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repSales.ItemDataBound
            Dim sale_id%
            Dim drv As DataRowView
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim d As Double

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Try
                    If Not (IsDBNull(e.Item.DataItem("price"))) Then
                        d = CDbl(e.Item.DataItem("price"))
                        CType(e.Item.FindControl("Cost"), Label).Text = CStr(d)
                        TotalClientSum = TotalClientSum + d
                        TotalClientNum = TotalClientNum + 1
                    End If
                Catch
                End Try
            ElseIf e.Item.ItemType = ListItemType.Footer Then

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
