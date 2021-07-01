Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.UI.HtmlTextWriter
Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class DataManagerAjax
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    ''' <summary>
    ''' Function to bind Table in listbox
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
     Public Function dind_list(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal userid As String)
        ds.Clear()
        Dim clientvalue, lobvalue
        If clientid = "--Select--" Or clientid = "" Then
            clientvalue = 0
        Else
            clientvalue = clientid
        End If
        If lobid = "--Select--" Or lobid = "" Then
            lobvalue = 0
        Else
            lobvalue = lobid
        End If
        cmd = New SqlCommand("select_Tablename", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@cientid").Value = clientvalue
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobvalue
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    <Ajax.AjaxMethod()> _
     Public Function user_datatable(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal userid As String)
        Dim clientvalue, lobvalue
        If clientid = "--Select--" Or clientid = "" Then
            clientvalue = 0
        Else
            clientvalue = clientid
        End If
        If lobid = "--Select--" Or lobid = "" Then
            lobvalue = 0
        Else
            lobvalue = lobid
        End If
        Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
        cmdupdate.CommandType = CommandType.StoredProcedure
        With cmdupdate.Parameters
            .AddWithValue("@userid", userid)
            .AddWithValue("@Deptid", deptid)
            .AddWithValue("@Clientid", clientvalue)
            .AddWithValue("@LOBID", lobvalue)
        End With
        Dim readerdata As SqlDataReader
        con.Open()
        readerdata = cmdupdate.ExecuteReader

        Dim b As Boolean = False
        If readerdata.HasRows Then
            b = True
        End If
        con.Close()
        readerdata.Close()
        ds.Clear()
        If b = True Then
            cmd = New SqlCommand("select_admintablesTablename", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@deptid").Value = deptid
            cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@cientid").Value = clientvalue
            cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@lobid").Value = lobvalue
            cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@userid").Value = userid
            con.Open()
            da.SelectCommand = cmd
            da.Fill(ds)
            ' dr = comdepart.ExecuteReader
            con.Close()
            Return ds
        Else
            cmd = New SqlCommand("select_adminwitoutspan", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@deptid").Value = deptid
            cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@cientid").Value = clientvalue
            cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@lobid").Value = lobvalue
            cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
            cmd.Parameters("@userid").Value = userid
            con.Open()
            da.SelectCommand = cmd
            da.Fill(ds)
            ' dr = comdepart.ExecuteReader
            con.Close()
            Return ds
     
        End If

    End Function
    <Ajax.AjaxMethod()> _
    Public Function dind_listpurg(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal userid As String)
        ds.Clear()
        Dim clientvalue, lobvalue
        If clientid = "--Select--" Or clientid = "" Then
            clientvalue = 0
        Else
            clientvalue = clientid
        End If
        If lobid = "--Select--" Or lobid = "" Then
            lobvalue = 0
        Else
            lobvalue = lobid
        End If
        cmd = New SqlCommand("select_pergingTablename", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@cientid").Value = clientvalue
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobvalue
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    ''' <summary>
    ''' Function to Bind column Name
    ''' </summary>
    ''' <param name="colname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
     Public Function dind_Collist(ByVal colname As String)
        Dim dr As SqlDataReader
        ds.Clear()
        Dim colvalue As String = ""
        cmd = New SqlCommand("select_Colname", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@tableid", SqlDbType.NVarChar, 100)
        cmd.Parameters("@tableid").Value = colname
        con.Open()
        da.SelectCommand = cmd
        dr = cmd.ExecuteReader
        If dr.Read Then
            colvalue = dr("visiblecolumn")
        End If
        dr.Close()
        con.Close()
        Return colvalue
    End Function
    ''' <summary>
    ''' function to bind column value according to Selected Column name
    ''' </summary>
    ''' <param name="colnamevalue"></param>
    ''' <param name="tablenamevalue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
   Public Function dind_Colvalue(ByVal colnamevalue As String, ByVal tablenamevalue As String)
        ds.Clear()
        cmd = New SqlCommand("select distinct " & colnamevalue & " as colvalue from  " & tablenamevalue & " ", con)
        con.Open()
        da.SelectCommand = cmd
        con.Close()
        da.Fill(ds)
        Return ds
    End Function
    ''' <summary>
    ''' Function to show no of colmn affected 
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="wherecol1"></param>
    ''' <param name="wherecol2"></param>
    ''' <param name="wherecol3"></param>
    ''' <param name="wherecol4"></param>
    ''' <param name="wherecol5"></param>
    ''' <param name="wherecol6"></param>
    ''' <param name="wherecol7"></param>
    ''' <param name="likeget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
    Public Function rowshow(ByVal tablename As String, ByVal wherecol1 As String, ByVal wherecol2 As String, ByVal wherecol3 As String, ByVal wherecol4 As String, ByVal wherecol5 As String, ByVal wherecol6 As String, ByVal wherecol7 As String, ByVal likeget As String)

    
        'ds1.Clear()

        'comdepart = New SqlCommand("select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'", connection)
        Dim drgetvalue As SqlDataReader
        Dim querystring As String = ""
        ''comdepart = New SqlCommand("select * from " & tablename & "", connection)
        ''connection.Open()
        'Dim strdiv As String = ""
        ''strdiv = strdiv & "<table width=100% border=1 bordercolor=darkgray cellspacing=0 cellpadding=0>"
        ''strdiv = strdiv & "<tr>"
        ''Dim j As Integer = 0

        ''dr = comdepart.ExecuteReader
        ''For j = 0 To dr.FieldCount - 1

        ''    strdiv = strdiv & "<td width=10%>" & dr.GetName(j) & "</td>"
        ''Next

        ''dr.Close()
        ''connection.Close()
        ''strdiv = strdiv & "</tr>"

        'comdepartgetvalue = New SqlCommand("select * from " & tablename & " ", con)
        If wherecol2 = 1 Then
            wherecol2 = "="
        ElseIf wherecol2 = 2 Then
            wherecol2 = "<"
        ElseIf wherecol2 = 3 Then
            wherecol2 = ">"
        ElseIf wherecol2 = 4 Then
            wherecol2 = "Like"
        ElseIf wherecol2 = 5 Then
            wherecol2 = "Between"
        End If


        If wherecol6 = 1 Then
            wherecol6 = "="
        ElseIf wherecol6 = 2 Then
            wherecol6 = "<"
        ElseIf wherecol6 = 3 Then
            wherecol6 = ">"
        ElseIf wherecol6 = 4 Then
            wherecol6 = "Like"
        ElseIf wherecol6 = 5 Then
            wherecol6 = "Between"
        End If

        If wherecol2 <> "Between" And wherecol2 <> "Like" And wherecol6 = "0" Then
            querystring = "select count(*)as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'"

        ElseIf wherecol2 <> "Between" And wherecol2 <> "4" And wherecol6 <> "0" Then
            querystring = "select count(*)as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "' and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"

        ElseIf wherecol2 = "Between" And wherecol6 = "0" Then
            'comdepart = New SqlCommand("select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'", connection)
            'connection.Open()
            querystring = "select count(*) as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'and '" & wherecol4 & "'"
        ElseIf wherecol2 = "Between" And wherecol6 <> "0" Then
            querystring = "select count(*) as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'and '" & wherecol4 & "'and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"


        ElseIf wherecol2 = "Like" And wherecol6 = "0" Then
            querystring = "select count(*) as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & likeget & "%'"

        ElseIf wherecol2 = "Like" And wherecol6 <> "0" Then
            querystring = "select count(*)  as count from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & likeget & "%' and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"

        End If
        cmd = New SqlCommand(querystring, con)
        con.Open()
        drgetvalue = cmd.ExecuteReader
        'Dim i As Integer
        ' drgetvalue.Read
        Dim rowcount As Integer
        If drgetvalue.Read Then
            rowcount = drgetvalue("count")
        End If
        'strdiv = strdiv & "<tr>"
        'For i = 0 To drgetvalue.FieldCount - 1
        '    strdiv = strdiv & "<td> " & drgetvalue.GetValue(i) & "</td>"
        'Next
        'strdiv = strdiv & "</tr>"
        'End While
        drgetvalue.Close()
        con.Close()
        'strdiv = strdiv & "<table>"
        Return rowcount

    End Function
    ''' <summary>
    ''' Function to show data
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="wherecol1"></param>
    ''' <param name="wherecol2"></param>
    ''' <param name="wherecol3"></param>
    ''' <param name="wherecol4"></param>
    ''' <param name="wherecol5"></param>
    ''' <param name="wherecol6"></param>
    ''' <param name="wherecol7"></param>
    ''' <param name="likeget"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
      Public Function RepShow(ByVal tablename As String, ByVal wherecol1 As String, ByVal wherecol2 As String, ByVal wherecol3 As String, ByVal wherecol4 As String, ByVal wherecol5 As String, ByVal wherecol6 As String, ByVal wherecol7 As String, ByVal likeget As String)


        ds.Clear()

        Dim drgetvalue As SqlDataReader
        Dim querystring As String = ""
        cmd = New SqlCommand("select * from " & tablename & "", con)
        con.Open()
        Dim strdiv As String = ""
        strdiv = strdiv & "<table width=100% border=1 bordercolor=darkgray cellspacing=0 cellpadding=0>"
        strdiv = strdiv & "<tr bgcolor=#42969f color=#ffffff>"
        Dim j As Integer = 0
        dr = cmd.ExecuteReader
        For j = 0 To dr.FieldCount - 1
            strdiv = strdiv & "<td width=10%>" & dr.GetName(j) & "</td>"
        Next
        dr.Close()
        con.Close()
        strdiv = strdiv & "</tr>"

        If wherecol2 = 1 Then
            wherecol2 = "="
        ElseIf wherecol2 = 2 Then
            wherecol2 = "<"
        ElseIf wherecol2 = 3 Then
            wherecol2 = ">"
        ElseIf wherecol2 = 4 Then
            wherecol2 = "Like"
        ElseIf wherecol2 = 5 Then
            wherecol2 = "Between"
        End If

        If wherecol6 = 1 Then
            wherecol6 = "="
        ElseIf wherecol6 = 2 Then
            wherecol6 = "<"
        ElseIf wherecol6 = 3 Then
            wherecol6 = ">"
        ElseIf wherecol6 = 4 Then
            wherecol6 = "Like"
        ElseIf wherecol6 = 5 Then
            wherecol6 = "Between"
        End If
        'comdepartgetvalue = New SqlCommand("select * from " & tablename & " ", con)
        If wherecol2 <> "Between" And wherecol2 <> "Like" And wherecol6 = "0" Then
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'"

        ElseIf wherecol2 <> "Between" And wherecol2 <> "Like" And wherecol6 <> "0" Then
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "' and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"

        ElseIf wherecol2 = "Between" And wherecol6 = "0" Then
            'comdepart = New SqlCommand("select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'", connection)
            'connection.Open()
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'and '" & wherecol4 & "'"
        ElseIf wherecol2 = "Between" And wherecol6 <> "0" Then
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & wherecol3 & "'and '" & wherecol4 & "'and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"


        ElseIf wherecol2 = "Like" And wherecol6 = "0" Then
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & likeget & "%'"

        ElseIf wherecol2 = "Like" And wherecol6 <> "0" Then
            querystring = "select * from " & tablename & " where  " & wherecol1 & " " & wherecol2 & " '" & likeget & "%' and " & wherecol5 & " " & wherecol6 & " '" & wherecol7 & "'"

        End If
        cmd = New SqlCommand(querystring, con)
        con.Open()
        drgetvalue = cmd.ExecuteReader
        Dim i As Integer
        While drgetvalue.Read
            strdiv = strdiv & "<tr bgcolor=#f5f5f5>"
            For i = 0 To drgetvalue.FieldCount - 1
                strdiv = strdiv & "<td> " & drgetvalue.GetValue(i) & "</td>"
            Next
            strdiv = strdiv & "</tr>"
        End While
        drgetvalue.Close()
        con.Close()
        strdiv = strdiv & "<table>"
        Return strdiv & "," & querystring
    End Function
    ''' <summary>
    ''' Function to Count row which are going to be purge
    ''' </summary>
    ''' <param name="tablename"></param>
    ''' <param name="condition"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
      Public Function Rowcount(ByVal tablename As String, ByVal condition As String)
        Dim drgetvalue As SqlDataReader
        Dim querystring As String = ""
        Dim res As String = ""
        Try

            querystring = "select count(*)as count from " & tablename & " where  " & condition & " "

            cmd = New SqlCommand(querystring, con)
            con.Open()
            drgetvalue = cmd.ExecuteReader
            Dim rowvalue As Integer
            If drgetvalue.Read Then
                rowvalue = drgetvalue("count")
            End If

            drgetvalue.Close()
            con.Close()
            res = CType(rowvalue, String)
            Return res

        Catch ex As Exception

            res = "f" & "," & "Apply Condition Properly!!!"
            Return res

        End Try
        Return res
    End Function

    
End Class

