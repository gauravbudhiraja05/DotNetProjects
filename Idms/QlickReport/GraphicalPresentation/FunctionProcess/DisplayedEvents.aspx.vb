Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Web.UI.HtmlControls.HtmlTextArea


Partial Class Function_And_Process_DisplayedEvents
    Inherits System.Web.UI.Page

    Dim constr As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(constr)


    Dim connection1 As New SqlConnection(constr)
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub
    Protected WithEvents txtTickerstartdate As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtTickerenddate As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtstartdate As System.Web.UI.HtmlControls.HtmlInputText
    Protected WithEvents txtenddate As System.Web.UI.HtmlControls.HtmlInputText


    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load


        'If Session("Userid") = "" Then
        '    Response.Redirect("/" & Session("projName") & "/Misc/sessionexpire.aspx")
        'Else
        pnlConfirmDel.Visible = False

        If Me.IsPostBack = False Then
            bindnewsmonth()
            bindnewsdate()
            bindnewsyear()
            bindnewsmonth1()
            bindnewsdate1()
            bindnewsyear1()
            dbbind(dlexistnews)
        End If
        'End If
    End Sub

    Public Function bindnewsmonth()
        Dim i
        For i = 1 To 12
            If i <= 9 Then
                Me.cbonewsmonth.Items.Add("0" & i)
            Else
                Me.cbonewsmonth.Items.Add(i)
            End If
        Next
        Me.cbonewsmonth.Items.Insert("0", "-Month-")
    End Function
    Public Function bindnewsdate()
        Dim i
        For i = 1 To 31
            If i <= 9 Then
                Me.cbonewsdate.Items.Add("0" & i)
            Else
                Me.cbonewsdate.Items.Add(i)
            End If
        Next
        Me.cbonewsdate.Items.Insert("0", "-Date-")
    End Function
    Public Function bindnewsyear()
       
        'Dim d As Integer = DateTime.Now.Year
        'Dim year As Integer = d
        'Dim NextYear As Integer = year + 1
        'Dim PreYear As Integer = year - 1
        'Dim i As Integer = 0
        ''cboyear.Items
        'cbonewsyear.Items.Insert("0", "-Year-")
        'cbonewsyear1.Items.Insert("0", "-Year-")

        'cbonewsyear.Items.Add(year - 1)
        'cbonewsyear1.Items.Add(year - 1)

        'For i = 0 To 9
        '    cbonewsyear.Items.Add(year + i)
        '    cbonewsyear1.Items.Add(year + i)
        'Next


        'Return 0

        Dim i
        For i = 2025 To 2005 Step -1
            Me.cbonewsyear.Items.Add(i)
        Next
        Me.cbonewsyear.Items.Insert("0", "-Year-")
        
    End Function
    Public Function bindnewsmonth1()
        Dim i
        For i = 1 To 12
            If i <= 9 Then
                Me.cbonewsmonth1.Items.Add("0" & i)
            Else
                Me.cbonewsmonth1.Items.Add(i)
            End If
        Next
        Me.cbonewsmonth1.Items.Insert("0", "-Month-")
    End Function
    Public Function bindnewsdate1()
        Dim i
        For i = 1 To 31
            If i <= 9 Then
                Me.cbonewsdate1.Items.Add("0" & i)
            Else
                Me.cbonewsdate1.Items.Add(i)
            End If
        Next
        Me.cbonewsdate1.Items.Insert("0", "-Date-")
    End Function
    Public Function bindnewsyear1()
        Dim i
        For i = 2025 To 2005 Step -1
            Me.cbonewsyear1.Items.Add(i)
        Next
        Me.cbonewsyear1.Items.Insert("0", "-Year-")

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
    Public Sub showmsg(ByVal strmsg As String)
        Dim str As String
        str = "<Script language='javascript'>"
        str = str + "alert('" + strmsg + "')"
        str = str + "</Script>"
        RegisterStartupScript("showmsg", str)
    End Sub
    Dim i = 0
    Public Function serial()
        i = i + 1
        Return i
    End Function

    Public Function ChkNewsblk()
        Dim str As String
        If txthead.Text = "" Then
            str = str & "Fill News HeadLine\n"
        ElseIf txtdesc.InnerText = "" And Me.txturlnews.Text = "" Then
            str = str & "Fill News Description or Enter URL\n"
        End If
        If cbonewsdate.SelectedIndex = 0 Then
            str = str & "Please select News Starting Date\n"
        End If
        If cbonewsmonth.SelectedIndex = 0 Then
            str = str & "Please select News Starting Month\n"
        End If
        If cbonewsyear.SelectedIndex = 0 Then
            str = str & "Please select News Starting Year\n"
        End If
        If cbonewsdate1.SelectedIndex = 0 Then
            str = str & "Please select News Ending Date\n"
        End If
        If cbonewsmonth1.SelectedIndex = 0 Then
            str = str & "Please select News Ending  Month\n"
        End If
        If cbonewsyear1.SelectedIndex = 0 Then
            str = str & "Please select News Ending Year\n"
        End If
        If Me.txturlnews.Text = "" Then
            str = str & "Please add  News URL\n"
        End If
        If str <> "" Then
            showmsg(str)
            Return False
        End If
        Return True
    End Function

    Private Sub cmdNewssave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewssave.Click
        Dim today As Date
        If ChkNewsblk() = False Then
            Exit Sub
        Else
            Try

            
                Dim newsstart = Me.cbonewsmonth.SelectedValue & "-" & Me.cbonewsdate.SelectedValue & "-" & Me.cbonewsyear.SelectedValue
                Dim newsend = Me.cbonewsmonth1.SelectedValue & "-" & Me.cbonewsdate1.SelectedValue & "-" & Me.cbonewsyear1.SelectedValue
                'If (newsstart > newsend) Then
                '    showmsg("Start Date Should Be Less Than End Date")
                'Else
                Dim StartDate As Date = CType(newsstart, Date)
                Dim EndDate As Date = CType(newsend, Date)
                If StartDate > EndDate Then

                    showmsg("Start date should be less than Enddate")

                    Exit Sub

                End If
                today = DateTime.Now.Date
                If StartDate < today Then
                    showmsg("Start Date Should Be Greater than or equal to Today")
                    Exit Sub

                End If
                Dim test As Decimal = DateDiff(DateInterval.Day, StartDate, EndDate)
                If (Left(test, 1) = "-") Then
                    showmsg("Start Date Should Be Less Than End Date")
                End If
                Dim objcmd As New SqlCommand("Insert_ManuNews", connection)
                objcmd.CommandType = CommandType.StoredProcedure
                With objcmd.Parameters
                    .Add("@Headline", txthead.Text)
                    .Add("@news", txtdesc.InnerText)
                    ' .Add("@AddDate", System.DateTime.Today)
                    .Add("@startdate", FormatDateTime(newsstart, DateFormat.ShortDate))
                    .Add("@Enddate", FormatDateTime(newsend, DateFormat.ShortDate))
                    .Add("@newsurl", Me.txturlnews.Text)
                    .Add("@Savedate", FormatDateTime(System.DateTime.Now, DateFormat.ShortDate))
                    .Add("@SaveBy", Session("userid"))

                End With
                connection.Open()
                objcmd.ExecuteNonQuery()
                connection.Close()
                objcmd.Dispose()
                showmsg("URL/Link Has Been Added Successfully")
                dbbind(dlexistnews)
                dlexistnews.Visible = True
                clearall()
            Catch ex As Exception
                showmsg("Enter Valid Date")
            End Try
        End If

        'End If
    End Sub
    Public Sub dbbind(ByVal control As WebControls.DataList)
        Dim objcmd As New SqlCommand("select autoid,Headline,News,CONVERT(varchar, SaveDate, 102) SaveDate,CONVERT(varchar, Startdate, 102) Startdate,CONVERT(varchar, Enddate, 102) Enddate,newsurl from ManuNews", connection)
        Dim objadp As New SqlDataAdapter
        Dim objds As New DataSet
        objadp.SelectCommand = objcmd
        connection.Open()
        objadp.Fill(objds)
        connection.Close()
        control.DataSource = objds
        control.DataBind()
    End Sub

    Private Sub clearall()
        txthead.Text = ""
        txtdesc.InnerText = ""
        Me.cbonewsdate.SelectedIndex = 0
        Me.cbonewsdate1.SelectedIndex = 0
        Me.cbonewsmonth.SelectedIndex = 0
        Me.cbonewsmonth1.SelectedIndex = 0
        Me.cbonewsyear.SelectedIndex = 0
        Me.cbonewsyear1.SelectedIndex = 0
        Me.txturlnews.Text = ""
    End Sub
    Private Sub dlexistnews_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlexistnews.EditCommand
        Me.dlexistnews.EditItemIndex = e.Item.ItemIndex
        txthead.Text = CType(e.Item.FindControl("lblN_H"), Label).Text
        txtrec_id.Text = dlexistnews.DataKeys(e.Item.ItemIndex)
        Dim cmd As New SqlCommand("select autoid,isnull(headline,'') as headline,isnull(news,'') as news,isnull(newsurl,'') as newsurl,convert(varchar,startdate,110) as startdate,convert(varchar,enddate,110) as enddate from manunews where autoid='" & txtrec_id.Text & "'", connection)
        Dim red As SqlDataReader
        connection.Open()
        red = cmd.ExecuteReader
        If red.Read Then
            txtdesc.InnerText = red("News")
            Dim newsstart = red("startdate")
            Dim arr()
            arr = Split(newsstart, "-")
            Dim k
            For k = 0 To arr.Length - 1
                Me.cbonewsmonth.SelectedValue = arr(0)
                Me.cbonewsdate.SelectedValue = arr(1)
                Me.cbonewsyear.SelectedValue = arr(2)
                Exit For
            Next
            Dim tickerend = red("enddate")
            Dim arr1()
            arr1 = Split(tickerend, "-")
            Dim k1
            For k1 = 0 To arr1.Length - 1
                Me.cbonewsmonth1.SelectedValue = arr1(0)
                Me.cbonewsdate1.SelectedValue = arr1(1)
                Me.cbonewsyear1.SelectedValue = arr1(2)
                Exit For
            Next
            Me.txturlnews.Text = red("newsurl")
        End If
        red.Close()
        connection.Close()
        cmd.Dispose()
        '''''''''''''''''''''''''''''''
        cmdNewssave.Enabled = False
        cmdNewsUpdate.Enabled = True
    End Sub
    Private Sub cmdNewsClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewsClear.Click
        cmdNewsUpdate.Enabled = False
        cmdNewssave.Enabled = True
        clearall()
        showmsg("Settings have been reset")
    End Sub
    Private Sub dlexistnews_DeleteCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlexistnews.DeleteCommand
        dlexistnews.EditItemIndex = e.Item.ItemIndex
        txtrec_id.Text = dlexistnews.DataKeys(e.Item.ItemIndex)
        pnlConfirmDel.Visible = True

        ''Dim query As String = "delete from ManuNews  where autoid = " & txtrec_id.Text & ""
        ''Dim objcmd1 As New SqlCommand(query, connection)
        ''connection.Open()
        ''objcmd1.ExecuteNonQuery()
        ''connection.Close()
        ''dbbind(dlexistnews)
        ''showmsg("URL/Link Deleted")
        ''dlexistnews.EditItemIndex = -1
    End Sub
    Private Sub cmdNewsUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdNewsUpdate.Click
        If ChkNewsblk() = False Then
            Exit Sub
        Else
            Try

            
                Dim newsstart = Me.cbonewsmonth.SelectedValue & "-" & Me.cbonewsdate.SelectedValue & "-" & Me.cbonewsyear.SelectedValue
                Dim newsend = Me.cbonewsmonth1.SelectedValue & "-" & Me.cbonewsdate1.SelectedValue & "-" & Me.cbonewsyear1.SelectedValue
                'If txthead.Text = "" Then
                '    showmsg(" Headline can not be blank")
                '    Exit Sub
                'End If
                'If txturlnews.Text = "" Then
                '    showmsg(" URL can not be blank")
                '    Exit Sub
                'End If

                Dim StartDate As Date = CType(newsstart, Date)
                Dim EndDate As Date = CType(newsend, Date)
                If StartDate > EndDate Then

                    showmsg("Start date should be less than Enddate")

                    Exit Sub

                End If
                Dim query As String = "update ManuNews set Headline = '" & txthead.Text & "',News = '" & txtdesc.InnerText & "',StartDate='" & newsstart & "',EndDate='" & newsend & "',newsurl='" & Me.txturlnews.Text & "',SavedBy='" & Session("userid") & "',SaveDate='" & System.DateTime.Now() & "'where autoid = " & txtrec_id.Text & ""
                Dim objcmd1 As New SqlCommand(query, connection)
                connection.Open()
                objcmd1.ExecuteNonQuery()
                connection.Close()
                dbbind(dlexistnews)
                cmdNewsUpdate.Enabled = False
                cmdNewssave.Enabled = True
                clearall()
                showmsg("URL/Link Edited")
            Catch ex As Exception
                showmsg("Enter Valid Date")
            End Try
        End If

    End Sub

    Protected Sub dlexistnews_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlexistnews.ItemCommand
        Try
            If e.CommandName = "Select All" Then
                '''''''To Select all items
                Dim myDataGridItem As DataListItem
                For Each myDataGridItem In dlexistnews.Items
                    CType(myDataGridItem.FindControl("chkVisible"), CheckBox).Checked = True
                Next
            End If
            If e.CommandName = "DeSelect All" Then
                '''''''To Select all items
                Dim myDataGridItem As DataListItem
                For Each myDataGridItem In dlexistnews.Items
                    CType(myDataGridItem.FindControl("chkVisible"), CheckBox).Checked = False
                Next
            End If

        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            showmsg(strmsg)
        End Try
    End Sub
    'Private Sub dlexistnews_Delete1Command(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataListCommandEventArgs) Handles dlreg.DeleteCommand
    '    Try
    '        Dim countDelete
    '        Dim chkDelete As New CheckBox
    '        Dim RecordId As Integer
    '        Dim i As Integer
    '        For i = 0 To dlexistnews.Items.Count - 1
    '            RecordId = Me.dlexistnews.DataKeys(Me.dlexistnews.Items(i).ItemIndex)
    '            If CType(Me.dlexistnews.Items(i).FindControl("chkVisible"), CheckBox).Checked = True Then
    '                Dim querydelete As String = "Delete from ManuNews where autoid = '" & RecordId & "'"
    '                Dim cmddelete As New SqlCommand(querydelete, connection)
    '                connection.Open()
    '                cmddelete.ExecuteNonQuery()
    '                connection.Close()
    '                cmddelete.Dispose()
    '            End If
    '        Next
    '        'dbbind()
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        showmsg(strmsg)
    '    End Try
    'End Sub
    Protected Sub dlexistnews_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlexistnews.SelectedIndexChanged

    End Sub

    Protected Sub cmdYes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdYes.Click
        Dim query As String = "delete from ManuNews  where autoid = " & txtrec_id.Text & ""
        Dim objcmd1 As New SqlCommand(query, connection)
        connection.Open()
        objcmd1.ExecuteNonQuery()
        connection.Close()
        dbbind(dlexistnews)
        showmsg("URL/Link Deleted")
        dlexistnews.EditItemIndex = -1
        pnlConfirmDel.Visible = False

    End Sub

    Protected Sub cmdNo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdNo.Click
        pnlConfirmDel.Visible = False

    End Sub
End Class
