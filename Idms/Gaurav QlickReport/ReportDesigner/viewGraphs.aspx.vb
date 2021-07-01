
Imports System.Data
Imports System.Data.SqlClient
Imports System
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
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
Imports System.Math
Imports System.Web.UI.WebControls.WebParts
Imports System.Drawing.FontFamily
Partial Class ReportDesigner_viewGraphs
    Inherits System.Web.UI.Page
    Dim con As String = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con)
    Dim com As SqlCommand
    Dim com1 As SqlCommand
    Dim data1 As SqlDataReader
    Public rep As String = ""
    Dim readquery As SqlDataReader
    Dim fb As Boolean
    Dim dsImage As New DataSet
    Dim bo As Boolean = False
    Dim t, chartloopindex, l, pp, mm, columncount1 As Integer
    Dim i As Double
    Dim bool1 As Boolean = True

    Dim chartcolarr
    Dim tablecolumn
    
    Dim chartcolumn, finalquery, newfinalquery, nn, m1, sumstr, selectquery, b, summcol, fmname, newsmzdquery, oldfinalquery, reportcol, newrepcol, duplicate, dupcol, columnseries, colname, newcolname, strcol, firsttablecolum As String
    Dim finalrepquery As String = ""
    Dim chartcolarray
    Dim newchartcolarr
    Dim formulaname
    Dim int As Integer = 0
    Dim k As Integer = 0
    Dim checkrowseries As Boolean
    Dim count, blankravl, iloopindex, totalcolcount As Integer
    Dim chartcol As Chart
    Dim label1 As Label
    Dim label2 As Label
    Dim label3 As Label
    Dim label4 As Label
    Dim xval As String
    Dim row As DataRow
    Dim rowname As String
    Dim rval, counter As Integer
    Dim sval As String
    Dim str, clumnser As String
    Public repName As String
    Public dept As String
    Public clt As String
    Public lob As String
    Public hidSelect As String = ""
    Public hidFrom As String = ""
    Public hidcolumn As String = ""
    Public hidWhere As String = ""
    Public hidGroup As String = ""
    Public hidHaving As String = ""
    Public hidorder As String = ""
    Dim newfinalqueryarr
    Dim strquery1, strformulaname, strfinal, strvalue, strvalue1, strfinalfor, groupbytext, havingtxt, strfinalcolumn, strnormalcol As String
    Dim hidi, ll, p, jk As Integer
    Dim hidcolarray
    Dim hidfromarray
    Dim forarray
    Dim normalcolarray
    Dim newarray
    Public hidlegendformat As String = ""
    Public hidcommanformat As String = ""
    Public hidcommanformat1 As String = ""
    Public hidcommanformat2 As String = ""
    Public hidcommanformat3 As String = ""
    Public hidTitle As String = ""
    Public hidxlablefont As String = ""
    Public hidxlablefontstyle As String = ""
    Public hidYlablefont As String = ""
    Public hidYlablefontstyle As String = ""
    Public hidtitleFont As String = ""
    Public hidTitlesize As String = ""
    Public hidtitlefontcolor As String = ""
    Public hidAlignment As String = ""
    Public hidMajorgridline As String = ""
    Public hidLinetypes As String = ""
    Public hidMinorType As String = ""
    Public hidMinoeLinetypes As String = ""
    Public hidMajorgridcolour As String = ""
    Public hidMajorline As String = ""
    Public hidMajorInterval As String = ""
    Public hidMinorColor1 As String = ""
    Public hidMinorWidth As String = ""
    Public hidMinorInterval As String = ""
    Public hidpalletes As String = ""
    Public hidstyle As String = ""
    Public hiddock As String = ""
    Public hidAlin As String = ""
    Public hidrev As String = ""
    Public hidtabst As String = ""
    Public hidlbk As String = ""
    Public hidlbr As String = ""
    Public hidgrd As String = ""
    Public hidhat As String = ""
    Public hidbrs As String = ""
    Public hidbrst As String = ""
    Public chart2xaxistiele As String = ""

    Public hidbackcolor As String = ""
    Public hidareabkcolor As String = ""
    Public hidbrcolor As String = ""
    Dim commonvalue As String
    Dim commanarray

    Dim dval, dateval, dat As Date
    Dim opencommanformat
    Dim bool As Boolean
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Public Sub Repgraph()
        Dim repName, anasource, viewrep As String
        repName = Request.QueryString("repname")
        viewrep = Request.QueryString("repnm")
        anasource = Request.QueryString("source")
        If repName = "" And viewrep = "" Then
            Close_msgbox("No Data Found")
            btngraph.Visible = False
            ddlchart.Visible = False
            Exit Sub
        End If

        If viewrep <> "" And anasource = "" Then
            btngraph.Visible = False
            ddlchart.Visible = True
            Try
                Dim chartname, Repcharttype As String
                Dim chartseries As String
                ' set the source of the data for the repeater control and bind it 
                chartcolumn = LCase(Session("Queryname"))
                ''''' from reportdesigner
                hidSelect = Session("colnames")
                hidcolarray = hidSelect.Split("~")
                Me.hidFrom = Session("tables")
                Me.hidWhere = Session("wheredata")
                Me.hidGroup = Session("groupdata")
                Me.hidorder = Session("orderdata")
                Me.hidHaving = Session("having")
                '''''''''''''''''''''
                Dim nm As Integer = chartcolumn.LastIndexOf("#")
                Dim nm1 As Integer = chartcolumn.Length
                Dim nm2 As Integer = nm1 - nm
                finalquery = chartcolumn.Substring(nm + 1, nm2 - 1)
                oldfinalquery = finalquery
                finalquery = "#" + finalquery
                finalquery = chartcolumn.Replace(finalquery, "")
                If finalquery.Contains(" order by") = True Then
                    Dim newnm As Integer = finalquery.LastIndexOf(" order by")
                    Dim newnm1 As Integer = finalquery.Length
                    Dim newnm2 As Integer = newnm1 - newnm
                    newfinalquery = finalquery.Substring(newnm + 1, newnm2 - 1)
                    newfinalquery = finalquery.Replace(newfinalquery, "")
                Else
                    newfinalquery = finalquery
                End If
                Dim myCommand1 As New SqlCommand(finalquery, conn)
                ' Open the connection	
                myCommand1.Connection.Open()
                ' Initializes a new instance of the OleDbDataAdapter class
                Dim myDataAdapter As New SqlDataAdapter
                myDataAdapter.SelectCommand = myCommand1
                ' Initializes a new instance of the DataSet class
                Dim myDataSet1 As New DataSet()
                ' Adds rows in the DataSet
                myDataAdapter.Fill(myDataSet1, "Query")
                myCommand1.Connection.Close()
                Dim strQuery As String = "select graphtype,columnseries,ColumnName,commanformat,legendformat,commanformat1,commanformat2,reporttype from idmsgraphmaster where queryname='" + viewrep + "' and ColumnName<>''"
                Dim da1 As New SqlDataAdapter(strQuery, conn)
                conn.Open()
                da1.Fill(dsImage)
                conn.Close()

                For l = 0 To myDataSet1.Tables(0).Columns.Count - 1
                    If firsttablecolum = "" Then
                        firsttablecolum = myDataSet1.Tables("Query").Columns(l).ColumnName
                    Else
                        firsttablecolum = firsttablecolum + "," + myDataSet1.Tables("Query").Columns(l).ColumnName
                    End If
                Next
                tablecolumn = firsttablecolum.Split(",")
                totalcolcount = dsImage.Tables(0).Rows.Count
                If totalcolcount = 0 Then
                    aspnet_msgbox("This Report Does Not Contain Any Graph")
                    Exit Sub
                End If
                strfinal = strnormalcol + "," + strfinalfor
                If hidGroup <> "" Then
                    groupbytext = "group by " & " " & hidGroup
                Else
                    groupbytext = ""
                End If

                If hidHaving <> "" Then
                    havingtxt = "having " & " " & hidHaving
                Else
                    havingtxt = ""
                End If
                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                For i = 0 To dsImage.Tables(0).Rows.Count - 1
                    Dim fontstylecheckbox As New FontStyle

                    summcol = ""
                    b = ""
                    strfinalfor = ""
                    strnormalcol = ""
                    bo = True

                    Repcharttype = dsImage.Tables(0).Rows(i)("Reporttype").ToString()
                    newcolname = dsImage.Tables(0).Rows(i)("ColumnName").ToString()
                    chartname = dsImage.Tables(0).Rows(i)("graphtype").ToString()
                    chartseries = dsImage.Tables(0).Rows(i)("columnseries").ToString()
                    colname = dsImage.Tables(0).Rows(i)("ColumnName").ToString()
                    colname = LCase(colname)
                    newchartcolarr = colname.Split(",")
                    hidcommanformat = dsImage.Tables(0).Rows(i)("commanformat").ToString()
                    hidcommanformat1 = dsImage.Tables(0).Rows(i)("commanformat1").ToString()
                    hidcommanformat3 = dsImage.Tables(0).Rows(i)("commanformat2").ToString()
                    hidlegendformat = dsImage.Tables(0).Rows(i)("Legendformat").ToString()
                    chartcolarr = colname.Split(",")
                    Dim chart1(totalcolcount) As Chart
                    Dim ChtToolbar(totalcolcount) As ChartToolbar
                    Dim ddlchartsize(totalcolcount) As DropDownList
                    hidcommanformat2 = hidcommanformat + "$" + hidcommanformat1 + "$" + hidcommanformat3 + "$" + hidlegendformat
                    opencommanformat = hidcommanformat2.Split("$")
                    Dim commanindexloop As Integer
                    chart1(i) = New Chart()
                    ChtToolbar(i) = New ChartToolbar
                    ChtToolbar(i).ChartID = chart1(i).ToString
                    ddlchartsize(i) = New DropDownList
                    ddlchartsize(i).Items.Add("100%")
                    chart1(i).ChartAreas.Add("chartar")
                    chart1(i).Width = 1000
                    chart1(i).Height = 500

                    'Gopal Chanages
                    chart1(i).Attributes("alt") = chartname
                    'End Gopal Changes

                    'Lalit change for CI-162 Compl.
                    'Start
                    chart1(i).MapEnabled = False
                    'chart1(i).MapEnabled = True

                    'End

                    For commanindexloop = 0 To (opencommanformat.Length - 1)
                        commonvalue = opencommanformat(commanindexloop)
                        commanarray = commonvalue.Split(":")
                        'chart1(i).ChartAreas("chartar").Area3DStyle.Clustered = True
                        'chart1(i).ChartAreas("chartar").Area3DStyle.WallWidth = 0
                        'chart1(i).ChartAreas("chartar").ShadowColor = Color.Black


                        If commanarray(0) = "X3DAngle" Then
                            chart1(i).ChartAreas("chartar").Area3DStyle.XAngle = Single.Parse(commanarray(1))
                        ElseIf commanarray(0) = "Perspective" Then
                            chart1(i).ChartAreas("chartar").Area3DStyle.Perspective = Single.Parse(commanarray(1))
                        ElseIf commanarray(0) = "Y3Dangle" Then
                            chart1(i).ChartAreas("chartar").Area3DStyle.YAngle = Single.Parse(commanarray(1))
                        ElseIf commanarray(0) = "Chk3D" Then
                            If commanarray(1) = "on" Then
                                chart1(i).ChartAreas("chartar").Area3DStyle.Enable3D = True
                            Else
                                chart1(i).ChartAreas("chartar").Area3DStyle.Enable3D = False
                            End If
                           

                        ElseIf commanarray(0) = "Palettes" Then
                            hidpalletes = commanarray(1)
                        ElseIf commanarray(0) = "Bkclr" Then
                            If commanarray(1) = "" Then
                                chart1(i).BackColor = Color.White
                            Else
                                chart1(i).BackColor = System.Drawing.ColorTranslator.FromHtml(commanarray(1))
                            End If

                        ElseIf commanarray(0) = "Gradient" Then
                            chart1(i).BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), commanarray(1)), GradientType)
                        ElseIf commanarray(0) = "Hatchstyle" Then
                            chart1(i).BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), commanarray(1)), ChartHatchStyle)
                        ElseIf commanarray(0) = "Brclr" Then

                            chart1(i).BorderColor = System.Drawing.ColorTranslator.FromHtml(commanarray(1))
                        ElseIf commanarray(0) = "Bordersize" Then
                            chart1(i).BorderWidth = Integer.Parse(commanarray(1))
                        ElseIf commanarray(0) = "Borderstyle" Then
                            chart1(i).BorderStyle = DirectCast(ChartDashStyle.Parse(GetType(ChartDashStyle), commanarray(1)), ChartDashStyle)
                        ElseIf commanarray(0) = "style" Then
                            hidstyle = commanarray(1)
                        ElseIf commanarray(0) = "Dock" Then
                            hiddock = commanarray(1)
                        ElseIf commanarray(0) = "Alin" Then
                            hidAlin = commanarray(1)
                        ElseIf commanarray(0) = "onRev" Then
                            hidrev = commanarray(1)
                        ElseIf commanarray(0) = "tabst" Then
                            hidtabst = commanarray(1)
                        ElseIf commanarray(0) = "lbk" Then
                            hidlbk = commanarray(1)
                        ElseIf commanarray(0) = "lbr" Then
                            hidlbr = commanarray(1)
                        ElseIf commanarray(0) = "grd" Then
                            hidgrd = commanarray(1)
                        ElseIf commanarray(0) = "hat" Then
                            hidhat = commanarray(1)
                        ElseIf commanarray(0) = "brs" Then
                            hidbrs = commanarray(1)
                        ElseIf commanarray(0) = "brst" Then
                            hidbrst = commanarray(1)
                        ElseIf commanarray(0) = "Xlabelfont" Then
                            hidxlablefont = commanarray(1)
                        ElseIf commanarray(0) = "Xfontsizelist" Then
                            hidxlablefontstyle = commanarray(1)
                        ElseIf commanarray(0) = "XLabelcolour" Then
                            chart1(i).ChartAreas("chartar").AxisX.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(commanarray(1))
                        ElseIf commanarray(0) = "Xontanglelist" Then
                            chart1(i).ChartAreas("chartar").AxisX.LabelStyle.FontAngle = Single.Parse(commanarray(1))
                        ElseIf commanarray(0) = "Offset" Then
                            If commanarray(1) = "on" Then
                                chart1(i).ChartAreas("chartar").AxisX.LabelStyle.OffsetLabels = True
                            Else
                                chart1(i).ChartAreas("chartar").AxisX.LabelStyle.OffsetLabels = False
                            End If
                        ElseIf commanarray(0) = "Enable" Then
                            If commanarray(1) = "on" Then
                                chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Enabled = True
                            Else
                                chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Enabled = False
                            End If
                        ElseIf commanarray(0) = "Yfont" Then
                            hidYlablefont = commanarray(1)
                        ElseIf commanarray(0) = "Yfontsize" Then
                            hidYlablefontstyle = commanarray(1)
                        ElseIf commanarray(0) = "Yfontcolour" Then
                            chart1(i).ChartAreas("chartar").AxisY.LabelStyle.FontColor = System.Drawing.ColorTranslator.FromHtml(commanarray(1))
                        ElseIf commanarray(0) = "Yangle" Then
                            chart1(i).ChartAreas("chartar").AxisY.LabelStyle.FontAngle = Single.Parse(commanarray(1))
                        ElseIf commanarray(0) = "chkoffset" Then
                            If commanarray(1) = "on" Then
                                chart1(i).ChartAreas("chartar").AxisY.LabelStyle.OffsetLabels = True
                            Else
                                chart1(i).ChartAreas("chartar").AxisY.LabelStyle.OffsetLabels = False

                            End If
                        ElseIf commanarray(0) = "Yenable" Then
                            If commanarray(1) = "on" Then
                                chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Enabled = True
                            Else
                                chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Enabled = False
                            End If
                        ElseIf commanarray(0) = "Charttitle" Then
                            hidTitle = commanarray(1)



                        ElseIf commanarray(0) = "XAxisTitle" Then
                            chart2xaxistiele = commanarray(1).ToString()

                            chart1(i).ChartAreas("chartar").AxisX.Title = commanarray(1)
                        ElseIf commanarray(0) = "Yaxistitle" Then
                            chart1(i).ChartAreas("chartar").AxisY.Title = commanarray(1)
                        ElseIf commanarray(0) = "Titlesize" Then
                            hidTitlesize = commanarray(1)
                        ElseIf commanarray(0) = "Font" Then
                            hidtitleFont = commanarray(1)
                        ElseIf commanarray(0) = "Color" Then
                            hidtitlefontcolor = commanarray(1)

                        ElseIf commanarray(0) = "BrClr" Then
                            hidbackcolor = commanarray(1)
                        ElseIf commanarray(0) = "ArClr" Then
                            hidareabkcolor = commanarray(1)
                        ElseIf commanarray(0) = "BkClr" Then
                            hidbrcolor = commanarray(1)
                        ElseIf commanarray(0) = "Alignment" Then
                            hidAlignment = commanarray(1)
                        ElseIf commanarray(0) = "Italic" Then
                            If commanarray(1) = "on" Then
                                fontstylecheckbox = FontStyle.Italic
                            End If
                        ElseIf commanarray(0) = "Bold" Then
                            If commanarray(1) = "on" Then
                                fontstylecheckbox = FontStyle.Bold

                            End If
                        ElseIf commanarray(0) = "Strikeout" Then
                            If commanarray(1) = "on" Then
                                fontstylecheckbox = FontStyle.Strikeout
                            End If
                        ElseIf commanarray(0) = "Mjrgdline" Then
                            hidMajorgridline = commanarray(1)
                        ElseIf commanarray(0) = "Linetypes" Then
                            hidLinetypes = commanarray(1)
                        ElseIf commanarray(0) = "Mjrgdclr" Then
                            hidMajorgridcolour = commanarray(1)
                        ElseIf commanarray(0) = "Mjrline" Then
                            hidMajorline = commanarray(1)
                        ElseIf commanarray(0) = "MjrInt" Then
                            hidMajorInterval = commanarray(1)
                        ElseIf commanarray(0) = "MnrType" Then
                            hidMinorType = commanarray(1)
                        ElseIf commanarray(0) = "MnrgidLineType" Then
                            hidMinoeLinetypes = commanarray(1)
                        ElseIf commanarray(0) = "MnrClr" Then
                            hidMinorColor1 = commanarray(1)
                        ElseIf commanarray(0) = "MnrWidth" Then
                            hidMinorWidth = commanarray(1)
                        ElseIf commanarray(0) = "MnrInt" Then
                            hidMinorInterval = commanarray(1)
                        End If
                    Next
                    'smitha
                   
                    chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFit = False
                    'chart1(i).Series(strcol).AxisLabel = sds
                    chart1(i).ChartAreas("chartar").AxisX.LabelStyle.ShowEndLabels = True


                    chart1(i).ChartAreas("chartar").AxisX.Interval = 1

                    chart1(i).ChartAreas("chartar").Position.Auto = False
                    chart1(i).ChartAreas("chartar").Position.X = 5
                    chart1(i).ChartAreas("chartar").Position.Y = 5
                    chart1(i).ChartAreas("chartar").Position.Width = 90
                    chart1(i).ChartAreas("chartar").Position.Height = 90

                    chart1(i).ChartAreas("chartar").InnerPlotPosition.X = 5
                    chart1(i).ChartAreas("chartar").InnerPlotPosition.Y = 5
                    chart1(i).ChartAreas("chartar").InnerPlotPosition.Width = 90
                    chart1(i).ChartAreas("chartar").InnerPlotPosition.Height = 80

                    chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFit = False
                    chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Enabled = False
                    chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Enabled = False
                    chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.Enabled = False
                    chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Enabled = False
                    chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.Enabled = False


                    chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.Enabled = False
                    chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.Enabled = False
                    'smitha

                    '---------Start Legend Style--------------
                    ' Set legend style 
                    chart1(i).Legends("Default").LegendStyle = DirectCast(LegendStyle.Parse(GetType(LegendStyle), hidstyle), LegendStyle)

                    ' Set legend docking 
                    chart1(i).Legends("Default").Docking = DirectCast(Docking.Parse(GetType(LegendDocking), hiddock), LegendDocking)

                    ' Set legend alignment 
                    chart1(i).Legends("Default").Alignment = DirectCast(StringAlignment.Parse(GetType(StringAlignment), hidAlin), StringAlignment)

                    ' Set whether the legend is reversed 
                    If hidrev = "on" Then
                        chart1(i).Legends("Default").Reversed = AutoBool.[True]
                    Else
                        chart1(i).Legends("Default").Reversed = AutoBool.[False]
                    End If

                    ' Set table style 
                    chart1(i).Legends("Default").TableStyle = DirectCast(LegendTableStyle.Parse(GetType(LegendTableStyle), hidtabst), LegendTableStyle)

                    ' Set Legend visual attributes 
                    chart1(i).Legends("Default").BackColor = System.Drawing.ColorTranslator.FromHtml(hidlbk)
                    chart1(i).Legends("Default").BorderColor = System.Drawing.ColorTranslator.FromHtml(hidlbr)
                    chart1(i).Legends("Default").BackGradientType = DirectCast(GradientType.Parse(GetType(GradientType), hidgrd), GradientType)
                    chart1(i).Legends("Default").BackHatchStyle = DirectCast(ChartHatchStyle.Parse(GetType(ChartHatchStyle), hidhat), ChartHatchStyle)
                    chart1(i).Legends("Default").BorderWidth = Integer.Parse(hidbrs)
                    chart1(i).Legends("Default").BorderStyle = DirectCast(ChartDashStyle.Parse(GetType(ChartDashStyle), hidbrst), ChartDashStyle)
                    '---------End Legend Style----------------
                    If ddlchart.SelectedItem.Text = "100%" Then
                       
                        'Set Chart Axis title font
                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse(hidTitlesize), fontstylecheckbox)
                            ' chart1(i).Titles(0).Font = New Font(hidtitleFont, Single.Parse(hidTitlesize), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font("Arial", Single.Parse(hidTitlesize), fontstylecheckbox)
                        End Try
                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse(hidTitlesize), fontstylecheckbox)

                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font("Arial", Single.Parse(hidTitlesize), fontstylecheckbox)
                        End Try
                        chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFit = False
                        chart1(i).ChartAreas("chartar").AxisX2.LabelsAutoFit = False

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse(hidxlablefontstyle))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse(hidYlablefontstyle))
                    ElseIf ddlchart.SelectedItem.Text = "125%" Then
                        chart1(i).Width = 1250
                        chart1(i).Height = 625
                        'Set Chart Axis title font
                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("18"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("18"), fontstylecheckbox)

                        End Try

                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("18"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("18"), fontstylecheckbox)
                        End Try

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse("15"))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse("15"))
                    ElseIf ddlchart.SelectedItem.Text = "150%" Then
                        chart1(i).Width = 1500
                        chart1(i).Height = 750
                        'Set Chart Axis title font
                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("19"), fontstylecheckbox)
                            chart1(i).Titles(0).Font = New Font(hidtitleFont, Single.Parse("19"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("19"), fontstylecheckbox)
                        End Try



                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("19"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("19"), fontstylecheckbox)
                        End Try

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse("18"))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse("18"))
                    ElseIf ddlchart.SelectedItem.Text = "175%" Then
                        chart1(i).Width = 1750
                        chart1(i).Height = 875
                        'Set Chart Axis title font
                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("20"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("20"), fontstylecheckbox)
                        End Try
                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("20"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("20"), fontstylecheckbox)
                        End Try

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse("19"))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse("19"))
                    ElseIf ddlchart.SelectedItem.Text = "200%" Then
                        chart1(i).Width = 2000
                        chart1(i).Height = 1000
                        'Set Chart Axis title font
                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("21"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("21"), fontstylecheckbox)
                        End Try
                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("21"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("21"), fontstylecheckbox)
                        End Try

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse("20"))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse("20"))
                    ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                        chart1(i).Width = 9000
                        chart1(i).Height = 1400
                        'Set Chart Axis title font

                        Try
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("22"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisX.TitleFont = New Font(hidtitleFont, Single.Parse("22"), fontstylecheckbox)
                        End Try
                        Try
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("22"), fontstylecheckbox)
                        Catch ex As Exception
                            chart1(i).ChartAreas("chartar").AxisY.TitleFont = New Font(hidtitleFont, Single.Parse("22"), fontstylecheckbox)
                        End Try

                        chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Font = New Font(hidxlablefont, Single.Parse("21"))
                        chart1(i).ChartAreas("chartar").AxisY.LabelStyle.Font = New Font(hidYlablefont, Single.Parse("21"))
                    End If
                    Dim apnagroupby As String = ""

                    For ll = 0 To hidcolarray.length - 1
                        strvalue = hidcolarray(ll)

                        For p = 0 To chartcolarr.Length - 1
                            'If strvalue.Contains("(") Or strvalue.Contains(")") Or strvalue.Contains("*") Or strvalue.Contains("+") Or strvalue.Contains("-") Or strvalue.Contains("/") Then
                            If LCase(strvalue).Contains("max(") Or LCase(strvalue).Contains("min(") Or LCase(strvalue).Contains("count(") Or LCase(strvalue).Contains("sum(") Or LCase(strvalue).Contains("avg(") Then

                                Dim strvalue3 = Split(strvalue, " AS ")
                                If strvalue3.Length > 1 Then
                                    strfinalcolumn = strvalue3(1)
                                    strfinalcolumn = strfinalcolumn.Replace("[", "")
                                    strfinalcolumn = strfinalcolumn.Replace("]", "")
                                    strfinalcolumn = LCase(strfinalcolumn)
                                End If


                                If chartcolarr(p) = strfinalcolumn Then
                                    If strfinalfor = "" Then
                                        summcol = strfinalcolumn
                                        strfinalfor = strvalue
                                    Else
                                        summcol = summcol + "," + strfinalcolumn
                                        strfinalfor = strfinalfor + "," + strvalue
                                    End If
                                End If
                            Else
                                Dim fgh
                                Dim fr
                                If strvalue.Contains("AS") = True Then
                                    fgh = Split(strvalue, " AS ")
                                    strfinalcolumn = strvalue
                                    If fgh.length > 1 Then
                                        strfinalcolumn = fgh(1)
                                        strfinalcolumn = Replace(strfinalcolumn, "[", "")
                                        strfinalcolumn = Replace(strfinalcolumn, "]", "")
                                        strfinalcolumn = LCase(strfinalcolumn)
                                        If chartcolarr(p) = strfinalcolumn Then

                                            If b = "" Then
                                                b = fgh(0)
                                                'strnormalcol = fgh(0)
                                                'add
                                                apnagroupby = fgh(0)
                                                strnormalcol = strvalue
                                            Else
                                                'b = b + "," + fgh(0)
                                                'add
                                                apnagroupby = apnagroupby + "," + fgh(0)
                                                'strnormalcol = strnormalcol + "," + fgh(0)
                                                b = b + "," + strvalue
                                                strnormalcol = strnormalcol + "," + strvalue
                                            End If
                                        End If
                                    End If
                                ElseIf strvalue.Contains("$") = True Then
                                    Dim asdtr As String
                                    asdtr = strvalue
                                    asdtr = asdtr.Replace("$", ".")
                                    fgh = Split(asdtr, ".")
                                    strfinalcolumn = fgh(1)
                                    strfinalcolumn = LCase(strfinalcolumn)
                                    If chartcolarr(p) = strfinalcolumn Then
                                        If b = "" Then
                                            b = asdtr
                                            'add
                                            apnagroupby = asdtr
                                            strnormalcol = asdtr
                                        Else
                                            b = b + "," + asdtr
                                            strnormalcol = strnormalcol + "," + asdtr
                                            'add
                                            apnagroupby = apnagroupby + "," + asdtr
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    Next
                    Dim summarr
                    summarr = strnormalcol.Split(",")
                    Dim strorder As String
                    If strnormalcol = "" Then
                        strorder = ""
                    Else
                        strorder = " order by " + " " + summarr(0)
                    End If

                    If strfinalfor = "" Then
                        fmname = strnormalcol
                    ElseIf strnormalcol = "" Then
                        fmname = strfinalfor
                    Else
                        fmname = strnormalcol + "," + strfinalfor
                    End If


                    If Repcharttype = "Summarized" Then
                        Dim grpby = ""
                        If b = "" Then
                            grpby = ""
                        Else
                            grpby = " Group by" + " " + b
                        End If

                        If strfinalfor = "" Then
                            m1 = strnormalcol
                        Else
                            If strnormalcol = "" Then
                                m1 = strfinalfor
                            Else
                                m1 = strnormalcol + "," + strfinalfor
                            End If

                        End If

                        'hidGroup = " group by " + " " + strnormalcol
                        'selectquery = "select " + m1 + " from " + hidFrom + hidWhere + grpby + strorder + " "'commented by atul on 30Oct2009 as order by was giving error
                        selectquery = "select " + m1 + " from " + hidFrom + hidWhere + grpby + " "
                    Else
                        'hidGroup = " group by " + " " + strnormalcol

                        If apnagroupby = "" Then
                            hidGroup = ""
                        Else
                            hidGroup = " group by " + " " + apnagroupby
                        End If

                        'selectquery = "Select " + fmname + " From " + hidFrom + hidWhere + hidGroup + strorder + " "'commented by atul on 30Oct2009 as order by was giving error
                        selectquery = "Select " + fmname + " From " + hidFrom + hidWhere + hidGroup + " "
                    End If
                    chart1(i).Title = hidTitle
                    If hidbackcolor = "" Then
                        hidbackcolor = "#ffffff"
                    End If
                    If hidareabkcolor = "" Then
                        hidareabkcolor = "#ffffff"
                    End If
                    'smitha
                    If hidMajorgridcolour = "" Then
                        hidMajorgridcolour = "#000000"
                    End If
                    If hidMinorColor1 = "" Then
                        hidMinorColor1 = "#000000"
                    End If

                    'smitha
                    chart1(i).Palette = DirectCast(ChartColorPalette.Parse(GetType(ChartColorPalette), hidpalletes.ToString), ChartColorPalette)
                    chart1(i).Titles(0).BorderColor = System.Drawing.ColorTranslator.FromHtml(hidbackcolor)
                    chart1(i).ChartAreas("chartar").BackColor = System.Drawing.ColorTranslator.FromHtml(hidareabkcolor)
                    chart1(i).Titles(0).Color = System.Drawing.ColorTranslator.FromHtml(hidtitlefontcolor)
                    'smitha this is to apply color on lable in x and y axis
                    chart1(i).ChartAreas("chartar").AxisX.TitleColor = System.Drawing.ColorTranslator.FromHtml(hidtitlefontcolor)

                    chart1(i).ChartAreas("chartar").AxisY.TitleColor = System.Drawing.ColorTranslator.FromHtml(hidtitlefontcolor)
                    'smitha
                    chart1(i).Titles(0).BackColor = System.Drawing.ColorTranslator.FromHtml(hidbrcolor)

                    If hidAlignment = "Left" Then
                        chart1(i).Titles(0).Alignment = ContentAlignment.MiddleLeft
                    ElseIf hidAlignment = "Center" Then
                        chart1(i).Titles(0).Alignment = ContentAlignment.MiddleCenter
                    ElseIf hidAlignment = "Right" Then
                        chart1(i).Titles(0).Alignment = ContentAlignment.MiddleRight
                    End If

                    If hidMajorgridline = "Grid" And hidLinetypes = "Horizontal" Then
                        chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Enabled = False

                        chart1(i).ChartAreas("chartar").AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                        'smitha
                    ElseIf hidMajorgridline = "Grid" And hidLinetypes = "Vertical" Then
                        chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Enabled = True

                        'smitha
                        chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                        'smitha
                    ElseIf hidMajorgridline = "Tickmark" And hidLinetypes = "Horizontal" Then
                        chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Enabled = False
                        'smitha

                    ElseIf hidMajorgridline = "Tickmark" And hidLinetypes = "Vertical" Then
                        chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Enabled = False
                        'smitha


                    End If

                    If hidMinorType = "Grid" And hidMinoeLinetypes = "Horizontal" Then
                        chart1(i).ChartAreas("chartar").AxisY.MinorGrid.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisY.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)
                        'smitha
                    ElseIf hidMinorType = "Grid" And hidMinoeLinetypes = "Vertical" Then
                        chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisY.MinorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)

                        'smitha
                    ElseIf hidMinorType = "Tickmark" And hidMinoeLinetypes = "Horizontal" Then
                        chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisY.MinorGrid.Enabled = False
                        'smitha
                    ElseIf hidMinorType = "Tickmark" And hidMinoeLinetypes = "Vertical" Then
                        chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.Enabled = True
                        'smitha
                        chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Enabled = False
                        chart1(i).ChartAreas("chartar").AxisY.MinorGrid.Enabled = False
                        'smitha
                    End If

                    ' Set Grid lines and tick marks interval
                    chart1(i).ChartAreas("chartar").AxisX.MajorGrid.Interval = Double.Parse(hidMajorInterval)
                    chart1(i).ChartAreas("chartar").AxisY.MajorGrid.Interval = Double.Parse(hidMajorInterval)
                    chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.Interval = Double.Parse(hidMajorInterval)
                    chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.Interval = Double.Parse(hidMajorInterval)

                    chart1(i).ChartAreas("chartar").AxisX.MinorGrid.Interval = Double.Parse(hidMinorInterval)
                    chart1(i).ChartAreas("chartar").AxisY.MinorGrid.Interval = Double.Parse(hidMinorInterval)
                    chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.Interval = Double.Parse(hidMinorInterval)
                    chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.Interval = Double.Parse(hidMinorInterval)
                    'If hidMajorgridcolour = "" Or hidMajorgridcolour = "#000000" Then
                    '    hidMajorgridcolour = "#ffffff"
                    'End If
                    'smitha
                    If hidMajorgridcolour = "" Then
                        hidMajorgridcolour = "#000000"
                    End If
                    If hidMinorColor1 = "" Then
                        hidMinorColor1 = "#000000"
                    End If

                    'smitha
                    '' Set Line Color
                    ' chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFit = False

                    chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFitStyle = LabelsAutoFitStyle.None
                    chart1(i).ChartAreas("chartar").AxisX.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                    chart1(i).ChartAreas("chartar").AxisY.MajorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                    chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                    chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMajorgridcolour)
                    'If hidMinorColor1 = "" Or hidMinorColor1 = "#000000" Then
                    '    hidMinorColor1 = "#ffffff"
                    'End If
                    chart1(i).ChartAreas("chartar").AxisX.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1) ' Color.FromName(ddlMinorColor1.SelectedItem.Value)
                    chart1(i).ChartAreas("chartar").AxisY.MinorGrid.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1)
                    chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1)
                    chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.LineColor = System.Drawing.ColorTranslator.FromHtml(hidMinorColor1)

                    '' Set Line Width
                    chart1(i).ChartAreas("chartar").AxisX.MajorGrid.LineWidth = Single.Parse(hidMajorline)
                    chart1(i).ChartAreas("chartar").AxisY.MajorGrid.LineWidth = Single.Parse(hidMajorline)
                    chart1(i).ChartAreas("chartar").AxisX.MajorTickMark.LineWidth = Single.Parse(hidMajorline)
                    chart1(i).ChartAreas("chartar").AxisY.MajorTickMark.LineWidth = Single.Parse(hidMajorline)

                    chart1(i).ChartAreas("chartar").AxisX.MinorGrid.LineWidth = Single.Parse(hidMinorWidth)
                    chart1(i).ChartAreas("chartar").AxisY.MinorGrid.LineWidth = Single.Parse(hidMinorWidth)
                    chart1(i).ChartAreas("chartar").AxisX.MinorTickMark.LineWidth = Single.Parse(hidMinorWidth)
                    chart1(i).ChartAreas("chartar").AxisY.MinorTickMark.LineWidth = Single.Parse(hidMinorWidth)

                    chart1(i).Controls.Add(ddlchartsize(i))

                    Dim myCommand As New SqlCommand(selectquery, conn)
                    ' Open the connection	
                    myCommand1.Connection.Open()
                    ' Initializes a new instance of the OleDbDataAdapter class
                    Dim myDataAdapter1 As New SqlDataAdapter
                    myDataAdapter.SelectCommand = myCommand
                    ' Initializes a new instance of the DataSet class
                    Dim myDataSet As New DataSet()
                    ' Adds rows in the DataSet
                    myDataAdapter.Fill(myDataSet, "Query")
                    myCommand1.Connection.Close()

                    Dim arr(myDataSet.Tables(0).Rows.Count - 1) As String





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


                    If chartname = "Line" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String = ""
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                Dim ij As Integer = 0
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    'arr(ii) = sval
                                                    arr(ij) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    'sonu
                                                    'GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    'arr(ii) = sval
                                                    arr(ij) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                'arr(ii) = dval
                                                arr(ij) = sval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Line
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If

                                        End Try
                                        If nodesign = "" Then
                                            GoTo label3
                                        End If
                                        ''smitha
                                        chart1(0).Titles(0).Font = New Font(hidtitleFont, Single.Parse(hidTitlesize), fontstylecheckbox)
                                        '' smitha

                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes

                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        chart1(i).Series(str).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        chart1(i).Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        chart1(i).Series(str).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        chart1(i).Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        chart1(i).Series(str).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        chart1(i).Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        chart1(i).Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        chart1(i).Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes
                                        'chart1(i).Series(str).LabelToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX" & "name=#VALX"
                                        'chart1(i).Series(str).LegendToolTip = "Legend" + strcol
                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
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
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td")
                                bool1 = False
                            ElseIf (bool1 = False) Then

                                writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim strcol As String = ""
                            Dim kk As Integer
                            'Dim myArea As MapArea = New MapArea()
                            'myArea.Shape = MapAreaShape.Polygon
                            'chart1(i).MapAreas.Add(myArea)


                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1

                               
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
                                             

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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Line
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval As String
                                        Dim row1 As DataRow
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                'columnseries = row1(columncount1).ToString()
                                                columnseries = row1(smitha).ToString()
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)


                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                       
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount


                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer2 As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then
                                writer2.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer2)
                                ' writer2.WriteEndTag("</td")
                                writer2.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer2.WriteBeginTag("table><tr><td>")
                                'writer2.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer2)
                                writer2.WriteEndTag("</td></tr></table")
                                bool1 = True
                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Column" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    Dim ij As Integer = 0
                                    'If chartcolarr(counter) = rowname Then ranjit commented
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
                                                'arr(ii) = sval
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
                                                'arr(ii) = sval
                                                arr(ij) = sval

                                            End If

                                        End Try
                                        If sval = "" Then
                                            'arr(ii) = dval
                                            arr(ij) = dval

                                        End If

                                    End Try

                                    'End If ranjit commented
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Column
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0.0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If

                                        End Try
                                        If nodesign = "" Then
                                            GoTo label4
                                        End If

                                        'Gopal Changes
                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"
                                        'End Changes

                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        chart1(i).Series(str).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        chart1(i).Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        chart1(i).Series(str).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        chart1(i).Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        chart1(i).Series(str).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        chart1(i).Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        chart1(i).Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        chart1(i).Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes


                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
