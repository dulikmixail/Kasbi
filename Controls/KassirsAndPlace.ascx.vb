Namespace Kasbi.Controls

    Partial Class KassirsAndPlace
        Inherits System.Web.UI.UserControl

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
        Private currentPage As PageBase

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currentPage = Page
            Dim cmd As SqlClient.SqlCommand
            Dim adapt As SqlClient.SqlDataAdapter
            Dim ds As DataSet
            Dim sql$ = "select 	a.*, a.name_city + COALESCE(', ' + c.name_obl, ', ' + c.name_obl, '') + COALESCE(', ' +  b.name_rn, ', ' + b.name_rn, '') as CityFullName from City a left outer join rn b on a.id_rn = b.id_rn left outer join obl c on a.id_obl = c.id_obl order by a.name_city"

            If ddlCity.Items.Count = 0 Then
                Try
                    adapt = currentPage.dbSQL.GetDataAdapter(sql)
                    ds = New DataSet
                    adapt.Fill(ds)
                    ddlCity.DataSource = ds
                    ddlCity.DataValueField = "id_city"
                    ddlCity.DataTextField = "CityFullName"
                    ddlCity.SelectedIndex = 0
                    ddlCity.DataBind()
                Catch

                End Try
            End If
        End Sub

    End Class

End Namespace
