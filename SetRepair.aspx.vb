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
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim query
        Dim icash
        Dim customer
        Dim repare_in_info

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            icash = Request.Params(0)

            query = dbSQL.ExecuteScalar("SELECT num_cashregister FROM good WHERE good_sys_id='" & icash & "'")

            lblCash.Text = "Принятие в ремонт кассового аппарата <br><b>'" & query & "'</b>"

            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapter = dbSQL.GetDataAdapter("select * from repair_bads")
                adapter.Fill(ds)
                grdRepairBads.DataSource = ds.Tables(0).DefaultView
                grdRepairBads.DataKeyField = "sys_id"
                grdRepairBads.DataBind()
            Catch
            End Try
        End Sub

        Function GetNewAktNumber() As String
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            'новый номер договора
            Try
                cmd = New SqlClient.SqlCommand("get_next_repair_akt")
                cmd.Parameters.AddWithValue("@good_sys_id", icash)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)

                Dim s
                Dim num_cashregister
                s = ds.Tables(0).Rows(0).Item("num_repairs")
                num_cashregister = ds.Tables(0).Rows(0).Item("num_cashregister")
                num_cashregister = Trim(num_cashregister)
                s = s + 1

                GetNewAktNumber = num_cashregister & "/" & Date.Now.Month & "/" & s
            Catch
                Return ""
            End Try
        End Function

        Protected Sub btnSetRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetRepair.Click
            Dim cmd As SqlClient.SqlCommand

            Dim akt$ = GetNewAktNumber()
            If akt Is Nothing Then
                akt = ""
            End If

            customer = dbSQL.ExecuteScalar("select top 1 cast(owner_sys_id as nvarchar) from cash_history where good_sys_id=" & icash & " order by sys_id desc")
            If customer = "" Or customer = 0 Or customer Is DBNull.Value Then
                customer = dbSQL.ExecuteScalar("SELECT sale.customer_sys_id FROM sale INNER JOIN good ON sale.sale_sys_id = good.sale_sys_id WHERE  (good.good_sys_id = " & icash & ")")
            End If

            Dim j = 0
            For j = 0 To grdRepairBads.Items.Count - 1
                If CType(grdRepairBads.Items(j).FindControl("cbxSelect"), CheckBox).Checked Then
                    repare_in_info &= grdRepairBads.DataKeys(grdRepairBads.Items(j).ItemIndex) & ","
                End If
            Next

            cmd = New SqlClient.SqlCommand("new_repair")
            cmd.Parameters.AddWithValue("@pi_good_sys_id", icash)
            cmd.Parameters.AddWithValue("@pi_owner_sys_id", customer)
            cmd.Parameters.AddWithValue("@pi_date_in", Now)
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
            cmd.Parameters.AddWithValue("@pi_executor", CurrentUser.sys_id)
            cmd.Parameters.AddWithValue("@pi_repair_in", 1)
            cmd.Parameters.AddWithValue("@updateUserID", CurrentUser.sys_id)
            cmd.Parameters.AddWithValue("@repare_in_info", repare_in_info)

            cmd.CommandType = CommandType.StoredProcedure
            dbSQL.Execute(cmd)

            query = dbSQL.ExecuteScalar("Update good SET inrepair='1' WHERE good_sys_id='" & icash & "'")

            query = dbSQL.ExecuteScalar("SELECT top 1 sys_id FROM cash_history WHERE good_sys_id='" & icash & "' AND state='5' ORDER BY sys_id DESC")
            btnSetRepair.Enabled = False

            'Сохраняем информацию на экспорт
            'находим номер ККМ
            Dim num_cashregister = dbSQL.ExecuteScalar("SELECT num_cashregister FROM good WHERE good_sys_id='" & icash & "'")
            'находим УНН клиента
            Dim customer_unn = dbSQL.ExecuteScalar("SELECT unn FROM customer WHERE customer_sys_id='" & customer & "'")

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

            Response.Redirect(GetAbsoluteUrl("~/documents.aspx?t=31&c=" & customer & "&g=" & icash & "&h=" & query))
        End Sub


    End Class
End Namespace
