Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data.Sql

Partial Class Accounts_Search
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connection3 As New SqlConnection(constr)
    Dim connection4 As New SqlConnection(constr)
    Dim classobj As New Functions
    Dim connectionlob As New SqlConnection(constr)
    Dim ObjLib As New Library
    Dim ds As New DataSet
    Dim Prefix As String
    Dim RecordId As Integer
    Dim DepartId As String
    Dim FirstClientId As String
    Dim LobId As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("userid") = "22499"
        'Session("typeofuser") = "Admin"

        If Session("status") = "True" Then
            cmdUpdate.Enabled = False

            cmdUpdate.Focus()

        End If
    End Sub

    Private Sub WARSSearchMemReg()
        Dim Qstr As String = ""
        Me.lblMsgBox.Text = ""
        Me.lblMsgBox.Text = ""
        If cboSearchCriteria.SelectedValue = "--Select--" Then
            WARSShowMsg("Please select some criteria!!!")
            Exit Sub
        End If


        If txtSearch.Text <> "" Then
            If cboSearchCriteria.SelectedValue = "Search All" Then
                Qstr = "select recid,usertype,userid,prefix,username,designation,bu,email,status,empid,pwdstatus,isnull(lockreason,'') as lockreason,lockdate,createdby,localuser,deptid,clientid,lobid,creatorid,adminid, convert(varchar,adddate,111) as adddate from registration where usertype<>3  order by "
            ElseIf cboSearchCriteria.SelectedValue = "DeptId" Then
                Qstr = "select recid,usertype,userid,prefix,username,designation,bu,email,status,empid,pwdstatus,isnull(lockreason,'') as lockreason,lockdate,createdby,localuser,deptid,clientid,lobid,creatorid,adminid, convert(varchar,adddate,111) as adddate from registration where  usertype<>3 and " & Trim(Me.cboSearchCriteria.SelectedValue) & " in(select autoid from IDMSDepartment where DepartmentName like '" & Trim(Me.txtSearch.Text) & "%')  and clientid='0' and lobid='0'   order by "
            ElseIf cboSearchCriteria.SelectedValue = "LOBID" Then
                Qstr = "select A.recid,A.usertype,A.userid,A.prefix,A.username,A.designation,A.bu,A.email,A.status,A.empid,A.pwdstatus,isnull(A.lockreason,'') as lockreason,A.lockdate,A.createdby,A.localuser,A.deptid,A.clientid,A.lobid,A.creatorid,A.adminid, convert(varchar,A.adddate,111) as adddate from registration A where  A.usertype<>3 and " & Trim(Me.cboSearchCriteria.SelectedValue) & " in (select autoid from WARSLobMaster where LOBname like '" & Trim(Me.txtSearch.Text) & "%')  order by "
            ElseIf cboSearchCriteria.SelectedValue = "ClientId" Then
                Qstr = "select A.recid,A.usertype,A.userid,A.prefix,A.username,A.designation,A.bu,A.email,A.status,A.empid,A.pwdstatus,isnull(A.lockreason,'') as lockreason,A.lockdate,A.createdby,A.localuser,A.deptid,A.clientid,A.lobid,A.creatorid,A.adminid, convert(varchar,A.adddate,111) as adddate from registration A  where clientid in ( select autoid from idmsclient where clientname like '" & Trim(Me.txtSearch.Text) & "%')  and lobid='0' order by "
            ElseIf cboSearchCriteria.SelectedValue = "Resigned" Then
                Qstr = "select * from registration where lockreason ='$Resigned' order by   "
            ElseIf cboSearchCriteria.SelectedValue = "Transfered" Then
                Qstr = "select * from registration where lockreason = '$Transfer' order by "

            ElseIf cboSearchCriteria.SelectedValue <> "Search All" And cboSearchCriteria.SelectedValue <> "DepartmentId" And cboSearchCriteria.SelectedValue <> "LOBDept" And cboSearchCriteria.SelectedValue <> "ClientId" Then
                Qstr = "select A.recid,A.usertype,A.userid,A.prefix,A.username,A.designation,A.bu,A.email,A.status,A.empid,A.pwdstatus,isnull(A.lockreason,'') as lockreason,A.lockdate,A.createdby,A.localuser,A.deptid,A.clientid,A.lobid,A.creatorid,A.adminid, convert(varchar,A.adddate,111) as adddate from registration A where  A.usertype<>3 and " & Trim(Me.cboSearchCriteria.SelectedValue) & " like '" & Trim(Me.txtSearch.Text) & "%'  order by "
                'ElseIf cboSearchCriteria.SelectedValue = "--Select--" Then
                '    WARSShowMsg("Please Select Some Criteria")
                '    Exit Sub
            End If



        ElseIf txtSearch.Text = "" Then
            If cboSearchCriteria.SelectedValue = "Search All" Then
                Qstr = "select recid,usertype,userid,prefix,username,designation,bu,email,status,empid,pwdstatus,isnull(lockreason,'') as lockreason,lockdate,createdby,localuser,deptid,clientid,lobid,creatorid,adminid, convert(varchar,adddate,111) as adddate from registration where usertype<>3  order by "
            ElseIf cboSearchCriteria.SelectedValue = "Resigned" Then
                Qstr = "select * from registration where lockreason ='$Resigned' order by   "
            ElseIf cboSearchCriteria.SelectedValue = "Transfered" Then
                Qstr = "select * from registration where lockreason = '$Transfer' order by  "


            ElseIf cboSearchCriteria.SelectedValue <> "Search All" Then
                lblMsgBox.Text = "Please enter at least first 3 characters  to search!!!"
                WARSSetFocus(txtSearch)
                grdSearchMember.Visible = False
                'lblCount.Text = ""
                lblLink.Text = ""
                Exit Sub
            End If
        End If
        'Session("Qstr") = Qstr
        Dim ds As New DataSet
        'Qstr = Session("Qstr")
        Dim sfield As String = Session("sortfield")
        connection.Open()
        If Session("sortfield") = "" Then
            Qstr = Qstr & "username"
        Else
            Qstr = Qstr & Session("sortfield")
        End If
        Dim cmd As SqlCommand = New SqlCommand(Qstr, connection)
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(ds)
        If ds.Tables(0).Rows.Count = 0 Then

            WARSShowMsg(" No Data Found")
            grdSearchMember.Visible = False
        Else
            Dim dv As DataView = New DataView(ds.Tables(0))
            If Trim(sfield) <> "" Then
                dv.Sort = sfield
            Else
                dv.Sort = "username"
            End If

            '''''''''''''''''''''
            Me.grdSearchMember.DataSource = dv
            Me.grdSearchMember.DataBind()        '''''''''Bind Grid
            connection.Close()
            cmd.Dispose()
            ds.Dispose()
            adp.Dispose()
            txtName.Focus()
        End If

        

    End Sub
    Function showStatus(ByVal upStatus As String) As String
        If upStatus = "$Resigned" Then
            Return "Resigned"
        ElseIf upStatus = "$Transfer" Then
            Return "Transfered"
        Else
            Return "Working"
        End If
    End Function
    Public Sub Bind()
        Dim ds As New DataSet
        Dim Qstr As String = Session("Qstr")
        Dim sfield As String = Session("sortfield")
        connection.Open()
        If Session("sortfield") = "" Then
            Qstr = Qstr & "username"
        Else
            Qstr = Qstr & Session("sortfield")
        End If
        Dim cmd As SqlCommand = New SqlCommand(Qstr, connection)
        Dim adp As New SqlDataAdapter(cmd)
        adp.Fill(ds)

        Dim dv As DataView = New DataView(ds.Tables(0))
        If Trim(sfield) <> "" Then
            dv.Sort = sfield
        Else
            dv.Sort = "username"
        End If

        '''''''''''''''''''''
        Me.grdSearchMember.DataSource = dv
        Me.grdSearchMember.DataBind()        '''''''''Bind Grid
        connection.Close()
        cmd.Dispose()
        ds.Dispose()
        adp.Dispose()
    End Sub

    'Public Sub WARSShowMsg(ByVal strmsg As String)
    '    'alert function for message display
    '    Dim str As String
    '    str = "<Script language='javascript'>"
    '    str = str + "alert('" + strmsg + "')"
    '    str = str + "</Script>"
    '    Page.ClientScript.RegisterStartupScript(Me.GetType(), "showmsg", str)
    'End Sub
    Public Sub WARSShowMsg(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Public Sub WARSSetFocus(ByVal FocusControl As Control)
        'this function is for setting the focus
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Me.GetType(), "setFocus", Script.ToString())
    End Sub

    Protected Sub ImgBtnSearch_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgBtnSearch.Click
        lblMsgBox.Text = ""
        grdSearchMember.Visible = True
        lblLink.Text = ""
        grdSearchMember.CurrentPageIndex = 0
        If cboSearchCriteria.SelectedIndex <> 0 Then
            WARSSearchMemReg()
        End If
    End Sub

    Protected Sub grdSearchMember_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdSearchMember.ItemCommand
        lblMsgBox.Text = ""
        lblLink.Text = "Click on name link below to view details!!!"
        If e.CommandName = "Select All" Then
            Dim myDataGridItem As DataGridItem
            For Each myDataGridItem In grdSearchMember.Items
                CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True
            Next
        End If
        If e.CommandName = "DeSelect All" Then
            Dim myDataGridItem As DataGridItem
            For Each myDataGridItem In grdSearchMember.Items
                CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = False
            Next
        End If
        If e.CommandName = "Detail" Then
            Session("sortfield") = "username"
            cmdUpdate.Focus()
            WARSSearchMemReg()
        End If

        ''''''''''''''''dleteing user on 27-09-2208'''''''''''''''
        If e.CommandName = "Delete" Then
            Dim intd As Integer
            Dim count As Integer
            Dim i As Integer
            For i = 0 To grdSearchMember.Items.Count - 1
                count = 0
                For intd = 0 To grdSearchMember.Items.Count - 1
                    If (CType(grdSearchMember.Items(intd).FindControl("chkSelect"), CheckBox).Checked = True) Then
                        count = count + 1
                        Exit For
                    End If
                Next
                If count = 0 Then
                    WARSShowMsg("Select a value to delete!!!")
                    Exit Sub
                End If
            Next
            pnlConfirm.Visible = True
        End If

        If e.CommandName = "Details1" Then
            Dim button1 As LinkButton
            button1 = CType(e.Item.Cells(0).FindControl("lnkname"), LinkButton)
            PnlRegDetails.Visible = True
            txtHidRecordId.Text = grdSearchMember.DataKeys(e.Item.ItemIndex)
            WARSFillDepartment()
            WARSFillClient()
            WARSRegFillLobDept()
            Dim cmdshow As New SqlCommand("Select Pwd,UserType,UserId,Isnull(Prefix,'') as Prefix,Username,case isnumeric(Lobid) when 1 then convert(numeric(4,0),isnull(LobId,'')) end as Department,Designation,Isnull(BU,'') as BU ,Isnull(EMail,'') as EMail,isnull(EMPId,'') as EMPId,DeptId,ClientId, isnull(lockreason,'') as lockreason from Registration where RecId='" & Trim(txtHidRecordId.Text) & "'  order by username", connection3)
            Dim readershow As SqlDataReader
            connection3.Open()
            readershow = cmdshow.ExecuteReader
            If readershow.Read Then
                Dim stat As String = Trim(readershow("lockreason"))
                If stat = "$Resigned" Or stat = "$Transfer" Then
                    Session("status") = "True"
                    cmdUpdate.Enabled = False

                Else
                    Session("status") = "False"
                    cmdUpdate.Enabled = True

                End If
                txtUserId.Text = readershow("UserId")
                Prefix = readershow("Prefix")
                If Prefix = "" Then
                    cboPrefix.SelectedIndex = 0
                Else
                    cboPrefix.SelectedItem.Text = readershow("Prefix")
                End If
                txtName.Text = readershow("Username")
                txtEmpId.Text = readershow("empid")
                WARSBindDesig()
                cboDesignation.SelectedValue = readershow("Designation")
                txtEmail.Text = readershow("EMail")
                WARSBindBU()
                Dim BU = readershow("BU")
                CBOBU.SelectedItem.Text = readershow("bu")
            Else
                lblMsgBox.Text = "No records found!!!"
            End If
            connection3.Close()
            readershow.Close()
            cmdshow.Dispose()
            cmdCancel.Focus()

        End If
    End Sub

    Protected Sub grdSearchMember_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdSearchMember.PageIndexChanged
        grdSearchMember.CurrentPageIndex = e.NewPageIndex
        WARSSearchMemReg()
    End Sub

    Protected Sub grdSearchMember_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdSearchMember.SortCommand
        Session("sortfield") = e.SortExpression
        WARSSearchMemReg()
    End Sub
    Private Sub cmdUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdUpdate.Click
        Try
            If WARSRegChkBlank() = False Then 'Check if any compulsory field id blank
                Exit Sub
            End If
            If Trim(txtBU.Text) <> "" Then
                If chkBUExist() = False Then
                    Exit Sub
                End If
                SaveBU()
                WARSBindBU()
                CBOBU.SelectedValue = txtBU.Text
                txtBU.Text = ""
            End If
            If Trim(txtDesig.Text) <> "" Then
                If chkDesigExist() = False Then
                    Exit Sub
                End If
                saveDesignation()
                WARSBindDesig()
                cboDesignation.SelectedValue = txtDesig.Text
                txtDesig.Text = ""
            End If
            If Session("typeofuser") = "Admin" Then
                Dim dept, lob, clt As String
                If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                    clt = "0"
                Else
                    clt = ClientName.SelectedValue
                End If
                If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                    lob = "0"
                Else
                    lob = cboLOBDept.SelectedValue
                End If
                If DepartmentName.SelectedIndex = 0 Or DepartmentName.SelectedIndex = -1 Then
                    dept = "0"
                Else
                    dept = DepartmentName.SelectedValue
                End If
                Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
                cmdupdate.CommandType = CommandType.StoredProcedure
                With cmdupdate.Parameters
                    .AddWithValue("@userid", Session("userid"))
                    .AddWithValue("@Deptid", dept)
                    .AddWithValue("@LOBID", lob)
                    .AddWithValue("@Clientid", clt)
                End With
                Dim readerdata As SqlDataReader
                connection.Open()
                readerdata = cmdupdate.ExecuteReader


                If readerdata.HasRows Then
                    cmdupdate = New SqlCommand("Update_Registration", connection)
                    cmdupdate.CommandType = CommandType.StoredProcedure
                    With cmdupdate.Parameters
                        .AddWithValue("@RecId", txtHidRecordId.Text)
                        .AddWithValue("@UserType", "1")
                        .AddWithValue("@UserId", txtUserId.Text)
                        .AddWithValue("@Prefix", cboPrefix.SelectedValue)
                        .AddWithValue("@Username", txtName.Text)
                        .AddWithValue("@EMPId", txtEmpId.Text)
                        If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                            .AddWithValue("@LOBId", "0")
                        Else
                            .AddWithValue("@LOBId", cboLOBDept.SelectedValue)
                        End If
                        If Trim(txtDesig.Text) = "" Then
                            .AddWithValue("@Designation", cboDesignation.SelectedItem.Text)
                        Else
                            .AddWithValue("@Designation", txtDesig.Text)
                        End If
                        If Trim(txtBU.Text) = "" Then
                            .AddWithValue("@BU", CBOBU.SelectedItem.Text)
                        Else
                            .AddWithValue("@BU", txtBU.Text)
                        End If
                        .AddWithValue("@EMail", txtEmail.Text)
                        .AddWithValue("@Status", "Active")
                        .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                        .AddWithValue("@DeptId", DepartmentName.SelectedValue)
                        If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                            .AddWithValue("@ClientId", "0")
                        Else
                            .AddWithValue("@ClientId", ClientName.SelectedValue)
                        End If
                        .AddWithValue("@Createdby", Session("userid"))

                    End With
                    readerdata.Close()
                    If cmdupdate.ExecuteNonQuery() <> -1 Then
                        WARSShowMsg("Record has been updated successfully!!!")
                    End If
                    connection.Close()
                    cmdupdate.Dispose()
                    PnlRegDetails.Visible = False

                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    connection.Open()
                    Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + Me.txtUserId.Text + "' and Action='Edit'", connection)
                    cmm.ExecuteNonQuery()
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha

                    Dim cmdSave1 As New SqlCommand("update_buddy", connection) 'save data through procedure
                    cmdSave1.CommandType = CommandType.StoredProcedure
                    With cmdSave1.Parameters
                        .AddWithValue("@UserId", txtUserId.Text)
                        .AddWithValue("@Username", txtName.Text)
                        .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                        .AddWithValue("@Departmentname", CType(DepartmentName.SelectedItem.Text, String))
                        If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                            .AddWithValue("@ClientId", "0")
                            .AddWithValue("@Clientname", "0")
                        Else
                            .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                            .AddWithValue("@Clientname", CType(ClientName.SelectedItem.Text, String))
                        End If


                        If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                            .AddWithValue("@LOBID", "0")
                            .AddWithValue("@LOBname", "0")
                        Else
                            .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                            .AddWithValue("@LOBname", CType(cboLOBDept.SelectedItem.Text, String))
                        End If


                    End With

                    connection.Open()
                    cmdSave1.ExecuteNonQuery()
                    connection.Close()
                    cmdSave1.Dispose()

                    WARSSearchMemReg()
                    WARSRegClear()

                Else
                    WARSShowMsg("You are not admin of this span!!!")
                End If
            ElseIf Session("typeofuser") = "Super Admin" Then


                Dim cmdUpdate As New SqlCommand("Update_Registration", connection)
                cmdUpdate.CommandType = CommandType.StoredProcedure
                With cmdUpdate.Parameters
                    .AddWithValue("@RecId", txtHidRecordId.Text)
                    .AddWithValue("@UserType", "1")
                    .AddWithValue("@UserId", txtUserId.Text)
                    .AddWithValue("@Prefix", cboPrefix.SelectedValue)
                    .AddWithValue("@Username", txtName.Text)
                    .AddWithValue("@EMPId", txtEmpId.Text)
                    If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                        .AddWithValue("@LOBId", "0")
                    Else
                        .AddWithValue("@LOBId", cboLOBDept.SelectedValue)
                    End If
                    If Trim(txtDesig.Text) = "" Then
                        .AddWithValue("@Designation", cboDesignation.SelectedItem.Text)
                    Else
                        .AddWithValue("@Designation", txtDesig.Text)
                    End If
                    If Trim(txtBU.Text) = "" Then
                        .AddWithValue("@BU", CBOBU.SelectedItem.Text)
                    Else
                        .AddWithValue("@BU", txtBU.Text)
                    End If
                    .AddWithValue("@EMail", txtEmail.Text)
                    .AddWithValue("@Status", "Active")
                    .AddWithValue("@AddDate", System.DateTime.Now.ToString("d"))
                    .AddWithValue("@DeptId", DepartmentName.SelectedValue)
                    If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                        .AddWithValue("@ClientId", "0")
                    Else
                        .AddWithValue("@ClientId", ClientName.SelectedValue)
                    End If
                    .AddWithValue("@Createdby", Session("userid"))

                End With
                connection.Open()
                If cmdUpdate.ExecuteNonQuery() <> -1 Then
                    WARSShowMsg("Record has been updated successfully!!!")
                End If
                connection.Close()
                cmdUpdate.Dispose()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                connection.Open()
                Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + Me.txtUserId.Text + "' and Action='Edit'", connection)
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                PnlRegDetails.Visible = False
                Dim cmdSave1 As New SqlCommand("update_buddy", connection) 'save data through procedure
                cmdSave1.CommandType = CommandType.StoredProcedure
                With cmdSave1.Parameters
                    .AddWithValue("@UserId", txtUserId.Text)
                    .AddWithValue("@Username", txtName.Text)
                    .AddWithValue("@DeptId", CType(DepartmentName.SelectedValue, Integer))
                    .AddWithValue("@Departmentname", CType(DepartmentName.SelectedItem.Text, String))
                    If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
                        .AddWithValue("@ClientId", "0")
                        .AddWithValue("@Clientname", "0")
                    Else
                        .AddWithValue("@ClientId", CType(ClientName.SelectedValue, Integer))
                        .AddWithValue("@Clientname", CType(ClientName.SelectedItem.Text, String))
                    End If


                    If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
                        .AddWithValue("@LOBID", "0")
                        .AddWithValue("@LOBname", "0")
                    Else
                        .AddWithValue("@LOBID", Convert.ToInt32(cboLOBDept.SelectedValue))
                        .AddWithValue("@LOBname", CType(cboLOBDept.SelectedItem.Text, String))
                    End If


                End With

                connection.Open()
                cmdSave1.ExecuteNonQuery()
                connection.Close()
                cmdSave1.Dispose()

                WARSSearchMemReg()
                WARSRegClear()
            Else
                WARSShowMsg("You are a simple user you cannot update user account!!!")
            End If

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub

    Private Sub SaveBU()
        Try
            Dim cmdsaveBU As New SqlCommand("Insert_WARSBUMaster", connection)
            cmdsaveBU.CommandType = CommandType.StoredProcedure
            With cmdsaveBU.Parameters
                .AddWithValue("@BuName", txtBU.Text)
                .AddWithValue("@AddedOn", System.DateTime.Now.ToString("d"))
            End With
            connection.Open()
            cmdsaveBU.ExecuteNonQuery()
            connection.Close()
            cmdsaveBU.Dispose()
        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub
    Private Sub saveDesignation()
        Dim cmdsave As New SqlCommand("insert into WARSDesignationMaster values('" & txtDesig.Text & "','" & System.DateTime.Now.ToString("d") & "')", connection)
        connection.Open()
        cmdsave.ExecuteNonQuery()
        connection.Close()
        cmdsave.Dispose()
    End Sub
    Public Function WARSRegChkBlank() As Boolean


        If ObjLib.chkvalidtxt(txtName.Text) = False Then
            WARSShowMsg("Name should not contain any special character!!!")
            txtName.Text = ""
            WARSSetFocus(txtName)
            Return False
        End If

        'If cboUserType.SelectedIndex = 0 Then
        '    WARSShowMsg("Please Select User Type")
        '    WARSSetFocus(cboUserType)
        '    Return False
        'End If

        If txtUserId.Text = "" Then
            WARSShowMsg("Please fill userid!!!")
            WARSSetFocus(txtUserId)
            Return False
        End If

        ''If WARSchkEmailExist() = False Then
        ''    txtEmail.Text = ""
        ''    Return False
        ''End If

        If txtName.Text = "" Then
            WARSShowMsg("Please fill name!!!")
            WARSSetFocus(txtName)
            Return False
        End If

        If txtEmpId.Text = "" Then
            WARSShowMsg("Please fill employeeid!!!")
            WARSSetFocus(txtEmpId)
            Return False
        End If

        If cboDesignation.SelectedItem.Text = "--Select--" Then
            WARSShowMsg("Please fill designation!!!")
            WARSSetFocus(cboDesignation)
            Return False
        End If

        If DepartmentName.SelectedIndex = 0 Then
            WARSShowMsg("Please select department!!!")
            WARSSetFocus(DepartmentName)
            Return False
        End If

        'If CBOBU.SelectedIndex = 0 And Trim(txtBU.Text) = "" Then
        '    WARSShowMsg("Please select a BU or Enter a new BU in textbox")
        '    WARSSetFocus(txtBU)
        '    Return False
        'End If


        If txtEmail.Text = "" Then
            WARSShowMsg("Please fill emailid!!!")
            WARSSetFocus(txtEmail)
            Return False
        End If

        If WARSEmailId() = False Then

            WARSSetFocus(txtEmail)
            Return False
        End If
        'If txtMobile.Text <> "" Then
        '    If txtMobile.Text <> "" And WARSCheckPhone(txtMobile.Text) = False Then
        '        WARSShowMsg("Please Enter Valid Mobile No. e.g +91-9899364645 or 919899364645")
        '        WARSSetFocus(txtMobile)
        '        Return False
        '    End If
        'End If
        'If ClientName.SelectedIndex = 0 Then
        '    If cboLOBDept.SelectedIndex <> 0 Then
        '        WARSShowMsg("Please Select the Client First!")
        '        Return False
        '    End If
        'End If

        Return True

    End Function

    Private Function WARSEmailId() As Boolean
        'check for uniquesness of Email id
        If txtEmail.Text = "" Then
            WARSShowMsg("Please enter a Emailid!!!")
            Exit Function
        End If
        Dim commId As New SqlCommand("select * from Registration where email='" & txtEmail.Text & "' and status='Active' and userid<> '" + txtUserId.Text + "'", connection)
        Dim rdrId As SqlDataReader
        connection.Open()
        rdrId = commId.ExecuteReader
        If rdrId.Read Then
            WARSShowMsg("Email Id already exists,Please select another!!!")
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            txtEmail.Text = ""

            Return False
        Else
            commId.Dispose()
            rdrId.Close()
            connection.Close()
            Return True
        End If
    End Function

    Public Sub WARSRegClear()


        txtUserId.Text = ""
        cboPrefix.SelectedIndex = 0
        txtName.Text = ""
        'cboLOBDept.SelectedIndex = 0
        cboDesignation.SelectedIndex = 0
        txtBU.Text = ""
        CBOBU.SelectedIndex = 0
        txtEmpId.Text = ""
        'txtPhoneNumber.Text = ""
        'txtMobile.Text = ""
        txtEmail.Text = ""
        txtHidRecordId.Text = ""
        txtEnteredDate.Text = ""
        txtEnteredDateView.Text = ""
        'WARSRegFillStates()
    End Sub
    Private Function chkBUExist() As Boolean
        Dim BU As String
        If Trim(txtBU.Text) = "" Then
            Exit Function
        End If

        BU = Trim(txtBU.Text)

        Dim com As New SqlCommand("select * from WARSBUMaster where buname='" & BU & "'", connection)
        connection.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader
        If rdr.Read Then
            WARSShowMsg("BU already exists,Please specify another!!!")
            WARSSetFocus(txtName)
            connection.Close()
            rdr.Close()
            com.Dispose()
            Return False
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
        Return True
    End Function
    Private Function chkDesigExist() As Boolean
        Dim desg As String
        If Trim(txtDesig.Text) = "" Then
            Exit Function
        End If
        desg = Trim(txtDesig.Text)
        Dim com As New SqlCommand("select * from WARSDesignationMaster where designationname='" & desg & "'", connection)
        connection.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader
        If rdr.Read Then
            WARSShowMsg("Designation already exists,Please specify another!!!")
            WARSSetFocus(txtDesig)
            connection.Close()
            rdr.Close()
            com.Dispose()
            Return False
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
        Return True
    End Function
    Private Sub WARSBindDesig()


        Dim cmd As New SqlCommand("Select * from WARSDesignationMaster where Designationname <> ''", connection)
        Dim adp As New SqlDataAdapter
        Dim ds As New DataSet
        adp.SelectCommand = cmd
        connection.Open()
        adp.Fill(ds)
        connection.Close()
        cboDesignation.DataSource = ds
        cboDesignation.DataTextField = "Designationname"
        cboDesignation.DataValueField = "Designationname"
        cboDesignation.DataBind()
        cboDesignation.Items.Insert(0, "--Select--")

        ds.Dispose()
        adp.Dispose()
        cmd.Dispose()
    End Sub
    Private Sub WARSBindBU()
        Dim cmdbu As New SqlCommand("select * from WARSBUmaster where BUName<>'' and BUName='BFI'", connection)
        Dim adpbu As New SqlDataAdapter
        Dim dsbu As New DataSet
        adpbu.SelectCommand = cmdbu
        connection.Open()
        adpbu.Fill(dsbu)
        connection.Close()
        CBOBU.DataSource = dsbu
        CBOBU.DataTextField = "BUName"
        CBOBU.DataValueField = "BUName"
        CBOBU.DataBind()
        'CBOBU.Items.Insert(0, "-----Select-----")
        dsbu.Dispose()
        adpbu.Dispose()
        cmdbu.Dispose()
    End Sub
    Private Sub WARSFillDepartment()
        Dim cmdshow As New SqlCommand("Select RecId,convert(numeric,DeptId) as DeptId from Registration where RecId='" & Trim(txtHidRecordId.Text) & "'", connection1)
        Dim readershow As SqlDataReader
        connection1.Open()
        readershow = cmdshow.ExecuteReader
        If readershow.Read Then
            DepartId = readershow("DeptId")

        End If
        connection.Close()
        readershow.Close()
        cmdshow.Dispose()
        Try
            Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
            Dim da As New SqlDataAdapter
            da.SelectCommand = comdepart
            Dim ds As New DataSet
            connection.Open()
            da.Fill(ds)
            connection.Close()
            DepartmentName.DataTextField = "DepartmentName"
            DepartmentName.DataValueField = "autoid"
            DepartmentName.DataSource = ds
            DepartmentName.DataBind()
            DepartmentName.Items.Insert("0", "--Select--")
            If DepartId = 0 Then
                DepartmentName.SelectedIndex = 0
            Else
                DepartmentName.SelectedValue = DepartId
            End If

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)

        End Try
    End Sub
    Private Sub WARSFillClient()
        Dim cmdshow As New SqlCommand("Select RecId,convert(numeric,DeptId) as DeptId,convert(numeric,ClientId) as ClientId from Registration where RecId='" & Trim(txtHidRecordId.Text) & "'", connection)
        Dim readershow As SqlDataReader
        connection.Open()
        readershow = cmdshow.ExecuteReader
        If readershow.Read Then
            FirstClientId = readershow("ClientId")
        End If
        connection.Close()
        readershow.Close()
        cmdshow.Dispose()

        Try
            Dim cmdst As New SqlCommand("select *  from idmsclient where deptid='" & DepartId & "'", connection)
            Dim dsst As New DataSet
            Dim adpst As New SqlDataAdapter
            adpst.SelectCommand = cmdst
            connection.Open()
            Dim cntr = adpst.Fill(dsst)
            connection.Close()
            ClientName.DataTextField = "clientname"
            ClientName.DataValueField = "autoid"
            ClientName.DataSource = dsst
            ClientName.DataBind()
            ClientName.Items.Insert("0", "--Select--")
            If FirstClientId = 0 Then
                ClientName.SelectedIndex = 0
            Else
                ClientName.SelectedValue = FirstClientId
            End If

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub
    Private Sub WARSRegFillLobDept()

        Dim cmdshow As New SqlCommand("Select RecId,convert(numeric,LOBID) as LOBID from Registration where RecId='" & Trim(txtHidRecordId.Text) & "'", connection)
        Dim readershow As SqlDataReader
        Dim myvar As Integer
        connection.Open()
        readershow = cmdshow.ExecuteReader
        If readershow.Read Then
            LobId = readershow("LOBID")
        End If
        connection.Close()
        readershow.Close()
        cmdshow.Dispose()

        Dim comm As New SqlCommand("select * from WARSLOBMaster where DeptId='" & DepartId & "' and ClientId='" & FirstClientId & "' ", connection)
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        da.SelectCommand = comm
        connection.Open()
        da.Fill(ds)
        connection.Close()
        cboLOBDept.DataSource = ds
        cboLOBDept.DataValueField = "AutoId"
        cboLOBDept.DataTextField = "LOBName"
        cboLOBDept.DataBind()
        cboLOBDept.Items.Insert(0, "--Select--")
        If LobId = 0 Then
            cboLOBDept.SelectedIndex = 0
        Else
            ' cboLOBDept.SelectedValue = LobId
            Dim mycom As New SqlCommand("select count(autoid) from WARSLOBMaster where autoid='" + LobId + "'", connection)
            connection.Open()
            myvar = mycom.ExecuteScalar()
            connection.Close()
            If myvar = 0 Then
                cboLOBDept.SelectedIndex = 0
            Else
                cboLOBDept.SelectedValue = LobId

            End If
            
        End If

        comm.Dispose()
        da.Dispose()
        ds.Dispose()
    End Sub

    Protected Sub btnYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Dim cmd2 As New SqlCommand
        Dim cmd1 As New SqlCommand
        Dim cmd3 As New SqlCommand
        Dim dr As SqlDataReader
        'Dim key As String
        ' key = Me.hidConfirm.Value
        Dim keyN As String
        'keyN = key.Split("$")
        Dim i As Integer
        Dim int As Integer
        Dim intt As Integer
        Dim arry As String = ""
        i = 0
        Dim dept As String = ""
        Dim lob As String = ""
        Dim clt As String = ""
        'If ClientName.SelectedIndex = 0 Or ClientName.SelectedIndex = -1 Then
        '    clt = "0"
        'Else
        '    clt = ClientName.SelectedValue
        'End If
        'If cboLOBDept.SelectedIndex = 0 Or cboLOBDept.SelectedIndex = -1 Then
        '    lob = "0"
        'Else
        '    lob = cboLOBDept.SelectedValue
        'End If
        'If DepartmentName.SelectedIndex = 0 Or DepartmentName.SelectedIndex = -1 Then
        '    dept = "0"
        'Else
        '    dept = DepartmentName.SelectedValue
        'End If
        Dim useridfound As String = ""
        For int = 0 To grdSearchMember.Items.Count - 1
            If (CType(grdSearchMember.Items(int).FindControl("chkSelect"), CheckBox).Checked = True) Then
                pnlConfirm.Visible = True
                'arry = CType(Me.grdSearchMember.Items(int).FindControl("lblAutoid"), Label).Text
                connection.Open()
                Dim key As Integer
                key = Me.grdSearchMember.DataKeys(Me.grdSearchMember.Items(int).ItemIndex)
                'For intt = 0 To grdSearchMember.Items.Count - 1
                cmd3 = New SqlCommand("Select userid from registration where recid = " & key & "", connection)
                'cmd3.ExecuteNonQuery()
                dr = cmd3.ExecuteReader
                While dr.Read
                    keyN = dr("userid")
                End While
                dr.Close()
                ' Next
                cmd3 = New SqlCommand("select deptid,clientid,lobid from registration where recid = " & key & "", connection)

                dr = cmd3.ExecuteReader
                While dr.Read
                    dept = dr("deptid").ToString
                    clt = dr("clientid").ToString
                    lob = dr("lobid").ToString
                End While
                dr.Close()
                connection.Close()


                If Session("typeofuser") = "Admin" Then
                    
                    Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
                    cmdupdate.CommandType = CommandType.StoredProcedure
                    With cmdupdate.Parameters
                        .AddWithValue("@userid", Session("userid"))
                        .AddWithValue("@Deptid", dept)
                        .AddWithValue("@LOBID", lob)
                        .AddWithValue("@Clientid", clt)
                    End With
                    Dim readerdata As SqlDataReader
                    connection.Open()
                    readerdata = cmdupdate.ExecuteReader


                    If readerdata.HasRows Then
                        readerdata.Close()
                        cmd1 = New SqlCommand("delete from buddy where userid = '" + keyN + "'  ", connection)
                        cmd1.ExecuteNonQuery()
                        cmd2 = New SqlCommand("delete from registration where Recid = " & key & "  ", connection)
                        cmd2.ExecuteNonQuery()

                    Else

                        If useridfound = "" Then
                            useridfound = keyN
                        Else
                            useridfound = useridfound & "," & keyN
                        End If
               
                    End If
                    Dim objDB As New Database()
                    objDB.trackAccountForMaster(Session("userid"), "Delete", "User", keyN, dept, clt, lob)
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    connection.Open()
                    Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName= '" + keyN + "' and Action='Delete'", connection)
                    cmm.ExecuteNonQuery()
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha


                ElseIf Session("typeofuser") = "Super Admin" Then
                    cmd1 = New SqlCommand("delete from buddy where userid = '" + keyN + "'  ", connection)
                    connection.Open()
                    cmd1.ExecuteNonQuery()
                    cmd2 = New SqlCommand("delete from registration where Recid = " & key & "  ", connection)
                    cmd2.ExecuteNonQuery()
                    connection.Close()
                    WARSShowMsg("Record Deleted successfully.")
                    Dim objDB As New Database()
                    objDB.trackAccountForMaster(Session("userid"), "Delete", "User", keyN, dept, clt, lob)
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    connection.Open()
                    Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName= '" + keyN + "' and Action='Delete'", connection)
                    cmm.ExecuteNonQuery()
                    connection.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha

                End If

            End If
        Next
        If useridfound <> "" Then
            WARSShowMsg("You are not admin of userid(s) " & useridfound & "!!!")
        End If

        WARSSearchMemReg()
        pnlConfirm.Visible = False
    End Sub

    Protected Sub btnNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNo.Click
        pnlConfirm.Visible = False
    End Sub

    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If DepartmentName.SelectedItem.Text = "--Select--" Then
            ClientName.Items.Clear()
            cboLOBDept.Items.Clear()
        Else

            ClientName.DataTextField = "clientname"
            ClientName.DataValueField = "autoid"
            ClientName.DataSource = classobj.bind_client(DepartmentName.SelectedValue)
            ClientName.DataBind()
            ClientName.Items.Insert(0, "--Select--")

            cboLOBDept.Items.Clear()
        End If

    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If ClientName.SelectedItem.Text = "--Select--" Then
            cboLOBDept.Items.Clear()



        Else

            cboLOBDept.DataTextField = "lobname"
            cboLOBDept.DataValueField = "autoid"
            cboLOBDept.DataSource = classobj.bind_lob(DepartmentName.SelectedValue, ClientName.SelectedValue)
            cboLOBDept.DataBind()
            cboLOBDept.Items.Insert(0, "--Select--")

        End If

    End Sub

    Protected Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        PnlRegDetails.Visible = False

    End Sub
End Class
