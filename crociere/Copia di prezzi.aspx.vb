Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Xml
Imports System.Web.Services.Protocols
Imports System.IO
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Partial Class crociere_prezzi
    Inherits System.Web.UI.Page
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim primo As Boolean = True
    Dim contaiti As Integer = 0
    Dim contatore As Integer = 0
    Dim rigona As String = ""
    Dim divone As String = ""
    Dim imapiu As String = ""
    Dim testone As String = ""
    Dim prezzone As String = ""
    Dim prezzone2 As String = ""
    Dim prezzone3 As String = ""
    Dim prezzocolonna1 As String = ""
    Dim prezzocolonna2 As String = ""
    Dim prezzocolonna3 As String = ""
    Dim appoggio1 As String = ""
    Dim appoggio2 As String = ""
    Dim appoggio3 As String = ""
    Dim manina1 As String = ""
    Dim manina2 As String = ""
    Dim manina3 As String = ""
    Dim conta1 As Integer = 0
    Dim conta2 As Integer = 0
    Dim conta3 As Integer = 0
    Dim contatoreriga As Integer = 1
    Dim contarighe As Integer = 0
    Dim tasse As Integer = 0
    Dim id_itinerario As Integer
    Dim dscat As New DsCosta
    Dim tutte As Boolean
    Dim tutte2 As Boolean
    Dim tutte3 As Boolean
    Dim primocolore As String = "#F1F1F1"
    Dim secondocolore As String = "#F6F6F6"
    Dim nomecabina As String = ""
    Dim dal1 As String = ""
    Dim dal2 As String = ""
    Dim dal3 As String = ""
    Dim co As Integer = 0
    Dim durata As String = ""
    Dim volo As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then            
            Dim codiceperiodo As String = Request.Params("id")
            Dim sqlconn As String
            sqlconn = "SELECT * from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' "
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                co = dr("compagnia")
                durata = dr("durata")
                volo = dr("volo")
            End If
            dr.Close()
            cn.Close()

            ricavadal(codiceperiodo, durata)
            Dim imbarco As String
            imbarco = cercaporto(codiceperiodo, "si")
            tasse = cctasse(durata)
            Dim arraypax(5) As String
            arraypax(1) = Request.Params("eta1")
            arraypax(2) = Request.Params("eta2")
            arraypax(3) = Request.Params("eta3")
            arraypax(4) = Request.Params("eta4")
            arraypax(5) = Request.Params("eta5")
            If co = 1 Then
                'Call caricacostacategorie(codiceperiodo, imbarco, dscat, Request.Params("persone"), arraypax)
            End If
            Call caricacategorie(codiceperiodo, Repeatercategorie, co) '0 è msc 1 è costa
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "iniaz('" & primocolore & "','" & secondocolore & "');" & rigona & divone & imapiu & testone & prezzone & prezzone2 & prezzone3 & prezzocolonna1 & prezzocolonna2 & prezzocolonna3 & manina1 & manina2 & manina3 & appoggio1 & appoggio2 & appoggio3 & "", True)
        End If
    End Sub

    Function cercaporto(ByVal codiceperiodo As String, ByVal tipo As String) As String
        cercaporto = ""
        Dim sqlconn As String
        sqlconn = "SELECT * from periodo WHERE codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If Not IsDBNull(dr(tipo)) Then cercaporto = dr(tipo)
        End If
        dr.Close()
        cn.Close()
    End Function

    Protected Sub Repeatercategorie_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeatercategorie.ItemCreated

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Repeaterprezzi As Repeater = CType(e.Item.FindControl("Repeaterprezzi"), Repeater)
            AddHandler Repeaterprezzi.ItemDataBound, AddressOf Repeaterprezzi_ItemDataBound
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Repeaterprezziprima As Repeater = CType(e.Item.FindControl("Repeaterprezziprima"), Repeater)
            AddHandler Repeaterprezziprima.ItemDataBound, AddressOf Repeaterprezzi_ItemDataBound
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim Repeaterprezzidopo As Repeater = CType(e.Item.FindControl("Repeaterprezzidopo"), Repeater)
            AddHandler Repeaterprezzidopo.ItemDataBound, AddressOf Repeaterprezzi_ItemDataBound
        End If
    End Sub
    Protected Sub Repeaterprezzi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs)
        Dim LabelPrezzo As Label = CType(e.Item.FindControl("LabelPrezzo"), Label)
        Dim LabelTipo As Label = CType(e.Item.FindControl("LabelTipo"), Label)
        Dim Labelpr As Label = CType(e.Item.FindControl("Labelpr"), Label)        
        Dim Labeldisponibile As Label = CType(e.Item.FindControl("Labeldisponibile"), Label)        
        Dim LabelCategoria As Label = CType(e.Item.FindControl("Labelcategoria"), Label)
        Dim codiceperiodo As Label = CType(e.Item.FindControl("codiceperiodo"), Label)
        Dim conta As Label = CType(e.Item.FindControl("conta"), Label)
        Dim controllo As Label = CType(e.Item.FindControl("controllo"), Label)
        Dim indica As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("indica"), System.Web.UI.HtmlControls.HtmlImage)
        Dim rigaprezzi As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("rigaprezzi"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim sconto As Integer
            sconto = 0
            LabelTipo.Text = Replace(LabelTipo.Text, "<br>", " ")          
            Dim ddispo As Boolean = True
            'If Request.Params("co") = 0 Then
            LabelPrezzo.Text = Format(CInt(Labelpr.Text), "##,##0.00")
            If Labeldisponibile.Text = 1 Then
                ddispo = False
            Else
                If controllo.Text = 0 Then tutte = False
                If controllo.Text = 1 Then tutte2 = False
                If controllo.Text = 2 Then tutte3 = False
            End If
            If controllo.Text = 0 Then
                prezzocolonna1 = prezzocolonna1 & "prezzocolonna1[" & conta1 & "]='" & LabelPrezzo.ClientID & "';"
                manina1 = manina1 & "manina1[" & conta1 & "]='" & indica.ClientID & "';"
            End If
            If controllo.Text = 1 Then
                prezzocolonna2 = prezzocolonna2 & "prezzocolonna2[" & conta2 & "]='" & LabelPrezzo.ClientID & "';"
                manina2 = manina2 & "manina2[" & conta2 & "]='" & indica.ClientID & "';"
            End If
            If controllo.Text = 2 Then
                prezzocolonna3 = prezzocolonna3 & "prezzocolonna3[" & conta3 & "]='" & LabelPrezzo.ClientID & "';"
                manina3 = manina3 & "manina3[" & conta3 & "]='" & indica.ClientID & "';"
            End If
            If co = 1 Then
                LabelPrezzo.Text = Format(CInt(Labelpr.Text - tasse), "##,##0.00")
            End If
            '    If verificacategorie(LabelCategoria.Text) = False Then
            '        ddispo = False
            '    Else
            '        tutte = False
            '    End If
            'End If
            If ddispo = False Then
                LabelPrezzo.Enabled = False
                LabelPrezzo.ForeColor = Color.LightGray
            Else
                Dim stringacostap As String = "cabinecosta.aspx"
                'LabelPrezzo.Attributes.Add("onclick", "javascript:caricacab('cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & rigaprezzi.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "')")                
                'LabelPrezzo.Attributes.Add("onclick", "javascript:caricacab('" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & indica.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "')")
                If controllo.Text = 0 Then                    
                    'appoggio1 = appoggio1 & "appoggio1[" & conta1 & "]=""" & "'" & dal1 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & indica.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "'"";"
                    appoggio1 = appoggio1 & "appoggio1[" & conta1 & "]=""" & "'" & dal1 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "'"";"
                End If
                If controllo.Text = 1 Then                    
                    appoggio2 = appoggio2 & "appoggio2[" & conta2 & "]=""" & "'" & dal2 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "'"";"
                End If
                If controllo.Text = 2 Then
                    appoggio3 = appoggio3 & "appoggio3[" & conta3 & "]=""" & "'" & dal3 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & Request.Params("persone") & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & Request.Params("volo") & "', '" & Request.Params("volo") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "'"";"
                End If
            End If
            If controllo.Text = 0 Then conta1 = conta1 + 1
            If controllo.Text = 1 Then conta2 = conta2 + 1
            If controllo.Text = 2 Then conta3 = conta3 + 1
            contarighe = contarighe + CInt(conta.Text)
        End If
    End Sub

    Function verificacategorie(ByVal categoria As String) As Boolean
        verificacategorie = False
        Dim dt As DataTable = dscat.Tables("Categorie")
        Dim dv As New DataView(dt)
        dv.RowFilter = "categoria = '" & categoria & "'"
        Dim dtn As New DataTable
        dtn = dv.ToTable
        For Each dr As DataRow In dtn.Rows
            If dr("categoria") = categoria Then
                If dr("StatusCode") = "AV" Or dr("StatusCode") = "GA" Then
                    verificacategorie = True
                End If
            End If
        Next
    End Function

    Protected Sub Repeatercategorie_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeatercategorie.ItemDataBound
        Dim Repeaterprezzi As Repeater = CType(e.Item.FindControl("Repeaterprezzi"), Repeater)
        Dim Repeaterprezziprima As Repeater = CType(e.Item.FindControl("Repeaterprezziprima"), Repeater)
        Dim Repeaterprezzidopo As Repeater = CType(e.Item.FindControl("Repeaterprezzidopo"), Repeater)
        Dim categoria As Label = CType(e.Item.FindControl("categoria"), Label)
        Dim descri As Label = CType(e.Item.FindControl("descri"), Label)
        Dim pr As Label = CType(e.Item.FindControl("pr"), Label)
        Dim Labelprezzo As Label = CType(e.Item.FindControl("Labelprezzo"), Label)
        Dim Labelprezzo2 As Label = CType(e.Item.FindControl("Labelprezzoprima"), Label)
        Dim Labelprezzo3 As Label = CType(e.Item.FindControl("Labelprezzodopo"), Label)
        Dim LabelRQ As Label = CType(e.Item.FindControl("LabelRq"), Label)
        Dim codiceperiodo As Label = CType(e.Item.FindControl("codiceperiodo"), Label)
        Dim HyperRQ As HyperLink = CType(e.Item.FindControl("HyperRQ"), HyperLink)
        Dim HyperRQ2 As HyperLink = CType(e.Item.FindControl("HyperRQ2"), HyperLink)
        Dim HyperRQ3 As HyperLink = CType(e.Item.FindControl("HyperRQ3"), HyperLink)
        Dim ImageDetail As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("ImageDetail"), System.Web.UI.WebControls.Image)
        Dim divdescri As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("divdescri"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim rigarq As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("rigarq"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim rigarq2 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("rigarq2"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim rigarq3 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("rigarq3"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If e.Item.ItemType = ListItemType.Header Then
            riga.Style.Add("background", secondocolore)
        End If
        If e.Item.ItemType = ListItemType.AlternatingItem Then
            riga.Style.Add("background", secondocolore)
        End If
        If e.Item.ItemType = ListItemType.Item Then
            riga.Style.Add("background", primocolore)
        End If
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            nomecabina = descri.Text
            Dim codcat As String = Left(categoria.Text, 1)
            Dim misura As Integer
            Dim addmisura As Integer = 0
            Dim primaperiodo As String = ricavaperiodo(codiceperiodo.Text, True)
            Dim dopoperiodo As String = ricavaperiodo(codiceperiodo.Text, False)
            Dim epr As Integer = 0
            Dim epr2 As Integer = 0
            Dim epr3 As Integer = 0
            tutte = True
            tutte2 = True
            tutte3 = True
            Dim ceprima As Boolean = True
            If (DateDiff(DateInterval.Day, Date.Now, DateAdd(DateInterval.Day, -CInt(Request.Params("durata")), CDate(Request.Params("dal")))) < 1) Then
                primaperiodo = ""
            End If
            If Request.Params("co") = 0 Then 'Msc
                Select Case codcat
                    Case "I"
                        epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND tipocabina >= 1 and tipocabina <= 2 ORDER BY prezzo")
                        epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND tipocabina >= 1 and tipocabina <= 2 ORDER BY prezzo")
                        epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND tipocabina >= 1 and tipocabina <= 2 ORDER BY prezzo")
                    Case "E"
                        epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND tipocabina >= 3 and tipocabina <= 4 ORDER BY prezzo")
                        epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND tipocabina >= 3 and tipocabina <= 4 ORDER BY prezzo")
                        epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND tipocabina >= 3 and tipocabina <= 4 ORDER BY prezzo")
                    Case "B"
                        epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND tipocabina >= 5 and tipocabina <= 6 ORDER BY prezzo")
                        epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND tipocabina >= 5 and tipocabina <= 6 ORDER BY prezzo")
                        epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND tipocabina >= 5 and tipocabina <= 6 ORDER BY prezzo")
                    Case "S"
                        epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND tipocabina >= 7 and tipocabina <= 8 ORDER BY prezzo")
                        epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND tipocabina >= 7 and tipocabina <= 8 ORDER BY prezzo")
                        epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND tipocabina >= 7 and tipocabina <= 8 ORDER BY prezzo")
                End Select
            ElseIf Request.Params("co") = 1 Then 'Costa
                If codcat = "S" Then
                    epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND (categoria like 'S%' or categoria like 'W%' or categoria like 'M%' or categoria like 'P%' or categoria like 'G%')  ORDER BY prezzo")
                    epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND (categoria like 'S%' or categoria like 'W%' or categoria like 'M%' or categoria like 'P%' or categoria like 'G%')  ORDER BY prezzo")
                    epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND (categoria like 'S%' or categoria like 'W%' or categoria like 'M%' or categoria like 'P%' or categoria like 'G%')  ORDER BY prezzo")
                Else
                    epr = esponiprezzo(codiceperiodo.Text, Repeaterprezzi, " AND categoria like '" & codcat & "%' ORDER BY prezzo")
                    epr2 = esponiprezzo(primaperiodo, Repeaterprezziprima, " AND categoria like '" & codcat & "%' ORDER BY prezzo")
                    epr3 = esponiprezzo(dopoperiodo, Repeaterprezzidopo, " AND categoria like '" & codcat & "%' ORDER BY prezzo")
                End If
            End If
            If primaperiodo = "" Then
                Repeaterprezziprima.Visible = False
            Else
                Labelprezzo2.Text = restituisciprezzo(epr2, tasse, Request.Params("co"))
            End If
            If dopoperiodo = "" Then
                Repeaterprezzidopo.Visible = False
            Else
                Labelprezzo3.Text = restituisciprezzo(epr3, tasse, Request.Params("co"))
            End If
            Labelprezzo.Text = restituisciprezzo(epr, tasse, Request.Params("co"))
            Dim sirq As Boolean = False
            If tutte = True Then
                rigarq.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ.Style.Add("cursor", "pointer")
                HyperRQ.Style.Add("text-decoration", "underline")
                HyperRQ.Attributes.Add("onclick", "javascript:caricarq('vcabi', 'costarq.aspx','" & codiceperiodo.Text & "', '" & categoria.Text & "', '" & Request.Params("volo") & "', '" & Request.Params("persone") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "')")
            End If
            If tutte2 = True And Labelprezzo2.Text <> "" Then
                rigarq2.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ2.Style.Add("cursor", "pointer")
                HyperRQ2.Style.Add("text-decoration", "underline")
                HyperRQ2.Attributes.Add("onclick", "javascript:caricarq('vcabi', 'costarq.aspx','" & codiceperiodo.Text & "', '" & categoria.Text & "', '" & Request.Params("volo") & "', '" & Request.Params("persone") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "')")
            End If
            If tutte3 = True And Labelprezzo3.Text <> "" Then
                rigarq3.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ3.Style.Add("cursor", "pointer")
                HyperRQ3.Style.Add("text-decoration", "underline")
                HyperRQ3.Attributes.Add("onclick", "javascript:caricarq('vcabi', 'costarq.aspx','" & codiceperiodo.Text & "', '" & categoria.Text & "', '" & Request.Params("volo") & "', '" & Request.Params("persone") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "')")
            End If
            If sirq Then
                addmisura = addmisura + 50
            End If
            misura = 55 + (contarighe * 31) + addmisura
            Dim cl As String = riga.Style.Item("background")
            Dim stringa As String = "vedisotto('" & divdescri.ClientID & "','" & riga.ClientID & "', '" & misura & "', '" & cl & "', '" & ImageDetail.ClientID & "', '" & descri.ClientID & "', $$);"
            ImageDetail.Attributes.Add("onclick", Replace(stringa, "$$", "'" & Labelprezzo.ClientID & "','1'"))
            descri.Attributes.Add("onclick", Replace(stringa, "$$", "'" & Labelprezzo.ClientID & "','1'"))
            descri.Style.Add("cursor", "pointer")
            Labelprezzo.Attributes.Add("onclick", Replace(stringa, "$$", "'" & Labelprezzo.ClientID & "','1'"))
            Labelprezzo.Style.Add("cursor", "pointer")
            Labelprezzo2.Attributes.Add("onclick", Replace(stringa, "$$", "'" & Labelprezzo2.ClientID & "','2'"))
            Labelprezzo2.Style.Add("cursor", "pointer")
            Labelprezzo3.Attributes.Add("onclick", Replace(stringa, "$$", "'" & Labelprezzo3.ClientID & "','3'"))
            Labelprezzo3.Style.Add("cursor", "pointer")
            rigona = rigona & "rigona[" & contatoreriga & "]='" & riga.ClientID & "';"
            divone = divone & "divone[" & contatoreriga & "]='" & divdescri.ClientID & "';"
            imapiu = imapiu & "imapiu[" & contatoreriga & "]='" & ImageDetail.ClientID & "';"
            testone = testone & "testone[" & contatoreriga & "]='" & descri.ClientID & "';"
            prezzone = prezzone & "prezzone[" & contatoreriga & "]='" & Labelprezzo.ClientID & "';"
            prezzone2 = prezzone2 & "prezzone2[" & contatoreriga & "]='" & Labelprezzo2.ClientID & "';"
            prezzone3 = prezzone3 & "prezzone3[" & contatoreriga & "]='" & Labelprezzo3.ClientID & "';"
            contatoreriga = contatoreriga + 1
            contarighe = 0
        End If
        If e.Item.ItemType = ListItemType.Header Then
            Dim eti2 As Label = CType(e.Item.FindControl("eti2"), Label)
            eti2.Text = Format(CDate(Request.Params("dal")), "dd/MM/yy")            
        End If
        If e.Item.ItemType = ListItemType.Header Then
            Dim eti3 As Label = CType(e.Item.FindControl("eti3"), Label)
            eti3.Text = Format(DateAdd(DateInterval.Day, -CInt(Request.Params("durata")), CDate(Request.Params("dal"))), "dd/MM/yy")            
        End If
        If e.Item.ItemType = ListItemType.Header Then
            Dim eti4 As Label = CType(e.Item.FindControl("eti4"), Label)
            eti4.Text = Format(DateAdd(DateInterval.Day, CInt(Request.Params("durata")), CDate(Request.Params("dal"))), "dd/MM/yy")
        End If

    End Sub

    Function restituisciprezzo(ByVal prezzo As Integer, ByVal tasse As Integer, ByVal compagnia As Integer) As String
        restituisciprezzo = "n.d."
        If prezzo > 0 Then
            If compagnia = 0 Then
                restituisciprezzo = Format(prezzo, "##,##0.00")
            ElseIf compagnia = 1 Then
                restituisciprezzo = Format(prezzo - CInt(tasse), "##,##0.00")
            End If
        End If
    End Function

    Private Sub caricacategorie(ByVal codiceperiodo As String, ByVal rpt As Repeater, ByVal compagnia As Integer)
        Dim ds As New DsCosta
        If compagnia = 0 Then 'msc
            Call caricacat(codiceperiodo, ds, "I", "Cabina Interna", compagnia, 1, 2)
            Call caricacat(codiceperiodo, ds, "E", "Cabina Esterna Finestra", compagnia, 3, 4)
            Call caricacat(codiceperiodo, ds, "B", "Cabina con Balcone", compagnia, 5, 6)
            Call caricacat(codiceperiodo, ds, "S", "Suite", compagnia, 7, 8)
        ElseIf compagnia = 1 Then 'costa
            Call caricacat(codiceperiodo, ds, "I", "Cabina Interna", compagnia, 0, 0)
            Call caricacat(codiceperiodo, ds, "E", "Cabina Esterna Finestra", compagnia, 0, 0)
            Call caricacat(codiceperiodo, ds, "B", "Cabina con Balcone", compagnia, 0, 0)
            Call caricacat(codiceperiodo, ds, "S", "Suite", compagnia, 0, 0)
        End If

        rpt.DataSource = ds.Tables("Categorie").DefaultView
        rpt.DataBind()        
    End Sub

    Private Sub caricacat(ByVal codiceperiodo As String, ByVal ds As DsCosta, ByVal cat As String, ByVal descrizione As String, ByVal compagnia As Integer, ByVal tipomin As Integer, ByVal tipomax As Integer)
        Dim sqlconn As String = ""
        If compagnia = 0 Then 'msc
            sqlconn = "SELECT codiceperiodo, '" & cat & "' As categoria, disponibile, listino, prezzo, '" & descrizione & "' As descrizione from prezzi WHERE disponibile = 1 AND codiceperiodo = '" & codiceperiodo & "' and tipocabina >= " & tipomin & " and tipocabina <= " & tipomax & " order by prezzo LIMIT 1"
        ElseIf compagnia = 1 Then 'costa
            sqlconn = "SELECT codiceperiodo, '" & cat & "' As categoria, disponibile, listino, prezzo, '" & descrizione & "' As descrizione from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' and categoria like '" & cat & "%' order by prezzo LIMIT 1"
        End If
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim da As New MySqlDataAdapter(sqlconn, cn)
        da.Fill(ds, "Categorie")
        cn.Close()
    End Sub

    Private Sub caricaprezzi(ByVal codiceperiodo As String, ByVal rpt As Repeater, ByVal filtro As String)
        Dim sqlconn As String
        sqlconn = "SELECT * from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' " & filtro
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            rpt.DataSource = dr
            rpt.DataBind()
        End If
        dr.Close()
        cn.Close()
    End Sub

    Function esponiprezzo(ByVal codiceperiodo As String, ByVal rpt As Repeater, ByVal filtro As String) As Integer
        esponiprezzo = 0
        Dim sqlconn As String
        sqlconn = "SELECT min(prezzo) as pr from prezzi WHERE disponibile = 0 AND codiceperiodo = '" & codiceperiodo & "' " & filtro
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If Not IsDBNull(dr("pr")) Then
                esponiprezzo = dr("pr")
            End If
        End If
        dr.Close()
        cn.Close()
        caricaprezzi(codiceperiodo, rpt, filtro)
    End Function

    Function calcoloassiestate(ByVal prezzo As Integer) As Integer
        calcoloassiestate = 0
        If Request.Params("co") = 0 Then
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
        ElseIf Request.Params("co") = 1 Then
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

    Function cctasse(ByVal durata As Integer) As Integer
        cctasse = 0
        Select Case durata
            Case 1 To 5
                cctasse = 80
            Case 6
                cctasse = 90
            Case 7
                cctasse = 120
            Case 8 To 9
                cctasse = 125
            Case 10 To 11
                cctasse = 135
            Case 12 To 30
                cctasse = 165
        End Select
    End Function


    Private Sub caricacostacategorie(ByVal codiceperiodo As String, ByVal imbarco As String, ByVal ds As DsCosta, ByVal persone As Integer, ByVal arraypax() As String)
        Dim ws As New net.costaclick.web.Availability
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
        'cat.Code = "I1"
        far.Code = "IND"
        dispo.Category = cat
        dispo.Code = codiceperiodo
        dispo.Type = net.costaclick.web.ComponentType.Cruise
        dispo.Fare = far
        Dim compo(0) As net.costaclick.web.Component
        compo(0) = dispo
        If Request.Params("volo") <> "" Then
            Dim dispo2 As New net.costaclick.web.Component
            dispo2.Type = net.costaclick.web.ComponentType.Flight
            dispo2.Code = Request.Params("volo")
            Dim city As New net.costaclick.web.City
            city.Code = Request.Params("volo")
            If Request.Params("direzione") = 1 Then
                dispo2.Direction = net.costaclick.web.Direction.Both
            ElseIf Request.Params("direzione") = 2 Then
                dispo2.Direction = net.costaclick.web.Direction.OutBound
            ElseIf Request.Params("direzione") = 3 Then
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
        For i = 1 To persone
            Dim guest As New net.costaclick.web.Guest
            guest.GuestType = ricavatipoguest(arraypax(i))
            pass(i - 1) = guest
            If i = 1 Then
                If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                    guest.CostaClubNumber = Request.Params("cartaclub")
                    guest.LastName = Request.Params("nomeclub")
                End If
            End If
        Next
        Try
            GridCat.DataSource = ws.ListAvailableCategories(compo, pass)
            GridCat.DataBind()
            Dim dt As DataTable = ds.Tables("categorie")
            Dim oItem As GridViewRow
            For Each oItem In GridCat.Rows
                Dim code As Label = CType(oItem.FindControl("Code"), Label)
                Dim StatusCode As Label = CType(oItem.FindControl("StatusCode"), Label)
                If Not IsDBNull(code.Text) Then
                    Dim dr As DataRow = dt.NewRow
                    dr("categoria") = code.Text
                    dr("statuscode") = StatusCode.Text
                    dt.Rows.Add(dr)
                End If
            Next oItem
        Catch ex As Exception
            Labelnonesiste.Visible = True
            Labelnonesiste.Text = ex.Message.ToString
            PanelCategorie.Visible = False
        End Try
    End Sub

    Function ricavatipoguest(ByVal eta As Integer) As net.costaclick.web.GuestType
        ricavatipoguest = net.costaclick.web.GuestType.Child
        If eta >= 18 Then
            ricavatipoguest = net.costaclick.web.GuestType.Adult
        ElseIf eta < 18 And eta >= 12 Then
            ricavatipoguest = net.costaclick.web.GuestType.Junior
        ElseIf eta < 12 And eta >= 2 Then
            ricavatipoguest = net.costaclick.web.GuestType.Child
        ElseIf eta < 2 Then
            ricavatipoguest = net.costaclick.web.GuestType.Infant
        End If
    End Function

    Private Sub ricavadal(ByVal codiceperiodo As String, ByVal durata As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT dal FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            dal1 = dr("dal")
            dal2 = DateAdd(DateInterval.Day, -durata, CDate(dr("dal")))
            dal3 = DateAdd(DateInterval.Day, durata, CDate(dr("dal")))
        End If
        dr.Close()
        cn.Close()
    End Sub

    Function ricavaperiodo(ByVal codiceperiodo As String, ByVal prima As Boolean) As String
        ricavaperiodo = ""
        Dim sqlconn As String
        sqlconn = "SELECT id_itinerario, durata, dal FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dal As Date
        Dim iditi As Integer = 0
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If prima = True Then
                dal = DateAdd(DateInterval.Day, -dr("durata"), CDate(dr("dal")))
            Else
                dal = DateAdd(DateInterval.Day, dr("durata"), CDate(dr("dal")))
            End If
            iditi = dr("id_itinerario")
        End If
        dr.Close()
        sqlconn = "SELECT codiceperiodo FROM periodo where id_itinerario = '" & iditi & "' AND dal = '" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "'"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
            dr2.Read()
            ricavaperiodo = dr2("codiceperiodo")
        End If
        dr2.Close()
        cn.Close()
    End Function
    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function
End Class
