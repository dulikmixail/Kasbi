
Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports
    Partial Class CashRegistersForTO
        Inherits PageBase
        Dim CurrentCustomer%
        Const ClearString = "---------------"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()>Private Sub InitializeComponent()
        End Sub


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        'Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '    'Put user code to initialize the page here
        '    'Common.SetUpLocalization()
        '    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")

        '    If Not IsPostBack Then
        '        Try
        '            'tbxBeginDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
        '            'tbxEndDate.Text = (New CultureInfo("ru-Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
        '        Catch
        '        End Try
        '        ShowContent()
        '    End If
        'End Sub

        'Private Sub ImgSearchClient_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearchClient.Click
        '    Dim str$ = tbSearchClient.Text.Replace("'","")
        '    If Trim(str).Length = 0 Then
        '        LoadClientList()
        '        Exit Sub
        '    End If
        '    LoadClientList(str)
        'End Sub

        'Private Sub ImgSearchDealer_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSearchDealers.Click
        '    Dim str$ = tbSearchDealers.Text.Replace("'","")
        '    If Trim(str).Length = 0 Then
        '        LoadDealerList()
        '        Exit Sub
        '    End If
        '    LoadDealerList(str)
        'End Sub

        'Private Sub ShowContent()
        '    LoadCashRegisterList()
        '    LoadDealerList()
        '    LoadClientList()
        'End Sub

        'Private Function GetCustomerDataView(Optional ByVal sRequest$ = "") As DataView
        '    Dim adapt As SqlClient.SqlDataAdapter
        '    Dim cmd As SqlClient.SqlCommand
        '    Dim ds As DataSet
        '    Dim dv As DataView = new DataView()
        '    Try
        '        cmd = New SqlClient.SqlCommand("get_customer_for_support")
        '        cmd.Parameters.AddWithValue("@pi_filter", sRequest$)
        '        cmd.CommandType = CommandType.StoredProcedure
        '        adapt = dbSQL.GetDataAdapter(cmd)
        '        ds = New DataSet
        '        adapt.Fill(ds)
        '        dv = ds.Tables(0).DefaultView
        '    Catch
        '        lblError.Text = "Ошибка загрузки информации customer!<br>" & Err.Description
        '    End Try
        '    Return dv
        'End Function

        'Private Sub LoadCashRegisterList()
        '    Dim sql$ = ""

        '    sql = "select * from good_type where is_cashregister='1' order by name"

        '    Dim adapt As SqlClient.SqlDataAdapter
        '    Dim ds As DataSet
        '    Try
        '        adapt = dbSQL.GetDataAdapter(sql)
        '        ds = New DataSet
        '        adapt.Fill(ds)
        '        lstCashRegister.DataSource = ds.Tables(0).DefaultView
        '        lstCashRegister.DataTextField = "name"
        '        lstCashRegister.DataValueField = "good_type_sys_id"
        '        lstCashRegister.DataBind()
        '        lstCashRegister.Items.Insert(0, New ListItem(ClearString, ClearString))
        '    Catch
        '        lblError.Text = "Ошибка загрузки информации о кассовых аппаратах!<br>" & Err.Description
        '    End Try
        '    lstCashRegister.Enabled = True
        'End Sub

        'Private Sub LoadClientList(Optional ByVal clientName$ = "")
        '    Dim s As String = " cto=0 " & "and customer_name like '%" & clientName &"%'"
        '    lstClient.Items.Clear()
        '    lstClient.DataSource = GetCustomerDataView(s)
        '    lstClient.DataTextField = "customer_name"
        '    lstClient.DataValueField = "customer_sys_id"
        '    lstClient.DataBind()
        '    lstClient.Items.Insert(0, New ListItem(ClearString, ClearString))
        '    lstClient.Enabled = True
        '    End Sub

        'Private Sub LoadDealerList(Optional ByVal dealerName$ = "")
        '    Dim s As String = " cto=1 " & "and customer_name like '%" & dealerName & "%'"
        '    lstDealers.Items.Clear()
        '    lstDealers.DataSource = GetCustomerDataView(s)
        '    lstDealers.DataTextField = "customer_name"
        '    lstDealers.DataValueField = "customer_sys_id"
        '    lstDealers.DataBind()
        '    lstDealers.Items.Insert(0, New ListItem(ClearString, ClearString))
        '    lstDealers.Enabled = True
        'End Sub
    End Class
End Namespace
