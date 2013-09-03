Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_itinerario
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim primo As Boolean = True
    Dim giornoArray As String() = New String() {"Domenica", "Lunedì", "Martedì", "Mercoledì", "Giovedì", "Venerdì", "Sabato"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}
    Dim contaiti As Integer = 0
    Dim contatore As Integer = 0
    Private Sub caricaiti(ByVal iditi As Integer, ByVal datadal As Date, ByVal dataal As Date, ByVal portopartenza As String, ByVal portoarrivo As String, ByVal rpt As Repeater)
        Dim sqlconn As String
        sqlconn = "SELECT * FROM tipoitinerario WHERE id_itinerario = " & iditi & " ORDER BY giorno"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim ds As New dspreno
        Dim da As New MySqlDataAdapter(sqlconn, cn)
        da.Fill(ds, "itinerario")
        da.Fill(ds, "itinerario")
        Dim giorni As Integer = DateDiff(DateInterval.Day, datadal, dataal)
        Dim dts As DataTable = ds.Tables("itinerario")
        Dim dtn As DataTable = ds.Tables("giorni")
        Dim datap As Date = datadal
        Dim gg As Integer = 0
        Dim esegui As Boolean = False
        Dim ieri As Integer = -1
        For Each drs As DataRow In dts.Rows
            Dim drn As DataRow = dtn.NewRow
            Dim appart As String = drs("luogo").ToString.ToUpper
            If appart.IndexOf(Trim(portopartenza)) >= 0 Then
                esegui = True
            End If
            If gg <= giorni And esegui Then
                drn("id_itinerario") = drs("id_itinerario")
                drn("giorno") = datap
                drn("luogo") = drs("luogo")
                If gg = giorni Then
                    drn("partenza") = "00:00"
                    If portoarrivo <> portopartenza Then
                        drn("luogo") = portoarrivo
                    End If
                Else
                    drn("partenza") = drs("partenza")
                End If
                drn("arrivo") = drs("arrivo")

                If drs("giorno") <> ieri Then
                    gg = gg + 1
                Else
                    datap = DateAdd(DateInterval.Day, -1, datap)
                    drn("giorno") = datap
                End If
                dtn.Rows.Add(drn)
                ieri = drs("giorno")
                datap = DateAdd(DateInterval.Day, 1, datap)
            End If
        Next
        rpt.DataSource = dtn
        rpt.DataBind()
        cn.Close()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("id")) Then
            If IsNumeric(Request.Params("id")) Then
                If Not Page.IsPostBack Then
                    caricadati(Request.Params("id"))
                    If (Request.Params("id")) = 4222 Then
                        framevideo.Visible = True
                        contatore = 30
                    End If

                    If panelroulette.Visible = True Then contatore = contatore + 8
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & contatore & "')", True)
                End If
            End If
        End If
    End Sub


    Private Sub caricadati(ByVal idperiodo As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT periodo.*, itinerario.* FROM periodo, itinerario WHERE periodo.id_itinerario = itinerario.id_itinerario AND periodo.id_periodo = " & idperiodo
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim imb As String = ""
        Dim sba As String = ""
        Dim imb2 As String = ""
        Dim sba2 As String = ""
        Dim dal, al, dal2, al2 As Date
        Dim id_itinerario As Integer
        Dim id_itinerario2 As Integer
        Dim roulette As Boolean = False
        Dim ima2 As String = ""
        If dr.HasRows Then
            dr.Read()
            dal = CDate(dr.GetMySqlDateTime("dal"))
            al = CDate(dr.GetMySqlDateTime("al"))
            id_itinerario = dr("id_itinerario")
            imb = dr("imbarco")
            sba = dr("sbarco")
            ImageIti.ImageUrl = dr("mappa")
            If Left(dr("codiceperiodo"), 1) = "Z" Then ' roulette                
                roulette = True
                Dim ricava(3) As String
                Dim ricava2(3) As String
                ricava = ricaviti(dr("id_nave"), dr("dal"))
                If Not IsDBNull(dr("dal2")) Then
                    dal2 = CDate(dr.GetMySqlDateTime("dal2"))
                    ricava2 = ricaviti(dr("id_nave2"), dr("dal2"))
                    id_itinerario2 = CInt(ricava2(0))
                    Dim xx As Integer = dr("imbarco").ToString.IndexOf("/")
                    Dim yy As Integer = dr("sbarco").ToString.IndexOf("/")
                    imb2 = ricava2(1)
                    sba2 = ricava2(2)
                    If xx >= 0 Then
                        imb = Left(dr("imbarco"), xx)
                        Dim appx As Integer = dr("imbarco").ToString.Length
                        Dim appi As String = Right(dr("imbarco"), appx - xx - 1)
                        imb2 = appi
                    End If
                    If yy >= 0 Then
                        sba = Left(dr("sbarco"), yy)
                        Dim appx As Integer = dr("sbarco").ToString.Length
                        Dim appi As String = Right(dr("sbarco"), appx - yy - 1)
                        sba2 = appi
                    End If
                    
                    ImageIti2.ImageUrl = ricava2(3)
                Else
                    ricava2 = ricaviti(dr("id_nave2"), dr("dal"))
                    id_itinerario2 = CInt(ricava2(0))
                    imb2 = ricava2(1)
                    sba2 = ricava2(2)
                    Imageiti2.ImageUrl = ricava2(3)
                    dal2 = CDate(dr.GetMySqlDateTime("dal"))
                    al2 = CDate(dr.GetMySqlDateTime("al"))
                End If
                If Not IsDBNull(dr("al2")) Then
                    al2 = CDate(dr.GetMySqlDateTime("al2"))
                End If
                id_itinerario = CInt(ricava(0))
                imb = ricava(1)
                sba = ricava(2)
                ImageIti.ImageUrl = ricava(3)
            End If
        End If
        dr.Close()
        cn.Close()
        Call caricaiti(id_itinerario, dal, al, imb, sba, Repeateriti)
        If roulette = True Then
            Call caricaiti(id_itinerario2, dal2, al2, imb2, sba2, Repeateriti2)
            panelroulette.Visible = True
            lblroulette.Visible = True
        End If
    End Sub

    Function ricaviti(ByVal idnave As Integer, ByVal dal As Date) As String()
        Dim app(3) As String
        Dim sqlconn As String
        sqlconn = "SELECT periodo.id_itinerario, periodo.imbarco, periodo.sbarco, itinerario.mappa FROM periodo, itinerario WHERE periodo.id_itinerario = itinerario.id_itinerario AND periodo.pubblica = 0 AND left(periodo.codiceperiodo,1) <> 'Z' and periodo.id_nave = " & idnave & " AND periodo.dal = '" & dal.Year & "-" & inseriscizero(dal.Month) & "-" & inseriscizero(dal.Day) & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            app(0) = dr("id_itinerario")
            app(1) = dr("imbarco")
            app(2) = dr("sbarco")
            app(3) = dr("mappa")
        End If
        dr.Close()
        cn2.Close()
        ricaviti = app
    End Function



    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Protected Sub Repeateriti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeateriti.ItemDataBound
        Dim oraarrivo As Label = CType(e.Item.FindControl("oraarrivo"), Label)
        Dim orapartenza As Label = CType(e.Item.FindControl("orapartenza"), Label)
        Dim orarioa As Label = CType(e.Item.FindControl("orarioa"), Label)
        Dim orariop As Label = CType(e.Item.FindControl("orariop"), Label)
        Dim giorno As Label = CType(e.Item.FindControl("giorno"), Label)
        Dim gg As Label = CType(e.Item.FindControl("gg"), Label)
        Dim gg2 As Label = CType(e.Item.FindControl("gg2"), Label)
        Dim label6 As Label = CType(e.Item.FindControl("label6"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If label6.Text.IndexOf(",") > -1 Then
                label6.Text = Left(label6.Text, label6.Text.IndexOf(",")) & "<span style='font-size:xx-small'>" & Right(label6.Text, label6.Text.Length - label6.Text.IndexOf(",")) & "</span>"
            End If
            Dim ora As Date
            If orapartenza.Text <> "" Then
                ora = CDate(orapartenza.Text)
                orariop.Text = Format(ora, "HH:mm")
                If ora = "00:00:00" Then
                    orariop.Text = "-------"
                End If
            End If
            If oraarrivo.Text <> "" Then
                ora = CDate(oraarrivo.Text)
                orarioa.Text = Format(ora, "HH:mm")
                If ora = "00:00:00" Then
                    orarioa.Text = "-------"
                End If
            End If
            If giorno.Text <> "" Then
                gg.Text = Format(CDate(giorno.Text), "dd/MM")
                gg2.Text = Left(giornoArray(CDate(giorno.Text).DayOfWeek), 3)
            End If
            If primo Then
                orarioa.Text = "-------"
                primo = False
            End If
            contatore = contatore + 1
            If contatore = contaiti Then
                orariop.Text = "-------"
            End If
        End If
    End Sub



    Protected Sub Repeateriti2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeateriti2.ItemDataBound
        Dim oraarrivo As Label = CType(e.Item.FindControl("oraarrivo"), Label)
        Dim orapartenza As Label = CType(e.Item.FindControl("orapartenza"), Label)
        Dim orarioa As Label = CType(e.Item.FindControl("orarioa"), Label)
        Dim orariop As Label = CType(e.Item.FindControl("orariop"), Label)
        Dim giorno As Label = CType(e.Item.FindControl("giorno"), Label)
        Dim gg As Label = CType(e.Item.FindControl("gg"), Label)
        Dim gg2 As Label = CType(e.Item.FindControl("gg2"), Label)
        Dim label6 As Label = CType(e.Item.FindControl("label6"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If label6.Text.IndexOf(",") > -1 Then
                label6.Text = Left(label6.Text, label6.Text.IndexOf(",")) & "<span style='font-size:xx-small'>" & Right(label6.Text, label6.Text.Length - label6.Text.IndexOf(",")) & "</span>"
            End If
            Dim ora As Date
            If orapartenza.Text <> "" Then
                ora = CDate(orapartenza.Text)
                orariop.Text = Format(ora, "HH:mm")
                If ora = "00:00:00" Then
                    orariop.Text = "-------"
                End If
            End If
            If oraarrivo.Text <> "" Then
                ora = CDate(oraarrivo.Text)
                orarioa.Text = Format(ora, "HH:mm")
                If ora = "00:00:00" Then
                    orarioa.Text = "-------"
                End If
            End If
            If giorno.Text <> "" Then
                gg.Text = Format(CDate(giorno.Text), "dd/MM")
                gg2.Text = Left(giornoArray(CDate(giorno.Text).DayOfWeek), 3)
            End If
            If primo Then
                orarioa.Text = "-------"
                primo = False
            End If
            contatore = contatore + 1
            If contatore = contaiti Then
                orariop.Text = "-------"
            End If
        End If
    End Sub
End Class
