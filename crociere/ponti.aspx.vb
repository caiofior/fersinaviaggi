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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsNothing(Request.Params("idnave")) Then
            If IsNumeric(Request.Params("idnave")) Then
                Call caricanave(Request.Params("idnave"))
                Dim altezza As Integer = 260
                altezza = altezza + (contarighe * 90)
                ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", "altezza('" & altezza & "', 'frameccc');", True)
            End If
        End If
    End Sub

    Private Sub caricanave(ByVal idnave As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from nave WHERE id_nave = " & idnave
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            dr.Read()
            Labeltitolo.Text = dr("titolo")
            If dr("piani") <> "" Then
                imagepiani.ImageUrl = dr("piani")
            Else
                Dim rigap As System.Web.UI.HtmlControls.HtmlGenericControl = CType(Page.FindControl("rigap"), System.Web.UI.HtmlControls.HtmlGenericControl)
                rigap.Visible = False
            End If
        End If
        dr.Close()
        cn.Close()
        Call caricaponti(idnave)
    End Sub
    Private Sub caricaponti(ByVal idnave As Integer)
        Dim sqlconn As String
        sqlconn = "SELECT * from ponti WHERE id_nave = " & idnave
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            Repeaterponti.DataSource = dr
            Repeaterponti.DataBind()
        End If
        dr.Close()
        cn.Close()
    End Sub

    Protected Sub Repeaterponti_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles Repeaterponti.ItemDataBound
        Dim imageponte As System.Web.UI.WebControls.Image = CType(e.Item.FindControl("imageponte"), System.Web.UI.WebControls.Image)
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim pos As Integer = imageponte.ImageUrl.IndexOf(".gif")
            Dim app As String = Left(imageponte.ImageUrl, pos)
            imageponte.ImageUrl = app & "o.gif"
            contarighe = contarighe + 1
            imageponte.Attributes.Add("onclick", "javascript:ruota('" & app & "v.gif" & "');")
        End If
    End Sub
End Class
