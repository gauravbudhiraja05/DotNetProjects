Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
'**************************************************
'*  Project Name: IDMS Phase 2                    *
'*  Module Name: Accounts Management              *
'*  Page Name: Password Duration                  *
'*  Summary: Sets the Duration for Password Expiry*
'*  Created on: 10/05/08                          *
'*  Created By: Yogesh Kumar Verma                *
'**************************************************
Partial Class PasswordDuration
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
        'Session("userid") = "peter4200"
        Page.SmartNavigation = True
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
                    commStr = "select distinct a.userid,a.username,b.duration,c.updatedate,c.updatedby from (registration as a left join warspasswordduration as b on a.userid = b.userid) left join (select updatedate, autoid, userid, updatedby from pwdhistory where autoid in (select max(autoid) as autoid from pwdhistory group by userid)) as c on b.userid = c.userid where a.deptid='" + dept + "' and a.lobid='" + lob + "' and a.clientid='" + client + "' and not a.userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                    'commStr = "SELECT * FROM registration WHERE deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')"
                    'Dim Command As New SqlCommand("SELECT * FROM registration WHERE deptid='" + ddlDepartmentuser.Text + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                    'ad.SelectCommand = Command
                    'Else
                    '    commStr = "select a.userid,a.username,b.duration,c.updatedate,c.updatedby from (registration as a left join warspasswordduration as b on a.userid = b.userid) left join (select updatedate, autoid, userid, updatedby from pwdhistory where autoid in (select max(autoid) as autoid from pwdhistory group by userid)) as c on b.userid = c.userid where a.userid='" + txtUserId.Text + "' and not a.userid in (select userid from registration where lockreason = '$Resigned')"
                    '    'Dim Command As New SqlCommand("SELECT * FROM registration where deptid in(select deptid from masteradmin where adminid='" + Session("userid") + "') and deptid='" + dept + "' and lobid='" + lob + "' and clientid='" + client + "' and not userid in (select userid from registration where lockreason = '$Resigned')", myConnection)
                    '    'ad.SelectCommand = Command
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

                ' DGUser.Visible = True

                userattach.Value = "All users of selected span"
            End If
        ElseIf CalledFrom = "Search" Then
            Dim commStr As String = ""
            If Session("typeofuser") = "Super Admin" Then
                commStr = "select distinct a.userid,a.username,b.duration,c.updatedate,c.updatedby,a.DeptId,a.ClientId,a.LobId from (registration as a left join warspasswordduration as b on a.userid = b.userid) left join (select updatedate, autoid, userid, updatedby from pwdhistory where autoid in (select max(autoid) as autoid from pwdhistory group by userid)) as c on b.userid = c.userid where a.userid='" + txtUserId.Text + "' and not a.userid in (select userid from registration where lockreason = '$Resigned' or lockreason='$Transfer')"
                'Else
                '    commStr = "select a.userid,a.username,b.duration,c.updatedate,c.updatedby from (registration as a left join warspasswordduration as b on a.userid = b.userid) left join (select updatedate, autoid, userid, updatedby from pwdhistory where autoid in (select max(autoid) as autoid from pwdhistory group by userid)) as c on b.userid = c.userid where a.userid='" + txtUserId.Text + "' and not a.userid in (select userid from registration where lockreason = '$Resigned')"
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
                Session("span") = ds.Tables(0).Rows(0)("DeptId").ToString() + "#" + ds.Tables(0).Rows(0)("ClientId").ToString() + "#" + ds.Tables(0).Rows(0)("LobId").ToString()
                lblUNA.Visible = False
            End If


            DGUser.DataSource = ds
            DGUser.DataBind()
            ' DGUser.Visible = True
            userattach.Value = txtUserId.Text


        Else
            WARSShowMsg("Critical error!!!")
            Exit Sub
        End If

    End Sub

    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click
        DGUser.Visible = True
        Session("Btn") = "User"
        BindData(Session("Btn"))

        lblDuration.Visible = False


    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If txtUserId.Text = "" Then
            WARSShowMsg("Please fill userid!!!")
            lblBlank.Visible = True
            Exit Sub
        Else
            lblBlank.Visible = False
        End If
        lblDuration.Visible = False
        Session("Btn") = "Search"
        BindData(Session("Btn"))
        DGUser.Visible = True
    End Sub

    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        ClientScript.RegisterStartupScript(Me.GetType(), "WARSShowMsg", str)
    End Sub

    Public Sub UpdateChanges(ByRef Duration As String)

        Dim i As Integer = 0
        Dim cnt As Integer = 0
        cnt = DGUser.Items.Count
        For i = 0 To DGUser.Items.Count - 1
            Dim lblUser As Label = CType(DGUser.Items(i).FindControl("lblUserId"), Label)
            If UserExist(lblUser.Text) = True Then
                UpdateStatus(lblUser.Text, Duration, Session("userid"), "Update")
            Else
                UpdateStatus(lblUser.Text, Duration, Session("userid"), "New")
            End If

        Next
        'Track for set duration
        Dim cmd As SqlCommand
        Dim strSpan() As String
        Try

        
            If ddlDepartmentuser.SelectedIndex <> 0 Then
                dept = ddlDepartmentuser.SelectedValue
                If ddlClientuser.SelectedIndex <> 0 Then
                    client = ddlClientuser.SelectedValue
                    If ddlLobuser.SelectedIndex <> 0 Then
                        lob = ddlLobuser.SelectedValue
                    End If
                End If
                Session("span") = dept + "#" + client + "#" + lob
            Else
                strSpan = Session("span").ToString().Split("#")
                dept = strSpan(0)
                client = strSpan(1)
                lob = strSpan(2)
            End If

            cmd = New SqlCommand("sp_LogAccountMaster", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = 0
            cmd.Parameters(0).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@ActionBy", SqlDbType.VarChar, 100).Value = Session("userid")
            cmd.Parameters.Add("@Action", SqlDbType.VarChar, 50).Value = "Set Password Duration"
            cmd.Parameters.Add("@Date", SqlDbType.VarChar, 50).Value = System.DateTime.Now()
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar, 50).Value = "Password"

            If (Session("Btn").ToString() = "User") Then
                If (ddlDepartmentuser.SelectedItem.Text <> "--Select--") Then
                    If (ddlClientuser.SelectedItem.Text <> "--Select--") Then
                        If (ddlLobuser.SelectedItem.Text <> "--Select--") Then
                            cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 500).Value = ddlDepartmentuser.SelectedItem.Text + " > " + ddlClientuser.SelectedItem.Text + " > " + ddlLobuser.SelectedItem.Text
                        Else
                            cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 500).Value = ddlDepartmentuser.SelectedItem.Text + " > " + ddlClientuser.SelectedItem.Text
                        End If
                    Else
                        cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 500).Value = ddlDepartmentuser.SelectedItem.Text
                   
                    End If
                


                End If


            ElseIf (Session("Btn").ToString() = "Search") Then
                cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 500).Value = txtUserId.Text

            End If

            cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50).Value = dept
            cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50).Value = client
            cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50).Value = lob
            cmd.Parameters.Add("@type", SqlDbType.VarChar, 20).Value = Session("usertype")
            conn.Open()
            cmd.ExecuteNonQuery()
            Dim AutoId As Int32
            AutoId = cmd.Parameters(0).Value
            cmd.Dispose()
            conn.Close()
            ' track for details

            cmd = New SqlCommand("sp_LogAccountSlave1", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            cmd.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = "No Of Records Effected"
            cmd.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = cnt
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            'had to call it twice
            cmd = New SqlCommand("sp_LogAccountSlave1", conn)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            cmd.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = "UserID"
            cmd.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = userattach.Value
            conn.Open()
            cmd.ExecuteNonQuery()
            conn.Close()
            ' suvidha
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            conn.Open()
            Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='Password' and Action='Set Password Duration'", conn)
            cmm.ExecuteNonQuery()
            conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha


        Catch ex As Exception
            WARSShowMsg("Critical error!!!")
        End Try
    End Sub

    Public Function UserExist(ByVal UserId As String) As Boolean
        Dim myConnection As New SqlConnection(con)
        Dim rowC As Integer
        Dim chkUser As New SqlCommand("select count(*) as co from warspasswordduration where userid='" + UserId + "'", myConnection)
        Dim dr As SqlDataReader
        myConnection.Open()
        dr = chkUser.ExecuteReader()
        If dr.Read Then
            rowC = Val(dr.GetValue(0))
            If rowC > 0 Then
                Return True
            End If
        End If
        myConnection.Close()
        chkUser.Dispose()
    End Function

    Public Sub UpdateStatus(ByVal userId As String, ByVal Duration As Integer, ByVal ChangedBy As String, ByVal CmdType As String)
        Dim durtion As String = Duration
        If Duration < 1 Then
            WARSShowMsg("Duration is not meaningfull!!!")
            Exit Sub
        ElseIf durtion = "" Then
            WARSShowMsg("Duration is not meaningfull!!!")
            Exit Sub
        End If
        If CmdType = "New" Then
            Dim myConnection As New SqlConnection(con)
            Dim UDC As New SqlCommand("sp_CreateDuration", myConnection)
            UDC.CommandType = CommandType.StoredProcedure
            With UDC.Parameters
                .AddWithValue("@UserId", userId)
                .AddWithValue("@Duration", Duration)
                .AddWithValue("@UpdBy", ChangedBy)
                .AddWithValue("@CurrDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
            End With
            myConnection.Open()
            UDC.ExecuteNonQuery()
            myConnection.Close()
        Else
            Dim myConnection As New SqlConnection(con)
            Dim UDC As New SqlCommand("sp_SetDuration", myConnection)
            UDC.CommandType = CommandType.StoredProcedure
            With UDC.Parameters
                .AddWithValue("@UserId", userId)
                .AddWithValue("@Duration", Duration)
                .AddWithValue("@UpdBy", ChangedBy)
                .AddWithValue("@CurrDate", FormatDateTime(System.DateTime.Today, DateFormat.ShortDate))
            End With
            myConnection.Open()
            UDC.ExecuteNonQuery()
            myConnection.Close()

           

        End If
        WARSShowMsg("Duration has been completed successfully!!!")
        DGUser.Visible = False
    End Sub

    Protected Sub DGUser_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGUser.ItemCommand
        If e.CommandName = "cmdSave" Then
            Dim Updated As Boolean = False
            Dim Flag As Boolean = False
            Dim txt As TextBox = CType(e.Item.FindControl("txtDuration"), TextBox)
            If txt.Text = "" Then
                WARSShowMsg("Please fill default duration!!!")
                lblDuration.Visible = True
                Exit Sub
            Else
                lblDuration.Visible = False
                UpdateChanges(txt.Text)
                updateGrid()
            End If
        End If
    End Sub 'Calls CheckHeaderChk() and UpdateChanges()

    Public Sub updateGrid()
        BindData(Session("Btn"))
    End Sub

    Protected Sub DGUser_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DGUser.PageIndexChanged
        DGUser.CurrentPageIndex = e.NewPageIndex
        updateGrid()
    End Sub
End Class
