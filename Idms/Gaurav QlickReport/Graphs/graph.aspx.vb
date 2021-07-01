Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.StreamReader
Imports System.IO.StreamWriter
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.DataVisualization.Charting.SeriesChartType
Partial Class Graphs_graph
    Inherits System.Web.UI.Page

    Dim classobj As New Functions
    Dim con1 As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con1)
    Dim ds2 As New DataSet
    Dim con As New SqlConnection(con1)
    Dim com1, com As SqlCommand
    Dim da As SqlDataAdapter
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim colname, formula, groupby, formultxt, orderby, havingcondition, seriesName, sortedseries, repcolumn, columnseries, dupcol As String
    Dim cmd As SqlCommand
    Dim readquery1 As SqlDataReader
    Dim readquery As SqlDataReader
    Dim strnorcol As String
    Dim colarray() As String
    Dim s1, s2, s3 As String
    Dim x1, y1, z1
    Dim producttype As String
    Dim connection As New SqlConnection(con1)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        save_multiple.Visible = False
        save_singleuser.Visible = False
        'get_rpcolumn.Visible = False
        'get_rpcolumn_singleuser.Visible = False
        'get_tbcolumn.Visible = False
        'get_tbcolumn_singleuser.Visible = False
        If (Not Page.IsPostBack) Then
            select_chart.Items.Insert(0, "--Select--")
            select_chart.Items.Insert(1, "Point")
            select_chart.Items.Insert(2, "FastPoint")
            select_chart.Items.Insert(3, "Bubble")
            select_chart.Items.Insert(4, "Line")
            select_chart.Items.Insert(5, "Spline")
            select_chart.Items.Insert(6, "StepLine")
            select_chart.Items.Insert(7, "FastLine")
            select_chart.Items.Insert(8, "Column")
            select_chart.Items.Insert(9, "StackedColumn")
            select_chart.Items.Insert(10, "StackedColumn100")
            select_chart.Items.Insert(11, "Area")
            select_chart.Items.Insert(12, "SplineArea")
            select_chart.Items.Insert(13, "StackedArea")
            select_chart.Items.Insert(14, "StackedArea100")
            select_chart.Items.Insert(15, "Stock")
            select_chart.Items.Insert(16, "Candlestick")
            select_chart.Items.Insert(17, "Range")
            select_chart.Items.Insert(18, "SplineRange")
            select_chart.Items.Insert(19, "ErrorBar")
            select_chart.Items.Insert(20, "BoxPlot")
            select_chart.Items.Insert(21, "Pie")
            select_chart.Items.Insert(22, "Bar")
            select_chart.Items.Insert(23, "Funnel")
            select_chart.Items.Insert(24, "Pyramid")
            select_chart.Items.Insert(25, "Doughnut")
            select_chart.Items.Insert(26, "Radar")
            select_chart.Items.Insert(27, "Polar")
        End If
        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            producttype = rdr("ProductType")
            If (producttype = "Multiple User") Then
                'Me.spandisplay.Visible = True
                plot_graph.Visible = True
                plot_graph_singleuser.Visible = False
                save_multiple.Visible = True
                save_singleuser.Visible = False
                get_tables.Visible = False
                get_report.Visible = False
                show_multi.Visible = True

                opengraph_multiple.Visible = True
                opengraph_single.Visible = False
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
                    select_level1.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    Select_level2.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    select_level3.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If
            Else
                'Me.spandisplay.Visible = False
                select_level1.Visible = False
                Select_level2.Visible = False
                select_level3.Visible = False
                show_single.Visible = True
                DepartmentName.Visible = False
                ClientName.Visible = False
                ddlLobName.Visible = False
                plot_graph_singleuser.Visible = True
                plot_graph.Visible = False
                save_singleuser.Visible = True
                save_multiple.Visible = False
                opengraph_multiple.Visible = False
                opengraph_single.Visible = True
            End If
        End If
        connection.Close()
    End Sub

    'Protected Sub list_tbrt_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles list_tbrt.SelectedIndexChanged

    '    select_chart.Items.Insert(0, "-- Select--")
    '    select_chart.Items.Insert(1, "Point")
    '    select_chart.Items.Insert(2, "FastPoint")
    '    select_chart.Items.Insert(3, "Bubble")
    '    select_chart.Items.Insert(4, "Line")
    '    select_chart.Items.Insert(5, "Spline")
    '    select_chart.Items.Insert(6, "StepLine")
    '    select_chart.Items.Insert(7, "FastLine")
    '    'select_chart.Items.Insert(8, "Bar")
    '    'select_chart.Items.Insert(9, "StackedBar")
    '    'select_chart.Items.Insert(10, "StackedBar100")
    '    select_chart.Items.Insert(8, "Column")
    '    select_chart.Items.Insert(9, "StackedColumn")
    '    select_chart.Items.Insert(10, "StackedColumn100")
    '    select_chart.Items.Insert(11, "Area")
    '    select_chart.Items.Insert(12, "SplineArea")
    '    select_chart.Items.Insert(13, "StackedArea")
    '    select_chart.Items.Insert(14, "StackedArea100")
    '    'select_chart.Items.Insert(18, "Pie")
    '    'select_chart.Items.Insert(19, "Doughnut")
    '    select_chart.Items.Insert(15, "Stock")
    '    select_chart.Items.Insert(16, "Candlestick")
    '    select_chart.Items.Insert(17, "Range")
    '    select_chart.Items.Insert(18, "SplineRange")
    '    'select_chart.Items.Insert(24, "RangeBar")
    '    'select_chart.Items.Insert(25, "RangeColumn")
    '    'select_chart.Items.Insert(26, "Radar")
    '    'select_chart.Items.Insert(27, "Polar")
    '    select_chart.Items.Insert(19, "ErrorBar")
    '    select_chart.Items.Insert(20, "BoxPlot")
    '    'select_chart.Items.Insert(30, "Renko")
    '    'select_chart.Items.Insert(31, "ThreeLineBreak")
    '    'select_chart.Items.Insert(32, "Kagi")
    '    'select_chart.Items.Insert(33, "PointAndFigure")
    '    'select_chart.Items.Insert(34, "Funnel")
    '    'select_chart.Items.Insert(35, "Pyramid")

    '    sltchart.Visible = False
    '    xaxis.Visible = False
    '    yaxis1.Visible = False
    '    yaxis2.Visible = False
    '    select_chart.Visible = False
    '    xaxis_select.Visible = False
    '    yaxix1_select.Visible = False
    '    yaxix2_select.Visible = False
    '    repcols.Items.Clear()
    '    selectedcols.Items.Clear()
    '    xaxis_select.Items.Clear()
    '    yaxix1_select.Items.Clear()
    '    yaxix2_select.Items.Clear()
    '    If (list_tbrt.SelectedItem.Text.Equals("Tables")) Then
    '        get_tables.Visible = True
    '        get_report.Visible = False
    '        selected_radio.Visible = True
    '        selected_radio.Text = "Select Table"
    '        ClientName.Items.Clear()
    '        ddlLobName.Items.Clear()
    '        ddlTable.Items.Clear()
    '    Else
    '        get_report.Visible = True
    '        get_tables.Visible = False
    '        selected_radio.Visible = True
    '        selected_radio.Text = "Select Report"
    '        ClientName.Items.Clear()
    '        ddlLobName.Items.Clear()
    '        ddlTable.Items.Clear()
    '        plot_graph_singleuser.Visible = False
    '    End If
    '    connection.Close()
    '    If (producttype.Equals("Multiple User") And list_tbrt.SelectedItem.Text.Equals("Tables")) Then
    '        get_report.Visible = False
    '        get_tables.Visible = False
    '    ElseIf (producttype.Equals("Multiple User") And list_tbrt.SelectedItem.Text.Equals("Report")) Then
    '        get_report.Visible = False
    '        get_tables.Visible = False
    '    End If

    '    Dim typeofuser = Session("typeofuser")
    '    If (typeofuser.Equals("Super Admin")) Then
    '        Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
    '        connection.Open()
    '        DepartmentName.DataSource = cmd.ExecuteReader()
    '        DepartmentName.DataTextField = "DepartmentName"
    '        DepartmentName.DataValueField = "AutoID"
    '        DepartmentName.Items.Insert(0, "-- Select--")
    '        DepartmentName.DataBind()
    '    ElseIf (typeofuser.Equals("User")) Then
    '        Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
    '        con.Open()
    '        DepartmentName.DataSource = cmd.ExecuteReader()
    '        DepartmentName.DataTextField = "DepartmentName"
    '        DepartmentName.DataValueField = "AutoID"
    '        DepartmentName.Items.Insert(0, "--Select--")
    '        DepartmentName.DataBind()
    '    ElseIf (typeofuser.Equals("Admin")) Then
    '        Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
    '        con.Open()
    '        DepartmentName.DataSource = cmd.ExecuteReader()
    '        DepartmentName.DataTextField = "DepartmentName"
    '        DepartmentName.DataValueField = "AutoID"
    '        DepartmentName.Items.Insert(0, "--Select--")
    '        DepartmentName.DataBind()
    '    End If
    '    DepartmentName.Items.Insert(0, "--select--")
    'End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        con.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ClientName.DataSource = dr
        ClientName.DataTextField = "ClientName"
        ClientName.DataValueField = "autoid"
        ClientName.DataBind()
        ClientName.Items.Insert(0, "--Select--")
        If (rbreport.Checked()) Then
            get_rpt(DepartmentName.SelectedValue, 0, 0)
        Else
            get_table(DepartmentName.SelectedValue, 0, 0)
        End If
    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If (ClientName.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please Select ClientName")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ddlLobName.DataSource = dr
        ddlLobName.DataTextField = "LOBName"
        ddlLobName.DataValueField = "autoid"
        ddlLobName.DataBind()
        ddlLobName.Items.Insert(0, "--Select--")
        If (rbreport.Checked()) Then
            get_rpt(DepartmentName.SelectedValue, ClientName.SelectedValue, 0)
        Else
            get_table(DepartmentName.SelectedValue, ClientName.SelectedValue, 0)
        End If
    End Sub

    Protected Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        If (rbreport.Checked()) Then
            get_rpt(DepartmentName.SelectedValue, ClientName.SelectedValue, ddlLobName.SelectedValue)
        Else
            get_table(DepartmentName.SelectedValue, ClientName.SelectedValue, ddlLobName.SelectedValue)
        End If
        'ElseIf (rbreport.Checked()) Then
         
        'End If
    End Sub
    Public Function get_table(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        'If (rbtable.Checked()) Then
        con.Close()
        con.Open()
        cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId='" + deptid + "' and ClientId='" + clientid + "' and LOBId='" + lobid + "' and createdBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "Tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--Select--")
        get_tbcolumn.Visible = True
        get_rpcolumn.Visible = False
        get_tbcolumn_singleuser.Visible = False
        get_rpcolumn_singleuser.Visible = False

        'End If
    End Function
    Public Function get_rpt(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        con.Close()
        con.Open()
        cmd = New SqlCommand("select * from IDMSReportMaster where DepartmentId='" + deptid + "' and ClientId='" + clientid + "' and UnderLOB='" + lobid + "' and SavedBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "QueryName"
        ddlTable.DataValueField = "Recordid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--Select--")
        get_tbcolumn.Visible = False
        get_tbcolumn_singleuser.Visible = False
        get_rpcolumn_singleuser.Visible = False
        get_rpcolumn.Visible = True
    End Function
    Protected Sub ddlTable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTable.SelectedIndexChanged
        xaxis_select.Items.Clear()
        yaxix1_select.Items.Clear()
        yaxix2_select.Items.Clear()
        repcols.Items.Clear()
        selectedcols.Items.Clear()

        'If (list_tbrt.SelectedItem.Text.Equals("Tables")) Then
        '    get_tbcolumn.Visible = True
        'Else
        '    get_rpcolumn.Visible = True
        'End If
    End Sub

    Protected Sub get_tbcolumn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_tbcolumn.Click
        If (ddlTable.SelectedItem.Text = "--select--") Then
            aspnet_msgbox("Please Select Table or Report First")
            Exit Sub
        End If
        con.Close()
        con.Open()
        Dim com1 As New SqlCommand("select Visiblecolumn from WARSLOBTableMaster where TableName='" + ddlTable.SelectedItem.Text + "'", con)
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

    Protected Sub get_rpcolumn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_rpcolumn.Click
        If (ddlTable.SelectedItem.Text = "--select--") Then
            aspnet_msgbox("Please Select Table or Report First")
            Exit Sub
        End If
        Dim dept, client, lob As String
        dept = DepartmentName.SelectedValue
        client = ClientName.SelectedValue
        lob = ddlLobName.SelectedValue

        Dim dept_id, client_id, lobid As String
        If Me.DepartmentName.SelectedIndex > 0 Then
            If Me.ClientName.SelectedIndex > 0 Then
                client_id = Convert.ToInt32(ClientName.SelectedValue)
            Else
                client_id = "0"
            End If
            dept_id = DepartmentName.SelectedValue.ToString()
            If Me.ddlLobName.SelectedIndex > 0 Then
                lobid = Convert.ToInt32(ddlLobName.SelectedValue)
            Else
                lobid = "0"
            End If
        End If
        gridreportbind()
    End Sub
    Public Sub gridreportbind()
        Session("hidwheretxt") = ""
        Session("hidordertxt") = ""
        Session("hidgroupby") = ""
        Session("hidhavingtxt") = ""
        Dim columnname, cname, tabcolumn, reportDept, reportclient, reportlob, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, havingtxt, totalquerycloumn, strspace, strDollar, strcomma, txtformula1 As String
        Dim aa1
        Dim b As Boolean
        Dim da As New SqlDataAdapter
        Dim da1 As New SqlDataAdapter

        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim colfinal As Integer
        Dim ttcount, cccount, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer

        Dim final
        Dim repcolarray
        Dim data As SqlDataReader
        Dim tablearray
        Dim colarry
        'Dim formulaarray
        Dim colsss
        Dim tablename, strformulaname, strfinalfor, strcol, strfcol As String
        Dim colsa As DataColumn
        Dim clientAna, lobana As String
        clientAna = ClientName.SelectedValue
        If clientAna = "" Or clientAna = "--Select--" Then
            clientAna = "0"
        Else
            clientAna = ClientName.SelectedValue
        End If
        lobana = ddlLobName.SelectedValue
        If lobana = "" Or lobana = "--Select--" Then
            lobana = "0"
        Else
            lobana = ddlLobName.SelectedValue
        End If
        com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "'", con)

        con.Open()
        readquery = com1.ExecuteReader
        While readquery.Read()
            tablename = readquery("tablename")
        End While
        tablename = tablename.Replace("~", ",")
        tablearray = tablename.Split(",")
        com1.Dispose()


        tabcount = UBound(tablearray)
        con.Close()
        readquery.Close()
        If ddlTable.SelectedItem.Text = "" Then
            com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "' and  departmentid='" + DepartmentName.SelectedValue + "' and  clientid='" + clientAna + "' and  underlob='" + lobana + "'", con)
        Else
            com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "' and  departmentid='" + DepartmentName.SelectedValue + "' and  clientid='" + clientAna + "' and  underlob='" + lobana + "'", con)

        End If
        con.Open()
        readquery = com.ExecuteReader
        While readquery.Read()
            colname = readquery("colname")
            colname = Replace(colname, vbNewLine, "")
            '''''''' updated on 19/10/08
            Dim bnm = Replace(colname, " As ", " AS ")
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
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''updatation ends'''''''''''''''''
            colarry = bnm.Split("~")
            colname = bnm
            Dim jk As Integer
            For jk = 0 To colarry.length - 1
                strformulaname = colarry(jk)
                If strformulaname.Contains("+") Or strformulaname.Contains("-") Or strformulaname.Contains("(") Or strformulaname.Contains(")") Or strformulaname.Contains("*") Or strformulaname.Contains("/") Then
                    If strfinalfor = "" Then
                        strfinalfor = strformulaname
                    Else
                        strfinalfor = strfinalfor + ";" + strformulaname
                    End If
                Else
                    If strcol = "" Then
                        strcol = strformulaname
                    Else
                        strcol = strcol + ";" + strformulaname
                    End If

                End If
            Next
            If strfinalfor = "" Then
                strfcol = strcol
            Else
                strfcol = strcol + ";" + strfinalfor
            End If
            strnorcol = strcol + ";" + strfinalfor
            strnorcol = strnorcol.Replace("$", ".")
            strnorcol = strnorcol.Replace("AS", "%")
            Dim datestr As String
            If strfcol <> "" Then
                strfcol = strfcol.Replace("$", ".")
                strfcol = strfcol.Replace("AS", "%")
                Dim datestr1 As String
                datestr1 = strfcol
            End If
            '
            If colname = "" Then
                aspnet_msgbox("This Report Is Empty")
                Exit Sub
            End If
            strcomma = colname.Replace("~", ",")
            strspace = strcomma.Replace("$", ".")
            Dim Date1 As Boolean = False
            Dim Date2 As Boolean = False
            Date1 = strspace.Contains("@Date1@")
            Date2 = strspace.Contains("@Date2@")
            colname = strspace
            Session("totalcolumn") = strcomma
            If IsDBNull(readquery("wheredata")) Then
            Else
                formula = readquery("wheredata")
            End If

            If IsDBNull(readquery("groupby")) Then
            Else
                groupby = readquery("groupby")
            End If
            If IsDBNull(readquery("orderby")) Then
            Else
                orderby = readquery("orderby")
            End If
            If IsDBNull(readquery("havingcondition")) Then
            Else
                havingcondition = readquery("havingcondition")
            End If

        End While

        wheretxt = ""
        Dim astr = colname + formula + groupby + orderby
        Dim Date11 As Boolean = False
        Dim Date22 As Boolean = False


        If groupby <> "" Then
            groupbytext = "group by" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")

            Session("hidgroupby") = groupbytext

        Else
            groupbytext = ""
            Session("hidgroupby") = groupbytext
        End If
        If orderby <> "" Then
            orderbytext = "order by" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            Session("hidordertxt") = orderbytext

        Else
            orderbytext = ""
            Session("hidordertxt") = orderbytext
        End If
        If havingcondition <> "" Then
            havingtxt = "having" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            Session("hidhavingtxt") = havingtxt
        Else
            havingtxt = ""
            Session("hidhavingtxt") = havingtxt
        End If
        con.Close()
        readquery.Close()
        If ddlTable.SelectedItem.Text <> "" Then
            com1 = New SqlCommand("select DepartmentId,clientid,underlob from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "'", con)
            con.Open()
            readquery = com1.ExecuteReader
            While readquery.Read()
                reportDept = readquery("DepartmentId")
                reportclient = readquery("clientid")
                reportlob = readquery("underlob")
            End While
            com1.Dispose()
            con.Close()
            readquery.Close()
        End If


        Dim colarray
        colarray = colname.Split("~")
        Dim colcount As Integer
        colcount = UBound(colarray)
        For allcol = 0 To colcount
            If columnname = "" Then
                columnname = colarray(allcol)
            Else
                columnname = columnname & "," & colarray(allcol)

            End If

        Next
        Session("columnname") = columnname
        '----------------------------------
        Dim asasasa As String = ""
        If asasasa <> "" Then



            For alltabcol = 0 To colcount
                tabcolumn = colarray(alltabcol)
                final = tabcolumn.Split(".")
                tabcolength = UBound(final)
                For q = 0 To tabcount
                    For r = 0 To tabcolength
                        Dim p As String
                        p = final(tabcolength)
                        If final(r) = tablearray(q) Then
                            If tname = "" Then
                                tname = final(r)
                                cname = final(r + 1)
                            Else
                                tname = final(r) & "," & tname
                                cname = cname & "," & final(r + 1)
                            End If
                        End If
                    Next
                Next
            Next
        End If
        '------------------------
        Dim repName As String = ""
        com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        data = com1.ExecuteReader
        While data.Read()
            If ddlTable.SelectedItem.Text = "" Then
                repName = ddlTable.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlTable.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            Else
                repName = ddlTable.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlTable.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            End If


        End While
        com1.Dispose()
        con.Close()
        If b = False Then
            Dim asstr = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
            com1 = New SqlCommand(asstr, con)
            con.Open()
            da1.SelectCommand = com1
            da1.Fill(ds2)
            For Each reportcolumn In ds2.Tables(0).Columns
                If repcolumn = "" Then
                    If reportcolumn.ColumnName() <> "RecordId" Then
                        repcolumn = reportcolumn.ColumnName()
                    End If
                Else
                    If reportcolumn.ColumnName() <> "RecordId" Then
                        repcolumn = repcolumn & "," & reportcolumn.ColumnName()
                    End If
                End If
            Next
            repcolarray = repcolumn.Split(",")
            repcols.DataSource = repcolarray
            repcols.DataBind()
            con.Close()
        ElseIf b = True Then
            If cname <> "" Then
                aa1 = cname.Split(",")

                ttcount = UBound(tablearray)
                cccount = UBound(aa1)
                For po = 0 To ttcount

                    Try
                        currenttable = CType(tablearray(po), String)
                        da = New SqlDataAdapter("select * from " + currenttable + "", con)
                        con.Open()
                        da.Fill(ds)
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
                        Exit Sub
                        Exit For
                    End Try

                    For g2 = 0 To cccount
                        currentcolumn = CType(aa1(g2), String)
                        For Each colsa In ds.Tables(0).Columns
                            If colsa.ColumnName = currentcolumn Then
                                If totalquerycloumn = "" Then
                                    totalquerycloumn = currentcolumn
                                Else
                                    totalquerycloumn = totalquerycloumn & "," & currentcolumn
                                End If
                            End If
                        Next
                    Next
                    ds.Tables(0).Columns.Clear()
                    da.Dispose()
                Next
                con.Close()
                If totalquerycloumn <> "" Then
                    colsss = totalquerycloumn.Split(",")
                    colfinal = UBound(colsss)
                End If
            End If
            Try

                Dim asstring = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
                cmd = New SqlCommand(asstring, con)
                con.Open()
                da1.SelectCommand = cmd
                da1.Fill(ds2)
                For Each reportcolumn In ds2.Tables(0).Columns
                    If repcolumn = "" Then
                        If reportcolumn.ColumnName() <> "RecordId" Then
                            repcolumn = reportcolumn.ColumnName()
                        End If
                    Else
                        If reportcolumn.ColumnName() <> "RecordId" Then
                            repcolumn = repcolumn & "," & reportcolumn.ColumnName()
                        End If
                    End If
                Next
                repcolarray = repcolumn.Split(",")
                repcols.DataSource = repcolarray
                repcols.DataBind()
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                Exit Sub
            End Try
            con.Close()
            con.Close()
        End If
        '   If ddlDepartmant.SelectedItem.Text <> "---select---" Then
        ' gridstring = "select " + hidcolumname.Value + " from " + hidtablename.Value + " " + hidwheretxt.Value + " " + hidgroup.Value + " " + hidhaving.Value + " " + hidorder.Value + " "
        ' gridstring = Replace(gridstring, "@Date1@", txtFromdate.Text)
        ' gridstring = Replace(gridstring, "@Date2@", txtTodate.Text)
        ' Session("reportquery") = gridstring
        ' Dim strClose As String = ""
        ' strClose = "<Script language='Javascript'>"
        ' strClose = strClose + "gridshow();"
        ' strClose = strClose + "</Script>"
        ' ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", strClose)
        '  End If
    End Sub
    Public Sub gridreportbind1()
        Session("hidwheretxt") = ""
        Session("hidordertxt") = ""
        Session("hidgroupby") = ""
        Session("hidhavingtxt") = ""
        Dim columnname, cname, tabcolumn, reportDept, reportclient, reportlob, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, havingtxt, totalquerycloumn, strspace, strDollar, strcomma, txtformula1 As String
        Dim aa1
        Dim b As Boolean
        Dim da As New SqlDataAdapter
        Dim da1 As New SqlDataAdapter

        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim colfinal As Integer
        Dim ttcount, cccount, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer

        Dim final
        Dim repcolarray
        Dim data As SqlDataReader
        Dim tablearray
        Dim colarry
        'Dim formulaarray
        Dim colsss
        Dim tablename, strformulaname, strfinalfor, strcol, strfcol As String
        Dim colsa As DataColumn
        Dim clientAna, lobana As String
        clientAna = 0
        'If clientAna = "" Or clientAna = "--Select--" Then
        '    clientAna = "0"
        'Else
        '    clientAna = ClientName.SelectedValue
        'End If
        'lobana = ddlLobName.SelectedValue
        'If lobana = "" Or lobana = "--Select--" Then
        '    lobana = "0"
        'Else
        lobana = 0
        'End If
        com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "'", con)

        con.Open()
        readquery = com1.ExecuteReader
        While readquery.Read()
            tablename = readquery("tablename")
        End While
        tablename = tablename.Replace("~", ",")
        tablearray = tablename.Split(",")
        com1.Dispose()


        tabcount = UBound(tablearray)
        con.Close()
        readquery.Close()
        If ddlTable.SelectedItem.Text = "" Then
            com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "' and  departmentid='60' and  clientid='" + clientAna + "' and  underlob='" + lobana + "'", con)
        Else
            com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "' and  departmentid='60' and  clientid='0' and  underlob='0'", con)

        End If
        con.Open()
        readquery = com.ExecuteReader
        While readquery.Read()
            colname = readquery("colname")
            colname = Replace(colname, vbNewLine, "")
            '''''''' updated on 19/10/08
            Dim bnm = Replace(colname, " As ", " AS ")
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
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            '''''''''''''''''''''''''''''updatation ends'''''''''''''''''
            colarry = bnm.Split("~")
            colname = bnm
            Dim jk As Integer
            For jk = 0 To colarry.length - 1
                strformulaname = colarry(jk)
                If strformulaname.Contains("+") Or strformulaname.Contains("-") Or strformulaname.Contains("(") Or strformulaname.Contains(")") Or strformulaname.Contains("*") Or strformulaname.Contains("/") Then
                    If strfinalfor = "" Then
                        strfinalfor = strformulaname
                    Else
                        strfinalfor = strfinalfor + ";" + strformulaname
                    End If
                Else
                    If strcol = "" Then
                        strcol = strformulaname
                    Else
                        strcol = strcol + ";" + strformulaname
                    End If

                End If
            Next
            If strfinalfor = "" Then
                strfcol = strcol
            Else
                strfcol = strcol + ";" + strfinalfor
            End If
            strnorcol = strcol + ";" + strfinalfor
            strnorcol = strnorcol.Replace("$", ".")
            strnorcol = strnorcol.Replace("AS", "%")
            Dim datestr As String
            If strfcol <> "" Then
                strfcol = strfcol.Replace("$", ".")
                strfcol = strfcol.Replace("AS", "%")
                Dim datestr1 As String
                datestr1 = strfcol
            End If
            '
            If colname = "" Then
                aspnet_msgbox("This Report Is Empty")
                Exit Sub
            End If
            strcomma = colname.Replace("~", ",")
            strspace = strcomma.Replace("$", ".")
            Dim Date1 As Boolean = False
            Dim Date2 As Boolean = False
            Date1 = strspace.Contains("@Date1@")
            Date2 = strspace.Contains("@Date2@")
            colname = strspace
            Session("totalcolumn") = strcomma
            If IsDBNull(readquery("wheredata")) Then
            Else
                formula = readquery("wheredata")
            End If

            If IsDBNull(readquery("groupby")) Then
            Else
                groupby = readquery("groupby")
            End If
            If IsDBNull(readquery("orderby")) Then
            Else
                orderby = readquery("orderby")
            End If
            If IsDBNull(readquery("havingcondition")) Then
            Else
                havingcondition = readquery("havingcondition")
            End If

        End While

        wheretxt = ""
        Dim astr = colname + formula + groupby + orderby
        Dim Date11 As Boolean = False
        Dim Date22 As Boolean = False


        If groupby <> "" Then
            groupbytext = "group by" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")

            Session("hidgroupby") = groupbytext

        Else
            groupbytext = ""
            Session("hidgroupby") = groupbytext
        End If
        If orderby <> "" Then
            orderbytext = "order by" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            Session("hidordertxt") = orderbytext

        Else
            orderbytext = ""
            Session("hidordertxt") = orderbytext
        End If
        If havingcondition <> "" Then
            havingtxt = "having" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            Session("hidhavingtxt") = havingtxt
        Else
            havingtxt = ""
            Session("hidhavingtxt") = havingtxt
        End If
        con.Close()
        readquery.Close()
        If ddlTable.SelectedItem.Text <> "" Then
            com1 = New SqlCommand("select DepartmentId,clientid,underlob from idmsreportmaster where queryname='" + ddlTable.SelectedItem.Text + "'", con)
            con.Open()
            readquery = com1.ExecuteReader
            While readquery.Read()
                reportDept = readquery("DepartmentId")
                reportclient = readquery("clientid")
                reportlob = readquery("underlob")
            End While
            com1.Dispose()
            con.Close()
            readquery.Close()
        End If


        Dim colarray
        colarray = colname.Split("~")
        Dim colcount As Integer
        colcount = UBound(colarray)
        For allcol = 0 To colcount
            If columnname = "" Then
                columnname = colarray(allcol)
            Else
                columnname = columnname & "," & colarray(allcol)

            End If

        Next
        Session("columnname") = columnname
        '----------------------------------
        Dim asasasa As String = ""
        If asasasa <> "" Then



            For alltabcol = 0 To colcount
                tabcolumn = colarray(alltabcol)
                final = tabcolumn.Split(".")
                tabcolength = UBound(final)
                For q = 0 To tabcount
                    For r = 0 To tabcolength
                        Dim p As String
                        p = final(tabcolength)
                        If final(r) = tablearray(q) Then
                            If tname = "" Then
                                tname = final(r)
                                cname = final(r + 1)
                            Else
                                tname = final(r) & "," & tname
                                cname = cname & "," & final(r + 1)
                            End If
                        End If
                    Next
                Next
            Next
        End If
        '------------------------
        Dim repName As String = ""
        com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        data = com1.ExecuteReader
        While data.Read()
            If ddlTable.SelectedItem.Text = "" Then
                repName = ddlTable.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlTable.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            Else
                repName = ddlTable.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlTable.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            End If


        End While
        com1.Dispose()
        con.Close()
        If b = False Then
            Dim asstr = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
            com1 = New SqlCommand(asstr, con)
            con.Open()
            da1.SelectCommand = com1
            da1.Fill(ds2)
            For Each reportcolumn In ds2.Tables(0).Columns
                If repcolumn = "" Then
                    If reportcolumn.ColumnName() <> "RecordId" Then
                        repcolumn = reportcolumn.ColumnName()
                    End If
                Else
                    If reportcolumn.ColumnName() <> "RecordId" Then
                        repcolumn = repcolumn & "," & reportcolumn.ColumnName()
                    End If
                End If
            Next
            repcolarray = repcolumn.Split(",")
            repcols.DataSource = repcolarray
            repcols.DataBind()
            con.Close()
        ElseIf b = True Then
            If cname <> "" Then
                aa1 = cname.Split(",")

                ttcount = UBound(tablearray)
                cccount = UBound(aa1)
                For po = 0 To ttcount

                    Try
                        currenttable = CType(tablearray(po), String)
                        da = New SqlDataAdapter("select * from " + currenttable + "", con)
                        con.Open()
                        da.Fill(ds)
                        con.Close()
                    Catch ex As Exception

                        aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
                        Exit Sub
                        Exit For
                    End Try

                    For g2 = 0 To cccount
                        currentcolumn = CType(aa1(g2), String)
                        For Each colsa In ds.Tables(0).Columns
                            If colsa.ColumnName = currentcolumn Then
                                If totalquerycloumn = "" Then
                                    totalquerycloumn = currentcolumn
                                Else
                                    totalquerycloumn = totalquerycloumn & "," & currentcolumn
                                End If
                            End If
                        Next
                    Next
                    ds.Tables(0).Columns.Clear()
                    da.Dispose()
                Next
                con.Close()
                If totalquerycloumn <> "" Then
                    colsss = totalquerycloumn.Split(",")
                    colfinal = UBound(colsss)
                End If
            End If
            Try

                Dim asstring = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
                cmd = New SqlCommand(asstring, con)
                con.Open()
                da1.SelectCommand = cmd
                da1.Fill(ds2)
                For Each reportcolumn In ds2.Tables(0).Columns
                    If repcolumn = "" Then
                        If reportcolumn.ColumnName() <> "RecordId" Then
                            repcolumn = reportcolumn.ColumnName()
                        End If
                    Else
                        If reportcolumn.ColumnName() <> "RecordId" Then
                            repcolumn = repcolumn & "," & reportcolumn.ColumnName()
                        End If
                    End If
                Next
                repcolarray = repcolumn.Split(",")
                repcols.DataSource = repcolarray
                repcols.DataBind()
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                Exit Sub
            End Try
            con.Close()
            con.Close()
        End If
        '   If ddlDepartmant.SelectedItem.Text <> "---select---" Then
        ' gridstring = "select " + hidcolumname.Value + " from " + hidtablename.Value + " " + hidwheretxt.Value + " " + hidgroup.Value + " " + hidhaving.Value + " " + hidorder.Value + " "
        ' gridstring = Replace(gridstring, "@Date1@", txtFromdate.Text)
        ' gridstring = Replace(gridstring, "@Date2@", txtTodate.Text)
        ' Session("reportquery") = gridstring
        ' Dim strClose As String = ""
        ' strClose = "<Script language='Javascript'>"
        ' strClose = strClose + "gridshow();"
        ' strClose = strClose + "</Script>"
        ' ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", strClose)
        '  End If
    End Sub
    Public stt As String
    Private p As Object
    Protected Sub add_clmn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add_clmn.Click
        Dim stre As String = repcols.Items.Count()
        Dim i As String
        Dim iloopindex As Integer
        Dim co As Boolean
        co = False
        'Dim selectedarr
        If repcols.SelectedIndex = -1 Then
            aspnet_msgbox("Select Column First")
            Exit Sub
        Else
            If selectedcols.Items.Count = 0 Then
                For iloopindex = 0 To repcols.Items.Count - 1
                    If repcols.Items(iloopindex).Selected = True Then
                        i = repcols.Items(iloopindex).Text
                        selectedcols.Items.Add(i)
                    End If
                Next
                For j = 0 To selectedcols.Items.Count - 1
                    If stt = "" Then
                        stt = selectedcols.Items(j).Text
                    Else
                        stt = stt & "," & selectedcols.Items(j).Text
                        'hid.Value = stt
                    End If
                Next
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
                        Else
                            stt = stt & "," & selectedcols.Items(j).Text
                            'hid.Value = stt
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub remove_cols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles remove_cols.Click
        If selectedcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        End If
        Dim iloopindexrem, w As Integer
        For iloopindexrem = 0 To selectedcols.Items.Count - 1
            selectedcols.Items.Remove(selectedcols.SelectedItem)
        Next
        For j = 0 To selectedcols.Items.Count - 1
            If stt = "" Then
                stt = selectedcols.Items(j).Text
            Else
                stt = stt & selectedcols.Items(j).Text & ","
            End If
        Next
        If selectedcols.Items.Count = 0 Then
        Else
            p = stt.Split(",")
            Session("colsvalue") = p
        End If
    End Sub

    Protected Sub plot_graph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plot_graph.Click
        If (rbreport.Checked = False And rbtable.Checked = False) Then
            aspnet_msgbox("Please Select Atleast One Radio Button")
            Exit Sub
        End If
        If (DepartmentName.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select " + select_level1.Text + " First ")
            Exit Sub
        End If

        If (ddlTable.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Table Or Report First")
            Exit Sub
        End If
        save_multiple.Visible = True
        chart_data1.Visible = True
        chart_data.Visible = True
        Dim a1, a2, k As Integer
        Dim repcol As String = ""
        Dim sltcol As String = ""
        a1 = repcols.Items.Count
        a2 = selectedcols.Items.Count
        For k = 0 To a1 - 1
            If (repcol.Equals("")) Then
                repcol = repcols.Items(k).Text
            Else
                repcol = repcol + "," + repcols.Items(k).Text
            End If

        Next
        Session("repcols") = repcol
        For k = 0 To a2 - 1
            If (sltcol.Equals("")) Then
                sltcol = selectedcols.Items(k).Text
            Else
                sltcol = sltcol + "," + selectedcols.Items(k).Text
            End If

        Next
        Session("selectcols") = sltcol
        Session("deptid") = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            Session("clientid") = 0
        Else
            Session("clientid") = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            Session("lobid") = 0
        Else
            Session("lobid") = ddlLobName.SelectedValue
        End If
        Session("gtype") = "Column"
        If (rbreport.Checked()) Then
            Session("repname") = ddlTable.SelectedItem.Text
        ElseIf (rbtable.Checked()) Then
            Session("repname") = "table_" + ddlTable.SelectedItem.Text
        End If



        'If (select_chart.SelectedItem.Text.Equals("--Select--")) Then
        '    slt_chart.Text = "Column Chart"
        'Else
        '    slt_chart.Text = select_chart.SelectedItem.Text + " Chart"
        'End If
        'slt_chart.Visible = True
        If (selectedcols.Items.Count < 2) Then
            aspnet_msgbox("Minimum 2 Columns are Allowed To Make a Graph")
        End If
        xaxis_select.Items.Insert(0, "--Select--")
        yaxix1_select.Items.Insert(0, "--Select--")
        yaxix2_select.Items.Insert(0, "--Select--")
        Dim rep_name = ddlTable.SelectedItem.Text
        If (selectedcols.Items.Count > 3) Then
            aspnet_msgbox("Maximum 3 Columns are Allowed To Make a Graph")
            Exit Sub
        End If
        Dim i As Integer = selectedcols.Items.Count
        Dim cmd11, cmd22, cmd33 As SqlCommand
        con.Close()
        con.Open()
        If (i.Equals(3)) Then
            s1 = selectedcols.Items(i - 3).Text
            s2 = selectedcols.Items(i - 2).Text
            s3 = selectedcols.Items(i - 1).Text
        ElseIf (i.Equals(2)) Then
            s1 = selectedcols.Items(i - 2).Text
            s2 = selectedcols.Items(i - 1).Text
        End If
        Dim cmd1 As SqlCommand
        Dim name As String
        Dim c1, c2, c3 As String
        If (rbtable.Checked()) Then
            cmd1 = New SqlCommand("Select * from " + rep_name + "", con)
            Dim y As Integer = 1
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s1)
                    yaxix2_select.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s2)
                    yaxix2_select.Items.Insert(y, s2)
                    y = y + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s3)
                    yaxix2_select.Items.Insert(y, s3)
                    y = y + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s1)
                    yaxix2_select.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd11.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s2)
                    yaxix2_select.Items.Insert(y, s2)
                    y = y + 1
                End If

            End If
            con.Close()

        ElseIf (rbreport.Checked()) Then
            con.Close()
            con.Open()
            Dim x As Integer = 1
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + rep_name + "'", con)
            name = (cmd.ExecuteScalar()).ToString()
            cmd1 = New SqlCommand("Select * from  " + name + "", con)
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s1)
                    yaxix2_select.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s2)
                    yaxix2_select.Items.Insert(x, s2)
                    x = x + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s3)
                    yaxix2_select.Items.Insert(x, s3)
                    x = x + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s1)
                    yaxix2_select.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s2)
                    yaxix2_select.Items.Insert(x, s2)
                    x = x + 1
                End If
            End If

        End If
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)
        'If (select_chart.SelectedItem.Text.Equals("-- Select--")) Then
        chart_data.Series(0).ChartType = Column
        If (i.Equals(3)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(s1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(s2).ToString()
            chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(s3).ToString()
        ElseIf (i.Equals(2)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(s1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(s2).ToString()
        End If
        'End If
        chart_data.DataSource = ds1
        chart_data.DataBind()
        If (i.Equals(3)) Then
            sltchart.Visible = True
            xaxis.Visible = True
            yaxis1.Visible = True
            yaxis2.Visible = True
            select_chart.Visible = True
            xaxis_select.Visible = True
            yaxix1_select.Visible = True
            yaxix2_select.Visible = True
        ElseIf (i.Equals(2)) Then
            sltchart.Visible = True
            xaxis.Visible = True
            yaxis1.Visible = True
            yaxis1.Text = "Y Axis Member"
            select_chart.Visible = True
            xaxis_select.Visible = True
            yaxix1_select.Visible = True
        End If
        If (i.Equals(3)) Then
            xaxis_select.Items.Insert(1, s1)
            xaxis_select.Items.Insert(2, s2)
            xaxis_select.Items.Insert(3, s3)
        ElseIf (i.Equals(2)) Then
            xaxis_select.Items.Insert(1, s1)
            xaxis_select.Items.Insert(2, s2)
        End If
        plt_gph.Visible = True
        plot_graph.Visible = False
    End Sub
    Protected Sub plt_gph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plt_gph.Click
        If (select_chart.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Chart Type")
            Exit Sub
        End If
        If (xaxis_select.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select X axis Member")
            Exit Sub
        End If
        If (yaxix1_select.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Y axis Member")
            Exit Sub
        End If
        If (selectedcols.Items.Count.Equals(3)) Then
            If (yaxix2_select.SelectedItem.Text = "--Select--") Then
                aspnet_msgbox("Please Select Y axis IInd Member")
                Exit Sub
            End If
        End If
        Dim name1 As String
        Dim rep_name As String = ddlTable.SelectedItem.Text
        Dim cmd11 As SqlCommand
        x1 = xaxis_select.SelectedItem.Text
        y1 = yaxix1_select.SelectedItem.Text
        z1 = yaxix2_select.SelectedItem.Text
        If (rbtable.Checked()) Then
            cmd11 = New SqlCommand("Select * from " + rep_name + "", con)
            con.Close()
        ElseIf (rbreport.Checked()) Then
            con.Close()
            con.Open()
            Dim x As Integer = 1
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + rep_name + "'", con)
            name1 = (cmd.ExecuteScalar()).ToString()
            cmd11 = New SqlCommand("Select * from  " + name1 + "", con)
        End If
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd11)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)
        If (select_chart.SelectedItem.Text.Equals("Point")) Then
            chart_data.Series(0).ChartType = Point
        End If
        If (select_chart.SelectedItem.Text.Equals("FastPoint")) Then
            chart_data.Series(0).ChartType = FastPoint
        End If
        If (select_chart.SelectedItem.Text.Equals("Bubble")) Then
            chart_data.Series(0).ChartType = Bubble
        End If
        If (select_chart.SelectedItem.Text.Equals("Line")) Then
            chart_data.Series(0).ChartType = Line
        End If
        If (select_chart.SelectedItem.Text.Equals("Spline")) Then
            chart_data.Series(0).ChartType = Spline
        End If
        If (select_chart.SelectedItem.Text.Equals("StepLine")) Then
            chart_data.Series(0).ChartType = StepLine
        End If
        If (select_chart.SelectedItem.Text.Equals("FastLine")) Then
            chart_data.Series(0).ChartType = FastLine
        End If
        If (select_chart.SelectedItem.Text.Equals("Column")) Then
            chart_data.Series(0).ChartType = Column
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedColumn")) Then
            chart_data.Series(0).ChartType = StackedColumn
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedColumn100")) Then
            chart_data.Series(0).ChartType = StackedColumn100
        End If
        If (select_chart.SelectedItem.Text.Equals("Area")) Then
            chart_data.Series(0).ChartType = Area
        End If
        If (select_chart.SelectedItem.Text.Equals("SplineArea")) Then
            chart_data.Series(0).ChartType = SplineArea
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedArea")) Then
            chart_data.Series(0).ChartType = StackedArea
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedArea100")) Then
            chart_data.Series(0).ChartType = StackedArea100
        End If
        If (select_chart.SelectedItem.Text.Equals("Stock")) Then
            chart_data.Series(0).ChartType = Stock
        End If
        If (select_chart.SelectedItem.Text.Equals("Candlestick")) Then
            chart_data.Series(0).ChartType = Candlestick
        End If
        If (select_chart.SelectedItem.Text.Equals("Range")) Then
            chart_data.Series(0).ChartType = Range
        End If
        If (select_chart.SelectedItem.Text.Equals("SplineRange")) Then
            chart_data.Series(0).ChartType = SplineRange
        End If
        If (select_chart.SelectedItem.Text.Equals("ErrorBar")) Then
            chart_data.Series(0).ChartType = ErrorBar
        End If
        If (select_chart.SelectedItem.Text.Equals("BoxPlot")) Then
            chart_data.Series(0).ChartType = BoxPlot
        End If
        If (select_chart.SelectedItem.Text.Equals("Pie")) Then
            chart_data2.Visible = True
            chart_data_pie.Visible = True
            chart_data_pie.Series(0).ChartType = Pie
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_pie.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pie.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_pie.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_pie.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pie.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_pie.DataSource = ds1
            chart_data_pie.DataBind()
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Bar")) Then
            chart_data3.Visible = True
            chart_data_bar.Visible = True
            chart_data_bar.Series(1).ChartType = Bar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_bar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_bar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_bar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_bar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_bar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_bar.DataSource = ds1
            chart_data_bar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Pyramid")) Then
            chart_data4.Visible = True
            chart_data_pyramid.Visible = True
            chart_data_pyramid.Series(1).ChartType = Pyramid
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_pyramid.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pyramid.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_pyramid.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_pyramid.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pyramid.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_pyramid.DataSource = ds1
            chart_data_pyramid.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Funnel")) Then
            chart_data5.Visible = True
            chart_data_funnel.Visible = True
            chart_data_funnel.Series(1).ChartType = Funnel
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_funnel.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_funnel.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_funnel.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_funnel.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_funnel.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_funnel.DataSource = ds1
            chart_data_funnel.DataBind()
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Doughnut")) Then
            chart_data6.Visible = True
            chart_data_doughnut.Visible = True
            chart_data_doughnut.Series(1).ChartType = Doughnut
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_doughnut.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_doughnut.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_doughnut.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_doughnut.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_doughnut.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_doughnut.DataSource = ds1
            chart_data_doughnut.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Radar")) Then
            chart_data7.Visible = True
            chart_data_radar.Visible = True
            chart_data_radar.Series(1).ChartType = Radar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_radar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_radar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_radar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_radar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_radar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_radar.DataSource = ds1
            chart_data_radar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Polar")) Then
            chart_data8.Visible = True
            chart_data_polar.Visible = True
            chart_data_polar.Series(1).ChartType = Polar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_polar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_polar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_polar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_polar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_polar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_polar.DataSource = ds1
            chart_data_polar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            If (ClientName.SelectedValue = "--Select--") Then
                Session("clientid") = 0
            Else
                Session("clientid") = ClientName.SelectedValue
            End If
            If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
                Session("lobid") = 0
            Else
                Session("lobid") = ddlLobName.SelectedValue
            End If
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        chart_data1.Visible = True
        chart_data.Visible = True
        If (selectedcols.Items.Count.Equals(3)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
        ElseIf (selectedcols.Items.Count.Equals(2)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
        End If
        chart_data.DataSource = ds1
        chart_data.DataBind()
        plot_graph.Visible = False
        Session("deptid") = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            Session("clientid") = 0
        Else
            Session("clientid") = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            Session("lobid") = 0
        Else
            Session("lobid") = ddlLobName.SelectedValue
        End If
        Session("gtype") = select_chart.SelectedItem.Text
        If (rbreport.Checked()) Then
            Session("repname") = ddlTable.SelectedItem.Text
        ElseIf (rbtable.Checked()) Then
            Session("repname") = "table_" + ddlTable.SelectedItem.Text
        End If
    End Sub

    Protected Sub get_tables_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_tables.Click
        If (rbreport.Checked = False And rbtable.Checked = False) Then
            aspnet_msgbox("Please Select Atleast One Radio Button")
            Exit Sub
        End If

        cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId=' 60' and ClientId='0' and LOBId='0' and createdBy='" + Session("userid") + "'", con)
        con.Open()
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "Tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--select--")
        get_tbcolumn_singleuser.Visible = True
        get_tbcolumn.Visible = False
        get_rpcolumn.Visible = False
        get_rpcolumn_singleuser.Visible = False
    End Sub

    Protected Sub get_report_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_report.Click
        If (rbreport.Checked = False And rbtable.Checked = False) Then
            aspnet_msgbox("Please Select Atleast One Radio Button")
            Exit Sub
        End If
        cmd = New SqlCommand("select * from IDMSReportMaster where DepartmentId='60' and ClientId='0' and UnderLOB='0' and SavedBy='" + Session("userid") + "'", con)
        con.Open()
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "QueryName"
        ddlTable.DataValueField = "Recordid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--select--")
        get_rpcolumn_singleuser.Visible = True
        get_tbcolumn.Visible = False
        get_rpcolumn.Visible = False
        get_tbcolumn_singleuser.Visible = False
    End Sub

    Protected Sub get_tbcolumn_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_tbcolumn_singleuser.Click
        If (ddlTable.SelectedItem.Text = "--select--") Then
            aspnet_msgbox("Please Select Table or Report First")
            Exit Sub
        End If
        con.Close()
        con.Open()
        Dim com1 As New SqlCommand("select Visiblecolumn from WARSLOBTableMaster where Departmentid='60' and Clientid='0' and LOBid='0' and TableName='" + ddlTable.SelectedItem.Text + "'", con)
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

    Protected Sub get_rpcolumn_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_rpcolumn_singleuser.Click
        If (ddlTable.SelectedItem.Text = "--select--") Then
            aspnet_msgbox("Please Select Table or Report First")
            Exit Sub
        End If
        Dim dept, client, lob As String
        dept = 60
        client = 0
        lob = 0

        'Dim dept_id, client_id, lobid As String
        'If Me.DepartmentName.SelectedIndex > 0 Then
        '    If Me.ClientName.SelectedIndex > 0 Then
        '        client_id = Convert.ToInt32(ClientName.SelectedValue)
        '    Else
        '        client_id = "0"
        '    End If
        '    dept_id = DepartmentName.SelectedValue.ToString()
        '    If Me.ddlLobName.SelectedIndex > 0 Then
        '        lobid = Convert.ToInt32(ddlLobName.SelectedValue)
        '    Else
        '        lobid = "0"
        '    End If
        'End If
        gridreportbind1()
    End Sub

    Protected Sub plot_graph_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plot_graph_singleuser.Click
        If (rbreport.Checked = False And rbtable.Checked = False) Then
            aspnet_msgbox("Please Select Atleast One Radio Button")
            Exit Sub
        End If
        If (ddlTable.SelectedItem.Text = "--select--") Then
            aspnet_msgbox("Please Select Table Or Report First")
            Exit Sub
        End If
        save_singleuser.Visible = True
        chart_data1.Visible = True
        chart_data.Visible = True
        Dim a1, a2, k As Integer
        Dim repcol As String = ""
        Dim sltcol As String = ""
        a1 = repcols.Items.Count
        a2 = selectedcols.Items.Count
        For k = 0 To a1 - 1
            If (repcol.Equals("")) Then
                repcol = repcols.Items(k).Text
            Else
                repcol = repcol + "," + repcols.Items(k).Text
            End If

        Next
        Session("repcols") = repcol
        For k = 0 To a2 - 1
            If (sltcol.Equals("")) Then
                sltcol = selectedcols.Items(k).Text
            Else
                sltcol = sltcol + "," + selectedcols.Items(k).Text
            End If

        Next
        Session("selectcols") = sltcol
        Session("deptid") = 60
        Session("clientid") = 0
        Session("lobid") = 0
        Session("gtype") = "Column"
        If (rbreport.Checked()) Then
            Session("repname") = ddlTable.SelectedItem.Text
        ElseIf (rbtable.Checked()) Then
            Session("repname") = "table_" + ddlTable.SelectedItem.Text
        End If



        'If (select_chart.SelectedItem.Text.Equals("--Select--")) Then
        '    slt_chart.Text = "Column Chart"
        'Else
        '    slt_chart.Text = select_chart.SelectedItem.Text + " Chart"
        'End If
        'slt_chart.Visible = True
        If (selectedcols.Items.Count < 2) Then
            aspnet_msgbox("Minimum 2 Columns are Allowed To Make a Graph")
        End If
        xaxis_select.Items.Insert(0, "--Select--")
        yaxix1_select.Items.Insert(0, "--Select--")
        yaxix2_select.Items.Insert(0, "--Select--")
        Dim rep_name = ddlTable.SelectedItem.Text
        If (selectedcols.Items.Count > 3) Then
            aspnet_msgbox("Maximum 3 Columns are Allowed To Make a Graph")
            Exit Sub
        End If
        Dim i As Integer = selectedcols.Items.Count
        Dim cmd11, cmd22, cmd33 As SqlCommand
        con.Close()
        con.Open()
        If (i.Equals(3)) Then
            s1 = selectedcols.Items(i - 3).Text
            s2 = selectedcols.Items(i - 2).Text
            s3 = selectedcols.Items(i - 1).Text
        ElseIf (i.Equals(2)) Then
            s1 = selectedcols.Items(i - 2).Text
            s2 = selectedcols.Items(i - 1).Text
        End If
        Dim cmd1 As SqlCommand
        Dim name As String
        Dim c1, c2, c3 As String
        If (rbtable.Checked()) Then
            cmd1 = New SqlCommand("Select * from " + rep_name + "", con)
            Dim y As Integer = 1
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s1)
                    yaxix2_select.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s2)
                    yaxix2_select.Items.Insert(y, s2)
                    y = y + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s3)
                    yaxix2_select.Items.Insert(y, s3)
                    y = y + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s1)
                    yaxix2_select.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + rep_name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd11.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(y, s2)
                    yaxix2_select.Items.Insert(y, s2)
                    y = y + 1
                End If

            End If
            con.Close()

        ElseIf (rbreport.Checked()) Then
            con.Close()
            con.Open()
            Dim x As Integer = 1
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + rep_name + "'", con)
            name = (cmd.ExecuteScalar()).ToString()
            cmd1 = New SqlCommand("Select * from  " + name + "", con)
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s1)
                    yaxix2_select.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s2)
                    yaxix2_select.Items.Insert(x, s2)
                    x = x + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s3)
                    yaxix2_select.Items.Insert(x, s3)
                    x = x + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s1)
                    yaxix2_select.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    yaxix1_select.Items.Insert(x, s2)
                    yaxix2_select.Items.Insert(x, s2)
                    x = x + 1
                End If
            End If

        End If
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)
        'If (select_chart.SelectedItem.Text.Equals("-- Select--")) Then
        chart_data.Series(0).ChartType = Column
        If (i.Equals(3)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(s1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(s2).ToString()
            chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(s3).ToString()
        ElseIf (i.Equals(2)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(s1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(s2).ToString()
        End If
        'End If
        chart_data.DataSource = ds1
        chart_data.DataBind()
        If (i.Equals(3)) Then
            sltchart.Visible = True
            xaxis.Visible = True
            yaxis1.Visible = True
            yaxis2.Visible = True
            select_chart.Visible = True
            xaxis_select.Visible = True
            yaxix1_select.Visible = True
            yaxix2_select.Visible = True
        ElseIf (i.Equals(2)) Then
            sltchart.Visible = True
            xaxis.Visible = True
            yaxis1.Visible = True
            yaxis1.Text = "Y Axis Member"
            select_chart.Visible = True
            xaxis_select.Visible = True
            yaxix1_select.Visible = True
        End If
        If (i.Equals(3)) Then
            xaxis_select.Items.Insert(1, s1)
            xaxis_select.Items.Insert(2, s2)
            xaxis_select.Items.Insert(3, s3)
        ElseIf (i.Equals(2)) Then
            xaxis_select.Items.Insert(1, s1)
            xaxis_select.Items.Insert(2, s2)
        End If
        plt_gph_singleuser.Visible = True
        plot_graph_singleuser.Visible = False
    End Sub

    Protected Sub plt_gph_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plt_gph_singleuser.Click
        If (select_chart.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Chart Type")
            Exit Sub
        End If
        If (xaxis_select.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select X axis Member")
            Exit Sub
        End If
        If (yaxix1_select.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Y axis Member")
            Exit Sub
        End If
        If (selectedcols.Items.Count.Equals(3)) Then
            If (yaxix2_select.SelectedItem.Text = "--Select--") Then
                aspnet_msgbox("Please Select Y axis IInd Member")
                Exit Sub
            End If
        End If
        Dim name1 As String
        Dim rep_name As String = ddlTable.SelectedItem.Text
        Dim cmd11 As SqlCommand
        x1 = xaxis_select.SelectedItem.Text
        y1 = yaxix1_select.SelectedItem.Text
        z1 = yaxix2_select.SelectedItem.Text
        If (rbtable.Checked()) Then
            cmd11 = New SqlCommand("Select * from " + rep_name + "", con)
            con.Close()
        ElseIf (rbreport.Checked()) Then
            con.Close()
            con.Open()
            Dim x As Integer = 1
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + rep_name + "'", con)
            name1 = (cmd.ExecuteScalar()).ToString()
            cmd11 = New SqlCommand("Select * from  " + name1 + "", con)
        End If
        Session("deptid1") = 60
        Session("clientid1") = 0
        Session("lobid1") = 0
        Session("gtype") = select_chart.SelectedItem.Text
        Session("repname") = ddlTable.SelectedItem.Text
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd11)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)
        If (select_chart.SelectedItem.Text.Equals("Point")) Then
            chart_data.Series(0).ChartType = Point
        End If
        If (select_chart.SelectedItem.Text.Equals("FastPoint")) Then
            chart_data.Series(0).ChartType = FastPoint
        End If
        If (select_chart.SelectedItem.Text.Equals("Bubble")) Then
            chart_data.Series(0).ChartType = Bubble
        End If
        If (select_chart.SelectedItem.Text.Equals("Line")) Then
            chart_data.Series(0).ChartType = Line
        End If
        If (select_chart.SelectedItem.Text.Equals("Spline")) Then
            chart_data.Series(0).ChartType = Spline
        End If
        If (select_chart.SelectedItem.Text.Equals("StepLine")) Then
            chart_data.Series(0).ChartType = StepLine
        End If
        If (select_chart.SelectedItem.Text.Equals("FastLine")) Then
            chart_data.Series(0).ChartType = FastLine
        End If
        If (select_chart.SelectedItem.Text.Equals("Column")) Then
            chart_data.Series(0).ChartType = Column
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedColumn")) Then
            chart_data.Series(0).ChartType = StackedColumn
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedColumn100")) Then
            chart_data.Series(0).ChartType = StackedColumn100
        End If
        If (select_chart.SelectedItem.Text.Equals("Area")) Then
            chart_data.Series(0).ChartType = Area
        End If
        If (select_chart.SelectedItem.Text.Equals("SplineArea")) Then
            chart_data.Series(0).ChartType = SplineArea
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedArea")) Then
            chart_data.Series(0).ChartType = StackedArea
        End If
        If (select_chart.SelectedItem.Text.Equals("StackedArea100")) Then
            chart_data.Series(0).ChartType = StackedArea100
        End If
        If (select_chart.SelectedItem.Text.Equals("Stock")) Then
            chart_data.Series(0).ChartType = Stock
        End If
        If (select_chart.SelectedItem.Text.Equals("Candlestick")) Then
            chart_data.Series(0).ChartType = Candlestick
        End If
        If (select_chart.SelectedItem.Text.Equals("Range")) Then
            chart_data.Series(0).ChartType = Range
        End If
        If (select_chart.SelectedItem.Text.Equals("SplineRange")) Then
            chart_data.Series(0).ChartType = SplineRange
        End If
        If (select_chart.SelectedItem.Text.Equals("ErrorBar")) Then
            chart_data.Series(0).ChartType = ErrorBar
        End If
        If (select_chart.SelectedItem.Text.Equals("BoxPlot")) Then
            chart_data.Series(0).ChartType = BoxPlot
        End If
        If (select_chart.SelectedItem.Text.Equals("Pie")) Then
            chart_data2.Visible = True
            chart_data_pie.Visible = True
            chart_data_pie.Series(0).ChartType = Pie
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_pie.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pie.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_pie.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_pie.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pie.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_pie.DataSource = ds1
            chart_data_pie.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Bar")) Then
            chart_data3.Visible = True
            chart_data_bar.Visible = True
            chart_data_bar.Series(1).ChartType = Bar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_bar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_bar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_bar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_bar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_bar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_bar.DataSource = ds1
            chart_data_bar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Pyramid")) Then
            chart_data4.Visible = True
            chart_data_pyramid.Visible = True
            chart_data_pyramid.Series(1).ChartType = Pyramid
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_pyramid.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pyramid.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_pyramid.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_pyramid.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_pyramid.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_pyramid.DataSource = ds1
            chart_data_pyramid.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Funnel")) Then
            chart_data5.Visible = True
            chart_data_funnel.Visible = True
            chart_data_funnel.Series(1).ChartType = Funnel
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_funnel.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_funnel.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_funnel.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_funnel.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_funnel.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_funnel.DataSource = ds1
            chart_data_funnel.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Doughnut")) Then
            chart_data6.Visible = True
            chart_data_doughnut.Visible = True
            chart_data_doughnut.Series(1).ChartType = Doughnut
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_doughnut.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_doughnut.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_doughnut.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_doughnut.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_doughnut.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_doughnut.DataSource = ds1
            chart_data_doughnut.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Radar")) Then
            chart_data7.Visible = True
            chart_data_radar.Visible = True
            chart_data_radar.Series(1).ChartType = Radar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_radar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_radar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_radar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_radar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_radar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_radar.DataSource = ds1
            chart_data_radar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        If (select_chart.SelectedItem.Text.Equals("Polar")) Then
            chart_data8.Visible = True
            chart_data_polar.Visible = True
            chart_data_polar.Series(1).ChartType = Polar
            If (selectedcols.Items.Count.Equals(3)) Then
                chart_data_polar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_polar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
                chart_data_polar.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
            ElseIf (selectedcols.Items.Count.Equals(2)) Then
                chart_data_polar.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
                chart_data_polar.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            End If
            chart_data_polar.DataSource = ds1
            chart_data_polar.DataBind()
            Session("deptid") = DepartmentName.SelectedValue
            Session("clientid") = ClientName.SelectedValue
            Session("lobid") = ddlLobName.SelectedValue
            Session("gtype") = select_chart.SelectedItem.Text
            If (rbreport.Checked()) Then
                Session("repname") = ddlTable.SelectedItem.Text
            ElseIf (rbtable.Checked()) Then
                Session("repname") = "table_" + ddlTable.SelectedItem.Text
            End If
            Exit Sub
        End If
        chart_data1.Visible = True
        chart_data.Visible = True
        If (selectedcols.Items.Count.Equals(3)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
        ElseIf (selectedcols.Items.Count.Equals(2)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
        End If
        chart_data.DataSource = ds1
        chart_data.DataBind()
        plot_graph_singleuser.Visible = False
        
    End Sub

    Protected Sub opengraph_single_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opengraph_single.Click
        Response.Redirect("~\Graphs\open_single.aspx")
    End Sub

    Protected Sub opengraph_multiple_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles opengraph_multiple.Click
        Response.Redirect("~\Graphs\open_multiple.aspx")
    End Sub

    Protected Sub rbtable_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbtable.CheckedChanged
        ddlTable.Visible = True
        sltchart.Visible = False
        xaxis.Visible = False
        yaxis1.Visible = False
        yaxis2.Visible = False
        select_chart.Visible = False
        xaxis_select.Visible = False
        yaxix1_select.Visible = False
        yaxix2_select.Visible = False
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        xaxis_select.Items.Clear()
        yaxix1_select.Items.Clear()
        yaxix2_select.Items.Clear()
        get_tables.Visible = True
        get_report.Visible = False
        DepartmentName.Items.Clear()
        selected_radio.Visible = True
        selected_radio.Text = "Select Table"
        ClientName.Items.Clear()
        ddlLobName.Items.Clear()
        ddlTable.Items.Clear()
        If (producttype.Equals("Multiple User")) Then
            get_report.Visible = False
            get_tables.Visible = False
        ElseIf (producttype.Equals("Multiple User")) Then
            get_report.Visible = False
            get_tables.Visible = False
        End If
        Dim typeofuser = Session("typeofuser")
        If (typeofuser.Equals("Super Admin")) Then
            Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
            connection.Open()
            DepartmentName.DataSource = cmd.ExecuteReader()
            DepartmentName.DataTextField = "DepartmentName"
            DepartmentName.DataValueField = "AutoID"
            DepartmentName.Items.Insert(0, "-- Select--")
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
    End Sub

    Protected Sub rbreport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbreport.CheckedChanged
        ddlTable.Visible = True
        sltchart.Visible = False
        xaxis.Visible = False
        yaxis1.Visible = False
        yaxis2.Visible = False
        select_chart.Visible = False
        xaxis_select.Visible = False
        yaxix1_select.Visible = False
        yaxix2_select.Visible = False
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        xaxis_select.Items.Clear()
        yaxix1_select.Items.Clear()
        yaxix2_select.Items.Clear()
        get_report.Visible = True
        get_tables.Visible = False
        selected_radio.Visible = True
        selected_radio.Text = "Select Report"
        DepartmentName.Items.Clear()
        ClientName.Items.Clear()
        ddlLobName.Items.Clear()
        ddlTable.Items.Clear()
        plot_graph_singleuser.Visible = False
        If (producttype.Equals("Multiple User")) Then
            get_report.Visible = False
            get_tables.Visible = False
        ElseIf (producttype.Equals("Multiple User")) Then
            get_report.Visible = False
            get_tables.Visible = False
        End If
        Dim typeofuser = Session("typeofuser")
        If (typeofuser.Equals("Super Admin")) Then
            Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
            connection.Open()
            DepartmentName.DataSource = cmd.ExecuteReader()
            DepartmentName.DataTextField = "DepartmentName"
            DepartmentName.DataValueField = "AutoID"
            DepartmentName.Items.Insert(0, "-- Select--")
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
    End Sub

    Protected Sub show_multi_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles show_multi.Click
        Response.Redirect("~\Graphs\ViewGraph_multi.aspx")
    End Sub
End Class
