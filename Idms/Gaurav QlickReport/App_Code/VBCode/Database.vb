Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class Database
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As SqlDataAdapter
    Dim cmd As SqlCommand
    Public Function GetAllUser()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(UserID) from Buddy"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function

    Public Function BindUserAdminWise(ByVal AdminID As String)
        Dim tempDS1 As New DataSet
        Dim tempDa1 As New SqlDataAdapter
        Dim tempCmd1 As SqlCommand

        tempCmd1 = New SqlCommand("sp_GetUserAdminWise", con)
        tempCmd1.CommandType = CommandType.StoredProcedure
        tempCmd1.Parameters.Add("@AdminID", SqlDbType.VarChar, 50)
        tempCmd1.Parameters("@AdminID").Value = AdminID
        con.Open()
        tempDa1.SelectCommand = tempCmd1
        tempDa1.Fill(tempDS1)
        con.Close()
        Return tempDS1
        tempDa1.Dispose()
        tempDS1.Dispose()
        tempCmd1.Dispose()
    End Function
    Public Function BindGraphReport()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(ReportName) as ReportName from logGraphDesigner"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
    Public Function BindReportForSuperAdmin()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(ReportName) as ReportName from logRptDesigner"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function
Public Function BindReportForAdmin()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(ReportName) as ReportName from logRptDesigner"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
        ds.Dispose()
        da.Dispose()
    End Function

   
    Public Function BindTableName()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(TableName) as TableName from LogTableTool_Track"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
    End Function

 Public Function AllReportNameForUser(ByVal UserId As String)
        ds.Clear()
        Dim str As String
        str = "Select Distinct(ReportName) as ReportName from logRptDesigner where userid= '" + UserId + "'"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
        ds.Dispose()
        da.Dispose()
    End Function