label4:
                                    Next colIndex
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True
                            End If

                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim kk As Integer
                            Dim strcol As String = ""
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Column
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""

                                        'Try
                                        '    'For columncount1 = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                        '    For Each row1 In myDataSet.Tables(0).Rows
                                        '        ' for each row (row 1 and onward), add the value as a point
                                        '        'columnseries = row1(columncount1).ToString()
                                        '        If smitha = -1 Then
                                        '            smitha = 1
                                        '        End If
                                        '        columnseries = row1(smitha).ToString()
                                        '        arrcolumn(10) = columnseries
                                        '        Dim dateval As Date

                                        '        Try
                                        '            If IsDBNull(row1(colname)) Then
                                        '                yval1 = 0
                                        '            Else
                                        '                yval1 = CDbl(row1(colname))
                                        '            End If

                                        '            Try
                                        '                If IsDBNull(row1(colname)) Then
                                        '                    dateval = "0"
                                        '                Else
                                        '                    dateval = CDate(row1(colname))
                                        '                End If

                                        '            Catch ex As Exception
                                        '                chart1.Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                        '            End Try
                                        '        Catch ex As Exception
                                        '            Try
                                        '                If IsDBNull(row1(colname)) Then
                                        '                    dateval = "0"
                                        '                Else
                                        '                    dateval = CDate(row1(colname))
                                        '                End If
                                        '                chart1.Series(strcol).Points.AddXY(columnseries, dateval)
                                        '            Catch ex1 As Exception
                                        '            End Try
                                        '            xval = row1(colname)
                                        '        End Try


                                        Try
                                            Dim sds = String.Empty


                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                'columnseries = row1(columncount1).ToString()
                                                columnseries = row1(smitha).ToString()
                                                sds = columnseries
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If

                                                    Catch ex As Exception
                                                        ' Dim dddd As String = columnseries + "  " + clumnser

                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes
                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"
                                                'End Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True

                                               

                                                ' Dim csds = chart1(i).Series(strcol).Label
                                                ' chart1(i).Series(strcol).Label = columnseries

                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                ' writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                ' writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If

                    ElseIf chartname = "Pie" Then
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim strcol As String = ""
                            Dim kk As Integer
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Pie
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                'columnseries = row1(columncount1).ToString()
                                                columnseries = row1(smitha).ToString()
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception

                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try

                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount

                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                ' writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2

                        End If
                        If chartseries = "Row" Then
                            Dim xval, dup As String
                            xval = ""
                            Dim row As DataRow
                            ' Dim ik As Integer
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                Dim ij As Integer = 0
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
                                        Try
                                            If IsDBNull(row(rowname)) Then
                                            Else
                                                rval = CInt(row(rowname))
                                            End If
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(rowname)) Then
                                                Else
                                                    dval = ""
                                                    dval = CDate(row(rowname)).Date
                                                    sval = row(rowname)
                                                    'arr(ii) = sval
                                                    arr(ij) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    'sonu
                                                    'GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    'arr(ii) = sval
                                                    arr(ij) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                'arr(ii) = dval
                                                arr(ij) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                If str <> "" And sval <> "" Then
                                    chart1(i).Page = Me
                                    Dim writer1 As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                                    If (bool1 = True) Then

                                        writer1.WriteBeginTag("table><tr><td>")
                                        chart1(i).RenderControl(writer1)
                                        '  writer1.WriteEndTag("</td")
                                        writer1.WriteEndTag("</td></tr></table")
                                        bool1 = False
                                    ElseIf (bool1 = False) Then
                                        writer1.WriteBeginTag("table><tr><td>")
                                        'writer1.WriteBeginTag("td>")
                                        chart1(i).RenderControl(writer1)
                                        writer1.WriteEndTag("</td></tr></table")
                                        bool1 = True

                                    End If
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
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)

                                chart1(i).Series(str).Type = SeriesChartType.Pie
                                chart1(i).Series(str).Color = Color.Blue
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
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

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = ""
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = ""
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            bb = row(columnName)
                                        End Try
                                        If nodesign = "" Then
                                            GoTo label5
                                        End If


                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes

                                        chart1(i).Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
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
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                ' writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Daughnt" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series

                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    'arr(ii) = sval
                                                    arr(0) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    'sonu
                                                    'GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    arr(0) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                arr(0) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Doughnut
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For hh = 1 To chartcolarr.length - 1
                                        For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                            columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    YVal = CDbl(row(columnName))
                                                End If

                                                chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                                nodesign = "no"
                                            Catch ex As Exception
                                                Try
                                                    If IsDBNull(row(columnName)) Then
                                                    Else
                                                        dateval = CDate(row(columnName))
                                                    End If

                                                    If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                        chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                        nodesign = "no"
                                                    End If

                                                Catch ex1 As Exception

                                                End Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    bb = row(columnName)
                                                End If

                                            End Try
                                            If nodesign = "" Then
                                                GoTo label6
                                            End If

                                            'Gopal Changes 

                                            chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                            'End Gopal Changes


                                            chart1(i).Series(str).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                            chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                            If ddlchart.SelectedItem.Text = "125%" Then
                                                chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                            ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                            ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                            ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                                'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
                                            End If
                                        Next colIndex
