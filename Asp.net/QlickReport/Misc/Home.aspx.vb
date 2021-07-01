Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.DataTable
Imports System.Configuration.ConfigurationSettings
Imports System.Math
Partial Class Home
    Inherits System.Web.UI.Page
    Dim conn As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(conn)
    Dim da As SqlDataAdapter
    Dim dt As DataTable
    Dim cmd As SqlCommand
    Dim cmd2 As SqlCommand
    Dim dr As SqlDataReader
    Dim code As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tblquery.Visible = False
        tblreport.Visible = False

        connection.Open()
        cmd = New SqlCommand("select ProductCode from InternetProductDemo where UserID='" + Session("userid") + "'", connection)
        dr = cmd.ExecuteReader
        If (dr.Read) Then
            code = dr("ProductCode")
        End If
        connection.Close()
        dr.Close()
        cmd.Dispose()

        connection.Open()
        Dim dt2 As String
        Dim cmd2 As New SqlCommand("select Rights from ProductMaster where ProductCode='" + code + "'", connection)
        Dim rdrights As SqlDataReader
        rdrights = cmd2.ExecuteReader
        If rdrights.Read Then
            dt2 = rdrights("Rights")
        End If
        Dim arrhead() As String = dt2.Split(",")
        For i = 0 To arrhead.Length - 1
            If (arrhead(i).Equals("103")) Then
                tblquery.Visible = True
                tblreport.Visible = False
                Exit For
            Else
                tblquery.Visible = False
                tblreport.Visible = True
            End If
        Next
        cmd2.Dispose()
        rdrights.Close()

        If (Page.IsPostBack = False) Then
            da = New SqlDataAdapter("select Top(5) Queryname,SavedBy,DepartmentID,ClientID,UnderLOB,TableName from IDMSReportMaster  where Recordid<=(select MAX(Recordid) from IDMSReportMaster) and Savedby='" + Session("userid") + "'  order by recordid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lblreport.Visible = True
            Else
                gridreport.DataSource = dt
                gridreport.DataBind()
                da.Dispose()
                dt.Reset()
            End If
            connection.Close()

            connection.Open()
            da = New SqlDataAdapter("select Top(5) Tableid,tablename,CreatedBy  from WARSLOBTableMaster where Tableid<=(select MAX(Tableid) from WARSLOBTableMaster) and CreatedBy='" + Session("userid") + "'  order by tableid desc ", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lbltable.Visible = True
            Else
                gridtable.DataSource = dt
                gridtable.DataBind()
                da.Dispose()
                dt.Reset()
            End If
            connection.Close()

            connection.Open()
            da = New SqlDataAdapter("select Top(5) ViewID,Viewname,CreatedBy  from Idmsviewmaster where Viewid<=(select MAX(Viewid) from Idmsviewmaster) and CreatedBy='" + Session("userid") + "' order by viewid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lblview.Visible = True
            Else
                gridview.DataSource = dt
                gridview.DataBind()
                da.Dispose()
                dt.Reset()
            End If
            connection.Close()

            connection.Open()
            da = New SqlDataAdapter("select Top(5) Autoid,SavedFilename,SavedBy  from IDMSSavedHTMLFile where Autoid<=(select MAX(Autoid) from IdmssavedhtmlFile) and SavedBy='" + Session("userid") + "'  order by Autoid desc ", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lblhtmlreport.Visible = True
            Else
                gridhtmlreport.DataSource = dt
                gridhtmlreport.DataBind()
                da.Dispose()
                dt.Reset()
            End If
            connection.Close()

            connection.Open()
            da = New SqlDataAdapter("select Top(5) Recordid,Graphname,SavedBy  from IdmsGraphMaster where Recordid<=(select MAX(Recordid) from IDMSReportMaster ) and SavedBy='" + Session("userid") + "' order by Recordid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lblgraph.Visible = True
            Else
                gridgraph.DataSource = dt
                gridgraph.DataBind()
                da.Dispose()
                dt.Reset()
            End If
            connection.Close()

           connection.Open()
            da = New SqlDataAdapter("select Top(5) Queryname,SavedBy,convert(varchar(6),DepartmentId) as DepartmentId,convert(varchar(6),ClientId) as ClientId,lobyName from warsquerymaster where Recordid<=(select MAX(Recordid) from warsquerymaster) and SavedBy='" + Session("userid") + "'  order by Recordid desc", connection)
            dt = New DataTable()
            da.Fill(dt)
            If (dt.Rows.Count <= 0) Then
                lblquery.Visible = True
            Else
                gridquery.DataSource = dt
                gridquery.DataBind()
                'connection.Close()
                da.Dispose()
                dt.Reset()
            End If
            End If

        lblUserId.Text = Session("username")
        lblLoginTime.Text = System.DateTime.Now
        Dim strMsg As String = "Your Password Will Expire in "
        Dim strmsg1 As String = " Days"
        Dim warn = False
        Dim com As New SqlCommand("select Enddate from InternetProductDemo where UserID='" + Session("userid") + "'", connection)
        Dim rdr As SqlDataReader
        connection.Close()
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
    Protected Sub gridreport_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles gridreport.RowDataBound
        If (e.Row.RowType = DataControlRowType.DataRow) Then
            Dim lnkbtn As LinkButton = e.Row.FindControl("lnkbtn")
            Dim cmdnew As SqlCommand
            Dim val1 As String
            Dim val2 As String
            Dim val3 As String
            Dim val4 As String
            Dim Queryname As String
            Queryname = lnkbtn.Text
            cmdnew = New SqlCommand("select DepartmentID,ClientId,UnderLOB,TableName  from IDMSReportMaster where QueryName='" + Queryname + "'", connection)
            Dim dsar As DataSet = New DataSet()
            Dim daar As SqlDataAdapter = New SqlDataAdapter(cmdnew)
            daar.Fill(dsar)
            If (dsar.Tables(0).Rows.Count > 0) Then
                val1 = dsar.Tables(0).Rows(0)("DepartmentID").ToString()
                val2 = dsar.Tables(0).Rows(0)("ClientId").ToString()
                val3 = dsar.Tables(0).Rows(0)("UnderLOB").ToString()
                val4 = dsar.Tables(0).Rows(0)("TableName").ToString()
            End If
            lnkbtn.Attributes.Add("onClick", "javascript:popupwindow('" + val1 + "','" + val2 + "','" + val3 + "','" + val4 + "','" + Queryname + "')")
        End If
    End Sub
End Class
