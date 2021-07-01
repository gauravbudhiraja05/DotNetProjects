Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.IO.StreamReader
Imports System.Data

Partial Class QueryBuilder_SaveQuery

#Region "Variable Declration"
    Inherits System.Web.UI.Page
    Dim con As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(con)
    Dim connection1 As New SqlConnection(con)
    Dim connectionsave As New SqlConnection(con)
    Dim connectionsave1 As New SqlConnection(con)
    Dim connectionsave3 As New SqlConnection(con)
    Public QFilePath
    Dim strsqlshow
    Dim strsqlwhere
    Dim strsql
    Dim doct As IO.Directory
    Dim strsub1
    Dim path As String
    Dim filename As String
    Public QShowField
    Public QTableNAme
    Public QCriteria
    Dim i As Int16
#End Region

#Region "Page Load"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass))
        AjaxPro.Utility.RegisterTypeForAjax(GetType(AjaxClass1))
        If Me.IsPostBack = False Then
            callvalue()
            lblUser.Text = Session("userid")
            panconfirm.Visible = False
            txtshowfield.Text = Trim(Request("blank1"))
            txttbname.Text = Trim(Request("hidtablename"))
            txtCriteria.Text = Trim(Request("wheredata"))
            QFilePath = Trim(path)
            If Trim(Request("hidtablename")) <> "" Then

                If Trim(Request("blank1")) <> "" Then
                    strsqlshow = Trim(Request("blank1"))
                Else
                    strsqlshow = "*"
                End If

                If Trim(Request("wheredata")) <> "" Then
                    strsqlwhere = " where " & Trim(Request("wheredata"))
                Else
                    strsqlwhere = ""
                End If

                strsql = "select " & strsqlshow & " from " & Trim(Request("hidtablename")) & " " & strsqlwhere
                txttb.Text = strsql
            End If
            txthidden.Text = Request("sfilename")
            sfilename.Text = sfilename.Text

            '<------------------------Creating A main Directory--------------------------------------->

            'Dim fp As StreamWriter
            'If Not Directory.Exists(Server.MapPath("/IDMS/menu") & "\UsersSpace\" & Session("UserName")) Then
            '    '<----------------------Creating Directory for partcular user--------------------------------->
            '    Directory.CreateDirectory(Server.MapPath("/IDMS/menu") & "\UsersSpace\" & Session("UserName"))
            '    '<----------------------End of Creating Directory for partcular user------------------------>
            'End If
            '<------------------------End of Creating A main Directory--------------------------------------->


            'path = "UsersSpace/" & Session("Username") & "/" & Request("sfilename") & ".qry"

            'If File.Exists(Server.MapPath(path)) Then
            '    panconfirm.Visible = True
            'Else
            '<--------------------Creating a new text file---------------------------------->
            'fp = File.CreateText(Server.MapPath(path))
            'fp.WriteLine(strsql)
            'fp.Close()
            '<<<Insert file name in table of particular user>>>
            'savedby,tablename,queryname,lobyname

            Dim comdepart As New SqlCommand("select DepartmentName,AutoID from idmsdepartment", connection)
            Dim da As New SqlDataAdapter
            da.SelectCommand = comdepart
            Dim ds As New DataSet
            connection.Open()
            da.Fill(ds)
            connection.Close()
            'cbodept.DataTextField = "DepartmentName"
            'cbodept.DataValueField = "autoid"
            'cbodept.DataSource = ds
            'cbodept.DataBind()
            'cbodept.Items.Insert("0", "--Select--")
            '************************************************************
            If Session("mailtain") Is Nothing Then
                Exit Sub
            End If
            Dim sessiondata As String() = Session("mailtain").split(",")

            'If (Trim(Request("type")) = "edit") Then
            'If Session("mailtain") <> "" Then
            '    If sessiondata(0) = "Local" Then
            '        Me.chkstatus.Checked = True
            '    Else
            '        Me.chkstatus.Checked = False
            '    End If
            '    cbodept.Text = sessiondata(1)
            '    BindClient()
            '    If sessiondata(2) = "0" Or sessiondata(2) = "" Then
            '        cboclient.SelectedIndex = 0
            '    Else
            '        '  Clientname.SelectedIndex = Request("client")
            '        cboclient.Text = sessiondata(2)
            '    End If
            '    BindLOB()
            '    If sessiondata(3) = "0" Or sessiondata(3) = "" Then
            '        cbolob.SelectedIndex = 0
            '    Else
            '        '  ddlLobName.SelectedIndex = Request("lob")
            '        cbolob.Text = sessiondata(3)
            '    End If
            '    d.Value = cbodept.SelectedValue
            '    c.Value = sessiondata(2)
            '    l.Value = sessiondata(3)
            '    q.Value = sessiondata(4)
            '    Dim cmdne As New SqlDataAdapter("select savedby from WARSQueryMaster where queryname='" + q.Value + "' and departmentid='" + d.Value + "' and clientid='" + c.Value + "' and lobyname='" + l.Value + "'", connection)
            '    Dim dsss As New DataSet
            '    cmdne.Fill(dsss)
            '    owner.Value = dsss.Tables(0).Rows(0)(0).ToString
            'End If
        End If
    End Sub

