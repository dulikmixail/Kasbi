Imports Microsoft.Office.Interop

Namespace Kasbi.Reports

    Partial Class MasterworkReport
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
        Dim date_start
        Dim date_end

        Dim details As String
        Dim disrepair As String
        Dim Executor As String

        Dim xlsApp As Excel.ApplicationClass
        Private sheet As Excel.WorksheetClass
        Private book As Excel.WorkbookClass

        Dim total_normahour
        Dim total_normahour_garant
        Dim total_setto
        Dim total_delto

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = Request.QueryString("start_date")
                date_end = Request.QueryString("end_date")
                'lblStartDate.Text = Format(CDate(date_start), "MM.dd.yyyy")
                'lblEndDate.Text = Format(CDate(date_end), "MM.dd.yyyy")
                lblStartDate.Text = Format(CDate(date_start), "dd.MM.yyyy")
                lblEndDate.Text = Format(CDate(date_end), "dd.MM.yyyy")

                'Dim parce = Split(date_start, ".")
                'date_start = parce(0) & "/" & parce(1) & "/" & parce(2)
                'parce = Split(date_end, ".")
                'date_end = parce(0) & "/" & parce(1) & "/" & parce(2)
                Dim parce = Split(date_start, ".")
                date_start = parce(1) & "/" & parce(0) & "/" & parce(2)
                parce = Split(date_end, ".")
                date_end = parce(1) & "/" & parce(0) & "/" & parce(2)

            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try

            details = Request.QueryString("dt")
            If Not IsPostBack Then
                Bind()
            End If

            lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Try
                adapt = dbSQL.GetDataAdapter("select sys_id, name, " & _
                "  (SELECT     COUNT(sys_id)  FROM cash_history WHERE executor = employee.sys_id AND state IN (2, 3) AND dismissal_date BETWEEN '" & date_start & "' AND '" & date_end & "') AS count_dismiss, " & _
                "  (SELECT COUNT(sys_id) FROM cash_history WHERE state = '4' AND (executor = employee.sys_id OR updateUserID=employee.sys_id) AND change_state_date BETWEEN '" & date_start & "' AND '" & date_end & "') AS count_setto, " & _
                " (SELECT SUM(norma_hour) from repair_info WHERE hc_sys_id IN (SELECT sys_id FROM cash_history WHERE state = '5' AND executor = employee.sys_id AND garantia=0 AND repairdate_out BETWEEN '" & date_start & "' AND '" & date_end & "')) AS count_repair " & _
                "from employee where role_id=1 and inactive=0 order by Name")

                ds = New DataSet
                adapt.Fill(ds)
                grdUsers.DataSource = ds.Tables(0).DefaultView
                grdUsers.DataKeyField = "sys_id"
                grdUsers.DataBind()
            Catch
            End Try

        End Sub

        Protected Sub grdUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemDataBound
            If e.Item.ItemType = ListItemType.Item OrElse e.Item.ItemType = ListItemType.AlternatingItem Then
                Try
                    CType(e.Item.FindControl("lblSetTO"), Label).Text = e.Item.DataItem("count_setto")
                    CType(e.Item.FindControl("lblDelTO"), Label).Text = e.Item.DataItem("count_dismiss")


                    If Not (IsDBNull(e.Item.DataItem("count_repair"))) Then
                        CType(e.Item.FindControl("lblNormaHour"), Label).Text = Math.Round(e.Item.DataItem("count_repair"), 2).ToString
                        total_normahour += Math.Round(e.Item.DataItem("count_repair"), 2)
                    Else
                        CType(e.Item.FindControl("lblNormaHour"), Label).Text = "0"
                    End If

                    Dim countrepair2 = dbSQL.ExecuteScalar("SELECT SUM(norma_hour) from repair_info WHERE hc_sys_id IN (SELECT sys_id FROM cash_history WHERE state = '5' AND executor = " & e.Item.DataItem("sys_id") & "  AND garantia=1 AND repairdate_out BETWEEN '" & date_start & "' AND '" & date_end & "')")
                    If Not (IsDBNull(countrepair2)) Then
                        CType(e.Item.FindControl("lblNormaHour_garant"), Label).Text = Math.Round(countrepair2, 2).ToString
                    Else
                        CType(e.Item.FindControl("lblNormaHour_garant"), Label).Text = "0"
                    End If
                    total_normahour_garant += Math.Round(countrepair2, 2)



                    total_setto += e.Item.DataItem("count_setto")
                    total_delto += e.Item.DataItem("count_dismiss")

                Catch ex As Exception
                End Try

            End If

            If e.Item.ItemType = ListItemType.Footer Then

                CType(e.Item.FindControl("lblNormaHour"), Label).Text = total_normahour
                CType(e.Item.FindControl("lblNormaHour_garant"), Label).Text = total_normahour_garant
                CType(e.Item.FindControl("lblSetTO"), Label).Text = total_setto
                CType(e.Item.FindControl("lblDelTO"), Label).Text = total_delto

            End If

        End Sub
        

    End Class
End Namespace
