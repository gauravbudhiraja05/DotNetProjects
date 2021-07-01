Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.DBNull
Imports System.IO
Imports System.Web.UI.HtmlControls.HtmlTextArea

Partial Class FunctionProcess_AddNews
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionstring")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connection3 As New SqlConnection(constr)
    Dim connection4 As New SqlConnection(constr)
    Dim connection5 As New SqlConnection(constr)
    Dim connection6 As New SqlConnection(constr)
    Dim objds As New DataSet
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        'Put user code to initialize the page here
        If Me.IsPostBack = False Then

            pnlConfirmDel.Visible = False
            dbregInfo()
            tabAddNew.Visible = False
            '********For Track Management********************
            Dim cmdCal As New SqlCommand
            cmdCal.Connection = connection
            cmdCal.CommandText = "select Username from Registration where userid='" & Session("Userid") & "'"
            connection.Open()
            Dim UserName = cmdCal.ExecuteScalar
            connection.Close()
            'This code is going to be commented till track management is not prepared
            'Dim cmdsaveTrack As New SqlCommand("Insert_IDMSTrackManagement", connection)
            'cmdsaveTrack.CommandType = CommandType.StoredProcedure
            'With cmdsaveTrack.Parameters
            '    .Add("@UserId", trim(Session("Userid")))
            '    .Add("@FormName", "AddNews")
            '    .Add("@Comment", "Add Edit Delete Flash Messages")
            '    .Add("@Addedon", System.DateTime.Today)
            'End With
            'connection.Open()
            'cmdsaveTrack.ExecuteNonQuery()
            'connection.Close()
            'cmdsaveTrack.Dispose()
            '**************************************************
        End If

    End Sub
    Public Sub dbregInfo()
        Try
            Dim objcmd As New SqlCommand("select *,convert(varchar,startdate,110) as SDate,convert(varchar,Enddate,110) as EDate from warsnews", connection)
            ' Response.Write("select col.name as ColName,typ.name as Type ,col.length as Size from syscolumns col,systypes typ,sysobjects obj where col.id=obj.id and obj.xtype='U' and obj.name='" & ddlTablename.SelectedValue & "' and typ.xtype=col.xtype")
            Dim objadp As New SqlDataAdapter
            objadp.SelectCommand = objcmd
            connection.Open()
            objadp.Fill(objds, "table")
            connection.Close()
            dlreg.DataSource = objds
            dlreg.DataBind()
            objds.Clear()
            objcmd.Dispose()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub cmdAddnew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdAddnew.Click
        tabAddNew.Visible = True
    End Sub

    Private Sub cmdCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        tabAddNew.Visible = False
        'txtStDate.Value = System.DateTime.Today
        'txtEnDate.Value = System.DateTime.Today
        Me.txtStDate.Text = ""
        Me.txtEnDate.Text = ""
        Me.txtnNews.InnerText = ""

    End Sub
    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        Page.RegisterStartupScript("ShowConfirm", Script)
    End Function
    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click
       
        Dim StartDate As Date
        Dim EndDate As Date
        Dim today As Date
        Try
            If txtStDate.Text <> "" And Me.txtEnDate.Text <> "" And Me.txtnNews.InnerText <> "" Then

                Try
                    StartDate = CType(txtStDate.Text, Date)
                    EndDate = CType(txtEnDate.Text, Date)
                Catch ex As Exception
                    ShowConfirm("Date is not In Correct Format")
                End Try
                today = DateTime.Now.Date
                If StartDate < today Then
                    ShowConfirm("Start Date Should Be Greater than or equal to Today")
                    Exit Sub

                End If


                Dim test As Decimal = DateDiff(DateInterval.Day, StartDate, EndDate)
                If (Left(test, 1) = "-") Then
                    ShowConfirm("Start Date Should Be Less Than End Date")

                Else
                    Dim cmd As New SqlCommand("insert_warsnews", connection)
                    cmd.CommandType = CommandType.StoredProcedure
                    With cmd.Parameters
                        .Add("@StartDate", FormatDateTime(txtStDate.Text, DateFormat.ShortDate))
                        .Add("@EndDate", FormatDateTime(Me.txtEnDate.Text, DateFormat.ShortDate))
                        .Add("@news", Trim(Me.txtnNews.InnerText))
                        .Add("@Savedate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
                        .Add("@SaveBy", Session("userid"))


                    End With
                    connection.Open()
                    cmd.ExecuteNonQuery()
                    connection.Close()
                    cmd.Dispose()
                    dbregInfo()
                    ShowConfirm("Record Saved Successfully")
                    txtStDate.Text = ""
                    txtEnDate.Text = ""
                    txtnNews.InnerText = ""
                End If


            Else
                ShowConfirm("Please Fill The Blank Field First.")
            End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub dlreg_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.EditCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        dbregInfo()
    End Sub

    Private Sub dlreg_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.CancelCommand
        Me.dlreg.EditItemIndex = e.Item.ItemIndex
        Me.dlreg.EditItemIndex = -1
        dbregInfo()
    End Sub

    Private Sub dlreg_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.ItemCommand
        Try
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
            'If e.CommandName = "imageFromDate" Then
            '    'ShowConfirm("Hello")
            '    'CType(e.Item.FindControl("txtStartDate"), HtmlInputText).Value()= window.showModalDialog('/IDMS/Calender/Calendar.htm',CType(e.Item.FindControl("txtStartDate"), HtmlInputText).Value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
            '    Dim win As New System.Text.StringBuilder
            '    With win
            '        .Append("<script language='javascript'>")
            '        .Append("alert('its working');")
            '        '.Append("a=window.showModalDialog('/IDMS/Calender/Calendar.htm',document.all["txtStartDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;'); 
            '        .Append("document.All['dlreg:_ctl1:txtStartDate'].value='hi'")
            '        .Append("</script>")
            '    End With
            '    RegisterStartupScript("alert", win.ToString())
            'End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub dlreg_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.UpdateCommand
        Try
            Dim stdate As Date
            Dim endate As Date
            Dim news As String
            Dim intrec As Integer
            If CType(e.Item.FindControl("txtstrdate"), TextBox).Text = "" Then
                ShowConfirm("Start date Cannot be Empty")
                dlreg.EditItemIndex = -1
                dbregInfo()
                Exit Sub

            Else
                stdate = CType(e.Item.FindControl("txtstrdate"), TextBox).Text
            End If
            If CType(e.Item.FindControl("txtEnddate"), TextBox).Text = "" Then
                ShowConfirm("End date Cannot be Empty")
                dlreg.EditItemIndex = -1
                dbregInfo()
                Exit Sub

            Else
                endate = CType(e.Item.FindControl("txtEnddate"), TextBox).Text
            End If

            news = CType(e.Item.FindControl("txtNews"), TextBox).Text
            Dim index = CType(e.Item.FindControl("txtindex"), TextBox).Text
            'If stdate = "" Then
            '    ShowConfirm("Start date Cannot be Empty")
            '    dlreg.EditItemIndex = -1
            '    dbregInfo()
            '    Exit Sub
            'End If

            'If endate = "" Then
            '    ShowConfirm("End date Cannot be Empty")
            '    dlreg.EditItemIndex = -1
            '    dbregInfo()
            '    Exit Sub
            'End If


            If stdate > endate Then

                ShowConfirm("Start date should be less than Enddate")
                dlreg.EditItemIndex = -1
                dbregInfo()
                Exit Sub

            End If
            If news = "" Then
                ShowConfirm("Message can not be blank")
                dlreg.EditItemIndex = -1
                dbregInfo()
                Exit Sub
            End If
            If index = "" Then
                ShowConfirm("Index can not be blank")
                dlreg.EditItemIndex = -1
                dbregInfo()
                Exit Sub
            End If
            If IsNumeric(index) = False Then
                ShowConfirm("Index should be numeric")
                Exit Sub
            End If
            intrec = dlreg.DataKeys(e.Item.ItemIndex)
            Dim query As String = "update warsnews set StartDate='" & stdate & "' , EndDate='" & endate & "' , news='" & news & "',ShowIndex='" & index & "',Savedby='" & Session("userid") & "',Savedate='" & System.DateTime.Now() & "' where rec_id=' " & intrec & "' "
            Dim objcmd1 As New SqlCommand(query, connection)
            connection.Open()
            objcmd1.ExecuteNonQuery()
            connection.Close()
            'ShowConfirm("Table Updated successfully|||||")
            dlreg.EditItemIndex = -1
            dbregInfo()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub dlreg_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlreg.Disposed

    End Sub

    Private Sub dlreg_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.DeleteCommand
        pnlConfirmDel.Visible = True
        'Try
        '    Dim countDelete
        '    Dim chkDelete As New CheckBox
        '    Dim RecordId As Integer
        '    Dim i As Integer
        '    For i = 0 To dlreg.Items.Count - 1
        '        RecordId = Me.dlreg.DataKeys(Me.dlreg.Items(i).ItemIndex)
        '        If CType(Me.dlreg.Items(i).FindControl("chkVisible"), CheckBox).Checked = True Then
        '            Dim querydelete As String = "Delete from warsnews where rec_id = '" & RecordId & "'"
        '            Dim cmddelete As New SqlCommand(querydelete, connection)
        '            connection.Open()
        '            cmddelete.ExecuteNonQuery()
        '            connection.Close()
        '            cmddelete.Dispose()
        '        End If
        '    Next
        '    dbregInfo()
        'Catch ex As Exception
        '    Dim strmsg As String
        '    strmsg = Replace(ex.Message.ToString, "'", "")
        '    strmsg = Replace(strmsg, vbCrLf, " ")
        '    ShowConfirm(strmsg)
        'End Try
    End Sub

    Protected Sub dlreg_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlreg.SelectedIndexChanged

    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        Try
            Dim countDelete
            Dim chkDelete As New CheckBox
            Dim RecordId As Integer
            Dim i As Integer
            For i = 0 To dlreg.Items.Count - 1
                RecordId = Me.dlreg.DataKeys(Me.dlreg.Items(i).ItemIndex)
                If CType(Me.dlreg.Items(i).FindControl("chkVisible"), CheckBox).Checked = True Then
                    Dim querydelete As String = "Delete from warsnews where rec_id = '" & RecordId & "'"
                    Dim cmddelete As New SqlCommand(querydelete, connection)
                    connection.Open()
                    cmddelete.ExecuteNonQuery()
                    connection.Close()
                    cmddelete.Dispose()
                End If
            Next
            dbregInfo()
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
        pnlConfirmDel.Visible = False
    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        pnlConfirmDel.Visible = False
    End Sub
End Class
