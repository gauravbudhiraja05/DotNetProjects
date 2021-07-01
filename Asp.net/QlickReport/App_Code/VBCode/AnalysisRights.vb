Imports Microsoft.VisualBasic
Imports System
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls

Public Class AnalysisRights
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand


    Public Function bind_lobAnalysis(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDeptClientLob", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1


    End Function


    Public Function bind_clientAnalysis(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDeptClient", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1


    End Function

    Public Function bind_DeparmentAnalysis(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()

        Return ds
    End Function

    Public Function bind_DeparmentAnalysisUser(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDeptUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds

    End Function
   
    Public Function bind_clientAnalysisUser(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDeptClientUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
   
    Public Function bind_lobAnalysisUser(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectAnalysisOnDeptClientLobUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function

    Public Function Insert_AnalysisRights(ByVal UserId As String, ByVal Reportname As String, ByVal View As String, ByVal Delete As String, ByVal AssignedBy As String)

        connection.Open()

        comdepart = New SqlCommand("sp_InsertAnalysisRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@Reportname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy
        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Update_AnalysisRight(ByVal UserId As String, ByVal Reportname As String, ByVal View As String, ByVal Delete As String, ByVal AssignedBy As String)


        connection.Open()
        comdepart = New SqlCommand("sp_UpdateAnalysisRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@Reportname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname


        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy

        comdepart.ExecuteNonQuery()

        connection.Close()
        Return 1
    End Function
    Public Function bind_AnalysisRightsByUser(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetAnalysisByUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function

    Public Function bind_AnalysisRightsByAnalysis(ByVal Reportname As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetAnalysisByAnalysis", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@Reportname", SqlDbType.NVarChar)
        comdepart.Parameters("@Reportname").Value = Reportname
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function Delete_AnalysisRights(ByVal Reportname As String, ByVal UserId As String)


        comdepart = New SqlCommand("sp_DeleteAnalysisRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@Reportname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function Get_AnalysisOwner(ByVal Reportname As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetAnalysisOwner", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@Reportname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function

    Public Function Update_AnalysisOwnership(ByVal Reportname As String, ByVal newOwnerId As String, ByVal oldowner As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateAnalysisOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@Reportname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@oldowner", SqlDbType.VarChar, 50)
        comdepart.Parameters("@oldowner").Value = oldowner

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function


    Public Function Get_AnalysisStatus(ByVal Reportname As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetAnalysisStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@Reportname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function

    Public Function Change_AnalysisStatus(ByVal Reportname As String, ByVal newstatus As String)

        connection.Open()
        comdepart = New SqlCommand("sp_ChangeAnalysisStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@Reportname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Reportname").Value = Reportname

        comdepart.Parameters.Add("@newstatus", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newstatus").Value = newstatus

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function


End Class
