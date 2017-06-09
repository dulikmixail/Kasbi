Namespace Kasbi

    Partial Class MakeInvoice
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
        Dim CurrentCustomer%
        Const ClearString$ = "-------"

        Public Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load


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
                Dim startdate As DateTime = New DateTime
                Dim endDate As DateTime = New DateTime

            Catch

            End Try
            lstGoodGroup.SelectedIndex = 0
            lstGoodGroup_SelectedIndexChanged(Nothing, Nothing)
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
                cmd = New SqlClient.SqlCommand("get_good_rest_by_group_price")
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



        Private Sub lstCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomers.SelectedIndexChanged
            If lstCustomers.SelectedItem.Value > 0 Then
                lblCustInfo.Text = GetInfo(lstCustomers.SelectedItem.Value) & "<br><br>"
            Else
                lblCustInfo.Text = ""
            End If
            Session("Customer") = lstCustomers.SelectedItem.Value
        End Sub


        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text

            If Trim(str).Length = 0 Then LoadCustomerList() : Exit Sub
            If str.IndexOf("'") > -1 Then Exit Sub
            Dim s$ = " (customer_name like '%" & str & "%')"
            LoadCustomerList(s)
            Session("CustFilter") = s
        End Sub

        Public Sub LoadCustomerList(Optional ByVal sRequest$ = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            Try
                If sRequest = "" Then
                    If Session("CustFilter") <> "" Then
                        s = Session("CustFilter")
                    Else
                        s = ""
                    End If
                Else
                    s = sRequest
                End If

                If CurrentCustomer.ToString <> "" And s = "" Then
                    s &= " c.customer_sys_id=" & CurrentCustomer.ToString
                End If

                cmd = New SqlClient.SqlCommand("get_customer_for_support")
                cmd.Parameters.AddWithValue("@pi_filter", s)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                lstCustomers.DataSource = ds.Tables(0).DefaultView
                lstCustomers.DataTextField = "customer_name"
                lstCustomers.DataValueField = "customer_sys_id"
                lstCustomers.DataBind()
                lstCustomers.SelectedIndex = -1
                lstCustomers.Items.Insert(0, New ListItem(ClearString, "0"))

                Dim item As ListItem = lstCustomers.Items.FindByValue(CurrentCustomer)
                If item Is Nothing Then
                    lstCustomers.SelectedIndex = 0
                Else
                    item.Selected = True
                End If
                If lstCustomers.SelectedIndex = 0 Then
                    lblCustInfo.Text = ""
                End If
            Catch
                
            End Try
        End Sub



        Function GetInfo(ByVal cust As Integer, Optional ByVal flag As Boolean = True) As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            s = ""
            If cust = 0 Then
                lstCustomers.SelectedIndex = 0
                Return ""
                Exit Function
            End If
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
                'cmd.Parameters.Add("@pi_sys_id", 1)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).DefaultView(0)
                        Dim sTmp$

                        sTmp = .Item("customer_name")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                        sTmp = .Item("unn")
                        If sTmp.Length > 0 Then
                            s = s & "УНП: " & sTmp & "<br>"
                        End If

                        sTmp = .Item("registration")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = "по " & .Item("tax_inspection")
                        If s.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                        sTmp = .Item("customer_address")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "&nbsp;&nbsp;"
                            sTmp = .Item("customer_phone")
                            If sTmp.Length > 0 Then
                                s = s & sTmp
                            End If
                            s = s & "<br>"
                        End If

                        sTmp = .Item("bank")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If



                    End With
                End If
            Catch
            End Try
            GetInfo = s
        End Function


    End Class

End Namespace
