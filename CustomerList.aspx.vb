
Imports System.Windows.Forms

Namespace Kasbi

    Partial Class CustomerList
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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Dim sFilter$

            'Ограничение прав
            If Session("rule4") = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)
            If Session("rule5") = "0" Then btnNew.Visible = False

            If Not IsPostBack Then
                'Session("Filter") = ""
                Session("Filter") = "SELECT TOP 30 * FROM customer WHERE (cto is null or cto <> 1) ORDER BY customer_sys_id DESC"
                sFilter = Session("CustomerFilter")
                If sFilter = "" Then
                    pnlFilter.Visible = False
                    Bind()
                    Session("CustomerFilter") = "False"
                Else
                    Try
                        pnlFilter.Visible = CBool(sFilter)
                    Catch
                        pnlFilter.Visible = False
                    End Try
                    Bind()
                End If
            End If
            txtFilter.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind();}")
        End Sub

        Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
            pnlFilter.Visible = Not pnlFilter.Visible
            Session("CustomerFilter") = CStr(pnlFilter.Visible)
        End Sub

        Private Sub GetBankNameAddress(ByVal bank_id As String, ByRef BankName As String, ByRef BankAddress As String, ByRef msgctrl As WebControls.Label)
            Dim reader As SqlClient.SqlDataReader

            Try
                reader = dbSQL.GetReader("select name,address from bank where bank_sys_id='" & bank_id & "'")
                If reader.Read() Then
                    BankName = reader.Item(0)
                    BankAddress = reader.Item(1)
                Else
                    'msgctrl.Text = "Ошибка получения информации о банке!<br>" & Err.Description
                    Exit Sub
                End If
            Catch
                'msgctrl.Text = "Ошибка получения информации о банке!<br>" & Err.Description
                Exit Sub
            Finally
                reader.Close()
            End Try

        End Sub

        Sub BindBankList(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select bank_sys_id,bank_code from bank order by bank_code")
                ds = New DataSet
                adapt.Fill(ds)
                lst.DataSource = ds.Tables(0).DefaultView
                lst.DataTextField = "bank_code"
                lst.DataValueField = "bank_sys_id"
                lst.DataBind()
                Try
                    lst.Items.FindByValue(s).Selected = True
                Catch
                End Try
            Catch
                msgCust.Text = "Ошибка формирования списка банков!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub BindAdvert(ByRef lst As DropDownList, ByVal s As String)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select Advertise_id,Adv_Name from Advertising order by Advertise_id")
                ds = New DataSet
                adapt.Fill(ds)
                lst.Items.Add(" ")
                lst.DataSource = ds.Tables(0).DefaultView
                lst.DataTextField = "Adv_Name"
                lst.DataValueField = "Advertise_id"
                lst.DataBind()
                Try
                    lst.Items.FindByValue(s).Selected = True
                Catch
                End Try
            Catch
                msgCust.Text = "Ошибка формирования списка рекламы!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Sub Bind(Optional ByVal sRequest = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim s$
            Dim customer
            Dim action
            Try
                customer = Request.Params("customer")
                If customer <> "" Then
                    'If action = 1 Then
                    'Dim sEmployee$ = dbSQL.ExecuteScalar("select good_sys_id from good where num_cashregister='" & numcashregister & "'")
                    'If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                    'Else
                    'Response.Redirect(GetAbsoluteUrl("~/Repair.aspx?" + sEmployee))
                    'End If
                    'End If

                    s$ = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and (boos_last_name like '%" & customer & "%' or customer_name like '%" & customer & "%' or accountant like '%" & customer & "%' or unn like '%" & customer & "%' or city like '%" & customer & "%' or address like '%" & customer & "%' or bank_code like '%" & customer & "%' or dogovor like '%" & customer & "%')"
                    Session("Filter") = s
                    txtFilter.Text = customer

                End If

                If sRequest = "" Then
                    If Session("Filter") <> "" Then
                        s = Session("Filter")
                    Else
                        's = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and (((select count(*) from sale where customer_sys_id=c.customer_sys_iё d and cast(sale_date as bigint)>=cast(cast('" & Now().AddDays(-7).ToShortDateString() & "' as smalldatetime) as bigint)) > 0) or (select count(*) from sale where customer_sys_id=c.customer_sys_id)=0)"
                        Dim d1 = Now().AddDays(-7).ToShortDateString()
                        Dim d2 = Now()
                        Dim parce = Split(d1, ".")
                        d1 = parce(1) & "/" & parce(0) & "/" & parce(2)
                        parce = Split(d2, ".")
                        d2 = parce(1) & "/" & parce(0) & "/" & parce(2)
                        's = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and ((select count(*) from sale where customer_sys_id=c.customer_sys_id and datediff(d,sale_date,cast('" & d1 & "' as smalldatetime))<=0 and datediff(d,sale_date,cast('" & d2 & "' as smalldatetime))>=0) > 0)"
                        's = "SELECT TOP 30 * FROM customer WHERE (cto is null or cto <> 1) ORDER BY customer_sys_id DESC"

                        customer = "куч"
                        s = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and (boos_last_name like '%" & customer & "%' or customer_name like '%" & customer & "%' or accountant like '%" & customer & "%' or unn like '%" & customer & "%' or city like '%" & customer & "%' or address like '%" & customer & "%' or bank_code like '%" & customer & "%' or dogovor like '%" & customer & "%')"

                    End If
                Else
                    s = sRequest
                End If

                adapt = dbSQL.GetDataAdapter(s)
                ds = New DataSet
                adapt.Fill(ds)
                If ViewState("customersort") = "" Then
                    ds.Tables(0).DefaultView.Sort = "customer_sys_id DESC"
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("customersort")
                End If
                grdCustomers.DataSource = ds.Tables(0).DefaultView
                grdCustomers.DataKeyField = "customer_sys_id"
                grdCustomers.DataBind()

            Catch
                msgCust.Text = "Ошибка загрузки информации о покупателях!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdCustomers_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.CancelCommand
            grdCustomers.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdCustomers.ItemDataBound
            Dim s As String
            Dim s1 As String, s2 As String

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

                ' Информация о фирме

                s = ""
                If Not IsDBNull(e.Item.DataItem("customer_name")) AndAlso Trim(e.Item.DataItem("customer_name")).Length > 0 Then
                    If Not IsDBNull(e.Item.DataItem("customer_abr")) AndAlso Trim(e.Item.DataItem("customer_abr")).Length > 0 Then
                        s = e.Item.DataItem("customer_abr") & " "
                    End If
                    s = s & e.Item.DataItem("customer_name")
                End If
                If Not IsDBNull(e.Item.DataItem("registration")) AndAlso Trim(e.Item.DataItem("registration")).Length > 0 Then
                    If s.Length > 0 Then s = s & "<br>"
                    s = s & e.Item.DataItem("registration")
                End If

                If Not IsDBNull(e.Item.DataItem("branch")) AndAlso Trim(e.Item.DataItem("branch")).Length > 0 Then
                    If s.Length > 0 Then s = s & "<br>"
                    s = s & e.Item.DataItem("branch")
                End If
                'If Not IsDBNull(e.Item.DataItem("tax_inspection")) AndAlso Trim(e.Item.DataItem("tax_inspection")).Length > 0 Then
                '    If s.Length > 0 Then s = s & "<br>"
                '    s = s & e.Item.DataItem("tax_inspection")
                'End If
                s &= "<br>" & GetIMNSName(IIf(IsDBNull(e.Item.DataItem("imns_sys_id")), 0, e.Item.DataItem("imns_sys_id")))

                CType(e.Item.FindControl("lblCustomerName"), LinkButton).Text = s

                ' Информация о руководителях

                s = ""
                If Not IsDBNull(e.Item.DataItem("boos_last_name")) AndAlso Trim(e.Item.DataItem("boos_last_name")).Length > 0 Then
                    s = e.Item.DataItem("boos_last_name")
                End If
                If Not IsDBNull(e.Item.DataItem("boos_first_name")) AndAlso Trim(e.Item.DataItem("boos_first_name")).Length > 0 Then
                    If s.Length > 0 Then s = s & " "
                    s = s & e.Item.DataItem("boos_first_name")
                End If
                If Not IsDBNull(e.Item.DataItem("boos_patronymic_name")) AndAlso Trim(e.Item.DataItem("boos_patronymic_name")).Length > 0 Then
                    If s.Length > 0 Then s = s & " "
                    s = s & e.Item.DataItem("boos_patronymic_name")
                End If
                If Not IsDBNull(e.Item.DataItem("accountant")) AndAlso Trim(e.Item.DataItem("accountant")).Length > 0 Then
                    If s.Length > 0 Then s = s & "<br>"
                    s = s & e.Item.DataItem("accountant")
                End If

                CType(e.Item.FindControl("lblBoosAccountant"), WebControls.Label).Text = s

                ' Подтверждение удаления записи

                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить клиента?')){return confirm('Все продажи клиента будут удалены. Отменить удаление невозможно!!! Продолжить удаление?');}else {return false};")

                ' УНН, код ОКПО, плательщик НДС

                s = ""
                If Not IsDBNull(e.Item.DataItem("unn")) AndAlso Trim(e.Item.DataItem("unn")).Length > 0 Then
                    s = "УНП: " & e.Item.DataItem("unn") & " "
                End If
                If Not IsDBNull(e.Item.DataItem("OKPO")) AndAlso Trim(e.Item.DataItem("OKPO")).Length > 0 Then
                    s = s & "ОКЮЛП: " & e.Item.DataItem("OKPO") & " "
                End If
                If Not IsDBNull(e.Item.DataItem("NDS")) AndAlso e.Item.DataItem("NDS") = 1 Then
                    s = s & "НДС "
                End If
                CType(e.Item.FindControl("lblCodes"), WebControls.Label).Text = s

                ' Индекс, город, адрес

                s = ""
                If Not IsDBNull(e.Item.DataItem("zipcode")) AndAlso Trim(e.Item.DataItem("zipcode")).Length > 0 Then
                    s = e.Item.DataItem("zipcode")
                End If
                If Not IsDBNull(e.Item.DataItem("region")) AndAlso Trim(e.Item.DataItem("region")).Length > 0 Then
                    If s.Length > 0 Then s = s & ", "
                    s = s & e.Item.DataItem("region")
                End If
                If Not IsDBNull(e.Item.DataItem("city")) AndAlso Trim(e.Item.DataItem("city")).Length > 0 Then
                    If s.Length > 0 Then s = s & ", "
                    If Not IsDBNull(e.Item.DataItem("city_abr")) AndAlso Trim(e.Item.DataItem("city_abr")).Length > 0 Then
                        s = s & e.Item.DataItem("city_abr") & " "
                    End If
                    s = s & e.Item.DataItem("city")
                End If
                If Not IsDBNull(e.Item.DataItem("address")) AndAlso Trim(e.Item.DataItem("address")).Length > 0 Then
                    If s.Length > 0 Then s = s & ", "
                    If Not IsDBNull(e.Item.DataItem("street_abr")) AndAlso Trim(e.Item.DataItem("street_abr")).Length > 0 Then
                        s = s & e.Item.DataItem("street_abr") & " "
                    End If
                    Dim email_str As String = ""
                    If e.Item.DataItem("email").ToString <> "" Then
                        email_str = "<br>E-mail: " & e.Item.DataItem("email")
                    End If
                    s = s & e.Item.DataItem("address") & email_str
                End If

                CType(e.Item.FindControl("lblAddress"), WebControls.Label).Text = s

                'Факс, телефоны

                s = ""
                If Not IsDBNull(e.Item.DataItem("phone2")) AndAlso Trim(e.Item.DataItem("phone2")).Length > 0 Then
                    s = "Тел.: " & e.Item.DataItem("phone2")
                End If
                If Not IsDBNull(e.Item.DataItem("phone3")) AndAlso Trim(e.Item.DataItem("phone3")).Length > 0 Then
                    If s.Length > 0 Then s = s & ", " Else s = "Тел.: "
                    s = s & e.Item.DataItem("phone3")
                End If
                If Not IsDBNull(e.Item.DataItem("phone4")) AndAlso Trim(e.Item.DataItem("phone4")).Length > 0 Then
                    If s.Length > 0 Then s = s & ", " Else s = "Тел.: "
                    s = s & e.Item.DataItem("phone4")
                End If
                If Not IsDBNull(e.Item.DataItem("phone1")) AndAlso Trim(e.Item.DataItem("phone1")).Length > 0 Then
                    If s.Length > 0 Then s = s & "<br>"
                    s = s & "Факс: " & e.Item.DataItem("phone1")
                End If

                CType(e.Item.FindControl("lblPhone"), WebControls.Label).Text = s

                '  Код банка, адрес банка и расчетный счет

                s = ""
                s1 = ""
                s2 = ""
                If Not IsDBNull(e.Item.DataItem("bank_sys_id")) AndAlso Trim(e.Item.DataItem("bank_sys_id")).Length > 0 Then
                    GetBankNameAddress(e.Item.DataItem("bank_sys_id"), s1, s2, msgCust)
                    s = "Код банка: " & Trim(e.Item.DataItem("bank_code")) & ". " & s1
                End If
                If Not IsDBNull(e.Item.DataItem("bank_address")) AndAlso Trim(e.Item.DataItem("bank_address")).Length > 0 Then
                    If s1.Length > 0 Then s = s & ", "
                    s = s & e.Item.DataItem("bank_address")
                Else
                    If s2.Length > 0 Then s = s & ", " & s2
                End If
                If Not IsDBNull(e.Item.DataItem("bank_account")) AndAlso Trim(e.Item.DataItem("bank_account")).Length > 0 Then
                    If s.Length > 0 Then s = s & "<br>"
                    s = s & "Расчетный счет: " & e.Item.DataItem("bank_account")
                End If
                CType(e.Item.FindControl("lblBank"), WebControls.Label).Text = s

                If Not IsDBNull(e.Item.DataItem("alert")) AndAlso e.Item.DataItem("alert") = 1 Then
                    e.Item.FindControl("imgAlert").Visible = True
                    If Not IsDBNull(e.Item.DataItem("info")) AndAlso Trim(e.Item.DataItem("info")).Length > 0 Then
                        CType(e.Item.FindControl("imgAlert"), ImageButton).ToolTip = e.Item.DataItem("info")
                    End If
                Else
                    e.Item.FindControl("imgAlert").Visible = False
                End If

                e.Item.FindControl("imgSupport").Visible = Not (Not IsDBNull(e.Item.DataItem("support")) AndAlso e.Item.DataItem("support") = 0)

            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim bank_id$ = ""
                If Not IsDBNull(e.Item.DataItem("bank_sys_id")) Then
                    bank_id = Trim(e.Item.DataItem("bank_sys_id"))
                End If
                BindBankList(CType(e.Item.FindControl("lstBankCode"), DropDownList), bank_id)
                'End If

                Dim advert_id$ = ""
                If Not IsDBNull(e.Item.DataItem("Advertise_id")) Then
                    advert_id$ = Trim(e.Item.DataItem("Advertise_id"))
                End If
                BindAdvert(CType(e.Item.FindControl("lstAdvert"), DropDownList), advert_id)

                CType(e.Item.FindControl("chkNDS"), WebControls.CheckBox).Checked = Not IsDBNull(e.Item.DataItem("NDS")) AndAlso e.Item.DataItem("NDS")
                CType(e.Item.FindControl("chkCTO"), WebControls.CheckBox).Checked = Not IsDBNull(e.Item.DataItem("cto")) AndAlso e.Item.DataItem("cto")
                CType(e.Item.FindControl("chkSupport"), WebControls.CheckBox).Checked = Not (Not IsDBNull(e.Item.DataItem("support")) AndAlso e.Item.DataItem("support") = 0)
                CType(e.Item.FindControl("chkAlert"), WebControls.CheckBox).Checked = Not (IsDBNull(e.Item.DataItem("alert")) OrElse e.Item.DataItem("alert") = 0)


                If Not IsDBNull(e.Item.DataItem("region")) AndAlso CStr(e.Item.DataItem("region")).Length > 0 Then
                    Dim lstRegion As DropDownList = CType(e.Item.FindControl("lstRegion"), DropDownList)
                    Dim sRegion$ = e.Item.DataItem("region")
                    Dim i%
                    For i = 1 To lstRegion.Items.Count - 1
                        If sRegion.IndexOf(lstRegion.Items(i).Value) > -1 Then
                            lstRegion.SelectedIndex = i
                            sRegion = sRegion.Substring(lstRegion.SelectedItem.Value.Length)
                            CType(e.Item.FindControl("txtRegion"), WebControls.TextBox).Text = IIf(sRegion.Length = 0 Or sRegion.Trim = ",", "", sRegion.Trim.TrimStart(",").Trim)
                            Exit For
                        End If
                    Next
                End If
                Dim item As ListItem
                If Not IsDBNull(e.Item.DataItem("city_abr")) AndAlso CStr(e.Item.DataItem("city_abr")).Length > 0 Then
                    item = CType(e.Item.FindControl("lstCityAbr"), DropDownList).Items.FindByText(CStr(e.Item.DataItem("city_abr")).Trim)
                    If Not item Is Nothing Then item.Selected = True
                End If
                If Not IsDBNull(e.Item.DataItem("street_abr")) AndAlso CStr(e.Item.DataItem("street_abr")).Length > 0 Then
                    item = CType(e.Item.FindControl("lstStreetAbr"), DropDownList).Items.FindByText(CStr(e.Item.DataItem("street_abr")).Trim)
                    If Not item Is Nothing Then item.Selected = True
                End If
                BindIMNS(e.Item.FindControl("lstIMNS"), IIf(IsDBNull(e.Item.DataItem("imns_sys_id")), 0, e.Item.DataItem("imns_sys_id")))
            End If
        End Sub

        Private Sub grdCustomers_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.EditCommand
            'проверка прав на редактирование
            If Session("rule6") = "1" Then
                grdCustomers.EditItemIndex = CInt(e.Item.ItemIndex)
                Bind()
            End If
        End Sub

        Private Sub grdCustomers_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.DeleteCommand
            'проверка прав на удаление
            If Session("rule7") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                Try
                    cmd = New SqlClient.SqlCommand("remove_client")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", grdCustomers.DataKeys(e.Item.ItemIndex))
                    dbSQL.Execute(cmd)
                Catch
                    If Err.Number = 1 Then
                        msgCust.Text = "Выбранную запись нельзя удалить!"
                    Else
                        msgCust.Text = "Ошибка удаления записи!<br>" & Err.Description
                    End If
                End Try
                grdCustomers.EditItemIndex = -1
                Bind()
            End If
        End Sub

        Private Sub grdCustomers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.ItemCommand
            Dim i%
            Dim s$, s1$
            If grdCustomers.EditItemIndex = -1 AndAlso e.CommandName = "ViewDetail" Then
                i = e.Item.ItemIndex
                Session("Customer") = grdCustomers.DataKeys.Item(i)
                s = CType(grdCustomers.Items(i).Cells(0).FindControl("lblCustomerName"), LinkButton).Text
                s1 = CType(grdCustomers.Items(i).FindControl("lblBoosAccountant"), WebControls.Label).Text
                If s.Trim.Length > 0 And s1.Trim.Length > 0 Then s = s & "<br>"
                Session("CustomerInfo") = s & s1
                Session("CurrentPage") = "CustomerSales"
                Response.Redirect(GetAbsoluteUrl("~/CustomerSales.aspx?" & grdCustomers.DataKeys.Item(i)))
            End If
        End Sub

        Private Sub grdCustomers_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdCustomers.SortCommand
            If ViewState("customersort") = e.SortExpression Then
                ViewState("customersort") = e.SortExpression & " DESC"
            Else
                ViewState("customersort") = e.SortExpression
            End If
            Bind()
        End Sub

        Private Sub grdCustomers_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.UpdateCommand
            'проверка прав на редактирование
            If Session("rule6") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                Try
                    cmd = New SqlClient.SqlCommand("update_customer")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", grdCustomers.DataKeys(e.Item.ItemIndex))
                    cmd.Parameters.AddWithValue("@pi_customer_abr", CType(e.Item.FindControl("txtCustomerAbr"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_customer_name", CType(e.Item.FindControl("txtCustomerName"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_last_name", CType(e.Item.FindControl("txtLastName"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_first_name", CType(e.Item.FindControl("txtFirstName"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_patronymic_name", CType(e.Item.FindControl("txtPatronymicName"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_accountant", CType(e.Item.FindControl("txtAccountant"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_unn", CType(e.Item.FindControl("txtUNN"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_okpo", CType(e.Item.FindControl("txtOKPO"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_zipcode", CType(e.Item.FindControl("txtZipCode"), WebControls.TextBox).Text.Replace("'", """"))
                    Dim sTmp$ = CType(e.Item.FindControl("txtRegion"), WebControls.TextBox).Text.Replace("'", """").Trim
                    If sTmp.IndexOf(" ") > -1 Then
                        sTmp = sTmp.Substring(0, sTmp.IndexOf(" "))
                    End If
                    cmd.Parameters.AddWithValue("@pi_region", CType(e.Item.FindControl("lstRegion"), DropDownList).SelectedItem.Value & IIf(CType(e.Item.FindControl("txtRegion"), WebControls.TextBox).Text.Replace("'", """").Trim.Length > 0, ", " & sTmp & " р-н", ""))
                    cmd.Parameters.AddWithValue("@pi_city_abr", CType(e.Item.FindControl("lstCityAbr"), DropDownList).SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_city", CType(e.Item.FindControl("txtCity"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_street_abr", CType(e.Item.FindControl("lstStreetAbr"), DropDownList).SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_address", CType(e.Item.FindControl("txtAddress"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone1", CType(e.Item.FindControl("txtPhone1"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone2", CType(e.Item.FindControl("txtPhone2"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone3", CType(e.Item.FindControl("txtPhone3"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone4", CType(e.Item.FindControl("txtPhone4"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_tax_inspection", CType(e.Item.FindControl("txtTaxInspection"), WebControls.TextBox).Text.Replace("'", """"))
                    If CType(e.Item.FindControl("lstIMNS"), DropDownList).SelectedIndex > 0 Then
                        cmd.Parameters.AddWithValue("@pi_imns_sys_id", CType(e.Item.FindControl("lstIMNS"), DropDownList).SelectedValue)
                    End If
                    cmd.Parameters.AddWithValue("@pi_NDS", CType(e.Item.FindControl("chkNDS"), WebControls.CheckBox).Checked)
                    cmd.Parameters.AddWithValue("@pi_CTO", CType(e.Item.FindControl("chkCTO"), WebControls.CheckBox).Checked)
                    cmd.Parameters.AddWithValue("@pi_Support", CType(e.Item.FindControl("chkSupport"), WebControls.CheckBox).Checked)

                    Dim bank_id As Object = CType(e.Item.FindControl("lstBankCode"), DropDownList).SelectedItem.Value
                    If (CType(e.Item.FindControl("lstBankCode"), DropDownList).SelectedItem.Value = "0") Then
                        bank_id = DBNull.Value
                    End If

                    Dim advert_id As Object = CType(e.Item.FindControl("lstAdvert"), DropDownList).SelectedItem.Value
                    If (CType(e.Item.FindControl("lstAdvert"), DropDownList).SelectedItem.Value = "0") Then
                        advert_id = DBNull.Value
                    End If

                    cmd.Parameters.AddWithValue("@pi_bank_sys_id", bank_id)
                    cmd.Parameters.AddWithValue("@pi_advertise_id", advert_id)
                    cmd.Parameters.AddWithValue("@pi_bank_code", CType(e.Item.FindControl("lstBankCode"), DropDownList).SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@pi_bank_account", CType(e.Item.FindControl("txtBankAccount"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_bank_address", CType(e.Item.FindControl("txtBankAddress"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_registration", CType(e.Item.FindControl("txtRegistration"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_branch", CType(e.Item.FindControl("txtBranch"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_dogovor", CType(e.Item.FindControl("txtDogovor"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_alert", CType(e.Item.FindControl("chkAlert"), WebControls.CheckBox).Checked)
                    cmd.Parameters.AddWithValue("@pi_info", CType(e.Item.FindControl("txtInfo"), WebControls.TextBox).Text.Replace("'", """"))

                    cmd.Parameters.AddWithValue("@pi_post_adress", CType(e.Item.FindControl("txt_post_adress"), WebControls.TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_email", CType(e.Item.FindControl("txt_email"), WebControls.TextBox).Text.Replace("'", """"))

                    dbSQL.Execute(cmd)
                Catch
                    msgCust.Text = "Ошибка обновления записи!<br>" & Err.Description
                End Try
                grdCustomers.EditItemIndex = -1
                Bind()
            End If
        End Sub

        Private Sub cal_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cal.SelectionChanged
            Dim s$ = "SELECT * FROM customer c WHERE (cto is null or cto <> 1)"
            Try
                'Dim d1 As Date = cal.SelectedDates.Item(0)
                'Dim d2 As Date = cal.SelectedDates.Item(cal.SelectedDates.Count - 1)

                Dim d1 As Date = cal.SelectedDates.Item(0)
                Dim d2 As Date = cal.SelectedDates.Item(cal.SelectedDates.Count - 1)

                Dim parce = Split(d1, ".")
                d1 = parce(1) & "/" & parce(0) & "/" & parce(2)
                parce = Split(d2, ".")
                d2 = parce(1) & "/" & parce(0) & "/" & parce(2)

                s = s & " and ((select count(*) from sale where customer_sys_id=c.customer_sys_id and datediff(d,sale_date,cast('" & d1.ToShortDateString() & "' as smalldatetime))<=0 and datediff(d,sale_date,cast('" & d2.ToShortDateString() & "' as smalldatetime))>=0) > 0)"
            Catch
            End Try

            Bind(s)
            Session("Filter") = s
        End Sub
        Private Sub txtFilter_KeyDown(sender As Object, e As System.Windows.Forms.KeyEventArgs)
            If e.KeyCode = Keys.Enter Then
                Dim t As Integer = 0
            End If

        End Sub

        Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
            Dim str$ = txtFilter.Text

            If Trim(str).Length = 0 Then Bind() : Exit Sub
            If str.IndexOf("'") > -1 Then Exit Sub
            str = str.Trim
            Dim s$ = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and (boos_last_name like '%" & str & "%' or customer_name like '%" & str & "%' or accountant like '%" & str & "%' or unn like '%" & str & "%' or city like '%" & str & "%' or address like '%" & str & "%' or bank_code like '%" & str & "%' or dogovor like '%" & str & "%')"
            Bind(s)
            Session("Filter") = s
        End Sub

        Private Sub lnkShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
            Dim s$ = "SELECT * FROM customer WHERE (cto is null or cto <> 1)"
            Bind(s)
            Session("Filter") = s
        End Sub

        Private Sub lnkShowLastWeek_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkShowLastWeek.Click
            Session("Filter") = ""
            Bind()
        End Sub

        Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
            If Request.Form("FindHidden") = "1" Then
                btnFind_Click(sender, e)
            End If
        End Sub

        Private Sub BindIMNS(ByRef lst As DropDownList, ByVal imns_sys_id As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try

                adapt = dbSQL.GetDataAdapter("prc_getIMNS", True)
                ds = New DataSet
                adapt.Fill(ds)
                lst.DataSource = ds.Tables(0).DefaultView
                lst.DataTextField = "imns_name"
                lst.DataValueField = "sys_id"
                lst.DataBind()
                lst.Items.Insert(0, New ListItem(ClearString, ClearString))
                Try
                    lst.Items.FindByValue(imns_sys_id).Selected = True
                Catch
                End Try
            Catch
                msgCust.Text = "Ошибка формирования списка налоговых инспекций!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Function GetIMNSName(ByVal imns_sys_id As Integer) As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            GetIMNSName = ""
            Try
                adapt = dbSQL.GetDataAdapter("prc_getIMNS", True)
                ds = New DataSet
                adapt.Fill(ds)
                Dim rows() As DataRow = ds.Tables(0).Select("sys_id= " & imns_sys_id)
                If rows.Length <> 0 Then
                    GetIMNSName = "по " & rows(0).Item("imns_name")
                End If
            Catch
            End Try
        End Function
    End Class

End Namespace
