Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_EditUpadateCmdRights
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")

    Dim con As New SqlConnection(str)
    Dim ddlDepartment As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlLob As DropDownList
    Dim fun As New Functions
    Dim DeptId As String
    Dim Client_Id As String
    Dim LobId As String
    Dim objtable As New TableRight
    Dim loggedId As String
    Dim UserType As String
    Dim uspan As New UserSpan


    ''' <summary>
    ''' fill the span using user control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.SmartNavigation = True

        If Session("userid") = "" Then
            Response.Redirect("~/SessionExpired.aspx")
        Else
            loggedId = Session("userid").ToString()
            UserType = Session("typeofuser").ToString()

        End If

        ddlDepartment = CType(DCLHUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLHUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLHUserControl1.FindControl("ddlLob"), DropDownList)
        If Me.IsPostBack = False Then
            'binding ddlDepartment on page load


            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

        End If

    End Sub
    ''' <summary>
    ''' bind command according to the selected user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged

        gvCmdRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            Dim str4 As String = ""
            Dim objcmd As New SqlCommand
            Dim objadp As New SqlDataAdapter
            Dim ds1 As New DataSet
            str4 = "select userid,(username +'('+userid+')') as disname  from registration where userid in(select userbuddy from buddy where userid='" + Session("userid") + "' and userbuddy<>'0')or userid in(select userid from buddy where userbuddy='" + Session("userid") + "') order by username"
            objcmd = New SqlCommand(str4, con)
            con.Open()
            objadp.SelectCommand = objcmd
            objadp.Fill(ds1)
            con.Close()
            ddlUserCmd.DataTextField = "disname"
            ddlUserCmd.DataValueField = "userid"
            ddlUserCmd.DataSource = ds1
            ddlUserCmd.DataBind()
            ddlUserCmd.Items.Insert(0, "--Select--")
            Exit Sub
        End If
        

        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserCmd.Items.Clear()
            ddlUserCmd.DataTextField = "disname"
            ddlUserCmd.DataValueField = "UserId"
            ddlUserCmd.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, LobId)
            ddlUserCmd.DataBind()
            ddlUserCmd.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserCmd.Items.Clear()
            ddlUserCmd.DataTextField = "disname"
            ddlUserCmd.DataValueField = "UserId"
            ddlUserCmd.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, "0")
            ddlUserCmd.DataBind()
            ddlUserCmd.Items.Insert(0, "--Select--")
            Exit Sub
        End If


        If ddlDepartment.SelectedIndex <> 0 Then
            ddlUserCmd.Items.Clear()
            DeptId = ddlDepartment.SelectedValue

            ddlUserCmd.DataTextField = "disname"
            ddlUserCmd.DataValueField = "UserId"
            ddlUserCmd.DataSource = uspan.userselectadminspan(loggedId, DeptId, "0", "0")
            ddlUserCmd.DataBind()
            ddlUserCmd.Items.Insert(0, "--Select--")
            Exit Sub

        End If

    End Sub
    'mail to gopaqlfsdkjfskfjsdklfsmailma

    ''' <summary>
    ''' 'Binding cmd to gridview on selected radio button user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoCmd_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoCmd.CheckedChanged
        gvCmdRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department")
            Exit Sub
        End If


        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserCmd.Items.Clear()

            If UserType = "Admin" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdselectadminspan(loggedId, DeptId, Client_Id, LobId)
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdForuser(loggedId, DeptId, Client_Id, LobId)
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
                Exit Sub
            End If

        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserCmd.Items.Clear()
            If UserType = "Admin" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdselectadminspan(loggedId, DeptId, Client_Id, "0")
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdForuser(loggedId, DeptId, Client_Id, "0")
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlDepartment.SelectedIndex <> 0 Then
            DeptId = ddlDepartment.SelectedValue
            ddlUserCmd.Items.Clear()
            If UserType = "Admin" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdselectadminspan(loggedId, DeptId, "0", "0")
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
            End If
            If UserType = "User" Then
                ddlUserCmd.DataTextField = "CmdName"
                ddlUserCmd.DataValueField = "CmdId"
                ddlUserCmd.DataSource = uspan.cmdForuser(loggedId, DeptId, "0", "0")
                ddlUserCmd.DataBind()
                ddlUserCmd.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
    ''' <summary>
    ''' bind grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlUserCmd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserCmd.SelectedIndexChanged

        gvCmdRight.Visible = False


        If ddlUserCmd.SelectedIndex = 0 Then
            ShowConfirm("Select user or Cmd")
            Exit Sub
        Else
            gvCmdRight.Visible = True
            BindGridView()
            If gvCmdRight.Rows.Count = 0 Then
                ShowConfirm("No record found.........")
            End If
        End If


    End Sub
    ''' <summary>
    ''' bind grid as according to the selected radio button
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindGridView()
        If (rdoUser.Checked) Then

            BindDataByUser()

        End If

        If (rdoCmd.Checked) Then

            BindDataByCmd()
        End If

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to user selected
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByUser()

        Dim userid As String
        userid = ddlUserCmd.SelectedValue
        gvCmdRight.DataSource = objtable.bind_CmdRightsByUser(userid)
        gvCmdRight.Columns(0).Visible = True
        gvCmdRight.Columns(2).Visible = True
        gvCmdRight.DataBind()
        gvCmdRight.Columns(0).Visible = False
        ' gvCmdRight.Columns(2).Visible = False
    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to Table selected 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByCmd()

        Dim cmdid As String
        cmdid = ddlUserCmd.SelectedValue
        gvCmdRight.DataSource = objtable.bind_CmdRightsByCmd(cmdid)
        gvCmdRight.Columns(0).Visible = True
        gvCmdRight.Columns(2).Visible = True
        gvCmdRight.DataBind()
        gvCmdRight.Columns(0).Visible = False
        'gvCmdRight.Columns(2).Visible = False
    End Sub
    ''' <summary>
    ''' edit cancle and bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvCmdRight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvCmdRight.RowCancelingEdit

        gvCmdRight.EditIndex = -1

        BindGridView()

    End Sub
    ''' <summary>
    '''   'Delete functionality is applied 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvCmdRight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvCmdRight.RowDeleting


        Dim cmdid As String = gvCmdRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvCmdRight.Rows(e.RowIndex).Cells(2).Text
        Dim cmdowner As String

        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select CmdName from idmsupdateTabStruct where cmdid='" & cmdid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()
        con.Open()
        Dim com As New SqlCommand("select cmdid,CreatedBy from idmsupdateTabStruct where cmdid='" & cmdid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            cmdowner = rdr("CreatedBy").ToString()
            If (loggedId = cmdowner) Or UserType = "Admin" Then
                '**************************change****************************
                rdr.Close()
                Dim clta As Integer
                Dim lobt As Integer
                If IsNumeric(ddlClient.SelectedValue) Then
                    clta = ddlClient.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLob.SelectedValue) Then
                    lobt = ddlLob.SelectedValue
                Else
                    lobt = 0
                End If
                Dim dept As String = ""

                If ddlDepartment.SelectedValue = "--Select--" Then
                    Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + userid + "'", con)
                    Dim datadsset As New DataSet
                    com2.Fill(datadsset)
                    If datadsset.Tables(0).Rows.Count > 0 Then
                        dept = datadsset.Tables(0).Rows(0)(0).ToString
                        clta = datadsset.Tables(0).Rows(0)(1)
                        lobt = datadsset.Tables(0).Rows(0)(2)
                    Else
                        dept = "0"
                        clta = 0
                        lobt = 0
                    End If
                Else
                    dept = ddlDepartment.SelectedValue
                End If
                Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Delete Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "Update Command")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)
                   
                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", "Delete All")
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Close()
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                '*************change*************

                objtable.Delete_CmdRights(cmdid, userid)
                BindGridView()
                con.Close()
            Else
                ShowConfirm("You are not permitted to delete rights")
            End If
        End If
    End Sub
    ''' <summary>
    ''' ' Cmd rights Update code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvCmdRight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvCmdRight.RowUpdating



        Dim view As Boolean
        Dim run As Boolean
        Dim delete As Boolean
        Dim saveas As Boolean

        Dim cmdid As String = gvCmdRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvCmdRight.Rows(e.RowIndex).Cells(2).Text

        Dim chkview As CheckBox = CType(gvCmdRight.Rows(e.RowIndex).FindControl("chkView"), CheckBox)
        Dim all As String = ""
        If chkview.Checked = True Then
            view = True
            all = "View"
        End If
        Dim chkrun As CheckBox = CType(gvCmdRight.Rows(e.RowIndex).FindControl("chkRun"), CheckBox)
        If chkrun.Checked = True Then
            run = True
            If all = "" Then
                all = "Run"
            Else
                all = all & "," & "Run"
            End If
        End If
        'run = chkrun.Checked
        Dim chkdelete As CheckBox = CType(gvCmdRight.Rows(e.RowIndex).FindControl("chkDelete"), CheckBox)
        If chkdelete.Checked = True Then
            delete = True
            If all = "" Then
                all = "Delete"
            Else
                all = all & "," & "Delete"
            End If
        End If
        'delete = chkdelete.Checked
        Dim chksaveas As CheckBox = CType(gvCmdRight.Rows(e.RowIndex).FindControl("chkSaveAs"), CheckBox)
        If chksaveas.Checked = True Then
            saveas = True
            If all = "" Then
                all = "Save As"
            Else
                all = all & "," & "Save As"
            End If
        End If
        'saveas = chksaveas.Checked
        Dim cmdowner As String

        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select CmdName from idmsupdateTabStruct where cmdid='" & cmdid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()
        con.Open()
        Dim com As New SqlCommand("select cmdid,CreatedBy from idmsupdateTabStruct where cmdid='" & cmdid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            cmdowner = rdr("CreatedBy").ToString()
            If (loggedId = cmdowner) Or UserType = "Admin" Then
                rdr.Close()
                Dim clta As Integer
                Dim lobt As Integer
                If IsNumeric(ddlClient.SelectedValue) Then
                    clta = ddlClient.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLob.SelectedValue) Then
                    lobt = ddlLob.SelectedValue
                Else
                    lobt = 0
                End If
                Dim dept As String = ""

                If ddlDepartment.SelectedValue = "--Select--" Then
                    Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + userid + "'", con)
                    Dim datadsset As New DataSet
                    com2.Fill(datadsset)
                    If datadsset.Tables(0).Rows.Count > 0 Then
                        dept = datadsset.Tables(0).Rows(0)(0).ToString
                        clta = datadsset.Tables(0).Rows(0)(1)
                        lobt = datadsset.Tables(0).Rows(0)(2)
                    Else
                        dept = "0"
                        clta = 0
                        lobt = 0
                    End If
                Else
                    dept = ddlDepartment.SelectedValue
                End If
                Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Edit Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "Update Command")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)
                    
                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", all)
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Close()
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                ShowConfirm("View Updated Successfully")
                '*************change*************
                objtable.Update_CmdRights(userid, cmdid, view, run, delete, saveas)
                gvCmdRight.EditIndex = -1
                'bind grid view
                BindGridView()

            Else
                ShowConfirm("You are not permitted to edit rights")
            End If
        End If

    End Sub
    ''' <summary>
    '''     'bind grid view and edit ready
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvCmdRight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvCmdRight.RowEditing

        gvCmdRight.EditIndex = e.NewEditIndex

        BindGridView()
        Dim i As Integer = e.NewEditIndex
        CType(gvCmdRight.Rows(i).FindControl("chkView"), CheckBox).Enabled = True
        CType(gvCmdRight.Rows(i).FindControl("chkRun"), CheckBox).Enabled = True
        CType(gvCmdRight.Rows(i).FindControl("chkDelete"), CheckBox).Enabled = True
        CType(gvCmdRight.Rows(i).FindControl("chkSaveAs"), CheckBox).Enabled = True
       
    End Sub

    Protected Sub DCLHUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLHUserControl1.Load
        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            'gvCmdRight.Visible = False
            'ddlUserCmd.Items.Clear()

        End If
    End Sub
    ''' <summary>
    ''' display messgae
    ''' </summary>
    ''' <param name="strPassed"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        Page.RegisterStartupScript("ShowConfirm", Script)
    End Function
    ''' <summary>
    ''' paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvCmdRight_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvCmdRight.PageIndexChanging

        If gvCmdRight.PageIndex < gvCmdRight.PageCount Or gvCmdRight.PageIndex >= 0 Then
            gvCmdRight.PageIndex = e.NewPageIndex
        End If
        BindGridView()
    End Sub
End Class
