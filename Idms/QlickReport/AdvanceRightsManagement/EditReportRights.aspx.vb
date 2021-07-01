Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_EditReportRights
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
    ''' select data of user only like reports
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged
        gvReportRight.Visible = False

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
            ddlUserReport.DataTextField = "disname"
            ddlUserReport.DataValueField = "userid"
            ddlUserReport.DataSource = ds1
            ddlUserReport.DataBind()
            ddlUserReport.Items.Insert(0, "--Select--")
            Exit Sub
        End If


        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserReport.Items.Clear()
            ddlUserReport.DataTextField = "disname"
            ddlUserReport.DataValueField = "UserId"
            ddlUserReport.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, LobId)
            ddlUserReport.DataBind()
            ddlUserReport.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserReport.Items.Clear()
            ddlUserReport.DataTextField = "disname"
            ddlUserReport.DataValueField = "UserId"
            ddlUserReport.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, "0")
            ddlUserReport.DataBind()
            ddlUserReport.Items.Insert(0, "--Select--")
            Exit Sub
        End If


        If ddlDepartment.SelectedIndex <> 0 Then
            ddlUserReport.Items.Clear()
            DeptId = ddlDepartment.SelectedValue
            ddlUserReport.DataTextField = "disname"
            ddlUserReport.DataValueField = "UserId"
            ddlUserReport.DataSource = uspan.userselectadminspan(loggedId, DeptId, "0", "0")
            ddlUserReport.DataBind()
            ddlUserReport.Items.Insert(0, "--Select--")
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' select data of admin only like reports
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoReport.CheckedChanged
        gvReportRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department")
            Exit Sub
        End If

        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserReport.Items.Clear()
            If UserType = "Admin" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportselectadminspan(loggedId, DeptId, Client_Id, LobId)
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")
            End If
            If UserType = "User" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportForuser(loggedId, DeptId, Client_Id, LobId)
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")
            End If
            Exit Sub
        End If

        If ddlClient.SelectedIndex > 0 Then
            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserReport.Items.Clear()
            If UserType = "User" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportForuser(loggedId, DeptId, Client_Id, "0")
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")

            End If
            If UserType = "Admin" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportselectadminspan(loggedId, DeptId, Client_Id, "0")
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")
            End If
            Exit Sub
        End If

        If ddlDepartment.SelectedIndex <> 0 Then
            DeptId = ddlDepartment.SelectedValue
            ddlUserReport.Items.Clear()
            If UserType = "Admin" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportselectadminspan(loggedId, DeptId, "0", "0")
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")
            End If

            If UserType = "User" Then
                ddlUserReport.DataTextField = "QueryName"
                ddlUserReport.DataValueField = "RecordId"
                ddlUserReport.DataSource = uspan.reportForuser(loggedId, DeptId, "0", "0")
                ddlUserReport.DataBind()
                ddlUserReport.Items.Insert(0, "--Select--")
            End If
            Exit Sub
        End If
    End Sub
    ''' <summary>
    ''' bind data in grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlUserReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserReport.SelectedIndexChanged

        gvReportRight.Visible = False

        If ddlUserReport.SelectedIndex = 0 Then
            ShowConfirm("Select user or Report")
            Exit Sub
        Else
            gvReportRight.Visible = True
            BindGridView()
            If gvReportRight.Rows.Count = 0 Then
                ShowConfirm("No record found.........")
            End If
        End If

        
    End Sub
    ''' <summary>
    ''' bind data as according to the radio button 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindGridView()
        If (rdoUser.Checked) Then

            BindDataByUser()

        End If

        If (rdoReport.Checked) Then

            BindDataByReport()
        End If

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to user selected
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByUser()

        Dim userid As String
        userid = ddlUserReport.SelectedValue
        gvReportRight.DataSource = objtable.bind_ReportRightsByUser(userid)
        gvReportRight.Columns(0).Visible = True
        gvReportRight.Columns(2).Visible = True
        gvReportRight.DataBind()
        gvReportRight.Columns(0).Visible = False
        'gvReportRight.Columns(2).Visible = False
    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to Table selected 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByReport()

        Dim recordid As String
        recordid = ddlUserReport.SelectedValue
        gvReportRight.DataSource = objtable.bind_ReportRightsByReport(recordid)
        gvReportRight.Columns(0).Visible = True
        gvReportRight.Columns(2).Visible = True
        gvReportRight.DataBind()
        gvReportRight.Columns(0).Visible = False
        gvReportRight.Columns(2).Visible = True
    End Sub
    ''' <summary>
    ''' 'bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvReportRight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvReportRight.RowEditing
        gvReportRight.EditIndex = e.NewEditIndex

        BindGridView()
        Dim i As Integer = e.NewEditIndex
        CType(gvReportRight.Rows(i).FindControl("chkView"), CheckBox).Enabled = True
        CType(gvReportRight.Rows(i).FindControl("chkEdit"), CheckBox).Enabled = True
        CType(gvReportRight.Rows(i).FindControl("chkDelete"), CheckBox).Enabled = True
        CType(gvReportRight.Rows(i).FindControl("chkSaveAs"), CheckBox).Enabled = True



    End Sub
    ''' <summary>
    '''   'bind grid view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvReportRight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvReportRight.RowCancelingEdit
        gvReportRight.EditIndex = -1

        BindGridView()

    End Sub
    ''' <summary>
    '''  ' Report rights Update code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvReportRight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvReportRight.RowUpdating

        Dim view As Boolean
        Dim edit As Boolean
        Dim delete As Boolean
        Dim SaveAs As Boolean

        Dim recordid As String = gvReportRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvReportRight.Rows(e.RowIndex).Cells(2).Text
        
        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select QueryName from IDMSReportMaster where recordid='" & recordid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()
        Dim chkview As CheckBox = CType(gvReportRight.Rows(e.RowIndex).FindControl("chkView"), CheckBox)
        Dim all As String = ""
        If chkview.Checked = True Then
            view = True
            all = "View"
        End If
        'view = chkview.Checked
        Dim chkedit As CheckBox = CType(gvReportRight.Rows(e.RowIndex).FindControl("chkEdit"), CheckBox)
        If chkedit.Checked = True Then
            edit = True
            If all = "" Then
                all = "Edit"
            Else
                all = all & "," & "Edit"
            End If
        End If
        'edit = chkedit.Checked
        Dim chkdelete As CheckBox = CType(gvReportRight.Rows(e.RowIndex).FindControl("chkDelete"), CheckBox)
        If chkdelete.Checked = True Then
            delete = True
            If all = "" Then
                all = "Delete"
            Else
                all = all & "," & "Delete"
            End If
        End If
        'delete = chkdelete.Checked
        Dim chkSaveAs As CheckBox = CType(gvReportRight.Rows(e.RowIndex).FindControl("chkSaveAs"), CheckBox)
        If chkSaveAs.Checked = True Then
            SaveAs = True
            If all = "" Then
                all = "Save As"
            Else
                all = all & "," & "Save As"
            End If
        End If
        'SaveAs = chkSaveAs.Checked
        '************************************change***************************

        Dim reportowner As String
        con = New SqlConnection(str)
        con.Open()
        Dim com As New SqlCommand("select recordid,savedBy from IDMSReportMaster where recordid='" & recordid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            reportowner = rdr("savedBy").ToString()
            If (loggedId = reportowner) Or UserType = "Admin" Then
                '*********************change****************************
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
                    .AddWithValue("@Entity", "Report")
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
                ShowConfirm("Report Rights Updated Successfully")
                '*************change*************

                objtable.Update_ReportRights(userid, recordid, view, edit, delete, SaveAs)

                gvReportRight.EditIndex = -1
                'bind grid view
                BindGridView()

                con.Close()
            Else
                ShowConfirm("You are not permitted to edit rights")
            End If
        End If

    End Sub
    ''' <summary>
    ''' 'Delete functionality is applied
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvReportRight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvReportRight.RowDeleting

        Dim recordid As String = gvReportRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvReportRight.Rows(e.RowIndex).Cells(2).Text
        Dim reportowner As String


        con = New SqlConnection(str)
        Dim cmduse = New SqlCommand("select queryname from IDMSReportMaster where recordid='" & recordid & "'", con)
        Dim tableuse As String = ""
        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()
        con.Open()
        Dim com As New SqlCommand("select recordid,savedBy from IDMSReportMaster where recordid='" & recordid & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            reportowner = rdr("savedBy").ToString()
            If (loggedId = reportowner) Or UserType = "Admin" Then
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
                    .AddWithValue("@Entity", "Report")
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
                ShowConfirm("Report Rights Deleted Successfully")
                '*************change*************

                objtable.Delete_ReportRights(recordid, userid)
                BindGridView()
                con.Close()
            Else
                ShowConfirm("You are not permitted to delete rights")
            End If
        End If
        
    End Sub

    Protected Sub DCLHUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLHUserControl1.Load
        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            'ddlUserReport.Items.Clear()
            'gvReportRight.Visible = False
        End If
    End Sub
    ''' <summary>
    ''' to show message
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
    Protected Sub gvReportRight_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvReportRight.PageIndexChanging

        If gvReportRight.PageIndex < gvReportRight.PageCount And gvReportRight.PageIndex >= 0 Then
            gvReportRight.PageIndex = e.NewPageIndex
            BindGridView()
        End If
    End Sub

End Class
