Imports System.Globalization

Partial Class culture_info
    Inherits System.Web.UI.Page
    Public Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim culture As CultureInfo = CultureInfo.CurrentCulture
        tbCultureInfo.Text = culture.NativeName + "/" + culture.Name
    End Sub

End Class
