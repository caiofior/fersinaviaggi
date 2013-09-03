Imports System.Net.Mail
Imports MySql.Data.MySqlClient
Partial Class crociere_riceviemail
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim psswmail As String = ConfigurationSettings.AppSettings("psswmail")
    Dim miamail As String = ConfigurationSettings.AppSettings("miamail")
    Dim miosmtp As String = ConfigurationSettings.AppSettings("miosmtp")
    Dim arraypax(5) As String
    Dim idperiodo As Integer = 0
    Dim totalepr As Integer = 0
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Private Sub vedi(ByVal codiceperiodo As String, ByVal email As String, ByVal nome As String, ByVal persone As Integer, ByVal adulti As Integer, ByVal bambini As Integer)
        Dim preno As String = ""
        Dim sp As String = "<br />"
        Dim htmlvero As Boolean = True

        Dim sqlconn As String
        sqlconn = "SELECT * FROM periodo, nave WHERE nave.id_nave = periodo.id_nave AND periodo.codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim compagnia As Integer = 0
        Dim iditi As Integer = 0
        Dim imbarco As String = ""
        Dim sbarco As String = ""
        Dim dal As Date
        Dim al As Date
        If dr.HasRows Then
            dr.Read()
            idperiodo = dr("id_periodo")
            compagnia = dr("compagnia")
            Dim tipocabina As String = ""
            If Request.Params("tipocabina") >= 1 And Request.Params("tipocabina") <= 2 Then
                tipocabina = "Esterna con balcone"
            ElseIf Request.Params("tipocabina") >= 3 And Request.Params("tipocabina") <= 4 Then
                tipocabina = "Esterna con finestra"
            ElseIf Request.Params("tipocabina") >= 5 And Request.Params("tipocabina") <= 6 Then
                tipocabina = "Interna"
            ElseIf Request.Params("tipocabina") >= 7 And Request.Params("tipocabina") <= 8 Then
                tipocabina = "Suite"
            End If
            iditi = dr("id_itinerario")
            preno = preno & "<table style='font-family:Arial, Times New Roman;' >"
            Dim paxt As String = persone & " persone - " & adulti & " adult"
            If adulti > 1 Then
                paxt = paxt & "i "
            Else
                paxt = paxt & "o "
            End If
            If bambini > 0 Then                
                paxt = paxt & " + " & bambini & " minor"
                If bambini > 1 Then
                    paxt = paxt & "i"
                Else
                    paxt = paxt & "e"
                End If
            End If
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Referente:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & nome & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>E-mail:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & email & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Passeggeri:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & paxt & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Nave:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & dr("titolo").ToString.ToUpper & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Imbarco:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & dr("imbarco") & " - " & giornoArray(CDate(dr("dal")).DayOfWeek) & " " & CDate(dr("dal")).Day & " " & meseArray(CDate(dr("dal")).Month - 1) & " " & CDate(dr("dal")).Year & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Sbarco:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & dr("sbarco") & " - " & giornoArray(CDate(dr("al")).DayOfWeek) & " " & CDate(dr("al")).Day & " " & meseArray(CDate(dr("al")).Month - 1) & " " & CDate(dr("al")).Year & "</td></tr>"
            preno = preno & "<tr style='height:30px'><td style='width:100px; color: #067788; font-weight:bold; border-bottom:1px solid #dcdcdc;'>Durata:</td><td style='width:400px; border-bottom:1px solid #dcdcdc;'>" & dr("durata") & " notti</td></tr>"
            preno = preno & "</table>"
            preno = preno & sp & sp
            dal = dr("dal")
            al = dr("al")
            imbarco = dr("imbarco")
            sbarco = dr("sbarco")
        End If
        dr.Close()
        cn.Close()
        preno = preno & sp
        preno = preno & "<span style='font-size:large'><b>PREVENTIVO:</b></span>"
        preno = preno & "<table style='font-family:Arial, Times New Roman;'>"
        preno = preno & salvapreventivo()
        preno = preno & "</table>"
        preno = preno & sp
        preno = preno & sp
        preno = preno & sp
        preno = preno & "<span style='font-size:large'><b>ITINERARIO:</b></span>"
        preno = preno & "<table style='font-family:Arial, Times New Roman;'>"
        preno = preno & ricavaiti(iditi, dal, al, imbarco, sbarco)
        preno = preno & "</table>"
        preno = preno & sp
        preno = preno & sp
        preno = preno & "Per riaccedere al nostro sito alla pagina di questo itinerario <a href='http://www.fersinaviaggi.it/crociere/offerte-crociere.aspx?id=" & idperiodo & "'>clicca qui</a>"
        preno = preno & sp
        preno = preno & sp
        preno = preno & "Trattasi di solo preventivo non vincolante e soggetto a verifica disponibilità in fase di riconferma. <b>Se si desidera bloccare il prezzo e opzionare la cabina preghiamo premere il tasto prosegui ed inviare la richiesta</b>. Tutte le rischieste pervenute non sono impegnative o vincolanti fino alla reale accettazione della pratica che si completa con il pagamento."
        preno = preno & sp
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
        SendMail(email, preno, htmlvero, compagnia)
    End Sub


    Function ricavaiti(ByVal iditi As Integer, ByVal datadal As Date, ByVal dataal As Date, ByVal portopartenza As String, ByVal portoarrivo As String) As String
        ricavaiti = ""
        Dim sqlconn As String
        sqlconn = "SELECT * FROM tipoitinerario WHERE id_itinerario = " & iditi & " ORDER BY giorno"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim ds As New dspreno
        Dim da As New MySqlDataAdapter(sqlconn, cn)
        da.Fill(ds, "itinerario")
        da.Fill(ds, "itinerario")
        Dim giorni As Integer = DateDiff(DateInterval.Day, datadal, dataal)
        Dim dts As DataTable = ds.Tables("itinerario")
        Dim dtn As DataTable = ds.Tables("giorni")
        Dim datap As Date = datadal
        Dim gg As Integer = 0
        Dim esegui As Boolean = False
        Dim ieri As Integer = -1
        For Each drs As DataRow In dts.Rows
            Dim drn As DataRow = dtn.NewRow
            Dim appart As String = drs("luogo").ToString.ToUpper
            If appart.IndexOf(Trim(portopartenza)) >= 0 Then
                esegui = True
            End If
            If gg <= giorni And esegui Then
                drn("id_itinerario") = drs("id_itinerario")
                drn("giorno") = datap
                drn("luogo") = drs("luogo")
                If gg = giorni Then
                    drn("partenza") = "00:00"
                    If portoarrivo <> portopartenza Then
                        drn("luogo") = portoarrivo
                    End If
                Else
                    drn("partenza") = drs("partenza")
                End If
                drn("arrivo") = drs("arrivo")
                dtn.Rows.Add(drn)
                If drs("giorno") <> ieri Then
                    gg = gg + 1
                Else
                    datap = DateAdd(DateInterval.Day, -1, datap)
                End If
                ieri = drs("giorno")
                datap = DateAdd(DateInterval.Day, 1, datap)
            End If
        Next
        Dim preno As String = ""
        preno = preno & "<tr style='height:30px'><td style='width:60px; border-bottom: 1px solid #dcdcdc'>Giorno</td><td style='width:100px;  border-bottom: 1px solid #dcdcdc; text-align:left;'></td><td style='width:200px;  border-bottom: 1px solid #dcdcdc; text-align:left;'>Porto</td><td style='width:70px;  text-align:right; border-bottom: 1px solid #dcdcdc'>Arrivo</td><td style='width:70px;  text-align:right; border-bottom: 1px solid #dcdcdc'>Partenza</td></tr>"
        For Each dr As DataRow In dtn.Rows
            Dim orariop As String = ""
            Dim orarioa As String = ""
            Dim ora As TimeSpan
            ora = dr("partenza")
            orariop = inseriscizero(ora.Hours) & ":" & inseriscizero(ora.Minutes)
            If ora.Hours = 0 And ora.Minutes = 0 And ora.Seconds = 0 Then
                orariop = "-------"
            End If
            ora = dr("arrivo")
            orarioa = inseriscizero(ora.Hours) & ":" & inseriscizero(ora.Minutes)
            If ora.Hours = 0 And ora.Minutes = 0 And ora.Seconds = 0 Then
                orarioa = "-------"
            End If
            preno = preno & "<tr style='height:30px'><td style='width:60px; border-bottom: 1px solid #dcdcdc'>" & Format(dr("giorno"), "dd/MM") & "</td><td style='width:100px;  border-bottom: 1px solid #dcdcdc; text-align:left;'>" & giornoArray(CDate(dr("giorno")).DayOfWeek) & "</td><td style='width:200px;  border-bottom: 1px solid #dcdcdc; text-align:left;'>" & dr("luogo") & "</td><td style='width:70px;  text-align:right; border-bottom: 1px solid #dcdcdc'>" & orarioa & "</td><td style='width:70px;  text-align:right; border-bottom: 1px solid #dcdcdc'>" & orariop & "</td></tr>"
        Next
        cn.Close()
        ricavaiti = preno

    End Function


    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Function salvapreventivo() As String
        Dim stringa() As String
        stringa = Split(eccopreventivo.Value.ToString, "$")
        Dim word As String
        Dim frase As String
        Dim pax As String = ""
        Dim rimanente As String
        Dim importo As String = ""
        Dim totale As String = ""
        Dim pacc As String = ""
        Dim apppreventivo As String = ""
        Dim tot As Integer = 0
        For Each word In stringa
            If word.IndexOf("(") >= 0 Then
                frase = Left(word, word.IndexOf("("))
                rimanente = Right(word, word.Length - word.IndexOf("(") - 1)
                pax = Left(rimanente, rimanente.IndexOf(")"))
                rimanente = Right(rimanente, rimanente.Length - rimanente.IndexOf("(") - 1)
                importo = Left(rimanente, rimanente.IndexOf(")"))
                rimanente = Right(rimanente, rimanente.Length - rimanente.IndexOf("(") - 1)
                totale = Left(rimanente, rimanente.IndexOf(")"))
                rimanente = Right(rimanente, rimanente.Length - rimanente.IndexOf("(") - 1)
                pacc = Left(rimanente, rimanente.IndexOf(")"))
                apppreventivo = apppreventivo & addpreventivo(frase, CInt(pax), CInt(importo), CInt(totale), CInt(pacc))
                tot = tot + CInt(totale)
                If CInt(pacc) = 0 Then
                    totalepr = totalepr + CInt(totale)
                End If
            End If
        Next
        apppreventivo = apppreventivo & "<tr style='height:30px'><td style='width:500px; border-bottom: 1px solid #dcdcdc; font-size: large;' >TOTALE:</td><td style='width:50px;  border-bottom: 1px solid #dcdcdc; text-align:right;'></td><td style='width:80px;  border-bottom: 1px solid #dcdcdc; text-align:right;'></td><td style='width:120px; text-align:right;  border-bottom: 1px solid #dcdcdc; font-size: large;'>" & Format(tot, "€ ##,##0.00") & "</td></tr>"
        salvapreventivo = apppreventivo
    End Function

    Function addpreventivo(ByVal frase As String, ByVal pax As Integer, ByVal importo As Integer, ByVal totale As Integer, ByVal pacc As Integer) As String
        addpreventivo = ""
        Dim preno As String = ""
        Dim persone As String = ""
        Dim importot As String = ""
        Dim totalet As String = ""
        If pax > 0 Then
            persone = pax
        End If
        If importo > 0 Then
            importot = Format(importo, "€ ##,##0.00")
        End If
        If totale > 0 Then
            totalet = Format(totale, "€ ##,##0.00")
        End If

        preno = preno & "<tr style='height:30px'><td style='width:500px; border-bottom: 1px solid #dcdcdc'>" & frase & "</td><td style='width:50px;  border-bottom: 1px solid #dcdcdc; text-align:right;'>" & persone & "</td><td style='width:80px;  border-bottom: 1px solid #dcdcdc; text-align:right;'>" & importot & "</td><td style='width:120px;  text-align:right; border-bottom: 1px solid #dcdcdc'>" & totalet & "</td></tr>"

        addpreventivo = preno
    End Function

    Private Sub SendMail(ByVal maildest As String, ByVal preno As String, ByVal iishtml As Boolean, ByVal compagnia As Integer)
        Dim objMail As New MailMessage
        Dim strTemp As String = "Riceverà un e-mail di conferma al seguente indirizzo: "
        Dim Smtpmail As New SmtpClient(miosmtp)
        Dim da As New MailAddress(miamail)
        Smtpmail.Credentials = New System.Net.NetworkCredential(miamail, psswmail)
        objMail.From = da
        If compagnia = 0 Then
            objMail.Subject = "Richiesta preventivo MSC CROCIERE"
        ElseIf compagnia = 1 Then
            objMail.Subject = "Richiesta preventivo COSTA CROCIERE"
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
        objMail.Dispose()
    End Sub

    Protected Sub confmail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confmail.Click
        labelacc.visible = False
        If Checkboxprivacy.Checked = True Then            
            If Me.IsValid Then
                lblmail.Visible = False
                email.Visible = False
                cognome.Visible = False
                lblcognome.visible = False
                confmail.Visible = False
                telefono.Visible = False
                labeltel.visible = False
                avvmail.Visible = True
                ckconta.Checked = False
                Checkboxprivacy.Checked = False
                arraypax(1) = Request.Params("eta1")
                arraypax(2) = Request.Params("eta2")
                arraypax(3) = Request.Params("eta3")
                arraypax(4) = Request.Params("eta4")
                arraypax(5) = Request.Params("eta5")
                Dim adulti As Integer = 0
                Dim bambini As Integer = 0
                Dim i As Integer
                For i = 1 To Request.Params("persone")
                    If arraypax(i) < 18 Then
                        bambini = bambini + 1
                    Else
                        adulti = adulti + 1
                    End If
                Next
                Call vedi(Request.Params("codiceperiodo"), email.Text, cognome.Text, Request.Params("persone"), adulti, bambini)

                Dim stringa As String = "INSERT INTO contatto SET email = @email, cognome = @cognome, id_periodo = @id_periodo, adulti = @adulti, bambini = @bambini, prezzo = @prezzo, categoria = @categoria, chi = @chi, data = @data, volo = @volo, frasepreventivo = @frasepreventivo, siglavolo = @siglavolo, frasepac = @frasepac, tipocabina = @tipocabina, telefono = @telefono, contattotel = @contattotel"
                Dim cmd As New MySqlCommand(stringa, cn)
                cmd.Parameters.AddWithValue("@email", email.Text)
                cmd.Parameters.AddWithValue("@cognome", cognome.Text)
                cmd.Parameters.AddWithValue("@id_periodo", idperiodo)
                cmd.Parameters.AddWithValue("@adulti", adulti)
                cmd.Parameters.AddWithValue("@bambini", bambini)
                cmd.Parameters.AddWithValue("@prezzo", totalepr)
                cmd.Parameters.AddWithValue("@categoria", Request.Params("categoria"))
                If Request.ServerVariables("REMOTE_HOST") = "46.234.236.8" Then
                    cmd.Parameters.AddWithValue("@chi", "noi")
                Else
                    cmd.Parameters.AddWithValue("@chi", "")
                End If
                cmd.Parameters.AddWithValue("@data", Date.Now)
                cmd.Parameters.AddWithValue("@volo", ricavavolo(Request.Params("aeroporto")))
                cmd.Parameters.AddWithValue("@frasepreventivo", eccopreventivo.Value.ToString())
                cmd.Parameters.AddWithValue("@siglavolo", Request.Params("aeroporto"))
                cmd.Parameters.AddWithValue("@frasepac", Request.Params("frasepac"))
                cmd.Parameters.AddWithValue("@tipocabina", Request.Params("tipologiacabina"))
                cmd.Parameters.AddWithValue("@telefono", telefono.Text)
                If ckconta.Checked = 0 Then
                    cmd.Parameters.AddWithValue("@contattotel", 0)
                Else
                    cmd.Parameters.AddWithValue("@contattotel", 1)
                End If
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd.ExecuteNonQuery()
                cn.Close()
            End If
            Else
                avvmail.Visible = False
                Labelacc.Visible = True
            End If
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim altezza As Integer = 440
        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'framemail'); parti();", True)
    End Sub
End Class
