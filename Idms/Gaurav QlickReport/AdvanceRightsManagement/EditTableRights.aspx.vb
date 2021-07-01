Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class EditTableRights
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
    Dim tableid As String
    Dim userid As String
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
            pandelete.Visible = False
            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

        End If
        
    End Sub

    
    ''' <summary>
    ''' 'Binding User to gridview on selected radio button user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged

        gvTableRight.Visible = False

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
            ddlUserTable.DataTextField = "disname"
            ddlUserTable.DataValueField = "userid"
            ddlUserTable.DataSource = ds1
            ddlUserTable.DataBind()
            ddlUserTable.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserTable.Items.Clear()
            ddlUserTable.DataTextField = "disname"
            ddlUserTable.DataValueField = "UserId"
            ddlUserTable.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, LobId)
            ddlUserTable.DataBind()
            ddlUserTable.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlClient.SelectedIndex > 0 Then

            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserTable.Items.Clear()
            ' ddlUserTable.DataTextField = "UserName"
            ddlUserTable.DataTextField = "disname"
            ddlUserTable.DataValueField = "UserId"
            ddlUserTable.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, "0")
            ddlUserTable.DataBind()
            ddlUserTable.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlDepartment.SelectedIndex <> 0 Then
            ddlUserTable.Items.Clear()
            DeptId = ddlDepartment.SelectedValue
            ddlUserTable.DataTextField = "disname"
            ddlUserTable.DataValueField = "UserId"
            ddlUserTable.DataSource = uspan.userselectadminspan(loggedId, DeptId, "0", "0")
            ddlUserTable.DataBind()
            ddlUserTable.Items.Insert(0, "--Select--")
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' 'Binding Tables to gridview on selecting radio button table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoTable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoTable.CheckedChanged

        gvTableRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department")
            Exit Sub
        End If

        If ddlLob.SelectedIndex > 0 Then

            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserTable.Items.Clear()

            If UserType = "Admin" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableselectadminspan(loggedId, DeptId, Client_Id, LobId)
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If

            If UserType = "User" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableForuser(loggedId, DeptId, Client_Id, LobId)
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlClient.SelectedIndex > 0 Then

            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserTable.Items.Clear()
            If UserType = "User" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableForuser(loggedId, DeptId, Client_Id, "0")
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "Admin" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableselectadminspan(loggedId, DeptId, Client_Id, "0")
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlDepartment.SelectedIndex <> 0 Then

            DeptId = ddlDepartment.SelectedValue
            ddlUserTable.Items.Clear()
            If UserType = "User" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableForuser(loggedId, DeptId, "0", "0")
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
            End If
            If UserType = "Admin" Then
                ddlUserTable.DataTextField = "TableName"
                ddlUserTable.DataValueField = "TableId"
                ddlUserTable.DataSource = uspan.tableselectadminspan(loggedId, DeptId, "0", "0")
                ddlUserTable.DataBind()
                ddlUserTable.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
    ''' <summary>
    ''' bind data into gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlUserTable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserTable.SelectedIndexChanged

        gvTableRight.Visible = False


        If ddlUserTable.SelectedIndex = 0 Then
            ShowConfirm("Select user or Table")
            Exit Sub
        Else
            gvTableRight.Visible = True
            BindGridView()
            If gvTableRight.Rows.Count = 0 Then
                ShowConfirm("No record found.........")
            End If

        End If

    End Sub
    ''' <summary>
    ''' 'bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvTableRight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvTableRight.RowEditing

        gvTableRight.EditIndex = e.NewEditIndex

        BindGridView()
        Dim i As Integer = e.NewEditIndex
        CType(gvTableRight.Rows(i).FindControl("chkView"), CheckBox).Enabled = True
        CType(gvTableRight.Rows(i).FindControl("chkEdit"), CheckBox).Enabled = True
        CType(gvTableRight.Rows(i).FindControl("chkDelete"), CheckBox).Enabled = True
        CType(gvTableRight.Rows(i).FindControl("chkDeleteData"), CheckBox).Enabled = True
        CType(gvTableRight.Rows(i).FindControl("chkAddColumn"), CheckBox).Enabled = True
        CType(gvTableRight.Rows(i).FindControl("chkImportData"), CheckBox).Enabled = True


    End Sub
    ''' <summary>
    '''     'bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvTableRight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvTableRight.RowCancelingEdit

        gvTableRight.EditIndex = -1

        BindGridView()

    End Sub
    ''' <summary>
    '''   'Delete functionality is applied 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvTableRight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvTableRight.RowDeleting


        hdtableid.Value = gvTableRight.Rows(e.RowIndex).Cells(0).Text
        hduserid.Value = gvTableRight.Rows(e.RowIndex).Cells(2).Text
        pandelete.Visible = True
        'Dim tableowner As String

        'con = New SqlConnection(str)
        'con.Open()
        'Dim com As New SqlCommand("select tableid,CreatedBy from Warslobtablemaster where tableid='" & tableid & "'", con)
        'Dim rdr As SqlDataReader = com.ExecuteReader()
        'If rdr.Read() Then
        '    tableowner = rdr("CreatedBy").ToString()
        '    If (loggedId = tableowner) Or UserType = "Admin" Then
        '        objtable.Delete_TableRights(tableid, userid)
        '        BindGridView()
        '        con.Close()
        '        Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + tableid + "'", con)
        '        Dim tableuse As String = ""
        '        con.Open()
        '        tableuse = cmduse.executescalar()
        '        con.Close()
        '        '*************change*************

        '        Dim clta As Integer
        '        Dim lobt As Integer
        '        If IsNumeric(ddlClient.SelectedValue) Then
        '            clta = ddlClient.SelectedValue
        '        Else
        '            clta = 0
        '        End If
        '        If IsNumeric(ddlLob.SelectedValue) Then
        '            lobt = ddlLob.SelectedValue
        '        Else
        '            lobt = 0
        '        End If
        '        Dim dept As String = ""

        '        If ddlDepartment.SelectedValue = "--Select--" Then
        '            Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + userid + "'", con)
        '            Dim datadsset As New DataSet
        '            com2.Fill(datadsset)
        '            If datadsset.Tables(0).Rows.Count > 0 Then
        '                dept = datadsset.Tables(0).Rows(0)(0).ToString
        '                clta = datadsset.Tables(0).Rows(0)(1)
        '                lobt = datadsset.Tables(0).Rows(0)(2)
        '            Else
        '                dept = "0"
        '                clta = 0
        '                lobt = 0
        '            End If
        '        Else
        '            dept = ddlDepartment.SelectedValue
        '        End If
        '        Dim cmdins2 = New SqlCommand("TrackRightsTable", con)
        '        cmdins2.CommandType = CommandType.StoredProcedure
        '        With cmdins2.Parameters


        '            .AddWithValue("@actionBY", loggedId)
        '            .AddWithValue("@Action", "Delete Rights")
        '            .AddWithValue("@Date", System.DateTime.Now)
        '            .AddWithValue("@AssignTo", userid)
        '            .AddWithValue("@Entity", "Table")
        '            .AddWithValue("@EntityName", tableuse)

        '            .AddWithValue("@DeptId", dept)

        '            .AddWithValue("@ClientId", clta)
        '            .AddWithValue("@LOBId", lobt)
        '            .AddWithValue("@All", "Delete All")
        '            .AddWithValue("@type", Session("usertype"))
        '        End With
        '        con.Open()
        '        cmdins2.ExecuteNonQuery()
        '        con.Close()
        '        cmdins2.Dispose()
        '        ShowConfirm("Table Rights Deleted ")
        '        '*************change*************
        '    Else
        '        ShowConfirm("Access Denied")
        '    End If
        'End If

    End Sub

    ''' <summary>
    '''   ' Table rights Update code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvTableRight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvTableRight.RowUpdating


        Dim view As Boolean = False
        Dim edit As Boolean = False
        Dim delete As Boolean = False
        Dim deletedata As Boolean = False
        Dim addcolumn As Boolean = False
        Dim importdata As Boolean = False

        Dim tableid As String = gvTableRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvTableRight.Rows(e.RowIndex).Cells(2).Text

        Dim chkview As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkView"), CheckBox)
        Dim all As String = ""
        If chkview.Checked = True Then
            view = True
            all = "View"
        End If
        Dim chkedit As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkEdit"), CheckBox)

        If chkedit.Checked = True Then
            edit = True
            If all = "" Then
                all = "Edit"
            Else
                all = all & "," & "Edit"
            End If
        End If
        Dim chkdelete As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkDelete"), CheckBox)

        If chkdelete.Checked = True Then
            delete = True
            If all = "" Then
                all = "Delete"
            Else
                all = all & "," & "Delete"
            End If
        End If
        Dim chkdeletedata As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkDeleteData"), CheckBox)

        If chkdeletedata.Checked = True Then
            deletedata = True
            If all = "" Then
                all = "Delete Data"
            Else
                all = all & "," & "Delete Data"
            End If
        End If
        Dim chkaddcolumn As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkAddColumn"), CheckBox)

        If chkaddcolumn.Checked = True Then
            addcolumn = True
            If all = "" Then
                all = "Add Column"
            Else
                all = all & "," & "Add Column"
            End If
        End If
        Dim chkimportdata As CheckBox = CType(gvTableRight.Rows(e.RowIndex).FindControl("chkImportData"), CheckBox)

        If chkimportdata.Checked = True Then
            importdata = True
            If all = "" Then
                all = "Import Data"
            Else
                all = all & "," & "Import Data"
            End If
        End If

        Dim tableowner As String
        con = New SqlConnection(str)
        con.Open()
        Dim com As New SqlCommand("select tableid,CreatedBy from Warslobtablemaster where tableid='" & tableid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            tableowner = rdr("CreatedBy").ToString()
            If (loggedId = tableowner) Or UserType = "Admin" Then

                uspan.Update_editTableRights(userid, tableid, view, edit, delete, deletedata, addcolumn, importdata, loggedId)

                gvTableRight.EditIndex = -1
                ''bind grid view
                BindGridView()
                con.Close()
                Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + tableid + "'", con)
                Dim tableuse As String = ""
                con.Open()
                tableuse = cmduse.executescalar()
                con.Close()
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
                Dim cmdins2 = New SqlCommand("TrackRightsTable", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Edit Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "Table")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)
                    
                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", all)
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                ShowConfirm("Table Rights Updated Successfully ")

                '*************change*************
            Else
                ShowConfirm("You are not permitted to edit rights")
            End If
        End If
    End Sub
    ''' <summary>
    '''    'Method  that bind data according to user selected
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByUser()


        Dim userid As String
        userid = ddlUserTable.SelectedValue
        gvTableRight.DataSource = objtable.bind_TableRightsByUser(userid)
        gvTableRight.Columns(0).Visible = True
        gvTableRight.Columns(2).Visible = True
        gvTableRight.DataBind()
        gvTableRight.Columns(0).Visible = False
        ' gvTableRight.Columns(2).Visible = False

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to Table selected 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByTable()


        Dim tableid As String
        tableid = ddlUserTable.SelectedValue
        gvTableRight.DataSource = objtable.bind_TableRightsByTable(tableid)
        gvTableRight.Columns(0).Visible = True
        gvTableRight.Columns(2).Visible = True
        gvTableRight.DataBind()
        gvTableRight.Columns(0).Visible = False
        ' gvTableRight.Columns(2).Visible = False

    End Sub
    ''' <summary>
    ''' bind data as according to the selection of radio button
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindGridView()
        If (rdoUser.Checked) Then

            BindDataByUser()

        End If

        If (rdoTable.Checked) Then

            BindDataByTable()
        End If

    End Sub
    ''' <summary>
    ''' display message
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
    ''' applying the paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvTableRight_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTableRight.PageIndexChanging
        If gvTableRight.PageIndex < gvTableRight.PageCount And gvTableRight.PageIndex >= 0 Then
            gvTableRight.PageIndex = e.NewPageIndex
            BindGridView()
        End If


    End Sub

    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        pandelete.Visible = False
        Dim tableowner As String

        con = New SqlConnection(str)
        con.Open()
        Dim com As New SqlCommand("select tableid,CreatedBy from Warslobtablemaster where tableid='" & hdtableid.Value & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            tableowner = rdr("CreatedBy").ToString()
            If (loggedId = tableowner) Or UserType = "Admin" Then
                objtable.Delete_TableRights(hdtableid.Value, hduserid.Value)
                BindGridView()
                con.Close()
                Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + hdtableid.Value + "'", con)
                Dim tableuse As String = ""
                con.Open()
                tableuse = cmduse.executescalar()
                con.Close()
                '*************change*************

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
                    Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + hduserid.Value + "'", con)
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
                Dim cmdins2 = New SqlCommand("TrackRightsTable", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Delete Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", hduserid.Value)
                    .AddWithValue("@Entity", "Table")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)

                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", "Delete All")
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                ShowConfirm("Table Rights Deleted ")
                '*************change*************
            Else
                ShowConfirm("Access Denied")
            End If
        End If

    End Sub

    Protected Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False
    End Sub
End Class
