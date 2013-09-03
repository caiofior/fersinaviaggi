Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class assi
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim altezza As Integer = 780
        ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
        If Not Page.IsPostBack Then
            Call caricaassi(Request.Params("parsel"))
        End If
    End Sub

    Private Sub caricaassi(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT preno.*, periodo.*, itinerario.*, nave.titolo as titolonave, periodo.durata as duratat FROM preno, nave, periodo, itinerario WHERE periodo.id_periodo = preno.id_periodo AND itinerario.id_itinerario = periodo.id_itinerario AND  nave.id_nave = preno.id_nave AND preno.id_preno = '" & idpreno & "'"
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("compagnia") = 0 Then
                If dr("id_periodo") <> 4222 Then
                    If DateDiff(DateInterval.Day, CDate(dr("data_preno")), CDate("01-09-2013")) >= 0 Then
                        frameassi.Attributes.Add("src", "MSC_ASSICURAZIONE_ITALIA.pdf")
                        visualizza.NavigateUrl = "MSC_ASSICURAZIONE_ITALIA.pdf"
                    End If
                End If
            ElseIf dr("compagnia") = 1 Then
                If DateDiff(DateInterval.Day, CDate(dr("data_preno")), CDate("01-09-2013")) >= 0 Then
                    frameassi.Attributes.Add("src", "ASSICURAZIONE_COSTA.pdf")
                    visualizza.NavigateUrl = "ASSICURAZIONE_COSTA.pdf"
                End If
            End If
        End If
        dr.Close()
        cn.Close()
    End Sub
End Class