label6:
                                    Next
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                ' writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim kk As Integer
                            Dim strcol As String = ""
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Doughnut
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                ' columnseries = row1(columncount1).ToString()
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                columnseries = row1(smitha).ToString()

                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes


                                                chart1(i).Series(strcol).Label = "Value = #VALY\nName = #VALX" ' columnseries & "#VALY"
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & " Percentage=#PERCENT" & "Name = #VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Area" Then
                        If chartseries = "Row" Then
                            chart1(i).Series.Clear()
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            arr(0) = ""
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    arr(0) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    'sonu
                                                    'GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    arr(0) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                arr(0) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Area
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If
                                            If nodesign = "no" Then
                                                GoTo label7
                                            End If
                                        End Try

                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes


                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        chart1(i).Series(str).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        chart1(i).Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        chart1(i).Series(str).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        chart1(i).Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        chart1(i).Series(str).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        chart1(i).Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        chart1(i).Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        chart1(i).Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        chart1(i).Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        chart1(i).Series(str).LegendToolTip = "Legend" + strcol
                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
label7:
                                    Next colIndex
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim strcol As String = ""
                            Dim kk As Integer
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try

                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()

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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Area
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                'columnseries = row1(columncount1).ToString()
                                                columnseries = row1(smitha).ToString()

                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                '  writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Bar" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    arr(0) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    arr(0) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                arr(0) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Bar
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If

                                        End Try
                                        If nodesign = "" Then
                                            GoTo label8
                                        End If

                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes


                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
                                        End If
