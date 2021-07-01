Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings


Partial Class AdvanceRightsManagement_MakeAdmin
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(str)
    Dim con As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartment As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLob As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim deptid1 As String
    Dim client_id1 As String
    Dim lobid1 As String
    Dim deptid2 As String
    Dim client_id2 As String
    Dim lobid2 As String
    Dim tableobj As New TableRight
    Dim uspan As New UserSpan
    Dim loggedId As String
    Dim UserType As String
    ''' <summary>
    ''' fill the span using user control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Dim cmdnew As SqlCommand
    Dim danew As SqlDataAdapter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.SmartNavigation = True

        If Session("userid") = "" Then
            Response.Redirect("~/SessionExpired.aspx")
        Else
            loggedId = Session("userid").ToString()
            UserType = 3

        End If

        ddlDepartment = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        ddlDepartmentUser = CType(DCLUserControl2.FindControl("ddlDepartment"), DropDownList)
        ddlClientUser = CType(DCLUserControl2.FindControl("ddlClient"), DropDownList)
        ddlLobUser = CType(DCLUserControl2.FindControl("ddlLob"), DropDownList)



        If Me.IsPostBack = False Then
            Dim cmd As New SqlCommand("select DepartmentName,AutoID from IdmsDepartment where SavedBy='" + Session("userid") + "'", connection)
            Dim adp As New SqlDataAdapter
            adp.SelectCommand = cmd
            Dim ds As New DataSet()
            adp.Fill(ds)
            ddlDepartmentUser.DataSource = ds
            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "AutoID"
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "-----Select-----")
            ds.Dispose()
            adp.Dispose()
            cmd.Dispose()
            'ddlDepartmentUser.DataTextField = "DepartmentName"
            'ddlDepartmentUser.DataValueField = "DeptID"
            'ddlDepartmentUser.DataSource = fun.bind_Dept()
            'ddlDepartmentUser.DataBind()
            'ddlDepartmentUser.Items.Insert(0, "--Select--")
        End If
        If Me.IsPostBack = False Then
            Dim cmd As New SqlCommand("select DepartmentName,AutoID from IdmsDepartment where SavedBy='" + Session("userid") + "'", connection)
            Dim adp As New SqlDataAdapter
            adp.SelectCommand = cmd
            Dim ds As New DataSet()
            adp.Fill(ds)
            ddlDepartment.DataSource = ds
            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "AutoID"
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "-----Select-----")
            ds.Dispose()
            adp.Dispose()
            cmd.Dispose()
            'ddlDepartment.DataTextField = "DepartmentName"
            'ddlDepartment.DataValueField = "DeptID"
            'ddlDepartment.DataSource = fun.bind_Dept()
            'ddlDepartment.DataBind()
            'ddlDepartment.Items.Insert(0, "--Select--")
        End If

        'connection.Close()
        Me.Form.DefaultButton = Me.btnMakeAdmin.UniqueID

    End Sub
    ''' <summary>
    ''' 'Method to bind ddlSelectNewAdmin to choose new owner
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindNewAdmin()

        If ddlLobUser.SelectedIndex > 0 And ddlClientUser.SelectedIndex > 0 And ddlDepartmentUser.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLobUser.SelectedValue)
            client_id = Convert.ToInt32(ddlClientUser.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentUser.SelectedValue)
            ddlSelectNewAdmin.DataSource = uspan.bind_UserSAdminlob(deptid, client_id, lobid)
            ddlSelectNewAdmin.DataTextField = "disname"
            ddlSelectNewAdmin.DataValueField = "UserId"
            ddlSelectNewAdmin.DataBind()
            ddlSelectNewAdmin.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlDepartmentUser.SelectedIndex > 0 And ddlClientUser.SelectedIndex > 0 Then
            client_id = Convert.ToInt32(ddlClientUser.SelectedValue)
            deptid = ddlDepartmentUser.SelectedValue.ToString()
            ddlSelectNewAdmin.DataSource = uspan.bind_UserSAdminClient(deptid, client_id)
            ddlSelectNewAdmin.DataTextField = "disname"
            ddlSelectNewAdmin.DataValueField = "UserId"
            ddlSelectNewAdmin.DataBind()
            ddlSelectNewAdmin.Items.Insert(0, "--Select--")
            Exit Sub
        End If

        If ddlDepartmentUser.SelectedIndex > 0 Then
            deptid = ddlDepartmentUser.SelectedValue
            ddlSelectNewAdmin.DataSource = uspan.bind_UserSAdminDepartment(deptid)
            ddlSelectNewAdmin.DataTextField = "disname"
            ddlSelectNewAdmin.DataValueField = "UserId"
            ddlSelectNewAdmin.DataBind()
            ddlSelectNewAdmin.Items.Insert(0, "--Select--")

        End If


    End Sub
    ''' <summary>
    ''' 'populating ddlSelectNewAdmin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click
        BindNewAdmin()
    End Sub

    ''' <summary>
    ''' to make admin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnMakeAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnMakeAdmin.Click
        Dim adminid As String
        Dim adminname As String
        Dim usertype As String

        adminid = ddlSelectNewAdmin.SelectedValue
        usertype = 2.ToString()

        If ddlDepartmentUser.SelectedIndex = 0 Then
            ShowConfirm("Select Department for User")
            Exit Sub
        End If
        If ddlSelectNewAdmin.SelectedIndex = -1 Then
            ShowConfirm("Select User to Make Admin")
            Exit Sub
        End If

        If tbxComment.InnerText = "" Then
            ShowConfirm("Enter comment")
            Exit Sub
        End If

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department for Admin Span")
            Exit Sub
        End If

        'If (lstAdmin.Items.Count = 0) Or (lstAdmin.SelectedIndex = -1) Then
        '    ShowConfirm("Select user to make admin")
        '    Exit Sub
        'End If

        adminname = ddlSelectNewAdmin.SelectedItem.Text
        Dim aarr As String() = adminname.Split("(")
        adminname = aarr(0)
        getId()
        con = New SqlConnection(str)
        con.Open()
        Dim cmdexist As New SqlCommand("select  DeptId,ClientId,LOBId,AdminId,Adminname from masteradmin where DeptId='" & deptid1 & "' and Clientid= '" & client_id1 & "' and LOBID='" & lobid1 & "' and adminid='" & adminid & "'", con)
        Dim rdr As SqlDataReader = cmdexist.ExecuteReader()

        If rdr.Read() Then
            panConfirm.Visible = True
            con.Close()
        Else
            rdr.Close()
            tableobj.Insert_NewAdmin(adminid, adminname, usertype, deptid1, client_id1, lobid1, tbxComment.InnerText)
            ShowConfirm("Admin created successfully ")
            'Dim cmdexist1 As New SqlCommand("select  DeptId,ClientId,LOBId,AdminId,Adminname from masteradmin where DeptId='" & deptid1 & "' and Clientid= '" & client_id1 & "' and LOBID='" & lobid1 & "' and adminid='" & adminid & "'", con)
            'Dim rdr2 As SqlDataReader = cmdexist1.ExecuteReader()
            'If rdr2.Read() Then

            'Else
            'uspan.Set_AdminDefaultRights(adminid, loggedId)
            'End If

            getId()
            bindlist()    'method to bind admin list at selected span
            'rdr2.Close()
            'con.Close()

            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" & adminid & "' and Action='Make Admin'", con)
            'con.Open()
            'cmm.ExecuteNonQuery()
            'con.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'ClearAll()
        End If
        Dim ds As DataSet = New DataSet
        Dim code, producttype, databasetype, database As String
        ' insert the rights for admin
        cmdnew = New SqlCommand("select ProductCode,ProductType,DatabaseType,Database1 from InternetProductDemo where UserID='" + Session("userid") + "' ", con)
        danew = New SqlDataAdapter(cmdnew)
        danew.Fill(ds)
        code = ds.Tables(0).Rows(0)("ProductCode").ToString()
        producttype = ds.Tables(0).Rows(0)("ProductType").ToString()
        databasetype = ds.Tables(0).Rows(0)("DatabaseType").ToString()
        database = ds.Tables(0).Rows(0)("Database1").ToString()
        Dim dt As String
        Dim i As Integer
        Dim menu As Integer
        Dim cmd6 As New SqlCommand("select Rights from ProductMaster where ProductCode='" + code + "' and DatabaseType='" + databasetype + "' and UserType='" + producttype + "'", con)
        Dim rdrights As SqlDataReader
        rdrights = cmd6.ExecuteReader
        If rdrights.Read Then
            dt = rdrights("Rights")
        End If
        Dim arrhead() As String = dt.Split(",")
        connection.Close()
        connection.Open()
        For i = 0 To arrhead.Length - 1
            If (arrhead(i) <> 17) Then
                Dim cmdins1 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                cmdins1.CommandType = CommandType.StoredProcedure
                With cmdins1.Parameters
                    .AddWithValue("@LOB", "0")
                    .AddWithValue("@MenuId", arrhead(i))
                    .AddWithValue("@UserType", "2")
                    .AddWithValue("@Access", "")
                    .AddWithValue("@Currdate", System.DateTime.Now)
                    .AddWithValue("@AssignBy", Session("userid"))
                    .AddWithValue("@parentid", "0")
                    .AddWithValue("@userid", adminid)
                End With
                cmdins1.ExecuteNonQuery()
                cmdins1.Dispose()
                If (arrhead(i).Equals("31") And database.Equals("Excel")) Then
                    Dim arrhead2() As Integer = {32, 33, 34}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "2")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", Session("userid"))
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", adminid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("Oracle")) Then
                    Dim arrhead2() As Integer = {34, 159}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "2")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", Session("userid"))
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", adminid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                ElseIf (arrhead(i).Equals("31") And database.Equals("MS-SQL")) Then
                    Dim arrhead2() As Integer = {34, 158}
                    For Each a In arrhead2
                        menu = a.ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "2")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", Session("userid"))
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", adminid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                Else
                    Dim datacmd As New SqlCommand("select * from nlvl_menu where ParentID='" + arrhead(i) + "'", connection)
                    danew = New SqlDataAdapter(datacmd)
                    ds = New DataSet()
                    danew.Fill(ds, "abc")
                    Dim rowentry As DataRow
                    For Each rowentry In ds.Tables("abc").Rows
                        menu = rowentry("MenuId").ToString()
                        Dim cmdins2 As New SqlCommand("insert_NLVL_MENU_Rights", connection)
                        cmdins2.CommandType = CommandType.StoredProcedure
                        With cmdins2.Parameters
                            .AddWithValue("@LOB", "0")
                            .AddWithValue("@MenuId", menu)
                            .AddWithValue("@UserType", "2")
                            .AddWithValue("@Access", "")
                            .AddWithValue("@Currdate", System.DateTime.Now)
                            .AddWithValue("@AssignBy", Session("userid"))
                            .AddWithValue("@parentid", arrhead(i))
                            .AddWithValue("@userid", adminid)
                        End With
                        cmdins2.ExecuteNonQuery()
                    Next
                End If
            End If
        Next

    End Sub
    ''' <summary>
    ''' 'Method to getId of the user that  has been selected to make admin
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub getId()
        If ddlLob.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 And ddlDepartment.SelectedIndex > 0 Then
            deptid1 = ddlDepartment.SelectedValue
            client_id1 = ddlClient.SelectedValue
            lobid1 = ddlLob.SelectedValue
            Exit Sub

        End If
        If ddlDepartment.SelectedIndex > 0 And ddlClient.SelectedIndex > 0 Then
            deptid1 = ddlDepartment.SelectedValue
            client_id1 = ddlClient.SelectedValue
            lobid1 = 0.ToString()
            Exit Sub

        End If
        If ddlDepartment.SelectedIndex > 0 Then
            deptid1 = ddlDepartment.SelectedValue
            client_id1 = 0.ToString()
            lobid1 = 0.ToString()

        End If
    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        panConfirm.Visible = False
        ddlSelectNewAdmin.Items.Clear()
        tbxComment.InnerText = ""
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()

    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlSelectNewAdmin.Items.Clear()
        tbxComment.InnerText = ""
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlDepartment.SelectedIndex = 0
        ddlDepartmentUser.SelectedIndex = 0
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

    Protected Sub DCLUserControl2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl2.Load

        If ddlDepartment.SelectedIndex = 0 Then
            'ddlDepartmentUser.SelectedIndex = 0
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            'ddlSelectNewAdmin.SelectedIndex = 0

        End If
    End Sub
    ''' <summary>
    '''   'method to bind admin list at selected span
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShowAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowAdmin.Click
        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department to get admins")
            Exit Sub
        End If
        getId()
        bindlist()
    End Sub
    ''' <summary>
    ''' bind all admins in list
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub bindlist()
        lstAdmin.DataSource = uspan.BindAdmins(deptid1, client_id1, lobid1)
        lstAdmin.DataTextField = "disname"
        lstAdmin.DataValueField = "adminid"
        lstAdmin.DataBind()
    End Sub
    ''' <summary>
    ''' make visible panel to delete admin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnDeleteAdmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAdmin.Click

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department to get admins")
            Exit Sub
        End If
        If lstAdmin.SelectedIndex = -1 Then
            ShowConfirm("Select admin from list to delete")
            Exit Sub
        End If
        If lstAdmin.Items.Count = 0 Then
            ShowConfirm("Admin does not esist on selected span")
            Exit Sub
        End If

        Paneldelete.Visible = True
    End Sub
    ''' <summary>
    ''' display detail of selected admin and delete admin
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim adminid(20) As String
        Dim i As Integer

        'getting value of selected admins
        For i = 0 To lstAdmin.Items.Count - 1
            If lstAdmin.Items(i).Selected = True Then
                adminid(i) = lstAdmin.Items(i).Value
            End If
        Next
        getId()

        For i = 0 To adminid.Length - 1
            'method to delete admins
            If adminid(i) <> "" Then
                uspan.DeleteAdmin(deptid1, client_id1, lobid1, adminid(i))
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                'Dim cmm As New SqlCommand("insert into Account_utype select MAX(Autoid)," + Session("usertype") + " from logaccountmaster where EntityName='" & adminid(i) & "' and Action='Delete Admin'", con)
                'con.Open()
                'cmm.ExecuteNonQuery()
                'con.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
            End If
            

        Next
        ShowConfirm("Admin deleted successfully")
        Paneldelete.Visible = False
        bindlist()    'method to bind admin list at selected span


    End Sub
    ''' <summary>
    ''' make panle invisible
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click

        Paneldelete.Visible = False
        clearAfterDelete()
        Exit Sub
    End Sub
    ''' <summary>
    ''' clear control after delete
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub clearAfterDelete()
        ddlDepartment.SelectedIndex = 0
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        lstAdmin.Items.Clear()
    End Sub

    ''' <summary>
    '''    'Method called to bind user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSelectNewAdmin_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectNewAdmin.SelectedIndexChanged

        BindUserSpan()    
        If gvUserExistence.Rows.Count = 0 Then
            ShowConfirm("User is not admin of any span")
            Exit Sub
        End If

    End Sub
    ''' <summary>
    ''' applying paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvUserExistence_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUserExistence.PageIndexChanging

        If gvUserExistence.PageIndex < gvUserExistence.PageCount And gvUserExistence.PageIndex >= 0 Then

            gvUserExistence.PageIndex = e.NewPageIndex

            BindUserSpan()      'Method called to bind user
        End If

    End Sub
    ''' <summary>
    ''' bind users span
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub BindUserSpan()
        Dim useridshow As String
        useridshow = ddlSelectNewAdmin.SelectedValue
        gvUserExistence.DataSource = uspan.useradminspan(useridshow)
        gvUserExistence.DataBind()

    End Sub
End Class
