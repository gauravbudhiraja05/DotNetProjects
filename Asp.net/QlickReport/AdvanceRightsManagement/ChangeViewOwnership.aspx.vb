Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ViewRightsManagement_ChangeViewOwner
    Inherits System.Web.UI.Page


    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartmentView As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClientview As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLobView As DropDownList
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

        ddlDepartmentView = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClientview = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLobView = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load

            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "DeptID"
            ddlDepartmentUser.DataSource = fun.bind_Dept()
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "--Select--")

            ddlDepartmentView.DataTextField = "DepartmentName"
            ddlDepartmentView.DataValueField = "DeptID"
            ddlDepartmentView.DataSource = fun.bind_Dept()
            ddlDepartmentView.DataBind()
            ddlDepartmentView.Items.Insert(0, "--Select--")


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
    ''' bind view
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindView()
        If ddlLobView.SelectedIndex > 0 And ddlClientview.SelectedIndex > 0 And ddlDepartmentView.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLobView.SelectedValue)
            client_id = Convert.ToInt32(ddlClientview.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentView.SelectedValue)
            If UserType = "Admin" Then
                ddlSelectView.DataSource = uspan.viewselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlSelectView.DataSource = uspan.bind_lobViewUser(deptid, client_id, lobid)
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            
        End If

        If ddlClientview.SelectedIndex > 0 And ddlDepartmentView.SelectedIndex > 0 Then
            client_id = Convert.ToInt32(ddlClientview.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentView.SelectedValue)
            If UserType = "Admin" Then
                ddlSelectView.DataSource = uspan.viewselectadminspan(loggedId, deptid, client_id, "0")
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "User" Then
                ddlSelectView.DataSource = uspan.bind_clientViewUser(deptid, client_id)
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If


        If ddlDepartmentView.SelectedIndex > 0 Then
            deptid = ddlDepartmentView.SelectedValue
            If UserType = "Admin" Then
                ddlSelectView.DataSource = uspan.viewselectadminspan(loggedId, deptid, "0", "0")
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")

            End If
            If UserType = "User" Then
                ddlSelectView.DataSource = uspan.bind_DeparmentViewUser(deptid)
                ddlSelectView.DataTextField = "ViewName"
                ddlSelectView.DataValueField = "ViewId"
                ddlSelectView.DataBind()
                ddlSelectView.Items.Insert(0, "--Select--")
            End If
        End If
        Dim i As Integer = ddlDepartmentView.SelectedIndex

    End Sub
    ''' <summary>
    '''  'method called to bind users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click
        'If ddlDepartmentUser.SelectedIndex = 0 Then
        '    ShowConfirm("Select Department first")
        '    Exit Sub
        'End If
        BindUser() 'method called to bind users
    End Sub
    ''' <summary>
    ''' 'method called to bind views
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetView.Click
        If ddlDepartmentView.SelectedIndex = 0 Then
            ShowConfirm("Select Department first")
            Exit Sub
        End If

        BindView()  'method called to bind views

    End Sub
    ''' <summary>
    ''' change the view owner
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click


        'validating values
        '-------------------------------------------------------------------

        If ddlDepartmentView.SelectedIndex = 0 Then
            ShowConfirm("Select department to choose view")
            Exit Sub
        End If
        If (ddlSelectView.SelectedIndex = -1) Or (ddlSelectView.SelectedIndex = 0) Then
            ShowConfirm("Select View")
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
        '*************change*************
        Dim ViewId As String = ddlSelectView.SelectedValue
        Dim Viewname As String = ddlSelectView.SelectedItem.Text
        Dim newOwnerId As String = ddlNewOwner.SelectedValue

        Dim cmdins2 = New SqlCommand("TracktableOwnerchange", con)
        cmdins2.CommandType = CommandType.StoredProcedure
        With cmdins2.Parameters


            .AddWithValue("@tableid", ViewId)

            .AddWithValue("@actionby", Session("userid"))
            .AddWithValue("@action", "Change Owner")
            .AddWithValue("@date", System.DateTime.Now)

            .AddWithValue("@entity", "View")
            .AddWithValue("@entityname", Viewname)

            .AddWithValue("@deptid", ddlDepartmentView.SelectedValue)
            Dim clta As Integer
            Dim lobt As Integer

            If IsNumeric(ddlClientview.SelectedValue) Then
                clta = ddlClientview.SelectedValue
            Else
                clta = 0
            End If
            If IsNumeric(ddlLobView.SelectedValue) Then
                lobt = ddlLobView.SelectedValue
            Else
                lobt = 0
            End If
            .AddWithValue("@clientid", clta)
            .AddWithValue("@lobid", lobt)
            .AddWithValue("@assignto", newOwnerId)
            .AddWithValue("@type", Session("usertype"))
        End With
        con.Open()
        cmdins2.ExecuteNonQuery()
        con.Close()
        cmdins2.Dispose()
        '*************change*************


        

        If Session("typeofuser") = "Admin" Then

            Dim changedby As String = "admin"
            Dim oldowner = hdUserId.Value
            tableobj.Update_ViewOwnership(ViewId, Viewname, newOwnerId, oldowner)
            ShowConfirm("View ownership changed successfully")
            ClearAll()


        Else
            fun.aspnet_msgbox("You are not authorised")
        End If
    End Sub

    Protected Sub ddlSelectView_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectView.SelectedIndexChanged
        Dim selectedview As String
        If ddlSelectView.SelectedIndex <> -1 And ddlSelectView.SelectedIndex <> 0 Then
            selectedview = ddlSelectView.SelectedValue
            Dim ds1 As New DataSet
            ds1 = tableobj.Get_ViewOwner(selectedview)
            tbxPreviousOwner.Text = ds1.Tables(0).Rows(0)("UserName").ToString()
            hdUserId.Value = ds1.Tables(0).Rows(0)("UserId").ToString()
        Else
            tbxPreviousOwner.Text = ""
            hdUserId.Value = ""
        End If

    End Sub
    ''' <summary>
    ''' clear the controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlClientview.Items.Clear()
        ddlLobView.Items.Clear()
        ddlNewOwner.Items.Clear()
        ddlSelectView.Items.Clear()
        tbxPreviousOwner.Text = ""
        ddlDepartmentUser.SelectedIndex = 0
        ddlDepartmentView.SelectedIndex = 0
    End Sub
    ''' <summary>
    ''' display the message
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
