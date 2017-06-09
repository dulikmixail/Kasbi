Namespace Kasbi

    Partial Class Support
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink

        Dim iNumber%, iCustomer%
        Dim iSupportCost%, iSupportCostDiscount%
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
                Try
                    lstPayYear.Items.FindByValue(Year(Now)).Selected = True
                    lstPayMonth.Items.FindByValue(Month(Now)).Selected = True
                    lstPayDay.Items.FindByValue(Day(Now)).Selected = True
                Catch
                End Try

                Bind()

                Dim cust% = 0
                If Request.QueryString.Count > 0 Then
                    Try
                        cust = Request.QueryString(0)
                        Session("Customer") = cust
                    Catch
                        cust = Session("Customer")
                    End Try
                Else
                    cust = Session("Customer")
                End If

                GetInfo(cust)

                If Request.Form.Count < 10 Then

                    txtCostPerMonth.Text = iSupportCostDiscount

                End If

            End If

            lstCount.Attributes.Add("onchange", "javascript:ChangeCost('" & lstCount.ClientID & "', '" & txtCostPerMonth.ClientID & "','" & lblTotal.ClientID & "', " & iSupportCostDiscount & ", " & iSupportCost & ");")

        End Sub

        Public Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_customer_for_support")
                cmd.CommandType = CommandType.StoredProcedure

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet()
                adapt.Fill(ds)
                cmbCustomers.DataSource = ds.Tables(0).DefaultView
                cmbCustomers.DataTextField = "customer_name"
                cmbCustomers.DataValueField = "customer_sys_id"
                cmbCustomers.DataBind()

                iSupportCostDiscount = dbSQL.ExecuteScalar("select param_value_num from param where param_name = 'support_cost_discount'")
                iSupportCost = dbSQL.ExecuteScalar("select param_value_num from param where param_name = 'support_cost'")

            Catch
                msgSupport.Text = Err.Description
            End Try
        End Sub

        Sub GetInfo(ByVal cust As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                Dim item As ListItem = cmbCustomers.Items.FindByValue(cust)
                If item Is Nothing Then
                    cmbCustomers.SelectedIndex = 0
                    cust = cmbCustomers.Items(0).Value
                    msgSupport.Text = "С указанным клиентом не заключен договор на техобслуживание"
                Else
                    item.Selected = True
                End If

                cmd = New SqlClient.SqlCommand("get_support_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                cmd.Parameters.AddWithValue("@pi_sys_id", 1)

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet()
                adapt.Fill(ds)

                With ds.Tables(0).DefaultView(0)
                    Dim d As Date = .Item("end_date")
                    Dim s$, sTmp$

                    d.AddMonths(1)
                    lstMonth.SelectedIndex = d.Month - 1
                    If d.Year > 2002 And d.Year < 2014 Then
                        lstYear.SelectedIndex = d.Year - 2003
                    Else
                        lstYear.SelectedIndex = 0
                    End If

                    sTmp = .Item("unn")
                    If sTmp.Length > 0 Then
                        s = "УНП: " & sTmp & "<br>"
                    End If

                    sTmp = .Item("registration")
                    If sTmp.Length > 0 Then
                        s = s & sTmp & "<br>"
                    End If

                    sTmp = .Item("tax_inspection")
                    If s.Length > 0 Then
                        s = s & sTmp & "<br>"
                    End If

                    sTmp = .Item("bank")
                    If sTmp.Length > 0 Then
                        s = s & sTmp & "<br>"
                    End If

                    sTmp = .Item("customer_address")
                    If sTmp.Length > 0 Then
                        s = s & sTmp & "&nbsp;&nbsp;"
                        sTmp = .Item("customer_phone")
                        If sTmp.Length > 0 Then
                            s = s & sTmp
                        End If
                        s = s & "<br>"
                    End If

                    lblCustInfo.Text = "<br>" & s

                End With

                'кассы на техобслуживании
                cmd = New SqlClient.SqlCommand("get_client_cashregisters")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                cmd.Parameters.AddWithValue("@pi_support", 1)

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet()
                adapt.Fill(ds)

                lstSupportCash.DataSource = ds
                lstSupportCash.DataValueField = "good_sys_id"
                lstSupportCash.DataTextField = "good_description"
                lstSupportCash.DataBind()
                Dim i%
                For i = 0 To lstSupportCash.Items.Count - 1
                    lstSupportCash.Items(i).Selected = True
                Next
                'кассы на приостановке ТО
                cmd = New SqlClient.SqlCommand("get_client_cashregisters_delay")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                lstDelayCash.DataSource = ds
                lstDelayCash.DataValueField = "good_sys_id"
                lstDelayCash.DataTextField = "num_cashregister"
                lstDelayCash.DataBind()
                If lstDelayCash.Items.Count > 0 Then
                    lblDelay.Text = "То приостановлено: "

                    For i = 0 To lstDelayCash.Items.Count - 1
                        lstDelayCash.Items(i).Selected = True
                    Next
                End If
                'Кассы снятые с техобслуживания
                cmd = New SqlClient.SqlCommand("get_client_cashregisters_dismissal")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                i = 0

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                pnlCashregisters.Controls.Clear()
                If ds.Tables(0).Rows.Count > 0 Then
                    Dim ltrl As Literal = New Literal
                    ltrl.Text = "Сняты с ТО<br>"
                    pnlCashregisters.Controls.Add(ltrl)
                    For i = 0 To ds.Tables(0).Rows.Count - 1
                        'Dim lnk As HyperLink = New HyperLink()
                        'lnk.Text = ds.Tables(0).Rows(i)("num_cashregister")
                        'lnk.NavigateUrl = "repair.aspx?" & ds.Tables(0).Rows(i)("good_sys_id")
                        'pnlCashregisters.Controls.Add(lnk)
                        Dim lnk As Literal = New Literal
                        lnk.Text = ds.Tables(0).Rows(i)("num_cashregister")
                        pnlCashregisters.Controls.Add(lnk)
                        Dim ltrl1 As Literal = New Literal
                        ltrl1.Text = "<br>"
                        Dim ltrl2 As Literal = New Literal
                        ltrl2.Text = "&nbsp;&nbsp;&nbsp;"
                        If i Mod 2 = 0 Then
                            pnlCashregisters.Controls.Add(ltrl2)
                        Else
                            pnlCashregisters.Controls.Add(ltrl1)
                        End If
                    Next
                    pnlCashregisters.Enabled = False
                End If
                GetHistory(cust)
            Catch
                msgAddSupport.Text = Err.Description
            End Try

        End Sub

        Sub GetHistory(ByVal cust As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_support_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet()
                adapt.Fill(ds)

                If ds.Tables(0).DefaultView.Count = 0 Then
                    lblClearSupport.Text = "Платежи отсутствуют!"
                    grdSupportHistory.Visible = False
                Else
                    grdSupportHistory.Visible = True
                    lblClearSupport.Text = ""
                    iNumber = 1
                    grdSupportHistory.DataSource = ds
                    grdSupportHistory.DataKeyField = "sys_id"
                    grdSupportHistory.DataBind()
                End If

            Catch
                lblClearSupport.Text = Err.Description
            End Try

        End Sub

        Private Sub cmbCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbCustomers.SelectedIndexChanged
            GetInfo(cmbCustomers.SelectedItem.Value)
            Session("Customer") = cmbCustomers.SelectedItem.Value
        End Sub

        Private Sub grdSupportHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSupportHistory.ItemDataBound
            Dim cn As SqlClient.SqlConnection

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                ' Подтверждение удаления записи
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись о тех обслуживании?')){return confirm('Отменить удаление невозможно!!! Продолжить удаление?');}else {return false};")

                CType(e.Item.FindControl("lblNumber"), Label).Text = iNumber
                iNumber = iNumber + 1

                If e.Item.DataItem("pay") = 1 Then
                    CType(e.Item.FindControl("lnkPay"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lstYearPay"), DropDownList).Visible = False
                    CType(e.Item.FindControl("lstMonthPay"), DropDownList).Visible = False
                    CType(e.Item.FindControl("lstDayPay"), DropDownList).Visible = False
                    CType(e.Item.FindControl("lblPay"), Label).Visible = True
                    If Not IsDBNull(e.Item.DataItem("pay_date")) Then
                        CType(e.Item.FindControl("lblPay"), Label).Text = Format(e.Item.DataItem("pay_date"), "dd.MM.yyyy")
                    End If
                Else
                    CType(e.Item.FindControl("lnkPay"), LinkButton).Visible = True
                    CType(e.Item.FindControl("lstYearPay"), DropDownList).Visible = True
                    CType(e.Item.FindControl("lstMonthPay"), DropDownList).Visible = True
                    CType(e.Item.FindControl("lstDayPay"), DropDownList).Visible = True
                    CType(e.Item.FindControl("lblPay"), Label).Visible = False
                    Try
                        CType(e.Item.FindControl("lstMonthPay"), DropDownList).Items(Month(Now) - 1).Selected = True
                        CType(e.Item.FindControl("lstDayPay"), DropDownList).Items(Day(Now) - 1).Selected = True
                        CType(e.Item.FindControl("lstYearPay"), DropDownList).Items(Year(Now) - 2003).Selected = True
                    Catch
                    End Try

                    e.Item.BackColor = Color.MistyRose
                End If

                Dim s$ = ""
                If e.Item.DataItem("works") <> "" Then
                    s = e.Item.DataItem("works")
                    If e.Item.DataItem("materials") <> "" Then
                        s = s & ", "
                    End If
                End If
                s = s & e.Item.DataItem("materials")
                CType(e.Item.FindControl("lblWorksMaterials"), Label).Text = s

                CType(e.Item.FindControl("lnkInvoiceNDS"), HyperLink).NavigateUrl = "documents.aspx?c=" & cmbCustomers.SelectedItem.Value & "&s=" & e.Item.DataItem("sys_id") & "&t=0"
                CType(e.Item.FindControl("lnkAkt"), HyperLink).NavigateUrl = "documents.aspx?c=" & cmbCustomers.SelectedItem.Value & "&s=" & e.Item.DataItem("sys_id") & "&t=10"

                Dim d As Date
                d = e.Item.DataItem("start_date")
                d.AddDays(-d.Day + 1)
                CType(e.Item.FindControl("lblPeriod"), Label).Text = Format(d, "dd.MM.yyyy") & "&nbsp;-&nbsp;" & Format(d.AddMonths(e.Item.DataItem("period")), "dd.MM.yyyy")

            ElseIf e.Item.ItemType = ListItemType.EditItem Then

                Dim d As Date

                CType(e.Item.FindControl("lnkInvoiceNDS"), HyperLink).NavigateUrl = "documents.aspx?c=" & cmbCustomers.SelectedItem.Value & "&s=" & e.Item.DataItem("sys_id") & "&t=0"
                CType(e.Item.FindControl("lnkAkt"), HyperLink).NavigateUrl = "documents.aspx?c=" & cmbCustomers.SelectedItem.Value & "&s=" & e.Item.DataItem("sys_id") & "&t=10"

                If Not IsDBNull(e.Item.DataItem("pay_date")) Then
                    d = e.Item.DataItem("pay_date")
                Else
                    d = Now()
                End If
                Try
                    CType(e.Item.FindControl("lstYearPay"), DropDownList).Items(Year(d) - 2003).Selected = True
                    CType(e.Item.FindControl("lstMonthPay"), DropDownList).Items(Month(d) - 1).Selected = True
                    CType(e.Item.FindControl("lstDayPay"), DropDownList).Items(Day(d) - 1).Selected = True
                Catch
                End Try
                CType(e.Item.FindControl("chkedtPayed"), CheckBox).Checked = e.Item.DataItem("pay")

                d = e.Item.DataItem("start_date")
                CType(e.Item.FindControl("lstedtMonth"), DropDownList).Items(d.Month - 1).Selected = True
                Try
                    CType(e.Item.FindControl("lstedtYear"), DropDownList).Items(d.Year - 2003).Selected = True

                    Dim lst As DropDownList = CType(e.Item.FindControl("lstedtCount"), DropDownList)
                    Dim txtCost As TextBox = CType(e.Item.FindControl("txtedtCost"), TextBox)
                    Dim lblSumma As Label = CType(e.Item.FindControl("lbledtSumma"), Label)
                    lst.Items(e.Item.DataItem("period") - 1).Selected = True
                    lst.Attributes.Add("onchange", "javascript:ChangeCost('" & lst.ClientID & "', '" & txtCost.ClientID & "','" & lblSumma.ClientID & "', " & iSupportCostDiscount & ", " & iSupportCost & ");")
                    txtCost.Text = Math.Round(e.Item.DataItem("summ") / e.Item.DataItem("period"), 2)
                    lblSumma.Text = e.Item.DataItem("summ")

                    Dim adapt As SqlClient.SqlDataAdapter
                    Dim cmd As SqlClient.SqlCommand
                    Dim ds As DataSet

                    cmd = New SqlClient.SqlCommand("get_client_cashregisters")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cmbCustomers.SelectedItem.Value)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet()
                    adapt.Fill(ds)

                    With CType(e.Item.FindControl("lstedtSupportCash"), CheckBoxList)
                        .DataSource = ds
                        .DataValueField = "good_sys_id"
                        .DataTextField = "num_cashregister"
                        .DataBind()
                        Dim i%
                        Dim s$ = e.Item.DataItem("cash_list")
                        If s.Length = 0 Then
                            For i = 0 To .Items.Count - 1
                                .Items(i).Selected = True
                            Next
                        Else
                            For i = 0 To .Items.Count - 1
                                .Items(i).Selected = s.IndexOf(.Items(i).Text) > -1
                            Next
                        End If
                    End With

                    CType(e.Item.FindControl("txtedtWorks"), TextBox).Text = e.Item.DataItem("works")
                    CType(e.Item.FindControl("txtedtMaterials"), TextBox).Text = e.Item.DataItem("materials")

                Catch
                End Try
            End If
        End Sub

        Private Sub btnSaveSupport_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSaveSupport.Click
            Dim cmd As SqlClient.SqlCommand
            Dim iCost As Long
            Dim sCashList$, cust%, i%
            Dim isAllCash As Boolean

            If lstSupportCash.SelectedIndex = -1 Then
                msgAddSupport.Text = "Не выбранна ни одна касса!"
                Exit Sub
            End If

            Try
                iCost = CLng(txtCostPerMonth.Text)
            Catch
                msgAddSupport.Text = "Введите корректно стоимость тех. обслуживания за месяц!"
                Exit Sub
            End Try

            isAllCash = True
            For i = 0 To lstSupportCash.Items.Count - 1
                If lstSupportCash.Items(i).Selected Then
                    sCashList = sCashList & lstSupportCash.Items(i).Text & ", "
                Else
                    isAllCash = False
                End If
            Next
            If isAllCash Then
                sCashList = ""
            Else
                If sCashList.Length > 0 Then sCashList = sCashList.TrimEnd.TrimEnd(",")
            End If

            cust = cmbCustomers.SelectedItem.Value

            Try
                Dim d, dPay As Date
                d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)

                If chkPayed.Checked Then
                    dPay = New Date(lstPayYear.SelectedItem.Value, lstPayMonth.SelectedItem.Value, lstPayDay.SelectedItem.Value)
                End If

                For i = 1 To lstCount.SelectedItem.Value

                    cmd = New SqlClient.SqlCommand("insert_support")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                    cmd.Parameters.AddWithValue("@pi_start_date", d)
                    d = d.AddMonths(1)
                    cmd.Parameters.AddWithValue("@pi_period", 1)
                    cmd.Parameters.AddWithValue("@pi_summ", iCost)
                    cmd.Parameters.AddWithValue("@pi_cash_list", sCashList)
                    cmd.Parameters.AddWithValue("@pi_works", txtWorks.Text)
                    cmd.Parameters.AddWithValue("@pi_materials", txtMaterials.Text)
                    cmd.Parameters.AddWithValue("@pi_payed", chkPayed.Checked)
                    If chkPayed.Checked Then
                        cmd.Parameters.AddWithValue("@pi_pay_date", dPay)
                    Else
                        cmd.Parameters.AddWithValue("@pi_pay_date", System.DBNull.Value)
                    End If
                    dbSQL.Execute(cmd)
                Next
                GetHistory(cust)
            Catch
                msgAddSupport.Text = Err.Description
            End Try
        End Sub

        Private Sub grdSupportHistory_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportHistory.DeleteCommand
            Try
                dbSQL.Execute("DELETE FROM support where sys_id = " & grdSupportHistory.DataKeys(e.Item.ItemIndex))
            Catch
                If Err.Number = 1 Then
                    lblClearSupport.Text = "Выбранную запись нельзя удалить!"
                Else
                    lblClearSupport.Text = "Ошибка удаления записи!<br>" & Err.Description
                End If
            End Try
            GetHistory(cmbCustomers.SelectedItem.Value)
        End Sub

        Private Sub grdSupportHistory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportHistory.ItemCommand
            If e.CommandName = "Pay" Then
                Dim d As Date

                Try
                    d = New Date(CType(e.Item.FindControl("lstYearPay"), DropDownList).SelectedItem.Value, CType(e.Item.FindControl("lstMonthPay"), DropDownList).SelectedItem.Value, CType(e.Item.FindControl("lstDayPay"), DropDownList).SelectedItem.Value)
                Catch
                    lblClearSupport.Text = "Некорректная дата"
                    Exit Sub
                End Try

                Dim cmd As SqlClient.SqlCommand

                Try
                    cmd = New SqlClient.SqlCommand("set_pay_support")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_sys_id", e.Item.Cells(0).Text)
                    cmd.Parameters.AddWithValue("@pi_pay_date", d)
                    dbSQL.Execute(cmd)
                    GetHistory(cmbCustomers.SelectedItem.Value)
                Catch
                    lblClearSupport.Text = Err.Description
                End Try
            End If
        End Sub

        Private Overloads Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            lblTotal.Text = IIf(lstCount.SelectedItem.Value = 6, iSupportCostDiscount, iSupportCost) * lstCount.SelectedItem.Value
        End Sub

        Private Sub grdSupportHistory_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportHistory.EditCommand
            grdSupportHistory.EditItemIndex = CInt(e.Item.ItemIndex)
            GetHistory(cmbCustomers.SelectedItem.Value)
        End Sub

        Private Sub grdSupportHistory_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportHistory.CancelCommand
            grdSupportHistory.EditItemIndex = -1
            GetHistory(cmbCustomers.SelectedItem.Value)
        End Sub

        Private Sub grdSupportHistory_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportHistory.UpdateCommand
            Dim cmd As SqlClient.SqlCommand
            Dim iCost As Long
            Dim sCashList$, cust%, i%, iCount%
            Dim isAllCash As Boolean

            Dim lstSupportCash As CheckBoxList = CType(e.Item.FindControl("lstedtSupportCash"), CheckBoxList)

            If lstSupportCash.SelectedIndex = -1 Then
                lblClearSupport.Text = "Не выбранна ни одна касса!"
                Exit Sub
            End If

            Try
                iCost = CLng(CType(e.Item.FindControl("txtedtCost"), TextBox).Text)
                iCount = CType(e.Item.FindControl("lstedtCount"), DropDownList).SelectedItem.Value
            Catch
                lblClearSupport.Text = "Введите корректно стоимость тех. обслуживания за месяц!"
                Exit Sub
            End Try

            isAllCash = True
            For i = 0 To lstSupportCash.Items.Count - 1
                If lstSupportCash.Items(i).Selected Then
                    sCashList = sCashList & lstSupportCash.Items(i).Text & ", "
                Else
                    isAllCash = False
                End If
            Next
            If isAllCash Then
                sCashList = ""
            Else
                If sCashList.Length > 0 Then sCashList = sCashList.TrimEnd.TrimEnd(",")
            End If

            cust = cmbCustomers.SelectedItem.Value

            Dim d As Date
            Try
                d = New Date(CType(e.Item.FindControl("txtedtYearPay"), DropDownList).SelectedItem.Value, CType(e.Item.FindControl("txtedtMonthPay"), DropDownList).SelectedItem.Value, CType(e.Item.FindControl("txtedtDayPay"), DropDownList).SelectedItem.Value)
            Catch
                lblClearSupport.Text = "Некорректная дата"
                Exit Sub
            End Try

            Try
                cmd = New SqlClient.SqlCommand("update_support")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sys_id", grdSupportHistory.DataKeys(e.Item.ItemIndex))
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                cmd.Parameters.AddWithValue("@pi_pay_date", d)
                cmd.Parameters.AddWithValue("@pi_pay", CType(e.Item.FindControl("chkedtPayed"), CheckBox).Checked)
                cmd.Parameters.AddWithValue("@pi_start_date", New Date(CType(e.Item.FindControl("lstedtYear"), DropDownList).SelectedItem.Value, CType(e.Item.FindControl("lstedtMonth"), DropDownList).SelectedItem.Value, 1))
                cmd.Parameters.AddWithValue("@pi_period", iCount)
                cmd.Parameters.AddWithValue("@pi_summ", iCost * iCount)
                cmd.Parameters.AddWithValue("@pi_cash_list", sCashList)
                cmd.Parameters.AddWithValue("@pi_works", CType(e.Item.FindControl("txtedtWorks"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_materials", CType(e.Item.FindControl("txtedtMaterials"), TextBox).Text)
                dbSQL.Execute(cmd)
                grdSupportHistory.EditItemIndex = -1
                GetHistory(cust)
            Catch
                lblClearSupport.Text = Err.Description
            End Try
        End Sub

    End Class

End Namespace
