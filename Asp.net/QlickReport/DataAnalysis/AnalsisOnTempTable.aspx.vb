Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO
Partial Class DataAnalysis_AnalsisOnTempTable
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
    Dim rdr As SqlDataReader
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
    Dim dr As SqlDataReader
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Dim dept, lob, clt As String
        connection.Open()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay1.Visible = True
                Me.SAVE_multiuser.Visible = True
            Else
                Me.spandisplay1.Visible = False
                Me.SAVE_singleuser.Visible = True
            End If
        End If
        Dim tablename As String = Trim(Request("hidTables"))
        lblselect.Text = Session("tablename")
        Me.Calendar1.Visible = False
        Dim classobj As New Functions
        If result.InnerHtml = "" And report.InnerHtml = "" Then 'And relax.InnerHtml = "" Then

            'Button4.Enabled = False
        End If
        If result.InnerHtml <> "" Or report.InnerHtml <> "" Then 'Or relax.InnerHtml <> "" Then
            'Button4.Enabled = True
        End If

    End Sub
   
    Dim colarray As String()
    'Protected Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTable.SelectedIndexChanged
    '    repcols.Items.Clear()
    '    selectedcols.Items.Clear()
    '    End Sub
    
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim t1 As String = ddlTable.SelectedItem.Text
        Dim t1 As String
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
        Session("repname") = lblselect.Text
    End Sub

    Protected Sub repcols_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles repcols.SelectedIndexChanged

    End Sub
    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
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

    
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        If selectedcols.Items.Count = 0 Then
            aspnet_msgbox("No Column Is Selected")
            Exit Sub
        End If
        result.InnerHtml = ""

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

            cmd = New SqlCommand("select * from  " + Session("tablename") + "", con)
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
            cmd = New SqlCommand("select * from  " + Session("tablename") + "", con)
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
            htmlreport = htmlreport & "<caption>" & lblselect.Text
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
    
    Protected Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged



    End Sub
    Public Sub WinClose()
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")

        str.Append("window.parent.focus();")
        str.Append(" window.self.close();")
        str.Append("</Script>")
        RegisterStartupScript("WinClose", str.ToString)
    End Sub

    
    Protected Sub SAVE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SAVE_singleuser.Click

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
           If Not Directory.Exists(Server.MapPath("html/" & Session("userid"))) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("html/" & Session("userid") & "/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "html/" & Session("userid") & "/" & textreportname.Text & ".html"
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
        cmd = New SqlCommand("insert into dataanalysishtmlreport values('BMPEPL','60','Deveopment_Team','0','IDMS_Application','0',@ReportName,@SavedBy,@repname,'local')", con)
        cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
        cmd.Parameters("@ReportName").Value = textreportname.Text
        cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
        cmd.Parameters("@SavedBy").Value = Session("userid")
        cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
        cmd.Parameters("@repname").Value = lblselect.Text
        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
        aspnet_msgbox("Analysis Has Been Saved Successfully")
        divsavereport.Visible = False
        strdivreport.Value = ""
        textreportname.Text = ""
        repcols.Items.Clear()
        selectedcols.Items.Clear()

        'Changes by Mohit Tyagi on-03-Aug-2012
        Dim cmdnew = New SqlCommand("delete WARSLOBTableMaster where TableName='" + lblselect.Text + "'", con)
        con.Open()
        cmdnew.ExecuteNonQuery()
        cmdnew.Dispose()
        Dim cmdnew2 = New SqlCommand("drop table " + lblselect.Text + "", con)
        cmdnew2.ExecuteNonQuery()
        cmdnew2.Dispose()
        con.Close()
        WinClose()
    End Sub

    'Protected Sub level1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
    '    If (DepartmentName.SelectedValue = "-- Select --") Then
    '        aspnet_msgbox("Please Select Level1")
    '    End If
    '    con.Open()
    '    cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
    '    dr = cmd.ExecuteReader()
    '    ClientName.DataSource = dr
    '    ClientName.DataTextField = "ClientName"
    '    ClientName.DataValueField = "autoid"
    '    ClientName.DataBind()
    '    ClientName.Items.Insert(0, " --Select-- ")
    'End Sub

    'Protected Sub level2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
    '    If (ClientName.SelectedValue = "-- Select --") Then
    '        aspnet_msgbox("Please Select Level2")
    '    End If
    '    con.Open()
    '    cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
    '    dr = cmd.ExecuteReader()
    '    ddlLobName.DataSource = dr
    '    ddlLobName.DataTextField = "LOBName"
    '    ddlLobName.DataValueField = "autoid"
    '    ddlLobName.DataBind()
    '    ddlLobName.Items.Insert(0, "-- select --")
    'End Sub

    'Protected Sub level3_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
    '    If (ddlLobName.SelectedValue = "-- Select --") Then
    '        aspnet_msgbox("Please Select Level3")
    '    End If
    '    con.Open()
    '    cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId='" + DepartmentName.SelectedValue + "' and ClientId='" + ClientName.SelectedValue + "' and LOBId='" + ddlLobName.SelectedValue + "' and createdBy='" + Session("userid") + "' localTable='temp'", con)
    '    dr = cmd.ExecuteReader()
    '    ddlTable.DataSource = dr
    '    ddlTable.DataTextField = "TableName"
    '    ddlTable.DataValueField = "Tableid"
    '    ddlTable.DataBind()
    '    ddlTable.Items.Insert(0, "--select--")
    'End Sub

    'Protected Sub Gettable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Gettable.Click
    '    Dim cmdgettable As New SqlCommand("select Tableid,TableName from WARSLOBTableMaster where  CreatedBy ='" + Session("userid").ToString() + "' and localTable='temp'", connection)
    '    Dim dsgettable As New DataSet
    '    Dim adpgettable As New SqlDataAdapter
    '    adpgettable.SelectCommand = cmdgettable
    '    connection.Open()
    '    adpgettable.Fill(dsgettable)
    '    connection.Close()
    '    ddlTable.DataSource = dsgettable
    '    ddlTable.DataTextField = "TableName"
    '    ddlTable.DataValueField = "tableid"
    '    ddlTable.DataBind()
    '    ddlTable.Items.Insert(0, "--Select--")

    'End Sub

    Protected Sub getcols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles getcols_table.Click
        con.Close()
        con.Open()
        Dim com1 As New SqlCommand("select Visiblecolumn from WARSLOBTableMaster where  TableName='" + lblselect.Text + "' ", con)
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
        cmd.Parameters("@repname").Value = lblselect.Text
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
        textreportname.Text = ""

        Dim cmdnew = New SqlCommand("delete WARSLOBTableMaster where TableName='" + lblselect.Text + "'", con)
        con.Open()
        cmdnew.ExecuteNonQuery()
        cmdnew.Dispose()
        Dim cmdnew2 = New SqlCommand("drop table " + lblselect.Text + "", con)
        cmdnew2.ExecuteNonQuery()
        cmdnew2.Dispose()
        con.Close()
        WinClose()
    End Sub

    'Protected Sub DropDowndept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDowndept.SelectedIndexChanged
    '    If (DropDowndept.SelectedValue = "-- Select --") Then
    '        aspnet_msgbox("Please Select Level1")
    '    End If
    '    con.Open()
    '    cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DropDowndept.SelectedValue + "'", con)
    '    dr = cmd.ExecuteReader()
    '    DropDownclient.DataSource = dr
    '    DropDownclient.DataTextField = "ClientName"
    '    DropDownclient.DataValueField = "autoid"
    '    DropDownclient.DataBind()
    '    DropDownclient.Items.Insert(0, "--select--")
    'End Sub

    'Protected Sub DropDownclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownclient.SelectedIndexChanged
    '    If (DropDownclient.SelectedValue = "-- Select --") Then
    '        aspnet_msgbox("Please Select Level2")
    '    End If
    '    con.Open()
    '    cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DropDowndept.SelectedValue + "' and  clientid= '" + DropDownclient.SelectedValue + "'", con)
    '    dr = cmd.ExecuteReader()
    '    DropDownlob.DataSource = dr
    '    DropDownlob.DataTextField = "LOBName"
    '    DropDownlob.DataValueField = "autoid"
    '    DropDownlob.DataBind()
    '    DropDownlob.Items.Insert(0, "--select--")
    'End Sub

    Protected Sub DropDownlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownlob.SelectedIndexChanged
        textreportname.Enabled = True
    End Sub
End Class
