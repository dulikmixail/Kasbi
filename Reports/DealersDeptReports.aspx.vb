Namespace Kasbi.Reports

    Partial Class DealersDeptReports
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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                lblCurrentDate.Text = Format(Now, "dd.MM.yyyy")
                adapt = dbSQL.GetDataAdapter("prc_rpt_DealersDept", True)
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
                num = num + 1
                If Not IsDBNull(e.Item.DataItem("dolg")) Then
                    dTotal = dTotal + e.Item.DataItem("dolg")

                End If

                Dim s$ = "-"
                If Not IsDBNull(e.Item.DataItem("lastorder")) Then
                    Dim d As Date
                    d = e.Item.DataItem("lastorder")
                    s = d.ToString("dd.MM.yyyy")
                End If
                CType(e.Item.FindControl("lblLastOrder"), Label).Text = s
                CType(e.Item.FindControl("lblRecordNum"), Label).Text = num
            ElseIf e.Item.ItemType = ListItemType.Footer Then
                CType(e.Item.FindControl("lblTotal"), Label).Text = CStr(dTotal)
                dTotal = 0
            End If
        End Sub

    End Class

End Namespace
