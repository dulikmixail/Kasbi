Namespace Kasbi.Admin

    Partial Class Suppliers
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


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If CurrentUser.permissions = 4 Then
                btnNew.Visible = True
            Else
                btnNew.Visible = False
            End If
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select * from supplier order by supplier_name")
                ds = New DataSet
                adapt.Fill(ds)
                If ViewState("suppliersort") = "" Then
                    ds.Tables(0).DefaultView.Sort = " supplier_name"
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("suppliersort")
                End If
                grdSuppliers.DataSource = ds.Tables(0).DefaultView
                grdSuppliers.DataKeyField = "sys_id"
                grdSuppliers.DataBind()
            Catch
                msgSuppliers.Text = "Ошибка загрузки информации о поставщиках!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdSuppliers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSuppliers.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdSuppliers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSuppliers.ItemCommand
            If e.CommandName = "Delete" Then
                DeleteSupplier(grdSuppliers.DataKeys(e.Item.ItemIndex))
            ElseIf e.CommandName = "Edit" Then
                Response.Redirect("SupplierDetail.aspx?SupplierDetailID=" & grdSuppliers.DataKeys(e.Item.ItemIndex))
            End If
        End Sub

        Private Sub DeleteSupplier(ByVal supplier_id As Integer)
            Try
                Dim sql$ = String.Format("delete from supplier where sys_id = {0}", supplier_id)
                dbSQL.Execute(sql)
                Response.Redirect("Suppliers.aspx")
            Catch ex As Exception

            End Try
        End Sub

        Private Sub grdSuppliers_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdSuppliers.SortCommand
            If ViewState("suppliersort") = e.SortExpression Then
                ViewState("suppliersort") = e.SortExpression & " DESC"
            Else
                ViewState("suppliersort") = e.SortExpression
            End If
            Bind()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect("Default.aspx")
        End Sub

    End Class

End Namespace
