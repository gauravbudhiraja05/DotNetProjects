Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO
Imports System.IO.StreamReader
Partial Class DataAnalysis_DeleteAnalysis
    Inherits System.Web.UI.Page
    Dim repcolarray
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim con As New SqlConnection(conn)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim p
    ' Dim j As Integer = 0
    Dim bindmsg As Integer = 0
    Dim dr As SqlDataReader
    Public stt As String
    Dim colname, formula, groupby, orderby As String
    Dim connection As New SqlConnection(conn)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    
    ''' <summary>
    ''' call the function to show the analysis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

   
    
    
    ''' <summary>
    ''' fill departments
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                'Me.spandisplay.Visible = True
                dept_row.Visible = True
                client_row.Visible = True
                lob_row.Visible = True
                'getanalysis_singleuser.Visible = False
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
                'getanalysis_singleuser.Visible = True
                button_row.Visible = True
            End If
        End If
        'End If


        Dim typeofuser = Session("typeofuser")
        If Page.IsPostBack = False Then
            If (typeofuser.Equals("Super Admin")) Then
                connection.Close()
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
            DepartmentName.Items.Insert(0, "--select--")
        End If
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Try


            Dim classobj As New Functions
            If Page.IsPostBack = False Then
                Dim lblThispage As Label = Master.FindControl("lblPage")
                lblThispage.Text = "Data Analysis"
                'ddlDepartmant.DataTextField = "departmentname"
                'ddlDepartmant.DataValueField = "DeptID"
                'ddlDepartmant.DataSource = classobj.bind_Dept()
                'ddlDepartmant.DataBind()
                'ddlDepartmant.Items.Insert(0, "---select---")
                'pnlConfirmDel.Visible = False

            End If
        Catch ex As Exception
            'Dim strmsg As String
            '        strmsg = Replace(ex.Message.ToString, "'", "")
            '        strmsg = Replace(strmsg, vbCrLf, " ")
            '        aspnet_msgbox(strmsg)
        End Try
        'Page.SmartNavigation = True


    End Sub
    Protected Sub ShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport.Click
        If (DepartmentName.SelectedIndex = 0) Then
            aspnet_msgbox("Please Select Department First")
            Exit Sub
        End If
        If (ddlrpt.SelectedIndex = 0) Then
            aspnet_msgbox("Please Select Html Report First")
            Exit Sub
        End If
        'bindmsg = 0
        'Try


        '    'If ddlDepartmant.SelectedItem.Text = "---select---" Then
        '    '    '    aspnet_msgbox("Select Department First")

        '    '    Exit Sub
        '    'Else

        '    Dim newarr As String = hidvalue().ToString
        '    Dim arrrs = newarr.Split(",")

        '    gridbind(arrrs(0), arrrs(1), arrrs(2))
        '    'End If
        'Catch ex As Exception
        '    Dim strmsg As String
        '    strmsg = Replace(ex.Message.ToString, "'", "")
        '    strmsg = Replace(strmsg, vbCrLf, " ")
        '    aspnet_msgbox(strmsg)
        'End Try
        'Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("Bmp10ConnectionString").ConnectionString)
        con.Open()
        Dim cmd As SqlCommand = New SqlCommand("Delete from dataanalysishtmlreport where ReportName='" + ddlrpt.SelectedItem.Text + "' ", con)
        Dim dr As SqlDataReader = cmd.ExecuteReader()
        aspnet_msgbox("Report Deleted Successfully")
        con.Close()
        cmd.Dispose()
        dr.Close()
        get_analysis(DepartmentName.SelectedValue, ClientName.SelectedValue, ddlLobName.SelectedValue)
        'Page.SmartNavigation = True

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
    Public Sub gridbind(ByVal dept As String, ByVal client As String, ByVal lob As String)
        Dim obj As New SavedAnalysis
        Dim item As GridViewRow
        Dim ds As New DataSet
        Try


            If Session("typeofuser") = "Admin" Then
                ds = obj.Select_Analysis(Session("userid"), dept, client, lob)


            ElseIf Session("typeofuser") = "User" Then
                Dim SCOPE As String = (repobj.chkUserscope(Session("userid"))).ToString
                If SCOPE = "Local" Then
                    ds = obj.bind_localuser(Session("userid"), dept, client, lob)
                    'ds = = repobj.reportForlocal(Session("userid"), dept, client, lob)
                Else
                    ds = obj.bind_nonlocaluser(Session("userid"), dept, client, lob)
                End If

            End If


            If ds.Tables(0).Rows.Count = 0 Then
                If bindmsg = 0 Then
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
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            aspnet_msgbox(strmsg)
        End Try

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
    ''' paging to gridview
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
    ''' for deleting the selected row
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub grdhtmlreport_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdhtmlreport.RowDeleting
        'pnlConfirmDel.Visible = True
        'Dim b
        'Dim cmd As New SqlCommand


        'Dim item As GridViewRow

        'Dim deletecheck As Boolean = False
        'For Each item In grdhtmlreport.Rows

        '    If CType(item.FindControl("chk"), CheckBox).Checked = True Then
        '        deletecheck = True
        '        b = CType(item.FindControl("fndlbl"), Label).Text


        '        cmd = New SqlCommand("delete from savedanalysis where analysisname=@namerep", con)
        '        cmd.Parameters.Add("@namerep", SqlDbType.VarChar, 50)
        '        cmd.Parameters("@namerep").Value = b
        '        con.Open()
        '        cmd.ExecuteNonQuery()
        '        con.Close()
        '        cmd = New SqlCommand("Sp_LogDataAnalysisForAnalysisDelete", con)
        '        cmd.CommandType = CommandType.StoredProcedure

        '        cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50)
        '        cmd.Parameters("@UserID").Value = Session("userid")
        '        cmd.Parameters.Add("@analysisname", SqlDbType.VarChar, 50)
        '        cmd.Parameters("@analysisname").Value = b
        '        con.Open()
        '        cmd.ExecuteNonQuery()
        '        con.Close()
        '        cmd = New SqlCommand("select name from sysobjects where xtype='u' and name='" + b + "'", con)
        '        con.Open()
        '        readquery = cmd.ExecuteReader
        '        While readquery.Read()
        '            If readquery("name") = "R566R" Then
        '                b = False
        '                Exit While

        '            Else
        '                b = True
        '            End If
        '        End While
        '        readquery.Close()
        '        con.Close()

        '        If b = False Then
        '            cmd = New SqlCommand("drop table " + b + "", con)
        '            con.Open()
        '            cmd.ExecuteNonQuery()
        '            con.Close()
        '        End If
        '    End If
        'Next
        'If deletecheck = False Then
        '    aspnet_msgbox("No CheckBox Is Selected")
        'End If

        'Dim newarr As String = hidvalue().ToString
        'Dim arrrs = newarr.Split(",")

        'gridbind(arrrs(0), arrrs(1), arrrs(2))

    End Sub
    ''' <summary>
    ''' fill clients
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub ddlDepartmant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmant.SelectedIndexChanged
    '    Try


    '        If ddlDepartmant.SelectedItem.Text = "---select---" Then
    '            hid.Value = ""
    '            hid1.Value = ""
    '            hid2.Value = ""

    '            ddlClient.Items.Clear()
    '            ddlLob.Items.Clear()
    '        Else
    '            Dim classobj As New Functions



    '            ddlClient.DataTextField = "clientname"
    '            ddlClient.DataValueField = "autoid"
    '            ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)
    '            hid.Value = ddlDepartmant.SelectedValue
    '            'hid1.Value = "0"
    '            'hid2.Value = "0"
    '            'DEPART.Value = ddlDepartmant.SelectedItem.Text
    '            'client.Value = "0"
    '            'lob.Value = "0"
    '            ddlClient.DataBind()
    '            ddlClient.Items.Insert(0, "---select---")
    '            ddlLob.Items.Clear()
    '        End If
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        aspnet_msgbox(strmsg)
    '    End Try
    'End Sub
    ''' <summary>
    ''' fill lobs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
    '    Try


    '        If ddlClient.SelectedItem.Text = "---select---" Then


    '            hid1.Value = ""
    '            hid2.Value = ""
    '            ddlLob.Items.Clear()
    '        Else
    '            Dim classobj As New Functions

    '            ddlLob.DataTextField = "lobname"
    '            ddlLob.DataValueField = "autoid"
    '            ddlLob.DataSource = classobj.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
    '            hid1.Value = ddlClient.SelectedValue
    '            'hid2.Value = "0"
    '            'client.Value = DropDownclient.SelectedItem.Text
    '            'lob.Value = "0"
    '            ddlLob.DataBind()
    '            ddlLob.Items.Insert(0, "---select---")
    '        End If
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        aspnet_msgbox(strmsg)
    '    End Try
    'End Sub
    ''' <summary>
    ''' get id lob lob
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

    'Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
    '    Dim b
    '    Dim b1
    '    Dim cmd As New SqlCommand
    '    Dim dss As New DataSet
    '    Dim dataa As New SqlDataAdapter
    '    Dim item As GridViewRow
    '    Dim deleteright As String = ""
    '    Dim deletecheck As Boolean = False
    '    bindmsg = 1
    '    Try
    '        For Each item In grdhtmlreport.Rows

    '            If CType(item.FindControl("chk"), CheckBox).Checked = True Then
    '                deletecheck = True
    '                b = CType(item.FindControl("fndlbl"), Label).Text
    '                Dim x As String = CType(item.FindControl("fndlbl"), Label).Text
    '                cmd = New SqlCommand("select savedby from savedanalysis where analysisname='" + x + "'", con)

    '                dataa.SelectCommand = cmd

    '                dataa.Fill(dss)
    '                Dim owner As String = ""
    '                If dss.Tables(0).Rows.Count > 0 Then

    '                    owner = dss.Tables(0).Rows(0)(0)
    '                Else
    '                    aspnet_msgbox("You are not authorised to delete selected analysis")
    '                    'pnlConfirmDel.Visible = False
    '                    Dim newarr1 As String = hidvalue().ToString
    '                    Dim arrrs1 = newarr1.Split(",")

    '                    gridbind(arrrs1(0), arrrs1(1), arrrs1(2))
    '                    Exit Sub

    '                End If
    '                cmd.Dispose()
    '                dss.Clear()
    '                If LCase(owner) = LCase(Session("userid")) Then
    '                Else
    '                    cmd = New SqlCommand("select [delete] from idms_analysisrights where analysisname='" + x + "' and userid='" + Session("userid") + "'", con)

    '                    dataa.SelectCommand = cmd

    '                    dataa.Fill(dss)
    '                    If dss.Tables(0).Rows.Count > 0 Then
    '                        deleteright = dss.Tables(0).Rows(0)(0).ToString
    '                        If LCase(deleteright) = "true" Then
    '                        Else
    '                            aspnet_msgbox("You are not authorised to delete selected analysis")
    '                            'pnlConfirmDel.Visible = False
    '                            Dim newarr1 As String = hidvalue().ToString
    '                            Dim arrrs1 = newarr1.Split(",")

    '                            gridbind(arrrs1(0), arrrs1(1), arrrs1(2))
    '                            Exit Sub

    '                        End If

    '                    Else
    '                        aspnet_msgbox("You are not authorised to delete selected analysis")
    '                        'pnlConfirmDel.Visible = False
    '                        Dim newarr2 As String = hidvalue().ToString
    '                        Dim arrrs2 = newarr2.Split(",")

    '                        gridbind(arrrs2(0), arrrs2(1), arrrs2(2))
    '                        Exit Sub

    '                    End If
    '                    cmd.Dispose()
    '                    dss.Clear()
    '                End If
    '                '''''''''''track shifted here
    '                cmd = New SqlCommand("Sp_LogDataAnalysisForAnalysisDelete", con)
    '                cmd.CommandType = CommandType.StoredProcedure

    '                cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50)
    '                cmd.Parameters("@UserID").Value = Session("userid")
    '                cmd.Parameters.Add("@analysisname", SqlDbType.VarChar, 50)
    '                cmd.Parameters("@analysisname").Value = b
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()
    '                '''''''''''''''Usertype check for track goes here:- By Suvidha

    '                Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where analysisname='" + x + "' and Action='Delete' and Entity='Analysis'", con)
    '                con.Open()
    '                cmm.ExecuteNonQuery()
    '                con.Close()
    '                '''''''''''''''Usertype check for track goes here:- By Suvidha
    '                '''''''''''track shifted here
    '                cmd = New SqlCommand("delete from savedanalysis where analysisname=@namerep", con)
    '                cmd.Parameters.Add("@namerep", SqlDbType.VarChar, 50)
    '                cmd.Parameters("@namerep").Value = b
    '                con.Open()
    '                cmd.ExecuteNonQuery()
    '                con.Close()

    '                cmd = New SqlCommand("select name from sysobjects where xtype='u' and name='" + b + "'", con)
    '                con.Open()
    '                readquery = cmd.ExecuteReader
    '                While readquery.Read()
    '                    If readquery("name") = "R566R" Then
    '                        b1 = False
    '                        Exit While

    '                    Else
    '                        b1 = True
    '                    End If
    '                End While
    '                readquery.Close()
    '                con.Close()

    '                If b1 = False Then
    '                    cmd = New SqlCommand("drop table " + b + "", con)
    '                    con.Open()
    '                    cmd.ExecuteNonQuery()
    '                    con.Close()
    '                End If
    '            End If
    '        Next
    '        If deletecheck = False Then
    '            aspnet_msgbox("No CheckBox Is Selected")
    '        Else
    '            aspnet_msgbox("Analysis has been deleted successfully.")

    '        End If

    '        Dim newarr As String = hidvalue().ToString
    '        Dim arrrs = newarr.Split(",")

    '        gridbind(arrrs(0), arrrs(1), arrrs(2))
    '        'pnlConfirmDel.Visible = False
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        aspnet_msgbox(strmsg)
    '    End Try
    'End Sub

    'Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
    '    pnlConfirmDel.Visible = False
    'End Sub

    'Protected Sub grdhtmlreport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdhtmlreport.SelectedIndexChanged

    'End Sub

    Protected Sub getanalysis_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles getanalysis_singleuser.Click
        get_analysis(60, 0, 0)
    End Sub

    Protected Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        If (ddlLobName.SelectedIndex = 0 Or ddlLobName.SelectedValue = "--Select--") Then
            ddlrpt.Items.Clear()
            get_analysis(DepartmentName.SelectedValue, ClientName.SelectedValue, 0)
        Else
            get_analysis(DepartmentName.SelectedValue, ClientName.SelectedValue, ddlLobName.SelectedValue)
        End If
    End Sub
    Public Function get_analysis(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        con.Close()
        cmd = New SqlCommand("Select * from dataanalysishtmlreport where DepartmentId='" + deptid + "' and ClientId='" + clientid + "' and LOBId='" + lobid + "' and SavedBy='" + Session("userid") + "'", con)
        con.Open()
        Dim dr As SqlDataReader = cmd.ExecuteReader()
        ddlrpt.DataSource = dr
        ddlrpt.DataTextField = "ReportName"
        ddlrpt.DataValueField = "ReportName"
        ddlrpt.DataBind()
        ddlrpt.Items.Insert(0, "--Select--")
    End Function

    Protected Sub level1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If (DepartmentName.SelectedIndex = 0) Then
            ddlLobName.Items.Clear()
            ClientName.Items.Clear()
            ddlrpt.Items.Clear()
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ClientName.DataSource = dr
            ClientName.DataTextField = "ClientName"
            ClientName.DataValueField = "autoid"
            ClientName.DataBind()
            ClientName.Items.Insert(0, "--Select--")
            get_analysis(DepartmentName.SelectedValue, 0, 0)
        End If
        
    End Sub

    Protected Sub level2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If (ClientName.SelectedValue = "-- Select --") Then
            ddlLobName.Items.Clear()
            ddlrpt.Items.Clear()
            get_analysis(DepartmentName.SelectedValue, 0, 0)
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ddlLobName.DataSource = dr
            ddlLobName.DataTextField = "LOBName"
            ddlLobName.DataValueField = "autoid"
            ddlLobName.DataBind()
            ddlLobName.Items.Insert(0, "--Select--")
            get_analysis(DepartmentName.SelectedValue, ClientName.SelectedValue, 0)
        End If
        
    End Sub
End Class
