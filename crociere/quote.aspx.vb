
Partial Class crociere_iti
    Inherits System.Web.UI.Page





    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim altezza As Integer = 450
        If Request.Params("codiceperiodo") = "PO20130923GOAGOA" Then
            Liquote2.Visible = True
            Libevande2.Visible = True
            quote.Visible = False
            bevande.Visible = False
            Lidj.Visible = True
        End If
        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
        
    End Sub
End Class
