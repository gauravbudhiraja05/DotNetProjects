Imports Microsoft.VisualBasic
Imports System
Imports System.data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Public Class superadmin_span
    Dim ds1 As New DataSet
    Dim dr As SqlDataReader
    Dim da As New SqlDataAdapter
    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)
    Dim comdepart As New SqlCommand


    Public Function bind_user_admin_Superadminspan(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String)
        comdepart = New SqlCommand("select_SuperAdminSpan", connection)
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
        da.Dispose()
        connection.Close()
        Return ds1
        'Dim dt As New DataTable
        'dt.Columns.Add("User Name")
        'Dim i As Integer
        'For i = 0 To ds1.Tables(0).Rows.Count - 1
        '    Dim dr As DataRow = dt.NewRow()
        '    dr("User Name") = ds1.Tables(0).Rows(i)("User Name").ToString()
        'Next
        'For i = 0 To ds1.Tables(1).Rows.Count - 1
        '    Dim dr As DataRow = dt.NewRow
        '    dr("User Name") = ds1.Tables(1).Rows(i)("User Name").ToString()
        'Next


    End Function
End Class
