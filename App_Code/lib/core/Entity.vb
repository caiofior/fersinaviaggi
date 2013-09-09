Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports System.Collections.Specialized
Imports System.Globalization
Imports System.Threading

Namespace Libray.Core
    Public MustInherit Class Entity
        Protected _connection As MySql.Data.MySqlClient.MySqlConnection
        Protected _data As New NameValueCollection
        Protected _tablename As String
        Protected _primaryKey As String
        Protected _fields() As String
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            Dim newCulture As CultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture.Clone()
            newCulture.DateTimeFormat.ShortDatePattern = "yyyy-M-dd"
            newCulture.DateTimeFormat.ShortTimePattern = "hh:mm"
            newCulture.DateTimeFormat.DateSeparator = "-"
            Thread.CurrentThread.CurrentCulture = newCulture
            _connection = connection
        End Sub
        Public Sub setData(ByVal field As String, ByVal value As String)
            _data.Set(field, value)
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
        Public Overridable Sub insert()
            getMetadata()
            Dim sql As String = "INSERT INTO `" + _tablename + "` SET "
            Dim entityValue As String
            Dim c As Integer = 0
            For Each entityValue In _data.AllKeys
                If (Array.Exists(Of String)(_fields, Function(i) i = entityValue)) Then
                    If c > 0 Then
                        sql = sql + ","
                    End If
                    Dim value As String = _data(entityValue)
                    If value = "NULL" Or value = "NOW()" Then
                        sql = sql + " `" + entityValue + "` = " + Util.sqlSanitize(value)
                    Else
                        sql = sql + " `" + entityValue + "` = """ + Util.sqlSanitize(value) + """"
                    End If

                    c = c + 1
                End If
            Next
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            sql = "SELECT LAST_INSERT_ID() FROM `" + _tablename + "`"
            command = New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            reader = command.ExecuteReader(CommandBehavior.CloseConnection)
            reader.Close()
            reader.Read()
            For columCount As Integer = 0 To reader.FieldCount - 1
                setData(_primaryKey, reader.GetValue(columCount).ToString)
            Next
            reader.Close()

        End Sub
        Public Overridable Sub update()
            getMetadata()
            Dim sql As String = "UPDATE `" + _tablename + "` SET "
            Dim entityValue As String
            Dim c As Integer = 0
            For Each entityValue In _data.AllKeys
                If (Array.Exists(Of String)(_fields, Function(i) i = entityValue)) And entityValue <> _primaryKey Then
                    If c > 0 Then
                        sql = sql + ","
                    End If
                    Dim value As String = _data(entityValue)
                    If value = "NULL" Or value = "NOW()" Then
                        sql = sql + " `" + entityValue + "` = " + Util.sqlSanitize(value)
                    Else
                        sql = sql + " `" + entityValue + "` = """ + Util.sqlSanitize(value) + """"
                    End If
                    c = c + 1

                End If
            Next
            sql = sql + " WHERE `" + _primaryKey + "` = """ + Util.sqlSanitize(getData(_primaryKey)) + """"
            Dim command As New MySqlCommand(sql, _connection)
            If _connection.State = ConnectionState.Closed Then _connection.Open()
            Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
            reader.Close()
        End Sub

        Private Sub getMetadata()
            If _fields Is Nothing Then
                Dim command As New MySqlCommand("SELECT * FROM " + _tablename + " LIMIT 0", _connection)
                If _connection.State = ConnectionState.Closed Then _connection.Open()
                Dim reader As MySqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection)
                Dim schema As DataTable = reader.GetSchemaTable()
                Dim field As DataRow
                Dim c As Integer = 0
                For Each field In schema.Rows

                    ReDim Preserve _fields(c + 1)

                    _fields(c) = field("ColumnName")
                    c = c + 1
                    If field("IsAutoIncrement") = "True" Then
                        _primaryKey = field("ColumnName")

                    End If

                Next
                reader.Close()
            End If
        End Sub

    End Class
End Namespace

