Imports System.Data.SqlClient
Imports AjaxControlToolkit
Imports Microsoft.Ajax.Utilities
Imports Service

Namespace Kasbi
    Partial Class SetRepair
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
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim query
        Dim icash
        Dim customer As String = ""
        Dim _currentStateRepair As Integer = 0
        Dim _nextStateRepair As Integer = 0
        Const ClearString$ = "-------"
        Private ReadOnly _serviceCustomer As ServiceCustomer = New ServiceCustomer()
        Private ReadOnly _serviceGood As ServiceGood = New ServiceGood()
        Private ReadOnly _serviceRepair As ServiceRepair = New ServiceRepair()
        Dim ReadOnly _validTelCode As List(Of String) = New List(Of String) From {"25", "29", "33", "44"}

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            icash = Request.Params(0)
            customer = dbSQL.ExecuteScalar(
                "select top 1 owner_sys_id from cash_history where good_sys_id=" & icash & " order by sys_id desc")

            customer = IIf(customer Is Nothing, "", customer).ToString()

            query = dbSQL.ExecuteScalar("SELECT num_cashregister FROM good WHERE good_sys_id='" & icash & "'")

            lblCash.Text = "Принятие в ремонт кассового аппарата <br><b>'" & query & "'</b>"

            _currentStateRepair = _serviceGood.GetStateRepair(icash)
            If _currentStateRepair = 10
                isNeadSKNO.Checked = True
            End If

            If Not IsPostBack Then
                _serviceCustomer.LoadCustomerList(customer, lstCustomers, lblCustInfo, lblErrors)
                Bind()
                _serviceCustomer.LoadTelephoneNotice(lstCustomers, lstTelephoneNotice, lblTelephoneNotice)
            End If
        End Sub

        Private Sub SetTxtTelephoneNotice()
            If lstTelephoneNotice.Items.Count <> 0
                txtTelephoneNotice.Text = lstTelephoneNotice.SelectedItem.Text
            Else
                txtTelephoneNotice.Text = String.Empty
            End If
        End Sub

        Private Sub Bind()
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapter =
                    dbSQL.GetDataAdapter(
                        "select *, LTRIM(STR(price_from, 10, 2)) AS price_from_fix, LTRIM(STR(price_to, 10, 2)) AS price_to_fix from repair_bads WHERE deleted <> 1 order by name")
                adapter.Fill(ds)
                grdRepairBads.DataSource = ds.Tables(0).DefaultView
                grdRepairBads.DataKeyField = "repair_bads_sys_id"
                grdRepairBads.DataBind()
            Catch
            End Try
        End Sub

        Protected Sub btnSetRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetRepair.Click
            Dim ds As DataSet = New DataSet()
            Dim adapt As SqlClient.SqlDataAdapter

            Dim repairBadsList As String = String.Empty,
                repairBadsInfo As String = String.Empty,
                repairBadsSumItog As String = String.Empty,
                minItog As Double = 0,
                maxItog As Double = 0,
                priceFrom As Double = 0,
                priceTo As Double = 0,
                showMaxItog As Boolean = True

            Dim indexFotter As Integer = grdRepairBads.Controls(0).Controls.Count - 1
            Dim txtName As String =
                    CType(grdRepairBads.Controls(0).Controls(indexFotter).FindControl("txtName"), TextBox).Text
            Dim txtPriceFrom As String =
                    CType(grdRepairBads.Controls(0).Controls(indexFotter).FindControl("txtPriceFrom"), TextBox).Text
            Dim txtPriceTo As String =
                    CType(grdRepairBads.Controls(0).Controls(indexFotter).FindControl("txtPriceTo"), TextBox).Text

            For j = 0 To grdRepairBads.Items.Count - 1
                If CType(grdRepairBads.Items(j).FindControl("cbxSelect"), CheckBox).Checked Then
                    repairBadsList &= grdRepairBads.DataKeys(grdRepairBads.Items(j).ItemIndex).ToString() & ","
                End If
            Next

            If repairBadsList.Length <> 0
                repairBadsList = repairBadsList.Substring(0, repairBadsList.Length - 1)
            End If

            If _
                (dbSQL.ExecuteScalar(
                    "SELECT COUNT(*) FROM dbo.good WHERE inrepair = 1 AND good_sys_id = '" & icash & "'") <> 0)
                lblErrors.Text = "Кассовый аппарат уже принят или находится в ремонте"
            ElseIf lstCustomers.SelectedItem.Text = ClearString
                lblErrors.Text = "Выберите плательщика ремонта!"
            ElseIf String.IsNullOrEmpty(txtTelephoneNotice.Text)
                lblErrors.Text = "Вы не ввели номер телефона оповещения"
            ElseIf Not _validTelCode.Contains(txtTelephoneNotice.Text.Substring(0, 2))
                lblErrors.Text = "Введен некорректный мобильный телефон!"
            ElseIf String.IsNullOrEmpty(repairBadsList) And String.IsNullOrEmpty(txtName) And Not isNeadSKNO.Checked
                lblErrors.Text = "Вы не выбрали или не ввели неисправность"
            ElseIf _
                NOT _
                _serviceCustomer.AddCustomerTelNotice(Convert.ToInt32(lstCustomers.SelectedValue),
                                                      txtTelephoneNotice.Text)
                lblErrors.Text = "Ошибка при добавлении телефона оповещения"
            Else
                Dim cmd As SqlClient.SqlCommand
                Dim akt$ = _serviceRepair.GetNewAktNumberByGoodId(icash)
                If akt Is Nothing Then
                    akt = ""
                End If

                If Not String.IsNullOrEmpty(Trim(txtName))
                    Try
                        cmd = New SqlCommand("insert_repair_bads")
                        With cmd.Parameters
                            .AddWithValue("@pi_name", txtName)
                            .AddWithValue("@pi_price_from", Replace(txtPriceFrom, ",", "."))
                            .AddWithValue("@pi_price_to", Replace(txtPriceTo, ",", "."))
                            .AddWithValue("@pi_deleted", True)
                        End With
                        cmd.CommandType = CommandType.StoredProcedure
                        adapt = dbSQL.GetDataAdapter(cmd)
                        ds = New DataSet
                        adapt.Fill(ds)
                        If Not String.IsNullOrEmpty(repairBadsList)
                            repairBadsList &= ","
                        End If
                        repairBadsList &= Convert.ToInt32(ds.Tables(0).Rows(0).Item("return_value"))
                    Catch
                        lblErrors.Text = "Ошибка добавления дрйгой неисправности<br>" & Err.Description
                    End Try
                End If

                Dim j = 0

                If String.IsNullOrEmpty(repairBadsList)
                    repairBadsSumItog = "0 руб."
                Else
                    ds = New DataSet()
                    adapt = dbSQL.GetDataAdapter(
                        "SELECT * FROM repair_bads WHERE repair_bads_sys_id IN (" & repairBadsList &
                        ") ORDER BY name")
                    adapt.Fill(ds, "repair_bads_info")

                    Dim dt As DataTable = ds.Tables("repair_bads_info")

                    For Each dr As DataRow In dt.Rows
                        If IsDBNull(dr("price_from"))
                            priceFrom = 0
                        Else
                            priceFrom = dr("price_from")
                        End If
                        If IsDBNull(dr("price_to"))
                            priceTo = 0
                        Else
                            priceTo = dr("price_to")
                        End If
                        If priceFrom > 0 And priceTo <= 0
                            showMaxItog = False
                        End If
                        minItog += priceFrom
                        maxItog += priceTo

                        Dim nameRepairBad As String = dr("name").ToString()
                        nameRepairBad = nameRepairBad.Trim()
                        If priceFrom <= 0 And priceTo <= 0
                            repairBadsInfo &= nameRepairBad & " (0 руб.); "
                        Else
                            repairBadsInfo &= nameRepairBad & " (от " & priceFrom &
                                              IIf(priceTo > 0, " до " & priceTo, "").ToString() & " руб.); "
                        End If

                    Next


                End If
                If repairBadsInfo.Length > 1
                    repairBadsInfo = repairBadsInfo.Substring(0, repairBadsInfo.Length - 2)
                    repairBadsInfo &= ". "
                End If


                If minItog > 0 And maxItog > 0
                    repairBadsSumItog = "от " & minItog &
                                        IIf(showMaxItog, " до " & maxItog, "").ToString() & " руб."
                Else
                    repairBadsSumItog = "0 руб."
                End If

                If isNeadSKNO.Checked
                    repairBadsInfo &= "Пометка: Необходимо установить СКНО."
                End If

                cmd = New SqlClient.SqlCommand("new_repair_and_repair_bads_info")
                cmd.Parameters.AddWithValue("@pi_good_sys_id", icash)
                cmd.Parameters.AddWithValue("@pi_owner_sys_id", lstCustomers.SelectedValue)
                cmd.Parameters.AddWithValue("@pi_date_in", DBNull.Value)
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
                cmd.Parameters.AddWithValue("@pi_executor", DBNull.Value)
                cmd.Parameters.AddWithValue("@pi_repair_in", 1)
                cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
                cmd.Parameters.AddWithValue("@repare_in_info", "")
                cmd.Parameters.AddWithValue("@pi_repair_bads_info", repairBadsInfo)
                cmd.Parameters.AddWithValue("@pi_repair_bads_sum_itog", repairBadsSumItog)

                cmd.Parameters.AddWithValue("@pi_tel_notice",
                                            IIf(txtTelephoneNotice.Text <> String.Empty,
                                                "+375" & txtTelephoneNotice.Text,
                                                ""))

                cmd.Parameters.AddWithValue("@pi_repair_bads_list", repairBadsList)
                cmd.Parameters.AddWithValue("@pi_nead_SKNO", isNeadSKNO.Checked)

                cmd.Parameters.AddWithValue("@pi_reception_date", Now)
                cmd.Parameters.AddWithValue("@pi_receptionist_sys_id", CurrentUser.sys_id)

                cmd.CommandType = CommandType.StoredProcedure
                dbSQL.Execute(cmd)

                query = dbSQL.ExecuteScalar("Update good SET inrepair='1' WHERE good_sys_id='" & icash & "'")

                query =
                    dbSQL.ExecuteScalar(
                        "SELECT top 1 sys_id FROM cash_history WHERE good_sys_id='" & icash &
                        "' AND state='5' ORDER BY sys_id DESC")

                'Сохраняем приемшика и дату принятия 


                btnSetRepair.Enabled = False


                'Сохраняем информацию на экспорт
                'находим номер ККМ
                Dim num_cashregister =
                        dbSQL.ExecuteScalar("SELECT num_cashregister FROM good WHERE good_sys_id='" & icash & "'")
                'находим УНН клиента
                Dim customer_unn =
                        dbSQL.ExecuteScalar("SELECT unn FROM customer WHERE customer_sys_id='" & customer & "'")

                Dim export_content = Trim(customer_unn) & ";" & Trim(num_cashregister) & ";" & Now & vbCrLf

                Dim content_temp
                Dim file_open As IO.StreamReader
                Dim i = 1
                file_open = IO.File.OpenText(Server.MapPath("XML/new_repair.csv"))
                While Not file_open.EndOfStream
                    i = i + 1
                    content_temp = file_open.ReadLine()
                    If i < 20 Then
                        export_content &= content_temp & vbCrLf
                    End If
                End While
                file_open.Close()
                Try
                    Dim file_save As IO.StreamWriter
                    file_save = IO.File.CreateText(Server.MapPath("XML/new_repair.csv"))
                    file_save.Write(export_content)
                    file_save.Close()
                Catch ex As Exception
                End Try

                Dim defectAktUrl As String = "documents.aspx?t=31&c=" & lstCustomers.SelectedValue.ToString() & "&g=" &
                                             icash.ToString() & "&h=" & query.ToString()
                Dim strRequest$ = "<script>window.open('" & defectAktUrl & "','_new','');</script>"

                Page.ClientScript.RegisterStartupScript(Me.GetType(), "PopupScript", strRequest$)

                lnkDefectAkt.NavigateUrl = GetAbsoluteUrl(defectAktUrl)
                lnkDefectAkt.Visible = True

                If isNeadSKNO.Checked
                    _serviceGood.SetStateRepair(icash, 11)
                Else
                    _serviceGood.SetStateRepair(icash, 1)
                End If
