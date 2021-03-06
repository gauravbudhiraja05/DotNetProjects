Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class ReportDesigner_SaveReport
    Inherits System.Web.UI.Page
    Dim fun As New Functions
    Dim insertNew As New ReportDesigner
    Dim StrTableName As String = ""
    Dim dsProcess As New DataSet
    Dim daClient As New SqlDataAdapter
    Dim con As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Public dept As String = "0"
    Public client As String = "0"
    Public lob As String = "0"
    Public repType As String = "Simple"
    Public repName1 As String = ""
    Public repscope As String = "NonLocal"
    Public datetable As String = ""
    Dim createDate As String = ""
    Dim createdBy As String = ""
    Public recordid As String = ""
    Dim height As String()
    Public ex345 As String = ""
    Public tblDate = ""
    Public reload = True
    Public lo = ""
    Public od = "0"
    Public ocl = "0"
    Public olb = "0"
    Dim strcon As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(strcon)
    Dim cmdnew As SqlCommand
    Dim rdrnew As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        connection.Open()
        cmdnew = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdrnew = cmdnew.ExecuteReader
        If rdrnew.Read Then
            Dim producttype As String = rdrnew("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                Me.btnSavenewMul.Visible = True
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
                Me.btnSavenew.Visible = True
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
        connection.Close()
    End Sub
    ''' <summary>
    ''' Asp MsgBox
    ''' </summary>
    ''' <param name="message">messageTopass</param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' Save New Report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnSavenew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavenew.Click
        If Session("userid") = "" Then
            lblError.Text = "Session Expired. Login Again"
            Exit Sub
        End If
      
            If (txtReportname.Text = "") Then
                lblError.Text = "Please supply report name."

            Else

                If (txtReportname.Text.Contains(" ") Or LCase(txtReportname.Text).StartsWith("tab") Or txtReportname.Text.Contains("@") Or txtReportname.Text.Contains("#") Or txtReportname.Text.Contains("&") Or txtReportname.Text.Contains("*") Or txtReportname.Text.Contains("(") Or txtReportname.Text.Contains(")") Or txtReportname.Text.Contains("+") Or txtReportname.Text.Contains("-")) Then
                    lblError.Text = "Report Name Cannot Contain Any Blank Space Or Special Character"
                Else
                   
                Dim repname = hidReportname.Value
                    Dim precid = ""
                    Dim b As String = ""
                    Dim myRight As String = ""
                b = insertNew.CheckExistingReport2(Me.txtReportname.Text, 60, 0, 0) ' Check if the reportname already exists?

                    If b = "" Then
                        If (hidReportmode.Value = "Edit") Then
                            precid = hidRecordid.Value
                            b = insertNew.getOwner(hidRecordid.Value) '  if the report is being copied, check for the user rights
                            If b <> Session("userid") Then
                                '' Ask for the right on the report
                                Dim rights As String = insertNew.fetchReportrights(precid, Session("userid"))

                                Dim sp = Split(rights, ",")
                                If (sp.length > 1) Then
                                    myRight = sp(3)
                                End If

                            End If

                        End If

                        If b = Session("userid") Or myRight = "True" Or b = "" Then   ' if no then save the report

                            If (Me.chksimple.Checked = True) Then
                                repType = "Simple"
                            Else
                                repType = "Summarized"
                            End If

                            'txtReportname.Text = ddlClient.SelectedValue + "," + ddlLob.SelectedValue
                            createDate = System.DateTime.Today.ToString()
                            createdBy = Session("userid")
                            'createdBy = "admin"
                            height = Split(hidHeight.Value, ",")
                            Dim str As String = ""
                            Dim str1 As String = ""
                            Dim str2 As String = ""
                            Dim cols As String = ""
                            If (hidDpos.Value).Trim() = "" Then
                                cols = ""
                            Else
                                hidDpos.Value = Replace(hidDpos.Value, vbNewLine, "")
                                cols = Trim(hidDpos.Value)
                                ' replace formulas with formula definition
                                Dim formul = Me.hidFormulas.Value
                                If formul <> "" Then
                                    Dim for1 = Split(formul, "~")
                                    Dim nm = 0
                                    For nm = 0 To for1.length - 1
                                        Dim bnm = Split(for1(nm), " AS ")
                                        cols = Replace(cols, bnm(1), for1(nm))
                                    Next
                                End If

                                '''''''''''''''''''''

                            End If

                            Dim formulaname As String = ""
                            If (hidFormulaname.Value).Trim() = "" Then
                                formulaname = ""
                            Else
                                formulaname = Trim(hidFormulaname.Value)
                            End If
                            Dim formula As String = ""
                            If (hidDFormula.Value).Trim() = "" Then
                                formula = ""
                            Else
                                formula = Trim(hidDFormula.Value)
                            End If
                            Dim where As String = ""
                            If (hidWhere.Value).Trim() = "" Then 'Or hidWhere.Value = " " Or hidWhere.Value = "   " Or hidWhere.Value = "    " Or hidWhere.Value = "     " Then
                                where = ""
                            Else
                                where = Trim(hidWhere.Value)
                            End If

                            Dim groupby As String = ""
                            If (hidGroupby.Value).Trim() = "" Then
                                groupby = ""
                            Else

                                groupby = insertNew.updateGroupby(cols, hidGroupby.Value)
                                If groupby = "" Then
                                    groupby = ""
                                End If
                            End If

                            Dim orderby As String = ""
                            If (hidOrderby.Value).Trim() = "" Then
                                orderby = ""
                            Else
                                'orderby = groupby
                                If LCase(Trim(hidOrderby.Value)).Contains("desc") Then

                                    orderby = hidOrderby.Value
                                ElseIf LCase(Trim(hidOrderby.Value)).Contains("asc") Then
                                    orderby = hidOrderby.Value
                                    ''
                                ElseIf Trim(Request("hidOrderby")).Contains("AS") Then

                                    orderby = groupby

                                    ''
                                Else
                                    orderby = groupby

                                    ' orderby = hidOrderby.Value

                                End If
                                'orderby = hidOrderby.Value
                            End If

                            Dim having As String = ""
                            If (hidHaving.Value).Trim() = "" Then
                                having = ""
                            Else
                                having = Trim(hidHaving.Value)
                            End If

                            Dim colorcondition As String = ""
                            If (hidColorcondition.Value).Trim() = "" Then
                                colorcondition = ""
                            Else
                                colorcondition = Trim(Me.hidColorcondition.Value)
                            End If

                            Dim columnFormat As String = ""
                            If (hidColumnformat.Value).Trim() = "" Then
                                columnFormat = ""
                            Else
                                columnFormat = Trim(Me.hidColumnformat.Value)
                            End If
                            hidHformula.Value = Replace(hidHformula.Value, vbNewLine, "")
                            hidFformula.Value = Replace(hidFformula.Value, vbNewLine, "")
                        repname = txtReportname.Text
                        Session("reportname") = repname
                            repName1 = Me.txtReportname.Text
                            tblDate = Me.hidDatecon.Value
                            If hidTables.Value <> "" Then
                                Replace(hidTables.Value, "$", ",")
                            End If

                        str = insertNew.insertReport2(txtReportname.Text, 60, 0, 0, hidTables.Value, "Header_" + txtReportname.Text, "Footer_" + txtReportname.Text, cols, formulaname, formula, where, groupby, orderby, having, hidDetailsformat.Value, colorcondition, repType, repscope, hidDatecon.Value, createDate, createdBy, columnFormat, hidlevelsave.Value)

                            If (str = "1") Then
                                '''' fetch recordid of he latest report
                            hidRecordid.Value = insertNew.fetchReportRecordid(Me.txtReportname.Text)
                                recordid = hidRecordid.Value
                                '''''''''''''''Usertype check for track goes here:- By Suvidha

                            'Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + Me.txtReportname.Text + "' and Action='Save'", conn)
                            'conn.Open()
                            'cmm.ExecuteNonQuery()
                            'conn.Close()
                                '''''''''''''''Usertype check for track goes here:- By Suvidha


                                str1 = insertNew.insertHeader(txtReportname.Text, "Header_" + txtReportname.Text, height(0), Me.hidHeaderformat.Value, hidHpos.Value, hidHformat.Value, hidHformula.Value, hidHcolorcon.Value, createDate, createdBy, recordid)
                            End If
                            If (str = "1" And str1 = "1") Then
                                str2 = insertNew.insertFooter(txtReportname.Text, "Footer_" + txtReportname.Text, height(1), Me.hidFooterformat.Value, hidFpos.Value, hidFformat.Value, hidFformula.Value, hidFcolorcon.Value, createDate, createdBy, recordid)
                            End If
                            If (str = "1" And str1 = "1" And str2 = "1") Then
                                lblError.Text = "Report saved successfully."
                                Session("repName") = txtReportname.Text
                                ''''' save existing graphs on existing report
                                If Trim(precid) <> "" Then
                                    insertNew.copyGraphs(precid, Session("userid"), txtReportname.Text, dept, client, lob)
                                End If

                                '''''''''''''''''''''''''''''''''''''
                                final()
                            Else
                                lblError.Text = str + ", " + str1 + ", " + str2 '"Error Occured while saving the report. Please try again."
                                'aspnet_msgbox("Error Occured while saving the report. Please try again.")
                            End If
                        ElseIf myRight = "False" Or myRight = "" Then
                            lblError.Text = "You dont have rights to create a copy of this report."
                        End If

                    Else  ' else raise exception
                        If b <> "" And myRight = "" Then
                            lblError.Text = "Report name already exists. Please supply new report name."
                            'aspnet_msgbox("Report name already exists. Please supply new report name.")

                        End If

                    End If
                End If
            End If
    End Sub
    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        connection.Open()
        cmdnew = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdrnew = cmdnew.ExecuteReader
        If rdrnew.Read Then
            Dim producttype As String = rdrnew("ProductType")
            If (producttype = "Multiple User") Then
                dept = ddlDepartment.SelectedValue

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
            End If
            connection.Close()
            cmdnew.Dispose()
            rdrnew.Close()
        Else
            dept = 60
            client = 0
            lob = 0
            connection.Close()
            cmdnew.Dispose()
            rdrnew.Close()
        End If
        If Session("userid") = "" Then
            lblError.Text = "Session Expired. Login Again"
            Exit Sub
        End If
        'If Session("ex") = "" Then
        '    lblError.Text = "Process The Report First"
        '    Exit Sub

        'End If
        'ex345 = ex345.Replace("aa", "")
        If ddlDepartment.SelectedValue = "" Or ddlDepartment.SelectedValue = "--Select--" Then
            lblError.Text = "Please Select Department"
            'aspnet_msgbox("Please Select Department")
            'If (txtReportname.Text = "") Then
            '    lblError.Text = "Please supply report name."
            '    ' aspnet_msgbox("Please supply report name.")
        Else
            'If ddlDepartment.SelectedValue = "" Or ddlDepartment.SelectedValue = "--Select--" Then
            '    lblError.Text = "Please Select Department"
            'aspnet_msgbox("Please Select Department")
            If (txtReportname.Text = "") Then
                lblError.Text = "Please supply report name."
                '    ' aspnet_msgbox("Please supply report name.")
            Else

                Dim b As String = insertNew.CheckExistingReport2(Me.txtReportname.Text, dept, client, lob) ' Check if the reportname already exists?
                If b = "" Then   ' if no then save the report
                    lblError.Text = "No report name found. Please save the report first."
                    Exit Sub
                Else
                    '' Check if the user has got the right to update the report()
                    hidRecordid.Value = insertNew.fetchReportRecordid2(Me.txtReportname.Text, dept, client, lob)
                    Session("hidrecordid1") = hidRecordid.Value

                    Dim myRight As String = ""
                    If b <> Session("userid") Then

                        '' Ask for the right on the report
                        Dim rights As String = insertNew.fetchReportrights(hidRecordid.Value, Session("userid"))
                        Dim sp = Split(rights, ",")
                        If sp.Length > 1 Then
                            myRight = sp(1)
                        End If
                    End If
                    If (myRight = "True" Or b = Session("userid")) Then  '' Either the user has rights or is the report owner

                        If (Me.chksimple.Checked = True) Then
                            repType = "Simple"
                        Else
                            repType = "Summarized"
                        End If
                        'If (chkLocal.Checked = True) Then
                        repscope = "Local"
                        'Else
                        '    repscope = "NonLocal"
                        'End If
                        height = Split(hidHeight.Value, ",")
                        Dim str As String = ""
                        Dim str1 As String = ""
                        Dim str2 As String = ""
                        Dim cols As String = ""
                        If (hidDpos.Value).Trim() = "" Then
                            cols = ""
                        Else
                            hidDpos.Value = Replace(hidDpos.Value, vbNewLine, "")
                            cols = Trim(hidDpos.Value)
                            ' replace formulas with formula definition
                            Dim formul = Me.hidFormulas.Value
                            If formul <> "" Then
                                Dim for1 = Split(formul, "~")
                                Dim nm = 0
                                For nm = 0 To for1.Length - 1
                                    Dim bnm = Split(for1(nm), " AS ")
                                    cols = Replace(cols, bnm(1), for1(nm))
                                Next
                            End If
                            '''''''''''''''''''''
                        End If

                        Dim formulaname As String = ""
                        If (hidFormulaname.Value).Trim() = "" Then
                            formulaname = ""
                        Else
                            formulaname = Trim(hidFormulaname.Value)
                        End If
                        Dim formula As String = ""
                        If (hidFormula.Value).Trim() = "" Then
                            formula = ""
                        Else
                            formula = Trim(hidFormula.Value)
                        End If
                        Dim where As String = ""
                        If (hidWhere.Value).Trim() = "" Then 'Or hidWhere.Value = " " Or hidWhere.Value = "   " Or hidWhere.Value = "    " Or hidWhere.Value = "     " Then
                            where = ""
                        Else
                            where = Trim(hidWhere.Value)
                        End If

                        Dim groupby As String = ""
                        If (hidGroupby.Value).Trim() = "" Then
                            groupby = ""
                        Else
                            groupby = insertNew.updateGroupby(cols, hidGroupby.Value)
                            If groupby = "" Then
                                groupby = ""
                            End If
                        End If

                        Dim orderby As String = ""
                        If (hidOrderby.Value).Trim() = "" Then
                            orderby = ""
                        Else
                            'orderby = groupby
                            If LCase(Trim(hidOrderby.Value)).Contains("desc") Then

                                orderby = hidOrderby.Value
                            ElseIf LCase(Trim(hidOrderby.Value)).Contains("asc") Then
                                orderby = hidOrderby.Value
                                ''
                            ElseIf Trim(Request("hidOrderby")).Contains("AS") Then

                                orderby = groupby

                                ''
                            Else
                                orderby = groupby

                                'orderby = hidOrderby.Value

                            End If
                            'orderby = hidOrderby.Value
                        End If
                        '                     If (hidOrderby.Value).Trim() = "" Then
                        '                         orderby = ""
                        '                     Else
                        '                         'orderby = groupby
                        'orderby = hidOrderby.Value
                        '                     End If

                        Dim having As String = ""
                        If (hidHaving.Value).Trim() = "" Then
                            having = ""
                        Else
                            having = Trim(hidHaving.Value)
                        End If

                        Dim colorcondition As String = ""
                        If (hidColorcondition.Value).Trim() = "" Then
                            colorcondition = ""
                        Else
                            colorcondition = Trim(Me.hidColorcondition.Value)
                        End If

                        Dim columnFormat As String = ""
                        If (hidColumnformat.Value).Trim() = "" Then
                            columnFormat = ""
                        Else
                            columnFormat = Trim(Me.hidColumnformat.Value)
                        End If
                        repName1 = Me.txtReportname.Text
                        hidHformula.Value = Replace(hidHformula.Value, vbNewLine, "")
                        hidFformula.Value = Replace(hidFformula.Value, vbNewLine, "")
                        tblDate = Me.hidDatecon.Value
                        If hidTables.Value <> "" Then
                            Replace(hidTables.Value, "$", ",")
                        End If
                        str = insertNew.Update_Report(txtReportname.Text, dept, client, lob, hidTables.Value, cols, formulaname, formula, where, groupby, orderby, having, hidDetailsformat.Value, colorcondition, repType, repscope, hidDatecon.Value, columnFormat, hidlevelsave.Value)

                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                        Dim strup = insertNew.trackUpdatereport(Session("userid"), System.DateTime.Today.ToString(), txtReportname.Text, dept, client, lob)
                        Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + Me.txtReportname.Text + "' and Action='Update'", conn)
                        conn.Open()
                        cmm.ExecuteNonQuery()
                        conn.Close()
                        '''''''''''''''Usertype check for track goes here:- By Suvidha
                        If (str = "1") Then
                            str1 = insertNew.Update_Header(txtReportname.Text, height(0), Me.hidHeaderformat.Value, hidHpos.Value, hidHformat.Value, hidHformula.Value, hidHcolorcon.Value, hidRecordid.Value)
                        End If
                        If (str = "1" And str1 = "1") Then
                            str2 = insertNew.Update_Footer(txtReportname.Text, height(1), Me.hidFooterformat.Value, hidFpos.Value, hidFformat.Value, hidFformula.Value, hidFcolorcon.Value, hidRecordid.Value)
                        End If
                        If (str = "1" And str1 = "1" And str2 = "1") Then
                            Session("repName") = txtReportname.Text
                            lblError.Text = "Report Updated Successfully."
                            final()
                        Else
                            lblError.Text = "Error Occured. Please try again."
                            'aspnet_msgbox("Error Occured. Please try again.")
                        End If
                    Else  '' otherwise raise an alert
                        lblError.Text = "You dont have rights to update this report."
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub final()
        Dim str As String = "<script language=Javascript>"
        str = str + "Done();"
        str = str + "</script>"
        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "Done", str)
    End Sub

    Protected Sub btnSavenewMul_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSavenewMul.Click
        If Session("userid") = "" Then
            lblError.Text = "Session Expired. Login Again"
            Exit Sub
        End If
        If ddlDepartment.SelectedValue = "" Or ddlDepartment.SelectedValue = "--Select--" Then
            lblError.Text = "Please Select Department"

        Else

            If (txtReportname.Text = "") Then
                lblError.Text = "Please supply report name."

            Else

                If (txtReportname.Text.Contains(" ") Or LCase(txtReportname.Text).StartsWith("tab") Or txtReportname.Text.Contains("@") Or txtReportname.Text.Contains("#") Or txtReportname.Text.Contains("&") Or txtReportname.Text.Contains("*") Or txtReportname.Text.Contains("(") Or txtReportname.Text.Contains(")") Or txtReportname.Text.Contains("+") Or txtReportname.Text.Contains("-")) Then
                    lblError.Text = "Report Name Cannot Contain Any Blank Space Or Special Character"
                Else
                    dept = ddlDepartment.SelectedValue

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
                    Dim repname = hidReportname.Value
                    Dim precid = ""
                    Dim b As String = ""
                    Dim myRight As String = ""
                    b = insertNew.CheckExistingReport2(Me.txtReportname.Text, dept, client, lob) ' Check if the reportname already exists?

                    If b = "" Then
                        If (hidReportmode.Value = "Edit") Then
                            precid = hidRecordid.Value
                            b = insertNew.getOwner(hidRecordid.Value) '  if the report is being copied, check for the user rights
                            If b <> Session("userid") Then
                                '' Ask for the right on the report
                                Dim rights As String = insertNew.fetchReportrights(precid, Session("userid"))

                                Dim sp = Split(rights, ",")
                                If (sp.Length > 1) Then
                                    myRight = sp(3)
                                End If

                            End If

                        End If

                        If b = Session("userid") Or myRight = "True" Or b = "" Then   ' if no then save the report

                            If (Me.chksimple.Checked = True) Then
                                repType = "Simple"
                            Else
                                repType = "Summarized"
                            End If
                            'If (chkLocal.Checked = True) Then
                            '    repscope = "Local"
                            'ElseIf Gbl.Checked = True Then
                            '    repscope = "Global"
                            'Else

                            repscope = "NonLocal"
                            'End If
                            'txtReportname.Text = ddlClient.SelectedValue + "," + ddlLob.SelectedValue
                            createDate = System.DateTime.Today.ToString()
                            createdBy = Session("userid")
                            'createdBy = "admin"
                            height = Split(hidHeight.Value, ",")
                            Dim str As String = ""
                            Dim str1 As String = ""
                            Dim str2 As String = ""
                            Dim cols As String = ""
                            If (hidDpos.Value).Trim() = "" Then
                                cols = ""
                            Else
                                hidDpos.Value = Replace(hidDpos.Value, vbNewLine, "")
                                cols = Trim(hidDpos.Value)
                                ' replace formulas with formula definition
                                Dim formul = Me.hidFormulas.Value
                                If formul <> "" Then
                                    Dim for1 = Split(formul, "~")
                                    Dim nm = 0
                                    For nm = 0 To for1.Length - 1
                                        Dim bnm = Split(for1(nm), " AS ")
                                        cols = Replace(cols, bnm(1), for1(nm))
                                    Next
                                End If

                                '''''''''''''''''''''

                            End If

                            Dim formulaname As String = ""
                            If (hidFormulaname.Value).Trim() = "" Then
                                formulaname = ""
                            Else
                                formulaname = Trim(hidFormulaname.Value)
                            End If
                            Dim formula As String = ""
                            If (hidDFormula.Value).Trim() = "" Then
                                formula = ""
                            Else
                                formula = Trim(hidDFormula.Value)
                            End If
                            Dim where As String = ""
                            If (hidWhere.Value).Trim() = "" Then 'Or hidWhere.Value = " " Or hidWhere.Value = "   " Or hidWhere.Value = "    " Or hidWhere.Value = "     " Then
                                where = ""
                            Else
                                where = Trim(hidWhere.Value)
                            End If

                            Dim groupby As String = ""
                            If (hidGroupby.Value).Trim() = "" Then
                                groupby = ""
                            Else

                                groupby = insertNew.updateGroupby(cols, hidGroupby.Value)
                                If groupby = "" Then
                                    groupby = ""
                                End If
                            End If

                            Dim orderby As String = ""
                            If (hidOrderby.Value).Trim() = "" Then
                                orderby = ""
                            Else
                                'orderby = groupby
                                If LCase(Trim(hidOrderby.Value)).Contains("desc") Then

                                    orderby = hidOrderby.Value
                                ElseIf LCase(Trim(hidOrderby.Value)).Contains("asc") Then
                                    orderby = hidOrderby.Value
                                    ''
                                ElseIf Trim(Request("hidOrderby")).Contains("AS") Then

                                    orderby = groupby

                                    ''
                                Else
                                    orderby = groupby

                                    ' orderby = hidOrderby.Value

                                End If
                                'orderby = hidOrderby.Value
                            End If

                            Dim having As String = ""
                            If (hidHaving.Value).Trim() = "" Then
                                having = ""
                            Else
                                having = Trim(hidHaving.Value)
                            End If

                            Dim colorcondition As String = ""
                            If (hidColorcondition.Value).Trim() = "" Then
                                colorcondition = ""
                            Else
                                colorcondition = Trim(Me.hidColorcondition.Value)
                            End If

                            Dim columnFormat As String = ""
                            If (hidColumnformat.Value).Trim() = "" Then
                                columnFormat = ""
                            Else
                                columnFormat = Trim(Me.hidColumnformat.Value)
                            End If
                            hidHformula.Value = Replace(hidHformula.Value, vbNewLine, "")
                            hidFformula.Value = Replace(hidFformula.Value, vbNewLine, "")
                            repname = txtReportname.Text
                            Session("reportname") = repname
                            repName1 = Me.txtReportname.Text
                            tblDate = Me.hidDatecon.Value
                            If hidTables.Value <> "" Then
                                Replace(hidTables.Value, "$", ",")
                            End If

                            str = insertNew.insertReport2(txtReportname.Text, dept, client, lob, hidTables.Value, "Header_" + txtReportname.Text, "Footer_" + txtReportname.Text, cols, formulaname, formula, where, groupby, orderby, having, hidDetailsformat.Value, colorcondition, repType, repscope, hidDatecon.Value, createDate, createdBy, columnFormat, hidlevelsave.Value)

                            If (str = "1") Then
                                '''' fetch recordid of he latest report
                                hidRecordid.Value = insertNew.fetchReportRecordid2(Me.txtReportname.Text, dept, client, lob)
                                recordid = hidRecordid.Value
                                '''''''''''''''Usertype check for track goes here:- By Suvidha

                                Dim cmm As New SqlCommand("insert into Report_Designer_utype select MAX(newreportid)," + Session("usertype") + " from logRptDesigner where ReportName='" + Me.txtReportname.Text + "' and Action='Save'", conn)
                                conn.Open()
                                cmm.ExecuteNonQuery()
                                conn.Close()
                                '''''''''''''''Usertype check for track goes here:- By Suvidha


                                str1 = insertNew.insertHeader(txtReportname.Text, "Header_" + txtReportname.Text, height(0), Me.hidHeaderformat.Value, hidHpos.Value, hidHformat.Value, hidHformula.Value, hidHcolorcon.Value, createDate, createdBy, recordid)
                            End If
                            If (str = "1" And str1 = "1") Then
                                str2 = insertNew.insertFooter(txtReportname.Text, "Footer_" + txtReportname.Text, height(1), Me.hidFooterformat.Value, hidFpos.Value, hidFformat.Value, hidFformula.Value, hidFcolorcon.Value, createDate, createdBy, recordid)
                            End If
                            If (str = "1" And str1 = "1" And str2 = "1") Then
                                lblError.Text = "Report saved successfully."
                                Session("repName") = txtReportname.Text
                                ''''' save existing graphs on existing report
                                If Trim(precid) <> "" Then
                                    insertNew.copyGraphs(precid, Session("userid"), txtReportname.Text, dept, client, lob)
                                End If

                                '''''''''''''''''''''''''''''''''''''
                                final()
                            Else
                                lblError.Text = str + ", " + str1 + ", " + str2 '"Error Occured while saving the report. Please try again."
                                'aspnet_msgbox("Error Occured while saving the report. Please try again.")
                            End If
                        ElseIf myRight = "False" Or myRight = "" Then
                            lblError.Text = "You dont have rights to create a copy of this report."
                        End If

                    Else  ' else raise exception
                        If b <> "" And myRight = "" Then
                            lblError.Text = "Report name already exists. Please supply new report name."
                            'aspnet_msgbox("Report name already exists. Please supply new report name.")

                        End If

                    End If
                End If
            End If
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (ddlDepartment.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please select Level 1")
        End If
        connection.Close()
        connection.Open()
        'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        'connew.Open()
        cmdnew = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + ddlDepartment.SelectedValue + "'", connection)
        drnew = cmdnew.ExecuteReader()
        ddlClient.DataSource = drnew
        ddlClient.DataTextField = "ClientName"
        ddlClient.DataValueField = "autoid"
        ddlClient.DataBind()
        ddlClient.Items.Insert(0, "--Select--")
        'connew.Close()
        connection.Close()
        drnew.Close()
        cmdnew.Dispose()
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (ddlLob.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please select Level 2")
        End If
        'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        'connew.Open()
        connection.Open()
        cmdnew = New SqlCommand("select * from WARSLobMaster where deptid='" + ddlDepartment.SelectedValue + "' and  clientid= '" + ddlClient.SelectedValue + "'", connection)
        drnew = cmdnew.ExecuteReader()
        ddlLob.DataSource = drnew
        ddlLob.DataTextField = "LOBName"
        ddlLob.DataValueField = "autoid"
        ddlLob.DataBind()
        ddlLob.Items.Insert(0, "--Select--")
        'connew.Close()
        connection.Close()
        drnew.Close()
        cmdnew.Dispose()
    End Sub
End Class
