Namespace Kasbi.Admin

    Partial Class Delivery
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Label1 As System.Web.UI.WebControls.Label
        Protected WithEvents Label2 As System.Web.UI.WebControls.Label

        Const ClearString = "-------"
        Private UnitList As ListControl

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
                txtNewInfo.Text = ""
            End If
            btnSave.Attributes.Add("onclick", "if (confirm('Вы действительно хотите сохранить изменения?')){return confirm('Обновляемые данные будут потеряны');}else {return false};")
            btnDelete.Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить выделенную поставку?')){return confirm('Вся информация будет удалена. Продолжить?');}else {return false};")
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("get_deliveries", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstDelivery.DataSource = ds.Tables(0).DefaultView
                lstDelivery.DataBind()

                adapt = dbSQL.GetDataAdapter("select good_type_sys_id, name good_name from good_type order by good_name")
                ds = New DataSet
                adapt.Fill(ds)
                Dim row As DataRow = ds.Tables(0).NewRow()
                row(0) = 0
                row(1) = "Добавить новый тип товара"
                ds.Tables(0).Rows.Add(row)
                grdGoodsNew.DataSource = ds.Tables(0).DefaultView
                'grdGoodsNew.DataBind()

                chkEditMode_CheckedChanged(Nothing, Nothing)
                If lstDelivery.Items.Count > 0 Then lstDelivery.SelectedIndex = 0 : lstDelivery_SelectedIndexChanged(Me, Nothing)
            Catch
                msg.Text = "Ошибка загрузки списка поставок!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub lstDelivery_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstDelivery.SelectedIndexChanged

            If lstDelivery.SelectedIndex = -1 Then Exit Sub

            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            LoadDelivery()
            Try
                cmd = New SqlClient.SqlCommand("get_delivery_detail")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                reader = dbSQL.GetReader(cmd)
                reader.Read()
                txtDeliveryInfo.Text = IIf(IsDBNull(reader("info")), "", reader("info"))
                lblDeliveryInfo.Text = "Поставка от <b>" & String.Format("{0:dd/MM/yyyy.&nb\sp;&nb\sp;}", reader("delivery_date")) & IIf(txtDeliveryInfo.Text.Length = 0, "", "</b>Доп. информация: <b>" & txtDeliveryInfo.Text & "</b>")
                Calendar.SelectedDate = reader("delivery_date")
                Calendar.VisibleDate = reader("delivery_date")

                chkEditMode_CheckedChanged(Nothing, Nothing)

            Catch
                msg.Text = "Ошибка загрузки информации о поставке!<br>" & Err.Description
                Exit Sub
            Finally
                reader.Close()
            End Try
        End Sub

        Sub BindSupplierList(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select sys_id,supplier_abr+' '+supplier_name supplier_name from supplier order by supplier_name")
                ds = New DataSet
                adapt.Fill(ds)
                With lst
                    .DataSource = ds.Tables(0).DefaultView
                    .DataTextField = "supplier_name"
                    .DataValueField = "sys_id"
                    .DataBind()
                    .Items.Insert(0, New ListItem(ClearString, ClearString))
                    Try
                        .Items.FindByValue(s).Selected = True
                    Catch ex As Exception

                    End Try
                End With

            Catch
                msg.Text = "Ошибка формирования списка поставщиков!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub grdGoods_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoods.ItemDataBound
            If e.Item.ItemType = ListItemType.Footer Then
                Dim lst As DropDownList = CType(e.Item.FindControl("lstAddGood"), DropDownList)
                Dim cmd As SqlClient.SqlCommand
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet

                Try
                    cmd = New SqlClient.SqlCommand("get_free_goods_for_delivery")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)
                    lst.DataSource = ds.Tables(0).DefaultView
                    lst.DataTextField = "good_name"
                    lst.DataValueField = "good_type_sys_id"
                    lst.DataBind()

                    Dim b As Boolean = Not lst.Items.Count = 0
                    CType(e.Item.FindControl("txtAddGoodPrice"), TextBox).Enabled = b
                    CType(e.Item.FindControl("btnAddGood"), LinkButton).Enabled = b
                    lst.Enabled = b

                    AddUnits(e.Item.FindControl("lstNewUnits"), 0)
                Catch
                    msg.Text = "Ошибка загрузки списка типов товаров!<br>" & Err.Description
                    Exit Sub
                End Try

                BindSupplierList(CType(e.Item.FindControl("lstSupplier"), DropDownList), ClearString)
            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "return confirm('Вы действительно хотите удалить товар из поставки?');")

            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                If IsDBNull(e.Item.DataItem("supplier_id")) Then
                    BindSupplierList(CType(e.Item.FindControl("lstSupplierEdit"), DropDownList), ClearString)
                Else
                    BindSupplierList(CType(e.Item.FindControl("lstSupplierEdit"), DropDownList), e.Item.DataItem("supplier_id"))
                End If
                If (IsDBNull(e.Item.DataItem("unit_id"))) Then
                    AddUnits(e.Item.FindControl("lstUnits"), -1)
                Else
                    AddUnits(e.Item.FindControl("lstUnits"), CInt(e.Item.DataItem("unit_id")))
                End If
            End If
        End Sub

        Private Sub AddUnits(ByVal list As DropDownList, ByVal unit_id As Integer)
            If UnitList Is Nothing Then loadUnits()

            list.DataSource = UnitList.DataSource
            list.DataValueField = "unit_id"
            list.DataTextField = "UnitDesciption"
            list.DataBind()

            If unit_id > 0 Then
                list.Items.FindByValue(unit_id.ToString()).Selected = True
            End If
        End Sub

        Private Sub chkEditMode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkEditMode.CheckedChanged
            Dim b As Boolean = chkEditMode.Checked
            grdGoods.ShowFooter = b
            grdGoods.Columns(grdGoods.Columns.Count - 1).Visible = b
            If lstDelivery.SelectedIndex = -1 Then b = False
            grdGoods.Visible = lstDelivery.SelectedIndex > -1
            pnlEdit.Visible = b
            lblDeliveryInfo.Visible = (lblDeliveryInfo.Text.Length > 0) And Not b
        End Sub

        Private Sub grdGoods_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.ItemCommand

            If e.CommandName = "AddGood" Then
                Dim cmd As SqlClient.SqlCommand
                Dim price As Double, rur As Double, quantity As Integer
                Dim iGoodTypeSysId As Integer
                If Not CheckPricesAndQuantity(CType(e.Item.FindControl("txtAddGoodPrice"), TextBox).Text.Replace(",", "."), _
                                              CType(e.Item.FindControl("txtAddGoodRur"), TextBox).Text.Replace(",", "."), _
                                              CType(e.Item.FindControl("txtAddGoodQuantity"), TextBox).Text, _
                                              price, rur, quantity) Then
                    Exit Sub
                End If
                Try
                    If CType(e.Item.FindControl("tbxNewGood"), TextBox).Text.Trim() <> "" Then
                        cmd = New SqlClient.SqlCommand("new_good_type")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_name", CType(e.Item.FindControl("tbxNewGood"), TextBox).Text)
                        cmd.Parameters.AddWithValue("@pi_nadbavka", 0)
                        cmd.Parameters.AddWithValue("@pi_is_cashregister", IIf(CType(e.Item.FindControl("chkNewCashRegister"), CheckBox).Checked, 1, 0))
                        dbSQL.Execute(cmd)
                        iGoodTypeSysId = dbSQL.ExecuteScalar("select max(good_type_sys_id) from good_type")
                    Else
                        iGoodTypeSysId = CType(e.Item.FindControl("lstAddGood"), DropDownList).SelectedItem.Value()
                    End If

                    cmd = New SqlClient.SqlCommand("add_good_to_delivery")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_price", price)
                    cmd.Parameters.AddWithValue("@pi_rur", rur)
                    cmd.Parameters.AddWithValue("@pi_quantity", quantity)
                    cmd.Parameters.AddWithValue("@pi_info", CType(e.Item.FindControl("txtAddGoodInfo"), TextBox).Text)
                    cmd.Parameters.AddWithValue("@pi_good_type_sys_id", iGoodTypeSysId)
                    If CType(e.Item.FindControl("lstSupplier"), DropDownList).SelectedItem.Value <> ClearString Then
                        cmd.Parameters.AddWithValue("@pi_supplier_id", CType(e.Item.FindControl("lstSupplier"), DropDownList).SelectedItem.Value)
                    End If
                    cmd.Parameters.AddWithValue("@pi_unit_id", CType(e.Item.FindControl("lstNewUnits"), DropDownList).SelectedValue)
                    cmd.Parameters.AddWithValue("@pi_artikul", CType(e.Item.FindControl("txtArtikul"), TextBox).Text)
                    dbSQL.Execute(cmd)
                    lstDelivery_SelectedIndexChanged(Nothing, Nothing)
                Catch
                    msg.Text = "Ошибка добавления товара в поставку!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub grdGoods_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.DeleteCommand
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("remove_good_from_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
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
            LoadDelivery()
        End Sub

        Private Sub grdGoods_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.EditCommand
            grdGoods.EditItemIndex = CInt(e.Item.ItemIndex)
            LoadDelivery()
        End Sub

        Private Sub grdGoods_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.UpdateCommand
            Dim cmd As SqlClient.SqlCommand

            Dim price As Double, rur As Double, quantity As Integer
            If Not CheckPricesAndQuantity(CType(e.Item.FindControl("txtPrice"), TextBox).Text.Replace(",", "."), _
                                          CType(e.Item.FindControl("txtRur"), TextBox).Text.Replace(",", "."), _
                                          CType(e.Item.FindControl("txtQuantity"), TextBox).Text, _
                                          price, rur, quantity) Then
                Exit Sub
            End If

            Try
                cmd = New SqlClient.SqlCommand("add_good_to_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_good_type_sys_id", grdGoods.DataKeys(e.Item.ItemIndex))
                cmd.Parameters.AddWithValue("@pi_price", price)
                cmd.Parameters.AddWithValue("@pi_rur", rur)
                cmd.Parameters.AddWithValue("@pi_quantity", quantity)
                cmd.Parameters.AddWithValue("@pi_info", CType(e.Item.FindControl("txtInfo"), TextBox).Text)
                If CType(e.Item.FindControl("lstSupplierEdit"), DropDownList).SelectedItem.Value <> ClearString Then
                    cmd.Parameters.AddWithValue("@pi_supplier_id", CType(e.Item.FindControl("lstSupplierEdit"), DropDownList).SelectedItem.Value)
                End If
                cmd.Parameters.AddWithValue("@pi_unit_id", CType(e.Item.FindControl("lstUnits"), DropDownList).SelectedValue)
                cmd.Parameters.AddWithValue("@pi_artikul", CType(e.Item.FindControl("txtArtikul"), TextBox).Text)

                dbSQL.Execute(cmd)
                lstDelivery_SelectedIndexChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка обновления записи!<br>" & Err.Description
            End Try

            grdGoods.EditItemIndex = -1
            LoadDelivery()
        End Sub

        Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
            Dim cmd As SqlClient.SqlCommand
            Dim i% = lstDelivery.SelectedIndex
            Try
                cmd = New SqlClient.SqlCommand("update_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_delivery_date", Calendar.SelectedDate)
                cmd.Parameters.AddWithValue("@pi_info", txtDeliveryInfo.Text)
                dbSQL.Execute(cmd)
                Bind()
                lstDelivery.SelectedIndex = i
                chkEditMode_CheckedChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка обновления информации о поставке!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub LoadDelivery()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                cmd = New SqlClient.SqlCommand("get_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                grdGoods.DataSource = ds.Tables(0).DefaultView
                grdGoods.DataBind()
            Catch
                msg.Text = "Ошибка загрузки поставки!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub grdGoods_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdGoods.CancelCommand
            grdGoods.EditItemIndex = -1
            LoadDelivery()
        End Sub

        Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("remove_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstDelivery.SelectedItem.Value)
                dbSQL.Execute(cmd)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "Выбранную запись нельзя удалить!"
                Else
                    msg.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            chkEditMode.Checked = False
            chkEditMode_CheckedChanged(Nothing, Nothing)
            Bind()
        End Sub

        Private Sub btnSaveDelivery_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSaveDelivery.Click
            Dim i%, sName$
            Dim price As Double, rur As Double, quantity, units As Integer
            Dim b As Boolean = False
            Dim chk As CheckBox

            sName = txtNewInfo.Text

            For i = 0 To grdGoodsNew.Items.Count - 1
                chk = grdGoodsNew.Items(i).FindControl("chkGoodType")
                If Not chk Is Nothing AndAlso chk.Checked Then
                    If Not CheckPricesAndQuantity(CType(grdGoodsNew.Items(i).FindControl("txtPriceNew"), TextBox).Text.Replace(",", ".").Trim(), _
                                                  CType(grdGoodsNew.Items(i).FindControl("txtPriceRurNew"), TextBox).Text.Replace(",", ".").Trim(), _
                                                  CType(grdGoodsNew.Items(i).FindControl("txtQuantityNew"), TextBox).Text.Replace(",", ".").Trim(), _
                                                  price, rur, quantity, i + 1) Then
                        Exit Sub
                    End If
                    b = True
                End If
            Next

            'If Not b Then
            'msgNew.Text = "Необходимо задать хотя бы один товар"
            'Exit Sub
            'Else
            Dim cmd As SqlClient.SqlCommand
            Dim sys_id%
            Dim d As Date = calendarNew.SelectedDate

            Try
                Try
                    sys_id = dbSQL.ExecuteScalar("select max(delivery_sys_id)+1 from delivery")
                Catch
                    sys_id = 1
                End Try
                cmd = New SqlClient.SqlCommand("insert into delivery (delivery_sys_id, delivery_date, info) " & _
                                               "values(" & sys_id & ", dateadd(year," & d.Year - 1900 & ",dateadd(month," & d.Month - 1 & ",dateadd(day," & d.Day - 1 & ",0))), '" & txtNewInfo.Text & "')")
                dbSQL.Execute(cmd)

                For i = 0 To grdGoodsNew.Items.Count - 1
                    chk = grdGoodsNew.Items(i).FindControl("chkGoodType")
                    If Not chk Is Nothing Then
                        If chk.Checked Then
                            Dim iGoodTypeSysId% = grdGoodsNew.DataKeys(i)
                            If i = grdGoodsNew.Items.Count - 1 Then
                                Dim sNewGood$ = CType(grdGoodsNew.Items(i).FindControl("txtNewGoodType"), TextBox).Text.Replace("'", """")
                                If sNewGood.Length > 0 Then
                                    cmd = New SqlClient.SqlCommand("new_good_type")
                                    cmd.CommandType = CommandType.StoredProcedure
                                    cmd.Parameters.AddWithValue("@pi_name", sNewGood)
                                    cmd.Parameters.AddWithValue("@pi_nadbavka", 0) 'IIf(CType(grdGoodsNew.Items(i).FindControl("chkNewGoodType"), CheckBox).Checked, 1, 0))
                                    cmd.Parameters.AddWithValue("@pi_is_cashregister", IIf(CType(grdGoodsNew.Items(i).FindControl("chkNewCashReg"), CheckBox).Checked, 1, 0))
                                    dbSQL.Execute(cmd)
                                    Try
                                        iGoodTypeSysId = dbSQL.ExecuteScalar("select max(good_type_sys_id) from good_type")
                                    Catch
                                        iGoodTypeSysId = 1
                                    End Try
                                End If
                            End If
                            Dim ss$ = CType(grdGoodsNew.Items(i).FindControl("txtPriceNew"), TextBox).Text.Replace(",", ".").Trim()
                            If ss.Length > 0 Then : price = CDbl(ss)
                            Else : price = 0
                            End If
                            ss$ = CType(grdGoodsNew.Items(i).FindControl("txtPriceRurNew"), TextBox).Text.Replace(",", ".").Trim()
                            If ss.Length > 0 Then : rur = CDbl(ss)
                            Else : rur = 0
                            End If
                            quantity = CDbl(CType(grdGoodsNew.Items(i).FindControl("txtQuantityNew"), TextBox).Text.Replace(",", ".").Trim())
                            If CType(grdGoodsNew.Items(i).FindControl("lstUnitsNew"), DropDownList).Items.Count > 0 Then
                                units = CInt(CType(grdGoodsNew.Items(i).FindControl("lstUnitsNew"), DropDownList).SelectedValue)
                            End If
                            'Добавляем товар в поставку
                            cmd = New SqlClient.SqlCommand("add_good_to_delivery")
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@pi_delivery_sys_id", sys_id)
                            cmd.Parameters.AddWithValue("@pi_good_type_sys_id", iGoodTypeSysId)
                            cmd.Parameters.AddWithValue("@pi_price", price)
                            cmd.Parameters.AddWithValue("@pi_rur", rur)
                            cmd.Parameters.AddWithValue("@pi_quantity", quantity)
                            cmd.Parameters.AddWithValue("@pi_unit_id", IIf(units = 0, DBNull.Value, units))
                            cmd.Parameters.AddWithValue("@pi_info", CType(grdGoodsNew.Items(i).FindControl("txtInfoNew"), TextBox).Text)
                            If CType(grdGoodsNew.Items(i).FindControl("lstSupplierNew"), DropDownList).SelectedItem.Value <> ClearString Then
                                cmd.Parameters.AddWithValue("@pi_supplier_id", CType(grdGoodsNew.Items(i).FindControl("lstSupplierNew"), DropDownList).SelectedItem.Value)
                            End If
                            dbSQL.Execute(cmd)
                        End If
                    End If
                Next


                chkEditMode.Checked = False
                chkEditMode_CheckedChanged(Nothing, Nothing)
                Bind()
                lstDelivery.SelectedIndex = -1
                lstDelivery.Items.FindByValue(sys_id).Selected = True
                lstDelivery_SelectedIndexChanged(Nothing, Nothing)
            Catch
                msg.Text = "Ошибка добавления записи!<br>" & Err.Description
            End Try
            'End If
        End Sub

        Function CheckPricesAndQuantity(ByVal sPrice As String, ByVal sRur As String, ByVal sQuantity As String, ByRef price As Double, ByRef rur As Double, ByRef quantity As Integer, Optional ByVal i As Integer = -1) As Boolean
            CheckPricesAndQuantity = False
            Dim sError$ = ""

            If sPrice.Length = 0 And sRur.Length = 0 Then
                sError = "Введите стоимость товара" & IIf(i > -1, " в строке " & i, "") & "!"
                GoTo ExitFunction
            End If

            Try
                If sPrice.Length = 0 Then
                    price = 0
                Else
                    price = CDbl(sPrice)
                End If
            Catch
                sError = "Введите корректно стоимость товара" & IIf(i > -1, " в строке " & i, "") & "!"
                GoTo ExitFunction
            End Try

            Try
                If sRur.Length = 0 Then
                    rur = 0
                Else
                    rur = CDbl(sRur)
                End If
            Catch
                sError = "Введите корректно стоимость товара в российских рублях" & IIf(i > -1, " в строке " & i, "") & "!"
                GoTo ExitFunction
            End Try

            Try
                quantity = CInt(sQuantity)
            Catch
                sError = "Введите корректно количество товара" & IIf(i > -1, " в строке " & i, "") & "!"
                GoTo ExitFunction
            End Try

            CheckPricesAndQuantity = True

ExitFunction:
            If i > -1 Then
                msgNew.Text = sError
            Else
                msg.Text = sError
            End If

        End Function

        Private Sub grdGoodsNew_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoodsNew.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.DataItem(0) = 0 Then
                    CType(e.Item.FindControl("txtNewGoodType"), TextBox).Visible = True
                    ' CType(e.Item.FindControl("chkNewGoodType"), CheckBox).Visible = True
                    CType(e.Item.FindControl("chkNewCashReg"), CheckBox).Visible = True
                End If
                BindSupplierList(CType(e.Item.FindControl("lstSupplierNew"), DropDownList), ClearString)
                AddUnits(e.Item.FindControl("lstUnitsNew"), -1)
            End If
        End Sub

        Private Sub loadUnits()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from Units")
                ds = New DataSet
                adapt.Fill(ds)
                UnitList = New ListBox
                UnitList.DataSource = ds.Tables(0).DefaultView
                UnitList.DataValueField = "unit_id"
                UnitList.DataTextField = "UnitDesciption"
                UnitList.DataBind()
            Catch
                msg.Text = "Ошибка загрузки информармации о списке единиц измерения!<br>" & Err.Description
            End Try
        End Sub

    End Class
End Namespace
