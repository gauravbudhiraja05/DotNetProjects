Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Imports System.Web.UI.DataVisualization.Charting
Partial Class Home
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(conn)
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Page.IsPostBack = False) Then
            da = New SqlDataAdapter("select Top(5) Queryname,SavedBy,DepartmentID,ClientID,UnderLOB,TableName from IDMSReportMaster  where Recordid<(select MAX(Recordid) from IDMSReportMaster) and Savedby='" + Session("userid") + "'  order by recordid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            gridreport.DataSource = dt
            gridreport.DataBind()
            connection.Close()
            da.Dispose()
            dt.Reset()
            connection.Open()
            da = New SqlDataAdapter("select Top(5) Tableid,tablename,CreatedBy  from WARSLOBTableMaster where Tableid<(select MAX(Tableid) from WARSLOBTableMaster) and CreatedBy='" + Session("userid") + "'  order by tableid desc ", connection)
            dt = New DataTable()
            da.Fill(dt)
            gridtable.DataSource = dt
            gridtable.DataBind()
            connection.Close()
            da.Dispose()
            dt.Reset()
            connection.Open()
            da = New SqlDataAdapter("select Top(5) ViewID,Viewname,CreatedBy  from Idmsviewmaster where Viewid<(select MAX(Viewid) from Idmsviewmaster) and CreatedBy='" + Session("userid") + "' order by viewid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            gridview.DataSource = dt
            gridview.DataBind()
            connection.Close()
            da.Dispose()
            dt.Reset()
            connection.Open()
            da = New SqlDataAdapter("select Top(5) Autoid,SavedFilename,SavedBy  from IDMSSavedHTMLFile where Autoid<(select MAX(Autoid) from IdmssavedhtmlFile) and SavedBy='" + Session("userid") + "'  order by Autoid desc ", connection)
            dt = New DataTable()
            da.Fill(dt)
            gridhtmlreport.DataSource = dt
            gridhtmlreport.DataBind()
            connection.Close()
            da.Dispose()
            dt.Reset()
            connection.Open()
            'da = New SqlDataAdapter("select Top(5) Recordid,Graphname,SavedBy  from IdmsGraphMaster where Recordid<(select MAX(Recordid) from IDMSReportMaster ) and SavedBy='" + Session("userid") + "' order by Recordid desc", connection)
            'dt = New DataTable()
            'da.Fill(dt)
            'gridgraph.DataSource = dt
            'gridgraph.DataBind()
            connection.Close()
            'da.Dispose()
            'dt.Reset()
            connection.Open()
            da = New SqlDataAdapter("select Top(5) Queryname,SavedBy,DepartmentId,ClientId,lobyName from warsquerymaster where Recordid<(select MAX(Recordid) from warsquerymaster) and SavedBy='" + Session("userid") + "'  order by Recordid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            gridquery.DataSource = dt
            gridquery.DataBind()
            connection.Close()
            da.Dispose()
            dt.Reset()
        End If

        If Page.IsPostBack = False Then
            connection.Open()
            Dim cmd1 As SqlCommand = New SqlCommand("Select * from Corelation2", connection)
            Dim da1 As SqlDataAdapter = New SqlDataAdapter(cmd1)
            Dim ds1 As DataSet = New DataSet()
            da1.Fill(ds1)
            Chart1.Series(0).ChartType = SeriesChartType.Line
            Chart1.Series(0).XValueMember = ds1.Tables(0).Columns("V1").ToString()
            Chart1.Series(0).YValueMembers = ds1.Tables(0).Columns("v2").ToString()
            Chart1.DataSource = ds1
            Chart1.DataBind()
            connection.Close()
        End If
        lblUserId.Text = Session("username")
        lblLoginTime.Text = System.DateTime.Now
        Dim strMsg As String = "Your Password Will Expire in "
        Dim strmsg1 As String = " Days"
        Dim warn = False
        Dim com As New SqlCommand("select Enddate from InternetProductDemo where UserID='" + Session("userid") + "'", connection)
        Dim rdr As SqlDataReader
        connection.Open()
        rdr = com.ExecuteReader
        If rdr.Read Then
            Dim dt As Date = rdr("Enddate")
            Dim exp = Abs(DateDiff(DateInterval.Day, System.DateTime.Now, dt))
            If exp <= 10 And exp >= 0 Then
                If exp = 1 Then
                    strmsg1 = " Day"
                End If
                'WARSShowMsg(strMsg & exp.ToString & strmsg1)
                lblPasswordEx.Text += exp.ToString
                If exp = 1 Then
                    lblPasswordEx.Text += " Day"
                Else
                    lblPasswordEx.Text += " Days"
                End If
                lblPasswordEx.Visible = True
            Else
                lblPasswordEx.Visible = False
            End If
        End If
        connection.Close()
        rdr.Close()
        com.Dispose()
    End Sub
End Class
