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
        Public Overrides Sub update()
            If (getData("arrival_datetime") = "") Then
                setData("arrival_datetime", "NULL")
            End If
            MyBase.update()
        End Sub

        Sub loadFromId(ByVal id As Integer)
            Dim sql As String = "SELECT * FROM " + _tablename + " WHERE id = """ + Util.sqlSanitize(id) + """"
            'Try
            loadFromSql(sql)
            'Catch ex As Exception
            'End Try

        End Sub

    End Class
End Namespace

