Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core

Namespace Libray.Fly
    Public Class Airport
        Inherits Entity
        Protected _tablename As String = "airport"
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
        End Sub

        Sub loadFromIata(ByVal iata As String)
            Dim sql As String = "SELECT * FROM " + _tablename + " WHERE iata = """ + Util.sqlSanitize(iata) + """"
            loadFromSql(sql)
        End Sub

    End Class
End Namespace

