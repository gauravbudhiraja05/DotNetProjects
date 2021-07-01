
Imports Microsoft.VisualBasic
Imports System.data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.Configuration

Public Class QueryBuilder
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim con1 As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    <Ajax.AjaxMethod()> _
    Public Function GetSpanForSavedQuery(ByVal tablename As String)
        da = New SqlDataAdapter("select (select departmentname from idmsdepartment where autoid=departmentid) as departmentname,(select clientname from idmsclient where autoid=clientid) as clientname ,(select lobname from warslobmaster where autoid=lobid) as Lobname from warslobtablemaster where tablename='" + tablename + "'", con)
        ds.Clear()
        da.Fill(ds)
        Dim stringreturn As String = "Table Location Is"
        If ds.Tables(0).Rows.Count > 0 Then
            Dim st As Integer = ds.Tables(0).Columns.Count
            Dim ct As Integer = 0
            For ct = 0 To st - 1
                If stringreturn = "" Then
                    If IsDBNull(ds.Tables(0).Rows(0)(ct)) Then
                    Else
                        stringreturn = ds.Tables(0).Rows(0)(ct).ToString
                    End If
                Else
                    If IsDBNull(ds.Tables(0).Rows(0)(ct)) Then
                    Else
                        stringreturn = stringreturn + ">>" + ds.Tables(0).Rows(0)(ct).ToString
                    End If
                End If


            Next



        End If

        Return stringreturn

    End Function
    <Ajax.AjaxMethod()> _
    Public Function tableForadmin(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        If clientid = "" Then
            clientid = "0"
        End If
        If lobid = "" Then
            lobid = "0"
        End If
        cmd = New SqlCommand("sp_tableForadminquery", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Dim tables As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            Dim ends As Integer = ds.Tables(0).Rows.Count
            Dim i As Integer = 0
            For i = 0 To ends - 1
                If tables = "" Then
                    tables = ds.Tables(0).Rows(i)("tablename")
                Else
                    tables = tables + "," + ds.Tables(0).Rows(i)("tablename")
                End If
            Next

        End If
        Return tables
    End Function
    ''' <summary>
    ''' Fill dataset with Table in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    ''' 
    <Ajax.AjaxMethod()> _
    Public Function tableForlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        If clientid = "" Then
            clientid = "0"
        End If
        If lobid = "" Then
            lobid = "0"
        End If
        ds.Clear()
        cmd = New SqlCommand("sp_tableForlocalquery", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        cmd.Dispose()
        Dim tables As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            Dim ends As Integer = ds.Tables(0).Rows.Count
            Dim i As Integer = 0
            For i = 0 To ends - 1
                If tables = "" Then
                    tables = ds.Tables(0).Rows(i)("tablename")
                Else
                    tables = tables + "," + ds.Tables(0).Rows(i)("tablename")
                End If
            Next

        End If
        Return tables
    End Function
    ''' <summary>
    ''' Fill dataset with table in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    <Ajax.AjaxMethod()> _
    Public Function tableFornonlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        If (clientid = "") Then
            clientid = 0
        End If
        If (lobid = "") Then
            lobid = 0
        End If
        ds.Clear()
        cmd = New SqlCommand("sp_tableFornonlocalquery", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@lobid").Value = lobid
        'cmd.Parameters.Add("@tab", SqlDbType.NVarChar, 200)
        'cmd.Parameters("@tab").Value = tab()
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        cmd.Dispose()
        Dim tables As String = ""
        If ds.Tables(0).Rows.Count > 0 Then
            Dim ends As Integer = ds.Tables(0).Rows.Count
            Dim i As Integer = 0
            For i = 0 To ends - 1
                If tables = "" Then
                    tables = ds.Tables(0).Rows(i)("tablename")
                Else
                    tables = tables + "," + ds.Tables(0).Rows(i)("tablename")
                End If
            Next
        End If
        Return tables
    End Function
    <Ajax.AjaxMethod()> _
    Public Function chkUserscope(ByVal userid As String, ByVal deptid As Integer, ByVal clientid As Integer, ByVal lobid As Integer) As String
        If clientid.Equals("--Select--") Then
            clientid = 0
        End If
        If lobid.Equals("--Select--") Then
            lobid = 0
        End If
        con.Close()
        cmd = New SqlCommand("sp_chkUSerscope", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        cmd.Parameters("@userid").Value = userid
        'cmd.Parameters.Add("@deptid", SqlDbType.Int, 9)
        'cmd.Parameters("@deptid").Value = deptid
        'cmd.Parameters.Add("@clientid", SqlDbType.Int, 9)
        'cmd.Parameters("@clientid").Value = clientid
        'cmd.Parameters.Add("@lobid", SqlDbType.Int, 9)
        'cmd.Parameters("@lobid").Value = lobid
        con.Open()
        Dim str = Convert.ToString(cmd.ExecuteScalar())
        con.Close()
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' Function to Bind column Name
    ''' </summary>
    ''' <param name="colname"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    <Ajax.AjaxMethod()> _
    Public Function GetTableFields(ByVal tab As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        Dim qStr As String = "Select * from " + tab + ""
        Try


            'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
            Dim objcmd As New SqlCommand
            objcmd = New SqlCommand(qStr, con)
            con.Open()
            Dim thisReader As SqlDataReader = objcmd.ExecuteReader()
            Dim j As Integer = 0
            For j = 0 To thisReader.FieldCount - 1
                If str = "" Then
                    str = thisReader.GetName(j)
                Else
                    str = str + "$" + thisReader.GetName(j)
                End If
            Next
        Catch ex As Exception
            str = "Null"
        End Try
        Return str
    End Function
    <Ajax.AjaxMethod()> _
      Public Function Bind_Table(ByVal dept1 As String, ByVal client1 As String, ByVal lob1 As String)
        Dim bool As Boolean
        Dim strQryMod As String
        Dim client As String
        Dim lob As String
        If client1 = "--Select--" Or client1 = "" Then
            client = 0
        Else
            client = client1

        End If
        If lob1 = "--Select--" Or lob1 = "" Then
            lob = 0
        Else
            lob = lob1
        End If


        'If Trim(dept) <> "" And Trim(dept) <> "--Select--" And (Trim(client) = "" Or Trim(client) = "--Select--") And (Trim(lob) = "" Or Trim(lob) = "--Select--") Then
        'If Session("usertype") = "member" Then
        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & dept1 & "' and ClientId='" & client & "' and LOBId='" & lob & "' union select viewname from idmsviewmaster where  deptid='" & dept1 & "' and clientid='" & client & "' and lobid='" & lob & "' order by tablename "

        ' Else
        'strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & dept & "' and ClientId=0 and LOBId=0"  'table in database
        'End If

        'bool = True
        'ElseIf Trim(dept) <> "" And Trim(dept) <> "--Select--" And (Trim(Request("Clientname")) <> "" And Trim(Request("Clientname")) <> "--Select--") And (Trim(Request("cboLOB")) = "" Or Trim(Request("cboLOB")) = "--Select--") Then
        'If Session("usertype") = "member" Then
        'strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')"  'table in database
        'Else
        'strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId=0"  'table in database
        'End If

        'bool = True
        'ElseIf Trim(Request("DepartmentName")) <> "" And Trim(Request("DepartmentName")) <> "--Select--" And (Trim(Request("Clientname")) <> "" And Trim(Request("Clientname")) <> "--Select--") And (Trim(Request("cboLOB")) <> "" And Trim(Request("cboLOB")) <> "--Select--") Then
        'If Session("usertype") = "member" Then
        'strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId='" & Request("cboLOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')"  'table in database
        'Else
        'strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId='" & Request("cboLOB") & "'"  'table in database
        'End If

        ' bool = True
        ' End If
        Dim rdrPrgms As SqlDataReader
        Dim i As Integer = 0
        Dim str1 As String
        Dim datafieldstring As String = ""
        Dim tablename As String
        'If bool = True Then
        Dim cmdMod As New SqlCommand(strQryMod, con)
        con.Open()
        dr = cmdMod.ExecuteReader
        ' Dim strQryPrgms As String
        While dr.Read
            tablename = dr("tablename")
            If datafieldstring = "" Then
                datafieldstring = tablename
            Else
                datafieldstring = datafieldstring & "," & tablename
            End If
            'datafieldstring = datafieldstring & "<tr><td colspan=2><INPUT Type=button  value='" & tablename & "' class=button style='width:150px'></td></tr>"
            'If Trim(tablename) = Trim(Request("cbodatatable")) Then

            'strQryPrgms = "select visiblecolumn as str from warslobtablemaster where tablename = '" & Trim(tablename) & "'"
            'Dim cmdPrgms As New SqlCommand(strQryPrgms, con1)
            'con1.Open()
            'rdrPrgms = cmdPrgms.ExecuteReader
            'While rdrPrgms.Read
            '    str1 = rdrPrgms("str")
            '    Dim tokens As String() = Split(str1, ",")
            '    For i = 0 To tokens.Length - 1
            ' datafieldstring = datafieldstring & "<tr><td><A href='#'>" & Trim(tokens(i)) & "</a></td></tr>"
            '    Next
            'End While
            'con1.Close()

            'End If
        End While
        Return datafieldstring
    End Function

    Public Function getUserWithRights(ByVal recordId As String) As DataSet
        ds.Clear()
        cmd = New SqlCommand("sp_getUserWithRights", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@recordid", SqlDbType.Int)
        cmd.Parameters("@recordid").Value = recordId
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    Public Function queryForAdmin() As DataSet

        cmd = New SqlCommand("select recordId, QueryName from warsquerymaster", con)
        da.SelectCommand = cmd
        da.Fill(ds)
        cmd.Dispose()
        Return ds
    End Function
    Public Function queryForUser(ByVal userId As String) As DataSet

        cmd = New SqlCommand("select recordId, QueryName from warsquerymaster where savedBy='" + userId + "'", con)
        da.SelectCommand = cmd
        da.Fill(ds)
        cmd.Dispose()
        Return ds
    End Function
    Public Function trackQ_BuilderForMaster(ByVal ActionBy As String, ByVal Action As String, ByVal Entity As String, ByVal QueryName As String, ByVal Table_Name As String) As Integer
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("Sp_QueryBuilder", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 100).Value = ActionBy
            cmd.Parameters.Add("@Action", SqlDbType.VarChar, 50).Value = Action
            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar, 50).Value = System.DateTime.Now()
            cmd.Parameters.Add("@Query_Name", SqlDbType.VarChar, 50).Value = QueryName
            cmd.Parameters.Add("@Table_Name", SqlDbType.VarChar, 50).Value = Table_Name
            con.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            con.Close()
            Return 1
        Catch ex As Exception
            str = ex.Message
        End Try
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function GetDatetypeColumns(ByVal tablename As String) As String
        con.Open()
        Dim arrColumn As String = ""
        Dim da As New SqlDataAdapter("select * from " + tablename + " where 1<>1", con)
        'da.SelectCommand = cmdgetcolumn
        da.Fill(ds)
        Dim column As DataColumn
        For Each column In ds.Tables(0).Columns
            If column.DataType.ToString() = "System.DateTime" Then
                If arrColumn = "" Then
                    arrColumn = column.ColumnName
                Else
                    arrColumn = arrColumn + "," + column.ColumnName
                End If

            End If
        Next
        con.Close()
        'struct.datecolumn = arrColumn
        Return arrColumn
    End Function

End Class