Public Function AllReportNameForAdmin(ByVal UserId As String)
        Dim tempDS2 As New DataSet
        Dim tempDa2 As New SqlDataAdapter
        Dim tempCmd2 As SqlCommand

        tempCmd2 = New SqlCommand("sp_AllReportNameForAdmin", con)
        tempCmd2.CommandType = CommandType.StoredProcedure
        tempCmd2.Parameters.Add("@userid", SqlDbType.VarChar, 200)
        tempCmd2.Parameters("@userid").Value = UserId
        con.Open()
        tempDa2.SelectCommand = tempCmd2
        tempDa2.Fill(tempDS2)
        con.Close()
        Return tempDS2
        tempDa2.Dispose()
        tempDS2.Dispose()
        tempCmd2.Dispose()
    End Function

    Public Function checkAdminSpan(ByVal checkForAdmin As String, ByVal checkToAdmin As String) As Boolean
        Dim tempDS2 As New DataSet
        Dim tempDa2 As New SqlDataAdapter
        Dim tempCmd2 As SqlCommand
        tempCmd2 = New SqlCommand("sp_checkAdminSpantest", con)
        tempCmd2.CommandType = CommandType.StoredProcedure
        tempCmd2.Parameters.Add("@loginAdmin", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@loginAdmin").Value = checkForAdmin
        tempCmd2.Parameters.Add("@spanAdmin", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@spanAdmin").Value = checkToAdmin
        con.Open()
        tempDa2.SelectCommand = tempCmd2
        tempDa2.Fill(tempDS2)
        con.Close()
        If tempDS2.Tables(0).Rows(0)(0) > 0 Then
            Return True
        Else
            Return False
        End If

        tempDa2.Dispose()
        tempDS2.Dispose()
        tempCmd2.Dispose()
    End Function
    Public Function checkUserExistanceForAdmin(ByVal checkForAdmin As String, ByVal checkToAdmin As String, ByVal checkUser As String) As Boolean
        Dim tempDS2 As New DataSet
        Dim tempDa2 As New SqlDataAdapter
        Dim tempCmd2 As SqlCommand
        tempCmd2 = New SqlCommand("sp_checkUserExistance", con)
        tempCmd2.CommandType = CommandType.StoredProcedure
        tempCmd2.Parameters.Add("@loginAdminId", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@loginAdminId").Value = checkForAdmin
        tempCmd2.Parameters.Add("@SpanAdminId", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@spanAdminId").Value = checkToAdmin
        tempCmd2.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@UserId").Value = checkUser
        con.Open()
        tempDa2.SelectCommand = tempCmd2
        tempDa2.Fill(tempDS2)
        If tempDS2.Tables(0).Rows(0)(0) > 0 Then
            Return True
        Else
            Return False
        End If
        con.Close()
        tempDa2.Dispose()
        tempDS2.Dispose()
        tempCmd2.Dispose()
    End Function
    Public Function checkUserExistanceForLoginAdmin(ByVal checkForAdmin As String, ByVal checkUser As String) As Boolean
        Dim tempDS2 As New DataSet
        Dim tempDa2 As New SqlDataAdapter
        Dim tempCmd2 As SqlCommand

        tempCmd2 = New SqlCommand("sp_checkUserExistanceForLoginAdmin", con)
        tempCmd2.CommandType = CommandType.StoredProcedure
        tempCmd2.Parameters.Add("@loginAdminId", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@loginAdminId").Value = checkForAdmin

        tempCmd2.Parameters.Add("@UserId", SqlDbType.VarChar, 50)
        tempCmd2.Parameters("@UserId").Value = checkUser
        con.Open()
        tempDa2.SelectCommand = tempCmd2
        tempDa2.Fill(tempDS2)
        con.Close()
        If tempDS2.Tables(0).Rows(0)(0) > 0 Then
            tempDa2.Dispose()
            tempDS2.Dispose()
            tempCmd2.Dispose()
            Return True
        Else
            tempDa2.Dispose()
            tempDS2.Dispose()
            tempCmd2.Dispose()
            Return False
        End If
    End Function
    Public Function checkLoginUser(ByVal DeptId As String, ByVal ClientId As String, ByVal LobID As String) As String
        ds.Clear()
        Dim str As String
        Dim strID As String = ""
        Dim count As Integer
        str = "Select Distinct UserID  from Buddy where DepartmentId = " + DeptId + " and ClientID = " + ClientId + " and LobId = " + LobID
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        For count = 0 To ds.Tables(0).Rows.Count - 1

            Dim strTempID As String = "'" + ds.Tables(0).Rows(count)("UserID").ToString + "',"
            strID = strID + strTempID
        Next
        ds.Dispose()
        da.Dispose()
        strID = strID.Remove(strID.LastIndexOf(","))
        strID = "(" + strID + ")"
        Return strID
    End Function
    ' made on 12-08
    Public Function checkAdminExistance(ByVal adminid As String) As Boolean
        ds.Clear()
        Dim str As String
        str = "Select Distinct(Adminid)  from masteradmin where adminid= '" + adminid + "'"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        If ds.Tables(0).Rows.Count > 0 Then
            ds.Dispose()
            Return True
        Else
            ds.Dispose()
            Return False
        End If

    End Function
    ' made on 12-08
    Public Function checkUserExistance(ByVal userid As String) As Boolean
        ds.Clear()
        Dim str As String
        str = "Select Distinct(userid)  from registration where userid= '" + userid + "'"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        If ds.Tables(0).Rows.Count > 0 Then
            ds.Dispose()
            Return True
        Else
            ds.Dispose()
            Return False
        End If

    End Function
'made on 29aug
    Public Function GetAllAdmin()
        Dim tempDS As New DataSet
        Dim tempDa As New SqlDataAdapter
        'ds.Clear()
        Dim str As String
        str = "Select Distinct(AdminID) from Masteradmin"
        tempDa = New SqlDataAdapter(str, con)
        tempDa.Fill(tempDS)
        con.Close()
        Return tempDS
        tempDS.Dispose()
        tempDa.Dispose()
    End Function

    Public Function GetAdminWithinSpan(ByVal adminid As String)
        Dim tempDS1 As New DataSet
        Dim tempDa1 As New SqlDataAdapter
        Dim tempCmd1 As SqlCommand
        tempCmd1 = New SqlCommand("sp_GetAdminWithinSpan", con)
        tempCmd1.CommandType = CommandType.StoredProcedure
        tempCmd1.Parameters.Add("@AdminID", SqlDbType.VarChar, 50)
        tempCmd1.Parameters("@AdminID").Value = adminid
        con.Open()
        tempDa1.SelectCommand = tempCmd1
        tempDa1.Fill(tempDS1)
        con.Close()
        Return tempDS1
        tempDa1.Dispose()
        tempDS1.Dispose()
        tempCmd1.Dispose()
    End Function
    Public Function bindAdminWithinSpan(ByVal adminId As String) As DataSet
        ds.Clear()
        cmd = New SqlCommand("sp_GetAdminWithinSpan", con)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.Add("@AdminID", SqlDbType.VarChar, 50)
        cmd.Parameters("@AdminID").Value = adminId
        con.Open()
        da = New SqlDataAdapter()
        da.SelectCommand = cmd
        da.Fill(ds)
        con.Close()
        da.Dispose()
        cmd.Dispose()
        Return ds
    End Function
    Public Function trackAccountForMaster(ByVal ActionBy As String, ByVal Action As String, ByVal Entity As String, ByVal EntityName As String, ByVal DeptId As Integer, ByVal ClientId As Integer, ByVal LobId As Integer) As Integer
        Try

            cmd = New SqlCommand("sp_LogAccountMaster", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@AutoId", SqlDbType.Int).Value = 0
            cmd.Parameters(0).Direction = ParameterDirection.Output
            cmd.Parameters.Add("@ActionBy", SqlDbType.VarChar, 100).Value = ActionBy
            cmd.Parameters.Add("@Action", SqlDbType.VarChar, 50).Value = Action
            cmd.Parameters.Add("@Date", SqlDbType.VarChar, 50).Value = System.DateTime.Now()
            cmd.Parameters.Add("@Entity", SqlDbType.VarChar, 50).Value = Entity
            cmd.Parameters.Add("@EntityName", SqlDbType.VarChar, 50).Value = EntityName
            cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50).Value = DeptId
            cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50).Value = ClientId
            cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50).Value = LobId
            con.Open()
            cmd.ExecuteNonQuery()
            Dim AutoId As Integer
            AutoId = cmd.Parameters(0).Value
            cmd.Dispose()
            con.Close()
            Return AutoId
        Catch ex As Exception

        End Try

    End Function

    Public Sub trackAccountForSlave(ByVal AutoId As Integer, ByVal Attribute As String, ByVal Value As String)
        Try

            cmd = New SqlCommand("sp_LogAccountSlave", con)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@RefId", SqlDbType.Int).Value = AutoId
            cmd.Parameters.Add("@Attribute", SqlDbType.VarChar, 100).Value = Attribute
            cmd.Parameters.Add("@Value", SqlDbType.VarChar, 50).Value = Value
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
        Catch ex As Exception

        End Try

    End Sub
End Class
