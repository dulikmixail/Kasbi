Namespace Kasbi.Reports

    Partial Class MasterTOReport
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
        Dim date_start As Date
        Dim date_end As Date
        Dim period%
        Dim details As String
        Dim disrepair As String
        Dim Executor As String

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = Date.ParseExact(Request.QueryString("start_date"), "dd.MM.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                date_end = Date.ParseExact(Request.QueryString("end_date"), "dd.MM.yyyy", System.Globalization.DateTimeFormatInfo.InvariantInfo)
                'date_start = CDate(Request.QueryString("start_date"))
                'date_end = CDate(Request.QueryString("end_date"))

                period = GetPageParam("period")
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
                period = 0
            End Try

            Executor = Request.QueryString("ex")
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

                If period = 0 Then
                    cmd = New SqlClient.SqlCommand("prc_rpt_MasterTO")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    cmd.Parameters.AddWithValue("@pi_date_end", date_end)
                    If Not Executor Is Nothing AndAlso Executor <> String.Empty Then
                        cmd.Parameters.AddWithValue("@pi_executor", Executor)
                    End If
                Else
                    cmd = New SqlClient.SqlCommand("prc_rpt_MasterTOForPeriod")
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.AddWithValue("@pi_date_start", date_start)
                    If Not Executor Is Nothing AndAlso Executor <> String.Empty Then
                        cmd.Parameters.AddWithValue("@pi_executor", Executor)
                    End If
                End If


                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)
                grid.DataSource = ds.Tables(0).DefaultView
                grid.DataBind()

            Catch
                Dim s$ = Err.Description

            End Try
        End Sub

        Public Function GetRussianDate(ByVal d As Date) As String
            Dim m() As String = {" Январь ", " Февраль ", " Март ", " Апрель ", " Май ", " Июнь ", " Июль ", " Август ", " Сентябрь ", " Октябрь ", " Ноябрь ", " Декабрь "}
            GetRussianDate = m(Month(d) - 1) & Year(d) & "г."
        End Function

        Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                CType(e.Item.FindControl("lblClosePeriod"), Label).Text = GetRussianDate(e.Item.DataItem("start_date"))
            End If
        End Sub
    End Class

End Namespace
