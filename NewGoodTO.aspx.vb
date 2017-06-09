Namespace Kasbi

    Partial Class NewGoodTO
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblCashAll As System.Web.UI.WebControls.Label
        Protected WithEvents lblCashRest As System.Web.UI.WebControls.Label
        Dim sum, rest, outside As Integer

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

        Dim CurrentCustomer%, iNewCust%
        Const ClearString$ = "-------"

        Dim iCashCount As New SortedList
        Dim dTotal%, iCount1%, iCount2%, i%, iCustomer%, iSale%
        Dim TotalSum, TotalClientSum As Double

        Dim c, s

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            c = GetPageParam("c")
            s = GetPageParam("s")
            lnk_nav_back.PostBackUrl = "CustomerSales.aspx?c=" & c

            Session("CustFilter") = ""
            Session("addto") = ""

            Dim inf = GetInfo(4118, True)
            lblCustInfo.Text = "<b>Клиент:</b><br>" & inf

            cmd = New SqlClient.SqlCommand("get_goods_by_sale_TO")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_sale_sys_id", s)
            adapter = dbSQL.GetDataAdapter(cmd)
            adapter.Fill(ds)
            grdGoods.DataSource = ds
            grdGoods.DataBind()

            adapter = dbSQL.GetDataAdapter("SELECT good_type.name AS name, goodto.goodto_sys_id, goodto.good_num, customer.customer_name, goodto.add_date FROM goodto INNER JOIN good_type ON goodto.good_type_sys_id = good_type.good_type_sys_id INNER JOIN customer ON goodto.customer_sys_id = customer.customer_sys_id WHERE goodto.customer_sys_id='" & c & "' and goodto.sale_sys_id='" & s & "' ORDER BY goodto.goodto_sys_id DESC")
            ds = New DataSet
            adapter.Fill(ds)
            grdTO.DataKeyField = "goodto_sys_id"
            grdTO.DataSource = ds
            grdTO.DataBind()
        End Sub

        Private Sub grdTO_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdTO.ItemDataBound
            Dim sql$ = ""
            sql = "select * from good_type where is_cashregister='0' and allowCTO='1' order by name"

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)

                CType(e.Item.FindControl("lstAddGood"), DropDownList).DataSource = ds.Tables(0).DefaultView
                CType(e.Item.FindControl("lstAddGood"), DropDownList).DataTextField = "name"
                CType(e.Item.FindControl("lstAddGood"), DropDownList).DataValueField = "good_type_sys_id"
                CType(e.Item.FindControl("lstAddGood"), DropDownList).DataBind()
            Catch
            End Try

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "if (confirm('Вы действительно хотите запись ?')){return true} else {return false};")
                If CurrentUser.is_admin = True Then
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = True
                Else
                    CType(e.Item.FindControl("cmdDelete"), ImageButton).Visible = False
                End If
            End If
        End Sub

        Private Sub grdTO_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTO.UpdateCommand
            Dim txtgood_num_edit As TextBox
            Dim lstAddGood As DropDownList
            Dim sql$

            txtgood_num_edit = CType(e.Item.FindControl("txtgood_num_edit"), TextBox)
            lstAddGood = CType(e.Item.FindControl("lstAddGood"), DropDownList)

            If lstAddGood.SelectedIndex <> 0 Then
                sql = String.Format("update goodto set good_type_sys_id='{0}', good_num='{1}' where goodto_sys_id={2}", lstAddGood.SelectedItem.Value, txtgood_num_edit.Text, grdTO.DataKeys(e.Item.ItemIndex))
            Else
                sql = String.Format("update goodto set good_num='{0}' where goodto_sys_id={1}", txtgood_num_edit.Text, grdTO.DataKeys(e.Item.ItemIndex))
            End If

            dbSQL.Execute(sql)

            grdTO_CancelCommand(source, e)
            Bind()
        End Sub

        Private Sub grdTO_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTO.ItemCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim sql = ""
            If e.CommandName = "AddGoodTO" Then

                Dim txtgood_num As TextBox = CType(e.Item.FindControl("txtgood_num"), TextBox)
                Dim lstAddGood As DropDownList = CType(e.Item.FindControl("lstAddGood"), DropDownList)

                If txtgood_num.Text = String.Empty Then
                    msg.Text = "Неодходимо заполнить поля"
                    Exit Sub
                End If

                c = GetPageParam("c")
                s = GetPageParam("s")

                Dim goodto_sys_id = dbSQL.ExecuteScalar("select max(goodto_sys_id)+1 from goodto")

                Dim d = New Date(Year(Now), Month(Now), Day(Now))
                d = Format(d, "MM/dd/yyyy")

                sql = "INSERT INTO goodto VALUES ('" & goodto_sys_id & "', '" & lstAddGood.SelectedItem.Value & "', '" & txtgood_num.Text & "', '" & s & "', '" & c & "', '" & d & "', '', '', '', '', '', '')"
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)

                Bind()
            End If
        End Sub

        Private Sub grdTO_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTO.DeleteCommand
            Try
                Dim sql$ = String.Format("delete from goodto where goodto_sys_id={0}", grdTO.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(sql)
            Catch
            End Try
            Bind()
        End Sub

        Private Sub grdTO_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTO.CancelCommand
            grdTO.EditItemIndex = -1
            Bind()
        End Sub

        Private Sub grdAdvertising_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdTO.EditCommand
            grdTO.EditItemIndex = e.Item.ItemIndex
            Bind()
        End Sub

        Private Sub grdAdvertising_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdTO.PageIndexChanged
            grdTO.CurrentPageIndex = e.NewPageIndex
            Bind()
        End Sub

        Function GetInfo(ByVal cust As Integer, Optional ByVal flag As Boolean = True) As String
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            s = ""
            If cust = 0 Then
                Return ""
                Exit Function
            End If
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cust)
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
