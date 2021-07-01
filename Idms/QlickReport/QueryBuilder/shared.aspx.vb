Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Data
Imports System.web.UI.Control

Partial Class QueryBuilder_shared
    Inherits System.Web.UI.Page
#Region "Variable Declaration"
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim con As New SqlConnection(constr)
    Dim connectionlst As New SqlConnection(constr)
    Public dataqry1
    Dim objds As New DataSet
    Dim cmd As New SqlCommand
    Dim isItemCommand As Boolean = True
    Dim isCheckChanged As Boolean = True
    Dim flag As Boolean = True
    Dim all As String = ""
    Dim old As String = ""
#End Region
    
#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.IsPostBack = False Then
            lblUser.Text = Session("Userid")
            bindQueryName()
        Else
            dgUser.Visible = True
        End If
    End Sub
#End Region
    
#Region "Control Event"

    Protected Sub cmdup_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdup.Click
        Dim strusername As String = ""
        Try

            If ddlQueryName.SelectedValue = "0" Then
                lblMessage.Text = "No Report Is Selected"
                lblMessage.Visible = True
                Exit Sub
            ElseIf dgUser.Items.Count = 0 Then
                lblMessage.Text = "No User Is Present To Share Report With! Get Users To Share Report"
                lblMessage.Visible = True
                Exit Sub

            Else
                Dim dgItem As DataGridItem
                Dim userId As String = ""

                For Each dgItem In Me.dgUser.Items
                    Dim arrUserRights() As String = {"False", "False", "False", "False"}
                    Dim arrControlName() As String = {"chkBoxView", "chkBoxEdit", "chkBoxDelete", "chkBoxSaveAs"}
                    If CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = True Then

                        Dim i As Int16

                        userId = CType(dgItem.FindControl("lblUserId"), Label).Text
                        strusername += userId + ","
                        For i = 0 To arrControlName.Length - 1
                            If CType(dgItem.FindControl(arrControlName(i)), CheckBox).Checked = True Then
                                arrUserRights(i) = "True"
                                all = "None"
                            End If
                        Next

                        If isRightsExist(userId, ddlQueryName.SelectedValue) Then
                            old = getRights(userId)        ' here we get the old rights 
                            If updateQueryRights(ddlQueryName.SelectedValue, ddlQueryName.SelectedItem.ToString(), userId, arrUserRights) Then
                                flag = True
                                all = getRights(userId)    ' here we get the new rights 
                            Else
                                lblMessage.Text = "Error Occured.Please try again"
                                flag = False
                                lblMessage.Visible = True
                            End If
                        Else
                            If insertQueryRights(ddlQueryName.SelectedValue, ddlQueryName.SelectedItem.ToString(), userId, arrUserRights) Then
                                flag = True
                                all = getRights(userId)       ' here we get the new rights 
                            Else
                                lblMessage.Text = "Error Occured.Please try again"
                                flag = False
                                lblMessage.Visible = True
                            End If
                        End If
                        '********************track code starts****************************
                        Dim strTableName As String = ""
                        Dim RecID As String = ""
                        Dim savedby As String = ""
                        Dim btnrights As New SqlCommand("Select tablename,recordid,savedby  from WARSQueryMaster where queryname='" & ddlQueryName.SelectedItem.Text & "'", connection)
                        connection.Open()
                        Dim btnreader As SqlDataReader = btnrights.ExecuteReader
                        While btnreader.Read

                            strTableName = (btnreader("tablename")).ToString
                            RecID = (btnreader("recordid")).ToString
                            savedby = (btnreader("savedby")).ToString
                        End While
                        connection.Close()
                        Dim com As New SqlCommand("insert_WARSQMRemSharTracks2", connection)
                        Dim strReportName As String = ddlQueryName.SelectedItem.Text

                        connection.Open()
                        com.CommandType = CommandType.StoredProcedure
                        With com.Parameters
                            '.AddWithValue("@UserId", Trim(Session("Userid")))
                            .AddWithValue("@Rec_ID", RecID)
                            .AddWithValue("@Action", "Shared")
                            .AddWithValue("@SavedBy", savedby)
                            .AddWithValue("@ReportName", strReportName)
                            .AddWithValue("@Table_Name", strTableName)
                            .AddWithValue("@CreatedOn", System.DateTime.Now())
                            '.AddWithValue("@Type", "Query")


                        End With
                        com.ExecuteNonQuery()
                        connection.Close()
                        com.Dispose()
                        '''''''''''''''Usertype check for track goes here:- By Suvidha

                        'Dim cmm As New SqlCommand("insert into Querybuilder_utype select MAX(Auto_id)," + Session("usertype") + " from Track_QueryBuilder where Query_Name='" + strReportName + "' and Action='Shared'", connection)
                        'connection.Open()
                        'cmm.ExecuteNonQuery()
                        'connection.Close()
                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                        all = ""
                        old = ""
                        '********************track code starts****************************
                    End If

                Next
                'If old = "" Then
                '    old = "None"
                'End If
                If strusername.EndsWith(",") Then
                    strusername = strusername.TrimEnd(",")
                End If

                If flag Then
                    lblMessage.Visible = True
                    lblMessage.Text = "Right(s) Assigned"
                    lblMessage.Visible = True
                End If

                If strusername = "" Then
                    lblMessage.Visible = True
                    lblMessage.Text = "Please select At Least One UserId."
                    flag = False
                End If
            End If


        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
        lblMessage.Visible = True
        ' change made on 1/07/09
        dgUser.CurrentPageIndex = 0
        bindDgUser()
    End Sub
    

    'Protected Sub cbodept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbodept.SelectedIndexChanged
    '    cboclient.Items.Clear()
    '    cbolob.Items.Clear()
    '    ddlQueryName.Items.Clear()
    '    If cbodept.SelectedIndex <> 0 Then
    '        bindClient(cbodept, cboclient)
    '        bindLob(cbodept, cboclient, cbolob)
    '        bindQueryName()
    '    End If
    'End Sub

    'Protected Sub cboclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboclient.SelectedIndexChanged
    '    cbolob.Items.Clear()
    '    ddlQueryName.Items.Clear()
    '    If cbodept.SelectedIndex <> 0 Then
    '        bindLob(cbodept, cboclient, cbolob)

    '    End If
    '    bindQueryName()
    'End Sub

    'Protected Sub cbolob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolob.SelectedIndexChanged
    '    bindQueryName()
    'End Sub

    'Protected Sub ddlBuddyDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBuddyDepartment.SelectedIndexChanged
    '    If ddlBuddyDepartment.SelectedIndex = 0 Then
    '        ddlBuddyClient.Items.Clear()
    '        ddlBuddyLob.Items.Clear()
    '        dgUser.Visible = False
    '    Else
    '        dgUser.Visible = False
    '        bindClient(ddlBuddyDepartment, ddlBuddyClient)
    '        bindLob(ddlBuddyDepartment, ddlBuddyClient, ddlBuddyLob)
    '    End If
    'End Sub

    'Protected Sub ddlBuddyClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlBuddyClient.SelectedIndexChanged
    '    If ddlBuddyClient.SelectedIndex = 0 Then
    '        ddlBuddyLob.Items.Clear()
    '        dgUser.Visible = False
    '    Else
    '        dgUser.Visible = False
    '        bindLob(ddlBuddyDepartment, ddlBuddyClient, ddlBuddyLob)
    '    End If
    'End Sub

    'Protected Sub ddlQueryName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlQueryName.SelectedIndexChanged
    '    If ddlQueryName.SelectedValue <> "0" Then
    '        lblMessage.Visible = False
    '    End If
    'End Sub

    ''' <summary>
    ''' Displays Users And Buddy of a User
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 

    Protected Sub btnGetBudy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetBudy.Click
        If ddlQueryName.SelectedIndex <> 0 Then
            'change done 0n 01/07/09
            dgUser.CurrentPageIndex = 0

            bindDgUser()
        Else
            lblMessage.Visible = True
            lblMessage.Text = "Please Select Report First."
        End If
    End Sub

    Protected Sub dgUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgUser.ItemCommand
        isItemCommand = False
        If e.CommandName = "selectAll" Then
            selectDeSelectCheckBoxes(True)
        ElseIf e.CommandName = "deSelectAll" Then
            selectDeSelectCheckBoxes(False)
        End If

    End Sub

    ''' <summary>
    ''' Check/UnCheck all the Boxes acording to check box item is checked or not
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Public Sub chkBoxItem_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        isCheckChanged = False
        Dim selectedCheckBox As CheckBox
        selectedCheckBox = CType(sender, CheckBox)
        'Dim dtRow As IDataItemContainer()
        Dim dgItem As DataGridItem
        dgItem = CType(selectedCheckBox.Parent.Parent, DataGridItem)
        If (CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = True) Then
            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = True
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = True
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = True
        Else
            CType(dgItem.FindControl("chkBoxView"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = False
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = False
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Check/UnCheck all the Boxes acording to check box view is checked or not
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub chkBoxView_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        isCheckChanged = False
        Dim chkBoxView As CheckBox
        Dim dgItem As DataGridItem
        chkBoxView = CType(sender, CheckBox)
        dgItem = CType(chkBoxView.Parent.Parent, DataGridItem)
        If (CType(dgItem.FindControl("chkBoxView"), CheckBox).Checked = True) Then
            CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = True
            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = True
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = True
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = True
        Else

            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = False
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = False
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Checked = False
            CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = False
        End If
    End Sub

    Protected Sub dgUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgUser.PageIndexChanged

        If dgUser.CurrentPageIndex < dgUser.PageCount And dgUser.CurrentPageIndex >= 0 Then
            dgUser.CurrentPageIndex = e.NewPageIndex

            bindDgUser()
            '//added
            ' If isItemCommand And isCheckChanged And flag Then
            Dim objQB As New QueryBuilder()
            Dim sharedUserDS As New DataSet
            Dim dgItem As DataGridItem
            If ddlQueryName.SelectedValue = "Select" Then
                Exit Sub
            End If
            sharedUserDS = objQB.getUserWithRights(ddlQueryName.SelectedValue)
            If sharedUserDS.Tables(0).Rows.Count > 0 Then
                Dim i As Int16
                For i = 0 To sharedUserDS.Tables(0).Rows.Count - 1
                    For Each dgItem In dgUser.Items
                        'changed on 30/06/09
                        CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = True
                        If CType(dgItem.FindControl("lblUserId"), Label).Text.Trim() = sharedUserDS.Tables(0).Rows(i)(0) Then
                            Dim j As Int16 = 0
                            Dim arrControlName() As String = {"chkBoxView", "chkBoxEdit", "chkBoxDelete", "chkBoxSaveAs"}
                            Dim chkBoxRights As CheckBox
                            If (sharedUserDS.Tables(0).Rows(i)(1) = "True") Then
                                While (j <> 4)

                                    chkBoxRights = CType(dgItem.FindControl(arrControlName(j)), CheckBox)
                                    chkBoxRights.Enabled = True
                                    chkBoxRights.Checked = CType(sharedUserDS.Tables(0).Rows(i)(j + 1), Boolean)
                                    j += 1
                                End While
                            End If
                            Exit For
                        End If
                    Next
                Next
            End If
            'End If
        End If
        '//added
    End Sub

    
    ''' <summary>
    ''' Checks if user already has rights assigned to him 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' 
    Protected Sub dgUser_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles dgUser.PreRender
        If isItemCommand And isCheckChanged And flag Then

            Dim objQB As New QueryBuilder()
            Dim sharedUserDS As New DataSet
            Dim dgItem As DataGridItem
            If ddlQueryName.SelectedValue = "--Select--" Then
                Exit Sub
            End If
            sharedUserDS = objQB.getUserWithRights(ddlQueryName.SelectedValue)
            If sharedUserDS.Tables(0).Rows.Count > 0 Then
                Dim i As Int16
                For i = 0 To sharedUserDS.Tables(0).Rows.Count - 1
                    For Each dgItem In dgUser.Items


                        If CType(dgItem.FindControl("lblUserId"), Label).Text.Trim() = sharedUserDS.Tables(0).Rows(i)(0) Then
                            CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = True
                            Dim j As Int16 = 0
                            Dim arrControlName() As String = {"chkBoxView", "chkBoxEdit", "chkBoxDelete", "chkBoxSaveAs"}
                            Dim chkBoxRights As CheckBox
                            If (sharedUserDS.Tables(0).Rows(i)(1) = "True") Then
                                While (j <> 4)

                                    chkBoxRights = CType(dgItem.FindControl(arrControlName(j)), CheckBox)
                                    chkBoxRights.Enabled = True
                                    chkBoxRights.Checked = CType(sharedUserDS.Tables(0).Rows(i)(j + 1), Boolean)
                                    j += 1
                                End While
                            End If
                            Exit For
                        End If
                    Next
                Next
            End If
        End If
    End Sub

#End Region

#Region " User Functions"
    Private Sub ddldept_bind(ByVal ddlDept As DropDownList)
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
        Try
            Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
            Dim da As New SqlDataAdapter
            da.SelectCommand = comdepart
            Dim ds As New DataSet
            connection.Open()
            da.Fill(ds)
            connection.Close()
            ddlDept.DataTextField = "DepartmentName"
            ddlDept.DataValueField = "autoid"
            ddlDept.DataSource = ds
            ddlDept.DataBind()
            ddlDept.Items.Insert("0", "--Select--")
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub
    Private Sub bindClient(ByVal ddlDept As DropDownList, ByVal ddlClient As DropDownList)
        Try
            Dim cmdst As New SqlCommand("select *  from idmsclient where DeptId='" & ddlDept.SelectedValue & "' ", connection)
            Dim dsst As New DataSet
            Dim adpst As New SqlDataAdapter
            adpst.SelectCommand = cmdst
            connection.Open()
            adpst.Fill(dsst)
            connection.Close()
            ddlClient.DataSource = dsst
            ddlClient.DataTextField = "ClientName"
            ddlClient.DataValueField = "autoid"
            ddlClient.DataBind()
            ddlClient.Items.Insert("0", New ListItem("--Select--", "0"))
            ''''''''''''''''''''''''''''

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub bindLob(ByVal ddlDept As DropDownList, ByVal ddlClient As DropDownList, ByVal ddlLob As DropDownList)
        Try
            Dim cmdst As New SqlCommand("select autoid,LOBName  from warslobmaster where DeptId='" + ddlDept.SelectedValue + "' and ClientId='" + ddlClient.SelectedValue + "'", connection)
            Dim dsst As New DataSet
            Dim adpst As New SqlDataAdapter
            adpst.SelectCommand = cmdst
            connection.Open()
            adpst.Fill(dsst)
            connection.Close()
            ddlLob.DataSource = dsst
            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "autoid"
            ddlLob.DataBind()
            ddlLob.Items.Insert("0", New ListItem("--Select--", "0"))
            ''''''''''''''''''''''''''''

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub bindQueryName()
        ddlQueryName.DataTextField = "QueryName"
        ddlQueryName.DataValueField = "recordId"
        Dim objRptDsnr As New ReportDesigner()
        Dim objQB As New QueryBuilder()
        If (Session("typeofuser") = "Admin") Then

            If objRptDsnr.chkAdminSpan(Session("userid")) Then
                ddlQueryName.DataSource = objQB.queryForAdmin()
                ddlQueryName.DataBind()
            Else
                ddlQueryName.DataSource = objQB.queryForUser(Request("userid"))
                ddlQueryName.DataBind()
            End If
        Else
            ddlQueryName.DataSource = objQB.queryForUser(Request("userid"))
            ddlQueryName.DataBind()
        End If

        If ddlQueryName.Items.Count = 0 Then
            ddlQueryName.Items.Insert(0, New ListItem("No Query Within Span", "0"))
            lblMessage.Text = "This User Does Not Own Any Report Within This Span."
            lblMessage.Visible = True
        Else
            ddlQueryName.Items.Insert(0, New ListItem("--Select--", "0"))
            lblMessage.Visible = False
        End If
    End Sub

    Private Sub bindDgUser()
        If Session("userid") = "" Then
            lblMessage.Text = "Session Expired. Please Login Again."
            lblMessage.Visible = True
        Else
            Dim objcmd As New SqlCommand
            Dim objadp As New SqlDataAdapter
            Dim objds As New DataSet
            Dim str4 = ""
            'If ddlBuddyDepartment.SelectedIndex <> 0 Then
            '    Dim deptid = ddlBuddyDepartment.SelectedValue.ToString
            '    Dim clientid = "0"
            '    Dim lobid = "0"

            '    If Session("typeofuser") = "User" Then
            '        str4 = "select userid ,'('+username+')' as username from registration where(( deptid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "' and (deptid in (select departmentid from buddy where userid='" + Session("userid") + "' and departmentid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "' and userbuddy='0') and clientid in (select clientid from buddy where userid='" + Session("userid") + "' and departmentid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "' and userbuddy='0') and lobid in (select lobid from buddy where userid='" + Session("userid") + "' and departmentid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "' and userbuddy='0')))) order by username"
            '    ElseIf Session("typeofuser") = "Admin" Then
            '        Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
            '        cmdupdate.CommandType = CommandType.StoredProcedure
            '        With cmdupdate.Parameters
            '            .AddWithValue("@userid", Session("userid"))
            '            .AddWithValue("@Deptid", deptid)
            '            .AddWithValue("@Clientid", clientid)
            '            .AddWithValue("@LOBID", lobid)
            '        End With
            '        Dim readerdata As SqlDataReader
            '        con.Open()
            '        readerdata = cmdupdate.ExecuteReader


            '        If readerdata.HasRows Then
            '            str4 = "select distinct userid ,'('+username+')' as username from buddy where( departmentid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "' and userid<>'" + Session("userid") + "') order by username"

            '        Else
            '            lblMessage.Text = "You are not admin of this span"
            '            lblMessage.Visible = True
            '            dgUser.DataSource = Nothing
            '            dgUser.DataBind()
            '            dgUser.Visible = False
            '            Exit Sub
            '        End If
            '        readerdata.Close()
            '        con.Close()
            '    End If
            '    'or userid in (select userid from  buddy where userid='" + Session("userid") + "' and userbuddy='0' and departmentid='" + deptid + "' and clientid='" + clientid + "' and lobid='" + lobid + "') or userid in(select userid from buddy where userbuddy='" + Session("userid") + "')or  userid in(select userbuddy from buddy where userid='" + Session("userid") + "' and userbuddy<>'0')))"
            '    objcmd = New SqlCommand(str4, connection)
            'Else
            'change made on 1/07/09
            str4 = "select userid,'('+username+')' as username from registration where userid in(select userbuddy from buddy where userid='" + Session("userid") + "' and userbuddy<>'0')or userid in(select userid from buddy where userbuddy='" + Session("userid") + "') order by username"
            objcmd = New SqlCommand(str4, connection)
            'End If
            Try
                'Dim objcmd As New SqlCommand("Select distinct(username),userid,recid from WARSmemregistration where DepartmentId='" & ddlDepartmentuser.SelectedValue & "' and ClientId='" & ddlClientuser.SelectedValue & "' and LobDept='" & ddlLobuser.SelectedValue & "' ", connection)


                connection.Open()
                objadp.SelectCommand = objcmd
                objadp.Fill(objds)
                connection.Close()
                If objds.Tables(0).Rows.Count <> 0 Then
                    dgUser.DataSource = objds
                    dgUser.DataBind()
                    dgUser.Visible = True
                    lblMessage.Visible = False
                    'msg.Value = "nowcheck"
                Else
                    lblMessage.Text = "No User Buddy Found."
                    lblMessage.Visible = True
                    dgUser.Visible = False
                End If
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                lblMessage.Text = strmsg
                lblMessage.Visible = True
            End Try
        End If

    End Sub
    
    

    Public Sub winclose()
        Dim close As New System.Text.StringBuilder
        close.Append("<Script language=javascript>")
        close.Append("window.close();")
        close.Append("</Script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "winclose", close.ToString)
    End Sub

    Public Sub SetFocus(ByVal FocusControl As Control)
        ''''''''''''''this function is for setting the focus''''''''''''''''''''
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Page.GetType(), "setFocus", Script.ToString())
    End Sub
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1
    End Function
    Public Function ddlQueryName_bind()
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
        Try
           
            'Dim query As String = "select * from warsquerymaster where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and (savedby='" & Session("userid") & "' or ',' + sharedwith + ',' like '%," & Session("userid") & ",%')  and ',' + isnull(removesharing,'') + ',' not like '%," & Session("userid") & ",%'"
            Dim query As String = "select * from warsquerymaster where savedby='" & Session("userid") & "' or ',' + sharedwith + ',' like '%," & Session("userid") & ",%'"
            Dim objCbo1Cmd As New SqlCommand
            Dim adp As New SqlDataAdapter
            Dim ds As New DataSet
            objCbo1Cmd.Connection = connection
            objCbo1Cmd.CommandText = query
            adp.SelectCommand = objCbo1Cmd
            connection.Open()
            adp.Fill(ds, "warsquerymaster")
            connection.Close()
            ddlQueryName.DataSource = ds
            ddlQueryName.DataValueField = "recordId"
            ddlQueryName.DataTextField = "queryName"
            ddlQueryName.DataBind()
            ddlQueryName.Items.Insert(0, "--select--")
            'SetFocus(ddlTableName)
            objCbo1Cmd.Dispose()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
        Return 1
    End Function
    Public Function updateQueryRights(ByVal recordid As Integer, ByVal ReportName As String, ByVal userid As String, ByVal arrUserRights() As String)
        Try
            cmd = New SqlCommand("sp_updatequeryrights", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
            cmd.Parameters("@UserId").Value = userid
            'cmd.Parameters.Add("@queryname", SqlDbType.VarChar, 50)
            'cmd.Parameters("@queryname").Value = ReportName
            cmd.Parameters.Add("@RecordId", SqlDbType.Int, 18)
            cmd.Parameters("@RecordId").Value = recordid
            cmd.Parameters.Add("@View", SqlDbType.VarChar, 10)
            cmd.Parameters("@View").Value = arrUserRights(0)
            cmd.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
            cmd.Parameters("@Edit").Value = arrUserRights(1)
            cmd.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
            cmd.Parameters("@Delete").Value = arrUserRights(2)
            cmd.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
            cmd.Parameters("@SaveAs").Value = arrUserRights(3)
            cmd.Parameters.Add("@CurrDate", SqlDbType.DateTime)
            cmd.Parameters("@CurrDate").Value = System.DateTime.Now()
            cmd.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@AssignedBy").Value = Session("userId")
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Return False
        End Try
        cmd.Dispose()
        Return True
    End Function
    Public Function isRightsExist(ByVal userid As String, ByVal recordid As Integer) As Boolean

        Dim rdr As SqlDataReader
        cmd = New SqlCommand("sp_CheckExistingqueryRight", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        cmd.Parameters("@userid").Value = userid
        cmd.Parameters.Add("@recordid", SqlDbType.Int, 50)
        cmd.Parameters("@recordid").Value = recordid
        con.Open()
        rdr = cmd.ExecuteReader()

        cmd.Dispose()
        Dim st As String = ""
        If rdr.HasRows Then
            rdr.Close()
            con.Close()
            Return True
        Else
            rdr.Close()
            con.Close()
            Return False
        End If
    End Function
    Private Function insertQueryRights(ByVal recordid As Integer, ByVal ReportName As String, ByVal userid As String, ByVal arrUserRights() As String) As Boolean
        Try
            cmd = New SqlCommand("sp_warsquerysharerights", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@recordid", SqlDbType.Int, 18)
            cmd.Parameters("@recordid").Value = recordid
            cmd.Parameters.Add("@userid", SqlDbType.VarChar, 50)
            cmd.Parameters("@userid").Value = userid
            cmd.Parameters.Add("@queryname", SqlDbType.VarChar, 50)
            cmd.Parameters("@queryname").Value = ReportName
            cmd.Parameters.Add("@view", SqlDbType.VarChar, 10)
            cmd.Parameters("@view").Value = arrUserRights(0)
            cmd.Parameters.Add("@edit", SqlDbType.VarChar, 10)
            cmd.Parameters("@edit").Value = arrUserRights(1)
            cmd.Parameters.Add("@delete", SqlDbType.VarChar, 10)
            cmd.Parameters("@delete").Value = arrUserRights(2)
            cmd.Parameters.Add("@saveAs", SqlDbType.VarChar, 10)
            cmd.Parameters("@saveAs").Value = arrUserRights(3)
            cmd.Parameters.Add("@currdate", SqlDbType.DateTime)
            cmd.Parameters("@currdate").Value = System.DateTime.Now()
            cmd.Parameters.Add("@assignedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@assignedBy").Value = Session("userid")
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Return False
        End Try
        cmd.Dispose()
        Return True
    End Function

    Private Sub selectDeSelectCheckBoxes(ByVal selectDeSelect As Boolean)
        Dim dgItem As DataGridItem
        For Each dgItem In dgUser.Items
            CType(dgItem.FindControl("chkBoxItem"), CheckBox).Checked = selectDeSelect
            CType(dgItem.FindControl("chkBoxView"), CheckBox).Checked = selectDeSelect
            If selectDeSelect Then
                CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = True
                CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = True
                CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = True
            Else
                CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Checked = False
                CType(dgItem.FindControl("chkBoxEdit"), CheckBox).Enabled = False
                CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Checked = False
                CType(dgItem.FindControl("chkBoxSaveAs"), CheckBox).Enabled = False
                CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Checked = False
                CType(dgItem.FindControl("chkBoxDelete"), CheckBox).Enabled = False
            End If
            
        Next
    End Sub
#End Region


    Public Function getRights(ByVal userid As String)
        Dim EditRights As String
        Dim DeleteRights As String
        Dim SaveRights As String
        Dim viewrights As String
        Dim all As String = ""
        Dim btnrights As New SqlCommand("Select Edit ,[Delete] as TabDelete ,SaveAs ,[View] as TabView  from warsquerysharerights where QueryName='" & ddlQueryName.SelectedItem.Text & "' and userid='" & userid & " '", connection)
        connection.Open()
        Dim btnreader As SqlDataReader = btnrights.ExecuteReader
        While btnreader.Read

            viewrights = (btnreader("TabView")).ToString
            If (viewrights = "True") Then
                If all = "" Then
                    all = "View"
                Else
                    all = all & "," & "View"
                End If
            End If

            EditRights = (btnreader("Edit")).ToString
            If (EditRights = "True") Then
                If all = "" Then
                    all = "Edit"
                Else
                    all = all & "," & "Edit"
                End If
            End If

            DeleteRights = (btnreader("TabDelete")).ToString
            If (DeleteRights = "True") Then
                If all = "" Then
                    all = "Delete"
                Else
                    all = all & "," & "Delete"
                End If
            End If

            SaveRights = (btnreader("SaveAs")).ToString
            If (SaveRights = "True") Then
                If all = "" Then
                    all = "SaveAs"
                Else
                    all = all & "," & "SaveAs"
                End If
            End If
        End While
        connection.Close()
        btnreader.Close()
        Return all

    End Function
    

    
    
    
   
   
End Class
