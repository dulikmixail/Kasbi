Namespace Kasbi.Reports

    Partial Class ClientReports
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
        Dim num%
        Dim dTotal As Double
        Public param1

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If

        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim cmd As SqlClient.SqlCommand

            If Request.QueryString.Count > 0 Then
                param1 = GetPageParam(0)
            End If

            Try
                Dim filter
                If param1 = 11 Then
                    filter = " order by count_cash DESC"

                Else
                    filter = " order by dolg DESC"
                End If
                cmd = New SqlClient.SqlCommand("prc_rpt_ClientDept")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_filter", Filter)
                adapt = dbSQL.GetDataAdapter(cmd)

                adapt.Fill(ds)
                num = 0
                dTotal = 0
                grid.DataSource = ds
                grid.DataBind()
            Catch
            End Try

        End Sub

        Private Sub grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grid.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then


                Dim s$ = "-"
                If Not IsDBNull(e.Item.DataItem("count_cash")) And (param1 = 11 Or param1 = 12) Then
                    If e.Item.DataItem("count_cash") = 0 Then
                        e.Item.Visible = False
                    End If
                End If

                If Not IsDBNull(e.Item.DataItem("dolg")) And e.Item.Visible = True Then
                    dTotal = dTotal + e.Item.DataItem("dolg")
                    num = num + 1
                End If

                CType(e.Item.FindControl("lblRecordNum"), Label).Text = num
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblTotal"), Label).Text = CStr(dTotal)
                dTotal = 0
            End If
        End Sub

    End Class

End Namespace
