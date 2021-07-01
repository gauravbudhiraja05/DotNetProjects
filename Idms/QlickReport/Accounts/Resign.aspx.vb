Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Resignation                        *
'*  Summary: User can be Transfered/Resigned      *
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class Resign
    Inherits System.Web.UI.Page

    Dim fun As New Functions
    Dim dsProcess As New DataSet
    Public dept As String
    Public client As String
    Public lob As String
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim NSqlComm As New SqlCommand
    Dim dAdpt As New SqlDataAdapter
    Dim ds As New DataSet
    Dim deleteflag As Boolean = "false"


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

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
                    If chkShow.Checked = True Then
                        commStr = "SELECT  userid,username,status, isnull(lockreason,' ') as newlock FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' "
                    Else
                        commStr = "SELECT  userid,username,status, isnull(lockreason,' ') as newlock FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason = '$Transfer')"
                        'Dim Command As New SqlCommand("SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                        'ad.SelectCommand = Command
                    End If
                Else
                    'old
                    'commStr = "SELECT  userid,username,status, isnull(lockreason,' ') as newlock FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                    'old

                    If chkShow.Checked = True Then
                        commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where userid <>'" + Session("userid") + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  "
                    Else
                        commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where userid <>'" + Session("userid") + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"

                    End If

                    
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
                If chkShow.Checked Then
                    commStr = "select userid,username,status, isnull(lockreason,' ') as newlock from registration where userid='" + txtUserId.Text + "' "
                Else
                    commStr = "select  userid,username,status, isnull(lockreason,' ') as newlock from registration where userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                End If
            Else
                'old
                'commStr = "select  userid,username,status, isnull(lockreason,' ') as newlock from registration where deptid in (select deptid from masteradmin where adminid='" + Session("userid") + "') and userid='" + txtUserId.Text + "' and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                'old
                If chkShow.Checked = True Then
                    commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where userid <>'" + Session("userid") + "' and userid='" + Me.txtUserId.Text + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  "

                Else
                    'commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where deptid in(select deptid from masteradmin where userid <>'" + Session("userid") + "' and adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and lobid='" + lob + "' and clientid='" + client + "' and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                    commStr = "SELECT userid,username,status, isnull(lockreason,' ') as newlock FROM registration where userid <>'" + Session("userid") + "' and userid='" + Me.txtUserId.Text + "' and deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and lobid in (select lobid from masteradmin where adminid='" + Session("userid") + "') and clientid in  (select clientid from masteradmin where adminid='" + Session("userid") + "')  and not userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                End If


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


    Protected Sub DGUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGUser.ItemCommand
        Dim str As String = ""

        If e.CommandName = "cmdSave" Then
            str = "cmdSave"
            pandelete.Visible = True
        ElseIf e.CommandName = "cmdTransfer" Then
            str = "cmdTransfer"
            panTransfer.Visible = True
        ElseIf e.CommandName = "cmdreTransfer" Then
            str = "cmdreTransfer"
            panreTransfer.Visible = True
        End If




        'If deleteflag = True Then
        '    UpdateChanges(str)
        '    updateGrid()
        'End If

    End Sub

    Public Sub UpdateChanges(ByVal updatetype As String)
        Dim i As Integer = 0
        Dim flag As Boolean = False
        Dim reFlag As Boolean = False
        For i = 0 To DGUser.Items.Count - 1
            Dim CB As CheckBox = CType(DGUser.Items(i).FindControl("chkSelect"), CheckBox)
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            If CB.Checked = True Then

                'pandelete.Visible = True

                'If deleteflag = True Then
                UpdateStatus(lblUser.Text, updatetype, reFlag)
                flag = True
                'End If

            End If
        Next
        If Not flag Then
            WARSShowMsg("Please select atleast one user!!!")
        Else
            If Not reFlag Then
                If updatetype = "cmdTransfer" Then
                    WARSShowMsg("Operation Transfer: Completed")
                ElseIf updatetype = "cmdreTransfer" Then
                    WARSShowMsg("Operation ReJoin: Completed")
                ElseIf updatetype = "cmdSave" Then
                    WARSShowMsg("Operation Resign: Completed")
                End If

            End If
        End If
    End Sub

    Public Sub updateGrid()
        BindData(Session("Btn"))
    End Sub

    Public Sub UpdateStatus(ByVal userId As String, ByVal type As String, ByRef reflag As Boolean)
        If type = "cmdSave" Then
            Dim resStr As String = "select * from registration where userid='" & userId & "' and (lockreason='$Resigned' or lockreason='$Transfer')"
            Dim myConnection As New SqlConnection(con)
            Dim adp As New SqlCommand(resStr, myConnection)
            myConnection.Open()
            Dim tempReader As SqlDataReader
            tempReader = adp.ExecuteReader

            If tempReader.Read() Then
                reflag = True
                WARSShowMsg("Operation resign does not applicable!!!")
                reflag = True
                Exit Sub
            End If

            Dim NSqlComm As New SqlCommand("sp_UpdateResign", conn)
            NSqlComm.CommandType = CommandType.StoredProcedure


            NSqlComm.Parameters.AddWithValue("@UserId", userId)
            NSqlComm.Parameters.AddWithValue("@ActionBy", Session("userid"))
            NSqlComm.Parameters.AddWithValue("@Action", "Resign")
            NSqlComm.Parameters.AddWithValue("@Date", System.DateTime.Now)
            NSqlComm.Parameters.AddWithValue("@Entity", "User")
            NSqlComm.Parameters.AddWithValue("@EntityName", userId)
            NSqlComm.Parameters.AddWithValue("@DeptID", dept1.Value)
            'If IsNumeric(ddlClientuser.SelectedValue) Then
            '    client = ddlClientuser.SelectedValue
            'Else
            '    client = 0
            'End If
            'If IsNumeric(ddlLobuser.SelectedValue) Then
            '    lob = ddlLobuser.SelectedValue
            'Else
            '    lob = 0
            'End If
            If client1.Value = "" Then
                client1.Value = 0

            End If
            If lob1.Value = "" Then
                lob1.Value = 0
            End If
            NSqlComm.Parameters.AddWithValue("@ClientID", client1.Value)
            NSqlComm.Parameters.AddWithValue("@LOBID", lob1.Value)
            NSqlComm.Parameters.AddWithValue("@AutoId", SqlDbType.Int).Direction = ParameterDirection.Output




            conn.Open()
            NSqlComm.ExecuteNonQuery()
            Dim AutoId As Int32
            AutoId = NSqlComm.Parameters("@AutoId").Value


            conn.Close()
            '''track for details
            NSqlComm = New SqlCommand("sp_LogAccountSlave", conn)
            NSqlComm.CommandType = CommandType.StoredProcedure
            NSqlComm.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            NSqlComm.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = "User Resigned"
            NSqlComm.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = userId
            conn.Open()
            NSqlComm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            conn.Open()
            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + userId + "' and Action='Resign'", conn)
            cmm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

        ElseIf type = "cmdTransfer" Then
            'check if resigned then abort operation

            Dim resStr2 As String = "select * from registration where userid='" & userId & "' and (lockreason='$Resigned' or lockreason='$Transfer')"
            Dim myConnection2 As New SqlConnection(con)
            Dim adp2 As New SqlCommand(resStr2, myConnection2)
            myConnection2.Open()
            Dim tempReader2 As SqlDataReader
            tempReader2 = adp2.ExecuteReader

            If tempReader2.Read() Then
                reflag = True
                WARSShowMsg("Operation transfer does not applicable!!!")
                reflag = True
                Exit Sub
            End If
           
            '--------------------------------------
            Dim NSqlComm As New SqlCommand("sp_UpdateTransfer", conn)
            NSqlComm.CommandType = CommandType.StoredProcedure
            With NSqlComm.Parameters
                .AddWithValue("@UserId", userId)
                .AddWithValue("@ActionBy", Session("userid"))
                .AddWithValue("@Action", "Transfer")
                .AddWithValue("@Date", System.DateTime.Now)
                .AddWithValue("@Entity", "User")
                .AddWithValue("@EntityName", userId)
                .AddWithValue("@DeptID", dept1.Value)
                If client1.Value = "" Then
                    client1.Value = 0

                End If
                If lob1.Value = "" Then
                    lob1.Value = 0
                End If
                .AddWithValue("@ClientID", client1.Value)
                .AddWithValue("@LOBID", lob1.Value)
                .AddWithValue("@AutoId", SqlDbType.Int).Direction = ParameterDirection.Output

            End With
            conn.Open()
            NSqlComm.ExecuteNonQuery()
            Dim AutoId As Int32
            AutoId = NSqlComm.Parameters("@AutoId").Value
            conn.Close()
            '''track for details
            NSqlComm = New SqlCommand("sp_LogAccountSlave", conn)
            NSqlComm.CommandType = CommandType.StoredProcedure
            NSqlComm.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            NSqlComm.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = "User Transferred"
            NSqlComm.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = userId
            conn.Open()
            NSqlComm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            conn.Open()
            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + userId + "' and Action='Transfer'", conn)
            cmm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
        ElseIf type = "cmdreTransfer" Then
            'Check whether user has resigned and called for Rejoin"
            Dim resStr As String = "select * from registration where userid='" & userId & "' and not lockreason='$Transfer'"
            Dim myConnection As New SqlConnection(con)
            Dim adp As New SqlCommand(resStr, myConnection)
            myConnection.Open()
            Dim tempReader As SqlDataReader
            tempReader = adp.ExecuteReader

            If tempReader.Read() Then
                reflag = True
                WARSShowMsg("Operation rejoin is not applicable!!!")
                Exit Sub
            End If
           
            Dim NSqlComm1 As New SqlCommand("sp_UpdateTransfer", conn)
            NSqlComm1.CommandType = CommandType.StoredProcedure
            With NSqlComm1.Parameters
                .AddWithValue("@UserId", userId)
                .AddWithValue("@ActionBy", Session("userid"))
                .AddWithValue("@Action", "Cancel Transfer")
                .AddWithValue("@Date", System.DateTime.Now)
                .AddWithValue("@Entity", "User")
                .AddWithValue("@EntityName", userId)
                .AddWithValue("@DeptID", dept1.Value)
                If client1.Value = "" Then
                    client1.Value = 0

                End If
                If lob1.Value = "" Then
                    lob1.Value = 0
                End If
                .AddWithValue("@ClientID", client1.Value)
                .AddWithValue("@LOBID", lob1.Value)
                .AddWithValue("@AutoId", SqlDbType.Int).Direction = ParameterDirection.Output

            End With
            conn.Open()
            NSqlComm1.ExecuteNonQuery()

            conn.Close()


            '------------------------------------------------------
            Dim NSqlComm As New SqlCommand("sp_UpdateReTransfer", conn)
            NSqlComm.CommandType = CommandType.StoredProcedure
            With NSqlComm.Parameters
                .AddWithValue("@UserId", userId)
            End With
            conn.Open()
            NSqlComm.ExecuteNonQuery()

            conn.Close()
            '--------------------------------------
            Dim AutoId As Int32
            AutoId = NSqlComm1.Parameters("@AutoId").Value
            conn.Close()
            '''track for details
            NSqlComm1 = New SqlCommand("sp_LogAccountSlave", conn)
            NSqlComm1.CommandType = CommandType.StoredProcedure
            NSqlComm1.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            NSqlComm1.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = "Transfer Cancelled"
            NSqlComm1.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = userId
            conn.Open()
            NSqlComm1.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            conn.Open()
            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" + userId + "' and Action='Cancel Transfer'", conn)
            cmm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

        End If
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

    Function showStatus(ByVal upStatus As String) As String
        If upStatus = "$Resigned" Then
            Return "Resigned"
        ElseIf upStatus = "$Transfer" Then
            Return "Transfered"
        Else
            Return "Working"
        End If
    End Function

    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click

        'deleteflag = True
        UpdateChanges("cmdSave")
        updateGrid()
        pandelete.Visible = False
    End Sub

    Protected Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click


        'deleteflag = False



        pandelete.Visible = False
    End Sub

    Protected Sub transferyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles transferyes.Click
        UpdateChanges("cmdTransfer")
        updateGrid()
        panTransfer.Visible = False
    End Sub

    Protected Sub transferno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles transferno.Click
        panTransfer.Visible = False
    End Sub

    Protected Sub cancelyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelyes.Click
        UpdateChanges("cmdreTransfer")
        updateGrid()
        panreTransfer.Visible = False
    End Sub

    Protected Sub cancelNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cancelNo.Click
        panreTransfer.Visible = False
    End Sub

    Protected Sub DGUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DGUser.SelectedIndexChanged

    End Sub
End Class