label8:
                                    Next colIndex
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim strcol As String = ""
                            Dim col As DataRow
                            Dim kk As Integer
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Bar
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                'columnseries = row1(columncount1).ToString()
                                                columnseries = row1(smitha).ToString()
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CInt(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                '  writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Stock" Then
                        chartseries = "Column"

                        If chartseries = "Column" Then
                            'Chart2.Visible = False
                            chart1(i).Visible = True
                            ' chart1(i).Series.Clear()
                            'Assign Data Source
                            chart1(i).DataSource = myDataSet.Tables(0)

                            Dim StockI As Integer

                            For StockI = 1 To myDataSet.Tables(0).Columns.Count - 1
                                chart1(i).Series.Add(myDataSet.Tables(0).Columns(StockI).ColumnName).Type = SeriesChartType.Stock
                                'chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowLabelAsValue = True
                                'chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).AxisLabel
                                chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ShowInLegend = True
                                chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).BorderWidth = (myDataSet.Tables(0).Columns.Count - StockI) * 4
                                chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMemberX = myDataSet.Tables(0).Columns(0).ColumnName.ToString
                                chart1(i).Series(myDataSet.Tables(0).Columns(StockI).ColumnName).ValueMembersY = myDataSet.Tables(0).Columns(StockI).ColumnName.ToString
                            Next
                            chart1(i).ChartAreas("chartar").AxisX.LabelStyle.Enabled = True

                            chart1(i).ChartAreas("chartar").AxisX.LabelsAutoFitStyle = LabelsAutoFitStyle.WordWrap
                            chart1(i).DataBind()

                            '' ''If chartseries = "Column" Then
                            '' ''    Dim columncount, colintval As Integer
                            '' ''    Dim cc, colseries, colstrval As String
                            '' ''    Dim colarr(10) As String
                            '' ''    Dim col As DataRow
                            '' ''    If myDataSet.Tables(0).Columns.Count < 4 Then
                            '' ''        chart1(i).Visible = False
                            '' ''        GoTo label1
                            '' ''    End If
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

                            '' ''                    chart1(i).Visible = False
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
                            '' ''                If strcol = duplicate Then
                            '' ''                    strcol = strcol + count.ToString
                            '' ''                    count = count + 1
                            '' ''                End If
                            '' ''                chart1(i).Series.Add(strcol)
                            '' ''                chart1(i).Series(strcol).Type = SeriesChartType.Stock
                            '' ''                chart1(i).Series(strcol).BorderWidth = 2
                            '' ''                Dim st As String
                            '' ''                st = strcol.Replace(count - 1, "")
                            '' ''                duplicate = st
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
                            '' ''                                YVal1 = CDbl(row1(colname))
                            '' ''                            End If

                            '' ''                            chart1(i).Series(strcol).Points.AddXY(columnseries, YVal1)
                            '' ''                        Catch ex As Exception
                            '' ''                            If IsDBNull(row1(colname)) Then
                            '' ''                            Else
                            '' ''                                xval = row1(colname)
                            '' ''                            End If
                            '' ''                        End Try

                            '' ''                        'Gopal Changes 

                            '' ''                        chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                            '' ''                        'End Gopal Changes


                            '' ''                        chart1(i).Series(strcol).ShowLabelAsValue = True
                            '' ''                        '' Enable SmartLabels.   
                            '' ''                        chart1(i).Series(strcol).SmartLabels.Enabled = True
                            '' ''                        ' Set the callout style.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                            '' ''                        ' Set the callout line color.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                            '' ''                        ' Set the callout line style.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                            '' ''                        ' Set the callout line width.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                            '' ''                        ' Set the callout line anchor cap.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                            '' ''                        ' Set the controlling moving directions.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                            '' ''                        '' Set the minimum distance that the labels can move.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                            '' ''                        '' Set the minimum distance that the labels can move.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                            '' ''                        '' Allow the labels to be placed outside the plotting area.
                            '' ''                        chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                            '' ''                        chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                            '' ''                        chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                            '' ''                        chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                            '' ''                        If ddlchart.SelectedItem.Text = "125%" Then
                            '' ''                            chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                            '' ''                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                            '' ''                            chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                            '' ''                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                            '' ''                            chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                            '' ''                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                            '' ''                            chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                            '' ''                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                            '' ''                            '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                            '' ''                        End If
                            '' ''                    Next row1
                            '' ''                Catch ex As Exception
                            '' ''                End Try
                            '' ''            End If
                            '' ''        Catch ex As Exception
                            '' ''        End Try
                            '' ''    Next columncount


                            '' ''End If

                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Scatter" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    arr(0) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    'sonu
                                                    'GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    arr(0) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                arr(0) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Point
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    ' For hh = 1 To chartcolarr.length - 1
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If

                                        End Try
                                        If nodesign = "" Then
                                            GoTo label9
                                        End If
                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes


                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        chart1(i).Series(str).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        chart1(i).Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        chart1(i).Series(str).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        chart1(i).Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        chart1(i).Series(str).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        chart1(i).Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        chart1(i).Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        chart1(i).Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        chart1(i).Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        chart1(i).Series(str).LegendToolTip = "Legend" + strcol
                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
