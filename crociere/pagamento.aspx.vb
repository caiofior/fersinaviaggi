Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_pagamento
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim cn3 As New MySqlConnection(cnstring)
    Dim key As String = ConfigurationSettings.AppSettings("MAC")
    Dim contarighe As Integer = 0
    Dim compagnia As Integer = 0
    Dim contanome As Integer = 0
    Dim primosca As String = ""
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim codice As String = ""
    Dim email As String = ""
    Private Sub riempipagamenti(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from pagamenti WHERE id_preno = " & idpreno & " order by scadenza"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterPaga.DataSource = dr
            RepeaterPaga.DataBind()
        End If
        dr.Close()
        cn2.Close()
    End Sub


    Protected Sub RepeaterPaga_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPaga.ItemDataBound
        Dim prezzo As Label = CType(e.Item.FindControl("prezzo"), Label)
        Dim scadenza As Label = CType(e.Item.FindControl("scadenza"), Label)
        Dim labelpaga As Label = CType(e.Item.FindControl("labelpaga"), Label)
        Dim labelpaga2 As Label = CType(e.Item.FindControl("labelpaga2"), Label)
        Dim idpaga As Label = CType(e.Item.FindControl("idpaga"), Label)
        Dim ricevuto As Label = CType(e.Item.FindControl("ricevuto"), Label)
        Dim labelricevuto As Label = CType(e.Item.FindControl("labelricevuto"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            contarighe = contarighe + 1
            Dim i As Integer
            Dim apppaga As String = ""
            For i = idpaga.Text.Length To 9
                apppaga = apppaga & "0"
            Next
            Dim compa As String = ""
            If compagnia = 0 Then compa = "Msc Crociere"
            If compagnia = 1 Then compa = "Costa Crociere"
            Dim nomeform As String = "frm" & contanome
            'labelpaga.Text = "<input type=""submit"" value=""" & Format(CInt(prezzo.Text), "€ ##,##") & " con carta di credito"" border=""0"" style=""margin-top:-7px;padding:0; height:30px; width:180px;"" name=""submit"" >"
            Dim sca As String = CDate(scadenza.Text).Year & "-" & inseriscizero(CDate(scadenza.Text).Month) & "-" & inseriscizero(CDate(scadenza.Text).Day) & "T" & inseriscizero(CDate(scadenza.Text).Hour) & ":" & inseriscizero(CDate(scadenza.Text).Minute) & ":00"
            If primosca = "" Then
                primosca = sca
            End If
            If almenounoricevuto(Request.Params("parsel")) = True Then
                sca = "2050-01-01T01:00"
            Else
                sca = primosca
            End If
            labelpaga.Text = generacarta(sca, nomeform, mailx.Text, compa, lblcodicepreno.Text, CInt(prezzo.Text), apppaga & idpaga.Text)
            'labelpaga.Attributes.Add("onclick", "javascript:controllapag('" & CDate(scadenza.Text).Year & "-" & inseriscizero(CDate(scadenza.Text).Month) & "-" & inseriscizero(CDate(scadenza.Text).Day) & "T" & inseriscizero(CDate(scadenza.Text).Hour) & ":" & inseriscizero(CDate(scadenza.Text).Minute) & ":00', '" & nomeform & "');")
            prezzo.Text = Format(CInt(prezzo.Text), "€ ##,##0.00")
            If CDate(scadenza.Text).Hour <> 0 Then
                scadenza.Text = Format(CDate(scadenza.Text), "dd/MM/yyyy") & "&nbsp;&nbsp;&nbsp;<b>Ore: </b>" & inseriscizero(CDate(scadenza.Text).Hour) & ":" & inseriscizero(CDate(scadenza.Text).Minute)
            Else
                scadenza.Text = Format(CDate(scadenza.Text), "dd/MM/yyyy")
            End If
            If ricevuto.Text = 1 Then
                labelpaga.Visible = False
                labelricevuto.Visible = True
            Else
                Labelatt.Visible = True
            End If
            contanome = contanome + 1
        End If
    End Sub

    Function generacarta(ByVal scad As String, ByVal nome As String, ByVal email As String, ByVal titolo As String, ByVal pratica As String, ByVal importo As Integer, ByVal idpaga As String) As String
        'Dim lnk As String = "http://localhost:51192/fersina-nuovissimo/crociere/"
        Dim lnk As String = "http://www.fersinaviaggi.it/crociere/"
        Dim urlnotifica As String = lnk & "pagamento.aspx?par=1"
        Dim buttontest As String = ""
        Dim orderid As String = ""
        Dim MyRnd As New Random
        'Label1.Text = dr2("acconto") & ",00"
        Dim urlko, urlok As String
        urlko = lnk & "conferma-crociere.aspx?codice=" & pratica & "&email=" & email
        urlok = lnk & "conferma-crociere.aspx?codice=" & pratica & "&email=" & email
        buttontest = buttontest & "<form id=""" & nome & """ action=""https://www.payment.fccrt.it/CheckOutEGIPSy.asp"" target=""_top"" method=""post"">"
        buttontest = buttontest + "<input type=""hidden"" name=""MERCHANT_ID"" value=""410667600004"">"
        buttontest = buttontest + "<input type=""hidden"" name=""DIVISA"" value=""EUR"">"
        buttontest = buttontest + "<input type=""hidden"" name=""ABI"" value=""03599"">"
        buttontest = buttontest + "<input type=""hidden"" name=""URLOK"" value=""" & urlok & """>"
        buttontest = buttontest + "<input type=""hidden"" name=""URLKO"" value=""" & urlko & """>"
        buttontest = buttontest + "<input type=""hidden"" name=""EMAIL"" value=""" & email & """>"
        buttontest = buttontest + "<input type=""hidden"" name=""URLACK"" value=""" & urlnotifica & """>"
        buttontest = buttontest + "<input type=""hidden"" name=""ITEMS"" value=""" & titolo & """>"
        If compagnia = 0 Then
            orderid = "MSC-" & pratica & "-" & idpaga & "-" & MyRnd.Next(9999999) '0 = addconto
        ElseIf compagnia = 1 Then
            orderid = "COS-" & pratica & "-" & idpaga & "-" & MyRnd.Next(9999999) '0 = addconto            
        End If
        buttontest = buttontest + "<input type=""hidden"" name=""ORDER_ID"" value=""" & orderid & """>"
        buttontest = buttontest + "<input type=""hidden"" name=""IMPORTO"" value=" & importo & ",00" & ">"
        'buttontest = buttontest & "<input type=""button""  border=""0"" name=""submit"" value=""Procedi con il pagamento"">"
        ' buttontest = buttontest & "<input type=""image"" src=""../images/pxpaga.gif"" border=""0"" style=""margin-top:-7px;padding:0;"" name=""submit"" alt=""Procedi con il pagamento"">"
        buttontest = buttontest & "<input type=""button"" onclick=""controllapag('" & scad & "','" & nome & "');"" value=""" & Format(importo, "€ ##,##") & " con carta di credito"" border=""0"" style=""margin-top:-7px;padding:0; height:30px; width:180px;""  >"
        buttontest = buttontest + "</form>"
        'Labelpay.Text = buttontest
        generacarta = buttontest
    End Function

    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.Params("par") = 1 Then
            Call verificapag()
        Else
            If Not Page.IsPostBack Then
                Dim pass As Boolean = False
                Dim passaporto As Integer = 0
                Dim piulungo As Integer = 0
                If Not IsNothing(Request.Params("parsel")) And Not IsNothing(Request.Params("code")) Then
                    If controllo(Request.Params("parsel"), Request.Params("code")) Then
                        If IsNumeric(Request.Params("parsel")) Then
                            lblcodicepreno.Text = Request.Params("code")
                            mailx.Text = Request.Params("email")
                            compagnia = ricavacompagnia(Request.Params("parsel"))
                            riempipagamenti(Request.Params("parsel"))
                            Dim altezza As Integer = 350
                            If ricavabonifico(Request.Params("parsel")) = 1 Then
                                lblbonifico.Text = "<b>BONIFICO BANCARIO</b><br /><br />Bonifico Bancario intestato a Fersina Viaggi S.r.l. - Via Stella, 5/M - 38123 TRENTO presso la Cassa Rurale Giudicarie Valsabbia Paganella coordinate IBAN: IT 71 X 08078 34770 000003031254<br /> <br /><b>E' obbligatorio indicare nella causale di pagamento il codice della presente prenotazione. Preghiamo inviare copia della ricevuta bancaria via mail a info@fersinaviaggi.it oppure via fax al numero: 0461 1810136 entro la data ed ora di scadenza della prenotazione. <br />Per le partenze entro 5 giorni lavorativi non sono accettate copie in-bank, ma solo copie contabili con timbro della banca e numero di c.r.o.</b>"
                                altezza = 450
                            Else
                                lblbonifico.Text = "<b>CARTA DI CREDITO O POSTEPAY</b><br /><br />Preghiamo provvedere al pagamento con Carta di Credito Visa, Mastercard o Postepay entro la data ed ora di scadenza della prenotazione. <br /> Non garantiamo la prenotazione della cabina se il pagamento avviene dopo ora e data di scadenza indicata. "
                            End If
                            altezza = altezza + (contarighe * 35)
                            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza(" & altezza & ", 'frameccc');", True)
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub verificapag()
        Dim app As String = Request.Params("ORDER_ID")
        Dim app2 As String = Request.Params("IMPORTO")
        Dim app3 As String = Request.Params("TRANSACTION_ID")
        Dim app4 As String = Request.Params("MAC")
        Dim app5 As String = Request.Params("MERCHANT_ID")
        Dim app6 As String = Request.Params("COD_AUT")
        Dim app7 As String = Request.Params("DIVISA")
        Dim MAC As String = Request.Params("MAC")
        If MAC = getMD5Hash(app3 & app5 & app & app6 & app2 & app7 & key) Then
            'Call SendMail("massimo@bustravel.it", "qoi8fc", app & "<br/>" & app2 & "<br/>" & app3 & "<br/>" & app4 & "<br/>" & app5)
            Dim orderid As String = Right(Left(app, 10), 6)
            Dim orderpag As String = Right(Left(app, 21), 10)
            'Dim tipopag As Integer = Int(Left(Replace(app, orderid & "-", ""), 1))        
            Call salvapagamento(orderpag, app2, app6)
            'Call generamail(orderid, ricavamail(orderid))
            Dim idpreno As Integer = controllacat(orderpag)
            If idpreno > 0 Then ' procedi a prenotare
                prendicabina(idpreno)
            End If
        End If
    End Sub

    Private Sub prendicabina(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM preno, periodo, nave where preno.id_periodo = periodo.id_periodo AND nave.id_nave = preno.id_nave and preno.id_preno =  '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim trovato As Boolean = False
        Dim dal As Date = Date.Now
        Dim cabina As String = ""
        Dim categoria As String = ""
        Dim codiceperiodo As String = ""
        Dim aeroporto As String = ""
        Dim volo As Integer = 0
        Dim persone As Integer = 0
        Dim assi As Integer = 0
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            persone = dr("adulti") + dr("bambini")
            cabina = dr("cabina")
            categoria = dr("categoria")
            codiceperiodo = dr("codiceperiodo")
            codice = dr("codice")
            email = dr("email")
            dal = dr("dal")
            volo = dr("volo")
            aeroporto = dr("aeroporto")
            assi = dr("rinunciaassi")
            If dr("compagnia") = 1 Then
                If dr("confermato_msc") = 0 Then
                    If dr("nopzione") = 1000000 Then
                        trovato = True
                    End If
                End If
            End If
        End If
        dr.Close()
        cn.Close()
        Dim proc As Boolean = False
        If trovato Then
            proc = ricavabook(dal, cabina, categoria, codiceperiodo, idpreno, persone, volo, aeroporto, assi)
        End If
        If proc = True Then
            Addnota("Cabina confermata in automatico", "Sistema")
        End If
    End Sub

    Protected Sub Addnota(ByVal testo As String, ByVal utente As String)
        Dim stringa As String
        stringa = "INSERT INTO nota SET id_preno = @id_preno, datanota = @datanota, testonota = @testonota, chinota = @chinota"
        Dim cmd2 As New MySqlCommand(stringa, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd2.Parameters.AddWithValue("@id_preno", Request.Params("idpreno"))
        cmd2.Parameters.AddWithValue("@datanota", Date.Now)
        cmd2.Parameters.AddWithValue("@testonota", testo)
        cmd2.Parameters.AddWithValue("@chinota", utente)
        cmd2.ExecuteNonQuery()
        cmd2.Parameters.Clear()
        cn.Close()
    End Sub

    Function ricavabook(ByVal dal As Date, ByVal cabina As String, ByVal categoria As String, ByVal codiceperiodo As String, ByVal idpreno As Integer, ByVal persone As Integer, ByVal volo As Integer, ByVal aeroporto As String, ByVal assi As Integer) As Boolean
        Dim ws As New net.costaclick.web.Booking
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
        cabin.Number = cabina
        dispo.Cabin = cabin
        If assi = 1 Then
            dispo.Insurance = False
        Else
            dispo.Insurance = True
        End If

        cat.Code = categoria
        If Left(codiceperiodo, 1) = "Z" Then
            far.Code = "ROULETTE"
        Else
            If Right(categoria, 1) = "X" Then
                far.Code = "PInd" 'Mordi e fuggi
            Else
                If Right(categoria, 1) = "V" Then
                    far.Code = "VALUE" 'Risparmiasubito
                Else
                    far.Code = "IND"
                End If
            End If
        End If
        dispo.Category = cat
        dispo.Code = codiceperiodo
        dispo.Type = net.costaclick.web.ComponentType.Cruise
        dispo.Fare = far
        Dim compo(0) As net.costaclick.web.Component
        compo(0) = dispo
        If volo > 0 Then
            Dim dispo2 As New net.costaclick.web.Component
            dispo2.Type = net.costaclick.web.ComponentType.Flight
            dispo2.Code = aeroporto
            Dim city As New net.costaclick.web.City
            city.Code = aeroporto
            If volo = 1 Then
                dispo2.Direction = net.costaclick.web.Direction.Both
            ElseIf volo = 2 Then
                dispo2.Direction = net.costaclick.web.Direction.OutBound
            ElseIf volo = 3 Then
                dispo2.Direction = net.costaclick.web.Direction.InBound
            End If
            Dim cities(0) As net.costaclick.web.City
            cities(0) = city
            dispo2.Cities = cities
            If assi = 1 Then
                dispo2.Insurance = False
            Else
                dispo2.Insurance = True
            End If

            ReDim Preserve compo(1)
            compo(1) = dispo2
        End If
        Dim pass(0) As net.costaclick.web.Guest
        Dim i As Integer = 0

        Dim sqlconn As String
        sqlconn = "SELECT * FROM nomi WHERE id_preno = '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Do While dr.Read
            Dim guest As New net.costaclick.web.Guest
            Dim dataapp As Date = dr("datanascita")
            guest.FirstName = dr("nomep")
            guest.LastName = dr("cognomep")
            guest.LanguageCode = "I"
            guest.BirthDate = dataapp.Year & "-" & dataapp.Month & "-" & dataapp.Day & "T00:00:00+00:00"
            guest.Nationality = "IT"
            If dr("tipopax") = 1 Then
                guest.Gender = net.costaclick.web.Gender.Male
            Else
                guest.Gender = net.costaclick.web.Gender.Female
            End If
            ReDim Preserve pass(i)
            pass(i) = guest
            If i = 0 Then
                If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                    guest.CostaClubNumber = Request.Params("cartaclub")
                End If
            End If
            i = i + 1
        Loop
        dr.Close()
        cn.Close()
        Dim consumer As New net.costaclick.web.Consumer
        Dim book As New net.costaclick.web.BookingStatus
        book = net.costaclick.web.BookingStatus.BKD  'CONFERMO LA PRENO DIRETTA!
        Dim pay As New net.costaclick.web.Payment
        ricavabook = False
        Try
            Dim respo As net.costaclick.web.BookingDetail
            respo = ws.CreateAndReviseBookingComplete(book, compo, "", pass, "", pay, "", "", consumer, "")
            Dim stringacodice As String = "UPDATE preno SET nopzione=@nopzione, reale=@reale, confermato_msc=@confermato_msc  WHERE id_preno = '" & idpreno & "'"
            Dim cmd3 As New MySqlCommand(stringacodice, cn)
            cmd3.Parameters.AddWithValue("@nopzione", respo.BookingNumber)
            cmd3.Parameters.AddWithValue("@reale", respo.ExpirationDate)
            cmd3.Parameters.AddWithValue("@confermato_msc", 1)
            If cn.State = ConnectionState.Closed Then cn.Open()
            cmd3.ExecuteNonQuery()
            cmd3.Connection.Close()
            cn.Close()
            ricavabook = True
        Catch ex As Exception

        End Try

    End Function

    Function controllacat(ByVal codice As String) As Integer
        controllacat = 0
        Dim sqlconn As String
        sqlconn = "SELECT periodo.codiceperiodo, periodo.compagnia, preno.categoria, pagamenti.id_preno FROM preno, pagamenti, periodo WHERE preno.id_periodo = periodo.id_periodo AND preno.id_preno = pagamenti.id_preno AND pagamenti.id_pagamento = '" & CInt(codice) & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If (Right(dr("categoria"), 1) = "V" Or Left(dr("codiceperiodo"), 1) = "Z") And dr("compagnia") = 1 Then
                controllacat = dr("id_preno")
            End If
            ' se è costa ed Risparmia subito allora procedi a prenotare
        End If
        dr.Close()
        cn.Close()
    End Function

    Public Shared Function getMD5Hash(ByVal input As String) As String
        Dim x As New System.Security.Cryptography.MD5CryptoServiceProvider()
        Dim bs As Byte() = System.Text.Encoding.UTF8.GetBytes(input)
        bs = x.ComputeHash(bs)
        Dim s As New System.Text.StringBuilder()
        For Each b As Byte In bs
            s.Append(b.ToString("x2").ToUpper())
        Next
        Dim hash As String = s.ToString()
        Return hash
    End Function


    Private Sub salvapagamento(ByVal codice As String, ByVal importo As String, ByVal cod_aut As String)
        Dim prezzo As Integer = CInt(Replace(importo, ".", ",")) 'lui passa in testo 300.00 che in cdec diventa 30000
        Dim stringacodice As String = "UPDATE pagamenti SET ricevuto = @ricevuto, tiporicevuto = @tiporicevuto, ricevutodata = @ricevutodata, chiinserito = @chiinserito, cod_auth = @cod_auth WHERE prezzo = " & prezzo & " AND id_pagamento = '" & CInt(codice) & "'"
        Dim cmd3 As New MySqlCommand(stringacodice, cn)
        cmd3.Parameters.AddWithValue("@ricevuto", 1)
        cmd3.Parameters.AddWithValue("@tiporicevuto", 1)
        cmd3.Parameters.AddWithValue("@ricevutodata", Date.Now)
        cmd3.Parameters.AddWithValue("@chiinserito", "sistema")
        cmd3.Parameters.AddWithValue("@cod_auth", cod_aut)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd3.ExecuteNonQuery()
        cmd3.Connection.Close()
        cn.Close()
    End Sub
    Function ricavacompagnia(ByVal idpreno As Integer) As Integer
        ricavacompagnia = 0
        Dim sqlconn As String
        sqlconn = "SELECT periodo.compagnia from preno, periodo WHERE preno.id_periodo = periodo.id_periodo AND preno.id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavacompagnia = dr("compagnia")
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function ricavabonifico(ByVal idpreno As Integer) As Integer
        ricavabonifico = 0
        Dim sqlconn As String
        sqlconn = "SELECT bonifico from preno WHERE id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavabonifico = dr("bonifico")
        End If
        dr.Close()
        cn2.Close()
    End Function

    Function controllo(ByVal idpreno As String, ByVal code As String) As Boolean
        controllo = False
        Dim sqlconn As String
        sqlconn = "SELECT id_preno, codice, email FROM preno WHERE id_preno = '" & idpreno & "' AND codice = '" & code & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            controllo = True
        End If
        dr.Close()
        cn.Close()
    End Function

    Function almenounoricevuto(ByVal idpreno As String) As Boolean
        almenounoricevuto = False
        Dim sqlconn As String
        sqlconn = "SELECT * from pagamenti WHERE ricevuto = 1 AND id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn3)
        If cn3.State = ConnectionState.Closed Then cn3.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            almenounoricevuto = True
        End If
        dr.Close()
        cn3.Close()
    End Function
End Class
