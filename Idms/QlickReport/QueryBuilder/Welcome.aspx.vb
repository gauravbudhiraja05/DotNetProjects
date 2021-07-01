Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports System.Data
Imports System.IO.StreamReader
Imports System.Web.UI
Partial Class QueryBuilder_Welcome
    Inherits System.Web.UI.Page

    Public datatablestring
    Public datafieldstring
    Public datafieldstringc
    Public datafieldstring1
    Public dataLOB
    Public abc
    Dim constr As String = AppSettings("ConnectionString")
    Dim connection As New SqlConnection(constr)
    Dim connection1 As New SqlConnection(constr)
    Dim connection2 As New SqlConnection(constr)
    Dim connectionsave As New SqlConnection(constr)
    Dim cmd As SqlCommand
    Dim rdr As SqlDataReader
    Dim strsqlshow
    Dim strsqlwhere
    Dim strsql
    Dim doct As IO.Directory

    Public strmonthbutton1
    Public strmonthbutton2
    Public strcurrent1
    Public strcurrent2

    Public Function ShowConfirm(ByVal strPassed As String)
        Dim Script As String
        Script = "<script language='javascript'>"
        Script = Script + "alert('" + strPassed + "')"
        Script = Script + ("</script>")
        ClientScript.RegisterStartupScript(Page.GetType(), "ShowConfirm", Script)
        Return 1

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ajax.Utility.RegisterTypeForAjax(GetType(QueryBuilder))
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxClass))
        Ajax.Utility.RegisterTypeForAjax(GetType(AjaxSearchBind))
        Ajax.Utility.RegisterTypeForAjax(GetType(ReportDesignerAjax))
        Ajax.Utility.RegisterTypeForAjax(GetType(queryBuilderAjax))
        connection.Open()
        cmd = New SqlCommand("select Database1 from InternetProductDemo where UserID='" + Session("userid") + "' ", connection)
        rdr = cmd.ExecuteReader
        If rdr.Read Then
            Dim producttype As String = rdr("Database1")
            If (producttype = "Excel") Then
                uploadtable.Visible = True
            Else
                uploadtable.Visible = False
            End If

        End If
        connection.Close()
        'Me.ddlDept.Attributes.Add("onchange", "getclient();")
        'Me.ddlClient.Attributes.Add("onchange", "GetLOB();")
        'Me.ddlLob.Attributes.Add("onchange", "getdata();")
        Me.ddlTablename.Attributes.Add("onchange", "tabstatus();")
        Dim bodytag As HtmlGenericControl = Master.FindControl("Bodytag")
        ' bodytag.Attributes.Add("onmousemove", "setupDrag();")
        'bodytag.Attributes.Add("ondrag", "setupDrag();")
        'Try 
        Session("mailtain") = ""
        If Me.IsPostBack = False Then
            ddlTablename_bind()
            '    If Me.IsPostBack = True Then
            '******************Fill department*****************************
            'Try
            '    Session("count") = 0
            '    Dim comdepart As New SqlCommand("select * from idmsdepartment", connection)
            '    Dim da As New SqlDataAdapter
            '    da.SelectCommand = comdepart
            '    Dim ds1 As New Data.DataSet
            '    connection.Open()
            '    da.Fill(ds1)
            '    connection.Close()
            '    ddlDept.DataTextField = "DepartmentName"
            '    ddlDept.DataValueField = "autoid"
            '    ddlDept.DataSource = ds1
            '    ddlDept.DataBind()
            '    ddlDept.Items.Insert("0", "--Select--")
            'Catch ex As Exception
            '    Dim strmsg As String
            '    strmsg = Replace(ex.Message.ToString, "'", "")
            '    strmsg = Replace(strmsg, vbCrLf, " ")
            '    ShowConfirm(ex.ToString)
            'Finally
            '    connection.Close()
            'End Try

            '***************************************************************

        End If
        'datatablestring = "<option value=''>Select</option>"
        'Dim tablename As String
        'Dim tableid As Integer
        'Dim rdrModules As SqlDataReader
        'Dim rdrPrgms As SqlDataReader
        'Dim rdLOB As SqlDataReader
        'Dim strQryPrgms As String = ""
        'Dim cmdPrgms As New SqlCommand
        'Dim strSQL As String
        'Dim strUser As String = Session("UserName")
        'Select Case Session("typeofuser")
        '    Case "Member"
        '        Dim cmdLob As New SqlCommand("select lobdept from Registration where userid='" + Session("userid") + "'", connection)
        '        Dim rdrlob As SqlDataReader
        '        Dim strLobId As String
        '        connection.Open()
        '        rdrlob = cmdLob.ExecuteReader
        '        While rdrlob.Read
        '            strLobId = rdrlob("Lobdept")
        '        End While
        '        rdrlob.Close()
        '        cmdLob.Dispose()
        '        connection.Close()
        '        strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster where autoid in(" & strLobId & ") order by LOB"
        '    Case "Admin"
        '        'strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster where autoid in(select lobid from warslobadmin  where adminid ='" + Session("userid") + "' group by lobid, adminid ) order by LOB"
        '        strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster order by LOB"
        '    Case "Superadmin"
        '        'strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster order by LOB"
        'End Select

        ''strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster order by LOB" 'LOB
        ''strSQL = "select distinct(LOBName) LOB, AutoId from WarsLOBMaster where autoid in(select lobid from warslobadmin  where adminid ='" + +"' group by lobid, adminid ) order by LOB"  'LOB
        'Dim objSQLC As New SqlCommand(strSQL, connection)
        'connection.Open()
        'rdLOB = objSQLC.ExecuteReader
        'Dim strLOBName As String
        'Dim intLOBId As Integer
        'While rdLOB.Read
        '    intLOBId = rdLOB("AutoId")
        '    strLOBName = rdLOB("LOB")
        '    'If Trim(Request("cboLOB")) = Trim(intLOBId) Then
        '    '    dataLOB = dataLOB & "<option selected value='" & intLOBId & "'> " & strLOBName & " </option>"
        '    'Else
        '    '    dataLOB = dataLOB & "<option value='" & intLOBId & "'> " & strLOBName & " </option>"
        '    'End If
        'End While
        'connection.Close()
        ''Dim bool As Boolean
        ''Dim strQryMod As String
        ''If Trim(Request("DepartmentName")) <> "" And Trim(Request("DepartmentName")) <> "--Select--" And (Trim(Request("Clientname")) = "" Or Trim(Request("Clientname")) = "--Select--") And (Trim(Request("cboLOB")) = "" Or Trim(Request("cboLOB")) = "--Select--") Then
        ''    If Session("usertype") = "member" Then
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId=0 and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')"  'table in database
        ''    Else
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId=0 and LOBId=0"  'table in database
        ''    End If

        ''    bool = True
        ''ElseIf Trim(Request("DepartmentName")) <> "" And Trim(Request("DepartmentName")) <> "--Select--" And (Trim(Request("Clientname")) <> "" And Trim(Request("Clientname")) <> "--Select--") And (Trim(Request("cboLOB")) = "" Or Trim(Request("cboLOB")) = "--Select--") Then
        ''    If Session("usertype") = "member" Then
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId=0 and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')"  'table in database
        ''    Else
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId=0"  'table in database
        ''    End If

        ''    bool = True
        ''ElseIf Trim(Request("DepartmentName")) <> "" And Trim(Request("DepartmentName")) <> "--Select--" And (Trim(Request("Clientname")) <> "" And Trim(Request("Clientname")) <> "--Select--") And (Trim(Request("cboLOB")) <> "" And Trim(Request("cboLOB")) <> "--Select--") Then
        ''    If Session("usertype") = "member" Then
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId='" & Request("cboLOB") & "' and tablename in (select tablename from warslobtablerights where userid='" & Session("userid") & "')"  'table in database
        ''    Else
        ''        strQryMod = "select isnull(tablename,'') as tablename from warslobtablemaster where DepartmentId='" & Request("DepartmentName") & "' and ClientId='" & Request("Clientname") & "' and LOBId='" & Request("cboLOB") & "'"  'table in database
        ''    End If

        ''    bool = True
        ''End If
        ''Dim i As Integer = 0
        ''Dim str1 As String
        ''If bool = True Then
        ''    Dim cmdMod As New SqlCommand(strQryMod, connection)
        ''    connection.Open()
        ''    rdrModules = cmdMod.ExecuteReader
        ''    While rdrModules.Read
        ''        tablename = rdrModules("tablename")
        ''        datafieldstring = datafieldstring & "<tr><td colspan=2><INPUT Type=button onclick=javascript:OpenFields('" & tablename & "') value='" & tablename & "' class=button style='width:150px'></td></tr>"
        ''        If Trim(tablename) = Trim(Request("cbodatatable")) Then

        ''            strQryPrgms = "select visiblecolumn as str from warslobtablemaster where tablename = '" & Trim(tablename) & "'"
        ''            cmdPrgms = New SqlCommand(strQryPrgms, connection1)
        ''            connection1.Open()
        ''            rdrPrgms = cmdPrgms.ExecuteReader
        ''            While rdrPrgms.Read
        ''                str1 = rdrPrgms("str")
        ''                Dim tokens As String() = Split(str1, ",")
        ''                For i = 0 To tokens.Length - 1
        ''                    datafieldstring = datafieldstring & "<tr><td><A href='#'>" & Trim(tokens(i)) & "</a></td></tr>"
        ''                Next
        ''            End While
        ''            connection1.Close()

        ''        End If
        ''    End While
        ''    connection.Close()
        '***********************Get links of uploaded excel files*************************
        'Dim cmdgetexcelfile As New SqlCommand("select * from ExcelImportDetails where LOB='" & Request("cboLOB") & "'", connection)
        'Response.Write("select * from ExcelImportDetails where LOB='" & Request("cboLOB") & "'")
        'Response.End()
        'Dim drgetexcelfile As SqlDataReader
        'connection.Open()
        'drgetexcelfile = cmdgetexcelfile.ExecuteReader
        'While drgetexcelfile.Read
        'datafieldstring1 = datafieldstring1 & "<tr><td><INPUT type=button ID=cmdimport Class=button style=width:150px> </td></tr>"
        'datafieldstring1 = datafieldstring1 & "<tr><td><a href=/IDMS/Table_Management/" & drgetexcelfile("ExcelPath") & " style=width:150px;>" & drgetexcelfile("LinkName") & "</a></td></tr>"
        'End While
        'drgetexcelfile.Close()
        'connection.Close()
        'cmdgetexcelfile.Dispose()
        '*******************************Get buttons for roster view *******************
        '*******************************Current Month**********************************
        'Dim parentNode
        'Dim parentNodeNo = 0
        'Dim childnode
        'Dim curmonth = DatePart(DateInterval.Month, System.DateTime.Now)
        'Dim strcuryear = DatePart(DateInterval.Year, System.DateTime.Now)
        'Dim strprevdate = DateAdd(DateInterval.Month, -1, System.DateTime.Now)
        'Dim prevmonth = DatePart(DateInterval.Month, strprevdate)
        'Dim strprevyear = DatePart(DateInterval.Year, strprevdate)
        'Dim cntcur
        'Dim strbtn1
        'curmonth = CType(curmonth, String)
        'strcuryear = CType(strcuryear, String)
        'strcurrent1 = conMonth(curmonth, 0, strcuryear)
        'strmonthbutton1 = "<tr><td><INPUT type=button name=cmdcurbtn Class=button style=width:150px; value=" & conMonth(curmonth, 0, strcuryear) & " onclick=" & Chr(34) & "javascript:curwinopen('" + curmonth + "','" + strcuryear + "');" & Chr(34) & "> </td></tr>"
        ''**************************Tree View*****************************************
        ' ''parentNode = New Microsoft.Web.UI.WebControls.TreeNode
        ' ''parentNode.Text = "<font class=button face=verdana size=2><b>" + conMonth(curmonth, 0, strcuryear) + "</b></font>"
        ' ''parentNode.NavigateUrl = "/IDMS/Roster/RosterMatrix.aspx?strmon=" + curmonth + "&stryear=" + strcuryear
        ' ''parentNode.Target = "new"
        ' ''Menu.Nodes.Add(parentNode)
        ' ''parentNodeNo = parentNodeNo + 1
        ' ''For cntcur = 0 To getweek(curmonth, strcuryear)
        ' ''    childnode = New Microsoft.Web.UI.WebControls.TreeNode
        ' ''    childnode.Text = "<font class=button face=verdana size=2>" & "Week" & cntcur + 1 & "</font>"
        ' ''    childnode.NavigateUrl = "/IDMS/Roster/RosterMatrix.aspx?strmon=" & curmonth & "&stryear=" & strcuryear & "&strweek=" & cntcur + 1
        ' ''    childnode.Target = "new"
        ' ''    Menu.Nodes(parentNodeNo - 1).Nodes.Add(childnode)
        ' ''Next
        '****************************Previous Month*******************************
        ''Dim cntprev
        ''Dim strbtn2
        ''prevmonth = CType(prevmonth, String)
        ''strprevyear = CType(strprevyear, String)
        ''strcurrent2 = conMonth(prevmonth, 0, strprevyear)
        '''****************************Tree View*******************************
        ''parentNode = New Microsoft.Web.UI.WebControls.TreeNode
        ''parentNode.Text = "<font class=button face=verdana size=2><b>" + conMonth(prevmonth, 0, strprevyear) + "</b></font>"
        ''parentNode.NavigateUrl = "/IDMS/Roster/RosterMatrix.aspx?strmon=" + prevmonth + "&stryear=" + strprevyear
        ''parentNode.Target = "new"
        ''menu.Nodes.Add(parentNode)
        ''parentNodeNo = parentNodeNo + 1
        ''For cntprev = 0 To getweek(prevmonth, strprevyear)
        ''    'strbtn2 = strbtn2 & "<tr><td><INPUT type=button name=cmdprevweek Class=button style=width:100px; value=Week" & cntprev + 1 & " onclick=" & Chr(34) & "javascript:window.open('/IDMS/Roster/RosterMatrix.aspx?strmon=" & prevmonth & "&stryear=" & strprevyear & "&strweek=" & cntprev + 1 & "');" & Chr(34) & "> </td></tr>"
        ''    childnode = New Microsoft.Web.UI.WebControls.TreeNode
        ''    childnode.Text = "<font class=button face=verdana size=2>" & "Week" & cntprev + 1 & "</font>"
        ''    childnode.NavigateUrl = "/IDMS/Roster/RosterMatrix.aspx?strmon=" & prevmonth & "&stryear=" & strprevyear & "&strweek=" & cntprev + 1
        ''    childnode.Target = "new"
        ''    menu.Nodes(parentNodeNo - 1).Nodes.Add(childnode)
        ''Next
        'End If
        'Catch ex As Exception
        '    Dim strmsg As String
        '    strmsg = Replace(ex.Message.ToString, "'", "")
        '    strmsg = Replace(strmsg, vbCrLf, " ")
        '    ShowConfirm(strmsg)
        'End Try
    End Sub

    Public Sub ddlTablename_bind()
        '''''''''''''''''''''''filling of table dropdownlist'''''''''''''''''''''''''

        Dim query As String = "select tablename from warslobtablemaster where createdby='" + Session("userid") + "'"
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

    Public Function getweek(ByVal month, ByVal stryear)
        Dim st
        Dim se
        Dim ldate As Date
        Dim str2(6)
        Dim m
        st = CDate(month() & "/01" & "/" & stryear)
        se = CDate(month() & "/" & conMonth(month, 1, stryear) & "/" & stryear)
        ldate = st
        str2(0) = st
        m = 0
        Dim n
        Do While ldate < se
            ldate = DateAdd("d", 1, ldate)
            str2(m) = str2(m) & " -" & ldate
            n = m
            If DatePart("w", ldate) = 1 Then
                m = m + 1
            End If
        Loop
        Return n
    End Function

    Public Function conMonth(ByVal nummon, ByVal opt, ByVal year)
        Dim strmon(2) As String
        Select Case nummon
            Case 1
                strmon(0) = "January"
                strmon(1) = "31"
                Return strmon(opt)
            Case 2
                strmon(0) = "Feburary"
                If CInt(year()) Mod 4 = 0 Then
                    strmon(1) = "29"
                Else
                    strmon(1) = "28"
                End If
                Return strmon(opt)
            Case 3
                strmon(0) = "March"
                strmon(1) = "31"
                Return strmon(opt)
            Case 4
                strmon(0) = "April"
                strmon(1) = "30"
                Return strmon(opt)
            Case 5
                strmon(0) = "May"
                strmon(1) = "31"
                Return strmon(opt)
            Case 6
                strmon(0) = "June"
                strmon(1) = "30"
                Return strmon(opt)
            Case 7
                strmon(0) = "July"
                strmon(1) = "31"
                Return strmon(opt)
            Case 8
                strmon(0) = "August"
                strmon(1) = "31"
                Return strmon(opt)
            Case 9
                strmon(0) = "September"
                strmon(1) = "30"
                Return strmon(opt)
            Case 10
                strmon(0) = "October"
                strmon(1) = "31"
                Return strmon(opt)
            Case 11
                strmon(0) = "November"
                strmon(1) = "30"
                Return strmon(opt)
            Case 12
                strmon(0) = "December"
                strmon(1) = "31"
                Return strmon(opt)
        End Select
        Return ""
    End Function

    'Protected Sub DepartmentName_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DepartmentName.SelectedIndexChanged
    '    Try
    '        If DepartmentName.SelectedIndex <> 0 Then
    '            Dim cmdst As New SqlCommand("select *  from idmsclient where deptid='" & DepartmentName.SelectedValue & "'", connection)
    '            Dim dsst As New Data.DataSet
    '            Dim adpst As New SqlDataAdapter
    '            adpst.SelectCommand = cmdst
    '            connection.Open()
    '            Dim cntr = adpst.Fill(dsst)
    '            connection.Close()
    '            Clientname.DataTextField = "Clientname"
    '            Clientname.DataValueField = "autoid"
    '            Clientname.DataSource = dsst
    '            Clientname.DataBind()
    '            Clientname.Items.Insert("0", "--Select--")
    '        Else
    '            Clientname.Items.Clear()
    '            cboLOB.Items.Clear()
    '        End If
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(ex.ToString)
    '    Finally
    '        connection.Close()
    '    End Try
    '    cboLOB.Items.Clear()

    'End Sub

    'Protected Sub Clientname_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Clientname.SelectedIndexChanged
    '    Try
    '        If DepartmentName.SelectedIndex <> 0 And Clientname.SelectedIndex <> 0 Then
    '            Dim cmdst As New SqlCommand("select *  from warslobmaster where Deptid='" & DepartmentName.SelectedValue & "' and Clientid ='" & Clientname.SelectedValue & "'", connection)
    '            Dim dsst As New Data.DataSet
    '            Dim adpst As New SqlDataAdapter
    '            adpst.SelectCommand = cmdst
    '            connection.Open()
    '            Dim cntr = adpst.Fill(dsst)
    '            connection.Close()
    '            cboLOB.DataTextField = "LOBName"
    '            cboLOB.DataValueField = "autoid"
    '            cboLOB.DataSource = dsst
    '            cboLOB.DataBind()
    '            cboLOB.Items.Insert("0", "--Select--")
    '        Else
    '            cboLOB.Items.Clear()
    '        End If
    '    Catch ex As Exception
    '        Dim strmsg As String
    '        strmsg = Replace(ex.Message.ToString, "'", "")
    '        strmsg = Replace(strmsg, vbCrLf, " ")
    '        ShowConfirm(ex.ToString)
    '    Finally
    '        connection.Close()
    '    End Try
    'End Sub

   
End Class

