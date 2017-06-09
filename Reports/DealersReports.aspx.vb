Namespace Kasbi.Reports

    Partial Class DealersReports
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
        Dim num%

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")
                cmd = New SqlClient.SqlCommand("prc_rpt_Dealers")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                num = 0
                grid.DataSource = ds
                grid.DataBind()
            Catch
            End Try
        End Sub

        Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                num = num + 1
                CType(e.Item.FindControl("lblRecordNum"), Label).Text = num
            End If
        End Sub

    End Class

End Namespace
