
Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class SellingRequest
        Inherits PageBase
        Dim CurrentCustomer%
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lbxClient As System.Web.UI.WebControls.ListBox
        Protected WithEvents Radiobutton1 As System.Web.UI.WebControls.RadioButton
        Protected WithEvents rbtnGootTypeSet As System.Web.UI.WebControls.RadioButton


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            'Common.SetUpLocalization()
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("ru-Ru")
            lblError.Visible = False

            If Not IsPostBack Then
                Try
                    tbxBeginDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                    tbxEndDate.Text = (New CultureInfo("ru_Ru", False)).DateTimeFormat.ShortDatePattern.ToUpper()
                Catch
                End Try
                ShowContent()
            End If
        End Sub

        Private Sub ShowContent()
            LoadCustomerList()
            LoadGoodTypeList()
            LoadManagerList()
            rblReportType_SelectedIndexChanged(Me, Nothing)
        End Sub

        Private Sub lnkCustomerFind_KKMRequest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCustomerFind.Click
            Dim str$ = txtCustomerFind.Text
            If Trim(str).Length = 0 Then LoadCustomerList() : Exit Sub
            If str.IndexOf("'") > -1 Then Exit Sub

            Dim s$
            If rbtnClientSet2.Checked = True Then
                s = " (customer_name like '%" & str & "%') and cto=1 "
            Else
                s = " (customer_name like '%" & str & "%')"
            End If

            LoadCustomerList(s)
            Session("CustFilter") = s
        End Sub

        Private Sub LoadCustomerList(Optional ByVal sRequest$ = "")
            Dim adapt As SqlClient.SqlDataAdapter
            Dim cmd As SqlClient.SqlCommand
            Dim ds As DataSet
            Dim s$
            Try
                If sRequest = "" Then
                    If Session("CustFilter") <> "" Then
                        s = Session("CustFilter")
                    Else
                        s = ""
                    End If
                Else
                    s = sRequest
                End If

                If CurrentCustomer.ToString <> "" And s = "" Then
                    s &= " c.customer_sys_id=" & CurrentCustomer.ToString
                End If

                cmd = New SqlClient.SqlCommand("get_customer_for_support")
                cmd.Parameters.AddWithValue("@pi_filter", s)
                cmd.CommandType = CommandType.StoredProcedure
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                lstClient.DataSource = ds.Tables(0).DefaultView
                lstClient.DataTextField = "customer_name"
                lstClient.DataValueField = "customer_sys_id"
                lstClient.DataBind()
            Catch
                lblError.Text = "Ошибка загрузки информации о клиентах!<br>" & Err.Description
            End Try
            lstClient.Enabled = True
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

        Private Sub LoadManagerList()
            Dim sql$ = "select * from employee where inactive=0 order by name"
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter(sql)
                ds = New DataSet
                adapt.Fill(ds)
                lstManager.DataSource = ds.Tables(0).DefaultView
                lstManager.DataTextField = "name"
                lstManager.DataValueField = "sys_id"
                lstManager.DataBind()
            Catch
                lblError.Text = "Ошибка загрузки информации о клиентах!<br>" & Err.Description
            End Try
            lstManager.Enabled = True
        End Sub

        Private Sub rblReportType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblReportType.SelectedIndexChanged
            If rblReportType.SelectedValue = 1 Then
                pnlClient.Visible = True
                pnlGoods.Visible = False
                pnlManager.Visible = False
            ElseIf rblReportType.SelectedValue = 2 Then
                pnlClient.Visible = False
                pnlGoods.Visible = True
                pnlManager.Visible = False
            Else
                pnlClient.Visible = False
                pnlGoods.Visible = False
                pnlManager.Visible = True
            End If

            If rblReportType.SelectedValue = 3 Then
                chkManager.Visible = True
            Else
                chkManager.Visible = False
            End If
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

            Dim strRequest$ = "rt=" & rblReportType.SelectedValue
            strRequest &= "&db=" & Format(startdate, "MM/dd/yyyy")
            strRequest &= "&de=" & Format(endDate, "MM/dd/yyyy")

            If CInt(rblReportType.SelectedValue) = 1 Then
                If rbtnClientSet1.Checked Then
                    strRequest &= "&cs=1"
                ElseIf rbtnClientSet2.Checked Then
                    strRequest &= "&cs=2"
                ElseIf rbtnClientSet3.Checked Then
                    strRequest &= "&cs=3"
                ElseIf rbtnClientSet4.Checked Then
                    strRequest &= "&cs=4"
                    strRequest &= "&cust=" & GetList(lstClient)
                End If
                strRequest = "<script language='javascript' type='text/javascript'>window.open('SellingReport.aspx?" & strRequest & "')</script>"
            ElseIf CInt(rblReportType.SelectedValue) = 2 Then
                If rbtnGootTypeSet1.Checked Then
                    strRequest &= "&cs=1"
                ElseIf rbtnGootTypeSet2.Checked Then
                    strRequest &= "&cs=2"
                    strRequest &= "&g_t=" & GetList(lstGoodType)
                End If
                strRequest = "<script language='javascript' type='text/javascript'>window.open('SellingReportByGoods.aspx?" & strRequest & "')</script>"
            ElseIf CInt(rblReportType.SelectedValue) = 3 Then
                If rbtnManagerSet1.Checked Then
                    strRequest &= "&cs=1"
                ElseIf rbtnManagerSet2.Checked Then
                    strRequest &= "&cs=2"
                    strRequest &= "&mana=" & GetList(lstManager)
                End If

                If chkManager.Checked = True Then
                    strRequest &= "&first_sale=1"
                Else
                    strRequest &= "&first_sale=0"
                End If

                strRequest = "<script language='javascript' type='text/javascript'>window.open('SellingReportByManager.aspx?" & strRequest & "')</script>"
            ElseIf CInt(rblReportType.SelectedValue) = 4 Then
                If rbtnManagerSet1.Checked Then
                    strRequest &= "&cs=1"
                ElseIf rbtnManagerSet2.Checked Then
                    strRequest &= "&cs=2"
                    strRequest &= "&mana=" & GetList(lstManager)
                End If

                strRequest &= "&ttype=2"
                strRequest = "<script language='javascript' type='text/javascript'>window.open('SellingReportByManager.aspx?" & strRequest & "')</script>"
            End If

            Me.RegisterStartupScript("report", strRequest)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

        Protected Sub rbtnClientSet2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtnClientSet2.CheckedChanged
            'Если выбрали Только ЦТО, чтобы выгрузился список
            If rbtnClientSet2.Checked = True Then
                Dim s$ = " cto=1 "
                LoadCustomerList(s)
            End If
        End Sub
    End Class

End Namespace
