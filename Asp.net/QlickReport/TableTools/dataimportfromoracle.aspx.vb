Imports System.Data
Imports System.Data.OleDb
Imports Oracle.DataAccess.Client
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class TableTools_dataimportfromoracle
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim con As OleDbConnection
    Dim cmd As OleDbCommand
    Dim dr As OleDbDataReader
    Dim dr2 As SqlDataReader
    Dim desttab As String
    Dim cmdnew As SqlCommand
    Dim rdrnew As SqlDataReader
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Me.cbodept.Attributes.Add("onchange", "javascript:getclient();")
        Me.cboclient.Attributes.Add("onchange", "javascript:getlob();")
        connection.Open()
        cmdnew = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdrnew = cmdnew.ExecuteReader
        If rdrnew.Read Then
            Dim producttype As String = rdrnew("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                rdrnew.Close()
                Dim cmd As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    Dim val1 As String
                    Dim val2 As String
                    Dim val3 As String
                    val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                    lbl6.Text = val1
                    lbl7.Text = val2
                    lbl8.Text = val3
                End If
            Else
                Me.spandisplay.Visible = False
                Me.ImportdataSin.Visible = True
            End If
        End If
        rdrnew.Close()
        cmdnew.Dispose()
        Dim typeofuser = Session("typeofuser")
        If (Me.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmdgetdept As New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", connection)
                Dim dsgetdept As New DataSet
                Dim adpgetdept As New SqlDataAdapter
                adpgetdept.SelectCommand = cmdgetdept
                adpgetdept.Fill(dsgetdept)
                cbodept.DataSource = dsgetdept
                cbodept.DataTextField = "DepartmentName"
                cbodept.DataValueField = "autoid"
                cbodept.DataBind()
                cbodept.Items.Insert(0, "--Select--")
            End If
        End If
        connection.Close()
    End Sub
    Protected Sub getdb_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles getdb.Click
        msg1.Visible = False
        msg2.Visible = False
        msg3.Visible = False
        If (servername.Text = "") Then
            msg1.Visible = True
        ElseIf (uid.Text = "") Then
            msg2.Visible = True
        ElseIf (pw.Text = "") Then
            msg3.Visible = True
        Else
            selecttb.Items.Clear()
            con = New OleDbConnection("Provider=OraOLEDB.Oracle;dbq=" + servername.Text + ":1521/XE;User Id='" + uid.Text + "';Password='" + pw.Text + "'")
            con.Open()
            cmd = New OleDbCommand("select * from tab  ", con)
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader()
            While (dr.Read())
                selecttb.Items.Add(dr("TNAME"))
            End While
            con.Close()
            con.Dispose()
        End If
    End Sub
    Protected Sub addtable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles addtable.Click
        Dim j As Integer = 0
        Dim count As Integer = 0
        Dim i As Integer = 0
        lbl1.Visible = False
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        Dim Alltextbox As TextBox() = {Me.tb0, Me.tb1, Me.tb2, Me.tb3, Me.tb4}
        For j = 0 To selecttb.Items.Count - 1
            If (selecttb.Items(j).Selected.Equals(True)) Then
                count = count + 1
            End If
        Next
        If (count <= 0) Then
            error1.Visible = True
        ElseIf (count > 5) Then
            error1.Visible = True
            error1.Text = "Plz select Max 5 Tables"
        Else
            error1.Visible = False
            i = 0
            For j = 0 To selecttb.Items.Count - 1
                If (selecttb.Items(j).Selected.Equals(True)) Then
                    Alltextbox(i).Text = selecttb.Items(j).Text
                    i = i + 1
                End If
            Next
            For i = 0 To 4
                If (i < count) Then
                    Alltextbox(i).Visible = True
                Else
                    Alltextbox(i).Visible = False
                End If
            Next
        End If
    End Sub
    Protected Sub Importdata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Importdata.Click
        Dim txt_arr As String() = {"", "", "", "", ""}
        Dim lst_arr As String() = {"", "", "", "", ""}
        Dim alltext As TextBox() = {Me.tb0, Me.tb1, Me.tb2, Me.tb3, Me.tb4}
        Dim i = 0
        Dim count = 0
        For j = 0 To selecttb.Items.Count - 1
            If (selecttb.Items(j).Selected.Equals(True)) Then
                lst_arr(i) = selecttb.Items(j).Text
                txt_arr(i) = alltext(i).Text

                con = New OleDbConnection("Provider=OraOLEDB.Oracle;dbq=" + servername.Text + ":1521/XE;User Id='" + uid.Text + "';Password='" + pw.Text + "'")
                Dim oConn As SqlConnection
                Try
                    con.Open()
                    Dim tab As String
                    tab = lst_arr(i).ToString()
                    Dim command As OleDbCommand = con.CreateCommand
                    Dim sql As String = "Select * From " + tab + " WHERE ROWNUM < 100"
                    command.CommandText = sql
                    Dim reader As OleDbDataReader = command.ExecuteReader

                    'CONVERT DATAREADER TO DATATABLE
                    Dim dra As DataUtils.DataReaderAdapter = New DataUtils.DataReaderAdapter
                    Dim dt As DataTable = New DataTable
                    dra.FillFromReader(dt, reader)
                    reader.Close()

                    'Create the SQL Server Table
                    'oConn = New SqlConnection("data source=.;initial catalog=QlickReport;integrated security=sspi")
                    'oConn.Open()
                    connection.Open()
                    desttab = txt_arr(i).ToString()
                    Dim sqlTableCreator As SQLCreateTable = New SQLCreateTable
                    sqlTableCreator.Connection = connection
                    sqlTableCreator.DestinationTableName = desttab
                    'sqlTableCreator.DropTableIfExists = cb1.Checked
                    sqlTableCreator.CreateFromDataTable(dt)


                    'INSERT INTO SQL SERVER DATABASE
                    Dim s As SqlBulkCopy = New SqlBulkCopy(connection)
                    Try
                        s.DestinationTableName = desttab
                        s.NotifyAfter = 10000
                        AddHandler s.SqlRowsCopied, AddressOf s_SqlRowsCopied
                        s.WriteToServer(dt)
                        s.Close()
                    Finally
                        CType(s, IDisposable).Dispose()
                    End Try

                    i = i + 1
                    count = count + 1
                Catch ex As Exception
                    MsgBox(ex.ToString)
                Finally
                    'oConn.Close()
                    con.Close()

                    'CHANGES BY MOHIT TYAGI ON 4-AUGUST-2012
                    Dim connew As SqlConnection
                    Dim colname As String = ""
                    'connew = New SqlConnection("data source=.;initial catalog=QlickReport;integrated security=sspi")
                    Dim cmdnew = New SqlCommand("SELECT COLUMN_NAME 'All_Columns' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + desttab + "'", connection)
                    Dim drnew As SqlDataReader
                    connection.Close()
                    connection.Open()
                    drnew = cmdnew.ExecuteReader()
                    While (drnew.Read())
                        If (colname = "") Then
                            colname = drnew("All_Columns").ToString()
                        Else
                            colname = colname + "," + drnew("All_Columns").ToString()
                        End If
                    End While
                    drnew.Close()
                    'connew.Close()

                    'ENTRY IN WARSLOBTABLEMASTER OF EACH NEWLY IMPORTED TABLE // CHANGES BY MOHIT TYAGI ON 4-AUG-2012
                    Dim cmdin As New SqlCommand
                    cmdin.CommandType = CommandType.StoredProcedure
                    cmdin.CommandText = "insert_WARSLOBTableMaster"
                    connection.Close()
                    connection.Open()
                    cmdin.Connection = connection
                    With cmdin.Parameters
                        If cbolob.SelectedValue = "" Or cbolob.SelectedValue = "--Select--" Then
                            .AddWithValue("@LobId", "0")
                        Else
                            .AddWithValue("@LobId", cbolob.SelectedValue)
                        End If
                        .AddWithValue("@TableName", desttab)
                        .AddWithValue("@CreatedOn", System.DateTime.Today)
                        .AddWithValue("@CreatedBy", Session("userid"))
                        .AddWithValue("@LastModified", System.DateTime.Today)
                        .AddWithValue("@LastModifiedBy", Session("userid"))
                        .AddWithValue("@currdate", System.DateTime.Today)
                        .AddWithValue("@visiblecolumn", colname)
                        .AddWithValue("@DepartmentId", cbodept.SelectedValue)
                        If cboclient.SelectedValue = "--Select--" Then
                            .AddWithValue("@ClientId", "0")
                        Else
                            .AddWithValue("@ClientId", cboclient.SelectedValue)
                        End If
                        .AddWithValue("@Local", "NonLocal")
                        'End If
                        .AddWithValue("@primcol", "Null")
                        .AddWithValue("@constraintname", "null")
                    End With
                    cmdin.ExecuteNonQuery()
                    connection.Close()
                    cmdin.Dispose()

                End Try
            End If
        Next
        error1.Visible = True
        error1.Text = "Data Import to Sql Server Successfully"
        For i = 0 To 4
            alltext(i).Text = ""
            alltext(i).Visible = False
        Next
        lbl1.Visible = False
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        'ConnectAndQuery()
    End Sub
    Private Sub ConnectAndQuery()

    End Sub
    Private Sub CopyData(ByVal sourceTable As DataTable, ByVal destConnection As SqlConnection)

    End Sub
    Private Sub s_SqlRowsCopied(ByVal sender As Object, ByVal e As SqlRowsCopiedEventArgs)
        MsgBox("Copied " & e.RowsCopied & " rows, OK")
    End Sub
    Public Function GetTable(ByVal _reader As System.Data.OracleClient.OracleDataReader) As System.Data.DataTable
        Dim _table As System.Data.DataTable = _reader.GetSchemaTable
        Dim _dt As System.Data.DataTable = New System.Data.DataTable
        Dim _dc As System.Data.DataColumn
        Dim _row As System.Data.DataRow
        Dim _al As System.Collections.ArrayList = New System.Collections.ArrayList
        Dim i As Integer = 0
        While i < _table.Rows.Count
            _dc = New System.Data.DataColumn
            If Not _dt.Columns.Contains(_table.Rows(i)("ColumnName").ToString) Then
                _dc.ColumnName = _table.Rows(i)("ColumnName").ToString
                _dc.Unique = Convert.ToBoolean(_table.Rows(i)("IsUnique"))
                _dc.AllowDBNull = Convert.ToBoolean(_table.Rows(i)("AllowDBNull"))
                _dc.ReadOnly = Convert.ToBoolean(_table.Rows(i)("IsReadOnly"))
                _al.Add(_dc.ColumnName)
                _dt.Columns.Add(_dc)
            End If
            System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
        End While
        While _reader.Read
            _row = _dt.NewRow
            i = 0
            While i < _al.Count
                _row(CType(_al(i), System.String)) = _reader(CType(_al(i), System.String))
                System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            _dt.Rows.Add(_row)
        End While
        Return _dt
    End Function

    Protected Sub Chkexist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Chkexist.Click
        Dim txt_arr As String() = {"", "", "", "", ""}
        Dim lst_arr As String() = {"", "", "", "", ""}
        Dim k As Integer = 0
        Dim alltext As TextBox() = {Me.tb0, Me.tb1, Me.tb2, Me.tb3, Me.tb4}
        tb0.Enabled = True
        tb1.Enabled = True
        tb2.Enabled = True
        tb3.Enabled = True
        tb4.Enabled = True
        lbl1.Visible = False
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        Dim count1 As Integer = 0
        Dim cmd As SqlCommand
        Dim dt As DataTable = New DataTable()
        'Dim txt As String = TextBox1.Text
        Dim i = 0
        'Dim con As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        'con.Open()
        connection.Open()
        cmd = New SqlCommand("select COUNT(*) from sys.tables", connection)
        Dim count = Convert.ToInt32(cmd.ExecuteScalar())
        Dim str_arr(count) As String
        cmd = New SqlCommand("select name from sys.Tables", connection)
        'conection.Close()
        'con.Open()
        dr2 = cmd.ExecuteReader()
        Dim tvalue As String = ""
        Dim t1value As String = ""
        Dim t2value As String = ""
        Dim t3value As String = ""
        Dim t4value As String = ""
        For j = 0 To selecttb.Items.Count - 1
            If (selecttb.Items(j).Selected.Equals(True)) Then
                count1 = count1 + 1
            End If
        Next
        Dim tname As String = ""
        If (tb0.Visible.Equals(True)) Then
            tvalue = tb0.Text
        End If
        If (tb1.Visible.Equals(True)) Then
            t1value = tb1.Text
        End If
        If (tb2.Visible.Equals(True)) Then
            t2value = tb2.Text
        End If
        If (tb3.Visible.Equals(True)) Then
            t3value = tb3.Text
        End If
        If (tb4.Visible.Equals(True)) Then
            t4value = tb4.Text
        End If
        While (dr2.Read())
            tname = dr2("name").ToString()
            If (tvalue.Equals(tname)) Then
                lbl1.Visible = True
            End If
            If (t1value.Equals(tname)) Then
                lbl2.Visible = True
            End If
            If (t2value.Equals(tname)) Then
                lbl3.Visible = True
            End If
            If (t3value.Equals(tname)) Then
                lbl4.Visible = True
            End If
            If (t4value.Equals(tname)) Then
                lbl5.Visible = True
            End If
        End While
    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub

    Protected Sub cbodept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbodept.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (cbodept.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please select Level 1")
        End If
        'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        'connew.Open()
        connection.Open()
        cmdnew = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + cbodept.SelectedValue + "'", connection)
        drnew = cmdnew.ExecuteReader()
        cboclient.DataSource = drnew
        cboclient.DataTextField = "ClientName"
        cboclient.DataValueField = "autoid"
        cboclient.DataBind()
        cboclient.Items.Insert(0, "--Select--")
        connection.Close()
        drnew.Close()
        cmdnew.Dispose()
    End Sub

    Protected Sub cboclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboclient.SelectedIndexChanged
        Dim cmdnew As SqlCommand
        Dim drnew As SqlDataReader
        If (cbodept.SelectedValue = "--Select--") Then
            aspnet_msgbox("Please select Level 2")
        End If
        'Dim connew As SqlConnection = New SqlConnection("Data Source=.; Initial Catalog=QlickReport;Integrated Security=true")
        'connew.Open()
        connection.Open()
        cmdnew = New SqlCommand("select * from WARSLobMaster where deptid='" + cbodept.SelectedValue + "' and  clientid= '" + cboclient.SelectedValue + "'", connection)
        drnew = cmdnew.ExecuteReader()
        cbolob.DataSource = drnew
        cbolob.DataTextField = "LOBName"
        cbolob.DataValueField = "autoid"
        cbolob.DataBind()
        cbolob.Items.Insert(0, "--Select--")
        connection.Close()
        drnew.Close()
        cmdnew.Dispose()
    End Sub
    Protected Sub ImportdataSin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ImportdataSin.Click
        Dim txt_arr As String() = {"", "", "", "", ""}
        Dim lst_arr As String() = {"", "", "", "", ""}
        Dim alltext As TextBox() = {Me.tb0, Me.tb1, Me.tb2, Me.tb3, Me.tb4}
        Dim i = 0
        Dim count = 0
        For j = 0 To selecttb.Items.Count - 1
            If (selecttb.Items(j).Selected.Equals(True)) Then
                lst_arr(i) = selecttb.Items(j).Text
                txt_arr(i) = alltext(i).Text

                con = New OleDbConnection("Provider=OraOLEDB.Oracle;dbq=" + servername.Text + ":1521/XE;User Id='" + uid.Text + "';Password='" + pw.Text + "'")
                Dim oConn As SqlConnection
                Try
                    con.Open()
                    Dim tab As String
                    tab = lst_arr(i).ToString()
                    Dim command As OleDbCommand = con.CreateCommand
                    Dim sql As String = "Select * From " + tab + " WHERE ROWNUM < 100"
                    command.CommandText = sql
                    Dim reader As OleDbDataReader = command.ExecuteReader

                    'CONVERT DATAREADER TO DATATABLE
                    Dim dra As DataUtils.DataReaderAdapter = New DataUtils.DataReaderAdapter
                    Dim dt As DataTable = New DataTable
                    dra.FillFromReader(dt, reader)
                    reader.Close()

                    'Create the SQL Server Table
                    'oConn = New SqlConnection("data source=.;initial catalog=QlickReport;integrated security=sspi")
                    'oConn.Open()
                    connection.Open()
                    desttab = txt_arr(i).ToString()
                    Dim sqlTableCreator As SQLCreateTable = New SQLCreateTable
                    sqlTableCreator.Connection = connection
                    sqlTableCreator.DestinationTableName = desttab
                    'sqlTableCreator.DropTableIfExists = cb1.Checked
                    sqlTableCreator.CreateFromDataTable(dt)


                    'INSERT INTO SQL SERVER DATABASE
                    Dim s As SqlBulkCopy = New SqlBulkCopy(connection)
                    Try
                        s.DestinationTableName = desttab
                        s.NotifyAfter = 10000
                        AddHandler s.SqlRowsCopied, AddressOf s_SqlRowsCopied
                        s.WriteToServer(dt)
                        s.Close()
                    Finally
                        CType(s, IDisposable).Dispose()
                    End Try

                    i = i + 1
                    count = count + 1
                Catch ex As Exception
                    MsgBox(ex.ToString)
                Finally
                    connection.Close()
                    con.Close()

                    'CHANGES BY MOHIT TYAGI ON 4-AUGUST-2012
                    Dim connew As SqlConnection
                    Dim colname As String = ""
                    'connew = New SqlConnection("data source=.;initial catalog=QlickReport;integrated security=sspi")
                    connection.Open()
                    Dim cmdnew = New SqlCommand("SELECT COLUMN_NAME 'All_Columns' FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='" + desttab + "'", connection)
                    Dim drnew As SqlDataReader
                    'connection.Open()
                    drnew = cmdnew.ExecuteReader()
                    While (drnew.Read())
                        If (colname = "") Then
                            colname = drnew("All_Columns").ToString()
                        Else
                            colname = colname + "," + drnew("All_Columns").ToString()
                        End If
                    End While
                    drnew.Close()
                    connection.Close()

                    'ENTRY IN WARSLOBTABLEMASTER OF EACH NEWLY IMPORTED TABLE // CHANGES BY MOHIT TYAGI ON 4-AUG-2012
                    Dim cmdin As New SqlCommand
                    cmdin.CommandType = CommandType.StoredProcedure
                    cmdin.CommandText = "insert_WARSLOBTableMaster"
                    connection.Open()
                    cmdin.Connection = connection
                    With cmdin.Parameters
                        .AddWithValue("@LobId", "0")
                        .AddWithValue("@TableName", desttab)
                        .AddWithValue("@CreatedOn", System.DateTime.Today)
                        .AddWithValue("@CreatedBy", Session("userid"))
                        .AddWithValue("@LastModified", System.DateTime.Today)
                        .AddWithValue("@LastModifiedBy", Session("userid"))
                        .AddWithValue("@currdate", System.DateTime.Today)
                        .AddWithValue("@visiblecolumn", colname)
                        .AddWithValue("@DepartmentId", "60")
                        .AddWithValue("@ClientId", "0")
                        .AddWithValue("@Local", "NonLocal")
                        .AddWithValue("@primcol", "Null")
                        .AddWithValue("@constraintname", "null")
                    End With
                    cmdin.ExecuteNonQuery()
                    connection.Close()
                    cmdin.Dispose()
                End Try
            End If
        Next
        error1.Visible = True
        error1.Text = "Data Import to Sql Server Successfully"
        For i = 0 To 4
            alltext(i).Text = ""
            alltext(i).Visible = False
        Next
        lbl1.Visible = False
        lbl2.Visible = False
        lbl3.Visible = False
        lbl4.Visible = False
        lbl5.Visible = False
        'ConnectAndQuery()
    End Sub
End Class
