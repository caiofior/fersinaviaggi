Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Net.Mail

Partial Class crociere_dati
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim psswmail As String = ConfigurationSettings.AppSettings("psswmail")
    Dim miamail As String = ConfigurationSettings.AppSettings("miamail")
    Dim miosmtp As String = ConfigurationSettings.AppSettings("miosmtp")
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim contarighe As Integer = 0
    Dim arraypax(5) As String
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim prezzop As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        arraypax(1) = Request.Params("eta1")
        arraypax(2) = Request.Params("eta2")
        arraypax(3) = Request.Params("eta3")
        arraypax(4) = Request.Params("eta4")
        arraypax(5) = Request.Params("eta5")
        lblerror.Visible = False
        If Not Page.IsPostBack Then

            caricapax(Request.Params("persone"))
            Dim altezza As Integer = (contarighe * 41) + 1350
            Hyperassi.NavigateUrl = "CondizioniassicurazioneAMITRAVEL.pdf"
            If Request.Params("compagnia") = 0 Then
                funziona.Text = "<b>COME FUNZIONA ?</b> Compila tutti i campi sotto richiesti e premi il pulsante invia richiesta. La richiesta da te inoltrata sarà da noi elaborata e dovrai attendere la nostra riconferma con opzione e numero della cabina. Ricevuta la nostra conferma potrai decidere se pagare o meno tramite bonifico bancario o carta di credito entro il termine da noi indicato. I biglietti elettronici ti saranno spediti via mail a pagamento avvenuto prima della partenza."
                hypercondizioni.NavigateUrl = "condizionimsc.html"
                Hypercanc.NavigateUrl = "cancellazionimsc.html"

                If Right(Request.Params("categoria"), 1) = "1" Then
                    If Request.Params("categoria") <> "01" Then
                        DropTurno.Enabled = False
                        Labelturno.Text = "La tipologia Bella non prevede di selezionare il turno in ristorante.<br /> Se è fondamentale ai fini di prenotazione preghiamo selezionare la tipologia Fantastica"
                    End If
                End If
            ElseIf Request.Params("compagnia") = 1 Then
                funziona.Text = "<b>COME FUNZIONA ?</b> Compila tutti i campi sotto richiesti e premi il pulsante prenota. In tempo reale visualizzerai la conferma e potrai decidere se confermare o meno la cabina entro il termine che sarà indicato. La conferma della cabina avviene tramite il pagamento tramite bonifico bancario o carta di credito entro i termini indicati nella tabella pagamenti. I biglietti elettronici ti saranno spediti via mail a pagamento avvenuto prima della partenza."                
                ButtonPrenota.ImageUrl = "../images/prenota.gif"
                If Request.Params("rhq") = 1 Then
                    ButtonPrenota.ImageUrl = "../images/invia-richiesta.gif"
                End If
                hypercondizioni.NavigateUrl = "condizionicosta.html"
                Hypercanc.NavigateUrl = "cancellazionicosta.html"
                If Right(Request.Params("categoria"), 1) = "C" Then
                    DropTurno.Enabled = False
                    Labelturno.Text = "La categoria Classic non prevede di selezionare il turno in ristorante.<br /> Se è fondamentale ai fini di prenotazione preghiamo selezionare la categoria Premium"
                End If
                If Right(Request.Params("categoria"), 1) = "V" Then
                    DropTurno.Enabled = False
                    Labelturno.Text = "La categoria Risparmiasubito non prevede di selezionare il turno in ristorante.<br /> Se è fondamentale ai fini di prenotazione preghiamo selezionare la categoria Premium"
                End If
                If Right(Request.Params("categoria"), 1) = "X" Then
                    DropTurno.Enabled = False
                    Labelturno.Text = "La categoria Mordi e Fuggi non prevede di selezionare il turno in ristorante.<br /> Se è fondamentale ai fini di prenotazione preghiamo selezionare la categoria Premium"
                End If
               
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & "altezza('" & altezza & "', 'framedati');parti();", True)
        End If
    End Sub

    Private Sub caricapax(ByVal persone As Integer)
        Dim ds As New dspreno
        Dim dt As DataTable = ds.Tables("anno")
        Dim i As Integer = 0
        For i = 1 To CInt(persone)
            Dim dr2 As DataRow = dt.NewRow
            dr2("anno") = "1"
            dt.Rows.Add(dr2)
        Next
        RepeaterPax.DataSource = ds.Tables("anno")
        RepeaterPax.DataBind()
        mettidrop("DropGiorno", 1, 31)
        mettidrop("DropMese", 1, 12)
        mettidrop("DropAnno", Date.Now.Year, Date.Now.Year - 90)
        If Request.Params("compagnia") = 1 Then
            If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                metticostaclub()
            End If
        End If
    End Sub
    Private Sub mettidrop(ByVal nomedrop As String, ByVal min As Integer, ByVal max As Integer)
        Dim oItem As RepeaterItem
        Dim i As Integer
        For Each oItem In RepeaterPax.Items
            Dim ndrop As DropDownList = CType(oItem.FindControl(nomedrop), DropDownList)
            ndrop.Items.Add("--")
            If min > max Then
                For i = min To max Step -1
                    ndrop.Items.Add(i)
                Next
            Else
                For i = min To max
                    ndrop.Items.Add(i)
                Next
            End If
        Next oItem
    End Sub

    Private Sub metticostaclub()
        Dim oItem As RepeaterItem
        Dim i As Integer = 0
        For Each oItem In RepeaterPax.Items
            Dim cognomeparte As TextBox = CType(oItem.FindControl("cognomeparte"), TextBox)
            Dim labelclub As Label = CType(oItem.FindControl("labelclub"), Label)
            Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(oItem.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
            If i = 0 Then
                cognomeparte.Text = Request.Params("nomeclub")
                cognomeparte.Enabled = False
                labelclub.Visible = True
                riga.Style.Add("height", "45px")
            End If
            i = i + 1
        Next oItem
    End Sub

    Protected Sub RepeaterPax_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPax.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            contarighe = contarighe + 1
        End If
    End Sub

    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Function controllanascita() As Boolean
        Dim sqlconn As String
        Dim dal As Date
        sqlconn = "SELECT * FROM prezzi, periodo where  prezzi.codiceperiodo = '" & Request.Params("codiceperiodo") & "' AND prezzi.categoria = '" & Request.Params("categoria") & "' AND periodo.codiceperiodo = prezzi.codiceperiodo"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        Dim procedi As Boolean = False
        If dr.HasRows Then
            dr.Read()
            dal = dr("dal")
        End If
        dr.Close()
        cn.Close()
        controllanascita = True
        Dim oItem As RepeaterItem
        Dim arrayvedi(5) As Integer
        Dim persone As Integer = CInt(Request.Params("persone"))
        Dim i As Integer
        Dim apparraypax(5) As Integer
        For i = 1 To persone
            arrayvedi(i) = 0
            'Label1.Text = Label1.Text & " " & arraypax(i)
            apparraypax(i) = arraypax(i)
        Next
        Dim x As Integer = 1
        For Each oItem In RepeaterPax.Items
            Dim dropanno As DropDownList = CType(oItem.FindControl("DropAnno"), DropDownList)
            Dim dropgiorno As DropDownList = CType(oItem.FindControl("DropGiorno"), DropDownList)
            Dim dropmese As DropDownList = CType(oItem.FindControl("DropMese"), DropDownList)
            Dim Label25 As Label = CType(oItem.FindControl("Label25"), Label)
            Dim datap As Date = CDate("01-01-2015")
            Try
                datap = CDate(inseriscizero(dropgiorno.SelectedValue) & "-" & inseriscizero(dropmese.SelectedValue) & "-" & dropanno.SelectedValue)
            Catch ex As Exception
                controllanascita = False
            End Try
            Dim anni As Integer = Math.Truncate(DateDiff(DateInterval.Day, datap, dal) / 365)
            Dim etaesatta As Integer = (Math.Truncate((DateDiff(DateInterval.Day, datap, dal) - (CInt(anni / 4))) / 365))
            Dim app As Integer = apparraypax(x)
            For i = 1 To persone
                'Label25.Text = etaesatta
                If app > 0 Then
                    If etaesatta <= 17 Then
                        If apparraypax(i) <= 17 And apparraypax(i) >= 0 Then
                            arrayvedi(x) = 1
                            apparraypax(i) = -1
                            app = apparraypax(i)
                        End If
                    ElseIf etaesatta <= 34 And etaesatta >= 18 Then
                        If apparraypax(i) <= 34 And apparraypax(i) >= 18 Then
                            arrayvedi(x) = 1
                            apparraypax(i) = -1
                            app = apparraypax(i)
                        End If
                    ElseIf etaesatta >= 35 And etaesatta <= 64 Then
                        If apparraypax(i) >= 35 And apparraypax(i) <= 64 Then
                            arrayvedi(x) = 1
                            apparraypax(i) = -1
                            app = apparraypax(i)
                        End If
                    ElseIf etaesatta >= 65 Then
                        If apparraypax(i) >= 65 Then
                            arrayvedi(x) = 1
                            apparraypax(i) = -1
                            app = apparraypax(i)
                        End If
                    End If
                    'Label1.Text = Label1.Text & " " & etaesatta & "=" & arraypax(i) & "->" & arrayvedi(x) & "app=" & app
                End If

                'If (arraypax(i) = etaesatta) Then 'Or (arraypax(i) = Date.Now.Year - dropanno.SelectedValue) Then
                '    arrayvedi(x) = 1
                'End If
                'Label1.Text = Label1.Text & "<br/>" & arraypax(i) & " = " & Math.Truncate((DateDiff(DateInterval.Day, datap, Date.Now) - (Math.Truncate(anni / 4))) / 365) & " " & arrayvedi(i)
                ' Label1.Text = Label1.Text & " / " & arrayvedi(x) & " " & arraypax(i) & "-" & (Math.Truncate((DateDiff(DateInterval.Day, datap, Date.Now) - (CInt(anni / 4))) / 365))
            Next
            x = x + 1
        Next oItem
        ' Label1.Text = "</span>"
        For i = 1 To persone
            If arrayvedi(i) = 0 Then
                controllanascita = False
                LabelCampi.Text = LabelCampi.Text & "la data di nascita del " & i & " passeggero non corrisponde all'età inserita in precedenza <br/>"
            End If
        Next
    End Function

    Protected Sub ButtonPrenota_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ButtonPrenota.Click
        LabelCampi.Text = ""
        If Me.IsValid = True Then
            If Checkboxgenerali.Checked = True And Checkboxprivacy.Checked = True Then
                If Request.Params("compagnia") = 0 Then
                    'Call salva()
                    'If controllanascita() = True Then
                    Call salva()
                    LabelCampi.Text = ""
                    'End If
                ElseIf Request.Params("compagnia") = 1 Then
                    If controllanascita() = True Then
                        Call salva()
                        LabelCampi.Text = ""
                    End If
                End If
            Else
                LabelCampi.Text = "Per proseguire devi accettare le norme sulla privacy e sottoscrivere le condizioni generali e di cancellazione"
            End If
        Else
            LabelCampi.Text = "Compila tutti i campi evidenziati in rosso altrimenti la richiesta non può essere inoltrata!<br />"
        End If
    End Sub

    Private Sub salva()

        Dim sqlconn As String
        Dim imbarco As String = ""
        Dim sbarco As String = ""
        Dim dal As Date
        Dim al As Date
        Dim durata As String = ""
        Dim dur As Integer = 0
        Dim id_periodo As Integer = 0
        sqlconn = "SELECT * FROM prezzi, periodo where  prezzi.codiceperiodo = '" & Request.Params("codiceperiodo") & "' AND prezzi.categoria = '" & Request.Params("categoria") & "' AND periodo.codiceperiodo = prezzi.codiceperiodo"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        Dim procedi As Boolean = False
        If dr.HasRows Then
            dr.Read()
            dal = dr("dal")
            al = dr("al")
            imbarco = dr("imbarco")
            sbarco = dr("sbarco")
            dur = dr("durata")
            durata = dr("durata") & " notti"
            id_periodo = dr("id_periodo")
            id_nave.Text = dr("id_nave")
            procedi = True
        End If
        dr.Close()
        Dim adulti As Integer = 0
        Dim bimbi As Integer = 0
        Dim i As Integer
        For i = 1 To Request.Params("persone")
            If arraypax(i) < 18 Then
                bimbi = bimbi + 1
            Else
                adulti = adulti + 1
            End If
        Next
        Dim tipocabina As Integer = 2
        If procedi = True Then
            If Request.Params("tipologiacabina") >= 1 And Request.Params("tipologiacabina") <= 2 Then tipocabina = 2
            If Request.Params("tipologiacabina") >= 3 And Request.Params("tipologiacabina") <= 4 Then tipocabina = 1
            If Request.Params("tipologiacabina") >= 5 And Request.Params("tipologiacabina") <= 6 Then tipocabina = 0
            If Request.Params("tipologiacabina") >= 7 And Request.Params("tipologiacabina") <= 8 Then tipocabina = 4
            Dim stringa As String = "INSERT INTO preno SET tessera=@tessera, tipotessera = @tipotessera, id_periodo = @id_periodo, adulti = @adulti, bambini = @bambini, nome = @nome, indirizzo = @indirizzo, citta = @citta, provincia = @provincia, cap = @cap, telefono = @telefono, email = @email, fiscale = @fiscale, data_preno = @data_preno, iputente = @iputente , comunicazioni = @comunicazioni, prezzo = @prezzo, imbarco = @imbarco, sbarco = @sbarco, durata = @durata , dal = @dal, al = @al, id_nave = @id_nave, tipocabina = @tipocabina, turno = @turno, categoria = @categoria, cabina = @cabina, ponte = @ponte, voloda = @voloda, voloa = @voloa, frasepac = @frasepac, aeroporto = @aeroporto, volopreno = @volopreno, rinunciaassi = @rinunciaassi, passp = @passp"
            If id_periodo = 4222 Then
                stringa = "INSERT INTO preno SET tessera=@tessera, tipotessera = @tipotessera, id_periodo = @id_periodo, adulti = @adulti, bambini = @bambini, nome = @nome, indirizzo = @indirizzo, citta = @citta, provincia = @provincia, cap = @cap, telefono = @telefono, email = @email, fiscale = @fiscale, data_preno = @data_preno, iputente = @iputente , comunicazioni = @comunicazioni, prezzo = @prezzo, imbarco = @imbarco, sbarco = @sbarco, durata = @durata , dal = @dal, al = @al, id_nave = @id_nave, tipocabina = @tipocabina, turno = @turno, categoria = @categoria, cabina = @cabina, ponte = @ponte, voloda = @voloda, voloa = @voloa, frasepac = @frasepac, aeroporto = @aeroporto, volopreno = @volopreno, rinunciaassi = @rinunciaassi, passp = @passp, nopzione = @nopzione, opzione = @opzione, bonifico = @bonifico"
            End If

            Dim cmd As New MySqlCommand(stringa, cn)
            cmd.Parameters.AddWithValue("@tessera", Request.Params("cartaclub"))
            cmd.Parameters.AddWithValue("@tipotessera", Request.Params("nomeclub"))
            cmd.Parameters.AddWithValue("@id_periodo", id_periodo)
            cmd.Parameters.AddWithValue("@adulti", adulti)
            cmd.Parameters.AddWithValue("@bambini", bimbi)
            cmd.Parameters.AddWithValue("@nome", nomeecognome.Text)
            cmd.Parameters.AddWithValue("@indirizzo", indirizzo.Text)
            cmd.Parameters.AddWithValue("@citta", citta.Text)
            cmd.Parameters.AddWithValue("@provincia", "")
            cmd.Parameters.AddWithValue("@cap", cap.Text)
            cmd.Parameters.AddWithValue("@telefono", telefono.Text)
            cmd.Parameters.AddWithValue("@email", email.Text)
            cmd.Parameters.AddWithValue("@fiscale", codfiscale.Text)
            cmd.Parameters.AddWithValue("@data_preno", Date.Now)
            cmd.Parameters.AddWithValue("@iputente", Request.ServerVariables("REMOTE_HOST"))
            If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                If Request.Params("compagnia") = 0 Then
                    TextCmc.Text = TextCmc.Text & " - Msc club " & Request.Params("cartaclub") & " " & Request.Params("nomeclub")
                ElseIf Request.Params("compagnia") = 1 Then
                    TextCmc.Text = TextCmc.Text & " - Costaclub " & Request.Params("cartaclub") & " " & Request.Params("nomeclub")
                End If
            End If
            cmd.Parameters.AddWithValue("@comunicazioni", TextCmc.Text)
            cmd.Parameters.AddWithValue("@prezzo", Request.Params("prezzo"))
            cmd.Parameters.AddWithValue("@imbarco", imbarco & " - " & giornoArray(dal.DayOfWeek) & " " & dal.Day & " " & meseArray(dal.Month - 1) & " " & dal.Year)
            cmd.Parameters.AddWithValue("@sbarco", sbarco & " - " & giornoArray(al.DayOfWeek) & " " & al.Day & " " & meseArray(al.Month - 1) & " " & al.Year)
            cmd.Parameters.AddWithValue("@durata", durata)
            cmd.Parameters.AddWithValue("@dal", dal)
            cmd.Parameters.AddWithValue("@al", al)
            cmd.Parameters.AddWithValue("@id_nave", id_nave.Text)
            cmd.Parameters.AddWithValue("@tipocabina", tipocabina)
            'If Not IsDBNull(segnaturno) Then
            '    If segnaturno.Value <> "" Then
            '        turno = segnaturno.Value
            '    End If
            'End If
            cmd.Parameters.AddWithValue("@turno", DropTurno.SelectedValue)
            cmd.Parameters.AddWithValue("@categoria", Request.Params("categoria"))
            If id_periodo = 4222 Then
                cmd.Parameters.AddWithValue("@cabina", "G0000")
            Else
                cmd.Parameters.AddWithValue("@cabina", Trim(Request.Params("cabina")))
            End If

            cmd.Parameters.AddWithValue("@ponte", Trim(Request.Params("nomeponte")))
            Dim voloda As String = ricavavolo(Trim(Request.Params("aeroporto")))
            Dim voloa As String = ricavavolo(Trim(Request.Params("aeroporto")))
            If Request.Params("direzione") = 2 Then
                voloa = ""
            End If
            If Request.Params("direzione") = 3 Then
                voloda = ""
            End If
            cmd.Parameters.AddWithValue("@voloda", voloda)
            cmd.Parameters.AddWithValue("@voloa", voloa)
            cmd.Parameters.AddWithValue("@frasepac", Request.Params("frasepac"))
            cmd.Parameters.AddWithValue("@aeroporto", Request.Params("aeroporto"))
            cmd.Parameters.AddWithValue("@volopreno", Request.Params("volo"))
            Dim assi As Integer = 0
            If IsNumeric(Request.Params("assi")) Then
                If Request.Params("assi") = 1 Then
                    assi = 1
                End If
            End If
            cmd.Parameters.AddWithValue("@rinunciaassi", assi)
            Dim passaporto As Integer = 0
            If IsNumeric(Request.Params("passaporto")) Then passaporto = Request.Params("passaporto")
            cmd.Parameters.AddWithValue("@passp", passaporto)
            If id_periodo = 4222 Then
                cmd.Parameters.AddWithValue("@nopzione", 888888)
                Dim dataopz As Date = DateAdd(DateInterval.Day, 1, Date.Now)
                Dim dataopz2 As Date = CDate(inseriscizero(dataopz.Day) & "-" & inseriscizero(dataopz.Month) & "-" & dataopz.Year & " 11:00")
                cmd.Parameters.AddWithValue("@opzione", dataopz2)
                cmd.Parameters.AddWithValue("@bonifico", 1)
            End If
            Dim codice As String
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd.ExecuteNonQuery()
            Dim memid As Integer
            cmd.CommandText = "SELECT LAST_INSERT_ID()"
            memid = (cmd.ExecuteScalar())
            cmd.Connection.Close()
            codice = converti(memid)
            Dim stringacodice As String = "UPDATE preno SET codice=@codice WHERE id_preno = '" & memid & "'"
            Dim cmd3 As New MySqlCommand(stringacodice, cn)
            cmd3.Parameters.AddWithValue("@codice", codice)
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd3.ExecuteNonQuery()
            cmd3.Connection.Close()
            cn.Close()
            Call ricava_pax(memid)
            Dim offerta As Boolean = False
            If Right(Request.Params("categoria"), 1) = "V" Or Right(Request.Params("categoria"), 1) = "X" Then
                offerta = True
            End If
            Call salvapreventivo(memid)
            Call anota("prezzo richiesto:" & Request.Params("prezzo") & Chr(13) & "categoria richiesta:" & Request.Params("categoria") & Chr(13) & "dal:" & dal & Chr(13) & "al:" & al & Chr(13) & "Nave:" & caricanave(CInt(id_nave.Text)) & Chr(13) & "Adulti:" & adulti & Chr(13) & "Bambini:" & bimbi, memid)
            'Call ricavapagamenti(memid)
            If Not IsDBNull(Request.Params("frasepac")) Then
                If Request.Params("frasepac") <> "" Then
                    Call caricapac(Request.Params("frasepac"), memid, dur, adulti, bimbi)
                End If
            End If
            Call riempipaga(dal, (CInt(Request.Params("persone"))), Request.Params("prezzo"), prezzop, offerta, memid, id_periodo)
            Dim lnk As String
            If Request.Params("compagnia") = 0 Then
                lnk = "conferma-crociere.aspx?maila=1&codice=" & codice & "&email=" & email.Text
                ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
            ElseIf Request.Params("compagnia") = 1 Then
                If Request.Params("prezzo") = 0 Then
                    lnk = "conferma-crociere.aspx?maila=1&codice=" & codice & "&email=" & email.Text
                    ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
                Else
                    ClientScript.RegisterClientScriptBlock(Me.GetType(), "script", "prendicabina('divassegna', 'assegna.aspx', '" & memid & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "', '" & Request.Params("tipologiacabina") & "', '" & Request.Params("tipocabina") & "')", True)
                End If
            End If
        Else
            lblerror.Visible = True
        End If
    End Sub

    Function caricanave(ByVal idnave As Integer) As String
        Dim sqlconn As String
        caricanave = ""
        sqlconn = "SELECT * from nave where id_nave = " & idnave
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            caricanave = dr("titolo")
        End If
        dr.Close()
        cn.Close()
    End Function

    Private Sub anota(ByVal testo As String, ByVal idpreno As Integer)
        Dim stringa As String
        stringa = "INSERT INTO nota SET id_preno = @id_preno, datanota = @datanota, testonota = @testonota, chinota = @chinota, evinota = @evinota"
        Dim cmd2 As New MySqlCommand(stringa, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd2.Parameters.AddWithValue("@id_preno", idpreno)
        cmd2.Parameters.AddWithValue("@datanota", Date.Now)
        cmd2.Parameters.AddWithValue("@testonota", testo)
        cmd2.Parameters.AddWithValue("@chinota", "Sistema")
        cmd2.Parameters.AddWithValue("@evinota", 3)
        cmd2.ExecuteNonQuery()
        cmd2.Parameters.Clear()
        cn.Close()
    End Sub


    Private Sub salvapreventivo(ByVal idpreno As Integer)
        Dim stringa() As String
        stringa = Split(eccopreventivo.Value.ToString, "$")
        Dim word As String
        Dim frase As String
        Dim pax As String = ""
        Dim rimanente As String
        Dim importo As String = ""
        Dim totale As String = ""
        Dim pacc As String = ""
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
                addpreventivo(idpreno, frase, CInt(pax), CInt(importo), CInt(totale), CInt(pacc))
            End If
        Next
    End Sub

    Private Sub addpreventivo(ByVal idpreno As Integer, ByVal descri As String, ByVal pax As Integer, ByVal importo As Integer, ByVal totale As Integer, ByVal pacc As Integer)
        Dim stringa As String = "INSERT INTO preventivo SET id_preno = @id_preno, descrizione = @descrizione, pax = @pax, importo = @importo, totale = @totale, pacc = @pacc"
        Dim cmd As New MySqlCommand(stringa, cn)
        cmd.Parameters.AddWithValue("@id_preno", idpreno)
        cmd.Parameters.AddWithValue("@descrizione", descri)
        cmd.Parameters.AddWithValue("@pax", pax)
        cmd.Parameters.AddWithValue("@importo", importo)
        cmd.Parameters.AddWithValue("@totale", totale)
        cmd.Parameters.AddWithValue("@pacc", pacc)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub



    Private Sub ricava_pax(ByVal memid As Long)
        Dim stringa2 As String = "INSERT INTO nomi SET id_preno = @id_preno, nomecognome = @nomecognome, datanascita = @datanascita, nomep = @nomep, cognomep = @cognomep, tipopax = @tipopax "
        Dim cmd2 As New MySqlCommand(stringa2, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim oItem As RepeaterItem
        For Each oItem In RepeaterPax.Items
            Dim nome As TextBox = CType(oItem.FindControl("nomeparte"), TextBox)
            Dim cognome As TextBox = CType(oItem.FindControl("cognomeparte"), TextBox)
            Dim dropgiorno As DropDownList = CType(oItem.FindControl("DropGiorno"), DropDownList)
            Dim dropmese As DropDownList = CType(oItem.FindControl("DropMese"), DropDownList)
            Dim dropanno As DropDownList = CType(oItem.FindControl("DropAnno"), DropDownList)
            Dim droptipo As DropDownList = CType(oItem.FindControl("DropTipo"), DropDownList)
            cmd2.Parameters.AddWithValue("@id_preno", memid)
            cmd2.Parameters.AddWithValue("@nomecognome", nome.Text & " " & cognome.Text)
            Dim datap As Date = CDate("01-01-2015")
            Try
                datap = CDate(inseriscizero(dropgiorno.SelectedValue) & "-" & inseriscizero(dropmese.SelectedValue) & "-" & dropanno.SelectedValue)
            Catch ex As Exception

            End Try
            cmd2.Parameters.AddWithValue("@datanascita", datap)
            cmd2.Parameters.AddWithValue("@nomep", nome.Text)
            cmd2.Parameters.AddWithValue("@cognomep", cognome.Text)
            cmd2.Parameters.AddWithValue("@tipopax", droptipo.SelectedValue)
            cmd2.ExecuteNonQuery()
            cmd2.Parameters.Clear()
        Next oItem
        cmd2.Connection.Close()
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


    Private Sub caricapac(ByVal frasepac2 As String, ByVal id_preno As Integer, ByVal durata As Integer, ByVal adulti As Integer, ByVal bambini As Integer)
        Dim stringa() As String
        stringa = Split(frasepac2, ";")
        Dim word As String
        Dim indice As String
        Dim pax As String
        For Each word In stringa
            If word.IndexOf("-") >= 0 Then
                indice = Left(word, word.IndexOf("-"))
                pax = Right(word, word.Length - word.IndexOf("-") - 1)
                inseriscipac(indice, pax, id_preno)
                Dim prezzopp As Integer = ricavaprezzopac(indice, pax, durata, adulti, bambini)
                prezzop = prezzop + prezzopp
            End If
        Next
        If prezzop > 0 Then
            Call aggiornaprezzopac(id_preno, prezzop)
        End If
    End Sub



    Private Sub aggiornaprezzopac(ByVal id_preno As Integer, ByVal prezzop As Integer)
        Dim stringa As String = "UPDATE preno SET prezzopac = @prezzopac  WHERE id_preno = " & id_preno
        Dim cmd As New MySqlCommand(stringa, cn)
        cmd.Parameters.AddWithValue("@prezzopac", prezzop)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub

    Private Sub inseriscipac(ByVal idpac As Integer, ByVal pax As Integer, ByVal id_preno As Integer)
        Dim stringa As String = "INSERT INTO prenopacchetti SET id_preno = @id_preno, id_pacchetto = @id_pacchetto, persone = @persone"
        Dim cmd As New MySqlCommand(stringa, cn)
        cmd.Parameters.AddWithValue("@id_preno", id_preno)
        cmd.Parameters.AddWithValue("@id_pacchetto", idpac)
        cmd.Parameters.AddWithValue("@persone", pax)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub


    Function ricavaprezzopac(ByVal idpac As Integer, ByVal pax As Integer, ByVal durata As Integer, ByVal adulti As Integer, ByVal bambini As Integer) As Integer
        Dim sqlconn As String
        ricavaprezzopac = 0
        sqlconn = "SELECT * FROM pacchetti where id_pacchetto = '" & idpac & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("algiorno") = 0 Then
                ricavaprezzopac = Int(dr("vendita") * pax)
            Else
                ricavaprezzopac = Int((dr("vendita") * adulti * durata) + (dr("venditabambini") * bambini * durata))
            End If
        End If
        dr.Close()
        cn.Close()
    End Function

    Private Sub salvapagamenti(ByVal idpreno As Integer, ByVal descripaga As String, ByVal scadenza As Date, ByVal prezzox As Integer)
        Dim stringa As String = "INSERT INTO pagamenti SET id_preno = @id_preno, descripaga = @descripaga, scadenza = @scadenza, prezzo = @prezzo"
        Dim cmd As New MySqlCommand(stringa, cn)
        cmd.Parameters.AddWithValue("@id_preno", idpreno)
        cmd.Parameters.AddWithValue("@descripaga", descripaga)
        cmd.Parameters.AddWithValue("@scadenza", scadenza)
        cmd.Parameters.AddWithValue("@prezzo", prezzox)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd.ExecuteNonQuery()
        cn.Close()
    End Sub

    Private Sub riempipaga(ByVal dal As Date, ByVal pax As Integer, ByVal importo As Integer, ByVal pacchetti As Integer, ByVal offerta As Boolean, ByVal idpreno As Integer, ByVal id_periodo As Integer)
        Dim ds As New dspreno
        Dim saldomeno As Integer = 0
        Dim datasaldo As Date = DateAdd(DateInterval.Day, -35, dal)
        ' Dim rigapp As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigapp"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If offerta = False Then
            If DateDiff(DateInterval.Day, Date.Now, dal) > 75 Then
                salvapagamenti(idpreno, "1° Acconto:", Date.Now, pax * 50)
                salvapagamenti(idpreno, "2° Acconto:", DateAdd(DateInterval.Day, -70, dal), CInt(importo / 100 * 40))
                saldomeno = (CInt(importo / 100 * 40) + pax * 50)
            ElseIf DateDiff(DateInterval.Day, Date.Now, dal) <= 75 And DateDiff(DateInterval.Day, Date.Now, dal) >= 35 Then
                salvapagamenti(idpreno, "Acconto:", Date.Now, CInt(importo / 100 * 40))
                saldomeno = (CInt(importo / 100 * 40))
            Else
                datasaldo = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                saldomeno = 0
            End If
        Else
            If DateDiff(DateInterval.Day, Date.Now, dal) >= 50 Then
                salvapagamenti(idpreno, "Acconto:", Date.Now, CInt(importo / 100 * 40))
                saldomeno = (CInt(importo / 100 * 40))
                datasaldo = DateAdd(DateInterval.Day, -48, dal)
            Else
                datasaldo = Date.Now.Day & "-" & Date.Now.Month & "-" & Date.Now.Year
                saldomeno = 0
            End If
        End If
        If id_periodo = 4222 Then
            Dim dataopz As Date = DateAdd(DateInterval.Day, 1, Date.Now)
            Dim dataopz2 As Date = CDate(inseriscizero(dataopz.Day) & "-" & inseriscizero(dataopz.Month) & "-" & dataopz.Year & " 11:00")
            salvapagamenti(idpreno, "Saldo:", dataopz2, importo - saldomeno)
        Else
            salvapagamenti(idpreno, "Saldo:", datasaldo, importo - saldomeno)
        End If

        If pacchetti > 0 Then
            salvapagamenti(idpreno, "Pacchetti:", datasaldo, pacchetti)
        End If
    End Sub

    Protected Sub caricacliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles caricacliente.Click
        If Me.IsValid = True Then
            If Tcodice.Text.Length > 5 Then
                Dim sqlconn As String
                sqlconn = "SELECT * FROM preno WHERE email = '" & Ttemail.Text & "' AND codice = '" & Tcodice.Text & "'"
                Dim cmd As New MySqlCommand(sqlconn, cn)
                If cn.State = ConnectionState.Closed Then cn.Open()
                Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
                Dim idpreno As Integer
                If dr.HasRows Then
                    dr.Read()
                    nomeecognome.Text = dr("nome")
                    indirizzo.Text = dr("indirizzo")
                    cap.Text = dr("cap")
                    citta.Text = dr("citta")
                    telefono.Text = dr("telefono")
                    email.Text = dr("email")
                    codfiscale.Text = dr("fiscale")
                    idpreno = dr("id_preno")
                End If
                dr.Close()
                sqlconn = "SELECT * FROM nomi WHERE id_preno = " & idpreno
                Dim datanascita(0) As Date
                Dim nomep(0) As String
                Dim cognomep(0) As String
                Dim tipopax(0) As String
                Dim cmd2 As New MySqlCommand(sqlconn, cn)
                If cn.State = ConnectionState.Closed Then cn.Open()
                Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
                Dim i As Integer = 0
                If dr2.HasRows Then
                    Do While dr2.Read
                        i = i + 1
                        ReDim Preserve datanascita(i)
                        ReDim Preserve nomep(i)
                        ReDim Preserve cognomep(i)
                        ReDim Preserve tipopax(i)
                        datanascita(i) = dr2("datanascita")
                        If Not IsDBNull(dr2("nomep")) Then
                            nomep(i) = dr2("nomep")
                        Else
                            nomep(i) = ""
                        End If
                        If Not IsDBNull(dr2("cognomep")) Then
                            cognomep(i) = dr2("cognomep")
                        Else
                            cognomep(i) = ""
                        End If
                        If dr2("tipopax") = 0 Then
                            tipopax(i) = "--"
                        Else
                            tipopax(i) = dr2("tipopax")
                        End If

                    Loop
                End If
                dr2.Close()
                cn.Close()
                Dim oItem As RepeaterItem
                Dim x As Integer = 1
                For Each oItem In RepeaterPax.Items
                    Dim dropgiorno As DropDownList = CType(oItem.FindControl("DropGiorno"), DropDownList)
                    Dim dropmese As DropDownList = CType(oItem.FindControl("DropMese"), DropDownList)
                    Dim dropanno As DropDownList = CType(oItem.FindControl("DropAnno"), DropDownList)
                    Dim droptipo As DropDownList = CType(oItem.FindControl("DropTipo"), DropDownList)
                    Dim nomeparte As TextBox = CType(oItem.FindControl("nomeparte"), TextBox)
                    Dim cognomeparte As TextBox = CType(oItem.FindControl("cognomeparte"), TextBox)
                    If x <= i Then
                        nomeparte.Text = nomep(x)
                        cognomeparte.Text = cognomep(x)
                        droptipo.SelectedValue = tipopax(x)
                        dropgiorno.SelectedValue = datanascita(x).Day
                        dropmese.SelectedValue = datanascita(x).Month
                        dropanno.SelectedValue = datanascita(x).Year
                    End If
                    x = x + 1
                Next
            End If
        End If
    End Sub

    Private Sub vedi(ByVal email As String, ByVal codice As String)
        Dim preno As String = ""
        Dim aste As String = "-------------------------------------------------------------------------"
        Dim aste2 As String = "************************************************************************"
        Dim sp As String = Chr(13)
        Dim htmlvero As Boolean = False
        If email.IndexOf("virgilio.it") > 0 Or email.IndexOf("alice.it") Or email.IndexOf("live.it") Then
            sp = "<br />"
            htmlvero = True
        End If
        preno = preno & "**** RICHIESTA CODICE ULTIMA PRENOTAZIONE****" & sp & sp
        preno = preno & sp
        preno = preno & "Abbiamo ricevuto una richiesta di autorizzazione ad inviarti il codice della tua ultima prenotazione effettuata con fersinaviaggi.it. Questo codice ti permette di inserire tutti i tuoi dati nel nostro sito con un semplice click." & sp
        preno = preno & sp & sp
        preno = preno & aste & sp
        preno = preno & "Codice: " & codice & sp
        preno = preno & aste & sp & sp        
        preno = preno & "Documento riservato: Questo messaggio è di carattere riservato ed è indirizzato esclusivamente ai destinatari specificati. L'accesso, la divulgazione, la copia o la diffusione sono vietate a chiunque altro ai sensi delle normative vigenti, e possono costituire una violazione penale. Nel caso abbiate ricevuto questo messaggio per errore siete tenuti a cancellarlo immediatamente confermando al mittente, a mezzo e-mail, l'avvenuta cancellazione. " & sp & "Confidentiality notice: This e-mail is confidential and is for the intended recipient only. Access, disclosure, copying, distribution or reliance on any of it by anyone else is prohibited and may be a criminal offence. Please delete if obtained in error and email confirmation to sender."
        SendMail(email, codice, preno, htmlvero)
        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('Ti abbiamo appena inoltrato il codice della tua ultima prenotazione effettuata con fersinaviaggi.it al seguente indirizzo email: " & Ttemail.Text & " ');", True)
    End Sub

    Private Sub SendMail(ByVal maildest As String, ByVal codice As String, ByVal preno As String, ByVal iishtml As Boolean)
        Dim objMail As New MailMessage
        Dim strTemp As String = "Riceverà un e-mail di conferma al seguente indirizzo: "
        Dim Smtpmail As New SmtpClient(miosmtp)
        Dim da As New MailAddress(miamail)
        Smtpmail.Credentials = New System.Net.NetworkCredential(miamail, psswmail)
        objMail.From = da
        objMail.Subject = "Richiesta codice fersinaviaggi.it"
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

    Protected Sub inviamailcliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles inviamailcliente.Click
        Dim sqlconn As String
        Dim codice As String = ""
        sqlconn = "SELECT * FROM preno where email = '" & Ttemail.Text & "' ORDER BY id_preno DESC"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            codice = dr("codice")
        End If
        dr.Close()
        cn.Close()
        If codice <> "" Then
            Call vedi(Ttemail.Text, codice)
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('La mail " & Ttemail.Text & " non è presente nei nostri database clienti. Procedi ad effettuare la registrazione manualmente compilando il form dati anagrafici e dati passeggeri!');", True)
        End If
    End Sub



End Class
