Imports Microsoft.VisualBasic
Namespace Libray.Core
    Public Class Util
        Shared Function sqlSanitize(ByVal text As String) As String
            text = Replace(text, "'", "''")
            Return text
        End Function
        Shared Function parseDatepicker(ByVal text As String) As String
            Dim dataParts() As String = Split(text)
            Dim months As New NameValueCollection()
            months.Add("Gennaio", "01")
            months.Add("Febbraio", "02")
            months.Add("Marzo", "03")
            months.Add("Aprile", "04")
            months.Add("Maggio", "05")
            months.Add("Giugno", "06")
            months.Add("Luglio", "07")
            months.Add("Agosto", "08")
            months.Add("Settembre", "09")
            months.Add("Ottobre", "10")
            months.Add("Novembre", "11")
            months.Add("Dicembre", "12")
            If dataParts.Length <> 3 Then
                Throw New System.Exception("Wrong data format")
            End If
            dataParts("1") = months.Get(dataParts("1"))
            text = dataParts("2") + "-" + dataParts("1") + "-" + dataParts("0")

            'System.Web.HttpContext.Current.Response.Write(text)

            Return text
        End Function

    End Class
End Namespace

