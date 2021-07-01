Imports System
Imports System.Data
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.SessionState
Imports System.Collections
Imports Dundas.Charting.Utilities
Imports DundasUtilities.Charting.SixSigma
Imports System.ComponentModel
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Web.UI.WebControls.WebParts

Partial Class GraphData

#Region "variable declration"
    Inherits System.Web.UI.Page
    ' Protected TableHelper As Dundas.Charting.Utilities
    Public piehelper As Dundas.Charting.Utilities.Piecollected
    Public boolchart As Boolean
    Dim classobj As New Functions
    Dim AnalysisObject As New SavedAnalysis
    Dim x, y, n, b, m1, fmname
    Dim dr As SqlDataReader
    Dim countercol As Integer = 1
    Dim ds2 As New DataSet
    Dim dateval As Date
    Dim dat As DateTime
    Dim dval As Date
    Dim repobj As New ReportDesigner
    Dim fun As New Functions
    Dim selectRep As New ReportDesigner
    Dim graphobj As New GraphicalPresentation
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim conn2 As String = AppSettings("ConnectionString")
    Dim cmd As SqlCommand
    Dim connection As New SqlConnection(conn)
    Dim con As New SqlConnection(conn)
    Dim con2 As New SqlConnection(conn2)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim reportcolumn As DataColumn
    Dim fb As Boolean
    Dim colname, formula, groupby, formultxt, orderby, havingcondition, seriesName, sortedseries, repcolumn, columnseries, dupcol As String
    Dim fontname As FontFamily
    Public graph As Chart
    Public stt As String
    Dim p
    Dim txtformulaarr
    Dim norformulaarr
    Dim j As Integer = 0
    Public gridstring As String
    Dim bool As Boolean
    Dim Duplicate, clumnser, strnorcol As String
    Dim counter As Double = 0.0
    Dim count, t, iloopindex, blankravl As Integer
    Dim arr(30) As String
    Dim label1 As Label
    Dim label2 As Label
    Dim label3 As Label
    Dim label4 As Label
    Dim dsImage As New DataSet
    Dim gg As Label
    Public dept As String = "0"
    Public client As String = "0"
    Public lob As String = "0"
    Public userid As String = "0"
    Dim imgcounter As Integer = 0
    Public currsp As String
    Public repName As String = ""
    Public hidgraphtype As String = ""
    Public hidgraphname As String = ""
    Public hidcolumnname As String = ""
    Public hidcolumnseries As String = ""
    Public hidtodate As String = ""
    Public hidfromdate As String = ""
    Public hidcommanformat As String = ""
    Public hidcommanformat1 As String = ""
    Public hidcommanformat2 As String = ""
    Public hidcommanformat3 As String = ""
    Public hidspecificproperties As String = ""
    Public hidtotalcolumn As String = ""
    Public hidcreatedon As String = ""
    Dim deptid, clientid, lobid As String
    Public hidDept As String = ""
    Public hidClient As String = ""
    Public hidreptodate As String = ""
    Public hidrepfromdate As String = ""
    Public hidlob As String = ""
    Public hidsavedby As String = ""
    Dim com As SqlCommand
    Dim data1 As SqlDataReader
    Dim com1 As SqlCommand
    Protected imgImage As System.Web.UI.WebControls.Image
    Private THUMBNAIL_SIZE As Integer = 60

#End Region

