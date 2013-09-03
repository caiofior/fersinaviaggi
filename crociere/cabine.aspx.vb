Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_itinerario
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)       
    Dim contacabine As Integer = 0
    Dim fotona As String = ""
    Dim rigona As String = ""
    Dim valoreriga As String = ""
    Dim quante As Integer = 0
    Dim calcolo As Integer = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("id")) Then
            If IsNumeric(Request.Params("id")) Then
                If Not Page.IsPostBack Then
                    caricadati(Request.Params("id"))
                    Dim altezza As Integer = (calcolo * 25) + 150
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezzaframe=" & altezza & ";altezza2('" & altezza & "', 'framenave');" & fotona & rigona & valoreriga, True)
                End If
            End If
        End If
    End Sub
    Private Sub caricadati(ByVal idperiodo As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT periodo.*, nave.* FROM periodo, nave WHERE periodo.id_nave = nave.id_nave AND periodo.id_periodo = " & idperiodo
        Dim cmd As New MySqlCommand(sqlconn, cn)
        Dim id_nave As Integer = 0
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            id_nave = dr("id_nave")
        End If
        dr.Close()
        sqlconn = "SELECT count(id_cabine) as conto FROM cabine WHERE id_nave  = " & id_nave & " order by id_cabine"
        Dim cmd3 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr3 As MySqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)
        If dr3.HasRows Then
            dr3.Read()
            quante = dr3("conto")
        End If
        dr3.Close()
        cn.Close()
        sqlconn = "SELECT * FROM cabine WHERE id_nave  = " & id_nave & " order by id_cabine"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
            Repeatercabine.DataSource = dr2
            Repeatercabine.DataBind()
        End If
        dr2.Close()
        cn.Close()
    End Sub


    Protected Sub Repeatercabine_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeatercabine.ItemDataBound
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim fotocabinap As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("fotocabinap"), System.Web.UI.HtmlControls.HtmlImage)        
        Dim mappacabinap As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("mappacabinap"), System.Web.UI.HtmlControls.HtmlImage)
        Dim fotog As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("fotog"), System.Web.UI.HtmlControls.HtmlImage)
        Dim foto As Label = CType(e.Item.FindControl("foto"), Label)
        Dim mappa As Label = CType(e.Item.FindControl("mappa"), Label)
        Dim descricabine As Label = CType(e.Item.FindControl("descricabine"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim calcoloriga As Integer = CInt(descricabine.Text.Length / 90)

            'If calcoloriga > 5 Then calcoloriga = calcoloriga - 1
            If calcoloriga < 4 Then calcoloriga = 4
            calcolo = calcolo + calcoloriga
            calcoloriga = calcoloriga * 23
            riga.Style.Add("height", "" & calcoloriga & "px")
            fotocabinap.Style.Add("cursor", "pointer")
            mappacabinap.Style.Add("cursor", "pointer")
            fotocabinap.Attributes.Add("onclick", "javascript:cambiafoto('" & foto.Text & "', '" & riga.ClientID & "', '" & fotog.ClientID & "', '" & calcoloriga & "', '" & quante & "')")
            mappacabinap.Attributes.Add("onclick", "javascript:cambiafoto('" & mappa.Text & "', '" & riga.ClientID & "', '" & fotog.ClientID & "', '" & calcoloriga & "', '" & quante & "')")
            fotona = fotona & "fotona[" & contacabine & "]='" & fotog.ClientID & "';"
            rigona = rigona & "rigona[" & contacabine & "]='" & riga.ClientID & "';"
            valoreriga = valoreriga & "valoreriga[" & contacabine & "]='" & riga.Style.Item("height") & "';"
            contacabine = contacabine + 1
        End If
    End Sub
End Class
