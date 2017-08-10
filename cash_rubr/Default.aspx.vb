Namespace Kasbi.Reports
    Partial Class _Default1
        Inherits PageBase
        Dim cash

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
            cash = Request.Params(0)
            If Not IsPostBack Then
                Bind()
            End If
        End Sub

        Private Sub Bind()
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Try
                adapt = dbSQL.GetDataAdapter("select * from client_rubr order by id_show ")
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
                '
                'Достаем информацию о рубриках для ккм
                '
                Dim count_rubr
                Dim reader As SqlClient.SqlDataReader
                Dim query = "SELECT count(id_rubr) FROM cash_rubr WHERE good_sys_id='" & cash & "' and id_rubr='" & e.Item.DataItem("id") & "'"

                reader = dbSQL.GetReader(query)
                If reader.Read() Then
                    Try
                        count_rubr = reader.Item(0)
                    Catch

                    End Try
                Else
                End If
                reader.Close()

                If count_rubr > 0 Then
                    CType(e.Item.FindControl("cbxSelect"), CheckBox).Checked = True
                End If

                CType(e.Item.FindControl("cbxSelect"), CheckBox).Text = "<b>" & e.Item.DataItem("id_show") & ".</b> " & e.Item.DataItem("Name")
            End If

        End Sub

        Protected Sub cmdEdit_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles cmdEdit.Click
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim j, n
            Dim query

            query = "DELETE FROM cash_rubr WHERE good_sys_id='" & cash & "'"
            adapt = dbSQL.GetDataAdapter(query)
            ds = New DataSet
            adapt.Fill(ds)

            For j = 0 To grdGoodGroups.Items.Count - 1
                If CType(grdGoodGroups.Items(j).FindControl("cbxSelect"), CheckBox).Checked = True Then
                    query = "INSERT INTO cash_rubr VALUES ('" & grdGoodGroups.DataKeys.Item(j) & "', '" & cash & "')"
                    adapt = dbSQL.GetDataAdapter(query)
                    ds = New DataSet
                    adapt.Fill(ds)



                End If
            Next

        End Sub



    End Class

End Namespace
