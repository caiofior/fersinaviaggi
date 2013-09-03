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
    Dim contarighe As Integer = 0
    Dim id_itinerario As Integer
    Dim dscat As New DsCosta
    Dim rigona As String = ""
    Dim fremone As String = ""
    Dim volo As Integer = 0
    Dim voloobbligatorio As Integer = 0




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            Dim stringapreis As String = "preventivoparti.aspx"
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "javascript:assacabina('cabinecosta', '" & stringapreis & "', '" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '', '', '', '" & volo & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "','','" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "')", True)
        End If
    End Sub

   


End Class
