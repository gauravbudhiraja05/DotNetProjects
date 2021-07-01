Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Partial Class TableTools_table
    Inherits System.Web.UI.Page
    Dim dr As SqlDataReader
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim con As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim maxvalue As Integer
    Dim constraintname As String = ""
    Dim constriantnamefinal As String = ""

    Public Sub SetsFocus(ByVal FocusControl As Control)
        ''''''''''''''''this function is for setting the focus''''''''''''''''''''
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "SetsFocus", Script.ToString())
    End Sub
    ' Finction For Checking Special Character
    Public Function CheckSpecialCharacter(ByVal input As String) As Boolean
        Dim IsAllSpecial As Boolean = False
        Dim count As Integer
        For count = 1 To input.Length
            If ((Asc(Mid(input, count, 1)) >= 33 And Asc(Mid(input, count, 1)) <= 47)) Or ((Asc(Mid(input, count, 1)) >= 58 And Asc(Mid(input, count, 1)) <= 64)) Or ((Asc(Mid(input, count, 1)) >= 91 And Asc(Mid(input, count, 1)) <= 96)) Or ((Asc(Mid(input, count, 1)) >= 123 And Asc(Mid(input, count, 1)) <= 126)) Then
                IsAllSpecial = True
            Else
                IsAllSpecial = False
                Exit Function
            End If

        Next
        Return IsAllSpecial
    End Function

    Public Sub ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Me.GetType, "ShowConfirm", Script)
    End Sub
    Public Sub constrianname()

        Dim maxcount As New SqlCommand("select max (tableid+1) from WARSLOBTableMaster", connection)
        connection.Open()
        maxvalue = maxcount.ExecuteScalar
        connection.Close()
        constraintname = "Constraint pk_prim" & maxvalue & DateTime.Now.Millisecond

    End Sub
    Private Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes_singleuser.Click
        Try
            constrianname()
            divmsgbox.Visible = False
            Dim objCmd As New SqlCommand("select name from sysobjects where xtype='U' and name='" & Me.txttable.Value & "'", connection)
            Dim dr1 As SqlDataReader
            connection.Open()
            dr1 = objCmd.ExecuteReader
            If dr1.Read = True Then
                ShowConfirm("Table Name Already Exist So please Enter new name!")
                Me.txttable.Value = ""
                SetsFocus(Me.txttable)
                connection.Close()
                dr1.Close()
                objCmd.Dispose()
            Else
                '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                If txthidden.Value = "" Then
                    ShowConfirm("Please select at least one column to create table!!")
                Else

                    If PrimColValue.Value = "" Then
                        txttablsql.Value = txttablsql.Value
                        constriantnamefinal = "Null"
                    Else
                        txttablsql.Value = txttablsql.Value & " " & constraintname & " " & "PRIMARY KEY" & "(" & PrimColValue.Value & ")"
                        constriantnamefinal = constraintname

                    End If


                    '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                    Dim Cmd As New SqlCommand("create table " & Me.txttable.Value & "(" & Me.txttablsql.Value & ")", connection1)
                    connection1.Open()
                    Cmd.ExecuteNonQuery()
                    connection1.Close()
                    Cmd.Dispose()
                    ShowConfirm("Table Created Succesfully!!!!")
                    ''''''''''''''''entry of WARSLOBTableMaster table''''''''''''''''
                    Dim cmdin As New SqlCommand
                    cmdin.CommandType = CommandType.StoredProcedure
                    cmdin.CommandText = "insert_WARSLOBTableMaster"
                    cmdin.Connection = connection2
                    With cmdin.Parameters
                        'If ddlLobname.SelectedValue = "" Or ddlLobname.SelectedValue = "--Select--" Then
                        .AddWithValue("@LobId", "0")
                        'Else
                        '    .AddWithValue("@LobId", ddlLobname.SelectedValue)
                        'End If
                        .AddWithValue("@TableName", Me.txttable.Value)
                        .AddWithValue("@CreatedOn", System.DateTime.Today)
                        .AddWithValue("@CreatedBy", Session("Userid"))
                        .AddWithValue("@LastModified", System.DateTime.Today)
                        .AddWithValue("@LastModifiedBy", Session("Userid"))
                        .AddWithValue("@currdate", System.DateTime.Today)
                        .AddWithValue("@VisibleColumn", txthidden.Value)
                        .AddWithValue("@DepartmentId", "60")

                        'If Clientname.SelectedValue = "--Select--" Or Clientname.SelectedValue = "" Then
                        .AddWithValue("@ClientId", "0")
                        'Else
                        '    .AddWithValue("@ClientId", Clientname.SelectedValue)
                        'End If
                        'If chkSelectscope.Checked = True Then
                        '    .AddWithValue("@Local", "Local")
                        'Else
                        .AddWithValue("@Local", "NonLocal")
                        'End If
                        .AddWithValue("@primcol", PrimColValue.Value)
                        .AddWithValue("@constraintname", constriantnamefinal)
                    End With
                    connection2.Open()
                    cmdin.ExecuteNonQuery()
                    connection2.Close()
                    cmdin.Dispose()

                    connection2.Open()
                    Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txttable.Value + "' and Action='Save'", connection2)
                    cmm.ExecuteNonQuery()
                    connection2.Close()

                    '************************************ Entry In TableRights
                    Dim tabid1 As Integer
                    Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txttable.Value & "'", connection1)
                    connection1.Open()
                    tabid1 = tabid.ExecuteScalar
                    connection1.Close()
                    tabid.Dispose()

                    Dim cmdsave As New SqlCommand("insert_tablerights", connection1)
                    cmdsave.CommandType = CommandType.StoredProcedure
                    With cmdsave.Parameters
                        .Add("@TableId", tabid1)
                        .Add("@currdate", System.DateTime.Now)
                        .Add("@UserId", Session("Userid1"))
                        .Add("@AssignedBy", Session("userid1"))
                        'To be changed
                    End With
                    connection1.Open()
                    cmdsave.ExecuteNonQuery()
                    connection1.Close()
                    cmdsave.Dispose()
                    '************************************


                    Me.txthidden.Value = ""
                    Me.txttable.Value = ""
                    Me.txttablsql.Value = ""
                    SetsFocus(Me.txttable)
                End If
            End If
            connection.Close()
            dr1.Close()
            objCmd.Dispose()

            'End If
        Catch ex As Exception
            'Dim strmsg As String
            'strmsg = Replace(ex.Message.ToString, "'", "")
            'strmsg = Replace(strmsg, vbCrLf, " ")
            'ShowConfirm(strmsg)
            ShowConfirm("Incorrect Syntax")
        End Try

    End Sub

    Private Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click
        divmsgbox.Visible = False
        SetsFocus(Me.txttable)
        
    End Sub

    Private Sub createtable_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles createtable.ServerClick

        'If DepartmentName.SelectedValue = "--Select--" Then
        '    ShowConfirm("Please Enter the Department first !!!!")
        If Me.txttable.Value = "" Then
            ShowConfirm("Please Enter the tablename first !!!!")
            SetsFocus(Me.txttable)
        ElseIf txttablsql.Value = "" Then
            ShowConfirm("Table must have atleast one column !!!!")
            SetsFocus(Me.txtfieldname)
            Else
            If divmsgbox.Visible = False Then
                divmsgbox.Visible = True
            End If
            End If
    End Sub


    'Private Sub Clientname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
    '    If Clientname.SelectedValue = "--Select--" Or Clientname.SelectedValue = "" Then
    '        ddlLobname.Items.Clear()
    '        'ddlLobname.Items.Insert(0, "--Select--")
    '        status(DepartmentName.SelectedValue, "0", "0")
    '    Else
    '        Dim classobj As New Functions
    '        ddlLobname.DataTextField = "LOB"
    '        ddlLobname.DataValueField = "autoid"
    '        ddlLobname.DataSource = classobj.bindlob(DepartmentName.SelectedValue, Clientname.SelectedValue)
    '        ddlLobname.DataBind()
    '        ddlLobname.Items.Insert(0, "--Select--")
    '        status(DepartmentName.SelectedValue, Clientname.SelectedValue, "0")
    '    End If

    'End Sub

    'Private Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged

    '    If DepartmentName.SelectedValue = "--Select--" Or DepartmentName.SelectedValue = "" Then
    '        Clientname.Items.Clear()
    '        ddlLobname.Items.Clear()
    '        status("0", "0", "0")
    '        'Clientname.Items.Insert(0, "--Select--")
    '    Else
    '        Dim classobj As New Functions
    '        Clientname.DataTextField = "ClientName"
    '        Clientname.DataValueField = "autoid"
    '        Clientname.DataSource = classobj.bindclient(DepartmentName.SelectedValue)
    '        Clientname.DataBind()
    '        Clientname.Items.Insert(0, "--Select--")
    '        ddlLobname.Items.Clear()
    '        status(DepartmentName.SelectedValue, "0", "0")
    '    End If

    '    ' Commented As on Date 23/08/2008 By Rohit
    '    'If DepartmentName.SelectedItem.Text = "--Select--" Then
    '    '    Clientname.Items.Clear()
    '    '    ddlLobname.Items.Clear()
    '    'Else
    '    '    Dim classobj As New Functions

    '    '    '**************Bind Client When User Type is ADMIn***************
    '    '    If Session("typeofuser") = "Admin" Then
    '    '        Clientname.DataTextField = "clientname"
    '    '        Clientname.DataValueField = "autoid"
    '    '        Clientname.DataSource = classobj.bindAdminClients(Session("userid1"), DepartmentName.SelectedValue)
    '    '        Clientname.DataBind()
    '    '        Clientname.Items.Insert(0, "--Select--")
    '    '    End If
    '    '    If Session("typeofuser") = "User" Then
    '    '        '**************Bind Clent When User Type is ADMIn***************
    '    '        Clientname.DataTextField = "clientname"
    '    '        Clientname.DataValueField = "autoid"
    '    '        Clientname.DataSource = classobj.bindUserClients(Session("userid1"), DepartmentName.SelectedValue)
    '    '        Clientname.DataBind()
    '    '        Clientname.Items.Insert(0, "--Select--")
    '    '    End If
    '    '    ddlLobname.Items.Clear()
    '    'End If
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AjaxPro.Utility.RegisterTypeForAjax(GetType(AjaxClass))
       

        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                cmdyes_multiuser.Visible = True
                cmdyes_singleuser.Visible = False

                'Me.Gettable.Visible = False
                rdr.Close()
                Dim cmd1 As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd1)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    lblDept.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    lblClient.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    lblLOB.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                Else
                    Me.spandisplay.Visible = False
                    cmdyes_multiuser.Visible = False
                    cmdyes_singleuser.Visible = True

                    'Me.Gettable.Visible = True
                End If
                connection.Close()
            End If
        End If


        Dim typeofuser = Session("typeofuser")
        If (Page.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "-- Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            End If
            DepartmentName.Items.Insert(0, "--select--")
        End If

        ''**************************************************

        btnAddField.Attributes.Add("onclick", "addfield()")
    End Sub


    Protected Sub chkSelectscope_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelectscope.CheckedChanged
        If chkSelectscope.Checked = True Then
            chkSelectscope.ToolTip = "Making Local"
        Else
            chkSelectscope.ToolTip = "Making NonLocal"
        End If
    End Sub
    Public Sub status(ByVal dept As String, ByVal clt As String, ByVal lob As String)
        If Session("typeofuser") = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                .AddWithValue("@Deptid", dept)
                .AddWithValue("@Clientid", clt)
                .AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                chkSelectscope.Enabled = True
            Else

                chkSelectscope.Checked = False
                chkSelectscope.Enabled = False
            End If
            readerdata.Close()
            connection.Close()
        ElseIf Session("typeofuser") = "User" Then
            Dim repobj As New ReportDesigner
            'Dim SCOPE As String = repobj.chkUserscope(Session("userid"))
            'If SCOPE = "Local" Then
            '    chkSelectscope.Enabled = True
            'Else
            '    chkSelectscope.Checked = False
            '    chkSelectscope.Enabled = False
            'End If
        End If

    End Sub
    Protected Sub ddlLobname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobname.SelectedIndexChanged
        If ddlLobname.SelectedIndex = 0 Then
            status(DepartmentName.SelectedValue, Clientname.SelectedValue, "0")
        Else
            status(DepartmentName.SelectedValue, Clientname.SelectedValue, ddlLobname.SelectedValue)
        End If

    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        ddlLobname.Items.Clear()
        Clientname.Items.Clear()
        If (DepartmentName.SelectedValue = "--Select--") Then
            ddlLobname.Items.Clear()
            Clientname.Items.Clear()
            status("0", "0", "0")
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            Clientname.DataSource = dr
            Clientname.DataTextField = "ClientName"
            Clientname.DataValueField = "autoid"
            Clientname.DataBind()
            Clientname.Items.Insert(0, "--Select--")
            ddlLobname.Items.Clear()
            status(DepartmentName.SelectedValue, "0", "0")

        End If
       
    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
        If (Clientname.SelectedValue = "--Select--") Then
            'aspnet_msgbox("Please Select ClientName")
            ddlLobname.Items.Clear()
            status(DepartmentName.SelectedValue, "0", "0")
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + Clientname.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ddlLobname.DataSource = dr
            ddlLobname.DataTextField = "LOBName"
            ddlLobname.DataValueField = "autoid"
            ddlLobname.DataBind()
            ddlLobname.Items.Insert(0, "--Select--")
            status(DepartmentName.SelectedValue, Clientname.SelectedValue, "0")
        End If
    End Sub

    Protected Sub cmdyes_multiuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes_multiuser.Click
        Try
            constrianname()
            divmsgbox.Visible = False
            Dim objCmd As New SqlCommand("select name from sysobjects where xtype='U' and name='" & Me.txttable.Value & "'", connection)
            Dim dr1 As SqlDataReader
            connection.Open()
            dr1 = objCmd.ExecuteReader
            If dr1.Read = True Then
                ShowConfirm("Table Name Already Exist So please Enter new name!")
                Me.txttable.Value = ""
                SetsFocus(Me.txttable)
                connection.Close()
                dr1.Close()
                objCmd.Dispose()
            Else
                '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                If txthidden.Value = "" Then
                    ShowConfirm("Please select at least one column to create table!!")
                Else

                    If PrimColValue.Value = "" Then
                        txttablsql.Value = txttablsql.Value
                        constriantnamefinal = "Null"
                    Else
                        txttablsql.Value = txttablsql.Value & " " & constraintname & " " & "PRIMARY KEY" & "(" & PrimColValue.Value & ")"
                        constriantnamefinal = constraintname

                    End If


                    '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                    Dim Cmd As New SqlCommand("create table " & Me.txttable.Value & "(" & Me.txttablsql.Value & ")", connection1)
                    connection1.Open()
                    Cmd.ExecuteNonQuery()
                    connection1.Close()
                    Cmd.Dispose()
                    ShowConfirm("Table Created Succesfully!!!!")
                    ''''''''''''''''entry of WARSLOBTableMaster table''''''''''''''''
                    Dim cmdin As New SqlCommand
                    cmdin.CommandType = CommandType.StoredProcedure
                    cmdin.CommandText = "insert_WARSLOBTableMaster"
                    cmdin.Connection = connection2
                    With cmdin.Parameters
                        If ddlLobname.SelectedValue = "" Or ddlLobname.SelectedValue = "--Select--" Then
                            .AddWithValue("@LobId", "0")
                        Else
                            .AddWithValue("@LobId", ddlLobname.SelectedValue)
                        End If
                        .AddWithValue("@TableName", Me.txttable.Value)
                        .AddWithValue("@CreatedOn", System.DateTime.Today)
                        .AddWithValue("@CreatedBy", Session("Userid"))
                        .AddWithValue("@LastModified", System.DateTime.Today)
                        .AddWithValue("@LastModifiedBy", Session("Userid"))
                        .AddWithValue("@currdate", System.DateTime.Today)
                        .AddWithValue("@VisibleColumn", txthidden.Value)
                        .AddWithValue("@DepartmentId", DepartmentName.SelectedValue)

                        If Clientname.SelectedValue = "--Select--" Or Clientname.SelectedValue = "" Then
                            .AddWithValue("@ClientId", "0")
                        Else
                            .AddWithValue("@ClientId", Clientname.SelectedValue)
                        End If
                        If chkSelectscope.Checked = True Then
                            .AddWithValue("@Local", "Local")
                        Else
                            .AddWithValue("@Local", "NonLocal")
                        End If
                        .AddWithValue("@primcol", PrimColValue.Value)
                        .AddWithValue("@constraintname", constriantnamefinal)
                    End With
                    connection2.Open()
                    cmdin.ExecuteNonQuery()
                    connection2.Close()
                    cmdin.Dispose()

                    connection2.Open()
                    Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txttable.Value + "' and Action='Save'", connection2)
                    cmm.ExecuteNonQuery()
                    connection2.Close()

                    '************************************ Entry In TableRights
                    Dim tabid1 As Integer
                    Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txttable.Value & "'", connection1)
                    connection1.Open()
                    tabid1 = tabid.ExecuteScalar
                    connection1.Close()
                    tabid.Dispose()

                    Dim cmdsave As New SqlCommand("insert_tablerights", connection1)
                    cmdsave.CommandType = CommandType.StoredProcedure
                    With cmdsave.Parameters
                        .Add("@TableId", tabid1)
                        .Add("@currdate", System.DateTime.Now)
                        .Add("@UserId", Session("Userid1"))
                        .Add("@AssignedBy", Session("userid1"))
                        'To be changed
                    End With
                    connection1.Open()
                    cmdsave.ExecuteNonQuery()
                    connection1.Close()
                    cmdsave.Dispose()
                    '************************************


                    Me.txthidden.Value = ""
                    Me.txttable.Value = ""
                    Me.txttablsql.Value = ""
                    SetsFocus(Me.txttable)
                End If
            End If
            connection.Close()
            dr1.Close()
            objCmd.Dispose()

            'End If
        Catch ex As Exception
            'Dim strmsg As String
            'strmsg = Replace(ex.Message.ToString, "'", "")
            'strmsg = Replace(strmsg, vbCrLf, " ")
            'ShowConfirm(strmsg)
            ShowConfirm("Incorrect Syntax")
        End Try
    End Sub
End Class
