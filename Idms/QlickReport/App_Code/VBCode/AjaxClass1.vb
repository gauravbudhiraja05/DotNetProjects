Imports ajax
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class AjaxClass1
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    <Ajax.AjaxMethod()> Public Function Check_span_Local(ByVal typeofuser As String, ByVal userid As String, ByVal dept As String, ByVal client As String, ByVal lob As String)
        If typeofuser = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                .AddWithValue("@Deptid", dept)
                .AddWithValue("@Clientid", client)
                .AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader

            If readerdata.HasRows Then
                Return "YES"
            Else
                Return "NO"

            End If


            readerdata.Close()
            connection.Close()
        ElseIf typeofuser = "User" Then
            Dim repobj As New ReportDesigner
            Dim SCOPE As String = repobj.chkUserscope(userid)
            If SCOPE = "Local" Then
                Return "YES"
            Else
                Return "NO"
            End If
        End If
        Return "0"
    End Function
    ' bindhtml
    <Ajax.AjaxMethod()> Public Function bindhtml(ByVal filename) As String
        'Dim cmdchk As New SqlCommand("select * from IDMSSavedHTMLFile where SavedFilename='" & txtname.Text & "'", connection)
        Dim cmdchk As New SqlCommand("select * from IDMSSavedHTMLFile where SavedFilename='" & filename & "'", connection)
        Dim drchk As SqlDataReader
        Dim bool As Boolean
        If connection.State = ConnectionState.Open Then
            connection.Close()
        End If
        connection.Open()
        drchk = cmdchk.ExecuteReader
        If drchk.Read Then
            bool = True
        End If
        drchk.Close()
        connection.Close()
        cmdchk.Dispose()
        Dim returnvalue As String
        'returnvalue = 2
        If bool = True Then
            returnvalue = 2
        Else
            returnvalue = 1
        End If
        Return returnvalue
    End Function
    <Ajax.AjaxMethod()> Public Function bindclient(ByVal deptid As String) As String
        Dim cmdgetclient As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & deptid & "' order by ClientName", connection)
        Dim dsgetclient As New DataSet
        Dim adpgetclient As New SqlDataAdapter
        adpgetclient.SelectCommand = cmdgetclient
        connection.Open()
        adpgetclient.Fill(dsgetclient)
        connection.Close()
        cmdgetclient.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgetclient.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgetclient.Tables(0).Rows(i).Item("autoid") & "#" & dsgetclient.Tables(0).Rows(i).Item("ClientName")
            Else
                str = str & "$" & dsgetclient.Tables(0).Rows(i).Item("autoid") & "#" & dsgetclient.Tables(0).Rows(i).Item("ClientName")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindlob(ByVal deptid As String, ByVal clientid As String) As String
        Dim cmdgetlob As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & deptid & "' and ClientId='" & clientid & "' order by LOB", connection)
        Dim dsgetlob As New DataSet
        Dim adpgetlob As New SqlDataAdapter
        adpgetlob.SelectCommand = cmdgetlob
        connection.Open()
        adpgetlob.Fill(dsgetlob)
        connection.Close()
        cmdgetlob.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgetlob.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgetlob.Tables(0).Rows(i).Item("autoid") & "#" & dsgetlob.Tables(0).Rows(i).Item("LOB")
            Else
                str = str & "$" & dsgetlob.Tables(0).Rows(i).Item("autoid") & "#" & dsgetlob.Tables(0).Rows(i).Item("LOB")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindlobBLT(ByVal deptid, ByVal clientid) As String
        Dim cmdgetlob As New SqlCommand("select LTrim(RTrim(LOB)) as LOB,autoid from warslobmaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' order by LOB", connection)
        Dim dsgetlob As New DataSet
        Dim adpgetlob As New SqlDataAdapter
        adpgetlob.SelectCommand = cmdgetlob
        connection.Open()
        adpgetlob.Fill(dsgetlob)
        connection.Close()
        cmdgetlob.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgetlob.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgetlob.Tables(0).Rows(i).Item("autoid") & "#" & dsgetlob.Tables(0).Rows(i).Item("LOB")
            Else
                str = str & "$" & dsgetlob.Tables(0).Rows(i).Item("autoid") & "#" & dsgetlob.Tables(0).Rows(i).Item("LOB")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindtabs(ByVal lobid As String) As String
        Dim cmdgettab As New SqlCommand
        If Session("usertype") = "member" Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,LTrim(RTrim(Visiblecolumn)) as Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "'  and Editable='Yes' and Importable='Yes' order by TableName", connection)
        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgettab.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            Else
                str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindtable() As String
        Dim cmdgettab As New SqlCommand

        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where createdby='" + Session("userid") + "' and Editable='Yes' and Importable='Yes' order by TableName", connection)

        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgettab.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            Else
                str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindepttab(ByVal deptid As String) As String
        Dim cmdgettab As New SqlCommand
        If Session("usertype") = "member" Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)
        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgettab.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            Else
                str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindclienttab(ByVal deptid As String, ByVal clientid As String) As String
        Dim cmdgettab As New SqlCommand
        If Session("usertype") = "member" Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)
        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        Dim i As Integer
        For i = 0 To dsgettab.Tables(0).Rows.Count - 1
            If str = "" Then
                str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            Else
                str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
            End If
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function ChkRepName(ByVal name As String) As String
        Dim cmdgettab As New SqlCommand("select * from idmswfmreport where RepName='" & name & "'", connection)
        Dim str As String = ""
        Dim drgettab As SqlDataReader
        connection.Open()
        drgettab = cmdgettab.ExecuteReader
        If drgettab.Read Then
            str = "Y"
        End If
        drgettab.Close()
        connection.Close()
        cmdgettab.Dispose()
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function

    <Ajax.AjaxMethod()> Public Function ChkType(ByVal colname As String, ByVal func As String) As String
        Dim tabname = colname.Split(".")
        Dim cmdgettab As New SqlCommand("select c.name from sysobjects a, syscolumns b, systypes c where a.id=b.id and b.xtype=c.xtype and a.name='" & tabname(0) & "' and b.name='" & tabname(1) & "'", connection)
        Dim str As String = ""
        Dim drgettab As SqlDataReader
        connection.Open()
        drgettab = cmdgettab.ExecuteReader
        If drgettab.Read Then
            str = drgettab("name").ToString
        End If
        drgettab.Close()
        connection.Close()
        cmdgettab.Dispose()
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function ChkName(ByVal strname, ByVal deptid, ByVal clientid, ByVal lobid) As String
        Dim cmdgettab As New SqlCommand("select * from idmsquerymaster where queryName= '" & strname & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and UnderLOB='" & lobid & "'", connection)
        Dim drgettab As SqlDataReader
        Dim str As String = ""
        connection.Open()
        drgettab = cmdgettab.ExecuteReader
        If drgettab.Read Then
            str = "Y"
        End If
        drgettab.Close()
        connection.Close()
        cmdgettab.Dispose()
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function FillQuery(ByVal strname, ByVal deptid, ByVal clientid, ByVal lobid, ByVal index) As String
        Dim cmdget As New SqlCommand("select *,convert(varchar,CreateDate,103) as CreateDate1 from idmsquerymaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and UnderLOB='" & lobid & "' and QueryName='" & strname & "' and ',' + sharedwith + ',' like '%," & Session("username1") & ",%' ", connection)
        Dim drget As SqlDataReader
        Dim str(10) As String
        connection.Open()
        drget = cmdget.ExecuteReader
        If drget.Read Then
            str(0) = drget("txtFormula").ToString
            str(1) = drget("OrderBy").ToString
            str(2) = drget("wheredata").ToString
            str(3) = drget("GroupBy").ToString
            str(4) = drget("Header").ToString
            str(5) = drget("Footer").ToString
            str(6) = drget("colName").ToString
            str(7) = drget("tableName").ToString
            str(8) = drget("HeadingName").ToString
        End If
        drget.Close()
        connection.Close()
        cmdget.Dispose()
        If str(0) = "" Then
            str(0) = "N"
        End If
        If str(1) = "" Then
            str(1) = "N"
        End If
        If str(2) = "" Then
            str(2) = "N"
        End If
        If str(3) = "" Then
            str(3) = "N"
        End If
        If str(4) = "" Then
            str(4) = "N"
        End If
        If str(5) = "" Then
            str(5) = "N"
        End If
        If str(6) = "" Then
            str(6) = "N"
        End If
        If str(7) = "" Then
            str(7) = "N"
        End If
        If str(8) = "" Then
            str(8) = "N"
        End If
        Return str(index)
    End Function
    <Ajax.AjaxMethod()> Public Function ChkDate(ByVal tabname) As String
        Dim str As String = ""
        Dim cmdchk As New SqlCommand("select b.* from sysobjects a, syscolumns b where a.id=b.id and a.xtype='U' and a.name='" & tabname & "' and b.name='Date'", connection)
        Dim drchk As SqlDataReader
        connection.Open()
        drchk = cmdchk.ExecuteReader
        If drchk.Read Then
            str = "Y"
        Else
            str = "N"
        End If
        drchk.Close()
        connection.Close()
        cmdchk.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindlinks(ByVal mainhead) As String
        Dim arrhead = mainhead.Split(",")
        Dim j As Integer
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        For j = 0 To arrhead.Length - 1
            Dim arr = Split(arrhead(j), "-")
            Dim cmdget As New SqlCommand("select MenuId,MenuDescription from nlvl_menu where ParentId='" & arr(0) & "'", connection)
            Dim dsget As New DataSet
            Dim adpget As New SqlDataAdapter
            adpget.SelectCommand = cmdget
            connection.Open()
            adpget.Fill(dsget)
            connection.Close()
            cmdget.Dispose()
            Dim i As Integer
            For i = 0 To dsget.Tables(0).Rows.Count - 1
                If str = "" Then
                    str = dsget.Tables(0).Rows(i).Item("MenuId") & "-" & arr(0) & "#" & dsget.Tables(0).Rows(i).Item("MenuDescription") & "-" & arr(1)
                Else
                    str = str & "$" & dsget.Tables(0).Rows(i).Item("MenuId") & "-" & arr(0) & "#" & dsget.Tables(0).Rows(i).Item("MenuDescription") & "-" & arr(1)
                End If
            Next
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindsublinks(ByVal mainhead) As String
        Dim arrhead = mainhead.Split(",")
        Dim j As Integer
        Dim str As String
        Dim strname As String
        Dim strvalue As String
        For j = 0 To arrhead.Length - 1
            Dim arr = Split(arrhead(j), "-")
            Dim cmdget As New SqlCommand("select MenuId,MenuDescription from nlvl_menu where ParentId='" & arr(0) & "'", connection)
            Dim dsget As New DataSet
            Dim adpget As New SqlDataAdapter
            adpget.SelectCommand = cmdget
            connection.Open()
            adpget.Fill(dsget)
            connection.Close()
            cmdget.Dispose()
            Dim i As Integer
            For i = 0 To dsget.Tables(0).Rows.Count - 1
                If str = "" Then
                    str = dsget.Tables(0).Rows(i).Item("MenuId") & "-" & arr(0) & "#" & dsget.Tables(0).Rows(i).Item("MenuDescription") & "-" & arr(1)
                Else
                    str = str & "$" & dsget.Tables(0).Rows(i).Item("MenuId") & "-" & arr(0) & "#" & dsget.Tables(0).Rows(i).Item("MenuDescription") & "-" & arr(1)
                End If
            Next
        Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindclienttabview(ByVal deptid As String, ByVal clientid As String, ByVal usertype As String, ByVal userid As String) As String
        Dim cmdgettab As New SqlCommand
        Dim cmdtype As New SqlCommand
        Dim str As String
        Dim TypeOfUser As String
        If usertype = "User" Then

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & userid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0", connection)
            Dim a As Object
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception
                connection.Close()
                cmdtype.Dispose()
                '  Return str = "N"
            End Try
            If (TypeOfUser = "Local") Then
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') order by TableName", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true') or LocalView='Local') order by TableName", connection)

            Else
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "'  or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by TableName ", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true')) union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true') or LocalView='NonLocal') order by TableName", connection)

            End If
            connection.Close()
            cmdtype.Dispose()
        Else
            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' or (createdby='" & userid & "') and tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True')order by tablename", connection)
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                '.AddWithValue("@Deptid", deptid)
                '.AddWithValue("@Clientid", clientid)
                '.AddWithValue("@LOBID", "0")
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" + clientid + "'", connection)

                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" + clientid + "' union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid=0 and DeptId='" & deptid & "'  and clientid='" + clientid + "'", connection)

            Else
                ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "'", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "' union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid=0 and DeptId='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "'", connection)

            End If
            readerdata.Close()
        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab

        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim i As Integer

        Try
            For i = 0 To dsgettab.Tables(0).Rows.Count - 1
                If str = "" Then
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then

                        str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If

                Else
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If
                End If
            Next
        Catch ex As Exception
            Dim s = ex.ToString
        End Try
        'For i = 0 To dsgettab.Tables(0).Rows.Count - 1
        '    If str = "" Then
        '        str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
        '    Else
        '        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
        '    End If
        'Next
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindepttabview(ByVal deptid As String, ByVal usertype As String, ByVal userid As String) As String
        Dim cmdgettab As New SqlCommand
        Dim cmdtype As New SqlCommand
        Dim str As String
        Dim TypeUser As String = ""
        If usertype = "User" Then
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & userid & "' and DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0", connection)

            Dim a As Object
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeUser = a.ToString()
            Catch ex As Exception
                connection.Close()
                cmdtype.Dispose()
                '  Return str = "N"
            End Try

            'If (TypeUser = "Local") Then
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') order by TableName", connection)

            'Else
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "'  or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by TableName ", connection)

            'End If ''commented on 02Nov by atul
            ' Query Changed by atul to get view & tablenames
            If (TypeUser = "Local") Then
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true') or LocalView='Local') order by TableName", connection)

            Else
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "'  or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  union select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true')) order by TableName", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true')) union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId=0 and LOBId=0 and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true')) order by TableName", connection)

            End If
            connection.Close()
            cmdtype.Dispose()

        Else
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                '.AddWithValue("@Deptid", deptid)
                '.AddWithValue("@Clientid", "0")
                '.AddWithValue("@LOBID", "0")
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            'If readerdata.HasRows Then
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0", connection)
            'Else
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0 and createdby='" + userid + "'", connection)

            'End If 'commented on 02Nov by atul
            If readerdata.HasRows Then
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0 union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid=0 and DeptId='" & deptid & "'  and clientid=0", connection)
            Else
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0 and createdby='" + userid + "' union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid=0 and DeptId='" & deptid & "'  and clientid=0 and createdby='" + userid + "'", connection)

            End If
            readerdata.Close()

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0 and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid=0 and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid=0 and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid=0 and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid=0 or (createdby='" & userid & "') and tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True')order by tablename", connection)

        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab


        Try

       
            adpgettab.Fill(dsgettab)
            cmdgettab.Dispose()
            Dim i As Integer
            For i = 0 To dsgettab.Tables(0).Rows.Count - 1
                If str = "" Then
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then

                        str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If

                Else
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If
                End If
            Next
        Catch ex As Exception
            Dim s = ex.ToString
        End Try
        If str = "" Then
            str = "N"
        End If
        connection.Close()
        Return str
    End Function

    <Ajax.AjaxMethod()> Public Function bindtableview(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal usertype As String, ByVal userid As String) As String
        Dim cmdgettab As New SqlCommand
        Dim cmdtype As New SqlCommand
        Dim str As String
        Dim TypeOfUser As String
        If usertype = "User" Then
            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and tableid in (select tableid from idms_tablerights where userid='" & userid & "' and [view]='true') order by TableName", connection)
            cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & userid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "'", connection)
            Dim a As Object
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception
                connection.Close()
                cmdtype.Dispose()
                ' Return str = "N"
            End Try
            If (TypeOfUser = "Local") Then
                '  cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') order by TableName", connection)


                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') or localtable='Local') union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true') or LocalView='Local') order by TableName", connection)

            Else
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "'  or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by TableName ", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "' or tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') ) union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn, Headings from idmsviewmaster where  DeptId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "' and (createdby='" & userid & "' or ViewId in (select ViewId from idms_viewrights where UserID='" & userid & "' and [view]='true') ) order by TableName", connection)

            End If
            connection.Close()
            cmdtype.Dispose()
        Else
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' order by TableName", connection)
            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid='" & lobid & "' and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid='" & lobid & "' and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid='" & lobid & "' and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid='" & lobid & "' and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=" & lobid & " and departmentid='" & deptid & "'  and clientid='" & clientid & "' or (createdby='" & userid & "') and tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True')order by tablename", connection)
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=" & lobid & " and departmentid='" & deptid & "'  and clientid='" + clientid + "'", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid='" & lobid & "' and departmentid='" & deptid & "'  and clientid='" + clientid + "' union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid='" & lobid & "' and DeptId='" & deptid & "'  and clientid='" + clientid + "'", connection)

            Else
                'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid='" & lobid & "' and departmentid='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "'", connection)
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn,'#@DEWQA45tec' as Headings from warslobtablemaster where lobid='" & lobid & "' and departmentid='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "' union All select LTrim(RTrim(ViewName)) as TableName,Colname as Visiblecolumn,Headings from idmsviewmaster where lobid='" & lobid & "' and DeptId='" & deptid & "'  and clientid='" + clientid + "' and createdby='" + userid + "'", connection)

            End If
            readerdata.Close()
        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab

        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Dim i As Integer
        'For i = 0 To dsgettab.Tables(0).Rows.Count - 1
        '    If str = "" Then
        '        str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
        '    Else
        '        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
        '    End If
        'Next
        Try
            For i = 0 To dsgettab.Tables(0).Rows.Count - 1
                If str = "" Then
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then

                        str = dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If

                Else
                    If (dsgettab.Tables(0).Rows(i).Item("Headings").ToString() = "#@DEWQA45tec") Then
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Visiblecolumn") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    Else
                        str = str & "$" & dsgettab.Tables(0).Rows(i).Item("Headings") & "#" & dsgettab.Tables(0).Rows(i).Item("TableName")
                    End If
                End If
            Next
        Catch ex As Exception
            Dim s = ex.ToString
        End Try
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function

    <Ajax.AjaxMethod()> Public Function chkstructname(ByVal strname, ByVal deptid, ByVal clientid, ByVal lobid) As String
        Dim cmdchk As New SqlCommand("select * from idmsupdatetabstruct where StructName='" & strname & "' and DepartmentId='" & deptid & "' and clientid='" & clientid & "' and LobId='" & lobid & "'", connection)
        Dim drchk As SqlDataReader
        Dim str As String
        connection.Open()
        drchk = cmdchk.ExecuteReader
        If drchk.Read Then
            str = "Y"
        Else
            str = "N"
        End If
        drchk.Close()
        connection.Close()
        cmdchk.Dispose()
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function chkViewName(ByVal strname As String) As String
        Dim cmdchk As New SqlCommand("select name from sysobjects where xtype='V' and name='" & strname & "'", connection)
        Dim drchk As SqlDataReader
        Dim str As String
        connection.Open()
        drchk = cmdchk.ExecuteReader
        If drchk.Read Then
            str = "Y"
        Else
            str = "N"
        End If
        drchk.Close()
        connection.Close()
        cmdchk.Dispose()
        Return str
    End Function
    Dim conchk As New SqlConnection(constr)
    Public Function chkexists(ByVal strid As Integer, ByVal struser As String) As Boolean
        Dim cmdchk As New SqlCommand("select * from nlvl_menu_rights where MenuId='" & strid & "' and userid='" & struser & "'", conchk)
        Dim boolchk As Boolean
        Dim drchk As SqlDataReader
        conchk.Open()
        drchk = cmdchk.ExecuteReader
        While drchk.Read
            boolchk = True
        End While
        drchk.Close()
        conchk.Close()
        cmdchk.Dispose()
        If boolchk = True Then
            Return False
        Else
            Return True
        End If
    End Function
    <Ajax.AjaxMethod()> Public Function SaveRights(ByVal struser As String, ByVal strmainhead As String, ByVal strsubhead1 As String, ByVal strsubhead2 As String, ByVal strsubhead3 As String) As String
        Dim cnt As Integer = 0
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim l As Integer = 0
        Dim msg As String = ""
        Dim arruser = struser
        Try
            Dim mainhead = Trim(strmainhead)
            Dim arrhead = Split(mainhead, ",")
            For i = 0 To arrhead.length - 1
                If chkexists(arrhead(i), arruser) = True Then
                    Dim cmdins1 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                    cmdins1.CommandType = CommandType.StoredProcedure
                    With cmdins1.Parameters
                        .Add("@LOB", "0")
                        .Add("@MenuId", arrhead(i))
                        .Add("@UserType", "")
                        .Add("@Access", "")
                        .Add("@Currdate", System.DateTime.Now)
                        .Add("@AssignBy", Session("userid"))
                        .Add("@parentid", "0")
                        .Add("@userid", arruser)
                    End With
                    connection.Open()
                    cmdins1.ExecuteNonQuery()
                    connection.Close()
                    cmdins1.Dispose()
                End If
                If strsubhead1 <> "" Then
                    Dim arrlink = Split(strsubhead1, ",")
                    For j = 0 To arrlink.length - 1
                        Dim arrlink1 = Split(arrlink(j), "-")
                        If arrlink1(1) = arrhead(i) Then
                            If chkexists(arrlink1(0), arruser) = True Then
                                Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                cmdins2.CommandType = CommandType.StoredProcedure
                                With cmdins2.Parameters
                                    .Add("@LOB", "0")
                                    .Add("@MenuId", arrlink1(0))
                                    .Add("@UserType", "")
                                    .Add("@Access", "")
                                    .Add("@Currdate", System.DateTime.Now)
                                    .Add("@AssignBy", Session("userid"))
                                    .Add("@parentid", arrlink1(1))
                                    .Add("@userid", arruser)
                                End With
                                connection.Open()
                                cmdins2.ExecuteNonQuery()
                                connection.Close()
                                cmdins2.Dispose()
                            End If
                        End If
                        If strsubhead2 <> "" Then
                            Dim arrsublink = Split(strsubhead2, ",")
                            For k = 0 To arrsublink.length - 1
                                Dim arrsublink1 = Split(arrsublink(k), "-")
                                If arrsublink1(1) = arrlink1(0) Then
                                    If chkexists(arrsublink1(0), arruser) = True Then
                                        Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                        cmdins3.CommandType = CommandType.StoredProcedure
                                        With cmdins3.Parameters
                                            .Add("@LOB", "0")
                                            .Add("@MenuId", arrsublink1(0))
                                            .Add("@UserType", "")
                                            .Add("@Access", "")
                                            .Add("@Currdate", System.DateTime.Now)
                                            .Add("@AssignBy", Session("userid"))
                                            .Add("@parentid", arrsublink1(1))
                                            .Add("@userid", arruser)
                                        End With
                                        connection.Open()
                                        cmdins3.ExecuteNonQuery()
                                        connection.Close()
                                        cmdins3.Dispose()
                                    End If
                                End If
                                If strsubhead3 <> "" Then
                                    Dim arrsublink2 = Split(strsubhead3, ",")
                                    For l = 0 To arrsublink2.length - 1
                                        Dim arrsublink3 = Split(arrsublink2(l), "-")
                                        If arrsublink3(1) = arrsublink1(0) Then
                                            If chkexists(arrsublink3(0), arruser) = True Then
                                                Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                With cmdins3.Parameters
                                                    .Add("@LOB", "0")
                                                    .Add("@MenuId", arrsublink3(0))
                                                    .Add("@UserType", "")
                                                    .Add("@Access", "")
                                                    .Add("@Currdate", System.DateTime.Now)
                                                    .Add("@AssignBy", Session("userid"))
                                                    .Add("@parentid", arrsublink3(1))
                                                    .Add("@userid", arruser)
                                                End With
                                                connection.Open()
                                                cmdins3.ExecuteNonQuery()
                                                connection.Close()
                                                cmdins3.Dispose()
                                            End If
                                        End If
                                    Next
                                Else
                                    If arrsublink1(1) = arrlink1(0) Then
                                        Dim cmdget As New SqlCommand("select * from nlvl_menu where Level=3 and ParentId='" & arrsublink1(0) & "'", connection1)
                                        Dim drget As SqlDataReader
                                        connection1.Open()
                                        drget = cmdget.ExecuteReader
                                        While drget.Read
                                            If chkexists(drget("MenuId").ToString, arruser) = True Then
                                                Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                With cmdins3.Parameters
                                                    .Add("@LOB", "0")
                                                    .Add("@MenuId", drget("MenuId").ToString)
                                                    .Add("@UserType", "")
                                                    .Add("@Access", "")
                                                    .Add("@Currdate", System.DateTime.Now)
                                                    .Add("@AssignBy", Session("userid"))
                                                    .Add("@parentid", arrlink1(0))
                                                    .Add("@userid", arruser)
                                                End With
                                                connection.Open()
                                                cmdins3.ExecuteNonQuery()
                                                connection.Close()
                                                cmdins3.Dispose()
                                            End If
                                        End While
                                        drget.Close()
                                        connection1.Close()
                                        cmdget.Dispose()
                                    End If
                                End If
                            Next
                        Else
                            If arrlink1(1) = arrhead(i) Then
                                Dim cmdget As New SqlCommand("select * from nlvl_menu where Level=2 and ParentId='" & arrlink1(0) & "'", connection1)
                                Dim drget As SqlDataReader
                                connection1.Open()
                                drget = cmdget.ExecuteReader
                                While drget.Read
                                    If chkexists(drget("MenuId"), arruser) = True Then
                                        Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                        cmdins3.CommandType = CommandType.StoredProcedure
                                        With cmdins3.Parameters
                                            .Add("@LOB", "0")
                                            .Add("@MenuId", drget("MenuId").ToString)
                                            .Add("@UserType", "")
                                            .Add("@Access", "")
                                            .Add("@Currdate", System.DateTime.Now)
                                            .Add("@AssignBy", Session("userid"))
                                            .Add("@parentid", arrlink1(0))
                                            .Add("@userid", arruser)
                                        End With
                                        connection.Open()
                                        cmdins3.ExecuteNonQuery()
                                        connection.Close()
                                        cmdins3.Dispose()
                                    End If
                                    'If arrsublink(1) = arrlink1(i) Then
                                    Dim cmdget1 As New SqlCommand("select * from nlvl_menu where Level=3 and ParentId='" & drget("MenuId") & "'", connection2)
                                    Dim drget1 As SqlDataReader
                                    connection2.Open()
                                    drget1 = cmdget1.ExecuteReader
                                    While drget1.Read
                                        If chkexists(drget1("MenuId").ToString, arruser) = True Then
                                            Dim cmdins4 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                                            cmdins4.CommandType = CommandType.StoredProcedure
                                            With cmdins4.Parameters
                                                .Add("@LOB", "0")
                                                .Add("@MenuId", drget1("MenuId").ToString)
                                                .Add("@UserType", "")
                                                .Add("@Access", "")
                                                .Add("@Currdate", System.DateTime.Now)
                                                .Add("@AssignBy", Session("userid"))
                                                .Add("@parentid", drget("MenuId").ToString)
                                                .Add("@userid", arruser)
                                            End With
                                            connection.Open()
                                            cmdins4.ExecuteNonQuery()
                                            connection.Close()
                                            cmdins4.Dispose()
                                        End If
                                    End While
                                    ' End If
                                End While
                                drget.Close()
                                connection1.Close()
                                cmdget.Dispose()
                            End If
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
            msg = Replace(ex.Message, "'", " ")
            msg = Replace(msg, vbCrLf, " ")
        End Try
        'bindlist()
        msg = "Rights has been assigned successfully!"
        Return msg
    End Function
    <Ajax.AjaxMethod()> Public Function Delformula(ByVal name As String) As String
        Dim cmdgettab As New SqlCommand("select colname from idmswfmreport where colname='" & name & "'", connection)
        Dim str As String = ""
        Dim drgettab As SqlDataReader
        connection.Open()
        drgettab = cmdgettab.ExecuteReader
        If drgettab.Read Then

            Dim cmdgettab1 As New SqlCommand("delete from idmswfmreport where colname='" & name & "'", connection1)
            Dim str1 As String = ""
            Dim drgettab1 As SqlDataReader
            connection1.Open()
            drgettab1 = cmdgettab1.ExecuteReader
            drgettab1.Close()
            connection1.Close()
            str = "Y"
        End If
        drgettab.Close()
        connection.Close()
        cmdgettab.Dispose()
        If str = "" Then
            str = "N"
        End If
        Return str
    End Function
End Class
