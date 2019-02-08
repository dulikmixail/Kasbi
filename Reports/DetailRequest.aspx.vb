Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports
    Partial Class DetailRequest
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            lblError.Visible = False
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")

            If Not IsPostBack Then
                Try
                    tbxBeginDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    tbxEndDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                LoadMasters()
            End If
        End Sub

        Private Sub LoadMasters()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Dim cmd As SqlCommand
            Try

                cmd = New SqlClient.SqlCommand("get_employee_by_role_id")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_role_id", 0)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                ds.Tables(0).DefaultView.Sort = "Name"
                lbxExecutor.DataSource = ds.Tables(0).DefaultView
                lbxExecutor.DataValueField = "sys_id"
                lbxExecutor.DataTextField = "Name"
                lbxExecutor.DataBind()
            Catch
            End Try
        End Sub

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnView.Click
            Dim startdate As DateTime = New DateTime
            Dim endDate As DateTime = New DateTime

            lblError.Visible = False
            Try
                startdate = DateTime.Parse(tbxBeginDate.Text)
                endDate = DateTime.Parse(tbxEndDate.Text)
                If (startdate > endDate) Then
                    lblError.Text = "Конечная дата должна быть меньше начальной"
                    lblError.Visible = True
                End If
            Catch
                lblError.Text = "Пожалуйста, введите корректные значения дат"
                lblError.Visible = True
            End Try

            Dim Executor$ = ""
            If Executor <> "" Then
                MsgBox("Невозможно вывести дату ремонта", MsgBoxStyle.Information, "Ошибка")
            End If
            For Each item As ListItem In lbxExecutor.Items
                If item.Selected Then Executor &= item.Value & ","
            Next item

            Dim strRequest$ = String.Format("DetailReport.aspx?start_date={0}&end_date={1}&ex={2}",
                                            Format(startdate, "dd/MM/yyyy"), Format(endDate, "dd/MM/yyyy"), Executor)
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest &
                         "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) _
            Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub
    End Class
End Namespace