#End Region

#Region "User Function"

    'Public Sub BindClient()
    '    If cbodept.SelectedIndex <> 0 Then
    '        Dim cmdst As New SqlCommand("select *  from idmsclient where deptid='" & cbodept.Text & "'", connection)
    '        Dim dsst As New DataSet
    '        Dim adpst As New SqlDataAdapter
    '        adpst.SelectCommand = cmdst
    '        connection.Open()
    '        Dim cntr = adpst.Fill(dsst)
    '        connection.Close()
    '        cboclient.DataSource = dsst
    '        cboclient.DataValueField = "autoid"
    '        cboclient.DataTextField = "clientname"
    '        cboclient.DataBind()
    '        cboclient.Items.Insert(0, "--Select--")
    '    End If
    'End Sub

    'Public Sub BindLOB()
    '    If cboclient.SelectedIndex <> 0 Then
    '        Dim cmdst1 As New SqlCommand("select *  from warslobmaster where DeptId='" & cbodept.Text & "' and ClientId='" & cboclient.Text & "'", connection)
    '        Dim dsst1 As New DataSet
    '        Dim adpst1 As New SqlDataAdapter
    '        adpst1.SelectCommand = cmdst1
    '        connection.Open()
    '        Dim cntr = adpst1.Fill(dsst1)
    '        connection.Close()
    '        cbolob.DataSource = dsst1
    '        cbolob.DataValueField = "autoid"
    '        cbolob.DataTextField = "LOBname"
    '        cbolob.DataBind()
    '        cbolob.Items.Insert(0, "--Select--")
    '    End If
    'End Sub

    'Public Function ddldept_bind()
    '    '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '    Try
    '        Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
    '        Dim da As New SqlDataAdapter
    '        da.SelectCommand = comdepart
    '        Dim ds As New DataSet
    '        connection.Open()
    '        da.Fill(ds)
    '        connection.Close()
    '        cbodept.DataTextField = "DepartmentName"
    '        cbodept.DataValueField = "autoid"
    '        cbodept.DataSource = ds
    '        cbodept.DataBind()
    '        cbodept.Items.Insert("0", "--Select--")
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    'End Function
    Public Sub callvalue()
        Dim script1 As String = " <script langage='javascript'>" _
                                         + " FillText();" _
                                         + " </script>"
        ClientScript.RegisterStartupScript(Page.GetType(), "Startup", script1)
    End Sub
    Public Sub winclose()
        'Dim close As New System.Text.StringBuilder
        'close.Append("<Script language='javascript'>")
        'close.Append("winclose();")
        'close.Append("</Script>")
        'ClientScript.RegisterStartupScript(Page.GetType(), "winclose();", close.ToString)

        Dim script As String = " <script langage='javascript'>" _
                                 + " winclose();" _
                                 + " </script>"
        ClientScript.RegisterStartupScript(Page.GetType(), "close", script)
    End Sub

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1
    End Function
#End Region

