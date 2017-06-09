Namespace Kasbi.Reports

    Partial Class CTODismissReport
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

        Public date_start As DateTime
        Public date_end As DateTime

        Public date_start2
        Public date_end2

        Public onlyNI As Boolean
        Public niset As String

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))

                date_start2 = Request.QueryString("start_date")
                date_end2 = Request.QueryString("end_date")
                Dim parce = Split(date_start2, ".")
                date_start2 = parce(1) & "/" & parce(0) & "/" & parce(2)
                parce = Split(date_end2, ".")
                date_end2 = parce(1) & "/" & parce(0) & "/" & parce(2)

            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try

            niset = Request.QueryString("niset")
            onlyNI = Request.QueryString("oni")
            Bind()
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adap As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("prc_getDismissIMNS")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                If niset <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_niset", niset)
                End If
                If onlyNI Then
                    cmd.Parameters.AddWithValue("@pi_from_ni", onlyNI)
                End If

                adap = dbSQL.GetDataAdapter(cmd)
                adap.Fill(ds)
                rep.DataSource = ds.Tables(0).DefaultView
                rep.DataBind()
            Catch

            End Try
        End Sub

        Dim i As Integer = 0

        Private Sub BindGoods(ByRef repGoods As Repeater, ByVal imns_sys_id As Integer)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                date_start = Request.QueryString("start_date")
                date_end = Request.QueryString("end_date")

                cmd = New SqlClient.SqlCommand("prc_rpt_CTODismiss")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                cmd.Parameters.AddWithValue("@pi_niset", imns_sys_id)
                If onlyNI Then
                    cmd.Parameters.AddWithValue("@pi_from_ni", onlyNI)
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                repGoods.DataSource = ds
                repGoods.DataBind()
            Catch
            End Try
        End Sub

        Private Sub rep_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rep.ItemDataBound
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim dv As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim repGoods As Repeater = CType(e.Item.FindControl("repGood"), Repeater)
                BindGoods(repGoods, dv.Item("sys_id"))
            End If

            If e.Item.ItemType = ListItemType.Footer Then
                'Тут достаем мастеров и вешаем на них снятие ТО
                Try
                    Dim adapt As SqlClient.SqlDataAdapter
                    Dim ds As DataSet

                    adapt = dbSQL.GetDataAdapter("select sys_id, name,  (SELECT COUNT(sys_id)  FROM  cash_history  WHERE (executor = employee.sys_id) AND (state=2 or state=3) AND dismissal_date BETWEEN '" & date_start2 & "' AND '" & date_end2 & "') AS count_dismiss from employee where (role_id=1 or is_admin=1) and inactive=0 order by Name")
                    ds = New DataSet
                    adapt.Fill(ds)
                    grdUsers.DataSource = ds.Tables(0).DefaultView
                    grdUsers.DataKeyField = "sys_id"
                    grdUsers.DataBind()
                Catch
                End Try
            End If
        End Sub

        Protected Sub grdUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemDataBound
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                If e.Item.DataItem("count_dismiss") > 0 Then
                    CType(e.Item.FindControl("lblDismiss"), Label).Text = e.Item.DataItem("count_dismiss")
                Else
                    e.Item.Visible = False
                End If
            End If
        End Sub

    End Class

End Namespace
