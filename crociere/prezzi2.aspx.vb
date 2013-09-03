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
    Dim durata As Integer = 0
    Dim volo As Integer = 0
    Dim voloobbligatorio As Integer = 0
    Dim daltot As String = ""
    Dim altezza As Integer
    Dim dalprima As String = ""
    Dim daldopo As String = ""
    Dim primaperiodo As String
    Dim dopoperiodo As String
    Dim prezziperiodo(11) As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            Dim codiceperiodo As String = Request.Params("id")
            Dim prima As String() = ricavaperiodo(codiceperiodo, True)
            Dim dopo As String() = ricavaperiodo(codiceperiodo, False)            
            primaperiodo = prima(0)
            dopoperiodo = dopo(0)
            Dim parf As Boolean = False
            If Request.Params("parf") = "0013445" Then
                parf = True
            End If
            prezziperiodo = esponiprezzo2(primaperiodo, codiceperiodo, dopoperiodo, parf)
            dalprima = prima(1)
            daldopo = dopo(1)
            Dim sqlconn As String
            sqlconn = "SELECT * from periodo WHERE codiceperiodo = '" & codiceperiodo & "' "
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                co = dr("compagnia")
                durata = dr("durata")
                daltot = dr("dal")
                volo = dr("volo")
                voloobbligatorio = dr("voloobbligatorio")
            End If
            dr.Close()
            cn.Close()
            
            If co = 0 Then
                caricaprezzi2(primaperiodo, codiceperiodo, dopoperiodo, dscat, 0, parf)
            Else
                caricaprezzi2(primaperiodo, codiceperiodo, dopoperiodo, dscat, 1, parf)
            End If
            ricavadal(codiceperiodo, durata)
            Dim imbarco As String
            imbarco = cercaporto(codiceperiodo, "si")
            tasse = cctasse(durata)
            If co = 1 Then
                'Call caricacostacategorie(codiceperiodo, imbarco, dscat, Request.Params("persone"), arraypax)
            End If
            Call caricacategorie(codiceperiodo, Repeatercategorie, co) '0 è msc 1 è costa
            If altezza < 250 Then
                altezza = 250
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "','frameprezzi'); iniaz('" & primocolore & "','" & secondocolore & "');" & rigona & divone & imapiu & testone & prezzone & prezzone2 & prezzone3 & prezzocolonna1 & prezzocolonna2 & prezzocolonna3 & manina1 & manina2 & manina3 & appoggio1 & appoggio2 & appoggio3 & "", True)
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
        Dim tipocabina As Label = CType(e.Item.FindControl("tipocabina"), Label)
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
            LabelPrezzo.Text = Format(CInt(Labelpr.Text), "##,##0.00")
            If Labeldisponibile.Text = 1 Or Labeldisponibile.Text = 3 Then
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
            If ddispo = False Then
                LabelPrezzo.Enabled = False
                LabelPrezzo.ForeColor = Color.LightGray
            Else
                Dim stringacostap As String = "cabinecosta.aspx"
                If volo > 0 Then stringacostap = "voli.aspx"
                If controllo.Text = 0 Then
                    appoggio1 = appoggio1 & "appoggio1[" & conta1 & "]=""" & "'" & dal1 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & volo & "', '" & voloobbligatorio & "', '', '" & co & "', '" & tipocabina.Text & "'"";"
                End If
                If controllo.Text = 1 Then
                    appoggio2 = appoggio2 & "appoggio2[" & conta2 & "]=""" & "'" & dal2 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & volo & "', '" & voloobbligatorio & "', '', '" & co & "', '" & tipocabina.Text & "'"";"
                End If
                If controllo.Text = 2 Then
                    appoggio3 = appoggio3 & "appoggio3[" & conta3 & "]=""" & "'" & dal3 & "', '" & nomecabina & "', '" & LabelTipo.Text & "', 'cabinecosta', '" & stringacostap & "', '" & codiceperiodo.Text & "', '" & LabelCategoria.Text & "', '" & indica.ClientID & "', '" & volo & "', '" & voloobbligatorio & "', '', '" & co & "', '" & tipocabina.Text & "'"";"
                End If
            End If
            If controllo.Text = 0 Then conta1 = conta1 + 1
            If controllo.Text = 1 Then conta2 = conta2 + 1
            If controllo.Text = 2 Then conta3 = conta3 + 1
            contarighe = contarighe + CInt(conta.Text)
            If Labelpr.Text = 0 Then
                LabelPrezzo.Text = "n.d."
            End If
            If Left(LabelCategoria.Text, 2) = "00" Then
                LabelCategoria.Visible = False
            End If
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

            Dim epr As Integer = 0
            Dim epr2 As Integer = 0
            Dim epr3 As Integer = 0
            tutte = True
            tutte2 = True
            tutte3 = True
            If dalprima <> "" Then
                If (DateDiff(DateInterval.Day, Date.Now, CDate(dalprima)) < 1) Then 'se non c'è prima cancello
                    primaperiodo = ""
                End If
            Else
                primaperiodo = ""
            End If
            Dim tipocab As Integer = 0            

            Select Case codcat
                Case "I"
                    epr = prezziperiodo(4)
                    epr2 = prezziperiodo(0)
                    epr3 = prezziperiodo(8)
                    tipocab = 2
                    filtraprezzi(Repeaterprezzi, "codiceperiodo = '" & codiceperiodo.Text & "' AND (tipocabina >=1 and tipocabina <= 2)")
                    filtraprezzi(Repeaterprezziprima, "codiceperiodo = '" & primaperiodo & "' AND (tipocabina >=1 and tipocabina <= 2)")
                    filtraprezzi(Repeaterprezzidopo, "codiceperiodo = '" & dopoperiodo & "' AND (tipocabina >=1 and tipocabina <= 2)")
                Case "E"
                    epr = prezziperiodo(5)
                    epr2 = prezziperiodo(1)
                    epr3 = prezziperiodo(9)
                    tipocab = 4
                    filtraprezzi(Repeaterprezzi, "codiceperiodo = '" & codiceperiodo.Text & "' AND (tipocabina >=3 and tipocabina <= 4)")
                    filtraprezzi(Repeaterprezziprima, "codiceperiodo = '" & primaperiodo & "' AND (tipocabina >=3 and tipocabina <= 4)")
                    filtraprezzi(Repeaterprezzidopo, "codiceperiodo = '" & dopoperiodo & "' AND (tipocabina >=3 and tipocabina <= 4)")
                Case "B"
                    epr = prezziperiodo(6)
                    epr2 = prezziperiodo(2)
                    epr3 = prezziperiodo(10)
                    tipocab = 6
                    filtraprezzi(Repeaterprezzi, "codiceperiodo = '" & codiceperiodo.Text & "' AND (tipocabina >=5 and tipocabina <= 6)")
                    filtraprezzi(Repeaterprezziprima, "codiceperiodo = '" & primaperiodo & "' AND (tipocabina >=5 and tipocabina <= 6)")
                    filtraprezzi(Repeaterprezzidopo, "codiceperiodo = '" & dopoperiodo & "' AND (tipocabina >=6 and tipocabina <= 5)")
                Case Else
                    If codcat = "S" Or codcat = "G" Or codcat = "M" Or codcat = "P" Or codcat = "W" Then
                        epr = prezziperiodo(7)
                        epr2 = prezziperiodo(3)
                        epr3 = prezziperiodo(11)
                        tipocab = 8
                        filtraprezzi(Repeaterprezzi, "codiceperiodo = '" & codiceperiodo.Text & "' AND (tipocabina >=7 and tipocabina <= 8)")
                        filtraprezzi(Repeaterprezziprima, "codiceperiodo = '" & primaperiodo & "' AND (tipocabina >=7 and tipocabina <= 8)")
                        filtraprezzi(Repeaterprezzidopo, "codiceperiodo = '" & dopoperiodo & "' AND (tipocabina >=7 and tipocabina <= 8)")
                    End If
            End Select
            If primaperiodo = "" Then
                Repeaterprezziprima.Visible = False
            Else
                Labelprezzo2.Text = restituisciprezzo(epr2, tasse, co)

            End If
            If dopoperiodo = "" Then
                Repeaterprezzidopo.Visible = False
            Else
                Labelprezzo3.Text = restituisciprezzo(epr3, tasse, co)
            End If
            Labelprezzo.Text = restituisciprezzo(epr, tasse, co)
            Dim sirq As Boolean = False
            If tutte = True Then
                rigarq.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ.Style.Add("cursor", "pointer")
                HyperRQ.Style.Add("text-decoration", "underline")
                HyperRQ.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & codiceperiodo.Text & "', '" & ricavacat(codiceperiodo.Text, tipocab) & "', '" & co & "','0','" & tipocab & "')")
            End If
            If tutte2 = True And Labelprezzo2.Text <> "" Then
                rigarq2.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ2.Style.Add("cursor", "pointer")
                HyperRQ2.Style.Add("text-decoration", "underline")
                HyperRQ2.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & codiceperiodo.Text & "', '" & ricavacat(codiceperiodo.Text, tipocab) & "', '" & co & "','0','" & tipocab & "')")
            End If
            If tutte3 = True And Labelprezzo3.Text <> "" Then
                rigarq3.Visible = True
                Dim datapreno As Date = Date.Now
                LabelRQ.Text = "per la data del " & Format(datapreno, "dd/MM/yy") & " la " & descri.Text & "<br/>non è attualmente disponibile. Puoi inoltrare una<br/>richiesta nel caso si liverasse sarà nostra cura contattarti"
                sirq = True
                HyperRQ3.Style.Add("cursor", "pointer")
                HyperRQ3.Style.Add("text-decoration", "underline")
                HyperRQ3.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & codiceperiodo.Text & "', '" & ricavacat(codiceperiodo.Text, tipocab) & "', '" & co & "','0','" & tipocab & "')")
            End If
            If sirq Then
                addmisura = addmisura + 50
            End If
            misura = 55 + (contarighe * 31) + addmisura
            Dim cl As String = riga.Style.Item("background")
            Dim stringa As String = "vedisotto('" & divdescri.ClientID & "','" & riga.ClientID & "', '" & misura & "', '" & cl & "', '" & ImageDetail.ClientID & "', '" & descri.ClientID & "', $$, '" & altezza & "');"
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
            eti2.Text = Format(CDate(daltot), "dd/MM/yy")
            Dim eti3 As Label = CType(e.Item.FindControl("eti3"), Label)
            If dalprima <> "" Then eti3.Text = Format(CDate(dalprima), "dd/MM/yy")
            Dim eti4 As Label = CType(e.Item.FindControl("eti4"), Label)
            If daldopo <> "" Then eti4.Text = Format(CDate(daldopo), "dd/MM/yy")
        End If

    End Sub

    Private Sub filtraprezzi(ByVal rpt As Repeater, ByVal filtro As String)
        Dim dt As DataTable = dscat.Tables("Prezzi")
        Dim dv As New DataView(dt)
        dv.RowFilter = filtro
        rpt.DataSource = dv
        rpt.DataBind()
    End Sub

    Function ricavacat(ByVal codiceperiodo As String, ByVal tipocabina As Integer) As String
        Dim dt As DataTable = dscat.Tables("Prezzi")
        Dim dv As New DataView(dt)
        dv.RowFilter = "codiceperiodo = '" & codiceperiodo & "' AND tipocabina = " & tipocabina
        Dim dtn As New DataTable
        dtn = dv.ToTable
        Dim appoggio As String = ""
        For Each dr As DataRow In dtn.Rows
            appoggio = dr("categoria")
        Next
        ricavacat = appoggio
    End Function

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
            Call caricacat(codiceperiodo, ds, "I", "Cabina Interna", compagnia, 1, 2, "E", "Cabina Esterna Finestra", 3, 4, "B", "Cabina con Balcone", 5, 6, "S", "Suite", 7, 8)
        ElseIf compagnia = 1 Then 'costa
            Call caricacat(codiceperiodo, ds, "I", "Cabina Interna", compagnia, 0, 0, "E", "Cabina Esterna Finestra", 3, 4, "B", "Cabina con Balcone", 5, 6, "S", "Suite", 7, 8)
        End If
        Dim conta As Integer = 0
        For Each Row As DataRow In ds.Tables("Categorie").Rows
            conta = conta + 1
        Next
        altezza = (conta * 29) + 146
        rpt.DataSource = ds.Tables("Categorie").DefaultView
        rpt.DataBind()
    End Sub

    Private Sub caricacat(ByVal codiceperiodo As String, ByVal ds As DsCosta, ByVal cat As String, ByVal descrizione As String, ByVal compagnia As Integer, ByVal tipomin As Integer, ByVal tipomax As Integer, ByVal cat2 As String, ByVal descrizione2 As String, ByVal tipomin2 As Integer, ByVal tipomax2 As Integer, ByVal cat3 As String, ByVal descrizione3 As String, ByVal tipomin3 As Integer, ByVal tipomax3 As Integer, ByVal cat4 As String, ByVal descrizione4 As String, ByVal tipomin4 As Integer, ByVal tipomax4 As Integer)
        Dim sqlconn As String = ""

        If compagnia = 0 Then 'msc            
            sqlconn = "(SELECT codiceperiodo, '" & cat & "' As categoria, disponibile, listino, prezzo, '" & descrizione & "' As descrizione from prezzi WHERE  codiceperiodo = '" & codiceperiodo & "' and tipocabina >= " & tipomin & " and tipocabina <= " & tipomax & " order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat2 & "' As categoria, disponibile, listino, prezzo, '" & descrizione2 & "' As descrizione from prezzi WHERE  codiceperiodo = '" & codiceperiodo & "' and tipocabina >= " & tipomin2 & " and tipocabina <= " & tipomax2 & " order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat3 & "' As categoria, disponibile, listino, prezzo, '" & descrizione3 & "' As descrizione from prezzi WHERE  codiceperiodo = '" & codiceperiodo & "' and tipocabina >= " & tipomin3 & " and tipocabina <= " & tipomax3 & " order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat4 & "' As categoria, disponibile, listino, prezzo, '" & descrizione4 & "' As descrizione from prezzi WHERE  codiceperiodo = '" & codiceperiodo & "' and tipocabina >= " & tipomin4 & " and tipocabina <= " & tipomax4 & " order by prezzo LIMIT 1)"
        ElseIf compagnia = 1 Then 'costa
            sqlconn = "(SELECT codiceperiodo, '" & cat & "' As categoria, disponibile, listino, prezzo, '" & descrizione & "' As descrizione from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' and categoria like '" & cat & "%' order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat2 & "' As categoria, disponibile, listino, prezzo, '" & descrizione2 & "' As descrizione from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' and categoria like '" & cat2 & "%' order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat3 & "' As categoria, disponibile, listino, prezzo, '" & descrizione3 & "' As descrizione from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' and categoria like '" & cat3 & "%' order by prezzo LIMIT 1) UNION (SELECT codiceperiodo, '" & cat4 & "' As categoria, disponibile, listino, prezzo, '" & descrizione4 & "' As descrizione from prezzi WHERE codiceperiodo = '" & codiceperiodo & "' and categoria like '" & cat4 & "%' order by prezzo LIMIT 1)"
        End If
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim da As New MySqlDataAdapter(sqlconn, cn)
        da.Fill(ds, "Categorie")
        cn.Close()
    End Sub


    Function esponiprezzo2(ByVal codiceperiodo1 As String, ByVal codiceperiodo2 As String, ByVal codiceperiodo3 As String, ByVal parf As Boolean) As Integer()
        Dim appprezzi(11) As Integer
        Dim i As Integer
        For i = 0 To 11
            appprezzi(i) = 0
        Next
        Dim sqlconn As String
        If parf = False Then
            sqlconn = "SELECT min(prezzo) as pr, tipocabina, codiceperiodo  from prezzi WHERE disponibile = 0 AND (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "') GROUP By tipocabina, codiceperiodo order by codiceperiodo, tipocabina DESC"
        Else
            sqlconn = "SELECT min(prezzo) as pr, tipocabina, codiceperiodo  from prezzi WHERE disponibile = 2 AND (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "') GROUP By tipocabina, codiceperiodo order by codiceperiodo, tipocabina DESC"
        End If
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Do While dr.Read()
                If dr("codiceperiodo") = codiceperiodo1 Then
                    i = 0
                ElseIf dr("codiceperiodo") = codiceperiodo2 Then
                    i = 4
                ElseIf dr("codiceperiodo") = codiceperiodo3 Then
                    i = 8
                End If
                If dr("tipocabina") >= 1 And dr("tipocabina") <= 2 Then
                    appprezzi(i) = dr("pr")
                ElseIf dr("tipocabina") >= 3 And dr("tipocabina") <= 4 Then
                    appprezzi(i + 1) = dr("pr")
                ElseIf dr("tipocabina") >= 5 And dr("tipocabina") <= 6 Then
                    appprezzi(i + 2) = dr("pr")
                ElseIf dr("tipocabina") >= 7 And dr("tipocabina") <= 8 Then
                    appprezzi(i + 3) = dr("pr")
                End If
            Loop
        End If
        dr.Close()
        cn.Close()
        esponiprezzo2 = appprezzi
        'caricaprezzi(codiceperiodo, rpt, filtro)
    End Function

    Private Sub caricaprezzi2(ByVal codiceperiodo1 As String, ByVal codiceperiodo2 As String, ByVal codiceperiodo3 As String, ByVal ds As DsCosta, ByVal dicosta As Boolean, ByVal parf As Boolean)
        Dim sqlconn As String = ""
        If parf = False Then
            sqlconn = "SELECT prezzo, categoria, tipocabina, codiceperiodo, descrizione, disponibile  from prezzi WHERE disponibile <= 1 AND (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "')  order by codiceperiodo,  tipocabina,  categoria"
            If dicosta = True Then
                sqlconn = "SELECT prezzo, categoria, tipocabina, codiceperiodo, descrizione, disponibile  from prezzi WHERE disponibile <= 1 AND (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "')  order by codiceperiodo,  tipocabina, prezzo"
            End If
        Else
            sqlconn = "SELECT prezzo, categoria, tipocabina, codiceperiodo, descrizione, disponibile  from prezzi WHERE (disponibile > 1 AND disponibile <=3) AND (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "')  order by codiceperiodo,  tipocabina, categoria"
            If dicosta = True Then
                sqlconn = "SELECT prezzo, categoria, tipocabina, codiceperiodo, descrizione, disponibile  from prezzi WHERE  (codiceperiodo = '" & codiceperiodo1 & "' or codiceperiodo = '" & codiceperiodo2 & "' or codiceperiodo = '" & codiceperiodo3 & "')  order by codiceperiodo,  tipocabina, prezzo"
            End If
        End If
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim da As New MySqlDataAdapter(sqlconn, cn)
        da.Fill(ds, "prezzi")
        cn.Close()
    End Sub


    Function calcoloassiestate(ByVal prezzo As Integer) As Integer
        calcoloassiestate = 0
        If co = 0 Then
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
        ElseIf co = 1 Then
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

    Function ricavaperiodo(ByVal codiceperiodo As String, ByVal prima As Boolean) As String()        
        Dim sqlconn As String
        sqlconn = "SELECT id_itinerario, durata, dal FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dal As Date
        Dim iditi As Integer = 0
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            dal = CDate(dr("dal"))
            iditi = dr("id_itinerario")
        End If
        dr.Close()
        Dim appdal As String = ""
        If prima = True Then appdal = "dal < '" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "' ORDER BY DAL desc"
        If prima = False Then appdal = "dal > '" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "' ORDER BY DAL"
        sqlconn = "SELECT codiceperiodo, dal  FROM periodo where id_itinerario = '" & iditi & "' AND " & appdal
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim risu(1) As String
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
            dr2.Read()
            risu(1) = dr2("dal")
            risu(0) = dr2("codiceperiodo")
        End If
        dr2.Close()
        cn.Close()
        Return risu
    End Function
    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function
End Class
