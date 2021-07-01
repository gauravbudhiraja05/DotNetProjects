Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Accounts                           *
'*  Summary: Locks/Unlocks the User Accounts      *
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class Accounts_Account
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
    Dim ds As New DataSet()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("userid") = "peter4200"
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
                dept1.Value = dept
                If (ddlClientuser.SelectedValue <> "--Select--" And ddlClientuser.SelectedValue <> "") Then
                    client = ddlClientuser.SelectedValue
                    client1.Value = client
                Else
                    client = 0
                End If
                If (ddlLobuser.SelectedValue <> "--Select--" And ddlLobuser.SelectedValue <> "") Then
                    lob = ddlLobuser.SelectedValue
                    lob1.Value = lob
                Else
                    lob = 0
                End If
                'Dim ad As New SqlDataAdapter()
                Dim commStr As String = ""
                If Session("typeofuser") = "Super Admin" Then
                    commStr = "SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                    'Dim Command As New SqlCommand("SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                    'ad.SelectCommand = Command
                Else
                    'old
                    'commStr = "SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                    'old
                    commStr = "SELECT * FROM registration where userid <>'" + Session("userid") + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"


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
            If ddlDepartmentuser.Text = "" Or ddlDepartmentuser.Text = "--Select--" Then
                Dim resStr1 As String = "select * from registration where userid='" + txtUserId.Text + "' "
                Dim myConnection1 As New SqlConnection(con)
                Dim adp1 As New SqlCommand(resStr1, myConnection1)
                myConnection1.Open()
                Dim tempReader1 As SqlDataReader
                tempReader1 = adp1.ExecuteReader
                If tempReader1.Read() Then
                    dept = tempReader1("DeptID").ToString
                    dept1.Value = dept
                    client = tempReader1("ClientID").ToString
                    client1.Value = client
                    lob = tempReader1("LOBID").ToString
                    lob1.Value = lob
                End If
            End If
            Dim commStr As String = ""
            If Session("typeofuser") = "Super Admin" Then
                commStr = "select * from registration where  userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
            Else
                'old
                'commStr = "select * from registration where deptid in (select deptid from masteradmin where adminid='" + Session("userid") + "') and userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                '0ld
                'commStr = "SELECT * FROM registration where userid <>'" + Session("userid") + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                commStr = "SELECT * FROM registration where userid <>'" + Session("userid") + "' and userid='" + Me.txtUserId.Text + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
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

    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "WARSShowMsg", str)
    End Sub

    Function CheckTextBox(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) As Integer
        Dim i As Integer = 0
        Dim chkflag As Boolean = False

        If e.CommandName = "cmdSave" Then
            For i = 0 To DGUser.Items.Count - 1
                Dim TB As TextBox = CType(DGUser.Items(i).FindControl("txtreason"), TextBox)
                Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)

                If CB.Checked = True Then
                    If TB.Text = "" Then
                        Return 2
                    End If
                    chkflag = True
                End If
            Next
            If Not chkflag Then
                Return 1
            End If
        End If
    End Function

    Public Sub UpdateChanges()
        Dim i As Integer = 0
        Dim strMsg As String = "Updated:"
        For i = 0 To DGUser.Items.Count - 1
            Dim TB As TextBox = CType(DGUser.Items(i).FindControl("txtreason"), TextBox)
            Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)
            Dim lblSt As Label = CType(DGUser.Items(i).FindControl("lblStatus"), Label)
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            Dim txtC As TextBox = CType(DGUser.Items(i).FindControl("txtReason"), TextBox)
            Dim Action As String = ""
            If CB.Checked = True Then
                Dim newstr As String = ""
                newstr = Trim(lblSt.Text)
                If newstr = "Unlocked" Then
                    newstr = "Active"
                    Action = "Locked"
                ElseIf newstr = "Locked" Then
                    newstr = "Deactive"
                    Action = "UnLocked"
                End If
                If newstr = "Active" Then 'If Trim(lblSt.Text) = "Active" Then
                    UpdateStatus(lblUser.Text, "Deactive", txtC.Text, "Locked")
                    'track changes
                    Dim objDB As New Database()
                    'If ddlDepartmentuser.SelectedIndex <> 0 Then
                    '    dept = ddlDepartmentuser.SelectedValue
                    '    If ddlClientuser.SelectedIndex <> 0 Then
                    '        client = ddlClientuser.SelectedValue
                    '        If ddlLobuser.SelectedIndex <> 0 Then
                    '            lob = ddlLobuser.SelectedValue
                    '        End If
                    '    End If
                    'End If
                    If client1.Value = "" Then
                        client1.Value = 0

                    End If
                    If lob1.Value = "" Then
                        lob1.Value = 0
                    End If
                    objDB.trackAccountForMaster(Session("userid"), Action, "User", lblUser.Text, dept1.Value, client1.Value, lob1.Value)


                    'track changes
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    conn.Open()
                    Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + lblUser.Text + "' and Action='Locked'", conn)
                    cmm.ExecuteNonQuery()
                    conn.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    'strMsg = strMsg + Chr(15) + lblUser.Text + " :Deactivated"
                Else
                    UpdateStatus(lblUser.Text, "Active", txtC.Text, "UnLocked")
                    'track changes
                    Dim objDB As New Database()
                    'If ddlDepartmentuser.SelectedIndex <> 0 Then
                    '    dept = ddlDepartmentuser.SelectedValue
                    '    If ddlClientuser.SelectedIndex <> 0 Then
                    '        client = ddlClientuser.SelectedValue
                    '        If ddlLobuser.SelectedIndex <> 0 Then
                    '            lob = ddlLobuser.SelectedValue
                    '        End If
                    '    End If
                    'End If
                    If client1.Value = "" Then
                        client1.Value = 0

                    End If
                    If lob1.Value = "" Then
                        lob1.Value = 0
                    End If
                    objDB.trackAccountForMaster(Session("userid"), Action, "User", lblUser.Text, dept1.Value, client1.Value, lob1.Value)


                    'track changes
                    '''''''''''''''Usertype check for track goes here:- By Suvidha
                    conn.Open()
                    Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + lblUser.Text + "' and Action='UnLocked'", conn)
                    cmm.ExecuteNonQuery()
                    conn.Close()
                    '''''''''''''''Usertype check for track goes here:- By Suvidha

                    'strMsg = strMsg + Chr(15) + lblUser.Text + " :Activated"

                    Dim chkuseract As Integer = 0
                    Dim cmdSave112 As New SqlCommand("select countl from warscountlogin where userid=@userid", conn)
                    cmdSave112.Parameters.AddWithValue("@userid", lblUser.Text)
                    Dim rrrddd As SqlDataReader
                    conn.Open()
                    rrrddd = cmdSave112.ExecuteReader
                    If rrrddd.Read Then
                        chkuseract = rrrddd("countl")
                    End If
                    rrrddd.Close()
                    cmdSave112.ExecuteNonQuery()
                    conn.Close()
                    If chkuseract > 10 Then
                        Dim cmdSave11 As New SqlCommand("update warscountlogin set countl=0 where userid=@userid", conn)
                        cmdSave11.Parameters.AddWithValue("@userid", lblUser.Text)
                        conn.Open()
                        cmdSave11.ExecuteNonQuery()
                        conn.Close()
                    Else
                        Dim cmdSave11 As New SqlCommand("update warscountlogin set countl=1 where userid=@userid", conn)
                        cmdSave11.Parameters.AddWithValue("@userid", lblUser.Text)
                        conn.Open()
                        cmdSave11.ExecuteNonQuery()
                        conn.Close()
                    End If
                End If
               

               
            End If
        Next
        'WARSShowMsg(strMsg)
    End Sub

    Protected Sub DGUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGUser.ItemCommand
        Dim i As Integer = CheckTextBox(source, e)
        If i = 2 Then
            WARSShowMsg("Please fill the reason for operation!!!")
            Exit Sub
        ElseIf i = 1 Then
            WARSShowMsg("Please select atleast one user!!!")
            Exit Sub
        End If
        chklist()
        UpdateChanges()
        updateGrid()
    End Sub

    Public Sub updateGrid()
        'WARSShowMsg(Session("Btn"))
        BindData(Session("Btn"))
    End Sub

    Public Sub chklist()
        Dim grd As DataGrid = DGUser
        Dim i As Integer
        For i = 0 To grd.Items.Count - 1
            If CType(grd.Items(i).FindControl("chkSelect"), CheckBox).Checked = False Then
                CType(grd.Items(i).FindControl("txtreason"), TextBox).Text = ""
            End If
        Next
    End Sub

    Public Sub UpdateStatus(ByVal userId As String, ByVal Status As String, ByVal Cause As String, ByVal PwdStatus As String)
        
        Dim NSqlComm As New SqlCommand("sp_UpdateStatus", conn)
        NSqlComm.CommandType = CommandType.StoredProcedure
        With NSqlComm.Parameters
            .AddWithValue("@UserId", userId)
            .AddWithValue("@UserStatus", Status)
            .AddWithValue("@Cause", Cause)
            .AddWithValue("@PwdStatus", PwdStatus)
            .AddWithValue("@LockDate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
        End With

        conn.Open()
        NSqlComm.ExecuteNonQuery()
        conn.Close()

        If PwdStatus = "Locked" Then
            WARSShowMsg("User operation: Locked!!!")
        Else
            WARSShowMsg("User Operation: UnLocked!!!")
        End If
        'WARSShowMsg("User Operation: Successful!")
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

    Function setStatus(ByVal mystatus As String) As String
        If mystatus = "Deactive" Then
            Return "Locked"
        Else
            Return "Unlocked"
        End If
    End Function

   
    Protected Sub DGUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGUser.SelectedIndexChanged

    End Sub
End Class