label9:

                                    Next colIndex
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)
                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim colarr(10) As String
                            Dim col As DataRow
                            Dim strcol As String = ""
                            Dim kk As Integer
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Point
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                columnseries = row1(columncount1).ToString()
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try


                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount

                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Scaterplot" Then
                        If chartseries = "Row" Then
                            Dim xval As String
                            xval = ""
                            Dim row As DataRow
                            count = 1
                            duplicate = ""
                            Dim ii As Integer
                            Dim rowname As String
                            Dim rval, counter As Integer
                            counter = 0
                            Dim sval As String
                            For Each row In myDataSet.Tables(0).Rows
                                ' for each Row, add a new series
                                For ii = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                    rowname = myDataSet.Tables(0).Columns(ii).ColumnName
                                    rowname = LCase(rowname)
                                    If chartcolarr(counter) = rowname Then
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
                                                    arr(0) = sval
                                                End If

                                            Catch ex1 As Exception
                                                If rval <> 0 Then
                                                    For t = 0 To arr.Length - 1
                                                        arr(t) = ""
                                                    Next
                                                    GoTo label1
                                                End If
                                                If IsDBNull(row(rowname)) Then


                                                Else
                                                    sval = row(rowname)
                                                    arr(0) = sval
                                                End If

                                            End Try
                                            If sval = "" Then
                                                arr(0) = dval
                                            End If

                                        End Try

                                    End If
                                Next ii
                                Dim jk As Integer
                                Dim str As String
                                str = ""
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
                                Dim YVal As Double
                                Dim series0 As String
                                series0 = str
                                If str = duplicate Then
                                    str = str + count.ToString
                                    count = count + 1
                                End If
                                chart1(i).Series.Add(str)
                                chart1(i).Series(str).Type = SeriesChartType.Point
                                chart1(i).Series(str).BorderWidth = 2
                                Dim st As String
                                st = str.Replace(count - 1, "")
                                st = series0
                                duplicate = st
                                Dim colIndex, hh As Integer
                                Dim columnName As String
                                Dim bb As String = ""
                                Try
                                    Dim nodesign As String = ""
                                    For colIndex = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                        columnName = myDataSet.Tables(0).Columns(colIndex).ColumnName
                                        Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                YVal = CDbl(row(columnName))
                                            End If

                                            chart1(i).Series(str).Points.AddXY(columnName, YVal)
                                            nodesign = "no"
                                        Catch ex As Exception
                                            Try
                                                If IsDBNull(row(columnName)) Then
                                                Else
                                                    dateval = CDate(row(columnName))
                                                End If

                                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                    chart1(i).Series(str).Points.AddXY(columnName, dateval)
                                                    nodesign = "no"
                                                End If

                                            Catch ex1 As Exception

                                            End Try
                                            If IsDBNull(row(columnName)) Then
                                            Else
                                                bb = row(columnName)
                                            End If

                                        End Try
                                        If nodesign = "" Then
                                            GoTo label10
                                        End If

                                        'Gopal Changes 

                                        chart1(i).Series(str).MapAreaAttributes = "alt='Series" & str & "'"

                                        'End Gopal Changes


                                        chart1(i).Series(str).ShowLabelAsValue = True
                                        '' Enable SmartLabels.   
                                        chart1(i).Series(str).SmartLabels.Enabled = True
                                        ' Set the callout style.
                                        chart1(i).Series(str).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                        ' Set the callout line color.
                                        chart1(i).Series(str).SmartLabels.CalloutLineColor = Color.Red

                                        ' Set the callout line style.
                                        chart1(i).Series(str).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                        ' Set the callout line width.
                                        chart1(i).Series(str).SmartLabels.CalloutLineWidth = 1

                                        ' Set the callout line anchor cap.
                                        chart1(i).Series(str).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                        ' Set the controlling moving directions.
                                        chart1(i).Series(str).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MinMovingDistance = 100

                                        '' Set the minimum distance that the labels can move.
                                        chart1(i).Series(str).SmartLabels.MaxMovingDistance = 100

                                        '' Allow the labels to be placed outside the plotting area.
                                        chart1(i).Series(str).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                        chart1(i).Series(str).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                        chart1(i).Series(str).LegendToolTip = "Legend" + strcol
                                        chart1(i).Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                        If ddlchart.SelectedItem.Text = "125%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                            chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                            'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                            '    chart1(i).Series(str).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                        End If
