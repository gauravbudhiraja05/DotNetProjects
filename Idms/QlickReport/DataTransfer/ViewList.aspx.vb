Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class DataTransfer_ViewList
    Inherits System.Web.UI.Page

    Dim selectRep As New ReportDesigner
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        connection.Open()
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplaymul.Visible = True
                Me.cmdshow2.Visible = True
            Else
                Me.cmdshow.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
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
            Dim cmd As SqlCommand
            If (Session("typeofuser") = "Super Admin") Then
                cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
            Else
                cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
            End If
            Dim dsar As DataSet = New DataSet()
            Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
            daar.Fill(dsar)
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
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub
    Public Sub bindClient()
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
    Private Sub cmdshow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow.Click
        Dim lob As String = 0
        Dim client As String = 0
        'cbolob.Items(cbolob.SelectedIndex).Text
        Try


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

            'If cbodept.SelectedIndex = 0 Then
            '    Showmsg("Please select department")
            'Else
            txtdept.Value = 60
            txtclient.Value = 0
            txtlob.Value = 0
            dlshow.CurrentPageIndex = 0
            bindgrid("")
            'bindClient()
            'If client = 0 Then
            '    cboclient.SelectedIndex = 0
            'Else
            '    cboclient.SelectedValue = client
            'End If
            'bindLOB(0)
            'If lob = 0 Then
            '    cbolob.SelectedIndex = 0
            'Else
            '    cbolob.SelectedValue = lob
            'End If
            'End If
        Catch ex As Exception
            Showmsg(ex.Message)
        End Try

    End Sub
    Public Sub bindgrid(ByVal sortexp)
        Dim dept = txtdept.Value
        Dim client = txtclient.Value
        Dim lob = txtlob.Value
        Dim cmdget As New SqlCommand
        Dim uid = Session("userid")
        '        If (Session("typeofuser") = "Admin") Then
        '            Dim exist As Boolean = False
        '            exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
        '            If exist = True Then
        '                Dim ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (deptid in (select deptid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "') and clientid in(select clientid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "')   and lobid in (select lobid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "') or (createdby='" + uid + "') or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True'))"
        '                ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
        '                cmdget = New SqlCommand(ts, connection)
        '            Else
        '                GoTo adminOutofIndex
        '            End If

        '        ElseIf (Session("typeofuser") = "Super Admin") Then
        '            Dim ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby='" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True'))"
        '            ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
        '            cmdget = New SqlCommand(ts, connection)
        '        Else
        'adminOutofIndex:
        '            Dim scope = Trim(selectRep.chkUserscope(Session("userid"), dept, client, lob))
        Dim ts = ""
        '            If (scope = "Local") Then
        '                ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby= '" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True' )or localview='Local') "
        '                cmdget = New SqlCommand(ts, connection)
        '            Else
        ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby= '" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True' )) "
        cmdget = New SqlCommand(ts, connection)
        '            End If
        '        End If


        ''''''''''''before '''''''
        'Dim cmdget As New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'and (createdby= '" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True' )or localview='Local')", connection)
        Dim adpget As New SqlDataAdapter
        Dim dsget As New DataSet
        adpget.SelectCommand = cmdget
        'connection.Open()
        adpget.Fill(dsget)
        'connection.Close()
        Dim dv As DataView = New DataView(dsget.Tables(0))
        If sortexp <> "" Then
            dv.Sort = sortexp
        Else
            dv.Sort = "ViewName"
        End If
        dlshow.DataSource = dv
        dlshow.DataBind()
        '''''''''''''''''''
    End Sub

    Private Sub cmdno_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False
        txtrecid.Text = ""
    End Sub

    Private Sub cmdyes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        Try
            'Dim cmddel11 As New SqlDataAdapter("select [delete] from idms_viewrights where viewid= '" & txtrecid.Text & "' and userid='" + Session("userid") + "'", connection)
            'Dim ran_ds As New DataSet
            'cmddel11.Fill(ran_ds)
            'If ran_ds.Tables(0).Rows.Count > 0 Then
            '    Dim dan As String = ran_ds.Tables(0).Rows(0)(0).ToString
            '    dan = LCase(dan)
            '    If dan <> "true" Then
            '        Showmsg("You are not authorised to delete this view.")
            '        pandelete.Visible = False
            '        Exit Sub
            '    End If
            'Else
            '    Showmsg("You are not authorised to delete this view.")
            '    pandelete.Visible = False
            '    Exit Sub
            'End If


            Dim cmddel As New SqlCommand("delete idmsviewmaster where viewid='" & txtrecid.Text & "'", connection)
            Dim cmddel1 As New SqlCommand("delete warslobtablemaster where TableName='" & txtview.Text & "'", connection)
            Dim cmddrop As New SqlCommand("drop view " & Trim(txtview.Text), connection)
            connection.Open()

            cmddel.ExecuteNonQuery()
            cmddel1.ExecuteNonQuery()
            cmddrop.ExecuteNonQuery()
            connection.Close()

            cmddel.Dispose()
            cmddel1.Dispose()
            cmddrop.Dispose()
            '''''Track changes:By Suvidha
            '''''Track changes:By Suvidha
            Dim cmd As SqlCommand
            cmd = New SqlCommand("DataTransferOnIDMSViewMaster", connection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ActionBy", SqlDbType.VarChar, 100).Value = Session("userid")
            cmd.Parameters.Add("@Action", SqlDbType.VarChar, 50).Value = "Delete"
            cmd.Parameters.Add("@Date", SqlDbType.VarChar, 50).Value = System.DateTime.Now()
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar, 50).Value = "View"
            cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 50).Value = txtview.Text
            cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50).Value = txtdept.Value
            cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50).Value = txtclient.Value
            cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50).Value = txtlob.Value
            connection.Open()
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            connection.Close()
            '''''Track changes:By Suvidha
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmm As New SqlCommand("insert into datatransfer_utype select MAX(Autoid)," + Session("usertype") + " from logDataTransferMaster where EntityName='" & txtview.Text & "' and Action='Delete'", connection)
            'connection.Open()
            'cmm.ExecuteNonQuery()
            'connection.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
        Catch ex As Exception
            Dim msg As String
            msg = Replace(ex.Message.ToString, "'", "")
            'Showmsg(msg)
            Showmsg("View has been deleted successfully.")
        End Try
        Dim lob As String = 0
        Dim client As String = 0
        'If Request("cbolob2") = "" Or Request("cbolob2") = "--Select--" Then
        '    lob = 0
        'Else
        '    lob = Request("cbolob2")
        'End If
        'If Request("cboclient2") = "" Or Request("cboclient2") = "--Select--" Then
        '    client = 0
        'Else
        '    client = Request("cboclient2")
        'End If
        dlshow.CurrentPageIndex = 0
        bindgrid("")
        pandelete.Visible = False
        txtrecid.Text = ""
        Showmsg("View has been deleted successfully.")
    End Sub

    Private Sub dlshow_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dlshow.ItemCommand
        If e.CommandName = "sortname" Then
            bindgrid("ViewName")
        End If
        If e.CommandName = "sortdate" Then
            bindgrid("CreatedDate1")
        End If
        If e.CommandName = "sortby" Then
            bindgrid("CreatedBy")
        End If
        If e.CommandName = "select" Then
            Dim recid = CType(e.Item.FindControl("lblid"), Label).Text
            Response.Redirect("EditView.aspx?val=122&recid=" & recid)
        End If
        Try
            If e.CommandName = "delete" Then
                txtrecid.Text = CType(e.Item.FindControl("lblid"), Label).Text
                txtview.Text = CType(e.Item.FindControl("lkbname"), LinkButton).Text

                'If Session("typeofuser") = "User" Then
                connection.Open()
                Dim cmdchkRight As New SqlCommand("select [delete] from idms_viewrights where viewid= '" & txtrecid.Text & " ' and userid='" & Session("userid") & "'", connection)
                Dim rdrchk As SqlDataReader = cmdchkRight.ExecuteReader()
                'If rdrchk.Read() Then
                connection.Close()
                Dim cmdchk As New SqlCommand("select * from idmsquerymaster where CharIndex(',' + tablename + ',',',' + '" & txtview.Text & "' + ',') > 0", connection)
                Dim drchk As SqlDataReader
                connection.Open()
                drchk = cmdchk.ExecuteReader
                If drchk.Read Then
                    Showmsg("Sorry! you can not delete this view because this view used in some report.")
                    txtrecid.Text = ""
                Else
                    pandelete.Visible = True
                End If
                drchk.Close()
                connection.Close()
                cmdchk.Dispose()
                'Else
                '    Showmsg("You are not Authorised to Delete this View.")
                'End If
                'End If
                'If Session("typeofuser") = "Admin" Then
                '    Dim cmdchk As New SqlCommand("select * from idmsquerymaster where CharIndex(',' + tablename + ',',',' + '" & txtview.Text & "' + ',') > 0", connection)
                '    Dim drchk As SqlDataReader
                '    connection.Open()
                '    drchk = cmdchk.ExecuteReader
                '    If drchk.Read Then
                '        Showmsg("Sorry! you can not delete this view because this view used in some report.")
                '        txtrecid.Text = ""
                '    Else
                '        pandelete.Visible = True
                '    End If
                '    drchk.Close()
                '    connection.Close()
                '    cmdchk.Dispose()
                'End If

            End If
        Catch ex As Exception
            Showmsg(ex.Message)
        End Try

    End Sub

    Private Sub dlshow_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dlshow.PageIndexChanged
        dlshow.CurrentPageIndex = e.NewPageIndex
        bindgrid("")
    End Sub
    Private Sub cmdshow2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdshow2.Click
        Dim lob
        Dim client
        'cbolob.Items(cbolob.SelectedIndex).Text
        Try


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
                txtdept.Value = cbodept.SelectedValue
                txtclient.Value = client
                txtlob.Value = lob
                dlshow.CurrentPageIndex = 0
                bindgrid2("")
                bindClient()
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
            End If
        Catch ex As Exception
            Showmsg(ex.Message)
        End Try

    End Sub
    Public Sub bindgrid2(ByVal sortexp)
        Dim dept = txtdept.Value
        Dim client = txtclient.Value
        Dim lob = txtlob.Value
        Dim cmdget As New SqlCommand
        Dim uid = Session("userid")
        If (Session("typeofuser") = "Admin") Then
            Dim exist As Boolean = False
            'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
            'If exist = True Then
            Dim ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (deptid in (select deptid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "') and clientid in(select clientid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "')   and lobid in (select lobid from masteradmin where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and adminid='" + uid + "') or (createdby='" + uid + "') or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True'))"
            ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
            cmdget = New SqlCommand(ts, connection)
            'Else
            '    GoTo adminOutofIndex
            'End If

        ElseIf (Session("typeofuser") = "Super Admin") Then
            Dim ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby='" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True'))"
            ' cmdget = New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
            cmdget = New SqlCommand(ts, connection)
        Else
adminOutofIndex:
            'Dim scope = Trim(selectRep.chkUserscope(Session("userid"), dept, client, lob))
            Dim ts = ""
            'If (scope = "Local") Then
            '    ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby= '" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True' )or localview='Local') "
            '    cmdget = New SqlCommand(ts, connection)
            'Else
            ts = "select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where deptid='" + dept + "' and clientid='" + client + "' and lobid='" + lob + "' and (createdby= '" + uid + "' or viewid in (select viewid from idms_viewrights where userid='" + uid + "' and idms_viewrights.[View]='True' )) "
            cmdget = New SqlCommand(ts, connection)
            'End If
        End If


        ''''''''''''before '''''''
        'Dim cmdget As New SqlCommand("select *,convert(varchar,CreateDate,110) as CreatedOn,convert(varchar,CreateDate,111) as CreatedDate1 from idmsviewmaster where DeptId='" & txtdept.Value & "' and ClientId='" & txtclient.Value & "' and LOBId='" & txtlob.Value & "'", connection)
        Dim adpget As New SqlDataAdapter
        Dim dsget As New DataSet
        adpget.SelectCommand = cmdget
        'connection.Open()
        adpget.Fill(dsget)
        'connection.Close()
        Dim dv As DataView = New DataView(dsget.Tables(0))
        If sortexp <> "" Then
            dv.Sort = sortexp
        Else
            dv.Sort = "ViewName"
        End If
        dlshow.DataSource = dv
        dlshow.DataBind()
        '''''''''''''''''''
    End Sub
End Class
