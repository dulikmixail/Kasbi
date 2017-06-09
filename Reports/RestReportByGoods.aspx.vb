Namespace Kasbi.Reports

    Partial Class RestReportByGoods
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
        Dim WithEvents repSales As Repeater
        Dim WithEvents grdGoods As DataGrid
        Dim goodTypes$
        Dim num%
        Dim dTotal, dTotalSummComming, dTotalSummRetail, dTotalProfitability As Double
        Dim cs%

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
                lblCurrentDate.Text = Format(Now, "dd.MM.yyyy")
                cmd = New SqlClient.SqlCommand("prc_rpt_RestGoods")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_gt_type", cs)
                If cs = 2 Then
                    cmd.Parameters.AddWithValue("@pi_goodTypes", goodTypes)
                End If
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                dTotalSummComming = 0
                dTotalSummRetail = 0
                dTotalProfitability = 0
                num = 0
                grid.DataSource = ds
                grid.DataBind()
                tbl = CType(FindControl("tblSaleNotExists"), HtmlTable)
                If ds.Tables(0).Rows.Count = 0 Then
                    tbl.Visible = True
                Else
                    tbl.Visible = False
                End If
            Catch
            End Try
        End Sub

        Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                num = num + 1

                If Not IsDBNull(e.Item.DataItem("price_comming")) Then
                    CType(e.Item.FindControl("lblPriceComming"), Label).Text = e.Item.DataItem("price_comming")
                End If

                If Not IsDBNull(e.Item.DataItem("retail_price")) Then
                    CType(e.Item.FindControl("lblRetailPrice"), Label).Text = e.Item.DataItem("retail_price")
                Else
                    If Not IsDBNull(e.Item.DataItem("retail_price_opt")) Then
                        CType(e.Item.FindControl("lblRetailPrice"), Label).Text = e.Item.DataItem("retail_price_opt")

                    End If
                End If

                Dim SummComming As Double
                SummComming = 0
                If Not IsDBNull(e.Item.DataItem("price_comming")) Then
                    If e.Item.DataItem("price_comming") < 2 Then
                        CType(e.Item.FindControl("lblSummComming"), Label).Text = "Приходная цена не определена"
                    Else
                        SummComming = e.Item.DataItem("price_comming") * e.Item.DataItem("quantity")
                        CType(e.Item.FindControl("lblSummComming"), Label).Text = SummComming
                        dTotalSummComming = dTotalSummComming + SummComming
                    End If
                End If

                Dim SummRetail As Double
                SummRetail = 0
                If Not IsDBNull(e.Item.DataItem("retail_price")) Then
                    SummRetail = e.Item.DataItem("retail_price") * e.Item.DataItem("quantity")
                    CType(e.Item.FindControl("lblSummRetail"), Label).Text = SummRetail
                    dTotalSummRetail = dTotalSummRetail + SummRetail
                Else
                    If Not IsDBNull(e.Item.DataItem("retail_price_opt")) Then
                        SummRetail = e.Item.DataItem("retail_price_opt") * e.Item.DataItem("quantity")
                        CType(e.Item.FindControl("lblSummRetail"), Label).Text = SummRetail
                        dTotalSummRetail = dTotalSummRetail + SummRetail
                    Else
                        CType(e.Item.FindControl("lblSummRetail"), Label).Text = "Розничная цена не определена"
                    End If

                End If

                Dim Profitability As Double
                Profitability = 0
                If SummComming > 1 Then
                    Profitability = SummRetail / SummComming
                    CType(e.Item.FindControl("lblProfitability"), Label).Text = Math.Round(Profitability, 2)
                Else
                    CType(e.Item.FindControl("lblProfitability"), Label).Text = " Невозможно расчитать "
                End If

                CType(e.Item.FindControl("lblRecordNum"), Label).Text = num
                If ((num Mod 2) > 0) Then
                    e.Item.BackColor = Drawing.Color.FromArgb(248, 238, 210)
                Else
                    e.Item.BackColor = Drawing.Color.FromArgb(255, 255, 255)
                End If

            ElseIf e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblTotalSummComming"), Label).Text = CStr(dTotalSummComming)
                CType(e.Item.FindControl("lblTotalSummRetail"), Label).Text = CStr(dTotalSummRetail)
                CType(e.Item.FindControl("lblTotalProfitability"), Label).Text = CStr(Math.Round(dTotalSummRetail / dTotalSummComming, 2))
                dTotalSummComming = 0
                dTotalSummRetail = 0
            End If
        End Sub

    End Class

End Namespace
