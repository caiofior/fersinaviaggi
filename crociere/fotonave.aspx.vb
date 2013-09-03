Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_itinerario
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim primo As Boolean = True        
    Dim contafoto As Integer = 0
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("id")) Then
            If IsNumeric(Request.Params("id")) Then
                If Not Page.IsPostBack Then
                    caricadati(Request.Params("id"))                    
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza2('730', 'framenave')", True)
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
        sqlconn = "SELECT * FROM fotonave WHERE id_nave  = " & id_nave & " order by fotop"
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
            Repeaterfotonave.DataSource = dr2
            Repeaterfotonave.DataBind()
        End If
        dr2.Close()
        Dim cmd3 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr3 As MySqlDataReader = cmd3.ExecuteReader(CommandBehavior.CloseConnection)
        If dr3.HasRows Then
            dr3.Read()
            'Dim fotog As System.Web.UI.HtmlControls.HtmlImage = CType(Page.FindControl("fotog"), System.Web.UI.HtmlControls.HtmlImage)
            fotog.Src = dr3("foto")
            fotog.Alt = dr3("descrifotonave")        
        End If
        dr3.Close()
        cn.Close()

    End Sub


    Protected Sub Repeaterfotonave_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeaterfotonave.ItemDataBound
        Dim riga As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("riga"), System.Web.UI.HtmlControls.HtmlGenericControl)
        Dim fotop As System.Web.UI.HtmlControls.HtmlImage = CType(e.Item.FindControl("fotop"), System.Web.UI.HtmlControls.HtmlImage)
        Dim foto As Label = CType(e.Item.FindControl("foto"), Label)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If contafoto < 2 Then riga.Style.Add("float", "none")
            contafoto = contafoto + 1
            fotop.Style.Add("cursor", "pointer")
            fotop.Attributes.Add("onmouseover", "javascript:cambiafoto('" & foto.Text & "')")
        End If
    End Sub
End Class
