Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Collections.Specialized

Namespace Libray.Core
    Public MustInherit Class Entity
        Protected _connection As MySql.Data.MySqlClient.MySqlConnection
        Protected _data As New NameValueCollection
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            _connection = connection
        End Sub
        Public Sub setData(ByVal field As String, ByVal value As String)
            _data.Add(field, value)
        End Sub
        Public Function getData(ByVal field As String) As String
            Return _data.Get(field)
        End Function
        Public Sub loadFromSql(ByVal sql As String)
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            reader.Read()
            For columCount As Integer = 0 To reader.FieldCount - 1
                setData(reader.GetName(columCount).ToString, reader.GetValue(columCount).ToString)
            Next
            reader.Close()

        End Sub
    End Class
End Namespace

