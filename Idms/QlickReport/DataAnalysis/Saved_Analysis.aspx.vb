Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Partial Class DataAnalysis_Saved_Analysis
    Inherits System.Web.UI.Page
    Dim strcon As String = AppSettings("connectionString")
    Dim connection As New SqlConnection(strcon)
    Dim obj As New SavedAnalysis
    Dim cmd As New SqlCommand
    Dim readquery As SqlDataReader
    ''' <summary>
    ''' to show message
    ''' </summary>
    ''' <param name="message"></param>
    ''' <remarks></remarks>
    Public Sub aspnet_msgbox(ByVal message As String)
        System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE =""javascript"">" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("alert(""" & message & """)" & vbCrLf)
        System.Web.HttpContext.Current.Response.Write("</SCRIPT>")
    End Sub
    ''' <summary>
    ''' to fill departments
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Session("repname") = "report name"
        'Session("Queryname") = "query"
        'Session("colsvalue") = "processed columns"
        'Session("filtercolumns") = "all columns"
        If Session("analysis") <> "" Then


            cmd = New SqlCommand("select savedby from SavedAnalysis where savedby='" + Session("userid") + "' and analysisname='" + Session("table") + "'", connection)
            connection.Open()
            readquery = cmd.ExecuteReader
            If readquery.HasRows Then
                Button4.Enabled = True
            Else
                Button4.Enabled = False
                Errormsg.Text = "You don't have rights to update this analysis"


            End If
            connection.Close()
            readquery.Close()
        End If
        Dim repobj As New ReportDesigner


        Dim dept, lob, clt As String
        'If DropDownclient.SelectedIndex = 0 Or DropDownclient.SelectedIndex = -1 Then
        '    clt = "0"
        'Else
        '    clt = DropDownclient.SelectedValue
        'End If
        'If DropDownlob.SelectedIndex = 0 Or DropDownlob.SelectedIndex = -1 Then
        '    lob = "0"
        'Else
        '    lob = DropDownlob.SelectedValue
        'End If
        'If DropDowndept.SelectedIndex = 0 Or DropDowndept.SelectedIndex = -1 Then
        '    dept = "0"
        'Else
        '    dept = DropDowndept.SelectedValue
        'End If
        'If Session("typeofuser") = "Admin" Then
        '    Dim cmdupdate As New SqlCommand("Admin_Span_Check", connection)
        '    cmdupdate.CommandType = CommandType.StoredProcedure
        '    With cmdupdate.Parameters
        '        .AddWithValue("@userid", Session("userid"))
        '        .AddWithValue("@Deptid", dept)
        '        .AddWithValue("@Clientid", clt)
        '        .AddWithValue("@LOBID", lob)
        '    End With
        '    Dim readerdata As SqlDataReader
        '    connection.Open()
        '    readerdata = cmdupdate.ExecuteReader

        '    If readerdata.HasRows Then
        '        Status.Enabled = True
        '    Else

        '        Status.Checked = False
        '        Status.Enabled = False
        '    End If

        '    readerdata.Close()
        '    connection.Close()
        'ElseIf Session("typeofuser") = "User" Then
        '    Dim SCOPE As String = repobj.chkUserscope(Session("userid"), dept, clt, lob)
        '    If SCOPE = "Local" Then
        '        Status.Enabled = True
        '    Else
        '        Status.Checked = False
        '        Status.Enabled = False
        '    End If
        'End If
        Errormsg.Text = ""
        If Page.IsPostBack = False Then
            'Dim lblThispage As Label = Master.FindControl("lblPage")
            'lblThispage.Text = "Data Analysis"
            Dim classobj As New Functions
            'DropDowndept.DataTextField = "departmentname"
            'DropDowndept.DataValueField = "DeptID"

            'DropDowndept.DataSource = classobj.bind_Dept()
            'DropDowndept.DataBind()
            'DropDowndept.Items.Insert(0, "---select---")


        End If
        If Session("analysis") <> "" Then
            Button4.Text = "Update"
            'DropDowndept.Enabled = False
            'Status.Enabled = False
            'DropDownclient.Enabled = False
            'DropDownlob.Enabled = False
            textreportname.Enabled = False
        Else
            Button4.Text = "Save"
            'DropDowndept.Enabled = True
            'Status.Enabled = True
            'DropDownclient.Enabled = True
            'DropDownlob.Enabled = True
            textreportname.Enabled = True
        End If
    End Sub
    ''' <summary>
    ''' to save analysis
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Try

        
            If Button4.Text = "Save" Then


                'If DropDowndept.SelectedItem.Text = "---select---" Then
                '    Errormsg.Text = "Select Department First"
                '    Exit Sub
                'ElseIf textreportname.Text = "" Then
                '    Errormsg.Text = "Fill Analysis Name"
                '    Exit Sub

                'End If
                If textreportname.Text.Contains("(") Or textreportname.Text.Contains(")") Or textreportname.Text.Contains("~") Or textreportname.Text.Contains("`") Or textreportname.Text.Contains("$") Or textreportname.Text.Contains("#") Or textreportname.Text.Contains("@") Or textreportname.Text.Contains("!") Or textreportname.Text.Contains("%") Or textreportname.Text.Contains("^") Or textreportname.Text.Contains("&") Or textreportname.Text.Contains("*") Or textreportname.Text.Contains("-") Or textreportname.Text.Contains("+") Or textreportname.Text.Contains(":") Or textreportname.Text.Contains(";") Or textreportname.Text.Contains(",") Or textreportname.Text.Contains(".") Or textreportname.Text.Contains("/") Or textreportname.Text.Contains("?") Or textreportname.Text.Contains("|") Or textreportname.Text.Contains("\") Or textreportname.Text.Contains("{") Or textreportname.Text.Contains("}") Or textreportname.Text.Contains("[") Or textreportname.Text.Contains("]") Or textreportname.Text.Contains(" ") Or textreportname.Text.Contains("<") Or textreportname.Text.Contains(">") Then
                    'aspnet_msgbox("No Special Symbol Is Allowed")
                    Errormsg.Text = "No Special Symbol Is Allowed"
                    Exit Sub
                ElseIf textreportname.Text.StartsWith("1") Or textreportname.Text.StartsWith("2") Or textreportname.Text.StartsWith("3") Or textreportname.Text.StartsWith("4") Or textreportname.Text.StartsWith("5") Or textreportname.Text.StartsWith("6") Or textreportname.Text.StartsWith("7") Or textreportname.Text.StartsWith("8") Or textreportname.Text.StartsWith("9") Or textreportname.Text.StartsWith("0") Then
                    Errormsg.Text = "Analysis Name Cannot Starts With Numeric Value"
                    Exit Sub
                End If

                If Session("saveanalysis") = "" Then
                    'aspnet_msgbox("No Analysis Has Been Performed")
                    Errormsg.Text = "No Analysis Has Been Performed"
                    Exit Sub
                    If Session("analysis") <> "" Then


                    ElseIf textreportname.Text = "" Then
                        'aspnet_msgbox("Fill Analysis Name")
                        Errormsg.Text = "Fill Analysis Name"
                        Exit Sub
                    End If
                End If
            Else
                If Session("saveanalysis") = "" Then
                    'aspnet_msgbox("No Analysis Has Been Performed")
                    Errormsg.Text = "No Analysis Has Been Performed"
                    Exit Sub
                End If
            End If
            Dim status As String = ""
            'If Me.Status.Checked = True Then
            '    status = "Local"
            'Else
            '    status = "Non Local"
            'End If
            Dim reportcolumns = Session("filtercolumns")
            Dim allrepcolumn As String = ""
            Dim int As Integer = UBound(reportcolumns)
            Dim start As Integer = 0
            For start = 0 To int
                If allrepcolumn = "" Then
                    allrepcolumn = reportcolumns(start)
                Else
                    allrepcolumn = allrepcolumn & "," & reportcolumns(start)
                End If
            Next
            Dim processedcolumns = Session("colsvalue")
            Dim allprocessedcolumns As String = ""
            Dim intnew As Integer = UBound(processedcolumns)
            Dim starts As Integer = 0
            For starts = 0 To intnew
                If allprocessedcolumns = "" Then
                    allprocessedcolumns = processedcolumns(starts)
                Else
                    allprocessedcolumns = allprocessedcolumns & "," & processedcolumns(starts)
                End If
            Next
            Dim queryarray = Session("dataanalysissaveanalysis").split("$")
            Dim queryname As String = queryarray(0)
            Dim reportname As String = Session("repname").ToString
            If Button4.Text = "Save" Then
                'Dim valuecum As String = obj.insert_analysis(DropDowndept.SelectedItem.Text, DropDowndept.SelectedValue, hidclientname.Value, hidclientid.Value, hidlobname.Value, hidlobid.Value, reportname, queryname, textreportname.Text, allrepcolumn, allprocessedcolumns, textreportname.Text, Session("saveanalysis"), Session("userid"), status)

                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where analysisname='" + textreportname.Text + "' and Action='Save' and Entity='Analysis'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                'If valuecum = "1" Then
                '    Errormsg.Text = "Analysis Has Been Saved Successfully"
                '    'aspnet_msgbox("Analysis Has Been Saved Successfully")
                '    Exit Sub
                'End If
                'If valuecum = "2" Then
                '    Errormsg.Text = "Choose Another Name For Analysis"
                '    'aspnet_msgbox("Choose Another Name For Analysis")
                '    Exit Sub
                'End If
            Else

                Dim valuecum As String = obj.update_analysis(Session("table"), allprocessedcolumns, allrepcolumn, Session("saveanalysis"))
                ''''''''''''''''''suvidha
                '''''''''''track shifted here
                cmd = New SqlCommand("Sp_LogDataAnalysisForAnalysisUpdate", connection)
                cmd.CommandType = CommandType.StoredProcedure

                cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 50)
                cmd.Parameters("@UserID").Value = Session("userid")
                cmd.Parameters.Add("@analysisname", SqlDbType.VarChar, 50)
                cmd.Parameters("@analysisname").Value = Session("table")
                connection.Open()
                cmd.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha

                Dim cmm As New SqlCommand("insert into DataAnalysis_utype select MAX(Autoid)," + Session("usertype") + " from logdataanalysis where analysisname='" + Session("table") + "' and Action='Update' and Entity='Analysis'", connection)
                connection.Open()
                cmm.ExecuteNonQuery()
                connection.Close()
                '''''''''''''''Usertype check for track goes here:- By Suvidha
                '''''''''''track shifted here
                '''''''''''''suvidha
                If valuecum = "1" Then
                    'aspnet_msgbox("Analysis Has Been Updated Successfully")
                    Errormsg.Text = "Analysis Has Been Updated Successfully"
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            Dim strmsg As String
            strmsg = Replace(ex.Message.ToString, "'", "")
            strmsg = Replace(strmsg, vbCrLf, " ")
            aspnet_msgbox(strmsg)
        End Try
    End Sub
    Public Sub statuslocal(ByVal dept As String, ByVal clt As String, ByVal lob As String)
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


            'If readerdata.HasRows Then
            '    'Status.Enabled = True
            'Else

            '    Status.Checked = False
            '    Status.Enabled = False
            'End If
            readerdata.Close()
            connection.Close()
        ElseIf Session("typeofuser") = "User" Then
            Dim repobj As New ReportDesigner
            Dim SCOPE As String = (repobj.chkUserscope(Session("userid"))).ToString
            '    If SCOPE = "Local" Then
            '        Status.Enabled = True
            '    Else
            '        Status.Checked = False
            '        Status.Enabled = False
            '    End If
        End If

    End Sub
    ''' <summary>
    '''  fill clients
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub DropDowndept_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDowndept.SelectedIndexChanged
    '    If DropDowndept.SelectedItem.Text = "---select---" Then
    '        statuslocal("0", "0", "0")
    '        hidclientid.Value = "0"
    '        hidlobid.Value = "0"
    '        hidclientname.Value = "0"
    '        hidlobname.Value = "0"

    '        DropDownclient.Items.Clear()
    '        DropDownlob.Items.Clear()
    '    Else
    '        Dim classobj As New Functions

    '        statuslocal(DropDowndept.SelectedValue, "0", "0")
    '        hidclientid.Value = "0"
    '        hidlobid.Value = "0"
    '        hidclientname.Value = "0"
    '        hidlobname.Value = "0"
    '        DropDownclient.DataTextField = "clientname"
    '        DropDownclient.DataValueField = "autoid"
    '        DropDownclient.DataSource = classobj.bind_client(DropDowndept.SelectedValue)

    '        DropDownclient.DataBind()
    '        DropDownclient.Items.Insert(0, "---select---")
    '        DropDownlob.Items.Clear()
    '    End If
    'End Sub
    ''' <summary>
    '''  fill lobs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub DropDownclient_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownclient.SelectedIndexChanged
    '    If DropDownclient.SelectedItem.Text = "---select---" Then
    '        hidclientid.Value = "0"
    '        hidlobid.Value = "0"
    '        hidclientname.Value = "0"
    '        hidlobname.Value = "0"
    '        statuslocal(DropDowndept.SelectedValue, "0", "0")
    '        DropDownlob.Items.Clear()
    '    Else
    '        statuslocal(DropDowndept.SelectedValue, DropDownclient.SelectedValue, "0")
    '        Dim classobj As New Functions

    '        DropDownlob.DataTextField = "lobname"
    '        DropDownlob.DataValueField = "autoid"
    '        DropDownlob.DataSource = classobj.bind_lob(DropDowndept.SelectedValue, DropDownclient.SelectedValue)

    '        DropDownlob.DataBind()
    '        DropDownlob.Items.Insert(0, "---select---")
    '        hidclientname.Value = DropDownclient.SelectedItem.Text
    '        hidclientid.Value = DropDownclient.SelectedValue
    '        hidlobid.Value = "0"
    '        hidlobname.Value = "0"
    '    End If
    'End Sub
    ''' <summary>
    ''' give lob id value to controls 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    'Protected Sub DropDownlob_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DropDownlob.SelectedIndexChanged
    '    If DropDownlob.SelectedItem.Text = "---select---" Then
    '        hidlobid.Value = "0"
    '        hidlobname.Value = "0"
    '        statuslocal(DropDowndept.SelectedValue, DropDownclient.SelectedValue, "0")
    '    Else
    '        hidlobname.Value = DropDownlob.SelectedItem.Text
    '        hidlobid.Value = DropDownlob.SelectedValue
    '        statuslocal(DropDowndept.SelectedValue, DropDownclient.SelectedValue, hidlobid.Value)
    '    End If

    'End Sub
End Class
