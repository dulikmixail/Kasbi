Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports

    Partial Class QuartalMarkaRequest
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
            lblError.Visible = False
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
        End Sub

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
            Dim startdate As DateTime = New DateTime
            Dim endDate As DateTime = New DateTime

            lblError.Visible = False
            Dim strRequest$
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

            If chkDismissalMark.Checked = True Then
                strRequest = "../Documents.aspx?t=40&begin_date=" & Format(startdate, "MM/dd/yyyy") & "&end_date=" & Format(endDate, "MM/dd/yyyy")
            Else
                strRequest = "../Documents.aspx?t=30&begin_date=" & Format(startdate, "MM/dd/yyyy") & "&end_date=" & Format(endDate, "MM/dd/yyyy")
            End If

            strRequest = "<script language='javascript' type='text/javascript'>window.open('" & strRequest & "')</script>"

            If (lblError.Visible) Then Exit Sub

            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

    End Class

End Namespace
