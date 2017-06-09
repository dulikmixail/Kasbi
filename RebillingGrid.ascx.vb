
Namespace Kasbi

    Partial Class RebillingGrid
        Inherits System.Web.UI.UserControl

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
        Dim i%, iCount1%, iCount2%
        Dim dTotal As Double
        Public iSale As Integer
        Public iCustomer As Integer
        Dim iCashCount As New SortedList
        Private currentPage As PageBase

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currentPage = Page
            Bind()
            btnDelete.Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить переоформление?')){return confirm('Вся информация будет удалена. Продолжить?');}else {return false};")
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_goods_by_rebilling")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                adapt = currentPage.dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                grdRebilling.DataSource = ds.Tables(0).DefaultView
                grdRebilling.DataKeyField = "good_sys_id"
                dTotal = 0
                i = 0
                iCashCount.Clear()
                grdRebilling.DataBind()
            Catch
                msgRebilling.Text = Err.Description
            End Try
        End Sub

        Private Sub grd_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRebilling.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                If e.Item.DataItem("is_cashregister") Then
                    If iCashCount.Item(e.Item.DataItem("good_name")) Is Nothing Then
                        iCashCount.Add(e.Item.DataItem("good_name"), "1")
                    Else
                        Dim iC As Int32 = CInt(iCashCount.Item(e.Item.DataItem("good_name")))
                        iC = iC + 1
                        iCashCount.Item(e.Item.DataItem("good_name")) = CStr(iC)

                    End If
                    CType(e.Item.FindControl("lnkAkt_Pokazaniy"), HyperLink).NavigateUrl = currentPage.GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & iCustomer & "&s=" & iSale & "&t=4&n=" & e.Item.ItemIndex)
                    CType(e.Item.FindControl("lnkTeh_Zaklyuchenie"), HyperLink).NavigateUrl = currentPage.GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & iCustomer & "&s=" & iSale & "&t=8&n=" & e.Item.ItemIndex)
                    CType(e.Item.FindControl("lnkUdostoverenie_Kassira"), HyperLink).NavigateUrl = currentPage.GetAbsoluteUrl("~/documents.aspx?rebilling=1&c=" & iCustomer & "&s=" & iSale & "&t=9&n=" & e.Item.ItemIndex)
                Else
                    CType(e.Item.FindControl("lnkAkt_Pokazaniy"), HyperLink).Visible = False
                    CType(e.Item.FindControl("lnkTeh_Zaklyuchenie"), HyperLink).Visible = False
                    CType(e.Item.FindControl("lnkUdostoverenie_Kassira"), HyperLink).Visible = False
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                Dim s$ = ""
                For i = 0 To iCashCount.Count - 1
                    s = s & iCashCount.GetKey(i) & ":&nbsp;&nbsp;" & iCashCount.GetByIndex(i) & "<br>"
                Next
                CType(e.Item.FindControl("lblTotalCountByCash"), Label).Text = s

            End If
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnDelete.Click
            Dim cmd As SqlClient.SqlCommand

            Try
                cmd = New SqlClient.SqlCommand("remove_rebilling")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                currentPage.dbSQL.Execute(cmd)
                Session("CurrentPage") = "CustomerSales"
                Response.Redirect("CustomerSales.aspx?" & iCustomer)
            Catch
                msgRebilling.Text = "Ошибка удаления записи!<br>" & Err.Description
            End Try
        End Sub

        Private Sub btnCreateDocuments_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateDocuments.Click
            Dim doc_num() As Integer = New Integer(5) {6, 7, 4, 8, 9, 18}
            Dim docs As New Kasbi.Migrated_Documents
            docs.ProcessDocuments(doc_num, iCustomer, iSale, 1)
            docs = Nothing
        End Sub
    End Class

End Namespace
