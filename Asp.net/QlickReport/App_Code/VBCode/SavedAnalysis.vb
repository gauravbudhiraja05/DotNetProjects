Imports Microsoft.VisualBasic

Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.Data.OleDb

Imports System.IO
Imports System.IO.StreamReader
Imports System.Web.Configuration
Imports System.Math


Public Class SavedAnalysis
    Dim ds As New DataSet

    Dim drdata As SqlDataReader

    Dim comdepart As SqlCommand
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Public Function insert_analysis(ByVal depname As String, ByVal depid As String, ByVal cltname As String, ByVal cltid As String, ByVal lobname As String, ByVal lobid As String, ByVal ReportName As String, ByVal query As String, ByVal analysisname As String, ByVal Reportcolumns As String, ByVal Processedcolumns As String, ByVal tablename As String, ByVal formulas As String, ByVal SavedBy As String, ByVal status As String)
        'Reportcolumns = Reportcolumns.Replace(",", "$")
        'Processedcolumns = Processedcolumns.Replace(",", "$")
        Dim b As Boolean = True
        comdepart = New SqlCommand("select analysisname from SavedAnalysis where analysisname='" + analysisname + "'", connection)
        connection.Open()
        drdata = comdepart.ExecuteReader
        While drdata.Read
            If IsDBNull(drdata("analysisname")) Then
            Else
                'If analysisname = drdata("analysisname") Then
                Return "2"
                Exit Function
                'End If
            End If

        End While
        connection.Close()
        drdata.Close()
        comdepart = New SqlCommand("select name from sysobjects where name='" + analysisname + "' and xtype='u'", connection)
        connection.Open()
        drdata = comdepart.ExecuteReader
        While drdata.Read()


            'If drdata("name") = analysisname Then
            b = False

            Return "2"
            Exit Function
            'Else
            'b = True
            'End If
        End While
        drdata.Close()
        connection.Close()
        Dim Formulaarray = formulas.Split(",")
        comdepart = New SqlCommand("insert into SavedAnalysis values(@depname,@depid,@cltname,@cltid,@lobname,@lobid,@ReportName,@query,@analysisname,@Reportcolumns,@Processedcolumns,@tablename,@Maxs,@Mins,@Average,@counts,@Mean,@Mode,@Range,@Median,@Rowsumpercentage,@Standarderror,@Correlation,@Regression,@Standarddeviation,@Columnpercentage,@Rowpercentage,@Columnsumpercentage,@Accumulatedsum,@savedby,@status)", connection)
        connection.Open()
        comdepart.Parameters.Add("@depname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@depname").Value = depname
        comdepart.Parameters.Add("@depid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@depid").Value = depid
        comdepart.Parameters.Add("@cltname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@cltname").Value = cltname
        comdepart.Parameters.Add("@cltid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@cltid").Value = cltid
        comdepart.Parameters.Add("@lobname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobname").Value = lobname
        comdepart.Parameters.Add("@lobid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ReportName").Value = ReportName

        comdepart.Parameters.Add("@query", SqlDbType.VarChar, 7999)
        comdepart.Parameters("@query").Value = query

        comdepart.Parameters.Add("@analysisname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@analysisname").Value = analysisname

        comdepart.Parameters.Add("@Reportcolumns", SqlDbType.VarChar, 5044)
        comdepart.Parameters("@Reportcolumns").Value = Reportcolumns

        comdepart.Parameters.Add("@Processedcolumns", SqlDbType.VarChar, 5012)
        comdepart.Parameters("@Processedcolumns").Value = Processedcolumns

        comdepart.Parameters.Add("@tablename", SqlDbType.VarChar, 50)
        comdepart.Parameters("@tablename").Value = tablename

        comdepart.Parameters.Add("@Maxs", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Maxs").Value = Formulaarray(0)

        comdepart.Parameters.Add("@Mins", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mins").Value = Formulaarray(1)

        comdepart.Parameters.Add("@Average", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Average").Value = Formulaarray(2)

        comdepart.Parameters.Add("@counts", SqlDbType.VarChar, 50)
        comdepart.Parameters("@counts").Value = Formulaarray(3)

        comdepart.Parameters.Add("@Mean", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mean").Value = Formulaarray(4)

        comdepart.Parameters.Add("@Mode", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mode").Value = Formulaarray(5)

        comdepart.Parameters.Add("@Range", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Range").Value = Formulaarray(6)

        comdepart.Parameters.Add("@Median", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Median").Value = Formulaarray(7)

        comdepart.Parameters.Add("@Rowsumpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Rowsumpercentage").Value = Formulaarray(8)

        comdepart.Parameters.Add("@Standarderror", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Standarderror").Value = Formulaarray(9)

        comdepart.Parameters.Add("@Correlation", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Correlation").Value = Formulaarray(10)

        comdepart.Parameters.Add("@Regression", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Regression").Value = Formulaarray(11)

        comdepart.Parameters.Add("@Standarddeviation", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Standarddeviation").Value = Formulaarray(12)

        comdepart.Parameters.Add("@Columnpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Columnpercentage").Value = Formulaarray(13)

        comdepart.Parameters.Add("@Rowpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Rowpercentage").Value = Formulaarray(14)

        comdepart.Parameters.Add("@Columnsumpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Columnsumpercentage").Value = Formulaarray(15)

        comdepart.Parameters.Add("@Accumulatedsum", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Accumulatedsum").Value = Formulaarray(16)
        comdepart.Parameters.Add("@savedby", SqlDbType.VarChar, 50)
        comdepart.Parameters("@savedby").Value = SavedBy
        comdepart.Parameters.Add("@status", SqlDbType.VarChar, 50)
        comdepart.Parameters("@status").Value = status
        comdepart.ExecuteNonQuery()
        connection.Close()
        comdepart = New SqlCommand("select * into " + analysisname + "  from  tabddlReport" + ReportName & SavedBy + "", connection)
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()

        Return "1"
        Exit Function
    End Function
    Function SelectvalueFrom_Analysis(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)

        ds.Clear()
        comdepart = New SqlCommand("select_analysis", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()

        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function
    Function Analysis_forlocaluser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)

        ds.Clear()
        comdepart = New SqlCommand("SelectAnalysis_Forlocaluser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function
    Public Function Analysis_forNonlocaluser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("SelectAnalysis_ForNonlocaluser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function
    Public Function update_analysis(ByVal analysisname As String, ByVal procssedcolumns As String, ByVal allcolumns As String, ByVal formulas As String)
        Dim Formulaarray = formulas.Split(",") '@analysisname
        comdepart = New SqlCommand("update SavedAnalysis set reportcolumns=@Reportcolumns,processedcolumns=@Processedcolumns,Maxs=@Maxs,Mins=@Mins,Average=@Average,counts=@counts,Mean=@Mean,Mode=@Mode,Range=@Range,Median=@Median,Rowsumpercentage=@Rowsumpercentage,Standarderror=@Standarderror,Correlation=@Correlation,Regression=@Regression,Standarddeviation=@Standarddeviation,Columnpercentage=@Columnpercentage,Rowpercentage=@Rowpercentage,Columnsumpercentage=@Columnsumpercentage,Accumulatedsum=@Accumulatedsum where analysisname='" + analysisname + "'", connection)
        connection.Open()
       

        comdepart.Parameters.Add("@Reportcolumns", SqlDbType.VarChar, 5044)
        comdepart.Parameters("@Reportcolumns").Value = allcolumns

        comdepart.Parameters.Add("@Processedcolumns", SqlDbType.VarChar, 5012)
        comdepart.Parameters("@Processedcolumns").Value = procssedcolumns

        

        comdepart.Parameters.Add("@Maxs", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Maxs").Value = Formulaarray(0)

        comdepart.Parameters.Add("@Mins", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mins").Value = Formulaarray(1)

        comdepart.Parameters.Add("@Average", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Average").Value = Formulaarray(2)

        comdepart.Parameters.Add("@counts", SqlDbType.VarChar, 50)
        comdepart.Parameters("@counts").Value = Formulaarray(3)

        comdepart.Parameters.Add("@Mean", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mean").Value = Formulaarray(4)

        comdepart.Parameters.Add("@Mode", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Mode").Value = Formulaarray(5)

        comdepart.Parameters.Add("@Range", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Range").Value = Formulaarray(6)

        comdepart.Parameters.Add("@Median", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Median").Value = Formulaarray(7)

        comdepart.Parameters.Add("@Rowsumpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Rowsumpercentage").Value = Formulaarray(8)

        comdepart.Parameters.Add("@Standarderror", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Standarderror").Value = Formulaarray(9)

        comdepart.Parameters.Add("@Correlation", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Correlation").Value = Formulaarray(10)

        comdepart.Parameters.Add("@Regression", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Regression").Value = Formulaarray(11)

        comdepart.Parameters.Add("@Standarddeviation", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Standarddeviation").Value = Formulaarray(12)

        comdepart.Parameters.Add("@Columnpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Columnpercentage").Value = Formulaarray(13)

        comdepart.Parameters.Add("@Rowpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Rowpercentage").Value = Formulaarray(14)

        comdepart.Parameters.Add("@Columnsumpercentage", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Columnsumpercentage").Value = Formulaarray(15)

        comdepart.Parameters.Add("@Accumulatedsum", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Accumulatedsum").Value = Formulaarray(16)
        
        comdepart.ExecuteNonQuery()
        connection.Close()
        
        Return "1"
    End Function
    Function Select_Analysis(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)

        ds.Clear()
        comdepart = New SqlCommand("select_analysisvalue", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = user
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function
    Public Function bind_localuser(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("localuser_savedanalysis", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_nonlocaluser(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("nonlocaluser_savedanalysis", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function htmlreport_nonlocal(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("nonlocaluser_htmlreport", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
End Class
