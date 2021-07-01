Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_AssignUpdateCommandRights
    Inherits System.Web.UI.Page
    Dim str As String = AppSettings("ConnectionString")
    Dim str1 As String = AppSettings("ConnectionString1")
    Dim con As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartment As DropDownList
    Dim ddlDepartmentCmd As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlClientCmd As DropDownList
    Dim ddlLob As DropDownList
    Dim ddlLobCmd As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim loggedId As String
    Dim UserType As String
    Dim objUpdate As New UpdateItem
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

        ddlDepartment = CType(DCLUserControl3.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLUserControl3.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLUserControl3.FindControl("ddlLob"), DropDownList)

        ddlDepartmentCmd = CType(DCLUserControl4.FindControl("ddlDepartment"), DropDownList)
        ddlClientCmd = CType(DCLUserControl4.FindControl("ddlClient"), DropDownList)
        ddlLobCmd = CType(DCLUserControl4.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load


            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

            ddlDepartmentCmd.DataTextField = "DepartmentName"
            ddlDepartmentCmd.DataValueField = "DeptID"
            ddlDepartmentCmd.DataSource = fun.bind_Dept()
            ddlDepartmentCmd.DataBind()
            ddlDepartmentCmd.Items.Insert(0, "--Select--")

        End If


        Me.Form.DefaultButton = Me.btnAssignRight.UniqueID
    End Sub
    ''' <summary>
    ''' 'Method to bind user list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindUser()
        'Method to bind user according to selection
        If ddlDepartment.SelectedIndex > 0 Then
            gvUsers.Visible = True
            If ddlLob.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLob.SelectedValue)
            Else
                lobid = "0"
            End If
            If ddlClient.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ddlClient.SelectedValue)
            Else
                client_id = "0"
            End If

            deptid = Convert.ToInt32(ddlDepartment.SelectedValue)
            gvUsers.Columns(2).Visible = True
            If Session("typeofuser") = "Admin" Then
                Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
                cmdupdate.CommandType = CommandType.StoredProcedure
                With cmdupdate.Parameters
                    .AddWithValue("@userid", Session("userid"))
                    .AddWithValue("@Deptid", deptid)
                    .AddWithValue("@Clientid", client_id)
                    .AddWithValue("@LOBID", lobid)
                End With
                Dim readerdata As SqlDataReader
                con.Open()
                readerdata = cmdupdate.ExecuteReader


                If readerdata.HasRows Then
                    gvUsers.DataSource = uspan.bind_userbuddyAdmin(loggedId, deptid, client_id, lobid)
                    gvUsers.DataBind()
                Else
                    gvUsers.DataSource = Nothing
                    gvUsers.DataBind()
                End If
                readerdata.Close()
                con.Close()
            ElseIf Session("typeofuser") = "User" Then
                gvUsers.DataSource = uspan.bind_userbuddy(loggedId, deptid, client_id, lobid)
                gvUsers.DataBind()
            End If
        Else
            Dim str4 As String = ""
            Dim objcmd As New SqlCommand
            Dim objadp As New SqlDataAdapter
            Dim ds1 As New DataSet
            str4 = "select userid, username from registration where userid in(select userbuddy from buddy where userid='" + Session("userid") + "' and userbuddy<>'0')or userid in(select userid from buddy where userbuddy='" + Session("userid") + "') order by username"
            objcmd = New SqlCommand(str4, con)
            con.Open()
            objadp.SelectCommand = objcmd
            objadp.Fill(ds1)
            con.Close()
            gvUsers.DataSource = ds1
            gvUsers.DataBind()

        End If
    End Sub

    ''' <summary>
    ''' 'Method to bind cmd list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindCmd()
        If ddlDepartmentCmd.SelectedIndex <> 0 Then
            gvUpdateCommand.Visible = True

            If ddlLobCmd.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLobCmd.SelectedValue)
            Else
                lobid = "0"
            End If
            If ddlClientCmd.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ddlClientCmd.SelectedValue)
            Else
                client_id = "0"
            End If
            deptid = Convert.ToInt32(ddlDepartmentCmd.SelectedValue)
            If UserType = "Admin" Then
                gvUpdateCommand.Columns(2).Visible = True
                gvUpdateCommand.DataSource = uspan.cmdselectadminspan(loggedId, deptid, client_id, lobid)
                gvUpdateCommand.DataBind()
                gvUpdateCommand.Columns(2).Visible = False
                Exit Sub
            End If

            If UserType = "User" Then
                gvUpdateCommand.Columns(2).Visible = True
                gvUpdateCommand.DataSource = uspan.cmdForuser(loggedId, deptid, client_id, lobid)
                gvUpdateCommand.DataBind()
                gvUpdateCommand.Columns(2).Visible = False
                Exit Sub
            End If


        End If
    End Sub

    ''' <summary>
    ''' ' call for BindUser()
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click


        BindUser()
        If gvUsers.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If


    End Sub
    ''' <summary>
    '''  ' call for BindCmd()
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCmd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCmd.Click
       
        BindCmd()

        If gvUpdateCommand.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If
        btnAssignRight.Visible = True

    End Sub
    ''' <summary>
    ''' assign the update command rights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAssignRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssignRight.Click
        Dim i As Integer
        Dim j As Integer
        Dim UserId(20) As String
        Dim CmdId(20) As String
        Dim view(20) As String

        Dim Run(20) As String
        Dim isUserSelected As Boolean
        Dim delete(20) As String
        Dim SaveAs(20) As String
        Dim isCmdSelected As Boolean
        Dim selection As CheckBox
        Dim viewRight As CheckBox
        Dim runRight As CheckBox
        Dim deleteRight As CheckBox
        Dim saveAsRight As CheckBox
        Dim assignedby As String
        Static count As Integer


        'checking department for user is selected or not
        'If (ddlDepartment.SelectedIndex = 0) Then
        '    ShowConfirm("Please select department to choose user ")
        '    Exit Sub
        'End If
        If (ddlDepartmentCmd.SelectedIndex = 0) Then
            ShowConfirm("Please select department to choose Cmd ")
            Exit Sub
        End If

        'storing userid informations in variables
        isUserSelected = False
        For i = 0 To gvUsers.Rows.Count - 1
            selection = CType(gvUsers.Rows(i).FindControl("chkselectuser"), CheckBox)

            If selection.Checked = True Then
                UserId(i) = gvUsers.Rows(i).Cells(2).Text
                str = UserId(i)
                isUserSelected = True
            End If
        Next

        If (isUserSelected = False) Then
            ShowConfirm("Please select User ")
            Exit Sub
        End If

        'storing view rights informations in variables
        isCmdSelected = False

        For i = 0 To gvUpdateCommand.Rows.Count - 1

            selection = CType(gvUpdateCommand.Rows(i).FindControl("chkCmd"), CheckBox)
            If (selection.Checked = True) Then

                isCmdSelected = True
                viewRight = CType(gvUpdateCommand.Rows(i).FindControl("chkView"), CheckBox)
                runRight = CType(gvUpdateCommand.Rows(i).FindControl("chkRun"), CheckBox)
                deleteRight = CType(gvUpdateCommand.Rows(i).FindControl("chkDelete"), CheckBox)
                saveAsRight = CType(gvUpdateCommand.Rows(i).FindControl("chkSaveAs"), CheckBox)


                CmdId(i) = gvUpdateCommand.Rows(i).Cells(2).Text
                If (viewRight.Checked = True) Then
                    view(i) = True
                Else
                    view(i) = False
                    count = count + 1
                End If

                If (runRight.Checked = True) Then
                    Run(i) = True
                Else
                    Run(i) = False
                    count = count + 1
                End If

                If (deleteRight.Checked = True) Then
                    delete(i) = True

                Else
                    delete(i) = False
                    count = count + 1
                End If

                If (saveAsRight.Checked = True) Then
                    SaveAs(i) = True
                Else
                    SaveAs(i) = False
                    count = count + 1
                End If
            End If
            If count = 4 Then
                ShowConfirm("Select Rights on selected Cmds")
                Exit Sub
            Else
                count = 0
            End If
        Next
        If (isCmdSelected = False) Then
            ShowConfirm("Please select Cmd ")
            Exit Sub
        End If
        assignedby = loggedId
        'calling function for storing value in database
        'to stop duplicate values



        For i = 0 To UserId.Length - 1
            If UserId(i) <> "" Then
                For j = 0 To CmdId.Length - 1
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
                        Dim com As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + UserId(i) + "'", con)
                        Dim datadsset As New DataSet
                        com.Fill(datadsset)
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
                    If CmdId(j) <> "" Then
                        con.Open()
                        Dim comDup As New SqlCommand("select cmdid,userid from IDMS_UpdateCommandRights where cmdid='" & CmdId(j) & "' and userid ='" & UserId(i) & "'", con)
                        Dim rdrDup As SqlDataReader = comDup.ExecuteReader()
                        If rdrDup.Read() Then
                            objUpdate.Update_CmdRight(UserId(i), CmdId(j), view(j), Run(j), delete(j), SaveAs(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                            If Run(j) = True Then
                                If all = "" Then
                                    all = "Run"
                                Else
                                    all = all & "," & "Run"
                                End If
                            End If
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                            If SaveAs(j) = True Then
                                If all = "" Then
                                    all = "Save As"
                                Else
                                    all = all & "," & "Save As"
                                End If
                            End If
                            con.Close()
                            Dim cmduse = New SqlCommand("select CmdName from idmsupdateTabStruct where cmdid='" + CmdId(j) + "'", con)
                            Dim tableuse As String = ""
                            con.Open()
                            tableuse = cmduse.executescalar()
                            con.Close()
                            Dim cmdins2 = New SqlCommand("TrackViewRights", con)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters


                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)
                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Update Command")
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
                            '*************change*************

                        Else
                            fun.Insert_CmdRights(UserId(i), CmdId(j), view(j), Run(j), delete(j), SaveAs(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                            If Run(j) = True Then
                                If all = "" Then
                                    all = "Run"
                                Else
                                    all = all & "," & "Run"
                                End If
                            End If
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                            If SaveAs(j) = True Then
                                If all = "" Then
                                    all = "Save As"
                                Else
                                    all = all & "," & "Save As"
                                End If
                            End If
                            con.Close()
                            Dim cmduse = New SqlCommand("select CmdName from idmsupdateTabStruct where cmdid='" + CmdId(j) + "'", con)
                            Dim tableuse As String = ""
                            con.Open()
                            tableuse = cmduse.executescalar()
                            con.Close()
                            Dim cmdins2 = New SqlCommand("TrackViewRights", con)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters


                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)
                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Update Command")
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
                            '*************change*************

                        End If

                        con.Close()

                    End If
                Next

            End If
        Next
        'Confirmation of success
        ShowConfirm("Rights assigned successfully")
        ClearAll()
    End Sub

    Protected Sub gvUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUsers.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:Select('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If


    End Sub

    Protected Sub gvUpdateCommand_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUpdateCommand.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:SelectAll('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If

    End Sub
    ''' <summary>
    ''' paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUsers.PageIndexChanging
        If gvUsers.PageIndex < gvUsers.PageCount And gvUsers.PageIndex >= 0 Then

            gvUsers.PageIndex = e.NewPageIndex

            BindUser()      'Method called to bind user
        End If

    End Sub
    ''' <summary>
    ''' paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub gvUpdateCommand_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUpdateCommand.PageIndexChanging
        If gvUpdateCommand.PageIndex < gvUpdateCommand.PageCount And gvUpdateCommand.PageIndex >= 0 Then
            gvUpdateCommand.PageIndex = e.NewPageIndex
            BindCmd()
        End If
    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClient.ClearSelection()

        ddlLob.ClearSelection()
        ddlClientCmd.ClearSelection()
        ddlLobCmd.ClearSelection()
        'gvUsers.Visible = False
        gvUsers.DataSource = Nothing
        gvUsers.DataBind()

        ddlDepartment.ClearSelection()
        ddlDepartmentCmd.ClearSelection()
        'gvUpdateCommand.Visible = False
        gvUpdateCommand.DataSource = Nothing
        gvUpdateCommand.DataBind()
        btnAssignRight.Visible = False
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
    ''' clear ddlLob
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl3.Load
        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()

        End If

        If ddlClient.SelectedIndex = 0 Then
            ddlLob.Items.Clear()
        End If


    End Sub
    ''' <summary>
    ''' clear ddlLobCmd
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl4.Load
        If ddlDepartmentCmd.SelectedIndex = 0 Then
            ddlClientCmd.Items.Clear()
            ddlLobCmd.Items.Clear()
        End If

        If ddlClientCmd.SelectedIndex = 0 Then
            ddlLobCmd.Items.Clear()
        End If



    End Sub
End Class
