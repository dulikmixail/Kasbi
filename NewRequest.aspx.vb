Namespace Kasbi

    Partial Class NewRequest
        Inherits PageBase

        Const ClearString = "-------"
        Dim customer_sys_id%
        Dim bank_id$
        Dim dogovor$
        Dim isChangeMonth, isExpired, isCto As Boolean
        Dim m_goodsDS As DataSet


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

        ReadOnly Property GoodsDS() As DataSet
            Get
                If (m_goodsDS Is Nothing) Then
                    InitDataSet()
                End If
                Return m_goodsDS
            End Get
        End Property

        Private Sub InitDataSet()
            m_goodsDS = CType(Cache.Get(Session.SessionID & "GoodsDS"), DataSet)
            If (m_goodsDS Is Nothing) Then
                m_goodsDS = New DataSet
                Dim table As DataTable = New DataTable("GoodsDS")
                Dim col(1) As DataColumn
                col(0) = table.Columns.Add("good_sys_id")
                table.PrimaryKey = col
                table.Columns.Add("good_type_id")
                table.Columns.Add("good_description")
                table.Columns.Add("is_cashregister")
                table.Columns.Add("price")
                table.Columns.Add("quantity")
                table.Columns.Add("cost")
                table.Columns.Add("pricelist_sys_id")
                table.Columns.Add("city_id")
                table.Columns.Add("set_place")
                table.Columns.Add("kassir1")
                table.Columns.Add("kassir2")
                m_goodsDS.Tables.Add(table)
                Cache.Insert(Session.SessionID & "GoodsDS", m_goodsDS)
            End If
        End Sub

        Private Sub ClearRequest()
            'If Not (m_goodsDS Is Nothing)Then
            Cache.Remove(Session.SessionID & "GoodsDS")
            'End If
        End Sub

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Const javascript = "javascript:"
            chkPayed.Attributes.Add("onclick", javascript & radiobuttons.ClientID & ".disabled=!" & chkPayed.ClientID & ".checked;")
            'lnkAccountant.Attributes.Add("onclick", "javascript:" & txtAccountant.ClientID & ".value=" & txtBoosLastName.ClientID & ".value + ' ' + " & txtBoosFirstName.ClientID & ".value + ' ' + " & txtBoosPatronymicName.ClientID & ".value;")
            'txtUNN.Attributes.Add("onchange", javascript & txtOKPO.ClientID & ".value=" & txtUNN.ClientID & ".value;")
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
            isChangeMonth = False
            If Not IsPostBack Then


                BindLists()
                LoadGoodGroups()
                Try
                    If Request.QueryString.Count > 0 Then
                        customer_sys_id = GetPageParam(0)
                    Else
                        'customer_sys_id = Session("AddSaleForCustomer")
                    End If
                Catch
                    customer_sys_id = 0
                End Try


                'Session("AddSaleForCustomer") = customer_sys_id

                DisableCustomerPanel(Not GetCustomer())
                TreeGroup.ExpandDepth = 1

            End If

            If Request.QueryString.ToString = "0" Then
                Response.Redirect(GetAbsoluteUrl("~/NewRequest.aspx"))
            End If

            lbl_artikul_error.Text = ""
            lbl_fast_add_error.Text = ""
            txt_fast_add.Focus()

            Dim vis_addtosale
            If customer_sys_id Then
                vis_addtosale = dbSQL.ExecuteScalar("select max(sale_sys_id) from sale where customer_sys_id=" & customer_sys_id)
                If vis_addtosale Is DBNull.Value Then
                    chkAddToSale.Visible = False
                End If
            End If
        End Sub

        Sub BindLists()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            'определяем следующий номер договора
            If txtDogovor.Text.Length = 0 Then
                Try
                    'dogovor = dbSQL.ExecuteScalar("get_next_dogovor")
                    'Try
                    '    txtDogovor.Text = CInt(dogovor) + 1
                    'Catch
                    'End Try
                    txtDogovor.Text = txtUNN.Text
                Catch
                    msgAddCustomer.Text = "Ошибка определения номера договора!<br>" & Err.Description
                    Exit Sub
                End Try
            End If

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
            If lstBank.Items.Count > 0 Then
                lstBank_SelectedIndexChanged(Me, Nothing)
            End If

            'список типов товаров
            Try
                adapt = dbSQL.GetDataAdapter("select distinct(gt.good_type_sys_id),gt.name,gt.is_cashregister from  good_type gt ,good g where g.good_type_sys_id = gt.good_type_sys_id and g.sale_sys_id is null and (g.param_num > 0 or is_cashregister=1 ) order by is_cashregister DESC,name")
                ds = New DataSet
                adapt.Fill(ds)
                lstType.DataSource = ds.Tables(0).DefaultView
                lstType.DataTextField = "name"
                lstType.DataValueField = "good_type_sys_id"
                lstType.DataBind()
                lstType.Items.Insert(0, New ListItem(ClearString, "0"))
                lstType.SelectedIndex = 0
            Catch
                msgGoods.Text = "Ошибка формирования списка типов товаров!<br>" & Err.Description
                Exit Sub
            End Try
            If lstType.Items.Count > 0 Then
                lstType_SelectedIndexChanged(Me, Nothing)
            End If

            'список прейскурантов

            Try
                cmd = New SqlClient.SqlCommand("get_pricelist_by_good")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_good_type_sys_id", lstType.SelectedItem.Value)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If chkCTO.Checked Then
                    ds.Tables(0).DefaultView.Sort = "seq DESC"
                End If
                lstPriceList.DataSource = ds.Tables(0).DefaultView
                lstPriceList.DataTextField = "pricelist_name"
                lstPriceList.DataValueField = "pricelist_sys_id"

                lstPriceList.DataBind()
            Catch
                msgGoods.Text = "Ошибка формирования списка прейскурантов!<br>" & Err.Description
                Exit Sub
            End Try

            'список ответственных

            Try
                adapt = dbSQL.GetDataAdapter("get_salers", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstSaler.DataSource = ds.Tables(0).DefaultView
                lstSaler.DataTextField = "Name"
                lstSaler.DataValueField = "sys_id"
                lstSaler.DataBind()
                Dim item As ListItem = lstSaler.Items.FindByValue(CurrentUser.sys_id)
                If Not item Is Nothing Then item.Selected = True

                'Try
                '    customer_sys_id = Session("AddSaleForCustomer")
                'Catch
                '    customer_sys_id = 0
                'End Try

                If customer_sys_id = 0 Then
                    lstManager.DataSource = ds.Tables(0).DefaultView
                    lstManager.DataTextField = "Name"
                    lstManager.DataValueField = "sys_id"
                    lstManager.DataBind()
                    item = lstManager.Items.FindByValue(CurrentUser.sys_id)
                    If Not item Is Nothing Then item.Selected = True
                Else
                    lstManager.Visible = False
                End If
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка ответственных!<br>" & Err.Description
                Exit Sub
            End Try

            'список фирм-продавцев
            Try
                adapt = dbSQL.GetDataAdapter("get_firms", True)
                ds = New DataSet
                adapt.Fill(ds)
                lstFirm.DataSource = ds.Tables(0).DefaultView
                lstFirm.DataTextField = "firm_name"
                lstFirm.DataValueField = "firm_sys_id"
                lstFirm.DataBind()
            Catch
                msgAddCustomer.Text = "Ошибка формирования списка фирм!<br>" & Err.Description
                Exit Sub
            End Try

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
                adapt = dbSQL.GetDataAdapter("prc_getIMNS", True)
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

        Sub LoadGoodGroups()
            Dim Root As TreeNode = New TreeNode("Номенклатура товаров")
            Root.PopulateOnDemand = True

            Root.SelectAction = TreeNodeSelectAction.SelectExpand
            TreeGroup.Nodes.Add(Root)

        End Sub

        Sub PopulateGroups(ByVal node As TreeNode)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            adapt = dbSQL.GetDataAdapter("select good_group_sys_id,name group_name from good_group where good_group_sys_id <>0 order by group_name")
            ds = New DataSet
            adapt.Fill(ds, "Group")
            If ds.Tables.Count > 0 Then
                Dim row As DataRow
                For Each row In ds.Tables(0).Rows
                    Dim NewNode As TreeNode = New _
                        TreeNode(row("group_name").ToString(), _
                        row("good_group_sys_id").ToString())
                    NewNode.PopulateOnDemand = True
                    NewNode.SelectAction = TreeNodeSelectAction.SelectExpand
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        End Sub

        Sub PopulateGoods(ByVal node As TreeNode)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As New DataSet
            Dim cmd As SqlClient.SqlCommand

            cmd = New SqlClient.SqlCommand()
            cmd.CommandText = "select distinct(gt.good_type_sys_id),gt.name,gt.artikul, gt.is_cashregister,good_group_sys_id from  good_type gt ,good g where g.good_type_sys_id = gt.good_type_sys_id and g.sale_sys_id is null and (g.param_num > 0 or is_cashregister=1 ) and gt.good_group_sys_id = @good_group_sys_id order by is_cashregister DESC,name"
            cmd.Parameters.Add("@good_group_sys_id", SqlDbType.Int).Value = node.Value
            adapt = dbSQL.GetDataAdapter(cmd)
            adapt.Fill(ds, "Goods")
            If ds.Tables.Count > 0 Then
                Dim row As DataRow
                For Each row In ds.Tables(0).Rows
                    Dim NewNode As TreeNode = New _
                        TreeNode(row("name").ToString(), row("good_type_sys_id").ToString())
                    If row("artikul").ToString() <> "" Then
                        NewNode.ToolTip = "Артикул: " & row("artikul").ToString()
                    Else
                        NewNode.ToolTip = "Нет артикула"
                    End If
                    NewNode.PopulateOnDemand = False
                    NewNode.SelectAction = TreeNodeSelectAction.Select
                    node.ChildNodes.Add(NewNode)
                Next
            End If
        End Sub

        Sub export_customer()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0
                cmd = New SqlClient.SqlCommand("get_xml_new_customer")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)

                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_customer.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<Customers>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</Customers>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_sale()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0
                cmd = New SqlClient.SqlCommand("get_xml_new_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_sales.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<Sales>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</Sales>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            Dim cmd As SqlClient.SqlCommand
            Dim param As SqlClient.SqlParameter
            Dim ds As DataSet
            Dim adapt As SqlClient.SqlDataAdapter

            Dim i As Int16
            Dim iCustomer, iSale As Integer
            Dim s$, sKassir1$, sKassir2$, sPlace$, sSubDogovor$, dogovor%
            Dim ch() As Char = {"\", "/", ".", "-"}
            Dim sTmp As String
            'определяем номер договора и поддоговора
            'Try
            '    s = txtDogovor.Text
            '    i = s.IndexOfAny(ch)
            '    If i > 0 Then
            '        sSubDogovor = s.Substring(i)
            '    Else
            '        sSubDogovor = ""
            '    End If
            '    If Not Session("CTO") Then
            '        If i > 0 Then
            '            dogovor = CInt(s.Substring(0, i))
            '        Else
            '            dogovor = CInt(s)
            '        End If
            '    End If
            'Catch
            '    msgAddCustomer.Text = "Укажите правильный номер договора/поддоговора"
            '    Exit Sub
            'End Try



            '
            'проверяем можем ли мы заказать выбранный товар
            '

            If GoodsDS.Tables(0).Rows.Count > 0 Then
                Try
                    With GoodsDS.Tables(0)
                        For i = 0 To .Rows.Count - 1
                            If CBool(.Rows(i).Item("is_cashregister")) Then
                                s = "select count(*)-1 from good where good_sys_id=" & .Rows(i).Item("good_sys_id") & " and state<2"
                            Else
                                s = "select param_num-" & .Rows(i).Item("quantity") & " from good where good_sys_id=" & .Rows(i).Item("good_sys_id") & " and state<2"
                            End If
                            If dbSQL.ExecuteScalar(s) < 0 Then
                                msgAddCustomer.Text = "Товар " & .Rows(i).Item("good_description") & " уже заказан."
                                Exit Sub
                            End If
                        Next
                    End With
                Catch
                    msgAddCustomer.Text = Err.Description
                End Try
            End If

            'Нужно ли добавлять пользователя

            Try
                customer_sys_id = Session("AddSaleForCustomer")
            Catch
                customer_sys_id = 0
            End Try

            sSubDogovor = ""

            isCto = CBool(dbSQL.ExecuteScalar("Select top 1 cto from customer where customer_sys_id = " & customer_sys_id))
            If (Not isCto) Then
                dogovor = CInt(txtUNN.Text)
            End If

            Dim advertise_id As Object = lstAdvertising.SelectedItem.Value
            If (lstAdvertising.SelectedItem.Value = "0") Then
                advertise_id = DBNull.Value
            End If
            Dim imns_id As Object = dlstIMNS.SelectedItem.Value
            If (dlstIMNS.SelectedItem.Value = "0") Then
                imns_id = DBNull.Value
            End If
            Dim bank_id As Object = DBNull.Value
            Dim bank_code As String = ""

            If lstBank.Items.Count > 0 Then
                If (lstBank.SelectedItem.Value <> "0") Then
                    bank_id = lstBank.SelectedItem.Value
                    bank_code = lstBank.SelectedItem.Text
                End If
            End If

            If customer_sys_id = 0 Then
                'добавляем нового клиента
                Try
                    'проверяем УНН на присутствие в базе
                    Dim dublicate = dbSQL.ExecuteScalar("SELECT unn FROM customer WHERE unn='" & txtUNN.Text & "'")
                    If dublicate <> "" Then
                        msgAddCustomer.Text = "Ошибка! В базе уже есть клиент с таким УНН!<br>"
                        Exit Sub
                    End If
                    If txtUNN.Text.Trim.Length > 9 Or txtUNN.Text.Trim.Length < 9 Then
                        msgAddCustomer.Text = "Ошибка! Вы ввели неверный УНН!<br>"
                        Exit Sub
                    End If

                    'проверяем какой нужен номер договора
                    If chkCTO.Checked Then
                        dogovor = CInt(dbSQL.ExecuteScalar("SELECT TOP 1 dogovor FROM customer WHERE cto = 1 ORDER BY customer_sys_id DESC")) + 1
                    End If

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
                    cmd.Parameters.AddWithValue("@pi_street_abr", lstStreetAbr.SelectedValue)
                    cmd.Parameters.AddWithValue("@pi_address", txtAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_phone1", txtPhone1.Text)
                    cmd.Parameters.AddWithValue("@pi_phone2", txtPhone2.Text)
                    cmd.Parameters.AddWithValue("@pi_phone3", txtPhone3.Text)
                    cmd.Parameters.AddWithValue("@pi_phone4", txtPhone4.Text)
                    cmd.Parameters.AddWithValue("@pi_tax_inspection", txtTaxInspection.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_imns_sys_id", imns_id)
                    cmd.Parameters.AddWithValue("@pi_advertise_id", advertise_id)
                    cmd.Parameters.AddWithValue("@pi_NDS", -1 * CInt(chkNDS.Checked))
                    cmd.Parameters.AddWithValue("@pi_CTO", -1 * CInt(chkCTO.Checked))
                    cmd.Parameters.AddWithValue("@pi_Support", -1 * CInt(chkSupport.Checked))

                    cmd.Parameters.AddWithValue("@pi_bank_sys_id", bank_id)

                    cmd.Parameters.AddWithValue("@pi_bank_code", bank_code)
                    cmd.Parameters.AddWithValue("@pi_bank_account", txtBankAccount.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_bank_address", txtBankAddress.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_registration", txtRegistration.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_branch", txtBranch.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_info", txtInfo.Text.Replace("'", """"))
                    cmd.Parameters.AddWithValue("@pi_dogovor", dogovor)
                    '
                    cmd.Parameters.AddWithValue("@pi_Manager", lstManager.SelectedItem.Value)

                    cmd.Parameters.AddWithValue("@pi_post_adress", txt_post_adress.Text)
                    cmd.Parameters.AddWithValue("@pi_email", txtemail.Text)

                    '
                    param = New SqlClient.SqlParameter
                    param.Direction = ParameterDirection.Output
                    param.ParameterName = "@po_customer_sys_id"
                    param.SqlDbType = SqlDbType.Int
                    cmd.Parameters.Add(param)
                    dbSQL.Execute(cmd)
                    'Делаем выборку по новым клиентам
                    export_customer()
                    iCustomer = cmd.Parameters("@po_customer_sys_id").Value
                Catch
                    msgAddCustomer.Text = "Ошибка добавления нового клиента!<br>" & Err.Description
                    Exit Sub
                End Try
            Else
                iCustomer = customer_sys_id
                Try
                    dbSQL.Execute("update customer set Advertise_id =" & IIf(IsDBNull(advertise_id), "null", advertise_id) & " where customer_sys_id=" & iCustomer)
                Catch
                    msgAddCustomer.Text = "Ошибка сохранения информации о рекламе!<br>" & Err.Description
                End Try
            End If

            If GoodsDS.Tables(0).Rows.Count > 0 Then
                'Нужно ли добавлять товары в последний из заказов
                If chkAddToSale.Checked Then
                    Try
                        iSale = dbSQL.ExecuteScalar("select max(sale_sys_id) from sale where customer_sys_id=" & iCustomer)
                    Catch
                        If Err.Number = 13 Then
                            iSale = 0
                        Else
                            msgAddCustomer.Text = "Невозможно определить номер последнего заказа!<br>" & Err.Description
                            Exit Sub
                        End If
                    End Try
                End If
                If lstFirm.Items.Count = 0 Then
                    msgAddCustomer.Text = "Список фирм продавца пуст.Введите информацию в меню Администрирование->Фирмы"
                    Exit Sub
                End If
                If Not iSale > 0 Then
                    'добавляем новую покупку
                    Try
                        cmd = New SqlClient.SqlCommand("new_sale")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                        cmd.Parameters.AddWithValue("@pi_saler_sys_id", lstSaler.SelectedValue)

                        'Добавляем информацию об дополнительных услугах
                        If chkDocPack.Checked = True And chkTO2.Checked = True Then
                            cmd.Parameters.AddWithValue("@pi_info", 1)
                        ElseIf chkDocPack.Checked = False And chkTO2.Checked = True Then
                            cmd.Parameters.AddWithValue("@pi_info", 2)
                        ElseIf chkDocPack.Checked = True And chkTO2.Checked = False Then
                            cmd.Parameters.AddWithValue("@pi_info", 3)
                        Else
                            cmd.Parameters.AddWithValue("@pi_info", 4)
                        End If

                        If chkCTO.Checked Or Session("CTO") Then
                            cmd.Parameters.AddWithValue("@pi_firm_sys_id", 1)
                        Else
                            cmd.Parameters.AddWithValue("@pi_firm_sys_id", lstFirm.SelectedItem.Value)
                        End If

                        Dim d As Date
                        Try
                            d = Session("SelectedDate")
                        Catch
                            d = Now()
                        End Try
                        cmd.Parameters.AddWithValue("@pi_sale_date", d)
                        If chkPayed.Checked Then
                            If optBeznal.Checked Then
                                i = 1
                            ElseIf optNal.Checked Then
                                i = 2
                            Else
                                i = 3
                            End If

                            If Session("CTO") Then
                                cmd.Parameters.AddWithValue("@pi_state", 1)
                            Else
                                cmd.Parameters.AddWithValue("@pi_state", 3)
                            End If
                        Else
                            i = 0
                            cmd.Parameters.AddWithValue("@pi_state", 1)
                        End If
                        cmd.Parameters.AddWithValue("@pi_type", i)
                        cmd.Parameters.AddWithValue("@pi_dogovor", sSubDogovor)
                        s = txtProxy.Text.Trim.Replace("'", """")
                        If s.Length = 0 Then s = txtBoosLastName.Text.Trim.Replace("'", """") & " " & txtBoosFirstName.Text.Trim.Replace("'", """") & " " & txtBoosPatronymicName.Text.Trim.Replace("'", """")
                        cmd.Parameters.AddWithValue("@pi_proxy", s)
                        param = New SqlClient.SqlParameter
                        param.Direction = ParameterDirection.Output
                        param.SqlDbType = SqlDbType.Int
                        param.ParameterName = "@po_sale_sys_id"

                        cmd.Parameters.Add(param)
                        dbSQL.Execute(cmd)
                        'Делаем выборку по новым покупкам
                        export_sale()
                        iSale = cmd.Parameters("@po_sale_sys_id").Value
                    Catch
                        msgAddCustomer.Text = "Ошибка добавления нового заказа!<br>" & Err.Description
                        Exit Sub
                    End Try
                End If

                '
                'Делаем номер договора = УНП клиента
                '
                isCto = CBool(dbSQL.ExecuteScalar("Select top 1 cto from customer where customer_sys_id = " & iCustomer))
                If (Not isCto) Then
                    Try
                        dbSQL.Execute("update customer set dogovor=unn where  customer_sys_id=" & iCustomer)
                    Catch
                        msgAddCustomer.Text = "Ошибка обновления номера договора!<br>" & Err.Description
                    End Try

                End If
                '
                'Заказываем товары
                '
                With GoodsDS.Tables(0)
                    Try
                        Dim dolg As Double = 0
                        Dim drow As DataRow

                        For i = 0 To .Rows.Count - 1
                            drow = .Rows(i)
                            dolg += CDbl(drow("cost"))
                            cmd = New SqlClient.SqlCommand("request_good")
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@pi_good_sys_id", CInt(drow("good_sys_id")))
                            cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                            cmd.Parameters.AddWithValue("@pi_quantity", CInt(drow("quantity")))
                            cmd.Parameters.AddWithValue("@pi_price", CDbl(drow("price")))
                            If chkPayed.Checked Then
                                cmd.Parameters.AddWithValue("@pi_state", 3)
                            Else
                                cmd.Parameters.AddWithValue("@pi_state", 2)
                            End If

                            Dim b_is_cashregister As Boolean = CBool(drow("is_cashregister"))

                            If b_is_cashregister Then
                                sPlace = CType(repSalesGoods.Items(i).FindControl("txtPlace"), TextBox).Text
                                sKassir1 = CType(repSalesGoods.Items(i).FindControl("txtKasir1"), TextBox).Text
                                sKassir2 = CType(repSalesGoods.Items(i).FindControl("txtKasir2"), TextBox).Text
                            Else
                                sKassir1 = ""
                                sKassir2 = ""
                                sPlace = ""
                            End If

                            cmd.Parameters.AddWithValue("@pi_kassir1", sKassir1)
                            cmd.Parameters.AddWithValue("@pi_kassir2", sKassir2)
                            cmd.Parameters.AddWithValue("@pi_set_place", sPlace)
                            cmd.Parameters.AddWithValue("@pi_is_cashregister", CBool(drow("is_cashregister")))
                            cmd.Parameters.AddWithValue("@pi_pricelist_sys_id", CInt(drow("pricelist_sys_id")))
                            dbSQL.Execute(cmd)

                            cmd.Dispose()
                        Next

                        If chkPayed.Checked = False Then
                            'Записываем сумму в долг
                            cmd = New SqlClient.SqlCommand("update_customer_dolg")
                            cmd.CommandType = CommandType.StoredProcedure
                            cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                            cmd.Parameters.AddWithValue("@pi_dolg", dolg * 1.2)

                            dbSQL.Execute(cmd)
                        End If
                        'Делаем выборку по новым покупкам
                        export_sale()
                    Catch
                        msgAddCustomer.Text = "Ошибка заказа товара!<br>" & Err.Description
                        Exit Sub
                    End Try
                End With
            End If

            Try
                cmd = New SqlClient.SqlCommand("get_client_cashregisters_by_sale")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sale_sys_id", iSale)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                For i = 0 To ds.Tables(0).Rows.Count - 1
                    Dim sup% = 0
                    sup = CInt(ds.Tables(0).Rows(i).Item("support"))
                    If (sup = 1) Then
                        cmd = New SqlClient.SqlCommand("insert_cashOwner")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_sys_id", ds.Tables(0).Rows(i).Item("good_sys_id"))
                        cmd.Parameters.AddWithValue("@pi_customer_sys_id", iCustomer)
                        cmd.Parameters.AddWithValue("@pi_support_date", ds.Tables(0).Rows(i).Item("sale_date"))
                        cmd.Parameters.AddWithValue("@pi_marka_cto_in", ds.Tables(0).Rows(i).Item("num_control_cto"))
                        cmd.Parameters.AddWithValue("@pi_marka_cto_out", ds.Tables(0).Rows(i).Item("num_control_cto"))
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_in", ds.Tables(0).Rows(i).Item("num_control_pzu"))
                        cmd.Parameters.AddWithValue("@pi_marka_pzu_out", ds.Tables(0).Rows(i).Item("num_control_pzu"))
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_in", ds.Tables(0).Rows(i).Item("num_control_mfp"))
                        cmd.Parameters.AddWithValue("@pi_marka_mfp_out", ds.Tables(0).Rows(i).Item("num_control_mfp"))
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_in", ds.Tables(0).Rows(i).Item("num_control_reestr"))
                        cmd.Parameters.AddWithValue("@pi_marka_reestr_out", ds.Tables(0).Rows(i).Item("num_control_reestr"))
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_in", ds.Tables(0).Rows(i).Item("num_control_cto2"))
                        cmd.Parameters.AddWithValue("@pi_marka_cto2_out", ds.Tables(0).Rows(i).Item("num_control_cto2"))
                        cmd.Parameters.AddWithValue("@pi_marka_cp_in", ds.Tables(0).Rows(i).Item("num_control_cp"))
                        cmd.Parameters.AddWithValue("@pi_marka_cp_out", ds.Tables(0).Rows(i).Item("num_control_cp"))
                        cmd.Parameters.AddWithValue("@pi_zreport_in", "1")
                        cmd.Parameters.AddWithValue("@pi_zreport_out", "1")
                        cmd.Parameters.AddWithValue("@pi_itog_in", "0")
                        cmd.Parameters.AddWithValue("@pi_itog_out", "0")

                        cmd.Parameters.AddWithValue("@pi_info", ds.Tables(0).Rows(i).Item("good_info"))
                        cmd.Parameters.AddWithValue("@pi_executor", ds.Tables(0).Rows(i).Item("saler_sys_id"))
                        cmd.Parameters.AddWithValue("@pi_close_date", Now)
                        cmd.Parameters.AddWithValue("@place", ds.Tables(0).Rows(i).Item("set_place"))
                        cmd.Parameters.AddWithValue("@pi_sale_id", iSale)
                        cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                        dbSQL.Execute(cmd)
                    End If
                Next
            Catch
                msgAddCustomer.Text = "Ошибка заказа товара!<br>" & Err.Description
                Exit Sub
            End Try

            ClearRequest()
            Session("Customer") = iCustomer
            Session("Sale") = iSale
            Session("CustomerInfo") = txtCustomerName.Text & "<br>" & txtRegistration.Text & "<br>" & txtUNN.Text
            Session("CurrentPage") = "CustomerSales"
            Response.Redirect(GetAbsoluteUrl("~/CustomerSales.aspx?" & iCustomer))
        End Sub

        Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnClear.Click
            ClearFields()
        End Sub

        Private Sub ClearFields()
            Dim i%
            If (False) Then
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
                chkPayed.Checked = False
                optBeznal.Checked = False
                optNal.Checked = False
                optSberkassa.Checked = False
                txtQuantity.Text = ""
            End If

            Cache.Remove(Session.SessionID & "selectedgoods")
            Cache.Remove(Session.SessionID & "GoodsDS")
            Cache.Remove("")

            For i = 0 To lstType.Items.Count - 1
                Cache.Remove(Session.SessionID & "goods" & lstType.Items(i).Value)
            Next

            BindLists()
        End Sub

        Private Sub GetBankNameAddress(ByVal bank_id As String, ByRef BankName As String, ByRef BankAddress As String, ByRef msgctrl As Label)
            Dim reader As SqlClient.SqlDataReader

            Try
                reader = dbSQL.GetReader("Select name, address from bank where bank_sys_id='" & bank_id & "'")
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

        Private Function GoodTypeIsChashregister(ByVal ds As DataSet) As Boolean
            Dim dr As DataRow
            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = ds.Tables(0).Rows(i)
                If dr.RowState = DataRowState.Unchanged Then
                    GoodTypeIsChashregister = Not CBool(dr("is_cashregister"))
                    Exit Function
                End If
            Next
        End Function

        Private Sub lstType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstType.SelectedIndexChanged
            goods_to_list()
        End Sub

        Sub goods_to_list()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            If lstType.SelectedIndex <> 0 Then
                Try
                    ds = Cache.Get(Session.SessionID & "goods" & lstType.SelectedItem.Value)
                    If ds Is Nothing Then
                        cmd = New SqlClient.SqlCommand("get_goods_by_type")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_good_type_sys_id", lstType.SelectedItem.Value)
                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet

                        adapt.Fill(ds)
                        Cache.Insert(Session.SessionID & "goods" & lstType.SelectedItem.Value, ds)
                    End If
                    If Not isExpired Then
                        lstDescription.DataSource = ds.Tables(0).DefaultView
                        lstDescription.DataTextField = "good_description"
                        lstDescription.DataValueField = "good_sys_id"
                        lstDescription.DataBind()
                        If ds.Tables(0).Rows.Count = 0 Then
                            lstDescription.Enabled = False
                            txtQuantity.Enabled = False
                            lblQuantity.Enabled = False
                            btnAddGood.Enabled = False
                        Else
                            lstDescription.Enabled = True
                            btnAddGood.Enabled = True
                            txtQuantity.Enabled = GoodTypeIsChashregister(ds)
                            lblQuantity.Enabled = txtQuantity.Enabled
                            If txtQuantity.Enabled Then
                                txtQuantity.Text = "1"
                            End If
                        End If
                        isExpired = True
                    End If

                    cmd = New SqlClient.SqlCommand("get_pricelist_by_good")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_type_sys_id", lstType.SelectedItem.Value)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)
                    lstPriceList.DataSource = ds.Tables(0).DefaultView
                    lstPriceList.DataTextField = "pricelist_name"
                    lstPriceList.DataValueField = "pricelist_sys_id"
                    lstPriceList.DataBind()
                Catch
                    msgGoods.Text = "Ошибка формирования списка товаров!<br>" & Err.Description
                    Exit Sub
                Finally
                    lstPriceList_SelectedIndexChanged(Nothing, Nothing)
                End Try

            Else
                lstPriceList.Items.Clear()
                lstDescription.Items.Clear()
                lstDescription.Enabled = False
                txtQuantity.Enabled = False
                lblQuantity.Enabled = False
                btnAddGood.Enabled = False
            End If
        End Sub

        Private Sub btnAddGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGood.Click
            add_good()

        End Sub

        Private Function GetGood(ByVal ds As DataSet, ByVal good_sys_id As Integer) As DataRow
            Dim i As Integer
            Dim dr As DataRow
            For i = 0 To ds.Tables(0).Rows.Count - 1
                dr = ds.Tables(0).Rows(i)
                If Not dr.RowState = DataRowState.Deleted Then
                    If dr("good_sys_id") = good_sys_id Then
                        GetGood = dr
                        Exit Function
                    End If
                End If
            Next
        End Function

        Private Overloads Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.PreRender
            radiobuttons.Enabled = chkPayed.Checked
            chkNDS.Checked = False
            Try
                Dim s$
                s = Session("SelectedDate")
                If s Is Nothing Then
                    Session("SelectedDate") = Now
                    Calendar.Text = DateTime.Parse(Now).ToString("dd.MM.yyyy")
                Else
                    Calendar.Text = DateTime.Parse(Session("SelectedDate")).ToString("dd.MM.yyyy")
                    'If Not isChangeMonth Then calendar.VisibleDate = calendar.SelectedDate
                End If
            Catch
            End Try
        End Sub

        Private Function GetCustomer() As Boolean
            If Request.QueryString.Count > 0 Then
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
                            man_name.Text = .Item("man_name")
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
                            'новый номер договора
                            'adapt = dbSQL.GetDataAdapter("select case when dogovor is null then '' else dogovor end dogovor from sale where customer_sys_id=" & customer_sys_id & " order by d DESC")
                            'ds = New DataSet
                            'adapt.Fill(ds)
                            'Dim ch() As Char = {"\", "/", ".", "-"}
                            'Dim s$, i%, sTmp$
                            'If ds.Tables(0).Rows.Count > 0 Then
                            '    s = ds.Tables(0).Rows(0).Item("dogovor")
                            '    If chkCTO.Checked Then
                            '        Try
                            '            '##-yy/##
                            '            sTmp = s.Substring(s.LastIndexOfAny(ch) + 1).Trim
                            '            s = .Item("dogovor") & "-" & Format(Now, "yy") & "/"
                            '            i = CInt(sTmp) + 1
                            '            If i < 10 Then
                            '                s = s & "0" & i
                            '            Else
                            '                s = s & i
                            '            End If
                            '        Catch
                            '            s = .Item("dogovor") & "-" & Format(Now, "yy") & "/01"
                            '        End Try
                            '    Else
                            '        s = s.Trim(ch)
                            '        Try
                            '            i = CInt(s) + 1
                            '            s = "/" & i
                            '        Catch
                            '            s = "/1"
                            '        End Try
                            '        s = .Item("dogovor") & s
                            '    End If
                            'Else
                            '    If chkCTO.Checked Then
                            '        s = .Item("dogovor") & "-" & Format(Now, "yy") & "/01"
                            '    Else
                            '        s = .Item("dogovor")
                            '    End If
                            'End If
                            'txtDogovor.Text = s
                            txtDogovor.Text = .Item("unn")
                        End With
                    Catch
                        msgAddCustomer.Text = "Ошибка получения информации о пользователе!<br>" & Err.Description
                        Exit Function
                    End Try
                End If
            Else
                GetCustomer = False
                Exit Function
            End If

        End Function

        Private Sub DisableCustomerPanel(ByVal b As Boolean)
            CustomerPanel.Enabled = b
            txtCustomerAbr.Enabled = b And Not rdbtnIP.Checked
            txtCustomerName.Enabled = b And Not rdbtnIP.Checked
            txtBoosLastName.Enabled = b
            txtBoosFirstName.Enabled = b
            txtBoosPatronymicName.Enabled = b
            txtAccountant.Enabled = b
            txtUNN.Enabled = b
            txtOKPO.Enabled = b
            txtZipCode.Enabled = b
            lstRegion.Enabled = b
            txtRegion.Enabled = b
            lstCityAbr.Enabled = b
            txtCity.Enabled = b
            lstStreetAbr.Enabled = b
            txtAddress.Enabled = b
            txtPhone1.Enabled = b
            txtPhone2.Enabled = b
            txtPhone3.Enabled = b
            txtPhone4.Enabled = b
            txtTaxInspection.Enabled = b
            dlstIMNS.Enabled = b
            ' lstAdvertising.Enabled = b
            chkNDS.Checked = b
            txtBankName.Enabled = b
            txtBankAddress.Enabled = b
            txtBankAccount.Enabled = b
            lstBank.Enabled = b
            txtRegistration.Enabled = b
            txtBranch.Enabled = b
            txtInfo.Enabled = b
            chkAddToSale.Visible = Not b
        End Sub

        Private Sub calendar_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles calendar.TextChanged
            Session("SelectedDate") = DateTime.Parse(calendar.Text)
        End Sub

        Private Sub calendar_VisibleMonthChanged(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs)
            isChangeMonth = True
        End Sub

        Private Sub lstPriceList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstPriceList.SelectedIndexChanged
            Dim price%

            Try
                If lstPriceList.SelectedItem Is Nothing Then Exit Sub
                price = dbSQL.ExecuteScalar("select price from pricelist where pricelist_sys_id='" & lstPriceList.SelectedItem.Value & "' and good_type_sys_id=" & lstType.SelectedItem.Value)
                txtPrice.Text = price.ToString()
            Catch
                msgGoods.Text = "Ошибка определения цены товара по прейскуранту!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub repSalesGoods_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repSalesGoods.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item) Or (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim drw As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim trSetPlace As HtmlTableRow = e.Item.FindControl("trSetPlace")
                trSetPlace.Visible = CBool(drw("is_cashregister"))
                If (trSetPlace.Visible) Then
                    BindCities(e.Item.FindControl("ddlCity"), drw("city_id"))
                End If
            ElseIf (e.Item.ItemType = ListItemType.Footer) Then
                Dim lblTotalCost As Label = e.Item.FindControl("lblTotalCost")
                Dim z As Integer, totalCost As Double
                For z = 0 To GoodsDS.Tables(0).Rows.Count - 1
                    totalCost = totalCost + CDbl(GoodsDS.Tables(0).Rows(z)("cost"))
                Next
                lblTotalCost.Text = totalCost.ToString()
            End If
        End Sub

        Private Sub repSalesGoods_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.RepeaterCommandEventArgs) Handles repSalesGoods.ItemCommand
            If e.CommandName = "Delete" Then
                Dim drow As DataRow = GoodsDS.Tables(0).Rows.Find(e.CommandArgument)
                Dim good_type_id As String = drow("good_type_id")
                Dim ds As DataSet = Cache.Get(Session.SessionID & "goods" & good_type_id)
                Dim i As Integer
                Dim View As DataView = New DataView(ds.Tables(0))
                View.RowStateFilter = DataViewRowState.Deleted Or DataViewRowState.ModifiedCurrent Or DataViewRowState.CurrentRows
                For i = 0 To View.Count - 1
                    If (View.Item(i)("good_sys_id") = drow("good_sys_id")) Then
                        View.Item(i).Row.RejectChanges()
                        drow.Delete()
                        GoodsDS.Tables(0).AcceptChanges()
                        Exit For
                    End If
                Next
                BindGoogs()

            ElseIf (e.CommandName = "Refresh") Then

                Dim z%
                Dim drow As DataRow
                For z = 0 To repSalesGoods.Items.Count - 1
                    Dim item As RepeaterItem = repSalesGoods.Items(z)
                    If (item.ItemType = ListItemType.Item) Or (item.ItemType = ListItemType.AlternatingItem) Then
                        drow = GoodsDS.Tables(0).Rows.Find(CType(item.FindControl("btnDelete"), ImageButton).CommandArgument)
                        drow("price") = CType(item.FindControl("tbxPrice"), TextBox).Text
                        drow("quantity") = CType(item.FindControl("tbxQuantity"), TextBox).Text
                        Dim good_type_id As String = drow("good_type_id")
                        Dim ds As DataSet = Cache.Get(Session.SessionID & "goods" & good_type_id)
                        Dim i As Integer
                        Dim View As DataView = New DataView(ds.Tables(0))
                        View.RowStateFilter = DataViewRowState.ModifiedCurrent Or DataViewRowState.CurrentRows

                        For i = 0 To View.Count - 1
                            If (View.Item(i)("good_sys_id") = drow("good_sys_id")) Then
                                If drow("quantity") > ds.Tables(0).Rows((ds.Tables(0).Rows.Count - lstDescription.Items.Count - z) + i).Item("quantity") Then
                                    drow("quantity") = ds.Tables(0).Rows((ds.Tables(0).Rows.Count - lstDescription.Items.Count - z) + i).Item("quantity")
                                    msgGoods.Text = "Максимальное количество товара - " & ds.Tables(0).Rows((ds.Tables(0).Rows.Count - lstDescription.Items.Count - z) + i).Item("quantity")
                                End If
                                Exit For
                            End If
                        Next

                        View.RowStateFilter = DataViewRowState.Deleted
                        For i = 0 To View.Count - 1
                            If (View.Item(i)("good_sys_id") = drow("good_sys_id")) Then
                                If drow("quantity") > View.Item(i)("quantity") Then
                                    drow("quantity") = View.Item(i)("quantity")
                                    msgGoods.Text = "Максимальное количество товара - " & View.Item(i)("quantity")
                                End If
                                Exit For
                            End If
                        Next

                        drow("cost") = drow("price") * drow("quantity")
                        If (CBool(drow("is_cashregister"))) Then
                            drow("city_id") = CType(item.FindControl("ddlCity"), DropDownList).SelectedValue
                            drow("set_place") = CType(item.FindControl("txtPlace"), TextBox).Text
                            drow("kassir1") = CType(item.FindControl("txtKasir1"), TextBox).Text
                            drow("kassir2") = CType(item.FindControl("txtKasir2"), TextBox).Text
                        End If
                    End If
                Next
                GoodsDS.Tables(0).AcceptChanges()
                BindGoogs()
            End If
            lstType_SelectedIndexChanged(Nothing, Nothing)
        End Sub

        Private Sub BindGoogs()
            If (GoodsDS.Tables(0).Rows.Count > 0) Then
                repSalesGoods.Visible = True
                repSalesGoods.DataSource = GoodsDS.Tables(0).DefaultView
                repSalesGoods.DataBind()
            Else
                repSalesGoods.Visible = False
            End If
        End Sub

        Private Sub BindCities(ByVal ddlCity As DropDownList, ByVal id As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim sql$ = "select 	a.*, a.name_city + COALESCE(', ' + c.name_obl, ', ' + c.name_obl, '') + COALESCE(', ' +  b.name_rn, ', ' + b.name_rn, '') as CityFullName from City a left outer join rn b on a.id_rn = b.id_rn left outer join obl c on a.id_obl = c.id_obl order by a.name_city"

            If ddlCity.Items.Count = 0 Then
                Try
                    adapt = dbSQL.GetDataAdapter(sql)
                    ds = New DataSet
                    adapt.Fill(ds)
                    ddlCity.DataSource = ds
                    ddlCity.DataValueField = "id_city"
                    ddlCity.DataTextField = "CityFullName"
                    ddlCity.DataBind()
                    If id > 0 Then
                        ddlCity.Items.FindByValue(id.ToString()).Selected = True
                    End If
                Catch
                End Try
            End If
        End Sub

        Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
            'Dim i%
            'ClearRequest()
            'Cache.Remove(Session.SessionID & "selectedgoods")
            'For i = 0 To lstType.Items.Count - 1
            '    Cache.Remove(Session.SessionID & "goods" & lstType.Items(i).Value)
            'Next
        End Sub

        Protected Sub TreeGroup_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TreeGroup.SelectedNodeChanged
            If TreeGroup.SelectedNode.Depth = 2 Then

                lstType.SelectedIndex = -1
                Dim item As ListItem = lstType.Items.FindByValue(TreeGroup.SelectedNode.Value)
                If Not item Is Nothing Then item.Selected = True
            Else
                lstType.SelectedIndex = -1
            End If
            lstType_SelectedIndexChanged(Me, Nothing)
            TreeGroup.SelectedNodeStyle.BackColor = Color.Red
            TreeGroup.SelectedNodeStyle.ForeColor = Color.White
            TreeGroup.SelectedNodeStyle.Font.Bold = True
        End Sub

        Protected Sub TreeGroup_TreeNodeExpanded(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeGroup.TreeNodeExpanded
            TreeGroup_TreeNodePopulate(sender, e)
        End Sub

        Protected Sub TreeGroup_TreeNodePopulate(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.TreeNodeEventArgs) Handles TreeGroup.TreeNodePopulate
            If e.Node.ChildNodes.Count = 0 Then
                Select Case e.Node.Depth
                    Case 0
                        PopulateGroups(e.Node)
                    Case 1
                        PopulateGoods(e.Node)
                End Select
            End If
        End Sub

        Protected Sub btnArtikul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArtikul.Click
            Dim reader As SqlClient.SqlDataReader

            If txtArtikul.Text <> "" Then
                reader = dbSQL.GetReader("select good_type_sys_id from delivery_detail where artikul='" & txtArtikul.Text & "'")


                If reader.Read() Then
                    Dim type_id = reader.Item(0)

                    If type_id Is DBNull.Value Or type_id.ToString = "" Then
                        Exit Sub
                    End If

                    reader.Close()
                    'MsgBox(type_id)

                    lstType.SelectedIndex = -1
                    Dim item As ListItem = lstType.Items.FindByValue(type_id)
                    If Not item Is Nothing Then item.Selected = True

                    Dim nn = 0
                    For nn = 0 To lstDescription.Items.Count - 1 Step 1
                        If lstDescription.Items(nn).Text.Contains(txtArtikul.Text) = True Then
                            lstDescription.Items(nn).Selected = True
                        End If
                    Next

                    'MsgBox(lstType.SelectedIndex.ToString)

                    lstType_SelectedIndexChanged(Me, Nothing)
                Else
                    lbl_artikul_error.Text = "<br><b>Вы ввели неправильный артикул!</b>"
                    Exit Sub
                End If
            End If
        End Sub

        Protected Sub btn_fast_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_fast_add.Click
            Dim reader As SqlClient.SqlDataReader
            Dim ds As DataSet

            If (txt_fast_add.Text.Length = 8 Or txt_fast_add.Text.Length = 13) Then

                reader = dbSQL.GetReader("select good_type_sys_id, good_sys_id from good where num_cashregister='" & txt_fast_add.Text & "'")

                If reader.Read() Then
                    Dim good_type_sys_id = reader.Item(0)
                    Dim good_sys_id = reader.Item(1)

                    'Если совпадает тип товара
                    reader.Close()

                    Dim res
                    Try
                        res = lstDescription.Items.FindByValue(good_sys_id).Text
                    Catch
                    End Try

                    If good_type_sys_id = lstType.SelectedItem.Value Then
                        If res <> "" Then
                            lstDescription.Items.FindByValue(good_sys_id).Selected = True
                            add_good()
                        Else
                            lbl_fast_add_error.Text = "<br><b>Данного кассового аппарата нет в списке ККМ!</b>"
                        End If
                    Else
                        lbl_fast_add_error.Text = "<br><b>Выберите тип товара и цену!</b>"
                    End If

                End If
            Else
                lbl_fast_add_error.Text = "<br><b>Введите номер кассового аппарата!</b>"
            End If
        End Sub

        Sub add_good()
            Dim b As Boolean, d, p As Double
            Dim i, k As Integer
            Dim ds As DataSet
            Try
                'Выбран ли товар
                If lstDescription.SelectedIndex = -1 Then
                    If lstDescription.Items.Count > 1 Then
                        msgGoods.Text = "Выберите товар!"
                        Exit Sub
                    Else
                        lstDescription.SelectedIndex = 0
                    End If
                End If

                'Загружаем списки товаров
                ds = Cache.Get(Session.SessionID & "goods" & lstType.SelectedItem.Value)
                If ds Is Nothing Then
                    isExpired = False
                    lstType_SelectedIndexChanged(Nothing, Nothing)
                End If
                ds = Cache.Get(Session.SessionID & "goods" & lstType.SelectedItem.Value)
                If ds.Tables(0).Rows.Count = 0 Then
                    Exit Sub
                End If

                'Проверка на достаточность товара

                Dim dr As DataRow

                For i = 0 To lstDescription.Items.Count - 1
                    If lstDescription.SelectedIndex = i Then
                        dr = ds.Tables(0).Rows((ds.Tables(0).Rows.Count - lstDescription.Items.Count) + i)
                        If dr.RowState = DataRowState.Unchanged Then
                            If dr("is_cashregister") = 0 Then
                                Try
                                    d = CDbl(txtQuantity.Text)
                                    If d > dr.Item("quantity") Then
                                        msgGoods.Text = "Превышено количество товара"
                                        Exit Sub
                                    End If
                                    Exit For

                                Catch
                                    msgGoods.Text = "Укажите количество товара!"
                                    Exit Sub
                                End Try
                            Else
                                d = 1
                                Exit For
                            End If
                        ElseIf dr.RowState = DataRowState.Modified Then
                            If dr("is_cashregister") = 0 Then
                                Try
                                    d = CDbl(txtQuantity.Text)
                                    If d > dr.Item("quantity") Then
                                        msgGoods.Text = "Превышено количество товара"
                                        Exit Sub
                                    End If
                                    Exit For
                                Catch
                                    msgGoods.Text = "Укажите количество товара!"
                                    Exit Sub
                                End Try
                            End If
                        End If
                    End If
                Next

                'Проверка цены товара

                Try
                    p = CDbl(txtPrice.Text)
                Catch
                    msgGoods.Text = "Укажите цену товара!"
                    Exit Sub
                End Try

                k = 0
                Dim b_Modified As Boolean
                For i = 0 To lstDescription.Items.Count - 1
                    If lstDescription.Items(i - k).Selected Then
                        Dim drow As DataRow = GoodsDS.Tables(0).Rows.Find(lstDescription.SelectedValue)
                        b_Modified = False
                        If drow Is Nothing Then
                            drow = GoodsDS.Tables(0).NewRow()
                        Else
                            b_Modified = True
                        End If

                        Dim ds_dr As DataRow = GetGood(ds, CInt(lstDescription.SelectedValue))
                        drow("good_sys_id") = ds_dr.Item("good_sys_id")
                        Dim b_is_cashregister As Boolean = CBool(ds_dr.Item("is_cashregister"))
                        If Not b_Modified Then
                            drow("good_type_id") = lstType.SelectedValue
                        End If
                        drow("is_cashregister") = b_is_cashregister
                        If (b_is_cashregister) Then
                            drow("good_description") = ds_dr.Item("good_description")
                        Else
                            drow("good_description") = lstType.SelectedItem.Text
                        End If

                        drow("price") = p
                        drow("quantity") = d
                        drow("cost") = p * d
                        drow("pricelist_sys_id") = lstPriceList.SelectedItem.Value
                        drow("city_id") = -1
                        drow("set_place") = ""
                        drow("kassir1") = ""
                        drow("kassir2") = ""
                        drow.EndEdit()

                        If b_Modified = False Then
                            GoodsDS.Tables(0).Rows.InsertAt(drow, 0)
                        End If
                        If (b_is_cashregister) Then
                            ds_dr.Delete()
                            lstDescription.Items.RemoveAt(i - k)
                            k = k + 1
                        Else
                            If (ds_dr.Item("quantity") = d) Then
                                ds_dr.Delete()
                                lstDescription.Items.RemoveAt(i - k)
                                k = k + 1
                                'Else
                                'ds_dr.Item("quantity") = ds_dr.Item("quantity") - d
                            End If
                        End If
                        b = True
                    End If
                Next
                GoodsDS.Tables(0).AcceptChanges()
                BindGoogs()
                If b Then
                    'save
                    Cache.Item(Session.SessionID & "goods" & lstType.SelectedItem.Value) = ds
                Else
                    msgGoods.Text = "Выберите товар!"
                End If
            Catch
                Dim ex As Exception = HttpContext.Current.Server.GetLastError()
                msgGoods.Text = "Невозможно добавить товар! " & ex.Message & "source " & ex.Source & ex.StackTrace.ToString()
            End Try
        End Sub

        Protected Sub txtQuantity_TextChanged(sender As Object, e As EventArgs) Handles txtQuantity.TextChanged

        End Sub
    End Class
End Namespace
