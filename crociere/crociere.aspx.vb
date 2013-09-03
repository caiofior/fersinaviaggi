Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Net.Mail
Partial Class crociere_crociere
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim perpagina As Integer = 20
    Dim counter As Integer = 0
    Dim maxpagine As Integer = 15
    Dim scadenza1, scadenza2, scadenza3, scadenza4 As Date
    Dim elemento, elemento2, elemento3, elemento4 As String
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim nomezona(0) As String
    Dim promo(0) As String
    Dim nave2(0) As String
    Dim fotonave2(0) As String
    Dim contatore As Integer = 0
    Dim offertachiama As String = ""
    Dim roulette As String = ""
    Dim righeultime As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim querysql As String = ""
        If Not Page.IsPostBack Then
            Call caricapromo()
            Call caricanave()
            Dim framecerca As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("framecerca"), System.Web.UI.HtmlControls.HtmlGenericControl)
            Dim mese As Integer = 0
            Dim idnave As Integer = 0
            Dim zona As Integer = 0
            If IsNumeric(Request.Params("mese")) Then
                mese = (Request.Params("mese"))
            End If
            Dim compagnia As Integer = -1
            If IsNumeric(Request.Params("compagnia")) Then
                compagnia = (Request.Params("compagnia"))
                If compagnia = 1 Then
                    ricavaimmaginepromo(1)
                End If
            End If

            If IsNumeric(Request.Params("nave")) Then
                idnave = (Request.Params("nave"))
            End If
            Dim porto As String = ""
            If Not IsNothing(Request.Params("porto")) Then
                porto = Request.Params("porto")
            End If
            If Not IsNothing(Request.Params("zona")) Then
                zona = Request.Params("zona")
            End If
            Dim promo As String = ""
            If IsNumeric(Request.Params("promo")) Then
                ricavaimmaginepromo(Request.Params("promo"))
                promo = " AND (idpromo = " & Request.Params("promo")
                If IsNumeric(Request.Params("promo2")) Then
                    promo = promo & "  OR idpromo = " & Request.Params("promo2")
                End If
                If IsNumeric(Request.Params("promo3")) Then
                    promo = promo & "  OR idpromo = " & Request.Params("promo3")
                End If
                If IsNumeric(Request.Params("promo4")) Then
                    promo = promo & "  OR idpromo = " & Request.Params("promo4")
                End If
                If IsNumeric(Request.Params("promo5")) Then
                    promo = promo & "  OR idpromo = " & Request.Params("promo5")
                End If
                If IsNumeric(Request.Params("promo6")) Then
                    promo = promo & "  OR idpromo = " & Request.Params("promo6")
                End If
                querysql = querysql & promo & ")"
            End If
            framecerca.Attributes.Add("src", "../ricerca.aspx?mese=" & mese & "&compagnia=" & compagnia & "&nave=" & idnave & "&porto=" & porto & "&zona=" & zona)
            framecerca.Attributes.Add("onload", "ffiframe('carica')")
            If compagnia >= 0 Then
                querysql = querysql & " AND compagnia = " & compagnia
            End If
            If mese > 0 Then
                If mese <= 12 Then
                    querysql = querysql & " AND year(periodo.dal) = " & Date.Now.Year & " AND month(periodo.dal) = " & mese
                ElseIf mese >= 13 And mese <= 25 Then
                    querysql = querysql & " AND year(periodo.dal) = " & Date.Now.Year + 1 & " AND month(periodo.dal) = " & mese - 12
                Else
                    querysql = querysql & " AND year(periodo.dal) = " & Date.Now.Year + 2 & " AND month(periodo.dal) = " & mese - 25
                End If
            End If

            Dim incrocio As Boolean = False
            If idnave > 0 Then
                querysql = querysql & " AND nave.id_nave = " & idnave
            End If
            If zona > 0 Then
                querysql = querysql & " AND zona = " & zona
            End If
            If Not IsNothing(Request.Params("porto")) Then
                If Not IsNumeric(Request.Params("porto")) Then
                    porto = porto.Replace("'", "")
                    porto = porto.Replace("<", "")
                    porto = porto.Replace(">", "")
                    porto = porto.Replace("&", "")
                    porto = porto.Replace("%", "")
                    porto = porto.Replace("$", "")
                    porto = porto.Replace("£", "")
                    querysql = querysql & " AND (periodo.imbarco like '%" & porto & "%')"
                End If
            End If
            Dim spezzata As String = " AND (spezzata = 0 or spezzata = 1) "
            If IsNumeric(Request.Params("spezzata")) Then
                If Request.Params("spezzata") = 1 Then
                    spezzata = " AND spezzata = 1 "
                    riga1.InnerHtml = "<img src='../images/lowcosto.jpg' alt='Itinerari low cost spezzati' style='border:none;' />"
                    riga2.InnerHtml = "<img src='../images/lowcosto.jpg' alt='Itinerari low cost spezzati' style='border:none;' />"
                    riga3.Visible = False
                    'riga4.Visible = False
                    scontomsc.Visible = False
                    scontocosta.Visible = False
                End If
            End If
            Dim capodanno As String = " AND capodanno = 0 "
            Dim estate As String = " AND estate = 0 "
            Dim limite As Integer = 0
            If IsNumeric(Request.Params("n")) Then
                limite = Request.Params("n")
            End If
            Dim riservata As Integer = 0
            Dim aprifb As String = ""
            If Request.Params("parf") = "0013445" Then                
                riservata = 1
                riga1.InnerHtml = "<img src='../images/promofacebook.jpg' alt='Promozione riservata' style='border:none;' />"
                riga2.Visible = False
                riga3.Visible = False
                riga4.Visible = False
                scontomsc.Visible = False
                scontocosta.Visible = False
                framecerca.Attributes.Add("src", "ricerca-facebook.htm")
                If leggocookie("fb") <> 13445 Then
                    aprifb = "caricarichiesta('centroprogramma', 'layout');"
                    'aprifb = aprifb & "<script type='text/javascript' src='facebook.js?id=4'></script>"
                    ClientScript.RegisterClientScriptInclude(Page.[GetType](), "scriptKey", ResolveClientUrl("facebook.js?id=4"))
                End If
            Else
                If Not IsNumeric(Request.Params("n")) Then 'annunci sopratutti tipo chiusura ferragosto
                    'face.Visible = False
                    'ferragosto.Style.Add("visibility", "visible")
                    'aprifb = "caricarichiesta('centroprogramma', 'layout');"
                    ''aprifb = aprifb & "<script type='text/javascript' src='facebook.js?id=4'></script>"
                    'ClientScript.RegisterClientScriptInclude(Page.[GetType](), "scriptKey", ResolveClientUrl("facebook.js?id=4"))
                End If
                End If
                Call caricadati(querysql, spezzata, limite, riservata)
                'Call riempimese(DropMese, "- tutti i mesi - ")
                Dim jstr As String = ""
                If roulette <> "" Then
                    jstr = "applicaroulette();"
                End If
                Call tacaultime()
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "scrollDiv('" & righeultime & "');" & offertachiama & roulette & jstr & aprifb, True)
        End If
    End Sub

    Private Sub tacaultime()
        Dim sqlconn As String
        sqlconn = "SELECT dispo.*, periodo.dal, periodo.imbarco, nave.titolo from dispo, nave, periodo WHERE dispo.id_periodo = periodo.id_periodo AND nave.id_nave = dispo.id_nave AND dispo.ultimacabina = 1 and date(dispo.reale) >= date(now()) order by periodo.dal"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterUltime.DataSource = dr
            RepeaterUltime.DataBind()
            ultime.Style.Add("visibility", "visible")
        End If
        dr.Close()
        cn.Close()
        Dim oItem As RepeaterItem
        For Each oItem In RepeaterUltime.Items
            righeultime = righeultime + 1
        Next

    End Sub



    Private Sub ricavaimmaginepromo(ByVal idpromo As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from promozioni where id_promozione = " & idpromo
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If Not IsDBNull(dr("imagepromogrande")) Then
                If dr("imagepromogrande") <> "" Then
                    riga1.InnerHtml = "<img src='" & dr("imagepromogrande") & "' alt='" & dr("nomepromo") & "' style='border:none;' />"
                    riga2.InnerHtml = "<img src='" & dr("imagepromogrande") & "' alt='" & dr("nomepromo") & "' style='border:none;' />"
                    riga3.Visible = False
                    'riga4.Visible = False
                    scontomsc.Visible = False
                    scontocosta.Visible = False
                End If
            End If
        End If
        dr.Close()
        cn.Close()
    End Sub

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

    Function leggocookie(ByVal nome As String) As Integer
        Dim cookie As HttpCookie = Request.Cookies(nome)
        If Not cookie Is Nothing Then
            If IsNumeric(cookie.Value) Then
                leggocookie = cookie.Value
            Else
                leggocookie = 0
            End If
        Else
            leggocookie = 0
        End If
    End Function

    Private Sub caricapromo()
        Dim sqlconn As String
        sqlconn = "SELECT * from promozioni order by id_promozione"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Do While dr.Read()
                ReDim Preserve promo(dr("id_promozione"))
                promo(dr("id_promozione")) = dr("imagepromo")                
            Loop
        End If
        dr.Close()
        sqlconn = "SELECT * from promozioni where pubblicapromo = 0 order by id_promozione"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        Dim appmsc As String = "<ul>"
        Dim appcosta As String = "<ul>"
        If dr2.HasRows Then
            Do While dr2.Read()
                If dr2("compagnia") = 0 Then
                    appmsc = appmsc & "<li><a href='crociere.aspx?promo=" & dr2("id_promozione") & "' ><img src='" & dr2("imagepromo") & "' alt='" & dr2("nomepromo") & "' style='border:none;' /></a>"
                End If
                If dr2("compagnia") = 1 Then
                    appcosta = appcosta & "<li><a href='crociere.aspx?promo=" & dr2("id_promozione") & "' ><img src='" & dr2("imagepromo") & "' alt='" & dr2("nomepromo") & "' style='border:none;' /></a>"
                End If
            Loop
        End If
        appmsc = appmsc & "<ul/>"
        appcosta = appcosta & "<ul/>"
        offecosta.InnerHtml = appcosta
        offemsc.InnerHtml = appmsc
        dr2.Close()
        cn.Close()

    End Sub

    Protected Sub RepeaterPagine_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPagine.ItemDataBound
        Dim linkato As System.Web.UI.HtmlControls.HtmlAnchor = CType(e.Item.FindControl("linkato"), System.Web.UI.HtmlControls.HtmlAnchor)
        Dim lipag As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("lipag"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim illink As Label = CType(e.Item.FindControl("Labellink"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim n As Integer = 0
            If IsNumeric(Request.Params("n")) Then
                n = Request.Params("n")
            End If
            If ((n / perpagina) + 1) = (illink.Text) Then
                illink.Style.Add("font-weight", "bold")
                illink.Style.Add("color", "#FF0000")
                lipag.Style.Add("background", "url(../images/ttpaginasel.gif) no-repeat")
            Else
                illink.Style.Add("color", "#000000")
                lipag.Style.Add("cursor", "pointer")
                lipag.Attributes.Add("onclick", "location.href='" & linkato.HRef.ToString & "'")
            End If
        End If
    End Sub

    Private Sub riempipagine(ByVal querysql As String, ByVal spezzata As String, ByVal repeaterpag As Repeater, ByVal riservata As Integer)
        Dim contarecord As Integer = 0
        Dim conta As Integer = 0
        Dim sqlconn As String
        If riservata = 0 Then
            sqlconn = "SELECT count(id_periodo) As conta FROM periodo, nave, itinerario WHERE periodo.id_nave = nave.id_nave AND periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "'  AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.pubblica = 0 " & spezzata & querysql & " ORDER BY periodo.dal"
        Else
            sqlconn = "SELECT count(id_periodo) As conta FROM periodo, nave, itinerario WHERE periodo.riservata = 1 AND periodo.id_nave = nave.id_nave AND periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "'  AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.pubblica = 0 " & spezzata & querysql & " ORDER BY periodo.dal"
        End If
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.SingleResult)
        If dr.HasRows Then
            dr.Read()
            conta = CInt(dr("conta"))
        End If
        dr.Close()
        cn.Close()
        contarecord = Math.Ceiling(conta / perpagina)
        Labelp1.Text = "<b>" & conta & "</b> partenze suddivise in <b>" & contarecord & "</b> pagine"
        Labelp2.Text = "<b>" & conta & "</b> partenze suddivise in <b>" & contarecord & "</b> pagine"
        Dim dt As DataTable
        dt = New DataTable("paginare")
        Dim pagina As DataColumn = New DataColumn("pagina")
        pagina.DataType = System.Type.GetType("System.Int32")
        dt.Columns.Add(pagina)
        Dim linkato As DataColumn = New DataColumn("linkato")
        linkato.DataType = System.Type.GetType("System.String")
        dt.Columns.Add(linkato)
        Dim i As Integer
        Dim spezz As Integer = 0
        Dim tariffa As Integer = 0
        Dim capodan As Integer = 0
        Dim mese As Integer = 0
        Dim nave As Integer = 0
        Dim porto As String = ""
        Dim estate As Integer = 0
        Dim promo As String = ""
        Dim zona As Integer = 0
        Dim compagnia As Integer = -1
        If IsNumeric(Request.Params("promo")) Then
            promo = "&promo=" & Request.Params("promo")
        End If
        If IsNumeric(Request.Params("promo2")) Then
            promo = promo & "&promo2=" & Request.Params("promo2")
        End If
        If IsNumeric(Request.Params("promo3")) Then
            promo = promo & "&promo3=" & Request.Params("promo3")
        End If
        If IsNumeric(Request.Params("promo4")) Then
            promo = promo & "&promo4=" & Request.Params("promo4")
        End If
        If IsNumeric(Request.Params("promo5")) Then
            promo = promo & "&promo5=" & Request.Params("promo5")
        End If
        If IsNumeric(Request.Params("promo6")) Then
            promo = promo & "&promo6=" & Request.Params("promo6")
        End If
        If IsNumeric(Request.Params("spezzata")) Then
            spezz = Request.Params("spezzata")
        End If
        If IsNumeric(Request.Params("tariffa")) Then
            tariffa = Request.Params("tariffa")
        End If
        If IsNumeric(Request.Params("capodanno")) Then
            capodan = Request.Params("capodanno")
        End If
        If IsNumeric(Request.Params("mese")) Then
            mese = Request.Params("mese")
        End If
        If IsNumeric(Request.Params("nave")) Then
            nave = Request.Params("nave")
        End If
        If Not IsNothing(Request.Params("porto")) Then
            porto = Request.Params("porto")
        End If
        If IsNumeric(Request.Params("estate")) Then
            estate = Request.Params("estate")
        End If
        If IsNumeric(Request.Params("compagnia")) Then
            compagnia = Request.Params("compagnia")
        End If
        If IsNumeric(Request.Params("zona")) Then
            zona = Request.Params("zona")
        End If
        Dim partida As Integer = contarecord
        Dim finoa As Integer = 1
        Dim n As Integer = 0
        n = Request.Params("n")
        Dim p As Integer = 0
        p = Request.Params("p")
        If contarecord > maxpagine Then
            If (n / p) > Int(maxpagine / 2) Then
                If (n / p) + (maxpagine / 2) <= contarecord Then
                    partida = (n / p) + (maxpagine / 2)
                End If
                finoa = partida - maxpagine + 1
            Else
                partida = maxpagine
            End If
        End If
        For i = partida To finoa Step -1
            Dim dr2 As DataRow = dt.NewRow
            dr2("pagina") = i
            If riservata = 0 Then
                If porto <> "" Then
                    dr2("linkato") = "crociere.aspx?n=" & ((i - 1) * perpagina) & "&p=" & perpagina & "&tariffa=" & tariffa & "&capodanno=" & capodan & "&spezzata=" & spezz & "&mese=" & mese & "&nave=" & nave & "&porto=" & porto & "&estate=" & estate & "&zona=" & zona & "&compagnia= " & compagnia & promo
                Else
                    dr2("linkato") = "crociere.aspx?n=" & ((i - 1) * perpagina) & "&p=" & perpagina & "&tariffa=" & tariffa & "&capodanno=" & capodan & "&spezzata=" & spezz & "&mese=" & mese & "&nave=" & nave & "&estate=" & estate & "&zona=" & zona & "&compagnia= " & compagnia & promo
                End If
                dt.Rows.Add(dr2)
            Else
                If porto <> "" Then
                    dr2("linkato") = "crociere.aspx?parf=0013445&n=" & ((i - 1) * perpagina) & "&p=" & perpagina & "&tariffa=" & tariffa & "&capodanno=" & capodan & "&spezzata=" & spezz & "&mese=" & mese & "&nave=" & nave & "&porto=" & porto & "&estate=" & estate & "&zona=" & zona & "&compagnia= " & compagnia & promo
                Else
                    dr2("linkato") = "crociere.aspx?parf=0013445&n=" & ((i - 1) * perpagina) & "&p=" & perpagina & "&tariffa=" & tariffa & "&capodanno=" & capodan & "&spezzata=" & spezz & "&mese=" & mese & "&nave=" & nave & "&estate=" & estate & "&zona=" & zona & "&compagnia= " & compagnia & promo
                End If
                dt.Rows.Add(dr2)
            End If
        Next
        Dim ds As New DataSet
        ds.Tables.Add(dt)
        repeaterpag.DataSource = ds.Tables("paginare")
        repeaterpag.DataBind()
    End Sub

    Private Sub caricadati(ByVal querysql As String, ByVal spezzata As String, ByVal limite As Integer, ByVal riservata As Integer)
        Call caricazona()
        Dim sqlconn As String
        If riservata = 0 Then
            sqlconn = "SELECT * FROM periodo, nave, itinerario WHERE periodo.id_nave = nave.id_nave AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.pubblica = 0 AND periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' " & spezzata & querysql & " ORDER BY periodo.ordine DESC, periodo.dal ASC LIMIT " & limite & "," & perpagina
        Else
            sqlconn = "SELECT nave.*, itinerario.*,   periodo.prezzoir as prezzoi, periodo.prezzoer as prezzoe, periodo.prezzobr as prezzob, periodo.prezzosr as prezzos, periodo.* FROM periodo, nave, itinerario WHERE periodo.riservata = 1 AND periodo.id_nave = nave.id_nave AND periodo.id_itinerario = itinerario.id_itinerario AND periodo.pubblica = 0 AND periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' " & spezzata & querysql & " ORDER BY periodo.ordine DESC, periodo.dal ASC LIMIT " & limite & "," & perpagina
        End If
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Repeater1.DataSource = dr
            Repeater1.DataBind()
            Labelno.Visible = False
        Else
            Labelno.Visible = True
            nopromo.Visible = True
        End If
        If counter = 1 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "dataora = '" & CDate(scadenza1).Year & "/" & CDate(scadenza1).Month & "/" & CDate(scadenza1).Day & " " & CDate(scadenza1).Hour & ":" & CDate(scadenza1).Minute & ":" & CDate(scadenza1).Second & "'; elemento = '" & elemento & "';", True)
        ElseIf counter = 2 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "dataora = '" & CDate(scadenza1).Year & "/" & CDate(scadenza1).Month & "/" & CDate(scadenza1).Day & " " & CDate(scadenza1).Hour & ":" & CDate(scadenza1).Minute & ":" & CDate(scadenza1).Second & "'; elemento = '" & elemento & "'; dataora2 = '" & CDate(scadenza2).Year & "/" & CDate(scadenza2).Month & "/" & CDate(scadenza2).Day & " " & CDate(scadenza2).Hour & ":" & CDate(scadenza2).Minute & ":" & CDate(scadenza2).Second & "'; elemento2 = '" & elemento2 & "';", True)
        ElseIf counter = 3 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "dataora = '" & CDate(scadenza1).Year & "/" & CDate(scadenza1).Month & "/" & CDate(scadenza1).Day & " " & CDate(scadenza1).Hour & ":" & CDate(scadenza1).Minute & ":" & CDate(scadenza1).Second & "'; elemento = '" & elemento & "'; dataora2 = '" & CDate(scadenza2).Year & "/" & CDate(scadenza2).Month & "/" & CDate(scadenza2).Day & " " & CDate(scadenza2).Hour & ":" & CDate(scadenza2).Minute & ":" & CDate(scadenza2).Second & "'; elemento2 = '" & elemento2 & "'; dataora3 = '" & CDate(scadenza3).Year & "/" & CDate(scadenza3).Month & "/" & CDate(scadenza3).Day & " " & CDate(scadenza3).Hour & ":" & CDate(scadenza3).Minute & ":" & CDate(scadenza3).Second & "'; elemento3 = '" & elemento3 & "';", True)
        ElseIf counter = 4 Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "dataora = '" & CDate(scadenza1).Year & "/" & CDate(scadenza1).Month & "/" & CDate(scadenza1).Day & " " & CDate(scadenza1).Hour & ":" & CDate(scadenza1).Minute & ":" & CDate(scadenza1).Second & "'; elemento = '" & elemento & "'; dataora2 = '" & CDate(scadenza2).Year & "/" & CDate(scadenza2).Month & "/" & CDate(scadenza2).Day & " " & CDate(scadenza2).Hour & ":" & CDate(scadenza2).Minute & ":" & CDate(scadenza2).Second & "'; elemento2 = '" & elemento2 & "'; dataora3 = '" & CDate(scadenza3).Year & "/" & CDate(scadenza3).Month & "/" & CDate(scadenza3).Day & " " & CDate(scadenza3).Hour & ":" & CDate(scadenza3).Minute & ":" & CDate(scadenza3).Second & "'; elemento3 = '" & elemento3 & "'; dataora4 = '" & CDate(scadenza4).Year & "/" & CDate(scadenza4).Month & "/" & CDate(scadenza4).Day & " " & CDate(scadenza4).Hour & ":" & CDate(scadenza4).Minute & ":" & CDate(scadenza4).Second & "'; elemento4 = '" & elemento4 & "';", True)
        End If
        dr.Close()
        cn.Close()
        Call riempipagine(querysql, spezzata, RepeaterPagine, riservata)
        Call riempipagine(querysql, spezzata, RepeaterPagine2, riservata)
    End Sub

    Protected Sub Repeater1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeater1.ItemDataBound
        Dim dalc As Label = CType(e.Item.FindControl("dal"), Label)
        Dim dalc2 As Label = CType(e.Item.FindControl("dal2"), Label)
        Dim alc As Label = CType(e.Item.FindControl("al"), Label)
        Dim alc2 As Label = CType(e.Item.FindControl("al2"), Label)
        Dim imbarco As Label = CType(e.Item.FindControl("imbarco"), Label)
        Dim sbarco As Label = CType(e.Item.FindControl("sbarco"), Label)
        Dim dala As Label = CType(e.Item.FindControl("dala"), Label)
        Dim ala As Label = CType(e.Item.FindControl("ala"), Label)
        Dim imbarco1 As Label = CType(e.Item.FindControl("imbarco1"), Label)
        Dim sbarco1 As Label = CType(e.Item.FindControl("sbarco1"), Label)
        Dim imbarco2 As Label = CType(e.Item.FindControl("imbarco2"), Label)
        Dim sbarco2 As Label = CType(e.Item.FindControl("sbarco2"), Label)
        Dim imbarco3 As Label = CType(e.Item.FindControl("imbarco3"), Label)
        Dim sbarco3 As Label = CType(e.Item.FindControl("sbarco3"), Label)
        Dim dalimbarco As Label = CType(e.Item.FindControl("dalimbarco"), Label)
        Dim alsbarco As Label = CType(e.Item.FindControl("alsbarco"), Label)
        Dim interoi As Label = CType(e.Item.FindControl("interoi"), Label)
        Dim interos As Label = CType(e.Item.FindControl("interos"), Label)
        Dim prezzos As Label = CType(e.Item.FindControl("prezzos"), Label)
        Dim prezzoi As Label = CType(e.Item.FindControl("prezzoi"), Label)
        Dim interoe As Label = CType(e.Item.FindControl("interoe"), Label)
        Dim prezzoe As Label = CType(e.Item.FindControl("prezzoe"), Label)
        Dim interob As Label = CType(e.Item.FindControl("interob"), Label)
        Dim prezzob As Label = CType(e.Item.FindControl("prezzob"), Label)
        Dim tapromo As Label = CType(e.Item.FindControl("tapromo"), Label)
        Dim zona As Label = CType(e.Item.FindControl("zona"), Label)
        Dim titolo As Label = CType(e.Item.FindControl("titolo"), Label)
        Dim cabinapromo As Label = CType(e.Item.FindControl("cabinapromo"), Label)
        Dim capodanno As Label = CType(e.Item.FindControl("capodanno"), Label)
        Dim lblinterna As Label = CType(e.Item.FindControl("lblinterna"), Label)
        Dim lblesterna As Label = CType(e.Item.FindControl("lblfinestra"), Label)
        Dim lblbalcony As Label = CType(e.Item.FindControl("lblbalcony"), Label)
        Dim id_periodo As Label = CType(e.Item.FindControl("id_periodo"), Label)
        Dim scadenza As Label = CType(e.Item.FindControl("scadenza"), Label)
        Dim scadenzapromo As Label = CType(e.Item.FindControl("scadenzapromo"), Label)
        Dim id_nave As Label = CType(e.Item.FindControl("id_nave"), Label)
        Dim id_nave2 As Label = CType(e.Item.FindControl("id_nave2"), Label)
        Dim LabelTempo As Label = CType(e.Item.FindControl("LabelTempo"), Label)
        Dim banda As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("banda"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim pbanda As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("pbanda"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di1 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di1"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di2 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di2"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di3 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di3"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di1b As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di1b"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di2b As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di2b"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim di3b As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("di3b"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim hyperpreno As HyperLink = CType(e.Item.FindControl("hyperpreno"), HyperLink)
        Dim hypernave As HyperLink = CType(e.Item.FindControl("HyperNave"), HyperLink)
        Dim tariffapromo As Label = CType(e.Item.FindControl("tariffapromo"), Label)
        Dim scontoi As Label = CType(e.Item.FindControl("scontoi"), Label)
        Dim scontoe As Label = CType(e.Item.FindControl("scontoe"), Label)
        Dim scontob As Label = CType(e.Item.FindControl("scontob"), Label)
        Dim evidenzia As Label = CType(e.Item.FindControl("evidenzia"), Label)
        Dim tariffaunica As Label = CType(e.Item.FindControl("tariffaunica"), Label)
        Dim compagnia As Label = CType(e.Item.FindControl("compagnia"), Label)
        Dim durata As Label = CType(e.Item.FindControl("durata"), Label)
        Dim duratavedi As Label = CType(e.Item.FindControl("duratavedi"), Label)
        Dim chiama As Label = CType(e.Item.FindControl("chiama"), Label)
        Dim idpromo As Label = CType(e.Item.FindControl("idpromo"), Label)
        'Dim imagepromo As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("ImagePromo"), System.Web.UI.WebControls.Image)
        'Dim imagesconto As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("ImageSconto"), System.Web.UI.WebControls.Image)
        Dim Logocompa As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("Logocompa"), System.Web.UI.WebControls.Image)
        Dim imagescu As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("Imagescu"), System.Web.UI.WebControls.Image)
        Dim imagesc1 As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("Imagesc1"), System.Web.UI.WebControls.Image)
        Dim imagesc2 As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("Imagesc2"), System.Web.UI.WebControls.Image)
        Dim imagesc3 As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("Imagesc3"), System.Web.UI.WebControls.Image)
        Dim promozione As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("promozione"), System.Web.UI.WebControls.Image)
        Dim fotop As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("fotop"), System.Web.UI.WebControls.Image)
        Dim chiamaspeciale As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("chiamaspeciale"), System.Web.UI.WebControls.Image)
        Dim party As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("party"), System.Web.UI.WebControls.Image)
        Dim offertalimite As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("offertalimite"), System.Web.UI.WebControls.Image)
        ' Dim panelsconto As Panel = CType(e.Item.FindControl("panelsconto"), Panel)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If CInt(zona.Text) > 0 Then
                titolo.Text = nomezona(CInt(zona.Text) - 1) & ": "
            End If
            If idpromo.Text > 0 Then
                promozione.ImageUrl = promo(idpromo.Text)
                promozione.Visible = True
            End If
            Dim dal As Date
            Dim al As Date
            If compagnia.Text = 0 Then
                hyperpreno.NavigateUrl = "offerte-crociere.aspx?id=" & id_periodo.Text
                Logocompa.ImageUrl = "../images/lgmsc.gif"
            End If
            If compagnia.Text = 1 Then
                Logocompa.ImageUrl = "../images/lgcosta.gif"
                hyperpreno.NavigateUrl = "offerte-crociere.aspx?id=" & id_periodo.Text
            End If
            hypernave.NavigateUrl = "offerte-crociere.aspx?id=" & id_periodo.Text
            If compagnia.Text = 2 Then Logocompa.ImageUrl = "../images/lgroyal.gif"
            dal = CDate(dalc.Text.ToString)
            al = CDate(alc.Text.ToString)
            Dim imbarcoapp As String = imbarco.Text
            Dim sbarcoapp As String = sbarco.Text
            Dim xx As Integer = 0
            dala.Text = giornoArray(dal.DayOfWeek) & " " & dal.Day & " " & meseArray(dal.Month - 1) & " " & dal.Year
            ala.Text = giornoArray(al.DayOfWeek) & " " & al.Day & " " & meseArray(al.Month - 1) & " " & al.Year
            duratavedi.Text = durata.Text & " notti"
            dalimbarco.Text = imbarcoapp
            alsbarco.Text = sbarcoapp
            If CInt(id_nave2.Text) > 0 Then
                hypernave.Text = nave2(CInt(id_nave.Text)) & " / " & Replace(nave2(CInt(id_nave2.Text)), "COSTA", "")
                roulette = roulette & "roulette[" & contatore & "]='" & fotop.ClientID & "';"
                If dalc2.Text <> "" Then
                    dala.Text = dal.Day & " " & meseArray(dal.Month - 1) & " o " & CDate(dalc2.Text).Day & " " & meseArray(CDate(dalc2.Text).Month - 1)
                End If
                If alc2.Text <> "" Then
                    ala.Text = al.Day & " " & meseArray(al.Month - 1) & " o " & CDate(alc2.Text).Day & " " & meseArray(CDate(alc2.Text).Month - 1)
                End If

            End If
            If prezzoi.Text > 0 Then
                If interoi.Text = prezzoi.Text Then interoi.Text = CInt(interoi.Text) + 50
                interoi.Text = Format(CInt(interoi.Text), "##,##0.00")
                prezzoi.Text = Format(ccprezzo(prezzoi.Text, compagnia.Text, durata.Text), "##,##0.00")
            Else
                prezzoi.Style.Add("margin", "20px 0 0 20px;")
                prezzoi.Text = "n.d."
                interoi.Visible = False
                prezzoi.Style.Add("font-size", "small")
            End If
            If prezzoe.Text > 0 Then
                If interoe.Text = prezzoe.Text Then interoe.Text = CInt(interoe.Text) + 50
                interoe.Text = Format(CInt(interoe.Text), "##,##0.00")
                prezzoe.Text = Format(ccprezzo(prezzoe.Text, compagnia.Text, durata.Text), "##,##0.00")
            Else
                prezzoe.Style.Add("margin", "20px 0 0 20px;")
                prezzoe.Text = "n.d."
                interoe.Visible = False
                prezzoe.Style.Add("font-size", "small")
            End If
            If prezzob.Text > 0 Then
                If interob.Text = prezzob.Text Then interob.Text = CInt(interob.Text) + 50
                interob.Text = Format(CInt(interob.Text), "##,##0.00")
                prezzob.Text = Format(ccprezzo(prezzob.Text, compagnia.Text, durata.Text), "##,##0.00")
            Else
                prezzob.Style.Add("margin", "20px 0 0 20px;")
                prezzob.Text = "n.d."
                interob.Visible = False
                prezzob.Style.Add("font-size", "small")
            End If
            If prezzos.Text > 0 Then
                If interos.Text = prezzos.Text Then interos.Text = CInt(interos.Text) + 50
                interos.Text = Format(CInt(interos.Text), "##,##0.00")
                prezzos.Text = Format(ccprezzo(prezzos.Text, compagnia.Text, durata.Text), "##,##0.00")
            Else
                prezzos.Style.Add("margin", "20px 0 0 20px;")
                prezzos.Text = "n.d."
                interos.Visible = False
                prezzos.Style.Add("font-size", "small")
            End If
            If chiama.Text = 1 Then
                chiamaspeciale.Visible = True
                offertachiama = offertachiama & "offertachiama[" & contatore & "]='" & chiamaspeciale.ClientID & "';"
            End If
            If id_periodo.Text = 4222 Then
                party.Visible = True
                offertachiama = offertachiama & "offertachiama[" & contatore & "]='" & party.ClientID & "';"
            End If
            If Request.Params("parf") = "0013445" Then
                offertachiama = ""
                promozione.ImageUrl = "../images/promo-riservata.gif"
                promozione.Visible = True
                hyperpreno.NavigateUrl = hyperpreno.NavigateUrl & "&parf=0013445"
            End If
            contatore = contatore + 1
        End If
    End Sub



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

    Function ccprezzo(ByVal prezzo As Integer, ByVal compagnia As Integer, ByVal durata As Integer) As Integer
        ccprezzo = prezzo
        If compagnia = 1 Then
            Select Case durata
                Case 1 To 5
                    ccprezzo = prezzo - 80
                Case 6
                    ccprezzo = prezzo - 90
                Case 7
                    ccprezzo = prezzo - 120
                Case 8 To 9
                    ccprezzo = prezzo - 125
                Case 10 To 11
                    ccprezzo = prezzo - 135
                Case 12 To 30
                    ccprezzo = prezzo - 165
            End Select
        End If
    End Function

    Function controllacampo(ByVal dr As MySqlDataReader, ByVal campo As Object) As Object
        If Not IsDBNull(dr(campo)) Then
            controllacampo = dr(campo)
        Else
            controllacampo = ""
        End If
    End Function

    Protected Sub RepeaterPagine2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPagine2.ItemDataBound
        Dim linkato As System.Web.UI.HtmlControls.HtmlAnchor = CType(e.Item.FindControl("linkato"), System.Web.UI.HtmlControls.HtmlAnchor)
        Dim lipag As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("lipag"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim illink As Label = CType(e.Item.FindControl("Labellink"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim n As Integer = 0
            If IsNumeric(Request.Params("n")) Then
                n = Request.Params("n")
            End If
            If ((n / perpagina) + 1) = (illink.Text) Then
                illink.Style.Add("font-weight", "bold")
                illink.Style.Add("color", "#FF0000")
                lipag.Style.Add("background", "url(../images/ttpaginasel.gif) no-repeat")
            Else
                illink.Style.Add("color", "#000000")
                lipag.Style.Add("cursor", "pointer")
                lipag.Attributes.Add("onclick", "location.href='" & linkato.HRef.ToString & "'")
            End If
        End If
    End Sub

    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Protected Sub RepeaterUltime_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterUltime.ItemDataBound
        Dim HyperTitolo As HyperLink = CType(e.Item.FindControl("HyperTitolo"), HyperLink)
        Dim da As Label = CType(e.Item.FindControl("da"), Label)
        Dim tipocab As Label = CType(e.Item.FindControl("tipocab"), Label)
        Dim adulti As Label = CType(e.Item.FindControl("adulti"), Label)
        Dim bambini As Label = CType(e.Item.FindControl("bambini"), Label)
        Dim id_periodo As Label = CType(e.Item.FindControl("id_periodo"), Label)
        Dim prezzocab As Label = CType(e.Item.FindControl("prezzocab"), Label)
        Dim titolo As Label = CType(e.Item.FindControl("titolo"), Label)
        Dim dal As Label = CType(e.Item.FindControl("dal"), Label)
        Dim imbarco As Label = CType(e.Item.FindControl("imbarco"), Label)
        Dim tipocabina As Label = CType(e.Item.FindControl("tipocabina"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim pax As Integer = 0
            pax = CInt(adulti.Text) + CInt(bambini.Text)
            tipocab.Text = tipocab.Text & "<b style='font-size:small'>" & Format(CInt(prezzocab.Text), "##,##0.00") & "</b> "
            Select Case pax
                Case 1
                    tipocab.Text = tipocab.Text & "cabina singola"
                Case 2
                    tipocab.Text = tipocab.Text & "cabina doppia"
                Case 3
                    tipocab.Text = tipocab.Text & "cabina tripla"
                Case 4
                    tipocab.Text = tipocab.Text & "cabina quadrupla"
            End Select
            Select Case tipocabina.Text
                Case 2
                    tipocab.Text = tipocab.Text & " interna"
                Case 1
                    tipocab.Text = tipocab.Text & " esterna"
                Case 0
                    tipocab.Text = tipocab.Text & " balcone"
                Case 4
                    tipocab.Text = tipocab.Text & " suite"
            End Select

            HyperTitolo.Text = titolo.Text & " " & Format(CDate(dal.Text), "dd/MM/yy")
            HyperTitolo.NavigateUrl = "offerte-crociere.aspx?id=" & id_periodo.Text
            da.Text = "da " & imbarco.Text
        End If
    End Sub
End Class
