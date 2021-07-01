Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls

Public Class UserSpan
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    Dim countUserId As Integer
    Dim countUserTable As Integer
    Public Function bind_departmentrepUser(ByVal deptid As String)
        ds1.Clear()

        comdepart = New SqlCommand("sp_repdepartmentUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_DeptartmentrepUser(ByVal deptid As String)
        ds1.Clear()

        comdepart = New SqlCommand("sp_repdepartmentUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_clientrepUser(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_repclientUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    ''' <summary>
    ''' to check if the structure already exists?
    ''' </summary>
    ''' <param name="strname"></param>
    ''' <param name="deptid"></param>
    ''' <param name="clientid"></param>
    ''' <param name="lobid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function chkstructname(ByVal strname As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String) As String
        Dim cmdchk As New SqlCommand("select * from idmsupdatetabstruct where CmdName='" & strname & "' and DeptId='" & deptid & "' and clientid='" & clientid & "' and LobId='" & lobid & "'", connection)
        Dim drchk As SqlDataReader
        Dim str As String
        connection.Open()
        drchk = cmdchk.ExecuteReader
        If drchk.Read Then
            str = "Y"
        Else
            str = "N"
        End If
        drchk.Close()
        connection.Close()
        cmdchk.Dispose()
        Return str
    End Function
    Public Function bind_lobrepUser(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_replobUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1


    End Function

    Public Function bind_DeparmentTableUser(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDeptUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()

        Return ds

    End Function
    ''' <summary>
    ''' newly added
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="cientid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function bind_clientTableUser(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDeptClientUser", connection)
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
    ''' <summary>
    ''' newly added
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="cientid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function bind_lobTableUser(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_tableforuser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 200)
        comdepart.Parameters("@userid").Value = userid
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
    ''' <summary>
    ''' newly added
    ''' </summary>
    ''' <param name="deptid"></param>
    ''' <param name="cientid"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function bind_lobTableSA(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_tableforSupAd", connection)
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

    Public Function bind_lobViewUser(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDeptClientLobUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_clientViewUser(ByVal deptid As String, ByVal clientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDeptClientUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_DeparmentViewUser(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDeptUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()

        Return ds
    End Function

    Public Function bind_DeparmentCmdUser(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDeptUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()

        Return ds

    End Function
    Public Function bind_clientCmdUser(ByVal deptid As String, ByVal clientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDeptClientUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_lobCmdUser(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDeptClientLobUser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function Get_TableStatus(ByVal TableId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetTableStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@TableId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@TableId").Value = TableId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Get_ViewStatus(ByVal ViewId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetViewStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@ViewId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@ViewId").Value = ViewId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Get_CmdStatus(ByVal CmdId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetCmdStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@CmdId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@CmdId").Value = CmdId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Get_ReportStatus(ByVal RecordId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetReportStatus", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@RecordId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@RecordId").Value = RecordId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function
    Public Function Set_AdminDefaultRights(ByVal userid As String, ByVal assignby As String)
        ds.Clear()
        connection.Open()

        comdepart = New SqlCommand("sp_AdminDefaultRgt", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userid

        comdepart.Parameters.Add("@assignby", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@assignby").Value = assignby

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1

    End Function
    Public Function bind_UserSAdminDepartment(ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("sp_UserSAdminDepartment", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds
    End Function

    Public Function bind_UserSAdminClient(ByVal deptId As String, ByVal clientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("sp_UserSAdminClient", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId

        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_userbuddyAdmin(ByVal user As String, ByVal deptId As String, ByVal clientId As String, ByVal lobid As String)
        ds.Clear()
        da = New SqlDataAdapter
        'here usha's queries is replaced by new queries
        'Dim objcmd = New SqlCommand("select userid ,username from registration where(( deptid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and (deptid in (select departmentid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and clientid in (select clientid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and lobid in (select lobid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0'))or userid in (select userid from  buddy where userid='" + user + "' and userbuddy='0' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "') or userid in(select userid from buddy where userbuddy='" + user + "')or  userid in(select userbuddy from buddy where userid='" + user + "' and userbuddy<>'0'))) and userid <>'" + user + "' order by username", connection)
        Dim objcmd = New SqlCommand("select userid ,username from registration where deptid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and (LockReason <>'$Resigned' and LockReason <>'$Transfer' or LockReason is null)", connection)


        connection.Open()
        da.SelectCommand = objcmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_userbuddy(ByVal user As String, ByVal deptId As String, ByVal clientId As String, ByVal lobid As String)
        ds.Clear()
        da = New SqlDataAdapter
        'here usha's queries is replaced by new queries
        'Dim objcmd = New SqlCommand("select userid ,username from registration where(( deptid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and (deptid in (select departmentid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and clientid in (select clientid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and lobid in (select lobid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0'))or userid in (select userid from  buddy where userid='" + user + "' and userbuddy='0' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "') or userid in(select userid from buddy where userbuddy='" + user + "')or  userid in(select userbuddy from buddy where userid='" + user + "' and userbuddy<>'0'))) and userid <>'" + user + "' order by username", connection)
        'Dim objcmd = New SqlCommand("if exists (select * from buddy where departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid = '" + lobid + "' and userid= '" + user + "' ) begin  select userid,username from (select userid ,username ,LockReason from registration where userid in (select distinct userid from buddy where departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userid<>'" + user + "' union select userbuddy as userid from buddy where userid='" + user + "')) as t where LockReason <>'$Resigned' and LockReason <>'$Transfer' or LockReason is null order by username end else select Userid ,Username from registration where userid ='-1'", connection)
        Dim objcmd = New SqlCommand("select userid ,'('+username+')' as username from registration where(( deptid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and (deptid in (select departmentid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and clientid in (select clientid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0') and lobid in (select lobid from buddy where userid='" + user + "' and departmentid='" + deptId + "' and clientid='" + clientId + "' and lobid='" + lobid + "' and userbuddy='0'))))  order by username", connection)

        connection.Open()
        da.SelectCommand = objcmd
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_UserSAdminlob(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_UserSAdminlob", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid

        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid

        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds


    End Function
    Public Function bind_UserSAdminDepartmentMasterAdmin(ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("sp_UserSAdminDepartmentMasterAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds
    End Function

    Public Function bind_UserSAdminClientMasterAdmin(ByVal deptId As String, ByVal clientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("sp_UserSAdminClientMasterAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId

        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_UserSAdminlobMasterAdmin(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_UserSAdminlobMasterAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid

        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = clientid

        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds


    End Function
    Public Function bind_deptuserMenu(ByVal deptid As String, ByVal loggedId As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userdeptMenu", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid

        comdepart.Parameters.Add("@loggedId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@loggedId").Value = loggedId

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_clientuserMenu(ByVal deptid As String, ByVal cientid As String, ByVal loggedId As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userclientMenu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)

        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid

        comdepart.Parameters.Add("@loggedId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@loggedId").Value = loggedId


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_lobuserMenu(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String, ByVal loggedId As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userlobMenu", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid

        comdepart.Parameters.Add("@loggedId", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@loggedId").Value = loggedId


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function

    Public Function Bind_AssignedMenuRightsAdmin(ByVal DeptId As String, ByVal ClientId As String, ByVal LobId As String, ByVal loggedId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_AssignedMenuRightsAdmin11", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@deptId").Value = DeptId

        comdepart.Parameters.Add("@clientId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@clientId").Value = ClientId

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobId").Value = LobId

        comdepart.Parameters.Add("@loggedId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@loggedId").Value = loggedId

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Bind_SubLink1MenuAdmin(ByVal userid As String, ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetSubLink1MenuAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@UserId").Value = userid


        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function Bind_SubLink2MenuAdmin(ByVal userid As String, ByVal MenuId As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_GetSubLink2MenuAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@MenuId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@MenuId").Value = MenuId

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 25)
        comdepart.Parameters("@UserId").Value = userid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function userselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        Dim dt As New DataTable
        dt.Columns.Add("disname")
        dt.Columns.Add("userid")
        comdepart = New SqlCommand("select_userAdminSpan1", connection)
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


        Try


            da.SelectCommand = comdepart
            da.Fill(ds)

            connection.Close()
            If ds.Tables(0).Rows.Count > 0 Then

            End If
        Catch ex As Exception
            Return dt


        End Try

        Return ds
    End Function

    Public Function tableselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_tableForadmin1", connection)
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
    Public Function viewselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_viewForadmin1", connection)
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
    Public Function cmdselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_cmdForadmin1", connection)
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
    Public Function analysisselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_analysisForadmin1", connection)
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
    Public Function reportselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_reportForadmin1", connection)
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

    Public Function reportForuser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_reportForlocal1", connection)
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


    Public Function tableForuser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_tableForlocal1", connection)
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
    Public Function viewForuser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_viewForlocal1", connection)
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

    Public Function cmdForuser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_cmdForlocal1", connection)
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

    Public Function analysisForuser(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("sp_analysisForlocal1", connection)
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



    Public Function BindAdmins(ByVal DeptId As String, ByVal ClientId As String, ByVal LobId As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_BindAdmins", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@deptId").Value = DeptId

        comdepart.Parameters.Add("@clientId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@clientId").Value = ClientId

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobId").Value = LobId

        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function



    Public Function DeleteAdmin(ByVal DeptId As String, ByVal ClientId As String, ByVal LobId As String, ByVal adminId As String)

        connection.Open()

        comdepart = New SqlCommand("sp_DeleteAdmins", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@deptId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@deptId").Value = DeptId

        comdepart.Parameters.Add("@clientId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@clientId").Value = ClientId

        comdepart.Parameters.Add("@lobId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@lobId").Value = LobId

        comdepart.Parameters.Add("@adminid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@adminid").Value = adminId

        comdepart.ExecuteNonQuery()

        connection.Close()
        Return 1
    End Function

    Public Function GetMenuName(ByVal menuid As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetMenuName", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@menuid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@menuid").Value = menuid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function

    Public Function useradminspan(ByVal adminid As String)
        ds.Clear()

        comdepart = New SqlCommand("sp_GetUserAdmin", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@adminid").Value = adminid

        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        connection.Close()
        Return ds

    End Function

    Public Function Insert_TableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String, ByVal ImportData As String, ByVal AssignedBy As String)

        connection.Open()

        comdepart = New SqlCommand("sp_InsertTableRights1", connection)
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

        comdepart.Parameters.Add("@ImportData", SqlDbType.VarChar, 10)
        comdepart.Parameters("@ImportData").Value = ImportData

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy
        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Update_TableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String, ByVal ImportData As String, ByVal AssignedBy As String)


        connection.Open()
        'previous procedure was sp_UpdateTableRights1 still in DB
        comdepart = New SqlCommand("sp_UpdateTableRights2", connection)
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

        comdepart.Parameters.Add("@ImportData", SqlDbType.VarChar, 10)
        comdepart.Parameters("@ImportData").Value = ImportData

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy


        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1
    End Function
    Public Function Update_editTableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String, ByVal ImportData As String, ByVal AssignedBy As String)


        connection.Open()

        comdepart = New SqlCommand("sp_UpdateTableRights1", connection)
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

        comdepart.Parameters.Add("@ImportData", SqlDbType.VarChar, 10)
        comdepart.Parameters("@ImportData").Value = ImportData

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy


        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1
    End Function


End Class
