Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class _Default
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")
    Dim str1 As String = AppSettings("ConnectionString1")
    Dim con As New SqlConnection(str)
    Dim conn As New SqlConnection(str)
    Dim uspan As New UserSpan
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
    Dim objtable As New TableRight
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

        ddlDepartmentTable = CType(DCLUserControl4.FindControl("ddlDepartment"), DropDownList)
        ddlClientTable = CType(DCLUserControl4.FindControl("ddlClient"), DropDownList)
        ddlLobTable = CType(DCLUserControl4.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load


            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

            ddlDepartmentTable.DataTextField = "DepartmentName"
            ddlDepartmentTable.DataValueField = "DeptID"
            ddlDepartmentTable.DataSource = fun.bind_Dept()
            ddlDepartmentTable.DataBind()
            ddlDepartmentTable.Items.Insert(0, "--Select--")
        End If

        Me.Form.DefaultButton = Me.btnAssignRight.UniqueID


    End Sub
    ''' <summary>
    '''  assign table rights to selected users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAssignRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssignRight.Click

        Dim i As Integer
        Dim j As Integer
        Dim UserId(20) As String
        Dim TableId(20) As String
        Dim view(20) As String
        Dim edit(20) As String
        Dim isUserSelected As Boolean
        Dim delete(20) As String
        Dim deleteData(20) As String
        Dim addColumn(20) As String
        Dim importdata(20) As String
        Dim isTableSelected As Boolean
        Dim selection As CheckBox
        Dim viewRight As CheckBox
        Dim editRight As CheckBox
        Dim deleteRight As CheckBox
        Dim deleteDataRight As CheckBox
        Dim addColumnRight As CheckBox
        Dim importdataRight As CheckBox
        Dim assignedby As String
        Static count As Integer


        'checking department for user is selected or not
        'If (ddlDepartment.SelectedIndex = 0) Then
        '    ShowConfirm("Please select department to choose user ")
        '    Exit Sub
        'End If
        If (ddlDepartmentTable.SelectedIndex = 0) Then
            ShowConfirm("Please select department to choose Tables ")
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

        'storing table rights informations in variables
        isTableSelected = False

        For i = 0 To gvTables.Rows.Count - 1

            selection = CType(gvTables.Rows(i).FindControl("chkTable"), CheckBox)
            If (selection.Checked = True) Then
                isTableSelected = True
                viewRight = CType(gvTables.Rows(i).FindControl("chkView"), CheckBox)
                editRight = CType(gvTables.Rows(i).FindControl("chkEdit"), CheckBox)
                deleteRight = CType(gvTables.Rows(i).FindControl("chkDelete"), CheckBox)
                deleteDataRight = CType(gvTables.Rows(i).FindControl("chkDeleteData"), CheckBox)
                addColumnRight = CType(gvTables.Rows(i).FindControl("chkAddColumn"), CheckBox)
                importdataRight = CType(gvTables.Rows(i).FindControl("chkimportdata"), CheckBox)

                TableId(i) = gvTables.Rows(i).Cells(2).Text
                If (viewRight.Checked = True) Then
                    view(i) = True
                Else
                    view(i) = False
                    count = count + 1
                End If

                If (editRight.Checked = True) Then
                    edit(i) = True
                Else
                    edit(i) = False
                    count = count + 1
                End If

                If (deleteRight.Checked = True) Then
                    delete(i) = True
                Else
                    delete(i) = False
                    count = count + 1
                End If

                If (deleteDataRight.Checked = True) Then
                    deleteData(i) = True
                Else
                    deleteData(i) = False
                    count = count + 1
                End If

                If (addColumnRight.Checked = True) Then
                    addColumn(i) = True
                Else
                    addColumn(i) = False
                    count = count + 1
                End If

                If (importdataRight.Checked = True) Then
                    importdata(i) = True
                Else
                    importdata(i) = False
                    count = count + 1
                End If
            End If
            If count = 6 Then
                ShowConfirm("Select Rights on selected Tables")
                Exit Sub
            Else
                count = 0
            End If
        Next

        If (isTableSelected = False) Then
            ShowConfirm("Please select Tables ")
            Exit Sub
        End If
        assignedby = loggedId

        'to stop duplicate values

        For i = 0 To UserId.Length - 1
            If UserId(i) <> "" Then
                For j = 0 To TableId.Length - 1
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
                    If TableId(j) <> "" Then
                        con.Open()
                        Dim comDup As New SqlCommand("select tableid,userid from IDMS_TableRights where tableid='" & TableId(j) & "' and userid ='" & UserId(i) & "'", con)
                        Dim rdrDup As SqlDataReader = comDup.ExecuteReader()
                        If rdrDup.Read() Then
                            uspan.Update_TableRights(UserId(i), TableId(j), view(j), edit(j), delete(j), deleteData(j), addColumn(j), importdata(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                            If edit(j) = True Then
                                If all = "" Then
                                    all = "Edit"
                                Else
                                    all = all & "," & "Edit"
                                End If
                            End If
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                            If deleteData(j) = True Then
                                If all = "" Then
                                    all = "DeleteData"
                                Else
                                    all = all & "," & "DeleteData"
                                End If
                            End If
                            If addColumn(j) = True Then
                                If all = "" Then
                                    all = "AddColumn"
                                Else
                                    all = all & "," & "AddColumn"
                                End If
                            End If
                            If importdata(j) = True Then
                                If all = "" Then
                                    all = "ImportData"
                                Else
                                    all = all & "," & "ImportData"
                                End If
                            End If
                            Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + TableId(j) + "'", conn)
                            Dim tableuse As String = ""
                            conn.Open()
                            tableuse = cmduse.executescalar()
                            conn.Close()
                            Dim cmdins2 = New SqlCommand("TrackRightsTable", conn)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters


                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Edit Rights")
                                .AddWithValue("@Date", System.DateTime.Now)
                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Table")
                                .AddWithValue("@EntityName", tableuse)

                                .AddWithValue("@DeptId", dept)
                               
                                .AddWithValue("@ClientId", clta)
                                .AddWithValue("@LOBId", lobt)
                                .AddWithValue("@All", all)
                                .AddWithValue("@type", Session("usertype"))
                            End With
                            conn.Open()
                            cmdins2.ExecuteNonQuery()
                            conn.Close()
                            cmdins2.Dispose()
                            '*************change*************
                        Else
                            uspan.Insert_TableRights(UserId(i), TableId(j), view(j), edit(j), delete(j), deleteData(j), addColumn(j), importdata(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                            If edit(j) = True Then
                                If all = "" Then
                                    all = "Edit"
                                Else
                                    all = all & "," & "Edit"
                                End If
                            End If
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                            If deleteData(j) = True Then
                                If all = "" Then
                                    all = "DeleteData"
                                Else
                                    all = all & "," & "DeleteData"
                                End If
                            End If
                            If addColumn(j) = True Then
                                If all = "" Then
                                    all = "AddColumn"
                                Else
                                    all = all & "," & "AddColumn"
                                End If
                            End If
                            If importdata(j) = True Then
                                If all = "" Then
                                    all = "ImportData"
                                Else
                                    all = all & "," & "ImportData"
                                End If
                            End If
                            Dim cmduse = New SqlCommand("select tablename from warslobtablemaster where tableid='" + TableId(j) + "'", conn)
                            Dim tableuse As String = ""
                            conn.Open()
                            tableuse = cmduse.executescalar()
                            conn.Close()
                            Dim cmdins2 = New SqlCommand("TrackRightsTable", conn)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters


                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)
                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Table")
                                .AddWithValue("@EntityName", tableuse)

                                .AddWithValue("@DeptId", dept)
                               
                                .AddWithValue("@ClientId", clta)
                                .AddWithValue("@LOBId", lobt)
                                .AddWithValue("@All", all)
                                .AddWithValue("@type", Session("usertype"))
                            End With
                            conn.Open()
                            cmdins2.ExecuteNonQuery()
                            conn.Close()
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
    ''' <summary>
    ''' 'Method to bind user according to selection
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
    ''' bind table 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnTable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTable.Click
        btnAssignRight.Visible = False
        BindTable()

        If gvTables.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If

        btnAssignRight.Visible = True

    End Sub

    ''' <summary>
    ''' 'Method to bind user to gridview that show user list
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
    '''     'Method to bind table to gridview that show table list
    ''' </summary>
    ''' <remarks></remarks>

    Protected Sub BindTable()
        If ddlDepartmentTable.SelectedIndex <> 0 Then
            gvTables.Visible = True

            If ddlLobTable.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLobTable.SelectedValue)
            Else
                lobid = "0"
            End If
            If ddlClientTable.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ddlClientTable.SelectedValue)
            Else
                client_id = "0"
            End If
            deptid = Convert.ToInt32(ddlDepartmentTable.SelectedValue)

            If UserType = "Admin" Then
                gvTables.Columns(2).Visible = True
                gvTables.DataSource = uspan.tableselectadminspan(loggedId, deptid, client_id, lobid)
                gvTables.DataBind()
                gvTables.Columns(2).Visible = False
                Exit Sub
            End If
            If UserType = "User" Then
                gvTables.Columns(2).Visible = True
                gvTables.DataSource = uspan.tableForuser(loggedId, deptid, client_id, lobid)
                gvTables.DataBind()
                gvTables.Columns(2).Visible = False
                Exit Sub
            End If




        End If
    End Sub
    ''' <summary>
    ''' paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUsers.PageIndexChanging

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
    Protected Sub grdvTables_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvTables.PageIndexChanging

        If gvTables.PageIndex < gvTables.PageCount And gvTables.PageIndex >= 0 Then
            gvTables.PageIndex = e.NewPageIndex
            BindTable()
        End If

    End Sub

   
    ''' <summary>
    ''' 'if no record is found then message will be displayed
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub NoRecord()
        Dim dtTable As New DataTable
        dtTable.Rows.Add(dtTable.NewRow())
        gvUsers.DataSource = dtTable
        gvUsers.DataBind()
        Dim TotalColumn As Integer
        TotalColumn = gvUsers.Columns.Count
        gvUsers.Rows(0).Cells.Clear()
        gvUsers.Rows(0).Cells.Add(New TableCell)
        gvUsers.Rows(0).Cells(0).ColumnSpan = TotalColumn
        gvUsers.Rows(0).Cells(0).Text = "No Record Found"
    End Sub
    ''' <summary>
    ''' to check the checkbox is checked or not
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub UncheckTables()
        Dim RowItem As GridViewRow
        For Each RowItem In gvTables.Rows
            CType(RowItem.FindControl("chkTable"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkView"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkEdit"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkDelete"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkDeleteData"), CheckBox).Checked = False
            CType(RowItem.FindControl("chkAddColumn"), CheckBox).Checked = False
        Next
    End Sub

    Protected Sub gvTables_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvTables.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:SelectAll('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If
    End Sub

    Protected Sub gvUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUsers.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:Select('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If
    End Sub
    ''' <summary>
    '''  to clear the controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClient.ClearSelection()
        ddlLob.ClearSelection()
        ddlClientTable.ClearSelection()
        ddlLobTable.ClearSelection()
        gvUsers.DataSource = Nothing
        gvUsers.DataBind()
        gvTables.DataSource = Nothing
        gvTables.DataBind()
        ddlDepartment.ClearSelection()
        ddlDepartmentTable.ClearSelection()
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
    ''' clear ddlLobTable
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl4.Load

        If ddlDepartmentTable.SelectedIndex = 0 Then
            ddlClientTable.Items.Clear()
            ddlLobTable.Items.Clear()

        End If

        If ddlClientTable.SelectedIndex = 0 Then
            ddlLobTable.Items.Clear()

        End If
    End Sub
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

End Class