'                Response.Redirect(GetAbsoluteUrl("~/documents.aspx?t=31&c=" & customer & "&g=" & icash & "&h=" & query))
            End If
        End Sub

        Protected Sub lstTelephoneNotice_SelectedIndexChanged(sender As Object, e As EventArgs) _
            Handles lstTelephoneNotice.TextChanged

            SetTxtTelephoneNotice()
        End Sub

        Private Sub lstCustomers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lstCustomers.SelectedIndexChanged

            If lstCustomers.SelectedItem.Value > 0 Then
                lblCustInfo.Text = "<br>" &
                                   _serviceCustomer.GetInfo(Convert.ToInt32(lstCustomers.SelectedItem.Value),
                                                            lstCustomers, lblErrors)
            Else
                lblCustInfo.Text = ""
            End If
            _serviceCustomer.LoadTelephoneNotice(lstCustomers, lstTelephoneNotice, lblTelephoneNotice)
        End Sub

        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
            Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text

            If Trim(str).Length = 0 Then _
                _serviceCustomer.LoadCustomerList(customer, lstCustomers, lblCustInfo, lblErrors) : Exit Sub
            If str.IndexOf("'") > - 1 Then Exit Sub
            Dim s$ = " (customer_name like '%" & str & "%')"
            _serviceCustomer.LoadCustomerList(customer, lstCustomers, lblCustInfo, lblErrors, s)
            _serviceCustomer.LoadTelephoneNotice(lstCustomers, lstTelephoneNotice, lblTelephoneNotice)
        End Sub
    End Class
End Namespace
