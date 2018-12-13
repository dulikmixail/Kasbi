Namespace Kasbi.Admin
    Partial Class Restrictions
        Inherits PageBase

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnBack.Click
            Response.Redirect(GetAbsoluteUrl("~/Admin/Default.aspx"))
        End Sub
    End Class
End Namespace

