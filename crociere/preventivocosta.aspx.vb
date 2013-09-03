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
    Dim rigona As String = ""
    Dim arraypax(5) As String
    Dim altezza As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.Cache.SetNoStore()
        arraypax(1) = Request.Params("eta1")
        arraypax(2) = Request.Params("eta2")
        arraypax(3) = Request.Params("eta3")
        arraypax(4) = Request.Params("eta4")
        arraypax(5) = Request.Params("eta5")
        If Not Page.IsPostBack Then
            ' labeltitolo.Text = ricavatitolo(Request.Params("codiceperiodo"))
            Call prenota(Request.Params("codiceperiodo"), Request.Params("categoria"), Request.Params("persone"), arraypax, Request.Params("cabina"))
            altezza = 258 + (contarighe * 28)
            ' vdett.Attributes.Add("onclick", "javascript:vedidettaglio('dettaglio','dettaglio.aspx','" & Request.Params("codiceperiodo") & "','" & prezzo.Text & "', '" & Request.Params("persone") & "'); premuto('vdett');")
            'vpacc.Attributes.Add("onclick", "javascript:vedidettaglio('dettaglio','pacchetti.aspx','" & Request.Params("codiceperiodo") & "','" & prezzo.Text & "', '" & Request.Params("persone") & "'); premuto('vpacc');")
            rigona = "rigona[0]='vdett'; rigona[1]='vpacc'; rigona[2]='voffe'; rigona[3]='vprezz';"
            'If paneleerrore.Visible = False Then
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "" & rigona & "altezza('" & altezza & "'); vedidettaglio('dettaglio','dettaglio.aspx','" & Request.Params("codiceperiodo") & "','" & prezzo.Text & "', '" & Request.Params("persone") & "')", True)
            'End If
        End If
    End Sub


    Function ricavatitolo(ByVal codiceperiodo As String) As String
        Dim sqlconn As String
        ricavatitolo = ""
        sqlconn = "SELECT * FROM periodo, nave where nave.id_nave = periodo.id_nave AND periodo.codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavatitolo = "Preventivo " & dr("titolo").ToString.ToUpper & " partenza del " & Format(dr("dal"), "dd/MM/yyyy") & " <span style='font-size:small'>per " & Request.Params("persone") & " persone</span>"
        End If
        dr.Close()
        cn.Close()
    End Function


    Function ricavapart(ByVal codiceperiodo As String) As Date
        Dim sqlconn As String
        ricavapart = Date.Now
        sqlconn = "SELECT * FROM periodo where codiceperiodo = '" & codiceperiodo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            ricavapart = dr("dal")
        End If
        dr.Close()
        cn.Close()
    End Function

    Function ricavatipodata(ByVal eta As String, ByVal partenza As Date) As String
        ricavatipodata = "1960-01-01T00:00:00.0000000+01:00"
        Dim dateapp As Date = DateAdd(DateInterval.Year, -CInt(eta), partenza)
        ricavatipodata = dateapp.Year & "-" & dateapp.Month & "-" & dateapp.Day & "T00:00:00.0000000+01:00"""
    End Function

    Private Sub prenota(ByVal codiceperiodo As String, ByVal categoria As String, ByVal persone As String, ByVal arraypax() As String, ByVal cabina As String)
        Dim ws As New net.costaclick.web.Booking
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
        Dim cabin As New net.costaclick.web.Cabin
        cabin.Number = Trim(cabina)
        dispo.Cabin = cabin
        dispo.Insurance = True
        cat.Code = categoria
        far.Code = "IND"
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
                dispo2.Insurance = True
                ReDim Preserve compo(1)
                compo(1) = dispo2
            End If
        End If
        Dim pass(0) As net.costaclick.web.Guest
        ReDim Preserve pass(persone - 1)
        Dim i As Integer
        Dim partenza As Date = ricavapart(Request.Params("codiceperiodo"))
        For i = 1 To persone
            Dim guest As New net.costaclick.web.Guest
            guest.FirstName = "Firstname " & i
            guest.LastName = "Lastname " & i
            guest.BirthDate = ricavatipodata(arraypax(i), partenza)
            pass(i - 1) = guest
            If i = 1 Then
                If Request.Params("cartaclub") <> "" And Request.Params("nomeclub") <> "" Then
                    guest.CostaClubNumber = Request.Params("cartaclub")
                    guest.LastName = Request.Params("nomeclub")
                End If
            End If
        Next
        Dim quote As net.costaclick.web.QuoteDetail
        Try
            quote = ws.InvoiceAndPricing(compo, pass)
            GridPrezzi.DataSource = quote.Prices
            GridPrezzi.DataBind()
            Dim oItem As GridViewRow
            Dim totale As Integer = 0
            Dim ds2 As New dspreno
            For Each oItem In GridPrezzi.Rows
                Dim code As Label = CType(oItem.FindControl("Code"), Label)
                Dim Description As Label = CType(oItem.FindControl("Description"), Label)
                Dim Amount As Label = CType(oItem.FindControl("Amount"), Label)

                If Not IsDBNull(code.Text) Then
                    If code.Text = "CAB" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone categoria " & categoria & " - cabina n. " & cabina, 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "PCH" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone", 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "INS" Then
                        addrowprezzo(ds2, Description.Text & " per " & persone & " persone", 0, 0, CInt(Amount.Text))
                    ElseIf code.Text = "TOT" Then
                        totale = (CInt(Amount.Text))
                    ElseIf code.Text = "AIR" Then
                        Dim direzione As String = ""
                        If Request.Params("volo") = 1 Then direzione = "di andata e ritorno"
                        If Request.Params("volo") = 3 Then direzione = "di andata"
                        If Request.Params("volo") = 2 Then direzione = "di ritorno"
                        addrowprezzo(ds2, "Incluso volo " & direzione & " da " & ricavavolo(Request.Params("aeroporto")), 0, 0, 0)
                    ElseIf code.Text = "TRF" Then
                        addrowprezzo(ds2, "Incluso trasferimenti aeroporto / nave", 0, 0, 0)
                    End If
                End If
            Next oItem
            addrowprezzo(ds2, "<b>TOTALE</b>", 0, 0, totale)
            'RepeaterPreventivo.DataSource = ds2.Tables("preventivo")
            'RepeaterPreventivo.DataBind()
            prezzo.Text = totale
        Catch ex As Exception
            'paneleerrore.Visible = True
            'PanelPreventivo.Visible = False
            'labelerrore.Text = ex.Message.ToString
            'LabelErrore.Visible = True
        End Try
    End Sub

    Private Sub addrowprezzo(ByVal ds As DataSet, ByVal descrizione As String, ByVal persone As String, ByVal prezzo As Integer, ByVal totale As Integer)
        Dim dsnewrow As DataRow
        dsnewrow = ds.Tables("preventivo").NewRow
        dsnewrow("descrizione") = descrizione
        dsnewrow("persone") = persone
        dsnewrow("prezzo") = prezzo
        dsnewrow("totale") = totale
        ds.Tables("preventivo").Rows.Add(dsnewrow)

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




End Class
