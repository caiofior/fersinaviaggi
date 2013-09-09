Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core
Imports System.Collections.Specialized

Namespace Libray.Fly
    Public Class SearchCollection
        Inherits EntityCollection
        Protected _tablename As String = "search"
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
        End Sub
        Overrides Function createNewItem() As Entity
            Dim item As Search
            item = New Search(_connection)
            Return item
        End Function
        Public Overrides Function createSQL(ByVal filters As NameValueCollection) As String
            filters.Add("limit", "10")
            Dim sql As String
            sql = "SELECT * "
            sql = sql + " FROM " + _tablename + " WHERE TRUE "

            Return sql
        End Function
    End Class
End Namespace

