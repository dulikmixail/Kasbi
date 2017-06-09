Imports System.Globalization
Imports System.Threading

Namespace Kasbi

Partial Class RepairMaster
        Inherits PageBase

        Dim i
        Dim show_state = 0
        Dim to_made = 0
        Dim customer
        Dim iCash
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

        Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim type = Request.Params(0)
            iCash = Request.Params(1)
            customer = Request.Params(2)
            Dim query

            If type = "outrepair" Then
                'Выдача аппарата с ремонта
                query = dbSQL.ExecuteScalar("UPDATE good SET inrepair=null WHERE good_sys_id='" & iCash & "'")
                Response.Redirect(GetAbsoluteUrl("~/RepairMaster.aspx"))
            ElseIf type = "setrepair" Then
                'Принятие ККМ в ремонт
                Dim cmd As SqlClient.SqlCommand

                Dim akt$ = GetNewAktNumber()
                If akt Is Nothing Then
                    akt = ""
                End If

                If customer = "" Then
                    customer = dbSQL.ExecuteScalar("SELECT sale.customer_sys_id FROM sale INNER JOIN good ON sale.sale_sys_id = good.sale_sys_id WHERE  (good.good_sys_id = " & iCash & ")")
                End If

                cmd = New SqlClient.SqlCommand("new_repair")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.Parameters.AddWithValue("@pi_owner_sys_id", customer)
                cmd.Parameters.AddWithValue("@pi_date_in", Now)
                cmd.Parameters.AddWithValue("@pi_date_out", DBNull.Value)
                cmd.Parameters.AddWithValue("@pi_marka_cto_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_pzu_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_pzu_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_mfp_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_mfp_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_reestr_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_reestr_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto2_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cto2_out", "")
                cmd.Parameters.AddWithValue("@pi_marka_cp_in", "")
                cmd.Parameters.AddWithValue("@pi_marka_cp_out", "")
                cmd.Parameters.AddWithValue("@pi_zreport_in", "")
                cmd.Parameters.AddWithValue("@pi_zreport_out", "")
                cmd.Parameters.AddWithValue("@pi_itog_in", "")
                cmd.Parameters.AddWithValue("@pi_itog_out", "")
                cmd.Parameters.AddWithValue("@pi_details", "")
                cmd.Parameters.AddWithValue("@pi_akt", akt)
                cmd.Parameters.AddWithValue("@pi_summa", "")
                cmd.Parameters.AddWithValue("@pi_info", "")
                cmd.Parameters.AddWithValue("@pi_repair_info", "")
                cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@pi_repair_in", 1)
                cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@repare_in_info", " ")
                cmd.CommandType = CommandType.StoredProcedure
                dbSQL.Execute(cmd)

                query = dbSQL.ExecuteScalar("Update good SET inrepair='1' WHERE good_sys_id='" & iCash & "'")


                query = dbSQL.ExecuteScalar("SELECT top 1 sys_id FROM cash_history WHERE good_sys_id='" & iCash & "' AND state='5' ORDER BY sys_id DESC")
                Response.Redirect(GetAbsoluteUrl("~/RepairNew.aspx?cash=" & iCash & "&hc=" & query))
            ElseIf type = "" And iCash <> "" Then

            End If

            If Not IsPostBack Then
                If Session("repair-filter") = "" Then Session("repair-filter") = " where good.inrepair='1' "
                bind(Session("repair-filter"))
            End If
        End Sub

        Function GetNewAktNumber() As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            'новый номер договора
            Try
                cmd = New SqlClient.SqlCommand("get_next_repair_akt")
                cmd.Parameters.AddWithValue("@good_sys_id", iCash)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                Dim s
                Dim num_cashregister
                s = ds.Tables(0).Rows(0).Item("num_repairs")
                num_cashregister = ds.Tables(0).Rows(0).Item("num_cashregister")
                num_cashregister = Trim(num_cashregister)
                s = s + 1

                GetNewAktNumber = num_cashregister & "/" & Date.Now.Month & "/" & s
            Catch
                Return ""
            End Try
        End Function

        Public Sub findgood()
            Dim filter

            If txtFindGoodNum.Text.Trim.Length > 4 Then
                filter = " where good.num_cashregister like '%" & txtFindGoodNum.Text.Trim & "%' "
                Session("repair-filter") = filter

                grdRepair.PageSize = 10
                grdRepair.CurrentPageIndex = 0
                bind(filter)
            End If
        End Sub

        Sub bind(ByVal filter)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            grdRepair.Visible = False

            grdRepair.Visible = True
            cmd = New SqlClient.SqlCommand("get_repair_master")
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

            grdRepair.DataSource = ds.Tables(0).DefaultView
            grdRepair.DataKeyField = "good_sys_id"
            grdRepair.DataBind()
            Session("KKM_ds") = ds
        End Sub

        Protected Sub grdRepair_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdRepair.ItemDataBound
            Dim s$ = ""

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not IsDBNull(e.Item.DataItem("payerInfo")) Then
                    s = e.Item.DataItem("payerInfo")
                    CType(e.Item.FindControl("lblGoodOwner"), Label).Text = s
                End If

                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i

                s = ""
                If Not IsDBNull(e.Item.DataItem("dolg")) Then
                    s = s & e.Item.DataItem("dolg")
                End If
                CType(e.Item.FindControl("lblDolg"), Label).Text = s

                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "<b>" + GetRussianDate(e.Item.DataItem("lastTO")) + "</b><br><br>" + e.Item.DataItem("lastTOMaster")
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

                'Дата ремонта и мастер

                If Not IsDBNull(e.Item.DataItem("repairdate_in")) Then
                    s = "<b>" & Format(e.Item.DataItem("repairdate_in"), "dd.MM.yyyy HH:mm") & " / "
                End If

                If IsDBNull(e.Item.DataItem("repairdate_out")) Then
                    s = s & "??.??.????"
                Else
                    s = s & Format(e.Item.DataItem("repairdate_out"), "dd.MM.yyyy")
                End If

                If Not IsDBNull(e.Item.DataItem("executor")) Then
                    s = s & "</b><br>" & e.Item.DataItem("executor")
                End If

                CType(e.Item.FindControl("lblCto_master"), Label).Text = s

                '
                'If Not IsDBNull(e.Item.DataItem("stateTO")) Then
                '   s = e.Item.DataItem("stateTO")
                'End If
                '
                'CType(e.Item.FindControl("lnkStatus"), LinkButton).Text = "Просмотр"
                'CType(e.Item.FindControl("lnkStatus"), LinkButton).ToolTip = "Просмотр"
                '
                'Если открыт ремонт
                '
                If e.Item.DataItem("repair") = 1 Then
                    e.Item.BackColor = Drawing.Color.FromArgb(250, 210, 210)

                    CType(e.Item.FindControl("lnkSetRepair"), LinkButton).Visible = False
                    CType(e.Item.FindControl("lnkOutRepair"), LinkButton).Visible = False

                    CType(e.Item.FindControl("lnkEditRepair"), LinkButton).PostBackUrl = "RepairNew.aspx?cash=" & e.Item.DataItem("good_sys_id") & "&hc=" & e.Item.DataItem("hc_id")
                    CType(e.Item.FindControl("lnkStatus"), LinkButton).PostBackUrl = "Repair.aspx?" & e.Item.DataItem("good_sys_id")
                Else
                    CType(e.Item.FindControl("lnkEditRepair"), LinkButton).Visible = False

                    CType(e.Item.FindControl("lnkOutRepair"), LinkButton).PostBackUrl = "?a=outrepair&id=" & e.Item.DataItem("good_sys_id")
                    CType(e.Item.FindControl("lnkSetRepair"), LinkButton).PostBackUrl = "SetRepair.aspx?id=" & e.Item.DataItem("good_sys_id") & "&customer=" & e.Item.DataItem("payer_sys_id")
                    CType(e.Item.FindControl("lnkStatus"), LinkButton).PostBackUrl = "Repair.aspx?" & e.Item.DataItem("good_sys_id")

                    If e.Item.DataItem("inrepair") = 0 Then
                        CType(e.Item.FindControl("lnkOutRepair"), LinkButton).Visible = False
                    End If
                End If

            End If
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Protected Sub lnkFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFind.Click
            show_state = 0
            to_made = 2
            findgood()
        End Sub

        Protected Sub lnkFindRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkFindRepair.Click
            Dim filter

            filter = " where good.inrepair='1' "
            Session("repair-filter") = filter

            grdRepair.PageSize = 10
            grdRepair.CurrentPageIndex = 0
            bind(filter)
        End Sub

    End Class
End Namespace
