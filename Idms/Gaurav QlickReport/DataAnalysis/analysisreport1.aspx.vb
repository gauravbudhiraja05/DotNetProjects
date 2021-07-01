Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO
Imports System.IO.StreamReader

Partial Class analysisreport1
    Inherits System.Web.UI.Page
    Dim repcolarray
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(conn)
    Dim con As New SqlConnection(conn)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim p
    Dim msgdelete As Integer = 0

    ' Dim j As Integer = 0
    Public stt As String
    Dim colname, formula, groupby, orderby As String

    'Protected Sub ddlDepartmant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmant.SelectedIndexChanged
    'Dim strClose As String = ""

    'strClose = "<Script language='Javascript'>"
    'strClose = strClose + "get_department()"
    'strClose = strClose + "</Script>"
    'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)

    ''' <summary>
    ''' fill the department 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                'Me.spandisplay.Visible = True
                '        lbldept.Visible = True
                '       lblclient.Visible = True
                '      lbllob.Visible = True
                '     level1.Visible = True
                '    level2.Visible = True
                '   level3.Visible = True
                dept_row.Visible = True
                client_row.Visible = True
                lob_row.Visible = True
                Show_multiuser.Visible = True
                'row_button_multiple.Visible = True
                '  Me.ShowReport_singleuser.Visible = False
                'Me.Show_multiuser.Visible = True
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
                    lbllob.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If

            Else
                'Me.spandisplay.Visible = False
                'Me.ShowReport_multiuser.Visible = False
                row_button_single.Visible = True
            End If
            connection.Close()
        End If

        'Dim lblThispage As Label = Master.FindControl("lblPage")
        'lblThispage.Text = "Data Analysis"

        'If Page.IsPostBack = False Then
        Dim typeofuser = Session("typeofuser")
        If (Not IsPostBack) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                level1.DataSource = cmd.ExecuteReader()
                level1.DataTextField = "DepartmentName"
                level1.DataValueField = "AutoID"
                level1.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                level1.DataSource = cmd.ExecuteReader()
                level1.DataTextField = "DepartmentName"
                level1.DataValueField = "AutoID"
                level1.Items.Insert(0, "--Select--")
                level1.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                level1.DataSource = cmd.ExecuteReader()
                level1.DataTextField = "DepartmentName"
                level1.DataValueField = "AutoID"
                level1.Items.Insert(0, "--Select--")
                level1.DataBind()
            End If
            level1.Items.Insert(0, "--Select--")
        End If
        Dim classobj As New Functions
        'If Session("typeofuser") = "Admin" Then


        '    ddlDepartmant.DataTextField = "DepartmentName"
        '    ddlDepartmant.DataValueField = "deptid"
        '    ddlDepartmant.DataSource = classobj.bind_AdminDept("peter4200") '(Session("userid1"))
        '    ddlDepartmant.DataBind()
        '    ddlDepartmant.Items.Insert(0, "---select---")
        'End If
        'If Session("typeofuser") = "User" Then


        '    ddlDepartmant.DataTextField = "DepartmentName"
        '    ddlDepartmant.DataValueField = "deptid"
        '    ddlDepartmant.DataSource = classobj.bind_usersDept(Session("userid1"))
        '    ddlDepartmant.DataBind()
        '    ddlDepartmant.Items.Insert(0, "---select---")
        'End If
        ' End If

        'ddlDepartmant.DataTextField = "departmentname"
        'ddlDepartmant.DataValueField = "DeptID"
        'ddlDepartmant.DataSource = classobj.bind_Dept()
        'ddlDepartmant.DataBind()
        'ddlDepartmant.Items.Insert(0, "---select---")
        '  


        'ddlDepartmant.Attributes.Add("onchange", "get_department();")
        'ddlClient.Attributes.Add("onchange", "getclentrephtm();")
        'ddlLob.Attributes.Add("onchange", "reportfromlob();")
        'ShowReport.Attributes.Add("onclick", "ok_onclick();")
        '

        'Dim strClose As String = ""

        'strClose = "<Script language='Javascript'>"
        'strClose = strClose + "getdept();"
        'strClose = strClose + "</Script>"
        'Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Close", strClose)
        Page.SmartNavigation = True



    End Sub






    ''' <summary>
    ''' to show message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>


    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub

    ''' <summary>
    ''' fill vaue in gridview
    ''' </summary>
    ''' <param name="dept"></param>
    ''' <param name="client"></param>
    ''' <param name="lob"></param>
    ''' <remarks></remarks>
    ''' dim
    ''' 
    Dim repobj As New ReportDesigner

    Public Sub gridbind1(ByVal dept As String, ByVal client As String, ByVal lob As String)
        Dim obj As New Functions
        Dim item As GridViewRow
        Dim objsav As New SavedAnalysis
        Dim ds As New DataSet
        'If Session("typeofuser") = "Admin" Then
        '    ds = obj.bind_htmlreportforadmin(Session("userid"), dept, client, lob)


        'ElseIf Session("typeofuser") = "User" Then
        'Dim SCOPE As String = repobj.chkUserscope(Session("userid"), dept, client, lob)
        'If SCOPE = "Local" Then
        ds = obj.bind_htmlreport(Session("userid"), level1.SelectedValue, level2.SelectedValue, level3.SelectedValue)
        'ds = = repobj.reportForlocal(Session("userid"), dept, client, lob)
        'Else
        '    ds = objsav.htmlreport_nonlocal(Session("userid"), dept, client, lob)
        'End If

        'End If


        If ds.Tables(0).Rows.Count = 0 Then
            If msgdelete = 0 Then
                aspnet_msgbox("No Record Found")
            End If

            grdhtmlreport.Visible = False
            Exit Sub
        Else

            grdhtmlreport.Visible = True
            grdhtmlreport.DataSource = ds
            grdhtmlreport.DataBind()
        End If
        If client = "0" Then

            CType(grdhtmlreport.HeaderRow.FindControl("lbldclthead"), Label).Visible = False
            grdhtmlreport.Columns(1).Visible = False
        Else
            CType(grdhtmlreport.HeaderRow.FindControl("lbldclthead"), Label).Visible = True
            grdhtmlreport.Columns(1).Visible = True
        End If
        If lob = "0" Then
            CType(grdhtmlreport.HeaderRow.FindControl("lbllobhead"), Label).Visible = False
            grdhtmlreport.Columns(2).Visible = False
        Else
            CType(grdhtmlreport.HeaderRow.FindControl("lbllobhead"), Label).Visible = True
            grdhtmlreport.Columns(2).Visible = True
        End If
        For Each item In grdhtmlreport.Rows
            If client = "0" Then

                CType(item.FindControl("lblclt"), Label).Visible = False



            Else

                CType(item.FindControl("lblclt"), Label).Visible = True


            End If
            If lob = "0" Then

                CType(item.FindControl("lbllob"), Label).Visible = False


            Else

                CType(item.FindControl("lbllob"), Label).Visible = True

            End If
        Next

    End Sub


    Public Sub gridbind(ByVal dept As String, ByVal client As String, ByVal lob As String)
        Dim obj As New Functions
        Dim item As GridViewRow
        Dim objsav As New SavedAnalysis
        Dim ds As New DataSet
        'If Session("typeofuser") = "Admin" Then
        '    ds = obj.bind_htmlreportforadmin(Session("userid"), dept, client, lob)


        'ElseIf Session("typeofuser") = "User" Then
        'Dim SCOPE As String = repobj.chkUserscope(Session("userid"), dept, client, lob)
        'If SCOPE = "Local" Then
        ds = obj.bind_htmlreport(Session("userid"), dept, client, lob)
        'ds = = repobj.reportForlocal(Session("userid"), dept, client, lob)
        'Else
        '    ds = objsav.htmlreport_nonlocal(Session("userid"), dept, client, lob)
        'End If

        'End If


        If ds.Tables(0).Rows.Count = 0 Then
            If msgdelete = 0 Then
                aspnet_msgbox("No Record Found")
            End If

            grdhtmlreport.Visible = False
            Exit Sub
        Else

            grdhtmlreport.Visible = True
            grdhtmlreport.DataSource = ds
            grdhtmlreport.DataBind()
        End If
        If client = "0" Then

            CType(grdhtmlreport.HeaderRow.FindControl("lbldclthead"), Label).Visible = False
            grdhtmlreport.Columns(1).Visible = False
        Else
            CType(grdhtmlreport.HeaderRow.FindControl("lbldclthead"), Label).Visible = True
            grdhtmlreport.Columns(1).Visible = True
        End If
        If lob = "0" Then
            CType(grdhtmlreport.HeaderRow.FindControl("lbllobhead"), Label).Visible = False
            grdhtmlreport.Columns(2).Visible = False
        Else
            CType(grdhtmlreport.HeaderRow.FindControl("lbllobhead"), Label).Visible = True
            grdhtmlreport.Columns(2).Visible = True
        End If
        For Each item In grdhtmlreport.Rows
            If client = "0" Then

                CType(item.FindControl("lblclt"), Label).Visible = False



            Else

                CType(item.FindControl("lblclt"), Label).Visible = True


            End If
            If lob = "0" Then

                CType(item.FindControl("lbllob"), Label).Visible = False


            Else

                CType(item.FindControl("lbllob"), Label).Visible = True

            End If
        Next

    End Sub

    ''' <summary>
    ''' to check the client and lob value
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>


    Public Function hidvalue()
        Dim n = hid.Value
        If n = "---select---" Then
            aspnet_msgbox("No Department Is Selected")
            Exit Function
        End If
        Dim j = hid1.Value
        If j = "" Then
            j = "0"

        End If
        Dim b = hid2.Value
        If b = "" Then
            b = "0"

        End If

        Dim arr As String = n & "," & j & "," & b

        Return arr
    End Function

    ''' <summary>
    ''' call the grid fill fnction
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub ShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport_singleuser.Click

        msgdelete = 0
        'If ddlDepartmant.SelectedItem.Text = "---select---" Then
        '    aspnet_msgbox("Select Department First")
        '    Exit Sub
        'End If

        Dim newarr As String = hidvalue().ToString
        Dim arrrs = newarr.Split(",")
        gridbind(60, 0, 0)
        'hid1.Value = ""
        'hid2.Value = ""
        'hid.Value = ""

    End Sub
    ''' <summary>
    ''' use for paging 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdhtmlreport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdhtmlreport.PageIndexChanging
        If grdhtmlreport.PageIndex < grdhtmlreport.PageCount And grdhtmlreport.PageIndex >= 0 Then

            grdhtmlreport.PageIndex = e.NewPageIndex

            Dim newarr As String = hidvalue().ToString
            Dim arrrs = newarr.Split(",")

            gridbind(arrrs(0), arrrs(1), arrrs(2))

        End If
    End Sub

    
    
   

    
    
   
    ''' <summary>
    ''' delete the record
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    
    Protected Sub grdhtmlreport_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdhtmlreport.RowDeleting
        'Dim reportname As String = grdhtmlreport.Rows(e.RowIndex).Cells(3).Text
        Dim item As GridViewRow
        Dim namerep As String = ""
        Dim count As String = ""
        Dim bool As Boolean = False
        For Each item In grdhtmlreport.Rows

            If CType(item.FindControl("chk"), CheckBox).Checked = True Then
                bool = True
                Dim b = CType(item.FindControl("fndlbl"), Label).Text
                ' Dim b = grdhtmlreport.DataKeys(item.RowIndex).ToString
                Dim cmd As New SqlCommand("select repname from dataanalysishtmlreport where reportname=@repnm", con)
                cmd.Parameters.Add("@repnm", SqlDbType.NVarChar, 50)
                cmd.Parameters("@repnm").Value = b.ToString

                con.Open()
                readquery = cmd.ExecuteReader()
                While readquery.Read()


                    namerep = readquery("repname").ToString
                End While
                con.Close()
                readquery.Close()
                cmd = New SqlCommand("select count(repname) as repname from dataanalysishtmlreport", con)
                con.Open()
                readquery = cmd.ExecuteReader()
                If readquery.Read() Then
                    count = readquery("repname").ToString
                End If
                con.Close()
                readquery.Close()
                Dim cnt As Integer
                cnt = CType(count, Integer)
                If cnt = 1 Then
                    cmd = New SqlCommand("delete from analysisformula where reportname=@namerep", con)
                    cmd.Parameters.Add("@namerep", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@namerep").Value = namerep.ToString

                    con.Open()
                    cmd.ExecuteNonQuery()
                    con.Close()
                   

                End If

                Dim obj As New Functions
                obj.delete_html(b)
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where HTMLReport='" + b + "' and Action='Delete' and Entity='HTMLReport'", con)
                con.Open()
                cmm.ExecuteNonQuery()
                con.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                Dim DelPath As String
                DelPath = "html" '/" & b & ".html"
                Dim path1 = Server.MapPath(DelPath)
                Dim dir As New DirectoryInfo(path1)
                Dim file1() As FileInfo
                file1 = dir.GetFiles()

                If file1.Length > 0 Then
                    Dim i As Integer
                    For i = 0 To file1.Length - 1
                        Dim str As String
                        str = file1(i).ToString
                        If str = b & ".html" Then
                            file1(i).Delete()
                        End If
                        'If file1(i) Is b & ".html" Then
                        'file1(i).Delete()
                        'End If
                    Next

                End If


            End If


        Next


        Dim newarr As String = hidvalue().ToString
        Dim arrrs = newarr.Split(",")


        If bool = True Then
            msgdelete = 1
            aspnet_msgbox("Report(s) Has Been Deleted successfully")
        Else
            aspnet_msgbox("Select Atleast One Report")
        End If
        gridbind(arrrs(0), arrrs(1), arrrs(2))
        'Response.Write(reportname)
    End Sub

    ''' <summary>
    ''' fill clients of selected departments
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    'Protected Sub ddlDepartmant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmant.SelectedIndexChanged
    '    If ddlDepartmant.SelectedItem.Text = "---select---" Then
    '        hid.Value = ""
    '        hid1.Value = ""
    '        hid2.Value = ""

    '        ddlClient.Items.Clear()
    '        ddlLob.Items.Clear()
    '    Else
    '        Dim classobj As New Functions



    '        ddlClient.DataTextField = "clientname"
    '        ddlClient.DataValueField = "autoid"
    '        ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)
    '        hid.Value = ddlDepartmant.SelectedValue
    '        'hid1.Value = "0"
    '        'hid2.Value = "0"
    '        'DEPART.Value = ddlDepartmant.SelectedItem.Text
    '        'client.Value = "0"
    '        'lob.Value = "0"
    '        ddlClient.DataBind()
    '        ddlClient.Items.Insert(0, "---select---")
    '        ddlLob.Items.Clear()
    '    End If
    'End Sub
    ''' <summary>
    '''  fill lob of selected client
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
    '    If ddlClient.SelectedItem.Text = "---select---" Then


    '        hid1.Value = ""
    '        hid2.Value = ""
    '        ddlLob.Items.Clear()
    '    Else
    '        Dim classobj As New Functions

    '        ddlLob.DataTextField = "lobname"
    '        ddlLob.DataValueField = "autoid"
    '        ddlLob.DataSource = classobj.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '        hid1.Value = ddlClient.SelectedValue
    '        'hid2.Value = "0"
    '        'client.Value = DropDownclient.SelectedItem.Text
    '        'lob.Value = "0"
    '        ddlLob.DataBind()
    '        ddlLob.Items.Insert(0, "---select---")
    '    End If
    'End Sub
    ''' <summary>
    ''' fetching the id of lob into hidden field
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
    '    If ddlLob.SelectedItem.Text <> "---select---" Then
    '        hid2.Value = ddlLob.SelectedValue
    '    Else

    '        hid2.Value = ""
    '    End If

    'End Sub
    Dim dr, cmd
    Protected Sub level1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles level1.SelectedIndexChanged
        If (level1.SelectedValue = "--Select--") Then
            level2.Items.Clear()
            level3.Items.Clear()
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + level1.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            level2.DataSource = dr
            level2.DataTextField = "ClientName"
            level2.DataValueField = "autoid"
            level2.DataBind()
            level2.Items.Insert(0, "--Select--")
        End If
        
        ' gridbind(level1.SelectedValue.ToString, 0, 0)
    End Sub
    Protected Sub level2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles level2.SelectedIndexChanged
        If (level2.SelectedValue = "--Select--") Then
            level3.Items.Clear()
        Else
            con.Close()
            con.Open()
            Dim cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + level1.SelectedValue + "' and  clientid= '" + level2.SelectedValue + "'", con)
            Dim dr = cmd.ExecuteReader()
            level3.DataSource = dr
            level3.DataTextField = "LOBName"
            level3.DataValueField = "autoid"
            level3.DataBind()
            level3.Items.Insert(0, "--Select--")
        End If
        'gridbind(level1.SelectedValue.ToString, level2.SelectedValue.ToString, 0)
    End Sub

    Protected Sub level3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles level3.SelectedIndexChanged

    End Sub

    Protected Sub ShowReport_multiuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Show_multiuser.Click
        Dim dept = level1.SelectedValue
        Dim client = level2.SelectedValue
        Dim lob = level3.SelectedValue
        If (level1.SelectedIndex = 0) Then
            aspnet_msgbox("Please Select Department First")
            Exit Sub
        End If
        If (level2.SelectedItem.Text = "--Select--" Or level2.SelectedItem.Text = "") Then
            client = 0
        Else
            client = level2.SelectedValue
        End If
        If (level3.SelectedValue = "" Or level3.SelectedValue = "--Select--") Then
            lob = 0
        Else
            lob = level3.SelectedValue
        End If
        msgdelete = 0
        'If ddlDepartmant.SelectedItem.Text = "---select---" Then
        '    aspnet_msgbox("Select Department First")
        '    Exit Sub
        'End If

        Dim newarr As String = hidvalue().ToString
        Dim arrrs = newarr.Split(",")
        gridbind(dept, client, lob)
        'hid1.Value = ""
        'hid2.Value = ""
        'hid.Value = ""
    End Sub

    
End Class
