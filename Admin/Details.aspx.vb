Namespace Kasbi.Admin

    Partial Class Details
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
            'Put user code to initialize the page here
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from details order by in_stock desc, is_detail desc, detail_name asc")
                ds = New DataSet
                adapt.Fill(ds)
                grdDetails.DataSource = ds.Tables(0).DefaultView
                grdDetails.DataKeyField = "detail_id"
                grdDetails.DataBind()
            Catch
                msgError.Text = "Ошибка загрузки информации о деталях и работах!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdDetails.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdDetails_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.ItemCommand
            If e.CommandName = "AddGoodType" Then
                Dim txtName As TextBox = CType(e.Item.FindControl("tbxNameNew"), TextBox)
                If txtName.Text = String.Empty Then
                    msgError.Text = "Необходимо заполнить"
                    Exit Sub
                End If
                Dim cbxIsDetail As CheckBox = CType(e.Item.FindControl("cbxIsDetailNew"), CheckBox)
                Dim cbxDisabled As CheckBox = CType(e.Item.FindControl("cbxDisabledNew"), CheckBox)
                Dim tbxDetailNotation As TextBox = CType(e.Item.FindControl("tbxDetailNotationNew"), TextBox)
                Dim tbxPrice As TextBox = CType(e.Item.FindControl("tbxPriceNew"), TextBox)
                Dim tbxCostService As TextBox = CType(e.Item.FindControl("tbxCostServiceNew"), TextBox)
                Dim tbxTotalSum As TextBox = CType(e.Item.FindControl("tbxTotalSumNew"), TextBox)
                Dim tbxNormaHour As TextBox = CType(e.Item.FindControl("tbxNormaHourNew"), TextBox)
                Try
                    Dim sql$ = String.Format("INSERT INTO details (detail_name, is_detail, detail_notation, price, cost_service, total_sum,norma_hour, in_stock) VALUES('{0}', {1}, '{2}', '{3}', '{4}', '{5}','{6}','{7}')", txtName.Text.Trim, CInt(cbxIsDetail.Checked), tbxDetailNotation.Text, Replace(tbxPrice.Text, ",", "."), Replace(tbxCostService.Text, ",", "."), Replace(tbxTotalSum.Text, ",", "."), Replace(tbxNormaHour.Text, ",", "."), CInt(cbxDisabled.Checked))
                    dbSQL.Execute(sql)
                    Bind()
                Catch
                    msgError.Text = "Ошибка вставки новой записи деталей и работ!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub grdDetails_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from details where detail_id = {0}", grdDetails.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
                Bind()
            Catch
                If Err.Number = 1 Then
                    msgError.Text = "Выбранную запись нельзя удалить!"
                Else
                    msgError.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
        End Sub

        Private Sub grdDetails_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.CancelCommand
            grdDetails.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdDetails_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.UpdateCommand
            Dim sql$

            Dim tbxName As TextBox = CType(e.Item.FindControl("tbxNameEdit"), TextBox)
            Dim cbxIsDetail As CheckBox = CType(e.Item.FindControl("cbxIsDetailEdit"), CheckBox)
            Dim cbxDisabled As CheckBox = CType(e.Item.FindControl("cbxDisabledEdit"), CheckBox)
            Dim tbxDetailNotation As TextBox = CType(e.Item.FindControl("tbxDetailNotationEdit"), TextBox)
            Dim tbxPrice As TextBox = CType(e.Item.FindControl("tbxPriceEdit"), TextBox)
            Dim tbxCostService As TextBox = CType(e.Item.FindControl("tbxCostServiceEdit"), TextBox)
            Dim tbxTotalSum As TextBox = CType(e.Item.FindControl("tbxTotalSumEdit"), TextBox)
            Dim tbxNormaHour As TextBox = CType(e.Item.FindControl("tbxNormaHourEdit"), TextBox)

            Try
                sql = String.Format("update details set detail_name='{0}', is_detail={1}, detail_notation='{2}', price={3}, cost_service={4} , total_sum={5},norma_hour={6},in_stock={8} where detail_id = {7}", tbxName.Text, CInt(cbxIsDetail.Checked), tbxDetailNotation.Text, Replace(tbxPrice.Text, ",", "."), Replace(tbxCostService.Text, ",", "."), Replace(tbxTotalSum.Text, ",", "."), Replace(tbxNormaHour.Text, ",", "."), grdDetails.DataKeys(e.Item.ItemIndex), CInt(cbxDisabled.Checked))
                dbSQL.Execute(sql)
            Catch
                msgError.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try

            grdDetails.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdDetails_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdDetails.PageIndexChanged
            grdDetails.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdDetails_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.EditCommand
            grdDetails.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

        Protected Sub btnAddNormHour_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNormHour.Click
            If txtNormHour.Text <> "" Then
                dbSQL.Execute("update details set cost_service=ROUND(norma_hour*" & Replace(txtNormHour.Text, ",", ".") & ", 2), total_sum=ROUND(price+norma_hour*" & Replace(txtNormHour.Text, ",", ".") & ", 2)")
                Bind()
            End If
        End Sub
    End Class

End Namespace
