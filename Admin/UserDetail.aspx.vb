Namespace Kasbi.Admin

    Partial Class UserDetail
        Inherits PageBase


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents txtLastName As System.Web.UI.WebControls.TextBox
        Protected WithEvents RegularExpressionValidator1 As System.Web.UI.WebControls.RegularExpressionValidator
        Protected WithEvents lblUpdateDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblUpdateUser As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedDate As System.Web.UI.WebControls.Label
        Protected WithEvents lblCreatedUser As System.Web.UI.WebControls.Label


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Dim UserID As Integer
        Dim UserRow As DataRow

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            UserID = GetPageParam("UserDetailID")
            If Not IsPostBack Then
                If UserID <> -9999 Then
                    LoadUser()
                    rfvPassword.Enabled = False
                    rfvCPassword.Enabled = False
                Else
                    LoadRoles(1)
                End If

            End If

            cbxIsSaler.Attributes.Add("onclick", "enable_salers(this.checked)")
            EnableControls()
        End Sub

        Private Sub EnableControls()
            Dim bIsNew As Boolean = UserRow Is Nothing

            Dim bIsAdmin As Boolean = CurrentUser.is_admin
            Dim bCurrent As Boolean = Not bIsNew AndAlso (UserRow.Item("sys_id") = CurrentUser.sys_id)

            txtFirstName.Enabled = bIsAdmin Or bCurrent
            txtLogin.Enabled = bIsAdmin Or bCurrent
            txtPassword.Enabled = bIsAdmin Or bCurrent
            txtCPassword.Enabled = bIsAdmin Or bCurrent
            txtDocument.Enabled = bIsAdmin Or bCurrent
            cbxIsAdministrator.Enabled = bIsAdmin
            cbxIsSaler.Enabled = bIsAdmin
            rlstRoles.Enabled = bIsAdmin
            cbxInactive.Enabled = bIsAdmin
            cmdEdit2.Visible = bIsAdmin Or bCurrent
        End Sub

        Private Sub LoadUser()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim role_id As Integer = -9999
            Try
                adapt = dbSQL.GetDataAdapter("SELECT * FROM employee WHERE sys_id=" & UserID)
                ds = New DataSet
                adapt.Fill(ds)

                UserRow = ds.Tables(0).Rows(0)
                txtFirstName.Text = CStr(UserRow.Item("Name"))
                txtLogin.Text = CStr(UserRow.Item("account"))
                If Not IsDBNull(UserRow.Item("document")) Then
                    txtDocument.Text = UserRow.Item("document")
                End If
                If Not IsDBNull(UserRow.Item("is_admin")) Then
                    cbxIsAdministrator.Checked = CInt(UserRow.Item("is_admin"))
                End If
                If Not IsDBNull(UserRow.Item("role_id")) Then
                    role_id = CInt(UserRow.Item("role_id"))
                End If
                If Not IsDBNull(UserRow.Item("is_saler")) Then
                    cbxIsSaler.Checked = CBool(UserRow.Item("is_saler"))
                    txtDocument.Enabled = cbxIsSaler.Checked
                Else
                    txtDocument.Enabled = False
                End If
                If Not IsDBNull(UserRow.Item("inactive")) Then
                    cbxInactive.Checked = CBool(UserRow.Item("inactive"))
                End If



                LoadRoles(role_id)
                txt_work_type.Text = CStr(UserRow.Item("work_type")).Trim


                'txt_phone.Text = CStr(UserRow.Item("phone")).Trim



                Dim arr_rule = UserRow.Item("rules")
                arr_rule = Split(arr_rule, ";")
                If arr_rule(0) = 1 Then rule1.Checked = True
                If arr_rule(1) = 1 Then rule1_1.Checked = True
                If arr_rule(2) = 1 Then rule1_2.Checked = True
                If arr_rule(3) = 1 Then rule1_3.Checked = True
                If arr_rule(4) = 1 Then rule2.Checked = True
                If arr_rule(5) = 1 Then rule2_1.Checked = True
                If arr_rule(6) = 1 Then rule2_2.Checked = True
                If arr_rule(7) = 1 Then rule2_3.Checked = True
                If arr_rule(8) = 1 Then rule2_4.Checked = True
                If arr_rule(9) = 1 Then rule2_5.Checked = True
                If arr_rule(10) = 1 Then rule3.Checked = True
                If arr_rule(11) = 1 Then rule3_1.Checked = True
                If arr_rule(12) = 1 Then rule3_2.Checked = True
                If arr_rule(13) = 1 Then rule4.Checked = True
                If arr_rule(14) = 1 Then rule4_1.Checked = True
                If arr_rule(15) = 1 Then rule4_2.Checked = True
                If arr_rule(16) = 1 Then rule5.Checked = True
                If arr_rule(17) = 1 Then rule5_1.Checked = True
                If arr_rule(18) = 1 Then rule5_2.Checked = True
                If arr_rule(19) = 1 Then rule5_3.Checked = True
                If arr_rule(20) = 1 Then rule5_4.Checked = True
                If arr_rule(21) = 1 Then rule6.Checked = True
                If arr_rule(22) = 1 Then rule6_1.Checked = True
                If arr_rule(23) = 1 Then rule7.Checked = True
                If arr_rule(24) = 1 Then rule8.Checked = True
                If arr_rule(25) = 1 Then rule9.Checked = True
                If arr_rule(26) = 1 Then rule1_4.Checked = True
                If arr_rule(27) = 1 Then rule1_5.Checked = True
                If arr_rule(28) = 1 Then rule6_2.Checked = True
                If arr_rule(29) = 1 Then rule5_5.Checked = True
            Catch
            End Try
        End Sub

        Private Sub LoadRoles(ByVal role_id As Integer)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("SELECT * FROM roles")
                ds = New DataSet
                adapt.Fill(ds)
                rlstRoles.DataTextField = "role_name"
                rlstRoles.DataValueField = "role_id"
                rlstRoles.DataSource = ds.Tables(0).DefaultView
                rlstRoles.DataBind()
                For Each item As ListItem In rlstRoles.Items
                    If role_id = item.Value Then
                        item.Selected = True
                        Exit For
                    End If
                Next
            Catch
            End Try

        End Sub


        Private Sub cmdEdit2_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdEdit2.Click

            'MsgBox("333")

            Dim cmd As SqlClient.SqlCommand

            If txtPassword.Text <> String.Empty AndAlso (txtPassword.Text.Length < 6 Or txtPassword.Text.Length <> txtCPassword.Text.Length) Then
                lblError.Visible = True
                Exit Sub
            End If
            Try



                cmd = New SqlClient.SqlCommand("update_employee")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_sys_id", IIf(UserID <> -9999, UserID, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_name", txtFirstName.Text)
                cmd.Parameters.AddWithValue("@pi_ip", "127.0.0.1")
                cmd.Parameters.AddWithValue("@pi_account", txtLogin.Text)
                cmd.Parameters.AddWithValue("@pi_password", IIf(txtPassword.Text <> String.Empty, txtPassword.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_isadmin", CByte(cbxIsAdministrator.Checked))
                cmd.Parameters.AddWithValue("@pi_is_saler", CByte(cbxIsSaler.Checked))
                cmd.Parameters.AddWithValue("@pi_inactive", CByte(cbxInactive.Checked))
                cmd.Parameters.AddWithValue("@pi_document", IIf(cbxIsSaler.Checked, txtDocument.Text, DBNull.Value))
                cmd.Parameters.AddWithValue("@pi_role_id", rlstRoles.SelectedValue)
                cmd.Parameters.AddWithValue("@pi_work_type", txt_work_type.Text)
                'cmd.Parameters.AddWithValue("@pi_phone", txt_phone.Text)

                Dim rules = Int(rule1.Checked) & ";" _
                & Int(rule1_1.Checked) & ";" _
                & Int(rule1_2.Checked) & ";" _
                & Int(rule1_3.Checked) & ";" _
                & Int(rule2.Checked) & ";" _
                & Int(rule2_1.Checked) & ";" _
                & Int(rule2_2.Checked) & ";" _
                & Int(rule2_3.Checked) & ";" _
                & Int(rule2_4.Checked) & ";" _
                & Int(rule2_5.Checked) & ";" _
                & Int(rule3.Checked) & ";" _
                & Int(rule3_1.Checked) & ";" _
                & Int(rule3_2.Checked) & ";" _
                & Int(rule4.Checked) & ";" _
                & Int(rule4_1.Checked) & ";" _
                & Int(rule4_2.Checked) & ";" _
                & Int(rule5.Checked) & ";" _
                & Int(rule5_1.Checked) & ";" _
                & Int(rule5_2.Checked) & ";" _
                & Int(rule5_3.Checked) & ";" _
                & Int(rule5_4.Checked) & ";" _
                & Int(rule6.Checked) & ";" _
                & Int(rule6_1.Checked) & ";" _
                & Int(rule7.Checked) & ";" _
                & Int(rule8.Checked) & ";" _
                & Int(rule9.Checked) & ";" _
                & Int(rule1_4.Checked) & ";" _
                & Int(rule1_5.Checked) & ";" _
                & Int(rule6_2.Checked) & ";" _
                & Int(rule5_5.Checked)

                cmd.Parameters.AddWithValue("@pi_rules", rules)

                dbSQL.Execute(cmd)
                Response.Redirect(GetAbsoluteUrl("~/Admin/Users.aspx"))
            Catch
                lblSqlError.Visible = True
                lblSqlError.Text = Err.Description
            End Try
        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdCancel.Click


            Response.Redirect(GetAbsoluteUrl("~/Admin/Users.aspx"))
        End Sub

    End Class

End Namespace
