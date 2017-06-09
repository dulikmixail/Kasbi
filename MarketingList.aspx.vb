Namespace Kasbi

    Partial Class MarketingList
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
        Dim s

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                LoadCustomerList()
            End If
        End Sub

        Private Sub LoadCustomerList()
            Dim sql$ = "SELECT * FROM employee WHERE inactive='0' ORDER BY name"
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstEmployee.DataSource = ds.Tables(0).DefaultView
                lstEmployee.DataTextField = "name"
                lstEmployee.DataValueField = "sys_id"
                lstEmployee.DataBind()
            Catch
            End Try
            lstEmployee.Enabled = True
        End Sub

        Private Function GetList(ByVal list As ListBox) As String
            Dim d As String = ""
            Dim item As ListItem
            For i As Integer = 0 To list.Items.Count - 1
                item = list.Items(i)
                If (item.Selected) Then
                    d += item.Value
                    If i < list.Items.Count - 1 Then d += ","

                End If
            Next
            GetList = d.Trim(",")
        End Function

        Sub Bind(ByVal query)
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            grdCustomers.DataSource = ds.Tables(0).DefaultView
            grdCustomers.DataKeyField = "customer_sys_id"
            grdCustomers.DataBind()
        End Sub

        Private Sub grdCustomers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdCustomers.ItemCommand
            Dim i%
            i = e.Item.ItemIndex
            Response.Redirect(GetAbsoluteUrl("~/MarketingView.aspx?" & grdCustomers.DataKeys.Item(i)))
        End Sub

        Protected Sub btnFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFind.Click
            Dim m_filter
            Dim query
            Dim manager

            m_filter = txtFilter.Text
            manager = GetList(lstEmployee)

            query = "SELECT *, (SELECT name FROM employee e WHERE e.sys_id=c.manager) as manag, (SELECT count(*) FROM client_history WHERE customer_sys_id=c.customer_sys_id) as num_history FROM customer c WHERE (c.cto is null or c.cto <> 1) and (c.boos_last_name like '%" & m_filter & "%' or c.customer_name like '%" & m_filter & "%') "

            If manager <> "" Then
                query += " and c.manager IN(" + manager + ") "
            Else
                query += " and c.manager <> '' "
            End If

            Bind(query)
        End Sub
    End Class
End Namespace
