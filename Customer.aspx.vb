Namespace Kasbi

Partial Class Customer
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
    Const ClearString = "-------"
    Dim customer_sys_id%
        Dim bank_id$
    Dim icash%

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Const javascript = "javascript:"

            'lnkAccountant.Attributes.Add("onclick", "javascript:" & txtAccountant.ClientID & ".value=" & txtBoosLastName.ClientID & ".value + ' ' + " & txtBoosFirstName.ClientID & ".value + ' ' + " & txtBoosPatronymicName.ClientID & ".value;")
            txtUNN.Attributes.Add("onchange", javascript & txtOKPO.ClientID & ".value=" & txtUNN.ClientID & ".value;")
            txtBoosLastName.Attributes.Add("onchange", javascript & txtAccountant.ClientID & ".value=" & txtBoosLastName.ClientID & ".value + ' ' + " & txtBoosFirstName.ClientID & ".value + ' ' + " & txtBoosPatronymicName.ClientID & ".value;")
            txtBoosFirstName.Attributes.Add("onchange", javascript & txtAccountant.ClientID & ".value=" & txtBoosLastName.ClientID & ".value + ' ' + " & txtBoosFirstName.ClientID & ".value + ' ' + " & txtBoosPatronymicName.ClientID & ".value;")
            txtBoosPatronymicName.Attributes.Add("onchange", javascript & txtAccountant.ClientID & ".value=" & txtBoosLastName.ClientID & ".value + ' ' + " & txtBoosFirstName.ClientID & ".value + ' ' + " & txtBoosPatronymicName.ClientID & ".value;")
            rdbtnIP.Attributes.Add("onclick", javascript & chkNDS.ClientID & ".checked=false;" & txtCustomerName.ClientID & ".disabled=true;" & txtCustomerAbr.ClientID & ".disabled=true;" & txtBranch.ClientID & ".disabled=true;")
            rdbtnOrganization.Attributes.Add("onclick", javascript & chkNDS.ClientID & ".checked=true;" & txtCustomerName.ClientID & ".disabled=false;" & txtCustomerAbr.ClientID & ".disabled=false;" & txtBranch.ClientID & ".disabled=false;")
            Dim sHide$ = lstCity.ClientID & ".style.display='"
            Dim sSetValue$ = txtCity.ClientID & ".value=this.options[this.selectedIndex].text;"
            btnCity.Attributes.Add("onclick", javascript & sHide & "block';" & lstCity.ClientID & ".focus();")
            txtCity.Attributes.Add("ondblclick", javascript & sHide & "block';" & lstCity.ClientID & ".focus();")
            lstCity.Attributes.Add("onchange", javascript & sSetValue & sHide & "none';")
            lstCity.Attributes.Add("onfocusout", javascript & sHide & "none';")

            Dim sHide2$ = lstCustomerAbr.ClientID & ".style.display='"
            Dim sSetValue2$ = txtCustomerAbr.ClientID & ".value=this.options[this.selectedIndex].text;"
            btnCustomerAbr.Attributes.Add("onclick", javascript & sHide2 & "block';" & lstCustomerAbr.ClientID & ".focus();")
            txtCustomerAbr.Attributes.Add("ondblclick", javascript & sHide2 & "block';" & lstCustomerAbr.ClientID & ".focus();")
            lstCustomerAbr.Attributes.Add("onchange", javascript & sSetValue2 & sHide2 & "none';")
            lstCustomerAbr.Attributes.Add("onfocusout", javascript & sHide2 & "none';")

            bank_id = ""
            icash = 0
            Dim ch() As Char = {","}
            icash = CInt(Request.Params(0).Split(ch).GetValue(1))

            If Not IsPostBack Then
                BindLists()
                Try
                    If Request.Params.Count > 0 Then
                        customer_sys_id = Request.Params(0).Split(ch).GetValue(0)
                    Else
                        customer_sys_id = Session("AddSaleForCustomer")
                    End If
                    Session("AddSaleForCustomer") = customer_sys_id
                Catch
                    Try
                        If Session("AddSaleForCustomer") <> "" Then customer_sys_id = Session("AddSaleForCustomer")
                    Catch
                    End Try
                Finally
                    GetCustomer()
                End Try
            End If
        End Sub

        Sub BindLists()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            'список банков
            Try
                adapt = dbSQL.GetDataAdapter("select bank_sys_id,bank_code from bank order by bank_code")
                ds = New DataSet
                adapt.Fill(ds)
                lstBank.DataSource = ds.Tables(0).DefaultView
                lstBank.DataTextField = "bank_code"
                lstBank.DataValueField = "bank_sys_id"
                lstBank.DataBind()
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка банков!<br>" & Err.Description
                Exit Sub
            End Try
            'lstBank_SelectedIndexChanged(Me, Nothing)

            'список городов
            Try
                adapt = dbSQL.GetDataAdapter("select distinct city from customer where city is not null and ltrim(city)<>'' order by city")
                ds = New DataSet
                adapt.Fill(ds)
                lstCity.DataSource = ds.Tables(0).DefaultView
                lstCity.DataTextField = "city"
                lstCity.DataValueField = "city"
                lstCity.DataBind()
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка городов!<br>" & Err.Description
                Exit Sub
            End Try

            'список аббревиатур организаций
            Try
                adapt = dbSQL.GetDataAdapter("select distinct customer_abr from customer where customer_abr is not null and ltrim(customer_abr)<>'' order by customer_abr")
                ds = New DataSet
                adapt.Fill(ds)
                lstCustomerAbr.DataSource = ds.Tables(0).DefaultView
                lstCustomerAbr.DataTextField = "customer_abr"
                lstCustomerAbr.DataValueField = "customer_abr"
                lstCustomerAbr.DataBind()
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка аббревиатур организаций!<br>" & Err.Description
                Exit Sub
            End Try

            'список налоговых инспекций
            Try
                adapt = dbSQL.GetDataAdapter("prc_getIMNS")
                ds = New DataSet
                adapt.Fill(ds)
                dlstIMNS.DataSource = ds.Tables(0).DefaultView
                dlstIMNS.DataValueField = "sys_id"
                dlstIMNS.DataTextField = "imns_name"
                dlstIMNS.DataBind()
                dlstIMNS.Items.Insert(0, New ListItem(ClearString, "0"))
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка налоговых инспекций!<br>" & Err.Description
                Exit Sub
            End Try
            'список рекламы
            Try
                adapt = dbSQL.GetDataAdapter("prc_getAdvertising", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstAdvertising.DataSource = ds.Tables(0).DefaultView
                lstAdvertising.DataValueField = "Advertise_id"
                lstAdvertising.DataTextField = "Adv_Name"
                lstAdvertising.DataBind()
                lstAdvertising.Items.Insert(0, New ListItem(ClearString, "0"))
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка рекламы!<br>" & Err.Description
                Exit Sub
            End Try

        End Sub

        Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnClear.Click
            ClearFields()
        End Sub

        Private Sub ClearFields()
            txtCustomerName.Text = ""
            txtCustomerAbr.Text = ""
            txtBoosLastName.Text = ""
            txtBoosFirstName.Text = ""
            txtBoosPatronymicName.Text = ""
            txtAccountant.Text = ""
            txtUNN.Text = ""
            txtOKPO.Text = ""
            txtZipCode.Text = ""
            txtRegion.Text = ""
            lstRegion.SelectedIndex = 0
            lstCityAbr.SelectedIndex = 0
            lstStreetAbr.SelectedIndex = 0
            txtCity.Text = ""
            txtAddress.Text = ""
            txtPhone1.Text = ""
            txtPhone2.Text = ""
            txtPhone3.Text = ""
            txtPhone4.Text = ""
            txtTaxInspection.Text = ""
            chkNDS.Checked = False
            txtBankName.Text = ""
            txtBankAddress.Text = ""
            txtBankAccount.Text = ""
            lstBank.SelectedIndex = 0
            txtRegistration.Text = ""
            txtBranch.Text = ""
            txtInfo.Text = ""
            txtProxy.Text = ""
            BindLists()
        End Sub

        Private Sub GetBankNameAddress(ByVal bank_id As String, ByRef BankName As String, ByRef BankAddress As String, ByRef msgctrl As Label)
            Dim reader As SqlClient.SqlDataReader

            Try
                reader = dbSQL.GetReader("select name,address from bank where bank_sys_id='" & bank_id & "'")
                If reader.Read() Then
                    BankName = reader.Item(0)
                    BankAddress = reader.Item(1)
                Else
                    msgctrl.Text = "Ошибка получения информации о банке!<br>" & Err.Description
                    Exit Sub
                End If
            Catch
                msgAddCustomer.Text = "Ошибка получения информации о банке!<br>" & Err.Description
                Exit Sub
            Finally
                reader.Close()
            End Try

        End Sub

        Private Sub lstBank_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstBank.SelectedIndexChanged
            Dim s1 As String, s2 As String
            If bank_id.Trim <> "" Then
                lstBank.SelectedItem.Selected = False
                lstBank.Items.FindByValue(bank_id.Trim).Selected = True
            End If
            GetBankNameAddress(lstBank.SelectedItem.Value, s1, s2, msgAddCustomer)
            txtBankName.Text = s1
            txtBankAddress.Text = s2
        End Sub

        Private Function GetCustomer() As Boolean
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim sRegion As String
            Dim ii As Integer

            Try
                cmd = New SqlClient.SqlCommand("get_customer")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", customer_sys_id)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
            Catch
                msgAddCustomer.Text = "Невозможно получить информацию о пользователе!<br>" & Err.Description
                GetCustomer = False
                Exit Function
            End Try

            If ds.Tables(0).Rows.Count = 0 Then
                GetCustomer = False
            Else
                GetCustomer = True
                Try
                    With ds.Tables(0).Rows(0)
                        rdbtnIP.Checked = CStr(.Item("customer_abr")).Trim.ToUpper = "ИП"
                        txtCustomerName.Text = .Item("customer_name")
                        txtCustomerAbr.Text = .Item("customer_abr")
                        txtBoosLastName.Text = .Item("boos_last_name")
                        txtBoosFirstName.Text = .Item("boos_first_name")
                        txtBoosPatronymicName.Text = .Item("boos_patronymic_name")
                        txtProxy.Text = .Item("boos_last_name") & " " & .Item("boos_first_name") & " " & .Item("boos_patronymic_name")
                        txtAccountant.Text = .Item("accountant")
                        txtUNN.Text = .Item("unn")
                        txtOKPO.Text = .Item("okpo")
                        txtZipCode.Text = .Item("zipcode")
                        sRegion = .Item("region")
                        For ii = 1 To lstRegion.Items.Count - 1
                            If sRegion.IndexOf(lstRegion.Items(ii).Value) > -1 Then
                                lstRegion.SelectedIndex = ii
                                sRegion = sRegion.Substring(lstRegion.SelectedItem.Value.Length)
                                If sRegion.Length = 0 Or sRegion.Trim = "," Then
                                    txtRegion.Text = ""
                                Else
                                    txtRegion.Text = sRegion.Trim.TrimStart(",").Trim
                                End If
                                Exit For
                            End If
                        Next
                        Dim item As ListItem = lstCityAbr.Items.FindByText(CStr(.Item("city_abr")).Trim)
                        If Not item Is Nothing Then item.Selected = True
                        txtCity.Text = .Item("city")
                        item = lstStreetAbr.Items.FindByText(CStr(.Item("street_abr")).Trim)
                        If Not item Is Nothing Then item.Selected = True
                        txtAddress.Text = .Item("address")
                        txtPhone1.Text = .Item("phone1")
                        txtPhone2.Text = .Item("phone2")
                        txtPhone3.Text = .Item("phone3")
                        txtPhone4.Text = .Item("phone4")
                        txtTaxInspection.Text = .Item("tax_inspection")
                        If Not IsDBNull(.Item("imns_sys_id")) Then
                            item = dlstIMNS.Items.FindByValue(.Item("imns_sys_id"))
                            If Not item Is Nothing Then item.Selected = True
                        End If
                        If Not IsDBNull(.Item("Advertise_id")) Then
                            item = lstAdvertising.Items.FindByValue(.Item("Advertise_id"))
                            If Not item Is Nothing Then item.Selected = True
                        End If
                        chkNDS.Checked = .Item("NDS")
                        If Not IsDBNull(.Item("cto")) Then
                            chkCTO.Checked = .Item("cto")
                            Session("CTO") = .Item("cto")
                        Else
                            chkCTO.Checked = False
                            Session("CTO") = False
                        End If
                        chkSupport.Checked = Not (Not IsDBNull(.Item("support")) AndAlso (.Item("support") = 0))
                        bank_id = ""

                        bank_id = .Item("bank_sys_id")
                        lstBank_SelectedIndexChanged(Me, Nothing)

                        If .Item("bank_address") <> "" Then
                            txtBankAddress.Text = .Item("bank_address")
                        End If
                        txtBankAccount.Text = .Item("bank_account")
                        txtRegistration.Text = .Item("registration")
                        txtBranch.Text = .Item("branch")
                        txtInfo.Text = .Item("info")
                        txtDogovor.Text = .Item("dogovor")
                        chkAlert.Checked = Not (Not IsDBNull(.Item("alert")) AndAlso (.Item("alert") = 0))
                        'новый номер договора
                        adapt = dbSQL.GetDataAdapter("select case when dogovor is null then '' else dogovor end dogovor from sale where customer_sys_id=" & customer_sys_id & " order by d DESC")
                        ds = New DataSet
                        adapt.Fill(ds)
                        Dim ch() As Char = {"\", "/", ".", "-"}
                    End With
                Catch
                    msgAddCustomer.Text = "Ошибка получения информации о пользователе!<br>" & Err.Description
                    Exit Function

                End Try
            End If
        End Function

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancel.Click
            ClearFields()
            Response.Redirect(GetAbsoluteUrl("~/CashOwners.aspx?" & icash & "&cashowner=" & Session("AddSaleForCustomer")))
        End Sub

        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            Dim cmd As SqlClient.SqlCommand
            Dim param As SqlClient.SqlParameter
            Dim iCustomer As Integer
            Dim ch() As Char = {"\", "/", ".", "-"}
            Dim sTmp As String

            'Нужно ли добавлять пользователя
            Try
                customer_sys_id = Session("AddSaleForCustomer")
            Catch
                customer_sys_id = 0
            End Try

            Dim advertise_id As Object = lstAdvertising.SelectedItem.Value
            If (lstAdvertising.SelectedItem.Value = "0") Then
                advertise_id = DBNull.Value
            End If
            Dim imns_id As Object = dlstIMNS.SelectedItem.Value
            If (dlstIMNS.SelectedItem.Value = "0") Then
                imns_id = DBNull.Value
            End If
            Dim bank_id As Object = lstBank.SelectedItem.Value
            If (lstBank.SelectedItem.Value = "0") Then
                bank_id = DBNull.Value
            End If

            If customer_sys_id = 0 Then
                'добавляем нового клиента 
                Try
                    cmd = New SqlClient.SqlCommand("new_customer")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_abr", IIf(rdbtnIP.Checked, "ИП", txtCustomerAbr.Text.Replace("'", """")))
                    cmd.Parameters.AddWithValue("@pi_customer_name", IIf(rdbtnIP.Checked, txtBoosLastName.Text.Replace("'", """").Trim() & " " & txtBoosFirstName.Text.Replace("'", """").Trim() & " " & txtBoosPatronymicName.Text.Replace("'", """").Trim(), txtCustomerName.Text.Replace("'", """")))
                    cmd.Parameters.AddWithValue("@pi_boos_last_name", txtBoosLastName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_first_name", txtBoosFirstName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_patronymic_name", txtBoosPatronymicName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_accountant", txtAccountant.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_unn", txtUNN.Text)
                    cmd.Parameters.AddWithValue("@pi_okpo", txtOKPO.Text)
                    cmd.Parameters.AddWithValue("@pi_zipcode", txtZipCode.Text.Replace("'", """"))
                    sTmp = txtRegion.Text.Trim
                    If sTmp.IndexOf(" ") > -1 Then
                        sTmp = sTmp.Substring(0, sTmp.IndexOf(" "))
                    End If
                    cmd.Parameters.AddWithValue("@pi_region", lstRegion.SelectedItem.Value & IIf(txtRegion.Text.Trim.Length > 0, ", " & sTmp & " р-н", ""))
                    cmd.Parameters.AddWithValue("@pi_city_abr", lstCityAbr.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_city", txtCity.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_street_abr", lstStreetAbr.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_address", txtAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone1", txtPhone1.Text)
                    cmd.Parameters.AddWithValue("@pi_phone2", txtPhone2.Text)
                    cmd.Parameters.AddWithValue("@pi_phone3", txtPhone3.Text)
                    cmd.Parameters.AddWithValue("@pi_phone4", txtPhone4.Text)
                    cmd.Parameters.AddWithValue("@pi_tax_inspection", txtTaxInspection.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_imns_sys_id", imns_id)
                    cmd.Parameters.AddWithValue("@pi_NDS", -1 * CInt(chkNDS.Checked))
                    cmd.Parameters.AddWithValue("@pi_CTO", -1 * CInt(chkCTO.Checked))
                    cmd.Parameters.AddWithValue("@pi_Support", -1 * CInt(chkSupport.Checked))
                    cmd.Parameters.AddWithValue("@pi_bank_sys_id", bank_id)
                    cmd.Parameters.AddWithValue("@pi_bank_code", lstBank.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@pi_bank_account", txtBankAccount.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_bank_address", txtBankAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_registration", txtRegistration.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_branch", txtBranch.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_info", txtInfo.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_dogovor", txtDogovor.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_alert", -1 * CInt(chkAlert.Checked))
                    cmd.Parameters.AddWithValue("@pi_advertise_id", advertise_id)
                    param = New SqlClient.SqlParameter
                    param.Direction = ParameterDirection.Output
                    param.ParameterName = "@po_customer_sys_id"
                    param.SqlDbType = SqlDbType.Int
                    cmd.Parameters.Add(param)

                    dbSQL.Execute(cmd)
                    iCustomer = cmd.Parameters("@po_customer_sys_id").Value
                Catch
                    msgAddCustomer.Text = "Ошибка добавления нового клиента!<br>" & Err.Description
                    Exit Sub
                End Try
            Else
                iCustomer = customer_sys_id
                Try
                    cmd = New SqlClient.SqlCommand("update_customer")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                    cmd.Parameters.AddWithValue("@pi_customer_abr", txtCustomerAbr.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_customer_name", txtCustomerName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_last_name", txtBoosLastName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_first_name", txtBoosFirstName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_patronymic_name", txtBoosPatronymicName.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_accountant", txtAccountant.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_unn", txtUNN.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_okpo", txtOKPO.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_zipcode", txtZipCode.Text.Replace("'", """"))
                    sTmp = txtRegion.Text.Replace("'", """").Trim
                    If sTmp.IndexOf(" ") > -1 Then
                        sTmp = sTmp.Substring(0, sTmp.IndexOf(" "))
                    End If
                    cmd.Parameters.AddWithValue("@pi_region", lstRegion.SelectedItem.Value & IIf(txtRegion.Text.Replace("'", """").Trim.Length > 0, ", " & sTmp & " р-н", ""))
                    cmd.Parameters.AddWithValue("@pi_city_abr", lstCityAbr.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_city", txtCity.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_street_abr", lstStreetAbr.SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_address", txtAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone1", txtPhone1.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone2", txtPhone2.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone3", txtPhone3.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone4", txtPhone4.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_tax_inspection", txtTaxInspection.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_imns_sys_id", imns_id)
                    cmd.Parameters.AddWithValue("@pi_NDS", chkNDS.Checked)
                    cmd.Parameters.AddWithValue("@pi_CTO", chkCTO.Checked)
                    cmd.Parameters.AddWithValue("@pi_Support", chkSupport.Checked)
                    cmd.Parameters.AddWithValue("@pi_bank_sys_id", bank_id)
                    cmd.Parameters.AddWithValue("@pi_bank_code", lstBank.SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@pi_bank_account", txtBankAccount.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_bank_address", txtBankAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_registration", txtRegistration.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_branch", txtBranch.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_dogovor", txtDogovor.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_alert", chkAlert.Checked)
                    cmd.Parameters.AddWithValue("@pi_info", txtInfo.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_advertise_id", advertise_id)
                    dbSQL.Execute(cmd)
                Catch
                    msgAddCustomer.Text = "Ошибка обновления записи!<br>" & Err.Description
                End Try
            End If

            Session("Customer") = iCustomer
            Session("CustomerInfo") = txtCustomerName.Text & "<br>" & txtRegistration.Text & "<br>" & txtUNN.Text
            Session("CurrentPage") = "CashOwner"
            ClearFields()
            Response.Redirect(GetAbsoluteUrl("~/CashOwners.aspx?" & icash & "&cashowner=" & iCustomer))
        End Sub

End Class

End Namespace
