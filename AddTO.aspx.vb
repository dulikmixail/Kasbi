Imports Microsoft.Office.Interop

Namespace Kasbi

    Partial Class AddTO
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

        Dim CurrentCustomer%, iNewCust%
        Const ClearString$ = "-------"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Session("CustFilter") = ""
                LoadCustomerList()
                LoadExecutor()
                LoadPlaceRegion()
                LoadGoodType()
                Session("addto") = ""
            End If

            Dim good_num = dbSQL.ExecuteScalar("SELECT max(good_num) FROM goodto WHERE good_num LIKE '" & CType(lstGoodType.SelectedValue, String) & "%'")
            If IsDBNull(good_num) Then
                txt_GoodNum.Text = lstGoodType.SelectedValue & "000001"
            Else
                txt_GoodNum.Text = lstGoodType.SelectedValue & Left("000000", 6 - (Right(good_num, 6) + 1).ToString.Length) & Right(good_num, 6) + 1
            End If
        End Sub

        Private Sub lstCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomers.SelectedIndexChanged
            If lstCustomers.SelectedItem.Value > 0 Then
                lblCustInfo.Text = "<br>" & GetInfo(lstCustomers.SelectedItem.Value)
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

        Public Sub LoadCustomerList(Optional ByVal sRequest = "")
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

                Dim item As ListItem = lstCustomers.Items.FindByValue(CurrentCustomer)

                If item Is Nothing Then
                    lstCustomers.SelectedIndex = 0
                Else
                    item.Selected = True
                End If

                If lstCustomers.SelectedIndex > 0 Then
                    lblCustInfo.Text = "<br>" & GetInfo(lstCustomers.SelectedItem.Value)
                Else
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

        Sub LoadExecutor()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("get_salers", True)
                ds = New DataSet
                adapt.Fill(ds)

                lstWorker.DataSource = ds
                lstWorker.DataTextField = "name"
                lstWorker.DataValueField = "sys_id"
                lstWorker.DataBind()
                lstWorker.Items.Insert(0, New ListItem(ClearString, "0"))
                lstWorker.SelectedIndex = -1
            Catch
                Exit Sub
            End Try
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
                Exit Sub
            End Try
            lstPlaceRegion.Enabled = True
        End Sub

        Sub LoadGoodType()
            Dim sql$ = ""
            sql = "select * from good_type where is_cashregister='0' and allowCTO='1' order by name"

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstGoodType.DataSource = ds.Tables(0).DefaultView
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
            Catch
            End Try
            lstGoodType.Enabled = True
        End Sub

        Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cmd As SqlClient.SqlCommand
            Dim sql = ""

            Dim goodto_sys_id = dbSQL.ExecuteScalar("select max(goodto_sys_id)+1 from goodto")

            Dim d = New Date(Year(Now), Month(Now), 1)

            Dim d2 = DateTime.Parse(tbxBeginDate.Text)
            Dim add_date = Format(d2, "MM/dd/yyyy")
            Dim d_begin = Format(Now, "MM/dd/yyyy")

            sql = "INSERT INTO goodto VALUES ('" & goodto_sys_id & "', '" & CType(lstGoodType.SelectedValue, String) & "', '" & CType(txt_GoodNum.Text, String) & "', '" & add_date & "', '" & add_date & "', '" & CType(lstPlaceRegion.SelectedValue, String) & "', '" & CType(txt_SetPlace.Text, String) & "', '0', '" & CType(lstCustomers.SelectedValue, String) & "', '" & Session("User").sys_id & "', '" & CType(lstWorker.SelectedValue, String) & "', '0')"
            adapt = dbSQL.GetDataAdapter(sql)
            ds = New DataSet
            adapt.Fill(ds)

            If chk_SetTO.Checked = True Then
                'Постановка оборудования на ТО
                dbSQL.ExecuteScalar("UPDATE goodto SET support='1' WHERE goodto_sys_id='" & goodto_sys_id & "'")

                cmd = New SqlClient.SqlCommand("insert_supportConduct_prod")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_good_sys_id", goodto_sys_id)
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", lstCustomers.SelectedValue)
                cmd.Parameters.AddWithValue("@pi_start_date", d)
                cmd.Parameters.AddWithValue("@pi_executor", Session("User").sys_id)
                cmd.Parameters.AddWithValue("@pi_close_date", Now)
                cmd.Parameters.AddWithValue("@place", txt_SetPlace.Text)
                cmd.Parameters.AddWithValue("@pi_place_rn_id", lstPlaceRegion.SelectedValue)
                cmd.Parameters.AddWithValue("@pi_period", 1)

                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
            End If
        End Sub

    End Class
End Namespace
