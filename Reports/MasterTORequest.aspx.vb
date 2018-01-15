Imports System.Globalization
Imports System.Threading

Namespace Kasbi.Reports
    Partial Class MasterTORequest
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

                Dim d As Date = Now

                d.AddMonths(-1)
                lstMonth.SelectedIndex = d.Month - 1
                If d.Year > 2002 And d.Year <= 2020 Then
                    lstYear.SelectedIndex = d.Year - 2003
                Else
                    lstYear.SelectedIndex = 0
                End If
                cbxPeriod.Checked = True
                cbxPeriod_CheckedChanged(Me, Nothing)

            End If
        End Sub

        Private Sub LoadMasters()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapt = dbSQL.GetDataAdapter("get_masters", True)
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
            Dim d
            Dim strRequest$

            lblError.Visible = False

            Dim Executor$ = ""
            For Each item As ListItem In lbxExecutor.Items
                If item.Selected Then Executor &= item.Value & ","
            Next item

            If cbxPeriod.Checked = False Then
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
                strRequest = String.Format("MasterTOReport.aspx?start_date={0}&end_date={1}&ex={2}", Format(startdate, "dd/MM/yyyy"), Format(endDate, "dd/MM/yyyy"), Executor)
                strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"

            Else
                Try
                    d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
                Catch
                    lblError.Text = "Пожалуйста, введите корректное значение периода"
                    lblError.Visible = True
                End Try
                strRequest = String.Format("MasterTOReport.aspx?start_date={0}&end_date={1}&ex={2}&period=1", Format(d, "dd/MM/yyyy"), Format(d, "dd/MM/yyyy"), Executor)
                strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            End If

            If (lblError.Visible) Then Exit Sub
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

        Private Sub cbxPeriod_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxPeriod.CheckedChanged
            If cbxPeriod.Checked = True Then
                pnlDates.Visible = False
                lstMonth.Enabled = True
                lstYear.Enabled = True
            Else
                pnlDates.Visible = True
                lstMonth.Enabled = False
                lstYear.Enabled = False
            End If
        End Sub

        Private Sub lnkExportReportToExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportReportToExcel.Click
            Dim startdate As DateTime = New DateTime
            Dim endDate As DateTime = New DateTime
            Dim d
            Dim strRequest$

            lblError.Visible = False

            Dim Executor$ = ""
            For Each item As ListItem In lbxExecutor.Items
                If item.Selected Then Executor &= item.Value & ","
            Next item

            If cbxPeriod.Checked = False Then
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
                strRequest = String.Format("../documents.aspx?t=53&start_date={0}&end_date={1}&ex={2}", Format(startdate, "MM/dd/yyyy"), Format(endDate, "MM/dd/yyyy"), Executor)
                strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"

            Else
                Try
                    d = New Date(lstYear.SelectedItem.Value, lstMonth.SelectedItem.Value, 1)
                Catch
                    lblError.Text = "Пожалуйста, введите корректное значение периода"
                    lblError.Visible = True
                End Try
                strRequest = String.Format("../documents.aspx?t=53&start_date={0}&end_date={1}&ex={2}&period=1", Format(d, "MM/dd/yyyy"), Format(d, "MM/dd/yyyy"), Executor)
                strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            End If

            If (lblError.Visible) Then Exit Sub
            Me.RegisterStartupScript("report", strRequest)
        End Sub
    End Class

End Namespace




