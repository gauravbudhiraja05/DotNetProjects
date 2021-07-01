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
    Dim classobj As New Functions
    Dim ds2 As New DataSet
    Dim repobj As New ReportDesigner
    Dim graphobj As New GraphicalPresentation
    Dim conn As String = AppSettings("ConnectionString")
    Dim conn1 As String = AppSettings("ConnectionString")
    Dim conn2 As String = AppSettings("ConnectionString")
    Dim cmd As SqlCommand
    Dim con As New SqlConnection(conn)
    Dim con2 As New SqlConnection(conn2)
    Dim con1 As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim reportcolumn As DataColumn
    Dim colname, formula, groupby, formultxt, orderby, havingcondition, seriesName, sortedseries, repcolumn, columnseries, dupcol As String
    Dim fontname As FontFamily
    Public graph As Chart
    Public stt As String
    Dim p
    Dim txtformulaarr
    Dim j As Integer = 0
    Public gridstring As String
    Dim bool As Boolean
    Dim Duplicate As String
    Dim counter As Double = 0.0
    Dim count, t, iloopindex, blankravl As Integer
    Dim arr(30) As String
    Dim label1 As Label
    Dim label2 As Label
    Dim label3 As Label
    Dim label4 As Label
    Dim dsImage As New DataSet
    Dim gg As Label
    Public dept As String = "1"
    Public client As String = "0"
    Public lob As String = "0"
    Dim imgcounter As Integer = 0

    Public currsp As String
    ''' <summary>
    '''  These Global variables are defined to store the data of a report.
    '''  This data is to pass to the parent window.
    ''' The data is passed through a client side function named as assignToparent().
    ''' </summary>
    ''' <remarks></remarks>
    Public repName As String = ""
    'Public repType As String = "Simple"
    Public hidgraphtype As String = ""
    Public hidgraphname As String = ""
    Public hidcolumnname As String = ""
    Public hidcolumnseries As String = ""
    Public hidtodate As String = ""
    Public hidfromdate As String = ""
    Public hidcommanformat As String = ""
    Public hidspecificproperties As String = ""
    Public hidtotalcolumn As String = ""
    Public hidcreatedon As String = ""
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
        txtCurrentReport.Text = Request.QueryString("currentreport")
        currsp = txtCurrentReport.Text
        If txtCurrentReport.Text <> "" Then
            txtCurrentReport.Visible = True
            rbnReport.Checked = True
            ddlReport.Visible = False
        ElseIf txtCurrentReport.Text = "" Then
            txtCurrentReport.Visible = False
            ddlReport.Visible = True
        End If
        If rbnRow.Checked = True Then
            seriesbtn.Value = "Row"
        End If

        If Page.IsPostBack = False Then
            rbnRow.Checked = True
            btnsave.Disabled = True
            lblChartimage.Visible = False
            ddlAnalysistable.Visible = False
            labelAnalysis.Visible = False
            btnDelete.Visible = False
            btnUpdate.Visible = False
            ShowReport.Visible = True
            Button1.Visible = False
            Opengraph.Visible = False
            'gridframe.Visible = True
            'gdGraphreport.Visible = False
            'divdgShowImage.Visible = False
            Selectreport.Visible = True
            btnreset.Visible = False
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


        End If
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Enabled = False
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MajorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisY.MajorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MajorTickMark.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MinorGrid.Enabled = False
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.Enabled = False
        graph = Chart1

        divdaughnt.Style.Add("display", "none")
        divpie.Style.Add("display", "none")
        divScatter.Style.Add("display", "none")
        divArea.Style.Add("display", "none")
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

        ' Display legend in the "Default" chart area 
        If chk_InsideChartArea.Checked Then
            Chart1.Legends("Default").InsideChartArea = "Default"
        End If

        ' Set table style 
        Me.Chart1.Legends("Default").TableStyle = DirectCast(LegendTableStyle.Parse(GetType(LegendTableStyle), Me.ddlTheTableStyle.SelectedItem.ToString()), LegendTableStyle)

        If Me.ddlLegendStyleList.SelectedItem.ToString() = "Table" AndAlso Not Me.chk_Disabled.Checked Then
            Me.ddlTheTableStyle.Enabled = True
        Else

            Me.ddlTheTableStyle.Enabled = False
        End If
    End Sub
    ''' <summary>
    ''' Bind Gridview with Analysis Reports
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub gridbind()
        If rbnReport.Checked = False And rbnAnalysis.Checked = True Then
            Dim anada As New SqlDataAdapter
            Dim anastr As String
            anastr = "select * from  " + ddlAnalysistable.SelectedItem.Text
            Session("reportquery") = anastr
            com = New SqlCommand(anastr, con)
            con.Open()
            anada.SelectCommand = com
            anada.Fill(ds2)
            Dim Adpanalysis As New SqlDataAdapter
            Dim dsanalysis As New DataSet
            Dim Analysiscolumn As DataColumn
            Dim Analysistabcol As String
            Dim Analysisarry
            Adpanalysis.SelectCommand = com
            Adpanalysis.Fill(dsanalysis)
            For Each Analysiscolumn In dsanalysis.Tables(0).Columns
                If Analysistabcol = "" Then
                    Analysistabcol = Analysiscolumn.ColumnName()
                Else
                    Analysistabcol = Analysistabcol & "," & Analysiscolumn.ColumnName()
                End If
                Analysisarry = Analysistabcol.Split(",")
            Next
            totalcol.Value = Analysistabcol
            repcols.DataSource = Analysisarry
            repcols.DataBind()
            con.Close()
            Exit Sub
        End If
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
        firstColumn.HeaderBackColor = Color.WhiteSmoke
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
    Public Sub gridreportbind()
        Dim clientAna, lobana As String
        clientAna = ddlClient.SelectedValue
        If clientAna = "" Or clientAna = "---select---" Then
            clientAna = "0"
        Else
            clientAna = ddlClient.SelectedValue
        End If
        lobana = ddlLob.SelectedValue
        If lobana = "" Or lobana = "---select---" Then
            lobana = "0"
        Else
            lobana = ddlLob.SelectedValue
        End If
        If txtCurrentReport.Text = "" Then
            com = New SqlCommand("select colname, wheredata,txtformula, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "' and  departmentid='" + ddlDepartmant.SelectedValue + "' and  clientid='" + clientAna + "' and  underlob='" + lobana + "'", con)
            ' hidreport.Value = ddlReport.SelectedItem.Text
        Else
            com = New SqlCommand("select colname, wheredata,txtformula, groupby, orderby,havingcondition from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
            'hidreport.Value = txtCurrentReport.Text
        End If

        con.Open()
        Dim columnname, cname, tabcolumn, reportDept, reportclient, reportlob, currentcolumn, currenttable, tname, wheretxt, orderbytext, groupbytext, havingtxt, totalquerycloumn, strspace, strDollar, strcomma, txtformula1 As String
        Dim aa1
        Dim b As Boolean
        Dim da As New SqlDataAdapter
        Dim da1 As New SqlDataAdapter

        Dim cmd As SqlCommand
        Dim ds As New DataSet
        Dim colfinal As Integer
        Dim ttcount, cccount, p2, g2, po, tabcolength, tabcount, alltabcol, allcol, q, r As Integer

        Dim final
        Dim repcolarray
        Dim data As SqlDataReader
        Dim tablearray
        Dim colarry
        Dim colsss
        Dim tablename As String
        Dim colsa As DataColumn
        ' Dim reportcolumn As DataColumn
        readquery = com.ExecuteReader
        While readquery.Read()
            colname = readquery("colname")
            colarry = colname.Split("~")

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
                End If
            End If
            If Date2 = True Then
                If txtFromdate.Text = "" Then
                    lbldatemsg.Visible = False
                    aspnet_msgbox("Plz Fill From DATE")
                End If
            End If
            strspace = strspace.Replace("@Date1@", txtFromdate.Text)
            strspace = strspace.Replace("@Date2@", txtTodate.Text)
            strspace = strspace.Replace("String.fromCharCode(34)", "")
            strspace = strspace.Replace("+", "")
            colname = strspace
            formula = readquery("wheredata")
            formultxt = readquery("txtformula")
            groupby = readquery("groupby")
            orderby = readquery("orderby")
            havingcondition = readquery("havingcondition")
        End While
        'Dim col, col1 As Integer
        'Dim lk = colarry.length
        'Dim finalcolarry(50) As String
        'Dim str, str1 As String
        'Dim bool As Boolean
        'Dim cnt As Integer = 0
        'For col = 0 To colarry.length - 1

        '    bool = False
        '    Dim bool1 = False
        '    str = colarry(col)
        '    For col1 = 0 To colarry.length - 1
        '        If col = col1 Then
        '            colarry(col) = colarry(col1)
        '            bool1 = True
        '            Exit For
        '        ElseIf colarry(col) = colarry(col1) And col <> col1 Then
        '            bool = True
        '        End If
        '    Next
        '    If bool1 = True Then

        '        finalcolarry(cnt) = colarry(col)
        '        cnt = cnt + 1
        '    ElseIf bool = False And bool1 = False Then
        '        finalcolarry(cnt) = colarry(col)
        '        cnt = cnt + 1
        '    End If

        'finalcolarry = colarry(col)
        '  Next

        wheretxt = ""
        If formula <> "" Then
            Dim Date1 As Boolean = False
            Dim Date2 As Boolean = False
            Date1 = formula.Contains("@Date1@")
            Date2 = formula.Contains("@Date2@")
            If Date1 = True Then
                If txtTodate.Text = "" Then
                    aspnet_msgbox(" Plz Fill To Date")
                    Exit Sub
                End If
            End If
            If Date2 = True Then
                If txtFromdate.Text = "" Then
                    aspnet_msgbox("Fill From Date")
                    Exit Sub
                End If
            End If
            wheretxt = "where" & " " & formula
            formula = formula.Replace("'@Date1@'", "'" + txtFromdate.Text + "'")
            formula = formula.Replace("'@Date2@'", "'" + txtTodate.Text + "'")
            wheretxt = "where" & " " & formula
            wheretxt = wheretxt.Replace("$", ".")
            hidwheretxt.Value = wheretxt
        Else
            wheretxt = ""
        End If
        If formultxt <> "" Then
            txtformula1 = formultxt
            txtformula1 = txtformula1.Replace("$", ".")
            txtformula1 = txtformula1.Replace("~", "+")
            txtformula1 = txtformula1.Replace("AS", "%")
            formulaarray.Value = txtformula1 '

        Else
            txtformula1 = ""
        End If
        If groupby <> "" Then
            groupbytext = "group by" & " " & groupby
            groupbytext = groupbytext.Replace("$", ".")
            hidgroup.Value = groupbytext
        Else
            groupbytext = ""
        End If
        If orderby <> "" Then
            orderbytext = "order by" & " " & orderby
            orderbytext = orderbytext.Replace("$", ".")
            hidorder.Value = orderbytext
        Else
            orderbytext = ""
        End If
        If havingcondition <> "" Then
            havingtxt = "having" & " " & havingcondition
            havingtxt = havingtxt.Replace("$", ".")
            hidhaving.Value = havingtxt
        Else
            havingtxt = ""
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

        If txtCurrentReport.Text = "" Then
            com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + ddlReport.SelectedItem.Text + "'  and  departmentid='" + ddlDepartmant.SelectedValue + "'  and  clientid='" + clientAna + "'  and  underlob='" + lobana + "'", con)

        Else
            com1 = New SqlCommand("select tablename from idmsreportmaster where queryname='" + txtCurrentReport.Text + "'", con)
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


        reotablename.Value = tablename
        tabcount = UBound(tablearray)
        con.Close()
        readquery.Close()
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
        '------------------------
        Dim repName As String = ""
        com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
        con.Open()
        data = com1.ExecuteReader
        While data.Read()
            If txtCurrentReport.Text = "" Then
                repName = ddlReport.SelectedItem.Text
                Session("repname") = repName
                'hidreportname.Value = repName
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
                'hidreportname.Value = repName
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

        If b = False Then
            'com1 = New SqlCommand("select * from  " + "tab" & repName + "", con)
            com1 = New SqlCommand("select " + columnname + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext + " ", con)
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
                        ' dataoperation.Visible = False
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
                cmd = New SqlCommand("select Identity(int, 1,1) as RecordId,  " + columnname + " into " + "tab" & repName + " from " + tablename + " " + wheretxt + " " + groupbytext + " " + havingtxt + " " + orderbytext + " ", con)
                con.Open()
                cmd.ExecuteNonQuery()
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                ddlReport.Visible = True
                Exit Sub
            End Try
            con.Close()
            com1 = New SqlCommand("select * from " + "tab" & repName + "", con)
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
            'gridframe.Visible = True
            'gdGraphreport.Visible = True
            ''divgridReport.Style.Add("display", "block")
            'gdGraphreport.DataSource = ds2
            'gdGraphreport.DataBind()
            con.Close()
        End If
        'End If
    End Sub
    ''' <summary>
    ''' Multiple Chart Creation
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub makechart()
        Try
            Chart2.Visible = False
            StockChart.Visible = False
            'divdgShowImage.Visible = False
            dupcol = ""
            blankravl = 0
            'If ddlGraphname.SelectedIndex = 0 Then
            If Opengraph.Visible = True Then
                If ddlDepartment.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select Department.")
                    Exit Sub
                ElseIf rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = False Then
                    aspnet_msgbox("Please Select Graph Type As Analysis Or Report.")
                    Exit Sub
                ElseIf rbnOpenanalysis.Checked = True Then
                    If ddlOpenreport.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select ReportName.")
                        Exit Sub
                    ElseIf ddlOpenanalysistable.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select AnalysisReportName.")
                        Exit Sub
                    End If
                ElseIf rbnOpenreport.Checked = True Then
                    If ddlOpenreport.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select ReportName.")
                        Exit Sub
                    End If
                End If
            End If

            If Selectreport.Visible = True Then
                If txtCurrentReport.Text = "" Then

                    If ddlDepartmant.SelectedIndex = 0 Then
                        aspnet_msgbox("Please Select Department.")
                        Exit Sub

                    ElseIf rbnAnalysis.Checked = True Then
                        If ddlReport.SelectedIndex = 0 Then
                            aspnet_msgbox("Please Select ReportName.")
                            Exit Sub
                        ElseIf ddlAnalysistable.SelectedIndex = 0 Then
                            aspnet_msgbox("Please Select AnalysisReportName.")
                            Exit Sub
                        End If
                    ElseIf rbnReport.Checked = True Then
                        If ddlReport.SelectedIndex = 0 Then
                            aspnet_msgbox("Please Select ReportName.")
                            Exit Sub
                        End If
                    End If
                    If repcols.SelectedIndex = -1 Then
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
                    'If hidChart.Value = "" Then
                    '    aspnet_msgbox("Please Select Chart Type")
                    '    Exit Sub
                    'End If
                    If hidChart.Value = "Histogram" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                    If hidChart.Value = "Run" And rbnColumn.Checked = False Then
                        aspnet_msgbox("Please Select Chart Series")
                        Exit Sub
                    End If
                ElseIf txtCurrentReport.Text <> "" Then

                    If repcols.SelectedIndex = -1 Then
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
                    'If hidChart.Value = "" Then
                    '    aspnet_msgbox("Please Select Chart Type")
                    '    Exit Sub
                    'End If
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
            Dim x, y, n, b, m1, nn, fmname
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
            openselectedcolumn.Value = x
            If rbnReport.Checked = True And rbnAnalysis.Checked = False Then
                If txtCurrentReport.Text <> "" Then
                    y = txtCurrentReport.Text
                ElseIf txtCurrentReport.Text = "" Then
                    y = ddlReport.SelectedItem.Text
                End If
            ElseIf rbnReport.Checked = False And rbnAnalysis.Checked = True Then
                n = ddlAnalysistable.SelectedItem.Text
            End If
            'If rbnReport.Checked = False And rbnAnalysis.Checked = False Then
            '    y = txtCurrentReport.Text
            'End If
            If rbnOpenreport.Checked = True And rbnOpenanalysis.Checked = False Then
                y = ddlOpenreport.SelectedItem.Text
            ElseIf rbnOpenreport.Checked = False And rbnOpenanalysis.Checked = True Then
                n = ddlOpenanalysistable.SelectedItem.Text
            End If
            Dim sp As String()
            Dim formulaname As String()
            sp = x.Split(",")
            j = UBound(sp)
            ' Initialize a connection string	
            Dim myConnectionString As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
            ' Define the database query	
            Dim mySelectQuery As String

            If rbnReport.Checked = True And rbnAnalysis.Checked = False And chkSunnarized.Checked = False Then
                mySelectQuery = "select " + x + " from " + "tab" & y + " order by " & sp(0) + ""
            ElseIf rbnReport.Checked = False And rbnAnalysis.Checked = True Then
                mySelectQuery = "select " + x + " from " + n + " order by " & sp(0) + ""
            ElseIf rbnReport.Checked = True And rbnAnalysis.Checked = False And chkSunnarized.Checked = True Then
                If hidgroup.Value = "" Then
                    aspnet_msgbox("This is not Summarized Report")
                    Exit Sub
                End If
                Dim ll, jj, kk, pp As Integer
                Dim num, num1, num2 As Integer
                Dim strvalue, finalformula, stre As String
                stre = formulaarray.Value
                If stre = "" Then
                    aspnet_msgbox("This is not valid data for Summarized Report ")
                    Exit Sub
                End If
                txtformulaarr = stre.Split("+")
                For ll = 0 To txtformulaarr.length - 1
                    strvalue = txtformulaarr(ll)
                    num1 = strvalue.LastIndexOf("%")
                    num2 = strvalue.Length
                    num = num2 - num1
                    finalformula = strvalue.Substring(num1 + 1, num - 1)
                    finalformula = finalformula.Trim(" ")
                    For jj = 0 To sp.Length - 1
                        If sp(jj) = finalformula Then
                            If b = "" Then
                                b = strvalue.Replace("%", "AS")
                                fmname = finalformula
                            Else
                                b = b + "," + strvalue.Replace("%", "AS")
                                fmname = fmname + "," + finalformula
                            End If
                        End If
                    Next

                Next
                y = reotablename.Value
                '  For kk = 0 To sp.Length - 1
                formulaname = fmname.split(",")
                For pp = 0 To formulaname.Length - 1
                    If sp(pp) <> formulaname(pp) Then
                        If nn = "" Then
                            nn = sp(pp)
                        Else
                            nn = nn + "," + sp(pp)
                        End If
                    End If
                Next


                ' Next
                m1 = nn + "," + b
                mySelectQuery = "select " + m1 + " from " + y + " Group By " & nn + " order by " & sp(0) + ""
            End If
            
            If rbnOpenreport.Checked = True And rbnOpenanalysis.Checked = False Then
                mySelectQuery = "select " + x + " from " + "tab" & y + " order by " & sp(0) + ""
            ElseIf rbnOpenreport.Checked = False And rbnOpenanalysis.Checked = True Then
                mySelectQuery = "select " + x + " from " + n + " order by " & sp(0) + ""
            End If

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
                    Dim rval As Integer
                    Dim dval As Date
                    Dim sval As String
                    Dim str As String
                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                Try
                                    dval = CDate(row(rowname)).Date
                                Catch ex1 As Exception
                                    If rval <> 0 Then
                                        For t = 0 To arr.Length - 1
                                            arr(t) = ""
                                        Next
                                        ' GoTo label1
                                    End If
                                    sval = row(rowname)
                                    arr(i) = sval
                                End Try
                                If sval = "" Then
                                    arr(i) = dval
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
                                    str = str + " " + " " + +arr(jk)
                                End If
                            Else
                                Exit For
                            End If
                        Next
                        Dim YVal As Integer
                        Dim dateval As Date
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
                            For colIndex = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    'If dateval <> "" Then
                                    Chart1.Series(str).Points.Clear()
                                    'End If
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    'series.ToolTip = "Series:" + str + " , " + "value:" + YVal.ToString() + " , " + "Point:" + columnName
                                Catch ex As Exception
                                    Try
                                        dateval = CDate(row(columnName))
                                        dateval = dateval.Date.ToString
                                        Chart1.Series(str).Points.AddXY(columnName, dateval)
                                    Catch ex1 As Exception
                                        'dateval = ""
                                    End Try
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2

                End If

                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, columncount1, colintval As Integer
                    Dim cc, colseries, colstrval, clumnser As String
                    Dim counter As Integer
                    Dim strcol As String

                    counter = 0
                    Dim colarr(10) As String
                    Dim col As DataRow
                    Dim dat As DateTime
                    'Dim iLoopIndex As Integer
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    If dat <> "" Then

                                    End If

                                    Exit For
                                Catch ex As Exception
                                    If strcol = "" Then
                                        Chart1.Series(strcol).Points.Clear()
                                    End If

                                    Try
                                        dat = CDate(col(colname))
                                        If dat = "#12:00:00 AM#" Then
                                            dat = ""
                                        End If
                                        colarr(columncount) = cc
                                    Catch ex1 As Exception

                                    End Try
                                    colstrval = col(colname)
                                    'If colintval <> 0 Then
                                    '    'If colstrval <> dupcol Then
                                    '    '    For iloopindex = 0 To colarr.Length - 1
                                    '    '        colarr(iloopindex) = ""
                                    '    '        Chart1.Series.Clear()
                                    '    '    Next
                                    '    '    GoTo label1
                                    '    'End If
                                    'ElseIf dat <> "" Then
                                    '    'If colstrval <> dupcol Then
                                    '    '    For iloopindex = 0 To colarr.Length - 1
                                    '    '        colarr(iloopindex) = ""
                                    '    '        Chart1.Series.Clear()
                                    '    '    Next
                                    '    '    GoTo label1
                                    '    'End If
                                    'End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
                        Catch ex As Exception

                        End Try
                        Dim yo As Integer

                        Dim strnothing As String = ""
                        Dim YVal1 As Integer

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
                        'If colstrval = "" Then
                        '    GoTo label1
                        'End If
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
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(columncount1).ToString()
                                        arrcolumn(10) = columnseries
                                        Dim dateval As Date
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Try
                                                If dat = "#12:00:00 AM#" Then
                                                    counter = 1
                                                    clumnser = row1(counter).ToString()
                                                    Chart1.Series(strcol).Points.AddXY(columnseries + " " + clumnser, YVal1)
                                                End If
                                            Catch ex As Exception
                                                Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                            End Try
                                        Catch ex As Exception
                                            Try

                                                dateval = CDate(row1(colname))
                                                Chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                            Catch ex1 As Exception
                                            End Try
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        Chart1.Series(strcol).LegendToolTip = "Legend" + strcol
                                        Chart1.Series(strcol).XValueIndexed = True '.XValueType = ChartValueTypes.String
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                    Next row1
                                    'counter = counter + 1
                                    ' Next
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount

                End If
                GoTo label2
                'Set Pareto chart
            ElseIf hidChart.Value = "Run" Then
                Chart2.Visible = False
                Chart1.Visible = True

                Dim aSubGroup(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim aData(myDataSet.Tables(0).Rows.Count - 1) As Single
                Dim st1 As String = ""
                Dim st2 As String = ""

                'rbnRow.Visible = False
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()

                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
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
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        columnseries = row1(0).ToString()

                                        Try
                                            YVal1 = CSng(row1(0))
                                            data = CSng(row1(1))
                                            aData(runi) = data
                                            aSubGroup(runi) = YVal1
                                        Catch ex As Exception
                                            xval = row1(colname)
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
                        Dim tmpSeries As Series = Run_Chart.CreateSeries(aSubGroup, aData, Chart1)
                        ' Optionally before calling any chart creation function, you can setup styles for 
                        ' Control Lines. 
                        Run_Chart.UCLstyle.LineStyle = ChartDashStyle.Solid
                        Run_Chart.UCLstyle.LineColor = Color.Red
                        Run_Chart.UCLstyle.LineWidth = 2
                        ' Also you can set style for text 
                        ' Run_Chart.ShowText = True
                        Run_Chart.LCLstyle.TextColor = Color.Blue
                        Run_Chart.LCLstyle.TextFont = New Font("Arial", 10)
                        GoTo label2
                    Next columncount
                End If
            ElseIf hidChart.Value = "Pareto" Then
                Chart2.Visible = False
                Dim strcol As String
                'rbnRow.Visible = False
                Chart1.Visible = True
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
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
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
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
                Dim strcol As String
                Chart1.Visible = False
                Chart2.Visible = True
                'rbnRow.Visible = False
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
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
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart2.Series(strcol).Points.AddY(YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            'Chart2.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart2.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
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

                divpie.Style.Add("display", "block")
                divdaughnt.Style.Add("display", "none")
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
                            End Try
                        Next i
                        '    Try
                        '        rval = CInt(row(rowname))
                        '    Catch ex As Exception
                        '        sval = row(rowname)
                        '        arr(i) = sval
                        '    End Try
                        'Next i
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
                        Dim YVal As Integer
                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                'End If
                                Try
                                    YVal = CInt(row(columnName))
                                    Dim series1 As String
                                    series1 = columnName
                                    If columnName = dup Then
                                        columnName = ""
                                    End If
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    Dim st1 As String
                                    st1 = columnName.Replace(columnName, "")
                                    st1 = series1
                                    dup = st1
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Point=#VALX"

                                If ddlLabelstylelist.SelectedValue = "Outside" Then
                                    ddlPielinearrowsize.Enabled = True
                                    ddlPielinearrowtype.Enabled = True

                                    For Each series As Series In Chart1.Series
                                        series("PieLineArrowType") = ddlPielinearrowtype.SelectedValue
                                        series("PieLineArrowSize") = ddlPielinearrowsize.SelectedValue
                                    Next
                                Else
                                    ddlPielinearrowsize.Enabled = False
                                    ddlPielinearrowtype.Enabled = False
                                End If

                                If Me.chkShowlegend.Checked = True Then
                                    Chart1.Series(str)("PieLabelStyle") = "Disabled"

                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                End If

                                ' Set labels style 
                                Chart1.Series(str)("PieLabelStyle") = Me.ddlLabelstylelist.SelectedItem.ToString()
                                ' Set the PieLabelOffset Custom Attribute. 
                                Chart1.Series(str)("PieLabelOffset") = ddlPielabeloffsetlist.SelectedValue
                                ' Pie drawing style 
                                Me.ddldrawing.Enabled = True
                                Chart1.Series(0)("PieDrawingStyle") = Me.ddldrawing.SelectedItem.ToString()
                                If Me.chkShowlegend.Checked Then
                                    Me.Chart1.Legends(0).Enabled = True
                                Else
                                    Me.Chart1.Legends(0).Enabled = False
                                End If
                            Next colIndex
                            'Dim blankravl As Integer = 0
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval, strcol As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series(strcol).Type = SeriesChartType.Pie
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT"
                                        If ddlLabelstylelist.SelectedValue = "Outside" Then
                                            ddlPielinearrowsize.Enabled = True
                                            ddlPielinearrowtype.Enabled = True

                                            For Each series As Series In Chart1.Series
                                                series("PieLineArrowType") = ddlPielinearrowtype.SelectedValue
                                                series("PieLineArrowSize") = ddlPielinearrowsize.SelectedValue
                                            Next
                                        Else
                                            ddlPielinearrowsize.Enabled = False
                                            ddlPielinearrowtype.Enabled = False
                                        End If

                                        If Me.chkShowlegend.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Disabled"

                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                        End If

                                        ' Set labels style 
                                        Chart1.Series(strcol)("PieLabelStyle") = Me.ddlLabelstylelist.SelectedItem.ToString()
                                        ' Set the PieLabelOffset Custom Attribute. 
                                        Chart1.Series(strcol)("PieLabelOffset") = ddlPielabeloffsetlist.SelectedValue
                                        ' Pie drawing style 
                                        Me.ddldrawing.Enabled = True
                                        Chart1.Series(0)("PieDrawingStyle") = Me.ddldrawing.SelectedItem.ToString()
                                        If Me.chkShowlegend.Checked Then
                                            Me.Chart1.Legends(0).Enabled = True
                                        Else
                                            Me.Chart1.Legends(0).Enabled = False
                                        End If
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
                divpie.Style.Add("display", "none")
                divdaughnt.Style.Add("display", "none")
                divArea.Style.Add("display", "block")
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                ' Disable/enable X axis margin 
                                Chart1.ChartAreas("Chart Area 1").AxisX.Margin = chkShowmargins.Checked
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If

                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Area
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        ' Disable/enable X axis margin 
                                        Chart1.ChartAreas("Chart Area 1").AxisX.Margin = chkShowmargins.Checked
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Bar
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            ' Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        ' Use point index for drawing the chart 
                                        Chart1.Series(strcol).XValueIndexed = True

                                        '' Get sorting order 
                                        'Dim order As PointsSortOrder = PointsSortOrder.Ascending
                                        'order = PointsSortOrder.Descending
                                        'Chart1.DataManipulator.Sort(order, "Y", strcol)
                                        'If ddlSortorderlist.SelectedItem.Text = "Descending" Then
                                        '    order = PointsSortOrder.Descending
                                        'End If

                                        '' Sort series data points 
                                        'If ddlSortlist.SelectedItem.Text = "Y Value" Then
                                        '    Chart1.DataManipulator.Sort(order, "X", strcol)
                                        'End If
                                        'If ddlSortlist.SelectedItem.Text = "Y2 Value (Radius)" Then
                                        '    Chart1.DataManipulator.Sort(order, "Y", strcol)
                                        'End If

                                        ''Enable sort order control 
                                        'If ddlSortlist.SelectedItem.Text <> "Unsorted" Then
                                        '    ddlSortorderlist.Enabled = True
                                        'End If
                                        'Chart1.Series(seriesName).ShowLabelAsValue = True
                                        'Chart1.Series(seriesName).ShowInLegend = True
                                        'Chart1.Series(seriesName).MarkerStyle = MarkerStyle.Star4
                                        'Chart1.Series(seriesName).MarkerColor = Color.Aqua
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
                divdaughnt.Style.Add("display", "block")
                divpie.Style.Add("display", "none")
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        ' Dim chartareastr As ChartArea
                        'Chart1.ChartAreas.Add(chartareastr)
                        Chart1.Series(str).Type = SeriesChartType.Doughnut
                        Chart1.Series(str).BorderWidth = 2
                        Dim st As String
                        st = str.Replace(count - 1, "")
                        st = series0
                        Duplicate = st
                        Dim colIndex As Integer
                        Dim columnName As String
                        Dim bb As String = ""
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX" & "Percentage=#PERCENT"
                                If ddlDaughntlablestyle.SelectedValue = "Outside" Then
                                    ddlDaughntlinesize.Enabled = True
                                    ddlDaughntlinestyle.Enabled = True

                                    For Each series As Series In Chart1.Series
                                        series("PieLineArrowType") = ddlDaughntlinestyle.SelectedValue
                                        series("PieLineArrowSize") = ddlDaughntlinesize.SelectedValue
                                    Next
                                Else
                                    ddlDaughntlinesize.Enabled = False
                                    ddlDaughntlinestyle.Enabled = False
                                End If

                                If Me.chkDaughntshowlegend.Checked = True Then
                                    Chart1.Series(seriesName)("PieLabelStyle") = "Disabled"

                                Else
                                    Chart1.Series(str)("PieLabelStyle") = "Inside"
                                End If

                                ' Set labels style 
                                Chart1.Series(str)("PieLabelStyle") = Me.ddlDaughntlablestyle.SelectedItem.ToString()
                                ' Set the PieLabelOffset Custom Attribute. 
                                Chart1.Series(str)("PieLabelOffset") = ddlDaughntoffset.SelectedValue

                                Chart1.Series(seriesName).BorderColor = Color.MidnightBlue
                                'Chart1.Series(seriesName).Color = Color.Brown


                                ' Pie drawing style 
                                Me.ddlDaughntdrawing.Enabled = True
                                Chart1.Series(0)("PieDrawingStyle") = Me.ddlDaughntdrawing.SelectedItem.ToString()

                                ' Set Doughnut hole size 
                                Chart1.Series(str)("DoughnutRadius") = Me.ddlHoleSizeList.SelectedItem.ToString()

                                ' Disable Doughnut hole size control 
                                Me.ddlHoleSizeList.Enabled = True

                                If Me.chkDaughntshowlegend.Checked Then
                                    Me.Chart1.Legends(0).Enabled = True
                                Else
                                    Me.Chart1.Legends(0).Enabled = False
                                End If
                                'Chart1.Series(str).ShowLabelAsValue = True
                                Chart1.Series(str).ShowInLegend = True
                                'Chart1.Series(str).MarkerStyle = MarkerStyle.Circle
                                'Chart1.Series(str).MarkerColor = Color.Aqua
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If


                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Doughnut
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX" & "Percentage=#PERCENT"
                                        If ddlDaughntlablestyle.SelectedValue = "Outside" Then
                                            ddlDaughntlinesize.Enabled = True
                                            ddlDaughntlinestyle.Enabled = True

                                            For Each series As Series In Chart1.Series
                                                series("PieLineArrowType") = ddlDaughntlinestyle.SelectedValue
                                                series("PieLineArrowSize") = ddlDaughntlinesize.SelectedValue
                                            Next
                                        Else
                                            ddlDaughntlinesize.Enabled = False
                                            ddlDaughntlinestyle.Enabled = False
                                        End If

                                        If Me.chkDaughntshowlegend.Checked = True Then
                                            Chart1.Series(strcol)("PieLabelStyle") = "Disabled"

                                        Else
                                            Chart1.Series(strcol)("PieLabelStyle") = "Inside"
                                        End If

                                        ' Set labels style 
                                        Chart1.Series(strcol)("PieLabelStyle") = Me.ddlDaughntlablestyle.SelectedItem.ToString()

                                        ' Explode selected country 

                                        '***********************************************
                                        ' Explode selected country 

                                        ' ExplodedPointList.Items.Add(columnseries)
                                        'For Each point As DataPoint In Chart1.Series(seriesName).Points
                                        '    point("Exploded") = "false"
                                        '    If point.AxisLabel = Me.ExplodedPointList.SelectedItem.ToString() Then
                                        '        point("Exploded") = "true"
                                        '    End If
                                        'Next
                                        '*************

                                        ' Set the PieLabelOffset Custom Attribute. 
                                        Chart1.Series(strcol)("PieLabelOffset") = ddlDaughntoffset.SelectedValue

                                        'Chart1.Series(seriesName).BorderColor = Color.MidnightBlue
                                        'Chart1.Series(seriesName).Color = Color.Brown


                                        ' Pie drawing style 
                                        Me.ddlDaughntdrawing.Enabled = True
                                        Chart1.Series(0)("PieDrawingStyle") = Me.ddlDaughntdrawing.SelectedItem.ToString()

                                        ' Set Doughnut hole size 
                                        Chart1.Series(strcol)("DoughnutRadius") = Me.ddlHoleSizeList.SelectedItem.ToString()

                                        ' Disable Doughnut hole size control for Pie chart 
                                        Me.ddlHoleSizeList.Enabled = True

                                        If Me.chkDaughntshowlegend.Checked Then
                                            Me.Chart1.Legends(0).Enabled = True
                                        Else
                                            Me.Chart1.Legends(0).Enabled = False
                                        End If
                                        'Chart1.Series(seriesName).ShowLabelAsValue = True
                                        'Chart1.Series(seriesName).ShowInLegend = True
                                        'Chart1.Series(seriesName).MarkerStyle = MarkerStyle.Star4
                                        'Chart1.Series(seriesName).MarkerColor = Color.Aqua
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
                If rbnColumn.Checked = True Then
                    Chart2.Visible = False
                    Chart1.Visible = False
                    StockChart.Visible = True
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    If myDataSet.Tables("Query").Columns.Count < 4 Then
                        StockChart.Visible = False
                        GoTo label1
                    End If
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    StockChart.Visible = False
                                    GoTo label1
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
                                StockChart.Series.Add(strcol)
                                StockChart.Series(strcol).Type = SeriesChartType.Stock
                                StockChart.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            StockChart.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        StockChart.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                    Next row1
                                Catch ex As Exception
                                End Try
                            End If
                        Catch ex As Exception
                        End Try
                    Next columncount
                    GoTo label2
                End If
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Line
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
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
                divdaughnt.Style.Add("display", "none")
                divpie.Style.Add("display", "none")
                divScatter.Style.Add("display", "block")
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                ' Set marker size 
                                Chart1.Series(str).MarkerSize = Integer.Parse(ddlMarkersizelist.SelectedItem.Text)

                                ' Set marker shape 
                                If ddlMarkershapelist.SelectedIndex = 1 Then
                                    Chart1.Series(str).MarkerStyle = MarkerStyle.Diamond
                                ElseIf ddlMarkershapelist.SelectedIndex = 2 Then
                                    Chart1.Series(str).MarkerStyle = MarkerStyle.Cross
                                End If

                                ' Set marker shape colour 
                                If ddlMarkershapecolour.SelectedIndex = 1 Then
                                    Chart1.Series(str).MarkerColor = Color.Aquamarine
                                ElseIf ddlMarkershapecolour.SelectedIndex = 2 Then
                                    Chart1.Series(str).MarkerColor = Color.BlueViolet
                                ElseIf ddlMarkershapecolour.SelectedIndex = 3 Then
                                    Chart1.Series(str).MarkerColor = Color.Red
                                ElseIf ddlMarkershapecolour.SelectedIndex = 4 Then
                                    Chart1.Series(str).MarkerColor = Color.Yellow
                                ElseIf ddlMarkershapecolour.SelectedIndex = 5 Then
                                    Chart1.Series(str).MarkerColor = Color.Coral
                                ElseIf ddlMarkershapecolour.SelectedIndex = 6 Then
                                    Chart1.Series(str).MarkerColor = Color.Green
                                End If

                                ' Set X and Y axis scale 
                                Chart1.ChartAreas("Chart Area 1").AxisY.Maximum = 100
                                Chart1.ChartAreas("Chart Area 1").AxisY.Minimum = 0
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            ' Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        ' Enable data points labels 
                                        If ddlPointlabelslist.SelectedItem.Text <> "None" Then
                                            Chart1.Series(strcol).ShowLabelAsValue = True
                                            Chart1.Series(strcol)("LabelStyle") = ddlPointlabelslist.SelectedItem.Text
                                        End If

                                        ' Set marker size 
                                        Chart1.Series(strcol).MarkerSize = Integer.Parse(ddlMarkersizelist.SelectedItem.Text)

                                        ' Set marker shape 
                                        If ddlMarkershapelist.SelectedIndex = 1 Then
                                            Chart1.Series(strcol).MarkerStyle = MarkerStyle.Diamond
                                        ElseIf ddlMarkershapelist.SelectedIndex = 2 Then
                                            Chart1.Series(strcol).MarkerStyle = MarkerStyle.Cross
                                        End If

                                        ' Set marker shape colour 
                                        If ddlMarkershapecolour.SelectedIndex = 1 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Aquamarine
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 2 Then
                                            Chart1.Series(strcol).MarkerColor = Color.BlueViolet
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 3 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Red
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 4 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Yellow
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 5 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Coral
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 6 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Green
                                        End If

                                        ' Set X and Y axis scale 
                                        Chart1.ChartAreas("Chart Area 1").AxisY.Maximum = 100
                                        Chart1.ChartAreas("Chart Area 1").AxisY.Minimum = 0
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
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
                divdaughnt.Style.Add("display", "none")
                divpie.Style.Add("display", "none")
                divScatter.Style.Add("display", "block")
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

                    For Each row In myDataSet.Tables("Query").Rows
                        ' for each Row, add a new series
                        For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                            rowname = myDataSet.Tables("Query").Columns(i).ColumnName
                            Try
                                rval = CInt(row(rowname))
                            Catch ex As Exception
                                If rval <> 0 Then
                                    For t = 0 To arr.Length - 1
                                        arr(t) = ""
                                    Next
                                    GoTo label1
                                End If
                                sval = row(rowname)
                                arr(i) = sval
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
                        Dim YVal As Integer

                        Try
                            For colIndex = 1 To (myDataSet.Tables("Query").Columns.Count) - 1
                                ' for each column (column 1 and onward), add the value as a point
                                columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                Try
                                    YVal = CInt(row(columnName))
                                    Chart1.Series(str).Points.AddXY(columnName, YVal)
                                Catch ex As Exception
                                    bb = row(columnName)
                                End Try
                                Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                ' Set marker size 
                                Chart1.Series(str).MarkerSize = Integer.Parse(ddlMarkersizelist.SelectedItem.Text)

                                ' Set marker shape 
                                If ddlMarkershapelist.SelectedIndex = 1 Then
                                    Chart1.Series(str).MarkerStyle = MarkerStyle.Diamond
                                ElseIf ddlMarkershapelist.SelectedIndex = 2 Then
                                    Chart1.Series(str).MarkerStyle = MarkerStyle.Cross
                                End If

                                ' Set marker shape colour 
                                If ddlMarkershapecolour.SelectedIndex = 1 Then
                                    Chart1.Series(str).MarkerColor = Color.Aquamarine
                                ElseIf ddlMarkershapecolour.SelectedIndex = 2 Then
                                    Chart1.Series(str).MarkerColor = Color.BlueViolet
                                ElseIf ddlMarkershapecolour.SelectedIndex = 3 Then
                                    Chart1.Series(str).MarkerColor = Color.Red
                                ElseIf ddlMarkershapecolour.SelectedIndex = 4 Then
                                    Chart1.Series(str).MarkerColor = Color.Yellow
                                ElseIf ddlMarkershapecolour.SelectedIndex = 5 Then
                                    Chart1.Series(str).MarkerColor = Color.Coral
                                ElseIf ddlMarkershapecolour.SelectedIndex = 6 Then
                                    Chart1.Series(str).MarkerColor = Color.Green
                                End If

                                ' Set X and Y axis scale 
                                Chart1.ChartAreas("Chart Area 1").AxisY.Maximum = 100
                                Chart1.ChartAreas("Chart Area 1").AxisY.Minimum = 0
                            Next colIndex
                            rval = blankravl
                        Catch ex As Exception
                        End Try
                    Next row
                    GoTo label2
                End If
                'selet Column series data
                If rbnColumn.Checked = True Then
                    Dim columncount, colintval As Integer
                    Dim cc, colseries, colstrval As String
                    Dim colarr(10) As String
                    Dim col As DataRow
                    For columncount = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        Dim colname As String = myDataSet.Tables("Query").Columns(columncount).ColumnName
                        cc = colname
                        Try
                            For Each col In myDataSet.Tables("Query").Rows
                                colseries = col(0).ToString()
                                Try
                                    colintval = CInt(col(colname))
                                    colarr(columncount) = cc
                                    Exit For
                                Catch ex As Exception
                                    colstrval = col(colname)
                                    If colintval <> 0 Then
                                        If colstrval <> dupcol Then
                                            For iloopindex = 0 To colarr.Length - 1
                                                colarr(iloopindex) = ""
                                                Chart1.Series.Clear()
                                            Next
                                            GoTo label1
                                        End If
                                    End If
                                    Exit For
                                End Try
                            Next col
                            dupcol = colstrval
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
                                Chart1.Series.Add(strcol)
                                Chart1.Series(strcol).Type = SeriesChartType.Point
                                Chart1.Series(strcol).BorderWidth = 2
                                Dim st As String
                                st = strcol.Replace(count - 1, "")
                                Duplicate = st
                                Dim xval As String
                                Dim row1 As DataRow
                                Dim YVal1 As Integer
                                xval = ""
                                Try
                                    For Each row1 In myDataSet.Tables("Query").Rows
                                        ' for each row (row 1 and onward), add the value as a point
                                        columnseries = row1(0).ToString()
                                        Try
                                            YVal1 = CInt(row1(colname))
                                            Chart1.Series(strcol).Points.AddXY(columnseries, YVal1)
                                        Catch ex As Exception
                                            xval = row1(colname)
                                            ' Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                        End Try
                                        Chart1.Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        ' Enable data points labels 
                                        If ddlPointlabelslist.SelectedItem.Text <> "None" Then
                                            Chart1.Series(strcol).ShowLabelAsValue = True
                                            Chart1.Series(strcol)("LabelStyle") = ddlPointlabelslist.SelectedItem.Text
                                        End If

                                        ' Set marker size 
                                        Chart1.Series(strcol).MarkerSize = Integer.Parse(ddlMarkersizelist.SelectedItem.Text)

                                        ' Set marker shape 
                                        If ddlMarkershapelist.SelectedIndex = 1 Then
                                            Chart1.Series(strcol).MarkerStyle = MarkerStyle.Diamond
                                        ElseIf ddlMarkershapelist.SelectedIndex = 2 Then
                                            Chart1.Series(strcol).MarkerStyle = MarkerStyle.Cross
                                        End If

                                        ' Set marker shape colour 
                                        If ddlMarkershapecolour.SelectedIndex = 1 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Aquamarine
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 2 Then
                                            Chart1.Series(strcol).MarkerColor = Color.BlueViolet
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 3 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Red
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 4 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Yellow
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 5 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Coral
                                        ElseIf ddlMarkershapecolour.SelectedIndex = 6 Then
                                            Chart1.Series(strcol).MarkerColor = Color.Green
                                        End If

                                        ' Set X and Y axis scale 
                                        Chart1.ChartAreas("Chart Area 1").AxisY.Maximum = 100
                                        Chart1.ChartAreas("Chart Area 1").AxisY.Minimum = 0
                                        Chart1.Series(strcol).ShowLabelAsValue = True
                                        Chart1.Series(strcol).ShowInLegend = True
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
            If rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = True Then

            End If
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
    Public Sub Commanchartformat()
        Try
            'If ddlAxisList.SelectedItem.Text <> "--Select--" Then
            '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Position = 10
            '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.Size = 25

            '    ' scrolling and zooming will force keeping of series data between callbacks. 
            '    Me.Chart1.ChartAreas("Chart Area 1").CursorX.UserEnabled = Me.ddlAxisList.SelectedIndex = 1 OrElse Me.ddlAxisList.SelectedIndex = 2

            '    Me.Chart1.ChartAreas("Chart Area 1").CursorY.UserEnabled = Me.ddlAxisList.SelectedIndex = 2 OrElse Me.ddlAxisList.SelectedIndex = 3

            '    ' Set restriction on how far the user can zoom in 
            '    Me.Chart1.ChartAreas("Chart Area 1").AxisX.View.MinSize = 5
            '    Me.Chart1.ChartAreas("Chart Area 1").AxisY.View.MinSize = 50

            '    ' Check AJAXZoomEnabled property. 
            '    If CheckBoxAJAXZoomEnabled.Checked Then
            '        Chart1.AJAXZoomEnabled = True
            '    Else
            '        Chart1.AJAXZoomEnabled = False
            '    End If
            'End If
            UpdateColor()
            UpdateLegendCellColumns()
            SetupChart()
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
        Chart1.ChartAreas("Chart Area 1").AxisX.LabelStyle.Font = New Font(ddlXlabelfont.SelectedItem.Text, Single.Parse(ddlXfontsizelist.SelectedItem.Text))

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
        Chart1.ChartAreas("Chart Area 1").AxisY.LabelStyle.Font = New Font(ddlYfontname.SelectedItem.Text, Single.Parse(ddlYlabelfontsize.SelectedItem.Text))

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




        Dim fontstylecheckbox As New FontStyle
        'Set Italic Font
        If (chkItalic.Checked = True) Then
            fontstylecheckbox = FontStyle.Italic
        End If

        'Set Bold font
        If (chkBold.Checked) Then
            fontstylecheckbox = FontStyle.Bold
        End If

        'Set Strickout font
        If (chkSout.Checked) Then
            fontstylecheckbox = FontStyle.Strikeout
        End If

        'Set underline font
        If (chkUline.Checked) Then
            fontstylecheckbox = FontStyle.Underline
        End If


        'Set Chart Axis title font
        Try
            Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
            Chart1.Titles(0).Font = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Catch ex As Exception
            Chart1.ChartAreas("Chart Area 1").AxisX.TitleFont = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
            Chart1.Titles(0).Font = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        End Try
        Try
            Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font(ddlFont1.SelectedItem.Text, Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        Catch ex As Exception
            Chart1.ChartAreas("Chart Area 1").AxisY.TitleFont = New Font("Arial", Single.Parse(ddlTitlesize.SelectedItem.Text), fontstylecheckbox)
        End Try

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
        ddlPalettes.DataSource = [Enum].GetNames(GetType(ChartColorPalette))
        ddlPalettes.DataBind()

        ddlPalettes.Items.RemoveAt(0)
        ddlPalettes.Items.Remove("LightBeige")
        ddlPalettes.Items.Remove("GreenBlue")
        ddlPalettes.SelectedIndex = 11


        If ddlMajorgridline.SelectedIndex <> 0 Then
            If ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Enabled = True

            ElseIf ddlMajorgridline.SelectedItem.Value = "Grid" And ddlLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Enabled = True

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

            ElseIf ddlMinorType.SelectedItem.Value = "Grid" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Enabled = True

            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Horizontal" Then
                Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.Enabled = True

            ElseIf ddlMinorType.SelectedItem.Value = "Tickmark" And ddlMinoeLinetypes.SelectedItem.Value = "Vertical" Then
                Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Enabled = True
            End If
        End If

        ' Set Grid lines and tick marks interval
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.Interval = Double.Parse(ddlMajorInterval.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.Interval = Double.Parse(ddlMinorInterval.SelectedItem.Value)



        If Majorgridcolour.Value = "" Then
            Majorgridcolour.Value = "#000000"
        End If
        '' Set Line Color
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        'Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(Majorgridcolour.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(ddlMinorColor1.Value)

        '' Set Line Width
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        Chart1.ChartAreas("Chart Area 1").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        Chart1.ChartAreas("Chart Area 1").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
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
        StockChart.ChartAreas("Price").AxisY.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(yaxiscolor.Value)

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
                ' StockChart.ChartAreas("Price").AxisY.MajorGrid.Enabled = True

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
        StockChart.ChartAreas("Price").AxisY.MajorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)

        StockChart.ChartAreas("Price").AxisX.MinorGrid.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorGrid.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisX.MinorTickMark.LineWidth = Single.Parse(ddlMinorWidth.SelectedItem.Value)
        StockChart.ChartAreas("Price").AxisY.MinorTickMark.LineWidth = Single.Parse(ddlMajorline.SelectedItem.Value)
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
    ''' Animation Formate
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SetChartData()
        
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
    Private Sub LoadData()


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
    ''' <summary>
    ''' Get Images From Database
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetImagesFromDatabase()
        Try
            Dim path As String
            ' set the source of the data for the repeater control and bind it 
            Dim i As Integer

            Dim strQuery As String = "select imageaddress from idmsimagetable where queryname='" + ddlOpenreport.SelectedItem.Text + "'"
            Dim da1 As New SqlDataAdapter(strQuery, con)
            con.Open()
            da1.Fill(dsImage)
            Dim strTable As String = ""

            strTable += "<table>"

            For i = 0 To dsImage.Tables(0).Rows.Count - 1

                path = "../graphicalPresentation/AnalysisReportGraph" 'Server.MapPath("AnalysisReportGraph")
                '<img src="../images/Chart/RunChart.jpg" />
                path = path & "\" & dsImage.Tables(0).Rows(i)("imageaddress").ToString
                strTable += "<tr><td><img src='" + path + "'></td></tr>"
            Next
            strTable += "</table>"
            'dgShowImage.DataSource = dsImage
            'dgShowImage.DataBind()
            'divdgShowImage.InnerHtml = ""
            'divdgShowImage.InnerHtml = divdgShowImage.InnerHtml + strTable
            con.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub GetDeleteGraphName()
        Dim deleteStr, readdel As String
        Dim i As Integer
        deleteStr = ddlGraphname.SelectedItem.Text
        '<--------------------Deleting all the excel files---------------------------------->
        cmd = New SqlCommand("select imageaddress from idmsimagetable,idmsgraphmaster where idmsimagetable.graphname=idmsgraphmaster.graphname and idmsimagetable.graphname= '" + deleteStr + "'", con)
        con.Open()

        readquery = cmd.ExecuteReader()
        While readquery.Read()
            readdel = readquery("imageaddress")
            'Dim DelPath As String
            'DelPath = Server.MapPath("AnalysisReportGraph") + "\" + readdel

            'Kill(DelPath)
            'Dim path1 = Server.MapPath(DelPath)
            'Dim dir As New DirectoryInfo(path1)
            'Dim file1() As FileInfo
            'file1 = dir.GetFiles()
            'If file1.Length > 0 Then
            '    For i = 0 To file1.Length - 1s
            '        file1(i).Delete()
            '    Next
            'End If
        End While
        con.Close()
    End Sub
    Public Sub addgpname()
        If rbnOpenreport.Checked = True And rbnOpenanalysis.Checked = False Then
            ddlGraphname.Visible = True
            lblGraphname.Visible = True
            Dim graphobj As New GraphicalPresentation

            ddlGraphname.DataTextField = "graphname"
            ddlGraphname.DataValueField = "Recordid"
            ddlGraphname.DataSource = graphobj.ReportGraphName(ddlOpenreport.SelectedValue)
            ddlGraphname.DataBind()
            ddlGraphname.Items.Insert(0, "---Select---")
            selectedcols.Items.Clear()
            repcols.Items.Clear()

        End If
        If rbnOpenreport.Checked = False And rbnOpenanalysis.Checked = True Then
            ddlGraphname.Visible = True
            lblGraphname.Visible = True
            Dim graphobj As New GraphicalPresentation

            ddlGraphname.DataTextField = "graphname"
            ddlGraphname.DataValueField = "Recordid"
            ddlGraphname.DataSource = graphobj.ReportGraphName(ddlOpenreport.SelectedValue)

            ddlGraphname.DataBind()
            ddlGraphname.Items.Insert(0, "---Select---")
        End If
    End Sub
    Public Sub opengraphname()
        If rbnOpenreport.Checked = True And rbnOpenanalysis.Checked = False Then
            ddlGraphname.Visible = True
            lblGraphname.Visible = True
            Dim graphobj As New GraphicalPresentation

            ddlGraphname.DataTextField = "graphname"
            ddlGraphname.DataValueField = "Recordid"
            ddlGraphname.DataSource = graphobj.ReportGraphName(ddlOpenreport.SelectedValue)
            ddlGraphname.DataBind()
            ddlGraphname.Items.Insert(0, "---Select---")
            selectedcols.Items.Clear()
            repcols.Items.Clear()

        End If
        If rbnOpenreport.Checked = False And rbnOpenanalysis.Checked = True Then

            labelOpenanalysis.Visible = True
            ddlOpenanalysistable.Visible = True
            ddlOpenanalysistable.Items.Clear()
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            Dim no_column As Integer
            Dim tablenamepresent As String
            Dim tablenamepresent1 As String
            Dim classobj As New Functions
            Dim path As String
            If Not Directory.Exists(Server.MapPath("AnalysisReport/excel")) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("AnalysisReport/excel"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            Dim tabname As String = ddlOpenreport.SelectedValue
            path = "../DataAnalysis/html/" & ddlOpenreport.SelectedItem.Text & ".html"
            Dim st As StreamReader
            Dim cce
            Dim counteer As Integer = 0
            st = File.OpenText(Server.MapPath(path))
            Dim dd As String = st.ReadToEnd
            Dim cc
            Dim int As Integer = 0
            Dim k As Integer = 0
            Dim Analysistable
            For k = 0 To dd.Length
                If dd.Contains("</table>") Then
                    int = dd.IndexOf("</table>")
                    cce = dd.Substring(0, int + 8)
                    Dim ccestr As String
                    ccestr = cce
                    Dim hh
                    ccestr = ccestr.Replace("</tr>", "*")
                    ccestr = ccestr.Replace("</td>", "*")
                    ccestr = ccestr.Replace("<b>", "")
                    ccestr = ccestr.Replace("</b>", "")
                    Dim vaslueinsertes As String = ""
                    Dim columnval1 As String = ""
                    Dim tablename As String = ""
                    Dim tablecolname As String
                    hh = ccestr.Split("*")
                    Dim j As Integer = UBound(hh)
                    Dim i As Integer = 0
                    For i = 0 To j
                        Dim nm As Integer = hh(i).LastIndexOf(">")
                        Dim nm1 As Integer = hh(i).length
                        Dim nm2 As Integer = nm1 - nm
                        Dim columnval As String = hh(i)
                        If columnval1 = "" Then
                            columnval1 = columnval.Substring(nm + 1, nm2 - 1)
                        Else
                            columnval1 = columnval1 & "*" & columnval.Substring(nm + 1, nm2 - 1)
                        End If
                    Next
                    columnval1 = columnval1.Replace("**", "$")
                    Dim str3 As String
                    Dim str4 As String
                    Dim doublecomma = columnval1.Split("$")
                    Dim kyarakhu As Integer = UBound(doublecomma)
                    Dim nnn As Integer
                    str3 = "create table "
                    For nnn = 0 To kyarakhu
                        If nnn = 0 Then
                            str3 = str3 & "Analysis" & Trim(doublecomma(nnn)) & counteer & "("
                            tablenamepresent = "Analysis" & Trim(doublecomma(nnn)) & counteer

                            If tablenamepresent1 = "" Then
                                tablenamepresent1 = tablenamepresent
                            Else

                                tablenamepresent1 = tablenamepresent1 & "," & "Analysis" & Trim(doublecomma(nnn)) & counteer
                            End If

                        ElseIf nnn = 1 Then
                            Dim newstringnow As String = doublecomma(nnn)
                            Dim vvval = newstringnow.Split("*")
                            Dim u As Integer = UBound(vvval)
                            no_column = u
                            Dim q As Integer = 0
                            For q = 0 To u
                                If tablecolname = "" Then
                                    tablecolname = vvval(q) & " " & "varchar(1000)"
                                Else
                                    tablecolname = tablecolname & "," & vvval(q) & " " & "varchar(1000)"
                                End If
                            Next
                            str3 = str3 & tablecolname
                            str3 = str3 & ")"

                            Dim vv As Boolean
                            Dim repName As String = ""
                            com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
                            con.Open()
                            data1 = com1.ExecuteReader
                            While data1.Read()
                                If txtCurrentReport.Text = "" Then
                                    repName = tablenamepresent
                                    If data1("name") = tablenamepresent Then
                                        vv = False
                                        Exit While
                                    Else
                                        vv = True
                                    End If
                                Else
                                    'repName = txtCurrentReport.Text
                                    'If Data("name") = "tab" & txtCurrentReport.Text Then
                                    '    b = False
                                    '    Exit While
                                    'Else
                                    '    b = True
                                    'End If
                                End If


                            End While
                            com1.Dispose()
                            con.Close()
                            Dim blank As String
                            If vv = False Then

                                blank = "Drop table " + tablenamepresent + ""
                                com1 = New SqlCommand(blank, con)
                                con.Open()
                                com1.ExecuteNonQuery()
                                con.Close()
                                blank = ""
                                If blank = "" Then
                                    com1 = New SqlCommand(str3, con)
                                    con.Open()
                                    com1.ExecuteNonQuery()
                                    con.Close()
                                End If
                            End If
                            If vv = True Then
                                com1 = New SqlCommand(str3, con)
                                con.Open()
                                com1.ExecuteNonQuery()
                                con.Close()
                            End If
                            tablecolname = ""
                        ElseIf nnn >= 2 Then
                            Dim skip As Integer
                            Dim newstringnow As String = doublecomma(nnn)
                            Dim arrayvalues = newstringnow.Split("*")
                            Dim u As Integer = UBound(arrayvalues)
                            Dim q, b As Integer
                            For q = 0 To u
                                If arrayvalues(q) <> "" Then
                                    skip = UBound(arrayvalues)
                                    Dim meadian As String = Trim(arrayvalues(q))
                                    If meadian.Contains("Median") Or meadian.Contains("Correlation") Or meadian.Contains("Regression") Then
                                        If meadian.Contains("Median") = True Then
                                            If arrayvalues(q) <> "" Then
                                                Dim meadianstr As String = ""
                                                If meadianstr = "" Then
                                                    meadianstr = "'" & arrayvalues(q) & "'"
                                                Else
                                                    meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                End If
                                            End If
                                            q = skip
                                        End If
                                    Else
                                        If newstringnow = " FILTER PERCENTAGE" Then
                                            GoTo gg
                                        End If
                                        If meadian.Contains("FILTER PERCENTAGE") = True Then
                                            GoTo gg
                                        End If
                                        If vaslueinsertes = "" Then
                                            vaslueinsertes = "'" & arrayvalues(q) & "'"
                                        Else
                                            vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                        End If
                                    End If
                                End If
                            Next
                            If vaslueinsertes <> "" Then
                                If tablenamepresent = "" Then
                                    Exit For
                                Else
                                    com1 = New SqlCommand("insert into " & tablenamepresent & " values(" & vaslueinsertes & " )", con)
                                    con.Open()
                                    com1.ExecuteNonQuery()
                                    con.Close()
                                    vaslueinsertes = ""
                                End If

                            End If
                        End If
                    Next
                    Dim now = columnval1.Split("*")
                    Dim newint As Integer = dd.Length
                    dd = dd.Replace(cce, "")
                    If dd.Length = 0 Then
                        If k = 0 Then
                            dd = ""
                        End If
                        If k = 1 Then
                            dd = "1"
                        End If
                        If k = 2 Then
                            dd = "12"
                        End If
                        If k = 3 Then
                            dd = "123"
                        End If
                    End If
                    cc = dd.Split("</table>")
                End If
                counteer = counteer + 1
            Next
gg:
            Analysistable = tablenamepresent1.Split(",")
            Dim jj As Integer = UBound(Analysistable)
            Dim ii As Integer = 0

            For ii = 0 To jj
                ddlOpenanalysistable.Items.Insert(ii, Analysistable(ii))
            Next
            ddlOpenanalysistable.Items.Insert(0, "---Select---")
        End If
    End Sub
#End Region

#Region "Button_Click"
    Protected Sub ShowReport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ShowReport.Click
        Try
            Dim com As SqlCommand
            Dim com1 As SqlCommand
            Dim com2 As SqlCommand
            If txtCurrentReport.Text = "" Then
                If ddlDepartmant.SelectedItem.Text = "---select---" Then
                    aspnet_msgbox("Please Select Department.")
                    Exit Sub
                End If
                If ddlReport.SelectedItem.Text = "---select---" And rbnReport.Checked = True Then
                    aspnet_msgbox("Please Select ReportName.")
                    Exit Sub
                End If
                If ddlReport.SelectedItem.Text = "---select---" And rbnAnalysis.Checked = True Then
                    aspnet_msgbox("Please Select Analysis ReportName.")
                    Exit Sub
                End If
            End If
            If rbnAnalysis.Checked = True Then
                If ddlAnalysistable.SelectedItem.Text = "---select---" Then
                    aspnet_msgbox("Please Select Analysis ReportName.")
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

            If rbnReport.Checked = False And rbnAnalysis.Checked = True Then
                gridbind()
            Else
                gridreportbind()

                gridstring = "select " + hidcolumname.Value + " from " + hidtablename.Value + " " + hidwheretxt.Value + " " + hidgroup.Value + " " + hidhaving.Value + " " + hidorder.Value + " "
                Session("reportquery") = gridstring

            End If
            If ddlDepartmant.SelectedItem.Text <> "---select---" Then
                Dim strClose As String = ""
                strClose = "<Script language='Javascript'>"
                strClose = strClose + "gridshow();"
                strClose = strClose + "</Script>"
                ClientScript.RegisterClientScriptBlock(Page.GetType(), "ShowConfirm", strClose)
            End If
            
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
                        'stt = i
                        
                        'hid.Value = stt
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
                ' i = repcols.SelectedItem.Text


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
        ' p = stt.Split(",")
        'Session("colsvalue") = p
    End Sub

    Protected Sub remove_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles remove.Click
        ' selectedcols.Items.Remove(repcols.SelectedItem)
        If selectedcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        End If
        Dim iloopindexrem, w As Integer
        '        Dim irem As String
        '        Dim remarr
        '        If selectedcols.Items.Count <> 0 Then
        For iloopindexrem = 0 To selectedcols.Items.Count - 1
            'If selectedcols.SelectedIndex < 0 Then
            '    aspnet_msgbox("Please Select a value to delete")
            '    Exit Sub
            'End If
            selectedcols.Items.Remove(selectedcols.SelectedItem)
        Next
        'for 
        'selectedcols.Items.RemoveAt(remarr(i))
        ' End If

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
        'If Not Me.Page.IsPostBack Then
        '    ' Create the 'resize' function as a string 
        '    Dim s As String = "function resize(e) {" + "if (e != null) { event = e; }" + "if( width != getWidth() || height != getHeight()) { " + "width = getWidth(); height = getHeight();" + Chart1.CallbackManager.GetCallbackEventReference("RESIZE", "'+ height + ';' + width + '") + ";}}"

        '    ' Insert the resize() function at startup into the aspx page 
        '    Me.Page.ClientScript.RegisterStartupScript(GetType(Chart), "start", s, True)
        'End If
        makechart()
        If rbnOpenanalysis.Checked = True And rbnOpenreport.Checked = False Then
            Session("reoprtname") = ddlOpenreport.SelectedItem.Text
        ElseIf rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = True Then
            Session("reoprtname") = ddlOpenreport.SelectedItem.Text
        End If
        If rbnAnalysis.Checked = True And rbnReport.Checked = False Then
            Session("reoprtname") = ddlReport.SelectedItem.Text
        ElseIf rbnAnalysis.Checked = False And rbnReport.Checked = True Then
            If txtCurrentReport.Text <> "" Then
                Session("reoprtname") = txtCurrentReport.Text
            ElseIf txtCurrentReport.Text = "" Then
                Session("reoprtname") = ddlReport.SelectedItem.Text
            End If
        End If
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
        ' Dim CreatedOn As String = ""
        'CreatedOn = System.DateTime.Today.ToString()
        'Dim newvalue As String
        'newvalue = DateTime.Now.ToString().GetHashCode().ToString("x")

        'If Not Directory.Exists(Server.MapPath("AnalysisReportGraph")) Then
        '<----------------------Creating Directory for partcular user--------------------------------->
        'ctory.CreateDirectory(Server.MapPath("AnalysisReport"))
        '<----------------------End of Creating Directory for partcular user------------------------>
        'End If
        ' Dim imagepath As String = Server.MapPath("AnalysisReportGraph") + "\" + newvalue + ".Jpeg"

        'If hidChart.Value = "Histogram" Then
        'Chart2.Save(imagepath, ChartImageFormat.Jpeg)
        ' ElseIf hidChart.Value = "stock" Then
        'StockChart.Save(imagepath, ChartImageFormat.Jpeg)
        'Else
        'Chart1.Save(imagepath, ChartImageFormat.Jpeg)
        'End If
        'Dim strimage As String = newvalue + ".Jpeg"
        'Session("Imageadd") = strimage
        btnsave.Disabled = False
    End Sub

    Protected Sub Chart1_Click(ByVal sender As Object, ByVal e As ImageClickEventArgs)
        ' Using the coordinates of the Click event determine which 
        ' chart element was clicked on. 
        Dim hitTestResult As HitTestResult = Me.Chart1.HitTest(e.X, e.Y)
        If hitTestResult IsNot Nothing Then
            ' Update chart title with the name of the last clicked chart element 
            If hitTestResult.ChartElementType = ChartElementType.DataPoint Then
                Me.Chart1.Titles("ClickedElement").Text = "Last Clicked Element: " + hitTestResult.Series.Name + " - Data Point #" + hitTestResult.PointIndex.ToString()
            Else
                Me.Chart1.Titles("ClickedElement").Text = "Last Clicked Element: " + hitTestResult.ChartElementType.ToString()
            End If
        End If

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
        showchart()
    End Sub

    Protected Sub imgsPareto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgPareto.Click
        hidChart.Value = "Pareto"
        rbnRow.Visible = False
        showchart()
    End Sub

    Protected Sub imgStock_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgStock.Click
        hidChart.Value = "Stock"
        rbnRow.Visible = False
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
        showchart()
    End Sub

    Protected Sub LinkButton7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton7.Click
        hidChart.Value = "Pareto"
        rbnRow.Visible = False
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
        lblChartimage.Visible = False
        ddlAnalysistable.Visible = False
        rbnAnalysis.Checked = False
        labelAnalysis.Visible = False
        chkanimated.Checked = False
        chkSunnarized.Checked = False
        txtTodate.Text = ""
        txtFromdate.Text = ""
        btnsave.Disabled = True
        repcols.Items.Clear()
        StockChart.Visible = False
        selectedcols.Items.Clear()
        rbnRow.Checked = True
        rbnRow.Visible = True
        rbnColumn.Checked = False
        ShowReport.Visible = False
        Button1.Visible = True
        Selectreport.Visible = False
        ' gridframe.Visible = False
        ' gdGraphreport.Visible = False
        btnreset.Visible = True
        Opengraph.Visible = True
        ddlGraphname.Visible = False
        lblGraphname.Visible = False
        labelOpenanalysis.Visible = False
        ddlOpenanalysistable.Visible = False
        btnDelete.Visible = True
        btnUpdate.Visible = True
        'Button2.Visible = True
        Dim classobj As New Functions
        ddlDepartment.DataTextField = "departmentname"
        ddlDepartment.DataValueField = "autoid"
        ddlDepartment.DataSource = classobj.bind_Department()
        ddlDepartment.DataBind()
        ddlDepartment.Items.Insert(0, "---select---")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try


            rbnReport.Checked = False
            rbnAnalysis.Checked = False
            If ddlDepartment.SelectedIndex = 0 Then
                aspnet_msgbox("Please select department.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = False Then
                aspnet_msgbox("Please Select Graph Type As Analysis Or Report.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select ReportName.")
                    Exit Sub
                ElseIf ddlAnalysistable.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select AnalysisReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select GraphName.")
                    Exit Sub
                End If
            ElseIf rbnOpenreport.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select ReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select GraphName.")
                    Exit Sub
                End If
            End If
            dept = ddlDepartment.SelectedValue
            If ddlOpenclient.SelectedIndex <> 0 Then
                client = ddlOpenclient.SelectedValue
                If ddlOpenlob.SelectedIndex <> 0 Then
                    lob = ddlOpenlob.SelectedValue
                End If
            End If
            If ddlOpenreport.SelectedIndex = 0 Then
                aspnet_msgbox("Please select a report")
            Else
                Dim objcmd As New SqlCommand()
                Dim reader As SqlDataReader
                ' author = Session("userid")
                Dim sqlString As String = "Select idmsgraphmaster.Graphtype,idmsgraphmaster.Departmentid,idmsgraphmaster.Clientid,idmsgraphmaster.underlob,idmsgraphmaster.Queryname,idmsgraphmaster.columnname,idmsgraphmaster.columnseries,idmsgraphmaster.todate,idmsgraphmaster.fromdate,idmsgraphmaster.commanformat,idmsgraphmaster.specificproperties,idmsgraphmaster.totalcolumn from idmsgraphmaster where  idmsgraphmaster.graphname='" + ddlGraphname.SelectedItem.Text + "' and idmsgraphmaster.departmentid='" & dept & "' and idmsgraphmaster.clientid='" & client & "' and idmsgraphmaster.underlob='" & lob & "'"
                objcmd = New SqlCommand(sqlString, con)
                con.Open()
                reader = objcmd.ExecuteReader()
                While reader.Read
                    hidgraphtype = reader("Graphtype").ToString()
                    hidcolumnname = reader("columnname").ToString()
                    hidcolumnseries = reader("columnseries").ToString()
                    hidcommanformat = reader("commanformat").ToString()
                    hidtodate = reader("todate").ToString()
                    hidfromdate = reader("fromdate").ToString()
                    'hidspecificproperties = reader("specificproperties").ToString()
                    hidtotalcolumn = reader("totalcolumn").ToString()
                End While
                repName = ddlOpenreport.SelectedItem.ToString()
                txtTodate.Text = hidtodate
                txtFromdate.Text = hidfromdate
                Dim opentotalcol
                Dim openselectcol
                Dim opencommanformat

                opentotalcol = hidtotalcolumn.Split(",")
                repcols.DataSource = opentotalcol
                repcols.DataBind()

                opentotalcolumn.Value = hidtotalcolumn

                openselectcol = hidcolumnname.Split(",")
                selectedcols.DataSource = openselectcol
                selectedcols.DataBind()

                ' openselectedcolumn.Value = hidcolumnname
                If hidcolumnseries = "column" Then
                    rbnColumn.Checked = True
                ElseIf hidcolumnseries = "row" Then
                    rbnRow.Checked = True
                End If
                hidChart.Value = hidgraphtype
                hidcommanformat = hidcommanformat.Replace("$", ":")


                opencommanformat = hidcommanformat.Split(":")
                Dim commanindexloop As Integer
                For commanindexloop = 0 To (opencommanformat.Length - 1)

                    If opencommanformat(commanindexloop) = "XAngle" Then
                        ddlXangle.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Perspective" Then
                        ddlPerspective.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "YAngle" Then
                        ddlYangle.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "CheckBoxShow3D" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkShow3D.Checked = True
                        Else
                            chkShow3D.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Palettes" Then
                        ddlPalettes.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Backcolour" Then
                        bkcolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Gradient" Then
                        ddlGradient.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Hatchstyle" Then
                        ddlHatchstyle.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Bordercolor" Then
                        titlebordercolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Bordersize" Then
                        ddlBordersize.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Borderstyle" Then
                        ddlBorderstyle.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "XLabelfont" Then
                        ddlXlabelfont.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Xfontsizelist" Then
                        ddlXfontsizelist.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "XLabelcolour" Then
                        xaxiscolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Xontanglelist" Then
                        ddlXontanglelist.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "OffsetLabels" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkXoffset.Checked = True
                        Else
                            chkXoffset.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "EnableLabels" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkXenable.Checked = True
                        Else
                            chkXenable.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Yfontname" Then
                        ddlYfontname.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Ylabelfontsize" Then
                        ddlYlabelfontsize.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Yfontcolour" Then
                        yaxiscolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Yangle" Then
                        ddlYangle.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "chkoffset" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkYoffset.Checked = True
                        Else
                            chkYoffset.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Yenable" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkYenable.Checked = True
                        Else
                            chkYenable.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "charttitle" Then
                        txtCharttitle.Text = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "XAxisTitle" Then
                        txtTitleext.Text = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "YAxisTitle" Then
                        txtYTitle.Text = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Titlesize" Then
                        ddlTitlesize.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Font" Then
                        ddlFont1.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Color" Then
                        titlefontcolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Bordercolor" Then
                        titlebordercolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Backcolor" Then
                        titlebkcolor.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Alignment" Then
                        Alignment.SelectedIndex = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Italic" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkItalic.Checked = True
                        Else
                            chkItalic.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Bold" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkBold.Checked = True
                        Else
                            chkBold.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Strikeout" Then
                        If opencommanformat(commanindexloop + 1) = "on" Then
                            chkSout.Checked = True
                        Else
                            chkSout.Checked = False
                        End If
                    ElseIf opencommanformat(commanindexloop) = "Majorgridline" Then
                        ddlMajorgridline.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Linetypes" Then
                        ddlLinetypes.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Majorgridcolour" Then
                        Majorgridcolour.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "Majorline" Then
                        ddlMajorline.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "MajorInterval" Then
                        ddlMajorInterval.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "MinorType" Then
                        ddlMinorType.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "MinorColor" Then
                        ddlMinorColor1.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "MinorWidth" Then
                        ddlMinorWidth.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    ElseIf opencommanformat(commanindexloop) = "MinorInterval" Then
                        ddlMinorInterval.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                        'ElseIf opencommanformat(commanindexloop) = "Sortlist" Then
                        '    ddlSortlist.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                        'ElseIf opencommanformat(commanindexloop) = "Sortorderlist" Then
                        '    ddlSortorderlist.SelectedItem.Value = opencommanformat(commanindexloop + 1)
                    End If
                Next
                'dept = "1" ' ddlDepartment.SelectedItem.ToString()
                'client = "0" 'ddlClient.SelectedItem.ToString()
                'lob = "0" 'ddlLob.SelectedItem.ToString()
                ' Dim str = selectRep.trackOpenreport(Session("userid"), System.DateTime.Today.ToString(), ddlReport.SelectedItem.ToString(), dept, client, lob)

            End If
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try
    End Sub


    Protected Sub btnUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        'divdgShowImage.Visible = False
        Try


            If ddlDepartment.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Department.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = False Then
                aspnet_msgbox("Please Select Graph Type As Analysis Or Report.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select ReportName.")
                    Exit Sub
                ElseIf ddlAnalysistable.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select AnalysisReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select GraphName.")
                    Exit Sub
                ElseIf selectedcols.Items.Count = 0 Then
                    aspnet_msgbox("Please Click On ShowReport Button.")
                    Exit Sub
                End If

            ElseIf rbnOpenreport.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select ReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select GraphName.")
                    Exit Sub
                ElseIf selectedcols.Items.Count = 0 Then
                    aspnet_msgbox("Please Click On ShowReport Button.")
                    Exit Sub
                End If
            End If
            Dim Perspective As String = "Perspective:"
            Dim XAngle As String = "XAngle:"
            Dim CheckBoxShow3D1 As String = "CheckBoxShow3D:"
            Dim Yangle As String = "Yangle:"
            Dim Palettes As String = "Palettes:"
            Dim Backcolour As String = "Backcolour:"
            Dim Gradient As String = "Gradient:"
            Dim Hatchstyle As String = "Hatchstyle:"
            Dim Bordercolor As String = "Bordercolor:"
            Dim Bordersize As String = "Bordersize:"
            Dim Borderstyle As String = "Borderstyle:"
            Dim Xlabelfont As String = "Xlabelfont:"
            Dim Xfontsizelist As String = "Xfontsizelist:"
            Dim XLabelcolour As String = "XLabelcolour:"
            Dim Xontanglelist As String = "Xontanglelist:"
            Dim OffsetLabels As String = "OffsetLabels:"
            Dim EnableLabels As String = "EnableLabels:"
            Dim Yfontname As String = "Yfontname:"
            Dim Ylabelfontsize As String = "Ylabelfontsize:"
            Dim Yfontcolour As String = "Yfontcolour:"
            Dim Yangle1 As String = "Yangle:"
            Dim chkYoffset1 As String = "chkYoffset:"
            Dim Yenable As String = "Yenable:"
            Dim Charttitle As String = "Charttitle:"
            Dim XAxisTitle As String = "XAxisTitle:"
            Dim Yaxistitle As String = "Yaxistitle:"
            Dim Titlesize As String = "Titlesize:"
            Dim Font As String = "Font:"
            Dim Color As String = "Color:"
            Dim BorderColor1 As String = "BorderColor:"
            Dim BackColor As String = "BackColor:"
            Dim Alignment1 As String = "Alignment:"
            Dim Italic As String = "Italic:"
            Dim Bold As String = "Bold:"
            Dim Underline As String = "Underline:"
            Dim Strikeout As String = "Strikeout:"
            Dim Majorgridline As String = "Majorgridline:"
            Dim Linetypes As String = "Linetypes:"
            Dim Majorgridcolour1 As String = "Majorgridcolour:"
            Dim Majorline As String = "Majorline:"
            Dim MajorInterval As String = "MajorInterval:"
            Dim MinorType As String = "MinorType:"
            Dim MinorColor As String = "MinorColor:"
            Dim MinorWidth As String = "MinorWidth:"
            Dim MinorInterval As String = "MinorInterval:"
            Dim Sortlist As String = "Sortlist:"
            Dim Sortorderlist As String = "Sortorderlist:"
            Dim Linetype As String = "Linetype:"
            Dim ck As String = CheckBoxShow3D1 + chkShow3D.Checked.ToString
            Dim stroffset As String = OffsetLabels + chkXoffset.Checked.ToString
            Dim strEnable As String = EnableLabels + chkXenable.Checked.ToString
            Dim strchkYoff As String = chkYoffset1 + chkYoffset.Checked.ToString
            Dim strYenable As String = Yenable + chkYenable.Checked.ToString
            Dim strItalic As String = Italic + chkItalic.Checked.ToString
            Dim strBold As String = Bold + chkBold.Checked.ToString
            Dim strUnderline As String = Underline + chkUline.Checked.ToString
            Dim strStrikeout As String = Strikeout + chkSout.Checked.ToString
            commangraphfprmat.Value = XAngle + ddlXangle.SelectedItem.Text + "$" + Perspective + ddlPerspective.SelectedItem.Text + "$" + ck + "$" + Yangle + ddlYang.SelectedItem.Text + "$" + Palettes + ddlPalettes.SelectedItem.Text + "$" + Backcolour + bkcolor.Value + "$" + Gradient + ddlGradient.SelectedItem.Text + "$" + Hatchstyle + ddlHatchstyle.SelectedItem.Text + "$" + Bordercolor + titlebordercolor.Value + "$" + Bordersize + ddlBordersize.SelectedItem.Value + "$" + Borderstyle + ddlBorderstyle.SelectedItem.Text + "$" + Xlabelfont + ddlXlabelfont.SelectedItem.Text + "$" + Xfontsizelist + ddlXfontsizelist.SelectedItem.Text + "$" + XLabelcolour + xaxiscolor.Value + "$" + Xontanglelist + ddlXontanglelist.SelectedItem.Text + "$" + stroffset + "$" + strEnable + "$" + Yfontname + ddlYfontname.SelectedItem.Text + "$" + Ylabelfontsize + ddlYlabelfontsize.SelectedItem.Text + "$" + Yfontcolour + yaxiscolor.Value + "$" + Yangle + ddlYangle.SelectedItem.Value + "$" + strchkYoff + "$" + strYenable + "$" + Charttitle + txtCharttitle.Text + "$" + XAxisTitle + txtTitleext.Text + "$" + Yaxistitle + txtYTitle.Text + "$" + Titlesize + ddlTitlesize.SelectedItem.Value + "$" + Font + ddlFont1.SelectedItem.Text + "$" + Color + titlefontcolor.Value + "$" + BorderColor1 + titlebordercolor.Value + "$" + BackColor + titlebkcolor.Value + "$" + Alignment1 + Alignment.SelectedItem.Text + "$" + strItalic + "$" + strBold + "$" + strUnderline + "$" + strStrikeout + "$" + Majorgridline + ddlMajorgridline.SelectedItem.Value + "$" + Linetypes + ddlLinetypes.SelectedItem.Value + "$" + Majorgridcolour1 + Majorgridcolour.Value + "$" + Majorline + ddlMajorline.SelectedItem.Value + "$" + MajorInterval + ddlMajorInterval.SelectedItem.Value + "$" + MinorType + ddlMinorType.SelectedItem.Text + "$" + MinorColor + ddlMinorColor1.Value + "$" + MinorWidth + ddlMinorWidth.SelectedItem.Value + "$" + MinorInterval + ddlMinorInterval.SelectedItem.Value

            specificformat.Value = Linetype + ddlLabelstylelist.SelectedItem.Text
            cmd = New SqlCommand("update idmsgraphmaster set graphtype='" + hidChart.Value + "',columnname='" + openselectedcolumn.Value + "',columnseries='" + seriesbtn.Value + "',todate='" + txtTodate.Text + "',fromdate='" + txtFromdate.Text + "',commanformat='" + commangraphfprmat.Value + "',specificproperties='" + specificformat.Value + "', totalcolumn='" + opentotalcolumn.Value + "' where graphname='" + ddlGraphname.SelectedItem.Text + "'", con)

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            aspnet_msgbox("Graph Update Sucessfully")
            selectedcols.Items.Clear()
            repcols.Items.Clear()

        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try

    End Sub

    Protected Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' divdgShowImage.Visible = False
        Try
            If ddlDepartment.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Department.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = False And rbnOpenreport.Checked = False Then
                aspnet_msgbox("Please Select Graph Type As Analysis Or Report.")
                Exit Sub
            ElseIf rbnOpenanalysis.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select ReportName.")
                    Exit Sub
                ElseIf ddlAnalysistable.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select AnalysisReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please select GraphName.")
                    Exit Sub
                End If
            ElseIf rbnOpenreport.Checked = True Then
                If ddlOpenreport.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select ReportName.")
                    Exit Sub
                ElseIf ddlGraphname.SelectedIndex = 0 Then
                    aspnet_msgbox("Please Select GraphName.")
                    Exit Sub
                End If
            End If
            GetDeleteGraphName()
            cmd = New SqlCommand("Delete from idmsgraphmaster where graphname='" + ddlGraphname.SelectedItem.Text + "'", con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            cmd = New SqlCommand("Delete from idmsimagetable where graphname='" + ddlGraphname.SelectedItem.Text + "'", con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Dim clientAna, lobana As String
            clientAna = ddlOpenclient.SelectedItem.Text
            If clientAna = "" Or clientAna = "---select---" Then
                clientAna = "0"
            Else
                clientAna = ddlOpenclient.SelectedItem.Text
            End If
            lobana = ddlOpenlob.SelectedValue
            If lobana = "" Or lobana = "---select---" Then
                lobana = "0"
            Else
                lobana = ddlOpenlob.SelectedValue
            End If
            Dim strid As String = Session("Userid").ToString
            graphobj.LogDeletegraph(strid, ddlDepartment.Text, clientAna, lobana, ddlGraphname.SelectedItem.Text, ddlOpenreport.Text)

            aspnet_msgbox("Graph Delete Sucessfully ")

            selectedcols.Items.Clear()
            repcols.Items.Clear()

            ' Dim DelPath As String
            'DelPath = "AnalysisReportGraph/" & readdel
            addgpname()


            '<-----------------------End Code-------------------------------------------------------->
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            aspnet_msgbox(strmessage)
            Exit Sub
        End Try
    End Sub

    Protected Sub btnreset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnreset.Click
        chkanimated.Checked = False
        chkSunnarized.Checked = False
        Selectreport.Visible = True
        Opengraph.Visible = False
        ddlAnalysistable.Visible = False
        labelAnalysis.Visible = False
        StockChart.Visible = False
        'divdgShowImage.Visible = False
        'divdgShowImage.InnerHtml = ""
        btnUpdate.Visible = False
        btnDelete.Visible = False
        'Button2.Visible = False
        Button1.Visible = False
        ShowReport.Visible = True
        txtTodate.Text = ""
        txtFromdate.Text = ""
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        rbnRow.Checked = True
        rbnColumn.Checked = False
        btnreset.Visible = False
        ddlOpenreport.Items.Clear()
        rbnOpenanalysis.Checked = False
        ddlReport.Items.Clear()
        rbnOpenreport.Checked = False
        rbnReport.Checked = False
        ddlOpenclient.Items.Clear()
        rbnRow.Visible = True
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
            ddlAnalysistable.Items.Clear()
            ddlReport.Items.Clear()
            ddlClient.Items.Clear()
        End If

        txtTodate.Text = ""
        txtFromdate.Text = ""
        lbldatemsg.Visible = False
        selectedcols.Items.Clear()
        repcols.Items.Clear()
        ddlAnalysistable.Items.Clear()
        ' gdGraphreport.Visible = False
        Try
            If ddlDepartmant.SelectedIndex <> 0 Then


                If Session("typeofuser") = "Admin" Then
                    ddlUser.Enabled = True
                    ddlClient.DataTextField = "clientname"
                    ddlClient.DataValueField = "autoid"
                    ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)

                    'ddlClient.DataSource = classobj.bindAdminClients(Session("userid1"), ddlDepartmant.SelectedValue)
                    ddlClient.DataBind()
                    ddlClient.Items.Insert(0, "---select---")
                    ddlUser.DataTextField = "username"
                    ddlUser.DataValueField = "userid"
                    ddlUser.DataSource = classobj.userselectadminspan(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
                    ddlUser.DataBind()
                    ddlUser.Items.Insert(0, "---select---")

                    rbnReport.Checked = True
                    If txtCurrentReport.Text = "" Then
                        ddlReport.DataTextField = "QueryName"
                        ddlReport.DataValueField = "Recordid"
                        ddlReport.DataSource = repobj.reportForadmin(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
                        ddlReport.DataBind()
                        ddlReport.Items.Insert(0, "---select---")
                        ddlLob.Items.Clear()
                    End If

                End If
                If Session("typeofuser") = "User" Then
                    ddlClient.DataTextField = "clientname"
                    ddlClient.DataValueField = "autoid"
                    ddlClient.DataSource = classobj.bind_client(ddlDepartmant.SelectedValue)
                    'ddlClient.DataSource = classobj.bindUserClients(Session("userid1"), ddlDepartmant.SelectedValue)
                    ddlClient.DataBind()
                    ddlClient.Items.Insert(0, "---select---")
                    'ddlUser.DataTextField = "username"
                    'ddlUser.DataValueField = "userid"
                    'ddlUser.DataSource = classobj.bind_userlocalDept(ddlDepartmant.SelectedValue)
                    'ddlUser.DataBind()
                    'ddlUser.Items.Insert(0, "---select---")
                    ddlUser.Enabled = False
                    rbnReport.Checked = True
                    ddlReport.DataTextField = "QueryName"
                    ddlReport.DataValueField = "Recordid"
                    Dim SCOPE As String = repobj.chkUserscope(Session("userid"))
                    If SCOPE = "Local" Then
                        ddlReport.DataSource = repobj.reportForlocal(Session("userid"), ddlDepartmant.SelectedValue, "0", "0")
                    Else
                        ddlReport.DataSource = repobj.reportFornonlocal(Session("userid"))
                    End If
                    'ddlReport.DataSource = classobj.bind_departmentrepuser(ddlDepartmant.SelectedValue)
                    ddlReport.DataBind()
                    ddlReport.Items.Insert(0, "---select---")
                    ddlLob.Items.Clear()
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
        If ddlClient.SelectedItem.Text = "---select---" Then
            ddlLob.Items.Clear()
            ddlLob.Items.Insert(0, "---select---")
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")
        Else

            Dim classobj As New Functions
            ddlLob.DataTextField = "LOBName"
            ddlLob.DataValueField = "autoid"
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            ddlLob.DataSource = classobj.bind_lob(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
            ddlLob.DataBind()
            ddlLob.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "recordid"
            ddlReport.DataSource = classobj.bind_clientrep(ddlDepartmant.SelectedValue, ddlClient.SelectedValue)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")

        End If
    End Sub

    Protected Sub ddlLob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLob.SelectedIndexChanged
        If ddlLob.SelectedItem.Text = "---select---" Then
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")
        Else
            Dim classobj As New Functions
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "recordid"
            ddlReport.DataSource = classobj.bind_lobrep(ddlDepartmant.SelectedValue, ddlClient.SelectedValue, ddlLob.SelectedValue)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")
        End If
    End Sub

    Protected Sub ddlReport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlReport.SelectedIndexChanged
        If ddlReport.SelectedIndex = 0 Then
            ddlAnalysistable.Items.Clear()
            labelAnalysis.Visible = True
            ddlAnalysistable.Visible = True
            txtCurrentReport.Enabled = False
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            'gdGraphreport.Visible = False
            txtTodate.Text = ""
            txtFromdate.Text = ""
        End If
        If ddlReport.SelectedIndex <> 0 Then
            selectedcols.Items.Clear()
            repcols.Items.Clear()
            ' gdGraphreport.Visible = False
            Try

                If rbnReport.Checked = False And rbnAnalysis.Checked = True Then
                    ddlAnalysistable.Items.Clear()
                    selectedcols.Items.Clear()
                    repcols.Items.Clear()
                    labelAnalysis.Visible = True
                    ddlAnalysistable.Visible = True
                    txtCurrentReport.Enabled = False
                    selectedcols.Items.Clear()
                    repcols.Items.Clear()
                    ' gdGraphreport.Visible = False
                    txtTodate.Text = ""
                    txtFromdate.Text = ""
                    Dim no_column As Integer
                    Dim tablenamepresent As String
                    Dim tablenamepresent1 As String
                    Dim classobj As New Functions
                    Dim path As String
                    If Not Directory.Exists(Server.MapPath("AnalysisReport/excel")) Then
                        '<----------------------Creating Directory for partcular user--------------------------------->
                        Directory.CreateDirectory(Server.MapPath("AnalysisReport/excel"))
                        '<----------------------End of Creating Directory for partcular user------------------------>
                    End If
                    Dim tabname As String = ddlReport.SelectedValue
                    path = "../DataAnalysis/html/" & ddlReport.SelectedItem.Text & ".html"
                    Dim st As StreamReader
                    Dim cce
                    Dim counteer As Integer = 0
                    st = File.OpenText(Server.MapPath(path))
                    Dim dd As String = st.ReadToEnd
                    Dim cc
                    Dim int As Integer = 0
                    Dim k As Integer = 0
                    Dim Analysistable
                    For k = 0 To dd.Length
                        If dd.Contains("</table>") Then
                            int = dd.IndexOf("</table>")
                            cce = dd.Substring(0, int + 8)
                            Dim ccestr As String
                            ccestr = cce
                            Dim hh
                            ccestr = ccestr.Replace("</tr>", "*")
                            ccestr = ccestr.Replace("</td>", "*")
                            ccestr = ccestr.Replace("<b>", "")
                            ccestr = ccestr.Replace("</b>", "")
                            Dim vaslueinsertes As String = ""
                            Dim columnval1 As String = ""
                            Dim tablename As String = ""
                            Dim tablecolname As String
                            hh = ccestr.Split("*")
                            Dim j As Integer = UBound(hh)
                            Dim i As Integer = 0
                            For i = 0 To j
                                Dim nm As Integer = hh(i).LastIndexOf(">")
                                Dim nm1 As Integer = hh(i).length
                                Dim nm2 As Integer = nm1 - nm
                                Dim columnval As String = hh(i)
                                If columnval1 = "" Then
                                    columnval1 = columnval.Substring(nm + 1, nm2 - 1)
                                Else
                                    columnval1 = columnval1 & "*" & columnval.Substring(nm + 1, nm2 - 1)
                                End If
                            Next
                            columnval1 = columnval1.Replace("**", "$")
                            Dim str3 As String
                            Dim str4 As String
                            Dim doublecomma = columnval1.Split("$")
                            Dim kyarakhu As Integer = UBound(doublecomma)
                            Dim nnn As Integer
                            str3 = "create table "
                            For nnn = 0 To kyarakhu
                                If nnn = 0 Then
                                    str3 = str3 & "Analysis" & Trim(doublecomma(nnn)) & counteer & "("
                                    tablenamepresent = "Analysis" & Trim(doublecomma(nnn)) & counteer

                                    If tablenamepresent1 = "" Then
                                        tablenamepresent1 = tablenamepresent
                                    Else

                                        tablenamepresent1 = tablenamepresent1 & "," & "Analysis" & Trim(doublecomma(nnn)) & counteer
                                    End If

                                ElseIf nnn = 1 Then
                                    Dim newstringnow As String = doublecomma(nnn)
                                    Dim vvval = newstringnow.Split("*")
                                    Dim u As Integer = UBound(vvval)
                                    no_column = u
                                    Dim q As Integer = 0
                                    For q = 0 To u
                                        If tablecolname = "" Then
                                            tablecolname = vvval(q) & " " & "varchar(1000)"
                                        Else
                                            tablecolname = tablecolname & "," & vvval(q) & " " & "varchar(1000)"
                                        End If
                                    Next
                                    str3 = str3 & tablecolname
                                    str3 = str3 & ")"

                                    Dim vv As Boolean
                                    Dim repName As String = ""
                                    com1 = New SqlCommand("select name from sysobjects where xtype='u'", con)
                                    con.Open()
                                    data1 = com1.ExecuteReader
                                    While data1.Read()
                                        If txtCurrentReport.Text = "" Then
                                            repName = tablenamepresent
                                            If data1("name") = tablenamepresent Then
                                                vv = False
                                                Exit While
                                            Else
                                                vv = True
                                            End If
                                        Else
                                            'repName = txtCurrentReport.Text
                                            'If Data("name") = "tab" & txtCurrentReport.Text Then
                                            '    b = False
                                            '    Exit While
                                            'Else
                                            '    b = True
                                            'End If
                                        End If


                                    End While
                                    com1.Dispose()
                                    con.Close()
                                    Dim blank As String
                                    If vv = False Then

                                        blank = "Drop table " + tablenamepresent + ""
                                        com1 = New SqlCommand(blank, con)
                                        con.Open()
                                        com1.ExecuteNonQuery()
                                        con.Close()
                                        blank = ""
                                        If blank = "" Then
                                            com1 = New SqlCommand(str3, con)
                                            con.Open()
                                            com1.ExecuteNonQuery()
                                            con.Close()
                                        End If

                                        ' tablenamepresent = ""
                                        'ElseIf vv = True Then
                                        '    com1 = New SqlCommand(str3, con)
                                        '    con.Open()
                                        '    com1.ExecuteNonQuery()
                                        '    con.Close()
                                        '    str3 = ""

                                    End If

                                    If vv = True Then
                                        com1 = New SqlCommand(str3, con)
                                        con.Open()
                                        com1.ExecuteNonQuery()
                                        con.Close()
                                    End If




                                    'com1 = New SqlCommand(str3, con)
                                    'con.Open()
                                    'com1.ExecuteNonQuery()
                                    'con.Close()
                                    tablecolname = ""
                                ElseIf nnn >= 2 Then
                                    Dim skip As Integer
                                    Dim newstringnow As String = doublecomma(nnn)
                                    Dim arrayvalues = newstringnow.Split("*")
                                    Dim u As Integer = UBound(arrayvalues)
                                    Dim q, b As Integer
                                    For q = 0 To u
                                        If arrayvalues(q) <> "" Then
                                            skip = UBound(arrayvalues)
                                            Dim meadian As String = Trim(arrayvalues(q))
                                            If meadian.Contains("Median") Or meadian.Contains("Correlation") Or meadian.Contains("Regression") Then
                                                If meadian.Contains("Median") = True Then
                                                    If arrayvalues(q) <> "" Then
                                                        Dim meadianstr As String = ""
                                                        If meadianstr = "" Then
                                                            meadianstr = "'" & arrayvalues(q) & "'"
                                                        Else
                                                            meadianstr = meadianstr & "," & "'" & arrayvalues(q) & "'"
                                                        End If
                                                    End If
                                                    q = skip
                                                End If
                                            Else
                                                If newstringnow = " FILTER PERCENTAGE" Then
                                                    GoTo gg
                                                End If
                                                If meadian.Contains("FILTER PERCENTAGE") = True Then
                                                    GoTo gg
                                                End If
                                                If vaslueinsertes = "" Then
                                                    vaslueinsertes = "'" & arrayvalues(q) & "'"
                                                Else
                                                    vaslueinsertes = vaslueinsertes & "," & "'" & arrayvalues(q) & "'"
                                                End If
                                            End If
                                        End If
                                    Next
                                    If vaslueinsertes <> "" Then
                                        If tablenamepresent = "" Then
                                            Exit For
                                        Else
                                            com1 = New SqlCommand("insert into " & tablenamepresent & " values(" & vaslueinsertes & " )", con)
                                            con.Open()
                                            com1.ExecuteNonQuery()
                                            con.Close()
                                            vaslueinsertes = ""
                                        End If

                                    End If
                                End If
                            Next
                            Dim now = columnval1.Split("*")
                            'For jj = 0 To now.length - 1
                            Dim newint As Integer = dd.Length
                            dd = dd.Replace(cce, "")
                            If dd.Length = 0 Then
                                If k = 0 Then
                                    dd = ""
                                End If
                                If k = 1 Then
                                    dd = "1"
                                End If
                                If k = 2 Then
                                    dd = "12"
                                End If
                                If k = 3 Then
                                    dd = "123"
                                End If
                            End If
                            cc = dd.Split("</table>")
                        End If
                        counteer = counteer + 1
                    Next
gg:
                    Analysistable = tablenamepresent1.Split(",")
                    Dim jj As Integer = UBound(Analysistable)
                    Dim ii As Integer = 0
                    'Dim uu As Integer

                    For ii = 0 To jj
                        ddlAnalysistable.Items.Insert(ii, Analysistable(ii)) '.DataSource = Analysistable(ii)
                    Next
                    ddlAnalysistable.Items.Insert(0, "---select---")
                End If
            Catch ex As Exception
                Dim strmessage As String = ""
                strmessage = Replace(ex.Message.ToString, "'", "")
                strmessage = Replace(strmessage, vbCrLf, " ")
                aspnet_msgbox(strmessage)
                Exit Sub
            End Try
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

    Protected Sub ddlAnalysistable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAnalysistable.SelectedIndexChanged
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        'gdGraphreport.Visible = False
        'divgridReport.Style.Add("display", "none")
    End Sub

    Protected Sub ddlOpenreport_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOpenreport.SelectedIndexChanged
        opengraphname()
    End Sub

    Protected Sub ddlOpenanalysistable_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOpenanalysistable.SelectedIndexChanged
        If rbnOpenreport.Checked = False And rbnOpenanalysis.Checked = True Then
            ddlGraphname.Visible = True
            lblGraphname.Visible = True
            Dim graphobj As New GraphicalPresentation

            ddlGraphname.DataTextField = "graphname"
            ddlGraphname.DataValueField = "Recordid"
            ddlGraphname.DataSource = graphobj.ReportGraphName(ddlOpenreport.SelectedValue)

            ddlGraphname.DataBind()
            ddlGraphname.Items.Insert(0, "---Select---")
        End If
    End Sub

    Protected Sub ddlDepartment_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlDepartment.SelectedIndexChanged
        'rbnOpenreport.Checked = True
        If ddlDepartment.SelectedItem.Text = "---select---" Then
            ddlOpenclient.Items.Clear()
            ddlOpenclient.Items.Insert(0, "---select---")
            ddlReport.Items.Clear()
            ddlReport.Items.Insert(0, "---select---")

        Else

            Dim classobj As New Functions
            ddlOpenclient.DataTextField = "clientname"
            ddlOpenclient.DataValueField = "autoid"
            ddlOpenclient.DataSource = classobj.bind_client(ddlDepartment.SelectedValue)
            ddlOpenclient.DataBind()
            ddlOpenclient.Items.Insert("0", "---select---")
            rbnOpenanalysis.Checked = False
            rbnOpenreport.Checked = False
            ddlOpenreport.Items.Clear()
            ddlOpenanalysistable.Items.Clear()
            ddlGraphname.Items.Clear()
            txtTodate.Text = ""
            txtFromdate.Text = ""
            selectedcols.Items.Clear()
            repcols.Items.Clear()

            If rbnReport.Checked = True Then
                Dim clientana, lobana As String
                clientana = ddlOpenclient.SelectedItem.Text
                If clientana = "" Or clientana = "---select--" Then
                    clientana = "0"
                Else
                    clientana = ddlOpenclient.SelectedItem.Text
                End If
                lobana = ddlOpenclient.SelectedItem.Text
                If lobana = "" Or lobana = "---select--" Then
                    lobana = "0"
                Else
                    lobana = ddlOpenclient.SelectedItem.Text
                End If
                ddlOpenreport.Items.Insert(0, "---select---")
                ddlOpenreport.DataTextField = "queryname"
                ddlOpenreport.DataValueField = "queryname"
                ddlOpenreport.DataSource = classobj.bind_openrep(ddlDepartment.SelectedValue, clientana, lobana)
                ddlOpenreport.DataBind()
                ddlOpenreport.Items.Insert(0, "---select---")

            End If
        End If
    End Sub

    Protected Sub ddlOpenclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOpenclient.SelectedIndexChanged
        If ddlOpenclient.SelectedItem.Text = "---select---" Then
            ddlOpenlob.Items.Clear()
            ddlOpenlob.Items.Insert(0, "---select---")
            ddlOpenreport.Items.Clear()
            ddlOpenreport.Items.Insert(0, "---select---")
        Else

            Dim classobj As New Functions
            ddlOpenlob.DataTextField = "lob"
            ddlOpenlob.DataValueField = "autoid"
            ddlOpenlob.DataSource = classobj.bind_lob(ddlDepartment.SelectedValue, ddlOpenclient.SelectedValue)
            ddlOpenlob.DataBind()
            ddlOpenlob.Items.Insert(0, "---select---")
            ddlOpenreport.DataTextField = "queryname"
            ddlOpenreport.DataValueField = "recordid"
            ddlOpenreport.DataSource = classobj.bind_clientrep(ddlDepartment.SelectedValue, ddlOpenclient.SelectedValue)
            ddlOpenreport.DataBind()
            ddlOpenreport.Items.Insert(0, "---select---")

        End If
    End Sub

    Protected Sub ddlOpenlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlOpenlob.SelectedIndexChanged
        If ddlOpenlob.SelectedItem.Text = "---select---" Then
            ddlOpenreport.Items.Clear()
            ddlOpenreport.Items.Insert(0, "---select---")
        Else
            Dim classobj As New Functions
            ddlOpenreport.DataTextField = "queryname"
            ddlOpenreport.DataValueField = "recordid"
            ddlOpenreport.DataSource = classobj.bind_lobrep(ddlDepartment.SelectedValue, ddlOpenclient.SelectedValue, ddlOpenlob.SelectedValue)
            ddlOpenreport.DataBind()
            ddlOpenreport.Items.Insert(0, "---select---")
        End If
    End Sub

#End Region

#Region "Check_Changed"
    Protected Sub rbnRow_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnRow.CheckedChanged
        seriesbtn.Value = "Rows"
    End Sub

    Protected Sub rbnColumn_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnColumn.CheckedChanged
        seriesbtn.Value = "Column"
    End Sub

    Protected Sub rbnReport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnReport.CheckedChanged
        If rbnReport.Checked = True And rbnAnalysis.Checked = False Then
            repcols.Items.Clear()
            selectedcols.Items.Clear()
            ddlAnalysistable.Items.Clear()
            labelOpenanalysis.Visible = False
            labelAnalysis.Visible = False
            ddlAnalysistable.Visible = False
            ' gdGraphreport.Visible = False
            'divgridReport.Style.Add("display", "none")
            ddlOpenanalysistable.Visible = False
            Dim classobj As New Functions
            ddlReport.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "queryname"
            ddlReport.DataValueField = "queryname"
            ddlReport.DataSource = classobj.bind_departmentrep(ddlDepartmant.SelectedValue)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")
        End If
    End Sub

    Protected Sub rbnAnalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnAnalysis.CheckedChanged
        If rbnReport.Checked = False And rbnAnalysis.Checked = True Then
            'gdGraphreport.Visible = False
            'divgridReport.Style.Add("display", "none")
            ' trAnalysis.Visible = True

            repcols.Items.Clear()
            selectedcols.Items.Clear()
            Dim classobj As New Functions
            ddlReport.Items.Insert(0, "---select---")
            ddlReport.DataTextField = "Reportname"
            ddlReport.DataValueField = "Reportname"
            ddlReport.DataSource = classobj.bind_departmentAnalysisrep(ddlDepartmant.SelectedValue) ', client, lob)
            ddlReport.DataBind()
            ddlReport.Items.Insert(0, "---select---")
        End If
    End Sub

    Protected Sub rbnOpenanalysis_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnOpenanalysis.CheckedChanged
        If ddlDepartment.SelectedIndex = 0 Then
            aspnet_msgbox("Please select department.")
            rbnOpenanalysis.Checked = False
            Exit Sub
        ElseIf rbnOpenanalysis.Checked = True And rbnOpenreport.Checked = False Then
            ddlOpenanalysistable.Visible = False
            labelOpenanalysis.Visible = False
            repcols.Items.Clear()
            selectedcols.Items.Clear()
            ddlGraphname.Visible = False
            lblGraphname.Visible = False
            'divdgShowImage.Visible = False
            Dim clientAna, lobana As String
            clientAna = ddlOpenclient.SelectedItem.Text
            If clientAna = "" Or clientAna = "---select---" Then
                clientAna = "0"
            Else
                clientAna = ddlOpenclient.SelectedItem.Text
            End If
            lobana = ddlOpenclient.SelectedItem.Text
            If lobana = "" Or lobana = "---select---" Then
                lobana = "0"
            Else
                lobana = ddlOpenclient.SelectedItem.Text
            End If
            Dim classobj As New Functions
            ddlOpenreport.Items.Insert(0, "---select---")
            ddlOpenreport.DataTextField = "queryname"
            ddlOpenreport.DataValueField = "queryname"
            ddlOpenreport.DataSource = classobj.bind_openAnarep(ddlDepartment.SelectedValue, clientAna, lobana)
            ddlOpenreport.DataBind()
            ddlOpenreport.Items.Insert(0, "---select---")

        End If

    End Sub

    Protected Sub rbnOpenreport_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbnOpenreport.CheckedChanged

        If ddlDepartment.SelectedIndex = 0 Then
            aspnet_msgbox("Please select department.")
            rbnOpenreport.Checked = False
            Exit Sub
        ElseIf rbnOpenreport.Checked = True And rbnOpenanalysis.Checked = False Then
            ddlGraphname.Visible = False
            lblGraphname.Visible = False
            ddlOpenanalysistable.Visible = False
            labelOpenanalysis.Visible = False
            ' divdgShowImage.Visible = False
            Dim clientAna, lobana As String
            clientAna = ddlOpenclient.SelectedItem.Text
            If clientAna = "" Or clientAna = "---select---" Then
                clientAna = "0"
            Else
                clientAna = ddlOpenclient.SelectedItem.Text
            End If
            lobana = ddlOpenlob.SelectedValue
            If lobana = "" Or lobana = "---select---" Then
                lobana = "0"
            Else
                lobana = ddlOpenlob.SelectedValue
            End If
            Dim classobj As New Functions
            ddlOpenreport.Items.Insert(0, "---select---")
            ddlOpenreport.DataTextField = "queryname"
            ddlOpenreport.DataValueField = "queryname"
            ddlOpenreport.DataSource = classobj.bind_openrep(ddlDepartment.SelectedValue, clientAna, lobana)
            ddlOpenreport.DataBind()
            ddlOpenreport.Items.Insert(0, "---select---")

        End If
    End Sub

    Protected Sub CheckBoxPoints_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxPoints.CheckedChanged
        SetChartData()
    End Sub

    Protected Sub CheckBoxSeries_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxSeries.CheckedChanged
        SetChartData()
    End Sub

    Protected Sub CheckBoxLegend_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxLegend.CheckedChanged
        SetChartData()
    End Sub
#End Region

#Region "Page_indexchanged"
    'Protected Sub gdGraphreport_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gdGraphreport.PageIndexChanging
    '    Chart1.Visible = True
    '    If rbnAnalysis.Checked = True And rbnReport.Checked = False Then
    '        If gdGraphreport.PageIndex < gdGraphreport.PageCount And gdGraphreport.PageIndex >= 0 Then
    '            gdGraphreport.PageIndex = e.NewPageIndex
    '            gridbind()
    '        End If
    '    ElseIf rbnAnalysis.Checked = False And rbnReport.Checked = True Then
    '        If gdGraphreport.PageIndex < gdGraphreport.PageCount And gdGraphreport.PageIndex >= 0 Then
    '            gdGraphreport.PageIndex = e.NewPageIndex
    '            gridreportbind()
    '        End If
    '    End If


    'End Sub
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

    Protected Sub chk_Disabled_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chk_Disabled.CheckedChanged
        If (chk_Disabled.Checked = False) Then

            '// Enable legend
            Chart1.Legends("Default").Enabled = True

            '// Enable controls
            ddlLegendAlinmentList.Enabled = True
            ddlLegendDockingList.Enabled = True
            ddlTheTableStyle.Enabled = True
            ddlLegendStyleList.Enabled = True
            chk_InsideChartArea.Enabled = True
            chk_Reversed.Enabled = True


        Else

            '// Disable legend
            Chart1.Legends("Default").Enabled = False

            '// Disable controls
            ddlLegendAlinmentList.Enabled = False
            ddlLegendDockingList.Enabled = False
            ddlTheTableStyle.Enabled = False
            ddlLegendStyleList.Enabled = False
            chk_InsideChartArea.Enabled = False
            chk_Reversed.Enabled = False
        End If
    End Sub
End Class



