Imports Microsoft.VisualBasic
Imports MySql.Data.MySqlClient
Imports Libray.Core
Imports System.Collections.Specialized

Namespace Libray.Fly
    Public Class FlyCollection
        Inherits EntityCollection
        Protected _tablename As String = "fly_availability"
        Sub New(ByVal connection As MySql.Data.MySqlClient.MySqlConnection)
            MyBase.New(connection)
        End Sub
        Overrides Function createNewItem() As Entity
            Dim item As Fly
            item = New Fly(_connection)
            Return item
        End Function
        Public Overrides Function createSQL(ByVal filters As NameValueCollection) As String
            filters.Add("limit", "10")
            Dim sql As String
            sql = "SELECT * "
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".departure_location_info) AS departure_location_name"
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".arrival_location_info) AS arrival_location_name"
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".return_departure_location_info) AS return_departure_location_name"
            sql = sql + " , (SELECT name FROM airport WHERE airport.iata=" + _tablename + ".return_arrival_location_info) AS return_arrival_location_name"
            sql = sql + " FROM " + _tablename + " WHERE TRUE "

            If filters.Get("departure_location_info") <> "" Then
                sql = sql + " AND " + _tablename + ".departure_location_info=""" + Util.sqlSanitize(filters.Get("departure_location_info")) + """"
            End If

            If filters.Get("departure_datetime") <> "" Then
                Dim datetime As String
                datetime = Util.parseDatepicker(filters.Get("departure_datetime"))
                sql = sql + " AND " + _tablename + ".departure_datetime>""" + Util.sqlSanitize(datetime) + " 00:00:00"""
                sql = sql + " AND " + _tablename + ".departure_datetime<""" + Util.sqlSanitize(datetime) + " 23:59:59"""
            End If

            Return sql
        End Function
    End Class
End Namespace

