Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Partial Class crociere_assegna
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim contarighe As Integer = 0
    Dim rigona As String = ""
    Dim fremone As String = ""
    Dim codiceperiodo As String = ""
    Dim categoria As String = ""
    Dim cabina As String = ""
    Dim dal As Date = Date.Now
    Dim codice As String = ""
    Dim email As String = ""
    Dim ricavabook As Boolean = False
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            Call prendicabina(Request.Params("idpreno"))
            ' Dim altezza As Integer
            'altezza = 160 + (35 * contarighe)
            'altezza = 1300
            ' ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameassegna');", True)
        End If
    End Sub

    Private Sub prendicabina(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM preno, periodo, nave where preno.id_periodo = periodo.id_periodo AND nave.id_nave = preno.id_nave and preno.id_preno =  '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim trovato As Boolean = False
        Dim assi As Integer = 0
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            trovato = True
            cabina = dr("cabina")
            categoria = dr("categoria")
            codiceperiodo = dr("codiceperiodo")
            codice = dr("codice")
            email = dr("email")
            dal = dr("dal")
            assi = dr("rinunciaassi")
        End If
        dr.Close()
        cn.Close()
        'Dim proc As Boolean
        If trovato Then
            Call bookcosta(dal, cabina, categoria, codiceperiodo, idpreno, assi)
        End If
        If ricavabook = True Then
            Dim lnk As String = "conferma-crociere.aspx?lt=1&maila=1&codice=" & codice & "&email=" & email
            ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
        Else
            'Dim lnk As String = "conferma-crociere.aspx?lt=1&maila=1&codice=" & codice & "&email=" & email
            'ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
        End If
        'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "alert('" & Label1.Text & "')", True)
    End Sub

    Private Sub bookcosta(ByVal dal As Date, ByVal cabina As String, ByVal categoria As String, ByVal codiceperiodo As String, ByVal idpreno As Integer, ByVal assi As Integer)
        ' Label1.Text = "codiceperiodo " & Request.Params("codiceperiodo") & " - cabina: " & Request.Params("cabina") & " - categoria: " & Request.Params("categoria")
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
            If Right(categoria, 1) = "X" Then 'Mordi e fuggi
                far.Code = "PInd"
            Else
                If Right(categoria, 1) = "V" Then
                    far.Code = "VALUE" 'Risparmiasubito
                Else
                    If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                        far.Code = "IND_CC"
                    Else
                        far.Code = "IND"
                    End If
                End If
            End If
        End If
        dispo.Category = cat
        dispo.Code = codiceperiodo
        dispo.Type = net.costaclick.web.ComponentType.Cruise
        dispo.Fare = far
        Dim compo(0) As net.costaclick.web.Component
        compo(0) = dispo
        If Request.Params("volo") > 0 Then
            If Request.Params("aeroporto") <> "NNN" Then
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
                If assi = 1 Then
                    dispo2.Insurance = False
                Else
                    dispo2.Insurance = True
                End If
                ReDim Preserve compo(1)
                compo(1) = dispo2
            End If
        End If
        Dim pass(0) As net.costaclick.web.Guest
        Dim persone As Integer = CInt(Request.Params("persone"))

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
        'consumer.FirstName = "Massimo"
        'consumer.LastName = "Tonidandel"
        'consumer.StateCode = "TN"
        'consumer.CountryCode = "I"
        'consumer.Address = "Via Stella"
        'consumer.City = "Trento"
        'consumer.ZipCode = "38123"
        'Dim phone As New net.costaclick.web.ContactPhone
        'phone.Number = "0461914471"
        'phone.Type = net.costaclick.web.PhoneType.Home
        'consumer.Phone = phone
        Dim book As New net.costaclick.web.BookingStatus
        book = net.costaclick.web.BookingStatus.OPT
        Dim pay As New net.costaclick.web.Payment
        'GridView1.DataSource = pass

        ricavabook = False
        Dim ok As Boolean = False
        Try
            Dim nuovacabina As Boolean = False
            GridResult.DataSource = ws.HoldCabin(compo, pass)
            GridResult.DataBind()
            GridResult.Visible = True
            Dim oItem As GridViewRow
            For Each oItem In GridResult.Rows
                Dim Number As Label = CType(oItem.FindControl("Number"), Label)
                If Number.Text = cabina Then
                    ok = True
                Else
                    nuovacabina = True
                End If
            Next
            If ok = True Then

            Else
                If nuovacabina = True Then
                    Label7.Text = "La cabina presa non è più dispobibile - SCEGLI NUOVA CABINA:"
                    panelcabine.Visible = True
                    Repeatercabine.DataSource = ws.HoldCabin(compo, pass)
                    Repeatercabine.DataBind()
                Else
                    Label1.Text = "La cabina presa non è più dispobibile - Preghiamo contattare i nostri uffici al numero 0461 914471"
                End If
            End If
        Catch ex As Exception

            Label1.Text = ex.Message & "<br /><br />Preghiamo contattare l'ufficio al numero 0461 914471"
            If Left(codiceperiodo, 1) = "Z" Then 'roulette
                Dim stringacodice As String = "UPDATE preno SET nopzione=@nopzione, opzione=@opzione, reale=@reale, bonifico = @bonifico  WHERE id_preno = '" & idpreno & "'"
                Dim cmd3 As New MySqlCommand(stringacodice, cn)
                cmd3.Parameters.AddWithValue("@nopzione", 1000000)
                cmd3.Parameters.AddWithValue("@opzione", DateAdd(DateInterval.Minute, 10, Date.Now))
                cmd3.Parameters.AddWithValue("@reale", DateAdd(DateInterval.Minute, 10, Date.Now))
                cmd3.Parameters.AddWithValue("@bonifico", 0)
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd3.ExecuteNonQuery()
                cn.Close()
                Dim idpag As Integer = ricavaidpagamento(idpreno)
                Dim stringapag As String = "UPDATE pagamenti SET scadenza=@scadenza WHERE id_pagamento = '" & idpag & "'"
                Dim cmd4 As New MySqlCommand(stringapag, cn)
                cmd4.Parameters.AddWithValue("@scadenza", DateAdd(DateInterval.Minute, 10, Date.Now))
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd4.ExecuteNonQuery()
                cn.Close()
                ricavabook = True
            End If
        End Try
        If ok = True Then
            Try
                Dim respo As net.costaclick.web.BookingDetail
                respo = ws.CreateAndReviseBookingComplete(book, compo, "", pass, "", pay, "", "", consumer, "")
                Dim bonifico As Integer = 0
                If DateDiff(DateInterval.Day, Date.Now, dal) > 5 Then 'se la partenza è almeno fra 5 giorni
                    If Date.Now.DayOfWeek = 6 Then ' sabato 0
                        If DateDiff(DateInterval.Hour, Date.Now, respo.ExpirationDate) > (48 - Date.Now.Hour) Then
                            bonifico = 1
                        End If
                    ElseIf Date.Now.DayOfWeek = 0 Then 'domenica
                        If DateDiff(DateInterval.Hour, Date.Now, respo.ExpirationDate) > (24 - Date.Now.Hour) Then
                            bonifico = 1
                        End If
                    ElseIf Date.Now.DayOfWeek = 5 And Date.Now.Hour > 18 Then ' venerdì dopo le 19                   
                        If DateDiff(DateInterval.Hour, Date.Now, respo.ExpirationDate) > (24 - Date.Now.Hour) Then
                            If DateDiff(DateInterval.Hour, Date.Now, respo.ExpirationDate) > (72 - Date.Now.Hour) Then
                                bonifico = 1
                            End If
                        End If
                    Else ' da lunedì a venerdì dalle 00.01 di lunedì alle 18.59 di venerdì
                        bonifico = 1
                    End If
                End If
                Dim datap As Date

                If respo.ExpirationDate.Date = Date.Now.Date Then
                    If Date.Now.Hour > 15 Then
                        If Date.Now.Hour < 19 Then
                            datap = DateAdd(DateInterval.Hour, (19 - Date.Now.Hour), Date.Now)
                        Else
                            datap = DateAdd(DateInterval.Minute, 15, Date.Now) ' 10 minuti dopo averla fatta
                        End If
                    Else
                        datap = DateAdd(DateInterval.Hour, 4, Date.Now)
                    End If
                    ' datap = DateAdd(DateInterval.Minute, -Date.Now.Minute, datap)
                Else
                    datap = DateAdd(DateInterval.Hour, 12, respo.ExpirationDate.Date)
                End If

                Dim stringacodice As String = "UPDATE preno SET nopzione=@nopzione, opzione=@opzione, reale=@reale, bonifico = @bonifico  WHERE id_preno = '" & idpreno & "'"
                Dim cmd3 As New MySqlCommand(stringacodice, cn)
                cmd3.Parameters.AddWithValue("@nopzione", respo.BookingNumber)
                cmd3.Parameters.AddWithValue("@opzione", datap)
                cmd3.Parameters.AddWithValue("@reale", respo.ExpirationDate)
                cmd3.Parameters.AddWithValue("@bonifico", bonifico)
                If cn.State = ConnectionState.Closed Then cn.Open()
                cmd3.ExecuteNonQuery()
                cmd3.Connection.Close()
                cn.Close()
                Call updatepagamenti(idpreno, datap)
                ricavabook = True
            Catch ex As Exception
                Label1.Text = ex.Message.ToString & "<br /><br />Preghiamo contattare l'ufficio al numero 0461 914471"
                If ex.Message.IndexOf("immediata") >= 1 Then

                    Dim stringacodice As String = "UPDATE preno SET nopzione=@nopzione, opzione=@opzione, reale=@reale, bonifico = @bonifico  WHERE id_preno = '" & idpreno & "'"
                    Dim cmd3 As New MySqlCommand(stringacodice, cn)
                    cmd3.Parameters.AddWithValue("@nopzione", 1000000)
                    cmd3.Parameters.AddWithValue("@opzione", DateAdd(DateInterval.Minute, 10, Date.Now))
                    cmd3.Parameters.AddWithValue("@reale", DateAdd(DateInterval.Minute, 10, Date.Now))
                    cmd3.Parameters.AddWithValue("@bonifico", 0)
                    If cn.State = ConnectionState.Closed Then cn.Open()
                    cmd3.ExecuteNonQuery()
                    cn.Close()
                    Dim idpag As Integer = ricavaidpagamento(idpreno)
                    Dim stringapag As String = "UPDATE pagamenti SET scadenza=@scadenza WHERE id_pagamento = '" & idpag & "'"
                    Dim cmd4 As New MySqlCommand(stringapag, cn)
                    cmd4.Parameters.AddWithValue("@scadenza", DateAdd(DateInterval.Minute, 10, Date.Now))
                    If cn.State = ConnectionState.Closed Then cn.Open()
                    cmd4.ExecuteNonQuery()
                    cn.Close()
                    ricavabook = True
                End If

            End Try
        End If
    End Sub

    Private Sub updatepagamenti(ByVal idpreno As Integer, ByVal scadenza As String)
        Dim sqlconn As String
        Dim idpaga As Integer
        sqlconn = "SELECT * FROM pagamenti Where id_preno= " & idpreno & " order by scadenza"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleResult)
        If dr.HasRows Then
            dr.Read()
            idpaga = dr("id_pagamento")
        Else
            idpaga = 0
        End If
        dr.Close()
        sqlconn = "UPDATE pagamenti SET scadenza = @scadenza WHERE id_pagamento =" & idpaga
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        cmd2.Parameters.AddWithValue("@scadenza", CDate(scadenza))
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd2.ExecuteNonQuery()
        cmd2.Parameters.Clear()
        cn.Close()
    End Sub

    Protected Sub Repeatercabine_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeatercabine.ItemDataBound
        Dim Prendicabina As HyperLink = CType(e.Item.FindControl("prendi"), HyperLink)
        Dim LabelUrl As Label = CType(e.Item.FindControl("LabelUrl"), Label)
        Dim numcabina As Label = CType(e.Item.FindControl("LabelCabina"), Label)
        Dim LabelPonte As Label = CType(e.Item.FindControl("LabelPonte"), Label)
        Dim LabelNPonte As Label = CType(e.Item.FindControl("LabelNPonte"), Label)
        Dim LabelFacility As Label = CType(e.Item.FindControl("LabelFacility"), Label)
        Dim LabelTipoCabina As Label = CType(e.Item.FindControl("LabelTipoCabina"), Label)
        Dim poscabina As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("poscabina"), System.Web.UI.HtmlControls.HtmlImage)
        Dim disabili As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("disabili"), System.Web.UI.HtmlControls.HtmlImage)
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim frameimage As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("frameimage"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then            
            LabelPonte.Text = Replace(LabelPonte.Text, "'", "")
            If LabelFacility.Text.ToUpper = "TRUE" Then 'disabili
                disabili.Style.Add("visibility", "visible")
            End If
            Call cambiacabina(Request.Params("idpreno"), numcabina.Text)
            Prendicabina.Attributes.Add("onclick", "javascript:prendicabina('divassegna', 'assegna.aspx', '" & Request.Params("idpreno") & "', '" & Request.Params("volo") & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '1', '" & Request.Params("tipologiacabina") & "', '" & Request.Params("tipocabina") & "')")
            LabelTipoCabina.Text = Request.Params("tipocabina")
            contarighe = contarighe + 1
        End If
    End Sub

    Function ricavaidpagamento(ByVal idpreno As Integer) As Integer
        ricavaidpagamento = 0
        Dim sqlconn As String
        sqlconn = "SELECT id_pagamento FROM pagamenti WHERE id_preno = '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavaidpagamento = dr("id_pagamento")
        End If
        dr.Close()
        cn.Close()
    End Function
    Private Sub cambiacabina(ByVal idpreno As Integer, ByVal cab As String)
        Dim stringacodice As String = "UPDATE preno SET cabina=@cabina WHERE id_preno = '" & idpreno & "' order by scadenza ASC"
        Dim cmd3 As New MySqlCommand(stringacodice, cn)
        cmd3.Parameters.AddWithValue("@cabina", cab)
        If cn.State = ConnectionState.Closed Then cn.Open()
        cmd3.ExecuteNonQuery()
        cmd3.Connection.Close()
        cn.Close()
    End Sub
End Class
