Namespace Kasbi.Admin

    Partial Class PriceTO
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
        Const ClearString = "-------"


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If

        End Sub

        Sub BindPriceTOList(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from Place_Rn")
                ds = New DataSet
                adapt.Fill(ds)
                lst.DataSource = ds.Tables(0).DefaultView
                lst.DataTextField = "name"
                lst.DataValueField = "place_rn_id"
                lst.DataBind()
                'lst.Items.Insert(0, New ListItem(ClearString, ""))
                Try
                    lst.Items.FindByText(s).Selected = True
                Catch
                End Try
            Catch
                MsgBox("Ошибка загрузки информации о типах товара!<br>" & Err.Description)
                Exit Sub
            End Try
        End Sub

        Private Sub Bind()
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapter = dbSQL.GetDataAdapter("SELECT * FROM (select *, (select name from place_rn where place_rn.place_rn_id=priceto.place_rn_id) as place_name from priceto) t ORDER BY t.place_name")
                adapter.Fill(ds)
                grdPriceTO.DataSource = ds.Tables(0).DefaultView
                grdPriceTO.DataKeyField = "priceto_id"
                grdPriceTO.DataBind()
            Catch
                'msg.Text = "Ошибка загрузки списка единиц измерения !<br>" & Err.Description
            End Try

        End Sub

        Private Sub grdPriceTO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdPriceTO.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить запись ?')){return true} else {return false};")

                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
                '
            End If
            If e.Item.ItemType = ListItemType.Footer Then
                Dim lst As DropDownList = CType(e.Item.FindControl("lstPlaceRn"), DropDownList)
                BindPriceTOList(lst, 0)
            End If
            '
            If e.Item.ItemType = ListItemType.EditItem Then
                If IsDBNull(e.Item.DataItem("place_rn_id")) Then
                    BindPriceTOList(CType(e.Item.FindControl("lstPlaceRnEdit"), DropDownList), e.Item.DataItem("place_name"))
                Else
                    BindPriceTOList(CType(e.Item.FindControl("lstPlaceRnEdit"), DropDownList), e.Item.DataItem("place_name"))
                End If
            End If
        End Sub

        Private Sub grdPriceTO_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPriceTO.CancelCommand
            grdPriceTO.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdPriceTO_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPriceTO.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from priceto where priceto_id={0}", grdPriceTO.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Response.Redirect(GetAbsoluteUrl("~/Admin/PriceTO.aspx"))
        End Sub

        Private Sub grdPriceTO_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPriceTO.UpdateCommand
            Dim sql$

            Dim txtName As TextBox = CType(e.Item.FindControl("txtNameEdit"), TextBox)
            Dim txtValue As TextBox = CType(e.Item.FindControl("txtValueEdit"), TextBox)
            Dim txtGroup As TextBox = CType(e.Item.FindControl("txtGroupEdit"), TextBox)
            Dim lstPlaceRn As DropDownList = CType(e.Item.FindControl("lstPlaceRnEdit"), DropDownList)

            If txtName.Text = String.Empty Or txtValue.Text = String.Empty Then
                msg.Text = "Неодходимо заполнить все поля"
                Exit Sub
            End If
            Try
                sql = String.Format("update priceto set priceto_name='{0}',priceto_value='{1}', place_rn_id='{2}', group_id='{3}' where priceto_id={4}", txtName.Text, txtValue.Text, lstPlaceRn.SelectedValue, txtGroup.Text, grdPriceTO.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdPriceTO_CancelCommand(source, e)
        End Sub

        Private Sub grdPriceTO_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPriceTO.EditCommand
            grdPriceTO.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdPriceTO_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdPriceTO.PageIndexChanged
            grdPriceTO.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdPriceTO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdPriceTO.ItemCommand
            If e.CommandName = "AddUnit" Then
                Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
                Dim txtValue As TextBox = CType(e.Item.FindControl("txtValue"), TextBox)
                Dim txtGroup As TextBox = CType(e.Item.FindControl("txtGroup"), TextBox)
                Dim lstPlaceRn As DropDownList = CType(e.Item.FindControl("lstPlaceRn"), DropDownList)

                If txtName.Text = String.Empty Or txtValue.Text = String.Empty Then
                    msg.Text = "Неодходимо заполнить все поля"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader

                'sql = String.Format("select * from priceto where priceto_name like '%{0}%'", txtName.Text)
                'reader = dbSQL.GetReader(sql)
                'If reader.Read() Then
                'msg.Text = "Такое наименование уже есть."
                'Exit Sub
                'End If
                'reader.Close()

                Try
                    sql = String.Format("insert into priceto (priceto_id, priceto_name, priceto_value, place_rn_id, group_id) values('" & grdPriceTO.Items.Count + 1 & "', '{0}','{1}','{2}', '{3}')", txtName.Text, txtValue.Text, lstPlaceRn.Text, txtGroup.Text)
                    dbSQL.Execute(sql)
                Catch
                    msg.Text = "Ошибка добавления записи!<br>" & Err.Description
                Finally
                    Response.Redirect(GetAbsoluteUrl("~/Admin/PriceTO.aspx"))
                End Try

            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
