Imports System.Globalization
Imports System.Threading


Namespace Kasbi

Partial Class RepairNew
        Inherits PageBase

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
        Protected WithEvents lnkTTNRepair As System.Web.UI.WebControls.LinkButton
    Protected WithEvents lnkActRepairRealization As System.Web.UI.WebControls.LinkButton
    Protected WithEvents RequiredFieldValidator2 As System.Web.UI.WebControls.RequiredFieldValidator
    Protected WithEvents lblDateFormat3 As System.Web.UI.WebControls.Label
    Protected WithEvents CompareValidator1 As System.Web.UI.WebControls.CompareValidator


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Dim sCaptionAddSupport As String = "��������� �� ��"
    Dim CurrentCustomer%, iNewCust%
    Dim iCash%, iCashHistory%
    Dim sCaptionRemoveSupport As String = "����� � ��"
    Dim d As Kasbi.Documents
        Const ClearString$ = "-------"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                iCash = Request.Params("cash")
                iCashHistory = Request.Params("hc")
            Catch
                msg.Text = "�������� ������"
                Exit Sub
            End Try
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
            If Not IsPostBack Then
                Session("CustFilter") = ""
                LoadExecutor()
                LoadRepairInfo()
                LoadGoodInfo()
                LoadCustomerList()
            End If
        End Sub

        Sub LoadGoodInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim reader As SqlClient.SqlDataReader

            Try
                cmd = New SqlClient.SqlCommand("get_cash_repair_history")
                cmd.Parameters.AddWithValue("@good_sys_id", iCash)
                cmd.Parameters.AddWithValue("@sys_id", iCashHistory)
                cmd.CommandType = CommandType.StoredProcedure
                reader = dbSQL.GetReader(cmd)
                If Not reader.Read Then
                    msg.Text = "������ �������� ���������� � ������!<br>"
                    Exit Sub
                End If
                lblCashType.Text = reader("good_name") & "&nbsp;&nbsp;"
                lblCash.Text = "�" & reader("num_cashregister")
                Dim s$, sTmp$
                Dim b As Boolean

                s = Trim(reader("marka_reestr_out"))
                txtNewMarkaReestrIn.Text = Trim(reader("marka_reestr_in"))
                txtNewMarkaReestrOut.Text = Trim(reader("marka_reestr_out"))

                sTmp = Trim(reader("marka_pzu_out"))
                txtNewMarkaPZUIn.Text = Trim(reader("marka_pzu_in"))
                txtNewMarkaPZUOut.Text = Trim(reader("marka_pzu_out"))
                s = s & " / " & sTmp
                sTmp = Trim(reader("marka_mfp_out"))
                txtNewMarkaMFPIn.Text = Trim(reader("marka_mfp_in"))
                txtNewMarkaMFPOut.Text = Trim(reader("marka_mfp_out"))
                s = s & " / " & sTmp
                'If reader("good_type_sys_id") = Config.Kasbi04_ID Then
                '    lblCaptionNumbers.Text = "�� �������/���/���/��:"
                sTmp = Trim(reader("marka_cp_out"))
                txtNewMarkaCPIn.Text = Trim(reader("marka_cp_in"))
                txtNewMarkaCPOut.Text = Trim(reader("marka_cp_out"))
                s = s & " / " & sTmp
                'Else
                'txtNewMarkaCPIn.Visible = False
                'txtNewMarkaCPOut.Visible = False
                'lblCaptionCP.Visible = False
                'End If

                b = s.Length > 0
                If b Then lblNumbers.Text = s
                lblNumbers.Visible = b
                lblCaptionNumbers.Visible = b

                s = Trim(reader("marka_cto_out"))
                txtNewMarkaCTOIn.Text = Trim(reader("marka_cto_in"))
                txtNewMarkaCTOOut.Text = s
                'If reader("good_type_sys_id") = Config.Kasbi04_ID Or reader("good_type_sys_id") = 1119 Then
                sTmp = Trim(reader("marka_cto2_out"))
                txtNewMarkaCTO2In.Text = Trim(reader("marka_cto2_in"))
                txtNewMarkaCTO2Out.Text = sTmp
                s = s & " / " & sTmp
                lblCaptionMarka.Text = "����� ���/���2:"
                'Else
                '    txtNewMarkaCTO2In.Visible = False
                '    txtNewMarkaCTO2Out.Visible = False
                '    lblCaptionCTO2.Visible = False
                'End If
                b = s.Length > 0

                If b Then lblMarka.Text = s
                lblMarka.Visible = b
                lblCaptionMarka.Visible = b

                txtNewZReportIn.Text = Trim(reader("zreport_in"))
                txtNewZReportOut.Text = Trim(reader("zreport_out"))

                txtNewItogIn.Text = Trim(reader("itog_in"))
                txtNewItogOut.Text = Trim(reader("itog_out"))
                txtNewDetails.Text = Trim(reader("details"))
                txtNewInfo.Text = Trim(reader("info"))
                txtNewRepairInfo.Text = Trim(reader("repair_info"))

                's = Trim(reader("date_created"))
                'b = s.Length > 0
                'If b Then lblDateCreated.Text = s
                'lblDateCreated.Visible = b
                'lblCaptionDateCreated.Visible = b

                's = Trim(reader("worker"))
                'b = s.Length > 0
                'If b Then lblWorker.Text = s
                'lblWorker.Visible = b
                'lblCaptionWorker.Visible = b

                s = Trim(reader("set_place"))
                b = s.Length > 0
                If b Then lblSetPlace.Text = s
                lblSetPlace.Visible = b
                lblCaptionSetPlace.Visible = b

                CurrentCustomer = CInt(IIf(IsDBNull(reader("owner_sys_id")), 0, reader("owner_sys_id")))
                'If CurrentCustomer = 0 And Not IsDBNull(reader("customer_sys_id")) Then
                '    CurrentCustomer = CInt(reader("customer_sys_id"))
                'End If
                'b = CurrentCustomer > 0 And Not IsDBNull(reader("owner_sys_id"))
                'If b Then
                '    s = Trim(reader("current_Owner"))
                '    lnkOwner.Text = s
                '    lnkOwner.NavigateUrl = "Customer.aspx?" & CurrentCustomer & "&" & iCash
                'End If
                Parameters.Value = CurrentCustomer
                'lblOwner.Visible = b
                'lnkOwner.Visible = b
                ''            lblCash.NavigateUrl = "CashOwners.aspx?" & iCash & "&cashowner=" & CurrentCustomer
                'lblCash.NavigateUrl = "Repair1.aspx?" & iCash

                's = Trim(reader("sale"))
                'b = s.Length > 0
                'If b Then
                '    lblSale.Text = s
                '    lblSale.NavigateUrl = "CustomerSales.aspx?" & reader("customer_sys_id")
                'End If
                'lblCaptionSale.Visible = b

                lblRepairDateIn.Text = Format(reader("repairdate_in"), "dd.MM.yyyy HH:mm")

                If IsDBNull(reader("repairdate_out")) Then
                    tbxRepairDateOut.Text = DateTime.Now.ToShortDateString()
                Else
                    tbxRepairDateOut.Text = CDate(reader("repairdate_out")).ToShortDateString()
                End If

                Try
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxWorkNotCall"), CheckBox).Checked = reader("workNotCall")
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxGarantia"), CheckBox).Checked = reader("garantia_repair")
                Catch
                    msg.Text = "������ �������� ���������� � ������!<br>" & Err.Description

                End Try

                Try
                    lstWorker.Items.FindByValue(reader("executor_id")).Selected = True
                Catch
                    lstWorker.SelectedIndex = 0
                    'msg.Text = "������ �������� ���������� � ������!<br>" & Err.Description
                End Try

                If Not IsDBNull(reader("garantia")) Then
                    lblGarantia.Text = reader("garantia")
                End If
                If IsDBNull(reader("support")) OrElse reader("support") = 0 Then
                    lblSupport.Text = "�� �������� ������� �� ��"
                    lblSupport.Enabled = False
                    imgSupport.Visible = False
                Else
                    If reader("support") = "1" Then
                        If reader("stateTO") = "0" Or reader("stateTO") = "1" Or reader("stateTO") = "4" Then
                            lblSupport.Text = "��������� �� ��"

                        ElseIf reader("stateTO") = "6" Then
                            lblSupport.Text = "�� ��������������"

                        ElseIf reader("stateTO") = "5" Then
                            lblSupport.Text = "��������� � �������"
                        End If

                        imgSupport.Visible = True
                        'imgSupport.NavigateUrl = "Support.aspx?" & reader("owner_sys_id")
                    Else
                        If reader("stateTO") = "2" Then
                            lblSupport.Text = "���� � ��"
                        ElseIf reader("stateTO") = "3" Then
                            lblSupport.Text = "���� � �� � � ����"
                        End If

                        imgSupport.Visible = False
                    End If

                End If

                'If IsDBNull(reader("alert")) OrElse Trim(reader("alert")).Length = 0 Then
                '    imgAlert.Visible = False
                'Else
                '    imgAlert.Visible = True
                '    imgAlert.ToolTip = reader("alert")
                'End If

                If Not IsDBNull(reader("akt")) Then
                    txtNewAkt.Text = reader("akt")
                End If

                reader.Close()
                ShowRepairImage()
            Catch
                msg.Text = "������ �������� ���������� � ������!<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try
            RecalcCost(Nothing, Nothing)
        End Sub

        Sub LoadRepairInfo()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet


            Try
                cmd = New SqlClient.SqlCommand("get_repair_info")
                cmd.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                grdDetails.DataSource = ds
                grdDetails.DataKeyField = "detail_id"
                grdDetails.DataBind()
            Catch
                msg.Text = "������ �������� ���������� � ������!<br>" & Err.Description
                Exit Sub
            End Try
        End Sub

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
                msgNew.Text = "������ ������������ �������!<br>" & Err.Description
                Exit Sub
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
                            s = s & "���: " & sTmp & "<br>"
                        End If

                        sTmp = .Item("registration")
                        If sTmp.Length > 0 Then
                            s = s & sTmp & "<br>"
                        End If

                        sTmp = "�� " & .Item("tax_inspection")
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
                msg.Text = Err.Description
            End Try
            GetInfo = s
        End Function

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
                lstCustomers.Items.Insert(0, New ListItem(ClearString, "0"))

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
                msg.Text = Err.Description
            End Try
        End Sub

        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text

            If Trim(str).Length = 0 Then LoadCustomerList() : Exit Sub
            If str.IndexOf("'") > -1 Then Exit Sub
            Dim s$ = " (customer_name like '%" & str & "%')"
            LoadCustomerList(s)
            Session("CustFilter") = s
        End Sub

        Sub ShowRepairImage()
            Try
                imgRepair.Visible = dbSQL.ExecuteScalar("select dbo.udf_repair(" & iCash & ")") > 0
            Catch

            End Try
        End Sub

        Sub RecalcCost(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim j%
            Dim Price, Cost_Service, TotalSum, NormaHour, Quantity As Double
            Price = 0
            Cost_Service = 0
            TotalSum = 0
            NormaHour = 0
            Dim Details$ = ""
            Dim Works$ = ""
            Dim WorkNotCall As Boolean = False
            Dim Garantia As Boolean = False

            WorkNotCall = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxWorkNotCall"), CheckBox).Checked
            Garantia = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxGarantia"), CheckBox).Checked
            For j = 0 To grdDetails.Items.Count - 1
                Try
                    Quantity = CDbl(CType(grdDetails.Items(j).FindControl("lblQuantity"), Label).Text)
                Catch
                    Quantity = CDbl(CType(grdDetails.Items(j).FindControl("txtQuantity"), TextBox).Text)
                End Try

                Price = Price + Quantity * CDbl(CType(grdDetails.Items(j).FindControl("lblPrice"), Label).Text)
                NormaHour = NormaHour + Quantity * CDbl(CType(grdDetails.Items(j).FindControl("lblNormaHour"), Label).Text)

                If WorkNotCall = True Then
                    Cost_Service = Cost_Service + 0
                    TotalSum = TotalSum + Quantity * CDbl(CType(grdDetails.Items(j).FindControl("lblPrice"), Label).Text)
                Else
                    Cost_Service = Cost_Service + Quantity * CDbl(CType(grdDetails.Items(j).FindControl("lblCostService"), Label).Text)
                    TotalSum = TotalSum + Quantity * CDbl(CType(grdDetails.Items(j).FindControl("lblTotalSum"), Label).Text)
                End If

                If (CType(grdDetails.Items(j).FindControl("cbxIsDetail"), CheckBox).Checked) Then
                    Details &= "," & CType(grdDetails.Items(j).FindControl("lblName"), Label).Text.Trim()
                    If (CDbl(CType(grdDetails.Items(j).FindControl("lblCostService"), Label).Text) > 0) Then
                        If WorkNotCall = False Then
                            Works &= ",������ " & CType(grdDetails.Items(j).FindControl("lblName"), Label).Text.Trim()
                        End If
                    End If
                Else
                    If WorkNotCall = False Then
                        Works &= "," & CType(grdDetails.Items(j).FindControl("lblName"), Label).Text.Trim()
                    End If
                End If
            Next
            If Garantia = 0 Then
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalPrice"), Label).Text = CStr(Price)
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalCostService"), Label).Text = CStr(Cost_Service)
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalAllSum"), Label).Text = CStr(TotalSum)
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalNormaHour"), Label).Text = CStr(NormaHour)
            Else
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalPrice"), Label).Text = "0"
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalCostService"), Label).Text = "0"
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalAllSum"), Label).Text = "0"
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalNormaHour"), Label).Text = "0"
            End If
            txtNewDetails.Text = Details.TrimStart(",")
            txtNewInfo.Text = Works.TrimStart(",")
        End Sub

        Private Sub grdDetails_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdDetails.ItemDataBound
            If e.Item.ItemType = ListItemType.Footer Then
                Dim lst As DropDownList = CType(e.Item.FindControl("lstAddDetail"), DropDownList)
                Dim cmd As SqlClient.SqlCommand
                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet

                Try
                    cmd = New SqlClient.SqlCommand("get_free_detail_for_repair")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
                    adapt = dbSQL.GetDataAdapter(cmd)
                    ds = New DataSet
                    adapt.Fill(ds)
                    lst.DataSource = ds.Tables(0).DefaultView
                    lst.DataTextField = "name"
                    lst.DataValueField = "detail_id"
                    lst.DataBind()
                    lst.Items.Insert(0, New ListItem(ClearString, "0"))
                Catch
                    msg.Text = "������ �������� ������ ������� � �����!<br>" & Err.Description
                    Exit Sub
                End Try
                CType(e.Item.FindControl("lblTotalPrice"), Label).Text = "0"
                CType(e.Item.FindControl("lblTotalCostService"), Label).Text = "0"
                CType(e.Item.FindControl("lblTotalAllSum"), Label).Text = "0"
                CType(e.Item.FindControl("lblTotalNormaHour"), Label).Text = "0"

            ElseIf e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("cmdDelete"), ImageButton).Attributes.Add("onclick", "return confirm('�� ������������� ������ ������� ������ �� �������?');")
            End If



        End Sub

        Sub LoadDetailInfo(ByVal sender As Object, ByVal e As System.EventArgs)
            Dim lst As DropDownList = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lstAddDetail"), DropDownList)
            Dim reader As SqlClient.SqlDataReader
            If lst.SelectedIndex > 0 Then
                Try
                    reader = dbSQL.GetReader("select * from details where detail_id=" & lst.SelectedValue)
                    If Not reader.Read Then
                        msg.Text = "������ �������� ���������� � �������!<br>"
                        Exit Sub
                    End If
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("txtAddQuantity"), TextBox).Text = "1"
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddPrice"), Label).Text = CStr(IIf(IsDBNull(reader("price")), 0, reader("price")))
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddCostService"), Label).Text = CStr(IIf(IsDBNull(reader("cost_service")), 0, reader("cost_service")))
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalSum"), Label).Text = CStr(IIf(IsDBNull(reader("total_sum")), 0, reader("total_sum")))
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalNormaHour"), Label).Text = CStr(IIf(IsDBNull(reader("norma_hour")), 0, reader("norma_hour")))
                    reader.Close()
                Catch
                    msg.Text = "������ �������� ���������� � �������!<br>" & Err.Description
                    reader.Close()
                    Exit Sub
                End Try
            Else
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("txtAddQuantity"), TextBox).Text = ""
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddPrice"), Label).Text = ""
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddCostService"), Label).Text = ""
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalSum"), Label).Text = ""
                CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalNormaHour"), Label).Text = ""
            End If
        End Sub

        Private Sub grdDetails_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.ItemCommand
            If e.CommandName = "AddDetail" Then
                Dim cmd As SqlClient.SqlCommand
                Dim price As Double, cost_service As Double, total_sum As Double, norma_hour As Double, quantity As Integer
                Dim iDetailID As Integer
                Try
                    iDetailID = CType(e.Item.FindControl("lstAddDetail"), DropDownList).SelectedItem.Value()
                    If iDetailID <= 0 Then
                        Exit Sub
                    End If
                    quantity = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("txtAddQuantity"), TextBox).Text
                    price = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddPrice"), Label).Text
                    cost_service = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddCostService"), Label).Text
                    total_sum = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalSum"), Label).Text
                    norma_hour = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalNormaHour"), Label).Text

                    cmd = New SqlClient.SqlCommand("add_detail_to_repair")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
                    cmd.Parameters.AddWithValue("@pi_price", price)
                    cmd.Parameters.AddWithValue("@pi_quantity", quantity)
                    cmd.Parameters.AddWithValue("@pi_cost_service", cost_service)
                    cmd.Parameters.AddWithValue("@pi_total_sum", total_sum)
                    cmd.Parameters.AddWithValue("@pi_norma_hour", norma_hour)
                    cmd.Parameters.AddWithValue("@pi_detail_id", iDetailID)
                    dbSQL.Execute(cmd)

                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("txtAddQuantity"), TextBox).Text = ""
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddPrice"), Label).Text = ""
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddCostService"), Label).Text = ""
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalSum"), Label).Text = ""
                    CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblAddTotalNormaHour"), Label).Text = ""

                Catch
                    msg.Text = "������ ���������� ������ � ������!<br>" & Err.Description
                    Exit Sub
                End Try
                LoadRepairInfo()
                RecalcCost(Nothing, Nothing)
            End If
        End Sub

        Private Sub grdDetails_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.EditCommand
            grdDetails.EditItemIndex = CInt(e.Item.ItemIndex)
            LoadRepairInfo()
        End Sub

        Private Sub grdDetails_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.DeleteCommand
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("remove_detail_from_repair")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
                cmd.Parameters.AddWithValue("@pi_detail_id", grdDetails.DataKeys(e.Item.ItemIndex))
                dbSQL.Execute(cmd)
            Catch
                If Err.Number = 1 Then
                    msg.Text = "��������� ������ ������ �������!"
                Else
                    msg.Text = "������ �������� ������!<br>" & Err.Description
                End If
            End Try
            grdDetails.EditItemIndex = -1
            LoadRepairInfo()
        End Sub

        Private Sub grdDetails_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.CancelCommand
            grdDetails.EditItemIndex = -1
            LoadRepairInfo()
        End Sub

        Private Sub grdDetails_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdDetails.UpdateCommand
            Dim cmd As SqlClient.SqlCommand
            Dim price As Double, cost_service As Double, total_sum As Double, norma_hour As Double, quantity As Integer

            quantity = CLng(CType(e.Item.FindControl("txtQuantity"), TextBox).Text) 'CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("txtQuantity"), TextBox).Text
            price = CLng(CType(e.Item.FindControl("lblPrice"), Label).Text)
            cost_service = CLng(CType(e.Item.FindControl("lblCostService"), Label).Text)
            total_sum = CLng(CType(e.Item.FindControl("lblTotalSum"), Label).Text)
            norma_hour = CLng(CType(e.Item.FindControl("lblNormaHour"), Label).Text)
            Try
                cmd = New SqlClient.SqlCommand("add_detail_to_repair")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_hc_sys_id", iCashHistory)
                cmd.Parameters.AddWithValue("@pi_detail_id", grdDetails.DataKeys(e.Item.ItemIndex))
                cmd.Parameters.AddWithValue("@pi_price", price)
                cmd.Parameters.AddWithValue("@pi_quantity", quantity)
                cmd.Parameters.AddWithValue("@pi_cost_service", cost_service)
                cmd.Parameters.AddWithValue("@pi_total_sum", total_sum)
                cmd.Parameters.AddWithValue("@pi_norma_hour", norma_hour)
                dbSQL.Execute(cmd)
            Catch
                msg.Text = "������ ���������� ������!<br>" & Err.Description
            End Try
            grdDetails.EditItemIndex = -1
            LoadRepairInfo()
        End Sub

        Private Sub lstCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstCustomers.SelectedIndexChanged

            If lstCustomers.SelectedItem.Value > 0 Then
                lblCustInfo.Text = "<br>" & GetInfo(lstCustomers.SelectedItem.Value)
            Else
                lblCustInfo.Text = ""
            End If
            Session("Customer") = lstCustomers.SelectedItem.Value
        End Sub

        Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnCancel.Click
            Response.Redirect(GetAbsoluteUrl("~/Repair.aspx?" & iCash))
        End Sub

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
            iCash = GetPageParam("cash")
            iCashHistory = GetPageParam("hc")


            Dim iCost As Long
            Try
                iCost = CLng(CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("lblTotalAllSum"), Label).Text)
            Catch
                msgNew.Text = "������� ��������� ����� �������"
                Exit Sub
            End Try

            Dim WorkNotCall As Boolean = False
            Dim Garantia As Boolean = False

            WorkNotCall = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxWorkNotCall"), CheckBox).Checked
            Garantia = CType(grdDetails.Controls.Item(0).Controls.Item(grdDetails.Items.Count + 1).FindControl("cbxGarantia"), CheckBox).Checked

            Dim cmd As SqlClient.SqlCommand
            'CurrentCustomer = Parameters.Value
            If lstCustomers.SelectedIndex <= 0 Then
                msgNew.Text = "�� ������ �� ���� ������!"
                Exit Sub
            End If
            Dim cust%
            cust = lstCustomers.SelectedItem.Value
            If cust = 0 Then
                msgNew.Text = "������ ����������� �������!"
                Exit Sub
            End If

            If lstWorker.SelectedIndex <= 0 Then
                msgNew.Text = "�������� ����������� �������!"
                Exit Sub
            End If

            Dim d1, d2 As DateTime
            Dim s1 As String()
            If tbxRepairDateOut.Text = String.Empty Then
                d2 = DateTime.MinValue
            Else
                d2 = CDate(tbxRepairDateOut.Text)
            End If

            If chbRepairDateInEdit.Checked = False Then
                Dim s$ = lblRepairDateIn.Text.Trim
                s1$ = s.Split("./".ToCharArray())
                'd1 = New Date(CInt(s1.GetValue(2).SubString(0, 4)), CInt(s1.GetValue(1)), CInt(s1.GetValue(0)))
                d1 = CDate(lblRepairDateIn.Text.Trim)
            Else
                d1 = CDate(tbxRepairDateIn.Text)
            End If
            If d2 > DateTime.MinValue Then
                If (CDate(d1.ToShortDateString()) > d2) Then
                    msgNew.Text = "���� ������ �� ������� ������ ���� ������� � ������!"
                    Exit Sub
                End If
            End If

            Try
                cmd = New SqlClient.SqlCommand("update_repair")
                cmd.Parameters.AddWithValue("@pi_sys_id", iCashHistory)
                cmd.Parameters.AddWithValue("@pi_good_sys_id", iCash)
                cmd.Parameters.AddWithValue("@pi_owner_sys_id", cust)
                cmd.Parameters.AddWithValue("@pi_date_in", d1)
                cmd.Parameters.AddWithValue("@pi_date_out", IIf(d2 = DateTime.MinValue, DBNull.Value, d2)) 'IIf(chkDateOut.Checked, d2, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_marka_cto_in", txtNewMarkaCTOIn.Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto_out", txtNewMarkaCTOOut.Text)
                cmd.Parameters.AddWithValue("@pi_marka_pzu_in", txtNewMarkaPZUIn.Text)
                cmd.Parameters.AddWithValue("@pi_marka_pzu_out", txtNewMarkaPZUOut.Text)
                cmd.Parameters.AddWithValue("@pi_marka_mfp_in", txtNewMarkaMFPIn.Text)
                cmd.Parameters.AddWithValue("@pi_marka_mfp_out", txtNewMarkaMFPOut.Text)
                cmd.Parameters.AddWithValue("@pi_marka_reestr_in", txtNewMarkaReestrIn.Text)
                cmd.Parameters.AddWithValue("@pi_marka_reestr_out", txtNewMarkaReestrOut.Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto2_in", txtNewMarkaCTO2In.Text)
                cmd.Parameters.AddWithValue("@pi_marka_cto2_out", txtNewMarkaCTO2Out.Text)
                cmd.Parameters.AddWithValue("@pi_marka_cp_in", txtNewMarkaCPIn.Text)
                cmd.Parameters.AddWithValue("@pi_marka_cp_out", txtNewMarkaCPOut.Text)
                cmd.Parameters.AddWithValue("@pi_zreport_in", txtNewZReportIn.Text)
                cmd.Parameters.AddWithValue("@pi_zreport_out", txtNewZReportOut.Text)
                cmd.Parameters.AddWithValue("@pi_itog_in", txtNewItogIn.Text)
                cmd.Parameters.AddWithValue("@pi_itog_out", txtNewItogOut.Text)
                cmd.Parameters.AddWithValue("@pi_details", txtNewDetails.Text)
                cmd.Parameters.AddWithValue("@pi_akt", txtNewAkt.Text)
                cmd.Parameters.AddWithValue("@pi_summa", iCost)
                cmd.Parameters.AddWithValue("@pi_info", txtNewInfo.Text)
                cmd.Parameters.AddWithValue("@pi_repair_info", txtNewRepairInfo.Text)
                cmd.Parameters.AddWithValue("@pi_executor", lstWorker.SelectedItem.Value)
                cmd.Parameters.AddWithValue("@pi_workNotCall", WorkNotCall)
                cmd.Parameters.AddWithValue("@pi_garantia", Garantia)
                cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                cmd.CommandType = CommandType.StoredProcedure
                dbSQL.Execute(cmd)

                msgNew.Text = "������ � ������� ������� ���������!"
                txtNewMarkaCTOIn.Text = ""
                txtNewMarkaCTOOut.Text = ""
                txtNewMarkaPZUIn.Text = ""
                txtNewMarkaPZUOut.Text = ""
                txtNewMarkaMFPIn.Text = ""
                txtNewMarkaMFPOut.Text = ""
                txtNewZReportIn.Text = ""
                txtNewZReportOut.Text = ""
                txtNewItogIn.Text = ""
                txtNewItogOut.Text = ""
                txtNewDetails.Text = ""
                txtNewAkt.Text = ""
                txtNewInfo.Text = ""
                txtNewRepairInfo.Text = ""
                ShowRepairImage()
            Catch
                msgNew.Text = "������ ���������� ���������� � �������!<br>" & Err.Description
                Exit Sub
            End Try
            Response.Redirect(GetAbsoluteUrl("~/Repair.aspx?" & iCash))
        End Sub

        Private Sub chbRepairDateInEdit_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chbRepairDateInEdit.CheckedChanged
            If chbRepairDateInEdit.Checked = True Then
                lblRepairDateIn.Visible = False 'Text = Format(reader("repairdate_in"), "dd.MM.yyyy HH:mm")
                tbxRepairDateIn.Text = CDate(lblRepairDateIn.Text).ToShortDateString()
                pnlRepairDateIn.Visible = True
            Else
                pnlRepairDateIn.Visible = False
                lblRepairDateIn.Visible = True
            End If
        End Sub
End Class

End Namespace
