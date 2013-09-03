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
    Dim cn2 As New MySqlConnection(cnstring)
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim contarighe As Integer = 0
    Dim rigona1 As String = ""
    Dim divone1 As String = ""
    Dim contatore1 As Integer = 1
    Dim rigona2 As String = ""
    Dim divone2 As String = ""
    Dim contatore2 As Integer = 1
    Dim rigona3 As String = ""
    Dim divone3 As String = ""
    Dim contatore3 As Integer = 1
    Dim arraypax(5) As String
    Dim altezza As Integer = 0
    Dim frasepac As String = ";"
    Dim prezzopac As Integer = 0
    Dim frasepac2 As String = ";"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            arraypax(1) = Request.Params("eta1")
            arraypax(2) = Request.Params("eta2")
            arraypax(3) = Request.Params("eta3")
            arraypax(4) = Request.Params("eta4")
            arraypax(5) = Request.Params("eta5")
            Call ricavaperiodo(Request.Params("codiceperiodo"))
            Call caricapac2(Request.Params("frasepac"))
            'Dim partenza As Date = ricavapart(Request.Params("codiceperiodo"))
            'Dim totale As Integer = 0
            'totale = Request.Params("totale")
            'Call riempipaga(partenza, RepeaterPaga, (CInt(Request.Params("persone"))), totale, False)
            'Request.Params("frase")
            altezza = 50 + ((contatore1 * 30) + (contatore2 * 30) + (contatore3 * 30))
            'Dim qrystring As String = Request.ServerVariables("QUERY_STRING")
            Dim stringapreis As String = "preventivo.aspx"
            Dim Framealtezza As String = "framedettaglio"
            If Request.Params("conf") = 1 Then
                Framealtezza = "frameccc"
                bttadegua.Visible = False
                bttadegua2.Visible = True
                If pagatopratica(Request.Params("parsel")) = True Then
                    Labelblocco.Text = "Per questa pratica non puoi aggiungere pacchetti in quanto abbiamo avviato la procedura di bigliettazione. Contatta il call center"
                    bttadegua2.Visible = False
                    Labelblocco.Visible = True
                End If
            Else
                bttadegua.Attributes.Add("onclick", "javascript:caricaprezzi('cabinecosta', '" & stringapreis & "', '" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & Request.Params("riga") & "', ' " & Request.Params("cabina") & "', '" & Request.Params("nomeponte") & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "', 'pacc', '" & Request.Params("tipologiacabina") & "', '" & Request.Params("assi") & "', '" + Request.Params("passaporto") + "')")
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "iniaz(); altezza('" & altezza & "', '" & Framealtezza & "'); frasepac = '" & frasepac & "'; altezzadiv= " & altezza & ";" & rigona1 & rigona2 & rigona3 & divone1 & divone2 & divone3, True)
        End If
    End Sub

    Function pagatopratica(ByVal id_preno As Integer) As Boolean
        pagatopratica = False
        Dim sqlconn As String
        sqlconn = "SELECT pagatomsc FROM preno where id_preno = " & id_preno
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("pagatomsc") = 1 Then pagatopratica = True
        End If
        dr.Close()
        cn.Close()
    End Function

    Private Sub caricapac2(ByVal frasepac2 As String)
        Dim stringa() As String
        stringa = Split(frasepac2, ";")
        Dim word As String
        Dim indice As String
        Dim pax As String
        For Each word In stringa
            If word.IndexOf("-") >= 0 Then
                indice = Left(word, word.IndexOf("-"))
                pax = Right(word, word.Length - word.IndexOf("-") - 1)
                frasepac = frasepac & indice & "-" & pax & ";"
                aggiornarepeater(RepeaterBenessere, CInt(indice), CInt(pax))
                aggiornarepeater(RepeaterBevande, CInt(indice), CInt(pax))
                aggiornarepeater(RepeaterAltri, CInt(indice), CInt(pax))
            End If
        Next
    End Sub


    Private Sub aggiornarepeater(ByVal rpt As Repeater, ByVal indice As Integer, ByVal pax As Integer)
        Dim oItem As RepeaterItem
        For Each oItem In rpt.Items
            Dim DropSel As DropDownList = CType(oItem.FindControl("DropSel"), DropDownList)
            Dim idpac As Label = CType(oItem.FindControl("id_pacchetto"), Label)
            If idpac.Text = indice Then
                DropSel.SelectedValue = pax
            End If
        Next oItem
    End Sub

    Private Sub ricavaperiodo(ByVal codiceperiodo As String)
        Dim sqlconn As String
        Dim iditi As Integer = 0
        Dim idnave As Integer = 0
        sqlconn = "SELECT * FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            iditi = dr("id_itinerario")
            idnave = dr("id_nave")
        End If
        dr.Close()
        cn.Close()
        caricapac(idnave, iditi)
    End Sub

    Private Sub caricapac(ByVal idnave As Integer, ByVal iditi As Integer)
        If idnave > 0 And iditi > 0 Then
            Dim sqlconn As String
            sqlconn = "SELECT pacchetti.* from pacchetti, pacchettiassegnati WHERE pacchetti.id_pacchetto = pacchettiassegnati.id_pacchetto and (id_nave = " & idnave & " or id_itinerario = " & iditi & ") and tipopacchetto = 1 group by nomepacchetto, listino, vendita, descrizione, tipopacchetto order by pacchetti.nomepacchetto"
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                RepeaterBenessere.DataSource = dr
                RepeaterBenessere.DataBind()
            Else
                Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigabenessere"), System.Web.UI.HtmlControls.HtmlGenericControl)
                riga.Visible = False
            End If
            dr.Close()
            sqlconn = "SELECT pacchetti.* from pacchetti, pacchettiassegnati WHERE pacchetti.id_pacchetto = pacchettiassegnati.id_pacchetto and (id_nave = " & idnave & " or id_itinerario = " & iditi & ") and tipopacchetto = 2 group by nomepacchetto, listino, vendita, descrizione, tipopacchetto order by pacchetti.nomepacchetto"
            Dim cmd2 As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
            If dr2.HasRows Then
                RepeaterBevande.DataSource = dr2
                RepeaterBevande.DataBind()
            End If
            dr2.Close()
            cn.Close()
            sqlconn = "SELECT pacchetti.* from pacchetti, pacchettiassegnati WHERE pacchetti.id_pacchetto = pacchettiassegnati.id_pacchetto and (id_nave = " & idnave & " or id_itinerario = " & iditi & ") and tipopacchetto = 3 group by nomepacchetto, listino, vendita, descrizione, tipopacchetto order by pacchetti.nomepacchetto"
            Dim cmd3 As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr3 As MySqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)
            If dr3.HasRows Then
                RepeaterAltri.DataSource = dr3
                RepeaterAltri.DataBind()
            Else
                Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigaaltri"), System.Web.UI.HtmlControls.HtmlGenericControl)
                riga.Visible = False
            End If
            dr3.Close()
            cn.Close()

        End If

    End Sub

    Private Sub riempirepeater(ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs, ByVal numero As Integer)
        Dim DropSel As DropDownList = CType(e.Item.FindControl("DropSel"), DropDownList)
        Dim listino As Label = CType(e.Item.FindControl("listino"), Label)
        Dim vendita As Label = CType(e.Item.FindControl("vendita"), Label)
        Dim vendita2 As Label = CType(e.Item.FindControl("vendita2"), Label)
        Dim descrizione As Label = CType(e.Item.FindControl("descrizione"), Label)
        Dim id_pacchetto As Label = CType(e.Item.FindControl("id_pacchetto"), Label)
        Dim ImageDetail As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("ImageDetail"), System.Web.UI.WebControls.Image)
        Dim divdescri As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("divdescri"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If listino.Text = vendita.Text Then
                listino.Visible = False
            End If
            vendita.Text = Format(CInt(vendita.Text), "€ 0.00")
            listino.Text = Format(CInt(listino.Text), "€ 0.00")

            Dim cl As String = "#ffffff"
            Dim misura As Integer = ricavaaltezza(descrizione.Text)
            Dim Framealtezza As String = "framedettaglio"
            If Request.Params("conf") = 1 Then
                Framealtezza = "frameccc"
            End If
            Dim stringa As String = "javascript:vedisotto('" & divdescri.ClientID & "','" & riga.ClientID & "', '" & misura & "px', '" & cl & "', '" & Framealtezza & "');"
            ImageDetail.Attributes.Add("onclick", stringa)
            If numero = 1 Then
                rigona1 = rigona1 & "rigona" & numero & "[" & contatore1 & "]='" & riga.ClientID & "';"
                divone1 = divone1 & "divone" & numero & "[" & contatore1 & "]='" & divdescri.ClientID & "';"
                contatore1 = contatore1 + 1
                caricadrop(DropSel, Request.Params("persone"))
            ElseIf numero = 2 Then
                rigona2 = rigona2 & "rigona" & numero & "[" & contatore2 & "]='" & riga.ClientID & "';"
                divone2 = divone2 & "divone" & numero & "[" & contatore2 & "]='" & divdescri.ClientID & "';"
                contatore2 = contatore2 + 1
                Dim listinoadulti As Label = CType(e.Item.FindControl("listinoadulti"), Label)
                Dim venditaadulti As Label = CType(e.Item.FindControl("venditaadulti"), Label)
                Dim listinobambini As Label = CType(e.Item.FindControl("listinobambini"), Label)
                Dim venditabambini As Label = CType(e.Item.FindControl("venditabambini"), Label)
                Dim algiorno As Label = CType(e.Item.FindControl("algiorno"), Label)
                Dim adulti As Integer = 0
                Dim bimbi As Integer = 0
                If Request.Params("adulti") > 0 Then
                    adulti = Request.Params("adulti")
                    If Request.Params("bambini") > 0 Then
                        bimbi = Request.Params("bambini")
                    End If
                Else
                    Dim i As Integer
                    For i = 1 To Request.Params("persone")
                        If arraypax(i) < 18 Then
                            bimbi = bimbi + 1
                        Else
                            adulti = adulti + 1
                        End If
                    Next
                End If
                If algiorno.Text = 1 Then
                    Dim prezzoadulti As Integer = 0
                    Dim prezzobambini As Integer = 0
                    Dim ladulti As Integer = 0
                    Dim lbambini As Integer = 0
                    Dim durata As Integer = ricavadurata(Request.Params("codiceperiodo"))
                    prezzoadulti = venditaadulti.Text * adulti * durata
                    prezzobambini = venditabambini.Text * bimbi * durata
                    ladulti = listinoadulti.Text * adulti * durata
                    lbambini = listinobambini.Text * bimbi * durata
                    vendita.Text = Format(CInt(prezzoadulti + prezzobambini), "€ 0.00")
                    listino.Text = Format(CInt(ladulti + lbambini), "€ 0.00")
                    vendita2.Text = CInt(prezzoadulti + prezzobambini)
                    caricadrop(DropSel, 1)
                Else
                    caricadrop(DropSel, Request.Params("persone"))
                End If
            ElseIf numero = 3 Then
                rigona3 = rigona3 & "rigona" & numero & "[" & contatore3 & "]='" & riga.ClientID & "';"
                divone3 = divone3 & "divone" & numero & "[" & contatore3 & "]='" & divdescri.ClientID & "';"
                contatore3 = contatore3 + 1
                caricadrop(DropSel, Request.Params("persone"))
            End If
            descrizione.Text = Replace(descrizione.Text, "<li>", "<li style='width:510px;  padding:4px 0 4px 0; height: auto;'>")
            DropSel.Attributes.Add("onchange", "javascript:segna('" & id_pacchetto.Text & "', this.value, '" & DropSel.ClientID & "')")
        End If
    End Sub

    Function ricavadurata(ByVal codiceperiodo As String) As Integer
        ricavadurata = 0
        Dim sqlconn As String
        sqlconn = "SELECT durata FROM periodo WHERE codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd3 As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)
        Dim saldato As Boolean = False
        If dr.HasRows Then
            dr.Read()
            ricavadurata = dr("durata")
        End If
        dr.Close()
        cn2.Close()
    End Function

    Protected Sub RepeaterBevande_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterBevande.ItemDataBound
        Call riempirepeater(e, 2)
    End Sub

    Protected Sub RepeaterAltri_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterAltri.ItemDataBound
        Call riempirepeater(e, 3)
    End Sub

    Protected Sub RepeaterBenessere_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterBenessere.ItemDataBound
        Call riempirepeater(e, 1)
    End Sub


    Private Sub caricadrop(ByVal drop As DropDownList, ByVal pax As Integer)
        Dim i As Integer
        For i = 0 To pax
            drop.Items.Add(New ListItem(i, i))
        Next
        drop.DataBind()
    End Sub

    Function ricavaaltezza(ByVal descri As String) As Integer
        Dim conta As Integer = 1
        Dim conta2 As Integer = 0
        Dim app As String = descri
        Dim app2 As String
        Do While app.IndexOf("<li>") > 0
            app2 = Right(app, (app.Length - (app.IndexOf("<li>") + 4)))
            Dim lung As Integer = (Left(app2, app2.IndexOf("</li>"))).Length
            If (lung > 80 And lung <= 130) Then
                conta2 = conta2 + 15
            ElseIf (lung > 130 And lung <= 195) Then
                conta2 = conta2 + 30
            ElseIf (lung > 195 And lung <= 260) Then
                conta2 = conta2 + 45
            ElseIf (lung > 260 And lung <= 325) Then
                conta2 = conta2 + 60
            ElseIf (lung > 325 And lung <= 390) Then
                conta2 = conta2 + 75
            End If
            app = app2
            conta = conta + 1
        Loop
        ricavaaltezza = 20 + (conta * 30) + conta2
    End Function


    Protected Sub bttadegua2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bttadegua2.Click
        Dim sqlconn As String
        sqlconn = "SELECT ricevuto FROM pagamenti WHERE pacchetto = 1 AND id_preno = " & Request.Params("parsel")
        Dim cmd3 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)
        Dim saldato As Boolean = False
        If dr.HasRows Then
            Do While dr.Read
                If dr("ricevuto") = 1 Then
                    saldato = True
                End If
            Loop
        End If
        dr.Close()
        cn.Close()
        If saldato = False Then
            sqlconn = "DELETE FROM preventivo where id_preno = " & Request.Params("parsel") & " AND pacc = 1"
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd.ExecuteNonQuery()
            salvarpt(RepeaterBenessere, Request.Params("parsel"), 0)
            salvarpt(RepeaterBevande, Request.Params("parsel"), 1)
            salvarpt(RepeaterAltri, Request.Params("parsel"), 0)
            If Trim(Request.Params("frasepac")) <> Trim(frasepac2) Then
                aggiornaregistro(frasepac2, Request.Params("frasepac"), Request.Params("parsel"))
            End If
            sqlconn = "UPDATE preno SET prezzopac = @prezzopac, frasepac = @frasepac WHERE id_preno = " & Request.Params("parsel")
            Dim cmd2 As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd2.Parameters.AddWithValue("@prezzopac", prezzopac)
            cmd2.Parameters.AddWithValue("@frasepac", frasepac2)
            cmd2.ExecuteNonQuery()
            sqlconn = "DELETE FROM pagamenti where id_preno = " & Request.Params("parsel") & " AND pacchetto = 1"
            Dim cmd4 As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd4.ExecuteNonQuery()
            sqlconn = "SELECT scadenza FROM pagamenti WHERE id_preno = " & Request.Params("parsel") & " ORDER BY scadenza DESC"
            Dim cmd6 As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr6 As MySqlDataReader = cmd6.ExecuteReader(CommandBehavior.CloseConnection)
            Dim scadenza As Date = Date.Now
            If dr6.HasRows Then
                dr6.Read()
                scadenza = dr6("scadenza")
            End If
            dr6.Close()
            If prezzopac > 0 Then
                sqlconn = "INSERT INTO pagamenti SET id_preno = @id_preno, descripaga = @descripaga, scadenza = @scadenza, prezzo = @prezzo, pacchetto = @pacchetto"
                Dim cmd5 As New MySqlCommand(sqlconn, cn)
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd5.Parameters.AddWithValue("@id_preno", Request.Params("parsel"))
                cmd5.Parameters.AddWithValue("@descripaga", "Pacchetti:")
                cmd5.Parameters.AddWithValue("@scadenza", scadenza)
                cmd5.Parameters.AddWithValue("@prezzo", prezzopac)
                cmd5.Parameters.AddWithValue("@pacchetto", 1)
                cmd5.ExecuteNonQuery()
                cn.Close()
            End If
            Dim lnk As String = "conferma-crociere.aspx?maila=0&codice=" & Request.Params("codice") & "&email=" & Request.Params("email")
            ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Hai già pagato dei pacchetti. Per effettuare modifiche contattaci al numero 0461 914471.');", True)
        End If

    End Sub


    Private Sub salvarpt(ByVal rpt As Repeater, ByVal id_preno As Integer, ByVal tipo As Integer)
        Dim oItem As RepeaterItem
        For Each oItem In rpt.Items
            Dim DropSel As DropDownList = CType(oItem.FindControl("DropSel"), DropDownList)
            Dim nomepac As Label = CType(oItem.FindControl("nomepacchetto"), Label)
            Dim vendita As Label = CType(oItem.FindControl("vendita2"), Label)
            Dim id_pacchetto As Label = CType(oItem.FindControl("id_pacchetto"), Label)
            If DropSel.SelectedValue > 0 Then
                Dim sqlconn As String
                sqlconn = "INSERT INTO preventivo SET descrizione = @descrizione, id_preno = @id_preno, pax = @pax, importo = @importo, totale = @totale, pacc = @pacc"
                Dim cmd As New MySqlCommand(sqlconn, cn)
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd.Parameters.AddWithValue("@id_preno", id_preno)
                cmd.Parameters.AddWithValue("@descrizione", nomepac.Text.ToLower)
                cmd.Parameters.AddWithValue("@pax", DropSel.SelectedValue)
                cmd.Parameters.AddWithValue("@importo", CInt(vendita.Text))
                cmd.Parameters.AddWithValue("@totale", DropSel.SelectedValue * CInt(vendita.Text))
                cmd.Parameters.AddWithValue("@pacc", 1)
                cmd.ExecuteNonQuery()                
                prezzopac = prezzopac + DropSel.SelectedValue * vendita.Text
                frasepac2 = frasepac2 & id_pacchetto.Text & "-" & DropSel.SelectedValue & ";"                
                cn.Close()
            End If
        Next oItem
    End Sub

    Private Sub aggiornaregistro(ByVal frasenuova As String, ByVal frasevecchia As String, ByVal id_preno As Integer)
        Dim stringa2() As String
        stringa2 = Split(frasevecchia, ";")
        Dim word2 As String
        Dim indice2 As String
        Dim pax2 As String
        For Each word2 In stringa2
            If word2.IndexOf("-") >= 0 Then
                indice2 = Left(word2, word2.IndexOf("-"))
                pax2 = Right(word2, word2.Length - word2.IndexOf("-") - 1)                                                
                adeguapac(2, id_preno, indice2, 0)
            End If
        Next
        Dim stringa() As String
        stringa = Split(frasenuova, ";")
        Dim word As String
        Dim indice As String
        Dim pax As String
        For Each word In stringa
            If word.IndexOf("-") >= 0 Then
                indice = Left(word, word.IndexOf("-"))
                pax = Right(word, word.Length - word.IndexOf("-") - 1)
                frasepac = frasepac & indice & "-" & pax & ";"                
                Dim existpac As Integer = esistepac(indice, id_preno, pax)
                If existpac <> 3 Then 'nessuna modifica
                    adeguapac(existpac, id_preno, indice, pax)
                End If
            End If
        Next
    End Sub

    Private Sub adeguapac(ByVal existpac As Integer, ByVal id_preno As Integer, ByVal indice As Integer, ByVal pax As Integer)
        Dim stringaconn As String = ""
        If existpac = 1 Then 'aggiunta
            stringaconn = "INSERT INTO registropac SET id_preno = @id_preno, id_pacchetto = @id_pacchetto, persone = @persone, data = @data, confermato = @confermato, addmodcanc = @addmodcanc"
        Else
            stringaconn = "UPDATE registropac SET persone = @persone, data = @data, confermato = @confermato, addmodcanc = @addmodcanc WHERE id_preno = " & id_preno & " AND id_pacchetto = " & indice
        End If
        Dim cmd2 As New MySqlCommand(stringaconn, cn)
        If existpac = 1 Then
            cmd2.Parameters.AddWithValue("@id_preno", id_preno)
            cmd2.Parameters.AddWithValue("@id_pacchetto", indice)
        End If
        cmd2.Parameters.AddWithValue("@persone", pax)
        cmd2.Parameters.AddWithValue("@data", Date.Now)
        cmd2.Parameters.AddWithValue("@confermato", 1)
        If existpac = 4 Then 'modificato
            cmd2.Parameters.AddWithValue("@addmodcanc", 0)
        Else
            cmd2.Parameters.AddWithValue("@addmodcanc", existpac)
        End If
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd2.ExecuteNonQuery()
        cn.Close()
    End Sub

    Function esistepac(ByVal id_pacchetto As Integer, ByVal id_preno As Integer, ByVal persone As Integer) As Integer
        esistepac = False
        Dim sqlconn As String
        sqlconn = "SELECT * FROM registropac where id_preno = " & id_preno & " AND id_pacchetto = " & id_pacchetto
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If persone = 0 Then
                esistepac = 2 ' cancellato
            Else
                If dr("persone") = persone Then
                    esistepac = 3 'nessuna modifica
                Else
                    esistepac = 4 'modificato
                End If
            End If
        Else
            esistepac = 1 ' non esiste e viene aggiunto
        End If
            dr.Close()
            cn.Close()
    End Function

End Class
