Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class ReportDesigner_OpenReport
    Inherits System.Web.UI.Page
    Dim fun As New Functions
    Dim selectRep As New ReportDesigner
    Dim dsRep As New DataSet
    Dim dsProcess As New DataSet
    Dim daClient As New SqlDataAdapter
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Public dept As String = "0"
    Public client As String = "0"
    Public lob As String = "0"
    ''' <summary>
    '''  These Global variables are defined to store the data of a report.
    '''  This data is to pass to the parent window.
    ''' The data is passed through a client side function named as assignToparent().
    ''' </summary>
    ''' <remarks></remarks>
    Public repName As String = ""
    Public reportId As String = ""
    Public repType As String = "Simple"
    Public hidDpos As String = ""
    Public hidTables As String = ""
    Public hidWhere As String = ""
    Public hidGroupby As String = ""
    Public hidOrderby As String = ""
    Public hidHaving As String = ""
    Public hidColorcondition As String = ""
    Public hidDetailsformat As String = ""
    Public hidReporttype As String = ""
    Public hidReportscope As String = ""
    Public hidDatetable As String = ""
    Public hidDformat As String = ""
    Public hidHeight As String = ""
    Public hidHeaderformat As String = ""
    Public hidHpos As String = ""
    Public hidHformat As String = ""
    Public hidHformula As String = ""
    Public hidHcolorcon As String = ""
    Public hidFooterformat As String = ""
    Public hidFpos As String = ""
    Public hidFformat As String = ""
    Public hidFformula As String = ""
    Public hidFcolorcon As String = ""
    Public author As String = ""
    Public hidDFormula As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conn.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", conn)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                Me.btnDonemul.Visible = True
                rdr.Close()
                Dim cmd As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", conn)
                Else
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", conn)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    Dim val1 As String
                    Dim val2 As String
                    Dim val3 As String
                    val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                    lbl1.Text = val1
                    lbl2.Text = val2
                    lbl3.Text = val3
                   End If
            Else
                Me.spandisplay.Visible = False
                Me.btnDone.Visible = True
            End If
        End If
        rdr.Close()
        cmd.Dispose()
        Dim typeofuser = Session("typeofuser")
        If (Me.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", conn)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                ddlDepartment.DataSource = dsgetdept
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "autoid"
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", conn)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                 adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                ddlDepartment.DataSource = dsgetdept
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "autoid"
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", conn)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                 adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                ddlDepartment.DataSource = dsgetdept
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "autoid"
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
            End If
        End If

        If Me.IsPostBack = False Then
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            ddlReport.DataSource = selectRep.reportForuser(Session("userid"))
            ddlReport.DataBind()
            If ddlReport.Items.Count > 0 Then
                ddlReport.Items.Insert(0, "--Select--")
            End If
        End If

    End Sub

    Protected Sub btnDone_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDone.Click
        If Session("userid") = "" Then
            lblMsg.Text = "Session Expired. Login Again"
            Exit Sub
        End If
        lblMsg.Text = ""

        If ddlReport.SelectedIndex = 0 Then
            lblMsg.Text = "Please Select Report."
        ElseIf ddlReport.Items.Count = 0 Then
            lblMsg.Text = "No Report Found"
        Else
            'Dim pp = Replace(btnEx.Value, "aa", "")
            Dim objcmd As New SqlCommand()
            Dim reader As SqlDataReader
            author = Session("userid")

            Dim sqlString As String = "Select idmsreportmaster.recordid,idmsreportmaster.ColName, idmsreportmaster.TableName as TableName,idmsreportmaster.Txtformula as hidDFormula,idmsreportmaster.WhereData as WhereData,idmsreportmaster.GroupBy as GroupBy,idmsreportmaster.OrderBy as OrderBy,idmsreportmaster.HavingCondition as HavingCondition,idmsreportmaster.ColorCondition as ColorCondition,idmsreportmaster.ReportFormat as ReportFormat,idmsreportmaster.ReportType as ReportType,idmsreportmaster.ReportScope as ReportScope,idmsreportmaster.DateConTable as DateconTable,idmsreportmaster.columnFormat as columnFormat,idmsreportheadermaster.HeaderHeight as hheight,idmsreportheadermaster.HeaderFormat as hformat,idmsreportheadermaster.HeaderColumns as hcolumns,idmsreportheadermaster.ColumnFormat as hcolformat,idmsreportheadermaster.ColumnFormula as hcolformula,idmsreportheadermaster.ColorCondition as hcolcon,idmsreportfootermaster.FooterHeight as fheight,idmsreportfootermaster.FooterFormat as fformat,idmsreportfootermaster.FooterColumns as fcolumns,idmsreportfootermaster.ColumnFormat as fcolformat,idmsreportfootermaster.ColumnFormula as fcolformula,idmsreportfootermaster.ColorCondition as fcolcon from idmsreportmaster,idmsreportheadermaster,idmsreportfootermaster where  idmsreportmaster.queryname='" & ddlReport.SelectedItem.ToString() & "' and idmsreportmaster.recordid=idmsreportheadermaster.repid and  idmsreportmaster.recordid=idmsreportfootermaster.repid"
            objcmd = New SqlCommand(sqlString, conn)
            conn.Close()
            conn.Open()
            reader = objcmd.ExecuteReader()
            While reader.Read
                reportId = reader("recordid").ToString()
                Dim bnm = Replace(reader("ColName").ToString(), vbNewLine, "")
                bnm = Replace(bnm, " As ", " AS ")
                bnm = Replace(bnm, " as ", " AS ")
                '''''' for old ill-format reports of phase1
                If bnm <> "" Then
                    If bnm.Contains("$As$") = True Or bnm.Contains("String.fromCharCode(34)") = True Or bnm.Contains("$+$") = True Or bnm.Contains("$as$") = True Then
                        bnm = Replace(bnm, "$as$", " AS ")
                        bnm = Replace(bnm, "$As$", " AS ")
                        bnm = Replace(bnm, "+$String.fromCharCode(34)$+", "'")
                        bnm = Replace(bnm, "String.fromCharCode(34)", "'")
                        bnm = Replace(bnm, "$", " ")
                    End If
                End If
                '''''''''''''''''''''''''''''''''''''''''''''
                hidDpos = bnm
                hidTables = Replace(reader("TableName").ToString(), ",", "~")
                hidWhere = reader("WhereData").ToString()
                hidGroupby = reader("GroupBy").ToString()
                hidOrderby = reader("OrderBy").ToString()
                hidHaving = reader("HavingCondition").ToString()
                hidColorcondition = reader("ColorCondition").ToString()
                hidDetailsformat = reader("ReportFormat").ToString()
                hidReporttype = reader("ReportType").ToString()
                hidReportscope = reader("ReportScope").ToString()
                hidDatetable = reader("DateConTable").ToString()
                hidDformat = reader("columnFormat").ToString()
                'hidDFormula = reader("hidDFormula").ToString()
                hidHeight = reader("hheight").ToString() + "," + reader("fheight").ToString()

                hidHeaderformat = reader("hformat").ToString()
                hidHpos = reader("hcolumns").ToString()
                hidHformat = reader("hcolformat").ToString()
                hidHformula = reader("hcolformula").ToString()
                hidHcolorcon = reader("hcolcon").ToString()

                hidFooterformat = reader("fformat").ToString()
                hidFpos = reader("fcolumns").ToString()
                hidFformat = reader("fcolformat").ToString()
                hidFformula = reader("fcolformula").ToString()
                hidFcolorcon = reader("fcolcon").ToString()

            End While
            conn.Close()
            '''' extract formulas
            Dim formu = ""
            Dim elem = ""
            Dim jk = Split(hidDpos, "~")
            Dim l = 0
            For l = 0 To jk.Length - 1
                Dim lk = Split(jk(l), " AS ")

                If lk.Length > 1 Then
                    Dim ghj = Trim(lk(1))
                    If lk(1).Contains("[") = False Then
                        ghj = "[" + ghj + "]"
                    End If
                    If elem = "" Then
                        elem = ghj
                    Else
                        elem = elem + "~" + ghj
                    End If
                    ''
                    If formu = "" Then
                        formu = lk(0) + " AS " + ghj
                    Else
                        formu = formu + "~" + lk(0) + " AS " + ghj
                    End If
                Else
                    If elem = "" Then
                        elem = jk(l)
                    Else
                        elem = elem + "~" + jk(l)
                    End If
                End If
            Next
            '''''''''''''''''''''''''''
            hidDFormula = formu
            hidDpos = elem

            Session("repName") = ddlReport.SelectedItem.Text
            repName = ddlReport.SelectedItem.ToString()
            Dim str = selectRep.trackOpenreport(Session("userid"), System.DateTime.Today.ToString(), ddlReport.SelectedItem.ToString(), dept, client, lob)
            '''''''''''''''Usertype check for track goes here:- By Suvidha

            'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + ddlReport.SelectedItem.ToString() + "' and Action='View'", conn)
            'conn.Open()
            'cmm.ExecuteNonQuery()
            'conn.Close()
            '''''''''''''''Usertype check for track goes here:- By Suvidha
            'Session("repName") = repName
            callParent()
        End If
    End Sub
    ''' <summary>
    ''' This function calls the client side function,
    ''' which actually transfers the data of the selected report to the parent report.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub callParent()
        Dim str As String = ""
        str = "<script launguage=Javascript>"
        str = str + "assignToparent();"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "assignToparent", str)
    End Sub
    ''' <summary>
    ''' This function is used to show an alert
    ''' </summary>
    ''' <param name="strPassed"></param>
    ''' <returns>true</returns>
    ''' <remarks></remarks>
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", Script)
        Return True
    End Function

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Session("userid") = "" Then
            lblMsg.Text = "Session Expired. Login Again"
            Exit Sub
        End If
        lblMsg.Text = ""

            If ddlReport.SelectedIndex = 0 Then
                lblMsg.Text = "Please Select Report."
                Exit Sub
            ElseIf ddlReport.Items.Count = 0 Then
                lblMsg.Text = "No Report Found"
                Exit Sub
            Else
            Dim b As String = selectRep.CheckExistingReport(Me.ddlReport.SelectedItem.Text) ' fetch the owner of the report
                Dim myRight As String = ""
                If b <> Session("userid") Then
                    Dim rights As String = selectRep.fetchReportrights(Me.ddlReport.SelectedValue, Session("userid"))
                    Dim sp = Split(rights, ",")
                    If sp.length > 2 Then
                        myRight = sp(2)
                    End If

                End If
                If b = Session("userid") Or myRight = "True" Then

                Dim str = selectRep.deleteReport(Me.ddlReport.SelectedItem.ToString())
                    ' To track
                    selectRep.toTrackfordelete(Session("userid"), System.DateTime.Today.ToShortDateString(), Me.ddlReport.SelectedItem.ToString(), dept, client, lob)

                    If (str = "1") Then
                        ShowConfirm("Report Deleted successfully")
                        Session("repName") = ""
                        '' Reload report names

                    Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                    If (scope = "Local") Then
                        ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                    Else
                        ddlReport.DataSource = selectRep.reportFornonlocal(Session("userid"))

                    End If
                ddlReport.DataBind()
                If ddlReport.Items.Count > 0 Then
                    ddlReport.Items.Insert(0, "--Select--")
                End If
            Else
                lblMsg.Text = "Error occurred. Please try again."
            End If
                Else
                    lblMsg.Text = "You don't have rights to delete this report."
                End If
            End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        lblMsg.Text = ""
        ddlReport.Items.Clear()
        ddlLob.Items.Clear()
        ddlClient.Items.Clear()
        If ddlDepartment.SelectedIndex <> 0 Then
            Me.ddlLob.Items.Clear()
            Me.ddlClient.DataSource = fun.bind_client(ddlDepartment.SelectedValue)
            Me.ddlClient.DataTextField = "ClientName"
            Me.ddlClient.DataValueField = "AutoID"
            Me.ddlClient.DataBind()
            Me.ddlClient.Items.Insert("0", "--Select--")
            Me.ddlClient.Dispose()
            dsProcess.Dispose()
            conn.Close()
            ddlReport.Items.Clear()
            dept = ddlDepartment.SelectedValue
            client = "0"
            lob = "0"
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If

            ElseIf (Session("typeofuser") = "Super Admin") Then
                ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
            Else