#Region "Page_Load"
    ''' <summary>
    ''' Page Load Descriptiion
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        connection.Open()
        Dim producttype As String
        Dim cmd1 As SqlCommand = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        Dim rdr = cmd1.ExecuteReader
        If rdr.Read Then
            producttype = rdr("ProductType")
            If (producttype = "Multiple User") Then
                'spandisplay.Visible = True
                dept_row.Visible = True
                client_row.Visible = True
                lob_row.Visible = True

                get_report.Visible = False
                ShowReport.Visible = True
                ShowReport_singleuser.Visible = False
                btnGraph.Visible = True
                btnGraph_singleuser.Visible = False
                btnsave.Visible = True
                btnsave_singleuser.Visible = False
                btnOpenGraph.Visible = True
                btnOpenGraph_singleuser.Visible = False
                rdr.Close()
                cmd1.Dispose()
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
                    select_level2.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    select_level3.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If

            Else
                'spandisplay.Visible = False
                get_report.Visible = True
                button_single_row.Visible = True
                ShowReport_singleuser.Visible = True
                ShowReport.Visible = False
                btnGraph_singleuser.Visible = True
                btnGraph.Visible = False
                btnsave.Visible = False
                btnsave_singleuser.Visible = True
                btnOpenGraph.Visible = False
                btnOpenGraph_singleuser.Visible = True
            End If
        End If
        'End If
        cmd1.Dispose()
        rdr.Close()
        Dim typeofuser = Session("typeofuser")
        If Page.IsPostBack = False Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                dr = cmd.ExecuteReader()
                DepartmentName.DataSource = dr
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                dr = cmd.ExecuteReader()
                DepartmentName.DataSource = dr
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                dr = cmd.ExecuteReader()
                DepartmentName.DataSource = dr
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.DataBind()
            End If
            DepartmentName.Items.Insert(0, "--Select--")
            dr.Close()
        End If

        'cmd.Dispose()





        'lblgraph.Text = Session("Savegp")
        If Request.QueryString("currentreport") <> "" Then
            If (producttype = "Multiple User") Then
                txtCurrentReport.Text = Request.QueryString("currentreport")
                hidDept = Trim(Request.QueryString("dept"))

                hidClient = Trim(Request.QueryString("client"))
                hidlob = Trim(Request.QueryString("lob"))
                txtTodate.Text = Trim(Request.QueryString("todate"))
                txtFromdate.Text = Trim(Request.QueryString("fromdate"))
                currsp = txtCurrentReport.Text
                ddlDepartmant.Enabled = False
                ddlClient.Enabled = False
                ddlLob.Enabled = False
                'rbnReport.Enabled = False
                'rbnAnalysis.Enabled = False
                ShowReport.Enabled = False
                btnOpenGraph.Visible = False
                If selectedcols.Items.Count = 0 And repcols.Items.Count = 0 Then
                    gridreportbind()
                End If
            Else
                txtCurrentReport.Text = Request.QueryString("currentreport")
                hidDept = Trim(Request.QueryString("dept"))

                hidClient = Trim(Request.QueryString("client"))
                hidlob = Trim(Request.QueryString("lob"))
                txtTodate.Text = Trim(Request.QueryString("todate"))
                txtFromdate.Text = Trim(Request.QueryString("fromdate"))
                currsp = txtCurrentReport.Text
                ddlDepartmant.Enabled = False
                ddlClient.Enabled = False
                ddlLob.Enabled = False
                'rbnReport.Enabled = False
                'rbnAnalysis.Enabled = False
                ShowReport.Enabled = False
                btnOpenGraph.Visible = False
                If selectedcols.Items.Count = 0 And repcols.Items.Count = 0 Then
                    gridreportbind_single()
                End If
            End If

            

            'this is to get the recordid of report
            Dim cmdrecord As String
            If hidDept = "" And hidClient = "" And hidlob = "" Then

                cmdrecord = "select recordid from idmsreportmaster where queryname='" + txtCurrentReport.Text + "' "

            Else

                cmdrecord = "select recordid from idmsreportmaster where queryname='" + txtCurrentReport.Text + "' and DepartmentId='" + hidDept + "' and ClientId='" + hidClient + "' and underlob='" + hidlob + "' "

            End If
            con.Close()
            Dim cmdrecordid As New SqlCommand(cmdrecord, con)
            con.Open()
            Dim recordidnew As String = cmdrecordid.ExecuteScalar
            con.Close()
            checkreportrights(hidDept, hidClient, hidlob, recordidnew)
        End If





        If txtCurrentReport.Text <> "" Then
            txtCurrentReport.Visible = True
            'rbnReport.Checked = True
            ddlReport.Visible = False
        ElseIf txtCurrentReport.Text = "" Then

            txtCurrentReport.Visible = False
            ddlReport.Visible = True
        End If
        If rbnColumn.Checked = False Then
            seriesbtn.Value = "Row"
        End If
        If hidChart.Value = "Pareto" Or hidChart.Value = "Run" Or hidChart.Value = "Histogram" Then
            seriesbtn.Value = "Column"
        End If

        If Page.IsPostBack = False Then
            Session("Savegp") = ""
            lblgraphname.Visible = False
            ddlgraphname.Visible = False
            lblChartimage.Visible = False
            ' trAnalysis.Visible = False
            ShowReport.Visible = True
            Selectreport.Visible = True

            If rbnColumn.Checked = False Then
                rbnRow.Checked = True
            Else
                rbnColumn.Checked = True
            End If

            Dim classobj As New Functions
            ddlDepartmant.DataTextField = "departmentname"
            ddlDepartmant.DataValueField = "autoid"
            ddlDepartmant.DataSource = classobj.bind_Department()
            ddlDepartmant.DataBind()
            ddlDepartmant.Items.Insert(0, "---select---")
            ddlMajorgridline.Items.Insert(0, "---SELECT--")
            ddlMinorType.Items.Insert(0, "---SELECT--")

            For Each fontname In FontFamily.Families
                ddlFont1.Items.Add(fontname.Name)

            Next

            For Each fontname In FontFamily.Families
                ddlXlabelfont.Items.Add(fontname.Name)
            Next
            For Each fontname In FontFamily.Families
                ddlYfontname.Items.Add(fontname.Name)
            Next
            ' Add Hatch styles to control. 
            For Each colorName As String In [Enum].GetNames(GetType(ChartHatchStyle))
                ddlHatchstyle.Items.Add(colorName)
            Next
            ddlHatchstyle.Items(0).Selected = True
            For Each colorName As String In [Enum].GetNames(GetType(ChartHatchStyle))
                ddlLegendhatch.Items.Add(colorName)
            Next
            ddlLegendhatch.Items(0).Selected = True
            Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(Me.bkcolor.Value)

            ' Add Chart Gradient types to control. 
            For Each colorName As String In [Enum].GetNames(GetType(GradientType))
                ddlGradient.Items.Add(colorName)
            Next
            For Each colorName As String In [Enum].GetNames(GetType(GradientType))
                ddlLegendgradient.Items.Add(colorName)
            Next
            ' Add Chart Line styles to control. 
            For Each colorName As String In [Enum].GetNames(GetType(ChartDashStyle))
                ddlBorderstyle.Items.Add(colorName)
            Next

            ddlBorderstyle.Items.FindByValue("Solid").Selected = True
            For Each colorName As String In [Enum].GetNames(GetType(ChartDashStyle))
                ddlLegendborderstyle.Items.Add(colorName)
            Next

            ddlLegendborderstyle.Items.FindByValue("Solid").Selected = True

        Else

            Dim strcolor As String
            strcolor = "<script language='javascript' type='text/javascript'>"
            strcolor = strcolor + "var inputbtn= document.getElementById('sample_2');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + bkcolor.Value + "';"
            strcolor = strcolor + "var inputbtn= document.getElementById('sampleBordercolor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + brcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('txtlegengbkcolor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + legendbkcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('txtlegengbkcolor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + legendbkcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('txtlegengbrcolor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + legendbrcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('SampleCol');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + xaxiscolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('LColor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + yaxiscolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('SamColor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + titlefontcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('LineColor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + titlebordercolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('SampleColor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + titlebkcolor.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('SampleLine');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + Majorgridcolour.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('SampleLineColor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + ddlMinorColor1.Value + "';"
            strcolor = strcolor + " inputbtn= document.getElementById('Areabackcolor');"
            strcolor = strcolor + " inputbtn.style.backgroundColor = '" + chartareabkcolor.Value + "';"
            strcolor = strcolor + " </script>"
            ClientScript.RegisterStartupScript(Page.GetType(), "startup", strcolor)
        End If



        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Enabled = False
        'smitha'
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.Enabled = False
        StockChart.ChartAreas("Price").AxisY.MajorTickMark.Enabled = False
        StockChart.ChartAreas("Price").AxisY.MinorTickMark.Enabled = False
        'smitha'



        StockChart.ChartAreas("Price").AxisX.MajorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisY.MajorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MajorTickMark.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MinorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.Enabled = False
        graph = Chart1

    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Java script Message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Public Sub SetupChart()
        ' Set legend style 
        Chart1.Legends("Default").LegendStyle = DirectCast(LegendStyle.Parse(GetType(LegendStyle), ddlLegendStyleList.SelectedItem.Text), LegendStyle)

        ' Set legend docking 
        Chart1.Legends("Default").Docking = DirectCast(Docking.Parse(GetType(LegendDocking), ddlLegendDockingList.SelectedItem.Text), LegendDocking)

        ' Set legend alignment 
        Chart1.Legends("Default").Alignment = DirectCast(StringAlignment.Parse(GetType(StringAlignment), ddlLegendAlinmentList.SelectedItem.Text), StringAlignment)

        ' Set whether the legend is reversed 
        If Me.chk_Reversed.Checked Then
            Chart1.Legends("Default").Reversed = AutoBool.[True]
        Else
            Chart1.Legends("Default").Reversed = AutoBool.[False]

        End If

        ' Set table style 
        Me.Chart1.Legends("Default").TableStyle = DirectCast(LegendTableStyle.Parse(GetType(LegendTableStyle), Me.ddlTheTableStyle.SelectedItem.ToString()), LegendTableStyle)
        Me.ddlTheTableStyle.Enabled = False
        'End If
    End Sub
    
    ''' <summary>
    ''' Chart Legend CellColumn Formating
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateLegendCellColumns()
        ' Add first cell column 
        Dim firstColumn As New LegendCellColumn()
        firstColumn.ColumnType = LegendCellColumnType.SeriesSymbol
        firstColumn.HeaderText = "Color"
        firstColumn.HeaderBackColor = Color.AliceBlue
        Me.Chart1.Legends("Default").CellColumns.Add(firstColumn)

        ' Add second cell column 
        Dim secondColumn As New LegendCellColumn()
        secondColumn.ColumnType = LegendCellColumnType.Text
        secondColumn.HeaderText = "Name"
        secondColumn.Text = "#LEGENDTEXT"
        secondColumn.HeaderBackColor = Color.WhiteSmoke
        Me.Chart1.Legends("Default").CellColumns.Add(secondColumn)

        ' Add header separator of type line 
        Me.Chart1.Legends("Default").HeaderSeparator = LegendSeparatorType.Line
        Me.Chart1.Legends("Default").HeaderSeparatorColor = Color.FromArgb(64, 64, 64, 64)

        ' Add item column separator of type line 
        Me.Chart1.Legends("Default").ItemColumnSeparator = LegendSeparatorType.Line
        Me.Chart1.Legends("Default").ItemColumnSeparatorColor = Color.FromArgb(64, 64, 64, 64)

        ' Set AVG cell column attributes 
        Dim avgColumn As New LegendCellColumn()
        avgColumn.Text = "#AVG{N2}"
        avgColumn.HeaderText = "Avg"
        avgColumn.Name = "AvgColumn"
        avgColumn.HeaderBackColor = Color.WhiteSmoke

        ' Set Total cell column attributes 
        Dim totalColumn As New LegendCellColumn()
        totalColumn.Text = "#TOTAL{N1}"
        totalColumn.HeaderText = "Total"
        totalColumn.Name = "TotalColumn"
        totalColumn.HeaderBackColor = Color.WhiteSmoke

        ' Set Min cell column attributes 
        Dim minColumn As New LegendCellColumn()
        minColumn.Text = "#MIN{N1}"
        minColumn.HeaderText = "Min"
        minColumn.Name = "MinColumn"
        minColumn.HeaderBackColor = Color.WhiteSmoke

        If Me.chk_ShowAvg.Checked Then
            Me.Chart1.Legends("Default").CellColumns.Add(avgColumn)
        Else

            Dim cellColumn As LegendCellColumn = Me.Chart1.Legends("Default").CellColumns.FindByName("AvgColumn")
            Me.Chart1.Legends("Default").CellColumns.Remove(cellColumn)
        End If

        If Me.chk_ShowTotal.Checked Then
            Me.Chart1.Legends("Default").CellColumns.Add(totalColumn)
        Else

            Dim cellColumn As LegendCellColumn = Me.Chart1.Legends("Default").CellColumns.FindByName("TotalColumn")
            Me.Chart1.Legends("Default").CellColumns.Remove(cellColumn)
        End If

        If Me.chk_ShowMin.Checked Then
            Me.Chart1.Legends("Default").CellColumns.Add(minColumn)
        Else

            Dim columnToRemove As LegendCellColumn = Me.Chart1.Legends("Default").CellColumns.FindByName("MinColumn")
            Me.Chart1.Legends("Default").CellColumns.Remove(columnToRemove)
        End If
    End Sub
    
    ''' <summary>
    ''' Bind gridview with Report Designer Reports and Listbox
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gridreportbind_single()
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
            clientAna = "0"
            lobana = "0"
        If txtCurrentReport.Text = "" Then
            com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "'  and  departmentid='60'  and  clientid='" + clientAna + "'  and  underlob='" + lobana + "'", con)

        Else
            com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
        End If
        con.Close()
        con.Open()
        readquery = com1.ExecuteReader
        While readquery.Read()
            tablename = readquery("tablename")
        End While
        tablename = tablename.Replace("~", ",")
        tablearray = tablename.Split(",")
        hidtablename.Value = tablename
        com1.Dispose()


        reotablename.Value = tablename
        tabcount = UBound(tablearray)
        con.Close()
        readquery.Close()
        If txtCurrentReport.Text = "" Then
            com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "' and  departmentid='60' and  clientid='0' and  underlob='0'", con)
        Else
            If hidDept = "" Then
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            Else
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "' and  departmentid='60' and  clientid='0' and  underlob='0'", con)
            End If

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
            datestr = strnorcol
            datestr = Replace(datestr, "@Date1@", txtFromdate.Text)
            datestr = Replace(datestr, "@Date2@", txtTodate.Text)
            Normalarray.Value = datestr
            If strfcol <> "" Then
                strfcol = strfcol.Replace("$", ".")
                strfcol = strfcol.Replace("AS", "%")
                Dim datestr1 As String
                datestr1 = strfcol
                datestr1 = Replace(datestr1, "@Date1@", txtFromdate.Text)
                datestr1 = Replace(datestr1, "@Date2@", txtTodate.Text)
                formulaarray.Value = datestr1
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
            If Date1 = True Then
                If txtTodate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill TO DATE")
                    Exit Sub
                End If
            End If
            If Date2 = True Then
                If txtFromdate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill From DATE")

                    Exit Sub
                End If
            End If

            colname = strspace

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

        If astr.Contains("@Date1@") = False And astr.Contains("@Date2@") = False And txtFromdate.Text <> "" And txtTodate.Text <> "" Then
            wheretxt = "where" & " " & formula
            Dim ftext = tablearray(0) + ".date" + " " + "between" + " " + "'" + txtFromdate.Text + "'" + " " + "and" + " " + "'" + txtTodate.Text + "'"
            If formula <> "" Then
                wheretxt = " where " + ftext
            Else
                wheretxt = "where" & " " & ftext
            End If

            hidwheretxt.Value = wheretxt
            Session("hidwheretxt") = wheretxt


        ElseIf astr.Contains("@Date1@") = True And astr.Contains("@Date2@") = True Then

            Date11 = astr.Contains("@Date1@")
            Date22 = astr.Contains("@Date2@")
            If Date11 = True Then
                If txtTodate.Text = "" Then
                    aspnet_msgbox(" Plz Fill To Date")
                    Exit Sub
                End If
            End If
            If Date22 = True Then
                If txtFromdate.Text = "" Then
                    aspnet_msgbox("Fill From Date")
                    Exit Sub
                End If
            End If
            If formula <> "" Then
                wheretxt = "where" & " " & formula
                formula = formula.Replace("'@Date1@'", "'" + txtFromdate.Text + "'")
                formula = formula.Replace("'@Date2@'", "'" + txtTodate.Text + "'")
                wheretxt = "where" & " " & formula
                wheretxt = wheretxt.Replace("$", ".")
                hidwheretxt.Value = wheretxt
                Session("hidwheretxt") = wheretxt

            End If

        Else
            wheretxt = "where" & " " & formula
            wheretxt = wheretxt.Replace("$", ".")
            hidwheretxt.Value = wheretxt
            Session("hidwheretxt") = wheretxt

        End If
        If formula = "" And txtFromdate.Text = "" And txtTodate.Text = "" Then
            wheretxt = ""
            hidwheretxt.Value = ""
            Session("hidwheretxt") = wheretxt

        End If
        If groupby <> "" Then
            groupbytext = "group by" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")
            hidgroup.Value = groupbytext

            Session("hidgroupby") = groupbytext

        Else
            groupbytext = ""
            Session("hidgroupby") = groupbytext
        End If
        If orderby <> "" Then
            orderbytext = "order by" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            hidorder.Value = orderbytext
            Session("hidordertxt") = orderbytext

        Else
            orderbytext = ""
            Session("hidordertxt") = orderbytext
        End If
        If havingcondition <> "" Then
            havingtxt = "having" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            hidhaving.Value = havingtxt
            Session("hidhavingtxt") = havingtxt
        Else
            havingtxt = ""
            Session("hidhavingtxt") = havingtxt
        End If
        con.Close()
        readquery.Close()
        If txtCurrentReport.Text <> "" Then
            com1 = New SqlCommand("select DepartmentId,clientid,underlob from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            con.Open()
            readquery = com1.ExecuteReader
            While readquery.Read()
                reportDept = readquery("DepartmentId")
                reportclient = readquery("clientid")
                reportlob = readquery("underlob")
            End While
            Currentrepdept.Value = reportDept
            Currentrepclient.Value = reportclient
            Currentreplob.Value = reportlob
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
        hidcolumname.Value = columnname
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
            If txtCurrentReport.Text = "" Then
                repName = ddlReport.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlReport.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            Else
                repName = txtCurrentReport.Text
                Session("repname") = repName
                ddlReport.Visible = False
                If data("name") = "tab" & txtCurrentReport.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            End If


        End While
        com1.Dispose()
        con.Close()
        hidcolname.Value = columnname
        hidtabname.Value = tablename



        If b = False Then
            Dim asstr = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
            asstr = Replace(asstr, "@Date1@", txtFromdate.Text)
            asstr = Replace(asstr, "@Date2@", txtTodate.Text)
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
            totalcol.Value = repcolumn
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
                        da = New SqlDataAdapter("select * from " + currenttable + "", con1)
                        con1.Open()
                        da.Fill(ds)
                        con1.Close()
                    Catch ex As Exception

                        aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
                        ddlReport.Visible = True
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
                asstring = Replace(asstring, "@Date1@", txtFromdate.Text)
                asstring = Replace(asstring, "@Date2@", txtTodate.Text)
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
                totalcol.Value = repcolumn
                repcols.DataSource = repcolarray
                repcols.DataBind()
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                ddlReport.Visible = True
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

        hidorder.Value = ""
        hidgroup.Value = ""
        hidhaving.Value = ""
        hidwheretxt.Value = ""
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
        Dim cmd11 As SqlCommand
        If txtCurrentReport.Text = "" Then
            cmd11 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "'  and  departmentid='" + DepartmentName.SelectedValue + "'  and  clientid='" + clientAna + "'  and  underlob='" + lobana + "'", con)
        Else
            cmd11 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
        End If

        con.Open()
        tablename = cmd11.ExecuteScalar().ToString()
        tablename = tablename.Replace("~", ",")
        tablearray = tablename.Split(",")
        hidtablename.Value = tablename
        'com1.Dispose()


        reotablename.Value = tablename
        tabcount = UBound(tablearray)
        con.Close()
        'readquery.Close()
        If txtCurrentReport.Text = "" Then
            com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "' and  departmentid='" + DepartmentName.SelectedValue + "' and  clientid='" + clientAna + "' and  underlob='" + lobana + "'", con)
        Else
            If hidDept = "" Then
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            Else
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "' and  departmentid='" + hidDept + "' and  clientid='" + hidClient + "' and  underlob='" + hidlob + "'", con)
            End If

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
                If bnm.contains("$As$") = True Or bnm.contains("String.fromCharCode(34)") = True Or bnm.contains("$+$") = True Or bnm.contains("$as$") = True Then
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
            datestr = strnorcol
            datestr = Replace(datestr, "@Date1@", txtFromdate.Text)
            datestr = Replace(datestr, "@Date2@", txtTodate.Text)
            Normalarray.Value = datestr
            If strfcol <> "" Then
                strfcol = strfcol.Replace("$", ".")
                strfcol = strfcol.Replace("AS", "%")
                Dim datestr1 As String
                datestr1 = strfcol
                datestr1 = Replace(datestr1, "@Date1@", txtFromdate.Text)
                datestr1 = Replace(datestr1, "@Date2@", txtTodate.Text)
                formulaarray.Value = datestr1
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
            If Date1 = True Then
                If txtTodate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill TO DATE")
                    Exit Sub
                End If
            End If
            If Date2 = True Then
                If txtFromdate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill From DATE")

                    Exit Sub
                End If
            End If

            colname = strspace

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

        If astr.Contains("@Date1@") = False And astr.Contains("@Date2@") = False And txtFromdate.Text <> "" And txtTodate.Text <> "" Then
            wheretxt = "where" & " " & formula
            Dim ftext = tablearray(0) + ".date" + " " + "between" + " " + "'" + txtFromdate.Text + "'" + " " + "and" + " " + "'" + txtTodate.Text + "'"
            If formula <> "" Then
                wheretxt = " where " + ftext
            Else
                wheretxt = "where" & " " & ftext
            End If

            hidwheretxt.Value = wheretxt
            Session("hidwheretxt") = wheretxt


        ElseIf astr.Contains("@Date1@") = True And astr.Contains("@Date2@") = True Then

            Date11 = astr.Contains("@Date1@")
            Date22 = astr.Contains("@Date2@")
            If Date11 = True Then
                If txtTodate.Text = "" Then
                    aspnet_msgbox(" Plz Fill To Date")
                    Exit Sub
                End If
            End If
            If Date22 = True Then
                If txtFromdate.Text = "" Then
                    aspnet_msgbox("Fill From Date")
                    Exit Sub
                End If
            End If
            If formula <> "" Then
                wheretxt = "where" & " " & formula
                formula = formula.Replace("'@Date1@'", "'" + txtFromdate.Text + "'")
                formula = formula.Replace("'@Date2@'", "'" + txtTodate.Text + "'")
                wheretxt = "where" & " " & formula
                wheretxt = wheretxt.Replace("$", ".")
                hidwheretxt.Value = wheretxt
                Session("hidwheretxt") = wheretxt

            End If

        Else
            wheretxt = "where" & " " & formula
            wheretxt = wheretxt.Replace("$", ".")
            hidwheretxt.Value = wheretxt
            Session("hidwheretxt") = wheretxt

        End If
        If formula = "" And txtFromdate.Text = "" And txtTodate.Text = "" Then
            wheretxt = ""
            hidwheretxt.Value = ""
            Session("hidwheretxt") = wheretxt

        End If
        If groupby <> "" Then
            groupbytext = "group by" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")
            hidgroup.Value = groupbytext

            Session("hidgroupby") = groupbytext

        Else
            groupbytext = ""
            Session("hidgroupby") = groupbytext
        End If
        If orderby <> "" Then
            orderbytext = "order by" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            hidorder.Value = orderbytext
            Session("hidordertxt") = orderbytext

        Else
            orderbytext = ""
            Session("hidordertxt") = orderbytext
        End If
        If havingcondition <> "" Then
            havingtxt = "having" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            hidhaving.Value = havingtxt
            Session("hidhavingtxt") = havingtxt
        Else
            havingtxt = ""
            Session("hidhavingtxt") = havingtxt
        End If
        con.Close()
        readquery.Close()
        If txtCurrentReport.Text <> "" Then
            com1 = New SqlCommand("select DepartmentId,clientid,underlob from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            con.Open()
            readquery = com1.ExecuteReader
            While readquery.Read()
                reportDept = readquery("DepartmentId")
                reportclient = readquery("clientid")
                reportlob = readquery("underlob")
            End While
            Currentrepdept.Value = reportDept
            Currentrepclient.Value = reportclient
            Currentreplob.Value = reportlob
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
        hidcolumname.Value = columnname
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
            If txtCurrentReport.Text = "" Then
                repName = ddlReport.SelectedItem.Text
                Session("repname") = repName
                If data("name") = "tab" & ddlReport.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            Else
                repName = txtCurrentReport.Text
                Session("repname") = repName
                ddlReport.Visible = False
                If data("name") = "tab" & txtCurrentReport.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            End If


        End While
        com1.Dispose()
        con.Close()
        hidcolname.Value = columnname
        hidtabname.Value = tablename



        If b = False Then
            Dim asstr = "select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext
            asstr = Replace(asstr, "@Date1@", txtFromdate.Text)
            asstr = Replace(asstr, "@Date2@", txtTodate.Text)
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
            totalcol.Value = repcolumn
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
                        da = New SqlDataAdapter("select * from " + currenttable + "", con1)
                        con1.Open()
                        da.Fill(ds)
                        con1.Close()
                    Catch ex As Exception

                        aspnet_msgbox(currenttable & " " & "Table Does Not Exist")
                        ddlReport.Visible = True
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
                asstring = Replace(asstring, "@Date1@", txtFromdate.Text)
                asstring = Replace(asstring, "@Date2@", txtTodate.Text)
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
                totalcol.Value = repcolumn
                repcols.DataSource = repcolarray
                repcols.DataBind()
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                ddlReport.Visible = True
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

        hidorder.Value = ""
        hidgroup.Value = ""
        hidhaving.Value = ""
        hidwheretxt.Value = ""
    End Sub
    Public Sub chartseries()
        If rbnRow.Checked = True Then
            seriesbtn.Value = "Row"
        ElseIf rbnColumn.Checked = True Then
            seriesbtn.Value = "Column"
        End If
        Session("graphtype") = seriesbtn.Value
    End Sub
    ''' <summary>
    ''' Multiple Chart Creation
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub makechart1()
        Try
            If ddlgraphname.Visible = True Then
                lblgraphname.Visible = True

                ddlgraphname.Visible = True
            Else
                lblgraphname.Visible = False
                ddlgraphname.Visible = False
            End If

            Chart2.Visible = False
            StockChart.Visible = False
            dupcol = ""
            blankravl = 0
            'If txtCurrentReport.Text = "" Then
            '    If ddlgraphname.Visible = True Then
            '        If DepartmentName.SelectedIndex = 0 Then
            '            aspnet_msgbox("Please Select Department.")
            '            Exit Sub
            '        End If
            '    End If
            'End If
            'If Selectreport.Visible = True Then
            '    If txtCurrentReport.Text = "" Then

            'If DepartmentName.SelectedIndex = 0 Then
            '    aspnet_msgbox("Please Select Department.")
            '    Exit Sub
            'End If
            If repcols.Items.Count = 0 Then
                aspnet_msgbox("Please Select Report Columns.")
                Exit Sub
            End If
            If selectedcols.Items.Count = 0 Then
                aspnet_msgbox("Please Select Report Columns.")
                Exit Sub
            End If
            If selectedcols.Items.Count = 1 Then
                aspnet_msgbox("Please Select At Least Two Report Columns.")
                Exit Sub
            End If
            If hidChart.Value = "Histogram" And rbnColumn.Checked = False Then
                aspnet_msgbox("Please Select Chart Series")
                Exit Sub
            End If
            If hidChart.Value = "Run" And rbnColumn.Checked = False Then
                aspnet_msgbox("Please Select Chart Series")
                Exit Sub
            End If
            If txtCurrentReport.Text <> "" Then
                If ddlgraphname.Items.Count <> 0 Then
                    lblgraphname.Visible = True
                    ddlgraphname.Visible = True
                End If

                If repcols.Items.Count = 0 Then
                    aspnet_msgbox("Please Select Report Columns.")
                    Exit Sub
                End If
                If selectedcols.Items.Count = 0 Then
                    aspnet_msgbox("Please Select Report Columns.")
                    Exit Sub
                End If
                If selectedcols.Items.Count = 1 Then
                    aspnet_msgbox("Please Select At Least Two Report Columns.")
                    Exit Sub
                End If
                If hidChart.Value = "Histogram" And rbnColumn.Checked = False Then
                    aspnet_msgbox("Please Select Chart Series")
                    Exit Sub
                End If
                If hidChart.Value = "Run" And rbnColumn.Checked = False Then
                    aspnet_msgbox("Please Select Chart Series")
                    Exit Sub
                End If
            End If
            Dim r, m, j As Integer
            r = selectedcols.Items.Count
            Dim str4 As String = ""
            str4 = Session("graphType")
            For m = 0 To r - 1
                If x = "" Then
                    x = selectedcols.Items(m).Text
                Else
                    x = x & "," & selectedcols.Items(m).Text
                End If
            Next
            x = UCase(x)
            openselectedcolumn.Value = x
            If chkSunnarized.Checked = True Then
                RepGraphtype.Value = "Summarized"
            Else
                RepGraphtype.Value = "Normal"
            End If
            Dim sp As String()
            sp = x.Split(",")
            j = UBound(sp)
            ' Initialize a connection string	
            Dim myConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
            ' Define the database query	
            Dim mySelectQuery, orderstr As String
            Dim orderarr
            orderstr = hidtabname.Value
            orderarr = orderstr.Split(",")
            Dim strgroup As String

            If chkSunnarized.Checked = False Then
                Dim norll, norjj As Integer
                Dim nornum, nornum1, nornum2 As Integer
                Dim norstrvalue, norfinalformula, norstre, norb, norfmname As String
                norstre = Normalarray.Value
                Dim ranjitchk As String = ""
                Dim norstrfinalfor, norsumcol As String
                norformulaarr = norstre.Split(";")
                For norll = 0 To norformulaarr.length - 1
                    norstrvalue = norformulaarr(norll)
                    norstrvalue = UCase(norstrvalue)
                    For norjj = 0 To sp.Length - 1
                        'If norstrvalue.Contains("(") Or norstrvalue.Contains(")") Or norstrvalue.Contains("+") Or norstrvalue.Contains("-") Or norstrvalue.Contains("*") Or norstrvalue.Contains("/") Then
                        If LCase(norstrvalue).Contains("max(") Or LCase(norstrvalue).Contains("min(") Or LCase(norstrvalue).Contains("count(") Or LCase(norstrvalue).Contains("sum(") Or LCase(norstrvalue).Contains("avg(") Then

                            nornum1 = norstrvalue.LastIndexOf("%")
                            nornum2 = norstrvalue.Length
                            nornum = nornum2 - nornum1
                            norfinalformula = norstrvalue.Substring(nornum1 + 1, nornum - 1)
                            norfinalformula = norfinalformula.Trim(" ")
                            norfinalformula = norfinalformula.Replace("[", "")
                            norfinalformula = norfinalformula.Replace("]", "")
                            norfinalformula = norfinalformula.Trim(" ")
                            If sp(norjj) = norfinalformula Then

                                If norsumcol = "" Then
                                    norsumcol = norstrvalue.Replace("%", "AS")
                                    norstrfinalfor = norfinalformula
                                Else
                                    norsumcol = norsumcol + "," + norstrvalue.Replace("%", "AS")
                                    norstrfinalfor = norstrfinalfor + "," + norfinalformula
                                End If
                            End If
                        Else
                            Dim nornum12 As Integer
                            If norstrvalue.Contains("%") Then
                                nornum12 = norstrvalue.LastIndexOf("%")
                            Else
                                nornum12 = norstrvalue.LastIndexOf(".")
                            End If

                            Dim nornum13 As Integer = norstrvalue.Length
                            Dim nornum14 As Integer = nornum13 - nornum12
                            norfinalformula = norstrvalue.Substring(nornum12 + 1, nornum14 - 1)
                            norfinalformula = norfinalformula.Trim(" ")
                            norfinalformula = norfinalformula.Replace("[", "")
                            norfinalformula = norfinalformula.Replace("]", "")
                            norfinalformula = norfinalformula.Trim(" ")
                            If sp(norjj) = norfinalformula Then

                                If norb = "" Then

                                    norb = norstrvalue.Replace("%", "AS")


                                    norfmname = norfinalformula
                                Else
                                    norb = norb + "," + norstrvalue.Replace("%", "AS")


                                    norfmname = norfmname + "," + norfinalformula
                                End If
                            End If
                        End If

                    Next
                Next
                Dim nowgpy As String = ""
                Dim norcol As String
                Dim grpby = ""
                Dim ranjitmadegpby As String()


                If norb = "" Then
                    norb = sp(0)
                End If
                ranjitmadegpby = norb.Split(",")



                If LCase(ranjitmadegpby(0)).Contains(" as ") Then
                    Dim aajao As String = ranjitmadegpby(0).Replace(" AS ", "$")
                    Dim ranamade As String() = aajao.Split("$")
                    nowgpy = ranamade(0)
                Else
                    nowgpy = ranjitmadegpby(0)
                End If





                If norstrfinalfor = "" Then
                    norcol = norb
                ElseIf norfmname = "" Then
                    norcol = norsumcol
                Else
                    norcol = norb + "," + norsumcol
                End If

                'rana code start
                Dim gpdatabase As String = ""
                If hidgroup.Value <> "" Then
                    gpdatabase = hidgroup.Value.Remove(0, 9)

                    Dim splt = gpdatabase.Split(",")
                    Dim stst As Integer = 0
                    For stst = 0 To UBound(splt)
                        Dim nowda As String = LCase(splt(stst))

                        If LCase(norb).Contains(nowda) Then
                            If grpby = "" Then
                                'Gopal Changes - Added Start bracket
                                grpby = "group by " + nowda
                            Else
                                grpby = grpby + "," + nowda

                            End If

                        End If

                    Next

                    ''Gopal Changes
                    'If grpby <> "" Then
                    '    grpby = grpby + ")"
                    'End If
                    ''End Gopal Changes

                Else
                    'smithachanges
                    grpby = "group by " + norcol
                    grpby = ""

                    'smithachanges
                End If


                'stope and below was commented once
                'If norb = "" Then
                '    grpby = ""
                '    If hidgroup.Value <> "" Then
                '        grpby = hidgroup.Value
                '    End If
                'Else
                '    'ranjit changed start
                '    'Dim meracode As String = ""
                '    'If norb.Contains("AS") Then
                '    '    meracode = norb.Replace("AS", "^")
                '    'End If
                '    'Dim commacheck As String()
                '    ''If meracode.Contains(",") Then
                '    'commacheck = meracode.Split(",")
                '    ''End If
                '    'Dim jkl As Integer = 0
                '    'Dim madegp As String = ""
                '    'For jkl = 0 To UBound(commacheck)




                '    '    If meracode <> "" Then

                '    '        Dim codecomma As String = commacheck(jkl)
                '    '        Dim changedd As String() = codecomma.Split("^")
                '    '        Dim stloop As Integer = 0
                '    '        Dim lenloop As Integer = UBound(changedd)

                '    '        For stloop = 0 To lenloop


                '    '            If madegp = "" Then
                '    '                madegp = changedd(stloop)
                '    '            Else
                '    '                madegp = madegp + "," + changedd(stloop)
                '    '            End If
                '    '            stloop = stloop + 1
                '    '        Next
                '    '        grpby = " Group by" + " " + madegp
                '    '    Else
                '    '        'ranjit commented before change this whole code was containing only this below line
                '    '        'If norb <> "" Then
                '    grpby = " Group by" + " " + norb
                '    '        'End If

                '    '    End If
                '    'Next
                '    ' yahan tak
                'End If
                Dim hv = ""

                'ranjit changed for groupby
                'mySelectQuery = "select " + norcol + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " " + hidhaving.Value + " order by " & orderarr(0) + "." + sp(0) + ""

                mySelectQuery = "select " + norcol + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " " + hidhaving.Value + "Order By " & nowgpy
                mySelectQuery = UCase(mySelectQuery)

            ElseIf chkSunnarized.Checked = True Then

                If hidgroup.Value = "" Then
                    aspnet_msgbox("This is not Summarized Report")
                    Exit Sub
                End If
                Dim ll, jj As Integer
                Dim num, num1, num2 As Integer
                Dim strvalue, finalformula, stre As String
                stre = formulaarray.Value
                Dim ordermaderana As String() = stre.Split(";")
                Dim countall As Integer = 0
                Dim againorder As String()
                For countall = 0 To UBound(ordermaderana)
                    Dim udata As String = UCase(ordermaderana(countall))
                    Dim alltabdata As Integer = 0
                    For alltabdata = 0 To UBound(orderarr)
                        Dim utab As String = UCase(orderarr(alltabdata))
                        Dim ucol As String = "[" + UCase(sp(0)) + "]"
                        If udata.Contains(utab) And udata.Contains(ucol) Then
                            againorder = ordermaderana(countall).Split("%")

                        End If
                    Next

                Next
                'Dim ordsdd As String = ordermaderana(0)
                'Dim againorder As String() = ordsdd.Split("%")
                If stre = "" Then
                    aspnet_msgbox("This is not valid data for Summarized Report ")
                    Exit Sub
                End If
                Dim strfinalfor, sumcol As String
                txtformulaarr = stre.Split(";")
                For ll = 0 To txtformulaarr.length - 1
                    strvalue = txtformulaarr(ll)
                    strvalue = UCase(strvalue)
                    For jj = 0 To sp.Length - 1
                        If strvalue.Contains("(") Or strvalue.Contains(")") Or strvalue.Contains("+") Or strvalue.Contains("-") Or strvalue.Contains("*") Or strvalue.Contains("/") Then
                            num1 = strvalue.LastIndexOf("%")
                            num2 = strvalue.Length
                            num = num2 - num1
                            finalformula = strvalue.Substring(num1 + 1, num - 1)
                            finalformula = finalformula.Trim(" ")
                            finalformula = finalformula.Replace("[", "")
                            finalformula = finalformula.Replace("]", "")
                            finalformula = finalformula.Trim(" ")
                            If sp(jj) = finalformula Then

                                If sumcol = "" Then
                                    sumcol = strvalue.Replace("%", "AS")
                                    strfinalfor = finalformula
                                Else
                                    sumcol = sumcol + "," + strvalue.Replace("%", "AS")
                                    strfinalfor = strfinalfor + "," + finalformula
                                End If
                            End If
                        Else
                            Dim num12 As Integer
                            If strvalue.Contains("%") Then
                                num12 = strvalue.LastIndexOf("%")
                            Else
                                num12 = strvalue.LastIndexOf(".")
                            End If

                            Dim num13 As Integer = strvalue.Length
                            Dim num14 As Integer = num13 - num12
                            finalformula = strvalue.Substring(num12 + 1, num14 - 1)
                            finalformula = finalformula.Trim(" ")
                            finalformula = finalformula.Replace("[", "")
                            finalformula = finalformula.Replace("]", "")
                            finalformula = finalformula.Trim(" ")
                            If sp(jj) = finalformula Then
                                strvalue = Replace(strvalue, "%", "AS")
                                Dim oplk = Split(strvalue, " AS ")
                                If oplk.Length > 1 Then
                                    If b = "" Then
                                        b = oplk(0)
                                        fmname = finalformula
                                    Else
                                        b = b + "," + oplk(0)
                                        fmname = fmname + "," + finalformula
                                    End If
                                Else
                                    If b = "" Then
                                        b = strvalue
                                        fmname = finalformula
                                    Else
                                        b = b + "," + strvalue
                                        fmname = fmname + "," + finalformula
                                    End If
                                End If

                            End If
                        End If

                    Next
                Next
                y = reotablename.Value
                Dim grpby = ""
                If b = "" Then
                    grpby = ""

                Else
                    grpby = " Group by" + " " + b
                End If
                If strfinalfor = "" Then
                    m1 = b
                ElseIf b = "" Then
                    m1 = sumcol
                    strgroup = ""
                Else
                    m1 = b + "," + sumcol
                End If
                'ranjit commented its original now
                mySelectQuery = "select " + m1 + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " order by " & orderarr(0) + "." + sp(0) + ""
                'mySelectQuery = "select " + m1 + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " order by " & againorder(0)
                '+ "." + sp(0) + ""
                mySelectQuery = UCase(mySelectQuery)
            End If
            Dim drillstr As String
            Session("xvalue") = x
            Session("yvalue") = y
            drillstr = "select " + x + " from " + y + " "
            Session("drillquery") = drillstr
            ' Create a database connection object using the connection string	
            Dim myConnection As New SqlConnection(myConnectionString)
            ' Create a database command on the connection using query	
            Dim myCommand As New SqlCommand(mySelectQuery, myConnection)
            ' Open the connection	
            myCommand.Connection.Open()
            ' Initializes a new instance of the OleDbDataAdapter class
            Dim myDataAdapter As New SqlDataAdapter
            myDataAdapter.SelectCommand = myCommand
            ' Initializes a new instance of the DataSet class
            Dim myDataSet As New DataSet()
            ' Adds rows in the DataSet
            myDataAdapter.Fill(myDataSet, "Query")
            'smithachangesstart
            Dim myDataSetcount As Integer = myDataSet.Tables(0).Columns.Count
            Dim myDataSetcountstart As Integer = 0
            Dim datasetcolumntype As String = ""
            Dim datasetcolumnname As String = ""
            Dim positionasstring As String = ""
            Dim positionasint As String = ""
            Dim var As Integer
            Dim newdataset As String = ""
            Dim ramstr As String = ""
            For myDataSetcountstart = 0 To myDataSetcount - 1
                datasetcolumntype = myDataSet.Tables("Query").Columns(myDataSetcountstart).DataType.ToString
                datasetcolumnname = myDataSet.Tables("Query").Columns(myDataSetcountstart).ColumnName
                ramstr = ""
                If LCase(datasetcolumntype).Contains("string") Or LCase(datasetcolumntype).Contains("datetime") Then
                    Try


                        var = CInt(myDataSet.Tables("Query").Rows(0)(datasetcolumnname))
                    Catch ex As Exception
                        ramstr = "no"

                        If positionasstring = "" Then
                            positionasstring = datasetcolumnname
                        Else
                            positionasstring = positionasstring + "," + datasetcolumnname
                        End If



                    End Try
                    If ramstr = "" Then
                        If positionasint = "" Then
                            positionasint = datasetcolumnname
                        Else
                            positionasint = positionasint + "," + datasetcolumnname
                        End If
                    End If

                Else
                    If positionasint = "" Then
                        positionasint = datasetcolumnname
                    Else
                        positionasint = positionasint + "," + datasetcolumnname
                    End If


                End If

            Next

            If positionasstring = "" Then
                newdataset = positionasint
            ElseIf positionasint = "" Then
                newdataset = positionasstring

            Else
                newdataset = positionasstring + "," + positionasint
            End If

            Dim datasetarray As String() = newdataset.Split(",")
            Dim datasetnew As New DataSet
            Dim newdatatable As New DataTable

            Dim newdatacolumn As New DataColumn
            Dim newdatarow As DataRow
            Dim dtarow As DataRow
            For myDataSetcountstart = 0 To UBound(datasetarray)
                newdatacolumn = New DataColumn(datasetarray(myDataSetcountstart))

                newdatatable.Columns.Add(newdatacolumn)
            Next

            For Each dtarow In myDataSet.Tables("Query").Rows
                newdatarow = newdatatable.NewRow

                For myDataSetcountstart = 0 To UBound(datasetarray)

                    newdatarow.Item(datasetarray(myDataSetcountstart)) = dtarow(datasetarray(myDataSetcountstart))

                Next
                newdatatable.Rows.Add(newdatarow)

            Next
            myDataSet.Clear()
            myDataSet.Dispose()
            myDataSet.Tables.Remove("Query")
            'myDataSet.Tables.Add("Query", newdatatable.Namespace)
            myDataSet.Tables.Add(newdatatable)


            'stop

            myCommand.Connection.Close()
            '***********************************************************************************
            ' Set Column chart
            If hidChart.Value = "" Then
                hidChart.Value = "Column"
                showchart()
            End If
            If hidChart.Value = "Column" Then
                Chart1.Visible = True
                Chart2.Visible = False
                'Select Row  series data
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Double
                    Dim sval As String
                    Dim str As String
                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Dim ij As Integer = 0
                            Try

                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                    End If
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        str = ""
                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim YVal As Double

                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Dim series As Series = Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Column
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String

                        Dim bb As String = ""
                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                nodesign = ""

                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)


                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label3
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label3:
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False

                    Next row
                    GoTo label2
                End If

                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, columncount1, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim counter As Integer
                    Dim strcol As String
                    counter = 0
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                        colintval = 0
                                        colarr(columncount) = cc
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                        colarr(columncount) = cc
                                    End If

                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        Dim YVal1 As Double

                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Dim series As Series = Chart1.Series.Add(strcol)
                                Dim Datapoint As DataPoint

                                Chart1.Series(strcol).Type = SeriesChartType.Column
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim arrcolumn(10) As String
                                Dim arryval1(20) As Double

                                xval = ""
                                Try
                                    'For columncount1 = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        'columnseries = row1(0)
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.ShowEndLabels = True
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                End If
                GoTo label2
                'Set Run chart
            ElseIf hidChart.Value = "Run" Then
                Chart2.Visible = False
                Chart1.Visible = True

                Dim aSubGroup(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim aData(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim st1 As String = ""
                Dim st2 As String = ""
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    GoTo label1
                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer
                        Dim strcol As String
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Single
                                xval = ""
                                Try
                                    Dim runi As Integer = 0
                                    Dim colmnjmn As DataColumn
                                    Dim data As Single
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        columnseries = row1(0).ToString()

                                        Try
                                            If IsDBNull(row1(0)) Then
                                            Else
                                                YVal1 = CSng(row1(0))
                                            End If
                                            If IsDBNull(row1(1)) Then
                                            Else
                                                data = CSng(row1(1))
                                            End If

                                            aData(runi) = data
                                            aSubGroup(runi) = YVal1
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)
                                            End If

                                        End Try
                                        runi = runi + 1
                                    Next
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try

                        Dim Run_Chart As Run_Chart = New Run_Chart()
                        ' Create a C-Chart of the data and plot it on the chart. 
                        '

                        ' Optionally before calling any chart creation function, you can setup styles for 
                        ' Control Lines. 



                        Run_Chart.UCLstyle.LineStyle = ChartDashStyle.Solid
                        Run_Chart.UCLstyle.LineColor = Color.Red
                        Run_Chart.UCLstyle.LineWidth = 2
                        Run_Chart.UCLstyle.ShowText = True


                        ' Also you can set style for text 
                        ' Run_Chart.ShowText = True
                        Run_Chart.LCLstyle.TextColor = Color.Blue
                        Run_Chart.LCLstyle.TextFont = New Font("Arial", 10)
                        Run_Chart.LCLstyle.ShowText = True

                        'Run_Chart.AutoFitLines = True

                        Dim tmpSeries As Series = Run_Chart.CreateSeries(aSubGroup, aData, Chart1)

                        'Chart1.ChartAreas("Chart Area 1").AxisX.LabelsAutoFit = False

                        tmpSeries.ShowLabelAsValue = True
                        tmpSeries.SmartLabels.Enabled = True
                        tmpSeries.SmartLabels.MovingDirection = LabelAlignment.Top
                        tmpSeries.SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Round
                        tmpSeries.SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No
                        tmpSeries.SmartLabels.CalloutStyle = LabelCalloutStyle.Box
                        tmpSeries.SmartLabels.HideOverlapped = False
                        tmpSeries.SmartLabels.CalloutBackColor = Color.Bisque
                        GoTo label2
                    Next columncount
                End If
            ElseIf hidChart.Value = "Pareto" Then
                Chart2.Visible = False
                Dim strcol As String
                Chart1.Visible = True
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Column
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddY(YVal1)
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)

                                            End If
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    MakeParetoChart(Chart1, strcol, "Pareto")
                    Chart1.Series("Pareto").Type = SeriesChartType.Line
                    Chart1.Series("Pareto").ShowLabelAsValue = True
                    Chart1.Series("Pareto").MarkerColor = Color.Red
                    Chart1.Series("Pareto").MarkerBorderColor = Color.MidnightBlue
                    Chart1.Series("Pareto").MarkerStyle = MarkerStyle.Circle
                    Chart1.Series("Pareto").MarkerSize = 8
                    Chart1.Series("Pareto").LabelFormat = "0.#"
                    ' format with one decimal and leading zero 
                    ' Set Color of line Pareto chart 
                    Chart1.Series("Pareto").Color = Color.Aquamarine
                    GoTo label2
                End If

                'Set Histogram chart
            ElseIf hidChart.Value = "Histogram" Then

                Chart2.Titles(0).Text = txtCharttitle.Text
                Chart2.ChartAreas("HistogramArea").AxisX.Title = txtTitleext.Text
                Chart2.ChartAreas("HistogramArea").AxisY.Title = txtYTitle.Text
                Dim strcol As String = ""
                Chart1.Visible = False
                Chart2.Visible = True
                Chart2.Width = 1000
                Chart2.Height = 500

                'Chart2.ChartAreas("HistogramArea").Position.Auto = False

                Chart2.ChartAreas("Default").Position.Y = 10
                Chart2.ChartAreas("Default").Position.X = 5
                Chart2.ChartAreas("Default").Position.Height = 7
                Chart2.ChartAreas("HistogramArea").Position.X = 5 'Single.Parse(txtX1.Text)
                Chart2.ChartAreas("HistogramArea").Position.Y = Chart2.ChartAreas("Default").Position.Height + 10 'Single.Parse(txtY1.Text)
                'Chart2.ChartAreas("HistogramArea").Position.Width = 80 'Single.Parse(txtWidth1.Text)
                'Chart2.ChartAreas("HistogramArea").Position.Height = 80 'Single.Parse(txtHeight1.Text)

                ''Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Auto = False
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.X = 10
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Y = 10
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Width = 80 'Single.Parse(txtWidth2.Text)
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Height = 80 'Single.Parse(txtHeight2.Text)

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart2.Series.Add(strcol)
                                Chart2.Series(strcol).Type = SeriesChartType.Column
                                Chart2.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart2.Series(strcol).Points.AddY(YVal1)
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)

                                            End If
                                        End Try





                                        Chart2.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart2.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart2.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart2.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart2.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart2.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        'smitha
                                        'Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes
                                        'smitha
                                        Chart2.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart2.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart2.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                    'Dim maxValuePoint As DataPoint = Chart2.Series(strcol).Points.FindMaxValue()
                    'Dim b As Integer
                    'b = (maxValuePoint)
                    ' Chart2.Series(seriesName).Points.AddY(maxValuePoint.m)
                    ' Dim minValuePoint As DataPoint = Chart2.Series(strcol).Points.FindMinValue()
                    'Chart2.Series(seriesName).Points.AddY(minValuePoint)
                    ' Populate single axis data distribution series. Show Y value of the 
                    ' data series as X value and set all Y values to 1. 
                    For Each dataPoint As DataPoint In Chart2.Series(strcol).Points
                        'Chart2.Series.Add(DataDistribution)
                        Chart2.Series("DataDistribution").Points.AddXY(dataPoint.YValues(0), 1)
                    Next
                    ' Create a histogram series 
                    Dim histogramHelper As New HistogramChartHelper()
                    'histogramHelper.SegmentIntervalNumber = Integer.Parse(CollectedPercentage.SelectedItem.Text)
                    'histogramHelper.ShowPercentOnSecondaryYAxis = checkBoxPercent.Checked
                    ' NOTE: Interval width may be specified instead of interval number 
                    'histogramHelper.SegmentIntervalWidth = 15; 
                    histogramHelper.CreateHistogram(Chart2, strcol, "Histogram")

                    ' Set same X axis scale and interval in the single axis data distribution 
                    ' chart area as in the histogram chart area. 
                    Chart2.ChartAreas("Default").AxisX.Minimum = Chart2.ChartAreas("HistogramArea").AxisX.Minimum
                    Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("HistogramArea").AxisX.Maximum
                    Chart2.ChartAreas("Default").AxisX.Interval = Chart2.ChartAreas("HistogramArea").AxisX.Interval
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Pie" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    Dim dup As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String


                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval

                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        ' arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String
                        If str <> "" Then
                            GoTo label2
                        End If

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)

                        Chart1.Series(str).Type = SeriesChartType.Pie
                        Chart1.Series(str).Color = Color.Blue
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double
                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label4
                                End If
                                If chk_hide.Checked = True Then
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    piehelper.CollectedPercentage = Double.Parse("16")
                                    '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                    '// where original and supplemental pie charts should be placed.
                                    piehelper.SupplementedAreaSizeRatio = 1.1F
                                    piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                    '// Show supplemental pie for the "Default" series
                                    piehelper.ShowSmallSegmentsAsSupplementalPie(str)
                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    Chart1.Series(str).LabelBackColor = Color.Empty
                                End If
                                'Chart1.Series(strcol).ShowLabelAsValue = True
                                'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                Chart1.Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label4:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval, strcol As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    piehelper = New Piecollected(Chart1)
                    piehelper.CollectedLabel = String.Empty
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Pie
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                Dim counter As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1

                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        'Chart1.ChartAreas("Chart Area 1").Position.Auto = True
                                        If chk_hide.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            piehelper.CollectedPercentage = Double.Parse("16")
                                            '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                            '// where original and supplemental pie charts should be placed.
                                            piehelper.SupplementedAreaSizeRatio = 1.1F
                                            piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                            '// Show supplemental pie for the "Default" series
                                            piehelper.ShowSmallSegmentsAsSupplementalPie(strcol)
                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            Chart1.Series(strcol).LabelBackColor = Color.Empty
                                        End If
                                        'Chart1.Series(strcol).ShowLabelAsValue = True
                                        'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                        Chart1.Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                    GoTo label2

                End If
            ElseIf hidChart.Value = "Area" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String
                    Dim ij As Integer = 0

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            ij = 0
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Area
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label5
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label5:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False

                    Next row
                    GoTo label2
                End If

                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim strcol As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Area
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try

                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Bar" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        '  GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Bar
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label6
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                'Chart1.ChartAreas("Chart Area 1").AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount
                                ' Enabling the SmartLabels™ attribute.
                                Chart1.Series(str).SmartLabels.Enabled = True

                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Dash

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight

                                ' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 60

                                ' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 160

                                ' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No

                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label6:
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Bar
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        ' Use point index for drawing the chart 
                                        '  
                                        'hrt1.Series(strcol).XValueIndexed = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Daughnt" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Doughnut
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label7
                                End If

                                If chk_hide.Checked = True Then
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    piehelper.CollectedPercentage = Double.Parse("16")
                                    '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                    '// where original and supplemental pie charts should be placed.
                                    piehelper.SupplementedAreaSizeRatio = 1.1F
                                    piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                    '// Show supplemental pie for the "Default" series
                                    piehelper.ShowSmallSegmentsAsSupplementalPie(str)
                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    Chart1.Series(str).LabelBackColor = Color.Empty
                                End If
                                'Chart1.Series(strcol).ShowLabelAsValue = True
                                'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                Chart1.Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
