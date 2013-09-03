Imports System.Data
Imports MySql.Data.MySqlClient
Imports classfersina
Imports System.Net.Mail
Partial Class crociere_iti
    Inherits System.Web.UI.Page

    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim contarighe As Integer = 0
    Private Sub salva()
        Dim oItem As RepeaterItem
        For Each oItem In RepeaterPax.Items
            Dim idnomi As Label = CType(oItem.FindControl("idnomi"), Label)
            Dim Dropdocumento As DropDownList = CType(oItem.FindControl("Dropdocumento"), DropDownList)
            Dim Dropnazione As DropDownList = CType(oItem.FindControl("Dropnazione"), DropDownList)
            Dim documento As TextBox = CType(oItem.FindControl("documento"), TextBox)
            Dim Luogo As TextBox = CType(oItem.FindControl("Luogo"), TextBox)
            Dim emissione As TextBox = CType(oItem.FindControl("sel1"), TextBox)
            Dim scadenza As TextBox = CType(oItem.FindControl("sel2"), TextBox)
            Dim nome As TextBox = CType(oItem.FindControl("nome"), TextBox)
            Dim cognome As TextBox = CType(oItem.FindControl("cognome"), TextBox)
            Dim telefono As TextBox = CType(oItem.FindControl("telefono"), TextBox)
            Dim Luogonascita As TextBox = CType(oItem.FindControl("Luogonascita"), TextBox)
            Dim upstring As String = ""
            Try
                If emissione.Text <> "" Then upstring = "emissioned = @emissioned, "
                If scadenza.Text <> "" Then upstring = upstring & "scadenzad = @scadenzad, "
                Dim stringa2 As String = "UPDATE nomi SET " & upstring & " luogorilascio = @luorilascio, nazionalita = @nazionalita, nomereferente = @nomereferente, cognomereferente = @cognomereferente, telreferente = @telreferente,  tipodocumento = @tipodocumento, documento = @documento, luogonascita = @luogonascita WHERE id_nomi= '" & idnomi.Text & "'"
                Dim cmd2 As New MySqlCommand(stringa2, cn)
                If cn.State = ConnectionState.Closed Then cn.Open()
                If emissione.Text <> "" Then cmd2.Parameters.AddWithValue("@emissioned", CDate(emissione.Text))
                If scadenza.Text <> "" Then cmd2.Parameters.AddWithValue("@scadenzad", CDate(scadenza.Text))
                cmd2.Parameters.AddWithValue("@luorilascio", Luogo.Text)
                cmd2.Parameters.AddWithValue("@nazionalita", Dropnazione.SelectedValue)
                cmd2.Parameters.AddWithValue("@nomereferente", nome.Text)
                cmd2.Parameters.AddWithValue("@cognomereferente", cognome.Text)
                cmd2.Parameters.AddWithValue("@telreferente", telefono.Text)
                cmd2.Parameters.AddWithValue("@tipodocumento", Dropdocumento.SelectedValue)
                cmd2.Parameters.AddWithValue("@documento", documento.Text)
                cmd2.Parameters.AddWithValue("@luogonascita", Luogonascita.Text)
                cmd2.ExecuteNonQuery()
                cmd2.Parameters.Clear()
                cmd2.Connection.Close()
            Catch ex As Exception
            End Try
        Next oItem

    End Sub


    Protected Sub BttSalvadocumenti_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BttSalvadocumenti.Click
        Me.Validate()
        If Me.IsValid = True Then
            Call salva()
            '  Response.Redirect("documenti.aspx?parsel=" & Request.Params("parsel") & "&code=" & Request.Params("code") & "&rn=" & Request.Params("rn"))
            Dim stringa2 As String = "UPDATE preno SET datidoc = @datidoc WHERE id_preno = '" & Request.Params("parsel") & "'"
            Dim cmd2 As New MySqlCommand(stringa2, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()            
            cmd2.Parameters.AddWithValue("@datidoc", 1)
            cmd2.ExecuteNonQuery()
            cmd2.Parameters.Clear()
            cmd2.Connection.Close()
            Labelvalida.Visible = False
            Dim lnk As String = "conferma-msc.aspx?codice=" & Request.Params("code") & "&email=" & email.Text
            ClientScript.RegisterStartupScript(Me.[GetType](), "redirect", "if(top!=self) {top.location.href = '" & lnk & "';}", True)
            'Response.Redirect("conferma-msc.aspx?codice=" & codice.Text & "&email=" & email.Text)
        Else
            Labelvalida.Visible = True
        End If

    End Sub

    Private Sub mettiidentita(ByVal si As Boolean)
        Dim oItem As RepeaterItem
        For Each oItem In RepeaterPax.Items
            Dim documento As TextBox = CType(oItem.FindControl("documento"), TextBox)
            Dim lblnum As Label = CType(oItem.FindControl("lblnum"), Label)
            Dim tipodocumento As Label = CType(oItem.FindControl("tipodocumento"), Label)
            Dim Dropdocumento As DropDownList = CType(oItem.FindControl("Dropdocumento"), DropDownList)
            Dim listi As New ListItem
            RepeaterPax.Visible = True
            Label10.Visible = True
            regola.Visible = True
            listi.Text = "- Selezionare -"
            If si = True Then
                listi.Text = "Carta d'identità"
                listi.Value = 2
                Dropdocumento.Items.Insert(2, listi)
                If tipodocumento.Text = 2 Then
                    Dropdocumento.SelectedValue = tipodocumento.Text
                End If
            End If
        Next oItem
    End Sub

    Private Sub caricanomi(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from nomi WHERE id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterPax.DataSource = dr
            RepeaterPax.DataBind()
        End If
        dr.Close()
        cn.Close()        
    End Sub

    'Protected Sub RepeaterPax_ItemCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPax.ItemCreated
    '    Dim oRb2 As TextBox = (CType(e.Item.FindControl("sel1"), TextBox))
    '    If Not oRb2 Is Nothing Then
    '        AddHandler oRb2.TextChanged, AddressOf sel1_TextChanged
    '    End If
    'End Sub

    'Protected Sub sel1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Dim oRb1 As TextBox = CType(sender, TextBox)
    '    Dim oItem As RepeaterItem
    '    For Each oItem In RepeaterPax.Items
    '        Dim sel1 As TextBox = CType(oItem.FindControl("sel1"), TextBox)
    '        Dim sel1a As Label = CType(oItem.FindControl("sel1a"), Label)
    '        If sel1.Equals(oRb1) Then
    '            sel1a.Text = sel1.Text
    '        End If
    '    Next oItem
    'End Sub

    Protected Sub RepeaterPax_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterPax.ItemDataBound
        Dim datanascita As Label = CType(e.Item.FindControl("datanascita"), Label)
        Dim dropgiorno As DropDownList = CType(e.Item.FindControl("DropGiorno"), DropDownList)
        Dim dropmese As DropDownList = CType(e.Item.FindControl("DropMese"), DropDownList)
        Dim dropanno As DropDownList = CType(e.Item.FindControl("DropAnno"), DropDownList)
        Dim tipodocumento As Label = CType(e.Item.FindControl("tipodocumento"), Label)
        Dim docu As Label = CType(e.Item.FindControl("docu"), Label)
        Dim documento As TextBox = CType(e.Item.FindControl("documento"), TextBox)
        Dim sel1 As TextBox = CType(e.Item.FindControl("sel1"), TextBox)
        Dim sel2 As TextBox = CType(e.Item.FindControl("sel2"), TextBox)
        Dim sel11 As TextBox = CType(e.Item.FindControl("sel1"), TextBox)
        Dim sel22 As TextBox = CType(e.Item.FindControl("sel2"), TextBox)
        Dim Dropdocumento As DropDownList = CType(e.Item.FindControl("Dropdocumento"), DropDownList)
        Dim Dropnazione As DropDownList = CType(e.Item.FindControl("Dropnazione"), DropDownList)
        Dim emissione As TextBox = CType(e.Item.FindControl("sel1"), TextBox)
        Dim scadenza As TextBox = CType(e.Item.FindControl("sel2"), TextBox)
        Dim nome As TextBox = CType(e.Item.FindControl("nome"), TextBox)
        Dim cognome As TextBox = CType(e.Item.FindControl("cognome"), TextBox)
        Dim telefono As TextBox = CType(e.Item.FindControl("telefono"), TextBox)
        Dim iemissione As Label = CType(e.Item.FindControl("iemissione"), Label)
        Dim iscadenza As Label = CType(e.Item.FindControl("iscadenza"), Label)
        Dim inazionalita As Label = CType(e.Item.FindControl("inazionalita"), Label)
        Dim inomereferente As Label = CType(e.Item.FindControl("inomereferente"), Label)
        Dim icognomereferente As Label = CType(e.Item.FindControl("icognomereferente"), Label)
        Dim itelreferente As Label = CType(e.Item.FindControl("itelreferente"), Label)
        Dim iluogorilascio As Label = CType(e.Item.FindControl("iluogorilascio"), Label)
        Dim iluogonascita As Label = CType(e.Item.FindControl("iluogonascita"), Label)
        Dim Luogo As TextBox = CType(e.Item.FindControl("Luogo"), TextBox)
        Dim Luogonascita As TextBox = CType(e.Item.FindControl("Luogonascita"), TextBox)

        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            contarighe = contarighe + 1
            If Not IsDBNull(dropgiorno) Then
                mettidrop(dropgiorno, 1, 31)
                mettidrop(dropmese, 1, 12)
                mettidrop(dropanno, Date.Now.Year, Date.Now.Year - 90)
                dropgiorno.SelectedValue = CDate(datanascita.Text).Day
                dropmese.SelectedValue = CDate(datanascita.Text).Month
                dropanno.SelectedValue = CDate(datanascita.Text).Year
            End If
            If Not IsDBNull(inazionalita.Text) Then
                Dropnazione.SelectedValue = inazionalita.Text
            End If
            If Not IsDBNull(iemissione.Text) Then
                sel1.Text = iemissione.Text
            End If
            If Not IsDBNull(iscadenza.Text) Then
                sel2.Text = iscadenza.Text
            End If
            If Not IsDBNull(inazionalita.Text) Then
                Dropnazione.SelectedValue = inazionalita.Text
            End If
            If Not IsDBNull(iluogonascita.Text) Then
                Luogonascita.Text = iluogonascita.Text
            End If
            If Not IsDBNull(inomereferente.Text) Then
                nome.Text = inomereferente.Text
            End If
            If Not IsDBNull(icognomereferente.Text) Then
                cognome.Text = icognomereferente.Text
            End If
            If Not IsDBNull(iluogorilascio.Text) Then
                Luogo.Text = iluogorilascio.Text
            End If
            If Not IsDBNull(itelreferente.Text) Then
                telefono.Text = itelreferente.Text
            End If
            If Not IsDBNull(docu.Text) Then
                documento.Text = docu.Text
                Dropdocumento.SelectedValue = tipodocumento.Text
            End If
            'calendariosetup.Text = calendariosetup.Text & "Calendar.setup({inputField: '" & sel1.ClientID & "', ifFormat: '%d/%m/%Y', button: '" & sel1.ClientID & "', align: 'right', singleClick: true});"
            'calendariosetup.Text = calendariosetup.Text & "Calendar.setup({inputField: '" & sel2.ClientID & "', ifFormat: '%d/%m/%Y', button: '" & sel2.ClientID & "', align: 'right', singleClick: true});"

        End If
    End Sub



    Private Sub mettidrop(ByVal ndrop As DropDownList, ByVal min As Integer, ByVal max As Integer)
        Dim i As Integer
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
    End Sub

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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim pass As Boolean = False
            Dim passaporto As Integer = 0
            Dim piulungo As Integer = 0
            If Not IsNothing(Request.Params("parsel")) And Not IsNothing(Request.Params("code")) Then
                If controllo(Request.Params("parsel"), Request.Params("code")) Then
                    If IsNumeric(Request.Params("parsel")) Then
                        Dim sqlconn As String
                        sqlconn = "SELECT periodo.passaporto, preno.passp, preno.datidoc, preno.codice, preno.email FROM preno, periodo WHERE periodo.id_periodo = preno.id_periodo AND preno.id_preno = " & Request.Params("parsel")
                        Dim cmd2 As New MySqlCommand(sqlconn, cn)
                        If cn.State = ConnectionState.Closed Then cn.Open()
                        Dim dr As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
                        If dr.HasRows Then
                            dr.Read()
                            passaporto = dr("passaporto")
                            If dr("passp") = 1 Then passaporto = 1
                            codice.Text = dr("codice")
                            email.Text = dr("email")
                            If dr("datidoc") > 0 Then
                                BttSalvadocumenti.Enabled = False
                                BttSalvadocumenti.Text = "Documenti già inseriti"
                            End If
                        End If
                        dr.Close()
                        cn.Close()
                        If passaporto = 0 Then
                            lbldocumenti.Text = "Passaporto o carta d'identità in corso di validità (minori inclusi)<br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto. <br /><br /><b>Per i minori di anni 14</b>, l’uso della carta di identità ai fini dell’espatrio è subordinato alla condizione che il minore viaggi in compagnia di uno dei genitori o di chi ne fa le veci, o che venga menzionato – su una dichiarazione rilasciata da chi può dare l’assenso o l’autorizzazione, convalidata dalla Questura o dalle Autorità consolari – il nome della persona, dell’ente o della compagnia di trasporto a cui il minore medesimo è affidato. <br /> Per i minori di anni 14 è  richiesto da parte delle Autorità di Frontiera un certificato di nascita con indicazione della paternità e della maternità da esibire alla partenza. <br />Dal 26 giugno 2012 ciascun minore deve possedere il proprio documento d'identità, passaporto o carta d'identità. Non è più considerata valida l'iscrizione sul passaporto dei genitori o il certificato di nascita con il timbro della questura."
                            pass = True
                            piulungo = 120
                        Else
                            lbldocumenti.Text = "Passaporto. Per questo itinerario è richiesto obbligatoriamente il passaporto per tutti i partecipanti (minori inclusi) con validità residua di almeno 6 mesi. <br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto. <br />Dal 26 giugno 2012 ciascun minore deve possedere il proprio passaporto individuale in quanto non è più considerato valido l'iscrizione sul passaporto dei genitori."
                        End If

                        Call caricanomi(Request.Params("parsel"))
                        Call mettiidentita(pass)
                        Dim altezza As Integer = 700 + (contarighe * 165) + piulungo
                        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza(" & altezza & ", 'frameccc');", True)
                    End If
                End If
            End If
            If Not IsNothing(Request.Params("idperiodo")) Then
                If IsNumeric(Request.Params("idperiodo")) Then
                    Dim sqlconn As String
                    sqlconn = "SELECT periodo.passaporto FROM periodo WHERE id_periodo = " & Request.Params("idperiodo")
                    Dim cmd2 As New MySqlCommand(sqlconn, cn)
                    If cn.State = ConnectionState.Closed Then cn.Open()
                    Dim dr As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
                    If dr.HasRows Then
                        dr.Read()
                        passaporto = dr("passaporto")
                    End If
                    dr.Close()
                    cn.Close()
                    If passaporto = 0 Then
                        lbldocumenti.Text = "Passaporto o carta d'identità in corso di validità (minori inclusi)<br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto. <br /><br /><b>Per i minori di anni 14</b>, l’uso della carta di identità ai fini dell’espatrio è subordinato alla condizione che il minore viaggi in compagnia di uno dei genitori o di chi ne fa le veci, o che venga menzionato – su una dichiarazione rilasciata da chi può dare l’assenso o l’autorizzazione, convalidata dalla Questura o dalle Autorità consolari – il nome della persona, dell’ente o della compagnia di trasporto a cui il minore medesimo è affidato.<br /> Per i minori di anni 14 è  richiesto da parte delle Autorità di Frontiera un certificato di nascita con indicazione della paternità e della maternità da esibire alla partenza."
                    Else
                        lbldocumenti.Text = "Passaporto. Per questo itinerario è richiesto obbligatoriamente il passaporto per tutti i partecipanti (minori inclusi). <br /> Ciascun Crocierista prima della partenza avrà la responsabilità di munirsi del documento d'identità richiesto per la crociera scelta e di verificarne la validità residua. In caso contrario non sarà possibile imbarcarsi e non sarà concesso rimborso del biglietto."
                    End If
                    BttSalvadocumenti.Visible = False
                    'lbldocu.Text = "A conferma della pratica vi chiederemmo di inserire i documenti d'identità. Se non disponibili al momento della conferma i documenti possono essere inseriti prima della partenza"
                End If
            End If
            
        End If
        lbldocumenti.Text = lbldocumenti.Text & "<br /><br /><i>Non visualizzi tutta la pagina? <a href='documenti.aspx?parsel=" & Request.Params("parsel") & "&code=" & Request.Params("code") & "&idperiodo=" & Request.Params("idperiodo") & "' target='_BLANK'>premi qui</a></i>"
    End Sub


End Class
