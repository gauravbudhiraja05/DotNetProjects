Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration.ConfigurationSettings
Partial Class Misc_DisplayQuery
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim selectRep As New ReportDesigner
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim code As String
    Dim da As SqlDataAdapter
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.IsPostBack = False Then
            Dim department As String = Request("Department")
            Dim Client As String = Request("Client")
            Dim LOB As String = Request("LOB")
            Dim table As String = Request("Table")
            'department = "26"
            If Client = "" Then
                Client = "0"
            End If
            If LOB = "" Then
                LOB = "0"
            End If
            Me.divquery.InnerHtml = ""
            Dim qrystr As New System.Text.StringBuilder
            Dim strdiv As New System.Text.StringBuilder
            strdiv.Append("<table width=100%>")
            Dim userid As String = Session("userid")
            strdiv.Append("<tr bgcolor='whitesmoke'><td colspan=2><b>Query Name</b></td><td><b>Created By</b></td><td><b>Created On</b></td></tr>")
            'Dim cmdget As New SqlCommand("select *,convert(varchar,CreateDate,103) as CreateDate1 from idmsquerymaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and UnderLOB='" & LOB & "' and ',' + sharedwith + ',' like '%," & Session("username1") & ",%' ", connection)

            ''''''''''''''''''''''''''''''''''''''''''''''
            '' changed by Usha sheokand on 11/09/08 in order to display only report designer reports not the querybuilder reports
            '' and to access reports according to rights
            ' Dim cmdget As New SqlCommand("select queryName,savedby,convert(varchar,CreatedOn,103) as CreatedDate,'repqry' as type from idmsreportmaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and UnderLOB='" & LOB & "' and savedby='" & userid & "' union select queryName,savedby,convert(varchar,CreateDate,103) as CreatedDate,'qrybld' as type from warsquerymaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and lobyname='" & LOB & "' and savedby='" & userid & "' order by queryname", connection)

            '' the new code starts as:
            Dim ds As New DataSet()
            Dim dt As New DataTable()

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
            rdrights.Close()
            Dim arrhead() As String = dt2.Split(",")
            For i = 0 To arrhead.Length - 1
                If (arrhead(i).Equals("103")) Then
                    'cmd = New SqlCommand("select queryName as QueryName,savedBy as SavedBy,createDate as CreatedOn from warsquerymaster where DepartmentId='" + department + "' and ClientId='" + Client + "' and lobyName='" + LOB + "' and savedby='" + Session("userid") + "'  order by queryname", connection)
                    da = New SqlDataAdapter("select queryName as QueryName,savedBy as SavedBy,createDate as CreatedOn from warsquerymaster where DepartmentId='" + department + "' and ClientId='" + Client + "' and lobyName='" + LOB + "' and savedby='" + Session("userid") + "'  order by queryname", connection)
                    ds = New DataSet()
                    da.Fill(ds)
                    Exit For
                Else
                    If (Session("typeofuser") = "Admin") Then
                        'Dim exist As Boolean = False
                        'exist = selectRep.chkAdminSpan(Session("userid"))
                        'If exist = True Then
                        'ds = selectRep.reportForadmin(Session("userid"), department, Client, LOB)
                        ds = selectRep.reportForadminHomepage(Session("userid"), department, Client, LOB)
                        'Else
                        '    GoTo adminOutofIndex
                    ElseIf (Session("typeofuser") = "Super Admin") Then
                        ds = selectRep.reportForSA(Session("userid"), department, Client, LOB)
                    Else
adminOutofIndex:
                        'Dim scope = Trim(selectRep.chkUserscope(Session("userid")))
                        'If (scope = "Local") Then
                        '    'ds = selectRep.reportForlocal(Session("userid"), department, Client, LOB)
                        '    ds = selectRep.reportForlocalhomepage(Session("userid"), department, Client, LOB)

                        'Else
                        'ds = selectRep.reportFornonlocal(Session("userid"), department, Client, LOB)
                        ds = selectRep.reportFornonlocalhomepage(Session("userid"), department, Client, LOB)

                        'End If
                    End If
                End If
            Next
            cmd2.Dispose()
            rdrights.Close()
            connection.Close()


            dt = ds.Tables(0)
        For Each row As DataRow In dt.Rows()
            strdiv.Append("<tr>")
                strdiv.Append("<td width=35 height=17><img width=30 height=17 alt='Report Image' src='../images/rptimg.jpg' ></td><td valign=middle>" & row("QueryName").ToString & "</td>")
            strdiv.Append("<td>" & row("SavedBy").ToString & "</td>")
            Dim dte As Date = row("CreatedOn")
            dte = dte.ToShortDateString
            strdiv.Append("<td>" & dte & "</td></tr>")

        Next
        '' New code ends. Right now no query builder report will get displayed
        '' append code here to display query builder report
        '''''''''''''''''''''''''''''''''''''''''

        ''Dim cmdget As New SqlCommand("select queryName,savedby,convert(varchar,CreatedOn,103) as CreatedDate,'repqry' as type from idmsreportmaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and UnderLOB='" & LOB & "' and savedby='" & userid & "' union select queryName,savedby,convert(varchar,CreateDate,103) as CreatedDate,'qrybld' as type from warsquerymaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and lobyname='" & LOB & "' and savedby='" & userid & "' order by queryname", connection)
        ''Dim drget As SqlDataReader

        ''connection.Open()
        ''drget = cmdget.ExecuteReader
        ''While drget.Read
        ''    'qrystr.Append("\IDMS\Menu\ReportDesigner\SummarizedData.aspx?txtFormula=" & drget("txtFormula").ToString)
        ''    'qrystr.Append("&OrderbyData=" & drget("OrderBy").ToString & "&wheredata=" & drget("wheredata").ToString)
        ''    'qrystr.Append("&gruopdata=" & drget("GroupBy").ToString & "&txtHeader=" & drget("Header").ToString)
        ''    'qrystr.Append("&txtFooter=" & drget("Footer").ToString & "&column=" & drget("colName").ToString)
        ''    'qrystr.Append("&hidtablename=" & drget("tableName").ToString & "&txtheadingName=" & drget("HeadingName").ToString)
        ''    '''''''''''''''''''''''''''''''''''''''''
        ''    strdiv.Append("<tr>")
        ''    If drget("type").ToString = "repqry" Then
        ''        strdiv.Append("<td><a onclick=javascript:winopen('" & drget("QueryName").ToString & "') style=cursor:hand target=new>" & drget("QueryName").ToString & "</a></td>")
        ''    Else
        ''        strdiv.Append("<td><a onclick=javascript:winopen1('" & drget("QueryName").ToString & "') style=cursor:hand target=new>" & drget("QueryName").ToString & "</a></td>")
        ''    End If
        ''    strdiv.Append("<td>" & drget("savedby").ToString & "</td>")
        ''    strdiv.Append("<td>" & drget("CreatedDate").ToString & "</td></tr>")
        ''End While
        ''drget.Close()
        ''connection.Close()
        ''cmdget.Dispose()

        strdiv.Append("</table>")
        Me.divquery.InnerHtml = strdiv.ToString
        '********For Track Management********************
        Dim cmdCal As New SqlCommand
        cmdCal.Connection = connection
        cmdCal.CommandText = "select Username from registration where userid='" & Session("Userid") & "'"
        connection.Open()
        Dim UserName = cmdCal.ExecuteScalar
        connection.Close()
        ' To track management
        Dim cmdsaveTrack As New SqlCommand("Insert_IDMSTrackManagement", connection)
        cmdsaveTrack.CommandType = Data.CommandType.StoredProcedure
        With cmdsaveTrack.Parameters
            .AddWithValue("@UserId", Trim(Session("Userid")))
            .AddWithValue("@FormName", "Import")
            .AddWithValue("@Comment", "Import")
            .AddWithValue("@Addedon", System.DateTime.Today)
        End With
        connection.Open()
        cmdsaveTrack.ExecuteNonQuery()
        connection.Close()
        cmdsaveTrack.Dispose()
        '**************************************************
        End If
    End Sub
End Class

