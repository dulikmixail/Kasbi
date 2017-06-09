Imports System.Web.Security

Namespace Kasbi.Controls

    Partial Class Header
        Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents btnDeliveries As System.Web.UI.WebControls.HyperLink
        Protected WithEvents btnPricelists As System.Web.UI.WebControls.HyperLink
        Protected WithEvents Hyperlink1 As System.Web.UI.WebControls.HyperLink


        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region
        Private currentPage As PageBase

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            currentPage = Page
            If Not IsPostBack Then

                If currentPage.CurrentUser Is Nothing Then
                    mnuMainMenu.Visible = False
                    lblUser.Visible = False
                    lnkExitProgram.Visible = False
                Else
                    If currentPage.CurrentUser.isLogged = False Then
                        mnuMainMenu.Visible = False
                        lblUser.Visible = False
                        lnkExitProgram.Visible = False
                    Else
                        lblUser.Text = "Пользователь: " & currentPage.CurrentUser.Name

                        'Ограничение прав
                        If Session("rule0") = "0" Then btnMain.Visible = False
                        If Session("rule4") = "0" Then btnCustomers.Visible = False
                        If Session("rule10") = "0" Then btnCTO.Visible = False
                        If Session("rule13") = "0" Then btnCatalog.Visible = False
                        If 1 = 1 Then btnMsater.Visible = False
                        If 1 = 1 Then btnRepairs.Visible = False
                        If Session("rule23") = "0" Then btnReport.Visible = False
                        If Session("rule24") = "0" Then btnAdmin.Visible = False
                        If Session("rule25") = "0" Then btnTO.Visible = False

                        'If currentPage.CurrentUser.is_admin = False Or currentPage.CurrentUser.permissions <> 4 Then
                        '    btnReport.Visible = False
                        '    btnAdmin.Visible = False
                        'End If
                        'If currentPage.CurrentUser.permissions = 8 Then
                        '    mnuMainMenu.Visible = False
                        '    mnuExternalUser.Visible = True

                        'End If
                    End If

                End If

                lblCopmpany.Text = Config.Company
            End If

        End Sub

        Private Sub lnkExitProgram_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkExitProgram.Click
            Session.Abandon()
            FormsAuthentication.SignOut()
            Response.Redirect(currentPage.GetAbsoluteUrl("default.aspx"))
        End Sub
    End Class

End Namespace
