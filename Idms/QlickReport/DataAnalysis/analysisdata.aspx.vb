Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO


Partial Class analysisdata
    Inherits System.Web.UI.Page
    Dim repcolarray
    Dim stcolumns As String
    Dim dataadapter As New SqlDataAdapter
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(conn)
    Dim connection As New SqlConnection(conn)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim readquery1 As SqlDataReader
    Dim p
    Dim ttcount, cccount, p2, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer
    Dim cmd As New SqlCommand
    Dim columnname, cname, tabcolumn, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, repcolumn, str22, str12, totalquerycloumn As String
    Dim j As Integer = 0
    Public stt As String
    Dim colname, formula1, groupby, orderby As String
    Dim repobj As New ReportDesigner
    Public filtercolumn As String
    Dim ds2 As New DataSet
    Dim alertquery As String = ""
    Dim colarray1 As String()
    Dim alertquery1 As String = ""
    Dim AnalysisObject As New SavedAnalysis
    Dim tablename As String
    Dim tablearray
    ''' <summary>
    '''In this some functions are working to get the client name, user name, report name according to the department id
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 1 march 2008</remarks>
    'Protected Sub ddlDepartmant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmant.SelectedIndexChanged
    '    If RdioAnalysis.Checked = False And RdioReport.Checked = False Then
    '        aspnet_msgbox("Select Atleast One Radio Button")
    '        ddlDepartmant.SelectedIndex = 0
    '        Exit Sub

    '    End If
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    '    'Divhead.InnerHtml = ""
    '    headdiv.InnerHtml = ""
    '    dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'If relax.InnerHtml = "" Then
    '    '    allcolumns.Visible = False
    '    '    inde.Visible = False
    '    '    trcols1.Visible = False
    '    '    trcols.Visible = False
    '    'Else
    '    '    trcols1.Visible = True
    '    '    trcols.Visible = True
    '    '    allcolumns.Visible = True
    '    '    inde.Visible = True
    '    'End If
    '    report.InnerHtml = ""
    '    'Me.divFormula.Visible = False

    '    selectedcols.Items.Clear()
    '    hid.Value = ""
    '    repcols.Items.Clear()
    '    If ddlDepartmant.SelectedItem.Text = "---select---" Then
    '        ddlClient.Items.Clear()
    '        'ddlClient.Items.Insert(0, "---select---")
    '        ddlUser.Items.Clear()
    '        'ddlUser.Items.Insert(0, "---select---")
    '        ddlReport.Items.Clear()
    '        'ddlReport.Items.Insert(0, "---select---")
    '    Else

    '        Dim classobj As New Functions
    '        If Session("typeofuser") = "Admin" Then



    '            ddlClient.DataTextField = "clientname"
    '            ddlClient.DataValueField = "autoid"
    '            ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)

    '            'ddlClient.DataSource = classobj.bindAdminClients(Session("userid1"), ddlDepartmant.SelectedValue)
    '            ddlClient.DataBind()
    '            ddlClient.Items.Insert(0, "---select---")

    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"
    '                ddlReport.DataSource = AnalysisObject.SelectvalueFrom_Analysis(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")
    '                ddlUser.Enabled = False
    '            ElseIf Me.RdioReport.Checked Then
    '                ddlUser.Enabled = True
    '                ddlUser.DataTextField = "username"
    '                ddlUser.DataValueField = "userid"
    '                ddlUser.DataSource = classobj.userselectadminspan(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                ddlUser.DataBind()
    '                ddlUser.Items.Insert(0, "---select---")
    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"
    '                ddlReport.DataSource = repobj.reportForadmin(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")

    '            End If

    '            ddlLob.Items.Clear()
    '        End If
    '        If Session("typeofuser") = "User" Then



    '            ddlClient.DataTextField = "clientname"
    '            ddlClient.DataValueField = "autoid"
    '            ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)
    '            'ddlClient.DataSource = classobj.bindUserClients(Session("userid1"), ddlDepartmant.SelectedValue)
    '            ddlClient.DataBind()
    '            ddlClient.Items.Insert(0, "---select---")
    '            'ddlUser.DataTextField = "username"
    '            'ddlUser.DataValueField = "userid"
    '            'ddlUser.DataSource = classobj.bind_userlocalDept(ddlDepartmant.SelectedValue)
    '            'ddlUser.DataBind()
    '            'ddlUser.Items.Insert(0, "---select---")
    '            ddlUser.Enabled = False

    '            Dim SCOPE As String = repobj.chkUserscope(Session("userid"), ddlDepartmant.SelectedValue, 0, 0)
    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"
    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forlocaluser(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                Else
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forNonlocaluser(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                End If
    '            ElseIf Me.RdioReport.Checked Then
    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"
    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = repobj.reportForlocal(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                Else
    '                    ddlReport.DataSource = repobj.reportFornonlocal(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
    '                End If
    '            End If

    '            'ddlReport.DataSource = classobj.bind_departmentrepuser(ddlDepartmant.SelectedValue)
    '            ddlReport.DataBind()
    '            ddlReport.Items.Insert(0, "---select---")
    '            ddlLob.Items.Clear()

    '        End If


    '    End If
    '    If repcols.Items.Count = 0 Then
    '        'listcolumns.Items.Clear()
    '        'groupcolumns.Items.Clear()
    '    End If
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    'If result.InnerHtml <> "" Or report.InnerHtml <> "" Or relax.InnerHtml <> "" Then
    '    '    divsavereport.Visible = False
    '    '    btnsum.Enabled = True
    '    'End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    'End Sub
    ''' <summary>
    '''In this some functions are working to get all the department name
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 3 march 2008</remarks>
    Dim dr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                Me.Gettable.Visible = False
                Me.spandisplay1.Visible = True
                Me.SAVE_multiuser.Visible = True
                Me.SAVE_singleuser.Visible = False

            Else
                Me.spandisplay.Visible = False
                Me.spandisplay1.Visible = False
                Me.Gettable.Visible = True
                Me.SAVE_multiuser.Visible = False
                Me.SAVE_singleuser.Visible = True
            End If
            connection.Close()
        End If

        
        Dim typeofuser = Session("typeofuser")
        If Page.IsPostBack = False Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            End If
            DepartmentName.Items.Insert(0, "--Select--")
        End If


        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        'If relax.InnerHtml = "" Then
        '    relax.Visible = False
        'Else
        '    relax.Visible = True
        'End If
        'TextBox1.Visible = False



        'Session("table") = ddlTable.SelectedItem.Text

        Dim dept, lob, clt As String
        'If DropDownclient.SelectedIndex = 0 Or DropDownclient.SelectedIndex = -1 Then
        '    clt = "0"
        'Else
        '    clt = DropDownclient.SelectedValue
        'End If
        'If DropDownlob.SelectedIndex = 0 Or DropDownlob.SelectedIndex = -1 Then
        '    lob = "0"
        'Else
        '    lob = DropDownlob.SelectedValue
        'End If
        'If DropDowndept.SelectedIndex = 0 Or DropDowndept.SelectedIndex = -1 Then
        '    dept = "0"
        'Else
        '    dept = DropDowndept.SelectedValue
        'End If
        'If Session("typeofuser") = "Admin" Then
        '    Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
        '    cmdupdate.CommandType = CommandType.StoredProcedure
        '    With cmdupdate.Parameters
        '        .AddWithValue("@userid", Session("userid"))
        '        .AddWithValue("@Deptid", dept)
        '        .AddWithValue("@Clientid", clt)
        '        .AddWithValue("@LOBID", lob)
        '    End With
        '    Dim readerdata As SqlDataReader
        '    con.Open()
        '    readerdata = cmdupdate.ExecuteReader


        '    'If readerdata.HasRows Then
        '    '    chklocal.Enabled = True
        '    'Else

        '    '    chklocal.Checked = False
        '    '    chklocal.Enabled = False
        '    'End If
        '    readerdata.Close()
        '    con.Close()
        'ElseIf Session("typeofuser") = "User" Then
        '    Dim SCOPE As String = repobj.chkUserscope(Session("userid"))
        '    'If SCOPE = "Local" Then
        '    '    chklocal.Enabled = True
        '    'Else
        '    '    chklocal.Checked = False
        '    '    chklocal.Enabled = False
        '    'End If
        'End If
        'If Page.IsPostBack = False Then
        '    Session("Checkforreportformilas") = ""
        '    result.InnerHtml = ""
        '    report.InnerHtml = ""
        '    'relax.InnerHtml = ""
        '    divsavereport.Visible = False
        '    Session("reportname") = ""
        '    'dataoperation.Visible = False
        '    'allcolumns.Visible = False
        '    'inde.Visible = False
        '    'selectionformula.Visible = False
        '    'valueinput.Visible = False
        '    'go.Visible = False

        '    'Me.divFormula.Visible = False
        Me.Calendar1.Visible = False
        Dim classobj As New Functions

        'If Session("typeofuser") = "Admin" Then


        'ddlDepartmant.DataTextField = "departmentname"
        'ddlDepartmant.DataValueField = "DeptID"
        'ddlDepartmant.DataSource = classobj.bind_Dept()
        'ddlDepartmant.DataSource = classobj.bind_AdminDept(Session("userid1"))

        'ddlDepartmant.DataBind()
        'ddlDepartmant.Items.Insert(0, "---select---")
        'DropDowndept.DataTextField = "departmentname"
        'DropDowndept.DataValueField = "DeptID"
        'DropDowndept.DataSource = classobj.bind_Dept()
        ''DropDowndept.DataSource = classobj.bind_AdminDept(Session("userid1"))

        'DropDowndept.DataBind()
        'DropDowndept.Items.Insert(0, "---select---")
        'End If
        'If Session("typeofuser") = "User" Then


        'ddlDepartmant.DataTextField = "departmentname"
        'ddlDepartmant.DataValueField = "DeptID"
        'ddlDepartmant.DataSource = classobj.bind_usersDept(Session("userid1"))

        'ddlDepartmant.DataBind()
        'ddlDepartmant.Items.Insert(0, "---select---")
        'DropDowndept.DataTextField = "departmentname"
        'DropDowndept.DataValueField = "DeptID"
        'DropDowndept.DataSource = classobj.bind_usersDept(Session("userid1"))
        'DropDowndept.DataBind()
        'DropDowndept.Items.Insert(0, "---select---")
        'End If
        ' 
        'End If
        'If relax.InnerHtml = "" Then
        '    Divhead.Visible = False
        '    relax.Visible = False
        'Else
        '    Divhead.Visible = True
        '    relax.Visible = True
        'End If
        'If report.InnerHtml = "" Then
        '    headdiv.Visible = False
        '    report.Visible = False
        'Else
        '    headdiv.Visible = True
        '    report.Visible = True
        'End If


        If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

            'Button4.Enabled = False
        End If
        If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
            'Button4.Enabled = True
        End If

    End Sub
    ''' <summary>
    '''In this some functions are working to get the lob name, user name, report name according to the departmentid, clientid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 5 march 2008</remarks>
    'Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    '    dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    'Divhead.InnerHtml = ""
    '    headdiv.InnerHtml = ""
    '    Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'If relax.InnerHtml = "" Then
    '    '    allcolumns.Visible = False
    '    '    inde.Visible = False
    '    '    trcols1.Visible = False
    '    '    trcols.Visible = False
    '    'Else
    '    '    trcols1.Visible = True
    '    '    trcols.Visible = True
    '    '    allcolumns.Visible = True
    '    '    inde.Visible = True
    '    'End If
    '    report.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    selectedcols.Items.Clear()
    '    hid.Value = ""
    '    repcols.Items.Clear()
    '    If ddlClient.SelectedItem.Text = "---select---" Then
    '        ddlUser.Items.Clear()
    '        ddlUser.Items.Insert(0, "---select---")
    '        ddlLob.Items.Clear()
    '        ddlLob.Items.Insert(0, "---select---")
    '        ddlReport.Items.Clear()
    '        ddlReport.Items.Insert(0, "---select---")
    '    Else

    '        Dim classobj As New Functions


    '        If Session("typeofuser") = "Admin" Then

    '            ddlLob.DataTextField = "lobname"
    '            ddlLob.DataValueField = "autoid"
    '            ddlLob.DataSource = classobj.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)

    '            'ddlLob.DataSource = classobj.bindAdminLobOnDeptClient(Session("userid1"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '            ddlLob.DataBind()
    '            ddlLob.Items.Insert(0, "---select---")








    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"
    '                ddlReport.DataSource = AnalysisObject.SelectvalueFrom_Analysis(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")
    '                ddlUser.Enabled = False
    '            ElseIf Me.RdioReport.Checked Then
    '                ddlUser.Enabled = True
    '                ddlUser.DataTextField = "username"
    '                ddlUser.DataValueField = "userid"
    '                ddlUser.DataSource = classobj.userselectadminspan(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                ddlUser.DataBind()
    '                ddlUser.Items.Insert(0, "---select---")


    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"

    '                ddlReport.DataSource = repobj.reportForadmin(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")

    '            End If














    '        End If
    '            If Session("typeofuser") = "User" Then
    '                ddlLob.DataTextField = "lobname"
    '                ddlLob.DataValueField = "autoid"
    '                ddlLob.DataSource = classobj.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '                'ddlLob.DataSource = classobj.binduserLobOnDeptClient(Session("userid1"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '                ddlLob.DataBind()
    '                ddlLob.Items.Insert(0, "---select---")
    '                ddlUser.Enabled = False
    '                'ddlUser.DataTextField = "username"
    '                'ddlUser.DataValueField = "userid"
    '                'ddlUser.DataSource = classobj.bind_clientuserforusers(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '                'ddlUser.DataBind()
    '                'ddlUser.Items.Insert(0, "---select---")

    '            Dim SCOPE As String = repobj.chkUserscope(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, 0)



    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"

    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forlocaluser(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                Else
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forNonlocaluser(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                End If
    '            ElseIf Me.RdioReport.Checked Then

    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"
    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = repobj.reportForlocal(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                Else
    '                    ddlReport.DataSource = repobj.reportFornonlocal(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, "0")
    '                End If
    '            End If



    '                'ddlReport.DataSource = repobj.chkUserscope(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, 0)
    '                'ddlReport.DataSource = classobj.bind_clientrepforuser(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")

    '                '
    '            End If


    '        End If
    '    If repcols.Items.Count = 0 Then
    '        'listcolumns.Items.Clear()
    '        'groupcolumns.Items.Clear()
    '    End If
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
    '        divsavereport.Visible = False
    '        btnsum.Enabled = True
    '    End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    'End Sub
    ''' <summary>
    ''' this is fetching the report name saved by the selected user
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub ddlUser_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlUser.SelectedIndexChanged
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    '    dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
    '        divsavereport.Visible = False
    '        btnsum.Enabled = True
    '    End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    '    If ddlUser.SelectedItem.Text = "---select---" Then

    '        ddlReport.Items.Clear()
    '        ddlReport.Items.Insert(0, "---select---")
    '    Else


    '        Dim classobj As New Functions


    '        If Session("typeofuser") = "Admin" Then


    '            ddlReport.DataTextField = "QueryName"
    '            ddlReport.DataValueField = "Recordid"
    '            ddlReport.DataSource = classobj.reportsofuser(ddlUser.SelectedValue)
    '            ddlReport.DataBind()
    '            ddlReport.Items.Insert(0, "---select---")
    '        End If
    '        'If Session("typeofuser") = "User" Then


    '        '    ddlReport.DataTextField = "queryname"
    '        '    ddlReport.DataValueField = "recordid"
    '        '    ddlReport.DataSource = classobj.reportsofuserforuser(ddlUser.SelectedValue)
    '        '    ddlReport.DataBind()
    '        '    ddlReport.Items.Insert(0, "---select---")
    '        'End If
    '    End If


    'End Sub
    ''' <summary>
    '''In this some functions are working to get the user name, report name according to the departmentid, clientid and lobid id
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 7 march 2008</remarks>
    'Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    '    dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    'Divhead.InnerHtml = ""
    '    headdiv.InnerHtml = ""
    '    Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'If relax.InnerHtml = "" Then
    '    '    allcolumns.Visible = False
    '    '    inde.Visible = False
    '    '    trcols1.Visible = False
    '    '    trcols.Visible = False
    '    'Else
    '    '    trcols1.Visible = True
    '    '    trcols.Visible = True
    '    '    allcolumns.Visible = True
    '    '    inde.Visible = True
    '    'End If
    '    report.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    selectedcols.Items.Clear()
    '    hid.Value = ""
    '    repcols.Items.Clear()
    '    Dim classobj As New Functions
    '    If ddlLob.SelectedItem.Text = "---select---" Then
    '        ddlUser.Items.Clear()
    '        ddlUser.Items.Insert(0, "---select---")

    '        ddlReport.Items.Clear()
    '        ddlReport.Items.Insert(0, "---select---")
    '    Else
    '        If Session("typeofuser") = "Admin" Then



    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"
    '                ddlReport.DataSource = AnalysisObject.SelectvalueFrom_Analysis(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")
    '                ddlUser.Enabled = False
    '            ElseIf Me.RdioReport.Checked Then
    '                ddlUser.Enabled = True
    '                ddlUser.DataTextField = "username"
    '                ddlUser.DataValueField = "userid"
    '                ddlUser.DataSource = classobj.userselectadminspan(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                ddlUser.DataBind()
    '                ddlUser.Items.Insert(0, "---select---")
    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"
    '                ddlReport.DataSource = repobj.reportForadmin(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                ddlReport.DataBind()
    '                ddlReport.Items.Insert(0, "---select---")

    '            End If












    '        End If
    '        If Session("typeofuser") = "User" Then

    '            'ddlUser.DataTextField = "username"
    '            'ddlUser.DataValueField = "userid"
    '            'ddlUser.DataSource = classobj.bind_lobuserforuser(ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '            'ddlUser.DataBind()
    '            'ddlUser.Items.Insert(0, "---select---")
    '            ddlUser.Enabled = False

    '            Dim SCOPE As String = repobj.chkUserscope(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)


    '            If RdioAnalysis.Checked Then
    '                ddlReport.DataTextField = "analysisname"
    '                ddlReport.DataValueField = "Recordid"

    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forlocaluser(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                Else
    '                    ddlReport.DataSource = AnalysisObject.Analysis_forNonlocaluser(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                End If
    '            ElseIf Me.RdioReport.Checked Then

    '                ddlReport.DataTextField = "QueryName"
    '                ddlReport.DataValueField = "Recordid"
    '                If SCOPE = "Local" Then
    '                    ddlReport.DataSource = repobj.reportForlocal(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                Else
    '                    ddlReport.DataSource = repobj.reportFornonlocal(Session("userid"), ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '                End If
    '            End If













    '            'ddlReport.DataSource = classobj.bind_lobrepforuser(ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
    '            ddlReport.DataBind()
    '            ddlReport.Items.Insert(0, "---select---")
    '            '
    '        End If
    '        '
    '    End If
    '    If repcols.Items.Count = 0 Then
    '        'listcolumns.Items.Clear()
    '        'groupcolumns.Items.Clear()
    '    End If
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
    '        divsavereport.Visible = False
    '        btnsum.Enabled = True
    '    End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    'End Sub
    ''' <summary>
    '''this is use to enable and disable the controls according to the requirement
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh </remarks>
    Dim colarray As String()
    Protected Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTable.SelectedIndexChanged

        repcols.Items.Clear()
        selectedcols.Items.Clear()
        'Session("maxval1") = ""
        'Session("maxval") = ""
        ''Divhead.InnerHtml = ""
        'headdiv.InnerHtml = ""
        'Me.result.InnerHtml = ""
        ''Me.relax.InnerHtml = ""
        'report.InnerHtml = ""
        ''Me.divFormula.Visible = False
        'selectedcols.Items.Clear()
        'hid.Value = ""
        'repcols.Items.Clear()
        ''If relax.InnerHtml = "" Then
        ''    allcolumns.Visible = False
        ''    inde.Visible = False
        ''    trcols1.Visible = False
        ''    trcols.Visible = False
        ''Else
        ''    trcols1.Visible = True
        ''    trcols.Visible = True
        ''    allcolumns.Visible = True
        ''    inde.Visible = True
        ''End If
        'If repcols.Items.Count = 0 Then
        '    'listcolumns.Items.Clear()
        '    'groupcolumns.Items.Clear()
        'End If
        'If ddlTable.SelectedItem.Text <> "---select---" Then
        '    Session("reportname") = "tab" & ddlTable.SelectedItem.Text
        '    Session("reportname1") = ddlTable.SelectedItem.Text
        'Else
        '    Session("reportname") = ""
        'End If
        'If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

        '    divsavereport.Visible = False
        '    btnsum.Enabled = False
        'End If
        'If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
        '    divsavereport.Visible = False
        '    btnsum.Enabled = True
        'End If
        ''If relax.InnerHtml = "" Then
        ''    Divhead.Visible = False
        ''    relax.Visible = False
        ''Else
        ''    Divhead.Visible = True
        ''    relax.Visible = True
        ''End If
        ''If report.InnerHtml = "" Then
        ''    headdiv.Visible = False
        ''    report.Visible = False
        ''Else
        ''    headdiv.Visible = True
        ''    report.Visible = True
        ''End If
    End Sub
    ''' <summary>
    '''here the selected report is generated and the columns used in that report is binded in listbox and enable the panel if report is already analyzed
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 10 march 2008</remarks>
    'Protected Sub ShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport.Click
    '    Session("Checkforreportformilas") = ""
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    '    'If Me.RdioAnalysis.Checked = False And Me.RdioReport.Checked = False Then
    '    '    aspnet_msgbox("Select Atleast One Radio Button")
    '    '    Exit Sub
    '    'End If
    '    Dim datetime As String = ddlReport.SelectedItem.Text & Session("userid")
    '    'datetime = datetime & Session("userid")
    '    'Dim preday As DateTime
    '    'preday = System.DateTime.Now.AddDays(-1).ToShortDateString
    '    'Dim booaa As Boolean

    '    'cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    'con.Open()
    '    'readquery = cmd.ExecuteReader
    '    'While readquery.Read()
    '    '    Dim pretabvle As String = readquery("name")
    '    '    If pretabvle.Contains("tabddlReport" & preday) Then
    '    '        booaa = False
    '    '        Exit While

    '    '    Else
    '    '        booaa = True
    '    '    End If
    '    'End While
    '    'readquery.Close()
    '    'con.Close()

    '    'If booaa = False Then

    '    '    cmd = New SqlCommand("drop table tabddlReport" + datetime + "", con)
    '    '    con.Open()
    '    '    cmd.ExecuteNonQuery()
    '    '    con.Close()
    '    '    'dataoperation.Visible = True
    '    '    'ddlReport.Visible = False
    '    '    'Exit Sub


    '    'Else

    '    'End If







    '    If ddlDepartmant.SelectedValue = "" Or ddlDepartmant.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("Select Department First")
    '        Exit Sub
    '    ElseIf ddlReport.SelectedValue = "" Then
    '        aspnet_msgbox("Select Report First")
    '        repcols.Items.Clear()
    '        Exit Sub
    '    ElseIf ddlReport.SelectedItem.Text = "---select---" Then
    '        If Me.RdioAnalysis.Checked = True Then

    '            aspnet_msgbox("Select Analysis First")
    '            Exit Sub
    '        Else
    '            aspnet_msgbox("Select Report First")
    '            Exit Sub
    '        End If

    '    ElseIf Me.RdioAnalysis.Checked = True Then
    '        dataoperation.Visible = True
    '        cmd = New SqlCommand("select savedby from SavedAnalysis where savedby='" + Session("userid") + "' and analysisname='" + ddlReport.SelectedItem.Text + "'", con)
    '        con.Open()
    '        readquery = cmd.ExecuteReader
    '        If readquery.HasRows Then
    '            new1.Enabled = True
    '        Else
    '            new1.Enabled = False
    '        End If
    '        con.Close()
    '        readquery.Close()

    '        ddlReport.Visible = False
    '        Exit Sub


    '        'ElseIf Me.RdioReport.Checked = True Then



    '    Else




    '        '=====================''''''''''''''''''''''''''

    '        Dim bo As Boolean






    '        cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '        con.Open()
    '        readquery = cmd.ExecuteReader
    '        While readquery.Read()
    '            If readquery("name") = "tabddlReport" & datetime Then
    '                bo = False
    '                Exit While

    '            Else
    '                bo = True
    '            End If
    '        End While
    '        readquery.Close()
    '        con.Close()

    '        If bo = False Then

    '            cmd = New SqlCommand("drop table tabddlReport" + datetime + "", con)
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            con.Close()
    '            'dataoperation.Visible = True
    '            'ddlReport.Visible = False
    '            'Exit Sub


    '        Else
    '            dataoperation.Visible = False
    '            ddlReport.Visible = True
    '        End If



    '        ''''''''''''''''''============================='''''''''''''''''''''''''''''''''

    '        Dim clientselect As String = ""
    '        Dim lobselect As String = ""
    '        If ddlClient.SelectedIndex <= 0 Then
    '            clientselect = 0
    '        Else
    '            clientselect = ddlClient.SelectedValue
    '        End If
    '        If ddlLob.SelectedIndex <= 0 Then
    '            lobselect = 0
    '        Else
    '            lobselect = ddlLob.SelectedValue
    '        End If


    '        Dim com As New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition,datecontable from idmsreportmaster where departmentid='" + ddlDepartmant.SelectedValue + "' and clientid='" + clientselect + "' and underlob='" + lobselect + "' and queryname='" + ddlReport.SelectedItem.Text + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '        con.Open()
    '        Dim columnname, cname, tabcolumn, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, repcolumn, str22, str12, totalquerycloumn As String
    '        Dim aa1
    '        Dim colsss
    '        Dim b As Boolean
    '        Dim da As New SqlDataAdapter
    '        Dim da1 As New SqlDataAdapter
    '        'Dim cmd As SqlCommand
    '        Dim ds As New DataSet
    '        Dim colfinal As Integer
    '        Dim ttcount, cccount, p2, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer
    '        Dim ds2 As New DataSet
    '        Dim final
    '        Dim havingcondition As String = ""
    '        report.InnerHtml = ""
    '        Dim htmlreport As String = ""
    '        Dim no As Integer
    '        Dim data As SqlDataReader
    '        Dim tablearray
    '        Dim tablename As String
    '        Dim colsa As DataColumn
    '        Dim reportcolumn As DataColumn
    '        Dim datecondition As String = ""
    '        readquery = com.ExecuteReader
    '        While readquery.Read()
    '            colname = readquery("colname")
    '            Dim scode As String = vbNewLine
    '            colname = colname.Replace(scode, "")
    '            If colname = "" Then
    '                aspnet_msgbox("This Report Is Empty")
    '                dataoperation.Visible = False
    '                ddlReport.Visible = True
    '                Exit Sub
    '            End If
    '            str12 = colname.Replace("~", ",")

    '            str22 = str12.Replace("$", ".")
    '            alertquery=str22
    '            Dim boo As Boolean = False
    '            Dim boo1 As Boolean = False
    '            boo = str22.Contains("@Date1@")
    '            boo1 = str22.Contains("@Date2@")
    '            If boo = True Then
    '                If TextBox3.Text = "" Then
    '                    aspnet_msgbox("Fill From Date")
    '                    dataoperation.Visible = False
    '                    ddlReport.Visible = True
    '                    Exit Sub
    '                End If

    '            End If
    '            If boo1 = True Then
    '                If TextBox4.Text = "" Then
    '                    aspnet_msgbox("Fill To Date")
    '                    dataoperation.Visible = False
    '                    ddlReport.Visible = True
    '                    Exit Sub
    '                End If
    '            End If
    '            str22 = str22.Replace("@Date1@", TextBox3.Text)
    '            str22 = str22.Replace("@Date2@", TextBox4.Text)
    '            str22 = str22.Replace("String.fromCharCode(34)", "")
    '            'str22 = str22.Replace("+", "")
    '            colname = str22

    '            If IsDBNull(readquery("wheredata")) Then
    '            Else

    '                formula1 = readquery("wheredata")
    '            End If
    '            If IsDBNull(readquery("datecontable")) Then
    '            Else

    '                datecondition = readquery("datecontable")
    '            End If

    '            If IsDBNull(readquery("groupby")) Then
    '            Else

    '                groupby = readquery("groupby")
    '            End If
    '            If IsDBNull(readquery("orderby")) Then
    '            Else

    '                orderby = readquery("orderby")
    '            End If
    '            If IsDBNull(readquery("havingcondition")) Then

    '            Else
    '                havingcondition = readquery("havingcondition")
    '            End If

    '        End While
    '        wheretxt = ""
    '        If formula1 <> "" Then
    '            formula1 = formula1.Replace("$", ".")
    '            Dim boo As Boolean = False
    '            Dim boo1 As Boolean = False
    '            boo = formula1.Contains("'@Date1@'")
    '            boo1 = formula1.Contains("'@Date2@'")
    '            If boo = True Then
    '                If TextBox3.Text = "" Then
    '                    aspnet_msgbox("Fill To Date")
    '                    dataoperation.Visible = False
    '                    ddlReport.Visible = True
    '                    Exit Sub
    '                End If

    '            End If
    '            If boo1 = True Then
    '                If TextBox4.Text = "" Then
    '                    aspnet_msgbox("Fill From Date")
    '                    dataoperation.Visible = False
    '                    ddlReport.Visible = True
    '                    Exit Sub
    '                End If
    '            End If
    '            alertquery1 = "where" & " " & formula1
    '            wheretxt = "where" & " " & formula1
    '            formula1 = formula1.Replace("@Date1@", TextBox3.Text)
    '            formula1 = formula1.Replace("@Date2@", TextBox4.Text)
    '            wheretxt = "where" & " " & formula1
    '        Else

    '            wheretxt = ""
    '        End If
    '        If datecondition <> "" Then
    '            If TextBox3.Text <> "" And TextBox4.Text <> "" Then
    '                If wheretxt = "" Then
    '                    alertquery1 = "where" + " " + datecondition + ".Date >='@Date1@' and " + datecondition + ".Date <='@Date2@'"
    '                    wheretxt = "where" + " " + datecondition + ".Date >='" + TextBox3.Text + "' and " + datecondition + ".Date <='" + TextBox4.Text + "'"
    '                Else
    '                    alertquery1 = alertquery1 + " and " + datecondition + ".Date >='@Date2@' and " + datecondition + ".Date <='@Date2@'"
    '                    wheretxt = wheretxt + " and " + datecondition + ".Date >='" + TextBox3.Text + "' and " + datecondition + ".Date <='" + TextBox4.Text + "'"
    '                End If

    '            End If

    '        End If
    '        If groupby <> "" Then
    '            groupbytext = "group by" & " " & groupby
    '        Else
    '            groupbytext = ""

    '        End If
    '        If orderby <> "" Then
    '            orderbytext = "order by" & " " & orderby
    '        Else
    '            orderbytext = ""

    '        End If
    '        If havingcondition <> "" Then
    '            havingcondition = "having" & " " & havingcondition
    '        Else
    '            havingcondition = ""

    '        End If

    '        con.Close()
    '        readquery.Close()

    '        Dim com1 As New SqlCommand("select tablename from idmsreportmaster where departmentid='" + ddlDepartmant.SelectedValue + "' and clientid='" + clientselect + "' and underlob='" + lobselect + "' and  queryname='" + ddlReport.SelectedItem.Text + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '        con.Open()
    '        readquery = com1.ExecuteReader
    '        While readquery.Read()
    '            tablename = readquery("tablename")

    '        End While
    '        com1.Dispose()
    '        tablename = tablename.Replace("~", ",")
    '        tablearray = tablename.Split(",")
    '        tabcount = UBound(tablearray)
    '        con.Close()
    '        readquery.Close()
    '        Dim colarray
    '        colarray = colname.Split("~")
    '        Dim colcount As Integer
    '        colcount = UBound(colarray)
    '        For allcol = 0 To colcount
    '            If columnname = "" Then
    '                columnname = colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '            Else
    '                columnname = columnname & "," & colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '            End If

    '        Next
    '        Session("Dataanalysis") = "Select " + alertquery + "  from " + tablename + " " + alertquery1 + " " + groupbytext + " " + havingcondition + " " + orderbytext + "#" + tablename + "#" + ddlReport.SelectedItem.Text + ""
    '        '----------------------------------
    '        ''''fix this code'''''''''
    '        For alltabcol = 0 To colcount

    '            tabcolumn = colarray(alltabcol)

    '            final = tabcolumn.Split(".")


    '            tabcolength = UBound(final)

    '            For q = 0 To tabcount
    '                For r = 0 To tabcolength
    '                    Dim p As String
    '                    p = final(tabcolength)
    '                    If final(r) = tablearray(q) Then

    '                        If tname = "" Then
    '                            tname = final(r)
    '                            cname = final(r + 1)

    '                        Else
    '                            tname = final(r) & "," & tname
    '                            cname = cname & "," & final(r + 1)

    '                        End If

    '                    End If
    '                Next
    '            Next
    '        Next

    '        '------------------------
    '        com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '        con.Open()
    '        data = com1.ExecuteReader
    '        While data.Read()
    '            If data("name") = "tabddlReport" & datetime Then
    '                b = False
    '                Exit While

    '            Else
    '                b = True
    '            End If
    '        End While
    '        data.Close()

    '        com1.Dispose()
    '        con.Close()
    '        If b = False Then
    '            com1 = New SqlCommand("select * from  " + "tabddlReport" + datetime + "", con)
    '            con.Open()





    '            da1.SelectCommand = com1
    '            da1.Fill(ds2)

    '            con.Close()
    '            For Each reportcolumn In ds2.Tables(0).Columns

    '                If repcolumn = "" Then
    '                    If reportcolumn.ColumnName() <> "RecordId" Then


    '                        repcolumn = reportcolumn.ColumnName()
    '                    End If
    '                Else
    '                    If reportcolumn.ColumnName() <> "RecordId" Then


    '                        repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '                    End If
    '                End If



    '            Next
    '            repcolarray = repcolumn.Split(",")
    '            repcols.DataSource = repcolarray
    '            repcols.DataBind()
    '            'listcolumns.DataSource = repcolarray
    '            'listcolumns.DataBind()
    '            'ddlcolfrfun.DataSource = repcolarray
    '            'ddlcolfrfun.DataBind()
    '            Session("filtercolumns") = repcolarray
    '            'ddlcolfrfun.Items.Insert(0, "---select---")
    '            Dim divhead As String = ""

    '            com1 = New SqlCommand("select * from  " + "tabddlReport" + datetime + "", con)
    '            con.Open()
    '            data = com1.ExecuteReader
    '            divhead = "<table style='background-color:GradientActiveCaption;border:#336699 1px solid' >"

    '            Dim ub As Integer
    '            ub = UBound(repcolarray)


    '            divhead = divhead & "<tr style='background-color:lightgrey'>"
    '            Dim ubb As String
    '            ubb = (ub + 1).ToString
    '            'divhead = divhead & "<td align=center style=" + "background-color:lightgrey;Font-size:10pt;border:#336699 1px solid; colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
    '            'divhead = divhead & "</td></tr></table>"
    '            divhead = divhead & "</tr>"
    '            htmlreport = "<table  style='background-color:GradientActiveCaption;border:#336699 1px solid' > "
    '            htmlreport = htmlreport & "<caption>" & ddlReport.SelectedItem.Text
    '            htmlreport = htmlreport & "</caption>"
    '            htmlreport = htmlreport & "<tr style='background-color:lightgrey'>"

    '            For no = 0 To ub
    '                If repcolarray(no) <> "RecordId" Then
    '                    htmlreport = htmlreport & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & repcolarray(no) & "</b></td>"
    '                End If

    '            Next
    '            htmlreport = htmlreport & "</tr>"
    '            While data.Read

    '                htmlreport = htmlreport & "<tr>"


    '                For no = 0 To ub
    '                    If repcolarray(no) <> "RecordId" Then

    '                        Dim valuecheck As String = ""
    '                        valuecheck = data(repcolarray(no)).ToString
    '                        If valuecheck = "" Then
    '                            valuecheck = 0
    '                        End If
    '                        htmlreport = htmlreport & "<td style='color:black; border:#336699 1px solid; Font-size:10pt'>"

    '                        htmlreport = htmlreport & valuecheck & "</td>"
    '                    End If
    '                Next
    '                htmlreport = htmlreport & "</tr>"








    '            End While
    '            htmlreport = htmlreport & "</table>"
    '            headdiv.InnerHtml = ""
    '            report.InnerHtml = ""
    '            headdiv.InnerHtml = headdiv.InnerHtml & divhead
    '            report.InnerHtml = report.InnerHtml & htmlreport


    '            con.Close()
    '            Session("table") = "tabddlReport" & datetime
    '        ElseIf b = True Then
    '            If cname <> "" Then
    '                aa1 = cname.Split(",")

    '                ttcount = UBound(tablearray)
    '                cccount = UBound(aa1)
    '                For po = 0 To ttcount

    '                    Try
    '                        currenttable = CType(tablearray(po), String)
    '                        da = New SqlDataAdapter("select * from " + currenttable + "", con1)
    '                        con1.Open()
    '                        da.Fill(ds)
    '                        con1.Close()
    '                    Catch ex As Exception

    '                        aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
    '                        dataoperation.Visible = False
    '                        ddlReport.Visible = True
    '                        Exit Sub
    '                        Exit For
    '                    End Try

    '                    For g2 = 0 To cccount
    '                        currentcolumn = CType(aa1(g2), String)
    '                        For Each colsa In ds.Tables(0).Columns
    '                            If colsa.ColumnName = currentcolumn Then
    '                                If totalquerycloumn = "" Then
    '                                    totalquerycloumn = currentcolumn
    '                                Else
    '                                    totalquerycloumn = totalquerycloumn & "," & currentcolumn
    '                                End If
    '                            End If
    '                        Next
    '                    Next

    '                    ds.Tables(0).Columns.Clear()
    '                    da.Dispose()

    '                Next

    '                con.Close()
    '                If totalquerycloumn <> "" Then
    '                    colsss = totalquerycloumn.Split(",")
    '                    colfinal = UBound(colsss)
    '                End If



    '            End If





    '            Try
    'Session("dataanalysissaveanalysis") = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + "  " + orderbytext + "#" + tablename + "#" + ddlReport.SelectedItem.Text + " "

    '                cmd = New SqlCommand("select Identity(int, 1,1) as RecordId, " + columnname + " into tabddlReport" + datetime + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + "  " + orderbytext + " ", con)
    '                con.Open()

    '                cmd.ExecuteNonQuery()
    '            Catch ex As Exception
    '                Dim strmessage As String = ""
    '                strmessage = Replace(ex.Message.ToString, "'", "")
    '                strmessage = Replace(strmessage, vbCrLf, " ")
    '                aspnet_msgbox(strmessage)
    '                dataoperation.Visible = False
    '                ddlReport.Visible = True
    '                Exit Sub
    '            End Try
    '            con.Close()
    '            com1 = New SqlCommand("select * from " + "tabddlReport" + datetime + "", con)
    '            con.Open()

    '            da1.SelectCommand = com1
    '            da1.Fill(ds2)

    '            For Each reportcolumn In ds2.Tables(0).Columns

    '                If repcolumn = "" Then
    '                    If reportcolumn.ColumnName() <> "RecordId" Then


    '                        repcolumn = reportcolumn.ColumnName()
    '                    End If
    '                Else
    '                    If reportcolumn.ColumnName() <> "RecordId" Then


    '                        repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '                    End If
    '                End If


    '            Next
    '            repcolarray = repcolumn.Split(",")

    '            repcols.DataSource = repcolarray
    '            repcols.DataBind()
    '            con.Close()
    '            'listcolumns.DataSource = repcolarray
    '            'listcolumns.DataBind()
    '            'ddlcolfrfun.DataSource = repcolarray
    '            'ddlcolfrfun.DataBind()
    '            Session("filtercolumns") = repcolarray
    '            'ddlcolfrfun.Items.Insert(0, "---select---")

    '            Dim divhead As String = ""
    '            com1 = New SqlCommand("select * from  " + "tabddlReport" + datetime + "", con)
    '            con.Open()
    '            data = com1.ExecuteReader
    '            divhead = "<table  style='background-color:GradientActiveCaption;border:#336699 1px solid' >"
    '            'divhead = divhead & "<caption>" & ddlReport.SelectedItem.Text
    '            'divhead = divhead & "</caption>"
    '            Dim ub As Integer
    '            ub = UBound(repcolarray)

    '            divhead = divhead & "<tr style='background-color:lightgrey'>"
    '            Dim ubb As String
    '            ubb = (ub + 1).ToString
    '            'divhead = divhead & "<td align=center  colspan= " + ubb + "  style='background-color:lightgrey; border:#336699 1px solid; Font-size:10pt > " & ddlReport.SelectedItem.Text & ""
    '            'divhead = divhead & "</td></tr></table>"
    '            '  divhead = divhead & "</td></tr>"
    '            divhead = divhead & "</tr>"
    '            htmlreport = "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'> "
    '            htmlreport = htmlreport & "<caption>" & ddlReport.SelectedItem.Text
    '            htmlreport = htmlreport & "</caption>"
    '            htmlreport = htmlreport & "<tr>"
    '            For no = 0 To ub

    '                If repcolarray(no) <> "RecordId" Then

    '                    htmlreport = htmlreport & "<td  style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & repcolarray(no) & "</b>" & "</td>"
    '                End If
    '            Next
    '            htmlreport = htmlreport & "</tr>"
    '            While data.Read

    '                htmlreport = htmlreport & "<tr>"


    '                For no = 0 To ub
    '                    If repcolarray(no) <> "RecordId" Then
    '                        Dim valuecheck As String = ""
    '                        valuecheck = data(repcolarray(no)).ToString
    '                        If valuecheck = "" Then
    '                            valuecheck = 0
    '                        End If

    '                        htmlreport = htmlreport & "<td style='color:black; border:#336699 1px solid; Font-size:10pt'>"
    '                        htmlreport = htmlreport & valuecheck & "</td>"
    '                    End If
    '                Next
    '                htmlreport = htmlreport & "</tr>"








    '            End While
    '            htmlreport = htmlreport & "</table>"
    '            headdiv.InnerHtml = ""
    '            report.InnerHtml = ""
    '            headdiv.InnerHtml = headdiv.InnerHtml & divhead
    '            report.InnerHtml = report.InnerHtml & htmlreport

    '            Session("Nextdata") = headdiv.InnerHtml
    '            Session("Nextdata1") = report.InnerHtml





    '            con.Close()

    '        End If

    '        Session("table") = "tabddlReport" & datetime
    '    End If
    '    btnsum.Enabled = True
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
    '        divsavereport.Visible = False
    '        btnsum.Enabled = True
    '    End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    '    Dim strClose As String = ""
    '    strClose = "<Script language='Javascript'>"
    '    strClose = strClose + "window.open('ResultDisplay.aspx','ResultDisplay');"
    '    strClose = strClose + "</Script>"
    '    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    '    Session("repname") = ddlReport.SelectedItem.Text
    '    'Page.ClientScript.RegisterStartupScript(Page.GetType(), "openpopup", "<script language=javascript>window.open('ResultDisplay.aspx','test','height=400px;width=200px');</script>")
    'End Sub
    ''' <summary>
    ''' this function is use to show the message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks>created by: Ranjit Singh created on 20 march 2008</remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' this is use to add the columns from listbox1 to another
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 20 march 2008</remarks>
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Me.result.InnerHtml = ""
        'Me.relax.InnerHtml = ""
        'Me.divFormula.Visible = False
        Dim t1 As String = ddlTable.SelectedItem.Text
        Session("table") = t1

        Dim i As String

        Dim co As Boolean
        co = False
        If repcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        Else

            If selectedcols.Items.Count = 0 Then
                i = repcols.SelectedItem.Text
                selectedcols.Items.Add(i)
                stt = i

                hid.Value = stt
            Else


                For j = 0 To selectedcols.Items.Count - 1
                    If repcols.SelectedItem.Text = selectedcols.Items(j).Text Then
                        aspnet_msgbox("This Column Is Already Selected")
                        Exit Sub
                        co = True
                        Exit For
                    End If
                Next
                If co = False Then

                    i = repcols.SelectedItem.Text
                    selectedcols.Items.Add(i)
                    For j = 0 To selectedcols.Items.Count - 1
                        If stt = "" Then
                            stt = selectedcols.Items(j).Text
                            hid.Value = stt
                        Else
                            stt = selectedcols.Items(j).Text & "," & stt
                            hid.Value = stt
                        End If

                    Next

                End If

            End If

        End If


        p = stt.Split(",")
        p = hid.Value.Split(",")
        Session("colsvalue") = p
        Session("repname") = ddlTable.SelectedItem.Text
    End Sub

    Protected Sub repcols_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles repcols.SelectedIndexChanged

    End Sub

    ''' <summary>
    ''' this is use to remove the columns from listbox 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 21 march 2008</remarks>
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Me.result.InnerHtml = ""
        'Me.relax.InnerHtml = ""
        'Me.divFormula.Visible = False
        If selectedcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        End If


        selectedcols.Items.Remove(selectedcols.SelectedValue)


        For j = 0 To selectedcols.Items.Count - 1
            If stt = "" Then
                stt = selectedcols.Items(j).Text
                hid.Value = stt
            Else
                stt = selectedcols.Items(j).Text & "," & stt
                hid.Value = stt
            End If

        Next
        If selectedcols.Items.Count = 0 Then
            hid.Value = ""
        Else

            p = stt.Split(",")
            p = hid.Value.Split(",")
            Session("colsvalue") = p
        End If

    End Sub

    Protected Sub selectedcols_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectedcols.SelectedIndexChanged

    End Sub

    ''' <summary>
    ''' this is use to show the result of all the formulas
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh created on 22 march 2008</remarks>
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If selectedcols.Items.Count = 0 Then
            aspnet_msgbox("No Column Is Selected")
            Exit Sub
        End If
        result.InnerHtml = ""

        'report.InnerHtml = ""
        'allcolumns.Items.Clear()
        'allcolumns.Items.Insert(0, "---select---")
        'Me.relax.InnerHtml = ""
        'Me.divFormula.Visible = False
        If selectedcols.Items.Count = 0 Then
        Else
            For j = 0 To selectedcols.Items.Count - 1
                If stt = "" Then
                    stt = selectedcols.Items(j).Text
                    hid.Value = stt
                Else
                    stt = stt & "," & selectedcols.Items(j).Text
                    hid.Value = stt
                End If

            Next
        End If
        repcolarray = hid.Value.Split(",")
        If Session("maxval") = "" And Session("maxval1") = "" Then
            aspnet_msgbox("No Formula Is Selected")
            Exit Sub

        Else

            Dim result As String = CType(Session("maxval"), String)
            'Dim result1 As String = CType(Session("maxval1"), String)
            'Dim str As String
            Me.result.InnerHtml = ""
            'result = result & "</table>"
            result = result.Replace("yes", "")
            Me.result.InnerHtml = Me.result.InnerHtml & result
            Session("Nextdata2") = Me.result.InnerHtml
            Dim dtaset As New DataSet
            Dim reportcolumn As DataColumn
            Dim repcolumn As String = ""

            cmd = New SqlCommand("select * from  " + Session("table") + "", con)
            con.Open()





            dataadapter.SelectCommand = cmd
            dataadapter.Fill(dtaset)

            con.Close()
            For Each reportcolumn In dtaset.Tables(0).Columns

                If repcolumn = "" Then
                    If reportcolumn.ColumnName() <> "Recordid" Then


                        repcolumn = reportcolumn.ColumnName()
                    End If
                Else
                    If reportcolumn.ColumnName() <> "Recordid" Then


                        repcolumn = repcolumn & "," & reportcolumn.ColumnName()
                    End If
                End If



            Next

            Dim no As Integer
            Dim tableaiicolumn = repcolumn.Split(",")
            Dim htmlreport As String = ""
            Dim headid As String = ""

            'Session("table")
            cmd = New SqlCommand("select * from  " + Session("table") + "", con)
            con.Open()
            readquery = cmd.ExecuteReader
            headid = "<table style=" + "background-color:GradientActiveCaption;" + "> "
            Dim ub As Integer
            ub = UBound(tableaiicolumn)
            headid = headid & "<tr>"
            Dim ubb As String
            ubb = (ub + 1).ToString
            'headid = headid & "<td align=center style=" + "background-color:pink colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
            'headid = headid & "</td></tr></table>"
            headid = headid & "</tr>"
            htmlreport = "<table  style='background-color:GradientActiveCaption;border:#336699 1px solid' > "
            htmlreport = htmlreport & "<caption>" & ddlTable.SelectedItem.Text
            htmlreport = htmlreport & "</caption>"
            htmlreport = htmlreport & "<tr>"
            For no = 0 To ub

                If tableaiicolumn(no) <> "RecordId" Then

                    htmlreport = htmlreport & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & tableaiicolumn(no) & "</b>" & "</td>"
                End If
            Next
            htmlreport = htmlreport & "</tr>"
            While readquery.Read

                htmlreport = htmlreport & "<tr>"


                For no = 0 To ub
                    Dim valuecheck As String = ""
                    valuecheck = readquery(tableaiicolumn(no)).ToString
                    If valuecheck = "" Then
                        valuecheck = 0
                    End If
                    If tableaiicolumn(no) <> "RecordId" Then


                        htmlreport = htmlreport & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>"
                        htmlreport = htmlreport & valuecheck & "</td>"
                    End If
                Next
                htmlreport = htmlreport & "</tr>"








            End While
            htmlreport = htmlreport & "</table>"
            headdiv.InnerHtml = ""
            report.InnerHtml = ""
            headdiv.InnerHtml = headdiv.InnerHtml & headid
            report.InnerHtml = report.InnerHtml & htmlreport
            Session("Nextdata") = headdiv.InnerHtml
            Session("Nextdata1") = report.InnerHtml
            'strdivreport.Value = report.InnerHtml.ToString






            con.Close()


            'Session("maxval1") = ""
            'Session("maxval") = ""

            Dim strClose As String = ""
            strClose = "<Script language='Javascript'>"
            strClose = strClose + "window.open('ResultDisplay.aspx','ResultDisplay');"
            strClose = strClose + "</Script>"
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
        End If

        Dim typeofuser = Session("typeofuser")
        If (typeofuser.Equals("Super Admin")) Then
            Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
            connection.Open()
            DropDowndept.DataSource = cmd.ExecuteReader()
            DropDowndept.DataTextField = "DepartmentName"
            DropDowndept.DataValueField = "AutoID"
            DropDowndept.DataBind()
        ElseIf (typeofuser.Equals("User")) Then
            Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
            con.Open()
            DropDowndept.DataSource = cmd.ExecuteReader()
            DropDowndept.DataTextField = "DepartmentName"
            DropDowndept.DataValueField = "AutoID"
            DropDowndept.Items.Insert(0, "--Select--")
            DropDowndept.DataBind()
        ElseIf (typeofuser.Equals("Admin")) Then
            Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
            con.Open()
            DropDowndept.DataSource = cmd.ExecuteReader()
            DropDowndept.DataTextField = "DepartmentName"
            DropDowndept.DataValueField = "AutoID"
            DropDowndept.Items.Insert(0, "--Select--")
            DropDowndept.DataBind()
        End If
        DropDowndept.Items.Insert(0, "--select--")
    End Sub

    'Protected Sub openformula_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles openformula.Click
    '    formula.Visible = True
    '    'Dim strClose As String = ""
    '    'strClose = "<Script language='Javascript'>"
    '    'strClose = strClose + "formula_open();"
    '    'strClose = strClose + "</Script>"
    '    'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    'End Sub

    'Protected Sub listcolumns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles listcolumns.SelectedIndexChanged

    'End Sub

    'Protected Sub finalformula_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles finalformula.TextChanged

    'End Sub
    ''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click

    '    finalformula.Text = finalformula.Text + add.Text

    '    filtercolumn = finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub minus_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles minus.Click

    '    finalformula.Text = finalformula.Text + minus.Text
    '    filtercolumn = filtercolumn + finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub multy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles multy.Click

    '    finalformula.Text = finalformula.Text + multy.Text
    '    filtercolumn = filtercolumn + finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub divide_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles divide.Click

    '    finalformula.Text = finalformula.Text + divide.Text
    '    filtercolumn = filtercolumn + finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    '''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub leftb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles leftb.Click

    '    finalformula.Text = finalformula.Text + leftb.Text
    '    filtercolumn = filtercolumn + finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to make formula for filter
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub rightb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rightb.Click

    '    finalformula.Text = finalformula.Text + rightb.Text
    '    filtercolumn = filtercolumn + finalformula.Text
    '    listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to show the report with applied formula 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    '''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub formulafield_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles formulafield.Click
    '    Dim nameofarray = hidfun.Value.Split(",")
    '    Dim st2, st1 As Integer
    '    st2 = UBound(nameofarray)
    '    st1 = 0
    '    For st1 = 0 To st2
    '        If st2 > 0 Then
    '            If finalformula.Text.Contains(nameofarray(st1)) Then
    '                finalformula.Text = finalformula.Text.Replace(nameofarray(st1), nameofarray(st1 + 1))
    '            End If

    '            st1 = st1 + 1
    '        End If

    '    Next
    '    If listcolumns.Items.Count = 0 Then

    '        aspnet_msgbox("Column Field Is Empty")
    '        Exit Sub
    '    End If
    '    Dim cont As String
    '    'Dim cmdd As SqlCommand
    '    Dim formulaoutput As String = ""
    '    If finalformula.Text = "" Then
    '        aspnet_msgbox("No Formula Is Selected")
    '        Exit Sub
    '    End If
    '    'If TextBox2.Text = "" Then
    '    'aspnet_msgbox("Actual Field Should Not Empty")
    '    'Exit Sub
    '    'End If

    '    'If relaxvalue.Text = "" Then
    '    'aspnet_msgbox("Relaxsation Field Should Not Empty")
    '    'Exit Sub
    '    'End If
    '    Dim group_by1 As String = ""
    '    If groupcolumns.Items.Count = 0 Then
    '        group_by1 = ""
    '    Else
    '        For j = 0 To groupcolumns.Items.Count - 1
    '            If group_by1 = "" Then
    '                group_by1 = groupcolumns.Items(j).Text
    '            Else
    '                group_by1 = group_by1 & "," & groupcolumns.Items(j).Text
    '            End If
    '        Next
    '        If group_by1 <> "" Then
    '            group_by1 = "group by" & " " & group_by1
    '        End If

    '    End If
    '    Dim Table As String = ""

    '    If Session("reportname") = "" Then
    '        aspnet_msgbox("No Report Name Is Selected")
    '        Exit Sub
    '    End If
    '    Table = Session("reportname").ToString



    '    If Table = "" Then
    '        aspnet_msgbox("No Report Name Is Selected")
    '        Exit Sub
    '    End If
    '    Dim dr As SqlDataReader
    '    Dim p As Integer = 0
    '    Dim st As Integer = 1
    '    Dim datatype, allrows, allrows1 As String
    '    Dim strClose As String = ""

    '    'filtercolumn = finalformula.Text
    '    'filtercolumn = filtercolumn.Replace("+", ",")
    '    'filtercolumn = filtercolumn.Replace("+", ",")
    '    'filtercolumn = filtercolumn.Replace("-", ",")
    '    'filtercolumn = filtercolumn.Replace("*", ",")
    '    'filtercolumn = filtercolumn.Replace("(", ",")
    '    'filtercolumn = filtercolumn.Replace(")", ",")
    '    'filtercolumn = filtercolumn.Replace("/", ",")
    '    stcolumns = TextBox1.Text
    '    If stcolumns = "" Then
    '        aspnet_msgbox("Formula Is Not Valid")
    '        finalformula.Text = ""
    '        hidfun.Value = ""
    '        Exit Sub
    '    End If
    '    Dim columns = stcolumns.Split(",") 'filtercolumn.Split(",")

    '    Dim i, start, kl, len As Integer
    '    kl = 1
    '    i = UBound(columns)
    '    For start = 0 To i

    '        'If columns(kl) = columns(start) Then
    '        '    Dim s As String
    '        '    s = columns(start)
    '        '    If len = 0 Then
    '        '        len = s.Length
    '        '        len = len + 1
    '        '    Else
    '        '        len = len
    '        '        len = s.Length + len
    '        '        len = len + 1
    '        '    End If


    '        ' columns(start) = columns(start).RemovedControl 'columns(start).Replace(columns(start), "")
    '        'filtercolumn = filtercolumn.Remove(0, len)
    '        'End If
    '    Next
    '    ' columns = filtercolumn.Split(",")
    '    Dim b As Boolean
    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    dr = cmd.ExecuteReader
    '    While dr.Read()
    '        If dr("name") = "R566R" Then
    '            b = False
    '            Exit While

    '        Else
    '            b = True
    '        End If
    '    End While
    '    dr.Close()
    '    con.Close()

    '    If b = False Then


    '        cmd = New SqlCommand("drop table R566R", con)
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()
    '    End If





    '    For start = 0 To i







    '        cmd = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + Table + "' and COLUMN_NAME='" + (columns(start)) + "'", con)
    '        con.Open()
    '        dr = cmd.ExecuteReader

    '        If dr.Read Then
    '            datatype = dr("type")
    '        End If
    '        con.Close()
    '        dr.Close()
    '        If datatype <> "datetime" Then
    '            If allrows = "" Then

    '                allrows1 = "sum" & "(" & columns(start) & ")"
    '                allrows1 = columns(start)
    '                allrows = " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)

    '            Else
    '                allrows1 = allrows1.Replace("sum", "")
    '                allrows1 = allrows1.Replace("(", "")
    '                allrows1 = allrows1.Replace(")", "")
    '                allrows1 = allrows1 & "+" & columns(start)
    '                allrows = allrows & "," & " Convert(numeric," & columns(start) & ")" & " as" & " " & columns(start)

    '            End If
    '        Else
    '            ' aspnet_msgbox("Select Only Numeric Data")
    '            aspnet_msgbox("Select Only Numeric Data")
    '            strClose = "<Script language='Javascript'>"
    '            strClose = strClose + "window.close()"
    '            strClose = strClose + "</Script>"
    '            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    '            Exit Sub
    '        End If

    '    Next
    '    Try
    '        cmd = New SqlCommand("select Identity(int, 1,1) as nocount, " + allrows + "  into R566R from " + Table + "", con)
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()
    '    Catch ex As Exception


    '        aspnet_msgbox("Select Only Numeric Data")
    '        strClose = "<Script language='Javascript'>"
    '        strClose = strClose + "window.close()"
    '        strClose = strClose + "</Script>"
    '        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    '        Exit Sub
    '    End Try
    '    Try




    '        cmd = New SqlCommand("select count(*) as counts from R566R", con)
    '        con.Open()
    '        dr = cmd.ExecuteReader
    '        If dr.Read Then
    '            cont = dr("counts")
    '        End If


    '        con.Close()
    '        dr.Close()
    '        Dim str As String = ""
    '        Dim countnew As Integer
    '        countnew = CType(cont, Integer)
    '        For st = 1 To countnew
    '            Dim nows As String
    '            nows = st.ToString

    '            ''''''''count''''''''''
    '            cmd = New SqlCommand("select " + finalformula.Text + " as  '" + finalformula.Text + "'  from R566R where nocount='" + nows + "' " + group_by1 + " ", con)
    '            con.Open()
    '            dr = cmd.ExecuteReader




    '            If dr.Read Then
    '                Dim checkvalue As String = ""
    '                checkvalue = dr(finalformula.Text).ToString

    '                If formulaoutput = "" Then
    '                    If checkvalue = "" Then
    '                        formulaoutput = 0
    '                    Else
    '                        formulaoutput = dr(finalformula.Text).ToString
    '                    End If

    '                Else
    '                    If checkvalue = "" Then
    '                        formulaoutput = formulaoutput & "," & 0
    '                    Else
    '                        formulaoutput = formulaoutput & "," & dr(finalformula.Text).ToString
    '                    End If

    '                End If
    '            End If
    '            con.Close()
    '            dr.Close()
    '        Next


    '        cmd = New SqlCommand("alter table R566R add Filter numeric ", con)
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()


    '        Dim arrays = formulaoutput.Split(",")
    '        Dim bond, sond As Integer
    '        bond = UBound(arrays)
    '        For sond = 0 To bond
    '            Dim nows As String
    '            nows = (sond + 1).ToString
    '            If arrays(sond) = "" Then
    '                arrays(sond) = 0
    '            End If
    '            cmd = New SqlCommand("update R566R set filter=" + arrays(sond) + " where nocount='" + nows + "'", con)
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            con.Close()
    '            'cmdd = New SqlCommand("update R566R set filter=" + arrays(sond) + " where nocount='" + nows + 1 + "'", con)
    '            'con.Open()
    '            'cmdd.ExecuteNonQuery()
    '            'con.Close()

    '        Next
    '        ' Me.relax.InnerHtml = ""
    '        Dim headdi As String = ""

    '        headdi = headdi & "<table width=100%>"


    '        Dim arr = TextBox1.Text.Split(",")
    '        Dim asd, sdf As Integer
    '        asd = UBound(arr)

    '        Dim colspanvalue As String
    '        colspanvalue = asd + 2.ToString
    '        colspanforgo.Value = colspanvalue
    '        headdi = headdi & "<tr>"

    '        headdi = headdi & "<td colspan=" + colspanvalue + " align=center  style=" + "background-color:pink > " & "FILTER PERCENTAGE"
    '        'headdi = headdi & "</td></tr></tale>"
    '        headdi = headdi & "</td></tr>"
    '        'Divhead.InnerHtml = ""

    '        str = str & "<table width=100% border=2px>"
    '        str = str & "<tr>"
    '        For sdf = 0 To asd
    '            str = str & "<td>  <b>" & arr(sdf) & "</b>"
    '            str = str & "</td>"

    '        Next
    '        str = str & "<td>  <b>" & "Filter" & "</b>"
    '        str = str & "</td>"
    '        str = str & "</tr>"

    '        'Dim actualvalue, actualvalue1, valueout, valueout1, calculatedvalue, morevale As Double
    '        'actualvalue = CType(TextBox2.Text, Double)
    '        'actualvalue1 = CType(relaxvalue.Text, Double)
    '        'valueout = (actualvalue * actualvalue1) / 100

    '        'valueout1 = actualvalue - valueout
    '        For st = 1 To countnew
    '            Dim nows As String
    '            nows = st.ToString


    '            cmd = New SqlCommand("select * from r566r where nocount='" + nows + "'", con)
    '            con.Open()
    '            dr = cmd.ExecuteReader



    '            While dr.Read
    '                str = str & "<tr>"

    '                For sdf = 0 To asd
    '                    Dim chkval As String = ""
    '                    chkval = dr(arr(sdf)).ToString
    '                    If chkval = "" Then
    '                        chkval = 0
    '                    Else
    '                        chkval = dr(arr(sdf)).ToString
    '                    End If

    '                    'Dim stringval As String
    '                    'stringval = dr(finalformula.Text)
    '                    ' calculatedvalue = CType(stringval, Double)
    '                    'morevale = (calculatedvalue * 100) / valueout1
    '                    'If morevale < 60 Then
    '                    str = str & "<td >" & chkval & "</td>"
    '                    ' str = str & "<td style=" + "background-color:red><b>" & morevale & "%" & "</b></td>"
    '                    'ElseIf morevale > 60 Then
    '                    'str = str & "<td style=" + "background-color:green><b>" & dr(finalformula.Text) & "</b></td>"
    '                    ' str = str & "<td style=" + "background-color:green><b>" & morevale & "%" & "</b></td>"
    '                    'ElseIf morevale = 60 Then
    '                    'str = str & "<td style=" + "background-color:yellow><b>" & dr(finalformula.Text) & "</b></td>"
    '                    ' str = str & "<td style=" + "background-color:yellow><b>" & morevale & "%" & "</b></td>"
    '                    'End If

    '                Next
    '                str = str & "<td>" & dr("Filter") & "</td>"

    '                str = str & "</tr>"
    '            End While
    '            con.Close()
    '            dr.Close()

    '        Next
    '        str = str & "</table>"
    '        'If Divhead.Visible = False Then
    '        '    Divhead.Visible = True
    '        'End If
    '        'Divhead.InnerHtml = Divhead.InnerHtml & headdi
    '        'Me.relax.InnerHtml = Me.relax.InnerHtml & str


    '        con.Close()
    '        dr.Close()
    '        finalformula.Text = ""
    '        hidfun.Value = ""
    '        TextBox1.Text = ""
    '    Catch ex As Exception

    '        Dim strmessage As String = ""
    '        strmessage = Replace(ex.Message.ToString, "'", "")
    '        strmessage = Replace(strmessage, vbCrLf, " ")
    '        aspnet_msgbox(strmessage)
    '        dataoperation.Visible = False
    '        ddlReport.Visible = True

    '        ' aspnet_msgbox("Invalid Formula Or Group By Field Is Empty")
    '        strClose = "<Script language='Javascript'>"
    '        strClose = strClose + "window.close()"
    '        strClose = strClose + "</Script>"
    '        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    '        Exit Sub
    '    End Try




    '    ' Dim b As Boolean

    '    ' Dim dr As SqlDataReader
    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    dr = cmd.ExecuteReader
    '    While dr.Read()
    '        If dr("name") = "R566R" Then
    '            b = False
    '            Exit While

    '        Else
    '            b = True
    '        End If
    '    End While
    '    dr.Close()
    '    con.Close()
    '    If b = False Then


    '        dataadapter = New SqlDataAdapter("select * from r566r", con)
    '        con.Open()

    '        Dim ds As New DataSet
    '        dataadapter.Fill(ds)
    '        Dim data As DataColumn
    '        Dim strings As String = ""
    '        con.Close()
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For Each data In ds.Tables(0).Columns
    '                If strings = "" Then

    '                    strings = data.ColumnName()
    '                Else
    '                    strings = strings & "," & data.ColumnName()

    '                End If
    '            Next
    '            Dim columns1 = strings.Split(",")
    '            allcolumns.DataSource = columns1
    '            allcolumns.DataBind()
    '            allcolumns.Items.Remove("nocount")
    '            allcolumns.Items.Insert(0, "---Select---")


    '            'ddlcolfrfun.DataSource = columns1
    '            'ddlcolfrfun.DataBind()
    '            'ddlcolfrfun.Items.Remove("nocount")
    '            'ddlcolfrfun.Items.Insert(0, "---Select---")

    '            ' allcolumns.Visible = True
    '            'inde.Visible = True
    '        End If
    '    End If

    '    'If relax.InnerHtml = "" Then
    '    '    relax.Visible = False
    '    'Else
    '    '    relax.Visible = True
    '    'End If

    'End Sub

    'Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    'End Sub
    ''' <summary>
    '''this is use to clear the formula field
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles clear.Click
    '    If finalformula.Text = "" Then
    '        aspnet_msgbox("Formula Is Already Empty")
    '        Exit Sub
    '    End If
    '    hidfun.Value = ""
    '    TextBox1.Text = ""
    '    finalformula.Text = ""
    '    listcolumns.SelectedIndex = -1
    'End Sub


    ''' <summary>
    '''this is use to enable or disable the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub btnFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Click
    '    If repcols.Items.Count > 0 Then
    '        Response.Redirect("FilterPercentage.aspx?val=" + "3")
    '    Else
    '        aspnet_msgbox("First Make The Report")
    '    End If

    '    'Me.divFormula.Visible = True
    '    'Me.tdrang.Visible = False
    'End Sub

    'Protected Sub btnFilter_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.Load

    'End Sub

    'Protected Sub btnFilter_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFilter.PreRender

    'End Sub
    ''' <summary>
    '''this is use to insert the date into the textbox'''''fix this code changes has done
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Calendar1.SelectionChanged
        'Me.result.InnerHtml = ""
        'Me.relax.InnerHtml = ""
        'Me.divFormula.Visible = False
        Me.Calendar1.Visible = False

        If Me.stCal.Value = "cal1" Then
            TextBox3.Text = Calendar1.SelectedDate.Date
        Else
            TextBox4.Text = Calendar1.SelectedDate.Date
        End If
    End Sub
    ''' <summary>
    '''this is use to show the calender
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    Me.Calendar1.Visible = True
    '    Me.stCal.Value = "cal1"
    '    Me.Calendar1.SelectedDates.Clear()
    'End Sub
    ''' <summary>
    '''this is use to show the calender
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub ImageButton2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    Me.Calendar1.Visible = True
    '    Me.stCal.Value = "cal2"
    '    Me.Calendar1.SelectedDates.Clear()
    'End Sub

    ' Protected Sub btnsum_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnsum.Click

    'finalformula.Text = finalformula.Text + btnsum.Text

    'filtercolumn = finalformula.Text
    'listcolumns.SelectedIndex = -1
    'End Sub

    ' Protected Sub btnmin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmin.Click
    'finalformula.Text = finalformula.Text + btnmin.Text

    'filtercolumn = finalformula.Text
    'listcolumns.SelectedIndex = -1
    ' End Sub

    ' Protected Sub btnmax_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmax.Click
    'finalformula.Text = finalformula.Text + btnmax.Text

    'filtercolumn = finalformula.Text
    'listcolumns.SelectedIndex = -1
    ' End Sub

    'Protected Sub btnavg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnavg.Click
    'finalformula.Text = finalformula.Text + btnavg.Text

    'filtercolumn = finalformula.Text
    'listcolumns.SelectedIndex = -1
    'End Sub

    'Protected Sub btncount_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btncount.Click
    'finalformula.Text = finalformula.Text + btncount.Text

    'filtercolumn = finalformula.Text
    'listcolumns.SelectedIndex = -1
    'End Sub
    ''' <summary>
    '''this is use to display the columns in dropdownlist and enable disable the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub setfrm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles setfrm.Click
    '    'If relax.InnerHtml = "" Then
    '    '    allcolumns.Visible = False
    '    '    inde.Visible = False
    '    '    trcols1.Visible = False
    '    '    trcols.Visible = False
    '    '    aspnet_msgbox("First Make The Report")
    '    '    Exit Sub
    '    'Else
    '    '    trcols1.Visible = True
    '    '    trcols.Visible = True
    '    '    allcolumns.Visible = True
    '    '    inde.Visible = True
    '    'End If


    '    If listcolumns.Items.Count = 0 Then


    '        allcolumns.Items.Clear()
    '        allcolumns.Items.Insert(0, "---select---")
    '        aspnet_msgbox("Column Field Is Empty")
    '        Exit Sub
    '    End If
    '    Dim b As Boolean

    '    Dim dr As SqlDataReader
    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    dr = cmd.ExecuteReader
    '    While dr.Read()
    '        If dr("name") = "R566R" Then
    '            b = False
    '            Exit While

    '        Else
    '            b = True
    '        End If
    '    End While
    '    dr.Close()
    '    con.Close()
    '    If b = False Then


    '        dataadapter = New SqlDataAdapter("select * from r566r", con)
    '        con.Open()

    '        Dim ds As New DataSet
    '        dataadapter.Fill(ds)
    '        Dim data As DataColumn
    '        Dim strings As String = ""
    '        con.Close()
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For Each data In ds.Tables(0).Columns
    '                If strings = "" Then

    '                    strings = data.ColumnName()
    '                Else
    '                    strings = strings & "," & data.ColumnName()

    '                End If
    '            Next
    '            Dim columns = strings.Split(",")
    '            allcolumns.DataSource = columns
    '            allcolumns.DataBind()
    '            allcolumns.Items.Remove("nocount")
    '            allcolumns.Items.Insert(0, "---Select---")
    '            'ddlcolfrfun.DataSource = columns
    '            'ddlcolfrfun.DataBind()
    '            'ddlcolfrfun.Items.Remove("nocount")
    '            'ddlcolfrfun.Items.Insert(0, "---Select---")



    '            allcolumns.Visible = True
    '            inde.Visible = True
    '        End If
    '    Else
    '        'If relax.InnerHtml <> "" Then
    '        '    aspnet_msgbox("Make The Filtered Report Again")
    '        '    relax.InnerHtml = ""
    '        '    Exit Sub
    '        'End If
    '        allcolumns.Items.Clear()
    '        allcolumns.Items.Insert(0, "---Select---")
    '        ddlcolfrfun.Items.Clear()
    '        ddlcolfrfun.Items.Insert(0, "---select---")
    '        aspnet_msgbox("First Make The Report")
    '        Exit Sub
    '        'If relax.InnerHtml = "" Then
    '        '    aspnet_msgbox("First Make The Report")
    '        '    Exit Sub
    '        'End If

    '    End If




    'End Sub
    ''' <summary>
    '''this is use to enable disable the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub allcolumns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles allcolumns.SelectedIndexChanged
    '    If selectionformula.SelectedItem.Text = "Between" Then
    '        valueinput.Visible = False
    '        tdrang.Visible = True
    '    Else
    '        tdrang.Visible = False
    '        valueinput.Visible = True
    '    End If
    '    go.Visible = True
    '    selectionformula.Visible = True
    '    'valueinput.Visible = True
    'End Sub
    ''' <summary>
    '''this is use to display the filtered report after applying the selecting the formula from dropdownlist
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub go_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles go.Click
    '    If selectionformula.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("No Formula Is Selected")
    '        Exit Sub
    '    End If
    '    If allcolumns.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("No Column Is Selected")
    '        Exit Sub
    '    End If
    '    'If Me.relax.InnerHtml = "" Then
    '    '    aspnet_msgbox("First Make The Report")
    '    '    Exit Sub
    '    'End If
    '    If selectionformula.SelectedItem.Text = "Between" Then
    '        If txtfrm.Text = "" Then
    '            aspnet_msgbox("Fill From Data")
    '            Exit Sub

    '        End If

    '        If Textto.Text = "" Then
    '            aspnet_msgbox("Fill To Data")
    '            Exit Sub
    '        End If

    '    End If
    '    If selectionformula.SelectedItem.Text <> "Between" Then
    '        If valueinput.Text = "" Then
    '            aspnet_msgbox("Fill Value For Selected Formula")
    '            Exit Sub

    '        End If
    '    End If
    '    If allcolumns.SelectedItem.Text = "---Select---" Then
    '        aspnet_msgbox("Select Column First")
    '        Exit Sub
    '    Else

    '        Dim columns
    '        dataadapter = New SqlDataAdapter("select * from r566r", con)
    '        con.Open()

    '        Dim ds As New DataSet
    '        dataadapter.Fill(ds)
    '        Dim data As DataColumn
    '        Dim strings As String = ""
    '        con.Close()
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For Each data In ds.Tables(0).Columns
    '                If strings = "" Then

    '                    strings = data.ColumnName()
    '                Else
    '                    strings = strings & "," & data.ColumnName()

    '                End If
    '            Next
    '            columns = strings.Split(",")

    '        End If

    '        Dim str As String = ""
    '        Dim headid As String = ""
    '        headid = headid & "<table width=100%>"
    '        ' Dim arr = TextBox1.Text.Split(",")
    '        Dim asd, sdf As Integer
    '        asd = UBound(columns)




    '        headid = headid & "<tr>"

    '        headid = headid & "<td align=center style=" + "background-color:pink colspan=" + colspanforgo.Value + ">  <b>" & "FILTER PERCENTAGE" & "</b>"
    '        headid = headid & "</td></tr></table>"
    '        str = str & "<table width=100% border=2px>"
    '        str = str & "<tr>"
    '        For sdf = 0 To asd
    '            If columns(sdf) <> "nocount" Then


    '                str = str & "<td>  <b>" & columns(sdf) & "</b>"
    '                str = str & "</td>"
    '            End If
    '        Next
    '        If selectionformula.SelectedItem.Text = "Greater Than" Then


    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " >'" + valueinput.Text + "'", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        ElseIf selectionformula.SelectedItem.Text = "Less Than" Then
    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " <'" + valueinput.Text + "'", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        ElseIf selectionformula.SelectedItem.Text = "Equal To" Then
    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " ='" + valueinput.Text + "'", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        ElseIf selectionformula.SelectedItem.Text = "Starts With" Then
    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " like( '" + valueinput.Text + "%')", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        ElseIf selectionformula.SelectedItem.Text = "Ends With" Then
    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + "  like( '%" + valueinput.Text + "')", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        ElseIf selectionformula.SelectedItem.Text = "Between" Then
    '            cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + "  between '" + txtfrm.Text + "' and '" + Textto.Text + "'", con)
    '            con.Open()
    '            readquery = cmd.ExecuteReader
    '        End If

    '        Try
    '            While readquery.Read
    '                str = str & "<tr>"

    '                For sdf = 0 To asd
    '                    If columns(sdf) <> "nocount" Then


    '                        'Dim stringval As String
    '                        'stringval = dr(finalformula.Text)
    '                        ' calculatedvalue = CType(stringval, Double)
    '                        'morevale = (calculatedvalue * 100) / valueout1
    '                        'If morevale < 60 Then
    '                        str = str & "<td>" & readquery(columns(sdf)) & "</td>"
    '                        ' str = str & "<td style=" + "background-color:red><b>" & morevale & "%" & "</b></td>"
    '                        'ElseIf morevale > 60 Then
    '                        'str = str & "<td style=" + "background-color:green><b>" & dr(finalformula.Text) & "</b></td>"
    '                        ' str = str & "<td style=" + "background-color:green><b>" & morevale & "%" & "</b></td>"
    '                        'ElseIf morevale = 60 Then
    '                        'str = str & "<td style=" + "background-color:yellow><b>" & dr(finalformula.Text) & "</b></td>"
    '                        ' str = str & "<td style=" + "background-color:yellow><b>" & morevale & "%" & "</b></td>"
    '                        'End If
    '                    End If

    '                Next
    '                ' str = str & "<td style=" + "background-color:red><b>" & dr("Filter") & "</b></td>"

    '                str = str & "</tr>"
    '            End While
    '            'If columns(sdf) <> "nocount" Then

    '            con.Close()
    '            readquery.Close()
    '            'Me.relax.InnerHtml = ""
    '            str = str & "</table>"
    '            'Divhead.InnerHtml = ""
    '            'Divhead.InnerHtml = Divhead.InnerHtml & headid
    '            'Me.relax.InnerHtml = Me.relax.InnerHtml & str
    '        Catch ex As Exception
    '            valueinput.Text = ""
    '            Dim strmessage As String = ""
    '            strmessage = Replace(ex.Message.ToString, "'", "")
    '            strmessage = Replace(strmessage, vbCrLf, " ")
    '            aspnet_msgbox(strmessage)
    '            dataoperation.Visible = False
    '            ddlReport.Visible = True
    '            Exit Sub
    '        End Try
    '    End If
    'End Sub


    ''' <summary>
    '''this is use to make the formula with columns selected columns is inserted in textbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub formulafilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles formulafilter.Click
    '    If listcolumns.Items.Count = 0 Then
    '        aspnet_msgbox("List Is Empty")
    '        Exit Sub
    '    End If
    '    If listcolumns.SelectedIndex = -1 Then
    '        aspnet_msgbox("Select Column First")
    '        Exit Sub
    '    Else

    '        finalformula.Text = finalformula.Text + listcolumns.SelectedItem.Text
    '        Dim nowcol As String = ""
    '        nowcol = listcolumns.SelectedItem.Text
    '        If TextBox1.Text = "" Then
    '            TextBox1.Text = listcolumns.SelectedItem.Text
    '        End If
    '        Dim b As Boolean = False
    '        Dim columns = TextBox1.Text.Split(",")
    '        Dim colcount, colincrement As Integer
    '        colcount = UBound(columns)
    '        For colincrement = 0 To colcount


    '            If columns(colincrement) = nowcol Then
    '                Exit Sub
    '                b = False
    '            Else
    '                b = True

    '            End If

    '        Next
    '        If b = True Then
    '            TextBox1.Text = TextBox1.Text & "," & nowcol
    '        End If
    '        listcolumns.SelectedIndex = -1
    '    End If
    'End Sub

    'Protected Sub groupcolumns_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles groupcolumns.SelectedIndexChanged


    'End Sub

    ''' <summary>
    '''this is use to make the group by formula with columns selected columns is inserted in listbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub btngrp_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngrp.Click
    '    Dim i As String

    '    Dim co As Boolean
    '    co = False
    '    If listcolumns.Items.Count = 0 Then
    '        aspnet_msgbox("List Is Empty")
    '        Exit Sub
    '    End If
    '    If listcolumns.SelectedIndex = -1 Then
    '        aspnet_msgbox("Select Column First")
    '        Exit Sub
    '    Else
    '        If groupcolumns.Items.Count = 0 Then
    '            i = listcolumns.SelectedItem.Text
    '            groupcolumns.Items.Add(i)

    '        Else


    '            For j = 0 To groupcolumns.Items.Count - 1
    '                If listcolumns.SelectedItem.Text = groupcolumns.Items(j).Text Then
    '                    aspnet_msgbox("This Column Is Already Selected")
    '                    Exit Sub
    '                    co = True
    '                    Exit For
    '                End If
    '            Next
    '            If co = False Then

    '                i = listcolumns.SelectedItem.Text
    '                groupcolumns.Items.Add(i)
    '                'For j = 0 To groupcolumns.Items.Count - 1
    '                'If stt = "" Then
    '                'stt = groupcolumns.Items(j).Text
    '                'hid.Value = stt
    '                ' Else
    '                'stt = groupcolumns.Items(j).Text & "," & stt
    '                ' hid.Value = stt
    '            End If

    '            'Next

    '        End If
    '    End If

    'End Sub

    ''' <summary>
    '''this is use to remove the columns from group by listbox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub Btnrmv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnrmv.Click
    '    If groupcolumns.Items.Count = 0 Then
    '        aspnet_msgbox("List Is Already Empty")
    '        Exit Sub
    '    End If
    '    groupcolumns.Items.Clear()

    'End Sub

    ''' <summary>
    '''this is use to make the report indexed according to the selected column
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub inde_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles inde.Click
    '    If allcolumns.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("No Column Is Selected")
    '        Exit Sub
    '    End If
    '    'If Me.relax.InnerHtml = "" Then
    '    '    aspnet_msgbox("First Make The Report")
    '    '    Exit Sub
    '    'End If
    '    If allcolumns.SelectedItem.Text = "---Select---" Then
    '        aspnet_msgbox("Select Column First")
    '        Exit Sub
    '    Else

    '        Dim columns
    '        dataadapter = New SqlDataAdapter("select * from r566r", con)
    '        con.Open()

    '        Dim ds As New DataSet
    '        dataadapter.Fill(ds)
    '        Dim data As DataColumn
    '        Dim strings As String = ""
    '        con.Close()
    '        If ds.Tables(0).Rows.Count > 0 Then
    '            For Each data In ds.Tables(0).Columns
    '                If strings = "" Then

    '                    strings = data.ColumnName()
    '                Else
    '                    strings = strings & "," & data.ColumnName()

    '                End If
    '            Next
    '            columns = strings.Split(",")

    '        End If
    '        'Me.relax.InnerHtml = ""
    '        Dim str As String = ""
    '        Dim headid As String = ""
    '        headid = headid & "<table width=100% >"
    '        ' Dim arr = TextBox1.Text.Split(",")
    '        Dim asd, sdf As Integer
    '        asd = UBound(columns)




    '        headid = headid & "<tr>"

    '        headid = headid & "<td  align=center style=" + "background-color:pink colspan=" + colspanforgo.Value + ">  <b>" & "FILTER PERCENTAGE" & "</b>"
    '        headid = headid & "</td></tr>"
    '        headid = headid & "</table>"
    '        str = str & "<table width=100% border=2px>"
    '        str = str & "<tr>"
    '        For sdf = 0 To asd
    '            If columns(sdf) <> "nocount" Then


    '                str = str & "<td>  <b>" & columns(sdf) & "</b>"
    '                str = str & "</td>"
    '            End If
    '        Next
    '        ' If selectionformula.SelectedItem.Text = "Greater Than" Then


    '        cmd = New SqlCommand("select * from r566r order by " + allcolumns.SelectedItem.Text + "", con)
    '        con.Open()
    '        readquery = cmd.ExecuteReader
    '        'ElseIf selectionformula.SelectedItem.Text = "Less Than" Then
    '        ' cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " <'" + valueinput.Text + "'", con)
    '        ' con.Open()
    '        ' readquery = cmd.ExecuteReader
    '        'ElseIf selectionformula.SelectedItem.Text = "Equal To" Then
    '        ' cmd = New SqlCommand("select * from r566r where " + allcolumns.SelectedItem.Text + " ='" + valueinput.Text + "'", con)
    '        ' con.Open()
    '        ' readquery = cmd.ExecuteReader
    '        'End If


    '        While readquery.Read
    '            str = str & "<tr>"

    '            For sdf = 0 To asd

    '                If columns(sdf) <> "nocount" Then


    '                    'Dim stringval As String
    '                    'stringval = dr(finalformula.Text)
    '                    ' calculatedvalue = CType(stringval, Double)
    '                    'morevale = (calculatedvalue * 100) / valueout1
    '                    'If morevale < 60 Then
    '                    str = str & "<td >" & readquery(columns(sdf)) & "</td>"
    '                    ' str = str & "<td style=" + "background-color:red><b>" & morevale & "%" & "</b></td>"
    '                    'ElseIf morevale > 60 Then
    '                    'str = str & "<td style=" + "background-color:green><b>" & dr(finalformula.Text) & "</b></td>"
    '                    ' str = str & "<td style=" + "background-color:green><b>" & morevale & "%" & "</b></td>"
    '                    'ElseIf morevale = 60 Then
    '                    'str = str & "<td style=" + "background-color:yellow><b>" & dr(finalformula.Text) & "</b></td>"
    '                    ' str = str & "<td style=" + "background-color:yellow><b>" & morevale & "%" & "</b></td>"
    '                    'End If
    '                End If
    '            Next
    '            ' str = str & "<td style=" + "background-color:red><b>" & dr("Filter") & "</b></td>"

    '            str = str & "</tr>"
    '        End While
    '        con.Close()
    '        readquery.Close()
    '        str = str & "</table>"
    '        'Divhead.InnerHtml = ""
    '        'Divhead.InnerHtml = Divhead.InnerHtml & headid
    '        'Me.relax.InnerHtml = Me.relax.InnerHtml & str
    '    End If
    'End Sub
    ''' <summary>
    '''this is use to enable and disable the controls
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub selectionformula_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles selectionformula.SelectedIndexChanged
    '    If selectionformula.SelectedItem.Text = "Between" Then
    '        valueinput.Visible = False
    '        tdrang.Visible = True
    '    Else
    '        tdrang.Visible = False
    '        valueinput.Visible = True
    '    End If
    'End Sub



    ''' <summary>
    '''this is use to save the analyzed report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
    '    If textreportname.Text.Contains("(") Or textreportname.Text.Contains(")") Or textreportname.Text.Contains("~") Or textreportname.Text.Contains("`") Or textreportname.Text.Contains("$") Or textreportname.Text.Contains("#") Or textreportname.Text.Contains("@") Or textreportname.Text.Contains("!") Or textreportname.Text.Contains("%") Or textreportname.Text.Contains("^") Or textreportname.Text.Contains("&") Or textreportname.Text.Contains("*") Or textreportname.Text.Contains("-") Or textreportname.Text.Contains("+") Or textreportname.Text.Contains(":") Or textreportname.Text.Contains(";") Or textreportname.Text.Contains(",") Or textreportname.Text.Contains(".") Or textreportname.Text.Contains("/") Or textreportname.Text.Contains("?") Or textreportname.Text.Contains("|") Or textreportname.Text.Contains("\") Or textreportname.Text.Contains("{") Or textreportname.Text.Contains("}") Or textreportname.Text.Contains("[") Or textreportname.Text.Contains("]") Or textreportname.Text.Contains(" ") Or textreportname.Text.Contains("<") Or textreportname.Text.Contains(">") Then

    '        aspnet_msgbox("No Special Symbol Is Allowed")
    '        Exit Sub
    '    End If
    '    'If DropDowndept.SelectedItem.Text = "---select---" Then
    '    '    aspnet_msgbox("Select Department")
    '    '    Exit Sub
    '    'End If
    '    Dim repname As String = ""
    '    cmd = New SqlCommand("select ReportName as ReportName from dataanalysishtmlreport", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    While readquery.Read()

    '        repname = readquery("ReportName")
    '        If repname = textreportname.Text Then
    '            aspnet_msgbox("Choose Another Report Name")
    '            textreportname.Focus()
    '            Exit Sub
    '        End If

    '    End While
    '    readquery.Close()
    '    con.Close()

    '    If textreportname.Text = "" Then
    '        aspnet_msgbox("Fill Report Name")
    '        textreportname.Focus()
    '        Exit Sub
    '    End If
    '    strdivreport.Value = report.InnerHtml.ToString
    '    If strdivreport.Value <> "" Then
    '        'txtstrdiv.Value = strpretags & txtstrdiv.Value & strposttags
    '        Dim str As String = ""
    '        Dim fp As StreamWriter
    '        If Not Directory.Exists(Server.MapPath("html/")) Then
    '            '<----------------------Creating Directory for partcular user--------------------------------->
    '            Directory.CreateDirectory(Server.MapPath("html/"))
    '            '<----------------------End of Creating Directory for partcular user------------------------>
    '        End If
    '        '<------------------------End of Creating A main Directory--------------------------------------->


    '        Dim Path = "html/" & textreportname.Text & ".html"
    '        '<--------------------Creating a new text file---------------------------------->
    '        fp = File.CreateText(Server.MapPath(Path))
    '        'change
    '        fp.WriteLine(result.InnerHtml)
    '        fp.WriteLine(headdiv.InnerHtml)
    '        fp.WriteLine(strdivreport.Value)
    '        'fp.WriteLine(Divhead.InnerHtml)
    '        'fp.WriteLine(relax.InnerHtml)



    '        fp.Close()
    '    End If
    '    Dim deptval As String = ""
    '    Dim clientval As String = ""
    '    Dim lobval As String = ""
    '    'deptval = DropDowndept.SelectedValue.ToString
    '    'clientval = DropDownclient.SelectedValue.ToString
    '    'lobval = DropDownlob.SelectedValue.ToString
    '    'If DropDowndept.SelectedItem.Text = "---select---" Then
    '    '    aspnet_msgbox("Select Department First")
    '    '    Exit Sub
    '    'End If
    '    'If DropDownclient.SelectedItem.Text = "---select---" Then

    '    '    DropDownclient.SelectedItem.Text = "0"
    '    '    clientval = "0"

    '    '    DropDownlob.SelectedItem.Text = "0"
    '    '    lobval = "0"
    '    'End If
    '    'If DropDownlob.SelectedItem.Text = "---select---" Or DropDownlob.SelectedItem.Text = "0" Then
    '    '    DropDownlob.SelectedItem.Text = "0"
    '    '    lobval = "0"
    '    'End If
    '    'Dim localstatus As String = ""
    '    'If chklocal.Checked = True Then
    '    '    localstatus = "Local"
    '    'Else
    '    '    localstatus = "NonLocal"
    '    'End If
    '    cmd = New SqlCommand("insert into dataanalysishtmlreport values(@DeptName,@DeptId,@ClientName,@ClientId,@LobName,@LobId,@ReportName,@SavedBy,@repname,@local)", con)
    '    cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@DeptName").Value = DropDowndept.SelectedItem.Text
    '    cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@DeptId").Value = deptval
    '    cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@ClientName").Value = DropDownclient.SelectedItem.Text
    '    cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@ClientId").Value = clientval
    '    cmd.Parameters.Add("@LobName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@LobName").Value = DropDownlob.SelectedItem.Text
    '    cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@LobId").Value = lobval
    '    cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@ReportName").Value = textreportname.Text
    '    cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@SavedBy").Value = Session("userid")
    '    cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@repname").Value = ddlTable.SelectedItem.Text
    '    cmd.Parameters.Add("@local", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@local").Value = localstatus
    '    '
    '    con.Open()
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    '    '''''''''''''''Usertype check for track goes here:- By Suvidha

    '    Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where HTMLReport='" + textreportname.Text + "' and Action='Save' and Entity='HTMLReport'", con)
    '    con.Open()
    '    cmm.ExecuteNonQuery()
    '    con.Close()
    '    '''''''''''''''Usertype check for track goes here:- By Suvidha

    '    aspnet_msgbox("Report Has Been Saved Successfully")
    '    divsavereport.Visible = False
    '    'chklocal.Checked = False

    '    'If DropDownclient.SelectedItem.Text = "0" Then

    '    '    DropDownclient.SelectedItem.Text = "---select---"



    '    'End If
    '    'If DropDownlob.SelectedItem.Text = "0" Then
    '    '    DropDownlob.SelectedItem.Text = "---select---"

    '    'End If
    '    strdivreport.Value = ""
    '    'DropDowndept.SelectedIndex = 0
    '    'DropDownclient.Items.Clear()
    '    'DropDownlob.Items.Clear()
    '    'cmd = New SqlCommand("drop table R566R", con)
    '    'con.Open()
    '    'cmd.ExecuteNonQuery()
    '    'con.Close()

    '    'groupcolumns.Items.Clear()
    '    'allcolumns.Items.Clear()
    '    textreportname.Text = ""
    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'report.InnerHtml = ""

    'End Sub

    ''' <summary>
    '''this is use select the client according to the department
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub DropDowndept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDowndept.SelectedIndexChanged

    '    If DropDowndept.SelectedItem.Text = "---select---" Then
    '        DropDownclient.Items.Clear()
    '        DropDownclient.Items.Insert(0, "---select---")
    '        DropDownlob.Items.Clear()
    '        DropDownlob.Items.Insert(0, "---select---")
    '    Else
    '        If Session("typeofuser") = "Admin" Then


    '            Dim classobj As New Functions
    '            DropDownclient.DataTextField = "clientname"
    '            DropDownclient.DataValueField = "autoid"
    '            DropDownclient.DataSource = classobj.bind_client(DropDowndept.SelectedValue)
    '            DropDownclient.DataBind()
    '            DropDownclient.Items.Insert(0, "---select---")

    '            DropDownlob.Items.Clear()
    '            DropDownlob.Items.Insert(0, "---select---")
    '        End If
    '        If Session("typeofuser") = "User" Then


    '            Dim classobj As New Functions
    '            DropDownclient.DataTextField = "clientname"
    '            DropDownclient.DataValueField = "autoid"
    '            DropDownclient.DataSource = classobj.bind_client(DropDowndept.SelectedValue)
    '            DropDownclient.DataBind()
    '            DropDownclient.Items.Insert(0, "---select---")

    '            DropDownlob.Items.Clear()
    '            DropDownlob.Items.Insert(0, "---select---")
    '        End If

    '    End If
    'End Sub
    ''' <summary>
    '''this is use to select the lob according to the client
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub DropDownclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownclient.SelectedIndexChanged
    '    If DropDownclient.SelectedItem.Text = "---select---" Then
    '        DropDownlob.Items.Clear()
    '        DropDownlob.Items.Insert(0, "---select---")

    '    Else

    '        Dim classobj As New Functions

    '        If Session("typeofuser") = "Admin" Then




    '            DropDownlob.DataTextField = "lobname"
    '            DropDownlob.DataValueField = "autoid"
    '            DropDownlob.DataSource = classobj.bind_lob(DropDowndept.SelectedValue, DropDownclient.SelectedValue)
    '            DropDownlob.DataBind()
    '            DropDownlob.Items.Insert(0, "---select---")
    '        End If
    '        If Session("typeofuser") = "User" Then




    '            DropDownlob.DataTextField = "lobname"
    '            DropDownlob.DataValueField = "autoid"
    '            DropDownlob.DataSource = classobj.bind_lob(DropDowndept.SelectedValue, DropDownclient.SelectedValue)
    '            DropDownlob.DataBind()
    '            DropDownlob.Items.Insert(0, "---select---")
    '        End If

    '    End If
    'End Sub

    'Protected Sub DropDownlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownlob.SelectedIndexChanged

    'End Sub
    ''' <summary>
    '''this is use generate the report with old data from table which is already saved in table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub old_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles old.Click

    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    'selectedcols.Items.Clear()
    '    'hid.Value = ""
    '    'If ddlDepartmant.SelectedValue = "" Or ddlDepartmant.SelectedItem.Text = "---select---" Then
    '    '   aspnet_msgbox("Select Department First")

    '    'ElseIf ddlReport.SelectedValue = "" Then
    '    '  aspnet_msgbox("Select Report First")
    '    '   repcols.Items.Clear()
    '    'ElseIf ddlReport.SelectedItem.Text = "---select---" Then
    '    '    aspnet_msgbox("Select Report First")
    '    ' Else



    '    '=====================''''''''''''''''''''''''''

    '    Dim bo As Boolean






    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    While readquery.Read()
    '        If readquery("name") = ddlTable.SelectedItem.Text Then
    '            bo = False
    '            Exit While

    '        Else
    '            bo = True
    '        End If
    '    End While
    '    readquery.Close()
    '    con.Close()

    '    If bo = False Then




    '        Session("AnalysisQuery") = "Select * from  " + ddlTable.SelectedItem.Text + "#" + ddlTable.SelectedItem.Text + "#" + ddlTable.SelectedItem.Text + ""

    '        Session("Dataanalysis") = ""
    '        cmd = New SqlCommand("select * from  " + ddlTable.SelectedItem.Text + "", con)
    '        con.Open()



    '        Dim datasetaa As New DataSet

    '        dataadapter.SelectCommand = cmd
    '        dataadapter.Fill(datasetaa)

    '        con.Close()
    '        Dim culmnaaa As DataColumn
    '        Dim strclocheck As String
    '        For Each culmnaaa In datasetaa.Tables(0).Columns


    '            strclocheck = culmnaaa.ColumnName()
    '            If strclocheck.StartsWith("Accu_Sum") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '            ElseIf strclocheck.StartsWith("Column_Percentage_") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()

    '            ElseIf strclocheck.StartsWith("Column_Sum_Percentage") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '            ElseIf strclocheck.StartsWith("Column_Sum") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '                'End If
    '            ElseIf strclocheck.StartsWith("Row_Percentage_") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '            ElseIf strclocheck.StartsWith("Row_Sum_") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '            ElseIf strclocheck.Contains("Filter") Then
    '                cmd = New SqlCommand("alter table " + ddlTable.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '            End If




    '        Next
    '    Else
    '        aspnet_msgbox("Analysis Table Has Been Deleted")
    '        'dataoperation.Visible = False
    '        ddlTable.Visible = True
    '        Exit Sub
    '    End If



    '    ''''''''''''''''''============================='''''''''''''''''''''''''''''''''


    '    'Dim queryname As String = ""


    '    'cmd = New SqlCommand("select ReportName from SavedAnalysis where analysisname='" + ddlReport.SelectedItem.Text + "'", con)
    '    'con.Open()
    '    'readquery = cmd.ExecuteReader
    '    'While readquery.Read()
    '    '    queryname = readquery("ReportName")

    '    'End While
    '    'con.Close()
    '    'readquery.Close()


    '    'Dim com As New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + queryname + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '    'con.Open()
    '    Dim columnname, cname, tabcolumn, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, repcolumn, str22, str12, totalquerycloumn As String
    '    'Dim aa1
    '    'Dim colsss
    '    'Dim b As Boolean
    '    Dim da As New SqlDataAdapter
    '    Dim da1 As New SqlDataAdapter
    '    'Dim cmd As SqlCommand
    '    Dim ds As New DataSet
    '    Dim colfinal As Integer
    '    Dim ttcount, cccount, p2, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer
    '    Dim ds2 As New DataSet
    '    Dim final
    '    Dim havingcondition As String = ""
    '    report.InnerHtml = ""
    '    Dim htmlreport As String = ""
    '    Dim no As Integer
    '    Dim data As SqlDataReader
    '    Dim tablearray
    '    Dim tablename As String
    '    Dim colsa As DataColumn
    '    Dim reportcolumn As DataColumn
    '    'readquery = com.ExecuteReader
    '    'While readquery.Read()
    '    '    colname = readquery("colname")
    '    '    If colname = "" Then
    '    '        aspnet_msgbox("This Report Is Empty")
    '    '        dataoperation.Visible = False
    '    '        ddlReport.Visible = True
    '    '        Exit Sub
    '    '    End If
    '    '    str12 = colname.Replace("~", ",")

    '    '    str22 = str12.Replace("$", ".")
    '    '    Dim boo As Boolean = False
    '    '    Dim boo1 As Boolean = False
    '    '    boo = str22.Contains("@Date1@")
    '    '    boo1 = str22.Contains("@Date2@")
    '    '    If boo = True Then
    '    '        If TextBox3.Text = "" Then
    '    '            aspnet_msgbox("Fill To Date")
    '    '            dataoperation.Visible = False
    '    '            ddlReport.Visible = True
    '    '            Exit Sub
    '    '        End If

    '    '    End If
    '    '    If boo1 = True Then
    '    '        If TextBox4.Text = "" Then
    '    '            aspnet_msgbox("Fill From Date")
    '    '            dataoperation.Visible = False
    '    '            ddlReport.Visible = True
    '    '            Exit Sub
    '    '        End If
    '    '    End If
    '    '    str22 = str22.Replace("@Date1@", TextBox3.Text)
    '    '    str22 = str22.Replace("@Date2@", TextBox4.Text)
    '    '    str22 = str22.Replace("String.fromCharCode(34)", "")
    '    '    str22 = str22.Replace("+", "")
    '    '    colname = str22
    '    '    formula1 = readquery("wheredata")
    '    '    groupby = readquery("groupby")
    '    '    orderby = readquery("orderby")
    '    '    havingcondition = readquery("havingcondition")
    '    'End While
    '    'wheretxt = ""
    '    'If formula1 <> "" Then
    '    '    formula1 = formula1.Replace("$", ".")
    '    Dim boo As Boolean = False
    '    Dim boo1 As Boolean = False
    '    '    boo = formula1.Contains("'@Date1@'")
    '    '    boo1 = formula1.Contains("'@Date2@'")
    '    '    If boo = True Then
    '    '        If TextBox3.Text = "" Then
    '    '            aspnet_msgbox("Fill To Date")
    '    '            dataoperation.Visible = False
    '    '            ddlReport.Visible = True
    '    '            Exit Sub
    '    '        End If

    '    '    End If
    '    '    If boo1 = True Then
    '    '        If TextBox4.Text = "" Then
    '    '            aspnet_msgbox("Fill From Date")
    '    '            dataoperation.Visible = False
    '    '            ddlReport.Visible = True
    '    '            Exit Sub
    '    '        End If
    '    '    End If
    '    '    wheretxt = "where" & " " & formula1
    '    '    formula1 = formula1.Replace("@Date1@", TextBox3.Text)
    '    '    formula1 = formula1.Replace("@Date2@", TextBox4.Text)
    '    '    wheretxt = "where" & " " & formula1
    '    'Else
    '    '    wheretxt = ""
    '    'End If
    '    'If groupby <> "" Then
    '    '    groupbytext = "group by" & " " & groupby
    '    'Else
    '    '    groupbytext = ""

    '    'End If
    '    'If orderby <> "" Then
    '    '    orderbytext = "order by" & " " & orderby
    '    'Else
    '    '    orderbytext = ""

    '    'End If
    '    'If havingcondition <> "" Then
    '    '    havingcondition = "having" & " " & havingcondition
    '    'Else
    '    '    havingcondition = ""

    '    'End If
    '    'con.Close()
    '    'readquery.Close()

    '    Dim com1 As New SqlCommand
    '    '("select tablename from idmsreportmaster where queryname='" + queryname + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '    'con.Open()
    '    'readquery = com1.ExecuteReader
    '    'While readquery.Read()
    '    '    tablename = readquery("tablename")

    '    'End While
    '    'com1.Dispose()

    '    'tablearray = tablename.Split(",")
    '    'tabcount = UBound(tablearray)
    '    'con.Close()
    '    'readquery.Close()
    '    Dim colarray
    '    'colarray = colname.Split("~")
    '    Dim colcount As Integer
    '    'colcount = UBound(colarray)
    '    'For allcol = 0 To colcount
    '    '    If columnname = "" Then
    '    '        columnname = colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '    '    Else
    '    '        columnname = columnname & "," & colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '    '    End If

    '    'Next
    '    'Session("Queryname") = "select " + columnname + "  from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + " " + orderbytext + "$" + tablename + ""
    '    com1 = New SqlCommand("select reportcolumns,processedcolumns from savedanalysis where analysisname = '" + ddlTable.SelectedItem.Text + "'", con)
    '    con.Open()

    '    da1.SelectCommand = com1
    '    da1.Fill(ds2)
    '    Dim reprow As DataRow
    '    Dim processedcolumns As String = ""
    '    For Each reprow In ds2.Tables(0).Rows

    '        If repcolumn = "" Then
    '            'If reportcolumn.ColumnName() <> "RecordId" Then


    '            repcolumn = reprow.Item(0)
    '            processedcolumns = reprow.Item(1)
    '            'End If
    '        Else
    '            If reportcolumn.ColumnName() <> "RecordId" Then


    '                repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '            End If
    '        End If


    '    Next
    '    Dim processedcolumnsarr = processedcolumns.Split(",")
    '    repcolarray = repcolumn.Split(",")
    '    Session("colsvalue") = processedcolumnsarr

    '    hid.Value = processedcolumns

    '    repcols.DataSource = repcolarray
    '    repcols.DataBind()
    '    selectedcols.DataSource = processedcolumnsarr
    '    selectedcols.DataBind()
    '    con.Close()
    '    'listcolumns.DataSource = repcolarray
    '    'listcolumns.DataBind()
    '    'ddlcolfrfun.DataSource = repcolarray
    '    'ddlcolfrfun.DataBind()
    '    Session("filtercolumns") = repcolarray

    '    com1 = New SqlCommand("select * from  " + ddlTable.SelectedItem.Text + "", con)
    '    con.Open()
    '    data = com1.ExecuteReader
    '    Dim tablehead As String
    '    tablehead = "<table  style=" + "background-color:GradientActiveCaption;" + ">"
    '    Dim ub As Integer
    '    ub = UBound(repcolarray)


    '    tablehead = tablehead & "<tr>"
    '    Dim ubb As String
    '    ubb = (ub + 1).ToString
    '    'tablehead = tablehead & "<td align=center style=" + "background-color:pink colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
    '    tablehead = tablehead & "</tr>"

    '    'tablehead = tablehead & "</table>"
    '    htmlreport = "<table style='background-color:GradientActiveCaption;border:#336699 1px solid'> "
    '    htmlreport = htmlreport & "<caption>" & ddlTable.SelectedItem.Text
    '    htmlreport = htmlreport & "</caption>"

    '    htmlreport = htmlreport & "<tr>"

    '    For no = 0 To ub

    '        If repcolarray(no) <> "RecordId" Then

    '            htmlreport = htmlreport & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & repcolarray(no) & "</b>" & "</td>"
    '        End If

    '    Next
    '    htmlreport = htmlreport & "</tr>"

    '    While data.Read

    '        htmlreport = htmlreport & "<tr>"

    '        Dim intevalueincrement As Integer = 0
    '        For intevalueincrement = 0 To ub
    '            If repcolarray(intevalueincrement) <> "RecordId" Then

    '                Dim valuecheck As String = ""
    '                valuecheck = data(repcolarray(intevalueincrement)).ToString
    '                If valuecheck = "" Then
    '                    valuecheck = 0
    '                End If
    '                htmlreport = htmlreport & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>"
    '                htmlreport = htmlreport & valuecheck & "</td>"
    '            End If
    '        Next
    '        htmlreport = htmlreport & "</tr>"








    '    End While
    '    htmlreport = htmlreport & "</table>"
    '    report.InnerHtml = report.InnerHtml & htmlreport

    '    headdiv.InnerHtml = ""
    '    headdiv.InnerHtml = headdiv.InnerHtml & tablehead
    '    con.Close()
    '    Session("Nextdata") = headdiv.InnerHtml
    '    Session("Nextdata1") = report.InnerHtml
    '    'Session("table") = ddlTable.SelectedItem.Text
    '    Session("repname") = ddlTable.SelectedItem.Text

    '    '----------------------------------
    '    ''''''''''''fix this code''''''''''''''
    '    'For alltabcol = 0 To colcount

    '    '    tabcolumn = colarray(alltabcol)

    '    '    final = tabcolumn.Split(".")


    '    '    tabcolength = UBound(final)

    '    '    For q = 0 To tabcount
    '    '        For r = 0 To tabcolength
    '    '            Dim p As String
    '    '            p = final(tabcolength)
    '    '            If final(r) = tablearray(q) Then

    '    '                If tname = "" Then
    '    '                    tname = final(r)
    '    '                    cname = final(r + 1)

    '    '                Else
    '    '                    tname = final(r) & "," & tname
    '    '                    cname = cname & "," & final(r + 1)

    '    '                End If

    '    '            End If
    '    '        Next
    '    '    Next
    '    'Next

    '    ''------------------------
    '    'com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    'con.Open()
    '    'data = com1.ExecuteReader
    '    'While data.Read()
    '    '    If data("name") = "tab" & ddlReport.SelectedItem.Text Then
    '    '        b = False
    '    '        Exit While

    '    '    Else
    '    '        b = True
    '    '    End If
    '    'End While
    '    'data.Close()

    '    'com1.Dispose()
    '    'con.Close()
    '    'If b = False Then


    '    '*********************'
    '    '''''''''''''cut from here
    '    '************************'
    '    ''''''''''''''''''''''''''''''''''''''
    '    '''fix this code''''''''''''''''''''''''
    '    'If b = True Then
    '    '    If cname <> "" Then
    '    '        aa1 = cname.Split(",")

    '    '        ttcount = UBound(tablearray)
    '    '        cccount = UBound(aa1)
    '    '        For po = 0 To ttcount

    '    '            Try
    '    '                currenttable = CType(tablearray(po), String)
    '    '                da = New SqlDataAdapter("select * from " + currenttable + "", con1)
    '    '                con1.Open()
    '    '                da.Fill(ds)
    '    '                con1.Close()
    '    '            Catch ex As Exception

    '    '                aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
    '    '                dataoperation.Visible = False
    '    '                ddlReport.Visible = True
    '    '                Exit Sub
    '    '                Exit For
    '    '            End Try

    '    '            For g2 = 0 To cccount
    '    '                currentcolumn = CType(aa1(g2), String)
    '    '                For Each colsa In ds.Tables(0).Columns
    '    '                    If colsa.ColumnName = currentcolumn Then
    '    '                        If totalquerycloumn = "" Then
    '    '                            totalquerycloumn = currentcolumn
    '    '                        Else
    '    '                            totalquerycloumn = totalquerycloumn & "," & currentcolumn
    '    '                        End If
    '    '                    End If
    '    '                Next
    '    '            Next

    '    '            ds.Tables(0).Columns.Clear()
    '    '            da.Dispose()

    '    '        Next

    '    '        con.Close()
    '    '        If totalquerycloumn <> "" Then
    '    '            colsss = totalquerycloumn.Split(",")
    '    '            colfinal = UBound(colsss)
    '    '        End If



    '    '    End If





    '    'Try


    '    '    cmd = New SqlCommand("select Identity(int, 1,1) as RecordId, " + columnname + " into " + "tab" & ddlReport.SelectedItem.Text + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + " " + orderbytext + " ", con)
    '    '    con.Open()

    '    '    cmd.ExecuteNonQuery()
    '    'Catch ex As Exception
    '    '    Dim strmessage As String = ""
    '    '    strmessage = Replace(ex.Message.ToString, "'", "")
    '    '    strmessage = Replace(strmessage, vbCrLf, " ")
    '    '    aspnet_msgbox(strmessage)
    '    '    dataoperation.Visible = False
    '    '    ddlReport.Visible = True
    '    '    Exit Sub
    '    'End Try
    '    'con.Close()
    '    'com1 = New SqlCommand("select * from " + "tab" & ddlReport.SelectedItem.Text + "", con)
    '    'con.Open()

    '    'da1.SelectCommand = com1
    '    'da1.Fill(ds2)

    '    'For Each reportcolumn In ds2.Tables(0).Columns

    '    '    If repcolumn = "" Then
    '    '        If reportcolumn.ColumnName() <> "RecordId" Then


    '    '            repcolumn = reportcolumn.ColumnName()
    '    '        End If
    '    '    Else
    '    '        If reportcolumn.ColumnName() <> "RecordId" Then


    '    '            repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '    '        End If
    '    '    End If


    '    'Next
    '    'repcolarray = repcolumn.Split(",")

    '    'repcols.DataSource = repcolarray
    '    'repcols.DataBind()
    '    'con.Close()
    '    ''listcolumns.DataSource = repcolarray
    '    ''listcolumns.DataBind()
    '    ''ddlcolfrfun.DataSource = repcolarray
    '    ''ddlcolfrfun.DataBind()
    '    'Session("filtercolumns") = repcolarray
    '    ''ddlcolfrfun.Items.Insert(0, "---select---")


    '    'com1 = New SqlCommand("select * from  " + "tab" & ddlReport.SelectedItem.Text + "", con)
    '    'con.Open()
    '    'data = com1.ExecuteReader
    '    'Dim tablehead As String
    '    'tablehead = "<table style=" + "background-color:GradientActiveCaption;" + "> "
    '    'Dim ub As Integer
    '    'ub = UBound(repcolarray)
    '    'tablehead = tablehead & "<tr>"
    '    'Dim ubb As String
    '    'ubb = (ub + 1).ToString
    '    'tablehead = tablehead & "<td align=center style=" + "background-color:pink colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
    '    'tablehead = tablehead & "</td></tr>"

    '    'tablehead = tablehead & "</table>"
    '    'htmlreport = "<table border=" + "1" + " style=" + "background-color:GradientActiveCaption;" + "> "
    '    'htmlreport = htmlreport & "<tr>"
    '    'For no = 0 To ub

    '    '    If repcolarray(no) <> "RecordId" Then

    '    '        htmlreport = htmlreport & "<td style=" + "color:black;" + ">" & "<b>" & repcolarray(no) & "</b>" & "</td>"
    '    '    End If
    '    'Next
    '    'htmlreport = htmlreport & "</tr>"

    '    'While data.Read

    '    '    htmlreport = htmlreport & "<tr>"


    '    '    For no = 0 To ub
    '    '        If repcolarray(no) <> "RecordId" Then

    '    '            Dim valuecheck As String = ""
    '    '            valuecheck = data(repcolarray(no)).ToString
    '    '            If valuecheck = "" Then
    '    '                valuecheck = 0
    '    '            End If
    '    '            htmlreport = htmlreport & "<td color = black>"
    '    '            htmlreport = htmlreport & valuecheck & "</td>"
    '    '        End If
    '    '    Next
    '    '    htmlreport = htmlreport & "</tr>"








    '    'End While
    '    'htmlreport = htmlreport & "</table>"
    '    'report.InnerHtml = report.InnerHtml & htmlreport
    '    'headdiv.InnerHtml = ""
    '    'headdiv.InnerHtml = headdiv.InnerHtml & tablehead






    '    'con.Close()

    '    'End If


    '    '''''''''''''''''''''''''''''''''''''''''''''''''



    '    'Session("table") = "tab" & ddlReport.SelectedItem.Text
    '    'End If

    '    'dataoperation.Visible = False
    '    ddlTable.Visible = True
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    'If result.InnerHtml <> "" Or report.InnerHtml <> "" Or relax.InnerHtml <> "" Then
    '    '    divsavereport.Visible = False
    '    '    btnsum.Enabled = True
    '    'End If
    '    'If relax.InnerHtml = "" Then
    '    '    Divhead.Visible = False
    '    '    relax.Visible = False
    '    'Else
    '    '    Divhead.Visible = True
    '    '    relax.Visible = True
    '    'End If
    '    'If report.InnerHtml = "" Then
    '    '    headdiv.Visible = False
    '    '    report.Visible = False
    '    'Else
    '    '    headdiv.Visible = True
    '    '    report.Visible = True
    '    'End If
    '    Dim strClose As String = ""
    '    strClose = "<Script language='Javascript'>"
    '    strClose = strClose + "window.open('ResultDisplay.aspx','ResultDisplay');"
    '    strClose = strClose + "</Script>"
    '    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    'End Sub
    ''' <summary>
    '''this is use generate the report with new data from table 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>created by: Ranjit Singh</remarks>
    'Protected Sub new1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles new1.Click
    '    Dim datetime As String = Session.SessionID
    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'Me.divFormula.Visible = False
    '    'selectedcols.Items.Clear()
    '    'hid.Value = ""
    '    'If ddlDepartmant.SelectedValue = "" Or ddlDepartmant.SelectedItem.Text = "---select---" Then
    '    'aspnet_msgbox("Select Department First")

    '    'ElseIf ddlReport.SelectedValue = "" Then
    '    ' aspnet_msgbox("Select Report First")
    '    'repcols.Items.Clear()
    '    ' ElseIf ddlReport.SelectedItem.Text = "---select---" Then
    '    'aspnet_msgbox("Select Report First")
    '    'Else
    '    cmd = New SqlCommand("select savedby from SavedAnalysis where savedby='" + Session("userid") + "' and analysisname='" + ddlTable.SelectedItem.Text + "'", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    If readquery.HasRows Then

    '    Else

    '        aspnet_msgbox("You don't have rights to update this analysis")
    '        'dataoperation.Visible = False
    '        ddlTable.Visible = True
    '        Exit Sub


    '    End If
    '    con.Close()
    '    readquery.Close()
    '    cmd.Dispose()
    '    Dim queryname As String = ""


    '    cmd = New SqlCommand("select ReportName from SavedAnalysis where analysisname='" + ddlTable.SelectedItem.Text + "'", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    While readquery.Read()
    '        queryname = readquery("ReportName")

    '    End While
    '    con.Close()
    '    readquery.Close()
    '    cmd = New SqlCommand("select colname from idmsreportmaster where  queryname='" + queryname + "' and archivedstatus='No' and savedby <> 'Deleted'", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    If readquery.Read Then
    '        colname = readquery("colname")
    '    Else
    '        aspnet_msgbox("Report On This Analysis Has Been Deleted")
    '        'dataoperation.Visible = False
    '        ddlTable.Visible = True
    '        Exit Sub
    '    End If
    '    con.Close()
    '    readquery.Close()
    '    '=====================''''''''''''''''''''''''''

    '    Dim bo As Boolean







    '    Session("AnalysisQuery") = "Select * from  " + ddlTable.SelectedItem.Text + "#" + ddlTable.SelectedItem.Text + ""

    '    Session("Dataanalysis") = ""
    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    While readquery.Read()
    '        If readquery("name") = ddlTable.SelectedItem.Text Then
    '            bo = False
    '            Exit While

    '        Else
    '            bo = True
    '        End If
    '    End While
    '    readquery.Close()
    '    con.Close()
    '    If bo = False Then





    '        cmd = New SqlCommand("drop table " & ddlTable.SelectedItem.Text & "", con)
    '        con.Open()

    '        cmd.ExecuteNonQuery()
    '        con.Close()




    '    End If




    '    'Dim newvalstr As String = ""
    '    'Dim intval As Integer = 0
    '    'Dim newint As Integer = 0
    '    'newint = queryname.Length

    '    'intval = queryname.IndexOf(" from ")
    '    'newvalstr = queryname.Substring(0, intval - 1)
    '    'Dim intlength As Integer = 0
    '    'intlength = newint - intval
    '    'newvalstr = newvalstr & " into " & ddlReport.SelectedItem.Text
    '    'newvalstr = newvalstr & " " & queryname.Substring(intval - 1, intlength - 1)




    '    'cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    'con.Open()
    '    'readquery = cmd.ExecuteReader
    '    'While readquery.Read()
    '    '    If readquery("name") = "tab" & ddlReport.SelectedItem.Text Then
    '    '        bo = False
    '    '        Exit While

    '    '    Else
    '    '        bo = True
    '    '    End If
    '    'End While
    '    'readquery.Close()
    '    'con.Close()

    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''

    '    ''''''''''''fix this code''''''''''''''
    '    'If bo = False Then






    '    '    cmd = New SqlCommand("select * from  " & ddlReport.SelectedItem.Text + "", con)
    '    '    con.Open()



    '    '    Dim datasetaa As New DataSet

    '    '    dataadapter.SelectCommand = cmd
    '    '    dataadapter.Fill(datasetaa)

    '    '    con.Close()
    '    '    Dim culmnaaa As DataColumn
    '    '    Dim strclocheck As String
    '    '    For Each culmnaaa In datasetaa.Tables(0).Columns


    '    '        strclocheck = culmnaaa.ColumnName()
    '    '        If strclocheck.StartsWith("Accu_Sum") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '        ElseIf strclocheck.StartsWith("Column_Percentage_") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()

    '    '        ElseIf strclocheck.StartsWith("Column_Sum_Percentage") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '        ElseIf strclocheck.StartsWith("Column_Sum") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '            'End If
    '    '        ElseIf strclocheck.StartsWith("Row_Percentage_") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '        ElseIf strclocheck.StartsWith("Row_Sum_") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '        ElseIf strclocheck.Contains("Filter") Then
    '    '            cmd = New SqlCommand("alter table " + "tab" & ddlReport.SelectedItem.Text + " drop column " + strclocheck + " ", con)
    '    '            con.Open()
    '    '            cmd.ExecuteNonQuery()
    '    '            con.Close()
    '    '        End If




    '    '    Next
    '    'End If



    '    ''''''''''''''''''============================='''''''''''''''''''''''''''''''''




    '    Dim com As New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition,datecontable from idmsreportmaster where queryname='" + queryname + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '    con.Open()
    '    Dim columnname, cname, tabcolumn, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, repcolumn, str22, str12, totalquerycloumn As String
    '    Dim aa1
    '    Dim colsss
    '    Dim b As Boolean

    '    Dim datecondition As String = ""
    '    Dim da As New SqlDataAdapter
    '    Dim da1 As New SqlDataAdapter
    '    'Dim cmd As SqlCommand
    '    Dim ds As New DataSet
    '    Dim colfinal As Integer
    '    Dim ttcount, cccount, p2, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer
    '    Dim ds2 As New DataSet
    '    Dim final
    '    Dim havingcondition As String = ""
    '    report.InnerHtml = ""
    '    Dim htmlreport As String = ""
    '    Dim no As Integer
    '    Dim data As SqlDataReader
    '    Dim tablearray
    '    Dim tablename As String
    '    Dim colsa As DataColumn
    '    Dim reportcolumn As DataColumn
    '    readquery = com.ExecuteReader
    '    While readquery.Read()
    '        colname = readquery("colname")
    '        Dim scode As String = vbNewLine
    '        colname = colname.Replace(scode, "")
    '        If colname = "" Then
    '            aspnet_msgbox("This Report Is Empty")
    '            'dataoperation.Visible = False
    '            ddlTable.Visible = True
    '            Exit Sub
    '        End If
    '        str12 = colname.Replace("~", ",")

    '        str22 = str12.Replace("$", ".")
    '        Dim boo As Boolean = False
    '        Dim boo1 As Boolean = False
    '        boo = str22.Contains("@Date1@")
    '        boo1 = str22.Contains("@Date2@")
    '        If boo = True Then
    '            If TextBox3.Text = "" Then
    '                aspnet_msgbox("Fill To Date")
    '                'dataoperation.Visible = False
    '                ddlTable.Visible = True
    '                Exit Sub

    '            End If

    '        End If
    '        If boo1 = True Then
    '            If TextBox4.Text = "" Then
    '                aspnet_msgbox("Fill From Date")
    '                'dataoperation.Visible = False
    '                ddlTable.Visible = True
    '                Exit Sub

    '            End If
    '        End If
    '        str22 = str22.Replace("@Date1@", TextBox3.Text)
    '        str22 = str22.Replace("@Date2@", TextBox4.Text)
    '        str22 = str22.Replace("String.fromCharCode(34)", "")
    '        'str22 = str22.Replace("+", "")
    '        colname = str22


    '        If IsDBNull(readquery("wheredata")) Then
    '        Else
    '            formula1 = readquery("wheredata")
    '        End If
    '        If IsDBNull(readquery("groupby")) Then
    '        Else
    '            groupby = readquery("groupby")
    '        End If
    '        If IsDBNull(readquery("orderby")) Then
    '        Else

    '            orderby = readquery("orderby")
    '        End If
    '        If IsDBNull(readquery("havingcondition")) Then

    '        Else
    '            havingcondition = readquery("havingcondition")
    '        End If
    '        If IsDBNull(readquery("datecontable")) Then
    '        Else

    '            datecondition = readquery("datecontable")
    '        End If

    '    End While
    '    If readquery.HasRows Then

    '    Else
    '        aspnet_msgbox("Report On This Analysis Has Been Deleted")
    '        'dataoperation.Visible = False
    '        ddlTable.Visible = True
    '        Exit Sub
    '    End If
    '    wheretxt = ""
    '    If formula1 <> "" Then
    '        formula1 = formula1.Replace("$", ".")
    '        Dim boo As Boolean = False
    '        Dim boo1 As Boolean = False
    '        boo = formula1.Contains("'@Date1@'")
    '        boo1 = formula1.Contains("'@Date2@'")
    '        If boo = True Then
    '            If TextBox3.Text = "" Then
    '                aspnet_msgbox("Fill To Date")
    '                'dataoperation.Visible = False
    '                ddlTable.Visible = True
    '                Exit Sub

    '            End If

    '        End If
    '        If boo1 = True Then
    '            If TextBox4.Text = "" Then
    '                aspnet_msgbox("Fill From Date")
    '                'dataoperation.Visible = False
    '                ddlTable.Visible = True
    '                Exit Sub

    '            End If
    '        End If
    '        wheretxt = "where" & " " & formula1
    '        formula1 = formula1.Replace("@Date1@", TextBox3.Text)
    '        formula1 = formula1.Replace("@Date2@", TextBox4.Text)
    '        wheretxt = "where" & " " & formula1
    '    Else
    '        wheretxt = ""
    '    End If
    '    If datecondition <> "" Then
    '        If TextBox3.Text <> "" And TextBox4.Text <> "" Then
    '            If wheretxt = "" Then
    '                wheretxt = "where" + " " + datecondition + ".Date >='" + TextBox3.Text + "' and " + datecondition + ".Date <='" + TextBox4.Text + "'"
    '            Else
    '                wheretxt = wheretxt + " " + datecondition + ".Date >='" + TextBox3.Text + "' and " + datecondition + ".Date <='" + TextBox4.Text + "'"
    '            End If

    '        End If

    '    End If
    '    If groupby <> "" Then
    '        groupbytext = "group by" & " " & groupby
    '    Else
    '        groupbytext = ""

    '    End If
    '    If orderby <> "" Then
    '        orderbytext = "order by" & " " & orderby
    '    Else
    '        orderbytext = ""

    '    End If
    '    con.Close()
    '    readquery.Close()

    '    Dim com1 As New SqlCommand("select tablename from idmsreportmaster where queryname='" + queryname + "' and archivedstatus='No' and savedby<>'Deleted'", con)
    '    con.Open()
    '    readquery = com1.ExecuteReader
    '    While readquery.Read()
    '        tablename = readquery("tablename")

    '    End While
    '    com1.Dispose()
    '    tablename = tablename.Replace("~", ",")
    '    tablearray = tablename.Split(",")
    '    tabcount = UBound(tablearray)
    '    con.Close()
    '    readquery.Close()
    '    Dim colarray
    '    colarray = colname.Split("~")
    '    Dim colcount As Integer
    '    colcount = UBound(colarray)
    '    For allcol = 0 To colcount
    '        If columnname = "" Then
    '            columnname = colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '        Else
    '            columnname = columnname & "," & colarray(allcol) '& " " & "as" & " " & "column" & allcol
    '        End If

    '    Next
    '    'Session("Queryname") = "select " + columnname + "  from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + " " + orderbytext + "$" + tablename + ""
    '    ' ''----------------------------------
    '    '''''''''fix this code''''''''''''''''''
    '    'For alltabcol = 0 To colcount

    '    '    tabcolumn = colarray(alltabcol)

    '    '    final = tabcolumn.Split(".")


    '    '    tabcolength = UBound(final)

    '    '    For q = 0 To tabcount
    '    '        For r = 0 To tabcolength
    '    '            Dim p As String
    '    '            p = final(tabcolength)
    '    '            If final(r) = tablearray(q) Then

    '    '                If tname = "" Then
    '    '                    tname = final(r)
    '    '                    cname = final(r + 1)

    '    '                Else
    '    '                    tname = final(r) & "," & tname
    '    '                    cname = cname & "," & final(r + 1)

    '    '                End If

    '    '            End If
    '    '        Next
    '    '    Next
    '    'Next

    '    ''------------------------
    '    'com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    'con.Open()
    '    'data = com1.ExecuteReader
    '    'While data.Read()
    '    '    If data("name") = "tab" & ddlReport.SelectedItem.Text Then
    '    '        b = False
    '    '        Exit While

    '    '    Else
    '    '        b = True
    '    '    End If
    '    'End While
    '    'data.Close()

    '    'com1.Dispose()
    '    'con.Close()
    '    'If b = False Then
    '    '    com1 = New SqlCommand("select * from  " + "tab" & ddlReport.SelectedItem.Text + "", con)
    '    '    con.Open()





    '    '    da1.SelectCommand = com1
    '    '    da1.Fill(ds2)

    '    '    con.Close()
    '    '    For Each reportcolumn In ds2.Tables(0).Columns

    '    '        If repcolumn = "" Then
    '    '            If reportcolumn.ColumnName() <> "RecordId" Then


    '    '                repcolumn = reportcolumn.ColumnName()
    '    '            End If
    '    '        Else
    '    '            If reportcolumn.ColumnName() <> "RecordId" Then


    '    '                repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '    '            End If
    '    '        End If



    '    '    Next
    '    '    repcolarray = repcolumn.Split(",")
    '    '    repcols.DataSource = repcolarray
    '    '    repcols.DataBind()
    '    '    'listcolumns.DataSource = repcolarray
    '    '    'listcolumns.DataBind()
    '    '    'ddlcolfrfun.DataSource = repcolarray
    '    '    'ddlcolfrfun.DataBind()
    '    '    Session("filtercolumns") = repcolarray
    '    '    'ddlcolfrfun.Items.Insert(0, "---select---")

    '    '    com1 = New SqlCommand("select * from  " + "tab" & ddlReport.SelectedItem.Text + "", con)
    '    '    con.Open()
    '    '    data = com1.ExecuteReader

    '    '    Dim tablehead As String
    '    '    tablehead = "<table width=100%  style=" + "background-color:GradientActiveCaption;" + ">"
    '    '    Dim ub As Integer
    '    '    ub = UBound(repcolarray)


    '    '    tablehead = tablehead & "<tr>"
    '    '    Dim ubb As String
    '    '    ubb = (ub + 1).ToString
    '    '    tablehead = tablehead & "<td align=center style=" + "background-color:pink colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
    '    '    tablehead = tablehead & "</td></tr>"

    '    '    tablehead = tablehead & "</table>"
    '    '    htmlreport = "<table width=100% border=" + "1" + " style=" + "background-color:GradientActiveCaption;" + ">"
    '    '    htmlreport = htmlreport & "<tr>"


    '    '    For no = 0 To ub
    '    '        If repcolarray(no) <> "RecordId" Then
    '    '            htmlreport = htmlreport & "<td style=" + "color:black;" + ">" & "<b>" & repcolarray(no) & "</b>" & "</td>"
    '    '        End If

    '    '    Next
    '    '    htmlreport = htmlreport & "</tr>"
    '    '    While data.Read

    '    '        htmlreport = htmlreport & "<tr>"


    '    '        For no = 0 To ub
    '    '            If repcolarray(no) <> "RecordId" Then
    '    '                Dim valuecheck As String = ""
    '    '                valuecheck = data(repcolarray(no)).ToString
    '    '                If valuecheck = "" Then
    '    '                    valuecheck = 0
    '    '                End If

    '    '                htmlreport = htmlreport & "<td color = black>"
    '    '                htmlreport = htmlreport & valuecheck & "</td>"
    '    '            End If
    '    '        Next
    '    '        htmlreport = htmlreport & "</tr>"








    '    '    End While
    '    '    htmlreport = htmlreport & "</table>"
    '    '    headdiv.InnerHtml = ""
    '    '    headdiv.InnerHtml = headdiv.InnerHtml & tablehead
    '    '    report.InnerHtml = report.InnerHtml & htmlreport


    '    '    con.Close()
    '    '    Session("table") = "tab" & ddlReport.SelectedItem.Text
    '    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''




    '    'ElseIf b = True Then
    '    'If cname <> "" Then
    '    '    aa1 = cname.Split(",")

    '    '    ttcount = UBound(tablearray)
    '    '    cccount = UBound(aa1)
    '    '    For po = 0 To ttcount

    '    '        Try
    '    '            currenttable = CType(tablearray(po), String)
    '    '            da = New SqlDataAdapter("select * from " + currenttable + "", con1)
    '    '            con1.Open()
    '    '            da.Fill(ds)
    '    '            con1.Close()
    '    '        Catch ex As Exception

    '    '            aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
    '    '            dataoperation.Visible = False
    '    '            ddlReport.Visible = True
    '    '            Exit Sub
    '    '            Exit For
    '    '        End Try

    '    '        For g2 = 0 To cccount
    '    '            currentcolumn = CType(aa1(g2), String)
    '    '            For Each colsa In ds.Tables(0).Columns
    '    '                If colsa.ColumnName = currentcolumn Then
    '    '                    If totalquerycloumn = "" Then
    '    '                        totalquerycloumn = currentcolumn
    '    '                    Else
    '    '                        totalquerycloumn = totalquerycloumn & "," & currentcolumn
    '    '                    End If
    '    '                End If
    '    '            Next
    '    '        Next

    '    '        ds.Tables(0).Columns.Clear()
    '    '        da.Dispose()

    '    '    Next

    '    '    con.Close()
    '    '    If totalquerycloumn <> "" Then
    '    '        colsss = totalquerycloumn.Split(",")
    '    '        colfinal = UBound(colsss)
    '    '    End If



    '    'End If





    '    Try


    '        cmd = New SqlCommand("select Identity(int, 1,1) as RecordId, " + columnname + " into " + ddlTable.SelectedItem.Text + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingcondition + "  " + orderbytext + " ", con)
    '        con.Open()

    '        cmd.ExecuteNonQuery()
    '    Catch ex As Exception
    '        Dim strmessage As String = ""
    '        strmessage = Replace(ex.Message.ToString, "'", "")
    '        strmessage = Replace(strmessage, vbCrLf, " ")
    '        aspnet_msgbox(strmessage)
    '        'dataoperation.Visible = False
    '        ddlTable.Visible = True
    '        Exit Sub
    '    End Try
    '    con.Close()
    '    com1 = New SqlCommand("select reportcolumns,processedcolumns from savedanalysis where analysisname = '" + ddlTable.SelectedItem.Text + "'", con)
    '    con.Open()

    '    da1.SelectCommand = com1
    '    da1.Fill(ds2)
    '    Dim reprow As DataRow
    '    Dim processedcolumns As String = ""
    '    For Each reprow In ds2.Tables(0).Rows

    '        If repcolumn = "" Then
    '            'If reportcolumn.ColumnName() <> "RecordId" Then


    '            repcolumn = reprow.Item(0)
    '            processedcolumns = reprow.Item(1)
    '            'End If
    '        Else
    '            If reportcolumn.ColumnName() <> "RecordId" Then


    '                repcolumn = repcolumn & "," & reportcolumn.ColumnName()
    '            End If
    '        End If


    '    Next
    '    Dim processedcolumnsarr = processedcolumns.Split(",")
    '    repcolarray = repcolumn.Split(",")
    '    Session("colsvalue") = processedcolumnsarr

    '    hid.Value = processedcolumns

    '    repcols.DataSource = repcolarray
    '    repcols.DataBind()
    '    selectedcols.DataSource = processedcolumnsarr
    '    selectedcols.DataBind()
    '    con.Close()
    '    'listcolumns.DataSource = repcolarray
    '    'listcolumns.DataBind()
    '    'ddlcolfrfun.DataSource = repcolarray
    '    'ddlcolfrfun.DataBind()
    '    Session("filtercolumns") = repcolarray
    '    'ddlcolfrfun.Items.Insert(0, "---select---")

    '    com1 = New SqlCommand("select * from  " + ddlTable.SelectedItem.Text + "", con)
    '    con.Open()
    '    data = com1.ExecuteReader
    '    Dim tablehead As String
    '    tablehead = "<table> "
    '    Dim ub As Integer
    '    ub = UBound(repcolarray)
    '    tablehead = tablehead & "<tr>"
    '    Dim ubb As String
    '    ubb = (ub + 1).ToString
    '    'tablehead = tablehead & "<td align=center style=" + "background-color:pink colspan=" + "  " + ubb + "  >" & ddlReport.SelectedItem.Text
    '    tablehead = tablehead & "</tr>"

    '    'tablehead = tablehead & "</table>"
    '    htmlreport = "<table  style='background-color:GradientActiveCaption;border:#336699 1px solid' > "
    '    htmlreport = htmlreport & "<caption>" & ddlTable.SelectedItem.Text
    '    htmlreport = htmlreport & "</caption>"
    '    htmlreport = htmlreport & "<tr>"
    '    For no = 0 To ub

    '        If repcolarray(no) <> "RecordId" Then

    '            htmlreport = htmlreport & "<td style='background-color:lightgrey; color:black; Font-size:10pt; border:#336699 1px solid'><b>" & repcolarray(no) & "</b>" & "</td>"
    '        End If
    '    Next
    '    htmlreport = htmlreport & "</tr>"

    '    While data.Read

    '        htmlreport = htmlreport & "<tr>"


    '        For no = 0 To ub
    '            If repcolarray(no) <> "RecordId" Then

    '                Dim valuecheck As String = ""
    '                valuecheck = data(repcolarray(no)).ToString
    '                If valuecheck = "" Then
    '                    valuecheck = 0
    '                End If
    '                htmlreport = htmlreport & "<td style='color:black; Font-size:10pt; border:#336699 1px solid'>"
    '                htmlreport = htmlreport & valuecheck & "</td>"
    '            End If
    '        Next
    '        htmlreport = htmlreport & "</tr>"








    '    End While
    '    htmlreport = htmlreport & "</table>"
    '    report.InnerHtml = report.InnerHtml & htmlreport
    '    headdiv.InnerHtml = ""
    '    headdiv.InnerHtml = headdiv.InnerHtml & tablehead

    '    Session("Nextdata") = headdiv.InnerHtml
    '    Session("Nextdata1") = report.InnerHtml



    '    con.Close()

    '    'End If
    '    ' End If
    '    'dataoperation.Visible = False
    '    ddlTable.Visible = True
    '    If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '        divsavereport.Visible = False
    '        btnsum.Enabled = False
    '    End If
    '    Dim strClose As String = ""
    '    strClose = "<Script language='Javascript'>"
    '    strClose = strClose + "window.open('ResultDisplay.aspx','ResultDisplay');"
    '    strClose = strClose + "</Script>"
    '    Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
    '    Session("repname") = ddlTable.SelectedItem.Text

    'End Sub
    ''' <summary>
    ''' this function is making the sql aggrigate functions
    '''     ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub btnfun_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfun.Click

    '    If ddlfunctions.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("Function Is Not Selected")
    '        Exit Sub
    '    End If
    '    If ddlcolfrfun.SelectedItem.Text = "---select---" Then
    '        aspnet_msgbox("Column Is Not Selected")
    '        Exit Sub
    '    End If

    '    Dim Table As String = ""

    '    If Session("reportname") = "" Then
    '        aspnet_msgbox("No Report Name Is Selected")
    '        Exit Sub
    '    End If
    '    Table = Session("reportname").ToString



    '    If Table = "" Then
    '        aspnet_msgbox("No Report Name Is Selected")
    '        Exit Sub
    '    End If
    '    'ddlcolfrfun
    '    Dim boo As Boolean
    '    Dim arr = TextBox1.Text.Split(",")
    '    Dim cnt, cnt1 As Integer
    '    cnt = UBound(arr)
    '    For cnt1 = 0 To cnt
    '        If arr(cnt1) = ddlcolfrfun.SelectedItem.Text Then
    '            Exit For
    '            boo = False
    '        Else
    '            boo = True
    '        End If
    '        If boo = True Then
    '            If TextBox1.Text = "" Then
    '                TextBox1.Text = ddlcolfrfun.SelectedItem.Text
    '            Else
    '                TextBox1.Text = TextBox1.Text & "," & ddlcolfrfun.SelectedItem.Text
    '            End If

    '        End If
    '    Next
    '    Dim b As Boolean
    '    Dim datatype As String = ""
    '    Dim dr As SqlDataReader
    '    cmd = New SqlCommand("select name from sysobjects where xtype='u'", con)
    '    con.Open()
    '    dr = cmd.ExecuteReader
    '    While dr.Read()
    '        If dr("name") = "temptablehaig" Then
    '            b = False
    '            Exit While

    '        Else
    '            b = True
    '        End If
    '    End While
    '    dr.Close()
    '    con.Close()

    '    If b = False Then


    '        cmd = New SqlCommand("drop table temptablehaig", con)
    '        con.Open()
    '        cmd.ExecuteNonQuery()
    '        con.Close()
    '    End If

    '    cmd = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + Table + "' and COLUMN_NAME='" + ddlcolfrfun.SelectedItem.Text + "'", con)
    '    con.Open()
    '    dr = cmd.ExecuteReader

    '    If dr.Read Then
    '        datatype = dr("type")
    '    End If
    '    con.Close()
    '    dr.Close()
    '    If datatype <> "datetime" Then
    '        Try
    '            cmd = New SqlCommand("select (Convert(numeric," + ddlcolfrfun.SelectedItem.Text + ")) as " + ddlcolfrfun.SelectedItem.Text + " into temptablehaig  from " + Table + "", con)
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            con.Close()

    '        Catch ex As Exception
    '            Dim strmessage As String = ""
    '            strmessage = Replace(ex.Message.ToString, "'", "")
    '            strmessage = Replace(strmessage, vbCrLf, " ")
    '            aspnet_msgbox(strmessage)
    '            dataoperation.Visible = False
    '            ddlReport.Visible = True
    '            Exit Sub
    '        End Try


    '        cmd = New SqlCommand("select " + ddlfunctions.SelectedItem.Text + " (" + ddlcolfrfun.SelectedItem.Text + ") as functions from temptablehaig", con)
    '        con.Open()
    '        dr = cmd.ExecuteReader
    '        If dr.Read Then
    '            finalformula.Text = finalformula.Text & ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
    '            If hidfun.Value = "" Then
    '                hidfun.Value = ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
    '                hidfun.Value = hidfun.Value & "," & dr("functions").ToString

    '            Else
    '                hidfun.Value = hidfun.Value & "," & ddlfunctions.SelectedItem.Text & " (" & ddlcolfrfun.SelectedItem.Text & ")"
    '                hidfun.Value = hidfun.Value & "," & dr("functions").ToString
    '            End If
    '        End If
    '        con.Close()
    '        dr.Close()
    '    End If
    'End Sub
    ''' <summary>
    ''' This is use to enable the save report pane
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>


    ''' <summary>
    ''' this is closing the panel of old data and new data which comes after clicking on show report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub btnclosepanel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclosepanel.Click
    '    'dataoperation.Visible = False
    '    ddlTable.Visible = True
    'End Sub

    Protected Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged



    End Sub

    'Protected Sub ddlfunctions_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlfunctions.SelectedIndexChanged

    'End Sub

    'Protected Sub ddlcolfrfun_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcolfrfun.SelectedIndexChanged

    'End Sub

    'Protected Sub Btnrmv_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnrmv.Click

    'End Sub

    '''''''''''''''''''''''''''''''''''changes'''''''''''''''''''''''''''''''''

    '''' Commented code has been shifted to new page''''''''''''
    ''' <summary>
    ''' To set alert on shown data
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub btnSetalert_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSetalert.Click
    '    If report.InnerHtml <> "" Then
    '        Response.Redirect("../mailsandalerts/Escalation.aspx?val=70")
    '    Else
    '        aspnet_msgbox("No Data Is Present To Set Alert")
    '    End If
    'End Sub

    'Protected Sub RdioAnalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdioAnalysis.CheckedChanged
    '    Me.ddlDepartmant.SelectedIndex = 0
    '    Me.ddlClient.Items.Clear()
    '    Me.ddlLob.Items.Clear()
    '    Me.ddlReport.Items.Clear()
    '    Me.ddlUser.Items.Clear()
    '    Me.ddlUser.Enabled = False
    '    Session("analysis") = "yes"
    '    Me.result.InnerHtml = ""
    '    headdiv.InnerHtml = ""
    '    report.InnerHtml = ""
    '    repcols.Items.Clear()
    '    selectedcols.Items.Clear()
    '    dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    lblrepanalysis.InnerText = "Select Analysis"
    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    'End Sub

    'Protected Sub RdioReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RdioReport.CheckedChanged
    '    'Me.ddlDepartmant.SelectedIndex = 0
    '    'Me.ddlClient.Items.Clear()
    '    'Me.ddlLob.Items.Clear()
    '    'Me.ddlReport.Items.Clear()
    '    'Me.ddlUser.Items.Clear()
    '    Session("analysis") = ""
    '    Me.result.InnerHtml = ""
    '    headdiv.InnerHtml = ""
    '    report.InnerHtml = ""
    '    repcols.Items.Clear()
    '    selectedcols.Items.Clear()
    '    'dataoperation.Visible = False
    '    ddlReport.Visible = True
    '    lblrepanalysis.InnerText = "Select Report"

    '    Session("maxval1") = ""
    '    Session("maxval") = ""
    'End Sub

    'Protected Sub Button4_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    ''DropDowndept.DataTextField = "departmentname"
    '    ''DropDowndept.DataValueField = "autoid"
    '    ''Dim classobj As New Functions
    '    ''DropDowndept.Items.Clear()
    '    ''DropDowndept.DataSource = classobj.bind_Department()
    '    ''DropDowndept.DataBind()
    '    ''DropDowndept.Items.Insert(0, "---select---")
    '    'If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

    '    '    divsavereport.Visible = False
    '    'End If
    '    'If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
    '    '    divsavereport.Visible = True

    '    'End If
    '    If textreportname.Text.Contains("(") Or textreportname.Text.Contains(")") Or textreportname.Text.Contains("~") Or textreportname.Text.Contains("`") Or textreportname.Text.Contains("$") Or textreportname.Text.Contains("#") Or textreportname.Text.Contains("@") Or textreportname.Text.Contains("!") Or textreportname.Text.Contains("%") Or textreportname.Text.Contains("^") Or textreportname.Text.Contains("&") Or textreportname.Text.Contains("*") Or textreportname.Text.Contains("-") Or textreportname.Text.Contains("+") Or textreportname.Text.Contains(":") Or textreportname.Text.Contains(";") Or textreportname.Text.Contains(",") Or textreportname.Text.Contains(".") Or textreportname.Text.Contains("/") Or textreportname.Text.Contains("?") Or textreportname.Text.Contains("|") Or textreportname.Text.Contains("\") Or textreportname.Text.Contains("{") Or textreportname.Text.Contains("}") Or textreportname.Text.Contains("[") Or textreportname.Text.Contains("]") Or textreportname.Text.Contains(" ") Or textreportname.Text.Contains("<") Or textreportname.Text.Contains(">") Then

    '        aspnet_msgbox("No Special Symbol Is Allowed")
    '        Exit Sub
    '    End If
    '    'If DropDowndept.SelectedItem.Text = "---select---" Then
    '    '    aspnet_msgbox("Select Department")
    '    '    Exit Sub
    '    'End If
    '    Dim repname As String = ""
    '    cmd = New SqlCommand("select ReportName as ReportName from dataanalysishtmlreport", con)
    '    con.Open()
    '    readquery = cmd.ExecuteReader
    '    While readquery.Read()

    '        repname = readquery("ReportName")
    '        If repname = textreportname.Text Then
    '            aspnet_msgbox("Choose Another Report Name")
    '            textreportname.Focus()
    '            Exit Sub
    '        End If

    '    End While
    '    readquery.Close()
    '    con.Close()

    '    If textreportname.Text = "" Then
    '        aspnet_msgbox("Fill Report Name")
    '        textreportname.Focus()
    '        Exit Sub
    '    End If
    '    strdivreport.Value = report.InnerHtml.ToString
    '    If strdivreport.Value <> "" Then
    '        'txtstrdiv.Value = strpretags & txtstrdiv.Value & strposttags
    '        Dim str As String = ""
    '        Dim fp As StreamWriter
    '        If Not Directory.Exists(Server.MapPath("html/")) Then
    '            '<----------------------Creating Directory for partcular user--------------------------------->
    '            Directory.CreateDirectory(Server.MapPath("html/"))
    '            '<----------------------End of Creating Directory for partcular user------------------------>
    '        End If
    '        '<------------------------End of Creating A main Directory--------------------------------------->


    '        Dim Path = "html/" & textreportname.Text & ".html"
    '        '<--------------------Creating a new text file---------------------------------->
    '        fp = File.CreateText(Server.MapPath(Path))
    '        'change
    '        fp.WriteLine(result.InnerHtml)
    '        fp.WriteLine(headdiv.InnerHtml)
    '        fp.WriteLine(strdivreport.Value)
    '        'fp.WriteLine(Divhead.InnerHtml)
    '        'fp.WriteLine(relax.InnerHtml)



    '        fp.Close()
    '    End If
    '    Dim deptval As String = ""
    '    Dim clientval As String = ""
    '    Dim lobval As String = ""
    '    'deptval = DropDowndept.SelectedValue.ToString
    '    'clientval = DropDownclient.SelectedValue.ToString
    '    'lobval = DropDownlob.SelectedValue.ToString
    '    'If DropDowndept.SelectedItem.Text = "---select---" Then
    '    '    aspnet_msgbox("Select Department First")
    '    '    Exit Sub
    '    'End If
    '    'If DropDownclient.SelectedItem.Text = "---select---" Then

    '    '    DropDownclient.SelectedItem.Text = "0"
    '    '    clientval = "0"

    '    '    DropDownlob.SelectedItem.Text = "0"
    '    '    lobval = "0"
    '    'End If
    '    'If DropDownlob.SelectedItem.Text = "---select---" Or DropDownlob.SelectedItem.Text = "0" Then
    '    '    DropDownlob.SelectedItem.Text = "0"
    '    '    lobval = "0"
    '    'End If
    '    'Dim localstatus As String = ""
    '    'If chklocal.Checked = True Then
    '    '    localstatus = "Local"
    '    'Else
    '    '    localstatus = "NonLocal"
    '    'End If
    '    cmd = New SqlCommand("insert into dataanalysishtmlreport values('BMPEPL','60','Deveopment_Team','36','IDMS_Application','29',@ReportName,@SavedBy,@repname,'local')", con)
    '    'cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@DeptName").Value = "BMPEPL"
    '    'cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@DeptId").Value = 60
    '    'cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@ClientName").Value = "Development_Team"
    '    'cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@ClientId").Value = 36
    '    'cmd.Parameters.Add("@LobName", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@LobName").Value = "IDMS_Application"
    '    'cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@LobId").Value = 29
    '    cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@ReportName").Value = textreportname.Text
    '    cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@SavedBy").Value = Session("userid")
    '    cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
    '    cmd.Parameters("@repname").Value = ddlTable.SelectedItem.Text
    '    'cmd.Parameters.Add("@local", SqlDbType.VarChar, 50)
    '    'cmd.Parameters("@local").Value = localstatus
    '    '
    '    con.Open()
    '    cmd.ExecuteNonQuery()
    '    con.Close()
    '    '''''''''''''''Usertype check for track goes here:- By Suvidha

    '    'Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where HTMLReport='" + textreportname.Text + "' and Action='Save' and Entity='HTMLReport'", con)
    '    'con.Open()
    '    'cmm.ExecuteNonQuery()
    '    'con.Close()
    '    '''''''''''''''Usertype check for track goes here:- By Suvidha

    '    aspnet_msgbox("Report Has Been Saved Successfully")
    '    divsavereport.Visible = False
    '    'chklocal.Checked = False

    '    'If DropDownclient.SelectedItem.Text = "0" Then

    '    '    DropDownclient.SelectedItem.Text = "---select---"



    '    'End If
    '    'If DropDownlob.SelectedItem.Text = "0" Then
    '    '    DropDownlob.SelectedItem.Text = "---select---"

    '    'End If
    '    strdivreport.Value = ""
    '    'DropDowndept.SelectedIndex = 0
    '    'DropDownclient.Items.Clear()
    '    'DropDownlob.Items.Clear()
    '    'cmd = New SqlCommand("drop table R566R", con)
    '    'con.Open()
    '    'cmd.ExecuteNonQuery()
    '    'con.Close()

    '    'groupcolumns.Items.Clear()
    '    'allcolumns.Items.Clear()
    '    textreportname.Text = ""
    '    'Me.result.InnerHtml = ""
    '    'Me.relax.InnerHtml = ""
    '    'report.InnerHtml = ""


    'End Sub

    Protected Sub SAVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SAVE_singleuser.Click

        ''DropDowndept.DataTextField = "departmentname"
        ''DropDowndept.DataValueField = "autoid"
        ''Dim classobj As New Functions
        ''DropDowndept.Items.Clear()
        ''DropDowndept.DataSource = classobj.bind_Department()
        ''DropDowndept.DataBind()
        ''DropDowndept.Items.Insert(0, "---select---")
        'If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

        '    divsavereport.Visible = False
        'End If
        'If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
        '    divsavereport.Visible = True

        'End If
        If textreportname.Text.Contains("(") Or textreportname.Text.Contains(")") Or textreportname.Text.Contains("~") Or textreportname.Text.Contains("`") Or textreportname.Text.Contains("$") Or textreportname.Text.Contains("#") Or textreportname.Text.Contains("@") Or textreportname.Text.Contains("!") Or textreportname.Text.Contains("%") Or textreportname.Text.Contains("^") Or textreportname.Text.Contains("&") Or textreportname.Text.Contains("*") Or textreportname.Text.Contains("-") Or textreportname.Text.Contains("+") Or textreportname.Text.Contains(":") Or textreportname.Text.Contains(";") Or textreportname.Text.Contains(",") Or textreportname.Text.Contains(".") Or textreportname.Text.Contains("/") Or textreportname.Text.Contains("?") Or textreportname.Text.Contains("|") Or textreportname.Text.Contains("\") Or textreportname.Text.Contains("{") Or textreportname.Text.Contains("}") Or textreportname.Text.Contains("[") Or textreportname.Text.Contains("]") Or textreportname.Text.Contains(" ") Or textreportname.Text.Contains("<") Or textreportname.Text.Contains(">") Then

            aspnet_msgbox("No Special Symbol Is Allowed")
            Exit Sub
        End If
        'If DropDowndept.SelectedItem.Text = "---select---" Then
        '    aspnet_msgbox("Select Department")
        '    Exit Sub
        'End If
        Dim repname As String = ""
        cmd = New SqlCommand("select ReportName as ReportName from dataanalysishtmlreport ", con)
        con.Open()
        readquery = cmd.ExecuteReader
        While readquery.Read()

            repname = readquery("ReportName")
            If repname = textreportname.Text Then
                aspnet_msgbox("Choose Another Report Name")
                textreportname.Focus()
                Exit Sub
            End If

        End While
        readquery.Close()
        con.Close()

        If textreportname.Text = "" Then
            aspnet_msgbox("Fill Report Name")
            textreportname.Focus()
            Exit Sub
        End If
        strdivreport.Value = report.InnerHtml.ToString
        If strdivreport.Value <> "" Then
            'txtstrdiv.Value = strpretags & txtstrdiv.Value & strposttags
            Dim str As String = ""
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("html/")) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("html/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->


            Dim Path = "html/" & textreportname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))
            'change
            fp.WriteLine(result.InnerHtml)
            fp.WriteLine(headdiv.InnerHtml)
            fp.WriteLine(strdivreport.Value)
            'fp.WriteLine(Divhead.InnerHtml)
            'fp.WriteLine(relax.InnerHtml)



            fp.Close()
        End If
        Dim deptval As String = ""
        Dim clientval As String = ""
        Dim lobval As String = ""
        'deptval = DropDowndept.SelectedValue.ToString
        'clientval = DropDownclient.SelectedValue.ToString
        'lobval = DropDownlob.SelectedValue.ToString
        'If DropDowndept.SelectedItem.Text = "---select---" Then
        '    aspnet_msgbox("Select Department First")
        '    Exit Sub
        'End If
        'If DropDownclient.SelectedItem.Text = "---select---" Then

        '    DropDownclient.SelectedItem.Text = "0"
        '    clientval = "0"

        '    DropDownlob.SelectedItem.Text = "0"
        '    lobval = "0"
        'End If
        'If DropDownlob.SelectedItem.Text = "---select---" Or DropDownlob.SelectedItem.Text = "0" Then
        '    DropDownlob.SelectedItem.Text = "0"
        '    lobval = "0"
        'End If
        'Dim localstatus As String = ""
        'If chklocal.Checked = True Then
        '    localstatus = "Local"
        'Else
        '    localstatus = "NonLocal"
        'End If
        cmd = New SqlCommand("insert into dataanalysishtmlreport values('BMPEPL','60','Deveopment_Team','0','IDMS_Application','0',@ReportName,@SavedBy,@repname,'local')", con)
        'cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 50)
        'cmd.Parameters("@DeptName").Value = "BMPEPL"
        'cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50)
        'cmd.Parameters("@DeptId").Value = 60
        'cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50)
        'cmd.Parameters("@ClientName").Value = "Development_Team"
        'cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50)
        'cmd.Parameters("@ClientId").Value = 36
        'cmd.Parameters.Add("@LobName", SqlDbType.VarChar, 50)
        'cmd.Parameters("@LobName").Value = "IDMS_Application"
        'cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50)
        'cmd.Parameters("@LobId").Value = 29
        cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
        cmd.Parameters("@ReportName").Value = textreportname.Text
        cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
        cmd.Parameters("@SavedBy").Value = Session("userid")
        cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
        cmd.Parameters("@repname").Value = ddlTable.SelectedItem.Text
        'cmd.Parameters.Add("@local", SqlDbType.VarChar, 50)
        'cmd.Parameters("@local").Value = localstatus
        '
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        'Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where HTMLReport='" + textreportname.Text + "' and Action='Save' and Entity='HTMLReport'", con)
        'con.Open()
        'cmm.ExecuteNonQuery()
        'con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        aspnet_msgbox("Report Has Been Saved Successfully")
        divsavereport.Visible = False
        'chklocal.Checked = False

        'If DropDownclient.SelectedItem.Text = "0" Then

        '    DropDownclient.SelectedItem.Text = "---select---"



        'End If
        'If DropDownlob.SelectedItem.Text = "0" Then
        '    DropDownlob.SelectedItem.Text = "---select---"

        'End If
        strdivreport.Value = ""
        'DropDowndept.SelectedIndex = 0
        'DropDownclient.Items.Clear()
        'DropDownlob.Items.Clear()
        'cmd = New SqlCommand("drop table R566R", con)
        'con.Open()
        'cmd.ExecuteNonQuery()
        'con.Close()

        'groupcolumns.Items.Clear()
        'allcolumns.Items.Clear()
        textreportname.Text = ""
        'Me.result.InnerHtml = ""
        'Me.relax.InnerHtml = ""
        'report.InnerHtml = ""
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        SetFocus(ddlTable)

    End Sub

    Protected Sub level1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If (DepartmentName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level1")
        End If
        con.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ClientName.DataSource = dr
        ClientName.DataTextField = "ClientName"
        ClientName.DataValueField = "autoid"
        ClientName.DataBind()
        ClientName.Items.Insert(0, " --Select-- ")
    End Sub

    Protected Sub level2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If (ClientName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level2")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ddlLobName.DataSource = dr
        ddlLobName.DataTextField = "LOBName"
        ddlLobName.DataValueField = "autoid"
        ddlLobName.DataBind()
        ddlLobName.Items.Insert(0, "-- select --")
    End Sub

    Protected Sub level3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        If (ddlLobName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level3")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId='" + DepartmentName.SelectedValue + "' and ClientId='" + ClientName.SelectedValue + "' and LOBId='" + ddlLobName.SelectedValue + "' and createdBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "Tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--select--")
    End Sub

    Protected Sub Gettable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gettable.Click
        Dim cmdgettable As New SqlCommand("select Tableid,TableName from WARSLOBTableMaster where  CreatedBy ='" + Session("userid").ToString() + "' ", connection)
        Dim dsgettable As New DataSet
        Dim adpgettable As New SqlDataAdapter
        adpgettable.SelectCommand = cmdgettable
        connection.Open()
        adpgettable.Fill(dsgettable)
        connection.Close()
        ddlTable.DataSource = dsgettable
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--Select--")

    End Sub

    Protected Sub getcols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles getcols_table.Click
        con.Close()
        con.Open()
        Dim com1 As New SqlCommand("select Visiblecolumn from WARSLOBTableMaster where  TableName='" + ddlTable.SelectedItem.Text + "'", con)
        readquery1 = com1.ExecuteReader
        While readquery1.Read()
            colname = readquery1("Visiblecolumn")

        End While
        com1.Dispose()
        colarray = colname.Split(",")
        Dim colcount As Integer
        colcount = UBound(colarray)
        Dim i = 0
        For i = 1 To colarray.Length
            repcols.Items.Add(New ListItem(colarray(i - 1)))
            repcols.DataBind()
        Next
    End Sub

    Protected Sub SAVE_multiuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SAVE_multiuser.Click
        If textreportname.Text.Contains("(") Or textreportname.Text.Contains(")") Or textreportname.Text.Contains("~") Or textreportname.Text.Contains("`") Or textreportname.Text.Contains("$") Or textreportname.Text.Contains("#") Or textreportname.Text.Contains("@") Or textreportname.Text.Contains("!") Or textreportname.Text.Contains("%") Or textreportname.Text.Contains("^") Or textreportname.Text.Contains("&") Or textreportname.Text.Contains("*") Or textreportname.Text.Contains("-") Or textreportname.Text.Contains("+") Or textreportname.Text.Contains(":") Or textreportname.Text.Contains(";") Or textreportname.Text.Contains(",") Or textreportname.Text.Contains(".") Or textreportname.Text.Contains("/") Or textreportname.Text.Contains("?") Or textreportname.Text.Contains("|") Or textreportname.Text.Contains("\") Or textreportname.Text.Contains("{") Or textreportname.Text.Contains("}") Or textreportname.Text.Contains("[") Or textreportname.Text.Contains("]") Or textreportname.Text.Contains(" ") Or textreportname.Text.Contains("<") Or textreportname.Text.Contains(">") Then

            aspnet_msgbox("No Special Symbol Is Allowed")
            Exit Sub
        End If
        If DropDowndept.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("Select Department")
            Exit Sub
        End If
        Dim repname As String = ""
        cmd = New SqlCommand("select ReportName as ReportName from dataanalysishtmlreport", con)
        con.Open()
        readquery = cmd.ExecuteReader
        While readquery.Read()

            repname = readquery("ReportName")
            If repname = textreportname.Text Then
                aspnet_msgbox("Choose Another Report Name")
                textreportname.Focus()
                Exit Sub
            End If

        End While
        readquery.Close()
        con.Close()

        If textreportname.Text = "" Then
            aspnet_msgbox("Fill Report Name")
            textreportname.Focus()
            Exit Sub
        End If
        strdivreport.Value = report.InnerHtml.ToString
        If strdivreport.Value <> "" Then
            'txtstrdiv.Value = strpretags & txtstrdiv.Value & strposttags
            Dim str As String = ""
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("html/")) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("html/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->


            Dim Path = "html/" & textreportname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))
            'change
            fp.WriteLine(result.InnerHtml)
            fp.WriteLine(headdiv.InnerHtml)
            fp.WriteLine(strdivreport.Value)
            'fp.WriteLine(Divhead.InnerHtml)
            'fp.WriteLine(relax.InnerHtml)



            fp.Close()
        End If
        Dim deptval As String = ""
        Dim clientval As String = ""
        Dim lobval As String = ""
        deptval = DropDowndept.SelectedValue.ToString
        clientval = DropDownclient.SelectedValue.ToString
        lobval = DropDownlob.SelectedValue.ToString
        If DropDowndept.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("Select Department First")
            Exit Sub
        End If
        If DropDownclient.SelectedItem.Text = "---select---" Then

            DropDownclient.SelectedItem.Text = "0"
            clientval = "0"

            DropDownlob.SelectedItem.Text = "0"
            lobval = "0"
        End If
        If DropDownlob.SelectedItem.Text = "---select---" Or DropDownlob.SelectedItem.Text = "0" Then
            DropDownlob.SelectedItem.Text = "0"
            lobval = "0"
        End If
        Dim localstatus As String = ""
        'If chklocal.Checked = True Then
        'localstatus = "Local"
        'Else
        localstatus = "NonLocal"
        'End If
        cmd = New SqlCommand("insert into dataanalysishtmlreport values(@DeptName,@DeptId,@ClientName,@ClientId,@LobName,@LobId,@ReportName,@SavedBy,@repname,@local)", con)
        cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 50)
        cmd.Parameters("@DeptName").Value = DropDowndept.SelectedItem.Text
        cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50)
        cmd.Parameters("@DeptId").Value = deptval
        cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50)
        cmd.Parameters("@ClientName").Value = DropDownclient.SelectedItem.Text
        cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50)
        cmd.Parameters("@ClientId").Value = clientval
        cmd.Parameters.Add("@LobName", SqlDbType.VarChar, 50)
        cmd.Parameters("@LobName").Value = DropDownlob.SelectedItem.Text
        cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50)
        cmd.Parameters("@LobId").Value = lobval
        cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
        cmd.Parameters("@ReportName").Value = textreportname.Text
        cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
        cmd.Parameters("@SavedBy").Value = Session("userid")
        cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
        cmd.Parameters("@repname").Value = ddlTable.SelectedItem.Text
        cmd.Parameters.Add("@local", SqlDbType.VarChar, 50)
        cmd.Parameters("@local").Value = localstatus
        '
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where HTMLReport='" + textreportname.Text + "' and Action='Save' and Entity='HTMLReport'", con)
        con.Open()
        cmm.ExecuteNonQuery()
        con.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        aspnet_msgbox("Report Has Been Saved Successfully")
        divsavereport.Visible = False
        'chklocal.Checked = False

        If DropDownclient.SelectedItem.Text = "0" Then

            DropDownclient.SelectedItem.Text = "---select---"



        End If
        If DropDownlob.SelectedItem.Text = "0" Then
            DropDownlob.SelectedItem.Text = "---select---"

        End If
        strdivreport.Value = ""
        DropDowndept.SelectedIndex = 0
        DropDownclient.Items.Clear()
        DropDownlob.Items.Clear()
        'cmd = New SqlCommand("drop table R566R", con)
        'con.Open()
        'cmd.ExecuteNonQuery()
        'con.Close()

        'groupcolumns.Items.Clear()
        'allcolumns.Items.Clear()
        textreportname.Text = ""
        'Me.result.InnerHtml = ""
        'Me.relax.InnerHtml = ""
        'report.InnerHtml = ""

    End Sub

    Protected Sub DropDowndept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDowndept.SelectedIndexChanged
        If (DropDowndept.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level1")
        End If
        con.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DropDowndept.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        DropDownclient.DataSource = dr
        DropDownclient.DataTextField = "ClientName"
        DropDownclient.DataValueField = "autoid"
        DropDownclient.DataBind()
        DropDownclient.Items.Insert(0, "--select--")
    End Sub

    Protected Sub DropDownclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownclient.SelectedIndexChanged
        If (DropDownclient.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level2")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DropDowndept.SelectedValue + "' and  clientid= '" + DropDownclient.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        DropDownlob.DataSource = dr
        DropDownlob.DataTextField = "LOBName"
        DropDownlob.DataValueField = "autoid"
        DropDownlob.DataBind()
        DropDownlob.Items.Insert(0, "--select--")
    End Sub

    Protected Sub DropDownlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownlob.SelectedIndexChanged
        textreportname.Enabled = True
    End Sub

    

    

    
End Class
