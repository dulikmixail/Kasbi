Namespace Kasbi

    Partial Class SupportByMonth
        Inherits PageBase
        Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Dim iNum% = 0
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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
                txtCustomerName.Attributes.Add("onchange", "OnChangeReportKeyword(" & lnkCustomReport.ClientID & ", " & txtCustomerName.ClientID & ");")
            End If
        End Sub

        Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("get_support_debtors", True)
                ds = New DataSet()
                adapt.Fill(ds)
                grd.DataSource = ds.Tables(0).DefaultView
                grd.DataKeyField = "customer_sys_id"
                grd.DataBind()
            Catch
                msg.Text = "Ошибка загрузки информации о должниках!<br>" & Err.Description
            End Try
        End Sub

        Private Sub grd_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grd.ItemDataBound
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                If IsDBNull(e.Item.DataItem("start_date")) Then
                    e.Item.BackColor = Color.LightGray
                End If
                iNum = iNum + 1
                CType(e.Item.FindControl("lblNum"), Label).Text = iNum
            End If
        End Sub

    End Class

End Namespace
