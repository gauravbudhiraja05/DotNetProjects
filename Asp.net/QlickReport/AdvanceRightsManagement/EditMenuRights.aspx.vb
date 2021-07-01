Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_EditMenuRights
    Inherits System.Web.UI.Page
    Dim objMenu As New MenuRight
    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim da As New SqlDataAdapter
    Dim fun As New Functions
    Dim sessiondepvalue As String
    Dim uspan As New UserSpan
    Dim loggedId As String
    Dim UserType As String
    ''' <summary>
    ''' bind the menu of user in lists
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Page.SmartNavigation = True
        'getting value of selected userid

        If Session("userid") = "" Then
            Response.Redirect("~/SessionExpired.aspx")
        Else
            loggedId = Session("userid").ToString()
            UserType = Session("typeofuser").ToString()
            tbxUserId.Text = Session("linktext").ToString()
        End If



        'binding gridview for assigned rights
        If Me.IsPostBack = False Then
            bindgvRights()
            BindMenu()

        End If
        Me.Form.DefaultButton = Me.btnUpdateRights.UniqueID

    End Sub
    ''' <summary>
    '''     'Applying paging 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvRights_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvRights.PageIndexChanging
        'Applying paging 
        If gvRights.PageIndex < gvRights.PageCount And gvRights.PageIndex >= 0 Then
            gvRights.PageIndex = e.NewPageIndex
            bindgvRights()


        End If
    End Sub
    ''' <summary>
    '''  'Method bind gridView with assigned Menu Rights
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub bindgvRights()

        con = New SqlConnection(str)
        con.Open()
        Dim str1 As String = Session("linktext").ToString()
        Dim str3 As String = Session("linkUserType").ToString()
        Dim str2 As String

        If str3 = "User" Then
            str2 = "1"
        Else
            str2 = "2"
        End If

        Dim cmdget As New SqlCommand("select  * from Fn_GetMenuRights('" & str1 & "','" & str2 & "' )", con)
        Dim ds As New DataSet
        Dim adp As New SqlDataAdapter
        adp.SelectCommand = cmdget
        adp.Fill(ds)
        con.Close()
        gvRights.Columns(0).Visible = True
        gvRights.DataSource = ds
        gvRights.DataBind()
        gvRights.Columns(0).Visible = False
    End Sub
    ''' <summary>
    ''' deleting row from gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvRights_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvRights.RowDeleting
        Static count As Integer = 0
        Dim i As Integer
        Dim selection As CheckBox
        For i = 0 To gvRights.Rows.Count - 1
            selection = CType(gvRights.Rows(i).FindControl("chkdel"), CheckBox)

            If selection.Checked = True Then
                count = count + 1
            End If
        Next
        If count = 0 Then
            ShowConfirm("Select righs")
            Exit Sub
        End If
        pandelete.Visible = True
    End Sub
    ''' <summary>
    ''' make visible panel
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click
        pandelete.Visible = False
    End Sub
    ''' <summary>
    ''' Code to delete assigned Rights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click

        'Code to delete assigned Rights
        Dim i As Integer
        Dim autoid As String
        Dim type2 As String = Session("linkUserType").ToString()
        Dim type As String
        If type2 = "User" Then
            type = "1"
        Else
            type = "2"
        End If


        For i = 0 To gvRights.Rows.Count - 1
            If CType(gvRights.Rows(i).FindControl("chkdel"), CheckBox).Checked = True Then
                autoid = CType(gvRights.Rows(i).FindControl("lblAutoIdItem"), Label).Text
                Dim clta As Integer
                Dim lobt As Integer
                If IsNumeric(Session("cp")) Then
                    clta = Session("cp")
                Else
                    clta = 0
                End If
                If IsNumeric(Session("lp")) Then
                    lobt = Session("lp")
                Else
                    lobt = 0
                End If
                Dim cmdget As New SqlCommand("TrackRightsDelete", con)
                cmdget.CommandType = CommandType.StoredProcedure
                With cmdget.Parameters
                    .AddWithValue("@Autoid", autoid)
                    .AddWithValue("@actionBY", Session("userid"))
                    .AddWithValue("@Action", "Delete Rights")

                    .AddWithValue("@Date", System.DateTime.Now)

                    .AddWithValue("@Entity", "Menu")

                    .AddWithValue("@DeptId", Session("dp"))

                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@type", Session("usertype"))

                End With
                con.Open()
                cmdget.ExecuteNonQuery()
                con.Close()
                cmdget.Dispose()
                objMenu.CancelMenuRightsEdit(autoid, type)
                
                autoid = ""
            End If
        Next
        Dim userid As String = tbxUserId.Text
        objMenu.CancelMenuRightsRemaining(userid, type)
        pandelete.Visible = False
        bindgvRights()
        ShowConfirm("Rights deleted successfully")
    End Sub
    ''' <summary>
    '''  'bind menu list with lstmenu listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindMenu()
        'bind menu list with lstmenu listbox
        If Session("userid") <> "" Then


            Dim uty = "1"
            If (Session("typeofuser") = "User") Then
                uty = "1"
                lstmainhead.DataSource = objMenu.Bind_Menuother(Session("userid"), uty)
            ElseIf (Session("typeofuser") = "Admin") Then
                uty = "2"
                lstmainhead.DataSource = objMenu.Bind_Menuother(Session("userid"), uty)

            ElseIf Session("typeofuser") = "Super Admin" Then
                ' lstmainhead.DataSource = ObjMenu.Bind_Menu()                

                lstmainhead.DataSource = objMenu.Bind_Menu()
              
            End If
            lstmainhead.DataTextField = "MenuDescription"
            lstmainhead.DataValueField = "MenuId"
            lstmainhead.DataBind()
        Else
            ShowConfirm("Session Expired. Login Again.")
        End If
    End Sub

   
    Protected Sub gvRights_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvRights.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:SelectAll('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If
    End Sub
    
    ''' <summary>
    ''' update the menu rights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUpdateRights_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdateRights.Click
        Dim clta As Integer
        Dim lobt As Integer
        If IsNumeric(Session("cp")) Then
            clta = Session("cp")
        Else
            clta = 0
        End If
        If IsNumeric(Session("lp")) Then
            lobt = Session("lp")
        Else
            lobt = 0
        End If
        Dim cnt As Integer = 0
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim l As Integer = 0
        Dim dsmenuname As New DataSet
        Dim menuname As String
        Dim arruser As String
        Dim arrhead(30) As String
        Dim arrlink1(30) As String
        Dim arrsublink1(30) As String
        Dim arrsublink3(30) As String
        Dim type As String
        Dim type1 As String

        Dim permit As String
        Dim passwordreset As Boolean

        'validting values
        If lstSubhead3.Items.Count <> 0 And lstSubhead3.SelectedIndex = -1 Then
            ShowConfirm("Choose sublink2 ")
            Exit Sub
        End If

        If lstSubhead2.Items.Count <> 0 And lstSubhead2.SelectedIndex = -1 Then
            ShowConfirm("Choose sunlink1 ")
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

        'getting value of selected user

        arruser = tbxUserId.Text

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
        

        type1 = Session("linkUserType").ToString()

        If type1 = "User" Then
            type = "1"
        Else
            type = "2"
        End If

        'checking if password reset with the user
        passwordreset = CheckPasswordResetStatus(arruser, type)
        If (passwordreset = True) And (type = 1) Then
            ShowConfirm("More right can not be assigned")
            ClearAll()
            Exit Sub
        End If

        For i = 0 To arrhead.Length - 1  ' for arrhead
            If (arrhead(i) <> "") Then   ' if 8
                If chkexists(arrhead(i), arruser, type) = True Then  ' if 7
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
                        .AddWithValue("@userid", arruser)
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
                        .AddWithValue("@Action", "Edit Rights")
                        .AddWithValue("@Date", System.DateTime.Now)

                        .AddWithValue("@AssignTo", arruser)
                        .AddWithValue("@Entity", "Menu")
                        .AddWithValue("@DeptId", Session("dp"))
                       
                        If IsNumeric(clta) Then
                            clta = clta
                        Else
                            clta = 0
                        End If
                        If IsNumeric(lobt) Then
                            lobt = lobt
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
                    'Dim arrlink = Split(Request("lstsubhead1"), ",")
                    For j = 0 To arrlink1.Length - 1   ' for arrlink1
                        If arrlink1(j) <> "" Then   ' if 10

                            'checking if menu is for user or not
                            If type = 1 Then
                                permit = CheckLimit(arrlink1(j))
                                If permit = "OA" Then
                                    dsmenuname = uspan.GetMenuName(arrlink1(j))
                                    menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                    ShowConfirm(menuname & "is not permitted for user")
                                    ClearAll()
                                    Continue For
                                End If
                            End If

                            'checking if menu is Reset Password
                            If arrlink1(j) = 105 And type = 1 Then
                                Panel1.Visible = True
                                Exit Sub
                            End If

                            If chkexists(arrlink1(j), arruser, type) = True Then  ' if 11
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
                                    .AddWithValue("@userid", arruser)
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
                                    .AddWithValue("@Action", "Edit Rights")
                                    .AddWithValue("@Date", System.DateTime.Now)
                                    .AddWithValue("@AssignTo", arruser)
                                    .AddWithValue("@Entity", "Menu")

                                    .AddWithValue("@DeptId", Session("dp"))
                                    If IsNumeric(clta) Then
                                        clta = clta
                                    Else
                                        clta = 0
                                    End If
                                    If IsNumeric(lobt) Then
                                        lobt = lobt
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

                            If lstSubhead2.Items.Count <> 0 Then   ' if 12

                                For k = 0 To arrsublink1.Length - 1
                                    If arrsublink1(k) <> "" Then   ' if 13

                                        'checking if menu is for user or not
                                        If type = 1 Then
                                            permit = CheckLimit(arrsublink1(k))
                                            If permit = "OA" Then
                                                dsmenuname = uspan.GetMenuName(arrsublink1(k))
                                                menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                                ShowConfirm(menuname & "is not permitted for user")
                                                ClearAll()
                                                Exit For
                                            End If
                                        End If

                                        If chkexists(arrsublink1(k), arruser, type) = True Then  ' if 14
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
                                                .AddWithValue("@userid", arruser)
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
                                                .AddWithValue("@Action", "Edit Rights")
                                                .AddWithValue("@Date", System.DateTime.Now)
                                                .AddWithValue("@AssignTo", arruser)
                                                .AddWithValue("@Entity", "Menu")

                                                .AddWithValue("@DeptId", Session("dp"))
                                                If IsNumeric(clta) Then
                                                    clta = clta
                                                Else
                                                    clta = 0
                                                End If
                                                If IsNumeric(lobt) Then
                                                    lobt = lobt
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
                                        If lstSubhead3.Items.Count <> 0 Then   ' if 6
                                            For l = 0 To arrsublink3.Length - 1    ' for arrsublink3
                                                If arrsublink3(l) <> "" Then  ' if 4

                                                    'checking if menu is for user or not
                                                    If type = 1 Then
                                                        permit = CheckLimit(arrsublink3(l))
                                                        If permit = "OA" Then
                                                            dsmenuname = uspan.GetMenuName(arrsublink3(l))
                                                            menuname = dsmenuname.Tables(0).Rows(0)("MenuDescription").ToString()
                                                            ShowConfirm(menuname & "is not permitted for user")
                                                            ClearAll()
                                                            Exit For
                                                        End If
                                                    End If

                                                    If chkexists(arrsublink3(l), arruser, type) = True Then  ' if 5
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
                                                            .AddWithValue("@userid", arruser)
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
                                                            .AddWithValue("@Action", "Edit Rights")
                                                            .AddWithValue("@Date", System.DateTime.Now)
                                                            .AddWithValue("@AssignTo", arruser)
                                                            .AddWithValue("@Entity", "Menu")

                                                            .AddWithValue("@DeptId", Session("dp"))
                                                            If IsNumeric(clta) Then
                                                                clta = clta
                                                            Else
                                                                clta = 0
                                                            End If
                                                            If IsNumeric(lobt) Then
                                                                lobt = lobt
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
                                                If chkexists(drget("MenuId").ToString, arruser, type) = True Then   ' if 3 
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
                                                        .AddWithValue("@userid", arruser)
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
                                                        .AddWithValue("@Action", "Edit Rights")
                                                        .AddWithValue("@Date", System.DateTime.Now)
                                                        .AddWithValue("@AssignTo", arruser)
                                                        .AddWithValue("@Entity", "Menu")
                                                        .AddWithValue("@DeptId", Session("dp"))
                                                        If IsNumeric(clta) Then
                                                            clta = clta
                                                        Else
                                                            clta = 0
                                                        End If
                                                        If IsNumeric(lobt) Then
                                                            lobt = lobt
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

                                Dim cmdget As New SqlCommand("select * from nlvl_menu where ParentId='" & arrlink1(j) & "'", con)
                                Dim drget As SqlDataReader
                                con.Open()
                                drget = cmdget.ExecuteReader
                                While drget.Read    ' while 2
                                    If chkexists(drget("MenuId").ToString, arruser, type) = True Then  'if 2
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
                                            .AddWithValue("@userid", arruser)
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
                                            .AddWithValue("@Action", "Edit Rights")
                                            .AddWithValue("@Date", System.DateTime.Now)
                                            .AddWithValue("@AssignTo", arruser)
                                            .AddWithValue("@Entity", "Menu")
                                            .AddWithValue("@DeptId", Session("dp"))
                                            If IsNumeric(clta) Then
                                                clta = clta
                                            Else
                                                clta = 0
                                            End If
                                            If IsNumeric(lobt) Then
                                                lobt = lobt
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
                                        If chkexists(drget1("MenuId").ToString, arruser, type) = True Then   'if 1
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
                                                .AddWithValue("@userid", arruser)
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
                                                .AddWithValue("@Action", "Edit Rights")
                                                .AddWithValue("@Date", System.DateTime.Now)
                                                .AddWithValue("@AssignTo", arruser)
                                                .AddWithValue("@Entity", "Menu")

                                                .AddWithValue("@DeptId", Session("dp"))
                                                If IsNumeric(clta) Then
                                                    clta = clta
                                                Else
                                                    clta = 0
                                                End If
                                                If IsNumeric(lobt) Then
                                                    lobt = lobt
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
        '    End If  ' end 15
        'Next  'arruser
        ShowConfirm("Rights has been updated successfully!")
        ClearAll()
        bindgvRights()


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
    ''' check the user menu rights
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
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        lstsubhead1.Items.Clear()
        lstSubhead2.Items.Clear()
        lstSubhead3.Items.Clear()
    End Sub
    ''' <summary>
    ''' bind main menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lstmainhead_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstmainhead.SelectedIndexChanged

        lstsubhead1.Items.Clear()
        lstSubhead2.Items.Clear()
        lstSubhead3.Items.Clear()

        If UserType = "Super Admin" Then
            lstsubhead1.DataSource = objMenu.Bind_LinkMenu(lstmainhead.SelectedValue)
            lstsubhead1.DataTextField = "MenuDescription"
            lstsubhead1.DataValueField = "MenuId"
            lstsubhead1.DataBind()
        End If

        If UserType = "Admin" Then
            lstsubhead1.DataSource = objMenu.Bind_LinkMenuAdmin(loggedId, lstmainhead.SelectedValue)
            lstsubhead1.DataTextField = "MenuDescription"
            lstsubhead1.DataValueField = "MenuId"
            lstsubhead1.DataBind()
        End If

        If UserType = "User" Then
            lstsubhead1.DataSource = objMenu.Bind_LinkMenuAdmin(loggedId, lstmainhead.SelectedValue)
            lstsubhead1.DataTextField = "MenuDescription"
            lstsubhead1.DataValueField = "MenuId"
            lstsubhead1.DataBind()
        End If
    End Sub
    ''' <summary>
    ''' bind sub menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub lstsubhead1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstsubhead1.SelectedIndexChanged

        lstSubhead2.Items.Clear()
        lstSubhead3.Items.Clear()


        If UserType = "Super Admin" Then
            lstSubhead2.DataSource = objMenu.Bind_SubLink1Menu(lstsubhead1.SelectedValue)
            lstSubhead2.DataTextField = "MenuDescription"
            lstSubhead2.DataValueField = "MenuId"
            lstSubhead2.DataBind()
        End If

        If UserType = "Admin" Then
            lstSubhead2.DataSource = uspan.Bind_SubLink1MenuAdmin(loggedId, lstsubhead1.SelectedValue)
            lstSubhead2.DataTextField = "MenuDescription"
            lstSubhead2.DataValueField = "MenuId"
            lstSubhead2.DataBind()

        End If

    End Sub
    ''' <summary>
    ''' bind sub menu of sub menu
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub lstSubhead2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstSubhead2.SelectedIndexChanged

        lstSubhead3.Items.Clear()

        If UserType = "Super Admin" Then
            lstSubhead3.DataSource = objMenu.Bind_SubLink2Menu(lstSubhead2.SelectedValue)
            lstSubhead3.DataTextField = "MenuDescription"
            lstSubhead3.DataValueField = "MenuId"
            lstSubhead3.DataBind()
        End If

        If UserType = "Admin" Then
            lstSubhead3.DataSource = uspan.Bind_SubLink2MenuAdmin(loggedId, lstSubhead2.SelectedValue)
            lstSubhead3.DataTextField = "MenuDescription"
            lstSubhead3.DataValueField = "MenuId"
            lstSubhead3.DataBind()

        End If

    End Sub
    ''' <summary>
    ''' check the type of user
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

        Else
            type = 1.ToString()
        End If
        con.Close()
        Return type
    End Function
    Public Function CheckLimit(ByVal menuid As String)
        Dim permit As String
        con.Open()
        Dim chkpermit As New SqlCommand("select limit from nlvl_menu  where menuid='" & menuid & "'", con)
        permit = chkpermit.ExecuteScalar().ToString
        con.Close()
        Return permit
    End Function
    ''' <summary>
    ''' update the rights
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim arruser As String
        Dim cnt As Integer = 0
        Dim i As Integer
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim type As String = "1"

        Panel1.Visible = False
        arruser = tbxUserId.Text
       
        con.Open()
        Dim cmdChkUser As New SqlCommand("Select userid,usertype from nlvl_menu_rights where userid='" & arruser & "' and usertype='" & type & "'", con)
        Dim rdrChkUser As SqlDataReader = cmdChkUser.ExecuteReader()
        If rdrChkUser.Read() Then
            rdrChkUser.Close()
            con.Close()
            con.Open()
            Dim cmdMenu As New SqlCommand("Select menuid from nlvl_menu_rights where userid='" & arruser & "' and usertype ='" & type & "'", con)
            Dim da As New SqlDataAdapter
            da.SelectCommand = cmdMenu
            Dim dsCount As New DataSet
            da.Fill(dsCount)
            Dim arrmenucount(dsCount.Tables(0).Rows.Count) As String
            For j = 0 To arrmenucount.Length - 2
                arrmenucount(j) = dsCount.Tables(0).Rows(j)("MenuId").ToString()
            Next
            For k = 0 To arrmenucount.Length - 2
                objMenu.DeleteUserRights(arrmenucount(k), type, arruser)
            Next
            objMenu.InsertUserRights(arruser, 1, loggedId)

        Else
            objMenu.InsertUserRights(arruser, 1, loggedId)

        End If
            
        ShowConfirm("Rights updated successfully ")
        ClearAll()
        bindgvRights()
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Panel1.Visible = False
        Dim RowItem As GridViewRow
        CType(gvRights.HeaderRow.FindControl("ChkSelectAll"), CheckBox).Checked = False
        For Each RowItem In gvRights.Rows
            'CType(RowItem.FindControl("ChkSelectAll"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkdel"), CheckBox).Checked = False

        Next
        ClearAll()

    End Sub
    ''' <summary>
    ''' 'bind menu list with lstmenu listbox
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindMenuAdmin()

        lstmainhead.DataSource = ObjMenu.Bind_Menu()
        lstmainhead.DataTextField = "MenuDescription"
        lstmainhead.DataValueField = "MenuId"
        lstmainhead.DataBind()
    End Sub
    ''' <summary>
    ''' check password reset status
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <param name="usertype"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckPasswordResetStatus(ByVal userid As String, ByVal usertype As String)
        Dim passwordreset As String
        Dim menuid As String = "105"
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
End Class
