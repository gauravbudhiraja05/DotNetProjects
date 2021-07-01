Imports System.Data
Imports System.Data.SqlClient

Public Class SQLCreateTable

    Private _connection As SqlConnection

    Public Property Connection() As SqlConnection
        Get
            Return _connection
        End Get
        Set(ByVal value As SqlConnection)
            _connection = value
        End Set
    End Property
    Private _transaction As SqlTransaction

    Public Property Transaction() As SqlTransaction
        Get
            Return _transaction
        End Get
        Set(ByVal value As SqlTransaction)
            _transaction = value
        End Set
    End Property
    Private _tableName As String

    Public Property DestinationTableName() As String
        Get
            Return _tableName
        End Get
        Set(ByVal value As String)
            _tableName = value
        End Set
    End Property

    Private _dropTableIfExist As Boolean = False
    Public Property DropTableIfExists() As Boolean
        Get
            Return _dropTableIfExist
        End Get
        Set(ByVal value As Boolean)
            _dropTableIfExist = value
        End Set
    End Property

    Public Sub New()
    End Sub

    Public Sub New(ByVal connection As SqlConnection)
        MyClass.New(connection, Nothing)
    End Sub

    Public Sub New(ByVal connection As SqlConnection, ByVal transaction As SqlTransaction)
        _connection = connection
        _transaction = transaction
    End Sub
    Public Function DropTable(ByVal tablename As String) As Object
        Dim sql As String = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" & TableName & "]') AND type in (N'U')) DROP Table [dbo].[" & TableName & "]"
        Dim cmd As SqlCommand
        If Not (_transaction Is Nothing) AndAlso Not (_transaction.Connection Is Nothing) Then
            cmd = New SqlCommand(sql, _connection, _transaction)
        Else
            cmd = New SqlCommand(sql, _connection)
        End If
        'Console.WriteLine("DropTable: " & sql)
        Return cmd.ExecuteNonQuery
    End Function
    Public Function Create(ByVal schema As DataTable) As Object
        Return Create(schema, 0)
    End Function

    Public Function Create(ByVal schema As DataTable, ByVal numKeys As Integer) As Object
        Dim primaryKeys(0) As Integer
        Dim i As Integer = 0
        While i < numKeys
            primaryKeys(i) = i
            System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        Return Create(schema, primaryKeys)
    End Function

    Public Function Create(ByVal schema As DataTable, ByVal primaryKeys As Integer()) As Object

        If DropTableIfExists Then
            DropTable(DestinationTableName)
        End If
        Dim sql As String = GetCreateSQL(_tableName, schema, primaryKeys)
        Dim cmd As SqlCommand
        If Not (_transaction Is Nothing) AndAlso Not (_transaction.Connection Is Nothing) Then
            cmd = New SqlCommand(sql, _connection, _transaction)
        Else
            cmd = New SqlCommand(sql, _connection)
        End If
        Return cmd.ExecuteNonQuery
    End Function

    Public Function CreateFromDataTable(ByVal table As DataTable) As Object

        If DropTableIfExists Then
            DropTable(DestinationTableName)
        End If

        Dim sql As String = GetCreateFromDataTableSQL(_tableName, table)
        Dim cmd As SqlCommand
        If Not (_transaction Is Nothing) AndAlso Not (_transaction.Connection Is Nothing) Then
            cmd = New SqlCommand(sql, _connection, _transaction)
        Else
            cmd = New SqlCommand(sql, _connection)
        End If
        Return cmd.ExecuteNonQuery
    End Function


    Public Shared Function GetCreateSQL(ByVal tableName As String, ByVal schema As DataTable, ByVal primaryKeys As Integer()) As String
        Dim sql As String = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" & tableName & "]') AND type in (N'U')) CREATE TABLE " & tableName & " (" & Microsoft.VisualBasic.Chr(10) & ""
        For Each column As DataRow In schema.Rows
            If Not (schema.Columns.Contains("IsHidden") AndAlso CType(column("IsHidden"), Boolean)) Then
                sql += column("ColumnName").ToString & " " & SQLGetType(column) & "," & Microsoft.VisualBasic.Chr(10) & ""
            End If
        Next
        sql = sql.TrimEnd(New Char() {","c, Microsoft.VisualBasic.Chr(10)}) & "" & Microsoft.VisualBasic.Chr(10) & ""
        Dim pk As String = "CONSTRAINT PK_" & tableName & " PRIMARY KEY CLUSTERED ("
        Dim hasKeys As Boolean = (Not (primaryKeys Is Nothing) AndAlso primaryKeys.Length > 0)
        If hasKeys Then
            For Each key As Integer In primaryKeys
                pk += schema.Rows(key)("ColumnName").ToString & ", "
            Next
        Else
            Dim keys As String = String.Join(", ", GetPrimaryKeys(schema))
            pk += keys
            hasKeys = keys.Length > 0
        End If
        pk = pk.TrimEnd(New Char() {","c, " "c, Microsoft.VisualBasic.Chr(10)}) & ")" & Microsoft.VisualBasic.Chr(10) & ""
        If hasKeys Then
            sql += pk
        End If
        sql += ")"
        Console.WriteLine("Primary Keys: " & primaryKeys.Length & " " & "GetCreateSQL: " & sql)
        Return sql
    End Function

    Public Shared Function GetCreateFromDataTableSQL(ByVal tableName As String, ByVal table As DataTable) As String
        Dim sql As String = "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" & tableName & "]') AND type in (N'U')) CREATE TABLE [" & tableName & "] (" & Microsoft.VisualBasic.Chr(10) & ""
        For Each column As DataColumn In table.Columns
            sql += "[" & column.ColumnName & "] " & SQLGetType(column) & "," & Microsoft.VisualBasic.Chr(10) & ""
        Next
        sql = sql.TrimEnd(New Char() {","c, Microsoft.VisualBasic.Chr(10)}) & "" & Microsoft.VisualBasic.Chr(10) & ""
        If table.PrimaryKey.Length > 0 Then
            sql += "CONSTRAINT [PK_" & tableName & "] PRIMARY KEY CLUSTERED ("
            For Each column As DataColumn In table.PrimaryKey
                sql += "[" & column.ColumnName & "],"
            Next
            sql = sql.TrimEnd(New Char() {","c}) & "))" & Microsoft.VisualBasic.Chr(10)

        End If
        sql += ")"
        Console.WriteLine("Primary Keys: " & table.PrimaryKey.Length & " " & "GetCreateFromDataTableSQL: " & sql)
        Return sql
    End Function

    Public Shared Function GetPrimaryKeys(ByVal schema As DataTable) As String()
        Dim Keys As New List(Of String)

        For Each column As DataRow In schema.Rows
            If schema.Columns.Contains("IsKey") AndAlso CType(column("IsKey"), Boolean) Then
                Keys.Add(column("ColumnName").ToString)
            End If
        Next
        Return Keys.ToArray
    End Function

    Public Shared Function SQLGetType(ByVal type As Object, ByVal columnSize As Integer, ByVal numericPrecision As Integer, ByVal numericScale As Integer) As String
        Select Case type.ToString
            Case "System.String"
                Return "VARCHAR(" & (Microsoft.VisualBasic.IIf((columnSize = -1), 255, columnSize)) & ")"
            Case "System.Decimal"
                If numericScale > 0 Then
                    Return "REAL"
                Else
                    If numericPrecision > 10 Then
                        Return "BIGINT"
                    Else
                        Return "INT"
                    End If
                End If
            Case "System.Double", "System.Single"
                Return "REAL"
            Case "System.Int64"
                Return "BIGINT"
            Case "System.Int16", "System.Int32"
                Return "INT"
            Case "System.DateTime"
                Return "DATETIME"
            Case Else
                Throw New Exception(type.ToString & " not implemented.")
        End Select
    End Function
    Public Shared Function SQLGetType(ByVal schemaRow As DataRow) As String
        Return SQLGetType(schemaRow("DataType"), Integer.Parse(schemaRow("ColumnSize").ToString), Integer.Parse(schemaRow("NumericPrecision").ToString), Integer.Parse(schemaRow("NumericScale").ToString))
    End Function

    Public Shared Function SQLGetType(ByVal column As DataColumn) As String
        Return SQLGetType(column.DataType, column.MaxLength, 10, 2)
    End Function
End Class
