Imports System.Data
Imports MySql.Data.MySqlClient
Imports classfersina

Partial Class crociere_contratto
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim contarighe As Integer = 0
    Dim nave2(0) As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            If Not IsNothing(Request.Params("email")) And Not IsNothing(Request.Params("code")) Then
                If controllo(Request.Params("email"), Request.Params("code")) Then
                    If IsNumeric(Request.Params("parsel")) Then
                        Call caricanave()
                        Call compila(Request.Params("parsel"))
                        Dim altezza As Integer = 1000 + (contarighe * 25)
                        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
                    End If
                End If
            End If
        End If
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
            Loop
        End If
        dr.Close()
        cn.Close()
    End Sub
    Private Sub compila(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT preno.*, periodo.*, itinerario.*, nave.titolo as titolonave, periodo.durata as duratat FROM preno, nave, periodo, itinerario WHERE periodo.id_periodo = preno.id_periodo AND itinerario.id_itinerario = periodo.id_itinerario AND  nave.id_nave = preno.id_nave AND preno.id_preno = '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("compagnia") = 0 Then
                Labeloperatore.Text = "MSC CROCIERE"
            ElseIf dr("compagnia") = 1 Then
                Labeloperatore.Text = "COSTA CROCIERE"
            End If
            Labelpratica.Text = dr("nopzione")
            If dr("cabina") = "G00000" Or dr("cabina").ToString.ToUpper = "GARANTITA" Then
                garantita.Visible = True
                If dr("adulti") + dr("bambini") >= 4 Then
                    garantita.Style.Add("height", "35px")
                    If dr("compagnia") = 0 Then Labelgarantita.Text = Labelgarantita.Text & "<br/>Per le cabine quadruple garantite MSC si riserva il diritto di assegnare anche <br/ >2 cabine distinte non obbligatoriamente vicine"
                End If
            End If
            labelnave.Text = dr("titolonave")
            Labelnome.Text = dr("nome")
            Labelindirizzo.Text = dr("indirizzo")
            Labelcap.Text = dr("cap")
            Labelcitta.Text = dr("citta")
            Labeltelefono.Text = dr("telefono")
            Labelfiscale.Text = dr("fiscale")
            lblcodpre.Text = dr("nopzione")
            If Request.Params("vedi") = 1 Then
                HyperContratto.Visible = False
                Dim dettagliocontratto As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("dettagliocontratto"), System.Web.UI.HtmlControls.HtmlGenericControl)
                dettagliocontratto.Visible = True
            End If
            HyperContratto.NavigateUrl = "contratto.aspx?vedi=1&parsel=" & Request.Params("parsel") & "&email=" & Request.Params("email") & "&code=" & Request.Params("code")
            HyperContratto.Target = "_balnk"
            lbldestinazione.Text = dr("paesi")
            lbldal.Text = dr("dal")
            lblal.Text = dr("al")
            If Right(dr("categoria"), 1) = "V" And dr("compagnia") = 1 Then
                lblservizio.Text = "<span style='font-size:xx-small;'>Promozione <b>Risparmiasubito</b> Condizioni annullamento variate come da promozione pubblicata da " & Labeloperatore.Text & "</span>"
            Else
                lblservizio.Text = "catalogo " & Labeloperatore.Text & "- edizione " & Date.Now.Year
            End If
            If Left(dr("codiceperiodo"), 1) = "Z" And dr("compagnia") = 1 Then
                If Not IsDBNull(dr("dal2")) Then
                    lbldal.Text = Format(dr("dal"), "dd/MM/yy") & " o " & Format(dr("dal2"), "dd/MM/yy")
                    lbldal.Style.Add("margin-left", "280px")
                End If
                If Not IsDBNull(dr("al2")) Then
                    lblal.Text = Format(dr("dal"), "dd/MM/yy") & " o " & Format(dr("al2"), "dd/MM/yy")
                End If
                labelnave.Text = "FORMULA ROULETTE"
                lblservizio.Text = "<span style='font-size:xx-small;'>Promozione <b>Partisubito</b> Condizioni annullamento variate come da promozione pubblicata da " & Labeloperatore.Text & "</span>"
                lbldestinazione.Text = "Formula Roulette fra " & dr("titolonave") & " / " & nave2(dr("id_nave2")) & ""
            End If

            lbldurata.Text = dr("durata")

            lblpartenza.Text = Left(dr("imbarco"), dr("imbarco").ToString.IndexOf("-") - 1)
            lblritorno.Text = Left(dr("sbarco"), dr("sbarco").ToString.IndexOf("-") - 1)
            Dim pax As Integer = 0
            pax = dr("adulti") + dr("bambini")
            Dim tasse As Integer = cctasse(dr("duratat"))
            Dim taxtotali As Integer = tasse * pax
            Dim prezzo As Integer = CInt((dr("prezzo") - taxtotali) / dr("adulti"))
            Dim assi As Integer = calcoloassiestate(prezzo)
            compilapreventivo(dr("id_preno"))
            lbltotalone.Text = Format(dr("prezzopac") + dr("prezzo"), "€ ##,##0.00")
            If Not IsDBNull(dr("voloda")) Then
                If dr("voloda") <> "" Then
                    lblpartenza.Text = dr("voloda")
                End If
            End If
            If Not IsDBNull(dr("voloa")) Then
                If dr("voloa") <> "" Then
                    lblritorno.Text = dr("voloa")
                End If
            End If
            lbldata.Text = "Trento li, " & Format(dr("data_preno"), "dd/MM/yyyy")
            Labelcontraente.Text = "Il contraente " & dr("nome") & " pagando l'acconto/saldo <br/ > della pratica accetta automaticamente il presente contratto"
            If dr("passp") = 1 Or dr("passaporto") = 1 Then
                Labeldocumenti.Text = "Obbligatorio il passaporto con valida almeno 6 mesi per tutti i partecipanti"
                Labeldocumenti.Visible = True
            End If
        End If
        dr.Close()
        cn.Close()
        Call caricanomi(idpreno)
        Call caricapagamenti(idpreno)
    End Sub

    Private Sub compilapreventivo(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT  * from preventivo where id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterPreventivo.DataSource = dr
            RepeaterPreventivo.DataBind()
        End If
        dr.Close()
        cn2.Close()
    End Sub

    Function esistepromo(ByVal idpromo As Integer) As String
        esistepromo = ""
        Dim sqlconn As String
        sqlconn = "SELECT nomepromo FROM promo WHERE id_promo = '" & idpromo & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            esistepromo = dr("nomepromo")
        End If
        dr.Close()
        cn2.Close()
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

    Function calcoloassiestate(ByVal prezzo As Integer) As Integer
        calcoloassiestate = 0
        If prezzo <= 800 Then
            calcoloassiestate = 25
        ElseIf prezzo > 800 And prezzo <= 1300 Then
            calcoloassiestate = 35
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
        End If
    End Function

    Private Sub caricanomi(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM nomi WHERE id_preno = '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterNomi.DataSource = dr
            RepeaterNomi.DataBind()
        End If
        dr.Close()
        cn2.Close()
    End Sub

    Private Sub caricapagamenti(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM pagamenti WHERE id_preno = '" & idpreno & "'"
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

    Function controllo(ByVal email As String, ByVal code As String) As Boolean
        controllo = False
        Dim sqlconn As String
        sqlconn = "SELECT id_preno, codice, email FROM preno WHERE email = '" & email & "' AND codice = '" & code & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            controllo = True
        End If
        dr.Close()
        cn.Close()
    End Function

    Protected Sub RepeaterPaga_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPaga.ItemDataBound
        Dim ricevuto As Label = CType(e.Item.FindControl("ricevuto"), Label)
        Dim labelricevuto As Label = CType(e.Item.FindControl("labelricevuto"), Label)
        Dim ricevutodata As Label = CType(e.Item.FindControl("ricevutodata"), Label)
        Dim Labelscadenza As Label = CType(e.Item.FindControl("Labelscadenza"), Label)        
        Dim lblentro As Label = CType(e.Item.FindControl("lblentro"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If ricevuto.Text = 1 Then
                lblentro.Text = "Ricevuto"
                Labelscadenza.Visible = False
            End If
        End If
    End Sub

    Protected Sub RepeaterPreventivo_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPreventivo.ItemDataBound
        Dim lblquota As Label = CType(e.Item.FindControl("lblquota"), Label)
        Dim lblpax As Label = CType(e.Item.FindControl("lblpax"), Label)
        Dim lblprezzo As Label = CType(e.Item.FindControl("lblprezzo"), Label)
        Dim lbltotale As Label = CType(e.Item.FindControl("lbltotale"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            lblquota.Text = Left(lblquota.Text, 60)
            If lblprezzo.Text = 0 Then lblprezzo.Visible = False
            If lblpax.Text = 0 Then lblpax.Visible = False
            If lbltotale.Text = 0 Then lbltotale.Visible = False
            contarighe = contarighe + 1
        End If
    End Sub

    Protected Sub RepeaterNomi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterNomi.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            contarighe = contarighe + 1
        End If
    End Sub
End Class
