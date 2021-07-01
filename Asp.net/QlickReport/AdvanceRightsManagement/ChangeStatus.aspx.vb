Imports system.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class AdvanceRightsManagement_ChangeStatus
    Inherits System.Web.UI.Page
    Dim con As SqlConnection
    Dim str As String = AppSettings("ConnectionString")
    Dim fun As New Functions
    Dim ddlDepartment As DropDownList
    Dim ddlClient As DropDownList
    Dim ddlLob As DropDownList
    Dim deptid As String
    Dim client_id As String
    Dim lobid As String
    Dim tableobj As New TableRight
    Dim UserId As String
    Dim loggedId As String
    Dim UserType As String
    Dim uspan As New UserSpan
    Dim uitem As New UpdateItem
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

        ddlDepartment = CType(DCLUserControl1.FindControl("ddlDepartment"), DropDownList)
        ddlClient = CType(DCLUserControl1.FindControl("ddlClient"), DropDownList)
        ddlLob = CType(DCLUserControl1.FindControl("ddlLob"), DropDownList)

        If Me.IsPostBack = False Then
            'Department dropdownlist is being populated on page load
            If UserType = "Admin" Then

                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "DeptID"
                ddlDepartment.DataSource = fun.bind_Dept()
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")

                ddlChangeStatus.Items.Insert(0, "--Select--")
                ddlChangeStatus.Items.Insert(1, "Local")
                ddlChangeStatus.Items.Insert(2, "Non Local")


            End If
        End If
    End Sub
    ''' <summary>
    ''' bind tables according to selected sapn
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindTable()
        If ddlDepartment.SelectedIndex > 0 Then


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

            If UserType = "Admin" Then

                'ddlSelect.DataSource = fun.bind_lobTable(deptid, client_id, lobid)
                ddlSelect.DataSource = uspan.tableselectadminspan(loggedId, deptid, client_id, lobid)

                ddlSelect.DataTextField = "TableName"
                ddlSelect.DataValueField = "TableId"
                ddlSelect.DataBind()
                ddlSelect.Items.Insert(0, "--Select--")
                'ElseIf UserType = "User" Then

                '    ddlSelect.DataSource = uspan.bind_lobTableUser(loggedId, deptid, client_id, lobid)

                '    ddlSelect.DataTextField = "TableName"
                '    ddlSelect.DataValueField = "TableId"
                '    ddlSelect.DataBind()
                '    ddlSelect.Items.Insert(0, "--Select--")

                '    Exit Sub
                'ElseIf UserType = "Super admin" Then
                '    ddlSelect.DataSource = uspan.bind_lobTableSA(deptid, client_id, lobid)

                '    ddlSelect.DataTextField = "TableName"
                '    ddlSelect.DataValueField = "TableId"
                '    ddlSelect.DataBind()
                '    ddlSelect.Items.Insert(0, "--Select--")
            End If
        Else
            ShowConfirm("Please select Department.")
        End If

    End Sub
    ''' <summary>
    ''' bind the command
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindCmd()
        If ddlDepartment.SelectedIndex > 0 Then

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
            If UserType = "Admin" Then

                ddlSelect.DataSource = uspan.cmdselectadminspan(loggedId, deptid, client_id, lobid)

                ddlSelect.DataTextField = "cmdName"
                ddlSelect.DataValueField = "cmdid"
                ddlSelect.DataBind()
                ddlSelect.Items.Insert(0, "--Select--")

                Exit Sub
            End If

        Else
            ShowConfirm("Please select Department.")
        End If
    End Sub
    ''' <summary>
    ''' Method to bind reports according to selection
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindReport()

        If ddlDepartment.SelectedIndex > 0 Then

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
            If UserType = "Admin" Then
                ddlSelect.DataSource = uspan.reportselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelect.DataTextField = "QueryName"
                ddlSelect.DataValueField = "RecordId"
                ddlSelect.DataBind()
                ddlSelect.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        Else
            ShowConfirm("Please select Department.")
        End If

    End Sub
    ''' <summary>
    ''' bind the views
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindView()

        Dim dsViews As New DataSet()

        If ddlDepartment.SelectedIndex > 0 Then

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
            ddlSelect.DataSource = uspan.viewselectadminspan(loggedId, deptid, client_id, lobid)
            ddlSelect.DataTextField = "ViewName"
            ddlSelect.DataValueField = "ViewId"
            ddlSelect.DataBind()
            ddlSelect.Items.Insert(0, "--Select--")
            Exit Sub
        Else
            ShowConfirm("Please select Department.")
        End If

        'If UserType = "User" Then
        '    ddlSelect.DataSource = uspan.bind_lobViewUser(deptid, client_id, lobid)
        '    ddlSelect.DataTextField = "ViewName"
        '    ddlSelect.DataValueField = "ViewId"
        '    ddlSelect.DataBind()
        '    ddlSelect.Items.Insert(0, "--Select--")
        '    Exit Sub
        'End If
      
    End Sub

    ''' <summary>
    ''' bind table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoTable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoTable.CheckedChanged
        BindTable()

    End Sub
    ''' <summary>
    '''   ''' bind view
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoView_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoView.CheckedChanged
        BindView()

    End Sub
    ''' <summary>
    '''   ''' bind  command
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoCmd_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoCmd.CheckedChanged
        BindCmd()

    End Sub
    ''' <summary>
    '''  bind  reports
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoReport.CheckedChanged
        BindReport()

    End Sub
    ''' <summary>
    ''' insert the status
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub tbxLast_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxLast.TextChanged
        ddlChangeStatus.Items.Insert(0, "--Select--")
        ddlChangeStatus.Items.Insert(1, "Local")
        ddlChangeStatus.Items.Insert(2, "NonLocal")

    End Sub
    ''' <summary>
    ''' set the status
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub ddlSelect_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlSelect.SelectedIndexChanged

        If rdoTable.Checked = True Then

            Dim tableid As String
            Dim status As String
            tableid = ddlSelect.SelectedValue
            Dim ds1 As New DataSet
            ds1 = uspan.Get_TableStatus(tableid)
            status = ds1.Tables(0).Rows(0)("LocalTable").ToString()
            tbxLast.Text = status
        End If

        If rdoView.Checked = True Then

            Dim viewid As String
            Dim status As String
            viewid = ddlSelect.SelectedValue
            Dim ds1 As New DataSet
            ds1 = uspan.Get_ViewStatus(viewid)
            status = ds1.Tables(0).Rows(0)("LocalView").ToString()
            tbxLast.Text = status
        End If

        If rdoCmd.Checked = True Then
            Dim cmdid As String
            Dim status As String
            cmdid = ddlSelect.SelectedValue
            Dim ds1 As New DataSet
            ds1 = uspan.Get_CmdStatus(cmdid)
            status = ds1.Tables(0).Rows(0)("LocalCmd").ToString()
            tbxLast.Text = status
        End If

        If rdoReport.Checked = True Then
            Dim recordid As String
            Dim status As String
            recordid = ddlSelect.SelectedValue
            Dim ds1 As New DataSet
            ds1 = uspan.Get_ReportStatus(recordid)
            status = ds1.Tables(0).Rows(0)("ReportScope").ToString()
            tbxLast.Text = status

        End If

        If rdoAnalysis.Checked = True Then
            Dim Reportname As String
            Dim status As String
            Reportname = ddlSelect.SelectedValue
            Dim ds1 As New DataSet
            ds1 = objanalysis.Get_AnalysisStatus(Reportname)
            status = ds1.Tables(0).Rows(0)("Status").ToString()
            tbxLast.Text = status

        End If


    End Sub
    ''' <summary>
    ''' update the status
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSet_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSet.Click
        If ddlDepartment.SelectedIndex = 0 Then
            ShowConfirm("Select department")
            Exit Sub
        End If

        If (rdoTable.Checked = False) And (rdoReport.Checked = False) And (rdoView.Checked = False) And (rdoCmd.Checked = False) And (rdoAnalysis.Checked = False) Then
            ShowConfirm("Select choose item")
            Exit Sub
        End If

        If ddlSelect.SelectedIndex = 0 Then
            ShowConfirm("Select choose entity")
            Exit Sub
        End If

        If ddlChangeStatus.SelectedIndex = 0 Then
            ShowConfirm("Select change status")
            Exit Sub
        End If

        Dim entity As String = ""

        If rdoTable.Checked = True Then
            Dim tableid As String
            Dim newstatus As String
            tableid = ddlSelect.SelectedValue
            newstatus = ddlChangeStatus.SelectedValue
            uitem.Change_TableStatus(tableid, newstatus)
            entity = rdoTable.Text
        End If

        If rdoView.Checked = True Then
            Dim viewid As String
            Dim newstatus As String
            viewid = ddlSelect.SelectedValue
            newstatus = ddlChangeStatus.SelectedValue
            uitem.Change_ViewStatus(viewid, newstatus)
            entity = rdoView.Text
        End If

        If rdoCmd.Checked = True Then
            Dim cmdid As String
            Dim newstatus As String
            cmdid = ddlSelect.SelectedValue
            newstatus = ddlChangeStatus.SelectedValue
            uitem.Change_CmdStatus(cmdid, newstatus)
            entity = rdoCmd.Text
        End If

        If rdoReport.Checked = True Then
            Dim recordid As String
            Dim newstatus As String
            recordid = ddlSelect.SelectedValue
            newstatus = ddlChangeStatus.SelectedValue
            uitem.Change_ReportStatus(recordid, newstatus)
            entity = rdoReport.Text
        End If

        If rdoAnalysis.Checked = True Then
            Dim Reportname As String
            Dim newstatus As String
            Reportname = ddlSelect.SelectedValue
            newstatus = ddlChangeStatus.SelectedValue
            objanalysis.Change_AnalysisStatus(Reportname, newstatus)
            entity = rdoAnalysis.Text
        End If



        '*************change*************
        Dim con As New SqlConnection(str)

        Dim cmdins2 = New SqlCommand("sp_Trackchangestatus", con)
        cmdins2.CommandType = CommandType.StoredProcedure
        With cmdins2.Parameters
            .AddWithValue("@actionby", Session("userid"))
            .AddWithValue("@action", "Change Status")
            .AddWithValue("@date", System.DateTime.Now)
            .AddWithValue("@entity", entity)
            .AddWithValue("@entityname", ddlSelect.SelectedItem.Text)

            .AddWithValue("@deptid", Convert.ToInt32(ddlDepartment.SelectedValue))
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
            .AddWithValue("@clientid", clta)
            .AddWithValue("@lobid", lobt)
            .AddWithValue("@assignto", "")
            .AddWithValue("@oldstatus", tbxLast.Text)
            .AddWithValue("@newstatus", ddlChangeStatus.SelectedItem.Text)
            .AddWithValue("@type", Session("usertype"))
        End With
        con.Open()
        cmdins2.ExecuteNonQuery()
        con.Close()
        cmdins2.Dispose()

        '*************change*************
        ShowConfirm("Status updated successfully")
        ClearAll()

    End Sub
    ''' <summary>
    ''' clear controls
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ClearAll()
        ddlClient.Items.Clear()
        ddlLob.Items.Clear()
        ddlSelect.Items.Clear()
        tbxLast.Text = ""
        ddlChangeStatus.SelectedIndex = 0
        ddlDepartment.SelectedIndex = 0
        rdoTable.Checked = False
        rdoReport.Checked = False
        rdoCmd.Checked = False
        rdoView.Checked = False
        rdoAnalysis.Checked = False
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
    ''' call the bind analysis function
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub rdoAnalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdoAnalysis.CheckedChanged
        BindAnalysis()
    End Sub

    ''' <summary>
    ''' 'Method to bind analysis to gridview that show analysis list
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BindAnalysis()

        If ddlDepartment.SelectedIndex > 0 Then

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
            If UserType = "Admin" Then
                ddlSelect.DataTextField = "analysisname"
                ddlSelect.DataValueField = "analysisname"
                ddlSelect.DataSource = uspan.analysisselectadminspan(loggedId, deptid, client_id, lobid)
                ddlSelect.DataBind()
                ddlSelect.Items.Insert(0, "--Select--")
                Exit Sub
            End If
        Else
            ShowConfirm("Please Select Department.")
        End If
    End Sub

End Class