adminOutofIndex:
                'Dim scope = Trim(selectRep.chkUserscope(Session("userid"), dept, client, lob))
                'If (scope = "Local") Then
                '    ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                'Else
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)
            End If
        End If
        ddlReport.DataBind()
        If ddlReport.Items.Count > 0 Then
            ddlReport.Items.Insert(0, "--Select--")
        End If
        'End If
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        lblMsg.Text = ""
        ddlReport.Items.Clear()
        ddlLob.Items.Clear()
        If ddlDepartment.SelectedIndex <> 0 And ddlClient.SelectedIndex <> 0 Then
            Dim classobj As New Functions
            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "AutoID"
            ddlLob.DataSource = fun.bind_lob(ddlDepartment.SelectedValue, ddlClient.SelectedValue)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "--Select--")
            dept = ddlDepartment.SelectedValue
            client = ddlClient.SelectedValue
            lob = "0"
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                'Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If

            ElseIf (Session("typeofuser") = "Super Admin") Then
                ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
            Else
adminOutofIndex:
                'Dim scope = Trim(selectRep.chkUserscope(Session("userid"), dept, client, lob))
                'If (scope = "Local") Then
                'ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                'Else
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)
            End If
        End If
        ddlReport.DataBind()
        If ddlReport.Items.Count > 0 Then
            ddlReport.Items.Insert(0, "--Select--")
        End If

        'End If
    End Sub

    Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
        lblMsg.Text = ""
        If ddlLob.SelectedItem.Text <> "--Select--" Then
            ddlReport.Items.Clear()
            dept = ddlDepartment.SelectedValue
            client = ddlClient.SelectedValue
            lob = ddlLob.SelectedValue
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If

            ElseIf (Session("typeofuser") = "Super Admin") Then
                ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
            Else
