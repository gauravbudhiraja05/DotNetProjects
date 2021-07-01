Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_ViewUpdStructList
    Inherits System.Web.UI.Page
    Dim selectRep As New ReportDesigner
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim conn As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Dim dept = "0"
    Dim client = "0"
    Dim lob = "0"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay1.Visible = True
                rdr.Close()
                Dim cmd As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    Dim val1 As String
                    Dim val2 As String
                    Dim val3 As String
                    val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                    lbl1.Text = val1
                    lbl2.Text = val2
                    lbl3.Text = val3
                End If
            Else
                Me.cmdshow.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
        connection.Close()
        Session("nmsg") = ""
        Dim typeofuser = Session("typeofuser")
        If (Me.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
   
    Private Sub cmdyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        Dim a As String = txtrecid.Text
        Try
            Dim lob As String = 0
            Dim client As String = 0
            'If txtlob.Value = "" Or txtlob.Value = "--Select--" Then
            '    lob = 0
            'Else
            '    lob = txtlob.Value
            'End If


            'If txtclient.Value = "" Or txtclient.Value = "--Select--" Then
            '    client = 0
            'Else
            '    client = txtclient.Value
            'End If
            'If Session("typeofuser") = "User" Then


            Dim dsgetdept As New DataSet
            Dim da As New SqlDataAdapter
            Dim cmddel As New SqlCommand("select * from IDMS_UpdateCommandRights where userid='" + Session("userid") + "' and [delete] = 'true' and cmdid='" & Trim(txtrecid.Text) & "'", connection)
            connection.Open()
            da.SelectCommand = cmddel
            da.Fill(dsgetdept)
            connection.Close()
            If dsgetdept.Tables(0).Rows.Count > 0 Then

                cmddel = New SqlCommand("delete from IdmsUpdateTabStruct where CmdId='" & Trim(txtrecid.Text) & "'", connection)
                connection.Open()
                cmddel.ExecuteNonQuery()
                connection.Close()
                '****************************Change*********************************

                Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", Session("userid"))
                    .AddWithValue("@Action", "Delete")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@Entity", "Update Command")
                    .AddWithValue("@EntityName", txtname.Text)
                    .AddWithValue("@DeptId", 60)
                    .AddWithValue("@ClientId", client)
                    .AddWithValue("@LOBId", lob)

                End With
                connection.Open()
                cmdins2.ExecuteNonQuery()
                connection.Close()
                cmdins2.Dispose()
                '*************change*************
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                'Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Delete'", connection)
                'connection.Open()
                'cmm.ExecuteNonQuery()
                'connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                pandelete.Visible = False
                txtrecid.Text = ""
                dlshow.CurrentPageIndex = 0
                bindgrid("")
                Showmsg("Record Deleted Successfully")
                pandelete.Visible = False
                'Else

                '    'For Checkin That logged user is owner of the Structure
                '    Dim owneruser As String
                '    Dim slectuser As New SqlCommand("Select CreatedBy From IdmsUpdateTabStruct where CmdId = '" & Trim(txtrecid.Text) & "'", connection)
                '    connection.Open()
                '    owneruser = slectuser.ExecuteScalar()
                '    connection.Close()
                '    If owneruser = Session("userid") Then
                '        cmddel = New SqlCommand("delete from IdmsUpdateTabStruct where CmdId ='" & Trim(txtrecid.Text) & "'", connection)
                '        connection.Open()
                '        cmddel.ExecuteNonQuery()
                '        connection.Close()
                '        '****************************Change*********************************

                '        Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
                '        cmdins2.CommandType = CommandType.StoredProcedure
                '        With cmdins2.Parameters


                '            .AddWithValue("@actionBY", Session("userid"))
                '            .AddWithValue("@Action", "Delete")
                '            .AddWithValue("@Date", System.DateTime.Now)
                '            .AddWithValue("@Entity", "Update Command")
                '            .AddWithValue("@EntityName", txtname.Text)
                '            .AddWithValue("@DeptId", 60)
                '            .AddWithValue("@ClientId", client)
                '            .AddWithValue("@LOBId", lob)

                '        End With
                '        connection.Open()
                '        cmdins2.ExecuteNonQuery()
                '        connection.Close()
                '        cmdins2.Dispose()
                '        '*************change*************
                '        '''''''''''''''Usertype check for track goes here:- By Suvidha

                '        Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Delete'", connection)
                '        connection.Open()
                '        cmm.ExecuteNonQuery()
                '        connection.Close()
                '        '''''''''''''''Usertype check for track goes here:- By Suvidha
                '        pandelete.Visible = False
                '        txtrecid.Text = ""
                '        txtname.Text = ""
                '        dlshow.CurrentPageIndex = 0
                '        bindgrid("")
                '        Showmsg("Record Deleted Successfully")
                '        pandelete.Visible = False
                '    Else
                '        Showmsg("You are not Authorised to Delete this Command")
                '        pandelete.Visible = False
                '        Exit Sub
            End If
            'End If
            'ElseIf Session("typeofuser") = "Admin" Then

            '    Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            '    cmdupdate.CommandType = CommandType.StoredProcedure
            '    With cmdupdate.Parameters
            '        .AddWithValue("@userid", Session("userid"))
            '        .AddWithValue("@Deptid", cbodept.SelectedValue)
            '        .AddWithValue("@Clientid", client)
            '        .AddWithValue("@LOBID", lob)
            '    End With
            '    Dim readerdata As SqlDataReader
            '    connection.Open()
            '    readerdata = cmdupdate.ExecuteReader


            '    If readerdata.HasRows Then
            '        connection.Close()
            '        Dim cmddel As New SqlCommand("delete from IdmsUpdateTabStruct where CmdId='" & Trim(txtrecid.Text) & "'", connection)
            '        connection.Open()
            '        cmddel.ExecuteNonQuery()
            '        connection.Close()
            '        '****************************Change*********************************

            '        Dim cmdins2 = New SqlCommand("TrackTableReplaceData", connection)
            '        cmdins2.CommandType = CommandType.StoredProcedure
            '        With cmdins2.Parameters


            '            .AddWithValue("@actionBY", Session("userid"))
            '            .AddWithValue("@Action", "Delete")
            '            .AddWithValue("@Date", System.DateTime.Now)
            '            .AddWithValue("@Entity", "Update Command")
            '            .AddWithValue("@EntityName", txtname.Text)
            '            .AddWithValue("@DeptId", cbodept.SelectedValue)
            '            .AddWithValue("@ClientId", client)
            '            .AddWithValue("@LOBId", lob)

            '        End With
            '        connection.Open()
            '        cmdins2.ExecuteNonQuery()
            '        connection.Close()
            '        cmdins2.Dispose()
            '        '*************change*************

            '        '''''''''''''''Usertype check for track goes here:- By Suvidha

            '        Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtname.Text & "' and Action='Delete'", connection)
            '        connection.Open()
            '        cmm.ExecuteNonQuery()
            '        connection.Close()
            '        '''''''''''''''Usertype check for track goes here:- By Suvidha
            '        pandelete.Visible = False
            '        txtrecid.Text = ""
            '        dlshow.CurrentPageIndex = 0
            '        bindgrid("")
            '        Showmsg("Record Deleted Successfully")
            '        pandelete.Visible = False
            '    Else
            '        Showmsg("You Are Not The Admin Of Selected Span")
            '        pandelete.Visible = False
            '    End If
            'End If
        Catch ex As Exception
            Showmsg(ex.Message())
        End Try

    End Sub
    Private Sub cmdno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False
        txtrecid.Text = ""
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub
    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click


        'If txtlob.Value = "" Or txtlob.Value = "--Select--" Then
        lob = 0
        'Else
        'lob = txtlob.Value
        'End If


        'If txtclient.Value = "" Or txtclient.Value = "--Select--" Then
        client = 0
        'Else
        'client = txtclient.Value
        'End If
        'If cbodept.SelectedIndex = 0 Then
        '    Showmsg("Please select department")
        'Else
        dlshow.CurrentPageIndex = 0
        txtdept.Value = 60
        txtclient.Value = client
        txtlob.Value = lob
        bindgrid("")
        '''new********************************************************************************

        'bindClient()
        'If client = 0 Then
        '    cboclient.SelectedIndex = 0
        'Else
        cboclient.SelectedValue = client
        'End If
        'bindLOB(client)
        'If lob = 0 Then
        '    cbolob.SelectedIndex = 0
        'Else
        cbolob.SelectedValue = lob
        'End If

        '''end********************************************************************************
        'End If
        '******************************change***********************



        dept = 60
        If Session("typeofuser") = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", conn)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                '.AddWithValue("@Deptid", dept)
                '.AddWithValue("@Clientid", client)
                '.AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            conn.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                Session("vis") = "1"
            End If
            readerdata.Close()
            conn.Close()
        End If
        '************************change***********************************
    End Sub
    Public Sub bindgrid(ByVal sortexp)
        dept = 60
        Dim cmdget As New SqlCommand()
        Dim uid = Session("userid")
        'If (Session("typeofuser") = "Admin") Then
        '    Dim exist As Boolean = False
        '    exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
        '    If exist = True Then
        '        Dim ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='60' and ClientId='0' and LobId='0' and (deptid in (select deptid from masteradmin where DeptId='60' and ClientId='0' and LobId='0' and adminid='" + uid + "') and clientid in(select clientid from masteradmin where DeptId='60' and ClientId='0' and LobId='0' and adminid='" + uid + "')   and lobid in (select lobid from masteradmin where DeptId='60' and ClientId='0' and LobId='0' and adminid='" + uid + "') or (createdby='" + uid + "') or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True'))"
        '        ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
        '        cmdget = New SqlCommand(ts, connection)
        '    Else
        '        GoTo adminOutofIndex
        '    End If

        'ElseIf (Session("typeofuser") = "Super Admin") Then
        '    Dim ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (createdby='" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True'))"
        '    ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
        '    cmdget = New SqlCommand(ts, connection)
        'Else
