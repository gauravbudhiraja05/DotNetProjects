
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Partial Class QueryBuilder_SavedQueries
    Inherits System.Web.UI.Page
    Public dr As SqlDataReader
    Dim cmd As New SqlCommand
    Dim con As New SqlConnection(AppSettings("ConnectionString"))
    Dim adp As SqlDataAdapter
    Dim tab As New DataTable
    Dim count, i, j As Integer
    Dim constr As String = AppSettings("connectionstring")
    Dim connection As New SqlConnection(constr)
    Dim connectionsave As New SqlConnection(constr)
    Dim connectionsave1 As New SqlConnection(constr)
    Dim connectionsave2 As New SqlConnection(constr)
    Dim connectionsave4 As New SqlConnection(constr)
    Dim connectionsave3 As New SqlConnection(constr)
    Dim con1 As New SqlConnection(constr)

    Dim objds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            lblUser.Text = Session("UserId")
            'ddldept_bind()
            'ddlQueryName_bind()
            ddlYourQuery_bind()
        End If
        
    End Sub
    'Public Function ddldept_bind()
    '    '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '    Try
    '        Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
    '        Dim da As New SqlDataAdapter
    '        da.SelectCommand = comdepart
    '        Dim ds As New DataSet
    '        connection.Open()
    '        da.Fill(ds)
    '        connection.Close()
    '        cbodept.DataTextField = "DepartmentName"
    '        cbodept.DataValueField = "autoid"
    '        cbodept.DataSource = ds
    '        cbodept.DataBind()
    '        cbodept.Items.Insert("0", "--Select--")
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    '    Return 1
    'End Function
    Public Sub SetFocus(ByVal FocusControl As Control)
        ''''''''''''''this function is for setting the focus''''''''''''''''''''
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Page.GetType(), "setFocus", Script.ToString())
    End Sub

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1
    End Function

    'Protected Sub ddlQueryName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryName.SelectedIndexChanged
    '    Try

    '        If Me.ddlYourQuery.SelectedIndex > 0 Then
    '            ShowConfirm("Please select any one from your query and shared query ")
    '            Me.ddlQueryName.SelectedIndex = 0
    '            Exit Sub
    '        End If
    '        cmd.Connection = con
    '        'cmd.CommandText = "select * from warsquerymaster where queryname like '" & ddlQueryName.SelectedItem.Text & "' and savedby='" & Session("userid") & "'"
    '        'old query
    '        'cmd.CommandText = "select * from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and queryname like '" & ddlQueryName.SelectedItem.Text & "' and ',' + sharedwith + ',' like '%," & Session("userid") & ",%' and ',' + isnull(removesharing,'') + ',' not like '%," & Session("userid") & ",%'"
    '        'cmd.CommandText = "select * from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and queryname like '" & ddlQueryName.SelectedItem.Text & "' and ',' + sharedwith + ',' like '%," & Session("userid") & ",%'"
    '        cmd.CommandText = "select * from warsquerymaster where queryname = '" & ddlQueryName.SelectedItem.Text & "'"
    '        con.Open()
    '        dr = cmd.ExecuteReader

    '        While dr.Read = True
    '            chkPercentage.Value = dr("Percentage").ToString
    '            formulaName.Value = dr("formulaName").ToString
    '            Showdata.Value = dr("Showdata").ToString
    '            formula.Value = dr("formula").ToString
    '            crdata.Value = dr("crdata").ToString
    '            column.Value = dr("colName").ToString
    '            wheredata.Value = dr("wheredata").ToString
    '            wheredata1.Value = dr("wheredata1").ToString
    '            selectedfield.Value = dr("selected_field").ToString
    '            hidtablename.Value = dr("tableName").ToString
    '            Session("mailtain") = dr("status").ToString
    '        End While
    '        con.Close()
    '        cmd.Dispose()
    '        Session("mailtain") = Session("mailtain") + "," + ddlQueryName.SelectedItem.Text
    '        Dim reader As SqlDataReader
    '        Dim strQryMod As String = "select name from syscolumns where object_name(id)='" & hidtablename.Value + "'"
    '        Dim cmdMod As New SqlCommand(strQryMod, con)
    '        con.Open()
    '        reader = cmdMod.ExecuteReader
    '        Dim AllColumn As String = ""
    '        While reader.Read
    '            If AllColumn = "" Then
    '                AllColumn = reader("name")
    '            Else
    '                AllColumn = AllColumn + "," + reader("name")
    '            End If
    '        End While
    '        con.Close()
    '        Dim AllColumnArr As Array = AllColumn.Split(",")

    '        Dim k As Integer = 0
    '        Dim ActualWhere As String = ""
    '        For k = 0 To UBound(AllColumnArr)
    '            Dim chkval As String = AllColumnArr(k).ToString
    '            If wheredata.Value <> "" Then
    '                If wheredata.Value.Contains(chkval) Then
    '                    If ActualWhere = "" Then
    '                        ActualWhere = AllColumnArr(k).ToString
    '                    Else
    '                        ActualWhere = ActualWhere + "," + AllColumnArr(k).ToString
    '                    End If
    '                End If
    '            End If
    '            If wheredata1.Value <> "" Then
    '                If wheredata1.Value.Contains(chkval) Then
    '                    If ActualWhere = "" Then
    '                        ActualWhere = AllColumnArr(k).ToString
    '                    Else
    '                        ActualWhere = ActualWhere + "," + AllColumnArr(k).ToString
    '                    End If
    '                End If
    '            End If



    '        Next
    '        ActualWhereHId.Value = ActualWhere
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    'End Sub
    'Public Function ddlQueryName_bind()
    '    Try
    '        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '        'Session("username")
    '        ddlQueryName.Items.Clear()
    '        Dim dept
    '        Dim client
    '        Dim lob
    '        If cbodept.SelectedIndex <> 0 And cbodept.SelectedIndex <> -1 Then
    '            dept = cbodept.SelectedValue
    '        Else
    '            dept = 0
    '        End If
    '        If cboclient.SelectedIndex <> 0 And cboclient.SelectedIndex <> -1 Then
    '            client = cboclient.SelectedValue
    '        Else
    '            client = 0
    '        End If
    '        If cbolob.SelectedIndex <> 0 And cbolob.SelectedIndex <> -1 Then
    '            lob = cbolob.SelectedValue
    '        Else
    '            lob = 0
    '        End If
    '        Dim sharewith As String = ""
    '        'Dim cmduser As New SqlCommand("select queryName,sharedwith from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and sharedwith is not null  and ',' + isnull(removesharing,'') + ',' not like '%," & Session("userid") & ",%'", connection)
    '        Dim cmduser As New SqlCommand("select queryName,sharedwith from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and sharedwith is not null order by queryName", connection)
    '        Dim rdusern As SqlDataReader
    '        connection.Open()
    '        rdusern = cmduser.ExecuteReader
    '        Do While rdusern.Read
    '            Dim i As Integer = 0
    '            Dim j As Integer
    '            sharewith = rdusern("sharedwith")
    '            Dim str As String()
    '            str = Split(Trim(sharewith), ",")
    '            i = str.Length
    '            For j = 0 To i - 1
    '                'Response.Write(Session("username1"))
    '                'Response.End()
    '                If Trim(str(j)) = Trim(Session("userid")) Then
    '                    ddlQueryName.Items.Add(rdusern("queryName"))
    '                End If
    '            Next
    '        Loop
    '        ddlQueryName.Items.Insert(0, "--select--")
    '        connection.Close()
    '        rdusern.Close()
    '        cmduser.Dispose()
    '        cmddelete.Visible = False
    '        'Dim tokens As String() = Split(sharewith, ",")
    '        'Response.Write(LastIndexOf(tokens))
    '        ''''''''Dim query As String = "select * from warsquerymaster where tablename='" & ddlTableName.SelectedValue & "'"
    '        ''''''''Dim objCbo1Cmd As New SqlCommand
    '        ''''''''Dim adp As New SqlDataAdapter
    '        ''''''''Dim ds As New DataSet
    '        ''''''''objCbo1Cmd.Connection = connection
    '        ''''''''objCbo1Cmd.CommandText = query
    '        ''''''''adp.SelectCommand = objCbo1Cmd
    '        ''''''''connection.Open()
    '        ''''''''adp.Fill(ds, "warsquerymaster")
    '        ''''''''connection.Close()
    '        ''''''''ddlQueryName.DataSource = ds
    '        ''''''''ddlQueryName.DataTextField = "queryName"
    '        ''''''''ddlQueryName.DataBind()
    '        ''''''''ddlQueryName.Items.Insert(0, "--select--")
    '        '''''''''SetFocus(ddlTableName)
    '        ''''''''objCbo1Cmd.Dispose()
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    'End Function
    'Public Function ddlQueryName_bind()
    '    Try
    '        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '        'Session("username")
    '        Dim selectRep As New ReportDesigner
    '        ddlQueryName.Items.Clear()
    '        'Dim dept
    '        'Dim client
    '        'Dim lob
    '        'If cbodept.SelectedIndex <> 0 And cbodept.SelectedIndex <> -1 Then
    '        '    dept = cbodept.SelectedValue
    '        'Else
    '        '    dept = 0
    '        'End If
    '        'If cboclient.SelectedIndex <> 0 And cboclient.SelectedIndex <> -1 Then
    '        '    client = cboclient.SelectedValue
    '        'Else
    '        '    client = 0
    '        'End If
    '        'If cbolob.SelectedIndex <> 0 And cbolob.SelectedIndex <> -1 Then
    '        '    lob = cbolob.SelectedValue
    '        'Else
    '        '    lob = 0
    '        'End If
    '        Dim sharewith As String = ""
    '        'Dim cmduser As New SqlCommand("select queryName,sharedwith from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and sharedwith is not null  and ',' + isnull(removesharing,'') + ',' not like '%," & Session("userid") & ",%'", connection)

    '        Dim cmduser As New SqlCommand("select queryName,sharedwith from warsquerymaster where sharedwith is not null order by queryName", connection)
    '        'Dim cmduser As New SqlCommand(qstr, connection)

    '        Dim rdusern As SqlDataReader
    '        connection.Open()
    '        rdusern = cmduser.ExecuteReader
    '        Dim AllQuery As String = ""
    '        Do While rdusern.Read
    '            Dim i As Integer = 0
    '            Dim j As Integer
    '            sharewith = rdusern("sharedwith")
    '            Dim str As String()
    '            str = Split(Trim(sharewith), ",")
    '            i = str.Length
    '            For j = 0 To i - 1
    '                'Response.Write(Session("username1"))
    '                'Response.End()
    '                If Trim(str(j)) = Trim(Session("userid")) Then
    '                    ddlQueryName.Items.Add(rdusern("queryName"))
    '                    If AllQuery = "" Then

    '                        AllQuery = "'" + rdusern("queryName") + "'"
    '                    Else
    '                        AllQuery = AllQuery + "," + "'" + rdusern("queryName") + "'"
    '                    End If
    '                Else
    '                    If AllQuery = "" Then

    '                        AllQuery = "'" + rdusern("queryName") + "'"
    '                    Else
    '                        AllQuery = AllQuery + "," + "'" + rdusern("queryName") + "'"
    '                    End If
    '                End If
    '            Next
    '        Loop
    '        connection.Close()
    '        rdusern.Close()
    '        cmduser.Dispose()
    '        If AllQuery <> "" Then

    '            cmduser = New SqlCommand("select queryName,userid from warsquerysharerights where queryName in(" + AllQuery + ") and userid='" + Session("userid") + "' order by queryName", connection)


    '            connection.Open()
    '            rdusern = cmduser.ExecuteReader


    '            Do While rdusern.Read
    '                ddlQueryName.Items.Add(rdusern("queryName"))
    '            Loop
    '            connection.Close()
    '            rdusern.Close()
    '            cmduser.Dispose()
    '        End If
    '        ddlQueryName.Items.Insert(0, "--select--")


    '        cmddelete.Visible = True

    '        'Dim tokens As String() = Split(sharewith, ",")
    '        'Response.Write(LastIndexOf(tokens))
    '        ''''''''Dim query As String = "select * from warsquerymaster where tablename='" & ddlTableName.SelectedValue & "'"
    '        ''''''''Dim objCbo1Cmd As New SqlCommand
    '        ''''''''Dim adp As New SqlDataAdapter
    '        ''''''''Dim ds As New DataSet
    '        ''''''''objCbo1Cmd.Connection = connection
    '        ''''''''objCbo1Cmd.CommandText = query
    '        ''''''''adp.SelectCommand = objCbo1Cmd
    '        ''''''''connection.Open()
    '        ''''''''adp.Fill(ds, "warsquerymaster")
    '        ''''''''connection.Close()
    '        ''''''''ddlQueryName.DataSource = ds
    '        ''''''''ddlQueryName.DataTextField = "queryName"
    '        ''''''''ddlQueryName.DataBind()
    '        ''''''''ddlQueryName.Items.Insert(0, "--select--")
    '        '''''''''SetFocus(ddlTableName)
    '        ''''''''objCbo1Cmd.Dispose()
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    '    Return 1

    'End Function

    Protected Sub ddlYourQuery_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYourQuery.SelectedIndexChanged
        Try
            'If Me.ddlQueryName.SelectedIndex > 0 Then
            '    ShowConfirm("Please select any one from your query and shared query ")
            '    Me.ddlYourQuery.SelectedIndex = 0
            '    Exit Sub
            'End If
            'ddlYourQuery
            If Me.ddlYourQuery.SelectedIndex > 0 Then
                cmddelete.Visible = True
                'Dim dept
                'Dim client
                'Dim lob
                'If cbodept.SelectedIndex <> 0 And cbodept.SelectedIndex <> -1 Then
                '    dept = cbodept.SelectedValue
                'Else
                '    dept = 0
                'End If
                'If cboclient.SelectedIndex <> 0 And cboclient.SelectedIndex <> -1 Then
                '    client = cboclient.SelectedValue
                'Else
                '    client = 0
                'End If
                'If cbolob.SelectedIndex <> 0 And cbolob.SelectedIndex <> -1 Then
                '    lob = cbolob.SelectedValue
                'Else
                '    lob = 0
                'End If

                cmd.Connection = con
                'cmd.CommandText = "select * from warsquerymaster where queryname like '" & ddlYourQuery.SelectedItem.Text & "' and ',' + sharedwith + ',' like '%," & Session("username1") & ",%' "
                'Response.Write("select * from warsquerymaster where queryname like '" & ddlYourQuery.SelectedItem.Text & "' and ',' + sharedwith + ',' like '%," & Session("userid") & ",%' ")
                'Response.End()
                cmd.CommandText = "select * from warsquerymaster where queryname = '" & ddlYourQuery.SelectedItem.Text & "' order by queryName"
                con.Open()
                dr = cmd.ExecuteReader
                While dr.Read = True
                    chkPercentage.Value = dr("Percentage").ToString
                    formulaName.Value = dr("formulaName").ToString
                    Showdata.Value = dr("Showdata").ToString
                    formula.Value = dr("formula").ToString
                    crdata.Value = dr("crdata").ToString
                    column.Value = dr("colName").ToString
                    wheredata.Value = dr("wheredata").ToString
                    wheredata1.Value = dr("wheredata1").ToString
                    selectedfield.Value = dr("selected_field").ToString
                    hidtablename.Value = dr("tableName").ToString
                    Session("mailtain") = dr("status").ToString
                End While
                con.Close()
                cmd.Dispose()
                Session("mailtain") = Session("mailtain") + "," + ddlYourQuery.SelectedItem.Text
            Else
                cmddelete.Visible = True
            End If

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Protected Sub cmddelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmddelete.Click
        Try
            If Me.ddlYourQuery.SelectedIndex <= 0 Then
                ShowConfirm("Please select any one from your query.")
                Exit Sub
            End If
            Dim dept As String
            Dim client As String
            Dim lob As String

            '''ssss
            Dim q_name As String = ""
                q_name = ddlYourQuery.SelectedItem.Text
            If Session("usertype") = "2" And ddlYourQuery.SelectedIndex > 0 Then
            Else



                Dim cmdne As New SqlDataAdapter("select savedby from WARSQueryMaster where queryname='" + q_name + "' and departmentid='" + dept + "' and clientid='" + client + "' and lobyname='" + lob + "'", connection)
                Dim dsss As New DataSet
                cmdne.Fill(dsss)
                owner.Value = dsss.Tables(0).Rows(0)(0).ToString
                cmdne.Dispose()
                dsss.Clear()


                Dim recid As String = ""

                If LCase(owner.Value) <> LCase(Session("userid")) Then
                    Dim cmdne1 As New SqlDataAdapter("select recordid from WARSQueryMaster where queryname='" + q_name + "' and departmentid='" + dept + "' and clientid='" + client + "' and lobyname='" + lob + "'", connection)
                    Dim dsss1 As New DataSet
                    cmdne.Fill(dsss)
                    recid = dsss1.Tables(0).Rows(0)(0).ToString
                    cmdne1.Dispose()
                    dsss1.Clear()
                    cmdne1 = New SqlDataAdapter("select [Delete] from warsquerysharerights  where recid='" + recid + "'", connection)

                    dsss = New DataSet
                    cmdne1.Fill(dsss1)
                    If dsss1.Tables(0).Rows.Count > 0 Then


                        Dim t As String = dsss.Tables(0).Rows(0)(0).ToString
                        If LCase(t) = "true" Then

                        Else
                            ShowConfirm("You do not have Delete right!!")
                            Exit Sub
                        End If
                    Else
                        ShowConfirm("You do not have Delete right!!")
                        Exit Sub
                    End If
                End If
            End If
            ''''ssss
            ''************************************************************new**************************************
            'If Me.ddlYourQuery.SelectedIndex > 0 Then
            If Session("usertype") = "superadmin" Then
                Dim cmdins As New SqlCommand("insert into warsquerydeletelog select tableName,wheredata,wheredata1,showData,crdata,colName,selected_field,formula,queryName,savedBy,createDate,lobyName,sharedwith,Percentage,FormulaName,DepartmentId,ClientId,removesharing,getdate() from warsquerymaster where queryname='" & ddlYourQuery.SelectedItem.Text & "'", con)
                Dim cmddelete As New SqlCommand("delete warsquerymaster where queryname like '" & ddlYourQuery.SelectedItem.Text & "'", con)
                Dim cmddelete1 As New SqlCommand("delete subtotal where queryName= '" & ddlYourQuery.SelectedItem.Text & "'", con)
                con.Open()
                cmdins.ExecuteNonQuery()
                cmddelete.ExecuteNonQuery()
                cmddelete1.ExecuteNonQuery()
                con.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim objQB As New QueryBuilder()
                Dim str1 = objQB.trackQ_BuilderForMaster(Session("userid"), "Delete", "Query", ddlYourQuery.SelectedItem.Text, "  ")

                Dim cmm As New SqlCommand("insert into Querybuilder_utype select MAX(Auto_id)," + Session("usertype") + " from Track_QueryBuilder where Query_Name='" + ddlYourQuery.SelectedItem.Text + "' and Action='Delete'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            Else
                ''''''''No rights chk
                Dim cmddelete As New SqlCommand("delete warsquerymaster where queryname = '" & q_name & "' and departmentid='" + dept + "' and clientid='" + client + "' and lobyname='" + lob + "'", con)
                con.Open()
                cmddelete.ExecuteNonQuery()
                Dim cmddelete1 As New SqlCommand("delete subtotal where queryName= '" & q_name & "'", con1)
                con1.Open()
                cmddelete1.ExecuteNonQuery()
                con1.Close()
                con.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim objQB As New QueryBuilder()
                Dim str1 = objQB.trackQ_BuilderForMaster(Session("userid"), "Delete", "Query", q_name, "")

                Dim cmm As New SqlCommand("insert into Querybuilder_utype select MAX(Auto_id)," + Session("usertype") + " from Track_QueryBuilder where Query_Name='" + q_name + "' and Action='Delete'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            End If

            ddlYourQuery_bind()
            'ddlQueryName_bind()
            ShowConfirm("Your query has been deleted successfully")
            'End If

            ''************************************************************old***************************************
            ''If Me.ddlYourQuery.SelectedIndex > 0 Then

            ''    Dim cmddelete As New SqlCommand("delete warsquerymaster where queryname like '" & ddlYourQuery.SelectedItem.Text & "' and savedby='" & Session("userid") & "'", con)
            ''    con.Open()
            ''    cmddelete.ExecuteNonQuery()
            ''    '*********************************changes made by vini****************************************************************
            ''    Dim cmddelete1 As New SqlCommand("delete subtotal where queryName= '" & ddlYourQuery.SelectedItem.Text & "'", con1)
            ''    con1.Open()
            ''    cmddelete1.ExecuteNonQuery()
            ''    con1.Close()
            ''    '**********************************************************************************************************************
            ''    con.Close()
            ''    ddlYourQuery_bind()
            ''    ddlQueryName_bind()
            ''End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
        ddlYourQuery.SelectedIndex = 0
    End Sub

    'Protected Sub cbodept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbodept.SelectedIndexChanged
    '    cboclient.Items.Clear()
    '    cbolob.Items.Clear()

    '    Try
    '        Dim cmdst As New SqlCommand("select *  from idmsclient where deptid='" & cbodept.SelectedValue & "'", connection)
    '        Dim dsst As New DataSet
    '        Dim adpst As New SqlDataAdapter
    '        adpst.SelectCommand = cmdst
    '        connection.Open()
    '        Dim cntr = adpst.Fill(dsst)
    '        connection.Close()
    '        cboclient.DataSource = dsst
    '        cboclient.DataTextField = "clientname"
    '        cboclient.DataValueField = "autoid"
    '        cboclient.DataBind()
    '        cboclient.Items.Insert("0", "--Select--")
    '        'Me.cbolob.Items.Insert("0", "")
    '        'Me.cbolob.SelectedValue = ""
    '        If Me.cbolob.SelectedValue <> "" Then
    '            For i = 0 To cbolob.Items.Count - 1
    '                cbolob.Items.Clear()
    '            Next
    '        End If


    '        ddlQueryName_bind()
    '        ddlYourQuery_bind()
    '        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    '    ' End If
    'End Sub

    'Protected Sub cboclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboclient.SelectedIndexChanged
    '    ddlQueryName_bind()
    '    ddlYourQuery_bind()
    '    If cbodept.SelectedIndex = 0 Then
    '        ShowConfirm("Please select department")
    '    ElseIf cboclient.SelectedIndex = 0 Then
    '        cbolob.Items.Clear()
    '    Else
    '        Try
    '            Dim cmdst As New SqlCommand("select *  from warslobmaster where deptid='" & cbodept.SelectedValue & "' and ClientId='" & cboclient.SelectedValue & "'", connection)
    '            Dim dsst As New DataSet
    '            Dim adpst As New SqlDataAdapter
    '            adpst.SelectCommand = cmdst
    '            connection.Open()
    '            adpst.Fill(dsst)
    '            connection.Close()
    '            cbolob.DataSource = dsst
    '            cbolob.DataTextField = "LOBname"
    '            cbolob.DataValueField = "autoid"
    '            cbolob.DataBind()
    '            cbolob.Items.Insert("0", "--Select--")

    '            ''''''''''''''''''''''''''''

    '        Catch ex As Exception
    '            Dim strmsg As String
    '            strmsg = Replace(ex.Message.ToString, "'", "")
    '            strmsg = Replace(strmsg, vbCrLf, " ")
    '            ShowConfirm(strmsg)
    '        End Try

    '    End If
    'End Sub

    'Protected Sub cbolob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolob.SelectedIndexChanged
    '    ddlQueryName_bind()
    '    ddlYourQuery_bind()
    'End Sub
    Public Function getheading(ByVal hidtablename) As String
        Dim result As String
        'If ddlQueryName.SelectedIndex <> 0 Then
        '    Dim cmdsave As New SqlCommand("select queryName from warsquerymaster where tableName='" & hidtablename & "'", connectionsave)
        '    Dim rdrsave As SqlDataReader
        '    connectionsave.Open()
        '    rdrsave = cmdsave.ExecuteReader
        '    While rdrsave.Read
        '        Dim mainqury As String
        '        mainqury = rdrsave("queryName")

        '        Dim cmdsave1 As New SqlCommand("select *  from subtotal where queryName='" & Me.ddlQueryName.SelectedItem.Text & "'", connectionsave1)
        '        Dim rdrsave1 As SqlDataReader
        '        connectionsave1.Open()
        '        rdrsave1 = cmdsave1.ExecuteReader
        '        If rdrsave1.Read Then
        '            Dim subqury As String
        '            Dim head As String
        '            subqury = rdrsave1("queryName")
        '            head = rdrsave1("subheading")

        '            If mainqury = subqury Then
        '                Dim heading As String
        '                heading = head

        '                result = heading
        '            End If
        '        End If
        '        connectionsave1.Close()
        '        rdrsave1.Close()

        '    End While
        '    connectionsave.Close()
        '    rdrsave.Close()
        '    Return result

        'Else
        Dim cmdsave3 As New SqlCommand("select queryName from warsquerymaster where tableName='" & hidtablename & "'", connectionsave2)
        Dim rdrsave3 As SqlDataReader
        connectionsave2.Open()
        rdrsave3 = cmdsave3.ExecuteReader

        While rdrsave3.Read
            Dim mainqury As String
            mainqury = rdrsave3("queryName")

            Dim cmdsave4 As New SqlCommand("select *  from subtotal where queryName='" & Me.ddlYourQuery.SelectedItem.Text & "'", connectionsave4)
            Dim rdrsave4 As SqlDataReader
            connectionsave4.Open()
            rdrsave4 = cmdsave4.ExecuteReader
            If rdrsave4.Read Then
                Dim subqury As String
                Dim head As String
                subqury = rdrsave4("queryName")
                head = rdrsave4("subheading")

                If mainqury = subqury Then
                    Dim heading As String
                    heading = head

                    result = heading
                End If
            End If
            connectionsave4.Close()
            rdrsave4.Close()

        End While
        connectionsave2.Close()
        rdrsave3.Close()
        Return result

        'End If


    End Function
    Public Function getsub()
       
        Dim result1 As String

        result1 = getheading(hidtablename.Value)
        Return result1

    End Function
    Public Function ddlYourQuery_bind()
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
        Try
            Dim selectRep As New ReportDesigner
            
            Dim qstr As String = ""
           
                qstr = "select queryName from warsquerymaster where savedby='" + Session("userid") + "' order by queryName"

            'Dim query As String = "select queryName from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and savedby like'" & Session("UserId") & "'  and ',' + isnull(removesharing,'') + ',' not like '%," & Session("userid") & ",%'"
            'Dim query As String = "select queryName from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and savedby like'" & lblUser.Text & "'"
            Dim objCbo1Cmd As New SqlCommand
            Dim adp As New SqlDataAdapter
            Dim ds As New DataSet
            objCbo1Cmd.Connection = connection
            objCbo1Cmd.CommandText = qstr
            adp.SelectCommand = objCbo1Cmd
            connection.Open()
            adp.Fill(ds, "warsquerymaster")
            connection.Close()
            ddlYourQuery.DataSource = ds
            ddlYourQuery.DataTextField = "queryName"
            ddlYourQuery.DataBind()
            ddlYourQuery.Items.Insert(0, "--select--")
            objCbo1Cmd.Dispose()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Function

End Class
