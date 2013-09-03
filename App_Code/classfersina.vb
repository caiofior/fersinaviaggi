Imports Microsoft.VisualBasic

Public Module classfersina
    Function converti(ByVal x As Integer) As String
        Dim stringa, app As String
        Dim le1, le2, le3, le4, le5, le6 As Char
        Dim numero As Integer
        stringa = CStr(x)
        Select Case stringa.Length
            Case 1
                stringa = "0000" & stringa
            Case 2
                stringa = "000" & stringa
            Case 3
                stringa = "00" & stringa
            Case 4
                stringa = "0" & stringa
        End Select
        app = Mid(stringa, 5, 1)
        numero = CInt(app) + 97 + CInt(Mid(stringa, 5, 1))
        le1 = Chr(numero)
        app = Mid(stringa, 4, 1)
        numero = CInt(app) + 100 + CInt(Mid(stringa, 5, 1))
        le2 = Chr(numero)
        app = Mid(stringa, 3, 1)
        numero = CInt(app) + 112 - CInt(Mid(stringa, 5, 1))
        le3 = Chr(numero)
        le4 = Mid(stringa, 5, 1)
        app = Mid(stringa, 2, 1)
        numero = CInt(app) + 109 - CInt(Mid(stringa, 5, 1))
        le5 = Chr(numero)
        app = Mid(stringa, 1, 1)
        numero = CInt(app) + 99
        le6 = Chr(numero)
        converti = le1 & le2 & le3 & le4 & le5 & le6
    End Function
    Function sconverti(ByVal x As String) As Integer
        Dim app, app2, stringa As String
        Dim st1, st2, st3, st4, st5 As Char
        Dim num1, num2, num3, num4, num5 As Integer
        stringa = x
        If stringa.Length = 6 Then
            app = Mid(stringa, 1, 1)
            num5 = (Asc(app) - 97) / 2
            app = Mid(stringa, 2, 1)
            num4 = (Asc(app) - 100) - num5
            app = Mid(stringa, 3, 1)
            num3 = (Asc(app) - 112) + num5
            app = Mid(stringa, 5, 1)
            num2 = (Asc(app) - 109) + num5
            app = Mid(stringa, 6, 1)
            num1 = (Asc(app) - 99)
            st1 = num1.ToString
            st2 = num2.ToString
            st3 = num3.ToString
            st4 = num4.ToString
            st5 = num5.ToString
            app2 = st1 & st2 & st3 & st4 & st5
            sconverti = CInt(app2)
        Else
            sconverti = 0
        End If

    End Function
End Module



