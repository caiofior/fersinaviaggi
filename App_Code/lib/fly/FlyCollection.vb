Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Fly.Fly


Namespace Libray.Fly
    Public Class FlyCollection
        Private _items As New Queue
        Private _connection As MySql.Data.MySqlClient.MySqlConnection

        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            _connection = connection
        End Sub
        Sub LoadAll(ByVal filters As NameValueCollection)
            filters.Add("limit", "10")
            Dim sql As String
            sql = "SELECT * FROM airport "
            If filters.Get("term") <> "" Then
                sql = sql + " WHERE name LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " city LIKE """ + filters.Get("term") + "%"" OR "
                sql = sql + " iata LIKE """ + filters.Get("term") + "%"""
            End If
            sql = sql + " ORDER BY country=""Italy"" DESC"
            sql = sql + " LIMIT " + CInt(filters.Get("limit")).ToString
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            Dim item As Fly
            While reader.Read()
                item = New Fly(_connection)
                For columCount As Integer = 0 To reader.FieldCount - 1
                    item.setData(reader.GetName(columCount).ToString, reader.GetValue(columCount).ToString)
                Next
                _items.Enqueue(item)
            End While


        End Sub
        Public Function getItems() As Queue
            Return _items
        End Function


    End Class
End Namespace

