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
    Dim codicecosta As String = ConfigurationSettings.AppSettings("codicecosta")
    Dim namecosta As String = ConfigurationSettings.AppSettings("namecosta")
    Dim pswcosta As String = ConfigurationSettings.AppSettings("pswcosta")
    Dim contarighe As Integer = 0
    Dim id_itinerario As Integer
    Dim dscat As New DsCosta
    Dim rigona As String = ""
    Dim fremone As String = ""
    Dim volo As Integer = 0
    Dim voloobbligatorio As Integer = 0


    Private Sub caricacostacabine(ByVal codiceperiodo As String, ByVal categoria As String, ByVal imbarco As String, ByVal persone As Integer, ByVal arraypax() As String, ByVal volo As Integer, ByVal aeroporto As String)
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
        cat.Code = categoria
        far.Code = "IND"
        If Right(categoria, 1) = "X" Then
            far.Code = "PIND"
        ElseIf Right(categoria, 1) = "V" Then
            far.Code = "VALUE"
        End If
        If Left(codiceperiodo, 1) = "Z" Then
            far.Code = "ROULETTE"
        End If
        dispo.Category = cat
        dispo.Code = codiceperiodo
        dispo.Type = net.costaclick.web.ComponentType.Cruise
        dispo.Fare = far
        Dim compo(0) As net.costaclick.web.Component
        compo(0) = dispo
        If volo > 0 Then ' 3 -> andare, 2 -> tornare, 1 -> a/R            
            If aeroporto <> "NNN" Then
                Dim dispo2 As New net.costaclick.web.Component
                dispo2.Type = net.costaclick.web.ComponentType.Flight
                dispo2.Code = aeroporto
                Dim city As New net.costaclick.web.City
                city.Code = aeroporto
                Dim direzione As Integer = volo
                If direzione = 1 Then
                    dispo2.Direction = net.costaclick.web.Direction.Both
                ElseIf direzione = 2 Then
                    dispo2.Direction = net.costaclick.web.Direction.OutBound
                ElseIf direzione = 3 Then
                    dispo2.Direction = net.costaclick.web.Direction.InBound
                End If
                Dim cities(0) As net.costaclick.web.City
                cities(0) = city
                dispo2.Cities = cities
                dispo2.Insurance = True
                ReDim Preserve compo(1)
                compo(1) = dispo2
            End If
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
        Dim listadoc() As net.costaclick.web.CruiseDocument
        listadoc = ws.ListDocuments(codiceperiodo)
        Dim oitem2 As net.costaclick.web.CruiseDocument
        passaporto.Text = 1
        For Each oitem2 In listadoc
            If Not IsDBNull(oitem2.Code) Then
                If oitem2.Code = "NAT.ID" Then
                    passaporto.Text = 0
                End If
            End If
        Next
        Try
            Dim nonce As Boolean = False
            GridView1.DataSource = ws.ListAvailableCategories(compo, pass)
            GridView1.DataBind()

            Dim oItem3 As GridViewRow
            For Each oItem3 In GridView1.Rows
                Dim code As Label = CType(oItem3.FindControl("code"), Label)
                Dim Availability As Label = CType(oItem3.FindControl("Availability"), Label)
                If Trim(code.Text) = Trim(categoria) Then
                    If Trim(Availability.Text) = "True" Then
                        nonce = True
                    End If
                End If
            Next
            ' Labeltogli.Text = listadoc(0).Code
            If nonce = True Then
                Repeatercabine.DataSource = ws.ListAvailableCabins(compo, pass)
                Repeatercabine.DataBind()
                labelerrore.Visible = False
                paneleerrore.Visible = False
                Call verificaseuna()
            Else
                Dim rigatitoli As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigatitoli"), System.Web.UI.HtmlControls.HtmlGenericControl)
                Dim barrainfo As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("barrainfo"), System.Web.UI.HtmlControls.HtmlGenericControl)
                barrainfo.Visible = False
                rigatitoli.Visible = False

                paneleerrore.Style.Add("margin-top", "-35px")
                labelerrore.Text = labelerrore.Text & "<span style='font-size:small; font-weight:normal;'><br /><br />La cabina scelta non è attualmente disponibile, puoi inviare una richiesta così avrai la priorità nella ricerca della cabina. Questa richiesta non sarà impegnativa e sarai ricontattato dal nostro ufficio, solo allora potrai decidere se confermare o meno la cabina.<br /><br/></span>"
                HyperRQ.Visible = True
                'HyperRQ.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & codiceperiodo.Text & "', '" & ricavacat(codiceperiodo.Text, tipocab) & "', '" & co & "','0','" & tipocab & "')")
                HyperRQ.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & Request.Params("codiceperiodo") & "', '" & Request.Params("categoria") & "', '" & Request.Params("compagnia") & "','0', '" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "', '" & passaporto.Text & "')")
                labelerrore.Visible = True
                paneleerrore.Visible = True
            End If
        Catch ex As Exception
            Dim rigatitoli As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigatitoli"), System.Web.UI.HtmlControls.HtmlGenericControl)
            Dim barrainfo As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("barrainfo"), System.Web.UI.HtmlControls.HtmlGenericControl)
            barrainfo.Visible = False
            rigatitoli.Visible = False
            labelerrore.Text = ex.Message.ToString
            If labelerrore.Text.IndexOf("Nessuna") >= 0 Then
                paneleerrore.Style.Add("margin-top", "-35px")
                labelerrore.Text = labelerrore.Text & "<span style='font-size:small; font-weight:normal;'><br /><br />La cabina scelta non è attualmente disponibile, puoi inviare una richiesta così avrai la priorità nella ricerca della cabina. Questa richiesta non sarà impegnativa e sarai ricontattato dal nostro ufficio, solo allora potrai decidere se confermare o meno la cabina.<br /><br/></span>"
                HyperRQ.Visible = True
                'HyperRQ.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & codiceperiodo.Text & "', '" & ricavacat(codiceperiodo.Text, tipocab) & "', '" & co & "','0','" & tipocab & "')")
                HyperRQ.Attributes.Add("onclick", "javascript:vedidati('dati', 'dati.aspx','" & Request.Params("codiceperiodo") & "', '" & Request.Params("categoria") & "', '" & Request.Params("compagnia") & "','0', '" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "', '" & passaporto.Text & "')")
            End If
            labelerrore.Visible = True
            paneleerrore.Visible = True
        End Try
    End Sub

    Private Sub verificaseuna()
        Dim oItem As RepeaterItem
        Dim i As Integer = 0
        Dim numcabina As String = ""
        Dim rigan As String = ""
        Dim lblnponte As String = ""
        Dim lblponte As String = ""
        For Each oItem In Repeatercabine.Items
            Dim LabelCabina As Label = CType(oItem.FindControl("LabelCabina"), Label)
            Dim LabelPonte As Label = CType(oItem.FindControl("LabelPonte"), Label)
            Dim LabelNPonte As Label = CType(oItem.FindControl("LabelNPonte"), Label)
            lblnponte = LabelNPonte.Text
            lblponte = LabelPonte.Text
            Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(oItem.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
            numcabina = LabelCabina.Text
            rigan = riga.ClientID
            i = i + 1
        Next
        If i = 1 Then
            Dim stringapreis As String = "preventivo.aspx"
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "assacabina('cabinecosta', '" & stringapreis & "', '" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & rigan & "', ' " & Trim(numcabina) & "', '" & Trim(lblnponte) & " - " & Trim(lblponte) & "', '" & volo & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "','', '" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "', '" & passaporto.Text & "')", True)
        End If
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        If Not Page.IsPostBack Then
            Dim codiceperiodo As String = Request.Params("codiceperiodo")
            Dim sqlconn As String
            sqlconn = "SELECT * from periodo WHERE codiceperiodo = '" & codiceperiodo & "' "
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                volo = dr("volo")
                passaporto.Text = dr("passaporto")
            End If
            dr.Close()
            cn.Close()
            Dim aeroporto As String = ""
            aeroporto = Request.Params("aeroporto")
            Dim imbarco As String = ""
            Dim arraypax(5) As String
            Label7.Text = "Categoria scelta: " & Request.Params("dal") & " - " & Request.Params("nomecabina").ToString.ToUpper & " - " & Request.Params("categoria") & " " & Request.Params("tipocabina")
            'Label7.Text = Request.QueryString.ToString
            arraypax(1) = Request.Params("eta1")
            arraypax(2) = Request.Params("eta2")
            arraypax(3) = Request.Params("eta3")
            arraypax(4) = Request.Params("eta4")
            arraypax(5) = Request.Params("eta5")
            Dim altezza As Integer = 0
            If Request.Params("compagnia") = 1 Then
                Call caricacostacabine(codiceperiodo, Request.Params("categoria"), imbarco, Request.Params("persone"), arraypax, volo, aeroporto)
                altezza = 160 + (35 * contarighe)
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'framecabinecosta'); altezzaframe='" & altezza & "';" & rigona & fremone, True)
            Else
                Dim stringapreis As String = "preventivo.aspx"
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "javascript:assacabina('cabinecosta', '" & stringapreis & "', '" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '', '', '', '" & volo & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "','','" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "', '" & passaporto.Text & "')", True)
            End If
        End If
    End Sub

   
    Protected Sub Repeatercabine_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeatercabine.ItemDataBound
        Dim Prendicabina As HyperLink = CType(e.Item.FindControl("Prendicabina"), HyperLink)
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
            Dim stringapreis As String = "preventivo.aspx"
            LabelPonte.Text = Replace(LabelPonte.Text, "'", "")
            If LabelPonte.Text = "-" Then
                LabelPonte.Text = "GARANTITA"
                LabelPonte.ForeColor = Color.Red
                LabelUrl.Text = "http://www.costaclick.net/CostaClick/it-IT/Pianonave.htm?ShipId=" & Left(Request.Params("codiceperiodo"), 2)
            End If
            If LabelFacility.Text.ToUpper = "TRUE" Then 'disabili
                disabili.Style.Add("visibility", "visible")
            End If
            poscabina.Attributes.Add("onclick", "javascript:vedisottocabina('" & riga.ClientID & "', '330', '" & frameimage.ClientID & "', 'framecabinecosta')")
            poscabina.Style.Add("cursor", "pointer")
            LabelTipoCabina.Text = Request.Params("tipocabina")
            frameimage.Attributes.Add("src", LabelUrl.Text.ToString)
            Prendicabina.Attributes.Add("onclick", "javascript:assacabina('cabinecosta', '" & stringapreis & "', '" & Request.Params("codiceperiodo") & "', '" & Request.Params("persone") & "', '" & Request.Params("categoria") & "', '" & Request.Params("eta1") & "', '" & Request.Params("eta2") & "', '" & Request.Params("eta3") & "', '" & Request.Params("eta4") & "', '" & Request.Params("eta5") & "', '" & riga.ClientID & "', ' " & Trim(numcabina.Text) & "', '" & Trim(LabelNPonte.Text) & " - " & Trim(LabelPonte.Text) & "', '" & volo & "', '" & Request.Params("aeroporto") & "', '" & Request.Params("nomeclub") & "', '" & Request.Params("cartaclub") & "', '" & Request.Params("compagnia") & "','', '" & Request.Params("tipologiacabina") & "','" & Request.Params("tipocabina") & "', '" & passaporto.Text & "')")
            rigona = rigona & "rigona[" & contarighe & "]='" & riga.ClientID & "';"
            fremone = fremone & "fremone[" & contarighe & "]='" & frameimage.ClientID & "';"
            contarighe = contarighe + 1
        End If
    End Sub


End Class
