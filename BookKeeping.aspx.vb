Namespace Kasbi

Partial Class BookKeeping
        Inherits PageBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents btnFilter As System.Web.UI.WebControls.LinkButton
    Protected WithEvents pnlFilter As System.Web.UI.WebControls.Panel


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
        Dim tot As Integer = 0
    Dim rt%, cs%
    Dim customers$
    Dim date_start As DateTime
    Dim date_end As DateTime
    Dim WithEvents repSales As Repeater
    Dim WithEvents grdGoods As DataGrid
    Dim dTotal%, iCount1%, iCount2%, i%, iCustomer%, iSale%

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If
            txtFilter.Attributes.Add("onkeypress", "javascript:if(window.event.keyCode==13){isFind();}")
        End Sub

        Private Sub Bind(Optional ByVal sRequest = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                Dim s$

                If sRequest = "" Then
                    If Session("Filter") <> "" Then
                        s = Session("Filter")
                    End If
                Else
                    s = sRequest
                End If

                If s = "" Then Exit Sub
                adapt = dbSQL.GetDataAdapter(s)
                ds = New DataSet
                adapt.Fill(ds)
                repCustomers.DataSource = ds
                repCustomers.DataBind()
            Catch
                msgCust.Text = "Ошибка загрузки информации о покупателях!<br>" & Err.Description
            End Try
        End Sub

        Private Sub btnFind_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFind.Click
            Dim str$ = txtFilter.Text

            If Trim(str).Length = 0 Then Bind() : Exit Sub

            If str.IndexOf("'") > -1 Then Exit Sub

            Dim s$ = "SELECT * FROM customer c WHERE (cto is null or cto <> 1) and (boos_last_name like '%" & str & "%' or customer_name like '%" & str & "%' or accountant like '%" & str & "%' or unn like '%" & str & "%' or city like '%" & str & "%' or address like '%" & str & "%' or bank_code like '%" & str & "%' or dogovor like '%" & str & "%')"
            Bind(s)
            Session("Filter") = s
        End Sub

        Private Sub lnkShowAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkShowAll.Click
            Dim s$ = "select distinct c.customer_sys_id ,c.customer_name from customer c inner join cash_history hc on hc.owner_sys_id=c.customer_sys_id order by customer_name"
            Bind(s)
            Session("Filter") = s
        End Sub

        Private Sub txtFilter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilter.TextChanged
            If Request.Form("FindHidden") = "1" Then
                btnFind_Click(sender, e)
            End If
        End Sub

        Private Sub repCustomers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repCustomers.ItemDataBound
            Dim tbl As HtmlTable
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView
            Dim cust_id%

            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                grdGoods = CType(e.Item.FindControl("grdGoods"), DataGrid)
                rv = CType(e.Item.DataItem, DataRowView)
                cust_id = CInt(rv.Row("customer_sys_id"))
                i = 0
                Try
                    cmd = New SqlClient.SqlCommand("get_client_cashregisters_in_TO")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust_id)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds)
                    tbl = CType(e.Item.FindControl("tblSaleNotExists"), HtmlTable)
                    If ds.Tables.Count > 0 Then
                        If ds.Tables(ds.Tables.Count - 1).Rows.Count = 0 Then
                            tbl.Visible = True
                        Else
                            grdGoods.DataSource = ds.Tables(ds.Tables.Count - 1)
                            grdGoods.DataBind()
                            tbl.Visible = False
                        End If
                    End If
                Catch ex As Exception
                End Try

            End If
        End Sub

        Private Sub grdGoods_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoods.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                tot = tot + 1
                i = i + 1
                CType(e.Item.FindControl("lblNumGood"), Label).Text = i
                If Not IsDBNull(e.Item.DataItem("lastTO")) Then
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = GetRussianDate(e.Item.DataItem("lastTO")) + "<br>" + e.Item.DataItem("lastTOMaster") + "<br><b>" + Str(tot) + "</b>"
                    e.Item.BackColor = Drawing.Color.FromArgb(210, 210, 210)
                Else
                    CType(e.Item.FindControl("lblLastTO"), Label).Text = "ТО не проводилось"
                End If
                'e.Item.FindControl("imgSupport").Visible = Not IsDBNull(e.Item.DataItem("support")) AndAlso e.Item.DataItem("support") = "1"
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

                Dim cmd As SqlClient.SqlCommand
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet = New DataSet

                Try
                    cmd = New SqlClient.SqlCommand("get_cash_state_TO_history")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_good_sys_id", e.Item.DataItem("good_sys_id"))
                    adapt = dbSQL.GetDataAdapter(cmd)
                    adapt.Fill(ds)
                    Dim c% = ds.Tables(0).DefaultView.Count
                    Dim grdCashHistory As DataGrid = CType(e.Item.FindControl("grdCashHistory"), DataGrid)
                    AddHandler grdCashHistory.ItemDataBound, AddressOf grdCashOwnerHistory_ItemDataBound
                    grdCashHistory.DataSource = ds
                    grdCashHistory.DataBind()

                Catch ex As Exception

                End Try
            End If
        End Sub

        Private Sub grdCashOwnerHistory_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("lblPayer"), Label).Text = e.Item.DataItem("customer_name")
                CType(e.Item.FindControl("lblStatus"), Label).Text = e.Item.DataItem("stateTO")
            End If
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Янв ", " Фев ", " Мар ", " Апр ", " Май ", " Июн ", " Июл ", " Авг ", " Сен ", " Окт ", " Ноя ", " Дек "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Public Function GetRussianDateFull(ByVal d As Date) As String
            Dim m() As String = {" января ", " февраля ", " марта ", " апреля ", " мая ", " июня ", " июля ", " августа ", " сентября ", " октября ", " ноября ", " декабря "}
            GetRussianDateFull = Day(d) & m(Month(d) - 1) & Year(d) & "г."
        End Function

End Class

End Namespace
