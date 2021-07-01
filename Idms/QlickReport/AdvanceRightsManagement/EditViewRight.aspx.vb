Imports System.Data.SqlClient
Imports system.data
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ViewRightsManagement_EditViewRight
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
    '''  'Binding User to gridview on selected radio button user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged
        gvViewRight.Visible = False
        BindUserReport()
    End Sub

    Protected Sub BindUserReport()


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
            ddlUserView.DataTextField = "disname"
            ddlUserView.DataValueField = "userid"
            ddlUserView.DataSource = ds1
            ddlUserView.DataBind()
            ddlUserView.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserView.Items.Clear()
            ddlUserView.DataTextField = "disname"
            ddlUserView.DataValueField = "UserId"
            ddlUserView.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, LobId)
            ddlUserView.DataBind()
            ddlUserView.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserView.Items.Clear()
            ddlUserView.DataTextField = "disname"
            ddlUserView.DataValueField = "UserId"
            ddlUserView.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, "0")
            ddlUserView.DataBind()
            ddlUserView.Items.Insert(0, "--Select--")
            Exit Sub
        End If


        If ddlDepartment.SelectedIndex <> 0 Then
            ddlUserView.Items.Clear()
            DeptId = ddlDepartment.SelectedValue
            ddlUserView.DataTextField = "disname"
            ddlUserView.DataValueField = "UserId"
            ddlUserView.DataSource = uspan.userselectadminspan(loggedId, DeptId, "0", "0")
            ddlUserView.DataBind()
            ddlUserView.Items.Insert(0, "--Select--")
            Exit Sub
        End If



    End Sub

    ''' <summary>
    ''' 'Binding View to gridview on selected radio button user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BindViewReport()
        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department")
            Exit Sub
        End If



        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserView.Items.Clear()

            If UserType = "Admin" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewselectadminspan(loggedId, DeptId, Client_Id, LobId)
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewForuser(loggedId, DeptId, Client_Id, LobId)
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserView.Items.Clear()
            If UserType = "Admin" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewselectadminspan(loggedId, DeptId, Client_Id, "0")
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewForuser(loggedId, DeptId, Client_Id, "0")
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
                Exit Sub

            End If

        End If


        If ddlDepartment.SelectedIndex > 0 Then
            DeptId = ddlDepartment.SelectedValue
            ddlUserView.Items.Clear()
            If UserType = "Admin" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewselectadminspan(loggedId, DeptId, "0", "0")
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
            End If
            If UserType = "User" Then
                ddlUserView.DataTextField = "ViewName"
                ddlUserView.DataValueField = "ViewId"
                ddlUserView.DataSource = uspan.viewForuser(loggedId, DeptId, "0", "0")
                ddlUserView.DataBind()
                ddlUserView.Items.Insert(0, "--Select--")
            End If
        End If

    End Sub
    Protected Sub rdoView_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoView.CheckedChanged
        gvViewRight.Visible = False
        BindViewReport()


    End Sub
    ''' <summary>
    ''' bind grid 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlUserView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserView.SelectedIndexChanged

        gvViewRight.Visible = False

        If ddlUserView.SelectedIndex = 0 Then
            ShowConfirm("Select user or view")
            Exit Sub
        Else
            gvViewRight.Visible = True
            BindGridView()
            If gvViewRight.Rows.Count = 0 Then
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

        If (rdoView.Checked) Then

            BindDataByView()
        End If

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to user selected
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByUser()

        Dim userid As String
        userid = ddlUserView.SelectedValue
        gvViewRight.DataSource = objtable.bind_ViewRightsByUser(userid)
        gvViewRight.Columns(0).Visible = True
        gvViewRight.Columns(2).Visible = True
        gvViewRight.DataBind()
        gvViewRight.Columns(0).Visible = False
        'gvViewRight.Columns(2).Visible = False
    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to Table selected 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByView()

        Dim viewid As String
        viewid = ddlUserView.SelectedValue
        gvViewRight.DataSource = objtable.bind_ViewRightsByView(viewid)
        gvViewRight.Columns(0).Visible = True
        gvViewRight.Columns(2).Visible = True
        gvViewRight.DataBind()
        gvViewRight.Columns(0).Visible = False
        'gvViewRight.Columns(2).Visible = False
    End Sub
    ''' <summary>
    ''' 'Delete functionality is applied 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvViewRight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvViewRight.RowDeleting


        Dim viewid As String = gvViewRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvViewRight.Rows(e.RowIndex).Cells(2).Text
        Dim viewowner As String

        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select viewname from idmsviewmaster where viewid='" & viewid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()

        con.Open()
        Dim com As New SqlCommand("select viewid,CreatedBy from idmsviewmaster where viewid='" & viewid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            viewowner = rdr("CreatedBy").ToString()
            If (loggedId = viewowner) Or UserType = "Admin" Then
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
                Dim cmdins2 = New SqlCommand("TrackViewRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Delete Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "View")
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
                objtable.Delete_ViewRights(viewid, userid)
                BindGridView()
                con.Close()
                ShowConfirm("View Deleted Successfully")

            Else
                ShowConfirm("You are not permitted to delete rights")
            End If
        End If
    End Sub
    ''' <summary>
    ''' ' View rights Update code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvViewRight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvViewRight.RowUpdating


        Dim view As Boolean
        Dim edit As Boolean
        Dim delete As Boolean
        Dim saveas As Boolean


        Dim viewid As String = gvViewRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvViewRight.Rows(e.RowIndex).Cells(2).Text
        '************************Change*****************************************

        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select viewname from idmsviewmaster where viewid='" & viewid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()
        Dim chkview As CheckBox = CType(gvViewRight.Rows(e.RowIndex).FindControl("chkView"), CheckBox)
        Dim all As String = ""
        If chkview.Checked = True Then
            view = True
            all = "View"
        End If
        'view = chkview.Checked
        Dim chkedit As CheckBox = CType(gvViewRight.Rows(e.RowIndex).FindControl("chkEdit"), CheckBox)
        If chkedit.Checked = True Then
            edit = True
            If all = "" Then
                all = "Edit"
            Else
                all = all & "," & "Edit"
            End If
        End If
        'edit = chkedit.Checked
        Dim chkdelete As CheckBox = CType(gvViewRight.Rows(e.RowIndex).FindControl("chkDelete"), CheckBox)
        If chkdelete.Checked = True Then
            delete = True
            If all = "" Then
                all = "Delete"
            Else
                all = all & "," & "Delete"
            End If
        End If
        'delete = chkdelete.Checked
        Dim chksaveas As CheckBox = CType(gvViewRight.Rows(e.RowIndex).FindControl("chkSaveAs"), CheckBox)
        If chksaveas.Checked = True Then
            saveas = True
            If all = "" Then
                all = "Save As"
            Else
                all = all & "," & "Save As"
            End If
        End If
        'saveas = chksaveas.Checked

            Dim viewowner As String

            con = New SqlConnection(str)
            con.Open()
            Dim com As New SqlCommand("select viewid,CreatedBy from idmsviewmaster where viewid='" & viewid & "'", con)
            Dim rdr As SqlDataReader = com.ExecuteReader()
            If rdr.Read() Then
                viewowner = rdr("CreatedBy").ToString()
                If (loggedId = viewowner) Or UserType = "Admin" Then

                   
                '***************change***********************************
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
                Dim cmdins2 = New SqlCommand("TrackViewRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Edit Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "View")
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
                Else
                    ShowConfirm("You are not permitted to edit rights")
                End If
        End If
        objtable.Update_ViewRights(userid, viewid, view, edit, delete, saveas)
        gvViewRight.EditIndex = -1
        'bind grid view
        BindGridView()
        con.Close()
            '***************change*****************************************
            

            'Dim tableowner As String
            'con = New SqlConnection(str)
            'con.Open()
            'Dim com As New SqlCommand("select tableid,CreatedBy from Warslobtablemaster where tableid='" & tableid & "'", con)
            'Dim rdr As SqlDataReader = com.ExecuteReader()
            'If rdr.Read() Then
            '    tableowner = rdr("CreatedBy").ToString()
            '    If (loggedId = tableowner) Or UserType = "Admin" Then

            '        uspan.Update_TableRights(userid, tableid, view, edit, delete, deletedata, addcolumn, importdata, loggedId)

            '        gvTableRight.EditIndex = -1
            '        ''bind grid view
            '        BindGridView()
            '        con.Close()
            '        Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + tableid + "'", con)
            '        Dim tableuse As String = ""
            '        con.Open()
            '        tableuse = cmduse.executescalar()
            '        con.Close()
            '        
            '        '*************change*************

    End Sub
    ''' <summary>
    '''edit and bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvViewRight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvViewRight.RowEditing

        gvViewRight.EditIndex = e.NewEditIndex

        BindGridView()
        Dim i As Integer = e.NewEditIndex
        CType(gvViewRight.Rows(i).FindControl("chkView"), CheckBox).Enabled = True
        CType(gvViewRight.Rows(i).FindControl("chkEdit"), CheckBox).Enabled = True
        CType(gvViewRight.Rows(i).FindControl("chkDelete"), CheckBox).Enabled = True
        CType(gvViewRight.Rows(i).FindControl("chkSaveAs"), CheckBox).Enabled = True


    End Sub
    ''' <summary>
    ''' cancel edit and bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvViewRight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvViewRight.RowCancelingEdit

        gvViewRight.EditIndex = -1

        BindGridView()
    End Sub

    Protected Sub DCLHUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLHUserControl1.Load
        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            'gvViewRight.Visible = False
            'ddlUserView.Items.Clear()
        End If
    End Sub
    ''' <summary>
    ''' deisplay message
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
    ''' applying paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvViewRight_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvViewRight.PageIndexChanging
        If gvViewRight.PageIndex < gvViewRight.PageCount And gvViewRight.PageIndex >= 0 Then
            gvViewRight.PageIndex = e.NewPageIndex
            BindGridView()
        End If
    End Sub
End Class
