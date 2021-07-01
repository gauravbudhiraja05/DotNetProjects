Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class Misc_DispQuery
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    ''' <summary>
    ''' get span and display report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Me.IsPostBack = False Then
            Dim department As String = Request("Department")
            Dim Client As String = Request("Client")
            Dim LOB As String = Request("LOB")
            Dim table As String = Request("Table")
            department = "26"
            If Client = "" Then
                Client = 0
            End If
            If LOB = "" Then
                LOB = 0
            End If
            Me.divquery.InnerHtml = ""
            Dim qrystr As New System.Text.StringBuilder
            Dim strdiv As New System.Text.StringBuilder
            strdiv.Append("<table width=100%>")
            Dim userid As String = Session("userid")
            strdiv.Append("<tr><td><b>Query Name</b></td><td><b>Created By</b></td><td><b>Created On</b></td></tr>")
            'Dim cmdget As New SqlCommand("select *,convert(varchar,CreateDate,103) as CreateDate1 from idmsquerymaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and UnderLOB='" & LOB & "' and ',' + sharedwith + ',' like '%," & Session("username1") & ",%' ", connection)
            Dim cmdget As New SqlCommand("select queryName,savedby,convert(varchar,CreatedOn,103) as CreatedDate,'repqry' as type from idmsreportmaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and UnderLOB='" & LOB & "' and savedby='" & userid & "' union select queryName,savedby,convert(varchar,CreateDate,103) as CreatedDate,'qrybld' as type from warsquerymaster where DepartmentId='" & department & "' and ClientId='" & Client & "' and lobyname='" & LOB & "' and savedby='" & userid & "' order by queryname", connection)
            Dim drget As SqlDataReader

            connection.Open()
            drget = cmdget.ExecuteReader
            While drget.Read
                'qrystr.Append("\IDMS\Menu\ReportDesigner\SummarizedData.aspx?txtFormula=" & drget("txtFormula").ToString)
                'qrystr.Append("&OrderbyData=" & drget("OrderBy").ToString & "&wheredata=" & drget("wheredata").ToString)
                'qrystr.Append("&gruopdata=" & drget("GroupBy").ToString & "&txtHeader=" & drget("Header").ToString)
                'qrystr.Append("&txtFooter=" & drget("Footer").ToString & "&column=" & drget("colName").ToString)
                'qrystr.Append("&hidtablename=" & drget("tableName").ToString & "&txtheadingName=" & drget("HeadingName").ToString)
                '''''''''''''''''''''''''''''''''''''''''
                strdiv.Append("<tr>")
                If drget("type").ToString = "repqry" Then
                    strdiv.Append("<td><a onclick=javascript:winopen('" & drget("QueryName").ToString & "') style=cursor:hand target=new>" & drget("QueryName").ToString & "</a></td>")
                Else
                    strdiv.Append("<td><a onclick=javascript:winopen1('" & drget("QueryName").ToString & "') style=cursor:hand target=new>" & drget("QueryName").ToString & "</a></td>")
                End If
                strdiv.Append("<td>" & drget("savedby").ToString & "</td>")
                strdiv.Append("<td>" & drget("CreatedDate").ToString & "</td></tr>")
            End While
            drget.Close()
            connection.Close()
            cmdget.Dispose()
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
