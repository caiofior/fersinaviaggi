Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Partial Class crociere_categorie
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim volo As Integer = 0
    Dim voloobbligatorio As Integer = 0



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            volo = Request.Params("volo")
            voloobbligatorio = Request.Params("voloobb")
            Dim imbarco As String = ""
            Dim arraypax(5) As String
            arraypax(1) = Request.Params("eta1")
            arraypax(2) = Request.Params("eta2")
            arraypax(3) = Request.Params("eta3")
            arraypax(4) = Request.Params("eta4")
            arraypax(5) = Request.Params("eta5")
            Dim altezza As Integer = 0
            If volo > 0 Then
                panelvolo.Visible = True
                If volo = 1 Then
                    lblvolo.Text = "Volo di andata e ritorno"
                ElseIf volo = 2 Then
                    lblvolo.Text = "Volo di ritrono"
                ElseIf volo = 3 Then
                    lblvolo.Text = "Volo di andata"
                End If
                Labelvolodesc.Text = "Selezionare l'aeroporto desiderato oppure selezionare solo crociera volo escluso"
                If voloobbligatorio = 1 Then
                    lblvolo.Text = lblvolo.Text & " obbligatorio"
                    DropVolo.Items.RemoveAt(0)
                    Labelvolodesc.Text = "Per questa crociera è obbligatorio selezionare il volo"
                End If
                lblvolo.Text = lblvolo.Text.ToUpper
                altezza = 250
            End If
            If DropVolo.SelectedValue = "NNN" Then volo = 0
            'DropVolo.Attributes.Add("onchange", "javascript:cambiavolo(this, '')")
            volocontinua.Attributes.Add("onclick", "javascript:caricacab('" & Request.Params("dal") & "', '" & Request.Params("nomecabina") & "', '" & Request.Params("tipocabina") & "', 'cabinecosta', 'cabinecosta.aspx', '" & Request.Params("codiceperiodo") & "' , '" & Request.Params("categoria") & "', '', '" & volo & "', '" & Request.Params("voloobb") & "',  '" & DropVolo.SelectedValue & "', '" & Request.Params("compagnia") & "', '" & Request.Params("tipologiacabina") & "' )")
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'framecabinecosta'); altezzaframe='" & altezza & "';", True)
        End If
    End Sub

   
End Class
