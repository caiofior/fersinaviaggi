Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_msc_crociere
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim nomezona(0) As String
    Dim tasse As Integer = 0
    Dim frmprezzi As String = ""
    Dim imageback As String = ""
    Dim offertachiama As String = ""
    Dim nave2(0) As String
    Dim fotonave2(0) As String
    Dim idperiodo2 As Integer = 0
    Dim nave3 As String = ""
    Dim nave4 As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        mettidrop(eta1, 0, 100, 35)
        mettidrop(eta2, 0, 100, 35)
        mettidrop(eta3, 0, 100, 1)
        mettidrop(eta4, 0, 100, 1)
        mettidrop(eta5, 0, 100, 1)
        DropAd.SelectedValue = 2
        Call caricanave()
        If Not Page.IsPostBack Then
            caricapromo()
            If Not IsNothing(Request.UrlReferrer) Then
                lnkcrocere.NavigateUrl = Request.UrlReferrer.ToString()
            Else
                lnkcrocere.NavigateUrl = "crociere.aspx"
            End If
            lnkcrocere.Text = "Torna alla pagina di ricerca"            
            caricazona()
            Call caricaperiodo(Request.Params("id"))
            Dim idperiodo As Integer = 0
            If IsNumeric(Request.Params("id")) Then idperiodo = Request.Params("id")
            If Request.Params("id") = 4222 Then
                Labelsocio.Style.Add("visibility", "hidden")
                socio.Style.Add("visibility", "hidden")
                dropsocio.Style.Add("visibility", "hidden")
                cognomeclub.Style.Add("visibility", "hidden")
                Label1.Style.Add("visibility", "hidden")
                socio.Text = Request.Params("parf")
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "frmprezzi='" & frmprezzi & "'; caricaprezzi('divframeprezzi', '" + frmprezzi + "', 'carica', '" + imageback + "');caricaitinerario('itinerario','itinerario.aspx?id=" & idperiodo & "', '" + imageback + "'); caricadatinave('datinave', 'nave.aspx?id=" & idperiodo & "', '" + imageback + "', '" & idperiodo & "', '" & idperiodo2 & "', '" & nave3 & "','" & nave4 & "');" & offertachiama, True)
        End If
        vediinteressare(Request.Params("id"))
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

    Private Sub caricapromo()
        Dim sqlconn As String
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

    Private Sub mettidrop(ByVal ndrop As DropDownList, ByVal min As Integer, ByVal max As Integer, ByVal valore As Integer)
        Dim i As Integer
        ' ndrop.Items.Add("--")
        If min > max Then
            For i = min To max Step -1
                ndrop.Items.Add(i)
            Next
        Else
            For i = min To max
                ndrop.Items.Add(i)
            Next
        End If
        ndrop.SelectedValue = valore
    End Sub

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

    Private Sub caricaperiodo(ByVal idperiodo As Integer)
        Dim frame1 As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("frame1"), System.Web.UI.HtmlControls.HtmlGenericControl)        
        frame1.Attributes("src") = "rqinfo.aspx?id=" & idperiodo
        Dim prezzoda As Integer = 0
        Dim sqlconn As String
        sqlconn = "SELECT itinerario.*, periodo.*, nave.*, periodo.durata as pdurata, nave.titolo AS titolonave, itinerario.titolo as titoloitinerario from itinerario, periodo, nave WHERE periodo.id_itinerario = itinerario.id_itinerario AND periodo.id_nave = nave.id_nave AND periodo.id_periodo = " & idperiodo
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim imb As String = ""
        Dim sba As String = ""
        If dr.HasRows Then
            dr.Read()
            tasse = cctasse(dr("durata"))
            If idperiodo = 4222 Then
                party.Visible = True
                offertachiama = "offertachiama[0]='" & party.ClientID & "'"
            End If
            If dr("chiama") = 1 Then                
                chiamaspeciale.Visible = True
                offertachiama = "offertachiama[0]='" & chiamaspeciale.ClientID & "'"
            End If
            If Request.Params("parf") = "0013445" Then offertachiama = ""
            If dr("prezzoi") > 0 And prezzoda = 0 Then prezzoda = dr("prezzoi")
            If dr("prezzoe") > 0 And prezzoda = 0 Then prezzoda = dr("prezzoe")
            If dr("prezzob") > 0 And prezzoda = 0 Then prezzoda = dr("prezzob")
            If dr("prezzos") > 0 And prezzoda = 0 Then prezzoda = dr("prezzos")
            If dr("compagnia") = 0 Then ' MSC
                'carica.Style.Add("background", "url(../images/attesa-msc.jpg) no-repeat;")                
                Labelsocio.Text = Labelsocio.Text & " MSC Club <span style='font-size:x-small'>(opzionale)</span>:"
                Image1.ImageUrl = "../images/lgmsc.gif"
                imageback = "url(../images/attesa-msc.jpg) no-repeat;"
                If prezzoda > 0 Then
                    lblprezzo.Text = Format(prezzoda, "##,##") & ",-"
                End If
                Dim listi As New ListItem
                listi.Text = "Black"
                listi.Value = "Black"
                dropsocio.Items.Insert(0, listi)
                Dim listi2 As New ListItem
                listi2.Text = "Gold"
                listi2.Value = "Gold"
                dropsocio.Items.Insert(0, listi2)
                Dim listi3 As New ListItem
                listi3.Text = "Silver"
                listi3.Value = "Silver"
                dropsocio.Items.Insert(0, listi3)
                Dim listi4 As New ListItem
                listi4.Text = "Classic"
                listi4.Value = "Classic"
                dropsocio.Items.Insert(0, listi4)
                Dim listi5 As New ListItem
                listi5.Text = "Seleziona"
                listi5.Value = "Seleziona"
                dropsocio.Items.Insert(0, listi5)
            ElseIf dr("compagnia") = 1 Then 'Costa
                dropsocio.Visible = False
                cognomeclub.Visible = True
                'carica.Style.Add("background", "url(../images/attesa-costa.jpg) no-repeat;")
                imageback = "url(../images/attesa-costa.jpg) no-repeat;"
                Labelsocio.Text = Labelsocio.Text & " COSTA Club <span style='font-size:x-small'>(opzionale)</span>:"
                Image1.ImageUrl = "../images/lgcosta.gif"
                If prezzoda > 0 Then
                    lblprezzo.Text = Format(prezzoda - tasse, "##,##") & ",-"
                End If
            End If
            Image2.ImageUrl = dr("foto")
            Image3.ImageUrl = dr("mappa")
            Labelnave.Text = UpCase(dr("titolonave"))
            Labeldescri.Text = dr("pdurata") & " notti a bordo di " & Labelnave.Text & " da " & UpCase(dr("imbarco")) & " il <b>" & dr("dal") & "</b>"
            If dr("id_nave2") > 0 Then
                nave3 = Replace(dr("titolonave"), "COSTA", "C.")
                nave4 = Replace(ricavanave(dr("id_nave2")), "COSTA", "C.")
                If Not IsDBNull(dr("dal2")) Then
                    idperiodo2 = ricavaperiodo2(dr("id_nave2"), dr("dal2"))
                Else
                    idperiodo2 = ricavaperiodo2(dr("id_nave2"), dr("dal"))
                End If
                Labelnave.Text = "<span style='font-size:small'>Formula Roulette fra:</span><br/>" & Labelnave.Text & "<br/>" & UpCase(nave2(dr("id_nave2")))
                Labeldescri.Text = "partenza il <b>" & dr("dal") & "</b>"
                Image3.ImageUrl = fotonave2(dr("id_nave2"))
                If Not IsDBNull(dr("dal2")) Then
                    Labeldescri.Text = Labeldescri.Text & " o il <b>" & dr("dal2") & "</b>"
                End If
            End If

            If dr("zona") > 0 Then
                lbltitolo.Text = nomezona(CInt(dr("zona")) - 1) & ": " & UpCase(Replace(Replace(dr("titolo"), ", ", ","), ",", ", "))
            Else
                lbltitolo.Text = UpCase(Replace(Replace(dr("titolo"), ", ", ","), ",", ", "))
            End If
            'frameprezzi.Attributes.Add("onload", "ffiframe('carica')")
            'Dim stringaprezzi As String = "prezzi.aspx?id=" & dr("codiceperiodo") & "&co=" & dr("compagnia") & "&durata=" & dr("pdurata") & "&persone=" & DropAd.SelectedValue & "&eta1=" & eta1.SelectedValue & "&eta2=" & eta2.SelectedValue & "&eta3=" & eta3.SelectedValue & "&eta4=" & eta4.SelectedValue & "&eta5=" & eta5.SelectedValue & "&dal=" & dr("dal") & "&volo=" & dr("volo")

            Dim stringaprezzi As String = "prezzi.aspx?id=" & dr("codiceperiodo")
            If Request.Params("parf") = "0013445" Then
                stringaprezzi = "prezzi.aspx?id=" & dr("codiceperiodo") & "&parf=0013445"
            End If
            'frameprezzi.Attributes.Add("src", stringaprezzi)
            frmprezzi = stringaprezzi
            DropAd.Attributes.Add("onchange", "javascript:cambiapax('resto', '" & dr("compagnia") & "')")
            eta1.Attributes.Add("onchange", "javascript:controllaeta(); controllaetadp(this, '" & dr("compagnia") & "')")
            eta2.Attributes.Add("onchange", "javascript:controllaeta(); controllaetadp(this, '" & dr("compagnia") & "')")
            eta3.Attributes.Add("onchange", "javascript:controllaeta(); controllaetadp(this, '" & dr("compagnia") & "')")
            eta4.Attributes.Add("onchange", "javascript:controllaeta(); controllaetadp(this, '" & dr("compagnia") & "')")
            eta5.Attributes.Add("onchange", "javascript:controllaeta(); controllaetadp(this, '" & dr("compagnia") & "')")
        End If
        dr.Close()
        cn.Close()
    End Sub

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

    Function ricavaperiodo2(ByVal idnave As Integer, ByVal dal As Date) As Integer
        ricavaperiodo2 = 0
        Dim sqlconn As String
        sqlconn = "SELECT id_periodo FROM periodo WHERE periodo.id_nave = " & idnave & " AND periodo.dal = '" & inseriscizero(dal.Day) & "-" & inseriscizero(dal.Month) & "-" & dal.Year & "-" & "'"
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

    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
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

    Private Sub vediinteressare(ByVal idperiodo As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT typeiti, dal, imbarco FROM periodo where typeiti <> 0 and id_periodo = " & idperiodo
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim typeiti As Integer = 0
        Dim dal As Date
        Dim imbarco As String = ""
        If dr.HasRows Then
            dr.Read()
            typeiti = dr("typeiti")
            dal = dr("dal")
            imbarco = dr("imbarco")
            frameinteressare.Attributes.Add("src", "interessare.aspx?typeiti=" & typeiti & "&imbarco=" & imbarco & "&dal=" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "&idperiodo=" & idperiodo)        
        End If
        dr.Close()
        cn2.Close()

    End Sub
End Class
