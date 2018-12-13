Imports System.Diagnostics
Imports System.IO
Imports service

Namespace Kasbi
    Partial Class MasterCTO
        Inherits PageBase
        Protected WithEvents lnkDismissalIMNS As System.Web.UI.WebControls.LinkButton

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Const ClearString = "-------"
        Dim i = 0, iNumGoodOfError = 0
        Dim iType%, j%
        Dim show_state = 0
        Dim to_made = 0
        Dim to_made_tmp = 0
        Dim to_made_cnd = 0
        Private serviceTo As ServiceTo = New ServiceTo()
        Private serviceDoc As ServiceDocuments = New ServiceDocuments()


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Not IsPostBack Then
                LoadPlaceRegion()
                LoadEmployee()
                LoadGoodType(1)
                'Выставляем текущий месяц и год
                lstMonth.SelectedIndex = Month(Now) - 1
                lstYear.SelectedValue = Year(Now).ToString()
                tbxCloseDate.Text = Date.Today().ToString("dd.MM.yyyy")
                'If Year(Now) > 2002 And Year(Now) < 2019 Then
                '    lstYear.SelectedIndex = Year(Now) - 2003
                'ElselnkStatus
                '    lstYear.SelectedIndex = 0
                'End If
            End If

            myModal.Style.Add("display", "none")

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

        Private Sub grdTO_SortCommand(ByVal source As System.Object,
                                      ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
            Handles grdTO.SortCommand
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

            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet


            cmd = New SqlClient.SqlCommand("get_employee_by_role_id")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_role_id", 0)

            Try
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                ds.Tables(0).DefaultView.Sort = "name"
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

            filter =
                " where good.num_control_cto like 'МН%' AND (good.employee_cto is null OR good.employee_cto=0) AND (SELECT top 1 cash_history.state FROM cash_history WHERE cash_history.state in (2,3,4) AND cash_history.good_sys_id=good.good_sys_id ORDER BY cash_history.sys_id DESC) = '4'"
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

        Private Sub grdTO_ItemDataBound(ByVal sender As System.Object,
                                        ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdTO.ItemDataBound
            Dim s$ = ""
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If IsDBNull(e.Item.DataItem("state")) Then
                    e.Item.DataItem("state") = 0
                End If

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    If Month(e.Item.DataItem("lastTO")) = Month(Now()) And Year(e.Item.DataItem("lastTO")) = Year(Now()) _
                        Then
                        to_made_tmp = 1
                    Else
                        to_made_tmp = 2
                    End If

                    If _
                        Month(e.Item.DataItem("lastTO")) = lstMonth.Text And
                        Year(e.Item.DataItem("lastTO")) = lstYear.Text Then
                        to_made_tmp = 1
                    Else
                        to_made_tmp = 2
                    End If
                End If

                If _
                    (e.Item.DataItem("state") = show_state Or show_state = 0) And
                    ((to_made = to_made_tmp And Not IsDBNull(e.Item.DataItem("lastTO"))) Or to_made = 0) Then
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
                    query = "SELECT count(id_rubr) FROM cash_rubr WHERE good_sys_id='" & e.Item.DataItem("good_sys_id") &
                            "'"
                    reader = dbSQL.GetReader(query)
                    If reader.Read() Then
                        Try
                            count_rubr = reader.Item(0)
                        Catch
                        End Try
                    Else
                    End If
                    reader.Close()
                    CType(e.Item.FindControl("lblNumGood"), WebControls.HyperLink).NavigateUrl =
                        "cash_rubr/default.aspx?cash=" & e.Item.DataItem("good_sys_id")


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
                    CType(e.Item.FindControl("lblNumGood"), WebControls.HyperLink).Text = i
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
                    If s.Length > 0 Then CType(e.Item.FindControl("imgAlert"), WebControls.HyperLink).ToolTip = s
                    e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso
                                                               e.Item.DataItem("support") = "1"

                    Dim b As Boolean = e.Item.DataItem("repair")
                    e.Item.FindControl("imgRepair").Visible = b

                    If b Then
                        Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                        If i > 1 Then
                            CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                                "В ремонте. До этого в ремонте был " & i - 1 & " раз(а)"
                        Else
                            CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                                "В ремонте. До этого в ремонте не был"
                        End If
                    End If

                    e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                    If e.Item.FindControl("imgRepaired").Visible Then
                        CType(e.Item.FindControl("imgRepaired"), WebControls.HyperLink).ToolTip = "Был в ремонте " &
                                                                                                  CInt(
                                                                                                      e.Item.DataItem(
                                                                                                          "repaired")) &
                                                                                                  " раз(а)"
                    End If

                    If IsDBNull(e.Item.DataItem("state_skno")) Then
                        e.Item.FindControl("imgSupportSKNO").Visible = 0
                    Else
                        e.Item.FindControl("imgSupportSKNO").Visible = e.Item.DataItem("state_skno")
                    End If


                    '
                    'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                    '   s = e.Item.DataItem("stateTO")
                    'End If
                    '
                    CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).Text = "Просмотр"
                    CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).ToolTip = "Просмотр"

                    If IsDBNull(e.Item.DataItem("state")) Then
                        e.Item.DataItem("state") = 0
                    End If

                    If Not IsDBNull(e.Item.DataItem("lastTO")) And e.Item.DataItem("state") <> 4 Then
                        If e.Item.DataItem("state") = 6 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "ТО приостановлено<br>" &
                                                                                  GetRussianDate(e.Item.DataItem("ldate")) &
                                                                                  " (на " & e.Item.DataItem("period") &
                                                                                  " месяцев)<br>"
                        End If
                        If e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "Снят с ТО<br>" &
                                                                                  GetRussianDate(e.Item.DataItem("ldate")) &
                                                                                  "<br>"
                        End If
                        If lnkSetEmployee.Visible = True Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "<b>" +
                                                                                  GetRussianDate(
                                                                                      e.Item.DataItem("lastTO")) +
                                                                                  "</b><br><br>" +
                                                                                  e.Item.DataItem("lastTOMaster")
                        Else
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "<b>" +
                                                                                  GetRussianDate(
                                                                                      e.Item.DataItem("lastTO")) +
                                                                                  "</b>"
                        End If
                        e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                    ElseIf IsDBNull(e.Item.DataItem("lastTO")) And e.Item.DataItem("state") = 4 Then
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "Поставлен на ТО" & "<br>" &
                                                                             GetRussianDate(e.Item.DataItem("ldate")) &
                                                                             "<br>ТО не проводилось"
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
                        If _
                            Month(e.Item.DataItem("lastTO")) = Month(Now()) And
                            Year(e.Item.DataItem("lastTO")) = Year(Now()) Then
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
                        CType(e.Item.FindControl("lblNumGood"), WebControls.HyperLink).ForeColor = Color.White
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
            Dim m() As String =
                    {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ",
                     " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Sub SelectAll(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j
            grdTO.Columns(0).Visible = True
            Dim s As Boolean =
                    CType(grdTO.Controls.Item(0).Controls.Item(0).FindControl("cbxSelectAll"), WebControls.CheckBox).
                    Checked

            For j = 0 To grdTO.Items.Count - 1
                If grdTO.Items(j).Visible = True Then
                    CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = s
                End If
            Next
        End Sub

        Sub SelectAll_prod(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j
            grdTO_prod.Columns(0).Visible = True
            If grdTO_prod.Columns(1).Visible = False Then
                'MsgBox("Вы не можете добалять пустую запись в таблицу", MsgBoxStyle.Information, "Пожалуйста, введите достоверные данные о поставке")
            End If
            Dim s As Boolean =
                    CType(grdTO_prod.Controls.Item(0).Controls.Item(0).FindControl("cbxSelectAll"), WebControls.CheckBox) _
                    .Checked

            For j = 0 To grdTO_prod.Items.Count - 1
                CType(grdTO_prod.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = s
            Next
        End Sub

        Protected Sub lnkSetEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkSetEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = True Then
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

            If txtFindGoodCTO.Text <> "" Then _
                filter &= " and good.num_control_cto like '%" & txtFindGoodCTO.Text & "%'  "

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
                filter &=
                    " and good.good_sys_id IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='" &
                    lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue &
                    "' and cash_history.good_sys_id=good.good_sys_id)"
            End If
            If to_made = 2 Then
                'filter &= " and good.good_sys_id NOT IN (SELECT top 1 cash_history.good_sys_id FROM cash_history WHERE cash_history.state=4 and cash_history.good_sys_id=good.good_sys_id AND cash_history.change_state_date>'01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "') and good.good_sys_id NOT IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id) "
                'для сервера
                filter &=
                    " and good.good_sys_id NOT IN (SELECT top 1 cash_history.good_sys_id FROM cash_history WHERE cash_history.state=4 and cash_history.good_sys_id=good.good_sys_id AND cash_history.change_state_date>'" &
                    lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue &
                    "') and good.good_sys_id NOT IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='" &
                    lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue &
                    "' and cash_history.good_sys_id=good.good_sys_id) "

                'filter &= " and (select top 1 good_sys_id from cash_history WHERE cash_history.good_sys_id=good.good_sys_id and cash_history.start_date<>'" & lstMonth.SelectedValue & "." & "01" & "." & lstYear.SelectedValue & "' and (cash_history.start_date<'" & lstMonth.SelectedValue + 1 & "." & "01" & "." & lstYear.SelectedValue & "'  and cash_history.start_date>'" & lstMonth.SelectedValue - 1 & "." & "01" & "." & lstYear.SelectedValue & "') and cash_history.state='1') = good.good_sys_id"
            End If

            to_made = 0

            Session("filter") = filter
            Session("filter2") = filter2

            grdTO.PageSize = 10
            grdTO.CurrentPageIndex = 0
            bind(filter)
        End Sub

        Protected Sub lnkDelEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkDelEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0

            Dim query = ""
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = True Then
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
            Dim closedate As Date

            'Для(сервера)
            Dim d As DateTime = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)
            Dim listOfIndexOfSelectCheckBox As ArrayList = New ArrayList()

            For k = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(k).FindControl("cbxSelect"), WebControls.CheckBox).Checked Then
                    listOfIndexOfSelectCheckBox.Add(k)
                End If
            Next

            If listOfIndexOfSelectCheckBox.Count <> 0 Then

                'Проверяем корректность введенных данных
                If Not serviceTo.CheckDate(d, tbxCloseDate.Text) Then
                    msgCashregister.Text = serviceTo.GetTextStringAllExeption()
                    Exit Sub
                End If

                closedate = DateTime.Parse(tbxCloseDate.Text)

                Dim kkmDs As DataSet = CType(Session("KKM_ds"), DataSet)
                For Each index As Integer In listOfIndexOfSelectCheckBox
                    If _
                        serviceTo.CheckCashHistoryItem(Integer.Parse(grdTO.DataKeys.Item(index).ToString()), d,
                                                       tbxCloseDate.Text) Then
                        cmd = New SqlClient.SqlCommand("insert_TO")
                        cmd.CommandType = CommandType.StoredProcedure

                        cmd.Parameters.AddWithValue("@pi_good_sys_id", grdTO.DataKeys.Item(index))
                        cmd.Parameters.AddWithValue("@pi_start_date", d)
                        cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                        cmd.Parameters.AddWithValue("@pi_close_date", closedate)

                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet
                        adapt.Fill(ds)

                    End If

                Next

                Dim dv As DataView = New DataView(kkmDs.Tables(0))
                Dim filter As String = String.Empty
                If serviceTo.HaveAnyExeption() Then
                    filter = "good_sys_id in (" & String.Join(", ", serviceTo.GetListStringGoodSysId()) & ")"
                End If


                dv.RowFilter = filter
                grdError.DataSource = dv
                grdError.DataKeyField = "good_sys_id"
                grdError.DataBind()

                If serviceTo.HaveAnyExeption() Then
                    myModal.Style.Add("display", "block")
                End If
            Else
                msgCashregister.Text = "Выберите кассовый аппарат, которому хотите провести ТО"
                Exit Sub
            End If


            If chk_show_torg.Checked = True Then
                If (d <= Now) Then
                    For j = 0 To grdTO_prod.Items.Count - 1
                        If _
                            CType(grdTO_prod.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = True And
                            (grdTO_prod.Items(j).BackColor <> Drawing.Color.FromArgb(250, 210, 210)) Then
                            closedate = DateTime.Parse(tbxCloseDate.Text)
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

        Private Sub grdError_ItemDataBound(ByVal sender As System.Object,
                                           ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdError.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                iNumGoodOfError += 1
                CType(e.Item.FindControl("lblNumGood"), WebControls.HyperLink).Text = iNumGoodOfError.ToString()
                CType(e.Item.FindControl("lblToExeption"), Label).Text =
                    serviceTo.GetExeptionTextByGoodId(CInt(CType(e.Item.FindControl("lblGood"), Label).Text))
            End If
        End Sub

        Protected Sub lnkDelTO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDelTO.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""

            If lstEmployee.SelectedValue <> "" Then
                query = "DELETE FROM cash_history WHERE start_date='" & lstMonth.SelectedValue & "/01/" &
                        lstYear.SelectedValue & "' AND good_sys_id IN (SELECT good_sys_id FROM good WHERE place_rn_id='" &
                        lstPlaceRegion.SelectedValue & "' AND employee_cto='" & lstEmployee.SelectedValue & "')"
            Else
                query = "DELETE FROM cash_history WHERE start_date='" & lstMonth.SelectedValue & "/01/" &
                        lstYear.SelectedValue & "' AND good_sys_id IN (SELECT good_sys_id FROM good WHERE place_rn_id='" &
                        lstPlaceRegion.SelectedValue & "')"
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

        Protected Sub lnkExportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkExportData.Click
            'grdTO.Columns(0).Visible = True
            Dim s As String = String.Empty
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked Then
                    s &= grdTO.DataKeys(grdTO.Items(j).ItemIndex) & ","
                End If
            Next

            Session("selected_KKM") = s.TrimEnd(",")

            Dim strRequest$ = "documents.aspx?t=54"
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest &
                         "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Protected Sub lnk_show_no_comfirmed_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnk_show_no_comfirmed.Click
            Dim filter
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = True

            filter = " where num_control_cto like 'МН%' AND employee_cto is not null AND confirmed is null"
            Session("filter") = filter
            show_state = 0
            to_made = 0
            bind(filter)
        End Sub

        Protected Sub lnkConfirmEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkConfirmEmployee.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j
            Dim n = 0
            Dim query = ""
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = True Then
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

            filter =
                " where num_control_cto like 'МН%' AND employee_cto is not null AND confirmed is null and lastTO < '" &
                d1 & "'"
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

        'Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        '    Dim s As String = String.Empty
        '    Dim j As Integer
        '    Dim strRequest$ = "documents.aspx?t=10&s=10&c_list="
        '    Dim strRequestPayerId As String = ""
        '    Dim strRequestGoodId As String = ""


        '    For j = 0 To grdTO.Items.Count - 1
        '        If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked Then
        '            strRequestPayerId &= CType(grdTO.Items(j).FindControl("lblPayerId"), Label).Text & ","
        '            strRequestGoodId &= CType(grdTO.Items(j).FindControl("lblGood"), Label).Text & ","
        '        End If
        '    Next

        '    If strRequestPayerId.Length > 0 And strRequestGoodId.Length > 0 Then

        '        strRequestPayerId = Left(strRequestPayerId, strRequestPayerId.Length - 1)
        '        strRequestGoodId = Left(strRequestGoodId, strRequestGoodId.Length - 1)

        '        Dim script As String = "<script language='javascript' type='text/javascript'> var xhr = new XMLHttpRequest(); var url = '/documents.aspx?t=10&s=10'; xhr.open('POST', url, true); xhr.setRequestHeader('Content-type', 'application/json'); xhr.onreadystatechange = function () { if (xhr.readyState === 4 && xhr.status === 200) { var json = JSON.parse(xhr.responseText); } }; var data = JSON.stringify({'c_list': '" & strRequestPayerId & "', 'good_id_list': '" & strRequestGoodId & "'}); xhr.send(data); </script>"
        '        Me.RegisterStartupScript("report", script)
        '        strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & strRequestPayerId & "&good_id_list=" & strRequestGoodId & "')</script>"
        '        Me.RegisterStartupScript("report", strRequest)
        '    End If

        'End Sub

        Protected Sub chk_show_kkm_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles chk_show_kkm.CheckedChanged
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

        Protected Sub chk_show_torg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles chk_show_torg.CheckedChanged
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

        Protected Sub grdTO_prod_ItemDataBound(ByVal sender As Object,
                                               ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdTO_prod.ItemDataBound
            Dim s$ = ""

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s & "; Договор №" &
                                                                            e.Item.DataItem("dogovor")
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
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "<b>" +
                                                                             GetRussianDate(e.Item.DataItem("lastTO")) +
                                                                             "</b><br><br>" +
                                                                             e.Item.DataItem("lastTOMaster")
                    Else
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "<b>" +
                                                                             GetRussianDate(e.Item.DataItem("lastTO")) +
                                                                             "</b>"
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
                If s.Length > 0 Then CType(e.Item.FindControl("imgAlert"), WebControls.HyperLink).ToolTip = s
                e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso
                                                           e.Item.DataItem("support") = "1"

                Dim b As Boolean = e.Item.DataItem("repair")
                e.Item.FindControl("imgRepair").Visible = b

                If b Then
                    Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                    If i > 1 Then
                        CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                            "В ремонте. До этого в ремонте был " & i - 1 & " раз(а)"
                    Else
                        CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                            "В ремонте. До этого в ремонте не был"
                    End If
                End If

                e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                If e.Item.FindControl("imgRepaired").Visible Then
                    CType(e.Item.FindControl("imgRepaired"), WebControls.HyperLink).ToolTip = "Был в ремонте " &
                                                                                              CInt(
                                                                                                  e.Item.DataItem(
                                                                                                      "repaired")) &
                                                                                              " раз(а)"
                End If
                '
                'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                '   s = e.Item.DataItem("stateTO")
                'End If
                '
                CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).Text = "Просмотр"
                CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).ToolTip = "Просмотр"
                '
                'Если проведено ТО
                '
                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    If Month(e.Item.DataItem("lastTO")) = Month(Now()) And Year(e.Item.DataItem("lastTO")) = Year(Now()) _
                        Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                    End If
                End If
            End If
        End Sub

        Protected Sub grdTO_prod_SortCommand(ByVal source As Object,
                                             ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
            Handles grdTO_prod.SortCommand
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

        Protected Sub lnkNotConduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkNotConduct.Click
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
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked = True Then
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

        Private Sub AktForTOandDolg(withDate As Boolean)
            Dim checkGoods As ListDictionary
            checkGoods = FindCheckGoods()
            If checkGoods.Count > 0 Then
                serviceDoc.AktForTOandDolg(checkGoods, Response, withDate, DateTime.Parse(tbxCloseDate.Text))
            End If
            'End If
        End Sub

        Protected Sub lnk_aktForTOandDolgWithtDate_Click(sender As Object, e As EventArgs) _
            Handles lnk_aktForTOandDolgWithtDate.Click
            AktForTOandDolg(True)
        End Sub

        Protected Sub lnk_aktForTOandDolgWithoutDate_Click(sender As Object, e As EventArgs) _
            Handles lnk_aktForTOandDolgWithoutDate.Click
            AktForTOandDolg(False)
        End Sub


        Private Function FindCheckGoods() As ListDictionary
            Dim checkGoods As ListDictionary = New ListDictionary

            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), WebControls.CheckBox).Checked Then
                    checkGoods.Add(grdTO.DataKeys.Item(j).ToString(),
                                   CType(grdTO.Items(j).FindControl("lblNumGood"), WebControls.HyperLink).Text)
                End If
            Next

            Return checkGoods
        End Function

        Private Function FindCheckGoods1() As Hashtable
            Dim checkGoods As Hashtable = New Hashtable
            Dim _
                list As _
                    New List(Of Integer)(
                        {1821, 1830, 1897, 1928, 2131, 2132, 2293, 4394, 5257, 5571, 6141, 7658, 8646, 10358, 10936,
                         11041, 11115, 11362, 11465, 11580, 11587, 12017, 12100, 12182, 12196, 12202, 12236, 12238,
                         12259, 12515, 12729, 12730, 12793, 12794, 12796, 12893, 13412, 13494, 13599, 13801, 13803,
                         13966, 13998, 14026, 14060, 14099, 14100, 14117, 14118, 14120, 14186, 14187, 14245, 14260,
                         14271, 14297, 14327, 15345, 15551, 15602, 15628, 15629, 15768, 15834, 15976, 15977, 16067,
                         16078, 16163, 16195, 16459, 16468, 16541, 16542, 16612, 16783, 16960, 16963, 17057, 17306,
                         17353, 17558, 17682, 17687, 17705, 17798, 17840, 17915, 18067, 18068, 18142, 18230, 18390,
                         18492, 18626, 19086, 19097, 19106, 19124, 19193, 19365, 19398, 19454, 19518, 19540, 19562,
                         19806, 19869, 19887, 19931, 20012, 20038, 20076, 20230, 20300, 20328, 20329, 20334, 20364,
                         20416, 20458, 20463, 20582, 20588, 20689, 20703, 20747, 20752, 20766, 20781, 20783, 20910,
                         21024, 21048, 21086, 21091, 21103, 21130, 21427, 21428, 21429, 21430, 21431, 21432, 21435,
                         21436, 21437, 21440, 21443, 21453, 21644, 21647, 21696, 21699, 21714, 21840, 21842, 21875,
                         21876, 21918, 21921, 21980, 21985, 21987, 21989, 21990, 21995, 21996, 22008, 22015, 22016,
                         22018, 22020, 22021, 22022, 22023, 22037, 22038, 22039, 22132, 22162, 22167, 22174, 22176,
                         22230, 22239, 22240, 22251, 22277, 22281, 22282, 22283, 22284, 22285, 22286, 22287, 22288,
                         22289, 22290, 22291, 22292, 22296, 22299, 22302, 22304, 22325, 22328, 22331, 22334, 22336,
                         22337, 22338, 22363, 22364, 22365, 22366, 22397, 22409, 22450, 22452, 22463, 22474, 22515,
                         22520, 22521, 22522, 22523, 22524, 22526, 22588, 22589, 22590, 22594, 22595, 22596, 22602,
                         22603, 22604, 22606, 22607, 22623, 22624, 22625, 22627, 22633, 22678, 22680, 22687, 22707,
                         22786, 22792, 22799, 22882, 22887, 22899, 22900, 22902, 22906, 22914, 22922, 22941, 22946,
                         22947, 22984, 22998, 23014, 23015, 23018, 23056, 23105, 23108, 23109, 23150, 23152, 23176,
                         23180, 23190, 23191, 23192, 23259, 23322, 23345, 23350, 23357, 23373, 23427, 23428, 23430,
                         23436, 23455, 23462, 23485, 23509, 23537, 23582, 23593, 23599, 23613, 23815, 23860, 23863,
                         23887, 23896, 23903, 24034, 24035, 24045, 24050, 24171, 24173, 24175, 24180, 24189, 24191,
                         24194, 24195, 24197, 24200, 24201, 24205, 24219, 24383, 24434, 24446, 24458, 24492, 24546,
                         24555, 24556, 24580, 24643, 24652, 24680, 24684, 24712, 24724, 24787, 24806, 24821, 24822,
                         24826, 24877, 24887, 24951, 24952, 24954, 24956, 24958, 24990, 24991, 25013, 25016, 25042,
                         25061, 25082, 25083, 25111, 25139, 25151, 25154, 25161, 25181, 25218, 25239, 25251, 25253,
                         25295, 25317, 25320, 25325, 25328, 25387, 26217, 26222, 26256, 26287, 26301, 26635, 26639,
                         26680, 26747, 26755, 26781, 26785, 26831, 27055, 27194, 27197, 27319, 27455, 27481, 27490,
                         27705, 27755, 27774, 27963, 27964, 27971, 28002, 28006, 28133, 28134, 28137, 28138, 28182,
                         28186, 28305, 28306, 28354, 28356, 28358, 28365, 28366, 28393, 28422, 28423, 28479, 28543,
                         28911, 28913, 28952, 28954, 28956, 29013, 29030, 29062, 29063, 29069, 29072, 29080, 29083,
                         29095, 29116, 29117, 29139, 29141, 29142, 29166, 29171, 29172, 29212, 29219, 29235, 29242,
                         29286, 29295, 29296, 29298, 29304, 29306, 29309, 29327, 29333, 29344, 29349, 29350, 29372,
                         29379, 29395, 29417, 29432, 29444, 29448, 29450, 29496, 29503, 29525, 29530, 29531, 29571,
                         29575, 29580, 29592, 29603, 29627, 29635, 29665, 29709, 29755, 29775, 29816, 29858, 29859,
                         29893, 29921, 29926, 29927, 29928, 29929, 29930, 29951, 29981, 29985, 29988, 30270, 30271,
                         30272, 30306, 30336, 30365, 30398, 30418, 30450, 30454, 30511, 30513, 30535, 30539, 30566,
                         30579, 30592, 30636, 30670, 30676, 30728, 30731, 30751, 30778, 30779, 30804, 30805, 30809,
                         30815, 30846, 30847, 30863, 30866, 30903, 30917, 30935, 30945, 30953, 30972, 30973, 30974,
                         30975, 30980, 31019, 31035, 31049, 31054, 31065, 31067, 31080, 31085, 31092, 31109, 31110,
                         31115, 31119, 31120, 31121, 31127, 31128, 31132, 31134, 31135, 31165, 31173, 31179, 31197,
                         31209, 31210, 31214, 31217, 31218, 31220, 31259, 31263, 31272, 31276, 31286, 31293, 31309,
                         31316, 31338, 31346, 31357, 31365, 31385, 31396, 31411, 31413, 31417, 31424, 31428, 31429,
                         31435, 31436, 31471, 31493, 31497, 31514, 31527, 31530, 31534, 31535, 31538, 31541, 31542,
                         31550, 31553, 31568, 31575, 31578, 31580, 31581, 31582, 31598, 31620, 31628, 31629, 31675,
                         31831, 31833, 31896, 31942, 31948, 31955, 31973, 32015, 32084, 32124, 32134, 32137, 32170,
                         32173, 32187, 32189, 32209, 32236, 32242, 32255, 32262, 32281, 32283, 32292, 32296, 32298,
                         32306, 32331, 32333, 32338, 32343, 32351, 32379, 32384, 32385, 32386, 32390, 32440, 32443,
                         32456, 32462, 32464, 32465, 32468, 32472, 32473, 32474, 32475, 32477, 32512, 32518, 32545,
                         32548, 32564, 32567, 32573, 32604, 32605, 32606, 32611, 32615, 32616, 32621, 32651, 32652,
                         32654, 32656, 32657, 32658, 32670, 32683, 32684, 32691, 32700, 32703, 32704, 32705, 32716,
                         32721, 32733, 32737, 32738, 32739, 32741, 32744, 32745, 32746, 32750, 32752, 32762, 32892,
                         32897, 32924, 32926, 32930, 32931, 32937, 32941, 32944, 32955, 32970, 32975, 32977, 32979,
                         32995, 32999, 33004, 33017, 33025, 33027, 33028, 33029, 33037, 33055, 33059, 33070, 33079,
                         33089, 33090, 33092, 33095, 33109, 33135, 33157, 33162, 33163, 33165, 33169, 33173, 33188,
                         33189, 33190, 33191, 33192, 33279, 33292, 33304, 33324, 33330, 33360, 33380, 33381, 33435,
                         33447, 33456, 33468, 33470, 33487, 33494, 33499, 33500, 33509, 33510, 33516, 33518, 33520,
                         33524, 33526, 33529, 33531, 33533, 33535, 33536, 33555, 33556, 33562, 33565, 33566, 33567,
                         33579, 33592, 33614, 33615, 33627, 33629, 33635, 33639, 33643, 33655, 33660, 33661, 33671,
                         33676, 33677, 33678, 33680, 33681, 33683, 33687, 33688, 33697, 33702, 33713, 33715, 33716,
                         33718, 33721, 33725, 33727, 33729, 33733, 33746, 33747, 33757, 33769, 33774, 33778, 33786,
                         33801, 33802, 33811, 33812, 33840, 33841, 33854, 33856, 33865, 33866, 33870, 33875, 33969,
                         34018, 34036, 34039, 34046, 34058, 34059, 34061, 34072, 34089, 34092, 34093, 34148, 34156,
                         34187, 34199, 34200, 34201, 34206, 34207, 34208, 34209, 34210, 34211, 34215, 34229, 34234,
                         34251, 34282, 34283, 34285, 34286, 34287, 34290, 34293, 34294, 34295, 34315, 34347, 34412,
                         34415, 34417, 34432, 34451, 34457, 34464, 34466, 34495, 34497, 34513, 34560, 34598, 34599,
                         34603, 34604, 34605, 34609, 34611, 34612, 34615, 34616, 34617, 34618, 34619, 34621, 34627,
                         34631, 34634, 34646, 34658, 34679, 34680, 34683, 34689, 34692, 34695, 34697, 34700, 34701,
                         34709, 34713, 34715, 34717, 34720, 34723, 34724, 34725, 34726, 34734, 34739, 34743, 34752,
                         34754, 34758, 34779, 34780, 34782, 34783, 34786, 34788, 34799, 34824, 34825, 34827, 34829,
                         34830, 34831, 34832, 34837, 34844, 34845, 34847, 34849, 34850, 34851, 34852, 34853, 34854,
                         34855, 34856, 34857, 34858, 34859, 34860, 34861, 34862, 34864, 34865, 34866, 34868, 34870,
                         34871, 34872, 34875, 34876, 34880, 34881, 34894, 34895, 34902, 34904, 34990, 34997, 34998,
                         35000, 35008, 35011, 35012, 35013, 35014, 35016, 35017, 35018, 35019, 35020, 35021, 35022,
                         35061, 35067, 35168, 35183, 35186, 35188, 35213, 35220, 35231, 35233, 35235, 35237, 35238,
                         35239, 35240, 35255, 35256, 35257, 35258, 35259, 35260, 35261, 35262, 35263, 35264, 35265,
                         35266, 35267, 35268, 35269, 35270, 35271, 35272, 35273, 35274, 35275, 35276, 35352, 35365,
                         35369, 35386, 35444, 35659, 35694, 35705, 35706, 35707, 35710, 35771, 35773, 35792, 35824,
                         35827, 35828, 35830, 35837, 35851, 35852, 35860, 35871, 35879, 35882, 35883, 35884, 35887,
                         35888, 35889, 35891, 35900, 35902, 35904, 35905, 35912, 35913, 35914, 35915, 35917, 35929,
                         35941, 35951, 35954, 35965, 35988, 35994, 36008, 36166, 36168, 36175, 36176, 36190, 36194,
                         36201, 36206, 36212, 36217, 36231, 36252, 36338, 36357, 36377, 36382, 36400, 36542, 36545,
                         36546, 36547, 36559, 36565, 36566, 36567, 36568, 36580, 36581, 36584, 36590, 36593, 36594,
                         36596, 36599, 36603, 36611, 36617, 36629, 36630, 36641, 36643, 36644, 36645, 36647, 36648,
                         36650, 36651, 36654, 36655, 36656, 36672, 36673, 36674, 36675, 36678, 36682, 36692, 36697,
                         36701, 36704, 36705, 36786, 36787, 36788, 36791, 36792, 36808, 36810, 36812, 36814, 36819,
                         36840, 36953, 36954, 36956, 36976, 36979, 37000, 37004, 37014, 37019, 37074, 37089, 37092,
                         37109, 37148, 37150, 37195, 37200, 37205, 37227, 37234, 37238, 37255, 37264, 37268, 37311,
                         37313, 37315, 37319, 37320, 37321, 37322, 37324, 37326, 37363, 37366, 37368, 37370, 37372,
                         37374, 37376, 37401, 37411, 37480, 37521, 37529, 37531, 37532, 37601, 37604, 37633, 37635,
                         37636, 37640, 37661, 37663, 37668, 37679, 37686, 37687, 37689, 37690, 37691, 37697, 37709,
                         37710, 37712, 37713, 37714, 37715, 37717, 37718, 37719, 37721, 37722, 37723, 37724, 37725,
                         37726, 37727, 37731, 37736, 37737, 37738, 37739, 37740, 37741, 37742, 37743, 37744, 37745,
                         37746, 37747, 37748, 37749, 37750, 37751, 37752, 37755, 37756, 37775, 37776})
            For index As Integer = 0 To list.Count - 1
                checkGoods.Add(list(index).ToString(), index + 1)
            Next
            Return checkGoods
        End Function

        'Private Sub ResponseFile(savePath As String)
        '    Dim fileExtension = savePath.Substring(savePath.Length - 3, 3)
        '    Dim file As IO.FileInfo
        '    file = New FileInfo(savePath)
        '    If file.Exists Then
        '        Response.Clear()
        '        Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
        '        Response.AddHeader("Content-Length", file.Length.ToString())
        '        Response.ContentType = "application/octet-stream"
        '        If fileExtension = "zip"
        '            Response.ContentType = "application/zip"
        '        Else
        '            Response.ContentType = "application/octet-stream"
        '        End If
        '        Response.WriteFile(savePath)
        '        Response.End()
        '    Else
        '        Response.Write("This file does not exist.")
        '    End If
        'End Sub
    End Class
End Namespace
