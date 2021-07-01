Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Partial Class TableTools_createandimport
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionstring")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connection3 As New SqlConnection(constr)
    Dim connection4 As New SqlConnection(constr)
    Dim connection5 As New SqlConnection(constr)
    Dim connection6 As New SqlConnection(constr)
    Dim connection7 As New SqlConnection(constr)
    Dim con As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim dr As SqlDataReader
    Dim objds As New DataSet

    Dim classobj As New Functions
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        connection.Close()
        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
                cmdUpload_multiuser.Visible = True
                cmdUpload_singleuser.Visible = False
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
                '        Me.DepartmentName.Attributes.Add("onchange", "getclient();")
                '       Me.Clientname.Attributes.Add("onchange", "GetLOB();")
                '      Me.ddlLobname.Attributes.Add("onchange", "bindlisttab();")
                If (dsar.Tables(0).Rows.Count > 0) Then
                    lblDept.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    lblClient.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    lblLOB.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If

            Else
                Me.spandisplay.Visible = False
                cmdUpload_multiuser.Visible = False
                cmdUpload_singleuser.Visible = True
            End If
            connection.Close()
        End If
        'End If
        con.Close()
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

        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass1))

        If Me.IsPostBack = False Then
            divDatalist.Visible = False
        End If

    End Sub
    Function CSV2DataTable(ByVal filename As String, ByVal inpFile As System.Web.UI.HtmlControls.HtmlInputFile, ByVal sepchar As String) As DataTable
        Dim strFileName As String = ""
        Dim strFileName1 As String = ""
        Dim strNewFileName As String
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
            'Response.Write(strFileName & "<br>")
            txthiddenserver.Text = strNewFileName
            'Response.Write(txthiddenserver.Text & "<br>")
            'Response.End()
            Return CSV2DataTable(strNewFileName, sepchar)
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Function

    Function CSV2DataTable(ByVal filename As String, ByVal sepChar As String) As DataTable
        Dim reader As System.IO.StreamReader
        Dim table As New DataTable
        Dim str1 As String
        Dim colAdded As Boolean = False
        Try
            reader = New System.IO.StreamReader(filename)
            Dim colCount As Integer = 0
            Dim maxcolCount As Integer = 0

            If reader.Peek() >= 0 Then
                Dim tokens As String() = System.Text.RegularExpressions.Regex.Split(reader.ReadLine(), sepChar)
                colCount = UBound(tokens)
                'If Not colAdded Then
                If maxcolCount < colCount Then
                    For i As Integer = 0 To colCount - maxcolCount
                        table.Columns.Add("")
                    Next
                    maxcolCount = colCount
                End If
            End If
            reader.Close()
            reader = New System.IO.StreamReader(filename)
            Dim intloop As Integer
            If reader.Peek() >= 0 Then
                Dim tokens As String() = System.Text.RegularExpressions.Regex.Split(reader.ReadLine(), sepChar)
                Dim row As DataRow = table.NewRow()
                If table.Columns.Count < UBound(tokens) Then
                    intloop = UBound(tokens)
                Else
                    intloop = table.Columns.Count
                End If
                For i As Integer = 0 To intloop - 1
                    row(i) = tokens(i)
                Next
                table.Rows.Add(row)
            End If
            ' Loop
            Return table
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        Finally
            If Not reader Is Nothing Then reader.Close()
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
    Public Sub Showmsg(ByVal strmsg As String)
        Dim str As New System.Text.StringBuilder
        str.Append("<Script language=javascript>")
        str.Append("alert('" + strmsg + "')")
        str.Append("</Script>")
        RegisterStartupScript("Showmsg", str.ToString)
    End Sub
    ' Finction For Checking Special Character
    Public Function CheckSpecialCharacter(ByVal input As String) As Boolean
        Dim IsAllSpecial As Boolean = False
        Dim count As Integer
        For count = 1 To input.Length
            If ((Asc(Mid(input, count, 1)) >= 33 And Asc(Mid(input, count, 1)) <= 47)) Or ((Asc(Mid(input, count, 1)) >= 58 And Asc(Mid(input, count, 1)) <= 64)) Or ((Asc(Mid(input, count, 1)) >= 91 And Asc(Mid(input, count, 1)) <= 96)) Or ((Asc(Mid(input, count, 1)) >= 123 And Asc(Mid(input, count, 1)) <= 126)) Then
                IsAllSpecial = True
            Else
                IsAllSpecial = False
                Exit Function
            End If

        Next
        Return IsAllSpecial
    End Function

    Public Sub Clear()
        txtTablename.Text = ""
    End Sub
    Dim dt As New DataTable
    Dim row1 As DataRow
    Dim colum(10) As DataColumn


    Private Sub cmdGetColumn_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdGetColumn.Click
        If myfile.Value <> "" Then
            Me.txtHidden.Text = myfile.PostedFile.FileName
        End If

        Dim k As Integer = 0
        If myfile.Value = "" Then
            ShowConfirm("Please browse any File to Upload")
        ElseIf Right(myfile.PostedFile.FileName, 3) = "csv" Or Right(myfile.PostedFile.FileName, 3) = "xls" Or Right(myfile.PostedFile.FileName, 3) <> "txt" Then
            ShowConfirm("Please Select Tab Delimited File To Upload")
        Else
            Try
                Dim strFileName As String
                strFileName = myfile.PostedFile.FileName
                Dim table As DataTable = CSV2DataTable(strFileName, myfile, vbTab)
                Dim str0 As String = ""
                Dim Cmd9 As New SqlCommand("delete warstableschema where CreatedBy='" & Session("userid") & "'", connection)
                connection.Open()
                Cmd9.ExecuteNonQuery()
                connection.Close()
                Cmd9.Dispose()
                For Each row As DataRow In table.Rows
                    If k <> 1 Then
                        For i As Integer = 0 To table.Columns.Count - 1
                            row(i) = Replace(row(i), ".", "")
                            row(i) = Replace(row(i), " ", "_")
                            row(i) = Replace(row(i), "'", "''")
                            str0 = "'" & row(i) & "'" & "," & "'" & "VARCHAR" & "'" & "," & "100" & "," & "'" & Session("userid1") & "'"

                            Dim cmd As New SqlCommand("insertwarstableschema", connection)
                            cmd.CommandType = CommandType.StoredProcedure
                            With cmd.Parameters
                                .AddWithValue("@colname", row(i))
                                .AddWithValue("@CreatedBy", Session("userid1"))
                            End With
                            connection.Open()
                            cmd.ExecuteNonQuery()
                            connection.Close()
                            cmd.Dispose()
                            str0 = ""
                        Next
                        k = k + 1
                    End If
                Next
                dbregInfo(dlreg, "warstableschema")
                Me.divDatalist.Visible = True
            Catch ex As Exception
                Dim strmsg As String
                strmsg = Replace(ex.Message.ToString, "'", "")
                strmsg = Replace(strmsg, vbCrLf, " ")
                ShowConfirm(strmsg)
            End Try
        End If
    End Sub
    Function GetDataType() As DataSet
        Dim cboDataSetEx As New DataSet
        'Const strSQLDDL = "select distinct(type) from datatypes"
        Const strSQLDDL = "select distinct(type),rec_id from datatypes order by rec_id"
        Dim objTestEx As New SqlCommand(strSQLDDL, connection)
        Dim myDataAdapter As SqlDataAdapter = New SqlDataAdapter(strSQLDDL, connection)
        myDataAdapter.SelectCommand = objTestEx
        connection.Open()
        myDataAdapter.Fill(cboDataSetEx, "datatypes")
        connection.Close()
        Return cboDataSetEx
        objTestEx.Dispose()
    End Function

    Public Sub dbregInfo(ByVal control As WebControls.DataList, ByVal table As String)
        Dim objcmd As New SqlCommand("select * from warstableschema where CreatedBy='" & Session("userid1") & "'", connection)
        Dim objadp As New SqlDataAdapter
        objadp.SelectCommand = objcmd
        connection.Open()
        objadp.Fill(objds, "table")
        connection.Close()
        control.DataSource = objds
        control.DataBind()
        objcmd.Dispose()
    End Sub

    Private Sub dlreg_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.EditCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        dbregInfo(dlreg, "warstableschema")
    End Sub

    Private Sub dlreg_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.CancelCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        Me.dlreg.EditItemIndex = -1
        dbregInfo(dlreg, "warstableschema")
    End Sub

    Private Sub dlreg_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.UpdateCommand
        Dim strColname As String
        Dim strdataType As String
        Dim strsize As String
        Dim intrec As Integer
        strColname = CType(e.Item.FindControl("txtCol"), TextBox).Text

        If CheckSpecialCharacter(strColname) = True Then
            strColname = ""
            CType(e.Item.FindControl("txtCol"), TextBox).Text = ""
            Showmsg("Please Enter Valid Column Name")
            Exit Sub
        End If
        strdataType = CType(e.Item.FindControl("cboEx"), DropDownList).SelectedItem.Value
        strsize = CType(e.Item.FindControl("txtsize"), TextBox).Text

        If strsize.TrimStart("0").Length > 4 And strdataType = "VARCHAR" Then
            strsize = ""
            CType(e.Item.FindControl("txtsize"), TextBox).Text = ""
            Showmsg("Please enter valid column size less then 8000")
            Exit Sub
        End If

        If Convert.ToInt64(strsize) > 8000 And strdataType = "VARCHAR" Then
            strsize = ""
            CType(e.Item.FindControl("txtsize"), TextBox).Text = ""
            Showmsg("Please enter valid column size less then 8000")
            Exit Sub
        End If

        'intRecId = dgChMaster.DataKeys(e.Item.ItemIndex)
        intrec = dlreg.DataKeys(e.Item.ItemIndex)

        ''''''''''''''''''Update the structure of table''''''''''''''''''''''''
        Try
            If strColname = "" Then
                Showmsg("Column Name can not be blank!")

            Else
                If UCase(strdataType) = "DATETIME" Or UCase(strdataType) = "NUMERIC" Or UCase(strdataType) = "FLOAT" Then
                    e.Item.FindControl("txtsize").Visible = False
                    strsize = 8
                    Dim query As String = "update warstableschema set colname='" & strColname & "' , datatype='" & strdataType & "' , size='" & strsize & "' where autoid=' " & intrec & "' "
                    Dim objcmd1 As New SqlCommand(query, connection)
                    connection.Open()
                    objcmd1.ExecuteNonQuery()
                    connection.Close()
                    ShowConfirm("Table Updated successfully|||||")
                    dlreg.EditItemIndex = -1
                    dbregInfo(dlreg, "warstableschema")
                Else
                    If UCase(strdataType) = "VARCHAR" And Not IsNumeric(UCase(strsize)) Then
                        ShowConfirm("Please Enter valid Size")
                        Exit Sub
                    Else
                        'If strsize = "" Then
                        '    ShowConfirm("Please Enter Size") Commented On Date 19/09/2008
                        'Else
                        Dim query As String = "update warstableschema set colname='" & strColname & "' , datatype='" & strdataType & "' , size='" & strsize & "' where autoid=' " & intrec & "' "
                        Dim objcmd1 As New SqlCommand(query, connection)
                        connection.Open()
                        objcmd1.ExecuteNonQuery()
                        connection.Close()
                        ShowConfirm("Table Updated successfully|||||")
                        dlreg.EditItemIndex = -1
                        dbregInfo(dlreg, "warstableschema")

                    End If
                End If
            End If


        Catch ex As Exception
            ShowConfirm(ex.ToString())
        End Try
        'End If




        '*********************Commented As Per rquirment on 08/072008 by rohit**************

        'If UCase(strdataType) = "DATETIME" Then
        '    e.Item.FindControl("txtsize").Visible = False
        '    strsize = 8
        '    '(e.Item.FindControl("txtsize"), TextBox).Text = 8
        '    'End If
        'Else
        '    If UCase(strdataType) = "NUMERIC" Then
        '        e.Item.FindControl("txtsize").Visible = False
        '        strsize = 8
        '        '(e.Item.FindControl("txtsize"), TextBox).Text = 8
        '        'End If
        '    Else

        '        If UCase(strdataType) = "FLOAT" Then
        '            e.Item.FindControl("txtsize").Visible = False
        '            strsize = 8
        '            '(e.Item.FindControl("txtsize"), TextBox).Text = 8
        '            'End If
        '        Else


        '            If UCase(strdataType) = "VARCHAR" Then
        '                If strsize = "" Then
        '                    Showmsg("Size Can not be blank!")

        '                Else
        '                    Dim query As String = "update warstableschema set colname='" & strColname & "' , datatype='" & strdataType & "' , size='" & strsize & "' where autoid=' " & intrec & "' "

        '                    Dim objcmd1 As New SqlCommand(query, connection)
        '                    connection.Open()
        '                    objcmd1.ExecuteNonQuery()
        '                    connection.Close()

        '                End If
        '            End If
        '        End If
        '    End If
        'End If


        ''ShowConfirm("Table Updated successfully|||||")
        'dlreg.EditItemIndex = -1
        'dbregInfo(dlreg, "warstableschema")
    End Sub

    Private Sub cmdUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload_singleuser.Click
        Dim maxvalue As Integer
        Dim constraintname As String
        Dim constriantnamefinal As String

        txtTablename.Text = Replace(txtTablename.Text, " ", "")

        ' Finding The Highest Value Of Table
        Dim maxcount As New SqlCommand("select max (tableid+1) from WARSLOBTableMaster", connection)
        connection.Open()
        If IsDBNull(maxcount.ExecuteScalar) Then
            maxvalue = 1
        Else
            maxvalue = maxcount.ExecuteScalar
        End If

        connection.Close()
        constraintname = "Constraint pk_prim" & maxvalue & DateTime.Now.Millisecond
        'dim lob as String=
        If Me.txtTablename.Text <> "" Then
            Dim objCmd As New SqlCommand("select name from sysobjects where xtype='U' and name='" & Me.txtTablename.Text & "'", connection3)
            Dim dr1 As SqlDataReader
            connection3.Open()
            dr1 = objCmd.ExecuteReader
            If dr1.Read = True Then
                ShowConfirm("Table Name Already Exist So Please Enter Other Name")
                Me.txtTablename.Text = ""
                SetFocus(Me.txtTablename)
                connection3.Close()
                dr1.Close()
                objCmd.Dispose()
            Else
                Try
                    Dim j As Integer
                    Dim k As Integer = 0
                    For j = 0 To dlreg.Items.Count - 1
                        'intrec = Me.dlreg.DataKeys(Me.dlreg.Items(j).ItemIndex)

                        If CType(Me.dlreg.Items(j).FindControl("chkVisible"), CheckBox).Checked = True Then
                            k = k + 1
                        End If
                    Next
                    If k = 0 Then
                        ShowConfirm("Please select at least one column to create table!!")
                    Else
                        '*******************For Selecting At least on column As Primary
                        Dim j1 As Integer
                        Dim k1 As Integer = 0
                        Dim counter As Integer = 0
                        Dim primcol As String = ""
                        Dim primcol1 As String

                        For j1 = 0 To dlreg.Items.Count - 1
                            If CType(Me.dlreg.Items(j1).FindControl("chkprimary"), CheckBox).Checked = True Then
                                If primcol = "" Then
                                    primcol = CType(Me.dlreg.Items(j1).FindControl("Label1"), Label).Text
                                Else
                                    primcol = (primcol + "," + CType(Me.dlreg.Items(j1).FindControl("Label1"), Label).Text)
                                End If
                                k1 = k1 + 1
                            End If
                        Next
                        '****************Making Column Primary
                        If k1 = 1 Then
                            primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"
                        Else
                            primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"

                        End If
                        Dim j2 As Integer
                        Dim k2 As Integer = 0
                        Dim str As String = ""
                        Dim objupload As New SqlCommand("select * from warstableschema where CreatedBy='" & Session("Userid1") & "'", connection)
                        Dim objreader As SqlDataReader
                        connection.Open()
                        objreader = objupload.ExecuteReader




                        '******************change*************************
                        'intrec = Me.dlreg.DataKeys(Me.dlreg.Items(j).ItemIndex)
                        For j2 = 0 To dlreg.Items.Count - 1
                            Do While objreader.Read
                                If CType(Me.dlreg.Items(j2).FindControl("chkVisible"), CheckBox).Checked = True Then

                                    If str = "" Then
                                        If (UCase(objreader("datatype")) = "DATETIME") Or (UCase(objreader("datatype")) = "FLOAT") Then
                                            str = objreader("colname") & " " & objreader("datatype")
                                        Else
                                            Try


                                                Dim intchk As Integer = objreader("size")
                                                If intchk > 8000 Then
                                                    ShowConfirm("Lenght must less than 8001")
                                                    Exit Sub
                                                End If
                                            Catch ex As Exception
                                                ShowConfirm("Lenght value must numeric")
                                                Exit Sub
                                            End Try
                                            str = objreader("colname") & " " & objreader("datatype") & "(" & objreader("size") & ")"
                                        End If
                                    Else
                                        If (UCase(objreader("datatype")) = "DATETIME") Or (UCase(objreader("datatype")) = "FLOAT") Then
                                            str = str & "," & objreader("colname") & " " & objreader("datatype")
                                        Else
                                            Try


                                                Dim intchk As Integer = objreader("size")
                                                If intchk > 8000 Then
                                                    ShowConfirm("Lenght must less than 8001")
                                                    Exit Sub
                                                End If
                                            Catch ex As Exception
                                                ShowConfirm("Lenght value must numeric")
                                                Exit Sub
                                            End Try
                                            str = str & "," & objreader("colname") & " " & objreader("datatype") & "(" & objreader("size") & ")"
                                        End If
                                    End If

                                End If
                                j2 = j2 + 1
                            Loop
                            'Response.Write(objreader("datatype"))

                            '*********************change*******************************
                        Next




                        '***************For Ading Column As Primary
                        If k1 <> 0 Then
                            If k1 = 1 Then
                                str = str & " " & primcol1
                                constriantnamefinal = constraintname
                            Else
                                str = str & " " & primcol1
                                constriantnamefinal = constraintname
                            End If
                        Else
                            str = str
                            constriantnamefinal = "NULL"
                        End If
                        '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                        Dim Cmd As New SqlCommand("create table " & Me.txtTablename.Text & "(" & str & ")", connection1)
                        connection1.Open()
                        Cmd.ExecuteNonQuery()
                        connection1.Close()
                        Cmd.Dispose()
                        ShowConfirm("Table Created Succesfully!!!!")

                        divDatalist.Visible = False
                        '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                        Dim visbcol As String = ""
                        Dim i As Integer
                        Dim chk
                        Dim intrec
                        Dim strusername As String = ""
                        For i = 0 To dlreg.Items.Count - 1
                            intrec = Me.dlreg.DataKeys(Me.dlreg.Items(i).ItemIndex)
                            If CType(Me.dlreg.Items(i).FindControl("chkVisible"), CheckBox).Checked = True Then
                                Dim objchk As New SqlCommand("select colname from warstableschema where autoid='" & intrec & "'", connection4)

                                Dim rdrchk As SqlDataReader
                                connection4.Open()
                                rdrchk = objchk.ExecuteReader
                                If rdrchk.Read Then
                                    If visbcol = "" Then
                                        visbcol = rdrchk("colname")
                                    Else
                                        visbcol = visbcol & "," & rdrchk("colname")
                                    End If
                                End If
                                connection4.Close()
                                objchk.Dispose()
                                rdrchk.Close()
                            End If
                        Next

                        ''''''''''''''''entry of WARSLOBTableMaster table''''''''''''''''

                        Dim cmdin As New SqlCommand
                        cmdin.CommandType = CommandType.StoredProcedure
                        cmdin.CommandText = "insert_WARSLOBTableMaster"
                        cmdin.Connection = connection2
                        With cmdin.Parameters
                            If ddlLobname.SelectedValue = "" Or ddlLobname.SelectedValue = "--Select--" Then
                                .AddWithValue("@LobId", "0")
                            Else
                                .AddWithValue("@LobId", "0")
                            End If
                            .AddWithValue("@TableName", Me.txtTablename.Text)
                            .AddWithValue("@CreatedOn", System.DateTime.Today)
                            .AddWithValue("@CreatedBy", Session("Userid1"))
                            .AddWithValue("@LastModified", System.DateTime.Today)
                            .AddWithValue("@LastModifiedBy", Session("Userid1"))
                            .AddWithValue("@currdate", System.DateTime.Today)
                            .AddWithValue("@visiblecolumn", visbcol)
                            .AddWithValue("@DepartmentId", "60")

                            If Clientname.SelectedValue = "--Select--" Then
                                .AddWithValue("@ClientId", "0")
                            Else
                                .AddWithValue("@ClientId", "0")
                            End If
                            'If chkSelectscope.Checked = True Then
                            '    .AddWithValue("@Local", "Local")
                            'Else
                            .AddWithValue("@Local", "NonLocal")
                            ' End If
                            .AddWithValue("@primcol", primcol)
                            .AddWithValue("@constraintname", constriantnamefinal)
                        End With
                        connection2.Open()
                        cmdin.ExecuteNonQuery()
                        connection2.Close()
                        cmdin.Dispose()


                        connection2.Open()
                        Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txtTablename.Text + "' and Action='Save'", connection2)
                        cmm.ExecuteNonQuery()
                        connection2.Close()



                        '************************************ Entry In TableRights
                        Dim tabid1 As Integer
                        Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txtTablename.Text & "'", connection1)
                        connection1.Open()
                        tabid1 = tabid.ExecuteScalar
                        connection1.Close()
                        tabid.Dispose()

                        '************************************Delete Schema of Table

                        Dim Cmddel As New SqlCommand("delete warstableschema where CreatedBy='" & Session("userid1") & "'", connection1)
                        'Response.Write("create table " & Me.txtTablename.Text & "(" & str & ")")
                        'Response.End()
                        connection1.Open()
                        Cmddel.ExecuteNonQuery()
                        connection1.Close()
                        Cmddel.Dispose()
                    End If

                Catch ex As Exception
                    Dim dg As String = ex.ToString
                    Dim r As String = dg.Replace("(", "")
                    r = r.Replace(vbNewLine, "")
                    r = r.Replace("'", "")
                    Dim kol As Integer = r.Length
                    If kol > 10 Then
                        'r = r.Substring(0, 200)
                    End If
                    ShowConfirm(r)
                    'ShowConfirm("Incorrect Syntax")
                End Try
            End If
            connection3.Close()
            dr1.Close()
            objCmd.Dispose()
        Else
            ShowConfirm("Please enter table name")
            SetFocus(txtTablename)
        End If
    End Sub
    Private Sub UpdateFileFormat(ByVal colCount As Integer, ByVal filePath As String)
        Dim strNewFileName As String = Server.MapPath(filePath)
        Dim strFile As New System.Text.StringBuilder
        Dim reader As System.IO.StreamReader
        reader = New System.IO.StreamReader(strNewFileName)
        'Dim dadpter As New SqlDataAdapter("select column_name from information_schema.columns where table_name='" + txtTablename.Text + "'", connection)
        'Dim datra As New DataSet

        'dadpter.Fill(datra)
        'Dim column As String = ""
        'Dim allcnt As Integer = 0
        'If datra.Tables(0).Rows.Count > 0 Then
        '    allcnt = datra.Tables(0).Rows.Count
        'End If
        'Dim jh As Integer = 0
        'For jh = 0 To allcnt - 1
        '    If column = "" Then
        '        column = datra.Tables(0).Rows(jh)("column_name").ToString
        '    Else
        '        column = column + "," + datra.Tables(0).Rows(jh)("column_name").ToString
        '    End If
        'Next
        'Dim allsplit = column.Split(",")
        'jh = 0
        'Dim chk As String = ""
        Do While reader.Peek() > 0

            'Dim b As Boolean = False
            'Dim tkn1 As String = ""
            Dim tokens As String = reader.ReadLine()
            'If chk = "" Then
            '    tkn1 = tokens
            '    chk = "any"
            'End If
            'Dim tkn = tkn1.Split(vbTab)
            'Dim kl As Integer = 0
            'For kl = 0 To UBound(tkn)
            '    For jh = 0 To UBound(allsplit)
            '        If Trim(LCase(tkn(kl))) = Trim(LCase(allsplit(jh))) Then
            '            b = True
            '            Exit For
            '        Else
            '            Exit For
            '        End If
            '    Next
            'Next
            'If b = True Then


            Dim tokens1 As String = ""
            Dim arr
            Dim intCols, i As Integer
            arr = Split(tokens, vbTab)
            intCols = UBound(arr)

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
            'End If
        Loop
        reader.Close()
        Dim fp As StreamWriter
        fp = File.CreateText(Server.MapPath(filePath))
        fp.WriteLine(strFile)
        fp.Close()

    End Sub

    Private Sub cmdImport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImport.Click
        Dim k As Integer = 0
        Dim p As Integer = 0

        If Right(Me.txtHidden.Text, 3) = "csv" Or Right(Me.txtHidden.Text, 3) = "xls" Then
            ShowConfirm("Select File of type Tab Delimited to Upload")
        Else
            Try
                'Dim strFileName As String
                'strFileName = myfile.PostedFile.FileName
                Dim table As DataTable = CSV2DataTable(Me.txthiddenserver.Text, vbTab)
                Dim objcmd As New SqlCommand("select count(name) as ColumnNumber from syscolumns where id=(select id from sysobjects where xtype='U' and name='" & Me.txtTablename.Text & "')", connection2)
                Dim dr1 As SqlDataReader
                connection2.Open()
                dr1 = objcmd.ExecuteReader
                If dr1.Read = True Then
                    Dim ColCount As Integer = dr1("ColumnNumber")
                    Dim filePath As String = "Uploads\" & txthid1.Text
                    UpdateFileFormat(ColCount, filePath)
                    Dim str As String()
                    Dim str0 As String = ""
                    Dim Cmd9 As New SqlCommand("delete " & Me.txtTablename.Text & "", connection)
                    connection.Open()
                    Cmd9.ExecuteNonQuery()
                    connection.Close()
                    Cmd9.Dispose()
                    ''''''''''''''''''''For Each row As DataRow In table.Rows
                    ''''''''''''''''''''    If k <> 0 Then
                    ''''''''''''''''''''        For i As Integer = 0 To table.Columns.Count - 1
                    ''''''''''''''''''''            row(i) = Replace(row(i), "'", "''")
                    ''''''''''''''''''''            'row(i) = Replace(row(i), ",", "-")

                    ''''''''''''''''''''            If str0 = "" Then
                    ''''''''''''''''''''                str0 = "'" & row(i) & "'" '" " & Me.txtcol.Text & "    " & Me.ddlDatatype.SelectedValue & " (" & Me.txtsize.Text & ")"
                    ''''''''''''''''''''            Else
                    ''''''''''''''''''''                str0 = str0 & "," & "'" & row(i) & "'"
                    ''''''''''''''''''''            End If
                    ''''''''''''''''''''        Next
                    ''''''''''''''''''''        ''''''''''''''Start to send the data in table''''''''''''''''''''''''
                    ''''''''''''''''''''        Dim Cmd As New SqlCommand("insert into " & Me.txtTablename.Text & " values(" & str0 & ")", connection)
                    ''''''''''''''''''''        p = p + 1
                    ''''''''''''''''''''        connection.Open()
                    ''''''''''''''''''''        Cmd.ExecuteNonQuery()
                    ''''''''''''''''''''        connection.Close()
                    ''''''''''''''''''''        Cmd.Dispose()
                    ''''''''''''''''''''        str0 = ""
                    ''''''''''''''''''''        'Dim cmd As New SqlCommand("BULK INSERT testmaster FROM '\\bmp34\c\Inetpub\wwwroot\IDMS\Table_Management\Uploads\old1.TXT' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                    ''''''''''''''''''''        'Response.Write("BULK INSERT testmaster FROM '\\bmp34\c\Inetpub\wwwroot\IDMS\Table_Management\Uploads\old1.TXT' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n' )")
                    ''''''''''''''''''''        'Response.End()
                    ''''''''''''''''''''    End If

                    ''''''''''''''''''''    k = k + 1
                    ''''''''''''''''''''Next
                    ''''''''''''''''''''importData

                    'It is commented as per my testing:By Rohit

                    Dim strUploadPath = Server.MapPath("~/TableTools/Uploads/" & txthid1.Text & "")
                    Dim cmd As New SqlCommand("BULK INSERT " & Me.txtTablename.Text & " FROM '" & Trim(strUploadPath) & "' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                    'Dim cmd As New SqlCommand("BULK INSERT " & Me.txtTablename.Text & " FROM '\\bmp34\c\Inetpub\wwwroot\IDMS\Table_Management\Uploads\" & txthid1.Text & " ' WITH ( FIRSTROW = 2, FIELDTERMINATOR = '\t', ROWTERMINATOR = '\n')", connection)
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()
                    cmd.Dispose()
                    ShowConfirm("Data Uploaded Succesfully!!!!")
                    txthiddenserver.Text = ""

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
                Try
                    Dim Cmd11 As New SqlCommand("delete " & Me.txtTablename.Text & "", connection6)
                    connection6.Open()
                    Cmd11.ExecuteNonQuery()
                    connection6.Close()
                    Cmd11.Dispose()
                Catch ex1 As Exception
                    Dim strmsg1 As String
                    strmsg1 = Replace(ex1.Message.ToString, "'", "")
                    strmsg1 = Replace(strmsg1, vbCrLf, " ")
                    ShowConfirm(strmsg1)
                End Try
            Finally
                connection2.Close()
                connection.Close()
                connection5.Close()
                connection6.Close()

            End Try
            '**************For Track On Date 25/09/2008 
            Try
                Dim cmdtrack As New SqlCommand("sp_LogTableToolForImport", connection)
                cmdtrack.CommandType = CommandType.StoredProcedure
                With cmdtrack.Parameters
                    .Add("@TableName", Me.txtTablename.Text)
                    .Add("@CreatedBy", Session("userid"))
                End With
                connection.Open()
                cmdtrack.ExecuteNonQuery()
                connection.Close()
                cmdtrack.Dispose()

                connection.Open()
                Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txtTablename.Text + "' and Action='Import'", connection)
                cmm.ExecuteNonQuery()
                connection.Close()

            Catch ex As Exception
                Dim strmsg1 As String
                strmsg1 = Replace(ex.Message.ToString, "'", "")
                strmsg1 = Replace(strmsg1, vbCrLf, " ")
                ShowConfirm(strmsg1)
            End Try
            '******************End Track
            Clear()
        End If

    End Sub
    Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
        ddlLobname.Items.Clear()
        Clientname.Items.Clear()
        If (DepartmentName.SelectedValue = "--Select--") Then
            ddlLobname.Items.Clear()
            Clientname.Items.Clear()
        Else
            con.Close()
            con.Open()
            cmd = New SqlCommand("select autoid,ClientName from IdmsClient where deptid='" + DepartmentName.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            Clientname.DataSource = dr
            Clientname.DataTextField = "ClientName"
            Clientname.DataValueField = "autoid"
            Clientname.DataBind()
            Clientname.Items.Insert(0, "--Select--")
            ddlLobname.Items.Clear()
        End If
            
    End Sub

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
        If Clientname.SelectedValue = "--Select--" Or Clientname.SelectedValue = "" Then
            ddlLobname.Items.Clear()
        Else
            con.Open()
            cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + Clientname.SelectedValue + "'", con)
            dr = cmd.ExecuteReader()
            ddlLobname.DataSource = dr
            ddlLobname.DataTextField = "LOBName"
            ddlLobname.DataValueField = "autoid"
            ddlLobname.DataBind()
            ddlLobname.Items.Insert(0, "--Select--")
            con.Close()
        End If

    End Sub
    Private Sub cmdCancel1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel1.Click
        divDatalist.Visible = False
        Dim Cmddel1 As New SqlCommand("delete warstableschema where CreatedBy='" & Session("userid1") & "' ", connection1)
        connection1.Open()
        Cmddel1.ExecuteNonQuery()
        connection1.Close()
        Cmddel1.Dispose()
        txtHidden.Text = ""
        txthiddenserver.Text = ""

    End Sub

    Private Sub dlreg_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.ItemCommand
        If e.CommandName = "Select All" Then
            '''''''To Select all items
            Dim myDataGridItem As DataListItem
            For Each myDataGridItem In dlreg.Items
                CType(myDataGridItem.FindControl("chkVisible"), CheckBox).Checked = True
            Next
        End If
        If e.CommandName = "DeSelect All" Then
            '''''''To Select all items
            Dim myDataGridItem As DataListItem
            For Each myDataGridItem In dlreg.Items
                CType(myDataGridItem.FindControl("chkVisible"), CheckBox).Checked = False
            Next
        End If
    End Sub

    Private Sub dlreg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlreg.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim txt As TextBox
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim current As String = CType(drv("colname"), String)
            txt = CType(e.Item.FindControl("txtCol"), TextBox)
            SetFocus(txt)
        End If
    End Sub

    Protected Sub cmdUpload_multiuser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdUpload_multiuser.Click
        Dim maxvalue As Integer
        Dim constraintname As String
        Dim constriantnamefinal As String

        If Me.DepartmentName.SelectedIndex <> 0 Then
            txtTablename.Text = Replace(txtTablename.Text, " ", "")

            ' Finding The Highest Value Of Table
            Dim maxcount As New SqlCommand("select max (tableid+1) from WARSLOBTableMaster", connection)
            connection.Open()
            If IsDBNull(maxcount.ExecuteScalar) Then
                maxvalue = 1
            Else
                maxvalue = maxcount.ExecuteScalar
            End If

            connection.Close()
            constraintname = "Constraint pk_prim" & maxvalue & DateTime.Now.Millisecond
            'dim lob as String=
            If Me.txtTablename.Text <> "" Then
                Dim objCmd As New SqlCommand("select name from sysobjects where xtype='U' and name='" & Me.txtTablename.Text & "'", connection3)
                Dim dr1 As SqlDataReader
                connection3.Open()
                dr1 = objCmd.ExecuteReader
                If dr1.Read = True Then
                    ShowConfirm("Table Name Already Exist So Please Enter Other Name")
                    Me.txtTablename.Text = ""
                    SetFocus(Me.txtTablename)
                    connection3.Close()
                    dr1.Close()
                    objCmd.Dispose()
                Else
                    Try
                        Dim j As Integer
                        Dim k As Integer = 0
                        For j = 0 To dlreg.Items.Count - 1
                            'intrec = Me.dlreg.DataKeys(Me.dlreg.Items(j).ItemIndex)

                            If CType(Me.dlreg.Items(j).FindControl("chkVisible"), CheckBox).Checked = True Then
                                k = k + 1
                            End If
                        Next
                        If k = 0 Then
                            ShowConfirm("Please select at least one column to create table!!")
                        Else
                            '*******************For Selecting At least on column As Primary
                            Dim j1 As Integer
                            Dim k1 As Integer = 0
                            Dim counter As Integer = 0
                            Dim primcol As String = ""
                            Dim primcol1 As String

                            For j1 = 0 To dlreg.Items.Count - 1
                                If CType(Me.dlreg.Items(j1).FindControl("chkprimary"), CheckBox).Checked = True Then
                                    If primcol = "" Then
                                        primcol = CType(Me.dlreg.Items(j1).FindControl("Label1"), Label).Text
                                    Else
                                        primcol = (primcol + "," + CType(Me.dlreg.Items(j1).FindControl("Label1"), Label).Text)
                                    End If
                                    k1 = k1 + 1
                                End If
                            Next
                            '****************Making Column Primary
                            If k1 = 1 Then
                                primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"
                            Else
                                primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"

                            End If
                            Dim j2 As Integer
                            Dim k2 As Integer = 0
                            Dim str As String = ""
                            Dim objupload As New SqlCommand("select * from warstableschema where CreatedBy='" & Session("Userid1") & "'", connection)
                            Dim objreader As SqlDataReader
                            connection.Open()
                            objreader = objupload.ExecuteReader




                            '******************change*************************
                            'intrec = Me.dlreg.DataKeys(Me.dlreg.Items(j).ItemIndex)
                            For j2 = 0 To dlreg.Items.Count - 1
                                Do While objreader.Read
                                    If CType(Me.dlreg.Items(j2).FindControl("chkVisible"), CheckBox).Checked = True Then

                                        If str = "" Then
                                            If (UCase(objreader("datatype")) = "DATETIME") Or (UCase(objreader("datatype")) = "FLOAT") Then
                                                str = objreader("colname") & " " & objreader("datatype")
                                            Else
                                                Try


                                                    Dim intchk As Integer = objreader("size")
                                                    If intchk > 8000 Then
                                                        ShowConfirm("Lenght must less than 8001")
                                                        Exit Sub
                                                    End If
                                                Catch ex As Exception
                                                    ShowConfirm("Lenght value must numeric")
                                                    Exit Sub
                                                End Try
                                                str = objreader("colname") & " " & objreader("datatype") & "(" & objreader("size") & ")"
                                            End If
                                        Else
                                            If (UCase(objreader("datatype")) = "DATETIME") Or (UCase(objreader("datatype")) = "FLOAT") Then
                                                str = str & "," & objreader("colname") & " " & objreader("datatype")
                                            Else
                                                Try


                                                    Dim intchk As Integer = objreader("size")
                                                    If intchk > 8000 Then
                                                        ShowConfirm("Lenght must less than 8001")
                                                        Exit Sub
                                                    End If
                                                Catch ex As Exception
                                                    ShowConfirm("Lenght value must numeric")
                                                    Exit Sub
                                                End Try
                                                str = str & "," & objreader("colname") & " " & objreader("datatype") & "(" & objreader("size") & ")"
                                            End If
                                        End If

                                    End If
                                    j2 = j2 + 1
                                Loop
                                'Response.Write(objreader("datatype"))

                                '*********************change*******************************
                            Next




                            '***************For Ading Column As Primary
                            If k1 <> 0 Then
                                If k1 = 1 Then
                                    str = str & " " & primcol1
                                    constriantnamefinal = constraintname
                                Else
                                    str = str & " " & primcol1
                                    constriantnamefinal = constraintname
                                End If
                            Else
                                str = str
                                constriantnamefinal = "NULL"
                            End If
                            '''''''''''''''''''''''''''''''''''Create table''''''''''''''''''''''''''''''
                            Dim Cmd As New SqlCommand("create table " & Me.txtTablename.Text & "(" & str & ")", connection1)
                            connection1.Open()
                            Cmd.ExecuteNonQuery()
                            connection1.Close()
                            Cmd.Dispose()
                            ShowConfirm("Table Created Succesfully!!!!")

                            divDatalist.Visible = False
                            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                            Dim visbcol As String = ""
                            Dim i As Integer
                            Dim chk
                            Dim intrec
                            Dim strusername As String = ""
                            For i = 0 To dlreg.Items.Count - 1
                                intrec = Me.dlreg.DataKeys(Me.dlreg.Items(i).ItemIndex)
                                If CType(Me.dlreg.Items(i).FindControl("chkVisible"), CheckBox).Checked = True Then
                                    Dim objchk As New SqlCommand("select colname from warstableschema where autoid='" & intrec & "'", connection4)

                                    Dim rdrchk As SqlDataReader
                                    connection4.Open()
                                    rdrchk = objchk.ExecuteReader
                                    If rdrchk.Read Then
                                        If visbcol = "" Then
                                            visbcol = rdrchk("colname")
                                        Else
                                            visbcol = visbcol & "," & rdrchk("colname")
                                        End If
                                    End If
                                    connection4.Close()
                                    objchk.Dispose()
                                    rdrchk.Close()
                                End If
                            Next

                            ''''''''''''''''entry of WARSLOBTableMaster table''''''''''''''''
                            'Dim clientid, lobid As Integer
                            Dim cmdin As New SqlCommand
                            cmdin.CommandType = CommandType.StoredProcedure
                            cmdin.CommandText = "insert_WARSLOBTableMaster"
                            cmdin.Connection = connection2
                            With cmdin.Parameters
                                If ddlLobname.SelectedValue = "" Or ddlLobname.SelectedValue = "--Select--" Then
                                    .AddWithValue("@LobId", 0)
                                Else
                                    .AddWithValue("@LobId", ddlLobname.SelectedValue)
                                End If
                                .AddWithValue("@TableName", Me.txtTablename.Text)
                                .AddWithValue("@CreatedOn", System.DateTime.Today)
                                .AddWithValue("@CreatedBy", Session("Userid1"))
                                .AddWithValue("@LastModified", System.DateTime.Today)
                                .AddWithValue("@LastModifiedBy", Session("Userid1"))
                                .AddWithValue("@currdate", System.DateTime.Today)
                                .AddWithValue("@visiblecolumn", visbcol)
                                .AddWithValue("@DepartmentId", DepartmentName.SelectedValue)

                                If Clientname.SelectedValue = "--Select--" Then
                                    .AddWithValue("@ClientId", 0)
                                Else
                                    .AddWithValue("@ClientId", Clientname.SelectedValue)
                                End If
                                .AddWithValue("@Local", "NonLocal")
                                'End If
                                .AddWithValue("@primcol", primcol)
                                .AddWithValue("@constraintname", constriantnamefinal)
                            End With
                            connection2.Open()
                            cmdin.ExecuteNonQuery()
                            connection2.Close()
                            cmdin.Dispose()


                            connection2.Open()
                            Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txtTablename.Text + "' and Action='Save'", connection2)
                            cmm.ExecuteNonQuery()
                            connection2.Close()



                            '************************************ Entry In TableRights
                            Dim tabid1 As Integer
                            Dim tabid As New SqlCommand("Select distinct TableId from warslobtablemaster where Tablename='" & txtTablename.Text & "'", connection1)
                            connection1.Open()
                            tabid1 = tabid.ExecuteScalar
                            connection1.Close()
                            tabid.Dispose()

                            Dim cmdsave As New SqlCommand("insert_tablerights", connection1)
                            cmdsave.CommandType = CommandType.StoredProcedure
                            With cmdsave.Parameters
                                .Add("@TableId", tabid1)
                                .Add("@currdate", System.DateTime.Now.ToString("d"))
                                .Add("@UserId", Session("Userid1"))
                                .Add("@AssignedBy", Session("userid1"))
                                'To be changed
                            End With
                            connection1.Open()
                            cmdsave.ExecuteNonQuery()
                            connection1.Close()
                            cmdsave.Dispose()
                            'Clear()
                            '************************************Delete Schema of Table

                            Dim Cmddel As New SqlCommand("delete warstableschema where CreatedBy='" & Session("userid1") & "'", connection1)
                            'Response.Write("create table " & Me.txtTablename.Text & "(" & str & ")")
                            'Response.End()
                            connection1.Open()
                            Cmddel.ExecuteNonQuery()
                            connection1.Close()
                            Cmddel.Dispose()
                        End If

                    Catch ex As Exception
                        Dim dg As String = ex.ToString
                        Dim r As String = dg.Replace("(", "")
                        r = r.Replace(vbNewLine, "")
                        r = r.Replace("'", "")
                        Dim kol As Integer = r.Length
                        If kol > 10 Then
                            'r = r.Substring(0, 200)
                        End If
                        ShowConfirm(r)
                        'ShowConfirm("Incorrect Syntax")
                    End Try
                End If
                connection3.Close()
                dr1.Close()
                objCmd.Dispose()
            Else
                ShowConfirm("Please enter table name")
                SetFocus(txtTablename)
            End If
        Else
            ShowConfirm("Please Select DepartmentName")
            SetFocus(DepartmentName)
        End If
    End Sub
End Class
