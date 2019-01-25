Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Kasbi.Reports
    Partial Class HistorySKNO
        Inherits PageBase
        Const ClearString = "-------"
        Const NotHaveData As String = "Нет данных для экспорта!"
        Dim startdate As DateTime = New DateTime
        Dim endDate As DateTime = New DateTime
        Dim WithEvents oExcel As Microsoft.Office.Interop.Excel.Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ClearError()
            If Not IsPostBack Then
                LoadData()
                LoadEmployee()
            End If
        End Sub

        Private Sub LoadData()
            InitComonents()
        End Sub

        Private Sub InitComonents()
            tbxBeginDate.Text = DateTime.Today.AddDays(- DateTime.Today.Day + 1).ToString("dd.MM.yyyy")
            tbxEndDate.Text = DateTime.Today.ToString("dd.MM.yyyy")
        End Sub

        Private Function ValidateData() As Boolean
            ClearError()
            Try
                startdate = DateTime.Parse(tbxBeginDate.Text)
                endDate = DateTime.Parse(tbxEndDate.Text)
                If (startdate > endDate) Then
                    lblError.Text = "Конечная дата должна быть меньше начальной"
                    Return False
                End If
            Catch
                lblError.Text = "Пожалуйста, введите корректные значения дат"
                Return False
            End Try
            Return True
        End Function

        Protected Sub btnView_OnClick(sender As Object, e As ImageClickEventArgs) Handles btnView.Click
            If ValidateData() Then
                export_History_SKNO_to_Excel()
            End If
        End Sub

        Private Sub export_History_SKNO_to_Excel()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim executorId = 0

            If lstEmployee.SelectedValue <> ClearString
                executorId = Convert.ToInt32(lstEmployee.SelectedValue)
            End If
            startdate = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            endDate = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_kkm_skno_history")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@pi_date_start", startdate)
            cmd.Parameters.AddWithValue("@pi_date_end", endDate)
            cmd.Parameters.AddWithValue("@pi_executor", executorId)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            CreateAndSendFileHistorySKNO(ds, "kkm_skno_history.xlsx")
        End Sub

        Private Sub LoadEmployee()
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
                lstEmployee.Items.Insert(0, New ListItem(ClearString, ClearString))
            Catch
            End Try
        End Sub

        Private Sub ClearError()
            lblError.Text = "&nbsp;"
        End Sub
        Private Sub ShowNotHaveData()
            lblError.Text = NotHaveData
        End Sub

        Private Sub CreateAndSendFileHistorySkno(ds As DataSet, fileName As String)
            Dim oExcel As Application
            Dim oBook As Workbook
            Dim oSheet As Worksheet
            Dim docPath, savePath As String
            Dim file As IO.FileInfo
            Dim drs() As DataRow
            Dim iFirstTableRow = 2

            drs = ds.Tables(0).Select()
            If drs.Count = 0
                ShowNotHaveData()
                Exit Sub
            End If

            docPath = Server.MapPath("~") & "Templates\" & fileName
            savePath = Server.MapPath("~") & "Docs\TO\" & Session("User").sys_id.ToString() & "\" & fileName
            CopyFile(docPath, savePath, overwrite := True)

            oExcel = New ApplicationClass()
            oExcel.DisplayAlerts = False
            oBook = oExcel.Workbooks.Open(savePath)
            oSheet = oBook.ActiveSheet

            For i As Integer = 0 To drs.Length - 1
                oSheet.Cells(iFirstTableRow + i, 1).Value = i + 1
                oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item("name")
                oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item("address")
                oSheet.Cells(iFirstTableRow + i, 4).Value = drs(i).Item("registration_number_skno")
                oSheet.Cells(iFirstTableRow + i, 5).Value = drs(i).Item("date_update")
            Next

            oSheet.Range("A" & iFirstTableRow & ":E" & drs.Length + 1).Borders.LineStyle = 1

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
    End Class
End Namespace
