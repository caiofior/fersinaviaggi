Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Namespace Libray.Core
    Public MustInherit Class EntityCollection
        Protected _items As New Queue
        Protected _connection As MySql.Data.MySqlClient.MySqlConnection
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            _connection = connection
        End Sub
        Public Sub LoadAll(ByVal filters As NameValueCollection)
            Dim sql As String = createSQL(filters)
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            Dim item As Entity
            While reader.Read()
                item = createNewItem()
                For columCount As Integer = 0 To reader.FieldCount - 1
                    item.setData(reader.GetName(columCount).ToString, reader.GetValue(columCount).ToString)
                Next
                _items.Enqueue(item)
            End While
            reader.Close()
        End Sub
        Public Function getDataset(ByVal filters As NameValueCollection) As DataSet
            Dim flyDataset As New DataSet()
            Dim sql As String = createSQL(filters)
            Dim adapter As New MySqlDataAdapter(sql, _connection)
            adapter.Fill(flyDataset)
            Return flyDataset
        End Function
        Public Function getItems() As Queue
            Return _items
        End Function
        Public MustOverride Function createSQL(ByVal filters As NameValueCollection) As String
        Public MustOverride Function createNewItem() As Entity
    End Class
End Namespace