adminOutofIndex:
                'Dim scope = Trim(selectRep.chkUserscope(Session("userid"), dept, client, lob))
                'If (scope = "Local") Then
                '    ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                'Else
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)
            End If
        End If
        ddlReport.DataBind()
        If ddlReport.Items.Count > 0 Then
            ddlReport.Items.Insert(0, "--Select--")
        End If
        'End If
    End Sub

    Protected Sub btnDonemul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDonemul.Click
        conn.Close()
        If Session("userid") = "" Then
            lblMsg.Text = "Session Expired. Login Again"
            Exit Sub
        End If
        lblMsg.Text = ""
        If ddlDepartment.SelectedIndex = 0 Then
            lblMsg.Text = "Please select department."
        Else
            dept = ddlDepartment.SelectedValue
            If ddlClient.SelectedIndex <> 0 Then
                client = ddlClient.SelectedValue
                If ddlLob.SelectedIndex <> 0 Then
                    lob = ddlLob.SelectedValue
                End If
            End If
            If ddlReport.SelectedIndex = 0 Then
                lblMsg.Text = "Please Select Report."
            ElseIf ddlReport.Items.Count = 0 Then
                lblMsg.Text = "No Report Found"
            Else
                'Dim pp = Replace(btnEx.Value, "aa", "")
                Dim objcmd As New SqlCommand()
                Dim reader As SqlDataReader
                author = Session("userid")

                Dim sqlString As String = "Select idmsreportmaster.recordid,idmsreportmaster.ColName, idmsreportmaster.TableName as TableName,idmsreportmaster.Txtformula as hidDFormula,idmsreportmaster.WhereData as WhereData,idmsreportmaster.GroupBy as GroupBy,idmsreportmaster.OrderBy as OrderBy,idmsreportmaster.HavingCondition as HavingCondition,idmsreportmaster.ColorCondition as ColorCondition,idmsreportmaster.ReportFormat as ReportFormat,idmsreportmaster.ReportType as ReportType,idmsreportmaster.ReportScope as ReportScope,idmsreportmaster.DateConTable as DateconTable,idmsreportmaster.columnFormat as columnFormat,idmsreportheadermaster.HeaderHeight as hheight,idmsreportheadermaster.HeaderFormat as hformat,idmsreportheadermaster.HeaderColumns as hcolumns,idmsreportheadermaster.ColumnFormat as hcolformat,idmsreportheadermaster.ColumnFormula as hcolformula,idmsreportheadermaster.ColorCondition as hcolcon,idmsreportfootermaster.FooterHeight as fheight,idmsreportfootermaster.FooterFormat as fformat,idmsreportfootermaster.FooterColumns as fcolumns,idmsreportfootermaster.ColumnFormat as fcolformat,idmsreportfootermaster.ColumnFormula as fcolformula,idmsreportfootermaster.ColorCondition as fcolcon from idmsreportmaster,idmsreportheadermaster,idmsreportfootermaster where  idmsreportmaster.queryname='" & ddlReport.SelectedItem.ToString() & "' and idmsreportmaster.departmentid='" & dept & "' and idmsreportmaster.clientid='" & client & "' and idmsreportmaster.underlob='" & lob & "' and idmsreportmaster.recordid=idmsreportheadermaster.repid and  idmsreportmaster.recordid=idmsreportfootermaster.repid"
                objcmd = New SqlCommand(sqlString, conn)
                conn.Open()
                reader = objcmd.ExecuteReader()
                While reader.Read
                    reportId = reader("recordid").ToString()
                    Dim bnm = Replace(reader("ColName").ToString(), vbNewLine, "")
                    bnm = Replace(bnm, " As ", " AS ")
                    bnm = Replace(bnm, " as ", " AS ")
                    '''''' for old ill-format reports of phase1
                    If bnm <> "" Then
                        If bnm.Contains("$As$") = True Or bnm.Contains("String.fromCharCode(34)") = True Or bnm.Contains("$+$") = True Or bnm.Contains("$as$") = True Then
                            bnm = Replace(bnm, "$as$", " AS ")
                            bnm = Replace(bnm, "$As$", " AS ")
                            bnm = Replace(bnm, "+$String.fromCharCode(34)$+", "'")
                            bnm = Replace(bnm, "String.fromCharCode(34)", "'")
                            bnm = Replace(bnm, "$", " ")
                        End If
                    End If
                    '''''''''''''''''''''''''''''''''''''''''''''
                    hidDpos = bnm
                    hidTables = Replace(reader("TableName").ToString(), ",", "~")
                    hidWhere = reader("WhereData").ToString()
                    hidGroupby = reader("GroupBy").ToString()
                    hidOrderby = reader("OrderBy").ToString()
                    hidHaving = reader("HavingCondition").ToString()
                    hidColorcondition = reader("ColorCondition").ToString()
                    hidDetailsformat = reader("ReportFormat").ToString()
                    hidReporttype = reader("ReportType").ToString()
                    hidReportscope = reader("ReportScope").ToString()
                    hidDatetable = reader("DateConTable").ToString()
                    hidDformat = reader("columnFormat").ToString()
                    'hidDFormula = reader("hidDFormula").ToString()
                    hidHeight = reader("hheight").ToString() + "," + reader("fheight").ToString()

                    hidHeaderformat = reader("hformat").ToString()
                    hidHpos = reader("hcolumns").ToString()
                    hidHformat = reader("hcolformat").ToString()
                    hidHformula = reader("hcolformula").ToString()
                    hidHcolorcon = reader("hcolcon").ToString()

                    hidFooterformat = reader("fformat").ToString()
                    hidFpos = reader("fcolumns").ToString()
                    hidFformat = reader("fcolformat").ToString()
                    hidFformula = reader("fcolformula").ToString()
                    hidFcolorcon = reader("fcolcon").ToString()

                End While
                conn.Close()
                '''' extract formulas
                Dim formu = ""
                Dim elem = ""
                Dim jk = Split(hidDpos, "~")
                Dim l = 0
                For l = 0 To jk.Length - 1
                    Dim lk = Split(jk(l), " AS ")

                    If lk.Length > 1 Then
                        Dim ghj = Trim(lk(1))
                        If lk(1).Contains("[") = False Then
                            ghj = "[" + ghj + "]"
                        End If
                        If elem = "" Then
                            elem = ghj
                        Else
                            elem = elem + "~" + ghj
                        End If
                        ''
                        If formu = "" Then
                            formu = lk(0) + " AS " + ghj
                        Else
                            formu = formu + "~" + lk(0) + " AS " + ghj
                        End If
                    Else
                        If elem = "" Then
                            elem = jk(l)
                        Else
                            elem = elem + "~" + jk(l)
                        End If
                    End If
                Next
                '''''''''''''''''''''''''''
                hidDFormula = formu
                hidDpos = elem

                Session("repName") = ddlReport.SelectedItem.Text
                repName = ddlReport.SelectedItem.ToString()
                Dim str = selectRep.trackOpenreport(Session("userid"), System.DateTime.Today.ToString(), ddlReport.SelectedItem.ToString(), dept, client, lob)
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + ddlReport.SelectedItem.ToString() + "' and Action='View'", conn)
                conn.Open()
                cmm.ExecuteNonQuery()
                conn.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                'Session("repName") = repName
                callParent()
            End If
        End If
    End Sub
End Class
