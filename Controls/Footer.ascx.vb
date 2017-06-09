Imports System.Web.Security


Namespace Kasbi.Controls


    Partial Class Footer
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
                    lnkTop.Visible = False
                Else
                    If currentPage.CurrentUser.isLogged = False Then
                        lnkTop.Visible = False
                    End If
                End If
                lnkTop.NavigateUrl = currentPage.Page.AppRelativeVirtualPath() & "#top"
            End If

        End Sub

    End Class

End Namespace