Label7:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String

                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Doughnut
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        If chk_hide.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            piehelper.CollectedPercentage = Double.Parse("16")
                                            '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                            '// where original and supplemental pie charts should be placed.
                                            piehelper.SupplementedAreaSizeRatio = 1.1F
                                            piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                            '// Show supplemental pie for the "Default" series
                                            piehelper.ShowSmallSegmentsAsSupplementalPie(strcol)
                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            Chart1.Series(strcol).LabelBackColor = Color.Empty
                                        End If
                                        'Chart1.Series(strcol).ShowLabelAsValue = True
                                        'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                        Chart1.Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Stock" Then

                'selet Column series data
                Chart2.Visible = False
                Chart1.Visible = False
                StockChart.Visible = True
                StockChart.Series.Clear()


                ''Create command object
                'Dim StockCmd As New SqlCommand(

                StockChart.DataSource = myDataSet.Tables(0)
                'Assign Serise
                Dim StockI As Integer

                StockChart.Height = 500
                StockChart.Width = 1000

                For StockI = 1 To myDataSet.Tables(0).Columns.Count - 1
                    StockChart.Series.Add(myDataSet.Tables(0).Columns(StockI).ColumnName).Type = SeriesChartType.Stock
                    'StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowLabelAsValue = True
                    'StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).AxisLabel
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowInLegend = True


                    'With (StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).SmartLabels)
                    '    .Enabled = True
                    '    .CalloutLineStyle = ChartDashStyle.Solid
                    'End With


                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).BorderWidth = (myDataSet.Tables(0).Columns.Count - StockI) * 4
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMembersY = myDataSet.Tables(0).Columns(StockI).ColumnName.ToString
                Next
                'StockChart.Series.Add("Series1").Type = SeriesChartType.Stock
                'StockChart.Series("Series1").BorderWidth = 10
                'StockChart.Series("Series1").ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                'StockChart.Series("Series1").ValueMembersY = myDataSet.Tables(0).Columns(1).ColumnName.ToString


                'StockChart.Series.Add("Series2").Type = SeriesChartType.Stock
                'StockChart.Series("Series2").BorderWidth = 1
                'StockChart.Series("Series2").ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                'StockChart.Series("Series2").ValueMembersY = myDataSet.Tables(0).Columns(2).ColumnName.ToString

                'StockChart.DataBind()

                '' ''If rbnColumn.Checked = True Then
                '' ''    Chart2.Visible = False
                '' ''    Chart1.Visible = False
                '' ''    StockChart.Visible = True
                '' ''    StockChart.Series.Clear()
                '' ''    Dim columncount, colintval As Integer
                '' ''    Dim cc, colseries, colstrval As String
                '' ''    Dim colarr(10) As String
                '' ''    Dim col As DataRow
                '' ''    'If myDataSet.Tables(0).Columns.Count < 4 Then
                '' ''    '    StockChart.Visible = False
                '' ''    '    GoTo label1
                '' ''    'End If
                '' ''    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                '' ''        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                '' ''        cc = colname
                '' ''        Try
                '' ''            For Each col In myDataSet.Tables(0).Rows
                '' ''                colseries = col(0).ToString()
                '' ''                Try
                '' ''                    If IsDBNull(col(colname)) Then
                '' ''                    Else
                '' ''                        colintval = CInt(col(colname))
                '' ''                        colarr(columncount) = cc
                '' ''                    End If

                '' ''                    Exit For
                '' ''                Catch ex As Exception
                '' ''                    If IsDBNull(col(colname)) Then
                '' ''                    Else
                '' ''                        colstrval = col(colname)
                '' ''                    End If


                '' ''                    StockChart.Visible = False
                '' ''                    GoTo label1
                '' ''                End Try
                '' ''            Next col
                '' ''        Catch ex As Exception

                '' ''        End Try
                '' ''        Dim yo As Integer
                '' ''        Dim strcol As String
                '' ''        Dim strnothing As String = ""
                '' ''        strcol = ""
                '' ''        For yo = 0 To colarr.Length - 1
                '' ''            If (colarr(yo) = "") Then
                '' ''                strnothing = colarr(yo)
                '' ''            ElseIf (colarr(yo) <> "") Then
                '' ''                strcol = ""
                '' ''                If (strcol = "") Then
                '' ''                    strcol = colarr(yo)
                '' ''                Else
                '' ''                    Exit For
                '' ''                End If
                '' ''            End If
                '' ''        Next yo
                '' ''        Try
                '' ''            If strcol = "" Then
                '' ''                Exit Try
                '' ''            Else
                '' ''                If strcol = Duplicate Then
                '' ''                    strcol = strcol + count.ToString
                '' ''                    count = count + 1
                '' ''                End If


                '' ''                StockChart.Series.Add(strcol)
                '' ''                StockChart.Series(strcol).Type = SeriesChartType.Stock
                '' ''                StockChart.Series(strcol).BorderWidth = 2
                '' ''                Dim st As String
                '' ''                st = strcol.Replace(count - 1, "")
                '' ''                Duplicate = st
                '' ''                Dim xval As String
                '' ''                Dim row1 As DataRow
                '' ''                Dim YVal1 As Double
                '' ''                xval = ""
                '' ''                Try
                '' ''                    For Each row1 In myDataSet.Tables(0).Rows
                '' ''                        ' for each row (row 1 and onward), add the value as a point
                '' ''                        columnseries = row1(0).ToString()
                '' ''                        Try
                '' ''                            If IsDBNull(row1(colname)) Then
                '' ''                            Else
                '' ''                                YVal1 = CInt(row1(colname))
                '' ''                            End If

                '' ''                            StockChart.Series(strcol).Points.AddXY(columnseries, YVal1)
                '' ''                        Catch ex As Exception
                '' ''                            If IsDBNull(row1(colname)) Then
                '' ''                            Else
                '' ''                                xval = row1(colname)
                '' ''                            End If

                '' ''                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                '' ''                        End Try

                '' ''                        ''Gopal stock
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelsAutoFit = False
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelStyle.Enabled = True
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelStyle 


                '' ''                        StockChart.Series(strcol).ShowLabelAsValue = True
                '' ''                        '' Enable SmartLabels.   
                '' ''                        StockChart.Series(strcol).SmartLabels.Enabled = True
                '' ''                        ' Set the callout style.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                '' ''                        ' Set the callout line color.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                '' ''                        ' Set the callout line style.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                '' ''                        ' Set the callout line width.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineWidth = 1

                '' ''                        ' Set the callout line anchor cap.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                '' ''                        ' Set the controlling moving directions.
                '' ''                        StockChart.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                '' ''                        '' Set the minimum distance that the labels can move.
                '' ''                        StockChart.Series(strcol).SmartLabels.MinMovingDistance = 100

                '' ''                        '' Set the minimum distance that the labels can move.
                '' ''                        StockChart.Series(strcol).SmartLabels.MaxMovingDistance = 100

                '' ''                        '' Allow the labels to be placed outside the plotting area.
                '' ''                        StockChart.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                '' ''                        StockChart.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                '' ''                        StockChart.Series(strcol).LegendToolTip = "Legend" + strcol
                '' ''                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                '' ''                        StockChart.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                '' ''                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                '' ''                        End If
                '' ''                    Next row1
                '' ''                Catch ex As Exception
                '' ''                End Try
                '' ''            End If
                '' ''        Catch ex As Exception
                '' ''        End Try
                '' ''    Next columncount
                '' ''    GoTo label2
                '' ''End If
            ElseIf hidChart.Value = "Line" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Line
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                nodesign = "no"
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label8
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label8:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Line
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2

                End If
            ElseIf hidChart.Value = "Scaterplot" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series

                        Dim ij As Integer
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Point
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label9
                                End If

                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label9:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim strcol As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            strcol = ""


                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else

                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)

                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1


                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                'If chekdupli = "" Then



                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                'End If
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""

                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception

                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If
            ElseIf hidChart.Value = "Scatter" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Point
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = ""
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = ""

                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label10
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label10:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    'colintval = CInt(col(colname))
                                    'colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    'colstrval = col(colname)
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If
            End If
