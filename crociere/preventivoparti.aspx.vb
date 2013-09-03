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
    Dim rigona As String = ""
    Dim arraypax(5) As String
    Dim altezza As Integer = 0
    Dim totalepac As Integer = 0
    Dim procedi As Boolean = True
    Dim eccopreventivo As String = ""
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        arraypax(1) = Request.Params("eta1")
        arraypax(2) = Request.Params("eta2")
        arraypax(3) = Request.Params("eta3")
        arraypax(4) = Request.Params("eta4")
        arraypax(5) = Request.Params("eta5")
        If Not Page.IsPostBack Then
            labeltitolo.Text = ricavatitolo(Request.Params("codiceperiodo"))
            If ricavacompagnia(Request.Params("codiceperiodo")) = 1 Then
                Call prenotacosta(Request.Params("codiceperiodo"), Request.Params("categoria"), Request.Params("persone"), arraypax, Request.Params("cabina"))
            Else
                Call prenotamsc(Request.Params("codiceperiodo"), Request.Params("categoria"), Request.Params("persone"), arraypax, Request.Params("cabina"))
            End If
            altezza = 375 + (contarighe * 29)
            vdett.Attributes.Add("onclick", "javascript:vedidettaglio('dettaglio','dettaglioparti.aspx','" & Request.Params("codiceperiodo") & "','" & prezzo.Text & "', '" & Request.Params("persone") & "', '" & Request.Params("compagnia") & "', '" & totalepac & "', '" & Request.Params("categoria") & "'); premuto('vdett');")
            vpacc.Attributes.Add("onclick", "javascript:vedipacchetti('dettaglio','pacchetti.aspx','" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & Request.Params("riga") & "', '" & Request.Params("cabina") & "', '" & Request.Params("nomeponte") & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "', '" & Request.Params("frasepac") & "','" & Request.Params("tipologiacabina") & "'); premuto('vpacc');")
            prosegui.Attributes.Add("onclick", "javascript:vedidati('dati','dati.aspx','" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & Request.Params("riga") & "', '" & Request.Params("cabina") & "', '" & Request.Params("nomeponte") & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "', '" & Request.Params("frasepac") & "', '" & prezzo.Text & "', '" & Request.Params("tipologiacabina") & "', '" & Request.Params("tipocabina") & "'); altezza('" & altezza - 290 & "', 'framepreventivo')")
            If procedi = False Then
                prosegui.Attributes.Add("onclick", "")
                prosegui.Style.Add("visibility", "hidden")
                menu.Style.Add("visibility", "hidden")
            End If
            vpacc.Visible = False
            vprez.Visible = False
            voffe.Visible = False
            rigona = "rigona[0]='vdett'; rigona[1]='vpacc'; rigona[2]='voffe'; rigona[3]='vprezz';"
            If paneleerrore.Visible = False Then
                If Request.Params("primo") = "pacc" Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & rigona & "altezza('" & altezza & "','framepreventivo'); vedipacchetti('dettaglio','pacchetti.aspx','" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & Request.Params("riga") & "', '" & Request.Params("cabina") & "', '" & Request.Params("nomeponte") & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "', '" & Request.Params("frasepac") & "', '" & Request.Params("tipologiacabina") & "'); premuto('vpacc'); assegnapreventivo('" & eccopreventivo & "')", True)
                Else
                    If procedi = True Then
                        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & rigona & "altezza('" & altezza & "','framepreventivo'); vedidettaglio('dettaglio','dettaglioparti.aspx','" & Request.Params("codiceperiodo") & "','" & prezzo.Text & "', '" & Request.Params("persone") & "', '" & Request.Params("compagnia") & "', '" & totalepac & "', '" & Request.Params("categoria") & "'); assegnapreventivo('" & eccopreventivo & "')", True)
                    Else
                        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & rigona & "altezza('" & 180 & "','framepreventivo'); assegnapreventivo('" & eccopreventivo & "')", True)
                    End If
                End If
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & rigona & "altezza('" & 180 & "','framepreventivo'); assegnapreventivo('" & eccopreventivo & "')", True)
            End If
        End If
    End Sub


    Function ricavatitolo(ByVal codiceperiodo As String) As String
        Dim sqlconn As String
        ricavatitolo = ""
        sqlconn = "SELECT * FROM periodo, nave where nave.id_nave = periodo.id_nave AND periodo.codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavatitolo = "Preventivo " & dr("titolo").ToString.ToUpper & " partenza del " & Format(dr("dal"), "dd/MM/yyyy") & " <span style='font-size:small'>per " & Request.Params("persone") & " persone</span>"
        End If
        dr.Close()
        cn.Close()
    End Function

    Function ricavacompagnia(ByVal codiceperiodo As String) As Integer
        Dim sqlconn As String
        ricavacompagnia = 0
        sqlconn = "SELECT compagnia FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavacompagnia = dr("compagnia")
        End If
        dr.Close()
        cn.Close()
    End Function


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

    Private Sub prenotamsc(ByVal codiceperiodo As String, ByVal categoria As String, ByVal persone As String, ByVal arraypax() As String, ByVal cabina As String)
        Dim i As Integer
        Dim adulti As Integer = 0
        Dim bambini As Integer = 0
        Dim giovani As Integer = 0
        Dim senior As Integer = 0
        For i = 1 To persone
            If arraypax(i) < 18 Then
                bambini = bambini + 1
            ElseIf arraypax(i) >= 18 And arraypax(i) < 34 Then
                giovani = giovani + 1
            ElseIf arraypax(i) >= 34 And arraypax(i) < 65 Then
                adulti = adulti + 1
            ElseIf arraypax(i) >= 65 And arraypax(i) < 120 Then
                senior = senior + 1
            End If
        Next
        Call ricavaprezzomsc(codiceperiodo, categoria, bambini, adulti, giovani, senior, arraypax)
    End Sub

    Public Sub ricavaprezzomsc(ByVal codiceperiodo As String, ByVal categoria As String, ByVal bambini As Integer, ByVal adulti As Integer, ByVal giovani As Integer, ByVal senior As Integer, ByVal arraypax() As String)
        Dim ds2 As New dspreno
        Dim sqlconn As String
        sqlconn = "SELECT prezzi.*, periodo.tasse, periodo.quotaaddvolo FROM prezzi, periodo where periodo.codiceperiodo = prezzi.codiceperiodo AND prezzi.codiceperiodo = '" & codiceperiodo & "' AND categoria = '" & categoria & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim prezzototale As Integer = 0
        Dim totale As Integer = 0
        If dr.HasRows Then
            dr.Read()

            Dim adulti2 As Integer = adulti + giovani + senior
            If adulti2 + bambini = 5 Then
                If almenouno(arraypax, adulti2 + bambini, 5) = False Then
                    addrowprezzo(ds2, "MSC crociere non prevede sistemazioni in cabine da 5 posti letto.", 0, 0, 0)
                    addrowprezzo(ds2, "In questo caso devi effettuare la quotazione per 2 cabine.", 0, 0, 0)
                    addrowprezzo(ds2, "Ti preghiamo contattare i nostri uffici al numero 0461 914471.", 0, 0, 0)
                    procedi = False
                End If
            End If
            If procedi = True Then
                Dim ttcab As String = " - " & ricavatipocab(Request.Params("tipologiacabina"))

                Dim paxtotali As Integer = adulti + giovani + senior + bambini
                addrowprezzo(ds2, "Totale cabina - categoria " & categoria & ttcab, paxtotali, CInt(dr("prezzo2a") / 2), CInt(dr("prezzo2a") / 2) * paxtotali)
                Dim singola As Integer = 0
                If paxtotali = 1 Then
                    singola = 90
                    addrowprezzo(ds2, "Supplemento singola", 0, 0, singola)
                End If

                Dim assiadulto As Integer = calcoloassiestate(prezzototale, 0)
                Dim fraseofferta As String = ricavafrase(Request.Params("tipologiacabina"), codiceperiodo)


                addrowprezzo(ds2, "Quota iscrizione comprensiva di tasse portuali ", 0, 0, 0)
                addrowprezzo(ds2, "Assicurazione medico bagaglio / annullamento adulto", 0, 0, 0)
                caricapac(ds2, Request.Params("frasepac"))
                If fraseofferta <> "" Then
                    addrowprezzo(ds2, fraseofferta, 0, 0, 0)
                End If
                addrowprezzo(ds2, "Bevande illimitate 24h: vino al calice in una selezione di 2 bianchi, 1 rosè, 2 rossi e 2 spumanti, birra alla spina, bibite analcoliche, bevande calde, acqua minerale, e un ampia selezione di bevande e cocktail disponibili al bar, oltre al gelato in cono e coppetta. Tutto senza limiti!", 0, 0, 0)
                addrowprezzo(ds2, "Quote di servizio incluse!", 0, 0, 0)
                If categoria = "00T" Then
                    addrowprezzo(ds2, "Viaggio in Pullman da Trento a Genova e ritorno", 0, 0, 0)
                ElseIf categoria = "00V" Then
                    addrowprezzo(ds2, "Viaggio in Pullman da Verona a Genova e ritorno", 0, 0, 0)
                ElseIf categoria = "00P" Then
                    addrowprezzo(ds2, "Viaggio in Pullman da Parma a Genova e ritorno", 0, 0, 0)
                End If
                addrowprezzo(ds2, "<b>TOTALE</b>", 0, 0, (CInt(dr("prezzo2a") / 2) * paxtotali) + singola)
                totale = (CInt(dr("prezzo2a") / 2) * paxtotali) + singola

            End If
        End If
        RepeaterPreventivo.DataSource = ds2.Tables("preventivo")
        RepeaterPreventivo.DataBind()
        prezzo.Text = totale
        dr.Close()
        cn.Close()
    End Sub

    Function ricavafrase(ByVal tipocab As Integer, ByVal codiceperiodo As String) As String
        ricavafrase = ""
        Dim sqlconn As String
        sqlconn = "SELECT * FROM periodo, promozioni WHERE periodo.idpromo = promozioni.id_promozione and periodo.codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            Select Case tipocab
                Case 2
                    If Not IsDBNull(dr("frasenelpreventivoi")) Then
                        If dr("frasenelpreventivoi") <> "" Then
                            ricavafrase = dr("frasenelpreventivoi")
                        End If
                    End If
                Case 4
                    If Not IsDBNull(dr("frasenelpreventivoe")) Then
                        If dr("frasenelpreventivoe") <> "" Then
                            ricavafrase = dr("frasenelpreventivoe")
                        End If
                    End If
                Case 6
                    If Not IsDBNull(dr("frasenelpreventivob")) Then
                        If dr("frasenelpreventivob") <> "" Then
                            ricavafrase = dr("frasenelpreventivob")
                        End If
                    End If
                Case 8
                    If Not IsDBNull(dr("frasenelpreventivos")) Then
                        If dr("frasenelpreventivos") <> "" Then
                            ricavafrase = dr("frasenelpreventivos")
                        End If
                    End If
            End Select
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function almenouno(ByVal arraypax() As String, ByVal persone As Integer, ByVal eta As Integer) As Boolean
        almenouno = False
        Dim i As Integer
        For i = 1 To persone
            If arraypax(i) <= eta Then
                almenouno = True
            End If
        Next
    End Function

    Private Sub prenotacosta(ByVal codiceperiodo As String, ByVal categoria As String, ByVal persone As String, ByVal arraypax() As String, ByVal cabina As String)
        Dim ws As New net.costaclick.web.Booking
        ws.SoapVersion = SoapProtocolVersion.Soap11
        Dim agenzia As New net.costaclick.web.Agency
        agenzia.Code = codicecosta
        ws.AgencyValue = agenzia
        Dim partner As New net.costaclick.web.Partner
        partner.Name = namecosta
        partner.Password = pswcosta
        ws.PartnerValue = partner
        Dim dispo As New net.costaclick.web.Component
        Dim cat As New net.costaclick.web.Category
        Dim far As New net.costaclick.web.Fare
        Dim cabin As New net.costaclick.web.Cabin
        cabin.Number = Trim(cabina)
        dispo.Cabin = cabin
        dispo.Insurance = True
        cat.Code = categoria
        If Right(categoria, 1) = "V" Then
            far.Code = "VALUE" 'Risparmiasubito
        Else
            far.Code = "IND"
        End If
        dispo.Category = cat
        dispo.Code = codiceperiodo
        dispo.Type = net.costaclick.web.ComponentType.Cruise
        dispo.Fare = far
        Dim compo(0) As net.costaclick.web.Component
        compo(0) = dispo
        If Request.Params("volo") > 0 Then
            Dim dispo2 As New net.costaclick.web.Component
            dispo2.Type = net.costaclick.web.ComponentType.Flight
            dispo2.Code = Request.Params("aeroporto")
            Dim city As New net.costaclick.web.City
            city.Code = Request.Params("aeroporto")
            If Request.Params("volo") = 1 Then
                dispo2.Direction = net.costaclick.web.Direction.Both
            ElseIf Request.Params("volo") = 2 Then
                dispo2.Direction = net.costaclick.web.Direction.OutBound
            ElseIf Request.Params("volo") = 3 Then
                dispo2.Direction = net.costaclick.web.Direction.InBound
            End If
            Dim cities(0) As net.costaclick.web.City
            cities(0) = city
            dispo2.Cities = cities
            dispo2.Insurance = True
            ReDim Preserve compo(1)
            compo(1) = dispo2
        End If
        Dim pass(0) As net.costaclick.web.Guest
        ReDim Preserve pass(persone - 1)
        Dim i As Integer
        Dim partenza As Date = ricavapart(Request.Params("codiceperiodo"))
        For i = 1 To persone
            Dim guest As New net.costaclick.web.Guest
            guest.FirstName = "Firstname " & i
            guest.LastName = "Lastname " & i
            guest.BirthDate = ricavatipodata(arraypax(i), partenza)
            pass(i - 1) = guest
            If i = 1 Then
                If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                    guest.CostaClubNumber = Request.Params("cartaclub")
                    guest.LastName = Request.Params("nomeclub")
                End If
            End If
        Next
        Dim quote As net.costaclick.web.QuoteDetail
        Try
            quote = ws.InvoiceAndPricing(compo, pass)
            GridPrezzi.DataSource = quote.Prices
            GridPrezzi.DataBind()
            Dim oItem As GridViewRow
            Dim totale As Integer = 0
            Dim ds2 As New dspreno
            For Each oItem In GridPrezzi.Rows
                Dim code As Label = CType(oItem.FindControl("Code"), Label)
                Dim Description As Label = CType(oItem.FindControl("Description"), Label)
                Dim Amount As Label = CType(oItem.FindControl("Amount"), Label)
                If Not IsDBNull(code.Text) Then
                    If code.Text = "CAB" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone categoria " & categoria & " - cabina n. " & cabina, 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "PCH" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone", 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "INS" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone", 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "TOT" Then
                        totale = (CInt(Amount.Text))
                    ElseIf code.Text = "AIR" Then
                        Dim direzione As String = ""
                        If Request.Params("volo") = 1 Then direzione = "di andata e ritorno da "
                        If Request.Params("volo") = 3 Then direzione = "di andata da "
                        If Request.Params("volo") = 2 Then direzione = "di ritorno a "
                        addrowprezzo(ds2, "Incluso volo " & direzione & ricavavolo(Request.Params("aeroporto")), 0, 0, 0)
                    ElseIf code.Text = "TRF" Then
                        addrowprezzo(ds2, "Incluso trasferimenti aeroporto / nave", 0, 0, 0)
                    End If
                End If
            Next oItem
            Dim dataquando As Date = CDate("01-05-2013")
            If DateDiff(DateInterval.Day, Date.Now, dataquando) > 0 And totale >= 900 Then
                addrowprezzo(ds2, "Sconto Fersina Viaggi se prenoti entro " & giornoArray(dataquando.DayOfWeek) & " " & dataquando.Day & " " & meseArray(dataquando.Month - 1) & " " & dataquando.Year, 0, 0, -50)
                totale = totale - 50
            End If
            caricapac(ds2, Request.Params("frasepac"))
            addrowprezzo(ds2, "<b>TOTALE</b>", 0, 0, totale + totalepac)
            RepeaterPreventivo.DataSource = ds2.Tables("preventivo")
            RepeaterPreventivo.DataBind()
            prezzo.Text = totale
        Catch ex As Exception
            paneleerrore.Visible = True
            PanelPreventivo.Visible = False
            labelerrore.Text = ex.Message.ToString
            labelerrore.Visible = True
        End Try
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

    Private Sub caricapac(ByVal ds2 As DataSet, ByVal frasepac As String)
        Dim stringa() As String
        stringa = Split(frasepac, ";")
        Dim word As String
        Dim indice As String
        Dim pax As String
        For Each word In stringa
            If word.IndexOf("-") >= 0 Then
                indice = Left(word, word.IndexOf("-"))
                pax = Right(word, word.Length - word.IndexOf("-") - 1)
                addpac(ds2, CInt(indice), CInt(pax))
            End If
        Next
    End Sub


    Private Sub addpac(ByVal ds2 As DataSet, ByVal idpac As Integer, ByVal pax As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM pacchetti where pacchetti.id_pacchetto = " & idpac
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            addrowprezzo(ds2, dr("nomepacchetto").ToString.ToLower, pax, dr("vendita"), pax * dr("vendita"))
            totalepac = totalepac + (pax * dr("vendita"))
        End If
        dr.Close()
        cn2.Close()
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

    Function ricavatipocab(ByVal tipologia As Integer) As String
        ricavatipocab = ""
        Select Case tipologia
            Case 1
                ricavatipocab = "Cabina Interna"
            Case 2
                ricavatipocab = "Cabina Interna"
            Case 3
                ricavatipocab = "Cabina Esterna con Finestra"
            Case 4
                ricavatipocab = "Cabina Esterna con Finestra"
            Case 5
                ricavatipocab = "Cabina Esterna con Balcone"
            Case 6
                ricavatipocab = "Cabina Esterna con Balcone"
            Case 7
                ricavatipocab = "Cabina Suite"
            Case 8
                ricavatipocab = "Cabina Suite"
        End Select
    End Function

    Protected Sub RepeaterPreventivo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPreventivo.ItemDataBound
        Dim persone As Label = CType(e.Item.FindControl("persone"), Label)
        Dim prezzo As Label = CType(e.Item.FindControl("prezzo"), Label)
        Dim totale As Label = CType(e.Item.FindControl("totale"), Label)
        Dim prezzop As Label = CType(e.Item.FindControl("prezzop"), Label)
        Dim totalep As Label = CType(e.Item.FindControl("totalep"), Label)
        Dim descrizione As Label = CType(e.Item.FindControl("descrizione"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            contarighe = contarighe + 1
            If persone.Text = 0 Then persone.Visible = False
            If prezzo.Text = 0 Then prezzo.Visible = False
            If totale.Text = 0 Then totale.Visible = False
            If descrizione.Text.IndexOf("illimitate") >= 0 Then
                Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
                riga.Style.Add("height", "55px")
            End If
            If Trim(descrizione.Text) = "<b>TOTALE</b>" Then
                Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
                totale.Text = "<b>" & totale.Text & "</b>"
                totale.Style.Add("font-size", "x-large")
                totale.Style.Add("width", "200px")
                totale.Style.Add("margin-left", "480px")
                descrizione.Style.Add("font-size", "x-large")
                riga.Style.Add("height", "35px")
            Else
                eccopreventivo = eccopreventivo & descrizione.Text & "(" & persone.Text & ")" & "(" & prezzop.Text & ")" & "(" & totalep.Text & ")" & "$"
            End If

        End If
    End Sub

    Function calcoloassiestate(ByVal prezzo As Integer, ByVal compagnia As Integer) As Integer
        calcoloassiestate = 0
        If compagnia = 0 Then
            If prezzo <= 800 Then
                calcoloassiestate = 24
            ElseIf prezzo > 800 And prezzo <= 1300 Then
                calcoloassiestate = 34
            ElseIf prezzo > 1300 And prezzo <= 1850 Then
                calcoloassiestate = 40
            ElseIf prezzo > 1850 And prezzo <= 2350 Then
                calcoloassiestate = 46
            ElseIf prezzo > 2350 And prezzo <= 3400 Then
                calcoloassiestate = 54
            ElseIf prezzo > 3400 And prezzo <= 5200 Then
                calcoloassiestate = 70
            ElseIf prezzo > 5200 And prezzo <= 7750 Then
                calcoloassiestate = 99
            ElseIf prezzo > 7750 And prezzo <= 12000 Then
                calcoloassiestate = 170
            End If
        ElseIf compagnia = 1 Then
            If prezzo <= 800 Then
                calcoloassiestate = 25
            ElseIf prezzo > 800 And prezzo <= 1300 Then
                calcoloassiestate = 35
            ElseIf prezzo > 1300 And prezzo <= 1850 Then
                calcoloassiestate = 45
            ElseIf prezzo > 1850 And prezzo <= 2350 Then
                calcoloassiestate = 49
            ElseIf prezzo > 2350 And prezzo <= 3400 Then
                calcoloassiestate = 59
            ElseIf prezzo > 3400 And prezzo <= 5200 Then
                calcoloassiestate = 79
            ElseIf prezzo > 5200 And prezzo <= 7750 Then
                calcoloassiestate = 99
            ElseIf prezzo > 7750 And prezzo <= 12000 Then
                calcoloassiestate = 199
            ElseIf prezzo > 12000 And prezzo <= 26000 Then
                calcoloassiestate = 498
            End If
        End If

    End Function

End Class
