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

    Dim arraypax(5) As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            Call riempidocumenti(Request.Params("codiceperiodo"))
            Dim partenza As Date = ricavapart(Request.Params("codiceperiodo"))
            Dim totale As Integer = 0
            totale = Request.Params("totale")
            Dim pacchetti As Integer = 0
            If IsNumeric(Request.Params("pacchetti")) Then
                pacchetti = Request.Params("pacchetti")
            Else
                pacchetti = 0
            End If
            Dim offerta As Boolean = False
            If Right(Request.Params("categoria"), 1) = "V" Then
                offerta = True
            End If
            If Request.Params("codiceperiodo") = "PO20130923GOAGOA" Then
                Liquote2.Visible = True
                Libevande2.Visible = True
                quote.Visible = False
                bevande.Visible = False
                Lidj.Visible = True
            End If
            Call riempipaga(partenza, RepeaterPaga, (CInt(Request.Params("persone"))), totale, pacchetti, offerta)
            Dim altezza As Integer = 900
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'framedettaglio');", True)
        End If
    End Sub



    Function ricavapart(ByVal codiceperiodo As String) As Date
        Dim sqlconn As String
        ricavapart = Date.Now
        sqlconn = "SELECT * FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavapart = dr("dal")
        End If
        dr.Close()
        cn.Close()
    End Function

    Function ricavatipodata(ByVal eta As String, ByVal partenza As Date) As String
        ricavatipodata = "1960-01-01T00:00:00.0000000+01:00"
        Dim dateapp As Date = DateAdd(DateInterval.Year, -CInt(eta), partenza)
        ricavatipodata = dateapp.Year & "-" & dateapp.Month & "-" & dateapp.Day & "T00:00:00.0000000+01:00"""
    End Function

    Private Sub prenota(ByVal codiceperiodo As String, ByVal categoria As String, ByVal persone As String, ByVal arraypax() As String, ByVal cabina As String)
        
                      
    End Sub

    Private Sub addrowprezzo(ByVal ds As DataSet, ByVal descrizione As String, ByVal persone As String, ByVal prezzo As Integer, ByVal totale As Integer)
        Dim dsnewrow As DataRow
        dsnewrow = ds.Tables("preventivo").NewRow
        dsnewrow("descrizione") = descrizione
        dsnewrow("persone") = persone
        dsnewrow("prezzo") = prezzo
        dsnewrow("totale") = totale
        ds.Tables("preventivo").Rows.Add(dsnewrow)

    End Sub

    Private Sub riempipaga(ByVal dal As Date, ByVal repea As Repeater, ByVal pax As Integer, ByVal importo As Integer, ByVal pacchetti As Integer, ByVal offerta As Boolean)
        Dim ds As New dspreno
        Dim dsnewrow As DataRow
        Dim saldomeno As Integer = 0
        Dim datasaldo As Date = DateAdd(DateInterval.Day, -35, dal)
        ' Dim rigapp As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigapp"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If offerta = False Then
            If DateDiff(DateInterval.Day, Date.Now, dal) > 75 Then
                dsnewrow = ds.Tables("pagamenti").NewRow
                dsnewrow("descripaga") = "1° Acconto:"
                dsnewrow("scadenza") = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                dsnewrow("prezzo") = pax * 50
                ds.Tables("pagamenti").Rows.Add(dsnewrow)
                dsnewrow = ds.Tables("pagamenti").NewRow
                dsnewrow("descripaga") = "2° Acconto:"
                dsnewrow("scadenza") = DateAdd(DateInterval.Day, -70, dal)
                dsnewrow("prezzo") = CInt(importo / 100 * 40)
                ds.Tables("pagamenti").Rows.Add(dsnewrow)
                saldomeno = (CInt(importo / 100 * 40) + pax * 50)
                '  rigapp.Visible = True
                'ImagePaga.Visible = True
            ElseIf DateDiff(DateInterval.Day, Date.Now, dal) <= 75 And DateDiff(DateInterval.Day, Date.Now, dal) >= 35 Then
                dsnewrow = ds.Tables("pagamenti").NewRow
                dsnewrow("descripaga") = "Acconto:"
                dsnewrow("scadenza") = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                dsnewrow("prezzo") = CInt(importo / 100 * 40)
                ds.Tables("pagamenti").Rows.Add(dsnewrow)
                saldomeno = (CInt(importo / 100 * 40))
            Else
                datasaldo = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                saldomeno = 0
            End If
        Else
            If DateDiff(DateInterval.Day, Date.Now, dal) >= 50 Then
                dsnewrow = ds.Tables("pagamenti").NewRow
                dsnewrow("descripaga") = "Acconto:"
                dsnewrow("scadenza") = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                dsnewrow("prezzo") = CInt(importo / 100 * 40)
                ds.Tables("pagamenti").Rows.Add(dsnewrow)
                saldomeno = (CInt(importo / 100 * 40))
                datasaldo = DateAdd(DateInterval.Day, -48, dal)
            Else
                datasaldo = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                saldomeno = 0
                'ImagePaga.Visible = False
                'rigapp.Visible = False
            End If
        End If
        dsnewrow = ds.Tables("pagamenti").NewRow
        dsnewrow("descripaga") = "Saldo:"
        dsnewrow("scadenza") = datasaldo
        dsnewrow("prezzo") = importo - saldomeno
        ds.Tables("pagamenti").Rows.Add(dsnewrow)
        If pacchetti > 0 Then
            dsnewrow = ds.Tables("pagamenti").NewRow
            dsnewrow("descripaga") = "Pacchetti:"
            dsnewrow("scadenza") = datasaldo
            dsnewrow("prezzo") = pacchetti
            ds.Tables("pagamenti").Rows.Add(dsnewrow)
        End If
        repea.DataSource = ds.Tables("pagamenti")
        repea.DataBind()
    End Sub


    Function ricavavolo(ByVal volo As String) As String
        ricavavolo = ""
        Select Case volo
            Case Is = "AHO"
                ricavavolo = "ALGHERO"
            Case Is = "AOI"
                ricavavolo = "ANCONA"
            Case Is = "BRI"
                ricavavolo = "BARI"
            Case Is = "BLQ"
                ricavavolo = "BOLOGNA"
            Case Is = "BDS"
                ricavavolo = "BRINDISI"
            Case Is = "CAG"
                ricavavolo = "CAGLIARI"
            Case Is = "CTA"
                ricavavolo = "CATANIA"
            Case Is = "FLR"
                ricavavolo = "FIRENZE"
            Case Is = "GOA"
                ricavavolo = "GENOVA"
            Case Is = "SUF"
                ricavavolo = "LAMEZIA TERME"
            Case Is = "MXP"
                ricavavolo = "MILANO MALPENSA"
            Case Is = "NAP"
                ricavavolo = "NAPOLI"
            Case Is = "OLB"
                ricavavolo = "OLBIA"
            Case Is = "PMO"
                ricavavolo = "PALERMO"
            Case Is = "PEG"
                ricavavolo = "PERUGIA"
            Case Is = "PSA"
                ricavavolo = "PISA"
            Case Is = "REG"
                ricavavolo = "REGGIO CALABRIA"
            Case Is = "FCO"
                ricavavolo = "ROMA FIUMICINO"
            Case Is = "TRN"
                ricavavolo = "TORINO CASELLE"
            Case Is = "TRS"
                ricavavolo = "TRIESTE"
            Case Is = "VCE"
                ricavavolo = "VENEZIA"
            Case Is = "VRN"
                ricavavolo = "VERONA"
        End Select
    End Function


    Private Sub riempidocumenti(ByVal codiceperiodo As String)
        Dim passaporto As Integer = 0
        'Dim sqlconn As String
        'sqlconn = "SELECT periodo.passaporto FROM periodo WHERE codiceperiodo = '" & codiceperiodo & "'"
        'Dim cmd2 As New MySqlCommand(sqlconn, cn)
        'If cn.State = ConnectionState.Closed Then cn.Open()
        'Dim dr As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        'If dr.HasRows Then
        '    dr.Read()
        '    passaporto = dr("passaporto")
        'End If
        'dr.Close()
        'cn.Close()
        If IsNumeric(Request.Params("passaporto")) Then passaporto = Request.Params("passaporto")
        If passaporto = 0 Then
            lbldocumenti.Text = "Passaporto o carta d'identità in corso di validità (minori inclusi)<br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto. <br /><br /><b>Per i minori di anni 14</b>, l’uso della carta di identità ai fini dell’espatrio è subordinato alla condizione che il minore viaggi in compagnia di uno dei genitori o di chi ne fa le veci, o che venga menzionato – su una dichiarazione rilasciata da chi può dare l’assenso o l’autorizzazione, convalidata dalla Questura o dalle Autorità consolari – il nome della persona, dell’ente o della compagnia di trasporto a cui il minore medesimo è affidato.<br /> Per i minori di anni 14 è  richiesto da parte delle Autorità di Frontiera un certificato di nascita con indicazione della paternità e della maternità da esibire alla partenza."
        Else
            lbldocumenti.Text = "Passaporto. Per questo itinerario è richiesto obbligatoriamente il passaporto per tutti i partecipanti (minori inclusi). <br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto."
        End If
    End Sub

End Class
