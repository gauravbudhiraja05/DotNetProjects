Imports System
Imports System.Data
Imports System.Drawing
Imports Dundas.Charting.WebControl
Imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Collections
Imports Dundas.Charting.Utilities
Imports DundasUtilities.Charting.SixSigma
Imports System.ComponentModel
Imports System.Web
Imports System.Drawing.Imaging
Imports System.Web.UI.WebControls.WebParts
Public Class GraphicalPresentation
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As SqlDataAdapter
    Dim cmd As SqlCommand
    Public Const PAGE_NAME As String = "ThumbFromID.aspx"
    Public Const IMAGE_ID As String = "img_pk"
    Public Shared THUMBNAIL_SIZE As Integer
    Public Shared USE_SIZE_FOR_HEIGHT As Boolean
    Public Function insertGraph(ByVal GraphType, ByVal DepartmentID, ByVal ClientID, ByVal UnderLOB, ByVal QueryName, ByVal GraphName, ByVal ColumnName, ByVal ColumnSeries, ByVal ToDate, ByVal FromDate, ByVal CommanFormat1, ByVal LegendFormat, ByVal CommanFormat2, ByVal CommanFormat, ByVal SpecificProperties, ByVal CreatedOn, ByVal SavedBy, ByVal totalcolumn, ByVal ReportType) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_SaveGraph", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@GraphType", SqlDbType.VarChar, 200)
            cmd.Parameters("@GraphType").Value = GraphType
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 200)
            cmd.Parameters("@DepartmentID").Value = DepartmentID
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 200)
            cmd.Parameters("@ClientID").Value = ClientID
            cmd.Parameters.Add("@UnderLOB", SqlDbType.VarChar, 200)
            cmd.Parameters("@UnderLOB").Value = UnderLOB
            cmd.Parameters.Add("@QueryName", SqlDbType.VarChar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@GraphName", SqlDbType.VarChar, 200)
            cmd.Parameters("@GraphName").Value = GraphName
            cmd.Parameters.Add("@ColumnName", SqlDbType.VarChar, 200)
            cmd.Parameters("@ColumnName").Value = ColumnName
            cmd.Parameters.Add("@ColumnSeries", SqlDbType.VarChar, 200)
            cmd.Parameters("@ColumnSeries").Value = ColumnSeries
            cmd.Parameters.Add("@ToDate", SqlDbType.VarChar, 200)
            cmd.Parameters("@ToDate").Value = ToDate
            cmd.Parameters.Add("@FromDate", SqlDbType.VarChar, 200)
            cmd.Parameters("@FromDate").Value = FromDate
            cmd.Parameters.Add("@CommanFormat1", SqlDbType.VarChar, 7999)
            cmd.Parameters("@CommanFormat1").Value = CommanFormat1
            cmd.Parameters.Add("@Legendformat", SqlDbType.VarChar, 7999)
            cmd.Parameters("@Legendformat").Value = LegendFormat

            cmd.Parameters.Add("@CommanFormat2", SqlDbType.VarChar, 7999)
            cmd.Parameters("@CommanFormat2").Value = CommanFormat2

            cmd.Parameters.Add("@CommanFormat", SqlDbType.VarChar, 7999)
            cmd.Parameters("@CommanFormat").Value = CommanFormat
            cmd.Parameters.Add("@SpecificProperties", SqlDbType.NText)
            cmd.Parameters("@SpecificProperties").Value = SpecificProperties
            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar, 200)
            cmd.Parameters("@CreatedOn").Value = CreatedOn
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 200)
            cmd.Parameters("@SavedBy").Value = SavedBy
            cmd.Parameters.Add("@totalcolumn", SqlDbType.VarChar, 200)
            cmd.Parameters("@totalcolumn").Value = totalcolumn

            cmd.Parameters.Add("@ReportType", SqlDbType.VarChar, 200)
            cmd.Parameters("@ReportType").Value = ReportType
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "Graph Saved Successfully."
        Catch ex As Exception
            res = ex.Message
        End Try

        Return res
    End Function
    Public Function ReportGraphName(ByVal Report) As DataSet
        Dim res As String
        Dim ds As New DataSet
        res = ""
        Try
            cmd = New SqlCommand("sp_repgrphname", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@Report", SqlDbType.VarChar, 200)
            cmd.Parameters("@Report").Value = Report
            con.Open()
            Dim sda As New SqlDataAdapter
            sda.SelectCommand = cmd
            sda.Fill(ds)
            con.Close()
        Catch ex As Exception
            res = ex.Message
        End Try
        Return ds
    End Function
    Public Sub LogDeletegraph(ByVal UserID As String, ByVal DepartmentID As String, ByVal ClientID As String, ByVal UnderLOB As String, ByVal GraphName As String, ByVal Queryname As String)
        Dim res As String
        Dim ds As New DataSet
        res = ""
        Try
            cmd = New SqlCommand("sp_loggraphdesignerfordelete", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
            cmd.Parameters("@userid").Value = UserID
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 200)
            cmd.Parameters("@DepartmentID").Value = DepartmentID
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 200)
            cmd.Parameters("@ClientID").Value = ClientID
            cmd.Parameters.Add("@UnderLOB", SqlDbType.VarChar, 200)
            cmd.Parameters("@UnderLOB").Value = UnderLOB
            cmd.Parameters.Add("@GraphName", SqlDbType.VarChar, 200)
            cmd.Parameters("@GraphName").Value = GraphName
            cmd.Parameters.Add("@Reportname", SqlDbType.VarChar, 200)
            cmd.Parameters("@Reportname").Value = Queryname
            con.Open()
            cmd.ExecuteNonQuery()
            'Dim sda As New SqlDataAdapter
            'sda.SelectCommand = cmd
            'sda.Fill(ds)
            con.Close()
        Catch ex As Exception
            res = ex.Message
        End Try

    End Sub
    Function datecheck(ByVal val As String)
        Try
            Dim datecum As Date = CType(val, Date)

        Catch ex As Exception
            Dim str As String = "1"
            Return str
        End Try
        Dim str2 As String = "2"
        Return str2
        'Return "no"
    End Function
    

    Public Function trackUpdateGraph(ByVal GraphType As String, ByVal Graphname As String, ByVal userid As String, ByVal repname As String, ByVal dept As String, ByVal client As String, ByVal lob As String, ByVal Action As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_GraphDesigner_Track", con)
            cmd.CommandType = CommandType.StoredProcedure

            cmd.Parameters.Add("@GraphType", SqlDbType.VarChar, 200)
            cmd.Parameters("@GraphType").Value = GraphType
            cmd.Parameters.Add("@GraphName", SqlDbType.VarChar, 200)
            cmd.Parameters("@GraphName").Value = Graphname
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 200)
            cmd.Parameters("@ReportName").Value = repname
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 9)
            cmd.Parameters("@DepartmentID").Value = dept
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 9)
            cmd.Parameters("@ClientID").Value = client
            cmd.Parameters.Add("@UnderLOB", SqlDbType.VarChar, 9)
            cmd.Parameters("@UnderLOB").Value = lob
            cmd.Parameters.Add("@Action", SqlDbType.VarChar, 200)
            cmd.Parameters("@Action").Value = Action
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar, 50)
            cmd.Parameters("@CreatedOn").Value = System.DateTime.Now()

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function

    Public Sub Thumbnail()
    End Sub
End Class
