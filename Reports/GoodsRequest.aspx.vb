Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports

    Partial Class GoodsRequest
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
            lblError.Visible = False

            If Not IsPostBack Then
                Try
                    tbxBeginDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    tbxEndDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                LoadLists()
            End If
        End Sub

        Private Sub LoadLists()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet

            Try
                adapt = dbSQL.GetDataAdapter("get_dvr_Suppliers", True)
                adapt.Fill(ds)

                lbxProviders.DataSource = ds.Tables(0).DefaultView
                lbxProviders.DataValueField = "sys_id"
                lbxProviders.DataTextField = "supplier_name"
                lbxProviders.DataBind()
            Catch
                lblError.Text = "Ошибка загрузки списка поставщиков"
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

            Dim strRequest$ = "GoodsReport.aspx?start_date={0}&end_date={1}&supset={2}"

            Dim supset$ = ""
            If Radiobutton2.Checked Then
                Dim item As ListItem
                For Each item In lbxProviders.Items
                    If item.Selected Then supset &= item.Value & ","
                Next item
            End If

            strRequest$ = String.Format(strRequest, Format(startdate, "dd/MM/yyyy"), Format(endDate, "dd/MM/yyyy"), supset)
            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub
        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub
    End Class

End Namespace
