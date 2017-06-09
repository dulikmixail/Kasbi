Imports Microsoft.Office.Interop

Namespace Kasbi

    Partial Class InternalCTO
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

        Dim iType%, i%, j%
        Const ClearString = "-------"
        Dim isFind As Boolean = False
        Const scTrue = "True"
        Const scFalse = "False"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Ограничение прав
            'Dim rules() As String
            'rules = Split(Session("rules"), ";")
            'If rules(16) = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)

            grdTO.Columns(0).Visible = False
            lnkRouteRequest.Visible = False

            i = 0
            If Not IsPostBack Then
                Try
                    If Request.Cookies("Ramok") Is Nothing Then
                        Dim c As HttpCookie = New HttpCookie("Ramok")
                        c.Values.Add("Good_Type", getGood_type_List())
                        c.Values.Add("ConductDate", Now)
                        c.Expires = DateTime.MaxValue
                        Response.AppendCookie(c)
                    End If

                    setGood_type_List(Request.Cookies("Ramok")("Good_Type"))

                    Dim d As Date = Request.Cookies("Ramok")("ConductDate")
                    d.AddMonths(1)
                    lstMonth.SelectedIndex = d.Month - 1
                    If d.Year > 2002 And d.Year < 2014 Then
                        lstYear.SelectedIndex = d.Year - 2003
                    Else
                        lstYear.SelectedIndex = 0
                    End If
                Catch
                    Dim d As Date = Now
                    d.AddMonths(1)
                    lstMonth.SelectedIndex = d.Month - 1
                    If d.Year > 2002 And d.Year < 2014 Then
                        lstYear.SelectedIndex = d.Year - 2003
                    Else
                        lstYear.SelectedIndex = 0
                    End If
                End Try
            End If

            If Not IsPostBack Then
                LoadCashTypeList()
                LoadPlaceRegion()
                'BindTO()
            End If

            txtFindGoodNum.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('1');}")
            txtFindGoodManufacturer.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('2');}")
            'txtFindGoodReestr.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('2');}")
            'txtFindGoodPZU.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('3');}")
            'txtFindGoodMFP.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('4');}")
            txtFindGoodCTO.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('5');}")
            txtFindGoodSetPlace.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('6');}")
            txtFindGoodSetPlace.Attributes.Add("onselectedindexchanged", "javascript:isFind('7');")
            txtFindCustomer.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('8');}")
            ' lnkExportData.Attributes.Add("onclick", "javascript:ExportToExcel(); ") 'window.open('InternalCTO.aspx?bExcel=1')")
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
                Dim i%
                For i = 0 To lstCashType.Items.Count - 1
                    lstCashType.Items(i).Selected = True
                Next
            Catch
                msgCashregister.Text = "Ошибка формирования списка типов товаров!<br>" & Err.Description
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
                msgCashregister.Text = "Ошибка формирования списка районов установки!<br>" & Err.Description
                Exit Sub
            End Try
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

        Function GetTOStatistic() As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s As String
            Try
                Dim d1, d2 As Date
                d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
                d2 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, Date.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value), 23, 59, 59)

                'd1 = lstYear.SelectedItem.Value & "-" & lstMonth.SelectedItem.Value & "-1"
                'd2 = lstYear.SelectedItem.Value & "-" & Date.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value) & "-1"

                'd1 = Format(d1, "MM/dd/yyyy")
                'd2 = Format(d2, "MM/dd/yyyy")

                s = ""
                cmd = New SqlClient.SqlCommand("get_cash_TO_RecordCount")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@d1", d1)
                cmd.Parameters.AddWithValue("@d2", d2)
                cmd.Parameters.AddWithValue("@good_type", getGood_type_List())
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).DefaultView.Count <> 0 Then
                    s = "Всего на ТО - " & ds.Tables(0).Rows(0).Item("all_cash_TO").ToString & " : "
                    s = s & " ТО проведено - " & ds.Tables(0).Rows(0).Item("all_conduct_TO").ToString & " : "
                    s = s & " ТО не проведено - " & ds.Tables(0).Rows(0).Item("all_notconduct_TO").ToString & " : "
                    s = s & " ТО приостановлено - " & ds.Tables(0).Rows(0).Item("all_delay_TO").ToString & " : "
                    s = s & " Поставлены на ТО - " & ds.Tables(0).Rows(0).Item("all_support_TO").ToString & " : "
                    s = s & " Сняты с ТО - " & ds.Tables(0).Rows(0).Item("all_dismissal_TO").ToString
                End If
            Catch
                If Err.Description.Substring(0, 15) <> "Timeout expired" Then
                    msgCashregister.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
                End If
                Return ""
            End Try
            GetTOStatistic = s
        End Function

        Sub BindTO(Optional ByVal sCaption$ = "")
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                Dim s$ = Session("FilterTO")
                Dim sCaptionDate$ = ""
                Dim sCaptionType$ = ""

                Dim d1 As Date
                d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)

                cmd = New SqlClient.SqlCommand("get_internalCTO_cashRegister")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 60
                Dim good_types$ = getGood_type_List()
                cmd.Parameters.AddWithValue("@pi_good_type", IIf(good_types = "", DBNull.Value, good_types))

                cmd.Parameters.AddWithValue("@d1", d1)

                'обрабатываем только фильтр
                If Session("FilterTO") Is Nothing OrElse Session("FilterTO") = "" Then
                    lnkSelectKKM.Visible = False
                    Exit Sub
                Else
                    cmd.Parameters.AddWithValue("@pi_filter", Session("FilterTO"))
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If ds.Tables.Count > 0 Then
                    If ViewState("goodsort") = "" Then
                        ds.Tables(0).DefaultView.Sort = "payerdogovor,num_cashregister DESC,good_type_sys_id ASC "
                        ViewState("goodsort") = "payerdogovor,num_cashregister DESC"
                    Else
                        ds.Tables(0).DefaultView.Sort = ViewState("goodsort") & ",good_type_sys_id ASC "
                    End If
                    grdTO.DataSource = ds.Tables(0).DefaultView
                    grdTO.DataKeyField = "good_sys_id"
                    grdTO.DataBind()
                    Session("KKM_ds") = ds

                    Dim recordCount% = ds.Tables(0).DefaultView.Count()
                    If recordCount > 0 Then
                        lnkSelectKKM.Visible = True
                    Else
                        lnkSelectKKM.Visible = False
                    End If
                    lblRecordCount.Text = GetTOStatistic() '& " : найдено - " & recordCount
                End If

                If grdTO.Columns(0).Visible = True Then
                    lnkExportData.Visible = True
                Else
                    lnkExportData.Visible = False
                End If

                sCaption = sCaption & sCaptionDate & sCaptionType
                lblFilterCaption.Text = sCaption
            Catch
                msgCashregister.Text = "Ошибка загрузки информации о ТО!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grdTO_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdTO.ItemDataBound
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
                'If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                '    s = e.Item.DataItem("payerInfo")

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

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i
                s = ""
                If Not IsDBNull(e.Item.DataItem("dolg")) Then
                    s = s & e.Item.DataItem("dolg")
                End If
                CType(e.Item.FindControl("lblDolg"), Label).Text = s
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
                If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                    s = e.Item.DataItem("stateTO")
                End If
                CType(e.Item.FindControl("lnkStatus"), HyperLink).Text = s
                CType(e.Item.FindControl("lnkStatus"), HyperLink).ToolTip = "Просмотр"

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = GetRussianDate(e.Item.DataItem("lastTO")) + "<br>" + e.Item.DataItem("lastTOMaster")
                    e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                Else
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "ТО не проводилось"
                End If
                If IsDBNull(e.Item.DataItem("place_rn_id")) Then
                    CType(e.Item.FindControl("lblPlaceRegion"), Label).Visible = False
                End If
            End If
        End Sub

        Private Sub grdTO_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdTO.SortCommand
            If ViewState("goodsort") = e.SortExpression Then
                ViewState("goodsort") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort") = e.SortExpression
            End If
            BindTO()
        End Sub

        Private Sub btnFindGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFindGood.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
                sCaption = "номер ККМ: '" & str & "'; "
            End If

            Dim get_type = txtFindGoodNum.Text.Trim
            If get_type > txtFindGoodNum.Text.Trim Then
                sFilter = "SELECT * FROM sock_user WHERE customer_sys_id='id'"
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

            'str = txtFindGoodPZU.Text.Trim
            'If str.Length > 0 And str.IndexOf("'") = -1 Then
            '    If sFilter.Length > 0 Then sFilter = sFilter & " and "
            '    sFilter = sFilter & "vIC.num_control_pzu like '%" & str & "%'"
            '    sCaption = sCaption & "СК ПЗУ: '" & str & "'; "
            'End If

            'str = txtFindGoodMFP.Text.Trim
            'If str.Length > 0 And str.IndexOf("'") = -1 Then
            '    If sFilter.Length > 0 Then sFilter = sFilter & " and "
            '    sFilter = sFilter & "vIC.num_control_mfp like '%" & str & "%'"
            '    sCaption = sCaption & "СК МФП: '" & str & "'; "
            'End If

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If

                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If

            If sFilter.Length > 0 Then
                sFilter = sFilter & " and vIC.state<>5 and (vIC.sys_id in (select top 1 sys_id from cash_history " 'and substring(hc.marka_cto_out,1,2)='МН'
                sFilter = sFilter & " where(good_sys_id = vIC.good_sys_id and state<>5)order by change_state_date desc)or((vIC.support=0 or vIC.support is null) and vIC.good_sys_id not in (select good_sys_id from cash_history))) "
                Session("FilterTO") = sFilter
                sCaption = sCaption & " ККМ ( " & getGood_type_Name_List() & " ) "
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Private Sub txtFindGoodNum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodNum.TextChanged
            If Request.Form("FindHidden") = "1" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        Private Sub txtFindGoodManuacturer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodManufacturer.TextChanged
            If Request.Form("FindHidden") = "2" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        'Private Sub txtFindGoodReestr_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodReestr.TextChanged
        '    If Request.Form("FindHidden") = "2" Then
        '        btnFindGood_Click(sender, e)
        '    End If
        'End Sub

        'Private Sub txtFindGoodPZU_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodPZU.TextChanged
        '    If Request.Form("FindHidden") = "3" Then
        '        btnFindGood_Click(sender, e)
        '    End If
        'End Sub

        'Private Sub txtFindGoodMFP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodMFP.TextChanged
        '    If Request.Form("FindHidden") = "4" Then
        '        btnFindGood_Click(sender, e)
        '    End If
        'End Sub

        Private Sub txtFindGoodCTO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodCTO.TextChanged
            If Request.Form("FindHidden") = "5" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        Private Sub txtFindGoodSetPlace_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindGoodSetPlace.TextChanged
            If Request.Form("FindHidden") = "6" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        Private Sub lstPlaceRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstPlaceRegion.SelectedIndexChanged
            If Request.Form("FindHidden") = "7" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        Private Sub txtFindCustomer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFindCustomer.TextChanged
            If Request.Form("FindHidden") = "8" Then
                btnFindGood_Click(sender, e)
            End If
        End Sub

        Private Sub lnkConduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkConduct.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            '' поиск
            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
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

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items

                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If
                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If
            ' конец поиска

            str = ""
            Dim d As Date
            d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
            If sFilter.Length > 0 Then
                sFilter = sFilter & " and substring(vIC.num_control_cto,1,2)='МН' and vIC.state=1 and vIC.start_date='" & d & "' "
            Else
                sFilter = sFilter & " substring(vIC.num_control_cto,1,2)='МН' and vIC.state=1 and vIC.start_date='" & d & "' "
            End If

            sCaption = sCaption & " То за " & GetRussianDate(d) & " проведено для " & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Item("ConductDate") = d
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterTO") = sFilter
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Private Sub lnkNotConduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkNotConduct.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
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

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items

                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If
                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If

            str = ""
            Dim d As Date
            d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)

            If sFilter.Length > 0 Then
                'and  (substring(hc.marka_cto_out,1,2)='МН' and g.support = 1 and (hc.start_date is null and g.good_sys_id not in (select distinct good_sys_id from cash_history where state = 1 and start_date='2/1/2005')))
                sFilter = sFilter & " and vIC.state<>5 and substring(vIC.num_control_cto,1,2)='МН' and vIC.start_date is null and (vIC.good_sys_id not in (select DISTINCT vIC1.good_sys_id from view_Internal_CTO vIC1 where (vIC1.state = 4 AND vIC1.support_date >='" & d & "')or(vIC1.state = 4 AND vIC1.support_date >= vIC.dismissal_date) or(vIC1.state = 1 AND vIC1.start_date ='" & d & "')or(vIC1.state=6 and '" & d & "' >=vIC1.start_date and '" & d & "'<dateadd(m,vIC1.period,vIC1.start_date))or(vIC1.state in (2,3) and vIC1.dismissal_date >=vIC.support_date and vIC1.good_sys_id=vIC.good_sys_id))) "
            Else
                sFilter = sFilter & " vIC.state<>5 and substring(vIC.num_control_cto,1,2)='МН' and vIC.start_date is null and (vIC.good_sys_id not in (select DISTINCT vIC1.good_sys_id from view_Internal_CTO vIC1 where (vIC1.state = 4 AND vIC1.support_date >='" & d & "')or(vIC1.state = 4 AND vIC1.support_date >= vIC.dismissal_date) or(vIC1.state = 1 AND vIC1.start_date ='" & d & "')or(vIC1.state=6 and '" & d & "' >=vIC1.start_date and '" & d & "'<dateadd(m,vIC1.period,vIC1.start_date))or(vIC1.state in (2,3) and vIC1.dismissal_date >=vIC.support_date and vIC1.good_sys_id=vIC.good_sys_id))) "
            End If

            sCaption = sCaption & " То за " & GetRussianDate(d) & " не проведено для " & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Item("ConductDate") = d
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterTO") = sFilter
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Private Sub lnkSupport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkSupport.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
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

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items

                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If

                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If

            str = ""
            Dim d1, d2 As Date
            d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
            d2 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, d1.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value))
            If sFilter.Length > 0 Then
                sFilter = sFilter & " and substring(vIC.num_control_cto,1,2)='МН' and vIC.state=4 and vIC.support_date between '" & d1 & "' and dateadd(m,1,'" & d1 & "') "
            Else
                sFilter = sFilter & " substring(vIC.num_control_cto,1,2)='МН' and vIC.state=4 and vIC.support_date between '" & d1 & "' and dateadd(m,1,'" & d1 & "') "
            End If

            If sFilter.Length > 0 Then
                sFilter = sFilter & " and substring(vIC.num_control_cto,1,2)='МН' and vIC.state=4 and vIC.support_date between '07/07/2010' and '31/07/2010' "
            Else
                sFilter = sFilter & " substring(vIC.num_control_cto,1,2)='МН' and vIC.state=4 and vIC.support_date between '07/07/2010' and '31/07/2010' "
            End If

            sCaption = sCaption & " Поставлены на ТО в " & GetRussianDate(d1) & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Item("ConductDate") = d1
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterTO") = sFilter
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Private Sub lnkDismissal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDismissal.Click

            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
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

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If
                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If

            str = ""
            'Dim d1, d2 As Date
            'd1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
            'd2 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, Date.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value), 23, 59, 59)

            Dim d1, d2
            'd1 = lstMonth.SelectedItem.Value & "/01/" & lstYear.SelectedItem.Value
            'd2 = lstMonth.SelectedItem.Value & "/" & Date.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value) & "/" & lstYear.SelectedItem.Value
            d1 = "01/" & lstMonth.SelectedItem.Value & "/" & lstYear.SelectedItem.Value
            d2 = Date.DaysInMonth(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value) & "/" & lstMonth.SelectedItem.Value & "/" & lstYear.SelectedItem.Value

            If sFilter.Length > 0 Then
                sFilter = sFilter & " and ((vIC.state=2 or vIC.state=3) and vIC.dismissal_date between '" & d1 & "' and '" & d2 & "') "
            Else
                sFilter = sFilter & " ((vIC.state=2 or vIC.state=3) and vIC.dismissal_date between '" & d1 & "' and '" & d2 & "') "
            End If


            sCaption = sCaption & " Сняты с ТО в " & GetRussianDate(d1) & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Item("ConductDate") = d1
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)

            If sFilter.Length > 0 Then
                Session("FilterTO") = sFilter
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Private Sub lnkDelay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelay.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                sFilter = "vIC.num_cashregister like '%" & str & "%'"
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

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            str = ""
            Dim place_name$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Selected Then
                    If item.Value <> ClearString Then
                        str &= item.Value & ","
                        place_name &= item.Text & ","
                    End If

                End If
            Next item

            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.place_rn_id in (" & str.TrimEnd(",") & ")"
                sCaption = sCaption & "район установки ККМ: '" & place_name.TrimEnd(",") & "'; "
            End If


            Dim query = ""
            If query = "SELECT * FROM client_history WHERE customer_sys_id='rrg' WHERE good_sys_id='egrg'" Then
                query = "mysql_result['rgerg'];"
                Print(query)
            End If

            str = txtFindCustomer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = -1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "vIC.payerInfo like '%" & str & "%'"
                sCaption = sCaption & " клиенты: '" & str & "'; "
            End If

            str = ""
            Dim d1 As Date
            d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
            If sFilter.Length > 0 Then
                sFilter = sFilter & " and substring(vIC.num_control_cto,1,2)='МН' and vIC.state=6 and '" & d1 & "'>=vIC.start_date and '" & d1 & "'<dateadd(m,vIC.period,vIC.start_date) "
                'sFilter = sFilter & " and substring(vIC.num_control_cto,1,2)='МН' and vIC.state=6 and '" & d1 & "'>=vIC.start_date and vIC.start_date<dateadd(m,vIC.period,vIC.start_date) "
            Else
                sFilter = sFilter & " substring(vIC.num_control_cto,1,2)='МН' and vIC.state=6 and '" & d1 & "'>=vIC.start_date and '" & d1 & "'<dateadd(m,vIC.period,vIC.start_date) "
                'sFilter = sFilter & " substring(vIC.num_control_cto,1,2)='МН' and vIC.state=6 and '" & d1 & "'>=vIC.start_date and vIC.start_date<dateadd(m,vIC.period,vIC.start_date) "
            End If

            sCaption = sCaption & " То за " & GetRussianDate(d1) & " приостановлено для " & " ККМ ( " & getGood_type_Name_List() & " )  "
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("Good_Type") = getGood_type_List()
            c.Item("ConductDate") = d1
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)
            If sFilter.Length > 0 Then
                Session("FilterTO") = sFilter
                isFind = True
                BindTO("Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) & ")")
            End If
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Private Sub lnkRoute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRoute.Click
            grdTO.Columns(0).Visible = True
            lnkRouteRequest.Visible = True
            lnkNotConduct_Click(sender, e)
        End Sub

        Private Sub lnkRouteRegionRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRouteRegionRequest.Click
            lnkRouteRegionRequest.Visible = True

            Dim place_id$ = ""
            For Each item As ListItem In lstPlaceRegion.Items
                If item.Selected Then
                    If item.Value <> ClearString Then
                        place_id &= item.Value & ","
                    End If

                End If
            Next item
            If place_id.Length > 0 And place_id.IndexOf("'") = -1 Then
                Dim strRequest$ = "Documents.aspx?t=50&region=1&g_t={0}&date={1}&crs={2}"

                Dim d1 As Date
                d1 = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
                strRequest$ = String.Format(strRequest, getGood_type_List(), Format(d1, "MM/dd/yyyy"), place_id.TrimEnd(","))
                strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
                Me.RegisterStartupScript("report", strRequest)

                ' Page.RegisterStartupScript("Startup", "<script language=JavaScript>window.open('Documents.aspx?t=50&region=1&gt="& getGood_type_List()&"&date="& d1.ToString(& "&crs=" & lstPlaceRegion.SelectedValue.ToString() & "','_new','');</script>")
            Else
                msgCashregister.Text = "Выберите регион установки из списка"
            End If
        End Sub

        Private Sub lnkRouteRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRouteRequest.Click
            grdTO.Columns(0).Visible = True
            lnkRouteRequest.Visible = True
            Dim s As String = String.Empty
            For j = 0 To grdTO.Items.Count - 1
                If CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked Then
                    s &= grdTO.DataKeys(grdTO.Items(j).ItemIndex) & ","
                End If
            Next
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("insert_route_goodlist")
                cmd.Parameters.AddWithValue("@param_name", "route_good_list")
                cmd.Parameters.AddWithValue("@value_str", s)
                cmd.CommandType = CommandType.StoredProcedure
                dbSQL.Execute(cmd)
            Catch
                msgCashregister.Text = "Ошибка формирования списка касс для маршрутов!<br>" & Err.Description
                Exit Sub
            End Try
            If (s <> String.Empty) Then
                Page.RegisterStartupScript("Startup", "<script language=JavaScript>window.open('Documents.aspx?t=50&region=0&crs=route_good_list','_new','');</script>")
            End If
        End Sub

        Sub SelectAll(ByVal sender As Object, ByVal e As System.EventArgs)
            grdTO.Columns(0).Visible = True
            lnkRouteRequest.Visible = True

            Dim s As Boolean = CType(grdTO.Controls.Item(0).Controls.Item(0).FindControl("cbxSelectAll"), CheckBox).Checked

            For j = 0 To grdTO.Items.Count - 1
                CType(grdTO.Items(j).FindControl("cbxSelect"), CheckBox).Checked = s
            Next
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
            lnkExportData.Visible = False
        End Sub

        Protected Sub lnkSelectKKM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkSelectKKM.Click
            grdTO.Columns(0).Visible = True
            lnkExportData.Visible = True
            lnkRouteRequest.Visible = True
            BindTO()
        End Sub
    End Class

End Namespace
