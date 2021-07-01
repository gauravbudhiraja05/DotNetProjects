Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.Data
Imports System.IO
Partial Class DataAnalysis_SaveFilterReport
    Inherits System.Web.UI.Page
    Dim conn1 As String = AppSettings("ConnectionString")

    Dim con As New SqlConnection(conn1)
    Dim readquery As SqlDataReader
    Dim p
    Dim cmd As New SqlCommand
    'Dim DEPART, lob, client As String
    ''' <summary>
    ''' fill departments
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load



        Dim dept, lob, clt As String
        If DropDownclient.SelectedIndex = 0 Or DropDownclient.SelectedIndex = -1 Then
            clt = "0"
        Else
            clt = DropDownclient.SelectedValue
        End If
        If DropDownlob.SelectedIndex = 0 Or DropDownlob.SelectedIndex = -1 Then
            lob = "0"
        Else
            lob = DropDownlob.SelectedValue
        End If
        If DropDowndept.SelectedIndex = 0 Or DropDowndept.SelectedIndex = -1 Then
            dept = "0"
        Else
            dept = DropDowndept.SelectedValue
        End If
        If Session("typeofuser") = "Admin" Then
            Dim cmdupdate As New SqlCommand("Admin_Span_Check", con)
            cmdupdate.CommandType = CommandType.StoredProcedure
            With cmdupdate.Parameters
                .AddWithValue("@userid", Session("userid"))
                .AddWithValue("@Deptid", dept)
                .AddWithValue("@Clientid", clt)
                .AddWithValue("@LOBID", lob)
            End With
            Dim readerdata As SqlDataReader
            con.Open()
            readerdata = cmdupdate.ExecuteReader

            If readerdata.HasRows Then
                chklocal.Enabled = True
            Else

                chklocal.Checked = False
                chklocal.Enabled = False
            End If


            readerdata.Close()
            con.Close()
        ElseIf Session("typeofuser") = "User" Then
            Dim repobj As New ReportDesigner
            Dim SCOPE As String = repobj.chkUserscope(Session("userid"))
            If SCOPE = "Local" Then
                chklocal.Enabled = True
            Else
                chklocal.Checked = False
                chklocal.Enabled = False
            End If
        End If

        If Page.IsPostBack = False Then


            'Dim lblThispage As Label = Master.FindControl("lblPage")
            'lblThispage.Text = "Data Analysis"

            Dim classobj As New Functions
            DropDowndept.DataTextField = "departmentname"
            DropDowndept.DataValueField = "DeptID"

            DropDowndept.DataSource = classobj.bind_Dept()
            DropDowndept.DataBind()
            DropDowndept.Items.Insert(0, "---select---")
        End If
    End Sub
    ''' <summary>
    '''  fill clients
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DropDowndept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDowndept.SelectedIndexChanged
        If DropDowndept.SelectedItem.Text = "---select---" Then


            DropDownclient.Items.Clear()
            DropDownlob.Items.Clear()
        Else
            Dim classobj As New Functions



            DropDownclient.DataTextField = "clientname"
            DropDownclient.DataValueField = "autoid"
            DropDownclient.DataSource = classobj.bind_client(DropDowndept.SelectedValue)
            DEPART.Value = DropDowndept.SelectedItem.Text
            client.Value = "0"
            lob.Value = "0"
            DropDownclient.DataBind()
            DropDownclient.Items.Insert(0, "---select---")
            DropDownlob.Items.Clear()
        End If
    End Sub
    ''' <summary>
    ''' fill lobs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DropDownclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownclient.SelectedIndexChanged
        If DropDownclient.SelectedItem.Text = "---select---" Then


            DropDownlob.Items.Clear()
        Else
            Dim classobj As New Functions

            DropDownlob.DataTextField = "lobname"
            DropDownlob.DataValueField = "autoid"
            DropDownlob.DataSource = classobj.bind_lob(DropDowndept.SelectedValue, DropDownclient.SelectedValue)
            client.Value = DropDownclient.SelectedItem.Text
            lob.Value = "0"
            DropDownlob.DataBind()
            DropDownlob.Items.Insert(0, "---select---")
        End If
    End Sub
    ''' <summary>
    ''' give vaule of lob to control
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DropDownlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownlob.SelectedIndexChanged
        If DropDownlob.SelectedItem.Text = "---select---" Then


            lob.Value = DropDownlob.SelectedItem.Text
        End If
    End Sub
    ''' <summary>
    ''' show message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' save the filtered report
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        If DropDowndept.SelectedItem.Text = "---select---" Then
            aspnet_msgbox("Select Department")
            Exit Sub
        End If
        Dim repname As String = ""
        cmd = New SqlCommand("select ReportName as ReportName from dataanalysishtmlreport", con)
        con.Open()
        readquery = cmd.ExecuteReader
        While readquery.Read()

            repname = readquery("ReportName")
            If repname = textreportname.Text Then
                aspnet_msgbox("Choose Another Report Name")
                textreportname.Focus()
                Exit Sub
            End If

        End While
        readquery.Close()
        con.Close()

        If textreportname.Text = "" Then
            aspnet_msgbox("Fill Report Name")
            textreportname.Focus()
            Exit Sub
        End If
        'strdivreport.Value = report.InnerHtml.ToString
        If Session("div1") <> "" Then
            'txtstrdiv.Value = strpretags & txtstrdiv.Value & strposttags
            Dim str As String = ""
            Dim fp As StreamWriter
            If Not Directory.Exists(Server.MapPath("html/")) Then
                '<----------------------Creating Directory for partcular user--------------------------------->
                Directory.CreateDirectory(Server.MapPath("html/"))
                '<----------------------End of Creating Directory for partcular user------------------------>
            End If
            '<------------------------End of Creating A main Directory--------------------------------------->
            Dim Path = "html/" & textreportname.Text & ".html"
            '<--------------------Creating a new text file---------------------------------->
            fp = File.CreateText(Server.MapPath(Path))
            'change
            fp.WriteLine(Session("div"))
            fp.WriteLine(Session("div1"))



            fp.Close()

            Dim deptval As String = ""
            Dim clientval As String = ""
            Dim lobval As String = ""
            deptval = DropDowndept.SelectedValue.ToString
            clientval = DropDownclient.SelectedValue.ToString
            lobval = DropDownlob.SelectedValue.ToString
            If DropDowndept.SelectedItem.Text = "---select---" Then
                aspnet_msgbox("Select Department First")
                Exit Sub
            End If

            If client.Value = "0" Then

                'DropDownclient.SelectedItem.Text = "0"
                clientval = "0"

                'DropDownlob.SelectedItem.Text = "0"
                lobval = "0"
            End If
            ''If DropDownlob.SelectedItem.Text = "---select---" Or DropDownlob.SelectedItem.Text = "0" Then
            If lobval = "0" Then
                'DropDownlob.SelectedItem.Text = "0"
                lobval = "0"
            End If
            Dim localstatus As String = ""
            If chklocal.Checked = True Then
                localstatus = "Local"
            Else
                localstatus = "NonLocal"
            End If
            cmd = New SqlCommand("insert into dataanalysishtmlreport values(@DeptName,@DeptId,@ClientName,@ClientId,@LobName,@LobId,@ReportName,@SavedBy,@repname,@local)", con)
            cmd.Parameters.Add("@DeptName", SqlDbType.VarChar, 50)
            cmd.Parameters("@DeptName").Value = DEPART.Value
            cmd.Parameters.Add("@DeptId", SqlDbType.VarChar, 50)
            cmd.Parameters("@DeptId").Value = deptval
            cmd.Parameters.Add("@ClientName", SqlDbType.VarChar, 50)
            cmd.Parameters("@ClientName").Value = client.Value
            cmd.Parameters.Add("@ClientId", SqlDbType.VarChar, 50)
            cmd.Parameters("@ClientId").Value = clientval
            cmd.Parameters.Add("@LobName", SqlDbType.VarChar, 50)
            cmd.Parameters("@LobName").Value = lob.Value
            cmd.Parameters.Add("@LobId", SqlDbType.VarChar, 50)
            cmd.Parameters("@LobId").Value = lobval
            cmd.Parameters.Add("@ReportName", SqlDbType.VarChar, 50)
            cmd.Parameters("@ReportName").Value = textreportname.Text
            cmd.Parameters.Add("@SavedBy", SqlDbType.VarChar, 50)
            cmd.Parameters("@SavedBy").Value = Session("userid")
            cmd.Parameters.Add("@repname", SqlDbType.VarChar, 50)
            cmd.Parameters("@repname").Value = Session("reportname1")
            cmd.Parameters.Add("@local", SqlDbType.VarChar, 50)
            cmd.Parameters("@local").Value = localstatus
            ', ,  
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            aspnet_msgbox("Report Has Been Saved Successfully")
            'divsavereport.Visible = False
        End If
        'If DropDownclient.SelectedItem.Text = "0" Then

        '    DropDownclient.SelectedItem.Text = "---select---"



        'End If
        'If DropDownlob.SelectedItem.Text = "0" Then
        '    DropDownlob.SelectedItem.Text = "---select---"

        'End If
    End Sub
End Class
