﻿Imports Libray.Fly
Imports MySql.Data.MySqlClient
Imports Libray.Core

Partial Class Results
    Inherits System.Web.UI.Page
    Dim connectionString As String = ConfigurationSettings.AppSettings("connvoli")
    Dim connection As New MySqlConnection(connectionString)
    Public departure As New Airport(connection)
    Public arrival As New Airport(connection)
    Protected search As Search
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filter As New NameValueCollection(Context.Request.QueryString)
        departure.loadFromIata(Request.Form("departure_location_info"))
        arrival.loadFromIata(Request.Form("arrival_location_info"))
        departure_location_name.Text = departure.getData("name") + " (" + departure.getData("country") + ")"
        arrival_location_name.Text = arrival.getData("name") + " (" + arrival.getData("country") + ")"

        Try
            departure_datetime.Text = Util.parseDatepicker(Request.Form("departure_datetime"))
        Catch ex As Exception
            departure_datetime.Text = Request.Form("departure_datetime")
        End Try

        Try
            arrival_datetime.Text = Util.parseDatepicker(Request.Form("arrival_datetime"))
        Catch ex As Exception
            arrival_datetime.Text = Request.Form("arrival_datetime")
        End Try
        passengers.Text = Convert.ToInt32(Request.Form("adult")) + Convert.ToInt32(Request.Form("children")) + Convert.ToInt32(Request.Form("enfant"))
        search = New Search(connection)
        search.setData("departure_location_info", Request.Form("departure_location_info"))
        search.setData("arrival_location_info", Request.Form("arrival_location_info"))

        Dim departure_datetime_string As String
        Try
            departure_datetime_string = Util.parseDatepicker(Request.Form("departure_datetime"))
        Catch ex As Exception
            departure_datetime_string = "NULL"
        End Try
        search.setData("departure_datetime", departure_datetime_string)

        Dim arrival_datetime_string As String
        Try
            arrival_datetime_string = Util.parseDatepicker(Request.Form("arrival_datetime"))
        Catch ex As Exception
            arrival_datetime_string = "NULL"
        End Try

        search.setData("arrival_datetime", arrival_datetime_string)
        search.setData("adult", CInt(Request.Form("adult")))
        search.setData("children", CInt(Request.Form("children")))
        search.setData("enfant", CInt(Request.Form("enfant")))
        search.setData("request_datetime", "NOW()")
        Try
            search.insert()
        Catch ex As MySqlException

        End Try

        If (search.getData("id") <> "") Then
            Dim process As System.Diagnostics.Process = New System.Diagnostics.Process()
            process.StartInfo.FileName = System.Web.HttpContext.Current.Server.MapPath("..\Bin\FlyBackGroundSearch.exe")
            process.StartInfo.Arguments = search.getData("id")
            process.StartInfo.WorkingDirectory = System.Web.HttpContext.Current.Server.MapPath("..\Bin")
            process.StartInfo.RedirectStandardOutput = True
            process.StartInfo.RedirectStandardError = True
            process.StartInfo.CreateNoWindow = True
            process.StartInfo.UseShellExecute = False
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden
            process.Start()
        End If

    End Sub
    Public Function getReqestId() As String
        Return search.getData("id")
    End Function
End Class
