Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Reset Password                     *
'*  Summary: Resets the Password to Defaults      *
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class ResetPassword
    Inherits System.Web.UI.Page

    Dim fun As New Functions
    Dim dsProcess As New DataSet
    Public dept As String = "0"
    Public client As String = "0"
    Public lob As String = "0"
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim NSqlComm As New SqlCommand
    Dim dAdpt As New SqlDataAdapter
    Dim defPass As String
    Dim ds As New DataSet()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        '====================================================
        Dim cmd As New SqlCommand("select pass from IDMS_DefPass", conn)
        Dim reader As SqlDataReader
        conn.Open()
        reader = cmd.ExecuteReader
        If reader.Read Then
            defPass = reader("pass")
        End If
        reader.Close()
        cmd.Dispose()
        conn.Close()
        '=====================================================
        If Me.IsPostBack = False Then
            '' Fills ddlDepartment with departmentIDs

            ddlDepartmentuser.DataTextField = "departmentname"
            ddlDepartmentuser.DataValueField = "autoid"
            ddlDepartmentuser.DataSource = fun.bind_Department()
            ddlDepartmentuser.DataBind()
            ddlDepartmentuser.Items.Insert("0", "--Select--")
        End If
    End Sub

    Protected Sub ddlDepartmentuser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmentuser.SelectedIndexChanged
        If ddlDepartmentuser.SelectedIndex <> 0 Then
            Me.ddlLobuser.Items.Clear()
            Me.ddlClientuser.DataSource = fun.bind_client(ddlDepartmentuser.SelectedValue)
            Me.ddlClientuser.DataTextField = "ClientName"
            Me.ddlClientuser.DataValueField = "autoid"
            Me.ddlClientuser.DataBind()
            Me.ddlClientuser.Items.Insert("0", "--Select--")
            Me.ddlClientuser.Dispose()
            dsProcess.Dispose()
            conn.Close()
            'ddlReport.Items.Clear()
            dept = ddlDepartmentuser.SelectedValue
            client = "0"
            lob = "0"

        End If

    End Sub

    Protected Sub ddlClientuser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClientuser.SelectedIndexChanged
        If ddlDepartmentuser.SelectedIndex <> 0 Then
            Dim classobj As New Functions
            If ddlClientuser.SelectedValue <> "--Select--" Then

                ddlLobuser.DataTextField = "lobname"
                ddlLobuser.DataValueField = "autoid"
                ddlLobuser.DataSource = fun.bind_lob(ddlDepartmentuser.SelectedValue, ddlClientuser.SelectedValue)
                ddlLobuser.DataBind()
                ddlLobuser.Items.Insert(0, "--Select--")
                'ddlReport.Items.Clear()
                dept = ddlDepartmentuser.SelectedValue
                client = ddlClientuser.SelectedValue
                lob = "0"
            Else
                ddlLobuser.Items.Clear()

            End If
        End If
    End Sub

    Protected Sub ddlLobuser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobuser.SelectedIndexChanged
        If ddlLobuser.SelectedItem.Text = "--Select--" Then
            'ddlReport.Items.Clear()
            dept = ddlDepartmentuser.SelectedValue
            client = ddlClientuser.SelectedValue
            lob = ddlLobuser.SelectedValue

        End If
    End Sub

    Private Sub BindData(ByVal CalledFrom As String)
        Dim myConnection As New SqlConnection(con)
        Dim strDept As String = ""
        Dim strClient As String = ""
        Dim strLob As String = ""

        If CalledFrom = "User" Then
            If ddlDepartmentuser.Text = "" Or ddlDepartmentuser.Text = "--Select--" Then 'Will not possible
                WARSShowMsg("Please select department!!!")
                Exit Sub

            Else
                dept = ddlDepartmentuser.SelectedValue
                If (ddlClientuser.SelectedValue <> "--Select--" And ddlClientuser.SelectedValue <> "") Then
                    client = ddlClientuser.SelectedValue
                End If
                If (ddlLobuser.SelectedValue <> "--Select--" And ddlLobuser.SelectedValue <> "") Then
                    lob = ddlLobuser.SelectedValue
                End If
                'Dim ad As New SqlDataAdapter()
                Dim commStr As String = ""
                If Session("typeofuser") = "Super Admin" Then
                    commStr = "SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')"
                    'Dim Command As New SqlCommand("SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                    'ad.SelectCommand = Command
                Else
                    'old
                    'commStr = "SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')"

                    'old
                    commStr = "SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason ='$Transfer')"


                    'Dim Command As New SqlCommand("SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                    'ad.SelectCommand = Command
                End If
                Dim Adp As New SqlDataAdapter(commStr, myConnection)
                'Dim ds As New DataSet()
                Adp.Fill(ds)
                '===================================
                If ds.Tables(0).Rows.Count <= 0 Then
                    lblUNA.Visible = True
                    DGUser.DataSource = Nothing
                    DGUser.DataBind()
                    Exit Sub
                Else
                    lblUNA.Visible = False
                End If
                '===================================
                DGUser.DataSource = ds
                DGUser.DataBind()

            End If
        ElseIf CalledFrom = "Search" Then
            Dim commStr As String = ""
            If Session("typeofuser") = "Super Admin" Then
                commStr = "select * from registration where userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned')"
            Else
                'old
                'commStr = "select * from registration where deptid in (select deptid from masteradmin where adminid='" + Session("userid") + "') and userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned')"
                'old
                '  commStr = "SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned')"
                commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where userid <>'" + Session("userid") + "' and userid='" + Me.txtUserId.Text + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"

            End If
            Dim ad As New SqlDataAdapter(commStr, myConnection)
            'Dim ds As New DataSet()
            ad.Fill(ds)
            If ds.Tables(0).Rows.Count <= 0 Then
                lblUNA.Visible = True
                DGUser.DataSource = Nothing
                DGUser.DataBind()

                Exit Sub
            Else
                lblUNA.Visible = False
            End If


            DGUser.DataSource = ds
            DGUser.DataBind()
        Else
            WARSShowMsg("Critical error!!!")
            Exit Sub
        End If
    End Sub

    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click
        Session("Btn") = "User"
        BindData(Session("Btn"))
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtUserId.Text = "" Then
            WARSShowMsg("Please fill userid!!!")
            lblBlank.Visible = True
            Exit Sub
        Else
            lblBlank.Visible = False
        End If
        Session("Btn") = "Search"
        BindData(Session("Btn"))
    End Sub

    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<script language='javascript' type='text/javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "WARSShowMsg", str)
    End Sub
    Public Function durationcheck(ByVal usercheckid As String)
        Dim rdr As SqlDataReader
        Dim com As New SqlCommand("select max(updatedate) as date from PWDHistory  where userid='" & usercheckid & "'", conn)
        conn.Open()
        Dim datetable As DateTime
        Dim diff As TimeSpan

        rdr = com.ExecuteReader
        While rdr.Read
            If IsDBNull(rdr("date")) Then
            Else
                datetable = rdr("date")

                diff = System.DateTime.Now.Subtract(datetable)
                If diff.Days < 1 Then

                    Return "1"
                    If diff.Hours < 24 Then

                        Return "1"
                    End If


                End If
                If diff.Days = 1 Then
                    If diff.Hours < 24 Then

                        Return "1"
                    End If
                End If
                End If
        End While
        conn.Close()
        rdr.Close()
        Return "0"
    End Function
    Private Function chkLastFive(ByVal useruserid As String, ByVal userpassword As String)
        Dim rdr As SqlDataReader
        Dim com As New SqlCommand

        Dim pwd = userpassword
        Dim stat As Boolean = True
        com = New SqlCommand("select top 8 pwd from PWDHistory where userid='" & useruserid & "' order by autoid desc", conn)
        conn.Open()

        rdr = com.ExecuteReader
        While rdr.Read
            If pwd = Crypto.Decrypt(rdr(0)) Then
                stat = False

            End If
        End While
        conn.Close()
        rdr.Close()
        com.Dispose()
        Return stat
    End Function
    Public Sub UpdateChanges(ByVal changeTo As String, ByRef flag As Boolean)
        Dim txtAll As TextBox = CType(DGUser.Controls(0).Controls(0).FindControl("txtDefAll"), TextBox)
        Dim i As Integer = 0
        For i = 0 To DGUser.Items.Count - 1
            Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            Dim txtPass As TextBox = CType(DGUser.Items(i).FindControl("txtPassword"), TextBox)
            If txtPass.Text.Contains(lblUser.Text) Then
                WARSShowMsg("Password should not cantain userid of userid " & lblUser.Text & "!!!")
                Exit Sub
            End If
            If txtPass.Text <> "" Then
                If CB.Checked = True Then
                    If chkLastFive(lblUser.Text, txtPass.Text) = False Then
                        WARSShowMsg("Please choose a password other than eight old passwords for userid " & lblUser.Text & "!!!")
                        Exit Sub
                    End If
                End If

            End If

        Next
        For i = 0 To DGUser.Items.Count - 1
            Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            Dim txtPass As TextBox = CType(DGUser.Items(i).FindControl("txtPassword"), TextBox)
            
            If CB.Checked = True Then
                If durationcheck(lblUser.Text) = "1" Then
                    WARSShowMsg("Can not reset password with in 24 hours of last reset for userid: " & lblUser.Text & "!!!")
                    Exit Sub
                End If
            End If
        Next
        If ddlDepartmentuser.SelectedIndex <> 0 Then
            dept = ddlDepartmentuser.SelectedValue
            If ddlClientuser.SelectedIndex <> 0 Then
                client = ddlClientuser.SelectedValue
                If ddlLobuser.SelectedIndex <> 0 Then
                    lob = ddlLobuser.SelectedValue
                End If
            End If
        End If
        Dim cnt As Integer = 0
        For i = 0 To DGUser.Items.Count - 1
            Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            Dim txtPass As TextBox = CType(DGUser.Items(i).FindControl("txtPassword"), TextBox)
            Dim objDB As New Database()
            If changeTo = "Body" Then 'means Header Checkbox is not checked
                If CB.Checked = True Then

                    If txtPass.Text = "" Then
                        UpdateStatus(lblUser.Text, "$Reset", defPass, Session("userid")) 'defPass is already in encrypted  form

                        objDB.trackAccountForSlave(objDB.trackAccountForMaster(Session("userid"), "Set Default Password", "Password", lblUser.Text, dept, client, lob), "User Name", lblUser.Text)
                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                        conn.Open()
                        Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + lblUser.Text + "' and Action='Set Default Password'", conn)
                        cmm.ExecuteNonQuery()
                        conn.Close()
                        '''''''''''''''Usertype check for track goes here:- By Suvidha

                    Else
                        UpdateStatus(lblUser.Text, "$Reset", Crypto.Encrypt(txtPass.Text), Session("userid"))
                        objDB.trackAccountForSlave(objDB.trackAccountForMaster(Session("userid"), "Reset Password", "Password", lblUser.Text, dept, client, lob), "User Name", lblUser.Text)
                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                        conn.Open()
                        Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + lblUser.Text + "' and Action='Reset Password'", conn)
                        cmm.ExecuteNonQuery()
                        conn.Close()
                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                    End If
                    flag = True
                    'Code For Track


                    'objDB.trackAccountForSlave(objDB.trackAccountForMaster(Session("userid"), "Reset Password", "Password", lblUser.Text, dept, client, lob), "User Name", lblUser.Text)
                    'cnt = cnt + 1
                End If


            ElseIf changeTo = "Head" Then 'means Header Checkbox is Checked
            If txtAll.Text = "" Then 'means Default Password is not provided
                UpdateStatus(lblUser.Text, "$Reset", defPass, Session("userid"))
            Else
                UpdateStatus(lblUser.Text, "$Reset", Crypto.Encrypt(txtAll.Text), Session("userid"))
            End If
            flag = True

            End If
            If flag = True And changeTo = "Head" Then
                Dim objDB1 As New Database()
                objDB1.trackAccountForSlave(objDB.trackAccountForMaster(Session("userid"), "Set Default Password", "Password", lblUser.Text, dept, client, lob), "User Name", lblUser.Text)
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                conn.Open()
                Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + lblUser.Text + "' and Action='Set Default Password'", conn)
                cmm.ExecuteNonQuery()
                conn.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            End If

        Next
        
        ' WARSShowMsg("Password Reset: Successful")

    End Sub

    Public Function CheckHeaderChk() As Boolean
        If CType(DGUser.Controls(0).Controls(0).FindControl("chkAllItems"), CheckBox).Checked Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub UpdateStatus(ByVal userId As String, ByVal PwdStatus As String, ByVal NewPassword As String, ByVal ResetBy As String)
        
        Dim NSqlComm As New SqlCommand("sp_ResetPassword", conn)
        NSqlComm.CommandType = CommandType.StoredProcedure
        With NSqlComm.Parameters
            .AddWithValue("@UserId", userId)
            .AddWithValue("@PwdStatus", PwdStatus)
            .AddWithValue("@NewPassword", NewPassword)
            .AddWithValue("@ResetBy", ResetBy)
        End With

        conn.Open()
        NSqlComm.ExecuteNonQuery()
        conn.Close()
        'Force user to change password on next time login
        Dim cmdSave11 As New SqlCommand("update warscountlogin set countl=0 where userid=@userid", conn)
        cmdSave11.Parameters.AddWithValue("@userid", userId)
        conn.Open()
        cmdSave11.ExecuteNonQuery()
        conn.Close()
        'Increase duration of passwords by saving the record in pwd history
        Dim com As New SqlCommand("insert_PWDHistory", conn)

        com.CommandType = CommandType.StoredProcedure
        With com.Parameters
            .AddWithValue("@UserID", userId)
            .AddWithValue("@Pwd", NewPassword)
            .AddWithValue("@UpdateDate", System.DateTime.Now.ToString())
            .AddWithValue("@UpdatedBy", Session("userid"))
        End With
        conn.Open()
        com.ExecuteNonQuery()
        conn.Close()
        com.Dispose()
       
    End Sub
 
    Protected Sub DGUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGUser.ItemCommand
        Dim Updated As Boolean = False
        Dim flag As Boolean = False
        If CheckHeaderChk() = True Then
            UpdateChanges("Head", flag)
        Else
            UpdateChanges("Body", flag)
        End If
        If flag Then
            
            WARSShowMsg("Password has been reset successfully!!!")
        Else
            WARSShowMsg("Please select atleast one user!!!")
        End If
    End Sub

    Public Sub updateGrid()
        BindData(Session("Btn"))
    End Sub

   
End Class
