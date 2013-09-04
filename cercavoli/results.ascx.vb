Imports System.Data
Imports MySql.Data.MySqlClient
Imports Libray.Fly
Partial Class FlySearch_Results
    Inherits System.Web.UI.UserControl
    Dim connectionString As String = ConfigurationSettings.AppSettings("connvoli")
    Dim connection As New MySqlConnection(connectionString)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim flyCollection As New FlyCollection(connection)
        Dim filter As New NameValueCollection(Context.Request.QueryString)
        System.Web.HttpContext.Current.Response.Write(filter.Get("departure_location_info"))
        ResultsData.DataSource = flyCollection.getDataset(filter)
        ResultsData.DataBind()
    End Sub

End Class
