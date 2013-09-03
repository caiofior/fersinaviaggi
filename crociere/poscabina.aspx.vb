
Partial Class crociere_poscabina
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim str As String = "http://www.costaclick.net/CostaClick/it-IT/Pianonave.htm?CabinId=" & Request.Params("cabina") & "&ShipId=" & Request.Params("codicenave")
        Dim idf As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("idf"), System.Web.UI.HtmlControls.HtmlGenericControl)
        idf.Attributes.Add("src", Str)

        'Dim str2 As String = "http://www.costaclick.net/CostaClick/it-IT/CategoriaCabina.htm?CatId=" & Request.Params("categoria") & "&ShipId=" & Request.Params("codicenave")
        'Dim idf2 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("idf2"), System.Web.UI.HtmlControls.HtmlGenericControl)
        'idf2.Attributes.Add("src", str2)
        Dim altezza As Integer = 300
        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
    End Sub
End Class
