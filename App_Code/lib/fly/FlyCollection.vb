Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Namespace Libray.Fly
    Public Class FlyCollection
        Private _tablename As String = "fly_availability"
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
            sql = "SELECT * "
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".departure_location_info) AS departure_location_name"
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".arrival_location_info) AS arrival_location_name"
            sql = sql + " FROM " + _tablename + " "
            sql = sql + " LIMIT " + CInt(filters.Get("limit")).ToString
            Return sql
        End Function
    End Class
End Namespace