adminOutofIndex:
        'Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
        Dim ts = ""
        'If (scope = "Local") Then
        '    ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (createdby= '" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True' )or localcmd='Local') "
        '    cmdget = New SqlCommand(ts, connection)
        'Else
        ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='60' and ClientId='0' and LobId='0' and (createdby= '" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True')) "
        cmdget = New SqlCommand(ts, connection)
        'End If
        'End If


        'cmdget = New SqlCommand("select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "'", connection)
        Dim dsget As New DataSet
        Dim adpget As New SqlDataAdapter
        adpget.SelectCommand = cmdget
        connection.Open()
        adpget.Fill(dsget)
        connection.Close()
        Dim dv As DataView = New DataView(dsget.Tables(0))
        If sortexp <> "" Then
            dv.Sort = sortexp
        Else
            dv.Sort = "CmdName"
        End If
        dlshow.DataSource = dv
        dlshow.DataBind()
    End Sub
   
   
    Public Sub bindClient()
        Dim cmdclient As New SqlCommand("select * from IdmsClient where deptid='60'", connection)
        Dim dsclient As New DataSet
        Dim adpclient As New SqlDataAdapter
        adpclient.SelectCommand = cmdclient
        connection.Open()
        adpclient.Fill(dsclient)
        connection.Close()
        cboclient.DataSource = dsclient
        cboclient.DataTextField = "ClientName"
        cboclient.DataValueField = "autoid"
        cboclient.DataBind()
        cboclient.Items.Insert(0, "--Select--")
    End Sub
   
    Private Sub cmdshowmul_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshowmul.Click
        If txtlob.Value = "" Or txtlob.Value = "--Select--" Then
            lob = 0
        Else
            lob = txtlob.Value
        End If


        If txtclient.Value = "" Or txtclient.Value = "--Select--" Then
            client = 0
        Else
            client = txtclient.Value
        End If
        If cbodept.SelectedIndex = 0 Then
            Showmsg("Please select department")
        Else
            dlshow.CurrentPageIndex = 0
            txtdept.Value = cbodept.SelectedValue
            txtclient.Value = client
            txtlob.Value = lob
            bindgrid2("")
            '''new********************************************************************************

            bindClient2()
            If client = 0 Then
                cboclient.SelectedIndex = 0
            Else
                cboclient.SelectedValue = client
            End If
            bindLOB(client)
            If lob = 0 Then
                cbolob.SelectedIndex = 0
            Else
                cbolob.SelectedValue = lob
            End If

            '''end********************************************************************************
        End If
        '******************************change***********************



        dept = cbodept.SelectedValue
        If Session("typeofuser") = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", conn)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                .AddWithValue("@Deptid", dept)
                .AddWithValue("@Clientid", client)
                .AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            conn.Open()
            readerdata = cmdupdate.ExecuteReader
            If readerdata.HasRows Then
                Session("vis") = "1"
            End If
            readerdata.Close()
            conn.Close()
        End If
        '************************change***********************************
    End Sub
    Public Sub bindgrid2(ByVal sortexp)
        dept = txtdept.Value
        Dim cmdget As New SqlCommand()
        Dim uid = Session("userid")
        If (Session("typeofuser") = "Admin") Then
            Dim exist As Boolean = False
            'exist = selectRep.chkAdminSpan(Session("userid"))
            'If exist = True Then
            Dim ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (deptid in (select deptid from masteradmin where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and adminid='" + uid + "') and clientid in(select clientid from masteradmin where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and adminid='" + uid + "')   and lobid in (select lobid from masteradmin where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and adminid='" + uid + "') or (createdby='" + uid + "') or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True'))"
            ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
            cmdget = New SqlCommand(ts, connection)
            'Else
            '    GoTo adminOutofIndex
            'End If

        ElseIf (Session("typeofuser") = "Super Admin") Then
            Dim ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (createdby='" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True'))"
            ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
            cmdget = New SqlCommand(ts, connection)
        Else
