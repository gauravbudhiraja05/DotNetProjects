Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ChangeAnalysisOwnership
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim fun As New Functions
    Dim ddlDepartmentUser As DropDownList
    Dim ddlDepartmentAnalysis As DropDownList
    Dim ddlClientUser As DropDownList
    Dim ddlClientAnalysis As DropDownList
    Dim ddlLobUser As DropDownList
    Dim ddlLobAnalysis As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim tableobj As New TableRight
    Dim UserId As String
    Dim loggedId As String
    Dim UserType As String
    Dim uspan As New UserSpan
    Dim objanalysis As New AnalysisRights


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

        ddlDepartmentAnalysis = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClientAnalysis = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLobAnalysis = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load

            ddlDepartmentUser.DataTextField = "DepartmentName"
            ddlDepartmentUser.DataValueField = "DeptID"
            ddlDepartmentUser.DataSource = fun.bind_Dept()
            ddlDepartmentUser.DataBind()
            ddlDepartmentUser.Items.Insert(0, "--Select--")

            ddlDepartmentAnalysis.DataTextField = "DepartmentName"
            ddlDepartmentAnalysis.DataValueField = "DeptID"
            ddlDepartmentAnalysis.DataSource = fun.bind_Dept()
            ddlDepartmentAnalysis.DataBind()
            ddlDepartmentAnalysis.Items.Insert(0, "--Select--")
        End If

        Me.Form.DefaultButton = Me.btnChange.UniqueID

    End Sub
    ''' <summary>
    '''  'method called to bind tables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetAnalysis.Click

        BindAnalysis()


    End Sub
    ''' <summary>
    '''  'method called to bind users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGetUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetUser.Click
        BindUser()


    End Sub

    Protected Sub ddlSelectAnalysis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelectAnalysis.SelectedIndexChanged
        Dim selectedanalysis As String
        Dim ds As New DataSet
        If ddlSelectAnalysis.SelectedIndex <> -1 And ddlSelectAnalysis.SelectedIndex <> 0 Then
            selectedanalysis = ddlSelectAnalysis.SelectedValue
            ds = objanalysis.Get_AnalysisOwner(selectedanalysis)
            tbxPreviousOwner.Text = ds.Tables(0).Rows(0)("UserName").ToString()
            hdUserId.Value = ds.Tables(0).Rows(0)("UserId").ToString()
        Else
            tbxPreviousOwner.Text = ""
            hdUserId.Value = ""
        End If

    End Sub
 
    Protected Sub btnChange_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChange.Click

        '-------------------------------------------------------------------

        If ddlDepartmentAnalysis.SelectedIndex = 0 Then
            ShowConfirm("Select department to choose analysis")
            Exit Sub
        End If
        If (ddlSelectAnalysis.SelectedIndex = -1) Or (ddlSelectAnalysis.SelectedIndex = 0) Then
            ShowConfirm("Select analysis")
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
            Dim RecordId As Integer = ddlSelectAnalysis.SelectedIndex
            Dim reportname As String = ddlSelectAnalysis.SelectedItem.Text
            Dim newOwnerId As String = ddlNewOwner.SelectedValue
            '*************change*************
            Dim con As New SqlConnection(str)

            Dim cmdins2 = New SqlCommand("TracktableReportchange", con)
            cmdins2.CommandType = CommandType.StoredProcedure
            With cmdins2.Parameters


                .AddWithValue("@recordid", RecordId)

                .AddWithValue("@actionby", Session("userid"))
                .AddWithValue("@action", "Change Owner")
                .AddWithValue("@date", System.DateTime.Now)

                .AddWithValue("@entity", "Analysis")
                .AddWithValue("@entityname", reportname)

                .AddWithValue("@deptid", ddlDepartmentAnalysis.SelectedValue)
                Dim clta As Integer
                Dim lobt As Integer

                If IsNumeric(ddlClientAnalysis.SelectedValue) Then
                    clta = ddlClientAnalysis.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLobAnalysis.SelectedValue) Then
                    lobt = ddlLobAnalysis.SelectedValue
                Else
                    lobt = 0
                End If
                .AddWithValue("@clientid", clta)
                .AddWithValue("@lobid", lobt)
                .AddWithValue("@assignto", newOwnerId)
                .AddWithValue("@type", Session("usertype"))
            End With
            con.Open()
            cmdins2.ExecuteNonQuery()
            con.Close()
            cmdins2.Dispose()
            '*************change*************
            Dim oldowner = hdUserId.Value
            objanalysis.Update_AnalysisOwnership(reportname, newOwnerId, oldowner)
            ShowConfirm("Analysis ownership changed successfully")
            ClearAll()

        Else
            ShowConfirm("You are not authorised")
        End If

    End Sub
    ''' <summary>
    ''' ' Method to bind users according to selection
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
            ddlNewOwner.DataTextField = "disname"
            ddlNewOwner.DataValueField = "UserId"
            ddlNewOwner.DataBind()
            ddlNewOwner.Items.Insert(0, "--Select--")

        End If
    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClientUser.Items.Clear()
        ddlLobUser.Items.Clear()
        ddlClientAnalysis.Items.Clear()
        ddlLobAnalysis.Items.Clear()
        ddlNewOwner.Items.Clear()
        ddlSelectAnalysis.Items.Clear()
        tbxPreviousOwner.Text = ""
        ddlDepartmentUser.SelectedIndex = 0
        ddlDepartmentAnalysis.SelectedIndex = 0
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
    ''' 'Method to bind analysis to gridview that show analysis list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindAnalysis()
        
        If ddlLobAnalysis.SelectedIndex > 0 Then
            lobid = Convert.ToInt32(ddlLobAnalysis.SelectedValue)
            client_id = Convert.ToInt32(ddlClientanalysis.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentAnalysis.SelectedValue)

            If UserType = "Admin" Then
                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If

            If UserType = "User" Then
                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = objanalysis.bind_lobAnalysisUser(deptid, client_id, lobid)
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlClientAnalysis.SelectedIndex > 0 Then
            client_id = Convert.ToInt32(ddlClientAnalysis.SelectedValue)
            deptid = Convert.ToInt32(ddlDepartmentAnalysis.SelectedValue)

            If UserType = "Admin" Then

                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, deptid, client_id, "0")
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If

            If UserType = "User" Then

                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = objanalysis.bind_clientAnalysisUser(deptid, client_id)
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlDepartmentAnalysis.SelectedIndex <> 0 Then
            deptid = ddlDepartmentAnalysis.SelectedValue

            If UserType = "Admin" Then

                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, deptid, "0", "0")
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
            End If

            If UserType = "User" Then

                ddlSelectAnalysis.DataTextField = "analysisname"
                ddlSelectAnalysis.DataValueField = "analysisname"
                ddlSelectAnalysis.DataSource = objanalysis.bind_DeparmentAnalysisUser(deptid)
                ddlSelectAnalysis.DataBind()
                ddlSelectAnalysis.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub
End Class
