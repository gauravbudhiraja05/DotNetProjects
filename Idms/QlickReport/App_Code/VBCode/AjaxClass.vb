Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports System.data
Imports Microsoft.VisualBasic

Public Class AjaxClass
    Dim con As String = AppSettings("connectionString")
    Dim objsqlcon As New SqlConnection(con)
    Dim connection As New SqlConnection(con)
    Dim ds As New DataSet
    Dim da As New SqlDataAdapter
    <Ajax.AjaxMethod()> _
         Public Function GetTable(ByVal tab As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        Dim strArray As String() = tab.Split("$")
        For i = 0 To strArray.Length - 1
            If str = "" Then
                str = strArray(i)
            Else
                str = str + "||" + strArray(i)
            End If
        Next
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function GetTableFields(ByVal tab As String) As String
        Dim i As Integer = 0
        Dim str As String = ""
        Dim qStr As String = "Select * from " + tab + ""
        Try


            'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
            Dim objcmd As New SqlCommand
            objcmd = New SqlCommand(qStr, connection)
            connection.Open()
            Dim thisReader As SqlDataReader = objcmd.ExecuteReader()
            Dim j As Integer = 0
            For j = 0 To thisReader.FieldCount - 1
                If str = "" Then
                    str = tab + "$" + thisReader.GetName(j)
                Else
                    str = str + "~" + tab + "$" + thisReader.GetName(j)
                End If
            Next
        Catch ex As Exception
            str = "Null"
        End Try
        Return str
    End Function
    <Ajax.AjaxMethod()> _
    Public Function GetColumnValue(ByVal col As String) As DataSet
        Dim i As Integer = 0
        Dim str As String = ""
        Dim tbl As String()
        Dim ch As String = ""
        If (col.Contains(".")) Then
            ch = "."
        Else
            ch = "$"
        End If
        tbl = Split(col, ch)
        Dim qStr As String = "Select distinct(" + tbl(1) + ") as ColumnName from " + tbl(0) + ""
        'Dim objcmd As New SqlCommand("Select * from WARSlobtablemaster where DepartmentId='" & Request("Department") & "' and ClientId='" & Request("Client") & "' and LOBId='" & Request("LOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')", connection)
        Dim objcmd As New SqlCommand
        objcmd = New SqlCommand(qStr, connection)
        connection.Open()
        da.SelectCommand = objcmd
        da.Fill(ds)
        connection.Close()
        Return ds
    End Function
End Class
