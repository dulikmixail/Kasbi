Imports System.IO
Imports System.Windows.Forms
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic.FileIO.FileSystem
Imports Models.Sms.Sending.Request
Imports Models.Sms.Sending.Response
Imports Models.Sms.Statusing.Response
Imports Newtonsoft.Json
Imports Scripting
Imports Service

Namespace Kasbi
    Partial Class frmDefault
        Inherits PageBase
        Protected WithEvents Hyperlink1 As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnNewGoodMain As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkExport As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkReports As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lblHallo As System.Web.UI.WebControls.Label
        Protected WithEvents lblDateRange As System.Web.UI.WebControls.Label
        Protected WithEvents lblSale As System.Web.UI.WebControls.Label
        Protected WithEvents lblSaleClient As System.Web.UI.WebControls.Label
        Protected WithEvents lblSaleCTO As System.Web.UI.WebControls.Label
        Protected WithEvents repGoodTypes As System.Web.UI.WebControls.Repeater

        Private ReadOnly _serviceExport As ServiceExport = New ServiceExport()

        Dim startdate
        Dim enddate

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

        Dim query
        Dim countRest% = 0
        Dim groupName$ = ""
        Dim good_sys_id = ""
        Dim WithEvents oExcel As Microsoft.Office.Interop.Excel.Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet
        Const NotHaveData As String = "Íåò äàííûõ äëÿ ýêñïîðòà!"

        Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Îãðàíè÷åíèå ïðàâ
            If Session("rule1") = "0" Then
                lnkBookKeeping.Visible = False
            End If

            If Session("rule2") = "0" Then
                lnkMain.Visible = False
            End If

            If CurrentUser.permissions <> 4 Then
                pnlExport.Visible = False
            End If

            If Session("rule3") = "0" Then
                lnkNeopl.Visible = False
            End If

            If Session("rule5") = "0" Then
                btnNew.Visible = False
            End If

            If toExcel.Checked Then
                radioButtonListExport.Items.FindByValue("removedFromTO").Enabled = True
                radioButtonListExport.Items.FindByValue("toHistorySpecialRules").Enabled = True
                radioButtonListExport.Items.FindByValue("unpAndDogovor").Enabled = True
            Else
                radioButtonListExport.Items.FindByValue("removedFromTO").Enabled = False
                radioButtonListExport.Items.FindByValue("toHistorySpecialRules").Enabled = False
                radioButtonListExport.Items.FindByValue("unpAndDogovor").Enabled = False
            End If

            If _
                Not radioButtonListExport.SelectedValue = "removedFromTO" And
                Not radioButtonListExport.SelectedValue = "toHistorySpecialRules" And
                Not radioButtonListExport.SelectedValue = "unpAndDogovor" Then
                lstEmployee.Visible = True
            Else
                lstEmployee.Visible = False
            End If

            If radioButtonListExport.SelectedValue = "unpAndDogovor" Then
                tbxBeginDate.Visible = False
                tbxEndDate.Visible = False
            Else
                tbxBeginDate.Visible = True
                tbxEndDate.Visible = True
            End If


            If Not IsPostBack Then
                'lstcash.Visible = False

                tbxBeginDate.Text = Date.Now.Day & "." & Date.Now.Month & "." & Date.Now.Year
                tbxEndDate.Text = Date.Now.Day & "." & Date.Now.Month & "." & Date.Now.Year
                load_employee()
            End If
        End Sub

        Sub load_employee()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            cmd = New SqlClient.SqlCommand("get_employee_by_role_id")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@pi_role_id", 0)
            Try
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                ds.Tables(0).DefaultView.Sort = "name"
                lstEmployee.DataSource = ds.Tables(0).DefaultView
                lstEmployee.DataTextField = "name"
                lstEmployee.DataValueField = "sys_id"
                lstEmployee.DataBind()
            Catch
            End Try
        End Sub

        'Protected Sub lnk_search_cash_Click(sender As Object, e As EventArgs) Handles lnk_search_cash.Click

        '    If txtRequest.Text.Trim.Length > 5 Then


        '        Dim cmd As SqlClient.SqlCommand
        '        Dim rs As SqlClient.SqlDataReader

        '        cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
        '        'cmd.CommandType = CommandType.StoredProcedure
        '        rs = dbSQL.GetReader(cmd)

        '        lstcash.Visible = True
        '        lstcash.Items.Clear()

        '        Dim i = 0

        '        While rs.Read
        '            'MsgBox(rs(1))
        '            lstcash.Items.Add("¹" & rs(0) & " " & rs(1) & " " & rs(2) & " " & rs(3))
        '            i = i + 1
        '        End While

        '        If i = 0 Then
        '            lstcash.Visible = False
        '        End If


        '        rs.Close()


        '    Else
        '        lstcash.Items.Clear()
        '        lstcash.Visible = False
        '    End If

        'End Sub

        'Protected Sub btnRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequest.Click

        '    If lstcash.SelectedIndex >= 0 Then

        '        Dim cmd As SqlClient.SqlCommand
        '        Dim rs As SqlClient.SqlDataReader

        '        cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
        '        'cmd.CommandType = CommandType.StoredProcedure
        '        rs = dbSQL.GetReader(cmd)

        '        Dim i = 0

        '        While rs.Read
        '            If i = lstcash.SelectedIndex Then
        '                good_sys_id = rs(4)
        '            End If
        '            i = i + 1
        '        End While

        '        rs.Close()


        '        If good_sys_id > 0 Then
        '            Session("repair-filter") = " where good.good_sys_id=" & good_sys_id & " "
        '            Response.Redirect(GetAbsoluteUrl("~/RepairMaster.aspx"))
        '        End If


        '    End If


        'End Sub


        'Protected Sub lnksetRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnksetRepair.Click
        '    If lstcash.SelectedIndex >= 0 Then

        '        Dim cmd As SqlClient.SqlCommand
        '        Dim rs As SqlClient.SqlDataReader

        '        cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
        '        'cmd.CommandType = CommandType.StoredProcedure
        '        rs = dbSQL.GetReader(cmd)

        '        Dim i = 0

        '        While rs.Read
        '            If i = lstcash.SelectedIndex Then
        '                good_sys_id = rs(4)
        '            End If
        '            i = i + 1
        '        End While

        '        rs.Close()


        '        If good_sys_id > 0 Then
        '            Response.Redirect(GetAbsoluteUrl("~/SetRepair.aspx?id=" & good_sys_id))
        '        End If


        '    End If


        '    'query = dbSQL.ExecuteScalar("SELECT good_sys_id FROM good WHERE num_cashregister='" & txtRequest.Text.Trim & "'")

        '    'If Not query Is DBNull.Value And query > 1 Then
        '    'Response.Redirect(GetAbsoluteUrl("~/SetRepair.aspx?id=" & query))
        '    'End If
        'End Sub

        'Protected Sub lnkShowRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowRepair.Click
        '    Session("repair-filter") = " where good.inrepair='1' "
        '    Response.Redirect(GetAbsoluteUrl("~/RepairMaster.aspx"))
        'End Sub

        Sub export_customer()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                startdate = DateTime.Parse(tbxBeginDate.Text)
                enddate = DateTime.Parse(tbxEndDate.Text)

                cmd = New SqlClient.SqlCommand("get_xml_new_customer")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                cmd.Parameters.AddWithValue("@date_start", startdate)
                cmd.Parameters.AddWithValue("@date_end", enddate)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_customer.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<Customers>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</Customers>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_sale()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                startdate = DateTime.Parse(tbxBeginDate.Text)
                enddate = DateTime.Parse(tbxEndDate.Text)

                cmd = New SqlClient.SqlCommand("get_xml_new_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                cmd.Parameters.AddWithValue("@date_start", startdate)
                cmd.Parameters.AddWithValue("@date_end", enddate)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_sales.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<Sales>")

                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While

                PrintLine(1)
                PrintLine(1, "</Sales>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_history()
            'Try
            Dim cmd As SqlClient.SqlCommand
            Dim rs As SqlClient.SqlDataReader
            Dim i% = 0
            Dim cashHistories As ArrayList = New ArrayList()
            Dim row As String = String.Empty

            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")


            cmd = New SqlClient.SqlCommand("get_xml_new_history")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)

            If radioButtonListExport.SelectedValue = "fullHistory" Then

                cmd.Parameters.AddWithValue("@isWarranty", 1)
                cmd.Parameters.AddWithValue("@isNotWork", 1)
                cmd.Parameters.AddWithValue("@pi_state", 5)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            ElseIf radioButtonListExport.SelectedValue = "warrantyHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 1)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 5)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            ElseIf radioButtonListExport.SelectedValue = "notWorkHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 1)
                cmd.Parameters.AddWithValue("@pi_state", 5)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            ElseIf radioButtonListExport.SelectedValue = "standartHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 5)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            ElseIf radioButtonListExport.SelectedValue = "toHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 1)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            Else
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 1)
                cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            End If

            rs = dbSQL.GetReader(cmd)
            If rs.HasRows
                FileOpen(1, Server.MapPath("XML") & "\new_history.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<history>")
                While rs.Read
                    row = rs(0).ToString()
                    cashHistories = ParseCashHistoryRow(row)
                    Print(1, row)
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</history>")

                FileClose(1)
                rs.Close()
            Else
                rs.Close()
                ShowNotHaveData()
            End If

            _serviceExport.LockCashHistory(cashHistories, ExportType.OneC)
        End Sub

        Sub export_docs()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim i% = 0

                Dim query
                query = "SELECT * FROM cash_history WHERE "

                cmd = New SqlClient.SqlCommand("get_xml_new_doc")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_docs.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<docs>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</docs>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_ostatki()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("get_xml_ostatki_cash")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\ostatki.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<ostatki>")
                While rs.Read
                    'ïðîâåðÿåì, ïóñòîå ëè êîëè÷åñòâî
                    Dim str = rs(0)
                    Dim find() = Split(str, "good_num")
                    If find.Length > 0 Then
                        Print(1, rs(0))
                        i = i + 1
                    End If
                End While
                rs.Close()

                cmd = New SqlClient.SqlCommand("get_xml_ostatki")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)
                While rs.Read
                    'ïðîâåðÿåì, ïóñòîå ëè êîëè÷åñòâî
                    Dim str = rs(0)
                    Dim find() = Split(str, "good_num")
                    If find.Length > 0 Then
                        Print(1, rs(0))
                        i = i + 1
                    End If
                End While
                rs.Close()

                PrintLine(1)
                PrintLine(1, "</ostatki>")

                FileClose(1)

            Catch
            End Try
        End Sub


        Sub export_site()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("get_good_forexport")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\export_for_site.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_user()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from employee for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\employee.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_allcustomers()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from customer for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\allcustomers.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<goods>")
                While rs.Read
                    Print(1, rs(0))
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</goods>")

                FileClose(1)
                rs.Close()
            Catch
            End Try
        End Sub

        Sub export_TO_by_executor()
            'Try
            Dim cmd As SqlClient.SqlCommand
            Dim rs As SqlClient.SqlDataReader
            Dim i% = 0
            Dim cashHistories As ArrayList = New ArrayList()
            Dim row As String = String.Empty

            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")


            cmd = New SqlClient.SqlCommand("get_xml_TO_by_executor")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)
            cmd.Parameters.AddWithValue("@isWarranty", 0)
            cmd.Parameters.AddWithValue("@isNotWork", 0)
            cmd.Parameters.AddWithValue("@pi_state", 1)
            cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            rs = dbSQL.GetReader(cmd)
            If rs.HasRows
                FileOpen(1, Server.MapPath("XML") & "\new_history.xml", OpenMode.Output, OpenAccess.Write,
                         OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<history>")
                While rs.Read
                    row = rs(0).ToString()
                    cashHistories = ParseCashHistoryRow(row)
                    Print(1, row)
                    i = i + 1
                End While
                PrintLine(1)
                PrintLine(1, "</history>")

                FileClose(1)
                rs.Close()
            Else
                rs.Close()
                ShowNotHaveData()
            End If
            _serviceExport.LockCashHistory(cashHistories, ExportType.OneC)
            'MsgBox(lbxExecutor.SelectedValue)

            'Catch
            ' End Try
        End Sub
        Private Function ParseCashHistoryRow(row As String) As ArrayList
            Dim matchCollection As MatchCollection
            Dim cashHistories As ArrayList = New ArrayList()

            matchCollection = Regex.Matches(row, "(<cash_history sys_id="")([0-9]+?)("")")
                    For Each match As Match In matchCollection
                        cashHistories.Add(match.Groups.Item(2).Value)
                    Next
            Return cashHistories
        End Function

        Private Sub CreateAndSendFileHistory(ds As DataSet, fileName As String)
            If ds.Tables(0).Rows.Count > 0
                Dim docPath, savePath As String
                Dim drs() As Data.DataRow
                Dim iFirstTableRow = 4
                docPath = Server.MapPath("Templates\") & fileName
                savePath = Server.MapPath("Docs") & "\repair\" & Session("User").sys_id & "\" & fileName
                CopyFile(docPath, savePath, overwrite := True)

                oExcel = New ApplicationClass()
                oExcel.DisplayAlerts = False
                oBook = oExcel.Workbooks.Open(savePath)
                oSheet = oBook.ActiveSheet

                drs = ds.Tables(0).Select()

                Dim selection As Range = oSheet.Range("A4:H4")
                selection.Cut(selection.Offset(drs.Length, 0))

                If lstEmployee.SelectedValue.Length <> 0 Then
                    oSheet.Cells(2, 1).Value = radioButtonListExport.SelectedItem.Text & " (" & tbxBeginDate.Text &
                                               " - " &
                                               tbxEndDate.Text & ") - " & lstEmployee.SelectedItem.Text
                Else
                    oSheet.Cells(2, 1).Value = radioButtonListExport.SelectedItem.Text & " (" & tbxBeginDate.Text &
                                               " - " &
                                               tbxEndDate.Text & ")"
                End If
                For i As Integer = 0 To drs.Length - 1
                    oSheet.Cells(iFirstTableRow + i, 1).Value = i + 1
                    oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item(13)
                    oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item(18)
                    oSheet.Cells(iFirstTableRow + i, 4).Value = drs(i).Item(6)
                    oSheet.Cells(iFirstTableRow + i, 5).Value = drs(i).Item(4)
                    oSheet.Cells(iFirstTableRow + i, 6).Value = drs(i).Item(22)
                    oSheet.Cells(iFirstTableRow + i, 7).Value = drs(i).Item(21)
                    oSheet.Cells(iFirstTableRow + i, 8).Value = drs(i).Item(20)
                Next

                oSheet.Range("A" & iFirstTableRow & ":H" & iFirstTableRow + drs.Length).Borders.LineStyle = 1
                oSheet.Cells(iFirstTableRow + drs.Length, 7).Value =
                    oExcel.WorksheetFunction.Sum(oSheet.Range("G" & iFirstTableRow & ":G" & iFirstTableRow + drs.Length))
                oSheet.Cells(iFirstTableRow + drs.Length, 8).Value =
                    oExcel.WorksheetFunction.Sum(oSheet.Range("H" & iFirstTableRow & ":H" & iFirstTableRow + drs.Length))

                oBook.Close(True, savePath, True)
                oExcel.Quit()

                SendFile(savePath)
            Else
                ShowNotHaveData()
            End If
        End Sub

        Private Sub ÑreateAndSendFileToByExecutor(ds As DataSet, fileName As String)
            If ds.Tables(0).Rows.Count > 0
                Dim docPath, savePath As String
                Dim drs() As Data.DataRow
                Dim iFirstTableRow = 2

                docPath = Server.MapPath("Templates\") & "TO_by_executor.xlsx"
                savePath = Server.MapPath("Docs") & "\TO\" & Session("User").sys_id & "\" & fileName
                CopyFile(docPath, savePath, overwrite := True)

                oExcel = New ApplicationClass()
                oExcel.DisplayAlerts = False
                oBook = oExcel.Workbooks.Open(savePath)
                oSheet = oBook.ActiveSheet

                drs = ds.Tables(0).Select()

                Dim selection As Range = oSheet.Range("B7:D13")
                selection.Font.Size = 16
                selection.Cut(selection.Offset(drs.Length, 0))

                For i As Integer = 0 To drs.Length - 1
                    oSheet.Cells(iFirstTableRow + i, 1).Value = i + 1
                    oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item(0)
                    oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item(1)
                    oSheet.Cells(iFirstTableRow + i, 4).Value = drs(i).Item(2)
                    oSheet.Cells(iFirstTableRow + i, 5).Value = drs(i).Item(3)
                    oSheet.Cells(iFirstTableRow + i, 6).Value = drs(i).Item(5)
                    oSheet.Cells(iFirstTableRow + i, 7).Value = drs(i).Item(9)
                Next

                oSheet.Range("A" & iFirstTableRow & ":G" & drs.Length + 1).Borders.LineStyle = 1

                oBook.Close(True, savePath, True)
                oExcel.Quit()

                SendFile(savePath)
            Else
                ShowNotHaveData()
            End If
        End Sub

        Private Sub CreateAndSendFileRemovedFromTo(ds As DataSet, fileName As String)
            If ds.Tables(0).Rows.Count > 0
                Dim docPath, savePath As String
                Dim drs() As Data.DataRow
                Dim iFirstTableRow = 2

                docPath = Server.MapPath("Templates\") & "removed_from_TO.xlsx"
                savePath = Server.MapPath("Docs") & "\TO\" & Session("User").sys_id & "\" & fileName
                CopyFile(docPath, savePath, overwrite := True)

                oExcel = New ApplicationClass()
                oExcel.DisplayAlerts = False
                oBook = oExcel.Workbooks.Open(savePath)
                oSheet = oBook.ActiveSheet

                drs = ds.Tables(0).Select()

                Dim selection As Range = oSheet.Range("B7:D13")
                selection.Font.Size = 16
                selection.Cut(selection.Offset(drs.Length, 0))

                For i As Integer = 0 To drs.Length - 1
                    oSheet.Cells(iFirstTableRow + i, 1).Value = i + 1
                    oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item(3)
                    oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item(0)
                    oSheet.Cells(iFirstTableRow + i, 4).Value = drs(i).Item(1)
                    oSheet.Cells(iFirstTableRow + i, 5).Value = drs(i).Item(2)
                Next

                oSheet.Range("A" & iFirstTableRow & ":E" & drs.Length + 1).Borders.LineStyle = 1

                oBook.Close(True, savePath, True)
                oExcel.Quit()

                SendFile(savePath)
            Else
                ShowNotHaveData()
            End If
        End Sub

        Sub createAndSendFileUnpAndDogovor(ds As DataSet, fileName As String)
            If ds.Tables(0).Rows.Count > 0
                Dim docPath, savePath As String
                Dim drs() As Data.DataRow
                Dim iFirstTableRow = 1
                Dim fso As New FileSystemObject
                Dim folder As String
                Dim subFolder As String

                Try
                    docPath = Server.MapPath("Templates\") & fileName
                    savePath = Server.MapPath("Docs") & "\TO\" & Session("User").sys_id & "\" & fileName
                    CopyFile(docPath, savePath, overwrite := True)

                    oExcel = New ApplicationClass()
                    oExcel.DisplayAlerts = False
                    oBook = oExcel.Workbooks.Open(savePath)
                    oSheet = oBook.ActiveSheet

                    drs = ds.Tables(0).Select()

                    For i As Integer = 0 To drs.Length - 1
                        oSheet.Cells(iFirstTableRow + i, 1).Value = drs(i).Item(0)
                        oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item(1)
                        oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item(2)

                    Next

                    oBook.Close(True, savePath, True)
                    oExcel.Quit()

                    SendFile(savePath)
                    oExcel.Quit()
                Catch ex As Exception
                    oExcel.Quit()
                End Try
            Else
                ShowNotHaveData()
            End If
        End Sub


        Sub export_warrantyHistory_toExcel()
            export_history_toExcel(1, 0, 5)
        End Sub

        Sub export_notWorkHistory_toExcel()
            export_history_toExcel(0, 1, 5)
        End Sub

        Sub export_standartHistory_toExcel()
            export_history_toExcel(0, 0, 5)
        End Sub

        Private Sub export_history_toExcel(isWarranty As Integer, isNotWork As Integer, state As Integer)
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_new_history")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)
            cmd.Parameters.AddWithValue("@isWarranty", isWarranty)
            cmd.Parameters.AddWithValue("@isNotWork", isNotWork)
            cmd.Parameters.AddWithValue("@pi_state", state)
            cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)
            createAndSendFileHistory(ds, "Repair_history.xlsx")
        End Sub

        Sub export_TObyExecutor_toExcel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_TO_by_executor")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)
            cmd.Parameters.AddWithValue("@isWarranty", 0)
            cmd.Parameters.AddWithValue("@isNotWork", 0)
            cmd.Parameters.AddWithValue("@pi_state", 1)
            cmd.Parameters.AddWithValue("@pi_employee_sys_id", lstEmployee.SelectedValue)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            ÑreateAndSendFileToByExecutor(ds, "TO_by_executor.xlsx")
        End Sub

        Sub export_TOSpecialRules_toExcel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim docPath As String
            Dim file As IO.FileInfo
            Dim fileNotBusy As Boolean
            Dim dt As Data.DataTable
            Dim drs() As Data.DataRow

            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_TO_special_rules")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)
            cmd.Parameters.AddWithValue("@isWarranty", 0)
            cmd.Parameters.AddWithValue("@isNotWork", 0)
            cmd.Parameters.AddWithValue("@pi_state", 1)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            ÑreateAndSendFileToByExecutor(ds, "TO_by_executor_sr.xlsx")
        End Sub

        Sub export_unpAndDogovor_toExcel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim docPath As String
            Dim file As IO.FileInfo
            Dim fileNotBusy As Boolean
            Dim dt As Data.DataTable
            Dim drs() As Data.DataRow
            cmd = New SqlClient.SqlCommand("get_unn_dogovor")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            createAndSendFileUnpAndDogovor(ds, "unn_dogovor.xlsx")
        End Sub


        Sub export_removedFromTO_toExcel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim docPath As String
            Dim file As IO.FileInfo
            Dim fileNotBusy As Boolean
            Dim dt As Data.DataTable
            Dim drs() As Data.DataRow
            startdate = DateTime.Parse(tbxBeginDate.Text)
            enddate = DateTime.Parse(tbxEndDate.Text)

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")


            cmd = New SqlClient.SqlCommand("get_removed_from_TO")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date", Date.Today)
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", enddate)
            cmd.Parameters.AddWithValue("@date_start2", startdate2)
            cmd.Parameters.AddWithValue("@date_end2", enddate2)


            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            createAndSendFileRemovedFromTo(ds, "removed_from_TO.xlsx")
        End Sub

        Sub SendFile(savePath As String)
            Dim file As IO.FileInfo
            file = New System.IO.FileInfo(savePath)
            If file.Exists Then
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(savePath)
                Response.End()
            Else
                Response.Write("This file does not exist.")
            End If
        End Sub


        Public Function IsFileInUse(filename As String) As Boolean
            Dim Locked As Boolean = False
            Try
                'Open the file in a try block in exclusive mode.  
                'If the file is in use, it will throw an IOException. 
                Dim fs As FileStream = IO.File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                                                    FileShare.None)
                fs.Close()
                ' If an exception is caught, it means that the file is in Use 
            Catch ex As IOException
                Locked = True
            End Try
            Return Locked
        End Function


        Protected Sub lnk_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_export.Click
            If radioButtonListExport.SelectedValue.Length <> 0 Then

                If toExcel.Checked Then
                    Select Case radioButtonListExport.SelectedValue
                        Case "warrantyHistory"
                            export_warrantyHistory_toExcel()
                        Case "notWorkHistory"
                            export_notWorkHistory_toExcel()
                        Case "standartHistory"
                            export_standartHistory_toExcel()
                        Case "toHistoryByEmployee"
                            export_TObyExecutor_toExcel()
                        Case "removedFromTO"
                            export_removedFromTO_toExcel()
                        Case "toHistorySpecialRules"
                            export_TOSpecialRules_toExcel()
                        Case "unpAndDogovor"
                            export_unpAndDogovor_toExcel()
                    End Select
                Else
                    Select Case radioButtonListExport.SelectedValue
                        Case "toHistoryByEmployee"
                            export_TO_by_executor()
                        Case Else
                            export_customer()
                            export_sale()
                            export_history()
                            export_docs()
                            export_ostatki()
                            export_site()
                            export_user()
                            export_allcustomers()
                    End Select

                End If

            Else
                msg.Text = "Âûáåðèòå ïóíêò äëÿ ýêñïîðòà"
            End If
        End Sub

        Private Sub ShowNotHaveData()
            msg.Text = NotHaveData
        End Sub

        'Protected Sub testSms_OnClick(sender As Object, e As EventArgs)
        '    Dim serviceSms As ServiceSms = New ServiceSms()
        '    Dim result As SmsSendingResponse = serviceSms.SendSameSms(New List(Of String) From {"375294010101", "375294010102"}, "Òåñòîâîå ÑÌÑ 1")
        '    Dim smsStatusing As SmsStatusingResponse = serviceSms.GetSmsStatusingBySmsSendingResponse(result)
        '    System.Diagnostics.Debug.WriteLine(JsonConvert.SerializeObject(smsStatusing))
        '    'Dim result1 As String = serviceSms.GetJsonManySmsWithSameText(New List(Of String) From{"375297561519","375296936700"},"Òåñòîâîå ÑÌÑ 2", DateTime.Now.AddDays(1))
        '    'Dim result2 As String = serviceSms.GetJsonManySmsWithDifferentText(New Dictionary(Of String, String) From 
        '    '                                                                      {
        '    '                                                                      {"375297561519", "Òåêñò 1"},
        '    '                                                                      {"375296936700", "Òåêñò 2"}
        '    '                                                                      }
        '    '                                                                   )
        '    'Dim result3 As String = serviceSms.GetJsonManySmsWithDifferentText(New Dictionary(Of String, String) From 
        '    '                                                                      {
        '    '                                                                      {"375297561519", "Òåêñò 3"},
        '    '                                                                      {"375296936700", "Òåêñò 4"}
        '    '                                                                      },
        '    '                                                                   DateTime.Now.AddDays(5)
        '    '                                                                   )

        '    Dim stop1 As Boolean = True
        'End Sub
    End Class
End Namespace
