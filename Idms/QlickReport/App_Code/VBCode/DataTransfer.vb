Imports Microsoft.VisualBasic
Imports ajax
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class DataTransfer
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)

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
            'str = "N"
            str = ""
        End If
        Return str
    End Function


    <Ajax.AjaxMethod()> Public Function bindtable(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim cmdgettab As New SqlCommand
        Dim TypeOfUser = ""
        'If hfUserType = "User" Then

        '    ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
        '    Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='" + clientid + "' and LOBId='" + lobid + "'", connection)
        '    Dim a = ""
        '    Try
        '        connection.Open()
        '        a = cmdtype.ExecuteScalar()
        '        TypeOfUser = a.ToString()
        '    Catch ex As Exception
        '        connection.Close()
        '        cmdtype.Dispose()

        '    End Try

        '    If (TypeOfUser = "Local") Then
        '        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" + lobid + "' and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true') or localtable='Local') order by TableName", connection)

        '    Else
        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" + lobid + "' and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true'and importdata='true')) order by TableName", connection)

        '    End If
        '    connection.Close()
        '    cmdtype.Dispose()
        'Else
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 order by TableName", connection)
        '    'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

        'End If
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
            'str = "N"
            str = ""
        End If
        Return str
    End Function


    <Ajax.AjaxMethod()> Public Function bindepttab(ByVal deptid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim TypeOfUser As String
        Dim cmdgettab = New SqlCommand()
        If hfUserType = "User" Then

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='0' and LOBId='0'", connection)
            Dim a = ""
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception

                connection.Close()
                cmdtype.Dispose()
            End Try

            If (TypeOfUser = "Local") Then
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='0' and LOBId=0 and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true') or localtable='Local') order by TableName", connection)

            Else
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='0' and LOBId=0 and (createdby='" & UserId & "'   or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and idms_tablerights.[View]='True'))  order by TableName ", connection)

            End If
            connection.Close()
            cmdtype.Dispose()
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='0' and LOBId=0 order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

        End If


        'If hfUserType = "Admin" Then
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)

        'Else
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and tablename in (select tablename from idms_tablerights where userid='" & UserId & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'End If
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
            'str = "N"
            str = ""
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function bindclienttab(ByVal deptid As String, ByVal clientid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim cmdgettab As New SqlCommand
        Dim TypeOfUser = ""
        If hfUserType = "User" Then

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='" + clientid + "' and LOBId='0'", connection)
            Dim a = ""
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception
                connection.Close()
                cmdtype.Dispose()

            End Try

            If (TypeOfUser = "Local") Then
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and [view]='true') or localtable='Local') order by TableName", connection)

            Else
                cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & UserId & "'  or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and idms_tablerights.[View]='True'))  order by TableName ", connection)

            End If
            connection.Close()
            cmdtype.Dispose()
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

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
            'str = "N"
            str = ""
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function chkstructname(ByVal strname As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String) As String
        Dim cmdchk As New SqlCommand("select * from idmsupdatetabstruct where CmdName='" & strname & "' and DeptId='" & deptid & "' and clientid='" & clientid & "' and LobId='" & lobid & "'", connection)
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
            'str = "N"
            str = ""
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function ReplaceTable_bindepttab(ByVal deptid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim TypeOfUser As String
        Dim cmdgettab = New SqlCommand()
        If hfUserType = "User" Then

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='0' and LOBId='0'", connection)
            Dim a = ""
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception

                connection.Close()
                cmdtype.Dispose()
            End Try
            'Local Concept Cancelled Here by Gopal 
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='0' and LOBId='0' and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true' and importdata='true')) order by TableName", connection)
            'If (TypeOfUser = "Local") Then
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='0' and LOBId=0 and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true' and importdata='true') or localtable='Local') order by TableName", connection)

            'Else
            '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='0' and LOBId=0 and (createdby='" & UserId & "'   or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and idms_tablerights.[View]='True' and idms_tablerights.importdata='true'))  order by TableName ", connection)

            'End If
            connection.Close()
            cmdtype.Dispose()
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='0' and LOBId='0' order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

        End If


        'If hfUserType = "Admin" Then
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)

        'Else
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and tablename in (select tablename from idms_tablerights where userid='" & UserId & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'End If
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
            'str = "N"
            str = ""
        End If
        Return str
    End Function
    <Ajax.AjaxMethod()> Public Function ReplaceTable_bindclienttab(ByVal deptid As String, ByVal clientid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim cmdgettab As New SqlCommand
        Dim TypeOfUser = ""
        If hfUserType = "User" Then

            ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
            Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='" + clientid + "' and LOBId='0'", connection)
            Dim a = ""
            Try
                connection.Open()
                a = cmdtype.ExecuteScalar()
                TypeOfUser = a.ToString()
            Catch ex As Exception
                connection.Close()
                cmdtype.Dispose()

            End Try
            'Local Concept Cancelled Here by Gopal 
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and [view]='true' and importdata='true')) order by TableName", connection)
            ' If (TypeOfUser = "Local") Then
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and [view]='true' and importdata='true')) order by TableName", connection)
            'Else
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and (createdby='" & UserId & "'  or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and idms_tablerights.[View]='True' and idms_tablerights.importdata='true'))  order by TableName ", connection)

            'End If
            connection.Close()
            cmdtype.Dispose()
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 order by TableName", connection)
            'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

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
            'str = "N"
            str = ""
        End If
        Return str
    End Function

    <Ajax.AjaxMethod()> Public Function ReplaceTable_bindtable(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal hfUserType As String, ByVal UserId As String) As String
        Dim cmdgettab As New SqlCommand
        Dim TypeOfUser = ""
        'If hfUserType = "User" Then

        '    ' cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and tableid in (select tableid from idms_tablerights where userid='" & userid & "'and [view]='true') order by TableName", connection)
        '    Dim cmdtype = New SqlCommand("select buddystatus from buddy where userid='" & UserId & "' and DepartmentId='" & deptid & "' and ClientId='" + clientid + "' and LOBId='" + lobid + "'", connection)
        '    Dim a = ""
        '    Try
        '        connection.Open()
        '        a = cmdtype.ExecuteScalar()
        '        TypeOfUser = a.ToString()
        '    Catch ex As Exception
        '        connection.Close()
        '        cmdtype.Dispose()

        '    End Try
        '    'Local Concept Cancelled Here by Gopal 
        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" + lobid + "'  and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true'and importdata='true')) order by TableName", connection)
        '    'If (TypeOfUser = "Local") Then
        '    '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" + lobid + "' and (createdby='" & UserId & "' or tableid in (select tableid from idms_tablerights where userid='" & UserId & "'and [view]='true'and importdata='true') or localtable='Local') order by TableName", connection)

        '    'Else
        '    '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" + lobid + "' and (createdby='" & UserId & "'  or tableid in (select tableid from idms_tablerights where userid='" & UserId & "' and idms_tablerights.[View]='True' and idms_tablerights.importdata='true'))  order by TableName ", connection)

        '    'End If
        connection.Close()
        'cmdtype.Dispose()
        'Else
        'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 order by TableName", connection)
        ''cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where lobid=0 and departmentid='" & deptid & "'  and clientid='" & clientid & "' and (departmentid in (select deptid from masteradmin where deptid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') and clientid in(select clientid from masteradmin where  departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "')  and lobid in (select lobid from masteradmin where departmentid='" & deptid & "' and clientid='" & clientid & "' and lobid=0 and adminid='" & userid & "') or (createdby='" & userid & "') or tableid in (select tableid from idms_tablerights where userid='" & userid & "' and idms_tablerights.[View]='True'))  order by tablename ", connection)

        'End If
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
            'str = "N"
            str = ""
        End If
        Return str
    End Function

End Class
