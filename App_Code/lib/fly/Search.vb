Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core

Namespace Libray.Fly
    Public Class Search
        Inherits Entity
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
            _tablename = "fly_search"
        End Sub
    End Class
End Namespace