label1:

label2:
            Try
                If hidChart.Value = "Stock" Then
                    stockfrormat()
                    StockChart.DataBind()
                Else

                    Commanchartformat()

                    Chart1.DataBind()
                End If
                myCommand.Dispose()
                myConnection.Close()
            Catch ex As Exception
                aspnet_msgbox(ex.Message)
            End Try
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try

    End Sub
    
    
    ''' <summary>
    ''' Comman Formate for Commanchart
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub makechart()
        Try
            If ddlgraphname.Visible = True Then
                lblgraphname.Visible = True

                ddlgraphname.Visible = True
            Else
                lblgraphname.Visible = False
                ddlgraphname.Visible = False
            End If

            Chart2.Visible = False
            StockChart.Visible = False
            dupcol = ""
            blankravl = 0
            If txtCurrentReport.Text = "" Then
                If ddlgraphname.Visible = True Then
                    If DepartmentName.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select Department.")
                        Exit Sub
                    End If
                End If
            End If
            If Selectreport.Visible = True Then
                If txtCurrentReport.Text = "" Then

                    If DepartmentName.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select Department.")
                        Exit Sub
                    End If
                    If repcols.Items.Count = 0 Then
                        aspnet_msgbox("Please Select Report Columns.")
                        Exit Sub
                    End If
                    If selectedcols.Items.Count = 0 Then
                        aspnet_msgbox("Please Select Report Columns.")
                        Exit Sub
                    End If
                    If selectedcols.Items.Count = 1 Then
                        aspnet_msgbox("Please Select At Least Two Report Columns.")
                        Exit Sub
                    End If
                    If hidChart.Value = "Histogram" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                    If hidChart.Value = "Run" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                ElseIf txtCurrentReport.Text <> "" Then
                    If ddlgraphname.Items.Count <> 0 Then
                        lblgraphname.Visible = True
                        ddlgraphname.Visible = True
                    End If

                    If repcols.Items.Count = 0 Then
                        aspnet_msgbox("Please Select Report Columns.")
                        Exit Sub
                    End If
                    If selectedcols.Items.Count = 0 Then
                        aspnet_msgbox("Please Select Report Columns.")
                        Exit Sub
                    End If
                    If selectedcols.Items.Count = 1 Then
                        aspnet_msgbox("Please Select At Least Two Report Columns.")
                        Exit Sub
                    End If
                    If hidChart.Value = "Histogram" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                    If hidChart.Value = "Run" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                End If
            End If

            Dim r, m, j As Integer
            r = selectedcols.Items.Count
            Dim str4 As String = ""
            str4 = Session("graphType")
            For m = 0 To r - 1
                If x = "" Then
                    x = selectedcols.Items(m).Text
                Else
                    x = x & "," & selectedcols.Items(m).Text
                End If
            Next
            x = UCase(x)
            openselectedcolumn.Value = x
            If chkSunnarized.Checked = True Then
                RepGraphtype.Value = "Summarized"
            Else
                RepGraphtype.Value = "Normal"
            End If
            Dim sp As String()
            sp = x.Split(",")
            j = UBound(sp)
            ' Initialize a connection string	
            Dim myConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
            ' Define the database query	
            Dim mySelectQuery, orderstr As String
            Dim orderarr
            orderstr = hidtabname.Value
            orderarr = orderstr.Split(",")
            Dim strgroup As String

            If chkSunnarized.Checked = False Then
                Dim norll, norjj As Integer
                Dim nornum, nornum1, nornum2 As Integer
                Dim norstrvalue, norfinalformula, norstre, norb, norfmname As String
                norstre = Normalarray.Value
                Dim ranjitchk As String = ""
                Dim norstrfinalfor, norsumcol As String
                norformulaarr = norstre.Split(";")
                For norll = 0 To norformulaarr.length - 1
                    norstrvalue = norformulaarr(norll)
                    norstrvalue = UCase(norstrvalue)
                    For norjj = 0 To sp.Length - 1
                        'If norstrvalue.Contains("(") Or norstrvalue.Contains(")") Or norstrvalue.Contains("+") Or norstrvalue.Contains("-") Or norstrvalue.Contains("*") Or norstrvalue.Contains("/") Then
                        If LCase(norstrvalue).Contains("max(") Or LCase(norstrvalue).Contains("min(") Or LCase(norstrvalue).Contains("count(") Or LCase(norstrvalue).Contains("sum(") Or LCase(norstrvalue).Contains("avg(") Then

                            nornum1 = norstrvalue.LastIndexOf("%")
                            nornum2 = norstrvalue.Length
                            nornum = nornum2 - nornum1
                            norfinalformula = norstrvalue.Substring(nornum1 + 1, nornum - 1)
                            norfinalformula = norfinalformula.Trim(" ")
                            norfinalformula = norfinalformula.Replace("[", "")
                            norfinalformula = norfinalformula.Replace("]", "")
                            norfinalformula = norfinalformula.Trim(" ")
                            If sp(norjj) = norfinalformula Then

                                If norsumcol = "" Then
                                    norsumcol = norstrvalue.Replace("%", "AS")
                                    norstrfinalfor = norfinalformula
                                Else
                                    norsumcol = norsumcol + "," + norstrvalue.Replace("%", "AS")
                                    norstrfinalfor = norstrfinalfor + "," + norfinalformula
                                End If
                            End If
                        Else
                            Dim nornum12 As Integer
                            If norstrvalue.Contains("%") Then
                                nornum12 = norstrvalue.LastIndexOf("%")
                            Else
                                nornum12 = norstrvalue.LastIndexOf(".")
                            End If

                            Dim nornum13 As Integer = norstrvalue.Length
                            Dim nornum14 As Integer = nornum13 - nornum12
                            norfinalformula = norstrvalue.Substring(nornum12 + 1, nornum14 - 1)
                            norfinalformula = norfinalformula.Trim(" ")
                            norfinalformula = norfinalformula.Replace("[", "")
                            norfinalformula = norfinalformula.Replace("]", "")
                            norfinalformula = norfinalformula.Trim(" ")
                            If sp(norjj) = norfinalformula Then

                                If norb = "" Then

                                    norb = norstrvalue.Replace("%", "AS")


                                    norfmname = norfinalformula
                                Else
                                    norb = norb + "," + norstrvalue.Replace("%", "AS")


                                    norfmname = norfmname + "," + norfinalformula
                                End If
                            End If
                        End If

                    Next
                Next
                Dim nowgpy As String = ""
                Dim norcol As String
                Dim grpby = ""
                Dim ranjitmadegpby As String()


                If norb = "" Then
                    norb = sp(0)
                End If
                ranjitmadegpby = norb.Split(",")



                If LCase(ranjitmadegpby(0)).Contains(" as ") Then
                    Dim aajao As String = ranjitmadegpby(0).Replace(" AS ", "$")
                    Dim ranamade As String() = aajao.Split("$")
                    nowgpy = ranamade(0)
                Else
                    nowgpy = ranjitmadegpby(0)
                End If





                If norstrfinalfor = "" Then
                    norcol = norb
                ElseIf norfmname = "" Then
                    norcol = norsumcol
                Else
                    norcol = norb + "," + norsumcol
                End If

                'rana code start
                Dim gpdatabase As String = ""
                If hidgroup.Value <> "" Then
                    gpdatabase = hidgroup.Value.Remove(0, 9)

                    Dim splt = gpdatabase.Split(",")
                    Dim stst As Integer = 0
                    For stst = 0 To UBound(splt)
                        Dim nowda As String = LCase(splt(stst))

                        If LCase(norb).Contains(nowda) Then
                            If grpby = "" Then
                                'Gopal Changes - Added Start bracket
                                grpby = "group by " + nowda
                            Else
                                grpby = grpby + "," + nowda

                            End If

                        End If

                    Next

                    ''Gopal Changes
                    'If grpby <> "" Then
                    '    grpby = grpby + ")"
                    'End If
                    ''End Gopal Changes

                Else
                    'smithachanges
                    grpby = "group by " + norcol
                    grpby = ""

                    'smithachanges
                End If


                'stope and below was commented once
                'If norb = "" Then
                '    grpby = ""
                '    If hidgroup.Value <> "" Then
                '        grpby = hidgroup.Value
                '    End If
                'Else
                '    'ranjit changed start
                '    'Dim meracode As String = ""
                '    'If norb.Contains("AS") Then
                '    '    meracode = norb.Replace("AS", "^")
                '    'End If
                '    'Dim commacheck As String()
                '    ''If meracode.Contains(",") Then
                '    'commacheck = meracode.Split(",")
                '    ''End If
                '    'Dim jkl As Integer = 0
                '    'Dim madegp As String = ""
                '    'For jkl = 0 To UBound(commacheck)




                '    '    If meracode <> "" Then

                '    '        Dim codecomma As String = commacheck(jkl)
                '    '        Dim changedd As String() = codecomma.Split("^")
                '    '        Dim stloop As Integer = 0
                '    '        Dim lenloop As Integer = UBound(changedd)

                '    '        For stloop = 0 To lenloop


                '    '            If madegp = "" Then
                '    '                madegp = changedd(stloop)
                '    '            Else
                '    '                madegp = madegp + "," + changedd(stloop)
                '    '            End If
                '    '            stloop = stloop + 1
                '    '        Next
                '    '        grpby = " Group by" + " " + madegp
                '    '    Else
                '    '        'ranjit commented before change this whole code was containing only this below line
                '    '        'If norb <> "" Then
                '    grpby = " Group by" + " " + norb
                '    '        'End If

                '    '    End If
                '    'Next
                '    ' yahan tak
                'End If
                Dim hv = ""

                'ranjit changed for groupby
                'mySelectQuery = "select " + norcol + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " " + hidhaving.Value + " order by " & orderarr(0) + "." + sp(0) + ""

                mySelectQuery = "select " + norcol + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " " + hidhaving.Value + "Order By " & nowgpy
                mySelectQuery = UCase(mySelectQuery)

            ElseIf chkSunnarized.Checked = True Then

                If hidgroup.Value = "" Then
                    aspnet_msgbox("This is not Summarized Report")
                    Exit Sub
                End If
                Dim ll, jj As Integer
                Dim num, num1, num2 As Integer
                Dim strvalue, finalformula, stre As String
                stre = formulaarray.Value
                Dim ordermaderana As String() = stre.Split(";")
                Dim countall As Integer = 0
                Dim againorder As String()
                For countall = 0 To UBound(ordermaderana)
                    Dim udata As String = UCase(ordermaderana(countall))
                    Dim alltabdata As Integer = 0
                    For alltabdata = 0 To UBound(orderarr)
                        Dim utab As String = UCase(orderarr(alltabdata))
                        Dim ucol As String = "[" + UCase(sp(0)) + "]"
                        If udata.Contains(utab) And udata.Contains(ucol) Then
                            againorder = ordermaderana(countall).Split("%")

                        End If
                    Next

                Next
                'Dim ordsdd As String = ordermaderana(0)
                'Dim againorder As String() = ordsdd.Split("%")
                If stre = "" Then
                    aspnet_msgbox("This is not valid data for Summarized Report ")
                    Exit Sub
                End If
                Dim strfinalfor, sumcol As String
                txtformulaarr = stre.Split(";")
                For ll = 0 To txtformulaarr.length - 1
                    strvalue = txtformulaarr(ll)
                    strvalue = UCase(strvalue)
                    For jj = 0 To sp.Length - 1
                        If strvalue.Contains("(") Or strvalue.Contains(")") Or strvalue.Contains("+") Or strvalue.Contains("-") Or strvalue.Contains("*") Or strvalue.Contains("/") Then
                            num1 = strvalue.LastIndexOf("%")
                            num2 = strvalue.Length
                            num = num2 - num1
                            finalformula = strvalue.Substring(num1 + 1, num - 1)
                            finalformula = finalformula.Trim(" ")
                            finalformula = finalformula.Replace("[", "")
                            finalformula = finalformula.Replace("]", "")
                            finalformula = finalformula.Trim(" ")
                            If sp(jj) = finalformula Then

                                If sumcol = "" Then
                                    sumcol = strvalue.Replace("%", "AS")
                                    strfinalfor = finalformula
                                Else
                                    sumcol = sumcol + "," + strvalue.Replace("%", "AS")
                                    strfinalfor = strfinalfor + "," + finalformula
                                End If
                            End If
                        Else
                            Dim num12 As Integer
                            If strvalue.Contains("%") Then
                                num12 = strvalue.LastIndexOf("%")
                            Else
                                num12 = strvalue.LastIndexOf(".")
                            End If

                            Dim num13 As Integer = strvalue.Length
                            Dim num14 As Integer = num13 - num12
                            finalformula = strvalue.Substring(num12 + 1, num14 - 1)
                            finalformula = finalformula.Trim(" ")
                            finalformula = finalformula.Replace("[", "")
                            finalformula = finalformula.Replace("]", "")
                            finalformula = finalformula.Trim(" ")
                            If sp(jj) = finalformula Then
                                strvalue = Replace(strvalue, "%", "AS")
                                Dim oplk = Split(strvalue, " AS ")
                                If oplk.length > 1 Then
                                    If b = "" Then
                                        b = oplk(0)
                                        fmname = finalformula
                                    Else
                                        b = b + "," + oplk(0)
                                        fmname = fmname + "," + finalformula
                                    End If
                                Else
                                    If b = "" Then
                                        b = strvalue
                                        fmname = finalformula
                                    Else
                                        b = b + "," + strvalue
                                        fmname = fmname + "," + finalformula
                                    End If
                                End If

                            End If
                        End If

                    Next
                Next
                y = reotablename.Value
                Dim grpby = ""
                If b = "" Then
                    grpby = ""

                Else
                    grpby = " Group by" + " " + b
                End If
                If strfinalfor = "" Then
                    m1 = b
                ElseIf b = "" Then
                    m1 = sumcol
                    strgroup = ""
                Else
                    m1 = b + "," + sumcol
                End If
                'ranjit commented its original now
                mySelectQuery = "select " + m1 + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " order by " & orderarr(0) + "." + sp(0) + ""
                'mySelectQuery = "select " + m1 + " from " + hidtabname.Value + " " + hidwheretxt.Value + " " + grpby + " order by " & againorder(0)
                '+ "." + sp(0) + ""
                mySelectQuery = UCase(mySelectQuery)
            End If
            Dim drillstr As String
            Session("xvalue") = x
            Session("yvalue") = y
            drillstr = "select " + x + " from " + y + " "
            Session("drillquery") = drillstr
            ' Create a database connection object using the connection string	
            Dim myConnection As New SqlConnection(myConnectionString)
            ' Create a database command on the connection using query	
            Dim myCommand As New SqlCommand(mySelectQuery, myConnection)
            ' Open the connection	
            myCommand.Connection.Open()
            ' Initializes a new instance of the OleDbDataAdapter class
            Dim myDataAdapter As New SqlDataAdapter
            myDataAdapter.SelectCommand = myCommand
            ' Initializes a new instance of the DataSet class
            Dim myDataSet As New DataSet()
            ' Adds rows in the DataSet
            myDataAdapter.Fill(myDataSet, "Query")
            'smithachangesstart
            Dim myDataSetcount As Integer = myDataSet.Tables(0).Columns.Count
            Dim myDataSetcountstart As Integer = 0
            Dim datasetcolumntype As String = ""
            Dim datasetcolumnname As String = ""
            Dim positionasstring As String = ""
            Dim positionasint As String = ""
            Dim var As Integer
            Dim newdataset As String = ""
            Dim ramstr As String = ""
            For myDataSetcountstart = 0 To myDataSetcount - 1
                datasetcolumntype = myDataSet.Tables("Query").Columns(myDataSetcountstart).DataType.ToString
                datasetcolumnname = myDataSet.Tables("Query").Columns(myDataSetcountstart).ColumnName
                ramstr = ""
                If LCase(datasetcolumntype).Contains("string") Or LCase(datasetcolumntype).Contains("datetime") Then
                    Try


                        var = CInt(myDataSet.Tables("Query").Rows(0)(datasetcolumnname))
                    Catch ex As Exception
                        ramstr = "no"

                        If positionasstring = "" Then
                            positionasstring = datasetcolumnname
                        Else
                            positionasstring = positionasstring + "," + datasetcolumnname
                        End If



                    End Try
                    If ramstr = "" Then
                        If positionasint = "" Then
                            positionasint = datasetcolumnname
                        Else
                            positionasint = positionasint + "," + datasetcolumnname
                        End If
                    End If

                Else
                    If positionasint = "" Then
                        positionasint = datasetcolumnname
                    Else
                        positionasint = positionasint + "," + datasetcolumnname
                    End If


                End If

            Next

            If positionasstring = "" Then
                newdataset = positionasint
            ElseIf positionasint = "" Then
                newdataset = positionasstring

            Else
                newdataset = positionasstring + "," + positionasint
            End If

            Dim datasetarray As String() = newdataset.Split(",")
            Dim datasetnew As New DataSet
            Dim newdatatable As New DataTable

            Dim newdatacolumn As New DataColumn
            Dim newdatarow As DataRow
            Dim dtarow As DataRow
            For myDataSetcountstart = 0 To UBound(datasetarray)
                newdatacolumn = New DataColumn(datasetarray(myDataSetcountstart))

                newdatatable.Columns.Add(newdatacolumn)
            Next

            For Each dtarow In myDataSet.Tables("Query").Rows
                newdatarow = newdatatable.NewRow

                For myDataSetcountstart = 0 To UBound(datasetarray)

                    newdatarow.Item(datasetarray(myDataSetcountstart)) = dtarow(datasetarray(myDataSetcountstart))

                Next
                newdatatable.Rows.Add(newdatarow)

            Next
            myDataSet.Clear()
            myDataSet.Dispose()
            myDataSet.Tables.Remove("Query")
            'myDataSet.Tables.Add("Query", newdatatable.Namespace)
            myDataSet.Tables.Add(newdatatable)


            'stop

            myCommand.Connection.Close()
            '***********************************************************************************
            ' Set Column chart
            If hidChart.Value = "" Then
                hidChart.Value = "Column"
                showchart()
            End If
            If hidChart.Value = "Column" Then
                Chart1.Visible = True
                Chart2.Visible = False
                'Select Row  series data
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Double
                    Dim sval As String
                    Dim str As String
                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Dim ij As Integer = 0
                            Try

                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                    End If
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        str = ""
                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim YVal As Double

                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Dim series As Series = Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Column
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String

                        Dim bb As String = ""
                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                nodesign = ""

                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)


                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label3
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label3:
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False

                    Next row
                    GoTo label2
                End If

                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, columncount1, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim counter As Integer
                    Dim strcol As String
                    counter = 0
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                        colintval = 0
                                        colarr(columncount) = cc
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                        colarr(columncount) = cc
                                    End If

                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        Dim YVal1 As Double

                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Dim series As Series = Chart1.Series.Add(strcol)
                                Dim Datapoint As DataPoint

                                Chart1.Series(strcol).Type = SeriesChartType.Column
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim arrcolumn(10) As String
                                Dim arryval1(20) As Double

                                xval = ""
                                Try
                                    'For columncount1 = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        'columnseries = row1(0)
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.ShowEndLabels = True
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                End If
                GoTo label2
                'Set Run chart
            ElseIf hidChart.Value = "Run" Then
                Chart2.Visible = False
                Chart1.Visible = True

                Dim aSubGroup(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim aData(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim st1 As String = ""
                Dim st2 As String = ""
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    GoTo label1
                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer
                        Dim strcol As String
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Single
                                xval = ""
                                Try
                                    Dim runi As Integer = 0
                                    Dim colmnjmn As DataColumn
                                    Dim data As Single
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        columnseries = row1(0).ToString()

                                        Try
                                            If IsDBNull(row1(0)) Then
                                            Else
                                                YVal1 = CSng(row1(0))
                                            End If
                                            If IsDBNull(row1(1)) Then
                                            Else
                                                data = CSng(row1(1))
                                            End If

                                            aData(runi) = data
                                            aSubGroup(runi) = YVal1
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)
                                            End If

                                        End Try
                                        runi = runi + 1
                                    Next
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try

                        Dim Run_Chart As Run_Chart = New Run_Chart()
                        ' Create a C-Chart of the data and plot it on the chart. 
                        '

                        ' Optionally before calling any chart creation function, you can setup styles for 
                        ' Control Lines. 



                        Run_Chart.UCLstyle.LineStyle = ChartDashStyle.Solid
                        Run_Chart.UCLstyle.LineColor = Color.Red
                        Run_Chart.UCLstyle.LineWidth = 2
                        Run_Chart.UCLstyle.ShowText = True


                        ' Also you can set style for text 
                        ' Run_Chart.ShowText = True
                        Run_Chart.LCLstyle.TextColor = Color.Blue
                        Run_Chart.LCLstyle.TextFont = New Font("Arial", 10)
                        Run_Chart.LCLstyle.ShowText = True

                        'Run_Chart.AutoFitLines = True

                        Dim tmpSeries As Series = Run_Chart.CreateSeries(aSubGroup, aData, Chart1)

                        'Chart1.ChartAreas("Chart Area 1").AxisX.LabelsAutoFit = False

                        tmpSeries.ShowLabelAsValue = True
                        tmpSeries.SmartLabels.Enabled = True
                        tmpSeries.SmartLabels.MovingDirection = LabelAlignment.Top
                        tmpSeries.SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Round
                        tmpSeries.SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No
                        tmpSeries.SmartLabels.CalloutStyle = LabelCalloutStyle.Box
                        tmpSeries.SmartLabels.HideOverlapped = False
                        tmpSeries.SmartLabels.CalloutBackColor = Color.Bisque
                        GoTo label2
                    Next columncount
                End If
            ElseIf hidChart.Value = "Pareto" Then
                Chart2.Visible = False
                Dim strcol As String
                Chart1.Visible = True
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Column
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddY(YVal1)
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)

                                            End If
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    MakeParetoChart(Chart1, strcol, "Pareto")
                    Chart1.Series("Pareto").Type = SeriesChartType.Line
                    Chart1.Series("Pareto").ShowLabelAsValue = True
                    Chart1.Series("Pareto").MarkerColor = Color.Red
                    Chart1.Series("Pareto").MarkerBorderColor = Color.MidnightBlue
                    Chart1.Series("Pareto").MarkerStyle = MarkerStyle.Circle
                    Chart1.Series("Pareto").MarkerSize = 8
                    Chart1.Series("Pareto").LabelFormat = "0.#"
                    ' format with one decimal and leading zero 
                    ' Set Color of line Pareto chart 
                    Chart1.Series("Pareto").Color = Color.Aquamarine
                    GoTo label2
                End If

                'Set Histogram chart
            ElseIf hidChart.Value = "Histogram" Then

                Chart2.Titles(0).Text = txtCharttitle.Text
                Chart2.ChartAreas("HistogramArea").AxisX.Title = txtTitleext.Text
                Chart2.ChartAreas("HistogramArea").AxisY.Title = txtYTitle.Text
                Dim strcol As String = ""
                Chart1.Visible = False
                Chart2.Visible = True
                Chart2.Width = 1000
                Chart2.Height = 500

                'Chart2.ChartAreas("HistogramArea").Position.Auto = False

                Chart2.ChartAreas("Default").Position.Y = 10
                Chart2.ChartAreas("Default").Position.X = 5
                Chart2.ChartAreas("Default").Position.Height = 7
                Chart2.ChartAreas("HistogramArea").Position.X = 5 'Single.Parse(txtX1.Text)
                Chart2.ChartAreas("HistogramArea").Position.Y = Chart2.ChartAreas("Default").Position.Height + 10 'Single.Parse(txtY1.Text)
                'Chart2.ChartAreas("HistogramArea").Position.Width = 80 'Single.Parse(txtWidth1.Text)
                'Chart2.ChartAreas("HistogramArea").Position.Height = 80 'Single.Parse(txtHeight1.Text)

                ''Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Auto = False
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.X = 10
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Y = 10
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Width = 80 'Single.Parse(txtWidth2.Text)
                'Chart1.ChartAreas("HistogramArea").InnerPlotPosition.Height = 80 'Single.Parse(txtHeight2.Text)

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If

                                    Exit For
                                End Try
                            Next col
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart2.Series.Add(strcol)
                                Chart2.Series(strcol).Type = SeriesChartType.Column
                                Chart2.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart2.Series(strcol).Points.AddY(YVal1)
                                        Catch ex As Exception
                                            If IsDBNull(row1(colname)) Then
                                            Else
                                                xval = row1(colname)

                                            End If
                                        End Try





                                        Chart2.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart2.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart2.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart2.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart2.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart2.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart2.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        'smitha
                                        'Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes
                                        'smitha
                                        Chart2.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart2.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart2.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart2.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                    'Dim maxValuePoint As DataPoint = Chart2.Series(strcol).Points.FindMaxValue()
                    'Dim b As Integer
                    'b = (maxValuePoint)
                    ' Chart2.Series(seriesName).Points.AddY(maxValuePoint.m)
                    ' Dim minValuePoint As DataPoint = Chart2.Series(strcol).Points.FindMinValue()
                    'Chart2.Series(seriesName).Points.AddY(minValuePoint)
                    ' Populate single axis data distribution series. Show Y value of the 
                    ' data series as X value and set all Y values to 1. 
                    For Each dataPoint As DataPoint In Chart2.Series(strcol).Points
                        'Chart2.Series.Add(DataDistribution)
                        Chart2.Series("DataDistribution").Points.AddXY(dataPoint.YValues(0), 1)
                    Next
                    ' Create a histogram series 
                    Dim histogramHelper As New HistogramChartHelper()
                    'histogramHelper.SegmentIntervalNumber = Integer.Parse(CollectedPercentage.SelectedItem.Text)
                    'histogramHelper.ShowPercentOnSecondaryYAxis = checkBoxPercent.Checked
                    ' NOTE: Interval width may be specified instead of interval number 
                    'histogramHelper.SegmentIntervalWidth = 15; 
                    histogramHelper.CreateHistogram(Chart2, strcol, "Histogram")

                    ' Set same X axis scale and interval in the single axis data distribution 
                    ' chart area as in the histogram chart area. 
                    Chart2.ChartAreas("Default").AxisX.Minimum = Chart2.ChartAreas("HistogramArea").AxisX.Minimum
                    Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("HistogramArea").AxisX.Maximum
                    Chart2.ChartAreas("Default").AxisX.Interval = Chart2.ChartAreas("HistogramArea").AxisX.Interval
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Pie" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    Dim dup As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String


                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval

                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        ' arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String
                        If str <> "" Then
                            GoTo label2
                        End If

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)

                        Chart1.Series(str).Type = SeriesChartType.Pie
                        Chart1.Series(str).Color = Color.Blue
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double
                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label4
                                End If
                                If chk_hide.Checked = True Then
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    piehelper.CollectedPercentage = Double.Parse("16")
                                    '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                    '// where original and supplemental pie charts should be placed.
                                    piehelper.SupplementedAreaSizeRatio = 1.1F
                                    piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                    '// Show supplemental pie for the "Default" series
                                    piehelper.ShowSmallSegmentsAsSupplementalPie(str)
                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    Chart1.Series(str).LabelBackColor = Color.Empty
                                End If
                                'Chart1.Series(strcol).ShowLabelAsValue = True
                                'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                Chart1.Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label4:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval, strcol As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    piehelper = New Piecollected(Chart1)
                    piehelper.CollectedLabel = String.Empty
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Pie
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                Dim counter As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1

                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        'Chart1.ChartAreas("Chart Area 1").Position.Auto = True
                                        If chk_hide.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            piehelper.CollectedPercentage = Double.Parse("16")
                                            '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                            '// where original and supplemental pie charts should be placed.
                                            piehelper.SupplementedAreaSizeRatio = 1.1F
                                            piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                            '// Show supplemental pie for the "Default" series
                                            piehelper.ShowSmallSegmentsAsSupplementalPie(strcol)
                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            Chart1.Series(strcol).LabelBackColor = Color.Empty
                                        End If
                                        'Chart1.Series(strcol).ShowLabelAsValue = True
                                        'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                        Chart1.Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                    GoTo label2

                End If
            ElseIf hidChart.Value = "Area" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String
                    Dim ij As Integer = 0

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            ij = 0
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Area
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label5
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label5:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False

                    Next row
                    GoTo label2
                End If

                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim strcol As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Area
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try

                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Bar" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        '  GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Bar
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label6
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                'Chart1.ChartAreas("Chart Area 1").AxisX.IntervalAutoMode = IntervalAutoMode.VariableCount
                                ' Enabling the SmartLabels™ attribute.
                                Chart1.Series(str).SmartLabels.Enabled = True

                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Dash

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight

                                ' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 60

                                ' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 160

                                ' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No

                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label6:
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer
                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Bar
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        ' Use point index for drawing the chart 
                                        '  
                                        'hrt1.Series(strcol).XValueIndexed = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Daughnt" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Doughnut
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label7
                                End If

                                If chk_hide.Checked = True Then
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    piehelper.CollectedPercentage = Double.Parse("16")
                                    '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                    '// where original and supplemental pie charts should be placed.
                                    piehelper.SupplementedAreaSizeRatio = 1.1F
                                    piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                    '// Show supplemental pie for the "Default" series
                                    piehelper.ShowSmallSegmentsAsSupplementalPie(str)
                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                    Chart1.Series(str).LabelBackColor = Color.Empty
                                End If
                                'Chart1.Series(strcol).ShowLabelAsValue = True
                                'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                Chart1.Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
