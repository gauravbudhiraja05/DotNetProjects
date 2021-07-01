Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Partial Class QueryBuilder_where
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Public strData, strData1

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Put user code to initialize the page here
        strData1 = "<option > Select </option>"
        Dim TableName = Request("Table")
        Dim ColumnName = Request("data")
        Dim rdData As SqlDataReader
        Dim cmdPrgms As New SqlCommand
        Dim strUser As String = Session("UserName")
        Dim strQryMod As String = "select distinct(isnull(cast(" & ColumnName & " as varchar),'')) ColumnName from " & TableName & " order by ColumnName"
        Dim cmdMod As New SqlCommand(strQryMod, connection)
        connection.Open()
        rdData = cmdMod.ExecuteReader
        While rdData.Read
            ColumnName = rdData("ColumnName")
            strData = strData & "<option value='" & ColumnName & "'> " & ColumnName & " </option>"
        End While
        connection.Close()
        strData1 = strData1 & strData
    End Sub
End Class


