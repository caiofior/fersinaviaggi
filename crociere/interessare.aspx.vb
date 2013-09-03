Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class crociere_interessare
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)

    Dim objdiv As String = ""
    Dim objlnk As String = ""
    Dim contatore As Integer = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim sqlconn As String
        Dim datapart As Date = Request.Params("dal")
        Dim idperiodo As Integer = Request.Params("idperiodo")
        Dim typeiti As Integer = Request.Params("typeiti")
        Dim imbarco As String = Request.Params("imbarco")
        Dim datap As Date = DateAdd(DateInterval.Day, -5, datapart)
        Dim datad As Date = DateAdd(DateInterval.Day, 5, datapart)
        Dim fraseimbarco As String = ""
        fraseimbarco = " and imbarco = '" & imbarco & "'"
        If imbarco = "SAVONA" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'GENOVA') "
        End If
        If imbarco = "GENOVA" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'SAVONA') "
        End If
        If imbarco = "TRIESTE" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'VENEZIA') "
        End If
        If imbarco = "VENEZIA" And typeiti = 3 Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'TRIESTE') "
        End If
        If imbarco = "LIVORNO" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'GENOVA') "
        End If
        If imbarco = "GENOVA" And typeiti = 6 Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'LIVORNO') "
        End If
        If imbarco = "CAPRI" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'SALERNO') "
        End If
        If imbarco = "SALERNO" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'CAPRI') "
        End If
        If imbarco = "CAGLIARI" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'OLBIA') "
        End If
        If imbarco = "OLBIA" Then
            fraseimbarco = " and (imbarco = '" & imbarco & "' OR imbarco = 'CAGLIARI') "
        End If
        sqlconn = "SELECT * from periodo, nave where periodo.id_nave = nave.id_nave AND periodo.pubblica = 0 AND  typeiti = " & typeiti & "  and dal>= now() and (dal >= '" & datap.Year & "-" & inseriscizero(datap.Month) & "-" & inseriscizero(datap.Day) & "' and dal <= '" & datad.Year & "-" & inseriscizero(datad.Month) & "-" & inseriscizero(datad.Day) & "') and id_periodo <> " & idperiodo & fraseimbarco & " order by dal LIMIT 1" 'al momento solo 1
        'sqlconn = "SELECT * from periodo, nave where periodo.id_nave = nave.id_nave AND periodo.pubblica = 0 AND  typeiti = " & typeiti & "  and dal>= now() and (dal >= '" & datap.Year & "-" & inseriscizero(datap.Month) & "-" & inseriscizero(datap.Day) & "' and dal <= '" & datad.Year & "-" & inseriscizero(datad.Month) & "-" & inseriscizero(datad.Day) & "') " & fraseimbarco & " order by dal "
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            RepeaterInteressi.DataSource = dr
            RepeaterInteressi.DataBind()
            If (contatore) > 1 Then 'se ci sono più offerte
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", objdiv & objlnk & "javascript:caricaprimo(); scrollinte(" & (contatore - 1) & ")", True)
            ElseIf contatore = 1 Then
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", objdiv & objlnk & "javascript:caricaprimo();", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "javascript:closex();", True)
            End If
        Else
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "javascript:closex();", True)
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

    Protected Sub RepeaterInteressi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles RepeaterInteressi.ItemDataBound
        Dim lbli As Label = CType(e.Item.FindControl("lbli"), Label)
        Dim lble As Label = CType(e.Item.FindControl("lble"), Label)
        Dim lblb As Label = CType(e.Item.FindControl("lblb"), Label)
        Dim Labeli As Label = CType(e.Item.FindControl("Labeli"), Label)
        Dim Labele As Label = CType(e.Item.FindControl("Labele"), Label)
        Dim Labelb As Label = CType(e.Item.FindControl("Labelb"), Label)
        Dim idperiodo As Label = CType(e.Item.FindControl("idperiodo"), Label)
        Dim clicca As System.Web.UI.HtmlControls.HtmlGenericControl = CType(e.Item.FindControl("clicca"), System.Web.UI.HtmlControls.HtmlGenericControl)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            If lbli.Text = 0 Then
                lbli.Visible = False
                Labeli.Visible = False
            End If
            If lble.Text = 0 Then
                lble.Visible = False
                Labele.Visible = False
            End If
            If lblb.Text = 0 Then
                lblb.Visible = False
                Labelb.Visible = False
            End If
            'If CInt(idperiodo.Text) > 0 Then
            '    clicca.Attributes.Add("onclick", "javascript:top.document.location.href='offerte-crociere.aspx?id=" & idperiodo.Text & "'")
            'End If
            If CInt(idperiodo.Text) > 0 Then
                objlnk = objlnk & "objlnk[" & contatore & "] = 'javascript:top.document.location.href=""offerte-crociere.aspx?id=" & idperiodo.Text & """';"
            End If
            objdiv = objdiv & "objdiv[" & contatore & "] = document.getElementById('" & clicca.ClientID & "');"
            contatore = contatore + 1
        End If
    End Sub
End Class
