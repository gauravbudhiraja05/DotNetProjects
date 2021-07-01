Imports Microsoft.VisualBasic

Public Class Checks
    Public Function checkAlphabetic(ByVal input As String)
        Dim count As Integer
        For count = 1 To input.Length
            If Not (Asc(Mid(input, count, 1)) >= 65 And Asc(Mid(input, count, 1)) <= 90 Or Asc(Mid(input, count, 1)) >= 97 And Asc(Mid(input, count, 1)) <= 122) Or Asc(Mid(input, count, 1)) = 32 Then
                Return False
            End If
        Next
        Return True
    End Function
    Public Function checkAlphalob(ByVal input As String)
        Dim count As Integer
        For count = 1 To input.Length
            If Not ((Asc(Mid(input, count, 1)) >= 65 And Asc(Mid(input, count, 1)) <= 90) Or (Asc(Mid(input, count, 1)) >= 97 And Asc(Mid(input, count, 1)) <= 122) Or Asc(Mid(input, count, 1)) = 45 Or Asc(Mid(input, count, 1)) = 95) Or Asc(Mid(input, count, 1)) = 32 Then
                Return False
            End If
        Next
        Return True
    End Function

    Public Function checkPassSymbols(ByVal input As String)
        'Dim count As Integer

        If AlphTrue(input) = True And (NumericTrue(input) = True Or PuncTrue(input) = True Or OtherTrue(input) = True) Then
            Return True
        Else
            If (NumericTrue(input) = True And PuncTrue(input) = True) Or (NumericTrue(input) = True And OtherTrue(input) = True) Or (PuncTrue(input) = True And OtherTrue(input) = True) Then
                Return True
            End If
        End If

        Return False
    End Function

    Private Function AlphTrue(ByVal alphinput As String)
        Dim count As Integer
        Dim stat As Boolean = False

        For count = 1 To alphinput.Length
            If (Asc(Mid(alphinput, count, 1)) >= 65 And Asc(Mid(alphinput, count, 1)) <= 90) Or (Asc(Mid(alphinput, count, 1)) >= 97 And Asc(Mid(alphinput, count, 1)) <= 122) Then
                stat = True
            End If
        Next

        Return stat
    End Function

    Public Function AlphTrue1(ByVal alphinput As String)
        Dim count As Integer
        Dim stat As Boolean = False

        For count = 1 To alphinput.Length
            If (Asc(Mid(alphinput, count, 1)) >= 65 And Asc(Mid(alphinput, count, 1)) <= 90) Or (Asc(Mid(alphinput, count, 1)) >= 97 And Asc(Mid(alphinput, count, 1)) <= 122) Then
                stat = True

            End If
        Next

        Return stat
    End Function
    Public Function AlphTrue2(ByVal alphinput As String)
        Dim count As Integer
        Dim stat As Boolean = False

        For count = 1 To alphinput.Length
            If (Asc(Mid(alphinput, count, 1)) >= 65 And Asc(Mid(alphinput, count, 1)) <= 90) Or (Asc(Mid(alphinput, count, 1)) >= 97 And Asc(Mid(alphinput, count, 1)) <= 122) Then
                stat = True
                Exit For
            End If
        Next

        Return stat
    End Function


    ''''Public Function AlphNumTrue(ByVal alphinput As String)
    ''''    Dim count As Integer
    ''''    Dim stat As Boolean = False

    ''''    For count = 1 To alphinput.Length
    ''''        If ((Asc(Mid(alphinput, count, 1)) >= 65 And Asc(Mid(alphinput, count, 1)) <= 90) Or (Asc(Mid(alphinput, count, 1)) >= 97 And Asc(Mid(alphinput, count, 1)) <= 122)) And (Asc(Mid(alphinput, count, 1)) >= 48 And Asc(Mid(alphinput, count, 1)) <= 57) Then
    ''''            stat = True
    ''''        End If
    ''''    Next
    ''''    Return stat
    ''''End Function

    Public Function NumericTrue(ByVal str As String)
        Dim count As Integer
        Dim stat As Boolean = False

        For count = 1 To str.Length
            If (Asc(Mid(str, count, 1)) >= 48 And Asc(Mid(str, count, 1)) <= 57) Then
                stat = True

            End If
        Next
        Return stat
    End Function
    Public Function NumericTrue1(ByVal str As String)
        Dim count As Integer
        Dim stat As Boolean = False

        For count = 1 To str.Length
            If (Asc(Mid(str, count, 1)) >= 48 And Asc(Mid(str, count, 1)) <= 57) Then
                stat = True
                Exit For
            End If
        Next
        Return stat
    End Function

    Private Function PuncTrue(ByVal str As String)

        Dim punc(11) As Integer
        punc(0) = 33
        punc(1) = 34
        punc(2) = 39
        punc(3) = 40
        punc(4) = 41
        punc(5) = 44
        punc(6) = 45
        punc(7) = 46
        punc(8) = 58
        punc(9) = 59
        punc(10) = 63

        Dim count As Integer
        Dim pnc As Integer
        Dim stat As Boolean = False

        For count = 1 To str.Length
            Dim val = Asc(Mid(str, count, 1))
            For pnc = 0 To punc.Length - 1
                If val = punc(pnc) Then
                    stat = True
                End If
            Next
        Next


        Return stat
    End Function

    Private Function OtherTrue(ByVal str As String)

        Dim count As Integer

        Dim stat As Boolean = False

        For count = 1 To str.Length
            Dim val = Asc(Mid(str, count, 1))
            If AlphTrue(val) = False And NumericTrue(val) = False And PuncTrue(val) = False Then
                stat = True
            End If
        Next
        Return stat
    End Function

    Public Function chkvalidtxt(ByVal strchk As String)
        Dim i
        Dim booladd As Boolean
        For i = 0 To strchk.Length - 1
            ' If strchk.Chars(i) = "'" Or strchk.Chars(i) = "%" Or strchk.Chars(i) = "<" Or strchk.Chars(i) = ">" Or strchk.Chars(i) = "~" Then
            If strchk.Chars(i) = "'" Then
                booladd = True
                Exit For
            End If
        Next

        If booladd = True Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function chkvalidtxt1(ByVal strchk As String)
        Dim i
        Dim booladd As Boolean
        For i = 0 To strchk.Length - 1
            If strchk.Chars(i) = "'" Or strchk.Chars(i) = "%" Or strchk.Chars(i) = "<" Or strchk.Chars(i) = ">" Or strchk.Chars(i) = "~" Or strchk.Chars(i) = "*" Or strchk.Chars(i) = "@" Or strchk.Chars(i) = "^" Or strchk.Chars(i) = "&" Or strchk.Chars(i) = "?" Or strchk.Chars(i) = "(" Or strchk.Chars(i) = "#" Or strchk.Chars(i) = ")" Or strchk.Chars(i) = ":" Or strchk.Chars(i) = ";" Or strchk.Chars(i) = "/" Or strchk.Chars(i) = "[" Or strchk.Chars(i) = "]" Or strchk.Chars(i) = "{" Or strchk.Chars(i) = "}" Or strchk.Chars(i) = "+" Or strchk.Chars(i) = "-" Or strchk.Chars(i) = "!" Or strchk.Chars(i) = "$" Or strchk.Chars(i) = "|" Or strchk.Chars(i) = "," Or strchk.Chars(i) = "\" Then
                'If strchk.Chars(i) = "'" Then
                booladd = True
                Exit For
            End If
        Next

        If booladd = True Or NumericTrue1(strchk) = True Then
            Return False
        Else
            Return True

        End If
    End Function
    Public Function chkpwd(ByVal pwdchk As String)
        Dim i
        Dim boolpwd As Boolean = False
        i = pwdchk.Length
        If pwdchk.Length >= 8 And pwdchk.Length <= 15 Then
            boolpwd = True
        End If

        If boolpwd = True And AlphTrue2(pwdchk) = True And NumericTrue1(pwdchk) = True Then
            Return False
        Else
            Return True

        End If
    End Function
#Region "added on 12-08"
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
#End Region
End Class
