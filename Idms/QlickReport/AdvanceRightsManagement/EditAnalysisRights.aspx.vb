Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_EditAnalysisRights
    Inherits System.Web.UI.Page

    Dim str As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(str)
    Dim ddlDepartment As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlLob As DropDownList
    Dim fun As New Functions
    Dim DeptId As String
    Dim Client_Id As String
    Dim LobId As String
    Dim objtable As New TableRight
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

        ddlDepartment = CType(DCLHUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLHUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLHUserControl1.FindControl("ddlLob"), DropDownList)
        If Me.IsPostBack = False Then

            'binding ddlDepartment on page load

            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

        End If
       
    End Sub
    ''' <summary>
    ''' bind the analysis according to selected user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoUser_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoUser.CheckedChanged

        gvAnalysisRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            'ShowConfirm("Select Department")
            'Exit Sub
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
            ddlUserAnalysis.DataTextField = "disname"
            ddlUserAnalysis.DataValueField = "userid"
            ddlUserAnalysis.DataSource = ds1
            ddlUserAnalysis.DataBind()
            ddlUserAnalysis.Items.Insert(0, "--Select--")
            Exit Sub
        End If


        If ddlLob.SelectedIndex > 0 Then
            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserAnalysis.Items.Clear()
            ddlUserAnalysis.DataTextField = "disname"
            ddlUserAnalysis.DataValueField = "UserId"
            ddlUserAnalysis.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, LobId)
            ddlUserAnalysis.DataBind()
            ddlUserAnalysis.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlClient.SelectedIndex > 0 Then

            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserAnalysis.Items.Clear()
            ddlUserAnalysis.DataTextField = "disname"
            ddlUserAnalysis.DataValueField = "UserId"
            ddlUserAnalysis.DataSource = uspan.userselectadminspan(loggedId, DeptId, Client_Id, "0")
            ddlUserAnalysis.DataBind()
            ddlUserAnalysis.Items.Insert(0, "--Select--")
            Exit Sub

        End If

        If ddlDepartment.SelectedIndex <> 0 Then
            ddlUserAnalysis.Items.Clear()
            DeptId = ddlDepartment.SelectedValue
            ddlUserAnalysis.DataTextField = "disname"
            ddlUserAnalysis.DataValueField = "UserId"
            ddlUserAnalysis.DataSource = uspan.userselectadminspan(loggedId, DeptId, "0", "0")
            ddlUserAnalysis.DataBind()
            ddlUserAnalysis.Items.Insert(0, "--Select--")

        End If

    End Sub
    ''' <summary>
    ''' bind the analysis according to selected user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoAnalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAnalysis.CheckedChanged

        gvAnalysisRight.Visible = False

        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select Department")
            Exit Sub
        End If


        If ddlLob.SelectedIndex > 0 Then

            LobId = Convert.ToInt32(ddlLob.SelectedValue)
            Client_Id = Convert.ToInt32(ddlClient.SelectedValue)
            DeptId = Convert.ToInt32(ddlDepartment.SelectedValue)
            ddlUserAnalysis.Items.Clear()

            If UserType = "Admin" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, DeptId, Client_Id, LobId)
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If

            If UserType = "User" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisForuser(loggedId, DeptId, Client_Id, LobId)
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlClient.SelectedIndex > 0 Then

            Client_Id = ddlClient.SelectedValue.ToString()
            DeptId = ddlDepartment.SelectedValue.ToString()
            ddlUserAnalysis.Items.Clear()
            If UserType = "User" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisForuser(loggedId, DeptId, Client_Id, "0")
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If
            If UserType = "Admin" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, DeptId, Client_Id, "0")
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        End If

        If ddlDepartment.SelectedIndex <> 0 Then

            DeptId = ddlDepartment.SelectedValue
            ddlUserAnalysis.Items.Clear()
            If UserType = "User" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisForuser(loggedId, DeptId, "0", "0")
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
            End If
            If UserType = "Admin" Then
                ddlUserAnalysis.DataTextField = "analysisname"
                ddlUserAnalysis.DataValueField = "analysisname"
                ddlUserAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, DeptId, "0", "0")
                ddlUserAnalysis.DataBind()
                ddlUserAnalysis.Items.Insert(0, "--Select--")
            End If
        End If

    End Sub

   
    Protected Sub ddlUserAnalysis_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUserAnalysis.SelectedIndexChanged

        gvAnalysisRight.Visible = False

        If ddlUserAnalysis.SelectedIndex = 0 Then
            ShowConfirm("Select user or Analysis")
            Exit Sub
        Else
            gvAnalysisRight.Visible = True
            BindGridView()
            If gvAnalysisRight.Rows.Count = 0 Then
                ShowConfirm("No record found.........")
            End If
        End If

       
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
    ''' cancle row editing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysisRight_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs) Handles gvAnalysisRight.RowCancelingEdit

        gvAnalysisRight.EditIndex = -1
        'bind grid view
        BindGridView()

    End Sub
    ''' <summary>
    ''' delete row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub gvAnalysisRight_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles gvAnalysisRight.RowDeleting

        'Delete functionality is applied 
        con = New SqlConnection(str)

        Dim reportname As String = gvAnalysisRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvAnalysisRight.Rows(e.RowIndex).Cells(1).Text
        Dim cmduse = New SqlCommand("select analysisname from IDMS_AnalysisRights where analysisname='" + reportname + "'", con)
        Dim tableuse As String = ""

        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()

        Dim analysisowner As String

        con = New SqlConnection(str)
        con.Open()
        Dim com As New SqlCommand("select analysisname,SavedBy from savedanalysis where analysisname ='" & reportname & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            analysisowner = rdr("savedBy").ToString()
            If (loggedId = analysisowner) Or UserType = "Admin" Then
                objanalysis.Delete_AnalysisRights(reportname, userid)
                BindGridView()
                con.Close()
                '*********************change****************************
                Dim clta As Integer
                Dim lobt As Integer
                If IsNumeric(ddlClient.SelectedValue) Then
                    clta = ddlClient.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLob.SelectedValue) Then
                    lobt = ddlLob.SelectedValue
                Else
                    lobt = 0
                End If
                Dim dept As String = ""

                If ddlDepartment.SelectedValue = "--Select--" Then
                    Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + userid + "'", con)
                    Dim datadsset As New DataSet
                    com2.Fill(datadsset)
                    If datadsset.Tables(0).Rows.Count > 0 Then
                        dept = datadsset.Tables(0).Rows(0)(0).ToString
                        clta = datadsset.Tables(0).Rows(0)(1)
                        lobt = datadsset.Tables(0).Rows(0)(2)
                    Else
                        dept = "0"
                        clta = 0
                        lobt = 0
                    End If
                Else
                    dept = ddlDepartment.SelectedValue
                End If
                Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Delete Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "Analysis")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)
                    'Dim clta As Integer
                    'Dim lobt As Integer

                    'If IsNumeric(ddlClient.SelectedValue) Then
                    '    clta = ddlClient.SelectedValue
                    'Else
                    '    clta = 0
                    'End If
                    'If IsNumeric(ddlLob.SelectedValue) Then
                    '    lobt = ddlLob.SelectedValue
                    'Else
                    '    lobt = 0
                    'End If
                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", "All")
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Close()
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                ShowConfirm("Analysis Rights Deleted Successfully")
                '*************change*************
            Else
                ShowConfirm("You are not permitted to delete rights")
            End If
        End If

    End Sub
    ''' <summary>
    ''' edit row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysisRight_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvAnalysisRight.RowEditing
      


        gvAnalysisRight.EditIndex = e.NewEditIndex
        'bind grid view
        BindGridView()
        Dim asaas As Integer = e.NewEditIndex
        CType(gvAnalysisRight.Rows(asaas).Cells(3).FindControl("chkView"), CheckBox).Enabled = True
        CType(gvAnalysisRight.Rows(asaas).Cells(3).FindControl("chkDelete"), CheckBox).Enabled = True






    End Sub
    ''' <summary>
    ''' Analysis rights Update code
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysisRight_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs) Handles gvAnalysisRight.RowUpdating


        Dim view As Boolean = False
        Dim delete As Boolean = False
       
        con = New SqlConnection(str)
        Dim Reportname As String = gvAnalysisRight.Rows(e.RowIndex).Cells(0).Text
        Dim userid As String = gvAnalysisRight.Rows(e.RowIndex).Cells(1).Text
        Dim cmduse = New SqlCommand("select analysisname from IDMS_AnalysisRights where analysisname='" + Reportname + "'", con)
        Dim tableuse As String = ""

        con.Open()
        tableuse = cmduse.executescalar()
        con.Close()

        Dim chkview As CheckBox = CType(gvAnalysisRight.Rows(e.RowIndex).FindControl("chkView"), CheckBox)
        Dim all As String = ""
        If chkview.Checked = True Then
            view = True
            all = "View"
        End If
        
        Dim chkdelete As CheckBox = CType(gvAnalysisRight.Rows(e.RowIndex).FindControl("chkDelete"), CheckBox)

        If chkdelete.Checked = True Then
            delete = True
            If all = "" Then
                all = "Delete"
            Else
                all = all & "," & "Delete"
            End If
        End If

        Dim analysisowner As String
        con = New SqlConnection(str)
        con.Open()
        Dim com As New SqlCommand("select analysisname,SavedBy from savedanalysis where analysisname='" & Reportname & "'", con)
        Dim rdr As SqlDataReader = com.ExecuteReader()
        If rdr.Read() Then
            analysisowner = rdr("SavedBy").ToString()
            If (loggedId = analysisowner) Or UserType = "Admin" Then
                '*********************change****************************
                rdr.Close()


                Dim clta As Integer
                Dim lobt As Integer
                If IsNumeric(ddlClient.SelectedValue) Then
                    clta = ddlClient.SelectedValue
                Else
                    clta = 0
                End If
                If IsNumeric(ddlLob.SelectedValue) Then
                    lobt = ddlLob.SelectedValue
                Else
                    lobt = 0
                End If
                Dim dept As String = ""

                If ddlDepartment.SelectedValue = "--Select--" Then
                    Dim com2 As New SqlDataAdapter("select deptid,clientid,lobid from registration where userid='" + userid + "'", con)
                    Dim datadsset As New DataSet
                    com2.Fill(datadsset)
                    If datadsset.Tables(0).Rows.Count > 0 Then
                        dept = datadsset.Tables(0).Rows(0)(0).ToString
                        clta = datadsset.Tables(0).Rows(0)(1)
                        lobt = datadsset.Tables(0).Rows(0)(2)
                    Else
                        dept = "0"
                        clta = 0
                        lobt = 0
                    End If
                Else
                    dept = ddlDepartment.SelectedValue
                End If
                Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                cmdins2.CommandType = CommandType.StoredProcedure
                With cmdins2.Parameters


                    .AddWithValue("@actionBY", loggedId)
                    .AddWithValue("@Action", "Edit Rights")
                    .AddWithValue("@Date", System.DateTime.Now)
                    .AddWithValue("@AssignTo", userid)
                    .AddWithValue("@Entity", "Analysis")
                    .AddWithValue("@EntityName", tableuse)

                    .AddWithValue("@DeptId", dept)
                   
                    .AddWithValue("@ClientId", clta)
                    .AddWithValue("@LOBId", lobt)
                    .AddWithValue("@All", all)
                    .AddWithValue("@type", Session("usertype"))
                End With
                con.Close()
                con.Open()
                cmdins2.ExecuteNonQuery()
                con.Close()
                cmdins2.Dispose()
                ShowConfirm("Analysis Rights Updated Successfully")
                '*************change*************


                objanalysis.Update_AnalysisRight(userid, Reportname, view, delete, loggedId)

                gvAnalysisRight.EditIndex = -1
                ''bind grid view
                BindGridView()
                con.Close()
            Else
                ShowConfirm("You are not permitted to edit rights")
            End If
        End If

    End Sub

    Protected Sub DCLHUserControl1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLHUserControl1.Load
        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()
            'ddlUserAnalysis.Items.Clear()
            'gvAnalysisRight.Visible = False

        End If
    End Sub

    Protected Sub BindGridView()
        If (rdoUser.Checked) Then

            BindDataByUser()

        End If

        If (rdoAnalysis.Checked) Then

            BindDataByAnalysis()
        End If

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to user selected
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByUser()


        Dim userid As String
        userid = ddlUserAnalysis.SelectedValue
        gvAnalysisRight.DataSource = objanalysis.bind_AnalysisRightsByUser(userid)
        gvAnalysisRight.Columns(1).Visible = True
        gvAnalysisRight.DataBind()
        ' gvAnalysisRight.Columns(2).Visible = False

    End Sub
    ''' <summary>
    ''' 'Method  that bind data according to Table selected 
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindDataByAnalysis()
        Dim reportname As String
        reportname = ddlUserAnalysis.SelectedValue
        gvAnalysisRight.DataSource = objanalysis.bind_AnalysisRightsByAnalysis(reportname)
        gvAnalysisRight.Columns(1).Visible = True
        gvAnalysisRight.DataBind()
        ' gvAnalysisRight.Columns(1).Visible = False
    End Sub
    ''' <summary>
    ''' paging
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysisRight_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAnalysisRight.PageIndexChanging
        If gvAnalysisRight.PageIndex < gvAnalysisRight.PageCount And gvAnalysisRight.PageIndex >= 0 Then
            gvAnalysisRight.PageIndex = e.NewPageIndex
            BindGridView()
        End If


    End Sub
End Class
