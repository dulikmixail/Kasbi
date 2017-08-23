Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class CTODismissRequest
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
            If Not IsPostBack Then
                Try
                    tbxBeginDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    tbxEndDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                LoadIMNS()
            End If
        End Sub

        Private Sub LoadIMNS()
            Dim adapter As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                adapter = dbSQL.GetDataAdapter("prc_getIMNS", True)
                adapter.Fill(ds)

                lbxNI.DataSource = ds
                lbxNI.DataValueField = "sys_id"
                lbxNI.DataTextField = "imns_name"
                lbxNI.DataBind()
            Catch
                lblError.Text = "Ошибка загрузки списка ИМНС !<br>" & Err.Description
                lblError.Visible = True
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
                    lblError.Text = "Конечная дата должна быть меньше начальной"
                    lblError.Visible = True
                End If
            Catch
                lblError.Text = "Пожалуйста, введите корректные значения дат"
                lblError.Visible = True
            End Try

            If (lblError.Visible) Then Exit Sub

            Dim strRequest$
            If RadioButton2.Checked Then
                strRequest = "CTODismissReport.aspx?start_date=" & Format(startdate, "dd/MM/yyyy") & "&end_date=" & Format(endDate, "dd/MM/yyyy")
                Dim item As ListItem
                Dim s$ = ""
                For Each item In lbxNI.Items
                    If item.Selected Then s &= item.Value & ","
                Next item
                strRequest &= "&niset=" & s
            Else
                strRequest = "CTODismissReport.aspx?start_date=" & Format(startdate, "dd/MM/yyyy") & "&end_date=" & Format(endDate, "dd/MM/yyyy")
                strRequest &= "&niset="
            End If
            strRequest &= "&oni=" & IIf(cbxNI.Checked, "1", "0")
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"

            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

    End Class

End Namespace
