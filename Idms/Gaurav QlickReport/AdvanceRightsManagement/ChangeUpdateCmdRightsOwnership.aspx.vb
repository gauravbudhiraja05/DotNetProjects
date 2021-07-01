Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ChangeUpdateCmdRightsOwnership
    Inherits System.Web.UI.Page


    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim conn As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartmentCmd As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClientCmd As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLobCmd As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim tableobj As New TableRight
    Dim UserId As String
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
        ddlDepartmentUser = CType(DCLUserControl2.FindControl("ddlDepartment"), DropDownList)
        ddlClientUser = CType(DCLUserControl2.FindControl("ddlClient"), DropDownList)
        ddlLobUser = CType(DCLUserControl2.FindControl("ddlLob"), DropDownList)

        ddlDepartmentCmd = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClientCmd = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLobCmd = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load


            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "DeptID"
            ddlDepartmentUser.DataSource = fun.bind_Dept()
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "--Select--")

            ddlDepartmentCmd.DataTextField = "DepartmentName"
            ddlDepartmentCmd.DataValueField = "DeptID"
            ddlDepartmentCmd.DataSource = fun.bind_Dept()
            ddlDepartmentCmd.DataBind()
            ddlDepartmentCmd.Items.Insert(0, "--Select--")

        End If

    End Sub
    ''' <summary>
    ''' ' Method to bind users according to selection
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindUser()
        If ddlDepartmentUser.SelectedIndex = 0 Then


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
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "userid"
            ddlNewOwner.DataSource = ds1
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlLobUser.SelectedIndex > 0 And ddlClientUser.SelectedIndex > 0 And ddlDepartmentUser.SelectedIndex > 0 Then

            lobid = Convert.ToInt32(ddlLobUser.SelectedValue)
            client_id = Convert.ToInt32(ddlClientUser.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentUser.SelectedValue)
            ddlNewOwner.DataSource = uspan.userselectadminspan(loggedId, deptid, client_id, lobid)
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "UserId"
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlDepartmentUser.SelectedIndex > 0 And ddlClientUser.SelectedIndex > 0 Then

            client_id = Convert.ToInt32(ddlClientUser.SelectedValue)
            deptid = ddlDepartmentUser.SelectedValue.ToString()
            ddlNewOwner.DataSource = uspan.userselectadminspan(loggedId, deptid, client_id, "0")
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "UserId"
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlDepartmentUser.SelectedIndex > 0 Then

            deptid = ddlDepartmentUser.SelectedValue
            ddlNewOwner.DataSource = uspan.userselectadminspan(loggedId, deptid, "0", "0")
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "UserId"
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")

        End If
    End Sub
    ''' <summary>
    '''  to bind the command
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindCmd()
        If ddlLobCmd.SelectedIndex > 0 And ddlClientCmd.SelectedIndex > 0 And ddlDepartmentCmd.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLobCmd.SelectedValue)
            client_id = Convert.ToInt32(ddlClientCmd.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentCmd.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectCmd.DataSource = uspan.cmdselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlSelectCmd.DataSource = uspan.bind_lobCmdUser(deptid, client_id, lobid)
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
                Exit Sub

            End If
            
        End If

        If ddlClientCmd.SelectedIndex > 0 And ddlDepartmentCmd.SelectedIndex > 0 Then
            client_id = Convert.ToInt32(ddlClientCmd.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentCmd.SelectedValue)

            If UserType = "User" Then
                ddlSelectCmd.DataSource = uspan.bind_clientCmdUser(deptid, client_id)
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
            End If
            If UserType = "Admin" Then
                ddlSelectCmd.DataSource = uspan.cmdselectadminspan(loggedId, deptid, client_id, "0")
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
            End If
            
        End If


        If ddlDepartmentCmd.SelectedIndex > 0 Then
            deptid = ddlDepartmentCmd.SelectedValue

            If UserType = "User" Then
                ddlSelectCmd.DataSource = uspan.bind_DeparmentCmdUser(deptid)
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
            End If
            If UserType = "Admin" Then
                ddlSelectCmd.DataSource = uspan.cmdselectadminspan(loggedId, deptid, "0", "0")
                ddlSelectCmd.DataTextField = "CmdName"
                ddlSelectCmd.DataValueField = "CmdId"
                ddlSelectCmd.DataBind()
                ddlSelectCmd.Items.Insert(0, "--Select--")
            End If
        End If

        Dim i As Integer = ddlDepartmentCmd.SelectedIndex

    End Sub
    ''' <summary>
    ''' 'method called to bind users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click
        BindUser() 'method called to bind users
    End Sub

    
    Protected Sub ddlSelectCmd_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectCmd.SelectedIndexChanged
        Dim selectedcmd As String
        Dim ds As New DataSet
        If ddlSelectCmd.SelectedIndex <> -1 And ddlSelectCmd.SelectedIndex <> 0 Then
            selectedcmd = ddlSelectCmd.SelectedValue
            ds = tableobj.Get_CmdOwner(selectedcmd)
            tbxPreviousOwner.Text = ds.Tables(0).Rows(0)("UserName").ToString()
            hdUserId.Value = ds.Tables(0).Rows(0)("UserId").ToString()
        Else
            tbxPreviousOwner.Text = ""
            hdUserId.Value = ""
        End If
    End Sub
    ''' <summary>
    ''' 'Method to bind cmds
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUpdateCommnad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUpdateCommnad.Click
        BindCmd() 'Method to bind cmds
    End Sub
    ''' <summary>
    ''' change the owner of command
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click


        'validating values
        '-------------------------------------------------------------------

        If ddlDepartmentCmd.SelectedIndex = 0 Then
            ShowConfirm("Select department to choose update command")
            Exit Sub
        End If
        If (ddlSelectCmd.SelectedIndex = -1) Or (ddlSelectCmd.SelectedIndex = 0) Then
            ShowConfirm("Select update command")
            Exit Sub
        End If
        'If ddlDepartmentUser.SelectedIndex = 0 Then
        '    ShowConfirm("Select department to choose new owner")
        '    Exit Sub
        'End If
        If (ddlNewOwner.SelectedIndex = -1) Or (ddlNewOwner.SelectedIndex = 0) Then
            ShowConfirm("Select new owner")
            Exit Sub
        End If
        '------------------------------------------------------------






        If Session("typeofuser") = "Admin" Then

            Dim CmdId As String = ddlSelectCmd.SelectedValue
            Dim Cmdname As String = ddlSelectCmd.SelectedItem.Text
            Dim newOwnerId As String = ddlNewOwner.SelectedValue
            '*************change*************


            Dim cmdins2 = New SqlCommand("TrackCmdOwnerchange", conn)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters


                .AddWithValue("@cmdid", CmdId)

                .AddWithValue("@actionby", Session("userid"))
                .AddWithValue("@action", "Change Owner")
                .AddWithValue("@date", System.DateTime.Now)

                .AddWithValue("@entity", "Update Command")
                .AddWithValue("@entityname", Cmdname)

                .AddWithValue("@deptid", ddlDepartmentCmd.SelectedValue)
                Dim clta As Integer
                Dim lobt As Integer

               
                If IsNumeric(ddlClientCmd.SelectedValue) Then
                    clta = ddlClientCmd.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLobCmd.SelectedValue) Then
                    lobt = ddlLobCmd.SelectedValue
                Else
                    lobt = 0
                End If
                .AddWithValue("@clientid", clta)
                .AddWithValue("@lobid", lobt)
                .AddWithValue("@assignto", newOwnerId)
                .AddWithValue("@type", Session("usertype"))
            End With

            conn.Open()
            cmdins2.ExecuteNonQuery()
            conn.Close()
            cmdins2.Dispose()
            '*************change*************
            Dim oldowner = hdUserId.Value
            tableobj.Update_UpdateCommandOwnership(CmdId, Cmdname, newOwnerId, oldowner)
            ShowConfirm("Update command ownership changed successfully")
            ClearAll()

        Else
            ShowConfirm("You are not authorised")
        End If
    End Sub
    ''' <summary>
    ''' clear the controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlClientCmd.Items.Clear()
        ddlLobCmd.Items.Clear()
        ddlNewOwner.Items.Clear()
        ddlSelectCmd.Items.Clear()
        tbxPreviousOwner.Text = ""
        ddlDepartmentUser.SelectedIndex = 0
        ddlDepartmentCmd.SelectedIndex = 0
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
End Class
