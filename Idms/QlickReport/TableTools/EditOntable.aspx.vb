Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Partial Class TableTools_EditOntable
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionstring") + "; async=true"
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim objds As New DataSet
    Dim dr As SqlDataReader
    'Dim primcol As String
    Dim previousprimarycolumn As String = ""
    Dim previousconstraintname As String = ""
    Dim tablevalue
    Dim columcollection As Array
    Dim con As New SqlConnection(constr)
    Dim strColname As String
    Dim strdataType As String
    Dim strsize As String
    Dim constraintname As String
    Dim ChangeStruct As String = ""
    Dim ChangeStruct1 As String = ""
    Dim QueryDrop As String = ""
    Dim QuerAdd As String = ""
    Dim QueryUpadteTableMaster As String = ""
    Dim constriantnamefinal As String = ""
    Dim primcol As String = ""
    Dim primcol1 As String = ""
    Dim i As Integer
    Dim loggedId As String
    Dim cmd As SqlCommand


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        connection.Close()

        cmd = New SqlCommand("select ProductType from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        connection.Open()
        Dim rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("ProductType")
            If (producttype = "Multiple User") Then
                Me.spandisplay.Visible = True
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
                    lblDept.Text = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
                    lblClient.Text = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
                    lblLOB.Text = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
                End If
            Else
                Me.gettable.Visible = True
            End If
            connection.Close()
        End If


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
                con.Close()
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


        'Put user code to initialize the page here

        If (Session("userid") = "") Then
            Response.Redirect("~/SessionExpired.aspx")
        End If
        Ajax.Utility.RegisterTypeForAjax(GetType(Functions))
        Dim classobj As New Functions


        If Me.IsPostBack = False Then
            dvAddColumn.Visible = False
            divmsgboxdelrecord.Visible = False
            divmsgboxdeltable.Visible = False
            Me.divDatalist.Visible = False

        End If

    End Sub

    Public Sub SetsFocus(ByVal FocusControl As Control)
        ''''''''''''''this function is for setting the focus''''''''''''''''''''
        Dim Script As New System.Text.StringBuilder
        Dim ClientID As String = FocusControl.ClientID
        With Script
            .Append("<script language='javascript'>")
            .Append("document.getElementById('")
            .Append(ClientID)
            .Append("').focus();")
            .Append("</script>")
        End With
        ClientScript.RegisterStartupScript(Me.GetType, "SetsFocus", Script.ToString())
    End Sub
    Private isEditMode As Boolean = False
    Private isEditMode1 As Boolean = True
    Protected Property IsNotEditable() As Boolean

        Get
            Return Me.isEditMode1
        End Get

        Set(ByVal value As Boolean)
            Me.isEditMode1 = value
        End Set
    End Property


    Protected Property IsEditable() As Boolean
        Get

            Return Me.isEditMode
        End Get

        Set(ByVal value As Boolean)
            Me.isEditMode = value
        End Set
    End Property

    Public Sub ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Me.GetType, "ShowConfirm", Script)
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
        Me.ddlTablename.Items.Clear()
    End Sub
    Public Sub findprimcol()
        'Code For Checking the status of column as a primary or not
        Dim arrprimcol As Array
        Try
            Dim chkprimcol As New SqlCommand
            chkprimcol = New SqlCommand("Select PrimarCol From WARSLOBTableMaster where TableId='" & ddlTablename.SelectedValue & "'", connection)
            Dim dsgetprimcol As New DataSet
            Dim adpgetprimcol As New SqlDataAdapter
            adpgetprimcol.SelectCommand = chkprimcol
            connection.Open()
            adpgetprimcol.Fill(dsgetprimcol)
            connection.Close()
            chkprimcol.Dispose()
            Dim str As String
            Dim strname As String
            Dim strvalue As String
            Dim i As Integer
            Dim j As Integer
            For i = 0 To dsgetprimcol.Tables(0).Rows.Count - 1
                If str = "" Then
                    str = dsgetprimcol.Tables(0).Rows(i).Item("PrimarCol")
                Else
                    str = str & "$" & dsgetprimcol.Tables(0).Rows(i).Item("PrimarCol")
                End If
            Next
            If str = "" Then
                'str = "N"
            Else
                arrprimcol = str.Split(",")
            End If
            'Checking The Datalist and making primary column as true or false

            If arrprimcol.Length <> 0 Then
                For i = 0 To arrprimcol.Length - 1
                    For j = 0 To dlreg.Items.Count - 1
                        If arrprimcol(i) = CType(dlreg.Items(j).FindControl("Label1"), Label).Text Then
                            CType(dlreg.Items(j).FindControl("chkprimary"), CheckBox).Checked = True
                            'Else
                            '    CType(dlreg.Items(j).FindControl("chkprimary"), CheckBox).Checked = False
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            ShowConfirm(ex.ToString)
        End Try

    End Sub


    Public Sub ddlTablename_bind()
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''

        Dim query As String = "select tablename from warslobtablemaster where createdby='" + Session("userid") + "' and Editable='Yes' and Importable='Yes'"
        Dim objCbo1Cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        Dim ds As New DataSet
        objCbo1Cmd.Connection = connection
        objCbo1Cmd.CommandText = query
        adp.SelectCommand = objCbo1Cmd
        connection.Open()
        adp.Fill(ds, "warslobtablemaster")
        connection.Close()
        ddlTablename.Items.Clear()
        ddlTablename.DataSource = ds
        ddlTablename.DataTextField = "tablename"
        ddlTablename.DataBind()
        ddlTablename.Items.Insert(0, "--Select--")
        SetsFocus(ddlTablename)
        objCbo1Cmd.Dispose()
    End Sub
    Public Sub ddlTablename_bind1(ByVal deptid As String, ByVal clientid As String, ByVal lobid As String, ByVal userid As String)
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''

        Dim query As String = "select Tableid, LTrim(RTrim(TableName)) as TableName,Visiblecolumn from warslobtablemaster where  createdBy ='" & userid & "' and LOBid='" & lobid & "' and DepartmentId='" & deptid & "' and ClientId='" & clientid & "' order by TableName"
        Dim objCbo1Cmd As New SqlCommand
        Dim adp As New SqlDataAdapter
        Dim ds As New DataSet
        objCbo1Cmd.Connection = connection
        objCbo1Cmd.CommandText = query
        adp.SelectCommand = objCbo1Cmd
        connection.Open()
        adp.Fill(ds, "warslobtablemaster")
        connection.Close()
        ddlTablename.Items.Clear()
        ddlTablename.DataSource = ds
        ddlTablename.DataTextField = "tablename"
        ddlTablename.DataBind()
        ddlTablename.Items.Insert(0, "--Select--")
        SetsFocus(ddlTablename)
        objCbo1Cmd.Dispose()
    End Sub
    ''' <summary>
    ''' Function For Updating Table Date 9/10/2008
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub updatetable()
        'Final Execution

        'QueryDrop = "alter table " & txttablename.Text & " " & "Drop" & " " & previousconstraintname & " " & "Primary Key" & "(" & previousprimarycolumn & ")"
        QueryDrop = "alter table " & txttablename.Text & " " & "Drop" & " " & previousconstraintname

        columcollection = ChangeStruct.Split(",")
        ' QuerAdd = "alter table " & txttablename.Text & " alter column " & ChangeStruct
        QueryUpadteTableMaster = "Update warslobtablemaster Set PrimarCol='" & primcol & "',constraintname='" & constriantnamefinal & "' where Tableid='" & ddlTablename.SelectedValue & "'"
        Dim CmdQueryDrop As New SqlCommand
        Dim CmdQueryAdd As New SqlCommand
        Dim CmdQueryUpadteTableMaster As New SqlCommand
        Dim trans As SqlTransaction
        connection.Open()

        trans = connection.BeginTransaction()
        CmdQueryDrop.Transaction = trans
        CmdQueryAdd.Transaction = trans
        CmdQueryUpadteTableMaster.Transaction = trans

        Try
            '*****************ranjit commented ******************
            If previousconstraintname <> "Null" Then
                If previousconstraintname <> "" Then

                    If previousconstraintname <> "NULL" Then


                        CmdQueryDrop.Connection = connection
                        CmdQueryDrop.CommandText = QueryDrop
                        CmdQueryDrop.ExecuteNonQuery()
                        CmdQueryAdd.Connection = connection
                    End If
                End If
            End If

            '*****************ranjit commented ******************
            For i = 0 To columcollection.Length - 1

                CmdQueryAdd.Connection = connection
                QuerAdd = "alter table " & txttablename.Text & " alter column " & columcollection(i)
                CmdQueryAdd.CommandText = QuerAdd
                CmdQueryAdd.ExecuteNonQuery()

            Next
            If primcol1 <> "" Then


                QuerAdd = "alter table " & txttablename.Text & " " & "Add" & " " & primcol1
                CmdQueryAdd.CommandText = QuerAdd
                CmdQueryAdd.ExecuteNonQuery()

            End If
            CmdQueryUpadteTableMaster.Connection = connection
            CmdQueryUpadteTableMaster.CommandText = QueryUpadteTableMaster
            CmdQueryUpadteTableMaster.ExecuteNonQuery()

            Dim cmm As New SqlCommand
            cmm.Transaction = trans
            cmm.Connection = connection
            cmm.CommandText = "sp_LogTableTool_Track"
            cmm.CommandType = CommandType.StoredProcedure
            With cmm.Parameters
                .AddWithValue("@TableName", Me.txttablename.Text)
                .AddWithValue("@Action", "Update")
                .AddWithValue("@CreatedBy", Session("Userid"))
                .AddWithValue("@CreatedOn", System.DateTime.Today)
                .AddWithValue("@type", Session("usertype"))
            End With
            cmm.ExecuteNonQuery()

            trans.Commit()
            connection.Close()
            dbregInfo(dlreg, "syscolumns")
            findprimcol()
            ShowConfirm("Table Alterd Sucessfully!")

        Catch ex As Exception
            trans.Rollback()
            connection.Close()
            Dim dg As String = ex.ToString
            Dim r As String = dg.Replace("(", "")
            'r = r.Replace(")", "")
            r = r.Replace(",", "")
            r = r.Replace("'", "")
            'r = r.Replace("{", "")
            'r = r.Replace("}", "")
            'r = r.Replace(".", "")
            'r = r.Replace("/", "")
            'r = r.Replace("\", "")
            'r = r.Replace(";", "")
            'r = r.Replace(":", "")
            'r = r.Replace(" ", "")
            r = r.Replace(vbNewLine, "")
            Dim kol As Integer = r.Length
            If kol > 100 Then
                r = r.Substring(0, 111)
            End If
            'Response.Write(r)
            'ShowConfirm("Table could not be altered due to primary key rules voilation.")
            ShowConfirm(r)
            'ShowConfirm(dg)
            dbregInfo(dlreg, "syscolumns")
            findprimcol()

        Finally
            connection.Close()
            CmdQueryDrop.Dispose()
            CmdQueryAdd.Dispose()
            CmdQueryUpadteTableMaster.Dispose()
        End Try
    End Sub

    ''''''''''Private Sub ddlLobName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobName.SelectedIndexChanged
    ''''''''''    '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    ''''''''''    'Dim query As String = "select tablename from warslobtablemaster where lobid='" & ddlLobName.SelectedValue & "'"
    ''''''''''    'Dim objCbo1Cmd As New SqlCommand
    ''''''''''    'Dim adp As New SqlDataAdapter
    ''''''''''    'Dim ds As New DataSet
    ''''''''''    'objCbo1Cmd.Connection = connection
    ''''''''''    'objCbo1Cmd.CommandText = query
    ''''''''''    'adp.SelectCommand = objCbo1Cmd
    ''''''''''    'connection.Open()
    ''''''''''    'adp.Fill(ds, "warslobtablemaster")
    ''''''''''    'connection.Close()
    ''''''''''    'ddlTablename.DataSource = ds
    ''''''''''    'ddlTablename.DataTextField = "tablename"
    ''''''''''    'ddlTablename.DataBind()
    ''''''''''    'ddlTablename.Items.Insert(0, "---select---")
    ''''''''''    'SetsFocus(ddlTablename)
    ''''''''''    'objCbo1Cmd.Dispose()
    ''''''''''    If Not ddlLobName.Value = "---select---" Then
    ''''''''''        ddlTablename_bind()
    ''''''''''    End If
    ''''''''''    If ddlLobName.Value = "---select---" Or ddlLobName.Value = "" Then
    ''''''''''        Me.ddlTablename.Items.Clear()
    ''''''''''        'ddlLobName.Items.Clear()
    ''''''''''        'ddlLobName.ClearSelection()
    ''''''''''        'ddlTablename_bind()
    ''''''''''    End If
    ''''''''''End Sub

    Private Sub cmdyesr_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyesr.Click
        divmsgboxdelrecord.Visible = False
        ''''''''''''''''''''Delete the record from tables''''''''''''''''''
        If Me.txttablename.Text = "" Or Me.txttablename.Text = "--Select--" Then
            ShowConfirm("First Select Table.")
            SetsFocus(ddlTablename)
        Else
            Try


                Dim query As String = "delete " & Me.txttablename.Text & " "
                Dim cmd As New SqlCommand(query, connection)
                connection.Open()
                cmd.ExecuteNonQuery()
                connection.Close()
                cmd.Dispose()

                Dim cmdtrack As New SqlCommand("sp_LogTableToolForDeleteRecord", connection)
                cmdtrack.CommandType = CommandType.StoredProcedure
                With cmdtrack.Parameters
                    .Add("@TableName", Me.txttablename.Text)
                    .Add("@CreatedBy", Session("userid"))
                End With
                connection.Open()
                cmdtrack.ExecuteNonQuery()
                connection.Close()
                cmdtrack.Dispose()

                connection.Open()
                Dim cmm As New SqlCommand("insert into tooltable_utype select MAX(Autoid)," + Session("usertype") + " from LogTableTool_Track where TableName='" + Me.txttablename.Text + "' and Action='Delete Record'", connection)
                cmm.ExecuteNonQuery()
                connection.Close()

                ShowConfirm("Records Deleted Successfully.")
                Clear()
                Me.ddlTablename.Items.Remove(Me.txttablename.Text)
            Catch ex As Exception
                Dim str1 = Replace(ex.Message, "'", " ")
                ShowConfirm(str1)
            End Try
        End If
        dvAddColumn.Visible = False
        'cmdDeleterecord.Visible = False
        'cmdDeleteTable.Visible = False
        'cmdchastrutable.Visible = False
        'cmdAddColumn.Visible = False


    End Sub

    Private Sub cmdnor_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdnor.Click
        divmsgboxdelrecord.Visible = False
        ddlTablename.SelectedIndex = 0
        'Clear()
        'Me.ddlTablename.Items.Remove(Me.txttablename.Text)

    End Sub



    'Get Exchange for Combo in DataGrid
    Function GetDataType() As DataSet
        Dim cboDataSetEx As New DataSet
        Const strSQLDDL = "select type from datatypes"
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
        'Dim objcmd As New SqlCommand("select col.name as ColName,typ.name as Type ,col.length as Size from syscolumns col,systypes typ,sysobjects obj where col.id=obj.id and obj.xtype='U' and obj.name='" & txttablename.Text & "' and typ.xtype=col.xtype", connection)
        Dim objcmd As New SqlCommand("select b.column_name as ColName,b.data_type as Type,a.length as Size from syscolumns a ,INFORMATION_SCHEMA.COLUMNS b where a.id=(Select id from SYSOBJECTS where name = '" & txttablename.Text & "') and b.table_name='" & txttablename.Text & "' and a.name=b.column_name", connection)
        Dim objadp As New SqlDataAdapter
        objadp.SelectCommand = objcmd
        connection.Open()
        objadp.Fill(objds, "table")
        connection.Close()
        control.DataSource = objds
        control.DataBind()
        Try


            If control.ID = "dlreg" Then
                If isEditMode = True Then


                    Dim coun As Integer = CType(dlreg.Items(i).FindControl("cboEx"), DropDownList).Items.Count
                    Dim im As Integer = 0
                    For i = 0 To dlreg.Items.Count - 1
                        Dim a As Integer = 0

                        For im = 0 To coun - 1
                            Dim d As DropDownList = CType(dlreg.Items(i).FindControl("cboEx"), DropDownList)
                            Dim t As String = vbNewLine

                            Dim p As String = LCase(d.Items(im).Text)
                            p = p.Replace(t, "")
                            Dim l = LCase(CType(dlreg.Items(i).FindControl("label2"), Label).Text)
                            If p = l Then
                                a = im
                                Exit For
                            End If
                        Next
                        CType(dlreg.Items(i).FindControl("cboEx"), DropDownList).SelectedIndex = a
                    Next
                End If
            End If
        Catch ex As Exception

        End Try

        objcmd.Dispose()
    End Sub

    Private Sub dlreg_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.CancelCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        dlreg.EditItemIndex = -1
        dbregInfo(dlreg, "syscolumns")
        findprimcol()
    End Sub

    Private Sub dlreg_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.EditCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        isEditMode = True
        isEditMode1 = False
        dbregInfo(dlreg, "syscolumns")
        findprimcol()

        ' CType(dlreg.FooterTemplate.FindControl("cmdUpdate"), Button).Visible = False
        'CType(dlreg.FindControl("cmdCancel"), Button).Visible = False
    End Sub

    ''' <summary>
    ''' Code For Updating The Structure Of Table
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dlreg_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.UpdateCommand
        Dim i As Integer
        Dim maxvalue As Integer
        For i = 0 To dlreg.Items.Count - 1
            strColname = CType(dlreg.Items(i).FindControl("txtCol"), Label).Text
            strdataType = CType(dlreg.Items(i).FindControl("cboEx"), DropDownList).SelectedItem.Value
            strsize = CType(dlreg.Items(i).FindControl("txtsize"), TextBox).Text
            ''''''''''''''''''Update the structure of table''''''''''''''''''''''''
            If UCase(strdataType) = "VARCHAR" And Not IsNumeric(UCase(strsize)) Then
                CType(dlreg.Items(i).FindControl("txtsize"), TextBox).Text = ""
                ShowConfirm("Please Enter valid Size")
                Exit Sub
            End If

            If LCase(strdataType) = "datetime" Then
                If CType(dlreg.Items(i).FindControl("chkprimary"), CheckBox).Checked = True Then
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType & " " & "Not Null"
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType & " " & "Not Null"

                    End If
                Else
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType

                    End If


                End If

            ElseIf LCase(strdataType) = "numeric" Then
                If CType(dlreg.Items(i).FindControl("chkprimary"), CheckBox).Checked = True Then
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType & " " & "Not Null"
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType & " " & "Not Null"
                    End If
                Else
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType
                    End If

                End If

            ElseIf LCase(strdataType) = "float" Then
                If CType(dlreg.Items(i).FindControl("chkprimary"), CheckBox).Checked = True Then
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType & " " & "Not Null"
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType & " " & "Not Null"
                    End If
                Else
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType
                    End If
                End If

            Else

                'For Varchar
                If CType(dlreg.Items(i).FindControl("chkprimary"), CheckBox).Checked = True Then

                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType & "(" & strsize & ") " & " " & "Not Null"
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType & "(" & strsize & ") " & " " & "Not Null"
                    End If
                Else
                    If ChangeStruct = "" Then
                        ChangeStruct = strColname & " " & strdataType & "(" & strsize & ") "
                    Else
                        ChangeStruct = ChangeStruct + "," + strColname & " " & strdataType & "(" & strsize & ") "
                    End If
                End If
            End If


            'Change As Per Requirment On Date 06/11/2008 By Rohit
            'If strdataType = "DATETIME" Or strdataType = "FLOAT" Then
            '    Try
            '        Dim query As String = "alter table " & txttablename.Text & " alter column " & strColname & " " & strdataType & " "
            '        Dim objcmd1 As New SqlCommand(query, connection)
            '        connection.Open()
            '        objcmd1.ExecuteNonQuery()
            '        connection.Close()
            '        ShowConfirm("Table altered successfully|||||")
            '        dlreg.EditItemIndex = -1
            '        dbregInfo(dlreg, "syscolumns")
            '    Catch ex As Exception
            '        'Dim strmsg As String
            '        'strmsg = Replace(ex.Message.ToString, "'", "")
            '        'strmsg = Replace(strmsg, vbCrLf, " ")
            '        'ShowConfirm(strmsg)
            '        ShowConfirm("Incorrect Syntax")
            '    End Try
            'Else
            '    Try
            '        Dim query As String = "alter table " & txttablename.Text & " alter column " & strColname & " " & strdataType & "(" & strsize & ") "
            '        Dim objcmd2 As New SqlCommand(query, connection)
            '        connection.Open()
            '        objcmd2.ExecuteNonQuery()
            '        connection.Close()
            '        ShowConfirm("Table altered successfully|||||")
            '        dlreg.EditItemIndex = -1
            '        dbregInfo(dlreg, "syscolumns")
            '    Catch ex As Exception
            '        ' Dim strmsg As String = "Incorrect Syntax"
            '        'strmsg = Replace(ex.Message.ToString, "'", "")
            '        'strmsg = Replace(strmsg, vbCrLf, " ")
            '        ShowConfirm("Incorrect Syntax")
            '    End Try
            'End If
        Next
        'Old Constriant Name From Warslobtable Master
        Try

            Dim cmdprim As New SqlCommand("Select PrimarCol,constraintname From WarsLobTablemaster where Tableid='" & ddlTablename.SelectedValue & "'", connection)
            Dim reader As SqlDataReader
            connection.Open()
            reader = cmdprim.ExecuteReader()
            While reader.Read
                If IsDBNull(reader("PrimarCol")) Then
                    previousprimarycolumn = ""
                Else
                    previousprimarycolumn = reader("PrimarCol")
                End If
                If IsDBNull(reader("constraintname")) Then
                    previousconstraintname = ""
                Else
                    previousconstraintname = reader("constraintname")
                End If


            End While
            connection.Close()
        Catch ex As Exception
            ShowConfirm(ex.ToString)
        End Try
        '*******************************End

        Try
            '*******************For Selecting  column As Primary
            ' Finding The Highest Value Of Table
            If ConnectionState.Open Then
                connection.Close()
            End If
            Dim maxcount As New SqlCommand("select max (tableid+1) from WARSLOBTableMaster", connection)
            connection.Open()
            maxvalue = maxcount.ExecuteScalar
            connection.Close()
            constraintname = "Constraint pk_prim" & maxvalue & DateTime.Now.Millisecond
            Dim j1 As Integer
            Dim k1 As Integer = 0
            Dim counter As Integer = 0



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
            If primcol <> "" Then


                If k1 = 1 Then
                    primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"
                Else
                    primcol1 = constraintname & " " & "PRIMARY KEY" & " " & "(" & " " & primcol & " " & ")"

                End If
            End If
            '***************For Ading Column As Primary
            If k1 <> 0 Then
                If k1 = 1 Then
                    ChangeStruct1 = ChangeStruct & " " & primcol1
                    constriantnamefinal = constraintname
                Else
                    ChangeStruct1 = ChangeStruct & " " & primcol1
                    constriantnamefinal = constraintname
                End If
            Else
                ChangeStruct1 = ChangeStruct
                constriantnamefinal = "NULL"
            End If

        Catch ex As Exception
            ShowConfirm(ex.ToString)
        End Try
        '************************End
        'Function For Updating Table Finally
        updatetable()
        '*****************End

    End Sub



    Private Sub btnclosedatalist_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclosedatalist.Click
        Me.divDatalist.Visible = False
        ddlTablename.SelectedIndex = 0
        'Clear()
        'Me.ddlTablename.Items.Remove(Me.ddlTablename.Text)
    End Sub

    ' It is Comment By Rohit on date 2Aug08

    'Private Sub btnclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnclose.Click
    '    divmsgboxdelrecord.Visible = False
    '    Me.DepartmentName.SelectedIndex = 0
    '    Me.ddlTablename.Items.Remove(Me.txttablename.Text)
    '    Me.Clientname.Items.Remove(Me.txtclient.Text)
    '    Me.ddlLobName.Items.Remove(Me.txtlob.Text)
    'End Sub

    'Private Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    divmsgboxdeltable.Visible = False

    '    'Me.DepartmentName.SelectedIndex = 0
    '    'Me.ddlTablename.Items.Remove(Me.txttablename.Text)
    '    'Me.Clientname.Items.Remove(Me.txtclient.Text)
    '    'Me.ddlLobName.Items.Remove(Me.txtlob.Text)
    'End Sub

    Private Sub cmdyesdt_Command(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.CommandEventArgs) Handles cmdyesdt.Command
        Dim str As String = ""
        'str = Me.ddlTablename.Value
        divmsgboxdeltable.Visible = False
        '''''''''''''''Delete table'''''''''''''''''''''''''''''''''
        'divmsgboxdeltable.Visible = False
        Try
            If Me.txttablename.Text = "" Or Me.txttablename.Text = "--Select--" Then
                ShowConfirm("First You Select Table!!!!!!")
                SetsFocus(ddlTablename)
            Else
                divmsgboxdeltable.Visible = False
                Dim query As String = "drop table " & Me.txttablename.Text & " "
                Dim cmd As New SqlCommand(query, connection)
                connection.Open()
                cmd.ExecuteNonQuery()
                connection.Close()
                Me.ddlTablename.SelectedIndex = 0
                cmd.Dispose()
                ''''''''''''''''''''''''''''''drop views''''''''''''''''''''''''''''''''''''''
                Dim cmdchkview As New SqlCommand("select viewname from idmsviewmaster where '" & txttablename.Text & "' in (tablename)", connection1)
                Dim drchkview As SqlDataReader
                connection1.Open()
                drchkview = cmdchkview.ExecuteReader
                While drchkview.Read
                    Dim cmddrpview As New SqlCommand(" delete warslobtablemaster where tablename='" & drchkview("ViewName").ToString & "' delete idmsviewmaster where viewname='" & drchkview("ViewName").ToString & "'", connection)
                    connection.Open()
                    cmddrpview.ExecuteNonQuery()
                    connection.Close()
                    cmddrpview.Dispose()
                End While
                drchkview.Close()
                connection1.Close()
                cmdchkview.Dispose()
                '''''''''''''''delete the record from WARSLOBTableMaster''''''''''''''''''''''
                Dim deltab As String = "delete WARSLOBTableMaster where tablename='" & Me.txttablename.Text & "' delete warsquerymaster where tablename='" & Me.txttablename.Text & "' delete idmsquerymaster where '" & txttablename.Text & "' in (tablename)"

                Dim cmddeltab As New SqlCommand(deltab, connection)
                connection.Open()
                cmddeltab.ExecuteNonQuery()
                connection.Close()
                cmddeltab.Dispose()

                '''''''''''''''''''''''''''Track........................

                Dim cmdtrack As New SqlCommand
                cmdtrack.CommandType = CommandType.StoredProcedure
                cmdtrack.CommandText = "sp_LogTableTool_Track"
                cmdtrack.Connection = connection2
                With cmdtrack.Parameters
                    .AddWithValue("@TableName", Me.txttablename.Text)
                    .AddWithValue("@Action", "Delete")
                    .AddWithValue("@CreatedBy", Session("Userid"))
                    .AddWithValue("@CreatedOn", System.DateTime.Today)
                    .AddWithValue("@type", Session("usertype"))
                End With
                connection2.Open()
                cmdtrack.ExecuteNonQuery()
                connection2.Close()
                cmdtrack.Dispose()



                ShowConfirm("Table Deleted Successfully!!!!!!")
                Clear()
                Me.ddlTablename.Items.Remove(Me.txttablename.Text)
                ddlTablename_bind()
            End If
        Catch ex As Exception
            'Response.Write("Caught Function")
            'Response.Write(ex)
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
            'ShowConfirm("Incorrect Syntax")
        End Try
        dvAddColumn.Visible = False
        'cmdDeleterecord.Visible = False
        'cmdDeleteTable.Visible = False
        'cmdchastrutable.Visible = False
        'cmdAddColumn.Visible = False

    End Sub

    Private Sub cmdnodt_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdnodt.Click
        divmsgboxdeltable.Visible = False
        ddlTablename.SelectedIndex = 0
        'Clear()
        '*********************Check******************************
        'Me.ddlTablename.Items.Remove(Me.txttablename.Text)
    End Sub

    Private Sub dlreg_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataListItemEventArgs) Handles dlreg.ItemDataBound
        If e.Item.ItemType = ListItemType.EditItem Then
            Dim ddl As DropDownList
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim current As String = CType(drv("type"), String)
            ddl = CType(e.Item.FindControl("cboEx"), DropDownList)
            SetsFocus(ddl)
        End If
    End Sub
    Private Sub cmdchastrutable_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdchastrutable.ServerClick
        '************************validation*******************************

        loggedId = Session("userid").ToString()
        tablevalue = ddlTablename.SelectedValue

        ' Dim tablevalue = (Request("ddltablename"))
        If tablevalue = "" Or tablevalue = "--Select--" Then
            ShowConfirm("Please Select The Table!!!!!!")
            'Me.DepartmentName.SelectedIndex = 0
            Exit Sub

        End If
        If (Me.dvAddColumn.Visible = True) Then
            ShowConfirm("Please First Close The Add Column Pannel!!!!!!")
            Exit Sub
        End If

        If (Me.divDatalist.Visible = True) Then
            ShowConfirm("Click the close button first to close the alter pane.")
            Exit Sub
        End If
        Dim columnnamearr = Split(Trim((ddlTablename.SelectedItem.Text)), "$")
        Dim combovalue = Trim(columnnamearr(0))
        'Dim combovalue
        txttablename.Text = ""
        txttablename.Text = combovalue
        dbregInfo(dlreg, "syscolumns")
        Me.divDatalist.Visible = True
        'Me.ddlTablename.Items.Insert(0, combovalue) As it Fix the name of selected Table
        Dim clientv As String
        Dim lobv As String

        Dim clnt = Request("Clientname")
        Dim lobn = Request("ddllobname")

        'Fuction For Finding Primary Column
        findprimcol()



        '**********************Function Change 
        'Dim chkprimcol As New SqlCommand("Select PrimarCol From WARSLOBTableMaster where TableId='" & tablevalue & "'", connection)
        'Dim chkprimreader As SqlDataReader
        'connection.Open()
        'chkprimreader = chkprimcol.ExecuteReader()
        'If chkprimreader.Read Then
        '    primcol = chkprimreader("PrimarCol")
        'End If
        'chkprimcol.Dispose()
        'chkprimreader.Close()
        'connection.Close()



        'arrprimcol = primcol.Split(",")



        'Dim tablestatus As New SqlCommand("select isnull(localTable,'') as localTable from warslobtablemaster where TableName='" & txttablename.Text & "'", connection)
        ''Dim tabreader As SqlDataReader
        'Dim tabreader As String
        'connection.Open()
        'tabreader = tablestatus.ExecuteScalar
        'If tabreader = "Local" Then
        '    chkscope.Checked = True
        '    chkscope.ToolTip = "Click to make NonLocal"
        'Else
        '    chkscope.Checked = False
        '    chkscope.ToolTip = "Click to make Local"
        'End If

        'connection.Close()
        ' dlreg.FindControl("cmdUpdate").Visible = False
        'CType(dlreg.FindControl("cmdUpdate"), Button).Visible = False
        ' CType(dlreg.FindControl("cmdCancel"), Button).Visible = False

    End Sub

    Protected Sub cmdDeleterecord_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleterecord.ServerClick
        '********************check on delete record****************

        tablevalue = ddlTablename.SelectedValue
        If tablevalue = "" Or tablevalue = "--Select--" Then
            ShowConfirm("Please Select The Table!!!!!!")
            'Me.DepartmentName.SelectedIndex = 0
            Exit Sub

        End If
        If (Me.divDatalist.Visible = True) Then
            ShowConfirm("Click the close button first to close the alter pane.")
            Exit Sub
        End If

        If (Me.dvAddColumn.Visible = True) Then
            ShowConfirm("Please First Close The Add Column Pannel!!!!!!")
            Exit Sub
        End If
        Dim columnnamearr = Split(Trim((ddlTablename.SelectedItem.Text)), "$")
        ' Dim columnnamearr = Split(Trim(Request("ddltablename")), "$")
        Dim combovalue = Trim(columnnamearr(0))

        txttablename.Text = ""
        txttablename.Text = combovalue
        divmsgboxdelrecord.Visible = True
        'Me.ddlTablename.Items.Insert(0, combovalue)As it Fix Table Name in Table comboBox
        Dim clientv As String
        Dim lobv As String
        Dim clnt = Request("Clientname")
        Dim lobn = Request("ddllobname")

    End Sub

    Private Sub cmdDeleteTable_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdDeleteTable.ServerClick

        '********************valodation****************************************************************
        ' Dim tablevalue = (Request("ddltablename"))
        tablevalue = ddlTablename.SelectedItem.Text
        If tablevalue = "" Or tablevalue = "--Select--" Then
            ShowConfirm("Please Select The  Table!!!!!!")
            'Me.DepartmentName.SelectedIndex = 0
            Exit Sub

        End If


        If (Me.divDatalist.Visible = True) Then
            ShowConfirm("Click the close button first to close the alter pane.")
            Exit Sub
        End If
        If (Me.dvAddColumn.Visible = True) Then
            ShowConfirm("Please First Close The Add Column Pannel!!!!!!")
            Exit Sub
        End If

        ' Dim columnnamearr = Split(Trim(Request("ddltablename")), "$")
        Dim columnnamearr = Split(Trim(ddlTablename.SelectedItem.Text))

        ' Dim combovalue = Trim(columnnamearr(1))
        Dim combovalue = Trim(columnnamearr(0))
        txttablename.Text = ""
        txttablename.Text = combovalue
        If txttablename.Text = "" Or txttablename.Text = "--Select--" Then
            ShowConfirm("Please Select Table!!!!!!")
            SetsFocus(ddlTablename)
            'Me.ddlLobName.SelectedIndex = 0
        Else
            Try

                Dim strreports As String = ""
                Dim cmdchk As New SqlCommand("select * from idmsquerymaster where '" & txttablename.Text & "' in (tablename)", connection)
                Dim drchk As SqlDataReader
                connection.Open()
                drchk = cmdchk.ExecuteReader
                While drchk.Read
                    If strreports = "" Then
                        strreports = drchk("queryname").ToString
                    Else
                        strreports = strreports & "," & drchk("queryname").ToString
                    End If
                End While
                drchk.Close()
                connection.Close()
                cmdchk.Dispose()
                Dim strquery As String = ""
                Dim cmdchk1 As New SqlCommand("select * from warsquerymaster where '" & txttablename.Text & "' in (tablename)", connection)
                Dim drchk1 As SqlDataReader
                connection.Open()
                drchk1 = cmdchk1.ExecuteReader
                While drchk1.Read
                    If strquery = "" Then
                        strquery = drchk1("queryname").ToString
                    Else
                        strquery = strquery & "," & drchk1("queryname").ToString
                    End If
                End While
                drchk1.Close()
                connection.Close()
                cmdchk1.Dispose()
                Dim strview As String = ""
                Dim cmdchk2 As New SqlCommand("select * from idmsviewmaster where '" & txttablename.Text & "' in (tablename)", connection)
                Dim drchk2 As SqlDataReader
                connection.Open()
                drchk2 = cmdchk2.ExecuteReader
                While drchk2.Read
                    If strview = "" Then
                        strview = drchk2("ViewName").ToString
                    Else
                        strview = strview & "," & drchk2("ViewName").ToString
                    End If
                End While
                drchk2.Close()
                connection.Close()
                cmdchk2.Dispose()
                Dim strmsg As String = ""
                If strreports <> "" Then
                    strmsg = strmsg & strreports & " reports,"
                End If
                If strquery <> "" Then
                    strmsg = strmsg & strquery & " queries,"
                End If
                If strview <> "" Then
                    strmsg = strmsg & strview & " views"
                End If
                If strmsg <> "" Then
                    strmsg = strmsg & " are dependent on this table.\nIf you delete this table all these items will also be deleted."
                    ShowConfirm(strmsg)
                End If
                divmsgboxdeltable.Visible = True

                'Me.ddlTablename.Items.Insert(0, combovalue)As it Fix Table Name in Table comboBox 
                Dim clientv As String
                Dim lobv As String

                Dim clnt = Request("Clientname")
                Dim lobn = Request("ddllobname")
            Catch ex As Exception
                Dim str1 = Replace(ex.Message, "'", " ")
                ShowConfirm(str1)
            End Try

        End If '*********************************************end*******************************
        'Me.DepartmentName.SelectedIndex = 0
    End Sub


    Private Sub Add_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Add.ServerClick

        Dim VisCol As String
        Try
            Dim str As String = ""
            Dim str1 As String = ""

            If NewColName.Value = "" Then
                ShowConfirm("Please Enter Column Name")
                Exit Sub
            End If
            '************* Check Datatype***************
            If (LCase(cbodatatype.Value) = "selected") Then
                ShowConfirm("Please Select Data Type")
                Exit Sub
            End If


            If LCase(cbodatatype.Value) = "varchar" And Not IsNumeric(txtsize.Value) Then
                ShowConfirm("Please Enter valid Size")
                Exit Sub
            End If
            'If Not IsNumeric(txtsize.Value) Then
            '    ShowConfirm("Please Enter valid Size")
            '    Exit Sub
            'End If
            'DATETIME
            'NUMERIC
            'FLOAT
            If LCase(cbodatatype.Value) = "varchar" And txtsize.Value = "" Then
                ShowConfirm("Please Enter Size")
                Exit Sub
            End If
            If LCase(cbodatatype.Value) = "varchar" And txtsize.Value <> "" Then
                Try
                    Dim intchk As Integer = CType(txtsize.Value, Integer)
                    If intchk > 8000 Then
                        ShowConfirm("Size must less than 8001")
                        Exit Sub
                    End If
                Catch ex As Exception
                    ShowConfirm("Size should be numeric")
                    Exit Sub
                End Try
            End If
            If LCase(cbodatatype.Value) = "varchar" Then
                str = NewColName.Value & " " & cbodatatype.Value & "(" & txtsize.Value & ")"
            Else
                str = NewColName.Value & " " & cbodatatype.Value
            End If
            If txtdefault.Value <> "" Then
                str = str & " not null default " & "'" & txtdefault.Value & "'"
            End If
            str1 = "alter table" & " " & txtTbalename.Value & " add"
            Dim FinalString As String = ""
            FinalString = str1 & " " & str
            '''''Dim deltab As String = "delete WARSLOBTableMaster where tablename='" & Me.txttablename.Text & "' delete from warsquerymaster where tablename='" & Me.txttablename.Text & "'"
            Dim cmdAddColumn As New SqlCommand(FinalString, connection)
            connection.Open()
            cmdAddColumn.ExecuteNonQuery()
            connection.Close()
            cmdAddColumn.Dispose()
            Dim cmdread As New SqlCommand("select Visiblecolumn from warslobtablemaster where tablename='" & txtTbalename.Value & "'", connection)
            Dim drgettype As SqlDataReader
            connection.Open()
            drgettype = cmdread.ExecuteReader
            If drgettype.Read Then
                VisCol = drgettype("Visiblecolumn")
            End If
            cmdread.Dispose()
            drgettype.Close()
            connection.Close()
            If VisCol <> "" Then
                VisCol = VisCol & "," & NewColName.Value

            End If
            '**********************************change for track**********************************************
            '*************change*************
            '            Dim cmdins2 = New SqlCommand("TrackRightsTable", con)
            '            cmdins2.CommandType = CommandType.StoredProcedure
            '            With cmdins2.Parameters

            '@TableID varchar(50),       
            '@ TableName varchar(50),        
            '@Action varchar(50),        
            ' @ CreatedOn datetime,        
            '@ CreatedBy varchar(50),        
            '@ DepartmentID int,        
            '@ ClientID int,        
            '@ UnderLOB int,  
            '@VisibleColumn varchar(100),
            '@ LastModified int,
            '@ LastModifiedBy int,
            '@ CurrentDate int,
            '@ Editable int,
            '@ Importable int,
            '@ LocalTable int      


            '                .AddWithValue("@TableID ", loggedId)
            '                .AddWithValue("@TableName", "Delete Rights")
            '                .AddWithValue("@Date", System.DateTime.Now)

            '                .AddWithValue("@Entity", "Table")
            '                .AddWithValue("@EntityName", tableuse)

            '                .AddWithValue("@DeptId", ddlDepartment.SelectedValue)
            '                Dim clta As Integer
            '                Dim lobt As Integer

            '                If IsNumeric(ddlClient.SelectedValue) Then
            '                    clta = ddlClient.SelectedValue
            '                Else
            '                    clta = 0
            '                End If
            '                If IsNumeric(ddlLob.SelectedValue) Then
            '                    lobt = ddlLob.SelectedValue
            '                Else
            '                    lobt = 0
            '                End If
            '                .AddWithValue("@ClientId", clta)
            '                .AddWithValue("@LOBId", lobt)
            '                .AddWithValue("@All", "Delete All")
            '            End With
            '            con.Open()
            '            cmdins2.ExecuteNonQuery()
            '            con.Close()
            '            cmdins2.Dispose()
            '            ShowConfirm("View Deleted")
            '*************change*************

            '**********************************change for track**********************************************
            Dim cmdUpVisColumn As New SqlCommand("update warslobtablemaster set Visiblecolumn='" & VisCol & "' where tablename='" & txtTbalename.Value & "'", connection)
            connection.Open()
            cmdUpVisColumn.ExecuteNonQuery()
            connection.Close()
            cmdUpVisColumn.Dispose()
            ShowConfirm("Column Add Successfully")

            '''''''''''''''''''''''''''Track........................

            Dim cmdtrack As New SqlCommand
            cmdtrack.CommandType = CommandType.StoredProcedure
            cmdtrack.CommandText = "sp_LogTableTool_Track"
            cmdtrack.Connection = connection2
            With cmdtrack.Parameters
                .AddWithValue("@TableName", Me.txttablename.Text)
                .AddWithValue("@Action", "Add Column")
                .AddWithValue("@CreatedBy", Session("Userid"))
                .AddWithValue("@CreatedOn", System.DateTime.Today)
                .AddWithValue("@type", Session("usertype"))
            End With
            connection2.Open()
            cmdtrack.ExecuteNonQuery()
            connection2.Close()
            cmdtrack.Dispose()


            Me.txtsize.Value = ""
            txtTbalename.Value = ""
            NewColName.Value = ""
            txtdefault.Value = ""
            dvAddColumn.Visible = False
            Clear()
            Me.ddlTablename.Items.Remove(Me.txttablename.Text)

        Catch ex As Exception
            Dim dg As String = ex.ToString
            Dim r As String = dg.Replace("(", "")
            r = r.Replace(",", "")
            r = r.Replace("'", "")
            r = r.Replace(vbNewLine, "")
            ShowConfirm(r)

        End Try
    End Sub


    Private Sub Close_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Close.ServerClick
        Me.txtsize.Value = ""
        txtTbalename.Value = ""
        NewColName.Value = ""
        txtdefault.Value = ""
        dvAddColumn.Visible = False
        'Clear()
        '****************************Close**************************
        ddlTablename.SelectedIndex = 0
        'Me.ddlTablename.Items.Remove(Me.txttablename.Text)

        '*********************************End**********************************
    End Sub

    Protected Sub cmdAddColumn_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAddColumn.Click
        '******************************validation******************************************************

        'Dim tablevalue = (Request("ddltablename"))
        tablevalue = ddlTablename.SelectedValue

        If tablevalue = "" Or tablevalue = "--Select--" Then
            ShowConfirm("First You Select Table!!!!!!")
            'Me.DepartmentName.SelectedIndex = 0
            Exit Sub
        End If

        If (Me.divDatalist.Visible = True) Then
            ShowConfirm("Click the close button first to close the alter pane.")
            Exit Sub
        End If
        If (Me.dvAddColumn.Visible = True) Then
            ShowConfirm("Click the close button first to close the alter pane.")
            Exit Sub
        End If


        Dim columnnamearr = Split(Trim((ddlTablename.SelectedItem.Text)), "$")
        Dim combovalue = Trim(columnnamearr(0))
        txttablename.Text = ""
        txttablename.Text = combovalue
        txtTbalename.Value = combovalue
        dvAddColumn.Visible = True
        'Me.ddlTablename.Items.Insert(0, combovalue)As it Fix Table Name in Table comboBox

        Dim clientv As String
        Dim lobv As String
        Dim clnt = Request("Clientname")
        Dim lobn = Request("ddllobname")
    End Sub

    Protected Sub ddlTablename_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlTablename.SelectedIndexChanged
        cmdDeleterecord.Visible = True
        cmdchastrutable.Visible = True
        cmdDeleteTable.Visible = True
        cmdAddColumn.Visible = True
        dvAddColumn.Visible = False
    End Sub

    Protected Sub ddlLobname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLobname.SelectedIndexChanged
        Dim dept As String = DepartmentName.SelectedValue
        Dim client As String = Clientname.SelectedValue
        Dim lob As String = ddlLobname.SelectedValue
        Dim user As String = Session("userid")
        ddlTablename_bind1(dept, client, lob, user)
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
        Clientname.DataBind()
        Clientname.Items.Insert(0, "--select--")
    End Sub
    

    Protected Sub ClientName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
        If (ClientName.SelectedValue = "-- Select --") Then
            aspnet_msgbox("Please Select ClientName")
        End If
        con.Open()
        cmd = New SqlCommand("select * from WARSLobMaster where deptid='" + DepartmentName.SelectedValue + "' and  clientid= '" + ClientName.SelectedValue + "'", con)
        dr = cmd.ExecuteReader()
        ddlLobname.DataSource = dr
        ddlLobname.DataTextField = "LOBName"
        ddlLobname.DataValueField = "autoid"
        ddlLobname.DataBind()
        ddlLobname.Items.Insert(0, "--select--")
    End Sub

    Protected Sub gettable_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gettable.Click
        ddlTablename_bind()
    End Sub
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
End Class
