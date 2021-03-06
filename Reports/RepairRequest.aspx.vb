Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class RepairRequest
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
            Dim cmd As SqlClient.SqlCommand
            Try
                cmd = New SqlClient.SqlCommand("get_masters")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_permissions", "1 4")
                cmd.CommandTimeout = 0
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                lbxExecutor.DataSource = ds.Tables(0).DefaultView
                lbxExecutor.DataValueField = "sys_id"
                lbxExecutor.DataTextField = "Name"
                lbxExecutor.DataBind()
            Catch
            End Try
        End Sub

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
            Dim startdate As DateTime = New DateTime
            Dim endDate As DateTime = New DateTime

            lblError.Visible = False
            Try
                startdate = DateTime.Parse(tbxBeginDate.Text)
                endDate = DateTime.Parse(tbxEndDate.Text)
                If (startdate > endDate) Then
                    lblError.Text = "�������� ���� ������ ���� ������ ���������"
                    lblError.Visible = True
                End If
            Catch
                lblError.Text = "����������, ������� ���������� �������� ���"
                lblError.Visible = True
            End Try

            If (lblError.Visible) Then Exit Sub
            Dim Executor$ = ""
            If Executor <> "" Then
                MsgBox("���������� ������� ���� �������", MsgBoxStyle.Information, "������")
            End If
            For Each item As ListItem In lbxExecutor.Items
                If item.Selected Then Executor &= item.Value & ","
            Next item

            'Dim strRequest$ = String.Format("RepairReport.aspx?start_date={0}&end_date={1}&dt={2}&dr={3}&ex={4}", Format(startdate, "MM/dd/yyyy"), Format(endDate, "MM/dd/yyyy"), tbxDetails.Text, tbxDisrepair.Text, Executor)

            Dim strRequest$ = String.Format("RepairReport.aspx?start_date={0}&end_date={1}&dt={2}&dr={3}&ex={4}", Format(startdate, "dd/MM/yyyy"), Format(endDate, "dd/MM/yyyy"), tbxDetails.Text, tbxDisrepair.Text, Executor)

            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

    End Class

End Namespace
