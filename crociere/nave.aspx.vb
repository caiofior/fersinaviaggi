Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_itinerario
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim primo As Boolean = True    
    Dim contaiti As Integer = 0
    Dim contatore As Integer = 0
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("id")) Then
            If IsNumeric(Request.Params("id")) Then
                If Not Page.IsPostBack Then
                    caricadati(Request.Params("id"))                    
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza2('700', 'framenave');", True)
                End If
            End If
        End If
    End Sub
    Private Sub caricadati(ByVal idperiodo As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT periodo.*, nave.* FROM periodo, nave WHERE periodo.id_nave = nave.id_nave AND periodo.id_periodo = " & idperiodo
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            Imagenave.ImageUrl = dr("foto")
            Labeltitolo.Text = dr("titolo").ToString.ToUpper
        End If
        dr.Close()
        Dim cmd2 As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr2 As MySqlDataReader = cmd2.ExecuteReader(CommandBehavior.CloseConnection)
        If dr2.HasRows Then
            Repeaternave.DataSource = dr2
            Repeaternave.DataBind()
        End If
        dr2.Close()
        cn.Close()

    End Sub


End Class
