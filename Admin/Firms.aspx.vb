Namespace Kasbi.Admin

    Partial Class Firms
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
                adapter = dbSQL.GetDataAdapter("select * from firm")
                adapter.Fill(ds)
                grdFirm.DataSource = ds.Tables(0).DefaultView
                grdFirm.DataKeyField = "firm_sys_id"
                grdFirm.DataBind()
            Catch
                msg.Text = "Ошибка загрузки списка фирм !<br>" & Err.Description
            End Try

        End Sub

        Private Sub grdFirm_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdFirm.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdFirm_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFirm.CancelCommand
            grdFirm.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdFirm_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFirm.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from firm where firm_sys_id={0}", grdFirm.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Response.Redirect(GetAbsoluteUrl("~/Admin/Firms.aspx"))
        End Sub

        Private Sub grdFirm_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFirm.UpdateCommand
            Dim sql$
            Dim txtFirmName As TextBox = CType(e.Item.FindControl("txtFirmNameEdit"), TextBox)
            Dim txtRekvisit As TextBox = CType(e.Item.FindControl("txtRekvisitEdit"), TextBox)
            Dim txtUstav As TextBox = CType(e.Item.FindControl("txtUstavEdit"), TextBox)
            Dim txtFIO As TextBox = CType(e.Item.FindControl("txtFIOEdit"), TextBox)
            Dim txtRazreshil As TextBox = CType(e.Item.FindControl("txtRazreshilEdit"), TextBox)

            If txtFirmName.Text = String.Empty Or txtRekvisit.Text = String.Empty Or txtUstav.Text = String.Empty Or txtFIO.Text = String.Empty Then
                msg.Text = "Неодходимо заполнить все поля"
                Exit Sub
            End If

            Try
                sql = String.Format("update firm set firm_name='{0}',rekvisit='{1}',ustav='{2}',fio='{3}',razreshil='{4}' where firm_sys_id={5}", txtFirmName.Text, txtRekvisit.Text, txtUstav.Text, txtFIO.Text, txtRazreshil.Text, grdFirm.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdFirm_CancelCommand(source, e)
        End Sub

        Private Sub grdFirm_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFirm.EditCommand
            grdFirm.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdFirm_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdFirm.PageIndexChanged
            grdFirm.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdFirm_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdFirm.ItemCommand
            If e.CommandName = "AddFirm" Then

                Dim txtFirmName As TextBox = CType(e.Item.FindControl("txtFirmName"), TextBox)
                Dim txtRekvisit As TextBox = CType(e.Item.FindControl("txtRekvisit"), TextBox)
                Dim txtUstav As TextBox = CType(e.Item.FindControl("txtUstav"), TextBox)
                Dim txtFIO As TextBox = CType(e.Item.FindControl("txtFIO"), TextBox)
                Dim txtRazreshil As TextBox = CType(e.Item.FindControl("txtRazreshil"), TextBox)
                If txtFirmName.Text = String.Empty Or txtRekvisit.Text = String.Empty Or txtUstav.Text = String.Empty Or txtFIO.Text = String.Empty Then
                    msg.Text = "Неодходимо заполнить все поля"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader

                sql = String.Format("select * from firm where firm_name like '%{0}%'", txtFirmName.Text)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msg.Text = "Такое наименование уже есть."
                    Exit Sub
                End If
                reader.Close()
                Try
                    sql = String.Format("insert into firm (firm_name,rekvisit,ustav,fio,razreshil) values('{0}','{1}','{2}','{3}','{4}')", txtFirmName.Text, txtRekvisit.Text, txtUstav.Text, txtFIO.Text, txtRazreshil.Text)
                    dbSQL.Execute(sql)
                Catch
                    msg.Text = "Ошибка добавления записи!<br>" & Err.Description
                Finally
                    Response.Redirect(GetAbsoluteUrl("~/Admin/Firms.aspx"))
                End Try
            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace

