Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
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
    End Class
End Namespace