Label7:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                Dim smitha As Integer = -1
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String

                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Doughnut
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        If chk_hide.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            piehelper.CollectedPercentage = Double.Parse("16")
                                            '// Set position in relative coordinates ( 0,0 - top left corner; 100,100 - bottom right corner)
                                            '// where original and supplemental pie charts should be placed.
                                            piehelper.SupplementedAreaSizeRatio = 1.1F
                                            piehelper.ChartAreaPosition = New RectangleF(3.0F, 3.0F, 93.0F, 96.0F)
                                            '// Show supplemental pie for the "Default" series
                                            piehelper.ShowSmallSegmentsAsSupplementalPie(strcol)
                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                            Chart1.Series(strcol).LabelBackColor = Color.Empty
                                        End If
                                        'Chart1.Series(strcol).ShowLabelAsValue = True
                                        'Chart1.Series(strcol).AxisLabel = "Y = #VALY\nX = #VALX"
                                        Chart1.Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("18"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If

            ElseIf hidChart.Value = "Stock" Then

                'selet Column series data
                Chart2.Visible = False
                Chart1.Visible = False
                StockChart.Visible = True
                StockChart.Series.Clear()


                ''Create command object
                'Dim StockCmd As New SqlCommand(

                StockChart.DataSource = myDataSet.Tables(0)
                'Assign Serise
                Dim StockI As Integer

                StockChart.Height = 500
                StockChart.Width = 1000

                For StockI = 1 To myDataSet.Tables(0).Columns.Count - 1
                    StockChart.Series.Add(myDataSet.Tables(0).Columns(StockI).ColumnName).Type = SeriesChartType.Stock
                    'StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowLabelAsValue = True
                    'StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).AxisLabel
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowInLegend = True


                    'With (StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).SmartLabels)
                    '    .Enabled = True
                    '    .CalloutLineStyle = ChartDashStyle.Solid
                    'End With


                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).BorderWidth = (myDataSet.Tables(0).Columns.Count - StockI) * 4
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                    StockChart.Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMembersY = myDataSet.Tables(0).Columns(StockI).ColumnName.ToString
                Next
                'StockChart.Series.Add("Series1").Type = SeriesChartType.Stock
                'StockChart.Series("Series1").BorderWidth = 10
                'StockChart.Series("Series1").ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                'StockChart.Series("Series1").ValueMembersY = myDataSet.Tables(0).Columns(1).ColumnName.ToString


                'StockChart.Series.Add("Series2").Type = SeriesChartType.Stock
                'StockChart.Series("Series2").BorderWidth = 1
                'StockChart.Series("Series2").ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                'StockChart.Series("Series2").ValueMembersY = myDataSet.Tables(0).Columns(2).ColumnName.ToString

                'StockChart.DataBind()

                '' ''If rbnColumn.Checked = True Then
                '' ''    Chart2.Visible = False
                '' ''    Chart1.Visible = False
                '' ''    StockChart.Visible = True
                '' ''    StockChart.Series.Clear()
                '' ''    Dim columncount, colintval As Integer
                '' ''    Dim cc, colseries, colstrval As String
                '' ''    Dim colarr(10) As String
                '' ''    Dim col As DataRow
                '' ''    'If myDataSet.Tables(0).Columns.Count < 4 Then
                '' ''    '    StockChart.Visible = False
                '' ''    '    GoTo label1
                '' ''    'End If
                '' ''    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                '' ''        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                '' ''        cc = colname
                '' ''        Try
                '' ''            For Each col In myDataSet.Tables(0).Rows
                '' ''                colseries = col(0).ToString()
                '' ''                Try
                '' ''                    If IsDBNull(col(colname)) Then
                '' ''                    Else
                '' ''                        colintval = CInt(col(colname))
                '' ''                        colarr(columncount) = cc
                '' ''                    End If

                '' ''                    Exit For
                '' ''                Catch ex As Exception
                '' ''                    If IsDBNull(col(colname)) Then
                '' ''                    Else
                '' ''                        colstrval = col(colname)
                '' ''                    End If


                '' ''                    StockChart.Visible = False
                '' ''                    GoTo label1
                '' ''                End Try
                '' ''            Next col
                '' ''        Catch ex As Exception

                '' ''        End Try
                '' ''        Dim yo As Integer
                '' ''        Dim strcol As String
                '' ''        Dim strnothing As String = ""
                '' ''        strcol = ""
                '' ''        For yo = 0 To colarr.Length - 1
                '' ''            If (colarr(yo) = "") Then
                '' ''                strnothing = colarr(yo)
                '' ''            ElseIf (colarr(yo) <> "") Then
                '' ''                strcol = ""
                '' ''                If (strcol = "") Then
                '' ''                    strcol = colarr(yo)
                '' ''                Else
                '' ''                    Exit For
                '' ''                End If
                '' ''            End If
                '' ''        Next yo
                '' ''        Try
                '' ''            If strcol = "" Then
                '' ''                Exit Try
                '' ''            Else
                '' ''                If strcol = Duplicate Then
                '' ''                    strcol = strcol + count.ToString
                '' ''                    count = count + 1
                '' ''                End If


                '' ''                StockChart.Series.Add(strcol)
                '' ''                StockChart.Series(strcol).Type = SeriesChartType.Stock
                '' ''                StockChart.Series(strcol).BorderWidth = 2
                '' ''                Dim st As String
                '' ''                st = strcol.Replace(count - 1, "")
                '' ''                Duplicate = st
                '' ''                Dim xval As String
                '' ''                Dim row1 As DataRow
                '' ''                Dim YVal1 As Double
                '' ''                xval = ""
                '' ''                Try
                '' ''                    For Each row1 In myDataSet.Tables(0).Rows
                '' ''                        ' for each row (row 1 and onward), add the value as a point
                '' ''                        columnseries = row1(0).ToString()
                '' ''                        Try
                '' ''                            If IsDBNull(row1(colname)) Then
                '' ''                            Else
                '' ''                                YVal1 = CInt(row1(colname))
                '' ''                            End If

                '' ''                            StockChart.Series(strcol).Points.AddXY(columnseries, YVal1)
                '' ''                        Catch ex As Exception
                '' ''                            If IsDBNull(row1(colname)) Then
                '' ''                            Else
                '' ''                                xval = row1(colname)
                '' ''                            End If

                '' ''                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                '' ''                        End Try

                '' ''                        ''Gopal stock
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelsAutoFit = False
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelStyle.Enabled = True
                '' ''                        'StockChart.ChartAreas("Price").AxisY.LabelStyle 


                '' ''                        StockChart.Series(strcol).ShowLabelAsValue = True
                '' ''                        '' Enable SmartLabels.   
                '' ''                        StockChart.Series(strcol).SmartLabels.Enabled = True
                '' ''                        ' Set the callout style.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                '' ''                        ' Set the callout line color.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                '' ''                        ' Set the callout line style.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                '' ''                        ' Set the callout line width.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineWidth = 1

                '' ''                        ' Set the callout line anchor cap.
                '' ''                        StockChart.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                '' ''                        ' Set the controlling moving directions.
                '' ''                        StockChart.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                '' ''                        '' Set the minimum distance that the labels can move.
                '' ''                        StockChart.Series(strcol).SmartLabels.MinMovingDistance = 100

                '' ''                        '' Set the minimum distance that the labels can move.
                '' ''                        StockChart.Series(strcol).SmartLabels.MaxMovingDistance = 100

                '' ''                        '' Allow the labels to be placed outside the plotting area.
                '' ''                        StockChart.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                '' ''                        StockChart.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                '' ''                        StockChart.Series(strcol).LegendToolTip = "Legend" + strcol
                '' ''                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                '' ''                        StockChart.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                '' ''                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                '' ''                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                '' ''                            StockChart.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                '' ''                        End If
                '' ''                    Next row1
                '' ''                Catch ex As Exception
                '' ''                End Try
                '' ''            End If
                '' ''        Catch ex As Exception
                '' ''        End Try
                '' ''    Next columncount
                '' ''    GoTo label2
                '' ''End If
            ElseIf hidChart.Value = "Line" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Line
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                nodesign = "no"
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label8
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label8:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Line
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2

                End If
            ElseIf hidChart.Value = "Scaterplot" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series

                        Dim ij As Integer
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Point
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = "no"
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = "no"
                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label9
                                End If

                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label9:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim strcol As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            strcol = ""


                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else

                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)

                                    End If
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1


                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                'If chekdupli = "" Then



                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                'End If
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""

                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception

                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If
            ElseIf hidChart.Value = "Scatter" Then
                Chart2.Visible = False
                Chart1.Visible = True
                If rbnRow.Checked = True Then
                    Dim xval As String
                    xval = ""
                    Dim row As DataRow
                    count = 1
                    Duplicate = ""
                    Dim i As Integer
                    Dim rowname As String
                    Dim rval As Integer
                    Dim sval As String

                    For Each row In myDataSet.Tables(0).Rows
                        ' for each Row, add a new series
                        Dim ij As Integer = 0
                        For i = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                            rowname = myDataSet.Tables(0).Columns(i).ColumnName
                            Try
                                If IsDBNull(row(rowname)) Then
                                Else
                                    rval = CInt(row(rowname))
                                End If
                            Catch ex As Exception
                                Try
                                    If IsDBNull(row(rowname)) Then
                                    Else
                                        dval = CDate(row(rowname)).Date
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        'GoTo label1
                                    End If
                                    If IsDBNull(row(rowname)) Then


                                    Else
                                        sval = row(rowname)
                                        'arr(i) = sval
                                        arr(ij) = sval
                                    End If

                                End Try
                                If sval = "" Then
                                    'arr(i) = dval
                                    arr(ij) = dval
                                End If

                            End Try
                        Next i
                        Dim jk As Integer
                        Dim str As String = ""

                        For jk = 0 To arr.Length - 1
                            If (arr(jk) <> "") Then
                                If (str = "") Then
                                    str = arr(jk)
                                Else
                                    str = str + " " + arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim series0 As String
                        series0 = str
                        If str = Duplicate Then
                            str = str + count.ToString
                            count = count + 1
                        End If
                        Chart1.Series.Add(str)
                        Chart1.Series(str).Type = SeriesChartType.Point
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Double

                        Try
                            Dim nodesign As String = ""
                            For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                Try
                                    If IsDBNull(row(columnName)) Then
                                    Else
                                        YVal = CDbl(row(columnName))
                                    End If

                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    nodesign = ""
                                Catch ex As Exception
                                    Try
                                        If IsDBNull(row(columnName)) Then
                                        Else
                                            dateval = CDate(row(columnName))
                                        End If

                                        If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                            Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            nodesign = ""

                                        End If

                                    Catch ex1 As Exception

                                    End Try
                                    bb = row(columnName)
                                End Try
                                If nodesign = "" Then
                                    GoTo label10
                                End If
                                Chart1.Series(str).ShowLabelAsValue = True
                                '' Enable SmartLabels.   
                                Chart1.Series(str).SmartLabels.Enabled = True
                                ' Set the callout style.
                                Chart1.Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                ' Set the callout line color.
                                Chart1.Series(str).SmartLabels.CalloutLineColor = Color.Red

                                ' Set the callout line style.
                                Chart1.Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                ' Set the callout line width.
                                Chart1.Series(str).SmartLabels.CalloutLineWidth = 1

                                ' Set the callout line anchor cap.
                                Chart1.Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                ' Set the controlling moving directions.
                                Chart1.Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MinMovingDistance = 100

                                '' Set the minimum distance that the labels can move.
                                Chart1.Series(str).SmartLabels.MaxMovingDistance = 100

                                '' Allow the labels to be placed outside the plotting area.
                                Chart1.Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                Chart1.Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                Chart1.Series(str).LegendToolTip = "Legend" + str
                                'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                If ddlChartwidth.SelectedItem.Text = "200%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                    Chart1.Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                End If
label10:
                            Next colIndex
                            Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                        YVal = 0.0
                        dateval = "#12:00:00 AM#"
                        fb = False
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                Dim smitha As Integer = -1

                If rbnColumn.Checked = True Then
                    Dim columncount, colintval, counter As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim strcol As String
                    For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables(0).Rows
                                colseries = col(0).ToString()
                                Try
                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colintval = CInt(col(colname))
                                        colarr(columncount) = cc
                                    End If
                                    'colintval = CInt(col(colname))
                                    'colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        If IsDBNull(col(colname)) Then
                                        Else
                                            dat = CDate(col(colname))
                                            If dat = "#12:00:00 AM#" Then
                                                dat = ""
                                            End If
                                            colarr(columncount) = cc
                                        End If
                                    Catch ex1 As Exception

                                    End Try

                                    If IsDBNull(col(colname)) Then
                                    Else
                                        colstrval = col(colname)
                                    End If
                                    'colstrval = col(colname)
                                    If colstrval <> "" And colintval <> 0 Then
                                        GoTo label1
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception
                            smitha = smitha + 1
                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        strcol = ""
                        For yo = 0 To colarr.Length - 1
                            If (colarr(yo) = "") Then
                                strnothing = colarr(yo)
                            ElseIf (colarr(yo) <> "") Then
                                strcol = ""
                                If (strcol = "") Then
                                    strcol = colarr(yo)
                                Else
                                    Exit For
                                End If
                            End If
                        Next yo
                        Try
                            If strcol = "" Then
                                Exit Try
                            Else
                                If strcol = Duplicate Then
                                    strcol = strcol + count.ToString
                                    count = count + 1
                                End If
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Double
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables(0).Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        If smitha = -1 Then
                                            smitha = 1
                                        End If
                                        columnseries = row1(smitha)
                                        Try
                                            columnseries = CDbl(columnseries)
                                            columnseries = countercol

                                        Catch ex As Exception
                                            columnseries = columnseries
                                        End Try
                                        Dim dateval As Date
                                        Try
                                            If IsDBNull(row1(colname)) Then
                                                YVal1 = 0
                                            Else
                                                YVal1 = CDbl(row1(colname))
                                            End If
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, YVal1)
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row1(colname)) Then
                                                    dateval = "0"
                                                Else
                                                    dateval = CDate(row1(colname))
                                                End If
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        Chart1.Series(strcol).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        Chart1.Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        Chart1.Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        Chart1.Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        Chart1.Series(strcol).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        Chart1.Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        'Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlChartwidth.SelectedItem.Text = "200%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                        ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                                            Chart1.Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
                                        countercol = countercol + 1
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If
            End If
label1:

