Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class ReportDesigner_getTables
    Inherits System.Web.UI.Page
    Dim fun As New Functions
    Dim dAdapter As New SqlDataAdapter
    Dim dsProcess As New DataSet
    Dim daClient As New SqlDataAdapter
    Dim selectRep As New ReportDesigner
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim StrTableName As String = ""
    Dim dept As String = ""
    Dim client As String = ""
    Dim cmdnew As SqlCommand
    Dim rdrnew As SqlDataReader
    Dim lob As String = ""
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblMsg.Text = ""
        Page.SmartNavigation = True
        If Me.IsPostBack = False Then
            Me.hidSource.Value = Request("src").ToString()
            'Me.hidUsertype.Value = Session("userTypeId")
            Me.hidUsertype.Value = "3"

            'ddlDepartment.Items.Clear()
            'ddlDepartment.DataTextField = "departmentname"
            'ddlDepartment.DataValueField = "autoid"
            'ddlDepartment.DataSource = fun.bind_Department()
            'ddlDepartment.DataBind()
            'ddlDepartment.Items.Insert("0", "--Select--")
        End If
        conn.Open()
        cmdnew = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", conn)
        rdrnew = cmdnew.ExecuteReader
        If rdrnew.Read Then
            Dim producttype As String = rdrnew("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                rdrnew.Close()
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
                Me.btnGo.Visible = True
            End If
        End If
        rdrnew.Close()
        cmdnew.Dispose()
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
                ddlDepartment.DataSource = dsgetdept
                ddlDepartment.DataTextField = "DepartmentName"
                ddlDepartment.DataValueField = "autoid"
                ddlDepartment.DataBind()
                ddlDepartment.Items.Insert(0, "--Select--")
            End If
        End If
        conn.Close()
    End Sub
    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        If ddlDepartment.SelectedIndex <> 0 Then
            Me.ddlClient.DataSource = fun.bind_client(ddlDepartment.SelectedValue)
            Me.ddlClient.DataTextField = "ClientName"
            Me.ddlClient.DataValueField = "autoid"
            Me.ddlClient.DataBind()
            Me.ddlClient.Items.Insert("0", "--Select--")
            Me.ddlClient.Dispose()
            dsProcess.Dispose()
            conn.Close()
            ddlLob.Items.Clear()
        End If
    End Sub
    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        If ddlClient.SelectedItem.Text = "--Select--" Then
            ddlLob.Items.Clear()
            ddlLob.Items.Insert(0, "--Select--")
        Else
            Dim classobj As New Functions
            ddlLob.DataTextField = "lobname"
            ddlLob.DataValueField = "autoid"
            ddlLob.DataSource = fun.bind_lob(ddlDepartment.SelectedValue, ddlClient.SelectedValue)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "--Select--")

        End If
    End Sub
    ''' <summary>
    ''' This function is used to show an alert
    ''' </summary>
    ''' <param name="strPassed"></param>
    ''' <returns>true</returns>
    ''' <remarks></remarks>
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript' >"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", Script)
        Return True
    End Function
    ''' <summary>
    '''  Bind grid with tables
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnGo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGo.Click
        bind_Grid()
    End Sub
    ''' <summary>
    ''' Bind grid with tables
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function bind_Grid()

        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet

        Try

            objds = selectRep.tableFornonlocal(Session("userid"))

            If objds.Tables(0).Rows.Count().ToString() Then
                Dim dv As DataView = New DataView(objds.Tables(0))
                If (Session("typeofuser") = "Super Admin") Then
                    dv.Sort = "TableName"
                Else
                    dv.Sort = "TableName"
                End If

                datagridTables.DataSource = dv 'objds.Tables(0)
                datagridTables.DataBind()
                datagridTables.Visible = True
                Me.datagridTables.Visible = True
            Else
                lblMsg.Text = "No Table Found"
                Me.datagridTables.Visible = False
            End If
            conn.Close()
            'objcmd.Dispose()
            objds.Dispose()

        Catch ex As Exception
            lblMsg.Text = "Session Expired. Please Login Again"
        End Try
        Return 1
    End Function

    ''' <summary>
    ''' This methos handles different commands raised within the datagrid binded with the table name.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub datagridTables_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles datagridTables.ItemCommand
        If e.CommandName = "Done" Then
            Dim i As Integer
            i = 0
            Try

                StrTableName = ""

                Dim myDataGridItem As DataGridItem

                For Each myDataGridItem In datagridTables.Items

                    If CType(myDataGridItem.FindControl("chkItem"), CheckBox).Checked = True Then
                        i = i + 1
                        If StrTableName = "" Then
                            StrTableName = CType(myDataGridItem.FindControl("LnkLOB"), Label).Text
                        Else
                            StrTableName = StrTableName & "~" & CType(myDataGridItem.FindControl("LnkLOB"), Label).Text

                        End If
                    End If
                Next
                If Me.hidTables.Value = "" Then
                    Me.hidTables.Value = StrTableName
                Else
                    Me.hidTables.Value = Me.hidTables.Value + "~" + StrTableName
                End If
                If i = 0 Then
                    lblMsg.Text = "Select Table"
                Else
                    refParent()
                End If

            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                lblMsg.Text = strmsg
            End Try
            Me.datagridTables.Dispose()
        End If
    End Sub
    ''' <summary>
    ''' This method is called when the page of the datagrid is changed
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub datagridTables_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles datagridTables.PageIndexChanged
        Me.datagridTables.CurrentPageIndex = e.NewPageIndex
        bind_Grid()
    End Sub
    ''' <summary>
    '''  To call a javascript function to pass the selected tables value
    ''' to the parent window
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function refParent() As Integer
        Dim Script As New System.Text.StringBuilder
        With Script
            .Append("<Script language='javascript' type='text/javascript'>")
            .Append("parentUpdate();")
            .Append("</Script>")
        End With
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "blankrepRefresh", Script.ToString())
        Return 1
    End Function
    Protected Sub selecttemptab_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles selecttemptab.Click
        bind_Grid2()
    End Sub
    Function bind_Grid2()

        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet

        Try

            objds = selectRep.tableFortempimport(Session("userid"))

            If objds.Tables(0).Rows.Count().ToString() Then
                Dim dv As DataView = New DataView(objds.Tables(0))
                If (Session("typeofuser") = "Super Admin") Then
                    dv.Sort = "TableName"
                Else
                    dv.Sort = "TableName"
                End If

                datagridTables.DataSource = dv 'objds.Tables(0)
                datagridTables.DataBind()
                datagridTables.Visible = True
                Me.datagridTables.Visible = True
            Else
                lblMsg.Text = "No Table Found"
                Me.datagridTables.Visible = False
            End If
            conn.Close()
            'objcmd.Dispose()
            objds.Dispose()

        Catch ex As Exception
            lblMsg.Text = "Session Expired. Please Login Again"
        End Try
        Return 1
    End Function

    Protected Sub datagridTables_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles datagridTables.SelectedIndexChanged

    End Sub

    Protected Sub btnGoMul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGoMul.Click
        If ddlDepartment.SelectedValue = "" Or ddlDepartment.SelectedValue = "--Select--" Then
            lblMsg.Text = "Please Select Department"
        Else
            dept = ddlDepartment.SelectedValue
        End If
        If ddlClient.SelectedValue = "" Or ddlClient.SelectedValue = "--Select--" Then
            client = "0"
        Else
            client = ddlClient.SelectedValue
        End If
        If ddlLob.SelectedValue = "" Or ddlLob.SelectedValue = "--Select--" Then
            lob = "0"
        Else
            lob = ddlLob.SelectedValue
        End If
        If (dept <> "") Then
            bind_Grid3()
        Else
            lblMsg.Text = "Please Select Department"
        End If
    End Sub
    Function bind_Grid3()
        If ddlDepartment.SelectedValue = "" Or ddlDepartment.SelectedValue = "--Select--" Then
            lblMsg.Text = "Please Select Department"
        Else
            dept = ddlDepartment.SelectedValue
        End If
        If ddlClient.SelectedValue = "" Or ddlClient.SelectedValue = "--Select--" Then
            client = 0
        Else
            client = ddlClient.SelectedValue
        End If
        If ddlLob.SelectedValue = "" Or ddlLob.SelectedValue = "--Select--" Then
            lob = 0
        Else
            lob = ddlLob.SelectedValue
        End If
        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet
        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        'Dim objcmd As New SqlCommand
        'objcmd = New SqlCommand("Select Tableid,Tablename as TableName from WARSlobtablemaster where DepartmentId='" & dept & "' and ClientId='" & client & "' and LobId='" & lob & "' order by Tablename", conn)
        'objadp.SelectCommand = objcmd
        'conn.Open()
        'objadp.Fill(objds)
        ' Dim dv As DataView = New DataView(objds.Tables(0))
        ' dv.Sort = "TableName"
        '''''''''''''Updated by Usha sheokand on 4-09-08 accvording to rights mgmment
        Try

            If (Session("typeofuser") = "Admin") Then
                objds = selectRep.tableForadmin(Session("userid"), dept, client, lob)
            ElseIf (Session("typeofuser") = "Super Admin") Then
                objds = selectRep.tableForSA(Session("userid"), dept, client, lob)
            Else
                'Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                'If (scope = "Local") Then
                '    objds = selectRep.tableForlocal(Session("userid"), dept, client, lob)
                'Else
                objds = selectRep.tableFornonlocal2(Session("userid"), dept, client, lob)
                'End If
            End If

            ''''''''''''''
            If objds.Tables(0).Rows.Count().ToString() Then
                Dim dv As DataView = New DataView(objds.Tables(0))
                If (Session("typeofuser") = "Super Admin") Then
                    dv.Sort = "TableName"
                Else
                    dv.Sort = "TableName"
                End If

                datagridTables.DataSource = dv 'objds.Tables(0)
                datagridTables.DataBind()
                datagridTables.Visible = True
                Me.datagridTables.Visible = True
            Else
                lblMsg.Text = "No Table Found"
                Me.datagridTables.Visible = False
            End If
            conn.Close()
            'objcmd.Dispose()
            objds.Dispose()

        Catch ex As Exception
            lblMsg.Text = "Session Expired. Please Login Again"
        End Try
        Return 1
    End Function
End Class
