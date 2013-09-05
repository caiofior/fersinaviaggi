Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core
Imports System.Collections.Specialized


Namespace Libray.Fly
    Public Class AirportCollection
        Inherits EntityCollection

        Protected _tablename As String = "airport"
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
        End Sub
        Overrides Function createNewItem() As Entity
            Dim item As Airport
            item = New Airport(_connection)
            Return item
        End Function


        Overrides Function createSQL(ByVal filters As NameValueCollection) As String
            filters.Add("limit", "10")
            Dim sql As String
            sql = "SELECT * FROM " + _tablename + " WHERE TRUE "
            If filters.Get("term") <> "" Then
                sql = sql + " AND ( name LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " city LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " iata LIKE """ + filters.Get("term") + "%"")"
            End If
            sql = sql + " ORDER BY country=""Italy"" DESC"
            sql = sql + " LIMIT " + CInt(filters.Get("limit")).ToString
            Return sql
        End Function



    End Class
End Namespace

