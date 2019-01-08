Imports Service

Namespace Kasbi
    Partial Class GoodList
        Inherits PageBase

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

        Dim iType%, i%, j%
        Dim idDeliveryOld, idDeliveryNew, idGoodTypeOld, idGoodTypeNew As Integer
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink

        Const ClearString = "-------"
        Dim isFind As Boolean = False
        Const scTrue = "True"
        Const scFalse = "False"
        Private ReadOnly _serviceGood As ServiceGood = New ServiceGood()

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            txtFindGoodNum.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('1');}")
            txtFindGoodManufacturer.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('2');}")
            txtFindGoodCTO.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('5');}")
            txtFindGoodSetPlace.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind('6');}")
            i = 0

            'Ограничение прав
            If Session("rule13") = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)
            If Session("rule14") = "0" Then btnNew.Visible = False

            If Not IsPostBack Then
                Dim s1$ = lstUseDateType.ClientID
                Dim s2$ = ".disabled = !" & chkUseDate.ClientID & ".checked;"
                Dim s$ = "javascript:" & s1 & "_0" & s2 & s1 & "_1" & s2 & s1 & "_2" & s2 & cal.ClientID & s2

                chkUseDate.Attributes.Add("onkeypress", s)
                chkUseDate.Attributes.Add("onclick", s)
                Try
                    If Request.Cookies("Ramok") Is Nothing Then
                        Dim c As HttpCookie = New HttpCookie("Ramok")
                        c.Values.Add("GoodFilterPanel", scFalse)
                        c.Values.Add("ShowFreeGoods", scFalse)
                        c.Values.Add("ShowRequestedGoods", scFalse)
                        c.Values.Add("ShowSoldGoods", scTrue)
                        c.Values.Add("ShowOutSideGoods", scFalse)
                        c.Values.Add("Good_Type", getGood_type_List())
                        c.Values.Add("UseDate", scTrue)
                        c.Values.Add("UseDateType", "0")
                        c.Expires = DateTime.MaxValue
                        Response.AppendCookie(c)
                    End If

                    pnlFilter.Visible = Request.Cookies("Ramok")("GoodFilterPanel") = scTrue
                    chkFreeGoods.Checked = Request.Cookies("Ramok")("ShowFreeGoods") = scTrue
                    chkRequestedGoods.Checked = Request.Cookies("Ramok")("ShowRequestedGoods") = scTrue
                    chkSoldGoods.Checked = Request.Cookies("Ramok")("ShowSoldGoods") = scTrue
                    chkOutSideGoods.Checked = Request.Cookies("Ramok")("ShowOutSideGoods") = scTrue
                    setGood_type_List(Request.Cookies("Ramok")("Good_Type"))
                    chkUseDate.Checked = Request.Cookies("Ramok")("UseDate") = scTrue
                    lstUseDateType.Items(Request.Cookies("Ramok")("UseDateType")).Selected = True
                Catch
                End Try
            End If
            If Not IsPostBack Then
                cal.SelectedDate = Now
                cal.VisibleDate = Now
                LoadCashTypeList()
                'Bind()
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
                Dim i%
                For i = 0 To lstCashType.Items.Count - 1
                    lstCashType.Items(i).Selected = True
                Next
            Catch
                msgCashregister.Text = "Ошибка формирования списка типов товаров!<br>" & Err.Description
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

        Sub Bind(Optional ByVal sCaption$ = "")
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim numcashregister
            Dim findgoodcto
            Dim findgoodmanufacturer
            Dim action

            Try
                'Dim s$ = Session("FilterGood")
                Dim sCaptionDate$ = ""
                Dim sCaptionType$ = ""

                If txtFindGoodNum.Text = "" Then
                    numcashregister = CInt(IIf(Request.Params("numcashregister") <> "",
                                               Request.Params("numcashregister"), 0))
                    action = CInt(IIf(Request.Params("action") <> "", Request.Params("action"), 0))
                Else
                    numcashregister = txtFindGoodNum.Text
                End If

                If txtFindGoodCTO.Text = "" Then
                    findgoodcto = IIf(Request.Params("findgoodcto") <> "", Request.Params("findgoodcto"), 0)
                Else
                    findgoodcto = txtFindGoodCTO.Text
                End If
                If findgoodcto = 0 Then findgoodcto = ""

                If txtFindGoodManufacturer.Text = "" Then
                    findgoodmanufacturer = IIf(Request.Params("findgoodmanufacturer") <> "",
                                               Request.Params("findgoodmanufacturer"), 0)
                Else
                    findgoodmanufacturer = txtFindGoodManufacturer.Text
                End If
                If findgoodmanufacturer = 0 Then findgoodmanufacturer = ""

                If action = 1 Then
                    Dim sEmployee$ =
                            dbSQL.ExecuteScalar(
                                "select good_sys_id from good where num_cashregister='" & numcashregister & "'")
                    If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                    Else
                        Response.Redirect(GetAbsoluteUrl("~/Repair.aspx?" & sEmployee & "&action=1&param=" & sEmployee))
                    End If
                ElseIf action = 2 Then
                    ds = New DataSet
                    cmd =
                        New SqlClient.SqlCommand(
                            "SELECT TOP 1 dbo.cash_history.owner_sys_id, dbo.cash_history.sys_id, dbo.good.good_sys_id FROM dbo.good RIGHT OUTER JOIN dbo.cash_history ON dbo.good.good_sys_id = dbo.cash_history.good_sys_id WHERE (dbo.good.num_cashregister = '" &
                            numcashregister &
                            "') AND (dbo.cash_history.state = '5') ORDER BY dbo.cash_history.updateDate DESC")
                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds, "action2")
                    Response.Redirect(
                        GetAbsoluteUrl(
                            "~/documents.aspx?t=32&c=" & ds.Tables("action2").Rows(0)("owner_sys_id") & "&g=" &
                            ds.Tables("action2").Rows(0)("good_sys_id") & "&h=" & ds.Tables("action2").Rows(0)("sys_id") &
                            ""))
                ElseIf action = 3 Then
                    Dim sEmployee$ =
                            dbSQL.ExecuteScalar(
                                "select good_sys_id from good where num_cashregister='" & numcashregister & "'")
                    If sEmployee Is Nothing OrElse sEmployee = String.Empty Then
                    Else
                        Response.Redirect(GetAbsoluteUrl("~/NewSupportConduct.aspx?" & sEmployee))
                    End If
                End If
                cmd = New SqlClient.SqlCommand("get_cashregisters_list_by_num_cashregister")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                cmd.Parameters.AddWithValue("@num_cashregister", numcashregister)
                cmd.Parameters.AddWithValue("@findgoodcto", findgoodcto)
                cmd.Parameters.AddWithValue("@findgoodmanufacturer", findgoodmanufacturer)
                Session("FilterGood") = Str(numcashregister).Trim
                Session("num_cash") = Str(numcashregister).Trim
                'Else
                'cmd = New SqlClient.SqlCommand("get_cashregisters_list")
                'cmd.CommandType = CommandType.StoredProcedure
                'cmd.CommandTimeout = 0
                'MsgBox("eefef")
                'End If

                cmd.Parameters.AddWithValue("@pi_free", chkFreeGoods.Checked)
                cmd.Parameters.AddWithValue("@pi_requested", chkRequestedGoods.Checked)
                cmd.Parameters.AddWithValue("@pi_sold", chkSoldGoods.Checked)
                cmd.Parameters.AddWithValue("@pi_outside", chkOutSideGoods.Checked)
                Dim good_types$ = getGood_type_List()
                cmd.Parameters.AddWithValue("@pi_good_type", IIf(good_types = "", DBNull.Value, good_types))
                cmd.Parameters.AddWithValue("@pi_kassa1", False)
                cmd.Parameters.AddWithValue("@pi_kassa2", False)

                'условия на дату
                If chkUseDate.Checked Then
                    If cal.SelectedDates.Count = 0 Then
                        msgCashregister.Text = "Выберите дату"
                        Exit Sub
                    End If
                    cmd.Parameters.AddWithValue("@pi_start_date", cal.SelectedDates.Item(0).ToShortDateString)
                    cmd.Parameters.AddWithValue("@pi_finish_date",
                                                cal.SelectedDates.Item(cal.SelectedDates.Count - 1).ToShortDateString)
                    cmd.Parameters.AddWithValue("@pi_use_date_type", lstUseDateType.SelectedIndex)
                Else
                    cmd.Parameters.AddWithValue("@pi_start_date", "")
                    cmd.Parameters.AddWithValue("@pi_finish_date", "")
                    cmd.Parameters.AddWithValue("@pi_use_date_type", 255)
                End If
                'обрабатываем только фильтр
                If Session("FilterGood") Is Nothing OrElse Session("FilterGood") = "" Then
                    cmd.Parameters.AddWithValue("@pi_filter", "")
                Else
                    cmd.Parameters.AddWithValue("@pi_filter", Session("FilterGood"))
                    If numcashregister <> 0 Then
                        Session("FilterGood") = "num_cashregister = '" & numcashregister & "'"
                    End If
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                If ds.Tables.Count > 0 Then
                    If ViewState("goodsort") = "" Then
                        ds.Tables(0).DefaultView.Sort = "good_type_sys_id ASC, num_cashregister DESC"
                        ViewState("goodsort") = "num_cashregister DESC"
                    Else
                        ds.Tables(0).DefaultView.Sort = "good_type_sys_id ASC, " & ViewState("goodsort")
                    End If
                    grdGood.DataSource = ds.Tables(0).DefaultView
                    grdGood.DataKeyField = "good_sys_id"
                    grdGood.DataBind()
                End If

                If sCaption.Length = 0 Then
                    'Показываем какие кассы выбраны в фильтре
                    If Not chkFreeGoods.Checked And Not chkRequestedGoods.Checked And Not chkSoldGoods.Checked Then
                        sCaption = "Не задан критерий поиска. Выберите Свободные, Заказанные или Проданные"
                    Else
                        If chkFreeGoods.Checked Then
                            sCaption = "свободные"
                        End If
                        If chkRequestedGoods.Checked Then
                            If sCaption.Length > 0 Then sCaption = sCaption & ", "
                            sCaption = sCaption & "заказанные"
                        End If
                        If chkSoldGoods.Checked Then
                            If sCaption.Length > 0 Then sCaption = sCaption & " , "
                            sCaption = sCaption & "проданные"
                        End If
                        If chkOutSideGoods.Checked Then
                            If sCaption.Length > 0 Then sCaption = sCaption & " и "
                            sCaption = sCaption & "со стороны"
                        End If

                        sCaption = "Показаны "
                        sCaptionType = " " & getGood_type_Name_List()
                        sCaptionDate = ""
                        If chkUseDate.Checked Then
                            If cal.SelectedDates.Count = 1 Then
                                sCaptionDate = " " & cal.SelectedDate.ToString("dd.MM.yyyy")
                            ElseIf cal.SelectedDates.Count > 1 Then
                                sCaptionDate = " с " & cal.SelectedDates(0).ToString("dd.MM.yyyy") & " по " &
                                               cal.SelectedDates(cal.SelectedDates.Count - 1).ToString("dd.MM.yyyy")
                            Else
                                sCaptionDate = " Не выбрана дата"
                            End If
                        End If
                    End If

                    sCaption = sCaption & sCaptionDate & sCaptionType
                End If
                lblFilterCaption.Text = sCaption
            Catch
                msgCashregister.Text = "Ошибка загрузки информации о кассовых аппаратах!<br>" & Err.Description
            End Try
        End Sub

        Sub bindTO()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim num_goodto

            num_goodto = txt_numgoodto.Text

            cmd = New SqlClient.SqlCommand("get_goodto_list_by_num")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.Parameters.AddWithValue("@num_goodto", num_goodto)
            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            If ds.Tables.Count > 0 Then
                If ViewState("goodsort") = "" Then
                    ds.Tables(0).DefaultView.Sort = "good_type_sys_id ASC, good_num DESC"
                    ViewState("goodsort") = "good_num DESC"
                Else
                    ds.Tables(0).DefaultView.Sort = "good_type_sys_id ASC, " & ViewState("goodsort")
                End If
                grdGoodTO.DataSource = ds.Tables(0).DefaultView
                grdGoodTO.DataKeyField = "goodto_sys_id"
                grdGoodTO.DataBind()
            End If
        End Sub

        Private Sub grdGood_EditCommand(ByVal source As System.Object,
                                        ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdGood.EditCommand
            'Ограничение прав на редактирование
            If Session("rule15") = "1" Then
                grdGood.EditItemIndex = CInt(e.Item.ItemIndex)
                Bind()
            End If
        End Sub

        Private Sub grdGood_CancelCommand(ByVal source As System.Object,
                                          ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdGood.CancelCommand
            grdGood.EditItemIndex = - 1
            Bind()
        End Sub

        Private Sub grdGood_DeleteCommand(ByVal source As System.Object,
                                          ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdGood.DeleteCommand
            'Ограничение прав на удаление
            If Session("rule15") = "1" Then
                Try
                    dbSQL.Execute("DELETE FROM good WHERE good_sys_id='" & grdGood.DataKeys(e.Item.ItemIndex) & "'")
                Catch
                    If Err.Number = 1 Then
                        msgCashregister.Text = "Выбранную запись нельзя удалить!"
                    Else
                        msgCashregister.Text = "Ошибка удаления записи!<br>" & Err.Description
                    End If
                End Try
                grdGood.EditItemIndex = - 1
                Bind()
            End If
        End Sub

        Private Sub grdGood_ItemDataBound(ByVal sender As System.Object,
                                          ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) _
            Handles grdGood.ItemDataBound
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.DataItem("state") = 3 Then
                    e.Item.BackColor = Color.Azure ' Drawing.Color.FromArgb(210, 210, 210)
                ElseIf e.Item.DataItem("state") = 2 Then
                    e.Item.BackColor = Color.Honeydew 'Drawing.Color.FromArgb(230, 230, 230)
                ElseIf e.Item.DataItem("state") = 4 Then
                    e.Item.BackColor = Color.AntiqueWhite   ' Drawing.Color.FromArgb(200, 200, 200)
                End If
                Dim s$ = ""

                If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then

                    s = e.Item.DataItem("ownerInfo")
                    CType(e.Item.FindControl("lblSaleOwner"), Label).Text = s
                Else
                    s = ""
                End If
                If Not IsDBNull(e.Item.DataItem("payer_sys_id")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblCashOwner"), Label).Text = s
                    'ElseIf IsDBNull(e.Item.DataItem("owner_sys_id")) Then
                    '    s = e.Item.DataItem("customer_name") & "<br>UID: " & e.Item.DataItem("customer_sys_id")
                    '    CType(e.Item.FindControl("lblCashOwner"), Label).Text = s
                End If
                If Not IsDBNull(e.Item.DataItem("delivery_sys_id")) Then
                    If e.Item.DataItem("state") = 4 Then

                        CType(e.Item.FindControl("lblGoodDelivery"), Label).Text = "Аппарат со стороны"
                    Else
                        CType(e.Item.FindControl("lblGoodDelivery"), Label).Text = "№" &
                                                                                   e.Item.DataItem("delivery_sys_id") &
                                                                                   " от " &
                                                                                   Format(
                                                                                       e.Item.DataItem("delivery_date"),
                                                                                       "dd.MM.yyyy")
                    End If

                End If

                If IsDBNull(e.Item.DataItem("place_rn_id")) Then
                    CType(e.Item.FindControl("lblPlaceRegion"), Label).Visible = False
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i

                s = ""
                If Not IsDBNull(e.Item.DataItem("worker")) Then
                    s = s & e.Item.DataItem("worker") & " "
                End If
                If Not IsDBNull(e.Item.DataItem("d")) Then
                    s = s & Format(e.Item.DataItem("d"), "dd/MM/yyyy") & "&nbsp;" &
                        Format(e.Item.DataItem("d"), "HH:mm") & "<br>"
                End If
                If Not IsDBNull(e.Item.DataItem("good_info")) Then
                    s = s & "<br><b><span style='color:red;background-color:white'>" & e.Item.DataItem("good_info") &
                        "</span></b>"
                End If
                CType(e.Item.FindControl("lbledtInfo"), Label).Text = s

                'картинки
                s = CStr(e.Item.DataItem("alert"))
                e.Item.FindControl("imgAlert").Visible = s.Length > 0
                If s.Length > 0 Then CType(e.Item.FindControl("imgAlert"), HyperLink).ToolTip = s
                e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso
                                                           e.Item.DataItem("support") = "1"
                Dim b As Boolean = e.Item.DataItem("repair")
                e.Item.FindControl("imgRepair").Visible = b
                If b Then
                    Dim i As Integer = CInt(e.Item.DataItem("repaired"))
                    If i > 1 Then
                        CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip =
                            "В ремонте. До этого в ремонте был " & i - 1 & " раз(а)"
                    Else
                        CType(e.Item.FindControl("imgRepair"), HyperLink).ToolTip =
                            "В ремонте. До этого в ремонте не был"
                    End If
                End If
                e.Item.FindControl("imgRepaired").Visible = Not (b OrElse CInt(e.Item.DataItem("repaired")) = 0)
                If e.Item.FindControl("imgRepaired").Visible Then
                    CType(e.Item.FindControl("imgRepaired"), HyperLink).ToolTip = "Был в ремонте " &
                                                                                  CInt(e.Item.DataItem("repaired")) &
                                                                                  " раз(а)"
                End If

                If IsDBNull(e.Item.DataItem("state_skno")) Then
                    e.Item.FindControl("imgSupportSKNO").Visible = 0
                Else
                    e.Item.FindControl("imgSupportSKNO").Visible = e.Item.DataItem("state_skno")
                End If

            ElseIf e.Item.ItemType = ListItemType.EditItem Then

                If e.Item.DataItem("good_type_sys_id") = Config.Kasbi04_ID Or e.Item.DataItem("good_type_sys_id") = 1119 _
                    Then
                    CType(e.Item.FindControl("pnlMarkaCTO2"), Panel).Visible = True
                    ' CType(e.Item.FindControl("pnlMarkaCP"), Panel).Visible = True
                Else
                    CType(e.Item.FindControl("pnlMarkaCTO2"), Panel).Visible = False
                    ' CType(e.Item.FindControl("pnlMarkaCP"), Panel).Visible = False
                End If

                Try
                    adapt = dbSQL.GetDataAdapter("get_goods_is_cashregister", True)
                    ds = New DataSet
                    adapt.Fill(ds)
                    With CType(e.Item.FindControl("lstGoodType"), DropDownList)
                        .DataSource = ds
                        .DataTextField = "name"
                        .DataValueField = "good_type_sys_id"
                        .DataBind()
                        .Items.FindByValue(e.Item.DataItem("good_type_sys_id")).Selected = True
                        If (CurrentUser.is_admin = False) Then
                            .Enabled = False
                        End If
                    End With
                Catch
                    msgCashregister.Text = "Ошибка формирования списка касс!<br>" & Err.Description
                    Exit Sub
                End Try
                If e.Item.DataItem("state") = 4 Then
                    CType(e.Item.FindControl("lstGoodDelivery"), DropDownList).Visible = False
                    CType(e.Item.FindControl("lblGoodDelivery"), Label).Text = "Аппарат со стороны"
                    CType(e.Item.FindControl("lstSaleOwner"), DropDownList).Visible = False
                Else
                    Try
                        adapt =
                            dbSQL.GetDataAdapter(
                                "select delivery_sys_id,'№' + cast(delivery_sys_id as nvarchar) + ' от ' + cast(day(delivery_date) as nvarchar)+'.'+cast(month(delivery_date) as nvarchar)+'.'+cast(year(delivery_date) as nvarchar) as delivery from delivery where deleted =0 or deleted is NULL")
                        ds = New DataSet
                        adapt.Fill(ds)
                        With CType(e.Item.FindControl("lstGoodDelivery"), DropDownList)
                            .DataSource = ds.Tables(0).DefaultView
                            .DataValueField = "delivery_sys_id"
                            .DataTextField = "delivery"
                            .DataBind()
                            .Items.FindByValue(e.Item.DataItem("delivery_sys_id")).Selected = True
                            If (CurrentUser.is_admin = False) Then
                                .Enabled = False
                            End If
                        End With
                    Catch
                        msgCashregister.Text = "Ошибка загрузки информации о поставках!<br>" & Err.Description
                    End Try

                    Dim query As String
                    query = "select * from tbl_result where id in (select * from tbl_query)"

                    Try
                        adapt =
                            dbSQL.GetDataAdapter(
                                "select s.sale_sys_id, cast(c.customer_sys_id as nvarchar) + '/' + cast(s.sale_sys_id as nvarchar) + ' ' + cast(day(s.sale_date) as nvarchar)+'.'+cast(month(s.sale_date) as nvarchar)+'.'+cast(year(s.sale_date) as nvarchar) + ' ' + c.customer_name as sale from customer c, sale s where c.customer_sys_id=s.customer_sys_id " &
                                IIf(IsDBNull(e.Item.DataItem("sale_sys_id")), "".ToString(),"and s.sale_sys_id = " & e.Item.DataItem("sale_sys_id")).ToString() &
                                " order by c.cto DESC, c.customer_name, s.sale_date DESC")
                        ds = New DataSet
                        adapt.Fill(ds)
                        With CType(e.Item.FindControl("lstSaleOwner"), DropDownList)
                            .DataSource = ds.Tables(0).DefaultView
                            .DataValueField = "sale_sys_id"
                            .DataTextField = "sale"
                            .DataBind()
                            .Items.Insert(0, New ListItem(ClearString, ClearString))
                            If Not IsDBNull(e.Item.DataItem("sale_sys_id")) Then
                                .Items.FindByValue(e.Item.DataItem("sale_sys_id")).Selected = True
                            End If
                            If (CurrentUser.is_admin = False) Then
                                .Enabled = False
                            End If
                        End With
                    Catch
                        msgCashregister.Text = "Ошибка загрузки информации о продаже клиенту!<br>" & Err.Description
                    End Try
                End If
                If Not IsDBNull(e.Item.DataItem("payer_sys_id")) Then
                    CType(e.Item.FindControl("lblCashOwner_edit"), Label).Text = e.Item.DataItem("payerInfo")
                End If

                'районы установки

                Try
                    adapt = dbSQL.GetDataAdapter("select place_rn_id,name from Place_Rn order by name")
                    ds = New DataSet
                    adapt.Fill(ds)
                    With CType(e.Item.FindControl("lstRegionPlace"), DropDownList)
                        .DataSource = ds.Tables(0).DefaultView
                        .DataValueField = "place_rn_id"
                        .DataTextField = "name"
                        .DataBind()
                        .Items.Insert(0, New ListItem(ClearString, ClearString))
                        If Not IsDBNull(e.Item.DataItem("place_rn_id")) Then
                            .Items.FindByValue(e.Item.DataItem("place_rn_id")).Selected = True
                        End If
                    End With
                    'idDeliveryOld = CInt(CType(e.Item.FindControl("lstGoodDelivery"), DropDownList).SelectedItem.Value)
                    'idGoodTypeOld = CInt(CType(e.Item.FindControl("lstGoodType"), DropDownList).SelectedItem.Value)
                Catch
                    msgCashregister.Text = "Ошибка загрузки информации о поставках!<br>" & Err.Description
                End Try
            End If
        End Sub

        Private Sub grdGood_UpdateCommand(ByVal source As System.Object,
                                          ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) _
            Handles grdGood.UpdateCommand
            'Ограничение прав на редактирование
            If Session("rule15") = "1" Then
                'Обрабатываем одинарные кавычки (замена на двойные)
                Dim s0$ = CType(e.Item.FindControl("lstGoodType"), DropDownList).SelectedItem.Value
                Dim s1$
                Try
                    Dim d As Double
                    d = CDbl(CType(e.Item.FindControl("txtedtPrice"), TextBox).Text)
                    s1 = CStr(d)
                Catch
                    msgCashregister.Text = "Укажите корректно цену товара!"
                    Exit Sub
                End Try
                Dim s2$ = CType(e.Item.FindControl("txtedtNum"), TextBox).Text.Replace("'", """")
                Dim s3$ = CType(e.Item.FindControl("txtedtReestr"), TextBox).Text.Replace("'", """")
                Dim s4$ = CType(e.Item.FindControl("txtedtPZU"), TextBox).Text.Replace("'", """")
                Dim s5$ = CType(e.Item.FindControl("txtedtMFP"), TextBox).Text.Replace("'", """")
                Dim s6$ = CType(e.Item.FindControl("txtedtCTO"), TextBox).Text.Replace("'", """")
                Dim s7$ = CType(e.Item.FindControl("txtedtPlace"), TextBox).Text.Replace("'", """")
                Dim s8$ = CType(e.Item.FindControl("txtedtKassir1"), TextBox).Text.Replace("'", """")
                Dim s9$ = CType(e.Item.FindControl("txtedtKassir2"), TextBox).Text.Replace("'", """")
                Dim s10$ = CType(e.Item.FindControl("txtedtInfo"), TextBox).Text.Replace("'", """")
                's10$ = s10$ & " (" & CurrentUser.Name & "-" & Now & ")"
                Dim s11$, s12$, s13$, s14$
                If CType(e.Item.FindControl("lstGoodDelivery"), DropDownList).Visible = True Then
                    s11 = CType(e.Item.FindControl("lstGoodDelivery"), DropDownList).SelectedItem.Value
                    s12 = CType(e.Item.FindControl("lstSaleOwner"), DropDownList).SelectedItem.Value
                    If s12 = ClearString Then s12 = "null" : s13 = "1" Else s13 = "3"
                Else
                    s11 = "0"
                    s12 = "null"
                    s13 = "4"
                End If

                Dim check$ = CheckGoodInDelivery(s0, s11, grdGood.DataKeys(e.Item.ItemIndex))
                If check <> "" Then
                    msgCashregister.Text = check
                    Exit Sub
                End If

                If CType(e.Item.FindControl("lstRegionPlace"), DropDownList).SelectedIndex > 0 Then
                    s14 = CType(e.Item.FindControl("lstRegionPlace"), DropDownList).SelectedItem.Value
                Else
                    s14 = "null"
                End If
                Dim s15$ = CType(e.Item.FindControl("txtedtCP"), TextBox).Text.Replace("'", """")
                Dim s16$ = CType(e.Item.FindControl("txtedtCTO2"), TextBox).Text.Replace("'", """")
                Dim s17$ = CType(e.Item.FindControl("txtedtRegistrationNumberSkno"), TextBox).Text.Replace("'", """")
                Dim s18$ = CType(e.Item.FindControl("txtedtSerialNumberSkno"), TextBox).Text.Replace("'", """")

                'ПРОВЕРИТЬ ПОЗЖЕ
                idDeliveryNew = CInt(s11)
                idGoodTypeNew = CInt(s0)
                '_serviceGood.CheckTransferDelivery(idDeliveryOld, idDeliveryNew, idGoodTypeOld, idGoodTypeNew, 1)

                Try
                    s1 = Replace(s1, ",", ".")
                    Dim sql$ =
                            String.Format(
                                "UPDATE good SET good_type_sys_id='{0}',price='{1}',num_cashregister='{2}',num_control_reestr='{3}',num_control_pzu='{4}',num_control_mfp='{5}',num_control_cto='{6}',set_place='{7}',kassir1='{8}',kassir2='{9}',info='{10}',delivery_sys_id={11},sale_sys_id={12},state={13},place_rn_id={14},num_control_cp='{15}',num_control_cto2='{16}',registration_number_skno='{17}',serial_number_skno = '{18}' WHERE good_sys_id={19}",
                                s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18,
                                grdGood.DataKeys(e.Item.ItemIndex))
                    dbSQL.Execute(sql)
                Catch
                    msgCashregister.Text = "Ошибка обновления записи!<br>" & Err.Description & "(" &
                                           String.Format(
                                               "UPDATE good SET good_type_sys_id='{0}',price='{1}',num_cashregister='{2}',num_control_reestr='{3}',num_control_pzu='{4}',num_control_mfp='{5}',num_control_cto='{6}',set_place='{7}',kassir1='{8}',kassir2='{9}',info='{10}',delivery_sys_id={11},sale_sys_id={12},state={13},place_rn_id={14},num_control_cp='{15}',num_control_cto2='{16}',registration_number_skno='{17}',serial_number_skno = '{18}' WHERE good_sys_id={19}",
                                               s0, s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11, s12, s13, s14, s15, s16, s17, s18,
                                               grdGood.DataKeys(e.Item.ItemIndex)) & ")"
                End Try
                grdGood.EditItemIndex = - 1
                Bind()
            End If
        End Sub

        Private Sub grdGood_SortCommand(ByVal source As System.Object,
                                        ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) _
            Handles grdGood.SortCommand
            If ViewState("goodsort") = e.SortExpression Then
                ViewState("goodsort") = e.SortExpression & " DESC"
            Else
                ViewState("goodsort") = e.SortExpression
            End If
            Bind()
        End Sub

        Private Sub lblShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblShow.Click
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("ShowFreeGoods") = chkFreeGoods.Checked
            c.Item("ShowRequestedGoods") = chkRequestedGoods.Checked
            c.Item("ShowSoldGoods") = chkSoldGoods.Checked
            c.Item("ShowOutSideGoods") = chkOutSideGoods.Checked
            c.Item("Good_Type") = getGood_type_List()
            c.Item("UseDate") = chkUseDate.Checked
            If Not lstUseDateType.SelectedItem Is Nothing Then
                c.Item("UseDateType") = lstUseDateType.SelectedItem.Value
            End If
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)
            Session("FilterGood") = ""
            Bind()
        End Sub

        Private Sub btnFindGood_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles btnFindGood.Click
            Dim str$ = ""
            Dim sFilter$ = ""
            Dim sCaption$ = ""

            If isFind Then Exit Sub

            str = txtFindGoodNum.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = - 1 Then
                sFilter = "num_cashregister like '%" & str & "%'"
                Session("num_cashregister") = sFilter
                Session("num_cash") = str
                sCaption = "номер ККМ: '" & str & "'; "
            End If
            str = txtFindGoodManufacturer.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = - 1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(num_control_reestr like '%" & str & "%' or num_control_pzu like '%" & str &
                          "%' or num_control_mfp like '%" & str & "%' or num_control_cp like '%" & str & "%')"
                sCaption = sCaption & "СК Изготовителя: '" & str & "'; "
            End If

            str = txtFindGoodCTO.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = - 1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "(num_control_cto like '%" & str & "%' or num_control_cto2 like '%" & str & "%')"
                sCaption = sCaption & "СК ЦТО: '" & str & "'; "
            End If

            str = txtFindGoodSetPlace.Text.Trim
            If str.Length > 0 And str.IndexOf("'") = - 1 Then
                If sFilter.Length > 0 Then sFilter = sFilter & " and "
                sFilter = sFilter & "set_place like '%" & str & "%'"
                sCaption = sCaption & "место установки ККМ: '" & str & "'; "
            End If

            If sFilter.Length > 0 Then
                Session("FilterGood") = sFilter
                isFind = True
                Bind(
                    "Показаны ККМ удовлетворяющие заданному критерию (" & sCaption.Substring(0, sCaption.Length - 2) &
                    ")")
            End If
        End Sub

        Private Sub txtFindGoodNum_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtFindGoodNum.TextChanged
            If Request.Form("FindHidden") = "1" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub txtFindGoodManufacturer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtFindGoodManufacturer.TextChanged
            If Request.Form("FindHidden") = "2" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub txtFindGoodCTO_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If Request.Form("FindHidden") = "5" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Sub btnFilter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFilter.Click
            pnlFilter.Visible = Not pnlFilter.Visible
            Dim c As HttpCookie = Request.Cookies("Ramok")
            c.Item("GoodFilterPanel") = pnlFilter.Visible
            c.Item("Good_Type") = getGood_type_List()
            c.Expires = DateTime.MaxValue
            Response.SetCookie(c)
        End Sub

        Private Sub txtFindGoodSetPlace_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles txtFindGoodSetPlace.TextChanged
            If Request.Form("FindHidden") = "6" Then
                btnFindGood_Click(sender, Nothing)
            End If
        End Sub

        Private Function CheckGoodInDelivery(ByVal good_type_sys_id As String, ByVal delivery_sys_id As String,
                                             ByVal good_sys_id As String) As String
            Dim errStr$ = ""
            Dim cmd As SqlClient.SqlCommand
            If delivery_sys_id <> 0 Then
                cmd = New SqlClient.SqlCommand("prc_checkGoodInDelivery")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_good_type_sys_id", good_type_sys_id)
                cmd.Parameters.AddWithValue("@pi_delivery_sys_id", delivery_sys_id)
                cmd.Parameters.AddWithValue("@pi_good_sys_id", good_sys_id)
                errStr = dbSQL.ExecuteScalar(cmd)
            End If
            Return errStr
        End Function

        Protected Sub lnk_findgoodto_Click(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles lnk_findgoodto.Click
            bindTO()
        End Sub
    End Class
End Namespace
