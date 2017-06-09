Namespace Kasbi

Partial Class NewSupportConduct2
        Inherits PageBase
        Dim iNumber%
        Dim CurrentCustomer%, iNewCust%
        Const ClearString$ = "-------"
        Dim dogovor$


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

        Protected Sub CurrentPage_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CurrentPage.Load
            Dim goodto_sys_id = Request.Params(0)

            If Not IsPostBack Then
                lstCustomers.Items.Clear()
                LoadCustomerList()
            End If

            LoadGoodInfo(goodto_sys_id)
            LoadExecutor()
            LoadPlaceRegion()
            LoadCashRegList()

            load_history()
        End Sub


        Sub load_history()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cmd As SqlClient.SqlCommand
            Dim sql = ""

            cmd = New SqlClient.SqlCommand("get_goodto_history")
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.AddWithValue("@good_sys_id", Request.Params(0))
            cmd.Parameters.AddWithValue("@state", "4")
            cmd.Parameters.AddWithValue("@sys_id", "")

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            iNumber = 1
            grdSupportConductHistory.DataSource = ds.Tables(0).DefaultView
            grdSupportConductHistory.DataKeyField = "sys_id"
            grdSupportConductHistory.DataBind()
        End Sub

        Sub LoadGoodInfo(ByVal goodto_sys_id)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cmd As SqlClient.SqlCommand

            'Достаем информацию по оборудованию
            cmd = New SqlClient.SqlCommand("get_goodto_info")
            cmd.CommandType = CommandType.StoredProcedure
            Dim d = New Date(Year(Now), Month(Now), 1)

            cmd.Parameters.AddWithValue("@pi_goodto_sys_id", Request.Params(0))

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            Session("good_sys_id") = ds.Tables(0).Rows(0)("goodto_sys_id").ToString.Trim

            lblGoodNum.Text = ds.Tables(0).Rows(0)("good_num").ToString.Trim
            txtGoodNumCashregister.Text = ds.Tables(0).Rows(0)("good_num").ToString.Trim
            lblSetPlace.Text = ds.Tables(0).Rows(0)("place_region").ToString.Trim & ", " & ds.Tables(0).Rows(0)("set_place").ToString.Trim
            lblEmployeeAdd.Text = ds.Tables(0).Rows(0)("employee_add").ToString.Trim
            lblDateAdd.Text = ds.Tables(0).Rows(0)("date_created").ToString.Trim
            lblGarant.Text = ds.Tables(0).Rows(0)("garantia").ToString.Trim
            lblOwner.Text = ds.Tables(0).Rows(0)("current_Owner").ToString.Trim
            lblTO.Text = ds.Tables(0).Rows(0)("stateTO").ToString.Trim
            lblCustomer.Text = ds.Tables(0).Rows(0)("current_Owner").ToString.Trim
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
                Exit Sub
            End Try
        End Sub

        Sub LoadCashRegList()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select good_type_sys_id,name name from good_type where allowCTO=1 and is_cashregister=0 order by name")
                ds = New DataSet
                adapt.Fill(ds)

                lstGoodType.DataSource = ds
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
            Catch
                Exit Sub
            End Try
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
            Catch
            End Try
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
                ' cmd.Parameters.Add("@pi_sys_id", 1)
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
            End Try
            GetInfo = s
        End Function

        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text

            If Trim(str).Length = 0 Then
                lstCustomers.Items.Clear()
                LoadCustomerList(" (customer_name like '%" & str & "%')")
            Else
                lstCustomers.Items.Clear()
                LoadCustomerList(" (customer_name like '%" & str & "%')")
                Exit Sub
            End If
            Dim s$ = " (customer_name like '%" & str & "%')"
            Session("CustFilter") = s
        End Sub

        Protected Sub rbTO_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbTO.SelectedIndexChanged
            If rbTO.SelectedIndex = 0 Then
                pnlSupport.Visible = True
                pnlDelSupport.Visible = False
                pnlSupportConduct.Visible = False
            ElseIf rbTO.SelectedIndex = 1 Then
                pnlSupport.Visible = False
                pnlDelSupport.Visible = True
                pnlSupportConduct.Visible = False
            Else
                pnlSupport.Visible = False
                pnlDelSupport.Visible = False
                pnlSupportConduct.Visible = True

                DropDownList1.SelectedIndex = Month(Now) - 1
                DropDownList2.SelectedIndex = Year(Now) - 2003
            End If
        End Sub

        Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnAdd.Click
            If rbTO.SelectedIndex = 0 Then
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet
                Dim cmd As SqlClient.SqlCommand
                Dim sql = ""

                Dim d = New Date(Year(Now), Month(Now), 1)
                Dim d2 = DateTime.Parse(tbxSupportDate.Text)
                Dim add_date = Format(d2, "MM/dd/yyyy")
                Dim d_begin = Format(Now, "MM/dd/yyyy")

                cmd = New SqlClient.SqlCommand("insert_supportConduct_prod")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_good_sys_id", Session("good_sys_id"))
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", lstCustomers.SelectedValue)
                cmd.Parameters.AddWithValue("@pi_start_date", add_date)
                cmd.Parameters.AddWithValue("@pi_period", "1")
                cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                cmd.Parameters.AddWithValue("@pi_close_date", Now)
                cmd.Parameters.AddWithValue("@place", txtPlace2.Text)
                cmd.Parameters.AddWithValue("@pi_place_rn_id", CType(lstPlaceRegion.SelectedValue, Integer))

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
            ElseIf rbTO.SelectedIndex = 2 Then
                Dim adapt As SqlClient.SqlDataAdapter
                Dim query As String
                query = "select * from goodto_history where goodto_sys_id in (select * from good_type order b good_type_sys_id)"
                query = query.Trim
                If query.Length < 10 Then
                    query = query & "where good_type_sys_id in (select good_sys_id from)"
                End If
                Dim ds As DataSet
                Dim cmd As SqlClient.SqlCommand

                Dim d = DateTime.Parse("01." & DropDownList1.SelectedValue & "." & DropDownList2.SelectedValue)
                d = Format(d, "MM/dd/yyyy")

                Dim closedate = DateTime.Parse(txtSupportDate.Text)
                closedate = Format(closedate, "MM/dd/yyyy")

                cmd = New SqlClient.SqlCommand("insert_prod_TO")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_good_sys_id", Session("good_sys_id"))
                cmd.Parameters.AddWithValue("@pi_start_date", d)
                cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                cmd.Parameters.AddWithValue("@pi_close_date", closedate)

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
            End If
        End Sub

        Protected Sub grdSupportConductHistory_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSupportConductHistory.DeleteCommand
            MsgBox("Невозможно удалить запись о ТО")
            Try
                dbSQL.Execute("delete from goodto_history where sys_id='" & grdSupportConductHistory.DataKeys(e.Item.ItemIndex) & "'")
            Catch
                MsgBox("error: Невозможно удалить запись! Снимите защиту от записи!", MsgBoxStyle.Critical, "Ошибка")
            End Try
        End Sub

        Private Sub grdSupportConductHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdSupportConductHistory.ItemDataBound
            Dim s As String
            Dim d As Date
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                '
                'Dates
                '
                If Not IsDBNull(e.Item.DataItem("updateDate")) Then
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID") & "<br>" & Format(e.Item.DataItem("updateDate"), "dd.MM.yyyy HH:mm")
                Else
                    s = "Изменил:<br> " & e.Item.DataItem("updateUserID")
                End If
                CType(e.Item.FindControl("lblUpdateRec"), Label).Text = s
                '
                'Определяем тип ТО
                '
                If Not IsDBNull(e.Item.DataItem("state")) Then
                    ' проведение ТО
                    If e.Item.DataItem("state") = 1 Then
                        s = ""
                        If Not IsDBNull(e.Item.DataItem("start_date")) Then
                            d = e.Item.DataItem("start_date")
                            s = GetRussianDate(d)
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblPeriod"), Label).Text = GetRussianDate(d)
                        s = "ТО проведено <br>"
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 6 Then
                        '
                        'приостановка ТО
                        '
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
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 8 Then
                        If Not IsDBNull(e.Item.DataItem("support_date")) Then
                            d = e.Item.DataItem("support_date")
                            s = MsgBox("ТО проведено", MsgBoxStyle.Critical, "Ошибка")
                        End If
                    ElseIf e.Item.DataItem("state") = 3 Then
                        'Снятие с ТО и в ИМНС
                        If Not IsDBNull(e.Item.DataItem("dismissal_date")) Then
                            d = e.Item.DataItem("dismissal_date")
                            s = "Снят с ТО и в ИМНС " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    ElseIf e.Item.DataItem("state") = 4 Then
                        'Постановка на ТО
                        If Not IsDBNull(e.Item.DataItem("support_date")) Then
                            d = e.Item.DataItem("support_date")
                            s = "Поставлен на ТО " & GetRussianDateFull(d) & "<br>"
                            CType(e.Item.FindControl("btnDeleteDoc"), LinkButton).Text = "Удалить<br>документы"
                        Else
                            s = ""
                        End If
                        CType(e.Item.FindControl("lblStatus"), Label).Text = s
                    End If
                End If
                'Информация о плательщике ТО
                s = ""
                If Not IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    s = GetInfo(CInt(e.Item.DataItem("owner_sys_id")), False)
                End If
                CType(e.Item.FindControl("lnkPayer"), HyperLink).Text = s
                If (e.Item.DataItem("state") = 4) Then
                    CType(e.Item.FindControl("lnkPayer"), HyperLink).NavigateUrl = GetAbsoluteUrl("~/CustomerSales.aspx?" & e.Item.DataItem("owner_sys_id"))
                End If
                'Информация о исполнителе
                s = ""
                If Not IsDBNull(e.Item.DataItem("executor")) Then
                    s = e.Item.DataItem("executor")
                End If
                CType(e.Item.FindControl("lblExecutorTO"), Label).Text = s
                'дополнительная информация
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
                            '.SelectedIndex = -1
                            .Items.FindByValue(e.Item.DataItem("sale_sys_id")).Selected = True
                        End With
                    Catch
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
                    Exit Sub
                End Try
            End If
        End Sub

        Public Function GetRussianDateFull(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDateFull = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Январь ", " Февраль ", " Март ", " Апрель ", " Май ", " Июнь ", " Июль ", " Август ", " Сентябрь ", " Октябрь ", " Ноябрь ", " Декабрь "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

    End Class
End Namespace
