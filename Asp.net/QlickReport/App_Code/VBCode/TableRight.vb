Imports Microsoft.VisualBasic
Imports System
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls


Public Class TableRight
    Dim department As DropDownList
    Dim ds As New DataSet

    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    Dim identity As String
    Dim comdepartTrack As New SqlCommand

    Public Function bind_TableRightsByUser(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetTableByUser", connection)
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
    Public Function bind_TableRightsByTable(ByVal TableId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetTableByTable", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@TableId", SqlDbType.NVarChar)
        comdepart.Parameters("@TableId").Value = TableId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Update_TableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateTableRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@TableId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Edit").Value = Edit

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@DeleteData", SqlDbType.VarChar, 10)
        comdepart.Parameters("@DeleteData").Value = DeleteData

        comdepart.Parameters.Add("@AddColumn", SqlDbType.VarChar, 10)
        comdepart.Parameters("@AddColumn").Value = AddColumn

        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Delete_TableRights(ByVal TableId As String, ByVal UserId As String)


        comdepart = New SqlCommand("sp_DeleteTableRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@TableId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function Get_TableOwner(ByVal TableId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetTableOwner", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@TableId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Insert_TableOwnership(ByVal TableId As String, ByVal previousOwnerId As String, ByVal newOwnerId As String, ByVal ChangedBy As String)



        connection.Open()
        comdepart = New SqlCommand("sp_InsertTableOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@TableId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId

        comdepart.Parameters.Add("@previousOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@previousOwnerId").Value = previousOwnerId

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.Parameters.Add("@ChangeBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ChangeBy").Value = ChangedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Insert_NewAdmin(ByVal adminid As String, ByVal adminname As String, ByVal usertype As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid1 As String, ByVal comment As String)

        connection.Open()
        comdepart = New SqlCommand("sp_InsertNewAdmin ", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@adminid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@adminid").Value = adminid
        comdepart.Parameters.Add("@adminname", SqlDbType.VarChar, 10)
        comdepart.Parameters("@adminname").Value = adminname

        comdepart.Parameters.Add("@usertype", SqlDbType.VarChar, 10)
        comdepart.Parameters("@usertype").Value = usertype

        comdepart.Parameters.Add("@deptid", SqlDbType.VarChar, 10)
        comdepart.Parameters("@deptid").Value = deptid

        comdepart.Parameters.Add("@clientid", SqlDbType.VarChar, 10)
        comdepart.Parameters("@clientid").Value = clientid

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 10)
        comdepart.Parameters("@lobId").Value = lobid1

        comdepart.Parameters.Add("@createdDate", SqlDbType.DateTime)
        comdepart.Parameters("@createddate").Value = System.DateTime.Now.ToString()

        comdepart.Parameters.Add("@comment", SqlDbType.VarChar, 1000)
        comdepart.Parameters("@comment").Value = comment


        comdepart.ExecuteNonQuery()




        connection.Close()
        Return 1

    End Function
    Public Function Insert_ReportOwnership(ByVal ReportId As String, ByVal previousOwnerId As String, ByVal newOwnerId As String, ByVal ChangedBy As String)



        connection.Open()
        comdepart = New SqlCommand("sp_InsertReportOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@RecordId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@RecordId").Value = ReportId

        comdepart.Parameters.Add("@previousOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@previousOwnerId").Value = previousOwnerId

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.Parameters.Add("@ChangeBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ChangeBy").Value = ChangedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Insert_UpdateCommandOwnership(ByVal CmdId As String, ByVal previousOwnerId As String, ByVal newOwnerId As String, ByVal ChangedBy As String)

        connection.Open()
        comdepart = New SqlCommand("sp_InsertCmdOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@CmdId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId

        comdepart.Parameters.Add("@previousOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@previousOwnerId").Value = previousOwnerId

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.Parameters.Add("@ChangeBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ChangeBy").Value = ChangedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Insert_ViewOwnership(ByVal ViewId As String, ByVal previousOwnerId As String, ByVal newOwnerId As String, ByVal ChangedBy As String)

        connection.Open()
        comdepart = New SqlCommand("sp_InsertViewOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@ViewId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId

        comdepart.Parameters.Add("@previousOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@previousOwnerId").Value = previousOwnerId

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.Parameters.Add("@ChangeBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ChangeBy").Value = ChangedBy



        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function Get_Password(ByVal userid As String)

        Dim password As String
        connection.Open()
        comdepart = New SqlCommand("sp_GetPassword", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@userid").Value = userid



        password = comdepart.ExecuteScalar()

        connection.Close()

        Return password
    End Function
    Public Function Update_NewAdmin(ByVal adminid As String, ByVal adminname As String, ByVal usertype As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid1 As String, ByVal comment As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateNewAdmin ", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@adminid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@adminid").Value = adminid

        comdepart.Parameters.Add("@adminname", SqlDbType.VarChar, 10)
        comdepart.Parameters("@adminname").Value = adminname

        comdepart.Parameters.Add("@usertype", SqlDbType.VarChar, 10)
        comdepart.Parameters("@usertype").Value = usertype

        comdepart.Parameters.Add("@deptid", SqlDbType.VarChar, 10)
        comdepart.Parameters("@deptid").Value = deptid

        comdepart.Parameters.Add("@clientid", SqlDbType.VarChar, 10)
        comdepart.Parameters("@clientid").Value = clientid

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 10)
        comdepart.Parameters("@lobId").Value = lobid1

        comdepart.Parameters.Add("@createdDate", SqlDbType.DateTime)
        comdepart.Parameters("@createddate").Value = System.DateTime.Now.ToString()

        comdepart.Parameters.Add("@comment", SqlDbType.VarChar, 1000)
        comdepart.Parameters("@comment").Value = comment


        comdepart.ExecuteNonQuery()




        connection.Close()

        Return 1


    End Function
    Public Function Delete_ViewRights(ByVal ViewId As String, ByVal UserId As String)


        comdepart = New SqlCommand("sp_DeleteViewRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@ViewId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function Update_ViewRights(ByVal UserId As String, ByVal ViewId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateViewRights", connection)
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

        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Function bind_ViewRightsByView(ByVal ViewId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetViewByView", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@ViewId", SqlDbType.NVarChar)
        comdepart.Parameters("@ViewId").Value = ViewId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function bind_ViewRightsByUser(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetViewByUser", connection)
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
    Public Function bind_CmdRightsByUser(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetCmdByUser", connection)
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

    Public Function bind_CmdRightsByCmd(ByVal CmdId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetCmdByCmd", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@CmdId", SqlDbType.NVarChar)
        comdepart.Parameters("@CmdId").Value = CmdId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Delete_CmdRights(ByVal CmdId As String, ByVal UserId As String)


        comdepart = New SqlCommand("sp_DeleteCmdRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@CmdId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function

    Public Function Update_CmdRights(ByVal UserId As String, ByVal CmdId As String, ByVal View As String, ByVal run As String, ByVal Delete As String, ByVal SaveAs As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateCmdRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@CmdId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId

        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
        comdepart.Parameters("@View").Value = View

        comdepart.Parameters.Add("@Run", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Run").Value = run

        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@Delete").Value = Delete

        comdepart.Parameters.Add("@SaveAs", SqlDbType.VarChar, 10)
        comdepart.Parameters("@SaveAs").Value = SaveAs

        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Delete_ReportRights(ByVal RecordId As String, ByVal UserId As String)


        comdepart = New SqlCommand("sp_DeleteReportRight", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@RecordId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@RecordId").Value = RecordId
        comdepart.Parameters.Add("@UserId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function

    Public Function bind_ReportRightsByUser(ByVal UserId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetReportByUser", connection)
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

    Public Function bind_ReportRightsByReport(ByVal recordId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetReportByReport", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@RecordId", SqlDbType.NVarChar)
        comdepart.Parameters("@RecordId").Value = recordId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function Update_ReportRights(ByVal UserId As String, ByVal recordId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateReportRights", connection)
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

        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Update_TableOwnership(ByVal TableId As String, ByVal Tablename As String, ByVal newOwnerId As String, ByVal oldowner As String)



        connection.Open()
        comdepart = New SqlCommand("sp_UpdateTableOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@TableId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId

        comdepart.Parameters.Add("@Tablename", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Tablename").Value = Tablename


        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepart.Parameters.Add("@oldowner", SqlDbType.VarChar, 50)
        comdepart.Parameters("@oldowner").Value = oldowner

        comdepartTrack = New SqlCommand("sp_TrackOwneredBy", connection)
        comdepartTrack.CommandType = CommandType.StoredProcedure

        comdepartTrack.Parameters.Add("@Entity", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@Entity").Value = TableId

        comdepartTrack.Parameters.Add("@EntityName", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@EntityName").Value = "Table"

        comdepartTrack.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@newOwnerId").Value = newOwnerId

        comdepartTrack.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepartTrack.Parameters("@ChangeOn").Value = System.DateTime.Now()

        comdepartTrack.Parameters.Add("@AssignBy", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@AssignBy").Value = "Admin"

       
        comdepart.ExecuteNonQuery()
        comdepartTrack.ExecuteNonQuery()


        connection.Close()

        Return 1
    End Function
    Public Function Get_ViewOwner(ByVal ViewId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetViewOwner", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@ViewId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Get_CmdOwner(ByVal CmdId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetCmdOwner", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@CmdId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Get_ReportOwner(ByVal RecordId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetReportOwner", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@RecordId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@RecordId").Value = RecordId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function

    Public Function Update_UpdateCommandOwnership(ByVal CmdId As String, ByVal Cmdname As String, ByVal newOwnerId As String, ByVal oldowner As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateCmdOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@CmdId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId

        comdepart.Parameters.Add("@Cmdname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Cmdname").Value = Cmdname

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@oldowner", SqlDbType.VarChar, 50)
        comdepart.Parameters("@oldowner").Value = oldowner

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepartTrack = New SqlCommand("sp_TrackOwneredBy", connection)
        comdepartTrack.CommandType = CommandType.StoredProcedure
        comdepartTrack.Parameters.Add("@Entity", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@Entity").Value = CmdId

        comdepartTrack.Parameters.Add("@EntityName", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@EntityName").Value = "Update Command"

        comdepartTrack.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@newOwnerId").Value = newOwnerId

        comdepartTrack.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepartTrack.Parameters("@ChangeOn").Value = System.DateTime.Now()

        comdepartTrack.Parameters.Add("@AssignBy", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@AssignBy").Value = "Admin"
        comdepart.ExecuteNonQuery()
        comdepartTrack.ExecuteNonQuery()


        connection.Close()

        Return 1
    End Function
    Public Function Update_ReportOwnership(ByVal RecordId As String, ByVal Reportname As String, ByVal newOwnerId As String, ByVal oldowner As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateReportOwnership", connection)


        comdepart.CommandType = CommandType.StoredProcedure


        comdepart.Parameters.Add("@RecordId", SqlDbType.VarChar, 50)

        comdepart.Parameters("@RecordId").Value = RecordId

        comdepart.Parameters.Add("@Reportname", SqlDbType.VarChar, 50)

        comdepart.Parameters("@Reportname").Value = Reportname


        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@oldowner", SqlDbType.VarChar, 50)
        comdepart.Parameters("@oldowner").Value = oldowner

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)

        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepartTrack = New SqlCommand("sp_TrackOwneredBy", connection)
        comdepartTrack.CommandType = CommandType.StoredProcedure
        comdepartTrack.Parameters.Add("@Entity", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@Entity").Value = RecordId

        comdepartTrack.Parameters.Add("@EntityName", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@EntityName").Value = "Report"
        comdepartTrack.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@newOwnerId").Value = newOwnerId

        comdepartTrack.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepartTrack.Parameters("@ChangeOn").Value = System.DateTime.Now()

        comdepartTrack.Parameters.Add("@AssignBy", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@AssignBy").Value = "Admin"
        comdepart.ExecuteNonQuery()
        comdepartTrack.ExecuteNonQuery()
        connection.Close()

        Return 1
    End Function
    Public Function Update_ViewOwnership(ByVal ViewId As String, ByVal Viewname As String, ByVal newOwnerId As String, ByVal oldowner As String)

        connection.Open()
        comdepart = New SqlCommand("sp_UpdateViewOwnership", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@ViewId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId

        comdepart.Parameters.Add("@Viewname", SqlDbType.VarChar, 50)
        comdepart.Parameters("@Viewname").Value = Viewname

        comdepart.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@newOwnerId").Value = newOwnerId

        comdepart.Parameters.Add("@oldowner", SqlDbType.VarChar, 50)
        comdepart.Parameters("@oldowner").Value = oldowner

        comdepart.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepart.Parameters("@ChangeOn").Value = System.DateTime.Now().ToString()

        comdepartTrack = New SqlCommand("sp_TrackOwneredBy", connection)
        comdepartTrack.CommandType = CommandType.StoredProcedure
        comdepartTrack.Parameters.Add("@Entity", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@Entity").Value = ViewId

        comdepartTrack.Parameters.Add("@EntityName", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@EntityName").Value = "View"
        comdepartTrack.Parameters.Add("@newOwnerId", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@newOwnerId").Value = newOwnerId

        comdepartTrack.Parameters.Add("@ChangeOn", SqlDbType.DateTime)
        comdepartTrack.Parameters("@ChangeOn").Value = System.DateTime.Now()

        comdepartTrack.Parameters.Add("@AssignBy", SqlDbType.VarChar, 50)
        comdepartTrack.Parameters("@AssignBy").Value = "Admin"
        comdepart.ExecuteNonQuery()
        comdepartTrack.ExecuteNonQuery()


        connection.Close()

        Return 1
    End Function
    Public Function Update_TableRight(ByVal UserId() As String, ByVal TableId() As String, ByVal View() As String, ByVal Edit() As String, ByVal Delete() As String, ByVal DeleteData() As String, ByVal AddColumn() As String, ByVal AssignedBy As String)

        Dim i As Integer
        Dim j As Integer
        Dim count As Integer
        count = 10
        ' countUserId = Session("countUserId")
        'countUserTable = Session("countUserTable")
        For i = 0 To count
            If UserId(i) <> "" Then
                For j = 0 To count
                    If TableId(j) <> "" Then



                        connection.Open()
                        comdepart = New SqlCommand("sp_UpdateTableRight", connection)
                        comdepart.CommandType = CommandType.StoredProcedure

                        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
                        comdepart.Parameters("@UserId").Value = UserId(i)

                        comdepart.Parameters.Add("@TableId", SqlDbType.VarChar, 50)
                        comdepart.Parameters("@TableId").Value = TableId(j)

                        comdepart.Parameters.Add("@View", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@View").Value = View(j)

                        comdepart.Parameters.Add("@Edit", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@Edit").Value = Edit(j)

                        comdepart.Parameters.Add("@Delete", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@Delete").Value = Delete(j)

                        comdepart.Parameters.Add("@DeleteData", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@DeleteData").Value = DeleteData(j)

                        comdepart.Parameters.Add("@AddColumn", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@AddColumn").Value = AddColumn(j)

                        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
                        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date


                        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 10)
                        comdepart.Parameters("@AssignedBy").Value = AssignedBy


                        comdepart.ExecuteNonQuery()


                        connection.Close()
                    End If
                Next

            End If
        Next

        Return 1
    End Function








End Class
