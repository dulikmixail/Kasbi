Imports Kasbi.Common
Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class RestRequest
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
                LoadGoodTypeList()
            End If
        End Sub

        Private Sub LoadGoodTypeList()
            Dim sql$ = "select * from good_type order by name"
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstGoodType.DataSource = ds.Tables(0).DefaultView
                lstGoodType.DataTextField = "name"
                lstGoodType.DataValueField = "good_type_sys_id"
                lstGoodType.DataBind()
            Catch
                lblError.Text = "Ошибка загрузки информации о типах товаров!<br>" & Err.Description
            End Try
            lstGoodType.Enabled = True
        End Sub

        Private Function GetList(ByVal list As ListBox) As String
            Dim s$ = String.Empty
            Dim item As ListItem
            For i As Integer = 0 To list.Items.Count - 1
                item = list.Items(i)
                If (item.Selected) Then
                    s += item.Value
                    If i < list.Items.Count - 1 Then s += ","
                End If
            Next
            GetList = s
        End Function

        Private Sub btnView_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnView.Click
            Dim strRequest$ = ""

            If rbtnGootTypeSet1.Checked Then
                strRequest &= "cs=1"
            ElseIf rbtnGootTypeSet2.Checked Then
                strRequest &= "cs=2"
                strRequest &= "&g_t=" & GetList(lstGoodType)
            End If

            strRequest = "<script language='javascript' type='text/javascript'>window.open('RestReportByGoods.aspx?" & strRequest & "')</script>"
            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub
    End Class

End Namespace
