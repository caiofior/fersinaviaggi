Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_navedove
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("id")) Then
            If IsNumeric(Request.Params("id")) Then
                If Not Page.IsPostBack Then
                    caricaiti(Request.Params("id"))
                    'ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza2('730', 'framenave')", True)
                End If
            End If
        End If
    End Sub

    Private Sub caricaiti(ByVal idnave As Integer)
        Dim sqlconn As String
        ' sqlconn = "SELECT itinerario.id_itinerario, itinerario.mappa, periodo.imbarco, count(itinerario.id_itinerario) as conta from periodo, itinerario where periodo.pubblica = 0 AND itinerario.id_itinerario = periodo.id_itinerario and periodo.id_nave = " & idnave & " and periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' group by itinerario.id_itinerario, itinerario.mappa, periodo.imbarco "
        sqlconn = "SELECT periodo.id_periodo, itinerario.mappa, periodo.dal, periodo.imbarco, count(itinerario.id_itinerario) as conta, itinerario.durata  from periodo, itinerario where itinerario.id_itinerario = periodo.id_itinerario and periodo.id_nave = " & idnave & " and periodo.dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' group by itinerario.id_itinerario, itinerario.mappa, periodo.imbarco, itinerario.durata order by periodo.dal"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        Dim id_nave As Integer = 0
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Repeaternave.DataSource = dr
            Repeaternave.DataBind()
        End If
        dr.Close()
        cn.Close()

    End Sub

    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function


    Protected Sub Repeaternave_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeaternave.ItemDataBound
        Dim Labelconta As Label = CType(e.Item.FindControl("Labelconta"), Label)
        Dim Labeldescri As Label = CType(e.Item.FindControl("Labeldescri"), Label)
        Dim durata As Label = CType(e.Item.FindControl("durata"), Label)
        Dim imbarco As Label = CType(e.Item.FindControl("imbarco"), Label)
        Dim dal As Label = CType(e.Item.FindControl("dal"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim imb As String
            If imbarco.Text.IndexOf(",") >= 0 Then
                imb = Left(imbarco.Text, imbarco.Text.IndexOf(","))
            Else
                imb = imbarco.Text
            End If
            If imb.Length > 22 Then
                imb = Left(imb, 22)
            End If
            If Labelconta.Text > 1 Then
                Labeldescri.Text = "dal " & Format(CDate(dal.Text), "dd-MM-yy") & " da " & imb & "<br /><span style='color: #067788;'>Itinerario di " & durata.Text & " notti - per <b>" & Labelconta.Text & "</b> volte.</span>"
            Else
                Labeldescri.Text = "il " & Format(CDate(dal.Text), "dd-MM-yy") & " da " & imb & "<br /><span style='color: #067788;'>Itinerario di " & durata.Text & " notti - per <b>" & Labelconta.Text & "</b> volta.</span>"
            End If

        End If
    End Sub
End Class
