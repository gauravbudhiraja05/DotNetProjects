Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Partial Class DataManager_ArchivalReport
    Inherits System.Web.UI.Page

    Dim con As String = AppSettings("ConnectionString")
    Dim con1 As New SqlConnection(con)
    Dim connection As New SqlConnection(con)
    Dim cmd As SqlCommand
    Dim classobj1 As New DataManagerFunction
    Dim classobjdatamgr As New ReportDesigner
    Dim dr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Ajax.Utility.RegisterTypeForAjax(GetType(DataManagerAjax))
        'Me.ddlDept.Attributes.Add("onchange", "getClient();")
        'Me.ddlClient.Attributes.Add("onchange", "GetLOB();")
        'Me.ddlLob.Attributes.Add("onchange", "restorevalue();")


        If Me.IsPostBack = False Then
            Dim classobj As New Functions
            'classobj.bind_Department()

            Me.ddlDept.DataTextField = "DepartmentName"
            ddlDept.DataValueField = "AutoId"
            ddlDept.DataSource = classobj.bind_Department()

            ddlDept.DataBind()
            ddlDept.Items.Insert(0, "--Select--")
            ' Me.ddlClient.Items.Insert(0, "--Select--")
            'Me.ddlLob.Items.Insert(0, "--Select--")

            Me.PnlDel.Visible = False

            divUnarchive.Visible = False
            divPaging.Visible = False
        End If


        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                'Me.spandisplay.Visible = True
                dept_row.Visible = True
                client_row.Visible = True
                lob_row.Visible = True
                button_multi_row.Visible = True
                Me.btnShow_singleuser.Visible = False
                Me.btnShow_multiuser.Visible = True
                rdr.Close()
                Dim cmd1 As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd1)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    lblDept.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    lblClient.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    lblLOB.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If

            Else
                button_single_row.Visible = True
                'Me.spandisplay.Visible = False
                Me.btnShow_singleuser.Visible = True
                Me.btnShow_multiuser.Visible = False
            End If
            connection.Close()
        End If
        'End If

        Dim typeofuser = Session("typeofuser")
        If (Page.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "-- Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con1)
                con1.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con1)
                con1.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            End If
            DepartmentName.Items.Insert(0, "--Select--")
        End If


    End Sub


    ''' <summary>
    ''' Function to bind grid
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub bindgrid()
        'bind grid o display archive report
        ' Dim cmd As New SqlCommand("select * from IDMSSavedHTMLFile", connection)
        ' Dim cmd As New SqlCommand("select * from idmsreportmaster", connection)
        Dim client = 0
        Dim lob = 0
        Dim dept = 60

        'If lob = "--Select--" Or lob = "" Then
        '    lob = 0
        'Else
        '    lob = hidLob.Value
        'End If

        'If client = "--Select--" Or client = "" Then
        '    client = 0
        'Else
        '    client = hidClient.Value

        'End If


        'Dim cmd As New SqlCommand("select idmsquerymaster.recordId, idmsquerymaster.queryName+'_qb'as queryName,idmsquerymaster.savedby,idmsquerymaster.createdate from idmsquerymaster where departmentid=" & dept & " and clientid=" & client & " and underlob=" & lob & " and savedby<>'Deleted' and archivedstatus='Yes'  union (select idmsreportmaster.recordid as recordId ,idmsreportmaster.queryName +'_rd'as queryName,idmsreportmaster.savedby,idmsreportmaster.createdOn as savedby from idmsreportmaster  where  departmentid=" & dept & " and clientid=" & client & " and underlob=" & lob & " and savedby<>'Deleted' and archivedstatus='Yes'  )", connection)
        'connection.Open()
        ' Dim da As New SqlDataAdapter
        ' Dim ds As New DataSet
        'da.SelectCommand = cmd
        ' da.Fill(ds)
        If Me.txtpageing.Value <> "" Then
            grdArchiverep.PageSize = txtpageing.Value
        Else
            grdArchiverep.PageSize = 10
        End If
        'grdArchiverep.PageSize = 10
        'Me.grdArchiverep.DataSource = ds
        'Dim usertype = classobjdatamgr.chkUserscope(Session("userid"))

        'If Session("typeofuser") = "Admin" Then
        '    Me.grdArchiverep.DataSource = classobj1.showArcrepep(dept, client, lob) 'Function call to bind report
        '    Me.grdArchiverep.DataBind()
        'connection.Close()
        'ElseIf Session("typeofuser") = "User" Then

        'If usertype = "Local" Then
        'Me.grdArchiverep.DataSource = classobj1.reportForlocal(Session("userid"), dept, client, lob)
        'Me.grdArchiverep.DataBind()


        'Else
        Me.grdArchiverep.DataSource = classobj1.reportFornonlocal(Session("userid"), dept, client, lob)
        Me.grdArchiverep.DataBind()


        'End If

        'End If
        'Dim dsvalue As DataSet = classobj1.chkuserrights(Session("userid"))
        'Dim i As Integer
        'Dim grd As GridView = grdArchiverep

        'For i = 0 To grd.Rows.Count - 1
        '    If Session("userid") = dsvalue.Tables(0).Rows.Item(i).Item("Delete").ToString = "true" Then
        '        CType(grd.FindControl("Delete"), LinkButton).Visible = True
        '    Else
        '        CType(grd.FindControl("Delete"), LinkButton).Visible = False
        '    End If

        'Next

        'If you know the column index, you can do:
        'gvr.Cells[2].Visible = false;

        'For i = 0 To grdArchiverep.Parent.i.Count - 1
        'If Session("userid") = dsget.Tables(0).Rows.Item(i).Item("createdby").ToString Or dsget.Tables(0).Rows.Item(i).Item("createdby").ToString = "idmsadmin" Then
        'If Session("userid") = dsget.Tables(0).Rows.Item(i).Item("createdby").ToString Then
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = True

        'ElseIf Session("userid") = "idmsadmin" Then
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = True

        'Else
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = False
        'End If
        'Next


    End Sub
    ''' <summary>
    ''' Show report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow_singleuser.Click
        'If Me.ddlDept.Text = "--Select--" Then
        '    Showmsg("Please Select Department!!!")
        '    Exit Sub
        'End If
        Me.divPaging.Visible = True
        bindgrid()

    End Sub
    ''' <summary>
    ''' Paging in Gridview
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdArchiverep_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdArchiverep.PageIndexChanging
        If grdArchiverep.PageIndex < grdArchiverep.PageCount And grdArchiverep.PageIndex >= 0 Then
            Me.grdArchiverep.PageIndex = e.NewPageIndex
            bindgrid()
        End If
    End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "Showmsg", str.ToString)
    End Sub

    Protected Sub grdArchiverep_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdArchiverep.RowCommand
        If e.CommandName = "QueryName" Then
            hidQueryname.Value = CType(e.CommandSource.Parent.FindControl("lnkRepname"), LinkButton).Text
            hidRecordid.Value = CType(e.CommandSource.Parent.FindControl("lblId"), Label).Text
            Response.Redirect("Report.aspx?queryname=" & hidQueryname.Value & "&recid=" & hidRecordid.Value & "&ClientId=" & hidClient.Value & "&LobId=" & hidLob.Value)

        ElseIf e.CommandName = "UnArchive" Then
            hidQueryname.Value = CType(e.CommandSource.Parent.FindControl("lnkRepname"), LinkButton).Text
            hidRecordid.Value = CType(e.CommandSource.Parent.FindControl("lblId"), Label).Text
            divUnarchive.Visible = True
        ElseIf e.CommandName = "Recdel" Then
            hidQueryname.Value = CType(e.CommandSource.Parent.FindControl("lnkRepname"), LinkButton).Text
            hidRecordid.Value = CType(e.CommandSource.Parent.FindControl("lblId"), Label).Text
            PnlDel.Visible = True
        End If
    End Sub

    Protected Sub btnDeln_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeln.ServerClick
        PnlDel.Visible = False
    End Sub

    Protected Sub btnUnarchiven_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnarchiven.ServerClick
        divUnarchive.Visible = False
    End Sub

    Protected Sub btnDely_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDely.ServerClick

        Dim queryvalue As String = hidQueryname.Value
        Dim queryname = queryvalue.Split("_")
        Dim arrqurery1 As String = queryname(0)
        'Dim arrqurery2 As String = queryname(1)
        Dim arrqurery2 As String = queryname(0)
        Dim tabidvalue = CType(hidRecordid.Value, Integer)
        Dim queryget As String = classobj1.Delrep(arrqurery2, tabidvalue) ' Function to delete Report
        If queryget = "1" Then
            PnlDel.Visible = False
            Showmsg("Record Deleted Successfully!!!!")

            'Code for Track
            Dim ClientId
            Dim LobId


            If hidClient.Value = "" Then
                ClientId = "0"
            Else
                ClientId = hidClient.Value
            End If
            If hidLob.Value = "" Then
                LobId = "0"
            Else
                LobId = hidLob.Value
            End If
            Dim count As Integer = 0
            Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim cmd As SqlCommand
            cmd = New SqlCommand("Sp_LogDataManager", connection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
            cmd.Parameters("@ActionBy").Value = Session("userId")
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
            cmd.Parameters("@Action").Value = "Delete Report"
            cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
            cmd.Parameters("@Date").Value = System.DateTime.Now
            cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
            cmd.Parameters("@Entity").Value = "Table"
            cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
            cmd.Parameters("@EntityName").Value = arrqurery1
            cmd.Parameters.Add("@DeptId", SqlDbType.Int, 50)
            cmd.Parameters("@DeptId").Value = CType(60, Integer)
            cmd.Parameters.Add("@ClientId", SqlDbType.Int, 50)
            cmd.Parameters("@ClientId").Value = CType(0, Integer)
            cmd.Parameters.Add("@LobId", SqlDbType.Int, 50)
            cmd.Parameters("@LobId").Value = CType(0, Integer)
            cmd.Parameters.Add("@count", SqlDbType.Int, 50)
            cmd.Parameters("@count").Value = CType(count, Integer)
            connection.Close()
            connection.Open()
            cmd.ExecuteNonQuery()

            Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Delete Report' and EntityName='" + arrqurery1 + "'", connection)
            cmm.ExecuteNonQuery()

            connection.Close()
            'End Track
            bindgrid()
            Exit Sub
        Else
            PnlDel.Visible = False
            Showmsg(queryget)
            Exit Sub
        End If

    End Sub

    Protected Sub btnUnarchivey_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUnarchivey.ServerClick
        Dim queryvalue As String = hidQueryname.Value
        Dim queryname = queryvalue.Split("_")
        Dim arrqurery1 As String = queryname(0)
        'Dim arrqurery2 As String = queryname(1)
        Dim arrqurery2 As String = queryname(0)
        Dim tabidvalue = CType(hidRecordid.Value, Integer)
        Dim queryget As String = classobj1.Arvrep(arrqurery2, tabidvalue) ' Function to unarchive report
        If queryget = "1" Then
            divUnarchive.Visible = False
            Showmsg("Record Unarchived Successfully!!!!")

            'Code for Track
            Dim ClientId
            Dim LobId


            If hidClient.Value = "" Then
                ClientId = "0"
            Else
                ClientId = hidClient.Value
            End If
            If hidLob.Value = "" Then
                LobId = "0"
            Else
                LobId = hidLob.Value
            End If
            Dim count As Integer = 0
            Dim da As SqlDataAdapter = New SqlDataAdapter
            Dim cmd As SqlCommand
            cmd = New SqlCommand("Sp_LogDataManager", connection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
            cmd.Parameters("@ActionBy").Value = Session("userId")
            cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
            cmd.Parameters("@Action").Value = "Unarchive Report"
            cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
            cmd.Parameters("@Date").Value = System.DateTime.Now
            cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
            cmd.Parameters("@Entity").Value = "Table"
            cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
            cmd.Parameters("@EntityName").Value = arrqurery1
            cmd.Parameters.Add("@DeptId", SqlDbType.Int, 50)
            cmd.Parameters("@DeptId").Value = CType(ddlDept.SelectedValue, Integer)
            cmd.Parameters.Add("@ClientId", SqlDbType.Int, 50)
            cmd.Parameters("@ClientId").Value = CType(ClientId, Integer)
            cmd.Parameters.Add("@LobId", SqlDbType.Int, 50)
            cmd.Parameters("@LobId").Value = CType(LobId, Integer)
            cmd.Parameters.Add("@count", SqlDbType.Int, 50)
            cmd.Parameters("@count").Value = CType(count, Integer)
            connection.Open()
            cmd.ExecuteNonQuery()

            Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Unarchive Report' and EntityName='" + arrqurery1 + "'", connection)
            cmm.ExecuteNonQuery()

            connection.Close()

            'End Track
            bindgrid()
            Exit Sub
        Else
            divUnarchive.Visible = False
            Showmsg(queryget)
            Exit Sub
        End If

    End Sub

    Protected Sub btnAPaging_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAPaging.ServerClick
        'grdArchiverep.PageSize = Me.txtpageing.Value
        If Me.txtpageing.Value = "" Then
            Showmsg("Please Fill Paging Value!!!")
            Exit Sub
        End If
        If IsNumeric(txtpageing.Value) = False Then
            Showmsg("Please Fill Only Numeric Value!!!")
            txtpageing.Value = ""
            txtpageing.Focus()
            Exit Sub
        End If

        bindgrid()
    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If (DepartmentName.SelectedIndex = 0) Then
            aspnet_msgbox("Please Select Department First")
            Clientname.Items.Clear()
            ddlLobname.Items.Clear()
        Else
            con1.Close()
            con1.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con1)
            dr = cmd.ExecuteReader()
            Clientname.DataSource = dr
            Clientname.DataTextField = "ClientName"
            Clientname.DataValueField = "autoid"
            Clientname.DataBind()
            Clientname.Items.Insert(0, "--Select--")
        End If
        

    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
        If (Clientname.SelectedValue = "--Select--") Then
            ddlLobname.Items.Clear()
        Else
            con1.Close()
            con1.Open()
            cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + Clientname.SelectedValue + "'", con1)
            dr = cmd.ExecuteReader()
            ddlLobname.DataSource = dr
            ddlLobname.DataTextField = "LOBName"
            ddlLobname.DataValueField = "autoid"
            ddlLobname.DataBind()
            ddlLobname.Items.Insert(0, "--Select--")
        End If
        
    End Sub
    Protected Sub btnShow_multiuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnShow_multiuser.Click
        If Me.DepartmentName .Text = "--Select--" Then
            Showmsg("Please Select Department!!!")
            Exit Sub
        End If
        Me.divPaging.Visible = True
        bindgrid1()
    End Sub
    Public Function get_report(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        bindgrid1()
    End Function
    Private Sub bindgrid1()
        'bind grid o display archive report
        ' Dim cmd As New SqlCommand("select * from IDMSSavedHTMLFile", connection)
        ' Dim cmd As New SqlCommand("select * from idmsreportmaster", connection)
        Dim client = Me.Clientname.SelectedValue
        Dim lob = Me.ddlLobname.SelectedValue
        Dim dept = Me.DepartmentName.SelectedValue

        If lob = "--Select--" Or lob = "" Then
            lob = 0
        Else
            lob = ddlLobname.SelectedValue
        End If

        If client = "--Select--" Or client = "" Then
            client = 0
        Else
            client = Clientname.SelectedValue

        End If
        'dept = DepartmentName.SelectedValue
        'client = Clientname.SelectedValue
        'lob = ddlLobname.SelectedValue

        'Dim cmd As New SqlCommand("select idmsquerymaster.recordId, idmsquerymaster.queryName+'_qb'as queryName,idmsquerymaster.savedby,idmsquerymaster.createdate from idmsquerymaster where departmentid=" & dept & " and clientid=" & client & " and underlob=" & lob & " and savedby<>'Deleted' and archivedstatus='Yes'  union (select idmsreportmaster.recordid as recordId ,idmsreportmaster.queryName +'_rd'as queryName,idmsreportmaster.savedby,idmsreportmaster.createdOn as savedby from idmsreportmaster  where  departmentid=" & dept & " and clientid=" & client & " and underlob=" & lob & " and savedby<>'Deleted' and archivedstatus='Yes'  )", connection)
        'connection.Open()
        ' Dim da As New SqlDataAdapter
        ' Dim ds As New DataSet
        'da.SelectCommand = cmd
        ' da.Fill(ds)
        If Me.txtpageing.Value <> "" Then
            grdArchiverep.PageSize = txtpageing.Value
        Else
            grdArchiverep.PageSize = 10
        End If
        'grdArchiverep.PageSize = 10
        'Me.grdArchiverep.DataSource = ds
        'Dim usertype = classobjdatamgr.chkUserscope(Session("userid"))

        If Session("typeofuser") = "Admin" Then
            Me.grdArchiverep.DataSource = classobj1.showArcrepep(dept, client, lob) 'Function call to bind report
            Me.grdArchiverep.DataBind()
            'connection.Close()
            'ElseIf Session("typeofuser") = "User" Then

            'If usertype = "Local" Then
            'Me.grdArchiverep.DataSource = classobj1.reportForlocal(Session("userid"), dept, client, lob)
            'Me.grdArchiverep.DataBind()


        Else
            Me.grdArchiverep.DataSource = classobj1.reportFornonlocal(Session("userid"), dept, client, lob)
            Me.grdArchiverep.DataBind()


        End If

        'End If
        'Dim dsvalue As DataSet = classobj1.chkuserrights(Session("userid"))
        'Dim i As Integer
        'Dim grd As GridView = grdArchiverep

        'For i = 0 To grd.Rows.Count - 1
        '    If Session("userid") = dsvalue.Tables(0).Rows.Item(i).Item("Delete").ToString = "true" Then
        '        CType(grd.FindControl("Delete"), LinkButton).Visible = True
        '    Else
        '        CType(grd.FindControl("Delete"), LinkButton).Visible = False
        '    End If

        'Next

        'If you know the column index, you can do:
        'gvr.Cells[2].Visible = false;

        'For i = 0 To grdArchiverep.Parent.i.Count - 1
        'If Session("userid") = dsget.Tables(0).Rows.Item(i).Item("createdby").ToString Or dsget.Tables(0).Rows.Item(i).Item("createdby").ToString = "idmsadmin" Then
        'If Session("userid") = dsget.Tables(0).Rows.Item(i).Item("createdby").ToString Then
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = True

        'ElseIf Session("userid") = "idmsadmin" Then
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = True

        'Else
        '    CType(dlshow.Items(i).FindControl("lkbremove"), LinkButton).Visible = True
        '    CType(dlshow.Items(i).FindControl("lkbedit"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("lkbdelete"), LinkButton).Visible = False
        '    CType(dlshow.Items(i).FindControl("Linkbutton1"), LinkButton).Visible = False
        'End If
        'Next


    End Sub
End Class
