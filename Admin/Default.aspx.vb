Namespace Kasbi.Admin

    Partial Class _Default
        Inherits PageBase

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents lnkArticleGroup As System.Web.UI.WebControls.HyperLink
        Protected WithEvents lnkArticleCategory As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Hyperlink7 As System.Web.UI.WebControls.HyperLink


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Overloads Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Ограничение прав
            If Session("rule23") = "0" Then FormsAuthentication.RedirectFromLoginPage("*", True)

            'Put user code to initialize the page here
        End Sub


        Protected Sub lnk_export_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_export.Click

        End Sub

       
        Protected Sub lnk_clear_repair_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_clear_repair.Click
            dbSQL.ExecuteScalar("UPDATE good SET inrepair=null where good_sys_id NOT IN (SELECT DISTINCT good_sys_id FROM cash_history WHERE repairdate_out IS NULL AND state = 5)")
        End Sub

    End Class
End Namespace
