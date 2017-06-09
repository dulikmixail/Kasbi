Imports Microsoft.VisualBasic

Namespace Kasbi

    Partial Class Errors
        Inherits System.Web.UI.Page

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

        Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            ShowContent()
        End Sub

        Private Sub ShowContent()
            Dim back$ = ""
            Dim ErrorHeader$ = "ERROR"
            Dim ErrorMessage$ = ""

            If Not HttpContext.Current.Session("ErrorMessage") Is Nothing Then
                ErrorMessage = HttpContext.Current.Session("ErrorMessage").ToString()
            End If
            lblErrorHeader.Text = ErrorHeader
            lblErrorMessage.Text = ErrorMessage
            If (back = "") Then
                lnkBack.Visible = False
            Else
                lnkBack.NavigateUrl = HttpContext.Current.Session("BackTo").ToString()
            End If
        End Sub

    End Class

End Namespace

