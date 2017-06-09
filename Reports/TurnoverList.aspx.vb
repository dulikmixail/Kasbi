Namespace Kasbi.Reports

    Partial Class TurnoverList
        Inherits PageBase

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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            If Not IsPostBack Then
                LoadGoodGroups()
                lstGoodGroup.SelectedIndex = 0
                lstGoodGroup_SelectedIndexChanged(Nothing, Nothing)
            End If
        End Sub

        'Private Sub Bind(ByVal filter As String)
        '    Dim cmd As SqlClient.SqlCommand
        '    Dim adapter As SqlClient.SqlDataAdapter
        '    Dim ds As DataSet = New DataSet
        '    Try
        '        CheckConnection()
        '        cmd = New SqlClient.SqlCommand("prc_rpt_TurnoverList", cn)
        '        cmd.CommandType = CommandType.StoredProcedure
        '        cmd.CommandTimeout = 0

        '        If Not filter Is Nothing AndAlso filter <> String.Empty Then
        '            cmd.Parameters.AddWithValue("@pi_filter", filter)
        '        End If
        '        adapter = New SqlClient.SqlDataAdapter(cmd)
        '        adapter.Fill(ds)
        '        grdTurnoverList.DataSource = ds.Tables(0).DefaultView
        '        grdTurnoverList.DataKeyField = "good_type_sys_id"
        '        grdTurnoverList.DataBind()
        '    Catch
        '        msg.Text = "Ошибка получения информации оборотной ведомости товаров!<br>" & Err.Description
        '    Finally
        '        CloseConnection()
        '    End Try
        'End Sub

        'Private Sub grdTurnoverList_ItemCommand(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)

        '    'Dim i%
        '    'If grdTurnoverList.EditItemIndex = -1 AndAlso e.CommandName = "ViewDetail" Then
        '    '    i = e.Item.ItemIndex
        '    '    'Session("Customer") = grdTurnoverList.DataKeys.Item(i)
        '    '    's = CType(grdCustomers.Items(i).Cells(0).FindControl("lblCustomerName"), LinkButton).Text
        '    '    's1 = CType(grdCustomers.Items(i).FindControl("lblBoosAccountant"), Label).Text
        '    '    'If s.Trim.Length > 0 And s1.Trim.Length > 0 Then s = s & "<br>"
        '    '    'Session("CustomerInfo") = s & s1
        '    '    'Session("CurrentPage") = "CustomerSales"
        '    '    Response.Redirect("TurnoverListByGood.aspx?good =" & repTurnoverList.DataKeys.Item(i))
        '    'End If
        'End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
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
                lstGoodGroup.Items.Insert(0, New ListItem(" --- Все группы ---", "0"))
            Catch
                msg.Text = "Ошибка формирования списка групп товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub


        Function mysql_query(ByVal query)
            Return query
        End Function

        Function mysql_result(ByVal query)
            Return query
        End Function

        Function mysql_num_rows(ByVal query)
            Return query
        End Function

        Function mysql_num_fields(ByVal query)
            Return query
        End Function

        Private Sub lstGoodGroup_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstGoodGroup.SelectedIndexChanged
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                cmd = New SqlClient.SqlCommand("prc_rpt_TurnoverList")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                If (lstGoodGroup.SelectedIndex > 0) Then
                    cmd.Parameters.AddWithValue("@pi_good_group_sys_id", lstGoodGroup.SelectedValue)
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                countRest = 0
                groupName = ""
                repTurnoverList.DataSource = ds
                repTurnoverList.DataBind()
            Catch
                msg.Text = "Ошибка формирования списка остатков товаров!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

        Private Sub repTurnoverList_ItemDataBound(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles repTurnoverList.ItemDataBound
            If (e.Item.ItemType = ListItemType.Item) OrElse (e.Item.ItemType = ListItemType.AlternatingItem) Then
                Dim lblGoodGroupName As Label = CType(e.Item.FindControl("lblGoodGroupName"), Label)
                Dim lblRecordCount As Label = CType(e.Item.FindControl("lblRecordCount"), Label)
                Dim lnkGood As HyperLink = CType(e.Item.FindControl("lnkGood"), HyperLink)
                Dim value$

                countRest = countRest + 1
                lblRecordCount.Text = CStr(countRest)
                If (groupName <> e.Item.DataItem("group_name")) Then
                    groupName = e.Item.DataItem("group_name")
                    lblGoodGroupName.Text = groupName
                Else
                    CType(e.Item.FindControl("GroupSection"), Panel).Visible = False
                End If

                value = "&nbsp;"
                If Not IsDBNull(e.Item.DataItem("prichod_Kol")) Then
                    value = e.Item.DataItem("prichod_Kol")
                End If
                CType(e.Item.FindControl("lblPrichod"), Label).Text = value
                value = "&nbsp;"
                If Not IsDBNull(e.Item.DataItem("rashod_Kol")) Then
                    value = e.Item.DataItem("rashod_Kol")
                End If
                CType(e.Item.FindControl("lblRashod"), Label).Text = value
                value = "&nbsp;"
                If Not IsDBNull(e.Item.DataItem("ostatok_Kol")) Then
                    value = e.Item.DataItem("ostatok_Kol")
                End If
                CType(e.Item.FindControl("lblOstatok"), Label).Text = value

                lnkGood.NavigateUrl = "TurnoverListByGood.aspx?g_t=" & e.Item.DataItem("good_type_sys_id")
            End If

        End Sub

    End Class

End Namespace
