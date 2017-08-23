
Imports System.Globalization
Imports System.Threading

Namespace Kasbi

    Partial Class MasterCTO
        Inherits PageBase
        Protected WithEvents lnkDismissalIMNS As System.Web.UI.WebControls.LinkButton

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
        Dim i
        Dim iType%, j%
        Dim show_state = 0
        Dim to_made = 0
        Dim to_made_tmp = 0
        Dim to_made_cnd = 0


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                LoadPlaceRegion()
                LoadEmployee()
                LoadGoodType(1)

                'Выставляем текущий месяц и год
                lstMonth.SelectedIndex = Month(Now) - 1
                If Year(Now) > 2002 And Year(Now) < 2018 Then
                    lstYear.SelectedIndex = Year(Now) - 2003
                Else
                    lstYear.SelectedIndex = 0
                End If



            End If

            If Session("User").permissions <> "4" Then
                adm_panel.Visible = False
                lbl_otv_master.Visible = False
                lstEmployee.Visible = False
                lnkConfirmEmployee.Enabled = False
                lnkRaspl.Enabled = False
                lnk_show_no_comfirmed.Enabled = False
                lnkNotTO.Enabled = False
                lnkDelTO.Enabled = False
                lstGoodType.Width = "300"
                lstPlaceRegion.Width = "300"
            Else
                adm_panel.Visible = True
                lbl_otv_master.Visible = True
                lstEmployee.Visible = True
                lnkConfirmEmployee.Enabled = True
                lnkRaspl.Enabled = True
                lnk_show_no_comfirmed.Enabled = True
                lnkNotTO.Enabled = True
                lnkDelTO.Enabled = True
            End If
        End Sub

        Private Sub grdTO_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdTO.SortCommand
            If ViewState("goodsort") = e.SortExpression Then
                ViewState("goodsort") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort") = e.SortExpression
            End If
            bind(Session("filter"))
        End Sub

        Sub bind(ByVal filter)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            grdTO.Visible = False
            grdTO_prod.Visible = False

            If chk_show_kkm.Checked = True Then
                grdTO.Visible = True
                cmd = New SqlClient.SqlCommand("get_cto_master2")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_filter", filter)

                cmd.CommandTimeout = 0

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ViewState("goodsort") = "" Then
                    ds.Tables(0).DefaultView.Sort = "good_sys_id DESC "
                    ViewState("goodsort") = "good_sys_id DESC "
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("goodsort") & ", good_sys_id ASC "
                End If

                grdTO.DataSource = ds.Tables(0).DefaultView
                grdTO.DataKeyField = "good_sys_id"
                grdTO.DataBind()
                Session("KKM_ds") = ds
            End If
            '
            'ТО по торговому оборудованию
            '
            If chk_show_torg.Checked = True Then
                grdTO_prod.Visible = True
                filter = Session("filter2")
                cmd = New SqlClient.SqlCommand("get_ctoprod_master")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_filter", filter)
                cmd.CommandTimeout = 0

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ViewState("goodsort2") = "" Then
                    ds.Tables(0).DefaultView.Sort = "goodto_sys_id DESC  "
                    ViewState("goodsort2") = "goodto_sys_id DESC "
                Else
                    ds.Tables(0).DefaultView.Sort = ViewState("goodsort2") & ", goodto_sys_id ASC "
                End If

                grdTO_prod.DataSource = ds.Tables(0).DefaultView
                grdTO_prod.DataKeyField = "goodto_sys_id"
                grdTO_prod.DataBind()
            End If
        End Sub

        Sub LoadPlaceRegion()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("SELECT * from Place_Rn order by name")
                ds = New DataSet
                adapt.Fill(ds)

                lstPlaceRegion.DataSource = ds
                lstPlaceRegion.DataTextField = "name"
                lstPlaceRegion.DataValueField = "place_rn_id"
                lstPlaceRegion.DataBind()
                lstPlaceRegion.Items.Insert(0, New ListItem(ClearString, ClearString))
            Catch
                msgCashregister.Text = "Ошибка формирования списка районов установки!<br>" & Err.Description
                Exit Sub
            End Try
            lstPlaceRegion.Enabled = True
        End Sub

        Sub LoadEmployee()
            Dim sql$ = "select * from employee order by name"
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstEmployee.DataSource = ds.Tables(0).DefaultView
                lstEmployee.DataTextField = "name"
                lstEmployee.DataValueField = "sys_id"
                lstEmployee.DataBind()
                lstEmployee.Items.Insert(0, New ListItem(ClearString, ClearString))
            Catch
            End Try
            lstEmployee.Enabled = True
        End Sub

        Sub LoadGoodType(ByVal type)
            Dim sql$ = ""

            If type = 1 Then
                sql = "select * from good_type where is_cashregister='1' order by name"
            ElseIf type = 2 Then
                sql = "select * from good_type where is_cashregister='0' and allowCTO='1' order by name"
            ElseIf type = 3 Then
                sql = "select * from good_type where allowCTO='1' order by name"
            End If

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstGoodType.DataSource = ds.Tables(0).DefaultView
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
                lstGoodType.Items.Insert(0, New ListItem(ClearString, ClearString))
            Catch
            End Try
            lstGoodType.Enabled = True
        End Sub

        Protected Sub lnkNeraspl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNeraspl.Click
            Dim filter
            Dim filter2
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = False

            filter = " where good.num_control_cto like 'МН%' AND (good.employee_cto is null OR good.employee_cto=0) AND (SELECT top 1 cash_history.state FROM cash_history WHERE cash_history.state in (2,3,4) AND cash_history.good_sys_id=good.good_sys_id ORDER BY cash_history.sys_id DESC) = '4'"
            filter2 = " where goodto.support like '%%' "
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Value <> ClearString Then
                    If item.Selected Then place_name &= item.Value & ","
                End If
            Next item

            If place_name <> "" Then
                filter &= " and good.place_rn_id in (" & place_name.TrimEnd(",") & ") "
                filter2 &= " and goodto.place_rn_id in (" & place_name.TrimEnd(",") & ") "
            End If

            Session("filter") = filter
            Session("filter2") = filter2

            show_state = 0
            to_made = 0
            bind(filter)
        End Sub

        Private Function GetList(ByVal list As ListBox) As String
            Dim d As String = ""
            Dim item As ListItem
            For i As Integer = 0 To list.Items.Count - 1
                item = list.Items(i)
                If (item.Selected) Then
                    d += item.Value
                    d += item.Text
                    If i < list.Items.Count - 1 Then d += ","
                End If
            Next
            'd = list.SelectedValue
            GetList = d
        End Function

        Private Sub grdTO_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdTO.ItemDataBound
            Dim s$ = ""
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If IsDBNull(e.Item.DataItem("state")) Then
                    e.Item.DataItem("state") = 0
                End If

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    If Month(e.Item.DataItem("lastTO")) = Month(Now()) And Year(e.Item.DataItem("lastTO")) = Year(Now()) Then
                        to_made_tmp = 1
                    Else
                        to_made_tmp = 2
                    End If

                    If Month(e.Item.DataItem("lastTO")) = lstMonth.Text And Year(e.Item.DataItem("lastTO")) = lstYear.Text Then
                        to_made_tmp = 1
                    Else
                        to_made_tmp = 2
                    End If
                End If

                If (e.Item.DataItem("state") = show_state Or show_state = 0) And ((to_made = to_made_tmp And Not IsDBNull(e.Item.DataItem("lastTO"))) Or to_made = 0) Then
                    '
                    'If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                    'case when customer_sys_id then enter_sub excelent
                    'End If
                    '
                    Dim payersysid
                    If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                        s = e.Item.DataItem("payerInfo")
                        payersysid = e.Item.DataItem("payer_sys_id")
                    End If
                    '
                    'Достаем договор
                    '
                    Dim dogovor
                    Dim reader As SqlClient.SqlDataReader
                    Dim query = "SELECT dogovor FROM customer WHERE customer_sys_id='" & payersysid & "'"

                    reader = dbSQL.GetReader(query)
                    If reader.Read() Then
                        Try
                            dogovor = reader.Item(0)
                        Catch
                        End Try
                    Else
                    End If
                    reader.Close()

                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s & "; Договор №" & dogovor

                    '
                    'Достаем информацию о рубриках для ккм
                    '
                    Dim count_rubr
                    query = "SELECT count(id_rubr) FROM cash_rubr WHERE good_sys_id='" & e.Item.DataItem("good_sys_id") & "'"
                    reader = dbSQL.GetReader(query)
                    If reader.Read() Then
                        Try
                            count_rubr = reader.Item(0)
                        Catch
                        End Try
                    Else
                    End If
                    reader.Close()
                    CType(e.Item.FindControl("lblNumGood"), HyperLink).NavigateUrl = "cash_rubr/default.aspx?cash=" & e.Item.DataItem("good_sys_id")


                    '
                    'CType(e.Item.FindControl("lblGoodOwner"), Label).Text = e.Item.DataItem("customer_abr") & " " & e.Item.DataItem("customer_name") & _
                    '"<br>УНП: " & e.Item.DataItem("unn") & _
                    '"<br>Дог №: " & e.Item.DataItem("dogovor")
                    'End If
                    'If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                    '    s = e.Item.DataItem("payerInfo")
                    '
                    '    CType(e.Item.FindControl("lblCustDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    '    If e.Item.ItemIndex = 0 Then
                    '        CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s
                    '        CType(e.Item.FindControl("lblGoodOwner"), Label).ToolTip = "Владелец: " & e.Item.DataItem("ownerInfo")
                    '        CType(e.Item.FindControl("lblDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    '    Else
                    '        If CType(grdTO.Items(e.Item.ItemIndex - 1).FindControl("lblCustDogovor"), Label).Text <> CStr(e.Item.DataItem("payerdogovor")) Then
                    '            CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s
                    '            CType(e.Item.FindControl("lblGoodOwner"), Label).ToolTip = "Владелец: " & e.Item.DataItem("ownerInfo")
                    '            CType(e.Item.FindControl("lblDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    '        End If
                    '    End If
                    'End If
                    '

                    i = i + 1
                    CType(e.Item.FindControl("lblNumGood"), HyperLink).Text = i
                    s = ""
                    If Not IsDBNull(e.Item.DataItem("dolg")) Then
                        s = s & e.Item.DataItem("dolg")
                    End If
                    CType(e.Item.FindControl("lblDolg"), Label).Text = s
                    '
                    'Картинки
                    '
                    If Not IsDBNull(e.Item.DataItem("alert")) Then
                        s = CStr(e.Item.DataItem("alert"))
                    End If
                    e.Item.FindControl("imgAlert").Visible = s.Length > 0
                    If s.Length > 0 Then CType(e.Item.FindControl("imgAlert"), HyperLink).ToolTip = s
                    e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso e.Item.DataItem("support") = "1"

                    Dim b As Boolean = e.Item.DataItem("repair")
                    e.Item.FindControl("imgRepair").Visible = b

                    If b Then
                        Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                        If i > 1 Then
                            CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip = "В ремонте. До этого в ремонте был " & i - 1 & " раз(а)"
                        Else
                            CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip = "В ремонте. До этого в ремонте не был"
                        End If
                    End If

                    e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                    If e.Item.FindControl("imgRepaired").Visible Then
                        CType(e.Item.FindControl("imgRepaired"), HyperLink).ToolTip = "Был в ремонте " & CInt(e.Item.DataItem("repaired")) & " раз(а)"
                    End If
                    '
                    'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                    '   s = e.Item.DataItem("stateTO")
                    'End If
                    '
                    CType(e.Item.FindControl("lnkStatus"), HyperLink).Text = "Просмотр"
                    CType(e.Item.FindControl("lnkStatus"), HyperLink).ToolTip = "Просмотр"

                    If IsDBNull(e.Item.DataItem("state")) Then
                        e.Item.DataItem("state") = 0
                    End If

                    If Not IsDBNull(e.Item.DataItem("lastTO")) And e.Item.DataItem("state") <> 4 Then
                        If e.Item.DataItem("state") = 6 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "ТО приостановлено<br>" & GetRussianDate(e.Item.DataItem("ldate")) & " (на " & e.Item.DataItem("period") & " месяцев)<br>"
                        End If
                        If e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "Снят с ТО<br>" & GetRussianDate(e.Item.DataItem("ldate")) & "<br>"
                        End If
                        If lnkSetEmployee.Visible = True Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "<b>" + GetRussianDate(e.Item.DataItem("lastTO")) + "</b><br><br>" + e.Item.DataItem("lastTOMaster")
                        Else
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "<b>" + GetRussianDate(e.Item.DataItem("lastTO")) + "</b>"
                        End If
                        e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                    ElseIf IsDBNull(e.Item.DataItem("lastTO")) And e.Item.DataItem("state") = 4 Then
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "Поставлен на ТО" & "<br>" & GetRussianDate(e.Item.DataItem("ldate")) & "<br>ТО не проводилось"
                    Else
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "ТО не проводилось"
                    End If

                    If (e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3) And show_state <> 2 Then
                        e.Item.Visible = False
                    End If

                    If e.Item.DataItem("state") = 6 Then
                        Dim start_month = Month(e.Item.DataItem("ldate"))
                        Dim end_month = start_month + e.Item.DataItem("period")

                        If (show_state <> 6) Then 'end_month >= Month(Now()) And Or end_month > 12
                            'e.Item.Visible = False
                        End If
                    End If
                    '
                    'Если проведено ТО
                    '
                    If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                        If Month(e.Item.DataItem("lastTO")) = Month(Now()) And Year(e.Item.DataItem("lastTO")) = Year(Now()) Then
                            e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                        End If
                    End If

                    'CType(e.Item.FindControl("lblLastTO"), Label).Text &= e.Item.DataItem("state")

                    If e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3 Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 100, 100)
                    ElseIf e.Item.DataItem("state") = 4 Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 250, 250)
                    ElseIf e.Item.DataItem("state") = 6 Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 250, 210)
                    End If

                    If count_rubr > 0 Then
                        e.Item.Cells(1).BackColor = Color.Green
                        e.Item.Cells(1).ForeColor = Color.White
                        CType(e.Item.FindControl("lblNumGood"), HyperLink).ForeColor = Color.White
                    End If

                    If IsDBNull(e.Item.DataItem("place_rn_id")) Then
                        CType(e.Item.FindControl("lblPlaceRegion"), Label).Visible = False
                    End If
                Else
                    e.Item.Visible = False
                End If
            Else
            End If
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Sub SelectAll(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j
            grdTO.Columns(0).Visible = True
            Dim s As Boolean = CType(grdTO.Controls.Item(0).Controls.Item(0).FindControl("cbxSelectAll"), CheckBox).Checked

            For j = 0 To grdTO.Items.Count - 1
                If grdTO.Items(j).Visible = True Then
                    CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = s
                End If
            Next
        End Sub

        Sub SelectAll_prod(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j
            grdTO_prod.Columns(0).Visible = True
            If grdTO_prod.Columns(1).Visible = False Then
                'MsgBox("Вы не можете добалять пустую запись в таблицу", MsgBoxStyle.Information, "Пожалуйста, введите достоверные данные о поставке")
            End If
            Dim s As Boolean = CType(grdTO_prod.Controls.Item(0).Controls.Item(0).FindControl("cbxSelectAll"), CheckBox).Checked

            For j = 0 To grdTO_prod.Items.Count - 1
                CType(grdTO_prod.Items(j).FindControl("cbxSelect"), CheckBox).Checked = s
            Next
        End Sub

        Protected Sub lnkSetEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSetEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    If n = 1 Then query &= "," & grdTO.DataKeys.Item(j)
                    If n = 0 Then query &= grdTO.DataKeys.Item(j)
                    n = 1
                End If
            Next

            Dim user = Session("User").sys_id
            If lstEmployee.Visible = True Then
                user = lstEmployee.SelectedItem.Value
            End If

            query = "UPDATE good SET employee_cto='" & user & "', confirmed=null where good_sys_id IN (" & query & ")"
            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            'повтряется поле good_sys_id
            bind(Session("Filter"))
        End Sub

        Protected Sub btnFindGood_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFindGood.Click
            show_state = 0
            to_made = 0
            findgood()
        End Sub

        Public Sub findgood()
            Dim filter
            Dim filter2 = ""
            Dim str
            lnkSetEmployee.Visible = False
            grdTO.Columns(10).Visible = False

            filter = " where good.num_control_cto like 'МН%' "
            filter2 = " where goodto.support like '%%' "

            If txtFindGoodNum.Text <> "" Then
                filter &= " and good.num_cashregister like '%" & txtFindGoodNum.Text & "%' "
                filter2 &= " and goodto.good_num like '%" & txtFindGoodNum.Text & "%' "
            End If

            If txtFindGoodCTO.Text <> "" Then filter &= " and good.num_control_cto like '%" & txtFindGoodCTO.Text & "%'  "

            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Value <> ClearString Then
                    If item.Selected Then place_name &= item.Value & ","
                End If
            Next item

            If place_name <> "" Then
                filter &= " and good.place_rn_id in (" & place_name.TrimEnd(",") & ") "
                filter2 &= " and goodto.place_rn_id in (" & place_name.TrimEnd(",") & ") "
            End If

            Dim good_type$ = ""
            For Each item As ListItem In lstGoodType.Items
                If item.Value <> ClearString Then
                    If item.Selected Then good_type &= item.Value & ","
                End If
            Next item

            If good_type <> "" Then
                filter &= " and good.good_type_sys_id in (" & good_type.TrimEnd(",") & ") "
                filter2 &= " and goodto.good_type_sys_id in (" & good_type.TrimEnd(",") & ") "
            End If

            If lstEmployee.SelectedIndex > 0 Then
                filter &= " and good.employee_cto='" & lstEmployee.SelectedItem.Value & "' "
                filter2 &= " and goodto.employee_cto='" & lstEmployee.SelectedItem.Value & "' "
            ElseIf lstEmployee.SelectedIndex <= 0 And Session("User").permissions <> "4" Then
                filter &= " and good.employee_cto='" & Session("User").sys_id & "' "
                filter2 &= " and goodto.employee_cto='" & Session("User").sys_id & "' "
            ElseIf lstEmployee.SelectedIndex <= 0 And Session("User").permissions = "4" Then
                grdTO.Columns(10).Visible = True
            End If
            'фильтр по проведению ТО за произвольный месяц
            'Для(сервера)
            Dim d_to = DateTime.Parse(lstMonth.SelectedValue & "." & "01" & "." & lstYear.SelectedValue)
            'Dim d_to = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)

            If to_made = 1 Then
                'filter &= " and good.good_sys_id IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id)"
                'для сервера
                filter &= " and good.good_sys_id IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='" & lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id)"
            End If
            If to_made = 2 Then
                'filter &= " and good.good_sys_id NOT IN (SELECT top 1 cash_history.good_sys_id FROM cash_history WHERE cash_history.state=4 and cash_history.good_sys_id=good.good_sys_id AND cash_history.change_state_date>'01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "') and good.good_sys_id NOT IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id) "
                'для сервера
                filter &= " and good.good_sys_id NOT IN (SELECT top 1 cash_history.good_sys_id FROM cash_history WHERE cash_history.state=4 and cash_history.good_sys_id=good.good_sys_id AND cash_history.change_state_date>'" & lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue & "') and good.good_sys_id NOT IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='" & lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id) "

                'filter &= " and (select top 1 good_sys_id from cash_history WHERE cash_history.good_sys_id=good.good_sys_id and cash_history.start_date<>'" & lstMonth.SelectedValue & "." & "01" & "." & lstYear.SelectedValue & "' and (cash_history.start_date<'" & lstMonth.SelectedValue + 1 & "." & "01" & "." & lstYear.SelectedValue & "'  and cash_history.start_date>'" & lstMonth.SelectedValue - 1 & "." & "01" & "." & lstYear.SelectedValue & "') and cash_history.state='1') = good.good_sys_id"
            End If

            to_made = 0

            Session("filter") = filter
            Session("filter2") = filter2

            grdTO.PageSize = 10
            grdTO.CurrentPageIndex = 0
            bind(filter)
        End Sub

        Protected Sub lnkDelEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0

            Dim query = ""
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    If n = 1 Then query &= "," & grdTO.DataKeys.Item(j)
                    If n = 0 Then query &= grdTO.DataKeys.Item(j)
                    n = 1
                End If
            Next

            Dim user = Session("User").sys_id
            query = "UPDATE good SET employee_cto=null, confirmed=null where good_sys_id IN (" & query & ")"
            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            bind(Session("Filter"))
        End Sub

        Protected Sub lnkSetTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSetTO.Click
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            'Для(сервера)
            Dim d = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)
            'Dim d = DateTime.Parse(lstMonth.SelectedValue & "." & "01" & "." & lstYear.SelectedValue)

            'd = Format(d, "dd/MM/yyyy")

            'Проверяем корректность введенных данных
            Dim date1 As Date = Now
            If (d >= Now) Then
                msgCashregister.Text = "!!! Выбранный период больше текущего"
                Exit Sub
            End If

            If tbxCloseDate.Text = "" Then
                msgCashregister.Text = "!!! Вы не указали дату проведения ТО"
                Exit Sub
            End If

            'Для сервера
            Dim parce = Split(tbxCloseDate.Text, ".")
            'Dim closedate = DateTime.Parse(parce(1) & "." & parce(0) & "." & parce(2))
            Dim closedate = DateTime.Parse(parce(0) & "." & parce(1) & "." & parce(2))
            'Dim closedate = DateTime.Parse(tbxCloseDate.Text)

            'убрал пока
            'closedate = Format(closedate, "dd/MM/yyyy")

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True And (grdTO.Items(j).BackColor <> Drawing.Color.FromArgb(250, 210, 210)) Then
                    'тут вставляем проведение ТО
                    'проверяем, проводилось ли уже в этом месяце:

                    Dim reader As SqlClient.SqlDataReader
                    query = "SELECT TOP 1 start_date FROM cash_history WHERE good_sys_id='" & grdTO.DataKeys.Item(j) & "' AND state='1' AND start_date='" & d & "' ORDER by sys_id DESC"
                    Dim sample_date = ""
                    reader = dbSQL.GetReader(query)
                    If reader.Read() Then
                        Try
                            sample_date = reader.Item(0)
                            'MsgBox(sample_date)
                        Catch
                        End Try
                    Else
                    End If
                    reader.Close()

                    d = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)
                    d = Format(d, "dd/MM/yyyy")
                    'MsgBox(sample_date & " --- " & d)

                    If sample_date <> d Then
                        'MsgBox(sample_date & " --- " & d)
                        d = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)
                        'd = Format(d, "dd/MM/yyyy")

                        cmd = New SqlClient.SqlCommand("insert_TO")
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@pi_good_sys_id", grdTO.DataKeys.Item(j))
                        cmd.Parameters.AddWithValue("@pi_start_date", d)
                        cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                        cmd.Parameters.AddWithValue("@pi_close_date", closedate)

                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet
                        adapt.Fill(ds)
                    End If

                End If
            Next

            If chk_show_torg.Checked = True Then
                If (d <= Now) Then
                    For j = 0 To grdTO_prod.Items.Count - 1
                        If CType(grdTO_prod.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True And (grdTO_prod.Items(j).BackColor <> Drawing.Color.FromArgb(250, 210, 210)) Then

                            cmd = New SqlClient.SqlCommand("insert_prod_TO")
                            cmd.CommandType = CommandType.StoredProcedure

                            cmd.Parameters.AddWithValue("@pi_good_sys_id", grdTO_prod.DataKeys.Item(j))
                            cmd.Parameters.AddWithValue("@pi_start_date", d)
                            cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                            cmd.Parameters.AddWithValue("@pi_close_date", closedate)

                            adapt = dbSQL.GetDataAdapter(cmd)
                            ds = New DataSet
                            adapt.Fill(ds)
                        End If
                    Next
                Else
                    If Me.Title Like "%%" Then

                    End If
                    MsgBox("Выбранный период больше текущего", MsgBoxStyle.Critical, "Ошибка")
                End If
            End If
            bind(Session("Filter"))
        End Sub

        Protected Sub lnkDelTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelTO.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            If lstEmployee.SelectedValue <> "" Then
                query = "DELETE FROM cash_history WHERE start_date='" & lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue & "' AND good_sys_id IN (SELECT good_sys_id FROM good WHERE place_rn_id='" & lstPlaceRegion.SelectedValue & "' AND employee_cto='" & lstEmployee.SelectedValue & "')"
            Else
                query = "DELETE FROM cash_history WHERE start_date='" & lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue & "' AND good_sys_id IN (SELECT good_sys_id FROM good WHERE place_rn_id='" & lstPlaceRegion.SelectedValue & "')"
            End If

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            'For j = 0 To grdTO.Items.Count - 1
            'If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
            'And (grdTO.Items(j).BackColor = Drawing.Color.FromArgb(250, 210, 210))
            'query = "DELETE FROM cash_history WHERE (sys_id=(SELECT TOP 1 sys_id FROM cash_history WHERE (good_sys_id = '" & grdTO.DataKeys.Item(j) & "') AND (state = 1) ORDER BY sys_id DESC))"
            'adapt = dbSQL.GetDataAdapter(query)
            'ds = New DataSet
            'adapt.Fill(ds)
            'End If
            'Next

            'If chk_show_torg.Checked = True Then
            'For j = 0 To grdTO_prod.Items.Count - 1
            'If CType(grdTO_prod.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True And (grdTO_prod.Items(j).BackColor = Drawing.Color.FromArgb(250, 210, 210)) Then
            'query = "DELETE FROM goodto_history WHERE (sys_id=(SELECT TOP 1 sys_id FROM goodto_history WHERE (goodto_sys_id = '" & grdTO_prod.DataKeys.Item(j) & "') AND (state = 1) ORDER BY sys_id DESC))"
            'adapt = dbSQL.GetDataAdapter(query)
            'ds = New DataSet
            ' adapt.Fill(ds)
            ' End If
            ' Next
            'End If

            bind(Session("Filter"))
        End Sub

        Protected Sub lnkExportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportData.Click
            'grdTO.Columns(0).Visible = True
            Dim s As String = String.Empty
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked Then
                    s &= grdTO.DataKeys(grdTO.Items(j).ItemIndex) & ","
                End If
            Next

            Session("selected_KKM") = s.TrimEnd(",")

            Dim strRequest$ = "documents.aspx?t=54"
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Protected Sub lnk_show_no_comfirmed_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_show_no_comfirmed.Click
            Dim filter
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = True

            filter = " where num_control_cto like 'МН%' AND employee_cto is not null AND confirmed is null"
            Session("filter") = filter
            show_state = 0
            to_made = 0
            bind(filter)
        End Sub

        Protected Sub lnkConfirmEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConfirmEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    If n = 1 Then query &= "," & grdTO.DataKeys.Item(j)
                    If n = 0 Then query &= grdTO.DataKeys.Item(j)
                    n = 1
                End If
            Next
            Dim user = Session("User").sys_id
            query = "UPDATE good SET confirmed=1 where good_sys_id IN (" & query & ")"
            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            bind(Session("Filter"))
        End Sub

        Protected Sub lnkNotTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNotTO.Click
            Dim filter
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = True
            Dim d1 As Date
            d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)

            filter = " where num_control_cto like 'МН%' AND employee_cto is not null AND confirmed is null and lastTO < '" & d1 & "'"
            Session("filter") = filter
            bind(filter)
        End Sub

        Protected Sub lnkRaspl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRaspl.Click
            Dim filter
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = True

            filter = " where [dbo].[good].[num_control_cto] like 'МН%' AND [dbo].[good].[employee_cto] is not null"

            Dim employees$ = ""
            For Each item As ListItem In lstEmployee.Items
                If item.Value <> ClearString Then
                    If item.Selected Then employees &= item.Value & ","
                End If
            Next item
            If employees <> "" Then filter &= " and good.employee_cto in (" & employees.TrimEnd(",") & ") "

            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Value <> ClearString Then
                    If item.Selected Then place_name &= item.Value & ","
                End If
            Next item
            If place_name <> "" Then filter &= " and good.place_rn_id in (" & place_name.TrimEnd(",") & ") "

            Dim good_type$ = ""
            For Each item As ListItem In lstGoodType.Items
                If item.Value <> ClearString Then
                    If item.Selected Then good_type &= item.Value & ","
                End If
            Next item
            If good_type <> "" Then filter &= " and good.good_type_sys_id in (" & good_type.TrimEnd(",") & ") "
            Session("filter") = filter
            show_state = 0
            to_made = 0
            bind(filter)
        End Sub

        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
            Dim s As String = String.Empty
            Dim j

            Dim strRequest$ = "documents.aspx?t=10&s=10&c="

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & CType(grdTO.Items(j).FindControl("lblPayerId"), Label).Text & "&good_type=" & CType(grdTO.Items(j).FindControl("lblGoodType"), Label).Text & "')</script>"
                    Me.RegisterStartupScript("report", strRequest)
                    'lnkExportData.Visible = False
                End If
            Next

        End Sub

        Protected Sub chk_show_kkm_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_show_kkm.CheckedChanged
            lstGoodType.Items.Clear()
            If chk_show_kkm.Checked = True And chk_show_torg.Checked = False Then
                grdTO.Visible = True
                grdTO_prod.Visible = False
                LoadGoodType(1)
            ElseIf chk_show_kkm.Checked = False And chk_show_torg.Checked = True Then
                grdTO.Visible = False
                grdTO_prod.Visible = True
                LoadGoodType(2)
            ElseIf chk_show_kkm.Checked = True And chk_show_torg.Checked = True Then
                grdTO.Visible = True
                grdTO_prod.Visible = True
                LoadGoodType(3)
            End If
        End Sub

        Protected Sub chk_show_torg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_show_torg.CheckedChanged
            lstGoodType.Items.Clear()
            If chk_show_kkm.Checked = True And chk_show_torg.Checked = False Then
                grdTO.Visible = True
                grdTO_prod.Visible = False
                LoadGoodType(1)
            ElseIf chk_show_kkm.Checked = False And chk_show_torg.Checked = True Then
                grdTO.Visible = False
                grdTO_prod.Visible = True
                LoadGoodType(2)
            ElseIf chk_show_kkm.Checked = True And chk_show_torg.Checked = True Then
                grdTO.Visible = True
                grdTO_prod.Visible = True
                LoadGoodType(3)
            End If
        End Sub

        Protected Sub grdTO_prod_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdTO_prod.ItemDataBound
            Dim s$ = ""

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s & "; Договор №" & e.Item.DataItem("dogovor")
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i

                s = ""
                If Not IsDBNull(e.Item.DataItem("dolg")) Then
                    s = s & e.Item.DataItem("dolg")
                End If
                CType(e.Item.FindControl("lblDolg"), Label).Text = s

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    If lnkSetEmployee.Visible = True Then
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "<b>" + GetRussianDate(e.Item.DataItem("lastTO")) + "</b><br><br>" + e.Item.DataItem("lastTOMaster")
                    Else
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "<b>" + GetRussianDate(e.Item.DataItem("lastTO")) + "</b>"
                    End If
                    e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                Else
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "ТО не проводилось"
                End If
                '
                'Картинки
                '
                If Not IsDBNull(e.Item.DataItem("alert")) Then
                    s = CStr(e.Item.DataItem("alert"))
                End If

                e.Item.FindControl("imgAlert").Visible = s.Length > 0
                If s.Length > 0 Then CType(e.Item.FindControl("imgAlert"), HyperLink).ToolTip = s
                e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso e.Item.DataItem("support") = "1"

                Dim b As Boolean = e.Item.DataItem("repair")
                e.Item.FindControl("imgRepair").Visible = b

                If b Then
                    Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                    If i > 1 Then
                        CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip = "В ремонте. До этого в ремонте был " & i - 1 & " раз(а)"
                    Else
                        CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip = "В ремонте. До этого в ремонте не был"
                    End If
                End If

                e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                If e.Item.FindControl("imgRepaired").Visible Then
                    CType(e.Item.FindControl("imgRepaired"), HyperLink).ToolTip = "Был в ремонте " & CInt(e.Item.DataItem("repaired")) & " раз(а)"
                End If
                '
                'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                '   s = e.Item.DataItem("stateTO")
                'End If
                '
                CType(e.Item.FindControl("lnkStatus"), HyperLink).Text = "Просмотр"
                CType(e.Item.FindControl("lnkStatus"), HyperLink).ToolTip = "Просмотр"
                '
                'Если проведено ТО
                '
                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    If Month(e.Item.DataItem("lastTO")) = Month(Now()) And Year(e.Item.DataItem("lastTO")) = Year(Now()) Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                    End If
                End If
            End If
        End Sub

        Protected Sub grdTO_prod_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdTO_prod.SortCommand
            If ViewState("goodsort2") = e.SortExpression Then
                ViewState("goodsort2") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort2") = e.SortExpression
            End If
            bind(Session("filter"))
        End Sub

        Protected Sub lnk_onTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_onTO.Click
            show_state = 1
            findgood()
        End Sub

        Protected Sub lnk_stopTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_stopTO.Click
            show_state = 6
            findgood()
        End Sub

        Protected Sub lnk_delTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_delTO.Click
            show_state = 2
            findgood()
        End Sub

        Protected Sub lnkConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkConduct.Click
            show_state = 0
            to_made = 1
            findgood()
        End Sub

        Protected Sub lnkNotConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkNotConduct.Click
            show_state = 0
            to_made = 2
            findgood()
        End Sub

        Protected Sub lnkSetRaon_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSetRaon.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    If n = 1 Then query &= "," & grdTO.DataKeys.Item(j)
                    If n = 0 Then query &= grdTO.DataKeys.Item(j)
                    n = 1
                End If
            Next

            Dim pl_rn_id
            pl_rn_id = lstPlaceRegion.SelectedItem.Value

            query = "UPDATE good SET place_rn_id='" & pl_rn_id & "' where good_sys_id IN (" & query & ")"
            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            'повтряется поле good_sys_id
            bind(Session("Filter"))
        End Sub
    End Class
End Namespace
