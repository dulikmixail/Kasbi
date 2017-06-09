Imports System.Globalization
Imports System.Threading


Namespace Kasbi

Partial Class RepairNew2
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

    Dim sCaptionAddSupport As String = "Поставить на ТО"
    Dim CurrentCustomer%, iNewCust%
    Dim iCash%, iCashHistory%
    Dim sCaptionRemoveSupport As String = "Снять с ТО"
    Dim d As Kasbi.Documents
        Const ClearString$ = "-------"

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                iCash = Request.Params("cash")
                iCashHistory = Request.Params("hc")

            Catch
                msg.Text = "Неверный запрос"
                Exit Sub
            End Try
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
            If Not IsPostBack Then
                Session("CustFilter") = ""
                LoadExecutor()
                LoadGoodInfo()
                LoadCustomerList()
                LoadDetails()
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
                    msg.Text = "Ошибка загрузки информации о товаре!<br>"
                    Exit Sub
                End If
                lblCashType.Text = reader("good_name") & "&nbsp;&nbsp;"
                lblCash.Text = "№" & reader("num_cashregister")
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
                '    lblCaptionNumbers.Text = "СК Реестра/ПЗУ/МФП/ЦП:"
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
                If reader("good_type_sys_id") = Config.Kasbi04_ID Then
                    sTmp = Trim(reader("marka_cto2_out"))
                    txtNewMarkaCTO2In.Text = Trim(reader("marka_cto2_in"))
                    txtNewMarkaCTO2Out.Text = sTmp
                    s = s & " / " & sTmp
                    lblCaptionMarka.Text = "Марки ЦТО/ЦТО2:"
                Else
                    txtNewMarkaCTO2In.Visible = False
                    txtNewMarkaCTO2Out.Visible = False
                    lblCaptionCTO2.Visible = False
                End If
                b = s.Length > 0
                If b Then lblMarka.Text = s
                lblMarka.Visible = b
                lblCaptionMarka.Visible = b

                txtNewZReportIn.Text = Trim(reader("zreport_in"))
                txtNewZReportOut.Text = Trim(reader("zreport_out"))

                txtNewItogIn.Text = Trim(reader("itog_in"))
                txtNewItogOut.Text = Trim(reader("itog_out"))
                'txtNewDetails.Text = Trim(reader("details"))
                'txtNewInfo.Text = Trim(reader("info"))
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
                    lstWorker.Items.FindByValue(reader("executor_id")).Selected = True
                Catch
                    lstWorker.SelectedIndex = 0
                    'msg.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
                End Try

                If Not IsDBNull(reader("garantia")) Then
                    lblGarantia.Text = reader("garantia")
                End If
                If IsDBNull(reader("support")) OrElse reader("support") = 0 Then
                    lblSupport.Text = "Не заключен договор на ТО"
                    lblSupport.Enabled = False
                    imgSupport.Visible = False
                Else
                    If reader("support") = "1" Then
                        If reader("stateTO") = "0" Or reader("stateTO") = "1" Or reader("stateTO") = "4" Then
                            lblSupport.Text = "Находится на ТО"

                        ElseIf reader("stateTO") = "6" Then
                            lblSupport.Text = "ТО приостановлено"

                        ElseIf reader("stateTO") = "5" Then
                            lblSupport.Text = "Находится в ремонте"
                        End If

                        imgSupport.Visible = True
                        'imgSupport.NavigateUrl = "Support.aspx?" & reader("owner_sys_id")
                    Else
                        If reader("stateTO") = "2" Then
                            lblSupport.Text = "Снят с ТО"
                        ElseIf reader("stateTO") = "3" Then
                            lblSupport.Text = "Снят с ТО и в ИМНС"
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
                msg.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
                reader.Close()
                Exit Sub
            End Try
        End Sub


        Sub LoadDetails()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                cmd = New SqlClient.SqlCommand("get_detail_for_repair")
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                rep_details.DataSource = ds
                rep_details.DataBind()


            Catch
                msg.Text = "Ошибка загрузки информации о товаре!<br>" & Err.Description
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
                msgNew.Text = "Ошибка формирования списков!<br>" & Err.Description
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

        Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnSave.Click
            iCash = GetPageParam("cash")
            iCashHistory = GetPageParam("hc")

            Dim price = GetPageParam("price")
            Dim quantity = GetPageParam("quantity")
            Dim cost_service = GetPageParam("cost_service")
            Dim total_sum = GetPageParam("total_sum")
            Dim norma_hour = GetPageParam("norma_hour")
            Dim iDetailID = GetPageParam("iDetailID")

            Dim cmd As SqlClient.SqlCommand
            cmd = New SqlClient.SqlCommand("update_repair")

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



            Try

            Catch
            End Try
        End Sub

End Class

End Namespace


