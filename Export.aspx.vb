Namespace Kasbi

Partial Class Export
        Inherits PageBase
    Protected WithEvents btnMain As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnCustomers As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnCTO As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnCatalog As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
    Protected WithEvents btnTO As System.Web.UI.WebControls.HyperLink

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
        End Sub

        Private Sub btnExportCustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportCustomers.Click
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0
                cmd = New SqlClient.SqlCommand("get_xml_customers")
                cmd.CommandType = CommandType.StoredProcedure

                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\customers.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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

                cmd = New SqlClient.SqlCommand("select count(*) from customer")
                Dim j = dbSQL.ExecuteScalar(cmd)
                lblCustomersInfo.Text = "Экспорт успешно завершен! Всего экспортировано записей: " & i & "(" & j & ")"
                lblCustomersInfo.NavigateUrl = "XML/customers.xml"
            Catch
                msg.Text = "Проблемы с экспортом!<br>" & Err.Description
            End Try
        End Sub

        Private Sub btnExportSales_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportSales.Click
            Dim cmd As SqlClient.SqlCommand
            Dim rs As SqlClient.SqlDataReader
            Dim f As IO.File
            Dim fs As IO.FileStream
            Dim i% = 0

            Try
                cmd = New SqlClient.SqlCommand("get_xml_new_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                rs = dbSQL.GetReader(cmd)

                FileOpen(1, Server.MapPath("XML") & "\sales.xml", OpenMode.Output, OpenAccess.Write, OpenShare.LockWrite)
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

                cmd = New SqlClient.SqlCommand("select count(*) from sale")
                Dim j = dbSQL.ExecuteScalar(cmd)
                lblSaleInfo.Text = "Экспорт успешно завершен! Всего экспортировано записей: " & i & "(" & j & ")"
                lblSaleInfo.NavigateUrl = "XML/sales.xml"
            Catch
                msg.Text = "Проблемы с экспортом!<br>" & Err.Description
            End Try
        End Sub

        Sub export_customer()
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0
                cmd = New SqlClient.SqlCommand("get_xml_new_customer")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)

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

                cmd = New SqlClient.SqlCommand("get_xml_new_sales")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
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
            Try
                Dim cmd As SqlClient.SqlCommand
                Dim rs As SqlClient.SqlDataReader
                Dim f As IO.File
                Dim fs As IO.FileStream
                Dim i% = 0

                cmd = New SqlClient.SqlCommand("get_xml_new_history")
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@date", Date.Today)
                rs = dbSQL.GetReader(cmd)
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
            Catch
            End Try
        End Sub

        Protected Sub lnk_exportSales_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_exportSales.Click
            export_sale()
        End Sub

        Protected Sub lnk_exportCustomers_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_exportCustomers.Click
            export_customer()
        End Sub

        Protected Sub lnk_exporthistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_exportHistory.Click
            export_history()
        End Sub
    End Class

End Namespace
