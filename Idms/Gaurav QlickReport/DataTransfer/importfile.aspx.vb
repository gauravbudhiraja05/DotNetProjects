Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Partial Class DataTransfer_importfile
    Inherits System.Web.UI.Page
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connection3 As New SqlConnection(constr)
    Dim connection4 As New SqlConnection(constr)
    Dim connection5 As New SqlConnection(constr)
    Dim connection6 As New SqlConnection(constr)
    Dim connection7 As New SqlConnection(constr)
    Dim objds As New DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        Ajax.Utility.RegisterTypeForAjax(GetType(DataTransfer))

        cmdUpload.Attributes.Add("onclick", "validate();")

        If Session("userid") = "" Then
            Response.Redirect("~/SessionExpired.aspx")
        Else
            hfUserType.Value = Session("typeofuser").ToString()
            hfUserId.Value = Session("userid").ToString()

        End If

        

        If Me.IsPostBack = False Then
            '*******************Fill DepartMent*************************
            Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
            Dim da As New SqlDataAdapter
            da.SelectCommand = comdepart
            Dim ds As New DataSet
            connection.Open()
            da.Fill(ds)
            connection.Close()
            DepartmentName.DataTextField = "DepartmentName"
            DepartmentName.DataValueField = "autoid"
            DepartmentName.DataSource = ds
            DepartmentName.DataBind()
            DepartmentName.Items.Insert("0", "--Select--")
            Session("tablename") = ""
        End If
        txttablename.Text = HiddenField1.Value

        
    End Sub
    Function CSV2DataTable(ByVal filename As String, ByVal inpFile As System.Web.UI.HtmlControls.HtmlInputFile, ByVal sepchar As String) As DataTable
        Dim strFileName As String = ""
        Dim strFileName1 As String = ""
        Dim strNewFileName As String = ""
        Dim fileno As Integer
        Dim cmdFileinsert As String
        Try
            Dim cmdFileCount As New SqlCommand("select countfile from warsautofileno", connection7)
            Dim rdrFileCount As SqlDataReader
            connection7.Open()
            rdrFileCount = cmdFileCount.ExecuteReader
            If rdrFileCount.Read Then
                fileno = rdrFileCount("countfile")
                cmdFileinsert = "update warsautofileno set countfile=countfile+1"
            Else
                fileno = 1
                cmdFileinsert = "insert into warsautofileno values(2)"
            End If
            connection7.Close()
            cmdFileCount.Dispose()
            rdrFileCount.Close()
            Dim cmdFileNo As New SqlCommand(cmdFileinsert, connection7)
            connection7.Open()
            cmdFileNo.ExecuteNonQuery()
            connection7.Close()
            If (inpFile.PostedFile.FileName <> "") Then
                Dim ulFile As HttpPostedFile = inpFile.PostedFile
                Dim nFileLen As Int64 = ulFile.ContentLength
                If (nFileLen > 0) Then
                    strFileName = Path.GetFileName(inpFile.PostedFile.FileName)
                    strFileName1 = strFileName
                    txthid1.Text = fileno & "-" & strFileName
                    strFileName = "Uploads/" & fileno & "-" & strFileName
                    strNewFileName = Server.MapPath(strFileName)
                    inpFile.PostedFile.SaveAs(Server.MapPath(strFileName))
                End If
            End If
            Response.Flush()
            txthiddenserver.Text = strNewFileName
            'Return CSV2DataTable(strNewFileName, sepchar)
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Function
    Public Sub SetFocus(ByVal FocusControl As Control)
        'this function is for setting the focus
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        RegisterStartupScript("setFocus", Script.ToString())
    End Sub
    'This Function is For Displaying the Message Box
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        Page.RegisterStartupScript("ShowConfirm", Script)
    End Function
    Dim dt As New DataTable
    Dim row1 As DataRow
    Dim colum(10) As DataColumn
    Private Sub UpdateFileFormat(ByVal colCount As Integer, ByVal filePath As String)
        Dim strNewFileName As String = Server.MapPath(filePath)
        Dim strFile As New System.Text.StringBuilder
        Dim reader As System.IO.StreamReader
        reader = New System.IO.StreamReader(strNewFileName)

        Do While reader.Peek() > 0
            'Response.Write("inside loop")

            Dim tokens As String = reader.ReadLine()
            Dim tokens1 As String = ""
            Dim arr
            Dim intCols, i As Integer
            arr = Split(tokens, vbTab)
            intCols = UBound(arr)
            'Response.Write(intCols)
            'Response.Write(colCount)
            Dim strChk As String = ""
            For i = 0 To intCols
                strChk = ""
                If tokens1 = "" Then
                    strChk = Trim(arr(i))
                    arr(i) = strChk.Replace("  ", "")
                    strChk = Trim(arr(i))
                    arr(i) = strChk.Replace("'", "")
                    tokens1 = Trim(arr(i))
                Else
                    strChk = Trim(arr(i))
                    arr(i) = strChk.Replace("  ", "")
                    strChk = Trim(arr(i))
                    arr(i) = strChk.Replace("'", "")
                    tokens1 = tokens1 & vbTab & Trim(arr(i))
                End If
            Next

            For i = 1 To colCount - intCols - 1
                tokens1 = tokens1 & vbTab & ""
            Next
            tokens1 = tokens1 & vbCrLf
            strFile.Append(tokens1)
            ' Response.Write(tokens1)
            'Response.End()
        Loop
        reader.Close()
        Dim fp As StreamWriter
        fp = File.CreateText(Server.MapPath(filePath))
        fp.WriteLine(strFile)
        fp.Close()

    End Sub

    Protected Sub cmdUpload_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload.ServerClick
        'Code For Uploading File in a table 

        Dim k As Integer = 0
        Dim p As Integer = 0

        If rdAppend.Checked Then
            Dim columnnamearr = Split(Trim(Request("ddltabname")), "$")
            Dim combovalue = Trim(columnnamearr(0))

            'Dim combovalue = Request("ddltabname")
            'txttablename.Text = ""
            'Dim str22 As String = ddltabname.SelectedIndex.ToString()
            'Dim str21 As String = ddltabname.SelectedItem.Text
            'Dim str212 As String = ddltabname.SelectedValue.ToString()

            ' = ddltabname.Items(ddltabname).Text
            Dim txttablename1 As String
            txttablename1 = HiddenField1.Value
            'txttablename.Text = Session("tablename").ToString()

            If txttablename.Text = "" Or txttablename.Text = "--select--" Then
                ShowConfirm("First You Select Table!!!!!!")
                SetFocus(ddltabname)
            ElseIf myfile.Value = "" Then
                ShowConfirm("Select a File to Upload")
            ElseIf Right(myfile.PostedFile.FileName, 3) = "csv" Or Right(myfile.PostedFile.FileName, 3) = "xls" Or Right(myfile.PostedFile.FileName, 3) <> "txt" Then
                ShowConfirm("Select File of type Txt (Tab Delimited) to Upload")
            Else
                Try
                    Dim strFileName As String
                    strFileName = myfile.PostedFile.FileName
                    Dim table As DataTable = CSV2DataTable(strFileName, myfile, vbTab)
                    Dim objcmd As New SqlCommand("select count(name) as ColumnNumber from syscolumns where id=(select id from sysobjects where xtype='U' and name='" & Me.txttablename.Text & "')", connection2)
                    Dim dr1 As SqlDataReader
                    connection2.Open()
                    dr1 = objcmd.ExecuteReader
                    If dr1.Read = True Then
                        Dim ColCount As Integer = dr1("ColumnNumber")
                        Dim filePath As String = "Uploads\" & txthid1.Text
                        '''''''''If table.Columns.Count = dr1("ColumnNumber") Then
                        'Dim str As String()
                        Dim str0 As String = ""
                        UpdateFileFormat(ColCount, filePath)
                        ''''''''''''''First Delete the records of selected table''''''''''''''''''''''''''
                        ''Dim Cmd9 As New SqlCommand("delete " & Me.txttablename.Text & "", connection)
                        ''connection.Open()
                        ''Cmd9.ExecuteNonQuery()
                        ''connection.Close()
                        ''Cmd9.Dispose()
                        ''''''''''''''''''''''''''''''create temp table''''''''''''''''''''''''''''
                        Dim cmdcr As New SqlCommand("select * into temptab from " & Me.txttablename.Text, connection)
                        connection.Open()
                        cmdcr.ExecuteNonQuery()
                        connection.Close()
                        cmdcr.Dispose()
                        '''''''''''''''''''''''''''''''''''end create temp table'''''''''''''''''''
                        Dim strUploadPath = Server.MapPath("/AutoWhiz/DataTransfer/Uploads/" & txthid1.Text & "")
                        'Dim strUploadPath = Server.MapPath("/idms/Table_Management/Uploads/" & txthid1.Text & "")
                        Dim cmd As New SqlCommand("BULK INSERT " & Me.txttablename.Text & " FROM '" & Trim(strUploadPath) & "' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                        'Dim cmd As New SqlCommand("BULK INSERT " & Me.txttablename.Text & " FROM '\\bmp34\c\Inetpub\wwwroot\ARS\Table_Management\Uploads\" & txthid1.Text & " ' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                        'Response.Write("BULK INSERT " & Me.txttablename.Text & " FROM '\\bmp34\c\Inetpub\wwwroot\ARS\Table_Management\Uploads\importData.TXT' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')")
                        'Response.End()
                        connection.Open()
                        cmd.ExecuteNonQuery()
                        connection.Close()
                        cmd.Dispose()
                        ShowConfirm("Data Uploaded Succesfully!!!!")
                    End If
                    connection2.Close()
                    dr1.Close()
                    objcmd.Dispose()
                Catch ex As Exception
                    'Response.Write(ex)
                    Dim strmsg As String
                    strmsg = Replace(ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    ShowConfirm(strmsg)
                    Dim Cmd11 As New SqlCommand("delete " & Me.txttablename.Text & "", connection6)
                    connection6.Open()
                    Cmd11.ExecuteNonQuery()
                    connection6.Close()
                    Cmd11.Dispose()
                    ''''''''''''''''''''''insert data into original table from temp table'''''''''''''''''
                    Dim cmdins As New SqlCommand("insert into " & Me.txttablename.Text & " select * from temptab", connection1)
                    connection1.Open()
                    cmdins.ExecuteNonQuery()
                    connection1.Close()
                    cmdins.Dispose()
                    ''''''''''''''''''''''''''''''end insertion'''''''''''''''''''''''''''''''''''''''''''
                Finally
                    '''''''''''''''''''''drop temp tab'''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim cmddrop As New SqlCommand("drop table temptab", connection1)
                    connection1.Open()
                    cmddrop.ExecuteNonQuery()
                    connection1.Close()
                    cmddrop.Dispose()
                    '''''''''''''''''''end drop temp tab'''''''''''''''''''''''''''''''''''''''''''''''''''
                    connection2.Close()
                    connection.Close()
                    connection5.Close()
                    connection6.Close()
                End Try
            End If
        End If
        If Me.rdOverwrite.Checked Then

            Dim columnnamearr = Split(Trim(Request("ddltabname")), "$")
            Dim combovalue = Trim(columnnamearr(0))
            ' Dim columnnamearr = Split(Trim(Request("ddltabname")), "$")
            'Dim columnnamearr = ddltabname.Value

            'Dim combovalue = Trim(columnnamearr(0))
            'Dim combovalue = Request("ddltabname")
            'txttablename.Text = ""

            Dim txttablename1 As String
            txttablename1 = HiddenField1.Value
            If txttablename.Text = "" Or txttablename.Text = "--select--" Then
                ShowConfirm("First You Select Table!!!!!!")
                SetFocus(ddltabname)
            ElseIf myfile.Value = "" Then
                ShowConfirm("Select a File to Upload")
            ElseIf Right(myfile.PostedFile.FileName, 3) = "csv" Or Right(myfile.PostedFile.FileName, 3) = "xls" Or Right(myfile.PostedFile.FileName, 3) <> "txt" Then
                ShowConfirm("Select File of type Txt (Tab Delimited) to Upload")
            Else
                Try
                    Dim strFileName As String
                    strFileName = myfile.PostedFile.FileName
                    Dim table As DataTable = CSV2DataTable(strFileName, myfile, vbTab)
                    Dim objcmd As New SqlCommand("select count(name) as ColumnNumber from syscolumns where id=(select id from sysobjects where xtype='U' and name='" & Me.txttablename.Text & "')", connection2)
                    Dim dr1 As SqlDataReader
                    connection2.Open()
                    dr1 = objcmd.ExecuteReader
                    If dr1.Read = True Then
                        '''''''''If table.Columns.Count = dr1("ColumnNumber") Then
                        Dim ColCount As Integer = dr1("ColumnNumber")
                        Dim filePath As String = "Uploads\" & txthid1.Text
                        '''''''''If table.Columns.Count = dr1("ColumnNumber") Then
                        Dim str As String()
                        Dim str0 As String = ""
                        UpdateFileFormat(ColCount, filePath)
                        ''''''''''''''''''''''''''''''create temp table''''''''''''''''''''''''''''
                        Dim cmdcr As New SqlCommand("select * into temptab from " & Me.txttablename.Text, connection)
                        connection.Open()
                        cmdcr.ExecuteNonQuery()
                        connection.Close()
                        cmdcr.Dispose()
                        '''''''''''''''''''''''''''''''''''end create temp table'''''''''''''''''''
                        ''''''''''''''First Delete the records of selected table''''''''''''''''''''''''''
                        Dim Cmd9 As New SqlCommand("delete " & Me.txttablename.Text & "", connection)
                        connection.Open()
                        Cmd9.ExecuteNonQuery()
                        connection.Close()
                        Cmd9.Dispose()
                        Dim strUploadPath = Server.MapPath("/AutoWhiz/DataTransfer/Uploads/" & txthid1.Text & "")
                        'Dim strUploadPath = Server.MapPath("/idms/Table_Management/Uploads/" & txthid1.Text & "")
                        Dim cmd As New SqlCommand("BULK INSERT " & Me.txttablename.Text & " FROM '" & Trim(strUploadPath) & "' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                        'Dim cmd As New SqlCommand("BULK INSERT " & Me.txttablename.Text & " FROM '\\bmp34\c\Inetpub\wwwroot\ARS\Table_Management\Uploads\" & txthid1.Text & " ' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                        'Response.Write("BULK INSERT " & Me.txttablename.Text & " FROM '\\bmp34\c\Inetpub\wwwroot\ARS\Table_Management\Uploads\importData.TXT' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')")
                        'Response.End()
                        connection.Open()
                        cmd.ExecuteNonQuery()
                        connection.Close()
                        cmd.Dispose()
                        ShowConfirm("Data Uploaded Succesfully!!!!")
                    End If
                    connection2.Close()
                    dr1.Close()
                    objcmd.Dispose()
                Catch ex As Exception
                    'Response.Write(ex)
                    Dim strmsg As String
                    strmsg = Replace(ex.Message.ToString, "'", "")
                    strmsg = Replace(strmsg, vbCrLf, " ")
                    ShowConfirm(strmsg)
                    Dim Cmd11 As New SqlCommand("delete " & Me.txttablename.Text & "", connection6)
                    connection6.Open()
                    Cmd11.ExecuteNonQuery()
                    connection6.Close()
                    Cmd11.Dispose()
                    ''''''''''''''''''''''insert data into original table from temp table'''''''''''''''''
                    Dim cmdins As New SqlCommand("insert into " & Me.txttablename.Text & " select * from temptab", connection1)
                    connection1.Open()
                    cmdins.ExecuteNonQuery()
                    connection1.Close()
                    cmdins.Dispose()
                    ''''''''''''''''''''''''''''''end insertion '''''''''''''''''''''''''''''''''''''''''''
                Finally
                    '''''''''''''''''''''drop temp tab'''''''''''''''''''''''''''''''''''''''''''''''''''''
                    Dim cmddrop As New SqlCommand("drop table temptab", connection1)
                    connection1.Open()
                    cmddrop.ExecuteNonQuery()
                    connection1.Close()
                    cmddrop.Dispose()
                    '''''''''''''''''''end drop temp tab'''''''''''''''''''''''''''''''''''''''''''''''''''
                    connection2.Close()
                    connection.Close()
                    connection5.Close()
                    connection6.Close()
                End Try
            End If
        End If
        Exit Sub
    End Sub


End Class
