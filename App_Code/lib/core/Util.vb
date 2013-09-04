Imports Microsoft.VisualBasic
Namespace Libray.Core
    Public Class Util
        Shared Function sqlSanitize(ByVal text As String) As String
            Replace(text, "'", "''")
            Return text
        End Function


    End Class
End Namespace

