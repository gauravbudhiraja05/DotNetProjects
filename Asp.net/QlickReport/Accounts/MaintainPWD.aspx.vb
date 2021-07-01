Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Maintain Password                  *
'*  Summary: Resets Password and Delete Users     *
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class Account_MaintainPWD
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim ObjLib As New Library

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here


        If Session("Userid") = "" Then
            Response.Redirect("/" & Session("projName") & "/Misc/sessionexpire.aspx")
        End If
        If Me.IsPostBack = False Then
            'If Session("Userid") = "" Then
            '    Response.Redirect("/" & Session("projName") & "/Misc/sessionexpire.aspx")
            'Else
            WARSRegFillUserTypes(cboUserType)
            PassDuration()
            trgrid.Visible = False
            trstatus.Visible = False
            imglock.Visible = False
            rdoLock.SelectedValue = "Unlocked"
            '********For Track Management********************

            'Dim cmdCal As New SqlCommand
            'cmdCal.Connection = connection
            '' cmdCal.CommandText = "select Username from registration where userid='" & Session("Userid") & "'"
            'cmdCal.CommandText = "select Username from registration where userid='idmsdamin'"
            'connection.Open()
            'Dim UserName = cmdCal.ExecuteScalar
            'connection.Close()

            'Dim cmdsaveTrack As New SqlCommand("Insert_IDMSTrackManagement", connection)
            'cmdsaveTrack.CommandType = CommandType.StoredProcedure
            'With cmdsaveTrack.Parameters
            '    '.Add("@UserId", Trim(Session("Userid")))
            '    .Add("@UserId", Trim(Session("Userid")))
            '    .Add("@FormName", "MaintainPWD")
            '    .Add("@Comment", "Member Password History")
            '    .Add("@Addedon", System.DateTime.Today)
            'End With
            'connection.Open()
            'cmdsaveTrack.ExecuteNonQuery()
            'connection.Close()
            'cmdsaveTrack.Dispose()

            '**************************************************
        End If
        'End If
    End Sub
    'fetching password duration
    Private Function PassDuration()
        Dim com As New SqlCommand("select duration,convert(varchar,currdate,106),updBy from warspasswordduration", connection)
        connection.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader
        If rdr.Read Then
            txtDuration.Text = rdr(0)
            lblLastUpdate.Text = "( Last Updated On : " & rdr(1) & " By " & rdr(2) & " )"
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
    End Function
    'this function is for setting the focus
    Public Sub WARSSetFocus(ByVal FocusControl As Control)

        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        RegisterStartupScript("setFocus", Script.ToString())
    End Sub
    'alert function for message display
    Public Sub WARSShowMsg(ByVal strmsg As String)

        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub
    'Fills all UserTypes from database
    Private Sub WARSRegFillUserTypes(ByVal control As DropDownList)

        Dim uid = (Session("usertype1"))
        Dim query As String

        query = "select * from WARSUserType where UserType= 'member'"


        ''If uid = "Admin" Then
        ''    userid = "Member"
        ''    query = "select * from WARSUserType where UserType= '" & userid & "' "
        ''Else
        ''    query = "select * from WARSUserType"

        ''End If

        'Response.Write(uid)
        'Response.End()
        'Dim commUType As New SqlCommand("select * from WARSUserType where UserType= '" & userid & "' ", connection)
        Dim commUType As New SqlCommand(query, connection)

        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        da.SelectCommand = commUType
        connection.Open()
        da.Fill(ds)
        connection.Close()
        control.DataSource = ds
        control.DataValueField = "UserTypeId"
        control.DataTextField = "UserType"
        control.DataBind()
        control.Items.Insert(0, "---Select---")
        commUType.Dispose()
        da.Dispose()
        ds.Dispose()
        control.Items.Remove("idmsadmin")
    End Sub

    'sorting record according to user type
    Private Sub cboUserType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUserType.SelectedIndexChanged
        trgrid.Visible = False
        trstatus.Visible = False
        If cboUserType.SelectedIndex = 0 Then
            cboUserId.Items.Clear()
            Exit Sub
        End If

        Dim commUType As New SqlCommand("select UserId,UserId+'('+Username+')' as Username from registration where usertype=" & cboUserType.SelectedValue & " order by userid asc", connection)
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        da.SelectCommand = commUType
        connection.Open()
        da.Fill(ds)
        connection.Close()
        cboUserId.DataSource = ds
        cboUserId.DataValueField = "UserId"
        cboUserId.DataTextField = "Username"
        cboUserId.DataBind()
        cboUserId.Items.Insert(0, "---Select---")
        commUType.Dispose()
        da.Dispose()
        ds.Dispose()
    End Sub
    ' bind record with datalist
    Private Sub dlbind(ByVal control As WebControls.DataGrid)
        Dim objcmd
        objcmd = New SqlCommand("Select autoid,UserID,Pwd,UpdateDate=convert(varchar,UpdateDate,106),UpdatedBy from PWDHistory where userid='" & cboUserId.SelectedValue & "' order by updatedate desc", connection)
        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet
        objadp.SelectCommand = objcmd
        connection.Open()
        objadp.Fill(objds)
        Dim dv As DataView = New DataView(objds.Tables(0))
        If Trim(txtsort.Text) <> "" Then
            dv.Sort = txtsort.Text.ToString & " " & txtsortorder.Text
        Else
            dv.Sort = "UpdateDate"
        End If
        'If dv.Count <> 0 Then
        trgrid.Visible = True
        trstatus.Visible = True
        control.DataSource = dv
        control.DataBind()
        'End If
        connection.Close()
        objcmd.dispose()
        objds.Dispose()
    End Sub
    'locking unlocking account
    Private Function AccountStaus(ByVal userid As String, ByVal stat As String, ByVal lockReason As String)
        Dim comm As New SqlCommand("update registration set pwdstatus='" & stat & "',LockReason='" & lockReason & "',lockdate='" & System.DateTime.Now.ToString("d") & "' where userid='" & userid & "'", connection)
        connection.Open()
        comm.ExecuteNonQuery()
        connection.Close()
        comm.Dispose()
    End Function
    'checking locked user

    Private Function checkLocked()
        imglock.Visible = False
        rdoLock.SelectedValue = "Unlocked"
        Dim comm As New SqlCommand("select isnull(pwdstatus,'') from registration where userid='" & cboUserId.SelectedValue & "'", connection)
        Dim rdr As SqlDataReader
        connection.Open()
        rdr = comm.ExecuteReader
        If rdr.Read Then
            If rdr(0) = "Locked" Then
                imglock.Visible = True
                rdoLock.SelectedValue = "Locked"
                rdr.Close()
                connection.Close()
                comm.Dispose()
                Return True
            End If
        End If

        rdr.Close()
        connection.Close()
        comm.Dispose()
        Return False
    End Function


    Private Sub cboUserId_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboUserId.SelectedIndexChanged
        trgrid.Visible = False
        trstatus.Visible = False
        checkLocked()
        dlbind(grdUsers)
    End Sub
    'sorting record in  datagrid 
    Private Sub grdUsers_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdUsers.SortCommand
        If txtsort.Text = e.SortExpression.ToString Then

            If txtsortorder.Text = "ASC" Then
                txtsortorder.Text = "DESC"
            Else
                txtsortorder.Text = "ASC"
            End If
        Else
            txtsortorder.Text = "ASC"
        End If
        txtsort.Text = e.SortExpression
        dlbind(grdUsers)
    End Sub
    'datagrid item commnand function
    Private Sub grdUsers_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUsers.ItemCommand
        Try
            If e.CommandName = "Select All" Then
                '''''''To Select all items
                Dim myDataGridItem As DataGridItem

                For Each myDataGridItem In grdUsers.Items
                    CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True
                Next

            End If

            If e.CommandName = "DeSelect All" Then
                '''''''To Select all items
                Dim myDataGridItem As DataGridItem

                For Each myDataGridItem In grdUsers.Items
                    CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = False
                Next

            End If

            If e.CommandName = "Edit" Then
                CType(e.Item.FindControl("txtPWD"), TextBox).Visible = True
                CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = True
                CType(e.Item.FindControl("Cancel"), ImageButton).Visible = True
            End If

            If e.CommandName = "EditSave" Then

                Dim autoid, pwd, userid
                autoid = grdUsers.DataKeys(e.Item.ItemIndex)
                'userid = CType(e.Item.FindControl("lblusrId"), LinkButton).Text
                pwd = CType(e.Item.FindControl("txtPWD"), TextBox).Text
                Dim objCmduserid As New SqlCommand("select userid from pwdhistory where userid=(select userid from pwdhistory where autoid='" & autoid & "')", connection)
                Dim rdruserid As SqlDataReader
                connection.Open()
                rdruserid = objCmduserid.ExecuteReader
                If rdruserid.Read Then
                    userid = rdruserid("userid")
                End If
                connection.Close()
                rdruserid.Close()
                objCmduserid.Dispose()
                'Response.Write(userid)
                'Response.End()
                If chkLastFive(userid, Trim(pwd)) = False Then
                    WARSShowMsg("Please choose a password other than five old passwords!!!")
                    Exit Sub
                End If

                If WARSChkPass(userid, Trim(pwd)) = False Then
                    WARSSetFocus(e.Item.FindControl("txtPWD"))
                    Exit Sub
                End If
                'Response.Write(autoid)
                ''Response.Write(pwd)
                'Response.End()
                ChangePWD(pwd, autoid)
                ChangeInRegistration(pwd, autoid)
                CType(e.Item.FindControl("txtPWD"), TextBox).Visible = False
                CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = False
                CType(e.Item.FindControl("Cancel"), ImageButton).Visible = False
                dlbind(grdUsers)
                WARSShowMsg("Password has been updated sucessfully!!!")
            End If

            If e.CommandName = "Cancel" Then
                CType(e.Item.FindControl("txtPWD"), TextBox).Visible = False
                CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = False
                CType(e.Item.FindControl("Cancel"), ImageButton).Visible = False
            End If
        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try

    End Sub
    'last five  password record of user
    Private Function chkLastFive(ByVal userid, ByVal pwd)
        Dim stat As Boolean = True
        Dim com As New SqlCommand("select top 5 pwd from PWDHistory where userid='" & userid & "' order by autoid desc", connection)
        connection.Open()
        Dim rdr As SqlDataReader
        rdr = com.ExecuteReader
        While rdr.Read
            If pwd = rdr(0) Then
                stat = False
            End If
        End While
        connection.Close()
        rdr.Close()
        com.Dispose()
        Return stat
    End Function
    'update password history

    Private Sub ChangePWD(ByVal pwd As String, ByVal autoid As String)
        Dim com As New SqlCommand("update pwdhistory set AdminChange=1 , pwd='" & pwd & "',updatedate='" & System.DateTime.Now.ToString("d") & "',updatedby='" & Session("userid") & "' where autoid='" & autoid & "'", connection)
        connection.Open()
        com.ExecuteNonQuery()
        connection.Close()
        com.Dispose()
    End Sub
    'change user password
    Private Sub ChangeInRegistration(ByVal pwd As String, ByVal autoid As String)
        Dim com As New SqlCommand("update registration set pwd='" & pwd & "' where userid=(select userid from pwdhistory where autoid='" & autoid & "')", connection)
        connection.Open()
        com.ExecuteNonQuery()
        connection.Close()
        com.Dispose()
    End Sub
    'deleting user from datagrid

    Private Sub grdUsers_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdUsers.DeleteCommand
        ''Try
        ''    Dim itm As DataGridItem
        ''    Dim autoid
        ''    For Each itm In grdUsers.Items
        ''        If CType(itm.FindControl("chkSelect"), CheckBox).Checked = True Then
        ''            autoid = grdUsers.DataKeys(itm.ItemIndex)

        ''            Dim querydelete As String = "Delete from pwdHistory where autoid = " & autoid
        ''            Dim cmddelete As New SqlCommand(querydelete, connection)
        ''            connection.Open()
        ''            cmddelete.ExecuteNonQuery()
        ''            connection.Close()
        ''            cmddelete.Dispose()
        ''        End If
        ''    Next

        ''    dlbind(grdUsers)
        ''Catch ex As Exception
        ''    'Response.Write(ex)ss
        ''    Dim strmsg As String
        ''    strmsg = Replace(ex.Message.ToString, "'", "")
        ''    strmsg = Replace(strmsg, vbCrLf, " ")
        ''    WARSShowMsg(strmsg)
        ''End Try
        Try
            Dim itm As DataGridItem
            Dim autoid
            Dim count As Integer

            For Each itm In grdUsers.Items
                If CType(itm.FindControl("chkSelect"), CheckBox).Checked = True Then
                    '''''''''''''''''''''''''''''''changes maded by vini'''''''''''''''''''''''''''''''''''''''''
                    count = count + 1

                End If
            Next
            Dim total As Integer
            total = grdUsers.Items.Count
            If count = total Then
                WARSShowMsg("User can not delete all values!!!")
            Else
                For Each itm In grdUsers.Items
                    If CType(itm.FindControl("chkSelect"), CheckBox).Checked = True Then

                        '' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        autoid = grdUsers.DataKeys(itm.ItemIndex)
                        Dim querydelete As String = "Delete from pwdHistory where autoid = " & autoid
                        Dim cmddelete As New SqlCommand(querydelete, connection)
                        connection.Open()
                        cmddelete.ExecuteNonQuery()
                        connection.Close()
                        cmddelete.Dispose()
                    End If
                Next
                dlbind(grdUsers)
            End If

        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub
    ' save password duration

    Private Sub imgSaveDuration_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgSaveDuration.Click
        Try
            If txtDuration.Text = "" Then
                WARSShowMsg("Please fill duration!!!")
                WARSSetFocus(txtDuration)
                Exit Sub
            End If

            If Not IsNumeric(Trim(txtDuration.Text)) Then
                WARSShowMsg("Duration should be numeric!!!")
                WARSSetFocus(txtDuration)
                Exit Sub
            End If

            Dim com As New SqlCommand("update WARSPasswordDuration set duration='" & txtDuration.Text & "',updby='" & Session("userid") & "',currdate='" & System.DateTime.Now.ToString("d") & "'", connection)
            connection.Open()
            If com.ExecuteNonQuery() <> -1 Then
                WARSShowMsg("Successfully updated")
            End If
            connection.Close()
            com.Dispose()
            PassDuration()
        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub

    '***********************Password Check ***********************

    Private Function WARSChkPass(ByVal userid As String, ByVal pwd As String)
        If Len(Trim(pwd)) < 8 Then
            WARSShowMsg("Password must be at least 8 positions in length!!!")
            Return False
        End If

        If ChkUserIDPwdSimilar(userid, pwd) = False Then
            Return False
        End If

        If ObjLib.checkPassSymbols(pwd) = False Then
            WARSShowMsg("Please enter another password!!!")

            Return False
        End If

        Return True
    End Function
    'cheking password should not be userid
    Private Function ChkUserIDPwdSimilar(ByVal UserId, ByVal pwd)
        If InStr(UserId, pwd) = True Then
            WARSShowMsg("Password cannot contain userid!!!")
            Return False
        End If
        Return True
    End Function

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If rdoLock.SelectedValue = "Locked" Then
                imglock.Visible = True
                rdoLock.SelectedValue = "Locked"
            Else
                imglock.Visible = False
                rdoLock.SelectedValue = "Unlocked"
            End If
            AccountStaus(cboUserId.SelectedValue, rdoLock.SelectedValue, "Admin")
        Catch ex As Exception
            'Response.Write(ex)ss
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub

    'Private Sub grdUsers_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdUsers.PageIndexChanged
    '    If grdUsers.CurrentPageIndex < grdUsers.PageCount And grdUsers.CurrentPageIndex >= 0 Then
    '        Me.grdUsers.CurrentPageIndex = e.NewPageIndex
    '        WARSSearchMemPgChng("UserName", txtUser.Text, txtSearchcr.Text)
    '    End If
    '    lblCount.Text = "Page No. " & e.NewPageIndex + 1 & "/ Items On Page - " & grdUsers.Items.Count '''''''Display Page Number
    'End Sub

    Private Sub grdUsers_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles grdUsers.ItemDataBound

    End Sub

    Private Sub grdUsers_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdUsers.SelectedIndexChanged

    End Sub

End Class
