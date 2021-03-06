Imports System.Data.SqlClient
Imports System.Diagnostics
Imports System.IO
Imports Models
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

        Dim textSmsTemplate As String = "������ �������� ��� ���� �����={0}"

        Private ReadOnly _serviceTo As ServiceTo = New ServiceTo()
        Private ReadOnly _serviceDoc As ServiceDocuments = New ServiceDocuments()
        Private ReadOnly _serviceSms As ServiceSms = New ServiceSms()
        Private ReadOnly _serviceGood As ServiceGood = New ServiceGood()


        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Not IsPostBack Then
                LoadPlaceRegion()
                LoadEmployee()
                LoadGoodByType(1)
                '���������� ������� ����� � ���
                lstMonth.SelectedIndex = Month(Now) - 1
                lstYear.SelectedValue = Year(Now).ToString()
                tbxCloseDate.Text = Date.Today().ToString("dd.MM.yyyy")
                'If Year(Now) > 2002 And Year(Now) < 2019 Then
                '    lstYear.SelectedIndex = Year(Now) - 2003
                'ElselnkStatus
                '    lstYear.SelectedIndex = 0
                'End If
            End If
            msgCashregister.Text = ""
            myModal.Style.Add("display", "none")
            smsSendModal.Style.Add("display", "none")

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
            '�� �� ��������� ������������
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
                msgCashregister.Text = "������ ������������ ������ ������� ���������!<br>" & Err.Description
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

        Private Sub LoadGoodByType(ByVal type)
            Dim ds As DataSet = _serviceGood.GetGoodsByType(type)
            Try
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
                " where good.num_control_cto like '��%' AND (good.employee_cto is null OR good.employee_cto=0) AND (SELECT top 1 cash_history.state FROM cash_history WHERE cash_history.state in (2,3,4) AND cash_history.good_sys_id=good.good_sys_id ORDER BY cash_history.sys_id DESC) = '4'"
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
                    Dim payersysid = 0
                    If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                        s = e.Item.DataItem("payerInfo")
                        payersysid = e.Item.DataItem("payer_sys_id")
                    End If
                    '
                    '������� �������
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

                    CType(e.Item.FindControl("lblGoodOwner"), HyperLink).Text = s & "; ������� �" & dogovor
                    Dim isCto As Integer
                    If Not IsDBNull(e.Item.DataItem("payer_cto")) Then
                        isCto = Convert.ToInt32(e.Item.DataItem("payer_cto"))
                    Else
                        isCto = 0
                    End If
                    If isCto = 1
                        CType(e.Item.FindControl("lblGoodOwner"), HyperLink).NavigateUrl = "CTOList.aspx"
                    Else
                        CType(e.Item.FindControl("lblGoodOwner"), HyperLink).NavigateUrl = "CustomerList.aspx?edit=" &
                                                                                           payersysid.ToString()
                    End If

                    '
                    '������� ���������� � �������� ��� ���
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
                    '"<br>���: " & e.Item.DataItem("unn") & _
                    '"<br>��� �: " & e.Item.DataItem("dogovor")
                    'End If
                    'If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                    '    s = e.Item.DataItem("payerInfo")
                    '
                    '    CType(e.Item.FindControl("lblCustDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    '    If e.Item.ItemIndex = 0 Then
                    '        CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s
                    '        CType(e.Item.FindControl("lblGoodOwner"), Label).ToolTip = "��������: " & e.Item.DataItem("ownerInfo")
                    '        CType(e.Item.FindControl("lblDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    '    Else
                    '        If CType(grdTO.Items(e.Item.ItemIndex - 1).FindControl("lblCustDogovor"), Label).Text <> CStr(e.Item.DataItem("payerdogovor")) Then
                    '            CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s
                    '            CType(e.Item.FindControl("lblGoodOwner"), Label).ToolTip = "��������: " & e.Item.DataItem("ownerInfo")
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
                    '��������
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
                                "� �������. �� ����� � ������� ��� " & i - 1 & " ���(�)"
                        Else
                            CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                                "� �������. �� ����� � ������� �� ���"
                        End If
                    End If

                    e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                    If e.Item.FindControl("imgRepaired").Visible Then
                        CType(e.Item.FindControl("imgRepaired"), WebControls.HyperLink).ToolTip = "��� � ������� " &
                                                                                                  CInt(
                                                                                                      e.Item.DataItem(
                                                                                                          "repaired")) &
                                                                                                  " ���(�)"
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
                    CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).Text = "��������"
                    CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).ToolTip = "��������"

                    If IsDBNull(e.Item.DataItem("state")) Then
                        e.Item.DataItem("state") = 0
                    End If

                    If Not IsDBNull(e.Item.DataItem("lastTO")) And e.Item.DataItem("state") <> 4 Then
                        If e.Item.DataItem("state") = 6 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "�� ��������������<br>" &
                                                                                  GetRussianDate(e.Item.DataItem("ldate")) &
                                                                                  " (�� " & e.Item.DataItem("period") &
                                                                                  " �������)<br>"
                        End If
                        If e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3 Then
                            CType(e.Item.FindControl("lblLastTO"), Label).Text &= "���� � ��<br>" &
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
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "��������� �� ��" & "<br>" &
                                                                             GetRussianDate(e.Item.DataItem("ldate")) &
                                                                             "<br>�� �� �����������"
                    Else
                        CType(e.Item.FindControl("lblLastTO"), Label).Text = "�� �� �����������"
                    End If

                    If (e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3) And show_state <> 2 Then
                        e.Item.Visible = False
                    End If

                    Dim periodFix = IIf(IsDBNull(e.Item.DataItem("period")), 1, e.Item.DataItem("period"))
                    If e.Item.DataItem("state") = 6 Then
                        Dim start_month = Month(e.Item.DataItem("ldate"))
                        Dim end_month = start_month + periodFix

                        If (show_state <> 6) Then 'end_month >= Month(Now()) And Or end_month > 12
                            'e.Item.Visible = False
                        End If
                    End If
                    '
                    '���� ��������� ��
                    '

                    If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                        If _
                            Month(e.Item.DataItem("lastTO")) = Month(Now()) And
                            Year(e.Item.DataItem("lastTO")) = Year(Now()) Then
                            e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                        End If
                    End If

                    'CType(e.Item.FindControl("lblLastTO"), Label).Text &= e.Item.DataItem("state")

                    Dim dateEndDelayTo As DateTime = Now()
                    If Not IsDBNull(e.Item.DataItem("start_date"))
                        dateEndDelayTo =
                            Date.Parse(e.Item.DataItem("start_date").ToString).AddMonths(
                                Convert.ToInt32(periodFix))
                    End If

                    If e.Item.DataItem("state") = 2 Or e.Item.DataItem("state") = 3 Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 100, 100)
                    ElseIf e.Item.DataItem("state") = 4 Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 250, 250)
                    ElseIf e.Item.DataItem("state") = 6 And dateEndDelayTo > Now() Then
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
                    {" ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ", " ��� ",
                     " ��� "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "�."
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
                'MsgBox("�� �� ������ �������� ������ ������ � �������", MsgBoxStyle.Information, "����������, ������� ����������� ������ � ��������")
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

            '���������� ���� good_sys_id
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

            filter = " where good.num_control_cto like '��%' "
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
            '������ �� ���������� �� �� ������������ �����
            '���(�������)
            Dim d_to = DateTime.Parse(lstMonth.SelectedValue & "." & "01" & "." & lstYear.SelectedValue)
            'Dim d_to = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)

            If to_made = 1 Then
                'filter &= " and good.good_sys_id IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id)"
                '��� �������
                filter &=
                    " and good.good_sys_id IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='" &
                    lstMonth.SelectedValue & "/01/" & lstYear.SelectedValue &
                    "' and cash_history.good_sys_id=good.good_sys_id)"
            End If
            If to_made = 2 Then
                'filter &= " and good.good_sys_id NOT IN (SELECT top 1 cash_history.good_sys_id FROM cash_history WHERE cash_history.state=4 and cash_history.good_sys_id=good.good_sys_id AND cash_history.change_state_date>'01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "') and good.good_sys_id NOT IN (SELECT cash_history.good_sys_id FROM cash_history WHERE cash_history.start_date='01/" & lstMonth.SelectedValue & "/" & lstYear.SelectedValue & "' and cash_history.good_sys_id=good.good_sys_id) "
                '��� �������
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

            '���(�������)
            Dim d As DateTime = DateTime.Parse("01" & "." & lstMonth.SelectedValue & "." & lstYear.SelectedValue)
            Dim listOfIndexOfSelectCheckBox As ArrayList = New ArrayList()

            For k = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(k).FindControl("cbxSelect"), WebControls.CheckBox).Checked Then
                    listOfIndexOfSelectCheckBox.Add(k)
                End If
            Next

            If listOfIndexOfSelectCheckBox.Count <> 0 Then

                '��������� ������������ ��������� ������
                If Not _serviceTo.CheckDate(d, tbxCloseDate.Text) Then
                    msgCashregister.Text = _serviceTo.GetTextStringAllExeption()
                    Exit Sub
                End If

                closedate = DateTime.Parse(tbxCloseDate.Text)

                Dim kkmDs As DataSet = CType(Session("KKM_ds"), DataSet)
                For Each index As Integer In listOfIndexOfSelectCheckBox
                    If _
                        _serviceTo.CheckCashHistoryItem(Integer.Parse(grdTO.DataKeys.Item(index).ToString()), d,
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
                If _serviceTo.HaveAnyExeption() Then
                    filter = "good_sys_id in (" & String.Join(", ", _serviceTo.GetListStringGoodSysId()) & ")"
                End If


                dv.RowFilter = filter
                grdError.DataSource = dv
                grdError.DataKeyField = "good_sys_id"
                grdError.DataBind()

                If _serviceTo.HaveAnyExeption() Then
                    myModal.Style.Add("display", "block")
                End If
            Else
                msgCashregister.Text = "�������� �������� �������, �������� ������ �������� ��"
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
                    MsgBox("��������� ������ ������ ��������", MsgBoxStyle.Critical, "������")
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
                    _serviceTo.GetExeptionTextByGoodId(CInt(CType(e.Item.FindControl("lblGood"), Label).Text))
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

            bind(Session("Filter"))
        End Sub

        Protected Sub lnkExportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnkExportData.Click
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

            filter = " where num_control_cto like '��%' AND employee_cto is not null AND confirmed is null"
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
                " where num_control_cto like '��%' AND employee_cto is not null AND confirmed is null and lastTO < '" &
                d1 & "'"
            Session("filter") = filter
            bind(filter)
        End Sub

        Protected Sub lnkRaspl_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRaspl.Click
            Dim filter
            lnkSetEmployee.Visible = True
            grdTO.Columns(10).Visible = True

            filter = " where [dbo].[good].[num_control_cto] like '��%' AND [dbo].[good].[employee_cto] is not null"

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

        Protected Sub chk_show_kkm_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles chk_show_kkm.CheckedChanged
            lstGoodType.Items.Clear()
            If chk_show_kkm.Checked = True And chk_show_torg.Checked = False Then
                grdTO.Visible = True
                grdTO_prod.Visible = False
                LoadGoodByType(1)
            ElseIf chk_show_kkm.Checked = False And chk_show_torg.Checked = True Then
                grdTO.Visible = False
                grdTO_prod.Visible = True
                LoadGoodByType(2)
            ElseIf chk_show_kkm.Checked = True And chk_show_torg.Checked = True Then
                grdTO.Visible = True
                grdTO_prod.Visible = True
                LoadGoodByType(3)
            End If
        End Sub

        Protected Sub chk_show_torg_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles chk_show_torg.CheckedChanged
            lstGoodType.Items.Clear()
            If chk_show_kkm.Checked = True And chk_show_torg.Checked = False Then
                grdTO.Visible = True
                grdTO_prod.Visible = False
                LoadGoodByType(1)
            ElseIf chk_show_kkm.Checked = False And chk_show_torg.Checked = True Then
                grdTO.Visible = False
                grdTO_prod.Visible = True
                LoadGoodByType(2)
            ElseIf chk_show_kkm.Checked = True And chk_show_torg.Checked = True Then
                grdTO.Visible = True
                grdTO_prod.Visible = True
                LoadGoodByType(3)
            End If
        End Sub

        Protected Sub grdTO_prod_ItemDataBound(ByVal sender As Object,
                                               ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdTO_prod.ItemDataBound
            Dim s$ = ""

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s & "; ������� �" &
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
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "�� �� �����������"
                End If
                '
                '��������
                '
                If Not IsDBNull(e.Item.DataItem("alert")) AndAlso e.Item.DataItem("alert") = 1 Then
                    e.Item.FindControl("imgAlert").Visible = True
                    CType(e.Item.FindControl("imgAlert"), HyperLink).ToolTip =
                        IIf(IsDBNull(e.Item.DataItem("info")), "", e.Item.DataItem("info")).ToString()
                Else
                    e.Item.FindControl("imgAlert").Visible = False
                End If

                e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso
                                                           e.Item.DataItem("support") = "1"

                Dim b As Boolean = e.Item.DataItem("repair")
                e.Item.FindControl("imgRepair").Visible = b

                If b Then
                    Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                    If i > 1 Then
                        CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                            "� �������. �� ����� � ������� ��� " & i - 1 & " ���(�)"
                    Else
                        CType(e.Item.FindControl("imgRepair"), WebControls.HyperLink).ToolTip =
                            "� �������. �� ����� � ������� �� ���"
                    End If
                End If

                e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                If e.Item.FindControl("imgRepaired").Visible Then
                    CType(e.Item.FindControl("imgRepaired"), WebControls.HyperLink).ToolTip = "��� � ������� " &
                                                                                              CInt(
                                                                                                  e.Item.DataItem(
                                                                                                      "repaired")) &
                                                                                              " ���(�)"
                End If
                '
                'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                '   s = e.Item.DataItem("stateTO")
                'End If
                '
                CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).Text = "��������"
                CType(e.Item.FindControl("lnkStatus"), WebControls.HyperLink).ToolTip = "��������"
                '
                '���� ��������� ��
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

            '���������� ���� good_sys_id
            bind(Session("Filter"))
        End Sub

        Private Sub AktForTOandDolg(withDate As Boolean)
            Dim checkGoods As ListDictionary
            checkGoods = FindCheckGoods()
            If checkGoods.Count > 0 Then
                _serviceDoc.AktForTOandDolg(checkGoods, Response, withDate, DateTime.Parse(tbxCloseDate.Text))
            End If
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

        Protected Sub lnkSendSms_Click(sender As Object, e As EventArgs)
            Dim smsModels As List(Of SmsModel) = New List(Of SmsModel)
            Dim selectedCustomerIds As List(Of String) = New List(Of String)()
            Dim adapt As SqlDataAdapter
            Dim ds As DataSet = New DataSet()
            Dim phoneNotice As String
            Dim dolg As String
            Dim row As DataRow
            Dim countPhonesNotFound As Integer = 0
            Const smsType = 7

            Try
                For k = 0 To grdTO.Items.Count - 1
                    If CType(grdTO.Items(k).FindControl("cbxSelect"), CheckBox).Checked Then
                        selectedCustomerIds.Add(CType(grdTO.Items(k).FindControl("lblPayerId"), Label).Text)
                    End If
                Next
                selectedCustomerIds = selectedCustomerIds.Distinct().ToList()
                If selectedCustomerIds.Count = 0
                    msgCashregister.Text = "�������� ���� ������ ��������� ���."
                    Exit Sub
                End If
                For Each selectedCustomerId As String In selectedCustomerIds
                    ds = New DataSet()
                    adapt =
                        dbSQL.GetDataAdapter(
                            "SELECT ISNULL(phone_notice,'') AS phone_notice, dolg FROM customer WHERE customer_sys_id = " &
                            selectedCustomerId)
                    adapt.Fill(ds)
                    If ds.Tables.Count > 0
                        If ds.Tables(0).Rows.Count > 0
                            row = ds.Tables(0).Rows(0)
                            phoneNotice = row.Item("phone_notice").ToString()
                            dolg = row.Item("dolg").ToString()
                            If Not String.IsNullOrEmpty(phoneNotice.Trim())
                                smsModels.Add(New SmsModel(phoneNotice, String.Format(textSmsTemplate, dolg),
                                                           CurrentUser.sys_id,
                                                           smsType, customerId := Convert.ToInt32(selectedCustomerId)))
                            Else
                                countPhonesNotFound += 1
                            End If
                        End If
                    End If
                Next
                If smsModels.Count > 0
                    _serviceSms.SendManySmsWithInsertSmsHistory(smsModels.ToArray())
                End If
                ShowSmsSendModal(selectedCustomerIds.Count, smsModels.Count, countPhonesNotFound)
            Catch ex As Exception
                msgCashregister.Text = "��������� ���� ��� ����� ������ ��� �������� ���. " & ex.Message
            End Try
        End Sub

        Private Sub ShowSmsSendModal(countSelectedSms As Integer, countSendSms As Integer,
                                     countPhonesNotFound As Integer)
            lblCountSelectedSms.Text = countSelectedSms.ToString()
            lblCountSendSms.Text = countSendSms.ToString()
            lblCountPhonesNotFound.Text = countPhonesNotFound.ToString()
            smsSendModal.Style.Add("display", "block")
        End Sub
    End Class
End Namespace
