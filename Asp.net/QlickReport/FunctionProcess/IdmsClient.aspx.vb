Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Partial Class Function_And_Process_IdmsClient
    Inherits System.Web.UI.Page
    Dim strcon As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(strcon)
    Dim conCheck As New SqlConnection(strcon)
    Dim objclass As New Library()


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Session("userid") = "peter4200"
        'Session("typeofuser") = "admin"

        If Session("Userid") = "" Then
            Response.Redirect("/" & Session("projName") & "/Misc/sessionexpire.aspx")
        Else
            If IsPostBack = False Then
                Dim comdepart As New SqlCommand("select * from idmsdepartment where SavedBy='" + Session("userid") + "' ", connection)
                Dim da As New SqlDataAdapter
                da.SelectCommand = comdepart
                Dim ds As New DataSet
                da.Fill(ds)
                department.DataTextField = "DepartmentName"
                department.DataValueField = "autoid"
                department.DataSource = ds
                department.DataBind()
                department.Items.Insert("0", "--Select--")
                dlbind(grdlob)
                Me.pnlConfirmDel.Visible = False
                End If
        End If
        Dim cmd As SqlCommand
        If (Session("typeofuser") = "Super Admin") Then
            cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("userid").ToString() + "'", connection)
        Else
            cmd = New SqlCommand("Select MenuDescription from nlvl_menu_FP as a, nlvl_menu as b where a.menuid=b.MenuID and a.createdby='" + Session("CreatedBy").ToString() + "'", connection)
        End If
        Dim dsar As DataSet = New DataSet()
        Dim daar As SqlDataAdapter = New SqlDataAdapter(cmd)
        daar.Fill(dsar)
        Dim val1 As String
        Dim val2 As String
        Dim val3 As String
        val1 = dsar.Tables(0).Rows(0)("MenuDescription").ToString()
        val2 = dsar.Tables(0).Rows(1)("MenuDescription").ToString()
        val3 = dsar.Tables(0).Rows(2)("MenuDescription").ToString()
        lbl1.Text = val1
        lbl2.Text = val2
    End Sub

    Private Function ValidateLOB(ByVal lob)
        If objclass.checkAlphabetic(lob) = False Then
            WARSShowMsg("Level Name Must Be Alphabetic.")
            WARSSetFocus(txtclient)
            Return False
        Else
            Return True

        End If
    End Function
    Public Sub WARSSetFocus(ByVal FocusControl As Control)
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
    Public Sub WARSShowMsg(ByVal strmsg As String)
        'alert function for message display
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub

    Private Function CheckExist(ByVal lob As String)
        Dim com As New SqlCommand("select * from idmsclient where clientname='" & lob & "'", connection)
        Dim rdr As SqlDataReader
        connection.Open()
        rdr = com.ExecuteReader
        If rdr.Read Then
            WARSShowMsg("This Level Name Already Exists")
            rdr.Close()
            connection.Close()
            com.Dispose()
            Return False
        End If
        rdr.Close()
        connection.Close()
        com.Dispose()
        Return True


    End Function

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        Try
            If ValidateLOB(txtclient.Value) = False Then
                Exit Sub
            End If
            If department.SelectedIndex = 0 Then
                WARSShowMsg("Please Select Level1 Name!")
            End If
            If txtclient.Value = "" Then
                WARSShowMsg("Fill Level2 Name!")
                Exit Sub
            End If
            'If CheckExist(txtclient.Value) = False Then
            '    Exit Sub
            'End If
            Dim com As New SqlCommand("Insert_WARSAddClientMaster", connection)
            connection.Open()
            com.CommandType = CommandType.StoredProcedure
            With com.Parameters
                .Add("@Departmentid", department.Value)
                .Add("@ClientName", txtclient.Value)
                .Add("@Savedate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
                .Add("@message", txtmsg.Value)
                .Add("@SavedBy", Session("userid"))
            End With
            If com.ExecuteNonQuery() <> -1 Then
                WARSShowMsg("Level2 Added Successfully.")
                txtclient.Value = ""
                txtmsg.Value = ""
            End If
            com.Dispose()
            connection.Close()
            dlbind(grdlob)
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub

    Private Sub grdlob_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdlob.ItemCommand
        If e.CommandName = "ClientName" Then
            If txtsortorder.Text = "ASC" Then
                txtsortorder.Text = "DESC"
            Else
                txtsortorder.Text = "ASC"
            End If

            txtSort.Text = "clientname"
            dlbind(grdlob)

        End If
        If e.CommandName = "Select All" Then
            '''''''To Select all items
            Dim myDataGridItem As DataGridItem

            For Each myDataGridItem In grdlob.Items
                CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True
            Next

        End If

        If e.CommandName = "DeSelect All" Then
            '''''''To Select all items
            Dim myDataGridItem As DataGridItem

            For Each myDataGridItem In grdlob.Items
                CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = False
            Next

        End If

        If e.CommandName = "Edit" Then

            ' Dim index As Integer = e.Item.ItemIndex
            ' dlbind(grdlob)
            ' grdlob.EditItemIndex = index


            CType(e.Item.FindControl("txtLOB"), TextBox).Visible = True

            'CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = True
            CType(e.Item.FindControl("txtLOB"), TextBox).Text = CType(e.Item.FindControl("LnkLOB"), Label).Text
            CType(e.Item.FindControl("txtmessage"), TextBox).Visible = True
            CType(e.Item.FindControl("LnkLOB"), Label).Visible = False
            CType(e.Item.FindControl("lnkmsg"), Label).Visible = False
            CType(e.Item.FindControl("txtmessage"), TextBox).Text = CType(e.Item.FindControl("lnkmsg"), Label).Text
            CType(e.Item.FindControl("lnkupdate"), LinkButton).Visible = True
            CType(e.Item.FindControl("lnkcancel"), LinkButton).Visible = True
            CType(e.Item.FindControl("lnkedit"), LinkButton).Visible = False
            'grdlob.EditItemIndex = e.Item.ItemIndex
        End If
        'And CType(e.Item.FindControl("txtLOB"), TextBox).Text <> "CRT" And CType(e.Item.FindControl("txtLOB"), TextBox).Text <> "Sampark" And CType(e.Item.FindControl("txtLOB"), TextBox).Text <> "Support"
        If e.CommandName = "EditSave" Then
            Dim autoid, lob, msg
            autoid = grdlob.DataKeys(e.Item.ItemIndex)
            msg = CType(e.Item.FindControl("txtmessage"), TextBox).Text
            lob = CType(e.Item.FindControl("txtLOB"), TextBox).Text
            If lob = "" Then
                'CType(e.Item.FindControl("txtLOB"), TextBox).Visible = False
                'CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = False
                WARSShowMsg(" Level Name can not be Blank")
                grdlob.EditItemIndex = e.Item.ItemIndex
                grdlob.EditItemIndex = -1
                dlbind(grdlob)
                Exit Sub
            End If
            If ValidateLOB(lob) = False Then
                Exit Sub
            End If
            Dim com As New SqlCommand("select * from idmsclient where clientname='" & lob & "' and Autoid != autoid", connection)
            Dim rdr As SqlDataReader
            connection.Open()
            rdr = com.ExecuteReader
            If rdr.Read Then
                WARSShowMsg("This Level Name Already Exists")
                rdr.Close()
                connection.Close()
                com.Dispose()
                Exit Sub
            End If
            rdr.Close()
            connection.Close()
            com.Dispose()
            UpdateLobName(lob, msg, autoid)
            CType(e.Item.FindControl("txtLOB"), TextBox).Visible = False
            CType(e.Item.FindControl("cmdimgEdit"), ImageButton).Visible = False
            CType(e.Item.FindControl("txtmessage"), TextBox).Visible = False
            CType(e.Item.FindControl("cmdimgmsg"), ImageButton).Visible = False
            CType(e.Item.FindControl("lnkmsg"), Label).Visible = True
            CType(e.Item.FindControl("LnkLOB"), Label).Visible = True
            'CType(e.Item.FindControl("lnkupdate"), LinkButton).Visible = False
            'CType(e.Item.FindControl("lnkcancel"), LinkButton).Visible = False
            'CType(e.Item.FindControl("lnkedit"), LinkButton).Visible = True
            dlbind(grdlob)
        End If
        If e.CommandName = "CancelEdit" Then
            grdlob.EditItemIndex = e.Item.ItemIndex
            grdlob.EditItemIndex = -1
            dlbind(grdlob)
        End If


    End Sub
    Private Sub UpdateLobName(ByVal name, ByVal msg, ByVal autoid)
        Dim com As New SqlCommand("update idmsclient set clientname='" & name & "',message='" & msg & "' where autoid=" & autoid, connection)
        connection.Open()
        If com.ExecuteNonQuery() <> -1 Then
            WARSShowMsg("Level Updated Successfully.")
        End If
        connection.Close()
        com.Dispose()
    End Sub





    Private Sub grdlob_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles grdlob.SortCommand
        If txtSort.Text = e.SortExpression.ToString Then

            If txtsortorder.Text = "ASC" Then
                txtsortorder.Text = "DESC"
            Else
                txtsortorder.Text = "ASC"
            End If
        Else
            txtsortorder.Text = "ASC"
        End If
        txtSort.Text = e.SortExpression
        dlbind(grdlob)
    End Sub
    Private Sub grdlob_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdlob.DeleteCommand
        'If e.CommandName = "Delete" Then
        '    Me.pnlConfirmDel.Visible = True
        'End If
        Dim myDataGridItem As DataGridItem
        Dim ischeck As Boolean = False
        If e.CommandName = "Delete" Then
            For Each myDataGridItem In grdlob.Items
                If (CType(myDataGridItem.FindControl("chkSelect"), CheckBox).Checked = True) Then
                    ischeck = True
                End If
            Next

            If (ischeck) Then
                Me.pnlConfirmDel.Visible = True

            Else
                WARSShowMsg("Select Atleast One Item")
            End If
        End If
    End Sub

    Private Function del_Confirmed()
        '''''''''''''Initialize variable/Object
        Dim itm As DataGridItem
        Dim autoid
        Dim strLOBName As String = ""
        For Each itm In grdlob.Items
            If CType(itm.FindControl("chkSelect"), CheckBox).Checked = True Then
                autoid = grdlob.DataKeys(itm.ItemIndex)
                Dim comlob As New SqlCommand("select clientname from  idmsclient where autoid='" & autoid & "'", conCheck)
                conCheck.Open()
                Dim rdrlob As SqlDataReader
                rdrlob = comlob.ExecuteReader
                If rdrlob.Read Then
                    ''''If Trim(rdrlob("lob")) = "CRT" Or Trim(rdrlob("lob")) = "Sampark" Or Trim(rdrlob("lob")) = "Support" Then

                    ''''    If strLOBName = "" Then
                    ''''        strLOBName = rdrlob("lob")
                    ''''    Else
                    ''''        strLOBName = strLOBName & "," & rdrlob("lob")
                    ''''    End If
                    ''''Else
                    Dim count As Integer
                    Dim countclient As New SqlCommand("select count(clientid) from warslobmaster where clientid='" & autoid & "'", connection)
                    connection.Open()
                    count = countclient.ExecuteScalar
                    connection.Close()
                    If (count = 0) Then
                        Dim querydelete As String = "Delete from idmsclient where autoid = " & autoid
                        Dim cmddelete As New SqlCommand(querydelete, connection)
                        connection.Open()
                        cmddelete.ExecuteNonQuery()
                        connection.Close()
                        cmddelete.Dispose()
                    Else
                        WARSShowMsg("Sorry you can not delete:LOBs exist under this Client ")
                    End If
                    ''End If
                End If
                    rdrlob.Close()
                    conCheck.Close()
                    comlob.Dispose()
                End If
        Next
        dlbind(grdlob)
        If strLOBName <> "" Then
            WARSShowMsg("Sorry You Can Not Delete " & strLOBName)
        End If
        Return 1
    End Function
    Private Sub dlbind(ByVal control As WebControls.DataGrid)
        Dim objcmd
        objcmd = New SqlCommand("select ic.AutoId,ic.ClientName,ic.message,SaveDate=convert(varchar,ic.SaveDate,106),ic.SavedBy,id.departmentname from idmsclient ic,idmsdepartment id where ic.deptid = id.autoid and ic.SavedBy='" + Session("userid") + "'  order by id.departmentname", connection)
        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet
        objadp.SelectCommand = objcmd
        connection.Open()
        objadp.Fill(objds)
        Dim dv As DataView = New DataView(objds.Tables(0))
        If Trim(txtSort.Text) <> "" Then
            dv.Sort = txtSort.Text.ToString & " " & txtsortorder.Text
        Else
            dv.Sort = "Clientname"
        End If
        If dv.Count <> 0 Then
            control.DataSource = dv
            control.DataBind()
            control.Visible = True
        Else
            control.Visible = False
        End If
        connection.Close()
        objcmd.dispose()
        objds.Dispose()
    End Sub
    
    Private Sub cmdYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        Try
            del_Confirmed()
            Me.pnlConfirmDel.Visible = False
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            WARSShowMsg(strmsg)
        End Try
    End Sub

    Private Sub cmdNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        Me.pnlConfirmDel.Visible = False
        dlbind(grdlob)
    End Sub
    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        department.SelectedIndex = 0
        txtclient.Value = ""
        txtmsg.Value = ""
    End Sub

    Private Sub grdlob_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles grdlob.PageIndexChanged
        grdlob.CurrentPageIndex = e.NewPageIndex
        dlbind(grdlob)
    End Sub

    Protected Sub grdlob_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles grdlob.EditCommand
        'grdlob.EditItemIndex = e.Item.ItemIndex
        'dlbind(grdlob)
        'CType(e.Item.FindControl("txtLOB"), TextBox).Text = CType(e.Item.FindControl("LnkLOB"), Label).Text
        'CType(e.Item.FindControl("txtmessage"), TextBox).Visible = True
        'CType(e.Item.FindControl("LnkLOB"), Label).Visible = False
        'CType(e.Item.FindControl("lnkmsg"), Label).Visible = False
        'CType(e.Item.FindControl("txtmessage"), TextBox).Text = CType(e.Item.FindControl("lnkmsg"), Label).Text
        'CType(e.Item.FindControl("lnkupdate"), LinkButton).Visible = True
        'CType(e.Item.FindControl("lnkcancel"), LinkButton).Visible = True
        'CType(e.Item.FindControl("lnkedit"), LinkButton).Visible = False
    End Sub

    Protected Sub txtmsg_ServerChange(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtmsg.ServerChange

    End Sub
End Class
