Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class ricerca
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("conncroce")
    Dim cn As New MySqlConnection(cnstring)
    Dim cn2 As New MySqlConnection(cnstring)
    Dim compagnia As String = ""
    Dim porto As String = ""
    Dim nave As String = ""
    Dim portocompagnia As String = ""
    Dim naveporto As String = ""
    Dim compagniaporto As String = ""
    Dim navecompagnia As String = ""
    Dim nomecompagnia As String() = New String() {"MSC CROCIERE", "COSTA CROCIERE"}
    Dim meseArray As String() = New String() {"Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno", "Luglio", "Agosto", "Settembre", "Ottobre", "Novembre", "Dicembre"}

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Call riempimese(DropMese, "- tutti i mesi - ")
            Call riempidrop(DropZona, "SELECT * FROM zona", "nomezona", "id_zona", "- tutte le zone -", False)
            compagnia = ricavacompagnia()
            nave = ricavanave()
            porto = ricavaporto()
            portocompagnia = ricavaportocompagnia()
            naveporto = ricavanaveporto()
            compagniaporto = ricavacompagniaporto()
            navecompagnia = ricavanavecompagnia()
            DropCompagnia.Attributes.Add("onchange", "cambia(this, 'DropNave', nave, 'indicecompagnia'); cambia(this, 'DropPorto', portocompagnia, '');")
            DropNave.Attributes.Add("onchange", "cambia(this, 'DropPorto', porto, 'indicenave'); cambia(this,'DropCompagnia', navecompagnia, '');")
            DropPorto.Attributes.Add("onchange", "cambia(this, 'DropNave', naveporto, 'indiceporto'); cambia(this, 'DropCompagnia', compagniaporto, '');")
            DropMese.Attributes.Add("onchange", "cambiaindice(this, 'indicemese');")
            DropZona.Attributes.Add("onchange", "cambiaindice(this, 'indicezona');")

            Dim caricato As String = ""            
            Dim indici As String = "indicecompagnia = ""'-1'"";indicenave = ""'-1'"";indiceporto = ""'-1'"";"
            Dim sicompagnia As Boolean = False
            Dim sinave As Boolean = False
            Dim siporto As Boolean = False
            If IsNumeric(Request.Params("compagnia")) Then
                caricato = caricato & "indicecompagnia=""'" & Request.Params("compagnia") & "'""; "
                sicompagnia = True
            End If
            If IsNumeric(Request.Params("nave")) Then
                caricato = caricato & "indicenave=""'" & Request.Params("nave") & "'""; "
                sinave = True
            End If
            If Request.Params("porto") <> "" Then                
                caricato = caricato & "indiceporto=""'" & Request.Params("porto") & "'""; "
                siporto = True
            End If
            If Request.Params("mese") <> "" Then
                caricato = caricato & "indicemese=""'" & Request.Params("mese") & "'""; "                
            End If
            If Request.Params("zona") <> "" Then
                caricato = caricato & "indicezona=""'" & Request.Params("zona") & "'""; "
            End If
            caricato = caricato & "mettiindice();"
            If sicompagnia Then
                caricato = caricato & "cambia(document.getElementById('DropCompagnia'), 'DropNave', nave, 'indicecompagnia'); cambia(document.getElementById('DropCompagnia'), 'DropPorto', portocompagnia, '');"
            End If
            If sinave Then
                caricato = caricato & "cambia(document.getElementById('DropNave'), 'DropPorto', porto, 'indicenave'); cambia(document.getElementById('DropNave'),'DropCompagnia', navecompagnia, '');"
            End If
            If siporto Then
                caricato = caricato & "cambia(document.getElementById('DropPorto'), 'DropNave', naveporto, 'indiceporto'); cambia(document.getElementById('DropPorto'), 'DropCompagnia', compagniaporto, '');"
            End If
            ButtonAnnulla.Attributes.Add("onclick", "setInit('-1', 'DropCompagnia', compagnia);setInit('-1', 'DropNave', nave);setInit('-1', 'DropPorto', porto);" & indici & "annulla();")
            ButtonCerca.Attributes.Add("onclick", "cerca();")
            ClientScript.RegisterStartupScript(Me.GetType(), "ClientScript", compagnia & nave & porto & portocompagnia & naveporto & compagniaporto & navecompagnia & "setInit('-1', 'DropCompagnia', compagnia);setInit('-1', 'DropNave', nave);setInit('-1', 'DropPorto', porto);" & caricato, True)
        End If
    End Sub

    Private Sub riempidrop(ByVal drop As DropDownList, ByVal sqlconn As String, ByVal campotext As String, ByVal campovalue As String, ByVal dici As String, ByVal porto As Boolean)
        Dim cmd As New MySqlCommand(sqlconn, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        If dr.HasRows Then
            drop.DataSource = dr
            drop.DataTextField = campotext
            drop.DataValueField = campovalue
            drop.DataBind()
        Else
            'drop.DataBind()
        End If
        dr.Close()
        Dim listi As New ListItem
        listi.Text = dici
        listi.Value = -1
        drop.Items.Insert(0, listi)
        cn.Close()
    End Sub

    Private Sub riempimese(ByVal drop As DropDownList, ByVal dici As String)
        Dim i As Integer
        Dim ds As New dspreno
        Dim dt As DataTable = ds.Tables("anno")
        For i = Date.Now.Month To 12
            Dim dr2 As DataRow = dt.NewRow
            dr2("anno") = i
            dr2("testo") = meseArray(i - 1) & " " & Date.Now.Year
            dt.Rows.Add(dr2)
        Next
        For i = 1 To 12
            Dim dr2 As DataRow = dt.NewRow
            dr2("anno") = i + 12
            dr2("testo") = meseArray(i - 1) & " " & Date.Now.Year + 1
            dt.Rows.Add(dr2)
        Next
        drop.DataSource = ds.Tables("anno")
        drop.DataTextField = "testo"
        drop.DataValueField = "anno"
        drop.DataBind()
        Dim listi As New ListItem
        listi.Text = dici
        listi.Value = -1
        drop.Items.Insert(0, listi)
    End Sub

    Function ricavacompagnia() As String
        Dim app As String = ""
        app = app & "compagnia['-1'] = ["
        app = app & "{value:'0', text:'MSC CROCIERE'},"
        app = app & "{value:'1', text:'COSTA CROCIERE'}"
        app = app & "];"
        app = app & "compagnia['0'] = ["
        app = app & "{value:'0', text:'MSC CROCIERE'}"
        app = app & "];"
        app = app & "compagnia['1'] = ["
        app = app & "{value:'1', text:'COSTA CROCIERE'}"
        app = app & "];"
        ricavacompagnia = app
    End Function


    Function ricavanave() As String
        Dim app As String = ""
        app = riempinave(-1) ' tutti
        Dim sql As String
        sql = "SELECT compagnia FROM periodo GROUP BY compagnia"
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempinave(dr("compagnia"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavanave = app
    End Function


    Function inseriscizero(ByVal num As Integer) As String
        If num < 10 Then
            inseriscizero = "0" & num
        Else
            inseriscizero = num
        End If
    End Function

    Function riempinave(ByVal tipocompagnia As Integer) As String
        riempinave = ""
        Dim sql As String
        sql = "select id_nave, titolo From nave WHERE id_nave <>5 AND compagnia2 = " & tipocompagnia & " order by titolo"
        If tipocompagnia = -1 Then
            sql = "select id_nave, titolo From nave where id_nave <> 5 order by compagnia2, titolo"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        Dim app As String = ""
        If dr.HasRows Then
            app = "nave['" & tipocompagnia & "']=["
            Do While dr.Read()
                app = app & "{value:'" & dr("id_nave") & "', text:'" & dr("titolo") & "'},"
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempinave = app
    End Function

    Function riempiporto(ByVal id_nave As Integer) As String
        riempiporto = ""
        Dim sql As String
        sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco FROM periodo WHERE id_nave = " & id_nave & " AND dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' GROUP BY TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) ORDER BY imbarco"
        If id_nave = -1 Then
            sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco FROM periodo WHERE dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' GROUP BY TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) ORDER BY imbarco"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        Dim app As String = ""
        If dr.HasRows Then
            app = "porto['" & id_nave & "']=["
            Do While dr.Read()
                If dr("imbarco") <> "" Then
                    app = app & "{value:'" & dr("imbarco") & "', text:'" & dr("imbarco") & "'},"
                End If
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempiporto = app
    End Function

    Function ricavaporto() As String
        Dim app As String = ""
        app = riempiporto(-1)
        Dim sql As String
        sql = "SELECT * FROM nave where id_nave <> 5"
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempiporto(dr("id_nave"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavaporto = app
    End Function

    Function ricavaportocompagnia() As String
        Dim app As String = ""
        app = riempiportocompagnia(-1)
        Dim sql As String
        sql = "SELECT compagnia FROM periodo group by compagnia"
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempiportocompagnia(dr("compagnia"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavaportocompagnia = app
    End Function

    Function riempiportocompagnia(ByVal id_compagnia As Integer) As String
        riempiportocompagnia = ""
        Dim sql As String
        sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco FROM periodo WHERE compagnia = " & id_compagnia & " AND dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' GROUP BY TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) ORDER BY imbarco"
        If id_compagnia = -1 Then
            sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco FROM periodo WHERE dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' GROUP BY TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) ORDER BY imbarco"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        Dim app As String = ""
        If dr.HasRows Then
            app = "portocompagnia['" & id_compagnia & "']=["
            Do While dr.Read()
                If dr("imbarco") <> "" Then
                    app = app & "{value:'" & dr("imbarco") & "', text:'" & dr("imbarco") & "'},"
                End If
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempiportocompagnia = app
    End Function

    Function ricavanaveporto() As String
        Dim app As String = ""
        app = riempinaveporto(-1)
        Dim sql As String
        sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco  FROM periodo where dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' group by TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) "
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempinaveporto(dr("imbarco"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavanaveporto = app
    End Function

    Function riempinaveporto(ByVal porto As String) As String
        Dim sql As String
        sql = "SELECT nave.id_nave, nave.titolo FROM periodo, nave where  dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' AND nave.id_nave <> 5 AND nave.id_nave = periodo.id_nave AND periodo.imbarco like '%" & porto & "%' group by nave.id_nave ORDER BY nave.titolo"
        If porto = "-1" Then
            sql = "SELECT nave.id_nave, nave.titolo FROM periodo, nave where dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' AND nave.id_nave <> 5 AND nave.id_nave = periodo.id_nave group by nave.id_nave ORDER BY nave.titolo"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        Dim app As String = ""
        If dr.HasRows Then
            app = "naveporto['" & porto & "']=["
            Do While dr.Read()
                app = app & "{value:'" & dr("id_nave") & "', text:'" & dr("titolo") & "'},"
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempinaveporto = app
    End Function

    Function ricavacompagniaporto() As String
        Dim app As String = ""
        app = riempicompagniaporto(-1)
        Dim sql As String
        sql = "SELECT TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) as imbarco  FROM periodo where dal >= '" & Date.Now.Year & "-" & inseriscizero(Date.Now.Month) & "-" & inseriscizero(Date.Now.Day) & "' group by TRIM(SUBSTRING_INDEX(imbarco, ',', 1)) "
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempicompagniaporto(dr("imbarco"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavacompagniaporto = app
    End Function


    Function riempicompagniaporto(ByVal porto As String) As String
        Dim sql As String
        sql = "SELECT compagnia FROM periodo where imbarco like '%" & porto & "%' group by compagnia ORDER BY compagnia"
        If porto = "-1" Then
            sql = "SELECT compagnia FROM periodo group by compagnia ORDER BY compagnia"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        Dim app As String = ""
        If dr.HasRows Then
            app = "compagniaporto['" & porto & "']=["
            Do While dr.Read()
                app = app & "{value:'" & dr("compagnia") & "', text:'" & nomecompagnia(dr("compagnia")) & "'},"
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempicompagniaporto = app
    End Function

    Function ricavanavecompagnia() As String
        Dim app As String = ""
        app = riempinavecompagnia(-1)
        Dim sql As String
        sql = "SELECT id_nave FROM nave where id_nave <> 5 order by compagnia2, titolo"
        Dim cmd As New MySqlCommand(sql, cn)
        If cn.State = ConnectionState.Closed Then cn.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0
        If dr.HasRows Then
            Do While dr.Read
                app = app & riempinavecompagnia(dr("id_nave"))
            Loop
        End If
        dr.Close()
        cn.Close()
        ricavanavecompagnia = app
    End Function

    Function riempinavecompagnia(ByVal id_nave As Integer) As String
        Dim app As String = ""
        Dim sql As String
        sql = "select compagnia2 From nave WHERE id_nave = " & id_nave
        If id_nave = -1 Then
            sql = "select compagnia2 From nave group by compagnia2"
        End If
        Dim cmd As New MySqlCommand(sql, cn2)
        If cn2.State = ConnectionState.Closed Then cn2.Open()
        Dim dr As MySqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
        Dim i As Integer = 0        
        If dr.HasRows Then
            app = "navecompagnia['" & id_nave & "']=["
            Do While dr.Read()
                app = app & "{value:'" & dr("compagnia2") & "', text:'" & nomecompagnia(dr("compagnia2")) & "'},"
            Loop
            app = Left(app, app.Length - 1)
            app = app & "];"
        End If
        dr.Close()
        cn2.Close()
        riempinavecompagnia = app
    End Function


End Class
