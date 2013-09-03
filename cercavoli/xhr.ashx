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
                Dim flyColl As New Libray.Fly.FlyCollection(connection)
                Dim filter As NameValueCollection = New NameValueCollection(context.Request.QueryString)
                flyColl.LoadAll(filter)
                Dim flyItems As Queue
                flyItems = flyColl.getItems
                Dim fly As Fly
                Dim flyCount As Integer = 0
                For Each fly In flyItems
                    If flyCount > 0 Then
                        json = json + ","
                    End If
                    json = json + " { ""label"": "
                    json = json + """" + fly.getData("name") + " "
                    If (fly.getData("iata") <> "") Then
                        json = json + "(" & fly.getData("iata") + ") "
                    End If
                    
                    json = json + fly.getData("province") + " - " + fly.getData("country") + """"
                    json = json + " , ""value"": "
                    json = json + """" & fly.getData("iata") & """"
                    json = json + " } " & vbCrLf
                    flyCount = flyCount + 1
                Next fly
                
                json = json & " ]"
                context.Response.Write(json)
            Case "fly_to"
                context.Response.ContentType = "application/json"
                Dim json As String = "[ "
                Dim flyColl As New Libray.Fly.FlyCollection(connection)
                Dim filter As NameValueCollection = New NameValueCollection(context.Request.QueryString)
                flyColl.LoadAll(filter)
                Dim flyItems As Queue
                flyItems = flyColl.getItems
                Dim fly As Fly
                Dim flyCount As Integer = 0
                For Each fly In flyItems
                    If flyCount > 0 Then
                        json = json + ","
                    End If
                    json = json + " { ""label"": "
                    json = json + """" + fly.getData("name") + " "
                    If (fly.getData("iata") <> "") Then
                        json = json + "(" & fly.getData("iata") + ") "
                    End If
                    
                    json = json + fly.getData("province") + " - " + fly.getData("country") + """"
                    json = json + " , ""value"": "
                    json = json + """" & fly.getData("iata") & """"
                    json = json + " } " & vbCrLf
                    flyCount = flyCount + 1
                Next fly
                
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