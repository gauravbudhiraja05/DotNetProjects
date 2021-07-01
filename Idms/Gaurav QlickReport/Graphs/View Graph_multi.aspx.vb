Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.StreamReader
Imports System.IO.StreamWriter
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.DataVisualization.Charting.SeriesChartType
Imports System.Web.UI.DataVisualization.Charting
Partial Class Graphs_View_Graph_multi
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
    Dim count As Integer
    Dim c1, c2, c3 As String
    Dim dt As DataTable
    Dim gptype As String

    Dim cols As String()
    Dim producttype As String
    Dim connection As New SqlConnection(con1)
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim i As Integer
        'Dim allchart As Chart() = {Me.Chart1, Me.Chart2, Me.Chart3, Me.Chart4, Me.Chart5}
        Dim ss1 As String
        Dim cmd11, cmd12 As SqlCommand
        Session("repname") = "histogramreport"
        Dim queryname As String = Session("repname")
        'If (queryname = "") Then
        '    aspnet_msgbox("Please Make Atleast One Graph ")
        '    back.Visible = True
        '    Exit Sub
        'End If
        If (queryname.Contains("table_")) Then
            ss1 = queryname.Substring(queryname.IndexOf("_") + 1)
            con.Open()
            cmd11 = New SqlCommand("Select * from " + ss1 + "", con)
        Else
            cmd12 = New SqlCommand("Select TableName  from  IDMSReportMaster where QueryName='" + queryname + "'", con)
            con.Open()
            Dim ss As String = cmd12.ExecuteScalar().ToString()
            con.Close()
            con.Open()
            cmd11 = New SqlCommand("Select * from  " + ss + "", con)
        End If
        cmd = New SqlCommand("select GraphType,ColumnName from IdmsGraphMaster where QueryName='" + queryname + "' ", con)
        da = New SqlDataAdapter(cmd)
        dt = New DataTable()
        'ds = New DataSet()
        count = dt.Rows.Count()
        Dim Chart1(count) As Chart
        da.Fill(dt)
        'da.Fill(ds)
        For i = 0 To count - 1
            gptype = dt.Rows(i)("GraphType").ToString()
            s1 = dt.Rows(i)("ColumnName").ToString()
            colarray = s1.Split(",")
            da.Dispose()
            Chart1(i) = New Chart()
            Dim chartarea1 As ChartArea
            Dim chartarea2 As ChartArea
            Dim Series1 As Series()
            Dim Series2 As Series()
            Chart1(i).Series.Add(New Series())
            Chart1(i).Series.Add(New Series())
            Chart1(i).Series(0).Color = Drawing.Color.Blue
            Chart1(i).Series(0).Color = Drawing.Color.DarkGreen
            Chart1(i).Series(0).IsValueShownAsLabel = True
            Chart1(i).Series(0).IsValueShownAsLabel = False
            Chart1(i).ChartAreas.Add(New ChartArea())
            Chart1(i).ChartAreas.Add(New ChartArea())
            Chart1(i).ImageType = ChartImageType.Jpeg
            Chart1(i).Width = New System.Web.UI.WebControls.Unit(500, UnitType.Pixel)
            Chart1(i).Height = New System.Web.UI.WebControls.Unit(500, UnitType.Pixel)
            If (gptype.Equals("Point")) Then
                Chart1(i).Series(0).ChartType = 0
            End If
            If (gptype.Equals("FastPoint")) Then
                Chart1(i).Series(0).ChartType = 1
            End If
            If (gptype.Equals("Bubble")) Then
                Chart1(i).Series(0).ChartType = 2
            End If
            If (gptype.Equals("Line")) Then
                Chart1(i).Series(0).ChartType = 3
            End If
            If (gptype.Equals("Spline")) Then
                Chart1(i).Series(0).ChartType = 4
            End If
            If (gptype.Equals("StepLine")) Then
                Chart1(i).Series(0).ChartType = 5
            End If
            If (gptype.Equals("FastLine")) Then
                Chart1(i).Series(0).ChartType = 6
            End If
            If (gptype.Equals("Column")) Then
                Chart1(i).Series(0).ChartType = 10
            End If
            If (gptype.Equals("StackedColumn")) Then
                Chart1(i).Series(0).ChartType = 11
            End If
            If (gptype.Equals("StackedColumn100")) Then
                Chart1(i).Series(0).ChartType = 12
            End If
            If (gptype.Equals("Area")) Then
                Chart1(i).Series(0).ChartType = 13
            End If
            If (gptype.Equals("SplineArea")) Then
                Chart1(i).Series(0).ChartType = 14
            End If
            If (gptype.Equals("StackedArea")) Then
                Chart1(i).Series(0).ChartType = 15
            End If
            If (gptype.Equals("StackedArea100")) Then
                Chart1(i).Series(0).ChartType = 16
            End If
            If (gptype.Equals("Stock")) Then
                Chart1(i).Series(0).ChartType = 19
            End If
            If (gptype.Equals("Candlestick")) Then
                Chart1(i).Series(0).ChartType = 20
            End If
            If (gptype.Equals("Range")) Then
                Chart1(i).Series(0).ChartType = 21
            End If
            If (gptype.Equals("SplineRange")) Then
                Chart1(i).Series(0).ChartType = 22
            End If
            If (gptype.Equals("ErrorBar")) Then
                Chart1(i).Series(0).ChartType = 27
            End If
            If (gptype.Equals("BoxPlot")) Then
                Chart1(i).Series(0).ChartType = 28
            End If
            If (gptype.Equals("Pie")) Then
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Bar")) Then
                Chart1(i).Series(0).ChartType = 7
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Pyramid")) Then
                Chart1(i).Series(0).ChartType = 34
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Funnel")) Then
                Chart1(i).Series(0).ChartType = 33
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Doughnut")) Then
                Chart1(i).Series(0).ChartType = 18
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Radar")) Then
                Chart1(i).Series(0).ChartType = 25
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (gptype.Equals("Polar")) Then
                Chart1(i).Series(0).ChartType = 26
                Chart1(i).Series(0).ChartType = 17
                If (colarray.Length = 3) Then
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    c3 = colarray(2).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                    Chart1(i).Series(1).YValueMembers = c3.ToString()
                Else
                    c1 = colarray(0).ToString()
                    c2 = colarray(1).ToString()
                    Chart1(i).Series(0).XValueMember = c1.ToString()
                    Chart1(i).Series(0).YValueMembers = c2.ToString()
                End If
                da.Dispose()
                da = New SqlDataAdapter(cmd11)
                ds = New DataSet()
                da.Fill(ds)
                Chart1(i).DataSource = ds
                Chart1(i).DataBind()
                Exit Sub
            End If
            If (colarray.Length = 3) Then
                c1 = colarray(0).ToString()
                c2 = colarray(1).ToString()
                c3 = colarray(2).ToString()
                Chart1(i).Series(0).XValueMember = c1.ToString()
                Chart1(i).Series(0).YValueMembers = c2.ToString()
                Chart1(i).Series(1).YValueMembers = c3.ToString()
            Else
                c1 = colarray(0).ToString()
                c2 = colarray(1).ToString()
                Chart1(i).Series(0).XValueMember = c1.ToString()
                Chart1(i).Series(0).YValueMembers = c2.ToString()
            End If
            da.Dispose()
            da = New SqlDataAdapter(cmd11)
            ds = New DataSet()
            da.Fill(ds)
            Chart1(i).DataSource = ds
            Chart1(i).DataBind()
            Chart1(i).Width = 1000
            Chart1(i).Height = 1000

        Next
        'cmd = New SqlCommand("select GraphType,ColumnName from IdmsGraphMaster  where QueryName='" + queryname + "' ", con)
        'da = New SqlDataAdapter(cmd)
        'dt = New DataTable()
        'da.Fill(dt)
        'count = dt.Rows.Count
        'For i = 0 To count - 1
        'gptype = dt.Rows(0)("GraphType").ToString()
        's1 = dt.Rows(0)("ColumnName").ToString()
        'colarray = s1.Split(",")
        'da.Dispose()
        'If (gptype.Equals("Point")) Then
        '    Chart1.Series(0).ChartType = 0
        'End If
        'If (gptype.Equals("FastPoint")) Then
        '    Chart1.Series(0).ChartType = 1
        'End If
        'If (gptype.Equals("Bubble")) Then
        '    Chart1.Series(0).ChartType = 2
        'End If
        'If (gptype.Equals("Line")) Then
        '    Chart1.Series(0).ChartType = 3
        'End If
        'If (gptype.Equals("Spline")) Then
        '    Chart1.Series(0).ChartType = 4
        'End If
        'If (gptype.Equals("StepLine")) Then
        '    Chart1.Series(0).ChartType = 5
        'End If
        'If (gptype.Equals("FastLine")) Then
        '    Chart1.Series(0).ChartType = 6
        'End If
        'If (gptype.Equals("Column")) Then
        '    Chart1.Series(0).ChartType = 10
        'End If
        'If (gptype.Equals("StackedColumn")) Then
        '    Chart1.Series(0).ChartType = 11
        'End If
        'If (gptype.Equals("StackedColumn100")) Then
        '    Chart1.Series(0).ChartType = 12
        'End If
        'If (gptype.Equals("Area")) Then
        '    Chart1.Series(0).ChartType = 13
        'End If
        'If (gptype.Equals("SplineArea")) Then
        '    Chart1.Series(0).ChartType = 14
        'End If
        'If (gptype.Equals("StackedArea")) Then
        '    Chart1.Series(0).ChartType = 15
        'End If
        'If (gptype.Equals("StackedArea100")) Then
        '    Chart1.Series(0).ChartType = 16
        'End If
        'If (gptype.Equals("Stock")) Then
        '    Chart1.Series(0).ChartType = 19
        'End If
        'If (gptype.Equals("Candlestick")) Then
        '    Chart1.Series(0).ChartType = 20
        'End If
        'If (gptype.Equals("Range")) Then
        '    Chart1.Series(0).ChartType = 21
        'End If
        'If (gptype.Equals("SplineRange")) Then
        '    Chart1.Series(0).ChartType = 22
        'End If
        'If (gptype.Equals("ErrorBar")) Then
        '    Chart1.Series(0).ChartType = 27
        'End If
        'If (gptype.Equals("BoxPlot")) Then
        '    Chart1.Series(0).ChartType = 28
        'End If
        'If (gptype.Equals("Pie")) Then
        '    Chart1.Series(0).ChartType = 17
        'End If
        'If (gptype.Equals("Bar")) Then
        '    Chart1.Series(0).ChartType = 7
        'End If
        'If (gptype.Equals("Pyramid")) Then
        '    Chart1.Series(0).ChartType = 34
        'End If
        'If (gptype.Equals("Funnel")) Then
        '    Chart1.Series(0).ChartType = 33
        'End If
        'If (gptype.Equals("Doughnut")) Then
        '    Chart1.Series(0).ChartType = 18
        'End If
        'If (gptype.Equals("Radar")) Then
        '    Chart1.Series(0).ChartType = 25
        'End If
        'If (gptype.Equals("Polar")) Then
        '    Chart1.Series(0).ChartType = 26
        'End If
        'If (colarray.Length = 3) Then
        '    c1 = colarray(0).ToString()
        '    c2 = colarray(1).ToString()
        '    c3 = colarray(2).ToString()
        '    Chart1.Series(0).XValueMember = c1.ToString()
        '    Chart1.Series(0).YValueMembers = c2.ToString()
        '    Chart1.Series(1).YValueMembers = c3.ToString()
        'Else
        '    c1 = colarray(0).ToString()
        '    c2 = colarray(1).ToString()
        '    Chart1.Series(0).XValueMember = c1.ToString()
        '    Chart1.Series(0).YValueMembers = c2.ToString()
        'End If
        'da.Dispose()
        'da = New SqlDataAdapter(cmd11)
        'ds = New DataSet()
        'da.Fill(ds)
        'Chart1.DataSource = ds
        'Chart1.DataBind()
        'Me.Controls.Add(ci)
        'Next

    End Sub
End Class
