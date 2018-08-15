Imports Exeption
Imports Service

Namespace Kasbi

    Partial Class NewGood
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblCashAll As System.Web.UI.WebControls.Label
        Protected WithEvents lblCashRest As System.Web.UI.WebControls.Label
        Dim sum, rest, outside As Integer
        Private ReadOnly _serviceGood As ServiceGood = New ServiceGood()

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
                txtCTO2.Visible = False
                'txtCP.Visible = False
                lblCTO2.Visible = False
                'lblCP.Visible = False
                rbtnKKM_Type_SelectedIndexChanged(Me, Nothing)
            End If
            LoadStatistic()
            If tbxBeginDate.Text = "" Then
                tbxBeginDate.Text = Format(Now, "dd.MM.yyyy")
            End If
        End Sub

        Sub LoadStatistic()
            Dim adapt As SqlClient.SqlDataAdapter
            Try
                adapt = dbSQL.GetDataAdapter("get_cash_info_by_type", True)
                Dim ds As DataSet = New DataSet
                adapt.Fill(ds)
                sum = 0
                rest = 0
                repGoodStatistic.DataSource = ds
                repGoodStatistic.DataBind()
            Catch
                msg.Text = "Ошибка получения информации о кассовых аппаратах!<br>" & Err.Description
            End Try
        End Sub

        Sub BindDeliveryList()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("get_delivery_to_filling", True)
                ds = New DataSet
                adapt.Fill(ds)
                Dim i%
                For i = 0 To ds.Tables(0).Rows.Count - 1
                    lstGoodDelivery.Items.Add(New ListItem(GetRussianDate(ds.Tables(0).Rows(i)("delivery_date")), ds.Tables(0).Rows(i)("delivery_sys_id")))
                Next

                adapt = dbSQL.GetDataAdapter("get_salers", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstWorker.DataSource = ds
                lstWorker.DataTextField = "name"
                lstWorker.DataValueField = "sys_id"
                lstWorker.DataBind()
                Try
                    lstWorker.Items.FindByValue(CurrentUser.sys_id).Selected = True
                Catch
                End Try
            Catch
                msgAddGood.Text = "Ошибка формирования списков!<br>" & Err.Description
                Exit Sub
            End Try
            If lstGoodDelivery.Items.Count > 0 Then
                lstGoodDelivery_SelectedIndexChanged(Me, Nothing)
            End If

        End Sub

        Sub BindFreeKKMList()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("get_goods_is_cashregister", True)
                ds = New DataSet
                adapt.Fill(ds)

                lstGoodType.DataSource = ds.Tables(0).DefaultView
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
                If lstGoodType.Items.Count > 0 Then
                    lstGoodType_SelectedIndexChanged(Me, Nothing)
                End If

                adapt = dbSQL.GetDataAdapter("get_salers", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstWorker.DataSource = ds
                lstWorker.DataTextField = "name"
                lstWorker.DataValueField = "sys_id"
                lstWorker.DataBind()
                Try
                    lstWorker.Items.FindByValue(CurrentUser.sys_id).Selected = True
                Catch
                End Try
            Catch
                msgAddGood.Text = "Ошибка формирования списков!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub lstGoodDelivery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGoodDelivery.SelectedIndexChanged
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Dim delivery%
            delivery = lstGoodDelivery.SelectedItem.Value
            Try
                cmd = New SqlClient.SqlCommand("get_good_type_by_delivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", delivery)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                lstGoodType.DataSource = ds.Tables(0).DefaultView
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
                lstGoodType_SelectedIndexChanged(Me, Nothing)
            Catch
                msgAddGood.Text = "Ошибка формирования списка типов товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            Dim cmd As SqlClient.SqlCommand
            Try
                _serviceGood.CheckNumbers(txtGoodNumCashregister.Text.Trim, NumbersCashRegister.Number, lstGoodType.SelectedItem.Value)
                _serviceGood.CheckNumbers(txtReestr.Text.Trim, NumbersCashRegister.Register, lstGoodType.SelectedItem.Value)
                _serviceGood.CheckNumbers(txtPZU.Text.Trim, NumbersCashRegister.ROM, lstGoodType.SelectedItem.Value)
                _serviceGood.CheckNumbers(txtMFP.Text.Trim, NumbersCashRegister.FiscalMemory, lstGoodType.SelectedItem.Value)
                _serviceGood.CheckNumbers(txtCTO.Text.Trim, NumbersCashRegister.SC1, lstGoodType.SelectedItem.Value)

                If txtCTO2.Visible = True Then
                    _serviceGood.CheckNumbers(txtCTO2.Text.Trim, NumbersCashRegister.SC2, lstGoodType.SelectedItem.Value)
                End If

                If txtCP.Visible = True Then
                    _serviceGood.CheckNumbers(txtCP.Text.Trim, NumbersCashRegister.CPU, lstGoodType.SelectedItem.Value)
                End If

                Dim worker%
                If lstWorker.Items.Count > 0 Then
                    worker = lstWorker.SelectedItem.Value
                Else
                    msgAddGood.Text = "Выберите ответственного за установку средств контроля. "
                    Exit Sub
                End If
                _serviceGood.CanAddGoodToDelivery(CInt(lstGoodDelivery.SelectedItem.Value), CInt(lstGoodType.SelectedItem.Value), 1)
                If _serviceGood.HaveAnyExeption() Then
                    msgAddGood.Text = _serviceGood.GetTextStringAllExeption()
                    Exit Sub
                End If

                cmd = New SqlClient.SqlCommand("new_cashregister")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_good_type_sys_id", lstGoodType.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_num_cashregister", txtGoodNumCashregister.Text.Trim)
                cmd.Parameters.AddWithValue("@pi_num_control_reestr", txtReestr.Text.Trim)
                cmd.Parameters.AddWithValue("@pi_num_control_pzu", txtPZU.Text.Trim)
                cmd.Parameters.AddWithValue("@pi_num_control_mfp", txtMFP.Text.Trim)
                cmd.Parameters.AddWithValue("@pi_num_control_cto", txtCTO.Text.Trim)
                cmd.Parameters.AddWithValue("@pi_num_control_cto2", IIf(txtCTO2.Visible = True, txtCTO2.Text.Trim, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_num_control_cp", IIf(txtCP.Visible = True, txtCP.Text.Trim, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_info", txtGoodInfo.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_worker", worker)
                If (rbtnKKM_Type.SelectedIndex = 0) Then
                    cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstGoodDelivery.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_state", 1)
                Else
                    cmd.Parameters.AddWithValue("@pi_state", 4)
                End If

                Dim dateadd = CDate(tbxBeginDate.Text)
                cmd.Parameters.AddWithValue("@pi_date", dateadd)

                If dbSQL.Execute(cmd) = 1 Then
                    msgAddGood.Text = "Товар добавлен в базу"
                    lstGoodType_SelectedIndexChanged(Me, Nothing)
                Else
                    msgAddGood.Text = "Ошибка добавления товара<br>"
                End If
                msgAddGood.ForeColor = Color.Black
                msgAddGood.Text = "Кассовый аппарат №" & txtGoodNumCashregister.Text.Trim & " добавлен в базу!"
            Catch
                msgAddGood.Text = "Ошибка добавления товара<br>" & Err.Description
            End Try
            LoadStatistic()
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Private Sub repGoodStatistic_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGoodStatistic.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                sum = sum + CInt(e.Item.DataItem("cash_all"))
                outside = outside + CInt(e.Item.DataItem("cash_outside"))
                rest = rest + CInt(e.Item.DataItem("cash_rest"))
            ElseIf (e.Item.ItemType = ListItemType.Footer) Then
                CType(e.Item.FindControl("lblCashAll"), Label).Text = CStr(sum)
                CType(e.Item.FindControl("lblCashOutSide"), Label).Text = CStr(outside)
                CType(e.Item.FindControl("lblCashRest"), Label).Text = CStr(rest)
            End If
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancel.Click
            Response.Redirect("GoodList.aspx")
        End Sub

        Protected Sub lstGoodType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGoodType.SelectedIndexChanged
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            If Not dbSQL.ExecuteScalar("select name from good_type where good_type_sys_id='" & lstGoodType.SelectedItem.Value & "'").ToString.Contains("Касби-03МФ") Then
                txtPZU.Visible = True
            Else
                txtPZU.Visible = False
            End If
            txtCTO2.Visible = True
            lblCTO2.Visible = True

            'If lstGoodType.SelectedItem.Value = Config.Kasbi04_ID Then
            '    txtCTO2.Visible = True
            '    lblCTO2.Visible = True
            '    'txtCP.Visible = True
            '    'lblCP.Visible = True
            '    txtGoodNumCashregister.MaxLength = 13
            'Else
            '    txtCTO2.Visible = False
            '    lblCTO2.Visible = False
            '    ' txtCP.Visible = False
            '    ' lblCP.Visible = False
            '    txtGoodNumCashregister.MaxLength = 13
            'End If

            If rbtnKKM_Type.SelectedIndex = 0 Then
                Try
                    cmd = New SqlClient.SqlCommand("get_good_type_by_delivery_info")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_type_sys_id", lstGoodType.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_delivery_sys_id", lstGoodDelivery.SelectedItem.Value)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)
                    Dim i%
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        blstDeliveryInfo.Items(0).Text = "Пришло : " & ds.Tables(0).Rows(i)("prihod") & " шт."
                        blstDeliveryInfo.Items(1).Text = "Введено в базу : " & ds.Tables(0).Rows(i)("rashod") & " шт."
                        blstDeliveryInfo.Items(2).Text = "Нужно ввести : " & ds.Tables(0).Rows(i)("ostatok") & " шт."
                    Next
                Catch
                    msgAddGood.Text = "Ошибка формирования информации о поставке товара!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Protected Sub rbtnKKM_Type_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnKKM_Type.SelectedIndexChanged
            If rbtnKKM_Type.SelectedIndex = 0 Then
                lstGoodDelivery.Enabled = True
                blstDeliveryInfo.Visible = True
                BindDeliveryList()
            Else
                lstGoodDelivery.Enabled = False
                blstDeliveryInfo.Visible = False
                BindFreeKKMList()
            End If
        End Sub
    End Class

End Namespace
