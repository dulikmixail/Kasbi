Imports Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Kasbi.Reports

    Partial Class ExecutorTORequest
        Inherits PageBase
        Dim startdate As DateTime = New DateTime
        Dim endDate As DateTime = New DateTime
        Dim WithEvents oExcel As Microsoft.Office.Interop.Excel.Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                LoadData()
                Bind()
            End If
        End Sub

        Private Sub LoadData()
            LoadMasters()
            InitComonents()
        End Sub

        Private Sub InitComonents()
            Dim dLastReportingDay As DateTime = DateTime.Today
            While (dLastReportingDay.DayOfWeek <> 4)
                dLastReportingDay = dLastReportingDay.AddDays(-1)
            End While
            tbxBeginDate.Text = dLastReportingDay.ToString("dd.MM.yyyy")
            tbxEndDate.Text = DateTime.Today.ToString("dd.MM.yyyy")
        End Sub

        Private Sub LoadMasters()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                Dim cmd = New SqlClient.SqlCommand("get_employee_by_role_id")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@pi_role_id", 0)
                adapt = dbSQL.GetDataAdapter(cmd)
                ds = New DataSet
                adapt.Fill(ds)
                ds.Tables(0).DefaultView.Sort = "Name"
                lbxExecutor.DataSource = ds.Tables(0).DefaultView
                lbxExecutor.DataValueField = "sys_id"
                lbxExecutor.DataTextField = "Name"
                lbxExecutor.DataBind()
            Catch
            End Try
        End Sub



        Private Sub Bind()
        End Sub

        Private Function ValidateData() As Boolean
            lblError.Visible = False
            Try
                startdate = DateTime.Parse(tbxBeginDate.Text)
                endDate = DateTime.Parse(tbxEndDate.Text)
                If (startdate > endDate) Then
                    lblError.Text = "Конечная дата должна быть меньше начальной"
                    lblError.Visible = True
                    Return False
                End If
            Catch
                lblError.Text = "Пожалуйста, введите корректные значения дат"
                lblError.Visible = True
                Return False
            End Try
            Return True
        End Function

        Sub createAndSendFileToByExecutor(ds As DataSet, fileName As String)
            Dim docPath, savePath As String
            Dim file As IO.FileInfo
            Dim drs() As DataRow
            Dim iFirstTableRow = 2

            docPath = Server.MapPath("~") & "Templates\TO_by_executor.xlsx"
            savePath = Server.MapPath("~") & "Docs\TO\" & Session("User").sys_id & "\" & fileName
            CopyFile(docPath, savePath, overwrite:=True)

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


            file = New IO.FileInfo(savePath)
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


        Sub export_TO_by_executor_to_Excel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_TO_by_executor_2")
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
            cmd.Parameters.AddWithValue("@pi_employee_sys_id", lbxExecutor.SelectedValue)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            createAndSendFileToByExecutor(ds, "TO_by_executor_2.xlsx")
        End Sub


        Protected Sub btnView_OnClick(sender As Object, e As ImageClickEventArgs) Handles btnView.Click
            If ValidateData() Then
                export_TO_by_executor_to_Excel()
            End If
        End Sub

        Protected Sub lnkExportReportToExcel_OnClick(sender As Object, e As EventArgs) Handles lnkExportReportToExcel.Click
            If ValidateData() Then
                export_TO_by_executor_to_Excel()
            End If
        End Sub
    End Class
End Namespace
