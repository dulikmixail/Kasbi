Namespace Kasbi

    Partial Class Main
        Inherits PageBase
        Protected WithEvents Hyperlink1 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnNewGoodMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkExport As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkReports As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblHallo As System.Web.UI.WebControls.Label
        Protected WithEvents lblDateRange As System.Web.UI.WebControls.Label
        Protected WithEvents lblSale As System.Web.UI.WebControls.Label
        Protected WithEvents lblSaleClient As System.Web.UI.WebControls.Label
        Protected WithEvents lblSaleCTO As System.Web.UI.WebControls.Label
        Protected WithEvents repGoodTypes As System.Web.UI.WebControls.Repeater

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

        Dim countRest% = 0
        Dim groupName$ = ""

        Public Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If CurrentUser.permissions = 4 Then
                lnkBookKeeping.Visible = True
            Else
                lnkBookKeeping.Visible = False
                'If CurrentUser.permissions = 8 Then
                '    StatisticTable.Visible = False
                '    btnNew.NavigateUrl = "~/Customer.aspx"
                'End If
            End If

            'Определяем права
            If Session("rule26") = 0 Then
                lblCash.Visible = False
                lblLinks.Visible = False
            End If


            If Not IsPostBack Then
                Session("AddSaleForCustomer") = ""
                LoadGoodGroups()
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter

            Try
                cmd = New SqlClient.SqlCommand("get_cash_info_by_type")
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                Dim ds As DataSet = New DataSet
                adapt.Fill(ds)
                repGoodStatistic.DataSource = ds
                repGoodStatistic.DataBind()
            Catch
                msgCashInfo.Text = "Ошибка получения информации о кассовых аппаратах!<br>" & Err.Description
            End Try

            Try
                Dim startdate As DateTime = New DateTime
                Dim endDate As DateTime = New DateTime

                Try
                    startdate = Date.Today.AddMonths(-1)
                    endDate = Date.Today
                    If (startdate > endDate) Then
                        startdate = DateTime.Parse(tbxEndDate.Text)
                        endDate = DateTime.Parse(tbxBeginDate.Text)
                    Else
                        startdate = DateTime.Parse(tbxBeginDate.Text)
                        endDate = DateTime.Parse(tbxEndDate.Text)
                    End If
                Catch
                End Try

                tbxBeginDate.Text = startdate.ToShortDateString()
                tbxEndDate.Text = endDate.ToShortDateString()

                cmd = New SqlClient.SqlCommand("get_cash_info_date_range_by_type")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New SqlClient.SqlParameter("@begin_date", startdate))
                cmd.Parameters.Add(New SqlClient.SqlParameter("@end_date", endDate))
                adapt = dbSQL.GetDataAdapter(cmd)
                Dim ds As DataSet = New DataSet
                adapt.Fill(ds)
                repGoodStatisticByDateRange.DataSource = ds
                repGoodStatisticByDateRange.DataBind()
            Catch
                msgCashDateRangeInfo.Text = "Ошибка получения информации о кассовых аппаратах!<br>" & Err.Description
            End Try
            lstGoodGroup.SelectedIndex = 0
            lstGoodGroup_SelectedIndexChanged(Nothing, Nothing)
        End Sub

        Private Sub repGoodStatisticByDateRange_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGoodStatisticByDateRange.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then

                Dim lblDateRange As Label = CType(e.Item.FindControl("lblDateRange"), Label)
                Dim startdate As DateTime = New DateTime
                Dim endDate As DateTime = New DateTime

                Try
                    startdate = Date.Today.AddMonths(-1)
                    endDate = Date.Today
                    If (startdate > endDate) Then
                        startdate = DateTime.Parse(tbxEndDate.Text)
                        endDate = DateTime.Parse(tbxBeginDate.Text)
                    Else
                        startdate = DateTime.Parse(tbxBeginDate.Text)
                        endDate = DateTime.Parse(tbxEndDate.Text)
                    End If
                Catch
                End Try
                lblDateRange.Text = startdate.Day & "." & startdate.Month & "." & startdate.Year & " - " & endDate.Day & "." & endDate.Month & "." & endDate.Year
            End If
        End Sub

        Sub DisabledControls(ByVal b As Boolean)
            tblLinks.Visible = b
        End Sub

        Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter

            Try
                Dim startdate As DateTime = New DateTime
                Dim endDate As DateTime = New DateTime

                Try
                    startdate = Date.Today.AddMonths(-1)
                    endDate = Date.Today
                    If (startdate > endDate) Then
                        startdate = DateTime.Parse(tbxEndDate.Text)
                        endDate = DateTime.Parse(tbxBeginDate.Text)
                    Else
                        startdate = DateTime.Parse(tbxBeginDate.Text)
                        endDate = DateTime.Parse(tbxEndDate.Text)
                    End If
                Catch
                End Try

                cmd = New SqlClient.SqlCommand("get_cash_info_date_range_by_type")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.Add(New SqlClient.SqlParameter("@begin_date", startdate))
                cmd.Parameters.Add(New SqlClient.SqlParameter("@end_date", endDate))
                adapt = dbSQL.GetDataAdapter(cmd)
                Dim ds As DataSet = New DataSet
                adapt.Fill(ds)
                repGoodStatisticByDateRange.DataSource = ds
                repGoodStatisticByDateRange.DataBind()
            Catch
                msgCashDateRangeInfo.Text = "Ошибка получения информации о кассовых аппаратах!<br>" & Err.Description
            End Try
        End Sub

        Sub LoadGoodGroups()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select good_group_sys_id,name group_name from good_group order by group_name")
                ds = New DataSet
                adapt.Fill(ds)
                lstGoodGroup.DataSource = ds
                lstGoodGroup.DataTextField = "group_name"
                lstGoodGroup.DataValueField = "good_group_sys_id"
                lstGoodGroup.DataBind()
                lstGoodGroup.Items.Insert(0, New ListItem(" --- Все группы --- ", "0"))
            Catch
                msgGoodInfo.Text = "Ошибка формирования списка групп товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub lstGoodGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGoodGroup.SelectedIndexChanged
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                cmd = New SqlClient.SqlCommand("get_good_rest_by_group")
                cmd.CommandType = CommandType.StoredProcedure
                If (lstGoodGroup.SelectedIndex > 0) Then
                    cmd.Parameters.AddWithValue("@pi_good_group_sys_id", lstGoodGroup.SelectedValue)
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                countRest = 0
                groupName = ""
                repGoodRest.DataSource = ds
                repGoodRest.DataBind()
            Catch
                msgGoodInfo.Text = "Ошибка формирования списка остатков товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub
        Private Sub repGoodRest_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repGoodRest.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim lblGoodGroupName As Label = CType(e.Item.FindControl("lblGoodGroupName"), Label)
                Dim lblRecordCount As Label = CType(e.Item.FindControl("lblRecordCount"), Label)
                Dim lblGoodPrice As Label = CType(e.Item.FindControl("lblGoodPrice"), Label)
                Dim lblDescription As Label = CType(e.Item.FindControl("lblDescription"), Label)

                Dim price$
                If Not IsDBNull(e.Item.DataItem("price")) Then
                    price = Math.Round(CDbl(e.Item.DataItem("price")), 2) & "(" & Math.Round(CDbl(e.Item.DataItem("price") * 1.2), 2) & ")"
                Else
                    If Not IsDBNull(e.Item.DataItem("price_opt")) Then
                        price = Math.Round(CDbl(e.Item.DataItem("price_opt")), 2) & "(" & Math.Round(CDbl(e.Item.DataItem("price_opt") * 1.2), 2) & ")"
                    Else
                        price = "нет информации о цене"
                    End If
                End If

                'Достаем параметры
                Dim query As String
                query = Session("SELECT * FROM good_type WHERE id like '%%'")

                lblGoodPrice.Text = price
                countRest = countRest + 1

                If (groupName <> e.Item.DataItem("group_name")) Then
                    groupName = e.Item.DataItem("group_name")
                    lblGoodGroupName.Text = groupName
                End If
            End If
        End Sub

        Private Sub lnkRestReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkRestReport.Click
            Dim strRequest$ = "documents.aspx?t=51&group_id={0}"
            If (lstGoodGroup.SelectedIndex > 0) Then
                strRequest = String.Format(strRequest, lstGoodGroup.SelectedValue.ToString())
            Else
                strRequest = "documents.aspx?t=51&group_id=-1"
            End If

            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub
    End Class

End Namespace
