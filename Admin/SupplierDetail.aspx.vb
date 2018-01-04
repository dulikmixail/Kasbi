Namespace Kasbi.Admin

    Partial Class SupplierDetail
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
        Dim SupplierID As Integer

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Const javascript = "javascript:"
            SupplierID = GetPageParam("SupplierDetailID")

            Dim sHide2$ = lstSupplierAbr.ClientID & ".style.display='"
            Dim sSetValue2$ = lstSupplierAbr.ClientID & ".value=this.options[this.selectedIndex].text;"
            btnSupplierAbr.Attributes.Add("onclick", javascript & sHide2 & "block';" & lstSupplierAbr.ClientID & ".focus();")
            txtSupplierAbr.Attributes.Add("ondblclick", javascript & sHide2 & "block';" & lstSupplierAbr.ClientID & ".focus();")
            lstSupplierAbr.Attributes.Add("onchange", javascript & sSetValue2 & sHide2 & "none';")
            lstSupplierAbr.Attributes.Add("onfocusout", javascript & sHide2 & "none';")

            If Not IsPostBack Then
                If SupplierID <> -9999 Then
                    LoadSupplier()
                End If
            End If

        End Sub

        Private Sub LoadSupplier()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            'список аббревиатур организаций
            Try
                adapt = dbSQL.GetDataAdapter("select distinct supplier_abr from supplier where supplier_abr is not null and ltrim(supplier_abr)<>'' order by supplier_abr")
                ds = New DataSet
                adapt.Fill(ds)
                lstSupplierAbr.DataSource = ds.Tables(0).DefaultView
                lstSupplierAbr.DataTextField = "supplier_abr"
                lstSupplierAbr.DataValueField = "supplier_abr"
                lstSupplierAbr.DataBind()
            Catch
                msg.Text = "Ошибка формирования списка аббревиатур организаций!<br>" & Err.Description
                Exit Sub
            End Try
            Try
                adapt = dbSQL.GetDataAdapter("SELECT * FROM supplier WHERE sys_id=" & SupplierID)
                ds = New DataSet
                adapt.Fill(ds)
                txtSupplierAbr.Text = CStr(ds.Tables(0).Rows(0).Item("supplier_abr"))
                txtSupplierName.Text = CStr(ds.Tables(0).Rows(0).Item("supplier_name"))
                txtUNN.Text = CStr(ds.Tables(0).Rows(0).Item("unn")).Trim()
                txtOKPO.Text = CStr(ds.Tables(0).Rows(0).Item("okpo")).Trim()
                txtBoosLastName.Text = CStr(ds.Tables(0).Rows(0).Item("boos_last_name"))
                txtBoosFirstName.Text = CStr(ds.Tables(0).Rows(0).Item("boos_first_name"))
                txtBoosPatronymicName.Text = CStr(ds.Tables(0).Rows(0).Item("boos_patronymic_name"))
                txtCountry.Text = CStr(ds.Tables(0).Rows(0).Item("country"))
                txtCity.Text = CStr(ds.Tables(0).Rows(0).Item("city"))
                txtZipCode.Text = CStr(ds.Tables(0).Rows(0).Item("zipcode"))
                txtAddress.Text = CStr(ds.Tables(0).Rows(0).Item("address"))
                txtPhone1.Text = CStr(ds.Tables(0).Rows(0).Item("phone1"))
                txtPhone2.Text = CStr(ds.Tables(0).Rows(0).Item("phone2"))
                txtPhone3.Text = CStr(ds.Tables(0).Rows(0).Item("phone3"))
                txtPhone4.Text = CStr(ds.Tables(0).Rows(0).Item("phone4"))
                txtDogovor.Text = CStr(ds.Tables(0).Rows(0).Item("dogovor"))
                If IsDBNull(ds.Tables(0).Rows(0).Item("nadbavka")) Then
                    chkNadbavka.Checked = False
                Else
                    chkNadbavka.Checked = CBool(ds.Tables(0).Rows(0).Item("nadbavka"))
                End If
            Catch
            End Try
        End Sub

        Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
            Dim cmd As SqlClient.SqlCommand

            Try
                cmd = New SqlClient.SqlCommand("update_supplier")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sys_id", IIf(SupplierID <> -9999, SupplierID, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_supplier_abr", txtSupplierAbr.Text)
                cmd.Parameters.AddWithValue("@pi_supplier_name", txtSupplierName.Text)
                cmd.Parameters.AddWithValue("@pi_boos_last_name", txtBoosLastName.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_boos_first_name", txtBoosFirstName.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_boos_patronymic_name", txtBoosPatronymicName.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_unn", txtUNN.Text)
                cmd.Parameters.AddWithValue("@pi_okpo", txtOKPO.Text)
                cmd.Parameters.AddWithValue("@pi_country", txtCountry.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_city", txtCity.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_zipcode", txtZipCode.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_address", txtAddress.Text.Replace("'", """"))
                cmd.Parameters.AddWithValue("@pi_phone1", txtPhone1.Text)
                cmd.Parameters.AddWithValue("@pi_phone2", txtPhone2.Text)
                cmd.Parameters.AddWithValue("@pi_phone3", txtPhone3.Text)
                cmd.Parameters.AddWithValue("@pi_phone4", txtPhone4.Text)
                cmd.Parameters.AddWithValue("@pi_dogovor", txtDogovor.Text)
                cmd.Parameters.AddWithValue("@pi_nadbavka", chkNadbavka.Checked)
                dbSQL.Execute(cmd)
                Response.Redirect(GetAbsoluteUrl("~/Admin/Suppliers.aspx"))
            Catch
                lblSqlError.Visible = True
                lblSqlError.Text = Err.Description
            End Try
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdCancel.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Suppliers.aspx"))
        End Sub

    End Class

End Namespace
