Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.StreamReader
Imports System.IO.StreamWriter
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.DataVisualization.Charting.SeriesChartType

Partial Class Graphs_open_multiple
    Inherits System.Web.UI.Page
    Dim classobj As New Functions
    Dim graphobj As New GraphicalPresentation
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
    Dim colarray_repcols(), colarray_sltcols() As String
    Dim s1, s2, s3 As String
    Dim x1, y1, z1
    Dim dt As DataTable
    Dim producttype, rep_cols, slt_cols As String
    Dim connection As New SqlConnection(con1)

    Protected Sub queryname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles queryname.SelectedIndexChanged
        Dim query = queryname.SelectedValue
        Dim dept, client, lob As String
        dept = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            client = 0
        Else
            client = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            lob = 0
        Else
            lob = ddlLobName.SelectedValue
        End If
        get_graph(dept, client, lob, query)
    End Sub
    Public Function get_graph(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal queryname As String)
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        graphname.Visible = True
        ddlgraph.Visible = True
        con.Close()
        con.Open()
        cmd = New SqlCommand("Select * from IdmsGraphMaster where DepartmentID='" + deptid + "' and ClientID='" + clientid + "' and UnderLOB='" + lobid + "' and SavedBy='" + Session("userid") + "' and QueryName='" + queryname + "'", con)
        Dim s As String = cmd.ExecuteScalar().ToString()
        If (s.Equals("")) Then
            aspnet_msgbox("This Query Does Not Contain Any Graph")
            Exit Function
        End If
        dr = cmd.ExecuteReader()
        ddlgraph.DataSource = dr
        ddlgraph.DataTextField = "GraphName"
        ddlgraph.DataValueField = "Recordid"
        ddlgraph.DataBind()
        ddlgraph.Items.Insert(0, "--Select--")
        repcols.Items.Clear()
        selectedcols.Items.Clear()
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not Page.IsPostBack) Then
            select_chart.Items.Insert(0, "--Select--")
            select_chart.Items.Insert(1, "Point")
            select_chart.Items.Insert(2, "FastPoint")
            select_chart.Items.Insert(3, "Bubble")
            select_chart.Items.Insert(4, "Line")
            select_chart.Items.Insert(5, "Spline")
            select_chart.Items.Insert(6, "StepLine")
            select_chart.Items.Insert(7, "FastLine")
            'select_chart.Items.Insert(8, "Bar")
            'select_chart.Items.Insert(9, "StackedBar")
            'select_chart.Items.Insert(10, "StackedBar100")
            select_chart.Items.Insert(8, "Column")
            select_chart.Items.Insert(9, "StackedColumn")
            select_chart.Items.Insert(10, "StackedColumn100")
            select_chart.Items.Insert(11, "Area")
            select_chart.Items.Insert(12, "SplineArea")
            select_chart.Items.Insert(13, "StackedArea")
            select_chart.Items.Insert(14, "StackedArea100")
            'select_chart.Items.Insert(18, "Pie")
            'select_chart.Items.Insert(19, "Doughnut")
            select_chart.Items.Insert(15, "Stock")
            select_chart.Items.Insert(16, "Candlestick")
            select_chart.Items.Insert(17, "Range")
            select_chart.Items.Insert(18, "SplineRange")
            'select_chart.Items.Insert(24, "RangeBar")
            'select_chart.Items.Insert(25, "RangeColumn")
            'select_chart.Items.Insert(26, "Radar")
            'select_chart.Items.Insert(27, "Polar")
            select_chart.Items.Insert(19, "ErrorBar")
            select_chart.Items.Insert(20, "BoxPlot")
            select_chart.Items.Insert(21, "Pie")
            select_chart.Items.Insert(22, "Bar")
            select_chart.Items.Insert(23, "Funnel")
            select_chart.Items.Insert(24, "Pyramid")
            select_chart.Items.Insert(25, "Doughnut")
            select_chart.Items.Insert(26, "Radar")
            select_chart.Items.Insert(27, "Polar")
            'select_chart.Items.Insert(30, "Renko")
            'select_chart.Items.Insert(31, "ThreeLineBreak")
            'select_chart.Items.Insert(32, "Kagi")
            'select_chart.Items.Insert(33, "PointAndFigure")
            'select_chart.Items.Insert(34, "Funnel")
            'select_chart.Items.Insert(35, "Pyramid")
        End If
        Dim typeofuser = Session("typeofuser")
        If Page.IsPostBack = False Then
            If (typeofuser.Equals("Super Admin")) Then
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
            DepartmentName.Items.Insert(0, "--Select--")
        End If
        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            producttype = rdr("ProductType")
            If (producttype = "Multiple User") Then
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
            End If
        End If



    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If (DepartmentName.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please Select DepartmentName")
        End If
        con.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ClientName.DataSource = dr
        ClientName.DataTextField = "ClientName"
        ClientName.DataValueField = "autoid"
        ClientName.DataBind()
        ClientName.Items.Insert(0, "--Select--")
        get_query(DepartmentName.SelectedValue, 0, 0)
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
        get_query(DepartmentName.SelectedValue, ClientName.SelectedValue, 0)
    End Sub

    Protected Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        get_query(DepartmentName.SelectedValue, ClientName.SelectedValue, ddlLobName.SelectedValue)
    End Sub
    Public Function get_query(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        con.Close()
        con.Open()
        cmd = New SqlCommand("Select Distinct(QueryName) from IdmsGraphMaster where DepartmentID='" + deptid + "' and ClientID='" + clientid + "' and UnderLOB='" + lobid + "' and  SavedBy='" + Session("userid") + "' ", con)
        dr = cmd.ExecuteReader()
        queryname.DataSource = dr
        queryname.DataTextField = "QueryName"
        queryname.DataValueField = "QueryName"
        queryname.DataBind()
        queryname.Items.Insert(0, "--Select--")
    End Function

    Protected Sub ddlgraph_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlgraph.SelectedIndexChanged
        repcols.Items.Clear()
        selectedcols.Items.Clear()
        Dim s As String = ddlgraph.SelectedItem.Text
        con.Close()
        con.Open()
        cmd = New SqlCommand("Select * from IdmsGraphMaster where  SavedBy='" + Session("userid") + "' and QueryName='" + queryname.SelectedItem.Text + "' and GraphName='" + ddlgraph.SelectedItem.Text + "' ", con)
        dr = cmd.ExecuteReader()
        While (dr.Read())
            rep_cols = dr("TotalColumn").ToString()
            slt_cols = dr("ColumnName").ToString()
        End While
        colarray_repcols = rep_cols.Split(",")
        Dim colcount As Integer
        colcount = UBound(colarray_repcols)
        Dim i = 0
        For i = 1 To colarray_repcols.Length
            repcols.Items.Add(New ListItem(colarray_repcols(i - 1)))
            repcols.DataBind()
        Next
        colarray_sltcols = slt_cols.Split(",")
        Dim colcount1 As Integer
        colcount1 = UBound(colarray_sltcols)
        Dim j = 0
        For j = 1 To colarray_sltcols.Length
            selectedcols.Items.Add(New ListItem(colarray_sltcols(j - 1)))
            selectedcols.DataBind()
        Next
    End Sub

    Protected Sub plotgraph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plotgraph.Click
        chart_data1.Visible = False
        chart_data2.Visible = False
        chart_data3.Visible = False
        chart_data4.Visible = False
        chart_data5.Visible = False
        chart_data6.Visible = False
        chart_data7.Visible = False
        chart_data8.Visible = False
        con.Open()
        Dim gpname As String = ddlgraph.SelectedItem.Text
        Dim i As Integer = selectedcols.Items.Count
        Dim cmd11, cmd22, cmd33, cmd1, cmd2 As SqlCommand
        Dim name As String
        con.Close()
        con.Open()
        If (i.Equals(3)) Then
            x1 = selectedcols.Items(i - 3).Text
            y1 = selectedcols.Items(i - 2).Text
            z1 = selectedcols.Items(i - 1).Text
        ElseIf (i.Equals(2)) Then
            x1 = selectedcols.Items(i - 2).Text
            y1 = selectedcols.Items(i - 1).Text
        End If
        cmd = New SqlCommand("Select GraphType from IdmsGraphMaster where GraphName='" + gpname + "'", con)
        Dim gptype As String = cmd.ExecuteScalar().ToString()
        cmd.Dispose()
        Dim ss1 As String
        Dim s As String = queryname.SelectedItem.Text
        If (s.Contains("table_")) Then
            ss1 = s.Substring(s.IndexOf("_") + 1)
            cmd1 = New SqlCommand("Select * from " + ss1 + "", con)
        Else
            cmd2 = New SqlCommand("Select TableName  from  IDMSReportMaster where QueryName='" + s + "'", con)
            name = (cmd2.ExecuteScalar()).ToString()
            cmd1 = New SqlCommand("Select * from  " + name + "", con)
        End If
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
        Session("deptid1") = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            Session("clientid1") = 0
        Else
            Session("clientid1") = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            Session("lobid1") = 0
        Else
            Session("lobid1") = ddlLobName.SelectedValue
        End If
        Session("queryname") = queryname.SelectedItem.Text
        Session("gptype") = gptype
        plotgraph.Visible = False
        update_axis.Visible = True
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)

        If (gptype.Equals("Point")) Then
            chart_data.Series(0).ChartType = Point
        End If
        If (gptype.Equals("FastPoint")) Then
            chart_data.Series(0).ChartType = FastPoint
        End If
        If (gptype.Equals("Bubble")) Then
            chart_data.Series(0).ChartType = Bubble
        End If
        If (gptype.Equals("Line")) Then
            chart_data.Series(0).ChartType = Line
        End If
        If (gptype.Equals("Spline")) Then
            chart_data.Series(0).ChartType = Spline
        End If
        If (gptype.Equals("StepLine")) Then
            chart_data.Series(0).ChartType = StepLine
        End If
        If (gptype.Equals("FastLine")) Then
            chart_data.Series(0).ChartType = FastLine
        End If
        If (gptype.Equals("Column")) Then
            chart_data.Series(0).ChartType = Column
        End If
        If (gptype.Equals("StackedColumn")) Then
            chart_data.Series(0).ChartType = StackedColumn
        End If
        If (gptype.Equals("StackedColumn100")) Then
            chart_data.Series(0).ChartType = StackedColumn100
        End If
        If (gptype.Equals("Area")) Then
            chart_data.Series(0).ChartType = Area
        End If
        If (gptype.Equals("SplineArea")) Then
            chart_data.Series(0).ChartType = SplineArea
        End If
        If (gptype.Equals("StackedArea")) Then
            chart_data.Series(0).ChartType = StackedArea
        End If
        If (gptype.Equals("StackedArea100")) Then
            chart_data.Series(0).ChartType = StackedArea100
        End If
        If (gptype.Equals("Stock")) Then
            chart_data.Series(0).ChartType = Stock
        End If
        If (gptype.Equals("Candlestick")) Then
            chart_data.Series(0).ChartType = Candlestick
        End If
        If (gptype.Equals("Range")) Then
            chart_data.Series(0).ChartType = Range
        End If
        If (gptype.Equals("SplineRange")) Then
            chart_data.Series(0).ChartType = SplineRange
        End If
        If (gptype.Equals("ErrorBar")) Then
            chart_data.Series(0).ChartType = ErrorBar
        End If
        If (gptype.Equals("BoxPlot")) Then
            chart_data.Series(0).ChartType = BoxPlot
        End If
        If (gptype.Equals("Pie")) Then
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
            '    sltchart.Visible = True
            '   select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Bar")) Then
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
            '  sltchart.Visible = True
            ' select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Pyramid")) Then
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
            'sltchart.Visible = True
            'select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Funnel")) Then
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
            'sltchart.Visible = True
            'select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Doughnut")) Then
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
            'sltchart.Visible = True
            'select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Radar")) Then
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
            'sltchart.Visible = True
            'select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        If (gptype.Equals("Polar")) Then
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
            'sltchart.Visible = True
            'select_chart.Visible = True
            plotgraph.Visible = False
            update_axis.Visible = True
            plt_upgrph.Visible = True
            Exit Sub
        End If
        chart_data1.Visible = True
        chart_data.Visible = True
        If (i.Equals(3)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
            chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
        ElseIf (i.Equals(2)) Then
            chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
            chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
        End If
        
        chart_data.DataSource = ds1
        chart_data.DataBind()
        'sltchart.Visible = True
        'select_chart.Visible = True
        
    End Sub

    Protected Sub update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click
        Dim CreatedOn As String = System.DateTime.Now
        Dim deptid, clientid, lobid As String
        deptid = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            clientid = 0
        Else
            clientid = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            lobid = 0
        Else
            lobid = ddlLobName.SelectedValue
        End If
        Dim gtype As String = select_chart.SelectedItem.Text
        If (gtype = "--Select--") Then
            gtype = "Column"
        Else
            gtype = select_chart.SelectedItem.Text
        End If
        Dim gpname As String = ddlgraph.SelectedItem.Text
        Dim a1, a2 As Integer
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
        For k = 0 To a2 - 1
            If (sltcol.Equals("")) Then
                sltcol = selectedcols.Items(k).Text
            Else
                sltcol = sltcol + "," + selectedcols.Items(k).Text
            End If
        Next
        cmd = New SqlCommand("Update IdmsGraphMaster set GraphType='" + gtype + "',DepartmentID='" + deptid + "',ClientID='" + clientid + "',UnderLOB='" + lobid + "',QueryName='" + queryname.SelectedItem.Text + "',ColumnName='" + sltcol + "',SavedBy='" + Session("userid") + "',Totalcolumn='" + repcol + "'where GraphName='" + ddlgraph.SelectedItem.Text + "'", con)
        con.Open()
        dr = cmd.ExecuteReader()
        aspnet_msgbox("Graph Updated Sucessfully")
    End Sub
    Protected Sub plt_upgrph_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles plt_upgrph.Click
        chart_data1.Visible = False
        chart_data2.Visible = False
        chart_data3.Visible = False
        chart_data4.Visible = False
        chart_data5.Visible = False
        chart_data6.Visible = False
        chart_data7.Visible = False
        chart_data8.Visible = False
        If (select_chart.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Chart Type")
            Exit Sub
        End If
        If (ddl_xaxis.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select X axis Member")
            Exit Sub
        End If
        If (ddl_yaxis1.SelectedItem.Text = "--Select--") Then
            aspnet_msgbox("Please Select Y axis Member")
            Exit Sub
        End If
        If (selectedcols.Items.Count.Equals(3)) Then
            If (ddl_yaxis2.SelectedItem.Text = "--Select--") Then
                aspnet_msgbox("Please Select Y axis IInd Member")
                Exit Sub
            End If
        End If
        Dim a1, a2, k As Integer
        Dim repcol As String = ""
        Dim sltcol As String = ""
        Dim gptype = select_chart.SelectedItem.Text
        Dim x1, y1, z1 As String
        Dim i = selectedcols.Items.Count
        If (i.Equals(2)) Then
            x1 = ddl_xaxis.SelectedItem.Text
            y1 = ddl_yaxis1.SelectedItem.Text
        ElseIf (i.Equals(3)) Then
            x1 = ddl_xaxis.SelectedItem.Text
            y1 = ddl_yaxis1.SelectedItem.Text
            z1 = ddl_yaxis2.SelectedItem.Text
        End If
        Dim s As String = queryname.SelectedItem.Text
        Dim ss1, name As String
        Dim cmd1 As SqlCommand
        If (s.Contains("table_")) Then
            ss1 = s.Substring(s.IndexOf("_") + 1)
            cmd1 = New SqlCommand("Select * from " + ss1 + "", con)
        Else
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + queryname.SelectedItem.Text + "'", con)
            con.Open()
            name = (cmd.ExecuteScalar()).ToString()
            cmd1 = New SqlCommand("Select * from  " + name + "", con)
        End If
        a1 = repcols.Items.Count
        a2 = selectedcols.Items.Count
        For k = 0 To a1 - 1
            If (repcol.Equals("")) Then
                repcol = repcols.Items(k).Text
            Else
                repcol = repcol + "," + repcols.Items(k).Text
            End If

        Next

        Session("repcols1") = repcol
        For k = 0 To a2 - 1
            If (sltcol.Equals("")) Then
                sltcol = selectedcols.Items(k).Text
            Else
                sltcol = sltcol + "," + selectedcols.Items(k).Text
            End If

        Next
        Session("selectcols1") = sltcol
        Session("deptid1") = DepartmentName.SelectedValue
        If (ClientName.SelectedValue = "--Select--") Then
            Session("clientid1") = 0
        Else
            Session("clientid1") = ClientName.SelectedValue
        End If
        If (ddlLobName.SelectedValue = "" Or ddlLobName.SelectedValue = "--Select--") Then
            Session("lobid1") = 0
        Else
            Session("lobid1") = ddlLobName.SelectedValue
        End If
        Session("queryname") = queryname.SelectedItem.Text
        Session("gptype") = gptype
        Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
        Dim ds1 As New DataSet()
        da1.Fill(ds1)
        If (gptype.Equals("Point")) Then
            chart_data.Series(0).ChartType = Point
        End If
        If (gptype.Equals("FastPoint")) Then
            chart_data.Series(0).ChartType = FastPoint
        End If
        If (gptype.Equals("Bubble")) Then
            chart_data.Series(0).ChartType = Bubble
        End If
        If (gptype.Equals("Line")) Then
            chart_data.Series(0).ChartType = Line
        End If
        If (gptype.Equals("Spline")) Then
            chart_data.Series(0).ChartType = Spline
        End If
        If (gptype.Equals("StepLine")) Then
            chart_data.Series(0).ChartType = StepLine
        End If
        If (gptype.Equals("FastLine")) Then
            chart_data.Series(0).ChartType = FastLine
        End If
        If (gptype.Equals("Column")) Then
            chart_data.Series(0).ChartType = Column
        End If
        If (gptype.Equals("StackedColumn")) Then
            chart_data.Series(0).ChartType = StackedColumn
        End If
        If (gptype.Equals("StackedColumn100")) Then
            chart_data.Series(0).ChartType = StackedColumn100
        End If
        If (gptype.Equals("Area")) Then
            chart_data.Series(0).ChartType = Area
        End If
        If (gptype.Equals("SplineArea")) Then
            chart_data.Series(0).ChartType = SplineArea
        End If
        If (gptype.Equals("StackedArea")) Then
            chart_data.Series(0).ChartType = StackedArea
        End If
        If (gptype.Equals("StackedArea100")) Then
            chart_data.Series(0).ChartType = StackedArea100
        End If
        If (gptype.Equals("Stock")) Then
            chart_data.Series(0).ChartType = Stock
        End If
        If (gptype.Equals("Candlestick")) Then
            chart_data.Series(0).ChartType = Candlestick
        End If
        If (gptype.Equals("Range")) Then
            chart_data.Series(0).ChartType = Range
        End If
        If (gptype.Equals("SplineRange")) Then
            chart_data.Series(0).ChartType = SplineRange
        End If
        If (gptype.Equals("ErrorBar")) Then
            chart_data.Series(0).ChartType = ErrorBar
        End If
        If (gptype.Equals("BoxPlot")) Then
            chart_data.Series(0).ChartType = BoxPlot
        End If
        If (gptype.Equals("Pie")) Then
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
            Exit Sub
        End If
        If (gptype.Equals("Bar")) Then
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
            Exit Sub
        End If
        If (gptype.Equals("Pyramid")) Then
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
        End Sub

    Protected Sub add_clmn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add_clmn.Click
        xaxis.Visible = False
        yaxis1.Visible = False
        yaxis2.Visible = False
        ddl_xaxis.Visible = False
        ddl_yaxis1.Visible = False
        ddl_yaxis2.Visible = False
        ddl_xaxis.Items.Clear()
        ddl_yaxis1.Items.Clear()
        ddl_yaxis2.Items.Clear()
        Dim t1 As String = queryname.SelectedItem.Text
        Session("table") = t1

        Dim i1 As String
        Dim stt As String
        Dim co As Boolean
        co = False
        If repcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        Else

            If selectedcols.Items.Count = 0 Then
                i1 = repcols.SelectedItem.Text
                selectedcols.Items.Add(i1)
                stt = i1
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

                    i1 = repcols.SelectedItem.Text
                    selectedcols.Items.Add(i1)
                    For j = 0 To selectedcols.Items.Count - 1
                        If stt = "" Then
                            stt = selectedcols.Items(j).Text
                        Else
                            stt = selectedcols.Items(j).Text & "," & stt
                        End If

                    Next

                End If

            End If

        End If
    End Sub

    Protected Sub remove_cols_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles remove_cols.Click
        xaxis.Visible = False
        yaxis1.Visible = False
        yaxis2.Visible = False
        ddl_xaxis.Visible = False
        ddl_yaxis1.Visible = False
        ddl_yaxis2.Visible = False
        ddl_xaxis.Items.Clear()
        ddl_yaxis1.Items.Clear()
        ddl_yaxis2.Items.Clear()
        If selectedcols.SelectedIndex = -1 Then
            aspnet_msgbox("No Column Is Selected In The List")
            Exit Sub
        End If

        Dim stt As String
        selectedcols.Items.Remove(selectedcols.SelectedValue)


        For j = 0 To selectedcols.Items.Count - 1
            If stt = "" Then
                stt = selectedcols.Items(j).Text
            Else
                stt = selectedcols.Items(j).Text & "," & stt
            End If

        Next
        If selectedcols.Items.Count = 0 Then
        Else
        End If
    End Sub

    Protected Sub update_axis_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update_axis.Click
        plt_upgrph.Visible = True
        chart_data1.Visible = False
        chart_data2.Visible = False
        chart_data3.Visible = False
        chart_data4.Visible = False
        chart_data5.Visible = False
        chart_data6.Visible = False
        chart_data7.Visible = False
        chart_data8.Visible = False
        ddl_xaxis.Items.Clear()
        ddl_yaxis1.Items.Clear()
        ddl_yaxis2.Items.Clear()
        xaxis.Visible = False
        ddl_xaxis.Visible = False
        yaxis1.Visible = False
        yaxis2.Visible = False
        ddl_yaxis1.Visible = False
        ddl_yaxis2.Visible = False
        select_chart.Visible = True
        sltchart.Visible = True
        Dim i = selectedcols.Items.Count
        If (i > 3 And i < 2) Then
            aspnet_msgbox("Minimum 2 columns and Maximum 3 Columns are Allowed To Make a Graph")
        End If
        If (i.Equals(2)) Then
            xaxis.Visible = True
            yaxis1.Visible = True
            ddl_xaxis.Visible = True
            ddl_yaxis1.Visible = True
        ElseIf (i.Equals(3)) Then
            xaxis.Visible = True
            yaxis1.Visible = True
            ddl_xaxis.Visible = True
            ddl_yaxis1.Visible = True
            yaxis2.Visible = True
            ddl_yaxis2.Visible = True
        End If
        Dim name, gpname As String
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
        Dim cmd1, cmd2 As SqlCommand
        gpname = ddlgraph.SelectedItem.Text
        cmd = New SqlCommand("Select GraphType from IdmsGraphMaster where GraphName='" + gpname + "'", con)
        Dim gptype As String = cmd.ExecuteScalar().ToString()
        cmd.Dispose()
        Dim ss1 As String
        Dim cmd11, cmd22, cmd33 As SqlCommand
        Dim c1, c2, c3 As String
        Dim s As String = queryname.SelectedItem.Text
        ddl_xaxis.Items.Insert(0, "--Select--")
        ddl_yaxis1.Items.Insert(0, "--Select--")
        ddl_yaxis2.Items.Insert(0, "--Select--")
        If (s.Contains("table_")) Then
            ss1 = s.Substring(s.IndexOf("_") + 1)
            cmd1 = New SqlCommand("Select * from " + ss1 + "", con)
            Dim y As Integer = 1
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + ss1 + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(y, s1)
                    ddl_yaxis2.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + ss1 + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(y, s2)
                    ddl_yaxis2.Items.Insert(y, s2)
                    y = y + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + ss1 + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(y, s3)
                    ddl_yaxis2.Items.Insert(y, s3)
                    y = y + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + ss1 + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(y, s1)
                    ddl_yaxis2.Items.Insert(y, s1)
                    y = y + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + ss1 + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd11.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(y, s2)
                    ddl_yaxis1.Items.Insert(y, s2)
                    y = y + 1
                End If

            End If
        Else
            con.Close()
            con.Open()
            Dim x As Integer = 1
            cmd = New SqlCommand(" Select TableName  from  IDMSReportMaster where QueryName='" + queryname.SelectedItem.Text + "'", con)
            name = (cmd.ExecuteScalar()).ToString()
            cmd1 = New SqlCommand("Select * from  " + name + "", con)
            If (i.Equals(3)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(x, s1)
                    ddl_yaxis2.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(x, s2)
                    ddl_yaxis2.Items.Insert(x, s2)
                    x = x + 1
                End If
                cmd33 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s3 + "'", con)
                c3 = (cmd33.ExecuteScalar()).ToString()
                If (c3.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(x, s3)
                    ddl_yaxis2.Items.Insert(x, s3)
                    x = x + 1
                End If
            ElseIf (i.Equals(2)) Then
                cmd11 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s1 + "'", con)
                c1 = (cmd11.ExecuteScalar()).ToString()
                If (c1.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(x, s1)
                    ddl_yaxis2.Items.Insert(x, s1)
                    x = x + 1
                End If
                cmd22 = New SqlCommand("SELECT DATA_TYPE as type FROM INFORMATION_SCHEMA.COLUMNS WHERE Table_Name ='" + name + "' and COLUMN_NAME='" + s2 + "'", con)
                c2 = (cmd22.ExecuteScalar()).ToString()
                If (c2.Equals("numeric")) Then
                    ddl_yaxis1.Items.Insert(x, s2)
                    ddl_yaxis2.Items.Insert(x, s2)
                    x = x + 1
                End If
            End If
        End If
        If (i.Equals(3)) Then
            ddl_xaxis.Items.Insert(1, s1)
            ddl_xaxis.Items.Insert(2, s2)
            ddl_xaxis.Items.Insert(3, s3)
        ElseIf (i.Equals(2)) Then
            ddl_xaxis.Items.Insert(1, s1)
            ddl_xaxis.Items.Insert(2, s2)
        End If

        x1 = ddl_xaxis.SelectedItem.Text
        y1 = ddl_yaxis1.SelectedItem.Text
        z1 = ddl_yaxis2.SelectedItem.Text
        'Dim ds1 As New DataSet()
        'Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
        'da1.Fill(ds1)
        'If (selectedcols.Items.Count.Equals(3)) Then
        '    chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
        '    chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
        '    chart_data.Series(1).YValueMembers = ds1.Tables(0).Columns(z1).ToString()
        'ElseIf (selectedcols.Items.Count.Equals(2)) Then
        '    chart_data.Series(0).XValueMember = ds1.Tables(0).Columns(x1).ToString()
        '    chart_data.Series(0).YValueMembers = ds1.Tables(0).Columns(y1).ToString()
        'End If

        'chart_data.DataSource = ds1
        'chart_data.DataBind()

    End Sub

    Protected Sub delete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles delete.Click
        If queryname.SelectedItem.Text = "" Then
            If DepartmentName.SelectedIndex = 0 Then
                aspnet_msgbox("Please Select Department.")
                Exit Sub
            ElseIf ddlgraph.SelectedIndex = 0 Then
                aspnet_msgbox("Please select GraphName.")
                Exit Sub
            End If
        Else
            If ddlgraph.SelectedIndex = 0 Then
                aspnet_msgbox("Please select GraphName.")
                Exit Sub
            End If
        End If
        cmd = New SqlCommand("Delete from idmsgraphmaster where graphname='" + ddlgraph.SelectedItem.Text + "' and queryname='" + queryname.SelectedItem.Text + "'", con)
        con.Open()
        dr = cmd.ExecuteReader()
        aspnet_msgbox("Graph Deleted Successfully")
        cmd.Dispose()
        dr.Close()
        con.Close()
        con.Open()
        cmd = New SqlCommand("Select * from IdmsGraphMaster where DepartmentID='" + DepartmentName.SelectedValue + "' and ClientID='" + ClientName.SelectedValue + "' and UnderLOB='" + ddlLobName.SelectedValue + "' and SavedBy='" + Session("userid") + "' and QueryName='" + queryname.SelectedItem.Text + "'", con)
        dr = cmd.ExecuteReader()
        ddlgraph.DataSource = dr
        ddlgraph.DataTextField = "GraphName"
        ddlgraph.DataValueField = "Recordid"
        ddlgraph.DataBind()
        ddlgraph.Items.Insert(0, "--Select--")
        repcols.Items.Clear()
        selectedcols.Items.Clear()
    End Sub

    Protected Sub Reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Reset.Click
        Response.Redirect("~\Graphs\graph.aspx")
    End Sub

    
End Class
