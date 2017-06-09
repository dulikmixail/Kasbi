Namespace Kasbi.Admin

    Partial Class Pricelist
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink

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
                calendarNew.SelectedDate = Now
                calendarNew.VisibleDate = Now
            End If
            btnSave.Attributes.Add("onclick", "if (confirm('Вы действительно хотите сохранить изменения?')){return confirm('Обновляемые данные будут потеряны');}else {return false};")
            btnDelete.Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить выделенный прейскурант?')){return confirm('Вся информация будет удалена. Продолжить?');}else {return false};")
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("get_pricelists", True)
                ds = New DataSet()
                adapt.Fill(ds)
                ds.Tables(0).DefaultView.Sort = "pricelist_sys_id desc"

                lstPricelist.DataSource = ds.Tables(0).DefaultView
                lstPricelist.DataTextField = "pricelist_name"
                lstPricelist.DataValueField = "pricelist_sys_id"
                lstPricelist.DataBind()

                adapt = dbSQL.GetDataAdapter("select good_type_sys_id, name good_name from good_type order by is_cashregister desc,good_name")
                ds = New DataSet()
                adapt.Fill(ds)
                grdGoodsNew.DataSource = ds.Tables(0).DefaultView
                grdGoodsNew.DataKeyField = "good_type_sys_id"
                grdGoodsNew.DataBind()

                chkEditMode_CheckedChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка загрузки списка прейскурантов!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub lstPricelist_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPricelist.SelectedIndexChanged

            Dim b As Boolean = chkEditMode.Checked
            grdGoods.ShowFooter = b
            grdGoods.Columns(grdGoods.Columns.Count - 1).Visible = b
            If lstPricelist.SelectedIndex = -1 Then b = False
            grdGoods.Visible = lstPricelist.SelectedIndex > -1
            pnlEdit.Visible = b

            If lstPricelist.SelectedIndex = -1 Then Exit Sub

            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            LoadPricelist()
            Try
                cmd = New SqlClient.SqlCommand("get_pricelist_detail")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                reader = dbSQL.GetReader(cmd)
                reader.Read()
                txtPricelistName.Text = reader("pricelist_name")
                Calendar.SelectedDate = reader("pricelist_date")
                Calendar.VisibleDate = reader("pricelist_date")

                If Left(txtPricelistName.Text, 8) = "Протокол" Then
                    chkSavePrice.Visible = True
                Else
                    chkSavePrice.Visible = False
                End If
                txtKoefficient.Text = "1.000"

                chkEditMode_CheckedChanged(Nothing, Nothing)

            Catch
                msg.Text = "Ошибка загрузки прейскуранта!<br>" & Err.Description
                Exit Sub
            Finally
                reader.Close()
            End Try
        End Sub

        Private Sub grdGoods_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoods.ItemDataBound
            If e.Item.ItemType = ListItemType.Footer Then
                Dim lst As DropDownList = CType(e.Item.FindControl("lstAddGood"), DropDownList)

                Dim cmd As SqlClient.SqlCommand
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet

                Try
                    cmd = New SqlClient.SqlCommand("get_free_goods_for_pricelist")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet()
                    adapt.Fill(ds)
                    lst.DataSource = ds.Tables(0).DefaultView
                    lst.DataTextField = "good_name"
                    lst.DataValueField = "good_type_sys_id"
                    lst.DataBind()
                    Dim b As Boolean = Not lst.Items.Count = 0
                    CType(e.Item.FindControl("txtAddGoodPrice"), TextBox).Enabled = b
                    CType(e.Item.FindControl("btnAddGood"), LinkButton).Enabled = b
                    lst.Enabled = b
                Catch
                    msg.Text = "Ошибка загрузки списка типов товаров!<br>" & Err.Description
                    Exit Sub
                End Try
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "return confirm('Вы действительно хотите удалить товар из прайслиста?');")
            End If
        End Sub

        Private Sub chkEditMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditMode.CheckedChanged
            Dim b As Boolean = chkEditMode.Checked
            grdGoods.ShowFooter = b
            grdGoods.Columns(grdGoods.Columns.Count - 1).Visible = b
            If lstPricelist.SelectedIndex = -1 Then b = False
            grdGoods.Visible = lstPricelist.SelectedIndex > -1
            pnlEdit.Visible = b
        End Sub

        Private Sub grdGoods_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.ItemCommand
            If e.CommandName = "AddGood" Then
                Dim cmd As SqlClient.SqlCommand
                Dim price As Long

                Try
                    price = CLng(CType(e.Item.FindControl("txtAddGoodPrice"), TextBox).Text)
                Catch
                    msg.Text = "Введите корректно стоимость товара!"
                    Exit Sub
                End Try

                Try
                    cmd = New SqlClient.SqlCommand("add_good_to_pricelist")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_good_type_sys_id", CType(e.Item.FindControl("lstAddGood"), DropDownList).SelectedItem.Value())
                    cmd.Parameters.AddWithValue("@pi_price", price)
                    Dim seq As Int16 = 0
                    cmd.Parameters.AddWithValue("@pi_seq", seq)
                    dbSQL.Execute(cmd)
                    lstPricelist_SelectedIndexChanged(Nothing, Nothing)
                Catch
                    msg.Text = "Ошибка добавления товара в прейскурант!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub grdGoods_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.DeleteCommand
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("remove_good_from_pricelist")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_good_type_sys_id", grdGoods.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(cmd)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            grdGoods.EditItemIndex = -1
            LoadPricelist()
        End Sub

        Private Sub grdGoods_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.EditCommand
            grdGoods.EditItemIndex = CInt(e.Item.ItemIndex)
            LoadPricelist()
        End Sub

        Private Sub grdGoods_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.UpdateCommand
            Dim price As Long

            Try
                price = CLng(CType(e.Item.FindControl("txtPrice"), TextBox).Text)
            Catch
                msg.Text = "Введите корректно стоимость товара!"
                Exit Sub
            End Try
            Try
                dbSQL.Execute("update pricelist set price=" & price & " where pricelist_sys_id=" & lstPricelist.SelectedItem.Value & " and good_type_sys_id=" & grdGoods.DataKeys(e.Item.ItemIndex))
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try
            grdGoods.EditItemIndex = -1
            LoadPricelist()
        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim cmd As SqlClient.SqlCommand

            If txtPricelistName.Text.Length = 0 Then
                msg.Text = "Задайте название прайслиста"
                Exit Sub
            End If
            Dim i% = lstPricelist.SelectedIndex

            Try
                cmd = New SqlClient.SqlCommand("update_pricelist")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_pricelist_date", Calendar.SelectedDate)
                cmd.Parameters.AddWithValue("@pi_pricelist_name", txtPricelistName.Text)
                dbSQL.Execute(cmd)
                Bind()
                lstPricelist.SelectedIndex = i
                chkEditMode_CheckedChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка добавления товара в прейскурант!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub LoadPricelist()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                cmd = New SqlClient.SqlCommand("get_pricelist")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet()
                adapt.Fill(ds)
                grdGoods.DataSource = ds.Tables(0).DefaultView
                grdGoods.DataKeyField = "good_type_sys_id"
                grdGoods.DataBind()
            Catch
                msg.Text = "Ошибка загрузки прейскуранта!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub grdGoods_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.CancelCommand
            grdGoods.EditItemIndex = -1
            LoadPricelist()
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Try
                dbSQL.Execute("update pricelist set deleted = 1 where pricelist_sys_id=" & lstPricelist.SelectedItem.Value)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            Bind()
        End Sub

        Private Sub btnArchived_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnArchived.Click
            Dim cmd As SqlClient.SqlCommand
            Dim i% = lstPricelist.SelectedIndex
            Try
                cmd = New SqlClient.SqlCommand("prc_pricelist_archived")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", lstPricelist.SelectedItem.Value)
                dbSQL.Execute(cmd)
                Bind()
                lstPricelist_SelectedIndexChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка архивирования прейскуранта!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub btnSavePricelist_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSavePricelist.Click
            Dim i%, sName$
            Dim price As Long
            Dim b As Boolean = False
            Dim chk As CheckBox

            sName = txtNewPricelistName.Text
            If sName.Length = 0 Then
                msgNew.Text = "Необходимо задать название прейскуранта или протокола"
                Exit Sub
            End If

            For i = 0 To grdGoodsNew.Items.Count - 1
                chk = grdGoodsNew.Items(i).FindControl("chkGoodType")
                If Not chk Is Nothing Then
                    If chk.Checked Then
                        b = True
                        Try
                            price = CLng(CType(grdGoodsNew.Items(i).FindControl("txtPriceNew"), TextBox).Text)
                        Catch
                            msgNew.Text = "Введите корректно стоимость в строке №" & i + 1
                            Exit Sub
                        End Try
                    End If
                End If
            Next

            If Not b Then
                msgNew.Text = "Необходимо задать хотя бы один товар"
                Exit Sub
            Else
                Dim cmd As SqlClient.SqlCommand
                Dim s$, sys_id%
                Dim d As Date = calendarNew.SelectedDate

                Try
                    Try
                        sys_id = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
                    Catch
                        sys_id = 1
                    End Try

                    s = "insert into pricelist (pricelist_sys_id, pricelist_date, pricelist_name, seq, good_type_sys_id, price) values (" & _
                        sys_id & ", dateadd(year," & d.Year - 1900 & ",dateadd(month," & d.Month - 1 & ",dateadd(day," & d.Day - 1 & ",0))), '" & sName & "', 1, "

                    For i = 0 To grdGoodsNew.Items.Count - 1
                        chk = grdGoodsNew.Items(i).FindControl("chkGoodType")
                        If Not chk Is Nothing Then
                            If chk.Checked Then
                                cmd = New SqlClient.SqlCommand(s & grdGoodsNew.DataKeys(i) & ", " & CType(grdGoodsNew.Items(i).FindControl("txtPriceNew"), TextBox).Text & ")")
                                dbSQL.Execute(cmd)
                            End If
                        End If
                    Next
                    Bind()
                    lstPricelist.Items.FindByValue(sys_id).Selected = True
                    lstPricelist_SelectedIndexChanged(Nothing, Nothing)
                Catch
                    msg.Text = "Ошибка обновления записи!<br>" & Err.Description
                End Try
            End If
        End Sub

        Protected Sub btnKoefficient_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnKoefficient.Click
            Dim i = 0
            Dim cmd
            Dim price_name
            Dim price2
            Dim good_id = 0, good_id_in
            Dim query
            Dim reader As SqlClient.SqlDataReader

            'Определяем id следующего прейскуранта
            Dim sys_id = dbSQL.ExecuteScalar("select max(pricelist_sys_id)+1 from pricelist")
            'Определяем дату
            Dim price_date As Date = dbSQL.ExecuteScalar("select pricelist_date from pricelist where pricelist_sys_id=" & lstPricelist.SelectedItem.Value & "")
            Dim price_date2 As Date = dbSQL.ExecuteScalar("select d from pricelist where pricelist_sys_id=" & lstPricelist.SelectedItem.Value & "")
            price_date = Now
            price_date2 = Now

            'Определяем количество элементов прайса
            Dim price_count = dbSQL.ExecuteScalar("select COUNT(*) from pricelist where pricelist_sys_id=" & lstPricelist.SelectedItem.Value & " AND deleted=0")
            'MsgBox(lstPricelist.SelectedItem.Value)

            If chkSavePrice.Checked = True Then
                For i = 1 To price_count Step 1
                    query = "select top 1 good_type_sys_id, price from pricelist where good_type_sys_id not in (" & good_id.ToString.Trim(",") & ") AND pricelist_sys_id=" & lstPricelist.SelectedItem.Value & ""
                    'MsgBox(query)
                    reader = dbSQL.GetReader(query)

                    If reader.Read() Then
                        good_id_in = reader.Item(0)

                        price2 = reader.Item(1)
                        price2 = price2 * Int(Replace(txtKoefficient.Text, ".", ","))
                        price_name = txtPricelistName.Text
                        price_name = Replace(price_name, "Протокол", "Прейскурант")

                        'MsgBox(good_id_in & ":" & price2)
                        good_id = good_id & good_id_in & ","

                        reader.Close()

                        cmd = "INSERT INTO pricelist VALUES ('" & sys_id & "', '" & good_id_in & "', '" & price_date & "', '" & price2 & "', '" & price_name & "', '1', '0', '" & price_date2 & "', '0')"
                        dbSQL.Execute(cmd)
                    End If
                Next
                Bind()
            Else
                dbSQL.Execute("update pricelist set price=price*" & txtKoefficient.Text & " where pricelist_sys_id=" & lstPricelist.SelectedItem.Value)
                LoadPricelist()
            End If
            txtKoefficient.Text = "1.000"
        End Sub


    End Class

End Namespace