label2:
            Try
                If hidChart.Value = "Stock" Then
                    stockfrormat()
                    StockChart.DataBind()
                Else

                    Commanchartformat()

                    Chart1.DataBind()
                End If
                myCommand.Dispose()
                myConnection.Close()
            Catch ex As Exception
                aspnet_msgbox(ex.Message)
            End Try
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try

    End Sub


    Public Sub Commanchartformat()
        Dim fontstylecheckbox As New FontStyle
        Dim fontstylecheckbox1 As New FontStyle
        Dim fontstylecheckbox2 As New FontStyle
        Dim fontstylecheckbox3 As New FontStyle
        Chart1.ChartAreas("Chart Area 1").AxisX.Interval = 1

        Try
            If ddlChartwidth.SelectedItem.Text = "100%" Then
                Chart1.Width = 600
                Chart1.Height = 600
                'Set Chart Axis title font
               
                Try

                    'smitha
                    If (hidChart.Value = "Run") Then
                        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Format = "" ''added by atul to remove 'K' alphabet in run chart from x axis
                    End If
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                    'smitha
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Calibri", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
                End Try

                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font("Calibri", Single.Parse("7"))
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font("Calibri", Single.Parse("7"))
            ElseIf ddlChartwidth.SelectedItem.Text = "200%" Then
                Chart1.Width = 800
                Chart1.Height = 800

                'Chart1.Series(Str).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("45"), fontstylecheckbox)
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font("Calibri", Single.Parse("10"))
                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font("Calibri", Single.Parse("10"))
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("18"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("18"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse("18"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse("18"), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("18"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse("18"), fontstylecheckbox)
                End Try
            ElseIf ddlChartwidth.SelectedItem.Text = "300%" Then
                Chart1.Width = 1000
                Chart1.Height = 1000
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse("18"))
                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse("18"))
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("19"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("19"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse("19"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse("19"), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("19"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse("19"), fontstylecheckbox)
                End Try
            ElseIf ddlChartwidth.SelectedItem.Text = "400%" Then
                Chart1.Width = 1200
                Chart1.Height = 1200
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse("19"))
                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse("19"))
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("20"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("20"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse("20"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse("20"), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("20"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse("20"), fontstylecheckbox)
                End Try
            ElseIf ddlChartwidth.SelectedItem.Text = "500%" Then
                Chart1.Width = 5000
                Chart1.Height = 1400
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse("20"))
                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse("20"))
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("21"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("21"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse("21"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse("21"), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("21"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse("21"), fontstylecheckbox)
                End Try
            ElseIf ddlChartwidth.SelectedItem.Text = "1000%" Then
                Chart1.Width = 9000
                Chart1.Height = 1400
                Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse("21"))
                Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse("21"))
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("23"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse("23"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse("23"), fontstylecheckbox)
                    Chart1.Titles(0).Font = New Font("Arial", Single.Parse("23"), fontstylecheckbox)
                End Try
                Try
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse("23"), fontstylecheckbox)
                Catch ex As Exception
                    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse("23"), fontstylecheckbox)
                End Try
            End If
            UpdateColor()
            UpdateLegendCellColumns()
            SetupChart()
            Chart1.ChartAreas("Chart Area 1").BackColor = System.Drawing.ColorTranslator.FromHtml(chartareabkcolor.Value)

            Chart1.ChartAreas("Chart Area 1").Position.Auto = False
            Chart1.ChartAreas("Chart Area 1").Position.X = Single.Parse(txtX1.Text)
            Chart1.ChartAreas("Chart Area 1").Position.Y = Single.Parse(txtY1.Text)
            Chart1.ChartAreas("Chart Area 1").Position.Width = Single.Parse(txtWidth1.Text)
            Chart1.ChartAreas("Chart Area 1").Position.Height = Single.Parse(txtHeight1.Text)

            If Chart1.ChartAreas("Chart Area 1").Position.Width <= 10 Then
                Chart1.ChartAreas("Chart Area 1").Position.Width = 10
            End If
            If Chart1.ChartAreas("Chart Area 1").Position.Height <= 10 Then
                Chart1.ChartAreas("Chart Area 1").Position.Height = 10
            End If

            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Auto = False
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.X = Single.Parse(txtX2.Text)
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Y = Single.Parse(txtY2.Text)
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Width = Single.Parse(txtWidth2.Text)
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Height = Single.Parse(txtHeight2.Text)

            If Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Width <= 10 Then
                Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Width = 10
            End If
            If Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Height <= 10 Then
                Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Height = 10

            End If
        Catch ex As Exception
            ' Display exception message in chart title 
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub

            ' Re-set Chart Area coordinates 
            Chart1.ChartAreas("Chart Area 1").Position.X = 10
            Chart1.ChartAreas("Chart Area 1").Position.Y = 10
            Chart1.ChartAreas("Chart Area 1").Position.Width = 75
            Chart1.ChartAreas("Chart Area 1").Position.Height = 75

            ' Re-set Plotting Area coordinates 
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.X = 10
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Y = 10
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Width = 75
            Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Height = 75
        End Try

        ' Set text fields values 
        txtX1.Text = Chart1.ChartAreas("Chart Area 1").Position.X.ToString()
        txtY1.Text = Chart1.ChartAreas("Chart Area 1").Position.Y.ToString()
        txtWidth1.Text = Chart1.ChartAreas("Chart Area 1").Position.Width.ToString()
        txtHeight1.Text = Chart1.ChartAreas("Chart Area 1").Position.Height.ToString()

        txtX2.Text = Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.X.ToString()
        txtY2.Text = Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Y.ToString()
        txtWidth2.Text = Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Width.ToString()
        txtHeight2.Text = Chart1.ChartAreas("Chart Area 1").InnerPlotPosition.Height.ToString()
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelsAutoFit = False

        ' Set axis labels font


        ' Set axis labels angle
        '''' FontAngleList.Enabled = !OffsetLabels.Checked
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.FontAngle = Single.Parse(ddlXontanglelist.SelectedItem.Text)

        ' Set offset labels style
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.OffsetLabels = chkXoffset.Checked

        ' Disable X axis labels
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Enabled = chkXenable.Checked

        If xaxiscolor.Value = "" Then
            xaxiscolor.Value = "#000000"
        End If

        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(xaxiscolor.Value)
        '****************************************************************
        ' Disable axis labels text auto fitting
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelsAutoFit = False

        ' Set axis labels font

        ' Set axis labels angle
        '''' FontAngleList.Enabled = !OffsetLabels.Checked
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.FontAngle = Single.Parse(ddlYangle.SelectedItem.Text)

        ' Set offset labels style
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.OffsetLabels = chkYoffset.Checked

        ' Disable X axis labels
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Enabled = chkYenable.Checked
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(xaxiscolor.Value)
        '****************************************************************
        ' Disable axis labels text auto fitting

        If yaxiscolor.Value = "" Then
            yaxiscolor.Value = "#000000"
        End If
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(yaxiscolor.Value)

        ' Set the X Angle
        Chart1.ChartAreas(0).Area3DStyle.XAngle = Single.Parse(ddlXangle.SelectedItem.Value)

        'Set the Y Angle
        Chart1.ChartAreas(0).Area3DStyle.YAngle = Single.Parse(ddlYang.SelectedItem.Value)

        'Set Perspective
        Chart1.ChartAreas(0).Area3DStyle.Perspective = Single.Parse(ddlPerspective.SelectedItem.Value)

        ' Set Back Color 

        Chart1.BackColor = System.Drawing.ColorTranslator.FromHtml(Me.bkcolor.Value) '(bkcolor.Value)
        ' Set Hatch Style 
        Chart1.BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), ddlHatchstyle.SelectedItem.Text), ChartHatchStyle)

        ' Set Gradient Type 
        Chart1.BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), ddlGradient.SelectedItem.Text), GradientType)

        ' Set Border Color 
        Chart1.BorderColor = System.Drawing.ColorTranslator.FromHtml(brcolor.Value)

        ' Set Border Style 
        Chart1.BorderStyle = DirectCast(ChartDashStyle.Parse(GetType(ChartDashStyle), ddlBorderstyle.SelectedItem.Text), ChartDashStyle)

        ' Set Border Width 
        Chart1.BorderWidth = Integer.Parse(ddlBordersize.SelectedItem.Value)
        'Set 3D chart
        chkShow3D.Visible = True
        If (chkShow3D.Checked) Then

            Chart1.ChartAreas("Chart Area 1").Area3DStyle.Enable3D = True
        Else
            Chart1.ChartAreas("Chart Area 1").Area3DStyle.Enable3D = False
        End If
        Chart1.ChartAreas("Chart Area 1").AxisX.Title = txtTitleext.Text
        Chart1.ChartAreas("Chart Area 1").AxisY.Title = txtYTitle.Text
        Chart1.Titles(0).Text = txtCharttitle.Text





        'Set Italic Font changed on 21/10/09
        'If (chkItalic.Checked = True) Then
        '    fontstylecheckbox = FontStyle.Italic
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)

        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        '    'Chart1.Titles(0).Font.Italic = True
        '    'Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont.Italic = True
        '    'Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont.Italic = True


        'End If
        ''Set Undeline font
        'If (chkUline.Checked) Then
        '    fontstylecheckbox3 = FontStyle.Underline
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)

        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)
        'End If
        ''Set Bold font
        'If (chkBold.Checked) Then
        '    fontstylecheckbox1 = FontStyle.Bold
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox1)

        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox1)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox1)
        '    'Chart1.Titles(0).Font.Bold = True
        '    'Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont.Bold = True
        '    'Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont.Bold = True

        'End If
        ''Set Centreline font
        'If (chkSout.Checked) Then
        '    fontstylecheckbox2 = FontStyle.Strikeout
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox2)

        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox2)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox2)
        'End If


        ''Set underline font
        'If (chkUline.Checked) Then
        '    fontstylecheckbox3 = FontStyle.Underline
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)

        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox3)
        'End If
        'code added by atul start, To get all selected Font-Style at one time
        'If (chkItalic.Checked = True And chkBold.Checked And chkUline.Checked And chkSout.Checked) Then
        '    fontstylecheckbox = FontStyle.Italic
        '    fontstylecheckbox += FontStyle.Underline
        '    Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        '    Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        '    Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        'End If

        If (chkItalic.Checked = True) Then
            fontstylecheckbox += FontStyle.Italic
        End If
        If (chkBold.Checked = True) Then
            fontstylecheckbox += FontStyle.Bold
        End If

        If (chkUline.Checked = True) Then
            fontstylecheckbox += FontStyle.Underline
        End If
        If (chkSout.Checked = True) Then
            fontstylecheckbox += FontStyle.Strikeout

        End If
        Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        'code added by atul end
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Format.Replace("k", "").Replace("#", "")


        'Set Strickout font








        ' Set Title color
        If titlefontcolor.Value = "" Then
            yaxiscolor.Value = "#000000"
        End If
        Chart1.ChartAreas("Chart Area 1").AxisX.TitleColor = System.Drawing.ColorTranslator.FromHtml(titlefontcolor.Value)

        If titlefontcolor.Value = "" Then
            titlefontcolor.Value = "#000000"
        End If
        Chart1.ChartAreas("Chart Area 1").AxisY.TitleColor = System.Drawing.ColorTranslator.FromHtml(titlefontcolor.Value)



        ';;;;;;;;;;;;;;;;;;;;;;
        'Set Chart Axis title font

        ' Set Title color
        If titlefontcolor.Value = "" Then
            yaxiscolor.Value = "#000000"
        End If
        Chart1.ChartAreas("Chart Area 1").AxisX.TitleColor = System.Drawing.ColorTranslator.FromHtml(titlefontcolor.Value)

        If titlefontcolor.Value = "" Then
            titlefontcolor.Value = "#000000"
        End If
        Chart1.ChartAreas("Chart Area 1").AxisY.TitleColor = System.Drawing.ColorTranslator.FromHtml(titlefontcolor.Value)


        If titlefontcolor.Value = "" Then
            titlefontcolor.Value = "#000000"
        End If
        Chart1.Titles(0).Color = System.Drawing.ColorTranslator.FromHtml(titlefontcolor.Value) ' Color.FromName(ddlColor1.SelectedItem.Value)

        If titlebordercolor.Value = "" Then
            titlebordercolor.Value = "#000000"
        End If
        Chart1.Titles(0).BorderColor = System.Drawing.ColorTranslator.FromHtml(titlebordercolor.Value) ' Color.FromName(ddlFont1.SelectedItem.Value)


        If titlebkcolor.Value = "" Then
            titlebkcolor.Value = "#ffffff"
        End If
        Chart1.Titles(0).BackColor = System.Drawing.ColorTranslator.FromHtml(titlebkcolor.Value) ' Color.FromName(BackColor.SelectedItem.Value)



        'Set Chart Title alignment
        If Me.Alignment.SelectedIndex = 0 Then
            Chart1.Titles(0).Alignment = ContentAlignment.MiddleLeft
        ElseIf Me.Alignment.SelectedIndex = 1 Then
            Chart1.Titles(0).Alignment = ContentAlignment.MiddleCenter
        Else

            Chart1.Titles(0).Alignment = ContentAlignment.MiddleRight
        End If

        Chart1.Palette = DirectCast(ChartColorPalette.Parse(GetType(ChartColorPalette), ddlPalettes.SelectedItem.ToString), ChartColorPalette)
        'ddlPalettes.DataSource = [Enum].GetNames(GetType(ChartColorPalette))
        'ddlPalettes.DataBind()

        'ddlPalettes.Items.RemoveAt(0)
        'ddlPalettes.Items.Remove("LightBeige")
        'ddlPalettes.Items.Remove("GreenBlue")
        'ddlPalettes.SelectedIndex = 11
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)

        If ddlMajorgridline.SelectedIndex <> 0 Then
            If ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)

            ElseIf ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)

            ElseIf ddlMajorgridline.SelectedItem.Value = "Tickmark" And ddlLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.Enabled = True

            ElseIf ddlMajorgridline.SelectedItem.Value = "Tickmark" And ddlLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.Enabled = True

            End If
        End If
        'Enable Minor selected element
        If ddlMinorType.SelectedIndex <> 0 Then
            If ddlMinorType.SelectedItem.Value = "Grid" And ddlMinoeLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)

            ElseIf ddlMinorType.SelectedItem.Value = "Grid" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)


            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)

            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Enabled = True
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)
            End If
        End If


        ' Set Grid lines and tick marks interval smitha
       



        If Majorgridcolour.Value = "" Then
            Majorgridcolour.Value = "#000000"
        End If
        '' Set Line Color

        'Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        'smitha
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)



        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)
        'smitha
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)

        '' Set Line Width
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        'Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        'Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        'Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        'Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        'smitha
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)

        'smitha
    End Sub
    ''' <summary>
    ''' Stock Chart Formate
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub stockfrormat()
        Dim fontstylecheckbox As New FontStyle
        StockChart.ChartAreas("Price").AxisX.LabelsAutoFit = False

        ' Set axis labels font
        StockChart.ChartAreas("Price").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse(ddlXfontsizelist.SelectedItem.Text))

        ' Set axis labels angle
        '''' FontAngleList.Enabled = !OffsetLabels.Checked
        StockChart.ChartAreas("Price").AxisX.LabelStyle.FontAngle = Single.Parse(ddlXontanglelist.SelectedItem.Text)

        ' Set offset labels style
        StockChart.ChartAreas("Price").AxisX.LabelStyle.OffsetLabels = chkXoffset.Checked

        ' Disable X axis labels
        StockChart.ChartAreas("Price").AxisX.LabelStyle.Enabled = chkXenable.Checked
        StockChart.ChartAreas("Price").AxisY.LabelsAutoFit = False

        ' Set axis labels font
        StockChart.ChartAreas("Price").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse(ddlYlabelfontsize.SelectedItem.Text))

        ' Set axis labels angle
        '''' FontAngleList.Enabled = !OffsetLabels.Checked
        StockChart.ChartAreas("Price").AxisY.LabelStyle.FontAngle = Single.Parse(ddlYangle.SelectedItem.Text)

        ' Set offset labels style
        StockChart.ChartAreas("Price").AxisY.LabelStyle.OffsetLabels = chkYoffset.Checked

        ' Disable X axis labels
        StockChart.ChartAreas("Price").AxisY.LabelStyle.Enabled = chkYenable.Checked
        StockChart.ChartAreas("Price").AxisY.LabelStyle.FontColor = Color.Black 'System.Drawing.ColorTranslator.FromHtml(yaxiscolor.Value)

        ' Set the X Angle
        StockChart.ChartAreas(0).Area3DStyle.XAngle = Single.Parse(ddlXangle.SelectedItem.Value)

        'Set the Y Angle
        StockChart.ChartAreas(0).Area3DStyle.YAngle = Single.Parse(ddlYang.SelectedItem.Value)

        'Set Perspective
        StockChart.ChartAreas(0).Area3DStyle.Perspective = Single.Parse(ddlPerspective.SelectedItem.Value)

        ' Set Back Color 

        StockChart.BackColor = System.Drawing.ColorTranslator.FromHtml(bkcolor.Value)
        ' Set Hatch Style 
        StockChart.BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), ddlHatchstyle.SelectedItem.Text), ChartHatchStyle)

        ' Set Gradient Type 
        StockChart.BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), ddlGradient.SelectedItem.Text), GradientType)

        ' Set Border Color 
        StockChart.BorderColor = System.Drawing.ColorTranslator.FromHtml(brcolor.Value)

        ' Set Border Style 
        StockChart.BorderStyle = DirectCast(ChartDashStyle.Parse(GetType(ChartDashStyle), ddlBorderstyle.SelectedItem.Text), ChartDashStyle)

        'Set Border Width 
        StockChart.BorderWidth = Integer.Parse(ddlBordersize.SelectedItem.Value)
        'Set 3D chart
        chkShow3D.Visible = True
        If (chkShow3D.Checked) Then

            StockChart.ChartAreas("Price").Area3DStyle.Enable3D = True
        Else
            StockChart.ChartAreas("Price").Area3DStyle.Enable3D = False
        End If
        StockChart.ChartAreas("Price").AxisX.Title = txtTitleext.Text
        StockChart.ChartAreas("Price").AxisY.Title = txtYTitle.Text
        'change
        StockChart.Titles(0).Text = txtCharttitle.Text
        Try
            StockChart.ChartAreas("Price").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
            StockChart.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Catch ex As Exception
            StockChart.ChartAreas("Price").AxisX.TitleFont = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
            StockChart.Titles(0).Font = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        End Try
        Try
            StockChart.ChartAreas("Price").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Catch ex As Exception
            StockChart.ChartAreas("Price").AxisY.TitleFont = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        End Try
        StockChart.Titles(0).BackColor = System.Drawing.ColorTranslator.FromHtml(titlebkcolor.Value) ' Color.FromName(BackColor.SelectedItem.Value)
        StockChart.Titles(0).BorderColor = System.Drawing.ColorTranslator.FromHtml(titlebordercolor.Value) ' Color.FromName(ddlFont1.SelectedItem.Value)

        If ddlMajorgridline.SelectedIndex <> 0 Then
            If ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Horizontal" Then
                StockChart.ChartAreas("Price").AxisY.MajorGrid.Enabled = True

            ElseIf ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Vertical" Then
                StockChart.ChartAreas("Price").AxisX.MajorGrid.Enabled = True

            ElseIf ddlMajorgridline.SelectedItem.Value = "Tickmark" And ddlLinetypes.SelectedItem.Value = "Horizontal" Then
                StockChart.ChartAreas("Price").AxisY.MajorTickMark.Enabled = True

            ElseIf ddlMajorgridline.SelectedItem.Value = "Tickmark" And ddlLinetypes.SelectedItem.Value = "Vertical" Then
                StockChart.ChartAreas("Price").AxisX.MajorTickMark.Enabled = True

            End If
        End If
        If ddlMinorType.SelectedIndex <> 0 Then
            If ddlMinorType.SelectedItem.Value = "Grid" And ddlMinoeLinetypes.SelectedItem.Value = "Horizontal" Then
                StockChart.ChartAreas("Price").AxisY.MinorGrid.Enabled = True

            ElseIf ddlMinorType.SelectedItem.Value = "Grid" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                StockChart.ChartAreas("Price").AxisX.MinorGrid.Enabled = True

            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Horizontal" Then
                StockChart.ChartAreas("Price").AxisY.MinorTickMark.Enabled = True

            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                StockChart.ChartAreas("Price").AxisX.MinorTickMark.Enabled = True
            End If
        End If

        StockChart.ChartAreas("Price").AxisX.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)

        StockChart.ChartAreas("Price").AxisX.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        'Chart1.ChartAreas("Price").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        StockChart.ChartAreas("Price").AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        StockChart.ChartAreas("Price").AxisX.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        StockChart.ChartAreas("Price").AxisY.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)

        StockChart.ChartAreas("Price").AxisX.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)
        StockChart.ChartAreas("Price").AxisY.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)

        '' Set Line Width
        StockChart.ChartAreas("Price").AxisX.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        'StockChart.ChartAreas("Price").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        StockChart.ChartAreas("Price").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        'smitha
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        ' Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)

        'smitha
    End Sub
    ''' <summary>
    '''  Chart Legend Appreance and color
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateColor()
        ' Set Legend visual attributes 
        Chart1.Legends("Default").BackColor = System.Drawing.ColorTranslator.FromHtml(legendbkcolor.Value)
        Chart1.Legends("Default").BorderColor = System.Drawing.ColorTranslator.FromHtml(legendbrcolor.Value)
        Chart1.Legends("Default").BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), ddlLegendgradient.SelectedItem.Text), GradientType)
        Chart1.Legends("Default").BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), ddlLegendhatch.SelectedItem.Text), ChartHatchStyle)
        Chart1.Legends("Default").BorderWidth = Integer.Parse(ddlLegendbordersize.SelectedItem.Text)
        Chart1.Legends("Default").BorderStyle = DirectCast(ChartDashStyle.Parse(GetType(ChartDashStyle), ddlLegendborderstyle.SelectedItem.Text), ChartDashStyle)
    End Sub
    ''' <summary>
    ''' Custom Pallete formate
    ''' this is unused code     ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub UpdateCustomPalette()
        Dim colorSet As Color()
        If Me.ddlPalettes.SelectedItem.ToString() = "CorporateGold" Then
            colorSet = New Color(3) {Color.FromArgb(224, 131, 10), Color.FromArgb(255, 227, 130), Color.FromArgb(251, 197, 65), Color.FromArgb(251, 180, 65)}
            Chart1.PaletteCustomColors = colorSet
        ElseIf ddlPalettes.SelectedItem.ToString() = "CorporateBlue" Then

            colorSet = New Color(3) {Color.FromArgb(5, 100, 146), Color.FromArgb(144, 218, 255), Color.FromArgb(149, 193, 254), Color.FromArgb(65, 140, 240)}
            Chart1.PaletteCustomColors = colorSet
        Else

            colorSet = New Color(3) {Color.FromArgb(120, 147, 190), Color.FromArgb(241, 185, 168), Color.FromArgb(128, 184, 209), Color.FromArgb(243, 210, 136)}
            Chart1.PaletteCustomColors = colorSet
        End If
    End Sub
    ''' <summary>
    ''' chart Aligment formate 'unused code'
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Alig(ByVal str As Chart)
        If Me.Alignment.SelectedIndex = 0 Then
            str.Titles(0).Alignment = ContentAlignment.MiddleLeft
        ElseIf Me.Alignment.SelectedIndex = 1 Then
            str.Titles(0).Alignment = ContentAlignment.MiddleCenter
        Else

            str.Titles(0).Alignment = ContentAlignment.MiddleRight
        End If
        Return str
    End Function
    ''' <summary>
    ''' Selected Chart Type Message
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub showchart()
        lblChartimage.Visible = True
        lblChartimage.Text = "You Have Selected " + hidChart.Value + "" + "Chart"
    End Sub
    ''' <summary>
    ''' Pareto Chart Function
    ''' </summary>
    ''' <param name="chart"></param>
    ''' <param name="srcSeriesName"></param>
    ''' <param name="destSeriesName"></param>
    ''' <remarks></remarks>
    Private Sub MakeParetoChart(ByVal chart As Chart, ByVal srcSeriesName As String, ByVal destSeriesName As String)
        ' get name of the ChartAre of the source series 
        Dim strChartArea As String = chart.Series(srcSeriesName).ChartArea
        ' ensure the source series is a column chart type 
        chart.Series(srcSeriesName).Type = SeriesChartType.Column
        ' sort the data in the series to be by values in descending order 
        chart.DataManipulator.Sort(PointsSortOrder.Descending, srcSeriesName)
        ' find the total of all points in the source series 
        Dim total As Double = 0
        For Each pt As DataPoint In chart.Series(srcSeriesName).Points
            total += pt.YValues(0)
        Next
        ' set the max value on the primary axis to total 
        chart.ChartAreas(strChartArea).AxisY.Maximum = total
        ' create the destination series and add it to the chart 
        Dim destSeries As New Series(destSeriesName)
        chart.Series.Add(destSeries)
        ' ensure the destination series is a Line or Spline chart type 
        destSeries.Type = SeriesChartType.Line
        destSeries.BorderWidth = 3
        ' assign the series to the same chart area as the column chart 
        destSeries.ChartArea = chart.Series(srcSeriesName).ChartArea

        ' assign this series to use the secondary axis and set it maximum to be 100% 
        destSeries.YAxisType = AxisType.Secondary
        chart.ChartAreas(strChartArea).AxisY2.Maximum = 100

        ' locale specific percentage format with no decimals 
        chart.ChartAreas(strChartArea).AxisY2.LabelStyle.Format = "P0"

        ' turn off the end point values of the primary X axis 
        chart.ChartAreas(strChartArea).AxisX.LabelStyle.ShowEndLabels = False

        ' for each point in the source series find % of total and assign to series 
        Dim percentage As Double = 0

        For Each pt As DataPoint In chart.Series(srcSeriesName).Points

            Dim pos As Integer = 0
            percentage += (pt.YValues(0) / total * 100)

            pos = destSeries.Points.Add(Math.Round(percentage, 2))
        Next

    End Sub

    Public Sub addgpname()

        ddlgraphname.Visible = True
        lblgraphname.Visible = True
        Dim graphobj As New GraphicalPresentation

        ddlgraphname.DataTextField = "graphname"
        ddlgraphname.DataValueField = "Recordid"
        ddlgraphname.DataSource = graphobj.ReportGraphName(ddlReport.SelectedValue)
        ddlgraphname.DataBind()
        ddlgraphname.Items.Insert(0, "---Select---")
        selectedcols.Items.Clear()
        repcols.Items.Clear()

    End Sub
    Public Sub opengraphname()
        ddlgraphname.Visible = True
        lblgraphname.Visible = True
        Dim graphobj As New GraphicalPresentation

        ddlgraphname.DataTextField = "graphname"
        ddlgraphname.DataValueField = "Recordid"
        If Request.QueryString("currentreport") <> "" Then
            ddlgraphname.DataSource = graphobj.ReportGraphName(txtCurrentReport.Text)


        Else
            ddlgraphname.DataSource = graphobj.ReportGraphName(ddlReport.SelectedItem.Text)
        End If

        ddlgraphname.DataBind()
        boolchart = True
        If ddlgraphname.Items.Count = 0 Then
            aspnet_msgbox("This Report Does Not Contain Any Graph")
            lblgraphname.Visible = False
            ddlgraphname.Visible = False
            boolchart = False
            Exit Sub
        End If
        ddlgraphname.Items.Insert(0, "---Select---")
        selectedcols.Items.Clear()
        repcols.Items.Clear()
    End Sub
