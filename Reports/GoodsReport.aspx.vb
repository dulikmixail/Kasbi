Namespace Kasbi.Reports

    Partial Class GoodsReport
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
        Public supset As String
        Dim num%
        Dim delivery_id%

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try
            supset = Request.QueryString("supset")
            If Not IsPostBack Then Bind()
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")
                num = 0

                cmd = New SqlClient.SqlCommand("prc_rpt_Goods")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                If Not supset Is Nothing AndAlso supset <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_supset", supset)
                End If
                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                delivery_id = -1
                grid.DataSource = ds.Tables(0).DefaultView
                grid.DataBind()
            Catch ex As Exception

            End Try
        End Sub

        Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If (e.Item.DataItem("delivery_sys_id") <> delivery_id) Then
                    delivery_id = e.Item.DataItem("delivery_sys_id")
                    num = num + 1
                    CType(e.Item.FindControl("lblNumDelivery"), Label).Text = num
                End If
                If ((num Mod 2) > 0) Then
                    e.Item.BackColor = Drawing.Color.FromArgb(248, 238, 210)
                Else
                    e.Item.BackColor = Drawing.Color.FromArgb(255, 255, 255)
                End If
            End If
        End Sub

    End Class

End Namespace

