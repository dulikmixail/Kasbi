Imports Microsoft.Office.Interop.Excel
Imports Microsoft.VisualBasic.FileIO.FileSystem

Namespace Kasbi.Reports
    Partial Class HistorySKNO
        Inherits PageBase
        Dim startdate As DateTime = New DateTime
        Dim endDate As DateTime = New DateTime
        Dim WithEvents oExcel As Microsoft.Office.Interop.Excel.Application
        Dim WithEvents oBook As Workbook
        Dim WithEvents oSheet As Worksheet

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                LoadData()
            End If
        End Sub

        Private Sub LoadData()
            InitComonents()
        End Sub

        Private Sub InitComonents()
            tbxBeginDate.Text = DateTime.Today.AddDays(-DateTime.Today.Day + 1).ToString("dd.MM.yyyy")
            tbxEndDate.Text = DateTime.Today.ToString("dd.MM.yyyy")
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

        Protected Sub btnView_OnClick(sender As Object, e As ImageClickEventArgs) Handles btnView.Click
            If ValidateData() Then
                export_History_SKNO_to_Excel()
            End If
        End Sub

        Private Sub export_History_SKNO_to_Excel()
            Dim cmd As SqlClient.SqlCommand

            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet

            Dim startdate2 = DateTime.Parse(tbxBeginDate.Text + " 00:00:00")
            Dim enddate2 = DateTime.Parse(tbxEndDate.Text + " 23:59:59")

            cmd = New SqlClient.SqlCommand("get_kkm_skno_history")
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Clear()
            cmd.Parameters.AddWithValue("@date_start", startdate)
            cmd.Parameters.AddWithValue("@date_end", endDate)

            adapt = dbSQL.GetDataAdapter(cmd)
            ds = New DataSet
            adapt.Fill(ds)

            createAndSendFileHistorySKNO(ds, "kkm_skno_history.xlsx")

        End Sub

        Private Sub createAndSendFileHistorySKNO(ds As DataSet, fileName As String)
            Dim docPath, savePath As String
            Dim file As IO.FileInfo
            Dim drs() As DataRow
            Dim iFirstTableRow = 2

            docPath = Server.MapPath("~") & "\Templates\" & fileName
            savePath = Server.MapPath("~") & "\Docs\TO\" & Session("User").sys_id & "\" & fileName
            CopyFile(docPath, savePath, overwrite:=True)

            oExcel = New ApplicationClass()
            oExcel.DisplayAlerts = False
            oBook = oExcel.Workbooks.Open(savePath)
            oSheet = oBook.ActiveSheet

            drs = ds.Tables(0).Select()

            For i As Integer = 0 To drs.Length - 1
                oSheet.Cells(iFirstTableRow + i, 1).Value = i + 1
                oSheet.Cells(iFirstTableRow + i, 2).Value = drs(i).Item(0)
                oSheet.Cells(iFirstTableRow + i, 3).Value = drs(i).Item(1)
                oSheet.Cells(iFirstTableRow + i, 5).Value = drs(i).Item(2)
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