label10:
                                    Next colIndex
                                    rval = blankravl
                                Catch ex As Exception
                                End Try
                                YVal = 0.0
                                dateval = "#12:00:00 AM#"
                                fb = False
                            Next row
                            'Render the chart control. 

                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                ' writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        Dim smitha As Integer = -1
                        If chartseries = "Column" Then
                            Dim columncount, colintval As Integer
                            Dim cc, colseries, colstrval As String
                            Dim strcol As String = ""
                            Dim colarr(10) As String
                            Dim col As DataRow

                            Dim kk As Integer
                            For columncount = 0 To (myDataSet.Tables(0).Columns.Count) - 1
                                Dim colname As String = myDataSet.Tables(0).Columns(columncount).ColumnName
                                cc = colname
                                Try
                                    For Each col In myDataSet.Tables(0).Rows
                                        colseries = col(columncount).ToString()
                                        Try
                                            If IsDBNull(col(colname)) Then
                                            Else
                                                colintval = CInt(col(colname))
                                                colarr(columncount) = cc
                                            End If
                                            Exit For
                                        Catch ex As Exception
                                            If strcol = "" Then
                                                chart1(i).Series(strcol).Points.Clear()
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Point
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval, strcolname As String
                                        Dim row1 As DataRow
                                        Dim cyal, columnco As Integer
                                        Dim yval1 As Double
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                If smitha = -1 Then
                                                    smitha = 1
                                                End If
                                                columnseries = row1(columncount1).ToString()
                                                Dim dateval As Date
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        yval1 = CDbl(row1(colname))
                                                    End If

                                                    Try
                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                    Catch ex As Exception
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries + "  " + clumnser, yval1)
                                                    End Try
                                                Catch ex As Exception
                                                    Try

                                                        If IsDBNull(row1(colname)) Then
                                                        Else
                                                            dateval = CDate(row1(colname))
                                                        End If
                                                        chart1(i).Series(strcol).Points.AddXY(columnseries, dateval)
                                                    Catch ex1 As Exception
                                                    End Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                End Try

                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1

                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            'Render the chart control. 
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                '  writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                ' writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Run" Then
                        Dim aSubGroup(myDataSet.Tables(0).Rows.Count - 1) As Single
                        Dim aData(myDataSet.Tables(0).Rows.Count - 1) As Single
                        Dim st1 As String = ""
                        Dim st2 As String = ""
                        If chartseries = "Column" Then
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
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
                                                    If IsDBNull(CSng(row1(0))) Then
                                                    Else
                                                        YVal1 = CSng(row1(0))
                                                    End If
                                                    If IsDBNull(CSng(row1(1))) Then
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
                                chart1(i).ChartAreas("chartar").AxisX.Interval = 2
                                Dim tmpSeries As Series = Run_Chart.CreateSeries(aSubGroup, aData, chart1(i))
                                ' Optionally before calling any chart creation function, you can setup styles for 
                                ' Control Lines. 

                                tmpSeries.ShowLabelAsValue = True
                                tmpSeries.SmartLabels.Enabled = True
                                tmpSeries.SmartLabels.MovingDirection = LabelAlignment.Top
                                tmpSeries.SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Round
                                tmpSeries.SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.No
                                tmpSeries.SmartLabels.CalloutStyle = LabelCalloutStyle.Box
                                tmpSeries.SmartLabels.HideOverlapped = False
                                tmpSeries.SmartLabels.CalloutBackColor = Color.Bisque

                                Run_Chart.UCLstyle.LineStyle = ChartDashStyle.Solid
                                Run_Chart.UCLstyle.LineColor = Color.Red
                                Run_Chart.UCLstyle.LineWidth = 2
                                ' Also you can set style for text 
                                ' Run_Chart.ShowText = True
                                Run_Chart.LCLstyle.TextColor = Color.Blue
                                Run_Chart.LCLstyle.TextFont = New Font("Arial", 10)

                                chart1(i).Page = Me
                                Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                                If (bool1 = True) Then

                                    writer.WriteBeginTag("table><tr><td>")
                                    chart1(i).RenderControl(writer)
                                    'writer.WriteEndTag("</td")
                                    writer.WriteEndTag("</td></tr></table")
                                    bool1 = False
                                ElseIf (bool1 = False) Then
                                    writer.WriteBeginTag("table><tr><td>")
                                    ' writer.WriteBeginTag("td>")
                                    chart1(i).RenderControl(writer)
                                    writer.WriteEndTag("</td></tr></table")
                                    bool1 = True

                                End If
                                GoTo label2
                            Next columncount
                        End If
                    ElseIf chartname = "Pareto" Then
                        If chartseries = "Column" Then
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        chart1(i).Series.Add(strcol)
                                        chart1(i).Series(strcol).Type = SeriesChartType.Column
                                        chart1(i).Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval As String
                                        Dim row1 As DataRow
                                        Dim YVal1 As Integer
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                columnseries = row1(0).ToString()
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        YVal1 = CInt(row1(colname))
                                                    End If

                                                    chart1(i).Series(strcol).Points.AddXY(columnseries, YVal1)
                                                Catch ex As Exception
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If

                                                    'Chart1.Series(strcol).Points.AddXY(columnseries + "" + xval, YVal1)
                                                End Try

                                                'Gopal Changes 

                                                chart1(i).Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                                'End Gopal Changes

                                                chart1(i).Series(strcol).ShowLabelAsValue = True
                                                '' Enable SmartLabels.   
                                                chart1(i).Series(strcol).SmartLabels.Enabled = True
                                                ' Set the callout style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutStyle = LabelCalloutStyle.Box

                                                ' Set the callout line color.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineColor = Color.Red

                                                ' Set the callout line style.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineStyle = ChartDashStyle.Solid

                                                ' Set the callout line width.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineWidth = 1

                                                ' Set the callout line anchor cap.
                                                chart1(i).Series(strcol).SmartLabels.CalloutLineAnchorCap = LineAnchorCap.Arrow
                                                ' Set the controlling moving directions.
                                                chart1(i).Series(strcol).SmartLabels.MovingDirection = LabelAlignment.BottomLeft Or LabelAlignment.BottomRight Or LabelAlignment.Center Or LabelAlignment.Left Or LabelAlignment.Right Or LabelAlignment.Top Or LabelAlignment.TopLeft Or LabelAlignment.TopRight Or LabelAlignment.Bottom

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MinMovingDistance = 100

                                                '' Set the minimum distance that the labels can move.
                                                chart1(i).Series(strcol).SmartLabels.MaxMovingDistance = 100

                                                '' Allow the labels to be placed outside the plotting area.
                                                chart1(i).Series(strcol).SmartLabels.AllowOutsidePlotArea = LabelOutsidePlotAreaStyle.Yes

                                                chart1(i).Series(strcol).LabelToolTip = "label=#LABEL" & "Axislabel=#AXISLABEL"
                                                chart1(i).Series(strcol).LegendToolTip = "Legend" + strcol
                                                chart1(i).Series(strcol).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"
                                                If ddlchart.SelectedItem.Text = "125%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("15"))
                                                ElseIf ddlchart.SelectedItem.Text = "150%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("19"))
                                                ElseIf ddlchart.SelectedItem.Text = "175%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("20"))
                                                ElseIf ddlchart.SelectedItem.Text = "200%" Then
                                                    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("21"))
                                                    'ElseIf ddlchart.SelectedItem.Text = "1000%" Then
                                                    '    chart1(i).Series(strcol).Font = New Font("Calibri", Single.Parse("10")) '(ddlFont1.SelectedItem.Text, Single.Parse("23")) ', fontstylecheckbox)
                                                End If
                                            Next row1
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            MakeParetoChart(chart1(i), strcol, "Pareto")
                            chart1(i).Series("Pareto").Type = SeriesChartType.Line
                            chart1(i).Series("Pareto").ShowLabelAsValue = True
                            chart1(i).Series("Pareto").MarkerColor = Color.Red
                            chart1(i).Series("Pareto").MarkerBorderColor = Color.MidnightBlue
                            chart1(i).Series("Pareto").MarkerStyle = MarkerStyle.Circle
                            chart1(i).Series("Pareto").MarkerSize = 8
                            chart1(i).Series("Pareto").LabelFormat = "0.#"
                            ' format with one decimal and leading zero 
                            ' Set Color of line Pareto chart 
                            chart1(i).Series("Pareto").Color = Color.Aquamarine
                            chart1(i).Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                chart1(i).RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                chart1(i).RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                    ElseIf chartname = "Histogram" Then
                        Chart2.Title = hidTitle
                       
                        Chart2.ChartAreas("Default").Position.Y = 10
                        Chart2.ChartAreas("Default").Position.X = 5
                        Chart2.ChartAreas("Default").Position.Height = 7
                        Chart2.ChartAreas("HistogramArea").Position.X = 5 'Single.Parse(txtX1.Text)
                        Chart2.ChartAreas("HistogramArea").Position.Y = Chart2.ChartAreas("Default").Position.Height + 10 'Single.Parse(txtY1.Text)
                        Chart2.ChartAreas("HistogramArea").AxisX.Title = chart2xaxistiele
                        ' chart1.ChartAreas("HistogramArea1").AxisY.Title = 
                        If ddlchart.SelectedItem.Text = "100%" Then

                            Chart2.Width = 1000
                            Chart2.Height = 500
                        ElseIf ddlchart.SelectedItem.Text = "125%" Then
                            Chart2.Width = 1250
                            Chart2.Height = 625
                        ElseIf ddlchart.SelectedItem.Text = "150%" Then
                            Chart2.Width = 1500
                            Chart2.Height = 750
                        ElseIf ddlchart.SelectedItem.Text = "175%" Then
                            Chart2.Width = 1750
                            Chart2.Height = 875
                        ElseIf ddlchart.SelectedItem.Text = "200%" Then
                            Chart2.Width = 2000
                            Chart2.Height = 1000
                        End If

                        If chartseries = "Column" Then
                            Chart2.Visible = True
                            Chart2.Width = 1000
                            Chart2.Height = 500
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
                                        If strcol = duplicate Then
                                            strcol = strcol + count.ToString
                                            count = count + 1
                                        End If
                                        Chart2.Series.Add(strcol)


                                        'Gopal Changes 

                                        Chart2.Series(strcol).MapAreaAttributes = "alt='Series" & strcol & "'"

                                        'End Gopal Changes

                                        Chart2.Series(strcol).Type = SeriesChartType.Column
                                        Chart2.Series(strcol).BorderWidth = 2
                                        Dim st As String
                                        st = strcol.Replace(count - 1, "")
                                        duplicate = st
                                        Dim xval As String
                                        Dim row1 As DataRow
                                        Dim YVal1 As Integer
                                        xval = ""
                                        Try
                                            For Each row1 In myDataSet.Tables(0).Rows
                                                ' for each row (row 1 and onward), add the value as a point
                                                columnseries = row1(0).ToString()
                                                Try
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        YVal1 = CInt(row1(colname))
                                                    End If
                                                    'smitha
                                                    'chart1(i).Series(strcol).Points.AddXY(columnseries, YVal1)
                                                    Chart2.Series(strcol).Points.AddY(YVal1)

                                                Catch ex As Exception
                                                    If IsDBNull(row1(colname)) Then
                                                    Else
                                                        xval = row1(colname)
                                                    End If
                                                End Try
                                            Next row1
                                        Catch ex As Exception
                                        End Try
                                    End If
                                Catch ex As Exception
                                End Try
                            Next columncount
                            ' Populate single axis data distribution series. Show Y value of the 
                            ' data series as X value and set all Y values to 1. 
                            For Each dataPoint As DataPoint In Chart2.Series(strcol).Points
                                'Chart2.Series.Add(DataDistribution)
                                Chart2.Series("DataDistribution").Points.AddXY(dataPoint.YValues(0), 1)
                            Next
                            ' Create a histogram series 
                            Dim histogramHelper As New HistogramChartHelper()
                            histogramHelper.CreateHistogram(Chart2, strcol, "Histogram")

                            ' Set same X axis scale and interval in the single axis data distribution 
                            ' chart area as in the histogram chart area. 
                            Chart2.ChartAreas("Default").AxisX.Minimum = Chart2.ChartAreas("HistogramArea").AxisX.Minimum
                            Chart2.ChartAreas("Default").AxisX.Maximum = Chart2.ChartAreas("HistogramArea").AxisX.Maximum
                            Chart2.ChartAreas("Default").AxisX.Interval = Chart2.ChartAreas("HistogramArea").AxisX.Interval

                            Chart2.Page = Me
                            Dim writer As HtmlTextWriter = New HtmlTextWriter(Page.Response.Output)

                            If (bool1 = True) Then

                                writer.WriteBeginTag("table><tr><td>")
                                Chart2.RenderControl(writer)
                                'writer.WriteEndTag("</td")
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = False
                            ElseIf (bool1 = False) Then
                                writer.WriteBeginTag("table><tr><td>")
                                'writer.WriteBeginTag("td>")
                                Chart2.RenderControl(writer)
                                writer.WriteEndTag("</td></tr></table")
                                bool1 = True

                            End If
                            GoTo label2
                        End If
                        End If
