Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_AssignAnalysisRights
    Inherits System.Web.UI.Page
    Dim str As String = AppSettings("ConnectionString")
    Dim str1 As String = AppSettings("ConnectionString1")
    Dim con As New SqlConnection(Str)
    Dim uspan As New UserSpan
    Dim fun As New Functions
    Dim ddlDepartment As DropDownList
    Dim ddlDepartmentAnalysis As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlClientanalysis As DropDownList
    Dim ddlLob As DropDownList
    Dim ddlLobAnalysis As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim loggedId As String
    Dim recordId(20) As String
    Dim UserType As String
    Dim objtable As New TableRight
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

        ddlDepartment = CType(DCLUserControl3.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLUserControl3.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLUserControl3.FindControl("ddlLob"), DropDownList)

        ddlDepartmentAnalysis = CType(DCLUserControl4.FindControl("ddlDepartment"), DropDownList)
        ddlClientanalysis = CType(DCLUserControl4.FindControl("ddlClient"), DropDownList)
        ddlLobAnalysis = CType(DCLUserControl4.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load


            ddlDepartment.DataTextField = "DepartmentName"
            ddlDepartment.DataValueField = "DeptID"
            ddlDepartment.DataSource = fun.bind_Dept()
            ddlDepartment.DataBind()
            ddlDepartment.Items.Insert(0, "--Select--")

            ddlDepartmentAnalysis.DataTextField = "DepartmentName"
            ddlDepartmentAnalysis.DataValueField = "DeptID"
            ddlDepartmentAnalysis.DataSource = fun.bind_Dept()
            ddlDepartmentAnalysis.DataBind()
            ddlDepartmentAnalysis.Items.Insert(0, "--Select--")
        End If

        Me.Form.DefaultButton = Me.btnAssignAnalysisRight.UniqueID


    End Sub

    ''' <summary>
    ''' assigning the rights of analysis to selected users
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnAssignAnalysisRight_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAssignAnalysisRight.Click

        Dim i As Integer
        Dim j As Integer
        Dim UserId(20) As String
        Dim Reportname(20) As String
        Dim view(20) As String
        Dim isUserSelected As Boolean
        Dim delete(20) As String
        Dim isAnalysisSelected As Boolean
        Dim selection As CheckBox
        Dim viewRight As CheckBox
        Dim deleteRight As CheckBox
        Dim assignedby As String
        Static count As Integer


        'checking department for user is selected or not
        If (ddlDepartment.SelectedIndex = 0) Then
            ShowConfirm("Please select department to choose user ")
            Exit Sub
        End If
        If (ddlDepartmentAnalysis.SelectedIndex = 0) Then
            ShowConfirm("Please select department to choose Analysis ")
            Exit Sub
        End If

        'storing userid informations in variables
        isUserSelected = False
        For i = 0 To gvUsers.Rows.Count - 1
            selection = CType(gvUsers.Rows(i).FindControl("chkselectuser"), CheckBox)

            If selection.Checked = True Then

                UserId(i) = gvUsers.Rows(i).Cells(2).Text
                str = UserId(i)
                isUserSelected = True
            End If
        Next

        If (isUserSelected = False) Then
            ShowConfirm("Please select User ")
            Exit Sub
        End If

        'storing Analysis rights informations in variables
        isAnalysisSelected = False

        For i = 0 To gvAnalysis.Rows.Count - 1

            selection = CType(gvAnalysis.Rows(i).FindControl("chkAnalysis"), CheckBox)
            If (selection.Checked = True) Then
                isAnalysisSelected = True
                viewRight = CType(gvAnalysis.Rows(i).FindControl("chkView"), CheckBox)
                deleteRight = CType(gvAnalysis.Rows(i).FindControl("chkDelete"), CheckBox)
               
                Reportname(i) = gvAnalysis.Rows(i).Cells(1).Text
                If (viewRight.Checked = True) Then
                    view(i) = True
                Else
                    view(i) = False
                    count = count + 1
                End If

                
                If (deleteRight.Checked = True) Then
                    delete(i) = True
                Else
                    delete(i) = False
                    count = count + 1
                End If
               
            End If
            If count = 2 Then
                ShowConfirm("Select Rights on selected Analysis")
                Exit Sub
            Else
                count = 0
            End If
        Next

        If (isAnalysisSelected = False) Then
            ShowConfirm("Please select Analysis ")
            Exit Sub
        End If
        assignedby = loggedId

        'to stop duplicate values

        For i = 0 To UserId.Length - 1
            If UserId(i) <> "" Then
                For j = 0 To Reportname.Length - 1
                    If Reportname(j) <> "" Then
                        con.Open()
                        Dim comDup As New SqlCommand("select analysisname,userid from IDMS_AnalysisRights where analysisname='" & Reportname(j) & "' and userid ='" & UserId(i) & "'", con)
                        Dim rdrDup As SqlDataReader = comDup.ExecuteReader()
                        If rdrDup.Read() Then
                            objanalysis.Update_AnalysisRight(UserId(i), Reportname(j), view(j), delete(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                           
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                            
                            con.Close()
                            Dim cmduse = New SqlCommand("select analysisname from IDMS_AnalysisRights where analysisname='" + Reportname(j) + "'", con)
                            Dim tableuse As String = ""

                            con.Open()
                            tableuse = cmduse.executescalar()
                            con.Close()
                            
                            Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters

                                .AddWithValue("@EntityName", tableuse)
                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)

                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Analysis")
                                .AddWithValue("@DeptId", ddlDepartment.SelectedValue)
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
                                .AddWithValue("@ClientId", clta)
                                .AddWithValue("@LOBId", lobt)
                                .AddWithValue("@All", all)
                            End With
                            con.Open()
                            cmdins2.ExecuteNonQuery()
                            con.Close()
                            cmdins2.Dispose()
                            '*************change*************



                        Else
                            objanalysis.Insert_AnalysisRights(UserId(i), Reportname(j), view(j), delete(j), assignedby)
                            '*************change*************
                            Dim all As String = ""
                            If view(j) = True Then
                                all = "View"
                            End If
                            If delete(j) = True Then
                                If all = "" Then
                                    all = "Delete"
                                Else
                                    all = all & "," & "Delete"
                                End If
                            End If
                           
                            con.Close()
                            Dim cmduse = New SqlCommand("select analysisname from IDMS_AnalysisRights where analysisname='" + Reportname(j) + "'", con)
                            Dim tableuse As String = ""

                            con.Open()
                            tableuse = cmduse.executescalar()
                            con.Close()
                            Dim cmdins2 = New SqlCommand("TrackReportRights", con)
                            cmdins2.CommandType = CommandType.StoredProcedure
                            With cmdins2.Parameters


                                .AddWithValue("@actionBY", assignedby)
                                .AddWithValue("@Action", "Assign Rights")
                                .AddWithValue("@Date", System.DateTime.Now)
                                .AddWithValue("@AssignTo", UserId(i))
                                .AddWithValue("@Entity", "Analysis")
                                .AddWithValue("@EntityName", tableuse)

                                .AddWithValue("@DeptId", ddlDepartment.SelectedValue)
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
                                .AddWithValue("@ClientId", clta)
                                .AddWithValue("@LOBId", lobt)
                                .AddWithValue("@All", all)
                            End With
                            con.Open()
                            cmdins2.ExecuteNonQuery()
                            con.Close()
                            cmdins2.Dispose()
                            '*************change*************


                        End If

                        con.Close()

                    End If
                Next

            End If
        Next
        'Confirmation of success
        ShowConfirm("Rights assigned successfully")

        ClearAll()



    End Sub

    ''' <summary>
    ''' display the users from selected span
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUser.Click
        BindUser()
        If gvUsers.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If

    End Sub
    'Method to bind user to gridview that show user list
    Protected Sub BindUser()
        If ddlDepartment.SelectedIndex > 0 Then
            gvUsers.Visible = True
            gvUsers.Columns(2).Visible = True
            If ddlLob.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLob.SelectedValue)
            Else
                lobid = "0"
            End If
            If ddlClient.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ddlClient.SelectedValue)
            Else
                client_id = "0"
            End If

            deptid = Convert.ToInt32(ddlDepartment.SelectedValue)
            gvUsers.Columns(2).Visible = True
            gvUsers.DataSource = uspan.bind_userbuddy(loggedId, deptid, client_id, lobid)
            gvUsers.DataBind()
        End If
      
    End Sub
    ''' <summary>
    ''' paging to control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvUsers_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvUsers.PageIndexChanging

        If gvUsers.PageIndex < gvUsers.PageCount And gvUsers.PageIndex >= 0 Then

            gvUsers.PageIndex = e.NewPageIndex

            BindUser()      'Method called to bind user
        End If

    End Sub

    ''' <summary>
    ''' paging to control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysis_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAnalysis.PageIndexChanging
        If gvAnalysis.PageIndex < gvAnalysis.PageCount And gvAnalysis.PageIndex >= 0 Then
            gvAnalysis.PageIndex = e.NewPageIndex
            BindAnalysis()
        End If
    End Sub
    ''' <summary>
    ''' clear the controls after assigning the rights
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        ddlClientanalysis.Items.Clear()
        ddlLobAnalysis.Items.Clear()
        gvUsers.Visible = False
        gvAnalysis.Visible = False
        ddlDepartment.SelectedIndex = 0
        ddlDepartmentAnalysis.SelectedIndex = 0
        btnAssignAnalysisRight.Visible = False

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
    ''' to select all checkbox in gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvUsers_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvUsers.RowDataBound
        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:Select('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If
    End Sub
    ''' <summary>
    ''' to select all checkbox in gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub gvAnalysis_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gvAnalysis.RowDataBound

        If e.Row.RowType = DataControlRowType.Header Then
            CType(e.Row.FindControl("chkSelectAll"), CheckBox).Attributes.Add("onClick", "javascript:SelectAll('" + CType(e.Row.FindControl("chkSelectAll"), CheckBox).ClientID + "')")
        End If
    End Sub


    ''' <summary>
    ''' 'Method to bind analysis to gridview that show analysis list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindAnalysis()
        If ddlDepartmentAnalysis.SelectedIndex <> 0 Then
            gvAnalysis.Visible = True
            If ddlClientanalysis.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ddlClientanalysis.SelectedValue)
            Else
                client_id = "0"
            End If
            If ddlLobAnalysis.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLobAnalysis.SelectedValue)
            Else
                lobid = "0"
            End If
            deptid = Convert.ToInt32(ddlDepartmentAnalysis.SelectedValue)

            If UserType = "Admin" Then
                gvAnalysis.DataSource = uspan.analysisselectadminspan(loggedId, deptid, client_id, lobid)
                gvAnalysis.DataBind()
                Exit Sub
            End If

            If UserType = "User" Then
                gvAnalysis.DataSource = uspan.analysisForuser(loggedId, deptid, client_id, lobid)
                gvAnalysis.DataBind()
                Exit Sub
            End If
        End If
    End Sub
    ''' <summary>
    ''' call the function to show analysis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShowAnalysis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShowAnalysis.Click

        btnAssignAnalysisRight.Visible = False
        BindAnalysis()        'Method to bind analysis

        If gvAnalysis.Rows.Count = 0 Then
            ShowConfirm("No record found...")
            Exit Sub
        End If
        btnAssignAnalysisRight.Visible = True

    End Sub
    ''' <summary>
    ''' clear the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl4.Load
        If ddlDepartmentAnalysis.SelectedIndex = 0 Then
            ddlClientanalysis.Items.Clear()
            ddlLobAnalysis.Items.Clear()

        End If

        If ddlClientanalysis.SelectedIndex = 0 Then
            ddlLobAnalysis.Items.Clear()

        End If

    End Sub
    ''' <summary>
    ''' clear the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DCLUserControl3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles DCLUserControl3.Load

        If ddlDepartment.SelectedIndex = 0 Then
            ddlClient.Items.Clear()
            ddlLob.Items.Clear()

        End If

        If ddlClient.SelectedIndex = 0 Then
            ddlLob.Items.Clear()

        End If

    End Sub
End Class
