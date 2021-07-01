Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_ViewFormula
    Inherits System.Web.UI.Page
    Public datatablestring
    Public datafieldstring
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim j As Integer = 0
        Dim strmain As String
        Dim strTable As String
        strTable = Trim(Request("tabname"))
        'txttype.Value = Trim(Request("formula"))
        Dim viewname As String = Request("vname")
        Dim typeofpage As String = Request("type")
        Dim data As String = Request("data")
        Dim datatext As String = Request("datatext")
        duplicatename.Value = LCase(datatext)
        data = Replace(data, "|~|", "+")
        Dim columnnamearr = Split(Trim(data), ",")
        Dim columnnamearr1 = Split(Trim(datatext), ",")
        Dim tabname
        tabname = strTable.Split(",")
        Dim i As Integer = 0
        If typeofpage = "Create" Or typeofpage = "Edit" Then
            For i = 0 To tabname.Length - 1
                Dim tablename As String
                Dim arr As Array
                Dim rdrModules As SqlDataReader
                Dim strQryMod As String = "select Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where TableName='" & tabname(i) & "' union all select null as Visiblecolumn,Headings as Visiblecolumn from idmsviewmaster where viewname='" & tabname(i) & "'"
                Dim cmdMod As New SqlCommand(strQryMod, connection)
                connection.Open()
                rdrModules = cmdMod.ExecuteReader
                'below code  altered by atul to create view from view
                Dim arrlength As Int16
                If rdrModules.Read Then
                    If (rdrModules("Headings").ToString <> "#@DEWQA45tec") Then
                        tablename = rdrModules("Headings")
                        arr = tablename.Split(",")
                        arrlength = arr.Length - 2
                    Else
                        tablename = rdrModules("visiblecolumn")
                        arr = tablename.Split(",")
                        arrlength = arr.Length - 1
                    End If
                    'above code  altered by atul to create view from view
                    For j = 0 To arrlength
                        Dim tabcol As String = tabname(i) & "." & arr(j)
                        cbofdata.Items.Add(tabcol)
                        If duplicatename.Value = "" Then
                            duplicatename.Value = tabcol
                        Else
                            duplicatename.Value = duplicatename.Value + "," + tabcol
                        End If
                        ' datatablestring = datatablestring & "<option value='" & tabcol & "'> " & tabcol & " </option>"
                    Next
                End If
                rdrModules.Close()
                connection.Close()
                cmdMod.Dispose()
            Next
            'End If
            Dim k As Integer = 0
            Dim bool As Boolean
            If Request("data") <> "" Then
                For i = 0 To columnnamearr.length - 1
                    For k = 0 To cbofdata.Items.Count - 1
                        If cbofdata.Items(k).Value = columnnamearr(i) Then
                            bool = True
                            Exit For
                        End If
                    Next
                    If bool = False Then
                        'Dim Len = Split(columnnamearr(i), "'")
                        columnnamearr(i) = Replace(columnnamearr(i), "~~", ",")
                        strmain = columnnamearr(i)
                        'columnnamearr(i)
                        'document.Form1.txtFormula.value = strformula.replace("'", " + String.fromCharCode(34) + ")
                        'strformula = document.Form1.txtFormula.value
                        'For j = 0 To Len.length - 1
                        '    columnnamearr(i) = strmain.Replace("'", " + String.fromCharCode(34) + ")
                        '    strmain = columnnamearr(i)
                        'Next
                        Dim cnt As Integer = cbofdata.Items.Count
                        If columnnamearr(i) <> "" Then
                            'cbofdata.Items.Add(columnnamearr(i))
                            cbofdata.Items.Insert(cnt, "")
                            cbofdata.Items(cnt).Value = columnnamearr(i)
                            cbofdata.Items(cnt).Text = columnnamearr1(i)

                        End If
                        'datatablestring = datatablestring & "<option value='" & columnnamearr(i) & "'> " & columnnamearr(i) & " </option>"
                    End If
                    bool = False
                Next
            End If
        End If
        'If data <> "" And typeofpage = "Edit" Then
        '    'Dim viewname As String = Request("vname")
        '    Dim k As Integer
        '    Dim colname As String
        '    Dim formulaval As String
        '    Dim Headings As String
        '    Dim arr1 As Array
        '    Dim arr2 As Array
        '    Dim str As String
        '    Dim rdrModules1 As SqlDataReader
        '    Dim strQryMod1 As String = "select colname,formula,headings from idmsviewmaster where viewname='" & viewname & "'"
        '    Dim cmdMod1 As New SqlCommand(strQryMod1, connection)
        '    connection.Open()
        '    rdrModules1 = cmdMod1.ExecuteReader
        '    If rdrModules1.Read Then
        '        colname = rdrModules1("colname")
        '        formulaval = rdrModules1("formula")
        '        str = colname + formulaval
        '        arr1 = str.Split(",")
        '        Headings = rdrModules1("headings")
        '        arr2 = Headings.Split(",")
        '        k = 0
        '        For k = 0 To arr1.Length - 1
        '            cbofdata.Items.Insert(k, "")

        '            cbofdata.Items(k).Value = arr1(k)

        '            cbofdata.Items(k).Text = arr2(k)

        '        Next
        '    End If

        'End If


        duplicatename.Value = LCase(duplicatename.Value)
    End Sub
End Class
