Namespace Kasbi.Admin

    Partial Class Units
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
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapter = dbSQL.GetDataAdapter("select * from Units")
                adapter.Fill(ds)
                grdUnits.DataSource = ds.Tables(0).DefaultView
                grdUnits.DataKeyField = "unit_id"
                grdUnits.DataBind()
            Catch
                msg.Text = "Ошибка загрузки списка единиц измерения !<br>" & Err.Description
            End Try

        End Sub

        Private Sub grdUnits_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUnits.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdUnits_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUnits.CancelCommand
            grdUnits.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdUnits_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUnits.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from Units where unit_id={0}", grdUnits.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Response.Redirect(GetAbsoluteUrl("~/Admin/Units.aspx"))
        End Sub

        Private Sub grdUnits_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUnits.UpdateCommand
            Dim sql$

            Dim txtName As TextBox = CType(e.Item.FindControl("txtNameEdit"), TextBox)
            Dim txtShortName As TextBox = CType(e.Item.FindControl("txtShortNameEdit"), TextBox)
            If txtName.Text = String.Empty Or txtShortName.Text = String.Empty Then
                msg.Text = "Неодходимо заполнить все поля"
                Exit Sub
            End If

            Try
                sql = String.Format("update Units set UnitDesciption='{0}',UnitShortDesciption='{1}' where unit_id={2}", txtName.Text, txtShortName.Text, grdUnits.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdUnits_CancelCommand(source, e)
        End Sub

        Private Sub grdUnits_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUnits.EditCommand
            grdUnits.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdUnits_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdUnits.PageIndexChanged
            grdUnits.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdUnits_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUnits.ItemCommand
            If e.CommandName = "AddUnit" Then

                Dim txtName As TextBox = CType(e.Item.FindControl("txtName"), TextBox)
                Dim txtShortName As TextBox = CType(e.Item.FindControl("txtShortName"), TextBox)
                If txtName.Text = String.Empty Or txtShortName.Text = String.Empty Then
                    msg.Text = "Неодходимо заполнить все поля"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader

                sql = String.Format("select * from Units where UnitDesciption like '%{0}%'", txtName.Text)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msg.Text = "Такое наименование уже есть."
                    Exit Sub
                End If
                reader.Close()
                Try
                    sql = String.Format("insert into Units (UnitDesciption,UnitShortDesciption) values('{0}','{1}')", txtName.Text, txtShortName.Text)
                    dbSQL.Execute(sql)
                Catch
                    msg.Text = "Ошибка добавления записи!<br>" & Err.Description
                Finally
                    Response.Redirect(GetAbsoluteUrl("~/Admin/Units.aspx"))
                End Try
            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
