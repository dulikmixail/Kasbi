Namespace Kasbi.Admin
    Partial Class BankList
        Inherits PageBase
        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind(Optional ByVal filterCode As String = "", Optional ByVal filterName As String = "")
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            Try
                cmd = New SqlClient.SqlCommand("prc_getBankList")
                cmd.CommandType = CommandType.StoredProcedure
                If Not filterCode Is Nothing AndAlso filterCode <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_bank_code_filter", filterCode)
                End If
                If Not filterName Is Nothing AndAlso filterName <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_bank_name_filter", filterName)
                End If
                adapter = dbSQL.GetDataAdapter(cmd)
                adapter.Fill(ds)
                grdBankList.DataSource = ds.Tables(0).DefaultView
                grdBankList.DataKeyField = "bank_sys_id"
                grdBankList.DataBind()
            Catch
                msg.Text = "Ошибка загрузки списка банков !<br>" & Err.Description
            End Try

        End Sub

        Private Sub grdBankList_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdBankList.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdBankList_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBankList.CancelCommand
            grdBankList.EditItemIndex = -1
            Bind(txtFilterByCode.Text, txtFilterByName.Text)
        End Sub

        Private Sub grdBankList_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBankList.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from bank where bank_sys_id={0}", grdBankList.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Response.Redirect(GetAbsoluteUrl("~/Admin/BankList.aspx"))
        End Sub

        Private Sub grdBankList_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBankList.UpdateCommand
            Dim sql$

            Dim txtBankCode As TextBox = CType(e.Item.FindControl("txtBankCodeEdit"), TextBox)
            Dim txtBankName As TextBox = CType(e.Item.FindControl("txtBankNameEdit"), TextBox)
            Dim txtBankAddress As TextBox = CType(e.Item.FindControl("txtBankAddressEdit"), TextBox)
            Dim txtBankMFO As TextBox = CType(e.Item.FindControl("txtBankMFOEdit"), TextBox)
            Dim txtBankUNN As TextBox = CType(e.Item.FindControl("txtBankUNNEdit"), TextBox)
            Dim txtBankPhone As TextBox = CType(e.Item.FindControl("txtBankPhoneEdit"), TextBox)
            Dim txtBankFax As TextBox = CType(e.Item.FindControl("txtBankFaxEdit"), TextBox)
            If txtBankCode.Text.Trim = String.Empty Then
                msg.Text = "Неодходимо заполнить код банка"
                Exit Sub
            End If
            If txtBankName.Text.Trim = String.Empty Then
                msg.Text = "Неодходимо заполнить название банка"
                Exit Sub
            End If
            If txtBankAddress.Text.Trim = String.Empty Then
                msg.Text = "Неодходимо заполнить адрес банка"
                Exit Sub
            End If
            Try
                sql = String.Format("update bank set bank_code='{0}', name='{1}', address='{2}', mfo='{3}', unn='{4}' , phone='{5}',fax='{6}' where bank_sys_id = {7}", txtBankCode.Text.Trim, txtBankName.Text.Replace("'", """"), txtBankAddress.Text.Replace("'", """"), txtBankMFO.Text.Trim, txtBankUNN.Text.Trim, txtBankPhone.Text.Trim, txtBankFax.Text.Trim, grdBankList.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdBankList_CancelCommand(source, e)
        End Sub

        Private Sub grdBankList_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBankList.EditCommand
            grdBankList.EditItemIndex = e.Item.ItemIndex
            Bind(txtFilterByCode.Text, txtFilterByName.Text)
        End Sub

        Private Sub grdBankList_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdBankList.PageIndexChanged
            grdBankList.CurrentPageIndex = e.NewPageIndex
            Bind(txtFilterByCode.Text, txtFilterByName.Text)
        End Sub

        Private Sub grdBankList_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdBankList.ItemCommand
            If e.CommandName = "AddBank" Then

                Dim txtBankCode As TextBox = CType(e.Item.FindControl("txtBankCodeNew"), TextBox)
                Dim txtBankName As TextBox = CType(e.Item.FindControl("txtBankNameNew"), TextBox)
                Dim txtBankAddress As TextBox = CType(e.Item.FindControl("txtBankAddressNew"), TextBox)
                Dim txtBankMFO As TextBox = CType(e.Item.FindControl("txtBankMFONew"), TextBox)
                Dim txtBankUNN As TextBox = CType(e.Item.FindControl("txtBankUNNNew"), TextBox)
                Dim txtBankPhone As TextBox = CType(e.Item.FindControl("txtBankPhoneNew"), TextBox)
                Dim txtBankFax As TextBox = CType(e.Item.FindControl("txtBankFaxNew"), TextBox)

                If txtBankCode.Text.Trim = String.Empty Then
                    msg.Text = "Неодходимо заполнить код банка"
                    Exit Sub
                End If
                If txtBankName.Text.Trim = String.Empty Then
                    msg.Text = "Неодходимо заполнить название банка"
                    Exit Sub
                End If
                If txtBankAddress.Text.Trim = String.Empty Then
                    msg.Text = "Неодходимо заполнить адрес банка"
                    Exit Sub
                End If
                Try
                    Dim sql$ = String.Format("INSERT INTO bank (bank_code, name, address, mfo,unn, phone, fax) VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}')", txtBankCode.Text.Trim, txtBankName.Text.Replace("'", """"), txtBankAddress.Text.Replace("'", """"), txtBankMFO.Text.Trim, txtBankUNN.Text.Trim, txtBankPhone.Text.Trim, txtBankFax.Text.Trim)
                    dbSQL.Execute(sql)
                Catch
                    msg.Text = "Ошибка сохранения информации о банке !<br>" & Err.Description
                Finally
                    Bind()
                End Try
            End If
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

        Protected Sub lnkShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShow.Click
            grdBankList.CurrentPageIndex = 0
            Bind(txtFilterByCode.Text, txtFilterByName.Text)
        End Sub
    End Class
End Namespace

