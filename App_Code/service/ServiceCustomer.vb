Imports System.Data.SqlClient
Imports Kasbi
Imports Microsoft.VisualBasic
Imports Service

Namespace Service
    Public Class ServiceCustomer
        Inherits ServiceExeption
        Implements IService

        Const ClearString$ = "-------"
        Dim ReadOnly _sharedDbSql As MSSqlDB = ServiceDbConnector.GetConnection()

        Public Function AddCustomerTelNotice(customerSysId As Integer, tel As String,
                                             Optional customerTelNoticeType As Integer = 1) As Boolean
            If customerSysId < 1
                Return False
            End If
            Dim cmd = New SqlCommand("set_customer_tel_notice")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_customer_sys_id", customerSysId)
            cmd.Parameters.AddWithValue("@pi_tel_notice", tel)
            If customerTelNoticeType <> 1
                cmd.Parameters.AddWithValue("@pi_customer_tel_notice_type", customerTelNoticeType)
            End If
            Return Convert.ToBoolean(_sharedDbSql.Execute(cmd))
        End Function

        Public Sub LoadTelephoneNotice(ByRef lstCustomers As ListBox, ByRef lstTelephoneNotice As DropDownList,
                                       ByRef lblTelephoneNotice As Label)
            Dim cmd As SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim cutomerSysId As Integer

            If lstCustomers.SelectedItem.Text <> ClearString
                cutomerSysId = Convert.ToInt32(lstCustomers.SelectedValue)
                cmd = New SqlClient.SqlCommand("get_customer_tel_notice")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", cutomerSysId)
                adapt = _sharedDbSql.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                lstTelephoneNotice.DataSource = ds
                lstTelephoneNotice.DataValueField = "customer_tel_notice_sys_id"
                lstTelephoneNotice.DataTextField = "tel_notice"
                lstTelephoneNotice.DataBind()
                lstTelephoneNotice.Items.Insert(0, New ListItem(ClearString, "0"))
                lstTelephoneNotice.Visible = True
                lblTelephoneNotice.Visible = True
            Else
                lstTelephoneNotice.Items.Clear()
                lstTelephoneNotice.Items.Insert(0, New ListItem(ClearString, "0"))
                lstTelephoneNotice.Visible = False
                lblTelephoneNotice.Visible = False
            End If
            If lstTelephoneNotice.Items.Count <= 1 And lstTelephoneNotice.SelectedItem.Text = ClearString
                lstTelephoneNotice.Items.Clear()
                lstTelephoneNotice.Items.Insert(0, New ListItem(ClearString, "0"))
                lstTelephoneNotice.Visible = False
                lblTelephoneNotice.Visible = False
            End If
        End Sub

        Public Sub LoadCustomerList(customerId As String, ByRef lstCustomers As ListBox, ByRef lblCustInfo As Label,
                                    ByRef lblErrors As Label, Optional ByVal sRequest As String = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            Try
                If sRequest <> "" Then
                    s = sRequest
                ElseIf customerId.ToString <> "" Then
                    s &= " c.customer_sys_id=" & customerId.ToString
                End If

                If Not String.IsNullOrEmpty(s)
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
                End If
                lstCustomers.Items.Insert(0, New ListItem(ClearString, "0"))

                Dim item As ListItem = lstCustomers.Items.FindByValue(customerId.ToString())
                If item Is Nothing Then
                    lstCustomers.SelectedIndex = 0
                Else
                    item.Selected = True
                End If
                If lstCustomers.SelectedIndex > 0 Then
                    lblCustInfo.Text = "<br>" &
                                       GetInfo(Convert.ToInt32(lstCustomers.SelectedItem.Value), lstCustomers, lblErrors)
                Else
                    lblCustInfo.Text = ""
                End If
            Catch
                lblErrors.Text = Err.Description
            End Try
        End Sub

        Public Function GetInfo(ByVal cust As Integer, ByRef lstCustomers As ListBox, ByRef lblErrors As Label,
                                Optional ByVal flag As Boolean = True) As String
            Dim ds As DataSet = New DataSet()
            Dim s$
            s = ""
            If cust = 0 Then
                lstCustomers.SelectedIndex = 0
                Return ""
            End If
            ds = GetCutomerInfo(cust)
            Try
                If ds.Tables(0).Rows.Count > 0 Then
                    With ds.Tables(0).DefaultView(0)
                        Dim sTmp$

                        sTmp = .Item("customer_name").ToString()
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = .Item("unn").ToString()
                        If sTmp.Length > 0 Then
                            s = s & "УНП: " & sTmp & "<br>"
                        End If

                        sTmp = .Item("registration").ToString()
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = "по " & .Item("tax_inspection").ToString()
                        If s.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = .Item("customer_address").ToString()
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "&nbsp;&nbsp;"
                            sTmp = .Item("customer_phone").ToString()
                            If sTmp.Length > 0 Then
                                s = s & sTmp
                            End If
                            s = s & "<br>"
                        End If

                        sTmp = .Item("bank").ToString()
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If
                    End With
                End If
            Catch
                lblErrors.Text = Err.Description
            End Try
            GetInfo = s
        End Function

        Public Function GetCutomerInfo(customerId As Integer) As DataSet
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet = New DataSet()
            Try
                cmd = New SqlClient.SqlCommand("get_customer_info")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_customer_sys_id", customerId)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
            Catch
            End Try
            Return ds
        End Function

        'Override Method
        Public Function GetСustomerDetails(customerId As Integer) As String
            Dim ds As DataSet = GetCutomerInfo(customerId)
            Return GetСustomerDetails(ds)
        End Function

        'Override Method
        Public Function GetСustomerDetails(ds As DataSet) As String
            Dim dr As DataRow
            Dim customerDetails As String = String.Empty
            If ds.Tables().Count > 0
                If ds.Tables(0).Rows.Count > 0
                    dr = ds.Tables(0).Rows(0)
                    customerDetails = String.Concat(dr("customer_name"), ", УНП: ", dr("unn"),
                                                    IIf(String.IsNullOrEmpty(ServiceDbHelper.FixNullAndEmpty(dr, "okpo")), "",
                                                        String.Concat(", ОКПО: ", dr("okpo"))),
                                                    vbCrLf, dr("customer_address"),
                                                    IIf(String.IsNullOrEmpty(ServiceDbHelper.FixNullAndEmpty(dr, "customer_phone")), "",
                                                        String.Concat(", Тел/ф.: ", dr("customer_phone"))),
                                                    IIf(String.IsNullOrEmpty(ServiceDbHelper.FixNullAndEmpty(dr, "emails")), "",
                                                        String.Concat(", Email: ", dr("emails"))),
                                                    vbCrLf, dr("bank"))
                End If
            End If
            Return customerDetails
        End Function
    End Class
End Namespace
