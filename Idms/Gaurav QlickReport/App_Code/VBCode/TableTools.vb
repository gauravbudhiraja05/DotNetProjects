Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class TableTools
    Dim conStr As String = AppSettings("connectionString")
    Dim con As New SqlConnection(conStr)
    Dim ds As New DataSet
    Dim da As SqlDataAdapter
    Dim cmd As SqlCommand
    Public Function GetAllUser()
        Dim tempDS As New DataSet
        Dim tempDa As New SqlDataAdapter
        'ds.Clear()
        Dim str As String
        str = "Select Distinct(UserID) from buddy"
        tempDa = New SqlDataAdapter(str, con)
        tempDa.Fill(tempDS)
        con.Close()
        Return tempDS
        tempDS.Dispose()
        tempDa.Dispose()
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
    Public Function BindTableName()
        ds.Clear()
        Dim str As String
        str = "Select Distinct(TableName) as TableName from logtabletool_track"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
        ds.Dispose()
        da.Dispose()
    End Function





    Public Function AllTableNameForUser(ByVal UserId As String)
        ds.Clear()
        Dim str As String
        str = "Select Distinct(TableName) as TableName from logtabletool_track where CreatedBy= '" + UserId + "'"
        da = New SqlDataAdapter(str, con)
        da.Fill(ds)
        con.Close()
        Return ds
        ds.Dispose()
        da.Dispose()
    End Function
    Public Function AllTableNameForAdmin(ByVal UserId As String)
        Dim tempDS2 As New DataSet
        Dim tempDa2 As New SqlDataAdapter
        Dim tempCmd2 As SqlCommand

        tempCmd2 = New SqlCommand("sp_AllTableNameForAdmin", con)
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
End Class