label1:
label2:

                Next


            Catch ex As Exception
            End Try
            If (bo = False) Then

            End If
        End If
    End Sub
    

    
    Private Sub MakeParetoChart(ByVal chart As Chart, ByVal srcSeriesName As String, ByVal destSeriesName As String)
        Dim fontstylecheckbox As New FontStyle
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
        If Page.IsPostBack = False Then
            Dim repName, anasource, viewrep As String
            repName = Request.QueryString("repname")
            viewrep = Request.QueryString("repnm")
            anasource = Request.QueryString("source")
            If repName = "" And viewrep = "" Then
                Session("Queryname") = ""
                Chart1.Visible = False
                Exit Sub
            End If

            

            If repName <> "" Then
                Chart1.Visible = True
                Chart2.Visible = False
                Chart3.Visible = False
                ddlchart.Visible = False
            End If
            If repName <> "" And anasource = "" Then
                Try
                    hidReporttype.Value = Session("reporttype")
                    chartcolumn = Session("Queryname")
                    If Session("Queryname") = "" Then
                        Close_msgbox("Please Process The Report.")
                        Chart1.Visible = False
                        Exit Sub
                    End If
                    Dim sp As String
                    Dim sp1 As String()
                    Dim nm As Integer = chartcolumn.LastIndexOf("#")
                    Dim nm1 As Integer = chartcolumn.Length
                    Dim nm2 As Integer = nm1 - nm
                    finalquery = chartcolumn.Substring(nm + 1, nm2 - 1)
                    finalquery = "#" + finalquery
                    finalquery = chartcolumn.Replace(finalquery, "")
                    finalquery = LCase(finalquery)
                    sp = finalquery.Remove(0, 7)
                    sp1 = sp.Split(",")
                    If sp1.Length = 1 Then
                        Close_msgbox("Please Select At Least Two Column.")
                        Exit Sub
                    End If
                    If finalquery.Contains("order by ") Then
                        finalquery = finalquery
                    Else
                        finalquery = finalquery + " " + "order by " + sp1(0)
                    End If
                    If finalquery.Contains("@Date1@") Then
                        Close_msgbox("Please Supply The Start Date")
                        Chart1.Visible = False
                        Exit Sub

                    ElseIf finalquery.Contains("@Date2@") Then
                        Close_msgbox("Please Supply The End Date")
                        Chart1.Visible = False

                        Exit Sub
                    End If

                    Dim myCommand As New SqlCommand(finalquery, conn)
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

                   


                    Dim arr(myDataSet.Tables("Query").Rows.Count - 1) As String
                    If repName <> "" Then
                        'Select Row  series data

                        btngraph.Visible = True

                        xval = ""
                        count = 1
                        duplicate = ""
                        Dim i As Integer
                        Dim dval As Date
                        Dim inte As Integer
                        inte = myDataSet.Tables("Query").Rows.Count
                        If inte = 0 Then
                            Close_msgbox("You Have Selected Empty Report.")
                            Exit Sub
                        End If
                        For Each row In myDataSet.Tables("Query").Rows
                            For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                rowname = myDataSet.Tables("Query").Columns(i).ColumnName
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
                                            arr(i) = sval
                                        End If

                                    Catch ex1 As Exception
                                        If rval <> 0 Then
                                            For t = 0 To arr.Length - 1
                                                arr(t) = ""
                                            Next
                                            'aspnet_msgbox("This Is Not Meaningfull Data")
                                            ' GoTo label1
                                        End If
                                        If IsDBNull(row(rowname)) Then
                                        Else
                                            sval = row(rowname)
                                            arr(i) = sval
                                        End If

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
                                        str = str + " " + " " + arr(jk)
                                    End If
                                Else
                                    Exit For
                                End If
                            Next
                            Dim YVal As Double

                            Dim series0 As String
                            series0 = str
                            If str = duplicate Then
                                str = str + count.ToString
                                count = count + 1
                            End If

                            Dim series As Series = Chart1.Series.Add(str)

                            Chart1.Series(str).Type = SeriesChartType.Column
                            Chart1.Series(str).BorderWidth = 2
                            Dim st As String
                            st = str.Replace(count - 1, "")
                            st = series0
                            duplicate = st
                            Dim colIndex As Integer
                            Dim columnName As String

                            Dim bb As String = ""
                            Try
                                For colIndex = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                                    ' for each column (column 1 and onward), add the value as a point
                                    columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                                    Try
                                        YVal = CDbl(row(columnName))
                                        Chart1.Series(str).Points.AddXY(columnName, YVal)
                                    Catch ex As Exception
                                        Try
                                            dateval = CDate(row(columnName))
                                            If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                                Chart1.Series(str).Points.AddXY(columnName, dateval)
                                            End If

                                        Catch ex1 As Exception

                                        End Try
                                        bb = row(columnName)
                                    End Try
                                    Chart1.Series(str).ShowLabelAsValue = True
                                    Chart1.Series(str).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"

                                Next colIndex
                                rval = blankravl
                            Catch ex As Exception
                            End Try
                            YVal = 0.0
                            dateval = "#12:00:00 AM#"
                            fb = False
                        Next row
                        Exit Sub
                    End If
