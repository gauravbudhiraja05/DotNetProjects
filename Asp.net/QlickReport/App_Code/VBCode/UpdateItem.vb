Imports Microsoft.VisualBasic
Imports System
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls

Public Class UpdateItem
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    Dim identity As String

    Public Function Update_ViewRight(ByVal UserId As String, ByVal ViewId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)
       
        connection.Open()
        'previous procedure was sp_UpdateViewRight still in DB
        comdepart = New SqlCommand("sp_UpdateViewRight1", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@ViewId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Edit").Value = Edit

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
        comdepart.Parameters("@SaveAs").Value = SaveAs

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date


        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy

        comdepart.ExecuteNonQuery()

        connection.Close()
      

        Return 1
    End Function

    Public Function Update_CmdRight(ByVal UserId As String, ByVal cmdId As String, ByVal View As String, ByVal run As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)

        connection.Open()
        'previous procedure was sp_UpdateCmdRight still in DB
        comdepart = New SqlCommand("sp_UpdateCmdRight1", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@CmdId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@CmdId").Value = cmdId

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Run", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Run").Value = run

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
        comdepart.Parameters("@SaveAs").Value = SaveAs

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy

        comdepart.ExecuteNonQuery()

        connection.Close()

            
        Return 1
    End Function

    Public Function Update_ReportRight(ByVal UserId As String, ByVal recordId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)


        connection.Open()
        'previous procedure was sp_UpdateReportRight still in DB
        comdepart = New SqlCommand("sp_UpdateReportRight1", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@RecordId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@RecordId").Value = recordId

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Edit").Value = Edit

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
        comdepart.Parameters("@SaveAs").Value = SaveAs


        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy

        comdepart.ExecuteNonQuery()

        connection.Close()
        Return 1
    End Function
    Public Function Change_TableStatus(ByVal tableid As String, ByVal newstatus As String)

        connection.Open()
        comdepart = New SqlCommand("sp_ChangeTableStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@tableid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@tableid").Value = tableid

        comdepart.Parameters.Add("@newstatus", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newstatus").Value = newstatus

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function
    Public Function Change_ReportStatus(ByVal recordid As String, ByVal newstatus As String)

        connection.Open()
        comdepart = New SqlCommand("sp_ChangeReportStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@recordid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@recordid").Value = recordid

        comdepart.Parameters.Add("@newstatus", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newstatus").Value = newstatus

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function
    Public Function Change_ViewStatus(ByVal viewid As String, ByVal newstatus As String)

        connection.Open()
        comdepart = New SqlCommand("sp_ChangeViewStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@viewid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@viewid").Value = viewid

        comdepart.Parameters.Add("@newstatus", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newstatus").Value = newstatus

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function
    Public Function Change_CmdStatus(ByVal cmdid As String, ByVal newstatus As String)

        connection.Open()
        comdepart = New SqlCommand("sp_ChangeCmdStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@cmdid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@cmdid").Value = cmdid

        comdepart.Parameters.Add("@newstatus", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newstatus").Value = newstatus

        comdepart.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function



End Class