#End Region
    'smithastart
    Public Sub checkreportrights(ByVal ndept As String, ByVal nclient As String, ByVal nlob As String, ByVal nrecordid As String)
        


        If Session("typeofuser") = "Admin" Then
            'to check if the user is admin of span
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                .AddWithValue("@Deptid", ndept)
                .AddWithValue("@LOBID", nlob)
                .AddWithValue("@Clientid", nclient)
            End With
            cmdupdate.Dispose()

            Dim readerdata As SqlDataReader
            con.Open()
            readerdata = cmdupdate.ExecuteReader

            Dim status As String = "false"
            If readerdata.HasRows Then
                status = "true"
            End If

            readerdata.Close()
            con.Close()
            'this is to check if the user is the owner of the report
            Dim cmdowner As New SqlCommand("select savedby from idmsreportmaster where recordid='" + nrecordid + "' ", con)
            con.Open()
            Dim owner As String = cmdowner.ExecuteScalar
            con.Close()
            If Trim(LCase(owner)) = Trim(LCase(Session("userid"))) Then
                status = "true"
            End If
            'this is to check if the user has edit right on the report

            Dim cmdeditright As New SqlCommand("select edit from idms_reportrights where recordid='" + nrecordid + "' and userid='" + Session("userid") + "' ", con)
            con.Open()
            Dim editright As String = cmdeditright.ExecuteScalar
            con.Close()
            If Trim(LCase(editright)) = "true" Then
                status = "true"
            End If
            If status = "true" Then
                btnsave.Disabled = False
            Else
                btnsave.Disabled = True
            End If

        ElseIf Session("typeofuser") = "User" Then
            Dim status As String = "false"
            'this is to check if the user is the owner of the report
            Dim cmdowner As New SqlCommand("select savedby from idmsreportmaster where recordid='" + nrecordid + "' ", con)
            con.Open()
            Dim owner As String = cmdowner.ExecuteScalar
            con.Close()
            If Trim(LCase(owner)) = Trim(LCase(Session("userid"))) Then
                status = "true"
            End If
            'this is to check if the user has edit right on the report

            'Dim cmdeditright As New SqlCommand("select edit from idms_reportrights where recordid='" + nrecordid + "' and userid='" + Session("userid") + "' ", con)
            'con.Open()
            'Dim editright As String = cmdeditright.ExecuteScalar
            'con.Close()
            'If Trim(LCase(editright)) = "true" Then
            '    status = "true"
            'End If
            'If status = "true" Then
            '    btnsave.Disabled = False
            'Else
            '    btnsave.Disabled = True
            'End If
        End If


    End Sub
    'smithaend
#Region "Button_Click"
    Protected Sub ShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport.Click
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
            checkreportrights(dept_id, client_id, lobid, ddlReport.SelectedValue)

        End If
        Try
            selectedcols.Items.Clear()
            If txtCurrentReport.Text = "" Then
                If DepartmentName.SelectedItem.Text = "--Select--" Then
                    aspnet_msgbox("Please Select Department.")
                    Exit Sub
                End If
            End If
            If txtTodate.Text <> "" And txtFromdate.Text <> "" Then
                Dim graphobj As New GraphicalPresentation
                Dim datacheck As String = txtTodate.Text
                Dim datefromchk As String = txtFromdate.Text
                Dim strchk As String = graphobj.datecheck(datacheck)
                Dim strfromchk As String = graphobj.datecheck(datefromchk)
                If strchk = 1 Or strfromchk = 1 Then
                    lbldatemsg.Visible = True
                    lbldatemsg.Text = " * Please Enter Date in mm/dd/yyyy format."
                    Exit Sub
                ElseIf strchk = 2 Or strfromchk = 2 Then
                    lbldatemsg.Visible = False
                End If
                If strchk = 1 And strfromchk = 1 Then
                    lbldatemsg.Visible = True
                    lbldatemsg.Text = " * Please Enter Date in mm/dd/yyyy format."
                    Exit Sub
                ElseIf strchk = 2 And strfromchk = 2 Then
                    lbldatemsg.Visible = False
                End If
            Else
                lbldatemsg.Visible = False
            End If
            gridreportbind()
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try
    End Sub
    Protected Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click
     

   Dim stre As String = repcols.Items.Count()
        Dim i As String
        Dim iloopindex As Integer
        Dim co As Boolean
        co = False
        Dim selectedarr
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
                        hid.Value = stt
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
                            hid.Value = stt
                        End If
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles remove.Click
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

    Protected Sub btnGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraph.Click
        hidwheretxt.Value = Session("hidwheretxt").ToString()
        hidorder.Value = Session("hidordertxt").ToString()
        hidgroup.Value = Session("hidgroupby").ToString()
        hidhaving.Value = Session("hidhavingtxt").ToString()
        Session("deptid") = DepartmentName.SelectedValue
        Session("clientid") = ClientName.SelectedValue
        Session("lobid") = ddlLobName
        If (txtCurrentReport.Visible = True) Then
            Session("reportname") = txtCurrentReport.Text
            DepartmentName.Enabled = False
            ClientName.Enabled = False
            ddlLob.Enabled = False

        Else
            Session("reportname") = ddlReport.SelectedItem.Text
        End If

        'If (hidwheretxt.Value.Contains("where")) Then
        'ElseIf (hidwheretxt.Value = "") Then
        'Else
        '    hidwheretxt.Value = "where" + hidwheretxt.Value

        'End If
        'If (hidhaving.Value.Contains("having")) Then
        'ElseIf (hidhaving.Value = "") Then
        'Else
        '    hidhaving.Value = "having" + hidhaving.Value

        'End If

        'If (hidorder.Value.Contains("order by")) Then
        'ElseIf (hidorder.Value = "") Then
        'Else
        '    hidorder.Value = "order by" + hidorder.Value

        'End If

        'If (hidgroup.Value.Contains("group by")) Then
        'ElseIf (hidgroup.Value = "") Then
        'Else
        '    hidgroup.Value = "group by" + hidgroup.Value

        'End If


        If txtCurrentReport.Text = "" Then
            If ddlgraphname.Visible = True And ddlgraphname.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Graph Name.")
                Exit Sub

            End If
        Else
            If ddlgraphname.Visible = True And ddlgraphname.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Graph Name.")
                Exit Sub
            End If
        End If
        makechart()
        If chkanimated.Checked = True Then
            ' Set Flash chart image type 
            Chart1.ImageType = ChartImageType.Flash
            ' Set Legend moving animation 
            Dim legendAnimation As New MovingAnimation()
            legendAnimation.AnimatedElements.Add(Chart1.Legends(0))
            legendAnimation.StartTime = 0
            legendAnimation.Duration = 2
            legendAnimation.StartPositionX = 100.0F
            legendAnimation.StartPositionY = 5.9F
            legendAnimation.OneByOne = CheckBoxLegend.Checked
            Chart1.CustomAnimation.Add(legendAnimation)
            ' Set series moving animation 
            Dim startTime As Single = 0
            For Each ser As Series In Chart1.Series
                Dim seriesLineAnimation As New MovingAnimation()
                seriesLineAnimation.AnimatedElements.Add(ser)
                seriesLineAnimation.StartTime = startTime
                seriesLineAnimation.Duration = 2
                seriesLineAnimation.OneByOne = CheckBoxPoints.Checked
                Chart1.CustomAnimation.Add(seriesLineAnimation)
                ' Adjust series animation start time 
                If CheckBoxSeries.Checked Then
                    startTime += 2.0F
                End If
            Next

        End If
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
            checkreportrights(dept_id, client_id, lobid, ddlReport.SelectedValue)

        End If
        hidhaving.Value = ""
        hidwheretxt.Value = ""
        hidorder.Value = ""
        hidgroup.Value = ""
        Session("graphname") = lblChartimage.Text
        ' Session("hidwheretxt") = ""
        'Session("hidordertxt") = ""
        'Session("hidgroupby") = ""
        'Session("hidhavingtxt") = ""
        'btnsave.Disabled = False
    End Sub

    Protected Sub imgColumn_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgcolumn.Click
        hidChart.Value = "Column"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgPie_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPie.Click
        hidChart.Value = "Pie"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgArea_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgArea.Click
        hidChart.Value = "Area"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgLine_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgLine.Click
        hidChart.Value = "Line"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgHistogram_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgHistogram.Click
        hidChart.Value = "Histogram"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub imgsPareto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPareto.Click
        hidChart.Value = "Pareto"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub imgStock_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStock.Click
        hidChart.Value = "Stock"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub imgDaughnt_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgDaughnt.Click
        hidChart.Value = "Daughnt"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgBar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgBar.Click
        hidChart.Value = "Bar"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgScatter_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgScatter.Click
        hidChart.Value = "Scatter"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub imgRun_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgRun.Click
        hidChart.Value = "Run"
        rbnRow.Visible = False
        rbnRow.Checked = False
        showchart()
    End Sub

    Protected Sub lnkColumnchart_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkColumnchart.Click
        hidChart.Value = "Column"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        hidChart.Value = "Area"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton3.Click
        hidChart.Value = "Pie"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        hidChart.Value = "Line"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        hidChart.Value = "Scatter"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton6.Click
        hidChart.Value = "Histogram"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub LinkButton7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton7.Click
        hidChart.Value = "Pareto"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub LinkButton9_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton9.Click
        hidChart.Value = "Scaterplot"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton10_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton10.Click
        hidChart.Value = "Stock"
        rbnRow.Visible = False
        rbnRow.Checked = False
        rbnColumn.Checked = True
        showchart()
    End Sub

    Protected Sub LinkButton11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton11.Click
        hidChart.Value = "Daughnt"
        rbnRow.Visible = True
        showchart()
    End Sub

    Protected Sub LinkButton12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton12.Click
        hidChart.Value = "Bar"
        rbnRow.Visible = True
        showchart()
    End Sub
    
    Protected Sub btnOpenGraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenGraph.Click
        Response.Redirect("open.aspx?val=2")
    End Sub

    Protected Sub LinkButton8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton8.Click
        hidChart.Value = "Run"
        rbnRow.Visible = False
        showchart()
    End Sub

    Protected Sub imgScaterplot_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgScaterplot.Click
        hidChart.Value = "Scaterplot"
        rbnRow.Visible = True
        showchart()
    End Sub

#End Region

#Region "SelectedIndexChanged"
    Protected Sub ddlDepartmant_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartmant.SelectedIndexChanged

        If ddlDepartmant.SelectedIndex = 0 Then
            txtTodate.Text = ""
            txtFromdate.Text = ""
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            ddlReport.Items.Clear()
            ddlClient.Items.Clear()
        End If

        txtTodate.Text = ""
        txtFromdate.Text = ""
        lbldatemsg.Visible = False
        selectedcols.Items.Clear()
        repcols.Items.Clear()
        Try
            If ddlDepartmant.SelectedIndex <> 0 Then
                Me.ddlLob.Items.Clear()
                Me.ddlClient.DataSource = fun.bind_client(ddlDepartmant.SelectedValue)
                Me.ddlClient.DataTextField = "ClientName"
                Me.ddlClient.DataValueField = "AutoID"
                Me.ddlClient.DataBind()
                Me.ddlClient.Items.Insert("0", "--Select--")
                Me.ddlClient.Dispose()
                ddlReport.Items.Clear()
                'dept = ddlDepartmant.SelectedValue
                'client = "0"
                'lob = "0"
                hiddclient.Value = client
                hiddlob.Value = lob
                ddlReport.DataTextField = "QueryName"
                ddlReport.DataValueField = "Recordid"
                If (Session("typeofuser") = "Admin") Then
                    Dim exist As Boolean = False
                    exist = selectRep.chkAdminSpan(Session("userid"))
                    If exist = True Then
                        ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                    Else
                        GoTo adminOutofIndex
                    End If

                ElseIf (Session("typeofuser") = "Super Admin") Then
                    ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
                Else
adminOutofIndex:
                    Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                    If (scope = "Local") Then
                        ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                    Else
                        ddlReport.DataSource = selectRep.reportFornonlocal(Session("userid"))
                    End If
                End If
                ddlReport.DataBind()
                If ddlReport.Items.Count > 0 Then
                    ddlReport.Items.Insert(0, "--Select--")
                End If
            End If
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
        End Try
    End Sub

    Protected Sub ddlClient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlClient.SelectedIndexChanged
        If ddlClient.SelectedItem.Text = "--Select--" Then
            ddlLob.Items.Clear()
            ddlLob.Items.Insert(0, "--Select--")
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "--Select--")
        Else
            ddlReport.Items.Clear()
            ddlLob.Items.Clear()
            If ddlDepartmant.SelectedIndex <> 0 And ddlClient.SelectedIndex <> 0 Then
                Dim classobj As New Functions
                ddlLob.DataTextField = "LOBName"
                ddlLob.DataValueField = "AutoID"
                ddlLob.DataSource = fun.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
                ddlLob.DataBind()
                ddlLob.Items.Insert(0, "--Select--")
                dept = ddlDepartmant.SelectedValue
                If ddlClient.SelectedValue = "" Or ddlClient.SelectedValue = "--Select--" Then
                    client = "0"
                Else
                    client = ddlClient.SelectedValue
                End If
                hiddclient.Value = client
                hiddlob.Value = lob
                lob = "0"
                ddlReport.DataTextField = "QueryName"
                ddlReport.DataValueField = "Recordid"
                If (Session("typeofuser") = "Admin") Then
                    Dim exist As Boolean = False
                    exist = selectRep.chkAdminSpan(Session("userid"))
                    If exist = True Then
                        ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                    Else
                        GoTo adminOutofIndex
                    End If

                ElseIf (Session("typeofuser") = "Super Admin") Then
                    ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
                Else
adminOutofIndex:
                    Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                    If (scope = "Local") Then
                        ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                    Else
                        ddlReport.DataSource = selectRep.reportFornonlocal(Session("userid"))
                    End If
                End If
                ddlReport.DataBind()
                If ddlReport.Items.Count > 0 Then
                    ddlReport.Items.Insert(0, "--Select--")
                End If

            End If
        End If
    End Sub

    Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
        If ddlLob.SelectedItem.Text <> "--Select--" Then
            ddlReport.Items.Clear()
            dept = ddlDepartmant.SelectedValue
            client = ddlClient.SelectedValue
            lob = ddlLob.SelectedValue
            hiddclient.Value = client
            hiddlob.Value = lob
            ddlReport.DataTextField = "QueryName"
            ddlReport.DataValueField = "Recordid"
            If (Session("typeofuser") = "Admin") Then
                Dim exist As Boolean = False
                exist = selectRep.chkAdminSpan(Session("userid"))
                If exist = True Then
                    ddlReport.DataSource = selectRep.reportForadmin(Session("userid"), dept, client, lob)
                Else
                    GoTo adminOutofIndex
                End If

            ElseIf (Session("typeofuser") = "Super Admin") Then
                ddlReport.DataSource = selectRep.reportForSA(Session("userid"), dept, client, lob)
            Else
adminOutofIndex:
                Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                If (scope = "Local") Then
                    ddlReport.DataSource = selectRep.reportForlocal(Session("userid"), dept, client, lob)
                Else
                    ddlReport.DataSource = selectRep.reportFornonlocal(Session("userid"))
                End If
            End If
            ddlReport.DataBind()
            If ddlReport.Items.Count > 0 Then
                ddlReport.Items.Insert(0, "--Select--")
            End If
        End If
    End Sub

    Protected Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReport.SelectedIndexChanged
        btnOpenGraph.Enabled = True
        If ddlReport.SelectedIndex = 0 Then
            txtCurrentReport.Enabled = False
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            txtTodate.Text = ""
            txtFromdate.Text = ""
        End If
        If ddlReport.SelectedIndex <> 0 Then
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            
        End If
    End Sub

    Protected Sub Perspective_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPerspective.SelectedIndexChanged
        'Set the Perspective
        Chart1.ChartAreas(0).Area3DStyle.Perspective = Single.Parse(ddlPerspective.SelectedItem.Value)
        StockChart.ChartAreas(0).Area3DStyle.Perspective = Single.Parse(ddlPerspective.SelectedItem.Value)
    End Sub

    Protected Sub XAngle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlXangle.SelectedIndexChanged
        'Set the X Angle
        Chart1.ChartAreas(0).Area3DStyle.XAngle = Single.Parse(ddlXangle.SelectedItem.Value)
        StockChart.ChartAreas(0).Area3DStyle.XAngle = Single.Parse(ddlXangle.SelectedItem.Value)
    End Sub

    Protected Sub YAngle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlYang.SelectedIndexChanged
        'Set the Y Angle
        Chart1.ChartAreas(0).Area3DStyle.YAngle = Single.Parse(ddlYang.SelectedItem.Value)
        StockChart.ChartAreas(0).Area3DStyle.YAngle = Single.Parse(ddlYang.SelectedItem.Value)
    End Sub

    Protected Sub Gradient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlGradient.SelectedIndexChanged
        ddlHatchstyle.SelectedIndex = 0
        ' Set Gradient Type 
        Chart1.BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), ddlGradient.SelectedItem.Text), GradientType)
        Chart1.BackHatchStyle = ChartHatchStyle.None
        StockChart.BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), ddlGradient.SelectedItem.Text), GradientType)
        StockChart.BackHatchStyle = ChartHatchStyle.None
    End Sub


    Protected Sub HatchStyle_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlHatchstyle.SelectedIndexChanged
        ddlGradient.SelectedIndex = 0
        ' Set Hatch Style 
        Chart1.BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), ddlHatchstyle.SelectedItem.Text), ChartHatchStyle)
        Chart1.BackGradientType = GradientType.None
        StockChart.BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), ddlHatchstyle.SelectedItem.Text), ChartHatchStyle)
        StockChart.BackGradientType = GradientType.None
    End Sub

    Protected Sub ddlPalettes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPalettes.SelectedIndexChanged
        Chart1.PaletteCustomColors = New Color(-1) {}
        Me.Chart1.Palette = DirectCast(ChartColorPalette.Parse(GetType(ChartColorPalette), Me.ddlPalettes.SelectedItem.ToString()), ChartColorPalette)
        StockChart.PaletteCustomColors = New Color(-1) {}
        Me.StockChart.Palette = DirectCast(ChartColorPalette.Parse(GetType(ChartColorPalette), Me.ddlPalettes.SelectedItem.ToString()), ChartColorPalette)
    End Sub

    


