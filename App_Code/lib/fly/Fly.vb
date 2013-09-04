Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core

Namespace Libray.Fly
    Public Class Fly
        Inherits Entity
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
        End Sub
    End Class
End Namespace

