Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Public Class ReportDesigner
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    Dim cmd As SqlCommand
    'for home page qurybuilder+reportdesigner

    Public Function reportForadminHomepage(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("Homepage_queries", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        cmd.Dispose()
        con.Close()
        Return ds
    End Function

    Public Function reportForlocalhomepage(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("homepage_forlocaluser", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        cmd.Dispose()
        con.Close()
        Return ds
    End Function

    Public Function reportFornonlocalhomepage(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("homepage_nonlocal", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    'q+r
    Public Function tableForadminwrm(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableforwrm", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function

    Public Function tableForlocalwrm(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableforwrmrights", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function


    Public Function tableFornonlocalwrm(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableFornonlocalwrm", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function

    ''' <summary>
    ''' fetch values corresponding to a formula / column from a table
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function get_Value(ByVal str As String) As String
        Dim value As String
        value = ""
        Dim sqlString = Replace(str, "$", ".")
        Try
            Dim sqlCmd As SqlCommand = New SqlCommand(sqlString, con)
            con.Open()
            value = sqlCmd.ExecuteScalar()
            con.Close()
            sqlCmd.Dispose()

        Catch ex As Exception
            value = ""
        End Try
        Return value
    End Function
    Public Function GetFormula(ByVal query As String) As String
        Dim i As Integer = 0
        Dim val As String = ""
        Dim str As String = ""
        Dim ch As String = ""
        con.Close()
        Dim query1 = Replace(query, "$", ",")

        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        Dim objcmd As New SqlCommand
        Dim sqlCmd As SqlCommand = New SqlCommand(query1, con)
        Try
            con.Open()
            val = sqlCmd.ExecuteScalar()
            con.Close()
            sqlCmd.Dispose()
        Catch ex As Exception
            val = "0"
        End Try
        

        Return val
    End Function
    Public Function GetAllFormula(ByVal query As String) As String
        Dim i As Integer = 0
        Dim val As String = ""
        Dim str As String = ""
        Dim ch As String = ""
        Dim query1 = Replace(query, "$", ",")
        Dim dreader As SqlDataReader
        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        Dim objcmd As New SqlCommand
        Dim sqlCmd As SqlCommand = New SqlCommand(query1, con)
        con.Close()
        Try
            con.Open()
            dreader = sqlCmd.ExecuteReader()
            If (dreader.Read) Then
                Dim cnt = 0
                For cnt = 0 To dreader.FieldCount - 1
                    If val = "" Then
                        val = dreader.Item(cnt)
                    Else
                        val = val + "," + dreader.Item(cnt)
                    End If
                Next
            End If
            con.Close()
            dreader.Close()
            sqlCmd.Dispose()
        Catch ex As Exception
            val = "0"
        End Try


        Return val
    End Function
    Public Function chkCondition(ByVal str As String) As String
        Dim stat As Boolean
        stat = False
        Dim rdr As SqlDataReader
        Dim sqlString = Replace(str, "$", ".")
        Dim sqlCmd As SqlCommand = New SqlCommand(sqlString, con)
        con.Close()
        Try

            con.Open()
            rdr = sqlCmd.ExecuteReader()
            Dim st As String = ""
            stat = rdr.HasRows
            rdr.Close()
            con.Close()
        Catch ex As Exception
            stat = False
        End Try
        sqlCmd.Dispose()
        Return stat
    End Function
    ''' <summary>
    ''' This function is to be used to store a new report into database.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="DepartmentID">ReportDepartment</param>
    ''' <param name="ClientID">ReportClient</param>
    ''' <param name="UnderLOB">ReportLOB</param>
    ''' <param name="TableName">TableOfReport</param>
    ''' <param name="HeaderID">ReportHeaderID</param>
    ''' <param name="FooterID">ReportFooterID</param>
    ''' <param name="ColName">DetailsPaneColumns</param>
    ''' <param name="FormulaName">formulaName</param>
    ''' <param name="TxtFormula">Formula</param>
    ''' <param name="WhereData">WhereClause</param>
    ''' <param name="GroupBy">GroupBy</param>
    ''' <param name="OrderBy">OrderBy</param>
    ''' <param name="HavingCondition">HavingClause</param>
    ''' <param name="ReportFormat">ReportFormat</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <param name="ReportType">ReportType</param>
    ''' <param name="ReportScope">ReportScope</param>
    ''' <param name="DateConTable">DateConditionTable</param>
    ''' <param name="CreatedOn">CreatedDate</param>
    ''' <param name="SavedBy">Author</param>
    ''' <remarks></remarks>
    Public Function insertReport(ByVal QueryName As String, ByVal TableName As String, ByVal HeaderID As String, ByVal FooterID As String, ByVal ColName As String, ByVal FormulaName As String, ByVal TxtFormula As String, ByVal WhereData As String, ByVal GroupBy As String, ByVal OrderBy As String, ByVal HavingCondition As String, ByVal ReportFormat As String, ByVal ColorCondition As String, ByVal ReportType As String, ByVal DateConTable As String, ByVal CreatedOn As String, ByVal SavedBy As String, ByVal columnFormat As String, ByVal level As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_SaveReport", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.VarChar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 500)
            cmd.Parameters("@TableName").Value = TableName
            cmd.Parameters.Add("@HeaderID", SqlDbType.VarChar, 200)
            cmd.Parameters("@HeaderID").Value = HeaderID
            cmd.Parameters.Add("@FooterID", SqlDbType.VarChar, 200)
            cmd.Parameters("@FooterID").Value = FooterID
            cmd.Parameters.Add("@ColName", SqlDbType.Text)
            cmd.Parameters("@ColName").Value = ColName
            cmd.Parameters.Add("@FormulaName", SqlDbType.VarChar, 200)
            cmd.Parameters("@FormulaName").Value = FormulaName
            cmd.Parameters.Add("@TxtFormula", SqlDbType.VarChar, 1000)
            cmd.Parameters("@TxtFormula").Value = TxtFormula
            cmd.Parameters.Add("@WhereData", SqlDbType.VarChar, 7999)
            cmd.Parameters("@WhereData").Value = WhereData
            cmd.Parameters.Add("@GroupBy", SqlDbType.VarChar, 500)
            cmd.Parameters("@GroupBy").Value = GroupBy
            cmd.Parameters.Add("@OrderBy", SqlDbType.VarChar, 500)
            cmd.Parameters("@OrderBy").Value = OrderBy
            cmd.Parameters.Add("@HavingCondition", SqlDbType.VarChar, 7999)
            cmd.Parameters("@HavingCondition").Value = HavingCondition
            cmd.Parameters.Add("@ReportFormat", SqlDbType.Text)
            cmd.Parameters("@ReportFormat").Value = ReportFormat
            cmd.Parameters.Add("@ColorCondition", SqlDbType.Text)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@ReportType", SqlDbType.VarChar, 50)
            cmd.Parameters("@ReportType").Value = ReportType
            cmd.Parameters.Add("@DateConTable", SqlDbType.VarChar, 50)
            cmd.Parameters("@DateConTable").Value = DateConTable
            cmd.Parameters.Add("@CreatedOn", SqlDbType.VarChar, 50)
            cmd.Parameters("@CreatedOn").Value = CreatedOn
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 200)
            cmd.Parameters("@SavedBy").Value = SavedBy
            cmd.Parameters.Add("@columnFormat", SqlDbType.Text)
            cmd.Parameters("@columnFormat").Value = columnFormat
            cmd.Parameters.Add("@level", SqlDbType.VarChar, 50)
            cmd.Parameters("@level").Value = level
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message.ToString()
            con.Close()
        End Try
        Return res
    End Function
    ''' <summary>
    ''' This function saves the header of the report.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="HeaderID">headerID</param>
    ''' <param name="HeaderHeight">HeaderHeight</param>
    ''' <param name="HeaderFormat">HeaderFormat</param>
    ''' <param name="HeaderColumns">HeaderColumns</param>
    ''' <param name="ColumnFormat">Columnformat</param>
    ''' <param name="ColumnFormula">ColumnFormula</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <param name="CreatedOn">CreatedOn</param>
    ''' <param name="CreatedBy">Author</param>
    ''' <returns>Status of record</returns>
    ''' <remarks></remarks>
    Public Function insertHeader(ByVal QueryName As String, ByVal HeaderID As String, ByVal HeaderHeight As String, ByVal HeaderFormat As String, ByVal HeaderColumns As String, ByVal ColumnFormat As String, ByVal ColumnFormula As String, ByVal ColorCondition As String, ByVal CreatedOn As String, ByVal CreatedBy As String, ByVal repid As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_SaveHeader", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.varchar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@HeaderID", SqlDbType.varchar, 200)
            cmd.Parameters("@HeaderID").Value = HeaderID
            cmd.Parameters.Add("@HeaderHeight", SqlDbType.varchar, 50)
            cmd.Parameters("@HeaderHeight").Value = HeaderHeight
            cmd.Parameters.Add("@HeaderFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@HeaderFormat").Value = HeaderFormat
            cmd.Parameters.Add("@HeaderColumns", SqlDbType.varchar, 7999)
            cmd.Parameters("@HeaderColumns").Value = HeaderColumns
            cmd.Parameters.Add("@ColumnFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormat").Value = ColumnFormat
            cmd.Parameters.Add("@ColumnFormula", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormula").Value = ColumnFormula
            cmd.Parameters.Add("@ColorCondition", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@CreatedOn", SqlDbType.varchar, 50)
            cmd.Parameters("@CreatedOn").Value = CreatedOn
            cmd.Parameters.Add("@CreatedBy", SqlDbType.varchar, 500)
            cmd.Parameters("@CreatedBy").Value = CreatedBy
            cmd.Parameters.Add("@repID", SqlDbType.varchar, 50)
            cmd.Parameters("@repID").Value = repid
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
            con.Close()
        End Try
        Return res
    End Function
    ''' <summary>
    ''' This function saves the footer of the report.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="FooterID">FooterID</param>
    ''' <param name="FooterHeight">FooterHeight</param>
    ''' <param name="FooterFormat">FooterFormat</param>
    ''' <param name="FooterColumns">FooterColumns</param>
    ''' <param name="ColumnFormat">Footerformat</param>
    ''' <param name="ColumnFormula">ColumnFormula</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <param name="CreatedOn">CreatedOn</param>
    ''' <param name="CreatedBy">Author</param>
    ''' <returns>Status of record</returns>
    ''' <remarks></remarks>
    Public Function insertFooter(ByVal QueryName As String, ByVal FooterID As String, ByVal FooterHeight As String, ByVal FooterFormat As String, ByVal FooterColumns As String, ByVal ColumnFormat As String, ByVal ColumnFormula As String, ByVal ColorCondition As String, ByVal CreatedOn As String, ByVal CreatedBy As String, ByVal repid As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_SaveFooter", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.varchar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@FooterID", SqlDbType.varchar, 200)
            cmd.Parameters("@FooterID").Value = FooterID
            cmd.Parameters.Add("@FooterHeight", SqlDbType.varchar, 50)
            cmd.Parameters("@FooterHeight").Value = FooterHeight
            cmd.Parameters.Add("@FooterFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@FooterFormat").Value = FooterFormat
            cmd.Parameters.Add("@FooterColumns", SqlDbType.varchar, 7999)
            cmd.Parameters("@FooterColumns").Value = FooterColumns
            cmd.Parameters.Add("@ColumnFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormat").Value = ColumnFormat
            cmd.Parameters.Add("@ColumnFormula", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormula").Value = ColumnFormula
            cmd.Parameters.Add("@ColorCondition", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@CreatedOn", SqlDbType.varchar, 50)
            cmd.Parameters("@CreatedOn").Value = CreatedOn
            cmd.Parameters.Add("@CreatedBy", SqlDbType.varchar, 500)
            cmd.Parameters("@CreatedBy").Value = CreatedBy
            cmd.Parameters.Add("@repID", SqlDbType.varchar, 50)
            cmd.Parameters("@repID").Value = repid
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
        End Try
        Return res
    End Function
    ''' <summary>
    ''' To check for the existing report name.
    ''' </summary>
    ''' <param name="repname">ReportName</param>
    ''' ''' <param name="deptid">DepartmentID</param>
    ''' <param name="clientid">ClientID</param>
    ''' <param name="lobid">LOBID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckExistingReport(ByVal repname As String) As String
        Dim stat As String = ""
        stat = False
        cmd = New SqlCommand("Select savedby from IDMSreportMaster where queryname='" + repname + "' and savedby<>'Deleted' and archivedstatus <>'Yes'", con)
        con.Open()
        stat = cmd.ExecuteScalar()
        con.Close()
        cmd.Dispose()
        Return stat
    End Function
    Public Function fetchReportrights(ByVal recordid As String, ByVal userid As String) As String
        Dim rdr As SqlDataReader
        cmd = New SqlCommand("sp_fetchReportrights", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@recordid", SqlDbType.varchar, 10)
        cmd.Parameters("@recordid").Value = recordid
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        Dim str = ""
        rdr = cmd.ExecuteReader()
        If (rdr.Read) Then
            str = rdr.Item(0).ToString
            str = str + "," + rdr.Item(1).ToString
            str = str + "," + rdr.Item(2).ToString()
            str = str + "," + rdr.Item(3).ToString
        End If
        con.Close()
        rdr.Close()
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' Get owner of a report
    ''' </summary>
    ''' <param name="recordid">recordIDofReport</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getOwner(ByVal recordid As String) As String
        cmd = New SqlCommand("sp_ReportOwner", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@recordid", SqlDbType.varchar, 10)
        cmd.Parameters("@recordid").Value = recordid
        con.Open()
        Dim str As String = cmd.ExecuteScalar()
        con.Close()
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' Fetch Recordid of report
    ''' </summary>
    ''' <param name="repname"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function fetchReportRecordid(ByVal repname As String) As String
        cmd = New SqlCommand("sp_reportRecordid", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@Reportname", SqlDbType.VarChar, 200)
        cmd.Parameters("@Reportname").Value = repname
        con.Open()
        Dim str As String = cmd.ExecuteScalar()
        con.Close()
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' To check for the existing HTML report name.
    ''' </summary>
    ''' <param name="repname">ReportName</param>
    ''' <param name="deptid">DepartmentID</param>
    ''' <param name="clientid">ClientID</param>
    ''' <param name="lobid">LOBID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckExistingHTMLReport(ByVal repname As String) As Boolean
        Dim stat As Boolean
        stat = False
        Dim rdr As SqlDataReader
        cmd = New SqlCommand("sp_CheckHTMLReport", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@SavedFilename", SqlDbType.VarChar, 200)
        cmd.Parameters("@SavedFilename").Value = repname
        con.Open()
        rdr = cmd.ExecuteReader()
        Dim st As String = ""
        stat = rdr.HasRows
        rdr.Close()
        con.Close()
        cmd.Dispose()
        Return stat
    End Function
    ''' <summary>
    ''' check if the admin belongs to the currently selected span
    ''' </summary>
    ''' <param name="userid"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkAdminSpan(ByVal userid As String) As String
        con.Close()
        Dim stat As Boolean
        stat = False
        Dim rdr As SqlDataReader
        cmd = New SqlCommand("sp_adminspan", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        rdr = cmd.ExecuteReader()
        Dim st As String = ""
        stat = rdr.HasRows
        rdr.Close()
        con.Close()
        cmd.Dispose()
        Return stat
    End Function
    Public Function chkUserscope(ByVal userid As String) As String
        con.Close()
        cmd = New SqlCommand("sp_chkUSerscope", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        Dim str = cmd.ExecuteScalar()
        con.Close()
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' This function is to be used to update an existing report.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="DepartmentID">ReportDepartment</param>
    ''' <param name="ClientID">ReportClient</param>
    ''' <param name="UnderLOB">ReportLOB</param>
    ''' <param name="TableName">TableOfReport</param>
    ''' <param name="ColName">DetailsPaneColumns</param>
    ''' <param name="FormulaName">formulaName</param>
    ''' <param name="TxtFormula">Formula</param>
    ''' <param name="WhereData">WhereClause</param>
    ''' <param name="GroupBy">GroupBy</param>
    ''' <param name="OrderBy">OrderBy</param>
    ''' <param name="HavingCondition">HavingClause</param>
    ''' <param name="ReportFormat">ReportFormat</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <param name="ReportType">ReportType</param>
    ''' <param name="ReportScope">ReportScope</param>
    ''' <param name="DateConTable">DateConditionTable</param>
    ''' <param name="columnFormat">columnFormat</param>
    ''' <remarks></remarks>
    Public Function Update_Report(ByVal QueryName As String, ByVal TableName As String, ByVal ColName As String, ByVal FormulaName As String, ByVal TxtFormula As String, ByVal WhereData As String, ByVal GroupBy As String, ByVal OrderBy As String, ByVal HavingCondition As String, ByVal ReportFormat As String, ByVal ColorCondition As String, ByVal ReportType As String, ByVal DateConTable As String, ByVal columnFormat As String, ByVal level As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("Update_Report", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.VarChar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@TableName", SqlDbType.VarChar, 500)
            cmd.Parameters("@TableName").Value = TableName
            cmd.Parameters.Add("@ColName", SqlDbType.Text)
            cmd.Parameters("@ColName").Value = ColName
            cmd.Parameters.Add("@FormulaName", SqlDbType.VarChar, 200)
            cmd.Parameters("@FormulaName").Value = FormulaName
            cmd.Parameters.Add("@TxtFormula", SqlDbType.VarChar, 1000)
            cmd.Parameters("@TxtFormula").Value = TxtFormula
            cmd.Parameters.Add("@WhereData", SqlDbType.VarChar, 7999)
            cmd.Parameters("@WhereData").Value = WhereData
            cmd.Parameters.Add("@GroupBy", SqlDbType.VarChar, 500)
            cmd.Parameters("@GroupBy").Value = GroupBy
            cmd.Parameters.Add("@OrderBy", SqlDbType.VarChar, 500)
            cmd.Parameters("@OrderBy").Value = OrderBy
            cmd.Parameters.Add("@HavingCondition", SqlDbType.VarChar, 7999)
            cmd.Parameters("@HavingCondition").Value = HavingCondition
            cmd.Parameters.Add("@ReportFormat", SqlDbType.Text)
            cmd.Parameters("@ReportFormat").Value = ReportFormat
            cmd.Parameters.Add("@ColorCondition", SqlDbType.Text)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@ReportType", SqlDbType.VarChar, 50)
            cmd.Parameters("@ReportType").Value = ReportType
            cmd.Parameters.Add("@DateConTable", SqlDbType.VarChar, 50)
            cmd.Parameters("@DateConTable").Value = DateConTable
            cmd.Parameters.Add("@columnFormat", SqlDbType.Text)
            cmd.Parameters("@columnFormat").Value = columnFormat
            cmd.Parameters.Add("@level", SqlDbType.VarChar, 50)
            cmd.Parameters("@level").Value = level
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
        End Try
        cmd.Dispose()
        Return res
    End Function
    ''' <summary>
    ''' This function updates the header of an existing report.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="HeaderHeight">HeaderHeight</param>
    ''' <param name="HeaderFormat">HeaderFormat</param>
    ''' <param name="HeaderColumns">HeaderColumns</param>
    ''' <param name="ColumnFormat">Columnformat</param>
    ''' <param name="ColumnFormula">ColumnFormula</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <returns>Status of record</returns>
    ''' <remarks></remarks>
    Public Function Update_Header(ByVal QueryName As String, ByVal HeaderHeight As String, ByVal HeaderFormat As String, ByVal HeaderColumns As String, ByVal ColumnFormat As String, ByVal ColumnFormula As String, ByVal ColorCondition As String, ByVal repid As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("Update_Header", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.varchar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@HeaderHeight", SqlDbType.varchar, 50)
            cmd.Parameters("@HeaderHeight").Value = HeaderHeight
            cmd.Parameters.Add("@HeaderFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@HeaderFormat").Value = HeaderFormat
            cmd.Parameters.Add("@HeaderColumns", SqlDbType.varchar, 7999)
            cmd.Parameters("@HeaderColumns").Value = HeaderColumns
            cmd.Parameters.Add("@ColumnFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormat").Value = ColumnFormat
            cmd.Parameters.Add("@ColumnFormula", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormula").Value = ColumnFormula
            cmd.Parameters.Add("@ColorCondition", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@repID", SqlDbType.varchar, 50)
            cmd.Parameters("@repID").Value = repid
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
        End Try
        cmd.Dispose()
        Return res
    End Function
    ''' <summary>
    ''' This function updates the footer of an existing report.
    ''' </summary>
    ''' <param name="QueryName">reportName</param>
    ''' <param name="FooterHeight">FooterHeight</param>
    ''' <param name="FooterFormat">FooterFormat</param>
    ''' <param name="FooterColumns">FooterColumns</param>
    ''' <param name="ColumnFormat">Footerformat</param>
    ''' <param name="ColumnFormula">ColumnFormula</param>
    ''' <param name="ColorCondition">ColorCondition</param>
    ''' <returns>Status of record</returns>
    ''' <remarks></remarks>
    Public Function Update_Footer(ByVal QueryName As String, ByVal FooterHeight As String, ByVal FooterFormat As String, ByVal FooterColumns As String, ByVal ColumnFormat As String, ByVal ColumnFormula As String, ByVal ColorCondition As String, ByVal repid As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("Update_Footer", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.varchar, 200)
            cmd.Parameters("@QueryName").Value = QueryName
            cmd.Parameters.Add("@FooterHeight", SqlDbType.varchar, 50)
            cmd.Parameters("@FooterHeight").Value = FooterHeight
            cmd.Parameters.Add("@FooterFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@FooterFormat").Value = FooterFormat
            cmd.Parameters.Add("@FooterColumns", SqlDbType.varchar, 7999)
            cmd.Parameters("@FooterColumns").Value = FooterColumns
            cmd.Parameters.Add("@ColumnFormat", SqlDbType.varchar, 7999)
            cmd.Parameters("@ColumnFormat").Value = ColumnFormat
            cmd.Parameters.Add("@ColumnFormula", SqlDbType.VarChar, 7999)
            cmd.Parameters("@ColumnFormula").Value = ColumnFormula
            cmd.Parameters.Add("@ColorCondition", SqlDbType.VarChar, 7999)
            cmd.Parameters("@ColorCondition").Value = ColorCondition
            cmd.Parameters.Add("@repID", SqlDbType.varchar, 50)
            cmd.Parameters("@repID").Value = repid
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
        End Try
        cmd.Dispose()
        Return res
    End Function
    ''' <summary>
    ''' To save an HTML Report
    ''' </summary>
    ''' <param name="repName">HTMLReportName</param>
    ''' <param name="Path">PhysicalPathOfTheReport</param>
    ''' <param name="DepartmentID">DepartmentID</param>
    ''' <param name="ClientID">ClientID</param>
    ''' <param name="UnderLOB">LOBID</param>
    ''' <param name="SavedBy">Author</param>
    ''' <param name="SavedOn">CurrentDate</param>
    ''' <param name="Type">Type(Summarized/Simple)</param>
    ''' <param name="queryname">OriginalReportName</param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function insertHTMLReport(ByVal repName As String, ByVal Path As String, ByVal SavedBy As String, ByVal SavedOn As String, ByVal Type As String, ByVal queryname As String) As String
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_SaveHTMLReport", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@SavedFilename", SqlDbType.VarChar, 200)
            cmd.Parameters("@SavedFilename").Value = repName
            cmd.Parameters.Add("@Path", SqlDbType.VarChar, 1000)
            cmd.Parameters("@Path").Value = Path
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 100)
            cmd.Parameters("@SavedBy").Value = SavedBy
            cmd.Parameters.Add("@SavedOn", SqlDbType.DateTime)
            cmd.Parameters("@SavedOn").Value = SavedOn
            cmd.Parameters.Add("@Type", SqlDbType.VarChar, 20)
            cmd.Parameters("@Type").Value = Type
            cmd.Parameters.Add("@queryname", SqlDbType.VarChar, 200)
            cmd.Parameters("@queryname").Value = queryname
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
        Catch ex As Exception
            res = ex.Message
            con.Close()
        End Try
        cmd.Dispose()
        Return res
    End Function
    Public Function reportArchive(ByVal user As String, ByVal repname As String)
        Dim res As String
        res = ""
        Try
            cmd = New SqlCommand("sp_archiveReport", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 200)
            cmd.Parameters("@SavedBy").Value = user
            cmd.Parameters.Add("@QueryName", SqlDbType.VarChar, 1000)
            cmd.Parameters("@QueryName").Value = repname
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            res = "1"
            '' To track management  
            cmd = New SqlCommand("sp_logRptDesignerForArchive", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = user
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 1000)
            cmd.Parameters("@ReportName").Value = repname
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            '' ends
        Catch ex As Exception
            res = ex.Message
            con.Close()
        End Try
        cmd.Dispose()
        Return res
    End Function
    ''' <summary>
    ''' Report for superadmin
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function reportForSA(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_reportForSA", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = userid
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with reports in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function reportForadmin(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_reportForadmin", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        cmd.Dispose()
        con.Close()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with reports in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function reportForlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_reportForlocal", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        cmd.Dispose()
        con.Close()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with reports in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function reportFornonlocal(ByVal user As String)
        ds.Clear()
        cmd = New SqlCommand("sp_reportFornonlocal", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = user
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' Table for superadmin
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function tableForSA(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        'ranjit made changes 

        cmd = New SqlCommand("TAbleInReportOf_SuperAdmin", con)

        'cmd = New SqlCommand("sp_tableForSA", con)
        cmd.CommandType = CommandType.StoredProcedure
        'cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        'cmd.Parameters("@userid").Value = userid
        'cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        'cmd.Parameters("@deptid").Value = deptid
        'cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        'cmd.Parameters("@clientid").Value = clientid
        'cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        'cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with Table in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function tableForadmin(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableForadmin", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with Table in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function tableForlocal(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableForlocal", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
        cmd.Parameters("@userid").Value = user
        cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' Fill dataset with table in a department
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns>StatusOfTheSP</returns>
    ''' <remarks></remarks>
    Public Function tableFornonlocal(ByVal user As String)
        ds.Clear()
        cmd = New SqlCommand("sp_tableFornonlocal", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = user
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' To track the action of exporting report to XLS
    ''' </summary>
    ''' <param name="userid">userid</param>
    ''' <param name="cudate">currentdate</param>
    ''' <param name="repscope">reportScope</param>
    ''' <param name="repname">reportName</param>
    ''' <param name="deptid">departmentid</param>
    ''' <param name="clientid">clientid</param>
    ''' <param name="lobid">lobid</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function trackXLS(ByVal userid As String, ByVal cudate As String, ByVal repscope As String, ByVal repname As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_logRptDesignerForXLS", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@OnDate", SqlDbType.VarChar, 50)
            cmd.Parameters("@OnDate").Value = cudate
            cmd.Parameters.Add("@ReportScope", SqlDbType.VarChar, 20)
            cmd.Parameters("@ReportScope").Value = repscope
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 200)
            cmd.Parameters("@ReportName").Value = repname
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function

    Public Function trackHTML(ByVal userid As String, ByVal cudate As String, ByVal repscope As String, ByVal repname As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal HTMLFilename As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_logRptDesignerForHTML", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.varchar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@OnDate", SqlDbType.varchar, 50)
            cmd.Parameters("@OnDate").Value = cudate
            cmd.Parameters.Add("@ReportScope", SqlDbType.varchar, 20)
            cmd.Parameters("@ReportScope").Value = repscope
            cmd.Parameters.Add("@ReportName", SqlDbType.varchar, 200)
            cmd.Parameters("@ReportName").Value = repname
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 9)
            cmd.Parameters("@DepartmentID").Value = deptid
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 9)
            cmd.Parameters("@ClientID").Value = clientid
            cmd.Parameters.Add("@LobID", SqlDbType.VarChar, 9)
            cmd.Parameters("@LobID").Value = lobid
            cmd.Parameters.Add("@Htmlfile", SqlDbType.varchar, 200)
            cmd.Parameters("@Htmlfile").Value = HTMLFilename
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function

    Public Function reportForuser(ByVal userid As String)
        ds.Clear()
        cmd = New SqlCommand("sp_reportForuser", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        cmd.Parameters("@userid").Value = userid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function

    'Public Function reportForuser(ByVal userid As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
    '    ds.Clear()
    '    cmd = New SqlCommand("sp_reportForuser", con)
    '    cmd.CommandType = CommandType.StoredProcedure
    '    cmd.Parameters.Add("@userid", SqlDbType.varchar, 200)
    '    cmd.Parameters("@userid").Value = userid
    '    cmd.Parameters.Add("@deptid", SqlDbType.varchar, 50)
    '    cmd.Parameters("@deptid").Value = deptid
    '    cmd.Parameters.Add("@clientid", SqlDbType.varchar, 50)
    '    cmd.Parameters("@clientid").Value = clientid
    '    cmd.Parameters.Add("@lobid", SqlDbType.varchar, 50)
    '    cmd.Parameters("@lobid").Value = lobid
    '    con.Open()
    '    da.SelectCommand = cmd
    '    da.Fill(ds)
    '    da.Dispose()
    '    con.Close()
    '    cmd.Dispose()
    '    Return ds
    'End Function
    ''' <summary>
    ''' To track the action of saving a report as HTML file
    ''' </summary>
    ''' <param name="userid">userid</param>
    ''' <param name="cudate">currentdate</param>
    ''' <param name="repname">reportName</param>
    ''' <param name="dept">departmentid</param>
    ''' <param name="client">clientid</param>
    ''' <param name="lob">lobid</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ''changed by smitha
    Public Function trackOpenreport(ByVal userid As String, ByVal cudate As String, ByVal repname As String, ByVal dept As String, ByVal client As String, ByVal lob As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_logRptDesignerForView", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@OnDate", SqlDbType.VarChar, 50)
            cmd.Parameters("@OnDate").Value = cudate
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 200)
            cmd.Parameters("@ReportName").Value = repname
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 9)
            cmd.Parameters("@DepartmentID").Value = dept
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 9)
            cmd.Parameters("@ClientID").Value = client
            cmd.Parameters.Add("@LobID", SqlDbType.VarChar, 9)
            cmd.Parameters("@LobID").Value = lob
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 200)
            cmd.Parameters("@SavedBy").Value = userid

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' To delete an existing report
    ''' </summary>
    ''' <param name="repname">reportname</param>
    ''' <param name="dept">departmentid</param>
    ''' <param name="client">clientid</param>
    ''' <param name="lob">lobid</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function deleteReport(ByVal repname As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_deleteReport", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@QueryName", SqlDbType.VarChar, 200)
            cmd.Parameters("@QueryName").Value = repname
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function
    ''' <summary>
    ''' To record the action of the deletion of a report into track mgtment
    '''  </summary>
    ''' <param name="userid">useridOftheUSerinAction</param>
    ''' <param name="cudate">currentdate</param>
    ''' <param name="repname">reportname</param>
    ''' <param name="dept">reportdepartment</param>
    ''' <param name="client">reportclient</param>
    ''' <param name="lob">reporlob</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function toTrackfordelete(ByVal userid As String, ByVal cudate As String, ByVal repname As String, ByVal dept As String, ByVal client As String, ByVal lob As String)
        Try
            cmd = New SqlCommand("sp_logRptDesignerForDelete", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@OnDate", SqlDbType.VarChar, 9)
            cmd.Parameters("@OnDate").Value = cudate
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 9)
            cmd.Parameters("@ReportName").Value = repname
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 9)
            cmd.Parameters("@DepartmentID").Value = dept
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 9)
            cmd.Parameters("@ClientID").Value = client
            cmd.Parameters.Add("@LobID", SqlDbType.VarChar, 9)
            cmd.Parameters("@LobID").Value = lob
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@SavedBy").Value = userid
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Return 0
            con.Close()
        End Try
        cmd.Dispose()
        Return 1
    End Function
    ''' <summary>
    ''' To assign report rights to a user
    ''' </summary>
    ''' <param name="recordid"></param>
    ''' <param name="userid"></param>
    ''' <param name="ReportName"></param>
    ''' <param name="view"></param>
    ''' <param name="edit"></param>
    ''' <param name="saveas"></param>
    ''' <param name="delete"></param>
    ''' <param name="cudate"></param>
    ''' <param name="assignedby"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function insertReportrights(ByVal recordid As Integer, ByVal userid As String, ByVal ReportName As String, ByVal view As String, ByVal edit As String, ByVal saveas As String, ByVal delete As String, ByVal cudate As String, ByVal assignedby As String)
        Try
            cmd = New SqlCommand("sp_insertReportRights", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@recordid", SqlDbType.Int, 18)
            cmd.Parameters("@recordid").Value = recordid
            cmd.Parameters.Add("@userid", SqlDbType.VarChar, 50)
            cmd.Parameters("@userid").Value = userid
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
            cmd.Parameters("@ReportName").Value = ReportName
            cmd.Parameters.Add("@view", SqlDbType.VarChar, 10)
            cmd.Parameters("@view").Value = view
            cmd.Parameters.Add("@edit", SqlDbType.VarChar, 10)
            cmd.Parameters("@edit").Value = edit
            cmd.Parameters.Add("@delete", SqlDbType.VarChar, 10)
            cmd.Parameters("@delete").Value = delete
            cmd.Parameters.Add("@saveAs", SqlDbType.VarChar, 10)
            cmd.Parameters("@saveAs").Value = saveas
            cmd.Parameters.Add("@currdate", SqlDbType.DateTime)
            cmd.Parameters("@currdate").Value = cudate
            cmd.Parameters.Add("@assignedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@assignedBy").Value = assignedby
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Return 0
        End Try
        cmd.Dispose()
        Return 1
    End Function
    Public Function updateReportrights(ByVal recordid As Integer, ByVal userid As String, ByVal ReportName As String, ByVal view As String, ByVal edit As String, ByVal saveas As String, ByVal delete As String, ByVal cudate As String, ByVal assignedby As String)
        Try
            cmd = New SqlCommand("sp_UpdateReportRight", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
            cmd.Parameters("@UserId").Value = userid
            cmd.Parameters.Add("@RecordId", SqlDbType.Int, 18)
            cmd.Parameters("@RecordId").Value = recordid
            cmd.Parameters.Add("@View", SqlDbType.VarChar, 10)
            cmd.Parameters("@View").Value = view
            cmd.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
            cmd.Parameters("@Edit").Value = edit
            cmd.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
            cmd.Parameters("@Delete").Value = delete
            cmd.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
            cmd.Parameters("@SaveAs").Value = saveas
            cmd.Parameters.Add("@CurrDate", SqlDbType.DateTime)
            cmd.Parameters("@CurrDate").Value = cudate
            cmd.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@AssignedBy").Value = assignedby
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            Return 0
        End Try
        cmd.Dispose()
        Return 1
    End Function
    Public Function checkExistingReportRight(ByVal userid As String, ByVal recordid As Integer) As Boolean
        Dim stat As Boolean
        stat = False
        Dim rdr As SqlDataReader
        cmd = New SqlCommand("sp_CheckExistingReportRight", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@userid", SqlDbType.VarChar, 50)
        cmd.Parameters("@userid").Value = userid
        cmd.Parameters.Add("@recordid", SqlDbType.Int, 50)
        cmd.Parameters("@recordid").Value = recordid
        con.Open()
        rdr = cmd.ExecuteReader()
        Dim st As String = ""
        stat = rdr.HasRows
        rdr.Close()
        con.Close()
        cmd.Dispose()
        Return stat
    End Function
    Public Function checkAllReportRight(ByVal recordid As Integer)
        ds.Clear()
        cmd = New SqlCommand("sp_CheckAllReportRight", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@recordid", SqlDbType.Int, 50)
        cmd.Parameters("@recordid").Value = recordid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    Public Function SelectRDReport(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        cmd = New SqlCommand("Select_RDReport1", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@deptid", SqlDbType.VarChar, 50)
        cmd.Parameters("@deptid").Value = deptid
        cmd.Parameters.Add("@clientid", SqlDbType.VarChar, 50)
        cmd.Parameters("@clientid").Value = clientid
        cmd.Parameters.Add("@lobid", SqlDbType.VarChar, 50)
        cmd.Parameters("@lobid").Value = lobid
        con.Open()
        da.SelectCommand = cmd
        da.Fill(ds)
        da.Dispose()
        con.Close()
        cmd.Dispose()
        Return ds
    End Function
    ''' <summary>
    ''' To update Groupby function
    ''' </summary>
    ''' <param name="Elem"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function updateGroupby(ByVal Elem As String, ByVal gr As String)
        Dim grpBy = ""
        Dim hj As String() = Split(Elem, "~")
        Dim k = 0
        Dim str = ""

        Dim grp As String() = Split(gr, ",")
        Dim gb = 0
        For k = 0 To hj.Length - 1
            Dim b = 0
            'If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
            Dim vbn = LCase(hj(k))
            If vbn.Contains("sum(") Or vbn.Contains("max(") Or vbn.Contains("min(") Or vbn.Contains("avg(") Or vbn.Contains("count(") Then
                Dim nm = 1
                For gb = 0 To grp.Length - 1
                    Dim hjk = Split(hj(k), " AS ")
                    If hjk(0) = grp(gb) Then
                        nm = 0
                    End If
                Next
                If nm = 1 Then
                    b = 1
                End If
            End If
            If b = 0 Then
                If str = "" Then
                    Dim hjk = Split(hj(k), " AS ")
                    str = hjk(0)

                Else
                    Dim hjk = Split(hj(k), " AS ")
                    str = str + "," + hjk(0)
                End If
            End If
        Next
        ''''''''''''''''''''''''''''''''''''''''''
        ''For k = 0 To hj.Length - 1
        ''    Dim b = 0
        ''    'If hj(k).Contains("(") Or hj(k).Contains(")") Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
        ''    If hj(k).Contains("(") Or hj(k).Contains(")") Then 'Or hj(k).Contains("+") Or hj(k).Contains("-") Or hj(k).Contains("*") Or hj(k).Contains("/") Or hj(k).Contains(">") Or hj(k).Contains("<") Then
        ''        b = 1
        ''    End If
        ''    If b = 0 Then
        ''        Dim hj1 = Split(hj(k), " AS ")
        ''        Dim tmp = hj(k)
        ''        If hj1.length > 1 Then
        ''            tmp = hj1(0)
        ''        End If
        ''        Dim ag = 0

        ''        If str = "" Then
        ''            str = tmp
        ''        Else
        ''            Dim sp = Split(str, ",")
        ''            Dim bn = 0
        ''            For ag = 0 To sp.length - 1
        ''                If Trim(sp(ag)) = Trim(tmp) Then
        ''                    bn = 1
        ''                    Exit For

        ''                End If
        ''            Next
        ''            If bn = 0 Then
        ''                str = str + "," + tmp
        ''            End If

        ''        End If
        ''    End If
        ''Next
        str = Replace(str, "$", ".")
        grpBy = str
        Return grpBy
    End Function
    ''' <summary>
    ''' to replace formulanames with their definition
    ''' </summary>
    ''' <param name="formula"></param>
    ''' <param name="tomatch"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function swapFormula(ByVal formula As String, ByVal tomatch As String)
        If (tomatch = "") Then
            GoTo ret
        Else
            Dim val As String() = tomatch.Split("~")
            Dim k = 0
            Dim l = 0
            For k = 0 To val.Length - 1
                Dim valN As String() = Split(val(k), " AS ")

                formula = formula.Replace(valN(1), valN(0))

            Next
        End If
ret:
        Return formula
    End Function
    ''' <summary>
    ''' to save exising graphs on existing report
    ''' </summary>
    ''' <param name="recid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function copyGraphs(ByVal recid, ByVal uid, ByVal nrep, ByVal ndp, ByVal ncl, ByVal nlb)
        '' fetch report name and span
        Dim dreader As SqlDataReader
        Dim objcmd As New SqlCommand("Select queryname,departmentid,clientid,underlob from idmsreportmaster where recordid='" + recid + "'", con)
        con.Close()
        Dim rname = ""
        Dim dp = ""
        Dim cl = ""
        Dim lb = ""
        Try
            con.Open()
            dreader = objcmd.ExecuteReader()
            While dreader.Read
                rname = dreader("queryname")
                dp = dreader("departmentid")
                cl = dreader("clientid")
                lb = dreader("underlob")
            End While
            dreader.Close()
            objcmd.Dispose()
            con.Close()

        Catch ex As Exception
        End Try
        Dim ds As New DataSet
        Dim da As New SqlDataAdapter
        Try
            objcmd = New SqlCommand("Select * from idmsgraphmaster where queryname='" + rname + "' and departmentid='" + dp + "' and clientid='" + cl + "' and underlob='" + lb + "'", con)
            con.Open()
            da.SelectCommand = objcmd
            da.Fill(ds)
            da.Dispose()
            con.Close()
            objcmd.Dispose()
            Dim g = 0
            For g = 0 To ds.Tables(0).Rows.Count - 1
                Dim GraphType = ds.Tables(0).Rows(g).Item("GraphType")
                Dim GraphName = ds.Tables(0).Rows(g).Item("GraphName")
                Dim ColumnName = ds.Tables(0).Rows(g).Item("ColumnName")
                Dim ColumnSeries = ds.Tables(0).Rows(g).Item("ColumnSeries")
                Dim ToDate = ds.Tables(0).Rows(g).Item("ToDate")
                Dim FromDate = ds.Tables(0).Rows(g).Item("FromDate")
                Dim CommanFormat1 = ds.Tables(0).Rows(g).Item("CommanFormat1")
                Dim legendformat = ds.Tables(0).Rows(g).Item("legendformat")
                Dim CommanFormat2 = ds.Tables(0).Rows(g).Item("CommanFormat2")
                Dim CommanFormat = ds.Tables(0).Rows(g).Item("CommanFormat")
                Dim SpecificProperties = ds.Tables(0).Rows(g).Item("SpecificProperties")
                Dim CreatedOn = DateTime.Today.ToString()
                Dim SavedBy = uid
                Dim Totalcolumn = ds.Tables(0).Rows(g).Item("Totalcolumn")
                Dim reporttype = ds.Tables(0).Rows(g).Item("reporttype")

                objcmd = New SqlCommand("insert into idmsgraphmaster values('" + GraphType + "','" + ndp + "','" + ncl + "','" + nlb + "','" + nrep + "','" + GraphName + "','" + ColumnName + "','" + ColumnSeries + "','" + ToDate + "','" + FromDate + "','" + CommanFormat1 + "','" + legendformat + "','" + CommanFormat2 + "','" + CommanFormat + "','" + SpecificProperties + "','" + CreatedOn + "','" + SavedBy + "','" + Totalcolumn + "','" + reporttype + "')", con)
                con.Open()
                objcmd.ExecuteNonQuery()
                con.Close()
                objcmd.Dispose()

            Next
        Catch ex As Exception

        End Try
    End Function
    Public Function trackUpdatereport(ByVal userid As String, ByVal cudate As String, ByVal repname As String, ByVal dept As String, ByVal client As String, ByVal lob As String)
        Dim str As String = "1"
        Try
            cmd = New SqlCommand("sp_logRptDesignerForUpdate", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 200)
            cmd.Parameters("@UserID").Value = userid
            cmd.Parameters.Add("@OnDate", SqlDbType.VarChar, 50)
            cmd.Parameters("@OnDate").Value = cudate
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 200)
            cmd.Parameters("@ReportName").Value = repname
            cmd.Parameters.Add("@DepartmentID", SqlDbType.VarChar, 9)
            cmd.Parameters("@DepartmentID").Value = dept
            cmd.Parameters.Add("@ClientID", SqlDbType.VarChar, 9)
            cmd.Parameters("@ClientID").Value = client
            cmd.Parameters.Add("@LobID", SqlDbType.VarChar, 9)
            cmd.Parameters("@LobID").Value = lob

            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception
            str = ex.Message
        End Try
        cmd.Dispose()
        Return str
    End Function

End Class
