Namespace Kasbi.Admin

    Partial Class Advertising
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
                adapter = dbSQL.GetDataAdapter("prc_getAdvertising", True)
                adapter.Fill(ds)
                grdAdvertising.DataSource = ds.Tables(0).DefaultView
                grdAdvertising.DataKeyField = "advertise_id"
                grdAdvertising.DataBind()
            Catch
                msg.Text = "Ошибка загрузки списка рекламы !<br>" & Err.Description
            End Try

        End Sub

        Private Sub grdAdvertising_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdAdvertising.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdAdvertising_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAdvertising.CancelCommand
            grdAdvertising.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdAdvertising_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAdvertising.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from Advertising where Advertise_id={0}", grdAdvertising.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Response.Redirect("Advertising.aspx")
        End Sub

        Private Sub grdAdvertising_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAdvertising.UpdateCommand
            Dim txtAdv_NameEdit As TextBox
            Dim sql$

            txtAdv_NameEdit = CType(e.Item.FindControl("txtAdv_NameEdit"), TextBox)
            Try
                sql = String.Format("update Advertising set Adv_Name='{0}' where Advertise_id={1}", txtAdv_NameEdit.Text, grdAdvertising.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdAdvertising_CancelCommand(source, e)
        End Sub

        Private Sub grdAdvertising_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAdvertising.EditCommand
            grdAdvertising.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdAdvertising_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdAdvertising.PageIndexChanged
            grdAdvertising.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub grdAdvertising_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdAdvertising.ItemCommand
            If e.CommandName = "AddAdvertise" Then

                Dim txtAdv_Name As TextBox = CType(e.Item.FindControl("txtAdv_Name"), TextBox)
                If txtAdv_Name.Text = String.Empty Then
                    msg.Text = "Неодходимо заполнить"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader

                sql = String.Format("select * from Advertising where Adv_Name like '%{0}%'", txtAdv_Name.Text)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msg.Text = "Такое наименование уже есть."
                    Exit Sub
                End If
                reader.Close()

                sql = String.Format("insert into Advertising (Adv_Name) values('{0}')", txtAdv_Name.Text)
                dbSQL.Execute(sql)
                Response.Redirect(GetAbsoluteUrl("~/Admin/Advertising.aspx"))
            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
