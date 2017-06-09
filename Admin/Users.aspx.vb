Namespace Kasbi.Admin

    Partial Class Users
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents Hyperlink1 As System.Web.UI.WebControls.HyperLink


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
            btnNew.Visible = CurrentUser.is_admin
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select * from employee left outer join roles on employee.role_id=roles.role_id order by Name")
                ds = New DataSet
                adapt.Fill(ds)
                grdUsers.DataSource = ds.Tables(0).DefaultView
                grdUsers.DataKeyField = "sys_id"
                grdUsers.DataBind()
            Catch
                msgEmployee.Text = "Ошибка загрузки информации о ЦТО!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
                Dim s$ = ""
                If Not IsDBNull(e.Item.DataItem("inactive")) Then
                    s = IIf(e.Item.DataItem("inactive") = False, "", "x")
                End If
                CType(e.Item.FindControl("lblInactive"), Label).Text = s
            End If

        End Sub

        Private Sub grdUsers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUsers.ItemCommand
            If e.CommandName = "Delete" Then
                DeleteUser(grdUsers.DataKeys(e.Item.ItemIndex))
            ElseIf e.CommandName = "Edit" Then
                Response.Redirect(GetAbsoluteUrl("~/Admin/UserDetail.aspx?UserDetailID=" & grdUsers.DataKeys(e.Item.ItemIndex)))
            End If
        End Sub

        Private Sub DeleteUser(ByVal user_id As Integer)
            Try
                Dim sql$ = String.Format("delete from employee where sys_id = {0}", user_id)
                dbSQL.Execute(sql)
                Response.Redirect(GetAbsoluteUrl("~/Admin/Users.aspx"))
            Catch
            End Try

        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
