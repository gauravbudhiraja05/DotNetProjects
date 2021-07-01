Imports Microsoft.VisualBasic
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.WebControls
Imports System.Data.OleDb
Imports System.IO.StreamReader



Public Class Functions
    Dim department As DropDownList
    Dim ds As New DataSet
    Dim ds1 As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim comdepart As New SqlCommand
    '****** pawan changes start**********
    Dim repobj As New ReportDesigner
    Public Function get_user_table(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        connection.Close()
        ds.Clear()

        Dim SCOPE As String = repobj.chkUserscope(user)
        If SCOPE = "Local" Then
            comdepart = New SqlCommand("SelectTable_ForLocaluser", connection)
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
        Else
            comdepart = New SqlCommand("SelectTable_ForNonLocaluser", connection)
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
        End If
    End Function
    '****** pawan changes end**********
    Public Function bind_Deptartment()
        ds.Clear()
        comdepart = New SqlCommand("select_dept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_Department()
        ds.Clear()
        comdepart = New SqlCommand("select_dept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.Dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function userselectadminspan(ByVal user As String, ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userAdminSpan", connection)
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
        da.dispose()
        connection.Close()
        Return ds
    End Function

    Public Function bind_departmentAnalysisrep(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("htmlanalysisreport", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function

    Public Function bind_openrep(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()

        comdepart = New SqlCommand("select_openreport", connection)
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
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function

    Public Function bind_openAnarep(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()

        comdepart = New SqlCommand("select_openanareport", connection)
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
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function get_table(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        connection.Close()
        ds.Clear()
        comdepart = New SqlCommand("select Tableid,tablename from warslobtablemaster where Departmentid='" + deptid + "' and Clientid='" + cientid + "' and lobid='" + lobid + "'", connection)
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        Return ds
    End Function
 Public Function bindUserClients(ByVal userId As String, ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_UserClients", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId
        comdepart.Parameters("@userid").Value = userId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        connection.Close()
        Return ds

    End Function
    Public Function bind_adminDept(ByVal loggedAdmin As String)
        ds.Clear()

        comdepart = New SqlCommand("select_AdminDept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@adminid").Value = loggedAdmin
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        connection.Close()
        Return ds

    End Function
    Public Function bind_Admin()
        comdepart = New SqlCommand("select_admin", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        Return ds
    End Function
    Public Function bind_client(ByVal deptid As String)
        ds.Clear()

        comdepart = New SqlCommand("select_client", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function
    Public Function bind_deptuser(ByVal deptid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userdept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_clientuser(ByVal deptid As String, ByVal cientid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userclient", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        connection.Close()
        Return ds
    End Function
 Public Function bindAdminClients(ByVal adminId As String, ByVal deptId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_AdminClients", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId
        comdepart.Parameters("@adminid").Value = adminId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        connection.Close()
        Return ds

    End Function
    Public Function bind_lob(ByVal deptid As String, ByVal cientid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_lob", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_lobuser(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userlob", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function
    Public Function bind_departmentrep(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("select_repdepartment", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1
    End Function
    Public Function bind_clientrep(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("select_repclient", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds1)
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    Public Function bind_lobrep(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("select_replob", connection)
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
        da.dispose()
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1


    End Function
    '''''''''''''''vikas functions''''''''''''
    Public Function bind_Dept()
        ds.Clear()
        comdepart = New SqlCommand("select_dept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader

        da.dispose()
        connection.Close()

        Return ds

    End Function

    ''' <summary>
    ''' assign report rights
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Insert_ReportRights(ByVal UserId As String, ByVal recordId As String, ByVal reportname As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)


        connection.Open()

        comdepart = New SqlCommand("sp_InsertReportRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@UserId").Value = UserId

        comdepart.Parameters.Add("@RecordId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@RecordId").Value = recordId

        comdepart.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
        comdepart.Parameters("@ReportName").Value = reportname


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


     Public Function Update_TableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String, ByVal AssignedBy As String)


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

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy


        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1
    End Function


    Public Function bind_lobTable(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDeptClientLob", connection)
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




    Public Function bind_clientTable(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDeptClient", connection)
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

    Public Function bind_DeparmentTable(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()

        Return ds

    End Function

    Public Function bind_lobCmd(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDeptClientLob", connection)
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

    Public Function bind_clientCmd(ByVal deptid As String, ByVal clientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDeptClient", connection)
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

    Public Function bind_DeparmentCmd(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectCmdOnDept", connection)
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


    Public Function Insert_CmdRights(ByVal UserId As String, ByVal cmdId As String, ByVal View As String, ByVal run As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)

        connection.Open()

        comdepart = New SqlCommand("sp_InsertUpdateCommandRights", connection)
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

    Public Function bind_lobView(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDeptClientLob", connection)
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
    Public Function bind_clientView(ByVal deptid As String, ByVal clientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDeptClient", connection)
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
    Public Function bind_DeparmentView(ByVal deptid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sp_selectViewOnDept", connection)
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

    Public Function Insert_ViewRights(ByVal UserId As String, ByVal ViewId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal SaveAs As String, ByVal AssignedBy As String)


        connection.Open()
        comdepart = New SqlCommand("sp_InsertViewRights", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@userid", SqlDbType.VarChar, 50)
        comdepart.Parameters("@userid").Value = UserId

        comdepart.Parameters.Add("@viewId", SqlDbType.VarChar, 50)
        comdepart.Parameters("@viewId").Value = ViewId

        comdepart.Parameters.Add("@view", SqlDbType.VarChar, 10)
        comdepart.Parameters("@view").Value = View

        comdepart.Parameters.Add("@edit", SqlDbType.VarChar, 10)
        comdepart.Parameters("@edit").Value = Edit

        comdepart.Parameters.Add("@delete", SqlDbType.VarChar, 10)
        comdepart.Parameters("@delete").Value = Delete

        comdepart.Parameters.Add("@saveAs", SqlDbType.VarChar, 10)
        comdepart.Parameters("@saveAs").Value = SaveAs

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy
        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function

    Public Function Insert_TableRights(ByVal UserId As String, ByVal TableId As String, ByVal View As String, ByVal Edit As String, ByVal Delete As String, ByVal DeleteData As String, ByVal AddColumn As String, ByVal AssignedBy As String)

        connection.Open()

        comdepart = New SqlCommand("sp_InsertTableRights", connection)
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

        comdepart.Parameters.Add("@CurrDate", SqlDbType.DateTime)
        comdepart.Parameters("@CurrDate").Value = System.DateTime.Now.Date

        comdepart.Parameters.Add("@AssignedBy", SqlDbType.VarChar, 50)
        comdepart.Parameters("@AssignedBy").Value = AssignedBy
        comdepart.ExecuteNonQuery()

        connection.Close()

        Return 1
    End Function
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    '''''''''Ranjit Functions'''''''''''''''''''''''
    Public Function bind_usersDept(ByVal loggeduser As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_usersDept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = loggeduser
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function


    Public Function bind_userlocalDept(ByVal loggedAdmin As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_userforuserdept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = loggedAdmin
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function


    Public Function bind_departmentrepuser(ByVal deptid As String)
        ds1.Clear()

        comdepart = New SqlCommand("select_repdepartmentuser", connection)
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


    Public Function bindAdminLobOnDeptClient(ByVal adminId As String, ByVal deptId As String, ByVal cientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_AdminLOBs", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@adminid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@adminid").Value = adminId
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function


    Public Function binduserLobOnDeptClient(ByVal userId As String, ByVal deptId As String, ByVal cientId As String)
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select_userLOBs", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@userid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@userid").Value = userId
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptId
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@clientid").Value = cientId
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function


    Public Function bind_clientuserforusers(ByVal deptid As String, ByVal cientid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userclientforuser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function



    Public Function bind_clientrepforuser(ByVal deptid As String, ByVal cientid As String)
        ds1.Clear()
        comdepart = New SqlCommand("select_repclientforuser", connection)
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


    Public Function reportsofuserforuser(ByVal savedby As String)
        ds.Clear()
        comdepart = New SqlCommand("report_accordingtouserforuser", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@savedby", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@savedby").Value = savedby
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function

    Public Function bind_lobuserforuser(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds.Clear()
        comdepart = New SqlCommand("select_userlobforuser", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters.Add("@cientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@cientid").Value = cientid
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds


    End Function



    Public Function bind_lobrepforuser(ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("select_replobforuser", connection)
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
    Public Function reportsofuser(ByVal savedby As String)
        ds.Clear()
        comdepart = New SqlCommand("report_accordingtouser", connection)
        comdepart.CommandType = CommandType.StoredProcedure

        comdepart.Parameters.Add("@savedby", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@savedby").Value = savedby
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds

    End Function


    Public Function delete_html(ByVal repname As String)
        ds1.Clear()
        comdepart = New SqlCommand("deletefromhtml", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@reportname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@reportname").Value = repname
        connection.Open()
        'da.SelectCommand = comdepart
        'da.Fill(ds1)

        comdepart.ExecuteNonQuery()
        connection.Close()
        Return "1"

    End Function
    Public Function bind_htmlreport(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("sel_htmlreport", connection)
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
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function
    Public Function bind_htmlreportforadmin(ByVal userid As String, ByVal deptid As String, ByVal cientid As String, ByVal lobid As String)
        ds1.Clear()
        comdepart = New SqlCommand("dataanalysisHtmlReportForAdmin", connection)
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
        da.Fill(ds1)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds1

    End Function

    Public Function bind_columnOnTable(ByVal tablename As String)
        ds.Clear()
        Dim columns As String = ""

        comdepart = New SqlCommand("SELECT VisibleColumn  from warslobtablemaster where tableid=" + tablename, connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read
            columns = dr("VisibleColumn").ToString

        End While
        connection.Close()
        Return columns
    End Function




    Public Function bindbucketvalue(ByVal bucketname As String, ByVal columnname As String)
        ds.Clear()
        Dim condition As String = ""
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select startdate, enddate from bucket where bucketname=@bucname", connection)
        comdepart.Parameters.Add("@bucname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@bucname").Value = bucketname
        connection.Open()

        dr = comdepart.ExecuteReader
        While dr.Read
            condition = "'" & dr("startdate").ToString & "'"
            condition = condition & " " & "and" & " " & "convert (datetime ," & columnname & ")" & " " & "<" & " " & "'" & dr("enddate").ToString & "'"
        End While

        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return condition
    End Function






    Function get_value_forbucket(ByVal tablename As String, ByVal column As String)
        Dim data As New DataSet
        comdepart = New SqlCommand("select distinct(" + column + ") from " + tablename + "", connection)
        connection.Open()
        Dim dataadapter As New SqlDataAdapter
        Try


            dataadapter.SelectCommand = comdepart
            dataadapter.Fill(data)
        Catch ex As Exception
            Dim strmessage As String = ""
            strmessage = Replace(ex.Message.ToString, "'", "")
            strmessage = Replace(strmessage, vbCrLf, " ")
            Dim strs As String = "3"
            Return (strs & "," & strmessage)
        End Try
        connection.Close()
        Return data
    End Function





    Public Function trd_createtable(ByVal tablecolumns As String, ByVal tabname As String)
        Dim b As Boolean
        comdepart = New SqlCommand("select name from sysobjects where xtype='u'", connection)
        connection.Open()
        dr = comdepart.ExecuteReader
        While dr.Read()
            If dr("name") = "trendingandsuggessionstable" Then
                b = False
                Exit While

            Else
                b = True
            End If
        End While
        dr.Close()
        connection.Close()

        If b = False Then


            comdepart = New SqlCommand("drop table trendingandsuggessionstable", connection)

            connection.Open()
            comdepart.ExecuteNonQuery()
            connection.Close()
        End If
        ds.Clear()
        Try


            comdepart = New SqlCommand("select Identity(int, 1,1) as RecordId, " + tablecolumns + " into trendingandsuggessionstable from " + tabname + " ", connection)

            connection.Open()
            comdepart.ExecuteNonQuery()
        Catch ex As Exception
            Return ex
        End Try

        connection.Close()
        Return 1

    End Function




















    Public Function bucket_insert(ByVal stdate As String, ByVal eddate As String, ByVal bkname As String)
        ds1.Clear()
        comdepart = New SqlCommand("insertbucket", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@stdate", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@stdate").Value = stdate
        comdepart.Parameters.Add("@enddate", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@enddate").Value = eddate
        comdepart.Parameters.Add("@bkname", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@bkname").Value = bkname
        connection.Open()
        comdepart.ExecuteNonQuery()
        connection.Close()
        Return 1
    End Function
    Public Function check_bucket(ByVal bucketname As String)
        ds1.Clear()
        comdepart = New SqlCommand("bucketchk", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        'comdepart.Parameters.Add("@nukname", SqlDbType.NVarChar, 50)
        'comdepart.Parameters("@nukname").Value = bucketname
        connection.Open()
        Dim int As Integer
        dr = comdepart.ExecuteReader
        While dr.Read
            Dim str As String = ""
            str = dr("bucketname")
            If str = bucketname Then
                int = 1
                Exit While
            Else
                int = 2
            End If

        End While
        connection.Close()
        dr.Close()
        Return int

    End Function



    Public Function bindbucket()
        ds.Clear()
        da = New SqlDataAdapter
        comdepart = New SqlCommand("select * from bucket", connection)
        connection.Open()
        comdepart.ExecuteNonQuery()

        da.SelectCommand = comdepart
        da.Fill(ds)
        ' dr = comdepart.ExecuteReader
        connection.Close()
        Return ds
    End Function








    ''''''jitender function'''
   
    Public Function bind_tableOnDepartment(ByVal deptid As String) As DataSet
        ds.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDept", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds

    End Function
    'get table's name specified by department and client, 2

    Public Function bind_tableOnClient(ByVal deptid, ByVal clientid) As DataSet
        ds.Clear()
        'commented on date 17/10/2008
        comdepart = New SqlCommand("sp_selectTableOnDeptClient", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters("@clientid").Value = clientid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds
        '--------------------------------------



    End Function
    'get table's name specified by department, client and LOB, 3

    Public Function bind_tableOnLob(ByVal deptid, ByVal clientid, ByVal lobid) As DataSet
        ' commented on date 17/10/2008
        ds.Clear()
        comdepart = New SqlCommand("sp_selectTableOnDeptClientLob", connection)
        comdepart.CommandType = CommandType.StoredProcedure
        comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        comdepart.Parameters("@deptid").Value = deptid
        comdepart.Parameters("@clientid").Value = clientid
        comdepart.Parameters("@lobid").Value = lobid
        connection.Open()
        da.SelectCommand = comdepart
        da.Fill(ds)

        connection.Close()
        Return ds
        '---------------------------------------------------

       
    End Function
    ''''''''''''''''''''''''Table and Tools fuction''''''''''''''''''''''''

    Public Function bindtabondept(ByVal deptid, ByVal usertype, ByVal userid)

        'commented on date 17/10/2008
        'ds.Clear()
        'comdepart = New SqlCommand("sp_selectTableOnDept", connection)
        'comdepart.CommandType = CommandType.StoredProcedure
        'comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters("@deptid").Value = deptid
        'connection.Open()
        'da.SelectCommand = comdepart
        'da.Fill(ds)

        'connection.Close()
        'Return ds
        Dim dsgettab As New DataSet
        Dim cmdgettab As New SqlCommand

        If (usertype = 2) Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                .AddWithValue("@Deptid", deptid)
                .AddWithValue("@Clientid", "0")
                .AddWithValue("@LOBID", "0")
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                readerdata.Close()

                cmdgettab = New SqlCommand("select Tableid,LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0  order by TableName", connection)



                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()
            Else
                readerdata.Close()
                cmdgettab = New SqlCommand("select Tableid,LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and CreatedBy='" + userid + "'  order by TableName", connection)



                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()
                'GoTo outofspan

                Return dsgettab
            End If
        ElseIf (usertype = 1) Then
            'outofspan:
            connection.Close()
            'ranjit changed
            'cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and ( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes') order by TableName", connection)
            cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and ( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "') and Editable='Yes' and Importable='Yes') order by TableName", connection)

            Dim adpgettab As New SqlDataAdapter
            adpgettab.SelectCommand = cmdgettab
            connection.Open()
            adpgettab.Fill(dsgettab)
            connection.Close()
            cmdgettab.Dispose()
            Return dsgettab
            If connection.State = ConnectionState.Open Then

                connection.Close()
            End If

        End If


        Return dsgettab



    End Function

    Public Function bindtabonclient(ByVal deptid, ByVal clientid, ByVal usertype, ByVal userid)
        'ds.Clear()
        'commented on date 17/10/2008
        'comdepart = New SqlCommand("sp_selectTableOnDeptClient", connection)
        'comdepart.CommandType = CommandType.StoredProcedure
        'comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters("@deptid").Value = deptid
        'comdepart.Parameters("@clientid").Value = clientid
        'connection.Open()
        'da.SelectCommand = comdepart
        'da.Fill(ds)

        'connection.Close()
        ' Return ds
        '--------------------------------------

        Dim cmdgettab As New SqlCommand
        Dim dsgettab As New DataSet
        'Commentd as on date 27/08/2008 by Rohit Now Table are bind according to Rights Managment

        If (usertype = 2) Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                .AddWithValue("@Deptid", deptid)
                .AddWithValue("@Clientid", clientid)
                .AddWithValue("@LOBID", "0")
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                readerdata.Close()
                cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0  order by TableName", connection)
                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()
            Else
                'GoTo outofspan
                readerdata.Close()
                cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and CreatedBy='" + userid + "' order by TableName", connection)
                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()

                Return dsgettab
            End If
        ElseIf (usertype = 1) Then
            'outofspan:
            connection.Close()
            'cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes') order by TableName", connection)
            cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "') and Editable='Yes' and Importable='Yes') order by TableName", connection)

            Dim adpgettab As New SqlDataAdapter
            adpgettab.SelectCommand = cmdgettab
            connection.Open()
            adpgettab.Fill(dsgettab)
            connection.Close()
            cmdgettab.Dispose()
            Return dsgettab
            If connection.State = ConnectionState.Open Then

                connection.Close()
            End If
        End If
        '        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0  and Editable='Yes' and Importable='Yes' order by TableName", connection)


        Return dsgettab
    End Function



    Public Function bindtabonlob(ByVal deptid, ByVal clientid, ByVal lobid, ByVal usertype, ByVal userid)
        ' commented on date 17/10/2008
        'ds.Clear()
        'comdepart = New SqlCommand("sp_selectTableOnDeptClientLob", connection)
        'comdepart.CommandType = CommandType.StoredProcedure
        'comdepart.Parameters.Add("@deptid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters.Add("@clientid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters.Add("@lobid", SqlDbType.NVarChar, 50)
        'comdepart.Parameters("@deptid").Value = deptid
        'comdepart.Parameters("@clientid").Value = clientid
        'comdepart.Parameters("@lobid").Value = lobid
        'connection.Open()
        'da.SelectCommand = comdepart
        'da.Fill(ds)

        'connection.Close()
        'Return ds
        '---------------------------------------------------
        Dim dsgettab As New DataSet
        Dim cmdgettab As New SqlCommand
        'Commented As on Date 27/08/2008 By rohit As table are now bond with Rights

        If (usertype = 2) Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", userid)
                .AddWithValue("@Deptid", deptid)
                .AddWithValue("@Clientid", clientid)
                .AddWithValue("@LOBID", lobid)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader


            If readerdata.HasRows Then
                readerdata.Close()
                cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' order by TableName", connection)

                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()
                Return dsgettab
            Else
                readerdata.Close()
                cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and CreatedBy='" + userid + "' order by TableName", connection)

                Dim adpgettab As New SqlDataAdapter
                adpgettab.SelectCommand = cmdgettab

                adpgettab.Fill(dsgettab)
                connection.Close()
                cmdgettab.Dispose()
                Return dsgettab
                'GoTo outofspan
            End If
        ElseIf (usertype = 1) Then
            'outofspan:
            connection.Close()
            'cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes') order by TableName", connection)
            cmdgettab = New SqlCommand("select Tableid, LTrim(RTrim(TableName)) as TableName from warslobtablemaster where  LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and( createdby='" + userid + "' or TableId in (select TableID from idms_tablerights where Userid='" & userid & "' ) and Editable='Yes' and Importable='Yes') order by TableName", connection)

            Dim adpgettab As New SqlDataAdapter
            adpgettab.SelectCommand = cmdgettab
            connection.Open()
            adpgettab.Fill(dsgettab)
            connection.Close()
            cmdgettab.Dispose()
            Return dsgettab
            If connection.State = ConnectionState.Open Then

                connection.Close()
            End If
        End If
        'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "'  and Editable='Yes' and Importable='Yes' order by TableName", connection)

        Return dsgettab

    End Function
    ''''''''''''''''WRM ROHIT"""""""""""""""""""""""

    Public Function bindepttab(ByVal deptid, ByVal usertype, ByVal userid) As DataSet

        Dim cmdgettab As New SqlCommand
        '******************* Change on Date 27/08/2008 by Rohit as per Rights Mangment 
        'If ("usertype") = "user" Then
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & userid & "') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'Else
        '    cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'End If

        '*********************************** Date 1 Sep 2008 ********************************************

        If (usertype = 1) Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0 and TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId=0 and LOBId=0  and Editable='Yes' and Importable='Yes' order by TableName", connection)

        End If
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Return dsgettab
    End Function
    Public Function bindlob(ByVal deptid, ByVal clientid) As DataSet
        Dim cmdgetlob As New SqlCommand("select LTrim(RTrim(LOBName)) as LOB,autoid from warslobmaster where DeptId='" & deptid & "' and ClientId='" & clientid & "' order by LOBName", connection)
        Dim dsgetlob As New DataSet
        Dim adpgetlob As New SqlDataAdapter
        adpgetlob.SelectCommand = cmdgetlob
        connection.Open()
        adpgetlob.Fill(dsgetlob)
        connection.Close()
        cmdgetlob.Dispose()
        Return dsgetlob
    End Function
    Public Function bindclienttab(ByVal deptid, ByVal clientid, ByVal usertype, ByVal userid) As DataSet
        Dim cmdgettab As New SqlCommand

        'Commentd as on date 27/08/2008 by Rohit Now Table are bind according to Rights Managment
        If (usertype = 1) Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and Editable='Yes' and Importable='Yes' order by TableName", connection)
        End If
        '        cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0 and TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId=0  and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Return dsgettab
    End Function
    Public Function bindtable(ByVal deptid, ByVal clientid, ByVal lobid, ByVal usertype, ByVal userid) As DataSet
        Dim cmdgettab As New SqlCommand
        'Commented As on Date 27/08/2008 By rohit As table are now bond with Rights
        If (usertype = 1) Then
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and TableId in (select TableID from idms_tablerights where Userid='" & userid & "' and [view]='True') and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Else
            cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and Editable='Yes' and Importable='Yes' order by TableName", connection)
        End If
        'cmdgettab = New SqlCommand("select LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  DepartmentId='" & deptid & "' and ClientId='" & clientid & "' and LOBId='" & lobid & "'  and Editable='Yes' and Importable='Yes' order by TableName", connection)
        Dim dsgettab As New DataSet
        Dim adpgettab As New SqlDataAdapter
        adpgettab.SelectCommand = cmdgettab
        connection.Open()
        adpgettab.Fill(dsgettab)
        connection.Close()
        cmdgettab.Dispose()
        Return dsgettab
    End Function
    Public Function bindclient(ByVal deptid) As DataSet
        Dim cmdgetclient As New SqlCommand("select LTrim(RTrim(ClientName)) as ClientName,autoid from IDMSClient where DeptId='" & deptid & "' order by ClientName", connection)
        Dim dsgetclient As New DataSet
        Dim adpgetclient As New SqlDataAdapter
        adpgetclient.SelectCommand = cmdgetclient
        connection.Open()
        adpgetclient.Fill(dsgetclient)
        connection.Close()
        cmdgetclient.Dispose()
        'Dim str As String
        'Dim strname As String
        'Dim strvalue As String
        'Dim i As Integer
        'For i = 0 To dsgetclient.Tables(0).Rows.Count - 1
        '    If str = "" Then
        '        str = dsgetclient.Tables(0).Rows(i).Item("autoid") & "#" & dsgetclient.Tables(0).Rows(i).Item("ClientName")
        '    Else
        '        str = str & "$" & dsgetclient.Tables(0).Rows(i).Item("autoid") & "#" & dsgetclient.Tables(0).Rows(i).Item("ClientName")
        '    End If
        'Next
        'If str = "" Then
        '    str = "N"
        'End If
        Return dsgetclient
    End Function

    ''' <summary>
    ''' Ekta(Datamanager )to import excel file
    ''' </summary>
    ''' <param name="sSQLTable"></param>
    ''' <param name="sExcelFileName"></param>
    ''' <param name="sWorkbook"></param>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function import_data(ByVal sSQLTable As String, ByVal sExcelFileName As String, ByVal sWorkbook As String, ByVal path As String)
        Dim res
        Dim OleDbConn As OleDbConnection
        Try

            'Create our connection strings
            Dim sExcelConnectionString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & path & ";Extended Properties=""Excel 8.0;HDR=YES"""
            'Dim sSqlConnectionString As String = WebConfigurationManager.ConnectionStrings("connectionString").ToString

            'Execute a query to erase any previous data from our destination table
            ' Dim sClearSQL = "DELETE FROM " & sSQLTable
            'Dim SqlConn As SqlConnection = New SqlConnection(connection)
            ' Dim SqlCmd As SqlCommand = New SqlCommand(sClearSQL, connection)
            'connection.Open()
            'SqlCmd.ExecuteNonQuery()
            'connection.Close()


            'Series of commands to bulk copy data from the excel file into our SQL table
            'sWorkbook




            OleDbConn = New OleDbConnection(sExcelConnectionString)

            'count  no of rows to be insert in sql server table from xsl
            Dim count As Integer
            Dim countq As String
            countq = "SELECT count(*) FROM " & sWorkbook
            Dim OleDbCmd1 As OleDbCommand = New OleDbCommand(countq, OleDbConn)
            Dim datard As OleDbDataReader
            OleDbConn.Open()
            datard = OleDbCmd1.ExecuteReader()
            datard.Read()

            If datard.HasRows Then
                count = Convert.ToInt32(datard.GetValue(0))


            End If
            datard.Close()
            OleDbConn.Close()

            'end of logic count inserted rows


            Dim strSql As String
            strSql = "SELECT * FROM " & sWorkbook
           
            Dim OleDbCmd As OleDbCommand = New OleDbCommand(strSql, OleDbConn)
            OleDbConn.Open()

            Dim dr As OleDbDataReader = OleDbCmd.ExecuteReader()
            Dim bulkCopy As SqlBulkCopy = New SqlBulkCopy(connection)
            connection.Open()
            bulkCopy.DestinationTableName = sSQLTable
            bulkCopy.WriteToServer(dr)



            OleDbConn.Close()
            connection.Close()
            ' Return dr
            res = Convert.ToString(count)
            Return res
        Catch ex As Exception
            res = "error :" & ex.Message
            OleDbConn.Close()
            connection.Close()
        End Try
        Return res
    End Function

End Class
