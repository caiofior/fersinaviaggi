Imports System.Data
Imports MySql.Data.MySqlClient
Partial Class FlySearch
    Inherits System.Web.UI.Page
    Dim cnstring As String = ConfigurationSettings.AppSettings("connvoli")
    Dim cn As New MySqlConnection(cnstring)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub



End Class
