Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class ChangeOwnership
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim conn As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartmentTable As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClientTable As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLobTable As DropDownList
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

        ddlDepartmentTable = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClientTable = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLobTable = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load


            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "DeptID"
            ddlDepartmentUser.DataSource = fun.bind_Dept()
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "--Select--")

            ddlDepartmentTable.DataTextField = "DepartmentName"
            ddlDepartmentTable.DataValueField = "DeptID"
            ddlDepartmentTable.DataSource = fun.bind_Dept()
            ddlDepartmentTable.DataBind()
            ddlDepartmentTable.Items.Insert(0, "--Select--")

        End If

    End Sub
    ''' <summary>
    ''' 'method called to bind tables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub btnGetTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetTable.Click

        BindTable()

    End Sub
    ''' <summary>
    ''' 'method called to bind users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click

        BindUser()

    End Sub
    ''' <summary>
    ''' bind the tables
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindTable()
        If ddlLobTable.SelectedIndex > 0 And ddlClientTable.SelectedIndex > 0 And ddlDepartmentTable.SelectedIndex > 0 Then

            lobid = Convert.ToInt32(ddlLobTable.SelectedValue)
            client_id = Convert.ToInt32(ddlClientTable.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentTable.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectTable.DataSource = uspan.tableselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelectTable.DataTextField = "TableName"
                ddlSelectTable.DataValueField = "TableId"
                ddlSelectTable.DataBind()
                ddlSelectTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If

            'If UserType = "User" Then
            '    ddlSelectTable.DataSource = uspan.bind_lobTableUser(deptid, client_id, lobid)
            '    ddlSelectTable.DataTextField = "TableName"
            '    ddlSelectTable.DataValueField = "TableId"
            '    ddlSelectTable.DataBind()
            '    ddlSelectTable.Items.Insert(0, "--Select--")
            '    Exit Sub
            'End If
        End If

        If ddlClientTable.SelectedIndex > 0 And ddlDepartmentTable.SelectedIndex > 0 Then

            client_id = Convert.ToInt32(ddlClientTable.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentTable.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectTable.DataSource = uspan.tableselectadminspan(loggedId, deptid, client_id, "0")
                ddlSelectTable.DataTextField = "TableName"
                ddlSelectTable.DataValueField = "TableId"
                ddlSelectTable.DataBind()
                ddlSelectTable.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            'If UserType = "User" Then
            '    ddlSelectTable.DataSource = uspan.bind_clientTableUser(deptid, client_id)
            '    ddlSelectTable.DataTextField = "TableName"
            '    ddlSelectTable.DataValueField = "TableId"
            '    ddlSelectTable.DataBind()
            '    ddlSelectTable.Items.Insert(0, "--Select--")
            '    Exit Sub

            'End If
        End If


        If ddlDepartmentTable.SelectedIndex > 0 Then

            deptid = ddlDepartmentTable.SelectedValue

            If UserType = "Admin" Then
                ddlSelectTable.DataSource = uspan.tableselectadminspan(loggedId, deptid, "0", "0")
                ddlSelectTable.DataTextField = "TableName"
                ddlSelectTable.DataValueField = "TableId"
                ddlSelectTable.DataBind()
                ddlSelectTable.Items.Insert(0, "--Select--")
            End If

            'If UserType = "User" Then
            '    ddlSelectTable.DataSource = uspan.bind_DeparmentTableUser(deptid)
            '    ddlSelectTable.DataTextField = "TableName"
            '    ddlSelectTable.DataValueField = "TableId"
            '    ddlSelectTable.DataBind()
            '    ddlSelectTable.Items.Insert(0, "--Select--")

            'End If
        End If
        Dim i As Integer = ddlDepartmentTable.SelectedIndex

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

    Protected Sub ddlSelectTable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectTable.SelectedIndexChanged

        Dim selectedtable As String
        Dim ds As New DataSet
        If ddlSelectTable.SelectedIndex <> -1 And ddlSelectTable.SelectedIndex <> 0 Then
            selectedtable = ddlSelectTable.SelectedValue
            ds = tableobj.Get_TableOwner(selectedtable)
            tbxPreviousOwner.Text = ds.Tables(0).Rows(0)("UserName").ToString()
            hdUserId.Value = ds.Tables(0).Rows(0)("UserId").ToString()
        Else
            tbxPreviousOwner.Text = ""
            hdUserId.Value = ""
        End If

    End Sub
    ''' <summary>
    ''' change the owner of table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click


        'validating values
        '-------------------------------------------------------------------

        If ddlDepartmentTable.SelectedIndex = 0 Then
            ShowConfirm("Select department to choose table")
            Exit Sub
        End If
        If (ddlSelectTable.SelectedIndex = -1) Or (ddlSelectTable.SelectedIndex = 0) Then
            ShowConfirm("Select table")
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

            Dim tableId As String = ddlSelectTable.SelectedValue
            Dim tablename As String = ddlSelectTable.SelectedItem.Text
            Dim newOwnerId As String = ddlNewOwner.SelectedValue

            '*************change*************

           
            Dim cmdins2 = New SqlCommand("TracktableOwnerchange", conn)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters


                .AddWithValue("@tableid", tableId)

                .AddWithValue("@actionby", Session("userid"))
                .AddWithValue("@action", "Change Owner")
                .AddWithValue("@date", System.DateTime.Now)

                .AddWithValue("@entity", "Table")
                .AddWithValue("@entityname", tablename)

                .AddWithValue("@deptid", ddlDepartmentTable.SelectedValue)
                Dim clta As Integer
                Dim lobt As Integer

                If IsNumeric(ddlClientTable.SelectedValue) Then
                    clta = ddlClientTable.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLobTable.SelectedValue) Then
                    lobt = ddlLobTable.SelectedValue
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
            Dim oldowner = hdUserId.Value
            '*************change*************
            tableobj.Update_TableOwnership(tableId, tablename, newOwnerId, oldowner)
            ShowConfirm("Table ownership changed successfully")
            ClearAll()



        Else
            fun.aspnet_msgbox("You are not authorised")
        End If
    End Sub
    ''' <summary>
    ''' clear the controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlClientTable.Items.Clear()
        ddlLobTable.Items.Clear()
        ddlNewOwner.Items.Clear()
        ddlSelectTable.Items.Clear()
        tbxPreviousOwner.Text = ""
        ddlDepartmentUser.SelectedIndex = 0
        ddlDepartmentTable.SelectedIndex = 0
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
End Class
