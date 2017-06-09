Namespace Kasbi.Admin

    Partial Class RegionPlace
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
                adapt = dbSQL.GetDataAdapter("select * from Place_Rn order by name")
                ds = New DataSet
                adapt.Fill(ds)
                grdRegionPlace.DataSource = ds.Tables(0).DefaultView
                grdRegionPlace.DataKeyField = "place_rn_id"
                grdRegionPlace.DataBind()
            Catch
                msgError.Text = "Ошибка загрузки информации о деталях и работах!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdRegionPlace_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRegionPlace.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdRegionPlace_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRegionPlace.ItemCommand
            If e.CommandName = "AddRegionPlace" Then
                Dim txtRegionPlaceName As TextBox = CType(e.Item.FindControl("txtRegionPlaceName"), TextBox)
                If txtRegionPlaceName.Text = String.Empty Then
                    msgError.Text = "Необходимо заполнить"
                    Exit Sub
                End If
                Dim sql As String
                Dim reader As SqlClient.SqlDataReader

                sql = String.Format("select * from Place_Rn where name like '%{0}%'", txtRegionPlaceName.Text)
                reader = dbSQL.GetReader(sql)
                If reader.Read() Then
                    msgError.Text = "Район с таким наименованием уже есть."
                    Exit Sub
                End If
                reader.Close()
                Try
                    sql = String.Format("INSERT INTO Place_Rn (name) VALUES('{0}')", txtRegionPlaceName.Text)
                    dbSQL.Execute(sql)
                    Bind()
                Catch
                    msgError.Text = "Ошибка вставки нового района установки ККМ!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub grdRegionPlace_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRegionPlace.EditCommand
            grdRegionPlace.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdIMNS_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRegionPlace.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from Place_Rn where place_rn_id={0}", grdRegionPlace.DataKeys(e.Item.ItemIndex))
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

        Private Sub grdRegionPlace_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRegionPlace.CancelCommand
            grdRegionPlace.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdRegionPlace_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdRegionPlace.UpdateCommand
            Dim txtRegionPlaceUpdateName As TextBox
            Dim sql$
            txtRegionPlaceUpdateName = CType(e.Item.FindControl("txtRegionPlaceUpdateName"), TextBox)
            Try
                sql = String.Format("update Place_Rn set name='{0}' where place_rn_id={1}", txtRegionPlaceUpdateName.Text, grdRegionPlace.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msgError.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdRegionPlace.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdRegionPlace_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdRegionPlace.PageIndexChanged
            grdRegionPlace.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

    End Class

End Namespace
