Namespace Kasbi.Admin

    Partial Class GoodGroups
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

            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select * from good_group order by name ")
                ds = New DataSet
                adapt.Fill(ds)
                grdGoodGroups.DataSource = ds.Tables(0).DefaultView
                grdGoodGroups.DataKeyField = "good_group_sys_id"
                grdGoodGroups.DataBind()
            Catch
                msgError.Text = "Ошибка загрузки информации о группах товара!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdGoodGroups_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoodGroups.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdGoodGroups_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodGroups.ItemCommand
            If e.CommandName = "AddGoodGroup" Then
                Dim txtName As TextBox = CType(e.Item.FindControl("tbxNameNew"), TextBox)
                If txtName.Text = String.Empty Then
                    msgError.Text = "Необходимо заполнить"
                    Exit Sub
                End If
                Dim reader As SqlClient.SqlDataReader

                Dim sql As String
                sql = String.Format("select * from good_group where name = '{0}'", txtName.Text.Trim)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msgError.Text = "Группа товара с таким наименованием уже есть."
                    Exit Sub
                End If
                reader.Close()

                Dim tbxDescription As TextBox = CType(e.Item.FindControl("tbxDescriptionNew"), TextBox)
                Dim cbxIs_show As CheckBox = CType(e.Item.FindControl("cbxIs_showNew"), CheckBox)
                Dim is_show
                If cbxIs_show.Checked = True Then
                    is_show = 1
                Else
                    is_show = 0
                End If

                Try
                    sql = String.Format("INSERT INTO good_group (name, description, is_show) VALUES('{0}', '{1}', '{2}')", txtName.Text.Trim, tbxDescription.Text, is_show)
                    dbSQL.Execute(sql)
                    Bind()
                Catch
                    msgError.Text = "Ошибка вставки нового типа товара !<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub grdGoodGroups_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodGroups.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from good_group where good_group_sys_id = {0}", grdGoodGroups.DataKeys(e.Item.ItemIndex))
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

        Private Sub grdGoodGroups_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodGroups.CancelCommand
            grdGoodGroups.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdGoodGroups_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodGroups.UpdateCommand
            Dim cmd As SqlClient.SqlCommand
            Dim sql$

            Dim tbxName As TextBox = CType(e.Item.FindControl("tbxNameEdit"), TextBox)
            Dim tbxDescription As TextBox = CType(e.Item.FindControl("tbxDescriptionEdit"), TextBox)
            Dim cbxIs_show As CheckBox = CType(e.Item.FindControl("cbxIs_showEdit"), CheckBox)
            Dim is_show

            If cbxIs_show.Checked = True Then
                is_show = 1
            Else
                is_show = 0
            End If

            Try
                sql = String.Format("update good_group set name='{0}',description='{1}', is_show='{2}' where good_group_sys_id = {3}", tbxName.Text, tbxDescription.Text, is_show, grdGoodGroups.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msgError.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdGoodGroups.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdGoodTypes_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdGoodGroups.PageIndexChanged
            grdGoodGroups.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdGoodTypes_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoodGroups.EditCommand
            grdGoodGroups.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