adminOutofIndex:
            'Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
            Dim ts = ""
            'If (scope = "Local") Then
            'ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (createdby= '" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True' )or localcmd='Local') "
            'cmdget = New SqlCommand(ts, connection)
            '    Else
            ts = "select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "' and (createdby= '" + uid + "' or cmdid in (select cmdid from IDMS_UpdateCommandRights where userid='" + uid + "' and IDMS_UpdateCommandRights.[View]='True' and IDMS_UpdateCommandRights.[run]='True')) "
            cmdget = New SqlCommand(ts, connection)
            'End If
        End If


        'cmdget = New SqlCommand("select *,convert(varchar,CreatedOn,110) as CreateDate,convert(varchar,CreatedOn,111) as CreateDate1 from IdmsUpdateTabStruct where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LobId='" & txtlob.Value & "'", connection)
        Dim dsget As New DataSet
        Dim adpget As New SqlDataAdapter
        adpget.SelectCommand = cmdget
        connection.Open()
        adpget.Fill(dsget)
        connection.Close()
        Dim dv As DataView = New DataView(dsget.Tables(0))
        If sortexp <> "" Then
            dv.Sort = sortexp
        Else
            dv.Sort = "CmdName"
        End If
        dlshow.DataSource = dv
        dlshow.DataBind()
    End Sub
    Private Sub dlshow_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dlshow.ItemCommand
        If e.CommandName = "sortname" Then
            bindgrid("cmdName")
        End If
        If e.CommandName = "sortdate" Then
            bindgrid("CreateDate1")
        End If
        If e.CommandName = "sortby" Then
            bindgrid("CreatedBy")
        End If
        If e.CommandName = "select" Then
            Dim recid = CType(e.Item.FindControl("lblid"), Label).Text
            Response.Redirect("EditUpdStruct.aspx?val=122&recid=" & recid)
        End If
        If e.CommandName = "delete" Then
            txtrecid.Text = CType(e.Item.FindControl("lblid"), Label).Text
            txtname.Text = CType(e.Item.FindControl("lkbname"), LinkButton).Text
            pandelete.Visible = True
        End If


        '******************************change**********************************
    End Sub
    Private Sub dlshow_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dlshow.PageIndexChanged
        dlshow.CurrentPageIndex = e.NewPageIndex
        bindgrid("")
    End Sub
    Public Sub bindClient2()
        Dim cmdclient As New SqlCommand("select * from IdmsClient where deptid='" & cbodept.SelectedValue & "'", connection)
        Dim dsclient As New DataSet
        Dim adpclient As New SqlDataAdapter
        adpclient.SelectCommand = cmdclient
        connection.Open()
        adpclient.Fill(dsclient)
        connection.Close()
        cboclient.DataSource = dsclient
        cboclient.DataTextField = "ClientName"
        cboclient.DataValueField = "autoid"
        cboclient.DataBind()
        cboclient.Items.Insert(0, "--Select--")
    End Sub
    Public Sub bindLOB(ByVal client)
        Dim cmdlob As New SqlCommand("select * from warslobmaster where deptid='" & cbodept.SelectedValue & "' And ClientId='" & client & "'", connection)
        Dim dslob As New DataSet
        Dim adplob As New SqlDataAdapter
        adplob.SelectCommand = cmdlob
        connection.Open()
        adplob.Fill(dslob)
        connection.Close()
        cbolob.DataSource = dslob
        cbolob.DataTextField = "LOBName"
        cbolob.DataValueField = "autoid"
        cbolob.DataBind()
        cbolob.Items.Insert(0, "--Select--")
    End Sub

End Class
