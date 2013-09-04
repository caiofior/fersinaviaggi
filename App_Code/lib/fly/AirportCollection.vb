Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Fly.Airport


Namespace Libray.Fly
    Public Class AirportCollection
        Private _tablename As String = "airport"
        Private _items As New Queue
        Private _connection As MySql.Data.MySqlClient.MySqlConnection

        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            _connection = connection
        End Sub
        Sub LoadAll(ByVal filters As NameValueCollection)
            Dim sql As String = createSQL(filters)
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            Dim item As Airport
            While reader.Read()
                item = New Airport(_connection)
                For columCount As Integer = 0 To reader.FieldCount - 1
                    item.setData(reader.GetName(columCount).ToString, reader.GetValue(columCount).ToString)
                Next
                _items.Enqueue(item)
            End While
        End Sub
        Public Function getDataset(ByVal filters As NameValueCollection) As DataSet
            Dim flyDataset As New DataSet()
            Dim sql As String = createSQL(filters)
            Dim adapter As New MySqlDataAdapter(sql, _connection)
            adapter.Fill(flyDataset)
            Return flyDataset
        End Function
        Private Function createSQL(ByVal filters As NameValueCollection) As String
            filters.Add("limit", "10")
            Dim sql As String
            sql = "SELECT * FROM " + _tablename + " "
            If filters.Get("term") <> "" Then
                sql = sql + " WHERE name LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " city LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " iata LIKE """ + filters.Get("term") + "%"""
            End If
            sql = sql + " ORDER BY country=""Italy"" DESC"
            sql = sql + " LIMIT " + CInt(filters.Get("limit")).ToString
            Return sql
        End Function
        Public Function getItems() As Queue
            Return _items
        End Function


    End Class
End Namespace

