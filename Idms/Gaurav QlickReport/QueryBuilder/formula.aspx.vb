Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class QueryBuilder_formula
    Inherits System.Web.UI.Page
    Public datatablestring
    Public datafieldstring
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'tr1.Visible = False
        'tr2.Visible = False
        Try

        
            Dim tablename As String
            Dim rdrModules As SqlDataReader
            'Dim strQryMod As String = "select DISTINCT(" & Trim(Request("data")) & ") from " & Request("table")
            Dim strQryMod As String = "select name from syscolumns where object_name(id)='" & Request("table") + "'"
            Dim cmdMod As New SqlCommand(strQryMod, connection)
            connection.Open()
            rdrModules = cmdMod.ExecuteReader
            While rdrModules.Read
                'tablename = rdrModules("" & Trim(Request("data")) & "")
                tablename = rdrModules("name")
                datatablestring = datatablestring & "<option value='" & tablename & "'> " & tablename & " </option>"
            End While
            connection.Close()
        Catch ex As Exception

        End Try

    End Sub
End Class