Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Partial Class crociere_conferma_crociere
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim mailconferma As String = ConfigurationSettings.AppSettings("mailconferma")
    Dim psswmail As String = ConfigurationSettings.AppSettings("psswmail")
    Dim miamail As String = ConfigurationSettings.AppSettings("miamail")
    Dim miosmtp As String = ConfigurationSettings.AppSettings("miosmtp")
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim nomezona(0) As String
    Dim contarighe As Integer = 0
    Dim imageback As String = ""
    Dim idperiodo As Integer = 0
    Dim tab As String = ""
    Dim compagnia As Integer = 0
    Dim nave2(0) As String
    Dim fotonave2(0) As String
    Dim idperiodo2 As Integer = 0
    Dim nave3 As String = ""
    Dim nave4 As String = ""


    Private Sub caricanave()
        Dim sqlconn As String
        sqlconn = "SELECT * from nave order by id_nave"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Do While dr.Read()
                ReDim Preserve nave2(dr("id_nave"))
                nave2(dr("id_nave")) = dr("titolo")
                ReDim Preserve fotonave2(dr("id_nave"))
                fotonave2(dr("id_nave")) = dr("fotop")
            Loop
        End If
        dr.Close()
        cn.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        Dim email As String = Request.Params("email")
        Dim codice As String = Request.Params("codice")
        If Not Page.IsPostBack Then
            Call caricanave()
            caricazona()
            lnkcrocere.Text = "Per ricevere nuovamente questa pratica via mail premi qui"
            lnkcrocere.NavigateUrl = "conferma-crociere.aspx?maila=1&codice=" & codice & "&email=" & email
            If Not IsNothing(email) And Not IsNothing(codice) Then
                Call caricadati(email, codice)
                If Request.Params("maila") = 1 Then
                    Call vedi(email, codice, compagnia)
                End If
                If Request.Params("lt") = 1 Then
                    Call salvaletto(email, codice)
                End If
                If Request.Params("lt") = 2 Then
                    If aggiornata(email, codice) = True Then
                        Call salvaletto(email, codice)
                    End If
                End If
                Dim MyRnd As New Random
                If PanelAttesa.Visible = True Then
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "caricaitinerario('itinerario','itinerario.aspx?id=" & idperiodo & "', '" + imageback + "'); caricadatinave('datinave', 'nave.aspx?id=" & idperiodo & "', '" + imageback + "', '" & idperiodo & "', '" & idperiodo2 & "', '" & nave3 & "','" & nave4 & "', '1');", True)
                Else
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "caricaitinerario('itinerario','itinerario.aspx?id=" & idperiodo & "', '" + imageback + "'); caricadatinave('datinave', 'nave.aspx?id=" & idperiodo & "', '" + imageback + "', '" & idperiodo & "', '" & idperiodo2 & "', '" & nave3 & "','" & nave4 & "', '1'); cccframe('divapriframe', 'pagamento.aspx?parsel=" & sconverti(codice) & "&code=" & codice & "&email=" & email & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabpag');" & tab, True)
                End If

            End If
        End If

    End Sub

    Function aggiornata(ByVal email As String, ByVal codice As String) As Boolean
        aggiornata = False
        Dim sqlconn As String
        sqlconn = "SELECT  itinerario.*, periodo.*, nave.*, preno.*, preno.idpromo as promop,  periodo.durata as pdurata, nave.titolo AS titolonave, itinerario.titolo as titoloitinerario from itinerario, periodo, nave, preno, promo WHERE  preno.id_periodo = periodo.id_periodo AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.id_nave = nave.id_nave AND preno.id_preno = " & sconverti(codice) & " AND preno.pubblica = 0 AND (preno.email = '" & email & "' OR preno.email2 = '" & email & "')"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If Not IsDBNull(dr("nopzione")) Then
                If dr("nopzione") > 0 Then
                    aggiornata = True
                End If
            End If
        End If
        dr.Close()
        cn.Close()
    End Function

    Private Sub salvaletto(ByVal email As String, ByVal codice As String)
        Dim stringa As String = "UPDATE preno SET letto = @letto WHERE id_preno = " & sconverti(codice) & " AND preno.pubblica = 0 AND preno.email = '" & email & "'"
        Dim cmd3 As New MySqlCommand(stringa, cn)
        cmd3.Parameters.AddWithValue("@letto", 1)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd3.ExecuteNonQuery()
        cmd3.Connection.Close()
        cn.Close()
    End Sub

    Private Sub vedi(ByVal email As String, ByVal codice As String, ByVal compagnia As Integer)
        Dim preno As String = ""
        Dim aste As String = "-------------------------------------------------------------------------"
        Dim aste2 As String = "************************************************************************"
        Dim sp As String = "<br />"
        Dim htmlvero As Boolean = True
        If PanelAttesa.Visible = True Then
            If compagnia = 0 Then
                preno = preno & "**** RICHIESTA QUOTAZIONE MSC CROCIERE ****" & sp & sp
            ElseIf compagnia = 1 Then
                preno = preno & "**** RICHIESTA QUOTAZIONE COSTA CROCIERE****" & sp & sp
            End If
            preno = preno & aste & sp
            preno = preno & "La presente prenotazione è in fase di elaborazione. Non appena avremmo notizie relative alla disponibilità le invieremmo una mail di conferma dove potrà scegliere senza nessun impegno se confermare o meno l'offerta." & sp
            preno = preno & aste & sp & sp
            preno = preno & "Puoi accedere alla richiesta in qualsiasi momento cliccando il seguente link:  <a href='http://www.fersinaviaggi.it/crociere/conferma-crociere.aspx?codice=" & codice & "&email=" & email & "'>http://www.fersinaviaggi.it/crociere/conferma-crociere.aspx?codice=" & codice & "&email=" & email & "</a>"
            preno = preno & "Il presente documento è di natura confidenziale e non può essere divulgato a soggetti diversi  senza il consenso scritto di Fersina Viaggi. " & sp
            preno = preno & sp & sp & sp & sp & sp
            preno = preno & "TUTELA DELLA PRIVACY - per leggere l'informativa è presente al seguente link http://www.fersinaviaggi.it/privacy.html" & sp
            preno = preno & "Gli indirizzi e-mail nel nostro archivio provengono da richieste pervenute al nostro recapito, da elenchi e servizi di pubblico dominio o pubblicati in internet. In ottemperanza alla legge 196/2003 (codice privacy) per la tutela delle persone e di altri soggetti rispetto al trattamento di dati personali, in ogni momento è possibile modificare o cancellare i dati presenti nei nostri archivi inviando una mail a info@fersinaviaggi.it" & sp
            preno = preno & "Ai sensi del D.Lgs. 196/2003 si precisa che le informazioni contenute in questo messaggio sono riservate ed a uso esclusivo del destinatario. Qualora il messaggio in parola Le fosse pervenuto per errore, La invitiamo ad eliminarlo senza copiarlo e a non inoltrarlo a terzi." & sp
            preno = preno & "Pursuant to Legislative Decree No. 196/2003, you are hereby informed that this message contains confidential information intended only for the use of the addressee. If you are not the addressee, and have received this message by mistake, please delete it and immediately notify us. You may not copy or disseminate this message to anyone. Thank you." & sp
        Else
            Dim sqlconn As String
            sqlconn = "SELECT nave.*, preno.* , periodo.id_nave2, periodo.dal2, periodo.al2, periodo.imbarco as pimbarco, periodo.sbarco as psbarco FROM preno, nave, periodo WHERE periodo.id_periodo = preno.id_periodo AND nave.id_nave = preno.id_nave AND preno.codice = '" & codice & "' AND preno.email = '" & email & "'"
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                Dim tipocabina As String = ""
                If dr("tipocabina") = 0 Then
                    tipocabina = "Esterna con balcone"
                ElseIf dr("tipocabina") = 1 Then
                    tipocabina = "Esterna con finestra"
                ElseIf dr("tipocabina") = 2 Then
                    tipocabina = "Interna"
                ElseIf dr("tipocabina") = 3 Then
                    tipocabina = "Esterna con vista ostruita"
                ElseIf dr("tipocabina") = 4 Then
                    tipocabina = "Suite"
                ElseIf dr("tipocabina") = 5 Then
                    tipocabina = "Aurea Suite"
                ElseIf dr("tipocabina") = 7 Then
                    tipocabina = "offerta SUPER BINGO"
                ElseIf dr("tipocabina") = 8 Then
                    tipocabina = "offerta BINGO ESTERNA"
                ElseIf dr("tipocabina") = 9 Then
                    tipocabina = "offerta BINGO BALCONE"
                ElseIf dr("tipocabina") = 10 Then
                    tipocabina = "Deluxe Suite"
                ElseIf dr("tipocabina") = 11 Then
                    tipocabina = "Executive & Family Suite"
                ElseIf dr("tipocabina") = 12 Then
                    tipocabina = "Royal Suite"
                End If
                preno = preno & "<table><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Referente:</td><td style='width:400px'>" & dr("nome") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Telefono:</td><td style='width:400px'>" & dr("telefono") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>E-mail:</td><td style='width:400px'>" & dr("email") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Passeggeri:</td><td style='width:400px'>" & dr("adulti") + dr("bambini") & "</td></tr><tr>"
                If dr("id_nave2") = 0 Then
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Nave:</td><td style='width:400px'>" & dr("titolo").ToString.ToUpper & "</td></tr><tr>"
                Else
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Nave:</td><td style='width:400px'>ROULETTE " & dr("titolo").ToString.ToUpper & "/" & ricavanave(dr("id_nave2")) & "</td></tr><tr>"
                End If
                If IsDBNull(dr("dal2")) Then
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Imbarco:</td><td style='width:400px'>" & dr("imbarco") & "</td></tr><tr>"
                Else
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Imbarco:</td><td style='width:400px'>" & dr("pimbarco") & " " & giornoArray(CDate(dr("dal")).DayOfWeek) & " " & CDate(dr("dal")).Day & " " & meseArray(CDate(dr("dal")).Month - 1) & " o " & giornoArray(CDate(dr("dal2")).DayOfWeek) & " " & CDate(dr("dal2")).Day & " " & meseArray(CDate(dr("dal2")).Month - 1) & " " & CDate(dr("dal2")).Year & "</td></tr><tr>"
                End If
                If IsDBNull(dr("al2")) Then
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Sbarco:</td><td style='width:400px'>" & dr("sbarco") & "</td></tr><tr>"
                Else
                    preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Sbarco:</td><td style='width:400px'>" & dr("psbarco") & " " & giornoArray(CDate(dr("al")).DayOfWeek) & " " & CDate(dr("al")).Day & " " & meseArray(CDate(dr("al")).Month - 1) & " o " & giornoArray(CDate(dr("al2")).DayOfWeek) & " " & CDate(dr("al2")).Day & " " & meseArray(CDate(dr("al2")).Month - 1) & " " & CDate(dr("al2")).Year & "</td></tr><tr>"
                End If
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Durata:</td><td style='width:400px'>" & dr("durata") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Prezzo totale:</td><td style='width:400px'>" & Format(dr("prezzo"), "##,##0.00") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Opzione:</td><td style='width:400px'>" & dr("nopzione") & "</td></tr><tr>"
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Scadenza:</td><td style='width:400px'>" & CDate(dr("opzione")).Date & " <span style='width:100px; color: #067788; font-weight:bold;'>ore:</span> " & inseriscizero(CDate(dr("opzione")).Hour) & ":" & inseriscizero(CDate(dr("opzione")).Minute) & "</td></tr><tr>"
                Dim appcab As String = ""
                appcab = dr("cabina")
                If dr("cabina") = "G00000" Or dr("cabina").ToString.ToUpper = "GARANTITA" Then
                    appcab = "Garantita"
                End If
                preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Cabina:</td><td style='width:400px'>" & appcab & " - " & tipocabina & "</td></tr><tr>"

                
                If Not IsDBNull(dr("categoria")) Then
                    If dr("categoria") <> "" Then
                        preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Categoria:</td><td style='width:400px'>" & dr("categoria") & "</td></tr><tr>"
                    End If
                End If
                If Not IsDBNull(dr("ponte")) Then
                    If dr("ponte") <> "" Then
                        preno = preno & "<td style='width:100px; color: #067788; font-weight:bold;'>Ponte:</td><td style='width:400px'>" & dr("ponte") & "</td></tr><tr>"
                    End If
                End If
                preno = preno & "</tr></table>"
                preno = preno & sp & sp
                If dr("cabina") = "G00000" Or dr("cabina").ToString.ToUpper = "GARANTITA" Then
                    Dim apptesto As String = "Cabina Garantita: durante la crociera potrebbe essere richiesto un cambio cabina (raramente)"
                    If dr("adulti") + dr("bambini") >= 4 Then
                        If compagnia = 0 Then
                            apptesto = apptesto & "<br/>Per le cabine quadruple garantite MSC si riserva il diritto di assegnare anche 2 cabine distinte non obbligatoriamente vicine"
                        End If
                    End If
                    preno = preno & "<span style='font-size:xx-small;'>" & apptesto & "</span>"
                End If
                preno = preno & sp & sp
                preno = preno & aste & sp & sp
                preno = preno & "Può accedere alla richiesta in qualsiasi momento cliccando il seguente link: <a href='http://www.fersinaviaggi.it/crociere/conferma-crociere.aspx?codice=" & codice & "&email=" & email & "'>http://www.fersinaviaggi.it/crociere/conferma-crociere.aspx?codice=" & codice & "&email=" & email & "</a>"
            End If
            dr.Close()
            cn.Close()
        End If
        preno = preno & sp & sp & "Cordiali saluti" & sp
        preno = preno & sp & sp & "Fersina Viaggi" & sp
        preno = preno & "info@fersinaviaggi.it" & sp
        preno = preno & "www.fersinaviaggi.it" & sp & sp
        preno = preno & "Agenzia Fersina Viaggi" & sp
        preno = preno & "Via Stella, 5/M" & sp
        preno = preno & "38123 - Trento" & sp
        preno = preno & "Tel. +39 0461 914471" & sp
        preno = preno & "Fax. +39 0461 1810136" & sp
        preno = preno & sp
        preno = preno & "FERSINA VIAGGI premiata agli <a href='http://www.fersinaviaggi.it/msc-awards.html' target='_blank' >MSC Adwards 2011 e Msc Adwards 2012</a> (2 anni consecutivi) come miglior agenzia Web MSC Crociere." & sp
        preno = preno & "A noi tutti un’incitazione a continuare sulla strada della professionalità, che ci ha consentito di salire la piramide fino a raggiungere la vetta come numero di cabine vendute sul mercato del web italiano! " & sp & sp & sp & sp
        preno = preno & "<img src='http://www.fersinaviaggi.it/images/msc-fer.gif' alt='Fersina Viaggi' />" & sp & sp
        preno = preno & "<span style='font-size:xx-small'>Il presente documento è di natura confidenziale e non può essere divulgato a soggetti diversi  senza il consenso scritto di Fersina Viaggi. <br/>"

        preno = preno & "<b>TUTELA DELLA PRIVACY</b> - per leggere l'informativa <a href='http://www.fersinaviaggi.it/privacy.html'>premi qui</a><br />"
        preno = preno & "Gli indirizzi e-mail nel nostro archivio provengono da richieste pervenute al nostro recapito, da elenchi e servizi di pubblico dominio o pubblicati in internet. In ottemperanza alla legge 196/2003 (codice privacy) per la tutela delle persone e di altri soggetti rispetto al trattamento di dati personali, in ogni momento è possibile modificare o cancellare i dati presenti nei nostri archivi inviando una mail a info@fersinaviaggi.it <br />"
        preno = preno & "Ai sensi del D.Lgs. 196/2003 si precisa che le informazioni contenute in questo messaggio sono riservate ed a uso esclusivo del destinatario. Qualora il messaggio in parola Le fosse pervenuto per errore, La invitiamo ad eliminarlo senza copiarlo e a non inoltrarlo a terzi.<br />"
        preno = preno & "Pursuant to Legislative Decree No. 196/2003, you are hereby informed that this message contains confidential information intended only for the use of the addressee. If you are not the addressee, and have received this message by mistake, please delete it and immediately notify us. You may not copy or disseminate this message to anyone. Thank you.</span>"
        SendMail(email, codice, preno, htmlvero, compagnia)
        SendMail(mailconferma, codice, preno, htmlvero, compagnia)
    End Sub



    Private Sub SendMail(ByVal maildest As String, ByVal codice As String, ByVal preno As String, ByVal iishtml As Boolean, ByVal compagnia As Integer)
        Dim objMail As New MailMessage
        Dim strTemp As String = "Riceverà un e-mail di conferma al seguente indirizzo: "
        Dim Smtpmail As New SmtpClient(miosmtp)
        Dim da As New MailAddress(miamail)
        Smtpmail.Credentials = New System.Net.NetworkCredential(miamail, psswmail)
        objMail.From = da
        If compagnia = 0 Then
            objMail.Subject = "Richiesta prenotazione MSC CROCIERE: " & codice
        ElseIf compagnia = 1 Then
            objMail.Subject = "Richiesta prenotazione COSTA CROCIERE: " & codice
        End If
        objMail.IsBodyHtml = iishtml
        objMail.Priority = MailPriority.High
        objMail.Body = preno
        Try ' proviamo ad inviare l'email...
            objMail.To.Add(maildest)
            Smtpmail.Send(objMail)
        Catch Ex As Exception ' si e' verificato un errore
            strTemp = "Errore nell'invio: "
            strTemp += Ex.Message
        End Try
        If maildest = mailconferma Then
        Else
            'lblmessaggio.Text = strTemp ' mostriamo a video l'esito dell'invio
        End If
        objMail.Dispose()
    End Sub



    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Private Sub caricadati(ByVal email As String, ByVal codice As String)
        Dim sqlconn As String
        Dim dal, al As Date
        sqlconn = "SELECT  itinerario.*, periodo.*, nave.*, preno.*,  preno.imbarco as pimbarco, preno.sbarco as psbarco, periodo.durata as pdurata, nave.titolo AS titolonave, itinerario.titolo as titoloitinerario from itinerario, periodo, nave, preno WHERE  preno.id_periodo = periodo.id_periodo AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.id_nave = nave.id_nave AND preno.id_preno = " & sconverti(codice) & " AND preno.pubblica = 0 AND (preno.email = '" & email & "' OR preno.email2 = '" & email & "')"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim visto As Boolean = False
        If dr.HasRows Then
            dr.Read()
            compagnia = dr("compagnia")
            idperiodo = dr("id_periodo")
            If dr("compagnia") = 0 Then
                distributore.Src = "../images/distributore-msc.jpg"
                distributore.Alt = "Ditributore ufficiale Msc Crociere"
                imageback = "url(../images/attesa-msc.jpg) no-repeat;"
            ElseIf dr("compagnia") = 1 Then
                distributore.Src = "../images/distributore-costa.jpg"
                distributore.Alt = "Ditributore ufficiale Costa Crociere"
                imageback = "url(../images/attesa-costa.jpg) no-repeat;"
                Label10.Text = "N°pratica Costa:"
            End If
            dal = dr("dal")
            al = dr("al")
            lbldatapreno.Text = Format(dr("data_preno"), "dd/MM/yyyy")
            lblcodicepreno.Text = dr("codice")
            lbltitolo.Text = dr("titolonave") & " - " & giornoArray(dal.DayOfWeek) & " " & dal.Day & " " & meseArray(dal.Month - 1) & " " & dal.Year
            lblnave.Text = dr("titolonave")
            lblitinerario.Text = dr("titoloitinerario")
            lbldurata.Text = dr("pdurata") & " notti"
            lblimbarco.Text = controllacampo(dr, "pimbarco") '& " - " & giornoArray(dal.DayOfWeek) & " " & dal.Day & " " & meseArray(dal.Month - 1) & " " & dal.Year
            lblsbarco.Text = controllacampo(dr, "psbarco") '& " - " & giornoArray(al.DayOfWeek) & " " & al.Day & " " & meseArray(al.Month - 1) & " " & al.Year
            If Not IsDBNull(dr("voloda")) Then
                If dr("voloda") <> "" Then
                    voloda.Text = controllacampo(dr, "voloda")
                    Dim livoloda As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("livoloda"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    livoloda.Visible = True
                End If
            End If
            If Not IsDBNull(dr("voloa")) Then
                If dr("voloa") <> "" Then
                    voloa.Text = controllacampo(dr, "voloa")
                    Dim livoloa As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("livoloa"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    livoloa.Visible = True
                End If
            End If
            Image2.ImageUrl = dr("foto")
            Image3.ImageUrl = dr("mappa")
            Labelnave.Text = UpCase(dr("titolonave"))
            Labeldescri.Text = dr("pdurata") & " notti a bordo di " & Labelnave.Text & " da " & UpCase(dr("imbarco")) & " il <b>" & dr("dal") & "</b>"
            labeltitolo.Text = "Preventivo " & dr("titolonave") & " partenza del " & Format(dr("dal"), "dd/MM/yyyy") & " <span style='font-size:small'>per " & dr("adulti") + dr("bambini") & " persone</span>"
            If dr("id_nave2") > 0 Then
                labeltitolo.Text = "Preventivo Roulette " & dr("titolonave") & " / " & nave2(dr("id_nave2")) & " <span style='font-size:small'>per " & dr("adulti") + dr("bambini") & " persone</span>"
                nave3 = Replace(dr("titolonave"), "COSTA", "C.")
                nave4 = Replace(ricavanave(dr("id_nave2")), "COSTA", "C.")
                lblnave.Text = "Formula Roulette fra " & dr("titolonave") & " / " & nave2(dr("id_nave2"))
                If Not IsDBNull(dr("dal2")) Then
                    idperiodo2 = ricavaperiodo2(dr("id_nave2"), dr("dal2"))
                    Dim dal2 As Date = CDate(dr("dal2"))
                    lblimbarco.Text = lblimbarco.Text & " / " & giornoArray(dal2.DayOfWeek) & " " & dal2.Day & " " & meseArray(dal2.Month - 1) & " " & dal2.Year
                Else
                    idperiodo2 = ricavaperiodo2(dr("id_nave2"), dr("dal"))
                End If
                If Not IsDBNull(dr("al2")) Then
                    Dim al2 As Date = CDate(dr("al2"))
                    lblsbarco.Text = lblsbarco.Text & " / " & giornoArray(al2.DayOfWeek) & " " & al2.Day & " " & meseArray(al2.Month - 1) & " " & al2.Year
                End If
                Labelnave.Text = "<span style='font-size:small'>Formula Roulette fra:</span><br/>" & Labelnave.Text & "<br/>" & UpCase(nave2(dr("id_nave2")))
                Labeldescri.Text = "partenza il <b>" & dr("dal") & "</b>"
                Image3.ImageUrl = fotonave2(dr("id_nave2"))
                If Not IsDBNull(dr("dal2")) Then
                    Labeldescri.Text = Labeldescri.Text & " o il <b>" & dr("dal2") & "</b>"
                End If
            End If
            If Not IsDBNull(dr("zona")) Then
                If dr("zona") > 0 Then
                    lbltitolo.Text = nomezona(CInt(dr("zona")) - 1) & ": " & UpCase(Replace(Replace(dr("titolo"), ", ", ","), ",", ", "))
                Else
                    lbltitolo.Text = UpCase(Replace(Replace(dr("titolo"), ", ", ","), ",", ", "))
                End If
            Else
                lbltitolo.Text = UpCase(Replace(Replace(dr("titolo"), ", ", ","), ",", ", "))
            End If

            If Not IsDBNull(dr("nopzione")) And Not IsDBNull(dr("opzione")) Then
                If dr("nopzione") > 0 Then
                    PanelAttesa.Visible = False
                    PanelOk.Visible = True
                    Paneldettaglio.Visible = True
                    lblopzione.Text = dr("nopzione")
                End If
            End If
            Dim alte As Integer = 0
            If Not IsDBNull(dr("opzione")) Then
                lblscadenza.Text = "entro le ore " & CDate(dr("opzione")).Hour & ":" & inseriscizero(CDate(dr("opzione")).Minute) & " del giorno " & Format(CDate(dr("opzione")), "dd/MM/yy")
                If dr("nopzione") = 1000000 Then
                    Labelscd.Visible = True
                    Labelscd.ForeColor = Color.Red
                    Labelscd.Text = "<span style='font-size:small'>La cabina selezionata è in conferma immediata. Per confermarla è necessario<br /> eseguire il pagamento immediato con carta di credito, pena decadenza della cabina stessa!</span><br/><br/>"
                    Dim liscade As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("liscade"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    alte = alte + 60
                    liscade.Style.Add("height", "60px")
                    liscade.Style.Add("width", "680px")
                    If Left(dr("codiceperiodo"), 1) = "Z" Then
                        alte = alte + 120
                        liscade.Style.Add("height", "120px")
                        liscade.Style.Add("width", "680px")
                        Labelscd.Text = Labelscd.Text & "<span style='font-size:small'>La Formula Roulette non permette di scegliere quale itinerario. L'itinerario sarà assegnato insindacabilmente<br /> da Costa Crociere (anche nel caso di itinerari da porti e date di partenza differenti)!</span><br /><br />"
                    End If

                    
                End If
            Else
                lblscadenza.Text = "in attesa di conferma"
            End If

            If Right(dr("categoria"), 1) = "V" Or Right(dr("categoria"), 1) = "X" Then
                Dim liscade As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("liscade"), System.Web.UI.HtmlControls.HtmlGenericControl)
                imagesotto.Style.Add("margin-top", "260px")
                Labelscd.Visible = True
                If Right(dr("categoria"), 1) = "X" Then
                    Labelscd.Text = Labelscd.Text & "<div style='font-size:small; width:680px; text-align:justify; font-weight:normal; color:#000000;'>La formula Mordi e Fuggi è prenotabile in conferma immediata alle seguenti condizioni:<br/>"
                Else
                    Labelscd.Text = Labelscd.Text & "<div style='font-size:small; width:680px; text-align:justify; font-weight:normal; color:#000000;'>La formula RisparmiaSubito è prenotabile in conferma immediata alle seguenti condizioni:<br/>"
                End If

                Labelscd.Text = Labelscd.Text & "- si sceglie la tipologia di cabina tra interna, vista mare o balcone, ma non il numero e la posizione. <br/>"
                Labelscd.Text = Labelscd.Text & "- Non è cumulabile con altre promozioni, né con lo sconto 5% Frequent Guest e non accumula punti CostaClub.<br/>"
                Labelscd.Text = Labelscd.Text & "- la Colazione in cabina e room service sono a pagamento senza possibilita' di scelta del turno ristorante<br/><br/>"
                Labelscd.Text = Labelscd.Text & "Annullamento:<br/>"
                Labelscd.Text = Labelscd.Text & "- a 45 giorni o più dalla data di partenza, la penale è del 25% del prezzo del pacchetto acquistato.<br/>"
                Labelscd.Text = Labelscd.Text & "- tra i 15 e i 44 giorni dalla data di partenza, la penale è del 75% del prezzo del pacchetto acquistato.<br/>"
                Labelscd.Text = Labelscd.Text & "- 14 giorni o meno dalla partenza, la penale è pari all’importo totale della pratica.</div><br />"
                alte = alte + 190
                liscade.Style.Add("height", alte & "px")
                liscade.Style.Add("width", "680px")
            End If
            If sipagamento(dr("id_preno")) = True Then
                If dr("datidoc") > 0 Then
                    lblscade.Text = "Biglietti:"
                    lblscadenza.ForeColor = Color.DarkGreen
                    lblscadenza.Text = "Attendere i documenti di viaggio"
                    alte = alte + 70
                    liscade.Style.Add("height", alte & "px")
                    liscade.Style.Add("width", "680px")
                    Labelscd.Visible = True
                    Labelscd.ForeColor = Color.Green
                    Labelscd.Text = Labelscd.Text & "<span style='font-size:small;'>Vi spediremmo i biglietti via mail prima della partenza.<br /> I biglietti si potranno stampare anche direttamente da questa pagina nella sezione biglietti.</span>"
                Else
                    imagesotto.Visible = True
                    lblscadenza.Text = "E' obbligatorio inserire documenti d'identità."
                    lblscade.Text = "Importante:"
                    Dim liscade As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("liscade"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    alte = alte + 100
                    liscade.Style.Add("height", alte & "px")
                    liscade.Style.Add("width", "680px")
                    Labelscd.Visible = True
                    Labelscd.ForeColor = Color.Blue
                    Labelscd.Text = Labelscd.Text & "<span style='font-size:small'>Preghiamo inserire i dati relativi ai propri documenti d'identità nella sezione documenti.<br />Ai fini della legge relativa alla privacy preghiamo <b>non inviare</b> documenti via mail o via fax.<br /> In mancanza dell'inserimento dei documenti d'identità non possiamo inviare i biglietti.</span><br/><br/>"
                End If
            End If

            Labelpasseggeri.Text = dr("adulti") + dr("bambini")
            'mailx.Text = dr("email")
            compilapreventivo(dr("id_preno"))
            tot.Text = Format(dr("prezzo") + dr("prezzopac"), "€ ##,##0.00")

            If dr("prezzo") = 0 Then
                tot.Visible = False
                labeltitolo.Text = "La categoria scelta non è disponibile. Ricerca in corso..."
                lbltot.Text = "<span style='font-size:small'>E' stata scelta una categoria non disponibile. Sarà nostra cura effettuare la ricerca della cabina richiesta e contattarla prima possibile essendo però non disponibile non possiamo garantire l'esito positivo.</span>"
            End If
            ' riempipagamenti(dr("id_preno"))
            Dim tabpag As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabpag"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabdoc As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabdoc"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabbig As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabbig"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabpac As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabpac"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabquo As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabquo"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabpon As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabpon"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim tabcon As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabcon"), System.Web.UI.HtmlControls.HtmlAnchor)             
            Dim tabassi As System.Web.UI.HtmlControls.HtmlAnchor = CType(Page.FindControl("tabassi"), System.Web.UI.HtmlControls.HtmlAnchor)
            Dim MyRnd As New Random
            tab = tab & "tab[0]='" & tabpag.ClientID & "';"
            tab = tab & "tab[1]='" & tabdoc.ClientID & "';"
            tab = tab & "tab[2]='" & tabbig.ClientID & "';"
            tab = tab & "tab[3]='" & tabpac.ClientID & "';"
            tab = tab & "tab[4]='" & tabquo.ClientID & "';"
            tab = tab & "tab[5]='" & tabpon.ClientID & "';"
            tab = tab & "tab[6]='" & tabcon.ClientID & "';"
            tab = tab & "tab[7]='" & tabassi.ClientID & "';"
            tabpag.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'pagamento.aspx?parsel=" & dr("id_preno") & "&code=" & dr("codice") & "&email=" & dr("email") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabpag')")
            tabdoc.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'documenti.aspx?parsel=" & dr("id_preno") & "&code=" & dr("codice") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabdoc')")
            tabbig.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'biglietti.aspx?parsel=" & dr("id_preno") & "&code=" & dr("codice") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabbig')")
            If dr("compagnia") = 0 Then
                tabpon.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'ponti.aspx?idnave=" & dr("id_nave") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabpon')")
            Else
                If Not IsDBNull(dr("cabina")) Then
                    If dr("cabina") <> "" And dr("cabina") <> "0" Then
                        tabpon.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'poscabina.aspx?cabina=" & dr("cabina") & "&codicenave=" & dr("codicenave") & "&categoria=" & dr("categoria") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabpon')")
                    End If
                End If
            End If
            tabpac.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'pacchetti.aspx?conf=1&email=" & dr("email") & "&codice=" & dr("codice") & "&parsel=" & dr("id_preno") & "&adulti=" & dr("adulti") & "&bambini=" & dr("bambini") & "&persone=" & dr("adulti") + dr("bambini") & "&codiceperiodo=" & dr("codiceperiodo") & "&frasepac=" & dr("frasepac") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabpac')")
            tabquo.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'quote.aspx?codiceperiodo=" & dr("codiceperiodo") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabquo')")
            tabcon.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'contratto.aspx?parsel=" & dr("id_preno") & "&code=" & dr("codice") & "&email=" & dr("email") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabcon')")
            If dr("rinunciaassi") = 0 Then
                tabassi.Attributes.Add("onclick", "javascript:cccframe('divapriframe', 'assi.aspx?parsel=" & dr("id_preno") & "&code=" & dr("codice") & "&email=" & dr("email") & "&rn=" & Trim(Str(MyRnd.Next(9999999))) & "', '" & imageback & "');cambia('tabassi')")
            Else
                tabassi.Visible = False
            End If
            If Not IsDBNull(dr("categoria")) Then
                If dr("categoria") <> "" Then
                    Dim ricat As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("ricat"), System.Web.UI.HtmlControls.HtmlGenericControl)
                    ricat.Visible = True
                    lblcat.Text = dr("categoria")
                    If Not IsDBNull(dr("ponte")) Then
                        If dr("ponte") <> "" Then
                            lblponte.Visible = True
                            If dr("ponte").ToString.IndexOf("assigned") >= 0 Then
                                lblponte.Text = "in attesa di assegnazione"
                            Else
                                lblponte.Text = dr("ponte")
                            End If
                            lponte.Visible = True
                        End If
                    End If
                End If
            End If
            If Not IsDBNull(dr("cabina")) Then
                If dr("cabina") <> "" Then
                    Dim tipocabina As String = ""
                    If dr("tipocabina") = 0 Then
                        tipocabina = "Esterna con balcone"
                    ElseIf dr("tipocabina") = 1 Then
                        tipocabina = "Esterna con finestra"
                    ElseIf dr("tipocabina") = 2 Then
                        tipocabina = "Interna"
                    ElseIf dr("tipocabina") = 3 Then
                        tipocabina = "Esterna con vista ostruita"
                    ElseIf dr("tipocabina") = 4 Then
                        tipocabina = "Suite con balcone"
                    ElseIf dr("tipocabina") = 5 Then
                        tipocabina = "Aurea Suite"
                    ElseIf dr("tipocabina") = 7 Then
                        tipocabina = "offerta SUPER BINGO"
                    ElseIf dr("tipocabina") = 8 Then
                        tipocabina = "offerta BINGO ESTERNA"
                    ElseIf dr("tipocabina") = 9 Then
                        tipocabina = "offerta BINGO BALCONE"
                    ElseIf dr("tipocabina") = 15 Then
                        tipocabina = "offerta BINGO INTERNA"
                    ElseIf dr("tipocabina") = 16 Then
                        tipocabina = "offerta BINGO FAMILY"
                    ElseIf dr("tipocabina") = 10 Then
                        tipocabina = "Deluxe Suite"
                    ElseIf dr("tipocabina") = 11 Then
                        tipocabina = "Executive & Family Suite"
                    ElseIf dr("tipocabina") = 12 Then
                        tipocabina = "Royal Suite"
                    End If
                    If Not IsDBNull(dr("cabina")) Then
                        If dr("cabina") <> "" Then
                            Dim appcab As String = dr("cabina")
                            If dr("cabina") = "G00000" Or dr("cabina").ToString.ToUpper = "GARANTITA" Then
                                appcab = "Garantita"
                            End If
                            lblcabina.Text = appcab & " - " & tipocabina
                            If dr("cabina") = "G00000" Or dr("cabina").ToString.ToUpper = "GARANTITA" Then
                                licab.Style.Add("height", "35px")
                                Dim apptcab As String = "Cabina garantita: durante la crociera potrebbe essere richiesto un cambio cabina (succede raramente)."
                                If dr("compagnia") = 0 Then
                                    If dr("adulti") + dr("bambini") >= 4 Then
                                        licab.Style.Add("height", "50px")
                                        apptcab = apptcab & "<br/>Per le cabine quadruple garantite MSC si riserva il diritto di assegnare anche 2 cabine distinte non obbligatoriamente vicine"
                                    End If
                                End If
                                Label9.Text = Label9.Text & "<br /><span style='font-size:xx-small'>" & apptcab & "</span>"

                            End If
                        Else
                            lblcabina.Text = tipocabina
                        End If
                    Else
                        lblcabina.Text = tipocabina
                    End If
                Else
                    Dim tipocabina As String = ""
                    If dr("tipocabina") = 7 Then
                        tipocabina = "offerta SUPER BINGO - può essere cabina interna, esterna o balcone e sarà assegnata 5 giorni prima della partenza"
                    ElseIf dr("tipocabina") = 8 Then
                        tipocabina = "offerta BINGO - scoprirai il numero della cabina esterna 5 giorni prima della partenza"
                    ElseIf dr("tipocabina") = 9 Then
                        tipocabina = "offerta BINGO - scoprirai il numero della cabina con balcone 5 giorni prima della partenza"
                    End If
                    lblcabina.Text = tipocabina
                End If
            End If
        End If
            dr.Close()
            cn.Close()
    End Sub

    Function ricavaperiodo2(ByVal idnave As Integer, ByVal dal As Date) As Integer
        ricavaperiodo2 = 0
        Dim sqlconn As String
        sqlconn = "SELECT id_periodo FROM periodo WHERE periodo.id_nave = " & idnave & " AND periodo.dal = '" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavaperiodo2 = dr("id_periodo")
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function ricavanave(ByVal idnave As Integer) As String
        ricavanave = ""
        Dim sqlconn As String
        sqlconn = "SELECT titolo FROM nave WHERE id_nave = " & idnave
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavanave = dr("titolo")
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function sipagamento(ByVal idpreno As String) As Boolean
        sipagamento = False
        Dim sqlconn As String
        sqlconn = "SELECT count(id_preno) as conta FROM pagamenti WHERE id_preno = '" & idpreno & "' and ricevuto = 1"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("conta") > 0 Then
                sipagamento = True
            End If
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function controllacampo(ByVal dr As MySqlDataReader, ByVal campo As Object) As Object
        If Not IsDBNull(dr(campo)) Then
            controllacampo = dr(campo)
        Else
            controllacampo = ""
        End If
    End Function

    Function UpCase(ByVal frase As String) As String
        Dim i As Integer
        Dim frase2() As String
        frase2 = Split(frase, " ")
        For i = 0 To UBound(frase2)
            frase2(i) = UCase(Mid(frase2(i), 1, 1)) & LCase(Mid(frase2(i), 2))
        Next
        UpCase = Join(frase2, " ")
    End Function

    Private Sub caricazona()
        Dim sqlconn As String
        sqlconn = "SELECT * from zona"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim conta As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                ReDim Preserve nomezona(conta)
                nomezona(conta) = dr("nomezona")
                conta = conta + 1
            Loop
        End If
        dr.Close()
        cn.Close()
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

    Private Sub compilapreventivo(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT  * from preventivo where id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterPreventivo.DataSource = dr
            RepeaterPreventivo.DataBind()
        End If
        dr.Close()
        cn2.Close()
    End Sub

    Protected Sub RepeaterPreventivo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPreventivo.ItemDataBound
        Dim lblquota As Label = CType(e.Item.FindControl("descrizione"), Label)
        Dim lblpax As Label = CType(e.Item.FindControl("persone"), Label)
        Dim lblprezzo As Label = CType(e.Item.FindControl("prezzo"), Label)
        Dim lbltotale As Label = CType(e.Item.FindControl("totale"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lblquota.Text = Left(lblquota.Text, 80)
            If lblprezzo.Text = 0 Then lblprezzo.Visible = False
            If lblpax.Text = 0 Then lblpax.Visible = False
            If lbltotale.Text = 0 Then lbltotale.Visible = False        
        End If
    End Sub

End Class
