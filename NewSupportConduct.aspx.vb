Imports service

Namespace Kasbi

    Partial Class NewSupportConduct
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lblCaptionKassir As System.Web.UI.WebControls.Label
        Protected WithEvents lblKassir As System.Web.UI.WebControls.Label
        Protected WithEvents Linkbutton2 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents Dropdownlist4 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents Linkbutton3 As System.Web.UI.WebControls.LinkButton
        Protected WithEvents Dropdownlist5 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents Dropdownlist6 As System.Web.UI.WebControls.DropDownList
        Protected WithEvents txtMarka_Cond_ZReport_in As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtMarka_Cond_ZReport_out As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtMarka_Cond_Itog_in As System.Web.UI.WebControls.TextBox
        Protected WithEvents txtMarka_Cond_Itog_out As System.Web.UI.WebControls.TextBox


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim iNumber%, iCash%
        Dim sCaptionRemoveSupport As String = "Снять с ТО"
        Dim sCaptionAddSupport As String = "Поставить на ТО"
        Dim CurrentCustomer%, iNewCust%
        Const ClearString$ = "-------"
        Dim d As Documents
        Dim dogovor$
        Private serviceTo As ServiceTo = New ServiceTo()

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                Dim ch() As Char = {","}
                iCash = Request.Params(0).Split(ch).GetValue(0)
            Catch
                msg.Text = "Неверный запрос"
                Exit Sub
            End Try
            pnlDataPickerCloseDate.Visible = True

            ' If CurrentUser.Item("permissions") = 4 Then
            '   pnlDataPickerCloseDate.Visible = True
            'Else
            '   pnlDataPickerCloseDate.Visible = False
            ' End If

            'Dim sFilter$
            If Not IsPostBack Then

                'sFilter = Session("CustomerTOFilter")
                'If sFilter = "" Then
                '    Session("CustomerTOFilter") = "False"
                'End If

                Session("CustFilter") = ""

                Parameters.Value = 0
                LoadExecutor()
                LoadPlaceRegion()
                LoadCashRegList()
                rbTO_SelectedIndexChanged(Me, Nothing)
                LoadGoodInfo()
                LoadCustomerList()
                LoadSKNOInfo()
            End If
        End Sub

        Sub LoadSKNOInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Try
                cmd = New SqlClient.SqlCommand("get_skno_history")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                reader = dbSQL.GetReader(cmd)

                rbSKNO.Visible = False
                btnSaveSKNOInfo.Visible = False
                If Session("rule29") = 1 Or CurrentUser.is_admin Then
                    rbSKNO.Visible = True
                    btnSaveSKNOInfo.Visible = True
                End If
                If Not reader.Read Then
                    rbSKNO.SelectedValue = 0.ToString()
                    lblSKNOExecutor.Visible = False
                    lblSKNOExecutorInfo.Visible = False
                Else
                    If reader("state_SKNO") = 1 Then
                        lblSupportSKNO.Text = ", установлено СКНО"
                    End If
                    rbSKNO.SelectedValue = reader("state_SKNO").ToString()
                    lblSKNOExecutorInfo.Text = reader("executor").ToString()
                End If
                reader.Close()
            Catch
                msg.Text = "Ошибка загрузки информации о установке СКНО!1<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try

        End Sub

        Sub LoadGoodInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Try
                cmd = New SqlClient.SqlCommand("get_cashregister_info")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                reader = dbSQL.GetReader(cmd)
                If Not reader.Read Then
                    msg.Text = "Ошибка загрузки информации о товаре1!<br>"
                    Exit Sub
                End If
                lblCashType.Text = reader("name") & "&nbsp;&nbsp;"
                lblCash.Text = "№" & reader("num_cashregister")
                txtGoodNumCashregister.Text = reader("num_cashregister")
                lstGoodType.Items.FindByText(reader("name")).Selected = True
                lstGoodType.Enabled = False
                Dim s$, sTmp$
                Dim b As Boolean
                Dim d As Date = reader("end_date")

                d.AddMonths(1)
                'lstMonth.SelectedIndex = d.Month - 1
                lstMonth.SelectedIndex = Now.Month - 1
                If d.Year > 2002 And d.Year < 2019 Then
                    'lstYear.SelectedIndex = d.Year - 2003
                    lstYear.SelectedIndex = Now.Year - 2003
                Else
                    lstYear.SelectedIndex = 0
                End If
                'для приостановки
                d = Now

                'lstMonthDelayIn.SelectedIndex = d.Month - 1
                lstMonthDelayIn.SelectedIndex = Now.Month - 1
                If d.Year > 2002 And d.Year < 2019 Then
                    'lstYearDelayIn.SelectedIndex = d.Year - 2003
                    lstYearDelayIn.SelectedIndex = Now.Year - 2003
                Else
                    lstYearDelayIn.SelectedIndex = 0
                End If
                'для снятия
                tbxDismissalDate.Text = Now.ToShortDateString()
                'для постановки
                tbxSupportDate.Text = Now.ToShortDateString()
                'дата выполнения
                txtCloseDate.Text = Now.ToShortDateString()

                s = Trim(reader("num_control_reestr"))
                txtMarkaReestr_in.Text = s
                txtMarkaReestr_out.Text = s
                txtMarka_Reestr_Sup_In.Text = s
                txtMarka_Reestr_Sup_Out.Text = s
                txtMarka_Cond_Reestr_in.Text = s
                txtMarka_Cond_Reestr_out.Text = s

                sTmp = Trim(reader("num_control_pzu"))
                txtMarkaPZU_in.Text = sTmp
                txtMarkaPZU_out.Text = sTmp
                txtMarka_PZU_Sup_In.Text = sTmp
                txtMarka_PZU_Sup_Out.Text = sTmp
                txtMarka_Cond_PZU_in.Text = sTmp
                txtMarka_Cond_PZU_out.Text = sTmp

                s = s & " / " & sTmp
                sTmp = Trim(reader("num_control_mfp"))
                txtMarkaMFP_in.Text = sTmp
                txtMarkaMFP_out.Text = sTmp
                txtMarka_MFP_Sup_In.Text = sTmp
                txtMarka_MFP_Sup_Out.Text = sTmp
                txtMarka_Cond_MFP_in.Text = sTmp
                txtMarka_Cond_MFP_out.Text = sTmp

                s = s & " / " & sTmp

                'If reader("good_type_sys_id") = Config.Kasbi04_ID Then
                'lblCaptionNumbers.Text = "СК Реестра/ПЗУ/МФП/ЦП:"
                sTmp = Trim(reader("num_control_cp"))
                txtMarkaCP_in.Text = sTmp
                txtMarkaCP_out.Text = sTmp
                txtMarka_CP_Sup_In.Text = sTmp
                txtMarka_CP_Sup_Out.Text = sTmp
                txtMarka_Cond_CP_in.Text = sTmp
                txtMarka_Cond_CP_out.Text = sTmp
                s = s & " / " & sTmp
                'Else
                'trMarkaCP.Visible = False
                'trMarkaCP_Sup.Visible = False
                'trMarkaCond_CP.Visible = False
                'End If
                b = s.Length > 0
                If b Then lblNumbers.Text = s
                lblNumbers.Visible = b
                lblCaptionNumbers.Visible = b

                s = Trim(reader("marka"))
                txtMarkaCTO_in.Text = s
                txtMarka_Cto_Sup_In.Text = s
                txtMarka_Cond_CTO_in.Text = s
                txtMarka_Cond_CTO_out.Text = s
                If reader("good_type_sys_id") = Config.Kasbi04_ID Or reader("good_type_sys_id") = 1119 Then
                    sTmp = Trim(reader("num_control_cto2"))
                    s = s & " / " & sTmp
                    txtMarkaCTO2_in.Text = s
                    txtMarka_Cto2_Sup_In.Text = sTmp
                    txtMarka_Cond_CTO2_in.Text = sTmp
                    txtMarka_Cond_CTO2_out.Text = sTmp
                    lblCaptionMarka.Text = "Марки ЦТО/ЦТО2:"
                Else
                    trMarkaCTO2.Visible = False
                    trMarkaCto2_Sup.Visible = False
                    trMarkaCond_CTO2.Visible = False
                End If
                b = s.Length > 0
                If b Then lblMarka.Text = s
                lblMarka.Visible = b
                lblCaptionMarka.Visible = b
                'ошибка тут
                s = Trim(reader("date_created"))
                b = s.Length > 0
                If b Then lblDateCreated.Text = s
                lblDateCreated.Visible = b
                lblCaptionDateCreated.Visible = b

                s = Trim(reader("worker").ToString)
                b = s.Length > 0
                If b Then lblWorker.Text = s
                lblWorker.Visible = b
                lblCaptionWorker.Visible = b

                s = Trim(reader("set_place"))
                b = s.Length > 0
                If b Then
                    lblSetPlace.Text = s
                    txtPlace.Text = s
                End If
                lblSetPlace.Visible = b
                lblCaptionSetPlace.Visible = b
                If Not IsDBNull(reader("place_rn_id")) Then
                    lstPlaceRegion.Items.FindByValue(reader("place_rn_id")).Selected = True
                End If

                CurrentCustomer = CInt(IIf(IsDBNull(reader("owner_sys_id")), 0, reader("owner_sys_id")))
                If CurrentCustomer = 0 And Not IsDBNull(reader("customer_sys_id")) Then
                    CurrentCustomer = CInt(reader("customer_sys_id"))
                End If
                b = CurrentCustomer > 0 And Not IsDBNull(reader("owner_sys_id"))
                If b Then
                    s = Trim(reader("current_Owner"))
                    lnkOwner.Text = s
                    lnkOwner.NavigateUrl = GetAbsoluteUrl("~/Customer.aspx?" & CurrentCustomer & "&" & iCash)
                End If
                Parameters.Value = CurrentCustomer
                lblOwner.Visible = b
                lnkOwner.Visible = b
                lblCash.NavigateUrl = GetAbsoluteUrl("~/CashOwners.aspx?" & iCash & "&cashowner=" & CurrentCustomer)

                s = Trim(reader("sale"))
                b = s.Length > 0
                If b Then
                    lblSale.Text = s
                    lblSale.NavigateUrl = GetAbsoluteUrl("~/CustomerSales.aspx?" & reader("customer_sys_id"))
                End If
                lblCaptionSale.Visible = b

                If Not IsDBNull(reader("garantia")) Then
                    lblGarantia.Text = reader("garantia")
                End If
                If IsDBNull(reader("support")) OrElse reader("support") = 0 Then
                    lblSupport.Text = "Не заключен договор на ТО"

                    lblSupport.Enabled = False
                    imgSupport.Visible = False
                    pnlTOType.Visible = False
                    pnlConduct.Visible = False
                    pnlDelay.Visible = False
                    pnlDismissal.Visible = False
                    pnlSupport.Visible = True
                    rbTO.SelectedIndex = 0

                    SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Постановка на техническое обслуживание"
                Else
                    If reader("support") = "1" Then
                        If reader("stateTO") = "0" Or reader("stateTO") = "1" Or reader("stateTO") = "4" Then
                            lblSupport.Text = "Находится на ТО"

                        ElseIf reader("stateTO") = "6" Then
                            lblSupport.Text = "ТО приостановлено"
                            'chkDelayTO.Checked = True

                        ElseIf reader("stateTO") = "5" Then
                            lblSupport.Text = "Находится в ремонте"
                        End If

                        imgSupport.Visible = True
                        'imgSupport.NavigateUrl = GetAbsoluteUrl("~/Support.aspx?" & reader("owner_sys_id"))
                    Else

                        If reader("stateTO") = "2" Then
                            lblSupport.Text = "Снят с ТО"
                        ElseIf reader("stateTO") = "3" Then
                            lblSupport.Text = "Снят с ТО и в ИМНС"
                        End If

                        pnlTOType.Visible = False
                        pnlConduct.Visible = False
                        pnlDelay.Visible = False
                        pnlDismissal.Visible = False
                        pnlSupport.Visible = True
                        rbTO.SelectedIndex = 0
                        SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Постановка на техническое обслуживание"
                        imgSupport.Visible = False
                    End If
                End If

                If IsDBNull(reader("alert")) OrElse Trim(reader("alert")).Length = 0 Then
                    imgAlert.Visible = False
                Else
                    imgAlert.Visible = True
                    imgAlert.ToolTip = reader("alert")
                End If
                reader.Close()
                ShowRepairImage()
            Catch
                msg.Text = "Ошибка загрузки информации о товаре!3<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try
            lblCustInfo.Text = "<br>" & GetInfo(CurrentCustomer)

            GetHistory(iCash)
        End Sub

        Sub ShowRepairImage()
            Try
                imgRepair.Visible = dbSQL.ExecuteScalar("select dbo.udf_repair(" & iCash & ")") > 0
            Catch
            End Try
        End Sub

        Sub LoadCashRegList()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select good_type_sys_id,name name from good_type where is_cashregister=1 order by name")
                ds = New DataSet
                adapt.Fill(ds)

                lstGoodType.DataSource = ds
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
            Catch
                msgAddSupportConduct.Text = "Ошибка формирования списков!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub LoadExecutor()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("get_salers", True)
                ds = New DataSet
                adapt.Fill(ds)

                lstWorker.DataSource = ds
                lstWorker.DataTextField = "name"
                lstWorker.DataValueField = "sys_id"
                lstWorker.DataBind()
                lstWorker.Items.Insert(0, New ListItem(ClearString, "0"))
                lstWorker.SelectedIndex = -1
                Try
                    lstWorker.Items.FindByValue(CurrentUser.sys_id).Selected = True
                Catch
                End Try
            Catch
                msgAddSupportConduct.Text = "Ошибка формирования списков!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub LoadPlaceRegion()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from Place_Rn order by name")
                ds = New DataSet
                adapt.Fill(ds)

                lstPlaceRegion.DataSource = ds
                lstPlaceRegion.DataTextField = "name"
                lstPlaceRegion.DataValueField = "place_rn_id"
                lstPlaceRegion.DataBind()
                lstPlaceRegion.Items.Insert(0, New ListItem(ClearString, ClearString))

            Catch
                msgAddSupportConduct.Text = "Ошибка формирования списка районов установки!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Public Sub LoadSaleByCustomer()
            Dim cmdSales As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cust%

            If lstCustomers.SelectedIndex <= 0 Then
                lblSaleInfo.Visible = False
                cmbSalesInfo.Items.Clear()
                cmbSalesInfo.Visible = False
                txtDogovor.Text = ""
                txtDogovor.Visible = False
                lblDogovor.Visible = False
                Exit Sub
            End If

            cust = lstCustomers.SelectedItem.Value
            If cust = 0 Then
                msgAddSupportConduct.Text = "Ошибка определения клиента!"
                Exit Sub
            End If
            Try
                cmdSales = New SqlClient.SqlCommand("get_customer_sale_info")
                cmdSales.CommandType = CommandType.StoredProcedure
                cmdSales.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                adapt = dbSQL.GetDataAdapter(cmdSales)
                ds = New DataSet
                adapt.Fill(ds)

                cmbSalesInfo.DataSource = ds
                cmbSalesInfo.DataValueField = "sale_sys_id"
                cmbSalesInfo.DataTextField = "sale"
                cmbSalesInfo.DataBind()
                cmbSalesInfo.Items.Insert(0, New ListItem("Новое переоформление", "0"))
                cmbSalesInfo_SelectedIndexChanged(Me, Nothing)
            Catch
                msgAddSupportConduct.Text = Err.Description
            End Try
        End Sub

        Private Sub cmbSalesInfo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbSalesInfo.SelectedIndexChanged

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cust%
            If lstCustomers.SelectedIndex <= 0 Then
                'msgAddSupportConduct.Text = "Не выбран ни один клиент!"
                lblSaleInfo.Visible = False
                cmbSalesInfo.Items.Clear()
                cmbSalesInfo.Visible = False
                txtDogovor.Text = ""
                txtDogovor.Visible = False
                lblDogovor.Visible = False
                Exit Sub
            Else
                lblCustInfo.Text = "<br>" & GetInfo(lstCustomers.SelectedItem.Value)
                lblSaleInfo.Visible = True
                cmbSalesInfo.Visible = True
                txtDogovor.Visible = True
                lblDogovor.Visible = True
            End If

            cust = lstCustomers.SelectedItem.Value
            If cust = 0 Then
                msgAddSupportConduct.Text = "Ошибка определения клиента!"
                Exit Sub
            End If
            If cmbSalesInfo.Items.Count = 0 Then
                Exit Sub
            End If

            Dim query
            query = "SELECT * FROM tbl_good ORDER BY sys_id"

            If cmbSalesInfo.SelectedItem.Value = "0" Then
                'новый номер договора
                Try
                    adapt = dbSQL.GetDataAdapter("select case when dogovor is null then '' else dogovor end dogovor from sale where customer_sys_id=" & cust & " order by d DESC")
                    ds = New DataSet
                    adapt.Fill(ds)
                    Dim ch() As Char = {"\", "/", ".", "-"}
                    Dim s$, i%
                    If ds.Tables(0).Rows.Count > 0 Then
                        s = ds.Tables(0).Rows(0).Item("dogovor")
                        s = s.Trim(ch)
                        Try
                            i = CInt(s) + 1
                            s = "/" & i
                        Catch
                            s = "/1"
                        End Try
                        s = dogovor & s
                    Else
                        s = dogovor
                    End If
                    txtDogovor.Visible = True
                    txtDogovor.Text = s
                Catch
                End Try
            Else
                txtDogovor.Text = ""
                txtDogovor.Visible = False
                lblDogovor.Visible = False
            End If
        End Sub

        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            'Ограничение прав на добавление
            If Session("rule17") = "1" Then

                Dim adapt As SqlClient.SqlDataAdapter
                Dim cmd As SqlClient.SqlCommand
                Dim cust%
                Dim ds As DataSet

                Dim d, dNow As DateTime
                CurrentCustomer = Parameters.Value

                If lstWorker.SelectedIndex <= 0 Then
                    msgAddSupportConduct.Text = "Выберите исполнителя !"
                    Exit Sub
                End If


                If rbTO.SelectedIndex = 2 And chkDelayTO.Checked = False Then


                    d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
                    If Not serviceTo.CheckCashHistoryItem(iCash, d, txtCloseDate.Text) Then
                        msgAddSupportConduct.Text = serviceTo.GetLastExeption()
                        Exit Sub
                    End If

                    Try
                        cmd = New SqlClient.SqlCommand("insert_supportConduct")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", CurrentCustomer)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_in", txtMarka_Cond_CTO_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_out", txtMarka_Cond_CTO_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_in", txtMarka_Cond_PZU_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_out", txtMarka_Cond_PZU_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_in", txtMarka_Cond_MFP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_out", txtMarka_Cond_MFP_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_in", txtMarka_Cond_Reestr_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_out", txtMarka_Cond_Reestr_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_in", txtMarka_Cond_CP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_out", txtMarka_Cond_CP_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_in", txtMarka_Cond_CTO2_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_out", txtMarka_Cond_CTO2_out.Text)
                        '
                        'cmd.Parameters.Add("@pi_zreport_in", txtMarka_Cond_ZReport_in.Text)
                        'cmd.Parameters.Add("@pi_zreport_out", txtMarka_Cond_ZReport_out.Text)
                        'cmd.Parameters.Add("@pi_itog_in", txtMarka_Cond_Itog_in.Text)
                        'cmd.Parameters.Add("@pi_itog_out", txtMarka_Cond_Itog_out.Text)
                        '
                        cmd.Parameters.AddWithValue("@pi_start_date", d)
                        If (txtCloseDate.Text <> "") Then
                            d = DateTime.Parse(txtCloseDate.Text)
                        Else
                            d = Now
                        End If
                        cmd.Parameters.AddWithValue("@pi_period", 1)
                        cmd.Parameters.AddWithValue("@pi_info", txtGoodInfo.Text)
                        cmd.Parameters.AddWithValue("@pi_executor", lstWorker.SelectedValue)
                        cmd.Parameters.AddWithValue("@pi_close_date", d)
                        cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                        cmd.Parameters.AddWithValue("@place", txtPlace.Text.Replace("'", """"))
                        If lstPlaceRegion.SelectedIndex > 0 Then
                            cmd.Parameters.AddWithValue("@pi_place_rn_id", lstPlaceRegion.SelectedValue)
                        End If
                        dbSQL.Execute(cmd)
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                    End Try

                    'приостановка ТО
                ElseIf rbTO.SelectedIndex = 2 And chkDelayTO.Checked = True Then
                    d = New Date(lstYearDelayIn.SelectedItem.Value, lstMonthDelayIn.SelectedItem.Value, 1)
                    '
                    'If (d > dNow) Then
                    '    msgAddSupportConduct.Text = "Выбранный период больше текущего периода"
                    '    Exit Sub
                    'End If
                    '
                    Try
                        cmd = New SqlClient.SqlCommand("get_supportconduct_end_date")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet
                        adapt.Fill(ds)
                        If ds.Tables(0).Rows.Count > 0 Then
                            With ds.Tables(0).DefaultView(0)
                                Dim enddate As Date = .Item("end_date")
                                Dim state As Integer = .Item("state")
                                Select Case state
                                    Case 6
                                        If d < enddate Then
                                            msgAddSupportConduct.Text = "Кассовый аппарат находиться на приостановке ТО"
                                            Exit Sub
                                        End If
                                    Case 2 To 3
                                        msgAddSupportConduct.Text = "Кассовый аппарат снят с ТО"
                                        Exit Sub
                                End Select
                            End With
                        End If
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                    End Try
                    Try
                        cmd = New SqlClient.SqlCommand("insert_supportDelay")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", CurrentCustomer)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_in", txtMarka_Cond_CTO_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_out", txtMarka_Cond_CTO_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_in", txtMarka_Cond_PZU_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_out", txtMarka_Cond_PZU_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_in", txtMarka_Cond_MFP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_out", txtMarka_Cond_MFP_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_in", txtMarka_Cond_Reestr_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_out", txtMarka_Cond_Reestr_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_in", txtMarka_Cond_CTO2_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_out", txtMarka_Cond_CTO2_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_in", txtMarka_Cond_CP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_out", txtMarka_Cond_CP_out.Text)
                        '
                        cmd.Parameters.AddWithValue("@pi_start_date", d)
                        cmd.Parameters.AddWithValue("@pi_period", lstMonthQty.SelectedValue)
                        cmd.Parameters.AddWithValue("@pi_info", txtGoodInfo.Text)
                        cmd.Parameters.AddWithValue("@pi_executor", lstWorker.SelectedValue)
                        cmd.Parameters.AddWithValue("@pi_close_date", Now)
                        cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                        cmd.Parameters.AddWithValue("@place", txtPlace.Text.Replace("'", """"))
                        If lstPlaceRegion.SelectedIndex > 0 Then
                            cmd.Parameters.AddWithValue("@pi_place_rn_id", lstPlaceRegion.SelectedValue)
                        End If
                        dbSQL.Execute(cmd)
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                    End Try


                    'Снятие с ТО
                ElseIf rbTO.SelectedIndex = 1 Then

                    cmd = New SqlClient.SqlCommand("get_supportconduct_end_date")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)

                    Try
                        If ds.Tables(0).Rows.Count > 0 Then
                            With ds.Tables(0).DefaultView(0)
                                Dim enddate As Date = .Item("end_date")
                                Dim state As Integer = .Item("state")
                                Select Case state
                                    Case 2 To 3
                                        msgAddSupportConduct.Text = "Кассовый аппарат снят с ТО"
                                        Exit Sub
                                End Select
                            End With
                        End If

                        dNow = Date.Now
                        d = DateTime.Parse(tbxDismissalDate.Text)
                        If (d > dNow) Then
                            msgAddSupportConduct.Text = "Дата снятия не может быть больше декущей даты"
                            Exit Sub
                        End If


                    Catch
                        msgAddSupportConduct.Text = "Пожалуйста, введите корректное значения даты снятия с ТО"
                        Exit Sub
                    End Try
                    Try
                        cmd = New SqlClient.SqlCommand("insert_supportDismissal")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", CurrentCustomer)
                        cmd.Parameters.AddWithValue("@pi_dismissal_date", d)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_in", txtMarkaCTO_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_out", txtMarkaCTO_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_in", txtMarkaPZU_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_out", txtMarkaPZU_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_in", txtMarkaMFP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_out", txtMarkaMFP_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_in", txtMarkaReestr_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_out", txtMarkaReestr_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_in", txtMarkaCTO2_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_out", txtMarkaCTO2_out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_in", txtMarkaCP_in.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_out", txtMarkaCP_out.Text)
                        cmd.Parameters.AddWithValue("@pi_zreport_in", txtZReportIn.Text)
                        cmd.Parameters.AddWithValue("@pi_zreport_out", txtZReportOut.Text)
                        cmd.Parameters.AddWithValue("@pi_itog_in", txtItogIn.Text)
                        cmd.Parameters.AddWithValue("@pi_itog_out", txtItogOut.Text)
                        cmd.Parameters.AddWithValue("@pi_info", txtGoodInfo.Text)
                        cmd.Parameters.AddWithValue("@pi_executor", lstWorker.SelectedValue)
                        cmd.Parameters.AddWithValue("@pi_close_date", Now)
                        cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                        cmd.Parameters.AddWithValue("@place", txtPlace.Text.Replace("'", """"))
                        If lstPlaceRegion.SelectedIndex > 0 Then
                            cmd.Parameters.AddWithValue("@pi_place_rn_id", lstPlaceRegion.SelectedValue)
                        End If
                        If chkDismissalIMNS.Checked = True Then
                            cmd.Parameters.AddWithValue("@state", 3)
                        Else
                            cmd.Parameters.AddWithValue("@state", 2)
                        End If
                        dbSQL.Execute(cmd)
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                    End Try

                    'Постановка на ТО
                ElseIf rbTO.SelectedIndex = 0 Then
                    If lstCustomers.SelectedIndex <= 0 Then
                        msgAddSupportConduct.Text = "Не выбран ни один клиент!"
                        Exit Sub
                    End If

                    cust = lstCustomers.SelectedItem.Value
                    If cust = 0 Then
                        msgAddSupportConduct.Text = "Ошибка определения клиента!"
                        Exit Sub
                    End If

                    cmd = New SqlClient.SqlCommand("get_supportconduct_end_date")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)

                    Try
                        dNow = Date.Now
                        d = DateTime.Parse(tbxSupportDate.Text)
                        Dim regex_ru As Regex = New Regex("МН\d+") 'МН написаны Кирилицей
                        Dim regex_en As Regex = New Regex("MH\d*") 'МН написаны Латиницей

                        If ds.Tables(0).Rows.Count > 0 Then
                            With ds.Tables(0).DefaultView(0)
                                Dim enddate As Date = .Item("end_date")
                                Dim state As Integer = .Item("state")
                                Select Case state
                                    Case 4
                                        msgAddSupportConduct.Text = "Кассовый аппарат уже поставлен на ТО"
                                        Exit Sub
                                End Select
                            End With
                        End If

                        If (d > dNow) Then
                            msgAddSupportConduct.Text = "Дата постановки не может быть больше декущей даты"
                            Exit Sub
                        ElseIf regex_en.Match(txtMarka_Cto_Sup_In.Text.Trim).Success Or regex_en.Match(txtMarka_Cto_Sup_Out.Text.Trim).Success Then
                            msgAddSupportConduct.Text = "МН необходимо писать Кирилицей"
                            Exit Sub
                        ElseIf txtMarka_Cto_Sup_In.Text.Trim <> "" And Not regex_ru.Match(txtMarka_Cto_Sup_In.Text.Trim).Success Then
                            msgAddSupportConduct.Text = "Введите корректно СК ЦТО в поле ""до постановки"""
                            Exit Sub
                        ElseIf txtMarka_Cto_Sup_Out.Text.Trim() = "" Then
                            msgAddSupportConduct.Text = "Не введена СК ЦТО в поле ""после постановки"""
                            Exit Sub
                        ElseIf Not regex_ru.Match(txtMarka_Cto_Sup_Out.Text.Trim).Success Then
                            msgAddSupportConduct.Text = "Введите корректно СК ЦТО в поле ""после постановки"""
                            Exit Sub

                        End If

                    Catch
                        msgAddSupportConduct.Text = "Пожалуйста, введите корректное значения даты"
                        Exit Sub
                    End Try
                    Dim sale_id = SaveRebillingInfo()
                    Try
                        cmd = New SqlClient.SqlCommand("insert_cashOwner")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                        cmd.Parameters.AddWithValue("@pi_support_date", d)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_in", txtMarka_Cto_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto_out", txtMarka_Cto_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_in", txtMarka_PZU_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_out", txtMarka_PZU_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_in", txtMarka_MFP_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_out", txtMarka_MFP_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_in", txtMarka_Reestr_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_out", txtMarka_Reestr_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_in", txtMarka_Cto2_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_out", txtMarka_Cto2_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_in", txtMarka_CP_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_marka_cp_out", txtMarka_CP_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_zreport_in", txtZReport_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_zreport_out", txtZReport_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_itog_in", txtItog_Sup_In.Text)
                        cmd.Parameters.AddWithValue("@pi_itog_out", txtItog_Sup_Out.Text)
                        cmd.Parameters.AddWithValue("@pi_info", txtGoodInfo.Text)
                        cmd.Parameters.AddWithValue("@pi_executor", lstWorker.SelectedValue)
                        cmd.Parameters.AddWithValue("@pi_close_date", Now)
                        cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                        cmd.Parameters.AddWithValue("@pi_sale_id", sale_id)
                        cmd.Parameters.AddWithValue("@place", txtPlace.Text.Replace("'", """"))

                        If lstPlaceRegion.SelectedIndex > 0 Then
                            cmd.Parameters.AddWithValue("@pi_place_rn_id", lstPlaceRegion.SelectedValue)
                        End If

                        dbSQL.Execute(cmd)
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                        rbTO.SelectedIndex = 2
                        rbTO_SelectedIndexChanged(Me, Nothing)
                    End Try
                End If
                LoadGoodInfo()
            End If
        End Sub

        Function GetInfo(ByVal cust As Integer, Optional ByVal flag As Boolean = True) As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            s = ""
            If cust = 0 Then
                lstCustomers.SelectedIndex = 0
                Return ""
                Exit Function
            End If
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                'cmd.Parameters.Add("@pi_sys_id", 1)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).DefaultView(0)
                        Dim sTmp$

                        sTmp = .Item("customer_name")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                        sTmp = .Item("unn")
                        If sTmp.Length > 0 Then
                            s = s & "УНП: " & sTmp & "<br>"
                        End If

                        sTmp = .Item("registration")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = "по " & .Item("tax_inspection")
                        If s.Length > 0 Then
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

                        sTmp = .Item("bank")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If


                        dogovor = .Item("dogovor")
                    End With
                End If
            Catch
                msgAddSupportConduct.Text = Err.Description
            End Try
            GetInfo = s
        End Function

        Private Sub lstCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomers.SelectedIndexChanged
            If lstCustomers.SelectedItem.Value > 0 Then
                lblCustInfo.Text = "<br>" & GetInfo(lstCustomers.SelectedItem.Value)
            Else
                lblCustInfo.Text = ""
            End If
            Session("Customer") = lstCustomers.SelectedItem.Value
            LoadSaleByCustomer()
        End Sub

        Sub GetHistory(ByVal cashRegister As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_cash_TO_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@good_sys_id", cashRegister)
                'cmd.Parameters.Add("@state", "(1,2,3)")
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).DefaultView.Count = 0 Then
                    msgSupportConductHistory.Text = ""
                    grdSupportConductHistory.Visible = False
                    pnlSupportConductHistory.Visible = False
                ElseIf ds.Tables(0).DefaultView.Count = 1 And IsDBNull(ds.Tables(0).Rows(0)("sys_id")) Then
                    msgSupportConductHistory.Text = ""
                    grdSupportConductHistory.Visible = False
                    pnlSupportConductHistory.Visible = False
                Else
                    grdSupportConductHistory.Visible = True
                    pnlSupportConductHistory.Visible = True
                    msgSupportConductHistory.Text = ""
                    iNumber = 1
                    grdSupportConductHistory.DataSource = ds
                    grdSupportConductHistory.DataKeyField = "sys_id"
                    grdSupportConductHistory.DataBind()
                End If
            Catch
                msgSupportConductHistory.Text = Err.Description
            End Try
        End Sub

        Private Sub grdSupportConductHistory_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.EditCommand
            grdSupportConductHistory.EditItemIndex = CInt(e.Item.ItemIndex)
            GetHistory(iCash)
        End Sub

        Private Sub grdSupportConductHistory_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.CancelCommand
            grdSupportConductHistory.EditItemIndex = -1
            GetHistory(iCash)
        End Sub

        Private Sub grdSupportConductHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSupportConductHistory.ItemDataBound
            Dim s As String
            Dim d As Date
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                'Dates
                If Not IsDBNull(e.Item.DataItem("updateDate")) Then
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID").ToString() & "<br>" & Format(e.Item.DataItem("updateDate"), "dd.MM.yyyy HH:mm")
                Else
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID").ToString()
                End If
                CType(e.Item.FindControl("lblUpdateRec"), Label).Text = s

                Dim query
                query = "select * from goodto where goodto_sys_id in (select id from good_type)"
                'определяем тип ТО
                If Not IsDBNull(e.Item.DataItem("state")) Then

                    ' проведение ТО ``````````````
                    If e.Item.DataItem("state") = 1 Then
                        s = ""
                        If Not IsDBNull(e.Item.DataItem("start_date")) Then
                            d = e.Item.DataItem("start_date")
                            s = GetRussianDate(d)
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lnkAkt_TO"), HyperLink).Text = "Акт ТО"
                        CType(e.Item.FindControl("lnkAkt_TO"), HyperLink).NavigateUrl = "~/documents.aspx?" & "&t=60&g=" & e.Item.DataItem("good_sys_id") & "&d=" & Format(e.Item.DataItem("change_state_date"), "ddMMyyyy")
                        CType(e.Item.FindControl("lblPeriod"), Label).Text = GetRussianDate(d)
                        s = "ТО проведено <br>"
                        s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") & "<br>"
                        If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Or (e.Item.DataItem("good_type_sys_id")) = 1119 Then
                            s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " & e.Item.DataItem("marka_cto2_out") & "<br>"
                        End If
                        s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " & e.Item.DataItem("marka_reestr_out") & "<br>"
                        s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") & "<br>"
                        s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") & "<br>"
                        'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                        s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") & "<br>"
                        'End If
                        s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") & "<br>"
                        s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 6 Then
                        'приостановка ТО
                        s = ""
                        If Not IsDBNull(e.Item.DataItem("start_date")) Then
                            d = e.Item.DataItem("start_date")
                            s = GetRussianDateFull(d)
                        Else
                            s = ""
                        End If
                        s = "ТО приостановлено c <br>" & s & " на " & e.Item.DataItem("period").ToString & " мес."
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 2 Then
                        'снятие с ТО
                        If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                            d = e.Item.DataItem("dismissal_date")
                            s = "Снят с ТО " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl = "documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=14&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id")
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl = "documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=19&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id")
                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Or (e.Item.DataItem("good_type_sys_id")) = 1119 Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " & e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " & e.Item.DataItem("marka_reestr_out") & "<br>"
                            s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") & "<br>"
                            s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") & "<br>"
                            'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                            s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") & "<br>"
                            'End If
                            s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") & "<br>"
                            s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 3 Then
                        'снятие с ТО и в ИМНС
                        If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                            d = e.Item.DataItem("dismissal_date")
                            s = "Снят с ТО и в ИМНС " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=15&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=20&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Or (e.Item.DataItem("good_type_sys_id")) = 1119 Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " & e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " & e.Item.DataItem("marka_reestr_out") & "<br>"
                            s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") & "<br>"
                            s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") & "<br>"
                            'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                            s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") & "<br>"
                            'End If
                            s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") & "<br>"
                            s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 4 Then
                        'постановка на ТО
                        If Not IsDBNull(e.Item.DataItem("support_date")) Then
                            d = e.Item.DataItem("support_date")
                            s = "Поставлен на ТО " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).Text = "Акт"
                            CType(e.Item.FindControl("lnkAct"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=11&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).Text = "Тех. закл."
                            CType(e.Item.FindControl("lnkTehZaklyuchenie"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=12&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("lnkDogovor_Na_TO"), HyperLink).Text = "Дог. на ТО"
                            CType(e.Item.FindControl("lnkDogovor_Na_TO"), HyperLink).NavigateUrl = GetAbsoluteUrl("documents.aspx?c=" & e.Item.DataItem("owner_sys_id") & "&s=" & e.Item.DataItem("sale_sys_id") & "&t=13&g=" & e.Item.DataItem("good_sys_id") & "&h=" & e.Item.DataItem("sys_id"))
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                            s = s & "&nbsp;СК ЦТО :" & e.Item.DataItem("marka_cto_in") & " / " & e.Item.DataItem("marka_cto_out") & "<br>"
                            If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Or (e.Item.DataItem("good_type_sys_id")) = 1119 Then
                                s = s & "&nbsp;СК ЦТО2 :" & e.Item.DataItem("marka_cto2_in") & " / " & e.Item.DataItem("marka_cto2_out") & "<br>"
                            End If
                            s = s & "&nbsp;СК Реестра :" & e.Item.DataItem("marka_reestr_in") & " / " & e.Item.DataItem("marka_reestr_out") & "<br>"
                            s = s & "&nbsp;СК ПЗУ :" & e.Item.DataItem("marka_pzu_in") & " / " & e.Item.DataItem("marka_pzu_out") & "<br>"
                            s = s & "&nbsp;СК МФП :" & e.Item.DataItem("marka_mfp_in") & " / " & e.Item.DataItem("marka_mfp_out") & "<br>"
                            'If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Then
                            s = s & "&nbsp;СК ЦП :" & e.Item.DataItem("marka_cp_in") & " / " & e.Item.DataItem("marka_cp_out") & "<br>"
                            'End If
                            s = s & "&nbsp;Z-отчет :" & e.Item.DataItem("zreport_in") & " / " & e.Item.DataItem("zreport_out") & "<br>"
                            s = s & "&nbsp;Итог :" & e.Item.DataItem("itog_in") & " / " & e.Item.DataItem("itog_out") & "<br>"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s


                    End If
                End If
                ' Информация о плательщике ТО
                s = ""
                If Not IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    s = GetInfo(CInt(e.Item.DataItem("owner_sys_id")), False)
                End If
                CType(e.Item.FindControl("lnkPayer"), HyperLink).Text = s
                If (e.Item.DataItem("state") = 4) Then
                    CType(e.Item.FindControl("lnkPayer"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/CustomerSales.aspx?" & e.Item.DataItem("owner_sys_id"))
                End If
                ' Информация о исполнителе
                s = ""
                If Not IsDBNull(e.Item.DataItem("executor")) Then
                    s = e.Item.DataItem("executor")
                End If
                CType(e.Item.FindControl("lblExecutorTO"), Label).Text = s

                ' дополнительная информация
                CType(e.Item.FindControl("lblInfo"), Label).Text = e.Item.DataItem("info")
                'дата когда закрыто ТО за месяц
                s = ""
                If Not IsDBNull(e.Item.DataItem("change_state_date")) Then
                    s = s & Format(e.Item.DataItem("change_state_date"), "dd.MM.yyyy")
                End If
                CType(e.Item.FindControl("lblCloseDate"), Label).Text = s

                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                s = ""
                'плательщик
                If Not IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    s = GetInfo(CInt(e.Item.DataItem("owner_sys_id")), False)
                End If
                CType(e.Item.FindControl("lnkPayer"), HyperLink).Text = s

                If e.Item.DataItem("state") = 1 Then
                    s = ""
                    If Not IsDBNull(e.Item.DataItem("start_date")) Then
                        d = e.Item.DataItem("start_date")
                        s = GetRussianDateFull(d)
                    Else
                        s = ""
                    End If
                    CType(e.Item.FindControl("lbledtPeriod"), Label).Text = s
                    's = "ТО проведено"
                    CType(e.Item.FindControl("lbledtStatus"), Label).Text = "ТО проведено"
                    CType(e.Item.FindControl("txtDate"), TextBox).Text = Format(d, "dd.MM.yyyy")
                    CType(e.Item.FindControl("txtDate"), TextBox).Visible = True

                ElseIf e.Item.DataItem("state") = 6 Then
                    s = ""
                    If Not IsDBNull(e.Item.DataItem("start_date")) Then
                        s = Format(e.Item.DataItem("start_date"), "dd.MM.yyyy")
                    Else
                        s = ""
                    End If
                    CType(e.Item.FindControl("lbledtStatus"), Label).Text = "ТО приостановлено "
                    CType(e.Item.FindControl("lbltxtDate"), Label).Visible = True
                    CType(e.Item.FindControl("txtDate"), TextBox).Text = s
                    CType(e.Item.FindControl("txtDate"), TextBox).Visible = True
                    CType(e.Item.FindControl("lbltxtPeriod1"), Label).Visible = True
                    CType(e.Item.FindControl("lbltxtPeriod2"), Label).Visible = True
                    CType(e.Item.FindControl("txtPeriod"), TextBox).Text = e.Item.DataItem("period").ToString
                    CType(e.Item.FindControl("txtPeriod"), TextBox).Visible = True
                ElseIf e.Item.DataItem("state") = 4 Then
                    'постановка на ТО
                    If Not IsDBNull(e.Item.DataItem("support_date")) Then
                        s = Format(e.Item.DataItem("support_date"), "dd.MM.yyyy")
                    Else
                        s = ""
                    End If
                    CType(e.Item.FindControl("lbledtStatus"), Label).Text = "Поставлен на ТО "
                    CType(e.Item.FindControl("txtDate"), TextBox).Text = s
                    CType(e.Item.FindControl("txtDate"), TextBox).Visible = True

                    Dim cmd As SqlClient.SqlCommand
                    Dim adapt As SqlClient.SqlDataAdapter
                    Dim ds As DataSet

                    Try
                        cmd = New SqlClient.SqlCommand("get_customer_sale_info")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", e.Item.DataItem("owner_sys_id"))
                        cmd.Parameters.AddWithValue("@pi_all_sale", 1)
                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet
                        adapt.Fill(ds)
                        With CType(e.Item.FindControl("lstSaleDoc"), DropDownList)
                            .DataSource = ds
                            .DataValueField = "sale_sys_id"
                            .DataTextField = "sale"
                            .DataBind()
                            .Items.Insert(0, New ListItem(ClearString, "0"))
                            '                        .SelectedIndex = -1
                            .Items.FindByValue(e.Item.DataItem("sale_sys_id")).Selected = True
                        End With
                    Catch
                        msgAddSupportConduct.Text = Err.Description
                    End Try
                    If CurrentUser.is_admin = True Then
                        CType(e.Item.FindControl("lstSaleDoc"), DropDownList).Visible = True
                        CType(e.Item.FindControl("lblSaleDoc"), Label).Visible = True
                    Else
                        CType(e.Item.FindControl("lstSaleDoc"), DropDownList).Visible = False
                        CType(e.Item.FindControl("lblSaleDoc"), Label).Visible = False
                    End If
                ElseIf e.Item.DataItem("state") = 2 Then
                    'снятие с ТО
                    If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                        s = Format(e.Item.DataItem("dismissal_date"), "dd.MM.yyyy")
                    Else
                        s = ""
                    End If

                    CType(e.Item.FindControl("lbledtStatus"), Label).Text = "Снят с ТО "
                    CType(e.Item.FindControl("txtDate"), TextBox).Text = s
                    CType(e.Item.FindControl("txtDate"), TextBox).Visible = True
                ElseIf e.Item.DataItem("state") = 3 Then
                    'снятие с ТО и в ИМНС
                    If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                        s = Format(e.Item.DataItem("dismissal_date"), "dd.MM.yyyy")
                    Else
                        s = ""
                    End If
                    CType(e.Item.FindControl("lbledtStatus"), Label).Text = "Снят с ТО и в ИМНС "
                    CType(e.Item.FindControl("txtDate"), TextBox).Text = s
                    CType(e.Item.FindControl("txtDate"), TextBox).Visible = True
                End If
                'Marka CTO in
                If Not IsDBNull(e.Item.DataItem("marka_cto_in")) AndAlso Trim(e.Item.DataItem("marka_cto_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaCTOIn"), TextBox).Text = Trim(e.Item.DataItem("marka_cto_in"))
                End If
                'Marka CTO out

                If Not IsDBNull(e.Item.DataItem("marka_cto_out")) AndAlso Trim(e.Item.DataItem("marka_cto_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaCTOOut"), TextBox).Text = Trim(e.Item.DataItem("marka_cto_out"))
                End If

                'Marka CTO2 in
                If (e.Item.DataItem("good_type_sys_id")) = Config.Kasbi04_ID Or (e.Item.DataItem("good_type_sys_id")) = 1119 Then
                    If Not IsDBNull(e.Item.DataItem("marka_cto2_in")) AndAlso Trim(e.Item.DataItem("marka_cto2_in")).Length > 0 Then
                        CType(e.Item.FindControl("txtedtMarkaCTO2In"), TextBox).Text = Trim(e.Item.DataItem("marka_cto2_in"))
                    End If
                    'Marka CTO2 out
                    If Not IsDBNull(e.Item.DataItem("marka_cto2_out")) AndAlso Trim(e.Item.DataItem("marka_cto2_out")).Length > 0 Then
                        CType(e.Item.FindControl("txtedtMarkaCTO2Out"), TextBox).Text = Trim(e.Item.DataItem("marka_cto2_out"))
                    End If
                Else
                    CType(e.Item.FindControl("pnlMarkaCTO2"), Panel).Visible = False
                    'CType(e.Item.FindControl("pnlMarkaCP"), Panel).Visible = False
                End If
                'Marka Reestr in
                If Not IsDBNull(e.Item.DataItem("marka_reestr_in")) AndAlso Trim(e.Item.DataItem("marka_reestr_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaReestrIn"), TextBox).Text = Trim(e.Item.DataItem("marka_reestr_in"))
                End If
                'Marka Reestr out
                If Not IsDBNull(e.Item.DataItem("marka_reestr_out")) AndAlso Trim(e.Item.DataItem("marka_reestr_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaReestrOut"), TextBox).Text = Trim(e.Item.DataItem("marka_reestr_out"))
                End If
                'Marka PZU in
                If Not IsDBNull(e.Item.DataItem("marka_pzu_in")) AndAlso Trim(e.Item.DataItem("marka_pzu_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaPZUIn"), TextBox).Text = Trim(e.Item.DataItem("marka_pzu_in"))
                End If
                'Marka PZU out
                If Not IsDBNull(e.Item.DataItem("marka_pzu_out")) AndAlso Trim(e.Item.DataItem("marka_pzu_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaPZUOut"), TextBox).Text = Trim(e.Item.DataItem("marka_pzu_out"))
                End If
                'Marka MFP in
                If Not IsDBNull(e.Item.DataItem("marka_mfp_in")) AndAlso Trim(e.Item.DataItem("marka_mfp_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaMFPIn"), TextBox).Text = Trim(e.Item.DataItem("marka_mfp_in"))
                End If
                'Marka MFP out
                If Not IsDBNull(e.Item.DataItem("marka_mfp_out")) AndAlso Trim(e.Item.DataItem("marka_mfp_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaMFPOut"), TextBox).Text = Trim(e.Item.DataItem("marka_mfp_out"))
                End If
                'Marka CP in
                If Not IsDBNull(e.Item.DataItem("marka_cp_in")) AndAlso Trim(e.Item.DataItem("marka_cp_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaCPIn"), TextBox).Text = Trim(e.Item.DataItem("marka_cp_in"))
                End If
                'Marka CP out
                If Not IsDBNull(e.Item.DataItem("marka_cp_out")) AndAlso Trim(e.Item.DataItem("marka_cp_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtMarkaCPOut"), TextBox).Text = Trim(e.Item.DataItem("marka_cp_out"))
                End If

                'ZReport in
                If Not IsDBNull(e.Item.DataItem("zreport_in")) AndAlso Trim(e.Item.DataItem("zreport_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtZReportIn"), TextBox).Text = Trim(e.Item.DataItem("zreport_in"))
                End If
                'ZReport out
                If Not IsDBNull(e.Item.DataItem("zreport_out")) AndAlso Trim(e.Item.DataItem("zreport_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtZReportOut"), TextBox).Text = Trim(e.Item.DataItem("zreport_out"))
                End If
                'Itog in
                If Not IsDBNull(e.Item.DataItem("itog_in")) AndAlso Trim(e.Item.DataItem("itog_in")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtItogIn"), TextBox).Text = Trim(e.Item.DataItem("itog_in"))
                End If
                'Itog out
                If Not IsDBNull(e.Item.DataItem("itog_out")) AndAlso Trim(e.Item.DataItem("itog_out")).Length > 0 Then
                    CType(e.Item.FindControl("txtedtItogOut"), TextBox).Text = Trim(e.Item.DataItem("itog_out"))
                End If
                'дата когда закрыто ТО за месяц
                s = ""
                If Not IsDBNull(e.Item.DataItem("change_state_date")) Then
                    s = s & Format(e.Item.DataItem("change_state_date"), "dd/MM/yyyy")
                End If
                CType(e.Item.FindControl("lbledtCloseDate"), Label).Text = s
                ' дополнительная информация
                CType(e.Item.FindControl("txtInfoEdit"), TextBox).Text = e.Item.DataItem("info")
                'Исполнитель
                Dim adapt1 As SqlClient.SqlDataAdapter
                Dim ds1 As DataSet
                Try
                    adapt1 = dbSQL.GetDataAdapter("get_salers")
                    ds1 = New DataSet
                    adapt1.Fill(ds1)
                    With CType(e.Item.FindControl("lstExecutor"), DropDownList)
                        .DataSource = ds1
                        .DataValueField = "sys_id"
                        .DataTextField = "name"
                        .DataBind()
                        .Items.FindByText(e.Item.DataItem("executor")).Selected = True
                    End With
                Catch
                    msgAddSupportConduct.Text = "Ошибка формирования списков!<br>" & Err.Description
                    Exit Sub

                End Try
            End If
        End Sub

        Private Sub grdSupportConductHistory_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.UpdateCommand
            Dim cmd As SqlClient.SqlCommand
            Dim d As DateTime
            Dim s As String
            Dim s1 As String()

            Try
                Try
                    s = CType(e.Item.FindControl("txtDate"), TextBox).Text.Trim
                    s1 = s.Split("./".ToCharArray())
                    d = New Date(CInt(s1.GetValue(2)), CInt(s1.GetValue(1)), CInt(s1.GetValue(0)))
                Catch
                    msgAddSupportConduct.Text = "Неверный формат даты! Формат для ввода - ДД.ММ.ГГГГ"
                End Try

                cmd = New SqlClient.SqlCommand("update_cash_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sys_id", grdSupportConductHistory.DataKeys(e.Item.ItemIndex))
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.Parameters.AddWithValue("@pi_marka_cto_in", CType(e.Item.FindControl("txtedtMarkaCTOIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto_out", CType(e.Item.FindControl("txtedtMarkaCTOOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_pzu_in", CType(e.Item.FindControl("txtedtMarkaPZUIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_pzu_out", CType(e.Item.FindControl("txtedtMarkaPZUOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_mfp_in", CType(e.Item.FindControl("txtedtMarkaMFPIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_mfp_out", CType(e.Item.FindControl("txtedtMarkaMFPOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_reestr_in", CType(e.Item.FindControl("txtedtMarkaReestrIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_reestr_out", CType(e.Item.FindControl("txtedtMarkaReestrOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto2_in", CType(e.Item.FindControl("txtedtMarkaCTO2In"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto2_out", CType(e.Item.FindControl("txtedtMarkaCTO2Out"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_cp_in", CType(e.Item.FindControl("txtedtMarkaCPIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_marka_cp_out", CType(e.Item.FindControl("txtedtMarkaCPOut"), TextBox).Text)

                cmd.Parameters.AddWithValue("@pi_zreport_in", CType(e.Item.FindControl("txtedtZReportIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_zreport_out", CType(e.Item.FindControl("txtedtZReportOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_itog_in", CType(e.Item.FindControl("txtedtItogIn"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_itog_out", CType(e.Item.FindControl("txtedtItogOut"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_info", CType(e.Item.FindControl("txtInfoEdit"), TextBox).Text)
                cmd.Parameters.AddWithValue("@pi_executor", CType(e.Item.FindControl("lstExecutor"), DropDownList).SelectedValue)
                cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                Dim lst As DropDownList = CType(e.Item.FindControl("lstSaleDoc"), DropDownList)
                If lst.Visible = True Then
                    If lst.SelectedIndex > 0 Then
                        cmd.Parameters.AddWithValue("@pi_sale_id", lst.SelectedValue)
                    End If
                End If

                Dim txt As TextBox
                txt = CType(e.Item.FindControl("txtPeriod"), TextBox)
                If Not txt Is Nothing And txt.Text <> "" Then
                    cmd.Parameters.AddWithValue("@pi_period", CInt(txt.Text))
                End If
                cmd.Parameters.AddWithValue("@pi_date", d)
                dbSQL.Execute(cmd)
            Catch
                msgAddSupportConduct.Text = "Не удается обновить запись!<br>" & Err.Description
            End Try
            grdSupportConductHistory.EditItemIndex = -1
            LoadGoodInfo()
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Январь ", " Февраль ", " Март ", " Апрель ", " Май ", " Июнь ", " Июль ", " Август ", " Сентябрь ", " Октябрь ", " Ноябрь ", " Декабрь "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDateFull(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDateFull = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Private Sub btnExpand1_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExpand1.Click
            Expand(pnlSupportConductHistory_body, btnExpand1)
        End Sub

        Private Sub Expand(ByVal section As System.Web.UI.WebControls.Panel, ByVal button As System.Web.UI.WebControls.ImageButton)
            If section.Visible = True Then
                section.Visible = False
                button.ImageUrl = "Images/collapsed.gif"
                button.ToolTip = "Показать"
            Else
                section.Visible = True
                button.ImageUrl = "Images/expanded.gif"
                button.ToolTip = "Скрыть"
            End If
        End Sub
        Private Sub btnSaveSKNOInfo_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSaveSKNOInfo.Click
            Dim test As String = iCash
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            cmd = New SqlClient.SqlCommand("insert_skno_history")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
            cmd.Parameters.AddWithValue("@pi_state_skno", rbSKNO.SelectedValue)
            cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
            cmd.Parameters.AddWithValue("@pi_comment", "")
            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

        End Sub


        Private Sub rbTO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTO.SelectedIndexChanged
            If rbTO.SelectedIndex = 2 And chkDelayTO.Checked = False Then
                pnlConduct.Visible = True
                pnlDelay.Visible = False
                pnlDismissal.Visible = False
                chkDelayTO.Visible = True
                pnlSupport.Visible = False
                SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Проведение технического обслуживания"
            ElseIf rbTO.SelectedIndex = 2 And chkDelayTO.Checked = True Then
                pnlConduct.Visible = False
                pnlDelay.Visible = True
                pnlDismissal.Visible = False
                pnlSupport.Visible = False
                chkDelayTO.Visible = True
                SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Приостановка технического обслуживания"
            ElseIf rbTO.SelectedIndex = 1 Then
                pnlConduct.Visible = False
                pnlDelay.Visible = False
                pnlDismissal.Visible = True
                pnlSupport.Visible = False
                chkDelayTO.Visible = False
                SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Снятие с технического обслуживания"
            ElseIf rbTO.SelectedIndex = 0 Then
                pnlConduct.Visible = False
                pnlDelay.Visible = False
                pnlDismissal.Visible = False
                pnlSupport.Visible = True
                'txtMarkaCTO.Enabled = True
                chkDelayTO.Visible = False
                Session("CustFilter") = ""
                txtCustomerFind.Text = ""
                LoadCustomerList()
                SectionTOName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Постановка на техническое обслуживание"
            End If
        End Sub

        Public Sub LoadCustomerList(Optional ByVal sRequest$ = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            Try
                If sRequest = "" Then
                    If Session("CustFilter") <> "" Then
                        s = Session("CustFilter")
                    Else
                        s = ""
                    End If
                Else
                    s = sRequest
                End If

                If CurrentCustomer.ToString <> "" And s = "" Then
                    s &= " c.customer_sys_id=" & CurrentCustomer.ToString
                End If

                cmd = New SqlClient.SqlCommand("get_customer_for_support")
                cmd.Parameters.AddWithValue("@pi_filter", s)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                lstCustomers.DataSource = ds.Tables(0).DefaultView
                lstCustomers.DataTextField = "customer_name"
                lstCustomers.DataValueField = "customer_sys_id"
                lstCustomers.DataBind()
                lstCustomers.SelectedIndex = -1
                lstCustomers.Items.Insert(0, New ListItem(ClearString, "0"))

                Dim item As ListItem = lstCustomers.Items.FindByValue(CurrentCustomer)
                If item Is Nothing Then
                    lstCustomers.SelectedIndex = 0
                Else
                    item.Selected = True
                End If
                If lstCustomers.SelectedIndex = 0 Then
                    lblCustInfo.Text = ""
                    lblSaleInfo.Visible = False
                    cmbSalesInfo.Items.Clear()
                    cmbSalesInfo.Visible = False
                    txtDogovor.Text = ""
                    txtDogovor.Visible = False
                    lblDogovor.Visible = False
                End If
                LoadSaleByCustomer()
                cmbSalesInfo_SelectedIndexChanged(Nothing, Nothing)
            Catch
                msgAddSupportConduct.Text = Err.Description
            End Try
        End Sub

        Private Sub chkDelayTO_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkDelayTO.CheckedChanged
            rbTO_SelectedIndexChanged(Me, Nothing)
        End Sub

        Private Sub grdSupportConductHistory_DeleteCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.DeleteCommand
            'Ограничение прав на удаление
            If Session("rule20") = "1" Then
                Try
                    dbSQL.Execute("delete cash_history where sys_id = " & grdSupportConductHistory.DataKeys(e.Item.ItemIndex))
                Catch
                    msgSupportConductHistory.Text = "Ошибка удаления записи!<br>" & Err.Description
                End Try
                grdSupportConductHistory.EditItemIndex = -1
                LoadGoodInfo()
            End If
        End Sub

        Private Sub grdSupportConductHistory_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.ItemCommand
            If e.CommandName = "DeleteDoc" Then
                Dim docs As New Kasbi.Migrated_Documents
                Try
                    docs.DeleteHistoryDocument(grdSupportConductHistory.DataKeys(e.Item.ItemIndex))
                Catch
                    msg.Text = "Ошибка удаления документов!<br>" & Err.Description
                    Exit Sub
                End Try
            End If
        End Sub

        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text

            If Trim(str).Length = 0 Then LoadCustomerList() : Exit Sub
            If str.IndexOf("'") > -1 Then Exit Sub
            Dim s$ = " (customer_name like '%" & str & "%')"
            LoadCustomerList(s)
            Session("CustFilter") = s
        End Sub

        Private Sub lnkGoods_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkGoods.Click
            Dim sFilter$ = "num_cashregister like '%" & lblCash.Text.TrimStart("№").Trim() & "%'"
            If sFilter.Length > 0 Then
                Session("FilterGood") = sFilter
            End If
            Response.Redirect(GetAbsoluteUrl("~/GoodList.aspx"))
        End Sub

        Function SaveRebillingInfo() As Integer
            Dim cmd As SqlClient.SqlCommand
            Dim i%, sSubDogovor$, dogovor%, s$, iSale%
            Dim param As SqlClient.SqlParameter
            Dim ch() As Char = {"\", "/", ".", "-"}
            Dim cust%

            If lstCustomers.SelectedIndex <= 0 Then
                msgAddSupportConduct.Text = "Не выбран ни один клиент!"
                Exit Function
            End If

            cust = lstCustomers.SelectedItem.Value
            If cust = 0 Then
                msgAddSupportConduct.Text = "Ошибка определения клиента!"
                Exit Function
            End If
            'определяем номер договора и поддоговора
            If cmbSalesInfo.SelectedItem.Value = "0" Then
                Try
                    s = txtDogovor.Text
                    i = s.IndexOfAny(ch)
                    If i > 0 Then
                        sSubDogovor = s.Substring(i)
                    Else
                        sSubDogovor = ""
                    End If
                    If i > 0 Then
                        dogovor = CInt(s.Substring(0, i))
                    Else
                        dogovor = CInt(s)
                    End If
                Catch
                    msgAddSupportConduct.Text = "Укажите правильный номер договора/поддоговора"
                    Exit Function
                End Try
                Try
                    cmd = New SqlClient.SqlCommand("new_sale")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                    cmd.Parameters.AddWithValue("@pi_saler_sys_id", CurrentUser.sys_id)
                    cmd.Parameters.AddWithValue("@pi_firm_sys_id", 1)
                    cmd.Parameters.AddWithValue("@pi_info", DBNull.Value)

                    Dim rebilling_date As DateTime = New DateTime
                    Try
                        rebilling_date = DateTime.Parse(tbxSupportDate.Text)
                    Catch
                        msgAddSupportConduct.Text = "Пожалуйста, введите корректное значение даты переоформления"
                        Exit Function
                    End Try

                    cmd.Parameters.AddWithValue("@pi_sale_date", rebilling_date)
                    cmd.Parameters.AddWithValue("@pi_state", 4)
                    cmd.Parameters.AddWithValue("@pi_type", 1)
                    cmd.Parameters.AddWithValue("@pi_dogovor", sSubDogovor)
                    cmd.Parameters.AddWithValue("@pi_proxy", "")
                    param = New SqlClient.SqlParameter
                    param.Direction = ParameterDirection.Output
                    param.SqlDbType = SqlDbType.Int
                    param.ParameterName = "@po_sale_sys_id"
                    cmd.Parameters.Add(param)
                    dbSQL.Execute(cmd)
                    iSale = cmd.Parameters("@po_sale_sys_id").Value
                Catch
                    msgAddSupportConduct.Text = "Ошибка добавления нового заказа!<br>" & Err.Description
                    Exit Function
                End Try
            Else
                iSale = cmbSalesInfo.SelectedItem.Value
            End If

            SaveRebillingInfo = iSale

        End Function

        Protected Sub lstGoodType_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGoodType.Load

        End Sub

        Protected Sub lnkAkt_TO_OnClick(sender As Object, e As EventArgs)
            CType(sender, LinkButton).Parent.FindControl("")
        End Sub
    End Class

End Namespace
