Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ChangeReportOwnership
    Inherits System.Web.UI.Page


    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim conn As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartmentReport As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClientReport As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLobReport As DropDownList
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

        ddlDepartmentReport = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClientReport = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLobReport = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load

            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "DeptID"
            ddlDepartmentUser.DataSource = fun.bind_Dept()
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "--Select--")

            ddlDepartmentReport.DataTextField = "DepartmentName"
            ddlDepartmentReport.DataValueField = "DeptID"
            ddlDepartmentReport.DataSource = fun.bind_Dept()
            ddlDepartmentReport.DataBind()
            ddlDepartmentReport.Items.Insert(0, "--Select--")
        End If

        Me.Form.DefaultButton = Me.btnChange.UniqueID

    End Sub
    ''' <summary>
    ''' binding users according to selected span
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
            'ddlNewOwner.DataTextField = "DepartmentName"
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "UserId"
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")

        End If
    End Sub
    ''' <summary>
    ''' ''' binding reports according to selected span
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindReport()
        If ddlLobReport.SelectedIndex > 0 And ddlClientReport.SelectedIndex > 0 And ddlDepartmentReport.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLobReport.SelectedValue)
            client_id = Convert.ToInt32(ddlClientReport.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentReport.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectReport.DataSource = uspan.reportselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If

            If UserType = "User" Then
                ddlSelectReport.DataSource = uspan.bind_lobrepUser(deptid, client_id, lobid)
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If
            Exit Sub
        End If

        If ddlClientReport.SelectedIndex > 0 And ddlDepartmentReport.SelectedIndex > 0 Then
            client_id = Convert.ToInt32(ddlClientReport.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentReport.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectReport.DataSource = uspan.reportselectadminspan(loggedId, deptid, client_id, "0")
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If

            If UserType = "User" Then
                ddlSelectReport.DataSource = uspan.bind_clientrepUser(deptid, client_id)
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If
            Exit Sub
        End If


        If ddlDepartmentReport.SelectedIndex > 0 Then
            deptid = ddlDepartmentReport.SelectedValue

            If UserType = "Admin" Then
                ddlSelectReport.DataSource = uspan.reportselectadminspan(loggedId, deptid, "0", "0")
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If

            If UserType = "User" Then
                ddlSelectReport.DataSource = uspan.bind_departmentrepUser(deptid)
                ddlSelectReport.DataTextField = "QueryName"
                ddlSelectReport.DataValueField = "RecordId"
                ddlSelectReport.DataBind()
                ddlSelectReport.Items.Insert(0, "--Select--")
            End If
        End If
        Dim i As Integer = ddlDepartmentReport.SelectedIndex
    End Sub

    ''' <summary>
    ''' ''' binding users according to selected span
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click
        BindUser() 'method called to bind users

    End Sub
    ''' <summary>
    ''' get the owner of selected report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSelectReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectReport.SelectedIndexChanged
        Dim selectedreport As String
        Dim ds As New DataSet
        If ddlSelectReport.SelectedIndex <> -1 And ddlSelectReport.SelectedIndex <> 0 Then
            selectedreport = ddlSelectReport.SelectedValue
            ds = tableobj.Get_ReportOwner(selectedreport)
            If ds.Tables(0).Rows.Count = 0 Then
                ShowConfirm("This report has been deleted")
                Exit Sub

            End If
            tbxPreviousOwner.Text = ds.Tables(0).Rows(0)("UserName").ToString()
            hdUserId.Value = ds.Tables(0).Rows(0)("UserId").ToString()
        Else
            tbxPreviousOwner.Text = ""
            hdUserId.Value = ""
        End If
    End Sub
    ''' <summary>
    ''' change the owner of report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click


        'validating values
        '-------------------------------------------------------------------

        If ddlDepartmentReport.SelectedIndex = 0 Then
            ShowConfirm("Select department to choose report")
            Exit Sub
        End If
        If (ddlSelectReport.SelectedIndex = -1) Or (ddlSelectReport.SelectedIndex = 0) Then
            ShowConfirm("Select report")
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

            Dim ReportId As String = ddlSelectReport.SelectedValue
            Dim Reportname As String = ddlSelectReport.SelectedItem.Text
            Dim newOwnerId As String = ddlNewOwner.SelectedValue
            '*************change*************


            Dim cmdins2 = New SqlCommand("TracktableReportchange", conn)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters


                .AddWithValue("@recordid", ReportId)

                .AddWithValue("@actionby", Session("userid"))
                .AddWithValue("@action", "Change Owner")
                .AddWithValue("@date", System.DateTime.Now)

                .AddWithValue("@entity", "Report")
                .AddWithValue("@entityname", Reportname)

                .AddWithValue("@deptid", ddlDepartmentReport.SelectedValue)
                Dim clta As Integer
                Dim lobt As Integer
                
                If IsNumeric(ddlClientReport.SelectedValue) Then
                    clta = ddlClientReport.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLobReport.SelectedValue) Then
                    lobt = ddlLobReport.SelectedValue
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
            '*************change*************


            Dim oldowner = hdUserId.Value
            tableobj.Update_ReportOwnership(ReportId, Reportname, newOwnerId, oldowner)
            ShowConfirm("Report ownership changed successfully")
            ClearAll()


        Else
            ShowConfirm("You are not authorised")
        End If


    End Sub
    ''' <summary>
    ''' ''' binding reports according to selected span
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetReport.Click
        BindReport() 'binding reports on button click

    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlClientReport.Items.Clear()
        ddlLobReport.Items.Clear()
        ddlNewOwner.Items.Clear()
        ddlSelectReport.Items.Clear()
        tbxPreviousOwner.Text = ""
        ddlDepartmentUser.SelectedIndex = 0
        ddlDepartmentReport.SelectedIndex = 0
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

End Class