label3:
                Catch ex As Exception

                End Try
            End If


        End If

    End Sub


    Protected Sub ddlchart_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlchart.SelectedIndexChanged
        '  Repgraph()

    End Sub
    Public Sub Close_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("  window.close();" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub

    Protected Sub btngraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btngraph.Click
        btngraph.Enabled = False
        Chart3.Series.Clear()
        repName = Request.QueryString("repnm")
        If repName = "" Then
            Chart1.Visible = False
            Chart3.Visible = True
            ChartToolbar1.Visible = True
            Chart2.Visible = False
        End If
        Me.ChartToolbar1.Chart = Me.Chart3
        hidReporttype.Value = Session("reporttype")
        Dim chartname, chcolumn As String
        Dim chartseries As String
        chartcolumn = Session("Queryname")
        Dim propertiesCommand As Command = Chart3.UI.Commands.FindCommand(ChartCommandType.SelectChartBar)
        propertiesCommand.Enabled = False
        Dim propertiesCommand1 As Command = Chart3.UI.Commands.FindCommand(ChartCommandType.SelectChartArea)
        propertiesCommand1.Enabled = False
        Dim propertiesCommand2 As Command = Chart3.UI.Commands.FindCommand(ChartCommandType.SelectChartFunnel)
        propertiesCommand2.Enabled = False
        If Session("Queryname") = "" Then
            aspnet_msgbox("Please Process The Report.")
            Chart3.Visible = False
            Exit Sub
        End If
        Dim sp As String
        Dim sp1 As String()
        Dim str3 As String
        Dim chkcolname
        'chartcolarray = chartcolumn
        Dim nm As Integer = chartcolumn.LastIndexOf("#")
        Dim nm1 As Integer = chartcolumn.Length
        Dim nm2 As Integer = nm1 - nm
        finalquery = chartcolumn.Substring(nm + 1, nm2 - 1)
        finalquery = "#" + finalquery
        finalquery = chartcolumn.Replace(finalquery, "")
        sp = finalquery.Remove(0, 7)
        sp1 = sp.Split(",")
        If sp1.Length = 1 Then
            aspnet_msgbox("Please Select At Least Two Column.")
            Exit Sub
        End If
        If finalquery.Contains("Order By ") Then
            finalquery = finalquery
        Else
            finalquery = finalquery + " " + "order by " + sp1(0)
        End If


        Dim myCommand As New SqlCommand(finalquery, conn)
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

        ''smithachangesstart
        'Dim myDataSetcount As Integer = myDataSet.Tables(0).Columns.Count
        'Dim myDataSetcountstart As Integer = 0
        'Dim datasetcolumntype As String = ""
        'Dim datasetcolumnname As String = ""
        'Dim positionasstring As String = ""
        'Dim positionasint As String = ""
        'Dim var As Integer
        'Dim newdataset As String = ""
        'Dim ramstr As String = ""
        'For myDataSetcountstart = 0 To myDataSetcount - 1
        '    datasetcolumntype = myDataSet.Tables("Query").Columns(myDataSetcountstart).DataType.ToString
        '    datasetcolumnname = myDataSet.Tables("Query").Columns(myDataSetcountstart).ColumnName
        '    ramstr = ""
        '    If LCase(datasetcolumntype).Contains("string") Or LCase(datasetcolumntype).Contains("datetime") Then
        '        Try


        '            var = CInt(myDataSet.Tables("Query").Rows(0)(datasetcolumnname))
        '        Catch ex As Exception
        '            ramstr = "no"

        '            If positionasstring = "" Then
        '                positionasstring = datasetcolumnname
        '            Else
        '                positionasstring = positionasstring + "," + datasetcolumnname
        '            End If



        '        End Try
        '        If ramstr = "" Then
        '            If positionasint = "" Then
        '                positionasint = datasetcolumnname
        '            Else
        '                positionasint = positionasint + "," + datasetcolumnname
        '            End If
        '        End If

        '    Else
        '        If positionasint = "" Then
        '            positionasint = datasetcolumnname
        '        Else
        '            positionasint = positionasint + "," + datasetcolumnname
        '        End If


        '    End If

        'Next
        'newdataset = positionasstring + "," + positionasint
        'Dim datasetarray As String() = newdataset.Split(",")
        'Dim datasetnew As New DataSet
        'Dim newdatatable As New DataTable

        'Dim newdatacolumn As New DataColumn
        'Dim newdatarow As DataRow
        'Dim dtarow As DataRow
        'For myDataSetcountstart = 0 To UBound(datasetarray)
        '    newdatacolumn = New DataColumn(datasetarray(myDataSetcountstart))

        '    newdatatable.Columns.Add(newdatacolumn)
        'Next

        'For Each dtarow In myDataSet.Tables("Query").Rows
        '    newdatarow = newdatatable.NewRow

        '    For myDataSetcountstart = 0 To UBound(datasetarray)

        '        newdatarow.Item(datasetarray(myDataSetcountstart)) = dtarow(datasetarray(myDataSetcountstart))

        '    Next
        '    newdatatable.Rows.Add(newdatarow)

        'Next
        'myDataSet.Clear()
        'myDataSet.Dispose()
        'myDataSet.Tables.Remove("Query")
        ''myDataSet.Tables.Add("Query", newdatatable.Namespace)
        'myDataSet.Tables.Add(newdatatable)


        ''stop

        Dim arr(myDataSet.Tables(0).Rows.Count - 1) As String
        If repName = "" Then
            'Select Row  series data
            xval = ""
            count = 1
            duplicate = ""
            Dim i As Integer
            Dim dval As Date
            For Each row In myDataSet.Tables("Query").Rows
                For i = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                    rowname = myDataSet.Tables("Query").Columns(i).ColumnName
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
                                arr(i) = sval
                            End If

                        Catch ex1 As Exception
                            If rval <> 0 Then
                                For t = 0 To arr.Length - 1
                                    arr(t) = ""
                                Next
                                'aspnet_msgbox("This Is Not Meaningfull Data")
                                ' GoTo label1
                            End If
                            If IsDBNull(row(rowname)) Then
                            Else
                                sval = row(rowname)
                                arr(i) = sval
                            End If

                        End Try
                        If sval = "" Then
                            arr(i) = dval
                        End If

                    End Try
                Next i
                Dim jk As Integer
                str3 = ""
                For jk = 0 To arr.Length - 1
                    If (arr(jk) <> "") Then
                        If (str3 = "") Then
                            str3 = arr(jk)
                        Else
                            str3 = str3 + " " + " " + arr(jk)
                        End If
                    Else
                        Exit For
                    End If
                Next
                Dim YVal As Double

                Dim series0 As String
                series0 = str3
                If str3 = duplicate Then
                    str3 = str3 + count.ToString
                    count = count + 1
                End If
                Dim series As Series = Chart3.Series.Add(str3)
                Chart3.Series(str3).Type = SeriesChartType.Column
                Chart3.Series(str3).BorderWidth = 2
                Dim st As String
                st = str3.Replace(count - 1, "")
                st = series0
                duplicate = st
                Dim colIndex As Integer
                Dim columnName As String

                Dim bb As String = ""
                Try
                    For colIndex = 0 To (myDataSet.Tables("Query").Columns.Count) - 1
                        ' for each column (column 1 and onward), add the value as a point
                        columnName = myDataSet.Tables("Query").Columns(colIndex).ColumnName
                        Try
                            If IsDBNull(row(columnName)) Then
                            Else
                                YVal = CDbl(row(columnName))
                            End If

                            Chart3.Series(str3).Points.AddXY(columnName, YVal)
                        Catch ex As Exception
                            Try
                                If IsDBNull(row(columnName)) Then
                                Else
                                    dateval = CDate(row(columnName))
                                End If

                                If YVal <> 0 And dateval <> "#12:00:00 AM#" Then
                                    Chart3.Series(str3).Points.AddXY(columnName, dateval)
                                End If

                            Catch ex1 As Exception

                            End Try
                            If IsDBNull(row(columnName)) Then
                            Else
                                bb = row(columnName)
                            End If

                        End Try
                        Chart3.Series(str3).ShowLabelAsValue = True
                        Chart3.Series(str3).ToolTip = "value= #VALY" & " " & "Series=#SER" & "Point=#VALX"

                    Next colIndex
                    rval = blankravl
                Catch ex As Exception
                End Try
                YVal = 0.0
                dateval = "#12:00:00 AM#"
                fb = False
            Next row
            Exit Sub
        End If
label3:
    End Sub
End Class

