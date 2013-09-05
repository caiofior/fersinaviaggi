Imports Libray.Fly
Imports MySql.Data.MySqlClient
Imports Libray.Core

Partial Class Results
    Inherits System.Web.UI.Page
    Dim connectionString As String = ConfigurationSettings.AppSettings("connvoli")
    Dim connection As New MySqlConnection(connectionString)
    Public departure As New Airport(connection)
    Public arrival As New Airport(connection)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filter As New NameValueCollection(Context.Request.QueryString)
        departure.loadFromIata(Request.Form("departure_location_info"))
        arrival.loadFromIata(Request.Form("arrival_location_info"))
        departure_location_name.Text = departure.getData("name") + " (" + departure.getData("country") + ")"
        arrival_location_name.Text = arrival.getData("name") + " (" + arrival.getData("country") + ")"
        ''System.Web.HttpContext.Current.Response.Write(Request.Form("departure_datetime"))
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

        ''System.Web.HttpContext.Current.Response.Write(filter.Get("adult") + filter.Get("children") + filter.Get("enfant"))
    End Sub
End Class
