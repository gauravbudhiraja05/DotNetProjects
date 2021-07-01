Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO.StreamReader
Imports System.IO.StreamWriter
Imports System.Data.OleDb
Imports System.Configuration.ConfigurationSettings


Partial Class DataManager_ImportData
    Inherits System.Web.UI.Page
    Dim classobj As New Functions
    Dim con1 As String = AppSettings("ConnectionString")
    Dim conn As New SqlConnection(con1)
    Dim con As New SqlConnection(con1)
    Dim dr As SqlDataReader
    Dim cmd As SqlCommand
    Dim connection As New SqlConnection(con1)
    ''' <summary>
    ''' To fill Department into dropdwonlist on page load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        'Ajax.Utility.RegisterTypeForAjax(GetType(DataManagerAjax))
        'Me.ddlDept.Attributes.Add("onchange", "getclient();")
        'Me.ddlClient.Attributes.Add("onchange", "GetLOB();")
        'Me.ddlLob.Attributes.Add("onchange", "bindlisttab();")
        'Me.ddlTable.Attributes.Add("onchange", "gettablename();")

        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                btnImport_multiuser.Visible = True
                btnFinalize_multiuser.Visible = True
                rdr.Close()
                Dim cmd1 As SqlCommand
                If (Session("typeofuser") = "Super Admin") Then
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
                Else
                    cmd1 = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
                End If
                Dim dsar As DataSet = New DataSet()
                Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd1)
                daar.Fill(dsar)
                If (dsar.Tables(0).Rows.Count > 0) Then
                    select_level1.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    Label2.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    Label3.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If

            Else
                get_table.Visible = True
                btnImport_singleuser.Visible = True
                btnFinalize_singleuser.Visible = True
            End If
        End If

        connection.Close()
        Dim typeofuser = Session("typeofuser")
        If (Page.IsPostBack = False) Then
            If (typeofuser.Equals("Super Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName,AutoID from idmsdepartment  where SavedBy='" + Session("userid") + "'", connection)
                connection.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "-- Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("User")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            ElseIf (typeofuser.Equals("Admin")) Then
                Dim cmd As SqlCommand = New SqlCommand("select DepartmentName ,AutoID from IdmsDepartment where SavedBy =( select CreatedBy from Registration where UserID='" + Session("userid") + "')", con)
                con.Open()
                DepartmentName.DataSource = cmd.ExecuteReader()
                DepartmentName.DataTextField = "DepartmentName"
                DepartmentName.DataValueField = "AutoID"
                DepartmentName.Items.Insert(0, "--Select--")
                DepartmentName.DataBind()
            End If
            DepartmentName.Items.Insert(0, "--select--")
        End If



        'If Me.IsPostBack = True Then

        '    If txtFile.PostedFile.FileName = "" Then
        '        Me.btnCancel.Visible = False
        '    End If

        '    If txtFile.PostedFile.FileName <> "" Then
        '        Me.btnCancel.Visible = True
        '    End If

        'End If


    End Sub
    ''' <summary>
    ''' Code to import data into sql table
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub btnImport_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport.ServerClick


    '    Dim Path As String = ""
    '    Try
    '        If txtFile.PostedFile.FileName = "" Then
    '            Showmsg("Please Select FileName!!!")
    '            Exit Sub

    '        ElseIf Right(txtFile.PostedFile.FileName, 3) <> "xls" Then
    '            If Right(txtFile.PostedFile.FileName, 3) <> "xlsx" Then
    '                Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
    '                Exit Sub
    '            End If


    '        ElseIf ddlDept.SelectedIndex = 0 Then
    '            Showmsg("Select Depatment!!!")
    '            Exit Sub
    '        ElseIf txttablname.Value = "" And ddlDept.SelectedIndex <> 0 Then
    '            Showmsg("Select Table Name!!!")
    '            Exit Sub
    'Right(txtFile.PostedFile.FileName, 3) = "csv" Or 
    '            Or Right(txtFile.PostedFile.FileName, 3) = "txt"

    '        End If
    '        Dim fp As StreamWriter
    '        Dim path1 As String = txtFile.Value
    '        Dim filePath As String = path1
    '        Dim slashPosition As Integer = filePath.LastIndexOf("\")
    '        Dim filenameOnly As String = filePath.Substring(slashPosition + 1)
    '        Dim filelength = filenameOnly.Length
    '        Dim wname As String = filenameOnly.Substring(0, filelength - 4)
    '        Path = Server.MapPath("~/Excelfile/") & filenameOnly
    '        <--------------------Creating a new text file---------------------------------->
    '        Dim path2 As String = txtFile.PostedFile.FileName
    '        If File.Exists(Path) Then
    '            File.Delete(Path)
    '        End If
    '        txtFile.PostedFile.SaveAs(Path)

    '        File.Copy(path2, Path)
    '        change()


    '        Dim sSQLTable As String = txttablname.Value
    '        If InStr(LCase(wname), LCase(sSQLTable)) <> 0 Then
    '            If InStr(LCase(sSQLTable), LCase(sSQLTable)) <> 0 Then
    '                Dim sExcelFileName As String = filenameOnly
    '                Dim sWorkbook As String = "[" + wname + "$" + "]"
    '                Dim sWorkbook As String = "[" + sSQLTable + "$" + "]"
    '                Dim impotrget = classobj.import_data(sSQLTable, sExcelFileName, sWorkbook, Path)

    '                If impotrget = "2" Then

    '            ***********change****************
    '            . to access the name of sheet from excel file
    '                    Dim objExcel As Excel.Application
    '                    Dim objWorkBook As Excel.Workbook
    '                    Dim totalWorkSheets As Excel.Worksheet
    '                    Dim objWorkSheets As Excel.Worksheet
    '                    Dim ExcelSheetName As String = ""

    '                    objExcel = CreateObject("Excel.Application")
    '                    objWorkBook = objExcel.Workbooks.Open("C:\myExcel.xls")

    '                    ' this code gets the names off all the worksheets 
    '                    For Each totalWorkSheets In myWorkBook.Worksheets
    '                        ExcelSheetName += totalWorkSheets.Name
    '                    Next totalWorkSheets

    '            ***********change****************


    '                    Dim al As String = Convert.ToString(impotrget)


    '                    If al.Contains("error :") Then
    '                        Dim strmsgval As String = impotrget.ToString
    '                        If strmsgval.Contains("PRIMARY") Then
    '                            Showmsg("Primary key violation!!!!")
    '                        ElseIf LCase(strmsgval).Contains("open") Then
    '                            Showmsg("File is already open. close the file first.")
    '                        ElseIf LCase(strmsgval).Contains("length") Then
    '                            Showmsg("Length of data should be same as length of table column")
    '                        ElseIf LCase(strmsgval).Contains("valid") Then
    '                            Showmsg("The name of data sheet should be same as table name")
    '                        Else
    '                            Response.Write(strmsgval)
    '                            strmsgval = strmsgval.Replace("'", "")
    '                            Showmsg(strmsgval)
    '                        End If




    '                        Exit Sub
    '                        Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
    '                    Else
    '                        Dim cc As String = Convert.ToInt32(impotrget)
    '                        Dim crdate As Date
    '                        crdate = Today.Date

    '                        Dim ds2 As DataSet = New DataSet()
    '                        Dim da2 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where tabname='" + txttablname.Value + "' and whetherdaily='true' and userid='" + Session("userid") + "'", conn)
    '                        da2.Fill(ds2)
    '                        Dim comm2 As SqlCommand
    '                        Dim dr2 As DataRow
    '                        If ds2.Tables(0).Rows.Count > 0 Then
    '                            For Each dr2 In ds2.Tables(0).Rows
    '                                conn.Open()
    '                                Dim alertid As String = dr2("AlertID").ToString()
    '                                comm2 = New SqlCommand("Update alertinfo set Appendtime = '" + crdate + "' where ALertID='" + alertid + "'", conn)
    '                                comm2.ExecuteNonQuery()
    '                                conn.Close()
    '                            Next
    '                        End If
    '                        ds2.Dispose()
    '                        da2.Dispose()
    '                        Showmsg("Data Imported  Successfully!!!!")

    '                        Me.ddlDept.SelectedIndex = 0
    '                code to Track
    '                        Dim ClientId
    '                        Dim LobId


    '                        If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
    '                            ClientId = "0"
    '                        Else
    '                            ClientId = clt.Value
    '                        End If
    '                        If lob.Value = "--Select--" Or lob.Value = "" Then
    '                            LobId = "0"
    '                        Else
    '                            LobId = lob.Value
    '                        End If
    '                        Dim da As SqlDataAdapter = New SqlDataAdapter
    '                        Dim cmd As SqlCommand
    '                        cmd = New SqlCommand("Sp_LogDataManager", conn)
    '                        cmd.CommandType = CommandType.StoredProcedure
    '                        cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
    '                        cmd.Parameters("@ActionBy").Value = Session("userId")
    '                        cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
    '                        cmd.Parameters("@Action").Value = "Data Import"
    '                        cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
    '                        cmd.Parameters("@Date").Value = System.DateTime.Now
    '                        cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
    '                        cmd.Parameters("@Entity").Value = "Table"
    '                        cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
    '                        cmd.Parameters("@EntityName").Value = txttablname.Value
    '                        cmd.Parameters.Add("@DeptId", SqlDbType.Int, 50)
    '                        cmd.Parameters("@DeptId").Value = ddlDept.SelectedValue
    '                        cmd.Parameters.Add("@ClientId", SqlDbType.Int, 50)
    '                        cmd.Parameters("@ClientId").Value = CType(ClientId, Integer)
    '                        cmd.Parameters.Add("@LobId", SqlDbType.Int, 50)
    '                        cmd.Parameters("@LobId").Value = CType(LobId, Integer)
    '                        cmd.Parameters.Add("@count", SqlDbType.Int, 50)
    '                        cmd.Parameters("@count").Value = CType(cc, Integer)

    '                        conn.Open()
    '                        cmd.ExecuteNonQuery()

    '                        Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Import' and EntityName='" + txttablname.Value + "'", conn)
    '                        cmm.ExecuteNonQuery()

    '                        conn.Close()
    '                End Track
    '                        ddlDept.SelectedIndex = 0

    '                    End If

    '                Else
    '                    Showmsg("Please Select Right Table Name or FileName!!!")
    '                    Me.ddlDept.SelectedIndex = 0
    '                End If
    '    Catch ex As Exception
    '        Showmsg(ex.Message)
    '    End Try
    '    If File.Exists(Path) Then
    '        File.Delete(Path)
    '    End If
    'End Sub
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "Showmsg", str.ToString)
    End Sub
    ''' <summary>
    ''' to cancel the file path
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btnCancel_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.ServerClick

        If txtFile.PostedFile.FileName = "" Then
            Showmsg("No File Name is Selected!!!!")
        End If

        If txtFile.PostedFile.FileName <> "" Then
            Dim fn As String = System.IO.Path.GetFileName(txtFile.PostedFile.FileName)
            txtFile.PostedFile.ContentLength.Equals(0)
            'ddlDept.SelectedIndex = 0
        End If

    End Sub

    

    'Protected Sub btnFinalize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalize.Click
    '    conn.Open()
    '    Dim result As String = ""
    '    Try
    '        Dim crdate As Date
    '        crdate = Today.Date
    '        If txttablname.Value = "" Then
    '            Showmsg("Please select atleast one table to finalize")
    '            Exit Sub
    '        End If

    '        Dim FromMail As String = ConfigurationSettings.AppSettings("MailFrom").ToString()
    '        Dim MailServer As String = ConfigurationSettings.AppSettings("MailSMTPServer").ToString()
    '        Dim folderpath As String = "C:/mailalerts/File"

    '        Dim ds1 As DataSet = New DataSet()
    '        Dim da1 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where whetherdaily='eve' and userid='" + Session("userid") + "'", conn)
    '        da1.Fill(ds1)
    '        Dim dr1 As DataRow
    '        If ds1.Tables(0).Rows.Count > 0 Then
    '            For Each dr1 In ds1.Tables(0).Rows
    '                Dim alertid As String = dr1("AlertID").ToString()
    '                If dr1("whethertable").ToString() = "true" And dr1("tabname").ToString().ToUpper = txttablname.Value.ToUpper Then

    '                    '''''''''''''''''''''Addddddddddd start
    '                    'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
    '                    'check.Parameters.AddWithValue("@alertid", alertid)
    '                    'Dim cond As String = check.ExecuteScalar().ToString()

    '                    'Dim dd As New DataSet()
    '                    'Dim da As New SqlDataAdapter(cond, conn)
    '                    'da.Fill(dd)
    '                    'Dim iscondition As Integer = dd.Tables(0).Rows.Count
    '                    '''''''''''''''''''''Addddddddddd stop

    '                    Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
    '                    cmd.CommandType = CommandType.StoredProcedure
    '                    cmd.Parameters.AddWithValue("@alertid", alertid)
    '                    cmd.Parameters.AddWithValue("@FromMail", FromMail)
    '                    cmd.Parameters.AddWithValue("@MailServer", MailServer)
    '                    'smitha
    '                    'cmd.Parameters.AddWithValue("@folderpath", folderpath)
    '                    'cmd.Parameters.AddWithValue("@iscondition", iscondition)
    '                    cmd.ExecuteNonQuery()
    '                Else
    '                    Dim comm1 As New SqlCommand("select tablenames from usp_mailview where alertid='" + alertid + "' and whetherdaily='eve'", conn)
    '                    Dim tab As String = Convert.ToString(comm1.ExecuteScalar())
    '                    Dim table As String()
    '                    table = tab.Split(",")
    '                    Dim i As Integer
    '                    For i = 0 To table.Length - 1
    '                        If table(i).ToUpper = txttablname.Value.ToUpper Then
    '                            ''''''''''''''''''''Addddddddddd start
    '                            'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
    '                            'check.Parameters.AddWithValue("@alertid", alertid)
    '                            'Dim cond As String = check.ExecuteScalar().ToString()

    '                            'Dim dd As New DataSet()
    '                            'Dim da As New SqlDataAdapter(cond, conn)
    '                            'da.Fill(dd)
    '                            'Dim iscondition As Integer = dd.Tables(0).Rows.Count
    '                            '''''''''''''''''''''Addddddddddd stop
    '                            Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
    '                            cmd.CommandType = CommandType.StoredProcedure
    '                            cmd.Parameters.AddWithValue("@alertid", alertid)
    '                            cmd.Parameters.AddWithValue("@FromMail", FromMail)
    '                            cmd.Parameters.AddWithValue("@MailServer", MailServer)
    '                            'cmd.Parameters.AddWithValue("@folderpath", folderpath)
    '                            'cmd.Parameters.AddWithValue("@iscondition", iscondition)
    '                            cmd.ExecuteNonQuery()
    '                        End If
    '                    Next
    '                End If
    '            Next
    '            result += "Data has been finalize on table " + txttablname.Value + ""
    '        Else
    '            result += "No event alert found on this table."
    '        End If
    '        ds1.Dispose()
    '        da1.Dispose()
    '        '''''''''''''''''''''''''''''''''''''''''''''''TRACK START

    '        Dim ClientId
    '        Dim LobId
    '        If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
    '            ClientId = "0"
    '        Else
    '            ClientId = clt.Value
    '        End If
    '        If lob.Value = "--Select--" Or lob.Value = "" Then
    '            LobId = "0"
    '        Else
    '            LobId = lob.Value
    '        End If
    '        Dim cmd2 As SqlCommand
    '        cmd2 = New SqlCommand("Sp_LogDataManagerFinilize", conn)
    '        cmd2.CommandType = CommandType.StoredProcedure
    '        cmd2.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
    '        cmd2.Parameters("@ActionBy").Value = Session("userId")
    '        cmd2.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
    '        cmd2.Parameters("@Action").Value = "Data Finilize"
    '        cmd2.Parameters.Add("@Date", SqlDbType.DateTime, 8)
    '        cmd2.Parameters("@Date").Value = System.DateTime.Now
    '        cmd2.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
    '        cmd2.Parameters("@Entity").Value = "Table"
    '        cmd2.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
    '        cmd2.Parameters("@EntityName").Value = txttablname.Value
    '        cmd2.Parameters.Add("@DeptId", SqlDbType.Int, 50)
    '        cmd2.Parameters("@DeptId").Value = ddlDept.SelectedValue
    '        cmd2.Parameters.Add("@ClientId", SqlDbType.Int, 50)
    '        cmd2.Parameters("@ClientId").Value = CType(ClientId, Integer)
    '        cmd2.Parameters.Add("@LobId", SqlDbType.Int, 50)
    '        cmd2.Parameters("@LobId").Value = CType(LobId, Integer)
    '        cmd2.Parameters.Add("@MailTo", SqlDbType.NVarChar, 799)
    '        cmd2.Parameters("@MailTo").Value = mail()
    '        cmd2.ExecuteNonQuery()
    '        Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Finilize' and EntityName='" + txttablname.Value + "'", conn)
    '        cmm.ExecuteNonQuery()
    '        '''''''''''''''''''''''''''''''''''''''''''''''TRACK END
    '        Showmsg(result)
    '        ddlDept.SelectedIndex = 0
    '    Catch ex As Exception
    '        Showmsg(ex.Message)
    '    Finally
    '        conn.Close()
    '    End Try
    'End Sub

    Public Function mail()
        Dim mailto As String = ""
        Dim alerts As String = ""
        Dim ds1 As DataSet = New DataSet()
        Dim da1 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where whetherdaily='eve' and userid='" + Session("userid") + "'", conn)
        da1.Fill(ds1)
        Dim dr1 As DataRow
        If ds1.Tables(0).Rows.Count > 0 Then
            For Each dr1 In ds1.Tables(0).Rows
                Dim alertid As String = dr1("AlertID").ToString()
                If dr1("whethertable").ToString() = "true" And dr1("tabname").ToString() = txttablname.Value Then
                    alerts = alerts + "," + alertid
                    Dim reader As SqlDataReader
                    Dim cmd As New SqlCommand("select EmailID+'(' + UserName + ')' from notifyauthorities a inner join registration b  on a.EmailID=b.UserID where alertid=@alertid", conn)
                    cmd.Parameters.AddWithValue("@alertid", alertid)
                    reader = cmd.ExecuteReader()
                    While reader.Read()
                        mailto = mailto + "," + reader(0).ToString()
                    End While
                    reader.Close()
                Else
                    Dim comm1 As New SqlCommand("select tablenames from usp_mailview where alertid='" + alertid + "' and whetherdaily='eve'", conn)
                    Dim tab As String = Convert.ToString(comm1.ExecuteScalar())
                    Dim table As String()
                    table = tab.Split(",")
                    Dim i As Integer
                    For i = 0 To table.Length - 1
                        If table(i) = txttablname.Value Then
                            Dim reader As SqlDataReader
                            alerts = alerts + "," + alertid
                            Dim cmd As New SqlCommand("select EmailID+'(' + UserName + ')' from notifyauthorities a inner join registration b  on a.EmailID=b.UserID where alertid=@alertid", conn)
                            cmd.Parameters.AddWithValue("@alertid", alertid)
                            reader = cmd.ExecuteReader()
                            While reader.Read()
                                mailto = mailto + "," + reader(0).ToString()
                            End While
                            reader.Close()
                        End If
                    Next
                End If
            Next
        End If
        ds1.Dispose()
        da1.Dispose()
        If mailto <> "" Then
            mailto = mailto.Remove(0, 1)
        End If
        If alerts <> "" Then
            alerts = alerts.Remove(0, 1)
        End If
        mailto = "Event based alerts : " + alerts + " and mail fired to " + mailto
        Return mailto
    End Function
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        If (DepartmentName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select DepartmentName")
        End If
        con.Open()
        cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ClientName.DataSource = dr
        ClientName.DataTextField = "ClientName"
        ClientName.DataValueField = "autoid"
        ClientName.DataBind()
        ClientName.Items.Insert(0, "--select--")
    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClientName.SelectedIndexChanged
        If (ClientName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select ClientName")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ddlLobName.DataSource = dr
        ddlLobName.DataTextField = "LOBName"
        ddlLobName.DataValueField = "autoid"
        ddlLobName.DataBind()
        ddlLobName.Items.Insert(0, "--select--")
    End Sub

    Protected Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
        If (ddlLobName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select Level3")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId='" + DepartmentName.SelectedValue + "' and ClientId='" + ClientName.SelectedValue + "' and LOBId='" + ddlLobName.SelectedValue + "' and createdBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "Tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--select--")
    End Sub

    Protected Sub btnImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport_multiuser.Click


        Dim Path As String = ""
        Try
            If txtFile.PostedFile.FileName = "" Then
                Showmsg("Please Select FileName!!!")
                Exit Sub

            ElseIf Right(txtFile.PostedFile.FileName, 3) <> "xls" Then
                If Right(txtFile.PostedFile.FileName, 3) <> "xlsx" Then
                    Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
                    Exit Sub
                End If


            ElseIf DepartmentName.SelectedIndex = 0 Then
                Showmsg("Select Depatment!!!")
                Exit Sub
            ElseIf ddlTable.SelectedItem.Text = "" Then
                Showmsg("Select Table Name!!!")
                Exit Sub
                'Right(txtFile.PostedFile.FileName, 3) = "csv" Or 
                'Or Right(txtFile.PostedFile.FileName, 3) = "txt"

            End If

            Dim fp As StreamWriter
            Dim path1 As String = txtFile.Value
            Dim filePath As String = path1
            Dim slashPosition As Integer = filePath.LastIndexOf("\")
            Dim filenameOnly As String = filePath.Substring(slashPosition + 1)
            Dim filelength = filenameOnly.Length
            Dim wname As String = filenameOnly.Substring(0, filelength - 4)
            Path = Server.MapPath("~/Excelfile/") & filenameOnly
            '<--------------------Creating a new text file---------------------------------->
            Dim path2 As String = txtFile.PostedFile.FileName
            If File.Exists(Path) Then
                File.Delete(Path)
            End If
            txtFile.PostedFile.SaveAs(Path)

            'File.Copy(path2, Path)
            'change

            txttablname.Value = ddlTable.SelectedItem.Text
            Dim sSQLTable As String = txttablname.Value
            ' If InStr(LCase(wname), LCase(sSQLTable)) <> 0 Then
            If InStr(LCase(sSQLTable), LCase(sSQLTable)) <> 0 Then
                Dim sExcelFileName As String = filenameOnly
                ' Dim sWorkbook As String = "[" + wname + "$" + "]"
                Dim sWorkbook As String = "[" + sSQLTable + "$" + "]"
                Dim impotrget = classobj.import_data(sSQLTable, sExcelFileName, sWorkbook, Path)

                'If impotrget = "2" Then

                '***********change****************
                '. to access the name of sheet from excel file
                'Dim objExcel As Excel.Application
                'Dim objWorkBook As Excel.Workbook
                'Dim totalWorkSheets As Excel.Worksheet
                'Dim objWorkSheets As Excel.Worksheet
                'Dim ExcelSheetName As String = ""

                'objExcel = CreateObject("Excel.Application")
                'objWorkBook = objExcel.Workbooks.Open("C:\myExcel.xls")

                '' this code gets the names off all the worksheets 
                'For Each totalWorkSheets In myWorkBook.Worksheets
                '    ExcelSheetName += totalWorkSheets.Name
                'Next totalWorkSheets

                '***********change****************


                Dim al As String = Convert.ToString(impotrget)


                If al.Contains("error :") Then
                    Dim strmsgval As String = impotrget.ToString
                    If strmsgval.Contains("PRIMARY") Then
                        Showmsg("Primary key violation!!!!")
                    ElseIf LCase(strmsgval).Contains("open") Then
                        Showmsg("File is already open. close the file first.")
                    ElseIf LCase(strmsgval).Contains("length") Then
                        Showmsg("Length of data should be same as length of table column")
                    ElseIf LCase(strmsgval).Contains("valid") Then
                        Showmsg("The name of data sheet should be same as table name")
                    Else
                        'Response.Write(strmsgval)
                        strmsgval = strmsgval.Replace("'", "")
                        Showmsg(strmsgval)
                    End If




                    Exit Sub
                    ' Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
                Else
                    Dim cc As String = Convert.ToInt32(impotrget)
                    Dim crdate As Date
                    crdate = Today.Date

                    Dim ds2 As DataSet = New DataSet()
                    Dim da2 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where tabname='" + txttablname.Value + "' and whetherdaily='true' and userid='" + Session("userid") + "'", conn)
                    da2.Fill(ds2)
                    Dim comm2 As SqlCommand
                    Dim dr2 As DataRow
                    If ds2.Tables(0).Rows.Count > 0 Then
                        For Each dr2 In ds2.Tables(0).Rows
                            conn.Open()
                            Dim alertid As String = dr2("AlertID").ToString()
                            comm2 = New SqlCommand("Update alertinfo set Appendtime = '" + crdate + "' where ALertID='" + alertid + "'", conn)
                            comm2.ExecuteNonQuery()
                            conn.Close()
                        Next
                    End If
                    ds2.Dispose()
                    da2.Dispose()
                    Showmsg("Data Imported  Successfully!!!!")

                    'Me.ddlDept.SelectedIndex = 0
                    'code to Track
                    Dim ClientId
                    Dim LobId


                    'If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
                    '    ClientId = "0"
                    'Else
                    '    ClientId = clt.Value
                    'End If
                    'If lob.Value = "--Select--" Or lob.Value = "" Then
                    '    LobId = "0"
                    'Else
                    '    LobId = lob.Value
                    'End If

                    Dim da As SqlDataAdapter = New SqlDataAdapter
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand("Sp_LogDataManager", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@Action").Value = "Data Import"
                    cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@ActionBy").Value = Session("userId")
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
                    cmd.Parameters("@Date").Value = System.DateTime.Now
                    cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@Entity").Value = "Table"
                    cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@EntityName").Value = txttablname.Value
                    cmd.Parameters.Add("@DeptId", SqlDbType.Int, 50)
                    cmd.Parameters("@DeptId").Value = DepartmentName.SelectedValue
                    cmd.Parameters.Add("@ClientId", SqlDbType.Int, 50)
                    cmd.Parameters("@ClientId").Value = ClientName.SelectedValue
                    cmd.Parameters.Add("@LobId", SqlDbType.Int, 50)
                    cmd.Parameters("@LobId").Value = ddlLobName.SelectedValue
                    'cmd.Parameters.Add("@count", SqlDbType.Int, 50)
                    'cmd.Parameters("@count").Value = CType(cc, Integer)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Import' and EntityName='" + txttablname.Value + "'", conn)
                    cmm.ExecuteNonQuery()

                    conn.Close()
                    'End Track
                    'ddlDept.SelectedIndex = 0

                End If

            Else
                Showmsg("Please Select Right Table Name or FileName!!!")
                Me.DepartmentName.SelectedValue = 0
            End If
        Catch ex As Exception
            Showmsg(ex.Message)
        End Try
        If File.Exists(Path) Then
            File.Delete(Path)
        End If
    End Sub

    Protected Sub btnFinalize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalize_multiuser.Click


        conn.Open()
        Dim result As String = ""
        txttablname.Value = ddlTable.SelectedItem.Text
        Try
            Dim crdate As Date
            crdate = Today.Date
            If txttablname.Value = "" Then
                Showmsg("Please select atleast one table to finalize")
                Exit Sub
            End If

            Dim FromMail As String = ConfigurationSettings.AppSettings("MailFrom").ToString()
            Dim MailServer As String = ConfigurationSettings.AppSettings("MailSMTPServer").ToString()
            Dim folderpath As String = "C:/mailalerts/File"

            Dim ds1 As DataSet = New DataSet()
            Dim da1 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where whetherdaily='eve' and userid='" + Session("userid") + "'", conn)
            da1.Fill(ds1)
            Dim dr1 As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                For Each dr1 In ds1.Tables(0).Rows
                    Dim alertid As String = dr1("AlertID").ToString()
                    If dr1("whethertable").ToString() = "true" And dr1("tabname").ToString().ToUpper = txttablname.Value.ToUpper Then

                        '''''''''''''''''''''Addddddddddd start
                        'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
                        'check.Parameters.AddWithValue("@alertid", alertid)
                        'Dim cond As String = check.ExecuteScalar().ToString()

                        'Dim dd As New DataSet()
                        'Dim da As New SqlDataAdapter(cond, conn)
                        'da.Fill(dd)
                        'Dim iscondition As Integer = dd.Tables(0).Rows.Count
                        '''''''''''''''''''''Addddddddddd stop

                        Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@alertid", alertid)
                        cmd.Parameters.AddWithValue("@FromMail", FromMail)
                        cmd.Parameters.AddWithValue("@MailServer", MailServer)
                        'smitha
                        'cmd.Parameters.AddWithValue("@folderpath", folderpath)
                        'cmd.Parameters.AddWithValue("@iscondition", iscondition)
                        cmd.ExecuteNonQuery()
                    Else
                        Dim comm1 As New SqlCommand("select tablenames from usp_mailview where alertid='" + alertid + "' and whetherdaily='eve'", conn)
                        Dim tab As String = Convert.ToString(comm1.ExecuteScalar())
                        Dim table As String()
                        table = tab.Split(",")
                        Dim i As Integer
                        For i = 0 To table.Length - 1
                            If table(i).ToUpper = txttablname.Value.ToUpper Then
                                ''''''''''''''''''''Addddddddddd start
                                'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
                                'check.Parameters.AddWithValue("@alertid", alertid)
                                'Dim cond As String = check.ExecuteScalar().ToString()

                                'Dim dd As New DataSet()
                                'Dim da As New SqlDataAdapter(cond, conn)
                                'da.Fill(dd)
                                'Dim iscondition As Integer = dd.Tables(0).Rows.Count
                                '''''''''''''''''''''Addddddddddd stop
                                Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("@alertid", alertid)
                                cmd.Parameters.AddWithValue("@FromMail", FromMail)
                                cmd.Parameters.AddWithValue("@MailServer", MailServer)
                                'cmd.Parameters.AddWithValue("@folderpath", folderpath)
                                'cmd.Parameters.AddWithValue("@iscondition", iscondition)
                                cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                Next
                result += "Data has been finalize on table " + txttablname.Value + ""
            Else
                result += "No event alert found on this table."
            End If
            ds1.Dispose()
            da1.Dispose()
            '''''''''''''''''''''''''''''''''''''''''''''''TRACK START

            Dim ClientId
            Dim LobId
            If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
                ClientId = "0"
            Else
                ClientId = clt.Value
            End If
            If lob.Value = "--Select--" Or lob.Value = "" Then
                LobId = "0"
            Else
                LobId = lob.Value
            End If
            Dim cmd2 As SqlCommand
            cmd2 = New SqlCommand("Sp_LogDataManagerFinilize", conn)
            cmd2.CommandType = CommandType.StoredProcedure
            cmd2.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@ActionBy").Value = Session("userId")
            cmd2.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@Action").Value = "Data Finilize"
            cmd2.Parameters.Add("@Date", SqlDbType.DateTime, 8)
            cmd2.Parameters("@Date").Value = System.DateTime.Now
            cmd2.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@Entity").Value = "Table"
            cmd2.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@EntityName").Value = txttablname.Value
            cmd2.Parameters.Add("@DeptId", SqlDbType.Int, 50)
            cmd2.Parameters("@DeptId").Value = DepartmentName.SelectedValue
            cmd2.Parameters.Add("@ClientId", SqlDbType.Int, 50)
            cmd2.Parameters("@ClientId").Value = ClientName.SelectedValue
            cmd2.Parameters.Add("@LobId", SqlDbType.Int, 50)
            cmd2.Parameters("@LobId").Value = ddlLobName.SelectedValue
            cmd2.Parameters.Add("@MailTo", SqlDbType.NVarChar, 799)
            cmd2.Parameters("@MailTo").Value = Mail()
            cmd2.ExecuteNonQuery()
            Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Finilize' and EntityName='" + txttablname.Value + "'", conn)
            cmm.ExecuteNonQuery()
            '''''''''''''''''''''''''''''''''''''''''''''''TRACK END
            Showmsg(result)
            DepartmentName.SelectedIndex = 0
        Catch ex As Exception
            Showmsg(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub

    Protected Sub btnImport_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImport_singleuser.Click


        Dim Path As String = ""
        Try
            If txtFile.PostedFile.FileName = "" Then
                Showmsg("Please Select FileName!!!")
                Exit Sub

            ElseIf Right(txtFile.PostedFile.FileName, 3) <> "xls" Then
                If Right(txtFile.PostedFile.FileName, 3) <> "xlsx" Then
                    Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
                    Exit Sub
                End If


                'ElseIf ddlDept.SelectedIndex = 0 Then
                '    Showmsg("Select Depatment!!!")
                '    Exit Sub
            ElseIf ddlTable.SelectedItem.Text = "" Then
                Showmsg("Select Table Name!!!")
                Exit Sub
                'Right(txtFile.PostedFile.FileName, 3) = "csv" Or 
                'Or Right(txtFile.PostedFile.FileName, 3) = "txt"

            End If

            Dim fp As StreamWriter
            Dim path1 As String = txtFile.Value
            Dim filePath As String = path1
            Dim slashPosition As Integer = filePath.LastIndexOf("\")
            Dim filenameOnly As String = filePath.Substring(slashPosition + 1)
            Dim filelength = filenameOnly.Length
            Dim wname As String = filenameOnly.Substring(0, filelength - 4)
            Path = Server.MapPath("~/Excelfile/") & filenameOnly
            '<--------------------Creating a new text file---------------------------------->
            Dim path2 As String = txtFile.PostedFile.FileName
            If File.Exists(Path) Then
                File.Delete(Path)
            End If
            txtFile.PostedFile.SaveAs(Path)

            'File.Copy(path2, Path)
            'change

            txttablname.Value = ddlTable.SelectedItem.Text
            Dim sSQLTable As String = txttablname.Value
            ' If InStr(LCase(wname), LCase(sSQLTable)) <> 0 Then
            If InStr(LCase(sSQLTable), LCase(sSQLTable)) <> 0 Then
                Dim sExcelFileName As String = filenameOnly
                ' Dim sWorkbook As String = "[" + wname + "$" + "]"
                Dim sWorkbook As String = "[" + sSQLTable + "$" + "]"
                Dim impotrget = classobj.import_data(sSQLTable, sExcelFileName, sWorkbook, Path)

                'If impotrget = "2" Then

                '***********change****************
                '. to access the name of sheet from excel file
                'Dim objExcel As Excel.Application
                'Dim objWorkBook As Excel.Workbook
                'Dim totalWorkSheets As Excel.Worksheet
                'Dim objWorkSheets As Excel.Worksheet
                'Dim ExcelSheetName As String = ""

                'objExcel = CreateObject("Excel.Application")
                'objWorkBook = objExcel.Workbooks.Open("C:\myExcel.xls")

                '' this code gets the names off all the worksheets 
                'For Each totalWorkSheets In myWorkBook.Worksheets
                '    ExcelSheetName += totalWorkSheets.Name
                'Next totalWorkSheets

                '***********change****************


                Dim al As String = Convert.ToString(impotrget)


                If al.Contains("error :") Then
                    Dim strmsgval As String = impotrget.ToString
                    If strmsgval.Contains("PRIMARY") Then
                        Showmsg("Primary key violation!!!!")
                    ElseIf LCase(strmsgval).Contains("open") Then
                        Showmsg("File is already open. close the file first.")
                    ElseIf LCase(strmsgval).Contains("length") Then
                        Showmsg("Length of data should be same as length of table column")
                    ElseIf LCase(strmsgval).Contains("valid") Then
                        Showmsg("The name of data sheet should be same as table name")
                    Else
                        'Response.Write(strmsgval)
                        strmsgval = strmsgval.Replace("'", "")
                        Showmsg(strmsgval)
                    End If




                    Exit Sub
                    ' Showmsg("Please Select File of type Microsoft Excel Workbook to Upload")
                Else
                    Dim cc As String = Convert.ToInt32(impotrget)
                    Dim crdate As Date
                    crdate = Today.Date

                    Dim ds2 As DataSet = New DataSet()
                    Dim da2 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where tabname='" + txttablname.Value + "' and whetherdaily='true' and userid='" + Session("userid") + "'", conn)
                    da2.Fill(ds2)
                    Dim comm2 As SqlCommand
                    Dim dr2 As DataRow
                    If ds2.Tables(0).Rows.Count > 0 Then
                        For Each dr2 In ds2.Tables(0).Rows
                            conn.Open()
                            Dim alertid As String = dr2("AlertID").ToString()
                            comm2 = New SqlCommand("Update alertinfo set Appendtime = '" + crdate + "' where ALertID='" + alertid + "'", conn)
                            comm2.ExecuteNonQuery()
                            conn.Close()
                        Next
                    End If
                    ds2.Dispose()
                    da2.Dispose()
                    Showmsg("Data Imported  Successfully!!!!")

                    'Me.ddlDept.SelectedIndex = 0
                    'code to Track
                    Dim ClientId
                    Dim LobId


                    'If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
                    '    ClientId = "0"
                    'Else
                    '    ClientId = clt.Value
                    'End If
                    'If lob.Value = "--Select--" Or lob.Value = "" Then
                    '    LobId = "0"
                    'Else
                    '    LobId = lob.Value
                    'End If

                    Dim da As SqlDataAdapter = New SqlDataAdapter
                    Dim cmd As SqlCommand
                    cmd = New SqlCommand("Sp_LogDataManager", conn)
                    cmd.CommandType = CommandType.StoredProcedure
                    cmd.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@Action").Value = "Data Import"
                    cmd.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@ActionBy").Value = Session("userId")
                    cmd.Parameters.Add("@Date", SqlDbType.DateTime, 8)
                    cmd.Parameters("@Date").Value = System.DateTime.Now
                    cmd.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@Entity").Value = "Table"
                    cmd.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
                    cmd.Parameters("@EntityName").Value = txttablname.Value
                    cmd.Parameters.Add("@DeptId", SqlDbType.Int, 50)
                    cmd.Parameters("@DeptId").Value = 60
                    cmd.Parameters.Add("@ClientId", SqlDbType.Int, 50)
                    cmd.Parameters("@ClientId").Value = 0
                    cmd.Parameters.Add("@LobId", SqlDbType.Int, 50)
                    cmd.Parameters("@LobId").Value = 0
                    'cmd.Parameters.Add("@count", SqlDbType.Int, 50)
                    'cmd.Parameters("@count").Value = CType(cc, Integer)

                    conn.Open()
                    cmd.ExecuteNonQuery()

                    Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Import' and EntityName='" + txttablname.Value + "'", conn)
                    cmm.ExecuteNonQuery()

                    conn.Close()
                    'End Track
                    'ddlDept.SelectedIndex = 0

                End If

            Else
                Showmsg("Please Select Right Table Name or FileName!!!")
                Me.DepartmentName.SelectedValue = 0
            End If
        Catch ex As Exception
            Showmsg(ex.Message)
        End Try
        If File.Exists(Path) Then
            File.Delete(Path)
        End If
    End Sub

    Protected Sub get_table_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles get_table.Click
        con.Open()
        cmd = New SqlCommand("select * from WARSLOBTableMaster where DepartmentId='60' and ClientId='0' and LOBId='0' and createdBy='" + Session("userid") + "'", con)
        dr = cmd.ExecuteReader()
        ddlTable.DataSource = dr
        ddlTable.DataTextField = "TableName"
        ddlTable.DataValueField = "Tableid"
        ddlTable.DataBind()
        ddlTable.Items.Insert(0, "--select--")
    End Sub

    Protected Sub btnFinalize_singleuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalize_singleuser.Click


        conn.Open()
        Dim result As String = ""
        txttablname.Value = ddlTable.SelectedItem.Text
        Try
            Dim crdate As Date
            crdate = Today.Date
            If txttablname.Value = "" Then
                Showmsg("Please select atleast one table to finalize")
                Exit Sub
            End If

            Dim FromMail As String = ConfigurationSettings.AppSettings("MailFrom").ToString()
            Dim MailServer As String = ConfigurationSettings.AppSettings("MailSMTPServer").ToString()
            Dim folderpath As String = "C:/mailalerts/File"

            Dim ds1 As DataSet = New DataSet()
            Dim da1 As SqlDataAdapter = New SqlDataAdapter("select * from alertinfo where whetherdaily='eve' and userid='" + Session("userid") + "'", conn)
            da1.Fill(ds1)
            Dim dr1 As DataRow
            If ds1.Tables(0).Rows.Count > 0 Then
                For Each dr1 In ds1.Tables(0).Rows
                    Dim alertid As String = dr1("AlertID").ToString()
                    If dr1("whethertable").ToString() = "true" And dr1("tabname").ToString().ToUpper = txttablname.Value.ToUpper Then

                        '''''''''''''''''''''Addddddddddd start
                        'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
                        'check.Parameters.AddWithValue("@alertid", alertid)
                        'Dim cond As String = check.ExecuteScalar().ToString()

                        'Dim dd As New DataSet()
                        'Dim da As New SqlDataAdapter(cond, conn)
                        'da.Fill(dd)
                        'Dim iscondition As Integer = dd.Tables(0).Rows.Count
                        '''''''''''''''''''''Addddddddddd stop

                        Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Parameters.AddWithValue("@alertid", alertid)
                        cmd.Parameters.AddWithValue("@FromMail", FromMail)
                        cmd.Parameters.AddWithValue("@MailServer", MailServer)
                        'smitha
                        'cmd.Parameters.AddWithValue("@folderpath", folderpath)
                        'cmd.Parameters.AddWithValue("@iscondition", iscondition)
                        cmd.ExecuteNonQuery()
                    Else
                        Dim comm1 As New SqlCommand("select tablenames from usp_mailview where alertid='" + alertid + "' and whetherdaily='eve'", conn)
                        Dim tab As String = Convert.ToString(comm1.ExecuteScalar())
                        Dim table As String()
                        table = tab.Split(",")
                        Dim i As Integer
                        For i = 0 To table.Length - 1
                            If table(i).ToUpper = txttablname.Value.ToUpper Then
                                ''''''''''''''''''''Addddddddddd start
                                'Dim check As SqlCommand = New SqlCommand("Select ConditionalQuery FROM AlertQuery WHERE QueryID =(SELECT QueryID FROM AlertInfo WHERE  AlertID=@alertid)", conn)
                                'check.Parameters.AddWithValue("@alertid", alertid)
                                'Dim cond As String = check.ExecuteScalar().ToString()

                                'Dim dd As New DataSet()
                                'Dim da As New SqlDataAdapter(cond, conn)
                                'da.Fill(dd)
                                'Dim iscondition As Integer = dd.Tables(0).Rows.Count
                                '''''''''''''''''''''Addddddddddd stop
                                Dim cmd As New SqlCommand("usp_EventAlertSender", conn)
                                cmd.CommandType = CommandType.StoredProcedure
                                cmd.Parameters.AddWithValue("@alertid", alertid)
                                cmd.Parameters.AddWithValue("@FromMail", FromMail)
                                cmd.Parameters.AddWithValue("@MailServer", MailServer)
                                'cmd.Parameters.AddWithValue("@folderpath", folderpath)
                                'cmd.Parameters.AddWithValue("@iscondition", iscondition)
                                cmd.ExecuteNonQuery()
                            End If
                        Next
                    End If
                Next
                result += "Data has been finalize on table " + txttablname.Value + ""
            Else
                result += "No event alert found on this table."
            End If
            ds1.Dispose()
            da1.Dispose()
            '''''''''''''''''''''''''''''''''''''''''''''''TRACK START

            Dim ClientId
            Dim LobId
            If Me.clt.Value = "--Select--" Or Me.clt.Value = "" Then
                ClientId = "0"
            Else
                ClientId = clt.Value
            End If
            If lob.Value = "--Select--" Or lob.Value = "" Then
                LobId = "0"
            Else
                LobId = lob.Value
            End If
            Dim cmd2 As SqlCommand
            cmd2 = New SqlCommand("Sp_LogDataManagerFinilize", conn)
            cmd2.CommandType = CommandType.StoredProcedure
            cmd2.Parameters.Add("@ActionBy", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@ActionBy").Value = Session("userId")
            cmd2.Parameters.Add("@Action", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@Action").Value = "Data Finilize"
            cmd2.Parameters.Add("@Date", SqlDbType.DateTime, 8)
            cmd2.Parameters("@Date").Value = System.DateTime.Now
            cmd2.Parameters.Add("@Entity", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@Entity").Value = "Table"
            cmd2.Parameters.Add("@EntityName", SqlDbType.NVarChar, 50)
            cmd2.Parameters("@EntityName").Value = txttablname.Value
            cmd2.Parameters.Add("@DeptId", SqlDbType.Int, 50)
            cmd2.Parameters("@DeptId").Value = 60
            cmd2.Parameters.Add("@ClientId", SqlDbType.Int, 50)
            cmd2.Parameters("@ClientId").Value = 0
            cmd2.Parameters.Add("@LobId", SqlDbType.Int, 50)
            cmd2.Parameters("@LobId").Value = 0
            cmd2.Parameters.Add("@MailTo", SqlDbType.NVarChar, 799)
            cmd2.Parameters("@MailTo").Value = mail()
            cmd2.ExecuteNonQuery()
            Dim cmm As New SqlCommand("insert into datamanager_utype select MAX(Autoid)," + Session("usertype") + " from LogDataManager where ActionBy='" + Session("userId") + "' and Action='Data Finilize' and EntityName='" + txttablname.Value + "'", conn)
            cmm.ExecuteNonQuery()
            '''''''''''''''''''''''''''''''''''''''''''''''TRACK END
            Showmsg(result)
            DepartmentName.SelectedIndex = 0
        Catch ex As Exception
            Showmsg(ex.Message)
        Finally
            conn.Close()
        End Try
    End Sub
End Class
