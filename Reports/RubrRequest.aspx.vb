Imports System.Globalization
Imports System.Threading


Namespace Kasbi.Reports


    Partial Class RubrRequest
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

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from client_rubr order by name ")
                ds = New DataSet
                adapt.Fill(ds)
                grdGoodGroups.DataSource = ds.Tables(0).DefaultView
                grdGoodGroups.DataKeyField = "id"
                grdGoodGroups.DataBind()
            Catch
                'msgError.Text = "Ошибка загрузки информации о группах товара!<br>" & Err.Description
            End Try
        End Sub

        Protected Sub grdGoodGroups_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdGoodGroups.ItemDataBound

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Pager Then
                CType(e.Item.FindControl("lblRubr"), Label).Text = e.Item.DataItem("Name")

                'Достаем количество ККМ
                Dim count_cash
                Dim reader As SqlClient.SqlDataReader
                Dim query = "SELECT count(id_rubr) FROM cash_rubr WHERE id_rubr='" & e.Item.DataItem("id") & "'"
                reader = dbSQL.GetReader(query)
                If reader.Read() Then
                    Try
                        count_cash = reader.Item(0)
                    Catch
                    End Try
                Else
                End If
                reader.Close()
                CType(e.Item.FindControl("lblNumcash"), Label).Text = count_cash

                'Достаем количество клиентов
                '
                Try
                    Dim count_client
                    query = "SELECT COUNT(DISTINCT cash_history.owner_sys_id) AS Expr1 FROM cash_rubr " & _
                            "INNER JOIN cash_history ON cash_rubr.good_sys_id = cash_history.good_sys_id " & _
                            "INNER JOIN customer ON cash_history.owner_sys_id = customer.customer_sys_id WHERE (cash_rubr.id_rubr='" & e.Item.DataItem("id") & "')"
                    reader = dbSQL.GetReader(query)
                    If reader.Read() Then
                        Try
                            count_client = reader.Item(0)
                        Catch
                        End Try
                    Else
                    End If
                    reader.Close()
                    CType(e.Item.FindControl("lblNumclient"), Label).Text = count_client
                Catch ex As Exception
                End Try

            End If

        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Reports/Default.aspx"))
        End Sub

    End Class

End Namespace
