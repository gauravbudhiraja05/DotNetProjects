Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class ReportDesigner_ArchiveReport
    Inherits System.Web.UI.Page
    Dim fun As New Functions
    Dim selectRep As New ReportDesigner
    Dim dsRep As New DataSet
    Dim dsProcess As New DataSet
    Dim daClient As New SqlDataAdapter
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Public dept As String = "0"
    Public client As String = "0"
    Public lob As String = "0"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        conn.Open()
        'If Me.IsPostBack = False Then
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", conn)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                Me.btnArchivemul.Visible = True
                rdr.Close()
                Dim cmdnew As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmdnew = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", conn)
                Else
                    cmdnew = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", conn)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmdnew)
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
                cmdnew.Dispose()
                daar.Dispose()
            Else
                Me.spandisplay.Visible = False
                Me.btnArchive.Visible = True
                If (Page.IsPostBack = False) Then
                    ddlReport.DataTextField = "QueryName"
                    ddlReport.DataValueField = "Recordid"
                    ddlReport.DataSource = selectRep.reportForuser(Session("userid"))
                    ddlReport.DataBind()
                    If ddlReport.Items.Count > 0 Then
                        ddlReport.Items.Insert(0, "--Select--")
                    End If
                End If
            End If
                conn.Close()
        End If
        cmd.Dispose()
        rdr.Close()
        Dim typeofuser = Session("typeofuser")
        conn.Open()
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
        rdr.Close()
        cmd.Dispose()
        conn.Close()
        'End If
    End Sub
    
    Protected Sub btnArchive_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchive.Click
        Dim cudate As String = System.DateTime.Today.ToString()
        lblMsg.Text = ""
        'By Mohit Tyagi on 14-Aug-2012

        If ddlReport.SelectedIndex = 0 Then
            lblMsg.Text = "Please Select Report."
            Exit Sub
        ElseIf ddlReport.Items.Count = 0 Then
            lblMsg.Text = "No Report Found"
            Exit Sub
        Else
            Dim str As String = selectRep.reportArchive(Session("userid"), ddlReport.SelectedItem.ToString())
            If (str = "1") Then
                lblMsg.Text = "Report is archived successfully. You can view this report through Data Manager"
                ddlReport.DataTextField = "QueryName"
                ddlReport.DataValueField = "Recordid"
                ddlReport.DataSource = selectRep.reportForuser(Session("userid"))
                ddlReport.DataBind()
                If ddlReport.Items.Count > 0 Then
                    ddlReport.Items.Insert(0, "--Select--")
                End If
            Else
                lblMsg.Text = "Error occured. Please try again."
            End If
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        lblMsg.Text = ""
        ddlReport.Items.Clear()
        Me.ddlclient.Items.Clear()
        Me.ddlLob.Items.Clear()
        If ddlDepartment.SelectedIndex <> 0 Then
            Me.ddlLob.Items.Clear()
            Me.ddlclient.DataSource = fun.bind_client(ddlDepartment.SelectedValue)
            Me.ddlclient.DataTextField = "ClientName"
            Me.ddlclient.DataValueField = "AutoID"
            Me.ddlclient.DataBind()
            Me.ddlclient.Items.Insert("0", "--Select--")
            Me.ddlclient.Dispose()
            dsProcess.Dispose()
            conn.Close()

            dept = ddlDepartment.SelectedValue
            client = "0"
            lob = "0"
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                ' ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If
            Else
adminOutofIndex:
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)

            End If

            ddlReport.DataBind()
            If ddlReport.Items.Count > 0 Then
                ddlReport.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub

    Protected Sub ddlclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlclient.SelectedIndexChanged
        lblMsg.Text = ""
        Me.ddlLob.Items.Clear()
        ddlReport.Items.Clear()
        If ddlDepartment.SelectedIndex <> 0 And ddlclient.SelectedIndex <> 0 Then
            Dim classobj As New Functions
            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "AutoID"
            ddlLob.DataSource = fun.bind_lob(ddlDepartment.SelectedValue, ddlclient.SelectedValue)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "--Select--")

            dept = ddlDepartment.SelectedValue
            client = ddlclient.SelectedValue
            lob = "0"
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                ' ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If
            Else
adminOutofIndex:
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)

            End If

            ddlReport.DataBind()
            If ddlReport.Items.Count > 0 Then
                ddlReport.Items.Insert(0, "--Select--")
            End If

        End If
    End Sub

    Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
        lblMsg.Text = ""
        ddlReport.Items.Clear()
        If ddlLob.SelectedItem.Text <> "--Select--" Then

            dept = ddlDepartment.SelectedValue
            client = ddlclient.SelectedValue
            lob = ddlLob.SelectedValue
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                ' ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                Dim exist As Boolean = False
                'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                'If exist = True Then
                ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                'Else
                '    GoTo adminOutofIndex
                'End If
            Else
adminOutofIndex:
                ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)

            End If

            ddlReport.DataBind()
            If ddlReport.Items.Count > 0 Then
                ddlReport.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub

    Protected Sub btnArchivemul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnArchivemul.Click
        Dim cudate As String = System.DateTime.Today.ToString()
        lblMsg.Text = ""
        If ddlDepartment.SelectedIndex = 0 Then
            lblMsg.Text = "Please select department."
        Else
            dept = ddlDepartment.SelectedValue
            If ddlclient.SelectedIndex <> 0 Then
                client = ddlclient.SelectedValue
                If ddlLob.SelectedIndex <> 0 Then
                    lob = ddlLob.SelectedValue
                End If
            End If
            If ddlReport.SelectedIndex = 0 Then
                lblMsg.Text = "Please Select Report."
                Exit Sub
            ElseIf ddlReport.Items.Count = 0 Then
                lblMsg.Text = "No Report Found"
                Exit Sub
            Else
                Dim str As String = selectRep.reportArchiveForMul(Session("userid"), cudate, ddlReport.SelectedItem.ToString(), dept, client, lob)
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(NewReportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + ddlReport.SelectedItem.ToString() + "' and Action='Archived'", conn)
                conn.Open()
                cmm.ExecuteNonQuery()
                conn.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                If (str = "1") Then
                    lblMsg.Text = "Report is archived successfully. You can view this report through Data Manager"
                    ddlReport.DataTextField = "QueryName"
                    ddlReport.DataValueField = "Recordid"
                    If (Session("typeofuser") = "Admin") Then
                        ' ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                        Dim exist As Boolean = False
                        'exist = selectRep.chkAdminSpan(Session("userid"), dept, client, lob)
                        'If exist = True Then
                        ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                        'Else
                        '    GoTo adminOutofIndex
                        'End If
                    Else
adminOutofIndex:
                        ddlReport.DataSource = selectRep.reportFornonlocalMulUser(Session("userid"), dept, client, lob)

                    End If

                    ddlReport.DataBind()
                    If ddlReport.Items.Count > 0 Then
                        ddlReport.Items.Insert(0, "--Select--")
                    End If
                Else
                    lblMsg.Text = "Error occured. Please try again."
                End If
            End If
        End If
    End Sub
End Class
