Imports System.IO
Imports Microsoft.Office.Interop.Excel

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

        Dim startdate
        Dim enddate

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
        Dim query
        Dim countRest% = 0
        Dim groupName$ = ""
        Dim good_sys_id = ""
        Dim WithEvents oExcel As Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet

        Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            'Ограничение прав
            If Session("rule1") = "0" Then
                lnkBookKeeping.Visible = False
            End If

            If Session("rule2") = "0" Then
                lnkMain.Visible = False
            End If

            If CurrentUser.permissions <> 4 Then
                lblExport.Visible = False
            End If

            If Session("rule3") = "0" Then
                lnkNeopl.Visible = False
            End If

            If Session("rule5") = "0" Then
                btnNew.Visible = False
            End If



            If Not IsPostBack Then
                lstcash.Visible = False

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
        
        Protected Sub lnk_search_cash_Click(sender As Object, e As EventArgs) Handles lnk_search_cash.Click

            If txtRequest.Text.Trim.Length > 5 Then


                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader

                cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                lstcash.Visible = True
                lstcash.Items.Clear()

                Dim i = 0

                While rs.Read
                    'MsgBox(rs(1))
                    lstcash.Items.Add("№" & rs(0) & " " & rs(1) & " " & rs(2) & " " & rs(3))
                    i = i + 1
                End While

                If i = 0 Then
                    lstcash.Visible = False
                End If


                rs.Close()


            Else
                lstcash.Items.Clear()
                lstcash.Visible = False
            End If

        End Sub

        Protected Sub btnRequest_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRequest.Click

            If lstcash.SelectedIndex >= 0 Then

                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader

                cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                Dim i = 0

                While rs.Read
                    If i = lstcash.SelectedIndex Then
                        good_sys_id = rs(4)
                    End If
                    i = i + 1
                End While

                rs.Close()


                If good_sys_id > 0 Then
                    Session("repair-filter") = " where good.good_sys_id=" & good_sys_id & " "
                    Response.Redirect(GetAbsoluteUrl("~/RepairMaster.aspx"))
                End If



            End If




        End Sub

        Protected Sub lnksetRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnksetRepair.Click
            If lstcash.SelectedIndex >= 0 Then

                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader

                cmd = New SqlClient.SqlCommand("select num_cashregister,num_control_reestr,num_control_pzu,num_control_mfp,good_sys_id from good where num_cashregister LIKE '%" & txtRequest.Text.Trim & "%' ORDER BY good_sys_id")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                Dim i = 0

                While rs.Read
                    If i = lstcash.SelectedIndex Then
                        good_sys_id = rs(4)
                    End If
                    i = i + 1
                End While

                rs.Close()


                If good_sys_id > 0 Then
                    Response.Redirect(GetAbsoluteUrl("~/SetRepair.aspx?id=" & good_sys_id))
                End If



            End If


            'query = dbSQL.ExecuteScalar("SELECT good_sys_id FROM good WHERE num_cashregister='" & txtRequest.Text.Trim & "'")

            'If Not query Is DBNull.Value And query > 1 Then
            'Response.Redirect(GetAbsoluteUrl("~/SetRepair.aspx?id=" & query))
            'End If
        End Sub

        Protected Sub lnkShowRepair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkShowRepair.Click
            Session("repair-filter") = " where good.inrepair='1' "
            Response.Redirect(GetAbsoluteUrl("~/RepairMaster.aspx"))
        End Sub

        Private Sub radioButtonListExport_SelectedIndexChanged(sender As Object, e As EventArgs) Handles radioButtonListExport.SelectedIndexChanged
            If radioButtonListExport.SelectedValue = "toHistoryByEmployee" 
                lstEmployee.Visible = True
            Else 
                lstEmployee.Visible = False
            End If
        End Sub

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

                FileOpen(1, Server.MapPath("XML") & "\new_customer.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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

                FileOpen(1, Server.MapPath("XML") & "\new_sales.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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
            Dim f As IO.File
            Dim fs As IO.FileStream
            Dim i% = 0

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

            ElseIf radioButtonListExport.SelectedValue = "warrantyHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 1)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 5)

            ElseIf radioButtonListExport.SelectedValue = "notWorkHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 1)
                cmd.Parameters.AddWithValue("@pi_state", 5)

            ElseIf radioButtonListExport.SelectedValue = "standartHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 5)

            ElseIf radioButtonListExport.SelectedValue = "toHistory" Then
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 1)

            Else
                cmd.Parameters.AddWithValue("@isWarranty", 0)
                cmd.Parameters.AddWithValue("@isNotWork", 0)
                cmd.Parameters.AddWithValue("@pi_state", 1)

            End If

            rs = dbSQL.GetReader(cmd)
            'MsgBox(lbxExecutor.SelectedValue)
            FileOpen(1, Server.MapPath("XML") & "\new_history.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
            PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
            PrintLine(1, "<history>")
            While rs.Read
                Print(1, rs(0))
                i = i + 1
            End While
            PrintLine(1)
            PrintLine(1, "</history>")

            FileClose(1)
            rs.Close()
            'Catch
            ' End Try
        End Sub

        Sub export_docs()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                Dim query
                query = "SELECT * FROM cash_history WHERE "

                cmd = New SqlClient.SqlCommand("get_xml_new_doc")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\new_docs.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("get_xml_ostatki_cash")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\ostatki.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
                PrintLine(1, "<?xml version='1.0' encoding='windows-1251' ?>")
                PrintLine(1, "<ostatki>")
                While rs.Read
                    'проверяем, пустое ли количество
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
                    'проверяем, пустое ли количество
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
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("get_good_forexport")
                cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\export_for_site.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from employee for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\employee.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("select * from customer for xml auto")
                'cmd.CommandType = CommandType.StoredProcedure
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\allcustomers.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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


            cmd = New SqlClient.SqlCommand("get_new_history")
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

            oExcel = New ApplicationClass()
            oExcel.DisplayAlerts = False
            oBook = oExcel.Workbooks.Add
            oSheet = oBook.Worksheets(1)
            oSheet.Columns("A").ColumnWidth = 20
            oSheet.Columns("B").ColumnWidth = 20
            oSheet.Columns("C").ColumnWidth = 50
            oSheet.Columns("D").ColumnWidth = 20
            oSheet.Columns("E").ColumnWidth = 30
            oSheet.Columns("F").ColumnWidth = 15
            'oSheet.Columns("A1:H1").Font.Bold = True
            oSheet.Range("A1").Value = "Исполнитель"
            oSheet.Range("B1").Value = "Дата проведения ТО"
            oSheet.Range("C1").Value = "Контраагент"
            oSheet.Range("D1").Value = "УНП"
            oSheet.Range("E1").Value = "ККМ"
            oSheet.Range("F1").Value = "Номер ККМ"

            dt = ds.Tables(0)
            drs = dt.Select()
            
            For i As Integer = 0 To drs.Length - 1
                oSheet.Range("A" & i+2).Value() = drs(i).Item(0)
                oSheet.Range("B" & i+2).Value() = drs(i).Item(1)
                oSheet.Range("C" & i+2).Value() = drs(i).Item(2)
                oSheet.Range("D" & i+2).Value() = drs(i).Item(3)
                oSheet.Range("E" & i+2).Value() = drs(i).Item(4)
                oSheet.Range("F" & i+2).Value() = drs(i).Item(5)
            Next
            
            docPath = Server.MapPath("XML") & "\TO_by_executor.xlsx"
            oBook.SaveAs(docPath)
            oExcel.Quit
            Threading.Thread.Sleep(1000)

            file = New System.IO.FileInfo(docPath)
            If file.Exists Then 'set appropriate headers
                Response.Clear()
                Response.AddHeader("Content-Disposition", "attachment; filename=" & file.Name)
                Response.AddHeader("Content-Length", file.Length.ToString())
                Response.ContentType = "application/octet-stream"
                Response.WriteFile(docPath)
                Response.End 'if file does not exist
            Else
                Response.Write("This file does not exist.")
            End If

            
        End Sub

        Public Function IsFileInUse(filename As String) As Boolean 
            Dim Locked As Boolean = False 
            Try 
                'Open the file in a try block in exclusive mode.  
                'If the file is in use, it will throw an IOException. 
                Dim fs As FileStream = File.Open(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None) 
                fs.Close() 
                ' If an exception is caught, it means that the file is in Use 
            Catch ex As IOException 
                Locked = True 
            End Try 
            Return Locked 
        End Function 


        Protected Sub lnk_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_export.Click
            If radioButtonListExport.SelectedValue = "toHistoryByEmployee"
                export_TO_by_executor()
            Else 
                export_customer()
                export_sale()
                export_history()
                export_docs()
                export_ostatki()
                export_site()
                export_user()
                export_allcustomers()
            End If

        End Sub


        
    End Class
End Namespace
