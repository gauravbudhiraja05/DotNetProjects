Imports Microsoft.VisualBasic
Imports System.Collections.ArrayList
Public Class queryBuilderAjax
    Dim arrBlank2 As ArrayList
    Dim arrBlank3 As ArrayList

    <Ajax.AjaxMethod()> _
    Public Function findctrldiv(ByVal divstr As String, ByVal divvalue As String)
        Dim econtains As Boolean
        econtains = InStr(divstr, divvalue, CompareMethod.Text)
        Return econtains
    End Function
    '<Ajax.AjaxMethod()> _
    'Public Function addtodivArrays(ByVal divid As String, ByVal divele As String) As ArrayList
    '    If divid = "blank2" Then
    '        arrBlank2.Add(divele)
    '        Return arrBlank2
    '    ElseIf divid = "blank3" Then
    '        arrBlank3.Add(divele)
    '        Return arrBlank3
    '    End If
    'End Function
    <Ajax.AjaxMethod()> _
    Public Function checkDuplicate(ByVal showdata As String, ByVal newstring As String, ByVal toReplace As String) As String
        Dim arryshowdata = showdata.Split(",")
        Dim i, j As Integer
        Dim b As Boolean = False
        i = UBound(arryshowdata)
        For j = 0 To i
            Dim str As String = arryshowdata(j)
            If str.Contains(toReplace) Then
                b = True
                showdata = showdata.Replace(arryshowdata(j), newstring)
            End If

        Next
        If b = False Then
            showdata = showdata + "," + newstring
        End If
        Return showdata
    End Function
End Class
