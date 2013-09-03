Imports System.Net.Mail
Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_rqinfo
    Inherits System.Web.UI.Page
    Dim psswmail As String = ConfigurationSettings.AppSettings("psswmail")
    Dim miamail As String = ConfigurationSettings.AppSettings("miamail")
    Dim miosmtp As String = ConfigurationSettings.AppSettings("miosmtp")
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        LabelNo.Text = ""
        If CheckBox1.Checked = True Then
            Dim sqlconn As String
            Dim testo As String
            testo = "E-mail: " & TextEmail.Text & Chr(13)
            testo = testo & "Telefono: " & TextTelefono.Text & Chr(13)
            sqlconn = "SELECT itinerario.*, periodo.*, nave.*, nave.titolo AS titolonave, itinerario.titolo as titoloitinerario from itinerario, periodo, nave WHERE periodo.id_itinerario = itinerario.id_itinerario AND periodo.id_nave = nave.id_nave AND periodo.id_periodo = " & Request.Params("id")
            Dim cmd As New MySqlCommand(sqlconn, cn)
            If cn.State = ConnectionState.Closed Then cn.Open()
            Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            If dr.HasRows Then
                dr.Read()
                testo = testo & "imbarco: " & dr("dal") & " - " & dr("imbarco") & Chr(13)
                testo = testo & "sbarco: " & dr("al") & " - " & dr("sbarco") & Chr(13)
                testo = testo & "Nave: " & dr("titolonave") & Chr(13) & Chr(13)
            End If
            testo = testo & "Messaggio: " & Chr(13) & TextRichiesta.Text & Chr(13)
            If Me.IsValid Then
                SendMail("info@fersinaviaggi.it", "Richiesta Crociera", testo)
                Panel1.Visible = False
                Panel2.Visible = True
            End If
        Else
            LabelNo.Text = "Devi accettare le condizioni sulla privacy per inviare la richiesta!"
        End If
    End Sub

    Private Sub SendMail(ByVal maildest As String, ByVal oggetto As String, ByVal testo As String)
        Dim objMail As New MailMessage
        Dim strTemp As String = "Richiesta inviata!<br /><br /> Sarai contattato da un nostro operatore prima possibile..."
        Dim Smtpmail As New SmtpClient(miosmtp)
        Dim da As New MailAddress(miamail)
        Smtpmail.Credentials = New System.Net.NetworkCredential(miamail, psswmail)
        objMail.From = da
        objMail.Subject = oggetto
        objMail.IsBodyHtml = False
        objMail.Priority = MailPriority.High
        objMail.Body = testo
        Try ' proviamo ad inviare l'email...
            objMail.To.Add(maildest)
            Smtpmail.Send(objMail)
        Catch Ex As Exception ' si e' verificato un errore
            strTemp = "Errore nell'invio: "
            strTemp += Ex.Message
        End Try
        risposta.Text = strTemp
        objMail.Dispose()
    End Sub


End Class
