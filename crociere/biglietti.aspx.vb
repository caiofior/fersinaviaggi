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
        If Not IsNothing(Request.Params("parsel")) And Not IsNothing(Request.Params("code")) Then
            If controllo(Request.Params("parsel"), Request.Params("code")) Then
                If IsNumeric(Request.Params("parsel")) Then
                    Call caricabiglietti(Request.Params("parsel"))
                    Dim altezza As Integer = 260
                    altezza = altezza + (contarighe * 50)
                    ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
                End If
            End If
        End If
    End Sub

    Private Sub caricabiglietti(ByVal idpreno As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from preno WHERE id_preno = " & idpreno
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            If dr("inviatoticket") = 1 Then
                HyperTicket.Visible = True
                If Not IsDBNull(dr("file_biglietto")) Then
                    HyperTicket.NavigateUrl = "http://www.fersinaviaggi.it/pdf/" & dr("file_biglietto")
                    contarighe = contarighe + 1
                End If
                If Not IsDBNull(dr("file_biglietto2")) Then
                    If dr("file_biglietto2") <> "" Then
                        HyperTicket2.NavigateUrl = "http://www.fersinaviaggi.it/pdf/" & dr("file_biglietto2")
                        HyperTicket2.Visible = True
                        contarighe = contarighe + 1
                    End If
                End If
                If Not IsDBNull(dr("file_biglietto3")) Then
                    If dr("file_biglietto3") <> "" Then
                        HyperTicket3.NavigateUrl = "http://www.fersinaviaggi.it/pdf/" & dr("file_biglietto3")
                        HyperTicket3.Visible = True
                        contarighe = contarighe + 1
                    End If
                End If
                If Not IsDBNull(dr("file_biglietto4")) Then
                    If dr("file_biglietto4") <> "" Then
                        HyperTicket4.NavigateUrl = "http://www.fersinaviaggi.it/pdf/" & dr("file_biglietto4")
                        HyperTicket4.Visible = True
                        contarighe = contarighe + 1
                    End If
                End If
            End If
        End If
        dr.Close()
        cn.Close()
    End Sub
End Class
