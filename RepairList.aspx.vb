Imports System.Globalization
Imports System.Threading


Namespace Kasbi

Partial Class RepairList
        Inherits PageBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents msg As System.Web.UI.WebControls.Label
    Protected WithEvents txtFindGoodSetPlace As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Dim iType%, i%, j%
    Const ClearString = "-------"
    Dim isFind As Boolean = False
    Const scTrue = "True"
    Const scFalse = "False"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            txtFindGoodNum.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('1');}")
            txtFindGoodManufacturer.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('2');}")
            txtFindGoodCTO.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('5');}")
            i = 0
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")

            If Not IsPostBack Then

                Try
                    If Request.Cookies("Ramok") Is Nothing Then
                        Dim c As HttpCookie = New HttpCookie("Ramok")
                        c.Values.Add("RepairDateIn", Now)
                        c.Values.Add("RepairDateOut", Now)
                        c.Values.Add("Good_Type", getGood_type_List())
                        c.Expires = DateTime.MaxValue
                        Response.AppendCookie(c)
                    End If
                    setGood_type_List(Request.Cookies("Ramok")("Good_Type"))
                    tbxBeginDate.Text = DateTime.Parse(Request.Cookies("Ramok")("RepairDateIn")).ToShortDateString()
                    tbxEndDate.Text = DateTime.Parse(Request.Cookies("Ramok")("RepairDateOut")).ToShortDateString()
                Catch
                    tbxBeginDate.Text = Now.ToShortDateString()
                    tbxEndDate.Text = Now.ToShortDateString()
                End Try
            End If

            If Not IsPostBack Then
                LoadCashTypeList()
                BindRepair()
            End If
        End Sub

        Sub LoadCashTypeList()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("get_goods_is_cashregister", True)
                ds = New DataSet
                adapt.Fill(ds)

                lstCashType.DataSource = ds
                lstCashType.DataTextField = "name"
                lstCashType.DataValueField = "good_type_sys_id"
                lstCashType.DataBind()

                For j = 0 To lstCashType.Items.Count - 1
                    lstCashType.Items(j).Selected = True
                Next
            Catch
                msgCashregister.Text = "Ошибка формирования списка типов товаров!<br>" & Err.Description
                Exit Sub
            End Try
            'lstCashType_SelectedIndexChanged(Me, Nothing)
        End Sub

        Public Function getGood_type_List() As String
            Dim good_type$ = ""
            For j = 0 To lstCashType.Items.Count - 1
                If lstCashType.Items(j).Selected = True Then
                    good_type = good_type & lstCashType.Items(j).Value & ","
                End If
            Next
            If good_type.Length > 0 Then
                good_type = good_type.Substring(0, good_type.Length - 1)
            End If
            getGood_type_List = good_type
        End Function

        Public Function getGood_type_Name_List() As String
            Dim good_type$ = ""
            For j = 0 To lstCashType.Items.Count - 1
                If lstCashType.Items(j).Selected = True Then
                    good_type = good_type & lstCashType.Items(j).Text & ","
                End If
            Next
            If good_type.Length > 0 Then
                good_type = good_type.Substring(0, good_type.Length - 1)
            End If
            getGood_type_Name_List = good_type
        End Function

        Public Sub setGood_type_List(ByVal value As String)
            Dim ch() As Char = (",")
            Dim s() As String
            If value = Nothing Or lstCashType.Items.Count = 0 Then Exit Sub
            s = value.Split(ch)
            For j = 0 To s.Length - 1
                lstCashType.Items.FindByValue(s(j)).Selected = True
            Next
        End Sub

        Sub BindRepair(Optional ByVal sCaption$ = "")
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet


            Try
                Dim s$ = Session("FilterRepair")
                Dim sCaptionDate$ = ""
                Dim sCaptionType$ = ""

                cmd = New SqlClient.SqlCommand("get_cash_repairlist")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                Dim good_types$ = getGood_type_List()
                cmd.Parameters.AddWithValue("@pi_good_type", IIf(good_types = "", DBNull.Value, good_types))

                'обрабатываем только фильтр
                If Session("FilterRepair") Is Nothing OrElse Session("FilterRepair") = "" Then
                    Exit Sub
                    'cmd.Parameters.Add("@pi_filter", "")
                Else
                    cmd.Parameters.AddWithValue("@pi_filter", Session("FilterRepair"))
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If ds.Tables.Count > 0 Then
                    If ViewState("goodsort") = "" Then
                        ds.Tables(0).DefaultView.Sort = "payerdogovor,num_cashregister DESC,good_type_sys_id ASC "
                        ViewState("goodsort") = "payerdogovor,num_cashregister DESC"
                    Else
                        ds.Tables(0).DefaultView.Sort = ViewState("goodsort") & ", good_type_sys_id ASC "
                    End If
                    grdRepair.DataSource = ds.Tables(0).DefaultView
                    grdRepair.DataKeyField = "good_sys_id"
                    grdRepair.DataBind()

                    If ds.Tables(0).DefaultView.Sort = ViewState("goodsort") Then
                        Dim arr_parce() As String
                        arr_parce = Split(ds.Tables(0).DefaultView.Sort, ";")
                        If arr_parce(1) <> "end_sub" Then
                            Dim query As QueryStringParameter
                            query.QueryStringField = "SELECT * FROM sock_user WHERE id_user='2' ORDER BY good_sys_id"
                        End If
                    End If

                    'If sCaption.Length > 0 Then
                    '    'Показываем какие кассы выбраны в фильтре
                    '    If Not lstCashType.Items.Item(0).Selected And Not lstCashType.Items.Item(1).Selected Then
                    '        sCaption = "Не задан критерий поиска. Выберите Касби 03Ф или Касби 03ФТ"
                    '    Else
                    '        sCaption = "Показаны " & sCaption
                    '        If lstCashType.Items.Item(0).Selected Then
                    '            sCaptionType = " Касби 03Ф"
                    '        End If
                    '        If lstCashType.Items.Item(1).Selected Then
                    '            If sCaptionType.Length > 0 Then sCaptionType = sCaptionType & " и"
                    '            sCaptionType = sCaptionType & " Касби 03ФТ"
                    '        End If

                    '    End If
                    'End If
                    Dim recordCount As String
                    recordCount = ds.Tables(0).DefaultView.Count().ToString()
                    lblRecordCount.Text = "  Найдено - " & recordCount & " записей "
                End If
                sCaption = sCaption & sCaptionType

                lblFilterCaption.Text = sCaption
            Catch
                msgCashregister.Text = "Ошибка загрузки информации о ТО!<br>" & Err.Description
                msgCashregister.Visible = True
            End Try
        End Sub

        Private Sub grdRepair_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRepair.ItemDataBound

            Dim s$ = ""
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s

                    CType(e.Item.FindControl("lblDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                    CType(e.Item.FindControl("lblCustDogovor"), Label).Text = e.Item.DataItem("payerdogovor")
                End If
                'End If

                i = i + 1
                s = ""

                CType(e.Item.FindControl("lblNumGood"), Label).Text = i
                s = ""
                If Not IsDBNull(e.Item.DataItem("dolg")) Then
                    s = s & e.Item.DataItem("dolg")
                End If
                CType(e.Item.FindControl("lblDolg"), Label).Text = s
                'даты ремонта
                s = Format(e.Item.DataItem("repairdate_in"), "dd.MM.yyyy HH:mm") & " / "
                If IsDBNull(e.Item.DataItem("repairdate_out")) Then
                    s = s & "??.??.????"
                    e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                Else
                    s = s & Format(e.Item.DataItem("repairdate_out"), "dd.MM.yyyy")
                End If
                CType(e.Item.FindControl("lblRepairDates"), Label).Text = s & "<br>" & e.Item.DataItem("executor")
                'картинки
                s = CStr(e.Item.DataItem("alert"))
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
                s = ""

                If IsDBNull(e.Item.DataItem("support")) Or e.Item.DataItem("support") = 0 Then
                    s = "Не заключен договор на ТО"
                Else
                    If e.Item.DataItem("support") = "1" Then
                        If e.Item.DataItem("stateTO") = "0" Or e.Item.DataItem("stateTO") = "1" Or e.Item.DataItem("stateTO") = "4" Then
                            s = "Находится на ТО"

                        ElseIf e.Item.DataItem("stateTO") = "6" Then
                            s = "ТО приостановлено"

                        ElseIf e.Item.DataItem("stateTO") = "5" Then
                            s = "Находится в ремонте"
                        End If
                    Else
                        If e.Item.DataItem("stateTO") = "2" Then
                            s = "Снят с ТО"
                        ElseIf e.Item.DataItem("stateTO") = "3" Then
                            s = "Снят с ТО и в ИМНС"
                        End If
                    End If

                End If

                'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                '    s = e.Item.DataItem("stateTO")
                'End If
                CType(e.Item.FindControl("lnkStatus"), HyperLink).Text = s
                CType(e.Item.FindControl("lnkStatus"), HyperLink).ToolTip = "Просмотр"

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = GetRussianDate(e.Item.DataItem("lastTO")) + "<br>" + e.Item.DataItem("lastTOMaster")
                    e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                    If IsDBNull(e.Item.DataItem("repairdate_out")) Then
                        e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)
                    End If
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "ТО не проводилось"

                End If
            End If
        End Sub

        Private Sub grdRepair_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdRepair.SortCommand
            If ViewState("goodsort") = e.SortExpression Then
                ViewState("goodsort") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort") = e.SortExpression
            End If
            BindRepair()
        End Sub

        Private Sub btnFindGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindGood.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "g.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(g.num_control_reestr like '%" & str & "%' or g.num_control_pzu like '%" & str & "%' or g.num_control_mfp like '%" & str & "%' or g.num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(g.num_control_cto like '%" & str & "%' or g.num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            If sFilter.Length > 0 Then
                Session("FilterRepair") = sFilter
                sCaption = sCaption & " ККМ ( " & getGood_type_Name_List() & " )  "
                isFind = True
                BindRepair("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If

        End Sub

        Private Sub txtFindGoodNum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodNum.TextChanged
            If Request.Form("FindHidden") = "1" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub txtFindGoodManufacturer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodManufacturer.TextChanged
            If Request.Form("FindHidden") = "2" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub txtFindGoodCTO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodCTO.TextChanged
            If Request.Form("FindHidden") = "5" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub lnkRepairs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRepairs.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "g.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_reestr like '%" & str & "%' or vIC.num_control_pzu like '%" & str & "%' or vIC.num_control_mfp like '%" & str & "%' or vIC.num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_cto like '%" & str & "%' or vIC.num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            str = ""

            Dim d1 As DateTime = New DateTime
            Dim d2 As DateTime = New DateTime

            msgCashregister.Visible = False
            Try
                d1 = DateTime.Parse(tbxBeginDate.Text)
                d2 = DateTime.Parse(tbxEndDate.Text)
                If (d1 > d2) Then
                    msgCashregister.Text = "Конечная дата должна быть меньше начальной"
                    msgCashregister.Visible = True
                End If
            Catch
            End Try

            If (msgCashregister.Visible) Then Exit Sub

            Dim convDate$ = "dateadd(year, datepart(year, '" & Format(d2, "MM/dd/yyy") & "') - 1900, dateadd(month, datepart(month, '" & Format(d2, "MM/dd/yyy") & "') -1,dateadd(day, datepart(day, '" & Format(d2, "MM/dd/yyy") & "')-1,dateadd(hour, datepart(hour, '" & Format(d2, "MM/dd/yyy") & "')-1,dateadd(minute, datepart(minute, '" & Format(d2, "MM/dd/yyy") & "'),dateadd(second, datepart(second, '" & Format(d2, "MM/dd/yyy") & "'),1))))))"
            If sFilter.Length > 0 Then
                ' sFilter = sFilter & " and hc.repairdate_in between '" & Format(d1, "MM/dd/yyy") & "' and '" & Format(d1, "MM/dd/yyy") & "' "
                'sFilter = sFilter & " and hc.repairdate_in >= '" & Format(d1, "MM/dd/yyy") & "' and hc.repairdate_in <='" & Format(d2, "MM/dd/yyy") & "' "
                sFilter = sFilter & " and hc.repairdate_in >= '" & Format(d1, "MM/dd/yyy") & "' and hc.repairdate_in <=" & convDate
            Else
                sFilter = sFilter & " hc.repairdate_in >= '" & Format(d1, "MM/dd/yyy") & "' and hc.repairdate_in <=" & convDate
            End If

            sCaption = sCaption & " Ремонт за период с " & GetRussianDate1(d1) & " по " & GetRussianDate1(d2) & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")

            c.Item("Good_Type") = getGood_type_List()
            c.Item("RepairDateIn") = d1
            c.Item("RepairDateOut") = d2
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterRepair") = sFilter
                isFind = True
                BindRepair("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If

        End Sub

        Private Sub lnkGarantia_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkGarantia.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "g.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_reestr like '%" & str & "%' or vIC.num_control_pzu like '%" & str & "%' or vIC.num_control_mfp like '%" & str & "%' or vIC.num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_cto like '%" & str & "%' or vIC.num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            str = ""
            If sFilter.Length > 0 Then
                '  sFilter = sFilter & " and hc.repairdate_in between dateadd(m, -18, getdate()) and getdate()"
                sFilter = sFilter & " and hc.garantia =1"
            Else
                sFilter = sFilter & " hc.garantia =1"
            End If

            sCaption = sCaption & " Ремонт за гарантийный период (18 мес) ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")

            c.Item("Good_Type") = getGood_type_List()
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterRepair") = sFilter
                isFind = True
                BindRepair("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If

        End Sub

        Private Sub lnkAllRepaired_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAllRepaired.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "g.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_reestr like '%" & str & "%' or vIC.num_control_pzu like '%" & str & "%' or vIC.num_control_mfp like '%" & str & "%' or vIC.num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_cto like '%" & str & "%' or vIC.num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            str = ""


            ' If (msgCashregister.Visible) Then Exit Sub

            If sFilter.Length > 0 Then
                sFilter = sFilter & " and g.good_sys_id in(SELECT DISTINCT (good_sys_id) FROM cash_history where repairdate_in is not null) "
            Else
                sFilter = sFilter & " g.good_sys_id in(SELECT DISTINCT (good_sys_id) FROM cash_history where repairdate_in is not null) "
            End If

            sCaption = sCaption & " Ремонт за весь период эксплуатации ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterRepair") = sFilter
                isFind = True
                BindRepair("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If

        End Sub

        Private Sub lnkAllRepair_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAllRepair.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "g.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_reestr like '%" & str & "%' or vIC.num_control_pzu like '%" & str & "%' or vIC.num_control_mfp like '%" & str & "%' or vIC.num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(vIC.num_control_cto like '%" & str & "%' or vIC.num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            str = ""
            Dim d1 As DateTime = New DateTime
            Dim d2 As DateTime = New DateTime

            If sFilter.Length > 0 Then
                sFilter = sFilter & " and hc.state=5 and hc.repairdate_out is null "
            Else
                sFilter = sFilter & " hc.state=5 and hc.repairdate_out is null "
            End If

            sCaption = sCaption & " ККМ ( " & getGood_type_Name_List() & " ) находящиеся в ремонте  "
            Dim c As HttpCookie = Request.Cookies("Ramok")

            c.Item("Good_Type") = getGood_type_List()
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterRepair") = sFilter
                isFind = True
                BindRepair("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If

        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDate1(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDate1 = " « " & Day(d) & " » " & m(Month(d) - 1) & Year(d) & "г."
        End Function

    End Class

End Namespace
