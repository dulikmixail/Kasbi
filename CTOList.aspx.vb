Imports Service

Namespace Kasbi

    Partial Class CTOList
        Inherits PageBase

        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents msg As System.Web.UI.WebControls.Label
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
        Const ClearString = "-------"
        Private ReadOnly _serviceTelNumber As ServiceTelNumber = New ServiceTelNumber()


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Ограничение прав
            If Session("rule10") = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)
            If Session("rule5") = "0" Then lnkNewClient.Visible = False

            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub grdCTO_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCTO.CancelCommand
            grdCTO.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdCTO_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCTO.DeleteCommand
            'Ограничение прав на удаление
            If Session("rule1") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                Try
                    cmd = New SqlClient.SqlCommand("remove_client")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", grdCTO.DataKeys(e.Item.ItemIndex))
                    dbSQL.Execute(cmd)
                Catch
                    If Err.Number = 1 Then
                        msgCTO.Text = "Выбранную запись нельзя удалить!"
                    Else
                        msgCTO.Text = "Ошибка удаления записи!<br>" & Err.Description
                    End If
                End Try
                grdCTO.EditItemIndex = -1
                Bind()
            End If
        End Sub

        Private Sub grdCTO_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCTO.EditCommand
            'Ограничение прав на редактирование
            If Session("rule6") = "1" Then
                grdCTO.EditItemIndex = CInt(e.Item.ItemIndex)
                Bind()
            End If
        End Sub

        Private Sub grdCTO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCTO.ItemCommand
            Dim i%
            Dim s$, s1$
            If grdCTO.EditItemIndex = -1 AndAlso e.CommandName = "ViewDetail" Then
                i = e.Item.ItemIndex
                Session("Customer") = grdCTO.DataKeys.Item(i)
                s = CType(grdCTO.Items(i).Cells(0).FindControl("lblCustomerName2"), LinkButton).Text
                s1 = CType(grdCTO.Items(i).FindControl("lblBoosAccountant2"), Label).Text
                If s.Trim.Length > 0 And s1.Trim.Length > 0 Then s = s & "<br>"
                Session("CustomerInfo") = s & s1
                Session("CurrentPage") = "CustomerSales"
                Response.Redirect(GetAbsoluteUrl("~/CustomerSales.aspx?" & grdCTO.DataKeys.Item(i)))
            End If
        End Sub

        Private Sub grdCTO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdCTO.ItemDataBound
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

                CType(e.Item.FindControl("lblCustomerName2"), LinkButton).Text = s

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

                CType(e.Item.FindControl("lblBoosAccountant2"), Label).Text = s

                ' Подтверждение удаления записи
                CType(e.Item.FindControl("cmdDelete2"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите удалить клиента?')){return confirm('Все продажи клиента будут удалены. Отменить удаление невозможно!!! Продолжить удаление?');}else {return false};")

                ' УНН, код ОКПО, плательщик НДС
                s = ""
                If Not IsDBNull(e.Item.DataItem("unn")) AndAlso Trim(e.Item.DataItem("unn")).Length > 0 Then
                    s = "УНП: " & e.Item.DataItem("unn") & " "
                End If
                If Not IsDBNull(e.Item.DataItem("OKPO")) AndAlso Trim(e.Item.DataItem("OKPO")).Length > 0 Then
                    s = s & "ОКЮЛП: " & e.Item.DataItem("OKPO") & " "
                End If
                If Not IsDBNull(e.Item.DataItem("NDS")) AndAlso e.Item.DataItem("NDS") = 1 Then
                    CType(e.Item.FindControl("lblCodes2"), Label).Text = s & "Плательщик НДС"
                Else
                    CType(e.Item.FindControl("lblCodes2"), Label).Text = s
                End If

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
                    s = s & e.Item.DataItem("address")
                End If

                If Not IsDBNull(e.Item.DataItem("email")) AndAlso Trim(e.Item.DataItem("email").ToString()).Length > 0
                    s = s & "<br>Email: " & e.Item.DataItem("email").ToString()
                End If

                CType(e.Item.FindControl("lblAddress2"), Label).Text = s

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
                If Not IsDBNull(e.Item.DataItem("phone_notice")) AndAlso Trim(e.Item.DataItem("phone_notice").ToString()).Length > 0 Then
                    If s.Length > 0 Then s = s & ", " Else s = ""
                    s = s & "<br/>Для СМС: " & "+375" & e.Item.DataItem("phone_notice").ToString()
                End If

                CType(e.Item.FindControl("lblPhone2"), Label).Text = s

                '  Код банка, адрес банка и расчетный счет
                s = ""
                s1 = ""
                s2 = ""
                If Not IsDBNull(e.Item.DataItem("bank_sys_id")) AndAlso Trim(e.Item.DataItem("bank_sys_id")).Length > 0 Then
                    GetBankNameAddress(e.Item.DataItem("bank_sys_id"), s1, s2, msgCTO)
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

                CType(e.Item.FindControl("lblBank2"), Label).Text = s

            ElseIf e.Item.ItemType = ListItemType.EditItem Then
                Dim bank_id$ = ""
                If Not IsDBNull(e.Item.DataItem("bank_sys_id")) Then
                    bank_id = e.Item.DataItem("bank_sys_id")
                End If
                BindBankList(CType(e.Item.FindControl("lstBankCode2"), DropDownList), bank_id)
                CType(e.Item.FindControl("chkNDS2"), CheckBox).Checked = Not IsDBNull(e.Item.DataItem("NDS")) AndAlso e.Item.DataItem("NDS") = 1

                If Not IsDBNull(e.Item.DataItem("region")) AndAlso CStr(e.Item.DataItem("region")).Length > 0 Then
                    Dim lstRegion As DropDownList = CType(e.Item.FindControl("lstRegion"), DropDownList)
                    Dim sRegion$ = e.Item.DataItem("region")
                    Dim i%
                    For i = 1 To lstRegion.Items.Count - 1
                        If sRegion.IndexOf(lstRegion.Items(i).Value) > -1 Then
                            lstRegion.SelectedIndex = i
                            sRegion = sRegion.Substring(lstRegion.SelectedItem.Value.Length)
                            CType(e.Item.FindControl("txtRegion"), TextBox).Text = IIf(sRegion.Length = 0 Or sRegion.Trim = ",", "", sRegion.Trim.TrimStart(",").Trim)
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

        Private Sub grdCTO_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdCTO.SortCommand
            If ViewState("ctosort") = e.SortExpression Then
                ViewState("ctosort") = e.SortExpression & " DESC"
            Else
                ViewState("ctosort") = e.SortExpression
            End If
            Bind()
        End Sub

        Private Sub grdCTO_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCTO.UpdateCommand
            'Ограничение прав на редактирование
            If Session("rule6") = "1" Then
                Dim cmd As SqlClient.SqlCommand
                'проверяем номер для СМС на валидность
                Dim phoneNotice As String =  CType(e.Item.FindControl("txtPhoneNotice"), WebControls.TextBox).Text
                If Not _serviceTelNumber.IsValidNumber(phoneNotice, False)
                    msgCTO.Text = _serviceTelNumber.GetStringAllExeption() & "<br>"
                    Exit Sub
                End If
                Try
                    cmd = New SqlClient.SqlCommand("update_customer")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", grdCTO.DataKeys(e.Item.ItemIndex))
                    cmd.Parameters.AddWithValue("@pi_customer_name", CType(e.Item.FindControl("txtCustomerName2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_customer_abr", CType(e.Item.FindControl("txtCustomerAbr"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_last_name", CType(e.Item.FindControl("txtLastName2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_first_name", CType(e.Item.FindControl("txtFirstName2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_boos_patronymic_name", CType(e.Item.FindControl("txtPatronymicName2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_accountant", CType(e.Item.FindControl("txtAccountant2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_unn", CType(e.Item.FindControl("txtUNN2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_okpo", CType(e.Item.FindControl("txtOKPO2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_zipcode", CType(e.Item.FindControl("txtZipCode2"), TextBox).Text.Replace("'", """"))
                    Dim sTmp$ = CType(e.Item.FindControl("txtRegion"), TextBox).Text.Replace("'", """").Trim
                    If sTmp.IndexOf(" ") > -1 Then
                        sTmp = sTmp.Substring(0, sTmp.IndexOf(" "))
                    End If
                    cmd.Parameters.AddWithValue("@pi_region", CType(e.Item.FindControl("lstRegion"), DropDownList).SelectedItem.Value & IIf(CType(e.Item.FindControl("txtRegion"), TextBox).Text.Replace("'", """").Trim.Length > 0, ", " & sTmp & " р-н", ""))
                    cmd.Parameters.AddWithValue("@pi_city_abr", CType(e.Item.FindControl("lstCityAbr"), DropDownList).SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_city", CType(e.Item.FindControl("txtCity2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_street_abr", CType(e.Item.FindControl("lstStreetAbr"), DropDownList).SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_address", CType(e.Item.FindControl("txtAddress2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone1", CType(e.Item.FindControl("txtPhone12"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone2", CType(e.Item.FindControl("txtPhone22"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone3", CType(e.Item.FindControl("txtPhone32"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone4", CType(e.Item.FindControl("txtPhone42"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone_notice", CType(e.Item.FindControl("txtPhoneNotice"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_tax_inspection", CType(e.Item.FindControl("txtTaxInspection2"), TextBox).Text.Replace("'", """"))
                    If CType(e.Item.FindControl("lstIMNS"), DropDownList).SelectedIndex > 0 Then
                        cmd.Parameters.AddWithValue("@pi_imns_sys_id", CType(e.Item.FindControl("lstIMNS"), DropDownList).SelectedValue)
                    End If
                    'cmd.Parameters.Add("@pi_imns_sys_id", CType(e.Item.FindControl("lstIMNS"), DropDownList).SelectedValue)

                    cmd.Parameters.AddWithValue("@pi_NDS", CType(e.Item.FindControl("chkNDS2"), CheckBox).Checked)
                    cmd.Parameters.AddWithValue("@pi_CTO", CType(e.Item.FindControl("chkCTO2"), CheckBox).Checked)
                    cmd.Parameters.AddWithValue("@pi_Support", False)
                    cmd.Parameters.AddWithValue("@pi_bank_sys_id", CType(e.Item.FindControl("lstBankCode2"), DropDownList).SelectedItem.Value)
                    cmd.Parameters.AddWithValue("@pi_bank_code", CType(e.Item.FindControl("lstBankCode2"), DropDownList).SelectedItem.Text)
                    cmd.Parameters.AddWithValue("@pi_bank_account", CType(e.Item.FindControl("txtBankAccount2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_bank_address", CType(e.Item.FindControl("txtBankAddress2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_registration", CType(e.Item.FindControl("txtRegistration2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_branch", CType(e.Item.FindControl("txtBranch2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_dogovor", CType(e.Item.FindControl("txtDogovor2"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_alert", False)
                    cmd.Parameters.AddWithValue("@pi_info", "")

                    cmd.Parameters.AddWithValue("@pi_post_adress", CType(e.Item.FindControl("txt_post_adress"), TextBox).Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_email", CType(e.Item.FindControl("txt_email"), TextBox).Text.Replace("'", """"))


                    dbSQL.Execute(cmd)
                Catch
                    msgCTO.Text = "Ошибка обновления записи!<br>" & Err.Description

                End Try
                grdCTO.EditItemIndex = -1
                Bind()
            End If
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("SELECT * FROM customer WHERE cto=1")
                ds = New DataSet()
                adapt.Fill(ds)
                If ViewState("ctosort") = "" Then
                    ds.Tables(0).DefaultView.Sort = "dogovor"
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("ctosort")
                End If
                grdCTO.DataSource = ds.Tables(0).DefaultView
                grdCTO.DataKeyField = "customer_sys_id"
                grdCTO.DataBind()
            Catch
                msgCTO.Text = "Ошибка загрузки информации о ЦТО!<br>" & Err.Description
            End Try
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
                msgctrl.Text = "Ошибка получения информации о банке!<br>" & Err.Description
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
                ds = New DataSet()
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
                msgCTO.Text = "Ошибка формирования списка банков!<br>" & Err.Description
                Exit Sub
            End Try
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
                msgCTO.Text = "Ошибка формирования списка налоговых инспекций!<br>" & Err.Description
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
                    GetIMNSName = rows(0).Item("imns_name")
                End If
            Catch
            End Try
        End Function

    End Class

End Namespace