#End Region

#Region "Check_Changed"
    Protected Sub rbnRow_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnRow.CheckedChanged
        chartseries()
    End Sub

    Protected Sub rbnColumn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnColumn.CheckedChanged
        chartseries()
    End Sub
#End Region

#Region "Commented_Code"
    'Protected Sub btnZoom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnZoom.Click
    '    'scrolling and zooming will force keeping of series data between callbacks. 
    '    Me.Chart1.ChartAreas("Chart Area 1").CursorX.UserEnabled = Me.ddlAxisList.SelectedIndex = 0 OrElse Me.ddlAxisList.SelectedIndex = 2

    '    Me.Chart1.ChartAreas("Chart Area 1").CursorY.UserEnabled = Me.ddlAxisList.SelectedIndex = 1 OrElse Me.ddlAxisList.SelectedIndex = 2

    '    ' Set restriction on how far the user can zoom in 
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.MinSize = 5
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.MinSize = 50

    '    ' Check AJAXZoomEnabled property. 
    '    If CheckBoxAJAXZoomEnabled.Checked Then
    '        Chart1.AJAXZoomEnabled = True
    '    Else
    '        Chart1.AJAXZoomEnabled = False
    '    End If


    '    'If Not Me.Page.IsPostBack Then

    '    ' Set initial X axis zooming 
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Position = 10
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Size = 25

    '    ' Populate chart with random data 
    '    ' Dim rand As New Random(976896)
    '    'Dim yValue1 As Double = rand.[Next](0, 200)
    '    'Dim yValue2 As Double = yValue1 + rand.[Next](-50, 50)
    '    'For index As Integer = 1 To 100
    '    '    Me.Chart1.Series("Series1").Points.AddXY(index, yValue1)
    '    '    yValue1 += rand.[Next](-20, 20)
    '    '    Me.Chart1.Series("Series2").Points.AddXY(index, yValue2)
    '    '    yValue2 += rand.[Next](-20, 20)
    '    'Next
    '    'End If
    'End Sub

    'Protected Sub ButtonReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonReset.Click
    '    ' Reset current X and Y axes views 

    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Position = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Size = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Position = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Size = Double.NaN
    'End Sub

    'Protected Sub ddlAxisList_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAxisList.SelectedIndexChanged
    '    ' Reset current X and Y axes views 
    '    Chart1.CallbackStateContent = CallbackStateContent.All
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Position = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Size = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Position = Double.NaN
    '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Size = Double.NaN

    '    ' Enable/disable zoominf for different axis 
    '    Me.Chart1.ChartAreas("Chart Area 1").CursorX.UserEnabled = False
    '    Me.Chart1.ChartAreas("Chart Area 1").CursorY.UserEnabled = False
    '    If Me.ddlAxisList.SelectedIndex = 1 OrElse Me.ddlAxisList.SelectedIndex = 2 Then
    '        Me.Chart1.ChartAreas("Chart Area 1").CursorX.UserEnabled = True

    '        ' Set initial X axis zooming 
    '        Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Position = 10
    '        Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Size = 25
    '    End If
    '    If Me.ddlAxisList.SelectedIndex = 2 OrElse Me.ddlAxisList.SelectedIndex = 3 Then
    '        Me.Chart1.ChartAreas("Chart Area 1").CursorY.UserEnabled = True

    '        ' Set initial Y axis zooming 
    '        Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Position = 200
    '        Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.Size = 100
    '    End If
    'End Sub

    'Protected Sub CheckBoxAJAXZoomEnabled_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxAJAXZoomEnabled.CheckedChanged

    'End Sub

    ''Protected Sub ddlTheme_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTheme.SelectedIndexChanged
    ''    'UpdateChartData()
    ''End Sub
    ''Private Sub UpdateChartData()
    ' ''Dim rand As New Random()
    ''Chart1.ImageType = ChartImageType.Flash
    ' '' Set animation theme 
    ''Chart1.AnimationTheme = DirectCast([Enum].Parse(GetType(AnimationTheme), ddlTheme.SelectedItem.Text), AnimationTheme)

    ' '' Set Flash/Svg chart image type 
    ' ''Chart1.ImageType = IIf((DropDownListImageType.SelectedIndex = 0), ChartImageType.Flash, ChartImageType.Svg)

    ' '' Set 3D chart flag 
    ' ''Chart1.ChartAreas("Default").Area3DStyle.Enable3D = CheckBox3D.Checked

    ' '' Set chart type 
    ''Dim index As Integer = 0
    ''For Each series As Series In Chart1.Series
    ''    '    ' Set chart type 
    ''    series.ChartType = hidChart.Value

    ''    ' Adjust chart appearance depending on the series type 
    ''    If hidChart.Value = "Line" Then
    ''        'pointNumber = 25; 
    ''        series.BorderWidth = IIf((chkShow3D.Checked), 1, 3)
    ''    ElseIf hidChart.Value = "Column" OrElse hidChart.Value = "Bar" Then
    ''        If index <> 0 Then
    ''            series.Enabled = False
    ''        End If
    ''    ElseIf hidChart.Value = "Pie" OrElse hidChart.Value = "Doughnut" Then
    ''        If index <> 0 Then
    ''            series.Enabled = False
    ''        End If
    ''    ElseIf hidChart.Value = "Area" Then
    ''        If index <> 0 Then
    ''            series.Enabled = False
    ''        End If
    ''    ElseIf hidChart.Value = "Stock" Then
    ''        If index <> 0 Then
    ''            series.Enabled = False
    ''        End If
    ''    End If
    ''    ' Increase series index 
    ''    index += 1
    ''Next

    'End Sub
    'Protected Sub Chart1_Callback(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs)
    '    If e.CommandName = "RESIZE" Then
    '        ' Parse command arguments 
    '        Dim argumentString As String = e.CommandArgument.ToString()
    '        Dim arguments As String() = argumentString.Split(";"c)

    '        Dim height As Integer, width As Integer
    '        If Integer.TryParse(arguments(0), height) AndAlso Integer.TryParse(arguments(1), width) Then
    '            ' Use this for Chart: 
    '            Chart1.Height = height

    '            ' Use this for Gauge: 
    '            'GaugeContainer1.Height = height; 
    '            'GaugeContainer1.Width = width; 

    '            ' Use this for Map: 
    '            'MapControl1.Height = height; 
    '            'MapControl1.Width = width; 
    '            Chart1.Width = width
    '        End If
    '    End If
    'End Sub
#End Region

   
    Protected Sub ddlLinetypes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLinetypes.SelectedIndexChanged

    End Sub

    
    Public Sub gridreportbindlalit()
        Dim com, com1 As SqlCommand
        Dim Data As SqlDataReader
        Dim colsplit, fcolsplit, fcollarray, arr, tablearray As String()
        Dim coll As String = String.Empty
        Dim asstring, tablename, columnname As String

        If txtCurrentReport.Text = "" Then
            com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "'  and  departmentid='" + ddlDepartmant.SelectedValue + "'", con)
        End If
        con.Open()
        readquery = com1.ExecuteReader
        While readquery.Read()
            tablename = readquery("tablename")
        End While
        tablename = tablename.Replace("~", ",")
        tablearray = tablename.Split(",")
        hidtablename.Value = tablename

        com1.Dispose()
        con.Close()
        com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "' and  departmentid='" + ddlDepartmant.SelectedValue + "'", con)

        con.Open()
        readquery = com.ExecuteReader
        While readquery.Read()
            colname = readquery("colname")
        End While
        con.Close()
        colsplit = colname.Split("~")
        Dim i As Integer
        For i = 0 To colsplit.Length - 1
            If (colsplit(i).ToString().Contains(" AS ") Or colsplit(i).ToString().Contains(" as ")) Then
                fcolsplit = colsplit(i).Split(LCase(" AS "))
                If (coll <> "") Then

                    asstring = fcolsplit(2).ToString()
                    asstring = asstring.Replace("[", "")
                    asstring = asstring.Replace("]", "")
                    coll = coll + "@" + asstring
                Else
                    asstring = fcolsplit(2).ToString()
                    asstring = asstring.Replace("[", "")
                    asstring = asstring.Replace("]", "")
                    coll = asstring
                End If
            Else
                If (coll <> "") Then

                    arr = colsplit(i).ToString().Split("$")

                    coll = coll + "@" + arr(1).ToString()
                Else
                    arr = colsplit(i).ToString().Split("$")
                    coll = arr(1).ToString()
                End If
            End If
        Next
        fcollarray = coll.Split("@")
        repcols.DataSource = fcollarray
        repcols.DataBind()



        Dim repName As String = ""
        com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        Data = com1.ExecuteReader
        While Data.Read()
            If txtCurrentReport.Text = "" Then
                repName = ddlReport.SelectedItem.Text
                Session("repname") = repName
                If Data("name") = "tab" & ddlReport.SelectedItem.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            Else
                repName = txtCurrentReport.Text
                Session("repname") = repName
                ddlReport.Visible = False
                If Data("name") = "tab" & txtCurrentReport.Text Then
                    b = False
                    Exit While
                Else
                    b = True
                End If
            End If


        End While
        com1.Dispose()
        con.Close()
        hidcolname.Value = columnname
        hidtabname.Value = tablename


        bindnormalarry()

        'Actual_CREW.LOB;Sum(Actual_CREW.FTE_Required) % [FTEs]

    End Sub

    Public Sub bindnormalarry()

        Dim strcol, strformulaname, strfinalfor, strfcol, strspace, strcomma As String
        Dim readquery As SqlDataReader
        Dim com As SqlCommand
        Dim colarry As String()


        If txtCurrentReport.Text = "" Then
            com = New SqlCommand("select colname, wheredata, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "' and  departmentid='" + ddlDepartmant.SelectedValue + "'", con)
        Else
            If hidDept = "" Then
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            Else
                com = New SqlCommand("select colname, wheredata,groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "' and  departmentid='" + hidDept + "'", con)
            End If

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
                If bnm.contains("$As$") = True Or bnm.contains("String.fromCharCode(34)") = True Or bnm.contains("$+$") = True Or bnm.contains("$as$") = True Then
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
            datestr = strnorcol
            datestr = Replace(datestr, "@Date1@", txtFromdate.Text)
            datestr = Replace(datestr, "@Date2@", txtTodate.Text)
            Normalarray.Value = datestr
            If strfcol <> "" Then
                strfcol = strfcol.Replace("$", ".")
                strfcol = strfcol.Replace("AS", "%")
                Dim datestr1 As String
                datestr1 = strfcol
                datestr1 = Replace(datestr1, "@Date1@", txtFromdate.Text)
                datestr1 = Replace(datestr1, "@Date2@", txtTodate.Text)
                formulaarray.Value = datestr1
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
            If Date1 = True Then
                If txtTodate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill TO DATE")
                    Exit Sub
                End If
            End If
            If Date2 = True Then
                If txtFromdate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill From DATE")

                    Exit Sub
                End If
            End If

            colname = strspace

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
        assghidval()
    End Sub
    Public Sub assghidval()
        Dim wheretxt, groupbytext, orderbytext, havingtxt As String
        Dim tablearray As String()
        wheretxt = ""

        


        Dim astr = colname + " " + formula + " " + groupby + " " + orderby
        Dim Date11 As Boolean = False
        Dim Date22 As Boolean = False

        If astr.Contains("@Date1@") = False And astr.Contains("@Date2@") = False And txtFromdate.Text <> "" And txtTodate.Text <> "" Then
            wheretxt = "" & " " & formula
            Dim ftext As String = "date" + " " + "between" + " " + "'" + txtFromdate.Text + "'" + " " + "and" + " " + "'" + txtTodate.Text + "'"
            If formula <> "" Then
                wheretxt = "" + ftext
            Else
                wheretxt = "" & " " & ftext
            End If

            hidwheretxt.Value = wheretxt
        ElseIf astr.Contains("@Date1@") = True And astr.Contains("@Date2@") = True Then

            Date11 = astr.Contains("@Date1@")
            Date22 = astr.Contains("@Date2@")
            If Date11 = True Then
                If txtTodate.Text = "" Then
                    aspnet_msgbox(" Plz Fill To Date")
                    Exit Sub
                End If
            End If
            If Date22 = True Then
                If txtFromdate.Text = "" Then
                    aspnet_msgbox("Fill From Date")
                    Exit Sub
                End If
            End If
            If formula <> "" Then
                wheretxt = "" & " " & formula
                formula = formula.Replace("'@Date1@'", "'" + txtFromdate.Text + "'")
                formula = formula.Replace("'@Date2@'", "'" + txtTodate.Text + "'")
                wheretxt = "" & " " & formula
                wheretxt = wheretxt.Replace("$", ".")
                hidwheretxt.Value = wheretxt
            End If

        Else
            wheretxt = "" & " " & formula
            wheretxt = wheretxt.Replace("$", ".")
            hidwheretxt.Value = wheretxt
        End If
        If formula = "" And txtFromdate.Text = "" And txtTodate.Text = "" Then
            wheretxt = ""
            hidwheretxt.Value = ""
        End If
        If groupby <> "" Then
            groupbytext = "" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")
            hidgroup.Value = groupbytext
        Else
            groupbytext = ""
        End If
        If orderby <> "" Then
            orderbytext = "" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            hidorder.Value = orderbytext
        Else
            orderbytext = ""
        End If
        If havingcondition <> "" Then
            havingtxt = "" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            hidhaving.Value = havingtxt
        Else
            havingtxt = ""
        End If
        con.Close()
        readquery.Close()
    End Sub

    
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        ddlLobName.Items.Clear()
        ClientName.Items.Clear()
        ddlReport.Items.Clear()
        If (DepartmentName.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please Select Department First")
            ddlLobName.Items.Clear()
            ClientName.Items.Clear()
            ddlReport.Items.Clear()
        Else
            con.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ClientName.DataSource = dr
            ClientName.DataTextField = "ClientName"
            ClientName.DataValueField = "autoid"
            ClientName.DataBind()
            ClientName.Items.Insert(0, "--Select--")
            deptid = DepartmentName.SelectedValue
            clientid = 0
            lobid = 0
            bind_report(deptid, clientid, lobid)
        End If
        
    End Sub

    Protected Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        deptid = DepartmentName.SelectedValue
        clientid = ClientName.SelectedValue
        lobid = ddlLobName.SelectedValue
        bind_report(deptid, clientid, lobid)
    End Sub
    Public Function bind_report(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        con.Close()
        con.Open()
        cmd = New SqlCommand("select * from IDMSReportMaster where DepartmentId='" + deptid + "' and ClientId='" + clientid + "' and UnderLOB='" + lobid + "' and SavedBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlReport.DataSource = dr
        ddlReport.DataTextField = "QueryName"
        ddlReport.DataValueField = "Recordid"
        ddlReport.DataBind()
        ddlReport.Items.Insert(0, "--Select--")
    End Function
    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        ddlLobName.Items.Clear()
        'ClientName.Items.Clear()
        ddlReport.Items.Clear()
        If (ClientName.SelectedValue = "--Select--" Or ClientName.SelectedValue = "") Then
            ddlLobName.Items.Clear()
            'ClientName.Items.Clear()
            ddlReport.Items.Clear()
            bind_report(DepartmentName.SelectedValue, 0, 0)
        Else
            con.Open()
            cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ddlLobName.DataSource = dr
            ddlLobName.DataTextField = "LOBName"
            ddlLobName.DataValueField = "autoid"
            ddlLobName.DataBind()
            ddlLobName.Items.Insert(0, "--Select--")
            deptid = DepartmentName.SelectedValue
            clientid = ClientName.SelectedValue
            lobid = 0
            bind_report(deptid, clientid, lobid)
        End If
        
    End Sub

    Protected Sub get_report_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_report.Click
        con.Close()
        con.Open()
        cmd = New SqlCommand("select * from IDMSReportMaster where DepartmentId='60' and ClientId='0' and UnderLOB='0' and SavedBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlReport.DataSource = dr
        ddlReport.DataTextField = "QueryName"
        ddlReport.DataValueField = "Recordid"
        ddlReport.DataBind()
        ddlReport.Items.Insert(0, "--select--")
    End Sub

    Protected Sub ShowReport_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport_singleuser.Click
        checkreportrights(60, 0, 0, ddlReport.SelectedValue)
        Try
            selectedcols.Items.Clear()
            'If txtCurrentReport.Text = "" Then
            '    aspnet_msgbox("Please Select Report")
            '        Exit Sub
            'End If
            If txtTodate.Text <> "" And txtFromdate.Text <> "" Then
                Dim graphobj As New GraphicalPresentation
                Dim datacheck As String = txtTodate.Text
                Dim datefromchk As String = txtFromdate.Text
                Dim strchk As String = graphobj.datecheck(datacheck)
                Dim strfromchk As String = graphobj.datecheck(datefromchk)
                If strchk = 1 Or strfromchk = 1 Then
                    lbldatemsg.Visible = True
                    lbldatemsg.Text = " * Please Enter Date in mm/dd/yyyy format."
                    Exit Sub
                ElseIf strchk = 2 Or strfromchk = 2 Then
                    lbldatemsg.Visible = False
                End If
                If strchk = 1 And strfromchk = 1 Then
                    lbldatemsg.Visible = True
                    lbldatemsg.Text = " * Please Enter Date in mm/dd/yyyy format."
                    Exit Sub
                ElseIf strchk = 2 And strfromchk = 2 Then
                    lbldatemsg.Visible = False
                End If
            Else
                lbldatemsg.Visible = False
            End If
            gridreportbind_single()
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try

    End Sub

    Protected Sub btnGraph_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGraph_singleuser.Click
        hidwheretxt.Value = Session("hidwheretxt").ToString()
        hidorder.Value = Session("hidordertxt").ToString()
        hidgroup.Value = Session("hidgroupby").ToString()
        hidhaving.Value = Session("hidhavingtxt").ToString()

        Session("deptid") = 60
        Session("clientid") = 0
        Session("lobid") = 0
        If (txtCurrentReport.Visible = True) Then
            Session("reportname") = txtCurrentReport.Text
        Else
            Session("reportname") = ddlReport.SelectedItem.Text
        End If

        'If (hidwheretxt.Value.Contains("where")) Then
        'ElseIf (hidwheretxt.Value = "") Then
        'Else
        '    hidwheretxt.Value = "where" + hidwheretxt.Value

        'End If
        'If (hidhaving.Value.Contains("having")) Then
        'ElseIf (hidhaving.Value = "") Then
        'Else
        '    hidhaving.Value = "having" + hidhaving.Value

        'End If

        'If (hidorder.Value.Contains("order by")) Then
        'ElseIf (hidorder.Value = "") Then
        'Else
        '    hidorder.Value = "order by" + hidorder.Value

        'End If

        'If (hidgroup.Value.Contains("group by")) Then
        'ElseIf (hidgroup.Value = "") Then
        'Else
        '    hidgroup.Value = "group by" + hidgroup.Value

        'End If


        If txtCurrentReport.Text = "" Then
            If ddlgraphname.Visible = True And ddlgraphname.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Graph Name.")
                Exit Sub

            End If
        Else
            If ddlgraphname.Visible = True And ddlgraphname.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Graph Name.")
                Exit Sub
            End If
        End If
        makechart1()
        If chkanimated.Checked = True Then
            ' Set Flash chart image type 
            Chart1.ImageType = ChartImageType.Flash
            ' Set Legend moving animation 
            Dim legendAnimation As New MovingAnimation()
            legendAnimation.AnimatedElements.Add(Chart1.Legends(0))
            legendAnimation.StartTime = 0
            legendAnimation.Duration = 2
            legendAnimation.StartPositionX = 100.0F
            legendAnimation.StartPositionY = 5.9F
            legendAnimation.OneByOne = CheckBoxLegend.Checked
            Chart1.CustomAnimation.Add(legendAnimation)
            ' Set series moving animation 
            Dim startTime As Single = 0
            For Each ser As Series In Chart1.Series
                Dim seriesLineAnimation As New MovingAnimation()
                seriesLineAnimation.AnimatedElements.Add(ser)
                seriesLineAnimation.StartTime = startTime
                seriesLineAnimation.Duration = 2
                seriesLineAnimation.OneByOne = CheckBoxPoints.Checked
                Chart1.CustomAnimation.Add(seriesLineAnimation)
                ' Adjust series animation start time 
                If CheckBoxSeries.Checked Then
                    startTime += 2.0F
                End If
            Next

        End If
        Dim dept_id, client_id, lobid As String
        client_id = "0"
        dept_id = "60"
        lobid = "0"
        checkreportrights(dept_id, client_id, lobid, ddlReport.SelectedValue)
        hidhaving.Value = ""
        hidwheretxt.Value = ""
        hidorder.Value = ""
        hidgroup.Value = ""
        'Session("graphname") = lblChartimage.Text.Substring(18)
        Session("hidwheretxt") = ""
        Session("hidordertxt") = ""
        Session("hidgroupby") = ""
        Session("hidhavingtxt") = ""
        btnsave.Disabled = False
    End Sub
    Protected Sub btnOpenGraph_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOpenGraph_singleuser.Click
        Response.Redirect("open_singleuser.aspx?val=2")
    End Sub
End Class



