
Partial Class crociere_conferma_msc
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lnk As String
        lnk = "conferma-crociere.aspx?codice=" & Request.Params("codice") & "&email=" & Request.Params("email")
        Response.Redirect(lnk)


    End Sub
End Class
