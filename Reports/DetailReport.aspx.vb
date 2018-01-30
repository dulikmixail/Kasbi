Imports Microsoft.Office.Interop

Namespace Kasbi.Reports

    Partial Class DetailReport
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
        Dim date_start As DateTime
        Dim date_end As DateTime
        Dim details As String
        Dim disrepair As String
        Dim Executor As String

        Dim xlsApp As Excel.ApplicationClass
        Private sheet As Excel.WorksheetClass
        Private book As Excel.WorkbookClass

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                date_start = CDate(Request.QueryString("start_date"))
                date_end = CDate(Request.QueryString("end_date"))
            Catch
                date_start = Now.AddMonths(-1)
                date_end = Now
            End Try

            details = Request.QueryString("dt")
            Executor = Request.QueryString("ex")
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet = New DataSet
            Try
                lblStartDate.Text = Format(date_start, "dd.MM.yyyy")
                lblEndDate.Text = Format(date_end, "dd.MM.yyyy")
                lblPrintDate.Text = Format(Now, "dd.MM.yyyy")

                cmd = New SqlClient.SqlCommand("prc_rpt_Detailrepair")
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.AddWithValue("@pi_begin_date", date_start)
                cmd.Parameters.AddWithValue("@pi_end_date", date_end)

                If Not details Is Nothing AndAlso details <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_details", details)
                End If
                If Not Executor Is Nothing AndAlso Executor <> String.Empty Then
                    cmd.Parameters.AddWithValue("@pi_executor", Executor)
                End If

                'If Not disrepair Is Nothing AndAlso disrepair <> String.Empty Then
                'cmd.Parameters.AddWithValue("@pi_disrepair", disrepair)
                'End If
                'If Not Executor Is Nothing AndAlso Executor <> String.Empty Then
                'cmd.Parameters.AddWithValue("@pi_executor", Executor)
                'End If

                adapt = dbSQL.GetDataAdapter(cmd)
                adapt.Fill(ds)

                'ProcessKKMListTO()

                grid.DataSource = ds.Tables(0).DefaultView
                grid.DataBind()
            Catch
            End Try
        End Sub

        Function ProcessKKMListTO()
            Dim j, a As Integer
            Dim ds As DataSet

            Dim dv As DataView
            Dim docFullPath$
            Dim path$ = Server.MapPath("Docs")
            docFullPath = path & "\Detail_Report.xls"
            Dim fls As IO.File
            Dim fl As IO.FileInfo

            Try
                'Create instance of Excel! 
                xlsApp = New Excel.Application
            Catch ex As Exception
                MsgBox("Œ¯Ë·Í‡")
            End Try

            a = 0

            book = xlsApp.Workbooks.Open(docFullPath)
            dv = ds.Tables(0).DefaultView

            With ds.Tables(0)
                For j = 0 To .Rows.Count - 1
                    book.ActiveSheet.Range("A" & CStr(3 + a)).Value = Format(dv.Item(j)("detail_id"))
                    book.ActiveSheet.Range("B" & CStr(3 + a)).Value = Format(dv.Item(j)("detail_name"))
                    book.ActiveSheet.Range("C" & CStr(3 + a)).Value = Format(dv.Item(j)("detail_notation"))
                    book.ActiveSheet.Range("D" & CStr(3 + a)).Value = Format(dv.Item(j)("num"))
                    book.ActiveSheet.Range("E" & CStr(3 + a)).Value = Format(dv.Item(j)("price"))
                    a = a + 1
                Next
            End With

            book.Save()

            ds.Clear()
            xlsApp.SaveWorkspace()
            xlsApp.Quit()

        End Function

    End Class
End Namespace
