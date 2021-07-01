Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_MenuRightsManagement_AssignMenuRights
    Inherits System.Web.UI.Page
    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim ObjMenu As New MenuRight
    Dim fun As New Functions
    Dim ddlDepartment As DropDownList
    Dim ddlDepartmentTable As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlClientTable As DropDownList
    Dim ddlLob As DropDownList
    Dim ddlLobTable As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim loggedId As String
    Dim UserType As String
    Dim uspan As New UserSpan
    Dim deleteuser As String
    ''' <summary>
    ''' fill the span using user control according to the type of user
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

        ddlDepartment = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            ' ddlDepartment is being populated on page load
            If UserType = "Admin" Then

                btnAdmin.Visible = True
                Superadmin.Visible = False
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "DeptID"
                ddlDepartment.DataSource = fun.bind_Dept()
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
                BindMenuAdmin()

            End If
            If UserType = "User" Then

                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "DeptID"
                ddlDepartment.DataSource = fun.bind_Dept()
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
                BindMenuAdmin()

            End If
            If UserType = "Super Admin" Then
                btnAdmin.Visible = False
                Superadmin.Visible = True
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "DeptID"
                ddlDepartment.DataSource = fun.bind_Dept()
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
                BindMenu()

            End If
        End If
    End Sub

   

    ''' <summary>
    '''  'Method to bind to choose  user from userlist
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindUser()
        If ddlDepartment.SelectedIndex > 0 Then
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

            If UserType = "Super Admin" Then
                lstusers.DataSource = uspan.bind_UserSAdminlob(deptid, client_id, lobid)
                lstusers.DataTextField = "disname"
                lstusers.DataValueField = "UserId"
                lstusers.DataBind()
                Exit Sub

            End If
            If UserType = "Admin" Then
                lstusers.DataSource = uspan.userselectadminspan(loggedId, deptid, client_id, lobid)
                lstusers.DataTextField = "disname"
                lstusers.DataValueField = "UserId"
                lstusers.DataBind()
                Exit Sub
            End If

        End If
    End Sub
    ''' <summary>
    '''  'bind menu list with lstmainhead listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindMenu()
      
                lstmainhead.DataSource = ObjMenu.Bind_Menu()
        

            lstmainhead.DataTextField = "MenuDescription"
            lstmainhead.DataValueField = "MenuId"
            lstmainhead.DataBind()
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
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", Script)
        Return 1
    End Function
    ''' <summary>
    ''' selecting main module in listbox according to type of user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lstmainhead_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstmainhead.SelectedIndexChanged
        lstsubhead1.Items.Clear()
        lstsubhead2.Items.Clear()
        lstsubhead3.Items.Clear()

        If UserType = "Super Admin" Then
            lstsubhead1.DataSource = ObjMenu.Bind_LinkMenu(lstmainhead.SelectedValue)
            lstsubhead1.DataTextField = "MenuDescription"
            lstsubhead1.DataValueField = "MenuId"
            lstsubhead1.DataBind()
        End If

        If UserType = "Admin" Then
            lstsubhead1.DataSource = ObjMenu.Bind_LinkMenuAdmin(loggedId, lstmainhead.SelectedValue)
            lstsubhead1.DataTextField = "MenuDescription"
            lstsubhead1.DataValueField = "MenuId"
            lstsubhead1.DataBind()
        End If

    End Sub
    ''' <summary>
    ''' selecting sub module in listbox according to type of user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lstsubhead1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstsubhead1.SelectedIndexChanged

        lstsubhead2.Items.Clear()
        lstsubhead3.Items.Clear()

        If UserType = "Super Admin" Then
            lstsubhead2.DataSource = ObjMenu.Bind_SubLink1Menu(lstsubhead1.SelectedValue)
            lstsubhead2.DataTextField = "MenuDescription"
            lstsubhead2.DataValueField = "MenuId"
            lstsubhead2.DataBind()
        End If

        If UserType = "Admin" Then
            lstsubhead2.DataSource = uspan.Bind_SubLink1MenuAdmin(loggedId, lstsubhead1.SelectedValue)
            lstsubhead2.DataTextField = "MenuDescription"
            lstsubhead2.DataValueField = "MenuId"
            lstsubhead2.DataBind()

        End If

    End Sub
    ''' <summary>
    ''' selcting the further sub module of sub modules according to type of user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lstsubhead2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstsubhead2.SelectedIndexChanged

        lstsubhead3.Items.Clear()

        If UserType = "Super Admin" Then
            lstsubhead3.DataSource = ObjMenu.Bind_SubLink2Menu(lstsubhead2.SelectedValue)
            lstsubhead3.DataTextField = "MenuDescription"
            lstsubhead3.DataValueField = "MenuId"
            lstsubhead3.DataBind()
        End If

        If UserType = "Admin" Then
            lstsubhead3.DataSource = uspan.Bind_SubLink2MenuAdmin(loggedId, lstsubhead2.SelectedValue)
            lstsubhead3.DataTextField = "MenuDescription"
            lstsubhead3.DataValueField = "MenuId"
            lstsubhead3.DataBind()

        End If

    End Sub
    ''' <summary>
    ''' selecting the all ready given rights of user
    ''' </summary>
    ''' <param name="strid"></param>
    ''' <param name="struser"></param>
    ''' <param name="usertype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkexists(ByVal strid As Integer, ByVal struser As String, ByVal usertype As Integer) As Boolean
        Dim cmdchk As New SqlCommand("select * from nlvl_menu_rights where MenuId='" & strid & "' and userid='" & struser & "' and usertype='" & usertype & "'", con)
        Dim boolchk As Boolean
        Dim drchk As SqlDataReader
        con.Close()
        con.Open()
        drchk = cmdchk.ExecuteReader
        While drchk.Read
            boolchk = True
        End While
        drchk.Close()
        con.Close()
        cmdchk.Dispose()
        If boolchk = True Then
            Return False
        Else
            Return True
        End If
    End Function
    ''' <summary>
    ''' assgning the selected rights to selected user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdsav_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdsav.Click
        Dim cnt As Integer = 0
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim l As Integer = 0
        Dim dsmenuname As New DataSet
        Dim menuname As String
        Dim arruser(40) As String
        Dim arrhead(30) As String
        Dim arrlink1(30) As String
        Dim arrsublink1(30) As String
        Dim arrsublink3(30) As String
        Dim type As String
        Dim permit As String
        Dim passwordreset As Boolean

        'validting values
        If lstsubhead3.Items.Count <> 0 And lstsubhead3.SelectedIndex = -1 Then
            ShowConfirm("Choose sublink2 ")
            Exit Sub
        End If

        If lstsubhead2.Items.Count <> 0 And lstsubhead2.SelectedIndex = -1 Then
            ShowConfirm("Choose sublink1 ")
            Exit Sub
        End If

        If lstsubhead1.Items.Count <> 0 And lstsubhead1.SelectedIndex = -1 Then
            ShowConfirm("Choose link ")
            Exit Sub
        End If

        If lstmainhead.Items.Count <> 0 And lstmainhead.SelectedIndex = -1 Then
            ShowConfirm("Choose main menu")
            Exit Sub
        End If

        If lstusers.SelectedIndex = -1 Then
            ShowConfirm("Choose user")
            Exit Sub
        End If


        'getting value of selected user

        For i = 0 To lstusers.Items.Count - 1
            If lstusers.Items(i).Selected = True Then
                arruser(i) = lstusers.Items(i).Value
            End If
        Next
        'getting value of main menu

        For i = 0 To lstmainhead.Items.Count - 1
            If lstmainhead.Items(i).Selected = True Then
                arrhead(i) = lstmainhead.Items(i).Value
            End If
        Next
        'getting values of sub menu first

        For i = 0 To lstsubhead1.Items.Count - 1
            If lstsubhead1.Items(i).Selected = True Then
                arrlink1(i) = lstsubhead1.Items(i).Value
            End If
        Next

        'getting value of sub menu second

        For i = 0 To lstsubhead2.Items.Count - 1
            If lstsubhead2.Items(i).Selected = True Then
                arrsublink1(i) = lstsubhead2.Items(i).Value
            End If
        Next

        'getting value of sub menu third

        For i = 0 To lstsubhead3.Items.Count - 1
            If lstsubhead3.Items(i).Selected = True Then
                arrsublink3(i) = lstsubhead3.Items(i).Value
            End If
        Next


        For cnt = 0 To arruser.Length - 1    ' for arruser
            If arruser(cnt) <> "" Then  'if 15
                If UserType = "Admin" Then
                    type = "1"
                Else
                    type = GetUserType(arruser(cnt))
                End If

                'type = GetUserType(arruser(cnt))
                'checking if password reset with the user
                passwordreset = CheckPasswordResetStatus(arruser(cnt), type, "105")
                If passwordreset = True And type = 1 Then
                    ShowConfirm("This User already has Reset Password Right.Therefore no other right can be assighned to it.")
                    ClearAll()
                    Exit Sub
                End If
                Dim ddpt As String = ddlDepartment.SelectedValue
                If ddpt = "--Select--" Then
                    ddpt = 0
                Else
                    ddpt = ddlDepartment.SelectedValue
                End If

                For i = 0 To arrhead.Length - 1  ' for arrhead
                    If (arrhead(i) <> "") Then   ' if 8
                        If chkexists(arrhead(i), arruser(cnt), type) = True Then  ' if 7
                            Dim cmdins1 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                            cmdins1.CommandType = CommandType.StoredProcedure
                            With cmdins1.Parameters
                                .AddWithValue("@LOB", "0")
                                .AddWithValue("@MenuId", arrhead(i))
                                .AddWithValue("@UserType", type)
                                .AddWithValue("@Access", "")
                                .AddWithValue("@Currdate", System.DateTime.Now)
                                .AddWithValue("@AssignBy", loggedId)
                                .AddWithValue("@parentid", "0")
                                .AddWithValue("@userid", arruser(cnt))
                            End With
                            con.Open()
                            cmdins1.ExecuteNonQuery()
                            con.Close()
                            cmdins1.Dispose()

                            '*************change*************
                            cmdins1 = New SqlCommand("TrackRights", con)
                            cmdins1.CommandType = CommandType.StoredProcedure
                            With cmdins1.Parameters

                                .AddWithValue("@menuid", arrhead(i))
                                .AddWithValue("@actionBY", loggedId)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)

                                .AddWithValue("@AssignTo", arruser(cnt))
                                .AddWithValue("@Entity", "Menu")
                               
                                .AddWithValue("@DeptId", ddpt)
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
                                .AddWithValue("@ClientId", clta)
                                .AddWithValue("@LOBId", lobt)
                                .AddWithValue("@type", Session("usertype"))
                            End With
                            con.Open()
                            cmdins1.ExecuteNonQuery()
                            con.Close()
                            cmdins1.Dispose()
                            '*************change*************
                        End If    ' end 7
                        If lstsubhead1.Items.Count <> 0 Then  ' if 9

                            For j = 0 To arrlink1.Length - 1   ' for arrlink1
                                If arrlink1(j) <> "" Then   ' if 10

                                    'checking if menu is for user or not
                                    If type = 1 Then
                                        permit = CheckLimit(arrlink1(j))
                                        If permit = "OA" Then
                                            dsmenuname = uspan.GetMenuName(arrlink1(j))
                                            menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                            ShowConfirm(menuname & " " & "is not permitted to be assigned to a User")
                                            ClearAll()
                                            Continue For
                                        End If
                                    End If

                                    'checking if menu is Reset Password
                                    If arrlink1(j) = 105 And type = 1 Then
                                        pandelete.Visible = True
                                        Exit Sub
                                    End If

                                    If chkexists(arrlink1(j), arruser(cnt), type) = True Then  ' if 11
                                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                        cmdins2.CommandType = CommandType.StoredProcedure
                                        With cmdins2.Parameters
                                            .AddWithValue("@LOB", "0")
                                            .AddWithValue("@MenuId", arrlink1(j))
                                            .AddWithValue("@UserType", type)
                                            .AddWithValue("@Access", "")
                                            .AddWithValue("@Currdate", System.DateTime.Now)
                                            .AddWithValue("@AssignBy", loggedId)
                                            .AddWithValue("@parentid", arrhead(i))
                                            .AddWithValue("@userid", arruser(cnt))
                                        End With
                                        con.Open()
                                        cmdins2.ExecuteNonQuery()
                                        con.Close()
                                        cmdins2.Dispose()


                                        '*************change*************
                                        cmdins2 = New SqlCommand("TrackRightsparentid", con)
                                        cmdins2.CommandType = CommandType.StoredProcedure
                                        With cmdins2.Parameters

                                            .AddWithValue("@menuid", arrlink1(j))
                                            .AddWithValue("@parentid", arrhead(i))
                                            .AddWithValue("@actionBY", loggedId)
                                            .AddWithValue("@Action", "Assign Rights")
                                            .AddWithValue("@Date", System.DateTime.Now)
                                            .AddWithValue("@AssignTo", arruser(cnt))
                                            .AddWithValue("@Entity", "Menu")
                                            .AddWithValue("@DeptId", ddpt)
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
                                            .AddWithValue("@ClientId", clta)
                                            .AddWithValue("@LOBId", lobt)
                                            .AddWithValue("@type", Session("usertype"))
                                        End With
                                        con.Open()
                                        cmdins2.ExecuteNonQuery()
                                        con.Close()
                                        cmdins2.Dispose()
                                        '*************change*************
                                    End If   'end 11

                                    If lstsubhead2.Items.Count <> 0 Then   ' if 12
                                        'Dim arrsublink = Split(Request("lstsubhead2"), ",")
                                        For k = 0 To arrsublink1.Length - 1
                                            If arrsublink1(k) <> "" Then   ' if 13

                                                'checking if menu is for user or not
                                                If type = 1 Then
                                                    permit = CheckLimit(arrsublink1(k))
                                                    If permit = "OA" Then
                                                        dsmenuname = uspan.GetMenuName(arrsublink1(k))
                                                        menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                                        ShowConfirm(menuname & " " & "is not permitted to be assigned to a User")
                                                        ClearAll()
                                                        Exit For
                                                    End If
                                                End If

                                                If chkexists(arrsublink1(k), arruser(cnt), type) = True Then  ' if 14
                                                    Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                                    cmdins3.CommandType = CommandType.StoredProcedure
                                                    With cmdins3.Parameters
                                                        .AddWithValue("@LOB", "0")
                                                        .AddWithValue("@MenuId", arrsublink1(k))
                                                        .AddWithValue("@UserType", type)
                                                        .AddWithValue("@Access", "")
                                                        .AddWithValue("@Currdate", System.DateTime.Now)
                                                        .AddWithValue("@AssignBy", loggedId)
                                                        .AddWithValue("@parentid", arrlink1(j))
                                                        .AddWithValue("@userid", arruser(cnt))
                                                    End With
                                                    con.Open()
                                                    cmdins3.ExecuteNonQuery()
                                                    con.Close()
                                                    cmdins3.Dispose()
                                                    '*************change*************
                                                    cmdins3 = New SqlCommand("TrackRightsparentid", con)
                                                    cmdins3.CommandType = CommandType.StoredProcedure
                                                    With cmdins3.Parameters

                                                        .AddWithValue("@menuid", arrsublink1(k))
                                                        .AddWithValue("@parentid", arrlink1(j))
                                                        .AddWithValue("@actionBY", loggedId)
                                                        .AddWithValue("@Action", "Assign Rights")
                                                        .AddWithValue("@Date", System.DateTime.Now)

                                                        .AddWithValue("@AssignTo", arruser(cnt))
                                                        .AddWithValue("@Entity", "Menu")
                                                        
                                                        .AddWithValue("@DeptId", ddpt)
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
                                                        .AddWithValue("@ClientId", clta)
                                                        .AddWithValue("@LOBId", lobt)
                                                        .AddWithValue("@type", Session("usertype"))
                                                    End With
                                                    con.Open()
                                                    cmdins3.ExecuteNonQuery()
                                                    con.Close()
                                                    cmdins3.Dispose()
                                                    '*************change*************
                                                End If  ' end 14
                                                If lstsubhead3.Items.Count <> 0 Then   ' if 6

                                                    For l = 0 To arrsublink3.Length - 1    ' for arrsublink3
                                                        If arrsublink3(l) <> "" Then  ' if 4

                                                            'checking if menu is for user or not
                                                            If type = 1 Then
                                                                permit = CheckLimit(arrsublink3(l))
                                                                If permit = "OA" Then
                                                                    dsmenuname = uspan.GetMenuName(arrsublink3(l))
                                                                    menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                                                    ShowConfirm(menuname & " " & "is not permitted to be assigned to a User")
                                                                    ClearAll()
                                                                    Exit For
                                                                End If
                                                            End If

                                                            If chkexists(arrsublink3(l), arruser(cnt), type) = True Then  ' if 5
                                                                Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                                With cmdins3.Parameters
                                                                    .AddWithValue("@LOB", "0")
                                                                    .AddWithValue("@MenuId", arrsublink3(l))
                                                                    .AddWithValue("@UserType", type)
                                                                    .AddWithValue("@Access", "")
                                                                    .AddWithValue("@Currdate", System.DateTime.Now)
                                                                    .AddWithValue("@AssignBy", loggedId)
                                                                    .AddWithValue("@parentid", arrsublink1(k))
                                                                    .AddWithValue("@userid", arruser(cnt))
                                                                End With
                                                                con.Open()
                                                                cmdins3.ExecuteNonQuery()
                                                                con.Close()
                                                                cmdins3.Dispose()
                                                                '*************change*************
                                                                cmdins3 = New SqlCommand("TrackRightsparentid", con)
                                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                                With cmdins3.Parameters

                                                                    .AddWithValue("@menuid", arrsublink3(l))
                                                                    .AddWithValue("@parentid", arrsublink1(k))
                                                                    .AddWithValue("@actionBY", loggedId)
                                                                    .AddWithValue("@Action", "Assign Rights")
                                                                    .AddWithValue("@Date", System.DateTime.Now)

                                                                    .AddWithValue("@AssignTo", arruser(cnt))
                                                                    .AddWithValue("@Entity", "Menu")
                                                                    
                                                                    .AddWithValue("@DeptId", ddpt)
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
                                                                    .AddWithValue("@ClientId", clta)
                                                                    .AddWithValue("@LOBId", lobt)
                                                                    .AddWithValue("@type", Session("usertype"))
                                                                End With
                                                                con.Open()
                                                                cmdins3.ExecuteNonQuery()
                                                                con.Close()
                                                                cmdins3.Dispose()
                                                                '*************change*************
                                                            End If   ' end 5
                                                        End If  ' end 4 

                                                    Next 'arrsublink3
                                                Else

                                                    Dim cmdget As New SqlCommand("select * from nlvl_menu where  ParentId='" & arrsublink1(k) & "'", con)
                                                    Dim drget As SqlDataReader
                                                    con.Open()
                                                    drget = cmdget.ExecuteReader
                                                    While drget.Read   ' while 3
                                                        If chkexists(drget("MenuId").ToString, arruser(cnt), type) = True Then   ' if 3 
                                                            Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                                            cmdins3.CommandType = CommandType.StoredProcedure
                                                            With cmdins3.Parameters
                                                                .AddWithValue("@LOB", "0")
                                                                .AddWithValue("@MenuId", drget("MenuId").ToString)
                                                                .AddWithValue("@UserType", type)
                                                                .AddWithValue("@Access", "")
                                                                .AddWithValue("@Currdate", System.DateTime.Now)
                                                                .AddWithValue("@AssignBy", loggedId)
                                                                .AddWithValue("@parentid", arrlink1(j))
                                                                .AddWithValue("@userid", arruser(cnt))
                                                            End With
                                                            con.Open()
                                                            cmdins3.ExecuteNonQuery()
                                                            con.Close()
                                                            cmdins3.Dispose()
                                                            '*************change*************
                                                            cmdins3 = New SqlCommand("TrackRightsparentid", con)
                                                            cmdins3.CommandType = CommandType.StoredProcedure
                                                            With cmdins3.Parameters

                                                                .AddWithValue("@menuid", drget("MenuId").ToString)
                                                                .AddWithValue("@parentid", arrlink1(j))
                                                                .AddWithValue("@actionBY", loggedId)
                                                                .AddWithValue("@Action", "Assign Rights")
                                                                .AddWithValue("@Date", System.DateTime.Now)

                                                                .AddWithValue("@AssignTo", arruser(cnt))
                                                                .AddWithValue("@Entity", "Menu")
                                                                .AddWithValue("@DeptId", ddpt)
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
                                                                .AddWithValue("@ClientId", clta)
                                                                .AddWithValue("@LOBId", lobt)
                                                                .AddWithValue("@type", Session("usertype"))
                                                            End With
                                                            con.Open()
                                                            cmdins3.ExecuteNonQuery()
                                                            con.Close()
                                                            cmdins3.Dispose()
                                                            '*************change*************
                                                        End If  '  end  3
                                                    End While  ' end while 3
                                                    drget.Close()
                                                    con.Close()
                                                    cmdget.Dispose()
                                                    'End If
                                                End If
                                            End If
                                        Next 'arrsublink1
                                    Else
                                        'If arrlink1(1) = arrhead(i) Then
                                        Dim cmdget As New SqlCommand("select * from nlvl_menu where ParentId='" & arrlink1(j) & "'", con)
                                        Dim drget As SqlDataReader
                                        con.Open()
                                        drget = cmdget.ExecuteReader
                                        While drget.Read    ' while 2
                                            If chkexists(drget("MenuId").ToString, arruser(cnt), type) = True Then  'if 2
                                                Dim cmdins3 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                With cmdins3.Parameters
                                                    .AddWithValue("@LOB", "0")
                                                    .AddWithValue("@MenuId", drget("MenuId").ToString)
                                                    .AddWithValue("@UserType", type)
                                                    .AddWithValue("@Access", "")
                                                    .AddWithValue("@Currdate", System.DateTime.Now)
                                                    .AddWithValue("@AssignBy", loggedId)
                                                    .AddWithValue("@parentid", arrlink1(j))
                                                    .AddWithValue("@userid", arruser(cnt))
                                                End With
                                                con.Open()
                                                cmdins3.ExecuteNonQuery()
                                                con.Close()
                                                cmdins3.Dispose()
                                                '*************change*************
                                                cmdins3 = New SqlCommand("TrackRightsparentid", con)
                                                cmdins3.CommandType = CommandType.StoredProcedure
                                                With cmdins3.Parameters

                                                    .AddWithValue("@menuid", drget("MenuId").ToString)
                                                    .AddWithValue("@parentid", arrlink1(j))
                                                    .AddWithValue("@actionBY", loggedId)
                                                    .AddWithValue("@Action", "Assign Rights")
                                                    .AddWithValue("@Date", System.DateTime.Now)

                                                    .AddWithValue("@AssignTo", arruser(cnt))
                                                    .AddWithValue("@Entity", "Menu")
                                                    .AddWithValue("@DeptId", ddpt)
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
                                                    .AddWithValue("@ClientId", clta)
                                                    .AddWithValue("@LOBId", lobt)
                                                    .AddWithValue("@type", Session("usertype"))
                                                End With
                                                con.Open()
                                                cmdins3.ExecuteNonQuery()
                                                con.Close()
                                                cmdins3.Dispose()
                                                '*************change*************
                                            End If       ' end 2
                                            Dim cmdget1 As New SqlCommand("select * from nlvl_menu where  ParentId='" & drget("MenuId") & "'", con)
                                            Dim drget1 As SqlDataReader
                                            con.Open()
                                            drget1 = cmdget1.ExecuteReader
                                            While drget1.Read     ' while 1
                                                If chkexists(drget1("MenuId").ToString, arruser(cnt), type) = True Then   'if 1
                                                    Dim cmdins4 As New SqlCommand("insert_NLVL_MENU_Rights", con)
                                                    cmdins4.CommandType = CommandType.StoredProcedure
                                                    With cmdins4.Parameters
                                                        .AddWithValue("@LOB", "0")
                                                        .AddWithValue("@MenuId", drget1("MenuId").ToString)
                                                        .AddWithValue("@UserType", type)
                                                        .AddWithValue("@Access", "")
                                                        .AddWithValue("@Currdate", System.DateTime.Now)
                                                        .AddWithValue("@AssignBy", loggedId)
                                                        .AddWithValue("@parentid", drget("MenuId").ToString)
                                                        .AddWithValue("@userid", arruser(cnt))
                                                    End With
                                                    con.Open()
                                                    cmdins4.ExecuteNonQuery()
                                                    con.Close()
                                                    cmdins4.Dispose()
                                                    '*************change*************
                                                    cmdins4 = New SqlCommand("TrackRightsparentid", con)
                                                    cmdins4.CommandType = CommandType.StoredProcedure
                                                    With cmdins4.Parameters

                                                        .AddWithValue("@menuid", drget1("MenuId").ToString)
                                                        .AddWithValue("@parentid", drget("MenuId").ToString)
                                                        .AddWithValue("@actionBY", loggedId)
                                                        .AddWithValue("@Action", "Assign Rights")
                                                        .AddWithValue("@Date", System.DateTime.Now)

                                                        .AddWithValue("@AssignTo", arruser(cnt))
                                                        .AddWithValue("@Entity", "Menu")
                                                        .AddWithValue("@DeptId", ddpt)
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
                                                        .AddWithValue("@ClientId", clta)
                                                        .AddWithValue("@LOBId", lobt)
                                                        .AddWithValue("@type", Session("usertype"))
                                                    End With
                                                    con.Open()
                                                    cmdins4.ExecuteNonQuery()
                                                    con.Close()
                                                    cmdins4.Dispose()
                                                    '*************change*************
                                                End If   ' end 1
                                            End While  ' end while 1
                                        End While   ' end while 2
                                        drget.Close()
                                        con.Close()
                                        cmdget.Dispose()
                                        'End If
                                    End If
                                End If   'end 10
                            Next ',arrlink1
                        End If
                    End If 'end 8
                Next  'arrhead
            End If  ' end 15
        Next  'arruser
        ShowConfirm("Rights has been assigned successfully!")
        'ClearAll()

    End Sub
    ''' <summary>
    ''' clear  controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        'lstsubhead1.Items.Clear()
        'lstsubhead2.Items.Clear()
        'lstsubhead3.Items.Clear()
        'lstusers.Items.Clear()
        ddlDepartment.SelectedIndex = 0
        cmdsav.Visible = False
        'rdoAdmin.Checked = False
        'rdoUser.Checked = False
    End Sub
    ''' <summary>
    ''' 'bind menu list with lstmenu listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindMenuAdmin()

  If Session("userid") <> "" Then


            Dim uty = "1"
            If (Session("typeofuser") = "User") Then
                uty = "1"
                lstmainhead.DataSource = ObjMenu.Bind_Menuother(Session("userid"), uty)
            ElseIf (Session("typeofuser") = "Admin") Then
                uty = "2"
                lstmainhead.DataSource = ObjMenu.Bind_Menuother(Session("userid"), uty)
            End If


            ' lstmainhead.DataSource = ObjMenu.Bind_Menu()
            lstmainhead.DataTextField = "MenuDescription"
            lstmainhead.DataValueField = "MenuId"
            lstmainhead.DataBind()
        Else
            ShowConfirm("Session Expired. Login Again.")
        End If
    End Sub
    ''' <summary>
    ''' assign the ReSetPassword Right
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        Dim arruser(40) As String
        Dim cnt As Integer = 0
        Dim i As Integer
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim type As String = "1"

        pandelete.Visible = False

        For i = 0 To lstusers.Items.Count - 1
            If lstusers.Items(i).Selected = True Then
                arruser(i) = lstusers.Items(i).Value
            End If
        Next

        For cnt = 0 To arruser.Length - 1
            If arruser(cnt) <> "" Then
                con.Open()
                Dim cmdChkUser As New SqlCommand("Select userid,usertype from nlvl_menu_rights where userid='" & arruser(cnt) & "' and usertype='" & type & "'", con)
                Dim rdrChkUser As SqlDataReader = cmdChkUser.ExecuteReader()
                If rdrChkUser.Read() Then
                    rdrChkUser.Close()
                    con.Close()
                    con.Open()
                    Dim cmdMenu As New SqlCommand("Select menuid from nlvl_menu_rights where userid='" & arruser(cnt) & "' and usertype ='" & type & "'", con)
                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmdMenu
                    Dim dsCount As New DataSet
                    da.Fill(dsCount)
                    Dim arrmenucount(dsCount.Tables(0).Rows.Count) As String
                    For j = 0 To arrmenucount.Length - 2
                        arrmenucount(j) = dsCount.Tables(0).Rows(j)("MenuId").ToString()
                    Next
                    For k = 0 To arrmenucount.Length - 2
                        ObjMenu.DeleteUserRights(arrmenucount(k), type, arruser(cnt))
                    Next
                    ObjMenu.InsertUserRights(arruser(cnt), 1, loggedId)

                Else
                    ObjMenu.InsertUserRights(arruser(cnt), 1, loggedId)

                End If
            End If
        Next
        ShowConfirm("Password Reset is assigned ")
        ClearAll()
    End Sub
    ''' <summary>
    ''' confirmation of command is dening here
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click

        pandelete.Visible = False
        ClearAll()
        ShowConfirm("No right given to user.")
        Exit Sub

    End Sub
    ''' <summary>
    ''' checking the type of user
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetUserType(ByVal userid As String)
        Dim type As String
        con.Open()
        Dim chktype As New SqlCommand("select adminid from masteradmin where adminid='" & userid & "'", con)
        Dim rdr As SqlDataReader = chktype.ExecuteReader()
        If rdr.Read() Then
            If rdoAdmin.Checked = True Then
                type = 2.ToString()
            End If
            If rdoUser.Checked = True Then
                type = 1.ToString()
            End If

        Else
            type = 1.ToString()
        End If
        con.Close()
        Return type
    End Function
    ''' <summary>
    ''' selecting the limit 
    ''' </summary>
    ''' <param name="menuid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckLimit(ByVal menuid As String)
        Dim permit As String
        con.Open()
        Dim chkpermit As New SqlCommand("select limit from nlvl_menu  where menuid='" & menuid & "'", con)
        permit = chkpermit.ExecuteScalar().ToString
        con.Close()
        Return permit
    End Function
    ''' <summary>
    ''' for users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Protected Sub rdoAdmin_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAdmin.CheckedChanged

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department for users")
            rdoAdmin.Checked = False
            Exit Sub
        End If
        cmdsav.Visible = True
        BindAdminUsers()

    End Sub
    ''' <summary>
    ''' for admins
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department for users")
            rdoUser.Checked = False
            Exit Sub
        End If
        cmdsav.Visible = True
        BindUser()

    End Sub
    ''' <summary>
    '''  Method to bind users from Admin list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindAdminUsers()
        If ddlLob.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 And ddlDepartment.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLob.SelectedValue)
            client_id = Convert.ToInt32(ddlClient.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartment.SelectedValue)

            If UserType = "Super Admin" Then
                lstusers.DataSource = uspan.bind_UserSAdminlobMasterAdmin(deptid, client_id, lobid)
                lstusers.DataTextField = "disname"
                lstusers.DataValueField = "AdminId"
                lstusers.DataBind()
                Exit Sub
            End If

        End If

        If ddlDepartment.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 Then

            client_id = Convert.ToInt32(ddlClient.SelectedValue)
            deptid = ddlDepartment.SelectedValue.ToString()

            If UserType = "Super Admin" Then
                lstusers.DataSource = uspan.bind_UserSAdminClientMasterAdmin(deptid, client_id)
                lstusers.DataTextField = "disname"
                lstusers.DataValueField = "AdminId"
                lstusers.DataBind()
                Exit Sub
            End If

        End If

        If ddlDepartment.SelectedIndex > 0 Then
            deptid = Convert.ToInt32(ddlDepartment.SelectedValue)

            If UserType = "Super Admin" Then
                lstusers.DataSource = uspan.bind_UserSAdminDepartmentMasterAdmin(deptid)
                lstusers.DataTextField = "disname"
                lstusers.DataValueField = "AdminId"
                lstusers.DataBind()
            End If

        End If
    End Sub
    ''' <summary>
    ''' check the password ReSet Right
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <param name="usertype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckPasswordResetStatus(ByVal userid As String, ByVal usertype As String, ByVal mid As String)
        Dim passwordreset As String
        Dim menuid As String = mid
        'Dim menuid As String = "105"
        con.Open()
        Dim chkpasswordreset As New SqlCommand("select menuid,userid,usertype from nlvl_menu_rights  where menuid='" & menuid & "' and userid='" & userid & "' and usertype='" & usertype & "'", con)
        Dim rdrpreset As SqlDataReader = chkpasswordreset.ExecuteReader()
        If rdrpreset.Read() Then
            passwordreset = True
        Else
            passwordreset = False
        End If
        con.Close()
        Return passwordreset
    End Function
    ''' <summary>
    ''' for users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdmin.Click
        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department for users")
        End If
        cmdsav.Visible = True
        BindUser()
    End Sub
    ''' <summary>
    ''' Newwly added on 05/11/08 by Usha Sheokand to refresh span
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl1.Load

      
        If ddlDepartment.SelectedIndex = 0 Then
            Me.lstusers.Items.Clear()
            ddlClient.Items.Clear()
            rdoAdmin.Checked = False
            rdoUser.Checked = False
            ddlLob.Items.Clear()
      
        End If
        If ddlClient.SelectedIndex = 0 Then
            ddlLob.Items.Clear()
       
        End If
    End Sub
End Class
