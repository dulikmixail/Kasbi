Namespace Kasbi.Admin

    Partial Class IMNS
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

        Private Sub Bind(Optional ByVal filter As String = "")
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                cmd = New SqlClient.SqlCommand("prc_getIMNS")
                cmd.CommandType = CommandType.StoredProcedure
                If Not filter Is Nothing AndAlso filter <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_imns_name_filter", filter)
                End If
                adapter = dbSQL.GetDataAdapter(cmd)
                adapter.Fill(ds)
                grdIMNS.DataSource = ds.Tables(0).DefaultView
                grdIMNS.DataKeyField = "sys_id"
                grdIMNS.DataBind()
            Catch
                msg.Text = "Ошибка загрузки списка ИМНС!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdIMNS_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdIMNS.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdIMNS_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIMNS.ItemCommand
            If e.CommandName = "AddIMNS" Then
                Dim txtIMNSName As TextBox = CType(e.Item.FindControl("txtIMNSName"), TextBox)
                If txtIMNSName.Text = String.Empty Then
                    msg.Text = "Необходимо заполнить"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader
                sql = String.Format("select * from IMNS where imns_name like '%{0}%'", txtIMNSName.Text)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msg.Text = "Налоговая инспекция с таким наименованием уже есть."
                    Exit Sub
                End If
                reader.Close()
                sql = String.Format("INSERT INTO IMNS (imns_name) VALUES('{0}')", txtIMNSName.Text)
                dbSQL.Execute(sql)
            End If
        End Sub

        Private Sub grdIMNS_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIMNS.EditCommand
            grdIMNS.EditItemIndex = e.Item.ItemIndex
            Bind(txtFilter.Text)
        End Sub

        Private Sub grdIMNS_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIMNS.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from IMNS where sys_id={0}", grdIMNS.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)

            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            Finally
                Bind(txtFilter.Text)
            End Try
        End Sub

        Private Sub grdIMNS_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIMNS.CancelCommand
            grdIMNS.EditItemIndex = -1
            Bind(txtFilter.Text)
        End Sub

        Private Sub grdIMNS_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdIMNS.UpdateCommand
            Dim txtIMNSUpdateName As TextBox
            Dim sql$

            txtIMNSUpdateName = CType(e.Item.FindControl("txtIMNSUpdateName"), TextBox)
            Try
                sql = String.Format("update IMNS set imns_name='{0}' where sys_id={1}", txtIMNSUpdateName.Text, grdIMNS.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdIMNS_CancelCommand(source, e)
        End Sub

        Private Sub grdIMNS_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdIMNS.PageIndexChanged
            grdIMNS.CurrentPageIndex = e.NewPageIndex
            Bind(txtFilter.Text)
        End Sub

        Private Sub lnkShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkShow.Click
            grdIMNS.CurrentPageIndex = 0
            Bind(txtFilter.Text)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
