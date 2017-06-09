Namespace Kasbi.Reports

    Partial Class RepairReport
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
        Dim date_start As DateTime
        Dim date_end As DateTime
        Dim details As String
        Dim disrepair As String
        Dim Executor As String
        Dim WithEvents grdRep As DataGrid
        Dim TotalNormaHour
        Dim arrnorm(1000)

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))
            Catch
                date_start = Now.AddMonths(-2)
                date_end = Now
            End Try

            details = Request.QueryString("dt")
            disrepair = Request.QueryString("dr")
            Executor = Request.QueryString("ex")
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim rv As DataRowView

            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("prc_rpt_Disrepair")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_begin_date", date_start)
                cmd.Parameters.AddWithValue("@pi_end_date", date_end)

                If Not details Is Nothing AndAlso details <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_details", details)
                End If
                If Not disrepair Is Nothing AndAlso disrepair <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_disrepair", disrepair)
                End If
                If Not Executor Is Nothing AndAlso Executor <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_executor", Executor)
                End If

                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                grid.DataSource = ds
                grid.DataBind()
            Catch
            End Try
        End Sub

        Protected Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound

            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                If Not (IsDBNull(e.Item.DataItem("norma_hour"))) Then
                    TotalNormaHour = TotalNormaHour + e.Item.DataItem("norma_hour")
                    CType(e.Item.FindControl("lblNormaHour"), Label).Text = Math.Round(e.Item.DataItem("norma_hour"), 2)

                    arrnorm(e.Item.DataItem("employee_id")) = arrnorm(e.Item.DataItem("employee_id")) + e.Item.DataItem("norma_hour")
                End If
            End If

            If e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblTotalNormaHour"), Label).Text = Math.Round(TotalNormaHour, 2)

                Dim adapt As SqlClient.SqlDataAdapter
                Dim ds As DataSet

                Try

                    'adapt = dbSQL.GetDataAdapter("select * from employee where role_id=1 and inactive=0 order by Name")
                    adapt = dbSQL.GetDataAdapter("select * from employee order by Name")

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
                If arrnorm(e.Item.DataItem("sys_id")) > 0 Then
                    CType(e.Item.FindControl("lblNormaHour"), Label).Text = Math.Round(arrnorm(e.Item.DataItem("sys_id")), 2)
                Else
                    e.Item.Visible = False
                End If
            End If
        End Sub
    End Class
End Namespace