#Region " Control Event"

    Private Sub cmdyes_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdyes.Click
        Try
            Dim fp As StreamWriter
            path = "UsersSpace/" & Session("Username") & "/" & txthidden.Text & ".qry"
            fp = File.CreateText(Server.MapPath(path))
            fp.WriteLine(txttb.Text)
            fp.Close()
            panconfirm.Visible = False

            'Updating data of existing file

            Dim objcmd As New SqlCommand("update warsquerymaster set qshowfield= '" & txtshowfield.Text & "' ,qtablename= '" & txttbname.Text & "' ,qfilepath='" & path & "',qcriteria='" & Replace(txtCriteria.Text, "'", "`") & "',Percentage='" & Trim(Request("chkPercentage")) & "',Formulaname='" & Trim(Request("hidFormulaName")) & "' where qfilename='" & txthidden.Text & "'", connection)
            connection.Open()
            objcmd.ExecuteNonQuery()
            connection.Close()
            objcmd.Dispose()
            Response.Redirect("welcome.aspx")
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            ShowConfirm(strmsg)
        End Try
    End Sub

    Private Sub cmdno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdno.Click
        panconfirm.Visible = False
        Response.Redirect("welcome.aspx")
    End Sub

    Protected Sub Button1_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.ServerClick
        If Session("mailtain") <> "" Then
            Dim recid As String = ""

            If LCase(owner.Value) <> LCase(Session("userid")) Then
                Dim cmdne As New SqlDataAdapter("select recordid from WARSQueryMaster where queryname='" + q.Value + "' and departmentid='" + d.Value + "' and clientid='" + c.Value + "' and lobyname='" + l.Value + "'", connection)
                Dim dsss As New DataSet
                cmdne.Fill(dsss)
                recid = dsss.Tables(0).Rows(0)(0).ToString
                cmdne.Dispose()
                dsss.Clear()
                cmdne = New SqlDataAdapter("select saveas from warsquerysharerights  where recid='" + recid + "'", connection)

                dsss = New DataSet
                cmdne.Fill(dsss)
                If dsss.Tables(0).Rows.Count > 0 Then


                    Dim t As String = dsss.Tables(0).Rows(0)(0).ToString
                    If t = "True" Then

                    Else
                        ShowConfirm("You donot have save as right!!")
                        Exit Sub
                    End If
                Else
                    ShowConfirm("You donot have save as right!!")
                    Exit Sub
                End If

            End If
        End If
        strsub1 = Session("heading")

        Session("mailtain") = ""
        'If cbodept.Text = "--Select--" Then
        '    ShowConfirm("Select atleast one department to save the report")
        '    Exit Sub
        'End If
        Dim statuschk As String = ""
        'If chkstatus.Checked = True Then
        '    statuschk = "Local"
        'Else
        '    statuschk = "NonLocal"
        'End If

        '***************************************end******************************************************************
        Dim client As String = ""
        Dim lob As String = ""
        'If (Trim(Request("type")) = "new") Then

        '    If (Client1.Text = "--Select--" Or Client1.Text = "" Or Client1.Text = "0") Then
        '        client = 0
        '    Else
        '        client = Client1.Text
        '    End If
        '    If (LOB1.Text = "--Select--" Or LOB1.Text = "" Or LOB1.Text = "0") Then
        '        lob = 0
        '    Else
        '        lob = LOB1.Text
        '    End If

        'ElseIf (Trim(Request("type")) = "edit") Then
        'If Trim(Request("client")) = "" Or Trim(Request("client")) = "0" Or Trim(Request("client")) = "--Select--" Then
        'If (cboclient.Text = "--Select--" Or cboclient.Text = "" Or cboclient.Text = "0") Then
        '    client = 0
        'Else
        '    client = cboclient.Text

        'End If
        'If (cbolob.Text = "--Select--" Or cbolob.Text = "" Or cbolob.Text = "0") Then
        '    'If Trim(Request("lob")) = "" Or Trim(Request("lob")) = "0" Or Trim(Request("lob")) = "--Select--" Then
        '    lob = 0
        'Else
        '    lob = cbolob.Text
        'End If

        'End If
        Dim cmdsave As New SqlCommand("select queryname from WARSQueryMaster where tablename='" & Trim(Request("hidtablename")) & "' and savedby='" & Session("userid") & "' and queryname='" & Trim(Request("sfilename")) & "'", connectionsave)
        Dim rdrsave As SqlDataReader
        connectionsave.Open()
        rdrsave = cmdsave.ExecuteReader
        If rdrsave.Read Then
            ShowConfirm("This Query Name Already Exists. So Please Give Another Query Name !!")
            Exit Sub
        Else
            Session("mailtain") = statuschk
            Session("mailtain") = Session("mailtain") + "," + client.ToString + "," + lob.ToString + "," + sfilename.Text
            Dim objcmd As New SqlCommand("insert_WARSQueryMaster", connection)
            objcmd.CommandType = CommandType.StoredProcedure
            With objcmd.Parameters
                .AddWithValue("@tableName", hidtablename.Value)
                .AddWithValue("@wheredata", WhereData.Value)
                .AddWithValue("@wheredata1", WhereData1.Value)
                .AddWithValue("@showData", sdata.Value)
                .AddWithValue("@crdata ", crdata.Value)
                .AddWithValue("@colName", Column.Value)
                .AddWithValue("@selected_field", "")
                .AddWithValue("@formula", txtformula.Value)
                .AddWithValue("@queryName", sfilename.Text)
                .AddWithValue("@savedBy", Session("userid").ToString())
                .AddWithValue("@createDate", System.DateTime.Now)
                .AddWithValue("@sharedwith", Session("userid"))
                .AddWithValue("@Percentage", chkper.Value)
                .AddWithValue("@Formulaname", hidforname.Value)

            End With
            connection.Open()
            objcmd.ExecuteNonQuery()
            '*************************************************changes made by vini********************************************************************
            '************************inserting subheading into database********************************************************************
            Dim cmdsave1 As New SqlCommand("select queryname from WARSQueryMaster where tablename='" & Trim(Request("hidtablename")) & "' and savedby='" & Session("userid") & "' and queryname='" & Trim(Request("sfilename")) & "'", connectionsave1)
            Dim rdrsave1 As SqlDataReader
            connectionsave1.Open()
            rdrsave1 = cmdsave1.ExecuteReader
            If rdrsave1.Read Then
                If sfilename.Text = rdrsave1("queryname") Then
                    Dim cmdsave2 As New SqlCommand("insert into subtotal(queryName,subheading) values('" & sfilename.Text & "','" & strsub1 & "')", connectionsave3)
                    connectionsave3.Open()
                    Dim da As New SqlDataAdapter
                    da.SelectCommand = cmdsave2

                    cmdsave2.ExecuteNonQuery()
                    connectionsave3.Close()

                    cmdsave2.Dispose()
                End If
            End If
            connectionsave1.Close()
            rdrsave1.Close()

            '************************************************************************************************************
            connection.Close()
            Session("Filename") = Request("sfilename")



            ''''Dim objcmd As New SqlCommand("update warsquerymaster set sharedwith = '" & strusername & "' where DepartmentId='" & cbodept.SelectedValue & "' and ClientId='" & client & "' and lobyname='" & lob & "' and queryname='" & ddlQueryName.SelectedValue & "'", connection1)
            '''''Response.Write("update warsquerymaster set sharedwith = '" & strusername & "' where tablename='" & ddlTableName.SelectedValue & "' and queryname='" & ddlQueryName.SelectedValue & "'")
            '''''Response.End()
            ''''connection1.Open()
            ''''objcmd.ExecuteNonQuery()
            ''''connection1.Close()
            ''''objcmd.Dispose()
        End If
        connectionsave.Close()
        rdrsave.Close()
        cmdsave.Dispose()
        '''''''''''''''Usertype check for track goes here:- By Suvidha

        'Dim cmm As New SqlCommand("insert into Querybuilder_utype select MAX(Auto_id)," + Session("usertype") + " from Track_QueryBuilder where Query_Name='" + sfilename.Text + "' and Action='Save'", connection)
        'connection.Open()
        'cmm.ExecuteNonQuery()
        'connection.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha
        ShowConfirm("CrossTab has been saved successfully!")
        winclose()
    End Sub

    Protected Sub cmdclose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdclose.Click
        'sfilename.Text = ""
        winclose()
    End Sub

    Protected Sub Button2_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.ServerClick
        If Session("mailtain") <> "" Then
            Dim recid As String = ""

            If LCase(owner.Value) <> LCase(Session("userid")) Then
                Dim cmdne As New SqlDataAdapter("select recordid from WARSQueryMaster where queryname='" + q.Value + "' and departmentid='" + d.Value + "' and clientid='" + c.Value + "' and lobyname='" + l.Value + "'", connection)
                Dim dsss As New DataSet
                cmdne.Fill(dsss)
                recid = dsss.Tables(0).Rows(0)(0).ToString
                cmdne.Dispose()
                dsss.Clear()
                cmdne = New SqlDataAdapter("select edit from warsquerysharerights  where recid='" + recid + "'", connection)

                dsss = New DataSet
                cmdne.Fill(dsss)
                If dsss.Tables(0).Rows.Count > 0 Then


                    Dim t As String = dsss.Tables(0).Rows(0)(0).ToString
                    If t = "True" Then

                    Else
                        ShowConfirm("You donot have Edit right!!")
                        Exit Sub
                    End If
                Else
                    ShowConfirm("You donot have Edit right!!")
                    Exit Sub
                End If
            End If
        End If
        Session("mailtain") = ""
        Dim statuschk As String = ""

        Dim str As String
        Dim client
        'If Trim(Request("client")) = "" Or Trim(Request("client")) = "0" Or Trim(Request("client")) = "--Select--" Then
        '    client = 0
        'Else
        '    client = cboclient.Text
        'End If
        Dim lob
        'If Trim(Request("lob")) = "" Or Trim(Request("lob")) = "0" Or Trim(Request("lob")) = "--Select--" Then
        '    lob = 0
        'Else
        '    lob = cbolob.Text
        'End If
        
        Dim cmd As New SqlDataAdapter("select  * from WARSQueryMaster where tablename='" & Trim(Request("hidtablename")) & "' and savedby='" & Session("userid") & "' and queryname='" & Trim(Request("sfilename")) & "'", connection)
        Dim setdata As New DataSet
        cmd.Fill(setdata)
        If setdata.Tables(0).Rows.Count > 0 Then

        Else
            ShowConfirm("There is no such CrossTab has been found!")
            Exit Sub
        End If
        Session("mailtain") = statuschk
        Session("mailtain") = Session("mailtain") + "," + client.ToString + "," + lob.ToString + "," + q.Value

        str = Session("userid").ToString
        Dim dt As String = ""
        dt = System.DateTime.Now.ToString
        Dim updatecmd As New SqlCommand("Update WARSQueryMaster set wheredata=@wheredata,wheredata1= '" & WhereData1.Value & "' , showData= '" & sdata.Value & "', colName='" & Column.Value & "', selected_field='', formula='" & txtformula.Value & "',  createDate='" & dt & "',  sharedwith='" & str & "', Percentage='" & chkper.Value & "', crdata='" & crdata.Value & "', Formulaname='" & hidforname.Value & "',status='" + statuschk + "' where tablename='" & Trim(Request("hidtablename")) & "' and savedby='" & Session("userid") & "' and queryname='" & Trim(Request("sfilename")) & "'", connection)
        updatecmd.Parameters.AddWithValue("@wheredata", WhereData.Value)
        connection.Open()
        updatecmd.ExecuteNonQuery()
        connection.Close()
        updatecmd.Dispose()
        '''''''''''''''Usertype check for track goes here:- By Suvidha
        Dim objQB As New QueryBuilder()
        Dim str1 = objQB.trackQ_BuilderForMaster(Session("userid"), "Update", "Query", Trim(Request("sfilename")), Trim(Request("hidtablename")))
        Dim cmm As New SqlCommand("insert into Querybuilder_utype select MAX(Auto_id)," + Session("usertype") + " from Track_QueryBuilder where Query_Name='" & Trim(Request("sfilename")) + "' and Action='Update'", connection)
        connection.Open()
        cmm.ExecuteNonQuery()
        connection.Close()
        '''''''''''''''Usertype check for track goes here:- By Suvidha
        ShowConfirm("CrossTab has been updated successfully!")
        winclose()
    End Sub
    Public Sub status(ByVal dept As String, ByVal clt As String, ByVal lob As String)
        If Session("typeofuser") = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                .AddWithValue("@Deptid", dept)
                .AddWithValue("@Clientid", clt)
                .AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            connection.Open()
            readerdata = cmdupdate.ExecuteReader

            readerdata.Close()
            connection.Close()
        ElseIf Session("typeofuser") = "User" Then
            Dim repobj As New ReportDesigner
            Dim SCOPE As String = repobj.chkUserscope(Session("userid"))
        End If

    End Sub
    'Protected Sub cbodept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbodept.SelectedIndexChanged
    '    If cbodept.SelectedIndex = 0 Then
    '        cboclient.Items.Clear()
    '        cbolob.Items.Clear()
    '        status("0", "0", "0")
    '        Exit Sub
    '    End If


    '    Try
    '        status(cbodept.SelectedValue, "0", "0")
    '        Dim cmdst As New SqlCommand("select clientname,autoid  from idmsclient where deptid='" & cbodept.SelectedValue & "'", connection)
    '        Dim dsst As New DataSet
    '        Dim adpst As New SqlDataAdapter
    '        adpst.SelectCommand = cmdst
    '        connection.Open()
    '        Dim cntr = adpst.Fill(dsst)
    '        connection.Close()
    '        cboclient.DataSource = dsst
    '        cboclient.DataTextField = "clientname"
    '        cboclient.DataValueField = "autoid"
    '        cboclient.DataBind()
    '        cboclient.Items.Insert("0", "--Select--")
    '        'Me.cbolob.Items.Insert("0", "")
    '        'Me.cbolob.SelectedValue = ""
    '        If Me.cbolob.SelectedValue <> "" Then
    '            For i = 0 To cbolob.Items.Count - 1
    '                cbolob.Items.Clear()
    '            Next
    '        End If
    '        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(strmsg)
    '    End Try
    '    ' End If
    'End Sub

    'Protected Sub cboclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboclient.SelectedIndexChanged

    '    If cbodept.SelectedIndex = 0 Then
    '        ShowConfirm("Please select department")
    '        status("0", "0", "0")
    '    ElseIf cboclient.SelectedIndex = 0 Then
    '        cbolob.Items.Clear()
    '        status(cbodept.SelectedValue, "0", "0")
    '    Else
    '        Try
    '            status(cbodept.SelectedValue, cboclient.SelectedValue, "0")
    '            Dim cmdst As New SqlCommand("select LOBName,Autoid  from warslobmaster where DeptId='" & cbodept.SelectedValue & "' and ClientId='" & cboclient.SelectedValue & "'", connection)
    '            Dim dsst As New DataSet
    '            Dim adpst As New SqlDataAdapter
    '            adpst.SelectCommand = cmdst
    '            connection.Open()
    '            adpst.Fill(dsst)
    '            connection.Close()
    '            cbolob.DataSource = dsst
    '            cbolob.DataTextField = "LOBName"
    '            cbolob.DataValueField = "autoid"
    '            cbolob.DataBind()
    '            cbolob.Items.Insert("0", "--Select--")

    '            ''''''''''''''''''''''''''''

    '        Catch ex As Exception
    '            Dim strmsg As String
    '            strmsg = Replace(ex.Message.ToString, "'", "")
    '            strmsg = Replace(strmsg, vbCrLf, " ")
    '            ShowConfirm(strmsg)
    '        End Try

    '    End If
    'End Sub
#End Region


    'Protected Sub cbolob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbolob.SelectedIndexChanged
    '    If cbolob.SelectedIndex = 0 Then
    '        status(cbodept.SelectedValue, cboclient.SelectedValue, "0")
    '    Else
    '        status(cbodept.SelectedValue, cboclient.SelectedValue, cbolob.SelectedValue)
    '    End If

    'End Sub
End Class
