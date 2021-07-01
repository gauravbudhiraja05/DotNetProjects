Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports System.data
Imports Microsoft.VisualBasic

Public Class ReportDesignerAjax
    Dim con As String = AppSettings("connectionString")
    Dim objsqlcon As New SqlConnection(con)
    Dim connection As New SqlConnection(con)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    <Ajax.AjaxMethod()> _
         Public Function GetTable(ByVal tab As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        Dim strArray As String() = tab.Split("$")
        For i = 0 To strArray.Length - 1
            If str = "" Then
                str = strArray(i)
            Else
                str = str + "||" + strArray(i)
            End If
        Next
        Return str
    End Function

    <Ajax.AjaxMethod()> _
    Public Function GetTableFields(ByVal tab As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        Dim qStr As String = "Select * from " + tab + " where 1<>1"
        Dim objcmd As New SqlCommand
        Try


            'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)

            objcmd = New SqlCommand(qStr, connection)
            objcmd.CommandTimeout = 10
            connection.Open()
            Dim thisReader As SqlDataReader = objcmd.ExecuteReader()
            Dim j As Integer = 0
            For j = 0 To thisReader.FieldCount - 1
                If str = "" Then
                    str = tab + "$" + thisReader.GetName(j)
                Else
                    str = str + "~" + tab + "$" + thisReader.GetName(j)
                End If
            Next
            thisReader.Close()
        Catch ex As Exception
            If str = "" Then
                str = "Null"
            End If
        End Try
        objcmd.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function GetColumnValue(ByVal col As String) As DataSet
        ds.Clear()
        Dim i As Integer = 0
        Dim str As String = ""
        Dim tbl As String()
        Dim ch As String = ""
        If (col.Contains(".")) Then
            ch = "."
        Else
            ch = "$"
        End If
        tbl = Split(col, ch)
        Dim qStr As String = "Select distinct(" + tbl(1) + ") as ColumnName from " + tbl(0) + ""
        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        Dim objcmd As New SqlCommand
        objcmd = New SqlCommand(qStr, connection)
        connection.Open()
        da.SelectCommand = objcmd
        da.Fill(ds)
        da.Dispose()
        connection.Close()
        Return ds
    End Function
    <Ajax.AjaxMethod()> _
   Public Function updateGroupby(ByVal Elem As String, ByVal gr As String)
        Dim grpBy = ""
        Dim hj As String() = Split(Elem, "~")
        Dim k = 0
        Dim str = ""


        Dim grp As String() = Split(gr, ",")
        Dim gb = 0
        For k = 0 To hj.Length - 1
            Dim b = 0
            'If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
            Dim vbn = LCase(hj(k))
            If vbn.Contains("sum(") Or vbn.Contains("max(") Or vbn.Contains("min(") Or vbn.Contains("avg(") Or vbn.Contains("count(") Then
                Dim nm = 1
                For gb = 0 To grp.Length - 1
                    Dim hjk = Split(hj(k), " AS ")
                    If hjk(0) = grp(gb) Then
                        nm = 0
                    End If
                Next
                If nm = 1 Then
                    b = 1
                End If
            End If
            If b = 0 Then
                If str = "" Then
                    Dim hjk = Split(hj(k), " AS ")
                    str = hjk(0)

                Else
                    Dim hjk = Split(hj(k), " AS ")
                    str = str + "," + hjk(0)
                End If
            End If
        Next
        ''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        ' ''For k = 0 To hj.Length - 1
        ' ''    Dim b = 0
        ' ''    '            If hj(k).Contains("(") Or hj(k).Contains(")") Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
        ' ''    If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
        ' ''        b = 1
        ' ''    End If
        ' ''    If b = 0 Then
        ' ''        Dim hj1 = Split(hj(k), " AS ")
        ' ''        Dim tmp = hj(k)
        ' ''        If hj1.length > 1 Then
        ' ''            tmp = hj1(0)
        ' ''        End If
        ' ''        Dim ag = 0

        ' ''        If str = "" Then
        ' ''            str = tmp
        ' ''        Else
        ' ''            Dim sp = Split(str, ",")
        ' ''            Dim bn = 0
        ' ''            For ag = 0 To sp.length - 1
        ' ''                If Trim(sp(ag)) = Trim(tmp) Then
        ' ''                    bn = 1
        ' ''                    Exit For

        ' ''                End If
        ' ''            Next
        ' ''            If bn = 0 Then
        ' ''                str = str + "," + tmp
        ' ''            End If

        ' ''        End If
        ' ''    End If
        ' ''Next
        str = Replace(str, "$", ".")
        grpBy = str
        Return grpBy
    End Function
    <Ajax.AjaxMethod()> _
   Public Function GetformulaValue(ByVal col As String) As DataSet
        ds.Clear()
        Dim i As Integer = 0
        Dim str As String = ""
        Dim tbl As String()
        Dim ch As String = ""
        If (col.Contains(".")) Then
            ch = "."
        Else
            ch = "$"
        End If
        tbl = Split(col, ch)
        Dim qStr As String = "Select distinct(" + tbl(1) + ") as ColumnName from " + tbl(0) + ""
        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        Dim objcmd As New SqlCommand
        objcmd = New SqlCommand(qStr, connection)
        connection.Open()
        da.SelectCommand = objcmd
        da.Fill(ds)
        da.Dispose()
        connection.Close()
        Return ds
    End Function

    ''execute function value
    <Ajax.AjaxMethod()> _
    Public Function GetFormula(ByVal query As String) As String
        Dim i As Integer = 0
        Dim val As String = ""
        Dim str As String = ""
        Dim ch As String = ""
        Dim query1 = Replace(query, "$", ",")
        Try

        
            'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
            Dim objcmd As New SqlCommand
            Dim sqlCmd As SqlCommand = New SqlCommand(query1, connection)
            connection.Open()
            val = sqlCmd.ExecuteScalar()
            sqlCmd.Dispose()
            connection.Close()
        Catch ex As Exception
            val = "0"
        End Try
        Return val
    End Function
    ''execute function value
    <Ajax.AjaxMethod()> _
    Public Function GetFormula1(ByVal query As String) As String
        Dim i As Integer = 0
        Dim val As String = ""
        Dim str As String = ""
        Dim ch As String = ""
        Dim query1 = Replace(query, "$", ",")
        Try

        
            'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
            Dim objcmd As New SqlCommand
            Dim sqlCmd As SqlCommand = New SqlCommand(query1, connection)
            connection.Open()
            val = sqlCmd.ExecuteScalar()
            sqlCmd.Dispose()
            connection.Close()
        Catch ex As Exception
            val = "Error"
        End Try
        Return val
    End Function
    ''Replace formula name wid formula
    <Ajax.AjaxMethod()> _
    Public Function swapFormula(ByVal formula As String, ByVal tomatch As String)
        If (tomatch = "") Then
            GoTo ret
        Else
            Dim val As String() = tomatch.Split("~")
            Dim k = 0
            Dim l = 0
            For k = 0 To val.Length - 1
                Dim valN As String() = Split(val(k), " AS ")

                formula = formula.Replace(valN(1), valN(0))

            Next
        End If
ret:
        Return formula
    End Function
    ''Delete formula name from list
    <Ajax.AjaxMethod()> _
    Public Function deleteFormula(ByVal nullObj As String, ByVal formula As String, ByVal formulas As String)
        Dim List = ""

        Dim k1 = Split(formulas, "~")
        Dim l1 = 0
        For l1 = 0 To k1.length - 1
            Dim hj = Split(k1(l1), " AS ")
            If (hj.length > 1) Then


                If List = "" Then
                    List = hj(1)
                Else
                    List = List + "~" + hj(1)
                End If
            End If
        Next
        Dim formulaName = ""
        nullObj = Replace(nullObj, "aa", "")
        formula = Replace(formula, nullObj, "")
       
        formulaName = Trim(Replace(formula, nullObj, ""))
        If (List = "") Then
            GoTo ret
        Else
            '' Delete formula name from the list
            If (List.Contains("~" + formulaName)) Then
                List = List.Replace("~" + formulaName, "")
            ElseIf (List.Contains(formulaName + "~")) Then
                List = List.Replace(formulaName + "~", "")
            ElseIf (List.Contains(formulaName)) Then
                List = List.Replace(formulaName, "")
            End If
            '' Delete whole formula from the list
            Dim k = 0
            Dim l = ""
            Dim fo = Split(formulas, "~")
            For k = 0 To fo.length - 1
                Dim fo1 = Split(fo(k), " AS ")
                If (fo1(1) = formulaName) Then
                    l = fo(k)
                    Exit For
                End If
            Next
            If l <> "" Then


                If (formulas.Contains("~" + l)) Then
                    formulas = formulas.Replace("~" + l, "")
                ElseIf (formulas.Contains(l + "~")) Then
                    formulas = formulas.Replace(l + "~", "")
                ElseIf (formulas.Contains(l)) Then
                    formulas = formulas.Replace(l, "")
                End If
            End If
        End If

        List = List + "@#@" + formulas  '' new formulanames @#@ new formula List

ret:
        Return List
    End Function
    ''update formula
    <Ajax.AjaxMethod()> _
    Public Function updateFormula(ByVal nullObj As String, ByVal formulaName As String, ByVal formula As String, ByVal formulas As String)
        nullObj = Replace(nullObj, "aa", "")
        formulaName = Trim(Replace(formulaName, nullObj, ""))
        formula = Trim(Replace(formula, nullObj, ""))
        Dim k = 0
        Dim l = ""
        Dim fo = Split(formulas, "~")
        For k = 0 To fo.length - 1
            Dim fo1 = Split(fo(k), " AS ")
            If (Trim(fo1(1)) = Trim(formulaName)) Then
                l = fo(k)
                Exit For
            End If
        Next
        formulas = Replace(formulas, l, formula)

        Return formulas
    End Function
    ''update formula using objects
    <Ajax.AjaxMethod()> _
   Public Function updateFormulaobjects(ByVal nullObj As String, ByVal nfor As String, ByVal exfor As String, ByVal formulas As String)
        nullObj = Replace(nullObj, "aa", "")
        exfor = Replace(exfor, nullObj, "")
        Dim val As String()
        Dim fg = Split(nfor, "@#@")
        Dim ob = fg(0)
        Dim fname = Split(fg(1), " AS ") ' fname(1) contains the formula name
        '' if no formula exists?
        If Trim(formulas) = "" Then
            formulas = fg(1) + ":new" ' hidFormula=formula:new
            GoTo endCon
        End If

        '' check if same object being updated?
        val = Split(formulas, "~")
        Dim k = 0
        For k = 0 To val.Length - 1
            If Trim(val(k)) = Trim(exfor) Then
                formulas = Replace(formulas, val(k), fg(1))
                formulas = formulas + ":updated"
                GoTo endCon
            End If
        Next
        '' if formula name already exists
        Dim b As Boolean = False
        For k = 0 To val.Length - 1
            Dim tm1 = Split(Trim(val(k)), " AS ")
            Dim tm2 = Split(Trim(nfor), " AS ")
            If tm1(1) = tm2(1) Then
                b = True
                formulas = formulas + ":already"
                GoTo endCon
            End If
        Next
        If (b = False) Then
            formulas = formulas + "~" + fg(1) + ":new"
            GoTo endCon
        End If

endCon:
        Return formulas
    End Function
    ''update/delete formula of header/footer
    <Ajax.AjaxMethod()> _
   Public Function updateDeleteFormulas(ByVal obj As String, ByVal formu As String, ByVal formulas As String)
        Dim form = ""
        '' delete the existing formula
        If formulas <> "" Then
            Dim k = 0
            Dim sp = Split(formulas, "~")
            For k = 0 To sp.length - 1
                Dim sp1 = Split(sp(k), "@#@")
                If sp1(0) <> obj Then
                    If form = "" Then
                        form = sp(k)
                    Else
                        form = form + "~" + sp(k)
                    End If
                End If
            Next
        End If
        '' now if new formula is present, add it
        Dim syn = ""
        Dim fname = ""
        If formu <> "" Then
            ''check if the formula is aggregate and contains alias
            If UCase(formu).Contains(" AS ") = False Then

                syn = "No Alias"
                GoTo home
            Else
                Dim mysp = Split(UCase(formu), " FROM ")
                Dim jkl1 = Replace(mysp(0), "Select ", "")
                Dim jkl = Split(jkl1, " AS ")
                Dim jkl2 = Split(jkl(1), ",")
                fname = Trim(jkl2(0))
            End If
            '''''''''''''''''
            '' check the syntax of the formula\
            'Dim synCmd = New SqlCommand(formu, connection)
            'connection.Close()
            'connection.Open()

            'Try
            '    syn = synCmd.executescalar()
            '    syn = ""
            '    connection.Close()
            'Catch ex As Exception
            '    syn = "Syntax Error"
            'End Try
            ''''''''''''''''''''''''
            If form = "" Then
                form = obj + "@#@" + formu
            Else
                form = form + "~" + obj + "@#@" + formu
            End If
        End If
home:
        If syn = "" Then
            formulas = form + "^^" + fname
        Else
            formulas = syn
        End If


        Return formulas
    End Function
    ''update color condition
    <Ajax.AjaxMethod()> _
   Public Function updateColorcondition(ByVal allCond As String, ByVal toDel As String, ByVal condition As String, ByVal objBtn As String)
        Dim sp = Split(allCond, "~")
        Dim cond = ""
        Dim toRep = ""
        Dim o = 0
        Dim b = 0
        Dim c = 0
        Dim var = Split(objBtn, " AS ")   '' if formula exixts
        If (var.length > 1) Then
            objBtn = var(1)
        End If
        For o = 0 To sp.length - 1
            Dim hj = Split(sp(o), "@#@")
            If (objBtn = hj(0)) Then '' button found
                b = 1
                '' search for the condition
                Dim sp2 = Split(hj(1), "##")
                Dim j = 0
                For j = 0 To sp2.length - 1
                    Dim tm = Split(sp2(j), "#@#")
                    If tm(0) = toDel Then '' condition found then update condition
                        toRep = sp(o)
                        cond = Replace(sp(o), sp2(j), condition)
                        c = 1
                        GoTo finish1
                    End If
                Next
                If (c = 0) Then '' append condition in the existing button
                    toRep = sp(o)
                    cond = sp(o) + "##" + condition

                    GoTo finish1
                End If
            End If
        Next
        If (b = 0) Then
            If (Trim(allCond) = "") Then
                allCond = objBtn + "@#@" + condition
            Else
                allCond = allCond + "~" + objBtn + "@#@" + condition
            End If
            GoTo finish
        End If
finish1:
        allCond = Trim(Replace(allCond, toRep, cond))
finish:
        Return allCond
    End Function
    ''delete color condition
    <Ajax.AjaxMethod()> _
   Public Function deleteColorcondition(ByVal allCond As String, ByVal toDel As String, ByVal objBtn1 As String)
        Dim sp = Split(allCond, "~")
        Dim cond = ""
        Dim toRep = ""
        Dim torep1 = ""
        Dim o = 0
        Dim objBtn = objBtn1
        Dim var = Split(objBtn1, " AS ")    '' if formula exixts
        If (var.length > 1) Then
            objBtn = var(1)
        End If
        For o = 0 To sp.length - 1
            Dim hj = Split(sp(o), "@#@")
            If (objBtn = hj(0)) Then '' button found
                '' search for the condition
                Dim sp2 = Split(hj(1), "##")
                Dim j = 0
                For j = 0 To sp2.length - 1
                    Dim tm = Split(sp2(j), "#@#")
                    If tm(0) = toDel Then '' condition found
                        '' match all conditions of condition existance
                        Dim cv = allCond + " "
                        Dim cv1 = " " + allCond + " "
                        If (cv1.Contains(" " + objBtn + "@#@" + sp2(j) + " ")) Then ' objbtn@#@condition
                            toRep = objBtn + "@#@" + sp2(j)  '' the only condition deleted
                            'ElseIf (cv1.Contains(" " + objBtn + "@#@" + sp2(j) + " ")) Then ' objbtn@#@condition
                            '    toRep = objBtn + "@#@" + sp2(j)  '' the only condition deleted
                        ElseIf (allCond.Contains("~" + objBtn + "@#@" + sp2(j))) Then
                            toRep = "~" + objBtn + "@#@" + sp2(j)

                        ElseIf (allCond.Contains(objBtn + "@#@" + sp2(j) + "~")) Then
                            toRep = objBtn + "@#@" + sp2(j) + "~"
                        ElseIf allCond.Contains(objBtn + "@#@" + sp2(j) + "##") Then
                            toRep = sp2(j) + "##"
                        ElseIf allCond.Contains("##" + sp2(j) + "##") Then
                            toRep = sp2(j) + "##"
                        ElseIf cv.Contains("##" + sp2(j) + " ") Then
                            toRep = "##" + sp2(j)
                        End If
                        ''
                        allCond = Trim(Replace(allCond, toRep, ""))

                        'torep1 = sp(0)
                        'cond = Replace(sp(0), toRep, "")
                        Exit For
                        Exit For
                    End If
                Next
            End If
        Next
        ' allCond = Trim(Replace(allCond, torep1, cond))
        Return allCond
    End Function
    ''delete color condition when object is deleted
    <Ajax.AjaxMethod()> _
    Public Function btnDelColConDel(ByVal nullObj As String, ByVal allCond As String, ByVal objBtn As String)

        nullObj = Replace(nullObj, "aa", "")
        objBtn = Replace(objBtn, nullObj, "")
        ' fetch button name
        Dim arr = Split(objBtn, " AS ")    '' if formula exixts
        Dim neCon = ""
        Dim obname = objBtn
        If arr.length > 1 Then
            obname = arr(1)
        End If
        ''''''''''

        Dim sp = Split(allCond, "~")
        Dim o = 0
        Dim toRep = ""
        For o = 0 To sp.length - 1
            Dim hj = Split(sp(o), "@#@")
            If (obname <> hj(0)) Then ''if not the same object
                If neCon = "" Then
                    neCon = sp(o)
                Else
                    neCon = neCon + "~" + sp(o)
                End If
            End If
        Next
        allCond = Trim(neCon)
        Return allCond
    End Function
    ''form groupby
    <Ajax.AjaxMethod()> _
    Public Function formGroupby(ByVal elem As String, ByVal formul As String, ByVal grpby As String)
        Dim str = ""

        If formul <> "" Then
            Dim for1 = Split(formul, "~")
            Dim nm = 0
            For nm = 0 To for1.length - 1
                Dim bnm = Split(for1(nm), " AS ")
                elem = Replace(elem, bnm(1), for1(nm))
            Next
        End If

        Dim hj As String() = Split(elem, "~")
        Dim k = 0
        If Trim(grpby) <> "" Then
            Dim grp As String() = Split(grpby, ",")
            Dim gb = 0
            For k = 0 To hj.Length - 1
                Dim b = 0
                ' If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
                Dim vbn = LCase(hj(k))
                If vbn.Contains("sum(") Or vbn.Contains("max(") Or vbn.Contains("min(") Or vbn.Contains("avg(") Or vbn.Contains("count(") Then
                    Dim nm = 1
                    For gb = 0 To grp.Length - 1
                        Dim hjk = Split(hj(k), " AS ")
                        If hjk(0) = grp(gb) Then
                            nm = 0
                        End If
                    Next
                    If nm = 1 Then
                        b = 1
                    End If
                End If
                If b = 0 Then
                    If str = "" Then
                        Dim hjk = Split(hj(k), " AS ")
                        str = hjk(0)

                    Else
                        Dim hjk = Split(hj(k), " AS ")
                        str = str + "~" + hjk(0)
                    End If
                End If
            Next
        Else

            For k = 0 To hj.Length - 1
                Dim b = 0
                ' If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
                Dim vbn = LCase(hj(k))
                If vbn.Contains("sum(") Or vbn.Contains("max(") Or vbn.Contains("min(") Or vbn.Contains("avg(") Or vbn.Contains("count(") Then
                    b = 1
                End If
                If b = 0 Then
                    If str = "" Then
                        str = hj(k)
                    Else
                        str = str + "~" + hj(k)
                    End If
                End If
            Next

        End If
        str = Replace(str, "$", ".")

        Return str
    End Function
End Class
