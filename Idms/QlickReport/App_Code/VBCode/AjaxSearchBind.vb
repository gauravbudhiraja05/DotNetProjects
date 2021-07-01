Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class AjaxSearchBind
    Dim conStr As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    <Ajax.AjaxMethod()> _
    Public Function searchOnUserNameOrId(ByVal searchKey As String) As String

        Dim cmdStr As String = "Select adminid, adminname from masteradmin where adminid like '" + searchKey + "%' or adminname like '" + searchKey + "%'"

        da = New SqlDataAdapter(cmdStr, con)

        da.Fill(ds)
        Dim str As String = ""
        Dim i As Integer
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("adminName") + "#" + ds.Tables(0).Rows(i).Item("adminid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("adminName") & "#" & ds.Tables(0).Rows(i).Item("adminid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()

        Return str

    End Function
    <Ajax.AjaxMethod()> _
    Public Function bind_Deptartment()
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_getDepartment", con)
        cmd.CommandType = CommandType.StoredProcedure
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    '' to bind dept of super admin
    <Ajax.AjaxMethod()> _
    Public Function bind_SuperAdminDept()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_superadmindept", con)
        cmd.CommandType = CommandType.StoredProcedure
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    '' to bind the dept of an admin who logged in
    <Ajax.AjaxMethod()> _
    Public Function bind_AdminDept(ByVal loggedAdmin As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_AdminDept", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@adminid").Value = loggedAdmin
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    '' for super admin
    <Ajax.AjaxMethod()> _
    Public Function bindClientOnDept(ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_client1", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptId
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)

        con.Close()
        Return ds

    End Function

    '' for admin 
    <Ajax.AjaxMethod()> _
    Public Function bindAdminClients(ByVal adminId As String, ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_AdminClients", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptId
        cmd.Parameters("@adminid").Value = adminId
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)

        con.Close()
        Return ds

    End Function

    <Ajax.AjaxMethod()> _
    Public Function bindLobOnDeptClient(ByVal deptId As String, ByVal cientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_lob", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptId
        cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@cientid").Value = cientId
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds

    End Function
    <Ajax.AjaxMethod()> _
    Public Function bindReports(ByVal deptId As String, ByVal clientId As String, ByVal lobId As String)
        ds.Clear()
        da = New SqlDataAdapter
        'cmd = New SqlCommand("select_Reports", con)
        'cmd.CommandType = CommandType.StoredProcedure
        cmd = New SqlCommand("select distinct queryname from idmsquerymaster where departmentid=" + deptId + " and clientid=" + clientId + " and underlob=" + lobId + "", con)

        da.SelectCommand = cmd
        con.Open()
        cmd.ExecuteNonQuery()
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function



    <Ajax.AjaxMethod()> _
    Public Function bindAdminLobOnDeptClient(ByVal adminId As String, ByVal deptId As String, ByVal cientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("select_AdminLOBs", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@adminid").Value = adminId
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptId
        cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@cientid").Value = cientId
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds

    End Function
    'ranjit department function
    <Ajax.AjaxMethod()> _
    Public Function bind_client(ByVal deptid As String)
        ds.Clear()

        cmd = New SqlCommand("select_client", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds

    End Function
    <Ajax.AjaxMethod()> _
    Public Function bind_Department()
        cmd = New SqlCommand("select_dept", con)
        cmd.CommandType = CommandType.StoredProcedure
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader


        con.Close()

        Return ds

    End Function
    ''ranjit to show html report acc to dept
    <Ajax.AjaxMethod()> _
    Public Function bind_reportdepthtml(ByVal deptid As String)
        ds.Clear()

        cmd = New SqlCommand("select_htmlReport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    'ranjit 
    <Ajax.AjaxMethod()> _
    Public Function bind_reportclienthtml(ByVal deptid As String, ByVal clintid As String)
        ' ds.Clear()
        Dim ds1 As New DataSet
        cmd = New SqlCommand("select_htmlclientReport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clintid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = "0"
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds1
    End Function
    'ranjit 
    <Ajax.AjaxMethod()> _
    Public Function bind_lobonly(ByVal deptid As String, ByVal clintid As String)
        ds.Clear()

        cmd = New SqlCommand("select_lobforhtml", con)
        ' cmd = New SqlCommand("select * from warslobmaster where departmentid=@deptid and clientid=@clientid ", con)

        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clintid

        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bind_reprtfromlob(ByVal deptid As String, ByVal clintid As String, ByVal lobid As String)
        ds.Clear()

        cmd = New SqlCommand("select_lobreports", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clintid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid

        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bind_htmlreport(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sel_htmlreport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@cientid").Value = cientid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        con.Close()
        Return ds

    End Function
    ''''''trending and suggession'''''''
    <Ajax.AjaxMethod()> _
    Public Function selecttrendingformula(ByVal functionname As String, ByVal tablename As String)
        ds.Clear()
        Dim val As String = ""
        Dim dr As SqlDataReader
        cmd = New SqlCommand("select " + functionname + " as name from  trendingandsuggessionstable", con)

        con.Open()
        dr = cmd.ExecuteReader
        If dr.Read Then
            If val = "" Then
                val = dr("name").ToString
            End If
        End If
        con.Close()
        Return val
    End Function
    <Ajax.AjaxMethod()> _
    Public Function stringreplace(ByVal functionname As String, ByVal valueoffunction As String, ByVal columnwithfunction As String)
        ds.Clear()
        Dim val As String = functionname
        Dim ss As String = valueoffunction
        Dim gg As String = columnwithfunction
        Dim array = ss.Split(",")
        Dim funtions = gg.Split(",")
        Dim i, j As Integer
        i = UBound(array)
        For j = 0 To i
            If val.Contains(funtions(j)) Then
                val = val.Replace(funtions(j), array(j))

            End If

        Next

        Return val
    End Function
    <Ajax.AjaxMethod()> _
    Function executetrendingformula(ByVal textformula As String, ByVal matrixcolumn As String, ByVal allcolumns As String, ByVal calculatedcolumns As String, ByVal tabname As String, ByVal groupby As String, ByVal bucketcondition As String, ByVal calculatedval As String, ByVal varianceval As String, ByVal aggregatevalue As String)
        Dim gpby As String = ""
        Dim vary As Double = 0
        Dim column As DataColumn
        Dim roedata As DataRow
        Dim nowcolll As String = matrixcolumn
        If bucketcondition <> "" Then
            bucketcondition = "where" & " " & bucketcondition
        End If
        Dim columnoftable As String = matrixcolumn + allcolumns
        Dim vary1, vary2 As String
        gpby = groupby
        Dim mmnn = aggregatevalue.Split(",")
        Dim hh, aa As Integer
        hh = UBound(mmnn)

        If gpby <> "--select--" Then
            gpby = "Group By" & " " & columnoftable & gpby
        Else
            gpby = "Group By" & " " & columnoftable

        End If
        'For aa = 0 To hh
        '    gpby = gpby.Replace(mmnn(aa), "")


        'Next
        gpby = gpby.Replace(",,,,,,", ",")
        gpby = gpby.Replace(",,,,,", ",")
        gpby = gpby.Replace(",,,,", ",")
        gpby = gpby.Replace(",,,", ",")
        gpby = gpby.Replace(",,", ",")

        Dim leng As Integer = gpby.Length

        If gpby.EndsWith(",,") Then
            gpby = gpby.Remove(leng - 3, 2)
        End If
        If gpby.EndsWith(",") Then
            gpby = gpby.Remove(leng - 1, 1)
        End If
        Try
            'Dim between As String = ""

            If varianceval <> "" Then


                vary = (calculatedval * varianceval) / 100
                vary1 = calculatedval - vary
                vary2 = calculatedval + vary

            Else
                'not select not ""
                vary = calculatedval
            End If
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            Dim strs As String = "3"
            Return (strs & "," & strmessage)
        End Try

        Dim ints As String = "1"
        If columnoftable = "" Then
            Return ints
        End If
        Dim int1 As Integer = allcolumns.Length
        allcolumns = allcolumns.Remove(int1 - 1, 1)
        Dim cols As String = ""
        Dim count As String = ""
        Dim formulascalculated As String = ""
        'solution to pate textformula calculatedcolumns
        If textformula <> "" And calculatedcolumns <> "null" Then
            formulascalculated = "," & " " & " (" & textformula & ")" & " " & "as" & " " & "Formula"
            'formulascalculated = "," & " " & " ((" & calculatedcolumns & ")" & " " & "*" & " " & "100) /" & vary & " " & "as" & " " & "Formula"
        ElseIf textformula <> "" And calculatedcolumns = "null" Then
            formulascalculated = "," & " " & " (" & textformula & ")" & " " & "as" & " " & "Formula"
        ElseIf matrixcolumn = "" And textformula = "" Then
            formulascalculated = ""
        Else
            formulascalculated = "," & " " & " (" & allcolumns & ")" & " " & "as" & " " & "Formula"
        End If
        'Dim namehead As String
        'namehead = formulascalculated
        'namehead = namehead.Replace(",", "")
        'namehead = namehead.Replace("as", "")
        'namehead = namehead.Replace("Formula", "")
        If matrixcolumn <> "" Then


            Dim int As Integer = matrixcolumn.Length

            matrixcolumn = matrixcolumn.Remove(int - 1, 1)

        End If
        Dim allnbhh As String = matrixcolumn

        'Dim int1 As Integer = allcolumns.Length
        'allcolumns = allcolumns.Remove(int1 - 1, 1)
        Dim allclmn = matrixcolumn.Split(",")
        Dim allcl = allcolumns.Split(",")
        Dim i, j, k, l As Integer
        Dim colnames As String = ""
        i = UBound(allcl)
        'For k = 0 To i
        '    If allcl(k) = "" Then
        '        allcl(k) = allcl(k).ToString.Insert(k, "kuchnahiyahanpe")
        '    End If
        'Next
        For k = 0 To i
            If allcl(k) = "" Then

            End If
            If matrixcolumn.Contains(allcl(k)) Then
                If cols = "" Then
                    cols = matrixcolumn
                Else
                    cols = cols
                End If

            Else

                If matrixcolumn = "" Then
                    cols = allcl(k)
                Else
                    cols = matrixcolumn & "," & allcl(k)

                End If





            End If
        Next
        If cols.Contains(",,") Then
            cols = cols.Replace(",,", ",")
        End If
        For j = 0 To i
            Dim str As String = allcl(j).ToString
            If matrixcolumn.Contains(allcl(j).ToString) Then
                matrixcolumn = matrixcolumn.Replace(allcl(j), "")


                matrixcolumn = matrixcolumn & "," & "(Convert(numeric," + allcl(j) + ")) as " & allcl(j)
            Else


                If matrixcolumn = "" Then
                    matrixcolumn = "(Convert(numeric," + allcl(j) + ")) as " & allcl(j)
                Else
                    matrixcolumn = matrixcolumn & "," & "(Convert(numeric," + allcl(j) + ")) as " & allcl(j)
                End If


            End If
        Next
        If matrixcolumn.Contains(",,") Then
            matrixcolumn = matrixcolumn.Replace(",,", ",")
        End If
        'Dim b As Boolean
        'cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
        'con.Open()
        'dr = cmd.ExecuteReader
        'While dr.Read()
        '    If dr("name") = "trendingandsuggessionstable" Then
        '        b = False
        '        Exit While

        '    Else
        '        b = True
        '    End If
        'End While
        'dr.Close()
        'con.Close()

        'If b = False Then


        '    cmd = New SqlCommand("drop table trendingandsuggessionstable", con)
        '    con.Open()
        '    cmd.ExecuteNonQuery()
        '    con.Close()
        'End If
        'ds.Clear()
        Try
            Dim allcolumnwith As String = ""
            If matrixcolumn = "" Then
                allcolumnwith = ""
            Else
                allcolumnwith = "," & matrixcolumn

            End If
            If allcolumnwith.Contains(",,") Then
                allcolumnwith = allcolumnwith.Replace(",,", ",")
            End If
            'cmd = New SqlCommand("select Identity(int, 1,1) as RecordId " + allcolumnwith + " into trendingandsuggessionstable from " + tabname + "  " + bucketcondition + "", con)

            'con.Open()
            'cmd.ExecuteNonQuery()
            'con.Close()
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            Dim strs As String = "3"
            Return (strs & "," & strmessage)
        End Try
        'cmd = New SqlCommand("select count(*) as counts from trendingandsuggessionstable", con)
        'con.Open()
        'dr = cmd.ExecuteReader
        'If dr.Read() Then
        '    count = dr("counts")


        'End If
        'con.Close()
        'dr.Close()

        'Dim counter As Integer = CType(count, Integer)
        Dim colss = allnbhh.Split(",")
        Dim start, ends As Integer
        ends = UBound(colss)
        Dim tablestr As String = ""
        tablestr = "<table border=2>"
        tablestr = tablestr & "<tr>"
        For start = 0 To ends
            tablestr = tablestr & "<td><b>" & colss(start) & "</b></td>"
        Next



        If textformula <> "" Then
            tablestr = tablestr & "<td><b>" & textformula & "</b> </td>"
        ElseIf textformula = "" Then

            tablestr = tablestr & "<td><b> Formula </b> </td>" 'allcolumns
        End If
        tablestr = tablestr & "</tr>"
        Dim val As String = ""
        'For l = 1 To counter
        Dim conter1 As String = ""
        conter1 = CType(l, String)
        Dim ds As New DataSet
        Try
            Dim formulahere As String = ""
            If formulascalculated <> "" Then
                formulahere = "order by Formula desc"
            End If


            Dim add As New SqlDataAdapter("select " + allnbhh + formulascalculated + " from " + tabname + " " + bucketcondition + " " + gpby + " " + formulahere + " ", con)
            con.Open()

            add.Fill(ds)
            con.Close()
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")

            Dim strs As String = "3"
            Return (strs & "," & strmessage)
        End Try
        Dim varyy, varyy2 As Double
        'While dr.Read()
        Try
            For Each roedata In ds.Tables(0).Rows
                tablestr = tablestr & "<tr>"
                For Each column In ds.Tables(0).Columns




                    'For start = 0 To ends
                    Dim stttt As String = roedata.Item(column.ColumnName).ToString
                    If stttt = "" Then
                        val = 0
                    Else
                        val = roedata.Item(column.ColumnName)
                    End If
                    'val = roedata.Item(column.ColumnName)   'dr(colss(start)).ToString
                    If val = "" Then
                        val = "0"
                    End If

                    varyy = CType(vary1, Double)
                    varyy2 = CType(vary2, Double)

                    Dim dbnull As Double = 0
                    Dim stnewa As String = roedata.Item("Formula")
                    If stnewa = "" Then
                        dbnull = 0
                    Else
                        dbnull = roedata.Item("Formula")
                    End If
                    If formulascalculated <> "" Then

                        If varyy <> varyy2 Then
                            If dbnull <= varyy Then
                                tablestr = tablestr & "<td title=Red style= background-color:#ff5f32>" & val & "</td>"
                            End If
                            If dbnull >= varyy2 Then
                                tablestr = tablestr & "<td title=Green style= background-color:#99ff8e>" & val & "</td>"
                            End If
                            If dbnull > varyy And dbnull < varyy2 Then
                                tablestr = tablestr & "<td title=Yellow style= background-color:#ffff8b>" & val & "</td>"
                            End If
                        Else
                            If dbnull < varyy Then
                                tablestr = tablestr & "<td title=Red style= background-color:#ff5f32>" & val & "</td>"
                            End If
                            If dbnull >= varyy2 Then
                                tablestr = tablestr & "<td title=Green style= background-color:#99ff8e>" & val & "</td>"
                            End If
                        End If
                    Else
                        tablestr = tablestr & "<td>" & val & "</td>"
                    End If
                    'Next
                    If val = "" Then
                        val = "0"
                    End If

                    'If formulascalculated <> "" Then
                    '    If roedata.Item("Formula") = "" Then
                    '        val = 0
                    '    Else
                    '        val = roedata.Item("Formula")
                    '    End If

                    '    'val = roedata.Item("Formula").ToString
                    '    If varyy <> varyy2 Then
                    '        If val <= varyy Then
                    '            tablestr = tablestr & "<td title=Red style= background-color:#ff5f32>" & val & "</td>"
                    '        End If
                    '        If val >= varyy2 Then
                    '            tablestr = tablestr & "<td title=Green style=background-color:#99ff8e>" & val & "</td>"
                    '        End If
                    '        If val > varyy And val < varyy2 Then
                    '            tablestr = tablestr & "<td title=Yellow style= background-color:#ffff8b>" & val & "</td>"
                    '        End If
                    '    Else
                    '        If val < varyy Then
                    '            tablestr = tablestr & "<td title=Red style= background-color:#ff5f32>" & val & "</td>"
                    '        End If
                    '        If val >= varyy2 Then
                    '            tablestr = tablestr & "<td title=Green style= background-color:#99ff8e>" & val & "</td>"
                    '        End If
                    '    End If
                    '    'End If

                    'End If


                Next
                tablestr = tablestr & "</tr>"
            Next
            'End While
            'dr.Close()



            'Next
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            Dim strs As String = "3"
            Return (strs & "," & strmessage)
        End Try
        tablestr = tablestr & "</table>"
        Return tablestr


    End Function
    <Ajax.AjaxMethod()> _
    Function valchk(ByVal int As String)
        Dim strmessage As String = ""
        Try
            Dim int1 As Double = CType(int, Double)
        Catch ex As Exception

            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")

        End Try
        Return strmessage
    End Function
    <Ajax.AjaxMethod()> _
    Public Function BindGraphReportForAdmin(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_trackGraphReportForAdmin", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid

        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid

        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clientid

        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid

        da.SelectCommand = cmd
        con.Open()
        da.Fill(ds)
        con.Close()
        Return ds
    End Function

    <Ajax.AjaxMethod()> _
    Function datecheck(ByVal val As String)
        Try


            Dim datecum As Date = CType(val, Date)

        Catch ex As Exception
            Dim str As String = "1"
            Return str
        End Try
        Return "no"
    End Function
    'Public Sub aspnet_msgbox(ByVal message As String)
    '    System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
    '    System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
    '    System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    'End Sub
    <Ajax.AjaxMethod()> _
    Public Function BindDeptUser(ByVal deptId As String)
        ds.Clear()
        Dim str As String
        str = "Select UserID from Registration where DeptID=" + deptId
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    ''Get All User
    <Ajax.AjaxMethod()> _
    Public Function GetAllUser()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(UserID) from Registration"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bindSupAdminDiv(ByVal searchKey As String, ByVal deptId As String, ByVal clientId As String, ByVal lobId As String)
        Dim i As Integer
        Dim cmdStr As String
        Dim arrQueryString() As String = {deptId, clientId, lobId}
        Dim strQuery() As String = {" AND DeptID", " AND ClientID", " AND LOBID"}
        Dim strConditions As String = ""
        For i = 1 To arrQueryString.Length - 2
            If arrQueryString(i) <> "0" Then
                strConditions += strQuery(i - 1) + "=" + arrQueryString(i - 1)
            End If
        Next
        If strConditions.StartsWith(" AND") Then
            strConditions = strConditions.Remove(0, 4)
        End If
        If strConditions <> "" Then
            cmdStr = "Select adminid, adminname from masteradmin where (" + strConditions + ") and (adminid like '" + searchKey + "%')"
        Else
            cmdStr = "Select adminid, adminname from masteradmin where adminid like '" + searchKey + "%'"
        End If
        ds.Clear()
        da = New SqlDataAdapter(cmdStr, con)
        da.Fill(ds)
        Dim str As String = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("adminName") + "#" + ds.Tables(0).Rows(i).Item("adminid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("adminName") & "#" & ds.Tables(0).Rows(i).Item("adminid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bind_AllDivAdmin(ByVal search As String, ByVal userid As String)
        Dim i As Integer
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_AllDivAdmin", con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid

        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200)
        cmd.Parameters("@Search").Value = search
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        Dim str As String = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("adminName") + "#" + ds.Tables(0).Rows(i).Item("adminid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("adminName") & "#" & ds.Tables(0).Rows(i).Item("adminid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bindAdminUserDiv(ByVal loginAdmin As String, ByVal spanAdmin As String, ByVal searchKey As String)
        Dim i As Integer
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_UserDivItems", con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@LoginAdmin", SqlDbType.NVarChar, 50)
        cmd.Parameters("@LoginAdmin").Value = loginAdmin

        cmd.Parameters.Add("@SpanAdmin", SqlDbType.NVarChar, 50)
        cmd.Parameters("@SpanAdmin").Value = spanAdmin

        cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 50)
        cmd.Parameters("@Key").Value = searchKey
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        Dim str As String = ""

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("userid") + "#" + ds.Tables(0).Rows(i).Item("userid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("userid") & "#" & ds.Tables(0).Rows(i).Item("userid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function bindAdminUserDivForSuperadmin(ByVal spanAdmin As String, ByVal searchKey As String)
        Dim i As Integer
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_UserDivItemsForSuperAdmin", con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@SpanAdmin", SqlDbType.NVarChar, 50)
        cmd.Parameters("@SpanAdmin").Value = spanAdmin

        cmd.Parameters.Add("@Key", SqlDbType.NVarChar, 50)
        cmd.Parameters("@Key").Value = searchKey
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        Dim str As String = ""

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("userid") + "#" + ds.Tables(0).Rows(i).Item("userid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("userid") & "#" & ds.Tables(0).Rows(i).Item("userid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function BindReportForAdmin(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_trackReportForAdmin", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid

        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid

        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clientid

        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid

        da.SelectCommand = cmd
        con.Open()
        da.Fill(ds)
        con.Close()
        Return ds

    End Function
    <Ajax.AjaxMethod()> _
    Public Function checkDateFormat(ByVal dateValue As String)
        Dim checkedDate As Date
        Try
            checkedDate = CType(dateValue, Date)
            Return 1
        Catch ex As Exception
            Return 0
        End Try


    End Function
    <Ajax.AjaxMethod()> _
    Function divide(ByVal val1 As String, ByVal val2 As String)
        Dim VolumeForTheMonth As Double = CType(val1, Double)

        Dim CphTarget As Double = CType(val2, Double)
        Dim txt As Double = VolumeForTheMonth / CphTarget
        Return txt
    End Function

#Region "Added on 12-08 for Track"
    'Aug 08
    <Ajax.AjaxMethod()> _
    Public Function bindDivAdminForSupAdmin(ByVal searchKey As String)
        Dim i As Integer
        Dim cmdStr As String
        cmdStr = "Select distinct adminid, adminname from masteradmin where adminid like '" + searchKey + "%'"
        ds.Clear()
        da = New SqlDataAdapter(cmdStr, con)
        da.Fill(ds)
        Dim str As String = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("adminName") + "#" + ds.Tables(0).Rows(i).Item("adminid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("adminName") & "#" & ds.Tables(0).Rows(i).Item("adminid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    'Aug 08
    <Ajax.AjaxMethod()> _
    Public Function bindDivUserForSupAdmin(ByVal searchKey As String)
        Dim i As Integer
        Dim cmdStr As String

        cmdStr = "Select Distinct username,userid from buddy where userid like '" + searchKey + "%'"

        ds.Clear()
        da = New SqlDataAdapter(cmdStr, con)
        da.Fill(ds)
        Dim str As String = ""

        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("username") + "#" + ds.Tables(0).Rows(i).Item("userid")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("username") & "#" & ds.Tables(0).Rows(i).Item("userid")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function


    'aug 08 
    <Ajax.AjaxMethod()> _
    Public Function bind_AllDivUser(ByVal search As String, ByVal userid As String)
        Dim i As Integer
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_AllDivUser", con)
        cmd.CommandType = CommandType.StoredProcedure

        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid

        cmd.Parameters.Add("@Search", SqlDbType.NVarChar, 200)
        cmd.Parameters("@Search").Value = search
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        Dim str As String = ""
        For i = 0 To ds.Tables(0).Rows.Count - 1
            If str = "" Then
                str = ds.Tables(0).Rows(i).Item("userName") + "#" + ds.Tables(0).Rows(i).Item("userId")
            Else
                str = str + "$" + ds.Tables(0).Rows(i).Item("userName") & "#" & ds.Tables(0).Rows(i).Item("userId")
            End If
        Next
        If str = "" Then
            str = "No Match"
        End If
        ds.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function BindReportForAnalysis(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        da = New SqlDataAdapter
        cmd = New SqlCommand("sp_trackGraphReportForAdmin", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid

        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid

        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clientid

        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid

        da.SelectCommand = cmd
        con.Open()
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
#End Region

End Class
