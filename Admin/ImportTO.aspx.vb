Imports System
Imports System.IO
Imports System.Xml
Imports System.Globalization



Namespace Kasbi.Admin

    Partial Class ImportTO
        Inherits PageBase


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents scrollPos As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents CurrentPage As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents Parameters As System.Web.UI.HtmlControls.HtmlInputHidden
        Protected WithEvents FindHidden As System.Web.UI.HtmlControls.HtmlInputHidden


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Dim table As DataTable
        Dim valid_count

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            msgImport.Text = ""
        End Sub

        Private Function CreateTable(ByVal tableName As String) As DataTable
            table = New DataTable(tableName)
            Dim col As DataColumn = New DataColumn("id", System.Type.GetType("System.Int32"))
            col.AllowDBNull = False
            col.AutoIncrement = True
            col.AutoIncrementSeed = 1

            table.Columns.Add(col)
            table.Columns.Add("unn", System.Type.GetType("System.String"))
            table.Columns.Add("name", System.Type.GetType("System.String"))
            table.Columns.Add("sum", System.Type.GetType("System.String"))
            CreateTable = table
        End Function

        Private Sub btnLoadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoadData.Click
            Dim i% = 0
            Try
                Dim doc As XmlDocument
                doc = New XmlDocument
                doc.Load(Server.MapPath("../XML/Import.xml"))
                Dim nodeList As XmlNode
                Dim root As XmlElement = doc.DocumentElement
                nodeList = root.SelectSingleNode("/Customers")
                Dim Customer As XmlNode

                'qqq
                Dim query = dbSQL.ExecuteScalar("DELETE FROM unn")

                CreateTable("import")
                Dim row As DataRow
                For Each Customer In nodeList.ChildNodes
                    Try
                        'Проверка на схожесть UNN названия клиента

                        Dim base_name = dbSQL.ExecuteScalar("SELECT customer_abr + ' ' + customer_name as customer_name FROM customer WHERE unn='" & Customer.Attributes.ItemOf("UNN").Value().Trim & "'")
                        'попытка
                        row = table.NewRow()
                        If base_name <> "" And Customer.Attributes.ItemOf("UNN").Value().Trim.Length = 9 Then
                            valid_count = valid_count + 1
                            'qqq
                            query = dbSQL.ExecuteScalar("INSERT INTO unn VALUES('" & Customer.Attributes.ItemOf("UNN").Value().Trim & "', '1', '1', '" & Customer.Attributes.ItemOf("Name").Value & "', '" & Double.Parse(Customer.Attributes.ItemOf("Sum").Value, CultureInfo.InvariantCulture) & "')")

                            If base_name = Customer.Attributes.ItemOf("Name").Value Then
                                row("name") = Customer.Attributes.ItemOf("Name").Value
                            Else
                                row("name") = "<span style=color:red><b>" & Customer.Attributes.ItemOf("Name").Value & "</b></span>"
                            End If
                            row("unn") = Customer.Attributes.ItemOf("UNN").Value()
                        Else
                            row("name") = Customer.Attributes.ItemOf("Name").Value
                            'qqq
                            query = dbSQL.ExecuteScalar("INSERT INTO unn VALUES('" & Customer.Attributes.ItemOf("UNN").Value().Trim & "', '0', '1', '" & Customer.Attributes.ItemOf("Name").Value & "', '" & Double.Parse(Customer.Attributes.ItemOf("Sum").Value, CultureInfo.InvariantCulture) & "')")

                            row("unn") = "<span style=color:red>" & Customer.Attributes.ItemOf("UNN").Value() & "</span>"
                        End If

                        If Customer.Attributes.ItemOf("Sum").Value.Trim() <> String.Empty Then
                            row("sum") = Double.Parse(Customer.Attributes.ItemOf("Sum").Value, CultureInfo.InvariantCulture)
                        Else
                            row("sum") = 0
                        End If

                        ' row("cto") = "" Customer.Attributes.ItemOf("CTO").Value
                        table.Rows.Add(row)
                    Catch
                        msgImport.Text = "ошибка тута!<br>" & Err.Description
                    End Try
                Next

                If ViewState("customersort") = "" Then
                    table.DefaultView.Sort = "id DESC"
                Else
                    table.DefaultView.Sort = ViewState("customersort")
                End If

                grdImportData.DataSource = table.DefaultView
                grdImportData.DataKeyField = "id"
                grdImportData.DataBind()
                msgImport.Text = "Загружено удачно " & table.DefaultView.Count & " записей из " & valid_count

            Catch
                msgImport.Text = "Проблемы с загрузкой данных!<br>" & Err.Description
            End Try
        End Sub

        Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdSave.Click
            Dim cmd As SqlClient.SqlCommand
            btnLoadData_Click(Me, Nothing)
            valid_count = 0

            If grdImportData.Items.Count = 0 Then
                msgImport.Text = "Данные не загружены."
                Exit Sub
            End If
            Try
                Dim row As DataRow
                For Each row In table.Rows
                    If Left(row("unn"), 5) = "<span" Then

                    Else
                        cmd = New SqlClient.SqlCommand("update_customer_dolg_by_UNP")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@pi_UNN", row("unn"))
                        cmd.Parameters.AddWithValue("@pi_dolg", CInt(row("sum")))
                        dbSQL.Execute(cmd)

                        valid_count = valid_count + 1
                    End If
                Next
                msgImport.Text = "Сохранено в базу " & valid_count & " из " & table.Rows.Count
            Catch
                msgImport.Text = Err.Description
            End Try

        End Sub

        Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdCancel.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub

        Private Sub grdImportData_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdImportData.SortCommand
            If ViewState("customersort") = e.SortExpression Then
                ViewState("customersort") = e.SortExpression & " DESC"
            Else
                ViewState("customersort") = e.SortExpression
            End If
            btnLoadData_Click(Me, Nothing)
        End Sub

    End Class

End Namespace
