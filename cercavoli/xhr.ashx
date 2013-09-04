<%@ WebHandler Language="VB" Class="CercaVoliXhr" %>
Imports System
Imports System.Web
Imports System.Data
Imports MySql.Data.MySqlClient
Imports Libray.Fly

Public Class CercaVoliXhr : Implements IHttpHandler
    Dim connectionString As String = ConfigurationSettings.AppSettings("connvoli")
    Dim connection As New MySqlConnection(connectionString)
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim task As String = context.Request.QueryString("task")
        Select Case task
            Case "fly_from"
                context.Response.ContentType = "application/json"
                Dim json As String = "[ "
                Dim airportColl As New Libray.Fly.AirportCollection(connection)
                Dim filter As NameValueCollection = New NameValueCollection(context.Request.QueryString)
                airportColl.LoadAll(filter)
                Dim airportItems As Queue
                airportItems = airportColl.getItems
                Dim airport As Airport
                Dim airportCount As Integer = 0
                For Each airport In airportItems
                    If airportCount > 0 Then
                        json = json + ","
                    End If
                    json = json + " { ""label"": "
                    json = json + """" + airport.getData("name") + " "
                    If (airport.getData("iata") <> "") Then
                        json = json + "(" & airport.getData("iata") + ") "
                    End If
                    
                    json = json + airport.getData("province") + " - " + airport.getData("country") + """"
                    json = json + " , ""value"": "
                    json = json + """" & airport.getData("iata") & """"
                    json = json + " } " & vbCrLf
                    airportCount = airportCount + 1
                Next airport
                
                json = json & " ]"
                context.Response.Write(json)
            Case "fly_to"
                context.Response.ContentType = "application/json"
                Dim json As String = "[ "
                Dim airportColl As New Libray.Fly.AirportCollection(connection)
                Dim filter As NameValueCollection = New NameValueCollection(context.Request.QueryString)
                airportColl.LoadAll(filter)
                Dim airportItems As Queue
                airportItems = airportColl.getItems
                Dim airport As Airport
                Dim airportCount As Integer = 0
                For Each airport In airportItems
                    If airportCount > 0 Then
                        json = json + ","
                    End If
                    json = json + " { ""label"": "
                    json = json + """" + airport.getData("name") + " "
                    If (airport.getData("iata") <> "") Then
                        json = json + "(" & airport.getData("iata") + ") "
                    End If
                    
                    json = json + airport.getData("province") + " - " + airport.getData("country") + """"
                    json = json + " , ""value"": "
                    json = json + """" & airport.getData("iata") & """"
                    json = json + " } " & vbCrLf
                    airportCount = airportCount + 1
                Next airport
                
                json = json & " ]"
                context.Response.Write(json)
        End Select
        
        
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class